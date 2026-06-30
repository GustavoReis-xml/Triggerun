using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CC RID: 716
public class RechargeZuanUICon : Window
{
	// Token: 0x060020F7 RID: 8439 RVA: 0x0013013C File Offset: 0x0012E33C
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.timeText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.descText1 = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.descText2 = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.totalNumTxt = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChargeBtnClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(6);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(7);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(8);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(9);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(10);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(11);
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(12);
		this.szReward.Add(gameObject);
		RechargeZuanUICon.inst = this;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x001302BE File Offset: 0x0012E4BE
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x001302C8 File Offset: 0x0012E4C8
	public void Update()
	{
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x001302CC File Offset: 0x0012E4CC
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(31);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			RechargeZuanData rechargeZuanData = fGameManager.mloadConfig.AllRechargeZuan[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + rechargeZuanData.sIconName + ".txt", this.bgRimg);
			this.descText1.text = rechargeZuanData.sDesc1;
			this.descText2.text = rechargeZuanData.sDesc2;
			this.totalNumTxt.text = LiBaoManager.GetInstance().GetRechargeZuanTotal().ToString();
			int num = 0;
			foreach (ArrayList arrayList in rechargeZuanData.allRewardInfo.Values)
			{
				bool flag2 = num < this.szReward.Count;
				if (flag2)
				{
					GameObject gameObject = this.szReward[num];
					Text component = gameObject.transform.Find("NumTxt").GetComponent<Text>();
					component.text = arrayList[1].ToString();
					component = gameObject.transform.Find("NumTxt2").GetComponent<Text>();
					component.text = (arrayList[2] as string[])[1];
					Button component2 = gameObject.transform.Find("Button").GetComponent<Button>();
					component = component2.transform.Find("Text").GetComponent<Text>();
					int rechargeZuanState = LiBaoManager.GetInstance().GetRechargeZuanState((int)arrayList[0]);
					int num2 = rechargeZuanState;
					if (num2 != 1)
					{
						if (num2 != 2)
						{
							component2.interactable = false;
							component.text = Language.GetLang(32004, new object[0]);
						}
						else
						{
							component2.interactable = false;
							component.text = Language.GetLang(42006, new object[0]);
						}
					}
					else
					{
						component2.interactable = true;
						component.text = Language.GetLang(42052, new object[0]);
						EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardBtnClick);
						EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
					}
				}
				num++;
			}
		}
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x001305C8 File Offset: 0x0012E7C8
	private void OnChargeBtnClick(GameObject go)
	{
		fUIManager.GetInstance().OpenChongZhiUI("RechargeZuanUICon");
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x001305DC File Offset: 0x0012E7DC
	private void OnRewardBtnClick(GameObject go)
	{
		int num = int.Parse(go.transform.parent.name.Replace("Render", string.Empty));
		int rechargeZuanState = LiBaoManager.GetInstance().GetRechargeZuanState(num);
		bool flag = rechargeZuanState == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15031,
				new ArrayList
				{
					num,
					0
				}
			});
		}
	}

	// Token: 0x04001DF8 RID: 7672
	public static RechargeZuanUICon inst;

	// Token: 0x04001DF9 RID: 7673
	private RawImage bgRimg;

	// Token: 0x04001DFA RID: 7674
	private Text timeText;

	// Token: 0x04001DFB RID: 7675
	private Text descText1;

	// Token: 0x04001DFC RID: 7676
	private Text descText2;

	// Token: 0x04001DFD RID: 7677
	private Text totalNumTxt;

	// Token: 0x04001DFE RID: 7678
	private List<GameObject> szReward = new List<GameObject>();
}
