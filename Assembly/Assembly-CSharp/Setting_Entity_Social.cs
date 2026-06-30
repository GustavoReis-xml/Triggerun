using System;

// Token: 0x020002F6 RID: 758
public class Setting_Entity_Social
{
	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x060023E0 RID: 9184 RVA: 0x00147828 File Offset: 0x00145A28
	// (set) Token: 0x060023E1 RID: 9185 RVA: 0x00147840 File Offset: 0x00145A40
	public int Team_Invited_Type
	{
		get
		{
			return this._Team_Invited_Type;
		}
		set
		{
			this._Team_Invited_Type = value;
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0014784C File Offset: 0x00145A4C
	// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00147864 File Offset: 0x00145A64
	public int Team_Apply_Type
	{
		get
		{
			return this._Team_Apply_Type;
		}
		set
		{
			this._Team_Apply_Type = value;
		}
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00147870 File Offset: 0x00145A70
	// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00147888 File Offset: 0x00145A88
	public int Team_Player_Invited_Permission
	{
		get
		{
			return this._Team_Player_Invited_Permission;
		}
		set
		{
			this._Team_Player_Invited_Permission = value;
		}
	}

	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x060023E6 RID: 9190 RVA: 0x00147894 File Offset: 0x00145A94
	// (set) Token: 0x060023E7 RID: 9191 RVA: 0x001478AC File Offset: 0x00145AAC
	public int Display_Offline_Friends
	{
		get
		{
			return this._Display_Offline_Friends;
		}
		set
		{
			this._Display_Offline_Friends = value;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x060023E8 RID: 9192 RVA: 0x001478B8 File Offset: 0x00145AB8
	// (set) Token: 0x060023E9 RID: 9193 RVA: 0x001478D0 File Offset: 0x00145AD0
	public int Auto_Accept_Add_Friend_Apply
	{
		get
		{
			return this._Auto_Accept_Add_Friend_Apply;
		}
		set
		{
			this._Auto_Accept_Add_Friend_Apply = value;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x060023EA RID: 9194 RVA: 0x001478DC File Offset: 0x00145ADC
	// (set) Token: 0x060023EB RID: 9195 RVA: 0x001478F4 File Offset: 0x00145AF4
	public int Friends_Online_Notice
	{
		get
		{
			return this._Friends_Online_Notice;
		}
		set
		{
			this._Friends_Online_Notice = value;
		}
	}

	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x060023EC RID: 9196 RVA: 0x00147900 File Offset: 0x00145B00
	// (set) Token: 0x060023ED RID: 9197 RVA: 0x00147918 File Offset: 0x00145B18
	public int Loading_Background_Image
	{
		get
		{
			return this._Loading_Background_Image;
		}
		set
		{
			this._Loading_Background_Image = value;
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x060023EE RID: 9198 RVA: 0x00147924 File Offset: 0x00145B24
	// (set) Token: 0x060023EF RID: 9199 RVA: 0x0014793C File Offset: 0x00145B3C
	public int Death_Playback
	{
		get
		{
			return this._Death_Playback;
		}
		set
		{
			this._Death_Playback = value;
			CCameraChange.Instance.openFilm = (this._Death_Playback == 1);
		}
	}

	// Token: 0x04002047 RID: 8263
	private int _Team_Invited_Type;

	// Token: 0x04002048 RID: 8264
	private int _Team_Apply_Type;

	// Token: 0x04002049 RID: 8265
	private int _Team_Player_Invited_Permission;

	// Token: 0x0400204A RID: 8266
	private int _Display_Offline_Friends;

	// Token: 0x0400204B RID: 8267
	private int _Auto_Accept_Add_Friend_Apply;

	// Token: 0x0400204C RID: 8268
	private int _Friends_Online_Notice;

	// Token: 0x0400204D RID: 8269
	private int _Loading_Background_Image;

	// Token: 0x0400204E RID: 8270
	private int _Death_Playback;
}
