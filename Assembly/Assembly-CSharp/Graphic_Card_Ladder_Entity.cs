using System;

// Token: 0x02000159 RID: 345
public class Graphic_Card_Ladder_Entity
{
	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00090630 File Offset: 0x0008E830
	// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00090648 File Offset: 0x0008E848
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00090654 File Offset: 0x0008E854
	// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x0009066C File Offset: 0x0008E86C
	public float Score
	{
		get
		{
			return this._Score;
		}
		set
		{
			this._Score = value;
		}
	}

	// Token: 0x04000C6E RID: 3182
	private string _Name = string.Empty;

	// Token: 0x04000C6F RID: 3183
	private float _Score;
}
