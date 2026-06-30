using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036F RID: 879
public class SkillShieldIt : SkillBaseIt
{
	// Token: 0x17000513 RID: 1299
	// (get) Token: 0x06002806 RID: 10246 RVA: 0x00168A34 File Offset: 0x00166C34
	// (set) Token: 0x06002807 RID: 10247 RVA: 0x00168A4C File Offset: 0x00166C4C
	public bool Shield_Call_State
	{
		get
		{
			return this._Shield_Call_State;
		}
		set
		{
			this._Shield_Call_State = value;
		}
	}

	// Token: 0x17000514 RID: 1300
	// (get) Token: 0x06002808 RID: 10248 RVA: 0x00168A58 File Offset: 0x00166C58
	// (set) Token: 0x06002809 RID: 10249 RVA: 0x00168A70 File Offset: 0x00166C70
	public int Shield_Current_Energy
	{
		get
		{
			return this._Shield_Current_Energy;
		}
		set
		{
			this._Shield_Current_Energy = value;
		}
	}

	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x0600280A RID: 10250 RVA: 0x00168A7C File Offset: 0x00166C7C
	// (set) Token: 0x0600280B RID: 10251 RVA: 0x00168A94 File Offset: 0x00166C94
	public int Shield_Max_Energy
	{
		get
		{
			return this._Shield_Max_Energy;
		}
		set
		{
			this._Shield_Max_Energy = value;
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x0600280C RID: 10252 RVA: 0x00168AA0 File Offset: 0x00166CA0
	// (set) Token: 0x0600280D RID: 10253 RVA: 0x00168AB8 File Offset: 0x00166CB8
	public float Shield_Call_Time
	{
		get
		{
			return this._Shield_Call_Time;
		}
		set
		{
			this._Shield_Call_Time = value;
		}
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x0600280E RID: 10254 RVA: 0x00168AC4 File Offset: 0x00166CC4
	// (set) Token: 0x0600280F RID: 10255 RVA: 0x00168ADC File Offset: 0x00166CDC
	public SkillShieldIt.State Shield_State
	{
		get
		{
			return this._Shield_State;
		}
		set
		{
			this._Shield_State = value;
		}
	}

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x06002810 RID: 10256 RVA: 0x00168AE8 File Offset: 0x00166CE8
	// (set) Token: 0x06002811 RID: 10257 RVA: 0x00168B00 File Offset: 0x00166D00
	public SkillShieldIt.Break_State Shield_Break_State
	{
		get
		{
			return this._Shield_Break_State;
		}
		set
		{
			this._Shield_Break_State = value;
		}
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x00168B0C File Offset: 0x00166D0C
	private void Update()
	{
		bool flag = base.hero == null || base.hero.tCharAnimationTps == null;
		if (!flag)
		{
			bool flag2 = this.PlayShieldSoundCD > 0f;
			if (flag2)
			{
				this.PlayShieldSoundCD -= Time.deltaTime;
			}
			else
			{
				this.PlayShieldOpenSound = true;
			}
			bool flag3 = !this.shieldCallOn;
			if (flag3)
			{
				bool @bool = base.hero.tCharAnimationTps.animator.GetBool("JvDun");
				if (@bool)
				{
					((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().isKinematic = false;
					((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().useGravity = false;
					((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<BoxCollider>().enabled = true;
					this.shieldCallOn = true;
					this.PlayShieldOpen();
				}
			}
			else
			{
				bool flag4 = this.shieldCallOn;
				if (flag4)
				{
					bool flag5 = !this.playAboutCrash && base.hero.tCharAnimationTps.animator.GetInteger("SheildState") == 5;
					if (flag5)
					{
						base.hero.playerSound.PlayShieldAboutCrashSound();
						this.playAboutCrash = true;
					}
					bool flag6 = !base.hero.tCharAnimationTps.animator.GetBool("JvDun");
					if (flag6)
					{
						((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().isKinematic = true;
						((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().useGravity = true;
						((HeroXingTianIt)base.hero.heroBase).Shield_Object.GetComponent<BoxCollider>().enabled = false;
						base.hero.Clear_Adhere_Npc("body2_dun");
						bool flag7 = this.canPlayShieldCloseSound;
						if (flag7)
						{
							base.hero.playerSound.PlayShieldContinueSound(1);
						}
						this.shieldCallOn = false;
						this.playAboutCrash = false;
						bool flag8 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
						if (flag8)
						{
							base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
						}
					}
				}
			}
			bool flag9 = Input_Manager.Get_Button_Up("WeaponSec");
			if (flag9)
			{
				this.OneSkillFinish();
			}
			this.State_Handle();
		}
	}

	// Token: 0x06002813 RID: 10259 RVA: 0x00168DC4 File Offset: 0x00166FC4
	private void PlayShieldOpen()
	{
		bool playShieldOpenSound = this.PlayShieldOpenSound;
		if (playShieldOpenSound)
		{
			this.PlayShieldSoundCD = 0f;
			this.PlayShieldOpenSound = false;
			this.canPlayShieldCloseSound = true;
			base.hero.playerSound.PlayShieldOpenSound();
		}
		base.StartCoroutine(this.PlayShieldOpenContinueSound());
		bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag)
		{
			base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
		}
	}

	// Token: 0x06002814 RID: 10260 RVA: 0x00168E7C File Offset: 0x0016707C
	private IEnumerator PlayShieldOpenContinueSound()
	{
		yield return new WaitForSeconds(1f);
		bool shield_Call_State = this.Shield_Call_State;
		if (shield_Call_State)
		{
			base.hero.playerSound.PlayShieldContinueSound(0);
		}
		yield break;
	}

	// Token: 0x06002815 RID: 10261 RVA: 0x00168E8C File Offset: 0x0016708C
	private void State_Handle()
	{
		bool flag = this.Shield_State == SkillShieldIt.State.Wait;
		if (flag)
		{
			this.State_Handle(-1);
		}
		else
		{
			bool flag2 = this.Shield_State == SkillShieldIt.State.CallOff;
			if (flag2)
			{
				bool shield_Call_State = this.Shield_Call_State;
				if (shield_Call_State)
				{
					this.State_Handle(1);
				}
			}
			else
			{
				bool flag3 = this.Shield_State == SkillShieldIt.State.CallOn && !this.Shield_Call_State;
				if (flag3)
				{
					this.State_Handle(0);
				}
			}
		}
	}

	// Token: 0x06002816 RID: 10262 RVA: 0x00168EFC File Offset: 0x001670FC
	private void State_Handle(int _Value)
	{
		switch (_Value)
		{
		case -1:
		{
			base.hero.SetAnimator("JvDun", 0, false, 1);
			((HeroXingTianIt)base.hero.heroBase).Shield_Object.SetActive(false);
			bool flag = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
			if (flag)
			{
				base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
			break;
		}
		case 0:
		{
			this.Shield_State = SkillShieldIt.State.CallOff;
			this.Shield_Break_State = SkillShieldIt.Break_State.State_1;
			base.hero.SetAnimator("JvDun", 0, false, 1);
			((HeroXingTianIt)base.hero.heroBase).Shield_Object.SetActive(false);
			bool flag2 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
			if (flag2)
			{
				base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
			break;
		}
		case 1:
		{
			bool flag3 = this.Shield_Current_Energy >= 1;
			if (flag3)
			{
				bool flag4 = this.Shield_Call_Time == 0f || Time.time >= this.Shield_Call_Time + 30f;
				if (flag4)
				{
					this.Shield_Call_Time = Time.time;
					PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1952];
					Fun.Load_Sound(base.hero, preloadData.sUrl, base.hero.thisModel, true, "Reinhardt_Shield_Sound");
				}
				this.Update_Shield_Data(null);
				this.Shield_State = SkillShieldIt.State.CallOn;
				base.hero.SetAnimator("JvDun", 0, true, 1);
				((HeroXingTianIt)base.hero.heroBase).Shield_Object.SetActive(true);
				bool flag5 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
				if (flag5)
				{
					base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
				}
			}
			break;
		}
		}
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x001691B4 File Offset: 0x001673B4
	public void Update_Shield_Data(ArrayList _Parameters)
	{
		bool flag = _Parameters != null;
		if (flag)
		{
			int num = int.Parse(_Parameters[0].ToString());
			this.Shield_Current_Energy = int.Parse(_Parameters[1].ToString());
			int num2 = num;
			int num3 = num2;
			if (num3 != -1)
			{
				if (num3 == 0)
				{
					this.Shield_State = SkillShieldIt.State.CallOff;
				}
			}
			else
			{
				this.Shield_State = SkillShieldIt.State.Wait;
			}
		}
		bool flag2 = this.Shield_Current_Energy >= 8005 && this.Shield_Current_Energy <= 10000;
		if (flag2)
		{
			bool flag3 = this.Shield_Break_State != SkillShieldIt.Break_State.State_1 && this.Shield_Call_State;
			if (flag3)
			{
				this.Shield_Break_State = SkillShieldIt.Break_State.State_1;
			}
		}
		else
		{
			bool flag4 = this.Shield_Current_Energy >= 6005 && this.Shield_Current_Energy <= 8004;
			if (flag4)
			{
				bool flag5 = this.Shield_Break_State != SkillShieldIt.Break_State.State_2 && this.Shield_Call_State;
				if (flag5)
				{
					this.Shield_Break_State = SkillShieldIt.Break_State.State_2;
				}
			}
			else
			{
				bool flag6 = this.Shield_Current_Energy >= 4005 && this.Shield_Current_Energy <= 6004;
				if (flag6)
				{
					bool flag7 = this.Shield_Break_State != SkillShieldIt.Break_State.State_3 && this.Shield_Call_State;
					if (flag7)
					{
						this.Shield_Break_State = SkillShieldIt.Break_State.State_3;
					}
				}
				else
				{
					bool flag8 = this.Shield_Current_Energy >= 2005 && this.Shield_Current_Energy <= 4004;
					if (flag8)
					{
						bool flag9 = this.Shield_Break_State != SkillShieldIt.Break_State.State_4 && this.Shield_Call_State;
						if (flag9)
						{
							this.Shield_Break_State = SkillShieldIt.Break_State.State_4;
						}
					}
					else
					{
						bool flag10 = this.Shield_Current_Energy >= 1 && this.Shield_Current_Energy <= 2005;
						if (flag10)
						{
							bool flag11 = this.Shield_Break_State != SkillShieldIt.Break_State.State_5 && this.Shield_Call_State;
							if (flag11)
							{
								this.Shield_Break_State = SkillShieldIt.Break_State.State_5;
							}
						}
						else
						{
							bool flag12 = this.Shield_Break_State != SkillShieldIt.Break_State.Break;
							if (flag12)
							{
								this.Shield_Break_State = SkillShieldIt.Break_State.Break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x001693B8 File Offset: 0x001675B8
	private void aaa()
	{
		this.Shield_Call_State = true;
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x001693C3 File Offset: 0x001675C3
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.OneSkillFinish();
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x001693D8 File Offset: 0x001675D8
	public override void OneSkillFinish()
	{
		bool shield_Call_State = this.Shield_Call_State;
		if (shield_Call_State)
		{
			this.Shield_Call_State = false;
			bool flag = base.hero.tCharAnimationFps != null && base.hero.tCharAnimationTps != null;
			if (flag)
			{
				base.hero.SetAnimator("JvDun", 0, this.Shield_Call_State, 1);
			}
			base.OneSkillFinish();
		}
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x0016944C File Offset: 0x0016764C
	public override void ModelLoaded()
	{
		base.ModelLoaded();
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x00169456 File Offset: 0x00167656
	public void DeactiveShield()
	{
		this.Shield_Call_State = false;
		base.hero.tCharAnimationTps.animator.SetBool("JvDun", false);
		base.OneSkillFinish();
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x00169484 File Offset: 0x00167684
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		this.shieldCallOn = false;
	}

	// Token: 0x040022EF RID: 8943
	private bool _Shield_Call_State;

	// Token: 0x040022F0 RID: 8944
	private int _Shield_Current_Energy = 500;

	// Token: 0x040022F1 RID: 8945
	private int _Shield_Max_Energy = 500;

	// Token: 0x040022F2 RID: 8946
	private float _Shield_Call_Time;

	// Token: 0x040022F3 RID: 8947
	private SkillShieldIt.State _Shield_State;

	// Token: 0x040022F4 RID: 8948
	private SkillShieldIt.Break_State _Shield_Break_State = SkillShieldIt.Break_State.State_1;

	// Token: 0x040022F5 RID: 8949
	private bool shieldCallOn;

	// Token: 0x040022F6 RID: 8950
	private bool PlayShieldOpenSound = true;

	// Token: 0x040022F7 RID: 8951
	private float PlayShieldSoundCD;

	// Token: 0x040022F8 RID: 8952
	private bool canPlayShieldCloseSound;

	// Token: 0x040022F9 RID: 8953
	private bool playAboutCrash;

	// Token: 0x02000638 RID: 1592
	public enum State
	{
		// Token: 0x04002FCB RID: 12235
		Wait = -1,
		// Token: 0x04002FCC RID: 12236
		CallOff,
		// Token: 0x04002FCD RID: 12237
		CallOn
	}

	// Token: 0x02000639 RID: 1593
	public enum Break_State
	{
		// Token: 0x04002FCF RID: 12239
		State_1 = 1,
		// Token: 0x04002FD0 RID: 12240
		State_2,
		// Token: 0x04002FD1 RID: 12241
		State_3,
		// Token: 0x04002FD2 RID: 12242
		State_4,
		// Token: 0x04002FD3 RID: 12243
		State_5,
		// Token: 0x04002FD4 RID: 12244
		Break
	}
}
