using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class PublicInfo : MonoBehaviour
{
	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0011FADC File Offset: 0x0011DCDC
	// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0011FAF4 File Offset: 0x0011DCF4
	public NetLiving NetObject
	{
		get
		{
			return this._NetObject;
		}
		set
		{
			this._NetObject = value;
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0011FB00 File Offset: 0x0011DD00
	// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0011FB18 File Offset: 0x0011DD18
	public string type
	{
		get
		{
			return this._type;
		}
		set
		{
			this._type = value;
		}
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x0011FB22 File Offset: 0x0011DD22
	public virtual void InitInfo(NetLiving _net)
	{
		this._NetObject = _net;
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x0011FB2C File Offset: 0x0011DD2C
	public NetLiving getNetBaseObj()
	{
		return this.NetObject;
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x0011FB44 File Offset: 0x0011DD44
	public void GetAllCollider()
	{
		bool flag = this.colls.Length == 0 && this.NetObject.userModelTps != null;
		if (flag)
		{
			this.colls = this.NetObject.userModelTps.GetComponentsInChildren<Collider>();
		}
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x0011FB8C File Offset: 0x0011DD8C
	public void SetAllColliderActive(bool active)
	{
		this.GetAllCollider();
		for (int i = 0; i < this.colls.Length; i++)
		{
			bool flag = !(this.colls[i] == null) && !this.colls[i].isTrigger;
			if (flag)
			{
				this.colls[i].enabled = active;
			}
		}
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0011FBF4 File Offset: 0x0011DDF4
	private void DisComput()
	{
		bool flag = Time.time >= this.disTimeOut;
		if (flag)
		{
			this.disTimeOut = Time.time + 0.5f;
			this.NetObject.disCameraNow = Vector3.Distance(CameraControl.Instance.transform.position, this.NetObject.UserControl.transform.position);
			bool flag2 = fGameManager.Heroplayer.UserControl != null;
			if (flag2)
			{
				this.NetObject.disNow = Vector3.Distance(fGameManager.Heroplayer.UserControl.transform.position, this.NetObject.UserControl.transform.position);
			}
		}
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x0011FCB0 File Offset: 0x0011DEB0
	protected virtual void CheckDisIK()
	{
		bool flag = this.NetObject == null || this.NetObject.haveDestroy || !this.NetObject.playerHaveCreate || CameraControl.Instance == null || CameraControl.Instance.transform == null || fGameManager.Heroplayer == null || fGameManager.Heroplayer.haveDestroy || !fGameManager.Heroplayer.playerHaveCreate;
		if (!flag)
		{
			this.DisComput();
			bool flag2 = fGameManager.Heroplayer != this.NetObject && this.NetObject.nForce == fGameManager.Heroplayer.nForce && this.NetObject.disNow < 0.6f && fGameManager.Heroplayer.tChar.onGround && fGameManager.Heroplayer.tChar.rig.velocity.magnitude < 0.01f && fGameManager.Heroplayer.charMove.mixInput == 0;
			if (flag2)
			{
				Vector3 normalized = (fGameManager.Heroplayer.UserControl.transform.position - this.NetObject.UserControl.transform.position).normalized;
				((TBCharacterThirdMe)fGameManager.Heroplayer.tChar).forceLevel = normalized * 10f;
				((TBCharacterThirdMe)fGameManager.Heroplayer.tChar).forceLevelTime = Time.time + 0.1f;
			}
			bool flag3 = !this.checkIK || this.breakWallCheck > Time.time;
			if (!flag3)
			{
				this.breakWallCheck = Time.time + 0.1f;
				this.eyeBlock = true;
				bool flag4 = this.NetObject != CameraControl.Instance.camerHero || this.NetObject.FpsOrTps != 1;
				if (flag4)
				{
					Camera controlCamera = CameraControl.Instance.controlCamera;
					Vector3 vector = controlCamera.WorldToViewportPoint(this.NetObject.defaultMesh.transform.position);
					bool flag5 = this.rect.Contains(vector) && vector.z >= controlCamera.nearClipPlane && vector.z <= controlCamera.farClipPlane;
					if (flag5)
					{
						Vector3 position = CameraControl.Instance.transform.position;
						Vector3 position2 = this.NetObject.defaultMesh.transform.position;
						float maxDistance = Vector3.Distance(position, position2);
						Vector3 normalized2 = (position2 - position).normalized;
						this.eyeBlock = Physics.Raycast(position, normalized2, maxDistance, this.lookLayer);
					}
				}
			}
		}
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x0011FF5E File Offset: 0x0011E15E
	protected virtual void Update()
	{
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x0011FF64 File Offset: 0x0011E164
	public void playerDeadRagdoll(Vector3 fV3, float fMax)
	{
		this.NetObject.RollDead = true;
		bool flag = this.NetObject.aimSystem != null;
		if (flag)
		{
			this.NetObject.aimSystem.SetEnable(false);
			this.NetObject.aimSystem.AimEnable(false);
		}
		bool flag2 = !(this.NetObject.tCharAnimationTps == null) && this.NetObject.playerHaveCreate;
		if (flag2)
		{
			this.NetObject.tCharAnimationTps.animator.SetInteger("Dead", 1);
			base.StartCoroutine(this.IDeadRagdoll(fV3, fMax));
		}
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x0012000B File Offset: 0x0011E20B
	private IEnumerator IDeadRagdoll(Vector3 fV3, float fMax)
	{
		bool flag = this.NetObject != null && !(this.NetObject.tCharAnimationTps == null) && !this.NetObject.haveDestroy && this.NetObject.playerHaveCreate;
		if (flag)
		{
			yield return 0;
			yield return new WaitForEndOfFrame();
			this.NetObject.tCharAnimationTps.animator.enabled = false;
			bool flag2 = !(fV3 == Vector3.zero);
			Vector3 v3Random;
			if (flag2)
			{
				v3Random = ((fMax > 0f) ? (fV3 * fMax) : (fV3 * 10f)) * 1.5f;
			}
			else
			{
				float x = UnityEngine.Random.Range(-0.9f, 0.9f);
				float y = UnityEngine.Random.Range(-0.9f, 0.9f);
				float z = UnityEngine.Random.Range(-0.9f, 0.9f);
				v3Random = new Vector3(x, y, z);
				v3Random = (v3Random + Vector3.up).normalized * 8f;
			}
			CObjectRender or = this.NetObject.userModelTps.GetComponent<CObjectRender>();
			bool flag3 = or != null;
			if (flag3)
			{
				or.OnDeathCloseTrans(false);
			}
			if (this.NetObject.userModelTps != null)
			{
				Collider[] allColls = this.NetObject.userModelTps.GetComponentsInChildren<Collider>();
				foreach (Collider col in allColls)
				{
					if (col != null)
					{
						col.enabled = true;
						col.isTrigger = false;
						col.gameObject.layer = 0;
					}
				}
				Rigidbody[] rigBodys = this.NetObject.userModelTps.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigidbody in rigBodys)
				{
					if (rigidbody != null)
					{
						rigidbody.isKinematic = false;
						rigidbody.useGravity = true;
						rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
						rigidbody.velocity = Vector3.zero;
						rigidbody.AddForce(v3Random, ForceMode.Impulse);
					}
				}
			}
			this.NetObject.tChar.DeadInit();
			bool flag4 = this.NetObject.tpsRender != null;
			if (flag4)
			{
				this.NetObject.tpsRender.SetParticleSystem(false);
			}
			v3Random = default(Vector3);
			or = null;
		}
		yield break;
	}

	// Token: 0x04001C91 RID: 7313
	protected NetLiving _NetObject;

	// Token: 0x04001C92 RID: 7314
	private string _type;

	// Token: 0x04001C93 RID: 7315
	private Collider[] colls = new Collider[0];

	// Token: 0x04001C94 RID: 7316
	protected bool checkIK;

	// Token: 0x04001C95 RID: 7317
	private int lookLayer = 1;

	// Token: 0x04001C96 RID: 7318
	public bool eyeBlock = true;

	// Token: 0x04001C97 RID: 7319
	private float disTimeOut;

	// Token: 0x04001C98 RID: 7320
	private float breakWallCheck;

	// Token: 0x04001C99 RID: 7321
	private Rect rect = new Rect(-0.1f, -0.1f, 1.2f, 1.2f);
}
