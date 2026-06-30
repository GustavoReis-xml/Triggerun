using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class Npc_UI : MonoBehaviour
{
	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0010AE80 File Offset: 0x00109080
	// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0010AEDE File Offset: 0x001090DE
	public Npc_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Npc_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Npc_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x04001971 RID: 6513
	private Npc_View _View_Component;
}
