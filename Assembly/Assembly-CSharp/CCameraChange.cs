using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class CCameraChange : MonoBehaviour
{
	// Token: 0x0600038D RID: 909 RVA: 0x0001BD86 File Offset: 0x00019F86
	private void Start()
	{
		CCameraChange.Instance = this;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0001BD8F File Offset: 0x00019F8F
	public void InitCameraChange(HeroPlayer hp)
	{
		this.hero = hp;
		this.deadNoCheck = -1;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0001BDA0 File Offset: 0x00019FA0
	public void setLuXiangState()
	{
		CameraControl.iCameraCtrl = CameraCtrlState.CameraLuxiang;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0001BDB0 File Offset: 0x00019FB0
	private void switchFilm()
	{
		bool flag = !this.bSwitchFilm && this.filmTarget != null;
		if (flag)
		{
			this.bSwitchFilm = true;
			CFilmManager.Instance.PlayFilms(-1, this.filmTarget.getNetid(), this.filmTarget.playerCommon.m_nShiliServer, this.filmTargetParent, this.filmParentNpcOrPlayerID);
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001BE14 File Offset: 0x0001A014
	public void setFilmPlayer(NetLiving net)
	{
		this.filmTarget = net;
		this.filmTargetParent = net.getNetid();
		this.filmParentNpcOrPlayerID = this.filmTargetParent;
		bool flag = net.netobject_type != 2;
		if (!flag)
		{
			NetLiving netLiving = Fun.Get_NetLiving(((Npc_Entity)net).Master_ID);
			bool flag2 = netLiving != null;
			if (flag2)
			{
				this.filmTargetParent = netLiving.getNetid();
				NetLiving camera_Switch = ((Npc_Entity)net).Get_Camera_Switch();
				bool flag3 = camera_Switch != null;
				if (flag3)
				{
					this.filmParentNpcOrPlayerID = camera_Switch.getNetid();
				}
			}
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
	public void playFilmByPlayer(NetLiving net)
	{
		bool flag = net.netobject_type == 2;
		if (flag)
		{
			net = ((Npc_Entity)net).Get_Camera_Switch();
		}
		bool bePlayer = net.bePlayer;
		if (bePlayer)
		{
			bool flag2 = net.FpsOrTps == -1;
			if (flag2)
			{
				net.FpsOrTps = 1;
			}
			CameraControl.Instance.InitCameraHero(net);
			bool flag3 = net.FpsOrTps == 1;
			if (flag3)
			{
				net.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
			else
			{
				net.changeCameraState(CameraState.ThirdState, null, string.Empty);
			}
			CameraControl.iCameraCtrl = CameraCtrlState.CameraLuxiang;
			fUIManager.GetInstance().openGameUi(net, 0);
		}
		else
		{
			bool flag4 = net.FpsOrTps == -1;
			if (flag4)
			{
				net.FpsOrTps = 2;
			}
			CameraControl.Instance.InitCameraHero(net);
			CameraControl.iCameraState = CameraState.ThirdState;
			CameraControl.iCameraCtrl = CameraCtrlState.CameraLuxiang;
			fUIManager.GetInstance().openGameUi(net, 0);
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001BF94 File Offset: 0x0001A194
	public void overFilmByPlayer(bool interrupt = false, int type = 0)
	{
		this.filmTarget = null;
		this.filmTargetParent = -1;
		this.filmParentNpcOrPlayerID = -1;
		this.bSwitchFilm = false;
		bool flag = type == 0;
		if (flag)
		{
			bool flag2 = MatchManager.GetInstance().m_pFbData.nType == 10;
			if (flag2)
			{
				fGameManager.Heroplayer.FpsOrTps = 2;
				fGameManager.Heroplayer.ChangeFpsTpsState();
				CameraControl.iCameraState = CameraState.DeadState;
				CameraControl.iCameraCtrl = CameraCtrlState.CameraWatchDeadBody;
				this.changeCameraTarget();
			}
			else
			{
				bool flag3 = MatchManager.GetInstance().m_pFbData.nType == 4;
				if (flag3)
				{
					fGameManager.Heroplayer.FpsOrTps = 2;
					fGameManager.Heroplayer.ChangeFpsTpsState();
					CameraControl.iCameraState = CameraState.DeadState;
					CameraControl.iCameraCtrl = CameraCtrlState.CameraGuanZhan;
					this.changeCameraTarget();
				}
				else
				{
					bool flag4 = !interrupt && fUIManager.mInst.GetNowGameState() > 0;
					if (flag4)
					{
						fGameManager.Heroplayer.FpsOrTps = 1;
						fGameManager.Heroplayer.ChangeFpsTpsState();
						CameraControl.Instance.InitCameraHero(fGameManager.Heroplayer);
						CameraControl.iCameraState = CameraState.FirstState;
						CameraControl.iCameraCtrl = CameraCtrlState.CameraPlayerCtrl;
					}
					else
					{
						fGameManager.Heroplayer.FpsOrTps = 2;
						fGameManager.Heroplayer.ChangeFpsTpsState();
						CameraControl.iCameraState = CameraState.DeadState;
						CameraControl.iCameraCtrl = CameraCtrlState.CameraWatchAttacker;
					}
				}
			}
		}
		CameraControl.Instance.CameraZoom = false;
		CameraControl.Instance.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
		fUIManager.GetInstance().openGameUi(fGameManager.Heroplayer, 0);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0001C120 File Offset: 0x0001A320
	private void Update()
	{
		bool flag = this.hero == null;
		if (!flag)
		{
			bool isDead = this.hero.isDead;
			if (isDead)
			{
				this.deadWatchSelfTimer -= Time.deltaTime;
				bool flag2 = !this.havePressedJ && this.deadWatchSelfTimer < 3.5f && MatchManager.GetInstance().m_pFbData.nType == 11 && Input_Manager.Get_Axis("Jump") > 0f && !this.hero.heroBase.Release_Musth_State;
				if (flag2)
				{
					this.havePressedJ = true;
					this.sendTime = 0f;
				}
				bool flag3 = this.havePressedJ && this.deadWatchSelfTimer < 1.5f && Time.time > this.sendTime;
				if (flag3)
				{
					this.sendTime = Time.time + 10f;
					this.hero.SendMessageToServer(19702, new ArrayList
					{
						this.hero.getNetid()
					});
				}
				bool flag4 = !this.havePressedJ && this.openFilm && this.filmTargetParent != -1 && this.hero.playerCommon.m_nDeathAttackNetType != 6 && this.hero.deadNo != this.deadNoCheck && this.deadWatchSelfTimer < 0f && MatchManager.GetInstance().m_pFbData.isRobotFb == 0;
				if (flag4)
				{
					this.deadNoCheck = this.hero.deadNo;
					this.switchFilm();
				}
				else
				{
					bool flag5 = Input_Manager.Get_Axis("Jump") > 0f && CFilmManager.Instance.playing && MatchManager.GetInstance().m_pFbData.nType != 11;
					if (flag5)
					{
						CFilmManager.Instance.breakFilm = true;
					}
					this.ChangeCameraCtrlState();
				}
			}
			bool flag6 = User_Data_Handle.Watch_Fight_State && HeroPlayer.isPlayerControl && CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
			if (flag6)
			{
				bool flag7 = Input.GetMouseButtonUp(0) && User_Data_Handle.Get_User_Data_Array(PlayerDataManager.getInstance().playerCommon.m_nShili) != null && User_Data_Handle.Get_User_Data_Array(PlayerDataManager.getInstance().playerCommon.m_nShili).Count >= 1;
				if (flag7)
				{
					User_Data_Handle.Watch_State = 1;
					CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(PlayerDataManager.getInstance().playerCommon.m_nShili);
				}
				bool flag8 = Input.GetMouseButtonUp(1) && User_Data_Handle.Get_User_Data_Array(PlayerDataManager.getInstance().playerCommon.m_nShili) != null && User_Data_Handle.Get_User_Data_Array(PlayerDataManager.getInstance().playerCommon.m_nShili).Count >= 1;
				if (flag8)
				{
					User_Data_Handle.Watch_State = 2;
					CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(PlayerDataManager.getInstance().playerCommon.m_nShili);
				}
			}
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001C418 File Offset: 0x0001A618
	private void ChangeCameraCtrlState()
	{
		bool flag = !this.hero.isDead;
		if (!flag)
		{
			bool flag2 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadBody;
			if (flag2)
			{
				bool flag3 = this.deadWatchSelfTimer <= 3f;
				if (flag3)
				{
					CameraControl.Instance.nowTarget = CameraControl.Instance.AttackTarget;
					CameraControl.iCameraCtrl = CameraCtrlState.CameraWatchAttacker;
				}
			}
			else
			{
				bool flag4 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchAttacker;
				if (flag4)
				{
					bool flag5 = this.deadWatchSelfTimer <= 0f;
					if (flag5)
					{
						bool flag6 = User_Data_Handle.Get_User_Data_Array(this.hero.playerCommon.m_nShili) == null || User_Data_Handle.Get_User_Data_Array(this.hero.playerCommon.m_nShili).Count < 1;
						if (flag6)
						{
							CameraControl.iCameraCtrl = CameraCtrlState.CameraWatchDeadSelf;
							CameraControl.Instance.AttackTarget = null;
							CameraControl.Instance.nowTarget = this.hero.camTarget;
						}
						else
						{
							CameraControl.iCameraCtrl = CameraCtrlState.CameraGuanZhan;
							CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
							User_Data_Handle.CanWatchTeamates = true;
						}
					}
				}
				else
				{
					bool flag7 = CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
					if (flag7)
					{
						bool flag8 = CameraControl.Instance.NowTargetNetObj != null && CameraControl.Instance.NowTargetNetObj.isDead;
						if (flag8)
						{
							CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
						}
						bool flag9 = CameraControl.Instance.NowTargetNetObj != null && CameraControl.Instance.NowTargetNetObj.thisModel == null;
						if (flag9)
						{
							CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
						}
						bool flag10 = CameraControl.Instance.NowTargetNetObj == null;
						if (flag10)
						{
							CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
						}
					}
					else
					{
						bool flag11 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadSelf && User_Data_Handle.Get_User_Data_Array(this.hero.playerCommon.m_nShili) != null && User_Data_Handle.Get_User_Data_Array(this.hero.playerCommon.m_nShili).Count > 1;
						if (flag11)
						{
							CameraControl.iCameraCtrl = CameraCtrlState.CameraGuanZhan;
							CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
	public void SetWatchTematesTimer(bool hasAttacker, NetLiving a)
	{
		CameraControl.iCameraCtrl = CameraCtrlState.CameraWatchDeadBody;
		if (hasAttacker)
		{
			this.deadWatchSelfTimer = 4.5f;
		}
		else
		{
			CameraControl.Instance.nowTarget = CameraControl.Instance.camerHero.camTarget;
			CameraControl.Instance.AttackTarget = null;
			this.deadWatchSelfTimer = 4.5f;
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0001C724 File Offset: 0x0001A924
	public void changeCameraTarget()
	{
		bool flag = CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
		if (flag)
		{
			CameraControl.Instance.NowTargetNetObj = (NetLiving)User_Data_Handle.Watch_Fight(this.hero.playerCommon.m_nShili);
			User_Data_Handle.CanWatchTeamates = true;
		}
	}

	// Token: 0x040002D8 RID: 728
	public bool openFilm;

	// Token: 0x040002D9 RID: 729
	public float deadWatchSelfTimer;

	// Token: 0x040002DA RID: 730
	private int deadNoCheck = -1;

	// Token: 0x040002DB RID: 731
	private HeroPlayer hero;

	// Token: 0x040002DC RID: 732
	public static CCameraChange Instance;

	// Token: 0x040002DD RID: 733
	private bool bSwitchFilm;

	// Token: 0x040002DE RID: 734
	public NetLiving filmTarget;

	// Token: 0x040002DF RID: 735
	public int filmTargetParent = -1;

	// Token: 0x040002E0 RID: 736
	public int filmParentNpcOrPlayerID = -1;

	// Token: 0x040002E1 RID: 737
	public bool havePressedJ;

	// Token: 0x040002E2 RID: 738
	private float sendTime;
}
