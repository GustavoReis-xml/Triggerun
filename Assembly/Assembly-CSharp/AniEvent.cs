using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class AniEvent : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x000039E5 File Offset: 0x00001BE5
	public void AniEnd(string sName)
	{
		Fun.Log("AniEvent====AniEnd==" + sName, null);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000039FA File Offset: 0x00001BFA
	public void InitEvent(NetLiving player)
	{
		this.hero = player;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00003A04 File Offset: 0x00001C04
	public void RunSound(string foot)
	{
		bool flag = (this.hero.isCopy || !CFilmManager.Instance.playing) && (this.hero.charMove.mixInput == 1 || this.hero.charMove.mixInput == 3);
		if (flag)
		{
			this.hero.playerSound.PlayFootSound(foot);
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003A70 File Offset: 0x00001C70
	public void RunSoundAssist(string foot)
	{
		bool flag = (this.hero.isCopy || !CFilmManager.Instance.playing) && this.hero.charMove.mixInput == 2;
		if (flag)
		{
			this.hero.playerSound.PlayFootSound(foot);
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003AC8 File Offset: 0x00001CC8
	public void FightEffectVector(int _weapon)
	{
		bool flag = !(this.hero.wc == null);
		if (flag)
		{
			CWeapon cweapon = this.hero.wc.weapons[_weapon];
			cweapon.launchersDict[cweapon.defaultLauncherNo].muzzle.ReSetphysicalDir();
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003B24 File Offset: 0x00001D24
	public void FightBegin(int _weapon)
	{
		bool flag = !(this.hero.wc == null);
		if (flag)
		{
			CWeapon cweapon = this.hero.wc.weapons[_weapon];
			cweapon.launchersDict[cweapon.defaultLauncherNo].muzzle.SetPhysicalBegin();
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003B80 File Offset: 0x00001D80
	public void FightEnd(int _weapon)
	{
		bool flag = !(this.hero.wc == null);
		if (flag)
		{
			CWeapon cweapon = this.hero.wc.weapons[_weapon];
			cweapon.launchersDict[cweapon.defaultLauncherNo].muzzle.SetPhysicalEnd();
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003BDC File Offset: 0x00001DDC
	public void EmoteSound(string emote)
	{
		bool flag = this.hero.isCopy || !CFilmManager.Instance.playing;
		if (flag)
		{
			this.hero.playerSound.PlayEmotionSound(emote);
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003C20 File Offset: 0x00001E20
	public void AnimationSound(string _name)
	{
		bool flag = this.hero.isCopy || !CFilmManager.Instance.playing;
		if (flag)
		{
			this.hero.playerSound.PlayAnimatorSound(_name);
		}
	}

	// Token: 0x04000076 RID: 118
	private NetLiving hero;
}
