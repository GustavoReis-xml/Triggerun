using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B7 RID: 1207
public class zhunxingDaWei : zhunxingBase
{
	// Token: 0x06002EB8 RID: 11960 RVA: 0x001A18F4 File Offset: 0x0019FAF4
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.oZxObj = gotherui.FindChild("zx").gameObject;
		this.fill = this.oZxObj.transform.Find("fill").GetComponent<Image>();
		this.oZxObj.SetActive(false);
		Transform x = gotherui.FindChild("read");
		bool flag = x == null;
		if (flag)
		{
			Debug.Log("准星zhunxingdawei初始化时gotherui中没有找到read对象");
		}
		this.gRead = gotherui.FindChild("read").gameObject;
		this.ldesc = this.gRead.transform.Find("ldesc").GetComponent<Text>();
		this.rdesc = this.gRead.transform.Find("rdesc").GetComponent<Text>();
		this.ldesc.text = Language.GetLang(3017, new object[0]);
		this.rdesc.text = Language.GetLang(3014, new object[0]);
		this.rImg = this.gRead.transform.Find("Image1").GetComponent<Image>();
		this.gRead.SetActive(false);
		this.hongwai = gotherui.FindChild("hongwai").gameObject;
		this.hwfill = this.hongwai.transform.Find("Image2").GetComponent<Image>();
		this.hongwai.transform.Find("desc").GetComponent<Text>().text = Language.GetLang(3036, new object[0]);
		this.hongwai.SetActive(false);
		this.djsimg = gotherui.FindChild("djsimg").gameObject;
		this.djffill = this.djsimg.transform.Find("img").GetComponent<Image>();
		this.djstime = this.djsimg.transform.Find("time").GetComponent<Text>();
		this.djsimg.SetActive(false);
	}

	// Token: 0x06002EB9 RID: 11961 RVA: 0x001A1B03 File Offset: 0x0019FD03
	protected override void Update()
	{
		base.Update();
		this.CheckDaZhaoDjs();
		this.CheckSkillDjs();
		this.CheckskillShield();
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x001A1B24 File Offset: 0x0019FD24
	public override void ShowSkillDo(object opam)
	{
		int num = (int)opam;
		bool flag = num == 1201;
		if (flag)
		{
			bool flag2 = this.hero.skillEffectDict.ContainsKey(1201);
			if (flag2)
			{
				this.skillShield = this.hero.skillEffectDict[1201];
			}
			bool flag3 = this.skillShield == null;
			if (flag3)
			{
				return;
			}
			float shield_Time = ((ISkillDataSkillDWShield)this.skillShield.ISD).Shield_Time;
			this.fEndTime = Time.time + shield_Time;
			bool flag4 = !this.oZxObj.activeSelf;
			if (flag4)
			{
				this.oZxObj.SetActive(true);
			}
		}
		bool flag5 = num == 1202;
		if (flag5)
		{
			this.fSkillEndTime = Time.time + this.skillETime;
			bool flag6 = !this.djsimg.activeSelf;
			if (flag6)
			{
				this.djsimg.SetActive(true);
			}
		}
	}

	// Token: 0x06002EBB RID: 11963 RVA: 0x001A1C24 File Offset: 0x0019FE24
	private void CheckskillShield()
	{
		bool flag = this.oZxObj == null || !this.oZxObj.activeSelf || this.fEndTime <= 0f || !(this.skillShield != null);
		if (!flag)
		{
			float shield_Time = ((ISkillDataSkillDWShield)this.skillShield.ISD).Shield_Time;
			float num = this.fEndTime - Time.time;
			bool flag2 = num > 0f;
			if (flag2)
			{
				this.fill.fillAmount = num / shield_Time;
			}
			else
			{
				bool activeSelf = this.oZxObj.activeSelf;
				if (activeSelf)
				{
					this.oZxObj.SetActive(false);
				}
				this.fEndTime = 0f;
				this.skillShield = null;
			}
		}
	}

	// Token: 0x06002EBC RID: 11964 RVA: 0x001A1CE8 File Offset: 0x0019FEE8
	private void CheckSkillDjs()
	{
		bool flag = this.djsimg == null || !this.djsimg.activeSelf;
		if (!flag)
		{
			float num = this.fSkillEndTime - Time.time;
			bool flag2 = num > 0f;
			if (flag2)
			{
				this.djffill.fillAmount = num / this.skillETime;
				this.djstime.text = Mathf.CeilToInt(num).ToString() + "s";
			}
			else
			{
				bool activeSelf = this.djsimg.activeSelf;
				if (activeSelf)
				{
					this.djsimg.SetActive(false);
				}
				this.fSkillEndTime = 0f;
			}
		}
	}

	// Token: 0x06002EBD RID: 11965 RVA: 0x001A1D9C File Offset: 0x0019FF9C
	public override void ShowSkillFinish(object opam)
	{
		int num = (int)opam;
		bool flag = num == 1201;
		if (flag)
		{
			bool flag2 = this.oZxObj != null && this.oZxObj.activeSelf;
			if (flag2)
			{
				this.oZxObj.SetActive(false);
			}
			this.fEndTime = 0f;
			this.skillShield = null;
		}
		bool flag3 = num == 1202;
		if (flag3)
		{
			bool activeSelf = this.djsimg.activeSelf;
			if (activeSelf)
			{
				this.djsimg.SetActive(false);
			}
			this.fSkillEndTime = 0f;
		}
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x001A1E3C File Offset: 0x001A003C
	public override void readyShow()
	{
		bool flag = !this.gRead.activeSelf;
		if (flag)
		{
			this.gRead.SetActive(true);
			base.SetKeyString(this.hero.playerCommon.m_nModelIndex, this.rdesc, this.rImg);
		}
		base.zhunxingshowOrHide(false);
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x001A1E98 File Offset: 0x001A0098
	public override void readyDisable()
	{
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
		base.zhunxingshowOrHide(true);
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x001A1ECC File Offset: 0x001A00CC
	public override void OnDeath()
	{
		base.OnDeath();
		bool activeSelf = this.gRead.activeSelf;
		if (activeSelf)
		{
			this.gRead.SetActive(false);
		}
	}

	// Token: 0x06002EC1 RID: 11969 RVA: 0x001A1F00 File Offset: 0x001A0100
	private void CheckDaZhaoDjs()
	{
		bool flag = this.hongwai == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerDazhao != null && this.hero.playerDazhao.doing && this.hero.playerDazhao.cdazhaomaininfo.fEndTime > 0f;
			if (flag2)
			{
				float num = this.hero.playerDazhao.cdazhaomaininfo.fEndTime - Time.time;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool flag4 = !this.hongwai.activeSelf;
					if (flag4)
					{
						this.hongwai.SetActive(true);
					}
					float fContinuesTime = this.hero.playerDazhao.cdazhaomaininfo.fContinuesTime;
					this.hwfill.fillAmount = num / fContinuesTime;
					return;
				}
			}
			bool activeSelf = this.hongwai.activeSelf;
			if (activeSelf)
			{
				this.hongwai.SetActive(false);
			}
		}
	}

	// Token: 0x0400285C RID: 10332
	private GameObject oZxObj;

	// Token: 0x0400285D RID: 10333
	private Image fill;

	// Token: 0x0400285E RID: 10334
	private float fEndTime;

	// Token: 0x0400285F RID: 10335
	private float fSkillEndTime;

	// Token: 0x04002860 RID: 10336
	private float skillETime = 1f;

	// Token: 0x04002861 RID: 10337
	private GameObject hongwai;

	// Token: 0x04002862 RID: 10338
	private Image hwfill;

	// Token: 0x04002863 RID: 10339
	private GameObject djsimg;

	// Token: 0x04002864 RID: 10340
	private Image djffill;

	// Token: 0x04002865 RID: 10341
	private Text djstime;

	// Token: 0x04002866 RID: 10342
	private Text ldesc;

	// Token: 0x04002867 RID: 10343
	private Text rdesc;

	// Token: 0x04002868 RID: 10344
	private Image rImg;

	// Token: 0x04002869 RID: 10345
	private SkillBase skillShield;

	// Token: 0x0400286A RID: 10346
	private GameObject gRead;
}
