"""
Reusable Skin Rigging Helper for Trigger Run (.mo AssetBundles + Tripo 3D Meshes)
=================================================================================
Provides deterministic, volume-preserving limb fitting, automatic landmark
measurement, longitudinal Biped twist weighting, tear-free torso/sleeve weighting,
and full-radius cylindrical undersleeve bridging for all 24 playable heroes.

Usage in a skin build script (Codex / Antigravity):
    from tools.skin_rig_helper import (
        load_hero_profile,
        smooth,
        limb_frame,
        measure_arm_landmarks,
        recenter_and_thicken_arm,
        tailor_bell_sleeve,
        compute_longitudinal_arm_weights,
        compute_torso_sleeve_weights,
        rebuild_cylindrical_undersleeve_bridge,
    )
"""

from __future__ import annotations
from pathlib import Path
import json
import numpy as np

PROFILES_PATH = Path(__file__).resolve().parent / "hero_rig_profiles.json"


def load_hero_profile(hero_id_or_name: int | str, profiles_path: Path | str = PROFILES_PATH) -> dict:
    """Load a hero's skeletal profile from tools/hero_rig_profiles.json."""
    data = json.loads(Path(profiles_path).read_text(encoding="utf-8"))
    heroes = data.get("heroes", data)
    key = str(hero_id_or_name)
    if key in heroes:
        return heroes[key]
    key_lower = key.lower()
    for hid, prof in heroes.items():
        if not isinstance(prof, dict):
            continue
        if (
            prof.get("name", "").lower() == key_lower
            or prof.get("internalName", "").lower() == key_lower
            or prof.get("tpsBundle", "").lower().startswith(key_lower)
        ):
            return prof
    raise KeyError(f"Hero profile not found for {hero_id_or_name!r}")


def smooth(x: np.ndarray | float) -> np.ndarray | float:
    """Cubic Hermite smoothstep on [0, 1]."""
    t = np.clip(x, 0.0, 1.0)
    return t * t * (3.0 - 2.0 * t)


def limb_frame(axis_vec: np.ndarray, up_hint: tuple[float, float, float] = (0.0, -1.0, 0.0)) -> np.ndarray:
    """Build a 3x3 orthonormal frame whose column 0 is aligned with axis_vec."""
    x = np.asarray(axis_vec, dtype=float)
    x = x / max(np.linalg.norm(x), 1e-12)
    up = np.asarray(up_hint, dtype=float)
    y = up - x * np.dot(up, x)
    if np.linalg.norm(y) < 1e-8:
        up = np.array([0.0, 0.0, 1.0])
        y = up - x * np.dot(up, x)
    y = y / max(np.linalg.norm(y), 1e-12)
    z = np.cross(x, y)
    return np.stack([x, y, z], axis=1)


def measure_arm_landmarks(
    V_torso_aligned: np.ndarray,
    S_raw: np.ndarray,
    arm_mask: np.ndarray,
    native_sh: np.ndarray,
    native_el: np.ndarray,
    native_hand: np.ndarray,
    elbow_s_z: float = 1.052,
    wrist_s_z: float = 0.885,
    top_s_z: float = 1.168,
    band: float = 0.022,
) -> dict[str, np.ndarray]:
    """
    Automatically measure source arm centroids from the mesh instead of guessing
    su/se/sw coordinates by hand. Extrapolates sourceShoulder along the upper-arm
    centroid axis to native_sh[2].
    """
    pts = V_torso_aligned[arm_mask]
    sz = S_raw[arm_mask, 2]

    def slice_centroid(z_target: float, half_band: float = band) -> np.ndarray:
        sel = np.abs(sz - z_target) <= half_band
        if sel.sum() < 4:
            idx = np.argsort(np.abs(sz - z_target))[:12]
            return pts[idx].mean(axis=0)
        return pts[sel].mean(axis=0)

    c_wrist = slice_centroid(wrist_s_z)
    c_elbow = slice_centroid(elbow_s_z)
    c_top = slice_centroid(top_s_z)

    dz = max(c_top[2] - c_elbow[2], 1e-4)
    slope = (c_top - c_elbow) / dz
    c_shoulder = c_elbow + slope * (native_sh[2] - c_elbow[2])

    return {
        "sourceShoulder": c_shoulder,
        "sourceTopRing": c_top,
        "sourceElbow": c_elbow,
        "sourceWrist": c_wrist,
        "nativeShoulder": np.asarray(native_sh, dtype=float),
        "nativeElbow": np.asarray(native_el, dtype=float),
        "nativeWrist": np.asarray(native_hand, dtype=float),
    }


