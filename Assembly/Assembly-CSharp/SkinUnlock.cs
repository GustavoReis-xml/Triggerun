using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class SkinUnlock
{
	// Token: 0x06002A04 RID: 10756 RVA: 0x00171630 File Offset: 0x0016F830
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
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
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
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x00171808 File Offset: 0x0016FA08
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x00171860 File Offset: 0x0016FA60
	public List<string> BtnUnlockData()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.szBtnData.Length; i += 2)
		{
			bool flag = i + 1 < this.szBtnData.Length;
			if (flag)
			{
				string text = this.szBtnData[i + 1];
				list.Add(this.szBtnData[i]);
				list.Add(text);
				list.Add(this.GetUnlockValue(text));
			}
		}
		return list;
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x001718DC File Offset: 0x0016FADC
	public string GetUnlockValue(string stype)
	{
		string result = string.Empty;
		for (int i = 0; i < this.unlockData.Length; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(this.unlockData[i]) && this.unlockData[i] == stype;
			if (flag)
			{
				result = this.unlockData[i + 1];
				break;
			}
		}
		return result;
	}

	// Token: 0x0400235A RID: 9050
	public int nId;

	// Token: 0x0400235B RID: 9051
	public int nDefault;

	// Token: 0x0400235C RID: 9052
	public string sName;

	// Token: 0x0400235D RID: 9053
	public string sIconName;

	// Token: 0x0400235E RID: 9054
	public string tpsModel;

	// Token: 0x0400235F RID: 9055
	public string tpsControl;

	// Token: 0x04002360 RID: 9056
	public string fpsModel;

	// Token: 0x04002361 RID: 9057
	public string fpsControl;

	// Token: 0x04002362 RID: 9058
	public int nQual;

	// Token: 0x04002363 RID: 9059
	public string[] unlockData;

	// Token: 0x04002364 RID: 9060
	public string sBtnData;

	// Token: 0x04002365 RID: 9061
	public string[] szBtnData;

	// Token: 0x04002366 RID: 9062
	public string sActDesc;

	// Token: 0x04002367 RID: 9063
	public string sDesc;

	// Token: 0x04002368 RID: 9064
	public int nChipId;

	// Token: 0x04002369 RID: 9065
	public int nIsZhekou;

	// Token: 0x0400236A RID: 9066
	public int nLockState;

	// Token: 0x0400236B RID: 9067
	public int nPieceNum;

	// Token: 0x0400236C RID: 9068
	public string overTime;

	// Token: 0x0400236D RID: 9069
	public int nNewState;

	// Token: 0x0400236E RID: 9070
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x0400236F RID: 9071
	public string[] szAddAttr;

	// Token: 0x04002370 RID: 9072
	public string[] szGiftData;

	// Token: 0x04002371 RID: 9073
	public string sGiftDesc;

	// Token: 0x04002372 RID: 9074
	public int nGiftNum;
}
