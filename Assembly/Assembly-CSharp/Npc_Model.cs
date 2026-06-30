using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class Npc_Model : MonoBehaviour
{
	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0010AD30 File Offset: 0x00108F30
	// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x0010AD48 File Offset: 0x00108F48
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

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0010AD54 File Offset: 0x00108F54
	// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0010AD9B File Offset: 0x00108F9B
	public Npc_AI_Entity Entity_AI_Component
	{
		get
		{
			bool flag = this._Entity_AI_Component == null;
			if (flag)
			{
				this._Entity_AI_Component = fGameManager.mloadConfig._Npc_AI_Xml_Data[this.Entity_Component.AI_ID];
			}
			return this._Entity_AI_Component;
		}
		set
		{
			this._Entity_AI_Component = value;
		}
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x0010ADA8 File Offset: 0x00108FA8
	public Npc_Entity Get_Entity()
	{
		return this.Entity_Component;
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0010ADC0 File Offset: 0x00108FC0
	public GameObject Get_Model_Object()
	{
		return this.Entity_Component.UserControl;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0010ADE0 File Offset: 0x00108FE0
	public Npc_AI_Entity Get_Npc_AI()
	{
		return this.Entity_AI_Component;
	}

	// Token: 0x0400196F RID: 6511
	private Npc_Entity _Entity_Component;

	// Token: 0x04001970 RID: 6512
	private Npc_AI_Entity _Entity_AI_Component;
}
