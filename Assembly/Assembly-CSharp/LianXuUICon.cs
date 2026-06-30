using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000217 RID: 535
public class LianXuUICon : Window
{
	// Token: 0x0600172F RID: 5935 RVA: 0x000D8A7C File Offset: 0x000D6C7C
	protected override void Awake()
	{
		base.Awake();
		LianXuUICon.inst = this;
		this.OneReward = this.UIGameObj.Get_GameObj(0);
		this.OneReward.SetActive(false);
		this.RewardList = this.UIGameObj.Get_GameObj(1);
		this.TitileMsgTxt = this.UIGameObj.Get_GameObj(2);
		this.LoginNumTxt = this.UIGameObj.Get_GameObj(3);
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x000D8AEC File Offset: 0x000D6CEC
	public void ShowUIData()
	{
		this.TitileMsgTxt.GetComponent<Text>().text = Language.GetLang(42008, new object[0]);
		this.LoginNumTxt.GetComponent<Text>().text = Language.GetLang(42007, new object[]
		{
			LiBaoManager.GetInstance().Getm_nLianXuNum()
		});
		bool flag = this.RewardList.transform.childCount < 1;
		if (flag)
		{
			this.ShowRewardList();
		}
		this.UpDataAllReward();
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000D8B78 File Offset: 0x000D6D78
	private void ShowRewardList()
	{
		Dictionary<int, LianXuXmlData> allLianXuData = fGameManager.mloadConfig.AllLianXuData;
		foreach (int num in allLianXuData.Keys)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneReward);
			gameObject.transform.SetParent(this.RewardList.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.FindChild("RewardTitle").GetComponent<Text>().text = allLianXuData[num].name;
			gameObject.transform.FindChild("RewardMsg").GetComponent<Text>().text = allLianXuData[num].msg;
			gameObject.transform.FindChild("RewardImage").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_LIBAO, allLianXuData[num].img);
			bool flag = LiBaoManager.GetInstance().Get_LianXuState(num) == -1;
			if (flag)
			{
				gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
			}
			else
			{
				gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
			}
			gameObject.SetActive(true);
			GameObject gameObject2 = gameObject.transform.FindChild("RewardBtn").gameObject;
			gameObject2.name = "Btn_" + num.ToString();
			EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
			this.AllRewardObj.Add(num, gameObject);
		}
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x000D8D78 File Offset: 0x000D6F78
	public void UpDataAllReward()
	{
		Dictionary<int, LianXuXmlData> allLianXuData = fGameManager.mloadConfig.AllLianXuData;
		int num = LiBaoManager.GetInstance().Getm_nLianXuNum();
		foreach (int num2 in this.AllRewardObj.Keys)
		{
			bool flag = allLianXuData[num2].denglunum > num;
			if (flag)
			{
				this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString()).GetComponent<Button>().interactable = false;
				bool flag2 = LiBaoManager.GetInstance().Get_LianXuState(num2) == -1;
				if (flag2)
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42011, new object[0]);
				}
				else
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
			}
			else
			{
				bool flag3 = LiBaoManager.GetInstance().Get_LianXuState(num2) != -1;
				if (flag3)
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString()).GetComponent<Button>().interactable = false;
				}
				else
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString()).GetComponent<Button>().interactable = true;
				}
				bool flag4 = LiBaoManager.GetInstance().Get_LianXuState(num2) == -1;
				if (flag4)
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
				}
				else
				{
					this.AllRewardObj[num2].transform.FindChild("Btn_" + num2.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
			}
		}
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x000D901C File Offset: 0x000D721C
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
				15005,
				arrayList
			});
		}
	}

	// Token: 0x04001268 RID: 4712
	public static LianXuUICon inst;

	// Token: 0x04001269 RID: 4713
	private GameObject OneReward;

	// Token: 0x0400126A RID: 4714
	private GameObject RewardList;

	// Token: 0x0400126B RID: 4715
	private GameObject TitileMsgTxt;

	// Token: 0x0400126C RID: 4716
	private GameObject LoginNumTxt;

	// Token: 0x0400126D RID: 4717
	private Dictionary<int, GameObject> AllRewardObj = new Dictionary<int, GameObject>();
}
