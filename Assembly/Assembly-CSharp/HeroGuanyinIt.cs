using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class HeroGuanyinIt : HeroBaseIt
{
	// Token: 0x060011C3 RID: 4547 RVA: 0x000A6B30 File Offset: 0x000A4D30
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.LoadFlyDownSound();
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x000A6B44 File Offset: 0x000A4D44
	private void LoadFlyDownSound()
	{
		string url = "SoundRes/skillvoice_luoshen_slowfall.sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFlyDownSoundPrefab), "start");
		}
		else
		{
			this.LoadFlyDownSoundPrefab(resWWW, "start");
		}
		url = "SoundRes/skillvoice_luoshen_slowfall_loop.sds";
		resWWW = GameRoot.GetResWWW(url);
		bool flag2 = resWWW == null;
		if (flag2)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFlyDownSoundPrefab), "loop");
		}
		else
		{
			this.LoadFlyDownSoundPrefab(resWWW, "loop");
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000A6BD4 File Offset: 0x000A4DD4
	private void LoadFlyDownSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = ((GamePlayer)base.hero)._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			component.transform.localRotation = Quaternion.Euler(Vector3.zero);
			bool flag2 = sStyle.ToString() == "start";
			if (flag2)
			{
				this.flydownStart = component;
			}
			bool flag3 = sStyle.ToString() == "loop";
			if (flag3)
			{
				this.flydownLoop = component;
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000A6CDC File Offset: 0x000A4EDC
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero != null;
		if (flag)
		{
			bool animatorBool = base.hero.GetAnimatorBool("IsFlyDown");
			if (animatorBool)
			{
				this.OnFlyDown(true);
			}
			else
			{
				this.OnFlyDown(false);
			}
		}
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000A6D2C File Offset: 0x000A4F2C
	private void OnFlyDown(bool state)
	{
		if (state)
		{
			bool flag = !this.isFlyDown;
			if (flag)
			{
				this.isFlyDown = true;
				bool flag2 = (this._hero.isCopy || !CFilmManager.Instance.playing) && this.flydownStart != null && !this.flydownStart.isPlaying;
				if (flag2)
				{
					this.flydownStart.Play();
				}
			}
			else
			{
				bool flag3 = this.flydownStart != null && !this.flydownStart.isPlaying && this.flydownLoop != null && !this.flydownLoop.isPlaying && (this._hero.isCopy || !CFilmManager.Instance.playing);
				if (flag3)
				{
					this.flydownLoop.loop = true;
					this.flydownLoop.Play();
				}
			}
		}
		else
		{
			bool flag4 = !this.isFlyDown;
			if (!flag4)
			{
				this.isFlyDown = false;
				bool flag5 = this._hero.isCopy || !CFilmManager.Instance.playing;
				if (flag5)
				{
					bool isPlaying = this.flydownLoop.isPlaying;
					if (isPlaying)
					{
						this.flydownLoop.Stop();
					}
					bool isPlaying2 = this.flydownStart.isPlaying;
					if (isPlaying2)
					{
						this.flydownStart.Stop();
					}
				}
			}
		}
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000A6E9C File Offset: 0x000A509C
	public void SetDeadMember(NetLiving gp, int ntype)
	{
		bool flag = !gp.bePlayer;
		if (!flag)
		{
			bool flag2 = this.szDeadMember.Contains(gp);
			bool flag3 = ntype == 1;
			if (flag3)
			{
				bool flag4 = !flag2;
				if (flag4)
				{
					this.szDeadMember.Add(gp);
				}
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					this.szDeadMember.Remove(gp);
				}
			}
		}
	}

	// Token: 0x04000E1D RID: 3613
	private bool isFlyDown;

	// Token: 0x04000E1E RID: 3614
	private AudioSource flydownStart;

	// Token: 0x04000E1F RID: 3615
	private AudioSource flydownLoop;

	// Token: 0x04000E20 RID: 3616
	private List<NetLiving> szDeadMember = new List<NetLiving>();
}
