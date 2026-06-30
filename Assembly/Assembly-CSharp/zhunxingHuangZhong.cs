using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004BB RID: 1211
public class zhunxingHuangZhong : zhunxingBase
{
	// Token: 0x06002ECC RID: 11980 RVA: 0x001A239C File Offset: 0x001A059C
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oPawer = gotherui.Find("power").gameObject;
		this.imagepawer = this.oPawer.transform.Find("Image2").GetComponent<Image>();
		this.oPawer.transform.Find("desc").GetComponent<Text>().text = Language.GetLang(3032, new object[0]);
		this.oPawer.SetActive(false);
		this.zx = gotherui.Find("zx").gameObject;
		this.tValue = this.zx.transform.Find("value").GetComponent<Text>();
		this.ui_quan = this.zx.transform.Find("ui_quan").gameObject;
		this.meshr = this.ui_quan.transform.Find("quan").GetComponent<MeshRenderer>();
		this.meshr.sharedMaterial.mainTextureOffset = new Vector2(0f, 0.5f);
		this.checkAuto = base.OtherAutoAirUI.gameObject.GetComponent<CheckAutoAirUI>();
		bool flag = this.checkAuto == null;
		if (flag)
		{
			this.checkAuto = base.OtherAutoAirUI.gameObject.AddComponent<CheckAutoAirUI>();
		}
		this.checkAuto.OnAwake();
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x001A2506 File Offset: 0x001A0706
	protected override void Update()
	{
		base.zhunxingshowOrHide(false);
		this.drawPawer();
		this.CheckAutoAir_HZ();
		this.ZhunxingEffect();
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x001A2528 File Offset: 0x001A0728
	public void CheckAutoAir_HZ()
	{
		bool flag = !(base.OtherAutoAirUI == null) && !(this.checkAuto == null);
		if (flag)
		{
			this.checkAuto.OnShowAutoAir_HZ(this.hero, this.m_nSkil_1, this.m_nSkil_2, this.m_nBuff_1, this.m_nBuff_2, this);
		}
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x001A2588 File Offset: 0x001A0788
	private void ZhunxingEffect()
	{
		bool flag = this.hero == null || this.hero.heroBase == null || (IHeroDataHuangZhong)this.hero.heroBase.IHD == null || this.tValue == null;
		if (!flag)
		{
			bool isDead = this.hero.isDead;
			if (isDead)
			{
				bool activeSelf = this.zx.activeSelf;
				if (activeSelf)
				{
					this.zx.SetActive(false);
				}
				bool activeSelf2 = this.ui_quan.activeSelf;
				if (activeSelf2)
				{
					this.ui_quan.SetActive(false);
				}
			}
			else
			{
				bool flag2 = !this.zx.activeSelf;
				if (flag2)
				{
					this.zx.SetActive(true);
					bool activeSelf3 = this.ui_quan.activeSelf;
					if (activeSelf3)
					{
						this.ui_quan.SetActive(false);
					}
				}
				int hd_Power = ((IHeroDataHuangZhong)this.hero.heroBase.IHD).HD_Power;
				this.tValue.text = hd_Power.ToString();
				bool flag3 = hd_Power < 1;
				if (flag3)
				{
					bool activeSelf4 = this.ui_quan.activeSelf;
					if (activeSelf4)
					{
						this.ui_quan.SetActive(false);
					}
				}
				else
				{
					bool flag4 = !this.ui_quan.activeSelf;
					if (flag4)
					{
						this.ui_quan.SetActive(true);
					}
					float y = 0.5f - (float)hd_Power * 0.5f / 100f;
					this.meshr.sharedMaterial.mainTextureOffset = new Vector2(0f, y);
				}
			}
		}
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x001A2728 File Offset: 0x001A0928
	private void drawPawer()
	{
		bool flag = this.oPawer == null || !(this.hero.playerDazhao != null);
		if (!flag)
		{
			bool flag2 = !this.hero.playerDazhao.doing;
			if (flag2)
			{
				bool activeSelf = this.oPawer.activeSelf;
				if (activeSelf)
				{
					this.oPawer.SetActive(false);
				}
			}
			else
			{
				bool flag3 = !this.oPawer.activeSelf;
				if (flag3)
				{
					this.oPawer.SetActive(true);
				}
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag4 = this.imagepawer != null;
				if (flag4)
				{
					this.imagepawer.fillAmount = num / this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
				}
			}
		}
	}

	// Token: 0x04002872 RID: 10354
	private int m_nSkil_1 = 1171;

	// Token: 0x04002873 RID: 10355
	private int m_nSkil_2 = 1172;

	// Token: 0x04002874 RID: 10356
	private int m_nBuff_1 = 1043;

	// Token: 0x04002875 RID: 10357
	private int m_nBuff_2 = 1044;

	// Token: 0x04002876 RID: 10358
	private GameObject oPawer;

	// Token: 0x04002877 RID: 10359
	private Image imagepawer;

	// Token: 0x04002878 RID: 10360
	private CheckAutoAirUI checkAuto;

	// Token: 0x04002879 RID: 10361
	private GameObject zx;

	// Token: 0x0400287A RID: 10362
	private Text tValue;

	// Token: 0x0400287B RID: 10363
	private MeshRenderer meshr;

	// Token: 0x0400287C RID: 10364
	private GameObject ui_quan;
}
