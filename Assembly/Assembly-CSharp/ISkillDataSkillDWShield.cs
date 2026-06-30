using System;

// Token: 0x020001FD RID: 509
public class ISkillDataSkillDWShield : ISkillData
{
	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x000CF594 File Offset: 0x000CD794
	// (set) Token: 0x0600164B RID: 5707 RVA: 0x000CF5AC File Offset: 0x000CD7AC
	public float Shield_Time
	{
		get
		{
			return this._ShiTime;
		}
		set
		{
			this._ShiTime = value;
		}
	}

	// Token: 0x04001148 RID: 4424
	private float _ShiTime = 3f;
}
