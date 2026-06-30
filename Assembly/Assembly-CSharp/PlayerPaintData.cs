using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class PlayerPaintData
{
	// Token: 0x06001E25 RID: 7717 RVA: 0x00119C44 File Offset: 0x00117E44
	public void Init(XmlElement xml, int sortid)
	{
		this.nPaintId = int.Parse(xml.GetAttribute("id"));
		this.nDefault = int.Parse(xml.GetAttribute("default"));
		this.sName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("iconname");
		this.animParm = xml.GetAttribute("animparm");
		this.nQual = int.Parse(xml.GetAttribute("qual"));
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
		this.unlockData = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
		this.sActDesc = xml.GetAttribute("actdesc");
		this.sDesc = xml.GetAttribute("desc");
		this.ShopModelPos = ShopData.GetShopPos(xml.GetAttribute("shoppos"));
		this.nChipId = int.Parse(xml.GetAttribute("suipianid"));
		this.nSortId = sortid;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x00119DBC File Offset: 0x00117FBC
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nGiftTime = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x00119E14 File Offset: 0x00118014
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

	// Token: 0x06001E28 RID: 7720 RVA: 0x00119E90 File Offset: 0x00118090
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

	// Token: 0x04001BB6 RID: 7094
	public int nPaintId;

	// Token: 0x04001BB7 RID: 7095
	public int nDefault;

	// Token: 0x04001BB8 RID: 7096
	public string sName;

	// Token: 0x04001BB9 RID: 7097
	public string sIconName;

	// Token: 0x04001BBA RID: 7098
	public string animParm;

	// Token: 0x04001BBB RID: 7099
	public int nQual;

	// Token: 0x04001BBC RID: 7100
	public int nIsZhekou;

	// Token: 0x04001BBD RID: 7101
	public string[] unlockData;

	// Token: 0x04001BBE RID: 7102
	public string[] szBtnData;

	// Token: 0x04001BBF RID: 7103
	public string[] szGiftData;

	// Token: 0x04001BC0 RID: 7104
	public string sGiftDesc;

	// Token: 0x04001BC1 RID: 7105
	public int nGiftNum;

	// Token: 0x04001BC2 RID: 7106
	public string sActDesc;

	// Token: 0x04001BC3 RID: 7107
	public string sDesc;

	// Token: 0x04001BC4 RID: 7108
	public int nChipId;

	// Token: 0x04001BC5 RID: 7109
	public int nSortId;

	// Token: 0x04001BC6 RID: 7110
	public int nLockState;

	// Token: 0x04001BC7 RID: 7111
	public int nGiftTime;

	// Token: 0x04001BC8 RID: 7112
	public string overTime;

	// Token: 0x04001BC9 RID: 7113
	public int nNewState;

	// Token: 0x04001BCA RID: 7114
	public List<Vector3> ShopModelPos = new List<Vector3>();
}
