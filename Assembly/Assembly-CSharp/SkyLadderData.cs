using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020003D9 RID: 985
public class SkyLadderData
{
	// Token: 0x06002A16 RID: 10774 RVA: 0x00172438 File Offset: 0x00170638
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.nMinOpenLvl = int.Parse(xml.GetAttribute("minlvl"));
		this.nMinMatchNum = int.Parse(xml.GetAttribute("minnum"));
		this.nOverTipDay = int.Parse(xml.GetAttribute("tipday"));
		this.sLvlDesc = xml.GetAttribute("lvldesc").Replace("\\n", "\n");
		this.sMatchDesc = xml.GetAttribute("matchdesc").Replace("\\n", "\n");
		this.sSeasonDesc = xml.GetAttribute("seasondesc").Replace("\\n", "\n");
		this.sBtnDesc = xml.GetAttribute("btndesc").Replace("\\n", "\n");
		this.sPreTime = xml.GetAttribute("pretime");
		this.sNextTime = xml.GetAttribute("nexttime");
		this.szOpenTime = xml.GetAttribute("starttime").Split(new char[]
		{
			','
		});
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "lvl";
			if (flag)
			{
				SkyLadderReward skyLadderReward = new SkyLadderReward();
				skyLadderReward.Init(xmlElement);
				bool flag2 = skyLadderReward.nLvl > this.nMaxLvl;
				if (flag2)
				{
					this.nMaxLvl = skyLadderReward.nLvl;
				}
				this.szSkyLadderReward.Add(skyLadderReward.nLvl, skyLadderReward);
			}
		}
	}

	// Token: 0x04002392 RID: 9106
	public int nId;

	// Token: 0x04002393 RID: 9107
	public int nMinOpenLvl;

	// Token: 0x04002394 RID: 9108
	public int nMinMatchNum;

	// Token: 0x04002395 RID: 9109
	public int nOverTipDay;

	// Token: 0x04002396 RID: 9110
	public string sLvlDesc;

	// Token: 0x04002397 RID: 9111
	public string sMatchDesc;

	// Token: 0x04002398 RID: 9112
	public string sSeasonDesc;

	// Token: 0x04002399 RID: 9113
	public string sBtnDesc;

	// Token: 0x0400239A RID: 9114
	public string sPreTime;

	// Token: 0x0400239B RID: 9115
	public string sNextTime;

	// Token: 0x0400239C RID: 9116
	public string[] szOpenTime;

	// Token: 0x0400239D RID: 9117
	public int nMaxLvl;

	// Token: 0x0400239E RID: 9118
	public Dictionary<int, SkyLadderReward> szSkyLadderReward = new Dictionary<int, SkyLadderReward>();
}
