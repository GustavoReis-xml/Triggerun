using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class Steerable_Entity : Npc_Entity
{
	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x06002A6D RID: 10861 RVA: 0x00176524 File Offset: 0x00174724
	// (set) Token: 0x06002A6E RID: 10862 RVA: 0x0017653C File Offset: 0x0017473C
	public Component Custom_Steerable_Component
	{
		get
		{
			return this._Custom_Steerable_Component;
		}
		set
		{
			this._Custom_Steerable_Component = value;
		}
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x00176548 File Offset: 0x00174748
	public override void Initialize_Component()
	{
		base.Initialize_Component();
		bool flag = base.UI_Component == null;
		if (flag)
		{
			base.UI_Component = base.Container.GetComponent<Steerable_UI>();
			bool flag2 = base.UI_Component == null;
			if (flag2)
			{
				base.UI_Component = base.Container.AddComponent<Steerable_UI>();
			}
		}
		bool flag3 = base.View_Component == null;
		if (flag3)
		{
			base.View_Component = base.Container.GetComponent<Steerable_View>();
			bool flag4 = base.View_Component == null;
			if (flag4)
			{
				base.View_Component = base.Container.AddComponent<Steerable_View>();
			}
		}
		bool flag5 = base.Controller_Component == null;
		if (flag5)
		{
			base.Controller_Component = base.Container.GetComponent<Steerable_Controller>();
			bool flag6 = base.Controller_Component == null;
			if (flag6)
			{
				base.Controller_Component = base.Container.AddComponent<Steerable_Controller>();
			}
		}
		bool flag7 = base.Model_Component == null;
		if (flag7)
		{
			base.Model_Component = base.Container.GetComponent<Steerable_Model>();
			bool flag8 = base.Model_Component == null;
			if (flag8)
			{
				base.Model_Component = base.Container.AddComponent<Steerable_Model>();
			}
		}
		((Steerable_UI)base.UI_Component).View_Component = (base.View_Component as Steerable_View);
		((Steerable_View)base.View_Component).Controller_Component = (base.Controller_Component as Steerable_Controller);
		((Steerable_Controller)base.Controller_Component).Model_Component = (base.Model_Component as Steerable_Model);
		((Steerable_Model)base.Model_Component).Entity_Component = this;
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x001766EC File Offset: 0x001748EC
	public override void Load_Model_Resouce_Complete()
	{
		bool main = base.Main;
		if (main)
		{
			this.charMove = this.UserControl.AddComponent<TBUserControlNpc>();
			this.charMove.InitControl(this);
			((TBUserControlNpc)this.charMove).beAutoForward = true;
			((TBUserControlNpc)this.charMove).autoForward = new Vector3(0f, 0f, 1f);
			CapsuleCollider capsuleCollider = this.UserControl.AddComponent<CapsuleCollider>();
			capsuleCollider.radius = 0.5f;
			capsuleCollider.height = 0.75f;
			Vector3 center = capsuleCollider.center;
			center.y = 0.375f;
			capsuleCollider.center = center;
			this.tChar = this.UserControl.AddComponent<TBCharacterThirdMe>();
			this.tChar.wallRunRotationSpeed = 5f;
			this.tChar.wallRunCan = true;
			this.tChar.jumpPowerForward = 12f;
			bool flag = !this.isDead;
			if (flag)
			{
				User_Data_Handle.CanWatchTeamates = false;
			}
			CameraControl.Instance.InitCameraHero(this);
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.OnCheckHeadHpData(base.Synchronization_Object, this);
			}
		}
		else
		{
			CapsuleCollider capsuleCollider2 = this.UserControl.AddComponent<CapsuleCollider>();
			capsuleCollider2.radius = 0.5f;
			capsuleCollider2.height = 0.75f;
			Vector3 center2 = capsuleCollider2.center;
			center2.y = 0.375f;
			capsuleCollider2.center = center2;
			this.charMove = this.UserControl.AddComponent<TBUserControlNpcIt>();
			this.charMove.InitControl(this);
			this.tChar = this.UserControl.AddComponent<TBCharacterThirdIt>();
		}
		base.Load_Model_Resouce_Complete();
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x001768A0 File Offset: 0x00174AA0
	public override void Suicide()
	{
		bool flag = base.Main && !base.Death_State;
		if (flag)
		{
			((CWeaponControlMe)this.wc).WeaponFire(1, "0", this.UserControl.transform.position, this.UserControl);
			base.Suicide();
		}
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x001768FC File Offset: 0x00174AFC
	public override void OnDeathNpc(int _attackId, int o, ArrayList forcev3, int forcemax, int _State, ArrayList m_szCanYuXiaoMie, int nDeadType)
	{
		bool main = base.Main;
		if (main)
		{
			bool flag = gameuiCon.inst != null;
			if (flag)
			{
				gameuiCon.inst.OnCheckHeadHpData(base.Synchronization_Object, null);
			}
			base.Synchronization_Object.heroBase.SuperSkillFinish();
		}
		base.OnDeathNpc(_attackId, 0, new ArrayList(), 0, _State, m_szCanYuXiaoMie, nDeadType);
	}

	// Token: 0x04002416 RID: 9238
	private Component _Custom_Steerable_Component;
}
