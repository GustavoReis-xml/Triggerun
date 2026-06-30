using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B6 RID: 1206
public class zhunxingDaJi : zhunxingBase
{
	// Token: 0x06002EB5 RID: 11957 RVA: 0x001A1678 File Offset: 0x0019F878
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		Transform x = gotherui.FindChild("dunpai");
		bool flag = x == null;
		if (flag)
		{
			Debug.Log("妲己zhunxingReinhardt初始化时gotherui中没有找到dunpai对象");
		}
		this.GDunParent = gotherui.FindChild("dunpai").gameObject;
		this.powervalue = this.GDunParent.transform.FindChild("powervalue").GetComponent<Image>();
		this.powerText = this.GDunParent.transform.FindChild("value").GetComponent<Text>();
		this.GDunParent.SetActive(false);
		this.GDunpower = gotherui.FindChild("power").gameObject;
		this.GDunpower.SetActive(false);
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x001A173C File Offset: 0x0019F93C
	protected override void Update()
	{
		base.Update();
		bool flag = this.hero == null || this.hero.haveDestroy || !this.hero.playerHaveCreate || this.GDunParent == null || this.GDunpower == null;
		if (!flag)
		{
			bool flag2 = this.skillShield == null;
			if (flag2)
			{
				bool flag3 = this.hero.skillEffectDict.ContainsKey(1151);
				if (flag3)
				{
					this.skillShield = this.hero.skillEffectDict[1151];
				}
			}
			else
			{
				bool flag4 = !this.hero.isDead;
				if (flag4)
				{
					bool shield_Call_State = ((ISkillDataSkillDaJiHudun)this.skillShield.ISD).Shield_Call_State;
					if (shield_Call_State)
					{
						bool flag5 = !this.GDunParent.activeSelf;
						if (flag5)
						{
							this.GDunParent.SetActive(true);
						}
						this.powerText.text = ((ISkillDataSkillDaJiHudun)this.skillShield.ISD).Shield_Current_Energy.ToString();
						this.powervalue.fillAmount = (float)((ISkillDataSkillDaJiHudun)this.skillShield.ISD).Shield_Current_Energy / (float)((ISkillDataSkillDaJiHudun)this.skillShield.ISD).Shield_Max_Energy;
					}
					else
					{
						bool activeSelf = this.GDunParent.activeSelf;
						if (activeSelf)
						{
							this.GDunParent.SetActive(false);
						}
					}
				}
				else
				{
					bool activeSelf2 = this.GDunParent.activeSelf;
					if (activeSelf2)
					{
						this.GDunParent.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x04002850 RID: 10320
	private GameObject GDunParent;

	// Token: 0x04002851 RID: 10321
	private Image powervalue;

	// Token: 0x04002852 RID: 10322
	private Text powerText;

	// Token: 0x04002853 RID: 10323
	private GameObject GDunpower;

	// Token: 0x04002854 RID: 10324
	private Text energyUiNow;

	// Token: 0x04002855 RID: 10325
	private Text energyUiMax;

	// Token: 0x04002856 RID: 10326
	private Text dunname;

	// Token: 0x04002857 RID: 10327
	private Image dpvalue;

	// Token: 0x04002858 RID: 10328
	private Material material;

	// Token: 0x04002859 RID: 10329
	private float e_now;

	// Token: 0x0400285A RID: 10330
	private float e_max;

	// Token: 0x0400285B RID: 10331
	private SkillBase skillShield;
}
