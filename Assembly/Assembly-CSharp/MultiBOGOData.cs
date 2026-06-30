using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200024B RID: 587
public class MultiBOGOData
{
	// Token: 0x06001A85 RID: 6789 RVA: 0x000FC93C File Offset: 0x000FAB3C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		string[] array = xml.GetAttribute("buyitem").Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				this.szBuyItem.Add(int.Parse(array[i]));
			}
		}
		this.nGiftItem = int.Parse(xml.GetAttribute("giftitem"));
		this.szRate = xml.GetAttribute("rate").Split(new char[]
		{
			':'
		});
		this.nMaxNum = int.Parse(xml.GetAttribute("maxnum"));
		this.sTitle1 = xml.GetAttribute("stitle1");
		this.sTitle2 = xml.GetAttribute("stitle2");
		this.sDesc1 = xml.GetAttribute("desc1");
		this.sDesc2 = xml.GetAttribute("desc2");
		this.sBottom1 = xml.GetAttribute("sbottom1");
		this.sBottom2 = xml.GetAttribute("sbottom2");
	}

	// Token: 0x04001539 RID: 5433
	public int nId;

	// Token: 0x0400153A RID: 5434
	public string sActName;

	// Token: 0x0400153B RID: 5435
	public string sIconName;

	// Token: 0x0400153C RID: 5436
	public List<int> szBuyItem = new List<int>();

	// Token: 0x0400153D RID: 5437
	public int nGiftItem;

	// Token: 0x0400153E RID: 5438
	public string[] szRate;

	// Token: 0x0400153F RID: 5439
	public int nMaxNum;

	// Token: 0x04001540 RID: 5440
	public string sDesc1;

	// Token: 0x04001541 RID: 5441
	public string sDesc2;

	// Token: 0x04001542 RID: 5442
	public string sTitle1;

	// Token: 0x04001543 RID: 5443
	public string sTitle2;

	// Token: 0x04001544 RID: 5444
	public string sBottom1;

	// Token: 0x04001545 RID: 5445
	public string sBottom2;
}
