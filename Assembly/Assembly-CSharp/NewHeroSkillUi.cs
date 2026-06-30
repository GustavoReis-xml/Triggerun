using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000259 RID: 601
public class NewHeroSkillUi : Window
{
	// Token: 0x06001B5B RID: 7003 RVA: 0x00102438 File Offset: 0x00100638
	protected override void Awake()
	{
		base.Awake();
		this.guninstance = base.transform.Find("guninstance").gameObject;
		this.guntag = this.guninstance.transform.Find("guntag").gameObject;
		this.gunpic = this.guninstance.transform.Find("gunpic").GetComponent<RawImage>();
		this.obulletUI = this.guninstance.transform.Find("bullet").gameObject;
		this.nownum = this.obulletUI.transform.Find("lnum").GetComponent<Text>();
		this.maxnum = this.obulletUI.transform.Find("rnum").GetComponent<Text>();
		this.maxbullet = this.guninstance.transform.Find("maxbullet").gameObject;
		this.guninstance.SetActive(false);
		this.cloneskill = base.transform.Find("cloneskill").gameObject;
		this.skillparent = base.transform.Find("skillparent").gameObject;
		this.skillparent.SetActive(false);
		this.GunInit(0);
		NewHeroSkillUi.Inst = this;
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x00102588 File Offset: 0x00100788
	public void CheckUiActSelf()
	{
		bool flag = this._NetLiving == null || (this._NetLiving != null && this._NetLiving.isDead) || roleuiManager.GetInstance().bOtherUiVisable || gameuiCon.inst.mYukiOnna || (fGameManager.IsSingle && PlayerLearnManager.GetInstance().HideSkillUI) || (gameuiCon.inst.Net != null && gameuiCon.inst.Net.netobject_type == 2);
		if (flag)
		{
			bool activeSelf = this.guninstance.activeSelf;
			if (activeSelf)
			{
				this.guninstance.SetActive(false);
			}
			bool activeSelf2 = this.skillparent.activeSelf;
			if (activeSelf2)
			{
				this.skillparent.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.guninstance.activeSelf;
			if (flag2)
			{
				this.guninstance.SetActive(true);
			}
			bool flag3 = !this.skillparent.activeSelf;
			if (flag3)
			{
				this.skillparent.SetActive(true);
			}
		}
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x0010268C File Offset: 0x0010088C
	private void Update()
	{
		this.CheckUiActSelf();
		this.CheckShow();
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x001026A0 File Offset: 0x001008A0
	private void CheckShow()
	{
		float time = Time.time;
		bool flag = time < this.cdCheckTimeOut;
		if (!flag)
		{
			this.cdCheckTimeOut = time + 0.1f;
			bool flag2 = this._NetLiving == null || this._NetLiving.wc == null;
			if (!flag2)
			{
				bool flag3 = this._NetLiving != null;
				if (flag3)
				{
					int iconWeapon = this._NetLiving.wc.iconWeapon;
					bool flag4 = this.nGunXingTai != iconWeapon;
					if (flag4)
					{
						this.nGunXingTai = iconWeapon;
						this.showGunUi();
						return;
					}
				}
				this.showBullet();
			}
		}
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x00102744 File Offset: 0x00100944
	public void GunInit(int nType)
	{
		this.m_nbulletNow = 0;
		this.nGunXingTai = -1;
		this.guntag.SetActive(false);
		this.obulletUI.SetActive(false);
		this.cloneskill.SetActive(false);
		this.maxbullet.SetActive(false);
		this.guninstance.SetActive(false);
		this.skillparent.SetActive(false);
		this.ClearSkill();
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x001027B8 File Offset: 0x001009B8
	public void OnChangeWeaponGroup(NetLiving _net)
	{
		bool flag = _net != null;
		if (flag)
		{
			this._NetLiving = _net;
			bool flag2 = !(this._NetLiving.wc == null);
			if (flag2)
			{
				this.showGunUi();
			}
		}
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x001027F8 File Offset: 0x001009F8
	public void showGunUi()
	{
		string[] groupIcon = this._NetLiving.wc.groupIcon;
		int iconWeapon = this._NetLiving.wc.iconWeapon;
		string spath = "weaponicon/" + groupIcon[0] + ".txt";
		sysload.loadImage(this.gunpic, spath, null);
		bool flag = groupIcon.Length > 1;
		if (flag)
		{
			this.guntag.transform.Find("tag").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "wuqi_" + (iconWeapon + 1).ToString());
			this.guntag.SetActive(true);
		}
		else
		{
			this.guntag.SetActive(false);
		}
		this.showBullet();
		this.guninstance.SetActive(true);
		this.skillparent.SetActive(true);
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x001028D4 File Offset: 0x00100AD4
	private void showBullet()
	{
		bool flag = this._NetLiving == null || this._NetLiving.wc == null;
		if (!flag)
		{
			int bulletNow = this._NetLiving.wc.bulletNow;
			bool flag2 = this.m_nbulletNow != bulletNow;
			if (flag2)
			{
				this.m_nbulletNow = bulletNow;
				int bulletMax = this._NetLiving.wc.bulletMax;
				bool flag3 = bulletMax == -1;
				if (flag3)
				{
					this.maxbullet.SetActive(true);
					this.obulletUI.SetActive(false);
				}
				else
				{
					this.obulletUI = this.guninstance.transform.Find("bullet").gameObject;
					this.nownum.text = bulletNow.ToString();
					this.maxnum.text = bulletMax.ToString();
					this.obulletUI.SetActive(true);
					this.maxbullet.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x001029CD File Offset: 0x00100BCD
	private void ClearSkill()
	{
		this.dicSkUi.Clear();
		Fun.ClearChildObejct(this.skillparent);
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x001029E8 File Offset: 0x00100BE8
	public void showSkillUi(NetLiving _net)
	{
		bool flag = _net == null;
		if (!flag)
		{
			this._NetLiving = _net;
			bool flag2 = this._NetLiving.heroBase == null;
			if (!flag2)
			{
				this.ClearSkill();
				Dictionary<int, CSpecialSkillStruct> playerSpecialSkills = this._NetLiving.heroBase.playerSpecialSkills;
				ArrayList arrayList = new ArrayList(playerSpecialSkills.Keys);
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					CSpecialSkillStruct cspecialSkillStruct = playerSpecialSkills[(int)arrayList[i]];
					bool flag3 = !(cspecialSkillStruct.icon == string.Empty) && !(cspecialSkillStruct.icon == "0");
					if (flag3)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cloneskill);
						gameObject.transform.SetParent(this.skillparent.transform);
						Fun.Reset(gameObject);
						bool flag4 = !fGameManager.IsSingle;
						if (flag4)
						{
							gameObject.SetActive(true);
						}
						OneSkilUI oneSkilUI = gameObject.AddComponent<OneSkilUI>();
						oneSkilUI.showui((int)arrayList[i], gameObject, 1, cspecialSkillStruct, this._NetLiving);
						this.dicSkUi.Add((int)arrayList[i], oneSkilUI);
					}
				}
				int nWeaponGroup = this._NetLiving.playerCommon.m_nWeaponGroup;
				Dictionary<int, CPSkillStruct> skillsDict = this._NetLiving.heroSkill.skillsDict;
				arrayList = new ArrayList(skillsDict.Keys);
				count = arrayList.Count;
				for (int j = 0; j < count; j++)
				{
					CPSkillStruct cpskillStruct = skillsDict[(int)arrayList[j]];
					bool flag5 = !(cpskillStruct.icon == string.Empty) && !(cpskillStruct.icon == "0") && (cpskillStruct.xingtai == 0 || cpskillStruct.xingtai == nWeaponGroup);
					if (flag5)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.cloneskill);
						gameObject2.transform.SetParent(this.skillparent.transform);
						Fun.Reset(gameObject2);
						bool flag6 = !fGameManager.IsSingle;
						if (flag6)
						{
							gameObject2.SetActive(true);
						}
						OneSkilUI oneSkilUI2 = gameObject2.AddComponent<OneSkilUI>();
						oneSkilUI2.showui((int)arrayList[j], gameObject2, 0, cpskillStruct, this._NetLiving);
						oneSkilUI2.InitSkill();
						this.dicSkUi.Add((int)arrayList[j], oneSkilUI2);
					}
				}
				this.LoadSingleEff();
			}
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x00102C98 File Offset: 0x00100E98
	public void OnSkillKeyShow()
	{
		bool flag = this.dicSkUi.Values.Count <= 0;
		if (!flag)
		{
			foreach (OneSkilUI oneSkilUI in this.dicSkUi.Values)
			{
				bool flag2 = !(oneSkilUI == null);
				if (flag2)
				{
					oneSkilUI.getKJJDesc();
				}
			}
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x00102D24 File Offset: 0x00100F24
	public void InitSkill(int ix)
	{
		bool flag = this.dicSkUi != null && this.dicSkUi.ContainsKey(ix);
		if (flag)
		{
			this.dicSkUi[ix].InitSkill();
		}
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x00102D64 File Offset: 0x00100F64
	private void LoadSingleEff()
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			string url = "UI/learn/ui_tishi.ab";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadMapEffOk), null);
			}
			else
			{
				this.LoadMapEffOk(resWWW, null);
			}
		}
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00102DB8 File Offset: 0x00100FB8
	private void LoadMapEffOk(WWW www, object Param)
	{
		bool flag = www != null && www.error == null && this.dicSkUi.ContainsKey(1031) && this.dicSkUi.ContainsKey(1032);
		if (flag)
		{
			bool flag2 = this.szSingleEffObj.Count >= 3;
			if (flag2)
			{
				Debug.Log("=============不需要重复加载...");
			}
			else
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = this.dicSkUi[1031].transform;
				gameObject.SetActive(false);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.szSingleEffObj.Add(1031, gameObject);
				gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = this.dicSkUi[1032].transform;
				gameObject.SetActive(false);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.szSingleEffObj.Add(1032, gameObject);
				gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = this.guninstance.transform;
				gameObject.SetActive(false);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = new Vector3(-50f, 50f, 0f);
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.szSingleEffObj.Add(4000, gameObject);
			}
		}
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x00102FE0 File Offset: 0x001011E0
	public void SetOneSkillActive(int nID, bool active)
	{
		bool flag = this.dicSkUi.ContainsKey(nID) && !(this.dicSkUi[nID] == null);
		if (flag)
		{
			this.dicSkUi[nID].transform.gameObject.SetActive(active);
		}
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x00103037 File Offset: 0x00101237
	public void AddEffOnSkill(int nID, bool active)
	{
		this.szSingleEffObj[nID].SetActive(active);
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x00103050 File Offset: 0x00101250
	public void CleanSingleEff()
	{
		foreach (GameObject obj in this.szSingleEffObj.Values)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.szSingleEffObj.Clear();
	}

	// Token: 0x040018B3 RID: 6323
	public static NewHeroSkillUi Inst;

	// Token: 0x040018B4 RID: 6324
	private GameObject guninstance;

	// Token: 0x040018B5 RID: 6325
	private GameObject guntag;

	// Token: 0x040018B6 RID: 6326
	private GameObject maxbullet;

	// Token: 0x040018B7 RID: 6327
	private RawImage gunpic;

	// Token: 0x040018B8 RID: 6328
	private GameObject obulletUI;

	// Token: 0x040018B9 RID: 6329
	private Text nownum;

	// Token: 0x040018BA RID: 6330
	private Text maxnum;

	// Token: 0x040018BB RID: 6331
	private GameObject cloneskill;

	// Token: 0x040018BC RID: 6332
	private GameObject skillparent;

	// Token: 0x040018BD RID: 6333
	private NetLiving _NetLiving;

	// Token: 0x040018BE RID: 6334
	private Dictionary<int, GameObject> szSingleEffObj = new Dictionary<int, GameObject>();

	// Token: 0x040018BF RID: 6335
	private int m_nbulletNow;

	// Token: 0x040018C0 RID: 6336
	private Dictionary<int, OneSkilUI> dicSkUi = new Dictionary<int, OneSkilUI>();

	// Token: 0x040018C1 RID: 6337
	private int nGunXingTai = -1;

	// Token: 0x040018C2 RID: 6338
	private float cdCheckTimeOut;
}
