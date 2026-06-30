using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class DamgeTextParabola : MonoBehaviour
{
	// Token: 0x06000894 RID: 2196 RVA: 0x00048C70 File Offset: 0x00046E70
	private void Start()
	{
		Vector3 position = base.gameObject.transform.position;
		Vector3[] array = new Vector3[3];
		float num = UnityEngine.Random.value * 0.5f;
		float num2 = UnityEngine.Random.value * 0.1f;
		float num3 = num / num2;
		float num4 = UnityEngine.Random.value * 0.002f;
		bool flag = UnityEngine.Random.value > 0.5f;
		if (flag)
		{
			position.x += 0.1f;
			base.gameObject.transform.position = position;
			array[0] = new Vector3(position.x, position.y, position.z);
			array[1] = new Vector3(position.x + 0.3f + num, position.y + 2f + num2, position.z);
			array[2] = new Vector3(position.x + 0.6f + 2f * num + num4 * num3, position.y + 1f + num4, position.z);
		}
		else
		{
			position.x -= 0.1f;
			base.gameObject.transform.position = position;
			array[0] = new Vector3(position.x, position.y, position.z);
			array[1] = new Vector3(position.x - 0.3f - num, position.y + 2f + num2, position.z);
			array[2] = new Vector3(position.x - 0.6f - 2f * num - num4 * num3, position.y + 1f + num4, position.z);
		}
		Tweener t = base.gameObject.transform.DOPath(array, 1f, PathType.Linear, PathMode.Full3D, 10, null);
		t.OnComplete(delegate
		{
			this.OnComplete();
		});
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00048EA0 File Offset: 0x000470A0
	private void OnComplete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
