using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class SkillJingKeShiftMe : SkillBaseMe
{
	// Token: 0x06002731 RID: 10033 RVA: 0x001626BC File Offset: 0x001608BC
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.bCanShoot = false;
		this.LoadSkillBeginSound();
		this.npcLayer = (1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			int num = 2;
			this.force1 = "force" + num.ToString() + "_player";
			this.force2 = "force" + num.ToString() + "_npc";
		}
		else
		{
			int num2 = 1;
			this.force1 = "force" + num2.ToString() + "_player";
			this.force2 = "force" + num2.ToString() + "_npc";
		}
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x00162798 File Offset: 0x00160998
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroJingKeMe)base.hero.heroBase).superDoing;
		bool result;
		if (superDoing)
		{
			result = false;
		}
		else
		{
			bool flag = this.dickeys.Count != 0;
			result = (!flag && base.OneSkillCanDo());
		}
		return result;
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x001627E8 File Offset: 0x001609E8
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.dic.Clear();
		this.doSkill = true;
		((HeroJingKeMe)base.hero.heroBase).IgnoreLayer(true);
		((HeroJingKeMe)base.hero.heroBase).JingKeObjShow(true);
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
		Vector3 a = Vector3.zero;
		Vector3 forward = CameraControl.Instance.nowTarget.forward;
		a = (forward * 1f).normalized;
		((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity(a * this.fMaxSpeed, 0.2f, true);
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.heroBase.B_Transforing = true;
		base.StartCoroutine(this.Wait_One(base.heroSkillData.m_nSKLongTime));
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x001629B5 File Offset: 0x00160BB5
	private IEnumerator Wait_One(int pa)
	{
		yield return new WaitForSeconds(0.2f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x001629CC File Offset: 0x00160BCC
	public override void OneSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.OneSkillFinish();
			((HeroJingKeMe)base.hero.heroBase).IgnoreLayer(false);
			((HeroJingKeMe)base.hero.heroBase).JingKeObjShow(false);
			this.doSkill = false;
			this.HurtEnemy();
			base.hero.heroBase.B_Transforing = false;
			base.hero.wInput.notInput = false;
		}
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x00162A5F File Offset: 0x00160C5F
	protected void HurtEnemy()
	{
		this.dickeys.Clear();
		this.dickeys = new List<int>(this.dic.Keys);
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x00162A84 File Offset: 0x00160C84
	public void Update()
	{
		bool flag = this.doSkill && base.hero.playerCommon.m_nDongjie == 1;
		if (flag)
		{
			((TBCharacterThirdMe)base.hero.tChar).SpeedMoveVelocity(Vector3.zero, 0f, false);
		}
		bool flag2 = this.dickeys.Count == 0;
		if (!flag2)
		{
			int key = this.dickeys[0];
			this.dickeys.RemoveAt(0);
			bool flag3 = this.dic.ContainsKey(key);
			if (flag3)
			{
				CJKData cjkdata = this.dic[key];
				this.dic.Remove(key);
				bool flag4 = cjkdata.obj != null;
				if (flag4)
				{
					base.RealShoot(cjkdata.at, cjkdata.obj);
				}
			}
		}
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x00162B64 File Offset: 0x00160D64
	private void OnTriggerEnter(Collider other)
	{
		bool flag = !((HeroJingKeMe)base.hero.heroBase).buffF && other.gameObject.layer == LayerMask.NameToLayer("BuffItem");
		if (flag)
		{
			bool flag2 = base.hero.isDead || base.hero.haveDestroy || !other.gameObject.name.Contains("npc_lhq");
			if (flag2)
			{
				return;
			}
			UnityEngine.Object.Destroy(other.gameObject);
			ArrayList arrayList = new ArrayList();
			int netid = base.hero.getNetid();
			arrayList.Add(netid);
			base.hero.SendMessageToServer(2060001, arrayList);
		}
		bool flag3 = !this.doSkill || (other.gameObject.layer != LayerMask.NameToLayer(this.force1) && other.gameObject.layer != LayerMask.NameToLayer(this.force2));
		if (!flag3)
		{
			int num = 1 << other.gameObject.layer;
			bool flag4 = (num & this.npcLayer) != 0;
			NetLiving netLiving;
			if (flag4)
			{
				Npc_Data component = other.gameObject.GetComponent<Npc_Data>();
				netLiving = component.Entity_Component;
			}
			else
			{
				GameObject gameObject = other.transform.root.gameObject;
				PublicInfo component2 = gameObject.GetComponent<PublicInfo>();
				bool flag5 = component2 == null;
				if (flag5)
				{
					Debug.LogError("发现可以攻击的东西没有PublicINfo, " + other.name + "," + gameObject.name);
					return;
				}
				netLiving = component2.NetObject;
			}
			bool flag6 = !this.dic.ContainsKey(netLiving.getNetid());
			if (flag6)
			{
				CJKData cjkdata = new CJKData();
				cjkdata.obj = netLiving.defaultMesh;
				cjkdata.at = other.gameObject.transform.position;
				this.dic.Add(netLiving.getNetid(), cjkdata);
			}
		}
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x00162D6C File Offset: 0x00160F6C
	private void LoadSkillBeginSound()
	{
		bool flag = base.iSkillIndex != 1131;
		if (!flag)
		{
			for (int i = 1; i < 4; i++)
			{
				string text = "skill_jingke_assault_" + i.ToString();
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
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x00162E08 File Offset: 0x00161008
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

	// Token: 0x0600273B RID: 10043 RVA: 0x00162ED7 File Offset: 0x001610D7
	public override void OneSkillInput()
	{
		base.OneSkillInput();
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x00162EE1 File Offset: 0x001610E1
	public override void OneSkillActionUpdate(ArrayList list)
	{
		this.PlayJingKeAud();
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x00162EEC File Offset: 0x001610EC
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

	// Token: 0x04002262 RID: 8802
	private float fMaxSpeed = 60f;

	// Token: 0x04002263 RID: 8803
	private bool doSkill;

	// Token: 0x04002264 RID: 8804
	private int npcLayer;

	// Token: 0x04002265 RID: 8805
	private Dictionary<int, CJKData> dic = new Dictionary<int, CJKData>();

	// Token: 0x04002266 RID: 8806
	private List<AudioSource> skillAudios = new List<AudioSource>();

	// Token: 0x04002267 RID: 8807
	private int skillNum;

	// Token: 0x04002268 RID: 8808
	private AudioSource audioBegin;

	// Token: 0x04002269 RID: 8809
	private float skillTime;

	// Token: 0x0400226A RID: 8810
	private List<int> dickeys = new List<int>();

	// Token: 0x0400226B RID: 8811
	private string force1;

	// Token: 0x0400226C RID: 8812
	private string force2;
}
