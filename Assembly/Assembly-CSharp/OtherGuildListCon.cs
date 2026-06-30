using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000287 RID: 647
public class OtherGuildListCon : Window
{
	// Token: 0x06001DA5 RID: 7589 RVA: 0x00113F24 File Offset: 0x00112124
	protected override void Awake()
	{
		base.Awake();
		OtherGuildListCon.inst = this;
		this.pGuildPraentList = this.UIGameObj.Get_GameObj(0);
		this.OneGuild = this.UIGameObj.Get_GameObj(1);
		this.pYouBtn = this.UIGameObj.Get_GameObj(2);
		this.pZuoBtn = this.UIGameObj.Get_GameObj(3);
		this.T_PageText = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.pCreateGuildBtn = this.UIGameObj.Get_GameObj(5);
		this.pNameInputField = this.UIGameObj.Get_GameObj(6);
		this.pInputName = this.pNameInputField.GetComponent<InputField>();
		this.pFindBtn = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(this.pCreateGuildBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCreateClick);
		EventTriggerListener.Get(this.pCreateGuildBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.OneGuild.SetActive(false);
		EventTriggerListener.Get(this.pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.pYouBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.T_PageText.text = "1/1";
		EventTriggerListener.Get(this.pFindBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFindGuildClick);
		EventTriggerListener.Get(this.pFindBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pNameInputField.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.pNameInputField.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x00114135 File Offset: 0x00112335
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
		GuildManager.isChangeGong = true;
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00114144 File Offset: 0x00112344
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
		GuildManager.isChangeGong = false;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00114154 File Offset: 0x00112354
	private void Start()
	{
		this.IsLoadOk = true;
		bool flag = this.m_szGuildList != null;
		if (flag)
		{
			this.OnShowGuildList(this.m_szGuildList, this.m_nCurPage, string.Empty);
		}
		this.IsShowCreateBtn();
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x00114198 File Offset: 0x00112398
	public void InitData(ArrayList Data, ArrayList szReques, ArrayList szCount, string sFindGuildName = "")
	{
		this.m_szGuildList = Data;
		this.m_szRequestList = szReques;
		int num = (int)szCount[0];
		bool isLoadOk = this.IsLoadOk;
		if (isLoadOk)
		{
			bool flag = num % this.nRowNum == 0;
			if (flag)
			{
				this.m_nMaxPage = num / this.nRowNum;
			}
			else
			{
				this.m_nMaxPage = num / this.nRowNum + 1;
			}
			this.m_nCurPage = (int)szCount[1];
			bool flag2 = this.m_nCurPage > this.m_nMaxPage;
			if (flag2)
			{
				this.m_nCurPage = this.m_nMaxPage;
			}
			bool flag3 = this.m_nCurPage < 1;
			if (flag3)
			{
				this.m_nCurPage = 1;
			}
			this.OnShowGuildList(this.m_szGuildList, this.m_nCurPage, sFindGuildName);
			this.onPageCd = false;
		}
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x00114269 File Offset: 0x00112469
	public void OnClearUserRequest()
	{
		this.m_szRequestList.Clear();
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x00114278 File Offset: 0x00112478
	private void OnShowGuildList(ArrayList arr, int nCurPage, string sFindGuildName = "")
	{
		int num = 0;
		this.onClear();
		bool flag = arr.Count == 0;
		if (flag)
		{
			GuildManager.GetInstance().UpdatePageBtnState(1, 1, this.pZuoBtn, this.pYouBtn);
		}
		else
		{
			bool flag2 = nCurPage <= this.m_nMaxPage;
			if (flag2)
			{
				bool flag3 = nCurPage == this.m_nMaxPage;
				int num2;
				if (flag3)
				{
					num2 = arr.Count;
				}
				else
				{
					num2 = this.nRowNum * nCurPage;
				}
				num2 = arr.Count;
				for (int i = 0; i < num2; i++)
				{
					this.OnAddOneGuild((ArrayList)arr[i], num, sFindGuildName);
					num++;
				}
				this.m_nCurPage = nCurPage;
				this.T_PageText.text = this.m_nCurPage.ToString() + "/" + this.m_nMaxPage.ToString();
				GuildManager.GetInstance().UpdatePageBtnState(this.m_nMaxPage, this.m_nCurPage, this.pZuoBtn, this.pYouBtn);
			}
		}
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x00114388 File Offset: 0x00112588
	private void OnAddOneGuild(ArrayList arr, int nBianHao, string sFindGuildName = "")
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneGuild);
		gameObject.transform.SetParent(this.pGuildPraentList.transform);
		gameObject.SetActive(true);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		OneGuildCon oneGuildCon = gameObject.AddComponent<OneGuildCon>();
		oneGuildCon.OnSetData(arr, nBianHao, this.m_szRequestList, sFindGuildName);
		gameObject.name = oneGuildCon.m_nGuildId.ToString();
		this.m_szGuildListObj.Add(gameObject);
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x00114428 File Offset: 0x00112628
	public void OnUpdateOneGuild(int nGuildId, ArrayList szData)
	{
		Transform transform = this.pGuildPraentList.transform.FindChild(nGuildId.ToString());
		bool flag = !(transform == null);
		if (flag)
		{
			OneGuildCon component = transform.GetComponent<OneGuildCon>();
			switch ((int)szData[0])
			{
			case 1:
				component.UpdateShengQingBtnState(false, string.Empty);
				break;
			case 2:
				component.UpdateShengQingBtnState(true, string.Empty);
				break;
			case 3:
				component.UpdateShengQingBtnState(false, "1");
				break;
			}
		}
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x001144BC File Offset: 0x001126BC
	private void OnFanYeClick(GameObject obj)
	{
		bool flag = this.onPageCd;
		if (!flag)
		{
			string a = string.Empty;
			a = obj.name;
			bool flag2 = a == "you_btn";
			if (flag2)
			{
				bool flag3 = this.m_nCurPage >= this.m_nMaxPage;
				if (flag3)
				{
					return;
				}
				this.m_nCurPage++;
			}
			else
			{
				bool flag4 = this.m_nCurPage <= 1;
				if (flag4)
				{
					return;
				}
				this.m_nCurPage--;
			}
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91013,
				new ArrayList
				{
					1,
					this.m_nCurPage,
					0
				}
			});
			this.onPageCd = true;
		}
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x001145AD File Offset: 0x001127AD
	private void onClear()
	{
		fUIManager.OnClearObj(this.m_szGuildListObj);
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x001145BC File Offset: 0x001127BC
	private void OnCreateClick(GameObject obj)
	{
		bool flag = GuildManager.GetInstance().m_nGuildId != 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38006, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nLevel < 10;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38005, new object[]
				{
					10
				}), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = CreateGuildUICon.inst != null;
				if (flag3)
				{
					bool visible = CreateGuildUICon.inst.visible;
					if (visible)
					{
						CreateGuildUICon.inst.Close();
					}
					else
					{
						CreateGuildUICon.inst.Open();
					}
				}
				else
				{
					fUIManager.GetInstance().ShowWin("CreateGuildUI", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
				}
			}
		}
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x001146EC File Offset: 0x001128EC
	private void OnFindGuildClick(GameObject obj)
	{
		string text = string.Empty;
		text = this.pInputName.text;
		bool flag = text == string.Empty;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38031, new object[0]), "ffdd00", "system");
		}
		else
		{
			bool flag2 = Fun.Text_Length(text) > 10;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38030, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag3 = !Fun.CheckSpecialStr(text);
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38064, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag4 = WebParam.client_language == "zh";
					if (flag4)
					{
						fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 1, 0);
					}
					else
					{
						int num = Fun.HaveShieldTxt(text, 0);
						bool flag5 = num != -1;
						if (flag5)
						{
							ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38064, new object[0]), "ffdd00", "system");
						}
						else
						{
							text = Fun.GetShieldTxt(text);
							GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
							{
								91031,
								new ArrayList
								{
									Fun.EncodingUTF8(text),
									Fun.EncodingUTF8(text)
								}
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00114880 File Offset: 0x00112A80
	private void OnCheckBack(int strstate, string sGuildName)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38064, new object[0]), "ffdd00", "system");
		}
		else
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91031,
				new ArrayList
				{
					Fun.EncodingUTF8(sGuildName),
					Fun.EncodingUTF8(sGuildName)
				}
			});
		}
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x00114910 File Offset: 0x00112B10
	public void IsShowCreateBtn()
	{
		bool flag = GuildManager.GetInstance().m_nGuildId == 0;
		if (flag)
		{
			this.pCreateGuildBtn.SetActive(true);
		}
		else
		{
			this.pCreateGuildBtn.SetActive(false);
		}
	}

