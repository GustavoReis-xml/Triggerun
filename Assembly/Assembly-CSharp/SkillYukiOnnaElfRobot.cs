using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class SkillYukiOnnaElfRobot : SkillBaseRobot
{
	// Token: 0x060028FB RID: 10491 RVA: 0x0016C834 File Offset: 0x0016AA34
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x0016C84D File Offset: 0x0016AA4D
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x0016C857 File Offset: 0x0016AA57
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}
