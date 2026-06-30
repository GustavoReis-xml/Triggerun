using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000032 RID: 50
public class BOGOActUICon : Window
{
	// Token: 0x060001DE RID: 478 RVA: 0x0000C100 File Offset: 0x0000A300
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.titleText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.descText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.numText = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.bottomText = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.rewardBtn = this.UIGameObj.Get_GameObj(4).GetComponent<Button>();
		this.rewardBtnTxt = this.rewardBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.rewardBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRewardBtn);
		EventTriggerListener.Get(this.rewardBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.timeText = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		BOGOActUICon.inst = this;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000C212 File Offset: 0x0000A412
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000C21C File Offset: 0x0000A41C
	public void Update()
	{
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000C220 File Offset: 0x0000A420
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(29);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			BOGOActData bogoactData = fGameManager.mloadConfig.AllBOGOActData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + bogoactData.sIconName + ".txt", this.bgRimg);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.ToString("yyyy/MM/dd")
			});
			ArrayList szBOGOActData = LiBaoManager.GetInstance().m_szBOGOActData;
			int num = (int)LiBaoManager.GetInstance().m_szBOGOActData[2];
			int num2 = bogoactData.nMaxNum - num;
			ItemData itemData = fGameManager.mloadConfig.AllItemData[bogoactData.nGiftItem];
			bool flag2 = (int)LiBaoManager.GetInstance().m_szBOGOActData[3] == 0;
			if (flag2)
			{
				this.titleText.text = string.Format(bogoactData.sTitle2, itemData.m_sName);
				this.descText.text = bogoactData.sDesc2;
				this.bottomText.text = string.Format(bogoactData.sBottom2, num2);
				this.numText.text = string.Empty;
				this.rewardBtnTxt.text = Language.GetLang(42074, new object[0]);
			}
			else
			{
				this.titleText.text = string.Format(bogoactData.sTitle1, num, itemData.m_sName);
				this.descText.text = bogoactData.sDesc1;
				this.bottomText.text = bogoactData.sBottom1;
				int num3 = num / int.Parse(bogoactData.szRate[0]) * int.Parse(bogoactData.szRate[1]);
				this.numText.text = string.Format("<color=#2acf0b>{0}*{1}</color>", itemData.m_sName, num3);
				this.rewardBtnTxt.text = Language.GetLang(41006, new object[0]);
			}
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000C490 File Offset: 0x0000A690
	private void OnClickRewardBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(29);
		bool flag = actDataByType != null;
		if (flag)
		{
			bool flag2 = (int)LiBaoManager.GetInstance().m_szBOGOActData[3] == 0;
			if (flag2)
			{
				BOGOActData bogoactData = fGameManager.mloadConfig.AllBOGOActData[actDataByType.nXmlId];
				ShopManager.GetInstance().OnBuyItem(bogoactData.nBuyItem);
			}
			else
			{
				fGameManager.SendMsgToHallServer(1315, new ArrayList
				{
					15030,
					new ArrayList()
				});
			}
		}
	}

	// Token: 0x04000180 RID: 384
	public static BOGOActUICon inst;

	// Token: 0x04000181 RID: 385
	private RawImage bgRimg;

	// Token: 0x04000182 RID: 386
	private Text titleText;

	// Token: 0x04000183 RID: 387
	private Text descText;

	// Token: 0x04000184 RID: 388
	private Text numText;

	// Token: 0x04000185 RID: 389
	private Text bottomText;

	// Token: 0x04000186 RID: 390
	private Button rewardBtn;

	// Token: 0x04000187 RID: 391
	private Text rewardBtnTxt;

	// Token: 0x04000188 RID: 392
	private Text timeText;
}
