using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000280 RID: 640
public class OneResultHeroCon : Window
{
	// Token: 0x06001D6F RID: 7535 RVA: 0x0011099C File Offset: 0x0010EB9C
	protected override void Awake()
	{
		base.Awake();
		this.herohead = base.transform.Find("herohead").GetComponent<RawImage>();
		this.heroname = base.transform.Find("heroname").GetComponent<Text>();
		this.level = base.transform.Find("level").GetComponent<Text>();
		this.oExp = base.transform.Find("exp").GetComponent<Image>();
		this.addexp = base.transform.Find("addexp").GetComponent<Text>();
		this.addico = base.transform.Find("addico").gameObject;
		this.addico.GetComponent<Image>().enabled = true;
		this.xianico = base.transform.Find("xian").GetComponent<Image>();
		this.xianico.enabled = false;
		this.data0 = base.transform.Find("data0").gameObject;
		this.data1 = base.transform.Find("data1").gameObject;
		this.data2 = base.transform.Find("data2").gameObject;
		this.data3 = base.transform.Find("data3").gameObject;
		this.ui_renwutouxiang = base.transform.Find("ui_renwutouxiang").gameObject;
		this.ui_renwutouxiang.SetActive(false);
		this.InitShow();
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x00110B28 File Offset: 0x0010ED28
	public void InitShow()
	{
		this.herohead.enabled = false;
		this.heroname.text = string.Empty;
		this.level.text = string.Empty;
		this.oExp.fillAmount = 0f;
		this.addexp.text = string.Empty;
		this.addico.SetActive(false);
		this.data0.SetActive(false);
		this.data1.SetActive(false);
		this.data2.SetActive(false);
		this.data3.SetActive(false);
		this.ui_renwutouxiang.SetActive(false);
		this.xianico.enabled = false;
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x00110BE4 File Offset: 0x0010EDE4
	public void OnSetUi(ArrayList szData, HeroPlayer hero, int nType)
	{
		bool flag = szData.Count < 1;
		if (flag)
		{
			this.InitShow();
		}
		else
		{
			this.heroMe = hero;
			bool flag2 = this.heroMe == null;
			if (!flag2)
			{
				bool flag3 = !base.gameObject.activeSelf;
				if (flag3)
				{
					base.gameObject.SetActive(true);
				}
				int num = (int)szData[1];
				HeroData heroData = roleuiManager.getHeroData(num);
				OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(num);
				bool flag4 = oneHeroData != null;
				if (flag4)
				{
					string sIconName = fGameManager.mloadConfig.AllHeroUnlock[num].szSkinUnlock[1].sIconName;
					sysload.loadImage(this.herohead, "herounlock/skin/" + sIconName + ".txt", null);
					this.heroname.text = heroData.hname;
					this.SetHeroData(this.data0, 11013, (int)szData[2]);
					this.SetHeroData(this.data1, 11011, (int)szData[3]);
					this.SetHeroData(this.data2, 11014, (int)szData[5]);
					this.SetHeroData(this.data3, 11012, (int)szData[4]);
					this.nAddExpValue = (int)szData[6];
					this.nAddLevel = (int)szData[7];
					this.nOldExp = (int)szData[8];
					this.nOldLevel = oneHeroData.nHeroLvl - this.nAddLevel;
					this.xianico.enabled = false;
					bool flag5 = nType == 0;
					if (flag5)
					{
						int heroLvlupExp = Fun.GetHeroLvlupExp(this.nOldLevel);
						this.level.text = "Lv." + this.nOldLevel.ToString();
						float fillAmount = 0.82f * (float)this.nOldExp / (float)heroLvlupExp + 0.07f;
						this.oExp.fillAmount = fillAmount;
						this.nAddValue = 0;
						this.fcheck = Time.time;
						this.nAddExpEffect = this.nAddExpValue;
					}
					else
					{
						this.level.text = "Lv." + oneHeroData.nHeroLvl.ToString();
						int heroLvlupExp = Fun.GetHeroLvlupExp(this.nOldLevel + this.nAddLevel);
						float fillAmount2 = 0.87f * ((float)(this.nOldLevel + this.nAddLevel) / (float)heroLvlupExp) + 0.07f;
						this.oExp.fillAmount = fillAmount2;
						this.addexp.text = Language.GetLang(11010, new object[]
						{
							this.nAddExpValue
						});
					}
				}
			}
		}
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x00110EB8 File Offset: 0x0010F0B8
	private void SetHeroData(GameObject oObj, int nNum, int nValue)
	{
		GameObject gameObject = oObj.transform.Find("pic").gameObject;
		Text component = oObj.transform.Find("Image/value").transform.GetComponent<Text>();
		gameObject.SetActive(false);
		component.text = Language.GetLang(nNum, new object[]
		{
			nValue
		});
		bool flag = !oObj.activeSelf;
		if (flag)
		{
			oObj.SetActive(true);
		}
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x00110F34 File Offset: 0x0010F134
	private void Update()
	{
		this.AddExpEffect();
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x00110F40 File Offset: 0x0010F140
	private void AddExpEffect()
	{
		bool flag = this.nAddExpEffect < 1 || this.heroMe == null;
		if (!flag)
		{
			float num = Time.time - this.fcheck;
			this.fcheck = Time.time;
			this.nAddValue = Mathf.CeilToInt((float)this.nAddExpValue * num / 3f);
			bool flag2 = this.nAddValue > this.nAddExpEffect;
			if (flag2)
			{
				this.nAddValue = this.nAddExpEffect;
			}
			bool flag3 = this.nAddExpEffect > 0 && this.nAddValue < 1;
			if (flag3)
			{
				this.nAddValue = 1;
			}
			this.nOldExp += this.nAddValue;
			this.nAddAllValue += this.nAddValue;
			int heroLvlupExp = Fun.GetHeroLvlupExp(this.nOldLevel);
			this.level.text = "Lv." + this.nOldLevel.ToString();
			float fillAmount = 0.87f * ((float)this.nOldExp / (float)heroLvlupExp) + 0.07f;
			this.oExp.fillAmount = fillAmount;
			this.addexp.text = Language.GetLang(11010, new object[]
			{
				this.nAddAllValue
			});
			bool flag4 = this.nAddExpEffect > 0;
			if (flag4)
			{
				this.nAddExpEffect -= this.nAddValue;
				bool flag5 = this.nAddExpEffect < 1;
				if (flag5)
				{
					this.nAddExpEffect = 0;
				}
			}
			bool flag6 = this.nOldExp >= heroLvlupExp && this.nAddLevel > 0;
			if (flag6)
			{
				this.nOldLevel++;
				this.nAddLevel--;
				this.nOldExp -= heroLvlupExp;
				this.OnUpLevelEffect();
				this.ui_renwutouxiang.SetActive(false);
				this.ui_renwutouxiang.SetActive(true);
			}
		}
	}

	// Token: 0x06001D75 RID: 7541 RVA: 0x0011112C File Offset: 0x0010F32C
	private void OnUpLevelEffect()
	{
		this.addico.transform.localPosition = new Vector3(68f, 105f, 0f);
		bool flag = !this.addico.activeSelf;
		if (flag)
		{
			this.addico.SetActive(true);
		}
		Tweener t = this.addico.transform.DOLocalMove(new Vector3(68f, 140f, 0f), 0.5f, false);
		t.OnComplete(delegate
		{
			bool activeSelf = this.addico.activeSelf;
			if (activeSelf)
			{
				this.addico.SetActive(false);
			}
		});
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x001111BE File Offset: 0x0010F3BE
	public override void Close()
	{
		this.InitShow();
		this.InitEffect();
		base.Close();
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x001111D6 File Offset: 0x0010F3D6
	private void InitEffect()
	{
		this.nAddExpEffect = 0;
		this.nAddLevel = 0;
		this.nOldExp = 0;
		this.nOldLevel = 0;
		this.nAddExpValue = 0;
		this.fcheck = 0f;
		this.nAddValue = 0;
		this.nAddAllValue = 0;
	}

	// Token: 0x04001A30 RID: 6704
	private RawImage herohead;

	// Token: 0x04001A31 RID: 6705
	private Text heroname;

	// Token: 0x04001A32 RID: 6706
	private Text level;

	// Token: 0x04001A33 RID: 6707
	private Image oExp;

	// Token: 0x04001A34 RID: 6708
	private Text addexp;

	// Token: 0x04001A35 RID: 6709
	private Image xianico;

	// Token: 0x04001A36 RID: 6710
	private GameObject addico;

	// Token: 0x04001A37 RID: 6711
	private GameObject data0;

	// Token: 0x04001A38 RID: 6712
	private GameObject data1;

	// Token: 0x04001A39 RID: 6713
	private GameObject data2;

	// Token: 0x04001A3A RID: 6714
	private GameObject data3;

	// Token: 0x04001A3B RID: 6715
	private GameObject ui_renwutouxiang;

	// Token: 0x04001A3C RID: 6716
	private HeroPlayer heroMe;

	// Token: 0x04001A3D RID: 6717
	private int nAddExpEffect;

	// Token: 0x04001A3E RID: 6718
	private int nAddLevel;

	// Token: 0x04001A3F RID: 6719
	private int nOldExp;

	// Token: 0x04001A40 RID: 6720
	private int nOldLevel;

	// Token: 0x04001A41 RID: 6721
	private int nAddExpValue;

	// Token: 0x04001A42 RID: 6722
	private float fcheck;

	// Token: 0x04001A43 RID: 6723
	private int nAddValue;

	// Token: 0x04001A44 RID: 6724
	private int nAddAllValue;
}
