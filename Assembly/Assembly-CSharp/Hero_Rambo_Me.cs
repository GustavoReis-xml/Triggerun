using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class Hero_Rambo_Me : HeroBaseMe
{
	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06001586 RID: 5510 RVA: 0x000C9A34 File Offset: 0x000C7C34
	// (set) Token: 0x06001587 RID: 5511 RVA: 0x000C9A4C File Offset: 0x000C7C4C
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

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06001588 RID: 5512 RVA: 0x000C9A58 File Offset: 0x000C7C58
	// (set) Token: 0x06001589 RID: 5513 RVA: 0x000C9A70 File Offset: 0x000C7C70
	public bool Accurately_Aim_State
	{
		get
		{
			return this._Accurately_Aim_State;
		}
		set
		{
			this._Accurately_Aim_State = value;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x0600158A RID: 5514 RVA: 0x000C9A7C File Offset: 0x000C7C7C
	// (set) Token: 0x0600158B RID: 5515 RVA: 0x000C9AAE File Offset: 0x000C7CAE
	public Dictionary<string, AudioSource> Aim_Switch_Sound
	{
		get
		{
			bool flag = this._Aim_Switch_Sound == null;
			if (flag)
			{
				this._Aim_Switch_Sound = new Dictionary<string, AudioSource>();
			}
			return this._Aim_Switch_Sound;
		}
		set
		{
			this._Aim_Switch_Sound = value;
		}
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000C9AB8 File Offset: 0x000C7CB8
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "EnemyCenterOne");
		base.hero.screentarget.targetAngle = GlobalDefine.Rambo_Aim_Range;
		base.hero.screentarget.targetDis = (float)GlobalDefine.Rambo_Aim_Distance;
		List<string> list = new List<string>();
		list.Add("skill_lanbo_aim_on");
		list.Add("skill_lanbo_aim_off");
		for (int i = 0; i < list.Count; i++)
		{
			string url = "SoundRes/" + list[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.Load_Aim_Switch_Sound), list[i]);
			}
			else
			{
				this.Load_Aim_Switch_Sound(resWWW, i);
			}
		}
		this.HeroAim = false;
		this.currentFOV = 60f;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x000C9BAC File Offset: 0x000C7DAC
	private void Load_Aim_Switch_Sound(WWW www, object _Value)
	{
		bool flag = www != null && www.error == null && base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = base.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this.Aim_Switch_Sound.ContainsKey(_Value.ToString());
			if (flag2)
			{
				this.Aim_Switch_Sound.Add(_Value.ToString(), component);
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			}
		}
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000C9C8C File Offset: 0x000C7E8C
	public void Play_Aim_Switch_Sound(string _Sound_Name)
	{
		bool flag = this.Aim_Switch_Sound.ContainsKey(_Sound_Name);
		if (flag)
		{
			this.Aim_Switch_Sound[_Sound_Name].Play();
		}
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000C9CC0 File Offset: 0x000C7EC0
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.aimFpsSystem = base.hero.userModelFps.GetComponent<CAimingFps>();
		this.aimFpsSystem.InitAimSystem(base.hero, base.hero.playerTarget.transform);
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000C9D10 File Offset: 0x000C7F10
	protected override void Update()
	{
		base.Update();
		bool flag = this.HeroAim && this.currentFOV != 35f;
		if (flag)
		{
			this.currentFOV = Mathf.Lerp(this.currentFOV, (float)GlobalDefine.Rambo_Aim_FOV, Time.deltaTime * 10f);
			CameraControl.Instance.controlCamera.fieldOfView = this.currentFOV;
		}
		bool flag2 = !this.HeroAim && this.currentFOV != 60f;
		if (flag2)
		{
			this.currentFOV = Mathf.Lerp(this.currentFOV, 60f, Time.deltaTime * 10f);
			CameraControl.Instance.controlCamera.fieldOfView = this.currentFOV;
		}
		bool flag3 = base.hero.GetAnimatorInteger("HuDongState") > 0;
		if (flag3)
		{
			this.Accurately_Aim_Close();
		}
		else
		{
			bool isDead = base.hero.isDead;
			if (!isDead)
			{
				bool flag4 = Input_Manager.Get_Button("Esc") || Input_Manager.Get_Button("Key_Open_Chat_Window");
				if (flag4)
				{
					this.Accurately_Aim_Close();
				}
				bool flag5 = !this.InputCheck();
				if (!flag5)
				{
					bool flag6 = this.aimFpsSystem != null;
					if (flag6)
					{
						bool flag7 = this.Musth_State && this._hero.screentarget != null && this._hero.screentarget.NetTarget != null && this._hero.screentarget.NetTarget.defaultMesh != null;
						if (flag7)
						{
							this.aimFpsSystem.target = this._hero.screentarget.NetTarget.defaultMesh.transform;
						}
						else
						{
							bool flag8 = this._hero.playerTarget != null;
							if (flag8)
							{
								this.aimFpsSystem.target = this._hero.playerTarget.transform;
							}
						}
					}
					this.Keyboard_Control();
					bool flag9 = Input_Manager.Get_Button_Down("superskill");
					if (flag9)
					{
						base.hero.superSkill(null);
					}
				}
			}
		}
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x000C9F3C File Offset: 0x000C813C
	private void Keyboard_Control()
	{
		bool musth_State = this.Musth_State;
		if (!musth_State)
		{
			bool animatorBool = base.hero.GetAnimatorBool("Reload");
			if (animatorBool)
			{
				bool animatorBool2 = base.hero.GetAnimatorBool("Accurately_Aim");
				if (animatorBool2)
				{
					this.Accurately_Aim_Close();
				}
			}
			else
			{
				bool flag = Input_Manager.Get_Button_Down("WeaponSec");
				if (flag)
				{
					this.Accurately_Aim_State = base.hero.GetAnimatorBool("Accurately_Aim");
					bool flag2 = !this.Accurately_Aim_State;
					if (flag2)
					{
						this.Accurately_Aim_Open();
					}
					else
					{
						this.Accurately_Aim_Close();
					}
				}
				bool flag3 = Input_Manager.Get_Axis("Jump") > 0f && base.hero.tChar.onGround;
				if (flag3)
				{
					this.Accurately_Aim_Close();
				}
			}
		}
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000CA00C File Offset: 0x000C820C
	public void Accurately_Aim_Open()
	{
		base.hero.SetAnimator("Accurately_Aim_State", 2, 1, 1);
		bool flag = !this.HeroAim;
		if (flag)
		{
			this.Play_Aim_Switch_Sound("skill_lanbo_aim_on");
			CameraControl.Instance.CameraZoom = true;
			this.HeroAim = true;
			base.hero.ChangeWeaponGroup(1);
		}
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000CA070 File Offset: 0x000C8270
	public void Accurately_Aim_Close()
	{
		base.hero.SetAnimator("Accurately_Aim_State", 2, 0, 1);
		bool heroAim = this.HeroAim;
		if (heroAim)
		{
			this.Play_Aim_Switch_Sound("skill_lanbo_aim_off");
			this.HeroAim = false;
			CameraControl.Instance.CameraZoom = false;
			base.hero.ChangeWeaponGroup(0);
		}
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000CA0D0 File Offset: 0x000C82D0
	public override void SuperSkillDo(object parm)
	{
		this.Accurately_Aim_Close();
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.SuperSkillDo(parm);
		base.hero.wInput.notInput = true;
		base.hero.bePlayActive = false;
		this.Musth_State = true;
		base.StartCoroutine(this.Wait_Action());
		base.hero.ChangeWeaponGroup(2);
		base.StartCoroutine(this.Wait_Super_Skill_Finish());
		base.hero.SetAnimator("Reload_Speed", 1, 2f, 1);
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000CA16A File Offset: 0x000C836A
	private IEnumerator Wait_Action()
	{
		yield return new WaitForSeconds(0.5f);
		base.hero.wInput.notInput = false;
		yield break;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000CA179 File Offset: 0x000C8379
	private IEnumerator Wait_Super_Skill_Finish()
	{
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000CA188 File Offset: 0x000C8388
	public override void SuperSkillFinish()
	{
		bool musth_State = this.Musth_State;
		if (musth_State)
		{
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			base.hero.skillEffectDict[1143].OneSkillFinish();
			base.hero.SetAnimator("Skill_3_Effect", 0, false, 1);
			base.hero.SetAnimator("Skill_3_Aim", 0, false, 1);
			this.Musth_State = false;
			base.hero.wc.notFillCartridge = false;
			base.hero.ChangeWeaponGroup(0);
			base.hero.SetAnimator("Reload_Speed", 1, 1f, 1);
			base.SuperSkillFinish();
		}
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000CA24E File Offset: 0x000C844E
	public override void OnDeath()
	{
		base.OnDeath();
		this.Accurately_Aim_Close();
		this.SuperSkillFinish();
		base.hero.SetAnimator("Accurately_Aim_State", 2, 0, 1);
		base.hero.ChangeWeaponGroup(0);
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000CA28C File Offset: 0x000C848C
	public override void Revive()
	{
		base.Revive();
		base.hero.SetAnimator("Dead", 2, 0, 1);
	}

	// Token: 0x04001076 RID: 4214
	private bool _Musth_State;

	// Token: 0x04001077 RID: 4215
	private bool _Accurately_Aim_State;

	// Token: 0x04001078 RID: 4216
	private Dictionary<string, AudioSource> _Aim_Switch_Sound;

	// Token: 0x04001079 RID: 4217
	private CAimingFps aimFpsSystem;

	// Token: 0x0400107A RID: 4218
	private bool _On_Ground_State;

	// Token: 0x0400107B RID: 4219
	private bool HeroAim;

	// Token: 0x0400107C RID: 4220
	private float currentFOV = 60f;
}
