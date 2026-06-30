using System;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class zhunxingYinglong : zhunxingBase
{
	// Token: 0x06002F40 RID: 12096 RVA: 0x001A5D13 File Offset: 0x001A3F13
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.superdjs = gotherui.FindChild("superdjs").gameObject;
		this.superdjs.SetActive(false);
	}

	// Token: 0x040028EE RID: 10478
	private GameObject superdjs;
}
