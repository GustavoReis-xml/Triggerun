using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000241 RID: 577
public class MatchRewardUICon : Window
{
	// Token: 0x06001A48 RID: 6728 RVA: 0x000F94D0 File Offset: 0x000F76D0
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
		MatchRewardUICon.inst = this;
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x000F95F6 File Offset: 0x000F77F6
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x000F9600 File Offset: 0x000F7800
	public void Update()
	{
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000F9604 File Offset: 0x000F7804
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(25);
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
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(matchNaRewardData.szReward[0])];
					RawImage component2 = gameObject.transform.Find("RawImage").GetComponent<RawImage>();
					component = gameObject.transform.Find("ItemName").GetComponent<Text>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
					component.text = string.Concat(new string[]
					{
						"<color=#2acf0b>",
						itemData.m_sName,
						"*",
						matchNaRewardData.szReward[1],
						"</color>"
					});
					int num2 = LiBaoManager.GetInstance().GetMatchReward(matchNaRewardData.nId);
					component = gameObject.transform.Find("StateTxt").GetComponent<Text>();
					int num3 = num2;
					int num4 = num3;
					if (num4 != 1)
					{
						if (num4 != 2)
						{
							num2 = LiBaoManager.GetInstance().GetMatchRewardNum(matchNaRewardData.nId);
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
			int matchReward = LiBaoManager.GetInstance().GetMatchReward();
			int num5 = matchReward;
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

	// Token: 0x06001A4C RID: 6732 RVA: 0x000F9940 File Offset: 0x000F7B40
	private void OnGetBtnClick(GameObject go)
	{
		int matchReward = LiBaoManager.GetInstance().GetMatchReward();
		bool flag = matchReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15025,
				new ArrayList()
			});
		}
	}

	// Token: 0x040014DF RID: 5343
	public static MatchRewardUICon inst;

	// Token: 0x040014E0 RID: 5344
	private RawImage bgRimg;

	// Token: 0x040014E1 RID: 5345
	private Text descTxt1;

	// Token: 0x040014E2 RID: 5346
	private Text descTxt2;

	// Token: 0x040014E3 RID: 5347
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x040014E4 RID: 5348
	private Button getBtn;

	// Token: 0x040014E5 RID: 5349
	private Text getBtnText;
}
