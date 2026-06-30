using System;
using TeamUtility.IO;

// Token: 0x020003E4 RID: 996
public class Steerable_Controller : Npc_Controller
{
	// Token: 0x06002A6B RID: 10859 RVA: 0x001764A0 File Offset: 0x001746A0
	public override void Update()
	{
		bool flag = base.Entity_Component.Main && !(base.Entity_Component.UserControl == null) && fGameManager.Heroplayer.canControl && !base.Entity_Component.Death_State;
		if (flag)
		{
			bool flag2 = InputManager.GetMouseButtonDown(0) || InputManager.GetMouseButtonDown(1);
			if (flag2)
			{
				base.Entity_Component.Suicide();
			}
			base.Update();
		}
	}
}
