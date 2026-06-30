using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class FlyShiledSkill : MonoBehaviour
{
	// Token: 0x06000B42 RID: 2882 RVA: 0x00068DF8 File Offset: 0x00066FF8
	private void Awake()
	{
		this.cor = base.transform.GetComponent<CObjectRender>();
		this.tx = base.transform.Find("tx").gameObject;
		this.tx_hong = base.transform.Find("tx_hong").gameObject;
		this.oBody = base.transform.Find("body").gameObject;
		this.bodyBox = base.transform.Find("body").GetComponent<BoxCollider>();
		this.oBody1 = base.transform.Find("body1").gameObject;
		this.oBody2 = base.transform.Find("body2").gameObject;
		this.allRenders = base.GetComponentsInChildren<Renderer>(true);
		this.trans_Layer = LayerMask.NameToLayer("TransparentFX");
		this.BackNormalLayer = LayerMask.NameToLayer("Default");
		this.npcLayer = (1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc"));
		this.lls_dun_po = base.transform.Find("lls_dun_po").gameObject;
		this.lls_dun_sui = base.transform.Find("lls_dun_sui").gameObject;
		this.flyshiledaud = base.transform.Find("skill_lilisi_shield_loop").GetComponent<AudioSource>();
		this.flyshiledbrokenaud = base.transform.Find("skill_lilisi_shield_broken").GetComponent<AudioSource>();
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00068F78 File Offset: 0x00067178
	public void CheckIfCHangeSeeThoughMat(NetLiving _net)
	{
		bool flag = !(this.cor == null);
		if (flag)
		{
			int num = CFilmManager.Instance.filmForce(_net);
			bool flag2 = num == _net.playerCommon.m_nShiliServer;
			if (flag2)
			{
				this.cor.SetEnemyFriendRenderParticle(false, false);
				this.Nowtx = this.tx;
			}
			else
			{
				this.cor.SetEnemyFriendRenderParticle(true, false);
				this.Nowtx = this.tx_hong;
			}
		}
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00068FF3 File Offset: 0x000671F3
	private void Update()
	{
		this.CheckFlyShiledPos();
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00069000 File Offset: 0x00067200
	private void CheckFlyShiledPos()
	{
		bool flag = this._NetLiving != null && Time.time - this.fcheck >= 0.1f;
		if (flag)
		{
			this.fcheck = Time.time;
			bool flag2 = this.m_nCreatePlayerNetID == fGameManager.Heroplayer.getNetid() && !fGameManager.Heroplayer.delayNet;
			if (flag2)
			{
				ArrayList value = new ArrayList
				{
					(int)(base.transform.position.x * 100f),
					(int)(base.transform.position.y * 100f),
					(int)(base.transform.position.z * 100f)
				};
				ArrayList szData = new ArrayList
				{
					0,
					this.m_nID,
					value
				};
				this.Send_Messages(szData);
			}
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00069118 File Offset: 0x00067318
	public void OnSetValue(NetLiving _Net, ArrayList szData)
	{
		this._NetLiving = _Net;
		this.bAutoDeath = false;
		bool flag = this.OnUpdateData(szData) == 0;
		if (!flag)
		{
			this.m_sObjName = this.m_nID.ToString() + "_dun";
			this.oBody.name = this.m_sObjName;
			this.oBody.tag = "can_damage_dun";
			this.oBody.layer = LayerMask.NameToLayer("shield" + _Net.nForce.ToString());
			this.oBody1.layer = LayerMask.NameToLayer("bullet_" + _Net.nForce.ToString());
			this.oBody2.layer = LayerMask.NameToLayer("bullet_" + _Net.nForce.ToString());
			this.CheckIfCHangeSeeThoughMat(_Net);
			bool flag2 = this.m_nCreatePlayerNetID == fGameManager.Heroplayer.getNetid();
			if (flag2)
			{
				this.obrig = this.oBody2.GetComponent<Rigidbody>();
				bool flag3 = this.obrig == null;
				if (flag3)
				{
					this.obrig = this.oBody2.AddComponent<Rigidbody>();
				}
				this.obrig.useGravity = false;
				this.obrig.isKinematic = true;
				this.flydead = this.oBody2.GetComponent<FlyShiledDead>();
				bool flag4 = this.flydead == null;
				if (flag4)
				{
					this.flydead = this.oBody2.AddComponent<FlyShiledDead>();
				}
				this.flydead.SetData((ISkillDataSkill_Lilisi_shiled)this._NetLiving.skillEffectDict[1163].ISD, this.m_nID, this.m_nCreatePlayerNetID, this);
			}
			this._Rigidbody = base.gameObject.GetComponent<Rigidbody>();
			bool flag5 = this._Rigidbody == null;
			if (flag5)
			{
				this._Rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			this._Rigidbody.useGravity = false;
			bool flag6 = this.flyshiledaud != null;
			if (flag6)
			{
				this.flyshiledaud.Play();
			}
		}
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00069338 File Offset: 0x00067538
	private void FixedUpdate()
	{
		bool flag = this._NetLiving != null && !(this._Rigidbody == null);
		if (flag)
		{
			this._Rigidbody.velocity = base.transform.forward * 5.5f;
			this._Rigidbody.angularVelocity = Vector3.zero;
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00069398 File Offset: 0x00067598
	public int OnUpdateData(ArrayList szData)
	{
		this.m_nID = (int)szData[0];
		this.m_nHP = (int)szData[1];
		this.m_nMaxHP = (int)szData[2];
		this.dCreateTime = DateTime.Parse(szData[3].ToString());
		this.m_nCreatePlayerNetID = (int)szData[4];
		bool flag = this.m_nCreatePlayerNetID != fGameManager.Heroplayer.getNetid();
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szData[5];
			base.transform.position = new Vector3((float)((int)arrayList[0]) / 100f, (float)((int)arrayList[1]) / 100f, (float)((int)arrayList[2]) / 100f);
			ArrayList arrayList2 = (ArrayList)szData[6];
			base.transform.rotation = Quaternion.Euler(new Vector3((float)((int)arrayList2[0]) / 100f, (float)((int)arrayList2[1]) / 100f, (float)((int)arrayList2[2]) / 100f));
		}
		bool flag2 = this.m_nHP < 1;
		int result;
		if (flag2)
		{
			bool flag3 = this.Nowtx != null && this.Nowtx.activeSelf;
			if (flag3)
			{
				this.Nowtx.SetActive(false);
			}
			bool activeSelf = this.lls_dun_po.activeSelf;
			if (activeSelf)
			{
				this.lls_dun_po.SetActive(false);
			}
			bool flag4 = !this.lls_dun_sui.activeSelf;
			if (flag4)
			{
				this.lls_dun_sui.SetActive(true);
			}
			bool flag5 = this.flyshiledaud != null;
			if (flag5)
			{
				this.flyshiledaud.Stop();
			}
			this.m_nCreatePlayerNetID = 0;
			bool flag6 = this.flyshiledbrokenaud != null;
			if (flag6)
			{
				this.flyshiledbrokenaud.Play();
			}
			base.StartCoroutine(this.OnDunSui(this.m_nID));
			result = 0;
		}
		else
		{
			bool flag7 = this.m_nHP <= this.m_nMaxHP / 2;
			if (flag7)
			{
				bool flag8 = !this.lls_dun_po.activeSelf;
				if (flag8)
				{
					this.lls_dun_po.SetActive(true);
				}
			}
			else
			{
				bool activeSelf2 = this.lls_dun_po.activeSelf;
				if (activeSelf2)
				{
					this.lls_dun_po.SetActive(false);
				}
			}
			bool activeSelf3 = this.lls_dun_sui.activeSelf;
			if (activeSelf3)
			{
				this.lls_dun_sui.SetActive(false);
			}
			bool flag9 = this.Nowtx != null && !this.Nowtx.activeSelf;
			if (flag9)
			{
				this.Nowtx.SetActive(true);
			}
			result = 1;
		}
		return result;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00069678 File Offset: 0x00067878
	private IEnumerator OnDunSui(int m_nID)
	{
		yield return new WaitForSeconds(2f);
		((ISkillDataSkill_Lilisi_shiled)this._NetLiving.skillEffectDict[1163].ISD).DestroyFlyShiled(this.m_sObjName);
		yield break;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00069690 File Offset: 0x00067890
	public void OnInit()
	{
		bool flag = this.flyshiledaud != null;
		if (flag)
		{
			this.flyshiledaud.Stop();
		}
		bool flag2 = this.flyshiledbrokenaud != null;
		if (flag2)
		{
			this.flyshiledbrokenaud.Stop();
		}
		this.m_nID = 0;
		this.m_nHP = 0;
		this.m_nMaxHP = 0;
		this.dCreateTime = DateTime.MinValue;
		this.m_nCreatePlayerNetID = 0;
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		this._NetLiving.Clear_Adhere_Npc(this.m_sObjName);
		base.StopCoroutine("OnDunSui");
		bool flag3 = this.Nowtx != null && !this.Nowtx.activeSelf;
		if (flag3)
		{
			this.Nowtx.SetActive(true);
			this.Nowtx = null;
		}
		bool activeSelf = this.lls_dun_po.activeSelf;
		if (activeSelf)
		{
			this.lls_dun_po.SetActive(false);
		}
		bool activeSelf2 = this.lls_dun_sui.activeSelf;
		if (activeSelf2)
		{
			this.lls_dun_sui.SetActive(false);
		}
		this.SetFilmMaterial(false);
		bool flag4 = this.flydead != null;
		if (flag4)
		{
			this.flydead.bCheck = false;
		}
		bool flag5 = this._Rigidbody != null;
		if (flag5)
		{
			UnityEngine.Object.Destroy(this._Rigidbody);
		}
		this.bAutoDeath = false;
		this._NetLiving = null;
		this.flydead = null;
		this.obrig = null;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0006981C File Offset: 0x00067A1C
	public void Send_Messages(ArrayList szData)
	{
		this._NetLiving.SendMessageToServer(2210002, szData);
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00069834 File Offset: 0x00067A34
	private void OnTriggerEnter(Collider collision)
	{
		bool flag = this._NetLiving == null || this.m_nCreatePlayerNetID != fGameManager.Heroplayer.getNetid() || !collision.tag.Contains("can_damage_") || collision.tag.Contains("can_damage_dun");
		if (!flag)
		{
			int num = 1 << collision.gameObject.layer;
			bool flag2 = (num & this.npcLayer) != 0;
			NetLiving netLiving;
			if (flag2)
			{
				Npc_Data component = collision.gameObject.GetComponent<Npc_Data>();
				netLiving = component.Entity_Component;
			}
			else
			{
				PublicInfo component2 = collision.transform.root.gameObject.GetComponent<PublicInfo>();
				bool flag3 = component2 == null;
				if (flag3)
				{
					Debug.LogError("莉莉丝飞盾检测碰到玩家时没有PublicINfo, " + collision.name);
					return;
				}
				netLiving = component2.NetObject;
			}
			bool flag4 = netLiving != null && (Fun.GetServerDate() - netLiving.FlyShileldBuffTime).TotalSeconds >= 1.0;
			if (flag4)
			{
				netLiving.FlyShileldBuffTime = Fun.GetServerDate();
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this._NetLiving.getNetid());
				arrayList.Add(netLiving.getNetid());
				arrayList.Add(1048);
				arrayList.Add(0);
				this._NetLiving.SendMessageToServer(41001, arrayList);
			}
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x000699B4 File Offset: 0x00067BB4
	public void SetFilmMaterial(bool active)
	{
		int backNormalLayer = this.BackNormalLayer;
		if (active)
		{
			backNormalLayer = this.trans_Layer;
		}
		for (int i = 0; i < this.allRenders.Length; i++)
		{
			bool flag = !(this.allRenders[i] == null);
			if (flag)
			{
				this.allRenders[i].gameObject.layer = backNormalLayer;
			}
		}
	}

	// Token: 0x04000989 RID: 2441
	private int m_nID;

	// Token: 0x0400098A RID: 2442
	private int m_nHP;

	// Token: 0x0400098B RID: 2443
	private int m_nMaxHP;

	// Token: 0x0400098C RID: 2444
	private string m_sObjName = string.Empty;

	// Token: 0x0400098D RID: 2445
	private DateTime dCreateTime = DateTime.MinValue;

	// Token: 0x0400098E RID: 2446
	private int m_nCreatePlayerNetID;

	// Token: 0x0400098F RID: 2447
	private SphereCollider Spherecolls;

	// Token: 0x04000990 RID: 2448
	private BoxCollider boxCollider;

	// Token: 0x04000991 RID: 2449
	private Rigidbody _Rigidbody;

	// Token: 0x04000992 RID: 2450
	private NetLiving _NetLiving;

	// Token: 0x04000993 RID: 2451
	private bool bAutoDeath;

	// Token: 0x04000994 RID: 2452
	private GameObject oBody;

	// Token: 0x04000995 RID: 2453
	private BoxCollider bodyBox;

	// Token: 0x04000996 RID: 2454
	private GameObject oBody1;

	// Token: 0x04000997 RID: 2455
	private GameObject oBody2;

	// Token: 0x04000998 RID: 2456
	private GameObject lls_dun_po;

	// Token: 0x04000999 RID: 2457
	private GameObject lls_dun_sui;

	// Token: 0x0400099A RID: 2458
	private GameObject tx;

	// Token: 0x0400099B RID: 2459
	private GameObject tx_hong;

	// Token: 0x0400099C RID: 2460
	private FlyShiledDead flydead;

	// Token: 0x0400099D RID: 2461
	private Rigidbody obrig;

	// Token: 0x0400099E RID: 2462
	private AudioSource flyshiledaud;

	// Token: 0x0400099F RID: 2463
	private AudioSource flyshiledbrokenaud;

	// Token: 0x040009A0 RID: 2464
	private Renderer[] allRenders;

	// Token: 0x040009A1 RID: 2465
	private int trans_Layer;

	// Token: 0x040009A2 RID: 2466
	private int BackNormalLayer;

	// Token: 0x040009A3 RID: 2467
	private string layerName;

	// Token: 0x040009A4 RID: 2468
	private int npcLayer;

	// Token: 0x040009A5 RID: 2469
	private CObjectRender cor;

	// Token: 0x040009A6 RID: 2470
	private GameObject Nowtx;

	// Token: 0x040009A7 RID: 2471
	private float fcheck;
}
