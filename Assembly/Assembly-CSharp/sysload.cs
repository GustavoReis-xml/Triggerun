using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F6 RID: 1014
public class sysload : MonoBehaviour
{
	// Token: 0x06002AA9 RID: 10921 RVA: 0x00177FB4 File Offset: 0x001761B4
	public static void loadImageBySprite(string spath, sysload.loadImageFinish func)
	{
		spath = "Icon/" + spath;
		bool flag = func != null && !sysload.dicLoadImages.ContainsKey(spath);
		if (flag)
		{
			sysload.dicLoadImages.Add(spath, new sysload.cloadimage(func));
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(spath);
		string url = spath;
		fResourceManager.DownFinishDelegate callBackFun;
		if ((callBackFun = sysload.O_cache._State_0_loadImageBySprite) == null)
		{
			callBackFun = (sysload.O_cache._State_0_loadImageBySprite = new fResourceManager.DownFinishDelegate(sysload.loadImageBySprite));
		}
		GameRoot.LoadAsset(url, callBackFun, arrayList);
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x0017802C File Offset: 0x0017622C
	private static void loadImageBySprite(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("loadImageBySprite资源问题");
		}
		else
		{
			ArrayList arrayList = (ArrayList)customParam;
			Sprite oImg = null;
			string key = (string)arrayList[0];
			bool flag2 = arrayList[1].GetType() == typeof(Sprite);
			if (flag2)
			{
				oImg = (Sprite)arrayList[1];
			}
			bool flag3 = sysload.dicLoadImages.ContainsKey(key);
			if (flag3)
			{
				sysload.dicLoadImages[key].func(oImg, null);
				sysload.dicLoadImages.Remove(key);
			}
		}
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x001780D8 File Offset: 0x001762D8
	public static void loadImage(RawImage image, string spath, sysload.loadImageFinish func)
	{
		bool flag = !(image == null);
		if (flag)
		{
			image.enabled = false;
			spath = "Icon/" + spath;
			bool flag2 = func != null && !sysload.dicLoadImages.ContainsKey(spath);
			if (flag2)
			{
				sysload.dicLoadImages.Add(spath, new sysload.cloadimage(func));
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(spath);
			arrayList.Add(image);
			string url = spath;
			fResourceManager.DownFinishDelegate callBackFun;
			if ((callBackFun = sysload.O_cache._State_1_loadImageOK) == null)
			{
				callBackFun = (sysload.O_cache._State_1_loadImageOK = new fResourceManager.DownFinishDelegate(sysload.loadImageOK));
			}
			GameRoot.LoadAsset(url, callBackFun, arrayList);
		}
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x00178170 File Offset: 0x00176370
	private static void loadImageOK(WWW www, object customParam)
	{
		bool flag = www == null || !string.IsNullOrEmpty(www.error);
		if (flag)
		{
			Debug.LogError("loadImageOK资源问题");
		}
		else
		{
			ArrayList arrayList = (ArrayList)customParam;
			string key = (string)arrayList[0];
			RawImage rawImage = (RawImage)arrayList[1];
			bool flag2 = !(rawImage == null);
			if (flag2)
			{
				bool flag3 = rawImage.texture == null;
				if (flag3)
				{
					rawImage.texture = new Texture2D(4, 4);
				}
				www.LoadImageIntoTexture((Texture2D)rawImage.texture);
				rawImage.SetNativeSize();
				rawImage.enabled = true;
				bool flag4 = sysload.dicLoadImages.ContainsKey(key);
				if (flag4)
				{
					sysload.dicLoadImages[key].func(rawImage.texture, rawImage);
					sysload.dicLoadImages.Remove(key);
				}
			}
		}
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x0017825C File Offset: 0x0017645C
	public static void loadImageNoSetN(RawImage image, string spath, sysload.loadImageFinish func)
	{
		spath = "Icon/" + spath;
		bool flag = func != null && !sysload.dicLoadImages.ContainsKey(spath);
		if (flag)
		{
			sysload.dicLoadImages.Add(spath, new sysload.cloadimage(func));
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(spath);
		arrayList.Add(image);
		string url = spath;
		fResourceManager.DownFinishDelegate callBackFun;
		if ((callBackFun = sysload.O_cache._State_2_loadImageOKNoSetN) == null)
		{
			callBackFun = (sysload.O_cache._State_2_loadImageOKNoSetN = new fResourceManager.DownFinishDelegate(sysload.loadImageOKNoSetN));
		}
		GameRoot.LoadAsset(url, callBackFun, arrayList);
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x001782DC File Offset: 0x001764DC
	private static void loadImageOKNoSetN(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("loadImageOKNoSetN资源问题");
		}
		else
		{
			ArrayList arrayList = (ArrayList)customParam;
			string text = (string)arrayList[0];
			RawImage rawImage = (RawImage)arrayList[1];
			bool flag2 = rawImage == null;
			if (flag2)
			{
				Debug.LogError(text + "准备放入图片的 RawImage 为空");
			}
			else
			{
				bool flag3 = rawImage.texture == null;
				if (flag3)
				{
					rawImage.texture = new Texture2D(4, 4);
				}
				www.LoadImageIntoTexture((Texture2D)rawImage.texture);
				rawImage.enabled = true;
				bool flag4 = sysload.dicLoadImages.ContainsKey(text);
				if (flag4)
				{
					Debug.LogError("注意:这里内存泄露,不能www.texture");
					sysload.dicLoadImages[text].func(www.texture, rawImage);
					sysload.dicLoadImages.Remove(text);
				}
			}
		}
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x001783D8 File Offset: 0x001765D8
	public static void downloadImageInAdvance(ArrayList arrPaths, sysload.loadImageFinish func)
	{
		bool flag = func != null;
		if (flag)
		{
			int count = arrPaths.Count;
			sysload.cLoadimageinadvance value = new sysload.cLoadimageinadvance(func, count);
			for (int i = 0; i < count; i++)
			{
				string text = "Icon/" + (string)arrPaths[i];
				ArrayList arrayList = new ArrayList();
				arrayList.Add(text);
				arrayList.Add(value);
				string url = text;
				fResourceManager.DownFinishDelegate callBackFun;
				if ((callBackFun = sysload.O_cache._State_3_downloadImageInAdvanceOK) == null)
				{
					callBackFun = (sysload.O_cache._State_3_downloadImageInAdvanceOK = new fResourceManager.DownFinishDelegate(sysload.downloadImageInAdvanceOK));
				}
				GameRoot.LoadAsset(url, callBackFun, arrayList);
			}
		}
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x0017846C File Offset: 0x0017666C
	private static void downloadImageInAdvanceOK(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			Debug.LogError("注意1:这里内存泄露,不能www.texture");
			Texture texture = www.texture;
			bool flag2 = texture != null;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)customParam;
				string sPath = (string)arrayList[0];
				sysload.cLoadimageinadvance cLoadimageinadvance = (sysload.cLoadimageinadvance)arrayList[1];
				cLoadimageinadvance.nowCount++;
				cLoadimageinadvance.setRes(sPath, texture);
				bool flag3 = cLoadimageinadvance.nowCount >= cLoadimageinadvance.maxCount;
				if (flag3)
				{
					Dictionary<string, Texture> arrRecord = cLoadimageinadvance.arrRecord;
					cLoadimageinadvance.func(arrRecord, null);
				}
			}
		}
	}

	// Token: 0x0400243B RID: 9275
	private static Dictionary<string, sysload.cloadimage> dicLoadImages = new Dictionary<string, sysload.cloadimage>();

	// Token: 0x02000677 RID: 1655
	// (Invoke) Token: 0x06003938 RID: 14648
	public delegate void loadImageFinish(object oImg, object oRaw);

	// Token: 0x02000678 RID: 1656
	private class cloadimage
	{
		// Token: 0x0600393B RID: 14651 RVA: 0x001C82D0 File Offset: 0x001C64D0
		public cloadimage(sysload.loadImageFinish _func)
		{
			this.func = _func;
		}

		// Token: 0x040030A7 RID: 12455
		public sysload.loadImageFinish func;
	}

	// Token: 0x02000679 RID: 1657
	private class cLoadimageinadvance
	{
		// Token: 0x0600393C RID: 14652 RVA: 0x001C82E1 File Offset: 0x001C64E1
		public cLoadimageinadvance(sysload.loadImageFinish _func, int _max)
		{
			this.func = _func;
			this.maxCount = _max;
			this.nowCount = 0;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x001C830C File Offset: 0x001C650C
		public void setRes(string sPath, Texture obj)
		{
			bool flag = !this.arrRecord.ContainsKey(sPath);
			if (flag)
			{
				this.arrRecord.Add(sPath, obj);
			}
		}

		// Token: 0x040030A8 RID: 12456
		public sysload.loadImageFinish func;

		// Token: 0x040030A9 RID: 12457
		public int maxCount;

		// Token: 0x040030AA RID: 12458
		public int nowCount;

		// Token: 0x040030AB RID: 12459
		public Dictionary<string, Texture> arrRecord = new Dictionary<string, Texture>();
	}

	// Token: 0x0200067A RID: 1658
	[CompilerGenerated]
	private static class O_cache
	{
		// Token: 0x040030AC RID: 12460
		public static fResourceManager.DownFinishDelegate _State_0_loadImageBySprite;

		// Token: 0x040030AD RID: 12461
		public static fResourceManager.DownFinishDelegate _State_1_loadImageOK;

		// Token: 0x040030AE RID: 12462
		public static fResourceManager.DownFinishDelegate _State_2_loadImageOKNoSetN;

		// Token: 0x040030AF RID: 12463
		public static fResourceManager.DownFinishDelegate _State_3_downloadImageInAdvanceOK;
	}
}
