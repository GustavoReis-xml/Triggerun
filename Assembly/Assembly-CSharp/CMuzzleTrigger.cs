using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class CMuzzleTrigger : MonoBehaviour
{
	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600065A RID: 1626 RVA: 0x000355E7 File Offset: 0x000337E7
	public CMuzzle Muzzle
	{
		get
		{
			return this._muzzle;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600065B RID: 1627 RVA: 0x000355F0 File Offset: 0x000337F0
	// (set) Token: 0x0600065C RID: 1628 RVA: 0x00035608 File Offset: 0x00033808
	public Collider targetCollider
	{
		get
		{
			return this._targetCollider;
		}
		set
		{
			this._targetCollider = value;
		}
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00035612 File Offset: 0x00033812
	public void InitTrigger(CMuzzle muzzle)
	{
		this._muzzle = muzzle;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0003561C File Offset: 0x0003381C
	private void OnTriggerStay(Collider collision)
	{
		bool flag = Input_Manager.Get_Button_Up("WeaponFirst");
		if (flag)
		{
			this._targetCollider = null;
			bool flag2 = this._muzzle != null;
			if (flag2)
			{
				this._muzzle.targetObject = null;
			}
		}
		bool flag3 = Input_Manager.Get_Button("WeaponFirst") && this._targetCollider == null && collision.tag.Contains("can_damage_");
		if (flag3)
		{
			this._targetCollider = collision;
		}
	}

	// Token: 0x04000493 RID: 1171
	private CMuzzle _muzzle;

	// Token: 0x04000494 RID: 1172
	private Collider _targetCollider;
}
