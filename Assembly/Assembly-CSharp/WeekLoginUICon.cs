using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A2 RID: 1186
public class WeekLoginUICon : Window
{
	// Token: 0x06002DCB RID: 11723 RVA: 0x00194544 File Offset: 0x00192744
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.curWeekUI = this.UIGameObj.Get_GameObj(0);
		this.curWeekTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		RawImage component = this.UIGameObj.Get_GameObj(2).GetComponent<RawImage>();
		this.curWeekIcon.Add(component);
		Text component2 = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.curWeekName.Add(component2);
		component = this.UIGameObj.Get_GameObj(4).GetComponent<RawImage>();
		this.curWeekIcon.Add(component);
		component2 = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.curWeekName.Add(component2);
		component = this.UIGameObj.Get_GameObj(6).GetComponent<RawImage>();
		this.curWeekIcon.Add(component);
		component2 = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.curWeekName.Add(component2);
		this.curWeekBtn = this.UIGameObj.Get_GameObj(8).GetComponent<Button>();
		EventTriggerListener.Get(this.curWeekBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCurWeekBtn);
		EventTriggerListener.Get(this.curWeekBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.curWeekBtnTxt = this.curWeekBtn.transform.Find("Text").GetComponent<Text>();
		this.nextWeekUI = this.UIGameObj.Get_GameObj(9);
		this.nextWeekTxt = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		component = this.UIGameObj.Get_GameObj(11).GetComponent<RawImage>();
		this.nextWeekIcon.Add(component);
		component2 = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.nextWeekName.Add(component2);
		component = this.UIGameObj.Get_GameObj(13).GetComponent<RawImage>();
		this.nextWeekIcon.Add(component);
		component2 = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.nextWeekName.Add(component2);
		component = this.UIGameObj.Get_GameObj(15).GetComponent<RawImage>();
		this.nextWeekIcon.Add(component);
		component2 = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.nextWeekName.Add(component2);
		this.nextWeekBtn = this.UIGameObj.Get_GameObj(17).GetComponent<Button>();
		this.nextWeekBtnTxt = this.nextWeekBtn.transform.Find("Text").GetComponent<Text>();
		WeekLoginUICon.inst = this;
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x001947E9 File Offset: 0x001929E9
	private void Start()
	{
		this.ShowWeekReward();
	}

	// Token: 0x06002DCD RID: 11725 RVA: 0x001947F3 File Offset: 0x001929F3
	public void Update()
	{
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x001947F8 File Offset: 0x001929F8
	public void ShowWeekReward()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(20);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dDate = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
			DateTime dDate2 = DateTime.Parse(DateTime.Parse(actDataByType.sStartTime).ToString("yyyy-MM-dd"));
			DateTime dDate3 = DateTime.Parse(DateTime.Parse(actDataByType.sEndTime).ToString("yyyy-MM-dd"));
			this.nCurWeek = Fun.GetWeek2(dDate) - Fun.GetWeek2(dDate2) + 1;
			this.nNextWeek = this.nCurWeek + 1;
			int num = Fun.GetWeek2(dDate3) - Fun.GetWeek2(dDate2);
			bool flag2 = dDate3.DayOfWeek > DayOfWeek.Monday;
			if (flag2)
			{
				num++;
			}
			bool flag3 = this.nNextWeek > num;
			if (flag3)
			{
				this.nNextWeek = 0;
			}
			WeekLoginData weekLoginData = fGameManager.mloadConfig.AllWeekLogin[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + weekLoginData.sIconName + ".txt", this.bgRimg);
			int num2 = weekLoginData.szWeekReward.IndexOf(this.nCurWeek);
			ArrayList arrayList = (ArrayList)weekLoginData.szWeekReward[num2 + 1];
			for (int i = 0; i < this.curWeekIcon.Count; i++)
			{
				GetIconComponent component = this.curWeekIcon[i].transform.GetComponent<GetIconComponent>();
				bool flag4 = component != null;
				if (flag4)
				{
					component._Url = string.Empty;
				}
				this.curWeekIcon[i].enabled = false;
				this.curWeekName[i].text = string.Empty;
				bool flag5 = i * 2 < arrayList.Count;
				if (flag5)
				{
					int key = (int)arrayList[i * 2];
					ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), this.curWeekIcon[i]);
					this.curWeekName[i].text = string.Format("<color=#2acf0b>{0}*{1}</color>", itemData.m_sName, arrayList[i * 2 + 1]);
				}
			}
			bool flag6 = LiBaoManager.GetInstance().GetWeekLoginReward() == 0;
			if (flag6)
			{
				this.curWeekBtn.interactable = true;
				this.curWeekBtnTxt.text = Language.GetLang(42052, new object[0]);
			}
			else
			{
				this.curWeekBtn.interactable = false;
				this.curWeekBtnTxt.text = Language.GetLang(42006, new object[0]);
			}
			num2 = weekLoginData.szWeekReward.IndexOf(this.nNextWeek);
			bool flag7 = num2 == -1;
			if (flag7)
			{
				this.nextWeekUI.SetActive(false);
			}
			else
			{
				this.nextWeekUI.SetActive(true);
				arrayList = (ArrayList)weekLoginData.szWeekReward[num2 + 1];
				for (int j = 0; j < this.nextWeekIcon.Count; j++)
				{
					this.nextWeekIcon[j].enabled = false;
					this.nextWeekName[j].text = string.Empty;
					bool flag8 = j * 2 < arrayList.Count;
					if (flag8)
					{
						int key2 = (int)arrayList[j * 2];
						ItemData itemData2 = fGameManager.mloadConfig.AllItemData[key2];
						fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData2.m_nType), itemData2.m_sIco), this.nextWeekIcon[j]);
						this.nextWeekName[j].text = string.Format("<color=#2acf0b>{0}*{1}</color>", itemData2.m_sName, arrayList[j * 2 + 1]);
					}
				}
				this.nextWeekBtn.interactable = false;
				this.nextWeekBtnTxt.text = Language.GetLang(42053, new object[0]);
			}
		}
	}

	// Token: 0x06002DCF RID: 11727 RVA: 0x00194C54 File Offset: 0x00192E54
	private void OnCurWeekBtn(GameObject go)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.nCurWeek);
		arrayList.Add(0);
		fGameManager.SendMsgToHallServer(1315, new ArrayList
		{
			15020,
			arrayList
		});
	}

	// Token: 0x04002716 RID: 10006
	public static WeekLoginUICon inst;

	// Token: 0x04002717 RID: 10007
	private RawImage bgRimg;

	// Token: 0x04002718 RID: 10008
	private GameObject curWeekUI;

	// Token: 0x04002719 RID: 10009
	private Text curWeekTxt;

	// Token: 0x0400271A RID: 10010
	private List<RawImage> curWeekIcon = new List<RawImage>();

	// Token: 0x0400271B RID: 10011
	private List<Text> curWeekName = new List<Text>();

	// Token: 0x0400271C RID: 10012
	private Button curWeekBtn;

	// Token: 0x0400271D RID: 10013
	private Text curWeekBtnTxt;

	// Token: 0x0400271E RID: 10014
	private GameObject nextWeekUI;

	// Token: 0x0400271F RID: 10015
	private Text nextWeekTxt;

	// Token: 0x04002720 RID: 10016
	private List<RawImage> nextWeekIcon = new List<RawImage>();

	// Token: 0x04002721 RID: 10017
	private List<Text> nextWeekName = new List<Text>();

	// Token: 0x04002722 RID: 10018
	private Button nextWeekBtn;

	// Token: 0x04002723 RID: 10019
	private Text nextWeekBtnTxt;

	// Token: 0x04002724 RID: 10020
	private int nCurWeek;

	// Token: 0x04002725 RID: 10021
	private int nNextWeek;
}
