using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class WeaponUnlock
{
	// Token: 0x06002DA3 RID: 11683 RVA: 0x001927E4 File Offset: 0x001909E4
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
		this.nGridNum = int.Parse(xml.GetAttribute("gridnum"));
		this.szGridOpen = xml.GetAttribute("gridlock").Split(new char[]
		{
			','
		});
		string[] array = xml.GetAttribute("gridpos").Split(new char[]
		{
			','
		});
		bool flag = array.Length > 2;
		if (flag)
		{
			this.gridPos1st = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		array = xml.GetAttribute("gridrot").Split(new char[]
		{
			','
		});
		bool flag2 = array.Length > 2;
		if (flag2)
		{
			this.gridRot1st = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		array = xml.GetAttribute("gridscale").Split(new char[]
		{
			','
		});
		bool flag3 = array.Length > 2;
		if (flag3)
		{
			this.gridScale1st = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		array = xml.GetAttribute("gridpos3").Split(new char[]
		{
			','
		});
		bool flag4 = array.Length > 2;
		if (flag4)
		{
			this.gridPos3rd = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		array = xml.GetAttribute("gridrot3").Split(new char[]
		{
			','
		});
		bool flag5 = array.Length > 2;
		if (flag5)
		{
			this.gridRot3rd = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		array = xml.GetAttribute("gridscale3").Split(new char[]
		{
			','
		});
		bool flag6 = array.Length > 2;
		if (flag6)
		{
			this.gridScale3rd = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x00192B98 File Offset: 0x00190D98
	public void SetData(ArrayList szData)
	{
		this.nLockState = (int)szData[1];
		this.nPieceNum = (int)szData[2];
		this.overTime = szData[4].ToString();
		this.nNewState = (int)szData[5];
		this.szPartsGrid = (ArrayList)szData[6];
	}

	// Token: 0x06002DA5 RID: 11685 RVA: 0x00192C00 File Offset: 0x00190E00
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

	// Token: 0x06002DA6 RID: 11686 RVA: 0x00192C7C File Offset: 0x00190E7C
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

	// Token: 0x040026B0 RID: 9904
	public int nId;

	// Token: 0x040026B1 RID: 9905
	public int nDefault;

	// Token: 0x040026B2 RID: 9906
	public string sName;

	// Token: 0x040026B3 RID: 9907
	public string sIconName;

	// Token: 0x040026B4 RID: 9908
	public string lowMat_1st;

	// Token: 0x040026B5 RID: 9909
	public string highMat_1st;

	// Token: 0x040026B6 RID: 9910
	public string comMat_3rd;

	// Token: 0x040026B7 RID: 9911
	public int nQual;

	// Token: 0x040026B8 RID: 9912
	public string[] unlockData;

	// Token: 0x040026B9 RID: 9913
	public string[] szBtnData;

	// Token: 0x040026BA RID: 9914
	public string sActDesc;

	// Token: 0x040026BB RID: 9915
	public string sDesc;

	// Token: 0x040026BC RID: 9916
	public int nChipId;

	// Token: 0x040026BD RID: 9917
	public int nIsZhekou;

	// Token: 0x040026BE RID: 9918
	public int nLockState;

	// Token: 0x040026BF RID: 9919
	public int nPieceNum;

	// Token: 0x040026C0 RID: 9920
	public string overTime;

	// Token: 0x040026C1 RID: 9921
	public int nNewState;

	// Token: 0x040026C2 RID: 9922
	public List<Vector3> ShopModelPos = new List<Vector3>();

	// Token: 0x040026C3 RID: 9923
	public string[] szAddAttr;

	// Token: 0x040026C4 RID: 9924
	public string[] szGiftData;

	// Token: 0x040026C5 RID: 9925
	public string sGiftDesc;

	// Token: 0x040026C6 RID: 9926
	public int nGiftNum;

	// Token: 0x040026C7 RID: 9927
	public int nGridNum;

	// Token: 0x040026C8 RID: 9928
	public string[] szGridOpen;

	// Token: 0x040026C9 RID: 9929
	public Vector3 gridPos1st;

	// Token: 0x040026CA RID: 9930
	public Vector3 gridRot1st;

	// Token: 0x040026CB RID: 9931
	public Vector3 gridScale1st;

	// Token: 0x040026CC RID: 9932
	public Vector3 gridPos3rd;

	// Token: 0x040026CD RID: 9933
	public Vector3 gridRot3rd;

	// Token: 0x040026CE RID: 9934
	public Vector3 gridScale3rd;

	// Token: 0x040026CF RID: 9935
	public ArrayList szPartsGrid = new ArrayList();
}
