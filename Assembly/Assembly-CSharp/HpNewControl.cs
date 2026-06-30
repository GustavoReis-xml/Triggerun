using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class HpNewControl : MonoBehaviour
{
	// Token: 0x060015BE RID: 5566 RVA: 0x000CB30C File Offset: 0x000C950C
	private void Awake()
	{
		this.heidi = base.transform.FindChild("heidi").gameObject;
		this.bai = base.transform.FindChild("bai").gameObject;
		this.lan = base.transform.FindChild("lan").gameObject;
		this.huan = base.transform.FindChild("huan").gameObject;
		this.hong = base.transform.FindChild("hong").gameObject;
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000CB3A4 File Offset: 0x000C95A4
	public void SetHp(int _MaxHp, int _nHp, int _nArmor, int _nShield, int nStat)
	{
		this.m_nHp = _nHp;
		this.m_nShield = _nShield;
		this.m_nArmor = _nArmor;
		this.m_nNowHp = _MaxHp;
		Vector3 vector = Vector3.one;
		Vector3 vector2 = Vector3.one;
		Vector3 vector3 = Vector3.one;
		vector = this.getlocalScale(this.m_nHp);
		float hpPos = this.getHpPos();
		bool flag = nStat == 1;
		if (flag)
		{
			bool flag2 = this.vHpOldSca != vector;
			if (flag2)
			{
				this.hong.transform.localScale = vector;
				this.vHpOldSca = vector;
			}
			bool flag3 = this.fhpOldpos != hpPos;
			if (flag3)
			{
				this.hong.transform.localPosition = new Vector3(hpPos, 0f, 0f);
				this.fhpOldpos = hpPos;
			}
		}
		else
		{
			bool flag4 = this.vHpOldSca != vector;
			if (flag4)
			{
				this.bai.transform.localScale = vector;
				this.vHpOldSca = vector;
			}
			bool flag5 = this.fhpOldpos != hpPos;
			if (flag5)
			{
				this.bai.transform.localPosition = new Vector3(hpPos, 0f, 0f);
				this.fhpOldpos = hpPos;
			}
		}
		vector2 = this.getlocalScale(this.m_nShield);
		bool flag6 = vector2 != this.vShieldOldSca;
		if (flag6)
		{
			this.lan.transform.localScale = vector2;
			this.vShieldOldSca = vector2;
		}
		float armorPos = this.getArmorPos();
		bool flag7 = armorPos != this.fshieldOldpos;
		if (flag7)
		{
			this.lan.transform.localPosition = new Vector3(armorPos, 0f, 0f);
			this.fshieldOldpos = armorPos;
		}
		vector3 = this.getlocalScale(this.m_nArmor);
		bool flag8 = vector3 != this.vArmorOldSca;
		if (flag8)
		{
			this.huan.transform.localScale = vector3;
			this.vArmorOldSca = vector3;
		}
		float shieldPos = this.getShieldPos();
		bool flag9 = shieldPos != this.farmorOldpos;
		if (flag9)
		{
			this.huan.transform.localPosition = new Vector3(shieldPos, 0f, 0f);
			this.farmorOldpos = shieldPos;
		}
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000CB5F0 File Offset: 0x000C97F0
	private float getHpPos()
	{
		float scale = this.getScale(this.m_nHp);
		return -75f + scale / 2f;
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000CB624 File Offset: 0x000C9824
	private float getArmorPos()
	{
		float scale = this.getScale(this.m_nShield);
		float num = -75f + scale / 2f;
		return num + this.getScale(this.m_nHp);
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000CB668 File Offset: 0x000C9868
	private float getShieldPos()
	{
		float scale = this.getScale(this.m_nArmor);
		float num = -75f + scale / 2f;
		return num + this.getScale(this.m_nHp) + this.getScale(this.m_nShield);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000CB6B8 File Offset: 0x000C98B8
	private float getScale(int nHp)
	{
		float result = 0f;
		bool flag = nHp > 0;
		if (flag)
		{
			result = (float)nHp / (float)this.m_nNowHp * 150f;
		}
		return result;
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000CB6EC File Offset: 0x000C98EC
	private Vector3 getlocalScale(int nValue)
	{
		Vector3 one = Vector3.one;
		one.x = this.getScale(nValue);
		one.y = 25f;
		return one;
	}

	// Token: 0x040010C9 RID: 4297
	public int m_nNowHp;

	// Token: 0x040010CA RID: 4298
	public int m_nHp;

	// Token: 0x040010CB RID: 4299
	public int m_nShield;

	// Token: 0x040010CC RID: 4300
	public int m_nArmor;

	// Token: 0x040010CD RID: 4301
	private float m_fMaxLong;

	// Token: 0x040010CE RID: 4302
	private GameObject heidi;

	// Token: 0x040010CF RID: 4303
	private GameObject bai;

	// Token: 0x040010D0 RID: 4304
	private GameObject lan;

	// Token: 0x040010D1 RID: 4305
	private GameObject huan;

	// Token: 0x040010D2 RID: 4306
	private GameObject hong;

	// Token: 0x040010D3 RID: 4307
	private float fhpOldpos;

	// Token: 0x040010D4 RID: 4308
	private float fshieldOldpos;

	// Token: 0x040010D5 RID: 4309
	private float farmorOldpos;

	// Token: 0x040010D6 RID: 4310
	private Vector3 vHpOldSca;

	// Token: 0x040010D7 RID: 4311
	private Vector3 vShieldOldSca;

	// Token: 0x040010D8 RID: 4312
	private Vector3 vArmorOldSca;
}
