using System;

// Token: 0x02000269 RID: 617
public class Npc_Info : PublicInfo
{
	// Token: 0x06001CE5 RID: 7397 RVA: 0x0010AD13 File Offset: 0x00108F13
	protected override void Update()
	{
		base.Update();
		this.CheckDisIK();
	}
}
