using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class CameraControl : MonoBehaviour
{
	// Token: 0x17000076 RID: 118
	// (get) Token: 0x0600024B RID: 587 RVA: 0x0001062A File Offset: 0x0000E82A
	public NetLiving camerHero
	{
		get
		{
			return this.nowHero;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600024C RID: 588 RVA: 0x00010634 File Offset: 0x0000E834
	private float tpsCameraDis
	{
		get
		{
			bool flag = this.nowHero == null || this.nowHero.heroData == null || CameraControl.iCameraState == CameraState.DeadState;
			float result;
			if (flag)
			{
				result = 4f;
			}
			else
			{
				result = this.nowHero.heroData.fTPSCameraDis;
			}
			return result;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600024D RID: 589 RVA: 0x00010688 File Offset: 0x0000E888
	// (set) Token: 0x0600024E RID: 590 RVA: 0x000106A0 File Offset: 0x0000E8A0
	public Transform nowTarget
	{
		get
		{
			return this._nowTarget;
		}
		set
		{
			this._nowTarget = value;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600024F RID: 591 RVA: 0x000106AC File Offset: 0x0000E8AC
	public int nowCameraMask
	{
		get
		{
			bool flag = this._nowCameraMask == 0;
			if (flag)
			{
				this._nowCameraMask = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2"));
			}
			return this._nowCameraMask;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000250 RID: 592 RVA: 0x00010708 File Offset: 0x0000E908
	public Vector3 fpsCameraTargetPostion
	{
		get
		{
			bool flag = this.camerHero == null;
			Vector3 zero;
			if (flag)
			{
				zero = Vector3.zero;
			}
			else
			{
				zero = this.fpsHeight;
			}
			return zero;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000251 RID: 593 RVA: 0x00010738 File Offset: 0x0000E938
	public Vector3 tpsCameraTargetPostion
	{
		get
		{
			bool flag = this.camerHero == null;
			Vector3 zero;
			if (flag)
			{
				zero = Vector3.zero;
			}
			else
			{
				zero = this.tpsHeight;
			}
			return zero;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000252 RID: 594 RVA: 0x00010768 File Offset: 0x0000E968
	// (set) Token: 0x06000253 RID: 595 RVA: 0x0001077F File Offset: 0x0000E97F
	public static CameraControl Instance
	{
		get
		{
			return CameraControl._instance;
		}
		set
		{
			CameraControl._instance = value;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000254 RID: 596 RVA: 0x00010788 File Offset: 0x0000E988
	// (set) Token: 0x06000255 RID: 597 RVA: 0x000107A0 File Offset: 0x0000E9A0
	public Vector2 CameraMove
	{
		get
		{
			return this._CameraMove;
		}
		set
		{
			this._CameraMove = value;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000256 RID: 598 RVA: 0x000107AC File Offset: 0x0000E9AC
	// (set) Token: 0x06000257 RID: 599 RVA: 0x000107C4 File Offset: 0x0000E9C4
	public Vector2 CameraZoomMove
	{
		get
		{
			return this._CameraZoomMove;
		}
		set
		{
			this._CameraZoomMove = value;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000258 RID: 600 RVA: 0x000107D0 File Offset: 0x0000E9D0
	// (set) Token: 0x06000259 RID: 601 RVA: 0x000107E8 File Offset: 0x0000E9E8
	public bool CameraZoom
	{
		get
		{
			return this._CameraZoom;
		}
		set
		{
			this._CameraZoom = value;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600025A RID: 602 RVA: 0x000107F4 File Offset: 0x0000E9F4
	// (set) Token: 0x0600025B RID: 603 RVA: 0x0001080C File Offset: 0x0000EA0C
	public bool DashMouseMoveSpeed
	{
		get
		{
			return this._DashMouseMoveSpeed;
		}
		set
		{
			this._DashMouseMoveSpeed = value;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600025C RID: 604 RVA: 0x00010818 File Offset: 0x0000EA18
	// (set) Token: 0x0600025D RID: 605 RVA: 0x00010859 File Offset: 0x0000EA59
	public Setting_Entity_Control Setting_Entity_Control
	{
		get
		{
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			PlayerCommon playerCommon = player.playerCommon;
			this._Setting_Entity_Control = this.HeroControl.Get_Hero_Control_Data(playerCommon.m_nModelIndex);
			return this._Setting_Entity_Control;
		}
		set
		{
			this._Setting_Entity_Control = value;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600025E RID: 606 RVA: 0x00010863 File Offset: 0x0000EA63
	public Setting_Entity_Hero_Control HeroControl
	{
		get
		{
			return GameRoot.inst.Setting.Hero_Control_Class;
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00010874 File Offset: 0x0000EA74
	private void Start()
	{
		CameraControl.Instance = this;
		this.CameraZoom = false;
		this.cameraSpeed = Mathf.Lerp(CameraControl.SmoothMax, CameraControl.SmoothMin, (float)GameRoot.inst.Setting.Video_Class.Smooth * 0.1f);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x000108C4 File Offset: 0x0000EAC4
	public void initCameraAngle(Transform __target, float fy)
	{
		this.nowTarget = __target;
		this.x = fy;
		this.y = 0f;
		this.cx = this.x;
		this.cy = this.y;
		Quaternion quaternion = Quaternion.Euler(this.cy, this.cx, 0f);
		Transform nowTarget = this.nowTarget;
		Quaternion rotation = this.lastTransformRotation = (this.lastTargetRotation = quaternion);
		base.transform.rotation = rotation;
		nowTarget.rotation = rotation;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0001094C File Offset: 0x0000EB4C
	public void InitCameraHero(NetLiving net)
	{
		this.nowHero = net;
		this.dontMove = false;
		this.nowTarget = net.camTarget;
		bool flag = net.userModelFps != null;
		if (flag)
		{
			this.cameraAnimator = Fun.FindGameObjectChild(net.userModelFps, "cam_ctrl001", false);
		}
		bool bePlayer = net.bePlayer;
		if (bePlayer)
		{
			this.tpsHeight = net.heroData.TPSCameraPos[1];
			this.fpsHeight = this.camerHero.heroData.FPSCameraPos[1];
		}
		else
		{
			bool flag2 = net.netobject_type == 2;
			if (flag2)
			{
				this.tpsHeight = new Vector3(0f, ((Npc_Entity)net).Camera_Distance, 0f);
				this.fpsHeight = this.tpsHeight;
			}
			else
			{
				this.tpsHeight = new Vector3(0f, 2f, 0f);
				this.fpsHeight = this.tpsHeight;
			}
		}
		bool flag3 = !net.isDead;
		if (flag3)
		{
			CameraControl.iCameraCtrl = CameraCtrlState.CameraPlayerCtrl;
		}
		this.nowHero.HuDongChangeCameraIng = false;
		this.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
		this.CameraZoom = false;
		bool flag4 = this.TransObjMid == null;
		if (flag4)
		{
			this.TransObjMid = new GameObject();
			this.TransObjMid.transform.SetParent(net._playerSoundContainer.transform, true);
		}
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00010ABE File Offset: 0x0000ECBE
	public void GameCameraInit(HeroPlayer h)
	{
		CameraControl.Instance = this;
		this.nowHero = h;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00010AD0 File Offset: 0x0000ECD0
	private void Update()
	{
		bool flag = !(this.nowTarget == null);
		if (flag)
		{
			this.UpdateIt();
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00010AFC File Offset: 0x0000ECFC
	private void UpdateIt()
	{
		float deltaTime = Time.deltaTime;
		bool flag = CameraControl.iCameraState == CameraState.FirstState;
		if (flag)
		{
			this.cameraAnimatorPos = Vector3.zero;
			this.cameraAnimatorRot = Quaternion.identity;
			this.cameraAnimatorSpeed = 0f;
			this.UpPostion = 0f;
			this.camHeightDis = 0f;
			bool flag2 = this.cameraAnimator != null;
			if (flag2)
			{
				this.cameraAnimatorPos = this.cameraAnimator.transform.localPosition;
				this.cameraAnimatorRot = this.cameraAnimator.transform.localRotation;
				this.cameraAnimatorSpeed = ((this.cameraAnimatorPos.magnitude <= 0.001f) ? 0f : 1f);
				this.cameraPaQiangPos = new Vector3(0f, this.cameraFDisFactor, 0f);
			}
			this.fDis = Mathf.Lerp(this.fDis, 0f, deltaTime * this.cameraChangeSpeed);
			this.nowTarget.localPosition = Vector3.Lerp(this.nowTarget.localPosition, this.fpsCameraTargetPostion + this.cameraAnimatorPos + this.cameraPaQiangPos, deltaTime * this.cameraChangeSpeed + this.cameraAnimatorSpeed);
		}
		else
		{
			bool flag3 = CameraControl.iCameraState == CameraState.ThirdState;
			if (flag3)
			{
				this.cameraMaxDis = 0.9f;
				this.cameraMinDis = 0.5f;
				this.camHeightDis = 0f;
				bool flag4 = this.tpsCameraDis > this.fDisAllow;
				if (flag4)
				{
					this.fDis = this.fDisAllow;
				}
				else
				{
					this.fDis = Mathf.Lerp(this.fDis, this.tpsCameraDis, deltaTime * this.cameraChangeSpeed);
				}
				this.nowTarget.localPosition = Vector3.Lerp(this.nowTarget.localPosition, this.tpsCameraTargetPostion, deltaTime * this.cameraChangeSpeed);
			}
			else
			{
				this.cameraMaxDis = 0.5f;
				this.cameraMinDis = 0.2f;
				bool flag5 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadBody || CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadSelf;
				if (flag5)
				{
					Vector3 b = new Vector3(0f, 1f, 0f);
					bool flag6 = this.tpsCameraDis > this.fDisAllow;
					if (flag6)
					{
						this.fDis = this.fDisAllow;
					}
					else
					{
						this.fDis = Mathf.Lerp(this.fDis, this.tpsCameraDis, deltaTime * this.cameraChangeSpeed);
					}
					this.nowTarget.localPosition = Vector3.Lerp(this.nowTarget.localPosition, b, deltaTime * this.cameraChangeSpeed);
				}
				else
				{
					bool flag7 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchAttacker;
					if (flag7)
					{
						Vector3 b2 = new Vector3(0f, 1.1f, 0f);
						bool flag8 = this.tpsCameraDis > this.fDisAllow;
						if (flag8)
						{
							this.fDis = this.fDisAllow;
						}
						else
						{
							this.fDis = Mathf.Lerp(this.fDis, this.tpsCameraDis, deltaTime * this.cameraChangeSpeed);
						}
						this.nowTarget.localPosition = Vector3.Lerp(this.nowTarget.localPosition, b2, deltaTime * this.cameraChangeSpeed);
					}
					else
					{
						bool flag9 = CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
						if (flag9)
						{
							bool flag10 = this.tpsCameraDis > this.fDisAllow;
							if (flag10)
							{
								this.fDis = this.fDisAllow;
							}
							else
							{
								this.fDis = Mathf.Lerp(this.fDis, this.tpsCameraDis, deltaTime * this.cameraChangeSpeed);
							}
						}
						else
						{
							this.fDis = Mathf.Lerp(this.fDis, 4f, deltaTime * this.cameraChangeSpeed);
						}
					}
				}
			}
		}
		this.checkHouzuoli();
		this.CameraMoveFunc();
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00010ED4 File Offset: 0x0000F0D4
	public void ChangeCameraState(int iState)
	{
		CameraControl.iCameraState = iState;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	private void MouseMove()
	{
		bool flag = this.dontMove;
		if (!flag)
		{
			float num = 0f;
			float num2 = 0f;
			bool flag2 = HeroPlayer.isPlayerControl && !ChatManager.ChatInput;
			if (flag2)
			{
				bool flag3 = !this.CameraZoom;
				if (flag3)
				{
					num = Input_Manager.Get_Axis("Mouse X") * 1f * this.CameraMove.x;
					num2 = Input_Manager.Get_Axis("Mouse Y") * 1f * this.CameraMove.y;
				}
				else
				{
					num = Input_Manager.Get_Axis("Mouse X") * 1f * this._CameraZoomMove.x;
					num2 = Input_Manager.Get_Axis("Mouse Y") * 1f * this._CameraZoomMove.y;
				}
				bool dashMouseMoveSpeed = this.DashMouseMoveSpeed;
				if (dashMouseMoveSpeed)
				{
					num = Input_Manager.Get_Axis("Mouse X") * 0.2f;
					num2 = Input_Manager.Get_Axis("Mouse Y") * 0.2f;
				}
			}
			float num3 = num;
			float num4 = num2;
			bool flag4 = this.isFiring;
			if (flag4)
			{
				this.fireingBackAngle = num4;
			}
			else
			{
				this.fireingBackAngle = 0f;
			}
			bool flag5 = num3 == 0f;
			if (flag5)
			{
				this.MoveDir = 0;
			}
			bool flag6 = num3 != 0f || this.xhouzuo != 0f;
			if (flag6)
			{
				bool dashMouseMoveSpeed2 = this.DashMouseMoveSpeed;
				if (dashMouseMoveSpeed2)
				{
					num3 = this.ClampAngle(num3, -3f, 3f);
				}
				this.x = this.x + num3 + this.xhouzuo;
				bool flag7 = num3 > 0f;
				if (flag7)
				{
					this.MoveDir = 1;
				}
				bool flag8 = num3 < 0f;
				if (flag8)
				{
					this.MoveDir = -1;
				}
				bool rotateCamera = this.RotateCamera;
				if (rotateCamera)
				{
					this.x = Mathf.Clamp(this.x, (float)this.xMinLimit, (float)this.xMaxLimit);
				}
			}
			bool flag9 = num4 != 0f || this.yhouzuo != 0f;
			if (flag9)
			{
				this.y = this.y - num4 + this.yhouzuo;
				this.y = this.ClampAngle(this.y, (float)this.yMinLimit, (float)this.yMaxLimit);
			}
			this.cx = this.x;
			this.cy = this.y;
			bool flag10 = this.nowHero != null && this.nowHero == fGameManager.Heroplayer;
			if (flag10)
			{
				fGameManager.Heroplayer.playerLocal.nowEuler = Quaternion.Euler(this.cy, this.cx, 0f);
			}
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0001118C File Offset: 0x0000F38C
	public void CameraMoveFunc()
	{
		bool flag = this.camerHero == null || this.nowTarget == null;
		if (!flag)
		{
			this.MouseMove();
			this.CameraCheckCollider();
			bool flag2 = CameraControl.iCameraCtrl == CameraCtrlState.CameraPlayerCtrl;
			if (flag2)
			{
				this.setPlayerTarget();
				this.nowTargetRot = Quaternion.Euler(this.cy, this.cx, 0f) * this.cameraAnimatorRot;
				this.nowTransformRot = this.nowTargetRot;
			}
			else
			{
				bool flag3 = CameraControl.iCameraCtrl == CameraCtrlState.CameraLuxiang;
				if (flag3)
				{
					Vector3 normalized = (this.nowHero.playerTarget.transform.position - this.nowTarget.position).normalized;
					this.nowTargetRot = Quaternion.LookRotation(normalized);
					this.nowTransformRot = this.nowTargetRot;
				}
				else
				{
					bool flag4 = CameraControl.iCameraCtrl == CameraCtrlState.CameraMoveCtrl;
					if (flag4)
					{
						this.nowTargetRot = Quaternion.Euler(this.cy, this.cx, 0f);
						this.nowTransformRot = this.nowTargetRot;
					}
					else
					{
						bool flag5 = CameraControl.iCameraCtrl == CameraCtrlState.CameraPaQiang && CameraControl.iCameraState == CameraState.FirstState;
						if (flag5)
						{
							this.nowTargetRot = Quaternion.LookRotation(this.nowHero.tChar.wallNormalStart, Vector3.up);
							this.nowTransformRot = Quaternion.Euler(this.cy, this.cx, 0f) * this.cameraAnimatorRot;
						}
						else
						{
							bool flag6 = CameraControl.iCameraCtrl == CameraCtrlState.CameraPaQiang && CameraControl.iCameraState == CameraState.ThirdState;
							if (flag6)
							{
								this.nowTargetRot = Quaternion.LookRotation(this.camerHero.tChar.wallNormalStart, Vector3.up);
								this.nowTransformRot = Quaternion.Euler(this.cy, this.cx, 0f);
							}
							else
							{
								bool flag7 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadBody;
								if (flag7)
								{
									Vector3 vector = Vector3.zero;
									bool flag8 = this.AttackTarget != null;
									if (flag8)
									{
										vector = (this.AttackTarget.transform.position - this.nowTarget.position).normalized;
										vector.y = 0f;
										bool flag9 = vector.x == 0f && vector.z == 0f;
										if (flag9)
										{
											vector.y = 0.1f;
										}
										this.nowTargetRot = Quaternion.LookRotation(vector);
										this.nowTransformRot = this.nowTargetRot;
									}
									else
									{
										vector = (this.nowTarget.position - (this.nowTarget.position - this.nowTarget.forward * this.fDis)).normalized;
										vector.x = 0f;
										vector.z = 0f;
										bool flag10 = vector.y == 0f;
										if (flag10)
										{
											vector.y = 0.1f;
										}
										this.nowTargetRot = Quaternion.LookRotation(vector);
										this.nowTransformRot = this.nowTargetRot;
									}
								}
								else
								{
									bool flag11 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchAttacker;
									if (flag11)
									{
										Vector3 vector2 = Vector3.zero;
										bool flag12 = this.AttackTarget != null;
										if (flag12)
										{
											vector2 = (this.nowTarget.position - base.transform.position).normalized;
											bool flag13 = vector2 == Vector3.zero;
											if (flag13)
											{
												vector2 = (this.nowTarget.position - (base.transform.position + Vector3.up * 0.1f)).normalized;
											}
											this.nowTargetRot = Quaternion.LookRotation(vector2);
											this.nowTransformRot = this.nowTargetRot;
										}
										else
										{
											vector2 = (this.nowTarget.position - base.transform.position).normalized;
											vector2.x = 0f;
											vector2.z = 0f;
											bool flag14 = vector2.y == 0f;
											if (flag14)
											{
												vector2.y = 0.1f;
											}
											this.nowTargetRot = Quaternion.LookRotation(vector2.normalized);
											this.nowTransformRot = this.nowTargetRot;
										}
									}
									else
									{
										bool flag15 = CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
										if (flag15)
										{
											Vector3 vector3 = Vector3.zero;
											bool flag16 = this.NowTargetNetObj != null;
											if (flag16)
											{
												vector3 = (this.NowTargetNetObj.playerTarget.transform.position - this.nowTarget.position).normalized;
												Vector3 axis = new Vector3(vector3.x, 0f, vector3.z);
												Vector3 axis2 = Quaternion.AngleAxis(-90f, axis) * Vector3.up;
												bool flag17 = vector3.y > 0.7f;
												if (flag17)
												{
													vector3 = Quaternion.AngleAxis(50f - Vector3.Angle(vector3, Vector3.up), axis2) * vector3;
												}
											}
											bool flag18 = vector3 == Vector3.zero;
											if (flag18)
											{
												vector3 = (this.nowTarget.position - (base.transform.position + Vector3.up * 0.1f)).normalized;
											}
											this.nowTargetRot = Quaternion.LookRotation(vector3);
											this.nowTransformRot = this.nowTargetRot;
										}
										else
										{
											bool flag19 = CameraControl.iCameraCtrl == CameraCtrlState.CameraWatchDeadSelf;
											if (flag19)
											{
												Vector3 forward = base.transform.forward;
												this.nowTargetRot = Quaternion.LookRotation(forward);
												this.nowTransformRot = this.nowTargetRot;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0001179C File Offset: 0x0000F99C
	public void CameraRoration(bool fake, int angle, Vector3 dir, int MouseMove)
	{
		this.RotateCamera = fake;
		bool rotateCamera = this.RotateCamera;
		if (rotateCamera)
		{
			float num = Vector3.Angle(dir, base.transform.forward);
			bool flag = MouseMove == -1;
			if (flag)
			{
				this.xMinLimit = (int)this.x - angle + (int)num;
				this.xMaxLimit = (int)this.x + angle + (int)num;
			}
			bool flag2 = MouseMove == 1;
			if (flag2)
			{
				this.xMinLimit = (int)this.x - angle - (int)num;
				this.xMaxLimit = (int)this.x + angle - (int)num;
			}
			bool flag3 = MouseMove == 0;
			if (flag3)
			{
				this.xMinLimit = (int)this.x - angle;
				this.xMaxLimit = (int)this.x + angle;
			}
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0001185C File Offset: 0x0000FA5C
	private void LateUpdate()
	{
		bool flag = !(this.nowTarget == null);
		if (flag)
		{
			float t = 1f - Mathf.Exp((0f - this.cameraSpeed) * Time.deltaTime);
			this.nowTarget.rotation = Quaternion.Lerp(this.lastTargetRotation, this.nowTargetRot, t);
			this.lastTargetRotation = this.nowTarget.rotation;
			base.transform.rotation = Quaternion.Lerp(this.lastTransformRotation, this.nowTransformRot, t);
			this.lastTransformRotation = base.transform.rotation;
			base.transform.position = this.nowTarget.position - this.nowTarget.forward * this.fDis + Vector3.up * this.camHeightDis;
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00011948 File Offset: 0x0000FB48
	public void setPlayerTarget()
	{
		bool flag = this.nowHero == null || this.nowHero.haveDestroy;
		if (!flag)
		{
			float num = 1000000f;
			float num2 = 300f;
			Transform transform = this.nowHero.FindMubiaoTarget.transform;
			Transform transform2 = this.nowHero.playerTarget.transform;
			this.nowHero.mubiaoUp = transform.up;
			transform2.position = this.nowTarget.position + this.nowTarget.forward * num2;
			this.cursorRay.origin = transform.position + transform.forward * 0.1f;
			this.cursorRay.direction = transform.forward;
			for (int i = 0; i < 3; i++)
			{
				// Each layer mask has its own nearest hit; another team's hit must not cap it.
				num = 1000000f;
				int num3 = TriggerunHitCollision.AimRaycast(this.cursorRay, this.rays, num2, LayerManager.getMiaoZhunLayer(i));
				int num4 = -1;
				bool flag2 = num3 > 0;
				if (flag2)
				{
					for (int j = 0; j < num3; j++)
					{
						bool flag3 = !(this.rays[j].collider == null) && this.rays[j].distance <= num && !this.rays[j].collider.tag.Contains("can_damage_dun");
						if (flag3)
						{
							num4 = j;
							num = this.rays[j].distance;
						}
					}
				}
				bool flag4 = num4 != -1;
				if (flag4)
				{
					this.nowHero.mubiaoPointHit[i] = this.rays[num4];
					this.nowHero.hitGamaObj[i] = this.rays[num4].transform.gameObject;
					this.nowHero.mubiaoPoint[i] = this.rays[num4].point;
				}
				else
				{
					this.nowHero.mubiaoPoint[i] = transform2.position;
					this.nowHero.hitGamaObj[i] = null;
				}
			}
		}
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00011BB8 File Offset: 0x0000FDB8
	private void CameraCheckCollider()
	{
		bool flag = CameraControl.iCameraState == CameraState.FirstState;
		if (!flag)
		{
			Vector3 direction = base.transform.position - this.nowTarget.position;
			bool flag2 = CameraControl.iCameraState == CameraState.ThirdState;
			Ray ray;
			if (flag2)
			{
				ray = new Ray(this.nowTarget.position, direction);
				this.cameraMaxDis += this.cameraMaxDis * ray.direction.y;
				this.cameraMinDis += this.cameraMinDis * ray.direction.y;
			}
			else
			{
				ray = new Ray(this.nowTarget.position, -base.transform.forward);
			}
			this.UpPostion = (0f - base.transform.forward.y) * 0.6f;
			bool flag3 = this.UpPostion < -1f;
			if (flag3)
			{
				this.UpPostion = -1f;
			}
			bool flag4 = this.UpPostion > 0f;
			if (flag4)
			{
				this.UpPostion = 0f;
			}
			RaycastHit raycastHit;
			bool flag5 = Physics.Raycast(ray, out raycastHit, this.tpsCameraDis, this.nowCameraMask);
			if (flag5)
			{
				this.fDis = Mathf.Lerp(this.fDis, Vector3.Distance(this.nowTarget.position, raycastHit.point) - this.cameraMaxDis, Time.deltaTime * this.cameraChangeSpeed);
				this.fDisAllow = this.fDis;
				bool flag6 = this.fDis < this.cameraMinDis;
				if (flag6)
				{
					this.fDis = this.cameraMinDis;
					this.fDisAllow = this.fDis;
				}
				bool flag7 = CameraControl.iCameraCtrl == CameraCtrlState.CameraGuanZhan;
				if (flag7)
				{
					bool flag8 = this.fDis < 0.8f;
					if (flag8)
					{
						this.camHeightDis = 1.3f;
					}
					else
					{
						this.camHeightDis = 0f;
					}
				}
			}
			else
			{
				this.fDisAllow = this.tpsCameraDis;
			}
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00011DCE File Offset: 0x0000FFCE
	public void SetPaQiangCaMera()
	{
		this.yMaxLimit = 10;
		this.cameraFDisFactor = -1.2f;
		CameraControl.iCameraCtrl = CameraCtrlState.CameraPaQiang;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00011DEE File Offset: 0x0000FFEE
	public void StopPaQiangCaMera()
	{
		this.cameraFDisFactor = 0f;
		this.yMaxLimit = 85;
		CameraControl.iCameraCtrl = CameraCtrlState.CameraPlayerCtrl;
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00011E10 File Offset: 0x00010010
	public void SystemCtrlCamera(float fx, float fy)
	{
		this.x = fy;
		this.y = fx;
		bool flag = this.y > 180f;
		if (flag)
		{
			this.y -= 360f;
		}
		else
		{
			bool flag2 = this.y < -180f;
			if (flag2)
			{
				this.y += 360f;
			}
		}
		this.y = this.ClampAngle(this.y, (float)this.yMinLimit, (float)this.yMaxLimit);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00011E98 File Offset: 0x00010098
	private float ClampAngle(float angle, float min, float max)
	{
		bool flag = angle < -360f;
		if (flag)
		{
			angle += 360f;
		}
		bool flag2 = angle > 360f;
		if (flag2)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00011EE0 File Offset: 0x000100E0
	public void ChangeMouseSensitivity()
	{
		float num = this.Setting_Entity_Control.Mouse_Sensitivity * 0.0335f + 0.25f;
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			num *= 2f;
		}
		this.CameraMove = new Vector2(num, num);
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00011F34 File Offset: 0x00010134
	public void ChangeMouseAimSensitivity()
	{
		float num = this.Setting_Entity_Control.Aim_Mouse_Sensitivity * 0.0105f + 0.075f;
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			num *= 2f;
		}
		this.CameraZoomMove = new Vector2(num, num);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00011F88 File Offset: 0x00010188
	public void onceFire()
	{
		this.fLastFireTime = Time.time;
		Vector2 b = new Vector2(UnityEngine.Random.Range(CameraControl.faRecoil[1] * -1f, CameraControl.faRecoil[2]), CameraControl.faRecoil[0]);
		this.recoilNow += b;
		this.recoilRecover.x = this.recoilNow.x / 0.3f;
		bool flag = this.recoilRecover.x < this.camerHero.wc.wpData.recoilFireX;
		if (flag)
		{
			this.recoilRecover.x = this.camerHero.wc.wpData.recoilFireX;
		}
		this.recoilRecover.y = this.recoilNow.y / 0.3f;
		bool flag2 = this.recoilRecover.y < this.camerHero.wc.wpData.recoilFireY;
		if (flag2)
		{
			this.recoilRecover.y = this.camerHero.wc.wpData.recoilFireY;
		}
		float num = 1f;
		bool flag3 = Mathf.Abs(this.recoilNow.x) > CameraControl.faRecoilMax[1] * num;
		if (flag3)
		{
			this.recoilNow.x = CameraControl.faRecoilMax[1] * num * (this.recoilNow.x / Mathf.Abs(this.recoilNow.x));
		}
		bool flag4 = this.recoilNow.y > CameraControl.faRecoilMax[0] * num;
		if (flag4)
		{
			this.recoilNow.y = CameraControl.faRecoilMax[0] * num;
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00012130 File Offset: 0x00010330
	public void checkHouzuoli()
	{
		bool flag = Time.time - this.fLastFireTime < 0.3f;
		if (flag)
		{
			this.isFiring = true;
		}
		else
		{
			this.isFiring = false;
		}
		bool flag2 = this.camerHero == null || this.camerHero.wc == null;
		if (!flag2)
		{
			float num = 1f;
			bool flag3 = this.recoilNow.y * this.fireingBackAngle < 0f;
			if (flag3)
			{
				this.recoilNow.y = this.recoilNow.y + this.fireingBackAngle;
			}
			bool flag4 = this.houzuoliAngle.y * this.fireingBackAngle < 0f;
			if (flag4)
			{
				this.houzuoliAngle.y = this.houzuoliAngle.y + this.fireingBackAngle;
			}
			bool flag5 = this.isFiring;
			float num2;
			float num3;
			if (flag5)
			{
				bool flag6 = Vector2.Distance(this.houzuoliAngle, this.recoilNow) < 0.01f;
				if (flag6)
				{
					num2 = this.camerHero.wc.wpData.recoilRecoverY * Time.deltaTime * num;
					num3 = ((Mathf.Abs(this.houzuoliAngle.y) >= 0.1f) ? Mathf.Abs(this.houzuoliAngle.x / this.houzuoliAngle.y * num2) : Mathf.Abs(this.houzuoliAngle.x));
					bool flag7 = Mathf.Abs(this.recoilNow.x) <= num3;
					if (flag7)
					{
						this.recoilNow.x = 0f;
					}
					else
					{
						this.recoilNow.x = (Mathf.Abs(this.recoilNow.x) - num3) * (this.recoilNow.x / Mathf.Abs(this.recoilNow.x));
					}
					bool flag8 = Mathf.Abs(this.recoilNow.y) <= num2;
					if (flag8)
					{
						this.recoilNow.y = 0f;
					}
					else
					{
						this.recoilNow.y = (Mathf.Abs(this.recoilNow.y) - num2) * (this.recoilNow.y / Mathf.Abs(this.recoilNow.y));
					}
				}
				else
				{
					num3 = this.recoilRecover.x * Time.deltaTime * num;
					num2 = this.recoilRecover.y * Time.deltaTime * num;
				}
			}
			else
			{
				num2 = this.camerHero.wc.wpData.recoilStop * Time.deltaTime * num;
				num3 = ((Mathf.Abs(this.houzuoliAngle.y) >= 0.1f) ? Mathf.Abs(this.houzuoliAngle.x / this.houzuoliAngle.y * num2) : Mathf.Abs(this.houzuoliAngle.x));
				this.recoilNow = Vector2.zero;
			}
			Vector2 vector = this.houzuoliAngle;
			Vector2 vector2 = this.houzuoliAngle;
			bool flag9 = vector.x > this.recoilNow.x;
			if (flag9)
			{
				vector.x -= num3;
				bool flag10 = vector.x < this.recoilNow.x;
				if (flag10)
				{
					vector.x = this.recoilNow.x;
				}
			}
			else
			{
				bool flag11 = vector.x < this.recoilNow.x;
				if (flag11)
				{
					vector.x += num3;
					bool flag12 = vector.x > this.recoilNow.x;
					if (flag12)
					{
						vector.x = this.recoilNow.x;
					}
				}
			}
			bool flag13 = vector.y > this.recoilNow.y;
			if (flag13)
			{
				vector.y -= num2;
				bool flag14 = vector.y < this.recoilNow.y;
				if (flag14)
				{
					vector.y = this.recoilNow.y;
				}
			}
			else
			{
				bool flag15 = vector.y < this.recoilNow.y;
				if (flag15)
				{
					vector.y += num2;
					bool flag16 = vector.y > this.recoilNow.y;
					if (flag16)
					{
						vector.y = this.recoilNow.y;
					}
				}
			}
			this.houzuoliAngle = vector;
			this.xhouzuo = vector2.x - vector.x;
			this.yhouzuo = vector2.y - vector.y;
		}
	}

	// Token: 0x040001E1 RID: 481
	private NetLiving nowHero;

	// Token: 0x040001E2 RID: 482
	public static float SmoothMin = 30f;

	// Token: 0x040001E3 RID: 483
	public static float SmoothMax = 150f;

	// Token: 0x040001E4 RID: 484
	public Camera controlCamera;

	// Token: 0x040001E5 RID: 485
	public Camera controlCameraSec;

	// Token: 0x040001E6 RID: 486
	public bool dontMove;

	// Token: 0x040001E7 RID: 487
	private Transform _cameraAnimator;

	// Token: 0x040001E8 RID: 488
	private Transform cameraAnimator;

	// Token: 0x040001E9 RID: 489
	private Transform _nowTarget;

	// Token: 0x040001EA RID: 490
	public Transform AttackTarget;

	// Token: 0x040001EB RID: 491
	public NetLiving NowTargetNetObj;

	// Token: 0x040001EC RID: 492
	private GameObject TransObjMid;

	// Token: 0x040001ED RID: 493
	public float cameraSpeed = 150f;

	// Token: 0x040001EE RID: 494
	public float cameraChangeSpeed = 20f;

	// Token: 0x040001EF RID: 495
	public static int iCameraState = -1;

	// Token: 0x040001F0 RID: 496
	public static int iCameraCtrl = 1;

	// Token: 0x040001F1 RID: 497
	public static float[] faRecoil = new float[]
	{
		1f,
		1f,
		1f
	};

	// Token: 0x040001F2 RID: 498
	public static float[] faRecoilMax = new float[]
	{
		1f,
		1f
	};

	// Token: 0x040001F3 RID: 499
	public float xhouzuo;

	// Token: 0x040001F4 RID: 500
	public float yhouzuo;

	// Token: 0x040001F5 RID: 501
	public Vector2 houzuoliAngle = Vector2.zero;

	// Token: 0x040001F6 RID: 502
	public float x;

	// Token: 0x040001F7 RID: 503
	public float y;

	// Token: 0x040001F8 RID: 504
	public float cx;

	// Token: 0x040001F9 RID: 505
	public float cy;

	// Token: 0x040001FA RID: 506
	public float gx;

	// Token: 0x040001FB RID: 507
	public float gy;

	// Token: 0x040001FC RID: 508
	public int yMinLimit = -85;

	// Token: 0x040001FD RID: 509
	public int yMaxLimit = 85;

	// Token: 0x040001FE RID: 510
	public int xMinLimit = -45;

	// Token: 0x040001FF RID: 511
	public int xMaxLimit = 45;

	// Token: 0x04000200 RID: 512
	public float sensitivityX = 0.01332f;

	// Token: 0x04000201 RID: 513
	public float sensitivityY = 0.01665f;

	// Token: 0x04000202 RID: 514
	private float fDis;

	// Token: 0x04000203 RID: 515
	private float fDisAllow;

	// Token: 0x04000204 RID: 516
	public Ray cursorRay = default(Ray);

	// Token: 0x04000205 RID: 517
	private Vector2 recoilNow;

	// Token: 0x04000206 RID: 518
	private Vector2 recoilRecover;

	// Token: 0x04000207 RID: 519
	private float fLastFireTime;

	// Token: 0x04000208 RID: 520
	private int _nowCameraMask;

	// Token: 0x04000209 RID: 521
	private float cameraMaxDis = 0.5f;

	// Token: 0x0400020A RID: 522
	public float cameraMinDis = 0.2f;

	// Token: 0x0400020B RID: 523
	private float cameraFDisFactor;

	// Token: 0x0400020C RID: 524
	public Vector3 tpsHeight;

	// Token: 0x0400020D RID: 525
	public Vector3 fpsHeight;

	// Token: 0x0400020E RID: 526
	private bool _CameraZoom;

	// Token: 0x0400020F RID: 527
	private Setting_Entity_Control _Setting_Entity_Control;

	// Token: 0x04000210 RID: 528
	private NetLiving attacker;

	// Token: 0x04000211 RID: 529
	private float camHeightDis;

	// Token: 0x04000212 RID: 530
	private static CameraControl _instance;

	// Token: 0x04000213 RID: 531
	private Vector2 _CameraMove = Vector2.one;

	// Token: 0x04000214 RID: 532
	private Vector2 _CameraZoomMove = Vector2.one;

	// Token: 0x04000215 RID: 533
	private bool _DashMouseMoveSpeed;

	// Token: 0x04000216 RID: 534
	private Vector3 cameraAnimatorPos;

	// Token: 0x04000217 RID: 535
	private Vector3 cameraPaQiangPos;

	// Token: 0x04000218 RID: 536
	private Quaternion cameraAnimatorRot;

	// Token: 0x04000219 RID: 537
	private float cameraAnimatorSpeed;

	// Token: 0x0400021A RID: 538
	public int MoveDir;

	// Token: 0x0400021B RID: 539
	private Quaternion nowTargetRot = Quaternion.identity;

	// Token: 0x0400021C RID: 540
	private Quaternion nowTransformRot = Quaternion.identity;

	// Token: 0x0400021D RID: 541
	private Vector3 nowPosition;

	// Token: 0x0400021E RID: 542
	private Quaternion lastTargetRotation;

	// Token: 0x0400021F RID: 543
	private Quaternion lastTransformRotation;

	// Token: 0x04000220 RID: 544
	private Vector3 lerpWatchTeamatesPos;

	// Token: 0x04000221 RID: 545
	private Vector3 lastTargetPos;

	// Token: 0x04000222 RID: 546
	private float lastHeightPos;

	// Token: 0x04000223 RID: 547
	private bool RotateCamera;

	// Token: 0x04000224 RID: 548
	private RaycastHit[] rays = new RaycastHit[5];

	// Token: 0x04000225 RID: 549
	private float UpPostion;

	// Token: 0x04000226 RID: 550
	private bool isFiring;

	// Token: 0x04000227 RID: 551
	private float fireingBackAngle;
}
