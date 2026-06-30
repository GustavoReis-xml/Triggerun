using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class PlayerLocal : MonoBehaviour
{
	// Token: 0x06001E1D RID: 7709 RVA: 0x001193E2 File Offset: 0x001175E2
	private void Start()
	{
		this.fOldAngle = base.transform.eulerAngles.y;
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x001193FC File Offset: 0x001175FC
	private void FixedUpdate()
	{
		bool flag = Time.deltaTime == 0f;
		if (!flag)
		{
			bool flag2 = this.hero == null;
			if (flag2)
			{
				this.hero = fGameManager.Heroplayer;
			}
			else
			{
				bool flag3 = Time.realtimeSinceStartup - this.hero.pingDelayBegin > 1f;
				if (flag3)
				{
					this.hero.delayNet = true;
				}
				else
				{
					this.hero.delayNet = false;
				}
				bool flag4 = this.fTongbuTime > Time.time;
				if (!flag4)
				{
					this.fTongbuTime = Time.time + 0.02f;
					try
					{
						this.onceMsg();
					}
					catch
					{
					}
					bool flag5 = !this.hero.playerHaveCreate || this.hero.haveDestroy || (this.hero.playerCommon != null && this.hero.playerCommon.m_nGM > 0 && this.hero.playerCommon.m_szGMTran.Count > 0);
					if (!flag5)
					{
						try
						{
							bool flag6 = this.hero.playerCommon != null && this.hero.UserControl != null && this.hero.UserControl.transform != null;
							if (flag6)
							{
								float num = Vector3.Distance(this.hero.playerCommon.pos, this.hero.UserControl.transform.position);
								bool flag7 = this.hero.charMove != null;
								if (flag7)
								{
									this.hero.charMove.isSleep = (num > 2.99f * this.hero.playerCommon.m_fMoveSpeed);
								}
							}
						}
						catch
						{
						}
						bool flag8 = this.hero.delayNet || !this.tongbu;
						if (!flag8)
						{
							try
							{
								this.TongbuPos();
							}
							catch
							{
							}
							try
							{
								this.TongbuAngle();
							}
							catch
							{
							}
							try
							{
								bool flag9 = this.hero.playerTarget != null && this.hero.playerTarget.transform != null;
								if (flag9)
								{
									this.TongbuTarget();
								}
							}
							catch
							{
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x00119698 File Offset: 0x00117898
	private void onceMsg()
	{
		this.aryOneMsgParam[0] = 0;
		float time = Time.time;
		bool flag = this.pingSend;
		if (flag)
		{
			this.ping = (int)((time - this.pingBegin) * 1000f);
			bool flag2 = this.ping > this.nowPing && time > this.pingGetDealy;
			if (flag2)
			{
				this.pingGetDealy = time + 0.5f;
				this.nowPing = this.ping;
			}
		}
		bool flag3 = time > this.pingtime;
		if (flag3)
		{
			this.pingtime = time + 10f;
			this.aryOneMsgParam[1] = 1;
			this.pingBegin = time;
			this.pingSend = true;
		}
		else
		{
			this.aryOneMsgParam[1] = 0;
		}
		bool delayNet = this.hero.delayNet;
		if (delayNet)
		{
			bool flag4 = Time.realtimeSinceStartup > this.lastDelayTime;
			if (flag4)
			{
				this.lastDelayTime = Time.realtimeSinceStartup + 1f;
				this.hero.MustToFun("SN", new object[]
				{
					this.aryOneMsgParam
				});
			}
		}
		else
		{
			this.hero.TosFun("SN", new object[]
			{
				this.aryOneMsgParam
			});
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x001197EA File Offset: 0x001179EA
	public void onceMsgPingBack()
	{
		this.pingtime = Time.time + 1f;
		this.ping = (int)((Time.time - this.pingBegin) * 1000f);
		this.nowPing = this.ping;
		this.pingSend = false;
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0011982C File Offset: 0x00117A2C
	private void TongbuPos()
	{
		float num = Vector3.Distance(this.v3OldPos, base.transform.position);
		bool flag = num >= 0.01f;
		if (flag)
		{
			this.v3OldPos = base.transform.position;
			this.posChild[0] = (int)(base.transform.position.x * 100f);
			this.posChild[1] = (int)(base.transform.position.y * 100f);
			this.posChild[2] = (int)(base.transform.position.z * 100f);
			this.posMain[0] = this.posChild;
			this.posMain[1] = (this.hero.isDead ? 1 : 0);
			this.hero.TosFun("SP", new object[]
			{
				this.posMain
			});
		}
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x00119948 File Offset: 0x00117B48
	private void TongbuAngle()
	{
		bool flag = Mathf.Abs(this.fOldAngle - base.transform.eulerAngles.y) >= 0.5f;
		if (flag)
		{
			this.fOldAngle = base.transform.eulerAngles.y;
			this.tongbuAry[0] = (int)base.transform.eulerAngles.y;
			this.tongbuAry[1] = (this.hero.isDead ? 1 : 0);
			this.hero.TosFun("SR", new object[]
			{
				this.tongbuAry
			});
		}
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x00119A00 File Offset: 0x00117C00
	private void TongbuTarget()
	{
		float num = Quaternion.Angle(this.lastEuler, this.nowEuler);
		bool flag = num >= 0.1f;
		if (flag)
		{
			this.lastEuler = this.nowEuler;
			this.targetChild[0] = (int)(this.hero.playerTarget.transform.position.x * 100f);
			this.targetChild[1] = (int)(this.hero.playerTarget.transform.position.y * 100f);
			this.targetChild[2] = (int)(this.hero.playerTarget.transform.position.z * 100f);
			this.targetMain[0] = this.targetChild;
			this.targetMain[1] = (this.hero.isDead ? 1 : 0);
			this.hero.TosFun("ST", new object[]
			{
				this.targetMain
			});
		}
	}

	// Token: 0x04001B9E RID: 7070
	private HeroPlayer hero;

	// Token: 0x04001B9F RID: 7071
	private Vector3 v3OldPos;

	// Token: 0x04001BA0 RID: 7072
	private float fOldAngle;

	// Token: 0x04001BA1 RID: 7073
	private float fTongbuTime;

	// Token: 0x04001BA2 RID: 7074
	public bool tongbu = true;

	// Token: 0x04001BA3 RID: 7075
	public Quaternion nowEuler;

	// Token: 0x04001BA4 RID: 7076
	private Quaternion lastEuler;

	// Token: 0x04001BA5 RID: 7077
	private float lastRigY;

	// Token: 0x04001BA6 RID: 7078
	private int checkNo;

	// Token: 0x04001BA7 RID: 7079
	private bool beFucked;

	// Token: 0x04001BA8 RID: 7080
	private float fuckTimeout;

	// Token: 0x04001BA9 RID: 7081
	private float pingtime;

	// Token: 0x04001BAA RID: 7082
	private float pingBegin;

	// Token: 0x04001BAB RID: 7083
	private int ping;

	// Token: 0x04001BAC RID: 7084
	public int nowPing;

	// Token: 0x04001BAD RID: 7085
	private float pingGetDealy;

	// Token: 0x04001BAE RID: 7086
	private bool pingSend;

	// Token: 0x04001BAF RID: 7087
	private ArrayList aryOneMsgParam = new ArrayList
	{
		0,
		0
	};

	// Token: 0x04001BB0 RID: 7088
	private float lastDelayTime;

	// Token: 0x04001BB1 RID: 7089
	private ArrayList posMain = new ArrayList
	{
		new ArrayList(),
		0
	};

	// Token: 0x04001BB2 RID: 7090
	private ArrayList posChild = new ArrayList
	{
		0,
		0,
		0
	};

	// Token: 0x04001BB3 RID: 7091
	private ArrayList tongbuAry = new ArrayList
	{
		0,
		0
	};

	// Token: 0x04001BB4 RID: 7092
	private ArrayList targetMain = new ArrayList
	{
		new ArrayList(),
		0
	};

	// Token: 0x04001BB5 RID: 7093
	private ArrayList targetChild = new ArrayList
	{
		0,
		0,
		0
	};
}