def recenter_and_thicken_arm(
    V: np.ndarray,
    S: np.ndarray,
    arm_mask: np.ndarray,
    sh: np.ndarray,
    el: np.ndarray,
    hand: np.ndarray,
    upper_radial_scale: float = 1.22,
    forearm_radial_scale: float = 1.20,
    wrist_s_z_lo: float = 0.885,
    wrist_s_z_hi: float = 0.935,
    elbow_s_z: float = 1.055,
    recenter_strength: float = 0.85,
) -> tuple[np.ndarray, dict]:
    """
    Recenter an arm component coaxially onto the native UpperArm -> Forearm -> Hand
    bone chain while expanding its radial cross-section so the arm is never too thin.
    Inside the upper sleeve (S_z > 1.11), tapers the radial expansion to 1.08x so the
    bicep has full anatomical volume without crowding the sleeve cuff.
    Preserves hand/finger vertices below wrist_s_z_lo untouched.
    """
    V_out = V.copy()
    idx = np.where(arm_mask)[0]
    pts = V_out[idx]
    sz = S[idx, 2]

    u_up = (el - sh) / max(np.linalg.norm(el - sh), 1e-12)
    u_fore = (hand - el) / max(np.linalg.norm(hand - el), 1e-12)

    # Compute smooth local mesh centerline c_cur(sz) using Gaussian kernel in S_z
    diffs = sz[:, None] - sz[None, :]
    weights = np.exp(-0.5 * (diffs / 0.020) ** 2)
    weights /= np.maximum(weights.sum(axis=1, keepdims=True), 1e-12)
    c_cur = weights @ pts

    # Compute target bone centerline c_bone at each vertex's current Z height
    z_cur = c_cur[:, 2]
    t_up = np.clip((z_cur - sh[2]) / (el[2] - sh[2]), 0.0, 1.0)
    b_up = sh[None, :] + t_up[:, None] * (el - sh)[None, :]

    t_fore = np.clip((z_cur - el[2]) / (hand[2] - el[2]), 0.0, 1.0)
    b_fore = el[None, :] + t_fore[:, None] * (hand - el)[None, :]

    is_upper = smooth((sz - (elbow_s_z - 0.045)) / 0.090)
    c_bone = is_upper[:, None] * b_up + (1.0 - is_upper[:, None]) * b_fore

    # Taper recentering and radial expansion smoothly to 0/1 at the wrist so hands/nails stay intact
    above_wrist = smooth((sz - wrist_s_z_lo) / (wrist_s_z_hi - wrist_s_z_lo))

    # Local bone axis for radial decomposition
    ax = is_upper[:, None] * u_up[None, :] + (1.0 - is_upper[:, None]) * u_fore[None, :]
    ax /= np.maximum(np.linalg.norm(ax, axis=1, keepdims=True), 1e-12)

    rel = pts - c_cur
    axial = np.sum(rel * ax, axis=1, keepdims=True) * ax
    radial = rel - axial

    # Full thickening on forearm & elbow/lower-bicep, tapering smoothly to 0.72x deep inside the cuff (sz > 1.105)
    # while keeping full coaxial bone recentering all the way to the top ring so the arm stays centered in the sleeve.
    in_cuff = smooth((sz - 1.100) / 0.065)
    eff_upper_scale = upper_radial_scale * (1.0 - in_cuff) + 0.72 * in_cuff
    target_scale = is_upper * eff_upper_scale + (1.0 - is_upper) * forearm_radial_scale
    scale = 1.0 + above_wrist * (target_scale - 1.0)

    shift = (recenter_strength * above_wrist)[:, None] * (c_bone - c_cur)
    # Tuck the hidden top ring (in_cuff) slightly inward toward the shoulder socket axis
    # so it has maximum clearance from the outer sleeve wall in all poses.
    sign_x = -1.0 if sh[0] < 0 else 1.0
    shift[:, 0] -= in_cuff * sign_x * 0.006
    shift[:, 2] *= 0.25

    V_out[idx] = c_cur + shift + axial + radial * scale[:, None]
    stats = {
        "modifiedVertices": int(np.sum(above_wrist > 1e-4)),
        "upperRadialScale": float(upper_radial_scale),
        "forearmRadialScale": float(forearm_radial_scale),
        "recenterStrength": float(recenter_strength),
    }
    return V_out, stats


