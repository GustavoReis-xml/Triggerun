using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000130 RID: 304
public class fRenderTextureManager : MonoBehaviour
{
	// Token: 0x06000B6C RID: 2924 RVA: 0x0006B348 File Offset: 0x00069548
	public static fRenderTextureManager GetInstance()
	{
		bool flag = fRenderTextureManager.mInst == null;
		if (flag)
		{
			fRenderTextureManager.mRTObj = new GameObject("_fRenderTextureManager");
			fRenderTextureManager.mInst = fRenderTextureManager.mRTObj.AddComponent<fRenderTextureManager>();
			fRenderTextureManager.mRTObj.transform.SetParent(fUIManager.mUIObj.transform);
		}
		return fRenderTextureManager.mInst;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0006B3A8 File Offset: 0x000695A8
	public myRenderTexture CreateOrGetRT(string rtname, RawImage ri)
	{
		bool flag = !fRenderTextureManager.m_RenderTexture.ContainsKey(rtname);
		myRenderTexture myRenderTexture;
		if (flag)
		{
			myRenderTexture = new myRenderTexture(rtname, this.iRTAddCount, ri);
			fRenderTextureManager.m_RenderTexture.Add(rtname, myRenderTexture);
			this.iRTAddCount++;
		}
		else
		{
			myRenderTexture = fRenderTextureManager.m_RenderTexture[rtname];
		}
		return myRenderTexture;
	}

	// Token: 0x040009DD RID: 2525
	private static Dictionary<string, myRenderTexture> m_RenderTexture = new Dictionary<string, myRenderTexture>();

	// Token: 0x040009DE RID: 2526
	public static fRenderTextureManager mInst;

	// Token: 0x040009DF RID: 2527
	public static GameObject mRTObj;

	// Token: 0x040009E0 RID: 2528
	private int iRTAddCount;
}
