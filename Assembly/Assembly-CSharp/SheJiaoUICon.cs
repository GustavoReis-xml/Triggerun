using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000307 RID: 775
public class SheJiaoUICon : Window
{
	// Token: 0x0600254C RID: 9548 RVA: 0x0015091C File Offset: 0x0014EB1C
	protected override void Awake()
	{
		base.Awake();
		SheJiaoUICon.Inst = this;
		Text component = base.transform.Find("Title").GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			component.font = fUIManager.mNewComic;
		}
		this.oToggle1 = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(this.oToggle1).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		this.oToggle2 = this.UIGameObj.Get_GameObj(1);
		EventTriggerListener.Get(this.oToggle2).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		this.oToggle3 = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(this.oToggle3).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeEvent);
		this.tNowOnLineNum = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.oAddFriend = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(this.oAddFriend).onClick = new EventTriggerListener.VoidDelegate(this.OnAddFrendEvent);
		this.oList1 = this.UIGameObj.Get_GameObj(5);
		this.oList2 = this.UIGameObj.Get_GameObj(6);
		this.oList3 = this.UIGameObj.Get_GameObj(7);
		this.CloneoneTable = this.UIGameObj.Get_GameObj(8);
		this.CloneoneTable.SetActive(false);
		this.CloneApple = this.UIGameObj.Get_GameObj(9);
		this.CloneApple.SetActive(false);
		this.InitFriendListObj(this.oList1, this.CloneoneTable, 24);
		this.InitFriendListObj(this.oList2, this.CloneoneTable, 24);
		this.InitFriendListObj(this.oList3, this.CloneApple, 9);
		this.oleftbut = this.UIGameObj.Get_GameObj(10);
		EventTriggerListener.Get(this.oleftbut).onClick = new EventTriggerListener.VoidDelegate(this.OnLeftTable);
		this.pagenum = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.oRightbut = this.UIGameObj.Get_GameObj(12);
		EventTriggerListener.Get(this.oRightbut).onClick = new EventTriggerListener.VoidDelegate(this.OnRightTable);
		this.BackBtn = this.UIGameObj.Get_GameObj(13);
		EventTriggerListener.Get(this.BackBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseUI);
		this.AddFriendPage = this.UIGameObj.Get_GameObj(14);
		this.rewardTipBg = this.AddFriendPage.GetComponent<RawImage>();
		this.oInputField = this.AddFriendPage.transform.Find("ContentPanel/InputField").gameObject;
		this.input = this.AddFriendPage.transform.Find("ContentPanel/InputField").GetComponent<InputField>();
		EventTriggerListener.Get(this.oInputField.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.oInputField.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		this.tiptext = this.AddFriendPage.transform.Find("ContentPanel/TipText").GetComponent<Text>();
		this.oCloseAddFriend = this.AddFriendPage.transform.Find("ContentPanel/CreateBtn").gameObject;
		EventTriggerListener.Get(this.oCloseAddFriend).onClick = new EventTriggerListener.VoidDelegate(this.CloseAddFriendUI);
		this.oAddBtnFriend = this.AddFriendPage.transform.Find("ContentPanel/AddBtn").gameObject;
		EventTriggerListener.Get(this.oAddBtnFriend).onClick = new EventTriggerListener.VoidDelegate(this.OnAddFriendUI);
		this.oTable1 = this.UIGameObj.Get_GameObj(15);
		this.oTable1.SetActive(false);
		this.oTable2 = this.UIGameObj.Get_GameObj(16);
		this.oTable2.SetActive(false);
		this.oTable3 = this.UIGameObj.Get_GameObj(17);
		this.oTable3.SetActive(false);
		this.pCheckBtn = this.UIGameObj.Get_GameObj(18).GetComponent<Toggle>();
		EventTriggerListener.Get(this.pCheckBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnCheckToggle);
		this.AddFriendPage.SetActive(false);
		this.GetButStat();
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x00150D84 File Offset: 0x0014EF84
	private void OnCheckToggle(GameObject obj)
	{
		string value = "0";
		bool isOn = this.pCheckBtn.isOn;
		if (isOn)
		{
			value = "1";
		}
		PlayerPrefs.SetString("Display_Offline_Friend", value);
		this.OnChangeTab();
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x00150DC4 File Offset: 0x0014EFC4
	private int GetCheckToggle()
	{
		string @string = PlayerPrefs.GetString("Display_Offline_Friend");
		bool flag = @string == string.Empty;
		if (flag)
		{
			PlayerPrefs.SetString("Display_Offline_Friend", "1");
			@string = PlayerPrefs.GetString("Display_Offline_Friend");
		}
		return int.Parse(@string);
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x00150E13 File Offset: 0x0014F013
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x00150E1C File Offset: 0x0014F01C
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x00150E25 File Offset: 0x0014F025
	private void Update()
	{
		this.CheckAddFriendUI();
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x00150E30 File Offset: 0x0014F030
	private int GetTableIndex(string sName)
	{
		int result = 0;
		if (!(sName == "Toggle0"))
		{
			if (!(sName == "Toggle1"))
			{
				if (sName == "Toggle2")
				{
					result = 2;
				}
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x00150E80 File Offset: 0x0014F080
	private GameObject GetTable(string sName)
	{
		GameObject result = null;
		if (!(sName == "Toggle0"))
		{
			if (!(sName == "Toggle1"))
			{
				if (sName == "Toggle2")
				{
					result = this.oTable3;
				}
			}
			else
			{
				result = this.oTable2;
			}
		}
		else
		{
			result = this.oTable1;
		}
		return result;
	}

	// Token: 0x06002554 RID: 9556 RVA: 0x00150EE0 File Offset: 0x0014F0E0
	public void OnChangeEvent(GameObject obj)
	{
		bool flag = this.oNowSelectTab != null;
		GameObject table;
		if (flag)
		{
			this.oNowSelectTab.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SHEJIAOATLAS, "anniu_normal");
			table = this.GetTable(this.oNowSelectTab.name);
			table.SetActive(false);
		}
		obj.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SHEJIAOATLAS, "anniu_selecte");
		table = this.GetTable(obj.name);
		table.SetActive(true);
		this.oNowSelectTab = obj;
		this.OnShowWin(this.GetTableIndex(obj.name));
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x00150F84 File Offset: 0x0014F184
	public void OnChangeTab()
	{
		GameObject butStat = this.GetButStat();
		this.OnChangeEvent(butStat);
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x00150FA4 File Offset: 0x0014F1A4
	private GameObject GetButStat()
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		arrayList = this.GetFriendAllData(2);
		bool flag = arrayList.Count < 1;
		GameObject gameObject;
		if (flag)
		{
			this.oToggle3.SetActive(false);
			gameObject = this.oToggle1;
			arrayList = this.GetFriendAllData(1);
			bool flag2 = arrayList.Count < 1;
			if (flag2)
			{
				this.oToggle2.SetActive(false);
				num = 1;
			}
			else
			{
				this.oToggle2.SetActive(true);
			}
		}
		else
		{
			gameObject = this.oToggle3;
			this.oToggle3.SetActive(true);
			this.oToggle3.transform.Find("Label").GetComponent<Text>().text = Language.GetLang(31002, new object[0]) + "(" + (arrayList.Count / 2).ToString() + ")";
			arrayList = this.GetFriendAllData(1);
			bool flag3 = arrayList.Count < 1;
			if (flag3)
			{
				this.oToggle2.SetActive(false);
				num = 1;
			}
			else
			{
				this.oToggle2.SetActive(true);
			}
		}
		bool flag4 = num == 1;
		GameObject result;
		if (flag4)
		{
			result = this.oToggle1;
		}
		else
		{
			result = gameObject;
		}
		return result;
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x001510E0 File Offset: 0x0014F2E0
	private void InitFriendListObj(GameObject oPrent, GameObject oClone, int nCount)
	{
		for (int i = 0; i < nCount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(oClone);
			gameObject.transform.SetParent(oPrent.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			gameObject.name = i.ToString();
			bool flag = oPrent.name == "List0";
			if (flag)
			{
				gameObject.AddComponent<OneFriendObjCon>();
				this.mFriendList.Add(gameObject);
			}
			bool flag2 = oPrent.name == "List1";
			if (flag2)
			{
				gameObject.AddComponent<OneFriendObjCon>();
				this.mNewFriendList.Add(gameObject);
			}
			bool flag3 = oPrent.name == "List2";
			if (flag3)
			{
				gameObject.AddComponent<OneAppleMsgCon>();
				this.mApplyList.Add(gameObject);
			}
		}
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x001511EF File Offset: 0x0014F3EF
	public void OnShowWin(int nIndex)
	{
		this.OnOnLineNum();
		this.nNowPage = 1;
		this.nTageType = nIndex;
		this.OnShowTableData(this.nTageType, this.nNowPage);
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x0015121C File Offset: 0x0014F41C
	private void OnShowTableData(int nIndex, int nNowPage)
	{
		switch (nIndex)
		{
		case 0:
			this.OnSheJiaoTabData(nNowPage);
			break;
		case 1:
			this.OnFriendLatelyData(nNowPage);
			break;
		case 2:
			this.m_szFriendUnMsgData(nNowPage);
			break;
		}
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x00151260 File Offset: 0x0014F460
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x0015126A File Offset: 0x0014F46A
	public void OnCloseUI(GameObject OBJ)
	{
		fUIManager.GetInstance().SetWinActive("MainUI", true);
		this.CloseAddFriendUI(null);
		this.Close();
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x00151290 File Offset: 0x0014F490
	private void OnAddFrendEvent(GameObject obj)
	{
		bool flag = !this.AddFriendPage.activeSelf;
		if (flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = false;
			fUIManager.tipBlur.rawImage = this.rewardTipBg;
			fUIManager.tipBlur.enabled = true;
			this.tiptext.text = string.Empty;
			this.input.text = string.Empty;
			this.AddFriendPage.SetActive(true);
			EventSystem.current.SetSelectedGameObject(this.input.transform.gameObject);
		}
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x0015132C File Offset: 0x0014F52C
	private void CloseAddFriendUI(GameObject obj)
	{
		bool activeSelf = this.AddFriendPage.activeSelf;
		if (activeSelf)
		{
			this.rewardTipBg.enabled = false;
			this.AddFriendPage.SetActive(false);
			this.tiptext.text = string.Empty;
			GameRoot.inst.Setting.Load_Setting_Data_State = true;
		}
	}

	// Token: 0x0600255E RID: 9566 RVA: 0x00151388 File Offset: 0x0014F588
	private void OnAddFriendUI(GameObject obj)
	{
		string text = string.Empty;
		bool flag = !(this.input.text == string.Empty);
		if (flag)
		{
			text = this.input.text;
			int num = Fun.HaveShieldTxt(text, 0);
			bool flag2 = num != -1;
			if (flag2)
			{
				this.OnAddFriendRsMsg(0, Language.GetLang(31023, new object[0]));
			}
			else
			{
				bool flag3 = !Fun.CheckSpecialStr(text);
				if (flag3)
				{
					this.OnAddFriendRsMsg(0, Language.GetLang(31023, new object[0]));
				}
				else
				{
					text = text.Replace("\r\n", string.Empty);
					FriendManager.GetInstance().AddFriend(text);
				}
			}
		}
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x00151440 File Offset: 0x0014F640
	private void CheckAddFriendUI()
	{
		bool flag = this.rewardTipBg.texture != null;
		if (flag)
		{
			this.rewardTipBg.enabled = true;
		}
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x00151474 File Offset: 0x0014F674
	private ArrayList GetFriendAllData(int nTabType)
	{
		ArrayList result = new ArrayList();
		switch (nTabType)
		{
		case 0:
			result = this.GetArrayListSort(nTabType, PlayerDataManager.getInstance().HUserData.m_szFriendData);
			break;
		case 1:
			result = this.GetArrayListSort(nTabType, PlayerDataManager.getInstance().HUserData.m_szFriendLately);
			break;
		case 2:
			result = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg;
			break;
		}
		return result;
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x001514EC File Offset: 0x0014F6EC
	private void OnLeftTable(GameObject obj)
	{
		bool flag = this.nNowPage - 1 >= 1;
		if (flag)
		{
			this.nNowPage--;
			this.OnShowTableData(this.nTageType, this.nNowPage);
		}
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x00151530 File Offset: 0x0014F730
	private void OnRightTable(GameObject obj)
	{
		bool flag = this.nNowPage + 1 <= this.nMaxPage;
		if (flag)
		{
			this.nNowPage++;
			this.OnShowTableData(this.nTageType, this.nNowPage);
		}
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x00151578 File Offset: 0x0014F778
	private void OnSheJiaoTabData(int nNowPage)
	{
		ArrayList friendAllData = this.GetFriendAllData(0);
		int nPageNum = 48;
		this.OnShowFenYeData(nNowPage, nPageNum, friendAllData, this.mFriendList);
		this.pCheckBtn.isOn = false;
		bool flag = this.GetCheckToggle() == 1;
		if (flag)
		{
			this.pCheckBtn.isOn = true;
		}
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x001515CC File Offset: 0x0014F7CC
	private void OnFriendLatelyData(int nNowPage)
	{
		ArrayList friendAllData = this.GetFriendAllData(1);
		int nPageNum = 48;
		this.OnShowFenYeData(nNowPage, nPageNum, friendAllData, this.mNewFriendList);
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x001515F8 File Offset: 0x0014F7F8
	private void m_szFriendUnMsgData(int nNowPage)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList friendAllData = this.GetFriendAllData(2);
		ArrayList szData = new ArrayList();
		int nPageNum = 9;
		arrayList = this.GetFenYeUNData(nNowPage, nPageNum, friendAllData);
		int count = arrayList.Count;
		int count2 = this.mApplyList.Count;
		for (int i = 0; i < count2; i++)
		{
			GameObject gameObject = this.mApplyList[i];
			bool flag = gameObject == null;
			if (!flag)
			{
				OneAppleMsgCon oneAppleMsgCon = gameObject.GetComponent<OneAppleMsgCon>();
				bool flag2 = oneAppleMsgCon == null;
				if (flag2)
				{
					oneAppleMsgCon = gameObject.AddComponent<OneAppleMsgCon>();
				}
				szData = new ArrayList();
				bool flag3 = i < count;
				if (flag3)
				{
					int num = (int)arrayList[i];
					int num2 = friendAllData.IndexOf(num);
					bool flag4 = num2 != -1;
					if (flag4)
					{
						szData = (ArrayList)friendAllData[num2 + 1];
					}
					oneAppleMsgCon.name = num.ToString();
				}
				else
				{
					oneAppleMsgCon.name = i.ToString();
				}
				oneAppleMsgCon.OnSetUI(szData);
			}
		}
		this.OnPageBut(nNowPage, nPageNum, friendAllData);
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x0015172C File Offset: 0x0014F92C
	private void OnOnLineNum()
	{
		int num = 0;
		ArrayList arrayList = new ArrayList();
		ArrayList szFriendData = PlayerDataManager.getInstance().HUserData.m_szFriendData;
		int count = szFriendData.Count;
		for (int i = 0; i < count; i += 2)
		{
			arrayList = (ArrayList)szFriendData[i + 1];
			bool flag = arrayList.Count >= 1 && (int)arrayList[5] == 1;
			if (flag)
			{
				num++;
			}
		}
		this.tNowOnLineNum.text = Language.GetLang(31005, new object[]
		{
			num
		});
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x001517D0 File Offset: 0x0014F9D0
	public void UpDataOneFriend(string uname, int nState)
	{
		ArrayList arrayList = new ArrayList();
		int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(uname);
		bool flag = num != -1;
		if (flag)
		{
			arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num + 1];
		}
		bool flag2 = arrayList.Count < 1;
		if (!flag2)
		{
			this.OnSheJiaoTabData(this.nNowPage);
			Transform transform = this.oList1.transform.FindChild(uname);
			bool flag3 = transform != null;
			if (flag3)
			{
				OneFriendObjCon component = transform.gameObject.GetComponent<OneFriendObjCon>();
				bool flag4 = component != null;
				if (flag4)
				{
					component.OnSetUI(0, arrayList);
				}
			}
		}
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x0015188C File Offset: 0x0014FA8C
	private ArrayList GetFenYeUNData(int nTable, int nPageNum, ArrayList szListData)
	{
		ArrayList arrayList = new ArrayList();
		int num = szListData.Count / 2;
		bool flag = num > 0;
		if (flag)
		{
			int num2 = this.GetStartPage(nTable, nPageNum);
			int num3 = this.GetEndPage(nTable, nPageNum);
			bool flag2 = num3 > num;
			if (flag2)
			{
				num3 = num;
			}
			num2 *= 2;
			num3 *= 2;
			for (int i = num2; i < num3; i += 2)
			{
				int num4 = (int)szListData[i];
				arrayList.Add(num4);
			}
		}
		return arrayList;
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x00151920 File Offset: 0x0014FB20
	private ArrayList GetFenYeData(int nTable, int nPageNum, ArrayList szListData)
	{
		ArrayList arrayList = new ArrayList();
		int count = szListData.Count;
		int startPage = this.GetStartPage(nTable, nPageNum);
		int num = this.GetEndPage(nTable, nPageNum);
		bool flag = num > count;
		if (flag)
		{
			num = count;
		}
		for (int i = startPage; i < num; i += 2)
		{
			string value = szListData[i].ToString();
			arrayList.Add(value);
		}
		return arrayList;
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x00151994 File Offset: 0x0014FB94
	private int GetStartPage(int nTable, int nPageNum)
	{
		return (nTable - 1) * nPageNum;
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x001519AC File Offset: 0x0014FBAC
	private int GetEndPage(int nTable, int nPageNum)
	{
		return (nTable - 1) * nPageNum + nPageNum;
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x001519C8 File Offset: 0x0014FBC8
	private void OnShowFenYeData(int nPage, int nPageNum, ArrayList szListData, List<GameObject> mFriendList)
	{
		string text = string.Empty;
		ArrayList szFrendData = new ArrayList();
		ArrayList arrayList = new ArrayList();
		arrayList = this.GetFenYeData(nPage, nPageNum, szListData);
		int count = arrayList.Count;
		int count2 = mFriendList.Count;
		for (int i = 0; i < count2; i++)
		{
			GameObject gameObject = mFriendList[i];
			bool flag = gameObject == null;
			if (!flag)
			{
				OneFriendObjCon oneFriendObjCon = gameObject.GetComponent<OneFriendObjCon>();
				bool flag2 = oneFriendObjCon == null;
				if (flag2)
				{
					oneFriendObjCon = gameObject.AddComponent<OneFriendObjCon>();
				}
				text = string.Empty;
				szFrendData = new ArrayList();
				bool flag3 = i < count;
				if (flag3)
				{
					text = arrayList[i].ToString();
					int num = szListData.IndexOf(text);
					bool flag4 = num != -1;
					if (flag4)
					{
						szFrendData = (ArrayList)szListData[num + 1];
					}
					gameObject.name = text;
				}
				else
				{
					gameObject.name = i.ToString();
				}
				oneFriendObjCon.OnSetUI(this.nTageType, szFrendData);
			}
		}
		this.OnPageBut(nPage, nPageNum / 2, szListData);
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x00151AEC File Offset: 0x0014FCEC
	private void OnPageBut(int nPage, int nPageNum, ArrayList szListData)
	{
		int num = szListData.Count / 2;
		int num2 = num / nPageNum;
		bool flag = num2 > 0 && num % nPageNum > 0;
		if (flag)
		{
			num2++;
		}
		else
		{
			bool flag2 = num2 == 0;
			if (flag2)
			{
				num2 = 1;
			}
		}
		this.nMaxPage = num2;
		this.pagenum.text = nPage.ToString() + " / " + this.nMaxPage.ToString();
		bool flag3 = nPage < 2;
		if (flag3)
		{
			this.oleftbut.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.oleftbut.GetComponent<Button>().interactable = true;
		}
		bool flag4 = nPage >= this.nMaxPage;
		if (flag4)
		{
			this.oRightbut.GetComponent<Button>().interactable = false;
		}
		else
		{
			this.oRightbut.GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x00151BCC File Offset: 0x0014FDCC
	public void OnAddFriendRsMsg(int nRet, string sMsg)
	{
		bool activeSelf = this.AddFriendPage.activeSelf;
		if (activeSelf)
		{
			bool flag = nRet == 1;
			if (flag)
			{
				this.CloseAddFriendUI(null);
			}
			else
			{
				this.tiptext.text = sMsg;
			}
		}
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x00151C10 File Offset: 0x0014FE10
	private ArrayList GetArrayListSort(int nTabType, ArrayList szData)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList();
		string text = string.Empty;
		int count = szData.Count;
		int checkToggle = this.GetCheckToggle();
		for (int i = 0; i < count; i += 2)
		{
			text = szData[i].ToString();
			arrayList2 = (ArrayList)szData[i + 1];
			bool flag = arrayList2.Count < 1;
			if (!flag)
			{
				bool flag2 = nTabType == 0;
				if (flag2)
				{
					bool flag3 = (int)arrayList2[5] == 1;
					if (flag3)
					{
						arrayList.Add(text);
						arrayList.Add(arrayList2);
					}
					else
					{
						bool flag4 = checkToggle == 0;
						if (flag4)
						{
							goto IL_D7;
						}
						arrayList3.Add(text);
						arrayList3.Add(arrayList2);
					}
				}
				bool flag5 = nTabType == 1;
				if (flag5)
				{
					arrayList4.Add(text);
				}
			}
			IL_D7:;
		}
		bool flag6 = nTabType == 0 && arrayList3.Count > 0;
		if (flag6)
		{
			arrayList.AddRange(arrayList3);
		}
		bool flag7 = nTabType == 1 && arrayList4.Count > 0;
		if (flag7)
		{
			count = arrayList4.Count;
			arrayList = new ArrayList();
			for (int j = 0; j < count; j++)
			{
				text = arrayList4[j].ToString();
				arrayList = this.GetSortData(text, szData, arrayList);
			}
			arrayList = this.GetAddData(arrayList, szData);
		}
		return arrayList;
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x00151D94 File Offset: 0x0014FF94
	private ArrayList GetAddData(ArrayList szSort, ArrayList szData)
	{
		string value = string.Empty;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int count = szSort.Count;
		for (int i = 0; i < count; i++)
		{
			value = szSort[i].ToString();
			int num = szData.IndexOf(value);
			bool flag = num != -1;
			if (flag)
			{
				arrayList2 = (ArrayList)szData[num + 1];
				bool flag2 = arrayList2.Count >= 1;
				if (flag2)
				{
					arrayList.Add(value);
					arrayList.Add(arrayList2);
				}
			}
		}
		return arrayList;
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x00151E38 File Offset: 0x00150038
	private ArrayList GetSortData(string sName, ArrayList szData, ArrayList szSort)
	{
		string sName2 = string.Empty;
		int count = szSort.Count;
		bool flag = count > 0;
		if (flag)
		{
			int nameData = this.GetNameData(sName, szData);
			for (int i = 0; i < count; i++)
			{
				sName2 = szSort[i].ToString();
				int nameData2 = this.GetNameData(sName2, szData);
				bool flag2 = nameData <= nameData2;
				if (flag2)
				{
					szSort.Insert(i, sName);
					break;
				}
				bool flag3 = i + 1 >= count;
				if (flag3)
				{
					szSort.Add(sName);
					break;
				}
			}
		}
		else
		{
			szSort.Add(sName);
		}
		return szSort;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x00151EE4 File Offset: 0x001500E4
	private int GetNameData(string sName, ArrayList szTemp)
	{
		ArrayList arrayList = new ArrayList();
		string sEndTime = string.Empty;
		int num = szTemp.IndexOf(sName);
		bool flag = num != -1;
		if (flag)
		{
			arrayList = (ArrayList)szTemp[num + 1];
		}
		bool flag2 = arrayList.Count > 0;
		if (flag2)
		{
			sEndTime = arrayList[4].ToString();
		}
		return this.GetEndCDTime(sEndTime);
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x00151F54 File Offset: 0x00150154
	private int GetEndCDTime(string sEndTime)
	{
		int num = 0;
		bool flag = sEndTime == string.Empty;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime d = DateTime.Parse(sEndTime);
			result = (int)(Fun.GetServerDate() - d).TotalSeconds;
		}
		return result;
	}

	// Token: 0x040020EB RID: 8427
	public static SheJiaoUICon Inst;

	// Token: 0x040020EC RID: 8428
	private Text tNowOnLineNum;

	// Token: 0x040020ED RID: 8429
	private GameObject oAddFriend;

	// Token: 0x040020EE RID: 8430
	private GameObject CloneoneTable;

	// Token: 0x040020EF RID: 8431
	private GameObject CloneApple;

	// Token: 0x040020F0 RID: 8432
	private Text pagenum;

	// Token: 0x040020F1 RID: 8433
	private GameObject oRightbut;

	// Token: 0x040020F2 RID: 8434
	private GameObject BackBtn;

	// Token: 0x040020F3 RID: 8435
	private GameObject AddFriendPage;

	// Token: 0x040020F4 RID: 8436
	private GameObject oleftbut;

	// Token: 0x040020F5 RID: 8437
	private RawImage rewardTipBg;

	// Token: 0x040020F6 RID: 8438
	private InputField input;

	// Token: 0x040020F7 RID: 8439
	private Text tiptext;

	// Token: 0x040020F8 RID: 8440
	private GameObject oCloseAddFriend;

	// Token: 0x040020F9 RID: 8441
	private GameObject oAddBtnFriend;

	// Token: 0x040020FA RID: 8442
	private GameObject oList1;

	// Token: 0x040020FB RID: 8443
	private GameObject oList2;

	// Token: 0x040020FC RID: 8444
	private GameObject oList3;

	// Token: 0x040020FD RID: 8445
	private List<GameObject> mFriendList = new List<GameObject>();

	// Token: 0x040020FE RID: 8446
	private List<GameObject> mNewFriendList = new List<GameObject>();

	// Token: 0x040020FF RID: 8447
	private List<GameObject> mApplyList = new List<GameObject>();

	// Token: 0x04002100 RID: 8448
	public GameObject oToggle1;

	// Token: 0x04002101 RID: 8449
	private GameObject oToggle2;

	// Token: 0x04002102 RID: 8450
	private GameObject oToggle3;

	// Token: 0x04002103 RID: 8451
	private GameObject oTable1;

	// Token: 0x04002104 RID: 8452
	private GameObject oTable2;

	// Token: 0x04002105 RID: 8453
	private GameObject oTable3;

	// Token: 0x04002106 RID: 8454
	private GameObject oInputField;

	// Token: 0x04002107 RID: 8455
	private Toggle pCheckBtn;

	// Token: 0x04002108 RID: 8456
	private GameObject oNowSelectTab;

	// Token: 0x04002109 RID: 8457
	private int nNowPage = 1;

	// Token: 0x0400210A RID: 8458
	private int nMaxPage = 1;

	// Token: 0x0400210B RID: 8459
	public int nTageType;
}
