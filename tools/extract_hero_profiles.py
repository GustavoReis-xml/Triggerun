from pathlib import Path
import sys, json, xml.etree.ElementTree as ET
import numpy as np

sys.path.insert(0, 'scratch/skin-inspection/python')
import UnityPy

HERO_NAMES = {
    1003: "Alice", 1005: "Glider", 1006: "Tagaloa", 1015: "Lars",
    1016: "Valkyrie", 1017: "Celina", 1021: "Carmen", 1022: "Scarlett",
    1023: "Brigit", 1024: "Shogun", 1025: "Eva", 1026: "Dragos",
    1027: "Jack", 1028: "Loki", 1029: "Angel", 1030: "Vincent",
    1031: "Johnny", 1032: "Venus (DaJi)", 1033: "Lily (Lilith)",
    1034: "Khan (HuangZhong)", 1035: "NeZha", 1036: "Gaya (Medusa)",
    1037: "David (DaWei)", 2000: "Lamster"
}

root = ET.parse('public/baxi/heroinfo.xml').getroot()
unlock_root = ET.parse('public/baxi/herounlock.xml').getroot()
unlock_map = {int(el.attrib['heroid']): el for el in unlock_root.findall('i') if 'heroid' in el.attrib}

bundle_dir = Path('downloads/cdn_cache/res/1.1.1/StreamingAssets/HeroAnimator')
profiles = {}

for el in root.findall('i'):
    hid = int(el.attrib['id'])
    if hid not in HERO_NAMES:
        continue
    tps_model = el.attrib.get('tps_model', '')
    fps_model = el.attrib.get('fps_model', '')
    heroclass = el.attrib.get('heroclass', '')
    career = int(el.attrib.get('career', 1))

    # Check default skin controller in herounlock.xml
    tps_controller = ''
    fps_controller = ''
    if hid in unlock_map:
        sk = unlock_map[hid].find('skin[@id="1"]')
        if sk is not None:
            tps_controller = sk.attrib.get('controller', '')
            fps_controller = sk.attrib.get('controller1', '')
            if not (bundle_dir / f'{tps_model}.mo').exists() and sk.attrib.get('model'):
                tps_model = sk.attrib.get('model')
            if not (bundle_dir / f'{fps_model}.mo').exists() and sk.attrib.get('model1'):
                fps_model = sk.attrib.get('model1')

    mo_path = bundle_dir / f'{tps_model}.mo'
    info = {
        'heroId': hid,
        'name': HERO_NAMES[hid],
        'internalName': el.attrib.get('head', ''),
        'heroClass': heroclass,
        'career': career,
        'tpsBundle': f'{tps_model}.mo',
        'fpsBundle': f'{fps_model}.mo',
        'tpsController': f'{tps_controller}.mo' if tps_controller else '',
        'fpsController': f'{fps_controller}.mo' if fps_controller else '',
        'exists': mo_path.exists()
    }

    if mo_path.exists():
        env = UnityPy.load(str(mo_path))
        objs = {o.path_id: o for o in env.objects}
        trees = {i: o.read_typetree() for i, o in objs.items() if o.type.name in ('Transform', 'GameObject')}
        def gname(tid):
            if tid not in trees: return ''
            goid = trees[tid]['m_GameObject']['m_PathID']
            return trees[goid]['m_Name'] if goid in trees else ''

        smrs = []
        main_smr = None
        main_verts = 0
        for o in env.objects:
            if o.type.name == 'SkinnedMeshRenderer':
                d = o.read_typetree()
                m_ptr = d['m_Mesh']['m_PathID']
                if m_ptr in objs:
                    md = objs[m_ptr].read_typetree()
                    vc = md['m_VertexData']['m_VertexCount']
                    ic = len(md['m_IndexBuffer']) // 6
                    go_name = trees[d['m_GameObject']['m_PathID']]['m_Name']
                    smrs.append({
                        'name': go_name,
                        'meshPathId': m_ptr,
                        'vertices': vc,
                        'triangles': ic,
                        'boneCount': len(d['m_Bones'])
                    })
                    if vc > main_verts and 'lod1' not in go_name.lower() and 'lod2' not in go_name.lower():
                        main_verts = vc
                        main_smr = (d, md, go_name)

        info['skinnedRenderers'] = smrs
        if main_smr is not None:
            d, md, go_name = main_smr
            bones = [gname(b['m_PathID']) for b in d['m_Bones']]
            bind = np.array([[[b[f'e{r}{c}'] for c in range(4)] for r in range(4)] for b in md['m_BindPose']])
            try:
                world = np.linalg.inv(bind)[:, :3, 3]
            except Exception:
                world = np.zeros((len(bones), 3))

            bmap = {n: world[i].round(4).tolist() for i, n in enumerate(bones)}
            key_joints = {}
            for k in [
                'Bip001 Pelvis', 'Bip001 Spine', 'Bip001 Spine1', 'Bip001 Spine2',
                'Bip001 Neck', 'Bip001 Head',
                'Bip001 L Clavicle', 'Bip001 L UpperArm', 'Bip001 L Forearm', 'Bip001 L Hand',
                'Bip001 R Clavicle', 'Bip001 R UpperArm', 'Bip001 R Forearm', 'Bip001 R Hand',
                'Bip001 L Thigh', 'Bip001 L Calf', 'Bip001 L Foot',
                'Bip001 R Thigh', 'Bip001 R Calf', 'Bip001 R Foot'
            ]:
                if k in bmap:
                    key_joints[k] = bmap[k]

            twist_bones = [n for n in bones if 'Twist' in n or 'twist' in n]
            extra_bones = [n for n in bones if not n.startswith('Bip001')]

            # Compute anatomical limb lengths for Tripo scaling
            lengths = {}
            if 'Bip001 L UpperArm' in bmap and 'Bip001 L Forearm' in bmap:
                lengths['upperArmLength'] = round(float(np.linalg.norm(np.array(bmap['Bip001 L Forearm']) - np.array(bmap['Bip001 L UpperArm']))), 4)
            if 'Bip001 L Forearm' in bmap and 'Bip001 L Hand' in bmap:
                lengths['forearmLength'] = round(float(np.linalg.norm(np.array(bmap['Bip001 L Hand']) - np.array(bmap['Bip001 L Forearm']))), 4)
            if 'Bip001 L UpperArm' in bmap and 'Bip001 R UpperArm' in bmap:
                lengths['shoulderSpan'] = round(float(np.linalg.norm(np.array(bmap['Bip001 R UpperArm']) - np.array(bmap['Bip001 L UpperArm']))), 4)
            if 'Bip001 Pelvis' in bmap and 'Bip001 Head' in bmap:
                lengths['pelvisHeight'] = round(float(bmap['Bip001 Pelvis'][2]), 4)
                lengths['shoulderHeight'] = round(float(bmap.get('Bip001 L UpperArm', [0,0,0])[2]), 4)
                lengths['headPivotHeight'] = round(float(bmap['Bip001 Head'][2]), 4)

            info['mainMesh'] = {
                'rendererName': go_name,
                'meshPathId': d['m_Mesh']['m_PathID'],
                'boneCount': len(bones),
                'twistBones': twist_bones,
                'extraClothOrWeaponBones': extra_bones,
                'keyJoints': key_joints,
                'proportions': lengths
            }

    profiles[str(hid)] = info

out_path = Path('tools/hero_rig_profiles.json')
out_path.parent.mkdir(parents=True, exist_ok=True)
out_path.write_text(json.dumps(profiles, indent=2, ensure_ascii=False), encoding='utf-8')
print('PROFILES_EXTRACTED:', len(profiles))
