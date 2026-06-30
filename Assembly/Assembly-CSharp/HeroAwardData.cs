using System;
using System.Collections;
using System.Xml;

// Token: 0x02000168 RID: 360
public class HeroAwardData
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x0600105E RID: 4190 RVA: 0x00098DFF File Offset: 0x00096FFF
	public int key
	{
		get
		{
			return this.nHeroid;
		}
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00098E08 File Offset: 0x00097008
	public void Init(XmlElement xml)
	{
		this.nHeroid = int.Parse(xml.GetAttribute("heroid"));
		string[] array = xml.GetAttribute("awarddata").Split(new char[]
		{
			'#'
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = !string.IsNullOrEmpty(array[i]);
			if (flag)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				int num = int.Parse(array2[0]);
				int num2 = int.Parse(array2[1]);
				int num3 = int.Parse(array2[2]);
				ArrayList value = new ArrayList
				{
					num,
					num2,
					num3
				};
				this.szAward.Add(value);
			}
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00098EF0 File Offset: 0x000970F0
	public string GetColor(int ntype, int nvalue)
	{
		int nqual = 1;
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroid];
		switch (ntype)
		{
		case 1:
		{
			bool flag = heroUnlockData.szSkinUnlock.ContainsKey(nvalue);
			if (flag)
			{
				nqual = heroUnlockData.szSkinUnlock[nvalue].nQual;
			}
			break;
		}
		case 2:
		{
			bool flag2 = heroUnlockData.szWeaponUnlock.ContainsKey(nvalue);
			if (flag2)
			{
				nqual = heroUnlockData.szWeaponUnlock[nvalue].nQual;
			}
			break;
		}
		case 3:
		{
			bool flag3 = heroUnlockData.szEmotionUnlock.ContainsKey(nvalue);
			if (flag3)
			{
				nqual = heroUnlockData.szEmotionUnlock[nvalue].nQual;
			}
			break;
		}
		case 4:
		{
			bool flag4 = heroUnlockData.szPoseUnlock.ContainsKey(nvalue);
			if (flag4)
			{
				nqual = heroUnlockData.szPoseUnlock[nvalue].nQual;
			}
			break;
		}
		}
		return Fun.GetQualColor(nqual);
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x00098FE4 File Offset: 0x000971E4
	public string GetDesc(int ntype, int nvalue)
	{
		string result = string.Empty;
		HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroid];
		switch (ntype)
		{
		case 0:
			result = Language.GetLang(7003, new object[]
			{
				nvalue
			});
			break;
		case 1:
		{
			bool flag = heroUnlockData.szSkinUnlock.ContainsKey(nvalue);
			if (flag)
			{
				result = heroUnlockData.szSkinUnlock[nvalue].sName;
			}
			break;
		}
		case 2:
		{
			bool flag2 = heroUnlockData.szWeaponUnlock.ContainsKey(nvalue);
			if (flag2)
			{
				result = heroUnlockData.szWeaponUnlock[nvalue].sName;
			}
			break;
		}
		case 3:
		{
			bool flag3 = heroUnlockData.szEmotionUnlock.ContainsKey(nvalue);
			if (flag3)
			{
				result = heroUnlockData.szEmotionUnlock[nvalue].sName;
			}
			break;
		}
		case 4:
		{
			bool flag4 = heroUnlockData.szPoseUnlock.ContainsKey(nvalue);
			if (flag4)
			{
				result = heroUnlockData.szPoseUnlock[nvalue].sName;
			}
			break;
		}
		case 5:
		{
			bool flag5 = fGameManager.mloadConfig.AllFaceData.ContainsKey(nvalue);
			if (flag5)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nvalue];
				result = playerFaceData.sFaceName;
			}
			break;
		}
		case 6:
		{
			PlayerPaintData playerPaintData = fGameManager.mloadConfig.AllPaintData[nvalue];
			result = playerPaintData.sName;
			break;
		}
		}
		return result;
	}

	// Token: 0x04000D19 RID: 3353
	public int nHeroid;

	// Token: 0x04000D1A RID: 3354
	public ArrayList szAward = new ArrayList();
}
