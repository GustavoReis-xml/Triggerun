using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class PoseUnlock
{
	// Token: 0x06001E5D RID: 7773 RVA: 0x0011AB5C File Offset: 0x00118D5C
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
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
		this.sDesc = xml.GetAttribute("desc");
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x0011ACD0 File Offset: 0x00118ED0
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x0011AD28 File Offset: 0x00118F28
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

	// Token: 0x06001E60 RID: 7776 RVA: 0x0011ADA4 File Offset: 0x00118FA4
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

	// Token: 0x04001C0B RID: 7179
	public int nId;

	// Token: 0x04001C0C RID: 7180
	public int nDefault;

	// Token: 0x04001C0D RID: 7181
	public string sName;

	// Token: 0x04001C0E RID: 7182
	public string sIconName;

	// Token: 0x04001C0F RID: 7183
	public string animParm;

	// Token: 0x04001C10 RID: 7184
	public int nQual;

	// Token: 0x04001C11 RID: 7185
	public string[] unlockData;

	// Token: 0x04001C12 RID: 7186
	public string[] szBtnData;

	// Token: 0x04001C13 RID: 7187
	public string sActDesc;

	// Token: 0x04001C14 RID: 7188
	public string sDesc;

	// Token: 0x04001C15 RID: 7189
	public int nChipId;

	// Token: 0x04001C16 RID: 7190
	public int nIsZhekou;

	// Token: 0x04001C17 RID: 7191
	public int nLockState;

	// Token: 0x04001C18 RID: 7192
	public int nPieceNum;

	// Token: 0x04001C19 RID: 7193
	public string overTime;

	// Token: 0x04001C1A RID: 7194
	public int nNewState;

	// Token: 0x04001C1B RID: 7195
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x04001C1C RID: 7196
	public string[] szGiftData;

	// Token: 0x04001C1D RID: 7197
	public string sGiftDesc;

	// Token: 0x04001C1E RID: 7198
	public int nGiftNum;
}
