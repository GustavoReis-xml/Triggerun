using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class GuanyinFazhangCon : MonoBehaviour
{
	// Token: 0x06000FBF RID: 4031 RVA: 0x00090710 File Offset: 0x0008E910
	private void Start()
	{
		this.head = base.transform.Find("Point002");
		this.body = base.transform.Find("Point008");
		this.leaf1 = this.body.transform.Find("Bone013");
		this.leaf1.localRotation = Quaternion.Euler(this.idel1);
		this.leaf2 = this.body.transform.Find("Bone015");
		this.leaf2.localRotation = Quaternion.Euler(this.idel2);
		this.leaf3 = this.body.transform.Find("Bone017");
		this.leaf3.localRotation = Quaternion.Euler(this.idel3);
		this.leaf4 = this.body.transform.Find("Bone019");
		this.leaf4.localRotation = Quaternion.Euler(this.idel4);
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00090814 File Offset: 0x0008EA14
	private void Update()
	{
		bool @bool = this.animator.GetBool("Aim");
		if (@bool)
		{
			bool flag = this.animator.GetInteger("AttackState") == 1;
			if (flag)
			{
				this.head.Rotate(Vector3.left * Time.deltaTime * 100f);
				this.body.Rotate(Vector3.right * Time.deltaTime * 100f);
				this.leaf1.localRotation = Quaternion.Lerp(this.leaf1.localRotation, Quaternion.Euler(this.addhp1), Time.deltaTime * 10f);
				this.leaf2.localRotation = Quaternion.Lerp(this.leaf2.localRotation, Quaternion.Euler(this.addhp2), Time.deltaTime * 10f);
				this.leaf3.localRotation = Quaternion.Lerp(this.leaf3.localRotation, Quaternion.Euler(this.addhp3), Time.deltaTime * 10f);
				this.leaf4.localRotation = Quaternion.Lerp(this.leaf4.localRotation, Quaternion.Euler(this.addhp4), Time.deltaTime * 10f);
			}
			bool flag2 = this.animator.GetInteger("AttackState") == 2;
			if (flag2)
			{
				this.head.Rotate(Vector3.right * Time.deltaTime * 120f);
				this.body.Rotate(Vector3.left * Time.deltaTime * 120f);
				this.leaf1.localRotation = Quaternion.Lerp(this.leaf1.localRotation, Quaternion.Euler(this.addatk1), Time.deltaTime * 10f);
				this.leaf2.localRotation = Quaternion.Lerp(this.leaf2.localRotation, Quaternion.Euler(this.addatk2), Time.deltaTime * 10f);
				this.leaf3.localRotation = Quaternion.Lerp(this.leaf3.localRotation, Quaternion.Euler(this.addatk3), Time.deltaTime * 10f);
				this.leaf4.localRotation = Quaternion.Lerp(this.leaf4.localRotation, Quaternion.Euler(this.addatk4), Time.deltaTime * 10f);
			}
			bool flag3 = this.animator.GetInteger("AttackState") == 0;
			if (flag3)
			{
				this.leaf1.localRotation = Quaternion.Lerp(this.leaf1.localRotation, Quaternion.Euler(this.idel1), Time.deltaTime * 10f);
				this.leaf2.localRotation = Quaternion.Lerp(this.leaf2.localRotation, Quaternion.Euler(this.idel2), Time.deltaTime * 10f);
				this.leaf3.localRotation = Quaternion.Lerp(this.leaf3.localRotation, Quaternion.Euler(this.idel3), Time.deltaTime * 10f);
				this.leaf4.localRotation = Quaternion.Lerp(this.leaf4.localRotation, Quaternion.Euler(this.idel4), Time.deltaTime * 10f);
			}
		}
	}

	// Token: 0x04000C71 RID: 3185
	public Animator animator;

	// Token: 0x04000C72 RID: 3186
	private Transform head;

	// Token: 0x04000C73 RID: 3187
	private Transform body;

	// Token: 0x04000C74 RID: 3188
	private Transform leaf1;

	// Token: 0x04000C75 RID: 3189
	private Transform leaf2;

	// Token: 0x04000C76 RID: 3190
	private Transform leaf3;

	// Token: 0x04000C77 RID: 3191
	private Transform leaf4;

	// Token: 0x04000C78 RID: 3192
	private Vector3 idel1 = new Vector3(0f, 0f, 173.84f);

	// Token: 0x04000C79 RID: 3193
	private Vector3 idel2 = new Vector3(0f, -2.822f, -180f);

	// Token: 0x04000C7A RID: 3194
	private Vector3 idel3 = new Vector3(-82.686f, -93.927f, -85.59f);

	// Token: 0x04000C7B RID: 3195
	private Vector3 idel4 = new Vector3(0.172f, -176.233f, 0.263f);

	// Token: 0x04000C7C RID: 3196
	private Vector3 addhp1 = new Vector3(0f, 0f, 162.288f);

	// Token: 0x04000C7D RID: 3197
	private Vector3 addhp2 = new Vector3(0f, -17.602f, -180f);

	// Token: 0x04000C7E RID: 3198
	private Vector3 addhp3 = new Vector3(-72.99001f, -91.704f, -87.857f);

	// Token: 0x04000C7F RID: 3199
	private Vector3 addhp4 = new Vector3(0.103f, -161.418f, 0.249f);

	// Token: 0x04000C80 RID: 3200
	private Vector3 addatk1 = new Vector3(0f, 0f, 146.845f);

	// Token: 0x04000C81 RID: 3201
	private Vector3 addatk2 = new Vector3(0f, -33.042f, -180f);

	// Token: 0x04000C82 RID: 3202
	private Vector3 addatk3 = new Vector3(-52.716f, -90.82201f, -88.832f);

	// Token: 0x04000C83 RID: 3203
	private Vector3 addatk4 = new Vector3(0.041f, -145.949f, 0.219f);
}
