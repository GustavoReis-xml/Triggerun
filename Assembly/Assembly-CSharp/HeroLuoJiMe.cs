using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class HeroLuoJiMe : HeroBaseMe
{
	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06001353 RID: 4947 RVA: 0x000B8640 File Offset: 0x000B6840
	// (set) Token: 0x06001354 RID: 4948 RVA: 0x000B8658 File Offset: 0x000B6858
	public Npc_Entity NpcObj
	{
		get
		{
			return this._NpcObj;
		}
		set
		{
			this._NpcObj = value;
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x000B8664 File Offset: 0x000B6864
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill");
		if (flag)
		{
			bool flag2 = this.isSuper;
			if (flag2)
			{
				Debug.Log("释放大招完毕");
			}
			else
			{
				bool flag3 = !base.hero.wInput.notInput && base.hero.superSkill(null);
				if (flag3)
				{
				}
			}
		}
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000B86D4 File Offset: 0x000B68D4
	public override void OnChangeToFps()
	{
		base.StartCoroutine(this.OnReviveResetAnimator());
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x000B86E4 File Offset: 0x000B68E4
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.wInput.notInput = true;
		base.hero.SetAnimator("Reload", 0, false, 1);
		this.isSuper = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		base.hero.playerDazhao.doShootReset(base.hero);
		base.hero.playerDazhao.doShoot(base.hero);
		base.hero.bePlayActive = false;
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000B87A3 File Offset: 0x000B69A3
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.2f);
		base.hero.wInput.notInput = false;
		yield return new WaitForSeconds(0.8f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x000B87B4 File Offset: 0x000B69B4
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		base.hero.charMove.notMove = false;
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.hero.bePlayActive = true;
		this.isSuper = false;
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x000B8808 File Offset: 0x000B6A08
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
			base.hero.SetAnimator("SkillBoom", 0, false, 2);
		}
		this.IsCreateItemIng = false;
		this.SuperSkillFinish();
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x000B8880 File Offset: 0x000B6A80
	public override void Revive()
	{
		base.Revive();
		base.StartCoroutine(this.OnReviveResetAnimator());
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x000B8897 File Offset: 0x000B6A97
	private IEnumerator OnReviveResetAnimator()
	{
		yield return new WaitForSeconds(0.1f);
		bool flag = this.NpcObj != null && !this.NpcObj.Death_State;
		if (flag)
		{
			base.hero.SetAnimator("LeftHandHadHide", 0, true, 2);
			base.hero.SetAnimator("LeftHandHide", 0, false, 2);
			base.hero.SetAnimator("IsShiftOut", 0, true, 1);
		}
		else
		{
			base.hero.SetAnimator("LeftHandHadHide", 0, false, 2);
			base.hero.SetAnimator("LeftHandHide", 0, true, 2);
		}
		yield break;
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x000B88A8 File Offset: 0x000B6AA8
	public override void GetHeroNpc(object obj)
	{
		base.GetHeroNpc(obj);
		bool main = ((Npc_Entity)obj).Main;
		if (main)
		{
			this.NpcObj = (Npc_Entity)obj;
		}
		this.OnCreateNpcOk();
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x000B88E4 File Offset: 0x000B6AE4
	private void OnCreateNpcOk()
	{
		bool isDoSkillYaoKong = this.IsDoSkillYaoKong;
		if (isDoSkillYaoKong)
		{
			this.isZiSha = true;
		}
		this.IsDoSkillYaoKong = true;
		this.UserYaoKong = false;
		this.IsCreateItemIng = false;
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000B891C File Offset: 0x000B6B1C
	public void OnNpcDestory()
	{
		this.IsDoSkillYaoKong = false;
		bool userYaoKong = this.UserYaoKong;
		if (userYaoKong)
		{
			this.UserYaoKong = false;
		}
		else
		{
			this.UserYaoKong = false;
			bool flag = !base.hero.GetAnimatorBool("SkillBoom");
			if (flag)
			{
				base.hero.SetAnimator("IsShiftOut", 0, false, 1);
				base.hero.SetAnimator("LeftHandHadHide", 0, false, 2);
				base.hero.SetAnimator("ShiftIng", 0, false, 1);
				base.hero.SetAnimator("LeftHandHide", 0, true, 2);
			}
			else
			{
				base.hero.SetAnimator("ShiftIng", 0, false, 1);
				base.hero.SetAnimator("LeftHandHadHide", 0, false, 2);
			}
		}
	}

	// Token: 0x04000F0D RID: 3853
	private Npc_Entity _NpcObj;

	// Token: 0x04000F0E RID: 3854
	public bool isZiSha;

	// Token: 0x04000F0F RID: 3855
	public bool IsDoSkillYaoKong;

	// Token: 0x04000F10 RID: 3856
	public bool isSuper;

	// Token: 0x04000F11 RID: 3857
	public bool UserYaoKong;

	// Token: 0x04000F12 RID: 3858
	public bool IsCreateItemIng;
}
