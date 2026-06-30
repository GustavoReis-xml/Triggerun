using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004BA RID: 1210
public class zhunxingHouYi : zhunxingBase
{
	// Token: 0x06002EC9 RID: 11977 RVA: 0x001A222C File Offset: 0x001A042C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gNengLian = gotherui.FindChild("nenglian").gameObject;
		this.Nlimg = this.gNengLian.transform.FindChild("nenglian").GetComponent<Image>();
		this.gNengLian.SetActive(false);
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x001A2288 File Offset: 0x001A0488
	protected override void Update()
	{
		base.Update();
		bool flag = this.gNengLian == null || this.hero == null || this.hero.wc == null;
		if (!flag)
		{
			float num = this.hero.wc.FullRate(1);
			bool flag2 = num > 0f && this.hero.wc.fullIng && this.hero.wc.fullWeapon.defaultLauncherNo == 0 && !this.hero.isDead;
			if (flag2)
			{
				bool flag3 = !this.gNengLian.activeSelf;
				if (flag3)
				{
					this.gNengLian.SetActive(true);
				}
				bool flag4 = num > 1f;
				if (flag4)
				{
					num = 1f;
				}
				this.Nlimg.fillAmount = num;
			}
			else
			{
				bool activeSelf = this.gNengLian.activeSelf;
				if (activeSelf)
				{
					this.gNengLian.SetActive(false);
				}
			}
		}
	}

	// Token: 0x04002870 RID: 10352
	private GameObject gNengLian;

	// Token: 0x04002871 RID: 10353
	private Image Nlimg;
}
