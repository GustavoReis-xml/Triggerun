using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class Elevator_Entity : Npc_Entity
{
	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0005BD10 File Offset: 0x00059F10
	// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0005BD28 File Offset: 0x00059F28
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

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0005BD34 File Offset: 0x00059F34
	// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0005BD4C File Offset: 0x00059F4C
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

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0005BD58 File Offset: 0x00059F58
	// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0005BD70 File Offset: 0x00059F70
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

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0005BD7C File Offset: 0x00059F7C
	// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0005BD94 File Offset: 0x00059F94
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

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0005BDA0 File Offset: 0x00059FA0
	// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0005BDB8 File Offset: 0x00059FB8
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

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0005BDC4 File Offset: 0x00059FC4
	// (set) Token: 0x06000A1C RID: 2588 RVA: 0x0005BDDC File Offset: 0x00059FDC
	public float Speed
	{
		get
		{
			return this._Speed;
		}
		set
		{
			this._Speed = value;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0005BDE8 File Offset: 0x00059FE8
	// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0005BE1A File Offset: 0x0005A01A
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

	// Token: 0x06000A1F RID: 2591 RVA: 0x0005BE24 File Offset: 0x0005A024
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Elevator_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Elevator_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Elevator_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Elevator_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Elevator_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Elevator_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Elevator_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Elevator_Model>();
			}
		}
		Elevator_Entity elevator_Entity = fGameManager.mloadConfig._Elevator_Xml_Data[base.Index];
		this.Npc_ID = base.Index;
		this.Map_ID = elevator_Entity.Map_ID;
		this.Origin_Point = elevator_Entity.Origin_Point;
		this.Origin_Angle = elevator_Entity.Origin_Angle;
		this.Target_Point = elevator_Entity.Target_Point;
		this.Speed = elevator_Entity.Speed;
		this.Node_Data = elevator_Entity.Node_Data;
		((Elevator_UI)base.UI_Component).View_Component = (base.View_Component as Elevator_View);
		((Elevator_View)base.View_Component).Controller_Component = (base.Controller_Component as Elevator_Controller);
		((Elevator_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Elevator_Model);
		((Elevator_Controller)base.Controller_Component).Speed = this.Speed;
		((Elevator_Model)base.Model_Component).Entity_Component = this;
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0005C053 File Offset: 0x0005A253
	public override void Load_Model_Resouce_Complete()
	{
		base.Load_Model_Resouce_Complete();
	}

	// Token: 0x04000825 RID: 2085
	private int _Map_ID;

	// Token: 0x04000826 RID: 2086
	private int _Npc_ID;

	// Token: 0x04000827 RID: 2087
	private Vector3 _Origin_Point = Vector3.zero;

	// Token: 0x04000828 RID: 2088
	private Vector3 _Origin_Angle = Vector3.zero;

	// Token: 0x04000829 RID: 2089
	private Vector3 _Target_Point = Vector3.zero;

	// Token: 0x0400082A RID: 2090
	private float _Speed;

	// Token: 0x0400082B RID: 2091
	private Dictionary<int, ArrayList> _Node_Data;
}
