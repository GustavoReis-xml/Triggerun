using System;

// Token: 0x02000480 RID: 1152
public class Trap_Entity : Npc_Entity
{
	// Token: 0x06002CAF RID: 11439 RVA: 0x00189674 File Offset: 0x00187874
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Trap_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Trap_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Trap_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Trap_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Trap_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Trap_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Trap_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Trap_Model>();
			}
		}
		((Trap_UI)base.UI_Component).View_Component = (base.View_Component as Trap_View);
		((Trap_View)base.View_Component).Controller_Component = (base.Controller_Component as Trap_Controller);
		((Trap_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Trap_Model);
		((Trap_Model)base.Model_Component).Entity_Component = this;
	}
}
