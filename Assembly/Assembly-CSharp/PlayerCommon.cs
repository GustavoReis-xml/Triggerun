using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class PlayerCommon : NetCommon
{
	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00116B5E File Offset: 0x00114D5E
	public int FashionDisplayState
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00116B64 File Offset: 0x00114D64
	// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x00116B7C File Offset: 0x00114D7C
	public int Fast_Race_Get_Reward_State
	{
		get
		{
			return this._Fast_Race_Get_Reward_State;
		}
		set
		{
			this._Fast_Race_Get_Reward_State = value;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00116B88 File Offset: 0x00114D88
	// (set) Token: 0x06001DDA RID: 7642 RVA: 0x00116BBA File Offset: 0x00114DBA
	public Prerogative_Data Prerogative_Class
	{
		get
		{
			bool flag = this._Prerogative_Class == null;
			if (flag)
			{
				this._Prerogative_Class = new Prerogative_Data();
			}
			return this._Prerogative_Class;
		}
		set
		{
			this._Prerogative_Class = value;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00116BC4 File Offset: 0x00114DC4
	// (set) Token: 0x06001DDC RID: 7644 RVA: 0x00116C01 File Offset: 0x00114E01
	public Gem_Data Gem_Data_Class
	{
		get
		{
			bool flag = this._Gem_Data_Class == null;
			if (flag)
			{
				this._Gem_Data_Class = new Gem_Data();
				Gem_Data.Instance = this._Gem_Data_Class;
			}
			return this._Gem_Data_Class;
		}
		set
		{
			this._Gem_Data_Class = value;
		}
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x00116C0C File Offset: 0x00114E0C
	public void initSuperData(ArrayList szBase)
	{
		bool flag = szBase[1].ToString() != string.Empty;
		if (flag)
		{
			this.m_dtToushi = Convert.ToDateTime(szBase[1].ToString());
			this.m_dtToushiCheck = new CCheckEditDateTime(this.m_dtToushi);
		}
		this.m_nTouShiAddId = (int)szBase[2];
		this.aryHeroGemData = (ArrayList)szBase[3];
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x00116C84 File Offset: 0x00114E84
	public void InitBase(ArrayList szBase)
	{
		this.aryBulletCartridgeID = new ArrayList();
		this.aryBulletGemData = new ArrayList();
		this.aryCartridgePublicNow = new ArrayList();
		this.aryHeroGemData = new ArrayList();
		this.m_nCid = (int)szBase[0];
		this.m_sName = Fun.DecodingUTF8(szBase[1].ToString());
		this.m_nShili = (int)szBase[2];
		this.m_nShiliServer = this.m_nShili;
		this.m_nModelIndex = (int)szBase[3];
		this.m_nXingTai = (int)szBase[4];
		this.m_nWeaponGroup = (int)szBase[5];
		this.m_nSex = (int)szBase[6];
		this.m_nFace = (int)szBase[7];
		this.m_nLevel = (int)szBase[8];
		this.m_nExp = (int)szBase[9];
		this.m_nGM = (int)szBase[10];
		this.UpdateOneHero((ArrayList)szBase[11]);
		this.m_nInJidi = (int)szBase[12];
		this.m_szGMTran = (ArrayList)szBase[13];
		GameRoot.inst.gmFollowHero = 0;
		this.m_nUsePaint = (int)szBase[14];
		this.nLoadID = (int)szBase[15];
		this.m_nKillCount = 0;
		this.m_dKillCountDate = DateTime.MinValue;
		this.m_nJiangzhi = 0;
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00116E1C File Offset: 0x0011501C
	public void InitBattleAttr(ArrayList szAttr)
	{
		this.m_nHp = (int)szAttr[1];
		this.m_nMaxHp = (int)szAttr[2];
		this.m_nShield = (int)szAttr[3];
		this.m_nMaxShield = (int)szAttr[4];
		this.m_nArmor = (int)szAttr[5];
		this.m_nMaxArmor = (int)szAttr[6];
		this.m_nPhyatk = (int)szAttr[7];
		this.m_nWpphyatk = (int)szAttr[8];
		this.m_fMoveSpeed = (float)((int)szAttr[9]) / 10000f;
		this.m_fMoveSpeedCheck = new CCheckEditFloat(this.m_fMoveSpeed);
		bool flag = !this.isNullObj && this.m_fMoveSpeed < 0.1f;
		if (flag)
		{
			Debug.Log(this.m_sName + ",的速度小于0.1" + this.m_fMoveSpeed.ToString());
		}
		this.m_nHpSp = (int)szAttr[10];
		this.m_nHpDelay = (int)szAttr[11];
		this.m_nShdSp = (int)szAttr[12];
		this.m_nShdDelay = (int)szAttr[13];
		this.m_nMp = (int)szAttr[14];
		this.m_nAtkSpeed = (int)szAttr[15];
		this.m_nReloadSp = (int)szAttr[16];
		this.m_nDebris = (int)szAttr[17];
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00116FBC File Offset: 0x001151BC
	public void InitScn(ArrayList szScn)
	{
		Debug.Log("MATCH_DEBUG: InitScn called, data: " + szScn.Count.ToString());
		this.m_nScnID = (int)szScn[0];
		this.pos = new Vector3((float)((int)szScn[1]) / 100f, (float)((int)szScn[3]) / 100f, (float)((int)szScn[2]) / 100f);
		Debug.Log(string.Format("MATCH_DEBUG: Spawn Pos set to: {0}, {1}, {2}", this.pos.x, this.pos.y, this.pos.z));
		this.m_nForward = (float)((int)szScn[7]) * 1f;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00117097 File Offset: 0x00115297
	public void InitBuffInfo(ArrayList szInfo)
	{
		this.m_szBuffEffectData = new ArrayList();
		this.m_nDongjie = 0;
		this.m_nHunMi = 0;
		this.m_nChenMo = 0;
		this.buffSpeed = 0f;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x001170C8 File Offset: 0x001152C8
	public int GetNowHp()
	{
		return this.m_nHp + this.m_nSpecialHp + this.m_nShield + this.m_nArmor;
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x001170F8 File Offset: 0x001152F8
	public int GetMaxHp()
	{
		return this.m_nMaxHp + this.m_nSpecialHp + this.m_nMaxShield + this.m_nMaxArmor;
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00117128 File Offset: 0x00115328
	public float GetHpBiLi()
	{
		int nowHp = this.GetNowHp();
		int maxHp = this.GetMaxHp();
		return (float)nowHp / (float)maxHp;
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x00117154 File Offset: 0x00115354
	public void InItQQGiftData(ArrayList szGetData)
	{
		this.m_nisCanGetDay = (int)szGetData[0];
		this.m_nisCanGetYear = (int)szGetData[1];
		this.m_nisCanGetNew = (int)szGetData[2];
		this.m_nszLvlUp = (ArrayList)szGetData[3];
		this.m_nisCanGetGrow = (int)szGetData[4];
		this.m_nisCanGetSuper = (int)szGetData[5];
		this.m_nTGPReward = (int)szGetData[6];
		this.m_nXuFeiReward = (ArrayList)szGetData[7];
		this.m_nTGPDayReward = (int)szGetData[8];
		this.m_szTGP_Reward_up_award = (ArrayList)szGetData[10];
		this.m_nTGPNewReward = (int)szGetData[9];
		bool flag = PrerogativeCon.Instance != null && PrerogativeCon.Instance.visible;
		if (flag)
		{
			PrerogativeCon.Instance.View_Component.Display_Button();
		}
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x0011725C File Offset: 0x0011545C
	public void ShowQQGameIco()
	{
		int num = 0;
		int num2 = 1021;
		int num3 = 0;
		PlayerCommon playerCommon = PlayerDataManager.getInstance().playerCommon;
		bool flag = playerCommon.m_sPFName == "qzone" || playerCommon.m_sPFName == "website" || playerCommon.m_sPFName == "pengyou";
		if (flag)
		{
			num2 = ((playerCommon.m_sPID != 14) ? 1038 : 0);
		}
		else
		{
			bool flag2 = playerCommon.m_sPFName == "qqgame";
			if (flag2)
			{
				num2 = ((playerCommon.m_sPID != 14) ? 1021 : 0);
				bool flag3 = playerCommon.m_sPID == 14;
				if (flag3)
				{
				}
			}
		}
		bool flag4 = playerCommon.m_nQQ_Lvl > 0;
		if (flag4)
		{
			foreach (object obj in playerCommon.m_nszLvlUp)
			{
				int num4 = (int)obj;
				bool flag5 = num4 == 1;
				if (flag5)
				{
					num++;
				}
				num3++;
				bool flag6 = num3 >= 6;
				if (flag6)
				{
					break;
				}
			}
			playerCommon.AllLanZuanReward = playerCommon.m_nisCanGetDay + playerCommon.m_nisCanGetYear + playerCommon.m_nisCanGetNew + playerCommon.m_nisCanGetSuper + num;
		}
		bool flag7 = num2 != 0;
		if (flag7)
		{
			bool flag8 = playerCommon.AllLanZuanReward >= 1;
			if (flag8)
			{
				fUIManager.GetInstance().lanzuanshow = true;
			}
			else
			{
				fUIManager.GetInstance().lanzuanshow = false;
			}
		}
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x001173F8 File Offset: 0x001155F8
	public void OnHeroDataMsg(int netID, ArrayList szMsg)
	{
		bool flag = netID == 12006;
		if (flag)
		{
			this.UpdateOneHero(szMsg);
		}
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x0011741C File Offset: 0x0011561C
	public void UpdateOneHero(ArrayList szData)
	{
		bool flag = szData.Count != 0;
		if (flag)
		{
			int num = (int)szData[0];
			ArrayList value = (ArrayList)szData[1];
			int num2 = this.m_szHeroBaseData.IndexOf(num);
			bool flag2 = num2 != -1;
			if (flag2)
			{
				this.m_szHeroBaseData[num2 + 1] = value;
			}
			else
			{
				this.m_szHeroBaseData.Add(num);
				this.m_szHeroBaseData.Add(value);
			}
		}
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x001174A4 File Offset: 0x001156A4
	public ArrayList GetOneHeroInfo(int nHeroId)
	{
		ArrayList result = new ArrayList();
		int num = this.m_szHeroBaseData.IndexOf(nHeroId);
		bool flag = num != -1;
		if (flag)
		{
			result = (ArrayList)this.m_szHeroBaseData[num + 1];
		}
		return result;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x001174F0 File Offset: 0x001156F0
	public string[] GetNowModelData()
	{
		string[] array = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};
		int num = (this.m_nVisualModelIndex > 0) ? this.m_nVisualModelIndex : this.m_nModelIndex;
		bool flag = num == 2000;
		string[] result;
		if (flag)
		{
			array[0] = "male_tps";
			array[1] = "male_tps_animator";
			array[2] = "male_fps";
			array[3] = "male_fps_animator";
			result = array;
		}
		else
		{
			bool flag2 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(num);
			if (flag2)
			{
				SkinUnlock skinUnlock = HeroDataManager.GetInstance().GetUseSkinData(num, this.m_nCid);
				bool flag3 = skinUnlock == null && fGameManager.mloadConfig.AllHeroUnlock[num].szSkinUnlock.ContainsKey(1);
				if (flag3)
				{
					skinUnlock = fGameManager.mloadConfig.AllHeroUnlock[num].szSkinUnlock[1];
				}
				bool flag4 = skinUnlock != null;
				if (flag4)
				{
					array[0] = skinUnlock.tpsModel;
					array[1] = skinUnlock.tpsControl;
					array[2] = skinUnlock.fpsModel;
					array[3] = skinUnlock.fpsControl;
				}
			}
			bool flag5 = string.IsNullOrEmpty(array[0]);
			if (flag5)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"获取英雄模型数据有误////",
					num.ToString(),
					"///",
					this.m_nCid.ToString(),
					"///",
					this.m_sName
				}));
			}
			result = array;
		}
		return result;
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x0011767C File Offset: 0x0011587C
	public List<string> GetNowModelWeaponMat(int ntype)
	{
		List<string> list = new List<string>();
		WeaponUnlock useWeaponData = HeroDataManager.GetInstance().GetUseWeaponData(this.m_nModelIndex, this.m_nCid);
		bool flag = useWeaponData != null;
		if (flag)
		{
			bool flag2 = ntype == 1;
			if (flag2)
			{
				list.Add(useWeaponData.lowMat_1st);
				list.Add(useWeaponData.highMat_1st);
			}
			else
			{
				list.Add(useWeaponData.comMat_3rd);
			}
		}
		return list;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x001176F0 File Offset: 0x001158F0
	public string[] GetNpcModelData(int createIndex)
	{
		string[] array = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};
		bool flag = this.m_nModelIndex != 0;
		if (flag)
		{
			RobotSkinUnlock npcSkinData = HeroDataManager.GetInstance().GetNpcSkinData(createIndex);
			bool flag2 = npcSkinData != null;
			if (flag2)
			{
				array[0] = npcSkinData.tpsModel;
				array[1] = npcSkinData.tpsControl;
				array[2] = npcSkinData.fpsModel;
				array[3] = npcSkinData.fpsControl;
			}
		}
		bool flag3 = string.IsNullOrEmpty(array[0]);
		if (flag3)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"获取英雄模型数据有误////",
				this.m_nModelIndex.ToString(),
				"///",
				this.m_nCid.ToString(),
				"///",
				this.m_sName
			}));
		}
		return array;
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x001177D3 File Offset: 0x001159D3
	public void UserUpExp(int nExp, int nLevel)
	{
		this.m_nLevel = nLevel;
		this.m_nExp = nExp;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x001177E4 File Offset: 0x001159E4
	public void OnAddKillCount()
	{
		DateTime serverDate = Fun.GetServerDate();
		bool flag = this.m_nKillCount + 1 > 5;
		if (flag)
		{
			this.m_nKillCount = 1;
		}
		else
		{
			bool flag2 = (float)(serverDate - this.m_dKillCountDate).TotalSeconds > 4f || this.m_nKillCount == 0;
			if (flag2)
			{
				this.m_nKillCount = 1;
			}
			else
			{
				this.m_nKillCount++;
			}
		}
		this.m_dKillCountDate = serverDate;
	}

	// Token: 0x04001B04 RID: 6916
	public int autoid;

	// Token: 0x04001B05 RID: 6917
	public int m_nCid;

	// Token: 0x04001B06 RID: 6918
	public int m_nVisualModelIndex = 0;

	// Token: 0x04001B07 RID: 6919
	public string m_sName;

	// Token: 0x04001B08 RID: 6920
	public int m_nShiliServer;

	// Token: 0x04001B09 RID: 6921
	public int m_nShili;

	// Token: 0x04001B0A RID: 6922
	public int m_nSex;

	// Token: 0x04001B0B RID: 6923
	public int m_nFace;

	// Token: 0x04001B0C RID: 6924
	public int m_nUsePaint;

	// Token: 0x04001B0D RID: 6925
	public int m_nLevel;

	// Token: 0x04001B0E RID: 6926
	public int m_nExp;

	// Token: 0x04001B0F RID: 6927
	public int m_nGM;

	// Token: 0x04001B10 RID: 6928
	public ArrayList m_szGMTran = new ArrayList();

	// Token: 0x04001B11 RID: 6929
	public ArrayList m_szHeroBaseData = new ArrayList();

	// Token: 0x04001B12 RID: 6930
	public DateTime m_dtToushi = Fun.Now;

	// Token: 0x04001B13 RID: 6931
	public CCheckEditDateTime m_dtToushiCheck;

	// Token: 0x04001B14 RID: 6932
	public int m_nTouShiAddId;

	// Token: 0x04001B15 RID: 6933
	public int m_nScnID;

	// Token: 0x04001B16 RID: 6934
	public float m_nForward;

	// Token: 0x04001B17 RID: 6935
	public Vector3 posCreate;

	// Token: 0x04001B18 RID: 6936
	public Vector3 pos;

	// Token: 0x04001B19 RID: 6937
	public Vector3 ve;

	// Token: 0x04001B1A RID: 6938
	public Vector3 rot;

	// Token: 0x04001B1B RID: 6939
	public int m_nPlayerState = 1;

	// Token: 0x04001B1C RID: 6940
	public DateTime m_dPlayerDeathDate = DateTime.MinValue;

	// Token: 0x04001B1D RID: 6941
	public DateTime m_dPlayerDeathChangeHeroDate = DateTime.MinValue;

	// Token: 0x04001B1E RID: 6942
	public int m_nDeathTimeLong;

	// Token: 0x04001B1F RID: 6943
	public int m_nDeathAttackId;

	// Token: 0x04001B20 RID: 6944
	public int m_nDeathAttackNetType = -1;

	// Token: 0x04001B21 RID: 6945
	public int m_nDeadType;

	// Token: 0x04001B22 RID: 6946
	public int m_nHp;

	// Token: 0x04001B23 RID: 6947
	public int m_nMaxHp;

	// Token: 0x04001B24 RID: 6948
	public int m_nSpecialHp;

	// Token: 0x04001B25 RID: 6949
	public int m_nShield;

	// Token: 0x04001B26 RID: 6950
	public int m_nMaxShield;

	// Token: 0x04001B27 RID: 6951
	public int m_nArmor;

	// Token: 0x04001B28 RID: 6952
	public int m_nMaxArmor;

	// Token: 0x04001B29 RID: 6953
	public int m_nPhyatk;

	// Token: 0x04001B2A RID: 6954
	public int m_nWpphyatk;

	// Token: 0x04001B2B RID: 6955
	public float m_fMoveSpeed = 1f;

	// Token: 0x04001B2C RID: 6956
	public float m_fJumpSpeed = 1f;

	// Token: 0x04001B2D RID: 6957
	public float m_fMoveSpeedAnimation = 1f;

	// Token: 0x04001B2E RID: 6958
	public CCheckEditFloat m_fMoveSpeedCheck;

	// Token: 0x04001B2F RID: 6959
	public int m_nHpSp;

	// Token: 0x04001B30 RID: 6960
	public int m_nHpDelay;

	// Token: 0x04001B31 RID: 6961
	public int m_nShdSp;

	// Token: 0x04001B32 RID: 6962
	public int m_nShdDelay;

	// Token: 0x04001B33 RID: 6963
	public int m_nMp;

	// Token: 0x04001B34 RID: 6964
	public int m_nAtkSpeed;

	// Token: 0x04001B35 RID: 6965
	public int m_nReloadSp;

	// Token: 0x04001B36 RID: 6966
	public int m_nDebris;

	// Token: 0x04001B37 RID: 6967
	public int m_nInJidi;

	// Token: 0x04001B38 RID: 6968
	public int m_nJiangzhi;

	// Token: 0x04001B39 RID: 6969
	public int m_nFightStat;

	// Token: 0x04001B3A RID: 6970
	public int m_nDongjie;

	// Token: 0x04001B3B RID: 6971
	public bool m_nStatForce;

	// Token: 0x04001B3C RID: 6972
	public int m_nHunMi;

	// Token: 0x04001B3D RID: 6973
	public int m_nChenMo;

	// Token: 0x04001B3E RID: 6974
	public int m_nIsFristFight;

	// Token: 0x04001B3F RID: 6975
	public ArrayList m_nHpEffData = new ArrayList
	{
		0,
		0,
		0,
		0
	};

	// Token: 0x04001B40 RID: 6976
	public int m_nHpEffType;

	// Token: 0x04001B41 RID: 6977
	public int m_nHpEffReason;

	// Token: 0x04001B42 RID: 6978
	public int m_nKillCount;

	// Token: 0x04001B43 RID: 6979
	public DateTime m_dKillCountDate = DateTime.MinValue;

	// Token: 0x04001B44 RID: 6980
	public int nLoadID;

	// Token: 0x04001B45 RID: 6981
	public float negativePlay_Walk;

	// Token: 0x04001B46 RID: 6982
	public float negativePlay_Attack;

	// Token: 0x04001B47 RID: 6983
	public float negativePlay_Status;

	// Token: 0x04001B48 RID: 6984
	private int _Fast_Race_Get_Reward_State;

	// Token: 0x04001B49 RID: 6985
	public string guildname = string.Empty;

	// Token: 0x04001B4A RID: 6986
	public int guildposition;

	// Token: 0x04001B4B RID: 6987
	public DateTime RegDatestr;

	// Token: 0x04001B4C RID: 6988
	public DateTime LoginDate;

	// Token: 0x04001B4D RID: 6989
	public int m_nHeFuDay;

	// Token: 0x04001B4E RID: 6990
	public ArrayList m_szHeFuReward;

	// Token: 0x04001B4F RID: 6991
	public ArrayList m_szBuffEffectData;

	// Token: 0x04001B50 RID: 6992
	public int m_nWorldLevel;

	// Token: 0x04001B51 RID: 6993
	public int m_nBuffItemNetId;

	// Token: 0x04001B52 RID: 6994
	public float m_fBuffItemTime;

	// Token: 0x04001B53 RID: 6995
	public bool inWater;

	// Token: 0x04001B54 RID: 6996
	private Prerogative_Data _Prerogative_Class;

	// Token: 0x04001B55 RID: 6997
	private Gem_Data _Gem_Data_Class;

	// Token: 0x04001B56 RID: 6998
	public ArrayList m_szGeneData = new ArrayList();

	// Token: 0x04001B57 RID: 6999
	public string m_sPFName;

	// Token: 0x04001B58 RID: 7000
	public int m_nQQ_Lvl;

	// Token: 0x04001B59 RID: 7001
	public int m_nQQ_Year;

	// Token: 0x04001B5A RID: 7002
	public int m_nBlueGrowLvl;

	// Token: 0x04001B5B RID: 7003
	public int m_nQQ_Super;

	// Token: 0x04001B5C RID: 7004
	public int m_sPID;

	// Token: 0x04001B5D RID: 7005
	public int m_nisCanGetDay;

	// Token: 0x04001B5E RID: 7006
	public int m_nisCanGetYear;

	// Token: 0x04001B5F RID: 7007
	public int m_nisCanGetNew;

	// Token: 0x04001B60 RID: 7008
	public ArrayList m_nszLvlUp;

	// Token: 0x04001B61 RID: 7009
	public int m_nisCanGetGrow;

	// Token: 0x04001B62 RID: 7010
	public int m_nisCanGetSuper;

	// Token: 0x04001B63 RID: 7011
	public int m_nTGPReward;

	// Token: 0x04001B64 RID: 7012
	public ArrayList m_nXuFeiReward;

	// Token: 0x04001B65 RID: 7013
	public ArrayList m_szTGP_Reward_up_award;

	// Token: 0x04001B66 RID: 7014
	public int m_nTGPNewReward;

	// Token: 0x04001B67 RID: 7015
	public int m_nTGPDayReward;

	// Token: 0x04001B68 RID: 7016
	public int AllLanZuanReward;
}
