using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020002D4 RID: 724
public class RobotCommonData
{
	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x0600218B RID: 8587 RVA: 0x00137EA1 File Offset: 0x001360A1
	public int key
	{
		get
		{
			return this.indexId;
		}
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x00137EAC File Offset: 0x001360AC
	public void Init(XmlElement xml)
	{
		this.AI_Index = new List<int>();
		this.indexId = int.Parse(xml.GetAttribute("Index"));
		this.skinID = int.Parse(xml.GetAttribute("SkinIndex"));
		this.heroId = int.Parse(xml.GetAttribute("Hero_Id"));
		this.hitRate = (float)int.Parse(xml.GetAttribute("Hit_rate")) / 100f;
		this.beMonster = int.Parse(xml.GetAttribute("monster"));
		string attribute = xml.GetAttribute("robot_ai");
		string[] array = attribute.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.AI_Index.Add(int.Parse(array[i]));
		}
	}

	// Token: 0x04001E8B RID: 7819
	public int heroId;

	// Token: 0x04001E8C RID: 7820
	public int indexId;

	// Token: 0x04001E8D RID: 7821
	public int skinID;

	// Token: 0x04001E8E RID: 7822
	public ArrayList szAward = new ArrayList();

	// Token: 0x04001E8F RID: 7823
	public List<int> AI_Index;

	// Token: 0x04001E90 RID: 7824
	public float hitRate;

	// Token: 0x04001E91 RID: 7825
	public int beMonster;
}
