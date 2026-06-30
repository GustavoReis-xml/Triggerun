using System;

// Token: 0x0200000C RID: 12
public class Area_Entity : Npc_Entity
{
	// Token: 0x06000035 RID: 53 RVA: 0x00003DE4 File Offset: 0x00001FE4
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Area_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Area_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Area_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Area_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Area_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Area_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Area_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Area_Model>();
			}
		}
		((Area_UI)base.UI_Component).View_Component = (base.View_Component as Area_View);
		((Area_View)base.View_Component).Controller_Component = (base.Controller_Component as Area_Controller);
		((Area_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Area_Model);
		((Area_Model)base.Model_Component).Entity_Component = this;
	}
}
