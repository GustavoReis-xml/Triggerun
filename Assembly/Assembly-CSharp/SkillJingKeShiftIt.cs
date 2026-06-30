using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class SkillJingKeShiftIt : SkillBaseIt
{
	// Token: 0x0600272B RID: 10027 RVA: 0x00162345 File Offset: 0x00160545
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.LoadSkillBeginSound();
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x00162358 File Offset: 0x00160558
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		float num = Time.time - this.skillTime;
		bool flag = num >= 6f && num < 12f;
		if (flag)
		{
			this.skillNum--;
		}
		bool flag2 = num >= 12f && num < 18f;
		if (flag2)
		{
			this.skillNum -= 2;
		}
		bool flag3 = num >= 18f;
		if (flag3)
		{
			this.skillNum -= 3;
		}
		bool flag4 = this.skillNum < 0;
		if (flag4)
		{
			this.skillNum = 0;
		}
		bool flag5 = this.skillNum >= 3;
		if (flag5)
		{
			this.skillNum = 2;
		}
		this.skillNum++;
		this.skillTime = Time.time;
		this.PlayJingKeAud();
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x0016243C File Offset: 0x0016063C
	private void LoadSkillBeginSound()
	{
		for (int i = 1; i < 4; i++)
		{
			string text = "skill_jingke_assault_" + i.ToString();
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefab), 0);
			}
			else
			{
				this.LoadSkillBeginSoundPrefab(resWWW, 0);
			}
		}
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x001624C0 File Offset: 0x001606C0
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
			this.skillAudios.Add(this.audioBegin);
		}
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x00162590 File Offset: 0x00160790
	protected void PlayJingKeAud()
	{
		bool flag = !this._hero.isCopy && CFilmManager.Instance.playing;
		if (!flag)
		{
			bool flag2 = this.skillAudios.Count >= 1 && this.skillNum == 1;
			if (flag2)
			{
				bool flag3 = this.skillAudios[0] == null;
				if (flag3)
				{
					return;
				}
				this.skillAudios[0].Play();
			}
			bool flag4 = this.skillAudios.Count >= 2 && this.skillNum == 2;
			if (flag4)
			{
				bool flag5 = this.skillAudios[1] == null;
				if (flag5)
				{
					return;
				}
				this.skillAudios[1].Play();
			}
			bool flag6 = this.skillAudios.Count >= 3 && this.skillNum == 3 && !(this.skillAudios[2] == null);
			if (flag6)
			{
				this.skillAudios[2].Play();
			}
		}
	}

	// Token: 0x0400225E RID: 8798
	private List<AudioSource> skillAudios = new List<AudioSource>();

	// Token: 0x0400225F RID: 8799
	private int skillNum;

	// Token: 0x04002260 RID: 8800
	private AudioSource audioBegin;

	// Token: 0x04002261 RID: 8801
	private float skillTime;
}
