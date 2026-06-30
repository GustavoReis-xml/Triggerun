using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class SkillSandstormDashMe : SkillBaseMe
{
	// Token: 0x17000512 RID: 1298
	// (get) Token: 0x060027E4 RID: 10212 RVA: 0x00167CF0 File Offset: 0x00165EF0
	// (set) Token: 0x060027E5 RID: 10213 RVA: 0x00167D08 File Offset: 0x00165F08
	private bool DashState
	{
		get
		{
			return this._DashState;
		}
		set
		{
			this._DashState = value;
		}
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x00167D14 File Offset: 0x00165F14
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.DashState = false;
		this.DashDistance = 20f;
		this.hitID = 0;
		this.layer = (1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2"));
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x00167DAC File Offset: 0x00165FAC
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.SkillActive = true;
		bool onGround = base.hero.tChar.onGround;
		if (onGround)
		{
			this.SkillAction();
		}
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x00167DE8 File Offset: 0x00165FE8
	private void SkillAction()
	{
		bool interuptDash = ((HeroShawangMe)base.hero.heroBase).InteruptDash;
		if (interuptDash)
		{
			((HeroShawangMe)base.hero.heroBase).InteruptDash = false;
		}
		CameraControl.Instance.DashMouseMoveSpeed = true;
		this.DashCurDistance = 0f;
		base.hero.SetAnimator("SkillDash", 0, true, 1);
		this.LastPos = base.transform.position;
		this.SkillActive = false;
		((TBCharacterThirdMe)base.hero.tChar).ForceZeroPhysic = true;
		((TBCharacterThirdMe)base.hero.tChar).airDrag = 100f;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.bePlayActive = false;
		base.StartCoroutine(this.Dash_Handle());
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x00167ED0 File Offset: 0x001660D0
	private IEnumerator Dash_Handle()
	{
		yield return new WaitForSeconds(0.2f);
		this.DashState = true;
		base.hero.wInput.notInput = true;
		yield break;
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x00167EE0 File Offset: 0x001660E0
	private void Dash()
	{
		bool dashState = this.DashState;
		if (dashState)
		{
			bool interuptDash = ((HeroShawangMe)base.hero.heroBase).InteruptDash;
			if (interuptDash)
			{
				this.OneSkillFinish();
			}
			Vector3 forward = CameraControl.Instance.nowTarget.forward;
			forward.y = 0f;
			Vector3 normalized = forward.normalized;
			Debug.DrawRay(base.hero.tChar.transform.position, CameraControl.Instance.nowTarget.forward, Color.black);
			Debug.DrawRay(base.hero.tChar.transform.position, normalized, Color.blue);
			((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(normalized * this.Addforce, 0.1f, false, 1000f, 1000f);
		}
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x00167FC8 File Offset: 0x001661C8
	public override void OneSkillFinish()
	{
		bool dashState = this.DashState;
		if (dashState)
		{
			base.OneSkillFinish();
			CameraControl.Instance.DashMouseMoveSpeed = false;
			base.hero.wInput.notInput = false;
			base.hero.bePlayActive = true;
			bool dashState2 = this.DashState;
			if (dashState2)
			{
				this.DashState = false;
				this.hitID = 0;
				base.StartCoroutine(this.ResetAirDrag());
			}
			base.hero.SetAnimator("SkillDash", 0, false, 1);
		}
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x00168055 File Offset: 0x00166255
	private IEnumerator ResetAirDrag()
	{
		yield return new WaitForSeconds(0.5f);
		((TBCharacterThirdMe)base.hero.tChar).ForceZeroPhysic = false;
		((TBCharacterThirdMe)base.hero.tChar).airDrag = 1f;
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.one * this.Addforce, 0f, false, 1000f, 1000f);
		yield break;
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x00168064 File Offset: 0x00166264
	public override bool OneSkillCanDo()
	{
		bool flag = !base.hero.tChar.onGround;
		return !flag && base.OneSkillCanDo();
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x00168098 File Offset: 0x00166298
	private void OnCollisionEnter(Collision _collision)
	{
		bool flag = !this.DashState || _collision.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet_1")) == 0 || _collision.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet_2")) == 0 || _collision.gameObject.layer.CompareTo(LayerMask.NameToLayer("bullet")) == 0;
		if (!flag)
		{
			bool flag2 = _collision.gameObject.tag == "can_damage_human" || _collision.gameObject.tag == "can_damage_robot";
			if (flag2)
			{
				int num = 0;
				ArrayList arrayList = new ArrayList();
				GameObject gameObject = _collision.gameObject.transform.root.gameObject;
				PublicInfo component = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component != null;
				if (flag3)
				{
					NetLiving netObject = component.NetObject;
					num = netObject.getNetid();
				}
				bool flag4 = this.hitID != num;
				if (flag4)
				{
					arrayList.Add(num);
					this.hitID = num;
					base.hero.SendMessageToServer(2030001, arrayList);
					this.OneSkillFinish();
				}
			}
			else
			{
				Vector3 vector = CameraControl.Instance.nowTarget.transform.position - Vector3.up * 0.6f;
				Ray ray = new Ray(vector, base.hero.UserControl.transform.forward);
				Debug.DrawRay(vector, ray.direction, Color.red);
				float num2 = 0f;
				for (int i = 0; i < _collision.contacts.Length; i++)
				{
					float num3 = Mathf.Abs(_collision.contacts[i].point.x - base.hero.tChar.rig.position.x);
					bool flag5 = num3 > num2;
					if (flag5)
					{
						num2 = num3;
					}
				}
				foreach (ContactPoint contactPoint in _collision.contacts)
				{
					bool flag6 = contactPoint.normal.y < 0.7f;
					if (flag6)
					{
						bool flag7 = base.hero.tChar.rig.position.y - contactPoint.point.y < -0.4f;
						if (flag7)
						{
							bool flag8 = num2 > 0.8f;
							if (flag8)
							{
								this.OneSkillFinish();
								break;
							}
						}
						else
						{
							bool flag9 = Physics.Raycast(ray, 1f, this.layer);
							if (flag9)
							{
								this.OneSkillFinish();
								break;
							}
						}
					}
					else
					{
						bool flag10 = Physics.Raycast(ray, 1f, this.layer);
						if (flag10)
						{
							this.OneSkillFinish();
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x00168398 File Offset: 0x00166598
	private void OnTriggerEnter(Collider c)
	{
		bool flag = this.DashState && (c.gameObject.tag == "can_damage_human" || c.gameObject.tag == "can_damage_robot");
		if (flag)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			GameObject gameObject = c.gameObject.transform.root.gameObject;
			PublicInfo component = gameObject.GetComponent<PublicInfo>();
			bool flag2 = component != null;
			if (flag2)
			{
				NetLiving netObject = component.NetObject;
				num = netObject.getNetid();
			}
			bool flag3 = this.hitID != num;
			if (flag3)
			{
				arrayList.Add(num);
				this.hitID = num;
				base.hero.SendMessageToServer(2030001, arrayList);
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x00168474 File Offset: 0x00166674
	private void FixedUpdate()
	{
		bool flag = base.hero == null;
		if (!flag)
		{
			bool flag2 = this.SkillActive && base.hero.tChar.onGround;
			if (flag2)
			{
				this.SkillAction();
			}
			bool flag3 = !this.DashState;
			if (!flag3)
			{
				bool flag4 = (base.hero.playerCommon.m_nHunMi == 1 || base.hero.playerCommon.m_nDongjie == 1) && this.DashState;
				if (flag4)
				{
					this.OneSkillFinish();
				}
				float num = Vector3.Distance(base.transform.position, this.LastPos);
				bool flag5 = Mathf.Abs(num) < 0.1f;
				if (flag5)
				{
					this.stayTimer -= Time.deltaTime;
					bool flag6 = this.stayTimer < 0f;
					if (flag6)
					{
						this.OneSkillFinish();
						this.stayTimer = 0.2f;
						this.DashState = false;
					}
				}
				else
				{
					this.stayTimer = 0.2f;
				}
				this.DashCurDistance += num;
				bool flag7 = this.DashCurDistance > this.DashDistance;
				if (flag7)
				{
					this.OneSkillFinish();
					this.DashState = false;
				}
				this.LastPos = base.transform.position;
				this.Dash();
			}
		}
	}

	// Token: 0x040022E3 RID: 8931
	private bool _DashState;

	// Token: 0x040022E4 RID: 8932
	private bool SkillActive;

	// Token: 0x040022E5 RID: 8933
	private float DashCurDistance;

	// Token: 0x040022E6 RID: 8934
	public float LimitYSpeed = 8f;

	// Token: 0x040022E7 RID: 8935
	public float LimitFrontSpeed = 100f;

	// Token: 0x040022E8 RID: 8936
	public float Addforce = 800f;

	// Token: 0x040022E9 RID: 8937
	private int layer;

	// Token: 0x040022EA RID: 8938
	private int hitID;

	// Token: 0x040022EB RID: 8939
	private Vector3 LastPos;

	// Token: 0x040022EC RID: 8940
	private float DashDistance;

	// Token: 0x040022ED RID: 8941
	private float stayTimer = 0.2f;
}
