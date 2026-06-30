using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004AA RID: 1194
public class WinterActUICon : Window
{
	// Token: 0x06002E00 RID: 11776 RVA: 0x00196784 File Offset: 0x00194984
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.timeTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.descTxt = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.szTargetTxt.Add(this.UIGameObj.Get_GameObj(3).GetComponent<Text>());
		this.szTargetSld.Add(this.UIGameObj.Get_GameObj(4).GetComponent<Slider>());
		GameObject gameObject = this.UIGameObj.Get_GameObj(5);
		gameObject.SetActive(false);
		this.szTargetInfo.Add(gameObject);
		this.szTargetTxt.Add(this.UIGameObj.Get_GameObj(7).GetComponent<Text>());
		this.szTargetSld.Add(this.UIGameObj.Get_GameObj(8).GetComponent<Slider>());
		gameObject = this.UIGameObj.Get_GameObj(9);
		gameObject.SetActive(false);
		this.szTargetInfo.Add(gameObject);
		this.szTargetTxt.Add(this.UIGameObj.Get_GameObj(11).GetComponent<Text>());
		this.szTargetSld.Add(this.UIGameObj.Get_GameObj(12).GetComponent<Slider>());
		gameObject = this.UIGameObj.Get_GameObj(13);
		gameObject.SetActive(false);
		this.szTargetInfo.Add(gameObject);
		this.szTargetTxt.Add(this.UIGameObj.Get_GameObj(15).GetComponent<Text>());
		this.szTargetSld.Add(this.UIGameObj.Get_GameObj(16).GetComponent<Slider>());
		gameObject = this.UIGameObj.Get_GameObj(17);
		gameObject.SetActive(false);
		this.szTargetInfo.Add(gameObject);
		this.szTargetTxt.Add(this.UIGameObj.Get_GameObj(19).GetComponent<Text>());
		this.szTargetSld.Add(this.UIGameObj.Get_GameObj(20).GetComponent<Slider>());
		gameObject = this.UIGameObj.Get_GameObj(21);
		gameObject.SetActive(false);
		this.szTargetInfo.Add(gameObject);
		this.itemNumTxt = this.UIGameObj.Get_GameObj(22).GetComponent<Text>();
		gameObject = this.UIGameObj.Get_GameObj(24);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverExchangeIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitExchangeIcon);
		this.szRewardIcon.Add(gameObject.GetComponent<RawImage>());
		this.szRewardNum.Add(this.UIGameObj.Get_GameObj(25).GetComponent<Text>());
		gameObject = this.UIGameObj.Get_GameObj(26);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szRewardBtn.Add(gameObject.GetComponent<Button>());
		this.szRewardDesc.Add(this.UIGameObj.Get_GameObj(27).GetComponent<Text>());
		gameObject = this.UIGameObj.Get_GameObj(29);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverExchangeIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitExchangeIcon);
		this.szRewardIcon.Add(gameObject.GetComponent<RawImage>());
		this.szRewardNum.Add(this.UIGameObj.Get_GameObj(30).GetComponent<Text>());
		gameObject = this.UIGameObj.Get_GameObj(31);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szRewardBtn.Add(gameObject.GetComponent<Button>());
		this.szRewardDesc.Add(this.UIGameObj.Get_GameObj(32).GetComponent<Text>());
		gameObject = this.UIGameObj.Get_GameObj(34);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverExchangeIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitExchangeIcon);
		this.szRewardIcon.Add(gameObject.GetComponent<RawImage>());
		this.szRewardNum.Add(this.UIGameObj.Get_GameObj(35).GetComponent<Text>());
		gameObject = this.UIGameObj.Get_GameObj(36);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnRewardClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szRewardBtn.Add(gameObject.GetComponent<Button>());
		this.szRewardDesc.Add(this.UIGameObj.Get_GameObj(37).GetComponent<Text>());
		this.szRewardName.Add(this.UIGameObj.Get_GameObj(39).GetComponent<Text>());
		this.szRewardName.Add(this.UIGameObj.Get_GameObj(40).GetComponent<Text>());
		this.szRewardName.Add(this.UIGameObj.Get_GameObj(41).GetComponent<Text>());
		WinterActUICon.inst = this;
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x00196CB4 File Offset: 0x00194EB4
	private void Start()
	{
		this.InitUI();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x00196CE8 File Offset: 0x00194EE8
	public override void Reset()
	{
		base.Reset();
		this.InitUI();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x00196D24 File Offset: 0x00194F24
	public void InitUI()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(46);
		bool flag = actDataByType == null;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			WinterActData winterActData = fGameManager.mloadConfig.AllWinterActData[actDataByType.nXmlId];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[winterActData.nActItemId];
			this.descTxt.text = Language.GetLang(36005, new object[]
			{
				itemData.m_sName
			});
			this.timeTxt.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.ToString("yyyy/MM/dd")
			});
			fUIManager.GetInstance().GetIconAsset("libao/" + winterActData.sIconName + ".txt", this.bgRimg);
			int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(winterActData.nActItemId);
			this.itemNumTxt.text = haveItemNum.ToString();
			int num = 0;
			foreach (WinterTarData winterTarData in winterActData.szTarget.Values)
			{
				int winterActTar = LiBaoManager.GetInstance().GetWinterActTar(winterTarData.nType);
				this.szTargetTxt[num].text = string.Format(winterTarData.sDesc, winterActTar, winterTarData.nMaxNum);
				float num2 = (float)winterActTar * 1f / (float)winterTarData.nMaxNum * 1f;
				num2 = num2 * 713f / 733f;
				this.szTargetSld[num].value = Mathf.Min(num2, 1f);
				bool flag2 = this.szAllTargetInfo.ContainsKey(winterTarData.nIndex);
				List<GameObject> list;
				if (flag2)
				{
					list = this.szAllTargetInfo[winterTarData.nIndex];
				}
				else
				{
					list = new List<GameObject>();
					this.szAllTargetInfo.Add(winterTarData.nIndex, list);
				}
				for (int i = 0; i < winterTarData.szData.Count; i++)
				{
					ArrayList arrayList = (ArrayList)winterTarData.szData[i];
					bool flag3 = i < list.Count;
					GameObject gameObject;
					if (flag3)
					{
						gameObject = list[i];
					}
					else
					{
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.szTargetInfo[num]);
						gameObject.transform.SetParent(this.szTargetInfo[num].transform.parent);
						gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((float)((int)arrayList[0]) * 1f / (float)winterTarData.nMaxNum * 713f - 20f, 0f, 0f);
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.SetActive(true);
						list.Add(gameObject);
					}
					Text component = gameObject.GetComponent<Text>();
					component.text = arrayList[0].ToString();
					Image component2 = gameObject.transform.Find("Arrow").GetComponent<Image>();
					bool flag4 = winterActTar < (int)arrayList[0];
					if (flag4)
					{
						component2.sprite = Window.spriteName(AtlasType.ATLAS_WINTERACT, "winteract_sjh");
					}
					else
					{
						component2.sprite = Window.spriteName(AtlasType.ATLAS_WINTERACT, "winteract_sj");
					}
					component = gameObject.transform.Find("Text").GetComponent<Text>();
					component.text = Language.GetLang(36004, new object[]
					{
						arrayList[1]
					});
				}
				num++;
			}
			num = 0;
			foreach (WinterExData winterExData in winterActData.szExchange.Values)
			{
				int winterActEx = LiBaoManager.GetInstance().GetWinterActEx(winterExData.nIndex);
				this.szRewardIcon[num].name = string.Format("RawImage{0}", winterExData.nIndex);
				fUIManager.GetInstance().GetIconAsset("libao/guoqing/" + winterExData.sIcon + ".txt", this.szRewardIcon[num]);
				this.szRewardNum[num].text = winterExData.nNeedNum.ToString();
				Text component3 = this.szRewardBtn[num].transform.Find("Text").GetComponent<Text>();
				bool flag5 = haveItemNum < winterExData.nNeedNum;
				if (flag5)
				{
					this.szRewardBtn[num].interactable = false;
					component3.text = Language.GetLang(36002, new object[0]);
				}
				else
				{
					bool flag6 = winterActEx >= winterExData.nMaxNum;
					if (flag6)
					{
						this.szRewardBtn[num].interactable = false;
						component3.text = Language.GetLang(36003, new object[0]);
					}
					else
					{
						this.szRewardBtn[num].interactable = true;
						component3.text = Language.GetLang(36008, new object[0]);
					}
				}
				this.szRewardBtn[num].name = string.Format("Button{0}", winterExData.nIndex);
				this.szRewardDesc[num].text = Language.GetLang(36009, new object[]
				{
					winterExData.nMaxNum - winterActEx
				});
				itemData = fGameManager.mloadConfig.AllItemData[int.Parse(winterExData.szExchange[0])];
				this.szRewardName[num].text = string.Concat(new string[]
				{
					"<color=",
					Fun.GetQualColor(itemData.m_nQuality),
					">",
					string.Format(itemData.m_sLiBaoDesc, winterExData.szExchange[1]),
					"</color>"
				});
				num++;
			}
		}
	}

	// Token: 0x06002E04 RID: 11780 RVA: 0x0019740C File Offset: 0x0019560C
	private void OnRewardClick(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(46);
		bool flag = actDataByType != null;
		if (flag)
		{
			WinterActData winterActData = fGameManager.mloadConfig.AllWinterActData[actDataByType.nXmlId];
			int haveItemNum = ShopManager.GetInstance().GetHaveItemNum(winterActData.nActItemId);
			int num = int.Parse(go.name.Replace("Button", string.Empty));
			WinterExData winterExData = winterActData.szExchange[num];
			int winterActEx = LiBaoManager.GetInstance().GetWinterActEx(winterExData.nIndex);
			bool flag2 = haveItemNum >= winterExData.nNeedNum && winterActEx < winterExData.nMaxNum;
			if (flag2)
			{
				fGameManager.SendMsgToHallServer(1315, new ArrayList
				{
					15042,
					new ArrayList
					{
						num,
						0
					}
				});
			}
		}
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x00197501 File Offset: 0x00195701
	private void ExitExchangeIcon(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x00197510 File Offset: 0x00195710
	private void OverExchangeIcon(GameObject go)
	{
		int key = int.Parse(go.name.Replace("RawImage", string.Empty));
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(46);
		bool flag = actDataByType != null;
		if (flag)
		{
			WinterActData winterActData = fGameManager.mloadConfig.AllWinterActData[actDataByType.nXmlId];
			WinterExData winterExData = winterActData.szExchange[key];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(winterExData.szExchange[0])];
			TipManager.GetInstance().ShowTextTip(itemData.m_sdesc, TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x04002759 RID: 10073
	public static WinterActUICon inst;

	// Token: 0x0400275A RID: 10074
	private RawImage bgRimg;

	// Token: 0x0400275B RID: 10075
	private Text timeTxt;

	// Token: 0x0400275C RID: 10076
	private Text descTxt;

	// Token: 0x0400275D RID: 10077
	private List<Text> szTargetTxt = new List<Text>();

	// Token: 0x0400275E RID: 10078
	private List<Slider> szTargetSld = new List<Slider>();

	// Token: 0x0400275F RID: 10079
	private List<GameObject> szTargetInfo = new List<GameObject>();

	// Token: 0x04002760 RID: 10080
	private Dictionary<int, List<GameObject>> szAllTargetInfo = new Dictionary<int, List<GameObject>>();

	// Token: 0x04002761 RID: 10081
	private Text itemNumTxt;

	// Token: 0x04002762 RID: 10082
	private List<RawImage> szRewardIcon = new List<RawImage>();

	// Token: 0x04002763 RID: 10083
	private List<Text> szRewardNum = new List<Text>();

	// Token: 0x04002764 RID: 10084
	private List<Button> szRewardBtn = new List<Button>();

	// Token: 0x04002765 RID: 10085
	private List<Text> szRewardDesc = new List<Text>();

	// Token: 0x04002766 RID: 10086
	private List<Text> szRewardName = new List<Text>();
}
