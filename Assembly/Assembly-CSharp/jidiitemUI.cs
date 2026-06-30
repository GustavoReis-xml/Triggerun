using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class jidiitemUI : MonoBehaviour
{
	// Token: 0x06001665 RID: 5733 RVA: 0x000CFBB8 File Offset: 0x000CDDB8
	public void setData(buffitem buffitem, BoxCollider boxCol, SphereCollider spherecol)
	{
		this.pBuffitem = buffitem;
		this.bBoxCollider = boxCol;
		this.bSphereCollider = spherecol;
		bool flag = fGameManager.mloadConfig.AllMapEffectObj.ContainsKey(buffitem.buffinfo.m_nMapEffectId);
		if (flag)
		{
			MapEffectData mapEffectData = fGameManager.mloadConfig.AllMapEffectObj[buffitem.buffinfo.m_nMapEffectId];
			bool flag2 = mapEffectData != null;
			if (flag2)
			{
				this.szJieDuanData = mapEffectData.szJieDuan;
			}
		}
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000CFC2C File Offset: 0x000CDE2C
	public bool GetJiDiJieDuanStat()
	{
		bool result = false;
		bool flag = this.szJieDuanData.Count > 0;
		if (flag)
		{
			int nowGameState = fUIManager.GetInstance().GetNowGameState();
			bool flag2 = this.szJieDuanData.IndexOf(-1) == -1 && this.szJieDuanData.IndexOf(nowGameState) == -1;
			if (flag2)
			{
				result = true;
				bool flag3 = this.bBoxCollider != null;
				if (flag3)
				{
					this.bBoxCollider.enabled = false;
				}
				bool flag4 = this.bSphereCollider != null;
				if (flag4)
				{
					this.bSphereCollider.enabled = false;
				}
			}
		}
		return result;
	}

	// Token: 0x0400116C RID: 4460
	public buffitem pBuffitem;

	// Token: 0x0400116D RID: 4461
	public ArrayList szJieDuanData = new ArrayList();

	// Token: 0x0400116E RID: 4462
	private BoxCollider bBoxCollider;

	// Token: 0x0400116F RID: 4463
	private SphereCollider bSphereCollider;
}
