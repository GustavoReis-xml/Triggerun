using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020E RID: 526
public class KingUserInfoCon : Window
{
	// Token: 0x06001701 RID: 5889 RVA: 0x000D5F64 File Offset: 0x000D4164
	private void Start()
	{
		this.tUserName = base.gameObject.transform.FindChild("username").GetComponent<Text>();
		this.tUserScore = base.gameObject.transform.FindChild("userscore").GetComponent<Text>();
		this.UserRank = base.gameObject.transform.FindChild("userrank").GetComponent<Image>();
		this.UserHead = base.gameObject.transform.FindChild("userhead").GetComponent<RawImage>();
		this.UserHpSdr = base.gameObject.transform.FindChild("TimeSlider").GetComponent<Slider>();
		this.tPlayerstate = base.gameObject.transform.FindChild("playerstate").GetComponent<Text>();
		this.IsLoad = true;
		this.zdlImg = base.gameObject.transform.FindChild("zdlimg").GetComponent<Image>();
		this.pGameMpEff = base.gameObject.transform.FindChild("ui_pve_mq").gameObject;
		this.InitData(this.m_szUserInfo);
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000D6088 File Offset: 0x000D4288
	public void UpdaeRank(int nRank, ArrayList szUserInfo)
	{
		bool isLoad = this.IsLoad;
		if (isLoad)
		{
			this.UserRank.sprite = Window.spriteName(AtlasType.ATLAS_KING, nRank.ToString());
			this.tUserScore.text = szUserInfo[2].ToString();
		}
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x000D60D8 File Offset: 0x000D42D8
	public void InitData(ArrayList szUserInfo)
	{
		bool flag = szUserInfo != null;
		if (flag)
		{
			this.m_szUserInfo = szUserInfo;
			this.nServerId = (int)szUserInfo[5];
			this.nCid = (int)szUserInfo[0];
			bool isLoad = this.IsLoad;
			if (isLoad)
			{
				this.tUserName.text = szUserInfo[1].ToString();
			}
		}
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x000D6140 File Offset: 0x000D4340
	private void OnShowHeroHead()
	{
		string empty = string.Empty;
		int num = 0;
		bool flag = this.pPlayer == null;
		if (flag)
		{
			this.pPlayer = Fun.Get_NetLiving(this.nServerId);
		}
		bool flag2 = this.pPlayer == null;
		if (!flag2)
		{
			int nModelIndex = this.pPlayer.playerCommon.m_nModelIndex;
			bool flag3 = nModelIndex != this.m_nHeroId;
			if (flag3)
			{
				ArrayList otherZhanDuLi = PveManager.GetInstance().GetOtherZhanDuLi(nModelIndex, (ArrayList)this.m_szUserInfo[6]);
				bool flag4 = this.pPlayer.playerCommon.m_szGeneData.Count > 0;
				if (flag4)
				{
					num = PveManager.GetInstance().StrengthenData.GetGeneEffect((int)this.pPlayer.playerCommon.m_szGeneData[0], (int)this.pPlayer.playerCommon.m_szGeneData[1]);
				}
				this.tUserScore.text = ((int)otherZhanDuLi[1] + num).ToString();
				int num2 = (int)otherZhanDuLi[0];
				this.m_szUserInfo[7] = (int)otherZhanDuLi[1] + num;
				Image image = this.zdlImg;
				string atlas_PVEFB = AtlasType.ATLAS_PVEFB;
				if (!true)
				{
				}
				string imageName;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						imageName = "fz";
					}
					else
					{
						imageName = "f";
					}
				}
				else
				{
					imageName = "g";
				}
				if (!true)
				{
				}
				image.sprite = Window.spriteName(atlas_PVEFB, imageName);
				KingTrainManager.GetInstance().OnSetKingUserData(this.nCid, this.m_szUserInfo);
				KingCon.inst.OnUpdatePaiXu();
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[nModelIndex];
				fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", this.UserHead);
				this.m_nHeroId = nModelIndex;
			}
		}
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x000D6340 File Offset: 0x000D4540
	public void CheckHp()
	{
		bool flag = this.pPlayer == null;
		if (flag)
		{
			this.pPlayer = Fun.Get_NetLiving(this.nServerId);
		}
		bool flag2 = this.pPlayer == null;
		if (!flag2)
		{
			bool flag3 = this.pPlayer.playerCommon.m_nPlayerState == 0;
			if (flag3)
			{
				this.OnDeath();
			}
			else
			{
				int nowHp = this.pPlayer.playerCommon.GetNowHp();
				int maxHp = this.pPlayer.playerCommon.GetMaxHp();
				this.UserHpSdr.value = (float)nowHp / (float)maxHp;
				bool flag4 = this.pPlayer.playerDazhao != null;
				if (flag4)
				{
					float daZhaoValue = this.pPlayer.playerDazhao.GetDaZhaoValue();
					bool flag5 = daZhaoValue >= 1f;
					if (flag5)
					{
						bool flag6 = !this.pGameMpEff.activeSelf;
						if (flag6)
						{
							this.pGameMpEff.SetActive(true);
						}
					}
					else
					{
						bool activeSelf = this.pGameMpEff.activeSelf;
						if (activeSelf)
						{
							this.pGameMpEff.SetActive(false);
						}
					}
				}
				bool flag7 = this.bPlayDeadState;
				if (flag7)
				{
					this.tPlayerstate.gameObject.SetActive(false);
					this.UserHpSdr.gameObject.SetActive(true);
					base.CancelInvoke("OnDeathDaoJiShi");
				}
				this.bPlayDeadState = false;
			}
		}
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000D64A4 File Offset: 0x000D46A4
	private void OnDeath()
	{
		bool flag = !this.bPlayDeadState;
		if (flag)
		{
			this.tPlayerstate.gameObject.SetActive(true);
			this.tPlayerstate.text = string.Empty;
			this.UserHpSdr.gameObject.SetActive(false);
			bool flag2 = base.IsInvoking("OnDeathDaoJiShi");
			if (flag2)
			{
				base.CancelInvoke("OnDeathDaoJiShi");
			}
			base.InvokeRepeating("OnDeathDaoJiShi", 0f, 1f);
			this.bPlayDeadState = true;
		}
		bool flag3 = this.nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag3)
		{
			KingCon.inst.OnSetFillAmount(this.pPlayer.playerCommon.m_nDeathTimeLong);
		}
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000D6568 File Offset: 0x000D4768
	private void OnDeathDaoJiShi()
	{
		bool flag = this.pPlayer == null;
		if (flag)
		{
			this.pPlayer = Fun.Get_NetLiving(this.nServerId);
		}
		bool flag2 = KingTrainManager.GetInstance().GetPveUserDeahState() == 1;
		if (flag2)
		{
			bool flag3 = this.nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
			if (flag3)
			{
				KingCon.inst.tDeadCdText.text = Language.GetLang(26052, new object[0]);
			}
			this.tPlayerstate.text = Language.GetLang(26052, new object[0]);
		}
		else
		{
			int num = Fun.GetEndCDTime(this.pPlayer.playerCommon.m_dPlayerDeathDate.ToString(), this.pPlayer.playerCommon.m_nDeathTimeLong + 1);
			bool flag4 = num <= 0;
			if (flag4)
			{
				num = 1;
			}
			this.tPlayerstate.text = Language.GetLang(26010, new object[]
			{
				num
			});
			bool flag5 = this.nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
			if (flag5)
			{
				KingCon.inst.OnSetDeadCd(num);
			}
		}
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000D6694 File Offset: 0x000D4894
	private void Update()
	{
		this.CheckHp();
		this.OnShowHeroHead();
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000D66A8 File Offset: 0x000D48A8
	public void ShowScoreMsg(int nHurt = 0)
	{
		NetLiving netLiving = Fun.Get_NetLiving(this.nServerId);
		bool flag = netLiving != null && netLiving.playerHaveCreate && !netLiving.haveDestroy;
		if (flag)
		{
			string text = string.Empty;
			text += nHurt.ToString();
			Vector3 position = netLiving.defaultRigidbody.transform.position;
			position.y += 1f;
			fUIManager.GetInstance().showDamage(position, text, 1);
		}
	}

	// Token: 0x04001237 RID: 4663
	private Text tUserName;

	// Token: 0x04001238 RID: 4664
	private Text tUserScore;

	// Token: 0x04001239 RID: 4665
	private ArrayList m_szUserInfo;

	// Token: 0x0400123A RID: 4666
	private int nServerId;

	// Token: 0x0400123B RID: 4667
	private int nCid;

	// Token: 0x0400123C RID: 4668
	private bool IsLoad;

	// Token: 0x0400123D RID: 4669
	private NetLiving pPlayer;

	// Token: 0x0400123E RID: 4670
	private Image UserRank;

	// Token: 0x0400123F RID: 4671
	private RawImage UserHead;

	// Token: 0x04001240 RID: 4672
	private Slider UserHpSdr;

	// Token: 0x04001241 RID: 4673
	private Text tPlayerstate;

	// Token: 0x04001242 RID: 4674
	private int m_nHeroId;

	// Token: 0x04001243 RID: 4675
	private string sDeadTime = string.Empty;

	// Token: 0x04001244 RID: 4676
	private bool bPlayDeadState;

	// Token: 0x04001245 RID: 4677
	private Image zdlImg;

	// Token: 0x04001246 RID: 4678
	private GameObject pGameMpEff;
}
