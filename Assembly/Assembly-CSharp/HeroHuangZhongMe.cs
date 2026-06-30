using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class HeroHuangZhongMe : HeroBaseMe
{
	// Token: 0x0600120A RID: 4618 RVA: 0x000A97DC File Offset: 0x000A79DC
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "TargetCenterOne");
		base.hero.screentarget.targetAngle = 0.99f;
		base.hero.screentarget.targetDis = 40f;
		((IHeroDataHuangZhong)this.IHD).HD_Power = 0;
		this.reduceGap = 0.5f;
		this.bAutoBreakCartridge = false;
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000A9854 File Offset: 0x000A7A54
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		Transform transform = base.hero.userModelTps.transform.FindChild("buff_hz");
		int childCount = transform.childCount;
		this.PowerBuffs = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			this.PowerBuffs[i] = transform.GetChild(i).gameObject;
		}
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x000A98BD File Offset: 0x000A7ABD
	public override void BreakSth(int type)
	{
		base.BreakSth(type);
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x000A98C8 File Offset: 0x000A7AC8
	public override void RoundBegin(int type)
	{
		base.RoundBegin(type);
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000A98D3 File Offset: 0x000A7AD3
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		this.b_superState = true;
		base.StartCoroutine(this.SuperSkillAnimationFinish());
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000A990B File Offset: 0x000A7B0B
	private IEnumerator SuperSkillAnimationFinish()
	{
		yield return new WaitForSeconds(5f);
		this.b_superState = false;
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x000A991A File Offset: 0x000A7B1A
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		((SkillHuangZhongQMe)base.hero.skillEffectDict[1173]).OneSkillFinish();
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x000A9944 File Offset: 0x000A7B44
	public override void OnDeath()
	{
		base.OnDeath();
		((IHeroDataHuangZhong)this.IHD).HD_Power = 0;
		this.D_Power = 0;
		this.SetLevelColor();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x000A9970 File Offset: 0x000A7B70
	public override void Revive()
	{
		base.Revive();
		((IHeroDataHuangZhong)this.IHD).HD_Power = 0;
		this.D_Power = 0;
		this.SetLevelColor();
		base.hero.SetAnimator("weaponReady", 0, false, 1);
		this.weaponReadyToFire = true;
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x000A99C4 File Offset: 0x000A7BC4
	protected override void Update()
	{
		base.Update();
		this.reducePower();
		bool flag = this.hzAnim == null && base.hero.tCharAnimationTps != null;
		if (flag)
		{
			this.hzAnim = base.hero.tCharAnimationTps.animator;
		}
		bool flag2 = this.hzwc == null;
		if (flag2)
		{
			bool flag3 = base.hero.wc != null;
			if (flag3)
			{
				this.hzwc = base.hero.wc;
			}
		}
		else
		{
			bool flag4 = this.hzwc.fullIng && !this.weaponReadyToFire;
			if (flag4)
			{
				bool flag5 = !this.hzAnim.GetBool("Reload");
				if (flag5)
				{
					this.weaponReadyToFire = true;
					base.hero.SetAnimator("weaponReady", 0, true, 2);
					base.hero.SetAnimator("AttackIng", 0, false, 2);
				}
			}
			else
			{
				bool flag6 = !this.hzwc.fullIng && this.weaponReadyToFire;
				if (flag6)
				{
					this.weaponReadyToFire = false;
					base.hero.SetAnimator("weaponReady", 0, false, 2);
					base.hero.SetAnimator("AttackIng", 0, false, 2);
				}
			}
			bool flag7 = this.hzwc.FullRate(1) > 1f && this.hzwc.fullIng;
			if (flag7)
			{
				base.hero.playerCommon.m_fJumpSpeed = 0.5f;
				base.hero.ChangeSpeed();
				base.hero.tChar.jumpPowerForward = 3f;
			}
			else
			{
				bool flag8 = !this.hzwc.fullIng;
				if (flag8)
				{
					base.hero.playerCommon.m_fJumpSpeed = 1f;
					base.hero.ChangeSpeed();
					base.hero.tChar.jumpPowerForward = 5f;
				}
			}
		}
		bool flag9 = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag9)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x000A9C17 File Offset: 0x000A7E17
	public override void Stun(bool m_bool)
	{
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000A9C1C File Offset: 0x000A7E1C
	public override void beDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.beDamage(szDamage, szHp);
		ArrayList arrayList = new ArrayList();
		arrayList = (ArrayList)szDamage[15];
		bool flag = arrayList.Count > 0 && !base.hero.isDead;
		if (flag)
		{
			this.D_Power = (int)arrayList[1];
			((IHeroDataHuangZhong)this.IHD).HD_Power = this.D_Power;
			this.SetLevelColor();
		}
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000A9C98 File Offset: 0x000A7E98
	private void SetLevelColor()
	{
		bool flag = base.hero.haveDestroy || !base.hero.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = this.D_Power > 80;
			if (flag2)
			{
				bool flag3 = !this.PowerBuffs[3].activeSelf;
				if (flag3)
				{
					this.MakeAlllevelColorDisable();
					this.PowerBuffs[3].SetActive(true);
				}
			}
			else
			{
				bool flag4 = this.D_Power > 60;
				if (flag4)
				{
					bool flag5 = !this.PowerBuffs[2].activeSelf;
					if (flag5)
					{
						this.MakeAlllevelColorDisable();
						this.PowerBuffs[2].SetActive(true);
					}
				}
				else
				{
					bool flag6 = this.D_Power > 40;
					if (flag6)
					{
						bool flag7 = !this.PowerBuffs[1].activeSelf;
						if (flag7)
						{
							this.MakeAlllevelColorDisable();
							this.PowerBuffs[1].SetActive(true);
						}
					}
					else
					{
						bool flag8 = this.D_Power > 20;
						if (flag8)
						{
							bool flag9 = !this.PowerBuffs[0].activeSelf;
							if (flag9)
							{
								this.MakeAlllevelColorDisable();
								this.PowerBuffs[0].SetActive(true);
							}
						}
						else
						{
							this.MakeAlllevelColorDisable();
						}
					}
				}
			}
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000A9DE0 File Offset: 0x000A7FE0
	private void MakeAlllevelColorDisable()
	{
		for (int i = 0; i < this.PowerBuffs.Length; i++)
		{
			bool activeSelf = this.PowerBuffs[i].activeSelf;
			if (activeSelf)
			{
				this.PowerBuffs[i].SetActive(false);
			}
		}
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000A9E2C File Offset: 0x000A802C
	private void reducePower()
	{
		bool flag = this.b_superState;
		if (!flag)
		{
			bool flag2 = this.reduceGap > 0f;
			if (flag2)
			{
				this.reduceGap -= Time.deltaTime;
			}
			else
			{
				this.reduceGap = 0.5f;
				bool flag3 = this.D_Power > 0;
				if (flag3)
				{
					this.D_Power--;
					((IHeroDataHuangZhong)this.IHD).HD_Power = this.D_Power;
					this.SetLevelColor();
				}
			}
		}
	}

	// Token: 0x04000E4D RID: 3661
	private CWeaponControl hzwc;

	// Token: 0x04000E4E RID: 3662
	private bool weaponReadyToFire;

	// Token: 0x04000E4F RID: 3663
	private Animator hzAnim;

	// Token: 0x04000E50 RID: 3664
	private GameObject[] PowerBuffs;

	// Token: 0x04000E51 RID: 3665
	private int D_Power;

	// Token: 0x04000E52 RID: 3666
	private float reduceGap;

	// Token: 0x04000E53 RID: 3667
	private bool b_superState;
}
