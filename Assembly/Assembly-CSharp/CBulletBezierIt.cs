using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class CBulletBezierIt : CBulletIt
{
	// Token: 0x060002EB RID: 747 RVA: 0x00015C7C File Offset: 0x00013E7C
	public void CreateBezier(int no, Vector3 dir, float rot, int _bezierRate)
	{
		List<Vector3> oneBezier = CBulletBezierData.getOneBezier(no);
		Quaternion rotation = Quaternion.LookRotation(dir);
		Quaternion rotation2 = Quaternion.Euler(0f, 0f, rot);
		this.movePoint = new List<Vector3>();
		Vector3 position = base.transform.position;
		float num = (float)_bezierRate / 10000f + 1f;
		for (int i = 0; i < oneBezier.Count; i++)
		{
			Vector3 vector = rotation2 * oneBezier[i];
			vector.x *= num;
			vector.y *= num;
			vector = rotation * vector + position;
			this.movePoint.Add(vector);
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00015D38 File Offset: 0x00013F38
	private void FixedUpdate()
	{
		bool readyDestory = base.readyDestory;
		if (!readyDestory)
		{
			bool flag = base.muzzle == null;
			if (flag)
			{
				base.end = true;
			}
			bool end = base.end;
			if (end)
			{
				bool flag2 = !base.readyDestory;
				if (flag2)
				{
					base.readyDestory = true;
					this.boom();
					this.destoryBullet();
				}
			}
			else
			{
				bool flag3 = this.movePoint == null;
				if (!flag3)
				{
					this.checkDelayBoom();
					this.addTime += Time.deltaTime;
					this.nowPoint = base.bulletData.bulletFlySpeed * this.addTime;
					float t = this.nowPoint / CBulletBezierData.maxLong;
					float num = Mathf.Lerp(0f, CBulletBezierData.maxLong - 1f, t);
					int num2 = (int)num;
					float t2 = num - (float)num2;
					int num3 = num2 + 1;
					bool flag4 = num3 >= this.movePoint.Count;
					if (flag4)
					{
						num3 = num2;
						base.end = true;
					}
					Vector3 vector = Vector3.Lerp(this.movePoint[num2], this.movePoint[num3], t2);
					Vector3 normalized = (vector - base.transform.localPosition).normalized;
					float maxDistance = Vector3.Distance(vector, base.transform.localPosition);
					Ray ray = new Ray(base.transform.localPosition, normalized);
					RaycastHit hitGameObject;
					bool flag5 = Physics.Raycast(ray, out hitGameObject, maxDistance, base.bulletLayer);
					if (flag5)
					{
						base.transform.localPosition = hitGameObject.point;
						bool flag6 = base.bulletData.bulletDestoryTimeOut > 0f && (base.bulletData.triggerType == 2 || base.bulletData.triggerType == 5);
						if (flag6)
						{
							bool flag7 = (1 << hitGameObject.collider.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
							if (flag7)
							{
								bool flag8 = (1 << hitGameObject.collider.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
								if (flag8)
								{
									base.transform.parent = hitGameObject.transform;
								}
							}
							else
							{
								this.mustDestoryModel = true;
							}
						}
						base.hitGameObject = hitGameObject;
						base.endPoint = hitGameObject.point;
						base.haveCollision = true;
						base.end = true;
					}
					else
					{
						base.transform.localPosition = vector;
					}
				}
			}
		}
	}

	// Token: 0x0400029E RID: 670
	private float addTime;

	// Token: 0x0400029F RID: 671
	private float nowPoint;

	// Token: 0x040002A0 RID: 672
	private List<Vector3> movePoint;
}
