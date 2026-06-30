using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class DtsMapItemCon : MonoBehaviour
{
	// Token: 0x0600093C RID: 2364 RVA: 0x0004EC70 File Offset: 0x0004CE70
	private void Awake()
	{
		this.oDiZuo = base.transform.FindChild("dizuo").gameObject;
		this.oDiPan = this.oDiZuo.transform.Find("dipan").gameObject;
		this.oDiPan.SetActive(true);
		this.oDiPan_hon = this.oDiZuo.transform.Find("dipan_hon").gameObject;
		this.oDiPan_hon.SetActive(false);
		this.oDtsmapitem = base.transform.FindChild("dtsmapitem").gameObject;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0004ED10 File Offset: 0x0004CF10
	public void setData(DtsMapItem _item)
	{
		this.thisItem = _item;
		bool flag = this.oDtsmapitem != null;
		if (flag)
		{
			this.dtsItemCon = this.oDtsmapitem.GetComponent<DtsItemObjCon>();
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0004ED48 File Offset: 0x0004CF48
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = !collision.transform.tag.Contains("can_damage_") || this.thisItem == null || this.oDiZuo == null || this.oDtsmapitem == null || collision != fGameManager.Heroplayer.defaultCollider || fGameManager.Heroplayer == null || fGameManager.Heroplayer.isDead || DtsBatteManager.GetInstance().m_nGameState < 2;
		if (!flag)
		{
			DtsUser userObj = DtsBatteManager.GetInstance().GetUserObj(fGameManager.Heroplayer.playerCommon.m_nCid);
			bool flag2 = userObj == null || !userObj.IsItemCan(this.thisItem.m_nItmeType, this.thisItem.m_nItmeIndex, 1, this.thisItem.m_szItemData);
			if (!flag2)
			{
				bool flag3 = this.thisItem.m_nItmeType == 1;
				if (flag3)
				{
					bool flag4 = userObj.m_nNowHeroID == 2000;
					if (flag4)
					{
						DtsBatteManager.GetInstance().OnCheckItem(1, this.thisItem.getNetid());
					}
					else
					{
						bool flag5 = fGameManager.Heroplayer.playerDtsItemObj == null;
						if (flag5)
						{
							fGameManager.Heroplayer.playerDtsItemObj = this.thisItem;
							bool flag6 = DtsUICon.Inst != null;
							if (flag6)
							{
								DtsUICon.Inst.OpenChangeHeroUI(1);
							}
						}
					}
				}
				else
				{
					bool flag7 = this.thisItem.m_nItmeType == 6;
					if (flag7)
					{
						bool flag8 = fGameManager.Heroplayer.playerDtsItemObj == null;
						if (flag8)
						{
							bool isPickBox = this.thisItem.GetIsPickBox(userObj);
							if (isPickBox)
							{
								DtsBatteManager.GetInstance().OnCheckItem(1, this.thisItem.getNetid());
							}
							fGameManager.Heroplayer.playerDtsItemObj = this.thisItem;
							bool flag9 = DtsUICon.Inst != null;
							if (flag9)
							{
								DtsUICon.Inst.OpenChangeHeroUI(1);
							}
						}
					}
					else
					{
						bool flag10 = this.thisItem.m_nItmeType == 7;
						if (flag10)
						{
							bool flag11 = this.thisItem.m_nItmeIndex == 0 && fGameManager.Heroplayer.playerDtsItemObj == null;
							if (flag11)
							{
								fGameManager.Heroplayer.playerDtsItemObj = this.thisItem;
								DtsBatteManager.GetInstance().OnCheckItem(1, this.thisItem.getNetid());
							}
						}
						else
						{
							DtsBatteManager.GetInstance().OnCheckItem(1, this.thisItem.getNetid());
						}
					}
				}
			}
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0004EFB4 File Offset: 0x0004D1B4
	public void SetMapItemStat(bool mbool)
	{
		bool flag = this.oDiZuo == null;
		if (!flag)
		{
			bool flag2 = !mbool;
			if (flag2)
			{
				bool activeSelf = this.oDiPan.activeSelf;
				if (activeSelf)
				{
					this.oDiPan.SetActive(false);
				}
				bool flag3 = !this.oDiPan_hon.activeSelf;
				if (flag3)
				{
					this.oDiPan_hon.SetActive(true);
				}
			}
			else
			{
				bool flag4 = !this.oDiPan.activeSelf;
				if (flag4)
				{
					this.oDiPan.SetActive(true);
				}
				bool activeSelf2 = this.oDiPan_hon.activeSelf;
				if (activeSelf2)
				{
					this.oDiPan_hon.SetActive(false);
				}
			}
			bool flag5 = this.dtsItemCon != null;
			if (flag5)
			{
				this.dtsItemCon.SetState(!mbool);
			}
		}
	}

	// Token: 0x040006BD RID: 1725
	private GameObject oDiZuo;

	// Token: 0x040006BE RID: 1726
	private GameObject oDiPan;

	// Token: 0x040006BF RID: 1727
	private GameObject oDiPan_hon;

	// Token: 0x040006C0 RID: 1728
	private GameObject oDtsmapitem;

	// Token: 0x040006C1 RID: 1729
	private DtsItemObjCon dtsItemCon;

	// Token: 0x040006C2 RID: 1730
	public DtsMapItem thisItem;
}
