using System;

// Token: 0x0200000B RID: 11
public class Area_Controller : Sentry_Controller
{
	// Token: 0x06000033 RID: 51 RVA: 0x00003D44 File Offset: 0x00001F44
	public override void Detection()
	{
		bool flag = base.Entity_Component.Main && !(base.Entity_Component.Detection_Controller_Component == null) && !base.Entity_Component.Death_State && !(base.Entity_Component.UserControl == null);
		if (flag)
		{
			((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", base.Entity_Component.UserControl.transform.position, base.Entity_Component.UserControl);
		}
	}
}
