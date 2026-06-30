using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A0 RID: 1184
public class WeekendOnlineUICon : Window
{
	// Token: 0x06002DC2 RID: 11714 RVA: 0x00193D7C File Offset: 0x00191F7C
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.descTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.rewardRender = this.UIGameObj.Get_GameObj(0);
		this.rewardRender.SetActive(false);
		WeekendOnlineUICon.inst = this;
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x00193DD9 File Offset: 0x00191FD9
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x00193DE4 File Offset: 0x00191FE4
	public void Update()
	{
		int dayOfWeek = (int)Fun.GetServerDate().DayOfWeek;
		bool flag = this.nWeekDay != dayOfWeek;
		if (flag)
		{
			this.ShowUIData();
		}
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x00193E19 File Offset: 0x00192019
	public override void Reset()
	{
		base.Reset();
		this.ShowUIData();
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x00193E2C File Offset: 0x0019202C
	public void ShowUIData()
	{
		fUIManager.OnClearObj(this.szReward);
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(23);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllWeekendOnline.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			WeekendOnlineData weekendOnlineData = fGameManager.mloadConfig.AllWeekendOnline[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + weekendOnlineData.sIconName + ".txt", this.bgRimg);
			this.descTxt.text = weekendOnlineData.sDescText;
			DateTime serverDate = Fun.GetServerDate();
			this.nWeekDay = (int)serverDate.DayOfWeek;
			foreach (WeekendOnlineReward weekendOnlineReward in weekendOnlineData.szReward.Values)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rewardRender);
				gameObject.transform.SetParent(this.rewardRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.name = string.Format("Render{0}", weekendOnlineReward.nIndex);
				gameObject.SetActive(true);
				this.szReward.Add(gameObject);
				Text component = gameObject.transform.Find("Title").GetComponent<Text>();
				component.text = weekendOnlineReward.sOnlineTitle;
				RawImage component2 = gameObject.transform.Find("RawImage").GetComponent<RawImage>();
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(weekendOnlineReward.szReward[0])];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
				component = gameObject.transform.Find("ItemName").GetComponent<Text>();
				component.text = string.Concat(new string[]
				{
					"<color=#2acf0b>",
					itemData.m_sName,
					"*",
					weekendOnlineReward.szReward[1],
					"</color>"
				});
				Button component3 = gameObject.transform.Find("GetReward").GetComponent<Button>();
				component = gameObject.transform.Find("GetReward/Text").GetComponent<Text>();
				EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardClick);
				EventTriggerListener.Get(component3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				int num = this.nWeekDay;
				bool flag2 = num == 0;
				if (flag2)
				{
					num = 7;
				}
				DateTime dateTime = DateTime.Parse(serverDate.AddDays((double)(weekendOnlineReward.nOnlineDay - num)).ToString("yyyy-MM-dd " + weekendOnlineReward.sStartTime));
				DateTime t = DateTime.Parse(serverDate.AddDays((double)(weekendOnlineReward.nOnlineDay - num)).ToString("yyyy-MM-dd " + weekendOnlineReward.sEndTime));
				int weekendOnlineReward2 = LiBaoManager.GetInstance().GetWeekendOnlineReward(weekendOnlineReward.nOnlineDay);
				int num2 = weekendOnlineReward2;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						component3.interactable = false;
						bool flag3 = serverDate > t;
						if (flag3)
						{
							component.text = Language.GetLang(42062, new object[0]);
						}
						else
						{
							component.text = Language.GetLang(25008, new object[0]);
						}
					}
					else
					{
						component3.interactable = false;
						component.text = Language.GetLang(42006, new object[0]);
					}
				}
				else
				{
					component3.interactable = true;
					bool flag4 = num != weekendOnlineReward.nOnlineDay;
					if (flag4)
					{
						component.text = Language.GetLang(42052, new object[0]);
					}
					else
					{
						component.text = Language.GetLang(42060, new object[0]);
					}
				}
			}
		}
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x00194284 File Offset: 0x00192484
	private void OnRewardClick(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(23);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllWeekendOnline.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			WeekendOnlineData weekendOnlineData = fGameManager.mloadConfig.AllWeekendOnline[actDataByType.nXmlId];
			int key = int.Parse(go.transform.parent.name.Replace("Render", string.Empty));
			bool flag2 = weekendOnlineData.szReward.ContainsKey(key);
			if (flag2)
			{
				WeekendOnlineReward weekendOnlineReward = weekendOnlineData.szReward[key];
				int num = (int)Fun.GetServerDate().DayOfWeek;
				bool flag3 = num == 0;
				if (flag3)
				{
					num = 7;
				}
				int weekendOnlineReward2 = LiBaoManager.GetInstance().GetWeekendOnlineReward(weekendOnlineReward.nOnlineDay);
				bool flag4 = weekendOnlineReward2 == 1 && num != weekendOnlineReward.nOnlineDay;
				if (flag4)
				{
					fGameManager.SendMsgToHallServer(1315, new ArrayList
					{
						15023,
						new ArrayList
						{
							weekendOnlineReward.nOnlineDay,
							0
						}
					});
				}
			}
		}
	}

	// Token: 0x0400270C RID: 9996
	public static WeekendOnlineUICon inst;

	// Token: 0x0400270D RID: 9997
	private RawImage bgRimg;

	// Token: 0x0400270E RID: 9998
	private Text descTxt;

	// Token: 0x0400270F RID: 9999
	private GameObject rewardRender;

	// Token: 0x04002710 RID: 10000
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x04002711 RID: 10001
	private int nWeekDay;
}
