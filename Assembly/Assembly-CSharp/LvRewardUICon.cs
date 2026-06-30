using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000229 RID: 553
public class LvRewardUICon : Window
{
	// Token: 0x060018F2 RID: 6386 RVA: 0x000E9CD0 File Offset: 0x000E7ED0
	protected override void Awake()
	{
		base.Awake();
		LvRewardUICon.inst = this;
		this.bgRimg = base.gameObject.GetComponent<RawImage>();
		this.RewardBnt = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(this.RewardBnt).onClick = new EventTriggerListener.VoidDelegate(this.GetReward);
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000E9D2C File Offset: 0x000E7F2C
	private void GetReward(GameObject go)
	{
		bool interactable = go.GetComponent<Button>().interactable;
		if (interactable)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(0);
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15006,
				arrayList
			});
		}
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x000E9D88 File Offset: 0x000E7F88
	public void ShowUIData()
	{
		LvRewardXmlData lvRewardXmlData = fGameManager.mloadConfig.AllLvReward[0];
		fUIManager.GetInstance().GetIconAsset("libao/" + lvRewardXmlData.sIconName + ".txt", this.bgRimg);
		switch (LiBaoManager.GetInstance().GetLvRewardSatte(0))
		{
		case -1:
			this.RewardBnt.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42011, new object[0]);
			this.RewardBnt.GetComponent<Button>().interactable = false;
			break;
		case 0:
			this.RewardBnt.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
			this.RewardBnt.GetComponent<Button>().interactable = false;
			break;
		case 1:
			this.RewardBnt.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
			this.RewardBnt.GetComponent<Button>().interactable = true;
			break;
		}
	}

	// Token: 0x040013AB RID: 5035
	public static LvRewardUICon inst;

	// Token: 0x040013AC RID: 5036
	private GameObject RewardBnt;

	// Token: 0x040013AD RID: 5037
	private RawImage bgRimg;
}
