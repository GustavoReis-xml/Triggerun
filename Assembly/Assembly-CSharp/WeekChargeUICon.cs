using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200049D RID: 1181
public class WeekChargeUICon : Window
{
	// Token: 0x06002DB8 RID: 11704 RVA: 0x00193638 File Offset: 0x00191838
	protected override void Awake()
	{
		base.Awake();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = base.transform.Find("TitileMsg/Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
		}
		this.numText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(1);
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			Text component2 = gameObject.transform.Find("Text").GetComponent<Text>();
			component2.font = fUIManager.mDINMedium;
		}
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChargeBtnClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.rewardRender = this.UIGameObj.Get_GameObj(2);
		this.rewardRender.SetActive(false);
		this.descText = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		WeekChargeUICon.inst = this;
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x0019374A File Offset: 0x0019194A
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x00193754 File Offset: 0x00191954
	public void Update()
	{
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x00193758 File Offset: 0x00191958
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(27);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
			DateTime dateTime2 = DateTime.Parse(DateTime.Parse(actDataByType.sStartTime).ToString("yyyy-MM-dd"));
			DateTime dateTime3 = DateTime.Parse(DateTime.Parse(actDataByType.sEndTime).ToString("yyyy-MM-dd"));
			this.nCurWeek = 1;
			WeekChargeData weekChargeData = fGameManager.mloadConfig.AllWeekCharge[actDataByType.nXmlId];
			int weekChargeNum = LiBaoManager.GetInstance().GetWeekChargeNum();
			this.numText.text = weekChargeNum.ToString();
			this.descText.text = weekChargeData.sDescTxt;
			WeekChargeReward weekChargeReward = weekChargeData.allChargeReward[this.nCurWeek];
			fUIManager.OnClearObj(this.szReward);
			for (int i = 0; i < weekChargeReward.szReward.Count; i++)
			{
				ArrayList arrayList = (ArrayList)weekChargeReward.szReward[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rewardRender);
				gameObject.transform.SetParent(this.rewardRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.name = string.Format("Reward{0}_{1}", weekChargeReward.nWeek, i);
				gameObject.SetActive(true);
				this.szReward.Add(gameObject);
				RawImage component = gameObject.transform.Find("RewardImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset("libaoico/" + arrayList[1].ToString() + ".txt", component);
				Text component2 = gameObject.transform.Find("RewardTitle").GetComponent<Text>();
				component2.text = string.Format(weekChargeReward.sTitle, arrayList[0].ToString());
				component2 = gameObject.transform.Find("RewardMsg").GetComponent<Text>();
				component2.text = arrayList[2].ToString();
				int weekChargeReward2 = LiBaoManager.GetInstance().GetWeekChargeReward(weekChargeReward.nWeek, i);
				Button component3 = gameObject.transform.Find("RewardBtn").GetComponent<Button>();
				component2 = gameObject.transform.Find("RewardBtn/Text").GetComponent<Text>();
				int num = weekChargeReward2;
				int num2 = num;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						component3.interactable = false;
						component2.text = Language.GetLang(42011, new object[0]);
					}
					else
					{
						component3.interactable = false;
						component2.text = Language.GetLang(42006, new object[0]);
					}
				}
				else
				{
					component3.interactable = true;
					component2.text = Language.GetLang(42052, new object[0]);
					EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardBtnClick);
					EventTriggerListener.Get(component3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
				}
			}
		}
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x00193ACD File Offset: 0x00191CCD
	private void OnChargeBtnClick(GameObject go)
	{
		fUIManager.GetInstance().OpenChongZhiUI("WeekChargeUICon");
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x00193AE0 File Offset: 0x00191CE0
	private void OnRewardBtnClick(GameObject go)
	{
		string[] array = go.transform.parent.name.Replace("Reward", string.Empty).Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[0]);
		int num2 = int.Parse(array[1]);
		int weekChargeReward = LiBaoManager.GetInstance().GetWeekChargeReward(num, num2);
		bool flag = weekChargeReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15027,
				new ArrayList
				{
					num,
					num2
				}
			});
		}
	}

	// Token: 0x040026FB RID: 9979
	public static WeekChargeUICon inst;

	// Token: 0x040026FC RID: 9980
	private Text numText;

	// Token: 0x040026FD RID: 9981
	private GameObject rewardRender;

	// Token: 0x040026FE RID: 9982
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x040026FF RID: 9983
	private Text descText;

	// Token: 0x04002700 RID: 9984
	private int nCurWeek;
}
