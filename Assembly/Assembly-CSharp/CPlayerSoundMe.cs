using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class CPlayerSoundMe : CPlayerSound
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x0003D734 File Offset: 0x0003B934
	public override void initPlayerSound(NetLiving user)
	{
		base.initPlayerSound(user);
		this.LoadJz_DaShangHaiSound();
		this.LoadJz_BaoTouSound();
		this.LoadJiZhongSound();
		this.LoadPinSiSound();
		this.LoadChangePinSiSound();
		this.LoadJiaXueSound();
		this.LoadMultiKillSound();
		this.LoadEnterSceneSound();
		this.LoadReviveInBaseSound();
		this.LoadUnbreakbleKillSound();
		this.LoadSniperSwitchSound();
		this.LoadRecoverySound();
		this.LoadAcedSound();
		this.LoadSkillCDFinishSound();
		this.LoadDaZhaoManSound();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
	private void Update()
	{
		bool flag = this.PlayRecHpCD >= 0f;
		if (flag)
		{
			this.PlayRecHpCD -= Time.deltaTime;
		}
		bool flag2 = this._JiaXueSound != null && this._JiaXueSound.isPlaying;
		if (flag2)
		{
			float num = (float)(DateTime.Now - this.tJiaXue).TotalMilliseconds;
			bool flag3 = num >= 1000f;
			if (flag3)
			{
				this._JiaXueSound.loop = false;
				this._JiaXueSound.Stop();
			}
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0003D852 File Offset: 0x0003BA52
	protected override void LoadHuDongStart()
	{
		base.LoadHuDongSound(true);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0003D860 File Offset: 0x0003BA60
	public void LoadJz_DaShangHaiSound()
	{
		string text = "zhandou_shanghaida";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadJz_DaShangHaiSoundPrefab), 0);
		}
		else
		{
			this.LoadJz_DaShangHaiSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
	private void LoadJz_DaShangHaiSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			this._damageBig = www;
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0003D904 File Offset: 0x0003BB04
	public void PlayJz_DaShangHaiSound()
	{
		bool flag = this._damageBig == null || Time.time < this.damageBigTime;
		if (!flag)
		{
			this.damageBigTime = Time.time + 0.1f;
			bool flag2 = true;
			AudioSource audioSource = null;
			for (int i = 0; i < this.listDamageBig.Count; i++)
			{
				audioSource = this.listDamageBig[i];
				bool flag3 = !(audioSource == null) && !audioSource.isPlaying;
				if (flag3)
				{
					flag2 = false;
					break;
				}
			}
			bool flag4 = flag2;
			if (flag4)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this._damageBig.assetBundle.LoadAsset(this._damageBig.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.parent = this.hero._playerSoundContainer.transform;
				component.transform.localPosition = Vector3.zero;
				this.listDamageBig.Add(component);
				audioSource = component;
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			}
			audioSource.Play();
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0003DA40 File Offset: 0x0003BC40
	public void LoadJz_BaoTouSound()
	{
		string text = "zhandou_baotou";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadJz_BaoTouSoundPrefab), 0);
		}
		else
		{
			this.LoadJz_BaoTouSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
	private void LoadJz_BaoTouSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			this._damageHead = www;
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
	public void PlayJz_BaoTouSound()
	{
		bool flag = this._damageHead == null || Time.time < this.damageHeadTime;
		if (!flag)
		{
			this.damageHeadTime = Time.time + 0.1f;
			bool flag2 = true;
			AudioSource audioSource = null;
			for (int i = 0; i < this.listDamageHead.Count; i++)
			{
				audioSource = this.listDamageHead[i];
				bool flag3 = !(audioSource == null) && !audioSource.isPlaying;
				if (flag3)
				{
					flag2 = false;
					break;
				}
			}
			bool flag4 = flag2;
			if (flag4)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this._damageHead.assetBundle.LoadAsset(this._damageHead.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.parent = this.hero._playerSoundContainer.transform;
				component.transform.localPosition = Vector3.zero;
				this.listDamageHead.Add(component);
				audioSource = component;
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			}
			audioSource.Play();
		}
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0003DC20 File Offset: 0x0003BE20
	public void LoadJiZhongSound()
	{
		string text = "zhandou_jisha";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadJiZhongSoundPrefab), 0);
		}
		else
		{
			this.LoadJiZhongSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0003DC84 File Offset: 0x0003BE84
	private void LoadJiZhongSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._JiZhongSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0003DD3C File Offset: 0x0003BF3C
	public void PlayJiZhongSound()
	{
		bool flag = !(this._JiZhongSound == null);
		if (flag)
		{
			this._JiZhongSound.Play();
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0003DD6C File Offset: 0x0003BF6C
	public void LoadPinSiSound()
	{
		string text = "zhandou_pinsijiemian";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadPinSiSoundPrefab), 0);
		}
		else
		{
			this.LoadPinSiSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
	private void LoadPinSiSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._PinSiSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0003DE88 File Offset: 0x0003C088
	public void PlayPinSiSound()
	{
		bool flag = !(this._PinSiSound == null);
		if (flag)
		{
			this._PinSiSound.loop = true;
			this._PinSiSound.Play();
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0003DEC4 File Offset: 0x0003C0C4
	public void StopPinSiSound()
	{
		bool flag = !(this._PinSiSound == null);
		if (flag)
		{
			this._PinSiSound.Stop();
		}
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
	private void LoadChangePinSiSound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._ChangePinSiSound = new Dictionary<int, AudioSource>();
		ArrayList pinsiSound = heroData.pinsiSound;
		for (int i = 0; i < pinsiSound.Count; i++)
		{
			string url = "SoundRes/" + (string)pinsiSound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadChangePinSiSoundPrefab), i.ToString());
			}
			else
			{
				this.LoadChangePinSiSoundPrefab(resWWW, i.ToString());
			}
		}
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0003DFAC File Offset: 0x0003C1AC
	private void LoadChangePinSiSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._ChangePinSiSound.ContainsKey(key);
			if (flag2)
			{
				this._ChangePinSiSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0003E090 File Offset: 0x0003C290
	public void PlayChangePinSiSound(int ntype)
	{
		bool flag = this._ChangePinSiSound.Count != 0 && this._ChangePinSiSound.ContainsKey(ntype);
		if (flag)
		{
			this._ChangePinSiSound[ntype].Play();
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0003E0D2 File Offset: 0x0003C2D2
	public override void PlayHurtSound(int soundType)
	{
		((CPlayerSoundMe)this.hero.playerSound).ShopChangePinSiSound();
		base.PlayHurtSound(soundType);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0003E0F3 File Offset: 0x0003C2F3
	public override void PlayDeadSound(int ntype)
	{
		((CPlayerSoundMe)this.hero.playerSound).ShopChangePinSiSound();
		base.PlayDeadSound(ntype);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0003E114 File Offset: 0x0003C314
	public void ShopChangePinSiSound()
	{
		bool flag = this._ChangePinSiSound.Count == 0;
		if (!flag)
		{
			foreach (AudioSource audioSource in this._ChangePinSiSound.Values)
			{
				bool flag2 = audioSource != null;
				if (flag2)
				{
					audioSource.Stop();
				}
			}
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0003E194 File Offset: 0x0003C394
	public void LoadJiaXueSound()
	{
		string text = "zhandou_jiaxue";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadJiaXueSoundPrefab), 0);
		}
		else
		{
			this.LoadJiaXueSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
	private void LoadJiaXueSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._JiaXueSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0003E2B0 File Offset: 0x0003C4B0
	public void PlayJiaXueSound()
	{
		bool flag = !(this._JiaXueSound == null);
		if (flag)
		{
			this.tJiaXue = DateTime.Now;
			bool flag2 = !this._JiaXueSound.isPlaying;
			if (flag2)
			{
				this._JiaXueSound.loop = true;
				this._JiaXueSound.Play();
			}
		}
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0003E30C File Offset: 0x0003C50C
	private void LoadMultiKillSound()
	{
		this.MultiKillSount = new Dictionary<int, AudioSource>();
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[719];
		this.multiKillUrl[0] = preloadData.sUrl;
		preloadData = fGameManager.mloadConfig.AllProLodaList[738];
		this.multiKillUrl[1] = preloadData.sUrl;
		preloadData = fGameManager.mloadConfig.AllProLodaList[721];
		this.multiKillUrl[2] = preloadData.sUrl;
		preloadData = fGameManager.mloadConfig.AllProLodaList[720];
		this.multiKillUrl[3] = preloadData.sUrl;
		for (int i = 0; i < 4; i++)
		{
			string url = "SoundRes/" + this.multiKillUrl[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadMultiKillSoundPrefeb), i);
			}
			else
			{
				this.LoadMultiKillSoundPrefeb(resWWW, i);
			}
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0003E41C File Offset: 0x0003C61C
	private void LoadMultiKillSoundPrefeb(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = (int)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this.MultiKillSount.ContainsKey(key);
			if (flag2)
			{
				this.MultiKillSount.Add(key, component);
			}
			CSoundManager.Instance.SetUISoundMixer(component, 20);
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0003E4F8 File Offset: 0x0003C6F8
	public void PlayMultiKillSound(int ntype)
	{
		bool flag = this.MultiKillSount.Count != 0;
		if (flag)
		{
			bool flag2 = ntype > 3;
			if (flag2)
			{
				ntype = 3;
			}
			bool flag3 = ntype > 0 && this.MultiKillSount.ContainsKey(this.lastPlayIndex) && this.MultiKillSount[this.lastPlayIndex].isPlaying;
			if (flag3)
			{
				this.MultiKillSount[this.lastPlayIndex].Stop();
			}
			bool flag4 = this.MultiKillSount.ContainsKey(ntype);
			if (flag4)
			{
				this.lastPlayIndex = ntype;
				this.MultiKillSount[ntype].Play();
			}
		}
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
	private void LoadEnterSceneSound()
	{
		this.b_playEnterSceneSound = false;
		this.FpsLoadedFinish(false);
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._EnterSceneSound = new Dictionary<int, AudioSource>();
		ArrayList enterSceneSound = heroData.EnterSceneSound;
		this.loadCount = enterSceneSound.Count;
		for (int i = 0; i < enterSceneSound.Count; i++)
		{
			string url = "SoundRes/" + (string)enterSceneSound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadEnterSceneSoundPrefab), i);
			}
			else
			{
				this.LoadEnterSceneSoundPrefab(resWWW, i);
			}
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0003E678 File Offset: 0x0003C878
	private void LoadEnterSceneSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || this.hero == null || this.hero.haveDestroy || !this.hero.playerHaveCreate;
		if (!flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._EnterSceneSound.ContainsKey(key);
			if (flag2)
			{
				this._EnterSceneSound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
			this.loadCount--;
			bool flag3 = this.loadCount == 0;
			if (flag3)
			{
				this.b_playEnterSceneSound = true;
				bool flag4 = this.b_FpsLoadedFinish;
				if (flag4)
				{
					this.PlayEnterSceneSound();
				}
			}
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
	public void PlayEnterSceneSound()
	{
		bool flag = this.hero != null && !this.hero.haveDestroy && this.hero.playerHaveCreate && !fGameManager.IsSingle && this._EnterSceneSound.Count != 0;
		if (flag)
		{
			int key = UnityEngine.Random.Range(0, this._EnterSceneSound.Count);
			bool flag2 = this._ReviveInBaseSound.ContainsKey(this.reviceSoundIndex);
			if (flag2)
			{
				this._ReviveInBaseSound[this.reviceSoundIndex].Stop();
			}
			bool flag3 = this._EnterSceneSound.ContainsKey(key) && this._EnterSceneSound[key] != null;
			if (flag3)
			{
				this._EnterSceneSound[key].Play();
				this.hero.playerSound.ExeptStopTalkSounds(this._EnterSceneSound[key]);
			}
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0003E894 File Offset: 0x0003CA94
	public bool CanPlayEnterSceneSound()
	{
		return this.b_playEnterSceneSound;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0003E8AC File Offset: 0x0003CAAC
	public void FpsLoadedFinish(bool b)
	{
		this.b_FpsLoadedFinish = b;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0003E8B8 File Offset: 0x0003CAB8
	private void LoadReviveInBaseSound()
	{
		this.reviceSoundIndex = 0;
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._ReviveInBaseSound = new Dictionary<int, AudioSource>();
		ArrayList reviveInBaseSound = heroData.ReviveInBaseSound;
		for (int i = 0; i < reviveInBaseSound.Count; i++)
		{
			string url = "SoundRes/" + (string)reviveInBaseSound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadReviveInBaseSoundPrefab), i.ToString());
			}
			else
			{
				this.LoadReviveInBaseSoundPrefab(resWWW, i.ToString());
			}
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0003E978 File Offset: 0x0003CB78
	private void LoadReviveInBaseSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._ReviveInBaseSound.ContainsKey(key);
			if (flag2)
			{
				this._ReviveInBaseSound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0003EA60 File Offset: 0x0003CC60
	public void PlayLoadReviveInBaseSound()
	{
		bool flag = this._ReviveInBaseSound.Count != 0;
		if (flag)
		{
			int key = UnityEngine.Random.Range(0, this._ReviveInBaseSound.Count);
			bool flag2 = this._ReviveInBaseSound.ContainsKey(key);
			if (flag2)
			{
				this.reviceSoundIndex = key;
				this._ReviveInBaseSound[key].Play();
				this.hero.playerSound.ExeptStopTalkSounds(this._ReviveInBaseSound[key]);
			}
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0003EADC File Offset: 0x0003CCDC
	private void LoadUnbreakbleKillSound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._UnbreakbleKillSound = new Dictionary<int, AudioSource>();
		ArrayList unbreakbleKillSound = heroData.UnbreakbleKillSound;
		for (int i = 0; i < unbreakbleKillSound.Count; i++)
		{
			string url = "SoundRes/" + (string)unbreakbleKillSound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadUnbreakbleKillSoundPrefeb), i);
			}
			else
			{
				this.LoadUnbreakbleKillSoundPrefeb(resWWW, i);
			}
		}
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0003EB94 File Offset: 0x0003CD94
	private void LoadUnbreakbleKillSoundPrefeb(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = (int)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._UnbreakbleKillSound.ContainsKey(key);
			if (flag2)
			{
				this._UnbreakbleKillSound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0003EC74 File Offset: 0x0003CE74
	public void PlayUnbreakbleKillSound()
	{
		bool flag = this._UnbreakbleKillSound.Count != 0;
		if (flag)
		{
			int key = UnityEngine.Random.Range(0, this._UnbreakbleKillSound.Count);
			bool flag2 = this._UnbreakbleKillSound.ContainsKey(key);
			if (flag2)
			{
				this._UnbreakbleKillSound[key].Play();
				this.hero.playerSound.ExeptStopTalkSounds(this._UnbreakbleKillSound[key]);
			}
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0003ECEC File Offset: 0x0003CEEC
	private void LoadSniperSwitchSound()
	{
		this.SniperSwichSound = new Dictionary<int, AudioSource>();
		this.SniperSwitchUrl[0] = "skill_dianna_miaozhunjing_on";
		this.SniperSwitchUrl[1] = "skill_dianna_miaozhunjing_off";
		for (int i = 0; i < 2; i++)
		{
			string url = "SoundRes/" + this.SniperSwitchUrl[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadSniperSwitchSoundPrefeb), i);
			}
			else
			{
				this.LoadSniperSwitchSoundPrefeb(resWWW, i);
			}
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0003ED88 File Offset: 0x0003CF88
	private void LoadSniperSwitchSoundPrefeb(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = (int)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this.SniperSwichSound.ContainsKey(key);
			if (flag2)
			{
				this.SniperSwichSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0003EE68 File Offset: 0x0003D068
	public void PlaySniperSwitchSound(int ntype)
	{
		bool flag = this.SniperSwichSound.Count != 0 && this.SniperSwichSound.ContainsKey(ntype);
		if (flag)
		{
			this.SniperSwichSound[ntype].Play();
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0003EEAC File Offset: 0x0003D0AC
	private void LoadRecoverySound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._RecoverySound = new Dictionary<int, AudioSource>();
		ArrayList recoverySound = heroData.recoverySound;
		for (int i = 0; i < recoverySound.Count; i++)
		{
			string url = "SoundRes/" + (string)recoverySound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadRecoverySoundPrefab), i.ToString());
			}
			else
			{
				this.LoadRecoverySoundPrefab(resWWW, i.ToString());
			}
		}
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0003EF64 File Offset: 0x0003D164
	private void LoadRecoverySoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._RecoverySound.ContainsKey(key);
			if (flag2)
			{
				this._RecoverySound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0003F04C File Offset: 0x0003D24C
	public void PlayRecoverySound()
	{
		bool flag = this._RecoverySound.Count != 0 && this.PlayRecHpCD <= 0f;
		if (flag)
		{
			int key = UnityEngine.Random.Range(0, this._RecoverySound.Count);
			bool flag2 = this._RecoverySound.ContainsKey(key);
			if (flag2)
			{
				this._RecoverySound[key].Play();
				this.hero.playerSound.ExeptStopTalkSounds(this._RecoverySound[key]);
				this.PlayRecHpCD = 5f;
			}
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
	public void LoadAcedSound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[716];
		string url = "SoundRes/" + preloadData.sUrl + ".sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadAcedSoundPrefab), 0);
		}
		else
		{
			this.LoadAcedSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0003F158 File Offset: 0x0003D358
	private void LoadAcedSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = fUIManager.mGameUISound.transform;
			component.transform.localPosition = Vector3.zero;
			this._AcedSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 1, null, 20);
		}
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0003F208 File Offset: 0x0003D408
	public void PlayAcedSound()
	{
		bool flag = !(this._AcedSound == null);
		if (flag)
		{
			base.StartCoroutine(this.PlayAceSoundDelay());
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0003F238 File Offset: 0x0003D438
	private IEnumerator PlayAceSoundDelay()
	{
		yield return new WaitForSeconds(2f);
		this._AcedSound.Play();
		yield break;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0003F248 File Offset: 0x0003D448
	private void LoadSkillCDFinishSound()
	{
		string text = "zhandou_lengquejieshu";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillCDFinishSoundPrefab), 0);
		}
		else
		{
			this.LoadSkillCDFinishSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0003F2AC File Offset: 0x0003D4AC
	private void LoadSkillCDFinishSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._skillCDSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0003F364 File Offset: 0x0003D564
	public void PlayCDFinishSound()
	{
		bool flag = !(this._skillCDSound == null);
		if (flag)
		{
			this._skillCDSound.Play();
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0003F394 File Offset: 0x0003D594
	public void LoadDaZhaoManSound()
	{
		string text = "zhandou_dazhaoman";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadDaZhaoManSoundPrefab), 0);
		}
		else
		{
			this.LoadDaZhaoManSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0003F3F8 File Offset: 0x0003D5F8
	private void LoadDaZhaoManSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._DaZhaoManSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0003F4B0 File Offset: 0x0003D6B0
	public void PlayDaZhaoManSound()
	{
		bool flag = !(this._DaZhaoManSound == null);
		if (flag)
		{
			this._DaZhaoManSound.Play();
		}
	}

	// Token: 0x040004FF RID: 1279
	private WWW _damageSmall;

	// Token: 0x04000500 RID: 1280
	private WWW _damageBig;

	// Token: 0x04000501 RID: 1281
	private WWW _damageHead;

	// Token: 0x04000502 RID: 1282
	private float damageBigTime;

	// Token: 0x04000503 RID: 1283
	private float damageHeadTime;

	// Token: 0x04000504 RID: 1284
	private const float damageSoundLoopTime = 0.1f;

	// Token: 0x04000505 RID: 1285
	private List<AudioSource> listDamageBig = new List<AudioSource>();

	// Token: 0x04000506 RID: 1286
	private List<AudioSource> listDamageHead = new List<AudioSource>();

	// Token: 0x04000507 RID: 1287
	private AudioSource _JiZhongSound;

	// Token: 0x04000508 RID: 1288
	private AudioSource _PinSiSound;

	// Token: 0x04000509 RID: 1289
	private AudioSource _JiaXueSound;

	// Token: 0x0400050A RID: 1290
	private AudioSource _AcedSound;

	// Token: 0x0400050B RID: 1291
	private Dictionary<int, AudioSource> _ChangePinSiSound = new Dictionary<int, AudioSource>();

	// Token: 0x0400050C RID: 1292
	private Dictionary<int, AudioSource> MultiKillSount = new Dictionary<int, AudioSource>();

	// Token: 0x0400050D RID: 1293
	private Dictionary<int, AudioSource> _EnterSceneSound = new Dictionary<int, AudioSource>();

	// Token: 0x0400050E RID: 1294
	private Dictionary<int, AudioSource> _ReviveInBaseSound = new Dictionary<int, AudioSource>();

	// Token: 0x0400050F RID: 1295
	private Dictionary<int, AudioSource> _UnbreakbleKillSound = new Dictionary<int, AudioSource>();

	// Token: 0x04000510 RID: 1296
	private Dictionary<int, AudioSource> SniperSwichSound = new Dictionary<int, AudioSource>();

	// Token: 0x04000511 RID: 1297
	private Dictionary<int, AudioSource> _RecoverySound = new Dictionary<int, AudioSource>();

	// Token: 0x04000512 RID: 1298
	private AudioSource _skillCDSound;

	// Token: 0x04000513 RID: 1299
	private AudioSource _DaZhaoManSound;

	// Token: 0x04000514 RID: 1300
	private string[] multiKillUrl = new string[4];

	// Token: 0x04000515 RID: 1301
	private string[] SniperSwitchUrl = new string[2];

	// Token: 0x04000516 RID: 1302
	private DateTime tJiaXue;

	// Token: 0x04000517 RID: 1303
	private int lastPlayIndex;

	// Token: 0x04000518 RID: 1304
	private bool b_playEnterSceneSound;

	// Token: 0x04000519 RID: 1305
	private int loadCount;

	// Token: 0x0400051A RID: 1306
	private bool b_FpsLoadedFinish;

	// Token: 0x0400051B RID: 1307
	private int reviceSoundIndex;

	// Token: 0x0400051C RID: 1308
	private float PlayRecHpCD;
}
