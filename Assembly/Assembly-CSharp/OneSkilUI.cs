using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000282 RID: 642
public class OneSkilUI : Window
{
	// Token: 0x06001D7E RID: 7550 RVA: 0x001116D0 File Offset: 0x0010F8D0
	private void OnStat()
	{
		this.skillback = base.transform.Find("up/back").GetComponent<Image>();
		this.lengque = base.transform.Find("up/lengque").gameObject;
		this.lqvalue = this.lengque.transform.Find("lq").GetComponent<Image>();
		this.skilljingyong = base.transform.Find("up/jingyong").GetComponent<Image>();
		this.skillready = base.transform.Find("up/ready").gameObject;
		this.skillnum = base.transform.Find("up/jishu").GetComponent<Text>();
		this.skillpic = base.transform.Find("up/skillpic").GetComponent<RawImage>();
		this.lengqueeffect = base.transform.Find("up/ui_lenque/lengque").gameObject;
		this.skilldown = base.transform.Find("down").gameObject;
		this.skillm_key = base.transform.Find("down/m_key").GetComponent<Image>();
		this.skilln_key = base.transform.Find("down/n_key").GetComponent<Image>();
		this.skillt_key = base.transform.Find("down/t_key").GetComponent<Image>();
		this.skill_keyTxt = base.transform.Find("down/key_txt").GetComponent<Text>();
		this.GTop = this.GMainUi.transform.FindChild("top").gameObject;
		this.tTJishu = this.GTop.transform.FindChild("Text").GetComponent<Text>();
		this.imageTLp = this.GTop.transform.FindChild("fillcount").GetComponent<Image>();
		this.imagefull = this.GTop.transform.FindChild("man").GetComponent<Image>();
		this.dtsusenum = base.transform.Find("up/dtsusenum").gameObject;
		this.dtsusrNumText = this.dtsusenum.transform.Find("num").GetComponent<Text>();
		this.dtsusenum.SetActive(false);
		this.GTop.SetActive(false);
		this.InitUi();
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x00111920 File Offset: 0x0010FB20
	public void InitUi()
	{
		this.lengque.SetActive(false);
		this.skilljingyong.enabled = false;
		this.skillready.SetActive(false);
		this.skillnum.text = string.Empty;
		this.skillpic.enabled = false;
		this.skilldown.SetActive(false);
		this.skillm_key.enabled = false;
		this.skilln_key.enabled = false;
		this.skillt_key.enabled = false;
		this.skill_keyTxt.text = string.Empty;
		this.GTop.SetActive(false);
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x001119C8 File Offset: 0x0010FBC8
	public void showui(int skindex, GameObject game, int _skType, object oPamClass, NetLiving _netliving)
	{
		this.heroMainMe = _netliving;
		this.nskindex = skindex;
		this.GMainUi = game;
		this.nSkillType = _skType;
		bool flag = this.nSkillType == 0;
		if (flag)
		{
			this.cpstruct = (CPSkillStruct)oPamClass;
		}
		else
		{
			this.csstruct = (CSpecialSkillStruct)oPamClass;
		}
		this.OnStat();
		this.showSkillCommon();
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x00111A30 File Offset: 0x0010FC30
	private void showSkillCommon()
	{
		string spath = (this.nSkillType != 0) ? ("skillicon/" + this.csstruct.icon + ".txt") : ("skillicon/" + this.cpstruct._skilldata.icon + ".txt");
		sysload.loadImage(this.skillpic, spath, null);
		this.getKJJDesc();
		bool flag = this.cpstruct.m_nDoNumberMax > 1;
		if (flag)
		{
			this.GTop.SetActive(true);
		}
		this.skillback.enabled = true;
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x00111AC8 File Offset: 0x0010FCC8
	public void getKJJDesc()
	{
		string text = string.Empty;
		string a = string.Empty;
		string dokey = this.cpstruct._skilldata.dokey;
		bool flag = this.heroMainMe.heroData != null;
		if (flag)
		{
			a = this.heroMainMe.heroData.secSkill;
		}
		this.skillm_key.enabled = false;
		bool flag2 = !this.skilldown.activeSelf;
		if (flag2)
		{
			this.skilldown.SetActive(true);
		}
		bool flag3 = a == dokey && a != "0";
		if (flag3)
		{
			text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.heroMainMe.playerCommon.m_nModelIndex).Key_Use_Weapon_2;
			bool flag4 = text == string.Empty;
			if (flag4)
			{
				text = this.GetSkillKey(dokey);
			}
		}
		else
		{
			text = this.GetSkillKey(dokey);
		}
		bool flag5 = fGameManager.IsSingle && this.nskindex == 1031;
		if (flag5)
		{
			text = "Mouse1";
		}
		bool flag6 = text == "Mouse0";
		if (flag6)
		{
			this.skillm_key.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "zuojian");
			this.skillm_key.enabled = true;
			this.skill_keyTxt.text = string.Empty;
		}
		else
		{
			bool flag7 = text == "Mouse1";
			if (flag7)
			{
				this.skillm_key.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "youjian");
				this.skillm_key.enabled = true;
				this.skill_keyTxt.text = string.Empty;
			}
			else
			{
				this.skillm_key.enabled = false;
				bool flag8 = fGameManager.IsSingle && this.nskindex == 1032;
				if (flag8)
				{
					this.skill_keyTxt.text = "E";
				}
				else
				{
					this.skill_keyTxt.text = GameRoot.inst.Setting.Get_Key_Describe(text);
				}
			}
		}
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00111CD8 File Offset: 0x0010FED8
	private string GetSkillKey(string sDoKey)
	{
		string result = string.Empty;
		if (!(sDoKey == "Skill1"))
		{
			if (!(sDoKey == "Skill2"))
			{
				if (!(sDoKey == "Skill3"))
				{
					if (sDoKey == "WeaponSec")
					{
						result = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.heroMainMe.playerCommon.m_nModelIndex).Key_Use_Weapon_2;
					}
				}
				else
				{
					result = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.heroMainMe.playerCommon.m_nModelIndex).Key_Skill_3;
				}
			}
			else
			{
				result = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.heroMainMe.playerCommon.m_nModelIndex).Key_Skill_2;
			}
		}
		else
		{
			result = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this.heroMainMe.playerCommon.m_nModelIndex).Key_Skill_1;
		}
		return result;
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x00111DE4 File Offset: 0x0010FFE4
	public void SetChenMoUI(bool mBool)
	{
		bool flag = this.bNowStat != mBool;
		if (flag)
		{
			if (mBool)
			{
				this.Jingyong(true);
				this.setBackColor(1);
				this.setSkillColor(2);
			}
			else
			{
				this.Jingyong(false);
				this.setBackColor(0);
				this.setSkillColor(1);
			}
			this.bNowStat = mBool;
		}
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x00111E48 File Offset: 0x00110048
	private bool bCanCheck()
	{
		bool result = false;
		bool flag = MatchManager.GetInstance().m_nMatchType == 4;
		if (flag)
		{
			int num = DtsBatteManager.GetInstance().CanDtsScnSkill(this.heroMainMe, this.nskindex);
			bool flag2 = num == -1;
			if (flag2)
			{
				return result;
			}
			bool flag3 = this.cpstruct.m_nDoNumberNow == 0 && num == 0;
			if (flag3)
			{
				result = true;
				bool activeSelf = this.dtsusenum.activeSelf;
				if (activeSelf)
				{
					this.dtsusenum.SetActive(false);
				}
			}
			else
			{
				int num2 = this.cpstruct.m_nDoNumberNow + num * this.cpstruct.m_nDoNumberMax;
				bool flag4 = num2 > 0;
				if (flag4)
				{
					this.dtsusrNumText.text = num2.ToString();
					bool flag5 = !this.dtsusenum.activeSelf;
					if (flag5)
					{
						this.dtsusenum.SetActive(true);
					}
				}
			}
		}
		bool flag6 = this.heroMainMe.playerCommon.m_nChenMo == 1;
		if (flag6)
		{
			result = true;
		}
		bool flag7 = this.nlengqueing != 1 && this.heroMainMe.skillEffectDict.ContainsKey(1163) && this.nskindex == 1163 && !((ISkillDataSkill_Lilisi_shiled)this.heroMainMe.skillEffectDict[1163].ISD).bCanDoSkill;
		if (flag7)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x00111FBC File Offset: 0x001101BC
	private void Update()
	{
		bool flag = this.heroMainMe != null && !(this.heroMainMe.wc == null);
		if (flag)
		{
			float time = Time.time;
			bool flag2 = time >= this.cdCheckTimeOut;
			if (flag2)
			{
				this.cdCheckTimeOut = time + 0.1f;
				this.fixUpdate(this.heroMainMe);
			}
		}
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x00112024 File Offset: 0x00110224
	public void fixUpdate(NetLiving heroMe)
	{
		this.heroMainMe = heroMe;
		bool flag = this.bCanCheck();
		if (flag)
		{
			this.SetChenMoUI(true);
			bool activeSelf = this.skillready.activeSelf;
			if (activeSelf)
			{
				this.skillready.SetActive(false);
			}
		}
		else
		{
			this.SetChenMoUI(false);
			bool flag2 = this.nSkillType == 0;
			int nActive;
			int nDoNumberNow;
			int nDoNumberMax;
			if (flag2)
			{
				nActive = this.cpstruct.m_nActive;
				nDoNumberNow = this.cpstruct.m_nDoNumberNow;
				nDoNumberMax = this.cpstruct.m_nDoNumberMax;
			}
			else
			{
				nActive = this.csstruct.m_nActive;
				nDoNumberNow = this.csstruct.m_nDoNumberNow;
				nDoNumberMax = this.csstruct.m_nDoNumberMax;
			}
			bool flag3 = nActive == 0 || (nDoNumberNow == 0 && nDoNumberMax > 1);
			if (flag3)
			{
				bool flag4 = this.nactive == 1;
				if (flag4)
				{
					this.Jingyong(true);
					this.setBackColor(1);
					this.setSkillColor(2);
				}
			}
			else
			{
				bool flag5 = this.nactive == 0;
				if (flag5)
				{
					this.Jingyong(false);
					this.setBackColor(0);
					this.setSkillColor(1);
				}
			}
			bool flag6 = (this.nSkillType == 0 && heroMe.heroSkill.defaultReadySkill == this.nskindex) || (this.nSkillType == 1 && this.csstruct.m_nUseing == 1);
			if (flag6)
			{
				bool flag7 = !this.skillready.activeSelf;
				if (flag7)
				{
					this.skillready.SetActive(true);
				}
			}
			else
			{
				bool activeSelf2 = this.skillready.activeSelf;
				if (activeSelf2)
				{
					this.skillready.SetActive(false);
				}
			}
			bool flag8 = nDoNumberNow == nDoNumberMax;
			if (!flag8)
			{
				DateTime now = Fun.Now;
				bool flag9 = this.nSkillType == 0;
				DateTime beginTime;
				int cd;
				if (flag9)
				{
					beginTime = this.cpstruct.beginTime;
					cd = this.cpstruct.cd;
				}
				else
				{
					beginTime = this.csstruct.beginTime;
					cd = this.csstruct.cd;
				}
				float num = (float)(beginTime - now).TotalMilliseconds - 10f;
				bool flag10 = num < 0f;
				if (!flag10)
				{
					bool flag11 = num > (float)cd;
					if (flag11)
					{
						bool flag12 = !this.skillready.activeSelf;
						if (flag12)
						{
							this.skillready.SetActive(true);
						}
					}
					else
					{
						bool activeSelf3 = this.skillready.activeSelf;
						if (activeSelf3)
						{
							this.skillready.SetActive(false);
						}
						bool flag13 = this.nlengqueing == 0;
						if (flag13)
						{
							this.nlengqueing = 1;
							this.setLengque(true);
						}
						this.skillUpdate(num * 1f / (float)cd * 1f);
					}
				}
			}
		}
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x001122E4 File Offset: 0x001104E4
	private void Jingyong(bool boo)
	{
		this.skilljingyong.enabled = boo;
		this.setKjjVisiable(!boo);
		if (boo)
		{
			this.nactive = 0;
		}
		else
		{
			this.nactive = 1;
		}
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x00112323 File Offset: 0x00110523
	private void setKjjVisiable(bool boo)
	{
		this.skilldown.SetActive(boo);
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x00112334 File Offset: 0x00110534
	private void setBackColor(int nType)
	{
		switch (nType)
		{
		case 0:
			this.skillback.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "jinengkuang");
			break;
		case 1:
			this.skillback.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "jinengjin");
			break;
		case 2:
			this.skillback.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "jinenghui");
			break;
		}
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x001123B4 File Offset: 0x001105B4
	private void setReady(bool boo)
	{
		bool flag = this.skillready.activeSelf != boo;
		if (flag)
		{
			this.skillready.SetActive(boo);
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x001123E8 File Offset: 0x001105E8
	private void setLengque(bool boo)
	{
		int num = (this.nSkillType != 0) ? this.csstruct.m_nDoNumberMax : this.cpstruct.m_nDoNumberMax;
		bool flag = !boo;
		if (flag)
		{
			bool flag2 = num == 1;
			if (flag2)
			{
				this.lengque.SetActive(false);
				this.setKjjVisiable(true);
				this.skillnum.text = string.Empty;
				this.lengqueeffect.GetComponent<ParticleSystem>().Play();
			}
			else
			{
				this.showSkillLpHasCs();
				this.imagefull.enabled = true;
				this.imageTLp.fillAmount = 0f;
			}
			this.setSkillColor(1);
		}
		else
		{
			bool flag3 = num == 1;
			if (flag3)
			{
				this.lengque.SetActive(true);
				this.setKjjVisiable(false);
				this.setSkillColor(0);
			}
			else
			{
				this.showSkillLpHasCs();
				this.imagefull.enabled = false;
				this.setSkillColor(1);
			}
		}
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x001124DC File Offset: 0x001106DC
	private void setSkillColor(int nType)
	{
		Color color = Color.white;
		switch (nType)
		{
		case 0:
			color = Fun.StrToColor("ffffff", 0.2f);
			break;
		case 1:
			color = Fun.StrToColor("ffffff", 1f);
			break;
		case 2:
			color = Fun.StrToColor("ff0000", 2f);
			break;
		}
		this.skillpic.color = color;
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x0011254B File Offset: 0x0011074B
	private void showSkillLpHasCs()
	{
		this.tTJishu.text = this.cpstruct.m_nDoNumberNow.ToString();
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x0011256C File Offset: 0x0011076C
	public void InitSkill()
	{
		bool flag = this.nSkillType == 0;
		if (flag)
		{
			bool flag2 = this.cpstruct.m_nDoNumberMax == this.cpstruct.m_nDoNumberNow && this.nlengqueing == 1;
			if (flag2)
			{
				this.nlengqueing = 0;
				this.setLengque(false);
			}
			bool flag3 = this.cpstruct.m_nDoNumberMax != 1;
			if (flag3)
			{
				this.showSkillLpHasCs();
				this.imageTLp.fillAmount = 0f;
				this.setSkillColor(1);
			}
		}
		else
		{
			bool flag4 = this.nSkillType == 1 && this.csstruct.m_nDoNumberMax == this.csstruct.m_nDoNumberNow && this.nlengqueing == 1;
			if (flag4)
			{
				this.nlengqueing = 0;
				this.setLengque(false);
			}
		}
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x00112640 File Offset: 0x00110840
	public override void Close()
	{
		bool flag = this.skillpic != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.skillpic.texture);
		}
		base.Close();
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x00112678 File Offset: 0x00110878
	private void skillUpdate(float fnum)
	{
		bool flag = this.nSkillType == 0;
		if (flag)
		{
			bool flag2 = this.cpstruct.m_nDoNumberMax == 1;
			if (flag2)
			{
				this.lqvalue.fillAmount = 1f - fnum;
				float num = (float)this.cpstruct.cd / 1000f * fnum;
				this.skillnum.text = num.ToString("0.0");
			}
			else
			{
				this.imageTLp.fillAmount = 1f - fnum;
			}
		}
		else
		{
			bool flag3 = this.csstruct.m_nDoNumberMax == 1;
			if (flag3)
			{
				this.lqvalue.fillAmount = 1f - fnum;
				float num2 = (float)this.csstruct.cd / 1000f * fnum;
				this.skillnum.text = num2.ToString("0.0");
			}
		}
	}

	// Token: 0x04001A50 RID: 6736
	private Image skillback;

	// Token: 0x04001A51 RID: 6737
	private GameObject lengque;

	// Token: 0x04001A52 RID: 6738
	private Image lqvalue;

	// Token: 0x04001A53 RID: 6739
	private Image skilljingyong;

	// Token: 0x04001A54 RID: 6740
	private GameObject skillready;

	// Token: 0x04001A55 RID: 6741
	private Text skillnum;

	// Token: 0x04001A56 RID: 6742
	private RawImage skillpic;

	// Token: 0x04001A57 RID: 6743
	private GameObject lengqueeffect;

	// Token: 0x04001A58 RID: 6744
	private GameObject skilldown;

	// Token: 0x04001A59 RID: 6745
	private Image skillm_key;

	// Token: 0x04001A5A RID: 6746
	private Image skilln_key;

	// Token: 0x04001A5B RID: 6747
	private Image skillt_key;

	// Token: 0x04001A5C RID: 6748
	private Text skill_keyTxt;

	// Token: 0x04001A5D RID: 6749
	private GameObject GMainUi;

	// Token: 0x04001A5E RID: 6750
	private int nskindex;

	// Token: 0x04001A5F RID: 6751
	private int nSkillType;

	// Token: 0x04001A60 RID: 6752
	private CPSkillStruct cpstruct;

	// Token: 0x04001A61 RID: 6753
	private CSpecialSkillStruct csstruct;

	// Token: 0x04001A62 RID: 6754
	private GameObject GTop;

	// Token: 0x04001A63 RID: 6755
	private Text tTJishu;

	// Token: 0x04001A64 RID: 6756
	private Image imageTLp;

	// Token: 0x04001A65 RID: 6757
	private Image imagefull;

	// Token: 0x04001A66 RID: 6758
	private GameObject dtsusenum;

	// Token: 0x04001A67 RID: 6759
	private Text dtsusrNumText;

	// Token: 0x04001A68 RID: 6760
	private NetLiving heroMainMe;

	// Token: 0x04001A69 RID: 6761
	private int nactive = 1;

	// Token: 0x04001A6A RID: 6762
	private int nlengqueing;

	// Token: 0x04001A6B RID: 6763
	private bool bNowStat;

	// Token: 0x04001A6C RID: 6764
	private float cdCheckTimeOut;
}
