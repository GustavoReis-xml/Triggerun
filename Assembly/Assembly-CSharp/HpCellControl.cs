using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class HpCellControl : MonoBehaviour
{
	// Token: 0x060015AC RID: 5548 RVA: 0x000CA7DC File Offset: 0x000C89DC
	public void SetValue(int nNum, HpControl _hpcon)
	{
		this.oBai = base.transform.Find("bai").gameObject;
		this.oBai.transform.localPosition = new Vector3(0f, 0f, -2f);
		this.oBai.transform.localScale = new Vector3(0f, 31f, 0f);
		this.oLan = base.transform.Find("lan").gameObject;
		this.oLan.transform.localPosition = new Vector3(0f, 0f, -2f);
		this.oLan.transform.localScale = new Vector3(0f, 31f, 0f);
		this.oHuan = base.transform.Find("huan").gameObject;
		this.oHuan.transform.localPosition = new Vector3(0f, 0f, -2f);
		this.oHuan.transform.localScale = new Vector3(0f, 31f, 0f);
		this.oHong = base.transform.Find("hong").gameObject;
		this.oHong.transform.localPosition = new Vector3(0f, 0f, -2f);
		this.oHong.transform.localScale = new Vector3(0f, 31f, 0f);
		this.oEffect = base.transform.Find("ui_diaoxue").gameObject;
		this.oEffect.SetActive(false);
		this.hpcon = _hpcon;
		this.m_nNow = (nNum - 1) * _hpcon.m_nCellHp;
		this.m_nMax = nNum * _hpcon.m_nCellHp;
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x000CA9CC File Offset: 0x000C8BCC
	private void Update()
	{
		this.CheckHp();
		this.CheckArmor();
		this.CheckShield();
		int num = this.GetHp() + this.GetArmor() + this.GetShield();
		bool flag = this.nOldNum != num;
		if (flag)
		{
			bool flag2 = num < 1;
			if (flag2)
			{
				this.oEffect.SetActive(false);
				this.oEffect.SetActive(true);
			}
			this.nOldNum = num;
		}
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x000CAA44 File Offset: 0x000C8C44
	private void DelayTime()
	{
		bool activeSelf = this.oEffect.activeSelf;
		if (activeSelf)
		{
			this.oEffect.SetActive(false);
		}
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x000CAA70 File Offset: 0x000C8C70
	private void CheckHp()
	{
		int hp = this.GetHp();
		bool flag = hp != this.nHp;
		if (flag)
		{
			this.nHp = hp;
		}
		float hpPos = this.getHpPos();
		bool flag2 = this.fhp_bai_pos != hpPos && this.hpcon.m_nHpStat == 0;
		if (flag2)
		{
			this.oBai.transform.localPosition = new Vector3(hpPos, 0f, -2f);
			this.fhp_bai_pos = hpPos;
		}
		bool flag3 = this.fhp_Hong_pos != hpPos && this.hpcon.m_nHpStat == 1;
		if (flag3)
		{
			this.oHong.transform.localPosition = new Vector3(hpPos, 0f, -2f);
			this.fhp_Hong_pos = hpPos;
		}
		float size = this.getSize(this.nHp);
		bool flag4 = this.fhp_bai_Scale != size && this.hpcon.m_nHpStat == 0;
		if (flag4)
		{
			this.oBai.transform.localScale = new Vector3(size, 31f, 0f);
			this.fhp_bai_Scale = size;
		}
		bool flag5 = this.fhp_Hong_Scale != size && this.hpcon.m_nHpStat == 1;
		if (flag5)
		{
			this.oHong.transform.localScale = new Vector3(size, 31f, 0f);
			this.fhp_Hong_Scale = size;
		}
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000CABE8 File Offset: 0x000C8DE8
	private int GetHp()
	{
		int num = 0;
		bool flag = this.hpcon == null;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = this.hpcon.m_nHp >= this.m_nMax;
			if (flag2)
			{
				num = this.hpcon.m_nCellHp;
			}
			else
			{
				bool flag3 = this.hpcon.m_nHp > this.m_nNow && this.hpcon.m_nHp < this.m_nMax;
				if (flag3)
				{
					num = this.hpcon.m_nHp - this.m_nNow;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000CAC80 File Offset: 0x000C8E80
	private float getHpPos()
	{
		return 15.62f - 0.124f * (float)this.nHp;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x000CACAC File Offset: 0x000C8EAC
	private void CheckArmor()
	{
		int armor = this.GetArmor();
		bool flag = armor != this.nArmor;
		if (flag)
		{
			this.nArmor = armor;
		}
		float armorPos = this.getArmorPos();
		bool flag2 = this.fArmor_Huan_pos != armorPos;
		if (flag2)
		{
			this.oHuan.transform.localPosition = new Vector3(armorPos, 0f, -2f);
			this.fArmor_Huan_pos = armorPos;
		}
		float size = this.getSize(this.nArmor);
		bool flag3 = this.fArmor_Huan_Scale != size;
		if (flag3)
		{
			this.oHuan.transform.localScale = new Vector3(size, 31f, 0f);
			this.fArmor_Huan_Scale = size;
		}
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000CAD74 File Offset: 0x000C8F74
	private int GetArmor()
	{
		int num = 0;
		bool flag = this.hpcon == null;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			int hp = this.GetHp();
			int num2 = this.hpcon.m_nHp + this.hpcon.m_nArmor;
			bool flag2 = num2 >= this.m_nMax && hp < this.hpcon.m_nCellHp;
			if (flag2)
			{
				num = this.m_nMax - this.m_nNow - hp;
			}
			else
			{
				bool flag3 = num2 < this.m_nMax && num2 > this.m_nNow;
				if (flag3)
				{
					num = num2 - this.m_nNow - hp;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000CAE24 File Offset: 0x000C9024
	private float getArmorPos()
	{
		return 15.62f - 0.124f * (float)this.nArmor - this.getSize(this.nHp);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000CAE5C File Offset: 0x000C905C
	private void CheckShield()
	{
		int shield = this.GetShield();
		bool flag = shield != this.nShield;
		if (flag)
		{
			this.nShield = shield;
		}
		float shieldPos = this.getShieldPos();
		bool flag2 = this.fShield_Lan_pos != shieldPos;
		if (flag2)
		{
			this.oLan.transform.localPosition = new Vector3(shieldPos, 0f, -2f);
			this.fShield_Lan_pos = shieldPos;
		}
		float size = this.getSize(this.nShield);
		bool flag3 = this.fShield_Lan_Scale != size;
		if (flag3)
		{
			this.oLan.transform.localScale = new Vector3(size, 31f, 0f);
			this.fShield_Lan_Scale = size;
		}
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000CAF24 File Offset: 0x000C9124
	private int GetShield()
	{
		int num = 0;
		bool flag = this.hpcon == null;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			int hp = this.GetHp();
			int armor = this.GetArmor();
			int num2 = this.hpcon.m_nHp + this.hpcon.m_nArmor + this.hpcon.m_nShield;
			bool flag2 = num2 >= this.m_nMax && hp + armor < this.hpcon.m_nCellHp;
			if (flag2)
			{
				num = this.m_nMax - this.m_nNow - hp - armor;
			}
			else
			{
				bool flag3 = num2 < this.m_nMax && num2 > this.m_nNow;
				if (flag3)
				{
					num = num2 - this.m_nNow - hp - armor;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000CAFF0 File Offset: 0x000C91F0
	private float getShieldPos()
	{
		return 15.62f - 0.124f * (float)this.nShield - this.getSize(this.nHp) - this.getSize(this.nArmor);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000CB038 File Offset: 0x000C9238
	private float getSize(int nValue)
	{
		return 0.248f * (float)nValue;
	}

	// Token: 0x040010A6 RID: 4262
	private int nHp = -1;

	// Token: 0x040010A7 RID: 4263
	private int nArmor = -1;

	// Token: 0x040010A8 RID: 4264
	private int nShield = -1;

	// Token: 0x040010A9 RID: 4265
	private HpControl hpcon;

	// Token: 0x040010AA RID: 4266
	private int m_nMax;

	// Token: 0x040010AB RID: 4267
	private int m_nNow;

	// Token: 0x040010AC RID: 4268
	private GameObject oBai;

	// Token: 0x040010AD RID: 4269
	private GameObject oLan;

	// Token: 0x040010AE RID: 4270
	private GameObject oHuan;

	// Token: 0x040010AF RID: 4271
	private GameObject oHong;

	// Token: 0x040010B0 RID: 4272
	private GameObject oEffect;

	// Token: 0x040010B1 RID: 4273
	private int nOldNum;

	// Token: 0x040010B2 RID: 4274
	private float fhp_bai_pos;

	// Token: 0x040010B3 RID: 4275
	private float fhp_Hong_pos;

	// Token: 0x040010B4 RID: 4276
	private float fhp_bai_Scale;

	// Token: 0x040010B5 RID: 4277
	private float fhp_Hong_Scale;

	// Token: 0x040010B6 RID: 4278
	private float fArmor_Huan_pos;

	// Token: 0x040010B7 RID: 4279
	private float fArmor_Huan_Scale;

	// Token: 0x040010B8 RID: 4280
	private float fShield_Lan_pos;

	// Token: 0x040010B9 RID: 4281
	private float fShield_Lan_Scale;
}
