using System;
using System.Collections;

// Token: 0x020001D7 RID: 471
public class Hero_Message
{
	// Token: 0x06001577 RID: 5495 RVA: 0x000C9340 File Offset: 0x000C7540
	public void GuanyinHandle(int netId, ArrayList szNetMsg)
	{
		bool flag = netId == 2040001;
		if (flag)
		{
			this.SetJiangZhiData((int)szNetMsg[0], (int)szNetMsg[1]);
		}
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x000C937C File Offset: 0x000C757C
	private void SetJiangZhiData(int serId, int nJiangzhi)
	{
		NetLiving netLiving = Fun.Get_NetLiving(serId);
		bool flag = netLiving != null && !netLiving.haveDestroy;
		if (flag)
		{
			netLiving.playerCommon.m_nJiangzhi = nJiangzhi;
			bool flag2 = nJiangzhi == 1;
			if (flag2)
			{
				netLiving.renderBianSe.InvincibleMat(true);
			}
			else
			{
				netLiving.renderBianSe.InvincibleMat(false);
			}
		}
	}
}
