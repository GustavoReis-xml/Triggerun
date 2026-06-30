using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class ExchangeData
{
	// Token: 0x06000A56 RID: 2646 RVA: 0x0005CEBC File Offset: 0x0005B0BC
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sTabName = xml.GetAttribute("name");
		string[] array = xml.GetAttribute("size").Split(new char[]
		{
			','
		});
		this.iconSize = new Vector2(float.Parse(array[0]), float.Parse(array[1]));
		this.sIconUrl = xml.GetAttribute("url");
		this.chipId = int.Parse(xml.GetAttribute("chip"));
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			ExchangeInfo exchangeInfo = new ExchangeInfo();
			exchangeInfo.Init(xml2);
			this.szExchangeInfo.Add(exchangeInfo);
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0005CFB8 File Offset: 0x0005B1B8
	public List<ExchangeInfo> GetExchangeInfo(int nCommon)
	{
		List<ExchangeInfo> list = new List<ExchangeInfo>();
		bool flag = this.nId != 3;
		List<ExchangeInfo> result;
		if (flag)
		{
			result = this.szExchangeInfo;
		}
		else
		{
			bool flag2 = nCommon == -1;
			if (flag2)
			{
				result = this.szExchangeInfo;
			}
			else
			{
				for (int i = 0; i < this.szExchangeInfo.Count; i++)
				{
					Gem_Entity gem_Entity = fGameManager.mloadConfig.Gem_Xml_Data[this.szExchangeInfo[i].itemData.m_nTypeId];
					bool flag3 = gem_Entity.Limit_Hero_Index == nCommon;
					if (flag3)
					{
						list.Add(this.szExchangeInfo[i]);
					}
				}
				result = list;
			}
		}
		return result;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0005D070 File Offset: 0x0005B270
	public ExchangeInfo GetOneInfo(int nIdx)
	{
		for (int i = 0; i < this.szExchangeInfo.Count; i++)
		{
			bool flag = this.szExchangeInfo[i].nIdx == nIdx;
			if (flag)
			{
				return this.szExchangeInfo[i];
			}
		}
		return null;
	}

	// Token: 0x0400085A RID: 2138
	public int nId;

	// Token: 0x0400085B RID: 2139
	public string sTabName;

	// Token: 0x0400085C RID: 2140
	public Vector2 iconSize;

	// Token: 0x0400085D RID: 2141
	public string sIconUrl;

	// Token: 0x0400085E RID: 2142
	public int chipId;

	// Token: 0x0400085F RID: 2143
	public List<ExchangeInfo> szExchangeInfo = new List<ExchangeInfo>();
}
