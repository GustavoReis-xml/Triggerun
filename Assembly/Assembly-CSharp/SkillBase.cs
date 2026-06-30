using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class SkillBase : MonoBehaviour
{
	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x0600264D RID: 9805 RVA: 0x0015D3B8 File Offset: 0x0015B5B8
	// (set) Token: 0x0600264E RID: 9806 RVA: 0x0015D3D0 File Offset: 0x0015B5D0
	public int iSkillIndex
	{
		get
		{
			return this._iSkillIndex;
		}
		set
		{
			this._iSkillIndex = value;
		}
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x0015D3DA File Offset: 0x0015B5DA
	public virtual void OneSkillInput()
	{
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x0015D3DD File Offset: 0x0015B5DD
	public virtual void OneSkillAction(object __target, ArrayList parm2)
	{
		this.PlayBeginSound();
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x0015D3E7 File Offset: 0x0015B5E7
	public virtual void OneSkillActionUpdate(ArrayList list)
	{
		this.PlayBeginSound();
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x0015D3F4 File Offset: 0x0015B5F4
	public virtual void SkillInit(NetLiving net, int idx)
	{
		this._hero = net;
		this.iSkillIndex = idx;
		this.skillData = fGameManager.mloadConfig.AllPlayerSkill[this.iSkillIndex];
		Type type = null;
		bool flag = this.skillData.skillclass != "0";
		if (flag)
		{
			string typeName = "ISkillData" + this.skillData.skillclass;
			type = Type.GetType(typeName);
		}
		bool flag2 = type == null;
		if (flag2)
		{
			type = Type.GetType("ISkillData");
		}
		this.ISD = (ISkillData)Activator.CreateInstance(type);
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x0015D48C File Offset: 0x0015B68C
	public virtual void ModelLoaded()
	{
		this.LoadSkillSound();
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x0015D496 File Offset: 0x0015B696
	public virtual void OneSkillFinish()
	{
		this.PlayEndSound();
	}

	// Token: 0x06002655 RID: 9813 RVA: 0x0015D4A0 File Offset: 0x0015B6A0
	public virtual bool OneSkillCanDo()
	{
		return true;
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x0015D4B3 File Offset: 0x0015B6B3
	private void LoadSkillSound()
	{
		this.LoadSkillBeginSound();
		this.LoadSkillEndSound();
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x0015D4C4 File Offset: 0x0015B6C4
	private void LoadSkillBeginSound()
	{
		string text = this.skillData.soundBegin;
		bool flag = !(text == "0");
		if (flag)
		{
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefab), 0);
			}
			else
			{
				this.LoadSkillBeginSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x0015D540 File Offset: 0x0015B740
	private void LoadSkillBeginSoundPrefab(WWW www, object sStyle)
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
			this.audioBegin = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x0015D600 File Offset: 0x0015B800
	protected void PlayBeginSound()
	{
		bool flag = (this._hero.isCopy || !CFilmManager.Instance.playing) && !(this.audioBegin == null);
		if (flag)
		{
			this.audioBegin.Play();
		}
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x0015D64C File Offset: 0x0015B84C
	private void LoadSkillEndSound()
	{
		string text = this.skillData.soundEnd;
		bool flag = !(text == "0");
		if (flag)
		{
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillEndSoundPrefab), 0);
			}
			else
			{
				this.LoadSkillEndSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x0015D6C8 File Offset: 0x0015B8C8
	private void LoadSkillEndSoundPrefab(WWW www, object sStyle)
	{
		bool flag = this._hero != null && !this._hero.haveDestroy;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this._hero._playerSoundContainer.transform;
			component.transform.localPosition = Vector3.zero;
			this.audioEnd = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0015D77C File Offset: 0x0015B97C
	protected void PlayEndSound()
	{
		bool flag = !(this.audioEnd == null) && (this._hero.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			this.audioEnd.Play();
		}
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x0015D7CA File Offset: 0x0015B9CA
	public virtual void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x0015D7D0 File Offset: 0x0015B9D0
	public virtual void SkillSoundStopAll()
	{
		bool flag = this.audioBegin != null && this.audioBegin.isPlaying;
		if (flag)
		{
			this.audioBegin.Stop();
		}
		bool flag2 = this.audioEnd != null && this.audioEnd.isPlaying;
		if (flag2)
		{
			this.audioEnd.Stop();
		}
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x0015D83A File Offset: 0x0015BA3A
	public virtual void BreakSkill()
	{
		this.SkillSoundStopAll();
	}

	// Token: 0x04002215 RID: 8725
	protected NetLiving _hero;

	// Token: 0x04002216 RID: 8726
	private int _iSkillIndex;

	// Token: 0x04002217 RID: 8727
	public PlayerSkillData skillData;

	// Token: 0x04002218 RID: 8728
	private AudioSource audioBegin;

	// Token: 0x04002219 RID: 8729
	private AudioSource audioEnd;

	// Token: 0x0400221A RID: 8730
	public ISkillData ISD;
}
