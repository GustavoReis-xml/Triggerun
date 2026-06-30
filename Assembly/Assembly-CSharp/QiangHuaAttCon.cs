using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B5 RID: 693
public class QiangHuaAttCon : Window
{
	// Token: 0x06001FCD RID: 8141 RVA: 0x00124448 File Offset: 0x00122648
	protected override void Awake()
	{
		base.Awake();
		this.tZhangDuLi = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.pAutoQHBtn = this.UIGameObj.Get_GameObj(1);
		this.pAttQhList = this.UIGameObj.Get_GameObj(2);
		this.pAttQhInfoOne = this.UIGameObj.Get_GameObj(3);
		this.pCheckTog_1 = this.UIGameObj.Get_GameObj(4).GetComponent<Toggle>();
		this.pCheckTog_2 = this.UIGameObj.Get_GameObj(7).GetComponent<Toggle>();
		this.mTypeImg = this.UIGameObj.Get_GameObj(12).GetComponent<Image>();
		EventTriggerListener.Get(this.pCheckTog_1.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		EventTriggerListener.Get(this.pCheckTog_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		this.pAttValueList = this.UIGameObj.Get_GameObj(5);
		this.pAttValue = this.UIGameObj.Get_GameObj(6);
		this.pHeroImgList = this.UIGameObj.Get_GameObj(8);
		this.pHeroImg = this.UIGameObj.Get_GameObj(9);
		this.pCaiLiaoList = this.UIGameObj.Get_GameObj(10);
		this.pCaiLiao = this.UIGameObj.Get_GameObj(11);
		this.tZhanDuLiTip = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.pAttValue.SetActive(false);
		this.pAttQhInfoOne.SetActive(false);
		this.pHeroImg.SetActive(false);
		this.pCaiLiao.SetActive(false);
		this.tZengFuQi = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.pZengFuQiBtn = this.UIGameObj.Get_GameObj(15);
		EventTriggerListener.Get(this.pAutoQHBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnAutoQHClick);
		EventTriggerListener.Get(this.pAutoQHBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pZengFuQiBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.pZengFuQiBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x00124691 File Offset: 0x00122891
	public void InitData(int nHeroType)
	{
		this.m_nHeroType = nHeroType;
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x0012469C File Offset: 0x0012289C
	public void OnHiveLpEff()
	{
		foreach (QiangHuaAttributeData qiangHuaAttributeData in this.HeroTypeXmlData.AllAttributeData.Values)
		{
			bool flag = qiangHuaAttributeData == null;
			if (!flag)
			{
				GameObject gameObject = this.szAttQhInfoList[qiangHuaAttributeData.nType];
				bool flag2 = !(gameObject == null);
				if (flag2)
				{
					Transform transform = gameObject.transform.FindChild("ui_xq");
					bool flag3 = transform != null;
					if (flag3)
					{
						transform.gameObject.SetActive(false);
					}
					Transform transform2 = gameObject.transform.FindChild("ui_sj_cg");
					bool flag4 = transform2 != null;
					if (flag4)
					{
						transform2.gameObject.SetActive(false);
					}
					Transform transform3 = gameObject.transform.FindChild("ui_sj_sb");
					bool flag5 = transform3 != null;
					if (flag5)
					{
						transform3.gameObject.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x001247C4 File Offset: 0x001229C4
	private void Start()
	{
		string imageName = string.Empty;
		bool flag = this.m_nHeroType == 1;
		string imageName2;
		if (flag)
		{
			imageName = "gongji";
			imageName2 = "huang";
		}
		else
		{
			bool flag2 = this.m_nHeroType == 2;
			if (flag2)
			{
				imageName2 = "lan";
				imageName = "fangyu";
			}
			else
			{
				imageName2 = "lv";
				imageName = "fuzhu";
			}
		}
		this.mTypeImg.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, imageName);
		this.pAttQhInfoOne.transform.FindChild("qh_img").GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, imageName2);
		this.OnInitShowAtt();
		this.OnHeroList();
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x0012487C File Offset: 0x00122A7C
	private void OnInitShowAtt()
	{
		int num = 0;
		bool flag = !fGameManager.mloadConfig.AllQiangHuaData.ContainsKey(this.m_nHeroType);
		if (!flag)
		{
			this.HeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[this.m_nHeroType];
			int num2 = 0;
			foreach (QiangHuaAttributeData qiangHuaAttributeData in this.HeroTypeXmlData.AllAttributeData.Values)
			{
				bool flag2 = qiangHuaAttributeData != null;
				if (flag2)
				{
					HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, qiangHuaAttributeData.nType);
					int num3 = this.OnCreateAttValue(qiangHuaAttributeData, attDataByType);
					this.OnCreateAttQhInfo(qiangHuaAttributeData, attDataByType, num2);
					this.OnCreateXiaoHaoValue(qiangHuaAttributeData.m_nItem, qiangHuaAttributeData.nType, qiangHuaAttributeData.m_sTypeName);
					num += num3;
					num2++;
				}
			}
			this.UpdateZhanDuLi(num);
		}
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x0012498C File Offset: 0x00122B8C
	private void UpdateZhanDuLi(int nZhanDuLi)
	{
		this.tZhangDuLi.text = nZhanDuLi.ToString();
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x001249A4 File Offset: 0x00122BA4
	private int OnCreateAttValue(QiangHuaAttributeData attdata, HeroOneAttData UserOneAttData)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pAttValue);
		gameObject.transform.SetParent(this.pAttValueList.transform);
		gameObject.SetActive(true);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.FindChild("Att_1").GetComponent<Text>().text = attdata.sName + ":";
		this.szAttValueList.Add(attdata.nType, gameObject);
		this.OnUpdateAttValue(UserOneAttData);
		return (int)PveManager.GetInstance().GetZdlValue(UserOneAttData.m_nQhLv, UserOneAttData.m_nJieLv);
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x00124A70 File Offset: 0x00122C70
	private void OnCreateAttQhInfo(QiangHuaAttributeData attdata, HeroOneAttData UserOneAttData, int nNum)
	{
		int nType = attdata.nType;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pAttQhInfoOne);
		gameObject.transform.SetParent(this.pAttQhList.transform);
		gameObject.SetActive(true);
		gameObject.transform.localPosition = this.szAttPos[nType];
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.name = nType.ToString();
		RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
		string format = "qianghuatubiao/{0}.txt";
		fUIManager.GetInstance().GetIconAsset(string.Format(format, attdata.m_sIco), component);
		this.szAttQhInfoList.Add(attdata.nType, gameObject);
		this.OnUpdateAttQhInfo(UserOneAttData, true);
		EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnQHClick);
		EventTriggerListener.Get(gameObject.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnAttEnterObj);
		EventTriggerListener.Get(gameObject.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnAttExitObj);
		bool flag = nNum % 2 == 1;
		if (flag)
		{
			gameObject.transform.FindChild("ui_dj_sj").localPosition = new Vector3(-118f, 15f, 0f);
			gameObject.transform.FindChild("ui_dj_sje").localPosition = new Vector3(-118f, 15f, 0f);
		}
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x00124BFC File Offset: 0x00122DFC
	private void OnCanLvUpEff(int nType, int nItemNum, int nHaveItemNum, bool isJie)
	{
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, nType);
		bool flag = nType == 100;
		if (!flag)
		{
			GameObject gameObject = this.szAttQhInfoList[nType];
			GameObject gameObject2 = gameObject.transform.FindChild("ui_qh_sj").gameObject;
			GameObject gameObject3 = gameObject.transform.FindChild("ui_dj_sj").gameObject;
			GameObject gameObject4 = gameObject.transform.FindChild("shengjieImg").gameObject;
			gameObject2.SetActive(false);
			gameObject3.SetActive(false);
			gameObject4.SetActive(false);
			int nMaxLv = this.HeroTypeXmlData.AllAttributeData[attDataByType.m_nType].nMaxLv;
			bool flag2 = attDataByType.m_nJieLv < this.HeroTypeXmlData.nMaxJie;
			if (flag2)
			{
				bool flag3 = attDataByType.m_nJieLv < this.HeroTypeXmlData.nMaxJie && attDataByType.m_nQhLv >= nMaxLv;
				if (flag3)
				{
					gameObject2.SetActive(true);
				}
				else
				{
					bool flag4 = nItemNum != 0 && nHaveItemNum >= nItemNum;
					if (flag4)
					{
						gameObject2.SetActive(true);
					}
				}
			}
		}
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x00124D28 File Offset: 0x00122F28
	private int GetQingHuaItem(int nType)
	{
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[nType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, nType);
		bool flag = attDataByType.m_nJieLv >= this.HeroTypeXmlData.nMaxJie;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = attDataByType.m_nQhLv == qiangHuaAttributeData.nMaxLv;
			if (flag2)
			{
				ArrayList szJinJieRand = this.HeroTypeXmlData.szJinJieRand;
				ArrayList arrayList = (ArrayList)szJinJieRand[attDataByType.m_nJieLv];
				bool flag3 = attDataByType.m_nJieLv >= this.HeroTypeXmlData.nMaxJie;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					int num = (int)arrayList[2];
					result = PveManager.GetInstance().GetUpJieItemNum(attDataByType, qiangHuaAttributeData, this.HeroTypeXmlData);
				}
			}
			else
			{
				int num = qiangHuaAttributeData.m_nItem;
				result = PveManager.GetInstance().GetUpQinaghuaItemNum(attDataByType, qiangHuaAttributeData, this.HeroTypeXmlData);
			}
		}
		return result;
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x00124E20 File Offset: 0x00123020
	private void onShowEffLv(int nType, GameObject pParent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pEffLvUp);
		gameObject.transform.SetParent(pParent.transform);
		gameObject.SetActive(true);
		gameObject.transform.localPosition = new Vector3(13f, -31f, 0f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x00124E94 File Offset: 0x00123094
	private void OnCreateXiaoHaoValue(int nItemId, int nType, string sTypeName = "")
	{
		string format = string.Empty;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pCaiLiao);
		gameObject.transform.SetParent(this.pAttQhList.transform);
		gameObject.SetActive(true);
		bool flag = nType != 100;
		if (flag)
		{
			gameObject.transform.localPosition = this.szAttPos[nType] + new Vector3(0f, -70f, 0f);
		}
		else
		{
			gameObject.transform.localPosition = new Vector3(0f, -70f, 0f);
			gameObject.SetActive(false);
		}
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		bool flag2 = !fGameManager.mloadConfig.AllItemData.ContainsKey(nItemId);
		if (flag2)
		{
			Debug.LogError("item 错误==" + nItemId.ToString());
		}
		else
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[nItemId];
			gameObject.transform.FindChild("valuename").GetComponent<Text>().text = sTypeName;
			RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
			component.gameObject.name = itemData.m_nItemId.ToString();
			GameObject gameObject2 = gameObject.transform.FindChild("Button").gameObject;
			gameObject2.name = itemData.m_nShopId.ToString();
			EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyItem);
			EventTriggerListener.Get(gameObject2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
			EventTriggerListener.Get(gameObject2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
			EventTriggerListener.Get(component.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterItemObj);
			EventTriggerListener.Get(component.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitItemOObj);
			format = ShopManager.GetIcoPath(itemData.m_nType);
			fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sIco), component);
			bool flag3 = !this.szXiaoItemList.ContainsKey(nItemId);
			if (flag3)
			{
				this.szXiaoItemList.Add(nItemId, gameObject);
			}
			bool flag4 = !this.szItemTypeList.ContainsKey(nItemId);
			if (flag4)
			{
				this.szItemTypeList.Add(nItemId, nType);
			}
			this.ShowEff(nType);
		}
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x0012511C File Offset: 0x0012331C
	public void OnBuyItem(GameObject obj)
	{
		int key = int.Parse(obj.name);
		bool flag = fGameManager.mloadConfig.AllShopData.ContainsKey(key);
		if (flag)
		{
			ShopData pShopData = fGameManager.mloadConfig.AllShopData[key];
			ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
		}
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x0012516A File Offset: 0x0012336A
	private void OnEnterObj(GameObject obj)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(27011, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x00125198 File Offset: 0x00123398
	private void OnExitObj(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x001251A8 File Offset: 0x001233A8
	private void OnEnterItemObj(GameObject obj)
	{
		int key = int.Parse(obj.name);
		ItemData pitemdata = fGameManager.mloadConfig.AllItemData[key];
		bool flag = QiangHuaUICon.inst != null;
		if (flag)
		{
			QiangHuaUICon.inst.OnShowItemTip(pitemdata);
		}
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x001251F0 File Offset: 0x001233F0
	private void OnExitItemOObj(GameObject obj)
	{
		bool flag = QiangHuaUICon.inst != null;
		if (flag)
		{
			QiangHuaUICon.inst.OnHideItemTip();
		}
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x0012521C File Offset: 0x0012341C
	private void OnAttEnterObj(GameObject obj)
	{
		int num = int.Parse(obj.name);
		bool flag = QiangHuaUICon.inst != null;
		if (flag)
		{
			float[] array = QiangHuaUICon.inst.OnShowAttTip(this.m_nHeroType, num);
			this.OnShowTipAtt(num, array[0], array[1]);
		}
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x00125268 File Offset: 0x00123468
	private void OnAttExitObj(GameObject obj)
	{
		bool flag = QiangHuaUICon.inst != null;
		if (flag)
		{
			QiangHuaUICon.inst.OnHideAttTip();
		}
		int nType = int.Parse(obj.name);
		this.OnShowTipAtt(nType, 0f, 0f);
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x001252B0 File Offset: 0x001234B0
	private void OnShowTipAtt(int nType, float nValue, float nzdl)
	{
		bool flag = nValue <= 0f;
		if (flag)
		{
			this.tZhanDuLiTip.text = string.Empty;
			GameObject gameObject = this.szAttValueList[nType];
			gameObject.transform.FindChild("g/tipvalue").GetComponent<Text>().text = string.Empty;
		}
		else
		{
			this.tZhanDuLiTip.text = Language.GetLang(27013, new object[0]) + "+" + nzdl.ToString();
			GameObject gameObject2 = this.szAttValueList[nType];
			gameObject2.transform.FindChild("g/tipvalue").GetComponent<Text>().text = "+" + PveManager.GetInstance().OnShowValue(nType, nValue);
		}
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x0012537C File Offset: 0x0012357C
	public void OnUpdateItemNum(int nItemID, string sForm)
	{
		bool flag = !this.szXiaoItemList.ContainsKey(nItemID);
		if (!flag)
		{
			int num = this.szItemTypeList[nItemID];
			int itemNumById = ShopManager.GetInstance().GetItemNumById(nItemID);
			bool flag2 = num < 100;
			if (flag2)
			{
				GameObject gameObject = this.szXiaoItemList[nItemID];
				int qingHuaItem = this.GetQingHuaItem(num);
				bool flag3 = qingHuaItem == 0;
				if (flag3)
				{
					gameObject.SetActive(false);
					this.OnCanLvUpEff(num, 0, 0, false);
				}
				else
				{
					string text = (itemNumById >= qingHuaItem) ? "#ffffff" : "#FF0000";
					gameObject.transform.FindChild("value").GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=",
						text,
						">",
						itemNumById.ToString(),
						"/",
						qingHuaItem.ToString(),
						"</color>"
					});
					this.OnCanLvUpEff(num, qingHuaItem, itemNumById, false);
				}
			}
		}
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x00125488 File Offset: 0x00123688
	public void OnQHClick(GameObject obj)
	{
		int num = int.Parse(obj.name);
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[num];
		bool flag = Time.time - this.fOnClickTime < 0.2f;
		if (!flag)
		{
			this.fOnClickTime = Time.time;
			HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, num);
			bool flag2 = attDataByType.m_nJieLv >= this.HeroTypeXmlData.nMaxJie;
			if (flag2)
			{
				this.UpdateQhResult(attDataByType.m_nType, -100, 1);
			}
			else
			{
				bool flag3 = attDataByType.m_nQhLv == qiangHuaAttributeData.nMaxLv;
				if (flag3)
				{
					ArrayList szJinJieRand = this.HeroTypeXmlData.szJinJieRand;
					ArrayList arrayList = (ArrayList)szJinJieRand[attDataByType.m_nJieLv];
					bool flag4 = attDataByType.m_nJieLv < this.HeroTypeXmlData.nMaxJie;
					if (flag4)
					{
						int nItemId = (int)arrayList[2];
						int nItemNum = PveManager.GetInstance().GetUpJieItemNum(attDataByType, qiangHuaAttributeData, this.HeroTypeXmlData);
						fUIManager.GetInstance().ShowWin("QingHuaJinJieUI", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.LoadJinJieUiOk), new ArrayList
						{
							this.m_nHeroType,
							num,
							this.isCheck_1
						}, true);
					}
				}
				else
				{
					int nItemId = qiangHuaAttributeData.m_nItem;
					int nItemNum = PveManager.GetInstance().GetUpQinaghuaItemNum(attDataByType, qiangHuaAttributeData, this.HeroTypeXmlData);
					int num2 = 1;
					ArrayList arrayList2 = PveManager.GetInstance().OnXiaoHaoCaiLiao(nItemId, nItemNum, this.isCheck_1, this.isCheck_2, num2, true, 0);
					int num3 = (int)arrayList2[0];
					int num4 = (int)arrayList2[1];
					bool flag5 = num3 < 1;
					if (flag5)
					{
						this.UpdateQhResult(attDataByType.m_nType, num3, 1);
						bool flag6 = num3 == -4;
						if (flag6)
						{
							fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
						}
					}
					else
					{
						bool flag7 = num4 > 0 && !this.IsCheckMoney;
						if (flag7)
						{
							string content = string.Empty;
							content = ((num2 != 1) ? Language.GetLang(27001, new object[]
							{
								num4
							}) : Language.GetLang(27000, new object[]
							{
								num4
							}));
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), content, Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnQiangHua), num, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
						}
						else
						{
							PveManager.GetInstance().OnSendMsgToServer(19502, new ArrayList
							{
								this.m_nHeroType,
								num,
								this.isCheck_1 ? 1 : 0,
								this.isCheck_2 ? 1 : 0,
								1
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x00125810 File Offset: 0x00123A10
	private void OnQiangHua(object obj)
	{
		int num = (int)obj;
		PveManager.GetInstance().OnSendMsgToServer(19502, new ArrayList
		{
			this.m_nHeroType,
			num,
			this.isCheck_1 ? 1 : 0,
			this.isCheck_2 ? 1 : 0,
			1
		});
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x00125898 File Offset: 0x00123A98
	private void LoadJinJieUiOk(string sUI, GameObject ob, object szParam)
	{
		ArrayList arrayList = (ArrayList)szParam;
		int nHeroType = (int)arrayList[0];
		int nAttType = (int)arrayList[1];
		bool isCheck = (bool)arrayList[2];
		QingHuaJinJieUICon.inst.OnSetData(nHeroType, nAttType, isCheck);
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x001258E4 File Offset: 0x00123AE4
	public void UpdateQhResult(int nAttType, int nResult, int nLvUpType = 1)
	{
		GameObject gameObject = this.szAttQhInfoList[nAttType];
		string str = string.Empty;
		bool flag = nResult > 0;
		if (flag)
		{
			str = ((nLvUpType != 1) ? Language.GetLang(27029, new object[0]) : Language.GetLang(27006, new object[0]));
		}
		else if (nResult != -100)
		{
			if (nResult != -10)
			{
				if (nResult != -4)
				{
					str = Language.GetLang(27048, new object[0]);
				}
				else
				{
					str = Language.GetLang(27007, new object[0]);
				}
			}
			else
			{
				str = Language.GetLang(27009, new object[0]);
				this.OnShowEff(gameObject, "ui_sj_sb", true);
				fUISoundManager.instance.PlayUISound("ui_qh_jinjie_fail", 0, null, 20);
			}
		}
		else
		{
			str = Language.GetLang(27008, new object[0]);
		}
		this.OnFightEnd(gameObject.transform.FindChild("shengjitishi").GetComponent<Text>(), "<color=#ff0000>" + str + "</color>");
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x001259F4 File Offset: 0x00123BF4
	private void OnShowEff(GameObject pParent, string sEffName, bool isShow)
	{
		GameObject gameObject = pParent.transform.FindChild(sEffName).gameObject;
		gameObject.SetActive(false);
		if (isShow)
		{
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x00125A2C File Offset: 0x00123C2C
	public void OnJinJieClick(GameObject obj)
	{
		int num = int.Parse(obj.name);
		PveManager.GetInstance().OnSendMsgToServer(19504, new ArrayList
		{
			this.m_nHeroType,
			num,
			this.isCheck_1 ? 1 : 0,
			this.isCheck_2 ? 1 : 0
		});
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x00125AAC File Offset: 0x00123CAC
	private void OnAutoQHClick(GameObject obj)
	{
		bool flag = Time.time - this.nTime >= 0.6f;
		if (flag)
		{
			this.nTime = Time.time;
			ArrayList autoAllXiaoHao = PveManager.GetInstance().GetAutoAllXiaoHao(this.m_nHeroType, this.isCheck_1, this.isCheck_2);
			int num = (int)autoAllXiaoHao[0];
			int num2 = (int)autoAllXiaoHao[1];
			int num3 = (int)autoAllXiaoHao[2];
			bool flag2 = num3 == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27015, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = num == num3 && num2 == 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27002, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = num == num3 && num2 > 0;
					if (flag4)
					{
						fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
					}
					else
					{
						bool flag5 = num < num3 && num2 > 0 && !this.IsCheckMoney;
						if (flag5)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27003, new object[]
							{
								num2
							}), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnAllQiangHua), string.Empty, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
						}
						else
						{
							this.OnAllQiangHua(obj);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x00125CEA File Offset: 0x00123EEA
	private void OnTipToggle(bool bstate)
	{
		this.IsCheckMoney = bstate;
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x00125CF4 File Offset: 0x00123EF4
	private void OnCancel(object obj)
	{
		this.IsCheckMoney = false;
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x00125D00 File Offset: 0x00123F00
	private void OnAllQiangHua(object obj)
	{
		PveManager.GetInstance().OnSendMsgToServer(19505, new ArrayList
		{
			this.m_nHeroType,
			0,
			this.isCheck_1 ? 1 : 0,
			this.isCheck_2 ? 1 : 0
		});
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x00125D74 File Offset: 0x00123F74
	private int OnUpdateAttValue(HeroOneAttData UserOneAttData)
	{
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[UserOneAttData.m_nType];
		int nBaseplus = qiangHuaAttributeData.nBaseplus;
		GameObject gameObject = this.szAttValueList[UserOneAttData.m_nType];
		float attValue = PveManager.GetInstance().GetAttValue(UserOneAttData.m_nQhLv, UserOneAttData.m_nJieLv, qiangHuaAttributeData);
		gameObject.transform.FindChild("g/value").GetComponent<Text>().text = "+" + PveManager.GetInstance().OnShowValue(UserOneAttData.m_nType, attValue);
		return (int)attValue;
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x00125E08 File Offset: 0x00124008
	private void OnUpdateAttQhInfo(HeroOneAttData UserOneAttData, bool isCreate = true)
	{
		GameObject gameObject = this.szAttQhInfoList[UserOneAttData.m_nType];
		gameObject.transform.FindChild("jie_lv").GetComponent<Text>().text = UserOneAttData.m_nJieLv.ToString() + Language.GetLang(27004, new object[0]);
		gameObject.transform.FindChild("qh_lv").GetComponent<Text>().text = UserOneAttData.m_nQhLv.ToString() + Language.GetLang(27005, new object[0]);
		Image component = gameObject.transform.FindChild("qh_img").GetComponent<Image>();
		int nMaxLv = this.HeroTypeXmlData.AllAttributeData[UserOneAttData.m_nType].nMaxLv;
		component.fillAmount = (float)UserOneAttData.m_nQhLv / (float)nMaxLv;
		bool flag = !isCreate;
		if (flag)
		{
			QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[UserOneAttData.m_nType];
			this.ShowEff(UserOneAttData.m_nType);
			bool flag2 = UserOneAttData.m_nQhLv == 0;
			if (flag2)
			{
				this.OnShowEff(gameObject, "ui_sj_cg", true);
				this.UpdateQhResult(UserOneAttData.m_nType, 1, 2);
				fUISoundManager.instance.PlayUISound("ui_qh_jinjie_success", 0, null, 20);
			}
			else
			{
				this.OnShowEff(gameObject, "ui_xq", true);
				this.UpdateQhResult(UserOneAttData.m_nType, 1, 1);
				fUISoundManager.instance.PlayUISound("ui_qh_qianghua_success", 0, null, 20);
			}
		}
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x00125F8C File Offset: 0x0012418C
	public void ShowEff(int nType)
	{
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[nType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, nType);
		this.OnUpdateItemNum(qiangHuaAttributeData.m_nItem, "ShowEff");
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x00125FD8 File Offset: 0x001241D8
	public void OnFightEnd(Text t, string sMsg)
	{
		t.transform.localScale = new Vector3(1f, 1f, 1f);
		t.gameObject.SetActive(true);
		t.text = sMsg;
		base.StartCoroutine(this.Wait_TextDOScale(t.gameObject));
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x0012602E File Offset: 0x0012422E
	private IEnumerator Wait_TextDOScale(GameObject Obj)
	{
		yield return new WaitForSeconds(2f);
		Obj.transform.DOScale(new Vector3(0f, 0f, 1f), 1f);
		yield break;
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x00126044 File Offset: 0x00124244
	public void OnHeroList()
	{
		ArrayList szHeroId = this.HeroTypeXmlData.szHeroId;
		int count = szHeroId.Count;
		for (int i = 0; i < count; i++)
		{
			int key = (int)szHeroId[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pHeroImg);
			gameObject.transform.SetParent(this.pHeroImgList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			RawImage component = gameObject.GetComponent<RawImage>();
			HeroData heroData = fGameManager.mloadConfig.AllHeroList[key];
			fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", component);
		}
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x0012612C File Offset: 0x0012432C
	public void OnUpdateOneValue(HeroOneAttData UserOneAttData)
	{
		this.OnUpdateAttValue(UserOneAttData);
		this.OnUpdateAttQhInfo(UserOneAttData, false);
		int zhanDuLi = PveManager.GetInstance().GetZhanDuLi(this.m_nHeroType);
		this.UpdateZhanDuLi(zhanDuLi);
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x00126164 File Offset: 0x00124364
	private void OnCheckToggle(GameObject obj)
	{
		bool flag = obj.name == "checktog_1";
		if (flag)
		{
			this.isCheck_1 = this.pCheckTog_1.isOn;
		}
		else
		{
			this.isCheck_2 = this.pCheckTog_2.isOn;
		}
	}

	// Token: 0x04001D07 RID: 7431
	public int m_nHeroType;

	// Token: 0x04001D08 RID: 7432
	private Text tZhangDuLi;

	// Token: 0x04001D09 RID: 7433
	private Image mTypeImg;

	// Token: 0x04001D0A RID: 7434
	private GameObject pAutoQHBtn;

	// Token: 0x04001D0B RID: 7435
	private GameObject pAttQhList;

	// Token: 0x04001D0C RID: 7436
	private GameObject pAttQhInfoOne;

	// Token: 0x04001D0D RID: 7437
	private Toggle pCheckTog_1;

	// Token: 0x04001D0E RID: 7438
	private Toggle pCheckTog_2;

	// Token: 0x04001D0F RID: 7439
	private GameObject pAttValueList;

	// Token: 0x04001D10 RID: 7440
	private GameObject pAttValue;

	// Token: 0x04001D11 RID: 7441
	private GameObject pHeroImgList;

	// Token: 0x04001D12 RID: 7442
	private GameObject pHeroImg;

	// Token: 0x04001D13 RID: 7443
	private GameObject pCaiLiaoList;

	// Token: 0x04001D14 RID: 7444
	private GameObject pCaiLiao;

	// Token: 0x04001D15 RID: 7445
	private bool isCheck_1;

	// Token: 0x04001D16 RID: 7446
	private bool isCheck_2;

	// Token: 0x04001D17 RID: 7447
	private Dictionary<int, GameObject> szXiaoItemList = new Dictionary<int, GameObject>();

	// Token: 0x04001D18 RID: 7448
	private Dictionary<int, GameObject> szAttValueList = new Dictionary<int, GameObject>();

	// Token: 0x04001D19 RID: 7449
	private Dictionary<int, GameObject> szAttQhInfoList = new Dictionary<int, GameObject>();

	// Token: 0x04001D1A RID: 7450
	private Dictionary<int, int> szItemTypeList = new Dictionary<int, int>();

	// Token: 0x04001D1B RID: 7451
	private List<Vector3> szAttPos = new List<Vector3>
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(138f, -210f, 0f),
		new Vector3(-42f, -317f, 0f),
		new Vector3(328f, -317f, 0f),
		new Vector3(-87f, -516f, 0f),
		new Vector3(387f, -516f, 0f),
		new Vector3(40f, -669f, 0f),
		new Vector3(260f, -669f, 0f)
	};

	// Token: 0x04001D1C RID: 7452
	private Text tZhanDuLiTip;

	// Token: 0x04001D1D RID: 7453
	private Text tZengFuQi;

	// Token: 0x04001D1E RID: 7454
	private GameObject pZengFuQiBtn;

	// Token: 0x04001D1F RID: 7455
	private GameObject pEffLvUp;

	// Token: 0x04001D20 RID: 7456
	private QiangHuaHeroTypeXmlData HeroTypeXmlData;

	// Token: 0x04001D21 RID: 7457
	private float fOnClickTime;

	// Token: 0x04001D22 RID: 7458
	private float nTime;

	// Token: 0x04001D23 RID: 7459
	private bool IsCheckMoney;
}