	// Token: 0x04001AA2 RID: 6818
	public static OtherGuildListCon inst;

	// Token: 0x04001AA3 RID: 6819
	public bool IsLoadOk;

	// Token: 0x04001AA4 RID: 6820
	private GameObject pGuildPraentList;

	// Token: 0x04001AA5 RID: 6821
	private GameObject OneGuild;

	// Token: 0x04001AA6 RID: 6822
	private GameObject pYouBtn;

	// Token: 0x04001AA7 RID: 6823
	private GameObject pZuoBtn;

	// Token: 0x04001AA8 RID: 6824
	private Text T_PageText;

	// Token: 0x04001AA9 RID: 6825
	private GameObject pCreateGuildBtn;

	// Token: 0x04001AAA RID: 6826
	private List<GameObject> m_szGuildListObj = new List<GameObject>();

	// Token: 0x04001AAB RID: 6827
	private ArrayList m_szGuildList;

	// Token: 0x04001AAC RID: 6828
	private ArrayList m_szRequestList;

	// Token: 0x04001AAD RID: 6829
	private int nRowNum = 10;

	// Token: 0x04001AAE RID: 6830
	public int m_nMaxPage = 1;

	// Token: 0x04001AAF RID: 6831
	public int m_nCurPage = 1;

	// Token: 0x04001AB0 RID: 6832
	private GameObject pNameInputField;

	// Token: 0x04001AB1 RID: 6833
	private InputField pInputName;

	// Token: 0x04001AB2 RID: 6834
	private GameObject pFindBtn;

	// Token: 0x04001AB3 RID: 6835
	private bool onPageCd;
}
