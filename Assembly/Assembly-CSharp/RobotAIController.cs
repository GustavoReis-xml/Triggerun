using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020002D1 RID: 721
public class RobotAIController : MonoBehaviour
{
	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x06002159 RID: 8537 RVA: 0x00134AAC File Offset: 0x00132CAC
	// (set) Token: 0x0600215A RID: 8538 RVA: 0x00134AC4 File Offset: 0x00132CC4
	public RobotAI robot
	{
		get
		{
			return this._robot;
		}
		set
		{
			this._robot = value;
		}
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x0600215B RID: 8539 RVA: 0x00134AD0 File Offset: 0x00132CD0
	// (set) Token: 0x0600215C RID: 8540 RVA: 0x00134AE8 File Offset: 0x00132CE8
	public Vector3 currentTargetPos
	{
		get
		{
			return this._currentTargetPos;
		}
		set
		{
			this._currentTargetPos = value;
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x0600215D RID: 8541 RVA: 0x00134AF4 File Offset: 0x00132CF4
	// (set) Token: 0x0600215E RID: 8542 RVA: 0x00134B0C File Offset: 0x00132D0C
	public Vector3 SyncTarget
	{
		get
		{
			return this._SyncTarget;
		}
		set
		{
			this._SyncTarget = value;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x0600215F RID: 8543 RVA: 0x00134B18 File Offset: 0x00132D18
	// (set) Token: 0x06002160 RID: 8544 RVA: 0x00134B30 File Offset: 0x00132D30
	public bool RobotHasPath
	{
		get
		{
			return this._RobotHasPath;
		}
		set
		{
			this._RobotHasPath = value;
		}
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x00134B3C File Offset: 0x00132D3C
	public virtual void Start()
	{
		bool flag = this.robot.playerCommon.m_nShili == 1;
		if (flag)
		{
			this.layer = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
		}
		else
		{
			this.layer = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_mainplayer"));
		}
		this.pullLayer = (1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2"));
		this.pullLayer2 = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2"));
		this.moveParm = 1f;
		this.b_moveAsDash = false;
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x00134C74 File Offset: 0x00132E74
	public virtual void InitRobotControllerData()
	{
		this.path = new NavMeshPath();
		this.RobotHasPath = true;
		this.moveAttack = true;
		this.moveType = this.robot.MoveType;
		this.followRange = this.robot.FollowRange;
		this.eyeRange = this.robot.EyeRange;
		this.eyeAngle = this.robot.EyeAngle;
		this.earRange = this.robot.EarRange;
		this.turnRotationSpeed = this.robot.turnRotationSpeed;
		this.DodgeRate = this.robot.DodgeRate;
		this.AttackRange = this.robot.shootRange;
		this.safeRange = this.robot.safeRange;
		this.hitRecoverCD = 0f;
		this.targetPos = new List<Vector3>();
		this.stayAreas = new List<float>();
		this.stayTimes = new List<float>();
		this.b_isTranes = new List<bool>();
		this.SyncPos = this.robot.UserControl.transform.position;
		this.SyncAngel = this.robot.UserControl.transform.eulerAngles;
		this.SyncTarget = this.robot.playerTarget.transform.position;
		this.setUpAgent();
		this.getPath = false;
		this.weaponFireName = 1;
		this.InitMoveTargetPos();
		this.CTPIndex = 0;
		this.currentStayTimer = 0f;
		this.currentStayArea = 1f;
		this.heroID = fGameManager.GetInstance().getPlayer().getNetid();
		this.hasPath = true;
		this.CurrentTargetDis = 0f;
		this.b_ChaseMove = false;
		this.b_MoveAroundAttack = false;
		this.stopChasing = false;
		this.needMoveBack = false;
		this.startToMoveNextPos = false;
		this.cancelSprint = false;
		this.chaseTarget = null;
		this.transprotPos = Vector3.zero;
		this.gameBegin = true;
		bool flag = this.robot.beMonster != 0;
		if (flag)
		{
			this.findTargetTimer = 0.5f;
			this.checkTargetInSightTimer = 0.5f;
		}
		else
		{
			this.findTargetTimer = 0f;
			this.checkTargetInSightTimer = 0f;
		}
		this.robot.tChar.groundCheckDelaySwitch = true;
		this.b_moveAsDash = false;
		this.robot.heroBase.RobotCreateFinish();
		this.robot.tChar.limitNowDis = 4f;
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x00134EE4 File Offset: 0x001330E4
	private void setUpAgent()
	{
		bool flag = base.GetComponent<NavMeshAgent>() == null;
		if (flag)
		{
			this.Agent = base.gameObject.AddComponent<NavMeshAgent>();
		}
		else
		{
			this.Agent = base.GetComponent<NavMeshAgent>();
		}
		this.Agent.speed = 3f;
		this.Agent.angularSpeed = 0f;
		this.Agent.nextPosition = this.SyncPos;
		this.Agent.enabled = true;
		bool needSendToServer = this.robot.needSendToServer;
		if (needSendToServer)
		{
			this.GetCTPos();
			this.SyncPosition();
			this.Agent.updatePosition = true;
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_pFbData.nType == 12;
			if (flag2)
			{
				this.Agent.updatePosition = false;
			}
			else
			{
				bool flag3 = MatchManager.GetInstance().m_pFbData.nType == 9;
				if (flag3)
				{
					this.Agent.updatePosition = true;
				}
			}
		}
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x00134FE2 File Offset: 0x001331E2
	public virtual void RobotHaveCreate()
	{
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x00134FE8 File Offset: 0x001331E8
	public virtual void RobotDeath()
	{
		this.skillIng = false;
		bool flag = this != null && base.GetComponent<NavMeshAgent>() != null;
		if (flag)
		{
			this.Agent.updatePosition = false;
			this.Agent.enabled = false;
			this.gameBegin = false;
		}
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x0013503C File Offset: 0x0013323C
	public virtual void Update()
	{
		bool flag = this.robot == null;
		if (!flag)
		{
			bool flag2 = this.robot.SyncNetObj == null;
			if (flag2)
			{
				this.SyncTimeStep = 1f;
			}
			else
			{
				bool flag3 = fUIManager.GetInstance().GetNowGameState() <= 0;
				if (!flag3)
				{
					bool flag4 = this.hitRecoverCD > 0f;
					if (flag4)
					{
						this.hitRecoverCD -= Time.deltaTime;
					}
					else
					{
						bool flag5 = !this.gameBegin;
						if (!flag5)
						{
							bool flag6 = this.currentStayTimer > 0f;
							if (flag6)
							{
								this.currentStayTimer -= Time.deltaTime;
							}
							this.MoveAsDash(this.robot.UserControl.transform.forward * Time.deltaTime * 20f);
							bool flag7 = this.SyncTimeStep < 0.1f;
							if (flag7)
							{
								this.SyncTimeStep += Time.deltaTime;
							}
							else
							{
								bool flag8 = !this.robot.needSendToServer || this.robot.SyncNetObj.getNetid() != this.heroID;
								if (!flag8)
								{
									bool flag9 = !this.RobotHasPath;
									if (flag9)
									{
										bool flag10 = this.calcuteTimer > 0f;
										if (flag10)
										{
											this.calcuteTimer -= Time.deltaTime;
										}
										else
										{
											this.calcuteTimer = 2f;
											this.StopChaseAndCalPath(true);
										}
									}
									bool flag11 = this.robot.playerCommon.m_nHunMi == 1 || this.robot.playerCommon.m_nDongjie == 1;
									if (flag11)
									{
										this.SyncPos = this.robot.UserControl.transform.position;
									}
									bool flag12 = this.robot.playerCommon.m_nHunMi == 0 && this.robot.playerCommon.m_nDongjie == 0;
									if (flag12)
									{
										this.CheckStuck();
									}
									bool flag13 = !this.hitByShawang && !this.robot.isDead && this.robot.playerCommon.m_nHunMi == 0 && this.robot.playerCommon.m_nDongjie == 0 && !this.b_moveAsDash && !this.releasedZR;
									if (flag13)
									{
										this.UpdateClosestEnemyTarget();
										bool flag14 = this.chaseTarget != null && this.chaseTarget.haveDestroy;
										if (flag14)
										{
											this.chaseTarget = null;
										}
										bool flag15 = this.moveAttack || !this.attack;
										if (flag15)
										{
											bool flag16 = !this.ReachPos() && !this.b_ChaseMove && this.chaseTarget == null;
											if (flag16)
											{
												this.GoToCTP();
											}
											bool flag17 = this.b_MoveAroundAttack && this.stopChasing;
											if (flag17)
											{
												bool flag18 = this.chaseTarget != null;
												if (flag18)
												{
													this.MoveAroundAttack();
												}
												else
												{
													this.b_MoveAroundAttack = false;
												}
											}
										}
										bool flag19 = this.moveAttack || this.robot.charMove.state.move.magnitude < 0.1f;
										if (flag19)
										{
											this.Attack();
										}
										bool flag20 = this.chaseTarget != null && this.followRange != 0f;
										if (flag20)
										{
											this.needTrans = false;
											this.ChaseEnemy();
											bool flag21 = !this.stopChasing;
											if (flag21)
											{
												this.GoToCTP();
											}
										}
										bool flag22 = !this.attack && this.startToMoveNextPos;
										if (flag22)
										{
											this.StopChaseAndCalPath(false);
										}
									}
									bool flag23 = this.robot.charMove != null;
									if (flag23)
									{
										((TBUserControlIt)this.robot.charMove).moveTarget = this.SyncPos;
									}
									this.SyncPosition();
									this.Rotate();
									this.TongbuAngle();
									this.lastRobotPos = this.robot.UserControl.transform.position;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x0013547C File Offset: 0x0013367C
	private void MoveAroundAttack()
	{
		bool flag = this.robot.beMonster != 0 || UnityEngine.Random.Range(0f, 1f) > this.DodgeRate;
		if (!flag)
		{
			bool flag2 = this.needMoveBack;
			if (flag2)
			{
				this.SyncPos = this.robot.tChar.transform.position - this.robot.tChar.transform.forward * 1.5f;
				this.moveAroundPos = this.SyncPos;
			}
			else
			{
				float num = Vector3.Distance(this.robot.tChar.transform.position, this.moveAroundPos);
				bool flag3 = Vector3.Distance(this.lastRobotPos, this.robot.tChar.transform.position) < 0.1f;
				if (flag3)
				{
					this.stayTimer -= Time.deltaTime;
					bool flag4 = this.stayTimer <= 0f;
					if (flag4)
					{
						this.SyncPos = this.robot.tChar.transform.position;
						this.caseNum = UnityEngine.Random.Range(0, 2);
						this.reach = true;
						this.stayTimer = 0.2f;
					}
				}
				else
				{
					this.stayTimer = 0.2f;
				}
				bool flag5 = num < 0.2f;
				if (flag5)
				{
					this.caseNum = UnityEngine.Random.Range(0, 2);
					this.reach = true;
				}
				bool flag6 = !this.reach || !this.robot.tChar.onGround;
				if (!flag6)
				{
					int num2 = this.caseNum;
					int num3 = num2;
					if (num3 != 0)
					{
						if (num3 == 1)
						{
							bool flag7 = !Physics.BoxCast(this.robot.tChar.transform.position + Vector3.up, Vector3.one / 2f, this.robot.tChar.transform.right, Quaternion.identity, 2f);
							if (flag7)
							{
								this.SyncPos = this.robot.tChar.transform.position + this.robot.tChar.transform.right * 1.5f;
								this.moveAroundPos = this.SyncPos;
								this.reach = false;
							}
							else
							{
								this.caseNum = UnityEngine.Random.Range(0, 2);
							}
						}
					}
					else
					{
						bool flag8 = !Physics.BoxCast(this.robot.tChar.transform.position + Vector3.up, Vector3.one / 2f, -this.robot.tChar.transform.right, Quaternion.identity, 2f);
						if (flag8)
						{
							this.SyncPos = this.robot.tChar.transform.position - this.robot.tChar.transform.right * 1.5f;
							this.moveAroundPos = this.SyncPos;
							this.reach = false;
						}
						else
						{
							this.caseNum = UnityEngine.Random.Range(0, 2);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x001357D0 File Offset: 0x001339D0
	private void MoveBack()
	{
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x001357D3 File Offset: 0x001339D3
	private void SyncUpdate()
	{
		this.SyncPosition();
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x001357E0 File Offset: 0x001339E0
	public virtual void SyncPosition()
	{
		bool flag = this.robot != null && this.robot.SyncNetObj != null && (Vector3.Distance(this.lastRobotPos, this.robot.UserControl.transform.position) >= 0.1f || Vector3.Distance(this.previousPos, this.SyncPos) >= 0.1f) && this.Agent.enabled;
		if (flag)
		{
			this.previousPos = this.robot.UserControl.transform.position;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new ArrayList
			{
				(int)(this.SyncPos.x * 100f),
				(int)(this.SyncPos.y * 100f),
				(int)(this.SyncPos.z * 100f)
			});
			arrayList.Add(this.robot.isDead ? 1 : 0);
			arrayList.Add(this.robot.getNetid());
			this.SyncTimeStep = 0f;
			this.robot.SyncNetObj.TosFun("SSP", new object[]
			{
				arrayList
			});
		}
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x00135944 File Offset: 0x00133B44
	public void InitMoveTargetPos()
	{
		this.targetPos.Clear();
		this.stayTimes.Clear();
		this.stayAreas.Clear();
		this.b_isTranes.Clear();
		bool flag = this.targetPos.Count == 0;
		if (flag)
		{
			for (int i = 0; i < this.robot.AIFollowPaths.Count; i++)
			{
				this.targetPos.Add(this.robot.AIFollowPaths[i]._movePos);
				this.stayTimes.Add(this.robot.AIFollowPaths[i].StayTime);
				this.stayAreas.Add(this.robot.AIFollowPaths[i].StayArea);
				this.b_isTranes.Add(this.robot.AIFollowPaths[i].b_isTrans);
			}
		}
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x00135A48 File Offset: 0x00133C48
	protected void GetCTPos()
	{
		bool flag = this.currentStayTimer > 0f;
		if (!flag)
		{
			bool flag2 = this.chaseTarget == null || this.chaseTarget.UserControl == null;
			if (flag2)
			{
				bool flag3 = this.CTPIndex >= this.targetPos.Count && this.targetPos.Count > 0;
				if (flag3)
				{
					bool flag4 = this.moveType == 0;
					if (flag4)
					{
						bool flag5 = this.robot.beMonster == 0;
						if (flag5)
						{
							this.PatrollCenterPos = this.targetPos[this.targetPos.Count - 1];
							this.Wander();
							this.Agent.updatePosition = true;
							this.Agent.enabled = true;
							this.needTrans = false;
							this.Agent.Stop();
							this.Agent.SetDestination(this.currentTargetPos);
							this.lastPathIndex = 0;
							this.getPath = false;
						}
						else
						{
							this.CTPIndex = this.targetPos.Count - 1;
						}
						return;
					}
					bool flag6 = this.moveType == 1;
					if (flag6)
					{
						this.CTPIndex = 0;
					}
				}
				bool flag7 = this.targetPos.Count > 0;
				if (flag7)
				{
					this.CTPIndex = Mathf.Clamp(this.CTPIndex, 0, this.targetPos.Count - 1);
					this.currentTargetPos = this.targetPos[this.CTPIndex];
					this.currentStayTimer = this.stayTimes[this.CTPIndex];
					bool flag8 = this.currentStayTimer != 0f;
					if (flag8)
					{
						this.currentStayTimer = UnityEngine.Random.Range(1f, this.currentStayTimer);
					}
					this.currentStayArea = this.stayAreas[this.CTPIndex];
					this.needTrans = this.b_isTranes[this.CTPIndex];
					this.transprotPos = this.targetPos[this.CTPIndex];
				}
				else
				{
					this.currentTargetPos = this.robot.RobotCreatePos;
				}
			}
			else
			{
				bool flag9 = this.followRange > 0f;
				if (flag9)
				{
					this.currentTargetPos = this.chaseTarget.UserControl.transform.position;
				}
			}
			this.Agent.updatePosition = true;
			this.Agent.enabled = true;
			this.Agent.Stop();
			this.Agent.SetDestination(this.currentTargetPos);
			this.lastPathIndex = 0;
			this.getPath = false;
			this.stopChasing = false;
			bool flag10 = this.CTPIndex != 0;
			if (flag10)
			{
				this.calcuteTimer = 0.5f;
			}
			this.CTPIndex++;
		}
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x00135D28 File Offset: 0x00133F28
	private void GoToCTP()
	{
		bool flag = !this.RobotHasPath || this.Agent == null || !this.Agent.enabled || !this.Agent.hasPath;
		if (flag)
		{
			this.Pathdir = (this.currentTargetPos - base.transform.position).normalized;
			bool flag2 = !this.robot.tChar.hangInAir;
			if (flag2)
			{
				this.SyncPos = base.transform.position + this.Pathdir * this.moveSpeed * this.moveParm;
			}
			bool flag3 = this.chaseTarget == null || !this.TargetInSight;
			if (flag3)
			{
				this.SyncTarget = this.SyncPos + this.Pathdir * 100f + Vector3.up;
			}
			bool flag4 = this.Agent != null && this.Agent.enabled;
			if (flag4)
			{
				this.Agent.nextPosition = this.SyncPos;
			}
			this.getPath = false;
		}
		else
		{
			bool flag5 = this.Agent.enabled && this.Agent.hasPath && !this.getPath;
			if (flag5)
			{
				this.paths = this.Agent.path.corners;
				this.getPath = true;
				bool flag6 = this.lastPathIndex + 1 < this.paths.Length;
				if (flag6)
				{
					this.PathIndex = this.lastPathIndex + 1;
				}
				else
				{
					this.PathIndex = this.paths.Length - 1;
				}
				this.RobotHasPath = true;
				this.Pathdir = (this.paths[this.PathIndex] - base.transform.position).normalized;
				bool flag7 = !this.robot.tChar.hangInAir;
				if (flag7)
				{
					this.SyncPos = base.transform.position + this.Pathdir * 1.2f;
				}
				bool flag8 = this.chaseTarget == null || !this.TargetInSight;
				if (flag8)
				{
					this.SyncTarget = this.SyncPos + this.Pathdir * 100f + Vector3.up;
				}
			}
			bool flag9 = this.getPath;
			if (flag9)
			{
				bool flag10 = this.PathIndex < this.paths.Length;
				if (flag10)
				{
					bool flag11 = this.chaseTarget == null || Vector3.Distance(this.currentTargetPos, this.robot.UserControl.transform.position) > this.AttackRange;
					if (flag11)
					{
						this.SyncTarget = this.SyncPos + this.Pathdir * 100f + Vector3.up;
					}
					bool flag12 = Vector3.Dot(this.robot.UserControl.transform.forward, (this.paths[this.PathIndex] - base.transform.position).normalized) < 0f;
					if (flag12)
					{
						this.Agent.updatePosition = true;
						this.Agent.enabled = true;
						this.Agent.Stop();
						this.getPath = false;
						this.Agent.SetDestination(this.currentTargetPos);
						this.lastPathIndex = 0;
					}
					bool flag13 = Vector3.Distance(this.paths[this.PathIndex], base.transform.position) < 0.2f;
					if (flag13)
					{
						this.lastPathIndex = this.PathIndex;
						this.Pathdir = (this.paths[this.PathIndex] - base.transform.position).normalized;
						this.PathIndex++;
					}
					else
					{
						bool flag14 = !this.robot.tChar.hangInAir;
						if (flag14)
						{
							this.SyncPos = base.transform.position + this.Pathdir * this.moveSpeed * this.moveParm;
						}
					}
				}
				else
				{
					this.getPath = false;
				}
			}
			else
			{
				bool flag15 = this.chaseTarget == null;
				if (flag15)
				{
					this.StopChaseAndCalPath(false);
				}
			}
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x001361CC File Offset: 0x001343CC
	public void StopChaseAndCalPath(bool needCal = false)
	{
		float num = 100000f;
		bool flag = false;
		this.Agent.updatePosition = true;
		this.Agent.enabled = true;
		this.Agent.Stop();
		bool flag2 = this.robot.beMonster > 0 && !needCal;
		if (flag2)
		{
			this.startToMoveNextPos = false;
			this.GetCTPos();
		}
		else
		{
			int ctpindex = 0;
			int num2 = -1;
			for (int i = 0; i < this.targetPos.Count; i++)
			{
				bool flag3 = this.Agent.CalculatePath(this.targetPos[i], this.path);
				if (flag3)
				{
					float num3 = Vector3.Distance(this.targetPos[i], this.robot.UserControl.transform.position);
					bool flag4 = num > num3 && num3 > 5f;
					if (flag4)
					{
						num2 = i;
						bool flag5 = this.CTPIndex == this.targetPos.Count;
						if (flag5)
						{
							bool flag6 = i < this.CTPIndex;
							if (flag6)
							{
								ctpindex = i;
								num = Vector3.Distance(this.targetPos[i], this.robot.UserControl.transform.position);
								flag = true;
							}
						}
						else
						{
							bool flag7 = i >= this.CTPIndex;
							if (flag7)
							{
								ctpindex = i;
								num = Vector3.Distance(this.targetPos[i], this.robot.UserControl.transform.position);
								flag = true;
							}
						}
					}
				}
				bool flag8 = num2 != -1 && !flag;
				if (flag8)
				{
					flag = true;
					ctpindex = num2;
				}
			}
			bool flag9 = flag;
			if (flag9)
			{
				this.CTPIndex = ctpindex;
				this.startToMoveNextPos = false;
				this.GetCTPos();
			}
			else
			{
				this.RobotHasPath = false;
			}
		}
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x001363B4 File Offset: 0x001345B4
	private bool ReachPos()
	{
		float f = Vector3.Distance(this.currentTargetPos, base.transform.position);
		float num = UnityEngine.Random.Range(1f, this.currentStayArea);
		bool flag = Mathf.Abs(f) < num;
		bool result;
		if (flag)
		{
			this.GetCTPos();
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x00136408 File Offset: 0x00134608
	private void Wander()
	{
		Vector3 vector = this.PatrollCenterPos + new Vector3(UnityEngine.Random.Range(-6f, 6f), 0f, UnityEngine.Random.Range(-6f, 6f));
		while (Physics.CheckSphere(vector + Vector3.up, 0.5f, this.pullLayer))
		{
			vector = this.PatrollCenterPos + new Vector3(UnityEngine.Random.Range(-6f, 6f), 0f, UnityEngine.Random.Range(-6f, 6f));
		}
		this.currentTargetPos = vector;
		this.currentStayArea = 3f;
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x001364BC File Offset: 0x001346BC
	private void ChaseEnemy()
	{
		bool flag = this.chaseTarget.defaultMesh == null;
		if (flag)
		{
			this.chaseTarget = null;
			this.startToMoveNextPos = true;
			this.TargetDis = 0f;
		}
		else
		{
			this.ignoreTargetTimer -= Time.deltaTime;
			this.stopChasing = false;
			float num = this.TargetDis = Vector3.Distance(this.robot.tChar.transform.position, this.chaseTarget.defaultMesh.transform.position);
			bool flag2 = this.AttackRange < 6f && this.chaseTarget.netobject_type == 2 && ((Npc_Entity)this.chaseTarget).Physics != 0;
			if (flag2)
			{
				Vector3 position = this.robot.tChar.transform.position;
				position.x = 0f;
				Vector3 position2 = this.chaseTarget.defaultMesh.transform.position;
				position2.x = 0f;
				num = Vector3.Distance(position, position2);
			}
			bool flag3 = num < this.AttackRange && this.TargetInSight;
			if (flag3)
			{
				this.canAttack = true;
			}
			bool flag4 = num > this.AttackRange || !this.TargetInSight;
			if (flag4)
			{
				this.canAttack = false;
				this.attack = false;
			}
			bool flag5 = num < this.safeRange;
			if (flag5)
			{
				this.b_MoveAroundAttack = true;
				this.b_ChaseMove = false;
				this.needMoveBack = true;
				bool flag6 = this.robot.beMonster > 0;
				if (flag6)
				{
					this.stopChasing = true;
				}
			}
			else
			{
				bool flag7 = num < this.AttackRange && this.TargetInSight && this.robot.beMonster == 0;
				if (flag7)
				{
					this.stopChasing = true;
					this.b_MoveAroundAttack = true;
					this.b_ChaseMove = false;
					this.needMoveBack = false;
				}
				else
				{
					bool flag8 = num < this.AttackRange && this.TargetInSight && this.robot.beMonster > 0;
					if (flag8)
					{
						this.stopChasing = true;
						this.b_MoveAroundAttack = true;
						this.b_ChaseMove = false;
						this.needMoveBack = false;
						this.SyncPos = this.robot.UserControl.transform.position;
					}
					else
					{
						bool flag9 = num < this.followRange;
						if (flag9)
						{
							this.stopChasing = false;
							this.b_MoveAroundAttack = false;
							this.needMoveBack = false;
							this.b_ChaseMove = true;
							this.calcuteTimer = 0.5f;
							this.currentTargetPos = this.chaseTarget.defaultMesh.transform.position;
							this.ignoreTargetTimer = 1f;
							this.Agent.updatePosition = true;
							this.Agent.enabled = true;
							this.Agent.SetDestination(this.currentTargetPos);
							this.lastPathIndex = 0;
							this.Agent.Stop();
							this.getPath = false;
						}
					}
				}
			}
			bool isDead = this.chaseTarget.isDead;
			if (isDead)
			{
				this.chaseTarget = null;
				this.b_ChaseMove = false;
				this.startToMoveNextPos = true;
				this.robot.heroBase.SetRobotState(false);
			}
		}
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x00136800 File Offset: 0x00134A00
	private void Rotate()
	{
		bool flag = !this.robot.isDead;
		if (flag)
		{
			this.v3OldTarget = this.robot.playerTarget.transform.position;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new ArrayList
			{
				(int)(this.SyncTarget.x * 100f),
				(int)(this.SyncTarget.y * 100f),
				(int)(this.SyncTarget.z * 100f)
			});
			arrayList.Add(this.robot.isDead ? 1 : 0);
			arrayList.Add(this.robot.getNetid());
			this.SyncTimeStep = 0f;
			this.robot.TosFun("RST", new object[]
			{
				arrayList
			});
		}
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x00136908 File Offset: 0x00134B08
	private void TongbuAngle()
	{
		bool flag = !this.robot.isDead;
		if (flag)
		{
			Vector3 normalized = (this.currentTargetPos - base.transform.position).normalized;
			Vector3 forward = this.robot.UserControl.transform.forward;
			float num = Vector3.Angle(normalized, forward);
			this.SyncAngel = Quaternion.AngleAxis(num * Time.deltaTime * 5f * this.turnRotationSpeed, Vector3.up).eulerAngles;
			bool flag2 = Mathf.Abs(num) >= 0.5f;
			if (flag2)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add((int)this.SyncAngel.y);
				arrayList.Add(this.robot.isDead ? 1 : 0);
				arrayList.Add(this.robot.getNetid());
				this.SyncTimeStep = 0f;
				this.robot.TosFun("RCR", new object[]
				{
					arrayList
				});
			}
		}
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x00136A30 File Offset: 0x00134C30
	public virtual void Attack()
	{
		bool flag = this.robot.playerCommon.m_nInJidi == 1 && this.chaseTarget == null;
		if (flag)
		{
			this.attack = false;
			this.b_ChaseMove = false;
		}
		else
		{
			this.FindTarget();
			bool flag2 = this.chaseTarget != null && this.TargetInSight;
			if (flag2)
			{
				bool flag3 = this.canAttack;
				if (flag3)
				{
					this.SyncTarget = this.chaseTarget.UserControl.transform.position + Vector3.up * 1.5f;
					this.Fire();
				}
			}
			else
			{
				this.attack = false;
				this.b_ChaseMove = false;
			}
			bool flag4 = this.chaseTarget == null;
			if (flag4)
			{
				this.CurrentTargetDis = 100f;
			}
		}
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x00136B04 File Offset: 0x00134D04
	private void FindTarget()
	{
		bool flag = this.chaseTarget != null;
		if (flag)
		{
			bool flag2 = this.robot.beMonster == 3;
			if (flag2)
			{
				this.TargetInSight = true;
			}
			else
			{
				Vector3 normalized = (this.chaseTarget.defaultMesh.transform.position - this.robot.tChar.transform.position).normalized;
				float d = 0.1f;
				bool flag3 = this.robot.tChar.transform.position.y - this.chaseTarget.defaultMesh.transform.position.y > 5f;
				if (flag3)
				{
					d = 1f;
				}
				this.TargetInSight = !Physics.Raycast(this.robot.tChar.transform.position + Vector3.up * d, normalized, Vector3.Distance(this.robot.tChar.transform.position, this.chaseTarget.defaultMesh.transform.position), this.pullLayer2);
				this.CurrentTargetDis = Vector3.Distance(this.chaseTarget.defaultMesh.transform.position, this.robot.tChar.transform.position);
				bool flag4 = this.CurrentTargetDis > this.eyeRange || this.chaseTarget.isDead || this.chaseTarget.heroBase.IsHeroInvi() || this.robot.playerCommon.m_nInJidi == 1;
				if (flag4)
				{
					this.chaseTarget = null;
					this.startToMoveNextPos = true;
				}
				bool flag5 = this.CurrentTargetDis > this.followRange && !this.TargetInSight;
				if (flag5)
				{
					this.chaseTarget = null;
					this.startToMoveNextPos = true;
				}
				bool flag6 = this.robot.playerCommon.m_nInJidi == 1;
				if (flag6)
				{
					this.chaseTarget = null;
					this.startToMoveNextPos = true;
				}
				bool flag7 = this.chaseTarget == null;
				if (flag7)
				{
					this.robot.heroBase.SetRobotState(false);
				}
			}
		}
		else
		{
			bool flag8 = this.robot.playerCommon.m_nInJidi == 0;
			if (flag8)
			{
				this.GetDeadMemberNum();
			}
		}
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x00136D68 File Offset: 0x00134F68
	private void GetDeadMemberNum()
	{
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(position, this.eyeRange, this.layer);
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = (array[i].gameObject.layer == LayerMask.NameToLayer("force2_npc") || array[i].gameObject.layer == LayerMask.NameToLayer("force1_npc")) && array[i].gameObject.tag.CompareTo("can_damage_robot") != 0;
			if (flag)
			{
				this.chaseTarget = null;
			}
			else
			{
				GameObject gameObject = array[i].gameObject.transform.root.gameObject;
				bool flag2 = this.goRoot.IndexOf(gameObject) != -1;
				if (!flag2)
				{
					this.goRoot.Add(gameObject);
					PublicInfo component = gameObject.GetComponent<PublicInfo>();
					bool flag3 = !(component != null);
					if (!flag3)
					{
						NetLiving netObject = component.NetObject;
						bool isDead = netObject.isDead;
						if (!isDead)
						{
							float d = 0.1f;
							bool flag4 = this.robot.tChar.transform.position.y - array[i].transform.position.y > 5f;
							if (flag4)
							{
								d = 1f;
							}
							bool flag5 = Physics.Raycast(this.robot.tChar.transform.position + Vector3.up * d, array[i].transform.position - this.robot.tChar.transform.position, Vector3.Distance(this.robot.tChar.transform.position, array[i].transform.position), this.pullLayer2) && this.robot.beMonster != 3;
							if (flag5)
							{
								this.chaseTarget = null;
							}
							else
							{
								bool flag6 = this.AttackRange < 6f && netObject.netobject_type == 2;
								if (flag6)
								{
									bool flag7 = ((Npc_Entity)netObject).Physics == 0;
									if (flag7)
									{
										this.chaseTarget = null;
										goto IL_4F7;
									}
								}
								else
								{
									bool flag8 = this.AttackRange >= 10f && netObject.netobject_type == 2 && ((Npc_Entity)netObject).Type != 1;
									if (flag8)
									{
										this.chaseTarget = null;
										goto IL_4F7;
									}
								}
								bool flag9 = netObject.heroBase.IsHeroInvi();
								if (flag9)
								{
									break;
								}
								this.chaseTarget = netObject;
								bool flag10 = this.chaseTarget.tChar != null && Vector3.Distance(position, this.chaseTarget.tChar.transform.position) > this.earRange;
								if (flag10)
								{
									Vector3 vector = this.robot.tChar.transform.forward;
									vector.y = 0f;
									vector = vector.normalized;
									Vector3 normalized = (this.chaseTarget.defaultMesh.transform.position - this.robot.tChar.transform.position).normalized;
									normalized.y = 0f;
									normalized = normalized.normalized;
									Debug.DrawRay(this.robot.tChar.transform.position, vector, Color.red);
									Debug.DrawRay(this.robot.tChar.transform.position, normalized, Color.blue);
									bool flag11 = Vector3.Angle(vector, normalized) > this.eyeAngle && this.robot.playerCommon.m_nFightStat == 0;
									if (flag11)
									{
										this.chaseTarget = null;
									}
								}
								bool flag12 = this.chaseTarget != null && this.chaseTarget.defaultMesh != null;
								if (flag12)
								{
									Vector3 normalized2 = (this.chaseTarget.defaultMesh.transform.position - this.robot.tChar.transform.position).normalized;
									bool flag13 = Physics.Raycast(this.robot.tChar.transform.position + Vector3.up * 0.7f, normalized2, Vector3.Distance(this.robot.tChar.transform.position, this.chaseTarget.defaultMesh.transform.position), this.pullLayer2) && this.robot.beMonster != 3;
									if (flag13)
									{
										this.chaseTarget = null;
									}
								}
								bool flag14 = this.chaseTarget != null;
								if (flag14)
								{
									this.robot.heroBase.SetRobotState(true);
								}
								break;
							}
						}
					}
				}
			}
			IL_4F7:;
		}
		this.goRoot.Clear();
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x0013728C File Offset: 0x0013548C
	public virtual void Fire()
	{
		bool flag = !(this.robot.wc != null) || this.skillIng;
		if (!flag)
		{
			bool flag2 = false;
			Vector3 normalized = (this.robot.playerTarget.transform.position - this.robot.tChar.transform.position).normalized;
			normalized.y = 0f;
			normalized = normalized.normalized;
			Vector3 normalized2 = (this.chaseTarget.UserControl.transform.position - this.robot.tChar.transform.position).normalized;
			normalized2.y = 0f;
			normalized2 = normalized2.normalized;
			bool flag3 = this.AttackRange < 6f;
			if (flag3)
			{
				flag2 = true;
			}
			else
			{
				bool flag4 = Vector3.Angle(normalized, normalized2) < 10f;
				if (flag4)
				{
					flag2 = true;
				}
			}
			bool flag5 = !flag2;
			if (!flag5)
			{
				int num = ((CWeaponControlMe)this.robot.wc).WeaponFire(this.weaponFireName, "0", this.chaseTarget.defaultMesh.transform.position, this.chaseTarget.defaultMesh);
				bool flag6 = num == 1;
				if (flag6)
				{
					this.bulletEmptyTimer = 0f;
					this.attack = true;
					this.hitRecoverCD = this.HitRecover;
				}
				else
				{
					bool flag7 = ((CWeaponControlMe)this.robot.wc).bulletNow == 0;
					if (flag7)
					{
						this.bulletEmptyTimer += Time.deltaTime;
						bool flag8 = this.bulletEmptyTimer > 0.5f;
						if (flag8)
						{
							((CWeaponControlMe)this.robot.wc).DoCartridge();
							this.bulletEmptyTimer = 0f;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x00137474 File Offset: 0x00135674
	public virtual void ChangeWeaponGround()
	{
		this.robot.ChangeWeaponGroup(0);
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x00137484 File Offset: 0x00135684
	public virtual bool UseSkill()
	{
		return this.robot.wc != null && !this.skillIng && this.robot.SyncNetObj != null && !this.robot.isDead && !this.hitByShawang && this.robot.needSendToServer && this.robot.playerCommon.m_nInJidi == 0 && fGameManager.Heroplayer.canControl;
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x00137509 File Offset: 0x00135709
	public void setSkillIng(bool b)
	{
		this.skillIng = b;
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x00137514 File Offset: 0x00135714
	public void HitByShawangSuper(Vector3 dir, bool hited)
	{
		bool needSendToServer = this.robot.needSendToServer;
		if (needSendToServer)
		{
			this.hitByShawang = hited;
			this.SyncPos = this.robot.UserControl.transform.position + dir;
			if (hited)
			{
				this.Agent.updatePosition = true;
				base.GetComponent<NavMeshAgent>().nextPosition = this.SyncPos;
			}
			this.SyncPosition();
		}
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x00137588 File Offset: 0x00135788
	public void MoveAsDash(Vector3 dir)
	{
		bool flag = this.robot.needSendToServer && this.b_moveAsDash;
		if (flag)
		{
			this.SyncTimeStep = 0.1f;
			this.robot.tChar.hangInAir = true;
			this.robot.tChar.m_MoveByAnimator = false;
			this.SyncPos = this.robot.UserControl.transform.position + dir;
			this.Agent.updatePosition = true;
			base.GetComponent<NavMeshAgent>().nextPosition = this.SyncPos;
		}
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x00137620 File Offset: 0x00135820
	public void MoveAsDashFinish()
	{
		this.robot.tChar.hangInAir = false;
		this.robot.tChar.m_MoveByAnimator = true;
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x00137648 File Offset: 0x00135848
	private void CheckStuck()
	{
		bool flag = this.attack;
		if (!flag)
		{
			bool flag2 = Vector3.Distance(this.lastRobotPos, this.robot.tChar.transform.position) < 0.2f;
			if (flag2)
			{
				this.stuckTimer += Time.deltaTime;
			}
			else
			{
				this.cancelSprint = false;
			}
			bool flag3 = this.stuckTimer > 0.3f;
			if (flag3)
			{
				this.cancelSprint = true;
			}
			bool flag4 = this.stuckTimer > 0.6f;
			if (flag4)
			{
				this.stuckTimer = 0f;
				this.SyncPos = this.robot.UserControl.transform.position;
				this.currentStayTimer = 0f;
				bool flag5 = this.chaseTarget != null && !this.TargetInSight;
				if (flag5)
				{
					this.chaseTarget = null;
				}
				bool flag6 = this.chaseTarget == null;
				if (flag6)
				{
					this.StopChaseAndCalPath(false);
				}
			}
		}
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x0013774C File Offset: 0x0013594C
	public void ChangeTarget(NetLiving chase)
	{
		bool flag = this.chaseTarget != null && !this.chaseTarget.bePlayer;
		if (flag)
		{
			this.chaseTarget = chase;
		}
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x00137780 File Offset: 0x00135980
	private void UpdateClosestEnemyTarget()
	{
		bool flag = this.targetPos != null && this.targetPos.Count > 0 && this.RobotHasPath;
		if (!flag)
		{
			bool flag2 = this.chaseTarget != null;
			if (!flag2)
			{
				this.updateEnemyTargetTimer -= Time.deltaTime;
				bool flag3 = this.updateEnemyTargetTimer <= 0f;
				if (flag3)
				{
					this.updateEnemyTargetTimer = 1f;
					bool flag4 = this.layer == 0 && this.robot != null && this.robot.playerCommon != null;
					if (flag4)
					{
						bool flag5 = this.robot.playerCommon.m_nShili == 1;
						if (flag5)
						{
							this.layer = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("force2_mainplayer"));
						}
						else
						{
							this.layer = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force1_mainplayer"));
						}
					}
					Vector3 vector = Vector3.zero;
					float num = float.MaxValue;
					Collider[] array = Physics.OverlapSphere(base.transform.position, 1000f, this.layer);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag6 = (array[i].gameObject.layer == LayerMask.NameToLayer("force2_npc") || array[i].gameObject.layer == LayerMask.NameToLayer("force1_npc")) && array[i].gameObject.tag.CompareTo("can_damage_robot") != 0;
						if (!flag6)
						{
							GameObject gameObject = array[i].gameObject.transform.root.gameObject;
							PublicInfo component = gameObject.GetComponent<PublicInfo>();
							bool flag7 = component != null && component.NetObject != null && !component.NetObject.isDead;
							if (flag7)
							{
								float num2 = Vector3.Distance(base.transform.position, gameObject.transform.position);
								bool flag8 = num2 < num;
								if (flag8)
								{
									num = num2;
									vector = gameObject.transform.position;
								}
							}
						}
					}
					bool flag9 = vector != Vector3.zero;
					if (flag9)
					{
						this.currentTargetPos = vector;
						bool flag10 = this.Agent != null && this.Agent.enabled;
						if (flag10)
						{
							this.Agent.SetDestination(this.currentTargetPos);
						}
					}
				}
			}
		}
	}

	// Token: 0x04001E2C RID: 7724
	private float SyncTimeStep;

	// Token: 0x04001E2D RID: 7725
	private RobotAI _robot;

	// Token: 0x04001E2E RID: 7726
	private Vector3 previousPos = Vector3.zero;

	// Token: 0x04001E2F RID: 7727
	private List<Vector3> targetPos;

	// Token: 0x04001E30 RID: 7728
	private List<float> stayAreas;

	// Token: 0x04001E31 RID: 7729
	private List<float> stayTimes;

	// Token: 0x04001E32 RID: 7730
	private List<bool> b_isTranes;

	// Token: 0x04001E33 RID: 7731
	private Vector3 _currentTargetPos;

	// Token: 0x04001E34 RID: 7732
	public float moveSpeed = 1.5f;

	// Token: 0x04001E35 RID: 7733
	private int CTPIndex;

	// Token: 0x04001E36 RID: 7734
	private bool b_reachPos;

	// Token: 0x04001E37 RID: 7735
	protected Vector3 SyncPos;

	// Token: 0x04001E38 RID: 7736
	public Vector3 v3OldTarget;

	// Token: 0x04001E39 RID: 7737
	private Vector3 _SyncTarget;

	// Token: 0x04001E3A RID: 7738
	private float fOldAngle;

	// Token: 0x04001E3B RID: 7739
	private Vector3 SyncAngel = Vector3.zero;

	// Token: 0x04001E3C RID: 7740
	public NavMeshAgent Agent;

	// Token: 0x04001E3D RID: 7741
	protected bool moveAttack;

	// Token: 0x04001E3E RID: 7742
	protected int moveType;

	// Token: 0x04001E3F RID: 7743
	protected float followRange;

	// Token: 0x04001E40 RID: 7744
	protected float AttackRange;

	// Token: 0x04001E41 RID: 7745
	protected float eyeRange;

	// Token: 0x04001E42 RID: 7746
	protected float eyeAngle;

	// Token: 0x04001E43 RID: 7747
	protected float safeRange;

	// Token: 0x04001E44 RID: 7748
	protected int weaponFireName;

	// Token: 0x04001E45 RID: 7749
	protected float earRange;

	// Token: 0x04001E46 RID: 7750
	private float findTargetTimer = 0.5f;

	// Token: 0x04001E47 RID: 7751
	private float checkTargetInSightTimer = 0.5f;

	// Token: 0x04001E48 RID: 7752
	public bool attack;

	// Token: 0x04001E49 RID: 7753
	protected bool skillIng;

	// Token: 0x04001E4A RID: 7754
	protected LayerMask layer;

	// Token: 0x04001E4B RID: 7755
	protected LayerMask pullLayer;

	// Token: 0x04001E4C RID: 7756
	protected LayerMask pullLayer2;

	// Token: 0x04001E4D RID: 7757
	public NetLiving chaseTarget;

	// Token: 0x04001E4E RID: 7758
	private Vector3 lastRobotPos;

	// Token: 0x04001E4F RID: 7759
	public bool cancelSprint;

	// Token: 0x04001E50 RID: 7760
	protected float AttackCD = 2f;

	// Token: 0x04001E51 RID: 7761
	protected float HitRecover = 1f;

	// Token: 0x04001E52 RID: 7762
	protected float hitRecoverCD;

	// Token: 0x04001E53 RID: 7763
	private int heroID;

	// Token: 0x04001E54 RID: 7764
	protected bool hasPath;

	// Token: 0x04001E55 RID: 7765
	protected float CurrentTargetDis;

	// Token: 0x04001E56 RID: 7766
	protected Vector3 lastPos;

	// Token: 0x04001E57 RID: 7767
	protected Vector3 transprotPos = Vector3.zero;

	// Token: 0x04001E58 RID: 7768
	private float turnRotationSpeed;

	// Token: 0x04001E59 RID: 7769
	private float DodgeRate;

	// Token: 0x04001E5A RID: 7770
	private bool gameBegin;

	// Token: 0x04001E5B RID: 7771
	private float ignoreTargetTimer;

	// Token: 0x04001E5C RID: 7772
	private float moveParm;

	// Token: 0x04001E5D RID: 7773
	private Vector3 moveAroundPos;

	// Token: 0x04001E5E RID: 7774
	private int caseNum;

	// Token: 0x04001E5F RID: 7775
	private bool reach = true;

	// Token: 0x04001E60 RID: 7776
	private float stayTimer = 0.1f;

	// Token: 0x04001E61 RID: 7777
	private NavMeshPath path;

	// Token: 0x04001E62 RID: 7778
	private Vector3[] paths;

	// Token: 0x04001E63 RID: 7779
	private bool getPath;

	// Token: 0x04001E64 RID: 7780
	private int PathIndex;

	// Token: 0x04001E65 RID: 7781
	public float currentStayTimer;

	// Token: 0x04001E66 RID: 7782
	protected float currentStayArea;

	// Token: 0x04001E67 RID: 7783
	protected bool needTrans;

	// Token: 0x04001E68 RID: 7784
	private Vector3 PatrollCenterPos = Vector3.zero;

	// Token: 0x04001E69 RID: 7785
	private float calcuteTimer;

	// Token: 0x04001E6A RID: 7786
	private Vector3 Pathdir = Vector3.zero;

	// Token: 0x04001E6B RID: 7787
	private bool _RobotHasPath = true;

	// Token: 0x04001E6C RID: 7788
	private int lastPathIndex;

	// Token: 0x04001E6D RID: 7789
	public bool b_ChaseMove;

	// Token: 0x04001E6E RID: 7790
	private bool stopChasing;

	// Token: 0x04001E6F RID: 7791
	private bool b_MoveAroundAttack;

	// Token: 0x04001E70 RID: 7792
	private bool needMoveBack;

	// Token: 0x04001E71 RID: 7793
	private bool startToMoveNextPos;

	// Token: 0x04001E72 RID: 7794
	private bool canAttack = true;

	// Token: 0x04001E73 RID: 7795
	private float TargetDis;

	// Token: 0x04001E74 RID: 7796
	protected bool TargetInSight;

	// Token: 0x04001E75 RID: 7797
	private List<GameObject> goRoot = new List<GameObject>();

	// Token: 0x04001E76 RID: 7798
	private float bulletEmptyTimer;

	// Token: 0x04001E77 RID: 7799
	private bool hitByShawang;

	// Token: 0x04001E78 RID: 7800
	protected bool b_moveAsDash;

	// Token: 0x04001E79 RID: 7801
	protected bool releasedZR;

	// Token: 0x04001E7A RID: 7802
	private float stuckTimer;

	// Token: 0x04001E7B RID: 7803
	private float updateEnemyTargetTimer = 0f;
}
