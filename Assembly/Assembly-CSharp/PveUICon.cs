using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AF RID: 687
public class PveUICon : Window
{
	// Token: 0x06001F8C RID: 8076 RVA: 0x00121F88 File Offset: 0x00120188
	protected override void Awake()
	{
		base.Awake();
		PveUICon.inst = this;
		this.pToggleBtn_1 = this.UIGameObj.Get_GameObj(0);
		this.pToggleBtn_1.SetActive(false);
		this.pToggleList = this.UIGameObj.Get_GameObj(1);
		this.pFb_Panel = this.UIGameObj.Get_GameObj(3);
		this.pFb_Panel.SetActive(false);
		this.pFb_PanelList = this.UIGameObj.Get_GameObj(2);
		this.pItemTip = this.UIGameObj.Get_GameObj(4);
		this.pItemTip.SetActive(false);
		this.pQiangHuaBtn = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(this.pQiangHuaBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGoToQinagHuaClick);
		this.TimeSdr = this.UIGameObj.Get_GameObj(9).GetComponent<Slider>();
		this.pBuyTiLiBtn = this.UIGameObj.Get_GameObj(11);
		this.tTiLiDesc = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.tTiLiValue = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		EventTriggerListener.Get(this.pBuyTiLiBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTilIBtnClick);
		EventTriggerListener.Get(this.pBuyTiLiBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pQiangHuaBtn.SetActive(false);
		this.OnShowToggLeBtn();
		this.RefreshToggleBtn();
		this.OnClickToggleFun(this.defaultTgObj, false);
		this.UpdateByTiliNum();
		this.UpdateTiLi();
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x00122128 File Offset: 0x00120328
	private void OnGoToQinagHuaClick(GameObject obj)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(5, null);
		}
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00122154 File Offset: 0x00120354
	public void UpdateTiLi()
	{
		float num = (float)PlayerDataManager.getInstance().HUserData.m_nEnergy;
		this.TimeSdr.value = num / PveManager.GetInstance().m_nMaxEnergy;
		this.tTiLiValue.text = PlayerDataManager.getInstance().HUserData.m_nEnergy.ToString() + "/" + PveManager.GetInstance().m_nMaxEnergy.ToString();
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x001221CA File Offset: 0x001203CA
	public void OnTilIBtnClick(GameObject obj)
	{
		this.OnBuyTiLi(1);
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x001221D8 File Offset: 0x001203D8
	public void UpdateByTiliNum()
	{
		ArrayList pveDataByFbType = PveManager.GetInstance().GetPveDataByFbType(9);
		int num = (int)pveDataByFbType[7];
		DateTime dateTime = DateTime.Parse(pveDataByFbType[8].ToString());
		bool flag = Fun.GetServerDate().Date != dateTime.Date;
		if (flag)
		{
			num = 0;
		}
		this.tTiLiDesc.text = Language.GetLang(27038, new object[]
		{
			num
		});
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x0012225C File Offset: 0x0012045C
	public void OnBuyTiLi(int nBuyType)
	{
		ArrayList pveDataByFbType = PveManager.GetInstance().GetPveDataByFbType(9);
		string content = string.Empty;
		int num = (int)pveDataByFbType[7];
		DateTime dateTime = DateTime.Parse(pveDataByFbType[8].ToString());
		bool flag = Fun.GetServerDate().Date != dateTime.Date;
		if (flag)
		{
			num = 0;
		}
		bool flag2 = num >= 3;
		if (flag2)
		{
			bool flag3 = nBuyType == 1;
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27031, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			bool flag4 = nBuyType == 2;
			if (flag4)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27037, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			bool flag5 = this.GetBuyTiLiMoney(num) > PlayerDataManager.getInstance().playerdet.chargeCurrency;
			if (flag5)
			{
				fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
			}
			else
			{
				content = ((nBuyType != 1) ? Language.GetLang(27047, new object[]
				{
					this.GetBuyTiLiMoney(num)
				}) : Language.GetLang(27030, new object[]
				{
					this.GetBuyTiLiMoney(num)
				}));
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), content, Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnBuy), null, new TipManager.TipCancelDelegate(this.OnCancel), null, string.Empty, false, null, 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
			}
		}
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x00122475 File Offset: 0x00120675
	private void OnCancel(object obj)
	{
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x00122478 File Offset: 0x00120678
	private void OnBuy(object obj)
	{
		PveManager.GetInstance().OnSendMsgToServer(19513, new ArrayList
		{
			0,
			0
		});
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x001224AC File Offset: 0x001206AC
	private int GetBuyTiLiMoney(int nNum)
	{
		if (!true)
		{
		}
		int result;
		if (nNum != 0)
		{
			if (nNum != 1)
			{
				result = 200;
			}
			else
			{
				result = 100;
			}
		}
		else
		{
			result = 50;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x001224E4 File Offset: 0x001206E4
	private void OnShowToggLeBtn()
	{
		int num = 0;
		Dictionary<int, PveXmlData> allPveFbData = fGameManager.mloadConfig.AllPveFbData;
		foreach (PveXmlData pveXmlData in allPveFbData.Values)
		{
			bool flag = Fun.IsFunctionOpen("ShowPveTabBtn", pveXmlData.m_nFbType);
			if (flag)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pToggleBtn_1);
				gameObject.transform.SetParent(this.pToggleList.transform);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.name = pveXmlData.nId.ToString();
				gameObject.transform.Find("Panel/Label").GetComponent<Text>().text = pveXmlData.sName;
				EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				this.szAllToggle.Add(gameObject);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.pFb_Panel);
				gameObject2.transform.SetParent(this.pFb_PanelList.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject2.SetActive(false);
				num++;
				this.fbCon = gameObject2.AddComponent<Pve_Fb_Con>();
				this.fbCon.InitData(pveXmlData.nId, num);
				this.AllPanel.Add(num, gameObject2);
			}
		}
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x001226E0 File Offset: 0x001208E0
	private void RefreshToggleBtn()
	{
		DateTime serverDate = Fun.GetServerDate();
		for (int i = 0; i < this.szAllToggle.Count; i++)
		{
			int key = int.Parse(this.szAllToggle[i].name);
			Text component = this.szAllToggle[i].transform.Find("Panel/OpenTime").GetComponent<Text>();
			bool flag = fGameManager.mloadConfig.AllPveFbData.ContainsKey(key);
			if (flag)
			{
				PveXmlData pveXmlData = fGameManager.mloadConfig.AllPveFbData[key];
				bool flag2 = ActivityManager.GetInstance().IsOpenLimit(pveXmlData.m_nFbType) == 0;
				if (flag2)
				{
					DateTime d = DateTime.Parse(ActivityManager.GetInstance().GetFbOpenTime(pveXmlData.m_nFbType));
					bool flag3 = (d - serverDate).TotalHours > 24.0;
					if (flag3)
					{
						component.text = Language.GetLang(17022, new object[]
						{
							d.Month,
							d.Day
						});
					}
					else
					{
						component.text = Language.GetLang(17021, new object[0]);
					}
				}
				else
				{
					component.text = string.Empty;
					bool flag4 = this.defaultTgObj == null;
					if (flag4)
					{
						this.defaultTgObj = this.szAllToggle[i];
					}
				}
			}
			else
			{
				component.text = string.Empty;
			}
		}
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x00122870 File Offset: 0x00120A70
	private void OnClickToggleFun(GameObject obj, bool isClick)
	{
		if (obj == null)
		{
			return;
		}
		int num = int.Parse(obj.name.Replace("Toggle", string.Empty));
		if (!fGameManager.mloadConfig.AllPveFbData.ContainsKey(num))
		{
			return;
		}
		PveXmlData pveXmlData = fGameManager.mloadConfig.AllPveFbData[num];
		bool flag = ActivityManager.GetInstance().IsOpenLimit(pveXmlData.m_nFbType) == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = this.m_nPanelType != num;
			if (flag2)
			{
				this.m_nPanelType = num;
				bool flag3 = this.OnSelectPanel != null;
				if (flag3)
				{
					this.OnSelectPanel.SetActive(false);
				}
				if (this.AllPanel.ContainsKey(this.m_nPanelType))
				{
					this.AllPanel[this.m_nPanelType].gameObject.SetActive(true);
					this.OnSelectPanel = this.AllPanel[this.m_nPanelType].gameObject;
					Pve_Fb_Con fbCon = this.AllPanel[this.m_nPanelType].GetComponent<Pve_Fb_Con>();
					if (fbCon != null)
					{
						fbCon.InitData(pveXmlData.nId, this.m_nPanelType);
					}
				}
				bool flag4 = this.OnSelectToggleBtn != null;
				if (flag4)
				{
					this.OnSelectToggleBtn.GetComponent<Toggle>().enabled = true;
					this.OnSelectToggleBtn.GetComponent<Toggle>().interactable = true;
				}
				if (isClick)
				{
					obj.GetComponent<Toggle>().enabled = false;
				}
				obj.GetComponent<Toggle>().interactable = false;
				this.OnSelectToggleBtn = obj;
			}
		}
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x001229E9 File Offset: 0x00120BE9
	private void OnClickToggle(GameObject obj)
	{
		this.OnClickToggleFun(obj, true);
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x001229F8 File Offset: 0x00120BF8
	public override void Reset()
	{
		base.Reset();
		foreach (GameObject gameObject in this.AllPanel.Values)
		{
			Pve_Fb_Con component = gameObject.GetComponent<Pve_Fb_Con>();
			component.UpdateShow();
		}
		this.pItemTip.SetActive(false);
		this.RefreshToggleBtn();
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x00122A78 File Offset: 0x00120C78
	public Pve_Fb_Con GetFb_Con(int nType)
	{
		GameObject gameObject = this.AllPanel[nType];
		return gameObject.GetComponent<Pve_Fb_Con>();
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x00122AA0 File Offset: 0x00120CA0
	public void OnCheckZdl(FbXmlData fbdata, int nTypeLongId)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(nTypeLongId, new object[0]), Language.GetLang(26062, new object[0]), Language.GetLang(26063, new object[0]), new TipManager.TipOKDelegate(this.OnMatchCancel), null, new TipManager.TipCancelDelegate(this.OnTipOkPiPei), fbdata, string.Empty, false, null, 1, string.Empty, string.Empty, string.Empty);
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x00122B28 File Offset: 0x00120D28
	private void OnTipOkPiPei(object pData)
	{
		FbXmlData fbXmlData = (FbXmlData)pData;
		FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
		PveManager.GetInstance().OnPveOk(fbXmlData.nType, fbModelXmlData.m_nNanDu, fbXmlData.nId, 0);
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x00122B71 File Offset: 0x00120D71
	private void OnTipToggle(bool bstate)
	{
		this.IsCheckChatMoney = bstate;
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x00122B7B File Offset: 0x00120D7B
	private void OnMatchCancel(object obj)
	{
		this.IsCheckChatMoney = false;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x00122B88 File Offset: 0x00120D88
	public void OnUpdateSaoDang(int nFbType)
	{
		Pve_Fb_Con component = this.AllPanel[this.GetFbPanelByType(nFbType)].GetComponent<Pve_Fb_Con>();
		component.OnUpdateSaoDangData();
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x00122BB8 File Offset: 0x00120DB8
	public void ShowCaiLiaoNum(int nItemNum)
	{
		foreach (GameObject gameObject in this.AllPanel.Values)
		{
			Pve_Fb_Con component = gameObject.GetComponent<Pve_Fb_Con>();
			component.ShowCaiLiaoNum(nItemNum);
		}
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x00122C20 File Offset: 0x00120E20
	public void OnShowItemTip(int nType, ItemData pitemdata, int nHeroType = 0)
	{
		Text component = this.pItemTip.transform.FindChild("ItemName").GetComponent<Text>();
		Text component2 = this.pItemTip.transform.FindChild("Itemdesc").GetComponent<Text>();
		switch (nType)
		{
		case 1:
			component.text = Language.GetLang(26044, new object[0]);
			component2.text = Language.GetLang(26045, new object[0]).Replace("\\n", "\n");
			this.pItemTip.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 160f);
			break;
		case 2:
			component.text = pitemdata.m_sName;
			component2.text = pitemdata.m_sTipDesc;
			this.pItemTip.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 160f);
			break;
		case 3:
		{
			QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[nHeroType];
			string text = string.Empty;
			text = this.OnHeroList(qiangHuaHeroTypeXmlData);
			component.text = qiangHuaHeroTypeXmlData.sName;
			component2.text = text;
			this.pItemTip.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 60f + (float)qiangHuaHeroTypeXmlData.szHeroId.Count * 25f + 30f);
			break;
		}
		default:
			component.text = this.GetBuffTipName(nHeroType);
			component2.text = this.GetBuffTipDesc(nHeroType);
			this.pItemTip.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 160f);
			break;
		}
		this.pItemTip.SetActive(true);
		PveManager.GetInstance().mousepos(this.pItemTip, 20f, -20f);
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x00122DFC File Offset: 0x00120FFC
	private string GetBuffTipName(int nType)
	{
		int nMsgID = 0;
		switch (nType)
		{
		case 1:
			nMsgID = 27023;
			break;
		case 2:
			nMsgID = 27024;
			break;
		case 3:
			nMsgID = 27025;
			break;
		}
		return Language.GetLang(nMsgID, new object[0]);
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x00122E50 File Offset: 0x00121050
	private string GetBuffTipDesc(int nType)
	{
		int nMsgID = 0;
		switch (nType)
		{
		case 1:
			nMsgID = 27026;
			break;
		case 2:
			nMsgID = 27027;
			break;
		case 3:
			nMsgID = 27028;
			break;
		}
		return Language.GetLang(nMsgID, new object[0]);
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x00122EA4 File Offset: 0x001210A4
	public int GetFbPanelByType(int nFbType)
	{
		int result = 1;
		if (nFbType != 9)
		{
			if (nFbType == 12)
			{
				result = 2;
			}
		}
		else
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x00122ED3 File Offset: 0x001210D3
	public void OnHideItemTip()
	{
		this.pItemTip.SetActive(false);
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x00122EE4 File Offset: 0x001210E4
	public string OnHeroList(QiangHuaHeroTypeXmlData HeroTypeXmlData)
	{
		string text = string.Empty;
		ArrayList szHeroId = HeroTypeXmlData.szHeroId;
		int count = szHeroId.Count;
		for (int i = 0; i < count; i++)
		{
			int key = (int)szHeroId[i];
			bool flag = fGameManager.mloadConfig.AllHeroList.ContainsKey(key);
			if (flag)
			{
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[key];
				bool flag2 = text != string.Empty;
				if (flag2)
				{
					text += "\n";
				}
				text += heroData.hname;
			}
		}
		return text;
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x00122F88 File Offset: 0x00121188
	private void Start()
	{
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x00122F8B File Offset: 0x0012118B
	private void Update()
	{
	}

	// Token: 0x04001CB9 RID: 7353
	public static PveUICon inst;

	// Token: 0x04001CBA RID: 7354
	private GameObject pToggleBtn_1;

	// Token: 0x04001CBB RID: 7355
	private GameObject pToggleList;

	// Token: 0x04001CBC RID: 7356
	private GameObject pFb_Panel;

	// Token: 0x04001CBD RID: 7357
	private GameObject pFb_PanelList;

	// Token: 0x04001CBE RID: 7358
	private Text tNanDuTxt;

	// Token: 0x04001CBF RID: 7359
	private Text tFuHuoTxt;

	// Token: 0x04001CC0 RID: 7360
	private Text tHpTxt;

	// Token: 0x04001CC1 RID: 7361
	private Text tChangeHeroTxt;

	// Token: 0x04001CC2 RID: 7362
	private Text tUserMaxScore;

	// Token: 0x04001CC3 RID: 7363
	private Pve_Fb_Con fbCon;

	// Token: 0x04001CC4 RID: 7364
	private int m_nPanelType;

	// Token: 0x04001CC5 RID: 7365
	private GameObject OnSelectToggleBtn;

	// Token: 0x04001CC6 RID: 7366
	private GameObject OnSelectPanel;

	// Token: 0x04001CC7 RID: 7367
	private List<GameObject> szAllToggle = new List<GameObject>();

	// Token: 0x04001CC8 RID: 7368
	private Dictionary<int, GameObject> AllPanel = new Dictionary<int, GameObject>();

	// Token: 0x04001CC9 RID: 7369
	private GameObject pItemTip;

	// Token: 0x04001CCA RID: 7370
	private GameObject pQiangHuaBtn;

	// Token: 0x04001CCB RID: 7371
	private Slider TimeSdr;

	// Token: 0x04001CCC RID: 7372
	private GameObject pBuyTiLiBtn;

	// Token: 0x04001CCD RID: 7373
	private Text tTiLiDesc;

	// Token: 0x04001CCE RID: 7374
	private Text tTiLiValue;

	// Token: 0x04001CCF RID: 7375
	private GameObject defaultTgObj;

	// Token: 0x04001CD0 RID: 7376
	public bool IsCheckChatMoney;
}
