using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class HeroLeiZhenZiIt : HeroBaseIt
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x060012DB RID: 4827 RVA: 0x000B50A0 File Offset: 0x000B32A0
	// (set) Token: 0x060012DC RID: 4828 RVA: 0x000B50B8 File Offset: 0x000B32B8
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

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x060012DD RID: 4829 RVA: 0x000B50C4 File Offset: 0x000B32C4
	// (set) Token: 0x060012DE RID: 4830 RVA: 0x000B5118 File Offset: 0x000B3318
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

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060012DF RID: 4831 RVA: 0x000B5124 File Offset: 0x000B3324
	// (set) Token: 0x060012E0 RID: 4832 RVA: 0x000B5268 File Offset: 0x000B3468
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

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000B5274 File Offset: 0x000B3474
	// (set) Token: 0x060012E2 RID: 4834 RVA: 0x000B528C File Offset: 0x000B348C
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

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000B5298 File Offset: 0x000B3498
	// (set) Token: 0x060012E4 RID: 4836 RVA: 0x000B52B0 File Offset: 0x000B34B0
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

	// Token: 0x060012E5 RID: 4837 RVA: 0x000B52BA File Offset: 0x000B34BA
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.HeroMask = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).getMiaoZhunLayer(net.nForce);
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000B52EC File Offset: 0x000B34EC
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool animatorBool = base.hero.GetAnimatorBool("SkillSuper");
		if (animatorBool)
		{
			this.SuperSkillDo(null);
		}
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x000B5320 File Offset: 0x000B3520
	public override void SuperSkillDo(object parm)
	{
		bool flag = base.hero != null && base.hero.playerHaveCreate && !base.hero.haveDestroy;
		if (flag)
		{
			base.SuperSkillDo(parm);
			this.isSuperSkill = true;
			base.hero.tChar.moveByControlIt = true;
			base.StartCoroutine(this.checkFinish());
			base.hero.tChar.m_MoveByAnimator = false;
			base.hero.tCharAnimationTps.animator.SetBool("SkillSuper", true);
		}
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000B53B5 File Offset: 0x000B35B5
	private IEnumerator checkFinish()
	{
		yield return new WaitForSeconds(1.1f);
		while (this.isSuperSkill)
		{
			yield return new WaitForSeconds(0.1f);
			this.isSuperSkill = base.hero.GetAnimatorBool("SkillSuper");
			bool flag = !this.isSuperSkill;
			if (flag)
			{
				break;
			}
		}
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x000B53C4 File Offset: 0x000B35C4
	public override void SuperSkillFinish()
	{
		this.isSuperSkill = false;
		base.hero.tChar.moveByControlIt = false;
		base.hero.bePlayActive = true;
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.ClearLzzEffect();
		}
		this.szLockUser.Clear();
		this.LockUserHp.Clear();
		base.SuperSkillFinish();
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000B5438 File Offset: 0x000B3638
	private void OnDestroy()
	{
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.ClearLzzEffect();
		}
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000B5468 File Offset: 0x000B3668
	protected override void Update()
	{
		base.Update();
		bool lerp_Execute_State = this.Lerp_Execute_State;
		if (lerp_Execute_State)
		{
			this.Rotation();
		}
		this.Detection_Bullet_Number();
		bool fly = base.hero.charMove.state.fly;
		if (fly)
		{
			this.PlayCDFinishSound();
		}
		else
		{
			this.StopCDFinishSound();
		}
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			this.GetRayHitObj();
		}
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x000B551C File Offset: 0x000B371C
	private void GetRayHitObj()
	{
		Vector3 direction = Vector3.zero;
		ArrayList arrayList = new ArrayList();
		bool flag = !base.hero.GetAnimatorBool("SkillSuper") || Time.time - this.Super_Duration_Time < 0.1f;
		if (!flag)
		{
			this.Super_Duration_Time = Time.time;
			foreach (KeyValuePair<string, NetObj> keyValuePair in CFilmManager.Instance.GetFilmNetLivingAll)
			{
				NetLiving netLiving = (NetLiving)keyValuePair.Value;
				int netid = netLiving.getNetid();
				bool flag2 = netLiving.defaultMesh == null || (MatchManager.GetInstance().m_pFbData.nType != 6 && !netLiving.bePlayer) || netLiving.nForce == base.hero.nForce;
				if (!flag2)
				{
					bool flag3 = ((IHeroDataLeiZhenZi)base.hero.heroBase.IHD).getScreenVector(netLiving.defaultMesh.transform.position) == new Vector3(-1f, -1f, -1f) || netLiving.isDead || netLiving.heroBase.IsHeroInvi();
					if (flag3)
					{
						bool flag4 = this.szLockUser.IndexOf(netid) != -1;
						if (flag4)
						{
							bool flag5 = netLiving.heroBase.IsHeroInvi() && gameuiCon.inst != null;
							if (flag5)
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
						bool flag6 = Physics.Raycast(ray, out raycastHit, 200f, this.HeroMask);
						if (flag6)
						{
							bool flag7 = !raycastHit.collider.tag.Contains("can_damage");
							if (!flag7)
							{
								bool flag8 = this.szLockUser.IndexOf(netid) == -1;
								if (flag8)
								{
									this.szLockUser.Add(netid);
								}
								bool flag9 = gameuiCon.inst != null;
								if (flag9)
								{
									int nowHp = netLiving.playerCommon.GetNowHp();
									bool flag10 = !this.LockUserHp.ContainsKey(netid);
									int num;
									if (flag10)
									{
										num = 70;
										Dictionary<int, int[]> lockUserHp = this.LockUserHp;
										int key = netid;
										int[] array = new int[2];
										array[0] = 70;
										lockUserHp.Add(key, array);
									}
									else
									{
										this.LockUserHp[netid][0] += 70;
										num = this.LockUserHp[netid][0];
									}
									bool flag11 = num >= nowHp;
									float sca;
									if (flag11)
									{
										sca = 0.5f;
										this.LockUserHp[netid][0] = nowHp;
										bool flag12 = this.LockUserHp[netid][1] == 0;
										if (flag12)
										{
											this.LockUserHp[netid][1] = 1;
										}
									}
									else
									{
										sca = 0.5f + 0.5f * (float)(nowHp - num) / (float)nowHp;
									}
									gameuiCon.inst.mainMsgUI.OnAddSuperSkillEffect(netid, sca);
								}
								arrayList.Add(netid);
								arrayList.Add(1);
							}
						}
						else
						{
							bool flag13 = this.szLockUser.IndexOf(netid) != -1;
							if (flag13)
							{
								this.szLockUser.Remove(netid);
								arrayList.Add(netid);
								arrayList.Add(0);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000B5964 File Offset: 0x000B3B64
	public override void BulletFillBegin()
	{
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Lerp_Target = this.Bullet_Parent_Object.transform.localEulerAngles;
			this.Bullet_Parent_Object.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000B59C4 File Offset: 0x000B3BC4
	public override void BulletFillBreak()
	{
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Bullet_Parent_Object.transform.localEulerAngles = this.Lerp_Target;
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x000B5A00 File Offset: 0x000B3C00
	public override void BulletFilled()
	{
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Initialize_Bullet_Display();
			this.Bullet_Parent_Object.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x000B5A50 File Offset: 0x000B3C50
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

	// Token: 0x060012F1 RID: 4849 RVA: 0x000B5A98 File Offset: 0x000B3C98
	private void Detection_Bullet_Number()
	{
		bool flag = base.hero.wc == null;
		if (!flag)
		{
			bool flag2 = this.Current_Bullet_Number < 0;
			if (flag2)
			{
				this.Current_Bullet_Number = ((CWeaponControlIt)base.hero.wc).cartridgePublicNow[0];
			}
			bool flag3 = this.Current_Bullet_Number != ((CWeaponControlIt)base.hero.wc).cartridgePublicNow[0] && this.Current_Bullet_Number >= 0;
			if (flag3)
			{
				this.Current_Bullet_Number = ((CWeaponControlIt)base.hero.wc).cartridgePublicNow[0];
				bool flag4 = this.Current_Bullet_Number < 6 && !(this.Bullet_Object_Array[this.Current_Bullet_Number] == null);
				if (flag4)
				{
					this.Bullet_Object_Array[this.Current_Bullet_Number].SetActive(false);
					base.StartCoroutine(this.Lerp_Cartridge());
				}
			}
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x000B5B89 File Offset: 0x000B3D89
	private IEnumerator Initialize_Bullet()
	{
		yield return new WaitForSeconds(1f);
		this.Initialize_Bullet_Display();
		yield break;
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x000B5B98 File Offset: 0x000B3D98
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
			yield return new WaitForSeconds(0.2f);
			this.Lerp_Execute_State = false;
		}
		yield break;
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x000B5BA8 File Offset: 0x000B3DA8
	private void Rotation()
	{
		bool flag = !(this.Bullet_Parent_Object == null);
		if (flag)
		{
			this.Bullet_Parent_Object.transform.localEulerAngles = Vector3.Lerp(this.Bullet_Parent_Object.transform.localEulerAngles, this.Lerp_Target, 0.1f);
		}
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x000B5BFC File Offset: 0x000B3DFC
	private void LoadSkillCDFinishSound()
	{
		string text = "skillvoice_leizhenzi_fly_loop";
		bool flag = !this.isLoad;
		if (flag)
		{
			this.isLoad = true;
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillCDFinishSoundPrefab), 0);
			}
			else
			{
				this.LoadSkillCDFinishSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x000B5C74 File Offset: 0x000B3E74
	private void LoadSkillCDFinishSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = base.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._LeiZhenZiFlySound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			this.isLoad = false;
			this.PlayCDFinishSound();
		}
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x000B5D3C File Offset: 0x000B3F3C
	public void PlayCDFinishSound()
	{
		bool flag = this._LeiZhenZiFlySound == null;
		if (flag)
		{
			this.LoadSkillCDFinishSound();
		}
		else
		{
			bool flag2 = (this._hero.isCopy || !CFilmManager.Instance.playing) && !this._LeiZhenZiFlySound.isPlaying;
			if (flag2)
			{
				this._LeiZhenZiFlySound.Play();
				this._LeiZhenZiFlySound.loop = true;
			}
		}
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x000B5DB0 File Offset: 0x000B3FB0
	private void StopCDFinishSound()
	{
		bool flag = this._LeiZhenZiFlySound != null && this._LeiZhenZiFlySound.isPlaying;
		if (flag)
		{
			this._LeiZhenZiFlySound.Stop();
		}
	}

	// Token: 0x04000ECF RID: 3791
	private AudioSource _LeiZhenZiFlySound;

	// Token: 0x04000ED0 RID: 3792
	private bool isLoad;

	// Token: 0x04000ED1 RID: 3793
	public const string LeiZhenZiFlySoundUrl = "skillvoice_leizhenzi_fly_loop";

	// Token: 0x04000ED2 RID: 3794
	private float Space_Duration_Time;

	// Token: 0x04000ED3 RID: 3795
	private LayerMask HeroMask;

	// Token: 0x04000ED4 RID: 3796
	private bool isSuperSkill;

	// Token: 0x04000ED5 RID: 3797
	private float Super_Duration_Time;

	// Token: 0x04000ED6 RID: 3798
	private ArrayList szLockUser = new ArrayList();

	// Token: 0x04000ED7 RID: 3799
	private Dictionary<int, int[]> LockUserHp = new Dictionary<int, int[]>();

	// Token: 0x04000ED8 RID: 3800
	private int _Current_Bullet_Number = -1;

	// Token: 0x04000ED9 RID: 3801
	private GameObject _Bullet_Parent_Object;

	// Token: 0x04000EDA RID: 3802
	private GameObject[] _Bullet_Object_Array;

	// Token: 0x04000EDB RID: 3803
	private bool _Lerp_Execute_State;

	// Token: 0x04000EDC RID: 3804
	private Vector3 _Lerp_Target = Vector3.zero;
}
