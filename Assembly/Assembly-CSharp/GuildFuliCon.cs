using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015C RID: 348
public class GuildFuliCon : Window
{
	// Token: 0x06000FC2 RID: 4034 RVA: 0x00090CC8 File Offset: 0x0008EEC8
	protected override void Awake()
	{
		base.Awake();
		this.dayHuoyueTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(1);
		this.szDayReward.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(2);
		this.szDayReward.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(3);
		this.szDayReward.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(4);
		this.szDayReward.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(5);
		this.szDayReward.Add(gameObject.GetComponent<Toggle>());
		this.weekHuoyueTxt = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.guildLvlTxt = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(10);
		this.szWeekReward.Add(gameObject);
		gameObject = gameObject.transform.Find("Icon").gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWeekIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWeekIcon);
		gameObject = this.UIGameObj.Get_GameObj(11);
		this.szWeekReward.Add(gameObject);
		gameObject = gameObject.transform.Find("Icon").gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWeekIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWeekIcon);
		gameObject = this.UIGameObj.Get_GameObj(12);
		this.szWeekReward.Add(gameObject);
		gameObject = gameObject.transform.Find("Icon").gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWeekIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWeekIcon);
		gameObject = this.UIGameObj.Get_GameObj(13);
		this.szWeekReward.Add(gameObject);
		gameObject = gameObject.transform.Find("Icon").gameObject;
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverWeekIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitWeekIcon);
		GuildFuliCon.inst = this;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00090F40 File Offset: 0x0008F140
	private void Start()
	{
		this.InitData();
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00090F4C File Offset: 0x0008F14C
	public void InitData()
	{
		bool flag = GuildFuliCon.inst == null || GuildManager.GetInstance().m_nGuildId == 0;
		if (!flag)
		{
			this.dayHuoyueTxt.text = Language.GetLang(46142, new object[]
			{
				GuildManager.GetInstance().m_nDayHuoyue
			});
			for (int i = 0; i < this.szDayReward.Count; i++)
			{
				GuildFuliDay guildFuliDay = fGameManager.mloadConfig.AllGuildFuliDay[i + 1];
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(guildFuliDay.szReward[0])];
				this.szDayReward[i].isOn = false;
				Text component = this.szDayReward[i].transform.Find("Label").GetComponent<Text>();
				component.text = guildFuliDay.sTitle;
				RawImage component2 = this.szDayReward[i].transform.Find("RawImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
				component = this.szDayReward[i].transform.Find("ItemName").GetComponent<Text>();
				bool flag2 = int.Parse(guildFuliDay.szReward[1]) > 1;
				if (flag2)
				{
					component.text = string.Concat(new string[]
					{
						"<color=",
						Fun.GetQualColor(itemData.m_nQuality),
						">",
						string.Format(itemData.m_sLiBaoDesc, guildFuliDay.szReward[1]),
						"</color>"
					});
				}
				else
				{
					component.text = string.Concat(new string[]
					{
						"<color=",
						Fun.GetQualColor(itemData.m_nQuality),
						">",
						itemData.m_sName,
						"</color>"
					});
				}
				component = this.szDayReward[i].transform.Find("DescTxt").GetComponent<Text>();
				GameObject gameObject = this.szDayReward[i].transform.Find("Button").gameObject;
				GameObject gameObject2 = this.szDayReward[i].transform.Find("Slider").gameObject;
				int guildFuliState = GuildManager.GetInstance().GetGuildFuliState(2, i + 1);
				int num = guildFuliState;
				if (num != 1)
				{
					if (num != 2)
					{
						component.text = Language.GetLang(46148, new object[]
						{
							GuildManager.GetInstance().m_nDayHuoyue,
							guildFuliDay.nNeednum
						});
						gameObject2.SetActive(true);
						Image component3 = gameObject2.transform.Find("HuoyueSlider").GetComponent<Image>();
						component3.fillAmount = (float)GuildManager.GetInstance().m_nDayHuoyue * 1f / (float)guildFuliDay.nNeednum * 1f;
						gameObject.SetActive(false);
					}
					else
					{
						component.text = Language.GetLang(42006, new object[0]);
						gameObject2.SetActive(false);
						gameObject.SetActive(false);
					}
				}
				else
				{
					component.text = string.Empty;
					gameObject2.SetActive(false);
					gameObject.SetActive(true);
					component = gameObject.transform.Find("Text").GetComponent<Text>();
					component.text = Language.GetLang(42052, new object[0]);
					EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDayReward);
					EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
			}
			this.weekHuoyueTxt.text = Language.GetLang(46146, new object[]
			{
				GuildManager.GetInstance().m_nWeekHuoyue
			});
			this.guildLvlTxt.text = string.Empty;
			for (int j = 0; j < this.szWeekReward.Count; j++)
			{
				GuildFuliWeek guildFuliWeek = fGameManager.mloadConfig.AllGuildFuliWeek[j + 1];
				ItemData itemData2 = fGameManager.mloadConfig.AllItemData[int.Parse(guildFuliWeek.szReward[0])];
				Text component4 = this.szWeekReward[j].transform.Find("Title").GetComponent<Text>();
				bool flag3 = int.Parse(guildFuliWeek.szReward[1]) > 1;
				if (flag3)
				{
					component4.text = string.Concat(new string[]
					{
						"<color=",
						Fun.GetQualColor(itemData2.m_nQuality),
						">",
						string.Format(itemData2.m_sLiBaoDesc, guildFuliWeek.szReward[1]),
						"</color>"
					});
				}
				else
				{
					component4.text = string.Concat(new string[]
					{
						"<color=",
						Fun.GetQualColor(itemData2.m_nQuality),
						">",
						itemData2.m_sName,
						"</color>"
					});
				}
				component4 = this.szWeekReward[j].transform.Find("DescTxt").GetComponent<Text>();
				GameObject gameObject3 = this.szWeekReward[j].transform.Find("Button").gameObject;
				int guildFuliState2 = GuildManager.GetInstance().GetGuildFuliState(3, j + 1);
				int num2 = guildFuliState2;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						component4.text = Language.GetLang(46151, new object[]
						{
							guildFuliWeek.nNeedTotal
						});
						gameObject3.SetActive(false);
					}
					else
					{
						component4.text = Language.GetLang(42006, new object[0]);
						gameObject3.SetActive(false);
					}
				}
				else
				{
					component4.text = string.Empty;
					gameObject3.SetActive(true);
					component4 = gameObject3.transform.Find("Text").GetComponent<Text>();
					component4.text = Language.GetLang(42052, new object[0]);
					EventTriggerListener.Get(gameObject3).onClick = new EventTriggerListener.VoidDelegate(this.OnClickWeekReward);
					EventTriggerListener.Get(gameObject3).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
			}
		}
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000915D0 File Offset: 0x0008F7D0
	private void OnClickWeekReward(GameObject go)
	{
		int num = int.Parse(go.transform.parent.name.Replace("WeekReward", string.Empty));
		int guildFuliState = GuildManager.GetInstance().GetGuildFuliState(3, num);
		bool flag = guildFuliState == 1;
		if (flag)
		{
			bool flag2 = Time.time - this.delayTime < 1f;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
			}
			else
			{
				this.delayTime = Time.time;
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91037,
					new ArrayList
					{
						2,
						num
					}
				});
			}
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000916B4 File Offset: 0x0008F8B4
	private void OnClickDayReward(GameObject go)
	{
		int num = int.Parse(go.transform.parent.name.Replace("DayReward", string.Empty));
		int guildFuliState = GuildManager.GetInstance().GetGuildFuliState(2, num);
		bool flag = guildFuliState == 1;
		if (flag)
		{
			bool flag2 = Time.time - this.delayTime < 1f;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
			}
			else
			{
				this.delayTime = Time.time;
				GuildManager.GetInstance().OnSendMsgToGuildServer(91000, new ArrayList
				{
					91037,
					new ArrayList
					{
						1,
						num
					}
				});
			}
		}
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00091797 File Offset: 0x0008F997
	private void ExitWeekIcon(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000917A8 File Offset: 0x0008F9A8
	private void OverWeekIcon(GameObject go)
	{
		bool flag = GuildManager.GetInstance().m_nGuildId != 0;
		if (flag)
		{
			int key = int.Parse(go.transform.parent.name.Replace("WeekReward", string.Empty));
			bool flag2 = fGameManager.mloadConfig.AllGuildFuliWeek.ContainsKey(key);
			if (flag2)
			{
				GuildFuliWeek guildFuliWeek = fGameManager.mloadConfig.AllGuildFuliWeek[key];
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(guildFuliWeek.szReward[0])];
				TipManager.GetInstance().ShowTextTip(itemData.m_sTipDesc, TextAnchor.MiddleLeft, 1f, 20f, -20f);
			}
		}
	}

	// Token: 0x04000C84 RID: 3204
	public static GuildFuliCon inst;

	// Token: 0x04000C85 RID: 3205
	private Text dayHuoyueTxt;

	// Token: 0x04000C86 RID: 3206
	private List<Toggle> szDayReward = new List<Toggle>();

	// Token: 0x04000C87 RID: 3207
	private Text weekHuoyueTxt;

	// Token: 0x04000C88 RID: 3208
	private Text guildLvlTxt;

	// Token: 0x04000C89 RID: 3209
	private List<GameObject> szWeekReward = new List<GameObject>();

	// Token: 0x04000C8A RID: 3210
	private float delayTime;
}
