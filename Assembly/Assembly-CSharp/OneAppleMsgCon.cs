using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000272 RID: 626
public class OneAppleMsgCon : Window
{
	// Token: 0x06001D09 RID: 7433 RVA: 0x0010B824 File Offset: 0x00109A24
	protected override void Awake()
	{
		base.Awake();
		this.oBody = base.transform.Find("body").gameObject;
		this.jieshou_but = this.oBody.transform.Find("jieshou_but").gameObject;
		this.jujue_but = this.oBody.transform.Find("jujue_but").gameObject;
		EventTriggerListener.Get(this.jieshou_but).onClick = new EventTriggerListener.VoidDelegate(this.OnJieShouEvent);
		EventTriggerListener.Get(this.jujue_but).onClick = new EventTriggerListener.VoidDelegate(this.OnJuJueEvent);
		this.tText = this.oBody.transform.Find("Text").GetComponent<Text>();
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0010B8EC File Offset: 0x00109AEC
	private void Update()
	{
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0010B8F0 File Offset: 0x00109AF0
	public void OnSetUI(ArrayList szData)
	{
		this.szApplyData = szData;
		bool flag = this.szApplyData.Count < 1;
		if (flag)
		{
			this.oBody.SetActive(false);
		}
		else
		{
			this.tText.text = Language.GetLang(31006, new object[]
			{
				szData[1]
			});
			this.oBody.SetActive(true);
		}
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x0010B95C File Offset: 0x00109B5C
	private void OnJieShouEvent(GameObject obj)
	{
		bool flag = this.szApplyData.Count >= 1;
		if (flag)
		{
			this.OnUnMsgClick(this.szApplyData[1].ToString(), 1, (int)this.szApplyData[0]);
		}
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0010B9AC File Offset: 0x00109BAC
	private void OnJuJueEvent(GameObject obj)
	{
		bool flag = this.szApplyData.Count >= 1;
		if (flag)
		{
			this.OnUnMsgClick(this.szApplyData[1].ToString(), 0, (int)this.szApplyData[0]);
		}
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0010B9FB File Offset: 0x00109BFB
	private void OnUnMsgClick(string sName, int nType, int nCid)
	{
		FriendManager.GetInstance().UnRequstMsg(nType, sName, nCid);
	}

	// Token: 0x04001979 RID: 6521
	private GameObject oBody;

	// Token: 0x0400197A RID: 6522
	private GameObject jieshou_but;

	// Token: 0x0400197B RID: 6523
	private GameObject jujue_but;

	// Token: 0x0400197C RID: 6524
	private Text tText;

	// Token: 0x0400197D RID: 6525
	private ArrayList szApplyData = new ArrayList();
}
