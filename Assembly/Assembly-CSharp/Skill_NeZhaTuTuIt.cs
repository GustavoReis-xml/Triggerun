using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C2 RID: 962
public class Skill_NeZhaTuTuIt : SkillBaseIt
{
	// Token: 0x06002999 RID: 10649 RVA: 0x0016EE09 File Offset: 0x0016D009
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.LoadSkillBeginSound();
		this.LoadSkillBeginSounds();
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x0016EE21 File Offset: 0x0016D021
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.Super_Jump_Start(__target);
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x0016EE35 File Offset: 0x0016D035
	private void Super_Jump_Start(object _target)
	{
		this.canGo = true;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x0016EE40 File Offset: 0x0016D040
	private void Update()
	{
		bool flag = this.canGo && base.hero.GetAnimatorBool("shift_go");
		if (flag)
		{
			this.PlayNeZhaAud();
			this.canGo = false;
		}
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x0016EE80 File Offset: 0x0016D080
	private void LoadSkillBeginSound()
	{
		bool flag = base.iSkillIndex == 1181;
		if (flag)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1920];
			string text = preloadData.sUrl;
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

	// Token: 0x0600299E RID: 10654 RVA: 0x0016EF10 File Offset: 0x0016D110
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
			this.audioTuTu = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x0016EFCD File Offset: 0x0016D1CD
	public override void OneSkillInput()
	{
		base.OneSkillInput();
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x0016EFD7 File Offset: 0x0016D1D7
	public override void OneSkillActionUpdate(ArrayList list)
	{
		this.PlayNeZhaAud();
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x0016EFE4 File Offset: 0x0016D1E4
	private void PlayNeZhaAud()
	{
		bool flag = (this._hero.isCopy || !CFilmManager.Instance.playing) && !(this.audioTuTu == null);
		if (flag)
		{
			this.audioTuTu.Play();
			bool flag2 = !(this.audioTuTub == null);
			if (flag2)
			{
				this.audioTuTub.Play();
			}
		}
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x0016F050 File Offset: 0x0016D250
	private void LoadSkillBeginSounds()
	{
		bool flag = base.iSkillIndex == 1181;
		if (flag)
		{
			string text = "skill_nezha_strike_rush";
			text = "SoundRes/" + text + ".sds";
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefabb), 0);
			}
			else
			{
				this.LoadSkillBeginSoundPrefabb(resWWW, 0);
			}
		}
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x0016F0C8 File Offset: 0x0016D2C8
	private void LoadSkillBeginSoundPrefabb(WWW www, object sStyle)
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
			this.audioTuTub = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x04002335 RID: 9013
	public bool canGo;

	// Token: 0x04002336 RID: 9014
	private AudioSource audioTuTu;

	// Token: 0x04002337 RID: 9015
	private AudioSource audioTuTub;
}
