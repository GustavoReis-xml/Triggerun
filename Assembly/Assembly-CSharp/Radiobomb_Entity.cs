using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public class Radiobomb_Entity : Npc_Entity
{
	// Token: 0x06002069 RID: 8297 RVA: 0x0012952C File Offset: 0x0012772C
	public override void Initialize_Component()
	{
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Radiobomb_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Radiobomb_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Radiobomb_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Radiobomb_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Radiobomb_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Radiobomb_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Radiobomb_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Radiobomb_Model>();
			}
		}
		base.Initialize_Component();
		((Radiobomb_UI)base.UI_Component).View_Component = (base.View_Component as Radiobomb_View);
		((Radiobomb_View)base.View_Component).Controller_Component = (base.Controller_Component as Radiobomb_Controller);
		((Radiobomb_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Radiobomb_Model);
		((Radiobomb_Model)base.Model_Component).Entity_Component = this;
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x001296D0 File Offset: 0x001278D0
	public override void Suicide()
	{
		Debug.LogError("[DEBUG] Radiobomb_Entity.Suicide() called. Stack trace:\n" + Environment.StackTrace);
		bool flag = base.Main && fGameManager.Heroplayer != null && !fGameManager.Heroplayer.delayNet;
		if (flag)
		{
			bool flag2 = this.wc != null && this.UserControl != null;
			if (flag2)
			{
				((CWeaponControlMe)this.wc).WeaponFire(1, "0", this.UserControl.transform.position, this.UserControl);
			}
			else
			{
				Fun.Log(string.Concat(new object[]
				{
					"注意:遥控炸弹类NPC自杀时，wc = ",
					this.wc,
					" --- UserControl = ",
					this.UserControl
				}), null);
			}
			base.Suicide();
		}
	}
}
