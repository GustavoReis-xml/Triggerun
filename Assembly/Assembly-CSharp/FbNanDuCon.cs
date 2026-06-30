using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000124 RID: 292
public class FbNanDuCon : Window
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x000614AC File Offset: 0x0005F6AC
	protected override void Awake()
	{
		base.Awake();
		this.pNanDuIco = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.pTongGuan = this.UIGameObj.Get_GameObj(4);
		this.pWeiTongGuan = this.UIGameObj.Get_GameObj(5);
		this.TNanDuText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.tMaxScore = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.tTjZdl = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.pXingXingList = this.UIGameObj.Get_GameObj(8);
		this.pXingXing = this.UIGameObj.Get_GameObj(9);
		this.pItemList = this.UIGameObj.Get_GameObj(6);
		this.pItem = this.UIGameObj.Get_GameObj(7);
		this.tXiaoHaoTiLi = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		this.BuffList = this.UIGameObj.Get_GameObj(11);
		this.objBuff = this.UIGameObj.Get_GameObj(12);
		this.objBuff.SetActive(false);
		this.pItem.SetActive(false);
		this.pXingXing.SetActive(false);
		this.pSaoDangBtn = this.UIGameObj.Get_GameObj(13);
		this.pSaoDangDesc = this.UIGameObj.Get_GameObj(14);
		this.tSaoDangXiaoHao = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.pBuyBtn = this.UIGameObj.Get_GameObj(16);
		this.pXiaoHaoPanel = this.UIGameObj.Get_GameObj(17);
		this.rCaiLiaoIco = this.UIGameObj.Get_GameObj(18).GetComponent<RawImage>();
		this.tzdZdl = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		EventTriggerListener.Get(this.pSaoDangBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSaoDang);
		EventTriggerListener.Get(this.pSaoDangBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pBuyBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyItem);
		EventTriggerListener.Get(this.pBuyBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.firstRewardTxt = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00061713 File Offset: 0x0005F913
	public void OnInitData(int nScnId, int nNanDu, int nType, int nFbNum)
	{
		this.m_nScnId = nScnId;
		this.m_nNanDu = nNanDu;
		this.m_nFbType = nType;
		this.m_nFbNum = nFbNum;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00061734 File Offset: 0x0005F934
	private void Start()
	{
		string format = string.Empty;
		format = ((this.m_nFbType != 9) ? "pveIco/heian/nd{0}.txt" : "pveIco/king/nd{0}.txt");
		fUIManager.GetInstance().GetIconAsset(string.Format(format, 0), this.pNanDuIco);
		FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[this.m_nScnId];
		FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
		this.RewardData = fGameManager.mloadConfig.AllPveRewardData[this.m_nScnId];
		this.InitShowXingXing();
		this.OnShowDropItemList(fbModelXmlData.m_szShowItem);
		this.OnShowFbInfo(fbXmlData);
		this.ShowBuffList(fbXmlData.m_szPveLevel);
		this.OnShowXiaoHaoItemPic();
		PveUICon.inst.GetFb_Con(this.m_nFbNum).OnAddRawImage(this.pNanDuIco);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00061810 File Offset: 0x0005FA10
	public void OnShowXiaoHaoItemPic()
	{
		int nSdItem = this.RewardData.m_nSdItem;
		ItemData itemData = fGameManager.mloadConfig.AllItemData[nSdItem];
		string icoPath = ShopManager.GetIcoPath(itemData.m_nType);
		fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, itemData.m_sIco), this.rCaiLiaoIco);
		this.pBuyBtn.name = itemData.m_nShopId.ToString();
		this.rCaiLiaoIco.name = itemData.m_nItemId.ToString();
		EventTriggerListener.Get(this.rCaiLiaoIco.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.rCaiLiaoIco.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
		PveUICon.inst.GetFb_Con(this.m_nFbNum).OnAddRawImage(this.rCaiLiaoIco);
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000618F0 File Offset: 0x0005FAF0
	public void UpdateShow()
	{
		FbXmlData fbdata = fGameManager.mloadConfig.FbData[this.m_nScnId];
		this.OnShowFbInfo(fbdata);
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0006191C File Offset: 0x0005FB1C
	public void OnShowFbInfo(FbXmlData fbdata)
	{
		bool flag = !fGameManager.mloadConfig.FbModeData.ContainsKey(fbdata.nstartplotid);
		if (!flag)
		{
			FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbdata.nstartplotid];
			PveRewardData pveRewardData = fGameManager.mloadConfig.AllPveRewardData[fbdata.nId];
			int nNanDu = fbModelXmlData.m_nNanDu;
			int num = (int)PveManager.GetInstance().GetPveDataByFbType(this.m_nFbType)[0];
			this.TNanDuText.text = pveRewardData.sName;
			this.tTjZdl.text = Language.GetLang(26019, new object[]
			{
				fbModelXmlData.m_nZdl
			});
			this.tzdZdl.text = Language.GetLang(26065, new object[]
			{
				fbModelXmlData.m_nMinZdl
			});
			this.tXiaoHaoTiLi.text = Language.GetLang(26020, new object[]
			{
				PveManager.GetInstance().GetTili(nNanDu)
			});
			this.tMaxScore.GetComponent<Text>().text = string.Empty;
			int num2 = (this.m_nFbType != 9) ? (nNanDu * 60 + 180) : (nNanDu * 60 + 60);
			this.firstRewardTxt.text = Language.GetLang(26022, new object[]
			{
				num2
			});
			bool flag2 = num < nNanDu;
			if (flag2)
			{
				this.pTongGuan.SetActive(false);
				this.pWeiTongGuan.SetActive(true);
				int nPreFbId = pveRewardData.m_nPreFbId;
				bool flag3 = fGameManager.mloadConfig.AllPveRewardData.ContainsKey(nPreFbId);
				if (flag3)
				{
					PveRewardData pveRewardData2 = fGameManager.mloadConfig.AllPveRewardData[nPreFbId];
					this.pWeiTongGuan.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(26021, new object[]
					{
						pveRewardData2.sName
					});
				}
				else
				{
					bool flag4 = this.m_nFbType == 12;
					if (flag4)
					{
						this.pWeiTongGuan.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(26021, new object[]
						{
							Language.GetLang(26060, new object[0])
						});
					}
					else
					{
						this.pWeiTongGuan.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(26021, new object[]
						{
							string.Empty
						});
					}
				}
			}
			else
			{
				int maxScore = this.GetMaxScore(nNanDu);
				bool flag5 = num == nNanDu && maxScore < 1;
				if (flag5)
				{
					this.pTongGuan.SetActive(false);
					this.pWeiTongGuan.SetActive(false);
				}
				else
				{
					this.pTongGuan.SetActive(true);
					this.pWeiTongGuan.SetActive(false);
					bool flag6 = maxScore >= 3;
					if (flag6)
					{
						this.pSaoDangBtn.SetActive(true);
						this.pSaoDangDesc.SetActive(false);
						this.OnUpdateSaoDangData();
					}
					else
					{
						this.pSaoDangBtn.SetActive(false);
						this.pSaoDangDesc.SetActive(true);
					}
					this.SerXingXingLv(maxScore);
				}
			}
		}
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00061C6C File Offset: 0x0005FE6C
	public void OnSaoDang(GameObject obj)
	{
		bool flag = ActivityManager.GetInstance().IsOpenLimit(this.m_nFbType) == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = Time.time - PveManager.GetInstance().nCdTime < 1f || !PveManager.GetInstance().IsPveOpen();
			if (!flag2)
			{
				bool flag3 = MatchManager.GetInstance().m_nIsMatch > 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27045, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = PlayerDataManager.getInstance().HUserData.m_nEnergy < PveManager.GetInstance().GetTili(this.m_nNanDu);
					if (flag4)
					{
						PveUICon.inst.OnBuyTiLi(2);
					}
					else
					{
						int saoDangNum = PveManager.GetInstance().GetSaoDangNum(this.m_nFbType);
						bool flag5 = saoDangNum >= 2;
						if (flag5)
						{
							int itemNumById = ShopManager.GetInstance().GetItemNumById(this.RewardData.m_nSdItem);
							bool flag6 = itemNumById < this.RewardData.m_nItemNum;
							if (flag6)
							{
								this.OnBuyItem(this.pBuyBtn);
								return;
							}
						}
						PveManager.GetInstance().nCdTime = Time.time;
						PveManager.GetInstance().OnSendMsgToServer(19514, new ArrayList
						{
							this.m_nScnId,
							0
						});
					}
				}
			}
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x00061E4C File Offset: 0x0006004C
	public void OnUpdateSaoDangData()
	{
		int saoDangNum = PveManager.GetInstance().GetSaoDangNum(this.m_nFbType);
		bool flag = saoDangNum < 2;
		if (flag)
		{
			this.pXiaoHaoPanel.SetActive(false);
		}
		else
		{
			this.pXiaoHaoPanel.SetActive(true);
			this.ShowCaiLiaoNum(this.RewardData.m_nSdItem);
		}
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x00061EA4 File Offset: 0x000600A4
	public void ShowCaiLiaoNum(int nItemId)
	{
		bool flag = nItemId == this.RewardData.m_nSdItem;
		if (flag)
		{
			int nSdItem = this.RewardData.m_nSdItem;
			int itemNumById = ShopManager.GetInstance().GetItemNumById(nSdItem);
			this.tSaoDangXiaoHao.text = Language.GetLang(27042, new object[]
			{
				(itemNumById < this.RewardData.m_nItemNum) ? "FF0000" : "00FF00",
				itemNumById,
				this.RewardData.m_nItemNum
			});
		}
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00061F34 File Offset: 0x00060134
	public void OnBuyItem(GameObject obj)
	{
		int key = int.Parse(obj.name);
		bool flag = fGameManager.mloadConfig.AllShopData.ContainsKey(key);
		if (flag)
		{
			ShopData pShopData = fGameManager.mloadConfig.AllShopData[key];
			ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00061F84 File Offset: 0x00060184
	private void ShowBuffList(ArrayList szBuffList)
	{
		string format = string.Empty;
		for (int i = 0; i < szBuffList.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objBuff);
			ArrayList arrayList = (ArrayList)szBuffList[i];
			gameObject.transform.SetParent(this.BuffList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			int num = (int)arrayList[0];
			gameObject.name = num.ToString();
			format = "pveIco/bufIco/b{0}.txt";
			RawImage component = gameObject.GetComponent<RawImage>();
			fUIManager.GetInstance().GetIconAsset(string.Format(format, num), component);
			EventTriggerListener.Get(gameObject.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBuffObj);
			EventTriggerListener.Get(gameObject.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBuffObj);
			PveUICon.inst.GetFb_Con(this.m_nFbNum).OnAddRawImage(component);
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x000620B4 File Offset: 0x000602B4
	private void OnEnterBuffObj(GameObject obj)
	{
		int nHeroType = int.Parse(obj.name);
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnShowItemTip(4, null, nHeroType);
		}
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000620F0 File Offset: 0x000602F0
	private void OnExitBuffObj(GameObject obj)
	{
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0006211C File Offset: 0x0006031C
	public void InitShowXingXing()
	{
		for (int i = 0; i < this.m_nMaxXing; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pXingXing);
			gameObject.transform.SetParent(this.pXingXingList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_szXingList.Add(gameObject);
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x000621AC File Offset: 0x000603AC
	public void SerXingXingLv(int nLv)
	{
		int num = 0;
		bool flag = nLv > this.m_nMaxXing;
		if (!flag)
		{
			foreach (GameObject gameObject in this.m_szXingList)
			{
				bool flag2 = num < nLv;
				if (flag2)
				{
					gameObject.transform.FindChild("Image").gameObject.SetActive(true);
				}
				else
				{
					gameObject.transform.FindChild("Image").gameObject.SetActive(false);
				}
				num++;
			}
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0006225C File Offset: 0x0006045C
	public void OnShowDropItemList(ArrayList szItem)
	{
		int count = szItem.Count;
		fUIManager.OnClearObj(this.AllDropItem);
		for (int i = 0; i < count; i++)
		{
			int key = (int)szItem[i];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pItem);
			gameObject.transform.SetParent(this.pItemList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.name = key.ToString();
			gameObject.transform.FindChild("Text").GetComponent<Text>().text = string.Empty;
			RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
			string format = "pveitemico/{0}.txt";
			fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sPveico), component);
			this.AllDropItem.Add(gameObject);
			EventTriggerListener.Get(gameObject.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
			EventTriggerListener.Get(gameObject.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
			PveUICon.inst.GetFb_Con(this.m_nFbNum).OnAddRawImage(component);
		}
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x000623E0 File Offset: 0x000605E0
	private void OnEnterObj(GameObject obj)
	{
		int key = int.Parse(obj.name);
		ItemData pitemdata = fGameManager.mloadConfig.AllItemData[key];
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnShowItemTip(2, pitemdata, 0);
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0006242C File Offset: 0x0006062C
	private void OnExitObj(GameObject obj)
	{
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00062458 File Offset: 0x00060658
	private int GetMaxScore(int nNanDu)
	{
		ArrayList pveDataByFbType = PveManager.GetInstance().GetPveDataByFbType(this.m_nFbType);
		ArrayList arrayList = (ArrayList)pveDataByFbType[1];
		bool flag = nNanDu >= arrayList.Count;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = (int)arrayList[nNanDu];
		}
		return result;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x000624AC File Offset: 0x000606AC
	private int GetIsSaoDang(int nNanDu)
	{
		ArrayList pveDataByFbType = PveManager.GetInstance().GetPveDataByFbType(this.m_nFbType);
		ArrayList arrayList = (ArrayList)pveDataByFbType[13];
		bool flag = nNanDu >= arrayList.Count;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			DateTime dateTime = DateTime.Parse(pveDataByFbType[11].ToString());
			bool flag2 = Fun.GetServerDate().Date != dateTime.Date;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				result = (int)arrayList[nNanDu];
			}
		}
		return result;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0006253A File Offset: 0x0006073A
	private void Update()
	{
	}

	// Token: 0x040008C1 RID: 2241
	private int m_nNanDu;

	// Token: 0x040008C2 RID: 2242
	private int m_nScnId;

	// Token: 0x040008C3 RID: 2243
	private int m_nFbType;

	// Token: 0x040008C4 RID: 2244
	private int m_nMaxXing = 3;

	// Token: 0x040008C5 RID: 2245
	private int m_nFbNum;

	// Token: 0x040008C6 RID: 2246
	private RawImage pNanDuIco;

	// Token: 0x040008C7 RID: 2247
	private GameObject pXingXingList;

	// Token: 0x040008C8 RID: 2248
	private GameObject pXingXing;

	// Token: 0x040008C9 RID: 2249
	private GameObject pTongGuan;

	// Token: 0x040008CA RID: 2250
	private GameObject pWeiTongGuan;

	// Token: 0x040008CB RID: 2251
	private Text TNanDuText;

	// Token: 0x040008CC RID: 2252
	private Text tMaxScore;

	// Token: 0x040008CD RID: 2253
	private Text tzdZdl;

	// Token: 0x040008CE RID: 2254
	private Text tTjZdl;

	// Token: 0x040008CF RID: 2255
	private GameObject pItemList;

	// Token: 0x040008D0 RID: 2256
	private GameObject pItem;

	// Token: 0x040008D1 RID: 2257
	private Text tXiaoHaoTiLi;

	// Token: 0x040008D2 RID: 2258
	private List<GameObject> AllDropItem = new List<GameObject>();

	// Token: 0x040008D3 RID: 2259
	private List<GameObject> m_szXingList = new List<GameObject>();

	// Token: 0x040008D4 RID: 2260
	private GameObject BuffList;

	// Token: 0x040008D5 RID: 2261
	private GameObject objBuff;

	// Token: 0x040008D6 RID: 2262
	private GameObject pSaoDangBtn;

	// Token: 0x040008D7 RID: 2263
	private GameObject pSaoDangDesc;

	// Token: 0x040008D8 RID: 2264
	private Text tSaoDangXiaoHao;

	// Token: 0x040008D9 RID: 2265
	private GameObject pBuyBtn;

	// Token: 0x040008DA RID: 2266
	private GameObject pXiaoHaoPanel;

	// Token: 0x040008DB RID: 2267
	private RawImage rCaiLiaoIco;

	// Token: 0x040008DC RID: 2268
	private Text firstRewardTxt;

	// Token: 0x040008DD RID: 2269
	private PveRewardData RewardData;
}
