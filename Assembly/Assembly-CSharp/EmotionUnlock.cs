using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class EmotionUnlock
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x0005C5D4 File Offset: 0x0005A7D4
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
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
		this.sDesc = xml.GetAttribute("desc");
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.sSound = xml.GetAttribute("ssound");
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0005C768 File Offset: 0x0005A968
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0005C7C0 File Offset: 0x0005A9C0
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

	// Token: 0x06000A3F RID: 2623 RVA: 0x0005C83C File Offset: 0x0005AA3C
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

	// Token: 0x04000831 RID: 2097
	public int nId;

	// Token: 0x04000832 RID: 2098
	public int nDefault;

	// Token: 0x04000833 RID: 2099
	public string sName;

	// Token: 0x04000834 RID: 2100
	public string sIconName;

	// Token: 0x04000835 RID: 2101
	public string animParm;

	// Token: 0x04000836 RID: 2102
	public int nQual;

	// Token: 0x04000837 RID: 2103
	public string[] unlockData;

	// Token: 0x04000838 RID: 2104
	public string sBtnData;

	// Token: 0x04000839 RID: 2105
	public string[] szBtnData;

	// Token: 0x0400083A RID: 2106
	public string sActDesc;

	// Token: 0x0400083B RID: 2107
	public string sDesc;

	// Token: 0x0400083C RID: 2108
	public int nChipId;

	// Token: 0x0400083D RID: 2109
	public string sSound;

	// Token: 0x0400083E RID: 2110
	public int nIsZhekou;

	// Token: 0x0400083F RID: 2111
	public int nLockState;

	// Token: 0x04000840 RID: 2112
	public int nPieceNum;

	// Token: 0x04000841 RID: 2113
	public string overTime;

	// Token: 0x04000842 RID: 2114
	public int nNewState;

	// Token: 0x04000843 RID: 2115
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04000844 RID: 2116
	public string[] szGiftData;

	// Token: 0x04000845 RID: 2117
	public string sGiftDesc;

	// Token: 0x04000846 RID: 2118
	public int nGiftNum;
}
