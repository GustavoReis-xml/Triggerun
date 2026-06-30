using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class CBulletTrackIt : CBulletIt
{
	// Token: 0x06000381 RID: 897 RVA: 0x0001AB5C File Offset: 0x00018D5C
	public override void initBullet(CMuzzle muzz, CBulletData __data, bool child, int ismaster = 0, int sandanChildNum = 0)
	{
		base.initBullet(muzz, __data, child, ismaster, sandanChildNum);
		base.trackSpeedNow = ((CBulletTrackData)base.bulletData).initSpeed;
		base.trackRotationSpeedNow = ((CBulletTrackData)base.bulletData).initRotationSpeed;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001AB9C File Offset: 0x00018D9C
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
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, deltaTime * base.trackRotationSpeedNow);
					}
				}
			}
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001ADF8 File Offset: 0x00018FF8
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
				bool flag4 = ((CBulletTrackData)base.bulletData).triggerType == 1;
				if (flag4)
				{
					for (int i = 0; i < this.allRig.Length; i++)
					{
						this.allRig[i].velocity = Vector3.zero;
						this.allRig[i].angularVelocity = Vector3.zero;
					}
					base.hitCollider = collision;
					base.end = true;
					base.readyDestory = true;
					this.boom();
					this.destoryBullet();
				}
				else
				{
					bool flag5 = ((CBulletTrackData)base.bulletData).triggerType == 2;
					if (flag5)
					{
						for (int j = 0; j < this.allRig.Length; j++)
						{
							this.allRig[j].velocity = Vector3.zero;
							this.allRig[j].angularVelocity = Vector3.zero;
						}
						bool flag6 = base.bulletData.bulletDestoryTimeOut <= 0f;
						if (!flag6)
						{
							bool flag7 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
							if (flag7)
							{
								bool flag8 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
								if (flag8)
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
						bool flag9 = ((CBulletTrackData)base.bulletData).triggerType == 3;
						if (flag9)
						{
							bool flag10 = collision.tag.Contains("can_damage");
							if (flag10)
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
							bool flag11 = ((CBulletTrackData)base.bulletData).triggerType == 4;
							if (flag11)
							{
								float num = Vector3.Dot(base.hitVelocity, Vector3.up);
								bool flag12 = num > 0.45f;
								if (flag12)
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
								bool flag13 = ((CBulletTrackData)base.bulletData).triggerType != 5;
								if (!flag13)
								{
									for (int m = 0; m < this.allRig.Length; m++)
									{
										this.allRig[m].velocity = Vector3.zero;
										this.allRig[m].angularVelocity = Vector3.zero;
									}
									base.hitCollider = collision;
									base.transform.position = this._hitPoint;
									bool flag14 = base.bulletData.bulletDestoryTimeOut > 0f;
									if (flag14)
									{
										bool flag15 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
										if (flag15)
										{
											bool flag16 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
											if (flag16)
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

	// Token: 0x06000384 RID: 900 RVA: 0x0001B290 File Offset: 0x00019490
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

	// Token: 0x06000385 RID: 901 RVA: 0x0001B304 File Offset: 0x00019504
	private void OnCollisionEnter(Collision collision)
	{
		bool flag = !base.readyDestory;
		if (flag)
		{
			this._hitPoint = collision.contacts[0].point;
			base.hitVelocity = collision.contacts[0].normal;
			base.hitPhysical = -base.hitVelocity;
			this.OnCollisionCheck(collision.collider);
		}
	}
}
