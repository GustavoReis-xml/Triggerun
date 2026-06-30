using System;
using UnityEngine;

// Token: 0x020004DB RID: 1243
[AddComponentMenu("Camera-Control/Mouse Orbit")]
public class _LushLODTreesMouseOrbit : MonoBehaviour
{
	// Token: 0x06002F8B RID: 12171 RVA: 0x001AA6C4 File Offset: 0x001A88C4
	public void Awake()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		bool flag = base.GetComponent<Rigidbody>() != null;
		if (flag)
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x001AA718 File Offset: 0x001A8918
	public void LateUpdate()
	{
		bool flag = this.target != null;
		if (flag)
		{
			bool key = Input.GetKey(KeyCode.Mouse1);
			if (key)
			{
				this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
				this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			}
			float num = 0.01f;
			bool flag2 = Input.GetAxis("Mouse ScrollWheel") < 0f - num || Input.GetAxis("Mouse ScrollWheel") > num;
			if (flag2)
			{
				this.distance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
			}
			this.y = _LushLODTreesMouseOrbit.ClampAngle(this.y, (float)this.yMinLimit, (float)this.yMaxLimit);
			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position = rotation * new Vector3(0f, 0f, 0f - this.distance) + this.target.position;
			base.transform.rotation = rotation;
			base.transform.position = position;
		}
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x001AA868 File Offset: 0x001A8A68
	public static float ClampAngle(float angle, float min, float max)
	{
		bool flag = angle < -360f;
		if (flag)
		{
			angle += 360f;
		}
		bool flag2 = angle > 360f;
		if (flag2)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x04002970 RID: 10608
	public Transform target;

	// Token: 0x04002971 RID: 10609
	public float distance = 10f;

	// Token: 0x04002972 RID: 10610
	public float xSpeed = 250f;

	// Token: 0x04002973 RID: 10611
	public float ySpeed = 120f;

	// Token: 0x04002974 RID: 10612
	public int yMinLimit = -20;

	// Token: 0x04002975 RID: 10613
	public int yMaxLimit = 80;

	// Token: 0x04002976 RID: 10614
	private float x;

	// Token: 0x04002977 RID: 10615
	private float y;
}
