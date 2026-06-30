using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010F RID: 271
public class easy_tipCon : Window
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060009BD RID: 2493 RVA: 0x000578B4 File Offset: 0x00055AB4
	// (set) Token: 0x060009BE RID: 2494 RVA: 0x000578CC File Offset: 0x00055ACC
	public Text ContentText
	{
		get
		{
			return this.contentText;
		}
		set
		{
			this.contentText = value;
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x000578D8 File Offset: 0x00055AD8
	protected override void Awake()
	{
		base.Awake();
		UIDepth uidepth = base.gameObject.AddComponent<UIDepth>();
		base.gameObject.AddComponent<GraphicRaycaster>();
		uidepth.SetUIDepth(5, true);
		this.OneMsg = this.UIGameObj.Get_GameObj(1);
		this.ObjListParent = this.UIGameObj.Get_GameObj(0);
		this.OneMsg.SetActive(false);
		this.easy_tipObj = this.UIGameObj.Get_GameObj(2);
		this.OnCloseTipMenu();
		this.text_tipObj = this.UIGameObj.Get_GameObj(3);
		this.tipText = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.text_tipObj.SetActive(false);
		this.common_tipObj = this.UIGameObj.Get_GameObj(5);
		this.comtipBg = this.common_tipObj.GetComponent<RawImage>();
		this.titleText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.contentText = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.tipToggle = this.UIGameObj.Get_GameObj(10).GetComponent<Toggle>();
		EventTriggerListener.Get(this.tipToggle.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.tipToggle.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.toggleText = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.tipTextUI = this.UIGameObj.Get_GameObj(18);
		this.tipTextLb = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		this.tipTextSp = this.UIGameObj.Get_GameObj(20).GetComponent<Image>();
		this.okBtn = this.UIGameObj.Get_GameObj(8);
		this.okBtnTxt = this.okBtn.GetComponentInChildren<Text>();
		EventTriggerListener.Get(this.okBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOk);
		EventTriggerListener.Get(this.okBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.cancelBtn = this.UIGameObj.Get_GameObj(9);
		this.cancelBtnTxt = this.cancelBtn.GetComponentInChildren<Text>();
		EventTriggerListener.Get(this.cancelBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCancel);
		EventTriggerListener.Get(this.cancelBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.tipsText = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.tipZhekouObj = this.UIGameObj.Get_GameObj(52);
		this.tipZhekouRimg = this.tipZhekouObj.GetComponent<RawImage>();
		EventTriggerListener.Get(this.tipZhekouObj).onEnter = new EventTriggerListener.VoidDelegate(this.OverZhekouObj);
		EventTriggerListener.Get(this.tipZhekouObj).onExit = new EventTriggerListener.VoidDelegate(this.ExitZhekouObj);
		this.tipZhekouNum = this.UIGameObj.Get_GameObj(53).GetComponent<Text>();
		this.tipZhekouObj.SetActive(false);
		this.common_tipObj.SetActive(false);
		this.rewardTipObj = this.UIGameObj.Get_GameObj(12);
		this.rewardTipBg = this.rewardTipObj.GetComponent<RawImage>();
		this.rewardHead = this.UIGameObj.Get_GameObj(22);
		this.rewardHeadIcon = this.rewardHead.transform.Find("RawImage").GetComponent<RawImage>();
		this.rewardTipTitle = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.rewardTipIcon = this.UIGameObj.Get_GameObj(14).GetComponent<RawImage>();
		this.rewardTipQual = this.UIGameObj.Get_GameObj(15).GetComponent<Image>();
		this.rewardTipDesc = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.rewardTipBtn = this.UIGameObj.Get_GameObj(17);
		EventTriggerListener.Get(this.rewardTipBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOk);
		EventTriggerListener.Get(this.rewardTipBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.rewardTipObj.SetActive(false);
		this.itemTipObj = this.UIGameObj.Get_GameObj(24);
		this.itemTipBg = this.itemTipObj.GetComponent<RawImage>();
		this.itemTitleTxt = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.itemContentTxt = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.itemGrid = this.UIGameObj.Get_GameObj(27);
		this.itemGrid.SetActive(false);
		this.itemOkBtn = this.UIGameObj.Get_GameObj(28);
		EventTriggerListener.Get(this.itemOkBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOk);
		EventTriggerListener.Get(this.itemOkBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.itemOkTxt = this.itemOkBtn.transform.Find("Text").GetComponent<Text>();
		this.itemTipObj.SetActive(false);
		this.unlockTipObj = this.UIGameObj.Get_GameObj(30);
		this.unlockTipBg = this.unlockTipObj.GetComponent<RawImage>();
		this.unlockTitleTxt = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.unlockContentTxt = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.unlockMsgTxt = this.UIGameObj.Get_GameObj(33).GetComponent<Text>();
		this.zsUnlockBtn = this.UIGameObj.Get_GameObj(34);
		EventTriggerListener.Get(this.zsUnlockBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnUnlockAClick);
		EventTriggerListener.Get(this.zsUnlockBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.zsUnlockTxt = this.zsUnlockBtn.transform.Find("Render/Text").GetComponent<Text>();
		this.zsHeroReturnSp = this.zsUnlockBtn.transform.Find("Image").GetComponent<Image>();
		this.jbUnlockBtn = this.UIGameObj.Get_GameObj(35);
		EventTriggerListener.Get(this.jbUnlockBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnUnlockBClick);
		EventTriggerListener.Get(this.jbUnlockBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.jbUnlockTxt = this.jbUnlockBtn.transform.Find("Text").GetComponent<Text>();
		this.unlockCancelBtn = this.UIGameObj.Get_GameObj(36);
		EventTriggerListener.Get(this.unlockCancelBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnUnlockCancel);
		EventTriggerListener.Get(this.unlockCancelBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.unlockWarnTxt = this.UIGameObj.Get_GameObj(37).GetComponent<Text>();
		this.unlockNotice = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.unlockDiscountSp = this.UIGameObj.Get_GameObj(39).GetComponent<Image>();
		this.unlockDiscountTxt = this.UIGameObj.Get_GameObj(40).GetComponent<Text>();
		this.unlockDiscountXian = this.UIGameObj.Get_GameObj(41).GetComponent<Image>();
		this.unlockToggle = this.UIGameObj.Get_GameObj(54).GetComponent<Toggle>();
		EventTriggerListener.Get(this.unlockToggle.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnUnlockToggle);
		EventTriggerListener.Get(this.unlockToggle.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.unlockToggleTxt = this.UIGameObj.Get_GameObj(55).GetComponent<Text>();
		this.unlockZhekouObj = this.UIGameObj.Get_GameObj(56);
		this.unlockZhekouRimg = this.unlockZhekouObj.GetComponent<RawImage>();
		EventTriggerListener.Get(this.unlockZhekouObj).onEnter = new EventTriggerListener.VoidDelegate(this.OverZhekouObj);
		EventTriggerListener.Get(this.unlockZhekouObj).onExit = new EventTriggerListener.VoidDelegate(this.ExitZhekouObj);
		this.unlockZhekouNum = this.UIGameObj.Get_GameObj(57).GetComponent<Text>();
		this.unlockToggle.gameObject.SetActive(false);
		this.unlockZhekouObj.SetActive(false);
		this.unlockTipObj.SetActive(false);
		this.inputTipObj = this.UIGameObj.Get_GameObj(42);
		this.inputTipBg = this.inputTipObj.GetComponent<RawImage>();
		this.inputTitle = this.UIGameObj.Get_GameObj(43).GetComponent<Text>();
		this.inputField = this.UIGameObj.Get_GameObj(44).GetComponent<InputField>();
		this.inputOkBtn = this.UIGameObj.Get_GameObj(45);
		EventTriggerListener.Get(this.inputOkBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnInputClickOk);
		EventTriggerListener.Get(this.inputOkBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.inputOkTxt = this.inputOkBtn.transform.Find("Text").GetComponent<Text>();
		this.inputCancelBtn = this.UIGameObj.Get_GameObj(46);
		EventTriggerListener.Get(this.inputCancelBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCancel);
		EventTriggerListener.Get(this.inputCancelBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.inputCancelTxt = this.inputCancelBtn.transform.Find("Text").GetComponent<Text>();
		this.inputTipObj.SetActive(false);
		this.relinkTipObj = this.UIGameObj.Get_GameObj(47);
		this.relinkTipBg = this.relinkTipObj.GetComponent<RawImage>();
		this.relinkTipTitle = this.UIGameObj.Get_GameObj(48).GetComponent<Text>();
		this.relinkTipContent = this.UIGameObj.Get_GameObj(49).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(50);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOk);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.relinkYesTxt = gameObject.transform.Find("Text").GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(51);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCancel);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.relinkNoTxt = gameObject.transform.Find("Text").GetComponent<Text>();
		this.relinkTipObj.SetActive(false);
		easy_tipCon.inst = this;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00058362 File Offset: 0x00056562
	private void Start()
	{
		this.isFristOpen = false;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0005836C File Offset: 0x0005656C
	private void Update()
	{
		bool flag = this.comTipState > 0 && this.comtipBg.texture != null;
		if (flag)
		{
			this.comtipBg.enabled = true;
			bool flag2 = this.comTipState == 1 && this.delayTime > 0f;
			if (flag2)
			{
				base.CancelInvoke("CloseDelayTip");
				base.Invoke("CloseDelayTip", this.delayTime);
			}
			this.comTipState = 0;
		}
		bool flag3 = this.rewardTipBg.texture != null;
		if (flag3)
		{
			this.rewardTipBg.enabled = true;
			bool flag4 = !this.rewardTipObj.activeSelf;
			if (flag4)
			{
				this.rewardTipObj.SetActive(true);
			}
		}
		bool flag5 = this.itemTipBg.texture != null;
		if (flag5)
		{
			this.itemTipBg.enabled = true;
			bool flag6 = !this.itemTipObj.activeSelf;
			if (flag6)
			{
				this.itemTipObj.SetActive(true);
			}
		}
		bool flag7 = this.unlockTipBg.texture != null;
		if (flag7)
		{
			this.unlockTipBg.enabled = true;
			bool flag8 = !this.unlockTipObj.activeSelf;
			if (flag8)
			{
				this.unlockTipObj.SetActive(true);
			}
		}
		bool flag9 = this.inputTipBg.texture != null;
		if (flag9)
		{
			this.inputTipBg.enabled = true;
		}
		bool flag10 = this.relinkTipBg.texture != null;
		if (flag10)
		{
			this.relinkTipBg.enabled = true;
		}
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		if (mouseButtonDown)
		{
			this.GetTheClick();
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00058528 File Offset: 0x00056728
	private void GetTheClick()
	{
		bool flag = this.easy_tipObj.activeSelf && !(fUIManager.uiCamera == null);
		if (flag)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = fUIManager.uiCamera.WorldToScreenPoint(this.easy_tipObj.transform.position);
			bool flag2 = mousePosition.x < vector.x || mousePosition.x > vector.x + 130f || mousePosition.y > vector.y || mousePosition.y < vector.y - (float)this.nHeight;
			if (flag2)
			{
				this.OnCloseTipMenu();
			}
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000585D4 File Offset: 0x000567D4
	public void OnCloseTipMenu()
	{
		this.easy_tipObj.SetActive(false);
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.isTeamTip = false;
		}
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0005860C File Offset: 0x0005680C
	private ArrayList OnCheckPublicBtnState(int nType, ArrayList szBtnState)
	{
		int[] typeList = this.GetTypeList(nType);
		bool flag = typeList == null;
		ArrayList result;
		if (flag)
		{
			result = szBtnState;
		}
		else
		{
			int num = typeList.Length;
			bool flag2 = szBtnState == null;
			if (flag2)
			{
				szBtnState = new ArrayList();
			}
			bool flag3 = this.m_sNick == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag3)
			{
				szBtnState.Add(num - 3);
				szBtnState.Add(num - 2);
			}
			else
			{
				bool flag4 = ChatManager.GetInstance().isPingBiUser(this.m_sNick);
				if (flag4)
				{
					szBtnState.Add(num - 3);
				}
				else
				{
					szBtnState.Add(num - 2);
				}
			}
			result = szBtnState;
		}
		return result;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x000586CC File Offset: 0x000568CC
	private void InitEasyTip(int nType, ArrayList szBtnState)
	{
		szBtnState = this.OnCheckPublicBtnState(nType, szBtnState);
		bool flag = this.nSelectType == nType;
		if (flag)
		{
			bool flag2 = szBtnState == null;
			if (!flag2)
			{
				int num = this.szBtnList.Count;
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = this.szBtnList[i];
					Button component = gameObject.GetComponent<Button>();
					bool flag3 = szBtnState != null;
					if (flag3)
					{
						int num2 = szBtnState.IndexOf(i);
						bool flag4 = num2 != -1;
						if (flag4)
						{
							component.gameObject.SetActive(false);
						}
						else
						{
							component.gameObject.SetActive(true);
						}
					}
				}
			}
		}
		else
		{
			this.nHeight = 0;
			fUIManager.OnClearObj(this.szBtnList);
			this.szBtnList.Clear();
			this.nSelectType = nType;
			int[] typeList = this.GetTypeList(nType);
			bool flag5 = typeList == null;
			if (!flag5)
			{
				int num = typeList.Length;
				for (int j = 0; j < num; j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneMsg);
					gameObject.transform.SetParent(this.ObjListParent.transform);
					gameObject.SetActive(true);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					Text component2 = gameObject.transform.FindChild("Text").GetComponent<Text>();
					component2.text = Language.GetLang(typeList[j], new object[0]);
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					int sMsg = typeList[j];
					Button component3 = gameObject.GetComponent<Button>();
					bool flag6 = szBtnState != null;
					if (flag6)
					{
						int num2 = szBtnState.IndexOf(j);
						bool flag7 = num2 != -1;
						if (flag7)
						{
							component3.gameObject.SetActive(false);
						}
						else
						{
							component3.gameObject.SetActive(true);
						}
					}
					component3.onClick.RemoveAllListeners();
					component3.onClick.AddListener(delegate()
					{
						this.OnBtnClick(sMsg);
					});
					this.szBtnList.Add(gameObject);
				}
				this.nHeight = num * 30 + (num - 1) + 30;
			}
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0005894E File Offset: 0x00056B4E
	public void InitName(string sNick, int nType = 1, ArrayList szBtnState = null, int nCid = 0)
	{
		this.m_sNick = sNick;
		this.m_nCid = nCid;
		this.InitEasyTip(nType, szBtnState);
		this.easy_tipObj.SetActive(true);
		base.StartCoroutine(this.OnShowTip());
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00058983 File Offset: 0x00056B83
	public IEnumerator OnShowTip()
	{
		yield return new WaitForSeconds(0.001f);
		this.mousepos(this.easy_tipObj, 20f, -20f);
		yield break;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x00058994 File Offset: 0x00056B94
	private int[] GetTypeList(int nType)
	{
		int[] result = null;
		switch (nType)
		{
		case 1:
			result = this.sChatList;
			break;
		case 2:
			result = this.sFriendList;
			break;
		case 3:
			result = this.sGuildList;
			break;
		case 4:
			result = this.sFriendLatelyList;
			break;
		case 5:
			result = this.sTeamList;
			break;
		}
		return result;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000589F8 File Offset: 0x00056BF8
	private void OnBtnClick(int nid)
	{
		bool flag = fUIManager.GetInstance().sChatName == string.Empty;
		if (flag)
		{
			Debug.LogError("没有选中玩家");
		}
		else
		{
			bool flag2 = this.m_sNick == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag2)
			{
				this.OnCloseTipMenu();
				if (nid != 47002)
				{
					if (nid != 47009)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(47016, new object[0]), "ffdd00", "system");
					}
				}
				else
				{
					TextEditor textEditor = new TextEditor();
					textEditor.text = this.m_sNick;
					textEditor.OnFocus();
					textEditor.Copy();
				}
			}
			else
			{
				switch (nid)
				{
				case 47000:
				{
					bool flag3 = ChatUICon.inst != null;
					if (flag3)
					{
						ChatUICon.inst.AddUserSiLiaoList(this.m_sNick);
					}
					break;
				}
				case 47001:
					GuildManager.GetInstance().OnInviteSendMsgGuild(this.m_sNick);
					break;
				case 47002:
				{
					TextEditor textEditor2 = new TextEditor();
					textEditor2.text = this.m_sNick;
					textEditor2.OnFocus();
					textEditor2.Copy();
					break;
				}
				case 47003:
					FriendManager.GetInstance().AddFriend(this.m_sNick);
					break;
				case 47004:
					FriendManager.GetInstance().DelFriend(fUIManager.GetInstance().sChatName);
					break;
				case 47005:
					this.JoinToMe();
					break;
				case 47006:
					this.OpenReport(fUIManager.GetInstance().sChatName);
					break;
				case 47007:
					ChatManager.GetInstance().OnSetPingBiUser(this.m_sNick);
					break;
				case 47008:
					ChatManager.GetInstance().OnQuXiaoPingBi(this.m_sNick);
					break;
				case 47010:
					GuildManager.GetInstance().OnDemiseSendMsgGuild(this.m_sNick);
					break;
				case 47011:
					GuildManager.GetInstance().OnSetZhiWeiSendMsgGuild(this.m_sNick, 1);
					break;
				case 47012:
					GuildManager.GetInstance().OnSetZhiWeiSendMsgGuild(this.m_sNick, 0);
					break;
				case 47013:
				{
					bool flag4 = TeamManager.GetInstance().Dyaoqing.Count > 0;
					if (flag4)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
						this.OnCloseTipMenu();
						return;
					}
					bool flag5 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
					if (flag5)
					{
						bool flag6 = TeamManager.GetInstance().m_nCaptain == 1;
						if (flag6)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8030, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnLeaveTeam), null, null, null);
						}
						else
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8029, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnLeaveTeam), null, null, null);
						}
						this.OnCloseTipMenu();
						return;
					}
					bool flag7 = MatchManager.GetInstance().m_nIsMatch > 0;
					if (flag7)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40025, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnLeaveTeam), null, null, null);
						this.OnCloseTipMenu();
						return;
					}
					this.OnLeaveTeam(null);
					break;
				}
				case 47014:
					this.OnChangeCap();
					break;
				case 47015:
					this.OnKickUser();
					break;
				}
				this.OnCloseTipMenu();
			}
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00058E2A File Offset: 0x0005702A
	private void JoinToOther()
	{
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00058E30 File Offset: 0x00057030
	private void JoinToMe()
	{
		bool flag = TeamManager.GetInstance().MyTeamData.Contains(fUIManager.GetInstance().sChatName);
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(8031, new object[]
			{
				fUIManager.GetInstance().sChatName
			}), "ffdd00", "system");
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().MyTeamData.Count >= 10;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40005, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().MyTeamData.Count / 2 + TeamManager.GetInstance().Dyaoqing.Count >= 5;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40005, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag4 = MatchManager.GetInstance().m_nIsMatch > 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40026, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.JoinToMeOK), null, null, null);
					}
					else
					{
						bool flag5 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0 && fGameManager.Heroplayer == null;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40013, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.JoinToMeOK), null, null, null);
						}
						else
						{
							this.JoinToMeOK(null);
						}
					}
				}
			}
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00059038 File Offset: 0x00057238
	private void OnLeaveTeam(object szParam)
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch > 0;
		if (flag)
		{
			MatchManager.GetInstance().OnCancelMatch();
		}
		TeamManager.GetInstance().OnLeaveTeam();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00059070 File Offset: 0x00057270
	private void JoinToMeOK(object szParam)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40017, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().Dyaoqing.Count >= 4;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag3)
				{
					RoomManage.GetInstance().OnLeave();
				}
				bool flag4 = MatchManager.GetInstance().m_nIsMatch > 0;
				if (flag4)
				{
					MatchManager.GetInstance().OnCancelMatch();
				}
				bool flag5 = TeamManager.GetInstance().Dyaoqing.ContainsKey(fUIManager.GetInstance().sChatName);
				if (flag5)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40032, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag6 = MainUICon.inst != null;
					if (flag6)
					{
						MainUICon.inst.SetTeamCD(fUIManager.GetInstance().sChatName);
					}
					ArrayList arrayList = new ArrayList();
					arrayList.Add(60005);
					arrayList.Add(Fun.EncodingUTF8(fUIManager.GetInstance().sChatName));
					fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40014, new object[0]), "ffdd00", "system");
				}
			}
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00059214 File Offset: 0x00057414
	private void OnChangeCap()
	{
		bool flag = TeamManager.GetInstance().Dyaoqing.Count > 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40030, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = fGameManager.LobbyPlayer != null;
				if (flag3)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(60004);
					arrayList.Add(Fun.EncodingUTF8(fUIManager.GetInstance().sChatName));
					fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
				}
			}
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00059310 File Offset: 0x00057510
	private void OnKickUser()
	{
		bool flag = TeamManager.GetInstance().Dyaoqing.Count > 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40030, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = fGameManager.LobbyPlayer != null;
				if (flag3)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(60009);
					arrayList.Add(Fun.EncodingUTF8(fUIManager.GetInstance().sChatName));
					fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
				}
			}
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0005940C File Offset: 0x0005760C
	public void ShowTextTip(string sText, TextAnchor align, float nline, float xpos, float ypos)
	{
		bool flag = !string.IsNullOrEmpty(sText);
		if (flag)
		{
			this.tipText.alignment = align;
			this.tipText.lineSpacing = nline;
			this.tipText.text = sText;
			this.mousepos(this.text_tipObj, xpos, ypos);
			this.text_tipObj.SetActive(true);
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00059470 File Offset: 0x00057670
	public void HideTextTip()
	{
		bool activeSelf = this.text_tipObj.activeSelf;
		if (activeSelf)
		{
			this.text_tipObj.SetActive(false);
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0005949C File Offset: 0x0005769C
	private void mousepos(GameObject obj, float xpos = 20f, float ypos = -20f)
	{
		Transform transform = obj.transform;
		Vector3 mousePosition = Input.mousePosition;
		RectTransform component = obj.transform.Find("tip/Content").GetComponent<RectTransform>();
		int num = Mathf.CeilToInt(component.sizeDelta.x);
		int num2 = Mathf.CeilToInt(component.sizeDelta.y);
		float screenWidth = SysConfig.ScreenWidth;
		float screenHeight = SysConfig.ScreenHeight;
		mousePosition.x = Mathf.Lerp(-0.5f * screenWidth, 0.5f * screenWidth, mousePosition.x / (float)Screen.width) + xpos;
		mousePosition.y = Mathf.Lerp(-0.5f * screenHeight, 0.5f * screenHeight, mousePosition.y / (float)Screen.height) + ypos;
		mousePosition.z = 0f;
		bool flag = mousePosition.x + (float)num > screenWidth / 2f;
		if (flag)
		{
			mousePosition.x = screenWidth / 2f - (float)num;
		}
		bool flag2 = mousePosition.y - (float)num2 < screenHeight / -2f;
		if (flag2)
		{
			mousePosition.y = (float)num2 - screenHeight / 2f;
		}
		transform.localPosition = mousePosition;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x000595C4 File Offset: 0x000577C4
	public void ShowCommonTip(string title, string content, string oktxt, string canceltxt, easy_tipCon.TipOKDelegate okfun, object okparam, easy_tipCon.TipCancelDelegate cancelfun, object cancelparam, string toggletxt, bool toggleon, easy_tipCon.TipToggleDelegate togglefun, object toggleparam, string sreward, string simg, string stip, float delay = 2f)
	{
		this.comTipState = 1;
		this.titleText.text = title;
		this.tipsText.text = stip;
		bool flag = string.IsNullOrEmpty(content);
		if (flag)
		{
			this.contentText.gameObject.SetActive(false);
		}
		else
		{
			this.contentText.gameObject.SetActive(true);
			this.contentText.text = content;
		}
		bool flag2 = string.IsNullOrEmpty(toggletxt);
		if (flag2)
		{
			this.tipToggle.gameObject.SetActive(false);
		}
		else
		{
			this.tipToggle.gameObject.SetActive(true);
			this.toggleText.text = toggletxt;
			this.tipToggle.isOn = toggleon;
		}
		bool flag3 = string.IsNullOrEmpty(sreward) && string.IsNullOrEmpty(simg);
		if (flag3)
		{
			this.tipTextUI.SetActive(false);
		}
		else
		{
			this.tipTextUI.SetActive(true);
			this.tipTextLb.text = sreward;
			bool flag4 = string.IsNullOrEmpty(simg);
			if (flag4)
			{
				this.tipTextSp.enabled = false;
			}
			else
			{
				this.tipTextSp.enabled = true;
				this.tipTextSp.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, simg);
				this.tipTextSp.SetNativeSize();
			}
		}
		this.tipZhekouObj.SetActive(false);
		this.okFunc = okfun;
		this.okParam = okparam;
		this.cancelFunc = cancelfun;
		this.cancelParam = cancelparam;
		this.toggleFunc = togglefun;
		this.toggleParam = toggleparam;
		this.delayTime = delay;
		bool flag5 = string.IsNullOrEmpty(oktxt);
		if (flag5)
		{
			this.okBtn.SetActive(false);
		}
		else
		{
			this.comTipState++;
			this.okBtnTxt.text = oktxt;
			this.okBtn.SetActive(true);
		}
		bool flag6 = string.IsNullOrEmpty(canceltxt);
		if (flag6)
		{
			this.cancelBtn.SetActive(false);
		}
		else
		{
			this.comTipState++;
			this.cancelBtnTxt.text = canceltxt;
			this.cancelBtn.SetActive(true);
		}
		this.comtipBg.enabled = false;
		this.comtipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.comtipBg;
		fUIManager.tipBlur.enabled = true;
		this.common_tipObj.SetActive(true);
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00059838 File Offset: 0x00057A38
	public void ShowTipZhekouObj()
	{
		ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
		int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(1027);
		this.tipZhekouObj.SetActive(true);
		fUIManager.GetInstance().GetIconAsset("ItemIco/" + itemData.m_sIco + ".txt", this.tipZhekouRimg);
		this.tipZhekouNum.text = haveItemNum.ToString();
		bool flag = haveItemNum < 1;
		if (flag)
		{
			this.tipToggle.interactable = false;
		}
		else
		{
			this.tipToggle.interactable = true;
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000598DC File Offset: 0x00057ADC
	public void ShowRelinkTip(string title, string content, int tipid, int delay, string canceltxt, easy_tipCon.TipOKDelegate okfun, object okparam, easy_tipCon.TipCancelDelegate cancelfun, object cancelparam)
	{
		this.relinkTipTitle.text = title;
		this.relinkTipContent.text = content;
		this.okFunc = okfun;
		this.okParam = okparam;
		this.cancelFunc = cancelfun;
		this.cancelParam = cancelparam;
		this.nZhTipId = tipid;
		this.delayTime = (float)delay;
		this.relinkYesTxt.text = Language.GetLang(this.nZhTipId, new object[]
		{
			this.delayTime
		});
		this.relinkNoTxt.text = canceltxt;
		base.CancelInvoke("CloseBtnDelayTip");
		base.InvokeRepeating("CloseBtnDelayTip", 1f, 1f);
		this.relinkTipBg.enabled = false;
		this.relinkTipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.relinkTipBg;
		fUIManager.tipBlur.enabled = true;
		this.relinkTipObj.SetActive(true);
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000599D8 File Offset: 0x00057BD8
	public void OnKeyPress()
	{
		bool flag = Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return);
		if (flag)
		{
			bool activeSelf = this.okBtn.activeSelf;
			if (activeSelf)
			{
				this.OnClickOk(this.okBtn);
			}
			else
			{
				bool activeSelf2 = this.cancelBtn.activeSelf;
				if (activeSelf2)
				{
					this.OnClickCancel(this.cancelBtn);
				}
			}
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Escape);
		if (keyDown)
		{
			bool activeSelf3 = this.cancelBtn.activeSelf;
			if (activeSelf3)
			{
				this.OnClickCancel(this.cancelBtn);
			}
			else
			{
				bool activeSelf4 = this.okBtn.activeSelf;
				if (activeSelf4)
				{
					this.OnClickOk(this.okBtn);
				}
			}
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00059A90 File Offset: 0x00057C90
	private void OnClickToggle(GameObject go)
	{
		bool flag = this.toggleFunc != null;
		if (flag)
		{
			this.toggleFunc(this.toggleParam, this.tipToggle.isOn);
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x00059ACC File Offset: 0x00057CCC
	private void OnClickCancel(GameObject go)
	{
		this.CloseCommonTip();
		this.CloseRewardTip();
		this.CloseItemTip();
		this.CloseNumInputTip();
		this.CloseRelinkTip();
		bool flag = this.cancelFunc != null;
		if (flag)
		{
			this.cancelFunc(this.cancelParam);
		}
		this.okFunc = null;
		this.okParam = null;
		this.cancelFunc = null;
		this.cancelParam = null;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00059B3C File Offset: 0x00057D3C
	private void OnInputClickOk(GameObject go)
	{
		bool flag = this.inputFunc != null;
		if (flag)
		{
			this.inputFunc(this.inputParam, this.inputField.text);
		}
		this.CloseNumInputTip();
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00059B80 File Offset: 0x00057D80
	private void OnClickOk(GameObject go)
	{
		this.CloseCommonTip();
		this.CloseRewardTip();
		this.CloseItemTip();
		this.CloseRelinkTip();
		bool flag = this.okFunc != null;
		if (flag)
		{
			this.okFunc(this.okParam);
		}
		this.okFunc = null;
		this.okParam = null;
		this.cancelFunc = null;
		this.cancelParam = null;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00059BE8 File Offset: 0x00057DE8
	private void CloseDelayTip()
	{
		this.CloseCommonTip();
		this.delayTime = 2f;
		bool flag = this.okFunc != null;
		if (flag)
		{
			this.okFunc(this.okParam);
		}
		bool flag2 = this.okFunc != null && this.okParam is TipMsgData;
		if (flag2)
		{
			TipManager.GetInstance().OnPopDelayOver(this.okParam);
		}
		this.okFunc = null;
		this.okParam = null;
		this.cancelFunc = null;
		this.cancelParam = null;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00059C78 File Offset: 0x00057E78
	private void CloseBtnDelayTip()
	{
		this.delayTime -= 1f;
		this.relinkYesTxt.text = Language.GetLang(this.nZhTipId, new object[]
		{
			this.delayTime
		});
		bool flag = this.delayTime == 0f;
		if (flag)
		{
			this.CloseRelinkTip();
			this.delayTime = 2f;
			bool flag2 = this.okFunc != null;
			if (flag2)
			{
				this.okFunc(this.okParam);
			}
			this.okFunc = null;
			this.okParam = null;
			this.cancelFunc = null;
			this.cancelParam = null;
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00059D28 File Offset: 0x00057F28
	private void CloseCommonTip()
	{
		this.comtipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.comtipBg.texture);
		this.comtipBg.enabled = false;
		bool activeSelf = this.common_tipObj.activeSelf;
		if (activeSelf)
		{
			this.common_tipObj.SetActive(false);
			bool flag = fGameManager.m_nUserType != 1 || !(SettingCon.Instance != null) || !SettingCon.Instance.visible;
			if (flag)
			{
				HeroPlayer.isMouseShow = false;
			}
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00059DB0 File Offset: 0x00057FB0
	private void CloseRelinkTip()
	{
		this.relinkTipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.relinkTipBg.texture);
		this.relinkTipBg.enabled = false;
		bool activeSelf = this.relinkTipObj.activeSelf;
		if (activeSelf)
		{
			this.relinkTipObj.SetActive(false);
			HeroPlayer.isMouseShow = false;
		}
		bool flag = base.IsInvoking("CloseBtnDelayTip");
		if (flag)
		{
			base.CancelInvoke("CloseBtnDelayTip");
			this.nZhTipId = 0;
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00059E34 File Offset: 0x00058034
	public void ShowRewardTip(string shead, string title, string sicon, string squal, string sdesc, easy_tipCon.TipOKDelegate okfun, object okparam)
	{
		bool flag = string.IsNullOrEmpty(shead);
		if (flag)
		{
			this.rewardHead.SetActive(false);
		}
		else
		{
			fUIManager.GetInstance().GetIconAsset(shead, this.rewardHeadIcon);
			this.rewardHead.SetActive(true);
		}
		bool flag2 = string.IsNullOrEmpty(title);
		if (flag2)
		{
			this.rewardTipTitle.gameObject.SetActive(false);
		}
		else
		{
			this.rewardTipTitle.text = title;
			this.rewardTipTitle.gameObject.SetActive(true);
		}
		bool flag3 = !string.IsNullOrEmpty(sicon);
		if (flag3)
		{
			fUIManager.GetInstance().GetIconAsset(sicon, this.rewardTipIcon);
		}
		bool flag4 = string.IsNullOrEmpty(squal);
		if (flag4)
		{
			this.rewardTipQual.enabled = false;
		}
		else
		{
			this.rewardTipQual.enabled = true;
			this.rewardTipQual.sprite = Window.spriteName(AtlasType.ATLAS_HEROUI, squal);
		}
		this.rewardTipDesc.text = sdesc;
		this.okFunc = okfun;
		this.okParam = okparam;
		this.rewardTipBg.enabled = false;
		this.rewardTipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.rewardTipBg;
		fUIManager.tipBlur.enabled = true;
		this.rewardTipObj.SetActive(true);
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00059F8C File Offset: 0x0005818C
	private void CloseRewardTip()
	{
		this.rewardTipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.rewardTipBg.texture);
		this.rewardTipBg.enabled = false;
		bool activeSelf = this.rewardTipObj.activeSelf;
		if (activeSelf)
		{
			this.rewardTipObj.SetActive(false);
			HeroPlayer.isMouseShow = false;
		}
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00059FEC File Offset: 0x000581EC
	public void ShowItemTip(string title, string content, ArrayList szitem, string oktxt, easy_tipCon.TipOKDelegate okfun, object okparam)
	{
		bool flag = string.IsNullOrEmpty(title);
		if (flag)
		{
			this.itemTitleTxt.enabled = false;
		}
		else
		{
			this.itemTitleTxt.enabled = true;
			this.itemTitleTxt.text = title;
		}
		bool flag2 = string.IsNullOrEmpty(content);
		if (flag2)
		{
			this.itemContentTxt.enabled = false;
		}
		else
		{
			this.itemContentTxt.enabled = true;
			this.itemContentTxt.text = content;
		}
		fUIManager.OnClearObj(this.szItemGrid);
		for (int i = 0; i < szitem.Count; i += 2)
		{
			int num = (int)szitem[i];
			bool flag3 = fGameManager.mloadConfig.AllItemData.ContainsKey(num);
			if (flag3)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[num];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemGrid);
				gameObject.transform.SetParent(this.itemGrid.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.SetActive(true);
				gameObject.name = string.Format("ItemGrid{0}", num);
				Image component = gameObject.transform.Find("QualSp").GetComponent<Image>();
				component.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, Fun.GetQualBgStr(itemData.m_nQuality));
				RawImage component2 = gameObject.transform.Find("QualSp/Image").GetComponent<RawImage>();
				Text component3 = gameObject.transform.Find("NumTxt").GetComponent<Text>();
				component3.text = szitem[i + 1].ToString();
				component3 = gameObject.transform.Find("NameTxt").GetComponent<Text>();
				component3.text = itemData.m_sName;
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
				this.szItemGrid.Add(gameObject);
			}
		}
		bool flag4 = string.IsNullOrEmpty(oktxt);
		if (flag4)
		{
			this.itemOkTxt.text = Language.GetLang(8009, new object[0]);
		}
		else
		{
			this.itemOkTxt.text = oktxt;
		}
		this.okFunc = okfun;
		this.okParam = okparam;
		this.itemTipBg.enabled = false;
		this.itemTipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.itemTipBg;
		fUIManager.tipBlur.enabled = true;
		this.itemTipObj.SetActive(true);
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0005A2C0 File Offset: 0x000584C0
	private void CloseItemTip()
	{
		this.itemTipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.itemTipBg.texture);
		this.itemTipBg.enabled = false;
		bool activeSelf = this.itemTipObj.activeSelf;
		if (activeSelf)
		{
			this.itemTipObj.SetActive(false);
			HeroPlayer.isMouseShow = false;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0005A320 File Offset: 0x00058520
	public void ShowUnlockTip(string title, string content, string msg, string zsbtn, string jbbtn, string tip, string snotice, easy_tipCon.UnlockADelegate afunc = null, object aparam = null, easy_tipCon.UnlockBDelegate bfunc = null, object bparam = null, string sdiscount = "", int ndiscount = 0, int iszhekou = 0)
	{
		this.unlockTitleTxt.text = title;
		bool flag = string.IsNullOrEmpty(sdiscount);
		if (flag)
		{
			this.unlockContentTxt.text = content;
			this.unlockDiscountSp.gameObject.SetActive(false);
			this.unlockDiscountTxt.text = string.Empty;
			this.unlockDiscountXian.enabled = false;
		}
		else
		{
			this.unlockContentTxt.text = content + "\n" + sdiscount;
			this.unlockDiscountSp.gameObject.SetActive(true);
			this.unlockDiscountTxt.text = Language.GetLang(7052, new object[]
			{
				ndiscount
			});
			this.unlockDiscountXian.enabled = true;
			this.unlockDiscountXian.rectTransform.sizeDelta = new Vector2(this.unlockContentTxt.preferredWidth, 3f);
		}
		this.unlockMsgTxt.text = msg;
		bool flag2 = string.IsNullOrEmpty(zsbtn);
		if (flag2)
		{
			this.zsUnlockBtn.SetActive(false);
		}
		else
		{
			this.zsUnlockTxt.text = zsbtn;
			this.zsUnlockBtn.SetActive(true);
		}
		bool flag3 = string.IsNullOrEmpty(jbbtn);
		if (flag3)
		{
			this.jbUnlockBtn.SetActive(false);
		}
		else
		{
			this.jbUnlockTxt.text = jbbtn;
			this.jbUnlockBtn.SetActive(true);
		}
		this.unlockWarnTxt.text = tip;
		this.unlockNotice.text = snotice;
		this.unlockAFunc = afunc;
		this.unlockAParam = aparam;
		this.unlockBFunc = bfunc;
		this.unlockBParam = bparam;
		ArrayList arrayList = (ArrayList)aparam;
		bool flag4 = (int)arrayList[6] > 0;
		if (flag4)
		{
			this.zsHeroReturnSp.gameObject.SetActive(LiBaoManager.GetInstance().CheckHeroReturn((int)arrayList[0], (int)arrayList[1], (int)arrayList[2]));
		}
		else
		{
			this.zsHeroReturnSp.gameObject.SetActive(false);
		}
		arrayList = (ArrayList)bparam;
		bool flag5 = (int)arrayList[4] == 0;
		if (flag5)
		{
			this.jbUnlockBtn.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.jbUnlockBtn.GetComponent<Button>().interactable = true;
		}
		bool flag6 = iszhekou == 1;
		if (flag6)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(1027);
			this.unlockZhekouObj.SetActive(true);
			fUIManager.GetInstance().GetIconAsset("ItemIco/" + itemData.m_sIco + ".txt", this.unlockZhekouRimg);
			this.unlockToggle.gameObject.SetActive(true);
			this.unlockZhekouNum.text = haveItemNum.ToString();
			this.unlockToggleTxt.text = Language.GetLang(39543, new object[]
			{
				haveItemNum
			});
			bool flag7 = haveItemNum < 1;
			if (flag7)
			{
				this.unlockToggle.interactable = false;
			}
			else
			{
				this.unlockToggle.interactable = true;
			}
			this.unlockChargeVal = (int)(this.unlockAParam as ArrayList)[6];
		}
		else
		{
			this.unlockZhekouObj.SetActive(false);
			this.unlockToggle.gameObject.SetActive(false);
		}
		this.unlockToggle.isOn = false;
		this.unlockTipBg.enabled = false;
		this.unlockTipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.unlockTipBg;
		fUIManager.tipBlur.enabled = true;
		this.unlockTipObj.SetActive(true);
		HeroPlayer.isMouseShow = true;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0005A6FC File Offset: 0x000588FC
	private void OnUnlockCancel(GameObject go)
	{
		this.CloseUnlockTip();
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0005A708 File Offset: 0x00058908
	private void OnUnlockBClick(GameObject go)
	{
		this.CloseUnlockTip();
		bool flag = this.unlockBFunc != null;
		if (flag)
		{
			this.unlockBFunc(this.unlockBParam, this.unlockToggle.isOn);
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0005A74C File Offset: 0x0005894C
	private void OnUnlockAClick(GameObject go)
	{
		this.CloseUnlockTip();
		bool flag = this.unlockAFunc != null;
		if (flag)
		{
			this.unlockAFunc(this.unlockAParam, this.unlockToggle.isOn);
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0005A790 File Offset: 0x00058990
	public void CloseUnlockTip()
	{
		this.unlockTipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.unlockTipBg.texture);
		this.unlockTipBg.enabled = false;
		bool activeSelf = this.unlockTipObj.activeSelf;
		if (activeSelf)
		{
			this.unlockTipObj.SetActive(false);
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0005A7E8 File Offset: 0x000589E8
	public void ClearPopTip(bool isback = true)
	{
		bool flag = this.okParam is TipMsgData && isback;
		if (flag)
		{
			TipManager.GetInstance().OnPopDelayOver(this.okParam);
		}
		this.CloseRewardTip();
		this.CloseItemTip();
		this.CloseCommonTip();
		this.CloseUnlockTip();
		this.CloseNumInputTip();
		this.CloseRelinkTip();
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0005A848 File Offset: 0x00058A48
	public void OpenReport(string unick)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.m_nCid);
		arrayList.Add(unick);
		fUIManager.GetInstance().OpenPlayerReportUI(arrayList);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0005A884 File Offset: 0x00058A84
	public void ShowInputTip(string title, string inputtip, string oktxt, string canceltxt, easy_tipCon.TipInputDelegate okfun, object okparam, easy_tipCon.TipCancelDelegate cancelfun, object cancelparam, int length, float width, float height)
	{
		this.inputTitle.text = title;
		this.inputField.placeholder.GetComponent<Text>().text = inputtip;
		this.inputField.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		this.inputField.characterLimit = length;
		this.inputField.text = string.Empty;
		this.inputFunc = okfun;
		this.inputParam = okparam;
		this.cancelFunc = cancelfun;
		this.cancelParam = cancelparam;
		bool flag = string.IsNullOrEmpty(oktxt);
		if (flag)
		{
			this.inputOkBtn.SetActive(false);
		}
		else
		{
			this.inputOkTxt.text = oktxt;
			this.inputOkBtn.SetActive(true);
		}
		bool flag2 = string.IsNullOrEmpty(canceltxt);
		if (flag2)
		{
			this.inputCancelBtn.SetActive(false);
		}
		else
		{
			this.inputCancelTxt.text = canceltxt;
			this.inputCancelBtn.SetActive(true);
		}
		this.inputTipBg.enabled = false;
		this.inputTipBg.texture = null;
		fUIManager.tipBlur.rawImage = this.inputTipBg;
		fUIManager.tipBlur.enabled = true;
		this.inputTipObj.SetActive(true);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0005A9C4 File Offset: 0x00058BC4
	public void CloseNumInputTip()
	{
		this.inputTipBg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.inputTipBg.texture);
		this.inputTipBg.enabled = false;
		bool activeSelf = this.inputTipObj.activeSelf;
		if (activeSelf)
		{
			this.inputTipObj.SetActive(false);
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0005AA1B File Offset: 0x00058C1B
	private void ExitZhekouObj(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0005AA2C File Offset: 0x00058C2C
	private void OverZhekouObj(GameObject go)
	{
		int itemNumById = ShopManager.GetInstance().GetItemNumById(1027);
		ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
		string text = string.Concat(new string[]
		{
			"<size=20><color=",
			Fun.GetQualColor(itemData.m_nQuality),
			">",
			itemData.m_sName,
			"</color></size>"
		});
		text = text + "\n" + itemData.m_sdesc;
		text = text + "\n" + Language.GetLang(39542, new object[]
		{
			itemNumById
		});
		TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0005AAF0 File Offset: 0x00058CF0
	private void OnUnlockToggle(GameObject go)
	{
		int num = ShopManager.GetInstance().GetHaveItemNum(1027);
		bool flag = num < 1;
		if (!flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[1027];
			bool flag2 = !(this.unlockAParam is ArrayList);
			if (!flag2)
			{
				int nSlcHeroId = (int)(this.unlockAParam as ArrayList)[0];
				int nSlcType = (int)(this.unlockAParam as ArrayList)[1];
				int nSlcId = (int)(this.unlockAParam as ArrayList)[2];
				num = (int)(this.unlockAParam as ArrayList)[6];
				bool flag3 = HeroDataManager.GetInstance().GetEquipZhekou(nSlcHeroId, nSlcType, nSlcId) == 1;
				if (flag3)
				{
					bool isOn = this.unlockToggle.isOn;
					if (isOn)
					{
						num = Mathf.CeilToInt((float)(num * itemData.nZhekou) / 100f);
					}
					string text = this.unlockContentTxt.text;
					text = text.Replace(this.unlockChargeVal.ToString(), num.ToString());
					this.unlockChargeVal = num;
					this.unlockContentTxt.text = text;
				}
			}
		}
	}

	// Token: 0x040007A5 RID: 1957
	public static easy_tipCon inst;

	// Token: 0x040007A6 RID: 1958
	public GameObject easy_tipObj;

	// Token: 0x040007A7 RID: 1959
	public bool isFristOpen = true;

	// Token: 0x040007A8 RID: 1960
	private GameObject OneMsg;

	// Token: 0x040007A9 RID: 1961
	public GameObject ObjListParent;

	// Token: 0x040007AA RID: 1962
	private string m_sNick = string.Empty;

	// Token: 0x040007AB RID: 1963
	private int m_nCid;

	// Token: 0x040007AC RID: 1964
	private int nSelectType;

	// Token: 0x040007AD RID: 1965
	private int[] sChatList = new int[]
	{
		47000,
		47001,
		47002,
		47003,
		47004,
		47005,
		47006,
		47007,
		47008,
		47009
	};

	// Token: 0x040007AE RID: 1966
	private int[] sFriendList = new int[]
	{
		47000,
		47004,
		47005,
		47001,
		47002,
		47006,
		47007,
		47008,
		47009
	};

	// Token: 0x040007AF RID: 1967
	private int[] sFriendLatelyList = new int[]
	{
		47000,
		47003,
		47005,
		47001,
		47002,
		47006,
		47007,
		47008,
		47009
	};

	// Token: 0x040007B0 RID: 1968
	private int[] sGuildList = new int[]
	{
		47000,
		47001,
		47002,
		47003,
		47004,
		47010,
		47011,
		47012,
		47005,
		47006,
		47007,
		47008,
		47009
	};

	// Token: 0x040007B1 RID: 1969
	private int[] sTeamList = new int[]
	{
		47000,
		47013,
		47014,
		47015,
		47003,
		47004,
		47006,
		47007,
		47008,
		47009
	};

	// Token: 0x040007B2 RID: 1970
	public int nHeight;

	// Token: 0x040007B3 RID: 1971
	private List<GameObject> szBtnList = new List<GameObject>();

	// Token: 0x040007B4 RID: 1972
	private GameObject text_tipObj;

	// Token: 0x040007B5 RID: 1973
	private Text tipText;

	// Token: 0x040007B6 RID: 1974
	private GameObject common_tipObj;

	// Token: 0x040007B7 RID: 1975
	private RawImage comtipBg;

	// Token: 0x040007B8 RID: 1976
	private Text titleText;

	// Token: 0x040007B9 RID: 1977
	private Text contentText;

	// Token: 0x040007BA RID: 1978
	private Text tipsText;

	// Token: 0x040007BB RID: 1979
	private Toggle tipToggle;

	// Token: 0x040007BC RID: 1980
	private Text toggleText;

	// Token: 0x040007BD RID: 1981
	private GameObject tipTextUI;

	// Token: 0x040007BE RID: 1982
	private Text tipTextLb;

	// Token: 0x040007BF RID: 1983
	private Image tipTextSp;

	// Token: 0x040007C0 RID: 1984
	private GameObject okBtn;

	// Token: 0x040007C1 RID: 1985
	private Text okBtnTxt;

	// Token: 0x040007C2 RID: 1986
	private GameObject cancelBtn;

	// Token: 0x040007C3 RID: 1987
	private Text cancelBtnTxt;

	// Token: 0x040007C4 RID: 1988
	private GameObject tipZhekouObj;

	// Token: 0x040007C5 RID: 1989
	private RawImage tipZhekouRimg;

	// Token: 0x040007C6 RID: 1990
	private Text tipZhekouNum;

	// Token: 0x040007C7 RID: 1991
	private int comTipState;

	// Token: 0x040007C8 RID: 1992
	private GameObject rewardTipObj;

	// Token: 0x040007C9 RID: 1993
	private RawImage rewardTipBg;

	// Token: 0x040007CA RID: 1994
	private GameObject rewardHead;

	// Token: 0x040007CB RID: 1995
	private RawImage rewardHeadIcon;

	// Token: 0x040007CC RID: 1996
	private Text rewardTipTitle;

	// Token: 0x040007CD RID: 1997
	private RawImage rewardTipIcon;

	// Token: 0x040007CE RID: 1998
	private Image rewardTipQual;

	// Token: 0x040007CF RID: 1999
	private Text rewardTipDesc;

	// Token: 0x040007D0 RID: 2000
	private GameObject rewardTipBtn;

	// Token: 0x040007D1 RID: 2001
	private GameObject itemTipObj;

	// Token: 0x040007D2 RID: 2002
	private RawImage itemTipBg;

	// Token: 0x040007D3 RID: 2003
	private Text itemTitleTxt;

	// Token: 0x040007D4 RID: 2004
	private Text itemContentTxt;

	// Token: 0x040007D5 RID: 2005
	private GameObject itemGrid;

	// Token: 0x040007D6 RID: 2006
	private List<GameObject> szItemGrid = new List<GameObject>();

	// Token: 0x040007D7 RID: 2007
	private GameObject itemOkBtn;

	// Token: 0x040007D8 RID: 2008
	private Text itemOkTxt;

	// Token: 0x040007D9 RID: 2009
	private GameObject inputTipObj;

	// Token: 0x040007DA RID: 2010
	private RawImage inputTipBg;

	// Token: 0x040007DB RID: 2011
	private Text inputTitle;

	// Token: 0x040007DC RID: 2012
	private InputField inputField;

	// Token: 0x040007DD RID: 2013
	private GameObject inputOkBtn;

	// Token: 0x040007DE RID: 2014
	private Text inputOkTxt;

	// Token: 0x040007DF RID: 2015
	private GameObject inputCancelBtn;

	// Token: 0x040007E0 RID: 2016
	private Text inputCancelTxt;

	// Token: 0x040007E1 RID: 2017
	private easy_tipCon.TipOKDelegate okFunc;

	// Token: 0x040007E2 RID: 2018
	private object okParam;

	// Token: 0x040007E3 RID: 2019
	private easy_tipCon.TipCancelDelegate cancelFunc;

	// Token: 0x040007E4 RID: 2020
	private object cancelParam;

	// Token: 0x040007E5 RID: 2021
	private easy_tipCon.TipToggleDelegate toggleFunc;

	// Token: 0x040007E6 RID: 2022
	private object toggleParam;

	// Token: 0x040007E7 RID: 2023
	private easy_tipCon.TipInputDelegate inputFunc;

	// Token: 0x040007E8 RID: 2024
	private object inputParam;

	// Token: 0x040007E9 RID: 2025
	private float delayTime = 2f;

	// Token: 0x040007EA RID: 2026
	private int nZhTipId;

	// Token: 0x040007EB RID: 2027
	private GameObject unlockTipObj;

	// Token: 0x040007EC RID: 2028
	private RawImage unlockTipBg;

	// Token: 0x040007ED RID: 2029
	private Text unlockTitleTxt;

	// Token: 0x040007EE RID: 2030
	private Text unlockContentTxt;

	// Token: 0x040007EF RID: 2031
	private Text unlockMsgTxt;

	// Token: 0x040007F0 RID: 2032
	private GameObject zsUnlockBtn;

	// Token: 0x040007F1 RID: 2033
	private Text zsUnlockTxt;

	// Token: 0x040007F2 RID: 2034
	private Image zsHeroReturnSp;

	// Token: 0x040007F3 RID: 2035
	private GameObject jbUnlockBtn;

	// Token: 0x040007F4 RID: 2036
	private Text jbUnlockTxt;

	// Token: 0x040007F5 RID: 2037
	private GameObject unlockCancelBtn;

	// Token: 0x040007F6 RID: 2038
	private Text unlockWarnTxt;

	// Token: 0x040007F7 RID: 2039
	private Text unlockNotice;

	// Token: 0x040007F8 RID: 2040
	private Image unlockDiscountSp;

	// Token: 0x040007F9 RID: 2041
	private Text unlockDiscountTxt;

	// Token: 0x040007FA RID: 2042
	private Image unlockDiscountXian;

	// Token: 0x040007FB RID: 2043
	private Toggle unlockToggle;

	// Token: 0x040007FC RID: 2044
	private Text unlockToggleTxt;

	// Token: 0x040007FD RID: 2045
	private GameObject unlockZhekouObj;

	// Token: 0x040007FE RID: 2046
	private RawImage unlockZhekouRimg;

	// Token: 0x040007FF RID: 2047
	private Text unlockZhekouNum;

	// Token: 0x04000800 RID: 2048
	private int unlockChargeVal;

	// Token: 0x04000801 RID: 2049
	private easy_tipCon.UnlockADelegate unlockAFunc;

	// Token: 0x04000802 RID: 2050
	private object unlockAParam;

	// Token: 0x04000803 RID: 2051
	private easy_tipCon.UnlockBDelegate unlockBFunc;

	// Token: 0x04000804 RID: 2052
	private object unlockBParam;

	// Token: 0x04000805 RID: 2053
	private GameObject relinkTipObj;

	// Token: 0x04000806 RID: 2054
	private RawImage relinkTipBg;

	// Token: 0x04000807 RID: 2055
	private Text relinkTipTitle;

	// Token: 0x04000808 RID: 2056
	private Text relinkTipContent;

	// Token: 0x04000809 RID: 2057
	private Text relinkYesTxt;

	// Token: 0x0400080A RID: 2058
	private Text relinkNoTxt;

	// Token: 0x02000552 RID: 1362
	// (Invoke) Token: 0x060033A7 RID: 13223
	public delegate void TipOKDelegate(object szParam);

	// Token: 0x02000553 RID: 1363
	// (Invoke) Token: 0x060033AB RID: 13227
	public delegate void TipCancelDelegate(object szParam);

	// Token: 0x02000554 RID: 1364
	// (Invoke) Token: 0x060033AF RID: 13231
	public delegate void TipToggleDelegate(object szParam, bool bstate);

	// Token: 0x02000555 RID: 1365
	// (Invoke) Token: 0x060033B3 RID: 13235
	public delegate void TipInputDelegate(object szParam, string sinput);

	// Token: 0x02000556 RID: 1366
	// (Invoke) Token: 0x060033B7 RID: 13239
	public delegate void UnlockADelegate(object szParam, bool ischk);

	// Token: 0x02000557 RID: 1367
	// (Invoke) Token: 0x060033BB RID: 13243
	public delegate void UnlockBDelegate(object szParam, bool ischk);
}
