using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class GameResultUICon : Window
{
	// Token: 0x06000CF4 RID: 3316 RVA: 0x0007AFC8 File Offset: 0x000791C8
	protected override void Awake()
	{
		base.Awake();
		this.mydata = this.UIGameObj.Get_GameObj(0);
		this.nametext = this.mydata.transform.Find("nametext").GetComponent<Text>();
		this.mylevel = this.mydata.transform.Find("level").GetComponent<Text>();
		this.expfilled = this.mydata.transform.Find("exp/expfilled").GetComponent<Image>();
		this.myHead = this.mydata.transform.Find("head").GetComponent<RawImage>();
		this.addico = this.mydata.transform.Find("addico").gameObject;
		this.addico.GetComponent<Image>().enabled = true;
		this.addico.SetActive(false);
		this.addexptext = this.mydata.transform.Find("addexptext").GetComponent<Text>();
		this.addgoldtext = this.mydata.transform.Find("addgoldtext").GetComponent<Text>();
		this.expvalue = this.mydata.transform.Find("expvalue").GetComponent<Text>();
		this.addGoldTxt = this.mydata.transform.Find("Addgold").GetComponent<Text>();
		this.ui_shenjitiao = this.mydata.transform.Find("ui_shenjitiao").gameObject;
		this.ui_shenjitiao.SetActive(false);
		this.returngame = this.UIGameObj.Get_GameObj(1);
		EventTriggerListener.Get(this.returngame).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseUi);
		this.xycbssj = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.mygamedata = this.UIGameObj.Get_GameObj(4);
		this.clonegameuserdata = this.UIGameObj.Get_GameObj(5);
		this.clonegameuserdata.SetActive(false);
		this.youfanglist = this.UIGameObj.Get_GameObj(6);
		this.difanglist = this.UIGameObj.Get_GameObj(7);
		this.leftbut = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(this.leftbut).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeSubPage);
		this.rightbut = this.UIGameObj.Get_GameObj(9);
		EventTriggerListener.Get(this.rightbut).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeAddPage);
		this.herolist = this.UIGameObj.Get_GameObj(10);
		this.InitHeroList();
		this.Winbg = this.UIGameObj.Get_GameObj(11).transform.GetComponent<RawImage>();
		this.ui_Clonejin = this.UIGameObj.Get_GameObj(12);
		this.ui_Clonejin.SetActive(false);
		this.ui_Cloneyin = this.UIGameObj.Get_GameObj(13);
		this.ui_Cloneyin.SetActive(false);
		this.ui_Clonetong = this.UIGameObj.Get_GameObj(14);
		this.ui_Clonetong.SetActive(false);
		this.ui_Clonexiaojin = this.UIGameObj.Get_GameObj(15);
		this.ui_Clonexiaojin.SetActive(false);
		this.ui_Clonexiaoyin = this.UIGameObj.Get_GameObj(16);
		this.ui_Clonexiaoyin.SetActive(false);
		this.ui_Clonexiaotong = this.UIGameObj.Get_GameObj(17);
		this.ui_Clonexiaotong.SetActive(false);
		fUIManager.GetInstance().GetIconAsset("GameUI/gameresultui.txt", this.Winbg);
		this.commonOverUI = this.UIGameObj.Get_GameObj(18);
		this.ladderOverAUI = this.UIGameObj.Get_GameObj(19);
		this.curLvlIcon = this.UIGameObj.Get_GameObj(21).GetComponent<Image>();
		this.curLvlTxt = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.curScoreTxt = this.UIGameObj.Get_GameObj(22).GetComponent<Text>();
		this.nowMaxIcon = this.UIGameObj.Get_GameObj(23).GetComponent<Image>();
		this.nowMaxTxt = this.UIGameObj.Get_GameObj(33).GetComponent<Text>();
		this.nowMaxScore = this.UIGameObj.Get_GameObj(24).GetComponent<Text>();
		this.curMaxIcon = this.UIGameObj.Get_GameObj(25).GetComponent<Image>();
		this.curMaxScore = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.nextLvlTxt = this.UIGameObj.Get_GameObj(28).GetComponent<Text>();
		this.ladderOverAUI.SetActive(false);
		this.ladderOverBUI = this.UIGameObj.Get_GameObj(20);
		this.resultTxt = this.UIGameObj.Get_GameObj(29).GetComponent<Text>();
		this.resultIcon = this.UIGameObj.Get_GameObj(30).GetComponent<Image>();
		this.resultIcon.gameObject.SetActive(false);
		this.nowScoreTxt = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.ladderOverBUI.SetActive(false);
		this.pPve_Center = this.UIGameObj.Get_GameObj(34);
		this.p_Center = this.UIGameObj.Get_GameObj(35);
		this.pPerson_Center = this.UIGameObj.Get_GameObj(36);
		GameResultUICon.Inst = this;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0007B524 File Offset: 0x00079724
	public void Start()
	{
		bool flag = SkyLadderManager.GetInstance().isSkyLadderMatch && !base.IsInvoking("ShowSkyLadderOver");
		if (flag)
		{
			base.Invoke("ShowSkyLadderOver", 10f);
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0007B568 File Offset: 0x00079768
	public override void Reset()
	{
		base.Reset();
		bool flag = SkyLadderManager.GetInstance().isSkyLadderMatch && !base.IsInvoking("ShowSkyLadderOver");
		if (flag)
		{
			base.Invoke("ShowSkyLadderOver", 10f);
		}
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0007B5B4 File Offset: 0x000797B4
	public GameObject GetRankEffect(int nRank, int nType)
	{
		GameObject result = null;
		switch (nRank)
		{
		case 0:
		{
			result = this.ui_Clonejin;
			bool flag = nType == 1;
			if (flag)
			{
				result = this.ui_Clonexiaojin;
			}
			break;
		}
		case 1:
		{
			result = this.ui_Cloneyin;
			bool flag2 = nType == 1;
			if (flag2)
			{
				result = this.ui_Clonexiaoyin;
			}
			break;
		}
		case 2:
		{
			result = this.ui_Clonetong;
			bool flag3 = nType == 1;
			if (flag3)
			{
				result = this.ui_Clonexiaotong;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0007B635 File Offset: 0x00079835
	private void Update()
	{
		this.NewAddExpEffect();
		this.NewAddGoldEffect();
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0007B648 File Offset: 0x00079848
	public void OnShowWin()
	{
		ArrayList szFbResultData = FBManager.GetInstance().m_szFbResultData;
		bool flag = szFbResultData.Count < 1;
		if (flag)
		{
			this.OnCloseUi(null);
			Debug.Log("打开结算界面时副本结算数据已经清空结算已经结束--名称:" + roleuiManager.instance.GetMeName() + "--时间:" + Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"));
		}
		else
		{
			this.m_szResultUserList = (ArrayList)szFbResultData[0];
			this.m_szResultHeroList = (ArrayList)szFbResultData[1];
			this.heroMe = fGameManager.GetInstance().getPlayer();
			bool flag2 = this.heroMe == null;
			if (flag2)
			{
				Debug.LogError("打开结算界面时没有找到玩家自己对象");
			}
			else
			{
				this.OnShowMyData();
				bool flag3 = MatchManager.GetInstance().m_pFbData.nType == 11;
				if (flag3)
				{
					this.pPve_Center.SetActive(false);
					this.p_Center.SetActive(false);
					this.OnPersonFightResultData();
					this.pPerson_Center.SetActive(true);
				}
				else
				{
					bool flag4 = MatchManager.GetInstance().m_nMatchType == 3;
					if (flag4)
					{
						this.pPve_Center.SetActive(true);
						this.p_Center.SetActive(false);
						this.pPerson_Center.SetActive(false);
						KingGameResultCon kingGameResultCon = this.pPve_Center.GetComponent<KingGameResultCon>();
						bool flag5 = kingGameResultCon == null;
						if (flag5)
						{
							kingGameResultCon = this.pPve_Center.AddComponent<KingGameResultCon>();
						}
						kingGameResultCon.OnSetDada(this);
					}
					else
					{
						this.pPve_Center.SetActive(false);
						this.pPerson_Center.SetActive(false);
						this.p_Center.SetActive(true);
						this.OnResultData();
					}
				}
				this.OnHeroListData(0, 0);
			}
		}
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0007B800 File Offset: 0x00079A00
	private ArrayList GetResultData(string sName)
	{
		ArrayList arrayList = new ArrayList();
		int num = this.m_szResultUserList.IndexOf(Fun.EncodingUTF8(sName));
		bool flag = num == -1;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			result = (ArrayList)this.m_szResultUserList[num + 1];
		}
		return result;
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0007B84C File Offset: 0x00079A4C
	private void OnShowMyData()
	{
		string text = string.Empty;
		ArrayList arrayList = new ArrayList();
		text = this.heroMe.playerCommon.m_sName;
		int nLevel = PlayerDataManager.getInstance().HUserData.m_nLevel;
		arrayList = this.GetResultData(text);
		bool flag = arrayList.Count >= 1;
		if (flag)
		{
			bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(this.heroMe.playerCommon.m_nFace);
			if (flag2)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this.heroMe.playerCommon.m_nFace];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.myHead);
			}
			this.nametext.text = text;
			bool activeSelf = this.addico.activeSelf;
			if (activeSelf)
			{
				this.addico.SetActive(false);
			}
			this.addexptext.text = string.Empty;
			this.addgoldtext.text = string.Empty;
			this.addGoldTxt.text = string.Empty;
			this.xycbssj.text = string.Empty;
			this.nAddAllValue = (int)arrayList[16];
			this.nNowExp = (int)arrayList[21];
			int num = (int)arrayList[17];
			this.nNowLevel = nLevel - num;
			bool flag3 = this.nNowLevel < 1;
			if (flag3)
			{
				this.nNowLevel = 1;
			}
			bool flag4 = this.nNowLevel < 5 && num > 0;
			if (flag4)
			{
				this.nAddAllValue = Fun.GetUserLvlupExp(this.nNowLevel) - this.nNowExp + (this.nNowLevel + 1) * 50;
			}
			bool flag5 = this.nAddAllValue < 3;
			if (flag5)
			{
				this.nOnceAddValue = this.nAddAllValue;
			}
			else
			{
				this.nOnceAddValue = this.nAddAllValue / 3;
			}
			this.nSumValue = 0;
			this.OnShowValue(0);
			this.bCheck = true;
			LiBaoManager.GetInstance().ShowSkyEveryDayGold();
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0007BA6C File Offset: 0x00079C6C
	private void NewAddExpEffect()
	{
		bool flag = !this.bCheck;
		if (!flag)
		{
			bool flag2 = this.nAddAllValue < 1;
			if (flag2)
			{
				this.bCheck = false;
				this.nAddAllValue = 0;
				this.nOnceAddValue = 0;
				this.nSumValue = 0;
				this.nNowLevel = 0;
				this.nNowExp = 0;
			}
			else
			{
				int nIsUp = 0;
				int userLvlupExp = Fun.GetUserLvlupExp(this.nNowLevel);
				int num = Mathf.CeilToInt(Time.deltaTime * (float)this.nOnceAddValue);
				bool flag3 = num > this.nAddAllValue;
				if (flag3)
				{
					num = this.nAddAllValue;
				}
				this.nAddAllValue -= num;
				bool flag4 = this.nAddAllValue < 1;
				if (flag4)
				{
					this.nAddAllValue = 0;
				}
				this.nNowExp += num;
				bool flag5 = (float)this.nNowExp >= (float)userLvlupExp;
				if (flag5)
				{
					this.nNowLevel++;
					this.nNowExp -= userLvlupExp;
					nIsUp = 1;
				}
				this.nSumValue += num;
				this.OnShowValue(nIsUp);
			}
		}
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0007BB84 File Offset: 0x00079D84
	private void OnShowValue(int nIsUp)
	{
		this.mylevel.text = this.nNowLevel.ToString();
		int userLvlupExp = Fun.GetUserLvlupExp(this.nNowLevel);
		this.expfilled.fillAmount = (float)this.nNowExp / (float)userLvlupExp;
		this.expvalue.text = this.nNowExp.ToString() + "/" + userLvlupExp.ToString();
		this.addgoldtext.text = Language.GetLang(2026, new object[]
		{
			this.nSumValue
		});
		bool flag = nIsUp == 1;
		if (flag)
		{
			this.OnUpLevelEffect();
			this.ui_shenjitiao.SetActive(false);
			this.ui_shenjitiao.SetActive(true);
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0007BC48 File Offset: 0x00079E48
	public void ShowAddGold(int nadd)
	{
		bool flag = nadd >= 1;
		if (flag)
		{
			this.nAddAllGold = nadd;
			this.nNowGold = 0;
			this.isAddGold = true;
			this.addGoldTxt.text = Language.GetLang(2048, new object[]
			{
				this.nNowGold
			});
		}
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0007BCA4 File Offset: 0x00079EA4
	private void NewAddGoldEffect()
	{
		bool flag = this.isAddGold;
		if (flag)
		{
			this.nNowGold = Mathf.CeilToInt((float)this.nNowGold * 1f + (float)this.nAddAllGold / 3f * Time.deltaTime);
			bool flag2 = this.nNowGold >= this.nAddAllGold;
			if (flag2)
			{
				this.nNowGold = this.nAddAllGold;
				this.isAddGold = false;
			}
			this.addGoldTxt.text = Language.GetLang(2048, new object[]
			{
				this.nNowGold
			});
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0007BD44 File Offset: 0x00079F44
	private void OnPersonFightResultData()
	{
		PersonEndCon personEndCon = this.pPerson_Center.AddComponent<PersonEndCon>();
		WarPersonManager.Instance.ShowEnd();
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0007BD6C File Offset: 0x00079F6C
	private void OnResultData()
	{
		string sName = string.Empty;
		ArrayList szData = new ArrayList();
		sName = this.heroMe.playerCommon.m_sName;
		int nShili = this.heroMe.playerCommon.m_nShili;
		this.myGameResult = this.mygamedata.GetComponent<OneGameResultCon>();
		bool flag = this.myGameResult == null;
		if (flag)
		{
			this.myGameResult = this.mygamedata.AddComponent<OneGameResultCon>();
		}
		szData = this.GetResultData(sName);
		this.myGameResult.SetData(szData, 0, this);
		this.OnCreateResultData(nShili, 1, this.youfanglist);
		this.OnCreateResultData(nShili, 2, this.difanglist);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0007BE14 File Offset: 0x0007A014
	private void OnCreateResultData(int nShili, int nForceType, GameObject oParent)
	{
		ArrayList szData = new ArrayList();
		ArrayList resultList = this.GetResultList(nShili, nForceType);
		int num = 5;
		bool flag = nForceType == 1;
		if (flag)
		{
			num--;
			string sName = this.heroMe.playerCommon.m_sName;
			int num2 = resultList.IndexOf(sName);
			bool flag2 = num2 != -1;
			if (flag2)
			{
				resultList.Remove(sName);
			}
		}
		int count = resultList.Count;
		for (int i = 0; i < num; i++)
		{
			Transform transform = oParent.transform.Find(i.ToString());
			bool flag3 = transform == null;
			GameObject gameObject;
			if (flag3)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonegameuserdata);
				gameObject.transform.SetParent(oParent.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = i.ToString();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			szData = new ArrayList();
			bool flag4 = i < count;
			if (flag4)
			{
				szData = this.GetResultData(resultList[i].ToString());
			}
			OneGameResultCon oneGameResultCon = gameObject.GetComponent<OneGameResultCon>();
			bool flag5 = oneGameResultCon == null;
			if (flag5)
			{
				oneGameResultCon = gameObject.AddComponent<OneGameResultCon>();
			}
			oneGameResultCon.SetData(szData, nForceType, this);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0007BF98 File Offset: 0x0007A198
	private ArrayList GetResultList(int nShili, int nForceType)
	{
		ArrayList arrayList = new ArrayList();
		int count = this.m_szResultUserList.Count;
		for (int i = 0; i < count; i += 2)
		{
			string value = Fun.DecodingUTF8(this.m_szResultUserList[i].ToString());
			bool flag = count < 2;
			if (!flag)
			{
				ArrayList arrayList2 = (ArrayList)this.m_szResultUserList[i + 1];
				bool flag2 = arrayList2.Count >= 1;
				if (flag2)
				{
					int num = (int)arrayList2[2];
					bool flag3 = nForceType == 1 && nShili == num;
					if (flag3)
					{
						arrayList.Add(value);
					}
					bool flag4 = nForceType == 2 && nShili != num;
					if (flag4)
					{
						arrayList.Add(value);
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0007C074 File Offset: 0x0007A274
	private void OnHeroListData(int nNowPage, int nType)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList szData = new ArrayList();
		this.m_nNowPage = nNowPage;
		arrayList = this.GetHeroData(nNowPage);
		int count = arrayList.Count;
		for (int i = 0; i < this.m_nOnePage; i++)
		{
			Transform transform = this.herolist.transform.Find("hero_" + i.ToString());
			bool flag = !(transform == null);
			if (flag)
			{
				OneResultHeroCon oneResultHeroCon = transform.gameObject.GetComponent<OneResultHeroCon>();
				bool flag2 = oneResultHeroCon == null;
				if (flag2)
				{
					oneResultHeroCon = transform.gameObject.AddComponent<OneResultHeroCon>();
				}
				szData = new ArrayList();
				bool flag3 = i < count;
				if (flag3)
				{
					szData = (ArrayList)arrayList[i];
				}
				oneResultHeroCon.OnSetUi(szData, this.heroMe, nType);
			}
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0007C158 File Offset: 0x0007A358
	private ArrayList GetHeroData(int nPage)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int count = this.m_szResultHeroList.Count;
		int num = nPage * this.m_nOnePage;
		int num2 = num + this.m_nOnePage;
		bool flag = num2 >= count;
		if (flag)
		{
			num2 = count;
			this.rightbut.SetActive(false);
		}
		else
		{
			this.rightbut.SetActive(true);
		}
		bool flag2 = num > 0;
		if (flag2)
		{
			this.leftbut.SetActive(true);
		}
		else
		{
			this.leftbut.SetActive(false);
		}
		for (int i = num; i < num2; i++)
		{
			arrayList2 = (ArrayList)this.m_szResultHeroList[i];
			bool flag3 = arrayList2.Count >= 1;
			if (flag3)
			{
				arrayList.Add(arrayList2);
			}
		}
		return arrayList;
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0007C23C File Offset: 0x0007A43C
	private void OnChangeSubPage(GameObject obj)
	{
		bool flag = this.m_nNowPage >= 1;
		if (flag)
		{
			this.OnHeroListData(this.m_nNowPage - 1, 1);
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0007C26C File Offset: 0x0007A46C
	private void OnChangeAddPage(GameObject obj)
	{
		int count = this.m_szResultHeroList.Count;
		bool flag = this.m_nNowPage * this.m_nOnePage <= count;
		if (flag)
		{
			this.OnHeroListData(this.m_nNowPage + 1, 1);
		}
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0007C2B0 File Offset: 0x0007A4B0
	public void OnCloseUi(GameObject obj)
	{
		this.Close();
		fUIManager.GetInstance().GotoHall(0);
		bool flag = PlayerDataManager.getInstance().HUserData != null;
		if (flag)
		{
			PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		}
		ChatManager.mInst.OnShowOrHideChatWin(true);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0007C300 File Offset: 0x0007A500
	public override void Close()
	{
		this.m_szResultUserList.Clear();
		this.m_szResultHeroList.Clear();
		Fun.ClearChildObejct(this.youfanglist);
		Fun.ClearChildObejct(this.difanglist);
		this.InitEffectData();
		this.InitHeroList();
		this.oNowClickAddFriendObj = null;
		base.Close();
		this.commonOverUI.SetActive(true);
		this.ladderOverAUI.SetActive(false);
		this.ladderOverBUI.SetActive(false);
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0007C384 File Offset: 0x0007A584
	private void InitEffectData()
	{
		this.nNowLevel = 0;
		this.bCheck = false;
		this.nAddAllValue = 0;
		this.nOnceAddValue = 0;
		this.nSumValue = 0;
		this.nNowLevel = 0;
		this.nNowExp = 0;
		this.ui_shenjitiao.SetActive(false);
		this.isAddGold = false;
		this.nAddAllGold = 0;
		this.nNowGold = 0;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0007C3E8 File Offset: 0x0007A5E8
	private void OnUpLevelEffect()
	{
		this.addico.transform.localPosition = new Vector3(245f, -5.75f, 0f);
		bool flag = !this.addico.activeSelf;
		if (flag)
		{
			this.addico.SetActive(true);
		}
		Tweener t = this.addico.transform.DOLocalMove(new Vector3(245f, 25f, 0f), 0.5f, false);
		t.OnComplete(delegate
		{
			bool activeSelf = this.addico.activeSelf;
			if (activeSelf)
			{
				this.addico.SetActive(false);
			}
		});
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0007C47C File Offset: 0x0007A67C
	private void InitHeroList()
	{
		for (int i = 0; i < this.herolist.transform.childCount; i++)
		{
			Transform transform = this.herolist.transform.Find("hero_" + i.ToString());
			bool flag = !(transform == null);
			if (flag)
			{
				OneResultHeroCon oneResultHeroCon = transform.gameObject.GetComponent<OneResultHeroCon>();
				bool flag2 = oneResultHeroCon == null;
				if (flag2)
				{
					oneResultHeroCon = transform.gameObject.AddComponent<OneResultHeroCon>();
				}
				oneResultHeroCon.InitShow();
			}
		}
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0007C50F File Offset: 0x0007A70F
	public void SetAddFriendBut(OneGameResultCon obj)
	{
		this.oNowClickAddFriendObj = obj;
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0007C51C File Offset: 0x0007A71C
	public void GetAddFriendStat(string sName)
	{
		bool flag = !(this.oNowClickAddFriendObj == null);
		if (flag)
		{
			this.oNowClickAddFriendObj.OnUpdateSheJieBut(sName);
		}
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0007C54C File Offset: 0x0007A74C
	private void ShowSkyLadderOver()
	{
		base.CancelInvoke("ShowSkyLadderOver");
		int nSkyLadderActId = SkyLadderManager.GetInstance().m_nSkyLadderActId;
		bool flag = !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(nSkyLadderActId);
		if (!flag)
		{
			SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[nSkyLadderActId];
			bool flag2 = SkyLadderManager.GetInstance().m_nSkyMatchnum <= skyLadderData.nMinMatchNum;
			if (flag2)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int nMinMatchNum = skyLadderData.nMinMatchNum;
				int num4 = (SkyLadderManager.GetInstance().m_szSkyMatchResult.Count > nMinMatchNum) ? (SkyLadderManager.GetInstance().m_szSkyMatchResult.Count - nMinMatchNum) : 0;
				for (int i = 0; i < nMinMatchNum; i++)
				{
					bool flag3 = i < this.szResultIcon.Count;
					Image image;
					if (flag3)
					{
						image = this.szResultIcon[i];
					}
					else
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.resultIcon.gameObject);
						gameObject.transform.SetParent(this.resultIcon.transform.parent);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.SetActive(true);
						gameObject.name = string.Format("result{0}", i);
						image = gameObject.GetComponent<Image>();
						this.szResultIcon.Add(image);
					}
					bool flag4 = num4 < SkyLadderManager.GetInstance().m_szSkyMatchResult.Count;
					if (flag4)
					{
						int num5 = (int)SkyLadderManager.GetInstance().m_szSkyMatchResult[num4];
						bool flag5 = num5 == 0;
						if (flag5)
						{
							num3++;
							image.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_fu");
						}
						bool flag6 = num5 == 1;
						if (flag6)
						{
							num++;
							image.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_sheng");
						}
						bool flag7 = num5 == 2;
						if (flag7)
						{
							num2++;
							image.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_ping");
						}
					}
					else
					{
						image.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladder_kong");
					}
					num4++;
				}
				this.resultTxt.text = Language.GetLang(24017, new object[]
				{
					num,
					num2,
					num3
				});
				this.nowScoreTxt.text = string.Format("{0}<color=#00ff00>+{1}</color>", PlayerDataManager.getInstance().playerdet.matchMoney - PlayerDataManager.getInstance().playerdet.addMatchMoney, PlayerDataManager.getInstance().playerdet.addMatchMoney);
				this.commonOverUI.SetActive(false);
				this.ladderOverBUI.SetActive(true);
				this.ladderOverAUI.SetActive(false);
			}
			else
			{
				string text = "+";
				int num6 = SkyLadderManager.GetInstance().m_nSkyCurMaxlvl;
				this.nowMaxTxt.text = string.Empty;
				this.nowMaxIcon.gameObject.SetActive(true);
				this.nowMaxIcon.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", num6));
				int num7 = SkyLadderManager.GetInstance().m_nSkyCurMaxScore;
				this.nowMaxScore.text = Language.GetLang(24000, new object[]
				{
					num7
				});
				num7 = SkyLadderManager.GetInstance().m_nSkyMaxlvl;
				this.curMaxIcon.gameObject.SetActive(true);
				this.curMaxIcon.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", num7));
				num7 = SkyLadderManager.GetInstance().m_nSkyMaxScore;
				this.curMaxScore.text = Language.GetLang(24000, new object[]
				{
					num7
				});
				num6 = SkyLadderManager.GetInstance().m_nSkyLevel;
				this.curLvlTxt.text = string.Empty;
				this.curLvlIcon.enabled = true;
				this.curLvlIcon.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladder_{0}", num6));
				int nSkyScore = SkyLadderManager.GetInstance().m_nSkyScore;
				num7 = SkyLadderManager.GetInstance().m_nAddSkyScore;
				bool flag8 = num7 < 0;
				if (flag8)
				{
					text = "-";
				}
				this.curScoreTxt.text = Language.GetLang(24025, new object[]
				{
					nSkyScore - num7,
					text,
					Mathf.Abs(num7)
				});
				this.nextLvlTxt.text = string.Format("{0}<color=#00ff00>+{1}</color>", PlayerDataManager.getInstance().playerdet.matchMoney - PlayerDataManager.getInstance().playerdet.addMatchMoney, PlayerDataManager.getInstance().playerdet.addMatchMoney);
				this.commonOverUI.SetActive(false);
				this.ladderOverBUI.SetActive(false);
				this.ladderOverAUI.SetActive(true);
			}
		}
	}

	// Token: 0x04000A89 RID: 2697
	public static GameResultUICon Inst;

	// Token: 0x04000A8A RID: 2698
	private GameObject mydata;

	// Token: 0x04000A8B RID: 2699
	private GameObject ui_shenjitiao;

	// Token: 0x04000A8C RID: 2700
	private Text nametext;

	// Token: 0x04000A8D RID: 2701
	private Text mylevel;

	// Token: 0x04000A8E RID: 2702
	private Image expfilled;

	// Token: 0x04000A8F RID: 2703
	private RawImage myHead;

	// Token: 0x04000A90 RID: 2704
	private RawImage Winbg;

	// Token: 0x04000A91 RID: 2705
	private GameObject addico;

	// Token: 0x04000A92 RID: 2706
	private Text addexptext;

	// Token: 0x04000A93 RID: 2707
	private Text addgoldtext;

	// Token: 0x04000A94 RID: 2708
	private Text expvalue;

	// Token: 0x04000A95 RID: 2709
	private Text addGoldTxt;

	// Token: 0x04000A96 RID: 2710
	private GameObject returngame;

	// Token: 0x04000A97 RID: 2711
	private Text xycbssj;

	// Token: 0x04000A98 RID: 2712
	private GameObject mygamedata;

	// Token: 0x04000A99 RID: 2713
	private GameObject clonegameuserdata;

	// Token: 0x04000A9A RID: 2714
	private GameObject youfanglist;

	// Token: 0x04000A9B RID: 2715
	private GameObject difanglist;

	// Token: 0x04000A9C RID: 2716
	private GameObject leftbut;

	// Token: 0x04000A9D RID: 2717
	private GameObject rightbut;

	// Token: 0x04000A9E RID: 2718
	private GameObject herolist;

	// Token: 0x04000A9F RID: 2719
	private GameObject ui_Clonejin;

	// Token: 0x04000AA0 RID: 2720
	private GameObject ui_Cloneyin;

	// Token: 0x04000AA1 RID: 2721
	private GameObject ui_Clonetong;

	// Token: 0x04000AA2 RID: 2722
	private GameObject ui_Clonexiaojin;

	// Token: 0x04000AA3 RID: 2723
	private GameObject ui_Clonexiaoyin;

	// Token: 0x04000AA4 RID: 2724
	private GameObject ui_Clonexiaotong;

	// Token: 0x04000AA5 RID: 2725
	private GameObject commonOverUI;

	// Token: 0x04000AA6 RID: 2726
	private GameObject ladderOverAUI;

	// Token: 0x04000AA7 RID: 2727
	private GameObject ladderOverBUI;

	// Token: 0x04000AA8 RID: 2728
	private Image nowMaxIcon;

	// Token: 0x04000AA9 RID: 2729
	private Text nowMaxTxt;

	// Token: 0x04000AAA RID: 2730
	private Text nowMaxScore;

	// Token: 0x04000AAB RID: 2731
	private Image curMaxIcon;

	// Token: 0x04000AAC RID: 2732
	private Text curMaxScore;

	// Token: 0x04000AAD RID: 2733
	private Image curLvlIcon;

	// Token: 0x04000AAE RID: 2734
	private Text curLvlTxt;

	// Token: 0x04000AAF RID: 2735
	private Text curScoreTxt;

	// Token: 0x04000AB0 RID: 2736
	private Text nextLvlTxt;

	// Token: 0x04000AB1 RID: 2737
	private Text resultTxt;

	// Token: 0x04000AB2 RID: 2738
	private Image resultIcon;

	// Token: 0x04000AB3 RID: 2739
	private List<Image> szResultIcon = new List<Image>();

	// Token: 0x04000AB4 RID: 2740
	private Text nowScoreTxt;

	// Token: 0x04000AB5 RID: 2741
	private HeroPlayer heroMe;

	// Token: 0x04000AB6 RID: 2742
	private OneGameResultCon myGameResult;

	// Token: 0x04000AB7 RID: 2743
	private ArrayList m_szResultUserList = new ArrayList();

	// Token: 0x04000AB8 RID: 2744
	private ArrayList m_szResultHeroList = new ArrayList();

	// Token: 0x04000AB9 RID: 2745
	private int m_nOnePage = 3;

	// Token: 0x04000ABA RID: 2746
	private OneGameResultCon oNowClickAddFriendObj;

	// Token: 0x04000ABB RID: 2747
	private GameObject pPve_Center;

	// Token: 0x04000ABC RID: 2748
	private GameObject p_Center;

	// Token: 0x04000ABD RID: 2749
	private GameObject pPerson_Center;

	// Token: 0x04000ABE RID: 2750
	private int nAddAllValue;

	// Token: 0x04000ABF RID: 2751
	private int nOnceAddValue;

	// Token: 0x04000AC0 RID: 2752
	private int nNowLevel;

	// Token: 0x04000AC1 RID: 2753
	private int nNowExp;

	// Token: 0x04000AC2 RID: 2754
	private bool bCheck;

	// Token: 0x04000AC3 RID: 2755
	private int nSumValue;

	// Token: 0x04000AC4 RID: 2756
	private bool isAddGold;

	// Token: 0x04000AC5 RID: 2757
	private int nAddAllGold;

	// Token: 0x04000AC6 RID: 2758
	private int nNowGold;

	// Token: 0x04000AC7 RID: 2759
	private int m_nNowPage;
}
