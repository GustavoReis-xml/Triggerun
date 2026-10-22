using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class Door_Control : MonoBehaviour
{
	// Token: 0x17000191 RID: 401
	// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0004BA10 File Offset: 0x00049C10
	// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0004BA27 File Offset: 0x00049C27
	public static GameObject Door_Control_Object
	{
		get
		{
			return Door_Control._Door_Control_Object;
		}
		set
		{
			Door_Control._Door_Control_Object = value;
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0004BA30 File Offset: 0x00049C30
	// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0004BA82 File Offset: 0x00049C82
	public static Door_Control Get_Instantiate
	{
		get
		{
			bool flag = Door_Control._Get_Instantiate == null;
			if (flag)
			{
				Door_Control.Door_Control_Object = new GameObject("Door_Control_Object");
				Door_Control._Get_Instantiate = Door_Control.Door_Control_Object.AddComponent<Door_Control>();
				Door_Control._Get_Instantiate.Fill();
			}
			return Door_Control._Get_Instantiate;
		}
		set
		{
			Door_Control._Get_Instantiate = value;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0004BA8C File Offset: 0x00049C8C
	// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0004BAC6 File Offset: 0x00049CC6
	public GameObject Parent_Object
	{
		get
		{
			bool flag = this._Parent_Object == null;
			if (flag)
			{
				this._Parent_Object = GameObject.FindGameObjectWithTag("Door");
			}
			return this._Parent_Object;
		}
		set
		{
			this._Parent_Object = value;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0004BAD0 File Offset: 0x00049CD0
	// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0004BB0B File Offset: 0x00049D0B
	public Door_Data Door_Data_Component
	{
		get
		{
			bool flag = this._Door_Data_Component == null;
			if (flag)
			{
				this._Door_Data_Component = this.Parent_Object.GetComponent<Door_Data>();
			}
			return this._Door_Data_Component;
		}
		set
		{
			this._Door_Data_Component = value;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0004BB18 File Offset: 0x00049D18
	// (set) Token: 0x060008E9 RID: 2281 RVA: 0x0004BB4A File Offset: 0x00049D4A
	public List<Animation> General_Door_Object_Array_List
	{
		get
		{
			bool flag = this._General_Door_Object_Array_List == null;
			if (flag)
			{
				this._General_Door_Object_Array_List = new List<Animation>();
			}
			return this._General_Door_Object_Array_List;
		}
		set
		{
			this._General_Door_Object_Array_List = value;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x060008EA RID: 2282 RVA: 0x0004BB54 File Offset: 0x00049D54
	// (set) Token: 0x060008EB RID: 2283 RVA: 0x0004BB86 File Offset: 0x00049D86
	public List<GameObject> Effect_Door_Object_Array_List
	{
		get
		{
			bool flag = this._Effect_Door_Object_Array_List == null;
			if (flag)
			{
				this._Effect_Door_Object_Array_List = new List<GameObject>();
			}
			return this._Effect_Door_Object_Array_List;
		}
		set
		{
			this._Effect_Door_Object_Array_List = value;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x060008EC RID: 2284 RVA: 0x0004BB90 File Offset: 0x00049D90
	// (set) Token: 0x060008ED RID: 2285 RVA: 0x0004BBC2 File Offset: 0x00049DC2
	public List<Animation> Special_Door_Object_Array_List
	{
		get
		{
			bool flag = this._Special_Door_Object_Array_List == null;
			if (flag)
			{
				this._Special_Door_Object_Array_List = new List<Animation>();
			}
			return this._Special_Door_Object_Array_List;
		}
		set
		{
			this._Special_Door_Object_Array_List = value;
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0004BBCC File Offset: 0x00049DCC
	private void Fill()
	{
		bool flag = this.Parent_Object == null;
		if (!flag)
		{
			this.Effect_Door_Object_Array_List = new List<GameObject>();
			this.Special_Door_Object_Array_List = new List<Animation>();
			this.General_Door_Object_Array_List = new List<Animation>();
			foreach (object obj in this.Parent_Object.transform)
			{
				Transform transform = (Transform)obj;
				string[] array = transform.name.Split(new char[]
				{
					'_'
				});
				bool flag2 = array[0].ToString() == "Effect";
				if (flag2)
				{
					this.Effect_Door_Object_Array_List.Add(transform.gameObject);
				}
				else
				{
					bool flag3 = array[0].ToString() == "jiguanmen";
					if (flag3)
					{
						this.Special_Door_Object_Array_List.Add(transform.gameObject.GetComponent<Animation>());
					}
					else
					{
						this.General_Door_Object_Array_List.Add(transform.gameObject.GetComponent<Animation>());
					}
				}
			}
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0004BD00 File Offset: 0x00049F00
	public int General_Door_Handle(int _Phase, string _Animation_Name, bool _State = true)
	{
		bool flag = this.Parent_Object == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = _Phase < 1;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				for (int i = 0; i < this.General_Door_Object_Array_List.Count; i++)
				{
					Animation animation = this.General_Door_Object_Array_List[i];
					bool flag3 = animation != null;
					if (flag3)
					{
						bool flag4 = animation.CompareTag("Door_Phase_" + _Phase.ToString()) && !animation.IsPlaying(_Animation_Name);
						if (flag4)
						{
							animation.Play(_Animation_Name);
						}
					}
				}
				if (_State)
				{
					_Phase--;
					bool flag5 = _Phase > 0;
					if (flag5)
					{
						this.General_Door_Handle(_Phase, _Animation_Name, true);
					}
				}
				result = this.General_Door_Object_Array_List.Count;
			}
		}
		return result;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0004BDE0 File Offset: 0x00049FE0
	public void Special_Door_Handle(int _Phase, string _Animation_Name)
	{
		bool flag = this.Parent_Object == null || _Phase < 1;
		if (!flag)
		{
			for (int i = 0; i < this.Special_Door_Object_Array_List.Count; i++)
			{
				Animation animation = this.Special_Door_Object_Array_List[i];
				bool flag2 = animation != null;
				if (flag2)
				{
					bool flag3 = animation.CompareTag("Special_Door_" + _Phase.ToString());
					if (flag3)
					{
						animation.Play(_Animation_Name);
					}
				}
			}
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0004BE70 File Offset: 0x0004A070
	public int Effect_Door_Handle(int _Country, bool _State = false)
	{
		bool flag = this.Parent_Object == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			if (_Country <= 0 && fGameManager.Heroplayer != null && fGameManager.Heroplayer.nForce > 0)
			{
				_Country = fGameManager.Heroplayer.nForce;
			}
			if (_Country <= 0 && PlayerDataManager.getInstance() != null && PlayerDataManager.getInstance().playercom != null && PlayerDataManager.getInstance().playercom.m_nShili > 0)
			{
				_Country = PlayerDataManager.getInstance().playercom.m_nShili;
			}
			if (_Country <= 0)
			{
				_Country = 1;
			}
			for (int i = 0; i < this.Effect_Door_Object_Array_List.Count; i++)
			{
				GameObject gameObject = this.Effect_Door_Object_Array_List[i];
				if (_State)
				{
					gameObject.gameObject.SetActive(false);
				}
				else if (_Country != 1)
				{
					if (_Country == 2)
					{
						bool flag2 = gameObject.CompareTag("Country_Door_2");
						int layer;
						if (flag2)
						{
							layer = 21;
							foreach (Renderer renderer in gameObject.transform.GetComponentsInChildren<Renderer>())
							{
								renderer.enabled = false;
							}
						}
						else
						{
							layer = 20;
						}
						foreach (Collider collider in gameObject.transform.GetComponentsInChildren<Collider>())
						{
							collider.gameObject.layer = layer;
						}
					}
				}
				else
				{
					bool flag3 = gameObject.CompareTag("Country_Door_1");
					int layer;
					if (flag3)
					{
						layer = 20;
						foreach (Renderer renderer in gameObject.transform.GetComponentsInChildren<Renderer>())
						{
							renderer.enabled = false;
						}
					}
					else
					{
						layer = 21;
					}
					foreach (Collider collider in gameObject.transform.GetComponentsInChildren<Collider>())
					{
						collider.gameObject.layer = layer;
					}
				}
			}
			result = this.Effect_Door_Object_Array_List.Count;
		}
		return result;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0004C050 File Offset: 0x0004A250
	public void Effect_Door_Handle(int _Country, int _Phase, bool _State)
	{
		GameObject[] array = null;
		int layer = 0;
		bool flag = _Phase < 1;
		if (flag)
		{
			_Phase = 1;
		}
		if (_Country != 1)
		{
			if (_Country == 2)
			{
				array = this.Door_Data_Component._Door_Effect_Data_Country_2;
				layer = 21;
			}
		}
		else
		{
			array = this.Door_Data_Component._Door_Effect_Data_Country_1;
			layer = 20;
		}
		foreach (GameObject gameObject in array)
		{
bool flag2 = gameObject.CompareTag("Effect_Door_Phase_" + _Phase.ToString());
			if (flag2)
			{
				foreach (Collider collider in gameObject.transform.GetComponentsInChildren<Collider>())
				{
					collider.gameObject.layer = layer;
					collider.enabled = _State;
				}
				foreach (Renderer renderer in gameObject.transform.GetComponentsInChildren<Renderer>())
				{
					renderer.enabled = _State;
				}
			}
		}
		if (_Country != 1)
		{
			if (_Country == 2)
			{
				array = this.Door_Data_Component._Door_Effect_Data_Country_1;
				layer = 20;
			}
		}
		else
		{
			array = this.Door_Data_Component._Door_Effect_Data_Country_2;
			layer = 21;
		}
		foreach (GameObject gameObject in array)
		{
bool flag3 = gameObject.CompareTag("Effect_Door_Phase_" + _Phase.ToString());
			if (flag3)
			{
				foreach (Collider collider in gameObject.transform.GetComponentsInChildren<Collider>())
				{
					collider.gameObject.layer = layer;
					collider.enabled = !_State;
				}
				foreach (Renderer renderer in gameObject.transform.GetComponentsInChildren<Renderer>())
				{
					renderer.enabled = !_State;
				}
			}
		}
		int num = _Phase - 1;
		for (int i = 0; i < this.Door_Data_Component._Door_Effect_Data_Country_1.Length; i++)
		{
			bool flag4 = num >= 1;
			if (flag4)
			{
				GameObject gameObject = this.Door_Data_Component._Door_Effect_Data_Country_1[i];
				bool flag5 = gameObject.CompareTag("Effect_Door_Phase_" + num.ToString());
				if (flag5)
				{
					gameObject.SetActive(false);
				}
			}
		}
		num = _Phase - 1;
		for (int i = 0; i < this.Door_Data_Component._Door_Effect_Data_Country_2.Length; i++)
		{
			bool flag6 = num >= 1;
			if (flag6)
			{
				GameObject gameObject = this.Door_Data_Component._Door_Effect_Data_Country_2[i];
				bool flag7 = gameObject.CompareTag("Effect_Door_Phase_" + num.ToString());
				if (flag7)
				{
					gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0004C33B File Offset: 0x0004A53B
	public void Delete_All_Door()
	{
		UnityEngine.Object.Destroy(this.Parent_Object);
		UnityEngine.Object.Destroy(GameObject.Find("fangxiangxian"));
	}

	// Token: 0x0400067B RID: 1659
	private static GameObject _Door_Control_Object;

	// Token: 0x0400067C RID: 1660
	private static Door_Control _Get_Instantiate;

	// Token: 0x0400067D RID: 1661
	private GameObject _Parent_Object;

	// Token: 0x0400067E RID: 1662
	private Door_Data _Door_Data_Component;

	// Token: 0x0400067F RID: 1663
	private List<Animation> _General_Door_Object_Array_List;

	// Token: 0x04000680 RID: 1664
	private List<GameObject> _Effect_Door_Object_Array_List;

	// Token: 0x04000681 RID: 1665
	private List<Animation> _Special_Door_Object_Array_List;
}
