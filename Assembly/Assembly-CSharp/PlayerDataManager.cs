using System;

// Token: 0x02000292 RID: 658
public class PlayerDataManager
{
	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00117941 File Offset: 0x00115B41
	public PlayerCommon playerCommon
	{
		get
		{
			return this.playercom;
		}
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x00117949 File Offset: 0x00115B49
	public PlayerDataManager()
	{
		this.playercom = new PlayerCommon();
		this.playerdet = new PlayerDetail();
		this.HUserData = new HallUserData();
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x00117974 File Offset: 0x00115B74
	public static int GetSettingsData(int type)
	{
		return 1;
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00117988 File Offset: 0x00115B88
	public static PlayerDataManager getInstance()
	{
		bool flag = PlayerDataManager.instance == null;
		if (flag)
		{
			PlayerDataManager.instance = new PlayerDataManager();
		}
		return PlayerDataManager.instance;
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x001179B8 File Offset: 0x00115BB8
	public static string GetSetName()
	{
		bool flag = PlayerDataManager.instance != null;
		if (flag)
		{
			bool flag2 = PlayerDataManager.instance.HUserData != null;
			if (flag2)
			{
				return PlayerDataManager.instance.HUserData.m_sName;
			}
			bool flag3 = PlayerDataManager.instance.playercom != null;
			if (flag3)
			{
				return PlayerDataManager.instance.playercom.m_sName;
			}
		}
		return string.Empty;
	}

	// Token: 0x04001B69 RID: 7017
	private static PlayerDataManager instance;

	// Token: 0x04001B6A RID: 7018
	public PlayerCommon playercom;

	// Token: 0x04001B6B RID: 7019
	public PlayerDetail playerdet;

	// Token: 0x04001B6C RID: 7020
	public HallUserData HUserData;
}
