using System;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class TBAnimatorParmChange : StateMachineBehaviour
{
	// Token: 0x06002AFC RID: 11004 RVA: 0x0017A948 File Offset: 0x00178B48
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		for (int i = 0; i < this.BoolEnter.Length; i++)
		{
			animator.SetBool(this.BoolEnter[i].name, this.BoolEnter[i].data);
		}
		for (int j = 0; j < this.FloatEnter.Length; j++)
		{
			animator.SetFloat(this.FloatEnter[j].name, this.FloatEnter[j].data);
		}
		for (int k = 0; k < this.IntEnter.Length; k++)
		{
			animator.SetInteger(this.IntEnter[k].name, this.IntEnter[k].data);
		}
	}

	// Token: 0x06002AFD RID: 11005 RVA: 0x0017AA24 File Offset: 0x00178C24
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		for (int i = 0; i < this.BoolExit.Length; i++)
		{
			animator.SetBool(this.BoolExit[i].name, this.BoolExit[i].data);
		}
		for (int j = 0; j < this.FloatExit.Length; j++)
		{
			animator.SetFloat(this.FloatExit[j].name, this.FloatExit[j].data);
		}
		for (int k = 0; k < this.IntExit.Length; k++)
		{
			animator.SetInteger(this.IntExit[k].name, this.IntExit[k].data);
		}
	}

	// Token: 0x040024A9 RID: 9385
	public BoolData[] BoolExit;

	// Token: 0x040024AA RID: 9386
	public FloatData[] FloatExit;

	// Token: 0x040024AB RID: 9387
	public IntData[] IntExit;

	// Token: 0x040024AC RID: 9388
	public BoolData[] BoolEnter;

	// Token: 0x040024AD RID: 9389
	public FloatData[] FloatEnter;

	// Token: 0x040024AE RID: 9390
	public IntData[] IntEnter;
}
