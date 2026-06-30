using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033B RID: 827
internal class SkillHuanYingTuXiIt : SkillBaseIt
{
	// Token: 0x060026FB RID: 9979 RVA: 0x00161277 File Offset: 0x0015F477
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x00161283 File Offset: 0x0015F483
	private IEnumerator DelayshakeCamera()
	{
		yield return new WaitForSeconds(1f);
		SC_shakeCamera.shakeCamera();
		yield break;
	}
}
