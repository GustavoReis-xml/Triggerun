using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class TBUserControlNpc : TBUserControl
{
	// Token: 0x06002C1E RID: 11294 RVA: 0x00183268 File Offset: 0x00181468
	protected override void checkSendSA()
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
					this.hero.getNetid(),
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
				this.hero.getNetid(),
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
				this.hero.getNetid(),
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
				this.hero.getNetid(),
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
				this.hero.getNetid(),
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
				this.hero.getNetid(),
				this.hero.loadID
			});
		}
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x00183738 File Offset: 0x00181938
	protected override void Update()
	{
		bool chatInput = ChatManager.ChatInput;
		bool flag = this.notMove || this.hero.isDead || chatInput;
		if (flag)
		{
			this.state.jump = false;
			this.state.move = Vector3.zero;
			this.checkSendSA();
		}
		else
		{
			this.state.jump = (this.canJump && (int)Input_Manager.Get_Axis_Raw("Jump") > 0);
			this.checkSendSA();
			float num = Input_Manager.Get_Axis_Raw("Horizontal");
			bool flag2 = num > 0f;
			if (flag2)
			{
				num = (float)Mathf.CeilToInt(num * this.moveAddSpeed);
			}
			else
			{
				bool flag3 = num < 0f;
				if (flag3)
				{
					num = (float)Mathf.FloorToInt(num * this.moveAddSpeed);
				}
			}
			float num2 = Input_Manager.Get_Axis_Raw("Vertical");
			bool flag4 = num2 > 0f;
			if (flag4)
			{
				num2 = (float)Mathf.CeilToInt(num2 * this.moveAddSpeed);
			}
			else
			{
				bool flag5 = num2 < 0f;
				if (flag5)
				{
					num2 = (float)Mathf.FloorToInt(num2 * this.moveAddSpeed);
				}
			}
			num = Mathf.Clamp(num, -1f, 1f);
			num2 = Mathf.Clamp(num2, -1f, 1f);
			bool flag6 = this.beAutoForward;
			if (flag6)
			{
				num2 = this.autoForward.z;
			}
			float num3 = Mathf.Abs(num);
			float num4 = Mathf.Abs(num2);
			bool flag7 = num3 == 0f && num4 > 0f;
			if (flag7)
			{
				this.mixInput = 1;
			}
			else
			{
				bool flag8 = num4 == 0f && num3 > 0f;
				if (flag8)
				{
					this.mixInput = 2;
				}
				else
				{
					bool flag9 = num4 > 0f && num3 > 0f;
					if (flag9)
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
			bool flag10 = normalized != Vector3.zero;
			if (flag10)
			{
				this.state.move = Quaternion.LookRotation(normalized) * new Vector3(num, 0f, num2);
			}
			this.state.lookPos = base.transform.position + this.cam.forward * 100f;
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x001839C0 File Offset: 0x00181BC0
	public override void SetAnimator(string ani, int type, object parm, int sendtype = 1)
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
			this.hero.getNetid(),
			this.hero.loadID
		});
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x00183AD8 File Offset: 0x00181CD8
	protected override void SA(ArrayList parm)
	{
		((Npc_Entity)this.hero).Synchronization_Object.TosFun("SA_Npc", new object[]
		{
			parm
		});
	}

	// Token: 0x0400259E RID: 9630
	public Vector3 autoForward;

	// Token: 0x0400259F RID: 9631
	public bool beAutoForward;
}
