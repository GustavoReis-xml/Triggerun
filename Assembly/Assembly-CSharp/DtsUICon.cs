using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000109 RID: 265
public class DtsUICon : Window
{
	// Token: 0x0600095F RID: 2399 RVA: 0x00051788 File Offset: 0x0004F988
	protected override void Awake()
	{
		base.Awake();
		this.tPlayerNum = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.tTopMsg = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.oHp = this.UIGameObj.Get_GameObj(2);
		this.hpfill = this.oHp.GetComponent<Image>();
		this.djstext = this.oHp.transform.Find("value").GetComponent<Text>();
		this.djsusetext = this.oHp.transform.Find("Text").GetComponent<Text>();
		this.oHp.SetActive(false);
		this.oCloneItem = this.UIGameObj.Get_GameObj(3);
		this.oCloneItem.SetActive(false);
		this.oCloneItem.SetActive(false);
		this.oItemList = this.UIGameObj.Get_GameObj(4);
		this.oChangeHeroUI = this.UIGameObj.Get_GameObj(5);
		this.tChangeTopName = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.TopImg = this.UIGameObj.Get_GameObj(7).GetComponent<RawImage>();
		this.tHeroName = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.oCloneOneskill = this.UIGameObj.Get_GameObj(9);
		this.oCloneOneskill.SetActive(false);
		this.oSkillList = this.UIGameObj.Get_GameObj(10);
		this.tKeyText = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.oChangeHeroUI.SetActive(false);
		this.tCenterMsg = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.oDead = this.UIGameObj.Get_GameObj(13);
		this.tDeadMsg = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.tKillNum = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.oDead.SetActive(false);
		this.mJdtFill = this.UIGameObj.Get_GameObj(16).GetComponent<Image>();
		this.mJdtFill.fillAmount = 0f;
		this.tDqDjs = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.tDqDjs.text = string.Empty;
		this.oRen = this.UIGameObj.Get_GameObj(18);
		this.oRen.SetActive(false);
		this.oHpBoxObj = this.UIGameObj.Get_GameObj(21);
		this.oHpBoxJy = this.UIGameObj.Get_GameObj(22);
		this.hpboxraw = this.UIGameObj.Get_GameObj(23).GetComponent<RawImage>();
		this.tHpboxName = this.UIGameObj.Get_GameObj(24).GetComponent<Text>();
		this.oHpBoxEff = this.oHpBoxObj.transform.Find("ui_jiemianbuff").gameObject;
		this.oHpBoxEff.SetActive(false);
		this.oHpNumObj = this.UIGameObj.Get_GameObj(25);
		this.tHpNum = this.UIGameObj.Get_GameObj(26).GetComponent<Text>();
		this.oHpBoxObj.SetActive(false);
		this.DtsMapObj = this.UIGameObj.Get_GameObj(27);
		this.DtsMapUICon = this.DtsMapObj.GetComponent<DtsMapUICon>();
		bool flag = this.DtsMapUICon == null;
		if (flag)
		{
			this.DtsMapUICon = this.DtsMapObj.AddComponent<DtsMapUICon>();
		}
		this.DtsResultUI = this.UIGameObj.Get_GameObj(28);
		this.DtsResultTop = this.UIGameObj.Get_GameObj(29);
		this.nametext = this.DtsResultTop.transform.Find("nametext").GetComponent<Text>();
		this.mylevel = this.DtsResultTop.transform.Find("level").GetComponent<Text>();
		this.expfilled = this.DtsResultTop.transform.Find("exp/expfilled").GetComponent<Image>();
		this.myHead = this.DtsResultTop.transform.Find("head").GetComponent<RawImage>();
		this.addico = this.DtsResultTop.transform.Find("addico").gameObject;
		this.addico.GetComponent<Image>().enabled = true;
		this.addico.SetActive(false);
		this.addexptext = this.DtsResultTop.transform.Find("addexptext").GetComponent<Text>();
		this.addgoldtext = this.DtsResultTop.transform.Find("addgoldtext").GetComponent<Text>();
		this.expvalue = this.DtsResultTop.transform.Find("expvalue").GetComponent<Text>();
		this.addGoldTxt = this.DtsResultTop.transform.Find("Addgold").GetComponent<Text>();
		this.tCloseText = this.DtsResultTop.transform.Find("xycbssj").GetComponent<Text>();
		this.ui_shenjitiao = this.UIGameObj.Get_GameObj(30);
		this.ui_shenjitiao.SetActive(false);
		this.tRanktext = this.UIGameObj.Get_GameObj(31).GetComponent<Text>();
		this.tResultMsg = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.tNowrank = this.UIGameObj.Get_GameObj(33).GetComponent<Text>();
		this.tKillnum = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.oReward = this.UIGameObj.Get_GameObj(35).gameObject;
		this.tJltext = this.UIGameObj.Get_GameObj(36).GetComponent<Text>();
		this.pJlpic = this.UIGameObj.Get_GameObj(37).GetComponent<Image>();
		this.tRawerdtext = this.UIGameObj.Get_GameObj(45).GetComponent<Text>();
		this.oFristReward = this.UIGameObj.Get_GameObj(46).gameObject;
		this.tFristJltext = this.UIGameObj.Get_GameObj(47).GetComponent<Text>();
		this.pFristJlpic = this.UIGameObj.Get_GameObj(48).GetComponent<Image>();
		this.tFristRawerdtext = this.UIGameObj.Get_GameObj(49).GetComponent<Text>();
		this.MaxNumText = this.UIGameObj.Get_GameObj(50).GetComponent<Text>();
		this.returngame = this.UIGameObj.Get_GameObj(38);
		EventTriggerListener.Get(this.returngame).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseUi);
		this.ui_jishi = this.UIGameObj.Get_GameObj(39);
		this.AddDjsList();
		this.ui_jishi.SetActive(false);
		this.oJdt = this.UIGameObj.Get_GameObj(40);
		this.oJdt.SetActive(false);
		this.zd_buff_cj = this.UIGameObj.Get_GameObj(41);
		this.zd_buff_cj.SetActive(false);
		this.tTopKillNum = this.UIGameObj.Get_GameObj(42).GetComponent<Text>();
		this.tTopKillNum.text = string.Empty;
		this.tKillName = this.UIGameObj.Get_GameObj(43).GetComponent<Text>();
		this.tKillName.text = string.Empty;
		this.DtsResultUI.SetActive(false);
		this.dzbox = this.UIGameObj.Get_GameObj(44);
		this.dzboxraw = this.dzbox.transform.Find("skill").GetComponent<RawImage>();
		this.dzboxText = this.dzbox.transform.Find("name").GetComponent<Text>();
		this.dzboxjy = this.dzbox.transform.Find("jy").gameObject;
		this.dzboxnumbg = this.dzbox.transform.Find("numbg").gameObject;
		this.dzboxnumText = this.dzboxnumbg.transform.Find("num").GetComponent<Text>();
		this.zboxEff = this.dzbox.transform.Find("ui_jiemianbuff").gameObject;
		this.dzbox.SetActive(false);
		DtsUICon.Inst = this;
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00051FBC File Offset: 0x000501BC
	public void OnUpdateKillNum(DtsUser dUser)
	{
		this.tTopKillNum.text = dUser.m_nKill.ToString();
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00051FD8 File Offset: 0x000501D8
	public void OnSetQuanStat(int nStat)
	{
		bool flag = nStat == 1;
		if (flag)
		{
			bool flag2 = !this.zd_buff_cj.activeSelf;
			if (flag2)
			{
				this.zd_buff_cj.SetActive(true);
			}
		}
		else
		{
			bool activeSelf = this.zd_buff_cj.activeSelf;
			if (activeSelf)
			{
				this.zd_buff_cj.SetActive(false);
			}
		}
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00052034 File Offset: 0x00050234
	public void OnSetJdt(bool mbool)
	{
		bool flag = this.oJdt.activeSelf != mbool;
		if (flag)
		{
			this.oJdt.SetActive(mbool);
		}
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00052068 File Offset: 0x00050268
	private void AddDjsList()
	{
		Transform transform = this.ui_jishi.transform.Find("Particle System");
		for (int i = 1; i <= 5; i++)
		{
			Transform transform2 = transform.transform.Find(i.ToString());
			bool flag = !(transform2 == null) && !this.szDjsGameObj.ContainsKey(i);
			if (flag)
			{
				transform2.gameObject.SetActive(false);
				this.szDjsGameObj.Add(i, transform2.gameObject);
			}
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x000520FC File Offset: 0x000502FC
	private void CheckJiShi(int nTime)
	{
		bool flag = nTime <= 5 && nTime > 0;
		if (flag)
		{
			bool flag2 = !this.ui_jishi.activeSelf;
			if (flag2)
			{
				this.ui_jishi.SetActive(true);
			}
			bool flag3 = this.szDjsGameObj.ContainsKey(nTime);
			if (flag3)
			{
				GameObject gameObject = this.szDjsGameObj[nTime];
				bool flag4 = gameObject != null;
				if (flag4)
				{
					gameObject.SetActive(true);
				}
			}
		}
		else
		{
			bool activeSelf = this.ui_jishi.activeSelf;
			if (activeSelf)
			{
				this.ui_jishi.SetActive(false);
			}
		}
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00052194 File Offset: 0x00050394
	private void Update()
	{
		bool flag = !(DtsUICon.Inst == null);
		if (flag)
		{
			this.OnCheckPickHeroUI();
			this.CheckGameTime();
			this.CheckKey();
			bool flag2 = this.fUsehpBox != 0f;
			if (flag2)
			{
				this.CheckHpDjs();
			}
			bool flag3 = this.fUseDzBox != 0f;
			if (flag3)
			{
				this.CheckDzDjs();
			}
			this.NewAddExpEffect();
			this.NewAddGoldEffect();
			this.CheckCloseUI();
			bool flag4 = this.fUsehpBox == 0f && this.fUseDzBox == 0f && this.oHp.activeSelf;
			if (flag4)
			{
				this.oHp.SetActive(false);
			}
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00052258 File Offset: 0x00050458
	private void OnCheckPickHeroUI()
	{
		bool flag = Time.time - this.fBoxCheck < 0.5f;
		if (!flag)
		{
			this.fBoxCheck = Time.time;
			HeroPlayer heroplayer = fGameManager.Heroplayer;
			bool flag2 = heroplayer == null || heroplayer.playerDtsItemObj == null;
			if (!flag2)
			{
				DtsMapItem playerDtsItemObj = heroplayer.playerDtsItemObj;
				bool flag3 = this.getIsLeave(heroplayer.playerCommon.pos, 2f, playerDtsItemObj) || heroplayer.isDead;
				if (flag3)
				{
					this.OpenChangeHeroUI(0);
					bool flag4 = playerDtsItemObj.m_nItmeType == 6 || playerDtsItemObj.m_nItmeType == 7;
					if (flag4)
					{
						DtsBatteManager.GetInstance().OnCheckItem(0, playerDtsItemObj.getNetid());
					}
					bool flag5 = playerDtsItemObj.m_nItmeType == 1 || playerDtsItemObj.m_nItmeType == 6;
					if (flag5)
					{
						fGameManager.Heroplayer.playerDtsItemObj = null;
					}
				}
			}
		}
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00052340 File Offset: 0x00050540
	private bool getIsLeave(Vector3 ve, float fleave, DtsMapItem thisItem)
	{
		bool result = false;
		bool flag = ve.x > thisItem.m_fX + fleave || ve.x < thisItem.m_fX - fleave || ve.z > thisItem.m_fZ + fleave || ve.z < thisItem.m_fZ - fleave;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x000523A0 File Offset: 0x000505A0
	private void CheckGameTime()
	{
		int nGameState = DtsBatteManager.GetInstance().m_nGameState;
		bool flag = nGameState < 1 || Time.time - this.fdCheck < 1f;
		if (!flag)
		{
			this.fdCheck = Time.time;
			DateTime serverDate = Fun.GetServerDate();
			DateTime dateTime = DateTime.Parse(MatchManager.GetInstance().m_sGameStartTime);
			string text = string.Empty;
			int num = nGameState;
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					bool flag2 = this._dDtsUser != null && this._dDtsUser.m_nSelectScnID == 0;
					if (flag2)
					{
						DateTime d = dateTime.AddSeconds((double)(DtsBatteManager.GetInstance().m_nDelayStartTime + DtsBatteManager.GetInstance().m_nDelayEnterScnTime));
						float num3 = (float)((int)(d - serverDate).TotalSeconds);
						bool flag3 = num3 > 0f;
						if (flag3)
						{
							text = Language.GetLang(29021, new object[]
							{
								num3
							});
							this.CheckJiShi((int)num3);
						}
					}
					else
					{
						text = string.Empty;
						this.CheckJiShi(0);
					}
				}
			}
			else
			{
				DateTime d2 = dateTime.AddSeconds((double)DtsBatteManager.GetInstance().m_nDelayStartTime);
				float num3 = (float)((int)(d2 - serverDate).TotalSeconds);
				bool flag4 = num3 > 0f;
				if (flag4)
				{
					text = Language.GetLang(29020, new object[]
					{
						num3
					});
				}
			}
			bool flag5 = text != string.Empty;
			if (flag5)
			{
				this.tTopMsg.text = text;
			}
			else
			{
				bool flag6 = this.tTopMsg.text != string.Empty;
				if (flag6)
				{
					this.tTopMsg.text = string.Empty;
				}
			}
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00052570 File Offset: 0x00050770
	public void showDtsUi(DtsUser dUser)
	{
		this._dDtsUser = dUser;
		this._NetLiving = fGameManager.GetInstance().getPlayer();
		this.tPlayerNum.text = DtsBatteManager.GetInstance().m_nLivePlayerNum.ToString();
		this.tTopKillNum.text = dUser.m_nKill.ToString();
		this.OnShowHpBox(dUser.m_nHpBox);
		this.AddItemObj("gongji_1", Language.GetLang(29005, new object[]
		{
			"+" + (dUser.m_nAttackLvl * 5).ToString() + "%"
		}), dUser.m_nAttackLvl);
		this.AddItemObj("fangyu_1", Language.GetLang(29006, new object[]
		{
			"+" + (dUser.m_nDefendLvl * 5).ToString() + "%"
		}), dUser.m_nDefendLvl);
		this.OnShowEnergyBox(dUser.m_nDaZhaoBox);
		bool flag = this.DtsMapUICon != null;
		if (flag)
		{
			this.DtsMapUICon.SetData(this._NetLiving, dUser);
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00052690 File Offset: 0x00050890
	public void SetDuQuanTimePos(float fMaxValue, float fNowValue)
	{
		bool flag = fMaxValue == 0f;
		if (flag)
		{
			this.mJdtFill.fillAmount = 0f;
		}
		else
		{
			bool flag2 = fNowValue > fMaxValue;
			if (flag2)
			{
				fNowValue = fMaxValue;
			}
			this.mJdtFill.fillAmount = fNowValue / fMaxValue;
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x000526DA File Offset: 0x000508DA
	public void OnUpdateDelayTime(string sMsg)
	{
		this.tDqDjs.text = sMsg;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x000526EC File Offset: 0x000508EC
	public void OnCenterMsg(int nType, int nTime, int nTemp)
	{
		string text = string.Empty;
		bool flag = Time.time - this.fCheck < 1f;
		if (!flag)
		{
			this.fCheck = Time.time;
			bool flag2 = nTime < 1 && nTemp == 0;
			if (flag2)
			{
				this.ClearMsg();
			}
			else
			{
				bool flag3 = this.nNowMsgType != nType;
				if (flag3)
				{
					this.nNowMsgType = nType;
					text = ((nTemp != 1) ? Language.GetLang(nType, new object[]
					{
						Fun.SecToFullTimeString(nTime)
					}) : Language.GetLang(nType, new object[0]));
					base.CancelInvoke("ClearMsg");
					base.Invoke("ClearMsg", 5f);
				}
				else
				{
					bool flag4 = nTime <= 60;
					if (flag4)
					{
						text = ((nTemp != 1) ? Language.GetLang(nType, new object[]
						{
							Fun.SecToFullTimeString(nTime)
						}) : Language.GetLang(nType, new object[0]));
					}
					else
					{
						bool flag5 = nTime % 60 == 0;
						if (flag5)
						{
							text = ((nTemp != 1) ? Language.GetLang(nType, new object[]
							{
								Fun.SecToFullTimeString(nTime)
							}) : Language.GetLang(nType, new object[0]));
							base.CancelInvoke("ClearMsg");
							base.Invoke("ClearMsg", 5f);
						}
					}
				}
				bool flag6 = text != string.Empty;
				if (flag6)
				{
					this.tCenterMsg.text = text;
				}
			}
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00052858 File Offset: 0x00050A58
	public void ClearMsg()
	{
		bool flag = base.IsInvoking("ClearMsg");
		if (flag)
		{
			base.CancelInvoke("ClearMsg");
		}
		this.tCenterMsg.text = string.Empty;
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00052894 File Offset: 0x00050A94
	private void OnShowEnergyBox(int nNum)
	{
		this.dzboxText.text = Language.GetLang(29060, new object[0]);
		fUIManager.GetInstance().GetIconAsset(string.Format("dtsitemico/{0}.txt", "djcnq_1"), this.dzboxraw);
		bool flag = nNum > 0;
		if (flag)
		{
			bool activeSelf = this.dzboxjy.activeSelf;
			if (activeSelf)
			{
				this.dzboxjy.SetActive(false);
			}
			bool flag2 = !this.dzboxnumbg.activeSelf;
			if (flag2)
			{
				this.dzboxnumbg.SetActive(true);
			}
			bool flag3 = nNum > 99;
			if (flag3)
			{
				nNum = 99;
			}
			this.dzboxnumText.text = nNum.ToString();
			this.dzboxraw.color = Fun.StrToColor("ffffff", 1f);
		}
		else
		{
			bool flag4 = !this.dzboxjy.activeSelf;
			if (flag4)
			{
				this.dzboxjy.SetActive(true);
			}
			this.dzboxraw.color = Fun.StrToColor("ff0000", 2f);
			bool activeSelf2 = this.dzboxnumbg.activeSelf;
			if (activeSelf2)
			{
				this.dzboxnumbg.SetActive(false);
			}
		}
		bool flag5 = !this.dzbox.activeSelf;
		if (flag5)
		{
			this.dzbox.SetActive(true);
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000529EC File Offset: 0x00050BEC
	private void OnShowHpBox(int nNum)
	{
		this.tHpboxName.text = Language.GetLang(29007, new object[0]);
		fUIManager.GetInstance().GetIconAsset(string.Format("dtsitemico/{0}.txt", "xuebao_1"), this.hpboxraw);
		bool flag = nNum > 0;
		if (flag)
		{
			bool activeSelf = this.oHpBoxJy.activeSelf;
			if (activeSelf)
			{
				this.oHpBoxJy.SetActive(false);
			}
			bool flag2 = !this.oHpNumObj.activeSelf;
			if (flag2)
			{
				this.oHpNumObj.SetActive(true);
			}
			bool flag3 = nNum > 99;
			if (flag3)
			{
				nNum = 99;
			}
			this.tHpNum.text = nNum.ToString();
			this.hpboxraw.color = Fun.StrToColor("ffffff", 1f);
		}
		else
		{
			bool flag4 = !this.oHpBoxJy.activeSelf;
			if (flag4)
			{
				this.oHpBoxJy.SetActive(true);
			}
			this.hpboxraw.color = Fun.StrToColor("ff0000", 2f);
			bool activeSelf2 = this.oHpNumObj.activeSelf;
			if (activeSelf2)
			{
				this.oHpNumObj.SetActive(false);
			}
		}
		bool flag5 = !this.oHpBoxObj.activeSelf;
		if (flag5)
		{
			this.oHpBoxObj.SetActive(true);
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00052B44 File Offset: 0x00050D44
	public void OnStartItemEff(int nItemType)
	{
		switch (nItemType)
		{
		case 3:
			this.oHpBoxEff.SetActive(false);
			this.oHpBoxEff.SetActive(true);
			break;
		case 4:
		{
			bool flag = this.oGjqEff != null;
			if (flag)
			{
				this.oGjqEff.SetActive(false);
				this.oGjqEff.SetActive(true);
			}
			break;
		}
		case 5:
		{
			bool flag2 = this.oFyqEff != null;
			if (flag2)
			{
				this.oFyqEff.SetActive(false);
				this.oFyqEff.SetActive(true);
			}
			break;
		}
		case 8:
			this.zboxEff.SetActive(false);
			this.zboxEff.SetActive(true);
			break;
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00052C14 File Offset: 0x00050E14
	private void AddItemObj(string sName, string sStr, int nLvl)
	{
		string text = string.Empty;
		Transform transform = this.oItemList.transform.Find(sName);
		bool flag = transform == null;
		GameObject gameObject;
		if (flag)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneItem);
			gameObject.transform.SetParent(this.oItemList.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			gameObject.name = sName;
			Transform transform2 = gameObject.transform.Find("ui_jiemianbuff");
			bool flag2 = transform2 != null;
			if (flag2)
			{
				bool flag3 = sName == "gongji_1";
				if (flag3)
				{
					this.oGjqEff = transform2.gameObject;
					this.oGjqEff.SetActive(false);
				}
				bool flag4 = sName == "fangyu_1";
				if (flag4)
				{
					this.oFyqEff = transform2.gameObject;
					this.oFyqEff.SetActive(false);
				}
			}
			RawImage component = gameObject.transform.Find("raw").GetComponent<RawImage>();
			fUIManager.GetInstance().GetIconAsset("dtsitemico/" + sName + ".txt", component);
		}
		else
		{
			gameObject = transform.gameObject;
		}
		bool flag5 = nLvl >= 10;
		if (flag5)
		{
			text = Language.GetLang(29044, new object[0]);
		}
		gameObject.transform.Find("ttext").GetComponent<Text>().text = text;
		gameObject.transform.Find("txt").GetComponent<Text>().text = sStr;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00052DCC File Offset: 0x00050FCC
	public void OpenChangeHeroUI(int nOpenType)
	{
		bool flag = nOpenType == 1;
		if (flag)
		{
			bool flag2 = !this.DtsResultUI.activeSelf;
			if (flag2)
			{
				bool flag3 = !this.oChangeHeroUI.activeSelf;
				if (flag3)
				{
					this.oChangeHeroUI.SetActive(true);
				}
				this.OnShowCheckHero();
			}
		}
		else
		{
			bool activeSelf = this.oChangeHeroUI.activeSelf;
			if (activeSelf)
			{
				this.OnCloseChangeHeroUI();
				this.oChangeHeroUI.SetActive(false);
			}
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00052E48 File Offset: 0x00051048
	public bool GetIsOpenChangeHero()
	{
		bool flag = false;
		bool flag2 = this.oChangeHeroUI == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			result = this.oChangeHeroUI.activeSelf;
		}
		return result;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00052E7C File Offset: 0x0005107C
	private void OnShowCheckHero()
	{
		int num = 0;
		HeroAllSkillData heroAllSkillData = null;
		string text = string.Empty;
		ArrayList szCanHeroSkill = new ArrayList();
		ArrayList arrayList = new ArrayList();
		Dictionary<int, OneHeroSkillData> mHeroSkillData = new Dictionary<int, OneHeroSkillData>();
		HeroPlayer heroplayer = fGameManager.Heroplayer;
		bool flag = heroplayer == null;
		if (!flag)
		{
			DtsMapItem playerDtsItemObj = heroplayer.playerDtsItemObj;
			bool flag2 = playerDtsItemObj == null;
			if (!flag2)
			{
				DtsUser userObj = DtsBatteManager.GetInstance().GetUserObj(fGameManager.Heroplayer.playerCommon.m_nCid);
				bool flag3 = userObj == null;
				if (!flag3)
				{
					bool flag4 = playerDtsItemObj.m_nItmeType == 1;
					if (flag4)
					{
						this.tChangeTopName.text = Language.GetLang(29008, new object[0]);
						num = playerDtsItemObj.m_nItmeIndex;
						szCanHeroSkill = userObj.m_szSkillList;
						bool flag5 = fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(num);
						if (flag5)
						{
							heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[num];
							mHeroSkillData = heroAllSkillData.mHeroSkillData;
						}
						this.OnPickNewHeroData(num, mHeroSkillData, szCanHeroSkill);
					}
					else
					{
						bool flag6 = playerDtsItemObj.m_nItmeType == 6;
						if (flag6)
						{
							arrayList = playerDtsItemObj.m_szItemData;
							bool flag7 = arrayList.Count > 0;
							if (flag7)
							{
								num = (int)arrayList[10];
								bool flag8 = fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(num);
								if (flag8)
								{
									heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[num];
								}
							}
							this.OnPickHeroLost(num, mHeroSkillData, playerDtsItemObj.m_szItemData);
						}
						else
						{
							bool flag9 = playerDtsItemObj.m_nItmeType == 7;
							if (flag9)
							{
								this.tChangeTopName.text = Language.GetLang(29008, new object[0]);
								arrayList = playerDtsItemObj.m_szItemData;
								bool flag10 = arrayList.Count > 0;
								if (flag10)
								{
									num = (int)arrayList[1];
								}
								bool flag11 = fGameManager.mloadConfig.HeroAllSkillXml.ContainsKey(num);
								if (flag11)
								{
									heroAllSkillData = fGameManager.mloadConfig.HeroAllSkillXml[num];
									mHeroSkillData = heroAllSkillData.mHeroSkillData;
								}
								this.OnPickNewHeroData(num, mHeroSkillData, szCanHeroSkill);
							}
						}
					}
					bool flag12 = heroAllSkillData != null;
					string sUrl;
					if (flag12)
					{
						text = heroAllSkillData.sName;
						this.tHeroName.text = "<color='#ff0000'>" + heroAllSkillData.sName + "</color>";
						sUrl = "SelectHeroUI/headbig/" + heroAllSkillData.sIco + ".txt";
					}
					else
					{
						this.tHeroName.text = string.Empty;
						sUrl = string.Format("SelectHeroUI/headbig/{0}.txt", "luomo");
					}
					fUIManager.GetInstance().GetIconAsset(sUrl, this.TopImg);
					bool flag13 = num == 2000 || text == string.Empty;
					if (flag13)
					{
						this.tKeyText.text = Language.GetLang(29019, new object[0]);
					}
					else
					{
						bool flag14 = !this.GetLockHeroStat(num);
						if (flag14)
						{
							string text2 = Language.GetLang(29018, new object[]
							{
								text
							});
							this.tKeyText.text = text2;
						}
						else
						{
							string key_Replace_Hero = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Replace_Hero;
							string text2 = "<color='#FFFF00'>" + Language.GetLang(29000, new object[]
							{
								key_Replace_Hero
							}) + "</color>";
							this.tKeyText.text = Language.GetLang(29002, new object[]
							{
								text2,
								text
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00053208 File Offset: 0x00051408
	private bool GetLockHeroStat(int nHero)
	{
		bool result = false;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nHero);
		if (flag)
		{
			OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nHero);
			bool flag2 = oneHeroData.nOpenState > 0;
			if (flag2)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00053254 File Offset: 0x00051454
	private void OnPickHeroLost(int nHero, Dictionary<int, OneHeroSkillData> mHeroSkillData, ArrayList szHeroData)
	{
		bool flag = szHeroData.Count < 1;
		if (!flag)
		{
			Color color = Color.white;
			string str = string.Empty;
			string text = string.Empty;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = (ArrayList)szHeroData[11];
			int num = (int)szHeroData[7];
			int num2 = (int)szHeroData[8];
			int num3 = (int)szHeroData[9];
			this.tChangeTopName.text = Language.GetLang(29009, new object[]
			{
				szHeroData[12]
			});
			int num4 = (int)szHeroData[13];
			bool flag2 = mHeroSkillData.Count > 0;
			if (flag2)
			{
				arrayList = new ArrayList(mHeroSkillData.Keys);
			}
			int num5 = arrayList.IndexOf(6);
			bool flag3 = num5 == -1;
			if (flag3)
			{
				arrayList.Add(6);
			}
			num5 = arrayList.IndexOf(7);
			bool flag4 = num5 == -1;
			if (flag4)
			{
				arrayList.Add(7);
			}
			num5 = arrayList.IndexOf(8);
			bool flag5 = num5 == -1;
			if (flag5)
			{
				arrayList.Add(8);
			}
			int count = arrayList.Count;
			for (int i = 0; i < 8; i++)
			{
				Transform transform = this.oSkillList.transform.Find(i.ToString());
				bool flag6 = transform == null;
				GameObject gameObject;
				if (flag6)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneOneskill);
					gameObject.transform.SetParent(this.oSkillList.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
					gameObject.name = i.ToString();
				}
				else
				{
					gameObject = transform.gameObject;
				}
				RawImage component = gameObject.transform.Find("skill").GetComponent<RawImage>();
				GameObject gameObject2 = gameObject.transform.Find("jy").gameObject;
				bool activeSelf = gameObject2.activeSelf;
				if (activeSelf)
				{
					gameObject2.SetActive(false);
				}
				Text component2 = gameObject.transform.Find("name").GetComponent<Text>();
				component.color = Fun.StrToColor("ffffff", 1f);
				color = Fun.StrToColor("ffbc00", 2f);
				component.enabled = false;
				component2.text = "-- --";
				component2.color = Fun.StrToColor("666666", 2f);
				bool flag7 = i >= count;
				if (!flag7)
				{
					int num6 = (int)arrayList[i];
					bool flag8 = num6 >= 5;
					if (flag8)
					{
						str = string.Empty;
						text = string.Empty;
						bool flag9 = num6 == 5;
						if (flag9)
						{
							str = "djcnq_1";
							bool flag10 = num4 > 0;
							if (flag10)
							{
								text = "*" + num4.ToString();
							}
						}
						bool flag11 = num6 == 6;
						if (flag11)
						{
							str = "xuebao_1";
							bool flag12 = num > 0;
							if (flag12)
							{
								text = "*" + num.ToString();
							}
						}
						bool flag13 = num6 == 7;
						if (flag13)
						{
							str = "gongji_1";
							bool flag14 = num2 > 0 && num2 <= 10;
							if (flag14)
							{
								text = "lv" + num2.ToString();
							}
						}
						bool flag15 = num6 == 8;
						if (flag15)
						{
							str = "fangyu_1";
							bool flag16 = num3 > 0 && num3 <= 10;
							if (flag16)
							{
								text = "lv" + num3.ToString();
							}
						}
						bool flag17 = text == string.Empty;
						if (flag17)
						{
							bool flag18 = !gameObject2.activeSelf;
							if (flag18)
							{
								gameObject2.SetActive(true);
							}
							component.color = Fun.StrToColor("ff0000", 2f);
							color = Fun.StrToColor("666666", 2f);
						}
						fUIManager.GetInstance().GetIconAsset("dtsitemico/" + str + ".txt", component);
						component2.text = this.GetSkillNameText(num6, text);
						component2.color = color;
					}
					else
					{
						OneHeroSkillData oneHeroSkillData = mHeroSkillData[num6];
						num5 = arrayList2.IndexOf(num6);
						bool flag19 = num5 == -1 && oneHeroSkillData.nIsOpen == 0;
						if (flag19)
						{
							bool flag20 = !gameObject2.activeSelf;
							if (flag20)
							{
								gameObject2.SetActive(true);
							}
							component.color = Fun.StrToColor("ff0000", 2f);
							color = Fun.StrToColor("666666", 2f);
						}
						fUIManager.GetInstance().GetIconAsset("dtsitemico/" + oneHeroSkillData.sSkillIco + "_1.txt", component);
						component2.text = this.GetSkillNameText(num6, string.Empty);
						component2.color = color;
					}
				}
			}
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00053798 File Offset: 0x00051998
	private void OnPickNewHeroData(int nHero, Dictionary<int, OneHeroSkillData> mHeroSkillData, ArrayList szCanHeroSkill)
	{
		Color color = Color.white;
		ArrayList arrayList = new ArrayList();
		bool flag = mHeroSkillData.Count > 0;
		if (flag)
		{
			arrayList = new ArrayList(mHeroSkillData.Keys);
		}
		int count = arrayList.Count;
		for (int i = 0; i < 8; i++)
		{
			Transform transform = this.oSkillList.transform.Find(i.ToString());
			bool flag2 = transform == null;
			GameObject gameObject;
			if (flag2)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneOneskill);
				gameObject.transform.SetParent(this.oSkillList.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = i.ToString();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			RawImage component = gameObject.transform.Find("skill").GetComponent<RawImage>();
			GameObject gameObject2 = gameObject.transform.Find("jy").gameObject;
			bool activeSelf = gameObject2.activeSelf;
			if (activeSelf)
			{
				gameObject2.SetActive(false);
			}
			Text component2 = gameObject.transform.Find("name").GetComponent<Text>();
			component.color = Fun.StrToColor("ffffff", 1f);
			color = Fun.StrToColor("ffbc00", 2f);
			component.enabled = false;
			component2.text = "-- --";
			component2.color = Fun.StrToColor("666666", 2f);
			bool flag3 = i >= count;
			if (!flag3)
			{
				int num = (int)arrayList[i];
				OneHeroSkillData oneHeroSkillData = mHeroSkillData[num];
				int num2 = szCanHeroSkill.IndexOf(num.ToString());
				int num3 = 0;
				bool flag4 = num2 != -1 && (int)szCanHeroSkill[num2 + 1] < 1;
				if (flag4)
				{
					num3 = 1;
				}
				bool flag5 = num3 == 1 || oneHeroSkillData.nIsOpen == 0;
				if (flag5)
				{
					bool flag6 = !gameObject2.activeSelf;
					if (flag6)
					{
						gameObject2.SetActive(true);
					}
					component.color = Fun.StrToColor("ff0000", 2f);
					color = Fun.StrToColor("666666", 2f);
				}
				fUIManager.GetInstance().GetIconAsset("dtsitemico/" + oneHeroSkillData.sSkillIco + "_1.txt", component);
				component2.text = this.GetSkillNameText(num, string.Empty);
				component2.color = color;
			}
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00053A5C File Offset: 0x00051C5C
	private string GetSkillNameText(int nType, string sValue)
	{
		string result = string.Empty;
		switch (nType)
		{
		case 1:
			result = Language.GetLang(29010, new object[0]);
			break;
		case 2:
			result = Language.GetLang(29011, new object[0]);
			break;
		case 3:
			result = Language.GetLang(29012, new object[0]);
			break;
		case 4:
			result = Language.GetLang(29013, new object[0]);
			break;
		case 5:
			result = Language.GetLang(29014, new object[]
			{
				sValue
			});
			break;
		case 6:
			result = Language.GetLang(29015, new object[]
			{
				sValue
			});
			break;
		case 7:
			result = Language.GetLang(29016, new object[]
			{
				sValue
			});
			break;
		case 8:
			result = Language.GetLang(29017, new object[]
			{
				sValue
			});
			break;
		}
		return result;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00053B54 File Offset: 0x00051D54
	private void OnCloseChangeHeroUI()
	{
		Fun.ClearChildObejct(this.oSkillList);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00053B64 File Offset: 0x00051D64
	private void CheckKey()
	{
		bool flag = Input_Manager.Get_Button_Down("Replace_Hero");
		if (flag)
		{
			bool flag2 = fGameManager.Heroplayer == null;
			if (flag2)
			{
				return;
			}
			DtsMapItem playerDtsItemObj = fGameManager.Heroplayer.playerDtsItemObj;
			bool flag3 = playerDtsItemObj == null;
			if (flag3)
			{
				return;
			}
			bool flag4 = this.GetLockHeroStat(playerDtsItemObj.m_nItmeIndex) && playerDtsItemObj.m_nItmeIndex != 2000;
			if (flag4)
			{
				DtsBatteManager.GetInstance().OnCheckItem(1, playerDtsItemObj.getNetid());
			}
			this.OpenChangeHeroUI(0);
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Alpha2);
		if (keyDown)
		{
			bool flag5 = this._NetLiving == null || this._NetLiving.haveDestroy || !this._NetLiving.playerHaveCreate || this._dDtsUser == null || this._NetLiving.playerCommon.m_nChenMo == 1 || this._NetLiving.playerCommon.m_nDongjie == 1 || this._NetLiving.playerCommon.m_nHunMi == 1 || ((HeroPlayer)this._NetLiving).wInput.notInput || this._NetLiving.charMove.notMove || this.fUsehpBox != 0f || gameuiCon.inst == null || Time.time - this.fCheckTime < 1f;
			if (flag5)
			{
				return;
			}
			this.fCheckTime = Time.time;
			bool flag6 = this._dDtsUser.m_nDaZhaoBox < 1;
			if (flag6)
			{
				gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29062, new object[0]));
				return;
			}
			bool flag7 = this._dDtsUser.m_nNowHeroID == 2000;
			if (flag7)
			{
				gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29045, new object[0]));
				return;
			}
			bool flag8 = this._NetLiving.playerDazhao.GetDaZhaoValue() == 1f || this._NetLiving.isDead;
			if (flag8)
			{
				bool flag9 = this._NetLiving.playerDazhao.GetDaZhaoValue() == 1f && gameuiCon.inst != null;
				if (flag9)
				{
					gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29061, new object[0]));
				}
				return;
			}
			bool flag10 = this.fUseDzBox != 0f;
			if (flag10)
			{
				return;
			}
			this.fUseDzBox = Time.time + (float)DtsBatteManager.GetInstance().m_nDzBosUseTime * 1f;
			this.OnUsesHpBox(1, 1);
		}
		bool flag11 = !Input.GetKeyDown(KeyCode.Alpha1) || this._NetLiving == null || this._NetLiving.haveDestroy || !this._NetLiving.playerHaveCreate || this._dDtsUser == null || this._NetLiving.playerCommon.m_nChenMo == 1 || this._NetLiving.playerCommon.m_nDongjie == 1 || this._NetLiving.playerCommon.m_nHunMi == 1 || ((HeroPlayer)this._NetLiving).wInput.notInput || this._NetLiving.charMove.notMove || this.fUseDzBox != 0f || gameuiCon.inst == null || Time.time - this.fCheckTime < 1f;
		if (!flag11)
		{
			this.fCheckTime = Time.time;
			bool flag12 = this._dDtsUser.m_nHpBox < 1;
			if (flag12)
			{
				gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29070, new object[0]));
			}
			else
			{
				bool flag13 = this._NetLiving.playerCommon.GetHpBiLi() == 1f || this._NetLiving.isDead;
				if (flag13)
				{
					bool flag14 = this._NetLiving.playerCommon.GetHpBiLi() == 1f && gameuiCon.inst != null;
					if (flag14)
					{
						gameuiCon.inst.mainMsgUI.AddGameMsg(Language.GetLang(29058, new object[0]));
					}
				}
				else
				{
					bool flag15 = this.fUsehpBox == 0f;
					if (flag15)
					{
						this.fUsehpBox = Time.time + (float)DtsBatteManager.GetInstance().m_nHpBosUseTime * 1f;
						this.OnUsesHpBox(1, 0);
					}
				}
			}
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00053FF8 File Offset: 0x000521F8
	private void OnUsesHpBox(int nType, int nItemType)
	{
		bool flag = this._NetLiving != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(19008);
			arrayList.Add(nType);
			arrayList.Add(nItemType);
			DtsBatteManager.GetInstance().OnSendMsgToServer(19602, arrayList);
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00054058 File Offset: 0x00052258
	public void OnSetHpTime(ArrayList szMsg)
	{
		int num = (int)szMsg[1];
		string text = string.Empty;
		bool activeSelf = this.oHp.activeSelf;
		if (activeSelf)
		{
			this.oHp.SetActive(false);
		}
		int num2 = num;
		int num3 = num2;
		if (num3 != 0)
		{
			if (num3 == 1)
			{
				text = szMsg[0].ToString();
				bool flag = !(text == string.Empty);
				if (flag)
				{
					string text2 = text;
					string a = text2;
					if (!(a == "0") && !(a == "-1") && !(a == "-2") && !(a == "-3"))
					{
						return;
					}
				}
				this.fUseDzBox = 0f;
			}
		}
		else
		{
			text = szMsg[0].ToString();
			bool flag2 = !(text == string.Empty);
			if (flag2)
			{
				string text3 = text;
				string a2 = text3;
				if (!(a2 == "0") && !(a2 == "-1") && !(a2 == "-2") && !(a2 == "-3"))
				{
					return;
				}
			}
			this.fUsehpBox = 0f;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00054198 File Offset: 0x00052398
	private void CheckDzDjs()
	{
		bool flag = this._NetLiving == null;
		if (!flag)
		{
			bool flag2 = this.fUseDzBox == 0f;
			if (flag2)
			{
				bool activeSelf = this.oHp.activeSelf;
				if (activeSelf)
				{
					this.oHp.SetActive(false);
				}
			}
			else
			{
				bool flag3 = Input_Manager.Get_Axis_Raw("Horizontal") != 0f || Input_Manager.Get_Axis_Raw("Vertical") != 0f || Input_Manager.Get_Axis("Jump") != 0f || Input_Manager.Get_Button_Down("Fight") || Input_Manager.Get_Button_Down("superskill") || Input_Manager.Get_Button_Down("WeaponSec") || Input_Manager.Get_Button_Down("Skill1") || Input_Manager.Get_Button_Down("Skill2") || Input_Manager.Get_Button_Down("WeaponFirst") || this._NetLiving.playerCommon.m_nChenMo == 1 || this._NetLiving.playerCommon.m_nDongjie == 1 || this._NetLiving.playerCommon.m_nHunMi == 1;
				if (flag3)
				{
					this.OnUsesHpBox(0, 1);
					this.fUseDzBox = 0f;
				}
				else
				{
					float num = (float)DtsBatteManager.GetInstance().m_nDzBosUseTime * 1f;
					float num2 = this.fUseDzBox - Time.time;
					bool flag4 = num2 > 0f;
					if (flag4)
					{
						this.hpfill.fillAmount = num2 / num;
						this.djstext.text = Mathf.CeilToInt(num2).ToString() + "s";
						bool flag5 = !this.oHp.activeSelf;
						if (flag5)
						{
							this.djsusetext.text = Language.GetLang(29063, new object[0]);
							this.oHp.SetActive(true);
						}
					}
					else
					{
						this.fUseDzBox = 0f;
						bool activeSelf2 = this.oHp.activeSelf;
						if (activeSelf2)
						{
							this.oHp.SetActive(false);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000543A4 File Offset: 0x000525A4
	private void CheckHpDjs()
	{
		bool flag = this._NetLiving == null;
		if (!flag)
		{
			bool flag2 = this.fUsehpBox == 0f;
			if (flag2)
			{
				bool activeSelf = this.oHp.activeSelf;
				if (activeSelf)
				{
					this.oHp.SetActive(false);
				}
			}
			else
			{
				bool flag3 = Input_Manager.Get_Axis_Raw("Horizontal") != 0f || Input_Manager.Get_Axis_Raw("Vertical") != 0f || Input_Manager.Get_Axis("Jump") != 0f || Input_Manager.Get_Button_Down("Fight") || Input_Manager.Get_Button_Down("superskill") || Input_Manager.Get_Button_Down("WeaponSec") || Input_Manager.Get_Button_Down("Skill1") || Input_Manager.Get_Button_Down("Skill2") || Input_Manager.Get_Button_Down("WeaponFirst") || this._NetLiving.playerCommon.m_nChenMo == 1 || this._NetLiving.playerCommon.m_nDongjie == 1 || this._NetLiving.playerCommon.m_nHunMi == 1;
				if (flag3)
				{
					this.OnUsesHpBox(0, 0);
					this.fUsehpBox = 0f;
				}
				else
				{
					float num = (float)DtsBatteManager.GetInstance().m_nHpBosUseTime * 1f;
					float num2 = this.fUsehpBox - Time.time;
					bool flag4 = num2 > 0f;
					if (flag4)
					{
						this.hpfill.fillAmount = num2 / num;
						this.djstext.text = Mathf.CeilToInt(num2).ToString() + "s";
						bool flag5 = !this.oHp.activeSelf;
						if (flag5)
						{
							this.djsusetext.text = Language.GetLang(29041, new object[0]);
							this.oHp.SetActive(true);
						}
					}
					else
					{
						this.fUsehpBox = 0f;
						bool activeSelf2 = this.oHp.activeSelf;
						if (activeSelf2)
						{
							this.oHp.SetActive(false);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x000545B0 File Offset: 0x000527B0
	public void OnStartGame(ArrayList szMsg)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		GameRoot.inst.Setting.Close_Setting_Option(null);
		bool flag = !HeroPlayer.isMouseShow;
		if (flag)
		{
			HeroPlayer.isPlayerControl = false;
			HeroPlayer.isMouseShow = true;
		}
		DtsMapItem playerDtsItemObj = fGameManager.Heroplayer.playerDtsItemObj;
		bool flag2 = playerDtsItemObj != null && (playerDtsItemObj.m_nItmeType == 6 || playerDtsItemObj.m_nItmeType == 7);
		if (flag2)
		{
			DtsBatteManager.GetInstance().OnCheckItem(0, playerDtsItemObj.getNetid());
		}
		fGameManager.Heroplayer.playerDtsItemObj = null;
		this.OpenChangeHeroUI(0);
		int nLevel = PlayerDataManager.getInstance().HUserData.m_nLevel;
		string sName = this._NetLiving.playerCommon.m_sName;
		this.nametext.text = sName;
		this.mylevel.text = string.Empty;
		bool flag3 = fGameManager.mloadConfig.AllFaceData.ContainsKey(this._NetLiving.playerCommon.m_nFace);
		if (flag3)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this._NetLiving.playerCommon.m_nFace];
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.myHead);
		}
		bool activeSelf = this.addico.activeSelf;
		if (activeSelf)
		{
			this.addico.SetActive(false);
		}
		this.addexptext.text = string.Empty;
		this.addgoldtext.text = string.Empty;
		this.addGoldTxt.text = string.Empty;
		this.nAddAllValue = (int)szMsg[2];
		this.nNowExp = (int)szMsg[3];
		int num = (int)szMsg[1];
		this.nNowLevel = nLevel - num;
		bool flag4 = this.nNowLevel < 1;
		if (flag4)
		{
			this.nNowLevel = 1;
		}
		bool flag5 = this.nNowLevel < 5 && num > 0;
		if (flag5)
		{
			this.nAddAllValue = Fun.GetUserLvlupExp(this.nNowLevel) - this.nNowExp + (this.nNowLevel + 1) * 50;
		}
		bool flag6 = this.nAddAllValue < 3;
		if (flag6)
		{
			this.nOnceAddValue = this.nAddAllValue;
		}
		else
		{
			this.nOnceAddValue = this.nAddAllValue / 3;
		}
		this.nSumValue = 0;
		this.OnShowValue(0);
		this.bCheck = true;
		int num2 = (int)szMsg[4];
		int num3 = (int)szMsg[5];
		this.tRanktext.text = Language.GetLang(29024, new object[]
		{
			num2,
			num3
		});
		bool flag7 = num2 == 1;
		if (flag7)
		{
			text = Language.GetLang(29025, new object[0]);
			HeroPlayer.isPlayerControl = false;
		}
		else
		{
			text = Language.GetLang(29036, new object[0]);
		}
		this.tResultMsg.text = text;
		this.tNowrank.text = Language.GetLang(29026, new object[]
		{
			num2
		});
		int num4 = (int)szMsg[6];
		this.tKillnum.text = Language.GetLang(29027, new object[]
		{
			num4
		});
		text2 = Fun.DecodingUTF8(szMsg[10].ToString());
		bool flag8 = text2 != string.Empty;
		if (flag8)
		{
			bool flag9 = sName != text2;
			if (flag9)
			{
				this.tKillName.text = Language.GetLang(29043, new object[]
				{
					text2
				});
			}
			else
			{
				this.tKillName.text = Language.GetLang(29059, new object[0]);
			}
		}
		int num5 = (int)szMsg[7];
		int num6 = (int)szMsg[8];
		bool flag10 = num6 > 0;
		if (flag10)
		{
			this.tJltext.text = Language.GetLang(29028, new object[0]);
			this.pJlpic.sprite = Window.spriteName(AtlasType.ATLAS_DTSATLAS, "faceui_jinbi");
			this.pJlpic.enabled = true;
			this.tRawerdtext.text = num6.ToString();
		}
		else
		{
			this.tJltext.text = string.Empty;
			this.pJlpic.enabled = false;
			this.tRawerdtext.text = string.Empty;
		}
		int num7 = (int)szMsg[11];
		bool flag11 = num7 > 0;
		if (flag11)
		{
			this.tFristJltext.text = Language.GetLang(29066, new object[0]);
			this.pFristJlpic.sprite = Window.spriteName(AtlasType.ATLAS_DTSATLAS, "faceui_jinbi");
			this.pFristJlpic.enabled = true;
			this.tFristRawerdtext.text = num7.ToString();
		}
		else
		{
			this.tFristJltext.text = string.Empty;
			this.pFristJlpic.enabled = false;
			this.tFristRawerdtext.text = string.Empty;
		}
		int num8 = (int)szMsg[12];
		int num9 = (int)szMsg[13];
		bool flag12 = num8 > num9;
		if (flag12)
		{
			this.MaxNumText.text = Language.GetLang(29067, new object[]
			{
				num9
			});
			bool activeSelf2 = this.oFristReward.activeSelf;
			if (activeSelf2)
			{
				this.oFristReward.SetActive(false);
			}
			bool activeSelf3 = this.oReward.activeSelf;
			if (activeSelf3)
			{
				this.oReward.SetActive(false);
			}
		}
		else
		{
			this.MaxNumText.text = string.Empty;
			bool flag13 = !this.oFristReward.activeSelf;
			if (flag13)
			{
				this.oFristReward.SetActive(true);
			}
			bool flag14 = !this.oReward.activeSelf;
			if (flag14)
			{
				this.oReward.SetActive(true);
			}
		}
		this.sCloseUiTime = szMsg[9].ToString();
		bool flag15 = !this.DtsResultUI.activeSelf;
		if (flag15)
		{
			this.DtsResultUI.SetActive(true);
		}
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00054C04 File Offset: 0x00052E04
	private void CheckCloseUI()
	{
		bool flag = !(this.sCloseUiTime == string.Empty) && !(this.tCloseText == null);
		if (flag)
		{
			DateTime d = Convert.ToDateTime(this.sCloseUiTime);
			int num = (int)(d - Fun.GetServerDate()).TotalSeconds;
			bool flag2 = num > 0;
			if (flag2)
			{
				this.tCloseText.text = Language.GetLang(29035, new object[]
				{
					Fun.SecToFullTimeString(num)
				});
			}
			else
			{
				this.OnCloseUi(null);
			}
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00054C9C File Offset: 0x00052E9C
	private void NewAddExpEffect()
	{
		bool flag = !this.bCheck;
		if (!flag)
		{
			bool flag2 = this.nAddAllValue < 1;
			if (flag2)
			{
				this.bCheck = false;
				this.nAddAllValue = 0;
				this.nOnceAddValue = 0;
				this.nSumValue = 0;
				this.nNowLevel = 0;
				this.nNowExp = 0;
			}
			else
			{
				int nIsUp = 0;
				int userLvlupExp = Fun.GetUserLvlupExp(this.nNowLevel);
				int num = Mathf.CeilToInt(Time.deltaTime * (float)this.nOnceAddValue);
				bool flag3 = num > this.nAddAllValue;
				if (flag3)
				{
					num = this.nAddAllValue;
				}
				this.nAddAllValue -= num;
				bool flag4 = this.nAddAllValue < 1;
				if (flag4)
				{
					this.nAddAllValue = 0;
				}
				this.nNowExp += num;
				bool flag5 = (float)this.nNowExp >= (float)userLvlupExp;
				if (flag5)
				{
					this.nNowLevel++;
					this.nNowExp -= userLvlupExp;
					nIsUp = 1;
				}
				this.nSumValue += num;
				this.OnShowValue(nIsUp);
			}
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00054DB4 File Offset: 0x00052FB4
	private void OnShowValue(int nIsUp)
	{
		this.mylevel.text = this.nNowLevel.ToString();
		int userLvlupExp = Fun.GetUserLvlupExp(this.nNowLevel);
		this.expfilled.fillAmount = (float)this.nNowExp / (float)userLvlupExp;
		this.expvalue.text = this.nNowExp.ToString() + "/" + userLvlupExp.ToString();
		this.addgoldtext.text = Language.GetLang(2026, new object[]
		{
			this.nSumValue
		});
		bool flag = nIsUp == 1;
		if (flag)
		{
			this.OnUpLevelEffect();
			this.ui_shenjitiao.SetActive(false);
			this.ui_shenjitiao.SetActive(true);
		}
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00054E78 File Offset: 0x00053078
	public void ShowAddGold(int nadd)
	{
		bool flag = nadd >= 1;
		if (flag)
		{
			this.nAddAllGold = nadd;
			this.nNowGold = 0;
			this.isAddGold = true;
			this.addGoldTxt.text = Language.GetLang(2048, new object[]
			{
				this.nNowGold
			});
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00054ED4 File Offset: 0x000530D4
	private void NewAddGoldEffect()
	{
		bool flag = this.isAddGold;
		if (flag)
		{
			this.nNowGold = Mathf.CeilToInt((float)this.nNowGold * 1f + (float)this.nAddAllGold / 3f * Time.deltaTime);
			bool flag2 = this.nNowGold >= this.nAddAllGold;
			if (flag2)
			{
				this.nNowGold = this.nAddAllGold;
				this.isAddGold = false;
			}
			this.addGoldTxt.text = Language.GetLang(2048, new object[]
			{
				this.nNowGold
			});
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00054F74 File Offset: 0x00053174
	private void OnUpLevelEffect()
	{
		this.addico.transform.localPosition = new Vector3(245f, -5.75f, 0f);
		bool flag = !this.addico.activeSelf;
		if (flag)
		{
			this.addico.SetActive(true);
		}
		Tweener t = this.addico.transform.DOLocalMove(new Vector3(245f, 25f, 0f), 0.5f, false);
		t.OnComplete(delegate
		{
			bool activeSelf = this.addico.activeSelf;
			if (activeSelf)
			{
				this.addico.SetActive(false);
			}
		});
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00055008 File Offset: 0x00053208
	private void InitEffectData()
	{
		this.nNowLevel = 0;
		this.bCheck = false;
		this.nAddAllValue = 0;
		this.nOnceAddValue = 0;
		this.nSumValue = 0;
		this.nNowLevel = 0;
		this.nNowExp = 0;
		this.ui_shenjitiao.SetActive(false);
		this.isAddGold = false;
		this.nAddAllGold = 0;
		this.nNowGold = 0;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0005506C File Offset: 0x0005326C
	public void OnCloseUi(GameObject obj)
	{
		this.Close();
		fUIManager.GetInstance().GotoHall(0);
		bool flag = PlayerDataManager.getInstance().HUserData != null;
		if (flag)
		{
			PlayerDataManager.getInstance().HUserData.m_nRoomUID = 0;
		}
		ChatManager.mInst.OnShowOrHideChatWin(true);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x000550BC File Offset: 0x000532BC
	public override void Close()
	{
		this.sCloseUiTime = string.Empty;
		this.InitEffectData();
		base.Close();
	}

	// Token: 0x040006F1 RID: 1777
	public static DtsUICon Inst;

	// Token: 0x040006F2 RID: 1778
	private Text tPlayerNum;

	// Token: 0x040006F3 RID: 1779
	private Text tTopMsg;

	// Token: 0x040006F4 RID: 1780
	private GameObject oHp;

	// Token: 0x040006F5 RID: 1781
	private Image hpfill;

	// Token: 0x040006F6 RID: 1782
	private Text djstext;

	// Token: 0x040006F7 RID: 1783
	private Text djsusetext;

	// Token: 0x040006F8 RID: 1784
	private GameObject oCloneItem;

	// Token: 0x040006F9 RID: 1785
	private GameObject oItemList;

	// Token: 0x040006FA RID: 1786
	private GameObject oChangeHeroUI;

	// Token: 0x040006FB RID: 1787
	private Text tChangeTopName;

	// Token: 0x040006FC RID: 1788
	private RawImage TopImg;

	// Token: 0x040006FD RID: 1789
	private Text tHeroName;

	// Token: 0x040006FE RID: 1790
	private Text tKeyText;

	// Token: 0x040006FF RID: 1791
	private Text tCenterMsg;

	// Token: 0x04000700 RID: 1792
	private Text tDeadMsg;

	// Token: 0x04000701 RID: 1793
	private Text tKillNum;

	// Token: 0x04000702 RID: 1794
	private GameObject oDead;

	// Token: 0x04000703 RID: 1795
	private Image mJdtFill;

	// Token: 0x04000704 RID: 1796
	private Text tDqDjs;

	// Token: 0x04000705 RID: 1797
	private GameObject oRen;

	// Token: 0x04000706 RID: 1798
	private RawImage rawmap;

	// Token: 0x04000707 RID: 1799
	private GameObject oSign;

	// Token: 0x04000708 RID: 1800
	private GameObject oHpBoxJy;

	// Token: 0x04000709 RID: 1801
	private GameObject oHpBoxObj;

	// Token: 0x0400070A RID: 1802
	private RawImage hpboxraw;

	// Token: 0x0400070B RID: 1803
	private Text tHpboxName;

	// Token: 0x0400070C RID: 1804
	private GameObject oCloneOneskill;

	// Token: 0x0400070D RID: 1805
	private GameObject oSkillList;

	// Token: 0x0400070E RID: 1806
	private GameObject oHpNumObj;

	// Token: 0x0400070F RID: 1807
	private Text tHpNum;

	// Token: 0x04000710 RID: 1808
	private GameObject oHpBoxEff;

	// Token: 0x04000711 RID: 1809
	private DtsUser _dDtsUser;

	// Token: 0x04000712 RID: 1810
	private NetLiving _NetLiving;

	// Token: 0x04000713 RID: 1811
	private float fUsehpBox;

	// Token: 0x04000714 RID: 1812
	private GameObject DtsMapObj;

	// Token: 0x04000715 RID: 1813
	private DtsMapUICon DtsMapUICon;

	// Token: 0x04000716 RID: 1814
	private float fUseDzBox;

	// Token: 0x04000717 RID: 1815
	private GameObject oJdt;

	// Token: 0x04000718 RID: 1816
	public GameObject DtsResultUI;

	// Token: 0x04000719 RID: 1817
	private GameObject DtsResultTop;

	// Token: 0x0400071A RID: 1818
	private GameObject mydata;

	// Token: 0x0400071B RID: 1819
	private GameObject ui_shenjitiao;

	// Token: 0x0400071C RID: 1820
	private Text nametext;

	// Token: 0x0400071D RID: 1821
	private Text mylevel;

	// Token: 0x0400071E RID: 1822
	private Image expfilled;

	// Token: 0x0400071F RID: 1823
	private RawImage myHead;

	// Token: 0x04000720 RID: 1824
	private RawImage Winbg;

	// Token: 0x04000721 RID: 1825
	private GameObject addico;

	// Token: 0x04000722 RID: 1826
	private Text addexptext;

	// Token: 0x04000723 RID: 1827
	private Text addgoldtext;

	// Token: 0x04000724 RID: 1828
	private Text expvalue;

	// Token: 0x04000725 RID: 1829
	private Text addGoldTxt;

	// Token: 0x04000726 RID: 1830
	private Text tCloseText;

	// Token: 0x04000727 RID: 1831
	private Text tRanktext;

	// Token: 0x04000728 RID: 1832
	private Text tResultMsg;

	// Token: 0x04000729 RID: 1833
	private Text tNowrank;

	// Token: 0x0400072A RID: 1834
	private Text tKillnum;

	// Token: 0x0400072B RID: 1835
	private GameObject oReward;

	// Token: 0x0400072C RID: 1836
	private Text tJltext;

	// Token: 0x0400072D RID: 1837
	private Text tRawerdtext;

	// Token: 0x0400072E RID: 1838
	private Image pJlpic;

	// Token: 0x0400072F RID: 1839
	private GameObject returngame;

	// Token: 0x04000730 RID: 1840
	private GameObject zd_buff_cj;

	// Token: 0x04000731 RID: 1841
	private Text tTopKillNum;

	// Token: 0x04000732 RID: 1842
	private Text tKillName;

	// Token: 0x04000733 RID: 1843
	private GameObject oGjqEff;

	// Token: 0x04000734 RID: 1844
	private GameObject oFyqEff;

	// Token: 0x04000735 RID: 1845
	private GameObject dzbox;

	// Token: 0x04000736 RID: 1846
	private RawImage dzboxraw;

	// Token: 0x04000737 RID: 1847
	private Text dzboxText;

	// Token: 0x04000738 RID: 1848
	private GameObject dzboxjy;

	// Token: 0x04000739 RID: 1849
	private GameObject dzboxnumbg;

	// Token: 0x0400073A RID: 1850
	private Text dzboxnumText;

	// Token: 0x0400073B RID: 1851
	private GameObject zboxEff;

	// Token: 0x0400073C RID: 1852
	private GameObject oFristReward;

	// Token: 0x0400073D RID: 1853
	private Text tFristJltext;

	// Token: 0x0400073E RID: 1854
	private Image pFristJlpic;

	// Token: 0x0400073F RID: 1855
	private Text tFristRawerdtext;

	// Token: 0x04000740 RID: 1856
	private Text MaxNumText;

	// Token: 0x04000741 RID: 1857
	private GameObject ui_jishi;

	// Token: 0x04000742 RID: 1858
	private string sCloseUiTime = string.Empty;

	// Token: 0x04000743 RID: 1859
	private Dictionary<int, GameObject> szDjsGameObj = new Dictionary<int, GameObject>();

	// Token: 0x04000744 RID: 1860
	private float fBoxCheck;

	// Token: 0x04000745 RID: 1861
	private float fdCheck;

	// Token: 0x04000746 RID: 1862
	private int nNowMsgType;

	// Token: 0x04000747 RID: 1863
	private float fCheck;

	// Token: 0x04000748 RID: 1864
	private float fCheckTime;

	// Token: 0x04000749 RID: 1865
	private int nAddAllValue;

	// Token: 0x0400074A RID: 1866
	private int nOnceAddValue;

	// Token: 0x0400074B RID: 1867
	private int nNowLevel;

	// Token: 0x0400074C RID: 1868
	private int nNowExp;

	// Token: 0x0400074D RID: 1869
	private bool bCheck;

	// Token: 0x0400074E RID: 1870
	private int nSumValue;

	// Token: 0x0400074F RID: 1871
	private bool isAddGold;

	// Token: 0x04000750 RID: 1872
	private int nAddAllGold;

	// Token: 0x04000751 RID: 1873
	private int nNowGold;
}
