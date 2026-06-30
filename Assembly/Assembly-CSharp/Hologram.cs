using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class Hologram : MonoBehaviour
{
	// Token: 0x060015A6 RID: 5542 RVA: 0x000CA5C9 File Offset: 0x000C87C9
	private void Start()
	{
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000CA5CC File Offset: 0x000C87CC
	private void Awake()
	{
		this.HoloMaterials = this.HoloRenderer.materials;
		this.clippower = this.HoloMaterials[0].GetFloat("_ClipPower");
		this.rimpower = this.HoloMaterials[0].GetFloat("_RimPower");
		bool flag = this.FlickerLight != null;
		if (flag)
		{
			this.intensity = this.FlickerLight.intensity;
		}
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000CA63E File Offset: 0x000C883E
	public void EnableScanlines(bool enabled)
	{
		this.clippower = 301f;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000CA64C File Offset: 0x000C884C
	private void Update()
	{
		float num = this.clippower - this.ClipVariance / 2f + UnityEngine.Random.value * this.ClipVariance;
		float value = UnityEngine.Random.value;
		float num2 = value * this.RimVariance;
		float num3 = this.rimpower - this.RimVariance / 2f;
		bool flag = num < 0f;
		if (flag)
		{
			num = 0f;
		}
		bool flag2 = num3 < 0f;
		if (flag2)
		{
			num3 = 0f;
		}
		Material[] holoMaterials = this.HoloMaterials;
		foreach (Material material in holoMaterials)
		{
			material.SetFloat("_RimPower", num3);
			material.SetFloat("_ClipPower", num);
		}
		bool flag3 = this.FlickerLight != null;
		if (flag3)
		{
			bool flag4 = num2 < 0f;
			if (flag4)
			{
				this.FlickerLight.intensity = this.intensity - this.intensity * this.lightflicker * value;
			}
			else
			{
				this.FlickerLight.intensity = this.intensity + this.intensity * this.lightflicker * value;
			}
		}
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000CA77C File Offset: 0x000C897C
	private void OnDestroy()
	{
		Material[] holoMaterials = this.HoloMaterials;
		foreach (Material obj in holoMaterials)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x0400109D RID: 4253
	public float ClipVariance = 5f;

	// Token: 0x0400109E RID: 4254
	public float RimVariance = 0.5f;

	// Token: 0x0400109F RID: 4255
	public float lightflicker = 0.3f;

	// Token: 0x040010A0 RID: 4256
	public Renderer HoloRenderer;

	// Token: 0x040010A1 RID: 4257
	public Light FlickerLight;

	// Token: 0x040010A2 RID: 4258
	private float clippower;

	// Token: 0x040010A3 RID: 4259
	private float rimpower;

	// Token: 0x040010A4 RID: 4260
	private float intensity;

	// Token: 0x040010A5 RID: 4261
	private Material[] HoloMaterials;
}
