using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CA RID: 714
public class RechargeItemUICon : Window
{
	// Token: 0x060020EB RID: 8427 RVA: 0x0012F830 File Offset: 0x0012DA30
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
		this.gridRender = this.UIGameObj.Get_GameObj(13);
		this.gridRender.SetActive(false);
		RechargeItemUICon.inst = this;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x0012F9D2 File Offset: 0x0012DBD2
	private void Start()
	{
		this.ShowUIData(1);
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x0012F9DD File Offset: 0x0012DBDD
	public void Update()
	{
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x0012F9E0 File Offset: 0x0012DBE0
	public void ShowUIData(int ntype = 0)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(34);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			RechargeItemData rechargeItemData = fGameManager.mloadConfig.AllRechargeItem[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + rechargeItemData.sIconName + ".txt", this.bgRimg);
			this.descText1.text = rechargeItemData.sDesc1;
			this.descText2.text = rechargeItemData.sDesc2;
			this.totalNumTxt.text = LiBaoManager.GetInstance().GetRechargeItemTotal().ToString();
			int num = 0;
			foreach (ArrayList arrayList in rechargeItemData.allRewardInfo.Values)
			{
				bool flag2 = num < this.szReward.Count;
				if (flag2)
				{
					GameObject gameObject = this.szReward[num];
					Text component = gameObject.transform.Find("NumTxt").GetComponent<Text>();
					component.text = arrayList[1].ToString();
					bool flag3 = ntype == 1;
					if (flag3)
					{
						GameObject gameObject2 = gameObject.transform.Find("GridList").gameObject;
						this.AddGridRender(gameObject2, (string[])arrayList[2]);
					}
					Button component2 = gameObject.transform.Find("Button").GetComponent<Button>();
					component = component2.transform.Find("Text").GetComponent<Text>();
					int rechargeItemState = LiBaoManager.GetInstance().GetRechargeItemState((int)arrayList[0]);
					int num2 = rechargeItemState;
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

	// Token: 0x060020EF RID: 8431 RVA: 0x0012FCE8 File Offset: 0x0012DEE8
	private void AddGridRender(GameObject obj, string[] szitem)
	{
		for (int i = 0; i < szitem.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(szitem[i]);
			if (flag)
			{
				string[] array = szitem[i].Split(new char[]
				{
					','
				});
				bool flag2 = fGameManager.mloadConfig.AllItemData.ContainsKey(int.Parse(array[0]));
				if (flag2)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(array[0])];
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gridRender);
					gameObject.transform.SetParent(obj.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = "Grid_" + itemData.m_nItemId.ToString();
					gameObject.SetActive(true);
					this.szGridRender.Add(gameObject);
					Text component = gameObject.transform.Find("Text").GetComponent<Text>();
					component.text = array[1];
					RawImage component2 = gameObject.transform.Find("RawImage").GetComponent<RawImage>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
					EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterItemGrid);
					EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitItemGrid);
				}
			}
		}
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x0012FE93 File Offset: 0x0012E093
	private void OnExitItemGrid(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x0012FEA4 File Offset: 0x0012E0A4
	private void OnEnterItemGrid(GameObject go)
	{
		int key = int.Parse(go.name.Replace("Grid_", string.Empty));
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		TipManager.GetInstance().ShowTextTip(itemData.m_sName, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x0012FEFF File Offset: 0x0012E0FF
	private void OnChargeBtnClick(GameObject go)
	{
		fUIManager.GetInstance().OpenChongZhiUI("RechargeItemUICon");
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x0012FF14 File Offset: 0x0012E114
	private void OnRewardBtnClick(GameObject go)
	{
		int num = int.Parse(go.transform.parent.name.Replace("Render", string.Empty));
		int rechargeItemState = LiBaoManager.GetInstance().GetRechargeItemState(num);
		bool flag = rechargeItemState == 1;
		if (flag)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15033,
				new ArrayList
				{
					num,
					0
				}
			});
		}
	}

	// Token: 0x04001DE9 RID: 7657
	public static RechargeItemUICon inst;

	// Token: 0x04001DEA RID: 7658
	private RawImage bgRimg;

	// Token: 0x04001DEB RID: 7659
	private Text timeText;

	// Token: 0x04001DEC RID: 7660
	private Text descText1;

	// Token: 0x04001DED RID: 7661
	private Text descText2;

	// Token: 0x04001DEE RID: 7662
	private Text totalNumTxt;

	// Token: 0x04001DEF RID: 7663
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x04001DF0 RID: 7664
	private GameObject gridRender;

	// Token: 0x04001DF1 RID: 7665
	private List<GameObject> szGridRender = new List<GameObject>();
}
