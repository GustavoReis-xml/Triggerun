using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x0200030A RID: 778
[Serializable]
public class ShopData
{
	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x060025BB RID: 9659 RVA: 0x001565FB File Offset: 0x001547FB
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x00156604 File Offset: 0x00154804
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.m_sName = xml.GetAttribute("name");
		this.m_nTitle = int.Parse(xml.GetAttribute("title"));
		this.m_nItemId = int.Parse(xml.GetAttribute("itemid"));
		this.m_nZheKou = int.Parse(xml.GetAttribute("zhekou"));
		this.m_nMaxnum = int.Parse(xml.GetAttribute("maxnum"));
		this.m_nIsTuiJian = int.Parse(xml.GetAttribute("istuijian"));
		this.m_nIsNew = int.Parse(xml.GetAttribute("isnew"));
		this.m_nIsHot = int.Parse(xml.GetAttribute("ishot"));
		this.m_nIsLimit = int.Parse(xml.GetAttribute("isxianshi"));
		this.m_sStartTime = xml.GetAttribute("starttime");
		this.m_sEndTime = xml.GetAttribute("endtime");
		this.m_nIsZengSong = int.Parse(xml.GetAttribute("iszs"));
		this.m_nZsMaxnum = int.Parse(xml.GetAttribute("zsnum"));
		this.m_nZsType = int.Parse(xml.GetAttribute("zstype"));
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x00156750 File Offset: 0x00154950
	public static List<Vector3> GetShopPos(string sPos)
	{
		List<Vector3> list = new List<Vector3>
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.zero,
			Vector3.zero,
			Vector3.zero
		};
		Vector3 zero = Vector3.zero;
		string[] array = sPos.Split(new char[]
		{
			','
		});
		int num = array.Length;
		bool flag = num < 1 || num % 3 != 0;
		List<Vector3> result;
		if (flag)
		{
			result = list;
		}
		else
		{
			for (int i = 0; i < num; i += 3)
			{
				zero = new Vector3(float.Parse(array[i]), float.Parse(array[i + 1]), float.Parse(array[i + 2]));
				list[i / 3] = zero;
			}
			result = list;
		}
		return result;
	}

	// Token: 0x04002163 RID: 8547
	public int id;

	// Token: 0x04002164 RID: 8548
	public string m_sName;

	// Token: 0x04002165 RID: 8549
	public int m_nTitle;

	// Token: 0x04002166 RID: 8550
	public int m_nItemId;

	// Token: 0x04002167 RID: 8551
	public int m_nZheKou;

	// Token: 0x04002168 RID: 8552
	public int m_nMaxnum;

	// Token: 0x04002169 RID: 8553
	public int m_nIsTuiJian;

	// Token: 0x0400216A RID: 8554
	public int m_nIsNew;

	// Token: 0x0400216B RID: 8555
	public int m_nIsHot;

	// Token: 0x0400216C RID: 8556
	public int m_nIsLimit;

	// Token: 0x0400216D RID: 8557
	public string m_sStartTime;

	// Token: 0x0400216E RID: 8558
	public string m_sEndTime;

	// Token: 0x0400216F RID: 8559
	public int m_nIsZengSong = 1;

	// Token: 0x04002170 RID: 8560
	public int m_nZsMaxnum;

	// Token: 0x04002171 RID: 8561
	public int m_nZsType;
}
