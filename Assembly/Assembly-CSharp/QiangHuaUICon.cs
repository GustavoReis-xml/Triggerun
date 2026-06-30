using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B8 RID: 696
public class QiangHuaUICon : Window
{
	// Token: 0x06001FFB RID: 8187 RVA: 0x0012652C File Offset: 0x0012472C
	protected override void Awake()
	{
		base.Awake();
		QiangHuaUICon.inst = this;
		this.pToggleBtn_1 = this.UIGameObj.Get_GameObj(0);
		this.pToggleBtn_2 = this.UIGameObj.Get_GameObj(1);
		this.pToggleBtn_3 = this.UIGameObj.Get_GameObj(2);
		this.geneBtnObj = this.UIGameObj.Get_GameObj(15);
		this.geneBtnObj.SetActive(Fun.IsFunctionOpen("GeneTabBtn", null));
		this.geneUIObj = this.UIGameObj.Get_GameObj(14);
		this.pQh_panel = this.UIGameObj.Get_GameObj(3);
		this.pQh_PanleList = this.UIGameObj.Get_GameObj(4);
		this.szToggBtnList.Add(1, this.pToggleBtn_1);
		this.szToggBtnList.Add(2, this.pToggleBtn_2);
		this.szToggBtnList.Add(3, this.pToggleBtn_3);
		this.szToggBtnList.Add(4, this.geneBtnObj);
		this.szToggBtnTanHao.Add(1, this.pToggleBtn_1.transform.FindChild("Image").gameObject);
		this.szToggBtnTanHao.Add(2, this.pToggleBtn_2.transform.FindChild("Image").gameObject);
		this.szToggBtnTanHao.Add(3, this.pToggleBtn_3.transform.FindChild("Image").gameObject);
		this.szToggBtnTanHao.Add(4, this.geneBtnObj.transform.FindChild("Image").gameObject);
		this.AllPanel.Add(1, this.pQh_panel);
		this.AllPanel.Add(4, this.geneUIObj);
		foreach (int num in this.szToggBtnList.Keys)
		{
			bool flag = this.AllPanel.ContainsKey(num);
			GameObject gameObject;
			if (flag)
			{
				gameObject = this.AllPanel[num];
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pQh_panel);
				gameObject.transform.SetParent(this.pQh_PanleList.transform);
				gameObject.SetActive(false);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.AllPanel.Add(num, gameObject);
			}
			bool flag2 = num == 4;
			if (flag2)
			{
				gameObject.AddComponent<GeneUICon>();
			}
			else
			{
				QiangHuaAttCon qiangHuaAttCon = gameObject.GetComponent<QiangHuaAttCon>();
				bool flag3 = qiangHuaAttCon == null;
				if (flag3)
				{
					qiangHuaAttCon = gameObject.AddComponent<QiangHuaAttCon>();
				}
				qiangHuaAttCon.InitData(num);
			}
			EventTriggerListener.Get(this.szToggBtnList[num].gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickToggle);
			EventTriggerListener.Get(this.szToggBtnList[num]).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		}
		this.OnClickToggleFun(this.pToggleBtn_1, false);
		this.pItemTip = this.UIGameObj.Get_GameObj(5);
		this.pItemAttTip = this.UIGameObj.Get_GameObj(6);
		this.tAttTipName = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.tAttTipDesc = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.tAttAdd_1 = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.tAttAdd_2 = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		this.tAtt_XiaoHao = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.rAtt_ItemRaw = this.UIGameObj.Get_GameObj(12).GetComponent<RawImage>();
		this.effectTxt1 = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.effectTxt2 = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.effectTxt3 = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		this.OnShowTanHao();
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x0012696C File Offset: 0x00124B6C
	public void UpdateZdl()
	{
		bool flag = !(this.effectTxt1 == null);
		if (flag)
		{
			this.effectTxt1.text = (PveManager.GetInstance().GetZhanDuLi(1) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
			this.effectTxt2.text = (PveManager.GetInstance().GetZhanDuLi(2) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
			this.effectTxt3.text = (PveManager.GetInstance().GetZhanDuLi(3) + PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)).ToString();
		}
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x00126A25 File Offset: 0x00124C25
	public void OnUpdateHeroTypeTanHao(int nHeroType, bool isShow)
	{
		this.szToggBtnTanHao[nHeroType].SetActive(isShow);
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x00126A3C File Offset: 0x00124C3C
	public void OnShowTanHao()
	{
		foreach (int num in this.szToggBtnTanHao.Keys)
		{
			bool flag = num == 4;
			if (flag)
			{
				this.szToggBtnTanHao[num].SetActive(PveManager.GetInstance().StrengthenData.ShowGeneTanhao());
			}
			else
			{
				bool flag2 = PveManager.GetInstance().m_szTanHaoState.IndexOf(num) != -1;
				if (flag2)
				{
					this.szToggBtnTanHao[num].SetActive(true);
				}
				else
				{
					this.szToggBtnTanHao[num].SetActive(false);
				}
			}
		}
		PveManager.GetInstance().m_szTanHaoState.Clear();
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x00126B20 File Offset: 0x00124D20
	private void Start()
	{
		this.UpdateZdl();
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x00126B2C File Offset: 0x00124D2C
	public override void Reset()
	{
		base.Reset();
		this.UpdateZdl();
		bool flag = this.m_nPanelType == 4;
		if (flag)
		{
			GeneUICon.inst.Reset();
		}
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x00126B64 File Offset: 0x00124D64
	private void OnClickToggleFun(GameObject obj, bool isClick)
	{
		int num = int.Parse(obj.name.Replace("Toggle", string.Empty));
		bool flag = this.m_nPanelType == num;
		if (!flag)
		{
			bool flag2 = this.OnSelectPanel != null;
			if (flag2)
			{
				this.OnSelectPanel.SetActive(false);
				if (isClick)
				{
					bool flag3 = this.m_nPanelType == 4;
					if (flag3)
					{
						GeneUICon.inst.Reset();
					}
					else
					{
						QiangHuaAttCon heroTypeCon = this.GetHeroTypeCon(this.m_nPanelType);
						bool flag4 = heroTypeCon != null;
						if (flag4)
						{
							heroTypeCon.OnHiveLpEff();
						}
					}
				}
			}
			this.m_nPanelType = num;
			this.AllPanel[this.m_nPanelType].gameObject.SetActive(true);
			this.OnSelectPanel = this.AllPanel[this.m_nPanelType].gameObject;
			bool flag5 = this.OnSelectToggleBtn != null;
			if (flag5)
			{
				this.OnSelectToggleBtn.GetComponent<Toggle>().enabled = true;
				this.OnSelectToggleBtn.GetComponent<Toggle>().interactable = true;
			}
			if (isClick)
			{
				obj.GetComponent<Toggle>().enabled = false;
			}
			obj.GetComponent<Toggle>().interactable = false;
			this.OnSelectToggleBtn = obj;
		}
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x00126CAE File Offset: 0x00124EAE
	private void OnClickToggle(GameObject obj)
	{
		this.OnClickToggleFun(obj, true);
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x00126CBC File Offset: 0x00124EBC
	public void OnUpdateValue(HeroOneAttData UserOneAttData)
	{
		QiangHuaAttCon heroTypeCon = this.GetHeroTypeCon(UserOneAttData.m_nHeroType);
		bool flag = !(heroTypeCon == null);
		if (flag)
		{
			heroTypeCon.OnUpdateOneValue(UserOneAttData);
			PveManager.GetInstance().OnCheckCanLevelUp();
			this.UpdateZdl();
		}
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x00126D04 File Offset: 0x00124F04
	public QiangHuaAttCon GetHeroTypeCon(int nHeroType)
	{
		bool flag = !this.AllPanel.ContainsKey(nHeroType);
		QiangHuaAttCon result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.AllPanel[nHeroType].GetComponent<QiangHuaAttCon>();
		}
		return result;
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x00126D40 File Offset: 0x00124F40
	public void UpdateDaoJuNum(int nItemId)
	{
		foreach (GameObject gameObject in this.AllPanel.Values)
		{
			QiangHuaAttCon component = gameObject.GetComponent<QiangHuaAttCon>();
			bool flag = component != null;
			if (flag)
			{
				component.OnUpdateItemNum(nItemId, "QhUI_UpdateDaoJuNum");
			}
		}
		bool flag2 = QingHuaJinJieUICon.inst != null;
		if (flag2)
		{
			QingHuaJinJieUICon.inst.UpdateItemNum(nItemId);
		}
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x00126DD8 File Offset: 0x00124FD8
	public void UpdateQhResult(int nHeroType, int nAttType, int nRet)
	{
		QiangHuaAttCon heroTypeCon = this.GetHeroTypeCon(nHeroType);
		bool flag = !(heroTypeCon == null) && nRet < 1;
		if (flag)
		{
			heroTypeCon.UpdateQhResult(nAttType, nRet, 1);
		}
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x00126E10 File Offset: 0x00125010
	public override void Close()
	{
		base.Close();
		bool flag = this.m_nPanelType == 4;
		if (flag)
		{
			GeneUICon.inst.Reset();
		}
		else
		{
			QiangHuaAttCon heroTypeCon = this.GetHeroTypeCon(this.m_nPanelType);
			bool flag2 = heroTypeCon != null;
			if (flag2)
			{
				heroTypeCon.OnHiveLpEff();
			}
		}
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x00126E64 File Offset: 0x00125064
	public void OnShowItemTip(ItemData pitemdata)
	{
		Text component = this.pItemTip.transform.FindChild("ItemName").GetComponent<Text>();
		Text component2 = this.pItemTip.transform.FindChild("Itemdesc").GetComponent<Text>();
		component.text = pitemdata.m_sName;
		component2.text = pitemdata.m_sTipDesc;
		this.pItemTip.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 160f);
		this.pItemTip.SetActive(true);
		PveManager.GetInstance().mousepos(this.pItemTip, 20f, -20f);
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x00126F0A File Offset: 0x0012510A
	public void OnHideItemTip()
	{
		this.pItemTip.SetActive(false);
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x00126F1C File Offset: 0x0012511C
	public float[] OnShowAttTip(int nHeroType, int nAttType)
	{
		string format = string.Empty;
		string text = string.Empty;
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = fGameManager.mloadConfig.AllQiangHuaData[nHeroType];
		QiangHuaAttributeData qiangHuaAttributeData = qiangHuaHeroTypeXmlData.AllAttributeData[nAttType];
		HeroOneAttData attDataByType = PveManager.GetInstance().StrengthenData.GetAttDataByType(nHeroType, nAttType);
		this.tAttTipName.text = qiangHuaAttributeData.sName;
		this.tAttTipDesc.text = qiangHuaAttributeData.m_sDesc;
		float attValue = PveManager.GetInstance().GetAttValue(attDataByType.m_nQhLv, attDataByType.m_nJieLv, qiangHuaAttributeData);
		float zdlValue = PveManager.GetInstance().GetZdlValue(attDataByType.m_nQhLv, attDataByType.m_nJieLv);
		this.tAttAdd_1.text = Language.GetLang(27017, new object[]
		{
			PveManager.GetInstance().OnShowValue(attDataByType.m_nType, attValue)
		});
		bool flag = attDataByType.m_nJieLv >= qiangHuaHeroTypeXmlData.nMaxJie;
		if (flag)
		{
			this.tAttAdd_2.text = Language.GetLang(27018, new object[0]);
			this.tAtt_XiaoHao.text = Language.GetLang(27019, new object[0]);
			this.rAtt_ItemRaw.gameObject.SetActive(false);
		}
		else
		{
			this.rAtt_ItemRaw.gameObject.SetActive(true);
			int num4 = attDataByType.m_nQhLv;
			int num5 = attDataByType.m_nJieLv;
			bool flag2 = num4 >= qiangHuaAttributeData.nMaxLv;
			if (flag2)
			{
				bool flag3 = num5 >= qiangHuaHeroTypeXmlData.nMaxJie;
				if (flag3)
				{
					num4 = qiangHuaAttributeData.nMaxLv;
					num5 = qiangHuaHeroTypeXmlData.nMaxJie;
				}
				else
				{
					num5++;
					num4 = 0;
					num3 = 1;
				}
			}
			else
			{
				num4++;
			}
			float attValue2 = PveManager.GetInstance().GetAttValue(num4, num5, qiangHuaAttributeData);
			float zdlValue2 = PveManager.GetInstance().GetZdlValue(num4, num5);
			this.tAttAdd_2.text = Language.GetLang(27020, new object[]
			{
				PveManager.GetInstance().OnShowValue(attDataByType.m_nType, attValue2)
			});
			ArrayList qiangHuaXiaoHaoItem = PveManager.GetInstance().GetQiangHuaXiaoHaoItem(qiangHuaHeroTypeXmlData, attDataByType, qiangHuaAttributeData);
			int num6 = (int)qiangHuaXiaoHaoItem[0];
			int num7 = (int)qiangHuaXiaoHaoItem[1];
			int itemNumById = ShopManager.GetInstance().GetItemNumById(num6);
			text = ((itemNumById >= num7) ? "00FF00" : "FF0000");
			int nMsgID = (num3 != 0) ? 27022 : 27021;
			this.tAtt_XiaoHao.text = string.Concat(new string[]
			{
				"<color=#00FF00>",
				Language.GetLang(nMsgID, new object[0]),
				"</color><color=#",
				text,
				">",
				itemNumById.ToString(),
				"/",
				num7.ToString(),
				"</color>"
			});
			ItemData itemData = fGameManager.mloadConfig.AllItemData[num6];
			format = ShopManager.GetIcoPath(itemData.m_nType);
			fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sIco), this.rAtt_ItemRaw);
			num = attValue2 - attValue;
			num2 = zdlValue2 - zdlValue;
		}
		this.pItemAttTip.SetActive(true);
		PveManager.GetInstance().mousepos(this.pItemAttTip, 20f, -20f);
		return new float[]
		{
			num,
			num2
		};
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x00127294 File Offset: 0x00125494
	public void OnShowAttTip(string title, string content, string note)
	{
		this.tAttTipName.text = title;
		this.tAttTipDesc.text = content;
		this.tAtt_XiaoHao.text = note;
		this.tAttAdd_1.text = string.Empty;
		this.tAttAdd_2.text = string.Empty;
		this.rAtt_ItemRaw.gameObject.SetActive(false);
		this.pItemAttTip.SetActive(true);
		PveManager.GetInstance().mousepos(this.pItemAttTip, 20f, -20f);
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x00127325 File Offset: 0x00125525
	public void OnHideAttTip()
	{
		this.pItemAttTip.SetActive(false);
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x00127335 File Offset: 0x00125535
	private void Update()
	{
	}

	// Token: 0x04001D36 RID: 7478
	public static QiangHuaUICon inst;

	// Token: 0x04001D37 RID: 7479
	private GameObject pQh_PanleList;

	// Token: 0x04001D38 RID: 7480
	private GameObject pQh_panel;

	// Token: 0x04001D39 RID: 7481
	private GameObject pToggleBtn_1;

	// Token: 0x04001D3A RID: 7482
	private GameObject pToggleBtn_2;

	// Token: 0x04001D3B RID: 7483
	private GameObject pToggleBtn_3;

	// Token: 0x04001D3C RID: 7484
	private Dictionary<int, GameObject> szToggBtnList = new Dictionary<int, GameObject>();

	// Token: 0x04001D3D RID: 7485
	private Dictionary<int, GameObject> szToggBtnTanHao = new Dictionary<int, GameObject>();

	// Token: 0x04001D3E RID: 7486
	private GameObject OnSelectToggleBtn;

	// Token: 0x04001D3F RID: 7487
	private GameObject OnSelectPanel;

	// Token: 0x04001D40 RID: 7488
	private int m_nPanelType;

	// Token: 0x04001D41 RID: 7489
	private Dictionary<int, GameObject> AllPanel = new Dictionary<int, GameObject>();

	// Token: 0x04001D42 RID: 7490
	private GameObject pItemTip;

	// Token: 0x04001D43 RID: 7491
	private GameObject pItemAttTip;

	// Token: 0x04001D44 RID: 7492
	private Text tAttTipName;

	// Token: 0x04001D45 RID: 7493
	private Text tAttTipDesc;

	// Token: 0x04001D46 RID: 7494
	private Text tAttAdd_1;

	// Token: 0x04001D47 RID: 7495
	private Text tAttAdd_2;

	// Token: 0x04001D48 RID: 7496
	private Text tAtt_XiaoHao;

	// Token: 0x04001D49 RID: 7497
	private RawImage rAtt_ItemRaw;

	// Token: 0x04001D4A RID: 7498
	private GameObject geneBtnObj;

	// Token: 0x04001D4B RID: 7499
	private GameObject geneUIObj;

	// Token: 0x04001D4C RID: 7500
	private Text effectTxt1;

	// Token: 0x04001D4D RID: 7501
	private Text effectTxt2;

	// Token: 0x04001D4E RID: 7502
	private Text effectTxt3;
}
