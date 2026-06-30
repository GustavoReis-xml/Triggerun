using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200035C RID: 860
public class SkillPropeIt : SkillBaseIt
{
	// Token: 0x06002777 RID: 10103 RVA: 0x0016399C File Offset: 0x00161B9C
	private void OnDestroy()
	{
		bool flag = this.shengziTransform != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.shengziTransform.gameObject);
		}
	}

	// Token: 0x06002778 RID: 10104 RVA: 0x001639D0 File Offset: 0x00161BD0
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
		this.meshRender = this.zhuaTransform.GetComponentInChildren<MeshRenderer>();
		this.SkinCastOn();
		this.finish();
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x00163AC4 File Offset: 0x00161CC4
	private void Init()
	{
		this.show = 1;
		this.shengziTime = 0f;
		this.longNow = Time.time;
		this.rubbleLong = 0f;
		this.lastDis = float.MaxValue;
		this.randomFly = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
		this.shengziTransform.transform.position = this.ropeTransform.transform.position;
		this.shengziTransform.LookAt(this.target);
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x00163B70 File Offset: 0x00161D70
	private void InitFirst()
	{
		bool flag = this.show != 1;
		if (!flag)
		{
			this.SkinCastOn();
			this.ShengZiShow(true);
			Transform[] bones = this.skinRender.bones;
			bool flag2 = false;
			this.targetV3 = this.target;
			Vector3 normalized = (this.targetV3 - this.ropeTransform.position).normalized;
			this.targetV3Temp = this.targetV3;
			float num = Vector3.Distance(this.ropeTransform.position, this.targetV3);
			bool flag3 = num > 20f;
			if (flag3)
			{
				this.targetV3Temp = 20f * normalized + this.ropeTransform.position;
				num = 20f;
				flag2 = true;
			}
			Vector3 vector = this.targetV3Temp;
			Vector3 normalized2 = (vector - this.ropeTransform.position).normalized;
			float d = Vector3.Distance(this.ropeTransform.position, vector);
			this.targetV3MoveTemp = this.ropeTransform.position + normalized2 * d;
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
					this.shengziTime = Time.time;
				}
			}
		}
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x00163E88 File Offset: 0x00162088
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

	// Token: 0x0600277C RID: 10108 RVA: 0x00164058 File Offset: 0x00162258
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
				this.finish();
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x0016421A File Offset: 0x0016241A
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.gameplayer.charMove.state.fly = false;
		base.gameplayer.tChar.moveByControlIt = false;
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x0016424C File Offset: 0x0016244C
	private void showLineMove()
	{
		bool flag = this.show != 2;
		if (!flag)
		{
			float num = Vector3.Distance(base.transform.position, this.targetV3MoveTemp);
			this.zhuaTransform.eulerAngles = this.rotZhuazi;
			bool flag2 = num == this.lastDis;
			if (flag2)
			{
				this.movenum++;
				bool flag3 = this.movenum > 3;
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
			bool flag4 = num > this.lastDis && Time.time - this.shengziTime > 0.2f;
			if (flag4)
			{
				this.finish();
				this.OneSkillFinish();
			}
			this.lastDis = num;
			bool flag5 = num < 2.1f;
			if (flag5)
			{
				this.finish();
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x00164338 File Offset: 0x00162538
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
		this.ShengZiShow(false);
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x001643E3 File Offset: 0x001625E3
	private void LateUpdate()
	{
		this.InitFirst();
		this.showLine();
		this.showLineMove();
		this.getRope();
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x00164404 File Offset: 0x00162604
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

	// Token: 0x06002782 RID: 10114 RVA: 0x001644D4 File Offset: 0x001626D4
	public void ShengZiShow(bool hide)
	{
		bool flag = this.shengziTransform.gameObject.activeSelf != hide;
		if (flag)
		{
			this.shengziTransform.gameObject.SetActive(hide);
		}
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x00164510 File Offset: 0x00162710
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

	// Token: 0x0400226D RID: 8813
	private SkinnedMeshRenderer skinRender;

	// Token: 0x0400226E RID: 8814
	private MeshRenderer meshRender;

	// Token: 0x0400226F RID: 8815
	private const float eachDis = 0.1f;

	// Token: 0x04002270 RID: 8816
	private const float longMax = 20f;

	// Token: 0x04002271 RID: 8817
	private const float longSpeed = 100f;

	// Token: 0x04002272 RID: 8818
	private const float longGetSpeed = 300f;

	// Token: 0x04002273 RID: 8819
	private const float outUnit = 0.5f;

	// Token: 0x04002274 RID: 8820
	private Vector3 randomFly = Vector3.one;

	// Token: 0x04002275 RID: 8821
	private Vector3 enterpoint = Vector3.one;

	// Token: 0x04002276 RID: 8822
	private const float rubberNumber = 15f;

	// Token: 0x04002277 RID: 8823
	private float longNow;

	// Token: 0x04002278 RID: 8824
	private float shengziTime;

	// Token: 0x04002279 RID: 8825
	public Vector3 target;

	// Token: 0x0400227A RID: 8826
	private Vector3 center;

	// Token: 0x0400227B RID: 8827
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x0400227C RID: 8828
	private int show;

	// Token: 0x0400227D RID: 8829
	private Vector3 targetV3;

	// Token: 0x0400227E RID: 8830
	private Vector3 targetV3Temp;

	// Token: 0x0400227F RID: 8831
	private Vector3 targetV3MoveTemp;

	// Token: 0x04002280 RID: 8832
	private Transform shengziTransform;

	// Token: 0x04002281 RID: 8833
	private Transform ropeTransform;

	// Token: 0x04002282 RID: 8834
	private Transform zhuaTransform;

	// Token: 0x04002283 RID: 8835
	private float rubbleLong;

	// Token: 0x04002284 RID: 8836
	private float lastDis = float.MaxValue;

	// Token: 0x04002285 RID: 8837
	private int movenum;

	// Token: 0x04002286 RID: 8838
	private Vector3 rotZhuazi = Vector3.zero;
}
