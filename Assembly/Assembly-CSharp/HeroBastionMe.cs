using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class HeroBastionMe : HeroBaseMe
{
	// Token: 0x060010E3 RID: 4323 RVA: 0x0009D208 File Offset: 0x0009B408
	private void Start()
	{
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0009D20C File Offset: 0x0009B40C
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill");
		if (flag)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0009D249 File Offset: 0x0009B449
	private IEnumerator SetMoveState()
	{
		base.hero.playerRuntimeData.NoInputMove = true;
		base.hero.wInput.notInput = true;
		yield return new WaitForSeconds(1.2f);
		base.hero.ChangeWeaponGroup(2);
		base.hero.playerRuntimeData.NoInputMove = false;
		base.hero.wInput.notInput = false;
		base.hero.playerRuntimeData.MoveState = 2;
		base.hero.playerRuntimeData.Deformation_State = true;
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		base.hero.playerRuntimeData.NoInputMove = true;
		base.hero.wInput.notInput = true;
		this.SuperSkillFinish();
		yield return new WaitForSeconds(1.2f);
		base.hero.ChangeWeaponGroup(0);
		base.hero.playerRuntimeData.NoInputMove = false;
		base.hero.wInput.notInput = false;
		base.hero.playerRuntimeData.MoveState = 0;
		base.hero.playerRuntimeData.Deformation_State = false;
		yield break;
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0009D258 File Offset: 0x0009B458
	public override void SuperSkillDo(object parm)
	{
		bool flag = base.hero.playerRuntimeData.MoveState != 2;
		if (flag)
		{
			base.SuperSkillDo(parm);
			base.StartCoroutine(this.SetMoveState());
		}
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0009D298 File Offset: 0x0009B498
	public override void OnDeath()
	{
		SkillShaoweiMe component = base.gameObject.GetComponent<SkillShaoweiMe>();
		component.SkillEnd();
	}
}
