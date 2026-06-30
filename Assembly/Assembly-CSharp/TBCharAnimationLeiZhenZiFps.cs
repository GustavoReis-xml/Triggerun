using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class TBCharAnimationLeiZhenZiFps : TBCharAnimationPlay
{
	// Token: 0x06002B93 RID: 11155 RVA: 0x001803C0 File Offset: 0x0017E5C0
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
		}
	}
}
