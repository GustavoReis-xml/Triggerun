using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class Skill_Lilisi_shiledIt : SkillBaseIt
{
	// Token: 0x06002962 RID: 10594 RVA: 0x0016D884 File Offset: 0x0016BA84
	private bool IsCreateShiled()
	{
		bool result = false;
		bool flag = this.perfabIt != null && this.perfabDunSuiIt != null && this.shield_loop != null && this.shield_broken != null;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x0016D8DA File Offset: 0x0016BADA
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.OnLoadPrefab();
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x0016D8EC File Offset: 0x0016BAEC
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

	// Token: 0x06002965 RID: 10597 RVA: 0x0016D93C File Offset: 0x0016BB3C
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)sStyle;
			this.perfabIt = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			bool flag2 = this.perfabDunSuiIt == null;
			if (flag2)
			{
				this.OnLoadDunSui();
			}
			this.LoadFlyShieldAudio("skill_lilisi_shield_loop");
			this.LoadFlyShieldAudio("skill_lilisi_shield_broken");
			this.m_szHeroSpecialData = base.gameplayer.playerCommon.aryHeroSpecialData;
		}
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x0016D9D0 File Offset: 0x0016BBD0
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

	// Token: 0x06002967 RID: 10599 RVA: 0x0016DA24 File Offset: 0x0016BC24
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
		}
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x0016DAD3 File Offset: 0x0016BCD3
	private void Update()
	{
		this.CreartOldFlyShiled();
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x0016DAE0 File Offset: 0x0016BCE0
	private void CreartOldFlyShiled()
	{
		bool flag = !this.IsCreateShiled() || this.m_szHeroSpecialData.Count < 1;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szHeroSpecialData.Clone();
			this.m_szHeroSpecialData.Clear();
			this.m_szHeroSpecialData = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			int count = arrayList.Count;
			for (int i = 0; i < count; i++)
			{
				arrayList2 = (ArrayList)arrayList[i];
				bool flag2 = arrayList2.Count >= 1;
				if (flag2)
				{
					this.OnCreateShiledObj(arrayList2);
				}
			}
		}
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x0016DB88 File Offset: 0x0016BD88
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

	// Token: 0x0600296B RID: 10603 RVA: 0x0016DBE4 File Offset: 0x0016BDE4
	private void loadDunSuiPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfabDunSuiIt = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			this.perfabDunSuiIt.transform.SetParent(this.perfabIt.transform);
			this.perfabDunSuiIt.transform.localPosition = Vector3.zero;
			this.perfabDunSuiIt.transform.localRotation = Quaternion.identity;
			this.perfabDunSuiIt.name = "lls_dun_sui";
		}
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x0016DC84 File Offset: 0x0016BE84
	private void OnCreateShiledObj(ArrayList pam)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		Vector3 zero = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		bool flag = !this.IsCreateShiled();
		if (!flag)
		{
			int nID = (int)pam[0];
			arrayList = (ArrayList)pam[5];
			zero = new Vector3((float)((int)arrayList[0]) / 100f, (float)((int)arrayList[1]) / 100f, (float)((int)arrayList[2]) / 100f);
			arrayList2 = (ArrayList)pam[6];
			rotation = Quaternion.Euler(new Vector3((float)((int)arrayList2[0]) / 100f, (float)((int)arrayList2[1]) / 100f, (float)((int)arrayList2[2]) / 100f));
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
				gameObject2.transform.position = zero;
				gameObject2.transform.rotation = rotation;
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.perfabIt, zero, rotation);
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
			gameObject2.transform.parent = base.gameplayer.UserControl.transform.parent;
			gameObject2.name = nID.ToString() + "_dun";
			bool flag4 = flyShiledSkill != null;
			if (flag4)
			{
				flyShiledSkill.OnSetValue(base.gameplayer, pam);
			}
			((ISkillDataSkill_Lilisi_shiled)this.ISD).AddShiledObj(nID, gameObject2);
		}
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x0016DF30 File Offset: 0x0016C130
	public void UpdateFlyShiled(ArrayList szData)
	{
		ArrayList szData2 = new ArrayList();
		int num = (int)szData[0];
		szData2 = (ArrayList)szData[1];
		string sID = num.ToString() + "_dun";
		GameObject gameObject = ((ISkillDataSkill_Lilisi_shiled)this.ISD).FindShiledObj(sID);
		bool flag = !(gameObject == null);
		if (flag)
		{
			FlyShiledSkill component = gameObject.GetComponent<FlyShiledSkill>();
			bool flag2 = !(component == null);
			if (flag2)
			{
				component.OnUpdateData(szData2);
			}
		}
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x0016DFBA File Offset: 0x0016C1BA
	public override void OneSkillActionUpdate(ArrayList list)
	{
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x0016DFBD File Offset: 0x0016C1BD
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.OnCreateShiledObj(parm2);
		base.gameplayer.SetAnimator("SkillOne", 0, true, 1);
		base.StartCoroutine(this.waitSkillFinish());
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x0016DFF7 File Offset: 0x0016C1F7
	private IEnumerator waitSkillFinish()
	{
		yield return new WaitForSeconds(1f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x0016E006 File Offset: 0x0016C206
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x0016E010 File Offset: 0x0016C210
	public GameObject getShieldCol(string shieldName)
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

	// Token: 0x06002973 RID: 10611 RVA: 0x0016E06C File Offset: 0x0016C26C
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

	// Token: 0x0400232A RID: 9002
	private GameObject perfabIt;

	// Token: 0x0400232B RID: 9003
	private GameObject perfabDunSuiIt;

	// Token: 0x0400232C RID: 9004
	private GameObject shield_loop;

	// Token: 0x0400232D RID: 9005
	private GameObject shield_broken;

	// Token: 0x0400232E RID: 9006
	private ArrayList m_szHeroSpecialData = new ArrayList();
}
