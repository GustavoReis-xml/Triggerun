using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class Sentry_Entity : Npc_Entity
{
	// Token: 0x06002304 RID: 8964 RVA: 0x00143134 File Offset: 0x00141334
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Sentry_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Sentry_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Sentry_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Sentry_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Sentry_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Sentry_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Sentry_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Sentry_Model>();
			}
		}
		((Sentry_UI)base.UI_Component).View_Component = (base.View_Component as Sentry_View);
		((Sentry_View)base.View_Component).Controller_Component = (base.Controller_Component as Sentry_Controller);
		((Sentry_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Sentry_Model);
		((Sentry_Model)base.Model_Component).Entity_Component = this;
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x001432D8 File Offset: 0x001414D8
	public override void Load_Model_Resouce_Complete()
	{
		((Sentry_Controller)base.Controller_Component).Initialize_Emitter_Object();
		CapsuleCollider capsuleCollider = this.UserControl.AddComponent<CapsuleCollider>();
		capsuleCollider.radius = 0.4f;
		capsuleCollider.height = 1.8f;
		base.Load_Model_Resouce_Complete();
		Vector3 center = capsuleCollider.center;
		center.y = 0.9f;
		capsuleCollider.center = center;
		this.defaultCollider = capsuleCollider;
	}
}
