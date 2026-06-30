using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class buffitemCon : buffitemUI
{
	// Token: 0x06000216 RID: 534 RVA: 0x0000DD79 File Offset: 0x0000BF79
	public override void SetUI(Transform tf)
	{
		base.SetUI(tf);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000DD84 File Offset: 0x0000BF84
	public void setData(buffitem _item)
	{
		this.thisItem = _item;
		this.Boxcolls = _item.thisModelParent.GetComponentsInChildren<BoxCollider>();
		this.Spherecolls = _item.thisModelParent.GetComponentsInChildren<SphereCollider>();
		this.hero = fGameManager.GetInstance().getPlayer();
		bool flag = _item.buffinfo.m_nBuffForce == 1;
		if (flag)
		{
			this.layMask = (1 << LayerMask.NameToLayer("force1_mainplayer") | 1 << LayerMask.NameToLayer("force1_player"));
		}
		else
		{
			this.layMask = (1 << LayerMask.NameToLayer("force2_mainplayer") | 1 << LayerMask.NameToLayer("force2_player"));
		}
		bool flag2 = this.thisItem.buffinfo.m_nBType != 1;
		if (!flag2)
		{
			int nMapEffectId = this.thisItem.buffinfo.m_nMapEffectId;
			bool flag3 = fGameManager.mloadConfig.AllMapEffectObj.ContainsKey(nMapEffectId);
			if (flag3)
			{
				MapEffectData mapEffectData = fGameManager.mloadConfig.AllMapEffectObj[nMapEffectId];
				bool flag4 = mapEffectData != null;
				if (flag4)
				{
					this.szJieDuanData = mapEffectData.szJieDuan;
				}
			}
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = this.thisItem.canTrigger > 0 || !collision.transform.tag.Contains("can_damage_");
		if (!flag)
		{
			bool flag2 = this.thisItem.buffinfo.m_nBType == 1;
			if (flag2)
			{
				this.fJiDiCheck = 0f;
			}
			else
			{
				GameObject gameObject = collision.gameObject.transform.root.gameObject;
				PublicInfo component = gameObject.GetComponent<PublicInfo>();
				bool flag3 = component == null;
				if (!flag3)
				{
					string type = component.type;
					bool flag4 = type == null || !(type == "HeroPlayer");
					if (!flag4)
					{
						PlayerInfo playerInfo = (PlayerInfo)component;
						NetLiving netObject = playerInfo.NetObject;
						bool flag5 = netObject != null && !netObject.isDead && (Time.time - netObject.playerCommon.m_fBuffItemTime >= 1f || netObject.playerCommon.m_nBuffItemNetId != this.thisItem.getNetid()) && netObject.playerCommon.GetHpBiLi() != 1f;
						if (flag5)
						{
							NetLiving netLiving = netObject;
							netObject.playerCommon.m_nBuffItemNetId = this.thisItem.getNetid();
							netObject.playerCommon.m_fBuffItemTime = Time.time;
							bool flag6 = netLiving != null;
							if (flag6)
							{
								ArrayList arrayList = new ArrayList();
								arrayList.Add(this.thisItem.getNetid());
								arrayList.Add(netLiving.getNetid());
								arrayList.Add(0);
								netLiving.TosFun("SendMsgToServer", new object[]
								{
									51004,
									arrayList
								});
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000E078 File Offset: 0x0000C278
	private void Update()
	{
		bool flag = Time.time - this.fCheck < 0.1f;
		if (!flag)
		{
			this.fCheck = Time.time;
			bool flag2 = this.thisItem.buffinfo.m_nBType != 0 || !this.thisItem.loadOver || base.Gui == null;
			if (!flag2)
			{
				float num = (float)this.thisItem.canTrigger;
				bool flag3 = num > 0f;
				if (flag3)
				{
					bool activeSelf = this.thisItem._thisModel.activeSelf;
					if (activeSelf)
					{
						this.thisItem._thisModel.SetActive(false);
					}
					bool flag4 = !base.Gui.activeSelf;
					if (flag4)
					{
						base.Gui.SetActive(true);
					}
					base.setImageFill(1f - num / (float)this.thisItem.buffinfo.m_nCD);
					bool flag5 = this.player == null;
					if (flag5)
					{
						this.player = fGameManager.GetInstance().getPlayer();
						base.GuiCanvas.worldCamera = CameraControl.Instance.controlCamera;
					}
					bool flag6 = this.player != null;
					if (flag6)
					{
						Vector3 position = this.player.UserControl.transform.position;
						Vector3 position2 = base.Gui.transform.position;
						Vector3 normalized = (position - position2).normalized;
						Quaternion quaternion = Quaternion.LookRotation(normalized);
						base.Gui.transform.localRotation = Quaternion.Euler(0f, quaternion.eulerAngles.y, 0f);
					}
				}
				else
				{
					bool flag7 = !this.thisItem._thisModel.activeSelf;
					if (flag7)
					{
						this.thisItem._thisModel.SetActive(true);
					}
					bool activeSelf2 = base.Gui.activeSelf;
					if (activeSelf2)
					{
						base.Gui.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000E27C File Offset: 0x0000C47C
	private void CheckJiDi()
	{
		bool flag = this.hero == null || (this.hero.playerCommon.m_nGM > 0 && this.hero.playerCommon.m_szGMTran.Count > 0) || this.thisItem.buffinfo.m_nBType != 1 || Time.time - this.fJiDiCheck < 0.1f;
		if (!flag)
		{
			this.fJiDiCheck = Time.time;
			bool flag2 = this.szJieDuanData.Count > 0;
			if (flag2)
			{
				int nowGameState = fUIManager.GetInstance().GetNowGameState();
				bool flag3 = this.szJieDuanData.IndexOf(-1) == -1 && this.szJieDuanData.IndexOf(nowGameState) == -1;
				if (flag3)
				{
					return;
				}
			}
			ArrayList robotJiDiStat = this.GetRobotJiDiStat();
			bool flag4 = robotJiDiStat.Count >= 1;
			if (flag4)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.thisItem.getNetid());
				arrayList.Add(robotJiDiStat);
				this.hero.TosFun("SendMsgToServer", new object[]
				{
					51006,
					arrayList
				});
			}
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000E3BC File Offset: 0x0000C5BC
	private ArrayList GetRobotJiDiStat()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList boxCollCastList = this.GetBoxCollCastList();
		bool flag = this.hero.nForce == this.thisItem.buffinfo.m_nBuffForce;
		if (flag)
		{
			bool flag2 = boxCollCastList.Contains(this.hero.getNetid());
			if (flag2)
			{
				bool flag3 = this.hero.playerCommon.m_nInJidi != 1;
				if (flag3)
				{
					arrayList.Add(this.hero.getNetid());
					arrayList.Add(1);
				}
			}
			else
			{
				bool flag4 = this.hero.playerCommon.m_nInJidi != 0;
				if (flag4)
				{
					arrayList.Add(this.hero.getNetid());
					arrayList.Add(0);
				}
			}
		}
		bool flag5 = this.thisItem.buffinfo.m_nBuffForce == 2;
		if (flag5)
		{
			int count = this.hero.robotList.Count;
			for (int i = 0; i < count; i++)
			{
				int num = this.hero.robotList[i];
				RobotAI robotAI = (RobotAI)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
				bool flag6 = robotAI == null;
				if (!flag6)
				{
					bool flag7 = boxCollCastList.Contains(num);
					if (flag7)
					{
						bool flag8 = robotAI.playerCommon.m_nInJidi != 1;
						if (flag8)
						{
							arrayList.Add(num);
							arrayList.Add(1);
						}
					}
					else
					{
						bool flag9 = robotAI.playerCommon.m_nInJidi != 0;
						if (flag9)
						{
							arrayList.Add(num);
							arrayList.Add(0);
						}
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000E5AC File Offset: 0x0000C7AC
	private ArrayList GetBoxCollCastList()
	{
		ArrayList arrayList = new ArrayList();
		Vector3 zero = Vector3.zero;
		int num = this.Boxcolls.Length;
		for (int i = 0; i < num; i++)
		{
			BoxCollider boxCollider = this.Boxcolls[i];
			bool flag = !(boxCollider == null);
			if (flag)
			{
				zero = new Vector3(boxCollider.transform.lossyScale.x * boxCollider.size.x * 0.5f, boxCollider.transform.lossyScale.y * boxCollider.size.y * 0.5f, boxCollider.transform.lossyScale.z * boxCollider.size.z * 0.5f);
				RaycastHit[] array = Physics.BoxCastAll(boxCollider.bounds.center, zero, Vector3.up, boxCollider.transform.rotation, 0f, this.layMask);
				bool flag2 = array.Length != 0;
				if (flag2)
				{
					arrayList = this.GetPhysicsList(arrayList, array, boxCollider, null);
				}
			}
		}
		num = this.Spherecolls.Length;
		for (int j = 0; j < num; j++)
		{
			SphereCollider sphereCollider = this.Spherecolls[j];
			bool flag3 = !(sphereCollider == null);
			if (flag3)
			{
				Ray ray = new Ray(sphereCollider.bounds.center, Vector3.up);
				float[] values = new float[]
				{
					sphereCollider.transform.lossyScale.x,
					sphereCollider.transform.lossyScale.y,
					sphereCollider.transform.lossyScale.z
				};
				float radius = Mathf.Max(values) * sphereCollider.radius;
				RaycastHit[] array2 = Physics.SphereCastAll(ray, radius, 0.01f, this.layMask);
				bool flag4 = array2.Length != 0;
				if (flag4)
				{
					arrayList = this.GetPhysicsList(arrayList, array2, null, sphereCollider);
				}
			}
		}
		return arrayList;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000E7BC File Offset: 0x0000C9BC
	private ArrayList GetPhysicsList(ArrayList szHitList, RaycastHit[] hits, BoxCollider coll, SphereCollider spcoll)
	{
		ArrayList arrayList = new ArrayList();
		int num = hits.Length;
		for (int i = 0; i < num; i++)
		{
			bool flag = (coll != null && coll == hits[i].collider) || (spcoll != null && spcoll == hits[i].collider) || !hits[i].collider.tag.Contains("can_damage_") || hits[i].collider.tag.Contains("can_damage_dun");
			if (!flag)
			{
				GameObject gameObject = hits[i].collider.transform.root.gameObject;
				bool flag2 = arrayList.Contains(gameObject);
				if (!flag2)
				{
					arrayList.Add(gameObject);
					PublicInfo component = gameObject.GetComponent<PublicInfo>();
					bool flag3 = !(component == null);
					if (flag3)
					{
						int netid = component.NetObject.getNetid();
						bool flag4 = this.IsNetList(component.NetObject) && !szHitList.Contains(netid);
						if (flag4)
						{
							szHitList.Add(netid);
						}
					}
				}
			}
		}
		return szHitList;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000E910 File Offset: 0x0000CB10
	private bool IsNetList(NetLiving _net)
	{
		bool flag = false;
		bool flag2 = _net == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			int netid = _net.getNetid();
			bool flag3 = _net.getNetid() == this.hero.getNetid();
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = _net.netobject_type == 6 && _net.needSendToServer;
				if (flag4)
				{
					RobotAI robotAI = (RobotAI)_net;
					bool flag5 = robotAI.SyncNetObj != null && robotAI.SyncNetObj.getNetid() == this.hero.getNetid() && this.hero.robotList.Contains(netid);
					if (flag5)
					{
						return true;
					}
				}
				result = flag;
			}
		}
		return result;
	}

	// Token: 0x040001B4 RID: 436
	private buffitem thisItem;

	// Token: 0x040001B5 RID: 437
	private HeroPlayer player;

	// Token: 0x040001B6 RID: 438
	private int layMask = -999;

	// Token: 0x040001B7 RID: 439
	private HeroPlayer hero;

	// Token: 0x040001B8 RID: 440
	private ArrayList szJieDuanData = new ArrayList();

	// Token: 0x040001B9 RID: 441
	private BoxCollider[] Boxcolls;

	// Token: 0x040001BA RID: 442
	private SphereCollider[] Spherecolls;

	// Token: 0x040001BB RID: 443
	private GameObject lastOb;

	// Token: 0x040001BC RID: 444
	private float fCheck;

	// Token: 0x040001BD RID: 445
	private float fJiDiCheck;
}
