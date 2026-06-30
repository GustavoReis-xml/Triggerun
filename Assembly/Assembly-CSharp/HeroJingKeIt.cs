using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class HeroJingKeIt : HeroBaseIt
{
	// Token: 0x0600127A RID: 4730 RVA: 0x000B22DC File Offset: 0x000B04DC
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		((TBCharacterThirdIt)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		((TBCharacterThirdIt)base.hero.tChar).wallRunWeightSpeed = 4f;
		base.hero.tChar.wallRunCan = true;
		this.CreatFilmBlood();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x000B235C File Offset: 0x000B055C
	private void CreatFilmBlood()
	{
		this.JingKeBloodFilmObj = new GameObject("JingKeBloodFilmObj");
		this.JingKeBloodFilmObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
		this.JingKeBloodFilmObj.transform.localScale = Vector3.one;
		this.JingKeBloodFilmObj.transform.localPosition = Vector3.zero;
		this.JingKeBloodFilmObj.transform.parent = base.transform.parent;
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x000B23DD File Offset: 0x000B05DD
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, true, 1);
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x000B2400 File Offset: 0x000B0600
	public override void OnDeath()
	{
		base.OnDeath();
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000B240A File Offset: 0x000B060A
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x000B2422 File Offset: 0x000B0622
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000B2431 File Offset: 0x000B0631
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x000B243C File Offset: 0x000B063C
	public Transform BloodFilmFather()
	{
		bool flag = this.JingKeBloodFilmObj != null;
		Transform transform;
		if (flag)
		{
			transform = this.JingKeBloodFilmObj.transform;
		}
		else
		{
			this.CreatFilmBlood();
			transform = this.JingKeBloodFilmObj.transform;
		}
		return transform;
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x000B2480 File Offset: 0x000B0680
	protected void OnTriggerEnter(Collider _Collider)
	{
		bool flag = base.hero.playerCommon.GetHpBiLi() != 1f && _Collider.gameObject.layer == LayerMask.NameToLayer("BuffItem") && !base.hero.isDead && !base.hero.haveDestroy && _Collider.gameObject.name.Contains("npc_lhq");
		if (flag)
		{
			UnityEngine.Object.Destroy(_Collider.gameObject);
		}
	}

	// Token: 0x04000EA3 RID: 3747
	private GameObject JingKeBloodFilmObj;
}
