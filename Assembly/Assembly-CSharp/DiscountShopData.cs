using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000F9 RID: 249
public class DiscountShopData
{
	// Token: 0x060008D8 RID: 2264 RVA: 0x0004B398 File Offset: 0x00049598
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sDesc = xml.GetAttribute("desc").Replace("\\n", "\n");
		this.sBtnTxt = xml.GetAttribute("btntxt");
		this.szTitleDesc = xml.GetAttribute("titledesc").Split(new char[]
		{
			'|'
		});
		string[] array = xml.GetAttribute("listdesc").Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				this.szDiscountInfo.Add(int.Parse(array[i]));
			}
		}
		array = xml.GetAttribute("shopid").Split(new char[]
		{
			'|'
		});
		for (int j = 0; j < array.Length; j++)
		{
			bool flag2 = !string.IsNullOrEmpty(array[j]);
			if (flag2)
			{
				this.szShopId.Add(int.Parse(array[j]));
			}
		}
	}

	// Token: 0x0400066C RID: 1644
	public int nId;

	// Token: 0x0400066D RID: 1645
	public string sActName;

	// Token: 0x0400066E RID: 1646
	public string sIconName;

	// Token: 0x0400066F RID: 1647
	public string sDesc;

	// Token: 0x04000670 RID: 1648
	public string sBtnTxt;

	// Token: 0x04000671 RID: 1649
	public string[] szTitleDesc;

	// Token: 0x04000672 RID: 1650
	public List<int> szDiscountInfo = new List<int>();

	// Token: 0x04000673 RID: 1651
	public List<int> szShopId = new List<int>();
}
