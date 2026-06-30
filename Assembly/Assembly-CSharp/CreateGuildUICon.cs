using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C3 RID: 195
public class CreateGuildUICon : Window
{
	// Token: 0x06000735 RID: 1845 RVA: 0x0003F950 File Offset: 0x0003DB50
	protected override void Awake()
	{
		base.Awake();
		CreateGuildUICon.inst = this;
		this.TipText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.pNameInputField = this.UIGameObj.Get_GameObj(3);
		this.pInputName = this.pNameInputField.GetComponent<InputField>();
		this.pCreateBtn_1 = this.UIGameObj.Get_GameObj(9);
		this.pCloseBtn = this.UIGameObj.Get_GameObj(6);
		this.pCreateBtn_2 = this.UIGameObj.Get_GameObj(10);
		this.TipTextTiShi = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		EventTriggerListener.Get(this.pCreateBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCreateClick);
		EventTriggerListener.Get(this.pCreateBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCreateClick);
		EventTriggerListener.Get(this.pCloseBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClsoeClick);
		EventTriggerListener.Get(this.pCreateBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pCloseBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pCreateBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pNameInputField.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.pNameInputField.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0003FAEA File Offset: 0x0003DCEA
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
		GuildManager.isChangeGong = true;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0003FAF9 File Offset: 0x0003DCF9
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
		GuildManager.isChangeGong = false;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0003FB08 File Offset: 0x0003DD08
	private void Start()
	{
		this.pInputName.text = string.Empty;
		this.TipText.text = string.Empty;
		this.TipTextTiShi.text = Language.GetLang(38004, new object[]
		{
			100,
			200
		});
		GameRoot.inst.Setting.Load_Setting_Data_State = false;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0003FB7C File Offset: 0x0003DD7C
	public override void Reset()
	{
		base.Reset();
		this.Start();
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0003FB90 File Offset: 0x0003DD90
	private void OnCreateClick(GameObject obj)
	{
		string text = string.Empty;
		text = this.pInputName.text;
		this.nBuyType = int.Parse(obj.name.Replace("CreateBtn_", string.Empty));
		bool flag = text == string.Empty;
		if (flag)
		{
			this.TipText.text = Language.GetLang(38031, new object[0]);
		}
		else
		{
			bool flag2 = Fun.Text_Length(text) < 4 || Fun.Text_Length(text) > 10;
			if (flag2)
			{
				this.TipText.text = Language.GetLang(38030, new object[0]);
			}
			else
			{
				bool flag3 = !Fun.CheckSpecialStr(text);
				if (flag3)
				{
					this.TipText.text = Language.GetLang(38064, new object[0]);
				}
				else
				{
					bool flag4 = this.nBuyType == 2 && PlayerDataManager.getInstance().playerdet.chargeCurrency < 100;
					if (flag4)
					{
						this.TipText.text = Language.GetLang(38032, new object[0]);
						fUIManager.GetInstance().OpenChongZhiUI("CreateGuildUICon");
					}
					else
					{
						bool flag5 = this.nBuyType == 1 && PlayerDataManager.getInstance().playerdet.gameCurrency < 200;
						if (flag5)
						{
							this.TipText.text = Language.GetLang(39016, new object[0]);
						}
						else
						{
							bool flag6 = PlayerDataManager.getInstance().HUserData.m_nLevel < 10;
							if (flag6)
							{
								this.TipText.text = Language.GetLang(38033, new object[0]);
							}
							else
							{
								fUIManager.GetInstance().TecentCheckShieldTxt(new fUIManager.ShieldCheckDelegate(this.OnCheckBack), text, 1, 0);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0003FD64 File Offset: 0x0003DF64
	private void OnCheckBack(int strstate, string sGuildName)
	{
		bool flag = strstate != -1;
		if (flag)
		{
			this.TipText.text = Language.GetLang(38064, new object[0]);
		}
		else
		{
			bool flag2 = this.nBuyType == 1;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38063, new object[]
				{
					200
				}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.onCreate), sGuildName, null, null);
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38034, new object[]
				{
					100
				}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), Language.GetLang(19006, new object[0]), new TipManager.TipOKDelegate(this.onCreate), sGuildName, null, null);
			}
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0003FEA0 File Offset: 0x0003E0A0
	private void onCreate(object obj)
	{
		string s = obj.ToString();
		GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
		{
			91001,
			new ArrayList
			{
				Fun.EncodingUTF8(s),
				this.nBuyType
			}
		});
		this.OnClsoeClick(null);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0003FF0E File Offset: 0x0003E10E
	private void OnClsoeClick(GameObject obj)
	{
		this.Close();
		GameRoot.inst.Setting.Load_Setting_Data_State = true;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0003FF29 File Offset: 0x0003E129
	private void Update()
	{
	}

	// Token: 0x04000540 RID: 1344
	private Text TipText;

	// Token: 0x04000541 RID: 1345
	private GameObject pNameInputField;

	// Token: 0x04000542 RID: 1346
	private GameObject pCreateBtn_1;

	// Token: 0x04000543 RID: 1347
	private GameObject pCreateBtn_2;

	// Token: 0x04000544 RID: 1348
	private GameObject pCloseBtn;

	// Token: 0x04000545 RID: 1349
	private InputField pInputName;

	// Token: 0x04000546 RID: 1350
	private Text TipTextTiShi;

	// Token: 0x04000547 RID: 1351
	public static CreateGuildUICon inst;

	// Token: 0x04000548 RID: 1352
	private Transform inputCaret;

	// Token: 0x04000549 RID: 1353
	private int nBuyType = 1;
}
