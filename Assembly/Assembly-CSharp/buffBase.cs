using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class buffBase : MonoBehaviour
{
	// Token: 0x060001F5 RID: 501 RVA: 0x0000C902 File Offset: 0x0000AB02
	public virtual void OneBuffInit(NetLiving net)
	{
		this._hero = net;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000C90C File Offset: 0x0000AB0C
	public void PlayBuffBeginSound(AudioSource audioBegin)
	{
		bool flag = !(audioBegin == null) && (this._hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			audioBegin.Play();
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000C950 File Offset: 0x0000AB50
	public void PlayBuffEndSound(AudioSource audioEnd)
	{
		bool flag = !(audioEnd == null) && (this._hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			audioEnd.Play();
		}
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000C994 File Offset: 0x0000AB94
	public void StopBuffSound(AudioSource audio)
	{
		bool flag = !(audio == null);
		if (flag)
		{
			audio.Stop();
		}
	}

	// Token: 0x04000198 RID: 408
	protected NetLiving _hero;
}
