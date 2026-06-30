using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class HeroZhurongIt : HeroBaseIt
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x0600155C RID: 5468 RVA: 0x000C8C58 File Offset: 0x000C6E58
	// (set) Token: 0x0600155D RID: 5469 RVA: 0x000C8C70 File Offset: 0x000C6E70
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

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x000C8C7C File Offset: 0x000C6E7C
	// (set) Token: 0x0600155F RID: 5471 RVA: 0x000C8C94 File Offset: 0x000C6E94
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

	// Token: 0x06001560 RID: 5472 RVA: 0x000C8C9E File Offset: 0x000C6E9E
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		this.Load_Stunt_Kill_Sound();
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000C8CAF File Offset: 0x000C6EAF
	public override void NpcKill(Npc_Entity npc)
	{
		base.NpcKill(npc);
		this.Play_Stunt_Kill_Sound();
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000C8CC4 File Offset: 0x000C6EC4
	public void Load_Stunt_Kill_Sound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[857];
		GameRoot.LoadAsset("SoundRes/" + preloadData.sUrl + ".sds", new fResourceManager.DownFinishDelegate(this.Load_Stunt_Kill_Sound_Complete), null);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000C8D10 File Offset: 0x000C6F10
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

	// Token: 0x06001564 RID: 5476 RVA: 0x000C8E24 File Offset: 0x000C7024
	public void Play_Stunt_Kill_Sound()
	{
		bool flag = !(this.Stunt_Kill_Audio_Source == null) && (this._hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this.Stunt_Kill_Audio_Source.Play();
			base.hero.playerSound.ExeptStopTalkSounds(this.Stunt_Kill_Audio_Source);
		}
	}

	// Token: 0x0400106E RID: 4206
	private GameObject _Stunt_Kill_Audio_Object;

	// Token: 0x0400106F RID: 4207
	private AudioSource _Stunt_Kill_Audio_Source;
}
