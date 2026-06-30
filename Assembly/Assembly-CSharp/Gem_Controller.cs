using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class Gem_Controller : MonoBehaviour
{
	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00082860 File Offset: 0x00080A60
	// (set) Token: 0x06000D88 RID: 3464 RVA: 0x000828BE File Offset: 0x00080ABE
	public Gem_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Gem_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Gem_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000828C8 File Offset: 0x00080AC8
	// (set) Token: 0x06000D8A RID: 3466 RVA: 0x00082926 File Offset: 0x00080B26
	public Gem_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Gem_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Gem_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x04000B2E RID: 2862
	private Gem_View _View_Component;

	// Token: 0x04000B2F RID: 2863
	private Gem_Model _Model_Component;
}
