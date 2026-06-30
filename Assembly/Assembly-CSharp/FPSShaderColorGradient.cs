using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class FPSShaderColorGradient : MonoBehaviour
{
	// Token: 0x06000B68 RID: 2920 RVA: 0x0006B170 File Offset: 0x00069370
	private void Start()
	{
		Renderer component = base.GetComponent<Renderer>();
		bool flag = component != null;
		if (flag)
		{
			Material[] materials = component.materials;
			bool flag2 = this.MaterialID >= materials.Length;
			if (flag2)
			{
				Debug.Log("ShaderColorGradient: Material ID more than shader materials count.");
			}
			this.matInstance = materials[this.MaterialID];
		}
		else
		{
			Projector component2 = base.GetComponent<Projector>();
			Material material = component2.material;
			bool flag3 = !material.name.EndsWith("(Instance)");
			if (flag3)
			{
				this.matInstance = new Material(material)
				{
					name = material.name + " (Instance)"
				};
			}
			else
			{
				this.matInstance = material;
			}
			component2.material = this.matInstance;
		}
		bool flag4 = !this.matInstance.HasProperty(this.ShaderProperty);
		if (flag4)
		{
			Debug.Log("ShaderColorGradient: Shader not have \"" + this.ShaderProperty + "\" property");
		}
		this.propertyID = Shader.PropertyToID(this.ShaderProperty);
		this.oldColor = this.matInstance.GetColor(this.propertyID);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0006B295 File Offset: 0x00069495
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0006B2AC File Offset: 0x000694AC
	private void Update()
	{
		float num = Time.time - this.startTime;
		bool flag = this.canUpdate;
		if (flag)
		{
			Color a = this.Color.Evaluate(num / this.TimeMultiplier);
			this.matInstance.SetColor(this.propertyID, a * this.oldColor);
		}
		bool flag2 = num >= this.TimeMultiplier;
		if (flag2)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x040009D4 RID: 2516
	public string ShaderProperty = "_TintColor";

	// Token: 0x040009D5 RID: 2517
	public int MaterialID;

	// Token: 0x040009D6 RID: 2518
	public Gradient Color = new Gradient();

	// Token: 0x040009D7 RID: 2519
	public float TimeMultiplier = 1f;

	// Token: 0x040009D8 RID: 2520
	private bool canUpdate;

	// Token: 0x040009D9 RID: 2521
	private Material matInstance;

	// Token: 0x040009DA RID: 2522
	private int propertyID;

	// Token: 0x040009DB RID: 2523
	private float startTime;

	// Token: 0x040009DC RID: 2524
	private Color oldColor;
}
