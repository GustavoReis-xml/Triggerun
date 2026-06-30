using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class HeroDaWeiMe : HeroBaseMe
{
	// Token: 0x0600115B RID: 4443 RVA: 0x000A2C6E File Offset: 0x000A0E6E
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.LoadDoorPrefeb();
		this.canStart = true;
		CameraControl.Instance.CameraRoration(false, 60, Vector3.zero, 0);
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x000A2C9C File Offset: 0x000A0E9C
	private void LoadDoorPrefeb()
	{
		WWW resWWW = GameRoot.GetResWWW("HeroAnimator/dawei_csm.mo");
		bool flag = resWWW == null || resWWW.error != null;
		if (flag)
		{
			GameRoot.LoadAsset("HeroAnimator/dawei_csm.mo", new fResourceManager.DownFinishDelegate(this.loadDoorPrefab), 0);
		}
		else
		{
			this.loadDoorPrefab(resWWW, 0);
		}
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x000A2CFC File Offset: 0x000A0EFC
	private void loadDoorPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && string.IsNullOrEmpty(www.error);
		if (flag)
		{
			this.doorObj = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x000A2D40 File Offset: 0x000A0F40
	private void ShieldSearch()
	{
		this.Shield = base.transform.Find("model_tps/body2_dun").gameObject;
		this.Shield.tag = "can_damage_dun";
		this.Shield.AddComponent<Rigidbody>();
		this.Shield.GetComponent<Rigidbody>().isKinematic = true;
		this.Shield.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000A2DAC File Offset: 0x000A0FAC
	public void ShieldObjShow(bool hide)
	{
		bool flag = this.Shield == null;
		if (flag)
		{
			this.ShieldSearch();
		}
		this.Shield.layer = LayerMask.NameToLayer("shield" + base.hero.nForce.ToString());
		bool flag2 = this.Shield.activeSelf != hide;
		if (flag2)
		{
			this.Shield.SetActive(hide);
		}
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000A2E24 File Offset: 0x000A1024
	private void FixedUpdate()
	{
		bool flag = base.hero.playerDazhao.canDo && Time.time - this.fcheck >= 0.1f;
		if (flag)
		{
			this.fcheck = Time.time;
			this.CheckDaZhao();
		}
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x000A2E78 File Offset: 0x000A1078
	private void CheckDaZhao()
	{
		Ray ray = new Ray(CameraControl.Instance.transform.position, CameraControl.Instance.transform.forward * 1.2f);
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(ray, out raycastHit, 1.2f, layerMask);
		if (flag)
		{
			this.canStart = false;
		}
		else
		{
			this.canStart = true;
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000A2EEC File Offset: 0x000A10EC
	public override void SuperSkillDo(object parm)
	{
		bool flag = !base.hero.GetAnimatorBool("skill_E") && this.canStart;
		if (flag)
		{
			base.SuperSkillDo(parm);
			this.superDoing = true;
			base.hero.SetAnimator("skill_Q", 0, true, 2);
			bool flag2 = this.doorObj == null;
			if (flag2)
			{
				this.LoadDoorPrefeb();
			}
			else
			{
				this.Door = (GameObject)UnityEngine.Object.Instantiate(this.doorObj);
				this.Door.transform.parent = base.transform.parent;
				this.Door.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.Door.transform.localScale = Vector3.one;
				this.Door.transform.localPosition = base.hero.UserControl.transform.position + 1f * base.transform.forward;
			}
			base.StartCoroutine(this.SuperFinish());
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000A3016 File Offset: 0x000A1216
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(1f);
		this.SuperSkillFinish();
		yield return new WaitForSeconds(base.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime - 1f);
		bool flag = this.Door != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.Door);
		}
		yield break;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000A3028 File Offset: 0x000A1228
	public override void SuperSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.SetAnimator("skill_Q", 0, false, 2);
			base.SuperSkillFinish();
			this.superDoing = false;
		}
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000A307C File Offset: 0x000A127C
	protected override void Update()
	{
		base.Update();
		bool flag = !this.InputCheck() || !Input_Manager.Get_Button_Down("superskill");
		if (!flag)
		{
			this.szBlackEnd.Clear();
			this.szBlackEnd.Add(Mathf.RoundToInt(base.hero.UserControl.transform.position.x * 100f));
			this.szBlackEnd.Add(Mathf.RoundToInt(base.hero.UserControl.transform.position.y * 100f + 5f));
			this.szBlackEnd.Add(Mathf.RoundToInt(base.hero.UserControl.transform.position.z * 100f));
			this.szBlackEnd.Add(base.hero.getNetid());
			this.AddHoleCar();
			bool flag2 = base.hero.wInput.notInput || !base.hero.playerDazhao.canDo || base.hero.GetAnimatorBool("skill_E");
			if (!flag2)
			{
				bool flag3 = !this.canStart;
				if (flag3)
				{
					bool flag4 = gameuiCon.inst != null;
					if (flag4)
					{
						gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(3035, new object[0]));
					}
				}
				else
				{
					base.hero.superSkill(this.szBlackEnd);
				}
			}
		}
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000A3220 File Offset: 0x000A1420
	private void AddHoleCar()
	{
		foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
		{
			NetLiving netLiving = (NetLiving)keyValuePair.Value;
			bool flag = netLiving.bePlayer && netLiving.nForce == base.hero.nForce && netLiving.getNetid() != base.hero.getNetid();
			if (flag)
			{
				this.szBlackEnd.Add(netLiving.getNetid());
			}
		}
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000A32DC File Offset: 0x000A14DC
	public override void Revive()
	{
		base.Revive();
		this.ShieldObjShow(false);
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
		this.canStart = true;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x000A3318 File Offset: 0x000A1518
	public override void OnDeath()
	{
		base.OnDeath();
		this.ShieldObjShow(false);
		base.hero.SetAnimator("skill_shift", 0, false, 1);
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000A3343 File Offset: 0x000A1543
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000A334E File Offset: 0x000A154E
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x000A3359 File Offset: 0x000A1559
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		CameraControl.Instance.CameraRoration(false, 60, Vector3.zero, 0);
	}

	// Token: 0x04000DB6 RID: 3510
	public bool superDoing;

	// Token: 0x04000DB7 RID: 3511
	private ArrayList szBlackEnd = new ArrayList();

	// Token: 0x04000DB8 RID: 3512
	private GameObject Shield;

	// Token: 0x04000DB9 RID: 3513
	private GameObject Door;

	// Token: 0x04000DBA RID: 3514
	private UnityEngine.Object doorObj;

	// Token: 0x04000DBB RID: 3515
	public bool canStart;

	// Token: 0x04000DBC RID: 3516
	private float fcheck;
}
