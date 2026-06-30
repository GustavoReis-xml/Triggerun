using System;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B5 RID: 1205
public class zhunxingBase : zhunxingUi
{
	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x06002E8E RID: 11918 RVA: 0x001A00EC File Offset: 0x0019E2EC
	// (set) Token: 0x06002E8F RID: 11919 RVA: 0x001A0104 File Offset: 0x0019E304
	public bool rateCan
	{
		get
		{
			return this._rateCan;
		}
		set
		{
			this._rateCan = value;
		}
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x06002E90 RID: 11920 RVA: 0x001A0110 File Offset: 0x0019E310
	// (set) Token: 0x06002E91 RID: 11921 RVA: 0x001A0128 File Offset: 0x0019E328
	public bool zhunxingHideing
	{
		get
		{
			return this._zhunxingHideing;
		}
		set
		{
			this._zhunxingHideing = value;
		}
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x001A0132 File Offset: 0x0019E332
	protected virtual void Update()
	{
		this.CheckShiZiZhunXing();
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x001A013C File Offset: 0x0019E33C
	public virtual void init(NetLiving _hero)
	{
		this.hero = _hero;
		base.oneHeroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x001A016C File Offset: 0x0019E36C
	public virtual void readyShow()
	{
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x001A016F File Offset: 0x0019E36F
	public virtual void readyDisable()
	{
	}

	// Token: 0x06002E96 RID: 11926 RVA: 0x001A0172 File Offset: 0x0019E372
	public virtual void ShowSkillDo(object opam)
	{
	}

	// Token: 0x06002E97 RID: 11927 RVA: 0x001A0175 File Offset: 0x0019E375
	public virtual void ShowSkillFinish(object opam)
	{
	}

	// Token: 0x06002E98 RID: 11928 RVA: 0x001A0178 File Offset: 0x0019E378
	public virtual void litterHit()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gLitterHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gLitterHit.SetActive(true);
			this.hitType = 0;
		}
	}

	// Token: 0x06002E99 RID: 11929 RVA: 0x001A01CC File Offset: 0x0019E3CC
	public void ZhunXingColor()
	{
		bool flag = !(base.Cross_Hair_Handle_Component == null);
		if (flag)
		{
			base.Cross_Hair_Handle_Component._Delay_Setting_Color_Time = 0.15f;
			base.Cross_Hair_Handle_Component.Setting_Color(true);
		}
	}

	// Token: 0x06002E9A RID: 11930 RVA: 0x001A020C File Offset: 0x0019E40C
	public virtual void bigHit()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gBigHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gBigHit.SetActive(true);
			this.hitType = 1;
		}
	}

	// Token: 0x06002E9B RID: 11931 RVA: 0x001A0260 File Offset: 0x0019E460
	public virtual void Shield_Hit()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gShieldHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gShieldHit.SetActive(true);
			this.hitType = 5;
		}
	}

	// Token: 0x06002E9C RID: 11932 RVA: 0x001A02B4 File Offset: 0x0019E4B4
	public virtual void baotouHit()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gBaotouHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gBaotouHit.SetActive(true);
			this.hitType = 2;
		}
	}

	// Token: 0x06002E9D RID: 11933 RVA: 0x001A0308 File Offset: 0x0019E508
	public virtual void joinKill()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gcanyuhitHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gcanyuhitHit.SetActive(true);
			this.hitType = 3;
		}
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x001A035C File Offset: 0x0019E55C
	public virtual void lastKill(int nPlace)
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gNewLastHit == null);
		if (flag)
		{
			this.hideHit();
			base.hiteffect.gNewLastHit.SetActive(true);
			bool flag2 = nPlace == 1;
			if (flag2)
			{
				base.hiteffect.gBaotouHit.SetActive(true);
			}
			this.hitType = 4;
		}
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x001A03CC File Offset: 0x0019E5CC
	public void skillReviveEffect()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gReviveIng == null) && !base.hiteffect.gReviveIng.activeSelf;
		if (flag)
		{
			base.hiteffect.gReviveIng.SetActive(true);
			bool flag2 = base.IsInvoking("CloseSkillRevive");
			if (flag2)
			{
				base.CancelInvoke("CloseSkillRevive");
			}
			base.Invoke("CloseSkillRevive", 4f);
		}
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x001A0454 File Offset: 0x0019E654
	private void CloseSkillRevive()
	{
		bool flag = base.hiteffect != null && !(base.hiteffect.gReviveIng == null) && base.hiteffect.gReviveIng.activeSelf;
		if (flag)
		{
			base.hiteffect.gReviveIng.SetActive(false);
		}
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x001A04A8 File Offset: 0x0019E6A8
	public void OnLianShaEffect(int nKillCount)
	{
		string text = string.Empty;
		int fontSize = 60;
		bool flag = base.hiteffect == null || nKillCount < 2;
		if (!flag)
		{
			Vector3 one = Vector3.one;
			switch (nKillCount)
			{
			case 2:
				text = Language.GetLang(2037, new object[0]);
				one = new Vector3(2.5f, 2.5f, 1f);
				fontSize = 70;
				break;
			case 3:
				text = Language.GetLang(2038, new object[0]);
				one = new Vector3(3f, 3f, 1f);
				fontSize = 80;
				break;
			case 4:
				text = Language.GetLang(2039, new object[0]);
				one = new Vector3(4f, 4f, 1f);
				fontSize = 90;
				break;
			case 5:
				text = Language.GetLang(2040, new object[0]);
				one = new Vector3(5f, 5f, 1f);
				fontSize = 100;
				break;
			default:
				if (nKillCount == 100)
				{
					text = Language.GetLang(2041, new object[0]);
					one = new Vector3(5f, 5f, 1f);
					fontSize = 110;
				}
				break;
			}
			bool flag2 = base.IsInvoking("OnDelayText");
			if (flag2)
			{
				base.CancelInvoke("OnDelayText");
			}
			base.hiteffect.gHitTextEffect.text = text;
			base.hiteffect.gHitTextEffect.fontSize = fontSize;
			base.hiteffect.gHitTextEffect.transform.localScale = Vector3.one;
			base.hiteffect.gHitTextEffect.enabled = true;
			Tweener t = base.hiteffect.gHitTextEffect.transform.transform.DOScale(one, 0.1f);
			t.OnComplete(delegate
			{
				Tweener t2 = base.hiteffect.gHitTextEffect.transform.transform.DOScale(Vector3.one, 0.1f);
				t2.OnComplete(delegate
				{
					base.Invoke("OnDelayText", 3f);
				});
			});
		}
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x001A0696 File Offset: 0x0019E896
	private void OnDelayText()
	{
		base.hiteffect.gHitTextEffect.text = string.Empty;
		base.hiteffect.gHitTextEffect.enabled = false;
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x001A06C4 File Offset: 0x0019E8C4
	public void OnPlayPinSiEffect(int nStat)
	{
		bool flag = base.hiteffect == null || base.hiteffect.gPinSiHit == null;
		if (!flag)
		{
			bool flag2 = nStat == 1;
			if (flag2)
			{
				bool flag3 = !base.hiteffect.gPinSiHit.activeSelf;
				if (flag3)
				{
					base.hiteffect.gPinSiHit.SetActive(true);
				}
			}
			else
			{
				bool activeSelf = base.hiteffect.gPinSiHit.activeSelf;
				if (activeSelf)
				{
					base.hiteffect.gPinSiHit.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x001A0754 File Offset: 0x0019E954
	public void OnPlayerFpsEffect(string sEffName, bool mbool)
	{
		GameObject playerFpsEffect = base.GetPlayerFpsEffect(sEffName);
		bool flag = !(playerFpsEffect == null) && playerFpsEffect.activeSelf != mbool;
		if (flag)
		{
			playerFpsEffect.SetActive(mbool);
		}
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x001A0794 File Offset: 0x0019E994
	public void zhunxingshowOrHide(bool boo)
	{
		bool flag = this.hero.netobject_type == 2 || this.hero.isDead;
		if (flag)
		{
			boo = false;
		}
		bool flag2 = base.zhunxingshizi != null && base.zhunxingshizi.gameObject.activeSelf != boo;
		if (flag2)
		{
			base.zhunxingshizi.gameObject.SetActive(boo);
		}
		bool flag3 = boo != this.zhunxingHideing;
		if (flag3)
		{
			this.zhunxingHideing = boo;
		}
		this.fshiziratenow = -1f;
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x001A082C File Offset: 0x0019EA2C
	public void CheckShiZiZhunXing()
	{
		bool flag = this._rateCan && this.hero.wc != null && this.zhunxingHideing && base.zhunxingshizi != null;
		if (flag)
		{
			bool flag2 = base.imageRate != null && base.imageRate.enabled;
			if (flag2)
			{
				base.imageRate.enabled = false;
			}
			bool flag3 = !base.zhunxingshizi.gameObject.activeSelf;
			if (flag3)
			{
				base.zhunxingshizi.gameObject.SetActive(true);
			}
			bool flag4 = this.fshiziratenow != this.hero.wc.hitRateNow;
			if (flag4)
			{
				float num = this.hero.wc.hitRateNow / this.hero.wc.hitRateMax * (float)base.oneHeroData.hratesize;
				bool flag5 = num < (float)base.oneHeroData.Min_Rate_Size;
				if (flag5)
				{
					num = (float)base.oneHeroData.Min_Rate_Size;
				}
				bool flag6 = base.doShiZiZhunxingChange(2f * num);
				if (flag6)
				{
					this.fshiziratenow = this.hero.wc.hitRateNow;
				}
			}
		}
		else
		{
			this.fshiziratenow = -1f;
		}
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x001A0984 File Offset: 0x0019EB84
	public bool IsCheckTarget(NetLiving NetTarget)
	{
		bool flag = this.hero.screentarget.NetTarget == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = NetTarget.thisModel == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				Vector3 position = NetTarget.thisModel.transform.position;
				bool flag3 = !NetTarget.isDead;
				result = flag3;
			}
		}
		return result;
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x001A09F0 File Offset: 0x0019EBF0
	public bool GetIsBuff(NetLiving netlve, int nBuffId)
	{
		bool flag = false;
		bool flag2 = netlve == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = netlve.netobject_type == 1;
			if (flag3)
			{
				flag = ((GamePlayer)netlve).playerBuffIt.GetIsBuff(nBuffId);
			}
			bool flag4 = netlve.netobject_type == 2;
			if (flag4)
			{
				flag = ((Npc_Entity)netlve).NpcBuff.GetIsBuff(nBuffId);
			}
			bool flag5 = netlve.netobject_type == 6;
			if (flag5)
			{
				flag = ((RobotAI)netlve).playerBuffIt.GetIsBuff(nBuffId);
			}
			result = flag;
		}
		return result;
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x001A0A7C File Offset: 0x0019EC7C
	public void CheckAutoAir_Yf(int nSkillId, int nBuffId)
	{
		bool flag = base.OwnAutoAirUI == null || CameraControl.Instance.camerHero != this.hero;
		if (!flag)
		{
			bool flag2 = this.hero != null && this.hero.screentarget != null && this.IsCheckTarget(this.hero.screentarget.NetTarget) && !this.hero.isDead && this.hero.screentarget.NetTarget.nForce == this.hero.nForce && !this.GetIsBuff(this.hero.screentarget.NetTarget, nBuffId) && !this.hero.screentarget.NetTarget.heroInvi && this.bCanCheckSkill(this.hero, nSkillId);
			if (flag2)
			{
				this.GetSkillKeys(base.OwnAutoAirUI, nSkillId);
				bool flag3 = !base.OwnAutoAirUI.gameObject.activeSelf;
				if (flag3)
				{
					base.OwnAutoAirUI.gameObject.SetActive(true);
				}
				base.OnAutoAirEffect(this.hero.screentarget.NetTarget, base.OwnAutoAirUI.gameObject);
			}
			else
			{
				bool activeSelf = base.OwnAutoAirUI.gameObject.activeSelf;
				if (activeSelf)
				{
					base.OwnAutoAirUI.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x001A0BF0 File Offset: 0x0019EDF0
	public void CheckAutoAir_Df(int nSkillId, int nBuffId)
	{
		bool flag = base.ItAutoAirUI == null || CameraControl.Instance.camerHero != this.hero;
		if (!flag)
		{
			bool flag2 = this.hero != null && this.hero.screentarget != null && this.IsCheckTarget(this.hero.screentarget.NetTarget) && !this.hero.isDead && this.hero.screentarget.NetTarget.nForce != this.hero.nForce && !this.GetIsBuff(this.hero.screentarget.NetTarget, nBuffId) && !this.hero.screentarget.NetTarget.heroInvi && this.bCanCheckSkill(this.hero, nSkillId);
			if (flag2)
			{
				this.GetSkillKeys(base.ItAutoAirUI, nSkillId);
				bool flag3 = !base.ItAutoAirUI.gameObject.activeSelf;
				if (flag3)
				{
					base.ItAutoAirUI.gameObject.SetActive(true);
				}
				base.OnAutoAirEffect(this.hero.screentarget.NetTarget, base.ItAutoAirUI.gameObject);
			}
			else
			{
				bool activeSelf = base.ItAutoAirUI.gameObject.activeSelf;
				if (activeSelf)
				{
					base.ItAutoAirUI.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x001A0D64 File Offset: 0x0019EF64
	private bool bCanCheckSkill(NetLiving heroMainMe, int nskindex)
	{
		bool flag = true;
		bool flag2 = !heroMainMe.heroSkill.skillsDict.ContainsKey(nskindex);
		bool result;
		if (flag2)
		{
			result = false;
		}
		else
		{
			CPSkillStruct cpskillStruct = heroMainMe.heroSkill.skillsDict[nskindex];
			bool flag3 = cpskillStruct == null;
			if (flag3)
			{
				result = false;
			}
			else
			{
				bool flag4 = MatchManager.GetInstance().m_nMatchType == 4;
				if (flag4)
				{
					int num = DtsBatteManager.GetInstance().CanDtsScnSkill(heroMainMe, nskindex);
					bool flag5 = num == -1;
					if (flag5)
					{
						return flag;
					}
					bool flag6 = cpskillStruct.m_nDoNumberNow == 0 && num == 0;
					if (flag6)
					{
						flag = false;
					}
				}
				result = flag;
			}
		}
		return result;
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x001A0E08 File Offset: 0x0019F008
	public void ChecklanboAutoAir_Df(bool isSuperSkillIng)
	{
		bool flag = base.ItAutoAirUI == null || CameraControl.Instance.camerHero != this.hero;
		if (!flag)
		{
			GameObject gameObject = base.ItAutoAirUI.Find("ico").gameObject;
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf)
			{
				gameObject.SetActive(false);
			}
			bool flag2 = this.hero != null && this.hero.screentarget != null && this.IsCheckTarget(this.hero.screentarget.NetTarget) && !this.hero.isDead && this.hero.screentarget.NetTarget.nForce != this.hero.nForce && !this.hero.screentarget.NetTarget.heroInvi && isSuperSkillIng;
			if (flag2)
			{
				bool flag3 = !base.ItAutoAirUI.gameObject.activeSelf;
				if (flag3)
				{
					base.ItAutoAirUI.gameObject.SetActive(true);
				}
				base.OnAutoAirEffect(this.hero.screentarget.NetTarget, base.ItAutoAirUI.gameObject);
			}
			else
			{
				bool activeSelf2 = base.ItAutoAirUI.gameObject.activeSelf;
				if (activeSelf2)
				{
					base.ItAutoAirUI.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x001A0F70 File Offset: 0x0019F170
	private void GetSkillKeys(Transform AutoAirUI, int nSkillId)
	{
		string text = string.Empty;
		Image component = AutoAirUI.Find("ico").GetComponent<Image>();
		Text component2 = AutoAirUI.Find("ico/key").GetComponent<Text>();
		Image component3 = AutoAirUI.Find("ico/m_key").GetComponent<Image>();
		string text2 = string.Empty;
		bool flag = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(nSkillId);
		if (flag)
		{
			PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[nSkillId];
			bool flag2 = playerSkillData != null;
			if (flag2)
			{
				bool flag3 = playerSkillData.dokey == "Skill1";
				if (flag3)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.hero.playerCommon.m_nModelIndex).Key_Use_Weapon_2;
					bool flag4 = text == string.Empty;
					if (flag4)
					{
						text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.hero.playerCommon.m_nModelIndex).Key_Skill_1;
					}
					text2 = "<color='#00ffff'>" + GameRoot.inst.Setting.Get_Key_Describe(text) + "</color>";
				}
				bool flag5 = playerSkillData.dokey == "Skill2";
				if (flag5)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.hero.playerCommon.m_nModelIndex).Key_Skill_2;
					text2 = "<color='#ff0000'>" + GameRoot.inst.Setting.Get_Key_Describe(text) + "</color>";
				}
				bool flag6 = playerSkillData.dokey == "WeaponSec";
				if (flag6)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.hero.playerCommon.m_nModelIndex).Key_Use_Weapon_2;
					text2 = "<color='#00ffff'>" + GameRoot.inst.Setting.Get_Key_Describe(text) + "</color>";
				}
			}
		}
		bool flag7 = text == "Mouse0";
		if (flag7)
		{
			component3.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "zuojian");
			component3.enabled = true;
			component2.text = string.Empty;
		}
		else
		{
			bool flag8 = text == "Mouse1";
			if (flag8)
			{
				component3.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "youjian");
				component3.enabled = true;
				component2.text = string.Empty;
			}
			else
			{
				bool enabled = component3.enabled;
				if (enabled)
				{
					component3.enabled = false;
				}
				bool flag9 = text2 != string.Empty;
				if (flag9)
				{
					text = GameRoot.inst.Setting.Get_Key_Describe(text);
					int num = Encoding.Default.GetBytes(text).Length - 1;
					Vector2 sizeDelta = component.GetComponent<RectTransform>().sizeDelta;
					sizeDelta.x = (float)(30 + num * 10);
					component.GetComponent<RectTransform>().sizeDelta = sizeDelta;
					component2.text = text2;
					component2.GetComponent<RectTransform>().sizeDelta = sizeDelta;
				}
			}
		}
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x001A127C File Offset: 0x0019F47C
	private void hideHit()
	{
		bool flag = base.hiteffect == null;
		if (!flag)
		{
			bool flag2 = this.hitType == 0;
			if (flag2)
			{
				bool flag3 = base.hiteffect.gLitterHit != null && base.hiteffect.gLitterHit.activeSelf;
				if (flag3)
				{
					base.hiteffect.gLitterHit.SetActive(false);
				}
			}
			else
			{
				bool flag4 = this.hitType == 1;
				if (flag4)
				{
					bool flag5 = base.hiteffect.gBigHit != null && base.hiteffect.gBigHit.activeSelf;
					if (flag5)
					{
						base.hiteffect.gBigHit.SetActive(false);
					}
				}
				else
				{
					bool flag6 = this.hitType == 2;
					if (flag6)
					{
						bool flag7 = base.hiteffect.gBaotouHit != null && base.hiteffect.gBaotouHit.activeSelf;
						if (flag7)
						{
							base.hiteffect.gBaotouHit.SetActive(false);
						}
					}
					else
					{
						bool flag8 = this.hitType == 3;
						if (flag8)
						{
							bool flag9 = base.hiteffect.gcanyuhitHit != null && base.hiteffect.gcanyuhitHit.activeSelf;
							if (flag9)
							{
								base.hiteffect.gcanyuhitHit.SetActive(false);
							}
						}
						else
						{
							bool flag10 = this.hitType == 4;
							if (flag10)
							{
								bool flag11 = base.hiteffect.gNewLastHit != null && base.hiteffect.gNewLastHit.activeSelf;
								if (flag11)
								{
									base.hiteffect.gNewLastHit.SetActive(false);
								}
								bool flag12 = base.hiteffect.gBaotouHit != null && base.hiteffect.gBaotouHit.activeSelf;
								if (flag12)
								{
									base.hiteffect.gBaotouHit.SetActive(false);
								}
							}
							else
							{
								bool flag13 = this.hitType == 5 && base.hiteffect.gShieldHit != null && base.hiteffect.gShieldHit.activeSelf;
								if (flag13)
								{
									base.hiteffect.gShieldHit.SetActive(false);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002EAF RID: 11951 RVA: 0x001A14CE File Offset: 0x0019F6CE
	public virtual void OnDeath()
	{
	}

	// Token: 0x06002EB0 RID: 11952 RVA: 0x001A14D1 File Offset: 0x0019F6D1
	public virtual void Revive()
	{
	}

	// Token: 0x06002EB1 RID: 11953 RVA: 0x001A14D4 File Offset: 0x0019F6D4
	public void SetKeyString(int nModelIndex, Text tText, Image rImg)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(nModelIndex).Key_Use_Weapon_2;
		bool flag = text == "Mouse0";
		if (flag)
		{
			rImg.sprite = Window.spriteName(AtlasType.ATLAS_ZHUNXING, "sbz");
			rImg.enabled = true;
			tText.text = Language.GetLang(3014, new object[0]);
		}
		else
		{
			bool flag2 = text == "Mouse1";
			if (flag2)
			{
				rImg.sprite = Window.spriteName(AtlasType.ATLAS_ZHUNXING, "sby");
				rImg.enabled = true;
				tText.text = Language.GetLang(3014, new object[0]);
			}
			else
			{
				bool enabled = rImg.enabled;
				if (enabled)
				{
					rImg.enabled = false;
				}
				text2 = GameRoot.inst.Setting.Get_Key_Describe(text);
				bool flag3 = text2 != string.Empty;
				if (flag3)
				{
					text2 = text2 + "  " + Language.GetLang(3014, new object[0]);
				}
				tText.text = text2;
			}
		}
	}

	// Token: 0x0400284B RID: 10315
	public NetLiving hero;

	// Token: 0x0400284C RID: 10316
	private int hitType;

	// Token: 0x0400284D RID: 10317
	private float fshiziratenow = -1f;

	// Token: 0x0400284E RID: 10318
	private bool _rateCan = true;

	// Token: 0x0400284F RID: 10319
	private bool _zhunxingHideing = true;
}
