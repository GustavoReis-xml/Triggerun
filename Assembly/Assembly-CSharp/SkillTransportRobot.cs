using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class SkillTransportRobot : SkillBaseRobot
{
	// Token: 0x060028A9 RID: 10409 RVA: 0x0016BB47 File Offset: 0x00169D47
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.transPortPos = Vector3.zero;
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x0016BB5C File Offset: 0x00169D5C
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x0016BBA0 File Offset: 0x00169DA0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		bool flag = this.perfab != null;
		if (flag)
		{
			this.eff = CEffectPools.Instance.CreateGameObject(this.perfab, (Vector3)__target, Quaternion.identity);
			this.eff.transform.parent = fGameManager.allBaozhaFather.transform;
			EffectAutoDestroy effectAutoDestroy = this.eff.AddComponent<EffectAutoDestroy>();
			effectAutoDestroy.PutInPools = false;
		}
		((RobotAI)base.hero).robotController.setSkillIng(true);
		base.hero.charMove.notMove = true;
		base.hero.SetAnimator("SkillTP", 0, true, 1);
		base.StartCoroutine(this.RealTransPort());
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x0016BC66 File Offset: 0x00169E66
	private IEnumerator RealTransPort()
	{
		yield return new WaitForSeconds(1f);
		base.hero.SetAnimator("SkillTP", 0, false, 1);
		this.gotoThird();
		base.hero.heroBase.B_Transforing = true;
		base.hero.SetAnimator("SkillTPIng", 0, true, 1);
		yield break;
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x0016BC75 File Offset: 0x00169E75
	private IEnumerator gotoPlace()
	{
		yield return new WaitForSeconds(1.5f);
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
		}
		else
		{
			bool flag = base.hero.playerCommon.m_nDongjie == 1;
			if (flag)
			{
				base.hero.SetAnimator("SkillTPIng", 0, false, 1);
				base.hero.SetAnimator("FreezeBreak", 0, true, 1);
				this.OneSkillFinish();
			}
			else
			{
				((RobotAI)base.hero).robotController.SyncPosition();
				base.hero.SetAnimator("SkillTPIng", 0, false, 1);
				yield return new WaitForSeconds(1f);
				this.OneSkillFinish();
			}
		}
		yield break;
	}

	// Token: 0x060028AE RID: 10414 RVA: 0x0016BC84 File Offset: 0x00169E84
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(base.hero.getNetid());
		base.hero.heroBase.B_Transforing = false;
		base.hero.charMove.notMove = false;
		base.hero.bePlayActive = true;
		((RobotAI)base.hero).robotController.setSkillIng(false);
	}

	// Token: 0x060028AF RID: 10415 RVA: 0x0016BD10 File Offset: 0x00169F10
	private void gotoThird()
	{
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
		}
		base.StartCoroutine(this.gotoPlace());
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x0016BD44 File Offset: 0x00169F44
	public override void readyItemSwitch(bool _Value, bool isDoSkill = false)
	{
		bool flag = !_Value && !isDoSkill;
		if (flag)
		{
			base.StartCoroutine(this.Detection_Reload());
		}
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x0016BD6F File Offset: 0x00169F6F
	private IEnumerator Detection_Reload()
	{
		yield return new WaitForSeconds(1f);
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		yield break;
	}

	// Token: 0x04002322 RID: 8994
	private UnityEngine.Object perfab;

	// Token: 0x04002323 RID: 8995
	private GameObject eff;

	// Token: 0x04002324 RID: 8996
	public Vector3 transPortPos;
}
