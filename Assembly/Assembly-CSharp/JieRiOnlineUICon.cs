using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
public class JieRiOnlineUICon : Window
{
	// Token: 0x06001682 RID: 5762 RVA: 0x000D07F0 File Offset: 0x000CE9F0
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
		JieRiOnlineUICon.inst = this;
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000D096B File Offset: 0x000CEB6B
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000D0975 File Offset: 0x000CEB75
	public void Update()
	{
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x000D0978 File Offset: 0x000CEB78
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(48);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllJieRiOnline.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			JieRiOnline jieRiOnline = fGameManager.mloadConfig.AllJieRiOnline[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + jieRiOnline.sIconName + ".txt", this.bgRimg);
			this.titleTxt.text = jieRiOnline.sTitle;
			this.descTxt.text = jieRiOnline.sDesc;
			int jieRiOnlineState = LiBaoManager.GetInstance().GetJieRiOnlineState();
			int num = jieRiOnlineState;
			if (num != 0)
			{
				if (num != 1)
				{
					this.getRewardBtn.interactable = false;
					this.getRewardBtnTxt.text = Language.GetLang(44002, new object[0]);
				}
				else
				{
					this.getRewardBtn.interactable = true;
					this.getRewardBtnTxt.text = Language.GetLang(44001, new object[0]);
				}
			}
			else
			{
				this.getRewardBtn.interactable = false;
				this.getRewardBtnTxt.text = Language.GetLang(44000, new object[0]);
			}
			for (int i = 0; i < jieRiOnline.szReward.Length; i += 2)
			{
				bool flag2 = !string.IsNullOrEmpty(jieRiOnline.szReward[i]);
				if (flag2)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(jieRiOnline.szReward[i])];
					bool flag3 = int.Parse(jieRiOnline.szReward[i + 1]) > 1;
					if (flag3)
					{
						this.szItemName[i / 2].text = string.Format(itemData.m_sLiBaoDesc, jieRiOnline.szReward[i + 1]);
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

	// Token: 0x06001686 RID: 5766 RVA: 0x000D0BB0 File Offset: 0x000CEDB0
	private void OnBtnClick(GameObject go)
	{
		int jieRiOnlineState = LiBaoManager.GetInstance().GetJieRiOnlineState();
		bool flag = jieRiOnlineState == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15043,
				new ArrayList()
			});
		}
	}

	// Token: 0x0400118E RID: 4494
	public static JieRiOnlineUICon inst;

	// Token: 0x0400118F RID: 4495
	private RawImage bgRimg;

	// Token: 0x04001190 RID: 4496
	private Text titleTxt;

	// Token: 0x04001191 RID: 4497
	private List<RawImage> szItemRimg = new List<RawImage>();

	// Token: 0x04001192 RID: 4498
	private List<Text> szItemName = new List<Text>();

	// Token: 0x04001193 RID: 4499
	private Text descTxt;

	// Token: 0x04001194 RID: 4500
	private Button getRewardBtn;

	// Token: 0x04001195 RID: 4501
	private Text getRewardBtnTxt;
}
