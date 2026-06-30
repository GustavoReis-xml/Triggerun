using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class CScreenTeamCenterOne : CScreenTarget
{
	// Token: 0x0600077E RID: 1918 RVA: 0x00042340 File Offset: 0x00040540
	private void Start()
	{
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00042344 File Offset: 0x00040544
	private void Update()
	{
		bool flag = this.hero == null || !this.hero.playerHaveCreate || this.hero.haveDestroy;
		if (!flag)
		{
			bool flag2 = base.NetTarget != null && !this.hero.isCopy && (Input_Manager.Get_Button_Up("WeaponFirst") || Input_Manager.Get_Button_Up("WeaponSec")) && !Input_Manager.Get_Button("WeaponFirst") && !Input_Manager.Get_Button("WeaponSec");
			if (flag2)
			{
				this.lockTarget = false;
			}
			bool flag3 = Time.time < this.mixTime;
			if (!flag3)
			{
				this.mixTime = Time.time + 0.05f;
				bool flag4 = base.NetTarget != null && base.NetTarget.isDead;
				if (flag4)
				{
					this.lockTarget = false;
				}
				bool lockTarget = this.lockTarget;
				if (lockTarget)
				{
					bool flag5 = base.NetTarget == null || !this.checkWall;
					if (!flag5)
					{
						float maxDistance = Vector3.Distance(this.hero.defaultMesh.transform.position, base.NetTarget.defaultMesh.transform.position);
						Vector3 normalized = (base.NetTarget.defaultMesh.transform.position - this.hero.defaultMesh.transform.position).normalized;
						bool flag6 = Physics.Raycast(this.hero.defaultMesh.transform.position, normalized, maxDistance, this.layer);
						if (flag6)
						{
							bool flag7 = !this.breaking;
							if (flag7)
							{
								this.breaking = true;
								this.beginBreakTime = Time.time + 2f;
							}
							else
							{
								bool flag8 = Time.time > this.beginBreakTime;
								if (flag8)
								{
									this.breaking = false;
									base.NetTarget = null;
									this.lockTarget = false;
								}
							}
						}
						else
						{
							this.breaking = false;
						}
						bool flag9 = base.NetTarget != null && (base.NetTarget.heroInvi || base.NetTarget.haveDestroy);
						if (flag9)
						{
							this.breaking = false;
							base.NetTarget = null;
							this.lockTarget = false;
						}
					}
				}
				else
				{
					bool flag10 = !this.lockTarget;
					if (flag10)
					{
						base.NetTarget = null;
					}
					float num = this.targetAngle;
					Vector3 position = this.hero.FindMubiaoTarget.transform.position;
					GameObject gameObject = this.hero.hitGamaObj[0];
					PublicInfo publicInfo = null;
					bool flag11 = gameObject != null;
					if (flag11)
					{
						publicInfo = gameObject.transform.root.gameObject.GetComponent<PublicInfo>();
					}
					bool flag12 = publicInfo != null && this.hero.nForce == publicInfo.NetObject.nForce && !publicInfo.NetObject.isDead && this.hero.getNetid() != publicInfo.NetObject.getNetid() && !publicInfo.NetObject.heroInvi && (publicInfo.NetObject.bePlayer || (publicInfo.NetObject.netobject_type == 2 && ((Npc_Entity)publicInfo.NetObject).Show_Locked == 1));
					if (flag12)
					{
						float num2 = Vector3.Distance(gameObject.transform.position, position);
						bool flag13 = num2 < this.targetDis;
						if (flag13)
						{
							base.NetTarget = publicInfo.NetObject;
						}
					}
					bool flag14 = base.NetTarget != null;
					if (!flag14)
					{
						Vector3 forward = this.hero.FindMubiaoTarget.transform.forward;
						Dictionary<string, NetObj>.ValueCollection valueCollection = (!this.hero.isCopy) ? GameRoot.SceneSocket.unFsorket.NetObjTable.Values : CFilmManager.Instance.GetFilmNetLivingAll.Values;
						foreach (NetObj netObj in valueCollection)
						{
							NetLiving netLiving = (NetLiving)netObj;
							bool flag15 = netLiving.isDead || (!netLiving.bePlayer && (netLiving.netobject_type != 2 || ((Npc_Entity)netLiving).Show_Locked == 0 || (((Npc_Entity)netLiving).Show_Locked == 2 && this.hero.nForce != ((Npc_Entity)netLiving).nForce) || (((Npc_Entity)netLiving).Show_Locked == 3 && this.hero.nForce == ((Npc_Entity)netLiving).nForce))) || netLiving.thisModel == null || netLiving.getNetid() == this.hero.getNetid() || netLiving.nForce != this.hero.nForce;
							if (!flag15)
							{
								float num3 = Vector3.Distance(netLiving.defaultMesh.transform.position, position);
								bool flag16 = num3 > this.targetDis;
								if (!flag16)
								{
									Vector3 normalized2 = (netLiving.defaultMesh.transform.position - position).normalized;
									bool flag17 = Physics.Raycast(position, normalized2, num3, this.layer);
									if (!flag17)
									{
										float num4 = Vector3.Dot(forward, normalized2);
										bool flag18 = num4 >= num;
										if (flag18)
										{
											num = num4;
											bool flag19 = !netLiving.heroInvi;
											if (flag19)
											{
												base.NetTarget = netLiving;
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

	// Token: 0x04000575 RID: 1397
	private bool checkWall = true;

	// Token: 0x04000576 RID: 1398
	private const float breakTime = 2f;

	// Token: 0x04000577 RID: 1399
	private float beginBreakTime;

	// Token: 0x04000578 RID: 1400
	private bool breaking;

	// Token: 0x04000579 RID: 1401
	private float mixTime;
}
