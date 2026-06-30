using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DA RID: 986
public class SkyLadderLoadUICon : Window
{
	// Token: 0x06002A18 RID: 10776 RVA: 0x0017261C File Offset: 0x0017081C
	protected override void Awake()
	{
		base.Awake();
		this.backgroundRImg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.sceneNameTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.szTeamRender = new List<GameObject>();
		GameObject item = this.UIGameObj.Get_GameObj(2);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(3);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(4);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(5);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(6);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(7);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(8);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(9);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(10);
		this.szTeamRender.Add(item);
		item = this.UIGameObj.Get_GameObj(11);
		this.szTeamRender.Add(item);
		SkyLadderLoadUICon.inst = this;
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x00172778 File Offset: 0x00170978
	public void ShowUserInfo(ArrayList szInfo)
	{
		ArrayList arrayList = (ArrayList)szInfo[1];
		int key = (int)szInfo[2];
		bool flag = !fGameManager.mloadConfig.FbData.ContainsKey(key);
		if (flag)
		{
			Debug.LogError("SkyLadderLoad***副本id不存在" + key.ToString());
		}
		else
		{
			FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[key];
			fUIManager.GetInstance().GetIconAsset(string.Format("selectherobg/{0}.txt", fbXmlData.nloadmap), this.backgroundRImg);
			this.sceneNameTxt.text = fbXmlData.sName;
			int num = 0;
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				GameObject gameObject = this.szTeamRender[num];
				bool flag2 = string.IsNullOrEmpty(arrayList[i].ToString());
				if (flag2)
				{
					Text component = gameObject.transform.Find("Score").GetComponent<Text>();
					component.text = string.Empty;
					component = gameObject.transform.Find("Name").GetComponent<Text>();
					component.text = string.Empty;
					RawImage component2 = gameObject.transform.Find("FaceIcon").GetComponent<RawImage>();
					GetIconComponent component3 = component2.transform.GetComponent<GetIconComponent>();
					bool flag3 = component3 != null;
					if (flag3)
					{
						component3._Url = string.Empty;
					}
					component2.enabled = false;
					Image component4 = gameObject.transform.Find("Image").GetComponent<Image>();
					component4.enabled = false;
				}
				else
				{
					ArrayList arrayList2 = (ArrayList)arrayList[i + 1];
					ArrayList arrayList3 = (ArrayList)arrayList2[19];
					Text component5 = gameObject.transform.Find("Score").GetComponent<Text>();
					bool flag4 = (int)arrayList3[0] > 0;
					if (flag4)
					{
						component5.text = arrayList3[0].ToString();
					}
					else
					{
						component5.text = Language.GetLang(24007, new object[0]);
					}
					component5 = gameObject.transform.Find("Name").GetComponent<Text>();
					string text = arrayList[i].ToString();
					bool flag5 = (int)arrayList2[12] != 0;
					if (flag5)
					{
						text = (((int)arrayList2[12] != TeamManager.GetInstance().m_nTeamID) ? ("<color=#ff9d00>" + arrayList[i].ToString() + "</color>") : ("<color=#00ff00>" + arrayList[i].ToString() + "</color>"));
					}
					component5.text = text;
					RawImage component6 = gameObject.transform.Find("FaceIcon").GetComponent<RawImage>();
					bool flag6 = fGameManager.mloadConfig.AllFaceData.ContainsKey((int)arrayList2[10]);
					if (flag6)
					{
						PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[(int)arrayList2[10]];
						fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component6);
					}
					Image component7 = gameObject.transform.Find("Image").GetComponent<Image>();
					component5 = gameObject.transform.Find("Image/NumTxt").GetComponent<Text>();
					Image component8 = gameObject.transform.Find("NoLvl").GetComponent<Image>();
					bool flag7 = (int)arrayList3[1] > 0;
					if (flag7)
					{
						int num2 = (int)arrayList3[1];
						component5.text = string.Empty;
						component7.enabled = true;
						component7.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num2.ToString());
						component8.enabled = false;
					}
					else
					{
						component5.text = string.Empty;
						component7.enabled = false;
						component8.enabled = true;
					}
				}
				num++;
			}
		}
	}

	// Token: 0x0400239F RID: 9119
	public static SkyLadderLoadUICon inst;

	// Token: 0x040023A0 RID: 9120
	private RawImage backgroundRImg;

	// Token: 0x040023A1 RID: 9121
	private Text sceneNameTxt;

	// Token: 0x040023A2 RID: 9122
	private List<GameObject> szTeamRender;
}
