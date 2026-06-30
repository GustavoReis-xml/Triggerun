using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class HeroNeZhaMe : HeroBaseMe
{
	// Token: 0x0600139D RID: 5021 RVA: 0x000BA214 File Offset: 0x000B8414
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.bAutoBreakCartridge = false;
		this.CreateNeZhaShift();
		this.NeZhaShiftShow(false);
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x000BA236 File Offset: 0x000B8436
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
		base.hero.SetAnimator("skill_Q", 0, true, 2);
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000BA26E File Offset: 0x000B846E
	private IEnumerator Wait_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000BA280 File Offset: 0x000B8480
	public override void SuperSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy && this.superDoing;
		if (flag)
		{
			base.SuperSkillFinish();
			((Skill_NeZhaBiBiMe)base.hero.skillEffectDict[1183]).OneSkillFinish();
			this.superDoing = false;
			base.hero.SetAnimator("skill_Q", 0, false, 2);
		}
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x000BA2FC File Offset: 0x000B84FC
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !base.hero.wInput.notInput;
		if (flag)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000BA34E File Offset: 0x000B854E
	public override void OnDeath()
	{
		base.OnDeath();
		this.IgnoreLayer(false);
		this.NeZhaShiftShow(false);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000BA368 File Offset: 0x000B8568
	public override void Revive()
	{
		base.Revive();
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000BA372 File Offset: 0x000B8572
	public override void ChanngeLayerCollider(bool revive)
	{
		base.ChanngeLayerCollider(false);
		this.IgnoreLayer(false);
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000BA385 File Offset: 0x000B8585
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		this.IgnoreLayer(false);
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x000BA398 File Offset: 0x000B8598
	private void CreateNeZhaShift()
	{
		this.neZhaShiftObj = new GameObject("NeZhaShiftObj");
		this.neZhaShiftObj.transform.SetParent(base.gameObject.transform);
		this.neZhaShiftObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
		this.neZhaShiftObj.transform.localScale = Vector3.one;
		this.neZhaShiftObj.transform.localPosition = new Vector3(0f, 0.35f, 1f);
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			this.neZhaShiftObj.layer = LayerMask.NameToLayer("fight_1");
		}
		bool flag2 = base.hero.playerCommon.m_nShili == 2;
		if (flag2)
		{
			this.neZhaShiftObj.layer = LayerMask.NameToLayer("fight_2");
		}
		this.cap = this.neZhaShiftObj.AddComponent<CapsuleCollider>();
		this.cap.isTrigger = true;
		this.cap.radius = 1.25f;
		this.cap.height = 1.8f;
		Vector3 center = this.cap.center;
		center.y = 0.9f;
		this.cap.center = center;
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000BA4EC File Offset: 0x000B86EC
	public void NeZhaShiftShow(bool hide)
	{
		bool flag = this.neZhaShiftObj.activeSelf != hide;
		if (flag)
		{
			this.neZhaShiftObj.SetActive(hide);
		}
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000BA520 File Offset: 0x000B8720
	public void IgnoreLayer(bool manLayer)
	{
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force1_mainplayer"), LayerMask.NameToLayer("force2_player"), manLayer);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force1_mainplayer"), LayerMask.NameToLayer("force2_npc"), manLayer);
		}
		bool flag2 = base.hero.playerCommon.m_nShili == 2;
		if (flag2)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force2_mainplayer"), LayerMask.NameToLayer("force1_player"), manLayer);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force2_mainplayer"), LayerMask.NameToLayer("force1_npc"), manLayer);
		}
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000BA5CC File Offset: 0x000B87CC
	public override void BreakSupperSkill()
	{
	}

	// Token: 0x04000F2C RID: 3884
	public bool superDoing;

	// Token: 0x04000F2D RID: 3885
	public GameObject neZhaShiftObj;

	// Token: 0x04000F2E RID: 3886
	protected CapsuleCollider cap;
}
