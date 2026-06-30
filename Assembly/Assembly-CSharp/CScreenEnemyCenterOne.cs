using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class CScreenEnemyCenterOne : CScreenTarget
{
	// Token: 0x06000770 RID: 1904 RVA: 0x00041648 File Offset: 0x0003F848
	private void Start()
	{
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0004164C File Offset: 0x0003F84C
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
						bool flag6 = base.NetTarget.heroInvi || base.NetTarget.haveDestroy;
						if (flag6)
						{
							this.breaking = false;
							base.NetTarget = null;
							this.lockTarget = false;
						}
						else
						{
							float maxDistance = Vector3.Distance(this.hero.defaultMesh.transform.position, base.NetTarget.defaultMesh.transform.position);
							Vector3 normalized = (base.NetTarget.defaultMesh.transform.position - this.hero.defaultMesh.transform.position).normalized;
							bool flag7 = Physics.Raycast(this.hero.defaultMesh.transform.position, normalized, maxDistance, this.layer);
							if (flag7)
							{
								bool flag8 = !this.breaking;
								if (flag8)
								{
									this.breaking = true;
									this.beginBreakTime = Time.time + 2f;
								}
								else
								{
									bool flag9 = Time.time > this.beginBreakTime;
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
								this.breaking = false;
							}
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
					bool flag12 = publicInfo != null && this.hero.nForce != publicInfo.NetObject.nForce && this.hero.getNetid() != publicInfo.NetObject.getNetid() && !publicInfo.NetObject.isDead && !publicInfo.NetObject.heroInvi && (publicInfo.NetObject.bePlayer || (publicInfo.NetObject.netobject_type == 2 && ((Npc_Entity)publicInfo.NetObject).Show_Locked == 1));
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
							bool flag15 = netLiving.isDead || (!netLiving.bePlayer && (netLiving.netobject_type != 2 || ((Npc_Entity)netLiving).Show_Locked == 0 || (((Npc_Entity)netLiving).Show_Locked == 2 && this.hero.nForce != ((Npc_Entity)netLiving).nForce) || (((Npc_Entity)netLiving).Show_Locked == 3 && this.hero.nForce == ((Npc_Entity)netLiving).nForce))) || netLiving.thisModel == null || netLiving.getNetid() == this.hero.getNetid() || netLiving.nForce == this.hero.nForce || netLiving.defaultMesh == null;
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

	// Token: 0x04000564 RID: 1380
	private bool checkWall = true;

	// Token: 0x04000565 RID: 1381
	private const float breakTime = 2f;

	// Token: 0x04000566 RID: 1382
	private float beginBreakTime;

	// Token: 0x04000567 RID: 1383
	private bool breaking;

	// Token: 0x04000568 RID: 1384
	private float mixTime;
}
