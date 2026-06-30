using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class FriendManager
{
	// Token: 0x06000B91 RID: 2961 RVA: 0x0006C10C File Offset: 0x0006A30C
	public static FriendManager GetInstance()
	{
		bool flag = FriendManager.instance == null;
		if (flag)
		{
			FriendManager.instance = new FriendManager();
		}
		return FriendManager.instance;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0006C13C File Offset: 0x0006A33C
	public void OnFriendMsg(ArrayList szNetMsg)
	{
		int num = (int)szNetMsg[0];
		int num2 = num;
		if (num2 <= 60114)
		{
			switch (num2)
			{
			case 60102:
				this.RequsttoMe(szNetMsg);
				break;
			case 60103:
			case 60105:
			case 60108:
				break;
			case 60104:
				this.UpDataFriendInfo((int)szNetMsg[1], (string)szNetMsg[2], (ArrayList)szNetMsg[3], (int)szNetMsg[4]);
				break;
			case 60106:
			{
				bool flag = SheJiaoUICon.Inst != null;
				if (flag)
				{
					SheJiaoUICon.Inst.OnAddFriendRsMsg((int)szNetMsg[1], Language.GetLang(31009, new object[]
					{
						Fun.DecodingUTF8(szNetMsg[2].ToString())
					}));
				}
				break;
			}
			case 60107:
				this.UpDataFriendData((ArrayList)szNetMsg[1], Fun.DecodingUTF8(szNetMsg[2].ToString()), (int)szNetMsg[3]);
				break;
			case 60109:
				this.Add_Del_User(szNetMsg);
				break;
			default:
				if (num2 == 60114)
				{
					this.OnFindApplyReturn(szNetMsg);
				}
				break;
			}
		}
		else if (num2 != 60115)
		{
			if (num2 != 60200)
			{
				if (num2 == 60204)
				{
					this.UpdateSkyLevel(szNetMsg);
				}
			}
			else
			{
				this.OnUpdateFriendLatelyData((ArrayList)szNetMsg[1]);
			}
		}
		else
		{
			this.OnGameAddUnMsg(szNetMsg);
		}
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0006C2D4 File Offset: 0x0006A4D4
	private void UpdateSkyLevel(ArrayList szNetMsg)
	{
		ArrayList arrayList = new ArrayList();
		string text = Fun.DecodingUTF8(szNetMsg[1].ToString());
		int num = (int)szNetMsg[2];
		int num2 = (int)szNetMsg[3];
		bool flag = num2 == 0;
		if (flag)
		{
			int num3 = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(text);
			bool flag2 = num3 != -1;
			if (flag2)
			{
				arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num3 + 1];
				arrayList[8] = num;
				PlayerDataManager.getInstance().HUserData.m_szFriendData[num3 + 1] = arrayList;
			}
		}
		bool flag3 = num2 == 1;
		if (flag3)
		{
			int num4 = PlayerDataManager.getInstance().HUserData.m_szFriendLately.IndexOf(text);
			bool flag4 = num4 != -1;
			if (flag4)
			{
				arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendLately[num4 + 1];
				arrayList[8] = num;
				PlayerDataManager.getInstance().HUserData.m_szFriendLately[num4 + 1] = arrayList;
			}
		}
		bool flag5 = SheJiaoUICon.Inst != null;
		if (flag5)
		{
			SheJiaoUICon.Inst.UpDataOneFriend(text, 0);
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0006C428 File Offset: 0x0006A628
	private void OnUpdateFriendLatelyData(ArrayList szMsg)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		int num = (int)szMsg[0];
		bool flag = num == 1;
		if (flag)
		{
			arrayList = (ArrayList)szMsg[1];
			int count = arrayList.Count;
			for (int i = 0; i < count; i += 2)
			{
				string value = Fun.DecodingUTF8(arrayList[i].ToString());
				arrayList2 = (ArrayList)arrayList[i + 1];
				arrayList2[1] = Fun.DecodingUTF8(arrayList2[1].ToString());
				bool flag2 = arrayList2.Count >= 1;
				if (flag2)
				{
					int num2 = PlayerDataManager.getInstance().HUserData.m_szFriendLately.IndexOf(value);
					bool flag3 = num2 == -1;
					if (flag3)
					{
						PlayerDataManager.getInstance().HUserData.m_szFriendLately.Add(value);
						PlayerDataManager.getInstance().HUserData.m_szFriendLately.Add(arrayList2);
					}
					else
					{
						PlayerDataManager.getInstance().HUserData.m_szFriendLately[num2 + 1] = arrayList2;
					}
				}
			}
		}
		else
		{
			string value2 = Fun.DecodingUTF8(szMsg[1].ToString());
			int num3 = PlayerDataManager.getInstance().HUserData.m_szFriendLately.IndexOf(value2);
			bool flag4 = num3 != -1;
			if (flag4)
			{
				PlayerDataManager.getInstance().HUserData.m_szFriendLately.RemoveAt(num3 + 1);
				PlayerDataManager.getInstance().HUserData.m_szFriendLately.RemoveAt(num3);
			}
		}
		bool flag5 = SheJiaoUICon.Inst != null;
		if (flag5)
		{
			SheJiaoUICon.Inst.OnChangeTab();
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0006C5E0 File Offset: 0x0006A7E0
	private void OnGameAddUnMsg(ArrayList szNetMsg)
	{
		bool flag = szNetMsg.Count < 1;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)szNetMsg[1];
			arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
			int num = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.IndexOf((int)arrayList[0]);
			bool flag2 = num == -1;
			if (flag2)
			{
				PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.Add((int)arrayList[0]);
				PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.Add(arrayList);
				bool flag3 = GameRoot.inst.Setting.Social_Class.Auto_Accept_Add_Friend_Apply == 1;
				if (flag3)
				{
					FriendManager.GetInstance().UnRequstMsg(1, arrayList[1].ToString(), (int)arrayList[0]);
				}
				else
				{
					fUIManager.GetInstance().OnPushMsg(new ArrayList
					{
						3,
						Language.GetLang(31016, new object[]
						{
							arrayList[1].ToString()
						}),
						new ArrayList
						{
							arrayList[1].ToString(),
							(int)arrayList[0]
						}
					}, 20);
				}
			}
		}
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0006C760 File Offset: 0x0006A960
	private void OnFindApplyReturn(ArrayList szMsg)
	{
		string text = string.Empty;
		szMsg[1] = Fun.DecodingUTF8(szMsg[1].ToString());
		switch ((int)szMsg[2])
		{
		case -1:
			text = Language.GetLang(31013, new object[0]);
			break;
		case 0:
			text = Language.GetLang(31018, new object[]
			{
				szMsg[1].ToString()
			});
			break;
		case 1:
			text = Language.GetLang(31017, new object[]
			{
				szMsg[1].ToString()
			});
			break;
		case 2:
			text = Language.GetLang(31019, new object[0]);
			break;
		case 3:
			text = Language.GetLang(31020, new object[0]);
			break;
		case 4:
		{
			text = Language.GetLang(31021, new object[0]);
			bool flag = SheJiaoUICon.Inst != null;
			if (flag)
			{
				SheJiaoUICon.Inst.OnAddFriendRsMsg(1, string.Empty);
			}
			bool flag2 = GameResultUICon.Inst != null && GameResultUICon.Inst.visible;
			if (flag2)
			{
				GameResultUICon.Inst.GetAddFriendStat(szMsg[1].ToString());
			}
			break;
		}
		case 5:
		{
			text = Language.GetLang(31022, new object[]
			{
				szMsg[1].ToString()
			});
			bool flag3 = SheJiaoUICon.Inst != null;
			if (flag3)
			{
				SheJiaoUICon.Inst.OnAddFriendRsMsg(0, text);
			}
			break;
		}
		}
		bool flag4 = text != string.Empty;
		if (flag4)
		{
			ChatManager.GetInstance().OnSendSystemMsg(text, "ffdd00", "system");
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0006C92C File Offset: 0x0006AB2C
	private void RequsttoMe(ArrayList szMsg)
	{
		ArrayList arrayList = szMsg[1] as ArrayList;
		ArrayList arrayList2 = (ArrayList)(arrayList[5] as ArrayList).Clone();
		arrayList[3] = Fun.DecodingUTF8(arrayList[3].ToString());
		arrayList[4] = Fun.DecodingUTF8(arrayList[4].ToString());
		arrayList2[1] = Fun.DecodingUTF8(arrayList2[1].ToString());
		bool flag = !this.RequstMsgArr.ContainsKey(arrayList[3].ToString());
		if (flag)
		{
			this.RequstMsgArr.Add(arrayList[3].ToString(), (ArrayList)arrayList[5]);
		}
		bool flag2 = arrayList2.Count <= 0;
		if (!flag2)
		{
			int num = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.IndexOf((int)arrayList2[0]);
			bool flag3 = num != -1;
			if (!flag3)
			{
				PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.Add((int)arrayList2[0]);
				PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.Add(arrayList2);
				bool flag4 = MainUICon.inst != null;
				if (flag4)
				{
					MainUICon.inst.UpdateUnMsgNum();
				}
				bool flag5 = GameRoot.inst.Setting.Social_Class.Auto_Accept_Add_Friend_Apply == 1;
				if (flag5)
				{
					FriendManager.GetInstance().UnRequstMsg(1, arrayList2[1].ToString(), (int)arrayList2[0]);
				}
				else
				{
					fUIManager.GetInstance().OnPushMsg(new ArrayList
					{
						3,
						Language.GetLang(31016, new object[]
						{
							arrayList2[1].ToString()
						}),
						new ArrayList
						{
							arrayList2[1].ToString(),
							(int)arrayList2[0]
						}
					}, 20);
					bool flag6 = SheJiaoUICon.Inst != null;
					if (flag6)
					{
						SheJiaoUICon.Inst.OnChangeTab();
					}
				}
			}
		}
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0006CB80 File Offset: 0x0006AD80
	private void OnChatOnLineMsg(ArrayList szNetMsg)
	{
		string text = string.Empty;
		bool flag = szNetMsg.Count >= 1 && GameRoot.inst.Setting.Social_Class.Friends_Online_Notice == 1;
		if (flag)
		{
			string text2 = szNetMsg[1].ToString();
			text = (((int)szNetMsg[5] != 1) ? Language.GetLang(31015, new object[]
			{
				text2
			}) : Language.GetLang(31014, new object[]
			{
				text2
			}));
			bool flag2 = text != string.Empty;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(text, "ffdd00", "system");
			}
		}
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0006CC2C File Offset: 0x0006AE2C
	private int GetUserOnLineStat(string sUname)
	{
		int result = 0;
		ArrayList arrayList = new ArrayList();
		int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(sUname);
		bool flag = num != -1;
		if (flag)
		{
			arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num + 1];
			bool flag2 = arrayList.Count > 0;
			if (flag2)
			{
				result = (int)arrayList[5];
			}
		}
		return result;
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0006CCA8 File Offset: 0x0006AEA8
	private void UpDataFriendData(ArrayList szNetMsg, string sUname, int nType)
	{
		int userOnLineStat = this.GetUserOnLineStat(sUname);
		for (int i = 0; i < szNetMsg.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(szNetMsg[i].ToString());
			if (flag)
			{
				szNetMsg[i] = Fun.DecodingUTF8(szNetMsg[i].ToString());
				ArrayList arrayList = (ArrayList)szNetMsg[i + 1];
				arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
			}
		}
		PlayerDataManager.getInstance().HUserData.m_szFriendData = szNetMsg;
		bool flag2 = sUname != string.Empty;
		if (flag2)
		{
			bool flag3 = Friend_UICon.inst != null;
			if (flag3)
			{
				Friend_UICon.inst.UpDataOneFriend(sUname, 0);
			}
			bool flag4 = SheJiaoUICon.Inst != null;
			if (flag4)
			{
				SheJiaoUICon.Inst.UpDataOneFriend(sUname, 0);
			}
			bool flag5 = nType == 0;
			if (flag5)
			{
				int userOnLineStat2 = this.GetUserOnLineStat(sUname);
				int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(sUname);
				bool flag6 = num != -1 && userOnLineStat != userOnLineStat2;
				if (flag6)
				{
					ArrayList szNetMsg2 = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num + 1];
					this.OnChatOnLineMsg(szNetMsg2);
				}
			}
		}
		bool flag7 = MainUICon.inst != null;
		if (flag7)
		{
			MainUICon.inst.SetFriendOnLineNum();
		}
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x0006CE24 File Offset: 0x0006B024
	private void UpDataFriendInfo(int nPos, string sUname, ArrayList szNetMsg, int nState)
	{
		sUname = Fun.DecodingUTF8(sUname);
		szNetMsg[1] = Fun.DecodingUTF8(szNetMsg[1].ToString());
		PlayerDataManager.getInstance().HUserData.m_szFriendData[nPos] = szNetMsg;
		this.OnChatOnLineMsg(szNetMsg);
		bool flag = Friend_UICon.inst != null;
		if (flag)
		{
			Friend_UICon.inst.UpDataOneFriend(sUname, nState);
		}
		bool flag2 = SheJiaoUICon.Inst != null;
		if (flag2)
		{
			SheJiaoUICon.Inst.UpDataOneFriend(sUname, nState);
		}
		bool flag3 = nState == 0 && MainUICon.inst != null;
		if (flag3)
		{
			MainUICon.inst.SetFriendOnLineNum();
		}
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0006CED4 File Offset: 0x0006B0D4
	public void AddFriend(string sUname)
	{
		int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.Count / 2;
		bool flag = num + 1 > 100;
		if (flag)
		{
			bool flag2 = SheJiaoUICon.Inst != null;
			if (flag2)
			{
				SheJiaoUICon.Inst.OnAddFriendRsMsg(0, Language.GetLang(31011, new object[0]));
			}
			this.OnFindApplyReturn(new ArrayList
			{
				0,
				Fun.EncodingUTF8(string.Empty),
				3
			});
		}
		else
		{
			bool flag3 = num > 0;
			if (flag3)
			{
				int num2 = PlayerDataManager.getInstance().HUserData.m_szFriendData.LastIndexOf(sUname);
				bool flag4 = num2 != -1;
				if (flag4)
				{
					bool flag5 = SheJiaoUICon.Inst != null;
					if (flag5)
					{
						SheJiaoUICon.Inst.OnAddFriendRsMsg(0, Language.GetLang(31010, new object[]
						{
							sUname
						}));
					}
					return;
				}
			}
			this.FriendMsgToServer(new ArrayList
			{
				60101,
				Fun.EncodingUTF8(sUname)
			});
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0006D004 File Offset: 0x0006B204
	public void DelFriend(string sUname)
	{
		TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(14001, new object[]
		{
			sUname
		}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnEntDelFriend), sUname, null, null);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0006D078 File Offset: 0x0006B278
	public void OnEntDelFriend(object szParam)
	{
		string text = (string)szParam;
		bool flag = PlayerDataManager.getInstance().HUserData.m_szFriendData.Count > 0;
		if (flag)
		{
			int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.LastIndexOf(text);
			bool flag2 = num == -1;
			if (flag2)
			{
				Debug.LogError("没找到好友！");
				return;
			}
		}
		this.FriendMsgToServer(new ArrayList
		{
			60103,
			Fun.EncodingUTF8(text)
		});
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0006D108 File Offset: 0x0006B308
	public bool IsFriendRlt(string sname)
	{
		bool result = false;
		bool flag = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(sname) != -1;
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0006D140 File Offset: 0x0006B340
	public void RequstMsg(int nState, string sUname)
	{
		ArrayList value = new ArrayList();
		bool flag = this.RequstMsgArr.ContainsKey(sUname);
		if (flag)
		{
			value = this.RequstMsgArr[sUname];
		}
		this.FriendMsgToServer(new ArrayList
		{
			60105,
			value,
			nState
		});
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0006D1A8 File Offset: 0x0006B3A8
	public void UnRequstMsg(int nState, string sUname, int nCid)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(nCid);
		arrayList.Add(Fun.EncodingUTF8(sUname));
		this.FriendMsgToServer(new ArrayList
		{
			60105,
			arrayList,
			nState
		});
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0006D20C File Offset: 0x0006B40C
	private void Add_Del_User(ArrayList arr)
	{
		ArrayList arrayList = (ArrayList)(arr[2] as ArrayList).Clone();
		arrayList[1] = Fun.DecodingUTF8(arrayList[1].ToString());
		string value = Fun.DecodingUTF8(arr[1].ToString());
		bool flag = (int)arr[3] == 1;
		int num;
		if (flag)
		{
			num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(value);
			bool flag2 = num == -1;
			if (flag2)
			{
				PlayerDataManager.getInstance().HUserData.m_szFriendData.Add(value);
				PlayerDataManager.getInstance().HUserData.m_szFriendData.Add(arrayList);
			}
		}
		else
		{
			num = PlayerDataManager.getInstance().HUserData.m_szFriendData.IndexOf(value);
			bool flag3 = num != -1;
			if (flag3)
			{
				PlayerDataManager.getInstance().HUserData.m_szFriendData.RemoveAt(num + 1);
				PlayerDataManager.getInstance().HUserData.m_szFriendData.RemoveAt(num);
			}
		}
		num = PlayerDataManager.getInstance().HUserData.m_szFriendLately.IndexOf(value);
		bool flag4 = num != -1;
		if (flag4)
		{
			PlayerDataManager.getInstance().HUserData.m_szFriendLately.RemoveAt(num + 1);
			PlayerDataManager.getInstance().HUserData.m_szFriendLately.RemoveAt(num);
		}
		num = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.IndexOf((int)arrayList[0]);
		bool flag5 = num != -1;
		if (flag5)
		{
			PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.RemoveAt(num + 1);
			PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.RemoveAt(num);
			bool flag6 = MainUICon.inst != null;
			if (flag6)
			{
				MainUICon.inst.UpdateUnMsgNum();
			}
		}
		bool flag7 = MainUICon.inst != null;
		if (flag7)
		{
			MainUICon.inst.SetFriendOnLineNum();
		}
		bool flag8 = Friend_UICon.inst != null;
		if (flag8)
		{
			Friend_UICon.inst.Add_Del_User(arrayList, (int)arr[3]);
		}
		bool flag9 = SheJiaoUICon.Inst != null;
		if (flag9)
		{
			SheJiaoUICon.Inst.OnChangeTab();
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0006D454 File Offset: 0x0006B654
	public ArrayList GetOnLineFriendList()
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		for (int i = 1; i < PlayerDataManager.getInstance().HUserData.m_szFriendData.Count; i += 2)
		{
			arrayList2 = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[i];
			bool flag = (int)arrayList2[5] > 0;
			if (flag)
			{
				arrayList.Add(arrayList2);
			}
		}
		return arrayList;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0006D4D4 File Offset: 0x0006B6D4
	private void FriendMsgToServer(ArrayList arr)
	{
		fGameManager.LobbyPlayer.SendMsgToHallServer(60100, arr);
	}

	// Token: 0x040009F4 RID: 2548
	private static FriendManager instance;

	// Token: 0x040009F5 RID: 2549
	public ArrayList MyTeamData = new ArrayList();

	// Token: 0x040009F6 RID: 2550
	private Dictionary<string, ArrayList> RequstMsgArr = new Dictionary<string, ArrayList>();
}
