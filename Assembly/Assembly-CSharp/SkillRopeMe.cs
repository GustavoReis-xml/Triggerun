using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000367 RID: 871
public class SkillRopeMe : SkillBaseMe
{
	// Token: 0x060027CB RID: 10187 RVA: 0x00166B8F File Offset: 0x00164D8F
	private void Start()
	{
	}

	// Token: 0x060027CC RID: 10188 RVA: 0x00166B92 File Offset: 0x00164D92
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
	}

	// Token: 0x060027CD RID: 10189 RVA: 0x00166BA8 File Offset: 0x00164DA8
	public override void ModelLoaded()
	{
		base.ModelLoaded();
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
		this.skinRender.shadowCastingMode = ShadowCastingMode.On;
		this.meshRender = this.zhuaTransform.GetComponentInChildren<MeshRenderer>();
		this.meshRender.shadowCastingMode = ShadowCastingMode.On;
		this.finish();
	}

	// Token: 0x060027CE RID: 10190 RVA: 0x00166CB4 File Offset: 0x00164EB4
	private void Init()
	{
		this.show = 1;
		this.longNow = Time.time;
		this.rubbleLong = 0f;
		this.lastDis = float.MaxValue;
		this.randomFly = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
		this.shengziTransform.transform.position = this.ropeTransform.transform.position;
		this.shengziTransform.LookAt(this.target);
	}

	// Token: 0x060027CF RID: 10191 RVA: 0x00166D58 File Offset: 0x00164F58
	private void InitFirst()
	{
		bool flag = this.show != 1;
		if (!flag)
		{
			this.shengziTransform.gameObject.SetActive(true);
			Transform[] bones = this.skinRender.bones;
			bool flag2 = false;
			this.targetV3 = this.target;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num = Vector3.Distance(this.ropeTransform.position, this.targetV3);
			bool flag3 = num > 30.000002f;
			if (flag3)
			{
				this.targetV3Temp = 30.000002f * normalized + this.ropeTransform.position;
				num = 30.000002f;
				flag2 = true;
			}
			Vector3 vector = this.targetV3Temp;
			vector.y += 1.8f;
			Vector3 normalized2 = (vector - this.ropeTransform.position).normalized;
			float d = Vector3.Distance(this.ropeTransform.position, vector) + 0.3f;
			this.targetV3MoveTemp = this.ropeTransform.position + normalized2 * d;
			this.targetV3MoveDir = (this.targetV3MoveTemp - this.ropeTransform.position).normalized;
			Vector3 a = Vector3.Lerp(this.ropeTransform.position, this.targetV3Temp, 0.5f);
			this.enterpoint = a + this.randomFly;
			int num2 = (int)(num / 0.15f);
			int num3 = (bones.Length <= num2) ? bones.Length : num2;
			this.center = this.enterpoint;
			this.point = new List<Vector3>();
			Vector3 item = Vector3.zero;
			float num4 = (Time.time - this.longNow) * 100f;
			float num5 = 0f;
			Vector3 position = bones[0].transform.position;
			for (int i = 0; i < bones.Length; i++)
			{
				num5 += 0.15f;
				bool flag4 = i < num3;
				if (flag4)
				{
					Vector3 a2 = Vector3.Lerp(this.ropeTransform.position, this.center, (float)i / (float)num3);
					Vector3 b = Vector3.Lerp(this.center, this.targetV3Temp, (float)i / (float)num3);
					item = Vector3.Lerp(a2, b, (float)i / (float)num3);
				}
				this.point.Add(item);
				bool flag5 = num4 > num5 && i < this.point.Count;
				if (flag5)
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
			bool flag6 = num4 > num;
			if (flag6)
			{
				bool flag7 = flag2;
				if (flag7)
				{
					this.show = 3;
					this.longNow = Time.time;
					ArrayList arrayList = new ArrayList();
					arrayList.Add(base.iSkillIndex);
					arrayList.Add(1);
					base.hero.SendMessageToServer(31105, arrayList);
				}
				else
				{
					this.show = 2;
					this.breakit();
					base.hero.charMove.state.fly = true;
				}
			}
		}
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x001670D8 File Offset: 0x001652D8
	private void getRope()
	{
		bool flag = this.show != 3;
		if (!flag)
		{
			Transform[] bones = this.skinRender.bones;
			float num = (Time.time - this.longNow) * 300f;
			this.targetV3 = this.target;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num2 = Vector3.Distance(this.ropeTransform.position, this.targetV3) - num;
			bool flag2 = num2 < 0f;
			if (flag2)
			{
				num2 = 0f;
			}
			this.targetV3Temp = this.ropeTransform.position + normalized * num2;
			int num3 = (int)(num2 / 0.15f);
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
				this.finish();
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x0016729A File Offset: 0x0016549A
	private void breakit()
	{
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x001672C8 File Offset: 0x001654C8
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.wInput.notInput = false;
		base.hero.charMove.state.fly = false;
		((HeroBaseMe)base.hero.heroBase).SetStat("rope", -1);
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x00167324 File Offset: 0x00165524
	private void showLine()
	{
		bool flag = this.show != 2;
		if (!flag)
		{
			Transform[] bones = this.skinRender.bones;
			this.targetV3 = this.target;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num = Vector3.Distance(this.ropeTransform.position, this.targetV3);
			bool flag2 = num > 30.000002f;
			if (flag2)
			{
				this.targetV3Temp = 30.000002f * normalized + this.ropeTransform.position;
				num = 30.000002f;
			}
			int num2 = (int)(num / 0.15f);
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

	// Token: 0x060027D4 RID: 10196 RVA: 0x001674F4 File Offset: 0x001656F4
	private void showLineMove()
	{
		bool flag = this.show != 2 || Time.deltaTime == 0f;
		if (!flag)
		{
			float num = Vector3.Distance(base.transform.position, this.targetV3MoveTemp);
			bool flag2 = this.targetHit.collider != null;
			if (flag2)
			{
				Quaternion rotation = Quaternion.FromToRotation(Vector3.right, this.targetHit.normal);
				this.zhuaTransform.rotation = rotation;
			}
			float deltaTime = Time.deltaTime;
			bool flag3 = num > this.lastDis;
			if (flag3)
			{
				this.finish();
				this.OneSkillFinish();
			}
			bool flag4 = num == this.lastDis;
			if (flag4)
			{
				this.nomoveTime += deltaTime;
				bool flag5 = this.nomoveTime > 0.5f;
				if (flag5)
				{
					this.finish();
					this.OneSkillFinish();
				}
			}
			else
			{
				this.nomoveTime = 0f;
				this.lastDis = num;
			}
			base.hero.tChar.moveDirectionNoForce = this.targetV3MoveDir * 0.25f;
			bool flag6 = this.show == -1;
			if (flag6)
			{
				base.hero.tChar.moveDirectionNoForce = Vector3.zero;
			}
		}
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x0016763A File Offset: 0x0016583A
	private void Update()
	{
		this.showLineMove();
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x00167644 File Offset: 0x00165844
	private void finish()
	{
		base.hero.wInput.notInput = false;
		this.nomoveTime = 0f;
		this.show = -1;
		Transform[] bones = this.skinRender.bones;
		Vector3 localEulerAngles = new Vector3(0f, 90f, 0f);
		for (int i = 0; i < bones.Length; i++)
		{
			bones[i].transform.localPosition = Vector3.zero;
			bones[i].transform.localEulerAngles = localEulerAngles;
		}
		this.zhuaTransform.transform.localPosition = Vector3.zero;
		this.zhuaTransform.localRotation = bones[0].transform.localRotation;
		this.shengziTransform.gameObject.SetActive(false);
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x0016770F File Offset: 0x0016590F
	private void LateUpdate()
	{
		this.InitFirst();
		this.showLine();
		this.getRope();
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x00167728 File Offset: 0x00165928
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		((HeroBaseMe)base.hero.heroBase).BreakSth(1);
		((HeroBaseMe)base.hero.heroBase).SetStat("rope", 1);
		this.skinRender.shadowCastingMode = ShadowCastingMode.On;
		this.meshRender.shadowCastingMode = ShadowCastingMode.On;
		bool flag = Physics.Raycast(CameraControl.Instance.cursorRay, out this.targetHit, 60.000004f, 1 << LayerMask.NameToLayer("Default"));
		if (flag)
		{
			this.target = this.targetHit.point;
		}
		else
		{
			this.target = base.hero.camTarget.position + base.hero.camTarget.forward * 30.000002f * 2f;
		}
		Vector3 eulerAngles = new Vector3(0f, 90f, 0f);
		bool flag2 = this.targetHit.collider != null;
		if (flag2)
		{
			eulerAngles = Quaternion.FromToRotation(Vector3.right, this.targetHit.normal).eulerAngles;
		}
		ArrayList arrayList = new ArrayList();
		ArrayList value = new ArrayList
		{
			(int)(this.target.x * 100f),
			(int)(this.target.y * 100f),
			(int)(this.target.z * 100f)
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
		this.Init();
		base.hero.SetAnimator("SkillRope", 0, true, 1);
	}

	// Token: 0x040022C4 RID: 8900
	private SkinnedMeshRenderer skinRender;

	// Token: 0x040022C5 RID: 8901
	private MeshRenderer meshRender;

	// Token: 0x040022C6 RID: 8902
	private RaycastHit targetHit;

	// Token: 0x040022C7 RID: 8903
	private const float eachDis = 0.15f;

	// Token: 0x040022C8 RID: 8904
	private const float longMax = 30.000002f;

	// Token: 0x040022C9 RID: 8905
	private const float longSpeed = 100f;

	// Token: 0x040022CA RID: 8906
	private const float longGetSpeed = 300f;

	// Token: 0x040022CB RID: 8907
	private const float moveSpeed = 0.25f;

	// Token: 0x040022CC RID: 8908
	private const float outUnit = 0.5f;

	// Token: 0x040022CD RID: 8909
	private Vector3 randomFly = Vector3.one;

	// Token: 0x040022CE RID: 8910
	private Vector3 enterpoint = Vector3.one;

	// Token: 0x040022CF RID: 8911
	private const float rubberNumber = 15f;

	// Token: 0x040022D0 RID: 8912
	private float longNow;

	// Token: 0x040022D1 RID: 8913
	public Vector3 target;

	// Token: 0x040022D2 RID: 8914
	private Vector3 center;

	// Token: 0x040022D3 RID: 8915
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x040022D4 RID: 8916
	private int show;

	// Token: 0x040022D5 RID: 8917
	private Vector3 targetV3;

	// Token: 0x040022D6 RID: 8918
	private Vector3 targetV3Temp;

	// Token: 0x040022D7 RID: 8919
	private Vector3 targetV3MoveTemp;

	// Token: 0x040022D8 RID: 8920
	private Vector3 targetV3MoveDir;

	// Token: 0x040022D9 RID: 8921
	private Transform shengziTransform;

	// Token: 0x040022DA RID: 8922
	private Transform ropeTransform;

	// Token: 0x040022DB RID: 8923
	private Transform zhuaTransform;

	// Token: 0x040022DC RID: 8924
	private float rubbleLong;

	// Token: 0x040022DD RID: 8925
	private float lastDis = float.MaxValue;

	// Token: 0x040022DE RID: 8926
	private float nomoveTime;
}
