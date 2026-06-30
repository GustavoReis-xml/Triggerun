using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
public class WenJuanManager
{
	// Token: 0x06002DD1 RID: 11729 RVA: 0x00194CE8 File Offset: 0x00192EE8
	public static WenJuanManager GetInstance()
	{
		bool flag = WenJuanManager.instance == null;
		if (flag)
		{
			WenJuanManager.instance = new WenJuanManager();
		}
		return WenJuanManager.instance;
	}

	// Token: 0x06002DD2 RID: 11730 RVA: 0x00194D18 File Offset: 0x00192F18
	public void WenJuanServerMsgToClient(int nNetId, ArrayList szMsg)
	{
		switch (nNetId)
		{
		case 14002:
			this.UpdataWenJuanData(szMsg);
			return;
		case 14004:
			this.OnLoadsetWenJuanData(szMsg);
			return;
		case 14005:
			this.SentSeverMsg(szMsg);
			return;
		}
		Debug.Log("WenJuanServerMsgToClient----" + nNetId.ToString());
	}

	// Token: 0x06002DD3 RID: 11731 RVA: 0x00194D80 File Offset: 0x00192F80
	public void OnLoadsetWenJuanData(ArrayList szData)
	{
		bool flag = szData.Count >= 1;
		if (flag)
		{
			this.m_szWenJuanEventList = (ArrayList)szData[0];
			this.m_szWenJuanData = (ArrayList)szData[1];
			this.CheckNowWenJuanData();
		}
	}

	// Token: 0x06002DD4 RID: 11732 RVA: 0x00194DCC File Offset: 0x00192FCC
	private void CheckNowWenJuanData()
	{
		int num = 0;
		int num2 = this.getNotRewardIndex();
		bool flag = num2 == 0 && this.m_szWenJuanEventList.Count > 0;
		if (flag)
		{
			this.m_szWenJuanEventList.Sort();
			num2 = (int)this.m_szWenJuanEventList[0];
			num = 1;
		}
		this.m_szNowOpenWenJuan = new ArrayList();
		bool flag2 = num2 != 0;
		if (flag2)
		{
			this.m_szNowOpenWenJuan = new ArrayList
			{
				num2,
				num
			};
			this.ButShow = true;
		}
		else
		{
			this.ButShow = false;
		}
		bool flag3 = MainUICon.inst != null;
		if (flag3)
		{
			MainUICon.inst.WenJuanBtnActive();
		}
	}

	// Token: 0x06002DD5 RID: 11733 RVA: 0x00194E8C File Offset: 0x0019308C
	private int getNotRewardIndex()
	{
		int result = 0;
		int count = this.m_szWenJuanData.Count;
		for (int i = 0; i < count; i += 2)
		{
			bool flag = (int)this.m_szWenJuanData[i + 1] == 0;
			if (flag)
			{
				result = (int)this.m_szWenJuanData[i];
				break;
			}
		}
		return result;
	}

	// Token: 0x06002DD6 RID: 11734 RVA: 0x00194EF4 File Offset: 0x001930F4
	public void UpdataWenJuanData(ArrayList szData)
	{
		bool flag = szData.Count >= 1;
		if (flag)
		{
			this.m_szWenJuanData = szData;
			this.CheckNowWenJuanData();
		}
	}

	// Token: 0x06002DD7 RID: 11735 RVA: 0x00194F24 File Offset: 0x00193124
	private void SentSeverMsg(ArrayList szMsg)
	{
		bool flag = WenJuanUICon.inst != null;
		if (flag)
		{
			WenJuanUICon.inst.SentSeverMsg(szMsg);
		}
	}

	// Token: 0x04002726 RID: 10022
	private static WenJuanManager instance;

	// Token: 0x04002727 RID: 10023
	public ArrayList m_szWenJuanEventList = new ArrayList();

	// Token: 0x04002728 RID: 10024
	public ArrayList m_szWenJuanData = new ArrayList();

	// Token: 0x04002729 RID: 10025
	public ArrayList m_szNowOpenWenJuan = new ArrayList();

	// Token: 0x0400272A RID: 10026
	public bool ButShow;
}
