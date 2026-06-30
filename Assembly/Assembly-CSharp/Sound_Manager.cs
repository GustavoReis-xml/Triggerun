using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class Sound_Manager
{
	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00176160 File Offset: 0x00174360
	public static Sound_Manager Instantiation
	{
		get
		{
			bool flag = Sound_Manager._Instantiation == null;
			if (flag)
			{
				Sound_Manager._Instantiation = new Sound_Manager();
			}
			return Sound_Manager._Instantiation;
		}
	}

	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06002A58 RID: 10840 RVA: 0x0017618F File Offset: 0x0017438F
	public float Chariot_Forward_Delay_Time
	{
		get
		{
			return this._Chariot_Forward_Delay_Time;
		}
	}

	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00176197 File Offset: 0x00174397
	public float Chariot_Backward_Delay_Time
	{
		get
		{
			return this._Chariot_Backward_Delay_Time;
		}
	}

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06002A5A RID: 10842 RVA: 0x0017619F File Offset: 0x0017439F
	public float Chariot_Fight_Delay_Time
	{
		get
		{
			return this._Chariot_Fight_Delay_Time;
		}
	}

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06002A5B RID: 10843 RVA: 0x001761A8 File Offset: 0x001743A8
	// (set) Token: 0x06002A5C RID: 10844 RVA: 0x001761C0 File Offset: 0x001743C0
	public float Chariot_Forward_Play_Time
	{
		get
		{
			return this._Chariot_Forward_Play_Time;
		}
		set
		{
			this._Chariot_Forward_Play_Time = value;
		}
	}

	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x06002A5D RID: 10845 RVA: 0x001761CC File Offset: 0x001743CC
	// (set) Token: 0x06002A5E RID: 10846 RVA: 0x001761E4 File Offset: 0x001743E4
	public float Chariot_Backward_Play_Time
	{
		get
		{
			return this._Chariot_Backward_Play_Time;
		}
		set
		{
			this._Chariot_Backward_Play_Time = value;
		}
	}

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06002A5F RID: 10847 RVA: 0x001761F0 File Offset: 0x001743F0
	// (set) Token: 0x06002A60 RID: 10848 RVA: 0x00176208 File Offset: 0x00174408
	public float Chariot_Fight_Play_Time
	{
		get
		{
			return this._Chariot_Fight_Play_Time;
		}
		set
		{
			this._Chariot_Fight_Play_Time = value;
		}
	}

	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06002A61 RID: 10849 RVA: 0x00176212 File Offset: 0x00174412
	public float Scramble_Prepare_Occupy_Delay_Time
	{
		get
		{
			return this._Scramble_Prepare_Occupy_Delay_Time;
		}
	}

	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0017621A File Offset: 0x0017441A
	public float Scramble_Had_Occupy_Delay_Time
	{
		get
		{
			return this._Scramble_Had_Occupy_Delay_Time;
		}
	}

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06002A63 RID: 10851 RVA: 0x00176224 File Offset: 0x00174424
	// (set) Token: 0x06002A64 RID: 10852 RVA: 0x0017623C File Offset: 0x0017443C
	public float Scramble_Prepare_Occupy_Play_Time
	{
		get
		{
			return this._Scramble_Prepare_Occupy_Play_Time;
		}
		set
		{
			this._Scramble_Prepare_Occupy_Play_Time = value;
		}
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x06002A65 RID: 10853 RVA: 0x00176248 File Offset: 0x00174448
	// (set) Token: 0x06002A66 RID: 10854 RVA: 0x00176260 File Offset: 0x00174460
	public float Scramble_Had_Occupy_Play_Time
	{
		get
		{
			return this._Scramble_Had_Occupy_Play_Time;
		}
		set
		{
			this._Scramble_Had_Occupy_Play_Time = value;
		}
	}

	// Token: 0x06002A67 RID: 10855 RVA: 0x0017626C File Offset: 0x0017446C
	public void Play_Sound(string _Type, string _Tag, string _Name)
	{
		if (!(_Type == "Chariot"))
		{
			if (_Type == "Scramble")
			{
				this.Scramble_Handle(_Tag, _Name);
			}
		}
		else
		{
			this.Chariot_Handle(_Tag, _Name);
		}
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x001762B0 File Offset: 0x001744B0
	private void Chariot_Handle(string _Tag, string _Name)
	{
		if (!(_Tag == "Forward"))
		{
			if (!(_Tag == "Backward"))
			{
				if (_Tag == "Fight")
				{
					bool flag = this.Chariot_Fight_Play_Time == 0f || Time.time > this.Chariot_Fight_Play_Time;
					if (!flag)
					{
						return;
					}
					this.Chariot_Fight_Play_Time = Time.time + this.Chariot_Fight_Delay_Time;
				}
			}
			else
			{
				bool flag2 = this.Chariot_Backward_Play_Time == 0f || Time.time > this.Chariot_Backward_Play_Time;
				if (!flag2)
				{
					return;
				}
				this.Chariot_Backward_Play_Time = Time.time + this.Chariot_Backward_Delay_Time;
			}
		}
		else
		{
			bool flag3 = this.Chariot_Forward_Play_Time == 0f || Time.time > this.Chariot_Forward_Play_Time;
			if (!flag3)
			{
				return;
			}
			this.Chariot_Forward_Play_Time = Time.time + this.Chariot_Forward_Delay_Time;
		}
		fUISoundManager.instance.PlayUISound(_Name, 0, null, 20);
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x001763B0 File Offset: 0x001745B0
	private void Scramble_Handle(string _Tag, string _Name)
	{
		if (!(_Tag == "Prepare_Occupy"))
		{
			if (_Tag == "Had_Occupy")
			{
				bool flag = this.Scramble_Had_Occupy_Play_Time == 0f || Time.time > this.Scramble_Had_Occupy_Play_Time;
				if (!flag)
				{
					return;
				}
				this.Scramble_Had_Occupy_Play_Time = Time.time + this.Scramble_Had_Occupy_Delay_Time;
			}
		}
		else
		{
			bool flag2 = this.Scramble_Prepare_Occupy_Play_Time == 0f || Time.time > this.Scramble_Prepare_Occupy_Play_Time;
			if (!flag2)
			{
				return;
			}
			this.Scramble_Prepare_Occupy_Play_Time = Time.time + this.Scramble_Prepare_Occupy_Delay_Time;
		}
		fUISoundManager.instance.PlayUISound(_Name, 0, null, 20);
	}

	// Token: 0x0400240B RID: 9227
	private static Sound_Manager _Instantiation;

	// Token: 0x0400240C RID: 9228
	private float _Chariot_Forward_Delay_Time = 15f;

	// Token: 0x0400240D RID: 9229
	private float _Chariot_Backward_Delay_Time = 15f;

	// Token: 0x0400240E RID: 9230
	private float _Chariot_Fight_Delay_Time = 15f;

	// Token: 0x0400240F RID: 9231
	private float _Chariot_Forward_Play_Time;

	// Token: 0x04002410 RID: 9232
	private float _Chariot_Backward_Play_Time;

	// Token: 0x04002411 RID: 9233
	private float _Chariot_Fight_Play_Time;

	// Token: 0x04002412 RID: 9234
	private float _Scramble_Prepare_Occupy_Delay_Time = 15f;

	// Token: 0x04002413 RID: 9235
	private float _Scramble_Had_Occupy_Delay_Time = 15f;

	// Token: 0x04002414 RID: 9236
	private float _Scramble_Prepare_Occupy_Play_Time;

	// Token: 0x04002415 RID: 9237
	private float _Scramble_Had_Occupy_Play_Time;
}
