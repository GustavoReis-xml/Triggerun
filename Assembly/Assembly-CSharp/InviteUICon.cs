using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FA RID: 506
public class InviteUICon : Window
{
	// Token: 0x0600162D RID: 5677 RVA: 0x000CE13C File Offset: 0x000CC33C
	protected override void Awake()
	{
		base.Awake();
		this.uiTitle = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.userRender = this.UIGameObj.Get_GameObj(2);
		this.userRender.SetActive(false);
		this.pageUI = this.UIGameObj.Get_GameObj(7);
		this.preBtn = this.UIGameObj.Get_GameObj(4).GetComponent<Button>();
		EventTriggerListener.Get(this.preBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPageBtn);
		EventTriggerListener.Get(this.preBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.nextBtn = this.UIGameObj.Get_GameObj(5).GetComponent<Button>();
		EventTriggerListener.Get(this.nextBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickPageBtn);
		EventTriggerListener.Get(this.nextBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.pageTxt = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.inviteBtn = this.UIGameObj.Get_GameObj(3).GetComponent<Button>();
		EventTriggerListener.Get(this.inviteBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickInvite);
		EventTriggerListener.Get(this.inviteBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.uiBtnText = this.inviteBtn.transform.FindChild("Text").GetComponent<Text>();
		GameObject go = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		InviteUICon.inst = this;
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000CE318 File Offset: 0x000CC518
	private void Update()
	{
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x000CE31C File Offset: 0x000CC51C
	public void ShowListByInfo(int ntype, int nteam, object param)
	{
		this.nowType = ntype;
		this.nowTeam = nteam;
		this.openParam = param;
		this.ShowUserList();
		int num = this.nowType;
		int num2 = num;
		if (num2 - 1 > 1)
		{
			if (num2 - 3 <= 3)
			{
				this.uiTitle.text = Language.GetLang(21019, new object[0]);
				this.uiBtnText.text = Language.GetLang(21020, new object[0]);
			}
		}
		else
		{
			this.uiTitle.text = Language.GetLang(21001, new object[0]);
			this.uiBtnText.text = Language.GetLang(21006, new object[0]);
		}
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000CE3D4 File Offset: 0x000CC5D4
	public void RemoveInviteUser(string sName)
	{
		int num = this.szInvited.IndexOf(sName);
		bool flag = num != -1;
		if (flag)
		{
			this.szInvited.RemoveRange(num, 2);
		}
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000CE40C File Offset: 0x000CC60C
	public void ShowUserList()
	{
		ArrayList arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData.Clone();
		this.szFriend.Clear();
		this.szToggled.Clear();
		this.nAllNum = 0;
		this.curPageIdx = 0;
		for (int i = 0; i < arrayList.Count; i += 2)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i + 1];
			bool flag = (int)arrayList2[5] == 1;
			if (flag)
			{
				this.szFriend.Add((ArrayList)arrayList2.Clone());
				this.nAllNum++;
			}
		}
		int num = this.nAllNum % this.onePageNum;
		bool flag2 = num > 0;
		if (flag2)
		{
			this.maxPageNum = this.nAllNum / this.onePageNum + 1;
		}
		else
		{
			this.maxPageNum = this.nAllNum / this.onePageNum;
		}
		bool flag3 = this.maxPageNum == 0;
		if (flag3)
		{
			this.maxPageNum = 1;
		}
		bool flag4 = this.szFriend.Count <= 0;
		if (flag4)
		{
			this.pageUI.SetActive(false);
			this.inviteBtn.interactable = false;
		}
		else
		{
			this.pageUI.SetActive(true);
			this.inviteBtn.interactable = true;
		}
		this.SetPageUI();
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000CE574 File Offset: 0x000CC774
	private void OnClickPageBtn(GameObject go)
	{
		bool flag = go.name == "PreBtn";
		if (flag)
		{
			this.curPageIdx--;
		}
		bool flag2 = go.name == "NextBtn";
		if (flag2)
		{
			this.curPageIdx++;
		}
		this.SetPageUI();
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000CE5D4 File Offset: 0x000CC7D4
	private void SetPageUI()
	{
		bool flag = this.curPageIdx < 0;
		if (flag)
		{
			this.curPageIdx = 0;
		}
		bool flag2 = this.curPageIdx > this.maxPageNum - 1;
		if (flag2)
		{
			this.curPageIdx = this.maxPageNum - 1;
		}
		bool flag3 = this.curPageIdx == 0;
		if (flag3)
		{
			this.preBtn.interactable = false;
		}
		else
		{
			this.preBtn.interactable = true;
		}
		bool flag4 = this.curPageIdx == this.maxPageNum - 1;
		if (flag4)
		{
			this.nextBtn.interactable = false;
		}
		else
		{
			this.nextBtn.interactable = true;
		}
		this.pageTxt.text = string.Format("{0}/{1}", this.curPageIdx + 1, this.maxPageNum);
		this.ShowPageData();
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000CE6B4 File Offset: 0x000CC8B4
	public void ShowPageData()
	{
		for (int i = 0; i < this.onePageNum; i++)
		{
			bool flag = i < this.szRender.Count;
			GameObject gameObject;
			if (flag)
			{
				gameObject = this.szRender[i];
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.userRender);
				gameObject.transform.SetParent(this.userRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.SetActive(true);
				this.szRender.Add(gameObject);
			}
			gameObject.name = string.Format("UserRender{0}_{1}", i, 0);
			bool flag2 = this.curPageIdx * this.onePageNum + i < this.szFriend.Count;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)this.szFriend[this.curPageIdx * this.onePageNum + i];
				gameObject.name = string.Format("UserRender{0}_{1}", this.curPageIdx * this.onePageNum + i, arrayList[0].ToString());
				string text = arrayList[1].ToString();
				GameObject gameObject2 = gameObject.transform.Find("DefaultSp").gameObject;
				gameObject2.SetActive(false);
				gameObject2 = gameObject.transform.Find("UserRender").gameObject;
				gameObject2.SetActive(true);
				EventTriggerListener.Get(gameObject2).onDown = new EventTriggerListener.VoidDelegate(this.OnUserDown);
				RawImage component = gameObject2.transform.Find("UserIcon").GetComponent<RawImage>();
				component.enabled = false;
				bool flag3 = fGameManager.mloadConfig.AllFaceData.ContainsKey((int)arrayList[3]);
				if (flag3)
				{
					PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[(int)arrayList[3]];
					fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component);
				}
				Text component2 = gameObject2.transform.Find("NameUI/Nick").GetComponent<Text>();
				component2.text = text;
				component2 = gameObject2.transform.Find("LvlUI/Text").GetComponent<Text>();
				component2.text = arrayList[2].ToString();
				string userState = this.GetUserState(arrayList[1].ToString());
				Toggle component3 = gameObject2.transform.Find("Toggle").GetComponent<Toggle>();
				component2 = gameObject2.transform.Find("StateText").GetComponent<Text>();
				bool flag4 = string.IsNullOrEmpty(userState);
				if (flag4)
				{
					component3.gameObject.SetActive(true);
					bool flag5 = this.szToggled.Contains(arrayList[1].ToString());
					if (flag5)
					{
						component3.isOn = true;
					}
					else
					{
						component3.isOn = false;
					}
					component2.enabled = false;
					EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnUserTgClick);
				}
				else
				{
					component3.gameObject.SetActive(false);
					component2.enabled = true;
					component2.text = userState;
					EventTriggerListener.Get(component3.gameObject).onClick = null;
				}
			}
			else
			{
				GameObject gameObject3 = gameObject.transform.Find("DefaultSp").gameObject;
				gameObject3.SetActive(true);
				gameObject3 = gameObject.transform.Find("UserRender").gameObject;
				gameObject3.SetActive(false);
				EventTriggerListener.Get(gameObject3).onDown = null;
				Toggle component4 = gameObject3.transform.Find("Toggle").GetComponent<Toggle>();
				EventTriggerListener.Get(component4.gameObject).onClick = null;
			}
		}
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000CEABC File Offset: 0x000CCCBC
	private string GetUserState(string sname)
	{
		string result = string.Empty;
		int num = this.nowType;
		int num2 = num;
		if (num2 != 1)
		{
			if (num2 == 2)
			{
				bool flag = TeamManager.GetInstance().Dyaoqing.ContainsKey(sname);
				if (flag)
				{
					result = Language.GetLang(21015, new object[0]);
				}
				else
				{
					bool flag2 = TeamManager.GetInstance().MyTeamData.Contains(sname);
					if (flag2)
					{
						result = Language.GetLang(21017, new object[0]);
					}
				}
			}
		}
		else
		{
			bool flag3 = RoomManage.GetInstance().szInvited.Contains(sname);
			if (flag3)
			{
				result = Language.GetLang(21015, new object[0]);
			}
			else
			{
				bool flag4 = RoomManage.GetInstance().PRoomData.GetUserData(sname) != null;
				if (flag4)
				{
					result = Language.GetLang(21016, new object[0]);
				}
			}
		}
		return result;
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000CEB9C File Offset: 0x000CCD9C
	private void OnUserDown(GameObject go)
	{
		bool flag = !Input.GetMouseButtonDown(1);
		if (!flag)
		{
			string[] array = go.transform.parent.name.Replace("UserRender", string.Empty).Split(new char[]
			{
				'_'
			});
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			bool flag2 = num < this.szFriend.Count;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)this.szFriend[num];
				bool flag3 = (int)arrayList[0] == num2;
				if (flag3)
				{
					ChatManager.GetInstance().OnMsgTip(arrayList[1].ToString(), 1, null, num2);
				}
			}
		}
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000CEC5C File Offset: 0x000CCE5C
	private void OnUserTgClick(GameObject go)
	{
		Toggle component = go.GetComponent<Toggle>();
		string[] array = go.transform.parent.parent.name.Replace("UserRender", string.Empty).Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[0]);
		bool flag = num >= this.szFriend.Count;
		if (flag)
		{
			component.isOn = false;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.szFriend[num];
			string text = arrayList[1].ToString();
			switch (this.nowType)
			{
			case 1:
			{
				bool isOn = component.isOn;
				if (isOn)
				{
					bool flag2 = this.szToggled.Count >= 5;
					if (flag2)
					{
						component.isOn = false;
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21011, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						bool flag3 = !this.szToggled.Contains(text);
						if (flag3)
						{
							this.szToggled.Add(text);
						}
					}
				}
				else
				{
					bool flag4 = this.szToggled.Contains(text);
					if (flag4)
					{
						this.szToggled.Remove(text);
					}
				}
				break;
			}
			case 2:
			{
				bool isOn2 = component.isOn;
				if (isOn2)
				{
					bool flag5 = this.szToggled.Count >= 4 - TeamManager.GetInstance().Dyaoqing.Count;
					if (flag5)
					{
						component.isOn = false;
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40016, new object[]
						{
							(4 - TeamManager.GetInstance().Dyaoqing.Count).ToString()
						}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
					else
					{
						bool flag6 = !this.szToggled.Contains(text);
						if (flag6)
						{
							this.szToggled.Add(text);
						}
					}
				}
				else
				{
					bool flag7 = this.szToggled.Contains(text);
					if (flag7)
					{
						this.szToggled.Remove(text);
					}
				}
				break;
			}
			case 3:
			case 4:
			case 5:
			case 6:
			{
				bool isOn3 = component.isOn;
				if (isOn3)
				{
					this.szToggled.Clear();
					bool flag8 = this.onSelectTogg != null && this.onSelectTogg != go;
					if (flag8)
					{
						this.onSelectTogg.GetComponent<Toggle>().isOn = false;
					}
					this.onSelectTogg = component.gameObject;
					bool flag9 = !this.szToggled.Contains(text);
					if (flag9)
					{
						this.szToggled.Add(text);
					}
				}
				else
				{
					bool flag10 = this.szToggled.Contains(text);
					if (flag10)
					{
						this.szToggled.Remove(text);
					}
				}
				break;
			}
			}
		}
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000CEF98 File Offset: 0x000CD198
	private void OnClickInvite(GameObject go)
	{
		switch (this.nowType)
		{
		case 1:
			this.RoomInviteFriend();
			break;
		case 2:
			this.TeamInvite();
			break;
		case 3:
			this.OnGiveUserItem();
			break;
		case 4:
			this.OnGiftUserHero();
			break;
		case 5:
			this.OnGiftFace();
			break;
		case 6:
			this.OnGiftPaint();
			break;
		}
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000CF008 File Offset: 0x000CD208
	private void RoomInviteFriend()
	{
		bool flag = RoomManage.GetInstance().PRoomData == null || PlayerDataManager.getInstance().HUserData.m_nRoomUID == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = this.szToggled.Count == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				RoomManage.GetInstance().AddInvitedFriend(this.szToggled);
				ArrayList arrayList = (ArrayList)this.szToggled.Clone();
				for (int i = 0; i < arrayList.Count; i++)
				{
					arrayList[i] = Fun.EncodingUTF8(arrayList[i].ToString());
				}
				fGameManager.LobbyPlayer.SendMsgToHallServer(1311, new ArrayList
				{
					RoomManage.GetInstance().PRoomData.m_nRoomUID,
					this.nowTeam,
					arrayList,
					RoomManage.GetInstance().PRoomData.GetRoomInfo(3).ToString()
				});
				bool flag3 = MainUICon.inst != null;
				if (flag3)
				{
					MainUICon.inst.ChangeTitleToIdx(0, null);
				}
			}
		}
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000CF1C8 File Offset: 0x000CD3C8
	private void TeamInvite()
	{
		TeamManager.GetInstance().TeamInviteFriend(this.szToggled);
		this.szToggled.Clear();
		bool flag = MainUICon.inst;
		if (flag)
		{
			MainUICon.inst.OnClickBack(null);
		}
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000CF210 File Offset: 0x000CD410
	private void OnGiveUserItem()
	{
		bool flag = this.szToggled.Count == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			ShopManager.GetInstance().OnShopGiveUserItem(this.szToggled, this.nowTeam);
		}
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x000CF298 File Offset: 0x000CD498
	private void OnGiftUserHero()
	{
		bool flag = this.szToggled.Count == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.openParam;
			HeroDataManager.GetInstance().OpenUnlockTip((int)arrayList[0], (int)arrayList[1], (int)arrayList[2], Fun.EncodingUTF8(this.szToggled[0].ToString()), 0);
		}
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000CF358 File Offset: 0x000CD558
	private void OnGiftPaint()
	{
		bool flag = this.szToggled.Count == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			HeroDataManager.GetInstance().OpenPaintTip((int)this.openParam, Fun.EncodingUTF8(this.szToggled[0].ToString()));
		}
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000CF3F4 File Offset: 0x000CD5F4
	private void OnGiftFace()
	{
		bool flag = this.szToggled.Count == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(21009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			HeroDataManager.GetInstance().OpenFaceTip((int)this.openParam, Fun.EncodingUTF8(this.szToggled[0].ToString()));
		}
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000CF48F File Offset: 0x000CD68F
	private void OnClickClose(GameObject go)
	{
		this.Close();
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000CF499 File Offset: 0x000CD699
	public override void Close()
	{
		base.Close();
		this.szToggled.Clear();
	}

	// Token: 0x0400112F RID: 4399
	public static InviteUICon inst;

	// Token: 0x04001130 RID: 4400
	private Text uiTitle;

	// Token: 0x04001131 RID: 4401
	private Text uiBtnText;

	// Token: 0x04001132 RID: 4402
	private GameObject userRender;

	// Token: 0x04001133 RID: 4403
	private List<GameObject> szRender = new List<GameObject>();

	// Token: 0x04001134 RID: 4404
	private GameObject pageUI;

	// Token: 0x04001135 RID: 4405
	private Button preBtn;

	// Token: 0x04001136 RID: 4406
	private Button nextBtn;

	// Token: 0x04001137 RID: 4407
	private Text pageTxt;

	// Token: 0x04001138 RID: 4408
	private Button inviteBtn;

	// Token: 0x04001139 RID: 4409
	private ArrayList szFriend = new ArrayList();

	// Token: 0x0400113A RID: 4410
	private int nAllNum;

	// Token: 0x0400113B RID: 4411
	private int curPageIdx;

	// Token: 0x0400113C RID: 4412
	private int onePageNum = 18;

	// Token: 0x0400113D RID: 4413
	private int maxPageNum = 1;

	// Token: 0x0400113E RID: 4414
	private int nowType;

	// Token: 0x0400113F RID: 4415
	private int nowTeam;

	// Token: 0x04001140 RID: 4416
	private object openParam;

	// Token: 0x04001141 RID: 4417
	private ArrayList szToggled = new ArrayList();

	// Token: 0x04001142 RID: 4418
	private GameObject onSelectTogg;

	// Token: 0x04001143 RID: 4419
	private ArrayList szInvited = new ArrayList();
}
