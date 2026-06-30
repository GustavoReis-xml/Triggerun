using System;

// Token: 0x020001F4 RID: 500
public class IHeroDataLingLong : IHeroData
{
	// Token: 0x170002BD RID: 701
	// (get) Token: 0x0600160D RID: 5645 RVA: 0x000CDC04 File Offset: 0x000CBE04
	// (set) Token: 0x0600160E RID: 5646 RVA: 0x000CDC1C File Offset: 0x000CBE1C
	public int Transmission_Durability
	{
		get
		{
			return this._Transmission_Durability;
		}
		set
		{
			this._Transmission_Durability = value;
		}
	}

	// Token: 0x0400111D RID: 4381
	private int _Transmission_Durability;
}
