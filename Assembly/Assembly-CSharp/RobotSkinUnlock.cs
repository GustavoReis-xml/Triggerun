using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class RobotSkinUnlock
{
	// Token: 0x06002194 RID: 8596 RVA: 0x0013831C File Offset: 0x0013651C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("iconname");
		this.tpsModel = xml.GetAttribute("skinname");
		this.tpsControl = xml.GetAttribute("controller");
		this.fpsModel = xml.GetAttribute("skinname1");
		this.fpsControl = xml.GetAttribute("controller1");
		this.nQual = int.Parse(xml.GetAttribute("qual"));
		this.unlockData = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		this.sBtnData = xml.GetAttribute("btndata");
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

	// Token: 0x06002195 RID: 8597 RVA: 0x001384C8 File Offset: 0x001366C8
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x04001EB8 RID: 7864
	public int nId;

	// Token: 0x04001EB9 RID: 7865
	public int nDefault;

	// Token: 0x04001EBA RID: 7866
	public string sName;

	// Token: 0x04001EBB RID: 7867
	public string sIconName;

	// Token: 0x04001EBC RID: 7868
	public string tpsModel;

	// Token: 0x04001EBD RID: 7869
	public string tpsControl;

	// Token: 0x04001EBE RID: 7870
	public string fpsModel;

	// Token: 0x04001EBF RID: 7871
	public string fpsControl;

	// Token: 0x04001EC0 RID: 7872
	public int nQual;

	// Token: 0x04001EC1 RID: 7873
	public string[] unlockData;

	// Token: 0x04001EC2 RID: 7874
	public string sBtnData;

	// Token: 0x04001EC3 RID: 7875
	public string[] szBtnData;

	// Token: 0x04001EC4 RID: 7876
	public string sActDesc;

	// Token: 0x04001EC5 RID: 7877
	public string sDesc;

	// Token: 0x04001EC6 RID: 7878
	public int nChipId;

	// Token: 0x04001EC7 RID: 7879
	public int nLockState;

	// Token: 0x04001EC8 RID: 7880
	public int nPieceNum;

	// Token: 0x04001EC9 RID: 7881
	public string overTime;

	// Token: 0x04001ECA RID: 7882
	public int nNewState;

	// Token: 0x04001ECB RID: 7883
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001ECC RID: 7884
	public string[] szAddAttr;

	// Token: 0x04001ECD RID: 7885
	public string[] szGiftData;

	// Token: 0x04001ECE RID: 7886
	public string sGiftDesc;
}
