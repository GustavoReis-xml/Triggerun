using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000196 RID: 406
public class HeroLeiZhenZiMe : HeroBaseMe
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060012FA RID: 4858 RVA: 0x000B5E20 File Offset: 0x000B4020
	// (set) Token: 0x060012FB RID: 4859 RVA: 0x000B5E38 File Offset: 0x000B4038
	public bool Power_State
	{
		get
		{
			return this._Power_State;
		}
		set
		{
			this._Power_State = value;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x060012FC RID: 4860 RVA: 0x000B5E44 File Offset: 0x000B4044
	// (set) Token: 0x060012FD RID: 4861 RVA: 0x000B5E5C File Offset: 0x000B405C
	public bool Multistage_Jump_State
	{
		get
		{
			return this._Multistage_Jump_State;
		}
		set
		{
			this._Multistage_Jump_State = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x060012FE RID: 4862 RVA: 0x000B5E68 File Offset: 0x000B4068
	// (set) Token: 0x060012FF RID: 4863 RVA: 0x000B5E80 File Offset: 0x000B4080
	public float Space_Duration_Time
	{
		get
		{
			return this._Space_Duration_Time;
		}
		set
		{
			this._Space_Duration_Time = value;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06001300 RID: 4864 RVA: 0x000B5E8C File Offset: 0x000B408C
	// (set) Token: 0x06001301 RID: 4865 RVA: 0x000B5EA4 File Offset: 0x000B40A4
	public int Current_Bullet_Number
	{
		get
		{
			return this._Current_Bullet_Number;
		}
		set
		{
			this._Current_Bullet_Number = value;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06001302 RID: 4866 RVA: 0x000B5EB0 File Offset: 0x000B40B0
	// (set) Token: 0x06001303 RID: 4867 RVA: 0x000B5F04 File Offset: 0x000B4104
	public GameObject Bullet_Parent_Object
	{
		get
		{
			bool flag = this._Bullet_Parent_Object == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.gameObject, "zidan_xuanzhuan");
				bool flag2 = transform != null;
				if (flag2)
				{
					this._Bullet_Parent_Object = transform.gameObject;
				}
			}
			return this._Bullet_Parent_Object;
		}
		set
		{
			this._Bullet_Parent_Object = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06001304 RID: 4868 RVA: 0x000B5F10 File Offset: 0x000B4110
	// (set) Token: 0x06001305 RID: 4869 RVA: 0x000B6054 File Offset: 0x000B4254
	public GameObject[] Bullet_Object_Array
	{
		get
		{
			bool flag = this._Bullet_Object_Array == null;
			if (flag)
			{
				this._Bullet_Object_Array = new GameObject[6];
				bool flag2 = this.Bullet_Parent_Object == null;
				if (flag2)
				{
					return this._Bullet_Object_Array;
				}
				foreach (object obj in this.Bullet_Parent_Object.transform)
				{
					Transform transform = (Transform)obj;
					string name = transform.gameObject.name;
					string a = name;
					int num;
					if (!(a == "1"))
					{
						if (!(a == "2"))
						{
							if (!(a == "3"))
							{
								if (!(a == "4"))
								{
									if (!(a == "5"))
									{
										if (!(a == "6"))
										{
											continue;
										}
										num = 5;
									}
									else
									{
										num = 4;
									}
								}
								else
								{
									num = 3;
								}
							}
							else
							{
								num = 2;
							}
						}
						else
						{
							num = 1;
						}
					}
					else
					{
						num = 0;
					}
					this._Bullet_Object_Array[num] = transform.gameObject;
				}
			}
			return this._Bullet_Object_Array;
		}
		set
		{
			this._Bullet_Object_Array = value;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06001306 RID: 4870 RVA: 0x000B6060 File Offset: 0x000B4260
	// (set) Token: 0x06001307 RID: 4871 RVA: 0x000B6078 File Offset: 0x000B4278
	public bool Lerp_Execute_State
	{
		get
		{
			return this._Lerp_Execute_State;
		}
		set
		{
			this._Lerp_Execute_State = value;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06001308 RID: 4872 RVA: 0x000B6084 File Offset: 0x000B4284
	// (set) Token: 0x06001309 RID: 4873 RVA: 0x000B609C File Offset: 0x000B429C
	public Vector3 Lerp_Target
	{
		get
		{
			return this._Lerp_Target;
		}
		set
		{
			this._Lerp_Target = value;
		}
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000B60A6 File Offset: 0x000B42A6
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.HeroMask = this.getMiaoZhunLayer(net.nForce);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000B60C4 File Offset: 0x000B42C4
	public void OnLvJing(bool isShow = true)
	{
		bool flag = this.colorc != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.colorc);
			this.colorc = null;
		}
		if (isShow)
		{
			this.colorc = CameraControl.Instance.controlCamera.gameObject.AddComponent<ColorCorrectionCurves>();
			this.colorc.colorCorrectionCurvesShader = Shader.Find("Hidden/ColorCorrectionCurves");
			this.colorc.colorCorrectionSelectiveShader = Shader.Find("Hidden/ColorCorrectionSelective");
			this.colorc.simpleColorCorrectionCurvesShader = Shader.Find("Hidden/ColorCorrectionCurvesSimple");
			this.colorc.saturation = 0.3f;
		}
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000B6168 File Offset: 0x000B4368
	private void InitEneyGy()
	{
		((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Max_Energy = 70f;
		bool flag = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy < -90f || !this.energyInitialized;
		if (flag)
		{
			((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy = 70f;
			this.energyInitialized = true;
		}
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000B61F0 File Offset: 0x000B43F0
	protected override void Update()
	{
		base.Update();
		bool lerp_Execute_State = this.Lerp_Execute_State;
		if (lerp_Execute_State)
		{
			this.Rotation();
		}
		this.Detection_Bullet_Number();
		bool flag = this.InputCheck();
		if (flag)
		{
			bool flag2 = Input_Manager.Get_Button_Down("superskill");
			if (flag2)
			{
				bool flag3 = base.GetStat("huanying") > 0 || !base.hero.playerDazhao.canDo || base.hero.isDead || base.hero.wInput.notInput || !base.hero.superSkill(null);
				if (flag3)
				{
					return;
				}
				base.hero.tChar.onAir = true;
				base.hero.wInput.notInput = true;
				base.hero.tChar.hangInAir = true;
				base.hero.charMove.notMove = true;
				base.hero.bePlayActive = false;
				base.hero.SetAnimator("Reload", 0, false, 1);
				this.OnLvJing(true);
			}
			this.InitEneyGy();
			this.Keyboard_Control();
			this.GetRayHitObj();
		}
		else
		{
			this.Multistage_Jump_State = false;
			this.Multistage_Jump_End(1);
		}
		bool flag4 = base.GetStat("huanying") > 0;
		if (flag4)
		{
			bool flag5 = !this.isSupper && Input.GetMouseButtonUp(0);
			if (flag5)
			{
				this.allTarget = this.doShootTarget();
				this.isSupper = true;
				this.waiteTime = Time.time;
			}
			bool mouseButtonUp = Input.GetMouseButtonUp(1);
			if (mouseButtonUp)
			{
				base.StopCoroutine(this.coroutine);
				this.BreakSupperSkill();
			}
		}
		bool flag6 = !this.isSupper || Time.time - this.waiteTime <= 0.1f;
		if (!flag6)
		{
			bool flag7 = this.allTarget.Count > 0;
			if (flag7)
			{
				NetLiving netLiving = (NetLiving)this.allTarget[0];
				bool flag8 = netLiving != null && !(netLiving.defaultMesh == null);
				if (flag8)
				{
					int num = ((CWeaponControlMe)base.hero.wc).WeaponFireBySkill(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dogun[0], "0", netLiving.defaultMesh.transform.position, netLiving.defaultMesh);
					bool flag9 = num == 1;
					if (flag9)
					{
						this.allTarget.RemoveAt(0);
					}
				}
			}
			else
			{
				base.StopCoroutine(this.coroutine);
				this.BreakSupperSkill();
			}
		}
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000B64A4 File Offset: 0x000B46A4
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		((HeroBaseMe)base.hero.heroBase).SetStat("huanying", 1);
		base.hero.bePlayActive = false;
		this.Multistage_Jump_State = false;
		this.coroutine = this.SkillFinish();
		base.StartCoroutine(this.coroutine);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000B651D File Offset: 0x000B471D
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(6f);
		bool flag = base.GetStat("huanying") != -1 && base.hero.playerCommon.m_nHunMi != 1;
		if (flag)
		{
			this.SuperSkillFinish();
		}
		yield break;
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000B652C File Offset: 0x000B472C
	public override void SuperSkillFinish()
	{
		this.isSupper = false;
		base.SuperSkillFinish();
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		base.hero.tChar.onAir = false;
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.hero.tChar.hangInAir = false;
		base.hero.bePlayActive = true;
		((HeroBaseMe)base.hero.heroBase).SetStat("huanying", -1);
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.ClearLzzEffect();
		}
		this.szLockUser.Clear();
		this.LockUserHp.Clear();
		this.OnLvJing(false);
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x000B6611 File Offset: 0x000B4811
	public override void BreakSupperSkill()
	{
		base.BreakSupperSkill();
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x000B661C File Offset: 0x000B481C
	private void GetRayHitObj()
	{
		Vector3 direction = Vector3.zero;
		int num = 0;
		ArrayList arrayList = new ArrayList();
		bool flag = base.GetStat("huanying") == -1 || Time.time - this.Super_Duration_Time < 0.1f;
		if (!flag)
		{
			this.Super_Duration_Time = Time.time;
			foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
			{
				NetLiving netLiving = (NetLiving)keyValuePair.Value;
				int netid = netLiving.getNetid();
				bool flag2 = netLiving.defaultMesh == null || (MatchManager.GetInstance().m_pFbData.nType != 6 && !netLiving.bePlayer) || netLiving.nForce == base.hero.nForce;
				if (!flag2)
				{
					bool flag3 = MatchManager.GetInstance().m_nMatchType == 3 && num >= 5;
					if (flag3)
					{
						break;
					}
					num++;
					bool flag4 = this.getScreenVector(netLiving.defaultMesh.transform.position) == new Vector3(-1f, -1f, -1f) || netLiving.isDead || netLiving.heroBase.IsHeroInvi();
					if (flag4)
					{
						bool flag5 = this.szLockUser.IndexOf(netid) != -1;
						if (flag5)
						{
							bool flag6 = netLiving.heroBase.IsHeroInvi() && gameuiCon.inst != null;
							if (flag6)
							{
								gameuiCon.inst.mainMsgUI.DelOneLzzEffect(netid);
							}
							this.szLockUser.Remove(netid);
							arrayList.Add(netid);
							arrayList.Add(0);
						}
					}
					else
					{
						direction = (netLiving.defaultMesh.transform.position - base.hero.defaultMesh.transform.position).normalized;
						Ray ray = new Ray(base.hero.defaultMesh.transform.position, direction);
						RaycastHit raycastHit;
						bool flag7 = Physics.Raycast(ray, out raycastHit, 200f, this.HeroMask);
						if (flag7)
						{
							bool flag8 = !raycastHit.collider.tag.Contains("can_damage");
							if (!flag8)
							{
								bool flag9 = this.szLockUser.IndexOf(netid) == -1;
								if (flag9)
								{
									this.szLockUser.Add(netid);
								}
								bool flag10 = gameuiCon.inst != null;
								if (flag10)
								{
									int nowHp = netLiving.playerCommon.GetNowHp();
									bool flag11 = !this.LockUserHp.ContainsKey(netid);
									int num2;
									if (flag11)
									{
										num2 = 70;
										Dictionary<int, int[]> lockUserHp = this.LockUserHp;
										int key = netid;
										int[] array = new int[2];
										array[0] = 70;
										lockUserHp.Add(key, array);
									}
									else
									{
										this.LockUserHp[netid][0] += 70;
										num2 = this.LockUserHp[netid][0];
									}
									bool flag12 = num2 >= nowHp;
									float sca;
									if (flag12)
									{
										sca = 0.5f;
										this.LockUserHp[netid][0] = nowHp;
										bool flag13 = this.LockUserHp[netid][1] == 0;
										if (flag13)
										{
											this.PlayCDFinishSound("skillvoice_leizhenzi_dazhao_lock");
											this.LockUserHp[netid][1] = 1;
										}
									}
									else
									{
										sca = 0.5f + 0.5f * (float)(nowHp - num2) / (float)nowHp;
									}
									gameuiCon.inst.mainMsgUI.OnAddSuperSkillEffect(netid, sca);
								}
								arrayList.Add(netid);
								arrayList.Add(1);
							}
						}
						else
						{
							bool flag14 = this.szLockUser.IndexOf(netid) != -1;
							if (flag14)
							{
								this.szLockUser.Remove(netid);
								arrayList.Add(netid);
								arrayList.Add(0);
							}
						}
					}
				}
			}
			bool flag15 = arrayList.Count > 0 && !this.isSupper;
			if (flag15)
			{
				base.hero.SendMessageToServer(50007, arrayList);
			}
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x000B6AB4 File Offset: 0x000B4CB4
	private ArrayList doShootTarget()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int count = this.szLockUser.Count;
		for (int i = 0; i < count; i++)
		{
			string key = this.szLockUser[i].ToString();
			bool flag = !GameRoot.SceneSocket.unFsorket.NetObjTable.ContainsKey(key);
			if (!flag)
			{
				NetObj netObj = GameRoot.SceneSocket.unFsorket.NetObjTable[key];
				bool flag2 = netObj != null;
				if (flag2)
				{
					NetLiving netLiving = (NetLiving)netObj;
					arrayList.Add(netLiving);
					int netid = netLiving.getNetid();
					bool flag3 = arrayList2.IndexOf(netid) == -1;
					if (flag3)
					{
						arrayList2.Add(netid);
						arrayList2.Add(0);
					}
				}
			}
		}
		base.hero.SendMessageToServer(50007, arrayList2);
		return arrayList;
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000B6BB8 File Offset: 0x000B4DB8
	private Vector3 getScreenVector(Vector3 bAt)
	{
		Vector3 result = new Vector3(-1f, -1f, -1f);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		Vector3 vector = CameraControl.Instance.controlCamera.WorldToScreenPoint(bAt);
		bool flag = vector.x >= 0f && vector.x <= num && vector.y >= 0f && vector.y <= num2 && vector.z > 0f;
		if (flag)
		{
			Vector2 sizeDelta = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
			float num3 = vector.x / num;
			float num4 = vector.y / num2;
			float x = sizeDelta.x * num3 - sizeDelta.x / 2f;
			float y = sizeDelta.y * num4 - sizeDelta.y / 2f;
			result.x = x;
			result.y = y;
			result.z = 0f;
		}
		return result;
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
	private LayerMask getMiaoZhunLayer(int force)
	{
		bool flag = force == 2;
		LayerMask result;
		if (flag)
		{
			result = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force1_npc"));
		}
		else
		{
			result = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force2_npc"));
		}
		return result;
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000B6D6C File Offset: 0x000B4F6C
	public override void Freeze(bool m_bool)
	{
		bool flag = !m_bool;
		if (flag)
		{
			this.SuperSkillFinish();
			bool flag2 = base.hero.GetAnimatorInteger("AttackState") == 2;
			if (flag2)
			{
				base.hero.SetAnimator("AttackState", 2, 0, 1);
			}
		}
		bool flag3 = !m_bool;
		if (flag3)
		{
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000B6DC8 File Offset: 0x000B4FC8
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
		bool flag = !m_bool;
		if (flag)
		{
		}
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000B6DE8 File Offset: 0x000B4FE8
	private void OnEnable()
	{
		this._Bullet_Object_Array = null;
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000B6DF4 File Offset: 0x000B4FF4
	private void Keyboard_Control()
	{
		bool flag = base.GetStat("huanying") > 0 || base.hero.playerRuntimeData.bSuperJump || base.hero.isDead;
		if (!flag)
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			if (flag2)
			{
				this.Multistage_Jump_State = false;
			}
			else
			{
				bool flag3 = base.hero.playerCommon.m_nHunMi == 1;
				if (flag3)
				{
					this.Multistage_Jump_State = false;
					this.Multistage_Jump_End(2);
				}
				else
				{
					bool flag4 = base.hero.nHuDongState >= 50;
					if (!flag4)
					{
						this.Multistage_Jump_State = false;
						bool multistage_Jump_State = this.Multistage_Jump_State;
						if (multistage_Jump_State)
						{
							bool flag5 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy > 0.1f;
							if (flag5)
							{
								this.Multistage_Jump_Start();
							}
							else
							{
								this.Multistage_Jump_End(3);
							}
						}
						else
						{
							bool flag6 = Input_Manager.Get_Axis("Jump") > 0f;
							if (flag6)
							{
								this.Multistage_Jump_State = true;
							}
							else
							{
								this.Multistage_Jump_State = false;
								this.is_Recover = false;
							}
							bool multistage_Jump_State2 = this.Multistage_Jump_State;
							if (multistage_Jump_State2)
							{
								bool onGround = base.hero.tChar.onGround;
								if (onGround)
								{
									this._Jump_State = true;
								}
								bool jump_State = this._Jump_State;
								if (jump_State)
								{
									bool flag7 = this.Space_Duration_Time < 1f;
									if (flag7)
									{
										this.Space_Duration_Time = Time.time + 0.3f;
									}
									bool flag8 = Time.time < this.Space_Duration_Time;
									if (flag8)
									{
										return;
									}
								}
								this._Jump_State = false;
								this.Space_Duration_Time = 0f;
								bool flag9 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy >= 0.4f && !this.is_Recover;
								if (flag9)
								{
									this.Multistage_Jump_Start();
								}
								else
								{
									this.Multistage_Jump_End(4);
								}
							}
							else
							{
								this._Jump_State = false;
								this.is_Recover = false;
								this.Space_Duration_Time = 0f;
								this.Multistage_Jump_End(5);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000B7035 File Offset: 0x000B5235
	private void FixedUpdate()
	{
		this.OnResetEnergy();
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000B7040 File Offset: 0x000B5240
	private void OnResetEnergy()
	{
		bool flag = this.Multistage_Jump_State && !this._Jump_State && !this.is_Recover;
		if (flag)
		{
			this.nRecover = 0f;
			((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Energy_Deduct_State = true;
			((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy -= 0.1f;
			bool flag2 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy < 0.1f;
			if (flag2)
			{
				((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy = 0f;
				this.is_Recover = true;
				return;
			}
		}
		bool flag3 = this.Multistage_Jump_State && !this.is_Recover;
		if (!flag3)
		{
			bool flag4 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Energy_Deduct_State && !this.is_Recover;
			if (flag4)
			{
				bool flag5 = this.nRecover == 0f;
				if (flag5)
				{
					this.nRecover = Time.time;
				}
				bool flag6 = Time.time - this.nRecover < 0.2f;
				if (flag6)
				{
					return;
				}
			}
			((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Energy_Deduct_State = false;
			bool flag7 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy == ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Max_Energy;
			if (!flag7)
			{
				bool flag8 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy + 0.09f > ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Max_Energy;
				if (flag8)
				{
					((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Max_Energy;
				}
				else
				{
					bool flag9 = this.is_Recover;
					if (flag9)
					{
						((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy += 0.01f;
						bool flag10 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy >= 0.4f;
						if (flag10)
						{
							this.is_Recover = false;
						}
					}
					else
					{
						((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy += 0.09f;
					}
				}
			}
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000B7310 File Offset: 0x000B5510
	private void Multistage_Jump_Start()
	{
		bool flag = !this.Power_State;
		if (flag)
		{
			this.Power_State = true;
			((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(true);
			((TBCharacterThirdMe)base.hero.tChar).SetjumpEndTime(0.1f);
		}
		else
		{
			((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.up * 27f, 0.1f, false, 5f, 1000f);
			this.PlayCDFinishSound("skillvoice_leizhenzi_fly_loop");
			bool flag2 = !base.hero.charMove.state.fly;
			if (flag2)
			{
				base.hero.charMove.state.fly = true;
			}
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000B73E0 File Offset: 0x000B55E0
	private void Multistage_Jump_End(int nFrom = 0)
	{
		bool power_State = this.Power_State;
		if (power_State)
		{
			this.StopCDFinishSound("skillvoice_leizhenzi_fly_loop");
		}
		this.Power_State = false;
		bool flag = !base.hero.charMove.state.fly;
		if (!flag)
		{
			((TBCharacterThirdMe)base.hero.tChar).SetForceTime(0f);
			bool fly = base.hero.charMove.state.fly;
			if (fly)
			{
				bool flag2 = base.hero.tChar.rig.velocity.y < 0f;
				if (flag2)
				{
					base.hero.charMove.state.fly = false;
				}
				bool onGround = base.hero.tChar.onGround;
				if (onGround)
				{
					base.hero.charMove.state.fly = false;
				}
			}
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000B74D4 File Offset: 0x000B56D4
	public override void OnDeath()
	{
		base.OnDeath();
		this.Multistage_Jump_State = false;
		base.hero.charMove.state.fly = false;
		base.hero.charMove.canJump = true;
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.wInput.notInput = false;
		this.Multistage_Jump_End(0);
		((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Current_Energy = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).Max_Energy;
		bool flag = this.coroutine != null;
		if (flag)
		{
			base.StopCoroutine(this.coroutine);
		}
		this.SuperSkillFinish();
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x000B759C File Offset: 0x000B579C
	public override void BulletFillBegin()
	{
		base.BulletFillBegin();
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Lerp_Target = this.Bullet_Parent_Object.transform.localEulerAngles;
			this.Bullet_Parent_Object.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000B7604 File Offset: 0x000B5804
	public override void BulletFillBreak()
	{
		base.BulletFillBreak();
		bool flag = !(this.Lerp_Target == Vector3.zero) && !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Bullet_Parent_Object.transform.localEulerAngles = this.Lerp_Target;
		}
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x000B765C File Offset: 0x000B585C
	public override void BulletFilled()
	{
		base.BulletFilled();
		this.Initialize_Bullet_Display();
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Bullet_Parent_Object.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000B76B4 File Offset: 0x000B58B4
	public void Initialize_Bullet_Display()
	{
		GameObject[] bullet_Object_Array = this.Bullet_Object_Array;
		foreach (GameObject gameObject in bullet_Object_Array)
		{
			bool flag = !(gameObject == null);
			if (flag)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x000B76FC File Offset: 0x000B58FC
	private void Detection_Bullet_Number()
	{
		bool flag = base.hero.wc == null;
		if (!flag)
		{
			bool flag2 = this.Current_Bullet_Number < 0;
			if (flag2)
			{
				this.Current_Bullet_Number = ((CWeaponControlMe)base.hero.wc).bulletNow;
			}
			bool flag3 = this.Current_Bullet_Number != ((CWeaponControlMe)base.hero.wc).bulletNow && this.Current_Bullet_Number >= 0;
			if (flag3)
			{
				this.Current_Bullet_Number = ((CWeaponControlMe)base.hero.wc).bulletNow;
				bool flag4 = this.Current_Bullet_Number < 6 && !(this.Bullet_Object_Array[this.Current_Bullet_Number] == null);
				if (flag4)
				{
					this.Bullet_Object_Array[this.Current_Bullet_Number].SetActive(false);
					base.StartCoroutine(this.Lerp_Cartridge());
				}
			}
		}
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x000B77E7 File Offset: 0x000B59E7
	private IEnumerator Initialize_Bullet()
	{
		yield return new WaitForSeconds(1f);
		this.Initialize_Bullet_Display();
		yield break;
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x000B77F6 File Offset: 0x000B59F6
	private IEnumerator Lerp_Cartridge()
	{
		bool flag = !this.Lerp_Execute_State;
		if (flag)
		{
			bool flag2 = this.Current_Bullet_Number == 0;
			if (flag2)
			{
				this.Lerp_Target = new Vector3(0f, 90f, 0f);
			}
			else
			{
				this.Lerp_Target = new Vector3(0f, 90f, this.Bullet_Parent_Object.transform.localEulerAngles.z + 60f);
			}
			this.Lerp_Execute_State = true;
			yield return new WaitForSeconds(((CWeaponControlMe)base.hero.wc).weapons[1].launchersDict[0].oneShootTime - 0.1f);
			this.Lerp_Execute_State = false;
		}
		yield break;
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000B7808 File Offset: 0x000B5A08
	private void Rotation()
	{
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Bullet_Parent_Object.transform.localEulerAngles = Vector3.Lerp(this.Bullet_Parent_Object.transform.localEulerAngles, this.Lerp_Target, 0.1f);
		}
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x000B785C File Offset: 0x000B5A5C
	private void LoadSkillCDFinishSound(string sSoundName = "skillvoice_leizhenzi_fly_loop")
	{
		string url = string.Empty;
		bool flag = !this.isLoad && !this.LeiZhenZiAudio.ContainsKey(sSoundName);
		if (flag)
		{
			this.isLoad = true;
			url = "SoundRes/" + sSoundName + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadSkillCDFinishSoundPrefab), sSoundName);
			}
			else
			{
				this.LoadSkillCDFinishSoundPrefab(resWWW, sSoundName);
			}
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000B78DC File Offset: 0x000B5ADC
	private void LoadSkillCDFinishSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			string text = string.Empty;
			bool flag2 = base.hero != null && !base.hero.haveDestroy;
			if (flag2)
			{
				text = sStyle.ToString();
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.parent = base.hero._playerSoundContainer.transform;
				component.transform.localPosition = Vector3.zero;
				this.isLoad = false;
				bool flag3 = !this.LeiZhenZiAudio.ContainsKey(text);
				if (flag3)
				{
					this.LeiZhenZiAudio.Add(text, component);
				}
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
				this.PlayCDFinishSound(text);
			}
		}
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000B79E4 File Offset: 0x000B5BE4
	public void PlayCDFinishSound(string sSoundName)
	{
		bool flag = !this.LeiZhenZiAudio.ContainsKey(sSoundName);
		if (flag)
		{
			this.LoadSkillCDFinishSound(sSoundName);
		}
		else
		{
			bool flag2 = !this.LeiZhenZiAudio[sSoundName].isPlaying;
			if (flag2)
			{
				this.LeiZhenZiAudio[sSoundName].Play();
				bool flag3 = sSoundName == "skillvoice_leizhenzi_fly_loop";
				if (flag3)
				{
					this.LeiZhenZiAudio[sSoundName].loop = true;
				}
			}
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000B7A64 File Offset: 0x000B5C64
	private void StopCDFinishSound(string sSoundName)
	{
		bool flag = this.LeiZhenZiAudio.ContainsKey(sSoundName) && this.LeiZhenZiAudio[sSoundName].isPlaying;
		if (flag)
		{
			this.LeiZhenZiAudio[sSoundName].Stop();
		}
	}

	// Token: 0x04000EDD RID: 3805
	private bool energyInitialized = false;

	// Token: 0x04000EDE RID: 3806
	private AudioSource _LeiZhenZiFlySound;

	// Token: 0x04000EDF RID: 3807
	private Dictionary<string, AudioSource> LeiZhenZiAudio = new Dictionary<string, AudioSource>();

	// Token: 0x04000EE0 RID: 3808
	private bool isLoad;

	// Token: 0x04000EE1 RID: 3809
	public const string LeiZhenZiFlySoundUrl = "skillvoice_leizhenzi_fly_loop";

	// Token: 0x04000EE2 RID: 3810
	public const string LeiZhenZiSuperSoundUrl = "skillvoice_leizhenzi_dazhao_lock";

	// Token: 0x04000EE3 RID: 3811
	private LayerMask HeroMask;

	// Token: 0x04000EE4 RID: 3812
	private float Super_Duration_Time;

	// Token: 0x04000EE5 RID: 3813
	private bool _Power_State;

	// Token: 0x04000EE6 RID: 3814
	private bool _Multistage_Jump_State;

	// Token: 0x04000EE7 RID: 3815
	private float _Space_Duration_Time;

	// Token: 0x04000EE8 RID: 3816
	private ColorCorrectionCurves colorc;

	// Token: 0x04000EE9 RID: 3817
	private float waiteTime;

	// Token: 0x04000EEA RID: 3818
	private bool isSupper;

	// Token: 0x04000EEB RID: 3819
	private ArrayList allTarget = new ArrayList();

	// Token: 0x04000EEC RID: 3820
	private IEnumerator coroutine;

	// Token: 0x04000EED RID: 3821
	private ArrayList szLockUser = new ArrayList();

	// Token: 0x04000EEE RID: 3822
	private Dictionary<int, int[]> LockUserHp = new Dictionary<int, int[]>();

	// Token: 0x04000EEF RID: 3823
	private bool _Jump_State;

	// Token: 0x04000EF0 RID: 3824
	private bool is_Recover;

	// Token: 0x04000EF1 RID: 3825
	private float nRecover;

	// Token: 0x04000EF2 RID: 3826
	private int _Current_Bullet_Number = -1;

	// Token: 0x04000EF3 RID: 3827
	private GameObject _Bullet_Parent_Object;

	// Token: 0x04000EF4 RID: 3828
	private GameObject[] _Bullet_Object_Array;

	// Token: 0x04000EF5 RID: 3829
	private bool _Lerp_Execute_State;

	// Token: 0x04000EF6 RID: 3830
	private Vector3 _Lerp_Target = Vector3.zero;
}
