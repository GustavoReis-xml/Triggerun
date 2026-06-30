using System;

// Token: 0x020002F8 RID: 760
public class Setting_Entity_Voice
{
	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x0600242D RID: 9261 RVA: 0x00148770 File Offset: 0x00146970
	// (set) Token: 0x0600242E RID: 9262 RVA: 0x00148788 File Offset: 0x00146988
	public float Main_Volume
	{
		get
		{
			return this._Main_Volume;
		}
		set
		{
			this._Main_Volume = value;
			CSoundManager.Instance.UpdateMainVol(this._Main_Volume * 0.01f * 0.7f);
		}
	}

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x0600242F RID: 9263 RVA: 0x001487B0 File Offset: 0x001469B0
	// (set) Token: 0x06002430 RID: 9264 RVA: 0x001487C8 File Offset: 0x001469C8
	public float Sound_Volume
	{
		get
		{
			return this._Sound_Volume;
		}
		set
		{
			this._Sound_Volume = value;
			CSoundManager.Instance.UpdateSoundVol(this._Sound_Volume * 0.01f);
		}
	}

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x06002431 RID: 9265 RVA: 0x001487EC File Offset: 0x001469EC
	// (set) Token: 0x06002432 RID: 9266 RVA: 0x00148804 File Offset: 0x00146A04
	public float Music_Volume
	{
		get
		{
			return this._Music_Volume;
		}
		set
		{
			this._Music_Volume = value;
			CSoundManager.Instance.UpdateMusicVol(this._Music_Volume * 0.01f);
		}
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06002433 RID: 9267 RVA: 0x00148828 File Offset: 0x00146A28
	// (set) Token: 0x06002434 RID: 9268 RVA: 0x00148840 File Offset: 0x00146A40
	public float Custom_Voice_Volume
	{
		get
		{
			return this._Custom_Voice_Volume;
		}
		set
		{
			this._Custom_Voice_Volume = value;
			CSoundManager.Instance.UpdateTalkVol(this._Custom_Voice_Volume * 0.01f);
		}
	}

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06002435 RID: 9269 RVA: 0x00148864 File Offset: 0x00146A64
	// (set) Token: 0x06002436 RID: 9270 RVA: 0x0014887C File Offset: 0x00146A7C
	public float Chat_Voice_Volume
	{
		get
		{
			return this._Chat_Voice_Volume;
		}
		set
		{
			this._Chat_Voice_Volume = value;
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06002437 RID: 9271 RVA: 0x00148888 File Offset: 0x00146A88
	// (set) Token: 0x06002438 RID: 9272 RVA: 0x001488A0 File Offset: 0x00146AA0
	public float Microphone_Volume
	{
		get
		{
			return this._Microphone_Volume;
		}
		set
		{
			this._Microphone_Volume = value;
			VoiceGameCon.Inst.SetVolume((int)this._Microphone_Volume);
		}
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x06002439 RID: 9273 RVA: 0x001488BC File Offset: 0x00146ABC
	// (set) Token: 0x0600243A RID: 9274 RVA: 0x001488D4 File Offset: 0x00146AD4
	public int Team_Voice_Chat_Type
	{
		get
		{
			return this._Team_Voice_Chat_Type;
		}
		set
		{
			this._Team_Voice_Chat_Type = value;
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x0600243B RID: 9275 RVA: 0x001488E0 File Offset: 0x00146AE0
	// (set) Token: 0x0600243C RID: 9276 RVA: 0x001488F8 File Offset: 0x00146AF8
	public int Country_Voice_Chat_Type
	{
		get
		{
			return this._Country_Voice_Chat_Type;
		}
		set
		{
			this._Country_Voice_Chat_Type = value;
		}
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x0600243D RID: 9277 RVA: 0x00148904 File Offset: 0x00146B04
	// (set) Token: 0x0600243E RID: 9278 RVA: 0x0014891C File Offset: 0x00146B1C
	public int Voice_Chat_Type
	{
		get
		{
			return this._Voice_Chat_Type;
		}
		set
		{
			this._Voice_Chat_Type = value;
			VoiceGameCon.Inst.SetTalkModel(this.Voice_Chat_Type == 0);
		}
	}

	// Token: 0x0600243F RID: 9279 RVA: 0x0014893C File Offset: 0x00146B3C
	public void Initialization()
	{
		this.Main_Volume = 100f;
		this.Sound_Volume = 100f;
		this.Music_Volume = 100f;
		this.Custom_Voice_Volume = 100f;
		this.Chat_Voice_Volume = 100f;
		this.Microphone_Volume = 100f;
		this.Team_Voice_Chat_Type = 0;
		this.Country_Voice_Chat_Type = 0;
		this.Voice_Chat_Type = 0;
	}

	// Token: 0x0400206A RID: 8298
	private float _Main_Volume = 100f;

	// Token: 0x0400206B RID: 8299
	private float _Sound_Volume = 100f;

	// Token: 0x0400206C RID: 8300
	private float _Music_Volume = 100f;

	// Token: 0x0400206D RID: 8301
	private float _Custom_Voice_Volume = 100f;

	// Token: 0x0400206E RID: 8302
	private float _Chat_Voice_Volume = 100f;

	// Token: 0x0400206F RID: 8303
	private float _Microphone_Volume = 100f;

	// Token: 0x04002070 RID: 8304
	private int _Team_Voice_Chat_Type;

	// Token: 0x04002071 RID: 8305
	private int _Country_Voice_Chat_Type;

	// Token: 0x04002072 RID: 8306
	private int _Voice_Chat_Type;
}
