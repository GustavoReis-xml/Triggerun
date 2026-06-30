using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class KingCon : Window
{
	// Token: 0x0600169E RID: 5790 RVA: 0x000D13F0 File Offset: 0x000CF5F0
	protected override void Awake()
	{
		base.Awake();
		KingCon.inst = this;
		this.tRoundText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.tMonsterNum = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.Remaining_Time = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.Prepare_Describe = this.UIGameObj.Get_GameObj(3);
		this.Countdown_Effect = this.UIGameObj.Get_GameObj(5);
		this.pDescribe = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.pUserListPanel = this.UIGameObj.Get_GameObj(7);
		this.OneUserInfo = this.UIGameObj.Get_GameObj(8);
		this.tRoundTiShi = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.Victory_Effect = this.UIGameObj.Get_GameObj(46);
		}
		else
		{
			this.Victory_Effect = this.UIGameObj.Get_GameObj(10);
		}
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			this.Failure_Effect = this.UIGameObj.Get_GameObj(47);
		}
		else
		{
			this.Failure_Effect = this.UIGameObj.Get_GameObj(11);
		}
		this.tKingFbName = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.tKingFbNanDu = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.tHeiAnFbName = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.tHeiAnFbNanDu = this.UIGameObj.Get_GameObj(39).GetComponent<Text>();
		this.pXingXingList = this.UIGameObj.Get_GameObj(15);
		this.pReviveUI = this.UIGameObj.Get_GameObj(17);
		this.OneUserInfo.SetActive(false);
		this.pReviveUI.SetActive(false);
		this.pBossHpPanel = this.UIGameObj.Get_GameObj(18);
		this.mBossHp_Value_1 = this.UIGameObj.Get_GameObj(19).GetComponent<Image>();
		this.mBossHp_Value_2 = this.UIGameObj.Get_GameObj(20).GetComponent<Image>();
		this.tBossHp_RoundNum = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		this.tBoss_Name = this.UIGameObj.Get_GameObj(22).GetComponent<Text>();
		this.tLianShaTxt = this.UIGameObj.Get_GameObj(23).GetComponent<Text>();
		this.tNpcHp = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.tNpcValueImg = this.UIGameObj.Get_GameObj(24).GetComponent<Image>();
		this.tAllDeadTiShi = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.tAllDeadTime = this.UIGameObj.Get_GameObj(27).GetComponent<Text>();
		this.pNpcHurtTiShi = this.UIGameObj.Get_GameObj(28);
		this.tNpcHurtTiShi = this.pNpcHurtTiShi.GetComponent<Text>();
		this.pDropPanel = this.UIGameObj.Get_GameObj(29);
		this.pDropItemList = this.UIGameObj.Get_GameObj(30);
		this.pDropOneItem = this.UIGameObj.Get_GameObj(31);
		this.pDropText = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.pDropPanel.SetActive(false);
		this.pDropOneItem.SetActive(false);
		this.tDeadTiao = this.UIGameObj.Get_GameObj(33).GetComponent<Image>();
		this.tDeadCdText = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.pNpcHpPanel = this.UIGameObj.Get_GameObj(35);
		this.pKingPanel = this.UIGameObj.Get_GameObj(36);
		this.pHeiAnPanel = this.UIGameObj.Get_GameObj(37);
		this.tHeiAnMonsterNum = this.UIGameObj.Get_GameObj(40).GetComponent<Text>();
		this.tChengFaTxt = this.UIGameObj.Get_GameObj(42).GetComponent<Text>();
		this.ShowObj = this.UIGameObj.Get_GameObj(41);
		this.jiantou = this.ShowObj.transform.FindChild("jiantou").gameObject;
		this.rect = this.ShowObj.GetComponent<RectTransform>();
		this.vSize = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
		this.szStarImg.Add(this.UIGameObj.Get_GameObj(43).GetComponent<Image>());
		this.szStarImg.Add(this.UIGameObj.Get_GameObj(44).GetComponent<Image>());
		this.szStarImg.Add(this.UIGameObj.Get_GameObj(45).GetComponent<Image>());
		this.highEffImg = this.UIGameObj.Get_GameObj(16).GetComponent<Image>();
		EventTriggerListener.Get(this.highEffImg.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverHighEff);
		EventTriggerListener.Get(this.highEffImg.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitHighEff);
		this.highEffImg.enabled = false;
		this.OnUpdateMonsterNum(0);
		this.OnAllUserDead(0);
		this.OnSetGameType();
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x000D1930 File Offset: 0x000CFB30
	public void OnSetGameType()
	{
		bool flag = KingTrainManager.GetInstance().m_pKingGame != null;
		if (flag)
		{
			int nGameType = KingTrainManager.GetInstance().m_pKingGame.m_nGameType;
			bool flag2 = nGameType == 9;
			if (flag2)
			{
				this.pNpcHpPanel.SetActive(true);
				this.pKingPanel.SetActive(true);
				this.tFbName = this.tKingFbName;
				this.tFbNanDu = this.tKingFbNanDu;
				this.pXingXingList.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -118f, 0f);
			}
			else
			{
				this.pHeiAnPanel.SetActive(true);
				this.pNpcHpPanel.SetActive(false);
				this.tFbName = this.tHeiAnFbName;
				this.tFbNanDu = this.tHeiAnFbNanDu;
				this.pXingXingList.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -124f, 0f);
			}
			FbXmlData pFbData = MatchManager.GetInstance().m_pFbData;
			FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[pFbData.nstartplotid];
			int num = (int)PveManager.GetInstance().GetPveDataByFbType(9)[6];
			bool flag3 = num > fbModelXmlData.m_nZdl * 2;
			if (flag3)
			{
				this.highEffImg.enabled = true;
				this.pDropPanel.SetActive(false);
			}
			else
			{
				this.highEffImg.enabled = false;
			}
		}
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000D1AA0 File Offset: 0x000CFCA0
	public void OnOtherUpdateBossHp(int nBossId, int nBossHp, string sName)
	{
		NetLiving netLiving = Fun.Get_NetLiving(nBossId);
		bool flag = netLiving == null;
		if (!flag)
		{
			bool flag2 = nBossHp <= 0 || netLiving.isDead;
			if (flag2)
			{
				this.BossFightIng = false;
			}
			else
			{
				this.BossFightIng = true;
				bool flag3 = this.pBossHpPanel.activeSelf && nBossId == this.m_nBossId;
				if (flag3)
				{
					this.OnShowBossHp(nBossId, nBossHp, sName);
				}
			}
		}
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x000D1B0C File Offset: 0x000CFD0C
	public void OnShowBossHp(int nBossId, int nBossHp, string sName)
	{
		bool flag = KingTrainManager.GetInstance().m_pKingGame == null;
		if (!flag)
		{
			int nGameState = KingTrainManager.GetInstance().m_pKingGame.m_nGameState;
			bool flag2 = nGameState != 1;
			if (flag2)
			{
				this.pBossHpPanel.SetActive(false);
			}
			else
			{
				this.BossFightIng = true;
				NetLiving netLiving = Fun.Get_NetLiving(nBossId);
				bool flag3 = nBossHp <= 0 || netLiving.isDead;
				if (flag3)
				{
					this.BossFightIng = false;
					this.pBossHpPanel.SetActive(false);
				}
				else
				{
					this.m_fShowTime = Time.time;
					int num = nBossHp % this.m_nMaxRoundHp;
					int num2 = (num != 0) ? (nBossHp / this.m_nMaxRoundHp + 1) : (nBossHp / this.m_nMaxRoundHp);
					bool flag4 = !this.pBossHpPanel.activeSelf;
					if (flag4)
					{
						this.pBossHpPanel.SetActive(true);
					}
					bool flag5 = num2 == this.m_nCurRound && this.m_nBossId == nBossId;
					if (flag5)
					{
						this.mBossHp_Value_1.fillAmount = 1f;
						this.mBossHp_Value_2.fillAmount = (float)num / (float)this.m_nMaxRoundHp;
					}
					else
					{
						this.tBoss_Name.text = sName;
						this.tBossHp_RoundNum.text = "*" + num2.ToString();
						this.m_nCurRound = num2;
						this.m_nBossId = nBossId;
						bool flag6 = num2 == 1;
						if (flag6)
						{
							this.mBossHp_Value_2.color = Fun.StrToColor("00ff00", 1f);
							this.mBossHp_Value_1.gameObject.SetActive(false);
						}
						else
						{
							this.mBossHp_Value_1.gameObject.SetActive(true);
							bool flag7 = num2 % 2 == 0;
							if (flag7)
							{
								this.mBossHp_Value_1.color = Fun.StrToColor("00ff00", 1f);
								this.mBossHp_Value_2.color = Fun.StrToColor("00a0e9", 1f);
							}
							else
							{
								this.mBossHp_Value_1.color = Fun.StrToColor("00ff00", 1f);
								this.mBossHp_Value_2.color = Fun.StrToColor("00a0e9", 1f);
							}
						}
						this.mBossHp_Value_1.fillAmount = 1f;
						this.mBossHp_Value_2.fillAmount = (float)num / (float)this.m_nMaxRoundHp;
					}
				}
			}
		}
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x000D1D68 File Offset: 0x000CFF68
	private void OnShowNpcHaveDamage()
	{
		bool flag = this.m_nNpcId == 0 && KingTrainManager.GetInstance().m_pKingGame != null;
		if (flag)
		{
			this.m_nNpcId = KingTrainManager.GetInstance().m_pKingGame.m_nNpcServerID;
		}
		this.pNpc = Fun.Get_NetLiving(this.m_nNpcId);
		bool flag2 = this.pNpc == null;
		if (!flag2)
		{
			int nowHp = this.pNpc.playerCommon.GetNowHp();
			bool flag3 = this.nNpcOldHp != nowHp;
			if (flag3)
			{
				bool flag4 = this.nNpcOldHp != -1;
				if (flag4)
				{
					bool flag5 = !this.pNpcHurtTiShi.activeSelf;
					if (flag5)
					{
						this.pNpcHurtTiShi.SetActive(true);
						fUISoundManager.instance.PlayUISound("ui_pve_shuijing_alarm", 0, null, 20);
					}
					this.tNpcHurtTime = Time.time;
				}
				this.nNpcOldHp = nowHp;
				int maxHp = this.pNpc.playerCommon.GetMaxHp();
				float num = (float)nowHp / (float)maxHp;
				this.tNpcValueImg.fillAmount = num;
				this.tNpcHp.text = Language.GetLang(26011, new object[]
				{
					(num * 100f).ToString("f0")
				});
				bool flag6 = num <= 0.2f;
				if (flag6)
				{
					this.tNpcHurtTiShi.text = Language.GetLang(26054, new object[]
					{
						(num * 100f).ToString("f0")
					});
				}
				bool flag7 = nowHp == 0;
				if (flag7)
				{
					this.pNpcHurtTiShi.SetActive(false);
					fUISoundManager.instance.StopSound("ui_pve_shuijing_alarm", 0);
				}
			}
			else
			{
				bool flag8 = Time.time - this.tNpcHurtTime > 3f && this.pNpcHurtTiShi.activeSelf;
				if (flag8)
				{
					this.pNpcHurtTiShi.SetActive(false);
					fUISoundManager.instance.StopSound("ui_pve_shuijing_alarm", 0);
				}
			}
		}
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x000D1F69 File Offset: 0x000D0169
	public void OnLianShaEffect(int nKillCount)
	{
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x000D1F6C File Offset: 0x000D016C
	public void OnHideBossHp()
	{
		this.pBossHpPanel.SetActive(false);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x000D1F7C File Offset: 0x000D017C
	public void OnKingDead(bool isshow)
	{
		bool flag = MatchManager.GetInstance().m_nMatchType == 3;
		if (flag)
		{
			this.pReviveUI.SetActive(isshow);
			bool flag2 = !isshow;
			if (flag2)
			{
				this.fDeadTime = 0f;
			}
			else
			{
				this.pReviveUI.SetActive(true);
				this.pReviveUI.transform.FindChild("Text (4)").GetComponent<Text>().text = string.Empty;
				this.UpdateRevive();
				this.pReviveUI.transform.FindChild("Text (1)").GetComponent<Text>().text = Language.GetLang(26005, new object[]
				{
					1
				});
				this.OnClearCheck();
			}
		}
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x000D203C File Offset: 0x000D023C
	public void OnSetDeadCd(int nCurCdTime)
	{
		this.tDeadCdText.text = nCurCdTime.ToString() + "S";
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000D205C File Offset: 0x000D025C
	public void OnSetFillAmount(int nMaxCd)
	{
		bool flag = KingTrainManager.GetInstance().GetPveUserDeahState() == 1;
		if (flag)
		{
			this.tDeadTiao.fillAmount = 1f;
			this.fDeadTime = 0f;
		}
		else
		{
			bool flag2 = this.fDeadTime == 0f;
			if (flag2)
			{
				this.fDeadTime = Time.time;
			}
			float fillAmount = ((float)nMaxCd + this.fDeadTime - Time.time) / (float)nMaxCd;
			this.tDeadTiao.fillAmount = fillAmount;
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000D20E0 File Offset: 0x000D02E0
	public void UpdateRevive()
	{
		bool activeSelf = this.pReviveUI.activeSelf;
		if (activeSelf)
		{
			Text component = this.pReviveUI.transform.FindChild("Text (2)").GetComponent<Text>();
			component.text = Language.GetLang(26006, new object[]
			{
				PlayerDataManager.getInstance().playerdet.m_nReviveMoney
			});
			bool flag = PlayerDataManager.getInstance().playerdet.m_nReviveMoney >= 1;
			if (flag)
			{
				this.pReviveUI.transform.FindChild("Text (3)").GetComponent<Text>().text = Language.GetLang(26007, new object[]
				{
					"X"
				});
			}
			else
			{
				this.pReviveUI.transform.FindChild("Text (3)").GetComponent<Text>().text = Language.GetLang(26050, new object[]
				{
					"X"
				});
			}
		}
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000D21DC File Offset: 0x000D03DC
	public void OnRevive()
	{
		bool flag = MatchManager.GetInstance().m_nMatchType == 3 && KingTrainManager.GetInstance().m_pKingGame.m_nGameState == 1 && fGameManager.Heroplayer.isDead;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().playerdet.m_nReviveMoney < 1;
			if (flag2)
			{
				Text component = this.pReviveUI.transform.FindChild("Text (4)").GetComponent<Text>();
				this.OnBuyItem(1029);
			}
			else
			{
				PveManager.GetInstance().OnSendMsgToServer(19503, new ArrayList
				{
					0,
					0
				});
			}
		}
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000D2290 File Offset: 0x000D0490
	public void OnBuyItem(int nShopId)
	{
		bool flag = fGameManager.mloadConfig.AllShopData.ContainsKey(nShopId);
		if (flag)
		{
			HeroPlayer.isMouseShow = true;
			ShopData pShopData = fGameManager.mloadConfig.AllShopData[nShopId];
			ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
		}
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000D22D8 File Offset: 0x000D04D8
	public void onGameData(KingGameInfo pGame)
	{
		int nGameState = pGame.m_nGameState;
		PveRewardData pveRewardData = fGameManager.mloadConfig.AllPveRewardData[fGameManager.GetInstance().nNowScnID];
		FbXmlData fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
		FbModelXmlData fbModelXmlData = fGameManager.mloadConfig.FbModeData[fbXmlData.nstartplotid];
		this.tFbName.text = fbXmlData.sName;
		this.tFbNanDu.text = pveRewardData.sName;
		this.OnFightReady(pGame);
		this.OnUpdateMonsterNum(pGame.m_nMonsterNum);
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000D2374 File Offset: 0x000D0574
	public void ShowHighEffTip()
	{
		bool enabled = this.highEffImg.enabled;
		if (enabled)
		{
			this.tRoundTiShi.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
			this.tRoundTiShi.gameObject.SetActive(true);
			this.tRoundTiShi.text = "<size=40><color=#FF9600>" + Language.GetLang(26068, new object[0]) + "</color></size>";
			base.StartCoroutine(this.Wait_DOScale(this.tRoundTiShi.gameObject, 10f, 0.5f));
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000D2420 File Offset: 0x000D0620
	private void OnShowRoundReward(int nRound, int nMaxRound)
	{
		bool flag = KingTrainManager.GetInstance().m_pKingGame.m_nGameType == 12 || !fGameManager.mloadConfig.AllPveRewardData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		if (!flag)
		{
			PveRewardData pveRewardData = fGameManager.mloadConfig.AllPveRewardData[fGameManager.GetInstance().nNowScnID];
			ArrayList nextRoundItem = pveRewardData.GetNextRoundItem(nRound, nMaxRound);
			ArrayList arrayList = (ArrayList)nextRoundItem[0];
			int num = (int)nextRoundItem[1];
			bool flag2 = arrayList.Count < 1;
			if (flag2)
			{
				this.pDropPanel.SetActive(false);
				fUIManager.OnClearObj(this.AllDropItem);
			}
			else
			{
				bool flag3 = num != this.m_nRewardRound;
				if (flag3)
				{
					bool flag4 = !this.highEffImg.enabled;
					if (flag4)
					{
						this.pDropPanel.SetActive(true);
					}
					this.pDropText.text = Language.GetLang(26049, new object[]
					{
						num
					});
					this.m_nRewardRound = num;
					this.OnCreateRewardItem(arrayList);
				}
			}
		}
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000D2540 File Offset: 0x000D0740
	public void OnCreateRewardItem(ArrayList szItem)
	{
		int count = szItem.Count;
		fUIManager.OnClearObj(this.AllDropItem);
		for (int i = 0; i < count; i += 2)
		{
			int key = (int)szItem[i];
			int num = (int)szItem[i + 1];
			bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(key);
			if (flag)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pDropOneItem);
				gameObject.transform.SetParent(this.pDropItemList.transform);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.FindChild("Text").GetComponent<Text>().text = string.Concat(new string[]
				{
					"<color=",
					Fun.GetQualColor(itemData.m_nQuality),
					">",
					itemData.m_sName,
					"*",
					szItem[i + 1].ToString(),
					"</color>"
				});
				RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
				string format = "pveitemico/{0}.txt";
				fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sPveico), component);
				this.AllDropItem.Add(gameObject);
			}
		}
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000D26E0 File Offset: 0x000D08E0
	public void OnDaoJiShi()
	{
		bool flag = KingTrainManager.GetInstance().m_pKingGame.m_nFightState == 0;
		string sEndTime;
		int num;
		if (flag)
		{
			sEndTime = KingTrainManager.GetInstance().m_pKingGame.m_sReadyEndTime;
			this.Prepare_Describe.SetActive(true);
			num = Fun.GetEndCDTime(sEndTime);
			bool flag2 = num < 6;
			if (flag2)
			{
				this.Prepare_Describe.GetComponent<Text>().text = Language.GetLang(26003, new object[0]);
			}
			else
			{
				this.Prepare_Describe.GetComponent<Text>().text = Language.GetLang(26003, new object[0]) + "\n" + Fun.sencondsToHMS_CXP(num);
			}
			this.Display_Countdown_Describe(num);
			bool flag3 = num == 5;
			if (flag3)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[878];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			}
		}
		else
		{
			this.Prepare_Describe.SetActive(false);
		}
		bool flag4 = KingTrainManager.GetInstance().m_pKingGame.m_nAllUserDeadState == 1;
		if (flag4)
		{
			sEndTime = KingTrainManager.GetInstance().m_pKingGame.m_sAllUserDeadTime;
			num = Fun.GetEndCDTime(sEndTime);
			bool flag5 = num <= 0;
			if (flag5)
			{
				num = 0;
			}
			this.tAllDeadTime.text = Fun.sencondsToHMS_CXP(num);
		}
		sEndTime = KingTrainManager.GetInstance().m_pKingGame.m_sGameEndTime;
		num = Fun.GetEndCDTime(sEndTime);
		bool flag6 = num <= 15;
		if (flag6)
		{
			this.Remaining_Time.transform.localScale = new Vector3(1f, 1f, 1f);
			this.Remaining_Time.transform.DOScale(new Vector3(2f, 2f, 2f), 0.8f);
			this.Remaining_Time.text = "<color=#FF0000>" + Fun.sencondsToHMS_CXP(num) + "</color>";
		}
		else
		{
			this.Remaining_Time.transform.localScale = new Vector3(1f, 1f, 1f);
			this.Remaining_Time.text = Fun.sencondsToHMS_CXP(num);
		}
		bool flag7 = num <= 0;
		if (flag7)
		{
			this.Remaining_Time.transform.localScale = new Vector3(1f, 1f, 1f);
			base.CancelInvoke("OnDaoJiShi");
		}
		num = KingTrainManager.GetInstance().GetXing();
		bool flag8 = this.nOldStar == num;
		if (!flag8)
		{
			this.nOldStar = num;
			for (int i = 0; i < this.szStarImg.Count; i++)
			{
				bool flag9 = i < num;
				if (flag9)
				{
					this.szStarImg[i].sprite = Window.spriteName(AtlasType.ATLAS_KING, "xing");
				}
				else
				{
					this.szStarImg[i].sprite = Window.spriteName(AtlasType.ATLAS_KING, "xing1");
				}
			}
		}
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000D29E8 File Offset: 0x000D0BE8
	private void Start()
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (flag)
		{
			this.onGameData(KingTrainManager.GetInstance().m_pKingGame);
		}
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x000D2A18 File Offset: 0x000D0C18
	public void Display_Countdown_Describe(int _Number)
	{
		bool flag = _Number > 0 && _Number <= 5;
		if (flag)
		{
			this.Countdown_Effect.transform.Find(_Number.ToString()).gameObject.SetActive(true);
		}
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x000D2A60 File Offset: 0x000D0C60
	public void OnInitCountdown_Effect()
	{
		this.Countdown_Effect.transform.Find("1").gameObject.SetActive(false);
		this.Countdown_Effect.transform.Find("2").gameObject.SetActive(false);
		this.Countdown_Effect.transform.Find("3").gameObject.SetActive(false);
		this.Countdown_Effect.transform.Find("4").gameObject.SetActive(false);
		this.Countdown_Effect.transform.Find("5").gameObject.SetActive(false);
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000D2B14 File Offset: 0x000D0D14
	public void OnFightStart(KingGameInfo pGame)
	{
		this.BossFightIng = false;
		bool flag = pGame.m_nGameType == 12;
		if (flag)
		{
			this.OnShowTimeRun();
		}
		else
		{
			fUISoundManager.instance.PlayUISound("ui_pve_battlestart", 0, null, 20);
			this.pDescribe.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
			this.pDescribe.gameObject.SetActive(true);
			this.pDescribe.text = Language.GetLang(26015, new object[]
			{
				pGame.m_nRound
			});
			this.OnInitCountdown_Effect();
			base.StartCoroutine(this.Wait_DOScale(this.pDescribe.gameObject, 1.5f, 2f));
			this.OnShowTimeRun();
		}
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000D2BF0 File Offset: 0x000D0DF0
	public void OnFightReady(KingGameInfo pGame)
	{
		this.tRoundText.text = pGame.m_nRound.ToString() + "/" + pGame.m_nMaxRound.ToString();
		this.OnShowRoundReward(pGame.m_nRound, pGame.m_nMaxRound);
		this.OnShowTimeRun();
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x000D2C44 File Offset: 0x000D0E44
	public void OnFightEnd(KingGameInfo pGame)
	{
		string text = string.Empty;
		bool flag = KingTrainManager.GetInstance().GetPveUserDeahState() != 1;
		if (flag)
		{
			this.tRoundTiShi.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
			this.tRoundTiShi.gameObject.SetActive(true);
			bool flag2 = pGame.m_nRoundIsOk == 0;
			if (flag2)
			{
				text = ((pGame.m_nNpcState != 1) ? Language.GetLang(26056, new object[0]) : Language.GetLang(26055, new object[0]));
				this.tRoundTiShi.text = text;
				base.StartCoroutine(this.Wait_DOScale(this.tRoundTiShi.gameObject, 3f, 0.5f));
			}
			else
			{
				this.tRoundTiShi.text = Language.GetLang(26013, new object[]
				{
					pGame.m_nRound
				}) + "\n" + Language.GetLang(26014, new object[]
				{
					pGame.m_nRoundScore
				});
				base.StartCoroutine(this.Wait_DOScale(this.tRoundTiShi.gameObject, 1.5f, 2f));
			}
		}
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000D2D90 File Offset: 0x000D0F90
	public void OnOpenDoor(ArrayList szOpenDoor, int OpenType = 1)
	{
		int count = szOpenDoor.Count;
		for (int i = 0; i < count; i++)
		{
			int phase = (int)szOpenDoor[i];
			Door_Control.Get_Instantiate.General_Door_Handle(phase, "close_open", false);
		}
		this.BossFightIng = false;
		bool flag = OpenType == 1 && count > 0;
		if (flag)
		{
			this.tRoundTiShi.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
			this.tRoundTiShi.gameObject.SetActive(true);
			this.tRoundTiShi.text = Language.GetLang(26057, new object[0]);
			base.StartCoroutine(this.Wait_DOScale(this.tRoundTiShi.gameObject, 5f, 0.5f));
		}
		else
		{
			bool flag2 = OpenType == 2 && KingTrainManager.GetInstance().m_pKingGame.m_nGameState != 1;
			if (flag2)
			{
				this.tRoundTiShi.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
				this.tRoundTiShi.gameObject.SetActive(true);
				this.tRoundTiShi.text = Language.GetLang(26059, new object[0]);
				base.StartCoroutine(this.Wait_DOScale(this.tRoundTiShi.gameObject, 10f, 0.5f));
			}
		}
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x000D2F10 File Offset: 0x000D1110
	private IEnumerator Wait_DOScale(GameObject Obj, float fWaitLong, float fDScaleDuration)
	{
		yield return new WaitForSeconds(fWaitLong);
		Obj.transform.DOScale(Vector3.zero, fDScaleDuration);
		yield break;
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000D2F34 File Offset: 0x000D1134
	public void OnShowTimeRun()
	{
		bool flag = base.IsInvoking("OnDaoJiShi");
		if (flag)
		{
			base.CancelInvoke("OnDaoJiShi");
		}
		base.InvokeRepeating("OnDaoJiShi", 0f, 1f);
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000D2F78 File Offset: 0x000D1178
	public void OnGameOver(ArrayList szData)
	{
		int nWin = (int)szData[1];
		this.BossFightIng = false;
		this.OnClearCheck();
		bool activeSelf = this.ShowObj.activeSelf;
		if (activeSelf)
		{
			this.ShowObj.SetActive(false);
		}
		base.StartCoroutine(this.Wait_GameOver_Fight(nWin));
		fUISoundManager.instance.StopSound("ui_pve_shuijing_alarm", 0);
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000D2FDE File Offset: 0x000D11DE
	private IEnumerator Wait_GameOver_Fight(int nWin)
	{
		yield return new WaitForSeconds(2f);
		this.Display_Fight_Result(nWin);
		yield break;
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x000D2FF4 File Offset: 0x000D11F4
	public void Display_Fight_Result(int nWin)
	{
		GameObject gameObject = (nWin != 1) ? this.Failure_Effect : this.Victory_Effect;
		gameObject.SetActive(true);
		base.StartCoroutine(this.Wait_PveHidden_Fight_Result(gameObject));
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000D302D File Offset: 0x000D122D
	private IEnumerator Wait_PveHidden_Fight_Result(GameObject _Parent)
	{
		yield return new WaitForSeconds(3f);
		_Parent.SetActive(false);
		yield break;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000D3044 File Offset: 0x000D1244
	public void OnUpdateMonsterNum(int nNum)
	{
		bool flag = nNum < 0;
		if (flag)
		{
			nNum = 0;
		}
		bool flag2 = KingTrainManager.GetInstance().m_pKingGame.m_nGameType == 9;
		if (flag2)
		{
			this.tMonsterNum.text = nNum.ToString();
		}
		else
		{
			this.tHeiAnMonsterNum.text = Language.GetLang(26058, new object[]
			{
				nNum
			});
		}
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x000D30B4 File Offset: 0x000D12B4
	public void OnShowAllUser(Dictionary<int, ArrayList> AllKingUserData)
	{
		foreach (ArrayList arrayList in AllKingUserData.Values)
		{
			bool flag = arrayList != null;
			if (flag)
			{
				this.OnAddUser(arrayList);
			}
		}
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000D3118 File Offset: 0x000D1318
	public void OnAddUser(ArrayList szInfo)
	{
		int key = (int)szInfo[0];
		bool flag = !this.m_szAllUserObj.ContainsKey(key);
		KingUserInfoCon kingUserInfoCon;
		if (flag)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneUserInfo);
			gameObject.transform.SetParent(this.pUserListPanel.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_szAllUserObj.Add(key, gameObject);
			kingUserInfoCon = gameObject.AddComponent<KingUserInfoCon>();
		}
		else
		{
			GameObject gameObject2 = this.m_szAllUserObj[key];
			kingUserInfoCon = gameObject2.GetComponent<KingUserInfoCon>();
		}
		kingUserInfoCon.InitData(szInfo);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000D31DC File Offset: 0x000D13DC
	public void OnDeleteUser(int nCid)
	{
		bool flag = this.m_szAllUserObj.ContainsKey(nCid);
		if (flag)
		{
			GameObject obj = this.m_szAllUserObj[nCid];
			this.m_szAllUserObj.Remove(nCid);
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x000D3220 File Offset: 0x000D1420
	private void UpPos()
	{
		Vector3 zero = Vector3.zero;
		bool flag = fGameManager.Heroplayer.isDead || KingTrainManager.GetInstance().m_pKingGame.m_nGameType != 12 || KingTrainManager.GetInstance().m_pKingGame.m_nGameState != 1;
		if (!flag)
		{
			bool flag2 = !this.BossFightIng;
			if (flag2)
			{
				bool flag3 = base.IsInvoking("onCheckFanWeiAddBuff");
				if (flag3)
				{
					base.CancelInvoke("onCheckFanWeiAddBuff");
				}
				bool activeSelf = this.tChengFaTxt.gameObject.activeSelf;
				if (activeSelf)
				{
					this.tChengFaTxt.gameObject.SetActive(false);
				}
				bool activeSelf2 = this.ShowObj.activeSelf;
				if (activeSelf2)
				{
					this.ShowObj.SetActive(false);
				}
			}
			else
			{
				bool flag4 = !fGameManager.mloadConfig.AllPveRewardData.ContainsKey(fGameManager.GetInstance().nNowScnID);
				if (!flag4)
				{
					PveRewardData pveRewardData = fGameManager.mloadConfig.AllPveRewardData[fGameManager.GetInstance().nNowScnID];
					ArrayList checkFanWei = pveRewardData.GetCheckFanWei(KingTrainManager.GetInstance().m_pKingGame.m_nRound);
					bool flag5 = checkFanWei.Count <= 0;
					if (!flag5)
					{
						bool flag6 = !this.ShowObj.activeSelf;
						if (flag6)
						{
							this.ShowObj.SetActive(true);
						}
						zero = new Vector3((float)((int)checkFanWei[1]) / 100f, (float)((int)checkFanWei[3]) / 100f, (float)((int)checkFanWei[2]) / 100f);
						bool flag7 = Time.time - this.waiteTime > 0.5f;
						if (flag7)
						{
							float magnitude = (zero - fGameManager.Heroplayer.defaultMesh.transform.position).magnitude;
							bool flag8 = magnitude > (float)((int)checkFanWei[4]);
							if (flag8)
							{
								bool flag9 = !base.IsInvoking("onCheckFanWeiAddBuff");
								if (flag9)
								{
									this.tChengFaTxt.gameObject.SetActive(true);
									this.m_sExitFightTime = Fun.GetServerDate().ToString();
									this.tChengFaTxt.text = Language.GetLang(26066, new object[]
									{
										5
									});
									base.InvokeRepeating("onCheckFanWeiAddBuff", 0f, 1f);
								}
							}
							else
							{
								this.OnClearCheck();
							}
							this.waiteTime = Time.time;
						}
						this.f = (float)Screen.width / SysConfig.ScreenWidth;
						this.position = CameraControl.Instance.controlCamera.WorldToScreenPoint(zero);
						this.fCx = this.position.x / this.fScreenW;
						this.fCy = this.position.y / this.fScreenH;
						this.fx = this.vSize.x * this.fCx - this.vSize.x / 2f;
						this.fy = this.vSize.y * this.fCy - this.vSize.y / 2f;
						this.szReturn.x = this.fx;
						this.szReturn.y = this.fy;
						this.szReturn.z = 0f;
						bool flag10 = this.position.z < 0f;
						if (flag10)
						{
							this.nChaoXiang = 1;
						}
						else
						{
							this.nChaoXiang = 0;
						}
						this.position = new Vector2((this.position.x - (float)(Screen.width / 2)) * this.f, (this.position.y - (float)(Screen.height / 2)) * this.f);
						this.fn_x = Mathf.Abs(this.position.x);
						bool flag11 = this.fn_x > (float)(Screen.width / 2);
						if (flag11)
						{
							bool flag12 = this.position.x < 0f;
							if (flag12)
							{
								this.position.x = (float)(-(float)Screen.width / 2);
							}
							else
							{
								this.position.x = (float)(Screen.width / 2);
							}
							bool flag13 = !this.jiantou.activeSelf;
							if (flag13)
							{
								this.jiantou.SetActive(true);
							}
						}
						else
						{
							bool activeSelf3 = this.jiantou.activeSelf;
							if (activeSelf3)
							{
								this.jiantou.SetActive(false);
							}
						}
						bool flag14 = this.nChaoXiang == 1;
						if (flag14)
						{
							bool flag15 = !this.jiantou.activeSelf;
							if (flag15)
							{
								this.jiantou.SetActive(true);
							}
							this.position = new Vector2(this.position.x, 0f);
							bool flag16 = this.position.x > 0f;
							if (flag16)
							{
								this.position.x = (float)(-(float)Screen.width / 2);
							}
							else
							{
								this.position.x = (float)(Screen.width / 2);
							}
						}
						this.fn_y = Mathf.Abs(this.position.y);
						bool flag17 = this.fn_y >= (float)(Screen.height / 2);
						if (flag17)
						{
							bool flag18 = this.position.y < 0f;
							if (flag18)
							{
								this.position.y = (float)(-(float)Screen.height / 2);
							}
							else
							{
								this.position.y = (float)(Screen.height / 2);
							}
							this.rect.localPosition = new Vector3(this.position.x, this.position.y, 0f);
						}
						else
						{
							this.rect.localPosition = new Vector3(this.position.x, this.szReturn.y, 0f);
						}
						this.q1 = Quaternion.Euler(0f, 0f, 0f);
						bool flag19 = this.rect.localPosition.x > 0f;
						if (flag19)
						{
							bool flag20 = this.rect.localPosition.x <= 200f;
							if (flag20)
							{
								this.q1 = Quaternion.Euler(0f, 0f, 55f);
							}
							else
							{
								this.q1 = Quaternion.Euler(0f, 0f, 90f);
							}
						}
						bool flag21 = this.position.x < 0f;
						if (flag21)
						{
							bool flag22 = this.rect.localPosition.x >= -200f;
							if (flag22)
							{
								this.q1 = Quaternion.Euler(0f, 0f, 55f);
							}
							else
							{
								this.q1 = Quaternion.Euler(0f, 0f, 0f);
							}
						}
						bool flag23 = this.position.x == 0f;
						if (flag23)
						{
							this.q1 = Quaternion.Euler(0f, 0f, 55f);
						}
						this.jiantou.transform.localRotation = this.q1;
					}
				}
			}
		}
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x000D3980 File Offset: 0x000D1B80
	private void onCheckFanWeiAddBuff()
	{
		int endCDTime = Fun.GetEndCDTime(this.m_sExitFightTime, 6);
		bool flag = endCDTime == 0;
		if (flag)
		{
			base.CancelInvoke("onCheckFanWeiAddBuff");
			bool bossFightIng = this.BossFightIng;
			if (bossFightIng)
			{
				fGameManager.Heroplayer.SendMessageToServer(19101, new ArrayList
				{
					0,
					0
				});
			}
		}
		else
		{
			this.tChengFaTxt.text = Language.GetLang(26066, new object[]
			{
				endCDTime
			});
		}
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000D3A18 File Offset: 0x000D1C18
	private void OnClearCheck()
	{
		this.tChengFaTxt.gameObject.SetActive(false);
		bool flag = base.IsInvoking("onCheckFanWeiAddBuff");
		if (flag)
		{
			base.CancelInvoke("onCheckFanWeiAddBuff");
		}
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000D3A58 File Offset: 0x000D1C58
	public void OnUpdatePaiXu()
	{
		ArrayList arrayList = KingTrainManager.GetInstance().DictonarySort();
		int count = arrayList.Count;
		bool flag = this.m_szAllUserObj.Count >= 1;
		if (flag)
		{
			Dictionary<int, ArrayList> allKingUserData = KingTrainManager.GetInstance().AllKingUserData;
			for (int i = 0; i < count; i++)
			{
				int key = (int)arrayList[i];
				GameObject gameObject = this.m_szAllUserObj[key];
				gameObject.transform.SetSiblingIndex(i);
			}
		}
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000D3ADC File Offset: 0x000D1CDC
	public void OnAllUserDead(int nState)
	{
		bool flag = nState == 1;
		if (flag)
		{
			string sAllUserDeadTime = KingTrainManager.GetInstance().m_pKingGame.m_sAllUserDeadTime;
			int num = Fun.GetEndCDTime(sAllUserDeadTime);
			bool flag2 = num <= 0;
			if (flag2)
			{
				num = 0;
			}
			this.tAllDeadTime.text = Fun.sencondsToHMS_CXP(num);
			this.tAllDeadTiShi.gameObject.SetActive(true);
		}
		else
		{
			this.tAllDeadTiShi.gameObject.SetActive(false);
		}
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000D3B54 File Offset: 0x000D1D54
	private void Update()
	{
		this.UpPos();
		bool flag = Time.time - this.m_fShowTime > 3f && this.pBossHpPanel.activeSelf;
		if (flag)
		{
			this.pBossHpPanel.SetActive(false);
		}
		this.OnShowNpcHaveDamage();
		bool keyDown = Input.GetKeyDown(KeyCode.X);
		if (keyDown)
		{
			this.OnRevive();
		}
		bool flag2 = MatchManager.GetInstance().m_nMatchType == 3 && !fGameManager.Heroplayer.isDead && this.pReviveUI.activeSelf;
		if (flag2)
		{
			this.OnKingDead(false);
			this.OnAllUserDead(0);
		}
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x000D3BF5 File Offset: 0x000D1DF5
	private void ExitHighEff(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000D3C03 File Offset: 0x000D1E03
	private void OverHighEff(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(26071, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x040011A9 RID: 4521
	public static KingCon inst;

	// Token: 0x040011AA RID: 4522
	private Text tRoundText;

	// Token: 0x040011AB RID: 4523
	private Text tMonsterNum;

	// Token: 0x040011AC RID: 4524
	private Text Remaining_Time;

	// Token: 0x040011AD RID: 4525
	private GameObject Prepare_Describe;

	// Token: 0x040011AE RID: 4526
	private GameObject Countdown_Effect;

	// Token: 0x040011AF RID: 4527
	private Text pDescribe;

	// Token: 0x040011B0 RID: 4528
	private GameObject OneUserInfo;

	// Token: 0x040011B1 RID: 4529
	private GameObject pUserListPanel;

	// Token: 0x040011B2 RID: 4530
	private Text tRoundTiShi;

	// Token: 0x040011B3 RID: 4531
	private GameObject Victory_Effect;

	// Token: 0x040011B4 RID: 4532
	private GameObject Failure_Effect;

	// Token: 0x040011B5 RID: 4533
	private GameObject pReviveUI;

	// Token: 0x040011B6 RID: 4534
	private Text tKingFbName;

	// Token: 0x040011B7 RID: 4535
	private Text tKingFbNanDu;

	// Token: 0x040011B8 RID: 4536
	private Text tHeiAnFbName;

	// Token: 0x040011B9 RID: 4537
	private Text tHeiAnFbNanDu;

	// Token: 0x040011BA RID: 4538
	private GameObject pXingXingList;

	// Token: 0x040011BB RID: 4539
	public Dictionary<int, GameObject> m_szAllUserObj = new Dictionary<int, GameObject>();

	// Token: 0x040011BC RID: 4540
	private GameObject pBossHpPanel;

	// Token: 0x040011BD RID: 4541
	private Image mBossHp_Value_1;

	// Token: 0x040011BE RID: 4542
	private Image mBossHp_Value_2;

	// Token: 0x040011BF RID: 4543
	private Text tBossHp_RoundNum;

	// Token: 0x040011C0 RID: 4544
	private Text tBoss_Name;

	// Token: 0x040011C1 RID: 4545
	private Text tNpcHp;

	// Token: 0x040011C2 RID: 4546
	private Image tNpcValueImg;

	// Token: 0x040011C3 RID: 4547
	private int m_nNpcId;

	// Token: 0x040011C4 RID: 4548
	private Text tLianShaTxt;

	// Token: 0x040011C5 RID: 4549
	private Text tAllDeadTiShi;

	// Token: 0x040011C6 RID: 4550
	private Text tAllDeadTime;

	// Token: 0x040011C7 RID: 4551
	private GameObject pNpcHurtTiShi;

	// Token: 0x040011C8 RID: 4552
	private GameObject pDropPanel;

	// Token: 0x040011C9 RID: 4553
	private GameObject pDropItemList;

	// Token: 0x040011CA RID: 4554
	private GameObject pDropOneItem;

	// Token: 0x040011CB RID: 4555
	private Text pDropText;

	// Token: 0x040011CC RID: 4556
	private Text tNpcHurtTiShi;

	// Token: 0x040011CD RID: 4557
	private List<GameObject> AllDropItem = new List<GameObject>();

	// Token: 0x040011CE RID: 4558
	private int m_nRewardRound;

	// Token: 0x040011CF RID: 4559
	private Image tDeadTiao;

	// Token: 0x040011D0 RID: 4560
	public Text tDeadCdText;

	// Token: 0x040011D1 RID: 4561
	private GameObject pNpcHpPanel;

	// Token: 0x040011D2 RID: 4562
	private GameObject pKingPanel;

	// Token: 0x040011D3 RID: 4563
	private GameObject pHeiAnPanel;

	// Token: 0x040011D4 RID: 4564
	private Text tFbName;

	// Token: 0x040011D5 RID: 4565
	private Text tFbNanDu;

	// Token: 0x040011D6 RID: 4566
	private Text tHeiAnMonsterNum;

	// Token: 0x040011D7 RID: 4567
	private GameObject ShowObj;

	// Token: 0x040011D8 RID: 4568
	private GameObject jiantou;

	// Token: 0x040011D9 RID: 4569
	private RectTransform rect;

	// Token: 0x040011DA RID: 4570
	private bool BossFightIng;

	// Token: 0x040011DB RID: 4571
	private Text tChengFaTxt;

	// Token: 0x040011DC RID: 4572
	private List<Image> szStarImg = new List<Image>();

	// Token: 0x040011DD RID: 4573
	private int nOldStar;

	// Token: 0x040011DE RID: 4574
	private Image highEffImg;

	// Token: 0x040011DF RID: 4575
	private int m_nMaxRoundHp = 30000;

	// Token: 0x040011E0 RID: 4576
	private int m_nCurRound = -1;

	// Token: 0x040011E1 RID: 4577
	private int m_nBossId = -1;

	// Token: 0x040011E2 RID: 4578
	private float m_fShowTime;

	// Token: 0x040011E3 RID: 4579
	private int nNpcOldHp = -1;

	// Token: 0x040011E4 RID: 4580
	private float tNpcHurtTime;

	// Token: 0x040011E5 RID: 4581
	private NetLiving pNpc;

	// Token: 0x040011E6 RID: 4582
	private Tween twDOFill;

	// Token: 0x040011E7 RID: 4583
	private float fDeadTime;

	// Token: 0x040011E8 RID: 4584
	private int m_nTickTime;

	// Token: 0x040011E9 RID: 4585
	private float f;

	// Token: 0x040011EA RID: 4586
	private Vector3 position;

	// Token: 0x040011EB RID: 4587
	private float fn_x;

	// Token: 0x040011EC RID: 4588
	private float fn_y;

	// Token: 0x040011ED RID: 4589
	private Quaternion q1;

	// Token: 0x040011EE RID: 4590
	private int nChaoXiang;

	// Token: 0x040011EF RID: 4591
	private Vector2 vSize;

	// Token: 0x040011F0 RID: 4592
	private Vector3 szReturn = new Vector3(-1f, -1f, -1f);

	// Token: 0x040011F1 RID: 4593
	private float waiteTime;

	// Token: 0x040011F2 RID: 4594
	private float fScreenW = (float)Screen.width;

	// Token: 0x040011F3 RID: 4595
	private float fScreenH = (float)Screen.height;

	// Token: 0x040011F4 RID: 4596
	private float fCx;

	// Token: 0x040011F5 RID: 4597
	private float fCy;

	// Token: 0x040011F6 RID: 4598
	private float fx;

	// Token: 0x040011F7 RID: 4599
	private float fy;

	// Token: 0x040011F8 RID: 4600
	private string m_sExitFightTime = string.Empty;

	// Token: 0x040011F9 RID: 4601
	private int bossHp = 10000000;
}
