using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class Elevator_Controller : Npc_Controller
{
	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0005BA54 File Offset: 0x00059C54
	// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0005BA6C File Offset: 0x00059C6C
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

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0005BA78 File Offset: 0x00059C78
	// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0005BA90 File Offset: 0x00059C90
	public int Order_Value
	{
		get
		{
			return this._Order_Value;
		}
		set
		{
			this._Order_Value = value;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0005BA9C File Offset: 0x00059C9C
	// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0005BAB4 File Offset: 0x00059CB4
	public int Current_Number
	{
		get
		{
			return this._Current_Number;
		}
		set
		{
			this._Current_Number = value;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0005BAC0 File Offset: 0x00059CC0
	// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0005BAD8 File Offset: 0x00059CD8
	public bool Arrive_State
	{
		get
		{
			return this._Arrive_State;
		}
		set
		{
			this._Arrive_State = value;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0005BAE4 File Offset: 0x00059CE4
	// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0005BB1F File Offset: 0x00059D1F
	public Elevator_Model Elevator_Model_Component
	{
		get
		{
			bool flag = this._Elevator_Model_Component == null;
			if (flag)
			{
				this._Elevator_Model_Component = (Elevator_Model)base.Model_Component;
			}
			return this._Elevator_Model_Component;
		}
		set
		{
			this._Elevator_Model_Component = value;
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0005BB2C File Offset: 0x00059D2C
	public override void Update()
	{
		bool flag = fGameManager.Heroplayer == null || fGameManager.Heroplayer.delayNet || base.Entity_Component.UserControl == null;
		if (!flag)
		{
			bool main = base.Entity_Component.Main;
			if (main)
			{
				this.Speed = 1f;
			}
			else
			{
				this.Speed = 999f;
			}
			base.Rigidbody_Component.MovePosition(Vector3.SmoothDamp(base.Entity_Component.UserControl.transform.localPosition, base.Entity_Component.Position, ref this._Velocity, Time.smoothDeltaTime, this.Speed));
			base.Rigidbody_Component.MoveRotation(Quaternion.RotateTowards(base.Entity_Component.UserControl.transform.localRotation, Quaternion.Euler(base.Entity_Component.Rotation), 1f * Time.time));
			bool flag2 = base.Entity_Component.Main && Time.time >= this._Update_Synchronization_Time;
			if (flag2)
			{
				this._Update_Synchronization_Time = Time.time + 0.033f;
				base.Synchronization();
				bool flag3 = !this.Arrive_State && Vector3.Distance(base.Entity_Component.UserControl.transform.localPosition, base.Entity_Component.Position) <= 0.01f;
				if (flag3)
				{
					this.Arrive_Destination();
				}
			}
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0005BCA0 File Offset: 0x00059EA0
	public void Arrive_Destination()
	{
		this.Arrive_State = true;
		float current_Node_Wait_Time = this.Elevator_Model_Component.Get_Current_Node_Wait_Time();
		base.StartCoroutine(this.Wait_Move(current_Node_Wait_Time));
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0005BCD6 File Offset: 0x00059ED6
	private IEnumerator Wait_Move(float _Time)
	{
		yield return new WaitForSeconds(_Time);
		this.Elevator_Model_Component.Modify_Current_Node_Number(this.Elevator_Model_Component.Current_Node_Number + this.Order_Value);
		bool flag = this.Elevator_Model_Component.Current_Node_Number == 1;
		if (flag)
		{
			this.Order_Value = 1;
		}
		else
		{
			bool flag2 = this.Elevator_Model_Component.Current_Node_Number >= this.Elevator_Model_Component.Node_Data.Count;
			if (flag2)
			{
				this.Order_Value = -1;
			}
		}
		bool flag3 = this.Order_Value > 0;
		if (flag3)
		{
			base.Entity_Component.Position = this.Elevator_Model_Component.Get_Next_Node_Position();
		}
		else
		{
			base.Entity_Component.Position = this.Elevator_Model_Component.Get_Previous_Node_Position();
		}
		yield return new WaitForSeconds(5f);
		this.Arrive_State = false;
		yield break;
	}

	// Token: 0x0400081F RID: 2079
	private float _Speed = 1f;

	// Token: 0x04000820 RID: 2080
	private int _Order_Value = 1;

	// Token: 0x04000821 RID: 2081
	private int _Current_Number = 1;

	// Token: 0x04000822 RID: 2082
	private bool _Arrive_State;

	// Token: 0x04000823 RID: 2083
	private Elevator_Model _Elevator_Model_Component;

	// Token: 0x04000824 RID: 2084
	private Vector3 _Velocity;
}
