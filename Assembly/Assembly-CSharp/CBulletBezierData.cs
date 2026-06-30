using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class CBulletBezierData : CBulletData
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001572B File Offset: 0x0001392B
	public static int listLength
	{
		get
		{
			return CBulletBezierData.bezierList.Count;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060002E3 RID: 739 RVA: 0x00015738 File Offset: 0x00013938
	// (set) Token: 0x060002E4 RID: 740 RVA: 0x00015750 File Offset: 0x00013950
	public int bezierRate
	{
		get
		{
			return this._bezierRate;
		}
		set
		{
			this._bezierRate = value;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0001575C File Offset: 0x0001395C
	private static void outPut(List<Vector3> points, out List<Vector3> point)
	{
		point = new List<Vector3>();
		int num = 0;
		while ((float)num < CBulletBezierData.maxLong)
		{
			Vector3 a = Vector3.Lerp(points[0], points[1], (float)num / CBulletBezierData.maxLong);
			Vector3 vector = Vector3.Lerp(points[1], points[2], (float)num / CBulletBezierData.maxLong);
			Vector3 vector2 = Vector3.Lerp(points[2], points[3], (float)num / CBulletBezierData.maxLong);
			Vector3 b = Vector3.Lerp(points[3], points[4], (float)num / CBulletBezierData.maxLong);
			Vector3 a2 = Vector3.Lerp(a, vector, (float)num / CBulletBezierData.maxLong);
			Vector3 vector3 = Vector3.Lerp(vector, vector2, (float)num / CBulletBezierData.maxLong);
			Vector3 b2 = Vector3.Lerp(vector2, b, (float)num / CBulletBezierData.maxLong);
			Vector3 a3 = Vector3.Lerp(a2, vector3, (float)num / CBulletBezierData.maxLong);
			Vector3 b3 = Vector3.Lerp(vector3, b2, (float)num / CBulletBezierData.maxLong);
			Vector3 item = Vector3.Lerp(a3, b3, (float)num / CBulletBezierData.maxLong);
			point.Add(item);
			num++;
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00015878 File Offset: 0x00013A78
	private static void initBezierData()
	{
		for (int i = 0; i < CBulletBezierData.bezierList.Count; i++)
		{
			CBulletBezierData.outPut(CBulletBezierData.bezierList[i].source, out CBulletBezierData.bezierList[i].point);
		}
		CBulletBezierData.initFinish = true;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000158D0 File Offset: 0x00013AD0
	public static List<Vector3> getOneBezier(int no)
	{
		bool flag = !CBulletBezierData.initFinish;
		if (flag)
		{
			CBulletBezierData.initBezierData();
		}
		return CBulletBezierData.bezierList[no].point;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00015908 File Offset: 0x00013B08
	public override void initBulletData(Transform point, int bullet_type, ArrayList mainParam)
	{
		base.initBulletData(point, bullet_type, mainParam);
		ArrayList arrayList = (ArrayList)mainParam[2];
		base.bulletFlySpeed = (float)((int)arrayList[0]);
		this._bezierRate = (int)arrayList[1];
	}

	// Token: 0x0400029A RID: 666
	public static float maxLong = 1000f;

	// Token: 0x0400029B RID: 667
	private static bool initFinish = false;

	// Token: 0x0400029C RID: 668
	private static List<CBezierBase> bezierList = new List<CBezierBase>
	{
		new CBezierBase(new List<Vector3>
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(3.8f, 1.599753f, 15f),
			new Vector3(-16.44f, 1.599753f, 30f),
			new Vector3(-6.4f, 1.599753f, 100f),
			new Vector3(117f, 1.599753f, 1000f)
		}),
		new CBezierBase(new List<Vector3>
		{
			new Vector3(0.05078763f, 0.099753f, 0f),
			new Vector3(3.8f, 1.599753f, 15f),
			new Vector3(10.44f, 1.599753f, 30f),
			new Vector3(-6.4f, 1.599753f, 100f),
			new Vector3(-127.44f, 1.599753f, 1000f)
		}),
		new CBezierBase(new List<Vector3>
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(3.8f, 1.599753f, 15f),
			new Vector3(0.7f, 1.599753f, 30f),
			new Vector3(13.1f, 1.599753f, 100f),
			new Vector3(117f, 1.599753f, 1000f)
		}),
		new CBezierBase(new List<Vector3>
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(1.34f, 1.599753f, 15f),
			new Vector3(6.26f, 1.599753f, 30f),
			new Vector3(13.1f, 1.599753f, 100f),
			new Vector3(117f, 1.599753f, 1000f)
		}),
		new CBezierBase(new List<Vector3>
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0.65f, 1.599753f, 15f),
			new Vector3(-4.1f, 1.599753f, 30f),
			new Vector3(-0.5f, 1.599753f, 100f),
			new Vector3(51f, 1.599753f, 1000f)
		})
	};

	// Token: 0x0400029D RID: 669
	private int _bezierRate;
}
