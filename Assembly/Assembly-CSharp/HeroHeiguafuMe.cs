using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class HeroHeiguafuMe : HeroBaseMe
{
	// Token: 0x060011DC RID: 4572 RVA: 0x000A7970 File Offset: 0x000A5B70
	public void XingTaiChange()
	{
		bool flag = !this.open && base.hero.tChar.onGround;
		if (flag)
		{
			this.open = true;
			base.hero.wc.UpdateFireTime(0.5f);
			base.hero.ChangeWeaponGroup(1);
			base.hero.SetAnimator("Sniper", 0, true, 1);
			base.hero.playerRuntimeData.CameraZoom = true;
			this.changeHongXian(0.0005f);
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x000A7A00 File Offset: 0x000A5C00
	public void changeHongXian(float size)
	{
		foreach (NetObj netObj in GameRoot.SceneSocket.unFsorket.NetObjTable.Values)
		{
			NetLiving netLiving = (NetLiving)netObj;
			bool flag = netLiving != null && netLiving.Stroke_Material_Array != null;
			if (flag)
			{
				for (int i = 0; i < netLiving.Stroke_Material_Array.Count; i++)
				{
					Material material = (Material)netLiving.Stroke_Material_Array[i];
					material.SetFloat("_Outline", size);
				}
			}
		}
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x000A7AC0 File Offset: 0x000A5CC0
	public void XingTaiBack(bool isbreak)
	{
		bool flag = this.open;
		if (flag)
		{
			this.open = false;
			base.hero.wc.UpdateFireTime(0.3f);
			base.hero.ChangeWeaponGroup(0);
			bool flag2 = !isbreak;
			if (flag2)
			{
				base.hero.SetAnimator("Sniper", 0, false, 1);
			}
			base.hero.playerRuntimeData.CameraZoom = false;
			this.changeHongXian(0.002f);
			((CWeaponControlMe)base.hero.wc).StopFulling();
		}
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000A7B5E File Offset: 0x000A5D5E
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillHongwai", 0, true, 1);
		((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000A7B98 File Offset: 0x000A5D98
	public override void BreakSth(int type)
	{
		base.BreakSth(type);
		this.XingTaiBack(true);
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000A7BAC File Offset: 0x000A5DAC
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck();
		if (flag)
		{
			bool flag2 = this.open;
			if (flag2)
			{
				bool isDead = base.hero.isDead;
				if (isDead)
				{
					this.XingTaiBack(true);
					return;
				}
				int value = (int)Input_Manager.Get_Axis("Jump");
				bool flag3 = Convert.ToBoolean(value);
				if (flag3)
				{
					this.XingTaiBack(false);
					return;
				}
				bool flag4 = Input_Manager.Get_Button_Down("Reload");
				if (flag4)
				{
					this.XingTaiBack(false);
					return;
				}
			}
			bool flag5 = base.hero.wInput.notInput || base.GetStat("rope") > 0 || base.GetStat("guilei") > 0;
			if (flag5)
			{
				return;
			}
			bool flag6 = Input_Manager.Get_Button("WeaponSec");
			if (flag6)
			{
				this.XingTaiChange();
			}
			bool flag7 = Input_Manager.Get_Button_Down("superskill");
			if (flag7)
			{
				base.hero.superSkill(null);
			}
		}
		bool flag8 = Input_Manager.Get_Button_Up("WeaponSec");
		if (flag8)
		{
			this.XingTaiBack(false);
		}
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000A7CC9 File Offset: 0x000A5EC9
	public override void alphaEnd()
	{
		base.hero.SetAnimator("SkillHongwai", 0, false, 1);
		this.SuperSkillFinish();
	}

	// Token: 0x04000E2A RID: 3626
	private bool open;
}
