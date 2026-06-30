using System;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class TBHitReaction : HitReaction
{
	// Token: 0x06002C00 RID: 11264 RVA: 0x00181AEC File Offset: 0x0017FCEC
	protected override void Start()
	{
		base.Start();
		HitReaction.HitPointEffector[] effectorHitPoints = this.effectorHitPoints;
		foreach (HitReaction.HitPointEffector hitPointEffector in effectorHitPoints)
		{
			bool flag = !this.colliderDict.ContainsKey(hitPointEffector.collider.name);
			if (flag)
			{
				this.colliderDict.Add(hitPointEffector.collider.name, hitPointEffector.collider);
			}
		}
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x00181B60 File Offset: 0x0017FD60
	public void Hit(string name, Vector3 force)
	{
		bool flag = this.colliderDict.ContainsKey(name);
		if (flag)
		{
			Collider collider = this.colliderDict[name];
			base.Hit(collider, force, collider.bounds.center);
		}
	}

	// Token: 0x04002571 RID: 9585
	public Dictionary<string, Collider> colliderDict = new Dictionary<string, Collider>();
}
