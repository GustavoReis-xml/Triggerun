using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CC RID: 1228
public class zhunxingTerminator : zhunxingBase
{
	// Token: 0x06002F0F RID: 12047 RVA: 0x001A4760 File Offset: 0x001A2960
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002F10 RID: 12048 RVA: 0x001A47BA File Offset: 0x001A29BA
	protected override void Update()
	{
		base.Update();
		this.CheckDjs();
	}

	// Token: 0x06002F11 RID: 12049 RVA: 0x001A47CC File Offset: 0x001A29CC
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

	// Token: 0x040028CE RID: 10446
	private GameObject oDjs;

	// Token: 0x040028CF RID: 10447
	private Image img;

	// Token: 0x040028D0 RID: 10448
	private DateTime m_dDateTime;
}
