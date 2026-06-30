using System;

// Token: 0x020002F0 RID: 752
public class SettingCon : Window
{
	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x0600230A RID: 8970 RVA: 0x00143368 File Offset: 0x00141568
	// (set) Token: 0x0600230B RID: 8971 RVA: 0x00143380 File Offset: 0x00141580
	public bool Modify_Key_State
	{
		get
		{
			return this._Modify_Key_State;
		}
		set
		{
			this._Modify_Key_State = value;
		}
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x0014338A File Offset: 0x0014158A
	protected override void Awake()
	{
		base.Awake();
		SettingCon.Instance = this;
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x0014339C File Offset: 0x0014159C
	public override void Close()
	{
		GameRoot.inst.Setting.Use_Video_Setting();
		bool flag = fGameManager.m_nUserType == 1 && CameraControl.Instance != null;
		if (flag)
		{
			HeroPlayer.isMouseShow = false;
			HeroPlayer.isPlayerControl = true;
			GameRoot.inst.Setting.Use_Setting(fGameManager.GetInstance().getPlayer().playerCommon.m_nModelIndex);
		}
		else
		{
			GameRoot.inst.Setting.Use_Setting(0);
		}
		GameRoot.inst.Setting.Save_Data();
		base.Close();
	}

	// Token: 0x04001FFB RID: 8187
	public static SettingCon Instance;

	// Token: 0x04001FFC RID: 8188
	private bool _Modify_Key_State;
}
