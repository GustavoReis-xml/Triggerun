using System;

// Token: 0x02000295 RID: 661
public class PlayerInfo : PublicInfo
{
	// Token: 0x06001DFD RID: 7677 RVA: 0x00117ED3 File Offset: 0x001160D3
	public override void InitInfo(NetLiving _net)
	{
		base.InitInfo(_net);
		this.checkIK = true;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x00117EE5 File Offset: 0x001160E5
	protected override void Update()
	{
		base.Update();
		this.CheckDisIK();
	}

	// Token: 0x020005F6 RID: 1526
	public enum ACTION_STAT
	{
		// Token: 0x04002ED5 RID: 11989
		dead,
		// Token: 0x04002ED6 RID: 11990
		stand,
		// Token: 0x04002ED7 RID: 11991
		walk,
		// Token: 0x04002ED8 RID: 11992
		run,
		// Token: 0x04002ED9 RID: 11993
		attack,
		// Token: 0x04002EDA RID: 11994
		stand1,
		// Token: 0x04002EDB RID: 11995
		skill,
		// Token: 0x04002EDC RID: 11996
		stand2,
		// Token: 0x04002EDD RID: 11997
		stand3,
		// Token: 0x04002EDE RID: 11998
		stand4,
		// Token: 0x04002EDF RID: 11999
		hurt,
		// Token: 0x04002EE0 RID: 12000
		none,
		// Token: 0x04002EE1 RID: 12001
		other,
		// Token: 0x04002EE2 RID: 12002
		fall_ground
	}
}
