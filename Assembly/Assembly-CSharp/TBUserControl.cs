using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class TBUserControl : MonoBehaviour
{
	// Token: 0x06002C10 RID: 11280 RVA: 0x001821A7 File Offset: 0x001803A7
	private void Start()
	{
		this.state.onground = true;
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x001821B8 File Offset: 0x001803B8
	public virtual void InitControl(NetLiving h)
	{
		this.hero = h;
		this.inited = true;
		if (CameraControl.Instance != null && CameraControl.Instance.controlCamera != null)
		{
			this.cam = CameraControl.Instance.controlCamera.transform;
		}
		else if (Camera.main != null)
		{
			this.cam = Camera.main.transform;
		}
		if (this.cam != null)
		{
			this.state.lookPos = base.transform.position + this.cam.forward * 100f;
		}
		else
		{
			this.state.lookPos = base.transform.position + base.transform.forward * 100f;
		}
		this.state.onjump = false;
	}

	// Token: 0x06002C12 RID: 11282 RVA: 0x00182228 File Offset: 0x00180428
	public void SAGet(string field, int fieldType, object parm)
	{
		if (!true)
		{
		}
		object obj;
		switch (fieldType)
		{
		case 0:
			obj = ((int)parm == 1);
			break;
		case 1:
			obj = (float)((int)parm) * 0.01f;
			break;
		case 2:
			obj = (int)parm;
			break;
		default:
			obj = parm;
			break;
		}
		if (!true)
		{
		}
		object obj2 = obj;
		object obj3 = this.state;
		FieldInfo field2 = this.state.GetType().GetField(field);
		field2.SetValue(obj3, obj2);
		this.state = (TBUserControl.State)obj3;
		bool flag = this.state.onjump && field == "onground" && (bool)obj2;
		if (flag)
		{
			this.state.onjump = false;
		}
	}

	// Token: 0x06002C13 RID: 11283 RVA: 0x001822FC File Offset: 0x001804FC
	protected virtual void checkSendSA()
	{
		bool flag = this.stateLast.onjump != this.state.onjump;
		if (flag)
		{
			this.stateLast.onjump = this.state.onjump;
			bool onjump = this.stateLast.onjump;
			if (onjump)
			{
				this.SA(new ArrayList
				{
					new ArrayList
					{
						"onjump",
						0,
						this.stateLast.onjump ? 1 : 0
					},
					0,
					this.hero.loadID
				});
				this.state.onjump = (this.stateLast.onjump = false);
			}
		}
		bool flag2 = this.stateLast.crouch != this.state.crouch;
		if (flag2)
		{
			this.stateLast.crouch = this.state.crouch;
			this.SA(new ArrayList
			{
				new ArrayList
				{
					"crouch",
					0,
					this.stateLast.crouch ? 1 : 0
				},
				0,
				this.hero.loadID
			});
		}
		bool flag3 = this.stateLast.onwall != this.state.onwall;
		if (flag3)
		{
			this.stateLast.onwall = this.state.onwall;
			this.SA(new ArrayList
			{
				new ArrayList
				{
					"onwall",
					2,
					this.stateLast.onwall
				},
				0,
				this.hero.loadID
			});
		}
		bool flag4 = this.stateLast.onground != this.state.onground;
		if (flag4)
		{
			this.stateLast.onground = this.state.onground;
			this.SA(new ArrayList
			{
				new ArrayList
				{
					"onground",
					0,
					this.stateLast.onground ? 1 : 0
				},
				0,
				this.hero.loadID
			});
		}
		bool flag5 = this.stateLast.fly != this.state.fly;
		if (flag5)
		{
			this.stateLast.fly = this.state.fly;
			this.SA(new ArrayList
			{
				new ArrayList
				{
					"fly",
					0,
					this.stateLast.fly ? 1 : 0
				},
				0,
				this.hero.loadID
			});
		}
		bool flag6 = this.stateLast.onair != this.state.onair;
		if (flag6)
		{
			this.stateLast.onair = this.state.onair;
			this.SA(new ArrayList
			{
				new ArrayList
				{
					"onair",
					0,
					this.stateLast.onair ? 1 : 0
				},
				0,
				this.hero.loadID
			});
		}
	}

	// Token: 0x06002C14 RID: 11284 RVA: 0x00182742 File Offset: 0x00180942
	private void dontControl()
	{
		this.state.jump = false;
		this.state.move = Vector3.zero;
		this.state.moveAir = Vector3.zero;
		this.checkSendSA();
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x00182778 File Offset: 0x00180978
	protected virtual void Update()
	{
		if (this.cam == null)
		{
			if (CameraControl.Instance != null && CameraControl.Instance.controlCamera != null)
			{
				this.cam = CameraControl.Instance.controlCamera.transform;
			}
			else if (Camera.main != null)
			{
				this.cam = Camera.main.transform;
			}
		}
		bool flag = this.hero == null || this.hero.isDead;
		if (flag)
		{
			this.dontControl();
		}
		else
		{
			bool flag2 = this.hero.playerCommon.m_nDongjie == 1 || this.hero.playerCommon.m_nHunMi == 1;
			if (flag2)
			{
				this.dontControl();
			}
			else
			{
				bool chatInput = ChatManager.ChatInput;
				bool flag3 = Time.time > this.nextLogTime;
				if (flag3)
				{
					this.nextLogTime = Time.time + 1f;
					Debug.Log(string.Concat(new string[]
					{
						"CTRL_DBG: notMove=",
						this.notMove.ToString(),
						", isPlayerControl=",
						HeroPlayer.isPlayerControl.ToString(),
						", canControl=",
						this.hero.canControl.ToString(),
						", chatInput=",
						chatInput.ToString(),
						", isSleep=",
						this.isSleep.ToString(),
						", CanHuDong=",
						((HeroPlayer)this.hero).CanHuDong.ToString(),
						", Dongjie=",
						this.hero.playerCommon.m_nDongjie.ToString(),
						", HunMi=",
						this.hero.playerCommon.m_nHunMi.ToString(),
						", nHuDongState=",
						this.hero.nHuDongState.ToString()
					}));
				}
				bool flag4 = this.notMove || !HeroPlayer.isPlayerControl || !this.hero.canControl || chatInput || this.isSleep;
				if (flag4)
				{
					this.dontControl();
				}
				else
				{
					bool flag5 = !((HeroPlayer)this.hero).CanHuDong;
					if (flag5)
					{
						this.dontControl();
					}
					else
					{
						this.state.jump = (this.canJump && (int)Input_Manager.Get_Axis_Raw("Jump") > 0);
						float num = Input_Manager.Get_Axis_Raw("Horizontal");
						bool flag6 = num > 0f;
						if (flag6)
						{
							num = (float)Mathf.CeilToInt(num * this.moveAddSpeed);
						}
						else
						{
							bool flag7 = num < 0f;
							if (flag7)
							{
								num = (float)Mathf.FloorToInt(num * this.moveAddSpeed);
							}
						}
						float num2 = Input_Manager.Get_Axis_Raw("Vertical");
						bool flag8 = num2 > 0f;
						if (flag8)
						{
							num2 = (float)Mathf.CeilToInt(num2 * this.moveAddSpeed);
						}
						else
						{
							bool flag9 = num2 < 0f;
							if (flag9)
							{
								num2 = (float)Mathf.FloorToInt(num2 * this.moveAddSpeed);
							}
						}
						float num3 = Input_Manager.Get_Axis("Jump");
						bool flag10 = num3 > 0f;
						if (flag10)
						{
							num3 = (float)Mathf.CeilToInt(num3 * this.moveAddSpeed);
						}
						else
						{
							bool flag11 = num3 < 0f;
							if (flag11)
							{
								num3 = (float)Mathf.FloorToInt(num3 * this.moveAddSpeed);
							}
						}
						num = Mathf.Clamp(num, -1f, 1f);
						num2 = Mathf.Clamp(num2, -1f, 1f);
						num3 = Mathf.Clamp(num3, -1f, 1f);
						bool isSingle = fGameManager.IsSingle;
						if (isSingle)
						{
							bool playerLimit = PlayerLearnManager.GetInstance().GetPlayerLimit(0);
							if (playerLimit)
							{
								num = 0f;
								num2 = 0f;
							}
							bool playerLimit2 = PlayerLearnManager.GetInstance().GetPlayerLimit(4);
							if (playerLimit2)
							{
								num3 = 0f;
							}
						}
						float num4 = Mathf.Abs(num);
						float num5 = Mathf.Abs(num2);
						bool flag12 = num4 == 0f && num5 > 0f;
						if (flag12)
						{
							this.mixInput = 1;
						}
						else
						{
							bool flag13 = num5 == 0f && num4 > 0f;
							if (flag13)
							{
								this.mixInput = 2;
							}
							else
							{
								bool flag14 = num5 > 0f && num4 > 0f;
								if (flag14)
								{
									this.mixInput = 3;
								}
								else
								{
									this.mixInput = 0;
								}
							}
						}
						Vector3 vector = new Vector3(this.cam.forward.x, 0f, this.cam.forward.z);
						Vector3 normalized = vector.normalized;
						bool flag15 = normalized != Vector3.zero;
						if (flag15)
						{
							this.state.move = Quaternion.LookRotation(normalized) * new Vector3(num, 0f, num2);
						}
						this.state.moveAir = (Quaternion.LookRotation(this.cam.forward) * new Vector3(num, num3, num2)).normalized;
						bool flag16 = this.state.move != Vector3.zero || this.state.jump;
						if (flag16)
						{
							bool flag17 = !((HeroPlayer)this.hero).moved;
							if (flag17)
							{
								((HeroPlayer)this.hero).OnCreateModelTime_Log(-2);
							}
							((HeroPlayer)this.hero).moved = true;
							bool flag18 = ((HeroPlayer)this.hero).CheckActiveStat();
							if (flag18)
							{
								this.state.jump = false;
								return;
							}
						}
						this.checkSendSA();
						this.state.lookPos = base.transform.position + this.cam.forward * 100f;
					}
				}
			}
		}
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x00182D08 File Offset: 0x00180F08
	public void SetDict(string ani, int type, object parm)
	{
		bool flag = !this.aniDict.ContainsKey(ani);
		if (flag)
		{
			this.aniDict.Add(ani, parm);
		}
		else
		{
			this.aniDict[ani] = parm;
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x00182D4C File Offset: 0x00180F4C
	public virtual void SetAnimator(string ani, int type, object parm, int sendtype)
	{
		bool flag = !this.aniDict.ContainsKey(ani);
		if (flag)
		{
			this.aniDict.Add(ani, parm);
		}
		else
		{
			this.aniDict[ani] = parm;
		}
		bool isMainPlayer = this.hero.isMainPlayer;
		if (isMainPlayer)
		{
			this.hero.tCharAnimationFps.SetAnimator(ani, type, parm);
		}
		this.hero.tCharAnimationTps.SetAnimator(ani, type, parm);
		switch (type)
		{
		case 0:
			parm = (((bool)parm) ? 1 : 0);
			break;
		case 1:
			parm = (int)Math.Ceiling((double)((float)parm * 100f));
			break;
		case 2:
			break;
		default:
			return;
		}
		this.SA(new ArrayList
		{
			new ArrayList
			{
				ani,
				type,
				parm
			},
			sendtype,
			this.hero.loadID
		});
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x00182E72 File Offset: 0x00181072
	protected virtual void SA(ArrayList parm)
	{
		((HeroPlayer)this.hero).TosFun("SA", new object[]
		{
			parm
		});
	}

	// Token: 0x04002588 RID: 9608
	protected const float disForHeroPlayer = 30f;

	// Token: 0x04002589 RID: 9609
	public int mixInput;

	// Token: 0x0400258A RID: 9610
	public bool notMove;

	// Token: 0x0400258B RID: 9611
	public bool isSleep;

	// Token: 0x0400258C RID: 9612
	public Dictionary<string, object> aniDict = new Dictionary<string, object>();

	// Token: 0x0400258D RID: 9613
	public bool canCrouch = true;

	// Token: 0x0400258E RID: 9614
	public bool canJump = true;

	// Token: 0x0400258F RID: 9615
	public TBUserControl.State state = default(TBUserControl.State);

	// Token: 0x04002590 RID: 9616
	public TBUserControl.State stateLast = default(TBUserControl.State);

	// Token: 0x04002591 RID: 9617
	private float nextLogTime;

	// Token: 0x04002592 RID: 9618
	protected Transform cam;

	// Token: 0x04002593 RID: 9619
	public NetLiving hero;

	// Token: 0x04002594 RID: 9620
	public float moveAddSpeed = 1000f;

	// Token: 0x04002595 RID: 9621
	protected bool inited;

	// Token: 0x02000682 RID: 1666
	public struct State
	{
		// Token: 0x040030CF RID: 12495
		public Vector3 moveAir;

		// Token: 0x040030D0 RID: 12496
		public Vector3 move;

		// Token: 0x040030D1 RID: 12497
		public Vector3 lookPos;

		// Token: 0x040030D2 RID: 12498
		public bool crouch;

		// Token: 0x040030D3 RID: 12499
		public bool jump;

		// Token: 0x040030D4 RID: 12500
		public bool fly;

		// Token: 0x040030D5 RID: 12501
		public int onwall;

		// Token: 0x040030D6 RID: 12502
		public int actionIndex;

		// Token: 0x040030D7 RID: 12503
		public bool onground;

		// Token: 0x040030D8 RID: 12504
		public bool onair;

		// Token: 0x040030D9 RID: 12505
		public bool onjump;
	}
}
