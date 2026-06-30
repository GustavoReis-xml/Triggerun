using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class SkillZRFireBallMe : SkillBaseMe
{
	// Token: 0x06002932 RID: 10546 RVA: 0x0016CE20 File Offset: 0x0016B020
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		bool flag = base.hero.GetAnimatorInteger("AttackState") != 0;
		if (flag)
		{
			base.hero.SetAnimator("AttackState", 2, 0, 2);
		}
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		this.isSkillDo = true;
		base.hero.bePlayActive = false;
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.ChangeCameraEnd), "SkillZRFireBallStart");
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x0016CED8 File Offset: 0x0016B0D8
	public override bool OneSkillCanDo()
	{
		base.hero.wc.UpdateFireTime(0f);
		bool flag = base.hero.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			result = (!flag2 && base.OneSkillCanDo());
		}
		return result;
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x0016CF40 File Offset: 0x0016B140
	private void ChangeCameraEnd(object objParam)
	{
		bool flag = objParam.ToString() == "SkillZRFireBallStart";
		if (flag)
		{
			bool isDead = base.hero.isDead;
			if (isDead)
			{
				this.OneSkillFinish();
			}
			base.hero.SetAnimator("SkillE", 0, true, 2);
			this.lastTime = Time.time + (float)base.heroSkillData.m_nSKLongTime / 1000f;
		}
		bool flag2 = objParam.ToString() == "SkillZRFireBallEnd";
		if (flag2)
		{
			base.hero.SetAnimator("SkillE", 0, false, 2);
			base.hero.charMove.notMove = false;
			base.hero.wInput.notInput = false;
			this.lastTime = 0f;
			base.hero.bePlayActive = true;
		}
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x0016D020 File Offset: 0x0016B220
	public override void OneSkillFinish()
	{
		bool flag = this.isSkillDo;
		if (flag)
		{
			base.OneSkillFinish();
			this.isSkillDo = false;
			base.hero.changeCameraState(CameraState.FirstState, new NetLiving.ChangeCameraStateEnd(this.ChangeCameraEnd), "SkillZRFireBallEnd");
			this.Send_Messages(1);
		}
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x0016D074 File Offset: 0x0016B274
	public void Update()
	{
		bool flag = this.isSkillDo;
		if (flag)
		{
			bool flag2 = Time.time >= this.lastTime;
			if (flag2)
			{
				this.OneSkillFinish();
			}
			bool flag3 = base.hero.playerCommon.m_nDongjie != 1 && (Input_Manager.Get_Button_Down("WeaponFirst") || Input_Manager.Get_Button_Down("WeaponSec"));
			if (flag3)
			{
				base.StopAllCoroutines();
				this.OneSkillFinish();
				base.hero.wc.UpdateFireTime(0.2f);
			}
		}
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x0016D104 File Offset: 0x0016B304
	private void Send_Messages(int _State)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.hero.getNetid());
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(_State);
		base.hero.SendSkillParms(arrayList);
	}

	// Token: 0x04002327 RID: 8999
	private bool isSkillDo;

	// Token: 0x04002328 RID: 9000
	private float lastTime;
}
