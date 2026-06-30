using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class SkillSprintMe : SkillBaseMe
{
	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x0600284B RID: 10315 RVA: 0x0016A5FC File Offset: 0x001687FC
	// (set) Token: 0x0600284C RID: 10316 RVA: 0x0016A614 File Offset: 0x00168814
	public bool Sprint_State
	{
		get
		{
			return this._Sprint_State;
		}
		set
		{
			this._Sprint_State = value;
		}
	}

	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x0600284D RID: 10317 RVA: 0x0016A620 File Offset: 0x00168820
	// (set) Token: 0x0600284E RID: 10318 RVA: 0x0016A67E File Offset: 0x0016887E
	public GameObject Sprint_Sound_Object
	{
		get
		{
			bool flag = this._Sprint_Sound_Object == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.hero.thisModel, "Terminator_Sprint_Sound");
				bool flag2 = transform == null;
				if (flag2)
				{
					return null;
				}
				this._Sprint_Sound_Object = transform.gameObject;
			}
			return this._Sprint_Sound_Object;
		}
		set
		{
			this._Sprint_Sound_Object = value;
		}
	}

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x0600284F RID: 10319 RVA: 0x0016A688 File Offset: 0x00168888
	// (set) Token: 0x06002850 RID: 10320 RVA: 0x0016A6C6 File Offset: 0x001688C6
	public AudioSource Sprint_Audio_Source
	{
		get
		{
			bool flag = this.Sprint_Sound_Object == null;
			AudioSource result;
			if (flag)
			{
				result = null;
			}
			else
			{
				this._Sprint_Audio_Source = this.Sprint_Sound_Object.GetComponent<AudioSource>();
				result = this._Sprint_Audio_Source;
			}
			return result;
		}
		set
		{
			this._Sprint_Audio_Source = value;
		}
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x0016A6D0 File Offset: 0x001688D0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.Sprint_State = true;
		bool flag = base.hero.tCharAnimationFps != null && base.hero.tCharAnimationTps != null;
		if (flag)
		{
			base.hero.SetAnimator("Sprint", 0, this.Sprint_State, 2);
		}
		base.hero.tChar.jumpPowerForward = 10f;
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x0016A768 File Offset: 0x00168968
	public override void OneSkillFinish()
	{
		bool sprint_State = this.Sprint_State;
		if (sprint_State)
		{
			this.Sprint_State = false;
			bool flag = base.hero.tCharAnimationFps != null && base.hero.tCharAnimationTps != null;
			if (flag)
			{
				base.hero.SetAnimator("Sprint", 0, this.Sprint_State, 2);
			}
			base.hero.tChar.jumpPowerForward = 5f;
			base.OneSkillFinish();
			bool flag2 = (CWeaponControlMe)base.hero.wc != null;
			if (flag2)
			{
				((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
			}
		}
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x0016A828 File Offset: 0x00168A28
	public override bool OneSkillCanDo()
	{
		bool flag = Input_Manager.Get_Axis("Vertical") <= 0.5f;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool superDoing = ((HeroTerminatorMe)base.hero.heroBase).superDoing;
			if (superDoing)
			{
				result = false;
			}
			else
			{
				bool sprint_State = this.Sprint_State;
				if (sprint_State)
				{
					this.OneSkillFinish();
					result = false;
				}
				else
				{
					bool notInput = base.hero.wInput.notInput;
					result = (!notInput && base.OneSkillCanDo());
				}
			}
		}
		return result;
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x0016A8AC File Offset: 0x00168AAC
	public bool Can_Reload()
	{
		bool sprint_State = this.Sprint_State;
		return !sprint_State;
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x0016A8D0 File Offset: 0x00168AD0
	private void Update()
	{
		bool flag = Input_Manager.Get_Axis("Vertical") <= 0.5f && this.Sprint_State;
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x0400230A RID: 8970
	private bool _Sprint_State;

	// Token: 0x0400230B RID: 8971
	private GameObject _Sprint_Sound_Object;

	// Token: 0x0400230C RID: 8972
	private AudioSource _Sprint_Audio_Source;
}
