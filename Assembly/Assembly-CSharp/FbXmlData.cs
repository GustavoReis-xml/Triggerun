using System;
using System.Collections;
using System.Xml;

// Token: 0x02000126 RID: 294
[Serializable]
public class FbXmlData
{
	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00062602 File Offset: 0x00060802
	public int key
	{
		get
		{
			return this.nId;
		}
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0006260C File Offset: 0x0006080C
	public void Init(XmlElement xml)
	{
		this.nId = int.Parse(xml.GetAttribute("id"));
		this.sName = xml.GetAttribute("name");
		this.sIcon = xml.GetAttribute("icon");
		this.nScnid = int.Parse(xml.GetAttribute("mapid"));
		this.sMapAss = xml.GetAttribute("prefb");
		this.nTeam = int.Parse(xml.GetAttribute("isteam"));
		this.nType = int.Parse(xml.GetAttribute("fbtype"));
		this.nstartplotid = int.Parse(xml.GetAttribute("startplotid"));
		this.sSmallMap = xml.GetAttribute("smallmap");
		this.selectsound = xml.GetAttribute("selectsound");
		this.loadsound = xml.GetAttribute("loadsound");
		this.sTemp = xml.GetAttribute("uipos");
		this.nloadmap = int.Parse(xml.GetAttribute("loadmap"));
		this.sTeamshowbg = xml.GetAttribute("teamshowbg");
		this.uipos = this.sTemp.Split(new char[]
		{
			','
		});
		this.sMapDesc = xml.GetAttribute("mapdesc");
		this.nispublic = int.Parse(xml.GetAttribute("ispublic"));
		this.isRobotFb = int.Parse(xml.GetAttribute("robotfb"));
		this.scnmodelimage = xml.GetAttribute("scnmodelicon");
		this.scnmodelname = xml.GetAttribute("scnmodelname");
		this.scnmodeldesc = xml.GetAttribute("scnmodeldesc");
		this.m_szPveLevel = ItemData.GetArrlist(xml.GetAttribute("pvelevel"), '|', false, ',');
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x000627D0 File Offset: 0x000609D0
	public string GetModeStr()
	{
		string result = string.Empty;
		switch (this.nType)
		{
		case 1:
			result = Language.GetLang(8001, new object[0]);
			break;
		case 2:
			result = Language.GetLang(8002, new object[0]);
			break;
		case 3:
			result = Language.GetLang(8003, new object[0]);
			break;
		case 4:
			result = Language.GetLang(8004, new object[0]);
			break;
		case 5:
			result = Language.GetLang(8005, new object[0]);
			break;
		case 7:
			result = Language.GetLang(8024, new object[0]);
			break;
		case 11:
			result = Language.GetLang(8032, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x040008E1 RID: 2273
	public int nId;

	// Token: 0x040008E2 RID: 2274
	public string sName;

	// Token: 0x040008E3 RID: 2275
	public string sIcon;

	// Token: 0x040008E4 RID: 2276
	public int nScnid;

	// Token: 0x040008E5 RID: 2277
	public string sMapAss;

	// Token: 0x040008E6 RID: 2278
	public int nTeam;

	// Token: 0x040008E7 RID: 2279
	public int nModelid;

	// Token: 0x040008E8 RID: 2280
	public int nType;

	// Token: 0x040008E9 RID: 2281
	public int nstartplotid;

	// Token: 0x040008EA RID: 2282
	public string sSmallMap;

	// Token: 0x040008EB RID: 2283
	public string selectsound;

	// Token: 0x040008EC RID: 2284
	public string loadsound;

	// Token: 0x040008ED RID: 2285
	public string[] uipos;

	// Token: 0x040008EE RID: 2286
	private string sTemp;

	// Token: 0x040008EF RID: 2287
	public int nloadmap;

	// Token: 0x040008F0 RID: 2288
	public string sTeamshowbg;

	// Token: 0x040008F1 RID: 2289
	public string sMapDesc;

	// Token: 0x040008F2 RID: 2290
	public int nispublic;

	// Token: 0x040008F3 RID: 2291
	public int isRobotFb;

	// Token: 0x040008F4 RID: 2292
	public string scnmodelimage;

	// Token: 0x040008F5 RID: 2293
	public string scnmodelname;

	// Token: 0x040008F6 RID: 2294
	public string scnmodeldesc;

	// Token: 0x040008F7 RID: 2295
	public ArrayList m_szPveLevel;
}
