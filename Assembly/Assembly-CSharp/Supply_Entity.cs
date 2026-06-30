using System;

// Token: 0x020003F0 RID: 1008
public class Supply_Entity : Npc_Entity
{
	// Token: 0x06002A8B RID: 10891 RVA: 0x001776DC File Offset: 0x001758DC
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Supply_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Supply_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Supply_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Supply_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Supply_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Supply_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Supply_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Supply_Model>();
			}
		}
		((Supply_UI)base.UI_Component).View_Component = (base.View_Component as Supply_View);
		((Supply_View)base.View_Component).Controller_Component = (base.Controller_Component as Supply_Controller);
		((Supply_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Supply_Model);
		((Supply_Model)base.Model_Component).Entity_Component = this;
	}
}
