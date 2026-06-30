using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FA RID: 250
public class DiscountShopUICon : Window
{
	// Token: 0x060008DA RID: 2266 RVA: 0x0004B504 File Offset: 0x00049704
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.transform.GetComponent<RawImage>();
		this.timeTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.descTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.descRender = this.UIGameObj.Get_GameObj(2);
		GameObject gameObject = this.UIGameObj.Get_GameObj(3);
		this.btnTxt = gameObject.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnUrlBtnClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		DiscountShopUICon.inst = this;
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0004B5C5 File Offset: 0x000497C5
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0004B5CF File Offset: 0x000497CF
	public void Update()
	{
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0004B5D4 File Offset: 0x000497D4
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(39);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllDiscountShopData.ContainsKey(actDataByType.nXmlId);
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			this.timeTxt.text = Language.GetLang(28013, new object[]
			{
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				dateTime2.Year,
				dateTime2.Month,
				dateTime2.Day
			});
			DiscountShopData discountShopData = fGameManager.mloadConfig.AllDiscountShopData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + discountShopData.sIconName + ".txt", this.bgRimg);
			bool flag2 = this.szDescRender.Count == 0;
			if (flag2)
			{
				Text component = this.descRender.transform.Find("Text1").GetComponent<Text>();
				component.text = discountShopData.szTitleDesc[0];
				component = this.descRender.transform.Find("Text2").GetComponent<Text>();
				component.text = discountShopData.szTitleDesc[1];
				component = this.descRender.transform.Find("Text3").GetComponent<Text>();
				component.text = discountShopData.szTitleDesc[2];
				component = this.descRender.transform.Find("Text4").GetComponent<Text>();
				component.text = discountShopData.szTitleDesc[3];
				for (int i = 0; i < discountShopData.szDiscountInfo.Count; i += 4)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.descRender);
					gameObject.transform.SetParent(this.descRender.transform.parent);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
					gameObject.name = "Render" + (i / 4).ToString();
					Image component2 = gameObject.GetComponent<Image>();
					component2.sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, (i / 4 % 2 != 0) ? "LiBao_Render_di1" : "LiBao_Render_di0");
					component = gameObject.transform.Find("Text1").GetComponent<Text>();
					bool flag3 = i + 4 > discountShopData.szDiscountInfo.Count;
					if (flag3)
					{
						component.text = string.Format("≥{0}", discountShopData.szDiscountInfo[i]);
					}
					else
					{
						component.text = discountShopData.szDiscountInfo[i].ToString();
					}
					component = gameObject.transform.Find("Text2").GetComponent<Text>();
					component.text = discountShopData.szDiscountInfo[i + 1].ToString();
					component = gameObject.transform.Find("Text3").GetComponent<Text>();
					component.text = string.Format("{0}%", discountShopData.szDiscountInfo[i + 2]);
					component = gameObject.transform.Find("Text4").GetComponent<Text>();
					component.text = discountShopData.szDiscountInfo[i + 3].ToString();
				}
			}
			this.btnTxt.text = discountShopData.sBtnTxt;
			this.descTxt.text = discountShopData.sDesc;
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0004B9EB File Offset: 0x00049BEB
	private void OnUrlBtnClick(GameObject go)
	{
		ShopManager.GetInstance().OnPenShopWin(1);
	}

	// Token: 0x04000674 RID: 1652
	public static DiscountShopUICon inst;

	// Token: 0x04000675 RID: 1653
	private RawImage bgRimg;

	// Token: 0x04000676 RID: 1654
	private Text timeTxt;

	// Token: 0x04000677 RID: 1655
	private GameObject descRender;

	// Token: 0x04000678 RID: 1656
	private List<GameObject> szDescRender = new List<GameObject>();

	// Token: 0x04000679 RID: 1657
	private Text btnTxt;

	// Token: 0x0400067A RID: 1658
	private Text descTxt;
}
