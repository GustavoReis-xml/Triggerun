using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class Sentry_Controller : Npc_Controller
{
	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x060022EB RID: 8939 RVA: 0x00142518 File Offset: 0x00140718
	// (set) Token: 0x060022EC RID: 8940 RVA: 0x00142573 File Offset: 0x00140773
	public GameObject Rotation_X_Axie
	{
		get
		{
			bool flag = this._Rotation_X_Axie == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.Entity_Component.thisModel, "X_Axie");
				bool flag2 = transform != null;
				if (flag2)
				{
					this._Rotation_X_Axie = transform.gameObject;
				}
			}
			return this._Rotation_X_Axie;
		}
		set
		{
			this._Rotation_X_Axie = value;
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x060022ED RID: 8941 RVA: 0x00142580 File Offset: 0x00140780
	// (set) Token: 0x060022EE RID: 8942 RVA: 0x001425DB File Offset: 0x001407DB
	public GameObject Rotation_Z_Axie
	{
		get
		{
			bool flag = this._Rotation_Z_Axie == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.Entity_Component.thisModel, "Z_Axie");
				bool flag2 = transform != null;
				if (flag2)
				{
					this._Rotation_Z_Axie = transform.gameObject;
				}
			}
			return this._Rotation_Z_Axie;
		}
		set
		{
			this._Rotation_Z_Axie = value;
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x060022EF RID: 8943 RVA: 0x001425E8 File Offset: 0x001407E8
	// (set) Token: 0x060022F0 RID: 8944 RVA: 0x001426D1 File Offset: 0x001408D1
	public GameObject Look_At_Object
	{
		get
		{
			bool flag = this._Look_At_Object == null;
			if (flag)
			{
				Vector3 zero = Vector3.zero;
				SkinnedMeshRenderer componentInChildren = base.Entity_Component.thisModel.GetComponentInChildren<SkinnedMeshRenderer>();
				bool flag2 = componentInChildren != null;
				if (flag2)
				{
					zero = new Vector3(0f, componentInChildren.bounds.size.y, 0f);
				}
				this._Look_At_Object = new GameObject("Look_At_Object");
				this._Look_At_Object.transform.parent = base.Entity_Component.thisModel.transform;
				this._Look_At_Object.transform.localPosition = zero;
				this._Look_At_Object.transform.localEulerAngles = Vector3.zero;
				this._Look_At_Object.transform.localScale = Vector3.one;
			}
			return this._Look_At_Object;
		}
		set
		{
			this._Look_At_Object = value;
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x060022F1 RID: 8945 RVA: 0x001426DC File Offset: 0x001408DC
	// (set) Token: 0x060022F2 RID: 8946 RVA: 0x0014270E File Offset: 0x0014090E
	public List<GameObject> Emitter_List
	{
		get
		{
			bool flag = this._Emitter_List == null;
			if (flag)
			{
				this._Emitter_List = new List<GameObject>();
			}
			return this._Emitter_List;
		}
		set
		{
			this._Emitter_List = value;
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00142718 File Offset: 0x00140918
	// (set) Token: 0x060022F4 RID: 8948 RVA: 0x00142730 File Offset: 0x00140930
	public Vector3 Direction
	{
		get
		{
			return this._Direction;
		}
		set
		{
			this._Direction = value;
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0014273C File Offset: 0x0014093C
	// (set) Token: 0x060022F6 RID: 8950 RVA: 0x00142754 File Offset: 0x00140954
	public Quaternion Target_Rotation
	{
		get
		{
			return this._Target_Rotation;
		}
		set
		{
			this._Target_Rotation = value;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x060022F7 RID: 8951 RVA: 0x00142760 File Offset: 0x00140960
	// (set) Token: 0x060022F8 RID: 8952 RVA: 0x00142778 File Offset: 0x00140978
	public Vector3 Euler_Angles
	{
		get
		{
			return this._Euler_Angles;
		}
		set
		{
			this._Euler_Angles = value;
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00142784 File Offset: 0x00140984
	// (set) Token: 0x060022FA RID: 8954 RVA: 0x0014279C File Offset: 0x0014099C
	public GameObject Target_Object
	{
		get
		{
			return this._Target_Object;
		}
		set
		{
			this._Target_Object = value;
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x060022FB RID: 8955 RVA: 0x001427A8 File Offset: 0x001409A8
	// (set) Token: 0x060022FC RID: 8956 RVA: 0x001427C0 File Offset: 0x001409C0
	public int Target_Net_ID
	{
		get
		{
			return this._Target_Net_ID;
		}
		set
		{
			this._Target_Net_ID = value;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x060022FD RID: 8957 RVA: 0x001427CC File Offset: 0x001409CC
	// (set) Token: 0x060022FE RID: 8958 RVA: 0x001427E4 File Offset: 0x001409E4
	public NetLiving Target_Net_Object
	{
		get
		{
			return this._Target_Net_Object;
		}
		set
		{
			this._Target_Net_Object = value;
		}
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x001427F0 File Offset: 0x001409F0
	public override void Update()
	{
		bool flag = fGameManager.Heroplayer == null || fGameManager.Heroplayer.delayNet;
		if (!flag)
		{
			bool flag2 = this.Target_Object != null;
			if (flag2)
			{
				bool flag3 = ((CWeaponControlMe)base.Entity_Component.wc).NetObject == null || !base.Entity_Component.Can_Shoot;
				if (flag3)
				{
					return;
				}
				((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", this.Target_Object.transform.position, this.Target_Object);
			}
			base.Update();
			bool flag4 = !base.Entity_Component.Main && !(base.Entity_Component.UserControl == null) && !(base.Entity_Component.thisModel == null) && Time.time >= this._Update_Time;
			if (flag4)
			{
				this._Update_Time = Time.time + 0.033f;
				this.Direction = base.Entity_Component.Target_Position - base.Entity_Component.UserControl.transform.position;
				this.Target_Rotation = Quaternion.LookRotation(this.Direction);
				this.Euler_Angles = this.Target_Rotation.eulerAngles;
				bool flag5 = this.Rotation_X_Axie != null;
				if (flag5)
				{
					this.Rotation_X_Axie.transform.eulerAngles = new Vector3(this.Rotation_X_Axie.transform.eulerAngles.x, this.Euler_Angles.y, 0f);
				}
				bool flag6 = this.Rotation_Z_Axie != null;
				if (flag6)
				{
					this.Rotation_Z_Axie.transform.eulerAngles = new Vector3(this.Euler_Angles.x, this.Rotation_Z_Axie.transform.eulerAngles.y, 0f);
				}
			}
		}
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x001429EC File Offset: 0x00140BEC
	public void Initialize_Emitter_Object()
	{
		Transform transform = Fun.Get_Child_Transform(base.Entity_Component.thisModel, "m1");
		bool flag = transform != null;
		if (flag)
		{
			this.Emitter_List.Add(transform.gameObject);
		}
		transform = Fun.Get_Child_Transform(base.Entity_Component.thisModel, "m2");
		bool flag2 = transform != null;
		if (flag2)
		{
			this.Emitter_List.Add(transform.gameObject);
		}
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00142A68 File Offset: 0x00140C68
	public override void Detection()
	{
		Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
		int num = -1;
		GameObject target_Object = null;
		bool flag = !base.Entity_Component.Main;
		if (!flag)
		{
			bool flag2 = base.Entity_Component.Detection_Controller_Component == null;
			if (flag2)
			{
				base.Entity_Component.Target_Net_ID = -1;
				bool flag3 = base.Entity_Component.AI_Component != null;
				if (flag3)
				{
					base.Entity_Component.AI_Component.Attack_Stop();
				}
				else
				{
					base.Entity_Component._Animator_Component.SetBool("Attack", false);
				}
			}
			else
			{
				bool death_State = base.Entity_Component.Death_State;
				if (death_State)
				{
					base.Entity_Component.Target_Net_ID = -1;
					bool flag4 = base.Entity_Component.AI_Component != null;
					if (flag4)
					{
						base.Entity_Component.AI_Component.Attack_Stop();
					}
					else
					{
						base.Entity_Component._Animator_Component.SetBool("Attack", false);
					}
				}
				else
				{
					bool flag5 = !base.Entity_Component.Can_Shoot;
					if (flag5)
					{
						bool flag6 = base.Entity_Component.AI_Component != null;
						if (flag6)
						{
							base.Entity_Component.AI_Component.Attack_Stop();
						}
						else
						{
							base.Entity_Component._Animator_Component.SetBool("Attack", false);
						}
					}
					else
					{
						bool flag7 = base.Entity_Component.AI_ID == 1104;
						if (flag7)
						{
							base.Entity_Component.playerTarget.transform.position = new Vector3(base.Entity_Component.UserControl.transform.position.x + 20f, base.Entity_Component.UserControl.transform.position.y + 1.8f, base.Entity_Component.UserControl.transform.position.z);
							bool flag8 = ((CWeaponControlMe)base.Entity_Component.wc).NetObject == null;
							if (flag8)
							{
								bool flag9 = base.Entity_Component.AI_Component != null;
								if (flag9)
								{
									base.Entity_Component.AI_Component.Attack_Stop();
								}
								else
								{
									base.Entity_Component._Animator_Component.SetBool("Attack", false);
								}
								return;
							}
							((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", base.Entity_Component.playerTarget.transform.position, base.Entity_Component.playerTarget);
						}
						else
						{
							dictionary = base.Entity_Component.Detection_Controller_Component.Range_Detection(base.Entity_Component.Eye_Object.transform.position);
							float num2 = 0f;
							List<int> list = new List<int>(dictionary.Keys);
							for (int i = 0; i < list.Count; i++)
							{
								int num3 = list[i];
								GameObject gameObject = dictionary[num3];
								bool flag10 = num2 == 0f || num2 > Vector3.Distance(base.Entity_Component.UserControl.transform.position, gameObject.transform.position);
								if (flag10)
								{
									num2 = Vector3.Distance(base.Entity_Component.UserControl.transform.position, gameObject.transform.position);
									num = num3;
									target_Object = gameObject;
								}
							}
							bool flag11 = num < 0;
							if (flag11)
							{
								this.Target_Object = null;
								bool flag12 = base.Entity_Component.AI_Component != null;
								if (flag12)
								{
									base.Entity_Component.AI_Component.Attack_Stop();
								}
								else
								{
									base.Entity_Component._Animator_Component.SetBool("Attack", false);
								}
								return;
							}
							bool flag13 = this.Target_Object != null;
							if (flag13)
							{
								NetLiving netObject = this.Target_Object.transform.root.gameObject.GetComponent<PublicInfo>().NetObject;
								bool flag14 = netObject.isDead || !dictionary.ContainsKey(netObject.getNetid());
								if (flag14)
								{
									this.Target_Object = target_Object;
								}
							}
							else
							{
								this.Target_Object = target_Object;
							}
							bool flag15 = ((CWeaponControlMe)base.Entity_Component.wc).NetObject == null;
							if (flag15)
							{
								bool flag16 = base.Entity_Component.AI_Component != null;
								if (flag16)
								{
									base.Entity_Component.AI_Component.Attack_Stop();
								}
								else
								{
									base.Entity_Component._Animator_Component.SetBool("Attack", false);
								}
								return;
							}
							((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", this.Target_Object.transform.position, this.Target_Object);
							this.LookAt(this.Target_Object, num);
							base.Entity_Component.Target_Net_ID = num;
						}
						bool flag17 = base.Entity_Component.AI_Component != null;
						if (flag17)
						{
							base.Entity_Component.AI_Component.Switch_Animation("Attack");
						}
						else
						{
							base.Entity_Component._Animator_Component.SetBool("Attack", true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00142FC8 File Offset: 0x001411C8
	private void LookAt(GameObject _Value, int _Net_ID)
	{
		Vector3 vector = Vector3.zero;
		this.Look_At_Object.transform.LookAt(_Value.transform);
		vector = this.Look_At_Object.transform.eulerAngles;
		bool main = base.Entity_Component.Main;
		if (main)
		{
			base.Entity_Component.Target_Position = _Value.transform.position;
			base.Entity_Component.Target_Net_ID = _Net_ID;
		}
		bool flag = this.Rotation_X_Axie != null;
		if (flag)
		{
			this.Rotation_X_Axie.transform.eulerAngles = new Vector3(this.Rotation_X_Axie.transform.eulerAngles.x, vector.y, 0f);
		}
		bool flag2 = this.Rotation_Z_Axie != null;
		if (flag2)
		{
			this.Rotation_Z_Axie.transform.eulerAngles = new Vector3(vector.x, this.Rotation_X_Axie.transform.eulerAngles.y, 0f);
		}
		for (int i = 0; i < this.Emitter_List.Count; i++)
		{
			this.Emitter_List[i].transform.LookAt(_Value.transform);
		}
	}

	// Token: 0x04001FF0 RID: 8176
	private GameObject _Rotation_X_Axie;

	// Token: 0x04001FF1 RID: 8177
	private GameObject _Rotation_Z_Axie;

	// Token: 0x04001FF2 RID: 8178
	private GameObject _Look_At_Object;

	// Token: 0x04001FF3 RID: 8179
	private List<GameObject> _Emitter_List;

	// Token: 0x04001FF4 RID: 8180
	private Vector3 _Direction = Vector3.zero;

	// Token: 0x04001FF5 RID: 8181
	private Quaternion _Target_Rotation = default(Quaternion);

	// Token: 0x04001FF6 RID: 8182
	private Vector3 _Euler_Angles = Vector3.zero;

	// Token: 0x04001FF7 RID: 8183
	private GameObject _Target_Object;

	// Token: 0x04001FF8 RID: 8184
	private int _Target_Net_ID;

	// Token: 0x04001FF9 RID: 8185
	private NetLiving _Target_Net_Object;

	// Token: 0x04001FFA RID: 8186
	public float _Update_Time;
}
