using System;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class CHitReaction : MonoBehaviour
{
	// Token: 0x060004FC RID: 1276 RVA: 0x0002906C File Offset: 0x0002726C
	public void HitReactionInit(NetLiving net)
	{
		this.hero = net;
		this.ccdik = net.userModelTps.GetComponentInChildren<CCDIK>();
		bool flag = this.ccdik == null;
		if (flag)
		{
			Debug.LogError("角色登陆发现模型没有受伤相关组件!");
		}
		else
		{
			this.ccdik.transform.localPosition = this.fromV3;
			this.ccdik.solver.SetIKPositionWeight(0f);
			this.ccdik.enabled = false;
		}
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x000290EC File Offset: 0x000272EC
	public void Hit(Vector3 pos)
	{
		bool flag = !(this.ccdik == null) && this.timeout <= Time.time && this.hero != null && this.hero.heroBase.CanHitReaction() && this.hero.disCameraNow <= this.hitDisMax && !this.hero.publicInfo.eyeBlock;
		if (flag)
		{
			this.continueTime += 1f;
			bool flag2 = this.continueTime > (float)this.continueMax;
			if (flag2)
			{
				this.continueTime = 0f;
				this.timeout = Time.time + this.timeoutMax;
			}
			else
			{
				this.hiting = true;
				Vector3 position = pos + this.ccdik.transform.parent.position + this.fromV3;
				this.isgo = true;
				this.nowTime = 0f;
				this.ccdik.enabled = true;
				this.ccdik.transform.position = position;
			}
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00029208 File Offset: 0x00027408
	private void Update()
	{
		bool flag = !this.hiting;
		if (!flag)
		{
			float ikpositionWeight = this.ccdik.solver.GetIKPositionWeight();
			this.nowTime += Time.deltaTime;
			bool flag2 = this.isgo;
			if (flag2)
			{
				this.ccdik.solver.SetIKPositionWeight(Mathf.Lerp(ikpositionWeight, 1f, Time.deltaTime * this.hitSpeed));
				bool flag3 = ikpositionWeight > this.maxWeight;
				if (flag3)
				{
					this.isgo = false;
				}
			}
			else
			{
				this.ccdik.solver.SetIKPositionWeight(Mathf.Lerp(ikpositionWeight, 0f, Time.deltaTime * this.hitSpeed));
				bool flag4 = ikpositionWeight < 0.01f;
				if (flag4)
				{
					this.continueTime -= 1f;
					this.hiting = false;
					this.ccdik.enabled = false;
					this.ccdik.solver.SetIKPositionWeight(0f);
					this.ccdik.transform.localPosition = this.fromV3;
				}
			}
		}
	}

	// Token: 0x040003D4 RID: 980
	private NetLiving hero;

	// Token: 0x040003D5 RID: 981
	public float hitSpeed = 20f;

	// Token: 0x040003D6 RID: 982
	private CCDIK ccdik;

	// Token: 0x040003D7 RID: 983
	private Vector3 fromV3 = new Vector3(0f, 3f, 0f);

	// Token: 0x040003D8 RID: 984
	private bool hiting;

	// Token: 0x040003D9 RID: 985
	private float nowTime;

	// Token: 0x040003DA RID: 986
	private bool isgo;

	// Token: 0x040003DB RID: 987
	public float maxWeight = 0.99f;

	// Token: 0x040003DC RID: 988
	private float continueTime;

	// Token: 0x040003DD RID: 989
	public int continueMax = 4;

	// Token: 0x040003DE RID: 990
	private float timeout;

	// Token: 0x040003DF RID: 991
	private float timeoutMax = 5f;

	// Token: 0x040003E0 RID: 992
	private float hitDisMax = 8f;
}
