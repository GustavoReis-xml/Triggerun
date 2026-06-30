using System;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class _LushLODTreesFlycam : MonoBehaviour
{
	// Token: 0x06002F74 RID: 12148 RVA: 0x001A8914 File Offset: 0x001A6B14
	private void Start()
	{
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x001A8918 File Offset: 0x001A6B18
	private void Update()
	{
		bool mouseButton = Input.GetMouseButton(1);
		if (mouseButton)
		{
			this.rotationX += Input.GetAxis("Mouse X") * this.cameraSensitivity * Time.deltaTime;
			this.rotationY += Input.GetAxis("Mouse Y") * this.cameraSensitivity * Time.deltaTime;
			this.rotationY = Mathf.Clamp(this.rotationY, -90f, 90f);
		}
		Vector2 vector = new Vector2(this.rotationX, this.rotationY);
		vector = Vector2.Scale(vector, new Vector2(this.sensitivity.x * this.smoothing.x, this.sensitivity.y * this.smoothing.y));
		this._smoothMouse.x = Mathf.Lerp(this._smoothMouse.x, vector.x, 1f / this.smoothing.x);
		this._smoothMouse.y = Mathf.Lerp(this._smoothMouse.y, vector.y, 1f / this.smoothing.y);
		base.transform.localRotation = Quaternion.AngleAxis(this._smoothMouse.x, Vector3.up);
		base.transform.localRotation *= Quaternion.AngleAxis(this._smoothMouse.y, Vector3.left);
		bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		if (flag)
		{
			base.transform.position += base.transform.forward * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
			base.transform.position += base.transform.right * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		}
		else
		{
			bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			if (flag2)
			{
				base.transform.position += base.transform.forward * (this.normalMoveSpeed * this.slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				base.transform.position += base.transform.right * (this.normalMoveSpeed * this.slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
			else
			{
				base.transform.position += base.transform.forward * this.normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				base.transform.position += base.transform.right * this.normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
		}
		bool key = Input.GetKey(KeyCode.Q);
		if (key)
		{
			base.transform.position += base.transform.up * this.climbSpeed * Time.deltaTime;
		}
		bool key2 = Input.GetKey(KeyCode.E);
		if (key2)
		{
			base.transform.position -= base.transform.up * this.climbSpeed * Time.deltaTime;
		}
	}

	// Token: 0x04002940 RID: 10560
	public Vector2 _smoothMouse;

	// Token: 0x04002941 RID: 10561
	public float cameraSensitivity = 90f;

	// Token: 0x04002942 RID: 10562
	public float climbSpeed = 4f;

	// Token: 0x04002943 RID: 10563
	public float normalMoveSpeed = 10f;

	// Token: 0x04002944 RID: 10564
	public float slowMoveFactor = 0.5f;

	// Token: 0x04002945 RID: 10565
	public float fastMoveFactor = 6f;

	// Token: 0x04002946 RID: 10566
	public float rotationX;

	// Token: 0x04002947 RID: 10567
	public float rotationY;

	// Token: 0x04002948 RID: 10568
	public Vector2 sensitivity = new Vector2(0.3f, 0.3f);

	// Token: 0x04002949 RID: 10569
	public Vector2 smoothing = new Vector2(5f, 5f);
}
