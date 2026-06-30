using System;
using System.Collections;

// Token: 0x0200020A RID: 522
public class KingGameInfo
{
	// Token: 0x060016CA RID: 5834 RVA: 0x000D3CD1 File Offset: 0x000D1ED1
	public KingGameInfo(ArrayList szData, int nType)
	{
		this.OnSetData(szData);
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000D3CF5 File Offset: 0x000D1EF5
	public void OnRoundEnd(ArrayList szData)
	{
		this.m_nFightState = -1;
		this.m_nRoundScore = (int)szData[2];
		this.m_nRoundIsOk = (int)szData[3];
		this.m_nNpcState = (int)szData[4];
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000D3D35 File Offset: 0x000D1F35
	public void OnGameOver()
	{
		this.m_nGameState = -1;
		this.m_nFightState = -1;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000D3D48 File Offset: 0x000D1F48
	public void OnSetData(ArrayList szData)
	{
		this.m_nGameState = (int)szData[0];
		this.m_nFightState = (int)szData[1];
		this.m_sRoundEndTime = szData[3].ToString();
		this.m_sReadyEndTime = szData[5].ToString();
		this.m_nRound = (int)szData[6];
		this.m_nMaxRound = (int)szData[7];
		this.m_nMonsterNum = (int)szData[8];
		this.m_sGameEndTime = szData[9].ToString();
		this.m_nNpcServerID = (int)szData[10];
		this.m_nGameType = (int)szData[11];
		bool flag = this.m_nGameType == 12;
		if (flag)
		{
			this.m_szOpenDoorData = (ArrayList)szData[12];
			this.m_sGameStart = szData[13].ToString();
		}
		else
		{
			this.m_sGameStart = szData[12].ToString();
		}
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000D3E5A File Offset: 0x000D205A
	public void OnUpdateAllUserDeadState(ArrayList szMsg)
	{
		this.m_nAllUserDeadState = (int)szMsg[0];
		this.m_sAllUserDeadTime = szMsg[1].ToString();
	}

	// Token: 0x040011FA RID: 4602
	public int m_nGameType;

	// Token: 0x040011FB RID: 4603
	public int m_nGameState;

	// Token: 0x040011FC RID: 4604
	public int m_nFightState;

	// Token: 0x040011FD RID: 4605
	public int m_nRound;

	// Token: 0x040011FE RID: 4606
	public int m_nMaxRound;

	// Token: 0x040011FF RID: 4607
	public int m_nMonsterNum;

	// Token: 0x04001200 RID: 4608
	public string m_sRoundEndTime;

	// Token: 0x04001201 RID: 4609
	public string m_sReadyEndTime;

	// Token: 0x04001202 RID: 4610
	public int m_nRoundScore;

	// Token: 0x04001203 RID: 4611
	public string m_sGameEndTime;

	// Token: 0x04001204 RID: 4612
	public int m_nNpcServerID;

	// Token: 0x04001205 RID: 4613
	public int m_nAllUserDeadState;

	// Token: 0x04001206 RID: 4614
	public string m_sAllUserDeadTime;

	// Token: 0x04001207 RID: 4615
	public int m_nRoundIsOk;

	// Token: 0x04001208 RID: 4616
	public int m_nNpcState = 1;

	// Token: 0x04001209 RID: 4617
	public ArrayList m_szOpenDoorData = new ArrayList();

	// Token: 0x0400120A RID: 4618
	public string m_sGameStart;
}
