using System;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class TBCharAnimationGuanyinFps : TBCharAnimationPlay
{
	// Token: 0x06002B6B RID: 11115 RVA: 0x0017EB3C File Offset: 0x0017CD3C
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

	// Token: 0x06002B6C RID: 11116 RVA: 0x0017EBF8 File Offset: 0x0017CDF8
	public void OnEnable()
	{
		bool flag = Time.deltaTime != 0f && !(this.characterController == null);
		if (flag)
		{
			this.animator.SetBool("Aim", base.hero.playerCommon.m_nWeaponGroup == 0);
			this.animator.SetBool("Aim2", base.hero.playerCommon.m_nWeaponGroup == 1);
			this.animator.SetInteger("Weapon", base.hero.playerCommon.m_nWeaponGroup);
		}
	}

	// Token: 0x04002542 RID: 9538
	private bool isAim;
}
