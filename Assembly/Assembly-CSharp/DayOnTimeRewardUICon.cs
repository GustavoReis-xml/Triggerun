using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F0 RID: 240
public class DayOnTimeRewardUICon : Window
{
	// Token: 0x0600089B RID: 2203 RVA: 0x00049118 File Offset: 0x00047318
	protected override void Awake()
	{
		base.Awake();
		DayOnTimeRewardUICon.inst = this;
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

	// Token: 0x0600089C RID: 2204 RVA: 0x000491D0 File Offset: 0x000473D0
	public void ShowUIData()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(18);
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
				OneDayOnTimeUI oneDayOnTimeUI;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneItem);
					gameObject.transform.SetParent(this.oList.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = key.ToString();
					oneDayOnTimeUI = gameObject.AddComponent<OneDayOnTimeUI>();
				}
				else
				{
					gameObject = transform.gameObject;
					oneDayOnTimeUI = gameObject.GetComponent<OneDayOnTimeUI>();
				}
				gameObject.SetActive(true);
				oneDayOnTimeUI.OnSetUi(aItem, this.oCloneReward);
			}
		}
	}

	// Token: 0x04000634 RID: 1588
	public static DayOnTimeRewardUICon inst;

	// Token: 0x04000635 RID: 1589
	private GameObject oCloneReward;

	// Token: 0x04000636 RID: 1590
	private GameObject oCloneItem;

	// Token: 0x04000637 RID: 1591
	private GameObject oList;

	// Token: 0x04000638 RID: 1592
	private RawImage bgRimg;
}
