using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027E RID: 638
public class OneOwnHeroTopCon : Window
{
	// Token: 0x06001D66 RID: 7526 RVA: 0x0010FFBC File Offset: 0x0010E1BC
	protected override void Awake()
	{
		base.Awake();
		this.oBg0 = base.transform.Find("bg0").gameObject;
		this.oRound = this.oBg0.transform.Find("round").gameObject;
		this.dr = this.oRound.GetComponent<DORotateCon>();
		bool flag = this.dr == null;
		if (flag)
		{
			this.dr = this.oRound.AddComponent<DORotateCon>();
			this.dr.bStart = false;
		}
		this.oBg1 = base.transform.Find("bg1").GetComponent<Image>();
		this.oBg1.enabled = false;
		this.herodata = base.transform.Find("herodata").gameObject;
		this.unbg = this.herodata.transform.Find("unbg").GetComponent<Image>();
		this.heroimg = this.herodata.transform.Find("heroimg").GetComponent<RawImage>();
		this.herodatabg = this.herodata.transform.Find("bg").GetComponent<Image>();
		this.heroname = this.herodata.transform.Find("heroname").GetComponent<Text>();
		this.outnale = this.herodata.transform.Find("heroname").GetComponent<Outline>();
		this.unlock = this.herodata.transform.Find("unlock").gameObject;
		this.unlocktext = this.unlock.transform.Find("Text").GetComponent<Text>();
		this.unlock.SetActive(false);
		this.rolelevel = this.herodata.transform.Find("rolelevel").GetComponent<Text>();
		this.lzimg = this.herodata.transform.Find("lzimg").gameObject;
		this.lzimg.SetActive(false);
		this.LzName = this.herodata.transform.Find("LzName").gameObject;
		this.lz = this.LzName.transform.Find("lz").gameObject;
		this.lz.SetActive(false);
		this.skyimg = this.herodata.transform.Find("skyimg").gameObject;
		this.skyimg.SetActive(false);
		this.rolename = this.LzName.transform.Find("rolename").GetComponent<Text>();
		this.LzName.SetActive(false);
		this.duizhang = this.herodata.transform.Find("duizhang").GetComponent<Image>();
		this.duizhang.enabled = false;
		this.herodata.SetActive(false);
		this.suo = base.transform.Find("suo").GetComponent<Image>();
		this.suo.enabled = false;
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x001102D4 File Offset: 0x0010E4D4
	public void OnShowData(string sUserName, ArrayList szUserList, int nNum)
	{
		string text = string.Empty;
		this.userdata = roleuiManager.GetInstance().GetUserData(sUserName);
		bool flag = this.userdata == null;
		if (flag)
		{
			this.InitUI();
		}
		else
		{
			int nHeroID = this.userdata.m_nHeroID;
			int num = this.userdata.m_nIsChsok;
			bool flag2 = nNum == 0;
			if (flag2)
			{
				num = 1;
			}
			this.oBg0.SetActive(false);
			this.dr.bStart = false;
			this.heroimg.enabled = false;
			this.LzName.SetActive(true);
			this.rolename.text = this.userdata.m_sNick;
			bool isSkyLadderMatch = SkyLadderManager.GetInstance().isSkyLadderMatch;
			if (isSkyLadderMatch)
			{
				this.rolelevel.text = string.Empty;
			}
			else
			{
				this.rolelevel.text = Language.GetLang(2023, new object[]
				{
					this.userdata.m_nLevel
				});
			}
			string sMyPF_Name = this.userdata.m_szQQdata[4].ToString();
			this.OnLzIcoData(this.userdata.m_szQQdata, sMyPF_Name);
			bool enabled = this.IsTeamIco(szUserList, nNum);
			bool flag3 = MatchManager.GetInstance().m_pFbData.nType == 11;
			if (flag3)
			{
				enabled = false;
			}
			this.suo.enabled = enabled;
			this.unlock.SetActive(false);
			int num2 = num;
			int num3 = num2;
			if (num3 != 0)
			{
				if (num3 == 1)
				{
					this.unbg.enabled = false;
					string str = string.Empty;
					bool flag4 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nHeroID);
					if (flag4)
					{
						HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[nHeroID];
						bool flag5 = heroUnlockData != null && heroUnlockData.szSkinUnlock.ContainsKey(1);
						if (flag5)
						{
							str = heroUnlockData.szSkinUnlock[1].sIconName;
							fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + str + ".txt", this.heroimg);
						}
					}
					HeroData heroData = roleuiManager.getHeroData(nHeroID);
					bool flag6 = heroData != null;
					if (flag6)
					{
						this.outnale.enabled = true;
						text = heroData.hname;
					}
					this.heroname.text = text;
					this.herodata.SetActive(true);
				}
			}
			else
			{
				this.outnale.enabled = false;
				this.heroname.text = Language.GetLang(2035, new object[0]);
				this.unbg.enabled = true;
				this.herodata.SetActive(true);
			}
		}
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00110584 File Offset: 0x0010E784
	private bool IsTeamIco(ArrayList szUserList, int nNum)
	{
		bool result = false;
		int count = szUserList.Count;
		int nTeamID = this.userdata.m_nTeamID;
		bool flag = nTeamID != 0 && nNum + 1 < count;
		if (flag)
		{
			string sUserName = szUserList[nNum + 1].ToString();
			int teamID = roleuiManager.GetInstance().GetTeamID(sUserName);
			bool flag2 = nTeamID == teamID;
			if (flag2)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x001105ED File Offset: 0x0010E7ED
	public void InitUI()
	{
		this.oBg0.SetActive(true);
		this.dr.bStart = true;
		this.herodata.SetActive(false);
		this.suo.enabled = false;
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x00110624 File Offset: 0x0010E824
	private void OnLzIcoData(ArrayList QQLzdata, string sMyPF_Name)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = (int)QQLzdata[0];
		int num2 = (int)QQLzdata[1];
		int num3 = (int)QQLzdata[2];
		int num4 = (int)QQLzdata[3];
		Fun.ClearChildObejct(this.lz);
		bool flag = this.userdata != null && this.userdata.m_szSkyData.Count > 0 && (int)this.userdata.m_szSkyData[1] > 0;
		if (flag)
		{
			Image image = this.herodatabg;
			string atlas_SELECTHERO = AtlasType.ATLAS_SELECTHERO;
			string str = "kapai_";
			object obj = this.userdata.m_szSkyData[1];
			image.sprite = Window.spriteName(atlas_SELECTHERO, str + ((obj != null) ? obj.ToString() : null));
		}
		else
		{
			this.herodatabg.sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, "kapai_0");
		}
		text2 = Fun.GetTwoQQ_State(sMyPF_Name, this.userdata.m_szQQdata[4].ToString());
		bool flag2 = num == 1 && text2 != string.Empty;
		if (flag2)
		{
			text = ((num2 != 1) ? string.Format("pt" + text2 + "_0{0}", num4 + 1) : string.Format("hh" + text2 + "_0{0}", num4 + 1));
		}
		bool flag3 = !string.IsNullOrEmpty(text);
		if (flag3)
		{
			this.AddLzIco(text, this.lz, this.lzimg);
		}
		text = string.Empty;
		bool flag4 = num3 == 1;
		if (flag4)
		{
			text = Fun.GetNainImgName(text2);
		}
		bool flag5 = !string.IsNullOrEmpty(text);
		if (flag5)
		{
			this.AddLzIco(text, this.lz, this.lzimg);
		}
		this.lz.SetActive(true);
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x00110808 File Offset: 0x0010EA08
	private void AddLzIco(string imgname, GameObject oList, GameObject oClone)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(oClone);
		gameObject.transform.SetParent(oList.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, imgname);
		gameObject.SetActive(true);
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x00110884 File Offset: 0x0010EA84
	public void OnShowTopImg(int nHeroId, bool mbool)
	{
		string text = string.Empty;
		HeroData heroData = roleuiManager.getHeroData(nHeroId);
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nHeroId);
		if (flag)
		{
			HeroUnlockData heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[nHeroId];
			bool flag2 = heroUnlockData != null && heroUnlockData.szSkinUnlock.ContainsKey(1);
			if (flag2)
			{
				text = heroUnlockData.szSkinUnlock[1].sIconName;
			}
		}
		this.heroimg.enabled = false;
		bool flag3 = text != string.Empty;
		if (flag3)
		{
			fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + text + ".txt", this.heroimg);
		}
		bool flag4 = heroData != null;
		if (flag4)
		{
			this.heroname.text = heroData.hname;
		}
		this.unlock.SetActive(mbool);
		if (mbool)
		{
			this.unlocktext.text = Language.GetLang(7035, new object[0]);
		}
	}

