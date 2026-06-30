using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B3 RID: 691
public class PvpEveryWeekUICon : Window
{
	// Token: 0x06001FC4 RID: 8132 RVA: 0x00123F18 File Offset: 0x00122118
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.titleTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.szItemRimg.Add(this.UIGameObj.Get_GameObj(1).GetComponent<RawImage>());
		this.szItemName.Add(this.UIGameObj.Get_GameObj(2).GetComponent<Text>());
		this.szItemRimg.Add(this.UIGameObj.Get_GameObj(3).GetComponent<RawImage>());
		this.szItemName.Add(this.UIGameObj.Get_GameObj(4).GetComponent<Text>());
		this.szItemRimg.Add(this.UIGameObj.Get_GameObj(5).GetComponent<RawImage>());
		this.szItemName.Add(this.UIGameObj.Get_GameObj(6).GetComponent<Text>());
		this.getRewardBtn = this.UIGameObj.Get_GameObj(7).GetComponent<Button>();
		this.getRewardBtnTxt = this.getRewardBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.getRewardBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		EventTriggerListener.Get(this.getRewardBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.descTxt = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		PvpEveryWeekUICon.inst = this;
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00124093 File Offset: 0x00122293
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x0012409D File Offset: 0x0012229D
	public void Update()
	{
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x001240A0 File Offset: 0x001222A0
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(44);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllPvpWeekReward.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			PvpWeekReward pvpWeekReward = fGameManager.mloadConfig.AllPvpWeekReward[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + pvpWeekReward.sIconName + ".txt", this.bgRimg);
			int pvpMatchNum = LiBaoManager.GetInstance().GetPvpMatchNum();
			this.titleTxt.text = string.Format(pvpWeekReward.sTitle, pvpMatchNum, pvpWeekReward.nMatchNum);
			this.descTxt.text = pvpWeekReward.sDesc;
			int pvpRewardState = LiBaoManager.GetInstance().GetPvpRewardState();
			int num = pvpRewardState;
			if (num != 0)
			{
				if (num != 1)
				{
					this.getRewardBtn.interactable = false;
					this.getRewardBtnTxt.text = Language.GetLang(34011, new object[0]);
				}
				else
				{
					this.getRewardBtn.interactable = true;
					this.getRewardBtnTxt.text = Language.GetLang(41006, new object[0]);
				}
			}
			else
			{
				this.getRewardBtn.interactable = true;
				this.getRewardBtnTxt.text = Language.GetLang(34010, new object[0]);
			}
			for (int i = 0; i < pvpWeekReward.szReward.Length; i += 2)
			{
				bool flag2 = !string.IsNullOrEmpty(pvpWeekReward.szReward[i]);
				if (flag2)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(pvpWeekReward.szReward[i])];
					bool flag3 = int.Parse(pvpWeekReward.szReward[i + 1]) > 1;
					if (flag3)
					{
						this.szItemName[i / 2].text = string.Format(itemData.m_sLiBaoDesc, pvpWeekReward.szReward[i + 1]);
					}
					else
					{
						this.szItemName[i / 2].text = itemData.m_sName;
					}
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), this.szItemRimg[i / 2]);
				}
			}
		}
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x001242FC File Offset: 0x001224FC
	private void OnBtnClick(GameObject go)
	{
		int pvpRewardState = LiBaoManager.GetInstance().GetPvpRewardState();
		bool flag = pvpRewardState == 0 && MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnGotoPveFbUI();
		}
		bool flag2 = pvpRewardState == 1;
		if (flag2)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15040,
				new ArrayList()
			});
		}
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x0012436E File Offset: 0x0012256E
	private void OnLoadUIOk(string sUI, GameObject UIObj, object szParam)
	{
	}

	// Token: 0x04001CF8 RID: 7416
	public static PvpEveryWeekUICon inst;

	// Token: 0x04001CF9 RID: 7417
	private RawImage bgRimg;

	// Token: 0x04001CFA RID: 7418
	private Text titleTxt;

	// Token: 0x04001CFB RID: 7419
	private List<RawImage> szItemRimg = new List<RawImage>();

	// Token: 0x04001CFC RID: 7420
	private List<Text> szItemName = new List<Text>();

	// Token: 0x04001CFD RID: 7421
	private Text descTxt;

	// Token: 0x04001CFE RID: 7422
	private Button getRewardBtn;

	// Token: 0x04001CFF RID: 7423
	private Text getRewardBtnTxt;
}
