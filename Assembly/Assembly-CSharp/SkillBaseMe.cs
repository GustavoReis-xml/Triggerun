using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class SkillBaseMe : SkillBase
{
	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x06002664 RID: 9828 RVA: 0x0015D86C File Offset: 0x0015BA6C
	protected CPSkillStruct heroSkillData
	{
		get
		{
			bool flag = this._heroSkillData == null;
			if (flag)
			{
				this._heroSkillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
			}
			return this._heroSkillData;
		}
	}

	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x06002665 RID: 9829 RVA: 0x0015D8AF File Offset: 0x0015BAAF
	protected HeroPlayer hero
	{
		get
		{
			return (HeroPlayer)this._hero;
		}
	}

	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x06002666 RID: 9830 RVA: 0x0015D8BC File Offset: 0x0015BABC
	public bool bePlaying
	{
		get
		{
			CPSkillStruct skillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
			bool flag = skillData == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = skillData.beginTime < Fun.GetServerDate();
				result = !flag2;
			}
			return result;
		}
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x0015D90C File Offset: 0x0015BB0C
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this._heroSkillData = this.hero.heroSkill.GetSkillData(base.iSkillIndex);
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x0015D934 File Offset: 0x0015BB34
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		bool flag = this.heroSkillData != null;
		if (flag)
		{
			bool flag2 = this.heroSkillData.m_nSKLongTime > 0;
			if (flag2)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(base.iSkillIndex);
				arrayList.Add(0);
				this.hero.SendMessageToServer(31105, arrayList);
			}
			this.hero.SetAnimator(this.skillData.animation, 0, false, 1);
			((CWeaponControlMe)this.hero.wc).CheckMustFillCartridge();
			this.hero.playerZhunxingMe.ShowSkillFinish(base.iSkillIndex);
			bool flag3 = this.skillData.dokey != "0";
			if (flag3)
			{
				((GamePlayer)this._hero).bePlayActive = true;
			}
		}
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x0015DA24 File Offset: 0x0015BC24
	public virtual void BreakSth(int type)
	{
		((HeroBaseMe)this.hero.heroBase).BreakSth(type);
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x0015DA40 File Offset: 0x0015BC40
	public override bool OneSkillCanDo()
	{
		bool flag = this.hero.wc == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.hero.playerCommon.m_nJiangzhi == 1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.hero.playerCommon.m_nDongjie == 1;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = this.heroSkillData == null;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this.hero.CheckActiveStat();
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = !this.hero.CanHuDong;
							if (flag6)
							{
								result = false;
							}
							else
							{
								PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[base.iSkillIndex];
								for (int i = 0; i < playerSkillData.triggerWeapon.Length; i++)
								{
									bool flag7 = !((CWeaponControlMe)this.hero.wc).Readyed(playerSkillData.triggerWeapon[i]);
									if (flag7)
									{
										return false;
									}
									bool flag8 = !((CWeaponControlMe)this.hero.wc).LauncherPublicCD(playerSkillData.triggerWeapon[i]);
									if (flag8)
									{
										return false;
									}
								}
								result = base.OneSkillCanDo();
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x0015DB90 File Offset: 0x0015BD90
	protected void SkillShoot(object __target)
	{
		bool flag = this.bCanShoot;
		if (flag)
		{
			this.RealShoot(__target, this.hero.hitGamaObj);
		}
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x0015DBC0 File Offset: 0x0015BDC0
	protected void RealShoot(object __target, object hitObj = null)
	{
		bool flag = hitObj == null;
		if (flag)
		{
			hitObj = this.hero.hitGamaObj;
		}
		PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[base.iSkillIndex];
		for (int i = 0; i < playerSkillData.triggerWeapon.Length; i++)
		{
			((CWeaponControlMe)this.hero.wc).WeaponActiveBySkill(playerSkillData.triggerWeapon[i], base.iSkillIndex);
			((CWeaponControlMe)this.hero.wc).WeaponFireBySkill(playerSkillData.triggerWeapon[i], "0", (Vector3)__target, hitObj);
		}
		this.hero.playerZhunxingMe.ShowSkillDo(base.iSkillIndex);
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x0015DC7E File Offset: 0x0015BE7E
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((HeroBaseMe)this.hero.heroBase).doSkillTime = 0f;
		this.SkillShoot(__target);
		((GamePlayer)this._hero).bePlayActive = false;
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x0015DCBE File Offset: 0x0015BEBE
	public virtual void Break_Reload()
	{
		this.hero.SetAnimator("Reload", 0, false, 1);
		((CWeaponControlMe)this.hero.wc).cancelFillCartridge();
	}

	// Token: 0x0400221B RID: 8731
	private CPSkillStruct _heroSkillData;

	// Token: 0x0400221C RID: 8732
	protected bool bCanShoot = true;
}
