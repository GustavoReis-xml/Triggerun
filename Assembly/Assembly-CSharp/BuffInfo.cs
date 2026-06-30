using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class BuffInfo
{
	// Token: 0x060001FD RID: 509 RVA: 0x0000CB3C File Offset: 0x0000AD3C
	public void InitBuff(int nBuffId, NetLiving hero)
	{
		bool flag = !fGameManager.mloadConfig.AllBuffObj.ContainsKey(nBuffId);
		if (flag)
		{
			Debug.LogError("配表中没有nBuffId:" + nBuffId.ToString() + "索引的buff");
		}
		else
		{
			this.buffdata = fGameManager.mloadConfig.AllBuffObj[nBuffId];
			this._hero = hero;
			this.LoadBuffBeginSound();
			this.LoadBuffEndSound();
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000CBAC File Offset: 0x0000ADAC
	private void LoadBuffBeginSound()
	{
		string text = this.buffdata.m_sSoundBegin;
		bool flag = !(text == "0");
		if (flag)
		{
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadBuffBeginSoundPrefab), 0);
			}
			else
			{
				this.LoadBuffBeginSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000CC28 File Offset: 0x0000AE28
	private void LoadBuffBeginSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this._hero != null && this._hero.playerHaveCreate && !this._hero.haveDestroy && !(this._hero._playerSoundContainer == null);
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.audioBegin = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000CD08 File Offset: 0x0000AF08
	private void LoadBuffEndSound()
	{
		string text = this.buffdata.m_sSoundEnd;
		bool flag = !(text == "0");
		if (flag)
		{
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadBuffEndSoundPrefab), 0);
			}
			else
			{
				this.LoadBuffEndSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000CD84 File Offset: 0x0000AF84
	private void LoadBuffEndSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this._hero != null && !this._hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.audioEnd = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
		}
	}

	// Token: 0x040001AA RID: 426
	public buffData buffdata;

	// Token: 0x040001AB RID: 427
	public AudioSource audioBegin;

	// Token: 0x040001AC RID: 428
	public AudioSource audioEnd;

	// Token: 0x040001AD RID: 429
	private NetLiving _hero;
}
