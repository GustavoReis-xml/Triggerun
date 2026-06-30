using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class HeroDaWeiIt : HeroBaseIt
{
	// Token: 0x0600114E RID: 4430 RVA: 0x000A292B File Offset: 0x000A0B2B
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.LoadDoorPrefeb();
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x000A2940 File Offset: 0x000A0B40
	private void ShieldSearch()
	{
		Transform x = base.gameObject.transform.Find("model_tps/body2_dun");
		bool flag = !(x == null);
		if (flag)
		{
			this.Shield = base.transform.Find("model_tps/body2_dun").gameObject;
			this.Shield.tag = "can_damage_dun";
			this.Shield.AddComponent<Rigidbody>();
			this.Shield.GetComponent<Rigidbody>().isKinematic = true;
			this.Shield.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000A29D0 File Offset: 0x000A0BD0
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

	// Token: 0x06001151 RID: 4433 RVA: 0x000A2A30 File Offset: 0x000A0C30
	private void loadDoorPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && string.IsNullOrEmpty(www.error);
		if (flag)
		{
			this.doorObj = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000A2A74 File Offset: 0x000A0C74
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = this.doorObj == null;
		if (flag)
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

	// Token: 0x06001153 RID: 4435 RVA: 0x000A2B5A File Offset: 0x000A0D5A
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(1f);
		this.SuperSkillFinish();
		yield return new WaitForSeconds(4f);
		bool flag = this.Door != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.Door);
		}
		yield break;
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x000A2B6C File Offset: 0x000A0D6C
	public override void SuperSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.SuperSkillFinish();
		}
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000A2BA0 File Offset: 0x000A0DA0
	public void ShieldObjShow(bool hide)
	{
		bool flag = this.Shield == null;
		if (flag)
		{
			this.ShieldSearch();
		}
		bool flag2 = !(this.Shield == null);
		if (flag2)
		{
			this.Shield.layer = LayerMask.NameToLayer("shield" + base.hero.nForce.ToString());
			bool flag3 = this.Shield.activeSelf != hide;
			if (flag3)
			{
				this.Shield.SetActive(hide);
			}
		}
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x000A2C2B File Offset: 0x000A0E2B
	public override void Revive()
	{
		base.Revive();
		this.ShieldObjShow(false);
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000A2C3D File Offset: 0x000A0E3D
	public override void OnDeath()
	{
		base.OnDeath();
		this.ShieldObjShow(false);
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x000A2C4F File Offset: 0x000A0E4F
	public override void Freeze(bool m_bool)
	{
		base.Freeze(m_bool);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x000A2C5A File Offset: 0x000A0E5A
	public override void Stun(bool m_bool)
	{
		base.Stun(m_bool);
	}

	// Token: 0x04000DB3 RID: 3507
	private GameObject Door;

	// Token: 0x04000DB4 RID: 3508
	private GameObject Shield;

	// Token: 0x04000DB5 RID: 3509
	private UnityEngine.Object doorObj;
}
