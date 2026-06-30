using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class Slay : MonoBehaviour
{
	// Token: 0x06002A46 RID: 10822 RVA: 0x001759BC File Offset: 0x00173BBC
	private void OnTriggerEnter(Collider _Collider)
	{
		bool flag = _Collider.gameObject.layer == 27;
		if (!flag)
		{
			Npc_Data component = _Collider.gameObject.GetComponent<Npc_Data>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Entity_Component.Suicide();
			}
			else
			{
				PublicInfo component2 = _Collider.transform.root.GetComponent<PublicInfo>();
				bool flag3 = !(component2 == null) && component2.NetObject != null;
				if (flag3)
				{
					NetObj netObject = component2.NetObject;
					int netid = netObject.getNetid();
					HeroPlayer player = fGameManager.GetInstance().getPlayer();
					bool flag4 = !player.isDead && netid == player.getNetid();
					if (flag4)
					{
						player.Hero_Suicide_State = true;
						player.SendMessageToServer(1000000, new ArrayList());
					}
				}
			}
		}
	}
}
