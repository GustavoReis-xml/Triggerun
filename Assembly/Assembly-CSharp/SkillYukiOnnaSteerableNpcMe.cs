using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class SkillYukiOnnaSteerableNpcMe : SkillBaseMe
{
	// Token: 0x06002902 RID: 10498 RVA: 0x0016C88E File Offset: 0x0016AA8E
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.wInput.notInput = true;
		base.OneSkillAction(target, parm2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x0016C8B9 File Offset: 0x0016AAB9
	public override void OneSkillFinish()
	{
		base.hero.wInput.notInput = false;
		base.OneSkillFinish();
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x0016C8D5 File Offset: 0x0016AAD5
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(0.6f);
		this.OneSkillFinish();
		yield break;
	}
}
