using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.Track;

// Token: 0x0200007B RID: 123
public class Chariot_Entity : Npc_Entity
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x0600041B RID: 1051 RVA: 0x00021148 File Offset: 0x0001F348
	// (set) Token: 0x0600041C RID: 1052 RVA: 0x00021160 File Offset: 0x0001F360
	public int Map_ID
	{
		get
		{
			return this._Map_ID;
		}
		set
		{
			this._Map_ID = value;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x0002116C File Offset: 0x0001F36C
	// (set) Token: 0x0600041E RID: 1054 RVA: 0x00021184 File Offset: 0x0001F384
	public int Npc_ID
	{
		get
		{
			return this._Npc_ID;
		}
		set
		{
			this._Npc_ID = value;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600041F RID: 1055 RVA: 0x00021190 File Offset: 0x0001F390
	// (set) Token: 0x06000420 RID: 1056 RVA: 0x000211A8 File Offset: 0x0001F3A8
	public Vector3 Origin_Point
	{
		get
		{
			return this._Origin_Point;
		}
		set
		{
			this._Origin_Point = value;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x000211B4 File Offset: 0x0001F3B4
	// (set) Token: 0x06000422 RID: 1058 RVA: 0x000211CC File Offset: 0x0001F3CC
	public Vector3 Origin_Angle
	{
		get
		{
			return this._Origin_Angle;
		}
		set
		{
			this._Origin_Angle = value;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x000211D8 File Offset: 0x0001F3D8
	// (set) Token: 0x06000424 RID: 1060 RVA: 0x000211F0 File Offset: 0x0001F3F0
	public Vector3 Target_Point
	{
		get
		{
			return this._Target_Point;
		}
		set
		{
			this._Target_Point = value;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x000211FC File Offset: 0x0001F3FC
	// (set) Token: 0x06000426 RID: 1062 RVA: 0x0002122E File Offset: 0x0001F42E
	public Dictionary<int, float> Move_Speed
	{
		get
		{
			bool flag = this._Move_Speed == null;
			if (flag)
			{
				this._Move_Speed = new Dictionary<int, float>();
			}
			return this._Move_Speed;
		}
		set
		{
			this._Move_Speed = value;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x00021238 File Offset: 0x0001F438
	// (set) Token: 0x06000428 RID: 1064 RVA: 0x0002126A File Offset: 0x0001F46A
	public Dictionary<int, ArrayList> Node_Data
	{
		get
		{
			bool flag = this._Node_Data == null;
			if (flag)
			{
				this._Node_Data = new Dictionary<int, ArrayList>();
			}
			return this._Node_Data;
		}
		set
		{
			this._Node_Data = value;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x00021274 File Offset: 0x0001F474
	// (set) Token: 0x0600042A RID: 1066 RVA: 0x000212A6 File Offset: 0x0001F4A6
	public Dictionary<int, ArrayList> Pass_Node_Data
	{
		get
		{
			bool flag = this._Pass_Node_Data == null;
			if (flag)
			{
				this._Pass_Node_Data = new Dictionary<int, ArrayList>();
			}
			return this._Pass_Node_Data;
		}
		set
		{
			this._Pass_Node_Data = value;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x000212B0 File Offset: 0x0001F4B0
	// (set) Token: 0x0600042C RID: 1068 RVA: 0x000212C8 File Offset: 0x0001F4C8
	public float Detection_Range_1
	{
		get
		{
			return this._Detection_Range_1;
		}
		set
		{
			this._Detection_Range_1 = value;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x000212D4 File Offset: 0x0001F4D4
	// (set) Token: 0x0600042E RID: 1070 RVA: 0x000212EC File Offset: 0x0001F4EC
	public float Detection_Range_2
	{
		get
		{
			return this._Detection_Range_2;
		}
		set
		{
			this._Detection_Range_2 = value;
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600042F RID: 1071 RVA: 0x000212F8 File Offset: 0x0001F4F8
	// (set) Token: 0x06000430 RID: 1072 RVA: 0x00021310 File Offset: 0x0001F510
	public int Movement_State
	{
		get
		{
			return this._Movement_State;
		}
		set
		{
			this._Movement_State = value;
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0002131C File Offset: 0x0001F51C
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Chariot_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Chariot_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Chariot_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Chariot_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Chariot_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Chariot_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Chariot_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Chariot_Model>();
			}
		}
		Chariot_Entity chariot_Entity = fGameManager.mloadConfig._Chariot_Xml_Data[base.Index];
		this.Npc_ID = base.Index;
		this.Map_ID = chariot_Entity.Map_ID;
		this.Origin_Point = chariot_Entity.Origin_Point;
		this.Origin_Angle = chariot_Entity.Origin_Angle;
		this.Target_Point = chariot_Entity.Target_Point;
		this.Move_Speed = chariot_Entity.Move_Speed;
		this.Node_Data = chariot_Entity.Node_Data;
		this.Pass_Node_Data = chariot_Entity.Pass_Node_Data;
		this.Detection_Range_1 = chariot_Entity.Detection_Range_1;
		this.Detection_Range_2 = chariot_Entity.Detection_Range_2;
		((Chariot_UI)base.UI_Component).View_Component = (base.View_Component as Chariot_View);
		((Chariot_View)base.View_Component).Controller_Component = (base.Controller_Component as Chariot_Controller);
		((Chariot_Controller)base.Controller_Component).View_Component = (base.View_Component as Chariot_View);
		((Chariot_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Chariot_Model);
		((Chariot_Model)base.Model_Component).Entity_Component = this;
		bool flag9 = base.AI_ID > 0 && base.AI_Component == null;
		if (flag9)
		{
			base.AI_Component = base.Container.GetComponent<Npc_AI_Controller>();
			bool flag10 = base.AI_Component == null;
			if (flag10)
			{
				base.AI_Component = base.Container.AddComponent<Npc_AI_Controller>();
			}
			base.AI_Component.Npc_Entity_Component = this;
			base.AI_Component.Initialize(base.AI_ID);
		}
		bool isCopy = this.isCopy;
		if (!isCopy)
		{
			bool flag11 = base.Detection_Controller_Component == null;
			if (flag11)
			{
				base.Detection_Controller_Component = base.Container.GetComponent<Detection_Controller>();
				bool flag12 = base.Detection_Controller_Component == null;
				if (flag12)
				{
					base.Detection_Controller_Component = base.Container.AddComponent<Detection_Controller>();
				}
				base.Detection_Controller_Component.Type = base.Type;
				base.Detection_Controller_Component.Country = base.Country;
				bool flag13 = base.AI_Component != null;
				if (flag13)
				{
					base.Detection_Controller_Component.Detection_Range = base.AI_Component.Model_Component.Entity_Component.Alert_Range;
				}
				else
				{
					bool flag14 = base.Type == 4;
					if (flag14)
					{
						base.Detection_Controller_Component.Detection_Range = this.Detection_Range_1;
					}
					else
					{
						base.Detection_Controller_Component.Detection_Range = base.Detection_Range;
					}
				}
			}
			((Chariot_Controller)base.Controller_Component).TrackCart_Component = this.UserControl.AddComponent<TrackCart>();
			((Chariot_Controller)base.Controller_Component).TrackCart_Component.clearingDistance = 0.1f;
			((Chariot_View)base.View_Component).Get_Track_Data();
			((Chariot_View)base.View_Component).Create_Node_Object();
			((Chariot_View)base.View_Component).Find_Nearest_Track(((Chariot_Controller)base.Controller_Component).TrackCart_Component);
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0002175C File Offset: 0x0001F95C
	public override void OnDestroy()
	{
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00021760 File Offset: 0x0001F960
	public override void Load_Model_Resouce_Complete()
	{
		base.Load_Model_Resouce_Complete();
		Rigidbody rigidbody = this.thisModel.GetComponent<Rigidbody>();
		bool flag = rigidbody == null;
		if (flag)
		{
			rigidbody = this.thisModel.AddComponent<Rigidbody>();
		}
		rigidbody.isKinematic = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		Chariot_Node_Trigger chariot_Node_Trigger = this.thisModel.AddComponent<Chariot_Node_Trigger>();
		chariot_Node_Trigger.Controller_Component = (Chariot_Controller)base.Controller_Component;
		base.Container.name = "Chariot";
		((Chariot_Controller)base.Controller_Component).Set_ZhanDian_Data();
		Fun.Change_Layer(base.Container, 22);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000217FC File Offset: 0x0001F9FC
	public void NCD(ArrayList _Parameters)
	{
		bool flag = !base.Main;
		if (flag)
		{
			Chariot_View chariot_View = (Chariot_View)base.View_Component;
			Chariot_Controller chariot_Controller = (Chariot_Controller)base.Controller_Component;
			Chariot_Model chariot_Model = (Chariot_Model)base.Model_Component;
			int value = int.Parse(_Parameters[1].ToString());
			chariot_Model.Modify_Current_Node_Number(value);
			chariot_Controller.Set_Track_State((ArrayList)_Parameters[2]);
		}
	}

	// Token: 0x04000343 RID: 835
	private int _Map_ID;

	// Token: 0x04000344 RID: 836
	private int _Npc_ID;

	// Token: 0x04000345 RID: 837
	private Vector3 _Origin_Point = Vector3.zero;

	// Token: 0x04000346 RID: 838
	private Vector3 _Origin_Angle = Vector3.zero;

	// Token: 0x04000347 RID: 839
	private Vector3 _Target_Point = Vector3.zero;

	// Token: 0x04000348 RID: 840
	private Dictionary<int, float> _Move_Speed;

	// Token: 0x04000349 RID: 841
	private Dictionary<int, ArrayList> _Node_Data;

	// Token: 0x0400034A RID: 842
	private Dictionary<int, ArrayList> _Pass_Node_Data;

	// Token: 0x0400034B RID: 843
	private float _Detection_Range_1;

	// Token: 0x0400034C RID: 844
	private float _Detection_Range_2;

	// Token: 0x0400034D RID: 845
	private int _Movement_State;
}
