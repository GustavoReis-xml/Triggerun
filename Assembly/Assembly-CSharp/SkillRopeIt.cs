using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class SkillRopeIt : SkillBaseIt
{
	// Token: 0x060027BE RID: 10174 RVA: 0x001660A5 File Offset: 0x001642A5
	private void Start()
	{
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x001660A8 File Offset: 0x001642A8
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x001660BC File Offset: 0x001642BC
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		bool flag = this.shengziTransform != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
		}
		this.shengziTransform = Fun.FindGameObjectChild(base.gameplayer.userRoot.transform.gameObject, "shengzi", false);
		this.ropeTransform = Fun.FindGameObjectChild(base.gameplayer.userRoot.transform.gameObject, "Bip001 L Forearm", false);
		this.shengziTransform.transform.parent = base.gameplayer.userRoot.transform;
		this.zhuaTransform = Fun.FindGameObjectChild(this.shengziTransform.gameObject, "zhuazi", false);
		this.skinRender = this.shengziTransform.GetComponentInChildren<SkinnedMeshRenderer>();
		this.finish();
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x00166198 File Offset: 0x00164398
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

	// Token: 0x060027C2 RID: 10178 RVA: 0x0016623C File Offset: 0x0016443C
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
			Vector3 normalized2 = (vector - this.ropeTransform.position).normalized;
			float d = Vector3.Distance(this.ropeTransform.position, vector);
			this.targetV3MoveTemp = this.ropeTransform.position + normalized2 * d;
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
				}
				else
				{
					this.show = 2;
					base.gameplayer.charMove.state.fly = true;
					base.gameplayer.tChar.moveByControlIt = true;
				}
			}
		}
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x0016654C File Offset: 0x0016474C
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

	// Token: 0x060027C4 RID: 10180 RVA: 0x0016671C File Offset: 0x0016491C
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

	// Token: 0x060027C5 RID: 10181 RVA: 0x001668DE File Offset: 0x00164ADE
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.gameplayer.charMove.state.fly = false;
		base.gameplayer.tChar.moveByControlIt = false;
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x00166910 File Offset: 0x00164B10
	private void showLineMove()
	{
		bool flag = this.show != 2;
		if (!flag)
		{
			float num = Vector3.Distance(base.transform.position, this.targetV3MoveTemp);
			this.zhuaTransform.eulerAngles = this.rotZhuazi;
			bool flag2 = num >= this.lastDis;
			if (flag2)
			{
				this.movenum++;
				bool flag3 = this.movenum > 10;
				if (flag3)
				{
					this.finish();
					this.OneSkillFinish();
				}
			}
			else
			{
				this.movenum = 0;
			}
			this.lastDis = num;
		}
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x001669AC File Offset: 0x00164BAC
	private void finish()
	{
		this.show = -1;
		this.movenum = 0;
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

	// Token: 0x060027C8 RID: 10184 RVA: 0x00166A61 File Offset: 0x00164C61
	private void LateUpdate()
	{
		this.InitFirst();
		this.showLine();
		this.showLineMove();
		this.getRope();
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x00166A80 File Offset: 0x00164C80
	public override void OneSkillActionUpdate(ArrayList list)
	{
		bool flag = !(this.shengziTransform == null);
		if (flag)
		{
			ArrayList arrayList = (ArrayList)list[2];
			this.target = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			ArrayList arrayList2 = (ArrayList)list[3];
			this.rotZhuazi = new Vector3((float)((int)arrayList2[0]) * 0.01f, (float)((int)arrayList2[1]) * 0.01f, (float)((int)arrayList2[2]) * 0.01f);
			this.Init();
		}
	}

	// Token: 0x040022AC RID: 8876
	private SkinnedMeshRenderer skinRender;

	// Token: 0x040022AD RID: 8877
	private const float eachDis = 0.15f;

	// Token: 0x040022AE RID: 8878
	private const float longMax = 30.000002f;

	// Token: 0x040022AF RID: 8879
	private const float longSpeed = 100f;

	// Token: 0x040022B0 RID: 8880
	private const float longGetSpeed = 300f;

	// Token: 0x040022B1 RID: 8881
	private const float outUnit = 0.5f;

	// Token: 0x040022B2 RID: 8882
	private Vector3 randomFly = Vector3.one;

	// Token: 0x040022B3 RID: 8883
	private Vector3 enterpoint = Vector3.one;

	// Token: 0x040022B4 RID: 8884
	private const float rubberNumber = 15f;

	// Token: 0x040022B5 RID: 8885
	private float longNow;

	// Token: 0x040022B6 RID: 8886
	public Vector3 target;

	// Token: 0x040022B7 RID: 8887
	private Vector3 center;

	// Token: 0x040022B8 RID: 8888
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x040022B9 RID: 8889
	private int show;

	// Token: 0x040022BA RID: 8890
	private Vector3 targetV3;

	// Token: 0x040022BB RID: 8891
	private Vector3 targetV3Temp;

	// Token: 0x040022BC RID: 8892
	private Vector3 targetV3MoveTemp;

	// Token: 0x040022BD RID: 8893
	private Transform shengziTransform;

	// Token: 0x040022BE RID: 8894
	private Transform ropeTransform;

	// Token: 0x040022BF RID: 8895
	private Transform zhuaTransform;

	// Token: 0x040022C0 RID: 8896
	private float rubbleLong;

	// Token: 0x040022C1 RID: 8897
	private float lastDis = float.MaxValue;

	// Token: 0x040022C2 RID: 8898
	private int movenum;

	// Token: 0x040022C3 RID: 8899
	private Vector3 rotZhuazi = Vector3.zero;
}
