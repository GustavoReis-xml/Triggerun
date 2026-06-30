using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000283 RID: 643
public class OneSkyActUI : Window
{
	// Token: 0x06001D93 RID: 7571 RVA: 0x00112768 File Offset: 0x00110968
	protected override void Awake()
	{
		base.Awake();
		OneSkyActUI.inst = this;
		this.acttext = base.transform.Find("acttext").GetComponent<Text>();
		this.oBut = base.transform.Find("but").gameObject;
		this.bButton = this.oBut.GetComponent<Button>();
		this.oBut.SetActive(false);
		this.butText = this.oBut.transform.Find("Text").GetComponent<Text>();
		this.rewardlist = base.transform.Find("rewardlist").gameObject;
		this.butimg = this.oBut.GetComponent<Image>();
		EventTriggerListener.Get(this.oBut).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00112840 File Offset: 0x00110A40
	public void OnSetUi(AItemData aItem, GameObject oClone)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		this.aNowItem = aItem;
		this.acttext.text = aItem.skyactDes;
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
		bool flag3 = !this.oBut.activeSelf;
		if (flag3)
		{
			this.oBut.SetActive(true);
		}
		int skyActNum = LiBaoManager.GetInstance().GetSkyActNum();
		ArrayList skyActReward = LiBaoManager.GetInstance().GetSkyActReward();
		int num = skyActReward.IndexOf(aItem.index);
		bool flag4 = num != -1;
		if (flag4)
		{
			this.bButton.interactable = false;
			this.butimg.enabled = true;
			this.butimg.sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, "LiBao_han_dis");
			this.butText.text = Language.GetLang(38507, new object[0]);
		}
		else
		{
			bool flag5 = skyActNum >= aItem.skyacttargetnum;
			if (flag5)
			{
				this.bButton.interactable = true;
				this.butimg.enabled = true;
				this.butimg.sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, "LiBao_han_normal");
				this.butText.text = Language.GetLang(42042, new object[0]);
			}
			else
			{
				this.bButton.interactable = false;
				this.butText.text = Language.GetLang(42049, new object[0]);
				this.butimg.enabled = false;
			}
		}
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x00112B50 File Offset: 0x00110D50
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
				15018,
				arrayList
			});
		}
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00112BC4 File Offset: 0x00110DC4
	private ArrayList GetItemPos(int nCount)
	{
		ArrayList arrayList = new ArrayList();
		switch (nCount)
		{
		case 1:
			arrayList.Add(new Vector3(0f, -12f, 0f));
			break;
		case 2:
			arrayList.Add(new Vector3(0f, 110f, 0f));
			arrayList.Add(new Vector3(0f, -52f, 0f));
			break;
		case 3:
			arrayList.Add(new Vector3(-70f, 100f, 0f));
			arrayList.Add(new Vector3(70f, 100f, 0f));
			arrayList.Add(new Vector3(0f, -90f, 0f));
			break;
		}
		return arrayList;
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x00112CC4 File Offset: 0x00110EC4
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

	// Token: 0x06001D98 RID: 7576 RVA: 0x00112D10 File Offset: 0x00110F10
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

	// Token: 0x04001A6D RID: 6765
	public static OneSkyActUI inst;

	// Token: 0x04001A6E RID: 6766
	private Text acttext;

	// Token: 0x04001A6F RID: 6767
	private GameObject oBut;

	// Token: 0x04001A70 RID: 6768
	private Text butText;

	// Token: 0x04001A71 RID: 6769
	private GameObject rewardlist;

	// Token: 0x04001A72 RID: 6770
	private AItemData aNowItem;

	// Token: 0x04001A73 RID: 6771
	private Button bButton;

	// Token: 0x04001A74 RID: 6772
	private Image butimg;
}
