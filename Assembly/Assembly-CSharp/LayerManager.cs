using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000215 RID: 533
public class LayerManager : MonoBehaviour
{
	// Token: 0x0600171C RID: 5916 RVA: 0x000D6D84 File Offset: 0x000D4F84
	public static int bulletNotShowLayer()
	{
		bool flag = LayerManager.notShowBulletLayer != 0;
		int result;
		if (flag)
		{
			result = LayerManager.notShowBulletLayer;
		}
		else
		{
			LayerManager.notShowBulletLayer = (1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("shield1") | 1 << LayerMask.NameToLayer("shield2"));
			result = LayerManager.notShowBulletLayer;
		}
		return result;
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000D6DF4 File Offset: 0x000D4FF4
	public static int bulletChangeParentLayer()
	{
		bool flag = LayerManager.changeParentBulletLayer != 0;
		int result;
		if (flag)
		{
			result = LayerManager.changeParentBulletLayer;
		}
		else
		{
			LayerManager.changeParentBulletLayer = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
			result = LayerManager.changeParentBulletLayer;
		}
		return result;
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000D6E64 File Offset: 0x000D5064
	public static int bulletLayer()
	{
		bool flag = LayerManager._bulletLayer != 0;
		int bulletLayer;
		if (flag)
		{
			bulletLayer = LayerManager._bulletLayer;
		}
		else
		{
			LayerManager._bulletLayer = 25174016;
			bulletLayer = LayerManager._bulletLayer;
		}
		return bulletLayer;
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000D6E9C File Offset: 0x000D509C
	public static int getBlockLayer(int force)
	{
		bool flag = force == 1;
		int result;
		if (flag)
		{
			bool flag2 = LayerManager._bockLayer1 != 0;
			if (flag2)
			{
				result = LayerManager._bockLayer1;
			}
			else
			{
				LayerManager._bockLayer1 = 539235329;
				result = LayerManager._bockLayer1;
			}
		}
		else
		{
			bool flag3 = LayerManager._bockLayer2 != 0;
			if (flag3)
			{
				result = LayerManager._bockLayer2;
			}
			else
			{
				LayerManager._bockLayer2 = 269748737;
				result = LayerManager._bockLayer2;
			}
		}
		return result;
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x000D6F04 File Offset: 0x000D5104
	public static int getMainPlayerBulletLayer(int attacktype, int force)
	{
		int num;
		switch (attacktype)
		{
		case 1:
		{
			bool flag = force == 1;
			if (flag)
			{
				bool flag2 = LayerManager.layerMainPlayerMaskBullet[1] != 0;
				if (flag2)
				{
					return LayerManager.layerMainPlayerMaskBullet[1];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("shield2"));
				LayerManager.layerMainPlayerMaskBullet[1] = num;
			}
			else
			{
				bool flag3 = LayerManager.layerMainPlayerMaskBullet[2] != 0;
				if (flag3)
				{
					return LayerManager.layerMainPlayerMaskBullet[2];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("shield1"));
				LayerManager.layerMainPlayerMaskBullet[2] = num;
			}
			break;
		}
		case 2:
		{
			bool flag4 = force == 1;
			if (flag4)
			{
				bool flag5 = LayerManager.layerMainPlayerMaskBullet[3] != 0;
				if (flag5)
				{
					return LayerManager.layerMainPlayerMaskBullet[3];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("shield1"));
				LayerManager.layerMainPlayerMaskBullet[3] = num;
			}
			else
			{
				bool flag6 = LayerManager.layerMainPlayerMaskBullet[4] != 0;
				if (flag6)
				{
					return LayerManager.layerMainPlayerMaskBullet[4];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("shield2"));
				LayerManager.layerMainPlayerMaskBullet[4] = num;
			}
			break;
		}
		case 3:
		{
			bool flag7 = force == 1;
			if (flag7)
			{
				bool flag8 = LayerManager.layerMainPlayerMaskBullet[5] != 0;
				if (flag8)
				{
					return LayerManager.layerMainPlayerMaskBullet[5];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("shield2") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
				LayerManager.layerMainPlayerMaskBullet[5] = num;
			}
			else
			{
				bool flag9 = LayerManager.layerMainPlayerMaskBullet[6] != 0;
				if (flag9)
				{
					return LayerManager.layerMainPlayerMaskBullet[6];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("shield1") | 1 << LayerMask.NameToLayer("force1_mainplayer"));
				LayerManager.layerMainPlayerMaskBullet[6] = num;
			}
			break;
		}
		case 4:
		{
			bool flag10 = force == 1;
			if (flag10)
			{
				bool flag11 = LayerManager.layerMainPlayerMaskBullet[7] != 0;
				if (flag11)
				{
					return LayerManager.layerMainPlayerMaskBullet[7];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player"));
				LayerManager.layerMainPlayerMaskBullet[7] = num;
			}
			else
			{
				bool flag12 = LayerManager.layerMainPlayerMaskBullet[8] != 0;
				if (flag12)
				{
					return LayerManager.layerMainPlayerMaskBullet[8];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player"));
				LayerManager.layerMainPlayerMaskBullet[8] = num;
			}
			break;
		}
		case 5:
		{
			bool flag13 = force == 1;
			if (flag13)
			{
				bool flag14 = LayerManager.layerMainPlayerMaskBullet[9] != 0;
				if (flag14)
				{
					return LayerManager.layerMainPlayerMaskBullet[9];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_2"));
				LayerManager.layerMainPlayerMaskBullet[9] = num;
			}
			else
			{
				bool flag15 = LayerManager.layerMainPlayerMaskBullet[10] != 0;
				if (flag15)
				{
					return LayerManager.layerMainPlayerMaskBullet[10];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1"));
				LayerManager.layerMainPlayerMaskBullet[10] = num;
			}
			break;
		}
		default:
		{
			bool flag16 = LayerManager.layerMainPlayerMaskBullet[0] != 0;
			if (flag16)
			{
				return LayerManager.layerMainPlayerMaskBullet[0];
			}
			num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("shield1") | 1 << LayerMask.NameToLayer("shield2"));
			LayerManager.layerMainPlayerMaskBullet[0] = num;
			break;
		}
		}
		return num;
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x000D7610 File Offset: 0x000D5810
	public static int getBulletFightLayer(int attacktype, int force)
	{
		int result;
		switch (attacktype)
		{
		case 1:
		case 3:
		{
			bool flag = force == 1;
			if (flag)
			{
				result = LayerMask.NameToLayer("fight_1");
			}
			else
			{
				result = LayerMask.NameToLayer("fight_2");
			}
			break;
		}
		case 2:
		case 4:
		{
			bool flag2 = force == 1;
			if (flag2)
			{
				result = LayerMask.NameToLayer("fight_2");
			}
			else
			{
				result = LayerMask.NameToLayer("fight_1");
			}
			break;
		}
		case 5:
			result = LayerMask.NameToLayer("fight");
			break;
		default:
			result = LayerMask.NameToLayer("fight");
			break;
		}
		return result;
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000D76A4 File Offset: 0x000D58A4
	public static int getPhysicalBulletLayer(int attacktype, int force)
	{
		int result;
		switch (attacktype)
		{
		case 1:
		case 3:
		{
			bool flag = force == 1;
			if (flag)
			{
				result = LayerMask.NameToLayer("bullet_1");
			}
			else
			{
				result = LayerMask.NameToLayer("bullet_2");
			}
			break;
		}
		case 2:
		case 4:
		{
			bool flag2 = force == 1;
			if (flag2)
			{
				result = LayerMask.NameToLayer("bullet_2");
			}
			else
			{
				result = LayerMask.NameToLayer("bullet_1");
			}
			break;
		}
		case 5:
			result = LayerMask.NameToLayer("Ignore Raycast");
			break;
		default:
			result = LayerMask.NameToLayer("bullet");
			break;
		}
		return result;
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000D7738 File Offset: 0x000D5938
	public static int getMiaoZhunLayer(int force)
	{
		int num;
		if (force != 1)
		{
			if (force != 2)
			{
				bool flag = LayerManager.layerMaskBulletNum[0] != 0;
				if (flag)
				{
					return LayerManager.layerMaskBulletNum[0];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player"));
				LayerManager.layerMaskBulletNum[0] = num;
			}
			else
			{
				bool flag2 = LayerManager.layerMaskBulletNum[2] != 0;
				if (flag2)
				{
					return LayerManager.layerMaskBulletNum[2];
				}
				num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player"));
				LayerManager.layerMaskBulletNum[2] = num;
			}
		}
		else
		{
			bool flag3 = LayerManager.layerMaskBulletNum[1] != 0;
			if (flag3)
			{
				return LayerManager.layerMaskBulletNum[1];
			}
			num = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player"));
			LayerManager.layerMaskBulletNum[1] = num;
		}
		return num;
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x000D7890 File Offset: 0x000D5A90
	public static int GetMainAllLayer()
	{
		bool flag = LayerManager.nMainPlayer != -999;
		int result;
		if (flag)
		{
			result = LayerManager.nMainPlayer;
		}
		else
		{
			LayerManager.nMainPlayer = (1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
			result = LayerManager.nMainPlayer;
		}
		return result;
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000D78E8 File Offset: 0x000D5AE8
	public static int GetPlayerAllLayer()
	{
		bool flag = LayerManager.nStaticAllPlayer != -999;
		int result;
		if (flag)
		{
			result = LayerManager.nStaticAllPlayer;
		}
		else
		{
			LayerManager.nStaticAllPlayer = (1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player"));
			result = LayerManager.nStaticAllPlayer;
		}
		return result;
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000D7960 File Offset: 0x000D5B60
	public static int GetLivingAllLayer()
	{
		bool flag = LayerManager.nStaticAllLiving != -999;
		int result;
		if (flag)
		{
			result = LayerManager.nStaticAllLiving;
		}
		else
		{
			LayerManager.nStaticAllLiving = (1 << LayerMask.NameToLayer("mainplayer") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
			result = LayerManager.nStaticAllLiving;
		}
		return result;
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000D79E8 File Offset: 0x000D5BE8
	public static int JustEnemyPlayerLayer(int iForce)
	{
		int result = 0;
		if (iForce != 1)
		{
			if (iForce == 2)
			{
				bool flag = LayerManager.nStaticForceTwo != -999;
				if (flag)
				{
					return LayerManager.nStaticForceTwo;
				}
				LayerManager.nStaticForceTwo = (1 << LayerMask.NameToLayer("force1_player"));
				result = LayerManager.nStaticForceTwo;
			}
		}
		else
		{
			bool flag2 = LayerManager.nStaticForceOne != -999;
			if (flag2)
			{
				return LayerManager.nStaticForceOne;
			}
			LayerManager.nStaticForceOne = (1 << LayerMask.NameToLayer("force2_player"));
			result = LayerManager.nStaticForceOne;
		}
		return result;
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
	public static int EnemyLayer(int iForce)
	{
		int result = 0;
		if (iForce != 1)
		{
			if (iForce == 2)
			{
				bool flag = LayerManager.nStaticPlayerForceTwo != -999;
				if (flag)
				{
					return LayerManager.nStaticPlayerForceTwo;
				}
				LayerManager.nStaticPlayerForceTwo = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc"));
				result = LayerManager.nStaticPlayerForceTwo;
			}
		}
		else
		{
			bool flag2 = LayerManager.nStaticPlayerForceOne != -999;
			if (flag2)
			{
				return LayerManager.nStaticPlayerForceOne;
			}
			LayerManager.nStaticPlayerForceOne = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc"));
			result = LayerManager.nStaticPlayerForceOne;
		}
		return result;
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000D7B84 File Offset: 0x000D5D84
	public static int FriendLayer(int iForce)
	{
		int result = 0;
		if (iForce != 1)
		{
			if (iForce == 2)
			{
				bool flag = LayerManager.nStaticFriendTwo != -999;
				if (flag)
				{
					return LayerManager.nStaticFriendTwo;
				}
				LayerManager.nStaticFriendTwo = (1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_player"));
				result = LayerManager.nStaticFriendTwo;
			}
		}
		else
		{
			bool flag2 = LayerManager.nStaticFriendOne != -999;
			if (flag2)
			{
				return LayerManager.nStaticFriendOne;
			}
			LayerManager.nStaticFriendOne = (1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_player"));
			result = LayerManager.nStaticFriendOne;
		}
		return result;
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000D7C64 File Offset: 0x000D5E64
	public static bool CanDamage(int iAttackForce, bool bAttackPlayer, int iTargetForce, bool isTargetPlayer)
	{
		string str = (!bAttackPlayer) ? ("M" + iAttackForce.ToString()) : ("P" + iAttackForce.ToString());
		string str2 = (!isTargetPlayer) ? ("M" + iTargetForce.ToString()) : ("P" + iTargetForce.ToString());
		string key = str + "vs" + str2;
		return LayerManager.canDamageDict[key];
	}

	// Token: 0x04001249 RID: 4681
	public static int layerMaskBullet;

	// Token: 0x0400124A RID: 4682
	public static int[] layerMainPlayerMaskBullet = new int[12];

	// Token: 0x0400124B RID: 4683
	public static int[] layerMaskBulletNum = new int[3];

	// Token: 0x0400124C RID: 4684
	private static int notShowBulletLayer;

	// Token: 0x0400124D RID: 4685
	private static int changeParentBulletLayer;

	// Token: 0x0400124E RID: 4686
	private static int _bulletLayer;

	// Token: 0x0400124F RID: 4687
	private static int _bockLayer1;

	// Token: 0x04001250 RID: 4688
	private static int _bockLayer2;

	// Token: 0x04001251 RID: 4689
	private static int nMainPlayer = -999;

	// Token: 0x04001252 RID: 4690
	private static int nStaticAllPlayer = -999;

	// Token: 0x04001253 RID: 4691
	private static int nStaticAllLiving = -999;

	// Token: 0x04001254 RID: 4692
	private static int nStaticForceOne = -999;

	// Token: 0x04001255 RID: 4693
	private static int nStaticForceTwo = -999;

	// Token: 0x04001256 RID: 4694
	private static int nStaticPlayerForceOne = -999;

	// Token: 0x04001257 RID: 4695
	private static int nStaticPlayerForceTwo = -999;

	// Token: 0x04001258 RID: 4696
	private static int nStaticFriendOne = -999;

	// Token: 0x04001259 RID: 4697
	private static int nStaticFriendTwo = -999;

	// Token: 0x0400125A RID: 4698
	public static Dictionary<string, bool> canDamageDict = new Dictionary<string, bool>
	{
		{
			"M-3vsM-3",
			true
		},
		{
			"M-3vsM-2",
			true
		},
		{
			"M-3vsM-1",
			true
		},
		{
			"M-3vsM0",
			true
		},
		{
			"M-3vsM1",
			true
		},
		{
			"M-3vsM2",
			true
		},
		{
			"M-3vsM3",
			true
		},
		{
			"M-3vsP0",
			true
		},
		{
			"M-3vsP1",
			true
		},
		{
			"M-3vsP2",
			true
		},
		{
			"M-3vsP3",
			true
		},
		{
			"M-2vsM-3",
			false
		},
		{
			"M-2vsM-2",
			false
		},
		{
			"M-2vsM-1",
			false
		},
		{
			"M-2vsM0",
			false
		},
		{
			"M-2vsM1",
			false
		},
		{
			"M-2vsM2",
			false
		},
		{
			"M-2vsM3",
			false
		},
		{
			"M-2vsP0",
			true
		},
		{
			"M-2vsP1",
			true
		},
		{
			"M-2vsP2",
			true
		},
		{
			"M-2vsP3",
			true
		},
		{
			"M-1vsM-3",
			true
		},
		{
			"M-1vsM-2",
			true
		},
		{
			"M-1vsM-1",
			false
		},
		{
			"M-1vsM0",
			true
		},
		{
			"M-1vsM1",
			true
		},
		{
			"M-1vsM2",
			true
		},
		{
			"M-1vsM3",
			true
		},
		{
			"M-1vsP0",
			false
		},
		{
			"M-1vsP1",
			false
		},
		{
			"M-1vsP2",
			false
		},
		{
			"M-1vsP3",
			false
		},
		{
			"M0vsM-3",
			true
		},
		{
			"M0vsM-2",
			false
		},
		{
			"M0vsM-1",
			true
		},
		{
			"M0vsM0",
			false
		},
		{
			"M0vsM1",
			true
		},
		{
			"M0vsM2",
			true
		},
		{
			"M0vsM3",
			true
		},
		{
			"M0vsP0",
			true
		},
		{
			"M0vsP1",
			true
		},
		{
			"M0vsP2",
			true
		},
		{
			"M0vsP3",
			true
		},
		{
			"M1vsM-3",
			true
		},
		{
			"M1vsM-2",
			false
		},
		{
			"M1vsM-1",
			true
		},
		{
			"M1vsM0",
			true
		},
		{
			"M1vsM1",
			false
		},
		{
			"M1vsM2",
			true
		},
		{
			"M1vsM3",
			true
		},
		{
			"M1vsP0",
			true
		},
		{
			"M1vsP1",
			false
		},
		{
			"M1vsP2",
			true
		},
		{
			"M1vsP3",
			true
		},
		{
			"M2vsM-3",
			true
		},
		{
			"M2vsM-2",
			false
		},
		{
			"M2vsM-1",
			true
		},
		{
			"M2vsM0",
			true
		},
		{
			"M2vsM1",
			true
		},
		{
			"M2vsM2",
			false
		},
		{
			"M2vsM3",
			true
		},
		{
			"M2vsP0",
			true
		},
		{
			"M2vsP1",
			true
		},
		{
			"M2vsP2",
			false
		},
		{
			"M2vsP3",
			true
		},
		{
			"M3vsM-3",
			true
		},
		{
			"M3vsM-2",
			false
		},
		{
			"M3vsM-1",
			true
		},
		{
			"M3vsM0",
			true
		},
		{
			"M3vsM1",
			true
		},
		{
			"M3vsM2",
			true
		},
		{
			"M3vsM3",
			false
		},
		{
			"M3vsP0",
			true
		},
		{
			"M3vsP1",
			true
		},
		{
			"M3vsP2",
			true
		},
		{
			"M3vsP3",
			false
		},
		{
			"P0vsM-3",
			true
		},
		{
			"P0vsM-2",
			true
		},
		{
			"P0vsM-1",
			false
		},
		{
			"P0vsM0",
			true
		},
		{
			"P0vsM1",
			true
		},
		{
			"P0vsM2",
			true
		},
		{
			"P0vsM3",
			true
		},
		{
			"P0vsP0",
			false
		},
		{
			"P0vsP1",
			false
		},
		{
			"P0vsP2",
			false
		},
		{
			"P0vsP3",
			false
		},
		{
			"P1vsM-3",
			true
		},
		{
			"P1vsM-2",
			true
		},
		{
			"P1vsM-1",
			false
		},
		{
			"P1vsM0",
			true
		},
		{
			"P1vsM1",
			false
		},
		{
			"P1vsM2",
			true
		},
		{
			"P1vsM3",
			true
		},
		{
			"P1vsP0",
			true
		},
		{
			"P1vsP1",
			false
		},
		{
			"P1vsP2",
			true
		},
		{
			"P1vsP3",
			true
		},
		{
			"P2vsM-3",
			true
		},
		{
			"P2vsM-2",
			true
		},
		{
			"P2vsM-1",
			false
		},
		{
			"P2vsM0",
			true
		},
		{
			"P2vsM1",
			true
		},
		{
			"P2vsM2",
			false
		},
		{
			"P2vsM3",
			true
		},
		{
			"P2vsP0",
			true
		},
		{
			"P2vsP1",
			true
		},
		{
			"P2vsP2",
			false
		},
		{
			"P2vsP3",
			true
		},
		{
			"P3vsM-3",
			true
		},
		{
			"P3vsM-2",
			true
		},
		{
			"P3vsM-1",
			false
		},
		{
			"P3vsM0",
			true
		},
		{
			"P3vsM1",
			true
		},
		{
			"P3vsM2",
			true
		},
		{
			"P3vsM3",
			false
		},
		{
			"P3vsP0",
			true
		},
		{
			"P3vsP1",
			true
		},
		{
			"P3vsP2",
			true
		},
		{
			"P3vsP3",
			false
		}
	};
}
