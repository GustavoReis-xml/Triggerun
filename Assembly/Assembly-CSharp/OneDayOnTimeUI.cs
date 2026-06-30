using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000274 RID: 628
public class OneDayOnTimeUI : Window
{
	// Token: 0x06001D19 RID: 7449 RVA: 0x0010C0E0 File Offset: 0x0010A2E0
	protected override void Awake()
	{
		base.Awake();
		OneDayOnTimeUI.inst = this;
		this.acttext = base.transform.Find("acttext").GetComponent<Text>();
		this.timetext = base.transform.Find("time").GetComponent<Text>();
		this.timetext.text = string.Empty;
		this.oBut = base.transform.Find("but").gameObject;
		this.bButton = this.oBut.GetComponent<Button>();
		this.oBut.SetActive(false);
		this.butText = this.oBut.transform.Find("Text").GetComponent<Text>();
		this.rewardlist = base.transform.Find("rewardlist").gameObject;
		this.butimg = this.oBut.GetComponent<Image>();
		EventTriggerListener.Get(this.oBut).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0010C1E4 File Offset: 0x0010A3E4
	public void OnSetUi(AItemData aItem, GameObject oClone)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		this.aNowItem = aItem;
		this.acttext.text = aItem.sOnTimeDes;
		this.dStartTime = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd") + " " + aItem.sOnTimeStart);
		this.dEndTime = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd") + " " + aItem.sOnTimeEnd);
		this.dOnLoginData = LiBaoManager.GetInstance().GetDayOnLoginDate();
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
		ArrayList onTimeReward = LiBaoManager.GetInstance().GetOnTimeReward();
		int num = onTimeReward.IndexOf(aItem.index);
		bool flag3 = num != -1;
		if (flag3)
		{
			this.bCheck = false;
			int num2 = (int)onTimeReward[num + 1];
			bool flag4 = num2 == 1;
			if (flag4)
			{
				this.SetButStat(1);
			}
			else
			{
				this.SetButStat(0);
			}
		}
		else
		{
			this.bCheck = true;
		}
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0010C47C File Offset: 0x0010A67C
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

	// Token: 0x06001D1C RID: 7452 RVA: 0x0010C4C4 File Offset: 0x0010A6C4
	private void CheckTime()
	{
		bool flag = !(this.dStartTime == DateTime.MinValue) && !(this.dEndTime == DateTime.MinValue);
		if (flag)
		{
			bool flag2 = (int)(this.dOnLoginData - this.dEndTime).TotalMilliseconds > 0;
			if (flag2)
			{
				this.SetButStat(2);
			}
			else
			{
				bool flag3 = (int)(this.dStartTime - Fun.GetServerDate()).TotalMilliseconds > 0;
				if (flag3)
				{
					this.SetButStat(-1);
				}
				else
				{
					this.SetButStat(0);
				}
			}
		}
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x0010C564 File Offset: 0x0010A764
	private void SetButStat(int nType)
	{
		bool flag = false;
		string text = string.Empty;
		this.butimg.enabled = true;
		switch (nType)
		{
		case -1:
			flag = false;
			text = Language.GetLang(42045, new object[0]);
			this.butimg.enabled = false;
			break;
		case 0:
			flag = true;
			text = Language.GetLang(42042, new object[0]);
			break;
		case 1:
			flag = false;
			text = Language.GetLang(38507, new object[0]);
			break;
		case 2:
			flag = false;
			text = Language.GetLang(42044, new object[0]);
			break;
		}
		bool flag2 = !this.oBut.activeSelf;
		if (flag2)
		{
			this.oBut.SetActive(true);
		}
		bool flag3 = this.bButton.interactable != flag;
		if (flag3)
		{
			this.bButton.interactable = flag;
			bool flag4 = flag;
			if (flag4)
			{
				this.butimg.sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, "LiBao_han_normal");
			}
			else
			{
				this.butimg.sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, "LiBao_han_dis");
			}
		}
		this.butText.text = text;
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x0010C6A0 File Offset: 0x0010A8A0
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
				15016,
				arrayList
			});
		}
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x0010C714 File Offset: 0x0010A914
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

	// Token: 0x06001D20 RID: 7456 RVA: 0x0010C760 File Offset: 0x0010A960
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

	// Token: 0x06001D21 RID: 7457 RVA: 0x0010C7DC File Offset: 0x0010A9DC
	private ArrayList GetItemPos(int nCount)
	{
		ArrayList arrayList = new ArrayList();
		switch (nCount)
		{
		case 1:
			arrayList.Add(new Vector3(0f, -12f, 0f));
			break;
		case 2:
			arrayList.Add(new Vector3(0f, 90f, 0f));
			arrayList.Add(new Vector3(0f, -105f, 0f));
			break;
		case 3:
			arrayList.Add(new Vector3(-70f, 100f, 0f));
			arrayList.Add(new Vector3(70f, 100f, 0f));
			arrayList.Add(new Vector3(0f, -105f, 0f));
			break;
		}
		return arrayList;
	}

	// Token: 0x04001988 RID: 6536
	public static OneDayOnTimeUI inst;

	// Token: 0x04001989 RID: 6537
	private Text acttext;

	// Token: 0x0400198A RID: 6538
	private Text timetext;

	// Token: 0x0400198B RID: 6539
	private GameObject oBut;

	// Token: 0x0400198C RID: 6540
	private Text butText;

	// Token: 0x0400198D RID: 6541
	private GameObject rewardlist;

	// Token: 0x0400198E RID: 6542
	private AItemData aNowItem;

	// Token: 0x0400198F RID: 6543
	private Button bButton;

	// Token: 0x04001990 RID: 6544
	private Image butimg;

	// Token: 0x04001991 RID: 6545
	private DateTime dStartTime = DateTime.MinValue;

	// Token: 0x04001992 RID: 6546
	private DateTime dEndTime = DateTime.MinValue;

	// Token: 0x04001993 RID: 6547
	private DateTime dOnLoginData = DateTime.MinValue;

	// Token: 0x04001994 RID: 6548
	private bool bCheck;

	// Token: 0x04001995 RID: 6549
	private float ftime;
}
