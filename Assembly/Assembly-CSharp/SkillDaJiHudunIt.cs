using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class SkillDaJiHudunIt : SkillBaseIt
{
	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x0600268B RID: 9867 RVA: 0x0015E244 File Offset: 0x0015C444
	// (set) Token: 0x0600268C RID: 9868 RVA: 0x0015E25C File Offset: 0x0015C45C
	public SkillDaJiHudunIt.Break_State Shield_Break_State
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

	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x0600268D RID: 9869 RVA: 0x0015E268 File Offset: 0x0015C468
	// (set) Token: 0x0600268E RID: 9870 RVA: 0x0015E4E4 File Offset: 0x0015C6E4
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

	// Token: 0x0600268F RID: 9871 RVA: 0x0015E4EE File Offset: 0x0015C6EE
	private void Start()
	{
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x0015E4F1 File Offset: 0x0015C6F1
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.canCreateShield = false;
		this.lifeTime = 0f;
		this.dunHaveCreate = false;
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x0015E516 File Offset: 0x0015C716
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.forward = CameraControl.Instance.transform.forward;
		this.forward.y = 0f;
		this.canCreateShield = true;
		this.OneSkillFinish();
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x0015E555 File Offset: 0x0015C755
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x0015E560 File Offset: 0x0015C760
	public void createShileObject(Vector3 releasePos, Vector3 pos)
	{
		bool flag = this.Shield_Object != null && !this.dunHaveCreate;
		if (flag)
		{
			((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = true;
			this.dunHaveCreate = true;
			base.hero.Clear_Adhere_Npc("body2_dun");
			this.canCreateShield = false;
			this.forward = Vector3.Normalize(pos - releasePos);
			this.forward.y = 0f;
			this.lifeTime = 20f;
			this.Shield_Object.SetActive(true);
			this.Shield_Object.GetComponentInChildren<MeshCollider>().enabled = true;
			this.Shield_Object.transform.position = pos;
			this.Shield_Object.transform.up = Vector3.up;
			this.Shield_Object.transform.forward = this.forward;
			((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(0);
			this.huzhao.gameObject.SetActive(true);
			this.dun.transform.position = pos;
			this.dun.transform.up = Vector3.up;
			this.dun.transform.forward = this.forward;
			this.dun.transform.parent = this.Shield_Object.transform.parent;
		}
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x0015E6DC File Offset: 0x0015C8DC
	private void Update()
	{
		bool flag = !(this.Shield_Object == null);
		if (flag)
		{
			bool flag2 = this.dajiAnim == null;
			if (flag2)
			{
				this.dajiAnim = base.hero.tCharAnimationTps.animator;
			}
			bool flag3 = this.lifeTime > 0f;
			if (flag3)
			{
				this.lifeTime -= Time.deltaTime;
			}
			else
			{
				bool flag4 = this.Shield_Object.activeSelf || this.openShield;
				if (flag4)
				{
					this.Shield_Object.SetActive(false);
					this.huzhao.gameObject.SetActive(false);
					this.huzhaoPo.gameObject.SetActive(false);
					((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
					((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(1);
					this.dunHaveCreate = false;
					base.hero.Clear_Adhere_Npc("body2_dun");
				}
			}
			bool flag5 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
			if (flag5)
			{
				this.UpdateFilmShiledState();
			}
		}
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x0015E840 File Offset: 0x0015CA40
	private void UpdateFilmShiledState()
	{
		bool flag = this.dajiAnim.GetInteger("SheildState") == 1;
		if (flag)
		{
			bool flag2 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_1;
			if (flag2)
			{
				this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_1;
				this.huzhao.gameObject.SetActive(true);
				this.huzhaoPo.gameObject.SetActive(false);
				this.openShield = true;
			}
		}
		else
		{
			bool flag3 = this.dajiAnim.GetInteger("SheildState") == 2;
			if (flag3)
			{
				bool flag4 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_2;
				if (flag4)
				{
					this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_2;
					this.huzhao.gameObject.SetActive(true);
					this.huzhaoPo.gameObject.SetActive(true);
					this.openShield = true;
				}
			}
			else
			{
				bool flag5 = this.dajiAnim.GetInteger("SheildState") == 3;
				if (flag5)
				{
					bool flag6 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_3;
					if (flag6)
					{
						this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_3;
						this.openShield = true;
					}
				}
				else
				{
					bool flag7 = this.dajiAnim.GetInteger("SheildState") == 4 && this.Shield_Break_State != SkillDaJiHudunIt.Break_State.Break;
					if (flag7)
					{
						this.Shield_Break_State = SkillDaJiHudunIt.Break_State.Break;
						this.Shield_Object.SetActive(false);
						this.huzhao.gameObject.SetActive(false);
						this.huzhaoPo.gameObject.SetActive(false);
						this.huzhaoSui.gameObject.SetActive(true);
						base.hero.Clear_Adhere_Npc("body2_dun");
						this.openShield = true;
						base.StartCoroutine(this.closeDunSui());
					}
				}
			}
		}
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x0015E9F4 File Offset: 0x0015CBF4
	public void Update_Shield_Data(ArrayList _Parameters)
	{
		bool flag = _Parameters == null || _Parameters.Count < 2;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)_Parameters[1];
			bool flag2 = arrayList != null && arrayList.Count >= 3;
			if (flag2)
			{
				((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy = int.Parse(arrayList[1].ToString());
				((ISkillDataSkillDaJiHudun)this.ISD).Shield_Max_Energy = int.Parse(arrayList[2].ToString());
				this.GemFactor = (float)(int.Parse(arrayList[2].ToString()) / 4000) + 0.001f;
				bool flag3 = int.Parse(arrayList[0].ToString()) == 1;
				if (flag3)
				{
					((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = true;
				}
				else
				{
					bool flag4 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 0f && int.Parse(arrayList[0].ToString()) == 0;
					if (flag4)
					{
						((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
						this.lifeTime = 0f;
					}
				}
			}
			bool flag5 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 2705f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 4000f * this.GemFactor;
			if (flag5)
			{
				bool flag6 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_1 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State && this.Shield_Object != null;
				if (flag6)
				{
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					bool flag7 = _Parameters.Count > 2;
					if (flag7)
					{
						ArrayList arrayList2 = (ArrayList)_Parameters[2];
						bool flag8 = arrayList2 != null && arrayList2.Count >= 3;
						if (flag8)
						{
							zero = new Vector3((float)((int)arrayList2[0]) / 100f, (float)((int)arrayList2[1]) / 100f, (float)((int)arrayList2[2]) / 100f);
						}
					}
					bool flag9 = _Parameters.Count > 3;
					if (flag9)
					{
						ArrayList arrayList3 = (ArrayList)_Parameters[3];
						bool flag10 = arrayList3 != null && arrayList3.Count >= 3;
						if (flag10)
						{
							zero2 = new Vector3((float)((int)arrayList3[0]) / 100f, (float)((int)arrayList3[1]) / 100f, (float)((int)arrayList3[2]) / 100f);
						}
					}
					this.createShileObject(zero2, zero);
					this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_1;
					this.huzhao.gameObject.SetActive(true);
					this.huzhaoPo.gameObject.SetActive(false);
				}
			}
			else
			{
				bool flag11 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 1305f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 2704f * this.GemFactor;
				if (flag11)
				{
					bool flag12 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_2 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State && this.Shield_Object != null;
					if (flag12)
					{
						Vector3 zero3 = Vector3.zero;
						Vector3 zero4 = Vector3.zero;
						bool flag13 = _Parameters.Count > 2;
						if (flag13)
						{
							ArrayList arrayList4 = (ArrayList)_Parameters[2];
							bool flag14 = arrayList4 != null && arrayList4.Count >= 3;
							if (flag14)
							{
								zero3 = new Vector3((float)((int)arrayList4[0]) / 100f, (float)((int)arrayList4[1]) / 100f, (float)((int)arrayList4[2]) / 100f);
							}
						}
						bool flag15 = _Parameters.Count > 3;
						if (flag15)
						{
							ArrayList arrayList5 = (ArrayList)_Parameters[3];
							bool flag16 = arrayList5 != null && arrayList5.Count >= 3;
							if (flag16)
							{
								zero4 = new Vector3((float)((int)arrayList5[0]) / 100f, (float)((int)arrayList5[1]) / 100f, (float)((int)arrayList5[2]) / 100f);
							}
						}
						this.createShileObject(zero4, zero3);
						this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_2;
						this.huzhao.gameObject.SetActive(true);
						this.huzhaoPo.gameObject.SetActive(true);
					}
				}
				else
				{
					bool flag17 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy >= 1f * this.GemFactor && (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 1304f * this.GemFactor;
					if (flag17)
					{
						bool flag18 = this.Shield_Break_State != SkillDaJiHudunIt.Break_State.State_3 && ((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State && this.Shield_Object != null;
						if (flag18)
						{
							Vector3 zero5 = Vector3.zero;
							Vector3 zero6 = Vector3.zero;
							bool flag19 = _Parameters.Count > 2;
							if (flag19)
							{
								ArrayList arrayList6 = (ArrayList)_Parameters[2];
								bool flag20 = arrayList6 != null && arrayList6.Count >= 3;
								if (flag20)
								{
									zero5 = new Vector3((float)((int)arrayList6[0]) / 100f, (float)((int)arrayList6[1]) / 100f, (float)((int)arrayList6[2]) / 100f);
								}
							}
							bool flag21 = _Parameters.Count > 3;
							if (flag21)
							{
								ArrayList arrayList7 = (ArrayList)_Parameters[3];
								bool flag22 = arrayList7 != null && arrayList7.Count >= 3;
								if (flag22)
								{
									zero6 = new Vector3((float)((int)arrayList7[0]) / 100f, (float)((int)arrayList7[1]) / 100f, (float)((int)arrayList7[2]) / 100f);
								}
							}
							this.createShileObject(zero6, zero5);
							this.Shield_Break_State = SkillDaJiHudunIt.Break_State.State_3;
							this.huzhao.gameObject.SetActive(true);
							this.huzhaoPo.gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag23 = (float)((ISkillDataSkillDaJiHudun)this.ISD).Shield_Current_Energy <= 0f && this.Shield_Break_State != SkillDaJiHudunIt.Break_State.Break && this.Shield_Object != null;
						if (flag23)
						{
							this.Shield_Break_State = SkillDaJiHudunIt.Break_State.Break;
							this.Shield_Object.SetActive(false);
							this.huzhao.gameObject.SetActive(false);
							this.huzhaoPo.gameObject.SetActive(false);
							this.huzhaoSui.gameObject.SetActive(true);
							this.dunHaveCreate = false;
							((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
							((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldContinueSound(1);
							((IHeroDataDaJi)base.hero.heroBase.IHD).PlayDaJiShieldBreakSound();
							base.StartCoroutine(this.closeDunSui());
						}
					}
				}
			}
		}
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x0015F16E File Offset: 0x0015D36E
	private IEnumerator closeDunSui()
	{
		yield return new WaitForSeconds(0.5f);
		this.huzhaoSui.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x0015F180 File Offset: 0x0015D380
	public GameObject getShieldCol()
	{
		bool flag = this.Shield_Object != null;
		GameObject result;
		if (flag)
		{
			result = this.Shield_Object.GetComponentInChildren<MeshCollider>().gameObject;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x0015F1B7 File Offset: 0x0015D3B7
	public void SetDunState()
	{
		((ISkillDataSkillDaJiHudun)this.ISD).Shield_Call_State = false;
	}

	// Token: 0x0400221F RID: 8735
	private SkillDaJiHudunIt.Break_State _Shield_Break_State;

	// Token: 0x04002220 RID: 8736
	private GameObject _Shield_Object;

	// Token: 0x04002221 RID: 8737
	private GameObject tempBodyTrans;

	// Token: 0x04002222 RID: 8738
	private Transform huzhao;

	// Token: 0x04002223 RID: 8739
	private Transform huzhaoPo;

	// Token: 0x04002224 RID: 8740
	private Transform huzhaoSui;

	// Token: 0x04002225 RID: 8741
	private Transform dun;

	// Token: 0x04002226 RID: 8742
	private Animator dajiAnim;

	// Token: 0x04002227 RID: 8743
	public bool dunHaveCreate;

	// Token: 0x04002228 RID: 8744
	private Vector3 forward;

	// Token: 0x04002229 RID: 8745
	private float lifeTime;

	// Token: 0x0400222A RID: 8746
	private Vector3 playerPos;

	// Token: 0x0400222B RID: 8747
	private bool canCreateShield;

	// Token: 0x0400222C RID: 8748
	private bool openShield;

	// Token: 0x0400222D RID: 8749
	private float GemFactor = 1f;

	// Token: 0x02000612 RID: 1554
	public enum Break_State
	{
		// Token: 0x04002F46 RID: 12102
		State_0,
		// Token: 0x04002F47 RID: 12103
		State_1,
		// Token: 0x04002F48 RID: 12104
		State_2,
		// Token: 0x04002F49 RID: 12105
		State_3,
		// Token: 0x04002F4A RID: 12106
		State_4,
		// Token: 0x04002F4B RID: 12107
		State_5,
		// Token: 0x04002F4C RID: 12108
		Break
	}
}
