using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DF RID: 991
public class Smooth_Mouse_Look : MonoBehaviour
{
	// Token: 0x06002A48 RID: 10824 RVA: 0x00175AA8 File Offset: 0x00173CA8
	private void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		bool flag = base.GetComponent<Rigidbody>();
		if (flag)
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.origRotation = base.transform.localRotation;
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x00175AF4 File Offset: 0x00173CF4
	private void Update()
	{
		bool flag = !Cursor.visible && Cursor.lockState == CursorLockMode.Locked;
		if (flag)
		{
			this.Rotation();
			this.Position();
		}
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x00175B28 File Offset: 0x00173D28
	private void Rotation()
	{
		this.rotationX += Input_Manager.Get_Axis("Mouse X") * this.sensitivityX * this._Camera_Speed;
		this.rotationsX.Add(this.rotationX);
		this.rotationY += Input_Manager.Get_Axis("Mouse Y") * this.sensitivityY * this._Camera_Speed;
		this.rotationY = this.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
		this.rotationsY.Add(this.rotationY);
		bool key = Input.GetKey(KeyCode.Q);
		if (key)
		{
			this.rotationZ += 0.5f * this.sensitivityZ * this._Camera_Speed;
			this.rotationsZ.Add(this.rotationZ);
		}
		else
		{
			bool key2 = Input.GetKey(KeyCode.E);
			if (key2)
			{
				this.rotationZ += -0.5f * this.sensitivityZ * this._Camera_Speed;
				this.rotationsZ.Add(this.rotationZ);
			}
		}
		float angle = Smooth_Mouse_Look.AverageRotations(this.rotationsX, this.frameCounterX);
		float angle2 = Smooth_Mouse_Look.AverageRotations(this.rotationsY, this.frameCounterY);
		float angle3 = Smooth_Mouse_Look.AverageRotations(this.rotationsZ, this.frameCounterZ);
		this.xQuaternion = Quaternion.AngleAxis(angle, Vector3.up);
		this.yQuaternion = Quaternion.AngleAxis(angle2, Vector3.left);
		bool flag = this.rotationsZ.Count > 0;
		if (flag)
		{
			this.zQuaternion = Quaternion.AngleAxis(angle3, Vector3.forward);
			base.transform.localRotation = this.origRotation * this.xQuaternion * this.yQuaternion * this.zQuaternion;
		}
		else
		{
			base.transform.localRotation = this.origRotation * this.xQuaternion * this.yQuaternion;
		}
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x00175D24 File Offset: 0x00173F24
	private static float AverageRotations(List<float> rotations, int frameCounter)
	{
		float num = 0f;
		bool flag = rotations.Count >= frameCounter;
		if (flag)
		{
			rotations.RemoveAt(0);
		}
		for (int i = 0; i < rotations.Count; i++)
		{
			num += rotations[i];
		}
		return num / (float)rotations.Count;
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x00175D84 File Offset: 0x00173F84
	private float ClampAngle(float angle, float min, float max)
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

	// Token: 0x06002A4D RID: 10829 RVA: 0x00175DCC File Offset: 0x00173FCC
	private void Position()
	{
		this._Horizontal = Input_Manager.Get_Axis("Horizontal");
		this._Vertical = Input_Manager.Get_Axis("Vertical");
		base.gameObject.transform.localPosition += base.gameObject.transform.forward * this._Vertical * Time.deltaTime * this._Move_Speed;
		base.gameObject.transform.localPosition += base.gameObject.transform.right * this._Horizontal * Time.deltaTime * this._Move_Speed;
		this._Up = Input.GetAxis("Up");
		this._Down = Input.GetAxis("Down");
		base.gameObject.transform.localPosition += base.gameObject.transform.up * this._Up * Time.deltaTime * this._Move_Speed;
		base.gameObject.transform.localPosition -= base.gameObject.transform.up * this._Down * Time.deltaTime * this._Move_Speed;
		bool key = Input.GetKey(KeyCode.PageUp);
		if (key)
		{
			this._Move_Speed += this._Addition_Move_Speed;
		}
		bool key2 = Input.GetKey(KeyCode.PageDown);
		if (key2)
		{
			this._Move_Speed -= this._Addition_Move_Speed;
		}
		bool key3 = Input.GetKey(KeyCode.Insert);
		if (key3)
		{
			this._Camera_Speed += this._Addition_Camera_Speed;
		}
		bool key4 = Input.GetKey(KeyCode.Delete);
		if (key4)
		{
			this._Camera_Speed -= this._Addition_Camera_Speed;
		}
	}

	// Token: 0x040023E7 RID: 9191
	public float sensitivityX = 2f;

	// Token: 0x040023E8 RID: 9192
	public float sensitivityY = 2f;

	// Token: 0x040023E9 RID: 9193
	public float sensitivityZ = 2f;

	// Token: 0x040023EA RID: 9194
	public float minimumY = -60f;

	// Token: 0x040023EB RID: 9195
	public float maximumY = 60f;

	// Token: 0x040023EC RID: 9196
	public int frameCounterX = 20;

	// Token: 0x040023ED RID: 9197
	public int frameCounterY = 20;

	// Token: 0x040023EE RID: 9198
	public int frameCounterZ = 20;

	// Token: 0x040023EF RID: 9199
	private float rotationX;

	// Token: 0x040023F0 RID: 9200
	private float rotationY;

	// Token: 0x040023F1 RID: 9201
	private float rotationZ;

	// Token: 0x040023F2 RID: 9202
	private Quaternion xQuaternion;

	// Token: 0x040023F3 RID: 9203
	private Quaternion yQuaternion;

	// Token: 0x040023F4 RID: 9204
	private Quaternion zQuaternion;

	// Token: 0x040023F5 RID: 9205
	private Quaternion origRotation;

	// Token: 0x040023F6 RID: 9206
	private List<float> rotationsX = new List<float>();

	// Token: 0x040023F7 RID: 9207
	private List<float> rotationsY = new List<float>();

	// Token: 0x040023F8 RID: 9208
	private List<float> rotationsZ = new List<float>();

	// Token: 0x040023F9 RID: 9209
	public float _Move_Speed = 8f;

	// Token: 0x040023FA RID: 9210
	public float _Camera_Speed = 1f;

	// Token: 0x040023FB RID: 9211
	public float _Addition_Move_Speed = 0.05f;

	// Token: 0x040023FC RID: 9212
	public float _Addition_Camera_Speed = 0.05f;

	// Token: 0x040023FD RID: 9213
	private float _Horizontal;

	// Token: 0x040023FE RID: 9214
	private float _Vertical;

	// Token: 0x040023FF RID: 9215
	private float _Up;

	// Token: 0x04002400 RID: 9216
	private float _Down;
}
