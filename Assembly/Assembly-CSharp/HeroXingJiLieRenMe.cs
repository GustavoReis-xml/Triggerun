using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class HeroXingJiLieRenMe : HeroBaseMe
{
	// Token: 0x060014DD RID: 5341 RVA: 0x000C62C9 File Offset: 0x000C44C9
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.onwall = false;
		this.checkNeedPlayClimAnim = false;
		this.ReleaseSuper = false;
		this.tpsCamPos = base.hero.heroData.fTPSCameraDis;
		this.xingjisushe = false;
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000C6308 File Offset: 0x000C4508
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.heroData.fTPSCameraDis = this.tpsCamPos + 1f;
		((TBCharacterThirdMe)base.hero.tChar).canWall = false;
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.SuperSkillDoCamera), parm);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
		base.hero.ChangeSpeed();
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000C63F3 File Offset: 0x000C45F3
	private void SuperSkillDoCamera(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000C6405 File Offset: 0x000C4605
	private IEnumerator DelayReleaseSuper()
	{
		yield return new WaitForSeconds(0.5f);
		bool flag = !base.hero.isDead;
		if (flag)
		{
			base.hero.tChar.m_MoveByAnimator = false;
			base.hero.playerCommon.m_fMoveSpeedAnimation = 5f;
			base.hero.ChangeSpeed();
		}
		yield break;
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000C6414 File Offset: 0x000C4614
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(2.7f);
		base.hero.bePlayActive = true;
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000C6424 File Offset: 0x000C4624
	public override void SuperSkillFinish()
	{
		base.hero.heroData.fTPSCameraDis = this.tpsCamPos;
		((TBCharacterThirdMe)base.hero.tChar).canWall = true;
		this.ReleaseSuper = false;
		base.SuperSkillFinish();
		base.hero.bePlayActive = true;
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000C647C File Offset: 0x000C467C
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		base.hero.heroData.fTPSCameraDis = this.tpsCamPos;
		base.hero.heroBase.B_Transforing = false;
		base.hero.tChar.m_MoveByAnimator = true;
		bool releaseSuper = this.ReleaseSuper;
		if (releaseSuper)
		{
			this.SuperSkillFinish();
		}
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000C6518 File Offset: 0x000C4718
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000C653C File Offset: 0x000C473C
	public override void Freeze(bool m_bool)
	{
		bool flag = !m_bool;
		if (flag)
		{
			base.hero.SetAnimator("FreezeBreak", 0, true, 1);
			base.hero.SetAnimator("SkillSuper", 0, false, 2);
			base.hero.SetAnimator("SkillLianShe", 0, false, 2);
			base.hero.heroData.fTPSCameraDis = this.tpsCamPos;
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000C65B8 File Offset: 0x000C47B8
	public override void Stun(bool m_bool)
	{
		bool flag = !m_bool;
		if (flag)
		{
			base.hero.SetAnimator("FreezeBreak", 0, true, 1);
			base.hero.SetAnimator("SkillSuper", 0, false, 2);
			base.hero.SetAnimator("SkillLianShe", 0, false, 2);
			base.hero.heroData.fTPSCameraDis = this.tpsCamPos;
		}
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000C6634 File Offset: 0x000C4834
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck();
		if (flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput && !Input_Manager.Get_Button("Skill2");
			if (flag2)
			{
				base.hero.superSkill(null);
			}
			bool flag3 = base.hero.charMove.state.onwall == 1 && !this.checkNeedPlayClimAnim;
			if (flag3)
			{
				this.checkNeedPlayClimAnim = true;
				base.StartCoroutine(this.checkNeedPlayClimbAnim());
			}
			else
			{
				bool flag4 = base.hero.tChar.wallState != TBCharacter.WallState.OnWall && this.onwall && !this.playPaqiang;
				if (flag4)
				{
					this.StopClimbAnim();
				}
				else
				{
					bool flag5 = base.hero.charMove.state.onwall == 3 && this.onwall && this.playPaqiang;
					if (flag5)
					{
					}
				}
			}
		}
		bool flag6 = !this.superDoing;
		if (!flag6)
		{
			bool flag7 = base.hero.playerDazhao.UseGun(base.hero);
			if (flag7)
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
				bool flag8 = !base.hero.isDead;
				if (flag8)
				{
					base.hero.SetAnimator("SkillSuper", 0, false, 2);
					base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
					this.SuperSkillFinish();
				}
			}
		}
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000C6831 File Offset: 0x000C4A31
	private IEnumerator checkNeedPlayClimbAnim()
	{
		yield return new WaitForSeconds(0.05f);
		bool flag = base.hero.charMove.state.onwall == 1;
		if (flag)
		{
			CameraControl.Instance.SetPaQiangCaMera();
			this.playClimbAnim();
		}
		else
		{
			bool flag2 = base.hero.charMove.state.onwall != 1;
			if (flag2)
			{
				this.checkNeedPlayClimAnim = false;
			}
		}
		yield break;
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000C6840 File Offset: 0x000C4A40
	private void playClimbAnim()
	{
		base.hero.wInput.notInput = true;
		this.onwall = true;
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000C685C File Offset: 0x000C4A5C
	private void StopClimbAnim()
	{
		base.StopCoroutine(this.checkNeedPlayClimbAnim());
		this.onwall = false;
		base.hero.wInput.notInput = false;
		this.checkNeedPlayClimAnim = false;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000C688C File Offset: 0x000C4A8C
	private void PlayPaQiangEnd()
	{
		base.hero.SetAnimator("PaQiangEnd", 0, true, 2);
		this.checkNeedPlayClimAnim = false;
		this.playPaqiang = true;
		base.StartCoroutine(this.stopPaqiangEnd());
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000C68C3 File Offset: 0x000C4AC3
	private IEnumerator stopPaqiangEnd()
	{
		yield return new WaitForSeconds(0.2f);
		base.hero.wInput.notInput = false;
		base.hero.SetAnimator("PaQiangEnd", 0, false, 2);
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		this.onwall = false;
		this.playPaqiang = false;
		yield break;
	}

	// Token: 0x04001034 RID: 4148
	private bool onwall;

	// Token: 0x04001035 RID: 4149
	private bool checkNeedPlayClimAnim;

	// Token: 0x04001036 RID: 4150
	private bool ReleaseSuper;

	// Token: 0x04001037 RID: 4151
	private bool superDoing;

	// Token: 0x04001038 RID: 4152
	private float tpsCamPos;

	// Token: 0x04001039 RID: 4153
	public bool xingjisushe;

	// Token: 0x0400103A RID: 4154
	private bool playPaqiang;
}
