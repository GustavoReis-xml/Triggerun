using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EF RID: 239
public class DayOnlineRewardUICon : Window
{
	// Token: 0x06000898 RID: 2200 RVA: 0x00048EC4 File Offset: 0x000470C4
	protected override void Awake()
	{
		base.Awake();
		DayOnlineRewardUICon.inst = this;
		this.bgRimg = base.transform.GetComponent<RawImage>();
		this.oCloneReward = this.UIGameObj.Get_GameObj(0);
		this.oCloneReward.SetActive(false);
		this.oCloneItem = this.UIGameObj.Get_GameObj(1);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = this.oCloneItem.transform.Find("acttext").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
		}
		this.oCloneItem.SetActive(false);
		this.oList = this.UIGameObj.Get_GameObj(2);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00048F7C File Offset: 0x0004717C
	public void ShowUIData()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(17);
		bool flag = actDataXML_ID == 0;
		if (!flag)
		{
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			fUIManager.GetInstance().GetIconAsset("libao/" + actDaTingAward.bgimg + ".txt", this.bgRimg);
			foreach (int key in actDaTingAward.szActList.Keys)
			{
				AItemData aItem = actDaTingAward.szActList[key];
				Transform transform = this.oList.transform.Find(key.ToString());
				bool flag2 = transform == null;
				GameObject gameObject;
				OneDayOnlineUI oneDayOnlineUI;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneItem);
					gameObject.transform.SetParent(this.oList.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = key.ToString();
					oneDayOnlineUI = gameObject.AddComponent<OneDayOnlineUI>();
				}
				else
				{
					gameObject = transform.gameObject;
					oneDayOnlineUI = gameObject.GetComponent<OneDayOnlineUI>();
				}
				gameObject.SetActive(true);
				oneDayOnlineUI.OnSetUi(aItem, this.oCloneReward);
			}
		}
	}

	// Token: 0x0400062F RID: 1583
	public static DayOnlineRewardUICon inst;

	// Token: 0x04000630 RID: 1584
	private GameObject oCloneReward;

	// Token: 0x04000631 RID: 1585
	private GameObject oCloneItem;

	// Token: 0x04000632 RID: 1586
	private GameObject oList;

	// Token: 0x04000633 RID: 1587
	private RawImage bgRimg;
}
