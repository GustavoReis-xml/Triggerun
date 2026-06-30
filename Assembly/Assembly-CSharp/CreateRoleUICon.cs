using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C4 RID: 196
public class CreateRoleUICon : Window
{
	// Token: 0x06000740 RID: 1856 RVA: 0x0003FF3C File Offset: 0x0003E13C
	protected override void Awake()
	{
		base.Awake();
		this.backgroundImg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.randNameInput = this.UIGameObj.Get_GameObj(1).GetComponent<InputField>();
		EventTriggerListener.Get(this.randNameInput.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		this.randBtn = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(this.randBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRandName);
		EventTriggerListener.Get(this.randBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.yesBtn = this.UIGameObj.Get_GameObj(3);
		EventTriggerListener.Get(this.yesBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		GameObject go = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickCancel);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.tipText = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.contentPanel = this.UIGameObj.Get_GameObj(6);
		this.contentPanel.SetActive(false);
		this.titleText = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.titleText.font = fUIManager.mNewComic;
		}
		this.descText = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.typeText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		CreateRoleUICon.inst = this;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x000400F8 File Offset: 0x0003E2F8
	private void Start()
	{
		fUIManager.tipBlur.rawImage = this.backgroundImg;
		fUIManager.tipBlur.enabled = true;
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00040118 File Offset: 0x0003E318
	private void Update()
	{
		bool flag = this.backgroundImg.texture != null;
		if (flag)
		{
			this.backgroundImg.enabled = true;
			bool flag2 = !this.contentPanel.activeSelf;
			if (flag2)
			{
				this.contentPanel.SetActive(true);
			}
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0004016B File Offset: 0x0003E36B
	public override void Reset()
	{
		base.Reset();
		fUIManager.tipBlur.rawImage = this.backgroundImg;
		fUIManager.tipBlur.enabled = true;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00040194 File Offset: 0x0003E394
	public override void Close()
	{
		base.Close();
		this.randNameInput.text = string.Empty;
		this.tipText.text = string.Empty;
		ChatManager.ChatInput = false;
		this.backgroundImg.texture = null;
		UnityEngine.Object.DestroyImmediate(this.backgroundImg.texture);
		this.backgroundImg.enabled = false;
		bool activeSelf = this.contentPanel.activeSelf;
		if (activeSelf)
		{
			this.contentPanel.SetActive(false);
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0004021C File Offset: 0x0003E41C
	public void SetUIType(int ntype)
	{
		this.nUIType = ntype;
		bool flag = this.nUIType == 0;
		if (flag)
		{
			this.titleText.text = Language.GetLang(10013, new object[0]);
			this.descText.text = Language.GetLang(10009, new object[0]);
			this.typeText.text = Language.GetLang(10002, new object[0]);
			this.tipText.text = Language.GetLang(10007, new object[0]);
			EventTriggerListener.Get(this.yesBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChange);
			this.randBtn.SetActive(true);
		}
		bool flag2 = this.nUIType == 1;
		if (flag2)
		{
			this.titleText.text = Language.GetLang(10018, new object[0]);
			this.descText.text = Language.GetLang(10019, new object[0]);
			this.typeText.text = Language.GetLang(10020, new object[0]);
			this.tipText.text = Language.GetLang(10021, new object[0]);
			EventTriggerListener.Get(this.yesBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnGuildName);
			this.randBtn.SetActive(false);
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00040387 File Offset: 0x0003E587
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00040390 File Offset: 0x0003E590
	public void ShowTipMsg(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		int num2 = num;
		if (num2 != 1016)
		{
			if (num2 != 1018)
			{
				if (num2 == 91033)
				{
					bool flag = (int)szNetMsg[1] == -1;
					if (flag)
					{
						this.tipText.text = Language.GetLang(10024, new object[0]);
					}
					bool flag2 = (int)szNetMsg[1] == -2;
					if (flag2)
					{
						this.tipText.text = Language.GetLang(10026, new object[0]);
					}
				}
			}
			else
			{
				this.Close();
			}
		}
		else
		{
			this.tipText.text = Language.GetLang(10006, new object[0]);
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			RandomName.AddUsedName(Fun.DecodingUTF8(arrayList[3].ToString()));
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00040484 File Offset: 0x0003E684
	private void OnClickCreate(GameObject go)
	{
		string text = this.randNameInput.text.Trim();
		int num = Fun.Text_Length(text);
		bool flag = string.IsNullOrEmpty(text) || num < 4;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10004, new object[0]);
		}
		else
		{
			bool flag2 = WebParam.client_language == "zh" && num > 12;
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10004, new object[0]);
			}
			else
			{
				bool flag3 = WebParam.client_language != "zh" && (num < 6 || num > 15);
				if (flag3)
				{
					this.tipText.text = Language.GetLang(10004, new object[0]);
				}
				else
				{
					bool flag4 = !Fun.CheckSpecialStr(text);
					if (flag4)
					{
						this.tipText.text = Language.GetLang(10005, new object[0]);
					}
					else
					{
						bool flag5 = WebParam.client_language == "zh" && !this.IsLegalUserName(text);
						if (flag5)
						{
							this.tipText.text = Language.GetLang(10005, new object[0]);
						}
						else
						{
							bool flag6 = WebParam.client_language != "zh" && !this.IsLegalUserName_bx(text);
							if (flag6)
							{
								this.tipText.text = Language.GetLang(10005, new object[0]);
							}
							else
							{
								bool flag7 = WebParam.client_language == "zh";
								if (flag7)
								{
									fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 0, 0);
								}
								else
								{
									int num2 = Fun.HaveShieldTxt(text, 0);
									bool flag8 = num2 != -1;
									if (flag8)
									{
										this.tipText.text = Language.GetLang(10005, new object[0]);
									}
									else
									{
										this.m_sVideoCard = SystemInfo.graphicsDeviceName;
										GameRoot.LoginSocket.unFsorket.ToServer("OnCreatePlayer", new object[]
										{
											Fun.EncodingUTF8(text),
											0,
											CreateRoleUICon.iSex,
											1,
											1,
											this.m_sVideoCard
										});
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x000406F0 File Offset: 0x0003E8F0
	private void OnCheckBack(int ishave, string sName)
	{
		bool flag = ishave != -1;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10005, new object[0]);
		}
		else
		{
			this.m_sVideoCard = SystemInfo.graphicsDeviceName;
			GameRoot.LoginSocket.unFsorket.ToServer("OnCreatePlayer", new object[]
			{
				Fun.EncodingUTF8(sName),
				0,
				CreateRoleUICon.iSex,
				1,
				1,
				this.m_sVideoCard
			});
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00040788 File Offset: 0x0003E988
	private void OnClickRandName(GameObject go)
	{
		this.GetRandName();
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00040794 File Offset: 0x0003E994
	private void GetRandName()
	{
		string text = string.Empty;
		text = RandomName.GetRandName();
		this.randNameInput.text = text;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x000407BC File Offset: 0x0003E9BC
	public bool IsLegalUserName(string sNick)
	{
		string pattern = "[一-龥a-zA-Z0-9_¤-㈩！-￥]+";
		return Regex.IsMatch(sNick, pattern);
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000407DC File Offset: 0x0003E9DC
	public bool IsLegalUserName_bx(string sNick)
	{
		string pattern = "[a-zA-Z0-9]+";
		return Regex.IsMatch(sNick, pattern);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000407FC File Offset: 0x0003E9FC
	private void OnClickChange(GameObject go)
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch > 0;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10008, new object[0]);
		}
		else
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10027, new object[0]);
			}
			else
			{
				string text = this.randNameInput.text.Trim();
				int num = Fun.Text_Length(text);
				bool flag3 = string.IsNullOrEmpty(text) || num < 4;
				if (flag3)
				{
					this.tipText.text = Language.GetLang(10004, new object[0]);
				}
				else
				{
					bool flag4 = WebParam.client_language == "zh" && num > 12;
					if (flag4)
					{
						this.tipText.text = Language.GetLang(10004, new object[0]);
					}
					else
					{
						bool flag5 = WebParam.client_language != "zh" && (num < 6 || num > 15);
						if (flag5)
						{
							this.tipText.text = Language.GetLang(10004, new object[0]);
						}
						else
						{
							bool flag6 = !Fun.CheckSpecialStr(text);
							if (flag6)
							{
								this.tipText.text = Language.GetLang(10005, new object[0]);
							}
							else
							{
								bool flag7 = WebParam.client_language == "zh" && !this.IsLegalUserName(text);
								if (flag7)
								{
									this.tipText.text = Language.GetLang(10005, new object[0]);
								}
								else
								{
									bool flag8 = WebParam.client_language != "zh" && !this.IsLegalUserName_bx(text);
									if (flag8)
									{
										this.tipText.text = Language.GetLang(10005, new object[0]);
									}
									else
									{
										bool flag9 = WebParam.client_language == "zh";
										if (flag9)
										{
											fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckChangeBack), text, 0, 0);
										}
										else
										{
											int num2 = Fun.HaveShieldTxt(text, 0);
											bool flag10 = num2 != -1;
											if (flag10)
											{
												this.tipText.text = Language.GetLang(10005, new object[0]);
											}
											else
											{
												bool flag11 = text == PlayerDataManager.getInstance().HUserData.m_sName;
												if (flag11)
												{
													this.tipText.text = Language.GetLang(10006, new object[0]);
												}
												else
												{
													bool flag12 = PlayerDataManager.getInstance().HUserData.m_nChangeName == 0;
													if (flag12)
													{
														fGameManager.LobbyPlayer.SendMsgToHallServer(10096, new ArrayList
														{
															Fun.EncodingUTF8(text),
															0
														});
													}
													else
													{
														TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(10010, new object[0]), Language.GetLang(10011, new object[0]), Language.GetLang(10012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmChgName), text, null, null);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00040B64 File Offset: 0x0003ED64
	private void OnCheckChangeBack(int strstate, string sName)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10005, new object[0]);
		}
		else
		{
			bool flag2 = sName == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10006, new object[0]);
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nChangeName == 0;
				if (flag3)
				{
					fGameManager.LobbyPlayer.SendMsgToHallServer(10096, new ArrayList
					{
						Fun.EncodingUTF8(sName),
						0
					});
				}
				else
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(10010, new object[0]), Language.GetLang(10011, new object[0]), Language.GetLang(10012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmChgName), sName, null, null);
				}
			}
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00040C88 File Offset: 0x0003EE88
	private void OnConfirmChgName(object szParam)
	{
		bool flag = PlayerDataManager.getInstance().playerdet.chargeCurrency < 200;
		if (flag)
		{
			fUIManager.GetInstance().OpenChongZhiUI("ChangeName");
		}
		else
		{
			fGameManager.LobbyPlayer.SendMsgToHallServer(10096, new ArrayList
			{
				Fun.EncodingUTF8(szParam.ToString()),
				0
			});
		}
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00040CF7 File Offset: 0x0003EEF7
	private void OnClickCancel(GameObject go)
	{
		this.Close();
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00040D04 File Offset: 0x0003EF04
	private void OnGuildName(GameObject go)
	{
		bool flag = GuildManager.GetInstance().m_nGuildId == 0 || GuildManager.GetInstance().m_nZhiWei != 2;
		if (!flag)
		{
			string text = this.randNameInput.text.Trim();
			bool flag2 = string.IsNullOrEmpty(text) || Fun.Text_Length(text) < 4;
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10022, new object[0]);
			}
			else
			{
				bool flag3 = Fun.Text_Length(text) > 10;
				if (flag3)
				{
					this.tipText.text = Language.GetLang(10022, new object[0]);
				}
				else
				{
					bool flag4 = !Fun.CheckSpecialStr(text);
					if (flag4)
					{
						this.tipText.text = Language.GetLang(10023, new object[0]);
					}
					else
					{
						bool flag5 = WebParam.client_language == "zh";
						if (flag5)
						{
							fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckGuildName), text, 0, 0);
						}
						else
						{
							int num = Fun.HaveShieldTxt(text, 0);
							bool flag6 = num != -1;
							if (flag6)
							{
								this.tipText.text = Language.GetLang(10023, new object[0]);
							}
							else
							{
								bool flag7 = text == GuildManager.GetInstance().m_sGuildName;
								if (flag7)
								{
									this.tipText.text = Language.GetLang(10024, new object[0]);
								}
								else
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(10025, new object[0]), Language.GetLang(10011, new object[0]), Language.GetLang(10012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmGuildName), text, null, null);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00040EF0 File Offset: 0x0003F0F0
	private void OnCheckGuildName(int strstate, string sName)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			this.tipText.text = Language.GetLang(10023, new object[0]);
		}
		else
		{
			bool flag2 = sName == GuildManager.GetInstance().m_sGuildName;
			if (flag2)
			{
				this.tipText.text = Language.GetLang(10024, new object[0]);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(10025, new object[0]), Language.GetLang(10011, new object[0]), Language.GetLang(10012, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnConfirmGuildName), sName, null, null);
			}
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00040FC4 File Offset: 0x0003F1C4
	private void OnConfirmGuildName(object szParam)
	{
		bool flag = PlayerDataManager.getInstance().playerdet.chargeCurrency < 200;
		if (flag)
		{
			fUIManager.GetInstance().OpenChongZhiUI("ChangeGuildName");
		}
		else
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91033,
				new ArrayList
				{
					Fun.EncodingUTF8(szParam.ToString()),
					0
				}
			});
		}
	}

	// Token: 0x0400054A RID: 1354
	public static CreateRoleUICon inst;

	// Token: 0x0400054B RID: 1355
	private RawImage backgroundImg;

	// Token: 0x0400054C RID: 1356
	private InputField randNameInput;

	// Token: 0x0400054D RID: 1357
	private GameObject contentPanel;

	// Token: 0x0400054E RID: 1358
	private GameObject randBtn;

	// Token: 0x0400054F RID: 1359
	private GameObject yesBtn;

	// Token: 0x04000550 RID: 1360
	private Text tipText;

	// Token: 0x04000551 RID: 1361
	private Text titleText;

	// Token: 0x04000552 RID: 1362
	private Text descText;

	// Token: 0x04000553 RID: 1363
	private Text typeText;

	// Token: 0x04000554 RID: 1364
	public static int iSex;

	// Token: 0x04000555 RID: 1365
	public string m_sVideoCard = string.Empty;

	// Token: 0x04000556 RID: 1366
	public string m_sCPU = string.Empty;

	// Token: 0x04000557 RID: 1367
	public int m_nMemory;

	// Token: 0x04000558 RID: 1368
	public int nUIType;
}
