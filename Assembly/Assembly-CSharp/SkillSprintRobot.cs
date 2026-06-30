using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class SkillSprintRobot : SkillBaseRobot
{
	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x06002857 RID: 10327 RVA: 0x0016A910 File Offset: 0x00168B10
	// (set) Token: 0x06002858 RID: 10328 RVA: 0x0016A928 File Offset: 0x00168B28
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

	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x06002859 RID: 10329 RVA: 0x0016A934 File Offset: 0x00168B34
	// (set) Token: 0x0600285A RID: 10330 RVA: 0x0016A992 File Offset: 0x00168B92
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

	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x0600285B RID: 10331 RVA: 0x0016A99C File Offset: 0x00168B9C
	// (set) Token: 0x0600285C RID: 10332 RVA: 0x0016A9DA File Offset: 0x00168BDA
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

	// Token: 0x0600285D RID: 10333 RVA: 0x0016A9E4 File Offset: 0x00168BE4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.Sprint_State = true;
		bool flag = base.hero.tCharAnimationFps != null && base.hero.tCharAnimationTps != null && base.hero.needSendToServer;
		if (flag)
		{
			base.hero.SetAnimator("Sprint", 0, this.Sprint_State, 1);
		}
		base.hero.tChar.jumpPowerForward = 10f;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x0016AA70 File Offset: 0x00168C70
	public override void OneSkillFinish()
	{
		bool sprint_State = this.Sprint_State;
		if (sprint_State)
		{
			this.Sprint_State = false;
			bool flag = base.hero.tCharAnimationTps != null && base.hero.needSendToServer;
			if (flag)
			{
				base.hero.SetAnimator("Sprint", 0, this.Sprint_State, 1);
			}
			base.hero.tChar.jumpPowerForward = 5f;
			base.OneSkillFinish();
		}
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x0016AAF4 File Offset: 0x00168CF4
	public override bool OneSkillCanDo()
	{
		return base.OneSkillCanDo();
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x0016AB0C File Offset: 0x00168D0C
	public bool Can_Reload()
	{
		bool sprint_State = this.Sprint_State;
		return !sprint_State;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x0016AB30 File Offset: 0x00168D30
	private void Update()
	{
		bool flag = this.Sprint_State && (((RobotAI)base.hero).robotController.attack || ((RobotAI)base.hero).robotController.currentStayTimer > 0f || ((RobotAI)base.hero).robotController.cancelSprint);
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x0400230D RID: 8973
	private bool _Sprint_State;

	// Token: 0x0400230E RID: 8974
	private GameObject _Sprint_Sound_Object;

	// Token: 0x0400230F RID: 8975
	private AudioSource _Sprint_Audio_Source;
}
