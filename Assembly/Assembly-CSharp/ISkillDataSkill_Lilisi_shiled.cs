using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class ISkillDataSkill_Lilisi_shiled : ISkillData
{
	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06001654 RID: 5716 RVA: 0x000CF658 File Offset: 0x000CD858
	// (set) Token: 0x06001655 RID: 5717 RVA: 0x000CF670 File Offset: 0x000CD870
	public bool bCanDoSkill
	{
		get
		{
			return this._bCanDoSkill;
		}
		set
		{
			this._bCanDoSkill = value;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06001656 RID: 5718 RVA: 0x000CF67C File Offset: 0x000CD87C
	// (set) Token: 0x06001657 RID: 5719 RVA: 0x000CF694 File Offset: 0x000CD894
	public Vector3 veCreartPos
	{
		get
		{
			return this._veCreartPos;
		}
		set
		{
			this._veCreartPos = value;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06001658 RID: 5720 RVA: 0x000CF6A0 File Offset: 0x000CD8A0
	// (set) Token: 0x06001659 RID: 5721 RVA: 0x000CF6B8 File Offset: 0x000CD8B8
	public Vector3 Quapos
	{
		get
		{
			return this._Quapos;
		}
		set
		{
			this._Quapos = value;
		}
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000CF6C4 File Offset: 0x000CD8C4
	public Dictionary<string, GameObject> GetAllShiedObj()
	{
		return this.m_dShiledObj;
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000CF6DC File Offset: 0x000CD8DC
	public void AddShiledObj(int nID, GameObject eff)
	{
		string key = nID.ToString() + "_dun";
		bool flag = !this.m_dShiledObj.ContainsKey(key);
		if (flag)
		{
			this.m_dShiledObj.Add(key, eff);
		}
		else
		{
			this.m_dShiledObj[key] = eff;
		}
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000CF734 File Offset: 0x000CD934
	public GameObject FindShiledObj(string sID)
	{
		GameObject gameObject = null;
		bool flag = !this.m_dShiledObj.ContainsKey(sID);
		GameObject result;
		if (flag)
		{
			result = gameObject;
		}
		else
		{
			result = this.m_dShiledObj[sID];
		}
		return result;
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000CF76C File Offset: 0x000CD96C
	public void DestroyFlyShiled(string sID)
	{
		GameObject gameObject = this.FindShiledObj(sID);
		bool flag = gameObject == null;
		if (!flag)
		{
			FlyShiledSkill component = gameObject.GetComponent<FlyShiledSkill>();
			bool flag2 = !(component == null);
			if (flag2)
			{
				component.OnInit();
				gameObject.SetActive(false);
				bool flag3 = !this.DestroyObjList.Contains(gameObject);
				if (flag3)
				{
					this.DestroyObjList.Add(gameObject);
				}
				this.m_dShiledObj.Remove(sID);
			}
		}
	}

	// Token: 0x0400114C RID: 4428
	public Dictionary<string, GameObject> m_dShiledObj = new Dictionary<string, GameObject>();

	// Token: 0x0400114D RID: 4429
	public List<GameObject> DestroyObjList = new List<GameObject>();

	// Token: 0x0400114E RID: 4430
	private bool _bCanDoSkill;

	// Token: 0x0400114F RID: 4431
	private Vector3 _veCreartPos = Vector3.zero;

	// Token: 0x04001150 RID: 4432
	private Vector3 _Quapos = Vector3.zero;
}
