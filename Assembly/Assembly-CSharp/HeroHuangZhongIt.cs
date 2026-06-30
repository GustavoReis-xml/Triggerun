using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class HeroHuangZhongIt : HeroBaseIt
{
	// Token: 0x060011FD RID: 4605 RVA: 0x000A9408 File Offset: 0x000A7608
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.buffhz = base.hero.userModelTps.transform.FindChild("buff_hz");
		int childCount = this.buffhz.childCount;
		this.PowerBuffs = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			this.PowerBuffs[i] = this.buffhz.GetChild(i).gameObject;
		}
		this.initFinish = true;
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000A9487 File Offset: 0x000A7687
	public override void SuperSkillDo(object parm)
	{
		this.b_superState = true;
		base.StartCoroutine(this.superStateFinish());
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000A949E File Offset: 0x000A769E
	private IEnumerator superStateFinish()
	{
		yield return new WaitForSeconds(5f);
		this.b_superState = false;
		yield break;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000A94AD File Offset: 0x000A76AD
	public override void WeaponFired(bool fired)
	{
		base.WeaponFired(fired);
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000A94B8 File Offset: 0x000A76B8
	protected override void Update()
	{
		base.Update();
		this.reducePower();
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000A94CC File Offset: 0x000A76CC
	public override void beDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.beDamage(szDamage, szHp);
		ArrayList arrayList = new ArrayList();
		arrayList = (ArrayList)szDamage[15];
		bool flag = arrayList.Count > 0 && !base.hero.isDead;
		if (flag)
		{
			this.D_Power = (int)arrayList[1];
			this.SetLevelColor();
		}
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000A9530 File Offset: 0x000A7730
	private void SetLevelColor()
	{
		bool flag = base.hero.haveDestroy || !base.hero.playerHaveCreate || this.PowerBuffs.Length == 0 || !this.initFinish;
		if (!flag)
		{
			bool flag2 = this.D_Power > 80;
			if (flag2)
			{
				bool flag3 = !this.PowerBuffs[3].activeSelf;
				if (flag3)
				{
					this.MakeAlllevelColorDisable();
					this.PowerBuffs[3].SetActive(true);
				}
			}
			else
			{
				bool flag4 = this.D_Power > 60;
				if (flag4)
				{
					bool flag5 = !this.PowerBuffs[2].activeSelf;
					if (flag5)
					{
						this.MakeAlllevelColorDisable();
						this.PowerBuffs[2].SetActive(true);
					}
				}
				else
				{
					bool flag6 = this.D_Power > 40;
					if (flag6)
					{
						bool flag7 = !this.PowerBuffs[1].activeSelf;
						if (flag7)
						{
							this.MakeAlllevelColorDisable();
							this.PowerBuffs[1].SetActive(true);
						}
					}
					else
					{
						bool flag8 = this.D_Power > 20;
						if (flag8)
						{
							bool flag9 = !this.PowerBuffs[0].activeSelf;
							if (flag9)
							{
								this.MakeAlllevelColorDisable();
								this.PowerBuffs[0].SetActive(true);
							}
						}
						else
						{
							this.MakeAlllevelColorDisable();
						}
					}
				}
			}
		}
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x000A9688 File Offset: 0x000A7888
	private void MakeAlllevelColorDisable()
	{
		int num = 0;
		while (num < this.PowerBuffs.Length && !(this.PowerBuffs[num] == null))
		{
			bool activeSelf = this.PowerBuffs[num].activeSelf;
			if (activeSelf)
			{
				this.PowerBuffs[num].SetActive(false);
			}
			num++;
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000A96E8 File Offset: 0x000A78E8
	private void reducePower()
	{
		bool flag = this.b_superState;
		if (!flag)
		{
			bool flag2 = this.reduceGap > 0f;
			if (flag2)
			{
				this.reduceGap -= Time.deltaTime;
			}
			else
			{
				this.reduceGap = 0.5f;
				bool flag3 = this.D_Power > 0;
				if (flag3)
				{
					this.D_Power--;
					bool flag4 = !base.hero.isDead;
					if (flag4)
					{
						this.SetLevelColor();
					}
				}
			}
		}
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x000A976C File Offset: 0x000A796C
	public override void OnDeath()
	{
		base.OnDeath();
		this.D_Power = 0;
		this.SetLevelColor();
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000A9784 File Offset: 0x000A7984
	public override void SetFilmMaterial(bool show)
	{
		bool flag = this.buffhz != null;
		if (flag)
		{
			this.buffhz.gameObject.SetActive(!show);
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000A97B9 File Offset: 0x000A79B9
	public override void Revive()
	{
		base.Revive();
		this.D_Power = 0;
		this.SetLevelColor();
	}

	// Token: 0x04000E46 RID: 3654
	private Animator m_anim;

	// Token: 0x04000E47 RID: 3655
	private GameObject[] PowerBuffs;

	// Token: 0x04000E48 RID: 3656
	private int D_Power;

	// Token: 0x04000E49 RID: 3657
	private float reduceGap;

	// Token: 0x04000E4A RID: 3658
	private Transform buffhz;

	// Token: 0x04000E4B RID: 3659
	private bool initFinish;

	// Token: 0x04000E4C RID: 3660
	private bool b_superState;
}
