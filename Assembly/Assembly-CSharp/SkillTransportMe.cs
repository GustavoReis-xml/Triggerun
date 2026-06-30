using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class SkillTransportMe : SkillBaseMe
{
	// Token: 0x0600289F RID: 10399 RVA: 0x0016B84F File Offset: 0x00169A4F
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x0016B85C File Offset: 0x00169A5C
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x0016B8A0 File Offset: 0x00169AA0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		bool flag = this.perfab != null;
		if (flag)
		{
			this.eff = CEffectPools.Instance.CreateGameObject(this.perfab, (Vector3)__target, Quaternion.identity);
			this.eff.transform.parent = fGameManager.allBaozhaFather.transform;
			EffectAutoDestroy effectAutoDestroy = this.eff.AddComponent<EffectAutoDestroy>();
			effectAutoDestroy.PutInPools = false;
		}
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.hero.SetAnimator("SkillTP", 0, false, 2);
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.gotoThird), __target);
		base.hero.heroBase.B_Transforing = true;
		base.hero.SetAnimator("SkillTPIng", 0, true, 2);
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x0016B9B0 File Offset: 0x00169BB0
	public override void OneSkillInput()
	{
		base.OneSkillInput();
		bool flag = base.hero.heroBase.IsHeroInvi();
		if (flag)
		{
			base.hero.DeleteBuffEffect(base.hero, 1012);
		}
		bool flag2 = base.hero.heroSkill.CanDo(1007) == 1;
		if (flag2)
		{
			this.BreakSth(0);
		}
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x0016BA18 File Offset: 0x00169C18
	private IEnumerator gotoPlace(Vector3 __target)
	{
		yield return new WaitForSeconds(1.5f);
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
			yield break;
		}
		bool flag = base.hero.playerCommon.m_nDongjie == 1;
		if (flag)
		{
			base.hero.SetAnimator("SkillTPIng", 0, false, 2);
			base.hero.SetAnimator("FreezeBreak", 0, true, 1);
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			base.hero.changeCameraState(CameraState.FirstState, null, "skilltp");
			this.OneSkillFinish();
			yield break;
		}
		Vector3 pos = base.hero.skillInfo.getReadyItem(base.iSkillIndex).ThisGameObjTrans.transform.position;
		base.hero.setPlayerNewPos(pos);
		base.hero.SetAnimator("SkillTPIng", 0, false, 2);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		yield return new WaitForSeconds(1f);
		bool flag2 = !base.hero.isDead;
		if (flag2)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, "skilltp");
		}
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x0016BA30 File Offset: 0x00169C30
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(0);
		base.hero.SendMessageToServer(31105, arrayList);
		this.BreakSth(0);
		base.hero.heroBase.B_Transforing = false;
		base.hero.charMove.notMove = false;
		base.hero.wInput.notInput = false;
		base.hero.bePlayActive = true;
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x0016BAC8 File Offset: 0x00169CC8
	private void gotoThird(object objParam)
	{
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
		}
		Vector3 _target = (Vector3)objParam;
		base.StartCoroutine(this.gotoPlace(_target));
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x0016BB04 File Offset: 0x00169D04
	public override void readyItemSwitch(bool _Value, bool isDoSkill = false)
	{
		bool flag = !_Value && !isDoSkill;
		if (flag)
		{
			base.StartCoroutine(this.Detection_Reload());
		}
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x0016BB2F File Offset: 0x00169D2F
	private IEnumerator Detection_Reload()
	{
		yield return new WaitForSeconds(1f);
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
		yield break;
	}

	// Token: 0x04002320 RID: 8992
	private UnityEngine.Object perfab;

	// Token: 0x04002321 RID: 8993
	private GameObject eff;
}
