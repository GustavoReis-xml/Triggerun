using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class Chariot_Node_Trigger : MonoBehaviour
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x00021B80 File Offset: 0x0001FD80
	// (set) Token: 0x0600044A RID: 1098 RVA: 0x00021B98 File Offset: 0x0001FD98
	public Chariot_Controller Controller_Component
	{
		get
		{
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00021BA4 File Offset: 0x0001FDA4
	private void OnTriggerStay(Collider _Collider)
	{
		bool flag = _Collider.gameObject.CompareTag("Chariot_Node");
		if (flag)
		{
			string[] array = _Collider.gameObject.name.Split(new char[]
			{
				'_'
			});
			int num = int.Parse(array[1].ToString());
			bool flag2 = this._Current_Node == num;
			if (flag2)
			{
				return;
			}
			this._Current_Node = num;
			this.Controller_Component.Arrive_Destination(array[0].ToString(), num);
		}
		bool flag3 = _Collider.gameObject.CompareTag("Chariot_Pass_Node");
		if (flag3)
		{
			string[] array = _Collider.gameObject.name.Split(new char[]
			{
				'_'
			});
			int num = int.Parse(array[2].ToString());
			bool flag4 = this._Current_Pass_Node != num;
			if (flag4)
			{
				this._Current_Pass_Node = num;
				this.Controller_Component.Arrive_Destination(array[0].ToString() + "_" + array[1].ToString(), int.Parse(array[2].ToString()));
			}
		}
	}

	// Token: 0x04000353 RID: 851
	private Chariot_Controller _Controller_Component;

	// Token: 0x04000354 RID: 852
	private int _Current_Node = -1;

	// Token: 0x04000355 RID: 853
	private int _Current_Pass_Node = -1;
}
