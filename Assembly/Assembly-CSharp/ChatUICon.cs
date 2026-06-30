using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public class ChatUICon : Window
{
	// Token: 0x06000491 RID: 1169 RVA: 0x00024C10 File Offset: 0x00022E10
	protected override void Awake()
	{
		base.Awake();
		ChatUICon.inst = this;
		this.pChatTip = this.UIGameObj.Get_GameObj(0);
		this.UI_NewChat_SJShow = this.UIGameObj.Get_GameObj(1);
		this.pInputField = this.UIGameObj.Get_GameObj(8);
		this.InputText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.ChatPinDao = this.UIGameObj.Get_GameObj(10);
		this.OneMsgText = this.UIGameObj.Get_GameObj(11);
		this.pEnter = this.UIGameObj.Get_GameObj(12);
		this.UI_NewChat_SJContent = this.UIGameObj.Get_GameObj(13);
		this.pChatEnter = this.UIGameObj.Get_GameObj(20);
		EventTriggerListener.Get(this.pEnter.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSendMsgClick);
		this.pInputName = this.pInputField.GetComponent<InputField>();
		this.pInputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdits));
		this.OneMsgText.SetActive(false);
		this.pLaBaListScn = this.UIGameObj.Get_GameObj(21);
		this.pOneLaBaMsgScn = this.UIGameObj.Get_GameObj(22);
		this.pLaBaListHall = this.UIGameObj.Get_GameObj(28);
		this.pOneLaBaMsgHall = this.UIGameObj.Get_GameObj(29);
		this.sPbar = this.UI_NewChat_SJShow.transform.FindChild("Scrollbar Vertical").GetComponent<Scrollbar>();
		ScrollRect component = this.UI_NewChat_SJShow.GetComponent<ScrollRect>();
		this.sPbar.onValueChanged.AddListener(new UnityAction<float>(this.OnBarOnChange));
		component.scrollSensitivity = 50f;
		component.elasticity = 0f;
		EventTriggerListener.Get(this.pChatTip).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOpen);
		this.pOnePushMsg = this.UIGameObj.Get_GameObj(24);
		this.pOnePushMsg.SetActive(false);
		this.pLaBaBtn = this.UIGameObj.Get_GameObj(25);
		this.pLaBaBtn.SetActive(false);
		EventTriggerListener.Get(this.pLaBaBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeLaBa);
		this.pLaBaMoney = this.UIGameObj.Get_GameObj(30);
		this.TLaBaMoney = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.pLaBaMoney.SetActive(false);
		this.pWorldMoney = this.UIGameObj.Get_GameObj(26);
		this.TWorldMoney = this.UIGameObj.Get_GameObj(27).GetComponent<Text>();
		this.pWorldMoney.SetActive(false);
		this.pLaBaListScn.SetActive(true);
		this.pOneLaBaMsgScn.SetActive(false);
		this.pLaBaListHall.SetActive(true);
		this.pOneLaBaMsgHall.SetActive(false);
		EventTriggerListener.Get(this.ChatPinDao.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangePinDaoClick);
		this.InItData();
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00024F2A File Offset: 0x0002312A
	private void OnClickOpen(GameObject obj)
	{
		ChatManager.GetInstance().OnPenChatUI();
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00024F38 File Offset: 0x00023138
	private void OnBarOnChange(float dd)
	{
		bool flag = this.isOpenChat && !ChatManager.ChatInput && base.IsInvoking("WaitHideChat");
		if (flag)
		{
			base.CancelInvoke("WaitHideChat");
			base.Invoke("WaitHideChat", 5f);
		}
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00024F88 File Offset: 0x00023188
	private void OnEndEdits(string stext)
	{
		ChatManager.ChatInput = false;
		bool flag = this.isOpenChat;
		if (flag)
		{
			base.CancelInvoke("WaitHideChat");
			base.Invoke("WaitHideChat", 5f);
		}
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00024FC8 File Offset: 0x000231C8
	private void InItData()
	{
		this.CheckPinDaoPanel(1);
		this.m_nPinDao = 0;
		this.OnCloseChatUI();
		this.SetPinDaoTypeMsg(this.GetChatType(this.m_nPinDao));
		this.pInputName.characterLimit = 100;
		this.TLaBaMoney.text = ChatManager.m_nLaBaMoney.ToString();
		this.TWorldMoney.text = ChatManager.m_nWorldMoney.ToString();
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0002503C File Offset: 0x0002323C
	public void SetPinDaoTypeMsg(string sMsg)
	{
		bool activeSelf = base.gameObject.activeSelf;
		if (activeSelf)
		{
			this.ChatPinDao.GetComponent<Text>().text = sMsg;
			base.StartCoroutine(this.UdpatePos());
			switch (this.m_nPinDao)
			{
			case 1:
				this.InputText.color = Fun.StrToColor(ChatColor.shijie, 1f);
				break;
			case 2:
				this.InputText.color = Fun.StrToColor(ChatColor.xiaodui, 1f);
				break;
			case 3:
				this.InputText.color = Fun.StrToColor(ChatColor.duiwu, 1f);
				break;
			case 4:
				this.InputText.color = Fun.StrToColor(ChatColor.bisai, 1f);
				break;
			case 5:
				this.InputText.color = Fun.StrToColor(ChatColor.siliao, 1f);
				break;
			case 6:
				this.InputText.color = Fun.StrToColor(ChatColor.room, 1f);
				break;
			case 7:
				this.InputText.color = Fun.StrToColor(ChatColor.guild, 1f);
				break;
			case 8:
				this.InputText.color = Fun.StrToColor(ChatColor.system, 1f);
				break;
			case 10:
				this.InputText.color = Fun.StrToColor(ChatColor.laba, 1f);
				break;
			}
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000251CE File Offset: 0x000233CE
	public IEnumerator UdpatePos()
	{
		yield return new WaitForSeconds(0.005f);
		RectTransform rec = this.ChatPinDao.GetComponent<RectTransform>();
		float nWidth = rec.sizeDelta.x;
		bool flag = nWidth == 0f;
		if (flag)
		{
			nWidth = 80f;
		}
		rec.anchoredPosition3D = new Vector3(80f + nWidth / 2f, -30f, 0f);
		RectTransform FieldRec = this.pInputField.GetComponent<RectTransform>();
		FieldRec.anchoredPosition3D = new Vector3(258f + nWidth / 2f, -26f, 0f);
		FieldRec.sizeDelta = new Vector2(328f - nWidth, 24f);
		yield break;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000251E0 File Offset: 0x000233E0
	private void Start()
	{
		GameObject gameObject = this.pInputField.transform.FindChild("InputField Input Caret").gameObject;
		ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39017, new object[]
		{
			ChatManager.m_nWorldMoney
		}), "ffdd00", "system");
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0002523C File Offset: 0x0002343C
	private void Update()
	{
		bool flag = fGameManager.m_nUserType != 1;
		if (!flag)
		{
			bool flag2 = BroadCastMSGCon.inst != null;
			if (flag2)
			{
				bool fadeOut = BroadCastMSGCon.inst.fadeOut;
				if (fadeOut)
				{
					this.OnSetLaBaoPos(true);
				}
				else
				{
					this.OnSetLaBaoPos(false);
				}
			}
			else
			{
				this.OnSetLaBaoPos(true);
			}
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x000252A0 File Offset: 0x000234A0
	public void onGetFocus()
	{
		bool flag = this.isOpenChat;
		if (flag)
		{
			this.pInputName.ActivateInputField();
			ChatManager.ChatInput = true;
			base.CancelInvoke("WaitHideChat");
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000252D8 File Offset: 0x000234D8
	public void OnOpenChat()
	{
		this.pChatTip.SetActive(!this.pChatTip.activeSelf);
		this.pChatEnter.SetActive(!this.pChatEnter.activeSelf);
		this.isOpenChat = this.pChatEnter.activeSelf;
		this.onGetFocus();
		this.UI_NewChat_SJShow.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(176f, 88f, 0f);
		this.UI_NewChat_SJShow.GetComponent<RectTransform>().sizeDelta = new Vector2(359f, 183f);
		bool flag = this.sPbar != null;
		if (flag)
		{
			this.sPbar.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(6f, 192f);
		}
		this.OnGoToLaBa();
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000253B8 File Offset: 0x000235B8
	public void OnCloseChatUI()
	{
		bool activeSelf = base.gameObject.activeSelf;
		if (activeSelf)
		{
			this.pChatTip.SetActive(true);
			this.pChatEnter.SetActive(false);
			this.isOpenChat = false;
			ChatManager.ChatInput = false;
			this.UI_NewChat_SJShow.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(176f, 38f, 0f);
			this.UI_NewChat_SJShow.GetComponent<RectTransform>().sizeDelta = new Vector2(359f, 80f);
			bool flag = this.sPbar != null;
			if (flag)
			{
				this.sPbar.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(6f, 0f);
				this.UpdateScollValue();
			}
			this.OnGoToLaBa();
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0002548C File Offset: 0x0002368C
	private void OnSetLaBaoPos(bool isOpen)
	{
		bool flag = isOpen != this.LaBaIsUp;
		if (flag)
		{
			this.LaBaIsUp = isOpen;
			if (isOpen)
			{
				this.pLaBaListScn.GetComponent<RectTransform>().anchoredPosition = new Vector3(479f, 668f, 0f);
			}
			else
			{
				this.pLaBaListScn.GetComponent<RectTransform>().anchoredPosition = new Vector3(479f, 633f, 0f);
			}
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00025512 File Offset: 0x00023712
	public void UpdateScrollBarPos(GameObject pScrollBar, Vector3 szpos)
	{
		pScrollBar.GetComponent<RectTransform>().anchoredPosition3D = szpos;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00025524 File Offset: 0x00023724
	public void SendPinDaoMsg(int nPd, int nPdmsgnum, string sPname, string sText, string sLink = "", int nSiLiaoType = 2, ArrayList szLanZuanInfo = null, int nCid = 0)
	{
		string str = string.Empty;
		this.DelMsgList();
		bool flag = this.sPbar != null && this.sPbar.value != 1f;
		if (flag)
		{
			this.nOldParValue = this.sPbar.value;
		}
		GameObject nowPDContent = this.GetNowPDContent(nPd);
		GameObject nowPD = this.GetNowPD(nPd);
		bool flag2 = nowPD != null && base.gameObject.activeSelf && this.sPbar != null;
		if (flag2)
		{
			base.StopCoroutine("WaitUpdateScrollBarSize");
			base.StartCoroutine(this.WaitUpdateScrollBarSize(this.nOldParValue));
		}
		bool flag3 = this.OneMsgText == null;
		if (!flag3)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneMsgText);
			gameObject.name = sPname + nPdmsgnum.ToString();
			bool flag4 = nPd == 5 && sLink != string.Empty;
			if (flag4)
			{
				this.sLastMiYuUser = sLink;
				this.OnSaveSiLiaoList(sLink);
			}
			bool flag5 = nPd != 9;
			if (flag5)
			{
				gameObject.transform.SetParent(nowPDContent.transform);
			}
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			LinkImageText component = gameObject.GetComponent<LinkImageText>();
			bool flag6 = nPd != 8 && szLanZuanInfo.Count > 1 && nPd != 5 && szLanZuanInfo[2].ToString() == Fun.GetQQ_Zuan_Type(null);
			if (flag6)
			{
				string text = szLanZuanInfo[0].ToString();
				bool flag7 = !string.IsNullOrEmpty(text);
				if (flag7)
				{
					GameObject gameObject2 = gameObject.transform.FindChild("buleImg").gameObject;
					gameObject2.transform.localPosition = new Vector3(60f, -10f, 0f);
					str = "\u3000";
					bool flag8 = nPd == 5 && nSiLiaoType == 2;
					if (flag8)
					{
						gameObject2.transform.localPosition = new Vector3(27f, -12f, 0f);
						str = string.Empty;
					}
					gameObject2.SetActive(true);
					gameObject2.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
				}
				text = szLanZuanInfo[1].ToString();
				bool flag9 = !string.IsNullOrEmpty(text);
				if (flag9)
				{
					GameObject gameObject3 = gameObject.transform.FindChild("buleYearImg").gameObject;
					gameObject3.transform.localPosition = new Vector3(79f, -10f, 0f);
					str += "\u3000";
					bool flag10 = nPd == 5 && nSiLiaoType == 2;
					if (flag10)
					{
						gameObject3.transform.localPosition = new Vector3(46f, -12f, 0f);
						str = string.Empty;
					}
					gameObject3.SetActive(true);
					gameObject3.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
				}
			}
			sText = str + sText;
			sText = this.GetChatTypeMsg(nPd, sPname) + sText;
			component.m_PreText = Language.GetUTFCodeStr(sText);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			this.CreateLink(component, sLink, nCid);
			this.AddMsgToList(nPd, gameObject);
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x000258A3 File Offset: 0x00023AA3
	private IEnumerator WaitUpdateScrollBarSize(float nOldValue)
	{
		yield return new WaitForSeconds(0.3f);
		bool flag = this.sPbar.size < 0.1f;
		if (flag)
		{
			this.sPbar.size = 0.1f;
		}
		bool flag2 = nOldValue <= 0.01f;
		if (flag2)
		{
			this.sPbar.value = 0f;
		}
		yield break;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000258B9 File Offset: 0x00023AB9
	public void UpdateScollValue()
	{
		base.StopCoroutine("WaitUpdateScrollBarSize");
		base.StartCoroutine(this.WaitUpdateScrollBarSize(0f));
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000258DA File Offset: 0x00023ADA
	private void WaitHideChat()
	{
		this.OnCloseChatUI();
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000258E4 File Offset: 0x00023AE4
	private void DelMsgList()
	{
		bool flag = this.m_nCurRow >= this.m_nMaxRow;
		if (flag)
		{
			GameObject gameObject = (GameObject)this.AllShiJieMsg[0];
			bool flag2 = gameObject == null;
			if (flag2)
			{
				this.AllShiJieMsg.RemoveAt(0);
				return;
			}
			float y = gameObject.GetComponent<RectTransform>().sizeDelta.y;
			int rowByHeight = this.GetRowByHeight(y);
			this.m_nCurRow -= rowByHeight;
			this.AllShiJieMsg.RemoveAt(0);
			bool flag3 = gameObject != null;
			if (flag3)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
		bool flag4 = this.AllShiJieMsg.Count > this.m_nMaxNum;
		if (flag4)
		{
			GameObject gameObject = (GameObject)this.AllShiJieMsg[0];
			this.AllShiJieMsg.RemoveAt(0);
			bool flag5 = gameObject != null;
			if (flag5)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000259D7 File Offset: 0x00023BD7
	private void AddMsgToList(int npd, GameObject obj)
	{
		this.AllShiJieMsg.Add(obj);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x000259E7 File Offset: 0x00023BE7
	private void FixedUpdate()
	{
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000259EC File Offset: 0x00023BEC
	private GameObject GetNowPD(int nPinDao)
	{
		return this.UI_NewChat_SJShow;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00025A04 File Offset: 0x00023C04
	private GameObject GetNowPDContent(int nPinDao)
	{
		return this.UI_NewChat_SJContent;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00025A1C File Offset: 0x00023C1C
	private void CheckPinDaoPanel(int nPindao)
	{
		bool flag = nPindao == 1;
		if (flag)
		{
			bool flag2 = this.pNowPinDaoObj != null;
			if (flag2)
			{
				this.pNowPinDaoObj.SetActive(false);
			}
			this.pNowPinDaoObj = this.GetNowPD(nPindao);
			this.pNowPinDaoObj.SetActive(true);
		}
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00025A70 File Offset: 0x00023C70
	public void OnChange()
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (flag)
		{
			bool flag2 = SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
			if (flag2)
			{
				this.UserChat = this.SelectHeroChat;
			}
			else
			{
				this.UserChat = this.ScnChat;
			}
			bool flag3 = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
			if (flag3)
			{
				FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
				int num = this.UserChat.IndexOf(3);
				bool flag4 = fbXmlData.nType == 11;
				if (flag4)
				{
					bool flag5 = num != -1;
					if (flag5)
					{
						this.UserChat.RemoveAt(num);
					}
				}
				else
				{
					bool flag6 = num == -1;
					if (flag6)
					{
						this.UserChat.Insert(0, 3);
					}
				}
			}
		}
		else
		{
			bool flag7 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag7)
			{
				this.UserChat = this.RoomChat;
			}
			else
			{
				this.UserChat = this.HallChat;
			}
		}
		this.ChangePinDao(true);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00025BAC File Offset: 0x00023DAC
	private void OnChangePinDaoClick(object obj)
	{
		bool flag = base.IsInvoking("WaitHideChat");
		if (flag)
		{
			base.CancelInvoke("WaitHideChat");
			base.Invoke("WaitHideChat", 5f);
		}
		this.ChangePinDao(false);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00025BF0 File Offset: 0x00023DF0
	public void ChangePinDao(bool isChangeUserType = false)
	{
		int num = this.UserChat.IndexOf(2);
		bool flag = TeamManager.GetInstance().m_nTeamID == 0;
		if (flag)
		{
			bool flag2 = num != -1;
			if (flag2)
			{
				this.UserChat.RemoveAt(num);
			}
		}
		else
		{
			bool flag3 = num == -1;
			if (flag3)
			{
				this.UserChat.Insert(1, 2);
			}
		}
		num = this.UserChat.IndexOf(7);
		bool flag4 = GuildManager.GetInstance().m_nGuildId == 0;
		if (flag4)
		{
			bool flag5 = num != -1;
			if (flag5)
			{
				this.UserChat.RemoveAt(num);
			}
		}
		else
		{
			bool flag6 = num == -1;
			if (flag6)
			{
				this.UserChat.Insert(1, 7);
			}
		}
		bool flag7 = this.m_nPinDao == 5 && this.CheckSiLiaoUser();
		if (flag7)
		{
			this.SetPinDaoTypeMsg(string.Concat(new string[]
			{
				"<color=#",
				ChatColor.siliao,
				">",
				this.m_sSelectUserName,
				": </color>"
			}));
		}
		else
		{
			int num2 = this.UserChat.IndexOf(this.m_nPinDao);
			bool flag8 = num2 == -1;
			if (flag8)
			{
				num2 = 0;
			}
			else
			{
				bool flag9 = !isChangeUserType;
				if (flag9)
				{
					num2 = ((num2 < this.UserChat.Count - 1) ? (num2 + 1) : 0);
				}
			}
			this.m_nPinDao = (int)this.UserChat[num2];
			this.m_sSelectUserName = string.Empty;
			this.OnGoToLaBa();
			bool flag10 = this.m_nPinDao == 5;
			if (flag10)
			{
				this.ChangePinDao(false);
			}
			else
			{
				this.SetPinDaoTypeMsg(this.GetChatType(this.m_nPinDao));
			}
		}
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00025DC0 File Offset: 0x00023FC0
	private bool CheckSiLiaoUser()
	{
		bool flag = this.m_szSiLiaoList.Count == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.m_sSelectUserName == string.Empty;
			if (flag2)
			{
				this.m_sSelectUserName = this.m_szSiLiaoList[0].ToString();
			}
			else
			{
				int num = this.m_szSiLiaoList.IndexOf(this.m_sSelectUserName);
				bool flag3 = num == this.m_szSiLiaoList.Count - 1;
				if (flag3)
				{
					return false;
				}
				this.m_sSelectUserName = this.m_szSiLiaoList[num + 1].ToString();
			}
			result = true;
		}
		return result;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00025E64 File Offset: 0x00024064
	public bool OnMiYuOpen()
	{
		bool isChangeGong = GuildManager.isChangeGong;
		bool result;
		if (isChangeGong)
		{
			result = false;
		}
		else
		{
			bool flag = this.isOpenChat;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.sLastMiYuUser == string.Empty;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.AddUserSiLiaoList(this.sLastMiYuUser);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00025EBA File Offset: 0x000240BA
	private void OnSendMsgClick(GameObject obj)
	{
		this.Chat_SendMsgToServer();
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00025EC4 File Offset: 0x000240C4
	public void AddUserSiLiaoList(string sName)
	{
		bool flag = sName == PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39018, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = ChatManager.GetInstance().isPingBiUser(sName);
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39029, new object[0]), "ffdd00", "system");
			}
			else
			{
				this.m_nPinDao = 5;
				this.m_sSelectUserName = sName;
				this.SetPinDaoTypeMsg(string.Concat(new string[]
				{
					"<color=#",
					ChatColor.siliao,
					">",
					this.m_sSelectUserName,
					": </color>"
				}));
				bool flag3 = this.isOpenChat;
				if (flag3)
				{
					this.onGetFocus();
					this.OnGoToLaBa();
				}
				else
				{
					this.OnOpenChat();
				}
			}
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00025FBC File Offset: 0x000241BC
	private void OnSaveSiLiaoList(string sName)
	{
		bool flag = this.m_szSiLiaoList.Count == 10;
		if (flag)
		{
			this.m_szSiLiaoList.RemoveAt(0);
		}
		bool flag2 = this.m_szSiLiaoList.IndexOf(sName) == -1;
		if (flag2)
		{
			this.m_szSiLiaoList.Add(sName);
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00026010 File Offset: 0x00024210
	public void OnDeleteSiLiaoList(string sName)
	{
		int num = this.m_szSiLiaoList.IndexOf(sName);
		bool flag = num != -1;
		if (flag)
		{
			this.m_szSiLiaoList.RemoveAt(num);
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00026048 File Offset: 0x00024248
	private string GetChatType(int nPinDao)
	{
		string result = string.Empty;
		switch (nPinDao)
		{
		case 1:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.shijie,
				">[",
				Language.GetLang(39019, new object[0]),
				"] </color>"
			});
			break;
		case 2:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.xiaodui,
				">[",
				Language.GetLang(39020, new object[0]),
				"] </color>"
			});
			break;
		case 3:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.duiwu,
				">[",
				Language.GetLang(39021, new object[0]),
				"] </color>"
			});
			break;
		case 4:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.bisai,
				">[",
				Language.GetLang(39022, new object[0]),
				"] </color>"
			});
			break;
		case 5:
			result = "[] ";
			break;
		case 6:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.room,
				">[",
				Language.GetLang(39022, new object[0]),
				"] </color>"
			});
			break;
		case 7:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.guild,
				">[",
				Language.GetLang(39023, new object[0]),
				"] </color>"
			});
			break;
		case 8:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.system,
				">[",
				Language.GetLang(39024, new object[0]),
				"] </color>"
			});
			break;
		case 10:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.laba,
				">[",
				Language.GetLang(39025, new object[0]),
				"] </color>"
			});
			break;
		}
		return result;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x000262C0 File Offset: 0x000244C0
	private string GetChatTypeMsg(int nPinDao, string sTitle = "")
	{
		string result = string.Empty;
		switch (nPinDao)
		{
		case 1:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.shijie,
				">[",
				Language.GetLang(39019, new object[0]),
				"] </color>"
			});
			break;
		case 2:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.xiaodui,
				">[",
				Language.GetLang(39020, new object[0]),
				"] </color>"
			});
			break;
		case 3:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.duiwu,
				">[",
				Language.GetLang(39021, new object[0]),
				"] </color>"
			});
			break;
		case 4:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.bisai,
				">[",
				Language.GetLang(39022, new object[0]),
				"] </color>"
			});
			break;
		case 5:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.siliao,
				">[",
				Language.GetLang(39030, new object[0]),
				"] </color>"
			});
			break;
		case 6:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.room,
				">[",
				Language.GetLang(39022, new object[0]),
				"] </color>"
			});
			break;
		case 7:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.guild,
				">[",
				Language.GetLang(39023, new object[0]),
				"] </color>"
			});
			break;
		case 8:
			result = ((!(sTitle == "choujiang")) ? string.Concat(new string[]
			{
				"<color=#",
				ChatColor.system,
				">[",
				Language.GetLang(39024, new object[0]),
				"] </color>"
			}) : string.Concat(new string[]
			{
				"<color=#",
				ChatColor.system,
				">[",
				Language.GetLang(28010, new object[0]),
				"] </color>"
			}));
			break;
		case 10:
			result = string.Concat(new string[]
			{
				"<color=#",
				ChatColor.laba,
				">[",
				Language.GetLang(39025, new object[0]),
				"] </color>"
			});
			break;
		}
		return result;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x000265C0 File Offset: 0x000247C0
	public void Chat_SendMsgToServer()
	{
		string text = this.Filte_chatMsg = this.pInputName.text;
		bool flag = fUIManager.mProgress != null && fUIManager.mProgress.activeSelf;
		if (!flag)
		{
			bool flag2 = text == string.Empty;
			if (flag2)
			{
				this.OnCloseChatUI();
			}
			else
			{
				bool flag3 = this.m_nPinDao == 10 && fGameManager.IsSingle;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39033, new object[0]), "ffdd00", "system");
				}
				else
				{
					text = Fun.CheckChatSpecialStr(text);
					bool flag4 = WebParam.client_language == "zh";
					if (flag4)
					{
						fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 1, 0);
					}
					else
					{
						text = Fun.GetShieldTxt(text);
						bool flag5 = this.m_nPinDao == 10 && text.Length > 34;
						if (flag5)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39034, new object[0]), "ffdd00", "system");
						}
						else
						{
							bool flag6 = this.m_nPinDao == 1 && !this.IsCheckChatMoney;
							if (flag6)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39027, new object[]
								{
									ChatManager.m_nWorldMoney
								}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnWorldChat), text, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
							}
							else
							{
								bool flag7 = this.m_nPinDao == 10 && !this.IsCheckLaBaChatMoney;
								if (flag7)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39031, new object[]
									{
										ChatManager.m_sLaBaMoney
									}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnWorldChat), text, new TipManager.TipCancelDelegate(this.OnLaBaCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipLaBaToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
								}
								else
								{
									this.OnWorldChat(text);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00026894 File Offset: 0x00024A94
	private void OnCheckBack(int ishave, string sMsg)
	{
		bool flag = this.m_nPinDao == 10 && sMsg.Length > 34;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39034, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = this.m_nPinDao == 1 && !this.IsCheckChatMoney;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39027, new object[]
				{
					ChatManager.m_nWorldMoney
				}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnWorldChat), sMsg, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
			}
			else
			{
				bool flag3 = this.m_nPinDao == 10 && !this.IsCheckLaBaChatMoney;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(39031, new object[]
					{
						ChatManager.m_sLaBaMoney
					}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnWorldChat), sMsg, new TipManager.TipCancelDelegate(this.OnLaBaCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipLaBaToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
				}
				else
				{
					this.OnWorldChat(sMsg);
				}
			}
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00026A88 File Offset: 0x00024C88
	private void OnWorldChat(object obj)
	{
		bool flag = this.m_nPinDao == 1 && ChatManager.m_nWorldMoney > PlayerDataManager.getInstance().playerdet.gameCurrency;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39016, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = this.m_nPinDao == 10 && ChatManager.m_nLaBaMoney > PlayerDataManager.getInstance().playerdet.chargeCurrency;
			if (flag2)
			{
				fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
			}
			else
			{
				string text = obj.ToString();
				text = text.Replace("\r\n", string.Empty);
				ChatManager.GetInstance().Chat_Client_To_ServerNetMsg(this.m_nPinDao, new ArrayList
				{
					this.m_nPinDao,
					Fun.EncodingUTF8(text),
					Fun.EncodingUTF8(this.m_sSelectUserName),
					ChatManager.GetInstance().GetUserChatParam()
				});
				this.pInputName.text = string.Empty;
				this.OnCloseChatUI();
				bool flag3 = !string.IsNullOrEmpty(this.Filte_chatMsg);
				if (flag3)
				{
					int num = Fun.HaveFilterTxt(this.Filte_chatMsg, 0);
					bool flag4 = num < 0;
					if (flag4)
					{
						this.Filte_chatMsg = string.Empty;
					}
					else
					{
						this.Filte_chatMsg = Fun.String2Unicode(this.Filte_chatMsg);
						base.StartCoroutine(Fun.SendHttpRequest_Post_NoRequest(SysConfig.getWebURL(string.Concat(new string[]
						{
							"/qqapi/logmsg.ashx?uid=",
							WebParam.client_account,
							"&cid=",
							WebParam.client_cid,
							"&unick=",
							PlayerDataManager.getInstance().HUserData.m_sName,
							"&pindao=",
							this.m_nPinDao.ToString(),
							"&selu=",
							this.m_sSelectUserName,
							"&msg=",
							this.Filte_chatMsg
						}))));
						this.Filte_chatMsg = string.Empty;
					}
				}
			}
		}
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00026C9B File Offset: 0x00024E9B
	private void OnTipToggle(bool bstate)
	{
		this.IsCheckChatMoney = bstate;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00026CA5 File Offset: 0x00024EA5
	private void OnCancel(object obj)
	{
		this.IsCheckChatMoney = false;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00026CAF File Offset: 0x00024EAF
	private void OnTipLaBaToggle(bool bstate)
	{
		this.IsCheckLaBaChatMoney = bstate;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00026CB9 File Offset: 0x00024EB9
	private void OnLaBaCancel(object obj)
	{
		this.IsCheckLaBaChatMoney = false;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00026CC4 File Offset: 0x00024EC4
	public void CreateLink(LinkImageText text, string LinkUrl = "", int nCid = 0)
	{
		bool flag = !(text == null);
		if (flag)
		{
			text.onHrefClick.AddListener(new UnityAction<string>(this.OnHrefClick));
			Button button = text.gameObject.AddComponent<Button>();
			ChatLink chatLink = text.gameObject.AddComponent<ChatLink>();
			chatLink.Init(LinkUrl, nCid);
			EventTriggerListener.Get(button.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.onButtonDown);
			EventTriggerListener.Get(button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.onButtonClick);
			bool activeSelf = base.gameObject.activeSelf;
			if (activeSelf)
			{
				base.StartCoroutine(this.WaitGetRow(text));
			}
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00026D75 File Offset: 0x00024F75
	private IEnumerator WaitGetRow(LinkImageText text)
	{
		yield return new WaitForSeconds(0.01f);
		bool flag = !(text == null);
		if (flag)
		{
			float nH = text.gameObject.GetComponent<RectTransform>().sizeDelta.y;
			int nRow = this.GetRowByHeight(nH);
			this.m_nCurRow += nRow;
		}
		yield break;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00026D8C File Offset: 0x00024F8C
	private int GetRowByHeight(float fHeight)
	{
		int num = (int)fHeight / 22;
		bool flag = num == 0;
		if (flag)
		{
			num = 1;
		}
		return num;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00026DB4 File Offset: 0x00024FB4
	private void OnHrefClick(string hrefName)
	{
		bool flag = hrefName.StartsWith("openui:");
		if (flag)
		{
			string[] array = hrefName.Split(new char[]
			{
				':'
			});
			this.OpenUIByName(array[1]);
		}
		else
		{
			Debuger.OpenURL(hrefName);
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00026DFA File Offset: 0x00024FFA
	private void OnHrefEnter(string hrefName)
	{
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00026DFD File Offset: 0x00024FFD
	private void OnHrefExit(string hrefName)
	{
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00026E00 File Offset: 0x00025000
	private void onButtonDown(GameObject obj)
	{
		ChatLink component = obj.GetComponent<ChatLink>();
		bool flag = !(component.sUrl == string.Empty) && Input.GetMouseButtonDown(1);
		if (flag)
		{
			ChatManager.GetInstance().OnMsgTip(component.sUrl, 1, null, component.m_nCid);
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00026E50 File Offset: 0x00025050
	private void onButtonClick(GameObject obj)
	{
		ChatLink component = obj.GetComponent<ChatLink>();
		bool flag = !(component.sUrl == string.Empty) && !Input.GetMouseButtonUp(1);
		if (flag)
		{
			this.AddUserSiLiaoList(component.sUrl);
		}
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00026E98 File Offset: 0x00025098
	private void OnChangeLaBa(GameObject obj)
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(39033, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag = this.isOpenChat;
			if (flag)
			{
				this.onGetFocus();
			}
			else
			{
				this.OnOpenChat();
			}
			bool flag2 = this.m_nPinDao != 10;
			if (flag2)
			{
				this.m_nPinDao = 10;
				this.ChangePinDao(true);
			}
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00026F18 File Offset: 0x00025118
	private void OnGoToLaBa()
	{
		bool flag = this.m_nPinDao == 10;
		bool flag2 = this.m_nPinDao == 1;
		bool flag3 = flag;
		if (flag3)
		{
			this.pInputName.characterLimit = 34;
		}
		else
		{
			this.pInputName.characterLimit = 100;
		}
		bool flag4 = !this.isOpenChat;
		if (flag4)
		{
			flag = false;
			flag2 = false;
		}
		bool flag5 = flag2 || flag;
		if (flag5)
		{
			bool flag6 = flag2;
			if (flag6)
			{
				this.pWorldMoney.SetActive(true);
				this.pLaBaMoney.SetActive(false);
			}
			else
			{
				this.pWorldMoney.SetActive(false);
				this.pLaBaMoney.SetActive(true);
			}
		}
		else
		{
			this.pWorldMoney.SetActive(false);
			this.pLaBaMoney.SetActive(false);
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00026FE0 File Offset: 0x000251E0
	public void OnLaBaChat(string sNick, string sMsg, ArrayList szData)
	{
		bool isSingle = fGameManager.IsSingle;
		if (!isSingle)
		{
			bool flag = fGameManager.m_nUserType == 1;
			int num;
			GameObject gameObject;
			GameObject original;
			if (flag)
			{
				num = 1;
				gameObject = this.pLaBaListScn;
				original = this.pOneLaBaMsgScn;
			}
			else
			{
				num = 1;
				gameObject = this.pLaBaListHall;
				original = this.pOneLaBaMsgHall;
			}
			bool flag2 = ChatManager.ShowLaBaList.Count == num;
			if (flag2)
			{
				ChatManager.AllLaBaList.Add(new ArrayList
				{
					sNick,
					sMsg,
					szData
				});
				bool flag3 = ChatManager.AllLaBaList.Count == 1;
				if (flag3)
				{
					GameObject gameObject2 = ChatManager.ShowLaBaList[0];
					bool flag4 = gameObject2 != null;
					if (flag4)
					{
						ChatLaBaCon chatLaBaCon = gameObject2.GetComponent<ChatLaBaCon>();
						chatLaBaCon.m_fLaBaTimeLong = 5f;
					}
				}
			}
			else
			{
				bool flag5 = ChatManager.AllLaBaList.Count > 0 && ChatManager.ShowLaBaList.Count > 1;
				ChatLaBaCon chatLaBaCon;
				if (flag5)
				{
					GameObject gameObject3 = ChatManager.ShowLaBaList[0];
					bool flag6 = gameObject3 != null;
					if (flag6)
					{
						chatLaBaCon = gameObject3.GetComponent<ChatLaBaCon>();
						chatLaBaCon.m_fLaBaTimeLong = 5f;
					}
				}
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(original);
				gameObject4.transform.SetParent(gameObject.transform);
				gameObject4.SetActive(true);
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.transform.localScale = new Vector3(1f, 1f, 1f);
				Text component = gameObject4.transform.Find("OnMsgText").GetComponent<Text>();
				Text component2 = gameObject4.transform.FindChild("NamePanel/OnMsgName").GetComponent<Text>();
				component2.text = sNick + ":";
				component.text = sMsg;
				Button button = component2.gameObject.AddComponent<Button>();
				ChatLink chatLink = component2.gameObject.AddComponent<ChatLink>();
				int nCid = (int)szData[6];
				chatLink.Init(sNick, nCid);
				EventTriggerListener.Get(button.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.onButtonDown);
				EventTriggerListener.Get(button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.onButtonClick);
				chatLaBaCon = gameObject4.AddComponent<ChatLaBaCon>();
				ChatManager.ShowLaBaList.Add(gameObject4);
				chatLaBaCon.InitData(sNick, sMsg, szData, component);
				bool flag7 = num == 1 && ChatManager.AllLaBaList.Count > 0;
				if (flag7)
				{
					chatLaBaCon.m_fLaBaTimeLong = 5f;
				}
			}
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0002726C File Offset: 0x0002546C
	public void AddPushMsg(OnePushMsgInfo pData, int nTimelong = 20)
	{
		bool flag = pData != null;
		if (flag)
		{
			this.m_nTimeLong = nTimelong;
			this.pOnePushMsg.SetActive(true);
			this.sMsg = this.pOnePushMsg.transform.FindChild("Text").GetComponent<Text>();
			this.TimeSdr = this.pOnePushMsg.transform.FindChild("TimeSlider").GetComponent<Slider>();
			this.sMsg.text = pData.m_sMsg;
			pData.m_nState = 1;
			ScnMsgManager.GetInstance().onShowPushMsgInfo = pData;
			int pushMsgTimeLong = this.GetPushMsgTimeLong(pData.dCreateTime);
			bool flag2 = pushMsgTimeLong == -1;
			if (flag2)
			{
				this.OnChangeNextMsg();
			}
			else
			{
				this.TimeSdr.value = 1f - ((float)this.m_nTimeLong - (float)pushMsgTimeLong) / (float)this.m_nTimeLong;
				base.CancelInvoke("onRunPushTime");
				base.InvokeRepeating("onRunPushTime", 0f, 0.1f);
			}
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00027368 File Offset: 0x00025568
	public void OnChangeNextMsg()
	{
		OnePushMsgInfo onePushMsgInfo = null;
		while (ScnMsgManager.GetInstance().m_szAllMsgList.Count > 0)
		{
			onePushMsgInfo = ScnMsgManager.GetInstance().m_szAllMsgList[0];
			ScnMsgManager.GetInstance().m_szAllMsgList.RemoveAt(0);
			bool flag = this.GetPushMsgTimeLong(onePushMsgInfo.dCreateTime) == -1;
			if (!flag)
			{
				break;
			}
			ScnMsgManager.GetInstance().onShowPushMsgInfo = onePushMsgInfo;
			ScnMsgManager.GetInstance().OnKeyCodeNo(false);
		}
		bool flag2 = onePushMsgInfo == null;
		if (flag2)
		{
			this.ClosePushMsg();
		}
		else
		{
			this.AddPushMsg(onePushMsgInfo, 20);
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00027404 File Offset: 0x00025604
	private int GetPushMsgTimeLong(string sCreateTime)
	{
		int endCDTime = Fun.GetEndCDTime(sCreateTime, this.m_nTimeLong);
		bool flag = endCDTime < 1;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = endCDTime;
		}
		return result;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00027434 File Offset: 0x00025634
	private void onRunPushTime()
	{
		int pushMsgTimeLong = this.GetPushMsgTimeLong(ScnMsgManager.GetInstance().onShowPushMsgInfo.dCreateTime);
		bool flag = pushMsgTimeLong == -1 || this.TimeSdr.value == 0f;
		if (flag)
		{
			base.CancelInvoke("onRunPushTime");
			ScnMsgManager.GetInstance().OnKeyCodeNo(false);
			this.OnChangeNextMsg();
		}
		else
		{
			this.TimeSdr.value -= 1f / (float)this.m_nTimeLong / 10f;
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000274C2 File Offset: 0x000256C2
	public void ClosePushMsg()
	{
		ScnMsgManager.GetInstance().onShowPushMsgInfo = null;
		this.pOnePushMsg.SetActive(false);
		base.CancelInvoke("onRunPushTime");
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000274EC File Offset: 0x000256EC
	private void OpenUIByName(string sUI)
	{
		bool flag = sUI != null && sUI == "LuckyDrawUI" && MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnLuckyDrawBtn(null);
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0002752C File Offset: 0x0002572C
	public void ClosePushUI(bool fake)
	{
		if (fake)
		{
			bool flag = ScnMsgManager.GetInstance().onShowPushMsgInfo == null;
			if (flag)
			{
				return;
			}
			int pushMsgTimeLong = this.GetPushMsgTimeLong(ScnMsgManager.GetInstance().onShowPushMsgInfo.dCreateTime);
			bool flag2 = (float)pushMsgTimeLong <= 0f;
			if (flag2)
			{
				this.pOnePushMsg.SetActive(false);
				return;
			}
		}
		bool flag3 = this.pOnePushMsg.activeSelf != fake;
		if (flag3)
		{
			this.pOnePushMsg.SetActive(fake);
		}
	}

	// Token: 0x04000376 RID: 886
	public static ChatUICon inst;

	// Token: 0x04000377 RID: 887
	private GameObject pChatTip;

	// Token: 0x04000378 RID: 888
	private GameObject pInputField;

	// Token: 0x04000379 RID: 889
	private GameObject ChatPinDao;

	// Token: 0x0400037A RID: 890
	private GameObject OneMsgText;

	// Token: 0x0400037B RID: 891
	private Text InputText;

	// Token: 0x0400037C RID: 892
	private GameObject UI_NewChat_SJShow;

	// Token: 0x0400037D RID: 893
	private GameObject UI_NewChat_SJContent;

	// Token: 0x0400037E RID: 894
	private GameObject pEnter;

	// Token: 0x0400037F RID: 895
	private GameObject pChatEnter;

	// Token: 0x04000380 RID: 896
	private int m_nMaxRow = 40;

	// Token: 0x04000381 RID: 897
	private int m_nCurRow;

	// Token: 0x04000382 RID: 898
	private InputField pInputName;

	// Token: 0x04000383 RID: 899
	private int m_nPinDao;

	// Token: 0x04000384 RID: 900
	private GameObject pNowPinDaoObj;

	// Token: 0x04000385 RID: 901
	private ArrayList AllShiJieMsg = new ArrayList();

	// Token: 0x04000386 RID: 902
	private int m_nMaxNum = 40;

	// Token: 0x04000387 RID: 903
	private ArrayList HallChat = new ArrayList
	{
		1,
		2,
		5,
		7,
		10
	};

	// Token: 0x04000388 RID: 904
	private ArrayList RoomChat = new ArrayList
	{
		1,
		2,
		6,
		5,
		7,
		10
	};

	// Token: 0x04000389 RID: 905
	private ArrayList SelectHeroChat = new ArrayList
	{
		1,
		2,
		3,
		6,
		5,
		7,
		10
	};

	// Token: 0x0400038A RID: 906
	private ArrayList ScnChat = new ArrayList
	{
		3,
		2,
		6,
		5,
		7,
		10
	};

	// Token: 0x0400038B RID: 907
	private ArrayList UserChat = new ArrayList();

	// Token: 0x0400038C RID: 908
	private ArrayList m_szSiLiaoList = new ArrayList();

	// Token: 0x0400038D RID: 909
	public string m_sSelectUserName = string.Empty;

	// Token: 0x0400038E RID: 910
	public bool isOpenChat;

	// Token: 0x0400038F RID: 911
	private GameObject pLaBaListScn;

	// Token: 0x04000390 RID: 912
	private GameObject pOneLaBaMsgScn;

	// Token: 0x04000391 RID: 913
	private GameObject pLaBaListHall;

	// Token: 0x04000392 RID: 914
	private GameObject pOneLaBaMsgHall;

	// Token: 0x04000393 RID: 915
	private GameObject pLaBaBtn;

	// Token: 0x04000394 RID: 916
	private GameObject pLaBaMoney;

	// Token: 0x04000395 RID: 917
	private GameObject pWorldMoney;

	// Token: 0x04000396 RID: 918
	private GameObject pOnePushMsg;

	// Token: 0x04000397 RID: 919
	private Text TLaBaMoney;

	// Token: 0x04000398 RID: 920
	private Text TWorldMoney;

	// Token: 0x04000399 RID: 921
	private string Filte_chatMsg = string.Empty;

	// Token: 0x0400039A RID: 922
	private bool LaBaIsUp = true;

	// Token: 0x0400039B RID: 923
	private float nOldParValue;

	// Token: 0x0400039C RID: 924
	private Scrollbar sPbar;

	// Token: 0x0400039D RID: 925
	public string sLastMiYuUser = string.Empty;

	// Token: 0x0400039E RID: 926
	private bool IsCheckChatMoney;

	// Token: 0x0400039F RID: 927
	private bool IsCheckLaBaChatMoney;

	// Token: 0x040003A0 RID: 928
	private Text sMsg;

	// Token: 0x040003A1 RID: 929
	private Slider TimeSdr;

	// Token: 0x040003A2 RID: 930
	private int m_nTimeLong = 20;
}
