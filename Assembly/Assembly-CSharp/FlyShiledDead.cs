using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class FlyShiledDead : MonoBehaviour
{
	// Token: 0x06000B3F RID: 2879 RVA: 0x00068D02 File Offset: 0x00066F02
	public void SetData(ISkillDataSkill_Lilisi_shiled isShiled, int nId, int nCreatID, FlyShiledSkill flyskill)
	{
		this.ISkillData = isShiled;
		this.m_nID = nId;
		this.m_nCreartID = nCreatID;
		this.oFlyShiled = flyskill;
		this.bCheck = true;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00068D2C File Offset: 0x00066F2C
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = this.bCheck && this.ISkillData != null && !(this.oFlyShiled == null) && (collision.gameObject.layer == 0 || collision.gameObject.layer == LayerMask.NameToLayer("Door_1") || collision.gameObject.layer == LayerMask.NameToLayer("Door_2"));
		if (flag)
		{
			ArrayList szData = new ArrayList
			{
				-1,
				this.m_nID,
				this.m_nCreartID
			};
			this.oFlyShiled.Send_Messages(szData);
			this.bCheck = false;
		}
	}

	// Token: 0x04000984 RID: 2436
	private ISkillDataSkill_Lilisi_shiled ISkillData;

	// Token: 0x04000985 RID: 2437
	private FlyShiledSkill oFlyShiled;

	// Token: 0x04000986 RID: 2438
	private int m_nID;

	// Token: 0x04000987 RID: 2439
	private int m_nCreartID;

	// Token: 0x04000988 RID: 2440
	public bool bCheck;
}
