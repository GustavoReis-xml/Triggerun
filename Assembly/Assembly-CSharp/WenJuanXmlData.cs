using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020004A5 RID: 1189
[Serializable]
public class WenJuanXmlData
{
	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x00196082 File Offset: 0x00194282
	public int key
	{
		get
		{
			return this.wId;
		}
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x0019608C File Offset: 0x0019428C
	public void Init(XmlElement xml)
	{
		this.wId = int.Parse(xml.GetAttribute("id"));
		this.wReward = xml.GetAttribute("reward");
		this.wText = xml.GetAttribute("text");
		this.wText = this.wText.Replace('[', '<');
		this.wText = this.wText.Replace(']', '>');
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			WenJuanXmlEventData wenJuanXmlEventData = new WenJuanXmlEventData();
			wenJuanXmlEventData.eId = int.Parse(xmlElement.GetAttribute("id"));
			wenJuanXmlEventData.eType = int.Parse(xmlElement.GetAttribute("type"));
			wenJuanXmlEventData.eText = xmlElement.GetAttribute("text");
			wenJuanXmlEventData.eText = wenJuanXmlEventData.eText.Replace('[', '<');
			wenJuanXmlEventData.eText = wenJuanXmlEventData.eText.Replace(']', '>');
			foreach (object obj2 in xmlElement.ChildNodes)
			{
				XmlElement xmlElement2 = (XmlElement)obj2;
				WenJuanXmlKeyData wenJuanXmlKeyData = new WenJuanXmlKeyData();
				wenJuanXmlKeyData.kId = int.Parse(xmlElement2.GetAttribute("id"));
				wenJuanXmlKeyData.kType = int.Parse(xmlElement2.GetAttribute("type"));
				wenJuanXmlKeyData.kText = xmlElement2.GetAttribute("text");
				wenJuanXmlKeyData.kText = wenJuanXmlKeyData.kText.Replace('[', '<');
				wenJuanXmlKeyData.kText = wenJuanXmlKeyData.kText.Replace(']', '>');
				wenJuanXmlEventData.eWenJuanKeyData.Add(wenJuanXmlKeyData.kId, wenJuanXmlKeyData);
			}
			this.wWenJuanEventData.Add(wenJuanXmlEventData.eId, wenJuanXmlEventData);
		}
	}

	// Token: 0x04002744 RID: 10052
	public int wId;

	// Token: 0x04002745 RID: 10053
	public string wReward;

	// Token: 0x04002746 RID: 10054
	public string wText;

	// Token: 0x04002747 RID: 10055
	public Dictionary<int, WenJuanXmlEventData> wWenJuanEventData = new Dictionary<int, WenJuanXmlEventData>();
}
