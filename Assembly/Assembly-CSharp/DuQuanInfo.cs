using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class DuQuanInfo : PublicInfo
{
	// Token: 0x0600099D RID: 2461 RVA: 0x00055ABD File Offset: 0x00053CBD
	private void Awake()
	{
		this.Sphere001 = base.transform.FindChild("Sphere001").gameObject;
		this.Sphere001.SetActive(false);
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00055AE8 File Offset: 0x00053CE8
	public override void InitInfo(NetLiving _net)
	{
		base.InitInfo(_net);
		this.dqObj = (DuQuanbuff)_net;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00055B00 File Offset: 0x00053D00
	protected override void Update()
	{
		base.Update();
		bool flag = this.dqObj != null;
		if (flag)
		{
			this.CheckDuQuanMove();
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00055B2C File Offset: 0x00053D2C
	private void CheckDuQuanMove()
	{
		bool flag = this.dqObj.m_nShow == -1 || this.dqObj.m_dMoveStartTime == DateTime.MinValue;
		if (!flag)
		{
			bool flag2 = this.dqObj.m_nShow == 0;
			if (flag2)
			{
				bool activeSelf = this.Sphere001.activeSelf;
				if (activeSelf)
				{
					this.Sphere001.SetActive(false);
				}
				DateTime d = this.dqObj.m_dMoveStartTime.AddSeconds((double)(-(double)this.dqObj.m_nWaitLongTime));
				int num = (int)(d - Fun.GetServerDate()).TotalSeconds;
				bool flag3 = num > 0 && DtsUICon.Inst != null;
				if (flag3)
				{
					DtsUICon.Inst.OnCenterMsg(29031, num, 0);
				}
			}
			else
			{
				bool flag4 = !this.Sphere001.activeSelf;
				if (flag4)
				{
					this.Sphere001.SetActive(true);
				}
				bool flag5 = DtsUICon.Inst != null;
				if (flag5)
				{
					DtsUICon.Inst.OnSetJdt(true);
				}
				bool flag6 = this.dqObj.m_nState == -1;
				if (!flag6)
				{
					bool flag7 = this.dqObj.m_nState == 1;
					if (flag7)
					{
						TimeSpan timeSpan = Fun.GetServerDate() - this.dqObj.m_dMoveStartTime;
						float num2 = (float)timeSpan.TotalMilliseconds;
						int num3 = this.dqObj.m_nRadius - this.dqObj.m_nSafeRadius;
						float num4 = (float)num3 - (float)num3 * num2 / (this.dqObj.MoveTime * 1000f) + (float)this.dqObj.m_nSafeRadius;
						float nPointX = this.dqObj.m_nPointX + (this.dqObj.m_nSafePointX - this.dqObj.m_nPointX) * num2 / (this.dqObj.MoveTime * 1000f);
						float nPointZ = this.dqObj.m_nPointZ + (this.dqObj.m_nSafePointZ - this.dqObj.m_nPointZ) * num2 / (this.dqObj.MoveTime * 1000f);
						this.dqObj.SetDuQuanPos((int)num4, nPointX, nPointZ);
						bool flag8 = DtsUICon.Inst != null;
						if (flag8)
						{
							DtsUICon.Inst.OnUpdateDelayTime(string.Empty);
						}
						bool flag9 = DtsUICon.Inst != null;
						if (flag9)
						{
							DtsUICon.Inst.SetDuQuanTimePos(this.dqObj.MoveTime, (float)timeSpan.TotalSeconds);
						}
						bool flag10 = (int)timeSpan.TotalSeconds == 1 && DtsUICon.Inst != null;
						if (flag10)
						{
							DtsUICon.Inst.OnCenterMsg(29033, 0, 1);
						}
					}
					bool flag11 = this.dqObj.m_nState == 0;
					if (flag11)
					{
						int nTime = (int)(this.dqObj.m_dMoveStartTime - Fun.GetServerDate()).TotalSeconds;
						bool flag12 = DtsUICon.Inst != null;
						if (flag12)
						{
							DtsUICon.Inst.OnUpdateDelayTime(nTime.ToString());
							DtsUICon.Inst.SetDuQuanTimePos(0f, 0f);
							DtsUICon.Inst.OnCenterMsg(29032, nTime, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x0400077B RID: 1915
	private DuQuanbuff dqObj;

	// Token: 0x0400077C RID: 1916
	private GameObject Sphere001;
}
