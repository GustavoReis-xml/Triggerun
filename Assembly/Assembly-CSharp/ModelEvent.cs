using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class ModelEvent : MonoBehaviour
{
	// Token: 0x06001A7C RID: 6780 RVA: 0x000FC49C File Offset: 0x000FA69C
	public void InitEvent(int heroid, GameObject model)
	{
		this.nHeroId = heroid;
		this.modelObj = model;
		Transform transform = this.modelObj.transform.Find("ModelSound");
		bool flag = transform == null;
		if (flag)
		{
			this.modelSound = new GameObject("ModelSound");
			this.modelSound.transform.SetParent(this.modelObj.transform);
			this.modelSound.transform.localPosition = Vector3.zero;
			this.modelSound.transform.localScale = Vector3.one;
			this.modelSound.transform.localRotation = Quaternion.Euler(Vector3.zero);
		}
		else
		{
			this.modelSound = transform.gameObject;
		}
		this.LoadEmotionSound();
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x000FC568 File Offset: 0x000FA768
	public void LoadEmotionSound()
	{
		bool flag = !fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(this.nHeroId);
		if (!flag)
		{
			HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId];
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

	// Token: 0x06001A7E RID: 6782 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
	private void LoadEmotionSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			string key = sStyle.ToString();
			bool flag2 = !(this.modelObj == null) && !this.emotionSounds.ContainsKey(key);
			if (flag2)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.SetParent(this.modelSound.transform);
				component.transform.localPosition = Vector3.zero;
				this.emotionSounds.Add(key, component);
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			}
		}
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x000FC78C File Offset: 0x000FA98C
	public void EmoteSound(string ssound)
	{
		bool flag = this.emotionSounds.Count != 0 && this.emotionSounds.ContainsKey(ssound);
		if (flag)
		{
			this.emotionSounds[ssound].Play();
		}
	}

	// Token: 0x0400152C RID: 5420
	private int nHeroId;

	// Token: 0x0400152D RID: 5421
	private GameObject modelObj;

	// Token: 0x0400152E RID: 5422
	private GameObject modelSound;

	// Token: 0x0400152F RID: 5423
	private Dictionary<string, AudioSource> emotionSounds = new Dictionary<string, AudioSource>();
}
