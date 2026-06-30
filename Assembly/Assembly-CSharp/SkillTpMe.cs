using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class SkillTpMe : SkillBaseMe
{
	// Token: 0x0600288D RID: 10381 RVA: 0x0016B3FC File Offset: 0x001695FC
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		string url = string.Format("EffectRes/{0}.ef", "skill_yanluo_aybmz");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), 0);
		}
		else
		{
			this.loadReayPrefab(resWWW, 0);
		}
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x0016B460 File Offset: 0x00169660
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x0016B4A4 File Offset: 0x001696A4
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
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.hero.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.gotoThird), __target);
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x0016B573 File Offset: 0x00169773
	private IEnumerator gotoPlace(Vector3 __target)
	{
		yield return new WaitForSeconds(1.5f);
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
			yield break;
		}
		base.hero.setPlayerNewPos(__target);
		base.hero.skillInfo.DisableReadyItem(base.iSkillIndex);
		yield return new WaitForSeconds(1f);
		base.hero.changeCameraState(CameraState.FirstState, null, "skilltp");
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x0016B589 File Offset: 0x00169789
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.charMove.notMove = false;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x0016B5B8 File Offset: 0x001697B8
	private void gotoThird(object objParam)
	{
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			this.OneSkillFinish();
		}
		Vector3 _target = (Vector3)objParam;
		base.StartCoroutine(this.gotoPlace(_target));
		base.hero.SetAnimator("SkillChuansong_do", 0, true, 1);
	}

	// Token: 0x0400231A RID: 8986
	private UnityEngine.Object perfab;

	// Token: 0x0400231B RID: 8987
	private GameObject eff;
}
