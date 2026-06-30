using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x02000294 RID: 660
public class PlayerFaceData
{
	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x00117CDD File Offset: 0x00115EDD
	public int key
	{
		get
		{
			return this.nFaceId;
		}
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x00117CE8 File Offset: 0x00115EE8
	public void Init(XmlElement xml, int sortid)
	{
		this.nFaceId = int.Parse(xml.GetAttribute("faceid"));
		this.sFaceName = xml.GetAttribute("name");
		this.sFaceIcon = xml.GetAttribute("faceicon");
		this.sOpenDesc = xml.GetAttribute("desc");
		this.nIsZhekou = int.Parse(xml.GetAttribute("iszhekou"));
		this.szUnLock = xml.GetAttribute("unlock").Split(new char[]
		{
			'|'
		});
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.sGiftDesc = xml.GetAttribute("zsdesc");
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
		this.nSortId = sortid;
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x00117DE8 File Offset: 0x00115FE8
	public List<string> BtnUnlockData()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.szBtnData.Length; i += 2)
		{
			bool flag = i + 1 < this.szBtnData.Length;
			if (flag)
			{
				string text = this.szBtnData[i + 1];
				list.Add(this.szBtnData[i]);
				list.Add(text);
				list.Add(this.GetUnlockValue(text));
			}
		}
		return list;
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x00117E64 File Offset: 0x00116064
	public string GetUnlockValue(string stype)
	{
		string result = string.Empty;
		for (int i = 0; i < this.szUnLock.Length; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(this.szUnLock[i]) && this.szUnLock[i] == stype;
			if (flag)
			{
				result = this.szUnLock[i + 1];
				break;
			}
		}
		return result;
	}

	// Token: 0x04001B7C RID: 7036
	public int nFaceId;

	// Token: 0x04001B7D RID: 7037
	public string sFaceName;

	// Token: 0x04001B7E RID: 7038
	public string sFaceIcon;

	// Token: 0x04001B7F RID: 7039
	public string sOpenDesc;

	// Token: 0x04001B80 RID: 7040
	public int nIsZhekou;

	// Token: 0x04001B81 RID: 7041
	public string[] szUnLock;

	// Token: 0x04001B82 RID: 7042
	public string[] szBtnData;

	// Token: 0x04001B83 RID: 7043
	public int nSortId;

	// Token: 0x04001B84 RID: 7044
	public string[] szGiftData;

	// Token: 0x04001B85 RID: 7045
	public string sGiftDesc;

	// Token: 0x04001B86 RID: 7046
	public int nGiftNum;

	// Token: 0x04001B87 RID: 7047
	public int nIsOpen;

	// Token: 0x04001B88 RID: 7048
	public int nIsNew;

	// Token: 0x04001B89 RID: 7049
	public int nGiftTime;
}
