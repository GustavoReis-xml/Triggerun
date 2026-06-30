using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200046E RID: 1134
public class TeamCDCon : MonoBehaviour
{
	// Token: 0x06002C2C RID: 11308 RVA: 0x00183EEF File Offset: 0x001820EF
	private void Start()
	{
		this.img = base.transform.FindChild("ImageCD").GetComponent<Image>();
		this.img.fillAmount = 0f;
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x00183F20 File Offset: 0x00182120
	private void FixedUpdate()
	{
		int num = Environment.TickCount - this.m_nTickTime;
		this.m_nTickTime = Environment.TickCount;
		bool flag = num > 0 && this.m_nTime > 0;
		if (flag)
		{
			this.img.fillAmount -= (float)num * this.m_nSpeed / 1000f / (float)this.m_nAllTime;
			bool flag2 = this.img.fillAmount == 1f;
			if (flag2)
			{
				base.CancelInvoke("CallTime");
			}
		}
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x00183FAA File Offset: 0x001821AA
	public void SetCD(string name)
	{
		this.sName = name;
		this.img.fillAmount = 1f;
		this.m_nTime = 25;
		base.InvokeRepeating("CallTime", 1f, 1f);
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x00183FE3 File Offset: 0x001821E3
	public void StopCD()
	{
		this.img.fillAmount = 0f;
		this.m_nTime = 0;
		base.CancelInvoke("CallTime");
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x0018400C File Offset: 0x0018220C
	private void CallTime()
	{
		this.m_nTime--;
		bool flag = this.m_nTime <= 0;
		if (flag)
		{
			TeamManager.GetInstance().DelImgCD(this.sName);
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40003, new object[]
			{
				this.sName
			}), "ffdd00", "system");
			MainUICon.inst.SortTeamObj();
			base.CancelInvoke("CallTime");
		}
		else
		{
			this.m_nTickTime = Environment.TickCount;
		}
	}

	// Token: 0x040025A5 RID: 9637
	private Image img;

	// Token: 0x040025A6 RID: 9638
	private float m_nSpeed = 1f;

	// Token: 0x040025A7 RID: 9639
	private int m_nTickTime;

	// Token: 0x040025A8 RID: 9640
	private int m_nAllTime = 25;

	// Token: 0x040025A9 RID: 9641
	public int m_nTime;

	// Token: 0x040025AA RID: 9642
	public string sName;
}
