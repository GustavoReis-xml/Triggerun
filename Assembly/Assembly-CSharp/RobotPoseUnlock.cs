using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class RobotPoseUnlock
{
	// Token: 0x06002191 RID: 8593 RVA: 0x00138168 File Offset: 0x00136368
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("iconname");
		this.animParm = xml.GetAttribute("animparm");
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
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x001382B0 File Offset: 0x001364B0
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x04001EA6 RID: 7846
	public int nId;

	// Token: 0x04001EA7 RID: 7847
	public int nDefault;

	// Token: 0x04001EA8 RID: 7848
	public string sName;

	// Token: 0x04001EA9 RID: 7849
	public string sIconName;

	// Token: 0x04001EAA RID: 7850
	public string animParm;

	// Token: 0x04001EAB RID: 7851
	public int nQual;

	// Token: 0x04001EAC RID: 7852
	public string[] unlockData;

	// Token: 0x04001EAD RID: 7853
	public string[] szBtnData;

	// Token: 0x04001EAE RID: 7854
	public string sActDesc;

	// Token: 0x04001EAF RID: 7855
	public string sDesc;

	// Token: 0x04001EB0 RID: 7856
	public int nChipId;

	// Token: 0x04001EB1 RID: 7857
	public int nLockState;

	// Token: 0x04001EB2 RID: 7858
	public int nPieceNum;

	// Token: 0x04001EB3 RID: 7859
	public string overTime;

	// Token: 0x04001EB4 RID: 7860
	public int nNewState;

	// Token: 0x04001EB5 RID: 7861
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001EB6 RID: 7862
	public string[] szGiftData;

	// Token: 0x04001EB7 RID: 7863
	public string sGiftDesc;
}
