using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002EA RID: 746
public class SelectHeroUICon : Window
{
	// Token: 0x060022C6 RID: 8902 RVA: 0x0014035C File Offset: 0x0013E55C
	protected override void Awake()
	{
		base.Awake();
		this.uibgimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.herogroup = this.UIGameObj.Get_GameObj(1).gameObject;
		this.CloneOnehero = this.UIGameObj.Get_GameObj(2).gameObject;
		this.CloneOnehero.SetActive(false);
		this.readyfight = this.UIGameObj.Get_GameObj(3).gameObject;
		this.ready_cd = this.readyfight.transform.Find("ready_cd").GetComponent<Text>();
		this.readyText = this.readyfight.transform.Find("Text").GetComponent<Text>();
		this.cloneherotype = this.UIGameObj.Get_GameObj(4).gameObject;
		this.cloneherotype.SetActive(false);
		this.herotypelist = this.UIGameObj.Get_GameObj(5).gameObject;
		this.herolist = this.UIGameObj.Get_GameObj(6).gameObject;
		GameObject gameObject = this.UIGameObj.Get_GameObj(7).gameObject;
		bool flag = WebParam.client_language == "zh";
		if (flag)
		{
			this.zhunbei = gameObject.transform.Find("ui_taruzhandou").gameObject;
		}
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			this.zhunbei = gameObject.transform.Find("ui_taruzhandou_bx").gameObject;
		}
		this.zhunbei.SetActive(false);
		this.quedingbut = this.UIGameObj.Get_GameObj(8).gameObject;
		this.quedingbut.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OnQueDingBut(this.quedingbut);
		});
		this.sencenameText = this.UIGameObj.Get_GameObj(9).gameObject.GetComponent<Text>();
		bool flag3 = WebParam.client_language == "baxi";
		if (flag3)
		{
			this.sencenameText.font = fUIManager.mNewComic;
		}
		this.attacktype = this.UIGameObj.Get_GameObj(10).gameObject.GetComponent<Text>();
		bool flag4 = WebParam.client_language == "baxi";
		if (flag4)
		{
			this.attacktype.font = fUIManager.mNewComic;
		}
		this.attacktype.text = string.Empty;
		this.attacktyecolor = this.UIGameObj.Get_GameObj(10).gameObject.GetComponent<GradientTowColor>();
		this.zbjdtext = this.UIGameObj.Get_GameObj(11).gameObject.GetComponent<Text>();
		this.zbjdtext.text = string.Empty;
		this.lookkeygrld = this.UIGameObj.Get_GameObj(12).gameObject.GetComponent<GridLayoutGroup>();
		this.lookkeytxt = this.UIGameObj.Get_GameObj(13).gameObject.GetComponent<Text>();
		this.oLuckHeroWin = this.UIGameObj.Get_GameObj(14).gameObject;
		this.rewardTipBg = this.oLuckHeroWin.GetComponent<RawImage>();
		this.LuckHeroTopText = this.UIGameObj.Get_GameObj(15).gameObject.GetComponent<Text>();
		this.LuckHeroContentText = this.UIGameObj.Get_GameObj(16).gameObject.GetComponent<Text>();
		this.zs_OkBtn = this.UIGameObj.Get_GameObj(17).gameObject;
		this.zs_OkBtn.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OnLuckHeroEvent(2);
		});
		this.jb_OkBtn = this.UIGameObj.Get_GameObj(18).gameObject;
		this.jb_OkBtn.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OnLuckHeroEvent(5);
		});
		this.CancelBtn = this.UIGameObj.Get_GameObj(19).gameObject;
		this.CancelBtn.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OnLuckHeroEvent(-1);
		});
		this.MoneyTip = this.UIGameObj.Get_GameObj(20).gameObject.GetComponent<Text>();
		this.oLuckHeroWin.SetActive(false);
		this.lockmsg = this.UIGameObj.Get_GameObj(21).gameObject.GetComponent<Text>();
		this.lockmsg.text = string.Empty;
		this.jbmsgtext = this.UIGameObj.Get_GameObj(22).gameObject.GetComponent<Text>();
		this.jbmsgtext.text = string.Empty;
		this.oGrsdBg = this.UIGameObj.Get_GameObj(23).gameObject;
		this.oGrsdBg.SetActive(false);
		this.InitMeHeroList();
		this.InitSelectHeroList();
		SelectHeroUICon.inst = this;
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x00140812 File Offset: 0x0013EA12
	private void Update()
	{
		this.CheckAddLuckHeroUI();
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x0014081C File Offset: 0x0013EA1C
	private void InitMeHeroList()
	{
		int nType = this.GetFbXmlData().nType;
		bool flag = nType == 11;
		int num;
		if (flag)
		{
			num = 1;
			this.oGrsdBg.SetActive(true);
		}
		else
		{
			this.oGrsdBg.SetActive(false);
			num = 5;
		}
		for (int i = 0; i < num; i++)
		{
			Transform transform = this.herogroup.transform.Find(i.ToString());
			bool flag2 = transform == null;
			GameObject gameObject;
			if (flag2)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CloneOnehero);
				gameObject.transform.SetParent(this.herogroup.transform);
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
			OneOwnHeroTopCon component = gameObject.GetComponent<OneOwnHeroTopCon>();
			bool flag3 = component == null;
			if (flag3)
			{
				gameObject.AddComponent<OneOwnHeroTopCon>();
			}
			bool flag4 = nType == 11;
			if (flag4)
			{
				gameObject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(555f, 0f, 0f);
			}
			else
			{
				gameObject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3((float)i * 260f, 0f, 0f);
			}
			bool flag5 = !this.m_szMeHeroList.Contains(gameObject);
			if (flag5)
			{
				this.m_szMeHeroList.Add(gameObject);
			}
		}
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x001409D8 File Offset: 0x0013EBD8
	private void InitSelectHeroList()
	{
		foreach (object obj in this.herolist.transform)
		{
			Transform transform = (Transform)obj;
			OneSelectHeroTopCon component = transform.gameObject.GetComponent<OneSelectHeroTopCon>();
			bool flag = component == null;
			if (flag)
			{
				transform.gameObject.AddComponent<OneSelectHeroTopCon>();
			}
			bool flag2 = !this.m_szHeroSelectList.Contains(transform.gameObject);
			if (flag2)
			{
				this.m_szHeroSelectList.Add(transform.gameObject);
				EventTriggerListener.Get(transform.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectHeroOk);
			}
		}
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x00140AAC File Offset: 0x0013ECAC
	public void InitUI()
	{
		this.InitHeroTypeList();
		this.oNowSelectHero = null;
		this.oNowSelectHeroType = null;
		this.m_nNowMyHeroId = 0;
		this.m_nMySelectStat = 0;
		this.m_nSeletHero = 0;
		this.m_nNowSelectHeroType = 0;
		this.m_sReadyTime = string.Empty;
		this.attacktype.text = string.Empty;
		this.zbjdtext.text = string.Empty;
		this.lockmsg.text = string.Empty;
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x00140B2C File Offset: 0x0013ED2C
	public FbXmlData GetFbXmlData()
	{
		return MatchManager.GetInstance().m_pFbData;
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x00140B48 File Offset: 0x0013ED48
	private void OnShowGameData()
	{
		int playerTeam = roleuiManager.GetInstance().GetPlayerTeam();
		int nType = this.GetFbXmlData().nType;
		bool flag = nType == 2 || nType == 1;
		if (flag)
		{
			bool flag2 = playerTeam == 2;
			if (flag2)
			{
				this.attacktype.text = Language.GetLang(46156, new object[0]);
				this.attacktyecolor.topColor = Fun.StrToColor("00FFFF", 1f);
			}
			else
			{
				this.attacktype.text = Language.GetLang(46155, new object[0]);
				this.attacktyecolor.topColor = Color.red;
			}
		}
		this.zbjdtext.text = string.Empty;
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x00140C0C File Offset: 0x0013EE0C
	public void show(string sUpdateName)
	{
		this.m_sUpdateName = sUpdateName;
		this.heroplayer = fGameManager.GetInstance().getPlayer();
		bool flag = this.heroplayer != null && this.heroplayer.playerZhunxingMe != null;
		if (flag)
		{
			this.heroplayer.playerZhunxingMe.zhunxingshowOrHide(false);
		}
		this.readyText.text = Language.GetLang(2020, new object[0]);
		bool flag2 = this.GetFbXmlData().nType == 11;
		if (flag2)
		{
			this.readyText.text = Language.GetLang(2002, new object[0]);
		}
		fUIManager.GetInstance().GetIconAsset(string.Format("selectherobg/{0}.txt", this.GetFbXmlData().nloadmap), this.uibgimg);
		this.OnShowGameData();
		this.sencenameText.text = this.GetFbXmlData().sName;
		fUIManager.mUILoading.SetRawImg(this.GetFbXmlData().nScnid.ToString(), this.GetFbXmlData().sMapAss);
		UserData nowUserData = roleuiManager.GetInstance().GetNowUserData();
		bool flag3 = nowUserData == null || (nowUserData != null && MatchManager.GetInstance().m_nIsChangeHero == 0 && nowUserData.m_nIsChsok == 1 && this.m_sReadyTime == string.Empty);
		if (flag3)
		{
			bool activeSelf = this.readyfight.activeSelf;
			if (activeSelf)
			{
				this.readyfight.SetActive(false);
			}
		}
		else
		{
			bool flag4 = this.m_sReadyTime == string.Empty;
			if (flag4)
			{
				this.m_sReadyTime = Fun.GetServerDate().AddSeconds((double)MatchManager.GetInstance().m_nSelectTime).ToString();
			}
			this.OnShowReayTime();
		}
		this.OnShowGameList();
		Transform transform = this.herotypelist.transform.Find(this.m_nNowSelectHeroType.ToString());
		bool flag5 = transform != null;
		if (flag5)
		{
			this.OnSelectHeroTypeTab(transform.gameObject);
		}
		int index = 1015;
		bool flag6 = fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().playerCommon != null;
		if (flag6)
		{
			index = fGameManager.GetInstance().getPlayer().playerCommon.m_nModelIndex;
		}
		string text = GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(index).Key_Check_Hero_Information);
		this.lookkeytxt.text = text;
		int num = Encoding.Default.GetBytes(text).Length;
		float num2 = (float)num * 25f;
		bool flag7 = num2 <= 25f;
		if (flag7)
		{
			num2 = 40f;
		}
		this.lookkeygrld.cellSize = new Vector2(num2, 50f);
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x00140EDC File Offset: 0x0013F0DC
	public void OnShowGameList()
	{
		ArrayList arrayList = new ArrayList();
		string text = string.Empty;
		string text2 = string.Empty;
		arrayList = roleuiManager.GetInstance().GetGameUserList(0, 0);
		int count = arrayList.Count;
		bool flag = count > 0;
		if (flag)
		{
			text2 = arrayList[0].ToString();
			UserData userData = roleuiManager.GetInstance().GetUserData(text2);
			bool flag2 = userData != null;
			if (flag2)
			{
				this.m_nNowMyHeroId = userData.m_nHeroID;
				this.m_nMySelectStat = userData.m_nIsChsok;
			}
		}
		bool flag3 = this.m_nSeletHero == 0;
		if (flag3)
		{
			this.m_nSeletHero = this.m_nNowMyHeroId;
		}
		int count2 = this.m_szMeHeroList.Count;
		for (int i = 0; i < count2; i++)
		{
			GameObject gameObject = this.m_szMeHeroList[i];
			OneOwnHeroTopCon component = gameObject.GetComponent<OneOwnHeroTopCon>();
			text = string.Empty;
			bool flag4 = i < count;
			if (flag4)
			{
				text = arrayList[i].ToString();
			}
			bool flag5 = !(text == text2) || this.m_nFindHero == 0;
			if (flag5)
			{
				component.OnShowData(text, arrayList, i);
			}
		}
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00141008 File Offset: 0x0013F208
	private void OnShowTopSelectData(int nWeiZhi, int nHeroId, bool mbool)
	{
		int count = this.m_szMeHeroList.Count;
		bool flag = count >= 1;
		if (flag)
		{
			GameObject gameObject = this.m_szMeHeroList[nWeiZhi];
			OneOwnHeroTopCon component = gameObject.GetComponent<OneOwnHeroTopCon>();
			component.OnShowTopImg(nHeroId, mbool);
			this.OnUpdateQueDingBut();
		}
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x00141054 File Offset: 0x0013F254
	private void OnShowHeroList(int nHeroType)
	{
		ArrayList arrayList = new ArrayList();
		arrayList = roleuiManager.GetInstance().GetHeroList(nHeroType);
		int count = arrayList.Count;
		int count2 = this.m_szHeroSelectList.Count;
		for (int i = 0; i < count2; i++)
		{
			GameObject gameObject = this.m_szHeroSelectList[i];
			OneSelectHeroTopCon component = gameObject.GetComponent<OneSelectHeroTopCon>();
			int nHeroId = 0;
			bool flag = i < count;
			if (flag)
			{
				nHeroId = (int)arrayList[i];
			}
			component.OnShowData(nHeroId, i, this.m_nNowMyHeroId);
		}
		string name = this.m_nSeletHero.ToString();
		bool flag2 = this.m_nFindHero != 0;
		if (flag2)
		{
			name = this.m_nFindHero.ToString();
		}
		Transform transform = this.herolist.transform.Find(name);
		bool flag3 = transform != null;
		if (flag3)
		{
			this.OnSelectHeroList(transform.gameObject);
		}
		this.OnUpdateQueDingBut();
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x00141150 File Offset: 0x0013F350
	private void OnUpdateQueDingBut()
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.m_nSeletHero == this.m_nFindHero && this.m_nFindHero != 0;
		if (flag)
		{
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(this.m_nFindHero);
			bool flag2 = oneHeroData != null && oneHeroData.unlockData != null && oneHeroData.unlockData.GetUnlockValue("6") != string.Empty;
			if (flag2)
			{
				this.lockmsg.text = "<color='#ff8400'>" + oneHeroData.unlockData.sUnlockDesc + "</color>";
				bool activeSelf = this.quedingbut.activeSelf;
				if (activeSelf)
				{
					this.quedingbut.SetActive(false);
				}
			}
			else
			{
				this.lockmsg.text = string.Empty;
				bool flag3 = !this.quedingbut.activeSelf;
				if (flag3)
				{
					this.quedingbut.SetActive(true);
				}
				bool flag4 = !this.herolist.activeSelf;
				if (flag4)
				{
					this.herolist.SetActive(true);
				}
				bool activeSelf2 = this.zhunbei.activeSelf;
				if (activeSelf2)
				{
					this.zhunbei.SetActive(false);
				}
				this.quedingbut.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2047, new object[0]);
				this.quedingbut.transform.Find("Image").GetComponent<Image>().color = Fun.StrToColor("ffffff", 1f);
				this.quedingbut.GetComponent<Button>().interactable = true;
			}
		}
		else
		{
			this.lockmsg.text = string.Empty;
			bool flag5 = !this.quedingbut.activeSelf;
			if (flag5)
			{
				this.quedingbut.SetActive(true);
			}
			arrayList = roleuiManager.GetInstance().GetTeamSelectOkHero();
			int num = arrayList.IndexOf(this.m_nSeletHero);
			bool flag6 = num != -1 && MatchManager.GetInstance().m_pFbData.nType != 11;
			if (flag6)
			{
				this.quedingbut.transform.Find("Image").GetComponent<Image>().color = Fun.StrToColor("ffffff", 0.2f);
				this.quedingbut.GetComponent<Button>().interactable = false;
			}
			else
			{
				this.quedingbut.transform.Find("Image").GetComponent<Image>().color = Fun.StrToColor("ffffff", 1f);
				this.quedingbut.GetComponent<Button>().interactable = true;
			}
			string b = (this.heroplayer != null && this.heroplayer.playerCommon != null) ? this.heroplayer.playerCommon.m_sName : roleuiManager.GetInstance().GetMeName();
			bool flag7 = !(this.m_sUpdateName == string.Empty) && !(this.m_sUpdateName == b);
			if (!flag7)
			{
				bool flag8 = this.m_nMySelectStat == 1 && MatchManager.GetInstance().m_nIsChangeHero == 1;
				if (flag8)
				{
					bool activeSelf3 = this.herolist.activeSelf;
					if (activeSelf3)
					{
						this.herolist.SetActive(false);
					}
					bool flag9 = !this.zhunbei.activeSelf;
					if (flag9)
					{
						this.zhunbei.SetActive(true);
					}
					this.quedingbut.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2019, new object[0]);
				}
				else
				{
					bool flag10 = !this.herolist.activeSelf;
					if (flag10)
					{
						this.herolist.SetActive(true);
					}
					bool activeSelf4 = this.zhunbei.activeSelf;
					if (activeSelf4)
					{
						this.zhunbei.SetActive(false);
					}
					this.quedingbut.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2003, new object[0]);
				}
			}
		}
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x0014156C File Offset: 0x0013F76C
	private bool OnSelectHeroList(GameObject obj)
	{
		string name = obj.name;
		int num = name.IndexOf("hero");
		bool flag = num != -1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			OneSelectHeroTopCon component = obj.GetComponent<OneSelectHeroTopCon>();
			bool flag2 = component == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num2 = int.Parse(name);
				bool flag3 = this.oNowSelectHero != null && this.oNowSelectHero.transform.Find("select").gameObject.activeSelf;
				if (flag3)
				{
					this.oNowSelectHero.transform.Find("select").gameObject.SetActive(false);
				}
				this.oNowSelectHero = obj;
				obj.transform.Find("select").gameObject.SetActive(true);
				this.m_nFindHero = 0;
				this.m_nSeletHero = num2;
				this.m_sUpdateName = string.Empty;
				bool flag4 = component.m_nHeroOpenStat == 0;
				if (flag4)
				{
					this.m_nFindHero = num2;
					this.OnShowTopSelectData(0, num2, true);
					result = false;
				}
				else
				{
					this.OnShowTopSelectData(0, num2, false);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x00141698 File Offset: 0x0013F898
	private void OnSelectHeroOk(GameObject obj)
	{
		fUISoundManager.instance.PlayUISound("jiemian_yingxiongxuanzhong", 0, null, 20);
		bool flag = this.OnSelectHeroList(obj);
		if (flag)
		{
			UserData nowUserData = roleuiManager.GetInstance().GetNowUserData();
			bool flag2 = (MatchManager.GetInstance().m_nIsChangeHero == 1 || (MatchManager.GetInstance().m_nIsChangeHero == 0 && nowUserData.m_nIsChsok == 0)) && this.m_nMySelectStat == 0;
			if (flag2)
			{
				MatchManager.GetInstance().OnSelectHero(this.m_nSeletHero, this.m_nMySelectStat);
			}
			else
			{
				this.OnShowTopSelectData(0, this.m_nSeletHero, false);
			}
		}
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x00141734 File Offset: 0x0013F934
	private void InitHeroTypeList()
	{
		Vector3 localPosition = this.herotypelist.transform.localPosition;
		ArrayList arrayList = new ArrayList(fGameManager.mloadConfig.HeroCareerList.Keys);
		arrayList.Insert(0, 0);
		localPosition.x = -375f;
		bool flag = MatchManager.GetInstance().m_nMatchType == 3;
		if (flag)
		{
			arrayList = new ArrayList
			{
				0,
				1,
				2,
				3
			};
			localPosition.x = -290f;
		}
		this.herotypelist.transform.localPosition = localPosition;
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			int nType = (int)arrayList[i];
			Transform transform = this.herotypelist.transform.Find(nType.ToString());
			bool flag2 = transform == null;
			GameObject gameObject;
			if (flag2)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cloneherotype);
				gameObject.transform.SetParent(this.herotypelist.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = nType.ToString();
				EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectHeroType);
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
			}
			else
			{
				gameObject = transform.gameObject;
			}
			this.getHeroTypeData(gameObject, nType);
		}
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x00141930 File Offset: 0x0013FB30
	private void getHeroTypeData(GameObject oClone, int nType)
	{
		Text component = oClone.transform.Find("name").GetComponent<Text>();
		Image component2 = oClone.transform.Find("ico").GetComponent<Image>();
		string lang = Language.GetLang(2006, new object[0]);
		string text = string.Empty;
		bool flag = MatchManager.GetInstance().m_nMatchType == 3;
		if (flag)
		{
			switch (nType)
			{
			case 0:
				lang = Language.GetLang(2006, new object[0]);
				oClone.transform.Find("name").localPosition = new Vector3(25f, 0f, 0f);
				break;
			case 1:
				lang = Language.GetLang(2049, new object[0]);
				text = "zhongjia";
				break;
			case 2:
				lang = Language.GetLang(2008, new object[0]);
				text = "fangyu";
				break;
			case 3:
				lang = Language.GetLang(2010, new object[0]);
				text = "fuzhu";
				break;
			}
		}
		else
		{
			switch (nType)
			{
			case 0:
				lang = Language.GetLang(2006, new object[0]);
				oClone.transform.Find("name").localPosition = new Vector3(25f, 0f, 0f);
				break;
			case 1:
				lang = Language.GetLang(2007, new object[0]);
				text = "tuji";
				break;
			case 2:
				lang = Language.GetLang(2008, new object[0]);
				text = "fangyu";
				break;
			case 3:
				lang = Language.GetLang(2009, new object[0]);
				text = "zhongjia";
				break;
			case 4:
				lang = Language.GetLang(2010, new object[0]);
				text = "fuzhu";
				break;
			}
		}
		component.text = lang;
		bool flag2 = text != string.Empty;
		if (flag2)
		{
			component2.enabled = true;
			component2.sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, text);
		}
		else
		{
			component2.enabled = false;
		}
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x00141B54 File Offset: 0x0013FD54
	private void OnSelectHeroType(GameObject obj)
	{
		this.m_sUpdateName = string.Empty;
		this.OnSelectHeroTypeTab(obj);
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x00141B6C File Offset: 0x0013FD6C
	private void OnSelectHeroTypeTab(GameObject obj)
	{
		string name = obj.name;
		bool flag = this.oNowSelectHeroType != null;
		if (flag)
		{
			this.oNowSelectHeroType.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "leixinganniu_normal");
		}
		int num = int.Parse(name);
		this.oNowSelectHeroType = obj;
		this.m_nNowSelectHeroType = num;
		obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "leixinganniu_ces");
		this.OnShowHeroList(num);
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x00141C0C File Offset: 0x0013FE0C
	private void OnEnterTitle(GameObject obj)
	{
		bool flag = !(this.oNowSelectHeroType == obj);
		if (flag)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "leixinganniu_up");
		}
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x00141C5C File Offset: 0x0013FE5C
	private void OnExitTitle(GameObject obj)
	{
		bool flag = !(this.oNowSelectHeroType == obj);
		if (flag)
		{
			obj.transform.Find("bg").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "leixinganniu_normal");
		}
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x00141CAC File Offset: 0x0013FEAC
	private void OnQueDingBut(GameObject obj)
	{
		int num = 1;
		bool flag = this.m_nMySelectStat == 1 && MatchManager.GetInstance().m_nIsChangeHero == 1;
		if (flag)
		{
			num = 0;
		}
		bool flag2 = num == 1 && this.m_nFindHero != 0;
		if (flag2)
		{
			HeroDataManager.GetInstance().OpenUnlockTip(this.m_nFindHero, 0, 0, string.Empty, 1);
		}
		else
		{
			bool flag3 = this.m_nNowMyHeroId == this.m_nSeletHero && this.m_nMySelectStat == 1 && MatchManager.GetInstance().m_nIsChangeHero == 0;
			if (flag3)
			{
				this.Close();
			}
			else
			{
				this.OnOkSelectHero(this.m_nSeletHero, num);
				bool flag4 = MatchManager.GetInstance().m_pFbData.nType == 6 && MatchManager.GetInstance().m_nIsChangeHero == 1;
				if (flag4)
				{
					this.OnEnterGame();
				}
			}
		}
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x00141D84 File Offset: 0x0013FF84
	public void OnHeroLockOver(int nSeletHero, int nStat)
	{
		ArrayList teamSelectOkHero = roleuiManager.GetInstance().GetTeamSelectOkHero();
		int num = teamSelectOkHero.IndexOf(nSeletHero);
		bool flag = num != -1;
		if (flag)
		{
			this.show(string.Empty);
		}
		else
		{
			this.OnOkSelectHero(nSeletHero, nStat);
		}
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x00141DD0 File Offset: 0x0013FFD0
	public void OnOkSelectHero(int nSeletHero, int nStat)
	{
		this.m_nSeletHero = nSeletHero;
		MatchManager.GetInstance().OnSelectHero(nSeletHero, nStat);
		UserData userData = roleuiManager.GetInstance().GetUserData(roleuiManager.GetInstance().GetMeName());
		bool flag = MatchManager.GetInstance().m_nIsChangeHero == 0 && userData.m_nIsChsok == 1;
		if (flag)
		{
			this.Close();
		}
		bool flag2 = this.m_nMySelectStat == 1;
		if (flag2)
		{
			fUISoundManager.instance.PlayUISound("jiemian_yingxiongxuanzhong", 0, null, 20);
		}
		else
		{
			fUISoundManager.instance.PlayUISound("jiemian_yingxiongqueding", 0, null, 20);
		}
		EventSystem.current.SetSelectedGameObject(this.uibgimg.gameObject);
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x00141E7D File Offset: 0x0014007D
	private void OnEnterGame()
	{
		MatchManager.GetInstance().OnEnterGame();
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x00141E8B File Offset: 0x0014008B
	private void OnShowReayTime()
	{
		base.CancelInvoke("OnGameDjsTime");
		base.InvokeRepeating("OnGameDjsTime", 0f, 1f);
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x00141EB0 File Offset: 0x001400B0
	private void OnGameDjsTime()
	{
		int endCDTime = Fun.GetEndCDTime(this.m_sReadyTime, 0);
		bool flag = endCDTime < 1;
		if (flag)
		{
			base.CancelInvoke("OnGameDjsTime");
			this.ready_cd.text = "0";
			this.readyfight.SetActive(false);
			this.OnEnterGame();
		}
		else
		{
			bool flag2 = !this.readyfight.activeSelf;
			if (flag2)
			{
				this.readyfight.SetActive(true);
			}
			this.ready_cd.text = endCDTime.ToString();
		}
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x00141F40 File Offset: 0x00140140
	public override void Close()
	{
		bool flag = base.IsInvoking("OnGameDjsTime") && fGameManager.IsJoinGame;
		if (flag)
		{
			base.CancelInvoke("OnGameDjsTime");
		}
		this.CloseLuckHeroWin();
		this.CloseMeHeroList();
		this.InitUI();
		this.m_nFindHero = 0;
		bool flag2 = gameuiCon.inst != null && gameuiCon.inst.visible;
		if (flag2)
		{
			roleuiManager.GetInstance().bOtherUiVisable = false;
		}
		bool flag3 = this.heroplayer != null && this.heroplayer.playerZhunxingMe != null;
		if (flag3)
		{
			this.heroplayer.playerZhunxingMe.zhunxingshowOrHide(true);
		}
		bool flag4 = this.GetFbXmlData() != null && !string.IsNullOrEmpty(this.GetFbXmlData().selectsound);
		if (flag4)
		{
			fUISoundManager.instance.StopSound(this.GetFbXmlData().selectsound, 0);
		}
		HeroPlayer.isPlayerControl = true;
		HeroPlayer.isMouseShow = false;
		base.Close();
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x00142040 File Offset: 0x00140240
	private void CloseMeHeroList()
	{
		int count = this.m_szMeHeroList.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = this.m_szMeHeroList[i];
			OneOwnHeroTopCon component = gameObject.GetComponent<OneOwnHeroTopCon>();
			component.InitUI();
		}
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x0014208C File Offset: 0x0014028C
	private void OpenLuckHeroWin(int nLuckHeroID)
	{
		string text = string.Empty;
		this.LuckHeroData = HeroDataManager.GetInstance().GetOneHeroData(nLuckHeroID);
		bool flag = this.LuckHeroData == null;
		if (!flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = false;
			fUIManager.tipBlur.rawImage = this.rewardTipBg;
			fUIManager.tipBlur.enabled = true;
			this.m_nLuckHeroID = nLuckHeroID;
			this.LuckHeroTopText.text = Language.GetLang(8009, new object[0]);
			this.LuckHeroContentText.text = this.LuckHeroData.unlockData.sUnlockDesc;
			this.MoneyTip.text = Language.GetLang(19006, new object[0]);
			text = this.LuckHeroData.unlockData.GetUnlockValue("5");
			bool flag2 = text != string.Empty;
			if (flag2)
			{
				this.jb_OkBtn.SetActive(true);
				int num = int.Parse(text);
				int gameCurrency = PlayerDataManager.getInstance().playerdet.gameCurrency;
				bool flag3 = gameCurrency < num;
				if (flag3)
				{
					this.jbmsgtext.text = "<color='#FF0000'>" + Language.GetLang(39016, new object[0]) + "</color>";
					this.jb_OkBtn.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "findroom_dahuang_dis");
					this.jb_OkBtn.GetComponent<Button>().interactable = false;
				}
				else
				{
					this.jbmsgtext.text = string.Empty;
					this.jb_OkBtn.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "findroom_dahong_normal");
					this.jb_OkBtn.GetComponent<Button>().interactable = true;
				}
			}
			else
			{
				this.jb_OkBtn.SetActive(false);
			}
			text = this.LuckHeroData.unlockData.GetUnlockValue("2");
			bool flag4 = text != string.Empty;
			if (flag4)
			{
				this.zs_OkBtn.SetActive(true);
			}
			else
			{
				this.zs_OkBtn.SetActive(false);
			}
			bool flag5 = !this.oLuckHeroWin.activeSelf;
			if (flag5)
			{
				this.oLuckHeroWin.SetActive(true);
			}
		}
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x001422C8 File Offset: 0x001404C8
	private void CheckAddLuckHeroUI()
	{
		bool flag = this.oLuckHeroWin.activeSelf && this.rewardTipBg.texture != null;
		if (flag)
		{
			this.rewardTipBg.enabled = true;
		}
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x0014230C File Offset: 0x0014050C
	public void CloseLuckHeroWin()
	{
		this.LuckHeroTopText.text = string.Empty;
		this.LuckHeroContentText.text = string.Empty;
		this.MoneyTip.text = string.Empty;
		this.m_nLuckHeroID = 0;
		this.LuckHeroData = null;
		GameRoot.inst.Setting.Load_Setting_Data_State = true;
		this.rewardTipBg.enabled = false;
		bool activeSelf = this.oLuckHeroWin.activeSelf;
		if (activeSelf)
		{
			this.oLuckHeroWin.SetActive(false);
		}
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x001423B4 File Offset: 0x001405B4
	private void OnLuckHeroEvent(int nValue)
	{
		bool flag = nValue != -1 && this.m_nLuckHeroID != 0 && this.LuckHeroData != null;
		if (flag)
		{
			bool flag2 = nValue == 2;
			if (flag2)
			{
				int num = int.Parse(this.LuckHeroData.unlockData.GetUnlockValue("2"));
				int chargeCurrency = PlayerDataManager.getInstance().playerdet.chargeCurrency;
				bool flag3 = chargeCurrency < num;
				if (flag3)
				{
					fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
					this.CloseLuckHeroWin();
					return;
				}
			}
			bool flag4 = nValue == 5;
			if (flag4)
			{
				int num2 = int.Parse(this.LuckHeroData.unlockData.GetUnlockValue("5"));
				int gameCurrency = PlayerDataManager.getInstance().playerdet.gameCurrency;
				bool flag5 = gameCurrency < num2;
				if (flag5)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39016, new object[0]), "ffdd00", "system");
					return;
				}
			}
		}
		this.CloseLuckHeroWin();
	}

	// Token: 0x04001FC5 RID: 8133
	public static SelectHeroUICon inst;

	// Token: 0x04001FC6 RID: 8134
	private RawImage uibgimg;

	// Token: 0x04001FC7 RID: 8135
	private GameObject herogroup;

	// Token: 0x04001FC8 RID: 8136
	private GameObject CloneOnehero;

	// Token: 0x04001FC9 RID: 8137
	private GameObject readyfight;

	// Token: 0x04001FCA RID: 8138
	private GameObject cloneherotype;

	// Token: 0x04001FCB RID: 8139
	private GameObject herotypelist;

	// Token: 0x04001FCC RID: 8140
	private GameObject herolist;

	// Token: 0x04001FCD RID: 8141
	private GameObject zhunbei;

	// Token: 0x04001FCE RID: 8142
	private GameObject quedingbut;

	// Token: 0x04001FCF RID: 8143
	private Text ready_cd;

	// Token: 0x04001FD0 RID: 8144
	private Text readyText;

	// Token: 0x04001FD1 RID: 8145
	private Text sencenameText;

	// Token: 0x04001FD2 RID: 8146
	private Text attacktype;

	// Token: 0x04001FD3 RID: 8147
	private Text zbjdtext;

	// Token: 0x04001FD4 RID: 8148
	private GridLayoutGroup lookkeygrld;

	// Token: 0x04001FD5 RID: 8149
	private Text lookkeytxt;

	// Token: 0x04001FD6 RID: 8150
	private List<GameObject> m_szHeroSelectList = new List<GameObject>();

	// Token: 0x04001FD7 RID: 8151
	private List<GameObject> m_szMeHeroList = new List<GameObject>();

	// Token: 0x04001FD8 RID: 8152
	private GameObject oNowSelectHero;

	// Token: 0x04001FD9 RID: 8153
	private GameObject oNowSelectHeroType;

	// Token: 0x04001FDA RID: 8154
	private int m_nNowMyHeroId;

	// Token: 0x04001FDB RID: 8155
	private int m_nMySelectStat;

	// Token: 0x04001FDC RID: 8156
	public int m_nSeletHero;

	// Token: 0x04001FDD RID: 8157
	private int m_nFindHero;

	// Token: 0x04001FDE RID: 8158
	private int m_nNowSelectHeroType;

	// Token: 0x04001FDF RID: 8159
	private string m_sReadyTime = string.Empty;

	// Token: 0x04001FE0 RID: 8160
	private GameObject oLuckHeroWin;

	// Token: 0x04001FE1 RID: 8161
	private Text LuckHeroTopText;

	// Token: 0x04001FE2 RID: 8162
	private Text LuckHeroContentText;

	// Token: 0x04001FE3 RID: 8163
	private Text MoneyTip;

	// Token: 0x04001FE4 RID: 8164
	private GameObject zs_OkBtn;

	// Token: 0x04001FE5 RID: 8165
	private GameObject jb_OkBtn;

	// Token: 0x04001FE6 RID: 8166
	private GameObject CancelBtn;

	// Token: 0x04001FE7 RID: 8167
	private Text lockmsg;

	// Token: 0x04001FE8 RID: 8168
	private Text jbmsgtext;

	// Token: 0x04001FE9 RID: 8169
	private HeroPlayer heroplayer;

	// Token: 0x04001FEA RID: 8170
	private RawImage rewardTipBg;

	// Token: 0x04001FEB RID: 8171
	private GameObject oGrsdBg;

	// Token: 0x04001FEC RID: 8172
	private GradientTowColor attacktyecolor;

	// Token: 0x04001FED RID: 8173
	private string m_sUpdateName = string.Empty;

	// Token: 0x04001FEE RID: 8174
	private int m_nLuckHeroID;

	// Token: 0x04001FEF RID: 8175
	private OpenHeroData LuckHeroData;
}
