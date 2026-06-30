using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025D RID: 605
public class NewSoldierUICon : Window
{
	// Token: 0x06001B94 RID: 7060 RVA: 0x00104B44 File Offset: 0x00102D44
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.actDescTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.actTimeTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.regTitleTxt = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		GameObject item = this.UIGameObj.Get_GameObj(3);
		this.regReward.Add(item);
		item = this.UIGameObj.Get_GameObj(4);
		this.regReward.Add(item);
		item = this.UIGameObj.Get_GameObj(5);
		this.regReward.Add(item);
		this.regBtn = this.UIGameObj.Get_GameObj(6).GetComponent<Button>();
		this.regBtnTxt = this.regBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.regBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRegBtnClick);
		EventTriggerListener.Get(this.regBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lvlTitleTxt = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		item = this.UIGameObj.Get_GameObj(8);
		this.lvlReward.Add(item);
		item = this.UIGameObj.Get_GameObj(9);
		this.lvlReward.Add(item);
		item = this.UIGameObj.Get_GameObj(10);
		this.lvlReward.Add(item);
		this.lvlBtn = this.UIGameObj.Get_GameObj(11).GetComponent<Button>();
		this.lvlBtnTxt = this.lvlBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.lvlBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnLvlBtnClick);
		EventTriggerListener.Get(this.lvlBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		NewSoldierUICon.inst = this;
	}

	// Token: 0x06001B95 RID: 7061 RVA: 0x00104D56 File Offset: 0x00102F56
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x00104D60 File Offset: 0x00102F60
	public void Update()
	{
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x00104D64 File Offset: 0x00102F64
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(22);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllSkyEveryDay.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			NewSoldierActData newSoldierActData = fGameManager.mloadConfig.AllNewSoldier[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + newSoldierActData.sIconName + ".txt", this.bgRimg);
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			DateTime dRegDate = PlayerDataManager.getInstance().HUserData.m_dRegDate;
			this.actDescTxt.text = newSoldierActData.sActDesc;
			this.actTimeTxt.text = string.Format(newSoldierActData.sActTime, dateTime.ToString("yyyy/MM/dd"), dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd"));
			this.regTitleTxt.text = newSoldierActData.sRegTitle;
			for (int i = 0; i < this.regReward.Count; i++)
			{
				GameObject gameObject = this.regReward[i];
				RawImage component = gameObject.GetComponent<RawImage>();
				component.enabled = false;
				Text component2 = gameObject.transform.Find("ItemName").GetComponent<Text>();
				component2.text = string.Empty;
				bool flag2 = i * 2 < newSoldierActData.szRegReward.Length;
				if (flag2)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(newSoldierActData.szRegReward[i * 2])];
					component = gameObject.GetComponent<RawImage>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
					component2.text = string.Concat(new string[]
					{
						"<color=#2acf0b>",
						itemData.m_sName,
						"*",
						newSoldierActData.szRegReward[i * 2 + 1],
						"</color>"
					});
					gameObject.SetActive(true);
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
			int newSoldierReward = LiBaoManager.GetInstance().GetNewSoldierReward(2);
			bool flag3 = newSoldierReward == 0;
			if (flag3)
			{
				this.regBtn.interactable = false;
				this.regBtnTxt.text = Language.GetLang(25008, new object[0]);
			}
			bool flag4 = newSoldierReward == 1;
			if (flag4)
			{
				this.regBtn.interactable = true;
				this.regBtnTxt.text = Language.GetLang(42052, new object[0]);
			}
			bool flag5 = newSoldierReward == 2;
			if (flag5)
			{
				this.regBtn.interactable = false;
				this.regBtnTxt.text = Language.GetLang(42006, new object[0]);
			}
			this.lvlTitleTxt.text = newSoldierActData.sLvlTitle;
			for (int j = 0; j < this.lvlReward.Count; j++)
			{
				GameObject gameObject2 = this.lvlReward[j];
				RawImage component3 = gameObject2.GetComponent<RawImage>();
				component3.enabled = false;
				Text component4 = gameObject2.transform.Find("ItemName").GetComponent<Text>();
				component4.text = string.Empty;
				bool flag6 = j * 2 < newSoldierActData.szLvlReward.Length;
				if (flag6)
				{
					ItemData itemData2 = fGameManager.mloadConfig.AllItemData[int.Parse(newSoldierActData.szLvlReward[j * 2])];
					component3 = gameObject2.GetComponent<RawImage>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData2.m_nType), itemData2.m_sIco), component3);
					component4.text = string.Concat(new string[]
					{
						"<color=#2acf0b>",
						itemData2.m_sName,
						"*",
						newSoldierActData.szLvlReward[j * 2 + 1],
						"</color>"
					});
					gameObject2.SetActive(true);
				}
				else
				{
					gameObject2.SetActive(false);
				}
			}
			newSoldierReward = LiBaoManager.GetInstance().GetNewSoldierReward(3);
			bool flag7 = newSoldierReward == 0;
			if (flag7)
			{
				this.lvlBtn.interactable = false;
				this.lvlBtnTxt.text = Language.GetLang(25008, new object[0]);
			}
			bool flag8 = newSoldierReward == 1;
			if (flag8)
			{
				this.lvlBtn.interactable = true;
				this.lvlBtnTxt.text = Language.GetLang(42052, new object[0]);
			}
			bool flag9 = newSoldierReward == 2;
			if (flag9)
			{
				this.lvlBtn.interactable = false;
				this.lvlBtnTxt.text = Language.GetLang(42006, new object[0]);
			}
		}
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x00105248 File Offset: 0x00103448
	private void OnLvlBtnClick(GameObject go)
	{
		int newSoldierReward = LiBaoManager.GetInstance().GetNewSoldierReward(3);
		bool flag = newSoldierReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15022,
				new ArrayList
				{
					3,
					0
				}
			});
		}
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x001052B4 File Offset: 0x001034B4
	private void OnRegBtnClick(GameObject go)
	{
		int newSoldierReward = LiBaoManager.GetInstance().GetNewSoldierReward(2);
		bool flag = newSoldierReward == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15022,
				new ArrayList
				{
					2,
					0
				}
			});
		}
	}

	// Token: 0x040018F1 RID: 6385
	public static NewSoldierUICon inst;

	// Token: 0x040018F2 RID: 6386
	private RawImage bgRimg;

	// Token: 0x040018F3 RID: 6387
	private Text actDescTxt;

	// Token: 0x040018F4 RID: 6388
	private Text actTimeTxt;

	// Token: 0x040018F5 RID: 6389
	private Text regTitleTxt;

	// Token: 0x040018F6 RID: 6390
	private List<GameObject> regReward = new List<GameObject>();

	// Token: 0x040018F7 RID: 6391
	private Button regBtn;

	// Token: 0x040018F8 RID: 6392
	private Text regBtnTxt;

	// Token: 0x040018F9 RID: 6393
	private Text lvlTitleTxt;

	// Token: 0x040018FA RID: 6394
	private List<GameObject> lvlReward = new List<GameObject>();

	// Token: 0x040018FB RID: 6395
	private Button lvlBtn;

	// Token: 0x040018FC RID: 6396
	private Text lvlBtnTxt;
}
