using System;
using System.Collections;
using System.Xml;

// Token: 0x020004AC RID: 1196
public class WinterTarData
{
	// Token: 0x06002E09 RID: 11785 RVA: 0x001976BC File Offset: 0x001958BC
	public WinterTarData(XmlElement xml)
	{
		this.nIndex = int.Parse(xml.GetAttribute("id"));
		this.sDesc = xml.GetAttribute("name");
		this.nType = int.Parse(xml.GetAttribute("type"));
		this.nMaxNum = int.Parse(xml.GetAttribute("maxnum"));
		string[] array = xml.GetAttribute("data").Split(new char[]
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
				this.szData.Add(new ArrayList
				{
					int.Parse(array2[0]),
					int.Parse(array2[1])
				});
			}
		}
	}

	// Token: 0x0400276C RID: 10092
	public int nIndex;

	// Token: 0x0400276D RID: 10093
	public string sDesc;

	// Token: 0x0400276E RID: 10094
	public int nType;

	// Token: 0x0400276F RID: 10095
	public int nMaxNum;

	// Token: 0x04002770 RID: 10096
	public ArrayList szData = new ArrayList();
}
