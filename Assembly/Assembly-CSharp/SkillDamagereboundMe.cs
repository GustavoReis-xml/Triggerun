using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class SkillDamagereboundMe : SkillBaseMe
{
	// Token: 0x060026B0 RID: 9904 RVA: 0x0015FEC4 File Offset: 0x0015E0C4
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
		this.tZiDan = Fun.Get_Child_Transform(base.hero.heroBase.gameObject, "kedu");
		this.pKeDu = Fun.Get_Child_Transform(this.tZiDan.gameObject, "kedu");
		this.modelAni = this.pKeDu.GetComponent<Animator>();
	}

	// Token: 0x060026B1 RID: 9905 RVA: 0x0015FF2C File Offset: 0x0015E12C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this._Firetarget = __target;
		base.hero.SetAnimator("Reload", 0, false, 1);
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.SetAnimator("AttackState", 2, 3, 1);
		base.StartCoroutine(this.dropOver());
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x0015FFC8 File Offset: 0x0015E1C8
	private IEnumerator dropOver()
	{
		yield return new WaitForSeconds(1.9f);
		base.RealShoot(this._Firetarget, null);
		base.hero.SetAnimator("AttackState", 2, 0, 1);
		base.hero.wInput.notInput = false;
		yield return new WaitForSeconds(0.2f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x0015FFD7 File Offset: 0x0015E1D7
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x0015FFE4 File Offset: 0x0015E1E4
	public void OnUpdateHurt(int nHurt)
	{
		bool flag = !(this.modelAni == null) && this.modelAni.isInitialized;
		if (flag)
		{
			float value = (float)nHurt / 1000f;
			this.modelAni.SetFloat("gxkz", value);
		}
	}

	// Token: 0x04002239 RID: 8761
	private object _Firetarget;

	// Token: 0x0400223A RID: 8762
	private Transform tZiDan;

	// Token: 0x0400223B RID: 8763
	private Animator modelAni;

	// Token: 0x0400223C RID: 8764
	private Transform pKeDu;
}
