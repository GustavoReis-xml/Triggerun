using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024C RID: 588
public class MultiBOGOUICon : Window
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x000FCAA0 File Offset: 0x000FACA0
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
		MultiBOGOUICon.inst = this;
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x000FCBB2 File Offset: 0x000FADB2
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000FCBBC File Offset: 0x000FADBC
	public void Update()
	{
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x000FCBC0 File Offset: 0x000FADC0
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(45);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			MultiBOGOData multiBOGOData = fGameManager.mloadConfig.AllMultiBOGOData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + multiBOGOData.sIconName + ".txt", this.bgRimg);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.ToString("yyyy/MM/dd")
			});
			ArrayList szMultiBOGOData = LiBaoManager.GetInstance().m_szMultiBOGOData;
			int num = (int)LiBaoManager.GetInstance().m_szMultiBOGOData[2];
			int num2 = multiBOGOData.nMaxNum - num;
			ItemData itemData = fGameManager.mloadConfig.AllItemData[multiBOGOData.nGiftItem];
			bool flag2 = (int)szMultiBOGOData[2] == 0;
			if (flag2)
			{
				this.titleText.text = multiBOGOData.sTitle2;
				this.descText.text = multiBOGOData.sDesc2;
				this.bottomText.text = string.Format(multiBOGOData.sBottom2, num2);
				this.numText.text = string.Empty;
				this.rewardBtnTxt.text = Language.GetLang(42074, new object[0]);
			}
			else
			{
				bool flag3 = (int)szMultiBOGOData[3] == 0;
				if (flag3)
				{
					this.titleText.text = string.Format(multiBOGOData.sTitle1, num);
					this.descText.text = multiBOGOData.sDesc2;
					this.bottomText.text = string.Format(multiBOGOData.sBottom2, num2);
					this.numText.text = string.Empty;
					this.rewardBtnTxt.text = Language.GetLang(42074, new object[0]);
				}
				else
				{
					this.titleText.text = string.Format(multiBOGOData.sTitle1, num);
					this.descText.text = multiBOGOData.sDesc1;
					this.bottomText.text = multiBOGOData.sBottom1;
					int num3 = num / int.Parse(multiBOGOData.szRate[0]) * int.Parse(multiBOGOData.szRate[1]);
					this.numText.text = string.Format("<color=#2acf0b>{0}*{1}</color>", itemData.m_sName, num3);
					this.rewardBtnTxt.text = Language.GetLang(41006, new object[0]);
				}
			}
		}
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x000FCEB0 File Offset: 0x000FB0B0
	private void OnClickRewardBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(45);
		bool flag = actDataByType != null;
		if (flag)
		{
			bool flag2 = (int)LiBaoManager.GetInstance().m_szMultiBOGOData[3] == 0;
			if (flag2)
			{
				ShopManager.GetInstance().OnPenShopWin(1);
			}
			else
			{
				fGameManager.SendMsgToHallServer(1315, new ArrayList
				{
					15041,
					new ArrayList()
				});
			}
		}
	}

	// Token: 0x04001546 RID: 5446
	public static MultiBOGOUICon inst;

	// Token: 0x04001547 RID: 5447
	private RawImage bgRimg;

	// Token: 0x04001548 RID: 5448
	private Text titleText;

	// Token: 0x04001549 RID: 5449
	private Text descText;

	// Token: 0x0400154A RID: 5450
	private Text numText;

	// Token: 0x0400154B RID: 5451
	private Text bottomText;

	// Token: 0x0400154C RID: 5452
	private Button rewardBtn;

	// Token: 0x0400154D RID: 5453
	private Text rewardBtnTxt;

	// Token: 0x0400154E RID: 5454
	private Text timeText;
}
