using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
public class CHeroRenderFps : MonoBehaviour
{
	// Token: 0x060004E5 RID: 1253 RVA: 0x00028284 File Offset: 0x00026484
	private void Start()
	{
		for (int i = 0; i < this.ShowTransform.Length; i++)
		{
			this.ShowTransform[i].gameObject.SetActive(true);
		}
	}

	// Token: 0x040003BB RID: 955
	public Transform[] ShowTransform;
}
