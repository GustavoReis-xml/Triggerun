using System;
using System.Collections;

// Token: 0x0200048D RID: 1165
public class UserData
{
	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0018BC6C File Offset: 0x00189E6C
	public NetLiving netLiving
	{
		get
		{
			bool flag = this._netliving == null;
			NetLiving netliving;
			if (flag)
			{
				this._netliving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(this.m_nNetid);
				netliving = this._netliving;
			}
			else
			{
				netliving = this._netliving;
			}
			return netliving;
		}
	}

	// Token: 0x06002D01 RID: 11521 RVA: 0x0018BCBC File Offset: 0x00189EBC
	public void InitUserData(ArrayList arr)
	{
		this.m_sNick = arr[0].ToString();
		this.m_nLevel = (int)arr[1];
		this.m_nAvator = (int)arr[2];
		this.m_nShili = (int)arr[3];
		this.m_nIsReady = (int)arr[4];
		this.m_nSex = (int)arr[5];
		this.m_nHeroID = (int)arr[6];
		this.m_nCid = (int)arr[7];
		this.m_nAutoID = (int)arr[8];
		this.m_nIsChsok = (int)arr[9];
		this.m_nFace = (int)arr[10];
		this.m_sServername = arr[11].ToString();
		this.m_nTeamID = (int)arr[12];
		this.m_szHeroInfo = (ArrayList)arr[13];
		this.m_nNetid = (int)arr[14];
		this.m_nIsMiJoin = (int)arr[15];
		this.m_nCaptain = (int)arr[17];
		this.m_szQQdata = (ArrayList)arr[18];
		this.m_szSkyData = (ArrayList)arr[19];
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x0018BE2C File Offset: 0x0018A02C
	public int GetUseEquipId(int ntype, int isdef = 0)
	{
		int num = 1;
		bool flag = isdef == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			for (int i = 0; i < this.m_szHeroInfo.Count; i += 5)
			{
				bool flag2 = (int)this.m_szHeroInfo[i] == this.m_nHeroID;
				if (flag2)
				{
					num = (int)this.m_szHeroInfo[i + ntype];
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x04002625 RID: 9765
	public string m_sNick;

	// Token: 0x04002626 RID: 9766
	public int m_nLevel;

	// Token: 0x04002627 RID: 9767
	public int m_nAvator;

	// Token: 0x04002628 RID: 9768
	public int m_nShili;

	// Token: 0x04002629 RID: 9769
	public int m_nIsReady;

	// Token: 0x0400262A RID: 9770
	public int m_nSex;

	// Token: 0x0400262B RID: 9771
	public int m_nHeroID;

	// Token: 0x0400262C RID: 9772
	public int m_nCid;

	// Token: 0x0400262D RID: 9773
	public int m_nAutoID;

	// Token: 0x0400262E RID: 9774
	public int m_nIsChsok;

	// Token: 0x0400262F RID: 9775
	public int m_nFace;

	// Token: 0x04002630 RID: 9776
	public string m_sServername;

	// Token: 0x04002631 RID: 9777
	public int m_nTeamID;

	// Token: 0x04002632 RID: 9778
	public int m_nNetid;

	// Token: 0x04002633 RID: 9779
	public int m_nIsMiJoin;

	// Token: 0x04002634 RID: 9780
	public int m_nCaptain;

	// Token: 0x04002635 RID: 9781
	public ArrayList m_szHeroInfo;

	// Token: 0x04002636 RID: 9782
	public ArrayList m_szQQdata;

	// Token: 0x04002637 RID: 9783
	public ArrayList m_szSkyData = new ArrayList();

	// Token: 0x04002638 RID: 9784
	private NetLiving _netliving;
}
