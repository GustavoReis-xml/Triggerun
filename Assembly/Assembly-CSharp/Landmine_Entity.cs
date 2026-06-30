using System;

// Token: 0x02000210 RID: 528
public class Landmine_Entity : Npc_Entity
{
	// Token: 0x06001712 RID: 5906 RVA: 0x000D6908 File Offset: 0x000D4B08
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Landmine_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Landmine_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Landmine_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Landmine_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Landmine_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Landmine_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Landmine_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Landmine_Model>();
			}
		}
		((Landmine_UI)base.UI_Component).View_Component = (base.View_Component as Landmine_View);
		((Landmine_View)base.View_Component).Controller_Component = (base.Controller_Component as Landmine_Controller);
		((Landmine_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Landmine_Model);
		((Landmine_Model)base.Model_Component).Entity_Component = this;
	}
}
