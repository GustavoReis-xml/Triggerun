using System;
using System.Xml;

// Token: 0x02000484 RID: 1156
public class TuijianData
{
	// Token: 0x06002CB4 RID: 11444 RVA: 0x0018983C File Offset: 0x00187A3C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sActName = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.nBtnType = int.Parse(xml.GetAttribute("btntype"));
		this.sBtnTxt = xml.GetAttribute("btntxt");
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
	}

	// Token: 0x040025E3 RID: 9699
	public int nId;

	// Token: 0x040025E4 RID: 9700
	public string sActName;

	// Token: 0x040025E5 RID: 9701
	public string sIconName;

	// Token: 0x040025E6 RID: 9702
	public int nBtnType;

	// Token: 0x040025E7 RID: 9703
	public string sBtnTxt;

	// Token: 0x040025E8 RID: 9704
	public string[] szBtnData;
}
