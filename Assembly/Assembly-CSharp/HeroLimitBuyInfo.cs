using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x0200019A RID: 410
public class HeroLimitBuyInfo
{
	// Token: 0x0600133F RID: 4927 RVA: 0x000B7FBC File Offset: 0x000B61BC
	public void Init(XmlElement xml)
	{
		this.nHeroId = int.Parse(xml.GetAttribute("heroid"));
		this.nType = int.Parse(xml.GetAttribute("type"));
		this.nIndex = int.Parse(xml.GetAttribute("index"));
		this.sKey = string.Format("{0}_{1}_{2}", this.nHeroId, this.nType, this.nIndex);
		this.szUnLock = xml.GetAttribute("unlockdata").Split(new char[]
		{
			'|'
		});
		this.sUnlockDesc = xml.GetAttribute("desc");
		this.szBtnData = xml.GetAttribute("btndata").Split(new char[]
		{
			'|'
		});
		this.szGiftData = xml.GetAttribute("zstype").Split(new char[]
		{
			','
		});
		this.nGiftNum = int.Parse(xml.GetAttribute("jbzsnum"));
		this.sGiftDesc = xml.GetAttribute("zsdesc");
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000B80D8 File Offset: 0x000B62D8
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

	// Token: 0x06001341 RID: 4929 RVA: 0x000B8154 File Offset: 0x000B6354
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

	// Token: 0x04000EFD RID: 3837
	public string sKey;

	// Token: 0x04000EFE RID: 3838
	public int nHeroId;

	// Token: 0x04000EFF RID: 3839
	public int nType;

	// Token: 0x04000F00 RID: 3840
	public int nIndex;

	// Token: 0x04000F01 RID: 3841
	public string[] szUnLock;

	// Token: 0x04000F02 RID: 3842
	public string sUnlockDesc;

	// Token: 0x04000F03 RID: 3843
	public string[] szBtnData;

	// Token: 0x04000F04 RID: 3844
	public string[] szGiftData;

	// Token: 0x04000F05 RID: 3845
	public int nGiftNum;

	// Token: 0x04000F06 RID: 3846
	public string sGiftDesc;
}
