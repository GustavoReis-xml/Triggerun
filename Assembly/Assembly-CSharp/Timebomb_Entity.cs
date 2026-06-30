using System;

// Token: 0x02000476 RID: 1142
public class Timebomb_Entity : Npc_Entity
{
	// Token: 0x06002C70 RID: 11376 RVA: 0x00186D68 File Offset: 0x00184F68
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Timebomb_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Timebomb_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Timebomb_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Timebomb_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Timebomb_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Timebomb_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Timebomb_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Timebomb_Model>();
			}
		}
		((Timebomb_UI)base.UI_Component).View_Component = (base.View_Component as Timebomb_View);
		((Timebomb_View)base.View_Component).Controller_Component = (base.Controller_Component as Timebomb_Controller);
		((Timebomb_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Timebomb_Model);
		((Timebomb_Model)base.Model_Component).Entity_Component = this;
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x00186F0C File Offset: 0x0018510C
	public override void Suicide()
	{
		bool main = base.Main;
		if (main)
		{
			bool flag = this.wc != null && this.UserControl != null;
			if (flag)
			{
				((CWeaponControlMe)this.wc).WeaponFire(1, "0", this.UserControl.transform.position, this.UserControl);
			}
			else
			{
				Fun.Log(string.Concat(new object[]
				{
					"注意:定时炸弹类NPC自杀时，wc = ",
					this.wc,
					" --- UserControl = ",
					this.UserControl
				}), null);
			}
			base.Suicide();
		}
	}
}
