using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class Hero_Rambo_It : HeroBaseIt
{
	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x0600157E RID: 5502 RVA: 0x000C95A0 File Offset: 0x000C77A0
	// (set) Token: 0x0600157F RID: 5503 RVA: 0x000C95B8 File Offset: 0x000C77B8
	public bool Musth_State
	{
		get
		{
			return this._Musth_State;
		}
		set
		{
			this._Musth_State = value;
		}
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000C95C4 File Offset: 0x000C77C4
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			CScreenTarget.InitScreenTarget(base.hero, "EnemyCenterOne");
			base.hero.screentarget.targetAngle = GlobalDefine.Rambo_Aim_Range;
			base.hero.screentarget.targetDis = (float)GlobalDefine.Rambo_Aim_Distance;
		}
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000C9668 File Offset: 0x000C7868
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool flag = (base.hero.isCopy && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			this.aimFpsSystem = base.hero.userModelFps.GetComponent<CAimingFps>();
			this.aimFpsSystem.InitAimSystem(base.hero, base.hero.playerTarget.transform);
		}
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000C96F8 File Offset: 0x000C78F8
	public override void SuperSkillDo(object parm)
	{
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		}
		this.Musth_State = true;
		base.SuperSkillDo(parm);
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000C9778 File Offset: 0x000C7978
	public override void SuperSkillFinish()
	{
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		}
		this.Musth_State = false;
		base.SuperSkillFinish();
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000C97F8 File Offset: 0x000C79F8
	protected override void Update()
	{
		base.Update();
		bool flag = !base.hero.playerHaveCreate || ((!base.hero.isCopy || base.hero.isDead || CFilmManager.Instance.filmParentNetId(base.hero) != base.hero.getNetid()) && !GameRoot.inst.IsGmTarget(base.hero));
		if (!flag)
		{
			this.HeroAim = base.hero.GetAnimatorBool("Accurately_Aim");
			CameraControl.Instance.CameraZoom = this.HeroAim;
			bool flag2 = this.HeroAim && this.currentFOV != 35f;
			if (flag2)
			{
				this.currentFOV = Mathf.Lerp(this.currentFOV, (float)GlobalDefine.Rambo_Aim_FOV, Time.deltaTime * 10f);
				CameraControl.Instance.controlCamera.fieldOfView = this.currentFOV;
			}
			bool flag3 = !this.HeroAim && this.currentFOV != 60f;
			if (flag3)
			{
				this.currentFOV = Mathf.Lerp(this.currentFOV, 60f, Time.deltaTime * 10f);
				CameraControl.Instance.controlCamera.fieldOfView = this.currentFOV;
			}
			bool flag4 = !(this.aimFpsSystem == null);
			if (flag4)
			{
				bool flag5 = this.Musth_State && this.aimFpsSystem != null && this._hero.screentarget != null && this._hero.screentarget.NetTarget != null && this._hero.screentarget.NetTarget.defaultMesh != null;
				if (flag5)
				{
					this.aimFpsSystem.target = this._hero.screentarget.NetTarget.defaultMesh.transform;
				}
				else
				{
					bool flag6 = this._hero.playerTarget != null;
					if (flag6)
					{
						this.aimFpsSystem.target = this._hero.playerTarget.transform;
					}
				}
			}
		}
	}

	// Token: 0x04001072 RID: 4210
	private bool _Musth_State;

	// Token: 0x04001073 RID: 4211
	private CAimingFps aimFpsSystem;

	// Token: 0x04001074 RID: 4212
	private bool HeroAim;

	// Token: 0x04001075 RID: 4213
	private float currentFOV = 60f;
}
