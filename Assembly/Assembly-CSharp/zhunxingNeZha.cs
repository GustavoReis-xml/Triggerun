using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C8 RID: 1224
public class zhunxingNeZha : zhunxingBase
{
	// Token: 0x06002F01 RID: 12033 RVA: 0x001A3D00 File Offset: 0x001A1F00
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stime = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x001A3D7A File Offset: 0x001A1F7A
	protected override void Update()
	{
		base.Update();
		this.CheckDjs();
	}

	// Token: 0x06002F03 RID: 12035 RVA: 0x001A3D8C File Offset: 0x001A1F8C
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

	// Token: 0x040028AF RID: 10415
	private GameObject oDjs;

	// Token: 0x040028B0 RID: 10416
	private Image img;

	// Token: 0x040028B1 RID: 10417
	private Text stime;

	// Token: 0x040028B2 RID: 10418
	private DateTime m_dDateTime;
}
