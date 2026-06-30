using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000286 RID: 646
public class OpenHeroData
{
	// Token: 0x06001DA2 RID: 7586 RVA: 0x00113C34 File Offset: 0x00111E34
	public OpenHeroData(int heroid)
	{
		this.nHeroId = heroid;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(this.nHeroId);
		if (flag)
		{
			this.unlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId];
		}
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x00113C88 File Offset: 0x00111E88
	public void SetData(ArrayList szInfo)
	{
		this.nHeroLvl = (int)szInfo[0];
		this.nHeroExp = (int)szInfo[1];
		this.nOpenState = (int)szInfo[2];
		this.overTime = szInfo[3].ToString();
		this.nChipNum = (int)szInfo[4];
		this.szAward = (ArrayList)szInfo[5];
		this.szSkin = (ArrayList)szInfo[6];
		this.szWeapon = (ArrayList)szInfo[7];
		this.szEmotion = (ArrayList)szInfo[8];
		this.szPose = (ArrayList)szInfo[9];
		this.nUseSkin = (int)szInfo[11];
		this.nUseWeapon = (int)szInfo[12];
		this.nUseEmotion = (int)szInfo[13];
		this.nUsePose = (int)szInfo[14];
		this.nGiftTime = (int)szInfo[15];
		this.nHeroNewType = 0;
		bool flag = this.unlockData != null;
		if (flag)
		{
			this.unlockData.heroNewType = 0;
			this.unlockData.heroLockType = this.nOpenState;
			this.unlockData.SetSkinData(this.szSkin);
			this.unlockData.SetWeaponData(this.szWeapon);
			this.unlockData.SetEmotionData(this.szEmotion);
			this.unlockData.SetPoseData(this.szPose);
			this.nHeroNewType = this.unlockData.heroNewType;
		}
		bool flag2 = this.nOpenState == 1 && this.nHeroNewType < 1;
		if (flag2)
		{
			this.nHeroNewType = 1;
		}
		bool flag3 = this.nOpenState == 2 && this.nHeroNewType < 2;
		if (flag3)
		{
			this.nHeroNewType = 2;
		}
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x00113E7C File Offset: 0x0011207C
	public List<int> GetUseParts(int nId)
	{
		List<int> list = new List<int>();
		ArrayList arrayList = (ArrayList)this.unlockData.szWeaponUnlock[nId].szPartsGrid.Clone();
		for (int i = 0; i < arrayList.Count; i++)
		{
			ArrayList arrayList2 = (ArrayList)arrayList[i];
			bool flag = (int)arrayList2[0] == 1 && (int)arrayList2[1] != 0;
			if (flag)
			{
				list.Add(i);
				list.Add((int)arrayList2[1]);
			}
		}
		return list;
	}

	// Token: 0x04001A90 RID: 6800
	public int nHeroId;

	// Token: 0x04001A91 RID: 6801
	public int nHeroLvl;

	// Token: 0x04001A92 RID: 6802
	public int nHeroExp;

	// Token: 0x04001A93 RID: 6803
	public int nOpenState;

	// Token: 0x04001A94 RID: 6804
	public string overTime;

	// Token: 0x04001A95 RID: 6805
	public int nChipNum;

	// Token: 0x04001A96 RID: 6806
	public int nHeroNewType;

	// Token: 0x04001A97 RID: 6807
	public HeroUnlockData unlockData;

	// Token: 0x04001A98 RID: 6808
	public ArrayList szAward;

	// Token: 0x04001A99 RID: 6809
	public ArrayList szSkin;

	// Token: 0x04001A9A RID: 6810
	public ArrayList szWeapon;

	// Token: 0x04001A9B RID: 6811
	public ArrayList szEmotion;

	// Token: 0x04001A9C RID: 6812
	public ArrayList szPose;

	// Token: 0x04001A9D RID: 6813
	public int nUseSkin;

	// Token: 0x04001A9E RID: 6814
	public int nUseWeapon;

	// Token: 0x04001A9F RID: 6815
	public int nUseEmotion;

	// Token: 0x04001AA0 RID: 6816
	public int nUsePose;

	// Token: 0x04001AA1 RID: 6817
	public int nGiftTime;
}
