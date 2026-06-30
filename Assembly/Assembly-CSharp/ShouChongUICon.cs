using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000311 RID: 785
public class ShouChongUICon : Window
{
	// Token: 0x06002623 RID: 9763 RVA: 0x0015B3FC File Offset: 0x001595FC
	protected override void Awake()
	{
		base.Awake();
		ShouChongUICon.inst = this;
		this.bgRimg = base.gameObject.GetComponent<RawImage>();
		this.UI_Btn_2 = this.UIGameObj.Get_GameObj(2);
		this.UI_Btn_1 = this.UIGameObj.Get_GameObj(6);
		this.UI_Btn_3 = this.UIGameObj.Get_GameObj(7);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = this.UI_Btn_1.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
			component = this.UI_Btn_2.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
			component = this.UI_Btn_3.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
		}
		EventTriggerListener.Get(this.UI_Btn_1).onClick = new EventTriggerListener.VoidDelegate(this.BtnClick);
		EventTriggerListener.Get(this.UI_Btn_2).onClick = new EventTriggerListener.VoidDelegate(this.BtnClick);
		EventTriggerListener.Get(this.UI_Btn_3).onClick = new EventTriggerListener.VoidDelegate(this.BtnClick);
		bool flag2 = fGameManager.mloadConfig.AllShouChongData.Count > 2;
		if (flag2)
		{
			this.UI_Btn_3.SetActive(true);
		}
		else
		{
			this.UI_Btn_3.SetActive(false);
		}
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x0015B572 File Offset: 0x00159772
	private void Start()
	{
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x0015B575 File Offset: 0x00159775
	private void Update()
	{
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x0015B578 File Offset: 0x00159778
	public void ShowUIData()
	{
		ShouChongXmlData shouChongXmlData = fGameManager.mloadConfig.AllShouChongData[0];
		fUIManager.GetInstance().GetIconAsset("libao/" + shouChongXmlData.sIconName + ".txt", this.bgRimg);
		switch (LiBaoManager.GetInstance().GetShouChongSatte(0))
		{
		case -1:
			this.UI_Btn_1.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42038, new object[0]);
			this.UI_Btn_1.GetComponent<RectTransform>().sizeDelta = new Vector2(370f, 70f);
			this.UI_Btn_1.GetComponent<Button>().interactable = true;
			break;
		case 0:
			this.UI_Btn_1.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
			this.UI_Btn_1.GetComponent<RectTransform>().sizeDelta = new Vector2(170f, 70f);
			this.UI_Btn_1.GetComponent<Button>().interactable = false;
			break;
		case 1:
			this.UI_Btn_1.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
			this.UI_Btn_1.GetComponent<RectTransform>().sizeDelta = new Vector2(270f, 70f);
			this.UI_Btn_1.GetComponent<Button>().interactable = true;
			break;
		}
		switch (LiBaoManager.GetInstance().GetShouChongSatte(1))
		{
		case -1:
			this.UI_Btn_2.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42039, new object[0]);
			this.UI_Btn_2.GetComponent<RectTransform>().sizeDelta = new Vector2(370f, 70f);
			this.UI_Btn_2.GetComponent<Button>().interactable = true;
			break;
		case 0:
			this.UI_Btn_2.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
			this.UI_Btn_2.GetComponent<RectTransform>().sizeDelta = new Vector2(170f, 70f);
			this.UI_Btn_2.GetComponent<Button>().interactable = false;
			break;
		case 1:
			this.UI_Btn_2.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
			this.UI_Btn_2.GetComponent<RectTransform>().sizeDelta = new Vector2(270f, 70f);
			this.UI_Btn_2.GetComponent<Button>().interactable = true;
			break;
		}
		switch (LiBaoManager.GetInstance().GetShouChongSatte(2))
		{
		case -1:
			this.UI_Btn_3.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42077, new object[0]);
			this.UI_Btn_3.GetComponent<RectTransform>().sizeDelta = new Vector2(370f, 70f);
			this.UI_Btn_3.GetComponent<Button>().interactable = true;
			break;
		case 0:
			this.UI_Btn_3.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
			this.UI_Btn_3.GetComponent<RectTransform>().sizeDelta = new Vector2(170f, 70f);
			this.UI_Btn_3.GetComponent<Button>().interactable = false;
			break;
		case 1:
			this.UI_Btn_3.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
			this.UI_Btn_3.GetComponent<RectTransform>().sizeDelta = new Vector2(270f, 70f);
			this.UI_Btn_3.GetComponent<Button>().interactable = true;
			break;
		}
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x0015B9C8 File Offset: 0x00159BC8
	private void BtnClick(GameObject go)
	{
		bool flag = !go.GetComponent<Button>().interactable;
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			bool flag2 = go.gameObject.name == "UI_Btn_1";
			if (flag2)
			{
				int shouChongSatte = LiBaoManager.GetInstance().GetShouChongSatte(0);
				bool flag3 = shouChongSatte == -1;
				if (flag3)
				{
					fUIManager.GetInstance().OpenChongZhiUI("ShouChongUICon");
					return;
				}
				arrayList.Add(0);
			}
			else
			{
				bool flag4 = go.gameObject.name == "UI_Btn_2";
				if (flag4)
				{
					int shouChongSatte = LiBaoManager.GetInstance().GetShouChongSatte(1);
					bool flag5 = shouChongSatte == -1;
					if (flag5)
					{
						fUIManager.GetInstance().OpenChongZhiUI("ShouChongUICon");
						return;
					}
					arrayList.Add(1);
				}
				else
				{
					bool flag6 = go.gameObject.name == "UI_Btn_3";
					if (flag6)
					{
						int shouChongSatte = LiBaoManager.GetInstance().GetShouChongSatte(2);
						bool flag7 = shouChongSatte == -1;
						if (flag7)
						{
							fUIManager.GetInstance().OpenChongZhiUI("ShouChongUICon");
							return;
						}
						arrayList.Add(2);
					}
				}
			}
			arrayList.Add(0);
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15003,
				arrayList
			});
		}
	}

	// Token: 0x040021E7 RID: 8679
	public static ShouChongUICon inst;

	// Token: 0x040021E8 RID: 8680
	private GameObject UI_Btn_2;

	// Token: 0x040021E9 RID: 8681
	private GameObject UI_Btn_1;

	// Token: 0x040021EA RID: 8682
	private GameObject UI_Btn_3;

	// Token: 0x040021EB RID: 8683
	private RawImage bgRimg;
}
