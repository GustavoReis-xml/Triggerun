using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000495 RID: 1173
public class VoiceUICon : Window
{
	// Token: 0x06002D5A RID: 11610 RVA: 0x0018ED98 File Offset: 0x0018CF98
	private void Start()
	{
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x0018ED9B File Offset: 0x0018CF9B
	private void Update()
	{
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x0018EDA0 File Offset: 0x0018CFA0
	protected override void Awake()
	{
		base.Awake();
		VoiceUICon.inst = this;
		this.BackBtn = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(this.BackBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		this.JoinTeamBtn = this.UIGameObj.Get_GameObj(1).GetComponent<Button>();
		this.TeamDesc = this.JoinTeamBtn.gameObject.transform.FindChild("VoiceNameText").GetComponent<Text>();
		this.TeamNum = this.JoinTeamBtn.gameObject.transform.FindChild("VoiceUserText").GetComponent<Text>();
		this.JoinGameBtn = this.UIGameObj.Get_GameObj(2).GetComponent<Button>();
		this.GameDesc = this.JoinGameBtn.gameObject.transform.FindChild("VoiceNameText").GetComponent<Text>();
		this.GameNum = this.JoinGameBtn.gameObject.transform.FindChild("VoiceUserText").GetComponent<Text>();
		this.OpenVoice_Team = this.UIGameObj.Get_GameObj(3);
		EventTriggerListener.Get(this.OpenVoice_Team).onClick = new EventTriggerListener.VoidDelegate(this.OnJoinTeamVoice);
		this.OpenVoice_Game = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(this.OpenVoice_Game).onClick = new EventTriggerListener.VoidDelegate(this.OnJoinGameVoice);
		this.VoiceState = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.Content = this.UIGameObj.Get_GameObj(7);
		this.UserGameObj = this.UIGameObj.Get_GameObj(8);
		this.UserGameObj.SetActive(false);
		this.UserDaTingObj = this.UIGameObj.Get_GameObj(9);
		this.UserDaTingObj.SetActive(false);
		this.OpenVoice_Team1 = this.UIGameObj.Get_GameObj(10);
		EventTriggerListener.Get(this.OpenVoice_Team1).onClick = new EventTriggerListener.VoidDelegate(this.OnJoinTeamVoice);
		this.OpenVoice_Team1.SetActive(false);
		this.OpenVoice_Game1 = this.UIGameObj.Get_GameObj(11);
		EventTriggerListener.Get(this.OpenVoice_Game1).onClick = new EventTriggerListener.VoidDelegate(this.OnJoinGameVoice);
		this.OpenVoice_Game1.SetActive(false);
		this.imgbk = this.UIGameObj.Get_GameObj(12);
		UIDepth uidepth = base.gameObject.AddComponent<UIDepth>();
		uidepth.SetUIDepth(4, true);
		this.InitUI();
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x0018F018 File Offset: 0x0018D218
	public void InitUI()
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (flag)
		{
			this.imgbk.SetActive(true);
		}
		else
		{
			this.imgbk.SetActive(false);
			bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
			if (flag2)
			{
				MainUICon.inst.Close();
			}
		}
		foreach (object obj in this.Content.transform)
		{
			Transform transform = (Transform)obj;
			bool flag3 = transform.gameObject.name != "Userico" && transform.gameObject.name != "DatingUserico";
			if (flag3)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.ObjArr.Clear();
		bool flag4 = TeamManager.GetInstance().m_nTeamID == 0 && MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag4)
		{
			this.TeamDesc.text = Language.GetLang(50016, new object[0]);
			this.GameDesc.text = Language.GetLang(50017, new object[0]);
			this.TeamNum.text = string.Empty;
			this.GameNum.text = string.Empty;
			this.VoiceState.text = string.Empty;
			this.JoinTeamBtn.interactable = false;
			this.JoinGameBtn.interactable = false;
			this.OpenVoice_Team.SetActive(false);
			this.OpenVoice_Team1.SetActive(false);
			this.OpenVoice_Game.SetActive(false);
			this.OpenVoice_Game1.SetActive(false);
			this.Content.SetActive(false);
		}
		else
		{
			bool flag5 = TeamManager.GetInstance().m_nTeamID != 0 && MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag5)
			{
				this.TeamDesc.text = Language.GetLang(50011, new object[0]);
				this.JoinTeamBtn.interactable = true;
				this.GameDesc.text = Language.GetLang(50012, new object[0]);
				this.JoinGameBtn.interactable = true;
			}
			else
			{
				bool flag6 = TeamManager.GetInstance().m_nTeamID == 0;
				if (flag6)
				{
					this.TeamDesc.text = Language.GetLang(50016, new object[0]);
					this.TeamNum.text = string.Empty;
					this.JoinTeamBtn.interactable = false;
					this.GameDesc.text = Language.GetLang(50012, new object[0]);
					this.JoinGameBtn.interactable = true;
				}
				bool flag7 = MatchManager.GetInstance().m_nGameVoice == 0;
				if (flag7)
				{
					this.TeamDesc.text = Language.GetLang(50011, new object[0]);
					this.JoinTeamBtn.interactable = true;
					this.GameDesc.text = Language.GetLang(50017, new object[0]);
					this.GameNum.text = string.Empty;
					this.JoinGameBtn.interactable = false;
				}
			}
			this.Content.SetActive(true);
			this.InitTeamUser();
		}
		bool flag8 = TeamManager.GetInstance().m_nTeamID != 0 || MatchManager.GetInstance().m_nGameVoice != 0;
		if (flag8)
		{
			this.OnSetBtn();
		}
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x0018F3C0 File Offset: 0x0018D5C0
	private void OnSetBtn()
	{
		bool flag = VoiceGameCon.m_nVoicePos == 0;
		if (flag)
		{
			this.OpenVoice_Team.SetActive(false);
			this.OpenVoice_Team1.SetActive(false);
			this.OpenVoice_Game1.SetActive(false);
			this.OpenVoice_Game.SetActive(false);
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag2)
			{
				this.OpenVoice_Team1.SetActive(true);
			}
			bool flag3 = MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag3)
			{
				this.OpenVoice_Game1.SetActive(true);
			}
		}
		bool flag4 = VoiceGameCon.m_nVoicePos == 1;
		if (flag4)
		{
			this.OpenVoice_Team.SetActive(true);
			this.OpenVoice_Team1.SetActive(false);
			this.OpenVoice_Game.SetActive(false);
			bool flag5 = MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag5)
			{
				this.OpenVoice_Game1.SetActive(true);
			}
			else
			{
				this.OpenVoice_Game1.SetActive(false);
			}
		}
		bool flag6 = VoiceGameCon.m_nVoicePos == 2;
		if (flag6)
		{
			this.OpenVoice_Team.SetActive(false);
			bool flag7 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag7)
			{
				this.OpenVoice_Team1.SetActive(true);
			}
			else
			{
				this.OpenVoice_Team1.SetActive(false);
			}
			this.OpenVoice_Game.SetActive(true);
			this.OpenVoice_Game1.SetActive(false);
		}
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x0018F524 File Offset: 0x0018D724
	public void InitTeamUser()
	{
		ArrayList arrayList = new ArrayList();
		GameObject gameObject = (MatchManager.GetInstance().m_nGameVoice != 0) ? this.UserGameObj : this.UserDaTingObj;
		arrayList = this.GetGameUserList();
		for (int i = 0; i < arrayList.Count; i++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(gameObject.transform.parent);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			string key = string.Empty;
			key = (gameObject2.name = this.GetUserCID(arrayList[i].ToString()).ToString());
			UserVoiceObj userVoiceObj = gameObject2.AddComponent<UserVoiceObj>();
			userVoiceObj.Init(arrayList, i);
			gameObject2.SetActive(true);
			this.ObjArr.Add(key, gameObject2);
		}
		this.SetBtnText();
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x0018F634 File Offset: 0x0018D834
	public void SetBtnActive(bool bl)
	{
		foreach (GameObject gameObject in this.ObjArr.Values)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				this.SetBtn(gameObject, bl);
			}
		}
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x0018F6A0 File Offset: 0x0018D8A0
	private void SetBtn(GameObject obj, bool bl)
	{
		bool activeSelf = obj.activeSelf;
		if (activeSelf)
		{
			UserVoiceObj component = obj.GetComponent<UserVoiceObj>();
			bool flag = component != null;
			if (flag)
			{
				component.OnSetBtnActive(bl);
			}
		}
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x0018F6D8 File Offset: 0x0018D8D8
	public void SetAllGameUser()
	{
		foreach (GameObject gameObject in this.ObjArr.Values)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				this.SetUserObjBtn(gameObject);
			}
		}
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x0018F744 File Offset: 0x0018D944
	private void SetUserObjBtn(GameObject obj)
	{
		bool activeSelf = obj.activeSelf;
		if (activeSelf)
		{
			this.SetBtnText();
			UserVoiceObj component = obj.GetComponent<UserVoiceObj>();
			string name = obj.name;
			int num = VoiceGameCon.Inst.VoiceUserList.IndexOf(name);
			bool flag = num == -1;
			if (flag)
			{
				component.maiopenbtn.SetActive(false);
				component.maiclosebtn.SetActive(false);
				component.labaopenbtn.SetActive(false);
				component.labaclosebtn.SetActive(false);
			}
			else
			{
				bool flag2 = name == PlayerDataManager.getInstance().HUserData.m_nCid.ToString();
				if (flag2)
				{
					component.maiclosebtn.SetActive(false);
					component.labaopenbtn.SetActive(false);
					component.labaclosebtn.SetActive(false);
					component.maiopenbtn.SetActive(true);
				}
				else
				{
					component.labaopenbtn.SetActive(true);
					component.maiopenbtn.SetActive(false);
					component.labaclosebtn.SetActive(false);
					component.maiclosebtn.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x0018F85C File Offset: 0x0018DA5C
	public void SetBtnText()
	{
		bool flag = VoiceGameCon.m_nVoicePos == 1;
		if (flag)
		{
			bool flag2 = GameRoot.inst.Setting.Voice_Class.Voice_Chat_Type == 0;
			if (flag2)
			{
				bool flag3 = fGameManager.m_nUserType == 0;
				if (flag3)
				{
					this.VoiceState.text = string.Format(Language.GetLang(50014, new object[]
					{
						GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Hero_Control_Data[0].Key_Voice_Chat)
					}), new object[0]);
				}
				else
				{
					this.VoiceState.text = string.Format(Language.GetLang(50014, new object[]
					{
						GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(fGameManager.Heroplayer.playerCommon.m_nModelIndex).Key_Voice_Chat)
					}), new object[0]);
				}
			}
			else
			{
				this.VoiceState.text = Language.GetLang(50018, new object[0]);
			}
			this.TeamNum.text = string.Format(Language.GetLang(50013, new object[]
			{
				VoiceGameCon.Inst.VoiceUserList.Count.ToString()
			}), new object[0]);
		}
		else
		{
			this.TeamNum.text = string.Empty;
		}
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x0018F9DC File Offset: 0x0018DBDC
	private ArrayList GetGameUserList()
	{
		bool flag = MatchManager.GetInstance().m_nGameVoice == 0;
		ArrayList result;
		if (flag)
		{
			result = this.GetTeamUserList();
		}
		else
		{
			result = roleuiManager.GetInstance().GetGameUserList(0, 0);
		}
		return result;
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x0018FA18 File Offset: 0x0018DC18
	private ArrayList GetTeamUserList()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < TeamManager.GetInstance().MyTeamData.Count; i += 2)
		{
			arrayList.Add(TeamManager.GetInstance().MyTeamData[i]);
		}
		return arrayList;
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x0018FA68 File Offset: 0x0018DC68
	public int GetUserFaceID(string sNick)
	{
		int result = 1009;
		bool flag = MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag)
		{
			int num = TeamManager.GetInstance().MyTeamData.IndexOf(sNick);
			bool flag2 = num != -1;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)TeamManager.GetInstance().MyTeamData[num + 1];
				return (int)arrayList[10];
			}
		}
		else
		{
			UserData userData = MatchManager.GetInstance().GetUserData(sNick);
			bool flag3 = userData != null;
			if (flag3)
			{
				result = userData.m_nHeroID;
			}
		}
		return result;
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x0018FB0C File Offset: 0x0018DD0C
	public int GetUserTeamID(string sNick)
	{
		int result = 0;
		bool flag = MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag)
		{
			int num = TeamManager.GetInstance().MyTeamData.IndexOf(sNick);
			bool flag2 = num != -1;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)TeamManager.GetInstance().MyTeamData[num + 1];
				return (int)arrayList[12];
			}
		}
		else
		{
			UserData userData = MatchManager.GetInstance().GetUserData(sNick);
			bool flag3 = userData != null;
			if (flag3)
			{
				result = userData.m_nTeamID;
			}
		}
		return result;
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x0018FBAC File Offset: 0x0018DDAC
	public int GetUserCID(string sNick)
	{
		int result = 0;
		bool flag = MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag)
		{
			int num = TeamManager.GetInstance().MyTeamData.IndexOf(sNick);
			bool flag2 = num != -1;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)TeamManager.GetInstance().MyTeamData[num + 1];
				return (int)arrayList[7];
			}
		}
		else
		{
			UserData userData = MatchManager.GetInstance().GetUserData(sNick);
			bool flag3 = userData != null;
			if (flag3)
			{
				result = userData.m_nCid;
			}
		}
		return result;
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x0018FC48 File Offset: 0x0018DE48
	public void SetUserObj(GameObject obj)
	{
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x0018FC4C File Offset: 0x0018DE4C
	public void SetBtnState(int nState)
	{
		bool flag = false;
		if (nState != 0)
		{
			if (nState == 1)
			{
				flag = true;
			}
			bool flag2 = nState == 2;
			if (flag2)
			{
				flag = false;
			}
			bool flag3 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag3)
			{
				this.OpenVoice_Team.SetActive(flag);
				this.OpenVoice_Team1.SetActive(!flag);
			}
			bool flag4 = MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag4)
			{
				this.OpenVoice_Game1.SetActive(flag);
				this.OpenVoice_Game.SetActive(!flag);
			}
		}
		else
		{
			bool flag5 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag5)
			{
				this.OpenVoice_Team.SetActive(false);
				this.OpenVoice_Team1.SetActive(true);
			}
			bool flag6 = MatchManager.GetInstance().m_nGameVoice != 0;
			if (flag6)
			{
				this.OpenVoice_Game.SetActive(false);
				this.OpenVoice_Game1.SetActive(true);
			}
		}
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x0018FD40 File Offset: 0x0018DF40
	private void OnJoinTeamVoice(GameObject obj)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0;
		if (flag)
		{
			bool flag2 = VoiceGameCon.m_nVoicePos == 0 || VoiceGameCon.m_nVoicePos == 2;
			if (flag2)
			{
				VoiceGameCon.Inst.JoinVoice(PlayerDataManager.getInstance().HUserData.m_nCid, TeamManager.GetInstance().m_nTeamID, 1);
			}
			else
			{
				VoiceGameCon.Inst.LeaveVoice();
			}
		}
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x0018FDB0 File Offset: 0x0018DFB0
	private void OnJoinGameVoice(GameObject obj)
	{
		bool flag = MatchManager.GetInstance().m_nGameVoice != 0;
		if (flag)
		{
			bool flag2 = VoiceGameCon.m_nVoicePos == 0 || VoiceGameCon.m_nVoicePos == 1;
			if (flag2)
			{
				VoiceGameCon.Inst.JoinVoice(PlayerDataManager.getInstance().HUserData.m_nCid, MatchManager.GetInstance().m_nGameVoice, 2);
			}
			else
			{
				VoiceGameCon.Inst.LeaveVoice();
			}
		}
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x0018FE20 File Offset: 0x0018E020
	private void OnClickClose(GameObject go)
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (flag)
		{
			HeroPlayer.isPlayerControl = true;
			HeroPlayer.isMouseShow = false;
		}
		this.Close();
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x0018FE50 File Offset: 0x0018E050
	public override void Close()
	{
		base.Close();
		bool flag = MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag)
		{
			fUIManager.GetInstance().SetWinActive("MainUI", true);
		}
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x0018FE89 File Offset: 0x0018E089
	public override void Reset()
	{
		base.Reset();
		this.InitUI();
	}

	// Token: 0x0400266F RID: 9839
	public static VoiceUICon inst;

	// Token: 0x04002670 RID: 9840
	private Button JoinTeamBtn;

	// Token: 0x04002671 RID: 9841
	private Button JoinGameBtn;

	// Token: 0x04002672 RID: 9842
	private Text TeamDesc;

	// Token: 0x04002673 RID: 9843
	private Text GameDesc;

	// Token: 0x04002674 RID: 9844
	private Text TeamNum;

	// Token: 0x04002675 RID: 9845
	private Text GameNum;

	// Token: 0x04002676 RID: 9846
	private GameObject OpenVoice_Team;

	// Token: 0x04002677 RID: 9847
	private GameObject OpenVoice_Game;

	// Token: 0x04002678 RID: 9848
	private GameObject OpenVoice_Team1;

	// Token: 0x04002679 RID: 9849
	private GameObject OpenVoice_Game1;

	// Token: 0x0400267A RID: 9850
	private Text VoiceState;

	// Token: 0x0400267B RID: 9851
	private Text JoinTeamBtn_Text;

	// Token: 0x0400267C RID: 9852
	private Text JoinGameBtn_Text;

	// Token: 0x0400267D RID: 9853
	private GameObject Content;

	// Token: 0x0400267E RID: 9854
	private GameObject UserDaTingObj;

	// Token: 0x0400267F RID: 9855
	private GameObject UserGameObj;

	// Token: 0x04002680 RID: 9856
	private GameObject BackBtn;

	// Token: 0x04002681 RID: 9857
	private GameObject imgbk;

	// Token: 0x04002682 RID: 9858
	public ArrayList UserVoiceState = new ArrayList();

	// Token: 0x04002683 RID: 9859
	private Dictionary<string, GameObject> ObjArr = new Dictionary<string, GameObject>();
}
