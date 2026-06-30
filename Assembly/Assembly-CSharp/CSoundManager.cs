using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020000CB RID: 203
public class CSoundManager : MonoBehaviour
{
	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06000781 RID: 1921 RVA: 0x00042904 File Offset: 0x00040B04
	// (set) Token: 0x06000782 RID: 1922 RVA: 0x0004291B File Offset: 0x00040B1B
	public static CSoundManager Instance
	{
		get
		{
			return CSoundManager._instance;
		}
		set
		{
			CSoundManager._instance = value;
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00042924 File Offset: 0x00040B24
	public void UpdateMainVol(float val)
	{
		this.mainVol = val;
		this.lerping = true;
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.SetSoundBtn();
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0004296B File Offset: 0x00040B6B
	public void UpdateMusicVol(float val)
	{
		this.musicVol = val;
		this.lerping = true;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0004297C File Offset: 0x00040B7C
	public void UpdateSoundVol(float val)
	{
		this.soundVol = val;
		this.lerping = true;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0004298D File Offset: 0x00040B8D
	public void UpdateTalkVol(float val)
	{
		this.talkVol = val;
		this.lerping = true;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x000429A0 File Offset: 0x00040BA0
	private void Update()
	{
		bool flag = this.lerping && Time.time >= this.updateRate;
		if (flag)
		{
			this.updateRate = Time.time + 0.02f;
			bool flag2 = false;
			bool flag3 = this.mainVolNow != this.mainVol;
			if (flag3)
			{
				flag2 = true;
				this.mainVolNow = Mathf.Lerp(this.mainVolNow, this.mainVol, Time.deltaTime * this.lerpSpeed);
				this.setValue(this.mainMixer, "mainVol", this.mainVolNow);
			}
			bool flag4 = this.musicVolNow != this.musicVol;
			if (flag4)
			{
				flag2 = true;
				this.musicVolNow = Mathf.Lerp(this.musicVolNow, this.musicVol, Time.deltaTime * this.lerpSpeed);
				this.setValue(this.musicMixer, "musicMasterVol", this.musicVol);
			}
			bool flag5 = this.soundVolNow != this.soundVol;
			if (flag5)
			{
				flag2 = true;
				this.soundVolNow = Mathf.Lerp(this.soundVolNow, this.soundVol, Time.deltaTime * this.lerpSpeed);
				this.setValue(this.soundMixer, "soundMasterVol", this.soundVol);
			}
			bool flag6 = this.talkVolNow != this.talkVol;
			if (flag6)
			{
				flag2 = true;
				this.talkVolNow = Mathf.Lerp(this.talkVolNow, this.talkVol, Time.deltaTime * this.lerpSpeed);
				this.setValue(this.soundMixer, "soundTalkVol", this.talkVol);
			}
			bool flag7 = !flag2;
			if (flag7)
			{
				this.lerping = false;
			}
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00042B4C File Offset: 0x00040D4C
	private void Start()
	{
		CSoundManager.Instance = this;
		this.setValue(this.effectme.audioMixer, "effectmeVol", this.meSoundVol);
		this.setValue(this.effectfriend.audioMixer, "effectfriendVol", this.friendSoundVol);
		this.setValue(this.effectenemy.audioMixer, "effectenemyVol", this.enemySoundVol);
		this.setValue(this.walkme.audioMixer, "walkmeVol", this.walkMeVol);
		this.setValue(this.walkfriend.audioMixer, "walkfriendVol", this.walkFriendVol);
		this.setValue(this.walkenemy.audioMixer, "walkenemyVol", this.walkEnemyVol);
		this.setValue(this.talkme.audioMixer, "talkmeVol", this.meSoundVol);
		this.setValue(this.talkfriend.audioMixer, "talkfriendVol", this.friendSoundVol);
		this.setValue(this.talkenemy.audioMixer, "talkenemyVol", this.enemySoundVol);
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00042C68 File Offset: 0x00040E68
	private void setValue(AudioMixer audioMix, string parm, float value)
	{
		this.lerping = true;
		float value2 = this.getValue(value);
		audioMix.SetFloat(parm, value2);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00042C90 File Offset: 0x00040E90
	private float getValue(float v)
	{
		return Mathf.Lerp(-80f, 0f, Mathf.Pow(v, this.percentageVolLg));
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00042CC0 File Offset: 0x00040EC0
	public void checkSceneSound(GameObject scene)
	{
		AudioSource[] componentsInChildren = scene.GetComponentsInChildren<AudioSource>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.SetSceneSoundMixer(componentsInChildren[i]);
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00042CF3 File Offset: 0x00040EF3
	public void SetSceneMusicMixer(AudioSource one)
	{
		this.addSoundMixer(10, one, 0);
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00042D01 File Offset: 0x00040F01
	public void SetSceneSoundMixer(AudioSource one)
	{
		this.addSoundMixer(11, one, 0);
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00042D10 File Offset: 0x00040F10
	public void SetEffectSoundMixer(AudioSource one, int forceType = 0, NetLiving net = null, int soundType = 20)
	{
		bool flag = net != null;
		if (flag)
		{
			int num = CFilmManager.Instance.filmForce(net);
			int num2 = CFilmManager.Instance.filmParentNetId(net);
			forceType = ((num2 != net.getNetid()) ? ((num == net.playerCommon.m_nShiliServer) ? 1 : 2) : 0);
		}
		this.addSoundMixer(soundType, one, forceType);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00042D6C File Offset: 0x00040F6C
	public void SetTalkSoundMixer(AudioSource one, int forceType, NetLiving net)
	{
		int num = CFilmManager.Instance.filmForce(net);
		int num2 = CFilmManager.Instance.filmParentNetId(net);
		forceType = ((num2 != net.getNetid()) ? ((num == net.playerCommon.m_nShiliServer) ? 1 : 2) : 0);
		this.addSoundMixer(30, one, forceType);
		net.playerSound.PutTalkSounds(one);
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00042DCA File Offset: 0x00040FCA
	public void SetUISoundMixer(AudioSource one, int soundType = 20)
	{
		this.addSoundMixer(soundType, one, 0);
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00042DD8 File Offset: 0x00040FD8
	private void addSoundMixer(int type, AudioSource one, int forceType = 0)
	{
		bool flag = one.outputAudioMixerGroup == null;
		if (flag)
		{
			one.maxDistance *= this.maxDisParm;
		}
		if (type <= 11)
		{
			if (type != 10)
			{
				if (type == 11)
				{
					one.outputAudioMixerGroup = this.backgroundSoundEffects;
				}
			}
			else
			{
				one.outputAudioMixerGroup = this.backgroundMusic;
			}
		}
		else if (type != 20)
		{
			if (type != 21)
			{
				if (type == 30)
				{
					if (forceType != 1)
					{
						if (forceType != 2)
						{
							one.outputAudioMixerGroup = this.talkme;
						}
						else
						{
							one.outputAudioMixerGroup = this.talkenemy;
						}
					}
					else
					{
						one.outputAudioMixerGroup = this.talkfriend;
					}
				}
			}
			else if (forceType != 1)
			{
				if (forceType != 2)
				{
					one.outputAudioMixerGroup = this.walkme;
				}
				else
				{
					one.outputAudioMixerGroup = this.walkenemy;
				}
			}
			else
			{
				one.outputAudioMixerGroup = this.walkfriend;
			}
		}
		else if (forceType != 1)
		{
			if (forceType != 2)
			{
				one.outputAudioMixerGroup = this.effectme;
			}
			else
			{
				one.outputAudioMixerGroup = this.effectenemy;
			}
		}
		else
		{
			one.outputAudioMixerGroup = this.effectfriend;
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00042F34 File Offset: 0x00041134
	public void SetVol()
	{
		bool flag = !this.mbool;
		if (flag)
		{
			this.mainVol = 0f;
			this.UpdateMainVol(this.mainVol);
			this.mbool = true;
		}
		else
		{
			this.mainVol = -80f;
			this.UpdateMainVol(this.mainVol);
			this.mbool = false;
		}
	}

	// Token: 0x0400057A RID: 1402
	private static CSoundManager _instance;

	// Token: 0x0400057B RID: 1403
	[SerializeField]
	private AudioMixerSnapshot snapShotPlayed;

	// Token: 0x0400057C RID: 1404
	[SerializeField]
	private AudioMixerSnapshot snapShotPPaused;

	// Token: 0x0400057D RID: 1405
	[SerializeField]
	private AudioMixerSnapshot snapShotPStoped;

	// Token: 0x0400057E RID: 1406
	[SerializeField]
	private AudioMixerGroup backgroundMusic;

	// Token: 0x0400057F RID: 1407
	[SerializeField]
	private AudioMixerGroup backgroundSoundEffects;

	// Token: 0x04000580 RID: 1408
	[SerializeField]
	private AudioMixerGroup soundeffect;

	// Token: 0x04000581 RID: 1409
	[SerializeField]
	private AudioMixerGroup soundtalk;

	// Token: 0x04000582 RID: 1410
	[SerializeField]
	private AudioMixerGroup effectme;

	// Token: 0x04000583 RID: 1411
	[SerializeField]
	private AudioMixerGroup effectfriend;

	// Token: 0x04000584 RID: 1412
	[SerializeField]
	private AudioMixerGroup effectenemy;

	// Token: 0x04000585 RID: 1413
	[SerializeField]
	private AudioMixerGroup walkfriend;

	// Token: 0x04000586 RID: 1414
	[SerializeField]
	private AudioMixerGroup walkenemy;

	// Token: 0x04000587 RID: 1415
	[SerializeField]
	private AudioMixerGroup walkme;

	// Token: 0x04000588 RID: 1416
	[SerializeField]
	private AudioMixerGroup talkme;

	// Token: 0x04000589 RID: 1417
	[SerializeField]
	private AudioMixerGroup talkfriend;

	// Token: 0x0400058A RID: 1418
	[SerializeField]
	private AudioMixerGroup talkenemy;

	// Token: 0x0400058B RID: 1419
	[SerializeField]
	private AudioMixer mainMixer;

	// Token: 0x0400058C RID: 1420
	[SerializeField]
	private AudioMixer musicMixer;

	// Token: 0x0400058D RID: 1421
	[SerializeField]
	private AudioMixer soundMixer;

	// Token: 0x0400058E RID: 1422
	private float mainVol;

	// Token: 0x0400058F RID: 1423
	private float musicVol;

	// Token: 0x04000590 RID: 1424
	private float soundVol;

	// Token: 0x04000591 RID: 1425
	private float talkVol;

	// Token: 0x04000592 RID: 1426
	private float mainVolNow;

	// Token: 0x04000593 RID: 1427
	private float musicVolNow;

	// Token: 0x04000594 RID: 1428
	private float soundVolNow;

	// Token: 0x04000595 RID: 1429
	private float talkVolNow;

	// Token: 0x04000596 RID: 1430
	private float meSoundVol = 1f;

	// Token: 0x04000597 RID: 1431
	private float friendSoundVol = 0.9f;

	// Token: 0x04000598 RID: 1432
	private float enemySoundVol = 1f;

	// Token: 0x04000599 RID: 1433
	private float walkFriendVol = 0.15f;

	// Token: 0x0400059A RID: 1434
	private float walkEnemyVol = 0.75f;

	// Token: 0x0400059B RID: 1435
	private float walkMeVol = 0.3f;

	// Token: 0x0400059C RID: 1436
	public float maxDisParm = 1f;

	// Token: 0x0400059D RID: 1437
	public float lerpSpeed = 2f;

	// Token: 0x0400059E RID: 1438
	public float percentageVolLg = 0.3f;

	// Token: 0x0400059F RID: 1439
	private bool lerping;

	// Token: 0x040005A0 RID: 1440
	private float updateRate;

	// Token: 0x040005A1 RID: 1441
	private bool mbool = true;
}
