using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class SkillYukiOnnaTrapMe : SkillBaseMe
{
	// Token: 0x06002909 RID: 10505 RVA: 0x0016C90C File Offset: 0x0016AB0C
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x0016C925 File Offset: 0x0016AB25
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x0016C92F File Offset: 0x0016AB2F
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}
