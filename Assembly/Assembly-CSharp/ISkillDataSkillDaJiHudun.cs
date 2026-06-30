using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
public class ISkillDataSkillDaJiHudun : ISkillData
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06001643 RID: 5699 RVA: 0x000CF508 File Offset: 0x000CD708
	// (set) Token: 0x06001644 RID: 5700 RVA: 0x000CF520 File Offset: 0x000CD720
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

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06001645 RID: 5701 RVA: 0x000CF52C File Offset: 0x000CD72C
	// (set) Token: 0x06001646 RID: 5702 RVA: 0x000CF544 File Offset: 0x000CD744
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

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06001647 RID: 5703 RVA: 0x000CF550 File Offset: 0x000CD750
	// (set) Token: 0x06001648 RID: 5704 RVA: 0x000CF568 File Offset: 0x000CD768
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

	// Token: 0x04001144 RID: 4420
	private bool _Shield_Call_State;

	// Token: 0x04001145 RID: 4421
	public GameObject _playerShieldSoundContainer;

	// Token: 0x04001146 RID: 4422
	private int _Shield_Current_Energy = 500;

	// Token: 0x04001147 RID: 4423
	private int _Shield_Max_Energy = 10000;
}
