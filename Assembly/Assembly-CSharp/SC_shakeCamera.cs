using System;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class SC_shakeCamera : MonoBehaviour
{
	// Token: 0x060022C0 RID: 8896 RVA: 0x0014018E File Offset: 0x0013E38E
	private void Awake()
	{
		this.selfCamera = base.GetComponent<Camera>();
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x0014019D File Offset: 0x0013E39D
	private void Start()
	{
		this.shakeTime = this.setShakeTime;
		this.fps = this.shakeFps;
		this.frameTime = 0.03f;
		this.shakeDelta = 0.005f;
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x001401D0 File Offset: 0x0013E3D0
	private void Update()
	{
		bool flag = !SC_shakeCamera.isshakeCamera || this.shakeTime <= 0f;
		if (!flag)
		{
			this.shakeTime -= Time.deltaTime;
			bool flag2 = this.shakeTime <= 0f;
			if (flag2)
			{
				this.selfCamera.rect = new Rect(0f, 0f, 1f, 1f);
				SC_shakeCamera.isshakeCamera = false;
				this.shakeTime = this.setShakeTime;
				this.fps = this.shakeFps;
				this.frameTime = 0.03f;
				this.shakeDelta = 0.005f;
			}
			else
			{
				this.frameTime += Time.deltaTime;
				bool flag3 = (double)this.frameTime > 1.0 / (double)this.fps;
				if (flag3)
				{
					this.frameTime = 0f;
					this.selfCamera.rect = new Rect(this.shakeDelta * (-1f + this.shakeLevel * UnityEngine.Random.value), this.shakeDelta * (-1f + this.shakeLevel * UnityEngine.Random.value), 1f, 1f);
				}
			}
		}
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x00140314 File Offset: 0x0013E514
	public static void shakeCamera()
	{
		SC_shakeCamera.isshakeCamera = true;
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x0014031D File Offset: 0x0013E51D
	public static void shakestop()
	{
		SC_shakeCamera.isshakeCamera = false;
	}

	// Token: 0x04001FBC RID: 8124
	private static bool isshakeCamera;

	// Token: 0x04001FBD RID: 8125
	public float shakeLevel = 5f;

	// Token: 0x04001FBE RID: 8126
	public float setShakeTime = 1f;

	// Token: 0x04001FBF RID: 8127
	public float shakeFps = 80f;

	// Token: 0x04001FC0 RID: 8128
	private float fps;

	// Token: 0x04001FC1 RID: 8129
	private float shakeTime;

	// Token: 0x04001FC2 RID: 8130
	private float frameTime;

	// Token: 0x04001FC3 RID: 8131
	private float shakeDelta = 0.005f;

	// Token: 0x04001FC4 RID: 8132
	private Camera selfCamera;
}
