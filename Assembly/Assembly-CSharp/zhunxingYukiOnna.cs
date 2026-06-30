using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D1 RID: 1233
public class zhunxingYukiOnna : zhunxingBase
{
	// Token: 0x06002F42 RID: 12098 RVA: 0x001A5D4C File Offset: 0x001A3F4C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gRead = gotherui.FindChild("read").gameObject;
		this.gRead.transform.Find("ldesc").GetComponent<Text>().text = Language.GetLang(3024, new object[0]);
		this.gRead.transform.Find("rdesc").GetComponent<Text>().text = Language.GetLang(3024, new object[0]);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stiem = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
		this.gRead.SetActive(false);
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x001A5E4C File Offset: 0x001A404C
	protected override void Update()
	{
		base.Update();
		bool flag = !(this.gRead == null) && !(this.oDjs == null);
		if (flag)
		{
			this.CheckDjs();
		}
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x001A5E90 File Offset: 0x001A4090
	private void CheckDjs()
	{
		bool flag = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
		if (flag)
		{
			float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
			bool flag2 = num > 0f && gameuiCon.inst.mYukiOnna;
			if (flag2)
			{
				bool flag3 = !this.gRead.activeSelf;
				if (flag3)
				{
					this.gRead.SetActive(true);
				}
				bool flag4 = !this.oDjs.activeSelf;
				if (flag4)
				{
					this.oDjs.SetActive(true);
				}
				float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
				this.img.fillAmount = num / fContinuesTime;
				this.stiem.text = Mathf.CeilToInt(num).ToString() + "s";
				return;
			}
		}
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
		bool activeSelf2 = this.oDjs.activeSelf;
		if (activeSelf2)
		{
			this.oDjs.SetActive(false);
		}
	}

	// Token: 0x040028EF RID: 10479
	private GameObject gRead;

	// Token: 0x040028F0 RID: 10480
	private GameObject oDjs;

	// Token: 0x040028F1 RID: 10481
	private Image img;

	// Token: 0x040028F2 RID: 10482
	private Text stiem;

	// Token: 0x040028F3 RID: 10483
	private string m_sDzEntTime = string.Empty;
}
