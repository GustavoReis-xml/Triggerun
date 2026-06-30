using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class CWeaponInput : MonoBehaviour
{
	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000861 RID: 2145 RVA: 0x00046E18 File Offset: 0x00045018
	// (set) Token: 0x06000862 RID: 2146 RVA: 0x00046E30 File Offset: 0x00045030
	public bool notInput
	{
		get
		{
			return this._notInput;
		}
		set
		{
			this._notInput = value;
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00046E3A File Offset: 0x0004503A
	public void InitWeaponInput(HeroPlayer hp)
	{
		this.NetObject = hp;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00046E44 File Offset: 0x00045044
	private bool noNetObject()
	{
		bool flag = false;
		bool flag2 = this.NetObject == null || this.NetObject.haveDestroy;
		return !flag2 || flag;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00046E78 File Offset: 0x00045078
	private void OnApplicationFocus(bool focusStatus)
	{
		bool flag = this.NetObject != null && !(this.NetObject.wc == null) && !this.NetObject.haveDestroy;
		if (flag)
		{
			bool flag2 = !focusStatus;
			if (flag2)
			{
				this.fullInputCancel();
			}
			else
			{
				this.switchFocusTrue = Time.time + 1f;
			}
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00046EE0 File Offset: 0x000450E0
	public bool focus()
	{
		bool flag = !HeroPlayer.isPlayerControl;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.switchFocusTrue > Time.time;
			result = !flag2;
		}
		return result;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00046F1C File Offset: 0x0004511C
	private void Update()
	{
		bool flag = this.NetObject == null || this.NetObject.wc == null || Time.deltaTime == 0f || this.NetObject.isDead || this.NetObject.haveDestroy || !this.NetObject.playerHaveCreate || this.NetObject.playerCommon.m_nDongjie == 1 || this.NetObject.playerCommon.m_nHunMi == 1;
		if (!flag)
		{
			this.OnShowZhunXin(this.NetObject.nHuDongState);
			bool flag2 = this.notInput || !fGameManager.Heroplayer.canControl || !HeroPlayer.isPlayerControl;
			if (flag2)
			{
				this.fullInputCancel();
			}
			else
			{
				bool flag3 = fGameManager.Heroplayer.delayNet || !this.focus();
				if (!flag3)
				{
					bool flag4 = !ChatManager.ChatInput && this.NetObject.CanHuDong && !ChatManager.IsHuDongHover;
					bool flag5 = false;
					bool flag6 = Input_Manager.Get_Button("WeaponFirst") && Input_Manager.Get_Button("WeaponSec");
					if (flag6)
					{
						flag5 = true;
					}
					string text = "WeaponFirst";
					bool flag7 = flag4 && !this.notShoot && Input_Manager.Get_Button_Down(text);
					if (flag7)
					{
						this.lastInput = text;
						bool flag8 = this.NetObject.heroSkill.defaultReadySkill != 0 && this.NetObject.skillInfo.getReadyItemKeyUpOrDown(this.NetObject.heroSkill.defaultReadySkill) == 1;
						if (flag8)
						{
							this.NetObject.heroSkill.DoSkill(this.NetObject.heroSkill.defaultReadySkill, 0);
							this.switchFocusTrue = Time.time + 0.1f;
							return;
						}
						bool flag9 = !this.notFire;
						if (flag9)
						{
							bool flag10 = this.NetObject.wc.sInputFullKey.Length > 1;
							if (flag10)
							{
								((CWeaponControlMe)this.NetObject.wc).readyCancel(this.NetObject.wc.sInputFullKey, false);
							}
							bool flag11 = !((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
							if (flag11)
							{
								return;
							}
							CWeaponMe cweaponMe = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
							bool flag12 = cweaponMe.weaponData.keyfull != text;
							if (flag12)
							{
								return;
							}
							cweaponMe.WeaponDownKey();
						}
					}
					bool flag13 = Input_Manager.Get_Button_Up(text);
					if (flag13)
					{
						bool flag14 = this.NetObject.wc.sInputFullKey.Length > 1;
						if (flag14)
						{
							bool flag15 = ((CWeaponControlMe)this.NetObject.wc).fullIsOpen && this.NetObject.heroSkill.defaultReadySkill != 0 && !this.notShoot;
							if (flag15)
							{
								bool flag16 = this.NetObject.skillInfo.getReadyItemKeyUpOrDown(this.NetObject.heroSkill.defaultReadySkill) == 3;
								if (flag16)
								{
									this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
								}
								bool flag17 = this.NetObject.skillInfo.getReadyItemKeyUpOrDown(this.NetObject.heroSkill.defaultReadySkill) == 2;
								if (flag17)
								{
									this.NetObject.heroSkill.DoSkill(this.NetObject.heroSkill.defaultReadySkill, 0);
								}
								return;
							}
							bool canHuDong = this.NetObject.CanHuDong;
							if (canHuDong)
							{
								((CWeaponControlMe)this.NetObject.wc).readyCancel(text, true);
							}
							else
							{
								this.fullInputCancel();
							}
						}
						((CWeaponControlMe)this.NetObject.wc).fullIsOpen = true;
					}
					bool flag18 = flag4 && !this.notShoot && !this.notFire && Input_Manager.Get_Button(text);
					if (flag18)
					{
						bool flag19 = this.NetObject.CheckActiveStat();
						if (flag19)
						{
							this.fullInputCancel();
							return;
						}
						bool flag20 = ((CWeaponControlMe)this.NetObject.wc).fullIsOpen && ((CWeaponControlMe)this.NetObject.wc).weaponsFull.ContainsKey(text);
						if (flag20)
						{
							CWeaponMe cweaponMe2 = ((CWeaponControlMe)this.NetObject.wc).weaponsFull[text];
							cweaponMe2.WeaponFull(text);
						}
						bool flag21 = this.NetObject.heroSkill.defaultReadySkill == 0 && (!flag5 || this.lastInput == text) && ((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
						if (flag21)
						{
							CWeaponMe cweaponMe3 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
							cweaponMe3.WeaponFire(text);
						}
					}
					text = "WeaponSec";
					bool flag22 = flag4 && !this.notShoot && Input_Manager.Get_Button_Down(text);
					if (flag22)
					{
						this.lastInput = text;
						bool flag23 = this.NetObject.heroData.secSkill.Length > 1;
						if (flag23)
						{
							bool flag24 = this.NetObject.heroSkill.defaultReadySkill != 0;
							if (flag24)
							{
								this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
							}
							this.NetObject.heroSkill.DoSomeOne(this.NetObject.heroData.secSkill, 0);
						}
						this.NetObject.heroSkill.DoSomeOne("WeaponSec", 0);
						bool flag25 = this.NetObject.wc.sInputFullKey.Length > 1;
						if (flag25)
						{
							((CWeaponControlMe)this.NetObject.wc).fullIsOpen = false;
							this.fullInputCancel();
							return;
						}
						bool flag26 = this.NetObject.heroSkill.defaultReadySkill != 0;
						if (flag26)
						{
							bool flag27 = this.NetObject.skillInfo.getReadyItemKeyUpOrDown(this.NetObject.heroSkill.defaultReadySkill) != 3;
							if (flag27)
							{
								this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
							}
							return;
						}
						bool flag28 = !((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text) || !((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
						if (flag28)
						{
							return;
						}
						CWeaponMe cweaponMe4 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
						bool flag29 = cweaponMe4.weaponData.keyfull != text;
						if (flag29)
						{
							return;
						}
						cweaponMe4.WeaponDownKey();
					}
					bool flag30 = Input_Manager.Get_Button_Up(text);
					if (flag30)
					{
						bool flag31 = this.NetObject.wc.sInputFullKey.Length > 1;
						if (flag31)
						{
							((CWeaponControlMe)this.NetObject.wc).readyCancel(text, true);
						}
						bool flag32 = this.NetObject.heroSkill.defaultReadySkill == 1021;
						if (flag32)
						{
							this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
							return;
						}
					}
					bool flag33 = flag4 && !this.notShoot && Input_Manager.Get_Button(text);
					if (flag33)
					{
						bool flag34 = this.NetObject.CheckActiveStat();
						if (flag34)
						{
							return;
						}
						bool flag35 = ((CWeaponControlMe)this.NetObject.wc).weaponsFull.ContainsKey(text);
						if (flag35)
						{
							CWeaponMe cweaponMe5 = ((CWeaponControlMe)this.NetObject.wc).weaponsFull[text];
							cweaponMe5.WeaponFull(text);
						}
						bool flag36 = this.NetObject.heroSkill.defaultReadySkill == 0 && (!flag5 || this.lastInput == text) && ((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
						if (flag36)
						{
							CWeaponMe cweaponMe6 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
							cweaponMe6.WeaponFire(text);
						}
					}
					text = "Fight";
					bool flag37 = flag4 && !this.notShoot && Input_Manager.Get_Button_Down(text);
					if (flag37)
					{
						this.lastInput = text;
						bool flag38 = this.NetObject.heroSkill.defaultReadySkill != 0;
						if (flag38)
						{
							this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
						}
						bool flag39 = this.NetObject.wc.sInputFullKey.Length > 1;
						if (flag39)
						{
							this.fullInputCancel();
						}
						bool flag40 = !((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
						if (flag40)
						{
							return;
						}
						CWeaponMe cweaponMe7 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
						bool flag41 = cweaponMe7.weaponData.keyfull != text;
						if (flag41)
						{
							return;
						}
						cweaponMe7.WeaponDownKey();
						this.NetObject.CheckActiveStat();
					}
					bool flag42 = Input_Manager.Get_Button_Up(text) && this.NetObject.wc.sInputFullKey.Length > 1;
					if (flag42)
					{
						((CWeaponControlMe)this.NetObject.wc).readyCancel(text, true);
					}
					bool flag43 = flag4 && !this.notShoot && Input_Manager.Get_Button(text);
					if (flag43)
					{
						bool flag44 = ((CWeaponControlMe)this.NetObject.wc).weaponsFull.ContainsKey(text);
						if (flag44)
						{
							CWeaponMe cweaponMe8 = ((CWeaponControlMe)this.NetObject.wc).weaponsFull[text];
							cweaponMe8.WeaponFull(text);
						}
						bool flag45 = !flag5 || this.lastInput == text;
						if (flag45)
						{
							bool flag46 = this.NetObject.CheckActiveStat();
							if (flag46)
							{
								return;
							}
							bool flag47 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys.ContainsKey(text);
							if (flag47)
							{
								CWeaponMe cweaponMe9 = ((CWeaponControlMe)this.NetObject.wc).weaponKeys[text];
								cweaponMe9.WeaponFire(text);
							}
						}
					}
					bool flag48 = flag4 && Input_Manager.Get_Button_Down("Skill1");
					if (flag48)
					{
						bool flag49 = this.NetObject.heroSkill.defaultReadySkill != 0;
						if (flag49)
						{
							int defaultReadySkill = this.NetObject.heroSkill.defaultReadySkill;
							bool flag50 = this.NetObject.heroSkill.CanDo(this.NetObject.heroSkill.GetKeySkill("Skill1")) == 1;
							if (flag50)
							{
								this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
							}
							bool flag51 = this.NetObject.heroSkill.GetKeySkill("Skill1") == defaultReadySkill;
							if (flag51)
							{
								return;
							}
						}
						this.NetObject.heroSkill.DoSomeOne("Skill1", 0);
					}
					bool flag52 = flag4 && Input_Manager.Get_Button_Down("Skill2");
					if (flag52)
					{
						bool flag53 = this.NetObject.heroSkill.defaultReadySkill != 0;
						if (flag53)
						{
							int defaultReadySkill2 = this.NetObject.heroSkill.defaultReadySkill;
							bool flag54 = this.NetObject.heroSkill.CanDo(this.NetObject.heroSkill.GetKeySkill("Skill2")) == 1;
							if (flag54)
							{
								this.NetObject.heroSkill.ReadyItemOp(this.NetObject.heroSkill.defaultReadySkill, false, false);
							}
							bool flag55 = this.NetObject.heroSkill.GetKeySkill("Skill2") == defaultReadySkill2;
							if (flag55)
							{
								return;
							}
						}
						this.NetObject.heroSkill.DoSomeOne("Skill2", 0);
					}
					bool flag56 = !this.notFire && flag4 && Input_Manager.Get_Button_Down("Reload") && !this.NetObject.CheckActiveStat();
					if (flag56)
					{
						bool flag57 = this.NetObject.nHuDongState == 1;
						if (flag57)
						{
							this.NetObject.SetAnimator("HuDongState", 2, 0, 2);
						}
						bool flag58 = this.NetObject.heroSkill.defaultReadySkill == 0;
						if (flag58)
						{
							((CWeaponControlMe)this.NetObject.wc).DoCartridge();
						}
					}
				}
			}
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00047C60 File Offset: 0x00045E60
	public void fullInputCancel()
	{
		bool flag = this.NetObject.wc.sInputFullKey.Length > 1;
		if (flag)
		{
			((CWeaponControlMe)this.NetObject.wc).fullBreakCancel();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00047CA4 File Offset: 0x00045EA4
	private void OnShowZhunXin(int hdstate)
	{
		bool flag = hdstate < 50 && !this.NetObject.playerZhunxingMe.zhunxingHideing && !ChatManager.IsShowZhunXing;
		if (flag)
		{
			ChatManager.IsShowZhunXing = true;
			this.NetObject.playerZhunxingMe.zhunxingshowOrHide(true);
		}
	}

	// Token: 0x0400061E RID: 1566
	private HeroPlayer NetObject;

	// Token: 0x0400061F RID: 1567
	private bool _notInput;

	// Token: 0x04000620 RID: 1568
	public bool notShoot;

	// Token: 0x04000621 RID: 1569
	public bool notFire;

	// Token: 0x04000622 RID: 1570
	private float switchFocusTrue;

	// Token: 0x04000623 RID: 1571
	public string _inputSkill = string.Empty;

	// Token: 0x04000624 RID: 1572
	private string lastInput = string.Empty;

	// Token: 0x04000625 RID: 1573
	private const float skillReadyDoTime = 0.1f;
}
