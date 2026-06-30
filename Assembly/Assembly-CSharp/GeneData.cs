using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000150 RID: 336
[Serializable]
public class GeneData
{
	// Token: 0x06000F6D RID: 3949 RVA: 0x0008CF8C File Offset: 0x0008B18C
	public void Init(XmlElement xml)
	{
		this.nStep = int.Parse(xml.GetAttribute("step"));
		this.sName = xml.GetAttribute("name");
		string[] array = xml.GetAttribute("genemap").Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				this.szStepMap.Add(int.Parse(array[i]));
			}
		}
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag2 = xmlElement.Name == "s";
			if (flag2)
			{
				GeneStarData geneStarData = new GeneStarData();
				geneStarData.Init(xmlElement);
				bool flag3 = geneStarData.nStar > this.nMaxNum;
				if (flag3)
				{
					this.nMaxNum = geneStarData.nStar;
				}
				this.allStarData.Add(geneStarData.nStar, geneStarData);
			}
		}
	}

	// Token: 0x04000BEC RID: 3052
	public int nStep;

	// Token: 0x04000BED RID: 3053
	public string sName;

	// Token: 0x04000BEE RID: 3054
	public List<int> szStepMap = new List<int>();

	// Token: 0x04000BEF RID: 3055
	public int nMaxNum;

	// Token: 0x04000BF0 RID: 3056
	public Dictionary<int, GeneStarData> allStarData = new Dictionary<int, GeneStarData>();
}
