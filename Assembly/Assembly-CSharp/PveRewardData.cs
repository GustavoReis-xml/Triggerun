using System;
using System.Collections;
using System.Xml;

// Token: 0x020002AE RID: 686
public class PveRewardData
{
	// Token: 0x06001F87 RID: 8071 RVA: 0x00121D5C File Offset: 0x0011FF5C
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.fbId = int.Parse(xml.GetAttribute("fbid"));
		this.sName = xml.GetAttribute("name");
		this.m_szRound = ItemData.GetArrlist(xml.GetAttribute("round"), ',', true, ',');
		this.m_szItem = ItemData.GetArrlist(xml.GetAttribute("item"), '|', false, ',');
		this.m_nSdItem = int.Parse(xml.GetAttribute("sditem"));
		this.m_nSdDrop = int.Parse(xml.GetAttribute("sddrop"));
		this.m_nItemNum = int.Parse(xml.GetAttribute("itemnum"));
		this.m_szCheckFanWei = ItemData.GetArrlist(xml.GetAttribute("checkfanwei"), '|', false, ',');
		this.m_nPreFbId = int.Parse(xml.GetAttribute("prefbid"));
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x00121E50 File Offset: 0x00120050
	public ArrayList GetOneRoundItem(int nRound)
	{
		ArrayList arrayList = new ArrayList();
		int num = this.m_szRound.IndexOf(nRound);
		bool flag = num == -1;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			bool flag2 = num >= this.m_szItem.Count;
			if (flag2)
			{
				result = arrayList;
			}
			else
			{
				result = (ArrayList)this.m_szItem[num];
			}
		}
		return result;
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x00121EB4 File Offset: 0x001200B4
	public ArrayList GetNextRoundItem(int nRound, int nMaxRound)
	{
		ArrayList arrayList = new ArrayList();
		while (nRound <= nMaxRound)
		{
			arrayList = this.GetOneRoundItem(nRound);
			bool flag = arrayList.Count > 0;
			if (flag)
			{
				break;
			}
			nRound++;
		}
		return new ArrayList
		{
			arrayList,
			nRound
		};
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x00121F18 File Offset: 0x00120118
	public ArrayList GetCheckFanWei(int nRound)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < this.m_szCheckFanWei.Count; i++)
		{
			arrayList = (ArrayList)this.m_szCheckFanWei[i];
			bool flag = (int)arrayList[0] == nRound;
			if (flag)
			{
				return arrayList;
			}
		}
		return new ArrayList();
	}

	// Token: 0x04001CAF RID: 7343
	public int id;

	// Token: 0x04001CB0 RID: 7344
	public int fbId;

	// Token: 0x04001CB1 RID: 7345
	public string sName;

	// Token: 0x04001CB2 RID: 7346
	public ArrayList m_szRound;

	// Token: 0x04001CB3 RID: 7347
	public ArrayList m_szItem;

	// Token: 0x04001CB4 RID: 7348
	public int m_nSdItem;

	// Token: 0x04001CB5 RID: 7349
	public int m_nItemNum;

	// Token: 0x04001CB6 RID: 7350
	public int m_nSdDrop;

	// Token: 0x04001CB7 RID: 7351
	public int m_nPreFbId;

	// Token: 0x04001CB8 RID: 7352
	public ArrayList m_szCheckFanWei;
}
