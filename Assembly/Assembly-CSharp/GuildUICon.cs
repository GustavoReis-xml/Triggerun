using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000161 RID: 353
public class GuildUICon : Window
{
	// Token: 0x06001021 RID: 4129 RVA: 0x00095FD0 File Offset: 0x000941D0
	protected override void Awake()
	{
		base.Awake();
		GuildUICon.inst = this;
		this.pToggleBtn_1 = this.UIGameObj.Get_GameObj(0);
		this.pToggleBtn_2 = this.UIGameObj.Get_GameObj(1);
		this.pToggleBtn_3 = this.UIGameObj.Get_GameObj(5);
		this.pMyGuildInfo = this.UIGameObj.Get_GameObj(2);
		this.pOtherGuildList = this.UIGameObj.Get_GameObj(3);
		this.pGuildFuli = this.UIGameObj.Get_GameObj(4);
		this.MyGuildCon = this.pMyGuildInfo.AddComponent<GuildInfoCon>();
		this.OtherListCon = this.pOtherGuildList.AddComponent<OtherGuildListCon>();
		this.MyGuildFuli = this.pGuildFuli.AddComponent<GuildFuliCon>();
		EventTriggerListener.Get(this.pToggleBtn_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.pToggleBtn_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.pToggleBtn_3).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
		EventTriggerListener.Get(this.pToggleBtn_1).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pToggleBtn_2).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pToggleBtn_3).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x00096139 File Offset: 0x00094339
	private void Start()
	{
		this.OnChangeToggle();
		this.ShowGuildFuli();
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0009614C File Offset: 0x0009434C
	public override void Reset()
	{
		base.Reset();
		bool flag = GuildManager.GetInstance().m_nGuildId == 0;
		if (flag)
		{
			GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
			{
				91013,
				new ArrayList
				{
					1,
					1,
					0
				}
			});
		}
		else
		{
			bool flag2 = this.m_nPanelType == 2;
			if (flag2)
			{
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91013,
					new ArrayList
					{
						1,
						1,
						0
					}
				});
			}
			else
			{
				bool flag3 = this.m_nPanelType == 3;
				if (flag3)
				{
					GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
					{
						91036,
						new ArrayList
						{
							0,
							0
						}
					});
				}
				else
				{
					GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
					{
						91018,
						new ArrayList
						{
							0,
							0
						}
					});
				}
			}
			this.ShowGuildFuli();
		}
		bool flag4 = OtherGuildListCon.inst != null;
		if (flag4)
		{
			OtherGuildListCon.inst.IsShowCreateBtn();
		}
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0009630F File Offset: 0x0009450F
	private void OnClickToggle(GameObject obj)
	{
		this.OnToggle(obj, true);
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0009631C File Offset: 0x0009451C
	private void OnToggle(GameObject obj, bool isClick = false)
	{
		int num = int.Parse(obj.name.Replace("Toggle", string.Empty));
		bool flag = this.m_nPanelType != num;
		if (flag)
		{
			this.m_nPanelType = num;
			switch (this.m_nPanelType)
			{
			case 1:
				this.pMyGuildInfo.SetActive(true);
				this.pOtherGuildList.SetActive(false);
				this.pGuildFuli.SetActive(false);
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91018,
					new ArrayList
					{
						0,
						0
					}
				});
				break;
			case 2:
				this.pMyGuildInfo.SetActive(false);
				this.pOtherGuildList.SetActive(true);
				this.pGuildFuli.SetActive(false);
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91013,
					new ArrayList
					{
						1,
						1,
						0
					}
				});
				break;
			case 3:
				this.pGuildFuli.SetActive(true);
				this.pMyGuildInfo.SetActive(false);
				this.pOtherGuildList.SetActive(false);
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91036,
					new ArrayList
					{
						0,
						0
					}
				});
				break;
			}
			bool flag2 = this.OnSelectToggleBtn != null;
			if (flag2)
			{
				this.OnSelectToggleBtn.GetComponent<Toggle>().enabled = true;
				this.OnSelectToggleBtn.GetComponent<Toggle>().interactable = true;
			}
			if (isClick)
			{
				obj.GetComponent<Toggle>().enabled = false;
			}
			obj.GetComponent<Toggle>().interactable = false;
			this.OnSelectToggleBtn = obj;
		}
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x00096554 File Offset: 0x00094754
	public void OnGuildInfo(ArrayList szData)
	{
		bool flag = this.MyGuildCon != null;
		if (flag)
		{
			this.MyGuildCon.InitData(szData);
		}
		bool flag2 = this.MyGuildFuli != null;
		if (flag2)
		{
			this.MyGuildFuli.InitData();
		}
		this.OnChangeToggle();
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000965A8 File Offset: 0x000947A8
	public void UpdateMemberList(ArrayList szData)
	{
		bool flag = this.MyGuildCon != null;
		if (flag)
		{
			this.MyGuildCon.OnUpdateMemberList((ArrayList)szData[0], (int)szData[1]);
		}
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x000965EC File Offset: 0x000947EC
	public void OnChangeToggle()
	{
		bool flag = GuildManager.GetInstance().m_nGuildId == 0;
		if (flag)
		{
			this.pToggleBtn_1.SetActive(false);
			this.pToggleBtn_3.SetActive(false);
			this.m_nPanelType = 0;
			this.OnToggle(this.pToggleBtn_2, false);
		}
		else
		{
			this.pToggleBtn_1.SetActive(true);
			this.pToggleBtn_3.SetActive(true);
			bool flag2 = this.m_nPanelType == 2 || this.m_nPanelType == 0;
			if (flag2)
			{
				this.OnToggle(this.pToggleBtn_1, false);
			}
		}
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x00096680 File Offset: 0x00094880
	public void OnOtherGuildList(ArrayList szData, ArrayList szRequest, ArrayList szCount, string sFindGuildName = "")
	{
		bool flag = this.OtherListCon != null;
		if (flag)
		{
			this.OtherListCon.InitData(szData, szRequest, szCount, sFindGuildName);
		}
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x000966B4 File Offset: 0x000948B4
	public void OnClearUserRequest()
	{
		bool flag = this.OtherListCon != null;
		if (flag)
		{
			this.OtherListCon.OnClearUserRequest();
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x000966E0 File Offset: 0x000948E0
	public void OnUpdateOneGuild(int nGuildId, ArrayList szInfo)
	{
		bool flag = this.OtherListCon != null;
		if (flag)
		{
			this.OtherListCon.OnUpdateOneGuild(nGuildId, szInfo);
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x00096710 File Offset: 0x00094910
	public void OnGetGuildRequestList(ArrayList szInfo)
	{
		bool flag = this.MyGuildCon != null;
		if (flag)
		{
			this.MyGuildCon.OnSetRequestData(szInfo);
		}
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x00096740 File Offset: 0x00094940
	public void OnSaveNotice(string sMsg)
	{
		bool flag = this.MyGuildCon != null;
		if (flag)
		{
			this.MyGuildCon.OnSaveNotice(sMsg);
		}
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x00096770 File Offset: 0x00094970
	public void ShowGuildFuli()
	{
		int guildfuliReward = GuildManager.GetInstance().GetGuildfuliReward();
		Image component = this.pToggleBtn_3.transform.Find("Image").GetComponent<Image>();
		component.enabled = (guildfuliReward == 1);
	}

	// Token: 0x04000CD8 RID: 3288
	public static GuildUICon inst;

	// Token: 0x04000CD9 RID: 3289
	private GameObject pToggleBtn_1;

	// Token: 0x04000CDA RID: 3290
	private GameObject pToggleBtn_2;

	// Token: 0x04000CDB RID: 3291
	private GameObject pToggleBtn_3;

	// Token: 0x04000CDC RID: 3292
	private GameObject pMyGuildInfo;

	// Token: 0x04000CDD RID: 3293
	private GameObject pOtherGuildList;

	// Token: 0x04000CDE RID: 3294
	private GameObject pGuildFuli;

	// Token: 0x04000CDF RID: 3295
	private GameObject OnSelectToggleBtn;

	// Token: 0x04000CE0 RID: 3296
	private GuildInfoCon MyGuildCon;

	// Token: 0x04000CE1 RID: 3297
	private OtherGuildListCon OtherListCon;

	// Token: 0x04000CE2 RID: 3298
	private GuildFuliCon MyGuildFuli;

	// Token: 0x04000CE3 RID: 3299
	private int m_nPanelType;
}
