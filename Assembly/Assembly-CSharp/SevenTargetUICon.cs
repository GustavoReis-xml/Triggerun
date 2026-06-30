using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000306 RID: 774
public class SevenTargetUICon : Window
{
	// Token: 0x06002542 RID: 9538 RVA: 0x0014FCD8 File Offset: 0x0014DED8
	protected override void Awake()
	{
		base.Awake();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = base.transform.Find("UIPanel/Title/Title").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
		}
		this.timeText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.dayFilter = this.UIGameObj.Get_GameObj(1).GetComponent<Image>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(3);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(6);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		gameObject = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDay);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szDayRender.Add(gameObject.GetComponent<Toggle>());
		this.dayText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(10);
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			Text component2 = gameObject.transform.Find("TargDesc").GetComponent<Text>();
			component2.font = fUIManager.mDINMedium;
			component2 = gameObject.transform.Find("Button/Text").GetComponent<Text>();
			component2.font = fUIManager.mDINMedium;
		}
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(11);
		bool flag3 = WebParam.client_language == "baxi";
		if (flag3)
		{
			Text component3 = gameObject.transform.Find("TargDesc").GetComponent<Text>();
			component3.font = fUIManager.mDINMedium;
			component3 = gameObject.transform.Find("Button/Text").GetComponent<Text>();
			component3.font = fUIManager.mDINMedium;
		}
		this.szReward.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(12);
		bool flag4 = WebParam.client_language == "baxi";
		if (flag4)
		{
			Text component4 = gameObject.transform.Find("TargDesc").GetComponent<Text>();
			component4.font = fUIManager.mDINMedium;
			component4 = gameObject.transform.Find("Button/Text").GetComponent<Text>();
			component4.font = fUIManager.mDINMedium;
		}
		this.szReward.Add(gameObject);
		SevenTargetUICon.inst = this;
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x001500F0 File Offset: 0x0014E2F0
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.InitUI();
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x00150124 File Offset: 0x0014E324
	private void Update()
	{
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x00150128 File Offset: 0x0014E328
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.InitUI();
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x00150163 File Offset: 0x0014E363
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x00150170 File Offset: 0x0014E370
	private void InitUI()
	{
		DateTime d = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
		DateTime d2 = DateTime.Parse(PlayerDataManager.getInstance().HUserData.m_dRegDate.ToString("yyyy-MM-dd"));
		int num = (int)(d - d2).TotalDays + 1;
		for (int i = 0; i < this.szDayRender.Count; i++)
		{
			Text component = this.szDayRender[i].transform.Find("DayNum").GetComponent<Text>();
			component.text = Language.GetLang(25003, new object[]
			{
				Fun.GetHZNum(i + 1)
			});
			Image component2 = this.szDayRender[i].transform.GetComponent<Image>();
			bool flag = i < num;
			if (flag)
			{
				component2.sprite = Window.spriteName(AtlasType.ATLAS_SEVENTAGER, "seventarg_kaiqi");
			}
			else
			{
				component2.sprite = Window.spriteName(AtlasType.ATLAS_SEVENTAGER, "seventarg_weikaiqi");
			}
			component2 = this.szDayRender[i].transform.Find("Lock").GetComponent<Image>();
			bool flag2 = i < num;
			if (flag2)
			{
				component2.enabled = false;
			}
			else
			{
				component2.enabled = true;
			}
			component = this.szDayRender[i].transform.Find("Text").GetComponent<Text>();
			bool flag3 = i == num;
			if (flag3)
			{
				component.enabled = true;
			}
			else
			{
				component.enabled = false;
			}
		}
		this.dayFilter.fillAmount = 0.16666667f * (float)(num - 1);
		this.OnClickDay(this.szDayRender[num - 1].gameObject);
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x0015034C File Offset: 0x0014E54C
	public void ResetSevenTargetData()
	{
		this.OnClickDay(this.szDayRender[this.dayIndex].gameObject);
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x0015036C File Offset: 0x0014E56C
	private void OnClickDay(GameObject go)
	{
		int num = int.Parse(go.name.Replace("Render", string.Empty));
		int num2 = 0;
		ArrayList arrayList = (ArrayList)ActivityManager.GetInstance().GetSeventTargetDay().Clone();
		int num3 = arrayList.IndexOf(num);
		bool flag = num3 == -1;
		if (!flag)
		{
			this.dayIndex = num - 1;
			this.szDayRender[this.dayIndex].isOn = true;
			ArrayList arrayList2 = (ArrayList)arrayList[num3 + 1];
			DateTime d = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
			DateTime d2 = DateTime.Parse(PlayerDataManager.getInstance().HUserData.m_dRegDate.ToString("yyyy-MM-dd"));
			int num4 = (int)(d - d2).TotalDays + 1;
			for (int i = 0; i < this.szReward.Count; i++)
			{
				SevenTargetData sevenTargetData = (SevenTargetData)arrayList2[i];
				Text component = this.szReward[i].transform.Find("TargDesc").GetComponent<Text>();
				component.text = sevenTargetData.sTitleDesc;
				component = this.szReward[i].transform.Find("TargInfo").GetComponent<Text>();
				component.text = sevenTargetData.sTargetDesc;
				RawImage component2 = this.szReward[i].transform.Find("RewardInfo/RawImage").GetComponent<RawImage>();
				ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(sevenTargetData.szReward[0])];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
				component = this.szReward[i].transform.Find("RewardInfo/ItemDesc").GetComponent<Text>();
				bool flag2 = int.Parse(sevenTargetData.szReward[1]) > 1;
				if (flag2)
				{
					component.text = itemData.m_sName + "*" + sevenTargetData.szReward[1];
				}
				else
				{
					component.text = itemData.m_sName;
				}
				ArrayList sevenTargetInfo = ActivityManager.GetInstance().GetSevenTargetInfo(sevenTargetData.nId);
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				bool flag3 = sevenTargetInfo.Count > 0;
				if (flag3)
				{
					num5 = (int)sevenTargetInfo[0];
					num6 = (int)sevenTargetInfo[1];
					num7 = (int)sevenTargetInfo[2];
				}
				Button component3 = this.szReward[i].transform.Find("Button").GetComponent<Button>();
				component = this.szReward[i].transform.Find("Button/Text").GetComponent<Text>();
				bool flag4 = sevenTargetData.nDay > num4;
				if (flag4)
				{
					component3.interactable = false;
					EventTriggerListener.Get(component3.gameObject).onClick = null;
					EventTriggerListener.Get(component3.gameObject).SetSoundStr(string.Empty, string.Empty);
					component.text = Language.GetLang(25019, new object[0]);
					num2 = 1;
				}
				else
				{
					bool flag5 = num6 == 1 && num7 == 0;
					if (flag5)
					{
						component3.interactable = true;
						EventTriggerListener.Get(component3.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickGetReward);
						EventTriggerListener.Get(component3.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
						component.text = Language.GetLang(25007, new object[0]);
					}
					else
					{
						bool flag6 = num6 == 1 && num7 == 1;
						if (flag6)
						{
							component3.interactable = false;
							EventTriggerListener.Get(component3.gameObject).onClick = null;
							EventTriggerListener.Get(component3.gameObject).SetSoundStr(string.Empty, string.Empty);
							component.text = Language.GetLang(25018, new object[0]);
						}
						else
						{
							component3.interactable = false;
							EventTriggerListener.Get(component3.gameObject).onClick = null;
							EventTriggerListener.Get(component3.gameObject).SetSoundStr(string.Empty, string.Empty);
							component.text = Language.GetLang(25008, new object[0]);
						}
					}
				}
				component = this.szReward[i].transform.Find("Button/InfoTxt").GetComponent<Text>();
				component.text = sevenTargetData.nId.ToString();
				component = this.szReward[i].transform.Find("Progress").GetComponent<Text>();
				bool flag7 = num2 == 1;
				if (flag7)
				{
					component.text = string.Empty;
				}
				else
				{
					component.text = Language.GetLang(25006, new object[]
					{
						num5,
						sevenTargetData.nTargetNum
					});
				}
			}
		}
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x0015089C File Offset: 0x0014EA9C
	private void OnClickGetReward(GameObject go)
	{
		Text component = go.transform.Find("InfoTxt").GetComponent<Text>();
		int num = int.Parse(component.text);
		fGameManager.LobbyPlayer.SendMsgToHallServer(16107, new ArrayList
		{
			num,
			0
		});
	}

	// Token: 0x040020E4 RID: 8420
	public static SevenTargetUICon inst;

	// Token: 0x040020E5 RID: 8421
	private Text timeText;

	// Token: 0x040020E6 RID: 8422
	private Image dayFilter;

	// Token: 0x040020E7 RID: 8423
	private List<Toggle> szDayRender = new List<Toggle>();

	// Token: 0x040020E8 RID: 8424
	private Text dayText;

	// Token: 0x040020E9 RID: 8425
	private List<GameObject> szReward = new List<GameObject>();

	// Token: 0x040020EA RID: 8426
	private int dayIndex;
}
