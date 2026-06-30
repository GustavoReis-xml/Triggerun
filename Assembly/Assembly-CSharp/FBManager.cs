using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class FBManager
{
	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00060CA4 File Offset: 0x0005EEA4
	// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00060CBC File Offset: 0x0005EEBC
	public ArrayList m_szFbResultData
	{
		get
		{
			return this.szFbResultData;
		}
		set
		{
			this.szFbResultData = value;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00060CC8 File Offset: 0x0005EEC8
	// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00060CE0 File Offset: 0x0005EEE0
	public ArrayList szTabWinInfo
	{
		get
		{
			return this._szTabWinInfo;
		}
		set
		{
			this._szTabWinInfo = value;
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00060CEC File Offset: 0x0005EEEC
	public static FBManager GetInstance()
	{
		bool flag = FBManager.instance == null;
		if (flag)
		{
			FBManager.instance = new FBManager();
		}
		return FBManager.instance;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00060D1C File Offset: 0x0005EF1C
	public void OnFbMsg(int nNetid, ArrayList szNetMsg)
	{
		switch (nNetid)
		{
		case 5001:
		{
			bool flag = fGameManager.IsSingle || fGameManager.bGoToHall || fGameManager.Heroplayer == null || !fUIManager.mGameUI.activeSelf;
			if (!flag)
			{
				this.StartInfo = (ArrayList)szNetMsg.Clone();
				bool flag2 = !GameRoot.inst.Setting_FPS.GM_State;
				if (flag2)
				{
					bool flag3 = ZhanDianCon.inst == null;
					if (flag3)
					{
						fUIManager.GetInstance().ShowWin("ZhanDian", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.loaduiok), null, true);
					}
					else
					{
						ZhanDianCon.inst.Open();
						ZhanDianCon.inst.InitData(szNetMsg);
					}
				}
			}
			break;
		}
		case 5002:
		{
			bool flag4 = ZhanDianCon.inst != null;
			if (flag4)
			{
				ZhanDianCon.inst.GameStartInfo(szNetMsg);
			}
			break;
		}
		case 5003:
		{
			bool flag5 = ZhanDianCon.inst != null;
			if (flag5)
			{
				ZhanDianCon.inst.GameOneOver(szNetMsg);
			}
			break;
		}
		case 5004:
		case 5005:
		case 5007:
		case 5009:
		case 5010:
		case 5011:
		case 5012:
		case 5015:
		case 5016:
		case 5023:
		case 5030:
		case 5031:
		case 5032:
		case 5033:
		case 5034:
		case 5036:
			break;
		case 5006:
		{
			bool flag6 = ZhanDianCon.inst != null;
			if (flag6)
			{
				ZhanDianCon.inst.GameOver(szNetMsg);
			}
			else
			{
				fUIManager.GetInstance().GotoHall(0);
			}
			break;
		}
		case 5008:
		{
			bool flag7 = ZhanDianCon.inst != null;
			if (flag7)
			{
				ZhanDianCon.inst.UpdataScrollbar(szNetMsg);
			}
			break;
		}
		case 5013:
		{
			bool flag8 = ZhanDianCon.inst != null;
			if (flag8)
			{
				ZhanDianCon.inst.UpdateCarDis(szNetMsg);
			}
			break;
		}
		case 5014:
		{
			bool flag9 = ZhanDianCon.inst != null;
			if (flag9)
			{
				ZhanDianCon.inst.UpdateUserNum(szNetMsg);
			}
			break;
		}
		case 5017:
		{
			bool flag10 = ZhanDianCon.inst != null;
			if (flag10)
			{
				ZhanDianCon.inst.UpdateCarstate(szNetMsg);
			}
			break;
		}
		case 5018:
		{
			bool flag11 = ZhanDianCon.inst != null;
			if (flag11)
			{
				ZhanDianCon.inst.RoundEnd(szNetMsg);
			}
			break;
		}
		case 5019:
		{
			bool flag12 = ZhanDianCon.inst != null;
			if (flag12)
			{
				ZhanDianCon.inst.UpDateQdState(szNetMsg);
			}
			break;
		}
		case 5020:
		{
			bool flag13 = ZhanDianCon.inst != null;
			if (flag13)
			{
				ZhanDianCon.inst.RoundStart(szNetMsg);
			}
			break;
		}
		case 5021:
		{
			bool flag14 = ZhanDianCon.inst != null;
			if (flag14)
			{
				ZhanDianCon.inst.AddTimeUICon((int)szNetMsg[0], (int)szNetMsg[1]);
			}
			break;
		}
		case 5022:
		{
			bool flag15 = ZhanDianCon.inst != null;
			if (flag15)
			{
				ZhanDianCon.inst.TongBuScr(szNetMsg);
			}
			break;
		}
		case 5024:
		{
			bool flag16 = ZhanDianCon.inst != null;
			if (flag16)
			{
				ZhanDianCon.inst.OnSetIsPlaySound(szNetMsg);
			}
			break;
		}
		case 5025:
			this.szTabWinInfo = szNetMsg;
			fUIManager.GetInstance().OpenGameTabWin();
			break;
		case 5026:
			this.OnStartResultGame(szNetMsg);
			break;
		case 5027:
		{
			bool flag17 = ZhanDianCon.inst != null;
			if (flag17)
			{
				ZhanDianCon.inst.OnBigScrollPoint(szNetMsg);
			}
			break;
		}
		case 5028:
		{
			bool flag18 = ZhanDianCon.inst != null;
			if (flag18)
			{
				bool flag19 = (int)szNetMsg[2] == 1;
				if (flag19)
				{
					ZhanDianCon.inst.ReadyTimeCall((int)szNetMsg[1]);
				}
				bool flag20 = (int)szNetMsg[2] == 2;
				if (flag20)
				{
					ZhanDianCon.inst.LockTime((int)szNetMsg[0]);
				}
				bool flag21 = (int)szNetMsg[2] == 3;
				if (flag21)
				{
					ZhanDianCon.inst.SetallTime((int)szNetMsg[1]);
				}
			}
			break;
		}
		case 5029:
		{
			bool flag22 = (int)szNetMsg[0] == 1;
			if (flag22)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40028, new object[0]), "ffdd00", "system");
			}
			fUIManager.GetInstance().GotoHall(0);
			break;
		}
		case 5035:
			fUIManager.GetInstance().OnPushMsg(new ArrayList
			{
				5,
				Language.GetLang(8028, new object[0]),
				new ArrayList()
			}, 5);
			break;
		case 5037:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(8023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			fUIManager.GetInstance().GotoHall(0);
			break;
		case 5038:
			PlayerDataManager.getInstance().playercom.m_nShili = (int)szNetMsg[0];
			fGameManager.Heroplayer.nForce = (int)szNetMsg[0];
			break;
		default:
			if (nNetid == 80005)
			{
				fUIManager.GetInstance().CloseAll();
				roleuiManager.GetInstance().OpenSelectHeroUI(1);
			}
			break;
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x000612B4 File Offset: 0x0005F4B4
	private void loaduiok(string sUI, GameObject ob, object szParam)
	{
		bool flag = fGameManager.m_nUserType == 1;
		if (flag)
		{
			ZhanDianCon.inst.InitData(this.StartInfo);
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x000612E4 File Offset: 0x0005F4E4
	public void OnStartResultGame(ArrayList szNetMsg)
	{
		this.m_szFbResultData = szNetMsg;
		bool flag = roleuiManager.instance != null && gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.OnLoadResultSence();
		}
		SkyLadderManager.GetInstance().OnSkyLadderGameOver();
	}

	// Token: 0x040008AC RID: 2220
	private static FBManager instance;

	// Token: 0x040008AD RID: 2221
	private ArrayList StartInfo = new ArrayList();

	// Token: 0x040008AE RID: 2222
	private ArrayList szFbResultData = new ArrayList();

	// Token: 0x040008AF RID: 2223
	private ArrayList _szTabWinInfo = new ArrayList();
}
