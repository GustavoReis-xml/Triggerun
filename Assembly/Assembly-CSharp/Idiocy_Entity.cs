using System;

// Token: 0x020001EB RID: 491
public class Idiocy_Entity : Npc_Entity
{
	// Token: 0x060015F4 RID: 5620 RVA: 0x000CD3A8 File Offset: 0x000CB5A8
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Idiocy_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Idiocy_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Idiocy_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Idiocy_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Idiocy_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Idiocy_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Idiocy_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Idiocy_Model>();
			}
		}
		((Idiocy_UI)base.UI_Component).View_Component = (base.View_Component as Idiocy_View);
		((Idiocy_View)base.View_Component).Controller_Component = (base.Controller_Component as Idiocy_Controller);
		((Idiocy_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Idiocy_Model);
		((Idiocy_Model)base.Model_Component).Entity_Component = this;
	}
}
