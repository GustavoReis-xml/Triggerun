using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class Npc_Data : MonoBehaviour
{
	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0010788C File Offset: 0x00105A8C
	// (set) Token: 0x06001C25 RID: 7205 RVA: 0x001078A4 File Offset: 0x00105AA4
	public Npc_Info Info_Component
	{
		get
		{
			return this._Info_Component;
		}
		set
		{
			this._Info_Component = value;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06001C26 RID: 7206 RVA: 0x001078B0 File Offset: 0x00105AB0
	// (set) Token: 0x06001C27 RID: 7207 RVA: 0x001078C8 File Offset: 0x00105AC8
	public Npc_Entity Entity_Component
	{
		get
		{
			return this._Entity_Component;
		}
		set
		{
			this._Entity_Component = value;
		}
	}

	// Token: 0x04001927 RID: 6439
	private Npc_Info _Info_Component;

	// Token: 0x04001928 RID: 6440
	private Npc_Entity _Entity_Component;
}
