using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class CBulletThrowMe : CBulletMe
{
	// Token: 0x0600036E RID: 878 RVA: 0x00019F30 File Offset: 0x00018130
	private void FixedUpdate()
	{
		this.checkDelayBoom();
		this.prevPosition[1] = this.prevPosition[0];
		this.prevPosition[0] = this.allRig[0].position;
		bool flag = !base.readyDestory && ((CBulletThrowData)base.bulletData).triggerType == 4 && this.allRig.Length != 0 && this.allRig[0].IsSleeping();
		if (flag)
		{
			for (int i = 0; i < this.allRig.Length; i++)
			{
				this.allRig[i].velocity = Vector3.zero;
				this.allRig[i].angularVelocity = Vector3.zero;
			}
			base.hitVelocity = this.allRig[0].transform.up;
			base.hitPhysical = -base.hitVelocity;
			base.end = true;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001A04C File Offset: 0x0001824C
	private void OnCollisionCheck(Collider collision)
	{
		bool flag = base.muzzle == null || base.muzzle.launcher.NetObject == null || base.muzzle.launcher.NetObject.haveDestroy;
		if (flag)
		{
			bool flag2 = !base.readyDestory;
			if (flag2)
			{
				base.end = true;
				base.readyDestory = true;
				this.destoryBullet();
			}
		}
		else
		{
			bool flag3 = base.readyDestory || collision.transform.root.name == base.bulletOwnerName;
			if (!flag3)
			{
				bool flag4 = ((CBulletThrowData)base.bulletData).bulletThrough == 1 && collision.tag.Contains("can_damage");
				if (flag4)
				{
					this.throughDamage(collision);
				}
				bool flag5 = ((CBulletThrowData)base.bulletData).triggerType == 1;
				if (flag5)
				{
					base.hitCollider = collision;
					for (int i = 0; i < this.allRig.Length; i++)
					{
						this.allRig[i].velocity = Vector3.zero;
						this.allRig[i].angularVelocity = Vector3.zero;
					}
					base.transform.position = this._hitPoint + base.hitVelocity * 0.1f;
					base.end = true;
					base.readyDestory = true;
					this.boom();
					this.destoryBullet();
				}
				else
				{
					bool flag6 = ((CBulletThrowData)base.bulletData).triggerType == 2;
					if (flag6)
					{
						for (int j = 0; j < this.allRig.Length; j++)
						{
							this.allRig[j].velocity = Vector3.zero;
							this.allRig[j].angularVelocity = Vector3.zero;
						}
						base.hitCollider = collision;
						base.transform.position = this._hitPoint + base.hitVelocity * 0.1f;
						bool flag7 = base.bulletData.bulletDestoryTimeOut > 0f;
						if (flag7)
						{
							bool flag8 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
							if (flag8)
							{
								bool flag9 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
								if (flag9)
								{
									base.transform.parent = collision.transform;
								}
							}
							else
							{
								this.mustDestoryModel = true;
							}
						}
						base.end = true;
						base.readyDestory = true;
						this.destoryBullet();
					}
					else
					{
						bool flag10 = ((CBulletThrowData)base.bulletData).triggerType == 4;
						if (flag10)
						{
							base.hitCollider = collision;
							float num = Vector3.Dot(base.hitVelocity, Vector3.up);
							bool flag11 = num > 0.45f;
							if (flag11)
							{
								for (int k = 0; k < this.allRig.Length; k++)
								{
									this.allRig[k].velocity = Vector3.zero;
									this.allRig[k].angularVelocity = Vector3.zero;
								}
								base.end = true;
								base.readyDestory = true;
								this.boom();
								this.destoryBullet();
							}
						}
						else
						{
							bool flag12 = ((CBulletThrowData)base.bulletData).triggerType != 5;
							if (!flag12)
							{
								for (int l = 0; l < this.allRig.Length; l++)
								{
									this.allRig[l].velocity = Vector3.zero;
									this.allRig[l].angularVelocity = Vector3.zero;
								}
								base.hitCollider = collision;
								base.transform.position = this._hitPoint;
								bool flag13 = base.bulletData.bulletDestoryTimeOut > 0f;
								if (flag13)
								{
									bool flag14 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
									if (flag14)
									{
										bool flag15 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
										if (flag15)
										{
											base.transform.parent = collision.transform;
										}
									}
									else
									{
										this.mustDestoryModel = true;
									}
								}
								base.end = true;
								base.readyDestory = true;
								this.boom();
								this.destoryBullet();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001A4D8 File Offset: 0x000186D8
	private void OnCollisionEnter(Collision collision)
	{
		bool readyDestory = base.readyDestory;
		if (!readyDestory)
		{
			this._hitPoint = collision.contacts[0].point;
			base.hitVelocity = collision.contacts[0].normal;
			Collider collider = collision.collider;
			bool flag = collider.gameObject.layer != 0;
			if (flag)
			{
				Vector3 vector = this.prevPosition[0];
				bool flag2 = false;
				Vector3 a = this._hitPoint;
				float num = Vector3.Distance(vector, this._hitPoint);
				bool flag3 = num >= 0.01f;
				if (flag3)
				{
					Vector3 normalized = (this._hitPoint - vector).normalized;
					foreach (RaycastHit raycastHit in Physics.RaycastAll(vector, normalized, num, this._blockLayer))
					{
						float num2 = Vector3.Distance(raycastHit.point, vector);
						bool flag4 = num2 < num;
						if (flag4)
						{
							num = num2;
							flag2 = true;
							a = raycastHit.point;
							collider = raycastHit.collider;
						}
					}
				}
				bool flag5 = flag2 && base.bulletData.bulletBoomBlock;
				if (flag5)
				{
					this._hitPoint = a + base.hitVelocity * 0.1f;
				}
			}
			base.hitPhysical = -base.hitVelocity;
			this.OnCollisionCheck(collider);
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001A658 File Offset: 0x00018858
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = !base.readyDestory && base.bulletData.bulletTrigger;
		if (flag)
		{
			this._hitPoint = base.transform.position;
			base.hitVelocity = -base.GetComponent<Rigidbody>().velocity.normalized;
			base.hitPhysical = -base.hitVelocity;
			this.OnCollisionCheck(collision);
		}
	}

	// Token: 0x040002CB RID: 715
	private Vector3[] prevPosition = new Vector3[2];
}
