using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020002CB RID: 715
public class RechargeZuanData
{
	// Token: 0x060020F5 RID: 8437 RVA: 0x0012FFC4 File Offset: 0x0012E1C4
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sBtnTxt = xml.GetAttribute("name");
		this.sIconName = xml.GetAttribute("bgimg");
		this.sDesc1 = xml.GetAttribute("desc1").Replace("\\n", "\n");
		this.sDesc2 = xml.GetAttribute("desc2").Replace("\\n", "\n");
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "d";
			if (flag)
			{
				ArrayList arrayList = new ArrayList();
				int num = int.Parse(xmlElement.GetAttribute("id"));
				arrayList.Add(num);
				arrayList.Add(int.Parse(xmlElement.GetAttribute("targnum")));
				arrayList.Add(xmlElement.GetAttribute("reward").Split(new char[]
				{
					','
				}));
				this.allRewardInfo.Add(num, arrayList);
			}
		}
	}

	// Token: 0x04001DF2 RID: 7666
	public int nId;

	// Token: 0x04001DF3 RID: 7667
	public string sIconName;

	// Token: 0x04001DF4 RID: 7668
	public string sDesc1;

	// Token: 0x04001DF5 RID: 7669
	public string sDesc2;

	// Token: 0x04001DF6 RID: 7670
	public string sBtnTxt;

	// Token: 0x04001DF7 RID: 7671
	public Dictionary<int, ArrayList> allRewardInfo = new Dictionary<int, ArrayList>();
}
