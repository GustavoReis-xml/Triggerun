using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class CHeroWeapon : MonoBehaviour
{
	// Token: 0x060004E7 RID: 1255 RVA: 0x000282C8 File Offset: 0x000264C8
	public void Awake()
	{
		this.weaponRenders = new Renderer[this.WeaponTransform.Length];
		for (int i = 0; i < this.WeaponTransform.Length; i++)
		{
			this.weaponRenders[i] = this.WeaponTransform[i].GetComponent<Renderer>();
		}
		this.isAwake = true;
		bool flag = this.nModelType != -1;
		if (flag)
		{
			this.SetHeroWeapon(this.nModelType, this.nWeaponId, 0);
		}
		bool flag2 = this.weaponKong == null;
		if (flag2)
		{
			this.weaponKong = base.gameObject;
		}
		string text = string.Format("HeroAnimator/{0}.mo", "dong");
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag3 = resWWW == null;
		if (flag3)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadWeapPartsOk), text);
		}
		else
		{
			this.LoadWeapPartsOk(resWWW, text);
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x000283AC File Offset: 0x000265AC
	private void LoadWeapPartsOk(WWW www, object customParam)
	{
		bool flag = !(this == null) && www != null && www.error == null;
		if (flag)
		{
			this.weapParts = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			this.weapParts.transform.SetParent(this.weaponKong.transform);
			this.weapParts.transform.localPosition = Vector3.zero;
			this.weapParts.transform.localRotation = Quaternion.Euler(new Vector3(-180f, 90f, 180f));
			this.weapParts.SetActive(false);
			bool flag2 = this.szPartsGrid.Count == 0;
			if (flag2)
			{
				this.ShowWeapParts();
			}
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00028487 File Offset: 0x00026687
	public void SetWeaponParts(bool isshow)
	{
		this.isShowParts = isshow;
		this.ShowWeapParts();
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00028498 File Offset: 0x00026698
	public void ShowWeapParts()
	{
		WeaponUnlock weaponUnlock = null;
		ArrayList arrayList = new ArrayList();
		fUIManager.OnClearObj(this.szPartsGrid);
		bool flag = this.weapParts == null || this.nModelType == 1 || this.isShowParts || !base.gameObject.activeSelf;
		if (!flag)
		{
			bool flag2 = this.nWeaponId == -1;
			if (flag2)
			{
				bool flag3 = this.nMode != 1;
				if (flag3)
				{
					weaponUnlock = HeroDataManager.GetInstance().GetUseWeaponData(this.nHeroId, this.nCid);
					arrayList = HeroDataManager.GetInstance().GetWeaponPartsData(this.nHeroId, this.nCid);
				}
			}
			else
			{
				bool flag4 = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId].szWeaponUnlock.ContainsKey(this.nWeaponId);
				if (flag4)
				{
					weaponUnlock = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId].szWeaponUnlock[this.nWeaponId];
					arrayList = (ArrayList)weaponUnlock.szPartsGrid.Clone();
				}
			}
			bool flag5 = weaponUnlock == null || arrayList.Count == 0;
			if (!flag5)
			{
				bool flag6 = false;
				bool flag7 = HeroInfoUICon.inst != null && HeroInfoUICon.inst.visible && HeroInfoUICon.inst.nSlcType == 2;
				if (flag7)
				{
					flag6 = true;
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					ArrayList arrayList2 = (ArrayList)arrayList[i];
					bool flag8 = (int)arrayList2[0] != 0 && (int)arrayList2[1] != 0;
					if (flag8)
					{
						HeroPartsData heroPartsData = fGameManager.mloadConfig.AllHeroPartsData[(int)arrayList2[1]];
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.weapParts);
						gameObject.transform.SetParent(this.weapParts.transform.parent);
						bool flag9 = flag6;
						if (flag9)
						{
							gameObject.transform.localPosition = weaponUnlock.gridPos1st;
							gameObject.transform.localRotation = Quaternion.Euler(weaponUnlock.gridRot1st);
							gameObject.transform.localScale = weaponUnlock.gridScale1st;
						}
						else
						{
							gameObject.transform.localPosition = weaponUnlock.gridPos3rd;
							gameObject.transform.localRotation = Quaternion.Euler(weaponUnlock.gridRot3rd);
							gameObject.transform.localScale = weaponUnlock.gridScale3rd;
						}
						Animation component = gameObject.GetComponent<Animation>();
						component.clip = component.GetClip(heroPartsData.animStr);
						this.szPartsGrid.Add(gameObject);
					}
				}
				int count = this.szPartsGrid.Count;
				bool flag10 = count >= 2;
				if (flag10)
				{
					for (int j = 0; j < this.szPartsGrid.Count; j++)
					{
						base.StartCoroutine(this.PlayPartsAnim(this.szPartsGrid[j], this.partsAnimTime / (float)count * (float)j));
					}
				}
			}
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x000287C0 File Offset: 0x000269C0
	private IEnumerator PlayPartsAnim(GameObject grid, float delay)
	{
		yield return new WaitForSeconds(delay);
		bool flag = grid != null && !grid.activeSelf;
		if (flag)
		{
			grid.SetActive(true);
		}
		yield break;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000287DD File Offset: 0x000269DD
	public void SetHeroData(int nheroid, int ncid)
	{
		this.nHeroId = nheroid;
		this.nCid = ncid;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000287F0 File Offset: 0x000269F0
	public void SetHeroWeapon(int ntype, int nid = -1, int nmode = 0)
	{
		this.nModelType = ntype;
		this.nWeaponId = nid;
		this.nMode = nmode;
		bool flag = !this.isAwake;
		if (flag)
		{
			Debug.LogError(string.Format("这里不应当出,除了就有问题,{0},{1}", this.nHeroId, this.nCid));
		}
		else
		{
			WeaponUnlock weaponUnlock = null;
			bool flag2 = nid == -1;
			if (flag2)
			{
				weaponUnlock = ((this.nMode != 1) ? HeroDataManager.GetInstance().GetUseWeaponData(this.nHeroId, this.nCid) : HeroDataManager.GetInstance().GetUseRobotWeaponData(this.nHeroId, 1));
			}
			else
			{
				bool flag3 = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId].szWeaponUnlock.ContainsKey(nid);
				if (flag3)
				{
					weaponUnlock = fGameManager.mloadConfig.AllHeroUnlock[this.nHeroId].szWeaponUnlock[nid];
				}
			}
			bool flag4 = weaponUnlock == null;
			if (!flag4)
			{
				string text = (ntype != 1) ? weaponUnlock.comMat_3rd : ((GameRoot.inst.Setting.Video_Class.Effect_Level != 0) ? weaponUnlock.highMat_1st : weaponUnlock.lowMat_1st);
				bool flag5 = this.nMode == 2;
				if (flag5)
				{
					text = weaponUnlock.highMat_1st;
				}
				bool flag6 = !string.IsNullOrEmpty(text);
				if (flag6)
				{
					text = "HeroAnimator/" + text + ".mo";
					bool flag7 = this.nMode == 2;
					if (flag7)
					{
						base.gameObject.SetActive(false);
					}
					WWW resWWW = GameRoot.GetResWWW(text);
					bool flag8 = resWWW == null;
					if (flag8)
					{
						GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadWeaponMatOk), text);
					}
					else
					{
						this.LoadWeaponMatOk(resWWW, text);
					}
				}
			}
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000289A8 File Offset: 0x00026BA8
	private void LoadWeaponMatOk(WWW wwwObj, object customParam)
	{
		bool flag = this == null || wwwObj == null;
		if (!flag)
		{
			Material material = wwwObj.assetBundle.LoadAsset<Material>(wwwObj.assetBundle.GetAllAssetNames()[0]);
			for (int i = 0; i < this.weaponRenders.Length; i++)
			{
				bool flag2 = this.weaponRenders[i] != null;
				if (flag2)
				{
					this.weaponRenders[i].material = material;
				}
			}
			bool flag3 = this.nMode == 2;
			if (flag3)
			{
				base.gameObject.SetActive(true);
			}
			this.ShowWeapParts();
		}
	}

	// Token: 0x040003BC RID: 956
	public Transform[] WeaponTransform;

	// Token: 0x040003BD RID: 957
	public GameObject weaponKong;

	// Token: 0x040003BE RID: 958
	private Renderer[] weaponRenders;

	// Token: 0x040003BF RID: 959
	private int nHeroId;

	// Token: 0x040003C0 RID: 960
	private int nCid;

	// Token: 0x040003C1 RID: 961
	private int nModelType = -1;

	// Token: 0x040003C2 RID: 962
	private int nWeaponId = -1;

	// Token: 0x040003C3 RID: 963
	private int nMode;

	// Token: 0x040003C4 RID: 964
	private bool isAwake;

	// Token: 0x040003C5 RID: 965
	private GameObject weapParts;

	// Token: 0x040003C6 RID: 966
	private float partsAnimTime = 10f;

	// Token: 0x040003C7 RID: 967
	private List<GameObject> szPartsGrid = new List<GameObject>();

	// Token: 0x040003C8 RID: 968
	public bool isShowParts;
}
