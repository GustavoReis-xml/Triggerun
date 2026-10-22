using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020F RID: 527
public class Landmine_Controller : Npc_Controller
{
	// Token: 0x170002CA RID: 714
	// (get) Token: 0x0600170B RID: 5899 RVA: 0x000D6738 File Offset: 0x000D4938
	// (set) Token: 0x0600170C RID: 5900 RVA: 0x000D6750 File Offset: 0x000D4950
	public bool Fire_State
	{
		get
		{
			return this._Fire_State;
		}
		set
		{
			this._Fire_State = value;
		}
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000D675C File Offset: 0x000D495C
	public override void Detection()
	{
		if (base.Entity_Component != null && base.Entity_Component.Index == 1003)
		{
			bool isMain = base.Entity_Component.Main && !base.Entity_Component.Death_State && !this.Fire_State;
			if (isMain)
			{
				base.StartCoroutine(this.Wait_Suicide());
			}
			return;
		}
		GameObject x = null;
		bool flag = !base.Entity_Component.Main || base.Entity_Component.Detection_Controller_Component == null || base.Entity_Component.Death_State || base.Entity_Component.thisModel == null || this.Fire_State;
		if (!flag)
		{
			Dictionary<int, GameObject> dictionary = base.Entity_Component.Detection_Controller_Component.Range_Detection(base.Entity_Component.Eye_Object.transform.position);
			List<int> list = new List<int>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int key = list[i];
				x = dictionary[key];
				bool flag2 = x == null;
				if (flag2)
				{
				}
			}
			bool flag3 = !(x == null);
			if (flag3)
			{
				base.StartCoroutine(this.Wait_Suicide());
			}
		}
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000D6855 File Offset: 0x000D4A55
	private IEnumerator Wait_Suicide()
	{
		ArrayList _ArrayList = null;
		this.Fire_State = true;
		_ArrayList = new ArrayList
		{
			base.Entity_Component.ID,
			0
		};
		bool flag = base.Entity_Component.Synchronization_Object != null;
		if (flag)
		{
			base.Entity_Component.Synchronization_Object.SendMessageToServer(1010006, _ArrayList);
		}
		base.Death_Effect_Object = null;
		yield return new WaitForSeconds(base.Entity_Component.Time);
		bool canFire = base.Entity_Component != null && base.Entity_Component.wc != null && base.Entity_Component.UserControl != null;
		if (canFire)
		{
			((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", base.Entity_Component.UserControl.transform.position, base.Entity_Component.UserControl);
		}
		yield return new WaitForSeconds(0.2f);
		this.Fire_State = false;
		if (base.Entity_Component != null)
		{
			base.Entity_Component.Suicide();
		}
		yield break;
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000D6864 File Offset: 0x000D4A64
	public override void Attack_Start()
	{
		base.Entity_Component.Show_Locked = 0;
		Fun.Change_Collider(base.Entity_Component.Container, false);
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000D6888 File Offset: 0x000D4A88
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x04001247 RID: 4679
	private bool _Fire_State;
}
