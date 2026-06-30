using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class CTestQu : MonoBehaviour
{
	// Token: 0x060007C3 RID: 1987 RVA: 0x00043C30 File Offset: 0x00041E30
	private void OnGUI()
	{
		string text = string.Format("当前度数:{0:F},Tan:{1:F3}", this.angle, Mathf.Tan(0.017453292f * this.angle));
		GUI.Label(new Rect(0f, 0f, 680f, 50f), text);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00043C8C File Offset: 0x00041E8C
	private void Update()
	{
		int num = this.show.Length;
		Vector3 forward = base.transform.forward;
		Vector3 position = base.transform.position;
		Debug.DrawRay(base.transform.position, forward * 150f, Color.red);
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = position + forward;
			Quaternion rotation = Quaternion.AngleAxis((float)i / (float)num * 360f, -base.transform.forward);
			Vector3 normalized = (rotation * base.transform.right).normalized;
			Debug.DrawRay(vector, normalized, Color.yellow);
			Vector3 vector2 = vector + normalized * Mathf.Tan(0.017453292f * this.angle);
			Debug.DrawLine(vector, vector2, Color.blue);
			Debug.DrawLine(position, vector2, Color.green);
			Quaternion rotation2 = Quaternion.LookRotation((vector2 - position).normalized);
			this.show[i].rotation = rotation2;
		}
	}

	// Token: 0x040005C4 RID: 1476
	public Transform[] show;

	// Token: 0x040005C5 RID: 1477
	public float angle = 5f;
}
