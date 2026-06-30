using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class SkillYukiOnnaTrapRobot : SkillBaseRobot
{
	// Token: 0x0600290D RID: 10509 RVA: 0x0016C947 File Offset: 0x0016AB47
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x0600290E RID: 10510 RVA: 0x0016C960 File Offset: 0x0016AB60
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x0600290F RID: 10511 RVA: 0x0016C96A File Offset: 0x0016AB6A
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}
