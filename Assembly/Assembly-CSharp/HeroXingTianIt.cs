using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class HeroXingTianIt : HeroBaseIt
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000C6A34 File Offset: 0x000C4C34
	// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000C6CC5 File Offset: 0x000C4EC5
	public GameObject Shield_Object
	{
		get
		{
			bool flag = this._Shield_Object == null && base.hero.netobject_type == 1;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.gameObject, "body2_dun");
				Transform transform2 = Fun.Get_Child_Transform(base.gameObject, "shieldbody");
				this.huzhao = Fun.Get_Child_Transform(base.gameObject, "huzhao");
				this.huzhaoPo = Fun.Get_Child_Transform(base.gameObject, "huzhaopo");
				this.huzhaoPo1 = Fun.Get_Child_Transform(base.gameObject, "huzhaopo1");
				this.dun = Fun.Get_Child_Transform(base.gameObject, "dun");
				this.Aim = Fun.Get_Child_Transform(base.gameObject, "Aim");
				this.tempBodyTrans = new GameObject();
				this.tempBodyTrans.name = "tempBodyTrans";
				bool flag2 = transform != null;
				if (flag2)
				{
					Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
					bool flag3 = rigidbody == null;
					if (flag3)
					{
						rigidbody = transform.gameObject.AddComponent<Rigidbody>();
					}
					rigidbody.isKinematic = true;
					rigidbody.useGravity = true;
					rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
					transform.gameObject.tag = "can_damage_dun";
					transform.gameObject.layer = LayerMask.NameToLayer("shield" + base.hero.nForce.ToString());
					this.tempCol = transform.GetComponent<Collider>();
					this.tempCol.isTrigger = false;
					this.tempCol.enabled = false;
					this.tempBodyTrans.transform.parent = transform2.transform.parent;
					this.tempBodyTrans.transform.localPosition = transform2.transform.localPosition;
					this.tempBodyTrans.transform.localRotation = transform2.transform.localRotation;
					this.tempBodyTrans.transform.parent = this.dun.transform;
					this.tempBodyTrans.transform.rotation = Quaternion.AngleAxis(-22f, this.tempBodyTrans.transform.right) * this.tempBodyTrans.transform.rotation;
					transform.transform.parent = base.hero.UserControl.transform.parent;
					this._Shield_Object = transform.gameObject;
					this.shieldRig = rigidbody;
				}
			}
			return this._Shield_Object;
		}
		set
		{
			this._Shield_Object = value;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000C6CD0 File Offset: 0x000C4ED0
	// (set) Token: 0x060014F8 RID: 5368 RVA: 0x000C6D22 File Offset: 0x000C4F22
	public GameObject Shield_Break_Object
	{
		get
		{
			bool flag = this._Shield_Break_Object == null;
			if (flag)
			{
				this._Shield_Break_Object = Fun.Get_Child_Transform(base.gameObject, "huzhaosui").gameObject;
				this._Shield_Break_Object.SetActive(false);
			}
			return this._Shield_Break_Object;
		}
		set
		{
			this._Shield_Break_Object = value;
		}
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x000C6D2C File Offset: 0x000C4F2C
	public IEnumerator Shield_Break_Effect()
	{
		GameObject _Temp = null;
		bool flag = this.Shield_Break_Object != null;
		if (flag)
		{
			_Temp = UnityEngine.Object.Instantiate<GameObject>(this.Shield_Break_Object);
			_Temp.transform.parent = base.gameObject.transform;
			_Temp.transform.localPosition = Vector3.zero;
			_Temp.transform.localEulerAngles = Vector3.zero;
			_Temp.transform.localScale = Vector3.one;
			_Temp.SetActive(true);
			yield return new WaitForSeconds(2f);
			UnityEngine.Object.Destroy(_Temp);
		}
		yield break;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000C6D3C File Offset: 0x000C4F3C
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
			int num = CFilmManager.Instance.filmForce(this._hero);
			bool flag2 = num == this._hero.playerCommon.m_nShiliServer;
			if (flag2)
			{
				base.hero.renderBianSe.makeHongXianDisable();
			}
			base.hero.aimSystem.b_closeDisCheck = true;
		}
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x000C6DB4 File Offset: 0x000C4FB4
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		base.hero.playerSound.LoadShieldContinue();
		base.hero.playerSound.LoadShieldBreak();
		base.hero.playerSound.LoadShieldOpen();
		base.hero.playerSound.LoadShieldClose();
		base.hero.playerSound.LoadShieldAboutCrash();
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x000C6E20 File Offset: 0x000C5020
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
			bool flag2 = base.hero.tCharAnimationTps != null;
			if (flag2)
			{
				base.hero.tCharAnimationTps.animator.SetBool("JvDun", false);
			}
			bool activeSelf = this.huzhao.gameObject.activeSelf;
			if (activeSelf)
			{
				this.huzhao.gameObject.SetActive(false);
			}
			bool activeSelf2 = this.huzhaoPo.gameObject.activeSelf;
			if (activeSelf2)
			{
				this.huzhaoPo.gameObject.SetActive(false);
			}
			bool activeSelf3 = this.huzhaoPo1.gameObject.activeSelf;
			if (activeSelf3)
			{
				this.huzhaoPo1.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000C6EFA File Offset: 0x000C50FA
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000C6F08 File Offset: 0x000C5108
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
		base.hero.SetAnimator("JvDun", 0, false, 1);
		bool flag = m_bool && base.hero.skillEffectDict.ContainsKey(1021);
		if (flag)
		{
			((SkillShieldIt)base.hero.skillEffectDict[1021]).DeactiveShield();
		}
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000C6F78 File Offset: 0x000C5178
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		base.hero.SetAnimator("Stun", 0, m_bool, 1);
		bool flag = m_bool && base.hero.skillEffectDict.ContainsKey(1021);
		if (flag)
		{
			((SkillShieldIt)base.hero.skillEffectDict[1021]).DeactiveShield();
		}
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x000C6FE8 File Offset: 0x000C51E8
	public override void Revive()
	{
		base.Revive();
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
		}
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000C7014 File Offset: 0x000C5214
	private void FixedUpdate()
	{
		bool flag = base.hero != null && !(base.hero.tCharAnimationTps == null) && !(this.Shield_Object == null);
		if (flag)
		{
			bool flag2 = this.dun == null;
			if (flag2)
			{
				this.dun = Fun.Get_Child_Transform(base.gameObject, "dun");
			}
			else
			{
				bool flag3 = this.Aim == null;
				if (flag3)
				{
					this.Aim = Fun.Get_Child_Transform(base.gameObject, "Aim");
				}
				else
				{
					bool @bool = base.hero.tCharAnimationTps.animator.GetBool("JvDun");
					if (@bool)
					{
						this.dun.transform.LookAt(this.Aim.position + this.Aim.forward * 20f);
						this.dun.transform.rotation = Quaternion.AngleAxis(8f, this.dun.transform.up) * this.dun.transform.rotation;
						this.shieldRig.velocity = Vector3.zero;
						this.shieldRig.angularVelocity = Vector3.zero;
						this.shieldRig.MovePosition(this.tempBodyTrans.transform.position);
						this.shieldRig.MoveRotation(this.tempBodyTrans.transform.rotation);
					}
				}
			}
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000C71A8 File Offset: 0x000C53A8
	public override bool CanHitReaction()
	{
		bool @bool = base.hero.tCharAnimationTps.animator.GetBool("JvDun");
		return !@bool && base.CanHitReaction();
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000C71E4 File Offset: 0x000C53E4
	public override GameObject GetShield(string shieldName)
	{
		bool flag = this.tempCol != null;
		GameObject result;
		if (flag)
		{
			result = this.tempCol.gameObject;
		}
		else
		{
			result = base.GetShield(shieldName);
		}
		return result;
	}

	// Token: 0x0400103F RID: 4159
	private GameObject _Shield_Object;

	// Token: 0x04001040 RID: 4160
	private GameObject tempBodyTrans;

	// Token: 0x04001041 RID: 4161
	private Rigidbody shieldRig;

	// Token: 0x04001042 RID: 4162
	private Transform huzhao;

	// Token: 0x04001043 RID: 4163
	private Transform huzhaoPo;

	// Token: 0x04001044 RID: 4164
	private Transform huzhaoPo1;

	// Token: 0x04001045 RID: 4165
	private Transform dun;

	// Token: 0x04001046 RID: 4166
	private Transform Aim;

	// Token: 0x04001047 RID: 4167
	private Collider tempCol;

	// Token: 0x04001048 RID: 4168
	private GameObject _Shield_Break_Object;
}
