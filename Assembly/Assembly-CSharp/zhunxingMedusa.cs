using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C7 RID: 1223
public class zhunxingMedusa : zhunxingBase
{
	// Token: 0x06002EFD RID: 12029 RVA: 0x001A3B40 File Offset: 0x001A1D40
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oDjs = gotherui.FindChild("djsimg").gameObject;
		this.img = this.oDjs.transform.Find("img").GetComponent<Image>();
		this.stime = this.oDjs.transform.Find("time").GetComponent<Text>();
		this.oDjs.SetActive(false);
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x001A3BBA File Offset: 0x001A1DBA
	protected override void Update()
	{
		base.Update();
		this.CheckDjs();
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x001A3BCC File Offset: 0x001A1DCC
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

	// Token: 0x040028AB RID: 10411
	private GameObject oDjs;

	// Token: 0x040028AC RID: 10412
	private Image img;

	// Token: 0x040028AD RID: 10413
	private Text stime;

	// Token: 0x040028AE RID: 10414
	private DateTime m_dDateTime;
}
