using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class SkillShieldMe : SkillBaseMe
{
	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x0600281F RID: 10271 RVA: 0x001694C4 File Offset: 0x001676C4
	// (set) Token: 0x06002820 RID: 10272 RVA: 0x001694DC File Offset: 0x001676DC
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

	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x06002821 RID: 10273 RVA: 0x001694E8 File Offset: 0x001676E8
	// (set) Token: 0x06002822 RID: 10274 RVA: 0x00169500 File Offset: 0x00167700
	public SkillShieldMe.State Shield_State
	{
		get
		{
			return this._Shield_State;
		}
		set
		{
			this._Shield_State = value;
			ArrayList arrayList = new ArrayList();
			arrayList.Add((int)this._Shield_State);
			arrayList.Add(0);
			base.hero.SendMessageToServer(2010001, arrayList);
		}
	}

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x06002823 RID: 10275 RVA: 0x0016954C File Offset: 0x0016774C
	// (set) Token: 0x06002824 RID: 10276 RVA: 0x00169564 File Offset: 0x00167764
	public SkillShieldMe.Break_State Shield_Break_State
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

	// Token: 0x06002825 RID: 10277 RVA: 0x00169570 File Offset: 0x00167770
	private void Update()
	{
		bool flag = base.hero == null;
		if (!flag)
		{
			bool flag2 = this.reviveTimer > 0f;
			if (flag2)
			{
				this.reviveTimer -= Time.deltaTime;
			}
			else
			{
				this.openShiledTimer -= Time.deltaTime;
				bool flag3 = this.PlayShieldSoundCD > 0f;
				if (flag3)
				{
					this.PlayShieldSoundCD -= Time.deltaTime;
				}
				else
				{
					this.PlayShieldOpenSound = true;
				}
				bool flag4 = !base.hero.wInput.focus();
				if (flag4)
				{
					bool flag5 = base.hero.heroSkill.defaultReadySkill != 0;
					if (flag5)
					{
						base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
					}
					this.OneSkillFinish();
				}
				bool flag6 = base.hero.isDead && !base.hero.wInput.notInput;
				if (flag6)
				{
					bool flag7 = base.hero.heroSkill.defaultReadySkill != 0;
					if (flag7)
					{
						base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
						this.OneSkillFinish();
						((ISkillDataSkillShield)this.ISD).Shield_Call_State = false;
						base.hero.Clear_Adhere_Npc("body2_dun");
					}
				}
				else
				{
					bool flag8 = Input_Manager.Get_Button_Up("WeaponSec");
					if (flag8)
					{
						bool flag9 = base.hero.heroSkill.defaultReadySkill != 0;
						if (flag9)
						{
							this.openShiledTimer = 2f;
							base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
							bool flag10 = this.canPlayShieldCloseSound;
							if (flag10)
							{
								base.hero.playerSound.PlayShieldContinueSound(1);
							}
						}
						((ISkillDataSkillShield)this.ISD).Shield_Call_State = false;
						base.hero.Clear_Adhere_Npc("body2_dun");
						this.ShieldActive = false;
						this.breakShieldOn = false;
					}
					bool flag11 = this.breakShieldOn;
					if (flag11)
					{
						this.ReActiveTimer -= Time.deltaTime;
						bool flag12 = this.ReActiveTimer <= 0f && Input_Manager.Get_Button("WeaponSec") && base.hero.playerCommon.m_nDongjie == 0 && base.hero.playerCommon.m_nHunMi == 0;
						if (flag12)
						{
							this.BreakTimer = 0.1f;
							base.hero.heroSkill.ReadyItemOp(this.skillData.id, true, false);
							this.aaa();
							this.breakShieldOn = false;
						}
					}
					bool flag13 = this.BreakTimer > 0f;
					if (flag13)
					{
						this.BreakTimer -= Time.deltaTime;
					}
					this.State_Handle();
				}
			}
		}
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x00169871 File Offset: 0x00167A71
	public void DeactiveShield()
	{
		base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		this.OneSkillFinish();
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x001698A0 File Offset: 0x00167AA0
	public void SkillBreakShield(float timer)
	{
		bool flag = base.hero.heroSkill.defaultReadySkill != 0;
		if (flag)
		{
			base.hero.heroSkill.ReadyItemOp(base.hero.heroSkill.defaultReadySkill, false, false);
		}
		((ISkillDataSkillShield)this.ISD).Shield_Call_State = false;
		base.hero.Clear_Adhere_Npc("body2_dun");
		this.breakShieldOn = true;
		this.ShieldActive = false;
		this.ReActiveTimer = timer;
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x00169922 File Offset: 0x00167B22
	private IEnumerator callOnShiled(float timer)
	{
		yield return new WaitForSeconds(timer);
		while (Input_Manager.Get_Button("WeaponSec") && this.breakShieldOn)
		{
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x00169938 File Offset: 0x00167B38
	private void State_Handle()
	{
		bool flag = this.Shield_State == SkillShieldMe.State.Wait;
		if (flag)
		{
			bool shield_Call_State = ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
			if (shield_Call_State)
			{
				this.State_Handle(0);
			}
			else
			{
				this.State_Handle(-1);
			}
		}
		else
		{
			bool flag2 = this.Shield_State == SkillShieldMe.State.CallOff;
			if (flag2)
			{
				bool shield_Call_State2 = ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
				if (shield_Call_State2)
				{
					this.State_Handle(1);
				}
			}
			else
			{
				bool flag3 = this.Shield_State == SkillShieldMe.State.CallOn && !((ISkillDataSkillShield)this.ISD).Shield_Call_State;
				if (flag3)
				{
					this.State_Handle(0);
				}
			}
		}
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x001699E0 File Offset: 0x00167BE0
	private void State_Handle(int _Value)
	{
		switch (_Value)
		{
		case -1:
		{
			bool flag = this.Shield_State != SkillShieldMe.State.Wait;
			if (flag)
			{
				base.hero.wInput.notShoot = false;
				this.Shield_State = SkillShieldMe.State.Wait;
				base.hero.SetAnimator("JvDun", 0, false, 2);
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().isKinematic = true;
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().useGravity = true;
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<BoxCollider>().enabled = false;
				base.hero.playerCommon.m_fJumpSpeed = 1f;
				base.hero.ChangeSpeed();
				base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
				base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			}
			break;
		}
		case 0:
			base.hero.wInput.notShoot = false;
			this.Shield_State = SkillShieldMe.State.CallOff;
			base.hero.SetAnimator("JvDun", 0, false, 2);
			((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().isKinematic = true;
			((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().useGravity = true;
			((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<BoxCollider>().enabled = false;
			base.hero.playerCommon.m_fJumpSpeed = 1f;
			base.hero.ChangeSpeed();
			base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			break;
		case 1:
		{
			bool flag2 = ((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 1;
			if (flag2)
			{
				bool flag3 = this.Shield_Call_Time == 0f || Time.time >= this.Shield_Call_Time + 30f;
				if (flag3)
				{
					this.Shield_Call_Time = Time.time;
					PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1952];
					Fun.Load_Sound(base.hero, preloadData.sUrl, base.hero.thisModel, true, "Reinhardt_Shield_Sound");
				}
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
				base.hero.playerCommon.m_fJumpSpeed = this.SheildJumpSpeed;
				base.hero.ChangeSpeed();
				this.Shield_State = SkillShieldMe.State.CallOn;
				base.hero.SetAnimator("JvDun", 0, true, 2);
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().isKinematic = false;
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<Rigidbody>().useGravity = false;
				((HeroXingTianMe)base.hero.heroBase).Shield_Object.GetComponent<BoxCollider>().enabled = true;
				base.hero.wInput.notShoot = true;
				base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
			}
			break;
		}
		}
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x00169D60 File Offset: 0x00167F60
	public void Update_Shield_Data(ArrayList _Parameters)
	{
		bool flag = _Parameters != null;
		if (flag)
		{
			int num = int.Parse(_Parameters[0].ToString());
			((ISkillDataSkillShield)this.ISD).Shield_Current_Energy = int.Parse(_Parameters[1].ToString());
			((ISkillDataSkillShield)this.ISD).Shield_Max_Energy = int.Parse(_Parameters[2].ToString());
			this.GemFactor = (float)((ISkillDataSkillShield)this.ISD).Shield_Max_Energy / 10000f + 0.001f;
			bool flag2 = num == -1 && this.Shield_State != SkillShieldMe.State.Wait;
			if (flag2)
			{
				this.Shield_State = SkillShieldMe.State.Wait;
			}
		}
		bool flag3 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 8005f * this.GemFactor && (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 10000f * this.GemFactor;
		if (flag3)
		{
			bool flag4 = this.Shield_Break_State != SkillShieldMe.Break_State.State_1 && ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
			if (flag4)
			{
				this.Shield_Break_State = SkillShieldMe.Break_State.State_1;
				base.hero.SetAnimator("SheildState", 2, 1, 2);
			}
		}
		else
		{
			bool flag5 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 6005f * this.GemFactor && (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 8004f * this.GemFactor;
			if (flag5)
			{
				bool flag6 = this.Shield_Break_State != SkillShieldMe.Break_State.State_2 && ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
				if (flag6)
				{
					this.Shield_Break_State = SkillShieldMe.Break_State.State_2;
					base.hero.SetAnimator("SheildState", 2, 2, 2);
				}
			}
			else
			{
				bool flag7 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 4005f * this.GemFactor && (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 6004f * this.GemFactor;
				if (flag7)
				{
					bool flag8 = this.Shield_Break_State != SkillShieldMe.Break_State.State_3 && ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
					if (flag8)
					{
						this.Shield_Break_State = SkillShieldMe.Break_State.State_3;
						base.hero.SetAnimator("SheildState", 2, 3, 2);
					}
				}
				else
				{
					bool flag9 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 2005f * this.GemFactor && (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 4004f * this.GemFactor;
					if (flag9)
					{
						bool flag10 = this.Shield_Break_State != SkillShieldMe.Break_State.State_4 && ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
						if (flag10)
						{
							this.Shield_Break_State = SkillShieldMe.Break_State.State_4;
							base.hero.SetAnimator("SheildState", 2, 4, 2);
						}
					}
					else
					{
						bool flag11 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy >= 1f * this.GemFactor && (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 2005f * this.GemFactor;
						if (flag11)
						{
							bool flag12 = this.Shield_Break_State != SkillShieldMe.Break_State.State_5 && ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
							if (flag12)
							{
								base.hero.playerSound.PlayShieldAboutCrashSound();
								this.Shield_Break_State = SkillShieldMe.Break_State.State_5;
								base.hero.SetAnimator("SheildState", 2, 5, 2);
							}
						}
						else
						{
							bool flag13 = (float)((ISkillDataSkillShield)this.ISD).Shield_Current_Energy <= 0f && this.Shield_Break_State != SkillShieldMe.Break_State.Break;
							if (flag13)
							{
								this.Shield_Break_State = SkillShieldMe.Break_State.Break;
								base.hero.playerSound.PlayShieldContinueSound(2);
								base.hero.playerSound.PlayShieldBreakSound();
								bool flag14 = base.hero.heroSkill.defaultReadySkill != 0;
								if (flag14)
								{
									base.hero.wInput.notShoot = false;
									base.hero.heroSkill.DoSkill(base.hero.heroSkill.defaultReadySkill, 0);
								}
								this.Shield_State = SkillShieldMe.State.Wait;
								base.hero.SetAnimator("JvDun", 0, false, 2);
								base.hero.SetAnimator("SheildState", 2, 6, 2);
								base.hero.playerCommon.m_fJumpSpeed = 1f;
								base.hero.ChangeSpeed();
								base.hero.changeCameraState(CameraState.FirstState, null, "yanluo_super");
								base.hero.wInput.notShoot = false;
								base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
								this.SkillBreakShield(5.5f);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x0016A260 File Offset: 0x00168460
	private void aaa()
	{
		this.ShieldActive = true;
		((ISkillDataSkillShield)this.ISD).Shield_Call_State = true;
		base.StartCoroutine(this.PlayShieldContinueSound());
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x0016A289 File Offset: 0x00168489
	private IEnumerator PlayShieldContinueSound()
	{
		bool playShieldOpenSound = this.PlayShieldOpenSound;
		if (playShieldOpenSound)
		{
			this.PlayShieldSoundCD = 0f;
			this.PlayShieldOpenSound = false;
			this.canPlayShieldCloseSound = true;
			base.hero.playerSound.PlayShieldOpenSound();
		}
		yield return new WaitForSeconds(1f);
		bool shield_Call_State = ((ISkillDataSkillShield)this.ISD).Shield_Call_State;
		if (shield_Call_State)
		{
			base.hero.playerSound.PlayShieldContinueSound(0);
		}
		yield break;
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x0016A298 File Offset: 0x00168498
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.OneSkillFinish();
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x0016A2AC File Offset: 0x001684AC
	public override void OneSkillFinish()
	{
		bool flag = !((ISkillDataSkillShield)this.ISD).Shield_Call_State;
		if (!flag)
		{
			((ISkillDataSkillShield)this.ISD).Shield_Call_State = false;
			base.hero.Clear_Adhere_Npc("body2_dun");
			bool flag2 = base.hero.tCharAnimationFps != null && base.hero.tCharAnimationTps != null;
			if (flag2)
			{
				base.hero.SetAnimator("JvDun", 0, ((ISkillDataSkillShield)this.ISD).Shield_Call_State, 1);
				bool flag3 = this.canPlayShieldCloseSound;
				if (flag3)
				{
					base.hero.playerSound.PlayShieldContinueSound(1);
				}
			}
			base.OneSkillFinish();
		}
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x0016A374 File Offset: 0x00168574
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
		this.SheildJumpSpeed = 0.5f;
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x0016A390 File Offset: 0x00168590
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		if (switchOpen)
		{
			bool flag = !this.breakShieldOn;
			if (flag)
			{
				this.aaa();
			}
		}
		else
		{
			this.Shield_Break_State = SkillShieldMe.Break_State.State_0;
			bool flag2 = this.canPlayShieldCloseSound;
			if (flag2)
			{
				base.hero.playerSound.PlayShieldContinueSound(1);
			}
		}
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x0016A3E4 File Offset: 0x001685E4
	public override bool OneSkillCanDo()
	{
		bool flag = this.BreakTimer > 0f;
		return !flag && base.OneSkillCanDo();
	}

	// Token: 0x040022FA RID: 8954
	private bool PlayShieldOpenSound = true;

	// Token: 0x040022FB RID: 8955
	private float PlayShieldSoundCD;

	// Token: 0x040022FC RID: 8956
	private bool canPlayShieldCloseSound;

	// Token: 0x040022FD RID: 8957
	private float _Shield_Call_Time;

	// Token: 0x040022FE RID: 8958
	private SkillShieldMe.State _Shield_State;

	// Token: 0x040022FF RID: 8959
	public bool ShieldActive;

	// Token: 0x04002300 RID: 8960
	private SkillShieldMe.Break_State _Shield_Break_State;

	// Token: 0x04002301 RID: 8961
	private float BreakTimer = 0.5f;

	// Token: 0x04002302 RID: 8962
	public float reviveTimer = 0.5f;

	// Token: 0x04002303 RID: 8963
	private float openShiledTimer = 0.2f;

	// Token: 0x04002304 RID: 8964
	private bool breakShieldOn;

	// Token: 0x04002305 RID: 8965
	private float ReActiveTimer;

	// Token: 0x04002306 RID: 8966
	private float GemFactor = 1f;

	// Token: 0x04002307 RID: 8967
	private float SheildJumpSpeed;

	// Token: 0x0200063B RID: 1595
	public enum State
	{
		// Token: 0x04002FD9 RID: 12249
		Wait = -1,
		// Token: 0x04002FDA RID: 12250
		CallOff,
		// Token: 0x04002FDB RID: 12251
		CallOn
	}

	// Token: 0x0200063C RID: 1596
	public enum Break_State
	{
		// Token: 0x04002FDD RID: 12253
		State_0,
		// Token: 0x04002FDE RID: 12254
		State_1,
		// Token: 0x04002FDF RID: 12255
		State_2,
		// Token: 0x04002FE0 RID: 12256
		State_3,
		// Token: 0x04002FE1 RID: 12257
		State_4,
		// Token: 0x04002FE2 RID: 12258
		State_5,
		// Token: 0x04002FE3 RID: 12259
		Break
	}
}
