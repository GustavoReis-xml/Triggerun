using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class RobotWeaponUnlock
{
	// Token: 0x0600219A RID: 8602 RVA: 0x00138960 File Offset: 0x00136B60
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("iconname");
		this.lowMat_1st = xml.GetAttribute("lowmat_1st");
		this.highMat_1st = xml.GetAttribute("highmat_1st");
		this.comMat_3rd = xml.GetAttribute("commat_3rd");
		this.nQual = int.Parse(xml.GetAttribute("qual"));
		this.unlockData = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.sActDesc = xml.GetAttribute("actdesc");
		this.sDesc = xml.GetAttribute("desc");
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.szAddAttr = xml.GetAttribute("szattr").Split(new char[]
		{
			','
		});
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x00138AE8 File Offset: 0x00136CE8
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = 0;
	}

	// Token: 0x04001EF0 RID: 7920
	public int nId;

	// Token: 0x04001EF1 RID: 7921
	public int nDefault;

	// Token: 0x04001EF2 RID: 7922
	public string sName;

	// Token: 0x04001EF3 RID: 7923
	public string sIconName;

	// Token: 0x04001EF4 RID: 7924
	public string lowMat_1st;

	// Token: 0x04001EF5 RID: 7925
	public string highMat_1st;

	// Token: 0x04001EF6 RID: 7926
	public string comMat_3rd;

	// Token: 0x04001EF7 RID: 7927
	public int nQual;

	// Token: 0x04001EF8 RID: 7928
	public string[] unlockData;

	// Token: 0x04001EF9 RID: 7929
	public string[] szBtnData;

	// Token: 0x04001EFA RID: 7930
	public string sActDesc;

	// Token: 0x04001EFB RID: 7931
	public string sDesc;

	// Token: 0x04001EFC RID: 7932
	public int nChipId;

	// Token: 0x04001EFD RID: 7933
	public int nLockState;

	// Token: 0x04001EFE RID: 7934
	public int nPieceNum;

	// Token: 0x04001EFF RID: 7935
	public string overTime;

	// Token: 0x04001F00 RID: 7936
	public int nNewState;

	// Token: 0x04001F01 RID: 7937
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001F02 RID: 7938
	public string[] szAddAttr;

	// Token: 0x04001F03 RID: 7939
	public string[] szGiftData;

	// Token: 0x04001F04 RID: 7940
	public string sGiftDesc;
}
