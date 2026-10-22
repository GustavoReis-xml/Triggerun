using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class CBulletLaserMe : CBulletMe
{
	// Token: 0x06000331 RID: 817 RVA: 0x0001747C File Offset: 0x0001567C
	private void FixedUpdate()
	{
		bool readyDestory = base.readyDestory;
		if (!readyDestory)
		{
			Vector3 vector = base.transform.position;
			float num = 0.1f;
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
				float num2 = base.bulletData.bulletFlySpeed * Time.deltaTime;
				bool flag3 = num2 <= 0f;
				if (!flag3)
				{
					while (!base.haveCollision)
					{
						bool flag4 = base.nowFlyDistance + num2 > base.bulletData.bulletMaxDistance;
						if (flag4)
						{
							num2 = base.bulletData.bulletMaxDistance - base.nowFlyDistance;
						}
						bool flag5 = num2 < 0.01f;
						if (flag5)
						{
							break;
						}
						Ray ray = new Ray(vector, base.transform.forward);
						RaycastHit hitGameObject;
						bool flag6 = TriggerunHitCollision.Raycast(ray, out hitGameObject, num2, base.bulletLayer);
						if (!flag6)
						{
							vector = base.transform.position + base.transform.forward * num2;
							base.transform.position = vector;
							break;
						}
						bool flag7 = hitGameObject.collider.transform.name.Contains("_IgnoreDamge");
						if (!flag7)
						{
							base.transform.position = hitGameObject.point - ray.direction * 0.1f;
							bool flag8 = base.bulletData.bulletDestoryTimeOut > 0f && (base.bulletData.triggerType == 2 || base.bulletData.triggerType == 5);
							if (flag8)
							{
								bool flag9 = (1 << hitGameObject.collider.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
								if (flag9)
								{
									bool flag10 = (1 << hitGameObject.collider.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
									if (flag10)
									{
										base.transform.parent = hitGameObject.collider.transform;
									}
								}
								else
								{
									this.mustDestoryModel = true;
								}
							}
							base.hitGameObject = hitGameObject;
							base.endPoint = base.transform.position;
							base.haveCollision = true;
							base.end = true;
							break;
						}
						float num3 = Vector3.Distance(vector, hitGameObject.point) + num;
						vector = hitGameObject.point + ray.direction * num;
						num2 -= num3;
					}
					base.nowFlyDistance += num2;
					bool flag11 = !base.end && base.nowFlyDistance >= base.bulletData.bulletMaxDistance;
					if (flag11)
					{
						base.endPoint = vector;
						base.end = true;
					}
					bool flag12 = base.end && !base.readyDestory;
					if (flag12)
					{
						base.readyDestory = true;
						this.boom();
						this.destoryBullet();
					}
				}
			}
		}
	}
}
