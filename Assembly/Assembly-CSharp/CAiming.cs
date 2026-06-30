using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class CAiming : MonoBehaviour
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600023E RID: 574 RVA: 0x0000F834 File Offset: 0x0000DA34
	public NetLiving netobject
	{
		get
		{
			return this.hero;
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000F83C File Offset: 0x0000DA3C
	public void AimEnable(bool enab)
	{
		if (enab)
		{
			this.weightMax = 1f;
		}
		else
		{
			this.weightMax = 0f;
		}
		for (int i = 0; i < this.aimStruct.Length; i++)
		{
			for (int j = 0; j < this.aimStruct[i].aims.Length; j++)
			{
				this.aimStruct[i].aims[j].enabled = enab;
			}
		}
		base.enabled = enab;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000F8CC File Offset: 0x0000DACC
	public void SetEnable(bool enable)
	{
		this.nowGroundIKEnable = enable;
		bool flag = this.groundIkTrs != null && this.groundIkTrs.gameObject.activeSelf != enable;
		if (flag)
		{
			bool flag2 = enable && this.hero.disCameraNow > this.footDisCheckMax;
			if (flag2)
			{
				enable = false;
			}
			this.groundIkTrs.localPosition = Vector3.zero;
			this.groundIkTrs.gameObject.SetActive(enable);
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000F954 File Offset: 0x0000DB54
	private void InitComm(NetLiving player)
	{
		this.taregetLerp = this.pin.transform.position;
		this.animator = base.GetComponent<Animator>();
		this.groundIk = base.GetComponentInChildren<GrounderIK>();
		bool flag = this.groundIk != null;
		if (flag)
		{
			this.groundIk.solver.pelvisSpeed = 100f;
			this.groundIkTrs = this.groundIk.transform;
			this.groundIk.characterRoot = player.UserControl.transform;
			bool activeSelf = this.groundIkTrs.gameObject.activeSelf;
			if (activeSelf)
			{
				this.groundIkTrs.gameObject.SetActive(false);
			}
		}
		for (int i = 0; i < this.aimStruct.Length; i++)
		{
			for (int j = 0; j < this.aimStruct[i].aims.Length; j++)
			{
				this.aimStruct[i].aims[j].solver.clampWeight = 0f;
				this.aimStruct[i].aims[j].solver.clampSmoothing = 0;
				this.aimStruct[i].aims[j].solver.useRotationLimits = false;
			}
		}
		this.inited = true;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
	public void InitAimSystem(NetLiving player, Transform tar)
	{
		this.hero = player;
		this.target = tar;
		this.InitComm(player);
		bool flag = player.userModelFps != null;
		if (flag)
		{
			DynamicBone[] components = player.userModelFps.GetComponents<DynamicBone>();
			foreach (DynamicBone dynamicBone in components)
			{
				dynamicBone.netobject = player;
			}
		}
		bool flag2 = player.userModelTps != null;
		if (flag2)
		{
			DynamicBone[] components2 = player.userModelTps.GetComponents<DynamicBone>();
			foreach (DynamicBone dynamicBone2 in components2)
			{
				dynamicBone2.netobject = player;
			}
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000FB60 File Offset: 0x0000DD60
	private void LateUpdate()
	{
		bool flag = !this.inited || this.hero.isDead || this.hero.haveDestroy || !this.hero.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = (this.hero.disCameraNow > this.footDisCheckMax || this.hero.publicInfo.eyeBlock || !this.hero.tChar.onGround || QualitySettings.GetQualityLevel() == 0 || !this.nowGroundIKEnable || this.hero.nHuDongState >= 200) && !this.b_closeDisCheck;
			if (flag2)
			{
				bool flag3 = this.groundIk.weight > 0.001f;
				if (flag3)
				{
					this.groundIk.weight = Mathf.Lerp(this.groundIk.weight, 0.0001f, Time.deltaTime * 5f);
					bool flag4 = this.groundIk.weight < 0.001f && this.groundIkTrs.gameObject.activeSelf;
					if (flag4)
					{
						this.groundIkTrs.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				this.groundIk.weight = this.groundTrsWeight;
				bool flag5 = !this.groundIkTrs.gameObject.activeSelf;
				if (flag5)
				{
					this.groundIkTrs.gameObject.SetActive(true);
				}
			}
			for (int i = 0; i < this.aimStruct.Length; i++)
			{
				bool flag6 = this.animator.GetBool(this.aimStruct[i].animatorParm);
				bool flag7 = !this.b_closeDisCheck && this.hero.publicInfo.eyeBlock;
				if (flag7)
				{
					flag6 = false;
				}
				for (int j = 0; j < this.aimStruct[i].aims.Length; j++)
				{
					bool flag8 = flag6;
					if (flag8)
					{
						bool flag9 = !this.aimStruct[i].aims[j].isEnable;
						if (flag9)
						{
							this.aimStruct[i].aims[j].isEnable = true;
						}
						Vector3 position = this.target.transform.position;
						position.y = base.transform.position.y;
						Vector3 normalized = (position - base.transform.position).normalized;
						float t = Vector3.Dot(base.transform.forward, normalized) + 1f;
						float b = Mathf.Lerp(0f, this.weightMax, t);
						this.aimStruct[i].weight = Mathf.Lerp(this.aimStruct[i].weight, b, Time.deltaTime * this.speedWeight);
						this.aimStruct[i].aims[j].solver.SetIKPositionWeight(this.aimStruct[i].weight);
						this.aimStruct[i].aims[j].solver.transform.LookAt(this.pin.position);
						this.taregetLerp = Vector3.Lerp(this.taregetLerp, this.target.transform.position, Time.deltaTime * this.speedWeight);
						this.aimStruct[i].aims[j].solver.IKPosition = this.taregetLerp;
					}
					else
					{
						bool isEnable = this.aimStruct[i].aims[j].isEnable;
						if (isEnable)
						{
							this.aimStruct[i].weight = Mathf.Lerp(this.aimStruct[i].weight, 0f, Time.deltaTime * this.speedWeight);
							this.aimStruct[i].aims[j].solver.SetIKPositionWeight(this.aimStruct[i].weight);
							this.aimStruct[i].aims[j].solver.transform.LookAt(this.pin.position);
							this.taregetLerp = Vector3.Lerp(this.taregetLerp, this.target.transform.position, Time.deltaTime * this.speedWeight);
							this.aimStruct[i].aims[j].solver.IKPosition = this.taregetLerp;
							bool flag10 = this.aimStruct[i].weight < 0.001f;
							if (flag10)
							{
								this.aimStruct[i].weight = 0f;
								this.aimStruct[i].aims[j].isEnable = false;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040001C9 RID: 457
	public Transform target;

	// Token: 0x040001CA RID: 458
	public Transform pin;

	// Token: 0x040001CB RID: 459
	public CAiming.AimStruct[] aimStruct;

	// Token: 0x040001CC RID: 460
	[Tooltip("Reference to the Animator component.")]
	private Animator animator;

	// Token: 0x040001CD RID: 461
	private GrounderIK groundIk;

	// Token: 0x040001CE RID: 462
	private Transform groundIkTrs;

	// Token: 0x040001CF RID: 463
	private float groundTrsWeight = 1f;

	// Token: 0x040001D0 RID: 464
	private NetLiving hero;

	// Token: 0x040001D1 RID: 465
	private Vector3 taregetLerp;

	// Token: 0x040001D2 RID: 466
	private float weightMax = 1f;

	// Token: 0x040001D3 RID: 467
	public float speedWeight = 20f;

	// Token: 0x040001D4 RID: 468
	private float footDisCheckMax = 8f;

	// Token: 0x040001D5 RID: 469
	public bool b_closeDisCheck;

	// Token: 0x040001D6 RID: 470
	private bool inited;

	// Token: 0x040001D7 RID: 471
	public bool nowGroundIKEnable = true;

	// Token: 0x02000529 RID: 1321
	[Serializable]
	public struct AimStruct
	{
		// Token: 0x04002BAB RID: 11179
		public string animatorParm;

		// Token: 0x04002BAC RID: 11180
		public float weight;

		// Token: 0x04002BAD RID: 11181
		public AimIK[] aims;
	}
}
