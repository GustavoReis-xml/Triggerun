using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000281 RID: 641
public class OneSelectHeroTopCon : Window
{
	// Token: 0x06001D7A RID: 7546 RVA: 0x0011124C File Offset: 0x0010F44C
	protected override void Awake()
	{
		base.Awake();
		this.bBut = base.transform.GetComponent<Button>();
		this.heropic = base.transform.Find("heropic").GetComponent<RawImage>();
		this.oSelectEffect = base.transform.Find("select").gameObject;
		this.expvalue = base.transform.Find("expvalue").GetComponent<Image>();
		this.mash = base.transform.Find("mash").GetComponent<Image>();
		this.suo = base.transform.Find("suo").gameObject;
		this.Tlvl = base.transform.Find("lvl").GetComponent<Text>();
		this.TlvlTransform = base.transform.Find("lvl").GetComponent<RectTransform>();
		this.xianimg = base.transform.Find("xian").GetComponent<Image>();
	}

	// Token: 0x06001D7B RID: 7547 RVA: 0x0011134C File Offset: 0x0010F54C
	public void OnShowData(int nHeroId, int nNum, int m_nNowMyHeroId)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = !fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nHeroId);
		if (flag)
		{
			this.InitUI(nNum);
		}
		else
		{
			HeroData heroData = roleuiManager.getHeroData(nHeroId);
			bool flag2 = heroData == null;
			if (flag2)
			{
				this.InitUI(nNum);
			}
			else
			{
				this.m_nHeroID = nHeroId;
				arrayList = roleuiManager.GetInstance().GetTeamSelectOkHero();
				base.name = nHeroId.ToString();
				OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nHeroId);
				this.xianimg.enabled = false;
				string sIconName = fGameManager.mloadConfig.AllHeroUnlock[nHeroId].szSkinUnlock[1].sIconName;
				bool flag3 = this.heropic != null && sIconName != string.Empty;
				if (flag3)
				{
					sysload.loadImage(this.heropic, "herounlock/skin/" + sIconName + ".txt", null);
				}
				this.mash.enabled = false;
				bool flag4 = oneHeroData.nOpenState > 0 || MatchManager.GetInstance().m_pFbData.nType == 6;
				if (flag4)
				{
					float fillAmount = 0.82f * (float)oneHeroData.nHeroExp / (float)Fun.GetHeroLvlupExp(oneHeroData.nHeroLvl) + 0.07f;
					this.expvalue.fillAmount = fillAmount;
					this.suo.SetActive(false);
					bool flag5 = nHeroId != m_nNowMyHeroId;
					if (flag5)
					{
						this.oSelectEffect.SetActive(false);
					}
					Vector3 zero = Vector3.zero;
					int num = arrayList.IndexOf(nHeroId);
					bool flag6 = num != -1 && MatchManager.GetInstance().m_pFbData.nType != 11;
					if (flag6)
					{
						this.mash.enabled = true;
						this.bBut.enabled = false;
						this.Tlvl.text = Language.GetLang(2042, new object[0]);
						zero.y = 6f;
					}
					else
					{
						this.bBut.enabled = true;
						this.Tlvl.text = oneHeroData.nHeroLvl.ToString();
					}
					this.TlvlTransform.anchoredPosition3D = zero;
					this.m_nHeroOpenStat = 1;
				}
				else
				{
					this.expvalue.fillAmount = 0f;
					this.suo.SetActive(true);
					this.oSelectEffect.SetActive(false);
					this.bBut.enabled = true;
					this.Tlvl.text = Language.GetLang(7035, new object[0]);
					this.m_nHeroOpenStat = 0;
				}
				bool flag7 = !base.gameObject.activeSelf;
				if (flag7)
				{
					base.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06001D7C RID: 7548 RVA: 0x00111610 File Offset: 0x0010F810
	private void InitUI(int nNum)
	{
		this.m_nHeroID = 0;
		this.m_nHeroOpenStat = 0;
		this.bBut.enabled = false;
		this.oSelectEffect.SetActive(false);
		this.heropic.enabled = false;
		this.mash.enabled = true;
		this.suo.SetActive(false);
		this.Tlvl.text = string.Empty;
		this.expvalue.fillAmount = 0f;
		base.name = "hero" + nNum.ToString();
		bool activeSelf = base.gameObject.activeSelf;
		if (activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001A45 RID: 6725
	private RawImage heropic;

	// Token: 0x04001A46 RID: 6726
	private GameObject oSelectEffect;

	// Token: 0x04001A47 RID: 6727
	private Image expvalue;

	// Token: 0x04001A48 RID: 6728
	private Image mash;

	// Token: 0x04001A49 RID: 6729
	private GameObject suo;

	// Token: 0x04001A4A RID: 6730
	private Text Tlvl;

	// Token: 0x04001A4B RID: 6731
	private Image xianimg;

	// Token: 0x04001A4C RID: 6732
	private Button bBut;

	// Token: 0x04001A4D RID: 6733
	private RectTransform TlvlTransform;

	// Token: 0x04001A4E RID: 6734
	public int m_nHeroOpenStat;

	// Token: 0x04001A4F RID: 6735
	public int m_nHeroID;
}
