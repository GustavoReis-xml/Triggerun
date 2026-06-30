using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class TBCharAnimationGuanyinTps : TBCharAnimationThirdIK
{
	// Token: 0x06002B6E RID: 11118 RVA: 0x0017EC9C File Offset: 0x0017CE9C
	protected override void Update()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			base.Update();
			bool flag2 = !this.isAim;
			if (flag2)
			{
				this.isAim = true;
				this.animator.SetBool("Aim", base.hero.playerCommon.m_nWeaponGroup == 0);
				this.animator.SetBool("Aim2", base.hero.playerCommon.m_nWeaponGroup == 1);
				this.animator.SetInteger("Weapon", base.hero.playerCommon.m_nWeaponGroup);
			}
		}
	}

	// Token: 0x04002543 RID: 9539
	private bool isAim;
}
