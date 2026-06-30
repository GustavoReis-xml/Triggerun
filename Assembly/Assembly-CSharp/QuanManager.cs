using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class QuanManager
{
	// Token: 0x06002063 RID: 8291 RVA: 0x0012949C File Offset: 0x0012769C
	public static QuanManager GetInstance()
	{
		bool flag = QuanManager.instance == null;
		if (flag)
		{
			QuanManager.instance = new QuanManager();
		}
		return QuanManager.instance;
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x001294CB File Offset: 0x001276CB
	public void SetDuQuan(DuQuanbuff obj)
	{
		this.DuQuanEff = obj;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x001294D8 File Offset: 0x001276D8
	public DuQuanbuff GetDuQuan()
	{
		return this.DuQuanEff;
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x001294F0 File Offset: 0x001276F0
	public void ClearDtsQuanData()
	{
		bool flag = this.DuQuanEff != null;
		if (flag)
		{
			this.DuQuanEff.OnNetDestroy();
		}
	}

	// Token: 0x04001D83 RID: 7555
	private static QuanManager instance;

	// Token: 0x04001D84 RID: 7556
	private GameObject thisModel;

	// Token: 0x04001D85 RID: 7557
	public DuQuanbuff DuQuanEff;
}
