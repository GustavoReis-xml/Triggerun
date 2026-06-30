using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class CAimingFps : MonoBehaviour
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000245 RID: 581 RVA: 0x000100C8 File Offset: 0x0000E2C8
	public NetLiving netobject
	{
		get
		{
			return this.hero;
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x000100D0 File Offset: 0x0000E2D0
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

	// Token: 0x06000247 RID: 583 RVA: 0x00010160 File Offset: 0x0000E360
	private void InitComm(NetLiving player)
	{
		this.taregetLerp = this.pin.transform.position;
		this.animator = base.GetComponent<Animator>();
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

	// Token: 0x06000248 RID: 584 RVA: 0x00010238 File Offset: 0x0000E438
	public void InitAimSystem(NetLiving player, Transform tar)
	{
		this.hero = player;
		this.target = tar;
		this.InitComm(player);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00010254 File Offset: 0x0000E454
	private void LateUpdate()
	{
		bool flag = !this.inited || this.hero == null || this.hero.isDead || this.hero.haveDestroy || !this.hero.playerHaveCreate;
		if (!flag)
		{
			for (int i = 0; i < this.aimStruct.Length; i++)
			{
				bool @bool = this.animator.GetBool(this.aimStruct[i].animatorParm);
				for (int j = 0; j < this.aimStruct[i].aims.Length; j++)
				{
					bool flag2 = @bool;
					if (flag2)
					{
						bool flag3 = !this.aimStruct[i].aims[j].isEnable;
						if (flag3)
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
							bool flag4 = this.aimStruct[i].weight < 0.001f;
							if (flag4)
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

	// Token: 0x040001D8 RID: 472
	public Transform target;

	// Token: 0x040001D9 RID: 473
	public Transform pin;

	// Token: 0x040001DA RID: 474
	public CAimingFps.AimStruct[] aimStruct;

	// Token: 0x040001DB RID: 475
	[Tooltip("Reference to the Animator component.")]
	private Animator animator;

	// Token: 0x040001DC RID: 476
	private NetLiving hero;

	// Token: 0x040001DD RID: 477
	private Vector3 taregetLerp;

	// Token: 0x040001DE RID: 478
	private float weightMax = 1f;

	// Token: 0x040001DF RID: 479
	public float speedWeight = 20f;

	// Token: 0x040001E0 RID: 480
	private bool inited;

	// Token: 0x0200052A RID: 1322
	[Serializable]
	public struct AimStruct
	{
		// Token: 0x04002BAE RID: 11182
		public string animatorParm;

		// Token: 0x04002BAF RID: 11183
		public float weight;

		// Token: 0x04002BB0 RID: 11184
		public AimIK[] aims;
	}
}
