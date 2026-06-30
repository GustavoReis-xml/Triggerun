using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class CBulletTrackMe : CBulletMe
{
	// Token: 0x06000387 RID: 903 RVA: 0x0001B378 File Offset: 0x00019578
	public override void initBullet(CMuzzle muzz, CBulletData __data, bool child, int ismaster = 0, int sandanChildNum = 0)
	{
		base.initBullet(muzz, __data, child, ismaster, 0);
		base.trackSpeedNow = ((CBulletTrackData)base.bulletData).initSpeed;
		base.trackRotationSpeedNow = ((CBulletTrackData)base.bulletData).initRotationSpeed;
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001B3B8 File Offset: 0x000195B8
	private void FixedUpdate()
	{
		bool readyDestory = base.readyDestory;
		if (!readyDestory)
		{
			this.checkDelayBoom();
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
				float deltaTime = Time.deltaTime;
				bool flag3 = deltaTime <= 0f;
				if (!flag3)
				{
					float num = ((CBulletTrackData)base.bulletData).acceleratedSpeed * deltaTime;
					base.trackSpeedNow += num;
					bool flag4 = base.trackSpeedNow > ((CBulletTrackData)base.bulletData).acceleratedSpeedMax;
					if (flag4)
					{
						base.trackSpeedNow = ((CBulletTrackData)base.bulletData).acceleratedSpeedMax;
					}
					num = ((CBulletTrackData)base.bulletData).acceleratedRotationSpeed * deltaTime;
					base.trackRotationSpeedNow += num;
					bool flag5 = base.trackRotationSpeedNow > ((CBulletTrackData)base.bulletData).acceleratedRotationSpeedMax;
					if (flag5)
					{
						base.trackRotationSpeedNow = ((CBulletTrackData)base.bulletData).acceleratedRotationSpeedMax;
					}
					bool flag6 = !(this.bulletTargetObject == null) && (this.bulletTargetNet == null || !this.bulletTargetNet.isDead);
					if (flag6)
					{
						this.allRig[0].velocity = this.allRig[0].transform.forward * base.trackSpeedNow;
						Vector3 position = base.transform.position;
						Vector3 position2 = this.bulletTargetObject.transform.position;
						Vector3 normalized = (position2 - position).normalized;
						bool flag7 = normalized == Vector3.zero;
						if (flag7)
						{
							normalized = (this.bulletTargetObject.transform.position + Vector3.up * 0.01f - position).normalized;
						}
						Quaternion b = Quaternion.LookRotation(normalized);
						Quaternion rot = Quaternion.Slerp(base.transform.rotation, b, deltaTime * base.trackRotationSpeedNow);
						this.allRig[0].MoveRotation(rot);
						this.prevPosition[1] = this.prevPosition[0];
						this.prevPosition[0] = this.allRig[0].position;
					}
				}
			}
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001B65C File Offset: 0x0001985C
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
				bool flag4 = ((CBulletTrackData)base.bulletData).bulletThrough == 1 && collision.tag.Contains("can_damage");
				if (flag4)
				{
					this.throughDamage(collision);
				}
				bool flag5 = ((CBulletTrackData)base.bulletData).triggerType == 1;
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
					bool flag6 = ((CBulletTrackData)base.bulletData).triggerType == 2;
					if (flag6)
					{
						for (int j = 0; j < this.allRig.Length; j++)
						{
							this.allRig[j].velocity = Vector3.zero;
							this.allRig[j].angularVelocity = Vector3.zero;
						}
						base.hitCollider = collision;
						base.transform.position = this._hitPoint + base.hitVelocity * 0.1f;
						bool flag7 = base.bulletData.bulletDestoryTimeOut <= 0f;
						if (!flag7)
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
					}
					else
					{
						bool flag10 = ((CBulletTrackData)base.bulletData).triggerType == 3;
						if (flag10)
						{
							bool flag11 = collision.tag.Contains("can_damage");
							if (flag11)
							{
								for (int k = 0; k < this.allRig.Length; k++)
								{
									this.allRig[k].velocity = Vector3.zero;
									this.allRig[k].angularVelocity = Vector3.zero;
								}
								base.hitCollider = collision;
								base.end = true;
								base.readyDestory = true;
								this.boom();
								this.destoryBullet();
							}
						}
						else
						{
							bool flag12 = ((CBulletTrackData)base.bulletData).triggerType == 4;
							if (flag12)
							{
								float num = Vector3.Dot(base.hitVelocity, Vector3.up);
								bool flag13 = num > 0.45f;
								if (flag13)
								{
									for (int l = 0; l < this.allRig.Length; l++)
									{
										this.allRig[l].velocity = Vector3.zero;
										this.allRig[l].angularVelocity = Vector3.zero;
									}
									base.hitCollider = collision;
									base.end = true;
									base.readyDestory = true;
									this.boom();
									this.destoryBullet();
								}
							}
							else
							{
								bool flag14 = ((CBulletTrackData)base.bulletData).triggerType != 5;
								if (!flag14)
								{
									for (int m = 0; m < this.allRig.Length; m++)
									{
										this.allRig[m].velocity = Vector3.zero;
										this.allRig[m].angularVelocity = Vector3.zero;
									}
									base.hitCollider = collision;
									base.transform.position = this._hitPoint;
									bool flag15 = base.bulletData.bulletDestoryTimeOut > 0f;
									if (flag15)
									{
										bool flag16 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
										if (flag16)
										{
											bool flag17 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
											if (flag17)
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
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001BB80 File Offset: 0x00019D80
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

	// Token: 0x0600038B RID: 907 RVA: 0x0001BBF4 File Offset: 0x00019DF4
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
					this._hitPoint = a + base.hitVelocity * 0.5f;
				}
			}
			base.hitPhysical = -base.hitVelocity;
			this.OnCollisionCheck(collider);
		}
	}

	// Token: 0x040002D7 RID: 727
	private Vector3[] prevPosition = new Vector3[2];
}
