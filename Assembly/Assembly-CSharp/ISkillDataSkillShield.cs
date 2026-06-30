using System;

// Token: 0x020001FE RID: 510
public class ISkillDataSkillShield : ISkillData
{
	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x0600164D RID: 5709 RVA: 0x000CF5CC File Offset: 0x000CD7CC
	// (set) Token: 0x0600164E RID: 5710 RVA: 0x000CF5E4 File Offset: 0x000CD7E4
	public bool Shield_Call_State
	{
		get
		{
			return this._Shield_Call_State;
		}
		set
		{
			this._Shield_Call_State = value;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x000CF5F0 File Offset: 0x000CD7F0
	// (set) Token: 0x06001650 RID: 5712 RVA: 0x000CF608 File Offset: 0x000CD808
	public int Shield_Current_Energy
	{
		get
		{
			return this._Shield_Current_Energy;
		}
		set
		{
			this._Shield_Current_Energy = value;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x000CF614 File Offset: 0x000CD814
	// (set) Token: 0x06001652 RID: 5714 RVA: 0x000CF62C File Offset: 0x000CD82C
	public int Shield_Max_Energy
	{
		get
		{
			return this._Shield_Max_Energy;
		}
		set
		{
			this._Shield_Max_Energy = value;
		}
	}

	// Token: 0x04001149 RID: 4425
	private bool _Shield_Call_State;

	// Token: 0x0400114A RID: 4426
	private int _Shield_Current_Energy = 500;

	// Token: 0x0400114B RID: 4427
	private int _Shield_Max_Energy = 10000;
}
