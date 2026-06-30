using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class HpControl : MonoBehaviour
{
	// Token: 0x060015BA RID: 5562 RVA: 0x000CB078 File Offset: 0x000C9278
	private void Awake()
	{
		this.hplist = base.transform.FindChild("hplist").gameObject;
		this.clonehp = base.transform.FindChild("clonehp").gameObject;
		this.clonehp.SetActive(false);
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x000CB0CC File Offset: 0x000C92CC
	public void SetHp(int _MaxHp, int _nHp, int _nArmor, int _nShield, int nStat)
	{
		bool flag = !(this.hplist == null) && !(this.clonehp == null);
		if (flag)
		{
			this.m_nHp = _nHp;
			this.m_nShield = _nShield;
			this.m_nArmor = _nArmor;
			this.m_nNowHp = _MaxHp;
			this.m_nHpStat = nStat;
			this.cell_Num = this.m_nNowHp / this.m_nCellHp;
			this.cell_Weight = 120f / (float)this.cell_Num;
			this.cell_Pos = 160f / (float)this.cell_Num;
			this.cell_gap = 15 - this.cell_Num;
			this.OnCreateCell();
		}
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000CB174 File Offset: 0x000C9374
	private void OnCreateCell()
	{
		bool flag = this.cell_Num == this.hplist.transform.childCount;
		if (!flag)
		{
			Fun.ClearChildObejct(this.hplist);
			for (int i = 0; i < this.cell_Num; i++)
			{
				Transform transform = this.hplist.transform.Find(i.ToString());
				bool flag2 = transform == null;
				GameObject gameObject;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonehp);
					gameObject.transform.SetParent(this.hplist.transform);
					gameObject.transform.localPosition = new Vector3((float)i * this.cell_Pos, 1f, 1f);
					gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, -180f, 0f));
					gameObject.transform.localScale = new Vector3(this.fbl * this.cell_Weight, this.fbl * this.cell_Height, 1f);
					gameObject.SetActive(true);
					gameObject.name = i.ToString();
				}
				else
				{
					gameObject = transform.gameObject;
				}
				HpCellControl hpCellControl = gameObject.GetComponent<HpCellControl>();
				bool flag3 = hpCellControl == null;
				if (flag3)
				{
					hpCellControl = gameObject.AddComponent<HpCellControl>();
					hpCellControl.SetValue(i + 1, this);
				}
			}
		}
	}

	// Token: 0x040010BA RID: 4282
	private GameObject clonehp;

	// Token: 0x040010BB RID: 4283
	private GameObject hplist;

	// Token: 0x040010BC RID: 4284
	private float cell_Pos;

	// Token: 0x040010BD RID: 4285
	public float cell_Weight;

	// Token: 0x040010BE RID: 4286
	public float cell_Height = 20f;

	// Token: 0x040010BF RID: 4287
	public int cell_Num;

	// Token: 0x040010C0 RID: 4288
	public int cell_gap;

	// Token: 0x040010C1 RID: 4289
	public float fbl = 0.0387f;

	// Token: 0x040010C2 RID: 4290
	public int m_nCellHp = 125;

	// Token: 0x040010C3 RID: 4291
	public int m_nNowHp;

	// Token: 0x040010C4 RID: 4292
	public int m_nHp;

	// Token: 0x040010C5 RID: 4293
	public int m_nShield;

	// Token: 0x040010C6 RID: 4294
	public int m_nArmor;

	// Token: 0x040010C7 RID: 4295
	public HeroPlayer heroMe;

	// Token: 0x040010C8 RID: 4296
	public int m_nHpStat;
}
