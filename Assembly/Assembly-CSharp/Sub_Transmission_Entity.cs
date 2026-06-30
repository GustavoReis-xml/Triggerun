using System;

// Token: 0x020003EB RID: 1003
public class Sub_Transmission_Entity : Npc_Entity
{
	// Token: 0x06002A83 RID: 10883 RVA: 0x00177304 File Offset: 0x00175504
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Sub_Transmission_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Sub_Transmission_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Sub_Transmission_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Sub_Transmission_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Sub_Transmission_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Sub_Transmission_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Sub_Transmission_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Sub_Transmission_Model>();
			}
		}
		((Sub_Transmission_UI)base.UI_Component).View_Component = (base.View_Component as Sub_Transmission_View);
		((Sub_Transmission_View)base.View_Component).Controller_Component = (base.Controller_Component as Sub_Transmission_Controller);
		((Sub_Transmission_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Sub_Transmission_Model);
		((Sub_Transmission_Model)base.Model_Component).Entity_Component = this;
	}
}
