using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class Skill_Lilisi_shiledMe : SkillBaseMe
{
	// Token: 0x06002975 RID: 10613 RVA: 0x0016E194 File Offset: 0x0016C394
	public override bool OneSkillCanDo()
	{
		bool flag = !((ISkillDataSkill_Lilisi_shiled)this.ISD).bCanDoSkill;
		return !flag && base.OneSkillCanDo();
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x0016E1C8 File Offset: 0x0016C3C8
	public override void OneSkillInput()
	{
		base.OneSkillInput();
		this.Send_Messages();
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x0016E1D9 File Offset: 0x0016C3D9
	public override void OneSkillActionUpdate(ArrayList list)
	{
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x0016E1DC File Offset: 0x0016C3DC
	private bool IsCreateShiled()
	{
		bool result = false;
		bool flag = this.perfab != null && this.perfabDunSui != null && this.shield_loop != null && this.shield_broken != null;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x0016E232 File Offset: 0x0016C432
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.OnLoadPrefab();
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x0016E244 File Offset: 0x0016C444
	private void OnLoadPrefab()
	{
		string url = string.Format("EffectRes/{0}.ef", "lls_hudun_dandao");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), null);
		}
		else
		{
			this.loadReayPrefab(resWWW, null);
		}
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x0016E294 File Offset: 0x0016C494
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject x = (GameObject)sStyle;
			this.perfab = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			bool flag2 = x == null;
			if (flag2)
			{
				this.OnLoadDunSui();
			}
			this.LoadFlyShieldAudio("skill_lilisi_shield_loop");
			this.LoadFlyShieldAudio("skill_lilisi_shield_broken");
		}
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x0016E310 File Offset: 0x0016C510
	private void OnLoadDunSui()
	{
		string url = string.Format("EffectRes/{0}.ef", "lls_dun_sui");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadDunSuiPrefab), 0);
		}
		else
		{
			this.loadDunSuiPrefab(resWWW, 0);
		}
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x0016E36C File Offset: 0x0016C56C
	private void loadDunSuiPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfabDunSui = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.perfabDunSui.transform.SetParent(this.perfab.transform);
			this.perfabDunSui.transform.localPosition = Vector3.zero;
			this.perfabDunSui.transform.localRotation = Quaternion.identity;
			this.perfabDunSui.name = "lls_dun_sui";
		}
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0016E40C File Offset: 0x0016C60C
	public void LoadFlyShieldAudio(string sName)
	{
		string url = "SoundRes/" + sName + ".sds";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadFlyShieldSoundPrefab), sName);
		}
		else
		{
			this.LoadFlyShieldSoundPrefab(resWWW, sName);
		}
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x0016E460 File Offset: 0x0016C660
	private void LoadFlyShieldSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			string text = sStyle.ToString();
			GameObject gameObject = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.loop = false;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.name = text;
			bool flag2 = text == "skill_lilisi_shield_loop";
			if (flag2)
			{
				component.loop = true;
				this.shield_loop = gameObject;
			}
			else
			{
				this.shield_broken = gameObject;
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, base.hero, 20);
		}
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x0016E524 File Offset: 0x0016C724
	private void OnCreateShiledObj(ArrayList pam)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		bool flag = !this.IsCreateShiled();
		if (!flag)
		{
			int nID = (int)pam[0];
			arrayList = (ArrayList)pam[5];
			Vector3 position = new Vector3((float)((int)arrayList[0]) / 100f, (float)((int)arrayList[1]) / 100f, (float)((int)arrayList[2]) / 100f);
			arrayList2 = (ArrayList)pam[6];
			Quaternion rotation = Quaternion.Euler(new Vector3((float)((int)arrayList2[0]) / 100f, (float)((int)arrayList2[1]) / 100f, (float)((int)arrayList2[2]) / 100f));
			List<GameObject> destroyObjList = ((ISkillDataSkill_Lilisi_shiled)this.ISD).DestroyObjList;
			bool flag2 = destroyObjList.Count > 0;
			GameObject gameObject2;
			FlyShiledSkill flyShiledSkill;
			if (flag2)
			{
				GameObject gameObject = destroyObjList[0];
				((ISkillDataSkill_Lilisi_shiled)this.ISD).DestroyObjList.Remove(gameObject);
				gameObject2 = gameObject;
				bool flag3 = !gameObject2.activeSelf;
				if (flag3)
				{
					gameObject2.SetActive(true);
				}
				flyShiledSkill = gameObject2.GetComponent<FlyShiledSkill>();
				gameObject2.transform.position = position;
				gameObject2.transform.rotation = rotation;
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.perfab, position, rotation);
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.shield_loop, Vector3.zero, Quaternion.identity);
				gameObject3.name = "skill_lilisi_shield_loop";
				gameObject3.transform.SetParent(gameObject2.transform);
				gameObject3.transform.localPosition = Vector3.zero;
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.shield_broken, Vector3.zero, Quaternion.identity);
				gameObject4.name = "skill_lilisi_shield_broken";
				gameObject4.transform.SetParent(gameObject2.transform);
				gameObject4.transform.localPosition = Vector3.zero;
				flyShiledSkill = gameObject2.AddComponent<FlyShiledSkill>();
			}
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.parent = base.hero.UserControl.transform.parent;
			gameObject2.name = pam[0].ToString() + "_dun";
			bool flag4 = flyShiledSkill != null;
			if (flag4)
			{
				flyShiledSkill.OnSetValue(base.hero, pam);
			}
			((ISkillDataSkill_Lilisi_shiled)this.ISD).AddShiledObj(nID, gameObject2);
		}
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x0016E7C8 File Offset: 0x0016C9C8
	public void UpdateFlyShiled(ArrayList szData)
	{
		ArrayList szData2 = new ArrayList();
		int num = (int)szData[0];
		szData2 = (ArrayList)szData[1];
		string sID = num.ToString() + "_dun";
		GameObject gameObject = ((ISkillDataSkill_Lilisi_shiled)this.ISD).FindShiledObj(sID);
		bool flag = gameObject == null;
		if (flag)
		{
			Debug.LogWarning("没有找到ID:" + num.ToString());
		}
		else
		{
			FlyShiledSkill component = gameObject.GetComponent<FlyShiledSkill>();
			bool flag2 = !(component == null);
			if (flag2)
			{
				component.OnUpdateData(szData2);
			}
		}
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x0016E868 File Offset: 0x0016CA68
	private void FixedUpdate()
	{
		bool flag = Time.time - this.fcheck >= 0.1f;
		if (flag)
		{
			this.fcheck = Time.time;
			this.CheckCandoSkill();
		}
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x0016E8A4 File Offset: 0x0016CAA4
	private void CheckCandoSkill()
	{
		bool flag = true;
		Ray ray = new Ray(CameraControl.Instance.transform.position, CameraControl.Instance.transform.forward * this.checkdis);
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit raycastHit;
		bool flag2 = Physics.Raycast(ray, out raycastHit, this.checkdis, layerMask);
		if (flag2)
		{
			flag = false;
		}
		else
		{
			Vector3 vector = CameraControl.Instance.transform.position + CameraControl.Instance.transform.forward * 1.5f;
			((ISkillDataSkill_Lilisi_shiled)this.ISD).veCreartPos = vector;
			Vector3 normalized = (vector - CameraControl.Instance.transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(normalized);
			((ISkillDataSkill_Lilisi_shiled)this.ISD).Quapos = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
		}
		bool flag3 = !this.IsCreateShiled();
		if (flag3)
		{
			flag = false;
		}
		bool flag4 = flag != ((ISkillDataSkill_Lilisi_shiled)this.ISD).bCanDoSkill;
		if (flag4)
		{
			((ISkillDataSkill_Lilisi_shiled)this.ISD).bCanDoSkill = flag;
		}
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x0016E9FC File Offset: 0x0016CBFC
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		base.hero.SetAnimator("SkillOne", 0, true, 1);
		base.hero.wInput.notInput = true;
		base.StartCoroutine(this.waitSkillFinish());
		this.OnCreateShiledObj(parm2);
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x0016EA53 File Offset: 0x0016CC53
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x0016EA62 File Offset: 0x0016CC62
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.bePlayActive = true;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x0016EA8C File Offset: 0x0016CC8C
	private void Send_Messages()
	{
		bool bCanDoSkill = ((ISkillDataSkill_Lilisi_shiled)this.ISD).bCanDoSkill;
		if (bCanDoSkill)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.hero.getNetid());
			arrayList.Add(base.iSkillIndex);
			Vector3 quapos = ((ISkillDataSkill_Lilisi_shiled)this.ISD).Quapos;
			Vector3 veCreartPos = ((ISkillDataSkill_Lilisi_shiled)this.ISD).veCreartPos;
			ArrayList value = new ArrayList
			{
				(int)(veCreartPos.x * 100f),
				(int)(veCreartPos.y * 100f),
				(int)(veCreartPos.z * 100f)
			};
			ArrayList value2 = new ArrayList
			{
				(int)(quapos.x * 100f),
				(int)(quapos.y * 100f),
				(int)(quapos.z * 100f)
			};
			arrayList.Add(value);
			arrayList.Add(value2);
			base.hero.SendSkillParms(arrayList);
		}
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x0016EBD8 File Offset: 0x0016CDD8
	public GameObject getFlyShield(string shieldName)
	{
		GameObject gameObject = ((ISkillDataSkill_Lilisi_shiled)this.ISD).FindShiledObj(shieldName);
		bool flag = gameObject == null;
		GameObject result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Transform transform = gameObject.transform.Find(shieldName);
			bool flag2 = transform == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = transform.gameObject;
			}
		}
		return result;
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x0016EC34 File Offset: 0x0016CE34
	public void SetFilmMat(bool mbool)
	{
		Dictionary<string, GameObject> allShiedObj = ((ISkillDataSkill_Lilisi_shiled)this.ISD).GetAllShiedObj();
		List<GameObject> destroyObjList = ((ISkillDataSkill_Lilisi_shiled)this.ISD).DestroyObjList;
		foreach (string key in allShiedObj.Keys)
		{
			GameObject gameObject = allShiedObj[key];
			bool flag = !(gameObject == null);
			if (flag)
			{
				FlyShiledSkill component = gameObject.GetComponent<FlyShiledSkill>();
				bool flag2 = !(component == null);
				if (flag2)
				{
					component.SetFilmMaterial(mbool);
				}
			}
		}
		int count = destroyObjList.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = destroyObjList[i];
			bool flag3 = !(gameObject == null);
			if (flag3)
			{
				FlyShiledSkill component = gameObject.GetComponent<FlyShiledSkill>();
				bool flag4 = !(component == null);
				if (flag4)
				{
					component.SetFilmMaterial(mbool);
				}
			}
		}
	}

	// Token: 0x0400232F RID: 9007
	private GameObject perfab;

	// Token: 0x04002330 RID: 9008
	private GameObject perfabDunSui;

	// Token: 0x04002331 RID: 9009
	private GameObject shield_loop;

	// Token: 0x04002332 RID: 9010
	private GameObject shield_broken;

	// Token: 0x04002333 RID: 9011
	private float checkdis = 5f;

	// Token: 0x04002334 RID: 9012
	private float fcheck;
}
