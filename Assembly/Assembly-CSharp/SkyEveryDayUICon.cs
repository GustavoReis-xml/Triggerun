using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D8 RID: 984
public class SkyEveryDayUICon : Window
{
	// Token: 0x06002A0F RID: 10767 RVA: 0x00171FE0 File Offset: 0x001701E0
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.goldUITitle = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.goldUIDesc = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.goldNum = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.expUITitle = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.expUIDesc = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.matchNumTxt = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.skyUIBtn = this.UIGameObj.Get_GameObj(6).GetComponent<Button>();
		this.skyUIBtnTxt = this.skyUIBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.skyUIBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnSkyUIClick);
		EventTriggerListener.Get(this.skyUIBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.goldUITitle.font = fUIManager.mDINMedium;
			this.expUITitle.font = fUIManager.mDINMedium;
			this.skyUIBtnTxt.font = fUIManager.mDINMedium;
		}
		SkyEveryDayUICon.inst = this;
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x00172151 File Offset: 0x00170351
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x0017215B File Offset: 0x0017035B
	public void Update()
	{
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x00172160 File Offset: 0x00170360
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(21);
		bool flag = actDataByType != null && fGameManager.mloadConfig.AllSkyEveryDay.ContainsKey(actDataByType.nXmlId);
		if (flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			SkyEveryDayData skyEveryDayData = fGameManager.mloadConfig.AllSkyEveryDay[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + skyEveryDayData.sIconName + ".txt", this.bgRimg);
			DateTime dateTime = DateTime.Parse(serverDate.ToString("yyyy-MM-dd") + " " + skyEveryDayData.sStartTime);
			DateTime dateTime2 = DateTime.Parse(serverDate.ToString("yyyy-MM-dd") + " " + skyEveryDayData.sEndTime);
			this.goldUITitle.text = skyEveryDayData.sMatchTitle;
			this.goldUIDesc.text = skyEveryDayData.sMatchDesc;
			this.goldNum.text = "<color=#2acf0b>" + Language.GetLang(5026, new object[]
			{
				skyEveryDayData.nGoldNum
			}) + "</color>";
			this.expUITitle.text = skyEveryDayData.sExpTitle;
			this.expUIDesc.text = skyEveryDayData.sExpDesc;
			this.matchNumTxt.text = string.Format(skyEveryDayData.sMatchNumDesc, LiBaoManager.GetInstance().GetLastMatchNum());
			bool flag2 = serverDate < dateTime || serverDate > dateTime2;
			if (flag2)
			{
				this.skyUIBtn.interactable = false;
				this.skyUIBtnTxt.text = Language.GetLang(42053, new object[0]);
			}
			else
			{
				this.skyUIBtn.interactable = true;
				this.skyUIBtnTxt.text = Language.GetLang(23040, new object[0]);
			}
			float num = (serverDate < dateTime) ? ((float)(dateTime - serverDate).TotalSeconds) : ((!(serverDate < dateTime2)) ? ((float)(dateTime.AddDays(1.0) - serverDate).TotalSeconds) : ((float)(dateTime2 - serverDate).TotalSeconds));
			base.CancelInvoke("ShowUIData");
			base.Invoke("ShowUIData", num + 2f);
		}
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x001723C0 File Offset: 0x001705C0
	private void OnSkyUIClick(GameObject go)
	{
		int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(2);
		ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
		fUIManager.GetInstance().ShowWin("SkyLadderUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadUIOk), actDataById.nXmlId, true);
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x0017240F File Offset: 0x0017060F
	private void OnLoadUIOk(string sUI, GameObject UIObj, object szParam)
	{
		MainUICon.inst.SetCurOpenUI(sUI);
		SkyLadderUICon.inst.ResetSkyLadderInfo((int)szParam);
	}

	// Token: 0x04002388 RID: 9096
	public static SkyEveryDayUICon inst;

	// Token: 0x04002389 RID: 9097
	private RawImage bgRimg;

	// Token: 0x0400238A RID: 9098
	private Text goldUITitle;

	// Token: 0x0400238B RID: 9099
	private Text goldUIDesc;

	// Token: 0x0400238C RID: 9100
	private Text goldNum;

	// Token: 0x0400238D RID: 9101
	private Text matchNumTxt;

	// Token: 0x0400238E RID: 9102
	private Text expUITitle;

	// Token: 0x0400238F RID: 9103
	private Text expUIDesc;

	// Token: 0x04002390 RID: 9104
	private Button skyUIBtn;

	// Token: 0x04002391 RID: 9105
	private Text skyUIBtnTxt;
}
