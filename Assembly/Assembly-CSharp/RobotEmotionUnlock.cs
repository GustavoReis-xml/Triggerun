using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class RobotEmotionUnlock
{
	// Token: 0x0600218E RID: 8590 RVA: 0x00137F94 File Offset: 0x00136194
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
		this.sBtnData = xml.GetAttribute("btndata");
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.sActDesc = xml.GetAttribute("actdesc");
		this.sDesc = xml.GetAttribute("desc");
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.sSound = xml.GetAttribute("ssound");
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x001380FC File Offset: 0x001362FC
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x04001E92 RID: 7826
	public int nId;

	// Token: 0x04001E93 RID: 7827
	public int nDefault;

	// Token: 0x04001E94 RID: 7828
	public string sName;

	// Token: 0x04001E95 RID: 7829
	public string sIconName;

	// Token: 0x04001E96 RID: 7830
	public string animParm;

	// Token: 0x04001E97 RID: 7831
	public int nQual;

	// Token: 0x04001E98 RID: 7832
	public string[] unlockData;

	// Token: 0x04001E99 RID: 7833
	public string sBtnData;

	// Token: 0x04001E9A RID: 7834
	public string[] szBtnData;

	// Token: 0x04001E9B RID: 7835
	public string sActDesc;

	// Token: 0x04001E9C RID: 7836
	public string sDesc;

	// Token: 0x04001E9D RID: 7837
	public int nChipId;

	// Token: 0x04001E9E RID: 7838
	public string sSound;

	// Token: 0x04001E9F RID: 7839
	public int nLockState;

	// Token: 0x04001EA0 RID: 7840
	public int nPieceNum;

	// Token: 0x04001EA1 RID: 7841
	public string overTime;

	// Token: 0x04001EA2 RID: 7842
	public int nNewState;

	// Token: 0x04001EA3 RID: 7843
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001EA4 RID: 7844
	public string[] szGiftData;

	// Token: 0x04001EA5 RID: 7845
	public string sGiftDesc;
}
