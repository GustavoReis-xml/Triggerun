using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B1 RID: 689
public class Pve_Fb_Con : Window
{
	// Token: 0x06001FAD RID: 8109 RVA: 0x00123086 File Offset: 0x00121286
	public void OnAddRawImage(RawImage rimg)
	{
		this.m_szAllRawImgList.Add(rimg);
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x00123098 File Offset: 0x00121298
	public void OnDelRawImage()
	{
		for (int i = 0; i < this.m_szAllRawImgList.Count; i++)
		{
			bool flag = !(this.m_szAllRawImgList[i] == null) && !(this.m_szAllRawImgList[i].texture == null);
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.m_szAllRawImgList[i].texture);
			}
		}
		this.m_szAllRawImgList.Clear();
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x0012311C File Offset: 0x0012131C
	protected override void Awake()
	{
		Pve_Fb_Con.inst = this;
		base.Awake();
		this.pScrollBar = this.UIGameObj.Get_GameObj(5);
		this.pZuoBtn = this.UIGameObj.Get_GameObj(6);
		this.pYouBtn = this.UIGameObj.Get_GameObj(7);
		this.sPbar = this.pScrollBar.GetComponent<Scrollbar>();
		this.pDiffPanel = this.UIGameObj.Get_GameObj(8);
		this.pOneDiffBtn = this.UIGameObj.Get_GameObj(9);
		this.pOneDiffBtn.SetActive(false);
		this.tZdl_1 = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.tZdl_2 = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.tZdl_3 = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.pTiLiPanel = this.UIGameObj.Get_GameObj(15);
		EventTriggerListener.Get(this.pTiLiPanel.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.pTiLiPanel.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
		this.pZdl_1 = this.UIGameObj.Get_GameObj(16);
		this.pZdl_2 = this.UIGameObj.Get_GameObj(17);
		this.pZdl_3 = this.UIGameObj.Get_GameObj(18);
		EventTriggerListener.Get(this.pZdl_1.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterZdlObj);
		EventTriggerListener.Get(this.pZdl_1.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitZdlObj);
		EventTriggerListener.Get(this.pZdl_2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterZdlObj);
		EventTriggerListener.Get(this.pZdl_2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitZdlObj);
		EventTriggerListener.Get(this.pZdl_3.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterZdlObj);
		EventTriggerListener.Get(this.pZdl_3.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitZdlObj);
		this.UpdateZdl();
		EventTriggerListener.Get(this.pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnPageClick);
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnPageClick);
		EventTriggerListener.Get(this.pYouBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.tSaoDangesc = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.ShowSaoDangNum();
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x001233EC File Offset: 0x001215EC
	public void OnPageClick(GameObject obj)
	{
		string a = obj.name.Replace("btn_", string.Empty);
		bool flag = a == "l";
		if (flag)
		{
			bool flag2 = this.m_nCurPage <= 1;
			if (flag2)
			{
				this.m_nCurPage = 1;
				return;
			}
			this.m_nCurPage--;
		}
		else
		{
			bool flag3 = this.m_nCurPage >= this.m_nMaxPage;
			if (flag3)
			{
				this.m_nCurPage = this.m_nMaxPage;
				return;
			}
			this.m_nCurPage++;
		}
		this.OnDiffList(this.pdata, this.m_nCurPage);
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x00123494 File Offset: 0x00121694
	public void UpdateZdl()
	{
		bool flag = !(this.tZdl_1 == null);
		if (flag)
		{
			this.tZdl_1.text = (PveManager.GetInstance().GetZhanDuLi(1) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
			this.tZdl_2.text = (PveManager.GetInstance().GetZhanDuLi(2) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
			this.tZdl_3.text = (PveManager.GetInstance().GetZhanDuLi(3) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x00123550 File Offset: 0x00121750
	private void OnEnterZdlObj(GameObject obj)
	{
		int nHeroType = int.Parse(obj.name.Replace("Image_", string.Empty));
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnShowItemTip(3, null, nHeroType);
		}
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x0012359C File Offset: 0x0012179C
	private void OnExitZdlObj(GameObject obj)
	{
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x001235C8 File Offset: 0x001217C8
	private void OnEnterObj(GameObject obj)
	{
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnShowItemTip(1, null, 0);
		}
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x001235F8 File Offset: 0x001217F8
	private void OnExitObj(GameObject obj)
	{
		bool flag = PveUICon.inst != null;
		if (flag)
		{
			PveUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x00123624 File Offset: 0x00121824
	private void Start()
	{
		this.pdata = fGameManager.mloadConfig.AllPveFbData[this.m_nPveModelId];
		int count = this.pdata.szScnid.Count;
		bool flag = count % this.m_nPageMaxCount == 0;
		if (flag)
		{
			this.m_nMaxPage = count / this.m_nPageMaxCount;
		}
		else
		{
			this.m_nMaxPage = count / this.m_nPageMaxCount + 1;
		}
		this.OnDiffList(this.pdata, this.m_nCurPage);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x001236A6 File Offset: 0x001218A6
	public void InitData(int nPveId, int nFbNum)
	{
		this.m_nPveModelId = nPveId;
		this.m_pPveData = fGameManager.mloadConfig.AllPveFbData[nPveId];
		this.m_nFbNum = nFbNum;
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x001236D0 File Offset: 0x001218D0
	public void OnDiffList(PveXmlData pdata, int nCurPage)
	{
		string[] szColor = pdata.szColor;
		ArrayList szScnid = pdata.szScnid;
		int count = szScnid.Count;
		this.OnDelRawImage();
		fUIManager.OnClearObj(this.m_szObjList);
		int num = (nCurPage != this.m_nMaxPage) ? (this.m_nPageMaxCount * nCurPage) : szScnid.Count;
		for (int i = (nCurPage - 1) * this.m_nPageMaxCount; i < num; i++)
		{
			int num2 = (int)szScnid[i];
			FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[num2];
			FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneDiffBtn);
			gameObject.transform.SetParent(this.pDiffPanel.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.name = num2.ToString() + "_" + fbModelXmlData.m_nNanDu.ToString();
			gameObject.transform.FindChild("bgdownimg").GetComponent<Image>().color = Fun.StrToColor(szColor[i], 1f);
			FbNanDuCon fbNanDuCon = gameObject.AddComponent<FbNanDuCon>();
			fbNanDuCon.OnInitData(num2, fbModelXmlData.m_nNanDu, fbXmlData.nType, this.m_nFbNum);
			this.m_szObjList.Add(gameObject);
			EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangDiffFbClick);
		}
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00123888 File Offset: 0x00121A88
	private void OnChangDiffFbClick(GameObject obj)
	{
		string[] array = obj.name.Split(new char[]
		{
			'_'
		});
		this.m_nFbId = int.Parse(array[0]);
		this.m_nFbLv = int.Parse(array[1]);
		FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[this.m_nFbId];
		FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
		int num = (int)PveManager.GetInstance().GetPveDataByFbType(fbXmlData.nType)[0];
		bool flag = ActivityManager.GetInstance().IsOpenLimit(fbXmlData.nType) == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = fGameManager.m_nUserType == 1 || Time.time - PveManager.GetInstance().nCdTime < 1f || !PveManager.GetInstance().IsPveOpen();
			if (!flag2)
			{
				bool flag3 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(26030, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag4 = TeamManager.GetInstance().Dyaoqing.Count > 0;
					if (flag4)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
					}
					else
					{
						bool flag5 = MatchManager.GetInstance().m_nIsMatch > 0;
						if (flag5)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(26029, new object[0]), "ffdd00", "system");
						}
						else
						{
							bool flag6 = num < this.m_nFbLv;
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26031, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							else
							{
								int num2 = 0;
								bool flag7 = TeamManager.GetInstance().m_nTeamID == 0;
								if (flag7)
								{
									int num3 = (int)PveManager.GetInstance().GetPveDataByFbType(9)[6];
									bool flag8 = num3 > fbModelXmlData.m_nZdl * 2;
									if (flag8)
									{
										num2 = 1;
									}
								}
								bool flag9 = num2 == 0 && PlayerDataManager.getInstance().HUserData.m_nEnergy < PveManager.GetInstance().GetTili(this.m_nFbLv);
								if (flag9)
								{
									PveUICon.inst.OnBuyTiLi(2);
								}
								else
								{
									bool flag10 = (int)PveManager.GetInstance().GetPveDataByFbType(9)[6] < fbModelXmlData.m_nMinZdl;
									if (flag10)
									{
										bool flag11 = TeamManager.GetInstance().m_nTeamID != 0;
										if (flag11)
										{
											TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
										}
										else
										{
											TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26028, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
										}
									}
									else
									{
										bool flag12 = TeamManager.GetInstance().m_nTeamID == 0 && (int)PveManager.GetInstance().GetPveDataByFbType(9)[6] < fbModelXmlData.m_nZdl;
										if (flag12)
										{
											PveUICon.inst.OnCheckZdl(fbXmlData, 26061);
										}
										else
										{
											PveManager.GetInstance().nCdTime = Time.time;
											bool flag13 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
											if (flag13)
											{
												TeamManager.GetInstance().LeaveLookingTeam(6, fbXmlData.nType, this.m_nFbLv, this.m_nFbId);
											}
											else
											{
												PveManager.GetInstance().OnPveOk(fbXmlData.nType, this.m_nFbLv, this.m_nFbId, 1);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x00123D04 File Offset: 0x00121F04
	private int nGetFbIdByLv(PveXmlData fbdata, int nLv)
	{
		bool flag = fbdata.szScnid.Count <= nLv;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = (int)fbdata.szScnid[nLv];
		}
		return result;
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x00123D44 File Offset: 0x00121F44
	public void UpdateShow()
	{
		foreach (GameObject gameObject in this.m_szObjList)
		{
			FbNanDuCon component = gameObject.GetComponent<FbNanDuCon>();
			component.UpdateShow();
		}
		this.UpdateZdl();
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x00123DAC File Offset: 0x00121FAC
	public void OnUpdateSaoDangData()
	{
		foreach (GameObject gameObject in this.m_szObjList)
		{
			FbNanDuCon component = gameObject.GetComponent<FbNanDuCon>();
			component.OnUpdateSaoDangData();
		}
		this.ShowSaoDangNum();
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x00123E14 File Offset: 0x00122014
	private void ShowSaoDangNum()
	{
		int num = 2 - PveManager.GetInstance().GetSaoDangNum(this.m_pPveData.m_nFbType);
		bool flag = num < 0;
		if (flag)
		{
			num = 0;
		}
		this.tSaoDangesc.text = Language.GetLang(27044, new object[]
		{
			num
		});
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x00123E70 File Offset: 0x00122070
	public void ShowCaiLiaoNum(int nItemId)
	{
		foreach (GameObject gameObject in this.m_szObjList)
		{
			FbNanDuCon component = gameObject.GetComponent<FbNanDuCon>();
			component.ShowCaiLiaoNum(nItemId);
		}
	}

	// Token: 0x04001CDA RID: 7386
	public static Pve_Fb_Con inst;

	// Token: 0x04001CDB RID: 7387
	private GameObject pFbListPanel;

	// Token: 0x04001CDC RID: 7388
	private GameObject pOneFb;

	// Token: 0x04001CDD RID: 7389
	private GameObject pEnterBtn;

	// Token: 0x04001CDE RID: 7390
	private PveXmlData m_pPveData;

	// Token: 0x04001CDF RID: 7391
	private int m_nFbId;

	// Token: 0x04001CE0 RID: 7392
	private int m_nFbLv;

	// Token: 0x04001CE1 RID: 7393
	private int m_nPveModelId;

	// Token: 0x04001CE2 RID: 7394
	private int m_nFbNum;

	// Token: 0x04001CE3 RID: 7395
	private GameObject pDiffPanel;

	// Token: 0x04001CE4 RID: 7396
	private GameObject pOneDiffBtn;

	// Token: 0x04001CE5 RID: 7397
	private List<GameObject> m_szObjList = new List<GameObject>();

	// Token: 0x04001CE6 RID: 7398
	private Text tZdl_1;

	// Token: 0x04001CE7 RID: 7399
	private Text tZdl_2;

	// Token: 0x04001CE8 RID: 7400
	private Text tZdl_3;

	// Token: 0x04001CE9 RID: 7401
	private GameObject pTiLiPanel;

	// Token: 0x04001CEA RID: 7402
	private GameObject pZdl_1;

	// Token: 0x04001CEB RID: 7403
	private GameObject pZdl_2;

	// Token: 0x04001CEC RID: 7404
	private GameObject pZdl_3;

	// Token: 0x04001CED RID: 7405
	private GameObject pZuoBtn;

	// Token: 0x04001CEE RID: 7406
	private GameObject pYouBtn;

	// Token: 0x04001CEF RID: 7407
	private GameObject pScrollBar;

	// Token: 0x04001CF0 RID: 7408
	private Scrollbar sPbar;

	// Token: 0x04001CF1 RID: 7409
	private Text tTiLiDesc;

	// Token: 0x04001CF2 RID: 7410
	private Text tSaoDangesc;

	// Token: 0x04001CF3 RID: 7411
	private int m_nMaxPage;

	// Token: 0x04001CF4 RID: 7412
	private int m_nCurPage = 1;

	// Token: 0x04001CF5 RID: 7413
	private int m_nPageMaxCount = 3;

	// Token: 0x04001CF6 RID: 7414
	public List<RawImage> m_szAllRawImgList = new List<RawImage>();

	// Token: 0x04001CF7 RID: 7415
	private PveXmlData pdata;
}
