using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000225 RID: 549
public class LookingTeamCon : Window
{
	// Token: 0x060018C7 RID: 6343 RVA: 0x000E71D8 File Offset: 0x000E53D8
	protected override void Awake()
	{
		base.Awake();
		this.QuickBtn = this.UIGameObj.Get_GameObj(0);
		this.QuickBtn.name = "QuickBtn";
		this.SkyBtn = this.UIGameObj.Get_GameObj(1);
		this.SkyBtn.name = "SkyBtn";
		this.PveBtn = this.UIGameObj.Get_GameObj(2);
		this.PveBtn.name = "PveBtn";
		this.Content = this.UIGameObj.Get_GameObj(3);
		this.SonGameobj = this.UIGameObj.Get_GameObj(4);
		this.SonGameobj.name = "SonGameobj";
		this.SonGameobj.SetActive(false);
		this.PageText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.LeftBtn = this.UIGameObj.Get_GameObj(7).GetComponent<Button>();
		this.RightBtn = this.UIGameObj.Get_GameObj(8).GetComponent<Button>();
		this.CreateBtn = this.UIGameObj.Get_GameObj(9);
		this.JoinBtn = this.UIGameObj.Get_GameObj(10);
		EventTriggerListener.Get(this.QuickBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectTeam);
		EventTriggerListener.Get(this.SkyBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectTeam);
		EventTriggerListener.Get(this.PveBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSelectTeam);
		EventTriggerListener.Get(this.LeftBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnUpPage);
		EventTriggerListener.Get(this.RightBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnDownPage);
		EventTriggerListener.Get(this.CreateBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnCreateTeam);
		EventTriggerListener.Get(this.JoinBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnQuickJoin);
		LookingTeamCon.inst = this;
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x000E73D8 File Offset: 0x000E55D8
	private void Start()
	{
		this.nSelectCD = Time.realtimeSinceStartup;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(60018);
		arrayList.Add(this.m_nType);
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
		this.m_nType = 1;
		base.CancelInvoke("OnTimerCall");
		base.Invoke("OnTimerCall", this.fTimerCall);
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x000E7450 File Offset: 0x000E5650
	public override void Reset()
	{
		base.Reset();
		this.OnSelectTeam(this.QuickBtn);
		base.CancelInvoke("OnTimerCall");
		base.Invoke("OnTimerCall", this.fTimerCall);
	}

	// Token: 0x060018CA RID: 6346 RVA: 0x000E7485 File Offset: 0x000E5685
	private void Update()
	{
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x000E7488 File Offset: 0x000E5688
	private void OnSelectTeam(GameObject obj)
	{
		fUIManager.OnClearObj(this.szSonRender);
		string text = string.Empty;
		text = obj.name;
		obj.GetComponent<Toggle>().isOn = true;
		string text2 = text;
		string a = text2;
		if (!(a == "QuickBtn"))
		{
			if (!(a == "SkyBtn"))
			{
				if (a == "PveBtn")
				{
					this.m_nType = 3;
				}
			}
			else
			{
				this.m_nType = 2;
			}
		}
		else
		{
			this.m_nType = 1;
		}
		bool flag = Time.realtimeSinceStartup - this.nSelectCD > 10f;
		if (flag)
		{
			this.nSelectCD = Time.realtimeSinceStartup;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(60018);
			arrayList.Add(this.m_nType);
			fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
			base.CancelInvoke("OnTimerCall");
			base.Invoke("OnTimerCall", this.fTimerCall);
		}
		else
		{
			this.SetData(this.m_nType);
		}
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x000E7598 File Offset: 0x000E5798
	private void OnTimerCall()
	{
		switch (this.m_nType)
		{
		case 1:
			this.OnSelectTeam(this.QuickBtn);
			break;
		case 2:
			this.OnSelectTeam(this.SkyBtn);
			break;
		case 3:
			this.OnSelectTeam(this.PveBtn);
			break;
		}
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x000E75F4 File Offset: 0x000E57F4
	public void SetLookingTeamData(ArrayList arr, int nType)
	{
		fUIManager.OnClearObj(this.szSonRender);
		this.m_szQuickData.Clear();
		this.m_szSkyData.Clear();
		this.m_szPveData.Clear();
		ArrayList arrayList = new ArrayList();
		for (int i = 1; i < arr.Count; i += 2)
		{
			arrayList = (ArrayList)arr[i];
			arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
			bool flag = (int)arrayList[6] == 1;
			if (flag)
			{
				this.m_szQuickData.Add(arrayList);
			}
			bool flag2 = (int)arrayList[6] == 2;
			if (flag2)
			{
				this.m_szSkyData.Add(arrayList);
			}
			bool flag3 = (int)arrayList[6] == 3;
			if (flag3)
			{
				this.m_szPveData.Add(arrayList);
			}
		}
		bool flag4 = arr.Count > 0;
		if (flag4)
		{
			this.SetData(nType);
		}
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x000E7700 File Offset: 0x000E5900
	private void SetData(int nType)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = nType == 1;
		if (flag)
		{
			arrayList = (ArrayList)this.m_szQuickData.Clone();
		}
		bool flag2 = nType == 2;
		if (flag2)
		{
			arrayList = (ArrayList)this.m_szSkyData.Clone();
		}
		bool flag3 = nType == 3;
		if (flag3)
		{
			arrayList = (ArrayList)this.m_szPveData.Clone();
		}
		int count = arrayList.Count;
		bool flag4 = (this.m_nNowPage - 1) * 12 > count;
		if (flag4)
		{
			this.m_nNowPage = 1;
			int num = (arrayList.Count <= 12) ? arrayList.Count : 12;
			bool flag5 = num > arrayList.Count;
			if (flag5)
			{
				num = arrayList.Count;
			}
			for (int i = 0; i < num; i++)
			{
				this.InitTeamData((ArrayList)(arrayList[i] as ArrayList).Clone());
			}
		}
		else
		{
			int num = (arrayList.Count <= (this.m_nNowPage - 1) * 12) ? arrayList.Count : ((this.m_nNowPage - 1) * 12 + 12);
			bool flag6 = num > arrayList.Count;
			if (flag6)
			{
				num = arrayList.Count;
			}
			for (int j = (this.m_nNowPage - 1) * 12; j < num; j++)
			{
				this.InitTeamData((ArrayList)(arrayList[j] as ArrayList).Clone());
			}
		}
		this.InitPage(count);
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x000E7888 File Offset: 0x000E5A88
	private void InitPage(int nNum)
	{
		bool flag = nNum == 0;
		if (flag)
		{
			this.nMax = 1;
		}
		else
		{
			bool flag2 = nNum % 12 == 0;
			if (flag2)
			{
				this.nMax = nNum / 12;
			}
			else
			{
				this.nMax = nNum / 12 + 1;
			}
		}
		this.PageText.text = this.m_nNowPage.ToString() + "/" + this.nMax.ToString();
		bool flag3 = this.nMax == 1;
		if (flag3)
		{
			this.LeftBtn.interactable = false;
			this.RightBtn.interactable = false;
		}
		else
		{
			bool flag4 = this.nMax > this.m_nNowPage;
			if (flag4)
			{
				this.RightBtn.interactable = true;
			}
		}
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x000E7948 File Offset: 0x000E5B48
	private void OnCreateTeam(GameObject obj)
	{
		bool flag = TeamManager.GetInstance().lookingTeamId != 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40054, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40051, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = MatchManager.GetInstance().m_nIsMatch > 0 || SkyLadderManager.GetInstance().isSkyMatching == 1;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40049, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool flag4 = fGameManager.Heroplayer == null;
					if (flag4)
					{
						ArrayList arrayList = new ArrayList();
						bool flag5 = TeamManager.GetInstance().m_nCaptain == 1;
						if (flag5)
						{
							arrayList.Add(60019);
							arrayList.Add(this.m_nType);
						}
						else
						{
							arrayList.Add(60001);
							arrayList.Add(this.m_nType);
						}
						bool flag6 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
						if (flag6)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6043, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnJoinLookingTeam), arrayList, null, null);
						}
						else
						{
							fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
						}
					}
				}
			}
		}
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x000E7BA8 File Offset: 0x000E5DA8
	private void OnQuickJoin(GameObject obj)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40052, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				MatchManager.GetInstance().OnCancelMatch();
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40027, new object[0]), "ffdd00", "system");
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			string s = string.Empty;
			bool flag3 = this.m_nType == 1;
			if (flag3)
			{
				arrayList = this.m_szQuickData;
			}
			bool flag4 = this.m_nType == 2;
			if (flag4)
			{
				arrayList = this.m_szSkyData;
			}
			bool flag5 = this.m_nType == 3;
			if (flag5)
			{
				arrayList = this.m_szPveData;
			}
			bool flag6 = arrayList.Count > 0;
			if (flag6)
			{
				arrayList2 = (ArrayList)arrayList[0];
				int num = (int)arrayList2[0];
				s = arrayList2[1].ToString();
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(60002);
				arrayList3.Add(new ArrayList
				{
					Fun.EncodingUTF8(s),
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					num
				});
				arrayList3.Add(this.m_nType);
				bool flag7 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag7)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6043, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnJoinLookingTeam), arrayList3, null, null);
				}
				else
				{
					fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList3);
				}
			}
		}
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x000E7E68 File Offset: 0x000E6068
	private void OnJoinLookingTeam(object szParam)
	{
		ArrayList szMsg = (ArrayList)szParam;
		bool flag = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag)
		{
			RoomManage.GetInstance().OnLeave();
		}
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, szMsg);
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000E7EB4 File Offset: 0x000E60B4
	private void OnUpPage(GameObject obj)
	{
		bool flag = this.m_nNowPage > 1;
		if (flag)
		{
			this.m_nNowPage--;
			bool flag2 = this.m_nNowPage <= 1;
			if (flag2)
			{
				this.LeftBtn.interactable = false;
			}
			else
			{
				this.LeftBtn.interactable = true;
			}
			this.OnPaged();
		}
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000E7F18 File Offset: 0x000E6118
	private void OnDownPage(GameObject obj)
	{
		bool flag = this.m_nNowPage < this.nMax;
		if (flag)
		{
			this.m_nNowPage++;
			this.LeftBtn.interactable = true;
			bool flag2 = this.m_nNowPage >= this.nMax;
			if (flag2)
			{
				this.RightBtn.interactable = false;
			}
			else
			{
				this.RightBtn.interactable = true;
			}
			this.OnPaged();
		}
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x000E7F94 File Offset: 0x000E6194
	private void OnPaged()
	{
		bool flag = this.m_nType == 1;
		if (flag)
		{
			this.OnSelectTeam(this.QuickBtn);
		}
		bool flag2 = this.m_nType == 2;
		if (flag2)
		{
			this.OnSelectTeam(this.SkyBtn);
		}
		bool flag3 = this.m_nType == 3;
		if (flag3)
		{
			this.OnSelectTeam(this.PveBtn);
		}
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000E7FF8 File Offset: 0x000E61F8
	private void InitTeamData(ArrayList arr)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SonGameobj);
		gameObject.transform.SetParent(this.Content.transform);
		gameObject.name = arr[0].ToString();
		gameObject.SetActive(true);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		this.szSonRender.Add(gameObject);
		OneLookingteamdata oneLookingteamdata = gameObject.AddComponent<OneLookingteamdata>();
		oneLookingteamdata.InitData(arr, this.m_nType);
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x000E8088 File Offset: 0x000E6288
	public void SetOneLookTeam(ArrayList arr)
	{
		bool flag = (int)arr[3] != this.m_nType;
		if (!flag)
		{
			int num = (int)arr[1];
			Transform[] componentsInChildren = this.Content.transform.GetComponentsInChildren<Transform>();
			foreach (Transform transform in componentsInChildren)
			{
				bool flag2 = transform.gameObject.name == num.ToString();
				if (flag2)
				{
					OneLookingteamdata component = transform.GetComponent<OneLookingteamdata>();
					component.SetNum((int)arr[2]);
					break;
				}
			}
		}
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x000E8130 File Offset: 0x000E6330
	public void RemoveTeamRender(int lookingTeamId)
	{
		int num = -1;
		for (int i = 0; i < this.m_szQuickData.Count; i++)
		{
			ArrayList arrayList = (ArrayList)this.m_szQuickData[i];
			bool flag = (int)arrayList[0] == lookingTeamId;
			if (flag)
			{
				num = i;
				break;
			}
		}
		bool flag2 = num != -1;
		if (flag2)
		{
			this.m_szQuickData.RemoveAt(num);
			this.OnPaged();
		}
		else
		{
			num = -1;
			for (int j = 0; j < this.m_szSkyData.Count; j++)
			{
				ArrayList arrayList2 = (ArrayList)this.m_szSkyData[j];
				bool flag3 = (int)arrayList2[0] == lookingTeamId;
				if (flag3)
				{
					num = j;
					break;
				}
			}
			bool flag4 = num != -1;
			if (flag4)
			{
				this.m_szSkyData.RemoveAt(num);
				this.OnPaged();
			}
			else
			{
				num = -1;
				for (int k = 0; k < this.m_szPveData.Count; k++)
				{
					ArrayList arrayList3 = (ArrayList)this.m_szPveData[k];
					bool flag5 = (int)arrayList3[0] == lookingTeamId;
					if (flag5)
					{
						num = k;
						break;
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					this.m_szPveData.RemoveAt(num);
					this.OnPaged();
				}
			}
		}
	}

	// Token: 0x0400136B RID: 4971
	public static LookingTeamCon inst;

	// Token: 0x0400136C RID: 4972
	private GameObject QuickBtn;

	// Token: 0x0400136D RID: 4973
	private GameObject SkyBtn;

	// Token: 0x0400136E RID: 4974
	private GameObject PveBtn;

	// Token: 0x0400136F RID: 4975
	private GameObject Content;

	// Token: 0x04001370 RID: 4976
	private GameObject SonGameobj;

	// Token: 0x04001371 RID: 4977
	private List<GameObject> szSonRender = new List<GameObject>();

	// Token: 0x04001372 RID: 4978
	private Text PageText;

	// Token: 0x04001373 RID: 4979
	private Button LeftBtn;

	// Token: 0x04001374 RID: 4980
	private Button RightBtn;

	// Token: 0x04001375 RID: 4981
	private GameObject CreateBtn;

	// Token: 0x04001376 RID: 4982
	private GameObject JoinBtn;

	// Token: 0x04001377 RID: 4983
	private int m_nType = 1;

	// Token: 0x04001378 RID: 4984
	private ArrayList m_szQuickData = new ArrayList();

	// Token: 0x04001379 RID: 4985
	private ArrayList m_szSkyData = new ArrayList();

	// Token: 0x0400137A RID: 4986
	private ArrayList m_szPveData = new ArrayList();

	// Token: 0x0400137B RID: 4987
	private int m_nNowPage = 1;

	// Token: 0x0400137C RID: 4988
	private int nMax = 1;

	// Token: 0x0400137D RID: 4989
	private float nSelectCD;

	// Token: 0x0400137E RID: 4990
	private float fTimerCall = 20f;
}
