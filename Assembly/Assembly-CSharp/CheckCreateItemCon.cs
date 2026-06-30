using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class CheckCreateItemCon : MonoBehaviour
{
	// Token: 0x060004DB RID: 1243 RVA: 0x00027EE8 File Offset: 0x000260E8
	private void Update()
	{
		bool flag = Input.GetMouseButton(1) && this.bMouseDown;
		if (flag)
		{
			this.oDtsUIObj.OnDelPos(this.m_nItemType, this.m_spos);
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00027F34 File Offset: 0x00026134
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = collision.transform.tag.Contains("can_damage_") && !(collision != fGameManager.Heroplayer.defaultCollider);
		if (flag)
		{
			NetLiving heroplayer = fGameManager.Heroplayer;
			bool flag2 = heroplayer != null && !heroplayer.isDead && !this.bMouseDown;
			if (flag2)
			{
				this.bMouseDown = true;
			}
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00027FA0 File Offset: 0x000261A0
	private void OnTriggerExit(Collider collision)
	{
		bool flag = collision.transform.tag.Contains("can_damage_") && !(collision != fGameManager.Heroplayer.defaultCollider);
		if (flag)
		{
			NetLiving heroplayer = fGameManager.Heroplayer;
			bool flag2 = heroplayer != null && !heroplayer.isDead && this.bMouseDown;
			if (flag2)
			{
				this.bMouseDown = false;
			}
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00028008 File Offset: 0x00026208
	public void SetValue(int nType, string sName, string spos, DtsUI dtsui)
	{
		this.m_nItemType = nType;
		this.m_sItemName = sName;
		this.m_spos = spos;
		this.oDtsUIObj = dtsui;
	}

	// Token: 0x040003B0 RID: 944
	public int m_nID;

	// Token: 0x040003B1 RID: 945
	private int m_nItemType;

	// Token: 0x040003B2 RID: 946
	private string m_sItemName;

	// Token: 0x040003B3 RID: 947
	private string m_spos;

	// Token: 0x040003B4 RID: 948
	private bool bMouseDown;

	// Token: 0x040003B5 RID: 949
	private DtsUI oDtsUIObj;
}
