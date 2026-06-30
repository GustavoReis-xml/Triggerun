using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class Ctrajectory : MonoBehaviour
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x0004461C File Offset: 0x0004281C
	private void Init()
	{
		this.point = new List<Vector3>();
		int num = 0;
		while ((float)num < 1000f)
		{
			Vector3 a = Vector3.Lerp(this.gameOjbet_tran[0].position, this.gameOjbet_tran[1].position, (float)num / 1000f);
			Vector3 vector = Vector3.Lerp(this.gameOjbet_tran[1].position, this.gameOjbet_tran[2].position, (float)num / 1000f);
			Vector3 vector2 = Vector3.Lerp(this.gameOjbet_tran[2].position, this.gameOjbet_tran[3].position, (float)num / 1000f);
			Vector3 b = Vector3.Lerp(this.gameOjbet_tran[3].position, this.gameOjbet_tran[4].position, (float)num / 1000f);
			Vector3 a2 = Vector3.Lerp(a, vector, (float)num / 1000f);
			Vector3 vector3 = Vector3.Lerp(vector, vector2, (float)num / 1000f);
			Vector3 b2 = Vector3.Lerp(vector2, b, (float)num / 1000f);
			Vector3 a3 = Vector3.Lerp(a2, vector3, (float)num / 1000f);
			Vector3 b3 = Vector3.Lerp(vector3, b2, (float)num / 1000f);
			Vector3 item = Vector3.Lerp(a3, b3, (float)num / 1000f);
			this.point.Add(item);
			num++;
		}
		Debug.Log(string.Concat(new string[]
		{
			this.showString(this.gameOjbet_tran[0].position),
			",",
			this.showString(this.gameOjbet_tran[1].position),
			",",
			this.showString(this.gameOjbet_tran[2].position),
			",",
			this.showString(this.gameOjbet_tran[3].position),
			",",
			this.showString(this.gameOjbet_tran[4].position)
		}));
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00044844 File Offset: 0x00042A44
	private string showString(Vector3 v)
	{
		return string.Format(" new Vector3({0}f,{1}f,{2}f) ", v.x, v.y, v.z);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00044884 File Offset: 0x00042A84
	private void OnDrawGizmos()
	{
		this.Init();
		Gizmos.color = Color.yellow;
		for (int i = 0; i < this.point.Count - 1; i++)
		{
			Gizmos.DrawLine(this.point[i], this.point[i + 1]);
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000448E2 File Offset: 0x00042AE2
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x000448EC File Offset: 0x00042AEC
	private void FixedUpdate()
	{
		bool flag = this.point.Count != 0;
		if (flag)
		{
			this.addTime += Time.deltaTime;
			this.nowPoint = this.Speed * this.addTime;
			float num = this.nowPoint / 1000f;
			float num2 = Mathf.Lerp(0f, 999f, num);
			int num3 = (int)num2;
			float t = num2 - (float)num3;
			int num4 = num3 + 1;
			bool flag2 = num4 >= this.point.Count;
			if (flag2)
			{
				num4 = num3;
			}
			this.ball.transform.localPosition = Vector3.Lerp(this.point[num3], this.point[num4], t);
			bool flag3 = num >= 1f;
			if (flag3)
			{
				this.addTime = 0f;
			}
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x000449CE File Offset: 0x00042BCE
	private void OnTriggerEnter(Collider collision)
	{
	}

	// Token: 0x040005D4 RID: 1492
	public List<Transform> gameOjbet_tran = new List<Transform>();

	// Token: 0x040005D5 RID: 1493
	private List<Vector3> point = new List<Vector3>();

	// Token: 0x040005D6 RID: 1494
	public GameObject ball;

	// Token: 0x040005D7 RID: 1495
	public float Speed = 1f;

	// Token: 0x040005D8 RID: 1496
	private const float maxLong = 1000f;

	// Token: 0x040005D9 RID: 1497
	private float addTime;

	// Token: 0x040005DA RID: 1498
	private float nowPoint;
}
