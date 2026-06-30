using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024E RID: 590
public class NaLianXuUICon : Window
{
	// Token: 0x06001A90 RID: 6800 RVA: 0x000FD1A8 File Offset: 0x000FB3A8
	protected override void Awake()
	{
		base.Awake();
		NaLianXuUICon.inst = this;
		this.OneReward = this.UIGameObj.Get_GameObj(0);
		this.OneReward.SetActive(false);
		this.RewardList = this.UIGameObj.Get_GameObj(1);
		this.TitileMsgTxt = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.LoginNumTxt = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.nationalUIBG = this.RewardList.transform.parent.GetComponent<RawImage>();
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x000FD240 File Offset: 0x000FB440
	public void ShowUIData()
	{
		bool flag = this.RewardList.transform.childCount < 1;
		if (flag)
		{
			this.ShowRewardList();
		}
		this.UpDataAllReward();
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000FD278 File Offset: 0x000FB478
	private void ShowRewardList()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(32);
		bool flag = actDataByType == null;
		if (!flag)
		{
			NaLianXuXmlDatas naLianXuXmlDatas = fGameManager.mloadConfig.AllNaLianXuData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + naLianXuXmlDatas.sIconName + ".txt", this.nationalUIBG);
			foreach (int num in naLianXuXmlDatas.naLianXus.Keys)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneReward);
				gameObject.transform.SetParent(this.RewardList.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				RawImage component = gameObject.GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset("libao/guoqing/frame_1.txt", component);
				gameObject.transform.FindChild("RewardTitle").GetComponent<Text>().text = naLianXuXmlDatas.naLianXus[num].name;
				gameObject.transform.FindChild("RewardMsg").GetComponent<Text>().text = naLianXuXmlDatas.naLianXus[num].msg;
				RawImage component2 = gameObject.transform.FindChild("RewardImage").GetComponent<RawImage>();
				fUIManager.GetInstance().GetIconAsset("libao/guoqing/" + naLianXuXmlDatas.naLianXus[num].img + ".txt", component2);
				bool flag2 = LiBaoManager.GetInstance().GetJieriLoginReward(num) == 1;
				if (flag2)
				{
					gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
				}
				else
				{
					gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
				gameObject.SetActive(true);
				GameObject gameObject2 = gameObject.transform.FindChild("RewardBtn").gameObject;
				gameObject2.name = string.Format("Btn_{0}", num);
				EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
				this.AllRewardObj.Add(num, gameObject);
			}
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000FD52C File Offset: 0x000FB72C
	public void UpDataAllReward()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(32);
		bool flag = actDataByType == null;
		if (!flag)
		{
			int jieriLoginDay = LiBaoManager.GetInstance().GetJieriLoginDay();
			NaLianXuXmlDatas naLianXuXmlDatas = fGameManager.mloadConfig.AllNaLianXuData[actDataByType.nXmlId];
			this.LoginNumTxt.text = string.Format(naLianXuXmlDatas.sBottom, jieriLoginDay);
			this.TitileMsgTxt.text = naLianXuXmlDatas.sTitle;
			foreach (int num in this.AllRewardObj.Keys)
			{
				Button component = this.AllRewardObj[num].transform.FindChild(string.Format("Btn_{0}", num)).GetComponent<Button>();
				Text component2 = this.AllRewardObj[num].transform.FindChild(string.Format("Btn_{0}/Text", num)).GetComponent<Text>();
				int jieriLoginReward = LiBaoManager.GetInstance().GetJieriLoginReward(num);
				bool flag2 = num > jieriLoginDay;
				if (flag2)
				{
					component.interactable = false;
					component2.text = Language.GetLang(42011, new object[0]);
				}
				else
				{
					bool flag3 = jieriLoginReward != 1;
					if (flag3)
					{
						component.interactable = false;
						component2.text = Language.GetLang(42006, new object[0]);
					}
					else
					{
						component.interactable = true;
						component2.text = Language.GetLang(42005, new object[0]);
					}
				}
			}
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000FD6F4 File Offset: 0x000FB8F4
	private void RewardBtnClick(GameObject go)
	{
		string[] array = go.name.Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[1]);
		bool interactable = go.GetComponent<Button>().interactable;
		if (interactable)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(num);
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15032,
				arrayList
			});
		}
	}

	// Token: 0x04001556 RID: 5462
	public static NaLianXuUICon inst;

	// Token: 0x04001557 RID: 5463
	private GameObject OneReward;

	// Token: 0x04001558 RID: 5464
	private GameObject RewardList;

	// Token: 0x04001559 RID: 5465
	private Text TitileMsgTxt;

	// Token: 0x0400155A RID: 5466
	private Text LoginNumTxt;

	// Token: 0x0400155B RID: 5467
	private RawImage nationalUIBG;

	// Token: 0x0400155C RID: 5468
	private Dictionary<int, GameObject> AllRewardObj = new Dictionary<int, GameObject>();
}
