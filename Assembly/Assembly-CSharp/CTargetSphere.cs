using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class CTargetSphere : CTarget
{
	// Token: 0x0600079E RID: 1950 RVA: 0x000430C8 File Offset: 0x000412C8
	public override void InitTarget()
	{
		base.targetType = 1;
		this._targetLayer = LayerManager.EnemyLayer(base.NetObject.getAttackForce());
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x000430EC File Offset: 0x000412EC
	public override UnityEngine.Object GetTarget()
	{
		return this.GetMuzzleTarget();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00043104 File Offset: 0x00041304
	private Collider GetMuzzleTarget()
	{
		Collider result = null;
		bool flag = this.targetObjs.Count == 0 || Time.time - this.lastFindTime > 10f;
		if (flag)
		{
			this.lastFindTime = Time.time;
			Collider[] array = Physics.OverlapSphere(base.NetObject.thisModel.transform.position, (float)this._targetRange, this._targetLayer);
			Vector3 position = base.NetObject.thisModel.transform.position;
			int blockLayer = LayerManager.getBlockLayer(base.NetObject.nForce);
			foreach (Collider collider in array)
			{
				string tag = collider.tag;
				RaycastHit raycastHit;
				bool flag2 = tag.Contains("can_damage") && Physics.Linecast(new Vector3(position.x, position.y + 2f, position.z), collider.bounds.center, out raycastHit, blockLayer) && raycastHit.collider.transform.tag.Contains("can_damage");
				if (flag2)
				{
					this.targetObjs.Add(collider);
				}
			}
		}
		bool flag3 = this.targetObjs.Count > 0;
		if (flag3)
		{
			result = (Collider)this.targetObjs[0];
			this.targetObjs.RemoveAt(0);
		}
		return result;
	}

	// Token: 0x040005AD RID: 1453
	private int _targetRange = 20;

	// Token: 0x040005AE RID: 1454
	private int _targetLayer;

	// Token: 0x040005AF RID: 1455
	private ArrayList targetObjs = new ArrayList();

	// Token: 0x040005B0 RID: 1456
	private float lastFindTime;
}
