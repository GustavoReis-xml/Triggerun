using System;
using System.Collections;
using System.Xml;

// Token: 0x02000201 RID: 513
public class ItemData
{
	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06001660 RID: 5728 RVA: 0x000CF830 File Offset: 0x000CDA30
	public int key
	{
		get
		{
			return this.m_nItemId;
		}
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x000CF838 File Offset: 0x000CDA38
	public void Init(XmlElement xml)
	{
		this.m_nItemId = int.Parse(xml.GetAttribute("id"));
		this.m_sName = xml.GetAttribute("name");
		this.m_nType = int.Parse(xml.GetAttribute("type"));
		this.m_sIco = xml.GetAttribute("ico");
		this.m_sdesc = xml.GetAttribute("desc");
		this.m_nGlodPrice = int.Parse(xml.GetAttribute("glodprice"));
		this.m_nDiamondPrice = int.Parse(xml.GetAttribute("diamondprice"));
		this.m_nBufftype = int.Parse(xml.GetAttribute("bufftype"));
		this.m_nBuffBeiShu = int.Parse(xml.GetAttribute("buffbeishu"));
		this.m_nBuffTime = int.Parse(xml.GetAttribute("bufftime"));
		this.m_szUseGive = ItemData.GetArrlist(xml.GetAttribute("usegive"), '|', true, ',');
		this.m_nQuality = int.Parse(xml.GetAttribute("quality"));
		this.m_nHeroId = int.Parse(xml.GetAttribute("heroid"));
		this.m_nTypeId = int.Parse((!(xml.GetAttribute("typeid") == string.Empty)) ? xml.GetAttribute("typeid") : "0");
		this.m_sLiBaoDesc = xml.GetAttribute("desc1");
		this.m_sLiBaoDesc = this.m_sLiBaoDesc.Replace("\\n", "\n");
		this.m_sLiBaoIco = xml.GetAttribute("ico1");
		this.m_sTipDesc = xml.GetAttribute("tipdesc");
		this.m_sTipDesc = this.m_sTipDesc.Replace("\\n", "\n");
		this.m_szChildItem = ItemData.GetArrlist(xml.GetAttribute("manyitem"), '#', false, ',');
		this.m_nShopId = int.Parse(xml.GetAttribute("shopid"));
		this.m_sPveico = xml.GetAttribute("pveico");
		this.nHuiGou = int.Parse(xml.GetAttribute("huigouprice"));
		this.nZhekou = int.Parse(xml.GetAttribute("zhekou"));
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000CFA68 File Offset: 0x000CDC68
	public static ArrayList GetArrlist(string items, char sc, bool isInt = false, char sc1 = ',')
	{
		ArrayList arrayList = new ArrayList();
		bool flag = items == string.Empty || items == "0";
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			string[] array = items.Split(new char[]
			{
				sc
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (isInt)
				{
					arrayList.Add(int.Parse(array[i]));
				}
				else
				{
					arrayList.Add(ItemData.GetArrlistShop(array[i], sc1));
				}
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000CFB04 File Offset: 0x000CDD04
	public static ArrayList GetArrlistShop(string items, char sc)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = items == string.Empty || items == "0";
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			string[] array = items.Split(new char[]
			{
				sc
			});
			for (int i = 0; i < array.Length; i++)
			{
				arrayList.Add(int.Parse(array[i]));
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x04001156 RID: 4438
	public int m_nItemId;

	// Token: 0x04001157 RID: 4439
	public string m_sName;

	// Token: 0x04001158 RID: 4440
	public int m_nType;

	// Token: 0x04001159 RID: 4441
	public string m_sIco;

	// Token: 0x0400115A RID: 4442
	public string m_sdesc;

	// Token: 0x0400115B RID: 4443
	public int m_nGlodPrice;

	// Token: 0x0400115C RID: 4444
	public int m_nDiamondPrice;

	// Token: 0x0400115D RID: 4445
	public int m_nBufftype;

	// Token: 0x0400115E RID: 4446
	public int m_nBuffBeiShu;

	// Token: 0x0400115F RID: 4447
	public int m_nBuffTime;

	// Token: 0x04001160 RID: 4448
	public ArrayList m_szUseGive;

	// Token: 0x04001161 RID: 4449
	public int m_nQuality;

	// Token: 0x04001162 RID: 4450
	public int m_nHeroId;

	// Token: 0x04001163 RID: 4451
	public int m_nTypeId;

	// Token: 0x04001164 RID: 4452
	public string m_sLiBaoDesc = string.Empty;

	// Token: 0x04001165 RID: 4453
	public string m_sLiBaoIco = "zishi";

	// Token: 0x04001166 RID: 4454
	public string m_sTipDesc = string.Empty;

	// Token: 0x04001167 RID: 4455
	public ArrayList m_szChildItem;

	// Token: 0x04001168 RID: 4456
	public int m_nShopId = 1002;

	// Token: 0x04001169 RID: 4457
	public string m_sPveico;

	// Token: 0x0400116A RID: 4458
	public int nHuiGou;

	// Token: 0x0400116B RID: 4459
	public int nZhekou;
}
