using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class CBulletNormalIt : CBulletIt
{
	// Token: 0x0600034C RID: 844 RVA: 0x00018FA0 File Offset: 0x000171A0
	private void FixedUpdate()
	{
		bool readyDestory = base.readyDestory;
		if (!readyDestory)
		{
			Vector3 vector = base.transform.position;
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
					base.end = true;
					base.readyDestory = true;
					this.boom();
					this.destoryBullet();
				}
			}
			else
			{
				float num = base.bulletData.bulletFlySpeed * Time.deltaTime;
				bool flag3 = num <= 0f;
				if (!flag3)
				{
					bool flag4 = !base.haveCollision;
					if (flag4)
					{
						bool flag5 = base.nowFlyDistance + num > base.bulletData.bulletMaxDistance;
						if (flag5)
						{
							num = base.bulletData.bulletMaxDistance - base.nowFlyDistance;
						}
						bool flag6 = num >= 0.01f;
						if (flag6)
						{
							Ray ray = new Ray(vector, base.transform.forward);
							RaycastHit hitGameObject;
							bool flag7 = Physics.Raycast(ray, out hitGameObject, num, base.bulletLayer);
							if (flag7)
							{
								base.transform.position = hitGameObject.point;
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
								base.endPoint = hitGameObject.point;
								base.haveCollision = true;
								base.end = true;
							}
							else
							{
								vector = base.transform.position + base.transform.forward * num;
								base.transform.position = vector;
							}
						}
					}
					base.nowFlyDistance += num;
					bool flag11 = !base.end && base.nowFlyDistance >= base.bulletData.bulletMaxDistance;
					if (flag11)
					{
						base.endPoint = vector;
						base.end = true;
					}
				}
			}
		}
	}
}
