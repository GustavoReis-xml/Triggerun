using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class HeroZhurongMe : HeroBaseMe
{
	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06001566 RID: 5478 RVA: 0x000C8E94 File Offset: 0x000C7094
	// (set) Token: 0x06001567 RID: 5479 RVA: 0x000C8EAC File Offset: 0x000C70AC
	public GameObject Stunt_Kill_Audio_Object
	{
		get
		{
			return this._Stunt_Kill_Audio_Object;
		}
		set
		{
			this._Stunt_Kill_Audio_Object = value;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06001568 RID: 5480 RVA: 0x000C8EB8 File Offset: 0x000C70B8
	// (set) Token: 0x06001569 RID: 5481 RVA: 0x000C8ED0 File Offset: 0x000C70D0
	public AudioSource Stunt_Kill_Audio_Source
	{
		get
		{
			return this._Stunt_Kill_Audio_Source;
		}
		set
		{
			this._Stunt_Kill_Audio_Source = value;
		}
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000C8EDC File Offset: 0x000C70DC
	public void OnApplicationFocus(bool focus)
	{
		bool flag = !focus && base.hero.GetAnimatorInteger("AttackState") != 0;
		if (flag)
		{
			base.hero.SetAnimator("AttackState", 2, 0, 2);
		}
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000C8F24 File Offset: 0x000C7124
	protected override void Update()
	{
		base.Update();
		bool flag = HeroPlayer.isMouseShow && !ChatManager.GetInstance().HuDongUIOpen && base.hero.GetAnimatorInteger("AttackState") != 0;
		if (flag)
		{
			base.hero.SetAnimator("AttackState", 2, 0, 2);
		}
		bool flag2 = Input_Manager.Get_Button_Up("WeaponFirst") && base.hero.GetAnimatorInteger("AttackState") != 0;
		if (flag2)
		{
			base.hero.SetAnimator("AttackState", 2, 0, 2);
		}
		bool flag3 = this.InputCheck() && !base.hero.wInput.notInput && Input_Manager.Get_Button_Down("superskill");
		if (flag3)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x000C8FFC File Offset: 0x000C71FC
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
		bool flag = m_bool && base.hero.GetAnimatorBool("SkillSuper");
		if (flag)
		{
			base.hero.SetAnimator("SkillSuper", 0, false, 2);
		}
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x000C9048 File Offset: 0x000C7248
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		base.hero.SetAnimator("SkillSuper", 0, true, 2);
		base.hero.bePlayActive = false;
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.StartCoroutine(this.SuperFinish());
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000C90B4 File Offset: 0x000C72B4
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(0.7f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x000C90C4 File Offset: 0x000C72C4
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		base.hero.SetAnimator("SkillSuper", 0, false, 2);
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000C9124 File Offset: 0x000C7324
	public override void OnDeath()
	{
		base.OnDeath();
		SkillZRFireBallMe component = base.gameObject.GetComponent<SkillZRFireBallMe>();
		component.OneSkillFinish();
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000C914E File Offset: 0x000C734E
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.Load_Stunt_Kill_Sound();
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000C9160 File Offset: 0x000C7360
	public override void NpcKill(Npc_Entity npc)
	{
		base.NpcKill(npc);
		bool flag = npc.Stunt >= 1;
		if (flag)
		{
			this.Play_Stunt_Kill_Sound();
		}
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x000C9190 File Offset: 0x000C7390
	public void Load_Stunt_Kill_Sound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[857];
		GameRoot.LoadAsset("SoundRes/" + preloadData.sUrl + ".sds", new fResourceManager.DownFinishDelegate(this.Load_Stunt_Kill_Sound_Complete), null);
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x000C91DC File Offset: 0x000C73DC
	public void Load_Stunt_Kill_Sound_Complete(WWW _WWW, object _Parameters)
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
			this.Stunt_Kill_Audio_Object = (GameObject)UnityEngine.Object.Instantiate(original);
			this.Stunt_Kill_Audio_Object.name = "Zhurong_Stunt_Kill";
			this.Stunt_Kill_Audio_Object.transform.parent = base.hero._playerSoundContainer.transform;
			this.Stunt_Kill_Audio_Object.transform.localPosition = Vector3.zero;
			this.Stunt_Kill_Audio_Object.transform.localEulerAngles = Vector3.zero;
			this.Stunt_Kill_Audio_Object.transform.localScale = Vector3.one;
			this.Stunt_Kill_Audio_Source = this.Stunt_Kill_Audio_Object.GetComponent<AudioSource>();
			this.Stunt_Kill_Audio_Source.playOnAwake = false;
			this.Stunt_Kill_Audio_Source.Stop();
			CSoundManager.Instance.SetTalkSoundMixer(this.Stunt_Kill_Audio_Source, 0, base.hero);
		}
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000C92F0 File Offset: 0x000C74F0
	public void Play_Stunt_Kill_Sound()
	{
		bool flag = !(this.Stunt_Kill_Audio_Source == null);
		if (flag)
		{
			this.Stunt_Kill_Audio_Source.Play();
			base.hero.playerSound.ExeptStopTalkSounds(this.Stunt_Kill_Audio_Source);
		}
	}

	// Token: 0x04001070 RID: 4208
	private GameObject _Stunt_Kill_Audio_Object;

	// Token: 0x04001071 RID: 4209
	private AudioSource _Stunt_Kill_Audio_Source;
}
