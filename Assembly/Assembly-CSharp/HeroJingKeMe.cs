using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class HeroJingKeMe : HeroBaseMe
{
	// Token: 0x06001284 RID: 4740 RVA: 0x000B250C File Offset: 0x000B070C
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.bAutoBreakCartridge = false;
		base.hero.SetAnimator("PlayerState", 2, 3, 1);
		((TBCharacterThirdMe)base.hero.tChar).wallRunRotationSpeed = 5f;
		((TBCharacterThirdMe)base.hero.tChar).wallRunMaxRotationAngle = 90f;
		((TBCharacterThirdMe)base.hero.tChar).wallRunWeightSpeed = 5f;
		base.hero.tChar.wallRunCan = true;
		this.checkNeedPlayClimAnim = false;
		this.playPaqiang = false;
		this.CreateJingKeShift();
		this.JingKeObjShow(false);
		this.CreatJingKeBloodObj();
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000B25C8 File Offset: 0x000B07C8
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
		base.hero.SetAnimator("skill_Q", 0, true, 2);
		((TBCharacterThirdMe)base.hero.tChar).canWall = false;
		base.StartCoroutine(this.Wait_Finish());
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x000B2621 File Offset: 0x000B0821
	private IEnumerator Wait_Finish()
	{
		this.superDoing = false;
		((TBCharacterThirdMe)base.hero.tChar).canWall = true;
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x000B2630 File Offset: 0x000B0830
	public override void SuperSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			((SkillJingKeQMe)base.hero.skillEffectDict[1133]).OneSkillFinish();
			base.hero.SetAnimator("skill_Q", 0, false, 2);
			base.SuperSkillFinish();
		}
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x000B26A0 File Offset: 0x000B08A0
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero.playerCommon.GetHpBiLi() == 1f;
		if (flag)
		{
			bool flag2 = !this.buffF;
			if (flag2)
			{
				this.buffF = true;
			}
		}
		else
		{
			bool flag3 = this.buffF;
			if (flag3)
			{
				this.buffF = false;
			}
		}
		bool flag4 = !this.InputCheck();
		if (!flag4)
		{
			bool flag5 = this.JingKeBloodObj.transform.position != Vector3.zero;
			if (flag5)
			{
				this.ChangeBloodObjV(Vector3.zero);
			}
			bool flag6 = Input_Manager.Get_Button_Down("superskill");
			if (flag6)
			{
				base.hero.superSkill(null);
			}
			bool flag7 = base.hero == null || base.hero.wInput == null || base.hero.tCharAnimationTps == null || base.hero.tCharAnimationFps == null;
			if (!flag7)
			{
				bool flag8 = base.hero.tChar.wallState > TBCharacter.WallState.OnGround;
				if (flag8)
				{
					bool flag9 = base.hero.tChar.wallState == TBCharacter.WallState.OnWall && !this.checkNeedPlayClimAnim;
					if (flag9)
					{
						this.checkNeedPlayClimAnim = true;
						base.StartCoroutine(this.checkNeedPlayClimbAnim());
						((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
					}
					else
					{
						bool flag10 = base.hero.tChar.wallState != TBCharacter.WallState.OnWall && this.onwall && !this.playPaqiang;
						if (flag10)
						{
							this.StopClimbAnim();
							bool flag11 = (CWeaponControlMe)base.hero.wc != null;
							if (flag11)
							{
								((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
							}
						}
					}
				}
				else
				{
					bool flag12 = this.onwall;
					if (flag12)
					{
						this.StopClimbAnim();
						base.hero.SetAnimator("PaQiang", 0, false, 1);
					}
				}
			}
		}
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x000B28B8 File Offset: 0x000B0AB8
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.tChar.wallState = TBCharacter.WallState.OnGround;
		this.onwall = false;
		base.hero.charMove.state.onwall = 0;
		this.StopClimbAnim();
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		base.hero.SetAnimator("PaQiangEnd", 0, false, 1);
		this.JingKeObjShow(false);
		this.IgnoreLayer(false);
		this.ChangeBloodObjV(Vector3.up * 100f);
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x000B295A File Offset: 0x000B0B5A
	private void ChangeBloodObjV(Vector3 vstat)
	{
		this.JingKeBloodObj.transform.position = vstat;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x000B2970 File Offset: 0x000B0B70
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Aim", 0, true, 1);
		base.hero.SetAnimator("PaQiang", 0, false, 1);
		base.hero.SetAnimator("PaQiangEnd", 0, false, 1);
		this.onwall = false;
		this.ChangeBloodObjV(Vector3.zero);
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x000B29E3 File Offset: 0x000B0BE3
	private void playClimbAnim()
	{
		base.hero.wInput.notInput = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		this.onwall = true;
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x000B2A15 File Offset: 0x000B0C15
	private void StopClimbAnim()
	{
		base.StopCoroutine(this.checkNeedPlayClimbAnim());
		CameraControl.Instance.StopPaQiangCaMera();
		this.onwall = false;
		base.hero.wInput.notInput = false;
		this.checkNeedPlayClimAnim = false;
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x000B2A50 File Offset: 0x000B0C50
	private IEnumerator checkNeedPlayClimbAnim()
	{
		yield return new WaitForSeconds(0.05f);
		bool flag = base.hero.charMove.state.onwall == 1;
		if (flag)
		{
			CameraControl.Instance.SetPaQiangCaMera();
			this.playClimbAnim();
		}
		else
		{
			bool flag2 = base.hero.charMove.state.onwall != 1;
			if (flag2)
			{
				this.checkNeedPlayClimAnim = false;
			}
		}
		yield break;
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000B2A60 File Offset: 0x000B0C60
	private void CreateJingKeShift()
	{
		this.jingKeShiftObj = new GameObject("JingkeShift");
		this.jingKeShiftObj.transform.SetParent(base.gameObject.transform);
		this.jingKeShiftObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
		this.jingKeShiftObj.transform.localScale = Vector3.one;
		this.jingKeShiftObj.transform.localPosition = Vector3.zero;
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			this.jingKeShiftObj.layer = LayerMask.NameToLayer("fight_1");
		}
		bool flag2 = base.hero.playerCommon.m_nShili == 2;
		if (flag2)
		{
			this.jingKeShiftObj.layer = LayerMask.NameToLayer("fight_2");
		}
		this.cap = this.jingKeShiftObj.AddComponent<CapsuleCollider>();
		this.cap.isTrigger = true;
		this.cap.radius = 1.25f;
		this.cap.height = 1.8f;
		Vector3 center = this.cap.center;
		center.y = 0.9f;
		this.cap.center = center;
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x000B2BA4 File Offset: 0x000B0DA4
	public void JingKeObjShow(bool hide)
	{
		bool flag = this.jingKeShiftObj.activeSelf != hide;
		if (flag)
		{
			this.jingKeShiftObj.SetActive(hide);
		}
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x000B2BD8 File Offset: 0x000B0DD8
	private void IgnoreBUFF(bool buffeOn)
	{
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force1_mainplayer"), LayerMask.NameToLayer("BuffItem"), buffeOn);
		}
		bool flag2 = base.hero.playerCommon.m_nShili == 2;
		if (flag2)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("force2_mainplayer"), LayerMask.NameToLayer("BuffItem"), buffeOn);
		}
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x000B2C50 File Offset: 0x000B0E50
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

	// Token: 0x06001293 RID: 4755 RVA: 0x000B2CFC File Offset: 0x000B0EFC
	public override void ChanngeLayerCollider(bool revive)
	{
		base.ChanngeLayerCollider(false);
		this.IgnoreLayer(false);
		bool flag = this.JingKeBloodObj != null && !revive;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.JingKeBloodObj);
		}
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000B2D44 File Offset: 0x000B0F44
	public Transform BloodFather()
	{
		bool flag = this.JingKeBloodObj != null;
		Transform transform;
		if (flag)
		{
			transform = this.JingKeBloodObj.transform;
		}
		else
		{
			this.CreatJingKeBloodObj();
			transform = this.JingKeBloodObj.transform;
		}
		return transform;
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x000B2D88 File Offset: 0x000B0F88
	private void CreatJingKeBloodObj()
	{
		bool flag = this.JingKeBloodObj == null;
		if (flag)
		{
			this.JingKeBloodObj = new GameObject("JingKeBloodObj");
			this.JingKeBloodObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this.JingKeBloodObj.transform.localScale = Vector3.one;
			this.JingKeBloodObj.transform.localPosition = Vector3.zero;
			this.JingKeBloodObj.transform.parent = base.transform.parent;
		}
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x000B2E1B File Offset: 0x000B101B
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		this.IgnoreLayer(false);
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x000B2E2D File Offset: 0x000B102D
	public override void BreakSupperSkill()
	{
	}

	// Token: 0x04000EA4 RID: 3748
	public bool superDoing;

	// Token: 0x04000EA5 RID: 3749
	private bool onwall;

	// Token: 0x04000EA6 RID: 3750
	private bool checkNeedPlayClimAnim;

	// Token: 0x04000EA7 RID: 3751
	private bool playPaqiang;

	// Token: 0x04000EA8 RID: 3752
	public PlayerCommon playerCommon;

	// Token: 0x04000EA9 RID: 3753
	public GameObject jingKeShiftObj;

	// Token: 0x04000EAA RID: 3754
	protected CapsuleCollider cap;

	// Token: 0x04000EAB RID: 3755
	public bool buffF;

	// Token: 0x04000EAC RID: 3756
	public GameObject JingKeBloodObj;
}
