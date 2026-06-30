using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class fUISoundManager : MonoBehaviour
{
	// Token: 0x06000C09 RID: 3081 RVA: 0x00072003 File Offset: 0x00070203
	private void Awake()
	{
		fUISoundManager.instance = this;
		this.allAudioSound = new Dictionary<string, AudioSource>();
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00072017 File Offset: 0x00070217
	private void Update()
	{
		this.OnAddSound();
		this.OnSubSound();
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00072028 File Offset: 0x00070228
	public void PlayUISound(string sSoundName, int nSoundType = 0, fUISoundManager.UISoundStopFun func = null, int soundManagerType = 20)
	{
		bool flag = this.allAudioSound.ContainsKey(sSoundName);
		if (flag)
		{
			AudioSource audioSource = this.allAudioSound[sSoundName];
			bool flag2 = nSoundType == 1;
			if (flag2)
			{
				audioSource.volume = 0f;
				this.szOpenSource.Add(audioSource);
			}
			audioSource.Play();
			bool flag3 = func != null && !audioSource.loop;
			if (flag3)
			{
				base.StartCoroutine(this.DealyTIme(func, audioSource));
			}
		}
		else
		{
			this.LoadUISound(sSoundName, nSoundType, func, soundManagerType);
		}
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x000720B5 File Offset: 0x000702B5
	private IEnumerator DealyTIme(fUISoundManager.UISoundStopFun func, AudioSource aus)
	{
		yield return new WaitForSeconds(0.5f);
		float ft = Time.time + 300f;
		while (ft > Time.time && aus.isPlaying)
		{
			yield return new WaitForSeconds(0.1f);
		}
		func();
		yield break;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x000720D4 File Offset: 0x000702D4
	private void LoadUISound(string sSoundName, int nSoundType, fUISoundManager.UISoundStopFun func, int soundManagerType)
	{
		string url = "SoundRes/" + sSoundName + ".sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadUISoundPrefab), new ArrayList
			{
				sSoundName,
				nSoundType,
				func,
				soundManagerType
			});
		}
		else
		{
			this.LoadUISoundPrefab(resWWW, new ArrayList
			{
				sSoundName,
				nSoundType,
				func,
				soundManagerType
			});
		}
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00072184 File Offset: 0x00070384
	private void LoadUISoundPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("资源问题3");
		}
		else
		{
			ArrayList arrayList = (ArrayList)sStyle;
			string text = (string)arrayList[0];
			int nSoundType = (int)arrayList[1];
			fUISoundManager.UISoundStopFun uisoundStopFun = (fUISoundManager.UISoundStopFun)arrayList[2];
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.transform.SetParent(fUIManager.mGameUISound.transform);
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this.allAudioSound.ContainsKey(text);
			if (flag2)
			{
				this.allAudioSound.Add(text, component);
			}
			CSoundManager.Instance.SetUISoundMixer(component, (int)arrayList[3]);
			this.PlayUISound(text, nSoundType, uisoundStopFun, (int)arrayList[3]);
			bool loop = component.loop;
			if (loop)
			{
				if (uisoundStopFun != null)
				{
					uisoundStopFun();
				}
			}
		}
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x000722B8 File Offset: 0x000704B8
	public void StopSound(string sName, int nSoundType = 0)
	{
		bool flag = this.allAudioSound.ContainsKey(sName);
		if (flag)
		{
			bool flag2 = nSoundType == 1;
			if (flag2)
			{
				this.szStopSource.Add(this.allAudioSound[sName]);
			}
			else
			{
				this.allAudioSound[sName].Stop();
			}
		}
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00072310 File Offset: 0x00070510
	public void StopSound()
	{
		foreach (string key in this.allAudioSound.Keys)
		{
			this.allAudioSound[key].Stop();
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00072378 File Offset: 0x00070578
	public bool SoundIsPlay(string sName)
	{
		bool flag = this.allAudioSound.ContainsKey(sName);
		return flag && this.allAudioSound[sName].isPlaying;
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x000723B0 File Offset: 0x000705B0
	private void OnAddSound()
	{
		int count = this.szOpenSource.Count;
		bool flag = count < 1;
		if (!flag)
		{
			for (int i = 0; i < count; i++)
			{
				AudioSource audioSource = this.szOpenSource[i];
				bool flag2 = !(audioSource == null);
				if (flag2)
				{
					audioSource.volume += Time.deltaTime / 3.5f;
					bool flag3 = audioSource.volume >= 1f;
					if (flag3)
					{
						audioSource.volume = 1f;
						this.szAddRemove.Add(audioSource);
					}
				}
			}
			while (this.szAddRemove.Count > 0)
			{
				AudioSource audioSource = this.szAddRemove[0];
				this.szOpenSource.Remove(audioSource);
				this.szAddRemove.Remove(audioSource);
			}
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00072498 File Offset: 0x00070698
	private void OnSubSound()
	{
		int count = this.szStopSource.Count;
		bool flag = count < 1;
		if (!flag)
		{
			for (int i = 0; i < count; i++)
			{
				AudioSource audioSource = this.szStopSource[i];
				bool flag2 = !(audioSource == null);
				if (flag2)
				{
					audioSource.volume -= Time.deltaTime / 3.5f;
					bool flag3 = audioSource.volume <= 0f;
					if (flag3)
					{
						audioSource.Stop();
						audioSource.volume = 1f;
						this.szDelRemove.Add(audioSource);
					}
				}
			}
			while (this.szDelRemove.Count > 0)
			{
				AudioSource audioSource = this.szDelRemove[0];
				this.szStopSource.Remove(audioSource);
				this.szDelRemove.Remove(audioSource);
			}
		}
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00072585 File Offset: 0x00070785
	public void RelinkStopSound()
	{
		this.StopSound("match_ganme_searching", 0);
	}

	// Token: 0x04000A3A RID: 2618
	public static fUISoundManager instance;

	// Token: 0x04000A3B RID: 2619
	private Dictionary<string, AudioSource> allAudioSound = new Dictionary<string, AudioSource>();

	// Token: 0x04000A3C RID: 2620
	private List<AudioSource> szOpenSource = new List<AudioSource>();

	// Token: 0x04000A3D RID: 2621
	private List<AudioSource> szStopSource = new List<AudioSource>();

	// Token: 0x04000A3E RID: 2622
	private List<AudioSource> szAddRemove = new List<AudioSource>();

	// Token: 0x04000A3F RID: 2623
	private List<AudioSource> szDelRemove = new List<AudioSource>();

	// Token: 0x0200056F RID: 1391
	// (Invoke) Token: 0x06003419 RID: 13337
	public delegate void UISoundStopFun();
}
