using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CB RID: 1227
public class zhunxingReinhardt : zhunxingBase
{
	// Token: 0x06002F0C RID: 12044 RVA: 0x001A4384 File Offset: 0x001A2584
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		Transform x = gotherui.FindChild("dunpai");
		bool flag = x == null;
		if (flag)
		{
			Debug.Log("刑天zhunxingReinhardt初始化时gotherui中没有找到dunpai对象");
		}
		this.GDunParent = gotherui.FindChild("dunpai").gameObject;
		this.powervalue = this.GDunParent.transform.FindChild("powervalue").GetComponent<Image>();
		this.powerText = this.GDunParent.transform.FindChild("value").GetComponent<Text>();
		this.GDunParent.SetActive(false);
		this.GDunpower = gotherui.FindChild("power").gameObject;
		this.energyUiNow = this.GDunpower.transform.FindChild("leftnum").GetComponent<Text>();
		this.energyUiMax = this.GDunpower.transform.FindChild("rightnum").GetComponent<Text>();
		this.GDunpower.transform.FindChild("name").GetComponent<Text>().text = Language.GetLang(3015, new object[0]);
		this.dpvalue = this.GDunpower.transform.FindChild("dpvalue").GetComponent<Image>();
		this.GDunpower.SetActive(false);
	}

	// Token: 0x06002F0D RID: 12045 RVA: 0x001A44D8 File Offset: 0x001A26D8
	protected override void Update()
	{
		base.Update();
		bool flag = this.hero == null || this.hero.haveDestroy || !this.hero.playerHaveCreate || this.GDunParent == null || this.GDunpower == null;
		if (!flag)
		{
			bool flag2 = this.skillShield == null;
			if (flag2)
			{
				bool flag3 = this.hero.skillEffectDict.ContainsKey(1021);
				if (flag3)
				{
					this.skillShield = this.hero.skillEffectDict[1021];
				}
			}
			else
			{
				bool flag4 = !this.hero.isDead;
				if (flag4)
				{
					bool shield_Call_State = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Call_State;
					if (shield_Call_State)
					{
						bool flag5 = !this.GDunParent.activeSelf;
						if (flag5)
						{
							this.GDunParent.SetActive(true);
						}
						this.powerText.text = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Current_Energy.ToString();
						this.powervalue.fillAmount = (float)((ISkillDataSkillShield)this.skillShield.ISD).Shield_Current_Energy / (float)((ISkillDataSkillShield)this.skillShield.ISD).Shield_Max_Energy;
					}
					else
					{
						bool activeSelf = this.GDunParent.activeSelf;
						if (activeSelf)
						{
							this.GDunParent.SetActive(false);
						}
					}
					bool flag6 = !this.GDunpower.activeSelf;
					if (flag6)
					{
						this.GDunpower.SetActive(true);
					}
					this.energyUiNow.text = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Current_Energy.ToString();
					this.energyUiMax.text = ((ISkillDataSkillShield)this.skillShield.ISD).Shield_Max_Energy.ToString();
					this.dpvalue.fillAmount = (float)((ISkillDataSkillShield)this.skillShield.ISD).Shield_Current_Energy / (float)((ISkillDataSkillShield)this.skillShield.ISD).Shield_Max_Energy;
				}
				else
				{
					bool activeSelf2 = this.GDunParent.activeSelf;
					if (activeSelf2)
					{
						this.GDunParent.SetActive(false);
					}
					bool activeSelf3 = this.GDunpower.activeSelf;
					if (activeSelf3)
					{
						this.GDunpower.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x040028C2 RID: 10434
	private GameObject GDunParent;

	// Token: 0x040028C3 RID: 10435
	private Image powervalue;

	// Token: 0x040028C4 RID: 10436
	private Text powerText;

	// Token: 0x040028C5 RID: 10437
	private GameObject GDunpower;

	// Token: 0x040028C6 RID: 10438
	private Text energyUiNow;

	// Token: 0x040028C7 RID: 10439
	private Text energyUiMax;

	// Token: 0x040028C8 RID: 10440
	private Text dunname;

	// Token: 0x040028C9 RID: 10441
	private Image dpvalue;

	// Token: 0x040028CA RID: 10442
	private Material material;

	// Token: 0x040028CB RID: 10443
	private float e_now;

	// Token: 0x040028CC RID: 10444
	private float e_max;

	// Token: 0x040028CD RID: 10445
	private SkillBase skillShield;
}
