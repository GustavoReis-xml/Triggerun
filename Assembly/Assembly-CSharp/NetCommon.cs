using System;
using System.Collections;

// Token: 0x02000253 RID: 595
public class NetCommon
{
	// Token: 0x04001586 RID: 5510
	public bool isNullObj;

	// Token: 0x04001587 RID: 5511
	public int m_nHeroNo;

	// Token: 0x04001588 RID: 5512
	public int m_nModelIndex;

	// Token: 0x04001589 RID: 5513
	public int m_nXingTai;

	// Token: 0x0400158A RID: 5514
	public int m_nWeaponGroup;

	// Token: 0x0400158B RID: 5515
	public ArrayList aryBulletCartridgeID = new ArrayList();

	// Token: 0x0400158C RID: 5516
	public ArrayList aryBulletGemData = new ArrayList();

	// Token: 0x0400158D RID: 5517
	public ArrayList aryCartridgePublicNow = new ArrayList();

	// Token: 0x0400158E RID: 5518
	public ArrayList aryHeroSpecialData = new ArrayList();

	// Token: 0x0400158F RID: 5519
	public ArrayList aryHeroGemData = new ArrayList();

	// Token: 0x04001590 RID: 5520
	public ArrayList aryheroDazhao;

	// Token: 0x04001591 RID: 5521
	public ArrayList arySkillServerData;

	// Token: 0x04001592 RID: 5522
	public ArrayList aryWarData = new ArrayList();

	// Token: 0x04001593 RID: 5523
	public const float jumpSpeedMax = 5f;

	// Token: 0x04001594 RID: 5524
	public float buffSpeed;
}
