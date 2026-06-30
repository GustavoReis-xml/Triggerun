using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class Npc_Trigger : MonoBehaviour
{
	// Token: 0x06001CEF RID: 7407 RVA: 0x0010AE04 File Offset: 0x00109004
	private void OnTriggerEnter(Collider _Collider)
	{
		bool flag = _Collider.gameObject.CompareTag("Elevator") || _Collider.gameObject.layer == 0;
		if (flag)
		{
			Npc_Data component = base.gameObject.GetComponent<Npc_Data>();
			bool flag2 = component != null && component.Entity_Component.Type == 1;
			if (flag2)
			{
				component.Entity_Component.Suicide();
			}
		}
	}
}
