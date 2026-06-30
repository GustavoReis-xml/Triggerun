using System;

// Token: 0x02000232 RID: 562
public class Main_Transmission_Entity : Npc_Entity
{
	// Token: 0x060019D8 RID: 6616 RVA: 0x000F4178 File Offset: 0x000F2378
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Main_Transmission_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Main_Transmission_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Main_Transmission_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Main_Transmission_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Main_Transmission_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Main_Transmission_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Main_Transmission_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Main_Transmission_Model>();
			}
		}
		((Main_Transmission_UI)base.UI_Component).View_Component = (base.View_Component as Main_Transmission_View);
		((Main_Transmission_View)base.View_Component).Controller_Component = (base.Controller_Component as Main_Transmission_Controller);
		((Main_Transmission_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Main_Transmission_Model);
		((Main_Transmission_Model)base.Model_Component).Entity_Component = this;
	}
}
