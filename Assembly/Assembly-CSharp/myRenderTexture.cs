using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024D RID: 589
public class myRenderTexture
{
	// Token: 0x06001A8D RID: 6797 RVA: 0x000FCF38 File Offset: 0x000FB138
	public myRenderTexture(string _name, int iRTAddCount, RawImage ri)
	{
		this.rtname = _name;
		RectTransform component = ri.gameObject.GetComponent<RectTransform>();
		this.rtmRoot = new GameObject(this.rtname);
		this.rtmRoot.transform.SetParent(fRenderTextureManager.mRTObj.transform);
		this.rtmRoot.transform.localPosition = new Vector3((float)(this.CameraDistance * iRTAddCount), 0f, 0f);
		this.rtmRoot.layer = this.RTLayer;
		GameObject gameObject = new GameObject("RenderTextureCamera");
		gameObject.transform.SetParent(this.rtmRoot.transform);
		gameObject.transform.localPosition = new Vector3(0f, 0f, -2f);
		gameObject.layer = this.RTLayer;
		this.mCamera = gameObject.AddComponent<Camera>();
		this.mCamera.orthographic = true;
		this.mCamera.orthographicSize = 1f;
		this.mCamera.clearFlags = CameraClearFlags.Depth;
		this.mCamera.farClipPlane = 100f;
		Vector2 sizeDelta = component.sizeDelta;
		Rect rect = new Rect(0f, 0f, sizeDelta.x, sizeDelta.y);
		this.mRenderTexture = new RenderTexture((int)rect.width, (int)rect.height, 0);
		this.mRenderTexture.depth = 24;
		this.mCamera.targetTexture = this.mRenderTexture;
		this.mCamera.cullingMask = 1 << this.RTLayer;
		this.mCamera.Render();
		RenderTexture.active = this.mRenderTexture;
		ri.texture = this.mRenderTexture;
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000FD118 File Offset: 0x000FB318
	public Dictionary<string, GameObject> AddGameObject(string obName, GameObject gameob, Vector3 lp)
	{
		bool flag = !myRenderTexture.m_GameObject.ContainsKey(obName);
		if (flag)
		{
			gameob.transform.SetParent(this.rtmRoot.transform);
			gameob.transform.localPosition = lp;
			gameob.layer = this.RTLayer;
			myRenderTexture.m_GameObject.Add(obName, gameob);
		}
		else
		{
			myRenderTexture.m_GameObject[obName].transform.localPosition = lp;
		}
		return myRenderTexture.m_GameObject;
	}

	// Token: 0x0400154F RID: 5455
	public string rtname = "rootrtm";

	// Token: 0x04001550 RID: 5456
	public GameObject rtmRoot;

	// Token: 0x04001551 RID: 5457
	public RenderTexture mRenderTexture;

	// Token: 0x04001552 RID: 5458
	public Camera mCamera;

	// Token: 0x04001553 RID: 5459
	public static Dictionary<string, GameObject> m_GameObject = new Dictionary<string, GameObject>();

	// Token: 0x04001554 RID: 5460
	private int CameraDistance = 2000;

	// Token: 0x04001555 RID: 5461
	private int RTLayer = 8;
}
