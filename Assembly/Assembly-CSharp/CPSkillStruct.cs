using System;

// Token: 0x020000C1 RID: 193
public class CPSkillStruct
{
	// Token: 0x0400051D RID: 1309
	public int idx;

	// Token: 0x0400051E RID: 1310
	public string name;

	// Token: 0x0400051F RID: 1311
	public string icon;

	// Token: 0x04000520 RID: 1312
	public DateTime beginTime;

	// Token: 0x04000521 RID: 1313
	public int cd;

	// Token: 0x04000522 RID: 1314
	public string desc;

	// Token: 0x04000523 RID: 1315
	public string readyItem;

	// Token: 0x04000524 RID: 1316
	public string doKey;

	// Token: 0x04000525 RID: 1317
	public int[] triggerWeapon;

	// Token: 0x04000526 RID: 1318
	public PlayerSkillData _skilldata;

	// Token: 0x04000527 RID: 1319
	public int m_nDoNumberNow;

	// Token: 0x04000528 RID: 1320
	public int m_nDoNumberMax;

	// Token: 0x04000529 RID: 1321
	public int m_nItemNumberNow;

	// Token: 0x0400052A RID: 1322
	public int m_nActive;

	// Token: 0x0400052B RID: 1323
	public int havecd;

	// Token: 0x0400052C RID: 1324
	public bool ReadyKeyOk;

	// Token: 0x0400052D RID: 1325
	public int xingtai;

	// Token: 0x0400052E RID: 1326
	public float cdCheckTime;

	// Token: 0x0400052F RID: 1327
	public int readySwitch;

	// Token: 0x04000530 RID: 1328
	public int m_nNpcNum;

	// Token: 0x04000531 RID: 1329
	public int m_nSKLongTime;

	// Token: 0x04000532 RID: 1330
	public float mClientDoTime;
}
