using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class HeroDaJiIt : HeroBaseIt
{
	// Token: 0x060010ED RID: 4333 RVA: 0x0009D428 File Offset: 0x0009B628
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		((IHeroDataDaJi)this.IHD).initSound(Fun.FindGameObjectChild(base.hero.UserControl, "dun", false), base.hero);
		bool flag = base.hero.skillEffectDict.ContainsKey(1151);
		if (flag)
		{
			((SkillDaJiHudunIt)base.hero.skillEffectDict[1151]).Update_Shield_Data(base.hero.playerCommon.aryHeroSpecialData);
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0009D4B5 File Offset: 0x0009B6B5
	public override void SuperSkillDo(object parm)
	{
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0009D4B8 File Offset: 0x0009B6B8
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0009D4C4 File Offset: 0x0009B6C4
	public override void bulletBoom(int _weaponName, int _weaponNo, Vector3 pos)
	{
		base.bulletBoom(_weaponName, _weaponNo, pos);
		bool flag = _weaponName == 2;
		if (flag)
		{
			Vector3 position = Fun.FindGameObjectChild(base.hero.UserControl, "m2", false).transform.position;
			bool flag2 = base.hero.skillEffectDict.ContainsKey(1151);
			if (flag2)
			{
				((SkillDaJiHudunIt)base.hero.skillEffectDict[1151]).dunHaveCreate = false;
				((SkillDaJiHudunIt)base.hero.skillEffectDict[1151]).createShileObject(position, pos);
			}
		}
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0009D567 File Offset: 0x0009B767
	protected override void Update()
	{
		base.Update();
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0009D574 File Offset: 0x0009B774
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = base.hero.skillEffectDict.ContainsKey(1151);
		if (flag)
		{
			((SkillDaJiHudunIt)base.hero.skillEffectDict[1151]).SetDunState();
		}
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x0009D5C4 File Offset: 0x0009B7C4
	public override GameObject GetShield(string shieldName)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1151);
		GameObject result;
		if (flag)
		{
			result = ((SkillDaJiHudunIt)base.hero.skillEffectDict[1151]).getShieldCol();
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x04000D75 RID: 3445
	private Animator m_anim;
}
