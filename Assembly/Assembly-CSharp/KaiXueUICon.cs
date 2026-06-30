using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000208 RID: 520
public class KaiXueUICon : Window
{
	// Token: 0x06001698 RID: 5784 RVA: 0x000D101C File Offset: 0x000CF21C
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.descTxt1 = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.descTxt2 = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
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
		KaiXueUICon.inst = this;
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x000D1137 File Offset: 0x000CF337
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x000D1141 File Offset: 0x000CF341
	public void Update()
	{
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x000D1144 File Offset: 0x000CF344
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(24);
		bool flag = actDataByType == null;
		if (!flag)
		{
			KaiXueGiftData kaiXueGiftData = fGameManager.mloadConfig.AllKaiXueGift[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + kaiXueGiftData.sIconName + ".txt", this.bgRimg);
			this.descTxt1.text = kaiXueGiftData.sActDesc1;
			this.descTxt2.text = kaiXueGiftData.sActDesc2;
			for (int i = 0; i < this.szReward.Count; i++)
			{
				GameObject gameObject = this.szReward[i];
				RawImage component = gameObject.GetComponent<RawImage>();
				component.enabled = false;
				Text component2 = gameObject.transform.Find("ItemName").GetComponent<Text>();
				component2.text = string.Empty;
				bool flag2 = i * 2 < kaiXueGiftData.szReward.Length;
				if (flag2)
				{
					gameObject.SetActive(true);
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(kaiXueGiftData.szReward[i * 2])];
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
					component2.text = string.Concat(new string[]
					{
						"<color=#2acf0b>",
						itemData.m_sName,
						"*",
						kaiXueGiftData.szReward[i * 2 + 1],
						"</color>"
					});
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
			int kaiXueGiftReward = LiBaoManager.GetInstance().GetKaiXueGiftReward();
			int num = kaiXueGiftReward;
			if (num != 1)
			{
				if (num != 2)
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

	// Token: 0x0600169C RID: 5788 RVA: 0x000D138C File Offset: 0x000CF58C
	private void OnGetBtnClick(GameObject go)
	{
		int kaiXueGiftReward = LiBaoManager.GetInstance().GetKaiXueGiftReward();
		bool flag = kaiXueGiftReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15024,
				new ArrayList()
			});
		}
	}

	// Token: 0x040011A2 RID: 4514
	public static KaiXueUICon inst;

	// Token: 0x040011A3 RID: 4515
	private RawImage bgRimg;

	// Token: 0x040011A4 RID: 4516
	private Text descTxt1;

	// Token: 0x040011A5 RID: 4517
	private Text descTxt2;

	// Token: 0x040011A6 RID: 4518
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x040011A7 RID: 4519
	private Button getBtn;

	// Token: 0x040011A8 RID: 4520
	private Text getBtnText;
}
