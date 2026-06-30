using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A4 RID: 1188
public class WenJuanUICon : Window
{
	// Token: 0x06002DD9 RID: 11737 RVA: 0x00194F7C File Offset: 0x0019317C
	protected override void Awake()
	{
		base.Awake();
		WenJuanUICon.inst = this;
		this.UI_TitleText = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.UI_TitleText.text = Language.GetLang(41000, new object[0]);
		this.WenTiUI = this.UIGameObj.Get_GameObj(0);
		this.RewardTiShiUI = this.UIGameObj.Get_GameObj(1);
		this.oTiMuWin = this.UIGameObj.Get_GameObj(5);
		this.AnswerList = this.UIGameObj.Get_GameObj(6);
		this.oDanXuan = this.UIGameObj.Get_GameObj(3);
		this.oDuoXuan = this.UIGameObj.Get_GameObj(4);
		this.oOther = this.UIGameObj.Get_GameObj(7);
		this.oOther_1 = this.UIGameObj.Get_GameObj(14);
		this.oList = this.UIGameObj.Get_GameObj(12).transform.FindChild("RewardList");
		this.OneItme = this.UIGameObj.Get_GameObj(10);
		this.MsgImage = this.UIGameObj.Get_GameObj(11);
		this.DaTiStart = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(this.DaTiStart).onClick = new EventTriggerListener.VoidDelegate(this.DaTiStartClick);
		this.CloseDaTi = this.UIGameObj.Get_GameObj(15);
		EventTriggerListener.Get(this.CloseDaTi).onClick = new EventTriggerListener.VoidDelegate(this.Close);
		this.UI_RewardTitleText = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.UI_RewardTitleText.text = Language.GetLang(41008, new object[0]);
		this.oDanXuan.SetActive(false);
		this.oDuoXuan.SetActive(false);
		this.oOther.SetActive(false);
		this.oOther_1.SetActive(false);
		this.Scrollbar_Vertical = this.UIGameObj.Get_GameObj(17).GetComponent<ScrollRect>();
	}

