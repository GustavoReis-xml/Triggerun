using System;

// Token: 0x02000319 RID: 793
public class SkillBaseIt : SkillBase
{
	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x06002661 RID: 9825 RVA: 0x0015D84D File Offset: 0x0015BA4D
	public GamePlayer gameplayer
	{
		get
		{
			return (GamePlayer)this._hero;
		}
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x06002662 RID: 9826 RVA: 0x0015D85A File Offset: 0x0015BA5A
	protected NetLiving hero
	{
		get
		{
			return this._hero;
		}
	}
}
