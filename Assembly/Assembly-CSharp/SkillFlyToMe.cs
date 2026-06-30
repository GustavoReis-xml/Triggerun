using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class SkillFlyToMe : SkillBaseMe
{
	// Token: 0x060026D2 RID: 9938 RVA: 0x00160748 File Offset: 0x0015E948
	private void Update()
	{
		bool flag = this.flyState == 0;
		if (!flag)
		{
			bool flag2 = this.flyState == 1;
			if (flag2)
			{
				float num = Vector3.Distance(base.transform.position, this.vTarget);
				bool flag3 = num > this.oldDis || (this.flyType == 1 && num < this.enddis1) || (this.flyType == 2 && num < this.enddis2) || (this.flyType == 3 && num < this.enddis3);
				if (flag3)
				{
					this.OnGrounded();
				}
				else
				{
					base.hero.tChar.moveDirectionNoForce = (this.vTarget - base.transform.position).normalized * this.movespeed;
					this.oldDis = num;
				}
			}
			bool flag4 = this.flyState == 2;
			if (flag4)
			{
				bool flag5 = base.hero.tChar.GroundDistance < 0.3f;
				if (flag5)
				{
					this.OneSkillFinish();
				}
				else
				{
					bool flag6 = base.hero.tChar.rig.velocity.y < 0f;
					if (flag6)
					{
						((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(this.vDirect * this.frontforce + Vector3.up * this.upforce, 0.1f, true, 1000f, 1000f);
					}
				}
			}
		}
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x001608DC File Offset: 0x0015EADC
	public override bool OneSkillCanDo()
	{
		bool flag = base.hero.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = base.hero.screentarget == null;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = base.hero.heroSkill.skillsDict[1052].m_nDoNumberNow <= 0;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = base.hero.screentarget.NetTarget == null;
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = base.hero.screentarget.NetTarget.nForce != base.hero.nForce;
							if (flag6)
							{
								result = false;
							}
							else
							{
								float num = Vector3.Distance(base.hero.screentarget.NetTarget.thisModel.transform.position, base.transform.position);
								bool flag7 = num < 5f;
								if (flag7)
								{
									result = false;
								}
								else
								{
									bool flag8 = num > 40f;
									result = (!flag8 && base.OneSkillCanDo());
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x00160A2C File Offset: 0x0015EC2C
	public override void OneSkillInput()
	{
		base.OneSkillInput();
		bool flag = this.flyState == 1;
		if (flag)
		{
			this.OnGrounded();
		}
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x00160A58 File Offset: 0x0015EC58
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.flyType = 1;
		this.vTarget = base.hero.screentarget.NetTarget.userHead;
		this.oldDis = float.MaxValue;
		this.vDirect = (this.vTarget - base.transform.position).normalized;
		float num = Vector3.Angle(this.vDirect, base.transform.rotation * Vector3.forward);
		bool flag = Vector3.Distance(base.transform.position, this.vTarget) < 6f;
		if (flag)
		{
			this.flyType = 2;
		}
		bool flag2 = num > this.upangle;
		if (flag2)
		{
			this.flyType = 3;
		}
		base.hero.SetAnimator("Fly", 0, true, 1);
		((TBCharacterThirdMe)base.hero.tChar).VelocitySetZero(false);
		base.hero.tChar.onGround = false;
		base.hero.charMove.state.fly = true;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		this.flyState = 1;
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x00160B98 File Offset: 0x0015ED98
	private void OnGrounded()
	{
		this.flyState = 2;
		base.hero.tChar.moveDirectionNoForce = Vector3.zero;
		base.hero.SetAnimator("Fly", 0, false, 1);
		base.hero.charMove.state.fly = false;
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x00160BF4 File Offset: 0x0015EDF4
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		this.vTarget = Vector3.zero;
		this.vDirect = Vector3.zero;
		this.oldDis = 0f;
		base.hero.SetAnimator("Fly", 0, false, 1);
		base.hero.tChar.onGround = true;
		base.hero.charMove.state.fly = false;
		this.flyType = 0;
		this.flyState = 0;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(0);
		base.hero.SendMessageToServer(31105, arrayList);
	}

	// Token: 0x04002243 RID: 8771
	private Vector3 vTarget = Vector3.zero;

	// Token: 0x04002244 RID: 8772
	private Vector3 vDirect = Vector3.zero;

	// Token: 0x04002245 RID: 8773
	private int flyType;

	// Token: 0x04002246 RID: 8774
	private int flyState;

	// Token: 0x04002247 RID: 8775
	private float oldDis;

	// Token: 0x04002248 RID: 8776
	public float enddis1 = 3f;

	// Token: 0x04002249 RID: 8777
	public float enddis2 = 1f;

	// Token: 0x0400224A RID: 8778
	public float enddis3 = 1f;

	// Token: 0x0400224B RID: 8779
	public float movespeed = 0.4f;

	// Token: 0x0400224C RID: 8780
	public float upforce = 15f;

	// Token: 0x0400224D RID: 8781
	public float frontforce = 4f;

	// Token: 0x0400224E RID: 8782
	public float upangle = 15f;
}
