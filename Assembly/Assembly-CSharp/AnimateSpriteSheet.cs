using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000A RID: 10
internal class AnimateSpriteSheet : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600002E RID: 46 RVA: 0x00003C6C File Offset: 0x00001E6C
	public float RunTimeInSeconds
	{
		get
		{
			return 1f / this.FramesPerSecond * (float)(this.Columns * this.Rows);
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003C8C File Offset: 0x00001E8C
	private void Start()
	{
		this.materialCopy = new Material(base.GetComponent<Renderer>().sharedMaterial);
		base.GetComponent<Renderer>().sharedMaterial = this.materialCopy;
		Vector2 scale = new Vector2(1f / (float)this.Columns, 1f / (float)this.Rows);
		base.GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", scale);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003CFA File Offset: 0x00001EFA
	private void OnEnable()
	{
		base.StartCoroutine(this.UpdateTiling());
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003D0A File Offset: 0x00001F0A
	private IEnumerator UpdateTiling()
	{
		float x = 0f;
		float y = 0f;
		Vector2 offset = Vector2.zero;
		do
		{
			int num;
			for (int i = this.Rows - 1; i >= 0; i = num - 1)
			{
				y = (float)i / (float)this.Rows;
				for (int j = 0; j <= this.Columns - 1; j = num + 1)
				{
					x = (float)j / (float)this.Columns;
					offset.Set(x, y);
					base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
					yield return new WaitForSeconds(1f / this.FramesPerSecond);
					num = j;
				}
				num = i;
			}
		}
		while (!this.RunOnce);
		yield break;
	}

	// Token: 0x04000077 RID: 119
	public int Columns = 5;

	// Token: 0x04000078 RID: 120
	public int Rows = 5;

	// Token: 0x04000079 RID: 121
	public float FramesPerSecond = 10f;

	// Token: 0x0400007A RID: 122
	public bool RunOnce = true;

	// Token: 0x0400007B RID: 123
	private Material materialCopy;
}
