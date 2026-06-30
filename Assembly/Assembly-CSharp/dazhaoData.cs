using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000F1 RID: 241
[Serializable]
public class dazhaoData
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x0600089E RID: 2206 RVA: 0x00049369 File Offset: 0x00047569
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00049374 File Offset: 0x00047574
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.dname = xml.GetAttribute("name");
		this.dtype = int.Parse(xml.GetAttribute("type"));
		this.dazhaoList = new List<dazhaoXml>();
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			dazhaoXml dazhaoXml = new dazhaoXml();
			this.dazhaoList.Add(dazhaoXml);
			dazhaoXml.id = int.Parse(xmlElement.GetAttribute("id"));
			dazhaoXml.dname = xmlElement.GetAttribute("name");
			dazhaoXml.denergycosttype = int.Parse(xmlElement.GetAttribute("energycosttype"));
			dazhaoXml.denergycost = int.Parse(xmlElement.GetAttribute("energycost"));
			dazhaoXml.denergycosttime = int.Parse(xmlElement.GetAttribute("energycosttime"));
			dazhaoXml.doskill = int.Parse(xmlElement.GetAttribute("doskill"));
			dazhaoXml.denergymax = int.Parse(xmlElement.GetAttribute("energymax"));
			dazhaoXml.dxingtai = int.Parse(xmlElement.GetAttribute("xingtai"));
			dazhaoXml.dcontinuetime = float.Parse(xmlElement.GetAttribute("continuestime")) / 1000f;
			string attribute = xmlElement.GetAttribute("dogun");
			bool flag = attribute != "0";
			if (flag)
			{
				string[] array = attribute.Split(new char[]
				{
					'|'
				});
				dazhaoXml.dogun = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					dazhaoXml.dogun[i] = int.Parse(array[i]);
				}
			}
			dazhaoXml.dicon = xmlElement.GetAttribute("icon");
			dazhaoXml.dcandotype = int.Parse(xmlElement.GetAttribute("candotype"));
		}
	}

	// Token: 0x04000639 RID: 1593
	public int id;

	// Token: 0x0400063A RID: 1594
	public string dname;

	// Token: 0x0400063B RID: 1595
	public int dtype;

	// Token: 0x0400063C RID: 1596
	public List<dazhaoXml> dazhaoList;
}
