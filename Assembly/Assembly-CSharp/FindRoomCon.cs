using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200012A RID: 298
public class FindRoomCon : Window
{
	// Token: 0x06000AFF RID: 2815 RVA: 0x00064714 File Offset: 0x00062914
	protected override void Awake()
	{
		base.Awake();
		this.titleTxt = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.joinRoomUI = this.UIGameObj.Get_GameObj(0);
		this.joinRoomUI.SetActive(false);
		this.joinRoomMapSp = this.UIGameObj.Get_GameObj(1).GetComponent<RawImage>();
		this.joinRoomMapSp.enabled = false;
		this.joinRoomName = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.joinRoomMapTypeSp = this.UIGameObj.Get_GameObj(49).GetComponent<Image>();
		this.joinRoomMapType = this.UIGameObj.Get_GameObj(50).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(26);
		gameObject = this.UIGameObj.Get_GameObj(27);
		gameObject = this.UIGameObj.Get_GameObj(28);
		gameObject = this.UIGameObj.Get_GameObj(29);
		gameObject = this.UIGameObj.Get_GameObj(30);
		this.joinRoomRender = this.UIGameObj.Get_GameObj(5);
		this.joinRoomRender.SetActive(false);
		gameObject = this.UIGameObj.Get_GameObj(6);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCreateRoom);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickJoinRoom);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pagePreBtn = this.UIGameObj.Get_GameObj(36).GetComponent<Button>();
		EventTriggerListener.Get(this.pagePreBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickListPage);
		EventTriggerListener.Get(this.pagePreBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pageNextBtn = this.UIGameObj.Get_GameObj(37).GetComponent<Button>();
		EventTriggerListener.Get(this.pageNextBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickListPage);
		EventTriggerListener.Get(this.pageNextBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pageNumLb = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.pageNumLb.text = "1/1";
		this.usePassword = this.UIGameObj.Get_GameObj(17).GetComponent<Toggle>();
		this.passwordInput = this.UIGameObj.Get_GameObj(18).GetComponent<InputField>();
		this.passwordInput.onValueChanged.AddListener(new UnityAction<string>(this.OnInputChange));
		this.createRoomUI = this.UIGameObj.Get_GameObj(9);
		this.createRoomUI.SetActive(false);
		this.createRoomMapSp = this.UIGameObj.Get_GameObj(10).GetComponent<RawImage>();
		this.createRoomMapSp.enabled = false;
		this.createRoomMapTypeSp = this.UIGameObj.Get_GameObj(33).GetComponent<Image>();
		this.createRoomMapType = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.createRoomMapName = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.createRoomMapDesc = this.UIGameObj.Get_GameObj(35).GetComponent<Text>();
		this.createMapList = this.UIGameObj.Get_GameObj(24).GetComponent<ScrollRect>();
		this.createRoomMapRender = this.UIGameObj.Get_GameObj(12);
		this.createRoomMapRender.SetActive(false);
		this.createRoomTeam1 = this.UIGameObj.Get_GameObj(13);
		this.createRoomTeam2 = this.UIGameObj.Get_GameObj(14);
		gameObject = this.UIGameObj.Get_GameObj(31);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeTeam);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverChangeTeam);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitChangeTeam);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(32);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeTeam);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverChangeTeam);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitChangeTeam);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.roomPlayerRender = this.UIGameObj.Get_GameObj(15);
		this.roomPlayerRender.SetActive(false);
		this.roomAddRender = this.UIGameObj.Get_GameObj(16);
		this.roomAddRender.SetActive(false);
		this.mapTabRender = this.UIGameObj.Get_GameObj(51);
		this.mapTabRender.SetActive(false);
		this.inviteBtn = this.UIGameObj.Get_GameObj(19);
		EventTriggerListener.Get(this.inviteBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRoomInvite);
		EventTriggerListener.Get(this.inviteBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.inviteBtn.SetActive(false);
		this.inviteABtn = this.UIGameObj.Get_GameObj(47);
		EventTriggerListener.Get(this.inviteABtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRoomInvite);
		EventTriggerListener.Get(this.inviteABtn).onEnter = new EventTriggerListener.VoidDelegate(this.OverRoomInvite);
		EventTriggerListener.Get(this.inviteABtn).onExit = new EventTriggerListener.VoidDelegate(this.ExitRoomInvite);
		EventTriggerListener.Get(this.inviteABtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.inviteBBtn = this.UIGameObj.Get_GameObj(48);
		EventTriggerListener.Get(this.inviteBBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRoomInvite);
		EventTriggerListener.Get(this.inviteBBtn).onEnter = new EventTriggerListener.VoidDelegate(this.OverRoomInvite);
		EventTriggerListener.Get(this.inviteBBtn).onExit = new EventTriggerListener.VoidDelegate(this.ExitRoomInvite);
		EventTriggerListener.Get(this.inviteBBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.startBtn = this.UIGameObj.Get_GameObj(20);
		EventTriggerListener.Get(this.startBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRoomStart);
		this.exitBtn = this.UIGameObj.Get_GameObj(21);
		EventTriggerListener.Get(this.exitBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickExitRoom);
		EventTriggerListener.Get(this.exitBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.startBtnTxt = this.UIGameObj.Get_GameObj(23).GetComponent<Text>();
		this.chsGroupUI = this.UIGameObj.Get_GameObj(39);
		this.chsGroupUI.SetActive(false);
		this.groupBtn1 = this.UIGameObj.Get_GameObj(40).GetComponent<Button>();
		EventTriggerListener.Get(this.groupBtn1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGroup);
		EventTriggerListener.Get(this.groupBtn1.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.groupBtn1.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(this.groupBtn1.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		this.groupTxt1 = this.UIGameObj.Get_GameObj(41).GetComponent<Text>();
		this.groupBtn2 = this.UIGameObj.Get_GameObj(42).GetComponent<Button>();
		EventTriggerListener.Get(this.groupBtn2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGroup);
		EventTriggerListener.Get(this.groupBtn2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.groupBtn2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(this.groupBtn2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		this.groupTxt2 = this.UIGameObj.Get_GameObj(43).GetComponent<Text>();
		this.groupBtn3 = this.UIGameObj.Get_GameObj(44).GetComponent<Button>();
		EventTriggerListener.Get(this.groupBtn3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGroup);
		EventTriggerListener.Get(this.groupBtn3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.groupBtn3.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(this.groupBtn3.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		this.groupTxt3 = this.UIGameObj.Get_GameObj(45).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(46);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGroup);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = this.chsGroupUI.transform.Find("Title/Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component.fontSize = 46;
			component = this.groupBtn1.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component = this.groupBtn2.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component = this.groupBtn3.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component = gameObject.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
		}
		FindRoomCon.inst = this;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00065174 File Offset: 0x00063374
	public void Start()
	{
		this.InitRoomList();
		this.InitMapName();
		this.refCount = 0f;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, new MainUICon.BackBtnDelegate(this.OnClickBack), null);
		}
		bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID == 0;
		if (flag2)
		{
			this.SetGroupData();
			RoomManage.GetInstance().GetGroupData();
		}
		else
		{
			this.SetCreateRoom(0);
		}
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x000651FC File Offset: 0x000633FC
	public override void Reset()
	{
		base.Reset();
		this.SetRoomBtn(true);
		this.refCount = 0f;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, new MainUICon.BackBtnDelegate(this.OnClickBack), null);
		}
		bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID == 0;
		if (flag2)
		{
			this.SetGroupData();
			RoomManage.GetInstance().GetGroupData();
		}
		else
		{
			this.SetCreateRoom(0);
		}
		bool flag3 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag3)
		{
			VoiceUICon.inst.Close();
		}
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000652B0 File Offset: 0x000634B0
	private void Update()
	{
		bool flag = !this.joinRoomUI.activeSelf;
		if (!flag)
		{
			bool flag2 = this.nPageIdx == this.lastStartIdx;
			if (flag2)
			{
				this.refCount += Time.deltaTime;
				bool flag3 = this.refCount >= 2f;
				if (flag3)
				{
					this.refCount = 0f;
					this.GetRoomList();
				}
			}
			else
			{
				this.lastStartIdx = this.nPageIdx;
				this.refCount = 0f;
			}
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0006533C File Offset: 0x0006353C
	private void GetRoomInfo(int sidx)
	{
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0006533F File Offset: 0x0006353F
	private void OnJoinListDrag(Vector2 arg0)
	{
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00065344 File Offset: 0x00063544
	private void OnClickBack(object szParam)
	{
		bool flag = this.createRoomUI.activeSelf || PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag)
		{
			RoomManage.GetInstance().OnLeaveRoom();
		}
		else
		{
			bool activeSelf = this.joinRoomUI.activeSelf;
			if (activeSelf)
			{
				RoomManage.GetInstance().GetGroupData();
			}
			else
			{
				bool flag2 = this.chsGroupUI.activeSelf && MainUICon.inst != null && MainUICon.inst.visible;
				if (flag2)
				{
					MainUICon.inst.ChangeTitleToIdx(0, null);
				}
			}
		}
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x000653E0 File Offset: 0x000635E0
	public void CloseCreateRoom()
	{
		this.m_nReadyState = 0;
		base.CancelInvoke("OnCheckReady");
		this.chsGroupUI.SetActive(false);
		this.joinRoomUI.SetActive(true);
		this.usePassword.isOn = false;
		this.passwordInput.text = string.Empty;
		this.createRoomUI.SetActive(false);
		this.szMeData.Clear();
		TipManager.GetInstance().HideTextTip();
		this.GetRoomList();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00065464 File Offset: 0x00063664
	public void UpdateOneUser(string sNick)
	{
		bool flag = RoomManage.GetInstance().PRoomData != null;
		if (flag)
		{
			this.RefreshPlayerList();
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_sName == sNick;
			if (flag2)
			{
				this.SetRoomInfo(0);
			}
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x000654B0 File Offset: 0x000636B0
	public void SetRoomInfo(int ncreate = 0)
	{
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData == null;
		if (!flag)
		{
			bool flag2 = proomData.m_sCreator == PlayerDataManager.getInstance().HUserData.m_sName;
			this.m_nRoomCreateState = ncreate;
			bool flag3 = flag2;
			if (flag3)
			{
				this.inviteABtn.SetActive(true);
				this.inviteBBtn.SetActive(true);
				this.startBtn.name = "start";
				this.startBtnTxt.text = Language.GetLang(6018, new object[0]);
				EventTriggerListener.Get(this.startBtn).SetSoundStr("sound_main_mouse", string.Empty);
			}
			else
			{
				this.inviteABtn.SetActive(false);
				this.inviteBBtn.SetActive(false);
				bool flag4 = this.szMeData.Count > 0 && (int)this.szMeData[4] == 1;
				if (flag4)
				{
					this.startBtn.name = "cancel";
					this.startBtnTxt.text = Language.GetLang(6019, new object[0]);
				}
				else
				{
					this.startBtn.name = "ready";
					this.startBtnTxt.text = Language.GetLang(6014, new object[0]);
				}
				EventTriggerListener.Get(this.startBtn).SetSoundStr("sound_main_mouse", "ui_room_ready");
			}
			DateTime serverDate = Fun.GetServerDate();
			for (int i = 0; i < this.szMapTab.Count; i++)
			{
				GameObject gameObject = this.szMapTab[i];
				int num = int.Parse(gameObject.name.Replace("MapTab", string.Empty));
				Toggle component = gameObject.GetComponent<Toggle>();
				Text component2 = gameObject.transform.Find("OpenTime").GetComponent<Text>();
				bool flag5 = ActivityManager.GetInstance().IsOpenLimit(num) == 0;
				if (flag5)
				{
					try
					{
						DateTime d = DateTime.Parse(ActivityManager.GetInstance().GetFbOpenTime(num));
						bool flag6 = (d - serverDate).TotalHours > 24.0;
						if (flag6)
						{
							component2.text = Language.GetLang(17022, new object[]
							{
								d.Month,
								d.Day
							});
						}
						else
						{
							component2.text = Language.GetLang(17021, new object[0]);
						}
						component.interactable = false;
					}
					catch
					{
						component2.text = string.Empty;
						component.interactable = true;
					}
				}
				else
				{
					component2.text = string.Empty;
					component.interactable = true;
				}
				bool flag7 = flag2;
				if (flag7)
				{
					component.targetGraphic.raycastTarget = true;
				}
				else
				{
					component.targetGraphic.raycastTarget = false;
				}
				bool flag8 = num == proomData.m_pRoomFbData.nType;
				if (flag8)
				{
					bool flag9 = !component.isOn;
					if (flag9)
					{
						component.isOn = true;
					}
					this.OnMapTabCheck(num, true);
				}
			}
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00065800 File Offset: 0x00063A00
	private void ResetMapList(int ntype)
	{
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData == null;
		if (!flag)
		{
			bool flag2 = proomData.m_sCreator == PlayerDataManager.getInstance().HUserData.m_sName;
			List<FbXmlData> roomMapData = RoomManage.GetInstance().GetRoomMapData(ntype);
			float num = -24f;
			float num2 = -24f;
			fUIManager.OnClearObj(this.szCreateRoomMap);
			for (int i = 0; i < roomMapData.Count; i++)
			{
				FbXmlData fbXmlData = roomMapData[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.createRoomMapRender);
				gameObject.transform.SetParent(this.createRoomMapRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.name = string.Format("MapRender{0}", fbXmlData.nId);
				gameObject.transform.Find("MapName").GetComponent<Text>().text = fbXmlData.sName;
				gameObject.transform.Find("Background").GetComponent<Image>().enabled = (i % 2 == 0);
				EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeMap);
				EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				gameObject.SetActive(true);
				this.szCreateRoomMap.Add(gameObject);
				Toggle component = gameObject.GetComponent<Toggle>();
				bool flag3 = flag2;
				if (flag3)
				{
					component.graphic.raycastTarget = true;
				}
				else
				{
					component.graphic.raycastTarget = false;
				}
				bool flag4 = fbXmlData.nId == proomData.m_pRoomFbData.nId;
				if (flag4)
				{
					num2 = num - (float)(i * 48);
					component.isOn = true;
					this.createRoomMapName.text = fbXmlData.sName;
					this.createRoomMapTypeSp.sprite = Window.spriteName(AtlasType.ATLAS_DATING, string.Format("findroom_type{0}", fbXmlData.nType));
					this.createRoomMapType.text = fbXmlData.GetModeStr();
					this.createRoomMapDesc.text = fbXmlData.sMapDesc;
					fUIManager.GetInstance().GetIconAsset(string.Format("mapshow/" + fbXmlData.sIcon + ".txt", new object[0]), this.createRoomMapSp);
				}
				else
				{
					component.isOn = false;
				}
			}
			float y = Mathf.Max(0f, Mathf.Abs(num2 - num) + 48f - 240f);
			bool flag5 = this.nCreateFbId != proomData.m_pRoomFbData.nId;
			if (flag5)
			{
				this.createMapList.content.anchoredPosition3D = new Vector3(0f, y, 0f);
			}
			bool flag6 = flag2;
			if (flag6)
			{
				this.createMapList.vertical = true;
				this.createMapList.verticalScrollbar.GetComponent<Image>().enabled = true;
				this.createMapList.verticalScrollbar.targetGraphic.enabled = true;
			}
			else
			{
				this.createMapList.verticalScrollbar.GetComponent<Image>().enabled = false;
				this.createMapList.verticalScrollbar.targetGraphic.enabled = false;
				this.createMapList.vertical = false;
			}
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00065B94 File Offset: 0x00063D94
	public void SetCreateRoom(int ncreate = 0)
	{
		this.chsGroupUI.SetActive(false);
		this.joinRoomUI.SetActive(false);
		this.createRoomUI.SetActive(true);
		this.RefreshPlayerList();
		this.SetRoomInfo(ncreate);
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00065BE4 File Offset: 0x00063DE4
	public void RefreshPlayerList()
	{
		fUIManager.OnClearObj(this.szRenderTeam);
		bool flag = RoomManage.GetInstance().PRoomData == null;
		if (!flag)
		{
			int preteam = 0;
			ArrayList userList = RoomManage.GetInstance().PRoomData.GetUserList();
			int num = userList.IndexOf(PlayerDataManager.getInstance().HUserData.m_sName);
			int myteam = (num >= 10) ? 1 : 0;
			for (int i = 0; i < userList.Count; i += 2)
			{
				int num2 = i / 10;
				string value = userList[i].ToString();
				bool flag2 = string.IsNullOrEmpty(value);
				GameObject gameObject;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.roomAddRender);
					bool flag3 = num2 == 0;
					if (flag3)
					{
						gameObject.transform.SetParent(this.createRoomTeam1.transform);
					}
					else
					{
						gameObject.transform.SetParent(this.createRoomTeam2.transform);
					}
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = string.Format("Team{0}_{1}", num2, i);
					preteam = 0;
				}
				else
				{
					ArrayList arrayList = (ArrayList)userList[i + 1];
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.roomPlayerRender);
					bool flag4 = num2 == 0;
					if (flag4)
					{
						gameObject.transform.SetParent(this.createRoomTeam1.transform);
					}
					else
					{
						gameObject.transform.SetParent(this.createRoomTeam2.transform);
					}
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = string.Format("Team{0}_{1}", num2, i);
					this.SetTeamMember(gameObject, arrayList, num2, myteam, preteam);
					preteam = (int)arrayList[12];
				}
				gameObject.SetActive(true);
				this.szRenderTeam.Add(gameObject);
			}
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00065E35 File Offset: 0x00064035
	private void OverKickBtn(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(6023, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00065E63 File Offset: 0x00064063
	private void ExitKickBtn(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00065E71 File Offset: 0x00064071
	private void OnClickFuncBtn(int nteam, string sname)
	{
		this.OnClickKickUser(sname);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00065E7C File Offset: 0x0006407C
	private void OnClickCreateFuncBtn(GameObject go)
	{
		string[] array = go.name.Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[0]);
		string text = array[1];
		string a = text;
		if (!(a == "kickuser"))
		{
			if (a == "changeteam")
			{
				this.OnClickChangeTeam((num != 1) ? 1 : 0, -1);
			}
		}
		else
		{
			this.OnClickKickUser(array[2]);
		}
		fUIManager.OnClearObj(this.szCreateRoomFunc);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00065EF8 File Offset: 0x000640F8
	private void OnClickChangeTeam(GameObject go)
	{
		bool flag = go.name == "Button1";
		if (flag)
		{
			this.OnClickChangeTeam(0, -1);
		}
		bool flag2 = go.name == "Button2";
		if (flag2)
		{
			this.OnClickChangeTeam(1, -1);
		}
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00065F44 File Offset: 0x00064144
	private void OverChangeTeam(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(6024, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00065F72 File Offset: 0x00064172
	private void ExitChangeTeam(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00065F80 File Offset: 0x00064180
	private void OnClickChangeTeam(int toTeam, int toIdx = -1)
	{
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData == null;
		if (!flag)
		{
			ArrayList userList = proomData.GetUserList();
			string sName = PlayerDataManager.getInstance().HUserData.m_sName;
			int num = userList.IndexOf(sName);
			bool flag2 = num == -1;
			if (!flag2)
			{
				int num2 = 0;
				bool flag3 = TeamManager.GetInstance().m_nTeamID != 0;
				if (flag3)
				{
					bool flag4 = TeamManager.GetInstance().m_nCaptain != 1;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6035, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						return;
					}
					num2 = TeamManager.GetInstance().m_nTeamID;
				}
				bool flag5 = (num < 10 && toTeam == 0) || (num >= 10 && toTeam == 1);
				if (!flag5)
				{
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int i = 0;
					while (i < userList.Count)
					{
						bool flag6 = userList[i].ToString() == sName;
						if (flag6)
						{
							ArrayList arrayList = (ArrayList)userList[i + 1];
							bool flag7 = arrayList[0].ToString() != proomData.m_sCreator;
							if (flag7)
							{
								num3 = (int)arrayList[4];
							}
						}
						bool flag8 = toIdx != -1;
						if (flag8)
						{
							bool flag9 = toIdx == i && !string.IsNullOrEmpty(userList[i].ToString());
							if (flag9)
							{
								num4 = 1;
							}
						}
						else
						{
							bool flag10 = i >= toTeam * 10 && i < (toTeam + 1) * 10;
							if (flag10)
							{
								bool flag11 = !string.IsNullOrEmpty(userList[i].ToString());
								if (flag11)
								{
									num4++;
								}
							}
							else
							{
								bool flag12 = num2 == 0 || string.IsNullOrEmpty(userList[i].ToString());
								if (!flag12)
								{
									ArrayList arrayList2 = (ArrayList)userList[i + 1];
									bool flag13 = (int)arrayList2[12] == num2;
									if (flag13)
									{
										num5++;
										bool flag14 = (int)arrayList2[4] == 1 && arrayList2[0].ToString() != proomData.m_sCreator;
										if (flag14)
										{
											num3 = 1;
										}
									}
								}
							}
						}
						IL_271:
						i += 2;
						continue;
						goto IL_271;
					}
					bool flag15 = TeamManager.GetInstance().m_nTeamID == 0 && num3 == 1;
					if (flag15)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6026, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						bool flag16 = toIdx != -1 && num4 > 0;
						if (flag16)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						}
						else
						{
							bool flag17 = toIdx == -1 && num4 >= 5;
							if (flag17)
							{
								TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6025, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							}
							else
							{
								bool flag18 = num2 != 0 && 5 - num4 < num5;
								if (flag18)
								{
									ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(6034, new object[0]), "ffdd00", "system");
								}
								else
								{
									ArrayList arrayList3 = new ArrayList();
									arrayList3.Add(PlayerDataManager.getInstance().HUserData.m_nRoomUID);
									arrayList3.Add(toTeam);
									arrayList3.Add(toIdx);
									arrayList3.Add(num2);
									RoomManage.GetInstance().SendRoomMsgToServer(3318, arrayList3);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000663E4 File Offset: 0x000645E4
	private void OnClickKickUser(string sNick)
	{
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData != null;
		if (flag)
		{
			ArrayList userList = proomData.GetUserList();
			bool flag2 = !(PlayerDataManager.getInstance().HUserData.m_sName != proomData.m_sCreator) && userList.IndexOf(sNick) != -1;
			if (flag2)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(PlayerDataManager.getInstance().HUserData.m_nRoomUID);
				arrayList.Add(Fun.EncodingUTF8(sNick));
				RoomManage.GetInstance().SendRoomMsgToServer(3313, arrayList);
			}
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00066484 File Offset: 0x00064684
	private void OnClickAddRender(GameObject go)
	{
		string[] array = go.name.Replace("Team", string.Empty).Split(new char[]
		{
			'_'
		});
		this.OnClickChangeTeam(int.Parse(array[0]), int.Parse(array[1]));
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x000664CF File Offset: 0x000646CF
	private void OnClickAddBtn(int nteam, int nindex)
	{
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x000664D4 File Offset: 0x000646D4
	private void SetTeamMember(GameObject obj, ArrayList oneuser, int nteam, int myteam, int preteam)
	{
		string text = string.Empty;
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData == null || obj.transform.Find("DelBtn") == null;
		if (!flag)
		{
			bool flag2 = proomData.m_sCreator == PlayerDataManager.getInstance().HUserData.m_sName;
			bool flag3 = oneuser[0].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag3)
			{
				this.szMeData = oneuser;
			}
			text = ((TeamManager.GetInstance().m_nTeamID == 0 || TeamManager.GetInstance().m_nTeamID != (int)oneuser[12]) ? "ffffff" : "06ff00");
			Image component = obj.transform.Find("Team").GetComponent<Image>();
			bool flag4 = preteam != 0 && preteam == (int)oneuser[12];
			if (flag4)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
			RawImage component2 = obj.transform.Find("PlayerIcon/Image").GetComponent<RawImage>();
			GetIconComponent component3 = component2.transform.GetComponent<GetIconComponent>();
			bool flag5 = component3 != null;
			if (flag5)
			{
				component3._Url = string.Empty;
			}
			component2.enabled = false;
			bool flag6 = fGameManager.mloadConfig.AllFaceData.ContainsKey((int)oneuser[10]);
			if (flag6)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[(int)oneuser[10]];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component2);
			}
			Text component4 = obj.transform.Find("PlayerName").GetComponent<Text>();
			component4.text = string.Concat(new string[]
			{
				"<color=#",
				text,
				">",
				oneuser[0].ToString(),
				"</color>"
			});
			component = obj.transform.Find("Leader").GetComponent<Image>();
			component.enabled = (oneuser[0].ToString() == proomData.m_sCreator);
			GameObject gameObject = obj.transform.Find("Image").gameObject;
			component4 = gameObject.transform.Find("Text").GetComponent<Text>();
			bool flag7 = (int)oneuser[4] == 0;
			if (flag7)
			{
				gameObject.SetActive(false);
			}
			else
			{
				gameObject.SetActive(true);
				component4.text = Language.GetLang(6014, new object[0]);
			}
			Button component5 = obj.transform.Find("DelBtn").GetComponent<Button>();
			component5.onClick.RemoveAllListeners();
			bool flag8 = proomData.m_nRoomState == 0 && flag2 && oneuser[0].ToString() != PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag8)
			{
				component5.gameObject.SetActive(true);
				component5.onClick.AddListener(delegate()
				{
					this.OnClickFuncBtn(nteam, oneuser[0].ToString());
				});
				EventTriggerListener.Get(component5.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverKickBtn);
				EventTriggerListener.Get(component5.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitKickBtn);
				EventTriggerListener.Get(component5.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			}
			else
			{
				component5.gameObject.SetActive(false);
				EventTriggerListener.Get(component5.gameObject).onEnter = null;
				EventTriggerListener.Get(component5.gameObject).onExit = null;
				EventTriggerListener.Get(component5.gameObject).SetSoundStr(string.Empty, string.Empty);
			}
			EventTriggerListener.Get(obj).onDown = new EventTriggerListener.VoidDelegate(this.OnClickTeamPlayer);
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00066928 File Offset: 0x00064B28
	private void OnClickTeamPlayer(GameObject go)
	{
		string text = go.transform.Find("PlayerName").GetComponent<Text>().text;
		text = Regex.Replace(text, "<[^>]+>", string.Empty, RegexOptions.IgnoreCase);
		bool flag = !(text == PlayerDataManager.getInstance().HUserData.m_sName);
		if (flag)
		{
			UserData userData = RoomManage.GetInstance().GetUserData(text);
			bool flag2 = Input.GetMouseButtonDown(1) && userData != null;
			if (flag2)
			{
				ChatManager.GetInstance().OnMsgTip(userData.m_sNick, 1, null, userData.m_nCid);
			}
		}
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x000669BC File Offset: 0x00064BBC
	private void OnClickRoomStart(GameObject go)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		RoomData proomData = RoomManage.GetInstance().PRoomData;
		bool flag = proomData == null;
		if (!flag)
		{
			bool flag2 = (proomData.m_nRoomState > 2 || proomData.m_sCreator != PlayerDataManager.getInstance().HUserData.m_sName) && TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40031, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag3 = proomData.m_sCreator == PlayerDataManager.getInstance().HUserData.m_sName || proomData.m_nRoomState > 2;
				if (flag3)
				{
					bool flag4 = RoomManage.GetInstance().szInvited.Count > 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						this.OnReadyState(1);
						arrayList2.Add(proomData.m_nRoomUID);
						arrayList2.Add(proomData.m_pRoomFbData.nId);
						RoomManage.GetInstance().SendRoomMsgToServer(3320, arrayList2);
						this.startBtn.GetComponent<Button>().interactable = false;
						this.exitBtn.GetComponent<Button>().interactable = false;
					}
				}
				else
				{
					arrayList2.Add(1);
					arrayList2.Add(0);
					arrayList.Add(proomData.m_nRoomUID);
					arrayList.Add(4);
					arrayList.Add(arrayList2);
					RoomManage.GetInstance().SendRoomMsgToServer(3308, arrayList);
				}
			}
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00066BB8 File Offset: 0x00064DB8
	public void SetStartBtn()
	{
		this.startBtn.GetComponent<Button>().interactable = true;
		this.exitBtn.GetComponent<Button>().interactable = true;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00066BE0 File Offset: 0x00064DE0
	private void OnReadyState(int nState)
	{
		this.m_nReadyState = nState;
		base.CancelInvoke("OnCheckReady");
		bool flag = this.m_nReadyState == 0 && PlayerDataManager.getInstance().HUserData.m_sName != RoomManage.GetInstance().PRoomData.m_sCreator;
		if (flag)
		{
			this.m_nReadyTime = 60;
			base.InvokeRepeating("OnCheckReady", 1f, 1f);
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00066C53 File Offset: 0x00064E53
	private void OnClickExitRoom(GameObject go)
	{
		RoomManage.GetInstance().OnLeaveRoom();
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00066C64 File Offset: 0x00064E64
	private void OnClickRoomInvite(GameObject go)
	{
		int nteam = int.Parse(go.name.Replace("Invite", string.Empty));
		fUIManager.GetInstance().ShowInviteUI(1, nteam, null);
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00066C9B File Offset: 0x00064E9B
	private void OverRoomInvite(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(6016, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00066CC9 File Offset: 0x00064EC9
	private void ExitRoomInvite(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00066CD7 File Offset: 0x00064ED7
	public override void Close()
	{
		base.Close();
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00066CEC File Offset: 0x00064EEC
	private void OnClickJoinRoom(GameObject go)
	{
		bool flag = this.nowSlcRoomUId != 0;
		if (flag)
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().Dyaoqing.Count > 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = !string.IsNullOrEmpty(this.nowSlcRoomCode);
					if (flag4)
					{
						TipManager.GetInstance().ShowInputTip(Language.GetLang(6048, new object[0]), string.Empty, Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), new TipManager.TipInputDelegate(this.OnInputCodeOk), null, null, null, 0, 272f, 36f);
					}
					else
					{
						this.OnInputCodeOk(null, this.nowSlcRoomCode);
					}
				}
			}
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00066E54 File Offset: 0x00065054
	private void OnInputCodeOk(object szParam, string sinput)
	{
		bool flag = this.nowSlcRoomCode != sinput;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40000, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.GoOnJoinRoom), sinput, null, null);
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().lookingTeamId != 0 && TeamManager.GetInstance().m_nCaptain == 1;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6042, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.GoOnJoinRoom), sinput, null, null);
				}
				else
				{
					this.GoOnJoinRoom(sinput);
				}
			}
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00066FD8 File Offset: 0x000651D8
	public void GoOnJoinRoom(object szParam)
	{
		bool flag = TeamManager.GetInstance().LoadTeamOK && fGameManager.LobbyPlayer != null;
		if (flag)
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
			if (flag2)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			bool flag3 = TeamManager.GetInstance().lookingTeamId != 0 && TeamManager.GetInstance().m_nCaptain == 1;
			if (flag3)
			{
				TeamManager.GetInstance().LeaveLookingTeam(8, 0, 0, 0);
			}
			base.CancelInvoke("OnCheckReady");
			this.m_nReadyTime = 60;
			base.InvokeRepeating("OnCheckReady", 1f, 1f);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(szParam.ToString());
			arrayList.Add(this.nowSlcRoomUId);
			arrayList.Add(this.nJoinFbId);
			arrayList.Add(this.nGroupType);
			arrayList.Add(0);
			RoomManage.GetInstance().PRoomData = null;
			RoomManage.GetInstance().SendRoomMsgToServer(3303, arrayList);
		}
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00067104 File Offset: 0x00065304
	private void OnCheckReady()
	{
		bool flag = this.m_nReadyState != 0;
		if (flag)
		{
			base.CancelInvoke("OnCheckReady");
		}
		else
		{
			this.m_nReadyTime--;
			bool flag2 = this.m_nReadyTime <= 0 && this.m_nReadyState == 0;
			if (flag2)
			{
				base.CancelInvoke("OnCheckReady");
				this.CheckUserReady();
			}
		}
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0006716C File Offset: 0x0006536C
	private void CheckUserReady()
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0;
		if (flag)
		{
			this.m_nReadyTime = 60;
		}
		else
		{
			bool flag2 = RoomManage.GetInstance().PRoomData == null;
			if (!flag2)
			{
				ArrayList userList = MatchManager.GetInstance().GetUserList();
				string sName = PlayerDataManager.getInstance().HUserData.m_sName;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < userList.Count; i += 2)
				{
					bool flag3 = userList[i].ToString() == sName;
					if (flag3)
					{
						ArrayList arrayList = (ArrayList)userList[i + 1];
						num = (int)arrayList[4];
					}
				}
				bool flag4 = num == 0;
				if (flag4)
				{
					bool flag5 = PlayerDataManager.getInstance().HUserData.m_sName == RoomManage.GetInstance().PRoomData.m_sCreator;
					if (flag5)
					{
						this.m_nReadyTime = 60;
					}
					else
					{
						bool flag6 = num2 != 0;
						if (flag6)
						{
							this.m_nReadyTime = 60;
						}
						else
						{
							this.m_nReadyTime = 60;
							RoomManage.GetInstance().OnLeave();
						}
					}
				}
			}
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00067298 File Offset: 0x00065498
	private void OnClickCreateRoom(GameObject go)
	{
		string text = string.Empty;
		bool isOn = this.usePassword.isOn;
		if (isOn)
		{
			text = this.passwordInput.text;
			bool flag = text.Length > 6;
			if (flag)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6047, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			}
			int num;
			bool flag2 = !int.TryParse(text, out num);
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6047, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			}
		}
		RoomManage.GetInstance().OnCreateRoom(text, this.nGroupType);
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00067394 File Offset: 0x00065594
	private void InitMapName()
	{
		DateTime serverDate = Fun.GetServerDate();
		List<int> allRoomMapType = RoomManage.GetInstance().GetAllRoomMapType();
		for (int i = 0; i < allRoomMapType.Count; i++)
		{
			int num = allRoomMapType[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mapTabRender);
			gameObject.transform.SetParent(this.mapTabRender.transform.parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.name = string.Format("MapTab{0}", num);
			gameObject.SetActive(true);
			Image component = gameObject.transform.Find("Icon").GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_DATING, this.GetMapTypeIcon(num));
			Text component2 = gameObject.transform.Find("Label").GetComponent<Text>();
			component2.text = this.GetMapTypeName(num);
			Toggle component3 = gameObject.GetComponent<Toggle>();
			EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMapTabCheck);
			component2 = gameObject.transform.Find("OpenTime").GetComponent<Text>();
			bool flag = ActivityManager.GetInstance().IsOpenLimit(num) == 0;
			if (flag)
			{
				try
				{
					DateTime d = DateTime.Parse(ActivityManager.GetInstance().GetFbOpenTime(num));
					bool flag2 = (d - serverDate).TotalHours > 24.0;
					if (flag2)
					{
						component2.text = Language.GetLang(17022, new object[]
						{
							d.Month,
							d.Day
						});
					}
					else
					{
						component2.text = Language.GetLang(17021, new object[0]);
					}
					component3.interactable = false;
				}
				catch
				{
					component2.text = string.Empty;
					component3.interactable = true;
				}
			}
			else
			{
				component2.text = string.Empty;
				component3.interactable = true;
			}
			this.szMapTab.Add(gameObject);
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x000675F8 File Offset: 0x000657F8
	private void OnChangeMap(GameObject go)
	{
		Toggle component = go.GetComponent<Toggle>();
		component.isOn = true;
		int num = int.Parse(go.name.Replace("MapRender", string.Empty));
		FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[num];
		bool flag = fbXmlData != null;
		if (flag)
		{
			this.createRoomMapName.text = fbXmlData.sName;
			this.createRoomMapTypeSp.sprite = Window.spriteName(AtlasType.ATLAS_DATING, string.Format("findroom_type{0}", fbXmlData.nType));
			this.createRoomMapType.text = fbXmlData.GetModeStr();
			this.createRoomMapDesc.text = fbXmlData.sMapDesc;
			fUIManager.GetInstance().GetIconAsset(string.Format("mapshow/" + fbXmlData.sIcon + ".txt", new object[0]), this.createRoomMapSp);
		}
		bool flag2 = this.nCreateFbId == num;
		if (!flag2)
		{
			RoomData proomData = RoomManage.GetInstance().PRoomData;
			bool flag3 = proomData != null && !string.IsNullOrEmpty(proomData.m_sCreator) && !(proomData.m_sCreator != PlayerDataManager.getInstance().HUserData.m_sName);
			if (flag3)
			{
				this.nCreateFbId = num;
				bool flag4 = proomData.m_nRoomUID != 0;
				if (flag4)
				{
					ArrayList value = new ArrayList
					{
						4,
						this.nCreateFbId,
						5,
						fbXmlData.nType
					};
					ArrayList arr = new ArrayList
					{
						proomData.m_nRoomUID,
						proomData.GetRoomInfo(3).ToString(),
						value
					};
					RoomManage.GetInstance().SendRoomMsgToServer(3324, arr);
				}
			}
		}
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x000677F4 File Offset: 0x000659F4
	private void LoadIconOk(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			RawImage rawImage = (RawImage)customParam;
			bool flag2 = !(rawImage == null);
			if (flag2)
			{
				bool flag3 = rawImage.texture == null;
				if (flag3)
				{
					rawImage.texture = new Texture2D(4, 4);
					www.LoadImageIntoTexture((Texture2D)rawImage.texture);
				}
				rawImage.enabled = true;
			}
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0006786C File Offset: 0x00065A6C
	public void GetRoomList()
	{
		bool flag = this.nGroupType == 0 && MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(0, null);
		}
		RoomManage.GetInstance().SendRoomMsgToServer(3306, new ArrayList
		{
			this.nPageIdx,
			this.nPageNum,
			this.nGroupType
		});
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x000678EE File Offset: 0x00065AEE
	private void OnSortClick(GameObject go)
	{
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x000678F1 File Offset: 0x00065AF1
	public void ShowRoomList()
	{
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000678F4 File Offset: 0x00065AF4
	private void ClearOneRoom(GameObject obj)
	{
		Text component = obj.transform.Find("Num").GetComponent<Text>();
		component.text = string.Empty;
		component = obj.transform.Find("RoomName").GetComponent<Text>();
		component.text = string.Empty;
		component = obj.transform.Find("MapName").GetComponent<Text>();
		component.text = string.Empty;
		component = obj.transform.Find("RoomType").GetComponent<Text>();
		component.text = string.Empty;
		component = obj.transform.Find("PlayerNum").GetComponent<Text>();
		component.text = string.Empty;
		component = obj.transform.Find("RoomState").GetComponent<Text>();
		component.text = string.Empty;
		Image component2 = obj.transform.Find("Password").GetComponent<Image>();
		component2.enabled = false;
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000679EC File Offset: 0x00065BEC
	private void OnSelectRoom(GameObject go)
	{
		string[] array = go.name.Replace("JoinRoom", string.Empty).Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[0]);
		ArrayList arrayList = new ArrayList();
		Color color = new Color(0.77254903f, 0.83137256f, 0.92156863f);
		int i = 0;
		while (i < this.nPageNum)
		{
			GameObject gameObject = this.szJoinRoomRender[i];
			Toggle component = gameObject.transform.GetComponent<Toggle>();
			bool flag = i == num;
			if (flag)
			{
				component.isOn = true;
				bool flag2 = num < this.szShowRoom.Count;
				if (flag2)
				{
					arrayList = (ArrayList)(this.szShowRoom[num] as ArrayList).Clone();
				}
				bool flag3 = arrayList.Count > 0;
				if (flag3)
				{
					int key = (int)arrayList[4];
					bool flag4 = !fGameManager.mloadConfig.FbData.ContainsKey(key);
					if (flag4)
					{
						Debug.Log("找策划处理，房间列表刷新时没找到副本对应的配表数据，fbid:" + key.ToString());
					}
					else
					{
						FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[key];
						this.nowSlcRoomUId = (int)arrayList[17];
						this.nowSlcRoomCode = arrayList[3].ToString();
						this.nJoinFbId = key;
						Text component2 = gameObject.transform.Find("Num").GetComponent<Text>();
						component2.color = Color.white;
						component2 = gameObject.transform.Find("RoomName").GetComponent<Text>();
						component2.color = Color.white;
						component2 = gameObject.transform.Find("MapName").GetComponent<Text>();
						component2.color = Color.white;
						component2 = gameObject.transform.Find("RoomType").GetComponent<Text>();
						component2.color = Color.white;
						this.joinRoomName.text = Language.GetLang(6021, new object[]
						{
							Fun.DecodingUTF8(arrayList[1].ToString())
						});
						this.joinRoomMapTypeSp.enabled = true;
						this.joinRoomMapTypeSp.sprite = Window.spriteName(AtlasType.ATLAS_DATING, string.Format("findroom_type{0}", fbXmlData.nType));
						this.joinRoomMapType.text = fbXmlData.GetModeStr();
						fUIManager.GetInstance().GetIconAsset("mapshow/" + fbXmlData.sIcon + ".txt", this.joinRoomMapSp);
					}
				}
				else
				{
					this.nowSlcRoomUId = 0;
					this.nowSlcRoomCode = string.Empty;
					this.nJoinFbId = 0;
					this.joinRoomName.text = string.Empty;
					this.joinRoomMapTypeSp.enabled = false;
					this.joinRoomMapType.text = string.Empty;
					this.joinRoomMapSp.enabled = false;
				}
			}
			else
			{
				component.isOn = false;
				Text component3 = gameObject.transform.Find("Num").GetComponent<Text>();
				component3.color = color;
				component3 = gameObject.transform.Find("RoomName").GetComponent<Text>();
				component3.color = color;
				component3 = gameObject.transform.Find("MapName").GetComponent<Text>();
				component3.color = color;
				component3 = gameObject.transform.Find("RoomType").GetComponent<Text>();
				component3.color = color;
			}
			IL_37B:
			i++;
			continue;
			goto IL_37B;
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x00067D90 File Offset: 0x00065F90
	private void OnClickListPage(GameObject go)
	{
		bool flag = Time.time - this.pageDelay < 1f;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = go.name == "PreBtn";
			if (flag2)
			{
				this.nPageIdx--;
			}
			bool flag3 = go.name == "NextBtn";
			if (flag3)
			{
				this.nPageIdx++;
			}
			bool flag4 = this.nPageIdx < 0;
			if (flag4)
			{
				this.nPageIdx = 0;
			}
			bool flag5 = this.nPageIdx > this.nMaxPage - 1;
			if (flag5)
			{
				this.nPageIdx = this.nMaxPage - 1;
			}
			this.pageDelay = Time.time;
			this.GetRoomList();
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00067E78 File Offset: 0x00066078
	public void ResetRoomNum(int allnum, int pageidx)
	{
		this.nPageIdx = pageidx;
		int num = allnum % this.nPageNum;
		bool flag = num > 0;
		if (flag)
		{
			this.nMaxPage = allnum / this.nPageNum + 1;
		}
		else
		{
			this.nMaxPage = allnum / this.nPageNum;
		}
		bool flag2 = this.nMaxPage == 0;
		if (flag2)
		{
			this.nMaxPage = 1;
		}
		bool flag3 = this.nPageIdx > this.nMaxPage - 1;
		if (flag3)
		{
			this.nPageIdx = this.nMaxPage - 1;
		}
		bool flag4 = this.nPageIdx == 0;
		if (flag4)
		{
			this.pagePreBtn.interactable = false;
		}
		else
		{
			this.pagePreBtn.interactable = true;
		}
		bool flag5 = this.nPageIdx == this.nMaxPage - 1;
		if (flag5)
		{
			this.pageNextBtn.interactable = false;
		}
		else
		{
			this.pageNextBtn.interactable = true;
		}
		this.pageNumLb.text = string.Format("{0}/{1}", this.nPageIdx + 1, this.nMaxPage);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00067F90 File Offset: 0x00066190
	private void InitRoomList()
	{
		for (int i = 0; i < this.nPageNum; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.joinRoomRender);
			gameObject.transform.SetParent(this.joinRoomRender.transform.parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			gameObject.name = string.Format("JoinRoom{0}", i);
			Image component = gameObject.transform.Find("Background").GetComponent<Image>();
			component.enabled = (i % 2 == 0);
			EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectRoom);
			EventTriggerListener.Get(gameObject).onDoubleClick = new EventTriggerListener.VoidDelegate(this.OnClickJoinRoom);
			EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			this.szJoinRoomRender.Add(gameObject);
		}
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x000680B0 File Offset: 0x000662B0
	public void ShowRoomList(ArrayList szRoom)
	{
		bool flag = this.szJoinRoomRender.Count == 0;
		if (!flag)
		{
			int num = -1;
			ArrayList arrayList = new ArrayList();
			Color color = new Color(0.77254903f, 0.83137256f, 0.92156863f);
			this.szShowRoom = (ArrayList)szRoom.Clone();
			this.szShowRoom.Sort(new FindRoomCon.RoomCompare());
			int i = 0;
			while (i < this.nPageNum)
			{
				GameObject gameObject = this.szJoinRoomRender[i];
				gameObject.name = string.Format("JoinRoom{0}", i);
				arrayList.Clear();
				bool flag2 = i < this.szShowRoom.Count;
				if (flag2)
				{
					arrayList = (ArrayList)(this.szShowRoom[i] as ArrayList).Clone();
				}
				bool flag3 = arrayList.Count > 0;
				if (flag3)
				{
					int num2 = i;
					int key = (int)arrayList[4];
					bool flag4 = !fGameManager.mloadConfig.FbData.ContainsKey(key);
					if (flag4)
					{
						Debug.Log("找策划处理，房间列表刷新时没找到副本对应的配表数据，fbid:" + key.ToString());
					}
					else
					{
						FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[key];
						gameObject.name = string.Format("JoinRoom{0}_{1}", num2, arrayList[17]);
						bool flag5 = (int)arrayList[17] == this.nowSlcRoomUId;
						if (flag5)
						{
							num = num2;
						}
						Toggle component = gameObject.GetComponent<Toggle>();
						Text component2 = gameObject.transform.Find("Num").GetComponent<Text>();
						component2.text = arrayList[0].ToString();
						component2 = gameObject.transform.Find("RoomName").GetComponent<Text>();
						component2.text = Language.GetLang(6021, new object[]
						{
							Fun.DecodingUTF8(arrayList[1].ToString())
						});
						bool isOn = component.isOn;
						if (isOn)
						{
							component2.color = Color.white;
						}
						else
						{
							component2.color = color;
						}
						component2 = gameObject.transform.Find("MapName").GetComponent<Text>();
						component2.text = fbXmlData.sName;
						bool isOn2 = component.isOn;
						if (isOn2)
						{
							component2.color = Color.white;
						}
						else
						{
							component2.color = color;
						}
						component2 = gameObject.transform.Find("RoomType").GetComponent<Text>();
						component2.text = fbXmlData.GetModeStr();
						bool isOn3 = component.isOn;
						if (isOn3)
						{
							component2.color = Color.white;
						}
						else
						{
							component2.color = color;
						}
						component2 = gameObject.transform.Find("PlayerNum").GetComponent<Text>();
						component2.text = string.Format("{0}/{1}", arrayList[11], arrayList[7]);
						component2 = gameObject.transform.Find("RoomState").GetComponent<Text>();
						component2.text = this.GetStateStr((int)arrayList[13]);
						Image component3 = gameObject.transform.Find("Password").GetComponent<Image>();
						component3.enabled = !string.IsNullOrEmpty(arrayList[3].ToString());
					}
				}
				else
				{
					this.ClearOneRoom(gameObject);
				}
				IL_366:
				i++;
				continue;
				goto IL_366;
			}
			bool flag6 = num == -1;
			if (flag6)
			{
				num = 0;
			}
			this.OnSelectRoom(this.szJoinRoomRender[num]);
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00068460 File Offset: 0x00066660
	private string GetStateStr(int nstate)
	{
		string result = string.Empty;
		switch (nstate)
		{
		case 0:
			result = Language.GetLang(8006, new object[0]);
			break;
		case 1:
		case 2:
		case 3:
			result = Language.GetLang(8007, new object[0]);
			break;
		case 4:
			result = Language.GetLang(8008, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x000684D4 File Offset: 0x000666D4
	public void SetGroupData()
	{
		this.chsGroupUI.SetActive(true);
		this.joinRoomUI.SetActive(false);
		this.createRoomUI.SetActive(false);
		int nLevel = PlayerDataManager.getInstance().HUserData.m_nLevel;
		int nGroupLvl = RoomManage.GetInstance().nGroupLvl1;
		int nGroupLvl2 = RoomManage.GetInstance().nGroupLvl2;
		bool flag = nLevel > 0 && nLevel <= nGroupLvl;
		if (flag)
		{
			this.groupBtn1.interactable = true;
			this.groupBtn1.transform.Find("LockSp").GetComponent<Image>().enabled = false;
		}
		else
		{
			this.groupBtn1.interactable = false;
			this.groupBtn1.transform.Find("LockSp").GetComponent<Image>().enabled = true;
		}
		bool flag2 = nLevel > nGroupLvl && nLevel <= nGroupLvl2;
		if (flag2)
		{
			this.groupBtn2.interactable = true;
			this.groupBtn2.transform.Find("LockSp").GetComponent<Image>().enabled = false;
		}
		else
		{
			this.groupBtn2.interactable = false;
			this.groupBtn2.transform.Find("LockSp").GetComponent<Image>().enabled = true;
		}
		bool flag3 = nLevel > nGroupLvl2;
		if (flag3)
		{
			this.groupBtn3.interactable = true;
			this.groupBtn3.transform.Find("LockSp").GetComponent<Image>().enabled = false;
		}
		else
		{
			this.groupBtn3.interactable = false;
			this.groupBtn3.transform.Find("LockSp").GetComponent<Image>().enabled = true;
		}
		this.groupTxt1.text = Language.GetLang(6032, new object[]
		{
			1,
			nGroupLvl
		});
		this.groupTxt2.text = Language.GetLang(6032, new object[]
		{
			nGroupLvl + 1,
			nGroupLvl2
		});
		this.groupTxt3.text = Language.GetLang(6032, new object[]
		{
			nGroupLvl2 + 1,
			"max"
		});
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00068714 File Offset: 0x00066914
	private void OnClickGroup(GameObject go)
	{
		this.nPageIdx = 0;
		this.nMaxPage = 1;
		this.pageDelay = 0f;
		this.szShowRoom.Clear();
		this.nGroupType = int.Parse(go.name.Replace("Group", string.Empty));
		this.joinRoomUI.SetActive(true);
		this.usePassword.isOn = false;
		this.passwordInput.text = string.Empty;
		this.chsGroupUI.SetActive(false);
		this.createRoomUI.SetActive(false);
		this.GetRoomList();
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x000687B3 File Offset: 0x000669B3
	private void OnOverGroup(GameObject go)
	{
		go.transform.Find("SlcSp").GetComponent<Image>().enabled = true;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x000687D2 File Offset: 0x000669D2
	private void OnExitGroup(GameObject go)
	{
		go.transform.Find("SlcSp").GetComponent<Image>().enabled = false;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x000687F4 File Offset: 0x000669F4
	private string GetMapTypeIcon(int nType)
	{
		string result = "findroom_zhengduo1";
		switch (nType)
		{
		case 2:
			result = "findroom_tuiche1";
			break;
		case 3:
			result = "findroom_zhengduo1";
			break;
		case 4:
			result = "findroom_jingji1";
			break;
		case 5:
			result = "findroom_ziyuanzhan1";
			break;
		case 7:
			result = "findroom_sidou1";
			break;
		case 11:
			result = "findroom_geren1";
			break;
		}
		return result;
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00068874 File Offset: 0x00066A74
	public string GetMapTypeName(int nType)
	{
		string result = string.Empty;
		switch (nType)
		{
		case 1:
			result = Language.GetLang(8001, new object[0]);
			break;
		case 2:
			result = Language.GetLang(8002, new object[0]);
			break;
		case 3:
			result = Language.GetLang(8003, new object[0]);
			break;
		case 4:
			result = Language.GetLang(8004, new object[0]);
			break;
		case 5:
			result = Language.GetLang(8005, new object[0]);
			break;
		case 7:
			result = Language.GetLang(8024, new object[0]);
			break;
		case 11:
			result = Language.GetLang(8032, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00068950 File Offset: 0x00066B50
	private void OnMapTabCheck(GameObject go)
	{
		int num = int.Parse(go.name.Replace("MapTab", string.Empty));
		bool flag = ActivityManager.GetInstance().IsOpenLimit(num) == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			List<FbXmlData> roomMapData = RoomManage.GetInstance().GetRoomMapData(num);
			RoomData proomData = RoomManage.GetInstance().PRoomData;
			bool flag2 = proomData != null && !string.IsNullOrEmpty(proomData.m_sCreator) && !(proomData.m_sCreator != PlayerDataManager.getInstance().HUserData.m_sName) && proomData.m_pRoomFbData.nType != num;
			if (flag2)
			{
				roomMapData = RoomManage.GetInstance().GetRoomMapData(num);
				bool flag3 = proomData.m_nRoomUID != 0;
				if (flag3)
				{
					ArrayList value = new ArrayList
					{
						4,
						roomMapData[0].nId,
						5,
						roomMapData[0].nType
					};
					ArrayList arr = new ArrayList
					{
						proomData.m_nRoomUID,
						proomData.GetRoomInfo(3).ToString(),
						value
					};
					RoomManage.GetInstance().SendRoomMsgToServer(3324, arr);
				}
			}
		}
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00068B04 File Offset: 0x00066D04
	private void OnMapTabCheck(int maptype, bool value)
	{
		for (int i = 0; i < this.szMapTab.Count; i++)
		{
			GameObject gameObject = this.szMapTab[i];
			int num = int.Parse(gameObject.name.Replace("MapTab", string.Empty));
			Image component = gameObject.transform.Find("Icon").GetComponent<Image>();
			Text component2 = gameObject.transform.Find("Label").GetComponent<Text>();
			string mapTypeIcon = this.GetMapTypeIcon(num);
			bool flag = num == maptype && value;
			if (flag)
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_DATING, mapTypeIcon.Remove(mapTypeIcon.Length - 1, 1));
				component2.color = Color.white;
				this.ResetMapList(maptype);
			}
			else
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_DATING, mapTypeIcon);
				component2.color = Color.gray;
			}
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00068C00 File Offset: 0x00066E00
	private void OnInputChange(string sstr)
	{
		bool flag = sstr.Contains("-");
		if (flag)
		{
			this.passwordInput.text = sstr.Replace("-", string.Empty);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00068C3B File Offset: 0x00066E3B
	public void SetRoomBtn(bool state)
	{
		this.inviteBtn.GetComponent<Button>().interactable = state;
		this.startBtn.GetComponent<Button>().interactable = state;
		this.exitBtn.GetComponent<Button>().interactable = state;
	}

	// Token: 0x04000944 RID: 2372
	public static FindRoomCon inst;

	// Token: 0x04000945 RID: 2373
	private Text titleTxt;

	// Token: 0x04000946 RID: 2374
	public GameObject joinRoomUI;

	// Token: 0x04000947 RID: 2375
	private RawImage joinRoomMapSp;

	// Token: 0x04000948 RID: 2376
	private Text joinRoomName;

	// Token: 0x04000949 RID: 2377
	private Image joinRoomMapTypeSp;

	// Token: 0x0400094A RID: 2378
	private Text joinRoomMapType;

	// Token: 0x0400094B RID: 2379
	private RectTransform joinListRect;

	// Token: 0x0400094C RID: 2380
	private GameObject joinRoomRender;

	// Token: 0x0400094D RID: 2381
	private List<GameObject> szJoinRoomRender = new List<GameObject>();

	// Token: 0x0400094E RID: 2382
	private Button pagePreBtn;

	// Token: 0x0400094F RID: 2383
	private Button pageNextBtn;

	// Token: 0x04000950 RID: 2384
	private Text pageNumLb;

	// Token: 0x04000951 RID: 2385
	private GameObject createRoomUI;

	// Token: 0x04000952 RID: 2386
	private RawImage createRoomMapSp;

	// Token: 0x04000953 RID: 2387
	private Image createRoomMapTypeSp;

	// Token: 0x04000954 RID: 2388
	private Text createRoomMapType;

	// Token: 0x04000955 RID: 2389
	private Text createRoomMapName;

	// Token: 0x04000956 RID: 2390
	private Text createRoomMapDesc;

	// Token: 0x04000957 RID: 2391
	private ScrollRect createMapList;

	// Token: 0x04000958 RID: 2392
	private GameObject createRoomMapRender;

	// Token: 0x04000959 RID: 2393
	private List<GameObject> szCreateRoomMap = new List<GameObject>();

	// Token: 0x0400095A RID: 2394
	private List<int> szCreateRoomId = new List<int>();

	// Token: 0x0400095B RID: 2395
	private GameObject createRoomTeam1;

	// Token: 0x0400095C RID: 2396
	private GameObject createRoomTeam2;

	// Token: 0x0400095D RID: 2397
	private List<GameObject> szRenderTeam = new List<GameObject>();

	// Token: 0x0400095E RID: 2398
	private GameObject roomPlayerRender;

	// Token: 0x0400095F RID: 2399
	private GameObject roomAddRender;

	// Token: 0x04000960 RID: 2400
	private List<GameObject> szCreateRoomFunc = new List<GameObject>();

	// Token: 0x04000961 RID: 2401
	private GameObject inviteBtn;

	// Token: 0x04000962 RID: 2402
	private GameObject inviteABtn;

	// Token: 0x04000963 RID: 2403
	private GameObject inviteBBtn;

	// Token: 0x04000964 RID: 2404
	private GameObject startBtn;

	// Token: 0x04000965 RID: 2405
	private Text startBtnTxt;

	// Token: 0x04000966 RID: 2406
	private GameObject exitBtn;

	// Token: 0x04000967 RID: 2407
	private GameObject chsGroupUI;

	// Token: 0x04000968 RID: 2408
	private Button groupBtn1;

	// Token: 0x04000969 RID: 2409
	private Text groupTxt1;

	// Token: 0x0400096A RID: 2410
	private Button groupBtn2;

	// Token: 0x0400096B RID: 2411
	private Text groupTxt2;

	// Token: 0x0400096C RID: 2412
	private Button groupBtn3;

	// Token: 0x0400096D RID: 2413
	private Text groupTxt3;

	// Token: 0x0400096E RID: 2414
	private GameObject mapTabRender;

	// Token: 0x0400096F RID: 2415
	private List<GameObject> szMapTab = new List<GameObject>();

	// Token: 0x04000970 RID: 2416
	private Toggle usePassword;

	// Token: 0x04000971 RID: 2417
	private InputField passwordInput;

	// Token: 0x04000972 RID: 2418
	private int nCreateFbId;

	// Token: 0x04000973 RID: 2419
	private int nJoinFbId;

	// Token: 0x04000974 RID: 2420
	private ArrayList szMeData = new ArrayList();

	// Token: 0x04000975 RID: 2421
	private int nowSlcRoomUId;

	// Token: 0x04000976 RID: 2422
	private string nowSlcRoomCode = string.Empty;

	// Token: 0x04000977 RID: 2423
	private int lastStartIdx;

	// Token: 0x04000978 RID: 2424
	private float refCount;

	// Token: 0x04000979 RID: 2425
	private int m_nReadyState;

	// Token: 0x0400097A RID: 2426
	private int m_nReadyTime = 60;

	// Token: 0x0400097B RID: 2427
	private int m_nRoomCreateState;

	// Token: 0x0400097C RID: 2428
	private int nPageIdx;

	// Token: 0x0400097D RID: 2429
	private int nMaxPage = 1;

	// Token: 0x0400097E RID: 2430
	private int nPageNum = 10;

	// Token: 0x0400097F RID: 2431
	private int nGroupType;

	// Token: 0x04000980 RID: 2432
	private float pageDelay;

	// Token: 0x04000981 RID: 2433
	private ArrayList szShowRoom = new ArrayList();

	// Token: 0x02000560 RID: 1376
	private class RoomCompare : IComparer
	{
		// Token: 0x060033E0 RID: 13280 RVA: 0x001BB724 File Offset: 0x001B9924
		public int Compare(object x, object y)
		{
			ArrayList arrayList = (ArrayList)x;
			ArrayList arrayList2 = (ArrayList)y;
			bool flag = arrayList.Count == 0 || arrayList2.Count == 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = ((int)arrayList[11]).CompareTo((int)arrayList2[11]);
			}
			return result;
		}
	}
}
