using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class MapEffectInfo : PublicInfo
{
	// Token: 0x060019F1 RID: 6641 RVA: 0x000F53E3 File Offset: 0x000F35E3
	public override void InitInfo(NetLiving _net)
	{
		base.InitInfo(this.net);
		this.net = (MapEffect)_net;
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x000F5400 File Offset: 0x000F3600
	protected override void Update()
	{
		base.Update();
		bool flag = this.net == null || this.net.thisModel == null || this.net.heroAIObjectForSearch == null || this.net._data.checkTime <= 0f || Time.time - this.timeOut < this.net._data.checkTime;
		if (!flag)
		{
			bool flag2 = this.net._data.checkNum > 0 && this.net.nowCheckNum <= 0;
			if (flag2)
			{
				bool flag3 = Time.time > this.net.nowCd;
				if (flag3)
				{
					this.net.nowCheckNum = this.net._data.checkNum;
				}
			}
			else
			{
				this.timeOut = Time.time;
				bool flag4 = this.net._data.checkNum > 0;
				if (flag4)
				{
					this.net.nowCheckNum--;
					bool flag5 = this.net.nowCheckNum == 0;
					if (flag5)
					{
						this.net.nowCd = this.timeOut + (float)this.net._data.checkCd;
					}
				}
				bool flag6 = this.net._data.sAnimation != "0";
				if (flag6)
				{
					Collider[] array = Physics.OverlapSphere(this.net.thisModel.transform.position, this.net._data.alertRange, this.net.enemyLayer);
					bool flag7 = array.Length != 0 && this.net._data.animationsound != "0";
					if (flag7)
					{
						this.net.loadTriggerSound(1);
					}
				}
				else
				{
					Collider[] array2 = Physics.OverlapSphere(this.net.thisModel.transform.position, this.net._data.radio, this.net.enemyLayer);
					ArrayList arrayList = new ArrayList();
					int num = array2.Length;
					for (int i = 0; i < num; i++)
					{
						Collider collider = array2[i];
						GameObject gameObject = collider.gameObject.transform.root.gameObject;
						PublicInfo component = gameObject.GetComponent<PublicInfo>();
						bool flag8 = component == null;
						if (!flag8)
						{
							string type = component.type;
							string a = type;
							if (a == "HeroPlayer" || a == "GamePlayer")
							{
								PlayerInfo playerInfo = (PlayerInfo)component;
								GamePlayer gamePlayer = (GamePlayer)playerInfo.NetObject;
								bool flag9 = gamePlayer == null || gamePlayer.isDead || arrayList.IndexOf(gameObject.name) != -1;
								if (!flag9)
								{
									arrayList.Add(gameObject.name);
									NetLiving netLiving = gamePlayer;
									bool flag10 = (this.net._data.buffid <= 0 && this.net._data.buffid_remove <= 0) || netLiving == null;
									if (!flag10)
									{
										float num2 = UnityEngine.Random.value * 10000f;
										bool flag11 = num2 >= (float)this.net._data.buffRate;
										if (flag11)
										{
											bool flag12 = this.net._data.buffid > 0;
											if (flag12)
											{
												ArrayList arrayList2 = new ArrayList();
												arrayList2.Add(this.net.getNetid());
												arrayList2.Add(netLiving.getNetid());
												arrayList2.Add(this.net._data.buffid);
												arrayList2.Add(0);
												this.net.heroAIObjectForSearch.TosFun("SendMsgToServer", new object[]
												{
													41001,
													arrayList2
												});
											}
											bool flag13 = this.net._data.buffid_remove > 0;
											if (flag13)
											{
												ArrayList arrayList3 = new ArrayList();
												arrayList3.Add(this.net.getNetid());
												arrayList3.Add(netLiving.getNetid());
												arrayList3.Add(this.net._data.buffid_remove);
												this.net.heroAIObjectForSearch.TosFun("SendMsgToServer", new object[]
												{
													41002,
													arrayList3
												});
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

	// Token: 0x060019F3 RID: 6643 RVA: 0x000F58C0 File Offset: 0x000F3AC0
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = this.net == null || this.net.heroAIObjectForSearch == null || this.net.thisModel == null || this.net._data.triggerCollide != 1 || collision.transform.root.name == this.net.thisModel.transform.root.name || !collision.transform.tag.Contains("can_damage_");
		if (!flag)
		{
			GameObject gameObject = collision.gameObject.transform.root.gameObject;
			bool flag2 = gameObject == this.lastOb && Time.time - this.lastTime < 0.5f;
			if (!flag2)
			{
				this.lastOb = gameObject;
				this.lastTime = Time.time;
				PublicInfo component = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component == null;
				if (!flag3)
				{
					string type = component.type;
					string a = type;
					if (a == "HeroPlayer" || a == "GamePlayer")
					{
						PlayerInfo playerInfo = (PlayerInfo)component;
						GamePlayer gamePlayer = (GamePlayer)playerInfo.NetObject;
						bool flag4 = gamePlayer == null || gamePlayer.isDead || !LayerManager.CanDamage(this.net.nForce, false, gamePlayer.nForce, true);
						if (!flag4)
						{
							NetLiving netLiving = gamePlayer;
							bool flag5 = this.net._data.buffid > 0 && netLiving != null;
							if (flag5)
							{
								float num = UnityEngine.Random.value * 10000f;
								bool flag6 = num >= (float)this.net._data.buffRate;
								if (flag6)
								{
									ArrayList arrayList = new ArrayList();
									arrayList.Add(this.net.getNetid());
									arrayList.Add(netLiving.getNetid());
									arrayList.Add(this.net._data.buffid);
									arrayList.Add(0);
									this.net.heroAIObjectForSearch.TosFun("SendMsgToServer", new object[]
									{
										41001,
										arrayList
									});
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04001487 RID: 5255
	private float timeOut;

	// Token: 0x04001488 RID: 5256
	private MapEffect net;

	// Token: 0x04001489 RID: 5257
	private GameObject lastOb;

	// Token: 0x0400148A RID: 5258
	private float lastTime;
}
