using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x02000244 RID: 580
[Serializable]
public class MaterialTag
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000F9F1F File Offset: 0x000F811F
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x000F9F28 File Offset: 0x000F8128
	public void Init(XmlElement xml)
	{
		this.loaded = false;
		this.id = int.Parse(xml.GetAttribute("id"));
		this.sTag = ((!(xml.GetAttribute("tag") == "0")) ? xml.GetAttribute("tag") : string.Empty);
		this.sItemName = ((!(xml.GetAttribute("name") == "0")) ? xml.GetAttribute("name") : string.Empty);
		this.sBaozhaName = ((!(xml.GetAttribute("baozhaname") == "0")) ? xml.GetAttribute("baozhaname") : string.Empty);
		this.sBaozhaName_little = string.Empty;
		this.sBaozhaName_little = ((!(xml.GetAttribute("baozhaname_little") == "0")) ? xml.GetAttribute("baozhaname_little") : string.Empty);
		this.fBaozhaDelTime = float.Parse(xml.GetAttribute("baozhandelTime"));
		this.sDecal = ((!(xml.GetAttribute("decal") == "0")) ? xml.GetAttribute("decal") : string.Empty);
		this.allDecalName = this.sDecal.Split(new char[]
		{
			'#'
		});
		this.allDecalObj = new ArrayList();
		this.sSound = ((!(xml.GetAttribute("baozhasound") == "0")) ? xml.GetAttribute("baozhasound") : string.Empty);
		this.allSoundName = this.sSound.Split(new char[]
		{
			'#'
		});
		this.allSoundObj = new ArrayList();
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x000FA0DC File Offset: 0x000F82DC
	private void LoadEffectCom(WWW www, object param)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)param;
			string text = (string)arrayList[0];
			string text2 = (string)arrayList[1];
			string a = text2;
			if (!(a == "baozha"))
			{
				if (!(a == "baozha_little"))
				{
					if (!(a == "decal"))
					{
						if (a == "sound")
						{
							this.allSoundObj.Add(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
						}
					}
					else
					{
						this.allDecalObj.Add(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
					}
				}
				else
				{
					this.BaozhaObj_little = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
				}
			}
			else
			{
				this.BaozhaObj = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			}
		}
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x000FA1EC File Offset: 0x000F83EC
	public void checkLoaded()
	{
		bool flag = this.loaded;
		if (!flag)
		{
			this.loaded = true;
			bool flag2 = this.sBaozhaName != string.Empty;
			if (flag2)
			{
				string text = "EffectRes/" + this.sBaozhaName + ".ef";
				ArrayList arrayList = new ArrayList();
				arrayList.Add(text);
				arrayList.Add("baozha");
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadEffectCom), arrayList);
			}
			bool flag3 = this.sBaozhaName_little != string.Empty;
			if (flag3)
			{
				string text2 = "EffectRes/" + this.sBaozhaName_little + ".ef";
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(text2);
				arrayList2.Add("baozha_little");
				GameRoot.LoadAsset(text2, new fResourceManager.DownFinishDelegate(this.LoadEffectCom), arrayList2);
			}
			bool flag4 = this.allDecalName.Length != 0;
			if (flag4)
			{
				for (int i = 0; i < this.allDecalName.Length; i++)
				{
					bool flag5 = !(this.allDecalName[i] == string.Empty);
					if (flag5)
					{
						string text3 = "EffectRes/" + this.allDecalName[i] + ".ef";
						ArrayList arrayList3 = new ArrayList();
						arrayList3.Add(text3);
						arrayList3.Add("decal");
						GameRoot.LoadAsset(text3, new fResourceManager.DownFinishDelegate(this.LoadEffectCom), arrayList3);
					}
				}
			}
			bool flag6 = this.allSoundName.Length == 0;
			if (!flag6)
			{
				for (int j = 0; j < this.allSoundName.Length; j++)
				{
					bool flag7 = !(this.allSoundName[j] == string.Empty);
					if (flag7)
					{
						string text4 = "SoundRes/" + this.allSoundName[j] + ".sds";
						ArrayList arrayList4 = new ArrayList();
						arrayList4.Add(text4);
						arrayList4.Add("sound");
						GameRoot.LoadAsset(text4, new fResourceManager.DownFinishDelegate(this.LoadEffectCom), arrayList4);
					}
				}
			}
		}
	}

	// Token: 0x040014F5 RID: 5365
	public int id;

	// Token: 0x040014F6 RID: 5366
	public string sTag;

	// Token: 0x040014F7 RID: 5367
	public string sItemName;

	// Token: 0x040014F8 RID: 5368
	public string sBaozhaName;

	// Token: 0x040014F9 RID: 5369
	public string sBaozhaName_little;

	// Token: 0x040014FA RID: 5370
	public float fBaozhaDelTime;

	// Token: 0x040014FB RID: 5371
	public string sDecal;

	// Token: 0x040014FC RID: 5372
	public string[] allDecalName;

	// Token: 0x040014FD RID: 5373
	public UnityEngine.Object BaozhaObj;

	// Token: 0x040014FE RID: 5374
	public UnityEngine.Object BaozhaObj_little;

	// Token: 0x040014FF RID: 5375
	public ArrayList allDecalObj;

	// Token: 0x04001500 RID: 5376
	public string sSound;

	// Token: 0x04001501 RID: 5377
	public string[] allSoundName;

	// Token: 0x04001502 RID: 5378
	public ArrayList allSoundObj;

	// Token: 0x04001503 RID: 5379
	private bool loaded;
}