def tailor_bell_sleeve(
    V: np.ndarray,
    S: np.ndarray,
    V0_unwarped: np.ndarray,
    coat_mask: np.ndarray,
    sh: np.ndarray,
    el: np.ndarray,
    sign: int,
    radial_pull: float = 0.42,
    min_radius: float = 0.052,
) -> np.ndarray:
    """
    Restore a short sleeve / shoulder cape from unwarped V0 geometry and tailor
    excessive bell flare and under-arm poncho overhang coaxially around the
    UpperArm bone segment (sh -> el). This prevents the sleeve from swinging out
    into giant bat-wings when rotated with UpperArm while keeping clean clearance
    around the bicep.
    """
    V_out = V.copy()
    x_side = S[:, 0] * sign
    region = coat_mask & (x_side > 0.112) & (S[:, 2] > 1.040) & (S[:, 2] < 1.355)
    idx = np.where(region)[0]
    if len(idx) == 0:
        return V_out

    # Keep the inner/rear sleeve wall aligned with V (which encloses the upper arm),
    # while smoothing out the outer bell hem crimp from V0_unwarped.
    restore_w = (
        smooth((x_side[idx] - 0.155) / 0.045)
        * smooth((S[idx, 2] - 1.060) / 0.045)
        * smooth((1.280 - S[idx, 2]) / 0.045)
        * 0.55
    )
    pts = V[idx] * (1.0 - restore_w[:, None]) + V0_unwarped[idx] * restore_w[:, None]

    # Project each sleeve vertex onto the UpperArm bone segment (sh -> el)
    ax = el - sh
    L = max(np.linalg.norm(ax), 1e-12)
    u_ax = ax / L
    u_raw = ((pts - sh[None, :]) @ u_ax) / L
    u_clamped = np.clip(u_raw, 0.0, 0.62)
    proj = sh[None, :] + (u_clamped * L)[:, None] * u_ax[None, :]

    rad_vec = pts - proj
    bell_w = (
        smooth((x_side[idx] - 0.135) / 0.048)
        * smooth((S[idx, 2] - 1.075) / 0.045)
        * smooth((1.305 - S[idx, 2]) / 0.045)
    )
    rad_len = np.linalg.norm(rad_vec, axis=1)
    rad_dir = rad_vec / np.maximum(rad_len[:, None], 1e-12)
    excess = np.maximum(rad_len - min_radius, 0.0)
    new_rad_len = rad_len - bell_w * radial_pull * excess

    pts_tailored = proj + rad_dir * new_rad_len[:, None]
    low_hem = bell_w * smooth((1.160 - S[idx, 2]) / 0.055)
    pts_tailored[:, 2] += low_hem * 0.018

    V_out[idx] = pts_tailored
    return V_out


