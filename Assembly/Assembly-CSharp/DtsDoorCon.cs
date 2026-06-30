using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class DtsDoorCon : MonoBehaviour
{
	// Token: 0x06000925 RID: 2341 RVA: 0x0004DA44 File Offset: 0x0004BC44
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = collision.transform.tag.Contains("can_damage_") && !(collision != fGameManager.Heroplayer.defaultCollider);
		if (flag)
		{
			NetLiving heroplayer = fGameManager.Heroplayer;
			bool flag2 = heroplayer != null && !heroplayer.isDead && (Fun.GetServerDate() - heroplayer.FlyShileldBuffTime).TotalSeconds >= 1.0;
			if (flag2)
			{
				heroplayer.FlyShileldBuffTime = Fun.GetServerDate();
				ArrayList arrayList = new ArrayList();
				arrayList.Add(19601);
				arrayList.Add(this.m_nID);
				DtsBatteManager.GetInstance().OnSendMsgToServer(19602, arrayList);
			}
		}
	}

	// Token: 0x0400069B RID: 1691
	public int m_nID;
}
