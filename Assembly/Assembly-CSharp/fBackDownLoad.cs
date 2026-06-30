using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class fBackDownLoad : MonoBehaviour
{
	// Token: 0x06000A8C RID: 2700 RVA: 0x00060BAA File Offset: 0x0005EDAA
	private void Start()
	{
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00060BB0 File Offset: 0x0005EDB0
	public static fBackDownLoad GetInstance()
	{
		bool flag = fBackDownLoad.mInst == null;
		if (flag)
		{
			GameObject gameObject = new GameObject("_fBackDownLoad");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			fBackDownLoad.mInst = gameObject.AddComponent<fBackDownLoad>();
			UnityEngine.Object.DontDestroyOnLoad(fBackDownLoad.mInst);
		}
		return fBackDownLoad.mInst;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00060C00 File Offset: 0x0005EE00
	public void doLoad()
	{
		bool flag = !this.bIsRuning && this.bRun && SysData.m_bgloading.Count > this.iLoadID;
		if (flag)
		{
			this.bIsRuning = true;
			bgloading bgloading = SysData.m_bgloading[this.iLoadID];
			string url = bgloading.url;
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFun), this.iLoadID);
			this.iLoadID++;
		}
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x00060C84 File Offset: 0x0005EE84
	private void LoadFun(WWW wwwParam, object customParam)
	{
		this.bIsRuning = false;
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00060C8E File Offset: 0x0005EE8E
	private void Update()
	{
		this.doLoad();
	}

	// Token: 0x040008A8 RID: 2216
	public static fBackDownLoad mInst;

	// Token: 0x040008A9 RID: 2217
	private int iLoadID;

	// Token: 0x040008AA RID: 2218
	public bool bRun;

	// Token: 0x040008AB RID: 2219
	private bool bIsRuning;
}
