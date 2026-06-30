using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000257 RID: 599
public class NewHeroDaZhaoUi : Window
{
	// Token: 0x06001B3F RID: 6975 RVA: 0x00101204 File Offset: 0x000FF404
	protected override void Awake()
	{
		base.Awake();
		this.oDaZhao = base.transform.FindChild("dazhao").gameObject;
		this.gFull = base.transform.FindChild("dazhao/full").gameObject;
		this.gEmpty = base.transform.FindChild("dazhao/empty").gameObject;
		this.Q_imgObj = base.transform.FindChild("dazhao/Q_img").gameObject;
		this.Q_img = base.transform.FindChild("dazhao/Q_img").GetComponent<Image>();
		this.QKeyText = base.transform.FindChild("dazhao/Q_img/key").GetComponent<Text>();
		this.pic = this.gFull.transform.FindChild("pic").GetComponent<RawImage>();
		this.tNum = this.gEmpty.transform.FindChild("num").GetComponent<Text>();
		this.imagefill = this.gEmpty.transform.FindChild("back1").GetComponent<Image>();
		this.dazhaojin = base.transform.FindChild("dazhao/dazhaojin").GetComponent<Image>();
		this.dazhaojin.enabled = false;
		this.dtsusenum = base.transform.Find("dazhao/dtsusenum").gameObject;
		this.dtsusrNumText = this.dtsusenum.transform.Find("num").GetComponent<Text>();
		this.dtsusenum.SetActive(false);
		this.oDaZhao.SetActive(false);
		NewHeroDaZhaoUi.Inst = this;
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x001013A0 File Offset: 0x000FF5A0
	public void Refresh(NetLiving _net)
	{
		bool flag = _net == null;
		if (!flag)
		{
			this._NetLiving = _net;
			bool flag2 = this._NetLiving.playerDazhao.cdazhaomaininfo == null;
			if (flag2)
			{
				bool activeSelf = base.transform.gameObject.activeSelf;
				if (activeSelf)
				{
					base.transform.gameObject.SetActive(false);
				}
			}
			else
			{
				bool flag3 = !base.transform.gameObject.activeSelf;
				if (flag3)
				{
					base.transform.gameObject.SetActive(true);
				}
				this.c_energy_now = 0;
				this.c_energy_max = 0;
				this.bStart = false;
				this.showPic();
				this.OnSuperSkilKey();
				this.LoadSingleEff();
			}
		}
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x0010145C File Offset: 0x000FF65C
	public void OnSuperSkilKey()
	{
		bool flag = this._NetLiving == null;
		if (flag)
		{
			bool activeSelf = this.Q_imgObj.activeSelf;
			if (activeSelf)
			{
				this.Q_imgObj.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.Q_imgObj.activeSelf;
			if (flag2)
			{
				this.Q_imgObj.SetActive(true);
			}
			string key_Skill_ = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Skill_3;
			bool flag3 = !(key_Skill_ == this.sOldKey);
			if (flag3)
			{
				this.sOldKey = key_Skill_;
				string s = GameRoot.inst.Setting.Get_Key_Describe(key_Skill_);
				int num = Encoding.Default.GetBytes(s).Length - 1;
				Vector2 sizeDelta = this.Q_img.GetComponent<RectTransform>().sizeDelta;
				sizeDelta.x = (float)(30 + num * 10);
				this.Q_img.GetComponent<RectTransform>().sizeDelta = sizeDelta;
				bool isSingle = fGameManager.IsSingle;
				if (isSingle)
				{
					this.QKeyText.text = "Q";
				}
				else
				{
					this.QKeyText.text = GameRoot.inst.Setting.Get_Key_Describe(key_Skill_);
				}
				this.QKeyText.GetComponent<RectTransform>().sizeDelta = sizeDelta;
			}
		}
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x001015B4 File Offset: 0x000FF7B4
	private void showPic()
	{
		string spath = "skillicon/" + this._NetLiving.playerDazhao.cdazhaomaininfo.dazhaoxml.dicon + ".txt";
		sysload.loadImage(this.pic, spath, null);
		this.imagefill.fillAmount = 0f;
		this.tNum.text = "0";
		bool flag = !this.gEmpty.activeSelf;
		if (flag)
		{
			this.gEmpty.SetActive(true);
		}
		bool activeSelf = this.gFull.activeSelf;
		if (activeSelf)
		{
			this.gFull.SetActive(false);
		}
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x0010165C File Offset: 0x000FF85C
	private void OnCheckEnergy()
	{
		bool flag = this._NetLiving == null || this._NetLiving.playerDazhao == null || this._NetLiving.playerDazhao.cdazhaomaininfo == null || this.oDaZhao == null;
		if (!flag)
		{
			bool flag2 = this.c_energy_max < 1;
			if (flag2)
			{
				this.c_energy_max = this._NetLiving.playerDazhao.cdazhaomaininfo.dazhaoxml.denergymax;
			}
			bool flag3 = this.c_energy_max < 1;
			if (!flag3)
			{
				int num = this._NetLiving.playerDazhao.cdazhaomaininfo.c_energy_now;
				bool flag4 = num >= this.c_energy_max && this.c_energy_now == this.c_energy_max;
				if (flag4)
				{
					bool flag5 = !this.gFull.activeSelf || this.tNum.text == "0";
					if (flag5)
					{
						this.setEnergy(num);
					}
				}
				else
				{
					bool flag6 = num != this.c_energy_now && !this.bStart;
					if (flag6)
					{
						this.bStart = true;
						int nNowValue = this.c_energy_now;
						bool flag7 = this.tTween != null;
						if (flag7)
						{
							this.tTween.Kill(false);
						}
						this.tTween = DOTween.To(() => nNowValue, delegate(int x)
						{
							nNowValue = x;
						}, num, 0.2f);
						this.tTween.OnUpdate(delegate
						{
							this.setEnergy(nNowValue);
						});
						this.tTween.OnComplete(delegate
						{
							this.EndOverTween();
						});
					}
				}
			}
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x00101821 File Offset: 0x000FFA21
	private void EndOverTween()
	{
		this.bStart = false;
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x0010182C File Offset: 0x000FFA2C
	private void setEnergy(int _now)
	{
		bool flag = this.c_energy_max < 1 || this.oDaZhao == null || this.imagefill == null;
		if (!flag)
		{
			this.c_energy_now = _now;
			float num = (float)this.c_energy_now * 1f / (float)this.c_energy_max * 1f;
			this.imagefill.fillAmount = num;
			this.tNum.text = ((int)(num * 100f)).ToString();
			bool flag2 = this.c_energy_now >= this.c_energy_max;
			if (flag2)
			{
				bool activeSelf = this.gEmpty.activeSelf;
				if (activeSelf)
				{
					this.gEmpty.SetActive(false);
				}
				bool flag3 = !this.gFull.activeSelf;
				if (flag3)
				{
					this.gFull.SetActive(true);
					bool flag4 = !this._NetLiving.isCopy && CFilmManager.Instance.filmParentNetId(this._NetLiving) == this._NetLiving.getNetid();
					if (flag4)
					{
						((CPlayerSoundMe)this._NetLiving.playerSound).PlayDaZhaoManSound();
					}
				}
			}
			else
			{
				bool flag5 = !this.gEmpty.activeSelf;
				if (flag5)
				{
					this.gEmpty.SetActive(true);
				}
				bool activeSelf2 = this.gFull.activeSelf;
				if (activeSelf2)
				{
					this.gFull.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x001019A8 File Offset: 0x000FFBA8
	private void Update()
	{
		int num = 1;
		bool flag = this._NetLiving != null && (this._NetLiving.playerCommon.m_nChenMo == 1 || num == 0 || (MatchManager.GetInstance().m_pFbData.nType == 11 && this._NetLiving.playerCommon.m_nModelIndex == 1025));
		if (flag)
		{
			bool flag2 = !this.dazhaojin.enabled;
			if (flag2)
			{
				this.dazhaojin.enabled = true;
			}
			bool activeSelf = this.dtsusenum.activeSelf;
			if (activeSelf)
			{
				this.dtsusenum.SetActive(false);
			}
		}
		else
		{
			bool enabled = this.dazhaojin.enabled;
			if (enabled)
			{
				this.dazhaojin.enabled = false;
			}
		}
		this.CheckUiActSelf();
		this.OnCheckEnergy();
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x00101A80 File Offset: 0x000FFC80
	public void CheckUiActSelf()
	{
		bool flag = this._NetLiving == null || (this._NetLiving != null && this._NetLiving.isDead) || roleuiManager.GetInstance().bOtherUiVisable || gameuiCon.inst.mYukiOnna || (fGameManager.IsSingle && PlayerLearnManager.GetInstance().HideDaZhao) || (gameuiCon.inst.Net != null && gameuiCon.inst.Net.netobject_type == 2);
		if (flag)
		{
			bool activeSelf = this.oDaZhao.activeSelf;
			if (activeSelf)
			{
				this.oDaZhao.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.oDaZhao.activeSelf;
			if (flag2)
			{
				this.oDaZhao.SetActive(true);
			}
		}
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x00101B44 File Offset: 0x000FFD44
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

	// Token: 0x06001B49 RID: 6985 RVA: 0x00101B98 File Offset: 0x000FFD98
	private void LoadMapEffOk(WWW www, object Param)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			bool flag2 = this.szSingleEffObj.Count > 0;
			if (flag2)
			{
				Debug.Log("=============不需要重复加载...");
			}
			else
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = this.oDaZhao.transform;
				gameObject.SetActive(false);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.szSingleEffObj.Add(gameObject);
			}
		}
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x00101C64 File Offset: 0x000FFE64
	public void SetUiEffActive(bool active)
	{
		bool flag = this.szSingleEffObj.Count > 0;
		if (flag)
		{
			this.szSingleEffObj[0].transform.gameObject.SetActive(active);
		}
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x00101CA4 File Offset: 0x000FFEA4
	public void CleanSingleEff()
	{
		foreach (GameObject obj in this.szSingleEffObj)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.szSingleEffObj.Clear();
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x00101D08 File Offset: 0x000FFF08
	public override void Close()
	{
		bool flag = this.tTween != null;
		if (flag)
		{
			this.tTween.Kill(false);
		}
		base.Close();
	}

	// Token: 0x0400188F RID: 6287
	public static NewHeroDaZhaoUi Inst;

	// Token: 0x04001890 RID: 6288
	private GameObject oDaZhao;

	// Token: 0x04001891 RID: 6289
	private NetLiving _NetLiving;

	// Token: 0x04001892 RID: 6290
	private GameObject gFull;

	// Token: 0x04001893 RID: 6291
	private GameObject gEmpty;

	// Token: 0x04001894 RID: 6292
	private RawImage pic;

	// Token: 0x04001895 RID: 6293
	private Text tNum;

	// Token: 0x04001896 RID: 6294
	private Image imagefill;

	// Token: 0x04001897 RID: 6295
	private Image Q_img;

	// Token: 0x04001898 RID: 6296
	private Text QKeyText;

	// Token: 0x04001899 RID: 6297
	private GameObject Q_imgObj;

	// Token: 0x0400189A RID: 6298
	private Image dazhaojin;

	// Token: 0x0400189B RID: 6299
	private GameObject dtsusenum;

	// Token: 0x0400189C RID: 6300
	private Text dtsusrNumText;

	// Token: 0x0400189D RID: 6301
	private int c_energy_now;

	// Token: 0x0400189E RID: 6302
	private int c_energy_max;

	// Token: 0x0400189F RID: 6303
	private List<GameObject> szSingleEffObj = new List<GameObject>();

	// Token: 0x040018A0 RID: 6304
	private string sOldKey = string.Empty;

	// Token: 0x040018A1 RID: 6305
	private bool bStart;

	// Token: 0x040018A2 RID: 6306
	private Tween tTween;

	// Token: 0x020005EE RID: 1518
	private enum EnergyReason
	{
		// Token: 0x04002EAD RID: 11949
		REASON_DZENERGY_ONLINE = 1,
		// Token: 0x04002EAE RID: 11950
		REASON_DZENERGY_RELEASE,
		// Token: 0x04002EAF RID: 11951
		REASON_DZENERGY_LASTEND,
		// Token: 0x04002EB0 RID: 11952
		REASON_DZENERGY_STOP,
		// Token: 0x04002EB1 RID: 11953
		REASON_DZENERGY_KILL,
		// Token: 0x04002EB2 RID: 11954
		REASON_DZENERGY_TEST,
		// Token: 0x04002EB3 RID: 11955
		REASON_DZENERGY_CURE,
		// Token: 0x04002EB4 RID: 11956
		REASON_DZENERGY_ADDMAX
	}
}
