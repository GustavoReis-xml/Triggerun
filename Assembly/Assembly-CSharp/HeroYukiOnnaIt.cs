using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class HeroYukiOnnaIt : HeroBaseIt
{
	// Token: 0x170002AA RID: 682
	// (get) Token: 0x0600153A RID: 5434 RVA: 0x000C83B8 File Offset: 0x000C65B8
	// (set) Token: 0x0600153B RID: 5435 RVA: 0x000C83D0 File Offset: 0x000C65D0
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

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x0600153C RID: 5436 RVA: 0x000C83DC File Offset: 0x000C65DC
	// (set) Token: 0x0600153D RID: 5437 RVA: 0x000C83F4 File Offset: 0x000C65F4
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

	// Token: 0x0600153E RID: 5438 RVA: 0x000C83FE File Offset: 0x000C65FE
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.Load_Stunt_Kill_Sound();
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000C840F File Offset: 0x000C660F
	public override void NpcKill(Npc_Entity npc)
	{
		base.NpcKill(npc);
		this.Play_Stunt_Kill_Sound();
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000C8421 File Offset: 0x000C6621
	public void Load_Stunt_Kill_Sound()
	{
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x000C8424 File Offset: 0x000C6624
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

	// Token: 0x06001542 RID: 5442 RVA: 0x000C8538 File Offset: 0x000C6738
	public void Play_Stunt_Kill_Sound()
	{
		bool flag = !(this.Stunt_Kill_Audio_Source == null) && (this._hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this.Stunt_Kill_Audio_Source.Play();
			base.hero.playerSound.ExeptStopTalkSounds(this.Stunt_Kill_Audio_Source);
		}
	}

	// Token: 0x04001063 RID: 4195
	private GameObject _Stunt_Kill_Audio_Object;

	// Token: 0x04001064 RID: 4196
	private AudioSource _Stunt_Kill_Audio_Source;
}
