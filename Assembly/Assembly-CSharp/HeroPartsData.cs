using System;
using System.Collections;
using System.Xml;

// Token: 0x020001AD RID: 429
public class HeroPartsData
{
	// Token: 0x060013B7 RID: 5047 RVA: 0x000BA880 File Offset: 0x000B8A80
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.nHeroid = int.Parse(xml.GetAttribute("heroid"));
		this.nIndex = int.Parse(xml.GetAttribute("idx"));
		this.szAttrData = xml.GetAttribute("attrdata").Split(new char[]
		{
			','
		});
		this.szOpenData = xml.GetAttribute("opendata").Split(new char[]
		{
			','
		});
		this.nLvlUpId = int.Parse(xml.GetAttribute("lvlupitem"));
		this.nRemoveId = int.Parse(xml.GetAttribute("removeitem"));
		this.szCostData = xml.GetAttribute("lvlupcost").Split(new char[]
		{
			','
		});
		this.szExpData = xml.GetAttribute("lvlexp").Split(new char[]
		{
			','
		});
		this.szLvlAward = xml.GetAttribute("lvlaward").Split(new char[]
		{
			','
		});
		this.animStr = xml.GetAttribute("animclip");
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000BA9C3 File Offset: 0x000B8BC3
	public void SetData(ArrayList szdata)
	{
		this.nNowLvl = (int)szdata[0];
		this.nNowExp = (int)szdata[1];
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000BA9EC File Offset: 0x000B8BEC
	public int GetAttrType()
	{
		int result = 0;
		for (int i = 0; i < this.szAttrData.Length; i += 2)
		{
			bool flag = int.Parse(this.szAttrData[i]) != 10;
			if (flag)
			{
				result = int.Parse(this.szAttrData[i]);
				break;
			}
		}
		return result;
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x000BAA48 File Offset: 0x000B8C48
	public int GetAttrValue()
	{
		int result = 0;
		for (int i = 0; i < this.szAttrData.Length; i += 2)
		{
			bool flag = int.Parse(this.szAttrData[i]) != 10;
			if (flag)
			{
				result = int.Parse(this.szAttrData[i + 1]);
				break;
			}
		}
		return result;
	}

	// Token: 0x04000F38 RID: 3896
	public int nId;

	// Token: 0x04000F39 RID: 3897
	public string sName;

	// Token: 0x04000F3A RID: 3898
	public int nHeroid;

	// Token: 0x04000F3B RID: 3899
	public int nIndex;

	// Token: 0x04000F3C RID: 3900
	public string[] szAttrData;

	// Token: 0x04000F3D RID: 3901
	public string[] szOpenData;

	// Token: 0x04000F3E RID: 3902
	public int nLvlUpId;

	// Token: 0x04000F3F RID: 3903
	public int nRemoveId;

	// Token: 0x04000F40 RID: 3904
	public string[] szCostData;

	// Token: 0x04000F41 RID: 3905
	public string[] szExpData;

	// Token: 0x04000F42 RID: 3906
	public string[] szLvlAward;

	// Token: 0x04000F43 RID: 3907
	public int nNowLvl;

	// Token: 0x04000F44 RID: 3908
	public int nNowExp;

	// Token: 0x04000F45 RID: 3909
	public string animStr;
}
