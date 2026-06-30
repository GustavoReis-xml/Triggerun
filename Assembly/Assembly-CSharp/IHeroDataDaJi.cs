using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class IHeroDataDaJi : IHeroData
{
	// Token: 0x060015FA RID: 5626 RVA: 0x000CD578 File Offset: 0x000CB778
	public void initSound(Transform _dun, NetLiving nhero)
	{
		this.hero = nhero;
		this.dun = _dun;
		this._playerShieldSoundContainer = new GameObject("_playerShieldSoundContainer");
		this._playerShieldSoundContainer.transform.parent = this.dun;
		this._playerShieldSoundContainer.transform.localPosition = Vector3.zero;
		this._playerShieldSoundContainer.transform.localEulerAngles = Vector3.zero;
		this.LoadDaJiShieldContinue();
		this.LoadDaJiShieldBreak();
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000CD5F8 File Offset: 0x000CB7F8
	public void LoadDaJiShieldContinue()
	{
		string text = "skill_daji_sheild_loop";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadDaJiShieldContinueSoundPrefab), 0);
		}
		else
		{
			this.LoadDaJiShieldContinueSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x000CD65C File Offset: 0x000CB85C
	private void LoadDaJiShieldContinueSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && (int)sStyle == this.hero.loadID && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			bool flag2 = this._playerShieldSoundContainer == null;
			if (flag2)
			{
				Debug.LogError(string.Format("空盾C:{0},{1}", this.dun == null, this.hero.playerHaveCreate));
			}
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.loop = true;
			component.transform.parent = this._playerShieldSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._DaJiShieldLoop = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x000CD774 File Offset: 0x000CB974
	public void PlayDaJiShieldContinueSound(int type)
	{
		bool flag = !(this._DaJiShieldLoop == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			if (type != 0)
			{
				if (type == 1)
				{
					this._DaJiShieldLoop.Stop();
				}
			}
			else
			{
				this._DaJiShieldLoop.Play();
			}
		}
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000CD7E4 File Offset: 0x000CB9E4
	public void LoadDaJiShieldBreak()
	{
		string text = "skill_daji_sheild_broken";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadDaJiShieldBreakSoundPrefab), 0);
		}
		else
		{
			this.LoadDaJiShieldBreakSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000CD848 File Offset: 0x000CBA48
	private void LoadDaJiShieldBreakSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && (int)sStyle == this.hero.loadID && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			bool flag2 = this._playerShieldSoundContainer == null;
			if (flag2)
			{
				Debug.LogError(string.Format("空盾B:{0},{1}", this.dun == null, this.hero.playerHaveCreate));
			}
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._playerShieldSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._DaJiShieldBreak = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000CD958 File Offset: 0x000CBB58
	public void PlayDaJiShieldBreakSound()
	{
		bool flag = !(this._DaJiShieldBreak == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._DaJiShieldBreak.Play();
		}
	}

	// Token: 0x04001110 RID: 4368
	public GameObject _playerShieldSoundContainer;

	// Token: 0x04001111 RID: 4369
	public const string DaJiShieldOpenContinue = "skill_daji_sheild_loop";

	// Token: 0x04001112 RID: 4370
	public const string DaJiShieldBreak = "skill_daji_sheild_broken";

	// Token: 0x04001113 RID: 4371
	private AudioSource _DaJiShieldLoop;

	// Token: 0x04001114 RID: 4372
	private AudioSource _DaJiShieldBreak;

	// Token: 0x04001115 RID: 4373
	private NetLiving hero;

	// Token: 0x04001116 RID: 4374
	private Transform dun;
}
