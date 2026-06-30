using System;
using System.Xml;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class SkillInfoData
{
	// Token: 0x0600271E RID: 10014 RVA: 0x00162168 File Offset: 0x00160368
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("icon");
		this.nCol = int.Parse(xml.GetAttribute("col"));
		this.sType = xml.GetAttribute("type");
		this.sKeyDesc = xml.GetAttribute("key");
		this.sDesc = xml.GetAttribute("desc");
		string[] array = xml.GetAttribute("pos").Split(new char[]
		{
			','
		});
		bool flag = array.Length == 3;
		if (flag)
		{
			this.showPos = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
	}

	// Token: 0x04002256 RID: 8790
	public int nId;

	// Token: 0x04002257 RID: 8791
	public string sName;

	// Token: 0x04002258 RID: 8792
	public string sIconName;

	// Token: 0x04002259 RID: 8793
	public int nCol;

	// Token: 0x0400225A RID: 8794
	public string sType;

	// Token: 0x0400225B RID: 8795
	public string sKeyDesc;

	// Token: 0x0400225C RID: 8796
	public string sDesc;

	// Token: 0x0400225D RID: 8797
	public Vector3 showPos = Vector3.zero;
}
