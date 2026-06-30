using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015F RID: 351
public class GuildInfoCon : Window
{
	// Token: 0x06000FCE RID: 4046 RVA: 0x0009196C File Offset: 0x0008FB6C
	protected override void Awake()
	{
		base.Awake();
		GuildInfoCon.inst = this;
		this.pToggleBtn_1 = this.UIGameObj.Get_GameObj(0);
		this.pToggleBtn_2 = this.UIGameObj.Get_GameObj(1);
		this.pMemberPanel = this.UIGameObj.Get_GameObj(2);
		this.pRequestPanel = this.UIGameObj.Get_GameObj(3);
		this.Member_pYouBtn = this.UIGameObj.Get_GameObj(4);
		this.Member_pZuoBtn = this.UIGameObj.Get_GameObj(5);
		this.Member_T_PageText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.Request_pYouBtn = this.UIGameObj.Get_GameObj(7);
		this.Request_pZuoBtn = this.UIGameObj.Get_GameObj(8);
		this.Request_T_PageText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.Member_List = this.UIGameObj.Get_GameObj(10);
		this.Request_List = this.UIGameObj.Get_GameObj(11);
		this.OneMember = this.UIGameObj.Get_GameObj(12);
		this.pInputGuildName = this.UIGameObj.Get_GameObj(13).GetComponent<InputField>();
		this.pChangeNameBtn = this.UIGameObj.Get_GameObj(14);
		this.pChangeNameBtn.SetActive(false);
		this.pInputGuildName.interactable = false;
		this.T_GuildLv = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.T_GuildHuoYue = this.UIGameObj.Get_GameObj(23).GetComponent<Text>();
		this.T_GuildRenShu = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.T_GuildDuiZhang = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		this.T_GuildTanHeTime = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.pExitGuildBtn = this.UIGameObj.Get_GameObj(20);
		this.pInputGuildGongGao = this.UIGameObj.Get_GameObj(17).GetComponent<InputField>();
		this.pChangeGongGaoBtn = this.UIGameObj.Get_GameObj(18);
		this.pTanHeBtn = this.UIGameObj.Get_GameObj(22);
		EventTriggerListener.Get(this.pTanHeBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTanHe);
		EventTriggerListener.Get(this.pTanHeBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.OneMember.SetActive(false);
		this.pTanHeBtn.SetActive(false);
		this.T_GuildTanHeTime.text = string.Empty;
		this.pCheckBtn_1 = this.UIGameObj.Get_GameObj(24).GetComponent<Toggle>();
		this.pCheckBtn_2 = this.UIGameObj.Get_GameObj(25).GetComponent<Toggle>();
		this.T_GuildRank = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.T_GuildRankImg = this.UIGameObj.Get_GameObj(27).GetComponent<Image>();
		this.isHaveRequestImg = this.UIGameObj.Get_GameObj(28);
		this.pLvUpBtn = this.UIGameObj.Get_GameObj(29);
		this.pRankPanel = this.UIGameObj.Get_GameObj(30);
		EventTriggerListener.Get(this.pCheckBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		EventTriggerListener.Get(this.pCheckBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		EventTriggerListener.Get(this.pToggleBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.pToggleBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.pToggleBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pToggleBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pExitGuildBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnExitClick);
		EventTriggerListener.Get(this.pExitGuildBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pChangeNameBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeNameClick);
		EventTriggerListener.Get(this.pChangeNameBtn).onEnter = new EventTriggerListener.VoidDelegate(this.OverChangeName);
		EventTriggerListener.Get(this.pChangeNameBtn).onExit = new EventTriggerListener.VoidDelegate(this.ExitChangeName);
		EventTriggerListener.Get(this.pChangeNameBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pChangeGongGaoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeGongGaoClick);
		EventTriggerListener.Get(this.pChangeGongGaoBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.Member_pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.Member_pYouBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.Member_pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.Member_pZuoBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.Request_pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRequestFanYeClick);
		EventTriggerListener.Get(this.Request_pYouBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.Request_pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRequestFanYeClick);
		EventTriggerListener.Get(this.Request_pZuoBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.pInputGuildGongGao.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.pInputGuildGongGao.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		EventTriggerListener.Get(this.pLvUpBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnLvUpClick);
		EventTriggerListener.Get(this.pLvUpBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pRankPanel.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnRankEnter);
		EventTriggerListener.Get(this.pRankPanel.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnRankExit);
		EventTriggerListener.Get(this.T_GuildHuoYue.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnHuoYueEnter);
		EventTriggerListener.Get(this.T_GuildHuoYue.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnRankExit);
		EventTriggerListener.Get(this.T_GuildRenShu.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnRenShuEnter);
		EventTriggerListener.Get(this.T_GuildRenShu.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnRankExit);
		this.pInputGuildGongGao.characterLimit = 100;
		this.isHaveRequestImg.SetActive(false);
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00092078 File Offset: 0x00090278
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
		GuildManager.isChangeGong = true;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00092087 File Offset: 0x00090287
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
		GuildManager.isChangeGong = false;
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00092096 File Offset: 0x00090296
	public void ShowRequestImg(bool isShow)
	{
		this.isHaveRequestImg.SetActive(isShow);
		GuildManager.GetInstance().OnUpdateGuildRequstNum(isShow ? 1 : 0);
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x000920B8 File Offset: 0x000902B8
	private void Start()
	{
		this.IsLoadOk = true;
		this.pCheckBtn_1.isOn = true;
		this.Request_T_PageText.text = "1/1";
		this.Member_T_PageText.text = "1/1";
		bool flag = this.m_szBase != null;
		if (flag)
		{
			this.InitData(this.m_szData);
		}
		this.OnisShowRequestTogg();
		this.pInputGuildGongGao.transform.FindChild("Text").GetComponent<Text>().lineSpacing = 1f;
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00092144 File Offset: 0x00090344
	public void OnisShowRequestTogg()
	{
		this.pCheckBtn_2.gameObject.SetActive(false);
		bool flag = GuildManager.GetInstance().m_nZhiWei > 0;
		if (flag)
		{
			this.pLvUpBtn.SetActive(true);
			this.pChangeGongGaoBtn.SetActive(true);
			this.pInputGuildGongGao.interactable = true;
			this.pToggleBtn_2.SetActive(true);
			bool flag2 = GuildManager.GetInstance().m_nZhiWei == 2;
			if (flag2)
			{
				this.pCheckBtn_2.gameObject.SetActive(true);
			}
		}
		else
		{
			this.pLvUpBtn.SetActive(false);
			this.pChangeGongGaoBtn.SetActive(false);
			this.pInputGuildGongGao.interactable = false;
			this.pToggleBtn_2.SetActive(false);
			this.OnToggle(this.pToggleBtn_1, false);
		}
		bool flag3 = GuildManager.GetInstance().m_nZhiWei == 2;
		if (flag3)
		{
			this.pChangeNameBtn.SetActive(true);
		}
		else
		{
			this.pChangeNameBtn.SetActive(false);
		}
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00092248 File Offset: 0x00090448
	public void InitData(ArrayList Data)
	{
		this.m_szData = Data;
		this.m_szBase = (ArrayList)Data[0];
		this.m_szMember = (ArrayList)Data[1];
		this.m_nMemberAllCount = (int)Data[2];
		bool isLoadOk = this.IsLoadOk;
		if (isLoadOk)
		{
			this.OnUpdateMemberList(this.m_szMember, this.m_nMemberAllCount);
			this.OnShowMyGuildData(this.m_szBase);
			this.OnToggle(this.pToggleBtn_1, false);
		}
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x000922D0 File Offset: 0x000904D0
	public void OnUpdateMemberList(ArrayList szData, int nCount)
	{
		this.m_nMemberAllCount = nCount;
		szData = szData.GetRange(0, nCount);
		bool isLoadOk = this.IsLoadOk;
		if (isLoadOk)
		{
			this.m_szMember = szData;
			bool flag = this.isShowOnLostMember;
			if (flag)
			{
				this.m_szOnShowMember = this.m_szMember;
			}
			else
			{
				this.m_szOnShowMember = this.GetOnLineMember();
			}
			bool flag2 = this.m_szOnShowMember.Count % this.nRowNum == 0;
			if (flag2)
			{
				this.m_nMaxPage = this.m_szOnShowMember.Count / this.nRowNum;
			}
			else
			{
				this.m_nMaxPage = this.m_szOnShowMember.Count / this.nRowNum + 1;
			}
			bool flag3 = this.m_nCurPage > this.m_nMaxPage;
			if (flag3)
			{
				this.m_nCurPage = this.m_nMaxPage;
			}
			bool flag4 = this.m_nCurPage < 1;
			if (flag4)
			{
				this.m_nCurPage = 1;
			}
			this.OnShowGuildMember(this.m_szOnShowMember, this.m_nCurPage);
		}
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000923CC File Offset: 0x000905CC
	private ArrayList GetOnLineMember()
	{
		ArrayList arrayList = new ArrayList();
		int count = this.m_szMember.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList2 = (ArrayList)this.m_szMember[i];
			bool flag = arrayList2.Count == 0 || (int)arrayList2[5] == 0;
			if (flag)
			{
				break;
			}
			arrayList.Add(arrayList2);
		}
		return arrayList;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00092444 File Offset: 0x00090644
	private void OnClickToggle(GameObject obj)
	{
		this.OnToggle(obj, true);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00092450 File Offset: 0x00090650
	private void OnToggle(GameObject obj, bool isSend = false)
	{
		int num = int.Parse(obj.name.Replace("Toggle", string.Empty));
		bool flag = this.m_nPanelType == num;
		if (!flag)
		{
			this.m_nPanelType = num;
			bool flag2 = this.m_nPanelType == 1;
			if (flag2)
			{
				this.pMemberPanel.SetActive(true);
				this.pRequestPanel.SetActive(false);
				if (isSend)
				{
					GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
					{
						91018,
						new ArrayList
						{
							1,
							0,
							0
						}
					});
				}
			}
			else
			{
				this.pMemberPanel.SetActive(false);
				this.pRequestPanel.SetActive(true);
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91017,
					new ArrayList
					{
						1,
						0,
						0
					}
				});
			}
			bool flag3 = this.OnSelectToggleBtn != null;
			if (flag3)
			{
				this.OnSelectToggleBtn.GetComponent<Toggle>().enabled = true;
				this.OnSelectToggleBtn.GetComponent<Toggle>().interactable = true;
			}
			if (isSend)
			{
				obj.GetComponent<Toggle>().enabled = false;
			}
			obj.GetComponent<Toggle>().interactable = false;
			this.OnSelectToggleBtn = obj;
		}
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x000925F8 File Offset: 0x000907F8
	public void OnShowMyGuildData(ArrayList arr)
	{
		this.m_nGuildID = (int)arr[0];
		this.m_sGuildName = Fun.DecodingUTF8(arr[1].ToString());
		this.pInputGuildName.text = this.m_sGuildName;
		GuildManager.GetInstance().m_nGuildLv = (int)arr[2];
		this.T_GuildLv.text = arr[2].ToString();
		this.m_nNeedHouYue = GuildUserState.GetNeedHuoYue((int)arr[2]);
		this.m_nHuoYue = (int)arr[3];
		this.T_GuildHuoYue.text = this.m_nHuoYue.ToString() + "/" + this.m_nNeedHouYue.ToString();
		this.T_GuildRenShu.text = arr[4].ToString() + "/" + arr[8].ToString();
		this.T_GuildDuiZhang.text = Fun.DecodingUTF8(arr[5].ToString());
		this.pInputGuildGongGao.text = this.OnSaveNotice(Fun.DecodingUTF8(arr[6].ToString()));
		this.OnChangeIsAutoJoin((int)arr[7]);
		this.m_nRank = (int)arr[9];
		bool flag = this.m_nRank > 10;
		if (flag)
		{
			this.T_GuildRank.gameObject.SetActive(false);
			this.T_GuildRankImg.gameObject.SetActive(false);
		}
		else
		{
			bool flag2 = this.m_nRank <= 3;
			if (flag2)
			{
				this.T_GuildRank.gameObject.SetActive(false);
				this.T_GuildRankImg.gameObject.SetActive(true);
				this.T_GuildRankImg.sprite = Window.spriteName(AtlasType.ATLAS_GUILD, "guild_" + this.m_nRank.ToString());
				this.T_GuildRankImg.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(24f, 37f);
			}
			else
			{
				this.T_GuildRank.gameObject.SetActive(true);
				this.T_GuildRankImg.gameObject.SetActive(true);
				this.T_GuildRank.text = this.m_nRank.ToString();
				this.T_GuildRankImg.sprite = Window.spriteName(AtlasType.ATLAS_GUILD, "guild_4");
				this.T_GuildRankImg.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38f, 20f);
			}
		}
		GuildManager.GetInstance().m_sTanheUser = Fun.DecodingUTF8(arr[10].ToString());
		GuildManager.GetInstance().m_sTanheTime = arr[11].ToString();
		bool flag3 = string.IsNullOrEmpty(GuildManager.GetInstance().m_sTanheUser);
		if (flag3)
		{
			this.T_GuildTanHeTime.text = string.Empty;
		}
		else
		{
			GameRoot.inst.CheckGuildTanhe();
		}
		this.CheckTanheBtn();
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00092908 File Offset: 0x00090B08
	public void CheckTanheBtn()
	{
		string sTanheUser = GuildManager.GetInstance().m_sTanheUser;
		string sTanheTime = GuildManager.GetInstance().m_sTanheTime;
		bool flag = !string.IsNullOrEmpty(sTanheUser) && !string.IsNullOrEmpty(sTanheTime);
		if (flag)
		{
			this.pTanHeBtn.SetActive(false);
		}
		else
		{
			ArrayList huiZhangData = this.GetHuiZhangData();
			int nZhiWei = GuildManager.GetInstance().m_nZhiWei;
			bool flag2 = huiZhangData.Count > 0 && (int)huiZhangData[5] == 0 && !string.IsNullOrEmpty(huiZhangData[6].ToString());
			if (flag2)
			{
				DateTime serverDate = Fun.GetServerDate();
				DateTime d = DateTime.Parse(huiZhangData[6].ToString());
				int num = (int)(serverDate - d).TotalHours;
				bool flag3 = nZhiWei == 1 && num >= 72;
				if (flag3)
				{
					this.pTanHeBtn.SetActive(true);
				}
				else
				{
					bool flag4 = nZhiWei == 0 && num >= 120;
					if (flag4)
					{
						this.pTanHeBtn.SetActive(true);
					}
					else
					{
						this.pTanHeBtn.SetActive(false);
					}
				}
			}
			else
			{
				this.pTanHeBtn.SetActive(false);
			}
		}
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00092A40 File Offset: 0x00090C40
	public void SetGuildTanhe(int nsec)
	{
		bool flag = nsec < 0;
		if (flag)
		{
			this.T_GuildTanHeTime.text = string.Empty;
		}
		else
		{
			this.T_GuildTanHeTime.text = Language.GetLang(38069, new object[]
			{
				Fun.SecToFullTimeString(nsec)
			});
		}
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00092A8F File Offset: 0x00090C8F
	public void OnCheckTanheOk(string sName)
	{
		this.T_GuildTanHeTime.text = string.Empty;
		this.T_GuildDuiZhang.text = sName;
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00092AB0 File Offset: 0x00090CB0
	public void OnShowGuildMember(ArrayList arr, int nCurPage)
	{
		int num = 0;
		fUIManager.OnClearObj(this.m_szMemberListObj);
		bool flag = arr.Count == 0;
		if (flag)
		{
			GuildManager.GetInstance().UpdatePageBtnState(1, 1, this.Member_pZuoBtn, this.Member_pYouBtn);
		}
		else
		{
			bool flag2 = nCurPage <= this.m_nMaxPage;
			if (flag2)
			{
				int num2 = (nCurPage != this.m_nMaxPage) ? (this.nRowNum * nCurPage) : arr.Count;
				int num3 = (nCurPage - 1) * this.nRowNum;
				while (num3 < num2 && ((ArrayList)arr[num3]).Count != 0)
				{
					this.OnAddOneMember((ArrayList)arr[num3], num, this.Member_List, 1);
					num++;
					num3++;
				}
				this.m_nCurPage = nCurPage;
				this.Member_T_PageText.text = this.m_nCurPage.ToString() + "/" + this.m_nMaxPage.ToString();
				GuildManager.GetInstance().UpdatePageBtnState(this.m_nMaxPage, this.m_nCurPage, this.Member_pZuoBtn, this.Member_pYouBtn);
			}
		}
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00092BD8 File Offset: 0x00090DD8
	private void OnAddOneMember(ArrayList arr, int nBianHao, GameObject pParent, int nType)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneMember);
		gameObject.transform.SetParent(pParent.transform);
		gameObject.name = nBianHao.ToString();
		gameObject.SetActive(true);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		OneMemberCon oneMemberCon = gameObject.AddComponent<OneMemberCon>();
		oneMemberCon.OnSetData(arr, nBianHao, nType);
		bool flag = nType == 1;
		if (flag)
		{
			this.m_szMemberListObj.Add(gameObject);
		}
		else
		{
			this.m_szRequestListObj.Add(gameObject);
		}
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x00092C88 File Offset: 0x00090E88
	private void OnFanYeClick(GameObject obj)
	{
		string a = string.Empty;
		a = obj.name;
		bool flag = a == "you_btn";
		if (flag)
		{
			bool flag2 = this.m_nCurPage >= this.m_nMaxPage;
			if (flag2)
			{
				return;
			}
			this.m_nCurPage++;
		}
		else
		{
			bool flag3 = this.m_nCurPage <= 1;
			if (flag3)
			{
				return;
			}
			this.m_nCurPage--;
		}
		this.OnShowGuildMember(this.m_szOnShowMember, this.m_nCurPage);
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00092D14 File Offset: 0x00090F14
	public void OnSetRequestData(ArrayList szData)
	{
		this.m_szRequest = (ArrayList)szData[0];
		bool flag = this.m_szRequest.Count % this.nRowNum == 0;
		if (flag)
		{
			this.m_nReq_MaxPage = this.m_szRequest.Count / this.nRowNum;
		}
		else
		{
			this.m_nReq_MaxPage = this.m_szRequest.Count / this.nRowNum + 1;
		}
		bool flag2 = this.m_szRequest.Count > 0;
		if (flag2)
		{
			this.ShowRequestImg(true);
		}
		else
		{
			this.ShowRequestImg(false);
		}
		bool flag3 = this.m_nReq_CurPage > this.m_nReq_MaxPage;
		if (flag3)
		{
			this.m_nReq_CurPage = this.m_nReq_MaxPage;
		}
		bool flag4 = this.m_nReq_CurPage < 1;
		if (flag4)
		{
			this.m_nReq_CurPage = 1;
		}
		this.OnShowRequest(this.m_szRequest, this.m_nReq_CurPage);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00092DF8 File Offset: 0x00090FF8
	private void OnShowRequest(ArrayList szData, int nCurPage)
	{
		int num = 0;
		fUIManager.OnClearObj(this.m_szRequestListObj);
		bool flag = szData.Count == 0;
		if (flag)
		{
			GuildManager.GetInstance().UpdatePageBtnState(1, 1, this.Request_pZuoBtn, this.Request_pYouBtn);
		}
		else
		{
			bool flag2 = nCurPage <= this.m_nReq_MaxPage;
			if (flag2)
			{
				int num2 = (nCurPage != this.m_nReq_MaxPage) ? (this.nRowNum * nCurPage) : szData.Count;
				for (int i = (nCurPage - 1) * this.nRowNum; i < num2; i++)
				{
					this.OnAddOneMember((ArrayList)szData[i], num, this.Request_List, 2);
					num++;
				}
				this.m_nReq_CurPage = nCurPage;
				this.Request_T_PageText.text = this.m_nReq_CurPage.ToString() + "/" + this.m_nReq_MaxPage.ToString();
				GuildManager.GetInstance().UpdatePageBtnState(this.m_nReq_MaxPage, this.m_nReq_CurPage, this.Request_pZuoBtn, this.Request_pYouBtn);
			}
		}
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x00092F08 File Offset: 0x00091108
	private void OnRequestFanYeClick(GameObject obj)
	{
		string a = string.Empty;
		a = obj.name;
		bool flag = a == "you_btn";
		if (flag)
		{
			bool flag2 = this.m_nReq_CurPage >= this.m_nReq_MaxPage;
			if (flag2)
			{
				return;
			}
			this.m_nReq_CurPage++;
		}
		else
		{
			bool flag3 = this.m_nReq_CurPage <= 1;
			if (flag3)
			{
				return;
			}
			this.m_nReq_CurPage--;
		}
		this.OnShowRequest(this.m_szRequest, this.m_nReq_CurPage);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00092F94 File Offset: 0x00091194
	public void OnDeleteRequest(int nDataId)
	{
		Transform transform = this.Request_List.transform.FindChild(nDataId.ToString());
		bool flag = transform != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(transform.gameObject);
		}
		int count = this.m_szRequest.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList = (ArrayList)this.m_szRequest[i];
			bool flag2 = (int)arrayList[0] == nDataId;
			if (flag2)
			{
				this.m_szRequest.RemoveAt(i);
				break;
			}
		}
		this.OnSetRequestData(new ArrayList
		{
			this.m_szRequest,
			0
		});
		bool flag3 = this.m_szRequest.Count < 1;
		if (flag3)
		{
			GuildInfoCon.inst.ShowRequestImg(false);
		}
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00093074 File Offset: 0x00091274
	private void OnExitClick(GameObject obj)
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_sName == GuildManager.GetInstance().m_sTanheUser;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(46143, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = GuildManager.GetInstance().m_nZhiWei == 2;
			if (flag2)
			{
				GuildManager.GetInstance().onExit(new ArrayList
				{
					this.m_nGuildID,
					0
				});
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38044, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(GuildManager.GetInstance().onExit), new ArrayList
				{
					this.m_nGuildID,
					0
				}, null, null);
			}
		}
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000931C3 File Offset: 0x000913C3
	private void OnChangeNameClick(GameObject obj)
	{
		fUIManager.GetInstance().ShowWin("CreateRoleUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnChangeNickUI), null, true);
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x000931E8 File Offset: 0x000913E8
	private void OnChangeNickUI(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = CreateRoleUICon.inst != null && CreateRoleUICon.inst.visible;
		if (flag)
		{
			CreateRoleUICon.inst.SetUIType(1);
		}
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00093222 File Offset: 0x00091422
	private void ExitChangeName(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00093230 File Offset: 0x00091430
	private void OverChangeName(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(38068, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x00093260 File Offset: 0x00091460
	private void OnChangeGongGaoClick(GameObject obj)
	{
		string sGuildName = this.m_sGuildName;
		string text = this.pInputGuildGongGao.text;
		bool flag = text == string.Empty;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38045, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = Fun.Text_Length(sGuildName) < 4 || Fun.Text_Length(sGuildName) > 200;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38046, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = !Fun.CheckSpecialStr(text);
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38047, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = WebParam.client_language == "zh";
					if (flag4)
					{
						fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 0, 0);
					}
					else
					{
						int num = Fun.HaveShieldTxt(text, 0);
						bool flag5 = num != -1;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38047, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						}
						else
						{
							text = text.Replace("\r\n", "{(#)}");
							text = text.Replace("\n", "{(#)}");
							GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
							{
								91019,
								new ArrayList
								{
									Fun.EncodingUTF8(text),
									0
								}
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000934B0 File Offset: 0x000916B0
	private void OnCheckBack(int strstate, string sGongGao)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38047, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			sGongGao = sGongGao.Replace("\r\n", "{(#)}");
			sGongGao = sGongGao.Replace("\n", "{(#)}");
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91019,
				new ArrayList
				{
					Fun.EncodingUTF8(sGongGao),
					0
				}
			});
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00093585 File Offset: 0x00091785
	private void Update()
	{
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00093588 File Offset: 0x00091788
	public string OnSaveNotice(string sGongGao)
	{
		sGongGao = sGongGao.Replace("{(#)}", "\n");
		this.pInputGuildGongGao.text = sGongGao;
		return sGongGao;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x000935BA File Offset: 0x000917BA
	public void OnChangeGuildName(string sName)
	{
		this.m_sGuildName = sName;
		this.pInputGuildName.text = this.m_sGuildName;
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x000935D8 File Offset: 0x000917D8
	private void OnCheckToggle(GameObject obj)
	{
		string name = obj.name;
		bool flag = name == "Toggle_1";
		if (flag)
		{
			bool isOn = this.pCheckBtn_1.isOn;
			this.isShowOnLostMember = isOn;
			this.OnUpdateMemberList(this.m_szMember, this.m_nMemberAllCount);
		}
		else
		{
			bool isOn = this.pCheckBtn_2.isOn;
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91022,
				new ArrayList
				{
					isOn ? 1 : 0,
					0
				}
			});
		}
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00093688 File Offset: 0x00091888
	public void OnChangeIsAutoJoin(int nValue)
	{
		bool flag = nValue == 1;
		if (flag)
		{
			this.pCheckBtn_2.isOn = true;
		}
		else
		{
			this.pCheckBtn_2.isOn = false;
		}
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x000936C0 File Offset: 0x000918C0
	public void OnLvUpClick(GameObject obj)
	{
		GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
		{
			91006,
			new ArrayList
			{
				0,
				0
			}
		});
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0009371A File Offset: 0x0009191A
	public void OnRankEnter(GameObject obj)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(38048, new object[0]) + this.m_nRank.ToString(), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00093758 File Offset: 0x00091958
	public void OnHuoYueEnter(GameObject obj)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(38049, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00093786 File Offset: 0x00091986
	public void OnRenShuEnter(GameObject obj)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(38060, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000937B4 File Offset: 0x000919B4
	public void OnRankExit(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000937C4 File Offset: 0x000919C4
	public void OnUpdateGuildBase(int nHuoYue, int nLv)
	{
		this.m_nNeedHouYue = GuildUserState.GetNeedHuoYue(nLv);
		this.m_nHuoYue = nHuoYue;
		this.T_GuildLv.text = nLv.ToString();
		this.T_GuildHuoYue.text = nHuoYue.ToString() + "/" + this.m_nNeedHouYue.ToString();
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x00093820 File Offset: 0x00091A20
	private void OnClickTanHe(GameObject go)
	{
		bool flag = string.IsNullOrEmpty(GuildManager.GetInstance().m_sTanheUser);
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38073, new object[]
			{
				GuildManager.GetInstance().m_nTanheCost
			}), Language.GetLang(38074, new object[0]), Language.GetLang(38075, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.ConfirmTanhe), null, null, null);
		}
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000938B4 File Offset: 0x00091AB4
	private void ConfirmTanhe(object szParam)
	{
		int nZhiWei = GuildManager.GetInstance().m_nZhiWei;
		bool flag = PlayerDataManager.getInstance().playerdet.gameCurrency < GuildManager.GetInstance().m_nTanheCost;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(7027, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			ArrayList huiZhangData = this.GetHuiZhangData();
			bool flag2 = (int)huiZhangData[5] == 0 && !string.IsNullOrEmpty(huiZhangData[6].ToString());
			if (flag2)
			{
				DateTime serverDate = Fun.GetServerDate();
				DateTime d = DateTime.Parse(huiZhangData[6].ToString());
				int num = (int)(serverDate - d).TotalHours;
				bool flag3 = (nZhiWei == 1 && num >= 72) || (nZhiWei == 0 && num >= 120);
				if (flag3)
				{
					GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
					{
						91034,
						new ArrayList()
					});
				}
			}
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000939F8 File Offset: 0x00091BF8
	private ArrayList GetHuiZhangData()
	{
		ArrayList result = new ArrayList();
		for (int i = 0; i < this.m_szMember.Count; i++)
		{
			ArrayList arrayList = (ArrayList)this.m_szMember[i];
			bool flag = arrayList.Count == 0;
			if (flag)
			{
				break;
			}
			bool flag2 = (int)arrayList[4] == 2;
			if (flag2)
			{
				result = arrayList;
				break;
			}
		}
		return result;
	}

	// Token: 0x04000C93 RID: 3219
	public bool IsLoadOk;

	// Token: 0x04000C94 RID: 3220
	public static GuildInfoCon inst;

	// Token: 0x04000C95 RID: 3221
	private string m_sGuildName = string.Empty;

	// Token: 0x04000C96 RID: 3222
	private int m_nGuildID;

	// Token: 0x04000C97 RID: 3223
	private ArrayList m_szData;

	// Token: 0x04000C98 RID: 3224
	private ArrayList m_szBase;

	// Token: 0x04000C99 RID: 3225
	private ArrayList m_szMember = new ArrayList();

	// Token: 0x04000C9A RID: 3226
	private ArrayList m_szOnShowMember;

	// Token: 0x04000C9B RID: 3227
	private ArrayList m_szRequest;

	// Token: 0x04000C9C RID: 3228
	private GameObject OneMember;

	// Token: 0x04000C9D RID: 3229
	private InputField pInputGuildName;

	// Token: 0x04000C9E RID: 3230
	private GameObject pChangeNameBtn;

	// Token: 0x04000C9F RID: 3231
	private Text T_GuildLv;

	// Token: 0x04000CA0 RID: 3232
	private Text T_GuildHuoYue;

	// Token: 0x04000CA1 RID: 3233
	private Text T_GuildRenShu;

	// Token: 0x04000CA2 RID: 3234
	private Text T_GuildDuiZhang;

	// Token: 0x04000CA3 RID: 3235
	private Text T_GuildTanHeTime;

	// Token: 0x04000CA4 RID: 3236
	private GameObject pExitGuildBtn;

	// Token: 0x04000CA5 RID: 3237
	private InputField pInputGuildGongGao;

	// Token: 0x04000CA6 RID: 3238
	private GameObject pChangeGongGaoBtn;

	// Token: 0x04000CA7 RID: 3239
	private GameObject pTanHeBtn;

	// Token: 0x04000CA8 RID: 3240
	private Text T_GuildRank;

	// Token: 0x04000CA9 RID: 3241
	private Image T_GuildRankImg;

	// Token: 0x04000CAA RID: 3242
	private GameObject pToggleBtn_1;

	// Token: 0x04000CAB RID: 3243
	private GameObject pToggleBtn_2;

	// Token: 0x04000CAC RID: 3244
	private GameObject pMemberPanel;

	// Token: 0x04000CAD RID: 3245
	private GameObject pRequestPanel;

	// Token: 0x04000CAE RID: 3246
	private GameObject Member_pYouBtn;

	// Token: 0x04000CAF RID: 3247
	private GameObject Member_pZuoBtn;

	// Token: 0x04000CB0 RID: 3248
	private Text Member_T_PageText;

	// Token: 0x04000CB1 RID: 3249
	private GameObject Request_pYouBtn;

	// Token: 0x04000CB2 RID: 3250
	private GameObject Request_pZuoBtn;

	// Token: 0x04000CB3 RID: 3251
	private Text Request_T_PageText;

	// Token: 0x04000CB4 RID: 3252
	private GameObject Member_List;

	// Token: 0x04000CB5 RID: 3253
	private GameObject Request_List;

	// Token: 0x04000CB6 RID: 3254
	private List<GameObject> m_szMemberListObj = new List<GameObject>();

	// Token: 0x04000CB7 RID: 3255
	private List<GameObject> m_szRequestListObj = new List<GameObject>();

	// Token: 0x04000CB8 RID: 3256
	private Toggle pCheckBtn_1;

	// Token: 0x04000CB9 RID: 3257
	private Toggle pCheckBtn_2;

	// Token: 0x04000CBA RID: 3258
	public int m_nMaxPage = 1;

	// Token: 0x04000CBB RID: 3259
	public int m_nCurPage = 1;

	// Token: 0x04000CBC RID: 3260
	public int m_nReq_MaxPage = 1;

	// Token: 0x04000CBD RID: 3261
	public int m_nReq_CurPage = 1;

	// Token: 0x04000CBE RID: 3262
	private int m_nPanelType;

	// Token: 0x04000CBF RID: 3263
	private bool isShowOnLostMember = true;

	// Token: 0x04000CC0 RID: 3264
	private GameObject isHaveRequestImg;

	// Token: 0x04000CC1 RID: 3265
	private GameObject pLvUpBtn;

	// Token: 0x04000CC2 RID: 3266
	private GameObject pRankPanel;

	// Token: 0x04000CC3 RID: 3267
	private int nRowNum = 7;

	// Token: 0x04000CC4 RID: 3268
	private int m_nMemberAllCount;

	// Token: 0x04000CC5 RID: 3269
	private GameObject OnSelectToggleBtn;

	// Token: 0x04000CC6 RID: 3270
	private int m_nRank;

	// Token: 0x04000CC7 RID: 3271
	private int m_nHuoYue;

	// Token: 0x04000CC8 RID: 3272
	private int m_nNeedHouYue;

	// Token: 0x04000CC9 RID: 3273
	private Transform inputCaret;

	// Token: 0x0200058E RID: 1422
	private class myMemberCompare : IComparer
	{
		// Token: 0x06003495 RID: 13461 RVA: 0x001BE1B8 File Offset: 0x001BC3B8
		public int Compare(object x, object y)
		{
			return new CaseInsensitiveComparer().Compare((int)(y as ArrayList)[5], (int)(x as ArrayList)[5]);
		}
	}
}
