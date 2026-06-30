using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class DtsMapItem : NetLiving
{
	// Token: 0x06000932 RID: 2354 RVA: 0x0004E354 File Offset: 0x0004C554
	public void IniDtsMapItem(int nDoorID, int nItemID, int nItemType, int nItemIndex, int xpos, int zpos, int ypos, int nRotation, string sLoadModel, string sName, ArrayList szItemData)
	{
		this.m_nDoorID = nDoorID;
		this.m_nItmeID = nItemID;
		this.m_nItmeType = nItemType;
		this.m_nItmeIndex = nItemIndex;
		this.m_sLoadModel = sLoadModel;
		this.m_fX = (float)xpos / 100f;
		this.m_fY = (float)ypos / 100f;
		this.m_fZ = (float)zpos / 100f;
		this.m_szItemData = szItemData;
		bool flag = nItemType == 6;
		if (flag)
		{
			this.m_sName = Language.GetLang(29003, new object[0]);
		}
		else
		{
			this.m_sName = sName;
		}
		float upPos = this.GetUpPos(new Vector3(this.m_fX, this.m_fY + 3f, this.m_fZ));
		bool flag2 = upPos != 0f;
		if (flag2)
		{
			this.m_fY = upPos;
		}
		this._rot = new Vector3(this.m_fX, this.m_fY, this.m_fZ);
		this.m_nRot = nRotation;
		bool flag3 = this.thisModelParent == null;
		if (flag3)
		{
			this.thisModelParent = new GameObject(base.getNetid().ToString());
			this.thisModelParent.tag = "can_interaction";
			this.thisModelParent.layer = LayerMask.NameToLayer("BuffItem");
			DtsBatteManager.GetInstance().SetParent(this.thisModelParent.transform);
			this.thisModelParent.transform.position = this._rot;
		}
		this.LoadStyle("xuebao_dipan", "dizuo");
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0004E4DC File Offset: 0x0004C6DC
	private float GetUpPos(Vector3 ve)
	{
		float result = 0f;
		Ray ray = new Ray(ve, -Vector3.up);
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(ray, out raycastHit, 100f, layerMask);
		if (flag)
		{
			result = raycastHit.point.y + 0.15f;
		}
		return result;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0004E544 File Offset: 0x0004C744
	public void UpdateItemData(ArrayList szItemData)
	{
		this.m_szItemData = szItemData;
		bool flag = this.m_szItemData.Count <= 0;
		if (!flag)
		{
			int num = (int)this.m_szItemData[0];
			bool flag2 = num == 7;
			if (flag2)
			{
				this.m_nItmeIndex = (int)this.m_szItemData[1];
				int num2 = (int)this.m_szItemData[2];
				int num3 = (int)this.m_szItemData[3];
				DtsMapItemCon component = this.thisModelParent.GetComponent<DtsMapItemCon>();
				bool flag3 = num3 == 0 && num2 == fGameManager.GetInstance().getPlayer().getNetid() && DtsUICon.Inst != null;
				if (flag3)
				{
					DtsUICon.Inst.OpenChangeHeroUI(1);
				}
				DtsUser userObj = DtsBatteManager.GetInstance().GetUserObj(fGameManager.GetInstance().getPlayer().playerCommon.m_nCid);
				bool flag4 = userObj != null && component != null;
				if (flag4)
				{
					bool mapItemStat = userObj.IsItemCan(this.m_nItmeType, this.m_nItmeIndex, 0, this.m_szItemData);
					component.SetMapItemStat(mapItemStat);
				}
			}
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0004E674 File Offset: 0x0004C874
	private void LoadStyle(string sName, string sType)
	{
		string url = (!(sType == "dtsmapitem")) ? string.Format("HeroAnimator/" + sName + ".mo", new object[0]) : ("EffectRes/" + sName + ".ef");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.InitStyle), sType);
		}
		else
		{
			this.InitStyle(resWWW, sType);
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
	public void InitStyle(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null || DtsBatteManager.GetInstance().GetDtsMapItemObj() == null || this.thisModelParent == null;
		if (!flag)
		{
			string text = (string)sStyle;
			bool flag2 = text == "dizuo";
			if (flag2)
			{
				bool flag3 = this._thisDizuo != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(this._thisDizuo);
				}
				this._thisDizuo = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
				this._thisDizuo.transform.SetParent(this.thisModelParent.transform);
				this._thisDizuo.transform.localScale = Vector3.one;
				this._thisDizuo.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
				this._thisDizuo.transform.localPosition = new Vector3(0f, 0.1f, 0f);
				this._thisDizuo.name = text;
				this._thisDizuo.layer = LayerMask.NameToLayer("BuffItem");
				this.LoadStyle("dtsmapitem", "dtsmapitem");
			}
			else
			{
				bool flag4 = this._thisModel != null;
				if (flag4)
				{
					UnityEngine.Object.Destroy(this._thisModel);
				}
				this._thisModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
				this._thisModel.transform.SetParent(this.thisModelParent.transform);
				this._thisModel.transform.localPosition = Vector3.zero;
				this._thisModel.transform.localRotation = Quaternion.identity;
				this._thisModel.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
				this._thisModel.name = text;
				BoxCollider[] componentsInChildren = this._thisModel.GetComponentsInChildren<BoxCollider>();
				int num = componentsInChildren.Length;
				for (int i = 0; i < num; i++)
				{
					BoxCollider boxCollider = componentsInChildren[i];
					bool flag5 = !(boxCollider == null);
					if (flag5)
					{
						boxCollider.gameObject.layer = LayerMask.NameToLayer("BuffItem");
					}
				}
				Rigidbody rigidbody = this.thisModelParent.AddComponent<Rigidbody>();
				rigidbody.useGravity = false;
				DtsItemObjCon dtsItemObjCon = this._thisModel.GetComponent<DtsItemObjCon>();
				bool flag6 = dtsItemObjCon == null;
				if (flag6)
				{
					dtsItemObjCon = this._thisModel.AddComponent<DtsItemObjCon>();
				}
				dtsItemObjCon.setUI(this, new Vector3(0f, 0.6f, 0f));
				DtsMapItemCon dtsMapItemCon = this.thisModelParent.GetComponent<DtsMapItemCon>();
				bool flag7 = dtsMapItemCon == null;
				if (flag7)
				{
					dtsMapItemCon = this.thisModelParent.AddComponent<DtsMapItemCon>();
				}
				dtsMapItemCon.setData(this);
				DtsBatteManager.GetInstance().AddDtsMapItemData(base.getNetid(), this.thisModelParent);
			}
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0004EA22 File Offset: 0x0004CC22
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.OnNetDestroy();
		DtsBatteManager.GetInstance().DelDtsMapItemData(base.getNetid());
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0004EA40 File Offset: 0x0004CC40
	public override void OnNetDestroy()
	{
		this.CheckClosePickUi();
		bool flag = this._thisModel != null;
		if (flag)
		{
			DtsItemObjCon component = this._thisModel.GetComponent<DtsItemObjCon>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.OnClose();
			}
			UnityEngine.Object.Destroy(this._thisModel);
			this._thisModel = null;
		}
		bool flag3 = this._thisDizuo != null;
		if (flag3)
		{
			UnityEngine.Object.Destroy(this._thisDizuo);
			this._thisDizuo = null;
		}
		bool flag4 = this.thisModelParent != null;
		if (flag4)
		{
			UnityEngine.Object.Destroy(this.thisModelParent);
			this.thisModelParent = null;
		}
		base.OnNetDestroy();
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
	private void CheckClosePickUi()
	{
		bool flag = fGameManager.Heroplayer == null;
		if (!flag)
		{
			DtsMapItem playerDtsItemObj = fGameManager.Heroplayer.playerDtsItemObj;
			bool flag2 = playerDtsItemObj != null && playerDtsItemObj.getNetid() == base.getNetid();
			if (flag2)
			{
				bool flag3 = DtsUICon.Inst != null && DtsUICon.Inst.GetIsOpenChangeHero();
				if (flag3)
				{
					DtsUICon.Inst.OpenChangeHeroUI(0);
				}
				fGameManager.Heroplayer.playerDtsItemObj = null;
			}
		}
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0004EB6C File Offset: 0x0004CD6C
	public bool GetIsPickBox(DtsUser dUser)
	{
		bool flag = false;
		ArrayList arrayList = new ArrayList();
		bool flag2 = this.m_nItmeType != 6;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = this.m_szItemData.Count < 1;
			if (flag3)
			{
				result = flag;
			}
			else
			{
				int num = (int)this.m_szItemData[7];
				int num2 = (int)this.m_szItemData[8];
				int num3 = (int)this.m_szItemData[9];
				arrayList = (ArrayList)this.m_szItemData[11];
				int num4 = (int)this.m_szItemData[13];
				bool flag4 = num4 > 0 || num > 0 || (num2 > 0 && dUser.m_nAttackLvl < 10) || (num3 > 0 && dUser.m_nDefendLvl < 10) || arrayList.Count > 0;
				if (flag4)
				{
					flag = true;
				}
				result = flag;
			}
		}
		return result;
	}

	// Token: 0x040006AE RID: 1710
	public int m_nDoorID;

	// Token: 0x040006AF RID: 1711
	public int m_nItmeID;

	// Token: 0x040006B0 RID: 1712
	public int m_nItmeType;

	// Token: 0x040006B1 RID: 1713
	public int m_nItmeIndex;

	// Token: 0x040006B2 RID: 1714
	public string m_sLoadModel;

	// Token: 0x040006B3 RID: 1715
	public string m_sName;

	// Token: 0x040006B4 RID: 1716
	private Vector3 _rot;

	// Token: 0x040006B5 RID: 1717
	public float m_fX;

	// Token: 0x040006B6 RID: 1718
	public float m_fY;

	// Token: 0x040006B7 RID: 1719
	public float m_fZ;

	// Token: 0x040006B8 RID: 1720
	public int m_nRot;

	// Token: 0x040006B9 RID: 1721
	private GameObject _thisDizuo;

	// Token: 0x040006BA RID: 1722
	public GameObject _thisModel;

	// Token: 0x040006BB RID: 1723
	public GameObject thisModelParent;

	// Token: 0x040006BC RID: 1724
	public ArrayList m_szItemData = new ArrayList();
}
