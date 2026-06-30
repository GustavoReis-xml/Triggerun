using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class Detection_Controller : MonoBehaviour
{
	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00049B5C File Offset: 0x00047D5C
	// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00049B74 File Offset: 0x00047D74
	public int Type
	{
		get
		{
			return this._Type;
		}
		set
		{
			this._Type = value;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00049B80 File Offset: 0x00047D80
	// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00049B98 File Offset: 0x00047D98
	public int Country
	{
		get
		{
			return this._Country;
		}
		set
		{
			this._Country = value;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00049BA4 File Offset: 0x00047DA4
	// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00049BBC File Offset: 0x00047DBC
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

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00049BC8 File Offset: 0x00047DC8
	// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00049BE0 File Offset: 0x00047DE0
	public float Detection_Distance
	{
		get
		{
			return this._Detection_Distance;
		}
		set
		{
			this._Detection_Distance = value;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00049BEC File Offset: 0x00047DEC
	// (set) Token: 0x060008BA RID: 2234 RVA: 0x00049C04 File Offset: 0x00047E04
	public float Detection_Angle
	{
		get
		{
			return this._Detection_Angle;
		}
		set
		{
			this._Detection_Angle = value;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060008BB RID: 2235 RVA: 0x00049C0E File Offset: 0x00047E0E
	public Quaternion Current_Euler
	{
		get
		{
			return base.gameObject.transform.rotation;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060008BC RID: 2236 RVA: 0x00049C20 File Offset: 0x00047E20
	public Quaternion Left_Euler
	{
		get
		{
			return Quaternion.Euler(base.gameObject.transform.rotation.eulerAngles.x, base.gameObject.transform.rotation.eulerAngles.y - this.Detection_Angle, base.gameObject.transform.rotation.eulerAngles.z);
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060008BD RID: 2237 RVA: 0x00049C90 File Offset: 0x00047E90
	public Quaternion Right_Euler
	{
		get
		{
			return Quaternion.Euler(base.gameObject.transform.rotation.eulerAngles.x, base.gameObject.transform.rotation.eulerAngles.y + this.Detection_Angle, base.gameObject.transform.rotation.eulerAngles.z);
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060008BE RID: 2238 RVA: 0x00049D00 File Offset: 0x00047F00
	public Vector3 Current_Terminal
	{
		get
		{
			return base.gameObject.transform.position + this.Current_Euler * Vector3.forward * this.Detection_Distance;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060008BF RID: 2239 RVA: 0x00049D32 File Offset: 0x00047F32
	public Vector3 Left_Terminal
	{
		get
		{
			return base.gameObject.transform.position + this.Left_Euler * Vector3.forward * this.Detection_Distance;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00049D64 File Offset: 0x00047F64
	public Vector3 Right_Terminal
	{
		get
		{
			return base.gameObject.transform.position + this.Right_Euler * Vector3.forward * this.Detection_Distance;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00049D98 File Offset: 0x00047F98
	// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00049DB0 File Offset: 0x00047FB0
	public float Detection_Range
	{
		get
		{
			return this._Detection_Range;
		}
		set
		{
			this._Detection_Range = value;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00049DBC File Offset: 0x00047FBC
	public LayerMask Detection_Layer
	{
		get
		{
			int type = this.Type;
			int num = type;
			LayerMask result;
			if (num != 1)
			{
				if (num != 4)
				{
					if (num != 14)
					{
						result = LayerManager.EnemyLayer(this.Country);
					}
					else
					{
						bool flag = this.Country == 1;
						if (flag)
						{
							result = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_mainplayer"));
						}
						else
						{
							bool flag2 = this.Country == 2;
							if (flag2)
							{
								result = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
							}
							else
							{
								result = 0;
							}
						}
					}
				}
				else
				{
					result = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("force1_npc"));
				}
			}
			else
			{
				bool flag3 = this.Country == 1;
				if (flag3)
				{
					result = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
				}
				else
				{
					bool flag4 = this.Country == 2;
					if (flag4)
					{
						result = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_mainplayer"));
					}
					else
					{
						result = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
					}
				}
			}
			return result;
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00049FB0 File Offset: 0x000481B0
	public bool Area_Detection(GameObject _Target_Object, float _Distance, float _Angle)
	{
		this.Target_Object = _Target_Object;
		this.Detection_Distance = _Distance;
		this.Detection_Angle = _Angle;
		Debug.DrawLine(base.gameObject.transform.position, this.Current_Terminal, Color.red);
		Debug.DrawLine(base.gameObject.transform.position, this.Left_Terminal, Color.red);
		Debug.DrawLine(base.gameObject.transform.position, this.Right_Terminal, Color.red);
		Debug.DrawLine(this.Left_Terminal, this.Right_Terminal, Color.red);
		bool result = this.In_Triangle(this.Target_Object.transform.position);
		this.Target_Object = null;
		this.Detection_Distance = 0f;
		this.Detection_Angle = 0f;
		return result;
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0004A090 File Offset: 0x00048290
	private bool In_Triangle(Vector3 _Position)
	{
		float x = _Position.x;
		float z = _Position.z;
		float x2 = base.gameObject.transform.position.x;
		float z2 = base.gameObject.transform.position.z;
		float x3 = this.Left_Terminal.x;
		float z3 = this.Left_Terminal.z;
		float x4 = this.Right_Terminal.x;
		float z4 = this.Right_Terminal.z;
		float num = this.Triangle_Area(x2, z2, x3, z3, x4, z4);
		float num2 = this.Triangle_Area(x2, z2, x3, z3, x, z) + this.Triangle_Area(x2, z2, x, z, x4, z4) + this.Triangle_Area(x, z, x3, z3, x4, z4);
		return Mathf.Abs(num - num2) <= 0.01f;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0004A178 File Offset: 0x00048378
	private float Triangle_Area(float v0x, float v0y, float v1x, float v1y, float v2x, float v2y)
	{
		return Mathf.Abs((v0x * v1y + v1x * v2y + v2x * v0y - v1x * v0y - v2x * v1y - v0x * v2y) / 2f);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0004A1B4 File Offset: 0x000483B4
	public Dictionary<int, GameObject> Range_Detection(Vector3 _Position)
	{
		Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
		Vector3 direction = Vector3.zero;
		bool flag = this.Detection_Range != 0f;
		if (flag)
		{
			foreach (Collider collider in Physics.OverlapSphere(_Position, this.Detection_Range, this.Detection_Layer))
			{
				direction = collider.gameObject.transform.position - _Position;
				float maxDistance = Vector3.Distance(_Position, collider.gameObject.transform.position);
				bool flag2 = Physics.Raycast(_Position, direction, maxDistance, 1 << LayerMask.NameToLayer("Default")) || collider.gameObject.transform.root.gameObject == base.gameObject;
				if (!flag2)
				{
					Npc_Data component = collider.gameObject.GetComponent<Npc_Data>();
					bool flag3 = component != null;
					PublicInfo publicInfo;
					int netid;
					NetLiving netLiving;
					GameObject value;
					if (flag3)
					{
						bool flag4 = component.Entity_Component.Max_HP < 1;
						if (flag4)
						{
							goto IL_219;
						}
						publicInfo = component.Info_Component;
						netid = component.Entity_Component.getNetid();
						netLiving = component.Entity_Component;
						value = component.Entity_Component.Eye_Object;
					}
					else
					{
						publicInfo = collider.gameObject.transform.root.GetComponent<PublicInfo>();
						bool flag5 = publicInfo == null;
						if (flag5)
						{
							goto IL_219;
						}
						netid = publicInfo.NetObject.getNetid();
						netLiving = publicInfo.NetObject;
						value = netLiving.defaultMesh;
					}
					bool flag6 = netLiving != null && !netLiving.isDead && !netLiving.isCopy && (!(publicInfo.type != "Npc") || publicInfo.NetObject.netobject_type == 6 || this.Type != 1 || !((GamePlayer)publicInfo.NetObject).heroBase.IsHeroInvi()) && !dictionary.ContainsKey(netid);
					if (flag6)
					{
						dictionary.Add(netid, value);
					}
				}
				IL_219:;
			}
		}
		return dictionary;
	}

	// Token: 0x04000650 RID: 1616
	private int _Type;

	// Token: 0x04000651 RID: 1617
	private int _Country;

	// Token: 0x04000652 RID: 1618
	private GameObject _Target_Object;

	// Token: 0x04000653 RID: 1619
	private float _Detection_Distance = 5f;

	// Token: 0x04000654 RID: 1620
	private float _Detection_Angle = 30f;

	// Token: 0x04000655 RID: 1621
	private float _Detection_Range;
}
