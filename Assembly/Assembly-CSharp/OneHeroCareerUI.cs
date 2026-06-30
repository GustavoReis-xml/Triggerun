using System;
using System.Collections;
using UnityEngine.UI;

// Token: 0x02000278 RID: 632
public class OneHeroCareerUI : Window
{
	// Token: 0x06001D3E RID: 7486 RVA: 0x0010E414 File Offset: 0x0010C614
	protected override void Awake()
	{
		base.Awake();
		this.tLevel = base.transform.Find("Text").GetComponent<Text>();
		this.lvldi = base.transform.Find("lvldi").GetComponent<Image>();
		this.tName = base.transform.Find("Text (2)").GetComponent<Text>();
		this.tTime = base.transform.Find("Text (3)").GetComponent<Text>();
		this.raw = base.transform.Find("RawImage").GetComponent<RawImage>();
		this.FIllImage = base.transform.Find("fill").GetComponent<Image>();
		this.FIllImage.enabled = false;
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x0010E4D8 File Offset: 0x0010C6D8
	private void Start()
	{
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0010E4DB File Offset: 0x0010C6DB
	private void Update()
	{
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0010E4E0 File Offset: 0x0010C6E0
	public void OnInit(ArrayList szData, int nAllTime)
	{
		int num = -1;
		string text = string.Empty;
		string text2 = string.Empty;
		string hexStr = string.Empty;
		bool flag = szData.Count < 1;
		if (!flag)
		{
			this.m_szCareerData = szData;
			int num2 = (int)szData[0];
			bool flag2 = num2 == -1;
			int num3;
			if (flag2)
			{
				num = -1;
				text = "<color='#FFAC00FF'>" + Language.GetLang(30000, new object[0]) + "</color>";
				num3 = (int)szData[4];
				text2 = "1";
				hexStr = "4eff09";
			}
			else
			{
				OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(num2);
				bool flag3 = oneHeroData != null;
				if (flag3)
				{
					num = oneHeroData.nHeroLvl;
				}
				text = szData[1].ToString();
				num3 = (int)szData[4];
				hexStr = "4eff09";
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[num2];
				bool flag4 = heroData != null;
				if (flag4)
				{
					hexStr = heroData.shengyacolor;
				}
				text2 = heroData.hhead;
			}
			bool flag5 = num != -1;
			if (flag5)
			{
				this.lvldi.gameObject.SetActive(true);
				this.tLevel.text = num.ToString();
			}
			else
			{
				this.tLevel.text = string.Empty;
				this.lvldi.gameObject.SetActive(false);
			}
			this.tName.text = text;
			this.tTime.text = Fun.SecToFullTimeStr(num3);
			float fillAmount = (float)num3 / (float)nAllTime;
			this.FIllImage.fillAmount = fillAmount;
			this.FIllImage.color = Fun.StrToColor(hexStr, 1f);
			this.FIllImage.enabled = true;
			bool flag6 = text2 != string.Empty;
			if (flag6)
			{
				fUIManager.GetInstance().GetIconAsset("kaixiangico/" + text2 + ".txt", this.raw);
			}
		}
	}

	// Token: 0x040019D1 RID: 6609
	private Text tLevel;

	// Token: 0x040019D2 RID: 6610
	private Text tName;

	// Token: 0x040019D3 RID: 6611
	private Text tTime;

	// Token: 0x040019D4 RID: 6612
	private RawImage raw;

	// Token: 0x040019D5 RID: 6613
	private Image FIllImage;

	// Token: 0x040019D6 RID: 6614
	private Image lvldi;

	// Token: 0x040019D7 RID: 6615
	public ArrayList m_szCareerData = new ArrayList();
}
