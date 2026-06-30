using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004BD RID: 1213
public class zhunxingJingKe : zhunxingBase
{
	// Token: 0x06002ED3 RID: 11987 RVA: 0x001A2854 File Offset: 0x001A0A54
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stime = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002ED4 RID: 11988 RVA: 0x001A28CE File Offset: 0x001A0ACE
	protected override void Update()
	{
		base.Update();
		this.CheckDjs();
	}

	// Token: 0x06002ED5 RID: 11989 RVA: 0x001A28E0 File Offset: 0x001A0AE0
	private void CheckDjs()
	{
		bool flag = this.oDjs == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
			if (flag2)
			{
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool flag4 = !this.oDjs.activeSelf;
					if (flag4)
					{
						this.oDjs.SetActive(true);
					}
					float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
					this.img.fillAmount = num / fContinuesTime;
					this.stime.text = Mathf.CeilToInt(num).ToString() + "s";
					return;
				}
			}
			bool activeSelf = this.oDjs.activeSelf;
			if (activeSelf)
			{
				this.oDjs.SetActive(false);
			}
		}
	}

	// Token: 0x0400287D RID: 10365
	private GameObject oDjs;

	// Token: 0x0400287E RID: 10366
	private Image img;

	// Token: 0x0400287F RID: 10367
	private Text stime;

	// Token: 0x04002880 RID: 10368
	private DateTime m_dDateTime;
}
