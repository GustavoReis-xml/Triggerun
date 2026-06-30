using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020C RID: 524
public class KingJieSuanUICon : Window
{
	// Token: 0x060016DA RID: 5850 RVA: 0x000D44F8 File Offset: 0x000D26F8
	protected override void Awake()
	{
		base.Awake();
		KingJieSuanUICon.inst = this;
		this.pReBackBtn = this.UIGameObj.Get_GameObj(0);
		this.pFightResultImg = this.UIGameObj.Get_GameObj(1).GetComponent<Image>();
		this.tFightResultValue = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.pDropItemList = this.UIGameObj.Get_GameObj(3);
		this.pDropOneItem = this.UIGameObj.Get_GameObj(4);
		this.pFanPaiItemList = this.UIGameObj.Get_GameObj(5);
		this.pFanPaiItem = this.UIGameObj.Get_GameObj(6);
		this.pZuanShiBtn = this.UIGameObj.Get_GameObj(7);
		this.pMianFeiBtn = this.UIGameObj.Get_GameObj(8);
		this.tZuanShi = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.pFanPaiItem.SetActive(false);
		this.pDropOneItem.SetActive(false);
		this.tButtonText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.tMaxRound = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		EventTriggerListener.Get(this.pReBackBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnReBackClick);
		EventTriggerListener.Get(this.pReBackBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.firstRewardObj = this.UIGameObj.Get_GameObj(12);
		this.firstRewardIcon = this.UIGameObj.Get_GameObj(13).GetComponent<RawImage>();
		this.firstRewardTxt = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.isLoad = true;
		bool flag = this.m_szData.Count > 0;
		if (flag)
		{
			this.OnSetData(this.m_szData);
		}
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000D46C4 File Offset: 0x000D28C4
	public void OnReBackMain(bool bToServer = true)
	{
		this.Close();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnClickBack(null);
			bool flag2 = this.m_nOpenType == 2;
			if (flag2)
			{
				MainUICon.inst.OnGotoPveFbUI();
			}
		}
		this.isReBackBtnClick = false;
		if (bToServer)
		{
			PveManager.GetInstance().OnSendMsgToServer(19509, new ArrayList
			{
				0,
				0
			});
		}
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x000D4750 File Offset: 0x000D2950
	public void OnReBackClick(GameObject obj)
	{
		bool flag = this.nRandFanPai_Index != -1;
		if (flag)
		{
			this.OnReBackMain(false);
		}
		else
		{
			bool flag2 = !this.isReBackBtnClick;
			if (flag2)
			{
				this.isReBackBtnClick = true;
				PveManager.GetInstance().OnSendMsgToServer(19509, new ArrayList
				{
					0,
					0
				});
				base.StartCoroutine(this.OnWaitReBackMain());
			}
		}
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x000D47CC File Offset: 0x000D29CC
	private IEnumerator OnWaitReBackMain()
	{
		yield return new WaitForSeconds(1f);
		this.OnReBackMain(false);
		yield break;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x000D47DC File Offset: 0x000D29DC
	private void OnChouJiangClick_1(GameObject obj)
	{
		int num = int.Parse(obj.name);
		ArrayList szFanPaiData = PveManager.GetInstance().m_szFanPaiData;
		bool flag = this.isReBackBtnClick;
		if (!flag)
		{
			int num2 = (int)szFanPaiData[0];
			bool flag2 = num2 == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(26048, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = this.nRandFanPai_Index != num;
				if (flag3)
				{
					bool flag4 = num2 == 1 && !this.IsCheckMoney;
					if (flag4)
					{
						string content = string.Empty;
						content = Language.GetLang(26051, new object[]
						{
							this.m_nMoney
						});
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), content, Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), new TipManager.TipOKDelegate(this.OnChouJiang), num, new TipManager.TipCancelDelegate(this.OnCancel), null, Language.GetLang(39028, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggle), 1, string.Empty, string.Empty, Language.GetLang(19006, new object[0]));
					}
					else
					{
						this.OnChouJiang(num);
					}
				}
			}
		}
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x000D4978 File Offset: 0x000D2B78
	private void OnChouJiang(object obj)
	{
		int num = (int)obj;
		PveManager.GetInstance().OnSendMsgToServer(19507, new ArrayList
		{
			num,
			0
		});
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x000D49BC File Offset: 0x000D2BBC
	private void OnTipToggle(bool bstate)
	{
		this.IsCheckMoney = bstate;
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000D49C6 File Offset: 0x000D2BC6
	private void OnCancel(object obj)
	{
		this.IsCheckMoney = false;
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000D49D0 File Offset: 0x000D2BD0
	private void OnChouJiangClick_2(GameObject obj)
	{
		PveManager.GetInstance().OnSendMsgToServer(19507, new ArrayList
		{
			0,
			0
		});
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000D4A04 File Offset: 0x000D2C04
	public void OnSetData(ArrayList szData)
	{
		bool flag = !this.isLoad;
		if (flag)
		{
			this.m_szData = szData;
		}
		else
		{
			this.nRandFanPai_Index = -1;
			this.IsCheckMoney = false;
			ArrayList szDropItemList = PveManager.GetInstance().m_szDropItemList;
			this.m_nResult = (int)szData[0];
			this.m_nOpenType = (int)szData[1];
			this.OnSetResult(this.m_nResult);
			ArrayList szFanPaiData = PveManager.GetInstance().m_szFanPaiData;
			this.m_nMoney = (int)szFanPaiData[4];
			this.InitShowFanPai(1);
			this.OnShowDropItemList(szDropItemList);
			bool flag2 = PveManager.GetInstance().m_nCurFbType == 9;
			if (flag2)
			{
				this.tMaxRound.text = Language.GetLang(26047, new object[]
				{
					PveManager.GetInstance().m_nRound
				});
			}
			else
			{
				this.tMaxRound.text = string.Empty;
			}
			this.UpdateButtonState(-1);
			this.isReBackBtnClick = false;
			bool flag3 = PveManager.GetInstance().m_szFirstReward.Count > 0;
			if (flag3)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[(int)PveManager.GetInstance().m_szFirstReward[0]];
				this.firstRewardObj.SetActive(true);
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), this.firstRewardIcon);
				this.firstRewardTxt.text = string.Format(itemData.m_sLiBaoDesc, (int)PveManager.GetInstance().m_szFirstReward[1]);
				PveManager.GetInstance().m_szFirstReward.Clear();
			}
			else
			{
				this.firstRewardObj.SetActive(false);
			}
		}
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x000D4BD8 File Offset: 0x000D2DD8
	private void UpdateButtonState(int isNum)
	{
		ArrayList szFanPaiData = PveManager.GetInstance().m_szFanPaiData;
		int num = (int)szFanPaiData[0];
		this.pMianFeiBtn.GetComponent<Button>().interactable = true;
		int num2 = num;
		int num3 = num2;
		if (num3 != 1)
		{
			if (num3 != 2)
			{
				this.UpdateShowZunshi(false, isNum);
			}
			else
			{
				this.UpdateShowZunshi(false, isNum);
			}
		}
		else
		{
			this.UpdateShowZunshi(true, isNum);
		}
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000D4C44 File Offset: 0x000D2E44
	private void UpdateShowZunshi(bool isShowZuanshi, int isNum)
	{
		for (int i = 0; i < 5; i++)
		{
			bool flag = !this.AllFanPai.ContainsKey(i);
			if (!flag)
			{
				GameObject gameObject = this.AllFanPai[i];
				GameObject gameObject2 = gameObject.transform.FindChild("zuanshi").gameObject;
				if (isShowZuanshi)
				{
					bool flag2 = i == isNum;
					if (flag2)
					{
						gameObject2.gameObject.SetActive(false);
					}
					else
					{
						gameObject2.gameObject.SetActive(true);
					}
				}
				else
				{
					gameObject2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000D4CEC File Offset: 0x000D2EEC
	private void OnSetResult(int nReslut)
	{
		string text = string.Empty;
		bool flag = nReslut == 1;
		string imageName;
		if (flag)
		{
			imageName = "sl";
			text = Language.GetLang(26046, new object[0]);
			this.pZuanShiBtn.GetComponent<Button>().interactable = true;
			this.pMianFeiBtn.GetComponent<Button>().interactable = true;
		}
		else
		{
			imageName = "sb";
			text = Language.GetLang(26046, new object[0]);
			this.pZuanShiBtn.GetComponent<Button>().interactable = false;
			this.pMianFeiBtn.GetComponent<Button>().interactable = false;
		}
		this.pFightResultImg.sprite = Window.spriteName(AtlasType.ATLAS_KINGJIESUAN, imageName);
		this.tFightResultValue.text = text;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000D4DA8 File Offset: 0x000D2FA8
	public void InitShowFanPai(int nResult)
	{
		string imageName = string.Empty;
		this.szRand = new ArrayList
		{
			0,
			1,
			2,
			3,
			4
		};
		for (int i = 0; i < 5; i++)
		{
			bool flag = this.AllFanPai.ContainsKey(i);
			GameObject gameObject;
			if (flag)
			{
				gameObject = this.AllFanPai[i];
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pFanPaiItem);
				gameObject.transform.SetParent(this.pFanPaiItemList.transform);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.name = i.ToString();
				EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnChouJiangClick_1);
				this.AllFanPai.Add(i, gameObject);
			}
			RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
			component.gameObject.SetActive(false);
			gameObject.transform.FindChild("Text").GetComponent<Text>().text = string.Empty;
			gameObject.transform.FindChild("zuanshi/Text").GetComponent<Text>().text = this.m_nMoney.ToString();
			imageName = ((nResult != 1) ? "kapai3" : "kapai1");
			gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_KINGJIESUAN, imageName);
		}
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000D4F6C File Offset: 0x000D316C
	public void OnShowDropItemList(ArrayList szItem)
	{
		int count = szItem.Count;
		fUIManager.OnClearObj(this.AllDropItem);
		for (int i = 0; i < count; i += 2)
		{
			int key = (int)szItem[i];
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
				string icoPath = ShopManager.GetIcoPath(itemData.m_nType);
				fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, itemData.m_sIco), component);
				this.AllDropItem.Add(gameObject);
			}
		}
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x000D5100 File Offset: 0x000D3300
	public void OnFanPai(ArrayList szData, int nIndex)
	{
		int key = (int)szData[0];
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		GameObject gameObject = this.AllFanPai[nIndex];
		gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_KINGJIESUAN, "kapai2");
		RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
		component.gameObject.SetActive(true);
		string icoPath = ShopManager.GetIcoPath(itemData.m_nType);
		fUIManager.GetInstance().GetIconAsset(string.Format(icoPath, itemData.m_sIco), component);
		gameObject.transform.FindChild("Text").GetComponent<Text>().text = string.Concat(new string[]
		{
			"<color=",
			Fun.GetQualColor(itemData.m_nQuality),
			">",
			itemData.m_sName,
			"*",
			szData[1].ToString(),
			"</color>"
		});
		this.nRandFanPai_Index = nIndex;
		this.UpdateButtonState(nIndex);
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x000D5218 File Offset: 0x000D3418
	private void Start()
	{
		GameRoot.inst.Setting.Load_Setting_Data_State = false;
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x000D522C File Offset: 0x000D342C
	private void Update()
	{
	}

	// Token: 0x04001218 RID: 4632
	public static KingJieSuanUICon inst;

	// Token: 0x04001219 RID: 4633
	private GameObject pReBackBtn;

	// Token: 0x0400121A RID: 4634
	private Image pFightResultImg;

	// Token: 0x0400121B RID: 4635
	private Text tFightResultValue;

	// Token: 0x0400121C RID: 4636
	private GameObject pDropItemList;

	// Token: 0x0400121D RID: 4637
	private GameObject pDropOneItem;

	// Token: 0x0400121E RID: 4638
	private GameObject pFanPaiItemList;

	// Token: 0x0400121F RID: 4639
	private GameObject pFanPaiItem;

	// Token: 0x04001220 RID: 4640
	private GameObject pMianFeiBtn;

	// Token: 0x04001221 RID: 4641
	private GameObject pZuanShiBtn;

	// Token: 0x04001222 RID: 4642
	private GameObject firstRewardObj;

	// Token: 0x04001223 RID: 4643
	private RawImage firstRewardIcon;

	// Token: 0x04001224 RID: 4644
	private Text firstRewardTxt;

	// Token: 0x04001225 RID: 4645
	private Dictionary<int, GameObject> AllFanPai = new Dictionary<int, GameObject>();

	// Token: 0x04001226 RID: 4646
	private List<GameObject> AllDropItem = new List<GameObject>();

	// Token: 0x04001227 RID: 4647
	private bool isLoad;

	// Token: 0x04001228 RID: 4648
	private ArrayList m_szData = new ArrayList();

	// Token: 0x04001229 RID: 4649
	private int m_nResult;

	// Token: 0x0400122A RID: 4650
	private Text tButtonText;

	// Token: 0x0400122B RID: 4651
	private Text tMaxRound;

	// Token: 0x0400122C RID: 4652
	private Text tZuanShi;

	// Token: 0x0400122D RID: 4653
	private int m_nMoney;

	// Token: 0x0400122E RID: 4654
	private int m_nOpenType = 1;

	// Token: 0x0400122F RID: 4655
	private bool isReBackBtnClick;

	// Token: 0x04001230 RID: 4656
	private bool IsCheckMoney;

	// Token: 0x04001231 RID: 4657
	private int nRandFanPai_Index = -1;

	// Token: 0x04001232 RID: 4658
	private ArrayList szRand = new ArrayList
	{
		0,
		1,
		2,
		3,
		4
	};
}
