using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class SkillTransportIt : SkillBaseIt
{
	// Token: 0x06002897 RID: 10391 RVA: 0x0016B630 File Offset: 0x00169830
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x0016B63C File Offset: 0x0016983C
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x0016B67D File Offset: 0x0016987D
	public override void OneSkillInput()
	{
		base.OneSkillInput();
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x0016B688 File Offset: 0x00169888
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		ArrayList arrayList = (ArrayList)__target;
		ArrayList arrayList2 = (ArrayList)arrayList[1];
		Vector3 vector = new Vector3((float)((int)arrayList2[0]) * 0.01f, (float)((int)arrayList2[1]) * 0.01f, (float)((int)arrayList2[2]) * 0.01f);
		bool flag = this.perfab != null;
		if (flag)
		{
			this.eff = CEffectPools.Instance.CreateGameObject(this.perfab, vector, Quaternion.identity);
			this.eff.transform.parent = fGameManager.allBaozhaFather.transform;
			EffectAutoDestroy effectAutoDestroy = this.eff.AddComponent<EffectAutoDestroy>();
			effectAutoDestroy.PutInPools = false;
		}
		bool flag2 = base.hero.tChar != null;
		if (flag2)
		{
			base.hero.tChar.hangInAir = true;
		}
		this.needCheck = true;
		base.hero.heroBase.changeBackToNormalState();
		base.hero.SetAnimator("SkillTPIng", 0, true, 1);
		base.hero.heroBase.B_Transforing = true;
		base.StartCoroutine(this.checkMove(vector));
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x0016B7CC File Offset: 0x001699CC
	private IEnumerator checkMove(Vector3 target)
	{
		float addtime = 0f;
		bool finish = false;
		while (this.needCheck)
		{
			addtime += Time.deltaTime;
			yield return new WaitForSeconds(0.05f);
			bool flag = !base.gameplayer.GetAnimatorBool("SkillTPIng") || addtime > 5f || base.hero.isDead;
			if (flag)
			{
				finish = true;
				this.needCheck = false;
			}
		}
		bool flag2 = finish;
		if (flag2)
		{
			while (addtime <= 0.15f)
			{
				addtime += Time.deltaTime;
				yield return new WaitForSeconds(0.05f);
			}
			bool flag3 = base.hero.tChar != null;
			if (flag3)
			{
				base.hero.tChar.hangInAir = false;
			}
			base.hero.SetAnimator("SkillTPIng", 0, false, 1);
			bool flag4 = !this.Freeze || !base.hero.isDead;
			if (flag4)
			{
				((TBUserControlIt)base.gameplayer.charMove).moveTarget = target;
				base.gameplayer.tChar.rig.transform.position = target;
			}
		}
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600289C RID: 10396 RVA: 0x0016B7E2 File Offset: 0x001699E2
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.heroBase.B_Transforing = false;
		this.Freeze = false;
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x0016B804 File Offset: 0x00169A04
	private void Update()
	{
		bool flag = base.hero != null && base.hero.playerCommon.m_nDongjie == 1 && !this.Freeze;
		if (flag)
		{
			this.Freeze = true;
		}
	}

	// Token: 0x0400231C RID: 8988
	private UnityEngine.Object perfab;

	// Token: 0x0400231D RID: 8989
	private GameObject eff;

	// Token: 0x0400231E RID: 8990
	private bool needCheck;

	// Token: 0x0400231F RID: 8991
	private bool Freeze;
}
