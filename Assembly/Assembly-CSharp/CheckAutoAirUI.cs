using System;
using UnityEngine.UI;

// Token: 0x02000088 RID: 136
public class CheckAutoAirUI : Window
{
	// Token: 0x060004D5 RID: 1237 RVA: 0x000278FC File Offset: 0x00025AFC
	public void OnAwake()
	{
		this.Q_img_l = base.transform.Find("ico_l").GetComponent<Image>();
		this.tText_l = base.transform.Find("ico_l/key").GetComponent<Text>();
		this.skillm_key_l = base.transform.Find("ico_l/m_key").GetComponent<Image>();
		this.skillm_key_l.enabled = false;
		this.tText_l.text = string.Empty;
		this.Q_img_r = base.transform.Find("ico_r").GetComponent<Image>();
		this.tText_r = base.transform.Find("ico_r/key").GetComponent<Text>();
		this.skillm_key_r = base.transform.Find("ico_r/m_key").GetComponent<Image>();
		this.skillm_key_r.enabled = false;
		this.tText_r.text = string.Empty;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000279E8 File Offset: 0x00025BE8
	private bool IsSkillDo(int nSkill)
	{
		bool flag = false;
		bool flag2 = this._NetPlayer == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = !this._NetPlayer.heroSkill;
			if (flag3)
			{
				result = flag;
			}
			else
			{
				int num = this._NetPlayer.heroSkill.CanDo(nSkill);
				result = (num == 1);
			}
		}
		return result;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00027A44 File Offset: 0x00025C44
	public void OnShowAutoAir_HZ(NetLiving _net, int nSkil_1, int nSkil_2, int nBuff_1, int nBuff_2, zhunxingBase zhunxing)
	{
		bool flag = CameraControl.Instance.camerHero != _net;
		if (!flag)
		{
			this._NetPlayer = _net;
			this.m_nSkil_1 = nSkil_1;
			this.m_nSkil_2 = nSkil_2;
			this.m_nBuff_1 = nBuff_1;
			this.m_nBuff_2 = nBuff_2;
			this.zhunxingobj = zhunxing;
			bool flag2 = _net != null && _net.screentarget != null && zhunxing.IsCheckTarget(_net.screentarget.NetTarget) && !_net.isDead && _net.screentarget.NetTarget.nForce == _net.nForce && !_net.screentarget.NetTarget.heroInvi;
			if (flag2)
			{
				this.GetSkillKeys();
				bool flag3 = !base.transform.gameObject.activeSelf;
				if (flag3)
				{
					base.transform.gameObject.SetActive(true);
				}
				zhunxing.OnAutoAirEffect(_net.screentarget.NetTarget, base.transform.gameObject);
			}
			else
			{
				bool activeSelf = base.transform.gameObject.activeSelf;
				if (activeSelf)
				{
					base.transform.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00027B74 File Offset: 0x00025D74
	private void GetSkillKeys()
	{
		string text = string.Empty;
		string imageName = string.Empty;
		text = this.GetKeyString(this.m_nSkil_1);
		bool flag = text == "zuojian" || text == "youjian";
		if (flag)
		{
			this.skillm_key_l.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, text);
			this.skillm_key_l.enabled = true;
			this.tText_l.text = string.Empty;
		}
		else
		{
			this.skillm_key_l.enabled = false;
			this.tText_l.text = "<color='#00ffff'>" + text + "</color>";
		}
		imageName = "suoding1";
		bool flag2 = !this.IsSkillDo(this.m_nSkil_1);
		if (flag2)
		{
			imageName = "suoding0";
			bool flag3 = !this.skillm_key_l.enabled;
			if (flag3)
			{
				this.tText_l.text = "<color='#FF0000'>" + text + "</color>";
			}
		}
		this.Q_img_l.sprite = Window.spriteName(AtlasType.ATLAS_ZHUNXING, imageName);
		text = this.GetKeyString(this.m_nSkil_2);
		bool flag4 = text == "zuojian" || text == "youjian";
		if (flag4)
		{
			this.skillm_key_r.sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, text);
			this.skillm_key_r.enabled = true;
		}
		else
		{
			this.skillm_key_r.enabled = false;
			this.tText_r.text = "<color='#00ffff'>" + text + "</color>";
		}
		imageName = "suoding1";
		bool flag5 = !this.IsSkillDo(this.m_nSkil_2);
		if (flag5)
		{
			imageName = "suoding0";
			bool flag6 = !this.skillm_key_r.enabled;
			if (flag6)
			{
				this.tText_r.text = "<color='#FF0000'>" + text + "</color>";
			}
		}
		this.Q_img_r.sprite = Window.spriteName(AtlasType.ATLAS_ZHUNXING, imageName);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00027D78 File Offset: 0x00025F78
	private string GetKeyString(int nSkillId)
	{
		string text = string.Empty;
		string empty = string.Empty;
		bool flag = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(nSkillId);
		if (flag)
		{
			PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[nSkillId];
			bool flag2 = playerSkillData != null;
			if (flag2)
			{
				bool flag3 = playerSkillData.dokey == "Skill1";
				if (flag3)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetPlayer.playerCommon.m_nModelIndex).Key_Skill_1;
				}
				bool flag4 = playerSkillData.dokey == "Skill2";
				if (flag4)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetPlayer.playerCommon.m_nModelIndex).Key_Skill_2;
				}
				bool flag5 = playerSkillData.dokey == "WeaponSec";
				if (flag5)
				{
					text = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetPlayer.playerCommon.m_nModelIndex).Key_Use_Weapon_2;
				}
			}
		}
		bool flag6 = text == "Mouse0";
		string result;
		if (flag6)
		{
			result = "zuojian";
		}
		else
		{
			bool flag7 = text == "Mouse1";
			if (flag7)
			{
				result = "youjian";
			}
			else
			{
				result = GameRoot.inst.Setting.Get_Key_Describe(text);
			}
		}
		return result;
	}

	// Token: 0x040003A4 RID: 932
	private Image Q_img_l;

	// Token: 0x040003A5 RID: 933
	private Text tText_l;

	// Token: 0x040003A6 RID: 934
	private Image skillm_key_l;

	// Token: 0x040003A7 RID: 935
	private Image Q_img_r;

	// Token: 0x040003A8 RID: 936
	private Text tText_r;

	// Token: 0x040003A9 RID: 937
	private Image skillm_key_r;

	// Token: 0x040003AA RID: 938
	private NetLiving _NetPlayer;

	// Token: 0x040003AB RID: 939
	private int m_nSkil_1;

	// Token: 0x040003AC RID: 940
	private int m_nSkil_2;

	// Token: 0x040003AD RID: 941
	private int m_nBuff_1;

	// Token: 0x040003AE RID: 942
	private int m_nBuff_2;

	// Token: 0x040003AF RID: 943
	private zhunxingBase zhunxingobj;
}
