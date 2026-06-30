using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x020001AF RID: 431
public class HeroPlayer : GamePlayer
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x060013BD RID: 5053 RVA: 0x000BAAB5 File Offset: 0x000B8CB5
	public override bool needSendToServer
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x060013BE RID: 5054 RVA: 0x000BAAB8 File Offset: 0x000B8CB8
	// (set) Token: 0x060013BF RID: 5055 RVA: 0x000BAAD0 File Offset: 0x000B8CD0
	public bool Slay_Death_State
	{
		get
		{
			return this._Slay_Death_State;
		}
		set
		{
			this._Slay_Death_State = value;
		}
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000BAADC File Offset: 0x000B8CDC
	public HeroPlayer()
	{
		fGameManager.GetInstance().CloseLink(3);
		this.filmObj = true;
		this.playerCommon = PlayerDataManager.getInstance().playercom;
		fGameManager.Heroplayer = this;
		fGameManager.m_nPreLoadOff = true;
		fGameManager.m_nUserType = 1;
		MatchManager.GetInstance().isEnterFb = false;
		ChatManager.GetInstance().OnInitUserChat();
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x000BABBC File Offset: 0x000B8DBC
	public void playHeroEnterSceneSound()
	{
		bool flag = !(this.playerSound == null);
		if (flag)
		{
			bool flag2 = ((CPlayerSoundMe)this.playerSound).CanPlayEnterSceneSound();
			if (flag2)
			{
				((CPlayerSoundMe)this.playerSound).PlayEnterSceneSound();
			}
			else
			{
				((CPlayerSoundMe)this.playerSound).FpsLoadedFinish(true);
			}
		}
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x000BAC1C File Offset: 0x000B8E1C
	public override void SetAnimator(string ani, int type, object parm, int sendtype = 1)
	{
		bool flag = !(ani == "0") && this.playerHaveCreate;
		if (flag)
		{
			bool flag2 = ani.Contains("AttackState") && (int)parm > 0;
			if (flag2)
			{
				CameraControl.Instance.onceFire();
			}
			this.charMove.SetAnimator(ani, type, parm, sendtype);
		}
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x000BAC81 File Offset: 0x000B8E81
	public override void SetAnimatorUpdate(string ani, int type, object parm)
	{
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x000BAC84 File Offset: 0x000B8E84
	public override void setNetid(int netid)
	{
		base.setNetid(netid);
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x000BAC8F File Offset: 0x000B8E8F
	public override void SendMsgToClient(int NetID, ArrayList szNetMsg)
	{
		CNetMsgManager.Instance.MSGClient(this, NetID, szNetMsg);
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x000BACA0 File Offset: 0x000B8EA0
	public void CN(int no)
	{
		this.pingDelayBegin = Time.realtimeSinceStartup;
		bool flag = no == 1 && this.playerLocal != null;
		if (flag)
		{
			this.playerLocal.onceMsgPingBack();
		}
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x000BACDE File Offset: 0x000B8EDE
	public override void SendMessageToServer(int nNetID, ArrayList szNetMsg)
	{
		this.TosFun("SendMsgToServer", new object[]
		{
			nNetID,
			szNetMsg
		});
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x000BAD00 File Offset: 0x000B8F00
	public void MustToFun(string funname, params object[] args)
	{
		bool flag = this.cs != null;
		if (flag)
		{
			this.cs.ToServerFunReady(this, true, funname, args);
		}
		else
		{
			Fun.SocketLog("ToServer: " + funname + "," + BaseSocket.GetShortStr(args).ToString());
		}
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x000BAD52 File Offset: 0x000B8F52
	public override void SendSkillParms(ArrayList list)
	{
		this.SendMessageToServer(31401, list);
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x000BAD62 File Offset: 0x000B8F62
	public void CloseGame()
	{
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x000BAD68 File Offset: 0x000B8F68
	public static void initMainCamera()
	{
		GameObject gameObject = GameObject.Find("scCamera");
		bool flag = gameObject != null;
		CameraControl cameraControl;
		if (flag)
		{
			gameObject.name = "mainCamera";
			gameObject.transform.parent = null;
			cameraControl = gameObject.AddComponent<CameraControl>();
			cameraControl.GameCameraInit(fGameManager.Heroplayer);
			cameraControl.controlCamera = gameObject.GetComponent<Camera>();
		}
		else
		{
			gameObject = new GameObject("mainCamera");
			cameraControl = gameObject.AddComponent<CameraControl>();
			cameraControl.GameCameraInit(fGameManager.Heroplayer);
			cameraControl.controlCamera = gameObject.AddComponent<Camera>();
		}
		AudioListener component = gameObject.GetComponent<AudioListener>();
		bool flag2 = component != null;
		if (flag2)
		{
			component.enabled = false;
			UnityEngine.Object.Destroy(component);
		}
		cameraControl.transform.tag = "MainCamera";
		cameraControl.controlCamera.cullingMask = ~(1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("FrontCamera"));
		cameraControl.controlCamera.nearClipPlane = 0.1f;
		cameraControl.controlCamera.backgroundColor = Color.black;
		cameraControl.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
		cameraControl.controlCamera.depth = -1f;
		CameraMotionBlur component2 = cameraControl.GetComponent<CameraMotionBlur>();
		bool flag3 = component2 != null;
		if (flag3)
		{
			component2.excludeLayers = ~(1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
		}
		cameraControl.transform.gameObject.AddComponent<SC_shakeCamera>();
		GameObject gameObject2 = new GameObject("deepCamera");
		cameraControl.controlCameraSec = gameObject2.AddComponent<Camera>();
		cameraControl.controlCameraSec.CopyFrom(cameraControl.controlCamera);
		cameraControl.controlCameraSec.renderingPath = RenderingPath.UsePlayerSettings;
		cameraControl.controlCameraSec.cullingMask = 1 << LayerMask.NameToLayer("FrontCamera");
		cameraControl.controlCameraSec.depth = 0f;
		cameraControl.controlCameraSec.nearClipPlane = 0.01f;
		cameraControl.controlCameraSec.farClipPlane = 10f;
		cameraControl.controlCameraSec.clearFlags = CameraClearFlags.Depth;
		cameraControl.controlCameraSec.transform.parent = cameraControl.transform;
		cameraControl.controlCameraSec.transform.localPosition = Vector3.zero;
		cameraControl.controlCameraSec.transform.localRotation = Quaternion.identity;
		cameraControl.gameObject.SetActive(false);
		CCOpyNameManager.Instance.InitCopyName();
		fGameManager.Heroplayer.initCameraTime = DateTime.Now;
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x000BB00B File Offset: 0x000B920B
	public override void OnCreate()
	{
		base.FpsOrTps = 1;
		fGameManager.GetInstance().setPlayer(this);
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x000BB022 File Offset: 0x000B9222
	public void MatchUserInfo(ArrayList aryNetInfo)
	{
		fGameManager.nJoinFbTime = Time.realtimeSinceStartup;
		this.robotList = new List<int>();
		this.InitPlayerInfo((ArrayList)aryNetInfo[0], (ArrayList)aryNetInfo[1], true);
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x000BB05C File Offset: 0x000B925C
	public void PlayerCreate(ArrayList aryNetInfo)
	{
		this.InitPlayerInfo((ArrayList)aryNetInfo[0], (ArrayList)aryNetInfo[1], false);
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.clear(1);
		}
		bool flag2 = this.playerCommon.m_nGM > 0 && this.playerCommon.m_szGMTran.Count > 0;
		if (flag2)
		{
			this.userRoot = new GameObject(string.Format("User_{0}", base.getNetid()));
			this.userRoot.transform.position = Vector3.zero;
			this.userRoot.transform.eulerAngles = Vector3.zero;
			this.UserControl = new GameObject("control");
			this.UserControl.layer = LayerMask.NameToLayer("Ignore Raycast");
			this.UserControl.transform.parent = this.userRoot.transform;
			bool flag3 = this.playerCommon.posCreate != Vector3.zero;
			if (flag3)
			{
				this.playerCommon.pos = this.playerCommon.posCreate;
				this.playerCommon.posCreate = Vector3.zero;
			}
			this.UserControl.transform.position = this.playerCommon.pos;
			this.UserControl.transform.eulerAngles = new Vector3(0f, this.playerCommon.m_nForward, 0f);
			this.playerLocal = this.UserControl.AddComponent<PlayerLocal>();
			GameRoot.inst.InitGMCamera();
		}
		else
		{
			base.addPlayer();
			GameObject gameObject = new GameObject("checkObj");
			gameObject.transform.SetParent(this.userRoot.transform);
			gameObject.transform.position = Vector3.one * -100f;
			this.checkRig = gameObject.AddComponent<Rigidbody>();
		}
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x000BB25A File Offset: 0x000B945A
	protected override void setHeroForce()
	{
		base.setHeroForce();
		HeroPlayer.heroForce = this.nForce;
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x000BB26F File Offset: 0x000B946F
	public override void PlayerSelect(ArrayList aryNetInfo)
	{
		UnityEngine.Object.Destroy(this.userRoot);
		base.FpsOrTps = 1;
		this.PlayerCreate(aryNetInfo);
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x000BB290 File Offset: 0x000B9490
	protected override void InitPlayerInfo(ArrayList msg, ArrayList msg2, bool isNullNetplayer = false)
	{
		this.isMainPlayer = true;
		base.InitPlayerInfo(msg, msg2, isNullNetplayer);
		if (isNullNetplayer)
		{
			fUIManager.GetInstance().CloseAll();
		}
		else
		{
			CameraControl.iCameraState = CameraState.FirstState;
			GameRoot.inst.Setting.Use_Setting(this.playerCommon.m_nModelIndex);
		}
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000BB2E8 File Offset: 0x000B94E8
	protected override void addMySelfCompent()
	{
		this.publicInfo.type = "HeroPlayer";
		fUISoundManager.instance.StopSound("music_commen", 0);
		GameRoot.inst.audioEar.transform.SetParent(CameraControl.Instance.transform, false);
		GameRoot.inst.audioEar.transform.localPosition = Vector3.zero;
		CameraControl.Instance.transform.position = this.playerCommon.pos + this.heroData.FPSCameraPos[1];
		CameraControl.Instance.transform.eulerAngles = new Vector3(0f, this.playerCommon.m_nForward, 0f);
		CameraControl.Instance.transform.gameObject.SetActive(true);
		CameraControl.Instance.initCameraAngle(this.camTarget, this.playerCommon.m_nForward);
		CCameraChange.Instance.InitCameraChange(this);
		this.playerCommon.negativePlay_Walk = 0f;
		this.playerCommon.negativePlay_Attack = 0f;
		bool flag = Environment.TickCount < 0;
		if (flag)
		{
			FunManager.Instance.Send(this, "-1", string.Format("玩家的系统时钟出现问题，名字:{0}，时钟:{1}", this.playerCommon.m_sName, Environment.TickCount));
		}
		this.tChar = this.UserControl.AddComponent<TBCharacterThirdMe>();
		this.playerRuntimeData = this.UserControl.AddComponent<PlayerRuntimeData>();
		this.playerRuntimeData.SetHeroPlayer(this);
		base.addSkillCompent();
		base.addZhunxingCompent();
		this.charMove = this.UserControl.AddComponent<TBUserControl>();
		this.charMove.InitControl(this);
		this.playerBuffMe = this.UserControl.AddComponent<buffMe>();
		this.playerBuffMe.OneBuffInit(this);
		this.playerEffectBase = this.UserControl.AddComponent<effectMe>();
		this.playerEffectBase.gameplayer = this;
		this.playerLocal = this.UserControl.AddComponent<PlayerLocal>();
		this.wInput = this.userRoot.AddComponent<CWeaponInput>();
		this.wInput.InitWeaponInput(this);
		base.LoadTpsAnimator();
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000BB518 File Offset: 0x000B9718
	protected override void ModeLoadOK()
	{
		CCheckDelay.Instance.resetTime();
		bool flag = !this.netinitok;
		if (flag)
		{
			this.OnCreateModelTime_Log(0);
		}
		base.ModeLoadOK();
		bool flag2 = !this.isDead;
		if (flag2)
		{
			User_Data_Handle.CanWatchTeamates = false;
		}
		CameraControl.Instance.InitCameraHero(this);
		fUIManager.GetInstance().openGameUi(this, 1);
		CameraControl.Instance.transform.position = this.playerCommon.pos + CameraControl.Instance.fpsCameraTargetPostion;
		CameraControl.Instance.transform.eulerAngles = new Vector3(0f, this.playerCommon.m_nForward, 0f);
		CameraControl.Instance.transform.gameObject.SetActive(true);
		CCameraChange.Instance.InitCameraChange(this);
		CameraControl.Instance.initCameraAngle(this.camTarget, this.camTarget.eulerAngles.y);
		CameraControl.Instance.ChangeCameraState(CameraControl.iCameraState);
		this.cplace = this.UserControl.AddComponent<CCheckPlace>();
		this.cplace.initStart();
		this.playHeroEnterSceneSound();
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000BB648 File Offset: 0x000B9848
	protected override void CreateDead()
	{
		base.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.DeadOrReviveChangeCameraState), string.Empty);
		base.CreateDead();
		bool flag = ArenaCon.inst != null && this.isDead;
		if (flag)
		{
			base.changeCameraState(CameraState.DeadState, new NetLiving.ChangeCameraStateEnd(this.DeadOrReviveChangeCameraState), string.Empty);
			this.playerZhunxingMe.zhunxingshowOrHide(false);
			User_Data_Handle.CanWatchTeamates = true;
			User_Data_Handle.Watch_Fight_State = true;
			User_Data_Handle.Watch_Fight(this.playerCommon.m_nShili);
			CameraControl.iCameraCtrl = CameraCtrlState.CameraGuanZhan;
		}
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x000BB6E8 File Offset: 0x000B98E8
	public void TPSLoadEnd(WWW www, object obj)
	{
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000BB6EC File Offset: 0x000B98EC
	public override void addHeroCompent(string className)
	{
		bool flag = className != "0";
		if (flag)
		{
			Type type = Type.GetType("Hero" + className + "Me");
			this.heroBase = (HeroBaseMe)this.UserControl.AddComponent(type);
		}
		else
		{
			this.heroBase = this.UserControl.AddComponent<HeroBaseMe>();
		}
		this.heroBase.HeroInit(this, className);
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000BB75C File Offset: 0x000B995C
	public override void addOneSkillCompent(int iSkillIndex, string className)
	{
		bool flag = className != "0";
		string text;
		Type type;
		if (flag)
		{
			text = className + "Me";
			type = Type.GetType(text);
		}
		else
		{
			text = string.Format("{0}Me", "SkillBase");
			type = Type.GetType(text);
		}
		bool flag2 = type == null;
		if (flag2)
		{
			Debug.LogError("不存在的技能类名称:" + text);
		}
		SkillBaseMe skillBaseMe = (SkillBaseMe)this.UserControl.AddComponent(type);
		skillBaseMe.SkillInit(this, iSkillIndex);
		this.skillEffectDict.Add(iSkillIndex, skillBaseMe);
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x000BB7F4 File Offset: 0x000B99F4
	public override bool GetAnimatorBool(string ani)
	{
		bool flag = this.charMove == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			result = (!flag2 && this.tCharAnimationTps.GetAnimatorBool(ani));
		}
		return result;
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x000BB83C File Offset: 0x000B9A3C
	public override float GetAnimatorFloat(string ani)
	{
		bool flag = this.charMove == null;
		float result;
		if (flag)
		{
			result = -999f;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			if (flag2)
			{
				result = -999f;
			}
			else
			{
				result = this.tCharAnimationTps.GetAnimatorFloat(ani);
			}
		}
		return result;
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000BB88C File Offset: 0x000B9A8C
	public override int GetAnimatorInteger(string ani)
	{
		bool flag = this.charMove == null;
		int result;
		if (flag)
		{
			result = -999;
		}
		else
		{
			bool flag2 = this.tCharAnimationTps == null;
			if (flag2)
			{
				result = -999;
			}
			else
			{
				result = this.tCharAnimationTps.GetAnimatorInteger(ani);
			}
		}
		return result;
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x000BB8DC File Offset: 0x000B9ADC
	public override void HaveDamage(ArrayList szDamage, ArrayList szHp)
	{
		float hpBiLi = this.playerCommon.GetHpBiLi();
		base.HaveDamage(szDamage, szHp);
		int num = (int)szDamage[0];
		int nAttackId = (int)szDamage[3];
		int num2 = (int)szDamage[9];
		int num3 = (int)szDamage[6];
		int soundType = (int)szDamage[12];
		bool flag = !this.playerCommon.m_nStatForce;
		if (flag)
		{
			int num4 = (int)szDamage[2];
			ArrayList arrayList = (ArrayList)szDamage[1];
			bool flag2 = arrayList.Count > 1;
			if (flag2)
			{
				this.forceDamage = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			}
			bool flag3 = num4 > 0 && arrayList.Count > 0;
			if (flag3)
			{
				this.AddCharForce(this.forceDamage * (float)num4 * 0.01f);
			}
		}
		bool flag4 = num == 1;
		if (flag4)
		{
		}
		this.OnIsPinSiEffect();
		bool flag5 = szHp.Count > 0;
		if (flag5)
		{
			ArrayList arrayList2 = (ArrayList)szHp[7];
			int nHpEffType = this.playerCommon.m_nHpEffType;
			bool flag6 = nHpEffType == 2;
			if (flag6)
			{
				int nHpValue = (int)arrayList2[0] + (int)arrayList2[1] + (int)arrayList2[2] + (int)arrayList2[3];
				this.ToHurtDiection((ArrayList)szDamage[10], nAttackId, nHpValue);
			}
			bool flag7 = nHpEffType == 1;
			if (flag7)
			{
				bool flag8 = (GameResultUICon.Inst != null && GameResultUICon.Inst.visible) || (GameShouUICon.Inst != null && GameShouUICon.Inst.visible);
				if (flag8)
				{
					return;
				}
				((CPlayerSoundMe)this.playerSound).PlayJiaXueSound();
			}
			bool flag9 = num3 == 0 && nHpEffType == 2 && !this.isDead;
			if (flag9)
			{
				this.playerSound.PlayHurtSound(soundType);
			}
			bool flag10 = hpBiLi < 1f && this.playerCommon.GetHpBiLi() == 1f && (int)szHp[9] != 5;
			if (flag10)
			{
				((CPlayerSoundMe)this.playerSound).PlayRecoverySound();
			}
		}
		bool flag11 = num2 == 1;
		if (flag11)
		{
			SC_shakeCamera.shakeCamera();
		}
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x000BBB94 File Offset: 0x000B9D94
	private void ToHurtDiection(ArrayList bulletBorn, int nAttackId, int nHpValue)
	{
		bool flag = !(this.thisModel == null) && !this.haveDestroy && ((int)bulletBorn[0] != 0 || (int)bulletBorn[1] != 0 || (int)bulletBorn[2] != 0);
		if (flag)
		{
			Vector3 vector = new Vector3((float)((int)bulletBorn[0]) * 0.01f, (float)((int)bulletBorn[1]) * 0.01f, (float)((int)bulletBorn[2]) * 0.01f);
			Transform transform = this.thisModel.transform;
			Vector3 position = transform.position;
			Vector3 from = vector - position;
			from.y = 0f;
			Vector3 forward = transform.forward;
			forward.y = 0f;
			float num = Vector3.Angle(from, forward);
			bool flag2 = transform.InverseTransformPoint(vector).x > 0f;
			if (flag2)
			{
				num = 360f - num;
			}
			bool flag3 = gameuiCon.inst != null;
			if (flag3)
			{
				gameuiCon.inst.mainMsgUI.OnHurtEffect(nAttackId, num);
			}
		}
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x000BBCCC File Offset: 0x000B9ECC
	private string getHurtEffect(float fAngle)
	{
		string result = "shang";
		float num = fAngle + 22.5f;
		bool flag = num < 0f || num > 360f;
		if (flag)
		{
			num = 1f;
		}
		int num2 = (int)(num / 45f);
		bool flag2 = num2 < this.mHurtEffectData.Count;
		if (flag2)
		{
			result = this.mHurtEffectData[num2].ToString();
		}
		return result;
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x000BBD40 File Offset: 0x000B9F40
	public override void ChangeHeroDestroy()
	{
		bool flag = this.heroBase != null;
		if (flag)
		{
			((HeroBaseMe)this.heroBase).ChanngeLayerCollider(false);
		}
		base.ChangeHeroDestroy();
		bool flag2 = this.heroSkill != null;
		if (flag2)
		{
			this.heroSkill.destroyReadyItem();
		}
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000BBD98 File Offset: 0x000B9F98
	private void OnIsPinSiEffect()
	{
		float hpBiLi = this.playerCommon.GetHpBiLi();
		bool flag = hpBiLi < 0.25f;
		int num;
		if (flag)
		{
			((CPlayerSoundMe)this.playerSound).PlayPinSiSound();
			this.playerZhunxingMe.OnPlayPinSiEffect(1);
			num = 0;
		}
		else
		{
			((CPlayerSoundMe)this.playerSound).StopPinSiSound();
			this.playerZhunxingMe.OnPlayPinSiEffect(0);
			num = 1;
		}
		bool flag2 = this.nChangePinSiStatSound != num;
		if (flag2)
		{
			this.nChangePinSiStatSound = num;
			((CPlayerSoundMe)this.playerSound).PlayChangePinSiSound(num);
		}
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x000BBE38 File Offset: 0x000BA038
	public void OnShowDeadMsg()
	{
		string sDeadname = string.Empty;
		sDeadname = ((base.GameAttackPlayer == null) ? Language.GetLang(3001, new object[0]) : ((this.playerCommon.m_nCid != base.GameAttackPlayer.playerCommon.m_nCid) ? Language.GetLang(3002, new object[]
		{
			base.GameAttackPlayer.playerCommon.m_sName
		}) : Language.GetLang(3001, new object[0])));
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.mainMsgUI.OnDeadname(sDeadname);
			gameuiCon.inst.mainMsgUI.ClearHurtEffect();
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000BBEEC File Offset: 0x000BA0EC
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.deadNo++;
		bool playerHaveCreate = this.playerHaveCreate;
		if (playerHaveCreate)
		{
			this.SetAnimator("Reload", 0, false, 1);
		}
		base.OnDeath(_attackId, o, forcev3, forcemax, weaponName, launcherName, m_nIsSupperKill, m_szCanYuXiaoMie, nDeadType, _warData);
		this.playerZhunxingMe.zhunxingshowOrHide(false);
		bool flag = this.heroBase.Release_Musth_State || !this.playerHaveCreate || this.haveDestroy;
		if (!flag)
		{
			this.playerEffectBase.OnDeadClearEffect();
			this.playerZhunxingMe.OnDeath();
			this.playerRuntimeData.initData();
			bool flag2 = this.wc != null && this.wc.sInputFullKey.Length > 1;
			if (flag2)
			{
				((CWeaponControlMe)this.wc).fullBreakCancel();
			}
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
			base.changeCameraState(CameraState.DeadState, new NetLiving.ChangeCameraStateEnd(this.DeadOrReviveChangeCameraState), new ArrayList
			{
				"dead",
				_attackId
			});
			bool flag3 = netLiving != null;
			if (flag3)
			{
				CCameraChange.Instance.setFilmPlayer(netLiving);
				ArrayList arrayList = User_Data_Handle._getNpcType(_attackId);
				int num = (int)arrayList[0];
				bool flag4 = _attackId != base.getNetid();
				if (flag4)
				{
					int num2 = num;
					int num3 = num2;
					if (num3 - 1 > 1)
					{
						if (num3 == 3)
						{
							GamePlayer gamePlayer = (GamePlayer)arrayList[1];
							CameraControl.Instance.AttackTarget = gamePlayer.camTarget;
						}
					}
					else
					{
						CameraControl.Instance.AttackTarget = netLiving.camTarget;
					}
					bool flag5 = _attackId != 0;
					if (flag5)
					{
						CCameraChange.Instance.SetWatchTematesTimer(true, netLiving);
						User_Data_Handle.CanWatchTeamates = false;
						User_Data_Handle.Watch_Fight_State = true;
					}
				}
				else
				{
					CCameraChange.Instance.SetWatchTematesTimer(false, null);
					User_Data_Handle.CanWatchTeamates = true;
					User_Data_Handle.Watch_Fight_State = true;
				}
			}
			this.playerCommon.negativePlay_Walk = 0f;
			this.playerCommon.negativePlay_Attack = 0f;
			this.OnShowDeadMsg();
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x000BC124 File Offset: 0x000BA324
	public void DelayDeath(int _attackId)
	{
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(_attackId);
		bool flag = netLiving != null;
		if (flag)
		{
			CCameraChange.Instance.setFilmPlayer(netLiving);
		}
		this.playerEffectBase.OnDeadClearEffect();
		this.playerZhunxingMe.OnDeath();
		this.playerRuntimeData.initData();
		((CWeaponControlMe)this.wc).StopFulling();
		this.playerSound.PlayDeadSound(0);
		this.OnShowDeadMsg();
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x000BC1A8 File Offset: 0x000BA3A8
	public override void OnFightStat(int m_nFightStat, int m_nFightID)
	{
		base.OnFightStat(m_nFightStat, m_nFightID);
		this.playerCommon.m_nFightStat = m_nFightStat;
		bool flag = this.playerCommon.m_nIsFristFight == 0 && m_nFightStat == 1;
		if (flag)
		{
			this.playerCommon.m_nIsFristFight = 1;
		}
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x000BC1F4 File Offset: 0x000BA3F4
	public void DeadLookAt()
	{
		bool flag = base.GameAttackPlayer != null && !(base.GameAttackPlayer.UserControl == null);
		if (flag)
		{
			CameraControl.Instance.transform.LookAt(base.GameAttackPlayer.UserControl.transform);
		}
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000BC248 File Offset: 0x000BA448
	public override void AddBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffMe == null);
		if (flag)
		{
			this.playerBuffMe.AddOneBuff((string)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000BC29C File Offset: 0x000BA49C
	public override void DeleteBuff(ArrayList arrPam)
	{
		bool flag = !(this.playerBuffMe == null);
		if (flag)
		{
			this.playerBuffMe.DeleteOneBuff((int)arrPam[0], (int)arrPam[1], (int)arrPam[2]);
		}
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x000BC2F0 File Offset: 0x000BA4F0
	public override void LLS_FlyShieldData(ArrayList arrPam)
	{
		bool flag = this.skillEffectDict.ContainsKey(1163);
		if (flag)
		{
			((Skill_Lilisi_shiledMe)this.skillEffectDict[1163]).UpdateFlyShiled(arrPam);
		}
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x000BC330 File Offset: 0x000BA530
	public override void OnRevive(ArrayList szNetMsg)
	{
		CCameraChange.Instance.havePressedJ = false;
		bool flag = this.cplace != null;
		if (flag)
		{
			this.cplace.resetCheck(1f);
		}
		base.OnRevive(szNetMsg);
		bool flag2 = this.heroBase != null;
		if (flag2)
		{
			((HeroBaseMe)this.heroBase).ChanngeLayerCollider(true);
		}
		this.playerZhunxingMe.zhunxingshowOrHide(true);
		string a = szNetMsg[6].ToString();
		bool flag3 = a == "SkillRevivelight";
		if (flag3)
		{
			this.playerZhunxingMe.skillReviveEffect();
			base.changeCameraState(CameraState.ThirdState, new NetLiving.ChangeCameraStateEnd(this.DeadOrReviveChangeCameraState), new ArrayList
			{
				"revive",
				0
			});
			int netID = (int)szNetMsg[7];
			((HeroBaseMe)this.heroBase).Delay(netID);
		}
		else
		{
			base.changeCameraState(CameraState.FirstState, new NetLiving.ChangeCameraStateEnd(this.DeadOrReviveChangeCameraState), new ArrayList
			{
				"revive",
				0
			});
			bool flag4 = !this.roundBegin;
			if (flag4)
			{
				((CPlayerSoundMe)this.playerSound).PlayLoadReviveInBaseSound();
			}
		}
		this.playerRuntimeData.Deformation_State = false;
		this.playerZhunxingMe.Revive();
		CameraControl.iCameraCtrl = CameraCtrlState.CameraPlayerCtrl;
		CameraControl.Instance.InitCameraHero(this);
		this.nChangePinSiStatSound = 1;
		bool flag5 = CFilmManager.Instance.playing && a != "SkillRevivelight";
		if (flag5)
		{
			CFilmManager.Instance.FilmFinish(false, 0);
		}
		else
		{
			CFilmManager.Instance.FilmFinish(true, 1);
		}
		this.ChangeWeaponGroup(0);
		User_Data_Handle.Watch_Fight_State = false;
		CameraControl.Instance.initCameraAngle(this.camTarget, this.camTarget.eulerAngles.y);
		this.playerCommon.negativePlay_Walk = 0f;
		this.playerCommon.negativePlay_Attack = 0f;
		this.Slay_Death_State = false;
		roleuiManager.GetInstance().OnCloseSelectHeroUI();
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x000BC55C File Offset: 0x000BA75C
	public void LuoJiHurtLeiJi(ArrayList list)
	{
		int num = (int)list[0];
		int key = (int)list[1];
		int nHurt = (int)list[2];
		bool flag = num == base.getNetid() && this.skillEffectDict.ContainsKey(key);
		if (flag)
		{
			SkillDamagereboundMe skillDamagereboundMe = (SkillDamagereboundMe)this.skillEffectDict[key];
			skillDamagereboundMe.OnUpdateHurt(nHurt);
		}
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x000BC5CB File Offset: 0x000BA7CB
	public void DeadOrReviveChangeCameraState(object parm)
	{
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x000BC5D0 File Offset: 0x000BA7D0
	public override void OnNetDestroy()
	{
		fGameManager.GetInstance().setPlayer(null);
		this.haveDestroy = true;
		fGameManager.Heroplayer = null;
		fGameManager.m_nUserType = 0;
		fGameManager.IsSingle = false;
		fUISoundManager.instance.StopSound();
		ChatManager.GetInstance().OnInitUserChat();
		base.OnNetDestroy();
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x000BC621 File Offset: 0x000BA821
	public override void CB(int launcherName, int launcherNo, string muzzleName, int bullId, int bullettype, ArrayList born, ArrayList target, ArrayList parm, int numberBullet, int _cartridge, int _loadid)
	{
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000BC624 File Offset: 0x000BA824
	public override void CBP(int weaponName, int launcherNo, string muzzleName, int bullId, int x, int y, int z)
	{
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x000BC627 File Offset: 0x000BA827
	public override void CW(ArrayList data)
	{
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x000BC62A File Offset: 0x000BA82A
	public override void CB_READY(int launcherName, int stat, int time1, int needTime)
	{
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000BC62D File Offset: 0x000BA82D
	public override void DamageMsgToServer(NetLiving player, ArrayList ary)
	{
		this.TosFun("SDG", new object[]
		{
			ary
		});
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000BC648 File Offset: 0x000BA848
	public override void SkillDo(ArrayList parm, ArrayList parm2)
	{
		int num = (int)parm[0];
		this.skillInfo.SkillTalk(num, true);
		this.skillInfo.SkillAnimation(num);
		bool flag = fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(num);
		if (flag)
		{
			ArrayList arrayList = (ArrayList)parm[1];
			Vector3 vector = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			this.SkillAction(num, vector, parm2);
		}
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000BC6F0 File Offset: 0x000BA8F0
	public override void SkillAction(int iSkillIndex, object to, ArrayList parm2)
	{
		bool flag = !this.skillEffectDict.ContainsKey(iSkillIndex);
		if (flag)
		{
			Debug.LogError("找不到这个技能的脚本，无法正常释放。");
		}
		else
		{
			SkillBaseMe skillBaseMe = (SkillBaseMe)this.skillEffectDict[iSkillIndex];
			skillBaseMe.OneSkillAction(to, parm2);
			bool flag2 = skillBaseMe.GetType() == typeof(SkillBaseMe);
			if (flag2)
			{
				skillBaseMe.OneSkillFinish();
			}
		}
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x000BC759 File Offset: 0x000BA959
	public override void ChangeWeaponGroup(int xingtai)
	{
		this.SendMessageToServer(31207, new ArrayList
		{
			xingtai,
			0
		});
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000BC787 File Offset: 0x000BA987
	public override void XingTaiGroup(int xingtai)
	{
		this.SendMessageToServer(31210, new ArrayList
		{
			xingtai,
			0
		});
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x000BC7B5 File Offset: 0x000BA9B5
	public override void XingTaiGroupUpdate(ArrayList data)
	{
		base.XingTaiGroupUpdate(data);
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x000BC7C0 File Offset: 0x000BA9C0
	public override bool superSkill(ArrayList arr = null)
	{
		bool flag = !this.playerDazhao.canDo;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool chatInput = ChatManager.ChatInput;
			if (chatInput)
			{
				result = false;
			}
			else
			{
				bool isDead = this.isDead;
				if (isDead)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.wInput == null || !base.CanHuDong || !this.playerHaveCreate;
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = !this.playerDazhao.weaponReaded(this);
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = Time.time < ((HeroBaseMe)this.heroBase).doSkillTime;
							if (flag4)
							{
								result = false;
							}
							else
							{
								bool flag5 = this.nHuDongState > 0;
								if (flag5)
								{
									bool flag6 = this.nHuDongState >= 200;
									if (flag6)
									{
										this.CheckActiveStat();
										return false;
									}
									this.SetAnimator("HuDongState", 2, 0, 2);
								}
								((HeroBaseMe)this.heroBase).doSkillTime = Time.time + 1f;
								bool flag7 = arr == null;
								if (flag7)
								{
									arr = new ArrayList();
								}
								this.SendMessageToServer(31208, arr);
								result = true;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x000BC904 File Offset: 0x000BAB04
	public override void OnStartSuperSkill(ArrayList arr = null)
	{
		bool flag = arr == null;
		if (flag)
		{
			arr = new ArrayList();
		}
		this.SendMessageToServer(31213, arr);
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000BC930 File Offset: 0x000BAB30
	public override void SuperSkillUpdate(object parm)
	{
		bool flag = this.heroBase != null;
		if (flag)
		{
			this.heroBase.SuperSkillDo(parm);
		}
		this.playerDazhao.SuperSkillDo(this);
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000BC96A File Offset: 0x000BAB6A
	public override void AddCharForce(Vector3 force)
	{
		((TBCharacterThirdMe)this.tChar).SpeedPowerForce(force, force.magnitude);
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x000BC988 File Offset: 0x000BAB88
	public bool Suicide_Handle()
	{
		bool flag = false;
		bool flag2 = this._Death_Time > Time.time;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			this._Death_Time = Time.time + 3f;
			result = true;
		}
		return result;
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000BC9C4 File Offset: 0x000BABC4
	public void DeleteBuffEffect(NetLiving player, int buffid)
	{
		bool flag = this.playerCommon.pos == Vector3.zero && this.playerCommon.posCreate != Vector3.zero;
		if (flag)
		{
			this.playerCommon.pos = this.playerCommon.posCreate;
			this.UserControl.transform.position = this.playerCommon.pos;
		}
		this.SendMessageToServer(41004, new ArrayList
		{
			player.getNetid(),
			buffid
		});
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000BCA6C File Offset: 0x000BAC6C
	public override bool CheckActiveStat()
	{
		bool flag = this.tChar.ActiveSwith == 1 && this.nHuDongState >= 200;
		bool result;
		if (flag)
		{
			this.SetAnimator("ActiveSwith", 2, 2, 2);
			result = true;
		}
		else
		{
			bool flag2 = this.tChar.ActiveSwith == 2 && this.nHuDongState >= 200;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = this.nHuDongState >= 200;
				result = flag3;
			}
		}
		return result;
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000BCB00 File Offset: 0x000BAD00
	public override void OnPveUpDateDeadTime(int nTime)
	{
		base.OnPveUpDateDeadTime(nTime);
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000BCB0C File Offset: 0x000BAD0C
	public void CheckHuDongState()
	{
		bool flag = this.nHuDongState == 1;
		if (flag)
		{
			this.SetAnimator("HuDongState", 2, 0, 2);
		}
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x000BCB40 File Offset: 0x000BAD40
	public void MultiRoundBegin(int round)
	{
		bool flag = round == 1;
		if (flag)
		{
			this.heroBase.RoundBegin();
			this.roundBegin = true;
		}
		else
		{
			this.roundBegin = false;
		}
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x000BCB78 File Offset: 0x000BAD78
	public override void setPlayerNewPos(Vector3 v3Pos)
	{
		bool flag = this.cplace != null;
		if (flag)
		{
			this.cplace.resetCheck(1f);
		}
		base.setPlayerNewPos(v3Pos);
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x000BCBB4 File Offset: 0x000BADB4
	public void Npc_Death(int _Npc_Type)
	{
		bool flag = _Npc_Type == 2;
		if (flag)
		{
			bool flag2 = !((HeroLuoJiMe)this.heroBase).isZiSha;
			if (flag2)
			{
				((HeroLuoJiMe)this.heroBase).OnNpcDestory();
			}
			((HeroLuoJiMe)this.heroBase).isZiSha = false;
		}
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000BCC08 File Offset: 0x000BAE08
	public void OnCreateModelTime_Log(int ntype)
	{
		fGameManager.m_nCreateModelTime = Time.realtimeSinceStartup - fGameManager.nJoinFbTime;
		fGameManager.nJoinFbTime = Time.realtimeSinceStartup;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(Mathf.FloorToInt(fGameManager.m_nLoadScnTime));
		arrayList.Add(Mathf.FloorToInt(fGameManager.m_nLoadCarTime));
		bool flag = ntype != -2;
		if (flag)
		{
			arrayList.Add(Mathf.FloorToInt(fGameManager.m_nCreateModelTime));
		}
		else
		{
			arrayList.Add(ntype);
		}
		this.SendMessageToServer(9004, arrayList);
	}

	// Token: 0x04000F46 RID: 3910
	private object changeCameraStateEndParam;

	// Token: 0x04000F47 RID: 3911
	public static int heroForce;

	// Token: 0x04000F48 RID: 3912
	public static bool isPlayerControl = true;

	// Token: 0x04000F49 RID: 3913
	public static bool isMouseShow = true;

	// Token: 0x04000F4A RID: 3914
	public CWeaponInput wInput;

	// Token: 0x04000F4B RID: 3915
	public Rigidbody checkRig;

	// Token: 0x04000F4C RID: 3916
	public PlayerLocal playerLocal;

	// Token: 0x04000F4D RID: 3917
	public PlayerRuntimeData playerRuntimeData;

	// Token: 0x04000F4E RID: 3918
	public buffMe playerBuffMe;

	// Token: 0x04000F4F RID: 3919
	public DtsMapItem playerDtsItemObj;

	// Token: 0x04000F50 RID: 3920
	public CCheckPlace cplace;

	// Token: 0x04000F51 RID: 3921
	public bool moved;

	// Token: 0x04000F52 RID: 3922
	public List<int> robotList = new List<int>();

	// Token: 0x04000F53 RID: 3923
	public DateTime initCameraTime;

	// Token: 0x04000F54 RID: 3924
	private ArrayList mHurtEffectData = new ArrayList
	{
		"shang",
		"zuoshang",
		"zuo",
		"zuoxia",
		"xia",
		"youxia",
		"you",
		"youshang"
	};

	// Token: 0x04000F55 RID: 3925
	private int nChangePinSiStatSound = 1;

	// Token: 0x04000F56 RID: 3926
	private bool roundBegin;

	// Token: 0x04000F57 RID: 3927
	private bool _Slay_Death_State;

	// Token: 0x04000F58 RID: 3928
	private float _Death_Time;
}
