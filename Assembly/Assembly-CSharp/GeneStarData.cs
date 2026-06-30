using System;
using System.Xml;

// Token: 0x02000151 RID: 337
public class GeneStarData
{
	// Token: 0x06000F6F RID: 3951 RVA: 0x0008D0EC File Offset: 0x0008B2EC
	public void Init(XmlElement xml)
	{
		this.nStar = int.Parse(xml.GetAttribute("star"));
		this.sName = xml.GetAttribute("name");
		this.nRate = int.Parse(xml.GetAttribute("rate"));
		this.szCost = xml.GetAttribute("cost").Split(new char[]
		{
			','
		});
		this.nEffect = int.Parse(xml.GetAttribute("effect"));
		this.szTip = xml.GetAttribute("stip").Replace("\\n", "\n").Split(new char[]
		{
			'|'
		});
		this.szAttr = xml.GetAttribute("attrdata").Split(new char[]
		{
			','
		});
	}

	// Token: 0x04000BF1 RID: 3057
	public int nStar;

	// Token: 0x04000BF2 RID: 3058
	public string sName;

	// Token: 0x04000BF3 RID: 3059
	public int nRate;

	// Token: 0x04000BF4 RID: 3060
	public string[] szCost;

	// Token: 0x04000BF5 RID: 3061
	public int nEffect;

	// Token: 0x04000BF6 RID: 3062
	public string[] szTip;

	// Token: 0x04000BF7 RID: 3063
	public string[] szAttr;
}
