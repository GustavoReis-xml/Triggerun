using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class SystemMsgTipCon : Window
{
	// Token: 0x06002AB3 RID: 10931 RVA: 0x00178538 File Offset: 0x00176738
	protected override void Awake()
	{
		base.Awake();
		SystemMsgTipCon.inst = this;
		this.BaseTipObj = this.UIGameObj.Get_GameObj(0);
		this.BaseTipObj.SetActive(false);
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x00178568 File Offset: 0x00176768
	public void CreateMsgTip(string sTabletext, string sConttext, ArrayList szBtnlist, int nTime, bool isShowSelect, ArrayList szPram, SystemMsgTipCon.onOkClick fun, int nTsize = 14, int nCsiez = 14)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BaseTipObj);
		gameObject.transform.SetParent(base.transform);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		this.InitAddTipPos(gameObject);
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x001785C8 File Offset: 0x001767C8
	private void InitAddTipPos(GameObject obj)
	{
		this.MsgList.Add(obj);
		for (int i = 0; i < this.MsgList.Count; i++)
		{
			GameObject gameObject = (GameObject)this.MsgList[i];
			gameObject.transform.localPosition = new Vector3(15f, (float)(this.MsgList.Count - 1 - i) * 154f + 15f, 0f);
		}
	}

	// Token: 0x06002AB6 RID: 10934 RVA: 0x0017864C File Offset: 0x0017684C
	public void InitDelTipPos(GameObject obj)
	{
		int num = this.MsgList.LastIndexOf(obj);
		bool flag = num != -1;
		if (flag)
		{
			this.MsgList.RemoveAt(num);
		}
		for (int i = 0; i < this.MsgList.Count; i++)
		{
			GameObject gameObject = (GameObject)this.MsgList[i];
			gameObject.transform.localPosition = new Vector3(15f, (float)(this.MsgList.Count - 1 - i) * 154f + 15f, 0f);
		}
	}

	// Token: 0x0400243C RID: 9276
	public static SystemMsgTipCon inst;

	// Token: 0x0400243D RID: 9277
	private GameObject BaseTipObj;

	// Token: 0x0400243E RID: 9278
	private ArrayList MsgList = new ArrayList();

	// Token: 0x0200067B RID: 1659
	// (Invoke) Token: 0x0600393F RID: 14655
	public delegate void onOkClick(bool bl, int k, ArrayList szpram);
}
