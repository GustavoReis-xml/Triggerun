using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000273 RID: 627
public class OneDayOnlineUI : Window
{
	// Token: 0x06001D10 RID: 7440 RVA: 0x0010BA20 File Offset: 0x00109C20
	protected override void Awake()
	{
		base.Awake();
		OneDayOnlineUI.inst = this;
		this.acttext = base.transform.Find("acttext").GetComponent<Text>();
		this.timetext = base.transform.Find("time").GetComponent<Text>();
		this.oBut = base.transform.Find("but").gameObject;
		this.bButton = this.oBut.GetComponent<Button>();
		this.oBut.SetActive(false);
		this.butText = this.oBut.transform.Find("Text").GetComponent<Text>();
		this.rewardlist = base.transform.Find("rewardlist").gameObject;
		EventTriggerListener.Get(this.oBut).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0010BB04 File Offset: 0x00109D04
	private void Update()
	{
		bool flag = Time.time - this.ftime >= 0.1f;
		if (flag)
		{
			this.ftime = Time.time;
			bool flag2 = this.bCheck;
			if (flag2)
			{
				this.CheckTime();
			}
		}
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0010BB4C File Offset: 0x00109D4C
	private void CheckTime()
	{
		bool flag = this.aNowItem == null;
		if (!flag)
		{
			ArrayList onLineReward = LiBaoManager.GetInstance().GetOnLineReward();
			int num = onLineReward.IndexOf(this.aNowItem.index);
			bool flag2 = num != -1;
			if (flag2)
			{
				bool activeSelf = this.oBut.activeSelf;
				if (activeSelf)
				{
					this.bButton.interactable = false;
					this.oBut.SetActive(false);
				}
				this.timetext.text = Language.GetLang(38507, new object[0]);
				this.bCheck = false;
			}
			else
			{
				this.bCheck = true;
				int onLineLong = LiBaoManager.GetInstance().GetOnLineLong();
				bool flag3 = onLineLong < this.aNowItem.OnLineTime * 60;
				if (flag3)
				{
					bool activeSelf2 = this.oBut.activeSelf;
					if (activeSelf2)
					{
						this.bButton.interactable = false;
						this.oBut.SetActive(false);
					}
					this.timetext.text = Fun.sencondsToHMS_CXP(this.aNowItem.OnLineTime * 60 - onLineLong);
				}
				else
				{
					bool flag4 = !this.oBut.activeSelf;
					if (flag4)
					{
						this.bButton.interactable = true;
						this.oBut.SetActive(true);
					}
					this.timetext.text = Language.GetLang(42042, new object[0]);
				}
			}
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0010BCBC File Offset: 0x00109EBC
	public void OnSetUi(AItemData aItem, GameObject oClone)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		this.aNowItem = aItem;
		this.bCheck = true;
		this.acttext.text = Language.GetLang(42041, new object[]
		{
			aItem.OnLineTime
		});
		arrayList = aItem.aryAward;
		int count = arrayList.Count;
		arrayList2 = this.GetItemPos(count);
		for (int i = 0; i < count; i++)
		{
			string[] array = (string[])arrayList[i];
			bool flag = array.Length >= 1;
			if (flag)
			{
				int nItemID = int.Parse(array[0].ToString());
				int nNum = int.Parse(array[1].ToString());
				Transform x = this.rewardlist.transform.Find(nItemID.ToString());
				bool flag2 = x == null;
				if (flag2)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(oClone);
					gameObject.transform.SetParent(this.rewardlist.transform);
					gameObject.transform.localPosition = (Vector3)arrayList2[i];
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
					gameObject.name = nItemID.ToString();
					fUIManager.GetInstance().GetIconAsset("ItemIco/" + this.GetRewardPic(nItemID) + ".txt", gameObject.transform.Find("bg").gameObject.GetComponent<RawImage>());
					gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = this.GetRewardName(nItemID, nNum);
				}
			}
		}
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0010BE94 File Offset: 0x0010A094
	private void RewardBtnClick(GameObject go)
	{
		bool flag = this.aNowItem != null && this.bButton.interactable;
		if (flag)
		{
			int index = this.aNowItem.index;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(index);
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15014,
				arrayList
			});
		}
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0010BF08 File Offset: 0x0010A108
	private string GetRewardPic(int nItemID)
	{
		string empty = string.Empty;
		bool flag = !fGameManager.mloadConfig.AllItemData.ContainsKey(nItemID);
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[nItemID];
			result = itemData.m_sIco;
		}
		return result;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0010BF54 File Offset: 0x0010A154
	private string GetRewardName(int nItemID, int nNum)
	{
		string empty = string.Empty;
		bool flag = !fGameManager.mloadConfig.AllItemData.ContainsKey(nItemID);
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[nItemID];
			result = string.Concat(new string[]
			{
				"<color=#00ff00>",
				itemData.m_sName,
				"*",
				nNum.ToString(),
				"</color>"
			});
		}
		return result;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0010BFD0 File Offset: 0x0010A1D0
	private ArrayList GetItemPos(int nCount)
	{
		ArrayList arrayList = new ArrayList();
		switch (nCount)
		{
		case 1:
			arrayList.Add(new Vector3(0f, -12f, 0f));
			break;
		case 2:
			arrayList.Add(new Vector3(0f, 100f, 0f));
			arrayList.Add(new Vector3(0f, -90f, 0f));
			break;
		case 3:
			arrayList.Add(new Vector3(-70f, 100f, 0f));
			arrayList.Add(new Vector3(70f, 100f, 0f));
			arrayList.Add(new Vector3(0f, -90f, 0f));
			break;
		}
		return arrayList;
	}

	// Token: 0x0400197E RID: 6526
	public static OneDayOnlineUI inst;

	// Token: 0x0400197F RID: 6527
	private Text acttext;

	// Token: 0x04001980 RID: 6528
	private Text timetext;

	// Token: 0x04001981 RID: 6529
	private GameObject oBut;

	// Token: 0x04001982 RID: 6530
	private Text butText;

	// Token: 0x04001983 RID: 6531
	private GameObject rewardlist;

	// Token: 0x04001984 RID: 6532
	private AItemData aNowItem;

	// Token: 0x04001985 RID: 6533
	private Button bButton;

	// Token: 0x04001986 RID: 6534
	private bool bCheck = true;

	// Token: 0x04001987 RID: 6535
	private float ftime;
}
