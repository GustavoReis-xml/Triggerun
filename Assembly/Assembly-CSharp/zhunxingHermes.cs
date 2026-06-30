using System;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public class zhunxingHermes : zhunxingBase
{
	// Token: 0x06002EC4 RID: 11972 RVA: 0x001A2020 File Offset: 0x001A0220
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.initShanxianUi(gotherui.transform.FindChild("shanxian"));
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x001A2044 File Offset: 0x001A0244
	protected override void Update()
	{
		base.Update();
		bool flag = this.cpstruct == null && this.hero.heroSkill.skillsDict != null;
		if (flag)
		{
			this.cpstruct = this.hero.heroSkill.skillsDict[1001];
		}
		bool flag2 = this.cpstruct != null && this.cpstruct.m_nDoNumberNow != this.nShanxianNowCs;
		if (flag2)
		{
			this.setShanxianCs(this.cpstruct.m_nDoNumberNow);
			this.nShanxianNowCs = this.cpstruct.m_nDoNumberNow;
		}
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x001A20E8 File Offset: 0x001A02E8
	private void setShanxianCs(int iDefference)
	{
		switch (iDefference)
		{
		case 0:
			this.GCs1.SetActive(false);
			this.GCs2.SetActive(false);
			this.GCs3.SetActive(false);
			break;
		case 1:
			this.GCs1.SetActive(true);
			this.GCs2.SetActive(false);
			this.GCs3.SetActive(false);
			break;
		case 2:
			this.GCs1.SetActive(true);
			this.GCs2.SetActive(true);
			this.GCs3.SetActive(false);
			break;
		case 3:
			this.GCs1.SetActive(true);
			this.GCs2.SetActive(true);
			this.GCs3.SetActive(true);
			break;
		}
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x001A21BC File Offset: 0x001A03BC
	private void initShanxianUi(Transform t)
	{
		this.GCs1 = t.transform.FindChild("cs3").gameObject;
		this.GCs2 = t.transform.FindChild("cs2").gameObject;
		this.GCs3 = t.transform.FindChild("cs1").gameObject;
	}

	// Token: 0x0400286B RID: 10347
	private int nShanxianNowCs = 3;

	// Token: 0x0400286C RID: 10348
	private CPSkillStruct cpstruct;

	// Token: 0x0400286D RID: 10349
	private GameObject GCs1;

	// Token: 0x0400286E RID: 10350
	private GameObject GCs2;

	// Token: 0x0400286F RID: 10351
	private GameObject GCs3;
}