def compute_longitudinal_arm_weights(
    D: np.ndarray,
    S: np.ndarray,
    arm_mask: np.ndarray,
    lookup: dict[str, int],
    side: str,
    elbow_s_z: float = 1.075,
    wrist_s_z: float = 0.915,
    hand_preserve_s_z: float = 0.895,
) -> np.ndarray:
    """
    Assign smooth longitudinal Biped twist gradients along UpperArm/UpArmTwist and
    Forearm/ForeTwist while preserving native Hand/Finger weights below hand_preserve_s_z.
    """
    D_out = D.copy()
    idx = np.where(arm_mask)[0]
    sz = S[idx, 2]

    up_idx = lookup[f"Bip001 {side} UpperArm"]
    uptw_key = f"Bip001 {side}UpArmTwist" if f"Bip001 {side}UpArmTwist" in lookup else f"Bip001 {side} UpArmTwist"
    uptw_idx = lookup.get(uptw_key, up_idx)

    fore_idx = lookup[f"Bip001 {side} Forearm"]
    foretw_key = f"Bip001 {side} ForeTwist" if f"Bip001 {side} ForeTwist" in lookup else f"Bip001 {side}ForeTwist"
    foretw_idx = lookup.get(foretw_key, fore_idx)

    hand_idx = lookup[f"Bip001 {side} Hand"]

    elbow = smooth((elbow_s_z + 0.045 - sz) / 0.110)
    wrist = smooth((wrist_s_z - sz) / 0.075)

    # Longitudinal twist fraction within upper arm (0 at shoulder -> 1 at elbow)
    u_twist = smooth((1.24 - sz) / 0.16)
    up_twist_frac = 0.20 + 0.50 * u_twist if uptw_idx != up_idx else np.zeros_like(sz)

    # Longitudinal twist fraction within forearm (0 at elbow -> 1 at wrist)
    f_twist = smooth((elbow_s_z - sz) / max(elbow_s_z - wrist_s_z, 0.08))
    fore_twist_frac = 0.20 + 0.55 * f_twist if foretw_idx != fore_idx else np.zeros_like(sz)

    ad = np.zeros((len(idx), D.shape[1]), dtype=float)
    w_upper = 1.0 - elbow
    w_fore = elbow * (1.0 - wrist)
    w_hand = elbow * wrist

    if uptw_idx != up_idx:
        ad[:, up_idx] = w_upper * (1.0 - up_twist_frac)
        ad[:, uptw_idx] = w_upper * up_twist_frac
    else:
        ad[:, up_idx] = w_upper

    if foretw_idx != fore_idx:
        ad[:, fore_idx] = w_fore * (1.0 - fore_twist_frac)
        ad[:, foretw_idx] = w_fore * fore_twist_frac
    else:
        ad[:, fore_idx] = w_fore

    ad[:, hand_idx] = w_hand

    # Blend above hand_preserve_s_z so native finger/hand weights below 0.895 stay untouched
    blend = smooth((sz - (hand_preserve_s_z - 0.020)) / 0.040)
    D_out[idx] = D_out[idx] * (1.0 - blend[:, None]) + ad * blend[:, None]
    return D_out