	// Token: 0x04001A16 RID: 6678
	private GameObject oBg0;

	// Token: 0x04001A17 RID: 6679
	private GameObject oRound;

	// Token: 0x04001A18 RID: 6680
	private DORotateCon dr;

	// Token: 0x04001A19 RID: 6681
	private Image oBg1;

	// Token: 0x04001A1A RID: 6682
	private GameObject herodata;

	// Token: 0x04001A1B RID: 6683
	private Image unbg;

	// Token: 0x04001A1C RID: 6684
	private RawImage heroimg;

	// Token: 0x04001A1D RID: 6685
	private Image herodatabg;

	// Token: 0x04001A1E RID: 6686
	private Text heroname;

	// Token: 0x04001A1F RID: 6687
	private Text rolelevel;

	// Token: 0x04001A20 RID: 6688
	private GameObject lzimg;

	// Token: 0x04001A21 RID: 6689
	private GameObject LzName;

	// Token: 0x04001A22 RID: 6690
	private GameObject lz;

	// Token: 0x04001A23 RID: 6691
	private Text rolename;

	// Token: 0x04001A24 RID: 6692
	private Image duizhang;

	// Token: 0x04001A25 RID: 6693
	private Image suo;

	// Token: 0x04001A26 RID: 6694
	private Outline outnale;

	// Token: 0x04001A27 RID: 6695
	private GameObject unlock;

	// Token: 0x04001A28 RID: 6696
	private Text unlocktext;

	// Token: 0x04001A29 RID: 6697
	private GameObject skyimg;

	// Token: 0x04001A2A RID: 6698
	private UserData userdata;
}
