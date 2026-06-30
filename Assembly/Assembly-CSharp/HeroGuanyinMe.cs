using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class HeroGuanyinMe : HeroBaseMe
{
	// Token: 0x060011CA RID: 4554 RVA: 0x000A6F14 File Offset: 0x000A5114
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "TeamCenterOne");
		base.hero.screentarget.targetAngle = 0.99f;
		base.hero.screentarget.targetDis = 100f;
		this.LoadFlyDownSound();
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000A6F70 File Offset: 0x000A5170
	public void LoadFlyDownSound()
	{
		string url = "SoundRes/skillvoice_luoshen_slowfall.sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFlyDownSoundPrefab), "start");
		}
		else
		{
			this.LoadFlyDownSoundPrefab(resWWW, "start");
		}
		url = "SoundRes/skillvoice_luoshen_slowfall_loop.sds";
		resWWW = GameRoot.GetResWWW(url);
		bool flag2 = resWWW == null;
		if (flag2)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFlyDownSoundPrefab), "loop");
		}
		else
		{
			this.LoadFlyDownSoundPrefab(resWWW, "loop");
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000A7000 File Offset: 0x000A5200
	private void LoadFlyDownSoundPrefab(WWW www, object sStyle)
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
			component.transform.localRotation = Quaternion.Euler(Vector3.zero);
			bool flag2 = sStyle.ToString() == "start";
			if (flag2)
			{
				this.flydownStart = component;
			}
			bool flag3 = sStyle.ToString() == "loop";
			if (flag3)
			{
				this.flydownLoop = component;
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000A7104 File Offset: 0x000A5304
	public void XingTaiChange(int ntype)
	{
		bool flag = !this.isChgWeapon && !base.hero.GetAnimatorBool("ChangeWeapon") && base.hero.playerCommon.m_nWeaponGroup != ntype;
		if (flag)
		{
			this.isChgWeapon = true;
			base.hero.wc.UpdateFireTime(1f);
			base.hero.ChangeWeaponGroup(ntype);
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000A7178 File Offset: 0x000A5378
	public override void ChangeWeaponGroup(int xingtai)
	{
		base.ChangeWeaponGroup(xingtai);
		int animatorInteger = base.hero.GetAnimatorInteger("Weapon");
		bool flag = animatorInteger != xingtai;
		if (flag)
		{
			base.hero.SetAnimator("ChangeWeapon", 0, true, 1);
			base.StartCoroutine(this.WaitAnimation(0.7f));
		}
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x000A71D7 File Offset: 0x000A53D7
	private IEnumerator WaitAnimation(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.isChgWeapon = false;
		yield break;
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000A71F0 File Offset: 0x000A53F0
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
		base.hero.SetAnimator("SkillSuper", 0, true, 1);
		base.hero.bePlayActive = false;
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.StartCoroutine(this.SuperFinish());
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000A7263 File Offset: 0x000A5463
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(1.333f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000A7272 File Offset: 0x000A5472
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000A72AC File Offset: 0x000A54AC
	protected override void Update()
	{
		base.Update();
		int deadMemberNum = this.GetDeadMemberNum();
		bool flag = deadMemberNum != this.canReviveNum;
		if (flag)
		{
			this.canReviveNum = deadMemberNum;
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.OnFriendDeadNum(this.canReviveNum);
			}
		}
		bool flag3 = !this.InputCheck();
		if (!flag3)
		{
			bool flag4 = (Input_Manager.Get_Button_Up("WeaponFirst") || Input_Manager.Get_Button_Up("WeaponSec")) && base.hero.GetAnimatorInteger("AttackState") != 0;
			if (flag4)
			{
				base.hero.SetAnimator("AttackState", 2, 0, 1);
			}
			bool notInput = base.hero.wInput.notInput;
			if (!notInput)
			{
				bool flag5 = Input_Manager.Get_Axis("Jump") > 0f;
				if (flag5)
				{
					bool flag6 = base.hero.tChar.rig.velocity.y < 0f;
					if (flag6)
					{
						this.OnFlyDown();
					}
				}
				else
				{
					bool flag7 = this.flyDownType == 2;
					if (flag7)
					{
						this.pressSpaceTime = 0f;
						this.OnFlyDown();
					}
				}
				bool flag8 = Input_Manager.Get_Button("Key_Use_Weapon_1");
				if (flag8)
				{
					this.XingTaiChange(0);
				}
				bool flag9 = Input_Manager.Get_Button("Key_Use_Weapon_2");
				if (flag9)
				{
					this.XingTaiChange(1);
				}
				bool flag10 = Input_Manager.Get_Button_Down("superskill") && deadMemberNum > 0;
				if (flag10)
				{
					base.hero.superSkill(this.szReliveId);
				}
			}
		}
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000A7458 File Offset: 0x000A5658
	public void SetDeadMember(NetLiving gp, int ntype)
	{
		bool flag = !gp.bePlayer;
		if (!flag)
		{
			bool flag2 = this.szDeadMember.Contains(gp);
			bool flag3 = ntype == 1;
			if (flag3)
			{
				bool flag4 = !flag2;
				if (flag4)
				{
					this.szDeadMember.Add(gp);
				}
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					this.szDeadMember.Remove(gp);
				}
			}
		}
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000A74BC File Offset: 0x000A56BC
	private int GetDeadMemberNum()
	{
		int num = 0;
		bool flag = this.szDeadMember.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = !base.hero.playerDazhao.canDo;
			if (flag2)
			{
				result = num;
			}
			else
			{
				bool isDead = base.hero.isDead;
				if (isDead)
				{
					result = num;
				}
				else
				{
					this.szReliveId.Clear();
					for (int i = 0; i < this.szDeadMember.Count; i++)
					{
						bool flag3 = this.szDeadMember[i] != null && !(this.szDeadMember[i].thisModel == null);
						if (flag3)
						{
							int num2 = Mathf.FloorToInt(Vector3.Distance(this.szDeadMember[i].bodyCollider.transform.position, base.transform.position));
							bool flag4 = num2 <= 15;
							if (flag4)
							{
								this.szReliveId.Add(this.szDeadMember[i].getNetid());
								ArrayList arrayList = new ArrayList();
								arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].bodyCollider.transform.position.x * 100f));
								arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].bodyCollider.transform.position.y * 100f));
								arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].bodyCollider.transform.position.z * 100f));
								this.szReliveId.Add(arrayList);
								num++;
							}
						}
					}
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x000A76C0 File Offset: 0x000A58C0
	private void OnFlyOver()
	{
		this.flyDownType = 0;
		this.pressSpaceTime = 0f;
		base.hero.SetAnimator("IsFlyDown", 0, false, 1);
		bool flag = this.flydownLoop != null && this.flydownLoop.isPlaying;
		if (flag)
		{
			this.flydownLoop.Stop();
		}
		bool flag2 = this.flydownStart != null && this.flydownStart.isPlaying;
		if (flag2)
		{
			this.flydownStart.Stop();
		}
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x000A7758 File Offset: 0x000A5958
	private void OnFlyDown()
	{
		bool flag = base.hero.tChar.GroundDistance < 0.3f;
		if (flag)
		{
			this.OnFlyOver();
		}
		else
		{
			bool flag2 = this.flyDownType == 0;
			if (flag2)
			{
				this.flyDownType = 1;
				((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(true);
			}
			else
			{
				this.pressSpaceTime += Time.deltaTime;
				bool flag3 = this.pressSpaceTime <= 0.1f;
				if (!flag3)
				{
					bool flag4 = this.flyDownType == 1;
					if (flag4)
					{
						this.flyDownType = 2;
						base.hero.SetAnimator("IsFlyDown", 0, true, 1);
						bool flag5 = this.flydownStart != null && !this.flydownStart.isPlaying;
						if (flag5)
						{
							this.flydownStart.loop = false;
							this.flydownStart.Play();
						}
					}
					this.pressSpaceTime = 0f;
					((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.up * 20f, 0.1f, true, 1000f, 1000f);
					bool flag6 = this.flyDownType == 2 && this.flydownStart != null && !this.flydownStart.isPlaying && this.flydownLoop != null && !this.flydownLoop.isPlaying;
					if (flag6)
					{
						this.flydownLoop.loop = true;
						this.flydownLoop.Play();
					}
				}
			}
		}
	}

	// Token: 0x04000E21 RID: 3617
	private bool isChgWeapon;

	// Token: 0x04000E22 RID: 3618
	private int flyDownType;

	// Token: 0x04000E23 RID: 3619
	private float pressSpaceTime;

	// Token: 0x04000E24 RID: 3620
	public bool superDoing;

	// Token: 0x04000E25 RID: 3621
	private int canReviveNum;

	// Token: 0x04000E26 RID: 3622
	private List<NetLiving> szDeadMember = new List<NetLiving>();

	// Token: 0x04000E27 RID: 3623
	private ArrayList szReliveId = new ArrayList();

	// Token: 0x04000E28 RID: 3624
	private AudioSource flydownStart;

	// Token: 0x04000E29 RID: 3625
	private AudioSource flydownLoop;
}