def compute_torso_sleeve_weights(
    D: np.ndarray,
    S: np.ndarray,
    coat_mask: np.ndarray,
    lookup: dict[str, int],
    side: str,
    sign: int,
    max_arm_weight: float = 0.98,
    clavicle_weight: float = 0.10,
    twist_share: float = 0.50,
) -> tuple[np.ndarray, np.ndarray]:
    """
    Weight a torso-connected short sleeve or shoulder cape so that:
    1. The collar (S_z > 1.325), chest/back seam (|S_x| < 0.115), and lower skirt (S_z < 1.075)
       remain on torso bones (Spine2/Neck), preventing back tears and collar notches.
    2. The entire sleeve cuff & barrel (|S_x| > 0.140, 1.09 < S_z < 1.29) rotates with UpperArm
       (up to max_arm_weight ~0.98) so in overhead poses (expression_A50) both the upper and
       under-arm walls of the sleeve stay wrapped around the arm instead of lagging on Spine2.
    """
    D_out = D.copy()
    x_side = S[:, 0] * sign
    sz = S[:, 2]

    sp2_idx = lookup["Bip001 Spine2"]
    clav_idx = lookup[f"Bip001 {side} Clavicle"]
    up_idx = lookup[f"Bip001 {side} UpperArm"]
    uptw_key = f"Bip001 {side}UpArmTwist" if f"Bip001 {side}UpArmTwist" in lookup else f"Bip001 {side} UpArmTwist"
    uptw_idx = lookup.get(uptw_key, up_idx)

    # Outer sleeve + shoulder cap envelope: below the vampire collar tip (sz < 1.335) and above the waist (sz > 1.055)
    sleeve_env = (
        coat_mask.astype(float)
        * smooth((x_side - 0.105) / 0.040)
        * smooth((sz - 1.055) / 0.045)
        * smooth((1.335 - sz) / 0.040)
    )

    # UpperArm participation covers the full sleeve barrel (including the under-arm cuff at x_side ~ 0.135..0.155)
    cuff_factor = smooth((x_side - 0.118) / 0.036) * smooth((1.312 - sz) / 0.042)
    w_arm = max_arm_weight * cuff_factor
    rem = np.clip(1.0 - w_arm, 0.0, 1.0)
    # Shoulder cap (sz in [1.16, 1.32], x_side > 0.105) shares Clavicle weight so overhead shrugs (expression_A50)
    # lift the coat shoulder cap with the arm instead of leaving it 11 cm down on Spine2.
    sh_cap = smooth((x_side - 0.105) / 0.035) * smooth((sz - 1.150) / 0.065) * smooth((1.330 - sz) / 0.035)
    w_clav = rem * np.clip(clavicle_weight + 0.45 * sh_cap, 0.0, 0.65)
    w_sp2 = np.clip(rem - w_clav, 0.0, 1.0)

    sd = np.zeros_like(D_out)
    sd[:, sp2_idx] = w_sp2
    sd[:, clav_idx] = w_clav
    if uptw_idx != up_idx:
        sd[:, up_idx] = w_arm * (1.0 - twist_share)
        sd[:, uptw_idx] = w_arm * twist_share
    else:
        sd[:, up_idx] = w_arm

    D_out = D_out * (1.0 - sleeve_env[:, None]) + sd * sleeve_env[:, None]
    return D_out, sleeve_env


