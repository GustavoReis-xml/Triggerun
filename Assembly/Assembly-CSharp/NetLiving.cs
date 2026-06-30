using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class NetLiving : NetObj
{
	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000FE760 File Offset: 0x000FC960
	// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x000FE778 File Offset: 0x000FC978
	public int FpsOrTps
	{
		get
		{
			return this._FpsOrTps;
		}
		set
		{
			this._FpsOrTps = value;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000FE784 File Offset: 0x000FC984
	// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x000FE7EF File Offset: 0x000FC9EF
	public Npc_Handle Npc_Handle_Component
	{
		get
		{
			bool flag = this._Npc_Handle_Component == null;
			if (flag)
			{
				this._Npc_Handle_Component = this.UserControl.GetComponent<Npc_Handle>();
				bool flag2 = this._Npc_Handle_Component == null;
				if (flag2)
				{
					this._Npc_Handle_Component = this.UserControl.AddComponent<Npc_Handle>();
				}
				this._Npc_Handle_Component.Hero_Player_Component = this;
			}
			return this._Npc_Handle_Component;
		}
		set
		{
			this._Npc_Handle_Component = value;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000FE7FC File Offset: 0x000FC9FC
	public bool CanHuDong
	{
		get
		{
			bool flag = this.nHuDongState >= 50 && this.nHuDongState < 200;
			return !flag;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06001ABA RID: 6842 RVA: 0x000FE834 File Offset: 0x000FCA34
	// (set) Token: 0x06001ABB RID: 6843 RVA: 0x000FE84C File Offset: 0x000FCA4C
	public Transform userHeadTransform
	{
		get
		{
			return this._userHeadTransform;
		}
		set
		{
			this._userHeadTransform = value;
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06001ABC RID: 6844 RVA: 0x000FE858 File Offset: 0x000FCA58
	// (set) Token: 0x06001ABD RID: 6845 RVA: 0x000FE870 File Offset: 0x000FCA70
	public Transform userSpine
	{
		get
		{
			return this._userBip;
		}
		set
		{
			this._userBip = value;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06001ABE RID: 6846 RVA: 0x000FE87C File Offset: 0x000FCA7C
	// (set) Token: 0x06001ABF RID: 6847 RVA: 0x000FE894 File Offset: 0x000FCA94
	public Transform userBip
	{
		get
		{
			return this._userSpine;
		}
		set
		{
			this._userSpine = value;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x000FE8A0 File Offset: 0x000FCAA0
	// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x000FE8B8 File Offset: 0x000FCAB8
	public virtual Transform camTarget
	{
		get
		{
			return this._camTarget;
		}
		set
		{
			this._camTarget = value;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x000FE8C4 File Offset: 0x000FCAC4
	// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x000FE8DC File Offset: 0x000FCADC
	public virtual Transform UserHeadTarget
	{
		get
		{
			return this._UserHeadTarget;
		}
		set
		{
			this._UserHeadTarget = value;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x000FE8E8 File Offset: 0x000FCAE8
	public virtual Vector3 userHead
	{
		get
		{
			bool flag = this._userHeadTransform == null;
			Vector3 result;
			if (flag)
			{
				result = this.userCore;
			}
			else
			{
				result = this._userHeadTransform.position;
			}
			return result;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x000FE91F File Offset: 0x000FCB1F
	public virtual Vector3 userCore
	{
		get
		{
			return this.playerCommon.pos + this._userCoreAdd;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x000FE937 File Offset: 0x000FCB37
	public virtual Vector3 userCenter
	{
		get
		{
			return this.thisModel.transform.position + this._userCenterAdd;
		}
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000FE954 File Offset: 0x000FCB54
	// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x000FE986 File Offset: 0x000FCB86
	public ArrayList Stroke_Material_Array
	{
		get
		{
			bool flag = this._Stroke_Material_Array == null;
			if (flag)
			{
				this._Stroke_Material_Array = new ArrayList();
			}
			return this._Stroke_Material_Array;
		}
		set
		{
			this._Stroke_Material_Array = value;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x000FE990 File Offset: 0x000FCB90
	// (set) Token: 0x06001ACA RID: 6858 RVA: 0x000FE9C2 File Offset: 0x000FCBC2
	public List<Renderer> Stroke_Material_Object_Array
	{
		get
		{
			bool flag = this._Stroke_Material_Object_Array == null;
			if (flag)
			{
				this._Stroke_Material_Object_Array = new List<Renderer>();
			}
			return this._Stroke_Material_Object_Array;
		}
		set
		{
			this._Stroke_Material_Object_Array = value;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000FE9CC File Offset: 0x000FCBCC
	// (set) Token: 0x06001ACC RID: 6860 RVA: 0x000FE9FE File Offset: 0x000FCBFE
	public Dictionary<string, List<Npc_Entity>> Adhere_Child_Object_Data
	{
		get
		{
			bool flag = this._Adhere_Child_Object_Data == null;
			if (flag)
			{
				this._Adhere_Child_Object_Data = new Dictionary<string, List<Npc_Entity>>();
			}
			return this._Adhere_Child_Object_Data;
		}
		set
		{
			this._Adhere_Child_Object_Data = value;
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000FEA08 File Offset: 0x000FCC08
	// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000FEA3A File Offset: 0x000FCC3A
	public Hero_Message Hero_Message
	{
		get
		{
			bool flag = this._Hero_Message == null;
			if (flag)
			{
				this._Hero_Message = new Hero_Message();
			}
			return this._Hero_Message;
		}
		set
		{
			this._Hero_Message = value;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000FEA44 File Offset: 0x000FCC44
	// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x000FEA76 File Offset: 0x000FCC76
	public Hero_Message_Handle Hero_Message_Handle
	{
		get
		{
			bool flag = this._Hero_Message_Handle == null;
			if (flag)
			{
				this._Hero_Message_Handle = new Hero_Message_Handle();
			}
			return this._Hero_Message_Handle;
		}
		set
		{
			this._Hero_Message_Handle = value;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x000FEA80 File Offset: 0x000FCC80
	// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x000FEA98 File Offset: 0x000FCC98
	public List<Renderer> SzHongwaiMat
	{
		get
		{
			return this.szHongwaiMat;
		}
		set
		{
			this.szHongwaiMat = value;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x000FEAA4 File Offset: 0x000FCCA4
	// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x000FEABC File Offset: 0x000FCCBC
	public List<Renderer> SzFuhuoMat
	{
		get
		{
			return this.szFuhuoMat;
		}
		set
		{
			this.szFuhuoMat = value;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000FEAC8 File Offset: 0x000FCCC8
	// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x000FEAE0 File Offset: 0x000FCCE0
	public virtual GameObject thisModel
	{
		get
		{
			return this._thisModel;
		}
		set
		{
			this._thisModel = value;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x000FEAEC File Offset: 0x000FCCEC
	// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x000FEB04 File Offset: 0x000FCD04
	public virtual bool needSendToServer
	{
		get
		{
			return this._needSendToServer;
		}
		set
		{
			this._needSendToServer = value;
		}
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000FEB10 File Offset: 0x000FCD10
	// (set) Token: 0x06001ADA RID: 6874 RVA: 0x000FEB28 File Offset: 0x000FCD28
	public bool heroInvi
	{
		get
		{
			return this._heroInvi;
		}
		set
		{
			this._heroInvi = value;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000FEB34 File Offset: 0x000FCD34
	// (set) Token: 0x06001ADC RID: 6876 RVA: 0x000FEB4C File Offset: 0x000FCD4C
	public bool bePlayActive
	{
		get
		{
			return this._bePlayActive;
		}
		set
		{
			this._bePlayActive = value;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06001ADD RID: 6877 RVA: 0x000FEB58 File Offset: 0x000FCD58
	// (set) Token: 0x06001ADE RID: 6878 RVA: 0x000FEB70 File Offset: 0x000FCD70
	public bool beSuperSkill
	{
		get
		{
			return this._beSuperSkill;
		}
		set
		{
			this._beSuperSkill = value;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000FEB7C File Offset: 0x000FCD7C
	// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x000FEB94 File Offset: 0x000FCD94
	public NetLiving GameAttackPlayer
	{
		get
		{
			return this._GameAttackPlayer;
		}
		set
		{
			this._GameAttackPlayer = value;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000FEB9E File Offset: 0x000FCD9E
	public Vector3 getMubiaoUp
	{
		get
		{
			return this.mubiaoUp;
		}
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x000FEBA6 File Offset: 0x000FCDA6
	public void SetGameUiSignObj(bool mbool)
	{
		this.mModelTopObjbool = mbool;
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x000FEBB0 File Offset: 0x000FCDB0
	protected virtual void setHeroForce()
	{
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x000FEBB4 File Offset: 0x000FCDB4
	protected virtual void InitPlayerInfo(ArrayList msg, ArrayList msg2, bool isNullNetplayer = false)
	{
		bool flag = this.playerCommon == null;
		if (flag)
		{
			this.playerCommon = new PlayerCommon();
		}
		this.playerCommon.InitBase((ArrayList)msg[0]);
		this.InJiDi = 0;
		this.setHeroForce();
		this.nForce = this.playerCommon.m_nShili;
		this.loadID = this.playerCommon.nLoadID;
		this.playerCommon.isNullObj = isNullNetplayer;
		this.playerCommon.InitBattleAttr((ArrayList)msg[1]);
		this.playerCommon.InitScn((ArrayList)msg[2]);
		this.playerCommon.InitBuffInfo((ArrayList)msg[3]);
		this.SetAnimatorServerData((ArrayList)msg[4]);
		this.playerCommon.Prerogative_Class.Initialize_Data((ArrayList)msg[5]);
		this.playerCommon.aryHeroSpecialData = (ArrayList)msg[6];
		this.playerCommon.m_szGeneData = (ArrayList)msg[7];
		this.playerCommon.m_nPlayerState = (int)msg2[0];
		this.isDead = ((int)msg2[0] == 0);
		bool flag2 = (int)msg2[1] == 0;
		if (flag2)
		{
			this.playerCommon.posCreate = this.playerCommon.pos;
		}
		ArrayList arrayList = (ArrayList)msg2[2];
		bool flag3 = arrayList.Count >= 2;
		if (flag3)
		{
			this.playerCommon.aryBulletCartridgeID = (ArrayList)arrayList[0];
			this.playerCommon.aryBulletGemData = (ArrayList)arrayList[1];
			this.playerCommon.aryCartridgePublicNow = (ArrayList)arrayList[2];
		}
		this.playerCommon.m_nHeroNo = (int)msg2[3];
		this.playerCommon.aryheroDazhao = (ArrayList)msg2[4];
		this.playerCommon.arySkillServerData = (ArrayList)msg2[5];
		this.OnSuperData((ArrayList)msg2[6]);
		this.playerCommon.aryWarData = (ArrayList)msg2[7];
		bool isMainPlayer = this.isMainPlayer;
		if (isMainPlayer)
		{
			HeroDataManager.GetInstance().InitOpenHeroData(this.playerCommon.m_szHeroBaseData);
		}
		bool flag4 = fGameManager.mloadConfig.AllHeroList.ContainsKey(this.playerCommon.m_nModelIndex);
		if (flag4)
		{
			this.heroData = fGameManager.mloadConfig.AllHeroList[this.playerCommon.m_nModelIndex];
		}
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x000FEE68 File Offset: 0x000FD068
	public void Open_Or_Close_Stroke_Material(bool _State)
	{
		bool gm_State = GameRoot.inst.Setting_FPS.GM_State;
		if (gm_State)
		{
			_State = false;
		}
		for (int i = 0; i < this.Stroke_Material_Object_Array.Count; i++)
		{
			Renderer renderer = this.Stroke_Material_Object_Array[i];
			bool flag = !(renderer == null) && !(renderer.gameObject == null);
			if (flag)
			{
				renderer.gameObject.SetActive(_State);
			}
		}
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x000FEEEC File Offset: 0x000FD0EC
	public void OnSuperData(ArrayList data)
	{
		bool flag = data.Count != 0;
		if (flag)
		{
			int num = (int)data[0];
			bool flag2 = this.playerCommon.m_nShiliServer == num;
			if (flag2)
			{
				this.playerCommon.initSuperData(data);
			}
		}
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x000FEF38 File Offset: 0x000FD138
	private void SetAnimatorServerData(ArrayList parm)
	{
		this.animatorParmObject = new Dictionary<string, CAnimatorParm>();
		for (int i = 0; i < parm.Count; i++)
		{
			ArrayList aryParam = (ArrayList)parm[i];
			this.UpdateAnimatorParm(aryParam);
		}
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x000FEF80 File Offset: 0x000FD180
	public void UpdateAnimatorParm(ArrayList aryParam)
	{
		ArrayList arrayList = (ArrayList)aryParam[0];
		bool flag = !this.animatorParmObject.ContainsKey((string)arrayList[0]);
		if (flag)
		{
			CAnimatorParm canimatorParm = new CAnimatorParm();
			canimatorParm.ani = (string)arrayList[0];
			canimatorParm.type = (int)arrayList[1];
			canimatorParm.parm = arrayList[2];
			canimatorParm.isstate = (int)aryParam[1];
			this.animatorParmObject.Add(canimatorParm.ani, canimatorParm);
		}
		else
		{
			CAnimatorParm canimatorParm2 = this.animatorParmObject[(string)arrayList[0]];
			canimatorParm2.ani = (string)arrayList[0];
			canimatorParm2.type = (int)arrayList[1];
			canimatorParm2.parm = arrayList[2];
			canimatorParm2.isstate = (int)aryParam[1];
		}
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x000FF078 File Offset: 0x000FD278
	public void Add_Adhere_Child_Object_Data(string _Collider_Name, Npc_Entity _Entity)
	{
		bool flag = !this.Adhere_Child_Object_Data.ContainsKey(_Collider_Name);
		if (flag)
		{
			this.Adhere_Child_Object_Data.Add(_Collider_Name, new List<Npc_Entity>());
		}
		this.Adhere_Child_Object_Data[_Collider_Name].Add(_Entity);
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x000FF0C0 File Offset: 0x000FD2C0
	public virtual void showRoot(bool bShow)
	{
		bool flag = this.userRoot == null;
		if (!flag)
		{
			if (bShow)
			{
				bool flag2 = !this.userRoot.activeSelf;
				if (flag2)
				{
					this.userRoot.SetActive(bShow);
				}
			}
			else
			{
				bool activeSelf = this.userRoot.activeSelf;
				if (activeSelf)
				{
					this.userRoot.SetActive(bShow);
				}
			}
		}
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x000FF129 File Offset: 0x000FD329
	public virtual void PlayerSelect(ArrayList aryNetInfo)
	{
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x000FF12C File Offset: 0x000FD32C
	public virtual int getAttackForce()
	{
		return this.nForce;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x000FF144 File Offset: 0x000FD344
	public virtual void HaveDamage(ArrayList szDamage, ArrayList szHp)
	{
		bool flag = this.hitReaction != null && szHp.Count > 0;
		if (flag)
		{
			int num = (int)szDamage[6];
			ArrayList arrayList = (ArrayList)szDamage[1];
			bool flag2 = arrayList.Count > 1;
			if (flag2)
			{
				this.forceDamage = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			}
			bool flag3 = arrayList.Count > 0 && num == 0;
			if (flag3)
			{
				Vector3 pos = this.forceDamage;
				this.hitReaction.Hit(pos);
			}
		}
		bool flag4 = (int)szDamage[3] == fGameManager.GetInstance().getPlayer().getNetid() && szHp.Count > 7;
		if (flag4)
		{
			ArrayList arrayList2 = (ArrayList)szHp[7];
			bool flag5 = (int)arrayList2[0] > 0;
			if (flag5)
			{
				HeroPlayer player = fGameManager.GetInstance().getPlayer();
				player.playerCommon.negativePlay_Status = 0f;
				((HeroBaseMe)player.heroBase).NegativeWarning_NoFireState = false;
			}
		}
		this.SetHp(szHp);
		this.livingDamageDate = Fun.Now;
		bool flag6 = this.heroBase != null;
		if (flag6)
		{
			this.heroBase.beDamage(szDamage, szHp);
		}
		int nNetID = (int)szDamage[3];
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
		int num2 = (int)szDamage[13];
		int _launcherNo = (int)szDamage[14];
		bool flag7 = num2 > 0 && netLiving != null && netLiving.wc != null;
		if (flag7)
		{
			netLiving.wc.damageHitTalk(num2, _launcherNo);
		}
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x000FF342 File Offset: 0x000FD542
	public virtual void InitUserControlScript()
	{
		this.heroSkill = this.userRoot.AddComponent<CPlayerSkill>();
		this.heroSkill.InitPSkills(this);
		this.SkillDataUpdate(31101, this.playerCommon.arySkillServerData);
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x000FF37C File Offset: 0x000FD57C
	public virtual void SetHp(ArrayList szHp)
	{
		bool flag = szHp.Count >= 1 && this.playerCommon != null;
		if (flag)
		{
			this.playerCommon.m_nHp = (int)szHp[0];
			this.playerCommon.m_nMaxHp = (int)szHp[1];
			this.playerCommon.m_nShield = (int)szHp[2];
			this.playerCommon.m_nMaxShield = (int)szHp[3];
			this.playerCommon.m_nArmor = (int)szHp[4];
			this.playerCommon.m_nMaxArmor = (int)szHp[5];
			this.playerCommon.m_nSpecialHp = (int)szHp[6];
			this.playerCommon.m_nHpEffData = (ArrayList)szHp[7];
			this.playerCommon.m_nHpEffType = (int)szHp[8];
			this.playerCommon.m_nHpEffReason = (int)szHp[9];
		}
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x000FF48F File Offset: 0x000FD68F
	public virtual void Hurt(int _Hp)
	{
		this.playerCommon.m_nHp = _Hp;
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x000FF49E File Offset: 0x000FD69E
	public virtual void Have_Shield_Damge(int _Value)
	{
		this.playerCommon.m_nShield = _Value;
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x000FF4B0 File Offset: 0x000FD6B0
	public virtual bool GetAnimatorBool(string ani)
	{
		return false;
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x000FF4C4 File Offset: 0x000FD6C4
	public virtual float GetAnimatorFloat(string ani)
	{
		return -999f;
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x000FF4DC File Offset: 0x000FD6DC
	public virtual int GetAnimatorInteger(string ani)
	{
		return -999;
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x000FF4F3 File Offset: 0x000FD6F3
	public virtual void DamageMsgToServer(NetLiving player, ArrayList ary)
	{
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x000FF4F6 File Offset: 0x000FD6F6
	public virtual void SendMsgToServer(int nNetID, ArrayList szNetMsg)
	{
		this.TosFun("SendMsgToServer", new object[]
		{
			nNetID,
			szNetMsg
		});
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x000FF518 File Offset: 0x000FD718
	public virtual void SendMessageToServer(int nNetID, ArrayList szNetMsg)
	{
		this.TosFun("SendMsgToServer", new object[]
		{
			nNetID,
			szNetMsg
		});
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000FF53A File Offset: 0x000FD73A
	public virtual void SendSkillParms(ArrayList list)
	{
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x000FF53D File Offset: 0x000FD73D
	public virtual void ChangeWeaponGroup(int xingtai)
	{
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x000FF540 File Offset: 0x000FD740
	public virtual void ChangeWeaponGroupUpdate(ArrayList data)
	{
		bool flag = this.wc != null;
		if (flag)
		{
			this.playerCommon.m_nWeaponGroup = (int)data[0];
			this.wc.SwitchWeaponGroup(this.playerCommon.m_nWeaponGroup);
		}
		bool flag2 = this.heroBase != null;
		if (flag2)
		{
			this.heroBase.ChangeWeaponGroup(this.playerCommon.m_nWeaponGroup);
		}
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x000FF5B7 File Offset: 0x000FD7B7
	public virtual void XingTaiGroup(int xingtai)
	{
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x000FF5BA File Offset: 0x000FD7BA
	public virtual void XingTaiGroupUpdate(ArrayList data)
	{
	}

	// Token: 0x06001AFD RID: 6909 RVA: 0x000FF5BD File Offset: 0x000FD7BD
	public virtual void SetAnimator(string ani, int type, object parm, int sendtype = 1)
	{
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x000FF5C0 File Offset: 0x000FD7C0
	public virtual void SetAnimatorUpdate(string ani, int type, object parm)
	{
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x000FF5C3 File Offset: 0x000FD7C3
	protected virtual void ModeLoadOK()
	{
		this.netinitok = true;
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x000FF5CD File Offset: 0x000FD7CD
	protected virtual void NetLivingCreateFinish()
	{
		CFilmManager.Instance.CheckFilmNetLivingState(this);
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x000FF5DC File Offset: 0x000FD7DC
	public GameObject getCanKillCollider(string name)
	{
		bool flag = this.colliderCankill.ContainsKey(name);
		GameObject result;
		if (flag)
		{
			result = this.colliderCankill[name];
		}
		else
		{
			bool flag2 = !name.Contains("defaultMesh");
			if (flag2)
			{
				Debug.LogError("colliderCankill hai 没初始化?找不到" + name);
			}
			result = this.defaultMesh;
		}
		return result;
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x000FF63C File Offset: 0x000FD83C
	public void InitHeroWeapon(int _index)
	{
		bool flag = true;
		bool flag2 = this.wc != null;
		if (flag2)
		{
			flag = false;
			bool flag3 = this.needSendToServer && this.wc.GetType() == typeof(CWeaponControlIt);
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.userRoot.GetComponent<CWeaponControlIt>());
				this.wc = this.userRoot.AddComponent<CWeaponControlMe>();
				flag = true;
			}
		}
		else
		{
			bool needSendToServer = this.needSendToServer;
			if (needSendToServer)
			{
				this.wc = this.userRoot.AddComponent<CWeaponControlMe>();
			}
			else
			{
				this.wc = this.userRoot.AddComponent<CWeaponControlIt>();
			}
		}
		bool flag4 = !fGameManager.mloadConfig.WeaponXmlData.ContainsKey(_index);
		if (flag4)
		{
			Debug.LogError(string.Format("注意:{0}这里没有这个英雄的武器信息:{1}", this.userRoot.name, _index));
		}
		WeaponData weaponData = fGameManager.mloadConfig.WeaponXmlData[_index];
		bool flag5 = flag;
		if (flag5)
		{
			this.wc.SetupWeapon(this, weaponData, weaponData.hitratesub, weaponData.cartridgepublic);
		}
		for (int i = 0; i < weaponData.weaponList.Count; i++)
		{
			XMLWeapon xmlweapon = (XMLWeapon)weaponData.weaponList[i];
			Transform[] array = new Transform[xmlweapon.muzzlename.Count];
			Transform[] array2 = new Transform[xmlweapon.muzzlename.Count];
			bool flag6 = this.userModelFps != null;
			if (flag6)
			{
				for (int j = 0; j < xmlweapon.muzzlename.Count; j++)
				{
					Transform transform = Fun.FindGameObjectChild(this.userModelFps, xmlweapon.muzzlename[j].ToString(), true);
					array[j] = transform;
				}
			}
			for (int j = 0; j < xmlweapon.muzzlename.Count; j++)
			{
				Transform transform2 = Fun.FindGameObjectChild(this.userModelTps, xmlweapon.muzzlename[j].ToString(), true);
				array2[j] = transform2;
			}
			Transform[] point = (this.FpsOrTps != 1) ? array2 : array;
			bool flag7 = flag;
			if (flag7)
			{
				this.wc.CreateWeaponControl(this, xmlweapon, point, array, array2);
			}
			else
			{
				this.wc.replaceMuzzlePoint(xmlweapon.shootname, point);
			}
		}
		bool flag8 = flag;
		if (flag8)
		{
			this.wc.InitBulletCartridge(this.playerCommon.aryBulletCartridgeID, this.playerCommon.aryCartridgePublicNow);
			this.wc.InitBulletGem(this.playerCommon.aryBulletGemData);
			this.wc.SwitchWeaponGroup(this.playerCommon.m_nWeaponGroup);
			bool flag9 = gameuiCon.inst != null && CFilmManager.Instance.filmParentNetId(this) == base.getNetid() && this.netobject_type == 1;
			if (flag9)
			{
				gameuiCon.inst.NewMainHeroSkill.OnChangeWeaponGroup(this);
			}
		}
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x000FF94C File Offset: 0x000FDB4C
	public virtual void CB(int weaponName, int launcherNo, string muzzleName, int bullId, int bullettype, ArrayList born, ArrayList target, ArrayList parm, int numberBullet, int _cartridge, int _loadid)
	{
		bool flag = _loadid == this.loadID && !(this.wc == null) && !this.haveDestroy && this.playerHaveCreate && (this.isCopy || !CFilmManager.Instance.playing);
		if (flag)
		{
			int num = 0;
			string targetBodyName = string.Empty;
			Vector3 fromPoint = new Vector3((float)((int)born[0]) / 100f, (float)((int)born[1]) / 100f, (float)((int)born[2]) / 100f);
			Vector3 targetPoint = new Vector3((float)((int)target[0]) / 100f, (float)((int)target[1]) / 100f, (float)((int)target[2]) / 100f);
			bool flag2 = bullettype == 4 || bullettype == 6;
			if (flag2)
			{
				num = (int)parm[0];
				targetBodyName = (string)parm[1];
			}
			((CWeaponControlIt)this.wc).BulletNumberChange(weaponName, launcherNo, numberBullet);
			((CWeaponControlIt)this.wc).WeaponFire(weaponName, launcherNo, "0", muzzleName, fromPoint, targetPoint, num, bullId, targetBodyName, parm);
		}
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x000FFAA4 File Offset: 0x000FDCA4
	public virtual void CBP(int weaponName, int launcherNo, string muzzleName, int bullId, int x, int y, int z)
	{
		bool flag = !(this.wc == null);
		if (flag)
		{
			((CWeaponControlIt)this.wc).BulletPostionUpdate(weaponName, launcherNo, muzzleName, bullId, x, y, z);
		}
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x000FFAE4 File Offset: 0x000FDCE4
	public virtual void CB_READY(int launcherName, int stat, int time1, int needTime)
	{
		bool flag = !(this.wc == null);
		if (flag)
		{
			bool flag2 = stat == 1;
			if (flag2)
			{
				((CWeaponControlIt)this.wc).WeaponFireFullReady(launcherName, time1, needTime);
			}
			else
			{
				((CWeaponControlIt)this.wc).WeaponFireFullCancel(launcherName);
			}
		}
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x000FFB3C File Offset: 0x000FDD3C
	public virtual void CW(ArrayList data)
	{
		CBulletIt.boomUpdate(data);
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x000FFB46 File Offset: 0x000FDD46
	public virtual void SkillDo(ArrayList parm, ArrayList parm2)
	{
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000FFB49 File Offset: 0x000FDD49
	public virtual void continueInit()
	{
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000FFB4C File Offset: 0x000FDD4C
	public virtual void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.isDead = true;
		this.DeadCameraLookAtObject(_attackId);
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = netLiving != null;
		if (flag)
		{
			this.playerCommon.m_nDeathAttackNetType = netLiving.netobject_type;
		}
		int num = (netLiving == null || netLiving.bePlayer) ? _attackId : ((Npc_Entity)netLiving).Master_ID;
		NetLiving netLiving2 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
		bool flag2 = _warData != null && _warData.Count > 0 && !this.isCopy;
		if (flag2)
		{
			NetLiving netLiving3 = (netLiving == null || netLiving.bePlayer) ? netLiving : netLiving2;
			bool flag3 = this.netobject_type != 2;
			if (flag3)
			{
				this.playerCommon.aryWarData = (ArrayList)_warData[0];
			}
			bool flag4 = netLiving3 != null;
			if (flag4)
			{
				netLiving3.playerCommon.aryWarData = (ArrayList)_warData[1];
				bool flag5 = MatchManager.GetInstance().m_pFbData.nType == 11 && this.netobject_type != 2;
				if (flag5)
				{
					WarPersonManager.Instance.Killed(netLiving3, this);
				}
			}
		}
		bool flag6 = netLiving2 != null && this.nForce != netLiving2.nForce && (this.netobject_type == 1 || (this.netobject_type == 2 && MatchManager.GetInstance().m_pFbData.nType == 6) || this.netobject_type == 6);
		if (flag6)
		{
			netLiving2.playerCommon.OnAddKillCount();
		}
		int netid = player.getNetid();
		bool flag7 = (_attackId == netid || base.getNetid() == netid) && _attackId != base.getNetid() && netLiving != null && netLiving.wc != null;
		if (flag7)
		{
			netLiving.wc.DeadSound(weaponName, launcherName);
		}
		int num2 = 0;
		bool flag8 = this.nForce != player.nForce && (this.netobject_type == 1 || (this.netobject_type == 2 && MatchManager.GetInstance().m_pFbData.nType == 6) || this.netobject_type == 6);
		if (flag8)
		{
			bool flag9 = num == netid && !this.isCopy;
			if (flag9)
			{
				num2 = player.playerCommon.m_nKillCount;
				int num3 = player.heroBase.UnBreakableKillCount();
				bool flag10 = num2 > 1;
				if (flag10)
				{
					((CPlayerSoundMe)player.playerSound).PlayMultiKillSound(num2 - 2);
				}
				bool flag11 = num3 != 0 && num3 % 5 == 0;
				if (flag11)
				{
					((CPlayerSoundMe)player.playerSound).PlayUnbreakbleKillSound();
				}
			}
			bool flag12 = netLiving != null && this.CheckAliveTemates(netLiving) == 0 && netLiving.nForce == player.nForce;
			if (flag12)
			{
				((CPlayerSoundMe)player.playerSound).PlayAcedSound();
				num2 = 100;
			}
			bool flag13 = num2 > 0 && player.playerZhunxingMe != null && !this.isCopy && MatchManager.GetInstance().m_nMatchType != 4;
			if (flag13)
			{
				player.playerZhunxingMe.OnLianShaEffect(num2);
				bool flag14 = MatchManager.GetInstance().m_nMatchType == 3;
				if (flag14)
				{
					KingTrainManager.GetInstance().OnLianSha(num2);
				}
			}
		}
		bool flag15 = gameuiCon.inst != null && !this.isCopy && MatchManager.GetInstance().m_nMatchType != 3;
		if (flag15)
		{
			gameuiCon.inst.mainMsgUI.ShowHintMsg(_attackId, base.getNetid());
			this.OnCanYuKillMsg(m_szCanYuXiaoMie, _attackId, base.getNetid());
		}
		this.Clear_Adhere_Npc();
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x000FFF0C File Offset: 0x000FE10C
	public void Clear_Adhere_Npc(string _Key_Name)
	{
		List<string> list = new List<string>(this.Adhere_Child_Object_Data.Keys);
		bool flag = !this.Adhere_Child_Object_Data.ContainsKey(_Key_Name);
		if (!flag)
		{
			List<Npc_Entity> list2 = this.Adhere_Child_Object_Data[_Key_Name];
			for (int i = 0; i < list2.Count; i++)
			{
				Npc_Entity npc_Entity = list2[i];
				bool flag2 = !(npc_Entity.UserControl == null) && !(npc_Entity.thisModel == null);
				if (flag2)
				{
					npc_Entity.UserControl.transform.parent = npc_Entity.Container.transform;
					Fun.Change_Collider(npc_Entity.thisModel, true);
				}
			}
			this.Adhere_Child_Object_Data.Remove(_Key_Name);
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000FFFD8 File Offset: 0x000FE1D8
	public void Clear_Adhere_Npc()
	{
		List<string> list = new List<string>(this.Adhere_Child_Object_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			this.Clear_Adhere_Npc(list[i]);
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x00100020 File Offset: 0x000FE220
	private int CheckAliveTemates(NetLiving attacker)
	{
		bool flag = attacker == null;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			int num = 0;
			Dictionary<int, GamePlayer> dictionary = null;
			dictionary = ((attacker.playerCommon.m_nShili != 1) ? User_Data_Handle.Get_User_Data(1) : User_Data_Handle.Get_User_Data(2));
			foreach (int key in dictionary.Keys)
			{
				bool flag2 = !dictionary[key].isDead;
				if (flag2)
				{
					num++;
				}
			}
			bool flag3 = num == 0 && dictionary.Count >= 3;
			if (flag3)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x001000E4 File Offset: 0x000FE2E4
	private void OnCanYuKillMsg(ArrayList szKill, int attackId, int nTargetId)
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 11 || MatchManager.GetInstance().m_pFbData.nType == 10;
		if (!flag)
		{
			int count = szKill.Count;
			for (int i = 0; i < count; i++)
			{
				int num = (int)szKill[i];
				bool flag2 = num != nTargetId;
				if (flag2)
				{
					gameuiCon.inst.mainMsgUI.OnAddKillDeadMsg(num, nTargetId);
				}
			}
		}
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x0010016C File Offset: 0x000FE36C
	private void DeadCameraLookAtObject(int _attackId)
	{
		bool flag = !this.isDead;
		if (!flag)
		{
			NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
			bool flag2 = netObj == null;
			if (!flag2)
			{
				bool flag3 = netObj.netobject_type == 2;
				if (flag3)
				{
					Npc_Entity npc_Entity = (Npc_Entity)netObj;
					bool flag4 = npc_Entity == null;
					if (flag4)
					{
						return;
					}
					bool flag5 = npc_Entity.Master_ID < 1;
					if (flag5)
					{
						netObj = npc_Entity;
					}
					else
					{
						netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(npc_Entity.Master_ID);
						bool flag6 = netObj == null;
						if (flag6)
						{
							return;
						}
					}
				}
				this.GameAttackPlayer = (NetLiving)netObj;
			}
		}
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x00100218 File Offset: 0x000FE418
	public virtual void OnFightStat(int m_nFightStat, int m_nFightID)
	{
		this.playerCommon.m_nFightStat = m_nFightStat;
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x00100228 File Offset: 0x000FE428
	public bool PlayerCreated()
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag = player == null || player.playerCommon == null || player.playerCommon.m_sName == string.Empty;
		return !flag;
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x00100272 File Offset: 0x000FE472
	public virtual void AddCharForce(Vector3 force)
	{
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x00100275 File Offset: 0x000FE475
	public virtual void OnPveUpDateDeadTime(int nTime)
	{
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x00100278 File Offset: 0x000FE478
	public override void OnNetDestroy()
	{
		this.Clear_Adhere_Npc();
		bool flag = this.heroBase != null;
		if (flag)
		{
			this.heroBase.OnNetDestroy();
		}
		this.haveDestroy = true;
		bool flag2 = this.wc != null;
		if (flag2)
		{
			this.wc.NetObject = null;
		}
		CFilmManager.Instance.InsertGamePlayerFilm(base.GetType(), this, 'c', "OnNetDestroy", null);
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x001002EC File Offset: 0x000FE4EC
	public virtual Vector3 getMubiao(int layerType)
	{
		Vector3 result;
		if (layerType != 1)
		{
			if (layerType != 2)
			{
				result = this.mubiaoPoint[0];
			}
			else
			{
				bool flag = this.nForce == 1;
				if (flag)
				{
					result = this.mubiaoPoint[1];
				}
				else
				{
					result = this.mubiaoPoint[2];
				}
			}
		}
		else
		{
			bool flag2 = this.nForce == 1;
			if (flag2)
			{
				result = this.mubiaoPoint[2];
			}
			else
			{
				result = this.mubiaoPoint[1];
			}
		}
		return result;
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x00100374 File Offset: 0x000FE574
	public GameObject getMubiaoObj(int layerType, bool justLock = false)
	{
		GameObject gameObject = null;
		if (layerType != 1)
		{
			if (layerType != 2)
			{
				bool flag = this.screentarget != null && this.screentarget.NetTarget != null;
				if (flag)
				{
					gameObject = this.screentarget.NetTarget.defaultMesh;
				}
				bool flag2 = !justLock && gameObject == null;
				if (flag2)
				{
					gameObject = this.hitGamaObj[0];
				}
			}
			else
			{
				bool flag3 = this.screentarget != null && this.screentarget.NetTarget != null;
				if (flag3)
				{
					gameObject = this.screentarget.NetTarget.defaultMesh;
				}
				bool flag4 = gameObject == null && !justLock;
				if (flag4)
				{
					gameObject = ((this.nForce != 1) ? this.hitGamaObj[2] : this.hitGamaObj[1]);
				}
			}
		}
		else
		{
			bool flag5 = this.screentarget != null && this.screentarget.NetTarget != null;
			if (flag5)
			{
				gameObject = this.screentarget.NetTarget.defaultMesh;
			}
			bool flag6 = gameObject == null && !justLock;
			if (flag6)
			{
				gameObject = ((this.nForce != 1) ? this.hitGamaObj[1] : this.hitGamaObj[2]);
			}
		}
		return gameObject;
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x001004D4 File Offset: 0x000FE6D4
	public RaycastHit getMubiaoRayHit(int layerType)
	{
		RaycastHit result;
		if (layerType != 1)
		{
			if (layerType != 2)
			{
				result = this.mubiaoPointHit[0];
			}
			else
			{
				bool flag = this.nForce == 1;
				if (flag)
				{
					result = this.mubiaoPointHit[1];
				}
				else
				{
					result = this.mubiaoPointHit[2];
				}
			}
		}
		else
		{
			bool flag2 = this.nForce == 1;
			if (flag2)
			{
				result = this.mubiaoPointHit[2];
			}
			else
			{
				result = this.mubiaoPointHit[1];
			}
		}
		return result;
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x0010055C File Offset: 0x000FE75C
	public void OnFillBullet(ArrayList data)
	{
		bool flag = !(this.heroBase == null) && !(this.wc == null);
		if (flag)
		{
			this.heroBase.BulletFilled();
			this.playerCommon.aryBulletCartridgeID[(int)data[1]] = (int)data[3];
			this.wc.fullBullet(data);
		}
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x001005D8 File Offset: 0x000FE7D8
	public void ChangeFpsTpsState()
	{
		bool flag = this.FpsOrTps == 1;
		if (flag)
		{
			this.tpsRender.SetRenderMode(true);
			bool flag2 = !this.userModelFps.activeSelf;
			if (flag2)
			{
				this.userModelFps.SetActive(true);
			}
		}
		else
		{
			this.tpsRender.SetRenderMode(false);
			bool flag3 = this.fpsRender != null;
			if (flag3)
			{
				this.fpsRender.SetParticleSystem(false);
			}
			bool flag4 = this.userModelFps != null && this.userModelFps.activeSelf;
			if (flag4)
			{
				this.userModelFps.SetActive(false);
			}
		}
		this.playerEffectBase.ChangeBuffEffect();
		this.InitHeroWeapon(this.playerCommon.m_nModelIndex);
		bool flag5 = (this.isCopy || this.isDead) && this.userModelTps != null;
		if (flag5)
		{
			CHeroWeapon component = this.userModelTps.GetComponent<CHeroWeapon>();
			bool flag6 = component != null;
			if (flag6)
			{
				component.SetWeaponParts(true);
			}
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x001006F0 File Offset: 0x000FE8F0
	protected void Get_Stroke_Material_Array()
	{
		this.Stroke_Material_Array = new ArrayList();
		this.Stroke_Material_Object_Array = new List<Renderer>();
		int num = CFilmManager.Instance.filmForce(this);
		Renderer[] componentsInChildren = this.userModelTps.transform.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			foreach (Material material in componentsInChildren[i].materials)
			{
				bool flag = !(material == null) && material.name.Contains("hongxian");
				if (flag)
				{
					bool flag2 = num == this.playerCommon.m_nShiliServer;
					if (flag2)
					{
						UnityEngine.Object.DestroyImmediate(componentsInChildren[i].gameObject);
						break;
					}
					this.Stroke_Material_Array.Add(material);
					this.Stroke_Material_Object_Array.Add(componentsInChildren[i]);
				}
			}
		}
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x001007EA File Offset: 0x000FE9EA
	public virtual void setPlayerNewPos(Vector3 v3Pos)
	{
		this.playerCommon.pos = v3Pos;
		this.UserControl.transform.position = v3Pos;
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x0010080C File Offset: 0x000FEA0C
	public virtual bool CheckActiveStat()
	{
		return true;
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x00100820 File Offset: 0x000FEA20
	public void changeCameraState(int iCameraState, NetLiving.ChangeCameraStateEnd callBackFun, object callBackParam)
	{
		bool flag = !this.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = CameraControl.iCameraState == iCameraState;
			if (flag2)
			{
				if (callBackFun != null)
				{
					callBackFun(callBackParam);
				}
			}
			else
			{
				CameraControl.Instance.ChangeCameraState(iCameraState);
				bool flag3 = CameraControl.iCameraState == CameraState.FirstState;
				if (flag3)
				{
					this.FpsOrTps = 1;
				}
				else
				{
					this.FpsOrTps = 2;
				}
				this.ChangeFpsTpsState();
				if (callBackFun != null)
				{
					callBackFun(callBackParam);
				}
			}
		}
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x001008A0 File Offset: 0x000FEAA0
	public void SkillDataUpdate(int type, ArrayList list)
	{
		bool flag = type == 31101;
		bool flag2 = this.heroSkill == null;
		if (!flag2)
		{
			bool flag3 = flag;
			if (flag3)
			{
				this.heroSkill.ClearSkill();
			}
			for (int i = 0; i < list.Count; i++)
			{
				ArrayList arrayList = (ArrayList)list[i];
				this.heroSkill.InitSkill((int)arrayList[0], arrayList);
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.heroSkill.SwitchSkillGroup(this.playerCommon.m_nXingTai);
				bool flag5 = gameuiCon.inst != null && CFilmManager.Instance.filmParentNetId(this) == base.getNetid();
				if (flag5)
				{
					gameuiCon.inst.NewMainHeroSkill.showSkillUi(this);
				}
			}
		}
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x0010097E File Offset: 0x000FEB7E
	public virtual void OnRevive(ArrayList szNetMsg)
	{
		this.isDead = false;
		this.heroBase.Revive();
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x00100994 File Offset: 0x000FEB94
	protected void addZhunxingCompent()
	{
		string hzhunxingClass = this.heroData.hzhunxingClass;
		bool flag = hzhunxingClass != "0";
		if (flag)
		{
			string text = hzhunxingClass ?? "";
			Type type = Type.GetType(text);
			bool flag2 = type == null;
			if (flag2)
			{
				Debug.LogError("不存在的准星类名称:" + text);
			}
			this.playerZhunxingMe = (zhunxingBase)this.UserControl.AddComponent(type);
		}
		else
		{
			this.playerZhunxingMe = this.UserControl.AddComponent<zhunxingBase>();
		}
		this.playerZhunxingMe.init(this);
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00100A28 File Offset: 0x000FEC28
	public virtual bool superSkill(ArrayList arr = null)
	{
		return false;
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x00100A3B File Offset: 0x000FEC3B
	public virtual void OnStartSuperSkill(ArrayList arr = null)
	{
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x00100A40 File Offset: 0x000FEC40
	public virtual void SendMsgToClient(int NetID, ArrayList szNetMsg)
	{
		bool flag = NetID == 2000;
		if (flag)
		{
			ChatManager.GetInstance().OnHuDongNetMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
		}
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x00100A80 File Offset: 0x000FEC80
	public void ToDamage(int nPlace, int damageType, bool isdead, int nDamageValue, bool bOldDead)
	{
		if (isdead)
		{
			bool flag = !bOldDead;
			if (flag)
			{
				this.playerZhunxingMe.lastKill(nPlace);
				((CPlayerSoundMe)fGameManager.Heroplayer.playerSound).PlayJiZhongSound();
			}
		}
		else if (nPlace != 1)
		{
			if (nPlace != 2)
			{
				bool flag2 = damageType == 0;
				if (flag2)
				{
					this.playerZhunxingMe.bigHit();
					((CPlayerSoundMe)fGameManager.Heroplayer.playerSound).PlayJz_DaShangHaiSound();
					this.playerZhunxingMe.ZhunXingColor();
				}
			}
			else
			{
				this.playerZhunxingMe.Shield_Hit();
			}
		}
		else
		{
			this.playerZhunxingMe.baotouHit();
			((CPlayerSoundMe)fGameManager.Heroplayer.playerSound).PlayJz_BaoTouSound();
		}
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x00100B40 File Offset: 0x000FED40
	public void SendMsgToAllClient(int nNetID, ArrayList szNetMsg)
	{
		if (nNetID <= 51003)
		{
			if (nNetID <= 31000)
			{
				if (nNetID != 1306)
				{
					if (nNetID != 10022)
					{
						if (nNetID == 31000)
						{
							int num = (int)szNetMsg[0];
							ArrayList arrayList = (ArrayList)szNetMsg[1];
							int num2 = num;
							int num3 = num2;
							if (num3 != 31209)
							{
								if (num3 == 31212)
								{
									bool flag = this.heroSkill != null;
									if (flag)
									{
										this.heroSkill.OnUpdateItemNum((int)arrayList[0], (int)arrayList[1]);
									}
								}
							}
							else
							{
								this.OnSuperData(arrayList);
							}
						}
					}
					else
					{
						this.OnRevive(szNetMsg);
					}
				}
				else
				{
					bool isMainPlayer = this.isMainPlayer;
					if (isMainPlayer)
					{
						HeroDataManager.GetInstance().OnHeroDataMsg(szNetMsg);
					}
					else
					{
						this.playerCommon.OnHeroDataMsg((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
					}
				}
			}
			else if (nNetID <= 50001)
			{
				if (nNetID != 31215)
				{
					if (nNetID == 50001)
					{
						this.DAZHAOToClient(szNetMsg);
					}
				}
				else
				{
					this.OnRoundEndClear(szNetMsg);
				}
			}
			else if (nNetID != 51002)
			{
				if (nNetID == 51003)
				{
					this.DeleteBuff(szNetMsg);
				}
			}
			else
			{
				this.AddBuff(szNetMsg);
			}
		}
		else if (nNetID <= 2040000)
		{
			if (nNetID <= 2010002)
			{
				if (nNetID != 1030000)
				{
					if (nNetID == 2010002)
					{
						this.boradCastDaJiHunDun(szNetMsg);
					}
				}
				else
				{
					Door_Control.Get_Instantiate.Special_Door_Handle((int)szNetMsg[0], szNetMsg[1].ToString());
					Door_Control.Get_Instantiate.General_Door_Handle((int)szNetMsg[0], szNetMsg[1].ToString(), true);
				}
			}
			else if (nNetID != 2030002)
			{
				if (nNetID == 2040000)
				{
					this.Hero_Message.GuanyinHandle((int)szNetMsg[0], (ArrayList)szNetMsg[1]);
				}
			}
			else
			{
				this.boradCastShawangSuper(szNetMsg);
			}
		}
		else if (nNetID <= 2210001)
		{
			if (nNetID != 2080001)
			{
				if (nNetID == 2210001)
				{
					this.LLS_FlyShieldData(szNetMsg);
				}
			}
			else
			{
				this.BlackCC(szNetMsg);
			}
		}
		else if (nNetID != 4010001)
		{
			if (nNetID == 4010004)
			{
				RobotAIManager.Instance.EndRobotAIForUser((int)szNetMsg[0]);
			}
		}
		else
		{
			RobotAIManager.Instance.BeginRobotAIForUser((int)szNetMsg[0], (int)szNetMsg[2]);
		}
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00100E38 File Offset: 0x000FF038
	public virtual void BlackCC(ArrayList szNetMsg)
	{
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00100E3B File Offset: 0x000FF03B
	public virtual void AddBuff(ArrayList arrPam)
	{
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x00100E3E File Offset: 0x000FF03E
	public virtual void DeleteBuff(ArrayList arrPam)
	{
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x00100E41 File Offset: 0x000FF041
	public virtual void DAZHAOToClient(ArrayList arrPam)
	{
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x00100E44 File Offset: 0x000FF044
	public virtual void boradCastShawangSuper(ArrayList szNetMsg)
	{
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x00100E47 File Offset: 0x000FF047
	public virtual void boradCastDaJiHunDun(ArrayList szNetMsg)
	{
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x00100E4A File Offset: 0x000FF04A
	public virtual void OnRoundEndClear(ArrayList szNetMsg)
	{
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x00100E4D File Offset: 0x000FF04D
	public virtual void SuperSkillUpdate(object parm)
	{
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00100E50 File Offset: 0x000FF050
	public virtual void LLS_FlyShieldData(ArrayList arrPam)
	{
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x00100E54 File Offset: 0x000FF054
	public void ChangeSpeed()
	{
		float animSpeedMove = this.playerCommon.m_fMoveSpeed * (1f + this.playerCommon.buffSpeed);
		this.tChar.animSpeedMove = animSpeedMove;
		this.tChar.airSpeedMax = 5f * this.playerCommon.m_fJumpSpeed * (1f + this.playerCommon.buffSpeed);
		this.tChar.MoveSpeedNoneAnimation = this.playerCommon.m_fMoveSpeedAnimation * (1f + this.playerCommon.buffSpeed);
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x00100EE4 File Offset: 0x000FF0E4
	public void NSS(ArrayList _Parameters)
	{
		float x = (float)Convert.ToInt32(_Parameters[0]) * 1f / 100f;
		float y = (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f;
		float z = (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f;
		bool flag = this.UserControl != null;
		if (flag)
		{
			this.UserControl.transform.localScale = new Vector3(x, y, z);
		}
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x00100F6B File Offset: 0x000FF16B
	public virtual void NVM(ArrayList _Parameters)
	{
	}

	// Token: 0x04001595 RID: 5525
	private NetLiving.ChangeCameraStateEnd changeCameraStateEndFun;

	// Token: 0x04001596 RID: 5526
	public PublicInfo publicInfo;

	// Token: 0x04001597 RID: 5527
	public CPlayerSkill heroSkill;

	// Token: 0x04001598 RID: 5528
	private int _FpsOrTps = 2;

	// Token: 0x04001599 RID: 5529
	public int SyncCount;

	// Token: 0x0400159A RID: 5530
	public int InJiDi;

	// Token: 0x0400159B RID: 5531
	public float jiDiTimeOut;

	// Token: 0x0400159C RID: 5532
	private Npc_Handle _Npc_Handle_Component;

	// Token: 0x0400159D RID: 5533
	public bool playerHaveCreate;

	// Token: 0x0400159E RID: 5534
	public HeroData heroData;

	// Token: 0x0400159F RID: 5535
	public int loadID;

	// Token: 0x040015A0 RID: 5536
	public CPlayerSound playerSound;

	// Token: 0x040015A1 RID: 5537
	public bool netinitok;

	// Token: 0x040015A2 RID: 5538
	public RaycastHit[] mubiaoPointHit = new RaycastHit[3];

	// Token: 0x040015A3 RID: 5539
	public Vector3[] mubiaoPoint = new Vector3[3];

	// Token: 0x040015A4 RID: 5540
	public GameObject[] hitGamaObj = new GameObject[3];

	// Token: 0x040015A5 RID: 5541
	public Vector3 mubiaoUp;

	// Token: 0x040015A6 RID: 5542
	public CMeshRenderBianSe renderBianSe;

	// Token: 0x040015A7 RID: 5543
	public CMeshRenderBianSe FPSrenderBianSe;

	// Token: 0x040015A8 RID: 5544
	public CPlayerDazhao playerDazhao;

	// Token: 0x040015A9 RID: 5545
	public CScreenTarget screentarget;

	// Token: 0x040015AA RID: 5546
	public CAiming aimSystem;

	// Token: 0x040015AB RID: 5547
	public HeroBase heroBase;

	// Token: 0x040015AC RID: 5548
	public GameObject UserControl;

	// Token: 0x040015AD RID: 5549
	public GameObject userModelFps;

	// Token: 0x040015AE RID: 5550
	public RuntimeAnimatorController userAnimatorFps;

	// Token: 0x040015AF RID: 5551
	public GameObject userModelTps;

	// Token: 0x040015B0 RID: 5552
	public RuntimeAnimatorController userAnimatorTps;

	// Token: 0x040015B1 RID: 5553
	public GameObject FindMubiaoTarget;

	// Token: 0x040015B2 RID: 5554
	public GameObject playerTarget;

	// Token: 0x040015B3 RID: 5555
	public TBCharAnimationPlay tCharAnimationFps;

	// Token: 0x040015B4 RID: 5556
	public TBCharAnimationThirdIK tCharAnimationTps;

	// Token: 0x040015B5 RID: 5557
	public Dictionary<string, CAnimatorParm> animatorParmObject = new Dictionary<string, CAnimatorParm>();

	// Token: 0x040015B6 RID: 5558
	public TBCharacterThird tChar;

	// Token: 0x040015B7 RID: 5559
	public TBUserControl charMove;

	// Token: 0x040015B8 RID: 5560
	public bool canControl = true;

	// Token: 0x040015B9 RID: 5561
	private Vector3 _userCore;

	// Token: 0x040015BA RID: 5562
	public Vector3 _userCoreAdd = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040015BB RID: 5563
	public GameObject _playerSoundContainer;

	// Token: 0x040015BC RID: 5564
	public GameObject _playerEffectContainer;

	// Token: 0x040015BD RID: 5565
	public GameObject _playerRootEffectContainer;

	// Token: 0x040015BE RID: 5566
	private Vector3 _userCenter;

	// Token: 0x040015BF RID: 5567
	public Vector3 _userCenterAdd = new Vector3(0f, 1.1f, 0f);

	// Token: 0x040015C0 RID: 5568
	private Transform _userHeadTransform;

	// Token: 0x040015C1 RID: 5569
	private Transform _userSpine;

	// Token: 0x040015C2 RID: 5570
	private Transform _userBip;

	// Token: 0x040015C3 RID: 5571
	public float disCameraNow;

	// Token: 0x040015C4 RID: 5572
	public float disNow = 10f;

	// Token: 0x040015C5 RID: 5573
	public PlayerCommon playerCommon;

	// Token: 0x040015C6 RID: 5574
	private GameObject _thisModel;

	// Token: 0x040015C7 RID: 5575
	public GameObject userRoot;

	// Token: 0x040015C8 RID: 5576
	public GameObject defaultMesh;

	// Token: 0x040015C9 RID: 5577
	public Collider defaultCollider;

	// Token: 0x040015CA RID: 5578
	public Rigidbody defaultRigidbody;

	// Token: 0x040015CB RID: 5579
	public bool RollDead;

	// Token: 0x040015CC RID: 5580
	public Collider bodyCollider;

	// Token: 0x040015CD RID: 5581
	public Rigidbody bodyRigidbody;

	// Token: 0x040015CE RID: 5582
	public string modelName;

	// Token: 0x040015CF RID: 5583
	public bool canKill = true;

	// Token: 0x040015D0 RID: 5584
	public int nForce;

	// Token: 0x040015D1 RID: 5585
	public Vector3 forceV3;

	// Token: 0x040015D2 RID: 5586
	public float forceMax;

	// Token: 0x040015D3 RID: 5587
	public bool isDead;

	// Token: 0x040015D4 RID: 5588
	public int deadNo;

	// Token: 0x040015D5 RID: 5589
	public bool isInitDead;

	// Token: 0x040015D6 RID: 5590
	public CWeaponControl wc;

	// Token: 0x040015D7 RID: 5591
	private bool _needSendToServer;

	// Token: 0x040015D8 RID: 5592
	public SkillInfo skillInfo;

	// Token: 0x040015D9 RID: 5593
	public string modelUrl;

	// Token: 0x040015DA RID: 5594
	public DateTime livingDamageDate;

	// Token: 0x040015DB RID: 5595
	public CHeroRender tpsRender;

	// Token: 0x040015DC RID: 5596
	public CHeroRender fpsRender;

	// Token: 0x040015DD RID: 5597
	public Dictionary<string, GameObject> colliderCankill = new Dictionary<string, GameObject>();

	// Token: 0x040015DE RID: 5598
	public ArrayList rigidbodyAll = new ArrayList();

	// Token: 0x040015DF RID: 5599
	public Vector3 forceDamage;

	// Token: 0x040015E0 RID: 5600
	public int nHuDongState;

	// Token: 0x040015E1 RID: 5601
	public bool HuDongChangeCameraIng;

	// Token: 0x040015E2 RID: 5602
	public CHitReaction hitReaction;

	// Token: 0x040015E3 RID: 5603
	public bool haveDestroy;

	// Token: 0x040015E4 RID: 5604
	public DateTime OnReviveTime;

	// Token: 0x040015E5 RID: 5605
	public buffIt playerBuffIt;

	// Token: 0x040015E6 RID: 5606
	public effectMe playerEffectBase;

	// Token: 0x040015E7 RID: 5607
	public zhunxingBase playerZhunxingMe;

	// Token: 0x040015E8 RID: 5608
	public bool mModelTopObjbool = true;

	// Token: 0x040015E9 RID: 5609
	public GameObject oGameUISignObj;

	// Token: 0x040015EA RID: 5610
	public NewPlayerTopUICon newplayercon;

	// Token: 0x040015EB RID: 5611
	public Dictionary<string, CAData> cAnimationData;

	// Token: 0x040015EC RID: 5612
	public DateTime FlyShileldBuffTime = DateTime.MinValue;

	// Token: 0x040015ED RID: 5613
	private Transform _camTarget;

	// Token: 0x040015EE RID: 5614
	protected Transform _UserHeadTarget;

	// Token: 0x040015EF RID: 5615
	private ArrayList _Stroke_Material_Array;

	// Token: 0x040015F0 RID: 5616
	private List<Renderer> _Stroke_Material_Object_Array;

	// Token: 0x040015F1 RID: 5617
	private Dictionary<string, List<Npc_Entity>> _Adhere_Child_Object_Data;

	// Token: 0x040015F2 RID: 5618
	private Hero_Message _Hero_Message;

	// Token: 0x040015F3 RID: 5619
	private Hero_Message_Handle _Hero_Message_Handle;

	// Token: 0x040015F4 RID: 5620
	private List<Renderer> szHongwaiMat;

	// Token: 0x040015F5 RID: 5621
	private List<Renderer> szFuhuoMat = new List<Renderer>();

	// Token: 0x040015F6 RID: 5622
	private bool _heroInvi;

	// Token: 0x040015F7 RID: 5623
	private bool _bePlayActive = true;

	// Token: 0x040015F8 RID: 5624
	private bool _beSuperSkill;

	// Token: 0x040015F9 RID: 5625
	public Dictionary<int, SkillBase> skillEffectDict = new Dictionary<int, SkillBase>();

	// Token: 0x040015FA RID: 5626
	private NetLiving _GameAttackPlayer;

	// Token: 0x020005E9 RID: 1513
	// (Invoke) Token: 0x0600365F RID: 13919
	public delegate void ChangeCameraStateEnd(object objParam);
}
