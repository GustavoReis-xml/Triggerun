using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class HeroDaJiMe : HeroBaseMe
{
	// Token: 0x060010F5 RID: 4341 RVA: 0x0009D61C File Offset: 0x0009B81C
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0009D628 File Offset: 0x0009B828
	public override void BreakSth(int type)
	{
		base.BreakSth(type);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0009D633 File Offset: 0x0009B833
	public override void RoundBegin(int type)
	{
		base.RoundBegin(type);
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0009D640 File Offset: 0x0009B840
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.m2 = Fun.FindGameObjectChild(base.hero.UserControl, "m2", false);
		((IHeroDataDaJi)this.IHD).initSound(Fun.FindGameObjectChild(base.hero.UserControl, "dun", false), base.hero);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x0009D6A0 File Offset: 0x0009B8A0
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.hero.playerDazhao.doShoot(base.hero);
		base.StartCoroutine(this.SuperSkillAnimationFinish());
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x0009D705 File Offset: 0x0009B905
	private IEnumerator SuperSkillAnimationFinish()
	{
		yield return new WaitForSeconds(1f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0009D714 File Offset: 0x0009B914
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		bool flag = base.hero.playerCommon.m_nHunMi == 0 && base.hero.playerCommon.m_nDongjie == 0;
		if (flag)
		{
			base.hero.bePlayActive = true;
			base.hero.wInput.notInput = false;
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0009D776 File Offset: 0x0009B976
	public override void OnDeath()
	{
		base.OnDeath();
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0009D780 File Offset: 0x0009B980
	public override void Revive()
	{
		base.Revive();
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0009D78C File Offset: 0x0009B98C
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0009D7DE File Offset: 0x0009B9DE
	public override void Stun(bool m_bool)
	{
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0009D7E4 File Offset: 0x0009B9E4
	public override void bulletBoom(int _weaponName, int _weaponNo, Vector3 pos)
	{
		base.bulletBoom(_weaponName, _weaponNo, pos);
		bool flag = _weaponName == 2;
		if (flag)
		{
			Vector3 position = this.m2.transform.position;
			bool flag2 = base.hero.skillEffectDict.ContainsKey(1151);
			if (flag2)
			{
				((SkillDaJiHudunMe)base.hero.skillEffectDict[1151]).createShileObject(position, pos);
			}
		}
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0009D854 File Offset: 0x0009BA54
	public override GameObject GetShield(string shieldName)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1151);
		GameObject result;
		if (flag)
		{
			result = ((SkillDaJiHudunMe)base.hero.skillEffectDict[1151]).getShieldCol();
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0009D8A4 File Offset: 0x0009BAA4
	public override void RoundBegin()
	{
		base.RoundBegin();
		bool flag = base.hero.skillEffectDict.ContainsKey(1151);
		if (flag)
		{
			((SkillDaJiHudunMe)base.hero.skillEffectDict[1151]).SetDunState();
		}
	}

	// Token: 0x04000D76 RID: 3446
	private Transform m2;
}