	// Token: 0x06002DDA RID: 11738 RVA: 0x00195181 File Offset: 0x00193381
	public void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
		this.InputClick(obj);
	}

	// Token: 0x06002DDB RID: 11739 RVA: 0x00195192 File Offset: 0x00193392
	public void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
		this.DaTiState();
	}

	// Token: 0x06002DDC RID: 11740 RVA: 0x001951A2 File Offset: 0x001933A2
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x001951AC File Offset: 0x001933AC
	public override void Reset()
	{
		base.Reset();
		this.InitData();
		fGameManager.SendMsgToHallServer(1314, new ArrayList
		{
			14006,
			new ArrayList()
		});
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x001951EC File Offset: 0x001933EC
	private void InitData()
	{
		ArrayList szNowOpenWenJuan = WenJuanManager.GetInstance().m_szNowOpenWenJuan;
		bool flag = szNowOpenWenJuan.Count > 0;
		if (flag)
		{
			this.iNowWenJuanId = int.Parse(szNowOpenWenJuan[0].ToString());
			this.iWinType = int.Parse(szNowOpenWenJuan[1].ToString());
		}
		this.iNowTiMuIndex = 0;
		this.iButType = 0;
		this.aRestuleData = new ArrayList();
		this.ShowWenJuanData();
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x00195264 File Offset: 0x00193464
	private void ShowWenJuanData()
	{
		bool flag = !fGameManager.mloadConfig.WenJuanXmlData.ContainsKey(this.iNowWenJuanId);
		if (!flag)
		{
			this.mWenJuanXmlData = fGameManager.mloadConfig.WenJuanXmlData[this.iNowWenJuanId];
			foreach (object obj in this.oList)
			{
				Transform transform = (Transform)obj;
				bool flag2 = transform;
				if (flag2)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			string wReward = this.mWenJuanXmlData.wReward;
			bool flag3 = wReward != string.Empty;
			if (flag3)
			{
				string[] array = wReward.Split(new char[]
				{
					'#'
				});
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					bool flag4 = array[i] == string.Empty;
					if (!flag4)
					{
						string[] array2 = array[i].ToString().Split(new char[]
						{
							','
						});
						bool flag5 = array2.Length < 1;
						if (!flag5)
						{
							int num2 = int.Parse(array2[0].ToString());
							ItemData itemData = fGameManager.mloadConfig.AllItemData[num2];
							bool flag6 = itemData != null;
							if (flag6)
							{
								GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneItme);
								gameObject.SetActive(true);
								gameObject.transform.SetParent(this.oList);
								gameObject.transform.localPosition = Vector3.zero;
								gameObject.transform.localScale = Vector3.one;
								BagUserItem bagUserItem = gameObject.GetComponent<BagUserItem>();
								bool flag7 = bagUserItem == null;
								if (flag7)
								{
									bagUserItem = gameObject.AddComponent<BagUserItem>();
								}
								bagUserItem.showItemData(new ArrayList
								{
									num2,
									int.Parse(array2[1].ToString())
								});
							}
						}
					}
				}
			}
			this.MsgImage.SetActive(true);
			this.DaTiStart.SetActive(true);
			this.CloseDaTi.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41007, new object[0]);
			bool flag8 = this.iWinType == 1;
			if (flag8)
			{
				this.MsgImage.transform.Find("Text").gameObject.GetComponent<Text>().text = this.mWenJuanXmlData.wText;
				this.DaTiStart.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41001, new object[0]);
				this.iTiMuIndex = 1;
				bool flag9 = !this.CloseDaTi.activeSelf;
				if (flag9)
				{
					this.CloseDaTi.SetActive(true);
				}
			}
			else
			{
				this.iButType = 1;
				this.MsgImage.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41002, new object[0]);
				this.DaTiStart.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41006, new object[0]);
				bool activeSelf = this.CloseDaTi.activeSelf;
				if (activeSelf)
				{
					this.CloseDaTi.SetActive(false);
				}
			}
			this.DaTiStart.GetComponent<Button>().interactable = true;
			this.RewardTiShiUI.SetActive(true);
			this.WenTiUI.SetActive(false);
		}
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x00195648 File Offset: 0x00193848
	private void ShowOnWenTiData()
	{
		this.RewardTiShiUI.SetActive(false);
		this.WenTiUI.SetActive(true);
		ArrayList arrayList = new ArrayList();
		bool flag = this.mWenJuanXmlData == null;
		if (!flag)
		{
			bool flag2 = this.iNowTiMuIndex != 0;
			if (flag2)
			{
				foreach (object obj in this.AnswerList.transform)
				{
					Transform transform = (Transform)obj;
					bool flag3 = !transform || !transform.gameObject.GetComponent<Toggle>().isOn;
					if (!flag3)
					{
						string text = string.Empty;
						string[] array = transform.gameObject.name.Split(new char[]
						{
							'_'
						});
						bool flag4 = array.Length != 0;
						if (flag4)
						{
							bool flag5 = int.Parse(array[3].ToString()) == 0;
							if (flag5)
							{
								arrayList.Add(new ArrayList
								{
									transform.gameObject.name
								});
							}
							else
							{
								text = transform.gameObject.transform.Find("InputField").gameObject.GetComponent<InputField>().text;
								text = Fun.CheckChatSpecialStr(text);
								text = Fun.GetShieldTxt(text);
								arrayList.Add(new ArrayList
								{
									transform.gameObject.name,
									text.Replace("\r\n", string.Empty).Replace("\n", string.Empty)
								});
							}
						}
					}
				}
				int count = arrayList.Count;
				bool flag6 = count < 1;
				if (flag6)
				{
					return;
				}
				for (int i = 0; i < count; i++)
				{
					ArrayList arrayList2 = arrayList[i] as ArrayList;
					bool flag7 = arrayList2.Count >= 1;
					if (flag7)
					{
						this.aRestuleData.Add(arrayList2);
					}
				}
			}
			this.iNowTiMuIndex = this.iTiMuIndex;
			bool flag8 = this.mWenJuanXmlData.wWenJuanEventData.ContainsKey(this.iTiMuIndex);
			if (flag8)
			{
				WenJuanXmlEventData wenJuanXmlEventData = this.mWenJuanXmlData.wWenJuanEventData[this.iTiMuIndex];
				bool flag9 = wenJuanXmlEventData == null;
				if (flag9)
				{
					return;
				}
				int eType = wenJuanXmlEventData.eType;
				string str = "<color=#ff0000> (" + Language.GetLang(41003, new object[0]) + ")</color>";
				bool flag10 = eType == 1;
				if (flag10)
				{
					str = "<color=#ff0000> (" + Language.GetLang(41004, new object[0]) + ")</color>";
				}
				this.oTiMuWin.GetComponent<Text>().text = wenJuanXmlEventData.eText + str;
				foreach (object obj2 in this.AnswerList.transform)
				{
					Transform transform2 = (Transform)obj2;
					bool flag11 = transform2;
					if (flag11)
					{
						UnityEngine.Object.Destroy(transform2.gameObject);
					}
				}
				Dictionary<int, WenJuanXmlKeyData> eWenJuanKeyData = wenJuanXmlEventData.eWenJuanKeyData;
				int count2 = eWenJuanKeyData.Count;
				for (int j = 1; j <= count2; j++)
				{
					bool flag12 = !eWenJuanKeyData.ContainsKey(j);
					if (!flag12)
					{
						WenJuanXmlKeyData wenJuanXmlKeyData = eWenJuanKeyData[j];
						bool flag13 = wenJuanXmlKeyData != null;
						if (flag13)
						{
							int kType = wenJuanXmlKeyData.kType;
							bool flag14 = kType != 1;
							GameObject gameObject;
							if (flag14)
							{
								gameObject = ((eType != 0) ? UnityEngine.Object.Instantiate<GameObject>(this.oDuoXuan) : UnityEngine.Object.Instantiate<GameObject>(this.oDanXuan));
							}
							else
							{
								gameObject = ((eType != 0) ? UnityEngine.Object.Instantiate<GameObject>(this.oOther_1) : UnityEngine.Object.Instantiate<GameObject>(this.oOther));
								gameObject.transform.FindChild("InputField/Placeholder").GetComponent<Text>().text = Language.GetLang(41009, new object[0]);
								EventTriggerListener.Get(gameObject.transform.FindChild("InputField").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
								EventTriggerListener.Get(gameObject.transform.FindChild("InputField").gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
							}
							gameObject.SetActive(true);
							gameObject.transform.SetParent(this.AnswerList.transform);
							gameObject.transform.localPosition = Vector3.zero;
							gameObject.transform.localScale = Vector3.one;
							gameObject.gameObject.GetComponent<Toggle>().isOn = false;
							gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool p0)
							{
								this.DaTiState();
							});
							gameObject.name = string.Concat(new string[]
							{
								this.iNowWenJuanId.ToString(),
								"_",
								this.iTiMuIndex.ToString(),
								"_",
								j.ToString(),
								"_",
								kType.ToString()
							});
							gameObject.transform.Find("Label").gameObject.GetComponent<Text>().text = wenJuanXmlKeyData.kText;
							gameObject.transform.Find("Label").gameObject.GetComponent<Text>().color = Color.white;
						}
					}
				}
				this.DaTiStart.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41005, new object[]
				{
					this.iTiMuIndex,
					this.mWenJuanXmlData.wWenJuanEventData.Count
				});
				this.DaTiStart.GetComponent<Button>().interactable = false;
			}
			else
			{
				this.DaTiStart.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41005, new object[]
				{
					this.iTiMuIndex,
					this.mWenJuanXmlData.wWenJuanEventData.Count
				});
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(this.iNowWenJuanId);
				arrayList3.Add(this.aRestuleData);
				fGameManager.SendMsgToHallServer(1314, new ArrayList
				{
					14001,
					arrayList3
				});
			}
			this.iTiMuIndex++;
			this.Scrollbar_Vertical.verticalNormalizedPosition = 1f;
		}
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x00195D70 File Offset: 0x00193F70
	private void DaTiState()
	{
		foreach (object obj in this.AnswerList.transform)
		{
			Transform transform = (Transform)obj;
			bool flag = transform && transform.gameObject.GetComponent<Toggle>().isOn;
			if (flag)
			{
				string text = string.Empty;
				string[] array = transform.gameObject.name.Split(new char[]
				{
					'_'
				});
				bool flag2 = array.Length != 0 && int.Parse(array[3].ToString()) != 0;
				if (flag2)
				{
					text = transform.gameObject.transform.Find("InputField").gameObject.GetComponent<InputField>().text;
					text = Fun.CheckChatSpecialStr(text);
					text = Fun.GetShieldTxt(text);
				}
				this.DaTiStart.GetComponent<Button>().interactable = true;
				return;
			}
		}
		this.DaTiStart.GetComponent<Button>().interactable = false;
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x00195E98 File Offset: 0x00194098
	private void DaTiStartClick(GameObject obj)
	{
		bool flag = this.iButType == 1;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.iNowWenJuanId);
			fGameManager.SendMsgToHallServer(1314, new ArrayList
			{
				14003,
				arrayList
			});
		}
		else
		{
			this.ShowOnWenTiData();
		}
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x00195F04 File Offset: 0x00194104
	private void Close(GameObject go)
	{
		this.Close();
		bool flag = MainUICon.inst;
		if (flag)
		{
			MainUICon.inst.OnClickBack(null);
		}
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x00195F35 File Offset: 0x00194135
	private void InputClick(GameObject go)
	{
		go.transform.parent.GetComponent<Toggle>().isOn = true;
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x00195F50 File Offset: 0x00194150
	public void SentSeverMsg(ArrayList szMsg)
	{
		bool flag = szMsg.Count >= 1;
		if (flag)
		{
			int num = int.Parse(szMsg[0].ToString());
			int num2 = int.Parse(szMsg[1].ToString());
			bool flag2 = num2 == -1 || num2 == -2;
			if (flag2)
			{
				this.Close(null);
			}
			else
			{
				bool flag3 = num == this.iNowWenJuanId;
				if (flag3)
				{
					this.MsgImage.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41002, new object[0]);
					this.iButType = 1;
					this.CloseDaTi.SetActive(false);
					this.RewardTiShiUI.SetActive(true);
					this.WenTiUI.SetActive(false);
					this.DaTiStart.SetActive(true);
					this.DaTiStart.transform.Find("Text").gameObject.GetComponent<Text>().text = Language.GetLang(41006, new object[0]);
				}
			}
		}
	}

	// Token: 0x0400272B RID: 10027
	public static WenJuanUICon inst;

	// Token: 0x0400272C RID: 10028
	private Text UI_TitleText;

	// Token: 0x0400272D RID: 10029
	private Text UI_RewardTitleText;

	// Token: 0x0400272E RID: 10030
	private GameObject WenTiUI;

	// Token: 0x0400272F RID: 10031
	private GameObject RewardTiShiUI;

	// Token: 0x04002730 RID: 10032
	private GameObject OneItme;

	// Token: 0x04002731 RID: 10033
	private GameObject MsgImage;

	// Token: 0x04002732 RID: 10034
	private GameObject DaTiStart;

	// Token: 0x04002733 RID: 10035
	private GameObject CloseDaTi;

	// Token: 0x04002734 RID: 10036
	private GameObject DanToggle;

	// Token: 0x04002735 RID: 10037
	private GameObject AnswerList;

	// Token: 0x04002736 RID: 10038
	private GameObject oTiMuWin;

	// Token: 0x04002737 RID: 10039
	private GameObject oDanXuan;

	// Token: 0x04002738 RID: 10040
	private GameObject oDuoXuan;

	// Token: 0x04002739 RID: 10041
	private GameObject oOther;

	// Token: 0x0400273A RID: 10042
	private GameObject oOther_1;

	// Token: 0x0400273B RID: 10043
	private ScrollRect Scrollbar_Vertical;

	// Token: 0x0400273C RID: 10044
	private int iNowWenJuanId;

	// Token: 0x0400273D RID: 10045
	private int iTiMuIndex;

	// Token: 0x0400273E RID: 10046
	private int iNowTiMuIndex;

	// Token: 0x0400273F RID: 10047
	private int iWinType;

	// Token: 0x04002740 RID: 10048
	private int iButType;

	// Token: 0x04002741 RID: 10049
	private ArrayList aRestuleData;

	// Token: 0x04002742 RID: 10050
	private WenJuanXmlData mWenJuanXmlData;

	// Token: 0x04002743 RID: 10051
	private Transform oList;
}