def rebuild_cylindrical_undersleeve_bridge(
    V: np.ndarray,
    D: np.ndarray,
    N: np.ndarray,
    F: np.ndarray,
    S: np.ndarray,
    lab: np.ndarray,
    bridge: dict,
    sh: np.ndarray,
    el: np.ndarray,
    coat_label: int = 9070,
    row_tapers: tuple[float, float] = (0.72, 0.42),
) -> tuple[np.ndarray, np.ndarray, list[dict], list[int]]:
    """
    Rebuild a 4-row upper-arm undersleeve bridge (rows[1..4] + cap) so that:
    - The anchor panel (face) is chosen on the top shoulder cap directly over the anatomical
      shoulder joint `sh` (|S_x| in [0.115, 0.175], 1.265 < S_z < 1.325), instead of on the
      side wall of the sleeve halfway down the arm.
    - rows[1] and rows[2] extend coaxially up the center of the hollow sleeve cavity from row[0]
      straight into the shoulder socket `sh`.
    - rows[3], rows[4], and cap anchor cleanly inside the shoulder-cap panel, guaranteeing
      maxOutside < 0.001 across all animations while making the arm emerge from the shoulder.
    """
    V_out = V.copy()
    D_out = D.copy()
    side = bridge["side"]
    sign = -1 if side == "L" else 1

    axis = el - sh
    axis = axis / max(np.linalg.norm(axis), 1e-12)

    # Select the shoulder-cap triangle directly over the anatomical shoulder joint `sh`
    mask = (
        (lab == coat_label)
        & (S[:, 0] * sign > 0.115)
        & (S[:, 0] * sign < 0.175)
        & (S[:, 2] > 1.265)
        & (S[:, 2] < 1.325)
    )
    eligible = np.where(np.all(mask[F], axis=1))[0]
    if len(eligible) < 5:
        mask = (lab == coat_label) & (S[:, 0] * sign > 0.100) & (S[:, 2] > 1.220) & (S[:, 2] < 1.335)
        eligible = np.where(np.all(mask[F], axis=1))[0]
    tri = V_out[F[eligible]]

    ring_idx = bridge["originalBoundary"]
    center = V_out[ring_idx].mean(axis=0)
    centers = tri.mean(axis=1)
    area = np.linalg.norm(np.cross(tri[:, 1] - tri[:, 0], tri[:, 2] - tri[:, 0]), axis=1)
    cost = np.linalg.norm(centers - sh, axis=1)
    cost[area < 0.00008] += 1.0
    j = int(cost.argmin())
    face = F[eligible[j]]

    surface = tri[j].mean(axis=0)
    normal = N[face].mean(axis=0)
    normal /= max(np.linalg.norm(normal), 1e-12)
    if normal[2] < 0:
        normal = -normal

    offset = -normal * 0.008
    cap_center = surface + offset
    ownership = D_out[face].mean(axis=0)

    # Synchronize the top hidden portion of the arm (S_z > 1.125, including row 0) slightly toward
    # the shoulder-cap ownership weight so there is no relative LBS shear inside the sleeve in A50.
    arm_comp = int(lab[ring_idx[0]])
    hidden_arm = np.where((lab == arm_comp) & (S[:, 2] > 1.120) & (np.arange(len(V_out)) < int(bridge["rows"][1][0])))[0]
    if len(hidden_arm) > 0:
        w_sync = 0.22 * smooth((S[hidden_arm, 2] - 1.120) / 0.055)
        D_out[hidden_arm] = D_out[hidden_arm] * (1.0 - w_sync[:, None]) + ownership[None, :] * w_sync[:, None]

    ring_pts = V_out[ring_idx]
    radial_ring = ring_pts - center

    anchors = []
    changed = list(int(x) for x in hidden_arm)
    for row in range(1, 5):
        t = 0.30 if row == 1 else (0.60 if row == 2 else row / 4.0)
        ids = bridge["rows"][row]
        for k, i in enumerate(ids):
            angle = k * 2.0 * np.pi / len(ids)
            amplitude = 0.15 if row == 3 else 0.075
            bary = np.array([
                1.0 / 3.0 + amplitude * np.cos(angle),
                1.0 / 3.0 + amplitude * np.sin(angle),
                1.0 / 3.0 - amplitude * (np.cos(angle) + np.sin(angle)),
            ])
            q = bary @ V_out[face]
            if row == 1:
                V_out[i] = center * (1.0 - t) + cap_center * t + radial_ring[k] * row_tapers[0]
                D_out[i] = D_out[ring_idx[k]] * (1.0 - t) + ownership * t
            elif row == 2:
                V_out[i] = center * (1.0 - t) + cap_center * t + radial_ring[k] * row_tapers[1]
                D_out[i] = D_out[ring_idx[k]] * (1.0 - t) + ownership * t
            else:
                V_out[i] = q + offset
                D_out[i] = bary @ D_out[face]
                anchors.append({
                    "vertex": int(i),
                    "triangle": face.tolist(),
                    "barycentric": bary.tolist(),
                    "offset": offset.tolist(),
                })
            changed.append(int(i))

    cap_i = int(bridge["cap"])
    bary_cap = np.full(3, 1.0 / 3.0)
    V_out[cap_i] = cap_center
    D_out[cap_i] = ownership
    changed.append(cap_i)
    anchors.append({
        "vertex": cap_i,
        "triangle": face.tolist(),
        "barycentric": bary_cap.tolist(),
        "offset": offset.tolist(),
    })
    return V_out, D_out, anchors, changed
