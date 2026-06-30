using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200035D RID: 861
public class SkillPropeMe : SkillBaseMe
{
	// Token: 0x06002785 RID: 10117 RVA: 0x001645A8 File Offset: 0x001627A8
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroMedusaMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x001645E0 File Offset: 0x001627E0
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.Layer();
		this.LoadSkillBeginSounds();
		bool flag = this.shengziTransform != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
		}
		this.shengziTransform = Fun.FindGameObjectChild(base.hero.userRoot.transform.gameObject, "shengzi", false);
		this.ropeTransform = Fun.FindGameObjectChild(base.hero.userRoot.transform.gameObject, "Bip001 L Forearm", false);
		this.shengziTransform.transform.parent = base.hero.userRoot.transform;
		this.zhuaTransform = Fun.FindGameObjectChild(this.shengziTransform.gameObject, "zhuazi", false).transform;
		this.skinRender = this.shengziTransform.GetComponentInChildren<SkinnedMeshRenderer>();
		this.meshRender = this.zhuaTransform.GetComponentInChildren<MeshRenderer>();
		this.SkinCastOn();
		this.Finish();
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x001646E4 File Offset: 0x001628E4
	private void Layer()
	{
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			int num = 2;
			this.forceman = "force" + num.ToString() + "_player";
			this.forcenpc = "force" + num.ToString() + "_npc";
		}
		else
		{
			int num2 = 1;
			this.forceman = "force" + num2.ToString() + "_player";
			this.forcenpc = "force" + num2.ToString() + "_npc";
		}
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x00164784 File Offset: 0x00162984
	public override void OneSkillAction(object target, ArrayList parm2)
	{
		base.OneSkillAction(target, parm2);
		this.SkinCastOn();
		this.zhuaMan = false;
		this.zhuaNpc = false;
		base.hero.wInput.notInput = true;
		base.hero.charMove.state.fly = true;
		int layerMask = 1 << LayerMask.NameToLayer(this.forceman) | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer(this.forcenpc);
		bool flag = Physics.Raycast(CameraControl.Instance.cursorRay, out this.targetHit, 40f, layerMask);
		if (flag)
		{
			bool flag2 = this.targetHit.transform.gameObject.layer == LayerMask.NameToLayer(this.forceman);
			if (flag2)
			{
				GameObject gameObject = this.targetHit.transform.root.gameObject;
				PublicInfo component = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component == null;
				if (flag3)
				{
					Debug.LogError("发现可以攻击的东西没有PublicINfo, " + this.targetHit.collider.name + "," + gameObject.name);
					return;
				}
				this.targetNet = component.NetObject;
				this.zhuaMan = true;
				bool flag4 = this.targetNet != null && this.targetNet.UserControl != null;
				if (flag4)
				{
					this.starget = this.targetNet.UserControl.transform.position;
				}
			}
			else
			{
				bool flag5 = this.targetHit.transform.gameObject.layer == LayerMask.NameToLayer(this.forcenpc);
				if (flag5)
				{
					this.zhuaNpc = true;
					this.starget = this.targetHit.point;
				}
				else
				{
					this.starget = this.targetHit.point;
				}
			}
		}
		else
		{
			this.starget = base.hero.camTarget.position + base.hero.camTarget.forward * 20f * 2f;
		}
		Vector3 eulerAngles = new Vector3(0f, 90f, 0f);
		bool flag6 = this.targetHit.collider != null;
		if (flag6)
		{
			eulerAngles = Quaternion.FromToRotation(Vector3.right, this.targetHit.normal).eulerAngles;
		}
		ArrayList arrayList = new ArrayList();
		ArrayList value = new ArrayList
		{
			(int)(this.starget.x * 100f),
			(int)(this.starget.y * 100f),
			(int)(this.starget.z * 100f)
		};
		ArrayList value2 = new ArrayList
		{
			(int)(eulerAngles.x * 100f),
			(int)(eulerAngles.y * 100f),
			(int)(eulerAngles.z * 100f)
		};
		arrayList.Add(base.hero.getNetid());
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(value);
		arrayList.Add(value2);
		base.hero.SendSkillParms(arrayList);
		base.hero.SetAnimator("skill_shift", 0, true, 1);
		this.Init();
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x00164B20 File Offset: 0x00162D20
	private void Init()
	{
		this.show = 1;
		this.longNow = Time.time;
		this.rubbleLong = 0f;
		this.lastDis = float.MaxValue;
		this.randomFly = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
		this.shengziTransform.transform.position = this.ropeTransform.transform.position;
		this.shengziTransform.LookAt(this.starget);
		this.finishTime = Time.time;
	}

	// Token: 0x0600278A RID: 10122 RVA: 0x00164BCC File Offset: 0x00162DCC
	private void Finish()
	{
		this.nomoveTime = 0f;
		this.finishTime = 0f;
		this.show = -1;
		base.hero.SetAnimator("skill_shift", 0, false, 1);
		base.hero.SetAnimator("shift_go", 0, false, 1);
		((TBCharacterThirdMe)base.hero.tChar).MoveDirectionByVector(false, Vector3.zero);
		((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
		((HeroMedusaMe)base.hero.heroBase).IgnoreLayer(false);
		Transform[] bones = this.skinRender.bones;
		Vector3 localEulerAngles = new Vector3(0f, 90f, 0f);
		for (int i = 0; i < bones.Length; i++)
		{
			bones[i].transform.localPosition = Vector3.zero;
			bones[i].transform.localEulerAngles = localEulerAngles;
		}
		this.zhuaTransform.transform.localPosition = Vector3.zero;
		this.zhuaTransform.localRotation = bones[0].transform.localRotation;
		this.ShengZiShow(false);
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x00164D04 File Offset: 0x00162F04
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("shift_end", 0, true, 1);
		this.zhuaMan = false;
		this.zhuaNpc = false;
		base.hero.wInput.notInput = false;
		base.hero.charMove.state.fly = false;
		((HeroBaseMe)base.hero.heroBase).SetStat("rope", -1);
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x00164D84 File Offset: 0x00162F84
	private void OnDestroy()
	{
		bool flag = !(this.shengziTransform == null);
		if (flag)
		{
			UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
		}
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x00164DB8 File Offset: 0x00162FB8
	private void InitFirst()
	{
		bool flag = this.show != 1;
		if (!flag)
		{
			this.ShengZiShow(true);
			Transform[] bones = this.skinRender.bones;
			bool flag2 = false;
			bool flag3 = this.zhuaMan;
			if (flag3)
			{
				bool flag4 = this.targetNet != null && this.targetNet.userBip != null;
				if (flag4)
				{
					this.targetV3 = this.targetNet.userBip.position;
				}
			}
			else
			{
				this.targetV3 = this.starget;
			}
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num = Vector3.Distance(this.ropeTransform.position, this.targetV3);
			bool flag5 = num > 20f;
			if (flag5)
			{
				this.targetV3Temp = 20f * normalized + this.ropeTransform.position;
				num = 20f;
				flag2 = true;
			}
			Vector3 vector = this.targetV3Temp;
			bool flag6 = !this.zhuaMan && !this.zhuaNpc;
			if (flag6)
			{
				vector.y += 2.2f;
			}
			Vector3 normalized2 = (vector - this.ropeTransform.position).normalized;
			float d = Vector3.Distance(this.ropeTransform.position, vector) + 0.3f;
			this.targetV3MoveTemp = this.ropeTransform.position + normalized2 * d;
			this.targetV3MoveDir = (this.targetV3MoveTemp - this.ropeTransform.position).normalized;
			Vector3 a = Vector3.Lerp(this.ropeTransform.position, this.targetV3Temp, 0.5f);
			this.enterpoint = a + this.randomFly;
			int num2 = (int)(num / 0.1f);
			int num3 = (bones.Length <= num2) ? bones.Length : num2;
			this.center = this.enterpoint;
			this.point = new List<Vector3>();
			Vector3 item = Vector3.zero;
			float num4 = (Time.time - this.longNow) * 100f;
			float num5 = 0f;
			Vector3 position = bones[0].transform.position;
			for (int i = 0; i < bones.Length; i++)
			{
				num5 += 0.1f;
				bool flag7 = i < num3;
				if (flag7)
				{
					Vector3 a2 = Vector3.Lerp(this.ropeTransform.position, this.center, (float)i / (float)num3);
					Vector3 b = Vector3.Lerp(this.center, this.targetV3Temp, (float)i / (float)num3);
					item = Vector3.Lerp(a2, b, (float)i / (float)num3);
				}
				this.point.Add(item);
				bool flag8 = num4 > num5 && i < this.point.Count;
				if (flag8)
				{
					position = this.point[i];
					bones[i].transform.position = position;
					this.zhuaTransform.transform.position = this.targetV3Temp;
				}
				else
				{
					bones[i].transform.position = position;
				}
			}
			bool flag9 = num4 > num;
			if (flag9)
			{
				bool flag10 = flag2;
				if (flag10)
				{
					this.show = 3;
					this.longNow = Time.time;
					this.PlaySkillAud();
				}
				else
				{
					this.show = 2;
					this.breakit();
					this.PlaySkillAud();
					base.hero.charMove.state.fly = true;
					((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
					base.hero.SetAnimator("shift_go", 0, true, 1);
					((HeroMedusaMe)base.hero.heroBase).IgnoreLayer(true);
				}
			}
		}
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x001651AB File Offset: 0x001633AB
	private void breakit()
	{
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x001651D8 File Offset: 0x001633D8
	private void getRope()
	{
		bool flag = this.show != 3;
		if (!flag)
		{
			Transform[] bones = this.skinRender.bones;
			float num = (Time.time - this.longNow) * 200f;
			this.targetV3 = this.starget;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num2 = Vector3.Distance(this.ropeTransform.position, this.targetV3) - num;
			bool flag2 = num2 < 0f;
			if (flag2)
			{
				num2 = 0f;
			}
			this.targetV3Temp = this.ropeTransform.position + normalized * num2;
			int num3 = (int)(num2 / 0.1f);
			int num4 = (bones.Length <= num3) ? bones.Length : num3;
			float num5 = Time.deltaTime * 15f;
			this.rubbleLong += num5;
			Vector3 vector = Vector3.Lerp(this.enterpoint, this.ropeTransform.position, this.rubbleLong);
			this.center = vector;
			this.point = new List<Vector3>();
			Vector3 position = Vector3.zero;
			for (int i = 0; i < bones.Length; i++)
			{
				bool flag3 = i < num4;
				if (flag3)
				{
					position = Vector3.Lerp(this.ropeTransform.position, this.targetV3Temp, (float)i / (float)num4);
				}
				bones[i].transform.position = position;
			}
			this.zhuaTransform.position = this.targetV3Temp;
			bool flag4 = num2 <= 0f;
			if (flag4)
			{
				this.Finish();
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x06002790 RID: 10128 RVA: 0x0016539C File Offset: 0x0016359C
	private void showLine()
	{
		bool flag = this.show != 2;
		if (!flag)
		{
			Transform[] bones = this.skinRender.bones;
			this.targetV3 = this.starget;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num = Vector3.Distance(this.ropeTransform.position, this.targetV3);
			bool flag2 = num > 20f;
			if (flag2)
			{
				this.targetV3Temp = 20f * normalized + this.ropeTransform.position;
				num = 20f;
			}
			int num2 = (int)(num / 0.1f);
			int num3 = (bones.Length <= num2) ? bones.Length : num2;
			float num4 = Time.deltaTime * 15f;
			this.rubbleLong += num4;
			Vector3 vector = Vector3.Lerp(this.enterpoint, this.ropeTransform.position, this.rubbleLong);
			this.center = vector;
			this.point = new List<Vector3>();
			Vector3 vector2 = Vector3.zero;
			for (int i = 0; i < bones.Length; i++)
			{
				bool flag3 = i < num3;
				if (flag3)
				{
					Vector3 a = Vector3.Lerp(this.ropeTransform.position, this.center, (float)i / (float)num3);
					Vector3 b = Vector3.Lerp(this.center, this.targetV3Temp, (float)i / (float)num3);
					vector2 = Vector3.Lerp(a, b, (float)i / (float)num3);
				}
				this.point.Add(vector2);
				bones[i].transform.position = vector2;
			}
			this.zhuaTransform.position = this.targetV3Temp;
		}
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x0016556C File Offset: 0x0016376C
	private void showLineMove()
	{
		bool flag = this.show != 2 || Time.deltaTime == 0f;
		if (!flag)
		{
			bool superDoing = ((HeroMedusaMe)base.hero.heroBase).superDoing;
			if (superDoing)
			{
				this.Finish();
			}
			float num = Vector3.Distance(base.transform.position, this.targetV3MoveTemp);
			bool flag2 = this.targetHit.collider != null;
			if (flag2)
			{
				Quaternion rotation = Quaternion.FromToRotation(Vector3.right, this.targetHit.normal);
				this.zhuaTransform.rotation = rotation;
			}
			float deltaTime = Time.deltaTime;
			bool flag3 = num > this.lastDis && Time.time - this.finishTime > 0.3f;
			if (flag3)
			{
				this.Finish();
				this.OneSkillFinish();
			}
			bool flag4 = this.lastDis - num < 0.001f;
			if (flag4)
			{
				this.nomoveTime += deltaTime;
				bool flag5 = this.nomoveTime > 0.3f;
				if (flag5)
				{
					this.Finish();
					this.OneSkillFinish();
				}
			}
			else
			{
				this.lastDis = num;
				bool flag6 = num < 2f;
				if (flag6)
				{
					this.Finish();
					this.OneSkillFinish();
				}
			}
			((TBCharacterThirdMe)base.hero.tChar).MoveDirectionByVector(true, this.targetV3MoveDir * 20f);
			bool flag7 = this.show == -1;
			if (flag7)
			{
				((TBCharacterThirdMe)base.hero.tChar).MoveDirectionByVector(false, Vector3.zero);
				((TBCharacterThirdMe)base.hero.tChar).VelocityYSetZero(false);
			}
		}
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x0016572A File Offset: 0x0016392A
	private void Update()
	{
		this.showLineMove();
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x00165734 File Offset: 0x00163934
	private void LateUpdate()
	{
		this.InitFirst();
		this.showLine();
		this.getRope();
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x0016574C File Offset: 0x0016394C
	public void ShengZiShow(bool hide)
	{
		bool flag = this.shengziTransform.gameObject.activeSelf != hide;
		if (flag)
		{
			this.shengziTransform.gameObject.SetActive(hide);
		}
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x00165788 File Offset: 0x00163988
	private void SkinCastOn()
	{
		bool flag = this.skinRender.shadowCastingMode != ShadowCastingMode.On;
		if (flag)
		{
			this.skinRender.shadowCastingMode = ShadowCastingMode.On;
		}
		bool flag2 = this.meshRender.shadowCastingMode != ShadowCastingMode.On;
		if (flag2)
		{
			this.meshRender.shadowCastingMode = ShadowCastingMode.On;
		}
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x001657E0 File Offset: 0x001639E0
	private void LoadSkillBeginSounds()
	{
		string text = "skill_medusa_hook_back";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadSkillBeginSoundPrefabb), 0);
		}
		else
		{
			this.LoadSkillBeginSoundPrefabb(resWWW, 0);
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x00165844 File Offset: 0x00163A44
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
			this.goAud = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, this._hero, 20);
		}
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x00165904 File Offset: 0x00163B04
	private void PlaySkillAud()
	{
		bool flag = (this._hero.isCopy || !CFilmManager.Instance.playing) && !(this.goAud == null);
		if (flag)
		{
			this.goAud.Play();
		}
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x0016594F File Offset: 0x00163B4F
	public override void OneSkillActionUpdate(ArrayList list)
	{
		this.PlaySkillAud();
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x00165959 File Offset: 0x00163B59
	public override void OneSkillInput()
	{
		base.OneSkillInput();
	}

	// Token: 0x04002287 RID: 8839
	private string forceman;

	// Token: 0x04002288 RID: 8840
	private string forcenpc;

	// Token: 0x04002289 RID: 8841
	private RaycastHit targetHit;

	// Token: 0x0400228A RID: 8842
	private SkinnedMeshRenderer skinRender;

	// Token: 0x0400228B RID: 8843
	private MeshRenderer meshRender;

	// Token: 0x0400228C RID: 8844
	private AudioSource goAud;

	// Token: 0x0400228D RID: 8845
	private int show;

	// Token: 0x0400228E RID: 8846
	private float longNow;

	// Token: 0x0400228F RID: 8847
	private float rubbleLong;

	// Token: 0x04002290 RID: 8848
	private float lastDis = float.MaxValue;

	// Token: 0x04002291 RID: 8849
	private float nomoveTime;

	// Token: 0x04002292 RID: 8850
	private const float eachDis = 0.1f;

	// Token: 0x04002293 RID: 8851
	private const float longMax = 20f;

	// Token: 0x04002294 RID: 8852
	private const float longSpeed = 100f;

	// Token: 0x04002295 RID: 8853
	private const float longGetSpeed = 200f;

	// Token: 0x04002296 RID: 8854
	private const float moveSpeed = 20f;

	// Token: 0x04002297 RID: 8855
	private const float rubberNumber = 15f;

	// Token: 0x04002298 RID: 8856
	private const float outUnit = 0.5f;

	// Token: 0x04002299 RID: 8857
	private float finishTime;

	// Token: 0x0400229A RID: 8858
	public Vector3 starget;

	// Token: 0x0400229B RID: 8859
	private Vector3 center;

	// Token: 0x0400229C RID: 8860
	private Vector3 randomFly = Vector3.one;

	// Token: 0x0400229D RID: 8861
	private Vector3 enterpoint = Vector3.one;

	// Token: 0x0400229E RID: 8862
	private Vector3 targetV3;

	// Token: 0x0400229F RID: 8863
	private Vector3 targetV3Temp;

	// Token: 0x040022A0 RID: 8864
	private Vector3 targetV3MoveTemp;

	// Token: 0x040022A1 RID: 8865
	private Vector3 targetV3MoveDir;

	// Token: 0x040022A2 RID: 8866
	private Transform shengziTransform;

	// Token: 0x040022A3 RID: 8867
	private Transform ropeTransform;

	// Token: 0x040022A4 RID: 8868
	private Transform zhuaTransform;

	// Token: 0x040022A5 RID: 8869
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x040022A6 RID: 8870
	private bool zhuaMan;

	// Token: 0x040022A7 RID: 8871
	private bool zhuaNpc;

	// Token: 0x040022A8 RID: 8872
	private NetLiving targetNet;
}
