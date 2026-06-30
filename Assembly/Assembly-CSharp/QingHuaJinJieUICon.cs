using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002B9 RID: 697
public class QingHuaJinJieUICon : Window
{
	// Token: 0x0600200F RID: 8207 RVA: 0x00127364 File Offset: 0x00125564
	protected override void Awake()
	{
		base.Awake();
		QingHuaJinJieUICon.inst = this;
		this.mJinBgLv = this.UIGameObj.Get_GameObj(0).GetComponent<Image>();
		this.rJinJieIco = this.UIGameObj.Get_GameObj(1).GetComponent<RawImage>();
		this.tJinJieLv = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.tZhanDuLiValue = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.tZhanDuLiValueAdd = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.tChengGongLvText = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.pCheckTog = this.UIGameObj.Get_GameObj(6);
		this.tCglShengYu = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.pBuyCglBtn = this.UIGameObj.Get_GameObj(8);
		this.tXiaoCaiLiao = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.rCaiLiaoIco = this.UIGameObj.Get_GameObj(10).GetComponent<RawImage>();
		this.tCaiLiaoHave = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.pBuyCaiLiaoBtn = this.UIGameObj.Get_GameObj(12);
		this.pShengJiBtn = this.UIGameObj.Get_GameObj(13);
		this.pCloseBtn = this.UIGameObj.Get_GameObj(14);
		this.pCheckTog_2 = this.pCheckTog.GetComponent<Toggle>();
		EventTriggerListener.Get(this.pCheckTog_2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		EventTriggerListener.Get(this.pShengJiBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnJinJieClick);
		EventTriggerListener.Get(this.pCloseBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnJinJieClick);
		bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(this.m_nZfqItemId);
		if (flag)
		{
			ItemData itemData = fGameManager.mloadConfig.AllItemData[this.m_nZfqItemId];
			this.pBuyCglBtn.name = itemData.m_nShopId.ToString();
		}
		EventTriggerListener.Get(this.pBuyCglBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyItem);
		EventTriggerListener.Get(this.pBuyCaiLiaoBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnBuyItem);
		this.pZuoBtn = this.UIGameObj.Get_GameObj(15);
		this.pYouBtn = this.UIGameObj.Get_GameObj(16);
		this.pInputField = this.UIGameObj.Get_GameObj(17).GetComponent<InputField>();
		this.pInputField.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChang));
		EventTriggerListener.Get(this.pZuoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.ChangeCountClick);
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pYouBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.ChangeCountClick);
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x00127674 File Offset: 0x00125874
	private void OnCheckToggle(GameObject obj)
	{
		bool flag = this.m_nMaxDaoJuNum == 0;
		if (flag)
		{
			this.pCheckTog_2.isOn = false;
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(27032, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.isCheck_2 = this.pCheckTog_2.isOn;
			bool flag2 = this.isCheck_2;
			if (flag2)
			{
				this.pInputField.text = this.m_nMaxDaoJuNum.ToString();
				bool flag3 = this.nRandNum + this.CurCount * 5 == 100;
				if (flag3)
				{
					this.tChengGongLvText.text = Language.GetLang(27049, new object[]
					{
						this.nRandNum + this.CurCount * 5
					});
				}
				else
				{
					this.tChengGongLvText.text = Language.GetLang(27033, new object[]
					{
						this.nRandNum + this.CurCount * 5
					});
				}
			}
			else
			{
				this.pInputField.text = "0";
			}
		}
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x001277BC File Offset: 0x001259BC
	public void OnCheckMaxNum(HeroOneAttData UserOneAttData)
	{
		ArrayList szJinJieRand = this.HeroTypeXmlData.szJinJieRand;
		ArrayList arrayList = (ArrayList)szJinJieRand[UserOneAttData.m_nJieLv];
		this.nRandNum = (int)arrayList[1];
		bool flag = this.nRandNum >= 100;
		if (flag)
		{
			this.pCheckTog_2.gameObject.SetActive(false);
		}
		else
		{
			this.pCheckTog_2.gameObject.SetActive(true);
		}
		this.CurCount = 0;
		this.OnZengFuQi(this.m_nZfqItemId);
		this.pInputField.text = "0";
		this.OnValueChang(this.CurCount.ToString());
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x0012786C File Offset: 0x00125A6C
	private void OnValueChang(string stext)
	{
		bool flag = this.pInputField.text == string.Empty;
		if (flag)
		{
			this.pInputField.text = "0";
		}
		bool flag2 = this.pInputField.text == "-";
		if (flag2)
		{
			this.pInputField.text = "0";
		}
		this.CurCount = int.Parse(this.pInputField.text);
		bool flag3 = this.CurCount > this.m_nMaxDaoJuNum;
		if (flag3)
		{
			this.pInputField.text = this.m_nMaxDaoJuNum.ToString();
		}
		bool flag4 = this.CurCount < 1;
		if (flag4)
		{
			this.pInputField.text = "0";
		}
		this.CurCount = int.Parse(this.pInputField.text);
		bool flag5 = this.CurCount == 0;
		if (flag5)
		{
			this.pCheckTog_2.isOn = false;
		}
		int num = (!this.pCheckTog_2.isOn) ? this.nRandNum : (this.nRandNum + this.CurCount * 5);
		bool flag6 = num == 100;
		if (flag6)
		{
			this.tChengGongLvText.text = Language.GetLang(27049, new object[]
			{
				num
			});
		}
		else
		{
			this.tChengGongLvText.text = Language.GetLang(27033, new object[]
			{
				num
			});
		}
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x001279EC File Offset: 0x00125BEC
	public void ChangeCountClick(GameObject obj)
	{
		string name = obj.name;
		this.CurCount = int.Parse(this.pInputField.text);
		bool flag = name == "zuobtn";
		if (flag)
		{
			bool flag2 = this.CurCount == 0;
			if (flag2)
			{
				return;
			}
			this.CurCount--;
		}
		bool flag3 = name == "youbtn";
		if (flag3)
		{
			bool flag4 = this.CurCount >= this.m_nMaxDaoJuNum;
			if (flag4)
			{
				return;
			}
			this.CurCount++;
		}
		this.pInputField.text = this.CurCount.ToString();
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x00127A98 File Offset: 0x00125C98
	private void Start()
	{
		this.IsLoadTrue = true;
		bool flag = this.HeroTypeXmlData == null;
		if (flag)
		{
			this.OnShowInfo();
		}
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x00127AC4 File Offset: 0x00125CC4
	public void OnSetData(int nHeroType, int nAttType, bool isCheck)
	{
		this.m_nHeroType = nHeroType;
		this.m_nAttType = nAttType;
		this.isCheck_1 = false;
		bool isLoadTrue = this.IsLoadTrue;
		if (isLoadTrue)
		{
			this.OnShowInfo();
		}
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x00127AFC File Offset: 0x00125CFC
	public void OnShowInfo()
	{
		this.HeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[this.m_nHeroType];
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[this.m_nAttType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, qiangHuaAttributeData.nType);
		this.OnCreateAttQhInfo(qiangHuaAttributeData, attDataByType);
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x00127B64 File Offset: 0x00125D64
	private void OnCreateAttQhInfo(QiangHuaAttributeData attdata, HeroOneAttData UserOneAttData)
	{
		string imageName = (this.m_nHeroType == 1) ? "huang" : ((this.m_nHeroType != 2) ? "lv" : "lan");
		int nType = attdata.nType;
		this.tJinJieLv.text = UserOneAttData.m_nJieLv.ToString() + Language.GetLang(27050, new object[0]);
		string format = "qianghuatubiao/{0}.txt";
		fUIManager.GetInstance().GetIconAsset(string.Format(format, attdata.m_sIco), this.rJinJieIco);
		this.mJinBgLv.sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, imageName);
		this.tZhanDuLiValue.text = PveManager.GetInstance().GetZhanDuLi(this.m_nHeroType).ToString();
		this.tZhanDuLiValueAdd.text = (PveManager.GetInstance().GetZdlValue(0, UserOneAttData.m_nJieLv + 1) - PveManager.GetInstance().GetZdlValue(UserOneAttData.m_nQhLv, UserOneAttData.m_nJieLv)).ToString();
		this.OnCheckMaxNum(UserOneAttData);
		int jinJieItem = this.HeroTypeXmlData.GetJinJieItem();
		ItemData itemData = fGameManager.mloadConfig.AllItemData[jinJieItem];
		this.m_nXiaoHaoItemNum = PveManager.GetInstance().GetQingHuaItem(nType, this.HeroTypeXmlData);
		this.tXiaoCaiLiao.text = this.m_nXiaoHaoItemNum.ToString();
		format = ShopManager.GetIcoPath(itemData.m_nType);
		fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sIco), this.rCaiLiaoIco);
		this.pBuyCaiLiaoBtn.name = itemData.m_nShopId.ToString();
		this.m_nXiaoHaoId = jinJieItem;
		this.UpdateItemNum(this.m_nXiaoHaoId);
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x00127D1C File Offset: 0x00125F1C
	public void UpdateItemNum(int nItemId)
	{
		bool flag = this.m_nXiaoHaoId == nItemId;
		if (flag)
		{
			int itemNumById = ShopManager.GetInstance().GetItemNumById(this.m_nXiaoHaoId);
			bool flag2 = itemNumById >= this.m_nXiaoHaoItemNum;
			if (flag2)
			{
				this.tCaiLiaoHave.text = Language.GetLang(27035, new object[]
				{
					itemNumById
				});
			}
			else
			{
				this.tCaiLiaoHave.text = Language.GetLang(27036, new object[]
				{
					itemNumById
				});
			}
		}
		bool flag3 = this.m_nZfqItemId == nItemId;
		if (flag3)
		{
			this.OnZengFuQi(this.m_nZfqItemId);
		}
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x00127DC8 File Offset: 0x00125FC8
	public void OnZengFuQi(int nItemId)
	{
		bool flag = nItemId == this.m_nZfqItemId;
		if (flag)
		{
			int itemNumById = ShopManager.GetInstance().GetItemNumById(this.m_nZfqItemId);
			this.tCglShengYu.text = Language.GetLang(27012, new object[0]) + itemNumById.ToString();
			this.m_nMaxDaoJuNum = (100 - this.nRandNum) / 5;
			this.m_nMaxDaoJuNum = ((this.m_nMaxDaoJuNum <= itemNumById) ? this.m_nMaxDaoJuNum : itemNumById);
		}
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x00127E4C File Offset: 0x0012604C
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

	// Token: 0x0600201B RID: 8219 RVA: 0x00127E9C File Offset: 0x0012609C
	private void OnJinJieClick(GameObject obj)
	{
		int num = int.Parse(obj.name.Replace("CreateBtn_", string.Empty));
		bool flag = num == 1;
		if (flag)
		{
			this.OnQHClick();
		}
		else
		{
			this.Close();
			this.pCheckTog_2.isOn = false;
		}
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x00127EEC File Offset: 0x001260EC
	public void OnQHClick()
	{
		int nItemNum = 0;
		int nItemId = 0;
		int nLvType = 0;
		int nAttType = this.m_nAttType;
		QiangHuaAttributeData qiangHuaAttributeData = this.HeroTypeXmlData.AllAttributeData[nAttType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(this.m_nHeroType, nAttType);
		bool flag = attDataByType.m_nJieLv >= this.HeroTypeXmlData.nMaxJie;
		if (!flag)
		{
			bool flag2 = attDataByType.m_nQhLv == qiangHuaAttributeData.nMaxLv;
			if (flag2)
			{
				ArrayList szJinJieRand = this.HeroTypeXmlData.szJinJieRand;
				ArrayList arrayList = (ArrayList)szJinJieRand[attDataByType.m_nJieLv];
				bool flag3 = attDataByType.m_nJieLv >= this.HeroTypeXmlData.nMaxJie;
				if (flag3)
				{
					return;
				}
				nItemId = (int)arrayList[2];
				nItemNum = PveManager.GetInstance().GetUpJieItemNum(attDataByType, qiangHuaAttributeData, this.HeroTypeXmlData);
				nLvType = 2;
			}
			ArrayList arrayList2 = PveManager.GetInstance().OnXiaoHaoCaiLiao(nItemId, nItemNum, this.isCheck_1, this.isGetCheck(), nLvType, true, this.CurCount);
			int num = (int)arrayList2[0];
			int num2 = (int)arrayList2[1];
			bool flag4 = num < 1;
			if (flag4)
			{
				bool flag5 = num == -4;
				if (flag5)
				{
					fUIManager.GetInstance().OpenChongZhiUI("ShopBuyItemUICon");
				}
				this.UpdateQhResult(num);
			}
			else
			{
				bool flag6 = num2 > 0 && !this.IsCheckMoney;
				if (flag6)
				{
					string content = string.Empty;
					content = Language.GetLang(27001, new object[]
					{
						num2
					});
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), content, Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnQiangHua), nAttType, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
				}
				else
				{
					PveManager.GetInstance().OnSendMsgToServer(19502, new ArrayList
					{
						this.m_nHeroType,
						nAttType,
						this.isCheck_1 ? 1 : 0,
						this.isGetCheck() ? 1 : 0,
						this.CurCount
					});
					this.Close();
				}
			}
		}
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x001281AC File Offset: 0x001263AC
	private bool isGetCheck()
	{
		return this.isCheck_2 && this.CurCount > 0;
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x001281DC File Offset: 0x001263DC
	public void UpdateQhResult(int nResult)
	{
		string content = string.Empty;
		string text;
		if (nResult > 0)
		{
			text = Language.GetLang(27006, new object[0]);
		}
		else
		{
			if (!true)
			{
			}
			string lang;
			if (nResult <= -10)
			{
				if (nResult == -100)
				{
					lang = Language.GetLang(27008, new object[0]);
					goto IL_A5;
				}
				if (nResult == -10)
				{
					lang = Language.GetLang(27009, new object[0]);
					goto IL_A5;
				}
			}
			else
			{
				if (nResult == -7)
				{
					lang = Language.GetLang(27032, new object[0]);
					goto IL_A5;
				}
				if (nResult == -4)
				{
					lang = Language.GetLang(27007, new object[0]);
					goto IL_A5;
				}
			}
			lang = Language.GetLang(27010, new object[0]);
			IL_A5:
			if (!true)
			{
			}
			text = lang;
		}
		content = text;
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), content, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x001282D0 File Offset: 0x001264D0
	private void OnTipToggle(bool bstate)
	{
		this.IsCheckMoney = bstate;
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x001282DA File Offset: 0x001264DA
	private void OnCancel(object obj)
	{
		this.IsCheckMoney = false;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x001282E4 File Offset: 0x001264E4
	private void OnQiangHua(object obj)
	{
		int num = (int)obj;
		PveManager.GetInstance().OnSendMsgToServer(19502, new ArrayList
		{
			this.m_nHeroType,
			num,
			this.isCheck_1 ? 1 : 0,
			this.isGetCheck() ? 1 : 0,
			this.CurCount
		});
		this.Close();
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x00128376 File Offset: 0x00126576
	private void Update()
	{
	}

	// Token: 0x04001D4F RID: 7503
	public static QingHuaJinJieUICon inst;

	// Token: 0x04001D50 RID: 7504
	private int m_nHeroType;

	// Token: 0x04001D51 RID: 7505
	private int m_nAttType;

	// Token: 0x04001D52 RID: 7506
	private Image mJinBgLv;

	// Token: 0x04001D53 RID: 7507
	private RawImage rJinJieIco;

	// Token: 0x04001D54 RID: 7508
	private Text tJinJieLv;

	// Token: 0x04001D55 RID: 7509
	private Text tZhanDuLiValue;

	// Token: 0x04001D56 RID: 7510
	private Text tZhanDuLiValueAdd;

	// Token: 0x04001D57 RID: 7511
	private Text tChengGongLvText;

	// Token: 0x04001D58 RID: 7512
	private GameObject pCheckTog;

	// Token: 0x04001D59 RID: 7513
	private Text tCglShengYu;

	// Token: 0x04001D5A RID: 7514
	private GameObject pBuyCglBtn;

	// Token: 0x04001D5B RID: 7515
	private Text tXiaoCaiLiao;

	// Token: 0x04001D5C RID: 7516
	private RawImage rCaiLiaoIco;

	// Token: 0x04001D5D RID: 7517
	private Text tCaiLiaoHave;

	// Token: 0x04001D5E RID: 7518
	private GameObject pBuyCaiLiaoBtn;

	// Token: 0x04001D5F RID: 7519
	private GameObject pShengJiBtn;

	// Token: 0x04001D60 RID: 7520
	private GameObject pCloseBtn;

	// Token: 0x04001D61 RID: 7521
	private Toggle pCheckTog_2;

	// Token: 0x04001D62 RID: 7522
	private int m_nXiaoHaoId;

	// Token: 0x04001D63 RID: 7523
	private int m_nChongGongId;

	// Token: 0x04001D64 RID: 7524
	private GameObject pZuoBtn;

	// Token: 0x04001D65 RID: 7525
	private GameObject pYouBtn;

	// Token: 0x04001D66 RID: 7526
	private InputField pInputField;

	// Token: 0x04001D67 RID: 7527
	private bool IsLoadTrue;

	// Token: 0x04001D68 RID: 7528
	private int nRandNum;

	// Token: 0x04001D69 RID: 7529
	private int CurCount;

	// Token: 0x04001D6A RID: 7530
	private int m_nMaxDaoJuNum;

	// Token: 0x04001D6B RID: 7531
	private int m_nXiaoHaoItemNum;

	// Token: 0x04001D6C RID: 7532
	private QiangHuaHeroTypeXmlData HeroTypeXmlData;

	// Token: 0x04001D6D RID: 7533
	private bool isCheck_2;

	// Token: 0x04001D6E RID: 7534
	private bool isCheck_1;

	// Token: 0x04001D6F RID: 7535
	private int m_nZfqItemId = 6022;

	// Token: 0x04001D70 RID: 7536
	private bool IsCheckMoney;
}
