using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class CPlayerDazhao : MonoBehaviour
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x0003908C File Offset: 0x0003728C
	public bool canDo
	{
		get
		{
			return this.cdazhaomaininfo != null && this.cdazhaomaininfo.c_energy_now >= this.cdazhaomaininfo.dazhaoxml.denergymax;
		}
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x000390D4 File Offset: 0x000372D4
	public void UpdateFlag(ArrayList arrPam)
	{
		bool flag = this.cdazhaomaininfo == null;
		if (!flag)
		{
			int num = (int)arrPam[0];
			bool flag2 = num != -1;
			if (flag2)
			{
				this.doing = false;
				bool flag3 = num == 1;
				if (flag3)
				{
					this.doing = true;
				}
			}
			this.cdazhaomaininfo.fContinuesTime = (float)((int)arrPam[2]) / 1000f;
			string text = (string)arrPam[1];
			bool flag4 = this.doing;
			if (flag4)
			{
				this.cdazhaomaininfo.fEndTime = Time.time + this.cdazhaomaininfo.fContinuesTime;
			}
			else
			{
				this.cdazhaomaininfo.fEndTime = 0f;
			}
			this.cdazhaomaininfo.c_Enddate = text;
			bool isSingle = fGameManager.IsSingle;
			if (isSingle)
			{
				PlayerLearnManager.GetInstance().OnSuperPower(text);
			}
		}
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x000391B8 File Offset: 0x000373B8
	public void SuperSkillDo(NetLiving player)
	{
		int doskill = this.cdazhaomaininfo.dazhaoxml.doskill;
		bool flag = doskill == 0;
		if (!flag)
		{
			bool flag2 = player.netobject_type != 6;
			if (flag2)
			{
				bool bCanDoSkill = ((HeroBaseMe)player.heroBase).bCanDoSkill;
				if (bCanDoSkill)
				{
					player.heroSkill.DoSkill(doskill, 0);
				}
			}
			else
			{
				bool flag3 = player.heroSkill != null;
				if (flag3)
				{
					player.heroSkill.DoSkill(doskill, 1);
				}
			}
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00039240 File Offset: 0x00037440
	public bool UseGun(NetLiving hero)
	{
		bool flag = !this.doing;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = hero.playerCommon.m_nPlayerState == 0;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.cdazhaomaininfo == null || !this.cdazhaomaininfo.canUseGun;
				result = !flag3;
			}
		}
		return result;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x000392A0 File Offset: 0x000374A0
	public void doShootReset(HeroPlayer hero)
	{
		bool flag = this.cdazhaomaininfo != null;
		if (flag)
		{
			int num = this.cdazhaomaininfo.dazhaoxml.dogun.Length;
			for (int i = 0; i < num; i++)
			{
				((CWeaponControlMe)hero.wc).WeaponDownKey(this.cdazhaomaininfo.dazhaoxml.dogun[i]);
			}
		}
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x00039308 File Offset: 0x00037508
	public void doShoot(NetLiving hero)
	{
		bool flag = this.cdazhaomaininfo != null;
		if (flag)
		{
			int num = this.cdazhaomaininfo.dazhaoxml.dogun.Length;
			for (int i = 0; i < num; i++)
			{
				((CWeaponControlMe)hero.wc).WeaponFire(this.cdazhaomaininfo.dazhaoxml.dogun[i], "0");
			}
		}
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00039374 File Offset: 0x00037574
	public bool weaponReaded(NetLiving hero)
	{
		bool flag = this.cdazhaomaininfo == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = this.cdazhaomaininfo.dazhaoxml.dogun.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag2 = !((CWeaponControlMe)hero.wc).Readyed(this.cdazhaomaininfo.dazhaoxml.dogun[i]);
				if (flag2)
				{
					return false;
				}
				bool flag3 = !((CWeaponControlMe)hero.wc).LauncherPublicCD(this.cdazhaomaininfo.dazhaoxml.dogun[i]);
				if (flag3)
				{
					return false;
				}
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00039424 File Offset: 0x00037624
	public void initDazhao(NetLiving player, int act, ArrayList arrList)
	{
		try
		{
			this.clear();
			this.nActing = act;
			dazhaoData dazhaoData = null;
			bool flag = fGameManager.mloadConfig.AllDazhaoObj.ContainsKey(player.playerCommon.m_nModelIndex);
			if (flag)
			{
				dazhaoData = fGameManager.mloadConfig.AllDazhaoObj[player.playerCommon.m_nModelIndex];
			}
			else
			{
				using (Dictionary<int, dazhaoData>.Enumerator enumerator = fGameManager.mloadConfig.AllDazhaoObj.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<int, dazhaoData> keyValuePair = enumerator.Current;
						dazhaoData = keyValuePair.Value;
					}
				}
			}
			bool flag2 = dazhaoData == null || dazhaoData.dazhaoList == null || dazhaoData.dazhaoList.Count < 1;
			if (!flag2)
			{
				this.cdazhaolist = new List<CDazhaoInfo>();
				int count = arrList.Count;
				for (int i = 0; i < count; i++)
				{
					bool flag3 = i >= dazhaoData.dazhaoList.Count;
					if (flag3)
					{
						break;
					}
					ArrayList arrayList = (ArrayList)arrList[i];
					CDazhaoInfo cdazhaoInfo = new CDazhaoInfo();
					this.cdazhaolist.Add(cdazhaoInfo);
					cdazhaoInfo.cid = (int)arrayList[0];
					cdazhaoInfo.c_energy_now = (int)arrayList[1];
					cdazhaoInfo.c_Enddate = DateTime.MinValue.ToString();
					cdazhaoInfo.dazhaoxml = dazhaoData.dazhaoList[i];
					int doskill = cdazhaoInfo.dazhaoxml.doskill;
					bool flag4 = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(doskill);
					if (flag4)
					{
						PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[doskill];
						bool flag5 = playerSkillData.dokey != "0";
						if (flag5)
						{
							Debug.LogError(string.Format("大招({0})的技能({1})不能设置通过按键来释放技能，请删除技能配表里的doKey。", cdazhaoInfo.dazhaoxml.dname, doskill));
						}
					}
					bool flag6 = cdazhaoInfo.dazhaoxml.dogun.Length != 0;
					if (flag6)
					{
						cdazhaoInfo.canUseGun = true;
					}
				}
				bool flag7 = this.nActing >= 0 && this.nActing < this.cdazhaolist.Count;
				if (flag7)
				{
					this.cdazhaomaininfo = this.cdazhaolist[this.nActing];
				}
				else
				{
					bool flag8 = this.cdazhaolist.Count > 0;
					if (flag8)
					{
						this.cdazhaomaininfo = this.cdazhaolist[0];
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("INIT DAZHAO EXCEPTION: " + ex.ToString());
		}
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x00039700 File Offset: 0x00037900
	public void EnergyUp(int _type, int _value, int nReason)
	{
		bool flag = this.cdazhaomaininfo != null;
		if (flag)
		{
			this.cdazhaomaininfo.c_energy_now = _value;
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0003972C File Offset: 0x0003792C
	public float GetDaZhaoValue()
	{
		float num = 0f;
		bool flag = this.cdazhaomaininfo == null;
		float result;
		if (flag)
		{
			result = num;
		}
		else
		{
			result = (float)this.cdazhaomaininfo.c_energy_now / (float)this.cdazhaomaininfo.dazhaoxml.denergymax;
		}
		return result;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00039774 File Offset: 0x00037974
	private void clear()
	{
		bool flag = this.cdazhaolist != null;
		if (flag)
		{
			this.cdazhaolist.Clear();
		}
		this.nActing = -1;
		this.doing = false;
		this.cdazhaomaininfo = null;
	}

	// Token: 0x040004CC RID: 1228
	private List<CDazhaoInfo> cdazhaolist;

	// Token: 0x040004CD RID: 1229
	private int nActing = -1;

	// Token: 0x040004CE RID: 1230
	public bool doing;

	// Token: 0x040004CF RID: 1231
	public CDazhaoInfo cdazhaomaininfo;

	// Token: 0x040004D0 RID: 1232
	public Npc_Entity superNpc;
}
