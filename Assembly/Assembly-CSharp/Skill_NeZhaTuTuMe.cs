using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class Skill_NeZhaTuTuMe : SkillBaseMe
{
	// Token: 0x060029A5 RID: 10661 RVA: 0x0016F190 File Offset: 0x0016D390
	public override bool OneSkillCanDo()
	{
		base.hero.wc.UpdateFireTime(0f);
		bool flag = base.hero.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			result = (!flag2 && base.OneSkillCanDo());
		}
		return result;
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x0016F1F8 File Offset: 0x0016D3F8
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
		this.LoadSkillBeginSound();
		this.LoadSkillBeginSounds();
		this.npcLayer = (1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			int num = 2;
			this.force1 = "force" + num.ToString() + "_player";
			this.force2 = "force" + num.ToString() + "_npc";
		}
		else
		{
			int num2 = 1;
			this.force1 = "force" + num2.ToString() + "_player";
			this.force2 = "force" + num2.ToString() + "_npc";
		}
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x0016F2D8 File Offset: 0x0016D4D8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("skill_shift", 0, true, 2);
		this.Super_Jump_Start(__target);
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x0016F308 File Offset: 0x0016D508
	private void Super_Jump_Start(object _target)
	{
		this._Target = _target;
		this.layerFade = false;
		((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
		((CWeaponControlMe)base.hero.wc).WeaponActiveBySkill(5, base.iSkillIndex);
		((CWeaponControlMe)base.hero.wc).WeaponDownKey(5);
		((CWeaponControlMe)base.hero.wc).WeaponFire(5, "0", (Vector3)this._Target, base.hero.hitGamaObj);
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.up * this.upForce, 0.1f, false, 1000f, 1000f);
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.charMove.canJump = false;
		base.hero.wc.UpdateFireTime(1f);
		this.canEnd = true;
		this.canGo = true;
		this.skillTime = Time.time;
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x0016F428 File Offset: 0x0016D628
	private void Update()
	{
		bool flag = this.canGo && Input.GetMouseButtonDown(0) && !this.OnSkillE();
		if (flag)
		{
			this.dic.Clear();
			this.canGo = false;
			this.layerFade = true;
			base.hero.wInput.notInput = true;
			((HeroNeZhaMe)base.hero.heroBase).IgnoreLayer(true);
			this.layerTime = Time.time;
			base.hero.SetAnimator("shift_go", 0, true, 2);
			this.vSpeed = Vector3.zero;
			Vector3 forward = CameraControl.Instance.nowTarget.forward;
			this.vSpeed = (forward * 1f).normalized;
			((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.zero * this.upForce, 0f, false, 1000f, 1000f);
			((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
			((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity(this.vSpeed * this.goSpeed, this.goTime, true);
			((HeroNeZhaMe)base.hero.heroBase).NeZhaShiftShow(true);
			this.PlayNeZhaAud();
		}
		bool flag2 = this.canEnd && base.hero.tChar.onGround && Time.time - this.skillTime > 0.05f;
		if (flag2)
		{
			this.canGo = false;
			bool flag3 = !this.layerFade;
			if (flag3)
			{
				this.OneSkillFinish();
			}
			this.canEnd = false;
		}
		bool flag4 = this.layerFade;
		if (flag4)
		{
			bool flag5 = Time.time - this.layerTime > this.goTime;
			if (flag5)
			{
				this.OneSkillFinish();
			}
			bool onGround = base.hero.tChar.onGround;
			if (onGround)
			{
				((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
			}
		}
		bool flag6 = this.dickeys.Count == 0;
		if (!flag6)
		{
			int key = this.dickeys[0];
			this.dickeys.RemoveAt(0);
			bool flag7 = this.dic.ContainsKey(key);
			if (flag7)
			{
				NZSData nzsdata = this.dic[key];
				this.dic.Remove(key);
				bool flag8 = nzsdata.obj != null;
				if (flag8)
				{
					base.RealShoot(nzsdata.at, nzsdata.obj);
				}
			}
		}
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x0016F6DC File Offset: 0x0016D8DC
	public bool OnSkillE()
	{
		return base.hero.GetAnimatorBool("skill_E");
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x0016F708 File Offset: 0x0016D908
	public override void OneSkillFinish()
	{
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveForce(Vector3.zero * 0f, 0f, false, 1000f, 1000f);
		base.OneSkillFinish();
		base.hero.SetAnimator("shift_go", 0, false, 2);
		base.hero.SetAnimator("skill_shift", 0, false, 2);
		base.hero.charMove.canJump = true;
		base.hero.tChar.m_MoveByAnimator = true;
		base.StartCoroutine(this.NoTrigger());
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x0016F7B3 File Offset: 0x0016D9B3
	private IEnumerator NoTrigger()
	{
		yield return new WaitForSeconds(0.2f);
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			this.dickeys.Clear();
			this.dickeys = new List<int>(this.dic.Keys);
			base.hero.wInput.notInput = false;
			this.layerFade = false;
			((HeroNeZhaMe)base.hero.heroBase).NeZhaShiftShow(false);
			((HeroNeZhaMe)base.hero.heroBase).IgnoreLayer(false);
		}
		yield break;
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x0016F7C4 File Offset: 0x0016D9C4
	private void OnTriggerEnter(Collider other)
	{
		bool flag = !this.layerFade || (other.gameObject.layer != LayerMask.NameToLayer(this.force1) && other.gameObject.layer != LayerMask.NameToLayer(this.force2));
		if (!flag)
		{
			int num = 1 << other.gameObject.layer;
			bool flag2 = (num & this.npcLayer) != 0;
			NetLiving netLiving;
			if (flag2)
			{
				Npc_Data component = other.gameObject.GetComponent<Npc_Data>();
				netLiving = component.Entity_Component;
			}
			else
			{
				GameObject gameObject = other.transform.root.gameObject;
				PublicInfo component2 = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component2 == null;
				if (flag3)
				{
					Debug.LogError("发现可以攻击的东西没有PublicINfo, " + other.name + "," + gameObject.name);
					return;
				}
				netLiving = component2.NetObject;
			}
			bool flag4 = !this.dic.ContainsKey(netLiving.getNetid());
			if (flag4)
			{
				NZSData nzsdata = new NZSData();
				nzsdata.obj = netLiving.defaultMesh;
				nzsdata.at = other.gameObject.transform.position;
				this.dic.Add(netLiving.getNetid(), nzsdata);
			}
		}
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x0016F90C File Offset: 0x0016DB0C
	private void LoadSkillBeginSound()
	{
		bool flag = base.iSkillIndex == 1181;
		if (flag)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1920];
			string text = preloadData.sUrl;
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefab), 0);
			}
			else
			{
				this.LoadSkillBeginSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x0016F99C File Offset: 0x0016DB9C
	private void LoadSkillBeginSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this._hero != null && !this._hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.audioTuTu = component;
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, base.hero);
		}
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x0016FA57 File Offset: 0x0016DC57
	public override void OneSkillInput()
	{
		base.OneSkillInput();
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x0016FA61 File Offset: 0x0016DC61
	public override void OneSkillActionUpdate(ArrayList list)
	{
		this.PlayNeZhaAud();
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x0016FA6C File Offset: 0x0016DC6C
	private void PlayNeZhaAud()
	{
		bool flag = (this._hero.isCopy || !CFilmManager.Instance.playing) && !(this.audioTuTu == null);
		if (flag)
		{
			this.audioTuTu.Play();
			bool flag2 = !(this.audioTuTub == null);
			if (flag2)
			{
				this.audioTuTub.Play();
			}
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x0016FAD8 File Offset: 0x0016DCD8
	private void LoadSkillBeginSounds()
	{
		bool flag = base.iSkillIndex == 1181;
		if (flag)
		{
			string text = "skill_nezha_strike_rush";
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefabb), 0);
			}
			else
			{
				this.LoadSkillBeginSoundPrefabb(resWWW, 0);
			}
		}
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x0016FB50 File Offset: 0x0016DD50
	private void LoadSkillBeginSoundPrefabb(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this._hero != null && !this._hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.audioTuTub = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x04002338 RID: 9016
	public bool canGo;

	// Token: 0x04002339 RID: 9017
	private bool canEnd;

	// Token: 0x0400233A RID: 9018
	private bool layerFade;

	// Token: 0x0400233B RID: 9019
	private int npcLayer;

	// Token: 0x0400233C RID: 9020
	private Vector3 vSpeed;

	// Token: 0x0400233D RID: 9021
	private float skillTime;

	// Token: 0x0400233E RID: 9022
	private float layerTime;

	// Token: 0x0400233F RID: 9023
	private float goTime = 0.3f;

	// Token: 0x04002340 RID: 9024
	private float goSpeed = 60f;

	// Token: 0x04002341 RID: 9025
	private float upForce = 150f;

	// Token: 0x04002342 RID: 9026
	private Dictionary<int, NZSData> dic = new Dictionary<int, NZSData>();

	// Token: 0x04002343 RID: 9027
	private List<int> dickeys = new List<int>();

	// Token: 0x04002344 RID: 9028
	private object _Target;

	// Token: 0x04002345 RID: 9029
	private AudioSource audioTuTu;

	// Token: 0x04002346 RID: 9030
	private AudioSource audioTuTub;

	// Token: 0x04002347 RID: 9031
	private string force1;

	// Token: 0x04002348 RID: 9032
	private string force2;
}
