using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class HeroMedusaMe : HeroBaseMe
{
	// Token: 0x06001380 RID: 4992 RVA: 0x000B9AC0 File Offset: 0x000B7CC0
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.ReleaseSuper = false;
		this.fpsCamPos = base.hero.heroData.fTPSCameraDis;
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000B9AEC File Offset: 0x000B7CEC
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.heroData.fTPSCameraDis = this.fpsCamPos + 1f;
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("skill_Q", 0, true, 2);
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
		base.hero.ChangeSpeed();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000B9BAB File Offset: 0x000B7DAB
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000B9BC0 File Offset: 0x000B7DC0
	public override void SuperSkillFinish()
	{
		((SkillPoisonMe)base.hero.skillEffectDict[1193]).OneSkillFinish();
		base.hero.heroData.fTPSCameraDis = this.fpsCamPos;
		base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		base.hero.playerDazhao.doing = false;
		base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		this.superDoing = false;
		this.ReleaseSuper = false;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.wInput.notInput = false;
		base.hero.bePlayActive = true;
		base.SuperSkillFinish();
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000B9C84 File Offset: 0x000B7E84
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("skill_Q", 0, false, 2);
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		base.hero.heroData.fTPSCameraDis = this.fpsCamPos;
		base.hero.heroBase.B_Transforing = false;
		base.hero.tChar.m_MoveByAnimator = true;
		bool releaseSuper = this.ReleaseSuper;
		if (releaseSuper)
		{
			this.SuperSkillFinish();
		}
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000B9D20 File Offset: 0x000B7F20
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.SetAnimator("skill_Q", 0, false, 2);
		this.IgnoreLayer(false);
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000B9D4C File Offset: 0x000B7F4C
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
		bool flag = !m_bool;
		if (flag)
		{
			base.hero.SetAnimator("skill_Q", 0, false, 2);
			this.SuperSkillFinish();
		}
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000B9D8C File Offset: 0x000B7F8C
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		bool flag = !m_bool;
		if (flag)
		{
			base.hero.SetAnimator("skill_Q", 0, false, 2);
			this.SuperSkillFinish();
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x000B9DCC File Offset: 0x000B7FCC
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill");
		if (flag)
		{
			base.hero.superSkill(null);
		}
		bool flag2 = !this.superDoing;
		if (!flag2)
		{
			bool flag3 = base.hero.playerDazhao.UseGun(base.hero);
			if (flag3)
			{
				base.hero.playerDazhao.doShoot(base.hero);
				base.hero.bePlayActive = false;
			}
			else
			{
				this.superDoing = false;
				base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
				base.hero.tChar.m_MoveByAnimator = true;
				base.hero.wInput.notInput = false;
				base.hero.bePlayActive = true;
				bool flag4 = !base.hero.isDead;
				if (flag4)
				{
					base.hero.SetAnimator("skill_Q", 0, false, 2);
					base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
					this.SuperSkillFinish();
				}
			}
		}
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000B9EF4 File Offset: 0x000B80F4
	public override void BreakSupperSkill()
	{
		bool flag = this.superDoing;
		if (flag)
		{
			base.BreakSupperSkill();
		}
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000B9F18 File Offset: 0x000B8118
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

	// Token: 0x0600138B RID: 5003 RVA: 0x000B9FC4 File Offset: 0x000B81C4
	public override void ChanngeLayerCollider(bool revive)
	{
		base.ChanngeLayerCollider(false);
		this.IgnoreLayer(false);
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000B9FD7 File Offset: 0x000B81D7
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		this.IgnoreLayer(false);
	}

	// Token: 0x04000F25 RID: 3877
	private bool ReleaseSuper;

	// Token: 0x04000F26 RID: 3878
	public bool superDoing;

	// Token: 0x04000F27 RID: 3879
	private float fpsCamPos;
}
