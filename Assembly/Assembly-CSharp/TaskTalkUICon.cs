using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000401 RID: 1025
public class TaskTalkUICon : Window
{
	// Token: 0x06002AE7 RID: 10983 RVA: 0x00179F74 File Offset: 0x00178174
	protected override void Awake()
	{
		base.Awake();
		TaskTalkUICon.inst = this;
		this.TitleText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.ContentText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.pOkBtn = this.UIGameObj.Get_GameObj(2);
		this.pItemPanel = this.UIGameObj.Get_GameObj(3);
		this.pOneItem = this.UIGameObj.Get_GameObj(4);
		this.comtipBg = this.UIGameObj.Get_GameObj(5).GetComponent<RawImage>();
		this.pOneItem.SetActive(false);
		EventTriggerListener.Get(this.pOkBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOk);
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x0017A038 File Offset: 0x00178238
	public void OnUpdateData(taskinfo tf)
	{
		string text = string.Empty;
		this.BaseXmlData = fGameManager.mloadConfig.AllTaskObj[tf.m_nTaskId];
		this.TaskData = tf;
		this.TitleText.text = this.BaseXmlData.m_sTaskName;
		text = TaskManager.GetInstance().GetsTarget(this.BaseXmlData, this.TaskData);
		this.ContentText.text = text;
		this.comTipState = false;
		fUIManager.tipBlur.rawImage = this.comtipBg;
		fUIManager.tipBlur.enabled = true;
		this.ShowReward();
	}

	// Token: 0x06002AE9 RID: 10985 RVA: 0x0017A0D4 File Offset: 0x001782D4
	private void ShowReward()
	{
		ArrayList arrayList = new ArrayList();
		int nZuanShi = this.BaseXmlData.m_nZuanShi;
		int nMoney = this.BaseXmlData.m_nMoney;
		arrayList.Add(9999);
		arrayList.Add(nZuanShi);
		arrayList.Add(0);
		arrayList.Add(9998);
		arrayList.Add(nMoney);
		arrayList.Add(0);
		int count = arrayList.Count;
		fUIManager.OnClearObj(this.ObjRewardList);
		for (int i = 0; i < count; i += 3)
		{
			int num = (int)arrayList[i + 1];
			bool flag = num != 0;
			if (flag)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneItem);
				gameObject.transform.SetParent(this.pItemPanel.transform);
				gameObject.name = (i + 1).ToString();
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.FindChild("Text").GetComponent<Text>().text = num.ToString();
				this.ObjRewardList.Add(gameObject);
			}
		}
	}

	// Token: 0x06002AEA RID: 10986 RVA: 0x0017A250 File Offset: 0x00178450
	private void Update()
	{
		bool flag = !this.comTipState && this.comtipBg.texture != null;
		if (flag)
		{
			this.comtipBg.enabled = true;
			this.comTipState = true;
		}
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x0017A294 File Offset: 0x00178494
	public void OnClickOk(GameObject obj)
	{
		this.Close();
		TaskManager.GetInstance().TaskCmdToServer(71005, new ArrayList
		{
			this.TaskData.m_nTaskId,
			this.TaskData.m_nEndNpcId,
			this.TaskData.m_nAutoId
		});
	}

	// Token: 0x04002485 RID: 9349
	public static TaskTalkUICon inst;

	// Token: 0x04002486 RID: 9350
	private taskinfo TaskData;

	// Token: 0x04002487 RID: 9351
	private TaskBaseData BaseXmlData;

	// Token: 0x04002488 RID: 9352
	private Text TitleText;

	// Token: 0x04002489 RID: 9353
	private Text ContentText;

	// Token: 0x0400248A RID: 9354
	private GameObject pOkBtn;

	// Token: 0x0400248B RID: 9355
	private GameObject pItemPanel;

	// Token: 0x0400248C RID: 9356
	private GameObject pOneItem;

	// Token: 0x0400248D RID: 9357
	private List<GameObject> ObjRewardList = new List<GameObject>();

	// Token: 0x0400248E RID: 9358
	private bool comTipState;

	// Token: 0x0400248F RID: 9359
	private RawImage comtipBg;
}
