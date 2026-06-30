using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class MirrorReflection : MonoBehaviour
{
	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06001A71 RID: 6769 RVA: 0x000FBB2C File Offset: 0x000F9D2C
	public static MirrorReflection instance
	{
		get
		{
			bool flag = MirrorReflection._instance == null;
			if (flag)
			{
				MirrorReflection._instance = (UnityEngine.Object.FindObjectOfType(typeof(MirrorReflection)) as MirrorReflection);
			}
			return MirrorReflection._instance;
		}
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000FBB70 File Offset: 0x000F9D70
	private void Awake()
	{
		bool flag = this.oceanMaterial == null;
		if (flag)
		{
			this.oceanMaterial = base.GetComponent<Renderer>().sharedMaterial;
		}
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000FBBA4 File Offset: 0x000F9DA4
	private void LateUpdate()
	{
		int num = 100;
		bool flag = this.enableMirrorReflection;
		if (flag)
		{
			num += 100;
		}
		this.alpha = Mathf.Clamp01(this.alpha);
		bool flag2 = this.alpha < 1f;
		if (flag2)
		{
			num += 50;
		}
		this.oceanMaterial.shader.maximumLOD = num;
		this.oceanMaterial.SetFloat("_Alpha", this.alpha);
		bool flag3 = this.enableMirrorReflection;
		if (flag3)
		{
			bool flag4 = this.cam == null;
			if (flag4)
			{
				this.cam = Camera.main.GetComponent<Camera>();
			}
			bool flag5 = this.oceanMaterial && this.cam;
			if (flag5)
			{
				Camera camera;
				this.CreateMirrorObjects(this.cam, out camera);
				Vector3 position = base.transform.position;
				Vector3 up = base.transform.up;
				this.UpdateCameraModes(this.cam, camera);
				float w = 0f - Vector3.Dot(up, position) - this.m_ClipPlaneOffset;
				Vector4 plane = new Vector4(up.x, up.y, up.z, w);
				Matrix4x4 zero = Matrix4x4.zero;
				MirrorReflection.CalculateReflectionMatrix(ref zero, plane);
				Vector3 position2 = this.cam.transform.position;
				Vector3 position3 = zero.MultiplyPoint(position2);
				camera.worldToCameraMatrix = this.cam.worldToCameraMatrix * zero;
				Vector4 clipPlane = this.CameraSpacePlane(camera, position, up, 1f);
				Matrix4x4 projectionMatrix = this.cam.projectionMatrix;
				MirrorReflection.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
				camera.projectionMatrix = projectionMatrix;
				camera.cullingMask = (-17 & this.m_ReflectLayers.value);
				camera.transform.position = position3;
				Vector3 eulerAngles = this.cam.transform.eulerAngles;
				camera.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
				camera.transform.position = position2;
				this.oceanMaterial.SetTexture("_ReflectionTex", MirrorReflection.m_ReflectionTexture);
				Matrix4x4 lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
				Vector3 lossyScale = base.transform.lossyScale;
				Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z));
				matrix4x = lhs * this.cam.projectionMatrix * this.cam.worldToCameraMatrix * matrix4x;
				this.oceanMaterial.SetMatrix("_ProjMatrix", matrix4x);
			}
		}
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000FBE90 File Offset: 0x000FA090
	private void OnDisable()
	{
		bool flag = MirrorReflection.m_ReflectionTexture;
		if (flag)
		{
			UnityEngine.Object.Destroy(MirrorReflection.m_ReflectionTexture);
			MirrorReflection.m_ReflectionTexture = null;
		}
		bool flag2 = this.m_ReflectionCamera;
		if (flag2)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionCamera.gameObject);
		}
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x000FBEE4 File Offset: 0x000FA0E4
	private void UpdateCameraModes(Camera src, Camera dest)
	{
		bool flag = dest == null;
		if (!flag)
		{
			dest.clearFlags = src.clearFlags;
			dest.backgroundColor = src.backgroundColor;
			bool flag2 = src.clearFlags == CameraClearFlags.Skybox;
			if (flag2)
			{
				Skybox skybox = src.GetComponent(typeof(Skybox)) as Skybox;
				Skybox skybox2 = dest.GetComponent(typeof(Skybox)) as Skybox;
				bool flag3 = !skybox || !skybox.material;
				if (flag3)
				{
					skybox2.enabled = false;
				}
				else
				{
					skybox2.enabled = true;
					skybox2.material = skybox.material;
				}
			}
			dest.farClipPlane = src.farClipPlane;
			dest.nearClipPlane = src.nearClipPlane;
			dest.orthographic = src.orthographic;
			dest.fieldOfView = src.fieldOfView;
			dest.aspect = src.aspect;
			dest.orthographicSize = src.orthographicSize;
		}
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x000FBFE8 File Offset: 0x000FA1E8
	private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera)
	{
		bool flag = !MirrorReflection.m_ReflectionTexture;
		if (flag)
		{
			MirrorReflection.m_ReflectionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
			MirrorReflection.m_ReflectionTexture.name = "__MirrorReflection" + base.GetInstanceID().ToString();
			MirrorReflection.m_ReflectionTexture.isPowerOfTwo = true;
			MirrorReflection.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
		}
		reflectionCamera = this.m_ReflectionCamera;
		bool flag2 = !reflectionCamera;
		if (flag2)
		{
			GameObject gameObject = new GameObject("Mirror Refl Camera id" + base.GetInstanceID().ToString() + " for " + currentCamera.GetInstanceID().ToString(), new Type[]
			{
				typeof(Camera),
				typeof(Skybox)
			});
			reflectionCamera = gameObject.GetComponent<Camera>();
			reflectionCamera.enabled = true;
			gameObject.hideFlags = HideFlags.DontSave;
			this.m_ReflectionCamera = reflectionCamera;
			reflectionCamera.gameObject.AddComponent<ReflectionCameraControl>();
		}
		reflectionCamera.depth = currentCamera.depth - 1f;
		reflectionCamera.fieldOfView = currentCamera.fieldOfView;
		reflectionCamera.transform.position = base.transform.position;
		reflectionCamera.transform.rotation = base.transform.rotation;
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000FC14C File Offset: 0x000FA34C
	private static float sgn(float a)
	{
		bool flag = a > 0f;
		float result;
		if (flag)
		{
			result = 1f;
		}
		else
		{
			bool flag2 = a < 0f;
			if (flag2)
			{
				result = -1f;
			}
			else
			{
				result = 0f;
			}
		}
		return result;
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x000FC190 File Offset: 0x000FA390
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.m_ClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 vector = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector.x, vector.y, vector.z, 0f - Vector3.Dot(lhs, vector));
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x000FC204 File Offset: 0x000FA404
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(MirrorReflection.sgn(clipPlane.x), MirrorReflection.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000FC2B4 File Offset: 0x000FA4B4
	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
	}

	// Token: 0x04001522 RID: 5410
	private int m_TextureSize = 512;

	// Token: 0x04001523 RID: 5411
	private float m_ClipPlaneOffset = 0.1f;

	// Token: 0x04001524 RID: 5412
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x04001525 RID: 5413
	private Camera m_ReflectionCamera;

	// Token: 0x04001526 RID: 5414
	public static RenderTexture m_ReflectionTexture;

	// Token: 0x04001527 RID: 5415
	public Material oceanMaterial;

	// Token: 0x04001528 RID: 5416
	public Camera cam;

	// Token: 0x04001529 RID: 5417
	public bool enableMirrorReflection = true;

	// Token: 0x0400152A RID: 5418
	public float alpha = 1f;

	// Token: 0x0400152B RID: 5419
	private static MirrorReflection _instance;
}
