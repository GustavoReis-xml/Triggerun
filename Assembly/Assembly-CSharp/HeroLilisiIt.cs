using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class HeroLilisiIt : HeroBaseIt
{
	// Token: 0x0600132C RID: 4908 RVA: 0x000B7B05 File Offset: 0x000B5D05
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000B7B14 File Offset: 0x000B5D14
	public override void SetFilmMaterial(bool show)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1163);
		if (flag)
		{
			((Skill_Lilisi_shiledIt)base.hero.skillEffectDict[1163]).SetFilmMat(show);
		}
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000B7B60 File Offset: 0x000B5D60
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool animatorBool = base.hero.GetAnimatorBool("SkillSuper");
		if (animatorBool)
		{
			this.SuperSkillDo(null);
		}
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000B7B93 File Offset: 0x000B5D93
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.isSuperSkill = true;
		base.hero.tChar.moveByControlIt = true;
		base.StartCoroutine(this.checkFinish());
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000B7BC4 File Offset: 0x000B5DC4
	private IEnumerator checkFinish()
	{
		yield return new WaitForSeconds(1.1f);
		while (this.isSuperSkill)
		{
			yield return new WaitForSeconds(0.1f);
			this.isSuperSkill = base.hero.GetAnimatorBool("SkillSuper");
			bool flag = !this.isSuperSkill;
			if (flag)
			{
				break;
			}
		}
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000B7BD4 File Offset: 0x000B5DD4
	public override void SuperSkillFinish()
	{
		this.isSuperSkill = false;
		base.hero.tChar.moveByControlIt = false;
		base.hero.bePlayActive = true;
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
		}
		base.SuperSkillFinish();
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000B7C78 File Offset: 0x000B5E78
	public override GameObject GetShield(string shieldName)
	{
		bool flag = base.hero.skillEffectDict.ContainsKey(1163);
		GameObject result;
		if (flag)
		{
			result = ((Skill_Lilisi_shiledIt)base.hero.skillEffectDict[1163]).getShieldCol(shieldName);
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000B7CC8 File Offset: 0x000B5EC8
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
	}

	// Token: 0x04000EF7 RID: 3831
	private bool isSuperSkill;
}
