using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class CBulletThrowStayTimeMe : CBulletMe
{
	// Token: 0x06000375 RID: 885 RVA: 0x0001A812 File Offset: 0x00018A12
	private void FixedUpdate()
	{
		this.checkDelayBoom();
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001A81C File Offset: 0x00018A1C
	private void OnTriggerStay(Collider collision)
	{
		bool flag = !collision.gameObject.tag.Contains("can_damage_");
		if (!flag)
		{
			int num = 1 << collision.gameObject.layer;
			bool flag2 = (num & base.bulletLayerNoneBuild) == 0;
			if (!flag2)
			{
				float time = Time.time;
				bool flag3 = time > this.ctime;
				if (flag3)
				{
					this.damage();
					this.colliderDict = new Dictionary<int, Collider>();
					this.colliderDictPos = new Dictionary<int, Vector3>();
					this.ctime = time + ((CBulletThrowData)base.bulletData).checkTime;
				}
				bool flag4 = !(collision.transform.root.name == base.bulletOwnerName);
				if (flag4)
				{
					int instanceID = collision.GetInstanceID();
					bool flag5 = !this.colliderDict.ContainsKey(instanceID);
					if (flag5)
					{
						this._hitPoint = collision.transform.position;
						this.colliderDict.Add(instanceID, collision);
						this.colliderDictPos.Add(instanceID, this._hitPoint);
					}
				}
			}
		}
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001A938 File Offset: 0x00018B38
	private void damage()
	{
		bool flag = this.colliderDict.Count == 0;
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			foreach (int key in this.colliderDict.Keys)
			{
				Collider collider = this.colliderDict[key];
				bool flag2 = !(collider == null) && !(collider.gameObject == null);
				if (flag2)
				{
					ArrayList arrayList3 = base.damageGetData(collider.gameObject, true);
					bool flag3 = arrayList3.Count > 0 && (arrayList.IndexOf((int)arrayList3[1]) == -1 || (int)arrayList3[2] == 2);
					if (flag3)
					{
						arrayList2.Add(arrayList3);
						arrayList.Add((int)arrayList3[1]);
					}
				}
			}
			base.sendDamageToServer(arrayList2);
		}
	}

	// Token: 0x040002CD RID: 717
	private Dictionary<int, Collider> colliderDict = new Dictionary<int, Collider>();

	// Token: 0x040002CE RID: 718
	private Dictionary<int, Vector3> colliderDictPos = new Dictionary<int, Vector3>();

	// Token: 0x040002CF RID: 719
	private float ctime;

	// Token: 0x040002D0 RID: 720
	private bool initDict;
}
