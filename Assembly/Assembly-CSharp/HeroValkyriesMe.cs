using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class HeroValkyriesMe : HeroBaseMe
{
	// Token: 0x06001481 RID: 5249 RVA: 0x000C109C File Offset: 0x000BF29C
	public override void SuperSkillDo(object parm)
	{
		bool flag = !this.IsSkillSuperJump;
		if (flag)
		{
			base.SuperSkillDo(parm);
			this.IsSkillSuperJump = true;
			base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
			base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
			base.hero.SetAnimator("SkillSuper", 0, true, 2);
			((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
			base.hero.wInput.notInput = true;
			base.hero.tChar.m_MoveByAnimator = false;
			base.hero.playerCommon.m_fMoveSpeedAnimation = 9.5f;
			base.hero.ChangeSpeed();
			base.hero.bePlayActive = false;
		}
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x000C117B File Offset: 0x000BF37B
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000C1190 File Offset: 0x000BF390
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput && !this.IsSkillSuperJump && !Input_Manager.Get_Button("Skill2");
		if (flag)
		{
			base.hero.superSkill(null);
		}
		bool flag2 = this.superDoing;
		if (flag2)
		{
			bool flag3 = base.hero.playerDazhao.UseGun(base.hero) && base.hero.playerCommon.m_nDongjie == 0 && base.hero.playerCommon.m_nHunMi == 0;
			if (flag3)
			{
				base.hero.playerDazhao.doShoot(base.hero);
			}
			else
			{
				this.SuperSkillFinish();
			}
		}
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000C126A File Offset: 0x000BF46A
	private IEnumerator Wait_Action()
	{
		yield return new WaitForSeconds(0.3f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x000C1279 File Offset: 0x000BF479
	public override void SuperSkillFinish()
	{
		this.SuperSkillEnd();
		this.IsSkillSuperJump = false;
		base.SuperSkillFinish();
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x000C1294 File Offset: 0x000BF494
	private void SuperSkillEnd()
	{
		this.superDoing = false;
		base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		bool flag = !base.hero.isDead;
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, "Valkyries_super");
		}
		base.hero.wInput.notInput = false;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.bePlayActive = true;
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000C1330 File Offset: 0x000BF530
	public override void BreakSupperSkill()
	{
		bool flag = this.superDoing;
		if (flag)
		{
			base.BreakSupperSkill();
		}
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x000C1351 File Offset: 0x000BF551
	public override void Freeze(bool mbool)
	{
		base.Freeze(mbool);
		base.hero.SetAnimator("ShopEffect", 0, mbool, 1);
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000C1375 File Offset: 0x000BF575
	public override void Stun(bool mbool)
	{
		base.Stun(mbool);
		base.hero.SetAnimator("ShopEffect", 0, mbool, 1);
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000C1399 File Offset: 0x000BF599
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, false, 1);
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x000C13BC File Offset: 0x000BF5BC
	public override void OnDeath()
	{
		base.OnDeath();
		this.IgnoreLayer(false);
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x000C13CE File Offset: 0x000BF5CE
	public override void ChanngeLayerCollider(bool revive)
	{
		base.ChanngeLayerCollider(false);
		this.IgnoreLayer(false);
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x000C13E1 File Offset: 0x000BF5E1
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		this.IgnoreLayer(false);
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000C13F4 File Offset: 0x000BF5F4
	public void IgnoreLayer(bool manLayer)
	{
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force1_mainplayer"), LayerMask.NameToLayer("force2_player"), manLayer);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force1_mainplayer"), LayerMask.NameToLayer("force2_npc"), manLayer);
		}
		bool flag2 = base.hero.playerCommon.m_nShili == 2;
		if (flag2)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force2_mainplayer"), LayerMask.NameToLayer("force1_player"), manLayer);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force2_mainplayer"), LayerMask.NameToLayer("force1_npc"), manLayer);
		}
	}

	// Token: 0x04000FE0 RID: 4064
	public bool superDoing;

	// Token: 0x04000FE1 RID: 4065
	public bool IsSkillSuperJump;
}
