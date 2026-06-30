using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000251 RID: 593
public class NaMatchRewardUICon : Window
{
	// Token: 0x06001A99 RID: 6809 RVA: 0x000FD9CC File Offset: 0x000FBBCC
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.descTxt1 = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		GameObject item = this.UIGameObj.Get_GameObj(2);
		this.szReward.Add(item);
		item = this.UIGameObj.Get_GameObj(3);
		this.szReward.Add(item);
		item = this.UIGameObj.Get_GameObj(4);
		this.szReward.Add(item);
		this.getBtn = this.UIGameObj.Get_GameObj(5).GetComponent<Button>();
		this.getBtnText = this.getBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.getBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGetBtnClick);
		EventTriggerListener.Get(this.getBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.descTxt2 = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		NaMatchRewardUICon.inst = this;
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x000FDAF2 File Offset: 0x000FBCF2
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x000FDAFC File Offset: 0x000FBCFC
	public void Update()
	{
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x000FDB00 File Offset: 0x000FBD00
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(33);
		bool flag = actDataByType == null;
		if (!flag)
		{
			MatchNaRewardActData matchNaRewardActData = fGameManager.mloadConfig.AllNaMatchRewardAct[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + matchNaRewardActData.sIconName + ".txt", this.bgRimg);
			this.descTxt1.text = matchNaRewardActData.sActDesc1;
			this.descTxt2.text = matchNaRewardActData.sActDesc2;
			int num = 0;
			foreach (MatchNaRewardData matchNaRewardData in matchNaRewardActData.szReward.Values)
			{
				bool flag2 = num < this.szReward.Count;
				if (flag2)
				{
					GameObject gameObject = this.szReward[num];
					Text component = gameObject.transform.Find("Title").GetComponent<Text>();
					component.text = matchNaRewardData.sTitle;
					RawImage component2 = gameObject.transform.Find("RawImage").GetComponent<RawImage>();
					component = gameObject.transform.Find("ItemName").GetComponent<Text>();
					fUIManager.GetInstance().GetIconAsset("ItemIco/" + matchNaRewardData.sIcon + ".txt", component2);
					component.text = "<color=#2acf0b>" + matchNaRewardData.sName + "</color>";
					int num2 = LiBaoManager.GetInstance().GetNaMatchReward(matchNaRewardData.nId);
					component = gameObject.transform.Find("StateTxt").GetComponent<Text>();
					int num3 = num2;
					int num4 = num3;
					if (num4 != 1)
					{
						if (num4 != 2)
						{
							num2 = LiBaoManager.GetInstance().GetMatchNaRewardNum(matchNaRewardData.nId);
							component.text = string.Format("<color=#ffcf8b>{0}/{1}</color>", num2, matchNaRewardData.nMatchNum);
						}
						else
						{
							component.text = Language.GetLang(42006, new object[0]);
						}
					}
					else
					{
						component.text = Language.GetLang(42042, new object[0]);
					}
				}
				num++;
			}
			int naMatchReward = LiBaoManager.GetInstance().GetNaMatchReward();
			int num5 = naMatchReward;
			if (num5 != 1)
			{
				if (num5 != 2)
				{
					this.getBtn.interactable = false;
					this.getBtnText.text = Language.GetLang(25008, new object[0]);
				}
				else
				{
					this.getBtn.interactable = false;
					this.getBtnText.text = Language.GetLang(42006, new object[0]);
				}
			}
			else
			{
				this.getBtn.interactable = true;
				this.getBtnText.text = Language.GetLang(42052, new object[0]);
			}
		}
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x000FDDF8 File Offset: 0x000FBFF8
	private void OnGetBtnClick(GameObject go)
	{
		int naMatchReward = LiBaoManager.GetInstance().GetNaMatchReward();
		bool flag = naMatchReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15034,
				new ArrayList()
			});
		}
	}

	// Token: 0x04001569 RID: 5481
	public static NaMatchRewardUICon inst;

	// Token: 0x0400156A RID: 5482
	private RawImage bgRimg;

	// Token: 0x0400156B RID: 5483
	private Text descTxt1;

	// Token: 0x0400156C RID: 5484
	private Text descTxt2;

	// Token: 0x0400156D RID: 5485
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x0400156E RID: 5486
	private Button getBtn;

	// Token: 0x0400156F RID: 5487
	private Text getBtnText;
}
