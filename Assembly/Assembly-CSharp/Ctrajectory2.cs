using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class Ctrajectory2 : MonoBehaviour
{
	// Token: 0x060007E7 RID: 2023 RVA: 0x000449FC File Offset: 0x00042BFC
	private void Init()
	{
		this.point = new List<Vector3>();
		int num = 0;
		while ((float)num < 10f)
		{
			Vector3 a = Vector3.Lerp(this.gameOjbet_tran[0].position, this.gameOjbet_tran[1].position, (float)num / 10f);
			Vector3 b = Vector3.Lerp(this.gameOjbet_tran[1].position, this.gameOjbet_tran[2].position, (float)num / 10f);
			Vector3 item = Vector3.Lerp(a, b, (float)num / 10f);
			this.point.Add(item);
			num++;
		}
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00044AB4 File Offset: 0x00042CB4
	private string showString(Vector3 v)
	{
		return string.Format(" new Vector3({0}f,{1}f,{2}f) ", v.x, v.y, v.z);
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00044AF4 File Offset: 0x00042CF4
	private void OnDrawGizmos()
	{
		this.Init();
		Gizmos.color = Color.yellow;
		for (int i = 0; i < this.point.Count - 1; i++)
		{
			Gizmos.DrawLine(this.point[i], this.point[i + 1]);
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00044B52 File Offset: 0x00042D52
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00044B5C File Offset: 0x00042D5C
	private void FixedUpdate()
	{
		bool flag = this.point.Count != 0;
		if (flag)
		{
			Vector3 forward = this.gameOjbet_tran[0].transform.forward;
			Vector3 normalized = (this.gameOjbet_tran[2].transform.position - this.gameOjbet_tran[0].transform.position).normalized;
			Debug.DrawRay(this.gameOjbet_tran[0].transform.position, forward * 100f, Color.red);
			Debug.DrawRay(this.gameOjbet_tran[0].transform.position, normalized * 100f, Color.green);
			Vector3 vector = Vector3.Cross(forward, normalized);
			float num = Vector3.Angle(forward, normalized);
			Debug.Log(string.Format("d1:{0},d2:{1},angle:{2},cross:{3}", new object[]
			{
				forward,
				normalized,
				num,
				vector
			}));
			float num2 = Vector3.Distance(this.gameOjbet_tran[0].transform.position, this.gameOjbet_tran[2].transform.position);
			float d = num2 * (1f - num / 180f);
			this.gameOjbet_tran[1].transform.position = this.gameOjbet_tran[0].transform.position + forward * d;
			this.addTime += Time.deltaTime;
			this.nowPoint = this.Speed * this.addTime;
			float num3 = this.nowPoint / 10f;
			float num4 = Mathf.Lerp(0f, 9f, num3);
			int num5 = (int)num4;
			float t = num4 - (float)num5;
			int num6 = num5 + 1;
			bool flag2 = num6 >= this.point.Count;
			if (flag2)
			{
				num6 = num5;
			}
			this.ball.transform.localPosition = Vector3.Lerp(this.point[num5], this.point[num6], t);
			bool flag3 = num3 >= 1f;
			if (flag3)
			{
				this.addTime = 0f;
			}
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00044DBF File Offset: 0x00042FBF
	private void OnTriggerEnter(Collider collision)
	{
	}

	// Token: 0x040005DB RID: 1499
	public List<Transform> gameOjbet_tran = new List<Transform>();

	// Token: 0x040005DC RID: 1500
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x040005DD RID: 1501
	public GameObject ball;

	// Token: 0x040005DE RID: 1502
	public float Speed = 1f;

	// Token: 0x040005DF RID: 1503
	private const float maxLong = 10f;

	// Token: 0x040005E0 RID: 1504
	private float addTime;

	// Token: 0x040005E1 RID: 1505
	private float nowPoint;
}
