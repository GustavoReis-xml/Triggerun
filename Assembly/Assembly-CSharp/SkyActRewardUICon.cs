using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D6 RID: 982
public class SkyActRewardUICon : Window
{
	// Token: 0x06002A09 RID: 10761 RVA: 0x00171958 File Offset: 0x0016FB58
	protected override void Awake()
	{
		base.Awake();
		SkyActRewardUICon.inst = this;
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
		this.actsilder = this.UIGameObj.Get_GameObj(3);
		this.imgfill = this.actsilder.transform.Find("fill").GetComponent<Image>();
		this.oTargetList = this.actsilder.transform.Find("list").gameObject;
		this.CloneTarget = this.UIGameObj.Get_GameObj(4);
		this.CloneTarget.SetActive(false);
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x00171A84 File Offset: 0x0016FC84
	public void ShowUIData()
	{
		int actDataXML_ID = ActivityManager.GetInstance().GetActDataXML_ID(19);
		bool flag = actDataXML_ID == 0;
		if (!flag)
		{
			int num = 0;
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataXML_ID];
			fUIManager.GetInstance().GetIconAsset("libao/" + actDaTingAward.bgimg + ".txt", this.bgRimg);
			foreach (int key in actDaTingAward.szActList.Keys)
			{
				AItemData aitemData = actDaTingAward.szActList[key];
				Transform transform = this.oList.transform.Find(key.ToString());
				bool flag2 = transform == null;
				GameObject gameObject;
				OneSkyActUI oneSkyActUI;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneItem);
					gameObject.transform.SetParent(this.oList.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = key.ToString();
					oneSkyActUI = gameObject.AddComponent<OneSkyActUI>();
				}
				else
				{
					gameObject = transform.gameObject;
					oneSkyActUI = gameObject.GetComponent<OneSkyActUI>();
				}
				gameObject.SetActive(true);
				bool flag3 = aitemData.skyacttargetnum > num;
				if (flag3)
				{
					num = aitemData.skyacttargetnum;
				}
				oneSkyActUI.OnSetUi(aitemData, this.oCloneReward);
			}
			int num2 = LiBaoManager.GetInstance().GetSkyActNum();
			bool flag4 = num2 > num;
			if (flag4)
			{
				num2 = num;
			}
			float fillAmount = (float)num2 * 1f / (float)num * 1f;
			this.imgfill.fillAmount = fillAmount;
			Fun.ClearChildObejct(this.oTargetList);
			foreach (int key2 in actDaTingAward.szActList.Keys)
			{
				AItemData aitemData = actDaTingAward.szActList[key2];
				this.AddTarget(aitemData, 1290 / num);
			}
		}
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x00171D04 File Offset: 0x0016FF04
	private void AddTarget(AItemData aItem, int mOnePos)
	{
		Vector3 zero = Vector3.zero;
		int skyacttargetnum = aItem.skyacttargetnum;
		zero.x = (float)(skyacttargetnum * mOnePos - 645);
		Transform transform = this.oTargetList.transform.Find(aItem.index.ToString());
		bool flag = transform == null;
		GameObject gameObject;
		if (flag)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CloneTarget);
			gameObject.transform.SetParent(this.oTargetList.transform);
			gameObject.transform.localPosition = zero;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localScale = Vector3.one;
			gameObject.name = aItem.index.ToString();
		}
		else
		{
			gameObject = transform.gameObject;
		}
		gameObject.SetActive(true);
		gameObject.transform.Find("target").GetComponent<Text>().text = skyacttargetnum.ToString();
	}

	// Token: 0x04002373 RID: 9075
	public static SkyActRewardUICon inst;

	// Token: 0x04002374 RID: 9076
	private GameObject oCloneReward;

	// Token: 0x04002375 RID: 9077
	private GameObject oCloneItem;

	// Token: 0x04002376 RID: 9078
	private GameObject oList;

	// Token: 0x04002377 RID: 9079
	private GameObject actsilder;

	// Token: 0x04002378 RID: 9080
	private GameObject CloneTarget;

	// Token: 0x04002379 RID: 9081
	private Image imgfill;

	// Token: 0x0400237A RID: 9082
	private GameObject oTargetList;

	// Token: 0x0400237B RID: 9083
	private RawImage bgRimg;
}
