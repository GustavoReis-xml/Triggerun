using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class HeroLuoLiIt : HeroBaseIt
{
	// Token: 0x06001361 RID: 4961 RVA: 0x000B8A0B File Offset: 0x000B6C0B
	public override void OnDeath()
	{
		base.OnDeath();
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000B8A18 File Offset: 0x000B6C18
	protected override void Update()
	{
		base.Update();
		int deadMemberNum = this.GetDeadMemberNum();
		bool flag = deadMemberNum != this.canReviveNum;
		if (flag)
		{
			this.canReviveNum = deadMemberNum;
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.OnFriendDeadNum(this.canReviveNum);
			}
		}
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000B8A74 File Offset: 0x000B6C74
	public void SetDeadMember(NetLiving gp, int ntype)
	{
		bool flag = !gp.bePlayer;
		if (!flag)
		{
			bool flag2 = this.szDeadMember.Contains(gp);
			bool flag3 = ntype == 1;
			if (flag3)
			{
				bool flag4 = !flag2;
				if (flag4)
				{
					this.szDeadMember.Add(gp);
				}
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					this.szDeadMember.Remove(gp);
				}
			}
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x000B8AD8 File Offset: 0x000B6CD8
	private int GetDeadMemberNum()
	{
		int num = 0;
		bool flag = this.szDeadMember.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = !base.hero.playerDazhao.canDo;
			if (flag2)
			{
				result = num;
			}
			else
			{
				bool isDead = base.hero.isDead;
				if (isDead)
				{
					result = num;
				}
				else
				{
					for (int i = 0; i < this.szDeadMember.Count; i++)
					{
						bool flag3 = this.szDeadMember[i] != null && !(this.szDeadMember[i].thisModel == null);
						if (flag3)
						{
							int num2 = Mathf.FloorToInt(Vector3.Distance(this.szDeadMember[i].userBip.transform.position, base.transform.position));
							bool flag4 = num2 <= 20;
							if (flag4)
							{
								num++;
							}
						}
					}
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x04000F13 RID: 3859
	private int canReviveNum;

	// Token: 0x04000F14 RID: 3860
	private List<NetLiving> szDeadMember = new List<NetLiving>();
}
