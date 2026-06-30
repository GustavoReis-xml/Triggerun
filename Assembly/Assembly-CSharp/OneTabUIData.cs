using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000284 RID: 644
public class OneTabUIData : Window
{
	// Token: 0x06001D9A RID: 7578 RVA: 0x00112D94 File Offset: 0x00110F94
	protected override void Awake()
	{
		base.Awake();
		this.tsl = base.transform.Find("sl").GetComponent<Image>();
		this.tBg = base.transform.Find("bg").GetComponent<Image>();
		this.raw = base.transform.Find("RawImage").GetComponent<RawImage>();
		this.tLevel = base.transform.Find("leve").GetComponent<Text>();
		this.tTeam = base.transform.Find("team").GetComponent<Image>();
		this.oMesh = base.transform.Find("mesh").gameObject;
		this.oMesh.SetActive(false);
		this.oDead = base.transform.Find("dead").gameObject;
		this.oDead.SetActive(false);
		this.deadfilled = this.oDead.transform.Find("deadfilled").GetComponent<Image>();
		this.oDaZhao = base.transform.Find("dz").gameObject;
		this.dzfilled = this.oDaZhao.transform.Find("dzfilled").GetComponent<Image>();
		this.oDaZhao.SetActive(false);
		this.duizhang = base.transform.Find("duizhang").GetComponent<Image>();
		this.duizhang.enabled = false;
		this.dz_Effect = base.transform.Find("dz_Effect").gameObject;
		this.skyladder = base.transform.Find("namelist/skyladder").gameObject;
		this.skyladderico = this.skyladder.transform.Find("ico").GetComponent<Image>();
		this.skylvl = this.skyladder.transform.Find("ico/lvl").GetComponent<Text>();
		this.skyladder.SetActive(false);
		this.tName = base.transform.Find("namelist/name").gameObject.GetComponent<Text>();
		this.RectNameForm = base.transform.Find("namelist").GetComponent<RectTransform>();
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x00112FD0 File Offset: 0x001111D0
	private void Update()
	{
		bool flag = this.bCheck;
		if (flag)
		{
			bool flag2 = this._NetObj.isDead || (MatchManager.GetInstance().m_pFbData.nType == 4 && this._NetObj.playerCommon.m_nPlayerState < 1);
			if (flag2)
			{
				bool flag3 = !this.oDead.activeSelf;
				if (flag3)
				{
					this.oDead.SetActive(true);
				}
				bool flag4 = !this.oMesh.activeSelf;
				if (flag4)
				{
					this.oMesh.SetActive(true);
				}
				bool activeSelf = this.oDaZhao.activeSelf;
				if (activeSelf)
				{
					this.oDaZhao.SetActive(false);
				}
				DateTime dPlayerDeathDate = this._NetObj.playerCommon.m_dPlayerDeathDate;
				DateTime d = dPlayerDeathDate.AddSeconds((double)this._NetObj.playerCommon.m_nDeathTimeLong);
				DateTime now = Fun.Now;
				float num = (float)(d - now).TotalSeconds;
				bool flag5 = num < 0f;
				if (flag5)
				{
					num = 0f;
				}
				this.deadfilled.fillAmount = num / (float)this._NetObj.playerCommon.m_nDeathTimeLong * 1f;
			}
			else
			{
				bool activeSelf2 = this.oDead.activeSelf;
				if (activeSelf2)
				{
					this.oDead.SetActive(false);
				}
				bool activeSelf3 = this.oMesh.activeSelf;
				if (activeSelf3)
				{
					this.oMesh.SetActive(false);
				}
				bool flag6 = this.m_nTempType == 0;
				if (flag6)
				{
					bool flag7 = !this.oDaZhao.activeSelf;
					if (flag7)
					{
						this.oDaZhao.SetActive(true);
					}
					float daZhaoValue = this._NetObj.playerDazhao.GetDaZhaoValue();
					this.dzfilled.fillAmount = daZhaoValue;
					bool flag8 = daZhaoValue >= 1f;
					if (flag8)
					{
						bool flag9 = !this.oDzEffect.activeSelf;
						if (flag9)
						{
							this.oDzEffect.SetActive(true);
						}
					}
					else
					{
						bool activeSelf4 = this.oDzEffect.activeSelf;
						if (activeSelf4)
						{
							this.oDzEffect.SetActive(false);
						}
					}
				}
				else
				{
					bool activeSelf5 = this.oDaZhao.activeSelf;
					if (activeSelf5)
					{
						this.oDaZhao.SetActive(false);
					}
					bool activeSelf6 = this.oDzEffect.activeSelf;
					if (activeSelf6)
					{
						this.oDzEffect.SetActive(false);
					}
				}
			}
		}
		else
		{
			bool activeSelf7 = this.oDead.activeSelf;
			if (activeSelf7)
			{
				this.oDead.SetActive(false);
			}
			bool activeSelf8 = this.oMesh.activeSelf;
			if (activeSelf8)
			{
				this.oMesh.SetActive(false);
			}
			bool activeSelf9 = this.oDaZhao.activeSelf;
			if (activeSelf9)
			{
				this.oDaZhao.SetActive(false);
			}
			bool flag10 = this.oDzEffect != null && this.oDzEffect.activeSelf;
			if (flag10)
			{
				this.oDzEffect.SetActive(false);
			}
		}
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x001132E8 File Offset: 0x001114E8
	private void InitTab()
	{
		this.bCheck = false;
		this.raw.enabled = false;
		this.tLevel.text = string.Empty;
		this.tName.text = string.Empty;
		this.tTeam.enabled = false;
		this.duizhang.enabled = false;
		this.skyladder.SetActive(false);
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x00113354 File Offset: 0x00111554
	public void OnSetUI(string sName, int nType, GameObject oCloneEffect, bool mSuo)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "#FF0000";
		string text4 = "zudui";
		string imageName = "hong";
		this.m_nTempType = nType;
		bool flag = this.m_nTempType == 0;
		if (flag)
		{
			imageName = "lan";
			text3 = "#00FFFF";
			this.RectNameForm.localPosition = new Vector3(0f, -145f, 0f);
			text4 = "duiyou";
		}
		else
		{
			this.RectNameForm.localPosition = new Vector3(0f, 145f, 0f);
		}
		this.tsl.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, imageName);
		this.tBg.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, "kp_0");
		this.userdata = MatchManager.GetInstance().GetUserData(sName);
		this.m_nNetId = 0;
		bool flag2 = this.userdata != null;
		if (flag2)
		{
			this.m_nNetId = this.userdata.m_nNetid;
		}
		this._NetObj = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(this.m_nNetId);
		bool flag3 = this._NetObj == null || this.m_nNetId == 0 || this.userdata == null;
		if (flag3)
		{
			this.InitTab();
			bool flag4 = this.userdata != null;
			if (flag4)
			{
				bool isSkyLadderMatch = SkyLadderManager.GetInstance().isSkyLadderMatch;
				if (isSkyLadderMatch)
				{
					this.tLevel.text = string.Empty;
				}
				else
				{
					this.tLevel.text = this.userdata.m_nLevel.ToString();
				}
				this.OnShowSkyLader();
				this.tName.text = string.Concat(new string[]
				{
					"<color=",
					text3,
					">",
					this.userdata.m_sNick,
					"</color>"
				});
				bool flag5 = SkyLadderManager.GetInstance().isSkyLadderMatch && text4 == "zudui";
				if (flag5)
				{
					this.tTeam.enabled = false;
				}
				else
				{
					this.tTeam.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, text4);
					this.tTeam.enabled = mSuo;
				}
			}
		}
		else
		{
			NetLiving player = fGameManager.GetInstance().getPlayer();
			bool flag6 = (this.m_nTempType == 1 && PlayerDataManager.getInstance().playercom.m_nIsFristFight == 0) || (this.m_nTempType == 0 && this.userdata.m_nIsChsok == 0);
			if (flag6)
			{
				this.InitTab();
				bool isSkyLadderMatch2 = SkyLadderManager.GetInstance().isSkyLadderMatch;
				if (isSkyLadderMatch2)
				{
					this.tLevel.text = string.Empty;
				}
				else
				{
					this.tLevel.text = this.userdata.m_nLevel.ToString();
				}
				this.OnShowSkyLader();
				this.tName.text = string.Concat(new string[]
				{
					"<color=",
					text3,
					">",
					this.userdata.m_sNick,
					"</color>"
				});
				bool flag7 = SkyLadderManager.GetInstance().isSkyLadderMatch && text4 == "zudui";
				if (flag7)
				{
					this.tTeam.enabled = false;
				}
				else
				{
					this.tTeam.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, text4);
					this.tTeam.enabled = mSuo;
				}
				bool flag8 = MatchManager.GetInstance().m_pFbData.nType == 4 && player.playerCommon.m_nPlayerState < 1;
				if (flag8)
				{
					text2 = fGameManager.mloadConfig.AllHeroUnlock[this.userdata.m_nHeroID].szSkinUnlock[1].sIconName;
					GetIconComponent component = this.raw.GetComponent<GetIconComponent>();
					bool flag9 = component != null;
					if (flag9)
					{
						component._Url = string.Empty;
					}
					this.raw.enabled = false;
					bool flag10 = text2 != string.Empty;
					if (flag10)
					{
						fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + text2 + ".txt", this.raw);
					}
					bool flag11 = this.dz_Effect.transform.childCount < 1 && this.oDzEffect == null;
					if (flag11)
					{
						this.oDzEffect = UnityEngine.Object.Instantiate<GameObject>(oCloneEffect);
						this.oDzEffect.transform.SetParent(this.dz_Effect.transform);
						this.oDzEffect.transform.localPosition = new Vector3(0f, 0f, 0f);
						this.oDzEffect.transform.localRotation = Quaternion.Euler(Vector3.zero);
						this.oDzEffect.transform.localScale = new Vector3(108.042f, 108.042f, 108.042f);
					}
					this.bCheck = true;
				}
			}
			else
			{
				text = this.userdata.m_sNick;
				int nLevel = this.userdata.m_nLevel;
				int nHeroID = this.userdata.m_nHeroID;
				this.OnShowSkyLader();
				this.tName.text = string.Concat(new string[]
				{
					"<color=",
					text3,
					">",
					text,
					"</color>"
				});
				text2 = fGameManager.mloadConfig.AllHeroUnlock[nHeroID].szSkinUnlock[1].sIconName;
				GetIconComponent component2 = this.raw.GetComponent<GetIconComponent>();
				bool flag12 = component2 != null;
				if (flag12)
				{
					component2._Url = string.Empty;
				}
				this.raw.enabled = false;
				bool flag13 = text2 != string.Empty;
				if (flag13)
				{
					fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + text2 + ".txt", this.raw);
				}
				bool isSkyLadderMatch3 = SkyLadderManager.GetInstance().isSkyLadderMatch;
				if (isSkyLadderMatch3)
				{
					this.tLevel.text = string.Empty;
				}
				else
				{
					this.tLevel.text = ((nLevel != 0) ? nLevel.ToString() : string.Empty);
				}
				bool flag14 = SkyLadderManager.GetInstance().isSkyLadderMatch && text4 == "zudui";
				if (flag14)
				{
					this.tTeam.enabled = false;
				}
				else
				{
					this.tTeam.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, text4);
					this.tTeam.enabled = mSuo;
				}
				bool flag15 = this.dz_Effect.transform.childCount < 1 && this.oDzEffect == null;
				if (flag15)
				{
					this.oDzEffect = UnityEngine.Object.Instantiate<GameObject>(oCloneEffect);
					this.oDzEffect.transform.SetParent(this.dz_Effect.transform);
					this.oDzEffect.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.oDzEffect.transform.localRotation = Quaternion.Euler(Vector3.zero);
					this.oDzEffect.transform.localScale = new Vector3(108.042f, 108.042f, 108.042f);
				}
				this.bCheck = true;
			}
		}
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x00113AC0 File Offset: 0x00111CC0
	private void OnShowSkyLader()
	{
		bool flag = this.userdata != null && this.userdata.m_szSkyData.Count > 0 && (int)this.userdata.m_szSkyData[1] > 0;
		if (flag)
		{
			int num = (int)this.userdata.m_szSkyData[1];
			bool flag2 = num > 8;
			if (flag2)
			{
				num = 8;
			}
			this.tBg.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, "kp_" + num.ToString());
		}
		else
		{
			this.tBg.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, "kp_0");
		}
	}

	// Token: 0x04001A75 RID: 6773
	private Image tBg;

	// Token: 0x04001A76 RID: 6774
	private Text tName;

	// Token: 0x04001A77 RID: 6775
	private RawImage raw;

	// Token: 0x04001A78 RID: 6776
	private RectTransform RectNameForm;

	// Token: 0x04001A79 RID: 6777
	private Text tLevel;

	// Token: 0x04001A7A RID: 6778
	private Image tTeam;

	// Token: 0x04001A7B RID: 6779
	private GameObject oDead;

	// Token: 0x04001A7C RID: 6780
	private Image deadfilled;

	// Token: 0x04001A7D RID: 6781
	private Image dzfilled;

	// Token: 0x04001A7E RID: 6782
	private Image duizhang;

	// Token: 0x04001A7F RID: 6783
	private GameObject oMesh;

	// Token: 0x04001A80 RID: 6784
	private GameObject oDaZhao;

	// Token: 0x04001A81 RID: 6785
	private GameObject dz_Effect;

	// Token: 0x04001A82 RID: 6786
	private GameObject oDzEffect;

	// Token: 0x04001A83 RID: 6787
	private UserData userdata;

	// Token: 0x04001A84 RID: 6788
	private NetLiving _NetObj;

	// Token: 0x04001A85 RID: 6789
	private GameObject skyladder;

	// Token: 0x04001A86 RID: 6790
	private Image skyladderico;

	// Token: 0x04001A87 RID: 6791
	private Text skylvl;

	// Token: 0x04001A88 RID: 6792
	private Image tsl;

	// Token: 0x04001A89 RID: 6793
	private int m_nTempType;

	// Token: 0x04001A8A RID: 6794
	private int m_nNetId;

	// Token: 0x04001A8B RID: 6795
	private bool bCheck;
}
