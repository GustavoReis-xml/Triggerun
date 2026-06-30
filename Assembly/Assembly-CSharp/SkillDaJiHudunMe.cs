using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class SkillDaJiHudunMe : SkillBaseMe
{
	// Token: 0x1700050D RID: 1293
	// (get) Token: 0x0600269B RID: 9883 RVA: 0x0015F1E0 File Offset: 0x0015D3E0
	// (set) Token: 0x0600269C RID: 9884 RVA: 0x0015F1F8 File Offset: 0x0015D3F8
	public SkillDaJiHudunMe.Break_State Shield_Break_State
	{
		get
		{
			return this._Shield_Break_State;
		}
		set
		{
			this._Shield_Break_State = value;
		}
	}

	// Token: 0x1700050E RID: 1294
	// (get) Token: 0x0600269D RID: 9885 RVA: 0x0015F204 File Offset: 0x0015D404
	// (set) Token: 0x0600269E RID: 9886 RVA: 0x0015F480 File Offset: 0x0015D680
	public GameObject Shield_Object
	{
		get
		{
			bool flag = this._Shield_Object == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.gameObject, "body2_dun");
				Transform transform2 = Fun.Get_Child_Transform(base.gameObject, "shieldbody");
				this.huzhao = Fun.Get_Child_Transform(base.gameObject, "huzhao");
				this.huzhaoPo = Fun.Get_Child_Transform(base.gameObject, "huzhaopo");
				this.huzhaoSui = Fun.Get_Child_Transform(base.gameObject, "huzhaosui");
				this.dun = Fun.Get_Child_Transform(base.gameObject, "dun");
				bool flag2 = this.tempBodyTrans == null;
				if (flag2)
				{
					this.tempBodyTrans = new GameObject();
					this.tempBodyTrans.name = "tempBodyTrans";
				}
				bool flag3 = transform != null;
				if (flag3)
				{
					Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
					bool flag4 = rigidbody == null;
					if (flag4)
					{
						rigidbody = transform.gameObject.AddComponent<Rigidbody>();
					}
					rigidbody.isKinematic = true;
					rigidbody.useGravity = true;
					rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
					transform.gameObject.tag = "can_damage_dun";
					transform.gameObject.layer = LayerMask.NameToLayer("shield" + base.hero.nForce.ToString());
					Collider component = transform.GetComponent<Collider>();
					component.isTrigger = false;
					component.enabled = false;
					this.tempBodyTrans.transform.parent = transform2.transform.parent;
					this.tempBodyTrans.transform.localPosition = transform2.transform.localPosition;
					this.tempBodyTrans.transform.localRotation = transform2.transform.localRotation;
					this.tempBodyTrans.transform.parent = this.dun.transform;
					this.tempBodyTrans.transform.rotation = Quaternion.AngleAxis(-22f, this.tempBodyTrans.transform.right) * this.tempBodyTrans.transform.rotation;
					transform.parent.transform.parent = base.hero.UserControl.transform.parent;
					this._Shield_Object = transform.gameObject.transform.parent.gameObject;
				}
			}
			return this._Shield_Object;
		}
		set
		{
			this._Shield_Object = value;
		}
	}

	// Token: 0x0600269F RID: 9887 RVA: 0x0015F48C File Offset: 0x0015D68C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillShift", 0, true, 1);
		this.forward = CameraControl.Instance.transform.forward;
		this.forward.y = 0f;
		this.canCreateShield = true;
		this.OneSkillFinish();
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x0015F4EF File Offset: 0x0015D6EF
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.canCreateShield = false;
		((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
		this.SendShieldData(0, Vector3.zero, Vector3.zero);
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x0015F526 File Offset: 0x0015D726
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x0015F530 File Offset: 0x0015D730
	public void createShileObject(Vector3 releasePos, Vector3 pos)
	{
		bool flag = this.canCreateShield;
		if (flag)
		{
			bool flag2 = this.lifeTime > 0f;
			if (flag2)
			{
				base.hero.Clear_Adhere_Npc("body2_dun");
				this.huzhao.gameObject.SetActive(false);
				this.huzhaoPo.gameObject.SetActive(false);
			}
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = true;
			this.SendShieldData(1, pos, releasePos);
			((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(0);
			this.canCreateShield = false;
			this.forward = Vector3.Normalize(pos - releasePos);
			this.forward.y = 0f;
			this.lifeTime = 20f;
			this.Shield_Object.SetActive(true);
			this.Shield_Object.GetComponentInChildren<MeshCollider>().enabled = true;
			this.Shield_Object.transform.position = pos;
			this.Shield_Object.transform.up = Vector3.up;
			this.Shield_Object.transform.forward = this.forward;
			this.huzhao.gameObject.SetActive(true);
			this.dun.transform.position = pos;
			this.dun.transform.up = Vector3.up;
			this.dun.transform.forward = this.forward;
			this.dun.transform.parent = this.Shield_Object.transform.parent;
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = true;
		}
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x0015F6E4 File Offset: 0x0015D8E4
	public void Update_Shield_Data(ArrayList _Parameters)
	{
		ArrayList arrayList = (ArrayList)_Parameters[1];
		bool flag = arrayList != null;
		if (flag)
		{
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy = int.Parse(arrayList[1].ToString());
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Max_Energy = int.Parse(arrayList[2].ToString());
			this.GemFactor = (float)(((ISkillDataSkillDaJiHudun)this.ISD).Shield_Max_Energy / 4000) + 0.001f;
			bool flag2 = int.Parse(arrayList[0].ToString()) == 1;
			if (flag2)
			{
				((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = true;
			}
			else
			{
				bool flag3 = ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy > 0 && int.Parse(arrayList[0].ToString()) == 0;
				if (flag3)
				{
					((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
					this.lifeTime = 0f;
				}
			}
		}
		bool flag4 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 2705f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 4000f * this.GemFactor;
		if (flag4)
		{
			bool flag5 = this.Shield_Break_State != SkillDaJiHudunMe.Break_State.State_1 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State;
			if (flag5)
			{
				this.Shield_Break_State = SkillDaJiHudunMe.Break_State.State_1;
				base.hero.SetAnimator("JvDun", 0, true, 2);
				base.hero.SetAnimator("SheildState", 2, 1, 2);
				this.huzhao.gameObject.SetActive(true);
				this.huzhaoPo.gameObject.SetActive(false);
			}
		}
		else
		{
			bool flag6 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 1305f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 2704f * this.GemFactor;
			if (flag6)
			{
				bool flag7 = this.Shield_Break_State != SkillDaJiHudunMe.Break_State.State_2 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State;
				if (flag7)
				{
					this.Shield_Break_State = SkillDaJiHudunMe.Break_State.State_2;
					this.huzhao.gameObject.SetActive(true);
					this.huzhaoPo.gameObject.SetActive(true);
					base.hero.SetAnimator("JvDun", 0, true, 2);
					base.hero.SetAnimator("SheildState", 2, 2, 2);
				}
			}
			else
			{
				bool flag8 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 1f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 1304f * this.GemFactor;
				if (flag8)
				{
					bool flag9 = this.Shield_Break_State != SkillDaJiHudunMe.Break_State.State_3 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State;
					if (flag9)
					{
						this.Shield_Break_State = SkillDaJiHudunMe.Break_State.State_3;
						base.hero.SetAnimator("JvDun", 0, true, 2);
						base.hero.SetAnimator("SheildState", 2, 3, 2);
					}
				}
				else
				{
					bool flag10 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 0f && this.Shield_Break_State != SkillDaJiHudunMe.Break_State.Break;
					if (flag10)
					{
						this.Shield_Break_State = SkillDaJiHudunMe.Break_State.Break;
						base.hero.playerSound.PlayShieldContinueSound(2);
						base.hero.playerSound.PlayShieldBreakSound();
						((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
						base.hero.SetAnimator("JvDun", 0, false, 2);
						base.hero.SetAnimator("SheildState", 2, 4, 2);
						this.Shield_Object.SetActive(false);
						this.huzhao.gameObject.SetActive(false);
						this.huzhaoPo.gameObject.SetActive(false);
						this.huzhaoSui.gameObject.SetActive(true);
						base.hero.Clear_Adhere_Npc("body2_dun");
						((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(1);
						((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldBreakSound();
						base.StartCoroutine(this.closeDunSui());
						this.SendShieldData(0, Vector3.zero, Vector3.zero);
					}
				}
			}
		}
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x0015FB94 File Offset: 0x0015DD94
	private IEnumerator closeDunSui()
	{
		yield return new WaitForSeconds(0.5f);
		this.huzhaoSui.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x0015FBA4 File Offset: 0x0015DDA4
	private void SendShieldData(int type, Vector3 pos, Vector3 relesPos)
	{
		bool flag = type == 0;
		if (flag)
		{
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(type);
		arrayList.Add(new ArrayList
		{
			(int)(pos.x * 100f),
			(int)(pos.y * 100f),
			(int)(pos.z * 100f)
		});
		arrayList.Add(new ArrayList
		{
			(int)(relesPos.x * 100f),
			(int)(relesPos.y * 100f),
			(int)(relesPos.z * 100f)
		});
		base.hero.SendMessageToServer(2100001, arrayList);
	}

	// Token: 0x060026A6 RID: 9894 RVA: 0x0015FCA8 File Offset: 0x0015DEA8
	private void Update()
	{
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
			bool flag2 = this.lifeTime > 0f;
			if (flag2)
			{
				this.lifeTime -= Time.deltaTime;
			}
			else
			{
				bool activeSelf = this.Shield_Object.activeSelf;
				if (activeSelf)
				{
					this.SendShieldData(0, Vector3.zero, Vector3.zero);
					((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(1);
					this.Shield_Object.SetActive(false);
					this.huzhao.gameObject.SetActive(false);
					this.huzhaoPo.gameObject.SetActive(false);
					((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
					base.hero.Clear_Adhere_Npc("body2_dun");
				}
			}
		}
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x0015FD90 File Offset: 0x0015DF90
	public GameObject getShieldCol()
	{
		return this.Shield_Object.GetComponentInChildren<MeshCollider>().gameObject;
	}

	// Token: 0x060026A8 RID: 9896 RVA: 0x0015FDB2 File Offset: 0x0015DFB2
	public void SetDunState()
	{
		((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
		this.lifeTime = 0f;
	}

	// Token: 0x0400222E RID: 8750
	private SkillDaJiHudunMe.Break_State _Shield_Break_State;

	// Token: 0x0400222F RID: 8751
	private GameObject _Shield_Object;

	// Token: 0x04002230 RID: 8752
	private GameObject tempBodyTrans;

	// Token: 0x04002231 RID: 8753
	private Transform huzhao;

	// Token: 0x04002232 RID: 8754
	private Transform huzhaoPo;

	// Token: 0x04002233 RID: 8755
	private Transform huzhaoSui;

	// Token: 0x04002234 RID: 8756
	private Transform dun;

	// Token: 0x04002235 RID: 8757
	private Vector3 forward;

	// Token: 0x04002236 RID: 8758
	private float lifeTime;

	// Token: 0x04002237 RID: 8759
	private bool canCreateShield;

	// Token: 0x04002238 RID: 8760
	private float GemFactor = 1f;

	// Token: 0x02000614 RID: 1556
	public enum Break_State
	{
		// Token: 0x04002F51 RID: 12113
		State_0,
		// Token: 0x04002F52 RID: 12114
		State_1,
		// Token: 0x04002F53 RID: 12115
		State_2,
		// Token: 0x04002F54 RID: 12116
		State_3,
		// Token: 0x04002F55 RID: 12117
		State_4,
		// Token: 0x04002F56 RID: 12118
		State_5,
		// Token: 0x04002F57 RID: 12119
		Break
	}
}
