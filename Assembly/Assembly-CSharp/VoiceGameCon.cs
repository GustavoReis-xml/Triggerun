using System;
using System.Collections;
using UnityEngine;
using YouMe;

// Token: 0x02000493 RID: 1171
public class VoiceGameCon : Window
{
	// Token: 0x06002D45 RID: 11589 RVA: 0x0018DD03 File Offset: 0x0018BF03
	protected override void Awake()
	{
		base.Awake();
		VoiceGameCon.Inst = this;
		this.IsStartVoice = false;
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x0018DD1A File Offset: 0x0018BF1A
	private void Start()
	{
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x0018DD20 File Offset: 0x0018BF20
	private void Update()
	{
		bool flag = !this.IsStartVoice;
		if (!flag)
		{
			bool flag2 = Input.GetKeyDown(KeyCode.Escape) && VoiceUICon.inst != null && VoiceUICon.inst.visible;
			if (flag2)
			{
				VoiceUICon.inst.Close();
			}
			bool flag3 = Input_Manager.Get_Button("Key_Voice_Chat") && this.bTalkModel && this.BtnState;
			if (flag3)
			{
				this.BtnState = false;
				YouMeVoiceAPI.GetInstance().SetMicrophoneMute(false);
			}
			bool flag4 = Input_Manager.Get_Button_Up("Key_Voice_Chat") && this.bTalkModel && !this.BtnState;
			if (flag4)
			{
				this.BtnState = true;
				YouMeVoiceAPI.GetInstance().SetMicrophoneMute(true);
			}
			bool flag5 = Input_Manager.Get_Button_Up("Key_Microphone_Mute_On_Off");
			if (flag5)
			{
				bool flag6 = this.bTalkModel;
				if (flag6)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40037, new object[0]), "ffdd00", "system");
					return;
				}
				bool flag7 = VoiceGameCon.m_nVoicePos == 0;
				if (flag7)
				{
					return;
				}
				VoiceGameCon.MyMai = !YouMeVoiceAPI.GetInstance().GetMicrophoneMute();
				bool flag8 = VoiceTalkCon.inst != null;
				if (flag8)
				{
					bool myMai = VoiceGameCon.MyMai;
					if (myMai)
					{
						bool flag9 = VoiceUICon.inst != null;
						if (flag9)
						{
							VoiceUICon.inst.UserVoiceState.Add(PlayerDataManager.getInstance().HUserData.m_sName);
							VoiceUICon.inst.UserVoiceState.Add(1);
						}
					}
					else
					{
						bool flag10 = VoiceUICon.inst != null;
						if (flag10)
						{
							int num = VoiceUICon.inst.UserVoiceState.IndexOf(PlayerDataManager.getInstance().HUserData.m_sName);
							bool flag11 = num != -1;
							if (flag11)
							{
								VoiceUICon.inst.UserVoiceState.RemoveAt(num + 1);
								VoiceUICon.inst.UserVoiceState.RemoveAt(num);
							}
						}
					}
					VoiceTalkCon.inst.SetTalk(PlayerDataManager.getInstance().HUserData.m_nCid.ToString(), VoiceGameCon.MyMai ? 1 : 2);
					bool flag12 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
					if (flag12)
					{
						VoiceUICon.inst.InitUI();
					}
				}
				YouMeVoiceAPI.GetInstance().SetMicrophoneMute(VoiceGameCon.MyMai);
			}
			bool flag13 = Input_Manager.Get_Button_Up("Key_Open_Chat_Window");
			if (flag13)
			{
				bool chatInput = ChatManager.ChatInput;
				if (chatInput)
				{
					return;
				}
				bool flag14 = fGameManager.m_nUserType == 1 && SelectHeroUICon.inst != null && SelectHeroUICon.inst.visible;
				if (flag14)
				{
					bool flag15 = !fGameManager.Heroplayer.netinitok;
					if (flag15)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40038, new object[0]), "ffdd00", "system");
						return;
					}
					SelectHeroUICon.inst.Close();
				}
				bool flag16 = VoiceUICon.inst == null;
				if (flag16)
				{
					fUIManager.GetInstance().ShowWin("VoiceUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loadchatuiok), null, true);
					bool flag17 = fGameManager.m_nUserType == 1;
					if (flag17)
					{
						HeroPlayer.isPlayerControl = false;
						HeroPlayer.isMouseShow = true;
					}
				}
				else
				{
					bool visible = VoiceUICon.inst.visible;
					if (visible)
					{
						bool flag18 = fGameManager.m_nUserType == 1;
						if (flag18)
						{
							HeroPlayer.isPlayerControl = true;
							HeroPlayer.isMouseShow = false;
						}
						VoiceUICon.inst.Close();
					}
					else
					{
						bool flag19 = fGameManager.m_nUserType == 1;
						if (flag19)
						{
							HeroPlayer.isPlayerControl = false;
							HeroPlayer.isMouseShow = true;
						}
						VoiceUICon.inst.Open();
						VoiceUICon.inst.Reset();
					}
				}
			}
			bool flag20 = Input_Manager.Get_Button_Up("Key_Team_Chat_On_Off");
			if (flag20)
			{
				bool flag21 = TeamManager.GetInstance().m_nTeamID == 0;
				if (flag21)
				{
					return;
				}
				bool flag22 = VoiceGameCon.m_nVoicePos == 0 || VoiceGameCon.m_nVoicePos == 2;
				if (flag22)
				{
					VoiceGameCon.Inst.JoinVoice(PlayerDataManager.getInstance().HUserData.m_nCid, TeamManager.GetInstance().m_nTeamID, 1);
				}
				else
				{
					VoiceGameCon.Inst.LeaveVoice();
				}
			}
			bool flag23 = Input_Manager.Get_Button_Up("Key_Game_Chat_On_Off") && MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag23)
			{
				bool flag24 = VoiceGameCon.m_nVoicePos == 0 || VoiceGameCon.m_nVoicePos == 1;
				if (flag24)
				{
					VoiceGameCon.Inst.JoinVoice(PlayerDataManager.getInstance().HUserData.m_nCid, MatchManager.GetInstance().m_nGameVoice, 2);
				}
				else
				{
					VoiceGameCon.Inst.LeaveVoice();
				}
			}
		}
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x0018E1D9 File Offset: 0x0018C3D9
	public void loadchatuiok(string sUI, GameObject ob, object szParam)
	{
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x0018E1DC File Offset: 0x0018C3DC
	public void InitVoice()
	{
		bool isStartVoice = this.IsStartVoice;
		if (isStartVoice)
		{
			YouMeVoiceAPI.GetInstance().SetCallback("VoiceGameObj");
			YouMeErrorCode youMeErrorCode = YouMeVoiceAPI.GetInstance().Init("YOUME8661B5FFE856F6887D41B94F6CC66F070A56BF09", "n96jbfQ/ibca2M8qRZV1+BByDmvG7wB57/hD/a6J/wW24hovSL887ySfODNhxBTYa7ZCxVKGG6Cjdo98TtVnHvb2KzNjbCg2/U2o7eCxdgft5ihIYsbsMMCb+AKC89DBjVGD7iDG8ZW2rZmz7DcR4Y3C5jJlLhFLa6Z2vguRArcBAAE=", YOUME_RTC_SERVER_REGION.RTC_CN_SERVER, string.Empty);
			bool flag = youMeErrorCode > YouMeErrorCode.YOUME_SUCCESS;
			if (flag)
			{
				Debug.Log("初始化失败" + youMeErrorCode.ToString());
			}
		}
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x0018E248 File Offset: 0x0018C448
	public void UnInit()
	{
		bool isStartVoice = this.IsStartVoice;
		if (isStartVoice)
		{
			YouMeVoiceAPI.GetInstance().UnInit();
		}
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x0018E270 File Offset: 0x0018C470
	public void JoinVoice(int nCid, int nVcoieID, int nType)
	{
		bool flag = this.IsStartVoice && !(this.m_nNowTalkID == Mathf.Abs(nVcoieID).ToString()) && (!(this.m_nNowTalkID != string.Empty) || this.LeaveVoice() == YouMeErrorCode.YOUME_SUCCESS);
		if (flag)
		{
			this.m_nNowTalkID = Mathf.Abs(nVcoieID).ToString();
			YouMeErrorCode youMeErrorCode = YouMeVoiceAPI.GetInstance().JoinChannelSingleMode(nCid.ToString(), this.m_nNowTalkID, 1);
			bool flag2 = youMeErrorCode > YouMeErrorCode.YOUME_SUCCESS;
			if (flag2)
			{
				this.m_nNowTalkID = string.Empty;
				VoiceGameCon.m_nVoicePos = 0;
				Debug.Log("加入频道失败" + youMeErrorCode.ToString());
			}
			else
			{
				VoiceGameCon.m_nVoicePos = nType;
			}
			bool flag3 = VoiceUICon.inst != null;
			if (flag3)
			{
				VoiceUICon.inst.SetBtnActive(true);
				VoiceUICon.inst.SetBtnState(VoiceGameCon.m_nVoicePos);
			}
			bool flag4 = VoiceTalkCon.inst == null;
			if (flag4)
			{
				fUIManager.GetInstance().ShowWin("VoiceTalk", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.loadchatuiok), null, true);
			}
		}
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x0018E39C File Offset: 0x0018C59C
	public YouMeErrorCode LeaveVoice()
	{
		bool flag = !this.IsStartVoice;
		YouMeErrorCode result;
		if (flag)
		{
			result = YouMeErrorCode.YOUME_ERROR_WRONG_STATE;
		}
		else
		{
			bool flag2 = VoiceGameCon.m_nVoicePos != 0;
			if (flag2)
			{
				bool flag3 = VoiceUICon.inst != null;
				if (flag3)
				{
					VoiceUICon.inst.SetBtnActive(false);
				}
				ChatManager.GetInstance().OnSendSystemMsg(string.Format(Language.GetLang(40036, new object[0]), new object[0]), "ffdd00", "system");
			}
			VoiceGameCon.m_nVoicePos = 0;
			bool flag4 = VoiceUICon.inst != null;
			if (flag4)
			{
				VoiceUICon.inst.SetBtnText();
			}
			YouMeErrorCode youMeErrorCode = YouMeVoiceAPI.GetInstance().LeaveChannelAll();
			bool flag5 = VoiceUICon.inst != null;
			if (flag5)
			{
				VoiceUICon.inst.SetBtnState(VoiceGameCon.m_nVoicePos);
				VoiceUICon.inst.UserVoiceState.Clear();
			}
			bool flag6 = VoiceTalkCon.inst != null;
			if (flag6)
			{
				VoiceTalkCon.inst.DelAllUser();
			}
			result = youMeErrorCode;
		}
		return result;
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x0018E4A4 File Offset: 0x0018C6A4
	public void SetTalkModel(bool bModel)
	{
		bool isStartVoice = this.IsStartVoice;
		if (isStartVoice)
		{
			this.bTalkModel = bModel;
			YouMeVoiceAPI.GetInstance().SetMicrophoneMute(bModel);
		}
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x0018E4D4 File Offset: 0x0018C6D4
	public void SetVolume(int nvol)
	{
		bool isStartVoice = this.IsStartVoice;
		if (isStartVoice)
		{
			YouMeVoiceAPI.GetInstance().SetVolume((uint)nvol);
		}
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x0018E4FC File Offset: 0x0018C6FC
	private void OnEvent(string strParam)
	{
		bool flag = !this.IsStartVoice;
		if (!flag)
		{
			string[] array = strParam.Split(new char[]
			{
				','
			});
			bool flag2 = array == null;
			if (!flag2)
			{
				YouMeEvent youMeEvent = (YouMeEvent)int.Parse(array[0]);
				YouMeErrorCode youMeErrorCode = (YouMeErrorCode)int.Parse(array[1]);
				string nNowTalkID = array[2];
				string text = array[3];
				switch (youMeEvent)
				{
				case YouMeEvent.YOUME_EVENT_INIT_OK:
					Debug.Log("初始化成功");
					break;
				case YouMeEvent.YOUME_EVENT_INIT_FAILED:
					Debug.Log("初始化失败，错误码:" + youMeErrorCode.ToString());
					break;
				case YouMeEvent.YOUME_EVENT_JOIN_OK:
				{
					YouMeVoiceAPI.GetInstance().SetSpeakerMute(false);
					bool flag3 = GameRoot.inst.Setting.Voice_Class.Voice_Chat_Type == 1;
					if (flag3)
					{
						YouMeVoiceAPI.GetInstance().SetMicrophoneMute(false);
					}
					YouMeVoiceAPI.GetInstance().SetVadCallbackEnabled(true);
					YouMeVoiceAPI.GetInstance().GetChannelUserList(this.m_nNowTalkID, -1, true);
					Debug.Log(text + "加入频道成功");
					bool flag4 = text == PlayerDataManager.getInstance().HUserData.m_nCid.ToString();
					if (flag4)
					{
						this.m_nNowTalkID = nNowTalkID;
					}
					break;
				}
				case YouMeEvent.YOUME_EVENT_JOIN_FAILED:
					Debug.Log("进入语音频道失败");
					break;
				case YouMeEvent.YOUME_EVENT_LEAVED_ALL:
					this.m_nNowTalkID = string.Empty;
					this.VoiceUserList.Clear();
					Debug.Log(text + "离开所有频道成功");
					break;
				case YouMeEvent.YOUME_EVENT_OTHERS_VOICE_ON:
				{
					bool flag5 = VoiceTalkCon.inst != null && text != string.Empty;
					if (flag5)
					{
						VoiceTalkCon.inst.SetTalk(text, 1);
					}
					break;
				}
				case YouMeEvent.YOUME_EVENT_OTHERS_VOICE_OFF:
				{
					bool flag6 = VoiceTalkCon.inst != null && text != string.Empty;
					if (flag6)
					{
						VoiceTalkCon.inst.SetTalk(text, 2);
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x0018E77C File Offset: 0x0018C97C
	private void OnMemberChange(string strParam)
	{
		bool flag = !this.IsStartVoice;
		if (!flag)
		{
			JsonData jsonData = JsonMapper.ToObject(strParam);
			bool flag2 = !jsonData.Keys.Contains("memchange");
			if (!flag2)
			{
				JsonData jsonData2 = jsonData["memchange"];
				Debug.Log(jsonData2.ToJson());
				for (int i = 0; i < jsonData2.Count; i++)
				{
					JsonData jsonData3 = jsonData2[i];
					bool flag3 = !(bool)jsonData3["isJoin"];
					if (flag3)
					{
						bool flag4 = this.VoiceUserList.IndexOf((string)jsonData3["userid"]) != -1;
						if (flag4)
						{
							this.VoiceUserList.Remove((string)jsonData3["userid"]);
						}
					}
					else
					{
						bool flag5 = this.VoiceUserList.IndexOf((string)jsonData3["userid"]) == -1;
						if (flag5)
						{
							this.VoiceUserList.Add((string)jsonData3["userid"]);
						}
						bool flag6 = !((string)jsonData3["userid"] == PlayerDataManager.getInstance().HUserData.m_nCid.ToString());
						if (flag6)
						{
						}
					}
				}
				bool flag7 = this.bTalkModel;
				if (flag7)
				{
					ChatManager.GetInstance().OnSendSystemMsg(string.Format(Language.GetLang(40034, new object[]
					{
						VoiceGameCon.Inst.VoiceUserList.Count.ToString(),
						GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Hero_Control_Data[0].Key_Open_Chat_Window),
						GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Hero_Control_Data[0].Key_Voice_Chat)
					}), new object[0]), "ffdd00", "system");
				}
				else
				{
					ChatManager.GetInstance().OnSendSystemMsg(string.Format(Language.GetLang(40035, new object[]
					{
						VoiceGameCon.Inst.VoiceUserList.Count.ToString(),
						GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Hero_Control_Data[0].Key_Open_Chat_Window)
					}), new object[0]), "ffdd00", "system");
				}
				bool flag8 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
				if (flag8)
				{
					VoiceUICon.inst.SetAllGameUser();
				}
			}
		}
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x0018EA50 File Offset: 0x0018CC50
	private void OnRequestRestApi(string strParam)
	{
		bool isStartVoice = this.IsStartVoice;
		if (isStartVoice)
		{
			string[] array = strParam.Split(new char[]
			{
				','
			});
			bool flag = array != null;
			if (flag)
			{
			}
		}
	}

	// Token: 0x04002663 RID: 9827
	public static VoiceGameCon Inst;

	// Token: 0x04002664 RID: 9828
	public static int m_nVoicePos;

	// Token: 0x04002665 RID: 9829
	public bool bTalkModel = true;

	// Token: 0x04002666 RID: 9830
	private string m_nNowTalkID = string.Empty;

	// Token: 0x04002667 RID: 9831
	public static bool MyMai = true;

	// Token: 0x04002668 RID: 9832
	public ArrayList VoiceUserList = new ArrayList();

	// Token: 0x04002669 RID: 9833
	public bool BtnState = true;

	// Token: 0x0400266A RID: 9834
	public bool IsStartVoice = true;
}
