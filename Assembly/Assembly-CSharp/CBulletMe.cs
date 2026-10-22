using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class CBulletMe : CBullet
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600033A RID: 826 RVA: 0x000178B8 File Offset: 0x00015AB8
	public static int bulletGIDGet
	{
		get
		{
			CBulletMe._bulletGIDGet++;
			bool flag = CBulletMe._bulletGIDGet > 99999;
			if (flag)
			{
				CBulletMe._bulletGIDGet = 1;
			}
			return CBulletMe._bulletGIDGet;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x000178F4 File Offset: 0x00015AF4
	public override void initBullet(CMuzzle muzz, CBulletData __data, bool child, int ismaster = 0, int sandanChildNum = 0)
	{
		this.chariotLayer = LayerMask.NameToLayer("Chariot");
		this.npcLayer = (1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
		base.initBullet(muzz, __data, child, ismaster, sandanChildNum);
		base.bulletGID = CBulletMe.bulletGIDGet;
		this.cartID = base.muzzle.launcher.CartridgePublicID;
		this.numberBefore = base.muzzle.launcher.nowBulletNumberBefore;
		base.bulletLayer = LayerManager.getMainPlayerBulletLayer(muzz.launcher.layerType, muzz.launcher.NetObject.nForce);
		base.boomLayer = LayerManager.getMainPlayerBulletLayer(muzz.launcher.damageLayerType, muzz.launcher.NetObject.nForce);
		base.bulletLayerNoneBuild = (base.bulletLayer ^ 1 << LayerMask.NameToLayer("Default"));
	}

	// Token: 0x0600033C RID: 828 RVA: 0x000179E8 File Offset: 0x00015BE8
	protected override void checkDelayBoom()
	{
		bool flag = !base.readyDestory && base.bulletData.bulletBoomDelay > 0f && Time.time > base.bulletCreateTime + this.boomTimeDealy && !base.readyDestory;
		if (flag)
		{
			base.end = true;
			base.readyDestory = true;
			this.boom();
			this.destoryBullet();
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00017A54 File Offset: 0x00015C54
	public void addEffect(int targetId, int weaponname, int skillid)
	{
		bool flag = base.muzzle.launcher.NetObject != null && !base.muzzle.launcher.NetObject.haveDestroy;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.muzzle.launcher.NetObject.getNetid());
			arrayList.Add(targetId);
			arrayList.Add(0);
			arrayList.Add(skillid);
			arrayList.Add(weaponname);
			arrayList.Add(base.muzzle.launcher.launcherNo);
			base.muzzle.launcher.NetObject.SendMessageToServer(41003, arrayList);
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00017B2C File Offset: 0x00015D2C
	public virtual void throughDamage(Collider ob)
	{
		ArrayList arrayList = this.damageGetData(ob.gameObject, true);
		bool flag = arrayList.Count > 0;
		if (flag)
		{
			this.sendDamageToServer(new ArrayList
			{
				arrayList
			});
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00017B70 File Offset: 0x00015D70
	protected override void boom()
	{
		base.boom();
		int target_ID = -1;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		string colliderName = string.Empty;
		Vector3 rotation = Vector3.zero;
		bool flag = base.bulletData.bulletBoomRange > 0f;
		if (flag)
		{
			Vector3 vector = base.damagePoint = base.transform.position;
			bool flag2 = base.muzzle.launcher.forceSource == 0;
			if (flag2)
			{
				base.physicsForceBornPoint = vector;
			}
			else
			{
				bool flag3 = base.muzzle.launcher.forceSource == 1 && (base.hitCollider == null || !base.hitCollider.tag.Contains("can_damage"));
				if (flag3)
				{
					base.physicsForceBornPoint = vector;
				}
			}
			Collider[] array = Physics.OverlapSphere(base.transform.position, base.bulletData.bulletBoomRange, base.boomLayer);
			string text = string.Empty;
			foreach (Collider collider in array)
			{
				text = collider.tag;
				bool flag4 = !text.Contains("can_damage");
				if (!flag4)
				{
					bool bulletBoomBlock = base.bulletData.bulletBoomBlock;
					if (bulletBoomBlock)
					{
						Vector3 normalized = (collider.bounds.center - vector).normalized;
						float maxDistance = Vector3.Distance(vector, collider.bounds.center);
						RaycastHit[] array3 = Physics.RaycastAll(vector, normalized, maxDistance, this._blockLayer);
						bool flag5 = false;
						foreach (RaycastHit raycastHit in array3)
						{
							bool flag6 = !raycastHit.collider.transform.tag.Contains("can_damage");
							if (flag6)
							{
								flag5 = true;
								break;
							}
							bool flag7 = text != "can_damage_dun" && raycastHit.collider.transform.tag.Contains("can_damage_dun");
							if (flag7)
							{
								flag5 = true;
								break;
							}
						}
						bool flag8 = flag5;
						if (flag8)
						{
							goto IL_346;
						}
					}
					bool flag9 = base.bulletData.bulletBoomCullBack > 0f;
					if (flag9)
					{
						Vector3 forward = base.transform.forward;
						Vector3 normalized2 = (collider.transform.position - base.transform.position).normalized;
						float num = 1f - Vector3.Dot(forward, normalized2);
						bool flag10 = base.bulletData.bulletBoomCullBack < num;
						if (flag10)
						{
							goto IL_346;
						}
					}
					base.hitPhysical = Vector3.zero;
					ArrayList arrayList3 = this.damageGetData(collider.gameObject, true);
					bool flag11 = arrayList3.Count > 0;
					if (flag11)
					{
						int num2 = ((int)arrayList3[2] != 2) ? ((int)arrayList3[1]) : ((int)arrayList3[1] - 10000);
						bool flag12 = arrayList2.IndexOf(num2) == -1;
						if (flag12)
						{
							base.showTagEffectBoom(collider);
							arrayList.Add(arrayList3);
							arrayList2.Add(num2);
						}
					}
				}
				IL_346:;
			}
			this.BoomSendMsg();
		}
		else
		{
			base.damagePoint = base.bornPoint;
			RaycastHit raycastHit2;
			bool flag13 = (base.hitGameObject.collider != null || base.hitCollider != null) && base.bulletData.bulletType == 1 && Physics.Linecast(base.transform.position, base.damagePoint, out raycastHit2, this._blockLayer) && raycastHit2.collider.gameObject.layer == 0;
			if (flag13)
			{
				return;
			}
			bool flag14 = base.hitGameObject.collider != null;
			if (flag14)
			{
				bool flag15 = base.hitGameObject.collider.transform.tag.Contains("can_damage");
				if (flag15)
				{
					ArrayList arrayList4 = this.damageGetData(base.hitGameObject.collider.transform.gameObject, false);
					bool flag16 = arrayList4.Count > 0 && arrayList2.IndexOf((int)arrayList4[1]) == -1;
					if (flag16)
					{
						colliderName = base.hitGameObject.collider.name;
						arrayList.Add(arrayList4);
						target_ID = (int)arrayList4[1];
						arrayList2.Add((int)arrayList4[1]);
					}
				}
				else
				{
					bool flag17 = base.hitGameObject.collider.gameObject.layer == this.chariotLayer;
					if (flag17)
					{
						colliderName = base.hitGameObject.collider.name;
						Npc_Data component = base.hitGameObject.collider.gameObject.GetComponent<Npc_Data>();
						bool flag18 = component == null;
						if (flag18)
						{
							Debug.LogError("打到了层Chariot，但是没有info。名称:" + base.hitGameObject.collider.gameObject.name + " - 父对象:" + base.hitGameObject.collider.gameObject.transform.root.gameObject.name);
						}
						target_ID = component.Entity_Component.getNetid();
					}
				}
				bool flag19 = base.muzzle.npcIndex.Length != 0 && base.muzzle.launcher.NetObject.bePlayer;
				if (flag19)
				{
					for (int k = 0; k < base.muzzle.npcIndex.Length; k++)
					{
						rotation = Quaternion.FromToRotation(Vector3.up, base.hitGameObject.normal).eulerAngles;
						((HeroPlayer)base.muzzle.launcher.NetObject).Npc_Handle_Component.Create_Npc(base.muzzle.launcher.NetObject, target_ID, base.muzzle.npcIndex[k], base.hitGameObject.point, rotation, Vector3.one, base.muzzle.launcher.skillIdByWeapon, Vector3.zero, colliderName);
					}
				}
			}
			else
			{
				bool flag20 = base.hitCollider != null;
				if (flag20)
				{
					bool flag21 = base.hitCollider.transform.tag.Contains("can_damage");
					if (flag21)
					{
						ArrayList arrayList5 = this.damageGetData(base.hitCollider.transform.gameObject, false);
						bool flag22 = arrayList5.Count > 0 && arrayList2.IndexOf((int)arrayList5[1]) == -1;
						if (flag22)
						{
							colliderName = base.hitCollider.name;
							arrayList.Add(arrayList5);
							target_ID = (int)arrayList5[1];
							arrayList2.Add((int)arrayList5[1]);
						}
					}
					else
					{
						bool flag23 = base.hitCollider.gameObject.layer == this.chariotLayer;
						if (flag23)
						{
							colliderName = base.hitCollider.name;
							Npc_Data component2 = base.hitCollider.gameObject.GetComponent<Npc_Data>();
							bool flag24 = component2 == null;
							if (flag24)
							{
								Debug.LogError("打到了层Chariot，但是没有info。名称:" + base.hitCollider.gameObject.name + " - 父对象:" + base.hitCollider.transform.root.gameObject.name);
							}
							target_ID = component2.Entity_Component.getNetid();
						}
					}
					bool flag25 = base.muzzle.npcIndex.Length != 0 && base.muzzle.launcher.NetObject.bePlayer;
					if (flag25)
					{
						for (int l = 0; l < base.muzzle.npcIndex.Length; l++)
						{
							rotation = Quaternion.FromToRotation(Vector3.up, base.hitVelocity).eulerAngles;
							base.muzzle.launcher.NetObject.Npc_Handle_Component.Create_Npc(base.muzzle.launcher.NetObject, target_ID, base.muzzle.npcIndex[l], base.transform.position, rotation, Vector3.one, base.muzzle.launcher.skillIdByWeapon, Vector3.zero, colliderName);
						}
					}
				}
				else
				{
					bool flag26 = base.muzzle.npcIndex.Length != 0 && base.muzzle.launcher.NetObject.bePlayer;
					if (flag26)
					{
						Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, base.transform.forward);
						for (int m = 0; m < base.muzzle.npcIndex.Length; m++)
						{
							rotation = quaternion.eulerAngles;
							base.muzzle.launcher.NetObject.Npc_Handle_Component.Create_Npc(base.muzzle.launcher.NetObject, 0, base.muzzle.npcIndex[m], base.transform.position, rotation, Vector3.one, base.muzzle.launcher.skillIdByWeapon, Vector3.zero, string.Empty);
						}
					}
				}
			}
		}
		this.sendDamageToServer(arrayList);
		bool flag27 = base.muzzle != null && base.muzzle.launcher != null && base.muzzle.launcher.NetObject != null && base.muzzle.launcher.NetObject.heroBase != null;
		if (flag27)
		{
			base.muzzle.launcher.NetObject.heroBase.bulletBoom(base.muzzle.launcher.launcherName, base.muzzle.launcher.launcherNo, base.transform.position);
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000185D0 File Offset: 0x000167D0
	private void BoomSendMsg()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.bulletGID);
		arrayList.Add(new ArrayList
		{
			(int)(base.transform.position.x * 100f),
			(int)(base.transform.position.y * 100f),
			(int)(base.transform.position.z * 100f)
		});
		arrayList.Add(this.cartID);
		arrayList.Add(this.numberBefore);
		arrayList.Add(base.bulletOneNo);
		base.muzzle.launcher.SendWeaponMsg(31205, arrayList);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x000186BC File Offset: 0x000168BC
	protected ArrayList damageGetData(GameObject collider, bool isRange)
	{
		ArrayList arrayList = new ArrayList();
		NetLiving netObject = base.muzzle.launcher.NetObject;
		bool flag = netObject == null || netObject.haveDestroy;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			int num = 1 << collider.layer;
			bool flag2 = (num & this.npcLayer) != 0;
			NetLiving netLiving;
			if (flag2)
			{
				Npc_Data component = collider.gameObject.GetComponent<Npc_Data>();
				netLiving = component.Entity_Component;
			}
			else
			{
				GameObject gameObject = collider.transform.root.gameObject;
				PublicInfo component2 = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component2 == null;
				if (flag3)
				{
					Debug.LogError("发现可以攻击的东西没有PublicINfo, " + collider.name + "," + gameObject.name);
					return arrayList;
				}
				netLiving = component2.NetObject;
			}
			bool flag4 = netLiving.OnReviveTime > this.bulletCreateSTime;
			if (flag4)
			{
				result = arrayList;
			}
			else
			{
				bool flag5 = !netLiving.canKill;
				if (flag5)
				{
					result = arrayList;
				}
				else
				{
					bool haveDestroy = netLiving.haveDestroy;
					if (haveDestroy)
					{
						result = arrayList;
					}
					else
					{
						int num2 = 0;
						bool flag6 = !isRange && base.bulletData.bulletType != 8 && collider.name.Contains("Head");
						if (!flag6 && !isRange && base.bulletData.bulletType != 8 && collider.name.Contains("_IgnoreDamge") && netLiving.userHeadTransform != null)
						{
							Vector3 hitPos = (base.hitGameObject.collider != null) ? base.hitGameObject.point : base.transform.position;
							Vector3 headPos = netLiving.userHeadTransform.position;
							if (Mathf.Abs(hitPos.y - headPos.y) <= 0.35f && Vector2.Distance(new Vector2(hitPos.x, hitPos.z), new Vector2(headPos.x, headPos.z)) <= 0.4f)
							{
								flag6 = true;
							}
						}
						if (flag6)
						{
							num2 = 1;
						}
						bool flag7 = collider.tag.Contains("dun");
						if (flag7)
						{
							num2 = 2;
						}
						float num3 = 0f;
						Transform transform = netObject.UserControl.transform;
						Transform transform2 = netLiving.UserControl.transform;
						bool bePlayer = netLiving.bePlayer;
						if (bePlayer)
						{
							Vector3 from = transform.position - transform2.position;
							from.y = 0f;
							Vector3 forward = transform2.forward;
							forward.y = 0f;
							num3 = Vector3.Angle(from, forward);
							bool flag8 = transform.InverseTransformPoint(transform.position).x > 0f;
							if (flag8)
							{
								num3 = 360f - num3;
							}
						}
						float num4;
						if (isRange)
						{
							Vector3 position = collider.transform.position;
							position.y += 1.5f;
							num4 = Vector3.Distance(base.damagePoint, position) * 100f;
						}
						else
						{
							num4 = Vector3.Distance(transform.position, collider.transform.position) * 100f;
						}
						netLiving.forceMax = base.muzzle.damageForce;
						bool flag9 = base.hitPhysical != Vector3.zero;
						if (flag9)
						{
							netLiving.forceV3 = base.hitPhysical;
						}
						else
						{
							bool flag10 = base.muzzle.launcher.forcePlace == 0;
							if (flag10)
							{
								netLiving.forceV3 = (collider.transform.position - base.physicsForceBornPoint).normalized;
							}
							else
							{
								netLiving.forceV3 = (netLiving.userHead - base.physicsForceBornPoint).normalized;
							}
						}
						bool flag11 = netObject != netLiving && netObject.nForce != netLiving.nForce;
						if (flag11)
						{
							this.hadhit = 1;
							bool flag12 = num2 == 1;
							if (flag12)
							{
								this.hadheadboom = 1;
							}
						}
						result = this.getDamageArray(netObject.getNetid(), netLiving.getNetid(), base.bornPoint, collider.transform.position, num2, (int)num4, (int)num3, netLiving.forceV3, netLiving.forceMax, collider.name);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00018A44 File Offset: 0x00016C44
	public ArrayList getDamageArray(int attackId, int targetId, Vector3 attackPos, Vector3 targetPos, int boomHead, int dis, int angle, Vector3 forceV3, float forceMax, string collName)
	{
		return new ArrayList
		{
			attackId,
			targetId,
			boomHead,
			dis,
			new ArrayList
			{
				(int)(attackPos.x * 100f),
				(int)(attackPos.y * 100f),
				(int)(attackPos.z * 100f)
			},
			angle,
			new ArrayList
			{
				(int)(forceV3.x * 100f),
				(int)(forceV3.y * 100f),
				(int)(forceV3.z * 100f)
			},
			(int)(forceMax * 100f),
			collName,
			new ArrayList
			{
				(int)(targetPos.x * 100f),
				(int)(targetPos.y * 100f),
				(int)(targetPos.z * 100f)
			}
		};
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00018BD4 File Offset: 0x00016DD4
	public void sendDamageToServer(ArrayList allDamage)
	{
		bool flag = base.muzzle.launcher.NetObject.loadID == this.netobjectCreateId && (fGameManager.Heroplayer == null || fGameManager.Heroplayer.canControl);
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(allDamage);
			arrayList.Add(base.muzzle.launcher.launcherName);
			arrayList.Add(0);
			arrayList.Add(1);
			arrayList.Add(base.muzzle.launcher.launcherNo);
			arrayList.Add(base.fullTimeShoot);
			arrayList.Add(this.hadheadboom);
			arrayList.Add(this.hadhit);
			arrayList.Add(base.muzzle.launcher.NetObject.getNetid());
			arrayList.Add(this.cartID);
			arrayList.Add(this.numberBefore);
			arrayList.Add(base.bulletOneNo);
			arrayList.Add(base.bulletChildNo);
			arrayList.Add((int)(base.bulletData.bulletBoomRange * 1000f));
			arrayList.Add(base.muzzle.launcher.NetObject.playerCommon.m_nHeroNo);
			base.muzzle.launcher.NetObject.DamageMsgToServer(base.muzzle.launcher.NetObject, new ArrayList
			{
				arrayList
			});
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00018D98 File Offset: 0x00016F98
	protected void SendBulletPosition()
	{
		bool flag = !base.readyDestory;
		if (flag)
		{
			float time = Time.time;
			bool flag2 = time >= this.bulletSendTime;
			if (flag2)
			{
				this.bulletSendTime = time + 0.1f;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(base.muzzle.launcher.launcherName);
				arrayList.Add(base.muzzle.launcher.launcherNo);
				arrayList.Add(base.muzzle.muzzleName);
				arrayList.Add(base.bulletGID);
				arrayList.Add((int)(base.transform.position.x * 100f));
				arrayList.Add((int)(base.transform.position.y * 100f));
				arrayList.Add((int)(base.transform.position.z * 100f));
				base.muzzle.launcher.NetObject.TosFun("SBP", new object[]
				{
					arrayList
				});
			}
		}
	}

	// Token: 0x040002B9 RID: 697
	private int npcLayer;

	// Token: 0x040002BA RID: 698
	private int hadheadboom;

	// Token: 0x040002BB RID: 699
	private int hadhit;

	// Token: 0x040002BC RID: 700
	private static int _bulletGIDGet;

	// Token: 0x040002BD RID: 701
	private int chariotLayer;

	// Token: 0x040002BE RID: 702
	private float bulletSendTime;
}
