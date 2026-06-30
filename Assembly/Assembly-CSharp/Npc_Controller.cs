using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class Npc_Controller : MonoBehaviour
{
	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06001BEC RID: 7148 RVA: 0x001061CC File Offset: 0x001043CC
	// (set) Token: 0x06001BED RID: 7149 RVA: 0x001061E4 File Offset: 0x001043E4
	public Npc_View View_Component
	{
		get
		{
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06001BEE RID: 7150 RVA: 0x001061F0 File Offset: 0x001043F0
	// (set) Token: 0x06001BEF RID: 7151 RVA: 0x00106208 File Offset: 0x00104408
	public Npc_Model Model_Component
	{
		get
		{
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00106214 File Offset: 0x00104414
	// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0010624C File Offset: 0x0010444C
	public Npc_Entity Entity_Component
	{
		get
		{
			bool flag = this._Entity_Component == null;
			if (flag)
			{
				this._Entity_Component = this.Model_Component.Get_Entity();
			}
			return this._Entity_Component;
		}
		set
		{
			this._Entity_Component = value;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00106258 File Offset: 0x00104458
	// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x00106270 File Offset: 0x00104470
	public GameObject Model_Object
	{
		get
		{
			return this._Model_Object;
		}
		set
		{
			this._Model_Object = value;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0010627C File Offset: 0x0010447C
	// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x001063A0 File Offset: 0x001045A0
	public Rigidbody Rigidbody_Component
	{
		get
		{
			Rigidbody rigidbody_Component;
			try
			{
				bool flag = this._Rigidbody_Component == null;
				if (flag)
				{
					this._Rigidbody_Component = this.Entity_Component.UserControl.GetComponent<Rigidbody>();
					bool flag2 = this._Rigidbody_Component == null;
					if (flag2)
					{
						this._Rigidbody_Component = this.Entity_Component.UserControl.AddComponent<Rigidbody>();
					}
					this._Rigidbody_Component.mass = this.Entity_Component.Mass;
				}
				rigidbody_Component = this._Rigidbody_Component;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"索引:",
					this.Entity_Component.Index,
					" - 类型:",
					this.Entity_Component.Type,
					" - 名称:",
					this.Entity_Component.Name,
					"这个NPC的刚体这里出了问题，查看重要对象，",
					this.Entity_Component.UserControl,
					" - ",
					ex.Message
				}));
				rigidbody_Component = this._Rigidbody_Component;
			}
			return rigidbody_Component;
		}
		set
		{
			this._Rigidbody_Component = value;
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x001063AC File Offset: 0x001045AC
	// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x001063C4 File Offset: 0x001045C4
	public GameObject Death_Effect_Object
	{
		get
		{
			return this._Death_Effect_Object;
		}
		set
		{
			this._Death_Effect_Object = value;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x001063D0 File Offset: 0x001045D0
	// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x001063E8 File Offset: 0x001045E8
	public GameObject Death_Sound_Object
	{
		get
		{
			return this._Death_Sound_Object;
		}
		set
		{
			this._Death_Sound_Object = value;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06001BFA RID: 7162 RVA: 0x001063F4 File Offset: 0x001045F4
	// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0010640C File Offset: 0x0010460C
	public AudioSource Death_Sound_Audio_Source
	{
		get
		{
			return this._Death_Sound_Audio_Source;
		}
		set
		{
			this._Death_Sound_Audio_Source = value;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00106418 File Offset: 0x00104618
	// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0010649A File Offset: 0x0010469A
	public Animator Animator_Component
	{
		get
		{
			bool flag = this._Animator_Component == null;
			if (flag)
			{
				bool flag2 = this.Entity_Component.thisModel == null;
				if (flag2)
				{
					return null;
				}
				this._Animator_Component = this.Entity_Component.thisModel.GetComponent<Animator>();
				bool flag3 = this._Animator_Component == null;
				if (flag3)
				{
					this._Animator_Component = this.Entity_Component.thisModel.AddComponent<Animator>();
				}
			}
			return this._Animator_Component;
		}
		set
		{
			this._Animator_Component = value;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06001BFE RID: 7166 RVA: 0x001064A4 File Offset: 0x001046A4
	// (set) Token: 0x06001BFF RID: 7167 RVA: 0x001064BC File Offset: 0x001046BC
	public RuntimeAnimatorController Runtime_Animator_Controller_Component
	{
		get
		{
			return this._Runtime_Animator_Controller_Component;
		}
		set
		{
			this._Runtime_Animator_Controller_Component = value;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06001C00 RID: 7168 RVA: 0x001064C8 File Offset: 0x001046C8
	// (set) Token: 0x06001C01 RID: 7169 RVA: 0x001064E0 File Offset: 0x001046E0
	public Vector3 Previous_Position
	{
		get
		{
			return this._Previous_Position;
		}
		set
		{
			this._Previous_Position = value;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06001C02 RID: 7170 RVA: 0x001064EC File Offset: 0x001046EC
	// (set) Token: 0x06001C03 RID: 7171 RVA: 0x00106504 File Offset: 0x00104704
	public Vector3 Previous_Rotation
	{
		get
		{
			return this._Previous_Rotation;
		}
		set
		{
			this._Previous_Rotation = value;
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00106510 File Offset: 0x00104710
	// (set) Token: 0x06001C05 RID: 7173 RVA: 0x00106528 File Offset: 0x00104728
	public Vector3 Previous_Target_Position
	{
		get
		{
			return this._Previous_Target_Position;
		}
		set
		{
			this._Previous_Target_Position = value;
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00106534 File Offset: 0x00104734
	// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0010654C File Offset: 0x0010474C
	public Vector3 Previous_Target_Point
	{
		get
		{
			return this._Previous_Target_Point;
		}
		set
		{
			this._Previous_Target_Point = value;
		}
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x00106556 File Offset: 0x00104756
	private void Load_Animator_Controller()
	{
		this.Runtime_Animator_Controller_Component = (RuntimeAnimatorController)Resources.Load(string.Empty);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x00106570 File Offset: 0x00104770
	public virtual void Load_Model(int _Target_ID, string _Collider_Name)
	{
		Debug.LogError(string.Format("[DEBUG MODEL LOAD] Entity Index: {0}, Name: {1}, Resource Name: {2}, Target ID: {3}", new object[]
		{
			this.Entity_Component.Index,
			this.Entity_Component.Name,
			this.Entity_Component.Resouce_Name,
			_Target_ID
		}));
		GameRoot.LoadAsset("HeroAnimator/" + this.Entity_Component.Resouce_Name + ".mo", new fResourceManager.DownFinishDelegate(this.Load_Model_Complete), new ArrayList
		{
			_Target_ID,
			_Collider_Name
		});
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x00106614 File Offset: 0x00104814
	public void Load_Model_Complete(WWW _WWW, object _Parameters)
	{
		Debug.LogError(string.Format("[DEBUG MODEL LOAD COMPLETE] WWW URL: {0}, Error: {1}, HasAssetBundle: {2}", _WWW.url, _WWW.error, (_WWW.assetBundle != null) ? "YES" : "NO"));
		int target_ID = -1;
		string collider_Name = string.Empty;
		ArrayList arrayList = (ArrayList)_Parameters;
		bool haveDestroy = this.Entity_Component.haveDestroy;
		if (haveDestroy)
		{
			this.Entity_Component.OnNetDestroy();
		}
		else
		{
			bool flag = fGameManager.GetInstance().nNowScnID < 1;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.Entity_Component.thisModel);
				UnityEngine.Object.Destroy(this.Entity_Component.UserControl);
				UnityEngine.Object.Destroy(this.Entity_Component.Container);
				this.Entity_Component.OnNetDestroy();
			}
			else
			{
				bool death_State = this.Entity_Component.Death_State;
				if (death_State)
				{
					UnityEngine.Object.Destroy(this.Entity_Component.thisModel);
					UnityEngine.Object.Destroy(this.Entity_Component.UserControl);
					UnityEngine.Object.Destroy(this.Entity_Component.Container);
					this.Entity_Component.OnNetDestroy();
				}
				else
				{
					UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
					this.Model_Object = (GameObject)UnityEngine.Object.Instantiate(original);
					foreach (AudioSource audioSource in this.Model_Object.GetComponentsInChildren<AudioSource>())
					{
						bool flag2 = audioSource != null;
						if (flag2)
						{
							CSoundManager.Instance.SetEffectSoundMixer(audioSource, 0, null, 20);
						}
					}
					bool flag3 = this.Entity_Component.Adhere > 0;
					if (flag3)
					{
						target_ID = int.Parse(arrayList[0].ToString());
						collider_Name = arrayList[1].ToString();
					}
					this.Entity_Component.thisModel = this.Model_Object;
					this.Entity_Component.userModelTps = this.Model_Object;
					this.Entity_Component.userModelFps = this.Model_Object;
					this.Model_Object.name = "model_" + this.Entity_Component.Resouce_Name;
					bool flag4 = this.Entity_Component.UserControl != null;
					if (flag4)
					{
						this.Model_Object.transform.parent = this.Entity_Component.UserControl.transform;
					}
					this.Model_Object.transform.localPosition = Vector3.zero;
					this.Model_Object.transform.localEulerAngles = Vector3.zero;
					this.Model_Object.transform.localScale = this.Entity_Component.Scale;
					Transform transform = Fun.Get_Child_Transform(this.Model_Object, this.Entity_Component.Focus);
					bool flag5 = transform == null;
					if (flag5)
					{
						transform = this.Model_Object.transform;
					}
					this.Entity_Component.defaultMesh.transform.position = transform.position;
					Rigidbody[] componentsInChildren2 = this.Model_Object.GetComponentsInChildren<Rigidbody>(true);
					for (int j = 0; j < componentsInChildren2.Length; j++)
					{
						UnityEngine.Object.Destroy(componentsInChildren2[j]);
					}
					this.Entity_Component.Load_Model_Resouce_Complete();
					Fun.Change_Collider(this.Model_Object, true);
					this.Set_Rigidbody_Component(target_ID, collider_Name);
				}
			}
		}
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x00106974 File Offset: 0x00104B74
	private void Set_Rigidbody_Component(int _Target_ID, string _Collider_Name)
	{
		this.Rigidbody_Component.useGravity = this.Entity_Component.Gravity;
		this.Rigidbody_Component.isKinematic = !this.Entity_Component.Gravity;
		bool gravity = this.Entity_Component.Gravity;
		if (gravity)
		{
			this.Rigidbody_Component.freezeRotation = true;
		}
		bool flag = this.Entity_Component.Force != Vector3.zero;
		if (flag)
		{
			this.Rigidbody_Component.velocity = this.Entity_Component.Force;
		}
		bool flag2 = _Target_ID >= 0;
		if (flag2)
		{
			this.Rigidbody_Component.isKinematic = true;
			base.StartCoroutine(this.Search_Npc_Parent_Object(_Target_ID, _Collider_Name));
		}
		this.Load_Death_Effect();
		this.Load_Death_Sound();
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x00106A3C File Offset: 0x00104C3C
	public void Load_Death_Effect()
	{
		bool flag = !(this.Entity_Component.Death_Effect == string.Empty);
		if (flag)
		{
			GameRoot.LoadAsset("EffectRes/" + this.Entity_Component.Death_Effect + ".ef", new fResourceManager.DownFinishDelegate(this.Load_Death_Effect_Complete), null);
		}
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x00106A98 File Offset: 0x00104C98
	public void Load_Death_Effect_Complete(WWW _WWW, object _Parameters)
	{
		bool flag = this.Entity_Component != null && !(this.Entity_Component.UserControl == null);
		if (flag)
		{
			UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
			this.Death_Effect_Object = (GameObject)UnityEngine.Object.Instantiate(original);
			this.Death_Effect_Object.SetActive(false);
			this.Death_Effect_Object.name = "Death_Effect_" + this.Entity_Component.Death_Effect;
			this.Death_Effect_Object.transform.parent = this.Entity_Component.UserControl.transform;
			this.Death_Effect_Object.transform.localPosition = Vector3.zero;
			this.Death_Effect_Object.transform.localEulerAngles = Vector3.zero;
			this.Death_Effect_Object.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x00106B8C File Offset: 0x00104D8C
	public void Load_Death_Sound()
	{
		bool flag = !(this.Entity_Component.Death_Sound == string.Empty);
		if (flag)
		{
			GameRoot.LoadAsset("SoundRes/" + this.Entity_Component.Death_Sound + ".sds", new fResourceManager.DownFinishDelegate(this.Load_Death_Sound_Complete), null);
		}
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x00106BE8 File Offset: 0x00104DE8
	public void Load_Death_Sound_Complete(WWW _WWW, object _Parameters)
	{
		bool flag = this.Entity_Component != null && !(this.Entity_Component.UserControl == null);
		if (flag)
		{
			UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
			this.Death_Sound_Object = (GameObject)UnityEngine.Object.Instantiate(original);
			this.Death_Sound_Object.name = "Death_Sound_" + this.Entity_Component.Death_Sound;
			this.Death_Sound_Object.transform.parent = this.Entity_Component.UserControl.transform;
			this.Death_Sound_Object.transform.localPosition = Vector3.zero;
			this.Death_Sound_Object.transform.localEulerAngles = Vector3.zero;
			this.Death_Sound_Object.transform.localScale = Vector3.one;
			this.Death_Sound_Audio_Source = this.Death_Sound_Object.GetComponent<AudioSource>();
			this.Death_Sound_Audio_Source.playOnAwake = false;
			this.Death_Sound_Audio_Source.Stop();
			CSoundManager.Instance.SetEffectSoundMixer(this.Death_Sound_Audio_Source, 0, null, 20);
		}
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00106D10 File Offset: 0x00104F10
	public void Play_Death_Sound()
	{
		bool flag = !(this.Death_Sound_Audio_Source == null);
		if (flag)
		{
			this.Death_Sound_Audio_Source.Play();
		}
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x00106D3F File Offset: 0x00104F3F
	private IEnumerator Search_Npc_Parent_Object(int _Target_ID, string _Collider_Name)
	{
		bool flag = _Target_ID <= 0;
		if (flag)
		{
			yield break;
		}
		NetLiving _NetLiving = null;
		Npc_Entity _Npc_Entity = null;
		RobotAI _RobotAI = null;
		GameObject _Parent_Object = null;
		bool flag2 = !base.enabled || this.Entity_Component.isDead || this.Entity_Component.UserControl == null;
		if (flag2)
		{
			yield break;
		}
		_NetLiving = ((!this.Entity_Component.isCopy) ? ((NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_Target_ID)) : Fun.Get_NetLiving(_Target_ID));
		bool flag3 = _NetLiving != null;
		if (flag3)
		{
			this.Entity_Component.Need_Synchronization = false;
			bool flag4 = _NetLiving.netobject_type == 2;
			if (flag4)
			{
				_Npc_Entity = (Npc_Entity)_NetLiving;
				bool flag5 = !(_Npc_Entity.defaultMesh == null) && !_Npc_Entity.Death_State;
				if (flag5)
				{
					_Npc_Entity.Add_Adhere_Child_Object_Data(_Collider_Name, this.Entity_Component);
					this.Entity_Component.UserControl.transform.position = this.Entity_Component.playerCommon.pos;
					this.Entity_Component.UserControl.transform.parent = _Npc_Entity.defaultMesh.transform;
					bool flag6 = _Npc_Entity.Rigidbody_Component != null;
					if (flag6)
					{
						_Npc_Entity.Rigidbody_Component.isKinematic = true;
					}
				}
			}
			else
			{
				bool flag7 = _NetLiving.netobject_type == 6;
				if (flag7)
				{
					_RobotAI = (RobotAI)_NetLiving;
					bool flag8 = !(_RobotAI.defaultMesh == null) && !_RobotAI.isDead;
					if (flag8)
					{
						_RobotAI.Add_Adhere_Child_Object_Data(_Collider_Name, this.Entity_Component);
						this.Entity_Component.UserControl.transform.position = this.Entity_Component.playerCommon.pos;
						this.Entity_Component.UserControl.transform.parent = _RobotAI.defaultMesh.transform;
					}
				}
				else
				{
					bool flag9 = this.Entity_Component.Adhere == 1 && !(_NetLiving.defaultMesh == null) && _NetLiving.playerCommon.m_nPlayerState != 0;
					if (flag9)
					{
						_NetLiving.Add_Adhere_Child_Object_Data(_Collider_Name, this.Entity_Component);
						GamePlayer gamePlayer = (GamePlayer)_NetLiving;
						bool flag10 = _Collider_Name.Contains("dun");
						if (flag10)
						{
							_Parent_Object = gamePlayer.heroBase.GetShield(_Collider_Name);
						}
						bool flag11 = _Parent_Object == null;
						if (flag11)
						{
							_Parent_Object = _NetLiving.defaultMesh;
						}
						this.Entity_Component.UserControl.transform.parent = _Parent_Object.transform;
						gamePlayer = null;
					}
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(0.1f);
			base.StartCoroutine(this.Search_Npc_Parent_Object(_Target_ID, _Collider_Name));
		}
		yield break;
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x00106D5C File Offset: 0x00104F5C
	public void Synchronization()
	{
		bool flag = this.Model_Component.Get_Model_Object() == null;
		if (!flag)
		{
			HeroPlayer synchronization_Object = this.Entity_Component.Synchronization_Object;
			bool flag2 = synchronization_Object != null && !(synchronization_Object.charMove == null);
			if (flag2)
			{
				bool flag3 = this.Entity_Component.Type == 5;
				if (flag3)
				{
					this.NSP();
				}
				bool flag4 = this.Entity_Component.Type == 4 || this.Entity_Component.Type == 8;
				if (flag4)
				{
					this.NSP();
					this.NSR();
					this.NST();
					this.NCD();
				}
				bool flag5 = this.Entity_Component.Type == 1;
				if (flag5)
				{
					this.NSP();
					this.NSTP();
				}
			}
		}
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x00106E34 File Offset: 0x00105034
	public void NSP()
	{
		bool flag = this.Entity_Component.Synchronization_Object != null && (this.Entity_Component.Type == 4 || Vector3.Distance(this.Previous_Position, this.Entity_Component.UserControl.transform.position) >= 0.001f);
		if (flag)
		{
			this.Previous_Position = this.Entity_Component.UserControl.transform.position;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Entity_Component.ID);
			arrayList.Add((int)(this.Previous_Position.x * 100f));
			arrayList.Add((int)(this.Previous_Position.y * 100f));
			arrayList.Add((int)(this.Previous_Position.z * 100f));
			this.Entity_Component.Synchronization_Object.TosFun("NSP", new object[]
			{
				new ArrayList
				{
					arrayList
				}
			});
		}
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x00106F58 File Offset: 0x00105158
	public void NSR()
	{
		bool flag = this.Entity_Component.Synchronization_Object != null && (this.Entity_Component.Type == 4 || Vector3.Angle(this.Previous_Position, this.Entity_Component.UserControl.transform.position) >= 0.001f);
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Entity_Component.ID);
			arrayList.Add((int)(this.Entity_Component.UserControl.transform.localEulerAngles.x * 100f));
			arrayList.Add((int)(this.Entity_Component.UserControl.transform.localEulerAngles.y * 100f));
			arrayList.Add((int)(this.Entity_Component.UserControl.transform.localEulerAngles.z * 100f));
			this.Entity_Component.Synchronization_Object.TosFun("NSR", new object[]
			{
				new ArrayList
				{
					arrayList
				}
			});
		}
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x0010708C File Offset: 0x0010528C
	public void NSS()
	{
		bool flag = this.Entity_Component.Synchronization_Object != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Entity_Component.ID);
			arrayList.Add(Convert.ToInt32(this.Entity_Component.UserControl.transform.localScale.x * 100f));
			arrayList.Add(Convert.ToInt32(this.Entity_Component.UserControl.transform.localScale.y * 100f));
			arrayList.Add(Convert.ToInt32(this.Entity_Component.UserControl.transform.localScale.z * 100f));
			this.Entity_Component.Synchronization_Object.TosFun("NSS", new object[]
			{
				new ArrayList
				{
					arrayList
				}
			});
		}
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x00107190 File Offset: 0x00105390
	public void NSTP()
	{
		bool flag = this.Entity_Component.Synchronization_Object != null && Vector3.Distance(this.Previous_Target_Position, this.Entity_Component.Target_Position) >= 0.1f;
		if (flag)
		{
			this.Previous_Target_Position = this.Entity_Component.Target_Position;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Entity_Component.ID);
			arrayList.Add(this.Entity_Component.Target_Net_ID);
			arrayList.Add(Convert.ToInt32(this.Entity_Component.Target_Position.x * 100f));
			arrayList.Add(Convert.ToInt32(this.Entity_Component.Target_Position.y * 100f));
			arrayList.Add(Convert.ToInt32(this.Entity_Component.Target_Position.z * 100f));
			this.Entity_Component.Synchronization_Object.TosFun("NSTP", new object[]
			{
				new ArrayList
				{
					arrayList
				}
			});
		}
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x001072C0 File Offset: 0x001054C0
	public void NST()
	{
		bool flag = this.Entity_Component.Synchronization_Object != null && Vector3.Distance(this.Previous_Target_Point, this.Entity_Component.playerTarget.transform.position) >= 0.01f;
		if (flag)
		{
			this.Previous_Target_Point = this.Entity_Component.playerTarget.transform.position;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Entity_Component.ID);
			arrayList.Add((int)(this.Previous_Target_Point.x * 100f));
			arrayList.Add((int)(this.Previous_Target_Point.y * 100f));
			arrayList.Add((int)(this.Previous_Target_Point.z * 100f));
			this.Entity_Component.Synchronization_Object.TosFun("NST", new object[]
			{
				new ArrayList
				{
					arrayList
				}
			});
		}
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x001073D0 File Offset: 0x001055D0
	public virtual void NCD()
	{
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x001073D3 File Offset: 0x001055D3
	public IEnumerator Wait_Resurgence()
	{
		Npc_AI_Entity _AI = null;
		_AI = this.Model_Component.Get_Npc_AI();
		yield return new WaitForSeconds(_AI.Resurgence_Time);
		this.Resurgence();
		yield break;
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x001073E2 File Offset: 0x001055E2
	private void Resurgence()
	{
		this.Entity_Component.Death_State = false;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x001073F4 File Offset: 0x001055F4
	public virtual void Update()
	{
		bool flag = fGameManager.Heroplayer == null || fGameManager.Heroplayer.delayNet;
		if (!flag)
		{
			bool main = this.Entity_Component.Main;
			if (main)
			{
				this.Update_Synchronization();
			}
			else
			{
				bool flag2 = this.Entity_Component.UserControl == null || this.Entity_Component.Position == Vector3.zero || this.Entity_Component.Type == 8 || !this.Entity_Component.Need_Synchronization || this._Rigidbody_Component == null;
				if (!flag2)
				{
					bool flag3 = this.Entity_Component.Type == 4;
					if (flag3)
					{
						bool flag4 = Vector3.Distance(this.Entity_Component.UserControl.transform.localPosition, this.Entity_Component.Position) > 1f;
						if (flag4)
						{
							this.Entity_Component.UserControl.transform.localPosition = this.Entity_Component.Position;
							this.Entity_Component.UserControl.transform.localRotation = Quaternion.Euler(this.Entity_Component.Rotation);
						}
						else
						{
							this.Rigidbody_Component.MovePosition(Vector3.LerpUnclamped(this.Entity_Component.UserControl.transform.localPosition, this.Entity_Component.Position, 0.1f));
							this.Rigidbody_Component.MoveRotation(Quaternion.LerpUnclamped(this.Entity_Component.UserControl.transform.localRotation, Quaternion.Euler(this.Entity_Component.Rotation), 0.1f));
						}
					}
					else
					{
						this.Rigidbody_Component.MovePosition(Vector3.LerpUnclamped(this.Entity_Component.UserControl.transform.localPosition, this.Entity_Component.Position, 0.1f));
						this.Rigidbody_Component.MoveRotation(Quaternion.LerpUnclamped(this.Entity_Component.UserControl.transform.localRotation, Quaternion.Euler(this.Entity_Component.Rotation), 0.1f));
					}
				}
			}
		}
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x0010761C File Offset: 0x0010581C
	public virtual void Update_Synchronization()
	{
		bool flag = this.Entity_Component.UserControl == null || this.Entity_Component.Death_State;
		if (!flag)
		{
			bool flag2 = Time.time > this._Update_Synchronization_Time;
			if (flag2)
			{
				this._Update_Synchronization_Time = Time.time + 0.013f + UnityEngine.Random.Range(0.01f, 0.09f);
				bool main = this.Entity_Component.Main;
				if (main)
				{
					this.Synchronization();
				}
			}
			bool flag3 = this.Entity_Component.AI_ID > 0;
			if (flag3)
			{
				bool flag4 = Time.time > this._Update_Auto_Detection_Time;
				if (flag4)
				{
					this._Update_Auto_Detection_Time = Time.time + 1f;
					bool main2 = this.Entity_Component.Main;
					if (main2)
					{
						this.Auto_Detection();
					}
				}
			}
			else
			{
				bool flag5 = Time.time > this._Update_Auto_Detection_Time;
				if (flag5)
				{
					this._Update_Auto_Detection_Time = Time.time + 0.033f;
					bool main3 = this.Entity_Component.Main;
					if (main3)
					{
						this.Auto_Detection();
					}
				}
			}
		}
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x00107738 File Offset: 0x00105938
	public virtual void Auto_Detection()
	{
		bool flag = fGameManager.Npc_Action_State && this.Entity_Component.Action_State && !this.Entity_Component.Death_State && !(this.Entity_Component.wc == null) && fGameManager.Heroplayer != null && !fGameManager.Heroplayer.delayNet;
		if (flag)
		{
			this.Detection();
		}
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x0010779E File Offset: 0x0010599E
	public virtual void Detection()
	{
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x001077A1 File Offset: 0x001059A1
	public virtual void Attack_Start()
	{
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x001077A4 File Offset: 0x001059A4
	public virtual void Attack_Stop()
	{
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x001077A8 File Offset: 0x001059A8
	public void Start_All_Action()
	{
		bool flag = this.Animator_Component != null;
		if (flag)
		{
			this.Animator_Component.enabled = true;
		}
		bool flag2 = this.Entity_Component.AI_Component != null;
		if (flag2)
		{
			this.Entity_Component.AI_Component.Start_All_Action();
		}
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x00107800 File Offset: 0x00105A00
	public void Stop_All_Action()
	{
		bool flag = this.Animator_Component != null;
		if (flag)
		{
			this.Animator_Component.enabled = false;
		}
		bool flag2 = this.Entity_Component.AI_Component != null;
		if (flag2)
		{
			this.Entity_Component.AI_Component.Stop_All_Action();
		}
	}

	// Token: 0x04001917 RID: 6423
	private Npc_View _View_Component;

	// Token: 0x04001918 RID: 6424
	private Npc_Model _Model_Component;

	// Token: 0x04001919 RID: 6425
	private Npc_Entity _Entity_Component;

	// Token: 0x0400191A RID: 6426
	private GameObject _Model_Object;

	// Token: 0x0400191B RID: 6427
	private Rigidbody _Rigidbody_Component;

	// Token: 0x0400191C RID: 6428
	private GameObject _Death_Effect_Object;

	// Token: 0x0400191D RID: 6429
	private GameObject _Death_Sound_Object;

	// Token: 0x0400191E RID: 6430
	private AudioSource _Death_Sound_Audio_Source;

	// Token: 0x0400191F RID: 6431
	private Animator _Animator_Component;

	// Token: 0x04001920 RID: 6432
	private RuntimeAnimatorController _Runtime_Animator_Controller_Component;

	// Token: 0x04001921 RID: 6433
	private Vector3 _Previous_Position = Vector3.zero;

	// Token: 0x04001922 RID: 6434
	private Vector3 _Previous_Rotation = Vector3.zero;

	// Token: 0x04001923 RID: 6435
	private Vector3 _Previous_Target_Position = Vector3.zero;

	// Token: 0x04001924 RID: 6436
	private Vector3 _Previous_Target_Point = Vector3.zero;

	// Token: 0x04001925 RID: 6437
	public float _Update_Synchronization_Time;

	// Token: 0x04001926 RID: 6438
	public float _Update_Auto_Detection_Time;
}
