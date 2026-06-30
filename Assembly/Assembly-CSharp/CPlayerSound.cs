using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class CPlayerSound : MonoBehaviour
{
	// Token: 0x060006B1 RID: 1713 RVA: 0x0003AEE4 File Offset: 0x000390E4
	public void PutTalkSounds(AudioSource audio)
	{
		int instanceID = audio.GetInstanceID();
		bool flag = !this.meTalkSounds.ContainsKey(instanceID);
		if (flag)
		{
			this.meTalkSounds.Add(instanceID, audio);
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0003AF1C File Offset: 0x0003911C
	public void ExeptStopTalkSounds(AudioSource audio)
	{
		int instanceID = audio.GetInstanceID();
		foreach (int num in this.meTalkSounds.Keys)
		{
			bool flag = instanceID != num;
			if (flag)
			{
				bool flag2 = this.meTalkSounds[num] == null;
				if (flag2)
				{
					Debug.LogError("有一个音效失踪了.不知道问题.");
				}
				else
				{
					this.meTalkSounds[num].Stop();
				}
			}
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0003AFC4 File Offset: 0x000391C4
	public virtual void initPlayerSound(NetLiving user)
	{
		this.meTalkSounds = new Dictionary<int, AudioSource>();
		this.hero = user;
		this.LoadFootSound();
		this.LoadCartridgeSound();
		this.LoadHurtSound();
		this.LoadDeadSound();
		this.LoadHuDongStart();
		this.LoadJumpOverSound();
		this.LoadJumpWaterOverSound();
		this.LoadPaintSound();
		this.LoadEmotionSound();
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0003B023 File Offset: 0x00039223
	protected virtual void LoadHuDongStart()
	{
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0003B028 File Offset: 0x00039228
	public void LoadHuDongSound(bool isFriend)
	{
		this._hudongSound = new Dictionary<string, AudioSource>();
		int nModelIndex = this.hero.playerCommon.m_nModelIndex;
		bool flag = !fGameManager.mloadConfig.AllHuDongUserData.ContainsKey(nModelIndex);
		if (!flag)
		{
			HuDongHeroData huDongHeroData = fGameManager.mloadConfig.AllHuDongUserData[nModelIndex];
			string[] array = (!isFriend) ? huDongHeroData.AllHuDongSound[2] : huDongHeroData.AllHuDongSound[1];
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = !(array[i] == "0");
				if (flag2)
				{
					string url = "SoundRes/" + array[i] + ".sds";
					WWW resWWW = GameRoot.GetResWWW(url);
					bool flag3 = resWWW == null;
					if (flag3)
					{
						GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadHuDongSoundPrefab), array[i]);
					}
					else
					{
						this.LoadHuDongSoundPrefab(resWWW, array[i]);
					}
				}
			}
		}
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0003B12C File Offset: 0x0003932C
	private void LoadHuDongSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || this.hero == null || this.hero.haveDestroy;
		if (!flag)
		{
			string key = (string)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			bool flag2 = gameObject == null;
			if (!flag2)
			{
				AudioSource component = gameObject.GetComponent<AudioSource>();
				bool flag3 = !(component == null);
				if (flag3)
				{
					component.playOnAwake = false;
					component.Stop();
					component.transform.parent = this.hero._playerSoundContainer.transform;
					component.transform.localPosition = Vector3.zero;
					bool flag4 = !this._hudongSound.ContainsKey(key);
					if (flag4)
					{
						this._hudongSound.Add(key, component);
					}
					CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
				}
			}
		}
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0003B230 File Offset: 0x00039430
	public virtual void PlayZhaoHuSound(string sName)
	{
		bool flag = this._hudongSound.Count != 0 && (this.hero.isCopy || !CFilmManager.Instance.playing) && this._hudongSound.ContainsKey(sName);
		if (flag)
		{
			this._hudongSound[sName].Play();
			this.hero.playerSound.ExeptStopTalkSounds(this._hudongSound[sName]);
		}
		else
		{
			bool flag2 = !string.IsNullOrEmpty(sName) && (this.hero.isCopy || !CFilmManager.Instance.playing);
			if (flag2)
			{
				string text = sName.EndsWith(".sds") ? sName : (sName + ".sds");
				bool flag3 = !text.StartsWith("SoundRes/");
				if (flag3)
				{
					text = "SoundRes/" + text;
				}
				WWW resWWW = GameRoot.GetResWWW(text);
				bool flag4 = resWWW == null;
				if (flag4)
				{
					GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadDynamicSoundPrefab), sName);
				}
				else
				{
					this.LoadDynamicSoundPrefab(resWWW, sName);
				}
			}
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0003B34C File Offset: 0x0003954C
	private void LoadDynamicSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			string key = (string)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			gameObject.transform.parent = this.hero._playerSoundContainer.transform;
			gameObject.transform.localPosition = Vector3.zero;
			component.spatialBlend = 1f;
			component.minDistance = 3f;
			component.maxDistance = 20f;
			component.Play();
			this.hero.playerSound.ExeptStopTalkSounds(component);
			UnityEngine.Object.Destroy(gameObject, component.clip.length + 0.1f);
			bool flag2 = !this._hudongSound.ContainsKey(key);
			if (flag2)
			{
				this._hudongSound.Add(key, component);
			}
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0003B44C File Offset: 0x0003964C
	private void LoadCartridgeSound()
	{
		WeaponData weaponData = fGameManager.mloadConfig.WeaponXmlData[this.hero.playerCommon.m_nModelIndex];
		this._cartridgeSound = new Dictionary<int, AudioSource>();
		string[] cartridgepublicsound = weaponData.cartridgepublicsound;
		for (int i = 0; i < cartridgepublicsound.Length; i++)
		{
			bool flag = !(cartridgepublicsound[i] == "0");
			if (flag)
			{
				string url = "SoundRes/" + cartridgepublicsound[i] + ".sds";
				WWW resWWW = GameRoot.GetResWWW(url);
				bool flag2 = resWWW == null;
				if (flag2)
				{
					GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadCartridgeSoundPrefab), i);
				}
				else
				{
					this.LoadCartridgeSoundPrefab(resWWW, i);
				}
			}
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0003B510 File Offset: 0x00039710
	private void LoadCartridgeSoundPrefab(WWW www, object sStyle)
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
			bool flag2 = !this._cartridgeSound.ContainsKey(key);
			if (flag2)
			{
				this._cartridgeSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0003B5F0 File Offset: 0x000397F0
	public void PlayLoadCartridgeSound(int cartridge)
	{
		bool flag = this._cartridgeSound.Count != 0 && this._cartridgeSound.ContainsKey(cartridge);
		if (flag)
		{
			this._cartridgeSound[cartridge].Play();
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0003B634 File Offset: 0x00039834
	private void LoadFootSound()
	{
		this._footSound = new Dictionary<string, AudioSource>();
		ArrayList footSound = this.hero.heroData.footSound;
		string url;
		WWW resWWW;
		for (int i = 0; i < footSound.Count; i++)
		{
			url = "SoundRes/" + (string)footSound[i] + ".sds";
			resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFootSoundPrefab), (string)footSound[i]);
			}
			else
			{
				this.LoadFootSoundPrefab(resWWW, (string)footSound[i]);
			}
		}
		url = string.Format("SoundRes/{0}.sds", "footstepleft_water");
		resWWW = GameRoot.GetResWWW(url);
		bool flag2 = resWWW == null;
		if (flag2)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFootSoundPrefab), "footstepleft_water");
		}
		else
		{
			this.LoadFootSoundPrefab(resWWW, "footstepleft_water");
		}
		url = string.Format("SoundRes/{0}.sds", "footstepright_water");
		resWWW = GameRoot.GetResWWW(url);
		bool flag3 = resWWW == null;
		if (flag3)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFootSoundPrefab), "footstepright_water");
		}
		else
		{
			this.LoadFootSoundPrefab(resWWW, "footstepright_water");
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0003B778 File Offset: 0x00039978
	private void LoadFootSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			string key = (string)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._footSound.ContainsKey(key);
			if (flag2)
			{
				this._footSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 21);
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0003B85C File Offset: 0x00039A5C
	public void PlayFootSound(string foot)
	{
		bool flag = this._footSound.Count == 0;
		if (!flag)
		{
			bool flag2 = !this.hero.playerCommon.inWater;
			if (flag2)
			{
				bool flag3 = this._footSound.ContainsKey(foot);
				if (flag3)
				{
					this._footSound[foot].Play();
				}
			}
			else
			{
				string key = (!foot.Contains("right")) ? "footstepleft_water" : "footstepright_water";
				bool flag4 = this._footSound.ContainsKey(key);
				if (flag4)
				{
					this._footSound[key].Play();
				}
			}
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0003B900 File Offset: 0x00039B00
	private void LoadHurtSound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._hurtsound = new Dictionary<int, AudioSource>();
		ArrayList arrayList = heroData.hurtSound;
		for (int i = 0; i < arrayList.Count; i++)
		{
			string url = "SoundRes/" + (string)arrayList[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadHurtSoundPrefab), i.ToString());
			}
			else
			{
				this.LoadHurtSoundPrefab(resWWW, i.ToString());
			}
		}
		this._tshurtsound = new Dictionary<int, AudioSource>();
		arrayList = heroData.ts_hurtSound;
		for (int j = 0; j < arrayList.Count; j++)
		{
			string url2 = "SoundRes/" + (string)arrayList[j] + ".sds";
			WWW resWWW2 = GameRoot.GetResWWW(url2);
			bool flag2 = resWWW2 == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.LoadTsHurtSoundPrefab), j.ToString());
			}
			else
			{
				this.LoadTsHurtSoundPrefab(resWWW2, j.ToString());
			}
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0003BA4C File Offset: 0x00039C4C
	private void LoadHurtSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy && !(this.hero._playerSoundContainer == null);
		if (flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._hurtsound.ContainsKey(key);
			if (flag2)
			{
				this._hurtsound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0003BB44 File Offset: 0x00039D44
	private void LoadTsHurtSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			int key = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._tshurtsound.ContainsKey(key);
			if (flag2)
			{
				this._tshurtsound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0003BC48 File Offset: 0x00039E48
	public virtual void PlayHurtSound(int soundType)
	{
		List<AudioSource> list = new List<AudioSource>();
		if (soundType != 0)
		{
			if (soundType == 1)
			{
				bool flag = this._tshurtsound.Count == 0;
				if (flag)
				{
					return;
				}
				list.AddRange(this._tshurtsound.Values);
			}
		}
		else
		{
			bool flag2 = this._hurtsound.Count == 0;
			if (flag2)
			{
				return;
			}
			list.AddRange(this._hurtsound.Values);
		}
		bool flag3 = Time.time - this.fHurtSound <= 5f;
		if (!flag3)
		{
			this.fHurtSound = Time.time;
			int count = list.Count;
			int num = UnityEngine.Random.Range(0, count);
			bool flag4 = num >= 0;
			if (flag4)
			{
				AudioSource audioSource = list[num];
				bool flag5 = !(audioSource == null) && !audioSource.isPlaying;
				if (flag5)
				{
					audioSource.Play();
					this.hero.playerSound.ExeptStopTalkSounds(audioSource);
				}
			}
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0003BD54 File Offset: 0x00039F54
	public void ShopHurtSound()
	{
		bool flag = this._hurtsound.Count == 0;
		if (!flag)
		{
			foreach (AudioSource audioSource in this._hurtsound.Values)
			{
				bool flag2 = audioSource != null;
				if (flag2)
				{
					audioSource.Stop();
				}
			}
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0003BDD4 File Offset: 0x00039FD4
	private void LoadDeadSound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		this._deadsound = new Dictionary<int, AudioSource>();
		ArrayList deadSound = heroData.deadSound;
		for (int i = 0; i < deadSound.Count; i++)
		{
			string url = "SoundRes/" + (string)deadSound[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadDeadSoundPrefab), i.ToString());
			}
			else
			{
				this.LoadDeadSoundPrefab(resWWW, i.ToString());
			}
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0003BE8C File Offset: 0x0003A08C
	private void LoadDeadSoundPrefab(WWW www, object sStyle)
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
			bool flag2 = !this._deadsound.ContainsKey(key);
			if (flag2)
			{
				this._deadsound.Add(key, component);
			}
			CSoundManager.Instance.SetTalkSoundMixer(component, 0, this.hero);
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0003BF74 File Offset: 0x0003A174
	public virtual void PlayDeadSound(int ntype)
	{
		this.hero.playerSound.ShopHurtSound();
		this.hero.skillInfo.SkillTalkStopAll();
		bool flag = (this.hero.isCopy || !CFilmManager.Instance.playing) && this._deadsound.ContainsKey(ntype);
		if (flag)
		{
			this._deadsound[ntype].Play();
			this.hero.playerSound.ExeptStopTalkSounds(this._deadsound[ntype]);
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0003C004 File Offset: 0x0003A204
	public void ShopDeadSound()
	{
		bool flag = this._deadsound.Count == 0;
		if (!flag)
		{
			foreach (AudioSource audioSource in this._deadsound.Values)
			{
				bool flag2 = audioSource != null;
				if (flag2)
				{
					audioSource.Stop();
				}
			}
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0003C084 File Offset: 0x0003A284
	public void LoadBuffSound(string url, int buffId)
	{
		url = "SoundRes/" + url + ".sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadBuffSoundPrefeb), 0);
		}
		else
		{
			this.LoadBuffSoundPrefeb(resWWW, buffId);
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0003C0E0 File Offset: 0x0003A2E0
	public void LoadBuffSoundPrefeb(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			int num = int.Parse(sStyle.ToString());
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._BuffSound.ContainsKey(num);
			if (flag2)
			{
				this._BuffSound.Add(num, component);
				this.PlayBuffSound(num);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0003C1CC File Offset: 0x0003A3CC
	public void PlayBuffSound(int ntype)
	{
		bool flag = this._BuffSound.Count != 0 && this._BuffSound.ContainsKey(ntype);
		if (flag)
		{
			this._BuffSound[ntype].Play();
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0003C210 File Offset: 0x0003A410
	public bool hasBuffSound(int buffId)
	{
		return this._BuffSound.ContainsKey(buffId);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0003C238 File Offset: 0x0003A438
	private void LoadJumpWaterOverSound()
	{
		string url = string.Format("SoundRes/{0}.sds", "jumpover_water");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadJumpOverWaterSoundPrefab), "jumpover_water");
		}
		else
		{
			this.LoadJumpOverWaterSoundPrefab(resWWW, "jumpover_water");
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0003C290 File Offset: 0x0003A490
	private void LoadJumpOverWaterSoundPrefab(WWW www, object sStyle)
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
			this._JumpWaterOverSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 21);
		}
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0003C350 File Offset: 0x0003A550
	private void LoadJumpOverSound()
	{
		HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.hero.playerCommon.m_nModelIndex];
		bool flag = heroData.JumpOverSound != null && heroData.JumpOverSound != "0" && heroData.JumpOverSound.Length != 0;
		if (flag)
		{
			string url = "SoundRes/" + heroData.JumpOverSound + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadJumpOverSoundPrefab), heroData.JumpOverSound);
			}
			else
			{
				this.LoadJumpOverSoundPrefab(resWWW, heroData.JumpOverSound);
			}
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0003C404 File Offset: 0x0003A604
	private void LoadJumpOverSoundPrefab(WWW www, object sStyle)
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
			this._JumpOverSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 21);
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
	public void PlayJumpOverSound()
	{
		bool inWater = this.hero.playerCommon.inWater;
		if (inWater)
		{
			bool flag = this._JumpWaterOverSound != null;
			if (flag)
			{
				this._JumpWaterOverSound.Play();
			}
		}
		else
		{
			bool flag2 = !(this._JumpOverSound == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
			if (flag2)
			{
				this._JumpOverSound.Play();
			}
		}
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0003C548 File Offset: 0x0003A748
	public void LoadSwitchArrowSound()
	{
		this.SwitchArrowSound = new Dictionary<int, AudioSource>();
		this.switchArrowUrl[0] = "skill_houyi_switch";
		this.switchArrowUrl[1] = "skill_houyi_switch1";
		for (int i = 0; i < 2; i++)
		{
			string url = "SoundRes/" + this.switchArrowUrl[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadSwitchArrowSoundPrefab), i);
			}
			else
			{
				this.LoadSwitchArrowSoundPrefab(resWWW, i);
			}
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
	private void LoadSwitchArrowSoundPrefab(WWW www, object sStyle)
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
			bool flag2 = !this.SwitchArrowSound.ContainsKey(key);
			if (flag2)
			{
				this.SwitchArrowSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0003C6C4 File Offset: 0x0003A8C4
	public void PlaySwitchArrowSound(int i)
	{
		bool flag = this.SwitchArrowSound.Count != 0;
		if (flag)
		{
			bool flag2 = this.SwitchArrowSound.ContainsKey(0) && this.SwitchArrowSound[0].isPlaying;
			if (flag2)
			{
				this.SwitchArrowSound[0].Stop();
			}
			bool flag3 = this.SwitchArrowSound.ContainsKey(1) && this.SwitchArrowSound[1].isPlaying;
			if (flag3)
			{
				this.SwitchArrowSound[1].Stop();
			}
			bool flag4 = this.SwitchArrowSound.ContainsKey(i);
			if (flag4)
			{
				this.SwitchArrowSound[i].Play();
			}
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0003C784 File Offset: 0x0003A984
	public void LoadPaintSound()
	{
		string url = string.Format("SoundRes/{0}.sds", "game_penqi");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadPaintSoundPrefab), 0);
		}
		else
		{
			this.LoadPaintSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0003C7E0 File Offset: 0x0003A9E0
	private void LoadPaintSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this._PaintSound = gameObject.GetComponent<AudioSource>();
			this._PaintSound.playOnAwake = false;
			this._PaintSound.Stop();
			this._PaintSound.transform.SetParent(this.hero._playerSoundContainer.transform);
			this._PaintSound.transform.localPosition = Vector3.zero;
			CSoundManager.Instance.SetEffectSoundMixer(this._PaintSound, 0, null, 20);
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
	public void PlayPaintSound()
	{
		bool flag = !(this._PaintSound == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._PaintSound.Play();
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0003C900 File Offset: 0x0003AB00
	public void LoadEmotionSound()
	{
		bool flag = !fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(this.hero.playerCommon.m_nModelIndex);
		if (!flag)
		{
			HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.hero.playerCommon.m_nModelIndex];
			foreach (EmotionUnlock emotionUnlock in heroUnlockData.szEmotionUnlock.Values)
			{
				bool flag2 = string.IsNullOrEmpty(emotionUnlock.sSound);
				if (!flag2)
				{
					string[] array = emotionUnlock.sSound.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					foreach (string text in array2)
					{
						bool flag3 = !string.IsNullOrEmpty(text);
						if (flag3)
						{
							string url = "SoundRes/" + text + ".sds";
							WWW resWWW = GameRoot.GetResWWW(url);
							bool flag4 = resWWW == null;
							if (flag4)
							{
								GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadEmotionSoundPrefab), text);
							}
							else
							{
								this.LoadEmotionSoundPrefab(resWWW, text);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0003CA5C File Offset: 0x0003AC5C
	private void LoadEmotionSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			string key = sStyle.ToString();
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.SetParent(this.hero._playerSoundContainer.transform);
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this.emotionSounds.ContainsKey(key);
			if (flag2)
			{
				this.emotionSounds.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0003CB3C File Offset: 0x0003AD3C
	public void PlayEmotionSound(string ssound)
	{
		bool flag = this.emotionSounds.Count != 0 && (this.hero.isCopy || !CFilmManager.Instance.playing) && this.emotionSounds.ContainsKey(ssound);
		if (flag)
		{
			this.emotionSounds[ssound].Play();
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0003CB98 File Offset: 0x0003AD98
	public void LoadShieldContinue()
	{
		string text = "skill_xingtian_shield_open_continued";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadShieldContinueSoundPrefab), 0);
		}
		else
		{
			this.LoadShieldContinueSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0003CBFC File Offset: 0x0003ADFC
	private void LoadShieldContinueSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.loop = true;
			component.transform.parent = this.hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this._shieldOpenContinue = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
	public void PlayShieldContinueSound(int type)
	{
		bool flag = !(this._shieldOpenContinue == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			switch (type)
			{
			case 0:
				this._shieldOpenContinue.Play();
				break;
			case 1:
				this._shieldOpenContinue.Stop();
				this.PlayShieldCloseSound();
				break;
			case 2:
				this._shieldOpenContinue.Stop();
				break;
			}
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0003CD50 File Offset: 0x0003AF50
	public void LoadShieldBreak()
	{
		string text = "skill_xingtian_shield_crash";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadShieldBreakSoundPrefab), 0);
		}
		else
		{
			this.LoadShieldBreakSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
	private void LoadShieldBreakSoundPrefab(WWW www, object sStyle)
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
			this._shieldCrash = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0003CE74 File Offset: 0x0003B074
	public void PlayShieldBreakSound()
	{
		bool flag = !(this._shieldCrash == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._shieldCrash.Play();
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
	public void LoadShieldOpen()
	{
		string text = "skill_xingtian_shield_open";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadShieldOpenSoundPrefab), 0);
		}
		else
		{
			this.LoadShieldOpenSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0003CF28 File Offset: 0x0003B128
	private void LoadShieldOpenSoundPrefab(WWW www, object sStyle)
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
			this._ShieldOpen = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
	public void PlayShieldOpenSound()
	{
		bool flag = !(this._ShieldOpen == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._ShieldOpen.Play();
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0003D038 File Offset: 0x0003B238
	public void LoadShieldClose()
	{
		string text = "skill_xingtian_shield_close";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadShieldCloseSoundPrefab), 0);
		}
		else
		{
			this.LoadShieldCloseSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0003D09C File Offset: 0x0003B29C
	private void LoadShieldCloseSoundPrefab(WWW www, object sStyle)
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
			this._ShieldClose = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0003D15C File Offset: 0x0003B35C
	public void PlayShieldCloseSound()
	{
		bool flag = !(this._ShieldClose == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._ShieldClose.Play();
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0003D1AC File Offset: 0x0003B3AC
	public void LoadShieldAboutCrash()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1955];
		string text = preloadData.sUrl;
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadShieldAboutCrashSoundPrefab), 0);
		}
		else
		{
			this.LoadShieldAboutCrashSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0003D224 File Offset: 0x0003B424
	private void LoadShieldAboutCrashSoundPrefab(WWW www, object sStyle)
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
			this._ShieldAboutCrash = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
	public void PlayShieldAboutCrashSound()
	{
		bool flag = !(this._ShieldAboutCrash == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._ShieldAboutCrash.Play();
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0003D334 File Offset: 0x0003B534
	public void LoadWeaponDrop()
	{
		string text = "huandan_yinglong_dropgun";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadWeaponDropSoundPrefab), 0);
		}
		else
		{
			this.LoadWeaponDropSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0003D398 File Offset: 0x0003B598
	private void LoadWeaponDropSoundPrefab(WWW www, object sStyle)
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
			this._WeaponDropSound = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0003D458 File Offset: 0x0003B658
	public void PlayWeaponDropSound()
	{
		bool flag = !(this._WeaponDropSound == null) && (this.hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this._WeaponDropSound.Play();
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
	public void PlayAnimatorSound(string sName)
	{
		bool flag = this.hero == null || (!this.hero.isCopy && CFilmManager.Instance.playing);
		if (!flag)
		{
			bool flag2 = this.playAnimatorSound.ContainsKey(sName);
			if (flag2)
			{
				bool flag3 = !this.playAnimatorSound[sName].isPlaying;
				if (flag3)
				{
					this.playAnimatorSound[sName].Play();
				}
			}
			else
			{
				string url = "SoundRes/" + sName + ".sds";
				WWW resWWW = GameRoot.GetResWWW(url);
				bool flag4 = resWWW == null;
				if (flag4)
				{
					GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.PlayAnimatorSoundPrefab), sName);
				}
				else
				{
					this.PlayAnimatorSoundPrefab(resWWW, sName);
				}
			}
		}
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0003D56C File Offset: 0x0003B76C
	private void PlayAnimatorSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.hero != null && !this.hero.haveDestroy;
		if (flag)
		{
			string key = (string)sStyle;
			bool flag2 = !this.playAnimatorSound.ContainsKey(key);
			if (flag2)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.parent = this.hero._playerSoundContainer.transform;
				component.transform.localPosition = Vector3.zero;
				this.playAnimatorSound.Add(key, component);
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, this.hero, 20);
				this.playAnimatorSound[key].Play();
			}
		}
	}

	// Token: 0x040004D9 RID: 1241
	public const string cdFinishSound = "zhandou_lengquejieshu";

	// Token: 0x040004DA RID: 1242
	public const string zd_shanghaixiaoSound = "zhandou_shanghaixiao";

	// Token: 0x040004DB RID: 1243
	public const string zd_shanghaidaSound = "zhandou_shanghaida";

	// Token: 0x040004DC RID: 1244
	public const string zd_baotouSound = "zhandou_baotou";

	// Token: 0x040004DD RID: 1245
	public const string zd_jishaSound = "zhandou_jisha";

	// Token: 0x040004DE RID: 1246
	public const string pinsiSound = "zhandou_pinsijiemian";

	// Token: 0x040004DF RID: 1247
	public const string dazhaomanSound = "zhandou_dazhaoman";

	// Token: 0x040004E0 RID: 1248
	public const string jiaxueSound = "zhandou_jiaxue";

	// Token: 0x040004E1 RID: 1249
	public const string paintSound = "game_penqi";

	// Token: 0x040004E2 RID: 1250
	public const string ShieldOpenContinue = "skill_xingtian_shield_open_continued";

	// Token: 0x040004E3 RID: 1251
	public const string ShieldBreak = "skill_xingtian_shield_crash";

	// Token: 0x040004E4 RID: 1252
	public const string ShieldOpen = "skill_xingtian_shield_open";

	// Token: 0x040004E5 RID: 1253
	public const string ShieldClose = "skill_xingtian_shield_close";

	// Token: 0x040004E6 RID: 1254
	public const string WeaponDropSound = "huandan_yinglong_dropgun";

	// Token: 0x040004E7 RID: 1255
	private AudioSource _JumpOverSound;

	// Token: 0x040004E8 RID: 1256
	private AudioSource _JumpWaterOverSound;

	// Token: 0x040004E9 RID: 1257
	private Dictionary<string, AudioSource> _footSound = new Dictionary<string, AudioSource>();

	// Token: 0x040004EA RID: 1258
	private Dictionary<int, AudioSource> _cartridgeSound = new Dictionary<int, AudioSource>();

	// Token: 0x040004EB RID: 1259
	private Dictionary<int, AudioSource> _hurtsound = new Dictionary<int, AudioSource>();

	// Token: 0x040004EC RID: 1260
	private Dictionary<int, AudioSource> _tshurtsound = new Dictionary<int, AudioSource>();

	// Token: 0x040004ED RID: 1261
	private Dictionary<int, AudioSource> _deadsound = new Dictionary<int, AudioSource>();

	// Token: 0x040004EE RID: 1262
	private Dictionary<string, AudioSource> _hudongSound = new Dictionary<string, AudioSource>();

	// Token: 0x040004EF RID: 1263
	private Dictionary<int, AudioSource> _BuffSound = new Dictionary<int, AudioSource>();

	// Token: 0x040004F0 RID: 1264
	private Dictionary<int, AudioSource> meTalkSounds = new Dictionary<int, AudioSource>();

	// Token: 0x040004F1 RID: 1265
	private Dictionary<int, AudioSource> SwitchArrowSound = new Dictionary<int, AudioSource>();

	// Token: 0x040004F2 RID: 1266
	private Dictionary<string, AudioSource> emotionSounds = new Dictionary<string, AudioSource>();

	// Token: 0x040004F3 RID: 1267
	private Dictionary<string, AudioSource> playAnimatorSound = new Dictionary<string, AudioSource>();

	// Token: 0x040004F4 RID: 1268
	private AudioSource _shieldOpenContinue;

	// Token: 0x040004F5 RID: 1269
	private AudioSource _shieldCrash;

	// Token: 0x040004F6 RID: 1270
	private AudioSource _ShieldOpen;

	// Token: 0x040004F7 RID: 1271
	private AudioSource _ShieldClose;

	// Token: 0x040004F8 RID: 1272
	private AudioSource _SwitchSound;

	// Token: 0x040004F9 RID: 1273
	private AudioSource _PaintSound;

	// Token: 0x040004FA RID: 1274
	private AudioSource _ShieldAboutCrash;

	// Token: 0x040004FB RID: 1275
	private AudioSource _WeaponDropSound;

	// Token: 0x040004FC RID: 1276
	public NetLiving hero;

	// Token: 0x040004FD RID: 1277
	private string[] switchArrowUrl = new string[4];

	// Token: 0x040004FE RID: 1278
	private float fHurtSound;
}
