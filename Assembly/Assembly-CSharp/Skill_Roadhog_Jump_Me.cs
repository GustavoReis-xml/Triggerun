using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class Skill_Roadhog_Jump_Me : SkillBaseMe
{
	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x060029CB RID: 10699 RVA: 0x0016FE50 File Offset: 0x0016E050
	// (set) Token: 0x060029CC RID: 10700 RVA: 0x0016FE68 File Offset: 0x0016E068
	public float Direction_Angel
	{
		get
		{
			return this._Direction_Angel;
		}
		set
		{
			this._Direction_Angel = value;
		}
	}

	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x060029CD RID: 10701 RVA: 0x0016FE74 File Offset: 0x0016E074
	// (set) Token: 0x060029CE RID: 10702 RVA: 0x0016FE8C File Offset: 0x0016E08C
	public GameObject Camera_Target_Object
	{
		get
		{
			return this._Camera_Target_Object;
		}
		set
		{
			this._Camera_Target_Object = value;
		}
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x0016FE98 File Offset: 0x0016E098
	private void Super_Jump_Start(object _target)
	{
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		Transform transform = this.Camera_Target_Object.transform;
		transform.localEulerAngles = new Vector3(this.Direction_Angel, transform.localEulerAngles.y, transform.localEulerAngles.z);
		((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(true);
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity((transform.forward + transform.up * 0.5f) * 14f, 1f, false);
		base.StartCoroutine(this.Super_Jump_End());
		this._Target = _target;
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x0016FF6B File Offset: 0x0016E16B
	private IEnumerator Super_Jump_End()
	{
		yield return new WaitForSeconds(0.1f);
		bool flag = base.hero.tChar.GroundDistance < 0.3f && base.hero.tChar.rig.velocity.y <= 0f;
		if (flag)
		{
			this.OneSkillFinish();
			((CWeaponControlMe)base.hero.wc).WeaponActiveBySkill(2, base.iSkillIndex);
			((CWeaponControlMe)base.hero.wc).WeaponDownKey(2);
			((CWeaponControlMe)base.hero.wc).WeaponFire(2, "0", (Vector3)this._Target, base.hero.hitGamaObj);
			base.hero.wInput.notInput = false;
			base.hero.charMove.notMove = false;
		}
		else
		{
			base.StartCoroutine(this.Super_Jump_End());
		}
		yield break;
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x0016FF7A File Offset: 0x0016E17A
	public override void OneSkillAction(object _target, ArrayList parm2)
	{
		this.Super_Jump_Start(_target);
	}

	// Token: 0x04002349 RID: 9033
	private float _Direction_Angel = -45f;

	// Token: 0x0400234A RID: 9034
	private GameObject _Camera_Target_Object;

	// Token: 0x0400234B RID: 9035
	private object _Target;
}
