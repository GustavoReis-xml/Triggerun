using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class SkillDWRocketMe : SkillBaseMe
{
	// Token: 0x060026BA RID: 9914 RVA: 0x0016006D File Offset: 0x0015E26D
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.onSkillDoing = false;
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x00160080 File Offset: 0x0015E280
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x00160098 File Offset: 0x0015E298
	public override void readyItemSwitch(bool _Value, bool isDoSkill = false)
	{
		if (_Value)
		{
			((CWeaponControlMe)this._hero.wc).cancelFillCartridge();
			base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
			base.hero.SetAnimator("skillE_S", 0, true, 1);
		}
		else
		{
			bool flag = !isDoSkill;
			if (flag)
			{
				base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
				base.hero.SetAnimator("skillE_S", 0, false, 1);
			}
		}
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x00160128 File Offset: 0x0015E328
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.hero.charMove.notMove = true;
		base.hero.wInput.notFire = true;
		this.Break_Reload();
		base.OneSkillAction(target, parm2);
		Vector3 position = Vector3.zero;
		Vector3 rotation = Vector3.zero;
		CPlaceItem readyItem = base.hero.skillInfo.getReadyItem(base.iSkillIndex);
		bool flag = !(readyItem == null);
		if (flag)
		{
			base.hero.SetAnimator("skill_E", 0, true, 1);
			this.onSkillDoing = true;
			position = readyItem.ThisGameObjTrans.transform.position;
			rotation = readyItem.ThisGameObjTrans.transform.eulerAngles;
			base.hero.Npc_Handle_Component.Create_Npc(base.hero, readyItem.HitNetId, 1040, position, rotation, Vector3.one, this.skillData.id, Vector3.zero, string.Empty);
			this.vpos = position;
			this.obj = readyItem;
			Vector3 dir = this.vpos - base.transform.position;
			CameraControl.Instance.CameraRoration(true, 60, dir, CameraControl.Instance.MoveDir);
			base.StartCoroutine(this.Wait_Finish());
		}
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x0016026B File Offset: 0x0015E46B
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(1f);
		this.EndSkill();
		yield break;
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x0016027C File Offset: 0x0015E47C
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			base.hero.charMove.notMove = false;
			this.onSkillDoing = false;
			base.OneSkillFinish();
			bool flag2 = !base.hero.GetAnimatorBool("skill_shift");
			if (flag2)
			{
				base.hero.wInput.notFire = false;
			}
			else
			{
				base.hero.bePlayActive = false;
			}
			CameraControl.Instance.CameraRoration(false, 60, Vector3.zero, 0);
			base.hero.SetAnimator("skillE_S", 0, false, 1);
			base.hero.SetAnimator("skill_E", 0, false, 1);
		}
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x00160360 File Offset: 0x0015E560
	private void EndSkill()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy && this.onSkillDoing;
		if (flag)
		{
			((CWeaponControlMe)base.hero.wc).WeaponActiveBySkill(2, base.iSkillIndex);
			((CWeaponControlMe)base.hero.wc).WeaponDownKey(2);
			((CWeaponControlMe)base.hero.wc).WeaponFire(2, "0", this.vpos, this.obj);
			base.Invoke("OneSkillFinish", 0.5f);
		}
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x00160400 File Offset: 0x0015E600
	private void Update()
	{
		bool flag = this.onSkillDoing && (base.hero.playerCommon.m_nHunMi == 1 || base.hero.playerCommon.m_nDongjie == 1);
		if (flag)
		{
			this.OneSkillFinish();
			base.hero.SetAnimator("skill_break", 0, true, 1);
		}
	}

	// Token: 0x0400223D RID: 8765
	private Vector3 vpos;

	// Token: 0x0400223E RID: 8766
	private object obj;

	// Token: 0x0400223F RID: 8767
	private bool onSkillDoing;
}
