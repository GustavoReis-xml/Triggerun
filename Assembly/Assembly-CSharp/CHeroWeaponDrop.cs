using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class CHeroWeaponDrop : MonoBehaviour
{
	// Token: 0x060004F4 RID: 1268 RVA: 0x00028B45 File Offset: 0x00026D45
	public void InitDrop(NetLiving gp)
	{
		this.hero = gp;
		this.hero.playerSound.LoadWeaponDrop();
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00028B60 File Offset: 0x00026D60
	private void OnDestroy()
	{
		for (int i = 0; i < this.weaponsA.Length; i++)
		{
			bool flag = this.weaponsA[i] != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.weaponsA[i].gameObject);
			}
		}
		for (int j = 0; j < this.weaponsB.Length; j++)
		{
			bool flag2 = this.weaponsB[j] != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.weaponsB[j].gameObject);
			}
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00028BF4 File Offset: 0x00026DF4
	private void Awake()
	{
		for (int i = 0; i < this.weaponsA.Length; i++)
		{
			this.weaponsA[i].parent = fGameManager.allBaozhaFather.transform;
			this.weaponsA[i].position = new Vector3(-100f, -100f, -100f);
			Rigidbody rigidbody = this.weaponsA[i].GetComponentInChildren<Rigidbody>();
			bool flag = rigidbody == null;
			if (flag)
			{
				rigidbody = this.weaponsA[i].gameObject.AddComponent<Rigidbody>();
			}
			rigidbody.isKinematic = false;
			Collider componentInChildren = this.weaponsA[i].GetComponentInChildren<Collider>();
			componentInChildren.gameObject.layer = this.layer;
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}
		for (int j = 0; j < this.weaponsB.Length; j++)
		{
			this.weaponsB[j].parent = fGameManager.allBaozhaFather.transform;
			this.weaponsB[j].position = new Vector3(-100f, -100f, -100f);
			Rigidbody rigidbody2 = this.weaponsB[j].GetComponentInChildren<Rigidbody>();
			bool flag2 = rigidbody2 == null;
			if (flag2)
			{
				rigidbody2 = this.weaponsB[j].gameObject.AddComponent<Rigidbody>();
			}
			rigidbody2.isKinematic = false;
			Collider componentInChildren2 = this.weaponsB[j].GetComponentInChildren<Collider>();
			componentInChildren2.gameObject.layer = this.layer;
			rigidbody2.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00028D88 File Offset: 0x00026F88
	private void OnDisable()
	{
		bool beginCheck = this.BeginCheck;
		if (beginCheck)
		{
			this.hero.playerSound.PlayWeaponDropSound();
			this.BeginCheck = false;
		}
		base.enabled = false;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00028DC4 File Offset: 0x00026FC4
	private void OnEnable()
	{
		bool flag = this.hero != null && (!this.hero.isMainPlayer || ((CameraControl.iCameraState != CameraState.FirstState || !(base.transform.gameObject != this.hero.userModelFps)) && (CameraControl.iCameraState != CameraState.ThirdState || !(base.transform.gameObject != this.hero.userModelTps))));
		if (flag)
		{
			Rigidbody componentInChildren = this.weaponsA[this.nowWeapon].GetComponentInChildren<Rigidbody>();
			componentInChildren.isKinematic = false;
			componentInChildren.Sleep();
			this.weaponsA[this.nowWeapon].position = this.weaponPointA.position;
			this.weaponsA[this.nowWeapon].rotation = this.weaponPointA.rotation;
			Rigidbody componentInChildren2 = this.weaponsB[this.nowWeapon].GetComponentInChildren<Rigidbody>();
			componentInChildren2.isKinematic = false;
			componentInChildren2.Sleep();
			this.weaponsB[this.nowWeapon].position = this.weaponPointB.position;
			this.weaponsB[this.nowWeapon].rotation = this.weaponPointB.rotation;
			Vector3 vector = base.transform.forward * 3f;
			bool flag2 = this.hero != null;
			if (flag2)
			{
				vector += this.hero.tChar.rig.velocity;
			}
			base.StartCoroutine(this.dropTo(vector, componentInChildren, componentInChildren2));
			this.nowWeapon++;
			bool flag3 = this.nowWeapon >= this.weaponsA.Length;
			if (flag3)
			{
				this.nowWeapon = 0;
			}
			this.weaponA = componentInChildren;
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00028F95 File Offset: 0x00027195
	private IEnumerator dropTo(Vector3 force, Rigidbody a, Rigidbody b)
	{
		yield return 0;
		this.BeginCheck = true;
		a.AddForce(force, ForceMode.Impulse);
		b.AddForce(force, ForceMode.Impulse);
		yield break;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00028FBC File Offset: 0x000271BC
	private void Update()
	{
		bool beginCheck = this.BeginCheck;
		if (beginCheck)
		{
			Ray ray = new Ray(this.weaponA.position + Vector3.up * 0.1f, -Vector3.up);
			RaycastHit raycastHit = default(RaycastHit);
			bool flag = Physics.SphereCast(ray, 0.3f, out raycastHit, 0.5f, 1) && raycastHit.normal.y > 0.7f;
			if (flag)
			{
				this.BeginCheck = false;
				this.hero.playerSound.PlayWeaponDropSound();
			}
		}
	}

	// Token: 0x040003CB RID: 971
	public Transform weaponPointA;

	// Token: 0x040003CC RID: 972
	public Transform[] weaponsA;

	// Token: 0x040003CD RID: 973
	public Transform weaponPointB;

	// Token: 0x040003CE RID: 974
	public Transform[] weaponsB;

	// Token: 0x040003CF RID: 975
	private Rigidbody weaponA;

	// Token: 0x040003D0 RID: 976
	private int layer = 14;

	// Token: 0x040003D1 RID: 977
	private int nowWeapon;

	// Token: 0x040003D2 RID: 978
	private NetLiving hero;

	// Token: 0x040003D3 RID: 979
	private bool BeginCheck;
}
