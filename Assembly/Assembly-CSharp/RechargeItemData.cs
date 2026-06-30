using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020002C9 RID: 713
public class RechargeItemData
{
	// Token: 0x060020E9 RID: 8425 RVA: 0x0012F6B8 File Offset: 0x0012D8B8
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
					'#'
				}));
				this.allRewardInfo.Add(num, arrayList);
			}
		}
	}

	// Token: 0x04001DE3 RID: 7651
	public int nId;

	// Token: 0x04001DE4 RID: 7652
	public string sIconName;

	// Token: 0x04001DE5 RID: 7653
	public string sDesc1;

	// Token: 0x04001DE6 RID: 7654
	public string sDesc2;

	// Token: 0x04001DE7 RID: 7655
	public string sBtnTxt;

	// Token: 0x04001DE8 RID: 7656
	public Dictionary<int, ArrayList> allRewardInfo = new Dictionary<int, ArrayList>();
}
