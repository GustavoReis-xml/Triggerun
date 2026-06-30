using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class HeroYukiOnnaMe : HeroBaseMe
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06001544 RID: 5444 RVA: 0x000C85A8 File Offset: 0x000C67A8
	// (set) Token: 0x06001545 RID: 5445 RVA: 0x000C8650 File Offset: 0x000C6850
	public Material Material_Component
	{
		get
		{
			bool flag = this._Material_Component == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.hero.thisModel, "shuijing");
				bool flag2 = transform == null;
				if (flag2)
				{
					return null;
				}
				Renderer component = transform.GetComponent<Renderer>();
				Material[] sharedMaterials = component.sharedMaterials;
				Material[] array = sharedMaterials;
				foreach (Material material in array)
				{
					bool flag3 = material.name == "shuijin (Instance)";
					if (flag3)
					{
						this._Material_Component = material;
					}
				}
			}
			return this._Material_Component;
		}
		set
		{
			this._Material_Component = value;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06001546 RID: 5446 RVA: 0x000C865C File Offset: 0x000C685C
	// (set) Token: 0x06001547 RID: 5447 RVA: 0x000C8674 File Offset: 0x000C6874
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

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06001548 RID: 5448 RVA: 0x000C8680 File Offset: 0x000C6880
	// (set) Token: 0x06001549 RID: 5449 RVA: 0x000C8698 File Offset: 0x000C6898
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

	// Token: 0x0600154A RID: 5450 RVA: 0x000C86A4 File Offset: 0x000C68A4
	protected override void Update()
	{
		base.Update();
		bool flag = this.InputCheck() && !base.hero.wInput.notInput && Input_Manager.Get_Button_Down("superskill") && base.hero.tChar.onGround && base.hero.superSkill(null);
		if (flag)
		{
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000C8708 File Offset: 0x000C6908
	public override void SuperSkillDo(object parm)
	{
		this.Release_Musth_State = true;
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.SuperSkillDo(parm);
		base.hero.changeCameraState(CameraState.ThirdState, null, "playeraction");
		base.StartCoroutine(this.Wait_Create_Npc());
		base.hero.SetAnimator("Skill_Musth", 0, true, 2);
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000C8785 File Offset: 0x000C6985
	public IEnumerator Wait_Create_Npc()
	{
		yield return new WaitForSeconds(1.3f);
		base.hero.Npc_Handle_Component.Create_Npc(base.hero, -1, 1013, base.hero.UserControl.transform.position, base.hero.UserControl.transform.eulerAngles, Vector3.one, 1079, Vector3.zero, string.Empty);
		base.hero.OnStartSuperSkill(null);
		yield break;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000C8794 File Offset: 0x000C6994
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		base.StartCoroutine(this.Wait_SuperSkillFinish());
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000C87AB File Offset: 0x000C69AB
	public IEnumerator Wait_SuperSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.Release_Musth_State = false;
		base.StartCoroutine(this.Wait_Handle());
		bool isDead = base.hero.isDead;
		if (isDead)
		{
			base.hero.DelayDeath(base.hero.playerCommon.m_nDeathAttackId);
			yield break;
		}
		CameraControl.Instance.InitCameraHero(base.hero);
		base.hero.changeCameraState(CameraState.FirstState, null, "playeraction");
		yield break;
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000C87BA File Offset: 0x000C69BA
	private IEnumerator Wait_Handle()
	{
		yield return new WaitForSeconds(0.5f);
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		base.hero.bePlayActive = true;
		yield break;
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000C87C9 File Offset: 0x000C69C9
	public override void OnDeath()
	{
		base.OnDeath();
		base.hero.SetAnimator("Skill_Musth", 0, false, 2);
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x000C87EC File Offset: 0x000C69EC
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.Load_Stunt_Kill_Sound();
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000C8800 File Offset: 0x000C6A00
	public override void NpcKill(Npc_Entity npc)
	{
		base.NpcKill(npc);
		bool flag = npc.Stunt >= 1;
		if (flag)
		{
			this.Play_Stunt_Kill_Sound();
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000C882F File Offset: 0x000C6A2F
	public void Load_Stunt_Kill_Sound()
	{
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000C8834 File Offset: 0x000C6A34
	public void Load_Stunt_Kill_Sound_Complete(WWW _WWW, object _Parameters)
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
			this.Stunt_Kill_Audio_Object = (GameObject)UnityEngine.Object.Instantiate(original);
			this.Stunt_Kill_Audio_Object.name = "YukiOnna_Stunt_Kill";
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

	// Token: 0x06001555 RID: 5461 RVA: 0x000C8948 File Offset: 0x000C6B48
	public void Play_Stunt_Kill_Sound()
	{
		bool flag = !(this.Stunt_Kill_Audio_Source == null);
		if (flag)
		{
			this.Stunt_Kill_Audio_Source.Play();
			base.hero.playerSound.ExeptStopTalkSounds(this.Stunt_Kill_Audio_Source);
		}
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000C898E File Offset: 0x000C6B8E
	public override void BreakSupperSkill()
	{
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000C8994 File Offset: 0x000C6B94
	public override void Stun(bool m_bool)
	{
		bool release_Musth_State = this.Release_Musth_State;
		if (release_Musth_State)
		{
			base.hero.wInput.notInput = this.Release_Musth_State;
			base.hero.charMove.notMove = this.Release_Musth_State;
			base.hero.bePlayActive = false;
		}
	}

	// Token: 0x04001065 RID: 4197
	private Material _Material_Component;

	// Token: 0x04001066 RID: 4198
	private GameObject _Stunt_Kill_Audio_Object;

	// Token: 0x04001067 RID: 4199
	private AudioSource _Stunt_Kill_Audio_Source;
}
