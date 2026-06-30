using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class CTestScale : MonoBehaviour
{
	// Token: 0x060007CD RID: 1997 RVA: 0x000441E4 File Offset: 0x000423E4
	private void Start()
	{
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000441E7 File Offset: 0x000423E7
	private void test(int a)
	{
		this.t1.transform.eulerAngles = -this.t1.parent.eulerAngles;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00044210 File Offset: 0x00042410
	private void OnGUI()
	{
		bool flag = GUI.Button(new Rect(1f, 100f, 60f, 20f), "test1");
		if (flag)
		{
			this.test(1);
		}
		bool flag2 = GUI.Button(new Rect(120f, 200f, 60f, 20f), "test2");
		if (flag2)
		{
			this.test(2);
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00044280 File Offset: 0x00042480
	private void Update()
	{
		string[] array = new string[5];
		array[0] = this.parentTrs.localScale.ToString("F1");
		array[1] = ",";
		string[] array2 = array;
		Vector3 vector = new Vector3(1f / this.parentTrs.localScale.x, 1f / this.parentTrs.localScale.y, 1f / this.parentTrs.localScale.z);
		array2[2] = vector.ToString("F1");
		array2[3] = ",";
		Vector3 vector2 = new Vector3(this.parentTrs.localScale.x / this.parentTrs.lossyScale.x, this.parentTrs.localScale.y / this.parentTrs.lossyScale.y, this.parentTrs.localScale.z / this.parentTrs.lossyScale.z);
		array2[4] = vector2.ToString("F1");
		Debug.Log(string.Concat(array2));
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x000443A0 File Offset: 0x000425A0
	private void CopyGameObject(GameObject copy, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(copy);
		gameObject.transform.parent = parent;
		gameObject.transform.position = copy.transform.position;
		gameObject.transform.rotation = copy.transform.rotation;
		Vector3 lossyScale = copy.transform.lossyScale;
		Vector3 lossyScale2 = parent.lossyScale;
		gameObject.transform.localScale = new Vector3(lossyScale.x / lossyScale2.x, lossyScale.y / lossyScale2.y, lossyScale.z / lossyScale2.z);
	}

	// Token: 0x040005CA RID: 1482
	public Transform t1;

	// Token: 0x040005CB RID: 1483
	public Transform parentTrs;
}
