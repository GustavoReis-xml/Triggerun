using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200048E RID: 1166
public class UserLvRewardUICon : Window
{
	// Token: 0x06002D04 RID: 11524 RVA: 0x0018BEB8 File Offset: 0x0018A0B8
	protected override void Awake()
	{
		base.Awake();
		UserLvRewardUICon.inst = this;
		this.OneReward = this.UIGameObj.Get_GameObj(0);
		this.OneReward.SetActive(false);
		this.LoginNumTxt = this.UIGameObj.Get_GameObj(3);
		this.Bnt_0 = this.UIGameObj.Get_GameObj(4);
		this.Bnt_1 = this.UIGameObj.Get_GameObj(5);
		this.Bnt_2 = this.UIGameObj.Get_GameObj(6);
		EventTriggerListener.Get(this.Bnt_0).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
		EventTriggerListener.Get(this.Bnt_1).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
		EventTriggerListener.Get(this.Bnt_2).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
		this.AllRewardObj.Add(0, this.Bnt_0);
		this.AllRewardObj.Add(1, this.Bnt_1);
		this.AllRewardObj.Add(2, this.Bnt_2);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = this.Bnt_0.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
			component = this.Bnt_1.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
			component = this.Bnt_2.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mDINMedium;
		}
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x0018C054 File Offset: 0x0018A254
	public void ShowUIData()
	{
		string text = string.Empty;
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(10);
		bool flag = actDataByType != null;
		if (flag)
		{
			text = Convert.ToDateTime(actDataByType.sStartTime).ToString("yyyy/MM/dd") + "-" + Convert.ToDateTime(actDataByType.sEndTime).AddDays(-1.0).ToString("yyyy/MM/dd");
			this.LoginNumTxt.GetComponent<Text>().text = Language.GetLang(42021, new object[]
			{
				text
			});
		}
		else
		{
			this.LoginNumTxt.GetComponent<Text>().text = Language.GetLang(42022, new object[0]);
		}
		this.ShowBg();
		this.UpDataAllReward();
	}

	// Token: 0x06002D06 RID: 11526 RVA: 0x0018C123 File Offset: 0x0018A323
	private void ShowRewardList()
	{
	}

	// Token: 0x06002D07 RID: 11527 RVA: 0x0018C128 File Offset: 0x0018A328
	public void UpDataAllReward()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(10);
		bool flag = actDataXML_ID == 0;
		if (!flag)
		{
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			this.nGetNum = 0;
			foreach (int num in this.AllRewardObj.Keys)
			{
				bool flag2 = actDaTingAward.szActList[num].needLv > PlayerDataManager.getInstance().HUserData.m_nLevel;
				if (flag2)
				{
					this.AllRewardObj[num].GetComponent<Button>().interactable = false;
					bool flag3 = LiBaoManager.GetInstance().Get_UserLvState(num) == -1;
					if (flag3)
					{
						this.AllRewardObj[num].transform.FindChild("Text").GetComponent<Text>().text = actDaTingAward.szActList[num].sName;
					}
					else
					{
						this.AllRewardObj[num].transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
					}
				}
				else
				{
					bool flag4 = LiBaoManager.GetInstance().Get_UserLvState(num) != -1;
					if (flag4)
					{
						this.AllRewardObj[num].GetComponent<Button>().interactable = false;
					}
					else
					{
						this.AllRewardObj[num].GetComponent<Button>().interactable = true;
					}
					bool flag5 = LiBaoManager.GetInstance().Get_UserLvState(num) == -1;
					if (flag5)
					{
						this.AllRewardObj[num].transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
						this.nGetNum++;
					}
					else
					{
						this.AllRewardObj[num].transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
					}
				}
			}
		}
	}

	// Token: 0x06002D08 RID: 11528 RVA: 0x0018C384 File Offset: 0x0018A584
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
				15008,
				arrayList
			});
		}
	}

	// Token: 0x06002D09 RID: 11529 RVA: 0x0018C404 File Offset: 0x0018A604
	private void ShowBg()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(10);
		bool flag = actDataXML_ID != 0;
		if (flag)
		{
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			fUIManager.GetInstance().GetIconAsset("libao/userlv/" + actDaTingAward.bgimg, base.transform.GetComponent<RawImage>());
		}
	}

	// Token: 0x04002639 RID: 9785
	public static UserLvRewardUICon inst;

	// Token: 0x0400263A RID: 9786
	private GameObject OneReward;

	// Token: 0x0400263B RID: 9787
	private GameObject LoginNumTxt;

	// Token: 0x0400263C RID: 9788
	private GameObject Bnt_0;

	// Token: 0x0400263D RID: 9789
	private GameObject Bnt_1;

	// Token: 0x0400263E RID: 9790
	private GameObject Bnt_2;

	// Token: 0x0400263F RID: 9791
	private Dictionary<int, GameObject> AllRewardObj = new Dictionary<int, GameObject>();

	// Token: 0x04002640 RID: 9792
	public int nGetNum;
}
