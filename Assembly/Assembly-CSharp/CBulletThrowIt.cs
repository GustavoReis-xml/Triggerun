using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class CBulletThrowIt : CBulletIt
{
	// Token: 0x06000369 RID: 873 RVA: 0x00019964 File Offset: 0x00017B64
	private void FixedUpdate()
	{
		bool flag = !this.renderDisable && this.objectRender != null && CFilmManager.Instance.playing && base.muzzle != null && base.muzzle.launcher != null && base.muzzle.launcher.NetObject != null && !base.muzzle.launcher.NetObject.isCopy;
		if (flag)
		{
			this.renderDisable = true;
			this.objectRender.SetEnemyFriendRenderParticle(false, true);
		}
		bool flag2 = !base.readyDestory && ((CBulletThrowData)base.bulletData).triggerType == 4 && this.allRig.Length != 0 && this.allRig[0].IsSleeping();
		if (flag2)
		{
			for (int i = 0; i < this.allRig.Length; i++)
			{
				this.allRig[i].velocity = Vector3.zero;
				this.allRig[i].angularVelocity = Vector3.zero;
			}
			base.end = true;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
		this.checkDelayBoom();
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00019A9C File Offset: 0x00017C9C
	private void OnCollisionCheck(Collider collision)
	{
		bool flag = base.muzzle == null;
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
				bool flag4 = ((CBulletThrowData)base.bulletData).triggerType == 1;
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
					bool flag5 = ((CBulletThrowData)base.bulletData).triggerType == 2;
					if (flag5)
					{
						for (int j = 0; j < this.allRig.Length; j++)
						{
							this.allRig[j].velocity = Vector3.zero;
							this.allRig[j].angularVelocity = Vector3.zero;
						}
						bool flag6 = base.bulletData.bulletDestoryTimeOut > 0f;
						if (flag6)
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
						this.destoryBullet();
					}
					else
					{
						bool flag9 = ((CBulletThrowData)base.bulletData).triggerType == 4;
						if (flag9)
						{
							base.hitCollider = collision;
							float num = Vector3.Dot(base.hitVelocity, Vector3.up);
							bool flag10 = num > 0.45f;
							if (flag10)
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
							bool flag11 = ((CBulletThrowData)base.bulletData).triggerType != 5;
							if (!flag11)
							{
								for (int l = 0; l < this.allRig.Length; l++)
								{
									this.allRig[l].velocity = Vector3.zero;
									this.allRig[l].angularVelocity = Vector3.zero;
								}
								base.end = true;
								base.readyDestory = true;
								bool flag12 = base.bulletData.bulletDestoryTimeOut > 0f;
								if (flag12)
								{
									bool flag13 = (1 << collision.gameObject.layer & LayerManager.bulletNotShowLayer()) == 0;
									if (flag13)
									{
										bool flag14 = (1 << collision.gameObject.layer & LayerManager.bulletChangeParentLayer()) != 0;
										if (flag14)
										{
											base.transform.parent = collision.transform;
										}
									}
									else
									{
										this.mustDestoryModel = true;
									}
								}
								this.boom();
								this.destoryBullet();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00019E44 File Offset: 0x00018044
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

	// Token: 0x0600036C RID: 876 RVA: 0x00019EB0 File Offset: 0x000180B0
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

	// Token: 0x040002CA RID: 714
	private bool renderDisable;
}
