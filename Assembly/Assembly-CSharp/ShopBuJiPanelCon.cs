using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000308 RID: 776
public class ShopBuJiPanelCon : Window
{
	// Token: 0x06002575 RID: 9589 RVA: 0x00151FD0 File Offset: 0x001501D0
	protected override void Awake()
	{
		base.Awake();
		this.pOpenBtn = this.UIGameObj.Get_GameObj(0);
		this.pItemName = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.pZuoBtn = this.UIGameObj.Get_GameObj(1);
		EventTriggerListener.Get(this.pZuoBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeItem);
		EventTriggerListener.Get(this.pZuoBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.pYouBtn = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(this.pYouBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickChangeItem);
		EventTriggerListener.Get(this.pYouBtn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.pOpenBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOpen);
		EventTriggerListener.Get(this.pOpenBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.ZhuangBeiBtn = this.UIGameObj.Get_GameObj(4);
		this.ModleTipObj = this.UIGameObj.Get_GameObj(7);
		this.ModleTipObj.SetActive(false);
		this.pBoxPanel = this.UIGameObj.Get_GameObj(11);
		this.LiBaoBoxItem = this.UIGameObj.Get_GameObj(12).GetComponent<RawImage>();
		EventTriggerListener.Get(this.LiBaoBoxItem.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOpen);
		this.pFanHuiBtn = this.UIGameObj.Get_GameObj(13);
		this.pBuy_Btn_All = this.UIGameObj.Get_GameObj(18);
		this.TBuy_Btn_Txt = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		EventTriggerListener.Get(this.pFanHuiBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFanHui);
		EventTriggerListener.Get(this.pFanHuiBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ZhuangBeiBtn).onDown = new EventTriggerListener.VoidDelegate(this.OnClickZhuangBei);
		EventTriggerListener.Get(this.ZhuangBeiBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.ZhuangBeiBtnText = this.ZhuangBeiBtn.transform.FindChild("Text").GetComponent<Text>();
		for (int i = 1; i < 5; i++)
		{
			this.pModelDescList.Add(i, this.UIGameObj.Get_GameObj(i + 13));
			this.pModelDescList[i].SetActive(false);
		}
		EventTriggerListener.Get(this.pBuy_Btn_All).onClick = new EventTriggerListener.VoidDelegate(this.OnClickOpenAll);
		EventTriggerListener.Get(this.pBuy_Btn_All).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x0015229F File Offset: 0x0015049F
	public void InitShowLiBao()
	{
		this.nIndex = 0;
		this.nSelectItemId = 0;
		this.ShowLiBao();
		this.SetBackgroundUI();
		this.UpdatePageBtnState();
		this.OnEffPlayOver(1);
		ChatManager.GetInstance().OnShowOrHideChatWin(false);
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x001522DC File Offset: 0x001504DC
	private string GetPinZhiName(int nPinZhi)
	{
		string empty = string.Empty;
		if (!true)
		{
		}
		string result;
		switch (nPinZhi)
		{
		case 1:
			result = "bai";
			break;
		case 2:
			result = "lv";
			break;
		case 3:
			result = "lan";
			break;
		case 4:
			result = "zi";
			break;
		case 5:
			result = "cheng";
			break;
		default:
			result = "bai";
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x0015234B File Offset: 0x0015054B
	public void InitData()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x0015235C File Offset: 0x0015055C
	private void OnClickChangeItem(GameObject go)
	{
		bool flag = this.isOPenIng;
		if (!flag)
		{
			int count = PlayerDataManager.getInstance().HUserData.m_szShopItemData.Count;
			bool flag2 = go.name == "PreBtn";
			if (flag2)
			{
				bool flag3 = this.nIndex <= 0;
				if (flag3)
				{
					return;
				}
				this.nIndex -= 2;
			}
			bool flag4 = go.name == "NextBtn";
			if (flag4)
			{
				bool flag5 = this.nIndex >= count - 2;
				if (flag5)
				{
					return;
				}
				this.nIndex += 2;
			}
			this.pBoxPanel.SetActive(false);
			this.ShowMoldeTaiZai(false);
			this.ShowMoldeDesc(false);
			fUIManager.OnClearObj(this.modelObjList);
			this.LoadAllInEff("shopdonghua", 0, "xiangzi_guang_1");
			this.ShowLiBao();
			this.UpdatePageBtnState();
		}
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x00152450 File Offset: 0x00150650
	public void ShowLiBao()
	{
		int count = PlayerDataManager.getInstance().HUserData.m_szShopItemData.Count;
		bool flag = count == 0;
		if (flag)
		{
			this.nSelectItemId = 0;
			this.pItemName.text = "<color=#ff0000>" + Language.GetLang(39500, new object[0]) + "</color>";
			this.pOpenBtn.SetActive(true);
			this.TBuy_Btn_Txt.text = Language.GetLang(39501, new object[0]);
			this.pOpenBtn.GetComponent<Button>().interactable = false;
			this.pBuy_Btn_All.GetComponent<Button>().interactable = false;
		}
		else
		{
			bool flag2 = this.pOpenBtn != null;
			if (flag2)
			{
				this.pOpenBtn.GetComponent<Button>().interactable = true;
			}
			bool flag3 = this.pBuy_Btn_All != null;
			if (flag3)
			{
				this.pBuy_Btn_All.GetComponent<Button>().interactable = true;
			}
			bool flag4 = this.nIndex >= count;
			if (flag4)
			{
				this.nIndex = 0;
			}
			int num = int.Parse(PlayerDataManager.getInstance().HUserData.m_szShopItemData[this.nIndex].ToString());
			bool flag5 = this.nSelectItemId == num;
			if (!flag5)
			{
				bool flag6 = !fGameManager.mloadConfig.AllItemData.ContainsKey(num);
				if (flag6)
				{
					Debug.Log("物品配表错误Id==" + num.ToString());
				}
				else
				{
					this.pItemData = fGameManager.mloadConfig.AllItemData[num];
					bool flag7 = this.pItemName != null;
					if (flag7)
					{
						this.pItemName.text = this.pItemData.m_sName + " * " + PlayerDataManager.getInstance().HUserData.m_szShopItemData[this.nIndex + 1].ToString();
					}
					this.ShowMoldeTaiZai(false);
					bool flag8 = this.pBoxPanel != null;
					if (flag8)
					{
						this.pBoxPanel.SetActive(false);
					}
					fUIManager.OnClearObj(this.modelObjList);
					this.nSelectItemId = num;
				}
			}
		}
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x00152680 File Offset: 0x00150880
	public void UpdateShowLiBaoItem(int nItemId, int nNum)
	{
		bool flag = this.nSelectItemId == 0 || nNum == 0;
		if (flag)
		{
			this.ShowLiBao();
			this.UpdatePageBtnState();
		}
		else
		{
			bool flag2 = this.pItemName != null;
			if (flag2)
			{
				this.pItemName.text = this.pItemData.m_sName + " * " + PlayerDataManager.getInstance().HUserData.m_szShopItemData[this.nIndex + 1].ToString();
			}
		}
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x00152708 File Offset: 0x00150908
	public void UpdatePageBtnState()
	{
		bool flag = !(this.pZuoBtn == null);
		if (flag)
		{
			int count = PlayerDataManager.getInstance().HUserData.m_szShopItemData.Count;
			bool flag2 = count <= 2;
			if (flag2)
			{
				this.pZuoBtn.SetActive(false);
				this.pYouBtn.SetActive(false);
			}
			else
			{
				bool flag3 = this.nIndex <= 0;
				if (flag3)
				{
					this.pZuoBtn.SetActive(false);
					this.pYouBtn.SetActive(true);
				}
				else
				{
					bool flag4 = this.nIndex == count - 2;
					if (flag4)
					{
						this.pZuoBtn.SetActive(true);
						this.pYouBtn.SetActive(false);
					}
					else
					{
						this.pZuoBtn.SetActive(true);
						this.pYouBtn.SetActive(true);
					}
				}
			}
		}
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x001527E7 File Offset: 0x001509E7
	public void OnLiBaoOpenAll(ArrayList szData)
	{
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x001527EC File Offset: 0x001509EC
	public void OnLiBaoOpen(ArrayList arr, int nOpenType)
	{
		this.pBoxPanel.SetActive(false);
		fUIManager.OnClearObj(this.modelObjList);
		this.AllItemModelData.Clear();
		this.HidePinZhi();
		bool flag = this.pModelPanel != null;
		if (flag)
		{
			this.pModelPanel.SetActive(true);
		}
		int num = 0;
		while (num < arr.Count && num < this.szEffOpenShunXu.Length)
		{
			bool flag2 = !(this.GetRawImg(num + 1) == null);
			if (flag2)
			{
				this.OnDelRawImage(num + 1);
				ArrayList arrayList = (ArrayList)arr[num];
				int key = (int)arrayList[0];
				bool flag3 = !fGameManager.mloadConfig.AllItemData.ContainsKey(key);
				if (flag3)
				{
					Debug.LogError("物品Id出错===Id=" + key.ToString());
				}
				else
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
					int nType = itemData.m_nType;
					int nHeroId = itemData.m_nHeroId;
					int nTypeId = itemData.m_nTypeId;
					ShopItemModelData value = new ShopItemModelData
					{
						nId = this.szEffOpenShunXu[num],
						nType = nType,
						nHeroId = nHeroId,
						nTypeId = nTypeId,
						pItemData = itemData
					};
					this.AllItemModelData.Add(this.szEffOpenShunXu[num], value);
				}
			}
			IL_158:
			num++;
			continue;
			goto IL_158;
		}
		bool flag4 = base.gameObject.activeSelf && ShopUICon.inst.gameObject.activeSelf;
		if (flag4)
		{
			base.StartCoroutine(this.OnWaitEffShow(0, nOpenType));
		}
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x001529B0 File Offset: 0x00150BB0
	private void OnDelRawImage(int nNum)
	{
		RawImage rawImg = this.GetRawImg(nNum);
		GetIconComponent component = rawImg.gameObject.GetComponent<GetIconComponent>();
		bool flag = component != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(component);
		}
		UnityEngine.Object.DestroyImmediate(rawImg.texture);
		rawImg.gameObject.SetActive(false);
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x001529FF File Offset: 0x00150BFF
	private IEnumerator OnWaitEffShow(int nIndex, int nOpenType)
	{
		int nType = 0;
		int nHeroId = 0;
		int nTypeId = 0;
		int nId = this.szEffOpenShunXu[nIndex];
		bool flag = !this.AllItemModelData.ContainsKey(nId);
		if (flag)
		{
			yield break;
		}
		ShopItemModelData ModelData = this.AllItemModelData[nId];
		ItemData pItemData = ModelData.pItemData;
		nType = ModelData.nType;
		nHeroId = ModelData.nHeroId;
		nTypeId = ModelData.nTypeId;
		this.OnShowPinZhi(nId, pItemData.m_nQuality);
		this.GetTaiZi(nId).SetActive(true);
		bool flag2 = this.EffObjList.ContainsKey(nId);
		if (flag2)
		{
			this.EffObjList[nId].SetActive(false);
		}
		ArrayList szHuiGouData = ShopManager.GetInstance().GetHuiGouInfo(nId);
		bool flag3 = szHuiGouData != null && (int)szHuiGouData[1] > 0;
		if (flag3)
		{
			bool flag4 = this.EffObjList.ContainsKey(nId);
			if (flag4)
			{
				this.EffObjList[nId].SetActive(true);
			}
			else
			{
				this.LoadAllInEff("shopjinbidonghua", nId, "jinbi");
			}
		}
		bool flag5 = this.isHero(nType);
		if (flag5)
		{
			Vector3 localPosition = this.modelScn.transform.FindChild("taizi" + nId.ToString()).localPosition;
			localPosition.y = -0.43f;
			this.GetRawImg(nId).gameObject.SetActive(false);
			this.LoadHeroModel(nType, nHeroId, nTypeId, localPosition, nId);
			localPosition = default(Vector3);
		}
		else
		{
			string empty = string.Empty;
			this.GetRawImg(nId).gameObject.SetActive(true);
			string icoPath = ShopManager.GetIcoPath(pItemData.m_nType);
			fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, pItemData.m_sIco), this.GetRawImg(nId));
			icoPath = null;
		}
		this.OnSetModleTipDesc(nId);
		yield return new WaitForSeconds(0.4f);
		bool flag6 = nIndex == this.szEffOpenShunXu.Length - 1 || nIndex == this.AllItemModelData.Count - 1;
		if (flag6)
		{
			this.OnEffPlayOver(nOpenType);
		}
		else
		{
			base.StartCoroutine(this.OnWaitEffShow(nIndex + 1, nOpenType));
		}
		yield break;
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x00152A1C File Offset: 0x00150C1C
	private void OnSetModleTipDesc(int nBianHao)
	{
		GameObject gameObject = this.pModelDescList[nBianHao];
		gameObject.SetActive(true);
		Text component = gameObject.transform.FindChild("Image (1)/itemdesc").GetComponent<Text>();
		GameObject gameObject2 = gameObject.transform.FindChild("Image (2)").gameObject;
		Text component2 = gameObject.transform.FindChild("Image (2)/jianglitext").GetComponent<Text>();
		RawImage component3 = gameObject.transform.FindChild("Image (1)/RawImage").GetComponent<RawImage>();
		ShopItemModelData shopItemModelData = this.AllItemModelData[nBianHao];
		ArrayList huiGouInfo = ShopManager.GetInstance().GetHuiGouInfo(nBianHao);
		string format = (shopItemModelData.nType == 7 || shopItemModelData.nType == 5) ? ShopManager.GetIcoPath(shopItemModelData.nType) : ((shopItemModelData.nType != 15) ? "kaixiangico/{0}.txt" : "userface/{0}.txt");
		fUIManager.GetInstance().GetIconAsset(string.Format(format, shopItemModelData.pItemData.m_sLiBaoIco), component3);
		bool flag = huiGouInfo == null;
		if (flag)
		{
			gameObject2.SetActive(false);
			component.text = shopItemModelData.pItemData.m_sLiBaoDesc;
		}
		else
		{
			bool flag2 = (int)huiGouInfo[1] < 1;
			if (flag2)
			{
				gameObject2.SetActive(false);
			}
			else
			{
				gameObject2.SetActive(true);
				component2.text = Language.GetLang(39502, new object[]
				{
					huiGouInfo[2].ToString()
				});
			}
			component.text = string.Format(shopItemModelData.pItemData.m_sLiBaoDesc, huiGouInfo[4].ToString());
		}
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x00152BB0 File Offset: 0x00150DB0
	private void OnEffPlayOver(int nOpenType)
	{
		this.isOPenIng = false;
		bool flag = nOpenType == 1 || !this.isPiLiangOpen;
		if (flag)
		{
			this.pOpenBtn.SetActive(true);
			this.pBuy_Btn_All.SetActive(true);
		}
		else
		{
			bool flag2 = this.isPiLiangOpen && nOpenType == 2;
			if (flag2)
			{
				bool flag3 = base.IsInvoking("OnPiLiangNext");
				if (flag3)
				{
					base.CancelInvoke("OnPiLiangNext");
				}
				base.Invoke("OnPiLiangNext", 1f);
			}
		}
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x00152C3A File Offset: 0x00150E3A
	private void OnPiLiangNext()
	{
		this.OnOpenLiBaoToServer(2);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x00152C48 File Offset: 0x00150E48
	private bool isHero(int nType)
	{
		return nType == 3 || nType == 4 || nType == 5 || nType == 6 || nType == 7;
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x00152C7B File Offset: 0x00150E7B
	public void OnClickOpen(GameObject obj)
	{
		this.OnOpenLiBaoToServer(1);
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x00152C88 File Offset: 0x00150E88
	private void OnOpenLiBaoToServer(int nType)
	{
		bool flag = !this.isOPenIng && !(this.modelScn == null) && this.EffObjList.Count != 0 && this.nSelectItemId != 0;
		if (flag)
		{
			this.pOpenBtn.SetActive(false);
			this.pBoxPanel.SetActive(false);
			this.ShowMoldeTaiZai(false);
			this.ShowMoldeDesc(false);
			bool flag2 = nType == 1;
			if (flag2)
			{
				this.pBuy_Btn_All.SetActive(false);
			}
			fUIManager.OnClearObj(this.modelObjList);
			this.LoadAllInEff("shopdonghua", 0, "xiangzi_kai");
			base.StartCoroutine(this.EffPalyOver(nType));
			this.isOPenIng = true;
			this.isOpenBox = true;
			fUISoundManager.instance.PlayUISound("shop_opencase", 0, null, 20);
			SC_shakeCamera.shakeCamera();
		}
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x00152D63 File Offset: 0x00150F63
	private IEnumerator EffPalyOver(int nType)
	{
		yield return new WaitForSeconds(1.5f);
		ShopManager.mInst.m_szHuiGouData.Clear();
		ShopManager.ShopClientToServer(70006, new ArrayList
		{
			this.nSelectItemId,
			nType
		});
		yield break;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x00152D7C File Offset: 0x00150F7C
	private void OnClickOpenAll(GameObject obj)
	{
		bool flag = this.modelScn == null || this.EffObjList.Count == 0 || this.nSelectItemId == 0;
		if (!flag)
		{
			bool flag2 = !this.isPiLiangOpen;
			if (flag2)
			{
				this.isPiLiangOpen = true;
				this.TBuy_Btn_Txt.text = Language.GetLang(39503, new object[0]);
				this.OnOpenLiBaoToServer(2);
			}
			else
			{
				this.isPiLiangOpen = false;
				bool flag3 = !this.isOPenIng;
				if (flag3)
				{
					this.pOpenBtn.SetActive(true);
				}
				this.TBuy_Btn_Txt.text = Language.GetLang(39501, new object[0]);
			}
		}
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x00152E32 File Offset: 0x00151032
	private void OnClickFanHui(GameObject obj)
	{
		ShopUICon.inst.ReBack();
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x00152E40 File Offset: 0x00151040
	private void OnClickZhuangBei(GameObject obj)
	{
		bool flag = this.OnSelectShowTipId != 0;
		if (flag)
		{
			this.isClickZhuangBeiBtn = true;
			ShopItemModelData shopItemModelData = this.AllItemModelData[this.OnSelectShowTipId];
			bool flag2 = shopItemModelData.nType == 15;
			if (flag2)
			{
				HeroDataManager.GetInstance().SendHeroDataMsgToServer(11003, new ArrayList
				{
					shopItemModelData.nTypeId,
					0
				});
			}
			else
			{
				bool flag3 = shopItemModelData.nType == 8;
				if (flag3)
				{
					HeroDataManager.GetInstance().SendHeroDataMsgToServer(11010, new ArrayList
					{
						shopItemModelData.nTypeId,
						0
					});
				}
				else
				{
					HeroDataManager.GetInstance().SendHeroDataMsgToServer(12009, new ArrayList
					{
						shopItemModelData.nHeroId,
						shopItemModelData.GetZhuanBeiType(),
						shopItemModelData.nTypeId
					});
				}
			}
			this.UpdateZhuangBieState(0, true);
		}
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x00152F60 File Offset: 0x00151160
	private void UpdateZhuangBieState(int ShowTipId, bool IsZhuangBei)
	{
		bool flag = !IsZhuangBei;
		if (flag)
		{
			this.ZhuangBeiBtnText.text = Language.GetLang(39504, new object[0]);
			this.ZhuangBeiBtn.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.ZhuangBeiBtnText.text = Language.GetLang(39505, new object[0]);
			this.ZhuangBeiBtn.GetComponent<Button>().interactable = false;
		}
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x00152FD8 File Offset: 0x001511D8
	private bool OnZhuangBei(int nType, int nHeroId, int nId)
	{
		bool flag = true;
		bool result;
		if (nType != 8)
		{
			if (nType != 15)
			{
				OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(nHeroId);
				bool flag2 = oneHeroData == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					switch (nType)
					{
					case 4:
					{
						bool flag3 = oneHeroData.nUseSkin != nId;
						if (flag3)
						{
							flag = false;
						}
						break;
					}
					case 5:
					{
						bool flag4 = oneHeroData.nUseEmotion != nId;
						if (flag4)
						{
							flag = false;
						}
						break;
					}
					case 6:
					{
						bool flag5 = oneHeroData.nUseWeapon != nId;
						if (flag5)
						{
							flag = false;
						}
						break;
					}
					case 7:
					{
						bool flag6 = oneHeroData.nUsePose != nId;
						if (flag6)
						{
							flag = false;
						}
						break;
					}
					}
					result = flag;
				}
			}
			else
			{
				bool flag7 = nId == PlayerDataManager.getInstance().HUserData.m_nFace;
				result = flag7;
			}
		}
		else
		{
			bool flag8 = PlayerDataManager.getInstance().HUserData.m_nUsePaint == nId;
			result = flag8;
		}
		return result;
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x001530F8 File Offset: 0x001512F8
	public void OnModelEnter(GameObject obj)
	{
		bool flag = !this.isPiLiangOpen;
		if (flag)
		{
			int nId = int.Parse(obj.name.Substring(obj.name.Length - 1, 1));
			this.ShowModelTip(nId);
		}
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x0015313C File Offset: 0x0015133C
	public void OnModelDown(GameObject obj)
	{
		this.isClickZhuangBeiBtn = true;
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x00153148 File Offset: 0x00151348
	private void GetTheMouseOverPoint()
	{
		bool flag = !(this.modelScn == null);
		if (flag)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			int mask = LayerMask.GetMask(new string[]
			{
				"UIModel"
			});
			RaycastHit raycastHit;
			bool flag2 = Physics.Raycast(ray, out raycastHit, 200f, mask);
			if (flag2)
			{
				string name = raycastHit.transform.root.name;
				int nId = int.Parse(name.Substring(name.Length - 1, 1));
				this.ShowModelTip(nId);
			}
		}
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x001531DC File Offset: 0x001513DC
	private void GetTheClick()
	{
		bool flag = Camera.main == null;
		if (!flag)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			bool flag2 = Physics.Raycast(ray, out raycastHit);
			if (flag2)
			{
				bool flag3 = this.isClickZhuangBeiBtn;
				if (flag3)
				{
					this.isClickZhuangBeiBtn = false;
				}
				else
				{
					this.ModleTipObj.SetActive(false);
					this.OnSelectShowTipId = 0;
				}
			}
		}
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x00153244 File Offset: 0x00151444
	private void Update()
	{
		bool flag = this.isOpenBox;
		if (flag)
		{
			bool flag2 = !this.isPiLiangOpen;
			if (flag2)
			{
				this.GetTheMouseOverPoint();
			}
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			if (mouseButtonDown)
			{
				this.GetTheClick();
			}
		}
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x00153288 File Offset: 0x00151488
	public void ShowModelTip(int nId)
	{
		bool flag = this.OnSelectShowTipId != nId;
		if (flag)
		{
			ShopItemModelData shopItemModelData = this.AllItemModelData[nId];
			bool flag2 = (this.isHero(shopItemModelData.nType) || shopItemModelData.nType == 15 || shopItemModelData.nType == 8) && shopItemModelData.nType != 3;
			if (flag2)
			{
				this.ZhuangBeiBtn.SetActive(true);
				bool isZhuangBei = this.OnZhuangBei(shopItemModelData.nType, shopItemModelData.nHeroId, shopItemModelData.nTypeId);
				this.UpdateZhuangBieState(nId, isZhuangBei);
				this.ModleTipObj.GetComponent<RectTransform>().localPosition = this.modelTiplist[shopItemModelData.nId];
				this.ModleTipObj.SetActive(true);
				this.OnSelectShowTipId = nId;
			}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x00153358 File Offset: 0x00151558
	private RawImage GetRawImg(int nNum)
	{
		if (!true)
		{
		}
		RawImage result;
		switch (nNum)
		{
		case 1:
			result = this.ItemModel_1;
			break;
		case 2:
			result = this.ItemModel_2;
			break;
		case 3:
			result = this.ItemModel_3;
			break;
		case 4:
			result = this.ItemModel_4;
			break;
		default:
			result = null;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x001533B8 File Offset: 0x001515B8
	public void SetBackgroundUI()
	{
		bool flag = BackgroundUICon.inst == null;
		if (flag)
		{
			BackgroundUICon.isShowBack = false;
		}
		else
		{
			BackgroundUICon.inst.SetMainBackground(false, string.Empty);
		}
		this.ShowMoldeTaiZai(false);
		bool flag2 = !(this.sCurBg == this.sBgName) && !this.IsLoadIng;
		if (flag2)
		{
			this.IsLoadIng = true;
			string url = "UI/" + this.sBgName + ".ab";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag3 = resWWW == null;
			if (flag3)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadUIOK), this.sBgName);
			}
			else
			{
				this.LoadUIOK(resWWW, this.sBgName);
			}
			this.sCurBg = this.sBgName;
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x00153488 File Offset: 0x00151688
	public void ShowMoldeTaiZai(bool isShow)
	{
		bool flag = !(this.modelScn == null);
		if (flag)
		{
			bool flag2 = this.pModelPanel != null;
			if (flag2)
			{
				this.pModelPanel.SetActive(isShow);
			}
			this.isOpenBox = isShow;
			for (int i = 1; i < 5; i++)
			{
				GameObject taiZi = this.GetTaiZi(i);
				taiZi.SetActive(isShow);
			}
		}
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x001534F8 File Offset: 0x001516F8
	private GameObject GetTaiZi(int nNum)
	{
		return this.modelScn.transform.FindChild("taizi" + nNum.ToString()).gameObject;
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x00153530 File Offset: 0x00151730
	private void OnShowPinZhi(int nNum, int nPinZhi)
	{
		GameObject taiZi = this.GetTaiZi(nNum);
		GameObject gameObject = taiZi.transform.Find("pan_guang/" + this.GetPinZhiName(nPinZhi)).gameObject;
		gameObject.SetActive(true);
		this.AllPinZhiEffList.Add(gameObject);
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x00153580 File Offset: 0x00151780
	private void HidePinZhi()
	{
		foreach (GameObject gameObject in this.AllPinZhiEffList)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				gameObject.SetActive(false);
			}
		}
		this.AllPinZhiEffList.Clear();
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x001535F4 File Offset: 0x001517F4
	public void ShowMoldeDesc(bool isShow)
	{
		foreach (GameObject gameObject in this.pModelDescList.Values)
		{
			gameObject.SetActive(isShow);
		}
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x00153654 File Offset: 0x00151854
	private void LoadUIOK(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			bool flag2 = ShopUICon.inst != null && !ShopUICon.inst.visible;
			if (flag2)
			{
				this.IsLoadIng = false;
			}
			else
			{
				bool flag3 = this.modelScn != null;
				if (flag3)
				{
					UnityEngine.Object.DestroyImmediate(this.modelScn);
					this.modelScn = null;
				}
				this.modelScn = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				this.modelScn.transform.localScale = Vector3.one;
				this.modelScn.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.modelScn.name = this.sBgName;
				this.IsLoadIng = false;
				this.ShowMoldeTaiZai(false);
				UIGameObjList component = this.modelScn.GetComponent<UIGameObjList>();
				this.ItemModel_1 = component.Get_GameObj(0).GetComponent<RawImage>();
				this.ItemModel_2 = component.Get_GameObj(1).GetComponent<RawImage>();
				this.ItemModel_3 = component.Get_GameObj(2).GetComponent<RawImage>();
				this.ItemModel_4 = component.Get_GameObj(3).GetComponent<RawImage>();
				this.pModelPanel = component.Get_GameObj(5);
				EventTriggerListener.Get(this.ItemModel_1.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnModelEnter);
				EventTriggerListener.Get(this.ItemModel_2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnModelEnter);
				EventTriggerListener.Get(this.ItemModel_3.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnModelEnter);
				EventTriggerListener.Get(this.ItemModel_4.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnModelEnter);
				EventTriggerListener.Get(this.ItemModel_1.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnModelDown);
				EventTriggerListener.Get(this.ItemModel_2.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnModelDown);
				EventTriggerListener.Get(this.ItemModel_3.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnModelDown);
				EventTriggerListener.Get(this.ItemModel_4.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnModelDown);
				this.LoadAllInEff("shopdonghua", 0, "xiangzi_guang_1");
				this.shopCamera = component.Get_GameObj(6);
				this.shopCamera.AddComponent<SC_shakeCamera>();
			}
		}
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x001538DC File Offset: 0x00151ADC
	private void LoadHeroModel(int nType, int nHeroId, int nId, Vector3 szPos, int nBianHao)
	{
		bool flag = !(this.modelScn == null);
		if (flag)
		{
			HeroUnlockData heroUnlockData = null;
			bool flag2 = heroUnlockData == null;
			if (flag2)
			{
				heroUnlockData = fGameManager.mloadConfig.AllHeroUnlock[nHeroId];
			}
			string text = (nType != 4) ? HeroDataManager.GetInstance().GetUseSkinData(nHeroId, PlayerDataManager.getInstance().HUserData.m_nCid).tpsModel : heroUnlockData.szSkinUnlock[nId].tpsModel;
			text = "HeroAnimator/" + text + ".mo";
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadModelTpsFinish), new ArrayList
			{
				heroUnlockData,
				nType,
				nHeroId,
				nId,
				szPos,
				nBianHao
			});
		}
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x001539CC File Offset: 0x00151BCC
	private void LoadModelTpsFinish(WWW www, object obj)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			Vector3 vector = Vector3.zero;
			bool flag2 = www == null;
			if (!flag2)
			{
				ArrayList arrayList = (ArrayList)obj;
				HeroUnlockData heroUnlockData = (HeroUnlockData)arrayList[0];
				int num = (int)arrayList[1];
				int key = (int)arrayList[3];
				Vector3 localPosition = (Vector3)arrayList[4];
				int num2 = (int)arrayList[5];
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.name = "heroinfo_" + heroUnlockData.key.ToString() + "|" + num2.ToString();
				if (!true)
				{
				}
				float num3;
				switch (num2)
				{
				case 1:
					num3 = 165f;
					break;
				case 2:
					num3 = 195f;
					break;
				case 3:
					num3 = 150f;
					break;
				case 4:
					num3 = 210f;
					break;
				default:
					num3 = 180f;
					break;
				}
				if (!true)
				{
				}
				float y = num3;
				Transform[] componentsInChildren = gameObject.transform.GetComponentsInChildren<Transform>();
				foreach (Transform transform in componentsInChildren)
				{
					bool flag3 = transform.name.Contains("yinshen") || transform.name.Contains("hongxian") || transform.name.Contains("honwai");
					if (flag3)
					{
						transform.GetComponent<Renderer>().enabled = false;
					}
				}
				Transform transform2 = gameObject.transform.FindChild("body2");
				bool flag4 = transform2 != null;
				if (flag4)
				{
					transform2.gameObject.SetActive(false);
				}
				DynamicBone[] components = gameObject.transform.GetComponents<DynamicBone>();
				foreach (DynamicBone dynamicBone in components)
				{
					dynamicBone.b_CheckDis = false;
					dynamicBone.switchCheck = false;
				}
				gameObject.SetActive(true);
				this.modelObjList.Add(gameObject);
				Animator component = gameObject.transform.GetComponent<Animator>();
				Transform transform3 = gameObject.transform.Find("GrounderTrs");
				bool flag5 = transform3 != null;
				if (flag5)
				{
					UnityEngine.Object.DestroyImmediate(transform3.gameObject);
				}
				transform3 = gameObject.transform.Find("HitReactionTrs");
				bool flag6 = transform3 != null;
				if (flag6)
				{
					UnityEngine.Object.DestroyImmediate(transform3.gameObject);
				}
				string text = (num != 4) ? HeroDataManager.GetInstance().GetUseSkinData(heroUnlockData.key, PlayerDataManager.getInstance().HUserData.m_nCid).tpsControl : heroUnlockData.szSkinUnlock[key].tpsControl;
				int num4 = 0;
				switch (num)
				{
				case 3:
					num4 = 0;
					vector = heroUnlockData.ShopModelPos[num2 - 1];
					break;
				case 4:
					num4 = 0;
					vector = heroUnlockData.szSkinUnlock[key].ShopModelPos[num2 - 1];
					break;
				case 5:
					vector = heroUnlockData.szEmotionUnlock[key].ShopModelPos[num2 - 1];
					num4 = int.Parse(heroUnlockData.szEmotionUnlock[key].animParm);
					break;
				case 6:
					num4 = 0;
					vector = heroUnlockData.szWeaponUnlock[key].ShopModelPos[num2 - 1];
					break;
				case 7:
					num4 = int.Parse(heroUnlockData.szPoseUnlock[key].animParm);
					vector = heroUnlockData.szPoseUnlock[key].ShopModelPos[num2 - 1];
					break;
				}
				localPosition.x += vector.x;
				localPosition.y += vector.y;
				localPosition.z += vector.z;
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, y, 0f));
				gameObject.transform.localScale = Vector3.one * heroUnlockData.modelScale;
				LODGroup component2 = gameObject.GetComponent<LODGroup>();
				bool flag7 = component2 != null;
				if (flag7)
				{
					component2.size = 100f;
				}
				bool flag8 = text != "0";
				if (flag8)
				{
					gameObject.SetActive(false);
					GameRoot.LoadAsset("HeroAnimator/" + text + ".mo", new fResourceManager.DownFinishDelegate(this.LoadControlTpsFinish), new ArrayList
					{
						heroUnlockData,
						component,
						num4,
						gameObject
					});
				}
			}
		}
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x00153EBB File Offset: 0x001520BB
	private void OnClickModel(GameObject obj)
	{
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x00153EC0 File Offset: 0x001520C0
	private void LoadControlTpsFinish(WWW www, object customParam)
	{
		bool flag = www != null && www.error == null && www != null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)customParam;
			Animator animator = (Animator)arrayList[1];
			GameObject gameObject = (GameObject)arrayList[3];
			bool flag2 = !(animator == null);
			if (flag2)
			{
				int nState = (int)arrayList[2];
				animator.runtimeAnimatorController = (RuntimeAnimatorController)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
				gameObject.SetActive(true);
				base.StartCoroutine(this.PlayModelDoangHua(animator, nState));
			}
		}
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x00153F63 File Offset: 0x00152163
	private IEnumerator PlayModelDoangHua(Animator modelAni, int nState)
	{
		yield return new WaitForSeconds(0.2f);
		bool flag = modelAni != null && modelAni.isInitialized && modelAni.GetInteger("HuDongState") != nState;
		if (flag)
		{
			modelAni.SetInteger("HuDongState", nState);
		}
		yield break;
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x00153F80 File Offset: 0x00152180
	public void OnClearScn()
	{
		this.sCurBg = string.Empty;
		fUIManager.OnClearObj(this.modelObjList);
		UnityEngine.Object.DestroyImmediate(this.modelScn);
		this.OnDeleteEff();
		this.modelScn = null;
		bool flag = this.ModleTipObj != null;
		if (flag)
		{
			this.ModleTipObj.SetActive(false);
		}
		this.OnSelectShowTipId = 0;
		this.isOpenBox = false;
		this.isOPenIng = false;
		this.ShowMoldeDesc(false);
		this.isPiLiangOpen = false;
		bool flag2 = this.TBuy_Btn_Txt != null;
		if (flag2)
		{
			this.TBuy_Btn_Txt.text = Language.GetLang(39501, new object[0]);
		}
		bool flag3 = BackgroundUICon.inst == null;
		if (flag3)
		{
			BackgroundUICon.isShowBack = true;
		}
		else
		{
			BackgroundUICon.inst.SetMainBackground(true, string.Empty);
		}
		bool flag4 = ChatUICon.inst != null;
		if (flag4)
		{
			ChatManager.GetInstance().OnShowOrHideChatWin(true);
			ChatUICon.inst.UpdateScollValue();
		}
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x00154084 File Offset: 0x00152284
	public void LoadAllInEff(string sEffName, int nTaiZi = 0, string sAni = "xiangzi_kai")
	{
		string url = "UI/ab/" + sEffName + ".ab";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.ShowAllInEff), new ArrayList
			{
				sAni,
				nTaiZi
			});
		}
		else
		{
			this.ShowAllInEff(resWWW, new ArrayList
			{
				sAni,
				nTaiZi
			});
		}
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x00154108 File Offset: 0x00152308
	public void ShowAllInEff(WWW www, object Param)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			string sAni = string.Empty;
			bool flag2 = this.modelScn == null;
			if (!flag2)
			{
				ArrayList arrayList = (ArrayList)Param;
				sAni = arrayList[0].ToString();
				int num = (int)arrayList[1];
				bool flag3 = !this.EffObjList.ContainsKey(num);
				GameObject gameObject;
				if (flag3)
				{
					UnityEngine.Object original = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
					gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
					bool flag4 = num == 0;
					if (flag4)
					{
						gameObject.transform.SetParent(this.modelScn.transform);
					}
					else
					{
						gameObject.transform.SetParent(this.GetTaiZi(num).transform);
						gameObject.transform.localPosition = new Vector3(0f, 0f, 1.8f);
					}
					this.EffObjList.Add(num, gameObject);
				}
				else
				{
					gameObject = this.EffObjList[num];
				}
				this.OnPlayEff(gameObject, sAni);
			}
		}
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x00154238 File Offset: 0x00152438
	private void OnPlayEff(GameObject pEffObj, string sAni)
	{
		bool flag = sAni != string.Empty && !(pEffObj == null);
		if (flag)
		{
			Animation component = pEffObj.GetComponent<Animation>();
			bool flag2 = !component.IsPlaying(sAni);
			if (flag2)
			{
				component.Play(sAni);
			}
		}
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x00154288 File Offset: 0x00152488
	private void OnDeleteEff()
	{
		foreach (GameObject gameObject in this.EffObjList.Values)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
		this.EffObjList.Clear();
	}

	// Token: 0x0400210C RID: 8460
	private GameObject pOpenBtn;

	// Token: 0x0400210D RID: 8461
	private Text pItemName;

	// Token: 0x0400210E RID: 8462
	private int nSelectItemId;

	// Token: 0x0400210F RID: 8463
	private int nIndex;

	// Token: 0x04002110 RID: 8464
	private RawImage BgImg;

	// Token: 0x04002111 RID: 8465
	public RawImage LiBaoBoxItem;

	// Token: 0x04002112 RID: 8466
	private RawImage ItemModel_1;

	// Token: 0x04002113 RID: 8467
	private RawImage ItemModel_2;

	// Token: 0x04002114 RID: 8468
	private RawImage ItemModel_3;

	// Token: 0x04002115 RID: 8469
	private RawImage ItemModel_4;

	// Token: 0x04002116 RID: 8470
	private GameObject pBoxPanel;

	// Token: 0x04002117 RID: 8471
	private GameObject pFanHuiBtn;

	// Token: 0x04002118 RID: 8472
	private ItemData pItemData;

	// Token: 0x04002119 RID: 8473
	private GameObject pModelPanel;

	// Token: 0x0400211A RID: 8474
	private GameObject ZhuangBeiBtn;

	// Token: 0x0400211B RID: 8475
	private Text ZhuangBeiBtnText;

	// Token: 0x0400211C RID: 8476
	private GameObject ModleTipObj;

	// Token: 0x0400211D RID: 8477
	private Dictionary<int, ShopItemModelData> AllItemModelData = new Dictionary<int, ShopItemModelData>();

	// Token: 0x0400211E RID: 8478
	private List<Vector3> modelTiplist = new List<Vector3>
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(-230f, -203f, 0f),
		new Vector3(260f, -203f, 0f),
		new Vector3(-725f, -295f, 0f),
		new Vector3(747f, -295f, 0f),
		new Vector3(630f, -203f, 0f)
	};

	// Token: 0x0400211F RID: 8479
	private int OnSelectShowTipId;

	// Token: 0x04002120 RID: 8480
	private bool isOpenBox;

	// Token: 0x04002121 RID: 8481
	private GameObject pZuoBtn;

	// Token: 0x04002122 RID: 8482
	private GameObject pYouBtn;

	// Token: 0x04002123 RID: 8483
	private int[] szEffOpenShunXu = new int[]
	{
		3,
		4,
		1,
		2
	};

	// Token: 0x04002124 RID: 8484
	private GameObject shopCamera;

	// Token: 0x04002125 RID: 8485
	private Dictionary<int, GameObject> pModelDescList = new Dictionary<int, GameObject>();

	// Token: 0x04002126 RID: 8486
	private GameObject pBuy_Btn_All;

	// Token: 0x04002127 RID: 8487
	private Text TBuy_Btn_Txt;

	// Token: 0x04002128 RID: 8488
	private bool isOPenIng;

	// Token: 0x04002129 RID: 8489
	private bool isPiLiangOpen;

	// Token: 0x0400212A RID: 8490
	private bool isClickZhuangBeiBtn;

	// Token: 0x0400212B RID: 8491
	private GameObject modelScn;

	// Token: 0x0400212C RID: 8492
	private List<GameObject> modelObjList = new List<GameObject>();

	// Token: 0x0400212D RID: 8493
	private string sBgName = "unlock_shop_bg";

	// Token: 0x0400212E RID: 8494
	private string sCurBg = string.Empty;

	// Token: 0x0400212F RID: 8495
	private bool IsLoadIng;

	// Token: 0x04002130 RID: 8496
	private List<GameObject> AllPinZhiEffList = new List<GameObject>();

	// Token: 0x04002131 RID: 8497
	private Dictionary<int, GameObject> EffObjList = new Dictionary<int, GameObject>();
}
