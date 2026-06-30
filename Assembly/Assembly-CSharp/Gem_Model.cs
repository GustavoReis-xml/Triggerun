using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class Gem_Model : MonoBehaviour
{
	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000DCF RID: 3535 RVA: 0x000833E0 File Offset: 0x000815E0
	// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0008343E File Offset: 0x0008163E
	public Gem_Controller Controller_Component
	{
		get
		{
			bool flag = this._Controller_Component == null;
			if (flag)
			{
				this._Controller_Component = base.gameObject.GetComponent<Gem_Controller>();
				bool flag2 = this._Controller_Component == null;
				if (flag2)
				{
					this._Controller_Component = base.gameObject.AddComponent<Gem_Controller>();
				}
			}
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x04000B4E RID: 2894
	private Gem_Controller _Controller_Component;
}
