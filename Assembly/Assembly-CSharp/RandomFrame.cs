using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class RandomFrame : MonoBehaviour
{
	// Token: 0x0600206F RID: 8303 RVA: 0x001297D0 File Offset: 0x001279D0
	public void Awake()
	{
		float length = base.GetComponent<Animation>()["modo_Anim"].length;
		float time = UnityEngine.Random.Range(0f, length);
		base.GetComponent<Animation>()["modo_Anim"].time = time;
	}
}
