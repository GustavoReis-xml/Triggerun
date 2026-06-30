using System;
using System.Collections;
using System.Xml;

// Token: 0x020000F6 RID: 246
public class DiscountInfo
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x0004A418 File Offset: 0x00048618
	public DiscountInfo(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sTabName = xml.GetAttribute("name");
		string[] array = xml.GetAttribute("itemdata").Split(new char[]
		{
			'#'
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				int key = int.Parse(array2[0]);
				bool flag2 = fGameManager.mloadConfig.AllItemData.ContainsKey(key);
				if (flag2)
				{
					ArrayList value = new ArrayList
					{
						int.Parse(array2[0]),
						int.Parse(array2[1]),
						int.Parse(array2[2]),
						int.Parse(array2[3]),
						int.Parse(array2[4]),
						int.Parse(array2[5]),
						int.Parse(array2[6]),
						int.Parse(array2[7])
					};
					this.szItemInfo.Add(value);
				}
			}
		}
		this.nItemNum = this.szItemInfo.Count;
		this.szItemIcon = xml.GetAttribute("icon").Split(new char[]
		{
			','
		});
	}

	// Token: 0x04000656 RID: 1622
	public int nId;

	// Token: 0x04000657 RID: 1623
	public string sTabName;

	// Token: 0x04000658 RID: 1624
	public int nItemNum;

	// Token: 0x04000659 RID: 1625
	public ArrayList szItemInfo = new ArrayList();

	// Token: 0x0400065A RID: 1626
	public string[] szItemIcon = new string[0];
}
