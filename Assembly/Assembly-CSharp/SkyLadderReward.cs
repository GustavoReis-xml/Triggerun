using System;
using System.Xml;

// Token: 0x020003DC RID: 988
public class SkyLadderReward
{
	// Token: 0x06002A34 RID: 10804 RVA: 0x0017485C File Offset: 0x00172A5C
	public void Init(XmlElement xml)
	{
		this.nLvl = int.Parse(xml.GetAttribute("id"));
		this.sLvlname = xml.GetAttribute("name");
		this.nMinScore = int.Parse(xml.GetAttribute("minscore"));
		this.nMaxScore = int.Parse(xml.GetAttribute("maxscore"));
		this.nIsNoLvl = int.Parse(xml.GetAttribute("nolvlmatch"));
		this.nIsSaveLvl = int.Parse(xml.GetAttribute("saveicon"));
		this.nCutScore = int.Parse(xml.GetAttribute("cutscore"));
		this.szLvlReward = xml.GetAttribute("reward").Split(new char[]
		{
			'#'
		});
		this.szEndReward = xml.GetAttribute("endreward").Split(new char[]
		{
			'#'
		});
	}

	// Token: 0x040023CA RID: 9162
	public int nLvl;

	// Token: 0x040023CB RID: 9163
	public string sLvlname;

	// Token: 0x040023CC RID: 9164
	public int nMinScore;

	// Token: 0x040023CD RID: 9165
	public int nMaxScore;

	// Token: 0x040023CE RID: 9166
	public int nIsNoLvl;

	// Token: 0x040023CF RID: 9167
	public int nIsSaveLvl;

	// Token: 0x040023D0 RID: 9168
	public int nCutScore;

	// Token: 0x040023D1 RID: 9169
	public string[] szLvlReward;

	// Token: 0x040023D2 RID: 9170
	public string[] szEndReward;
}
