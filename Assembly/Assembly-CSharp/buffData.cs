using System;
using System.Xml;

// Token: 0x02000037 RID: 55
[Serializable]
public class buffData
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060001FA RID: 506 RVA: 0x0000C9C2 File Offset: 0x0000ABC2
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000C9CC File Offset: 0x0000ABCC
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.bname = xml.GetAttribute("name");
		this.bType = int.Parse(xml.GetAttribute("type"));
		this.m_nBuffTimeLong = int.Parse(xml.GetAttribute("time"));
		this.m_nBuffChangeValueSe = int.Parse(xml.GetAttribute("value"));
		this.m_nBuffInterval = int.Parse(xml.GetAttribute("interval"));
		this.m_nBuffOverlayType = int.Parse(xml.GetAttribute("overlaytype"));
		this.m_nBuffExtra = int.Parse(xml.GetAttribute("extra"));
		this.m_sBuffEff_tps = xml.GetAttribute("effect_tps");
		this.m_sBuffEff_fps = xml.GetAttribute("effect_fps");
		this.m_sSoundBegin = xml.GetAttribute("soundbegin");
		this.m_sSoundEnd = xml.GetAttribute("soundend");
		this.m_nIsMsgTxt = int.Parse(xml.GetAttribute("ismsgtxt"));
		this.m_sHeadeffectico = xml.GetAttribute("headeffectico");
		this.m_nSoundbuff = int.Parse(xml.GetAttribute("soundbuff"));
		this.m_nStopFaceActive = int.Parse(xml.GetAttribute("isStopFaceActive"));
		this.m_nStopHelloActive = int.Parse(xml.GetAttribute("isStopHelloActive"));
	}

	// Token: 0x04000199 RID: 409
	public int id;

	// Token: 0x0400019A RID: 410
	public string bname;

	// Token: 0x0400019B RID: 411
	public int bType;

	// Token: 0x0400019C RID: 412
	public int m_nBuffTimeLong;

	// Token: 0x0400019D RID: 413
	public int m_nBuffChangeValueSe;

	// Token: 0x0400019E RID: 414
	public int m_nBuffInterval;

	// Token: 0x0400019F RID: 415
	public int m_nBuffOverlayType;

	// Token: 0x040001A0 RID: 416
	public int m_nBuffExtra;

	// Token: 0x040001A1 RID: 417
	public string m_sBuffEff_tps;

	// Token: 0x040001A2 RID: 418
	public string m_sBuffEff_fps;

	// Token: 0x040001A3 RID: 419
	public string m_sSoundBegin;

	// Token: 0x040001A4 RID: 420
	public string m_sSoundEnd;

	// Token: 0x040001A5 RID: 421
	public int m_nIsMsgTxt;

	// Token: 0x040001A6 RID: 422
	public string m_sHeadeffectico;

	// Token: 0x040001A7 RID: 423
	public int m_nSoundbuff;

	// Token: 0x040001A8 RID: 424
	public int m_nStopFaceActive;

	// Token: 0x040001A9 RID: 425
	public int m_nStopHelloActive;
}
