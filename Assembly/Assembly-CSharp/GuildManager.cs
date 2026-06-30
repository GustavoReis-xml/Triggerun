using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000160 RID: 352
public class GuildManager
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x00093ADC File Offset: 0x00091CDC
	public static GuildManager GetInstance()
	{
		bool flag = GuildManager.mInst == null;
		if (flag)
		{
			GuildManager.mInst = new GuildManager();
		}
		return GuildManager.mInst;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x00093B0C File Offset: 0x00091D0C
	public void GuildServerMsgToClient(int nNetId, ArrayList szMsg)
	{
		switch (nNetId)
		{
		case 91001:
			this.OnCreateGuild(szMsg);
			break;
		case 91002:
			this.OnDissolveGuild(szMsg);
			break;
		case 91003:
			this.OnAddGuild(szMsg);
			break;
		case 91004:
			this.OnExitGuild(szMsg);
			break;
		case 91005:
			this.OnKickUser(szMsg);
			break;
		case 91006:
			this.OnUpGuildLv(szMsg);
			break;
		case 91007:
			this.OnDemiseGuild(szMsg);
			break;
		case 91008:
			this.OnSetRank((int)szMsg[0], (ArrayList)szMsg[1]);
			break;
		case 91012:
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			break;
		case 91013:
			this.OnGuildList(szMsg);
			break;
		case 91015:
			this.OnAcceptGuildRequest(szMsg);
			break;
		case 91016:
			this.OnRefuseGuildRequest(szMsg);
			break;
		case 91017:
			this.OnGetGuildRequestList(szMsg);
			break;
		case 91018:
			this.OnGuildInfo(szMsg);
			break;
		case 91019:
			this.OnSaveNotice(szMsg);
			break;
		case 91020:
			this.OnSetUserGuildInfo(szMsg);
			break;
		case 91022:
			this.OnSetGuildAutoJoin(szMsg);
			break;
		case 91023:
			this.OnInvite(szMsg);
			break;
		case 91025:
			this.OnGuildAddActive(szMsg);
			break;
		case 91027:
			this.OnUpdateGuildRequstNum((int)szMsg[0]);
			break;
		case 91031:
			this.OnFindGuildByName(szMsg);
			break;
		case 91033:
			this.OnChangeGuildName((int)szMsg[0], (ArrayList)szMsg[1]);
			break;
		case 91034:
			this.OnGuildTanhe((int)szMsg[0], (ArrayList)szMsg[1]);
			break;
		case 91035:
			this.OnCheckTanheOk((int)szMsg[0], (ArrayList)szMsg[1]);
			break;
		case 91036:
			this.SetGuildFuliData(szMsg);
			break;
		}
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00093DA4 File Offset: 0x00091FA4
	private void OnGuildTanhe(int nCid, ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		ArrayList arrayList = (ArrayList)szMsg[1];
		bool flag = num == 1;
		if (flag)
		{
			this.m_sTanheUser = Fun.DecodingUTF8(arrayList[0].ToString());
			this.m_sTanheTime = arrayList[1].ToString();
			GameRoot.inst.CheckGuildTanhe();
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.CheckTanheBtn();
			}
			bool flag3 = nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38076, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00093E8C File Offset: 0x0009208C
	private void OnCheckTanheOk(int nGuildID, ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		string sName = Fun.DecodingUTF8(szMsg[1].ToString());
		this.m_sTanheUser = string.Empty;
		this.m_sTanheTime = string.Empty;
		bool flag = GuildInfoCon.inst != null;
		if (flag)
		{
			GuildInfoCon.inst.OnCheckTanheOk(sName);
		}
		bool flag2 = num == PlayerDataManager.getInstance().playerCommon.m_nCid;
		if (flag2)
		{
			this.m_nZhiWei = 2;
			this.OnSendMsgToGuildServer(91000, new ArrayList
			{
				91018,
				new ArrayList
				{
					1,
					0,
					0
				}
			});
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00093F60 File Offset: 0x00092160
	private void OnChangeGuildName(int nRet, ArrayList szData)
	{
		bool flag = nRet == 1;
		if (flag)
		{
			fUIManager.GetInstance().SetWinActive("CreateRoleUI", false);
			this.m_sGuildName = Fun.DecodingUTF8(szData[1].ToString());
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.OnChangeGuildName(this.m_sGuildName);
			}
		}
		else
		{
			bool flag3 = CreateRoleUICon.inst != null && CreateRoleUICon.inst.visible;
			if (flag3)
			{
				CreateRoleUICon.inst.ShowTipMsg(new ArrayList
				{
					91033,
					nRet
				});
			}
		}
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00094014 File Offset: 0x00092214
	private void OnFindGuildByName(ArrayList szData)
	{
		int num = (int)szData[0];
		bool flag = num < 1 && num == -1;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0009408C File Offset: 0x0009228C
	public void OnUpdateGuildRequstNum(int nNum)
	{
		this.m_nGuildRequstNum = nNum;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnShowGuildNum();
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000940C0 File Offset: 0x000922C0
	private void OnSaveNotice(ArrayList szData)
	{
		int num = (int)szData[0];
		ArrayList arrayList = (ArrayList)szData[1];
		int num2 = num;
		int num3 = num2;
		if (num3 != -3)
		{
			if (num3 != -2)
			{
				bool flag = GuildUICon.inst != null;
				if (flag)
				{
					GuildUICon.inst.OnSaveNotice(Fun.DecodingUTF8(arrayList[1].ToString()));
				}
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x000941C4 File Offset: 0x000923C4
	private void OnSetUserGuildInfo(ArrayList szGuildInfo)
	{
		bool flag = szGuildInfo.Count == 0;
		if (!flag)
		{
			this.m_nGuildId = (int)szGuildInfo[0];
			this.m_sGuildName = Fun.DecodingUTF8(szGuildInfo[1].ToString());
			this.m_nZhiWei = (int)szGuildInfo[2];
			this.m_nGuildLv = (int)szGuildInfo[3];
			this.m_sTanheUser = Fun.DecodingUTF8(szGuildInfo[5].ToString());
			this.m_sTanheTime = szGuildInfo[6].ToString();
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.OnisShowRequestTogg();
			}
			bool flag3 = OtherGuildListCon.inst != null;
			if (flag3)
			{
				OtherGuildListCon.inst.IsShowCreateBtn();
			}
			bool flag4 = this.m_nGuildId != 0;
			if (flag4)
			{
				bool flag5 = GuildInfoCon.inst != null;
				if (flag5)
				{
					GuildInfoCon.inst.CheckTanheBtn();
				}
				GameRoot.inst.CheckGuildTanhe();
			}
		}
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000942CC File Offset: 0x000924CC
	private void OnAddGuild(ArrayList szData)
	{
		int num = (int)szData[0];
		ArrayList arrayList = (ArrayList)szData[1];
		int nGuildId = (int)arrayList[0];
		bool flag = num < 1;
		if (flag)
		{
			int num2 = num;
			int num3 = num2;
			if (num3 != -20)
			{
				switch (num3)
				{
				case -8:
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38012, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				case -7:
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38010, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				case -4:
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38011, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				case -3:
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38006, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				case -2:
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				}
				bool flag2 = GuildUICon.inst != null;
				if (flag2)
				{
					GuildUICon.inst.OnUpdateOneGuild(nGuildId, new ArrayList
					{
						1,
						0
					});
				}
			}
			else
			{
				bool flag3 = GuildInfoCon.inst != null;
				if (flag3)
				{
					GuildInfoCon.inst.ShowRequestImg(true);
				}
			}
		}
		else
		{
			ArrayList szData2 = (ArrayList)arrayList[3];
			this.OnGuildInfo(szData2);
			bool flag4 = GuildUICon.inst != null;
			if (flag4)
			{
				GuildUICon.inst.OnClearUserRequest();
			}
		}
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0009456C File Offset: 0x0009276C
	private void OnCreateGuild(ArrayList szData)
	{
		int num = (int)szData[0];
		bool flag = num != 1;
		if (flag)
		{
			Debug.Log("创建失败");
		}
		else
		{
			ArrayList szData2 = (ArrayList)szData[1];
			this.OnGuildInfo(szData2);
		}
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x000945B8 File Offset: 0x000927B8
	private void OnGuildInfo(ArrayList szData)
	{
		bool flag = szData.Count != 0 && GuildUICon.inst != null;
		if (flag)
		{
			GuildUICon.inst.OnGuildInfo(szData);
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x000945F0 File Offset: 0x000927F0
	private void OnGuildList(ArrayList szData)
	{
		string sFindGuildName = string.Empty;
		int num = (int)szData[0];
		bool flag = num >= 1;
		if (flag)
		{
			ArrayList szData2 = (ArrayList)szData[1];
			ArrayList szRequest = (ArrayList)szData[2];
			ArrayList szCount = (ArrayList)szData[3];
			sFindGuildName = Fun.DecodingUTF8(szData[4].ToString());
			bool flag2 = GuildUICon.inst != null;
			if (flag2)
			{
				GuildUICon.inst.OnOtherGuildList(szData2, szRequest, szCount, sFindGuildName);
			}
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00094680 File Offset: 0x00092880
	private void OnExitGuild(ArrayList szData)
	{
		int num = (int)szData[0];
		ArrayList arrayList = (ArrayList)szData[1];
		int num2 = (int)arrayList[1];
		bool flag = num < 1;
		if (flag)
		{
			switch (num)
			{
			case -6:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(46143, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38014, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.onExit), new ArrayList
				{
					num2,
					1
				}, null, null);
				break;
			case -4:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38013, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			}
		}
		else
		{
			bool flag2 = GuildUICon.inst != null;
			if (flag2)
			{
				GuildUICon.inst.OnChangeToggle();
			}
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0009481C File Offset: 0x00092A1C
	public void onExit(object obj)
	{
		ArrayList arrayList = (ArrayList)obj;
		int num = (int)arrayList[0];
		int num2 = (int)arrayList[1];
		this.OnSendMsgToGuildServer(91000, new ArrayList
		{
			91004,
			new ArrayList
			{
				num,
				num2
			}
		});
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00094894 File Offset: 0x00092A94
	private void OnGetGuildRequestList(ArrayList szData)
	{
		bool flag = GuildUICon.inst != null;
		if (flag)
		{
			GuildUICon.inst.OnGetGuildRequestList(szData);
		}
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x000948C0 File Offset: 0x00092AC0
	private void OnRefuseGuildRequest(ArrayList szData)
	{
		int num = (int)szData[0];
		int num2 = (int)szData[1];
		ArrayList arrayList = (ArrayList)szData[2];
		int nDataId = (int)szData[3];
		string text = Fun.DecodingUTF8(arrayList[1].ToString());
		int nGuildId = (int)szData[4];
		bool flag = num == 0;
		if (flag)
		{
			bool flag2 = num2 < 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = GuildInfoCon.inst != null;
				if (flag3)
				{
					GuildInfoCon.inst.OnDeleteRequest(nDataId);
				}
			}
		}
		else
		{
			bool flag4 = num2 >= 0;
			if (flag4)
			{
				bool flag5 = GuildUICon.inst != null;
				if (flag5)
				{
					GuildUICon.inst.OnUpdateOneGuild(nGuildId, new ArrayList
					{
						2,
						0
					});
				}
				bool flag6 = num2 != 0;
				if (flag6)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38015, new object[]
					{
						text
					}), "ffdd00", "system");
				}
			}
		}
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00094A30 File Offset: 0x00092C30
	private void OnAcceptGuildRequest(ArrayList szData)
	{
		int num = (int)szData[0];
		int num2 = (int)szData[1];
		ArrayList arrayList = (ArrayList)szData[2];
		int nDataId = (int)szData[3];
		int nGuildId = (int)szData[4];
		bool flag = num == 0;
		if (flag)
		{
			bool flag2 = num2 < 0;
			if (flag2)
			{
				int num3 = num2;
				int num4 = num3;
				if (num4 != -12)
				{
					if (num4 == -11)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					}
				}
				else
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38016, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
			}
			else
			{
				bool flag3 = GuildInfoCon.inst != null;
				if (flag3)
				{
					GuildInfoCon.inst.OnDeleteRequest(nDataId);
				}
			}
		}
		else
		{
			bool flag4 = num2 < 0;
			if (!flag4)
			{
				bool flag5 = num2 == 0;
				if (flag5)
				{
					bool flag6 = GuildUICon.inst != null;
					if (flag6)
					{
						GuildUICon.inst.OnUpdateOneGuild(nGuildId, new ArrayList
						{
							2,
							0
						});
					}
				}
				else
				{
					bool flag7 = GuildUICon.inst != null;
					if (flag7)
					{
						GuildUICon.inst.OnChangeToggle();
					}
					bool flag8 = GuildUICon.inst != null;
					if (flag8)
					{
						GuildUICon.inst.OnClearUserRequest();
					}
					bool flag9 = GuildUICon.inst != null;
					if (flag9)
					{
						GuildUICon.inst.OnUpdateOneGuild(nGuildId, new ArrayList
						{
							3,
							0
						});
					}
				}
			}
		}
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00094C50 File Offset: 0x00092E50
	private void OnKickUser(ArrayList szData)
	{
		int num = (int)szData[0];
		int num2 = (int)szData[1];
		ArrayList szData2 = (ArrayList)szData[2];
		bool flag = num == 1;
		if (flag)
		{
			switch (num2)
			{
			case -6:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(46144, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38066, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -4:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38024, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -3:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -2:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			default:
			{
				bool flag2 = GuildUICon.inst != null;
				if (flag2)
				{
					GuildUICon.inst.UpdateMemberList(szData2);
				}
				break;
			}
			}
		}
		else
		{
			bool flag3 = GuildUICon.inst != null;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38025, new object[0]), "ffdd00", "system");
				GuildUICon.inst.OnChangeToggle();
			}
		}
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00094EA3 File Offset: 0x000930A3
	private void OnDissolveGuild(ArrayList szData)
	{
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x00094EA8 File Offset: 0x000930A8
	private void OnDemiseGuild(ArrayList szData)
	{
		int num = (int)szData[0];
		int num2 = (int)szData[1];
		ArrayList arrayList = (ArrayList)szData[2];
		arrayList = (ArrayList)arrayList[2];
		bool flag = num == 1;
		if (flag)
		{
			switch (num2)
			{
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38066, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			case -3:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			case -2:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				return;
			}
			this.m_nZhiWei = 0;
		}
		else
		{
			this.m_nZhiWei = 2;
		}
		this.OnGuildInfo(arrayList);
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0009501C File Offset: 0x0009321C
	public void OnDemiseSendMsgGuild(string szName)
	{
		bool flag = this.m_nZhiWei != 2;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38026, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38027, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnZhuanRang), szName, null, null);
		}
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000950E8 File Offset: 0x000932E8
	private void OnZhuanRang(object obj)
	{
		string s = string.Empty;
		s = obj.ToString();
		this.OnSendMsgToGuildServer(91000, new ArrayList
		{
			91007,
			new ArrayList
			{
				Fun.EncodingUTF8(s),
				0
			}
		});
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0009514C File Offset: 0x0009334C
	public void OnInviteSendMsgGuild(string szName)
	{
		bool flag = this.m_nGuildId == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38028, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = this.AllSendYaoQing.ContainsKey(szName);
			if (flag2)
			{
				OnePushMsgInfo onePushMsgInfo = this.AllSendYaoQing[szName];
				bool flag3 = ScnMsgManager.GetInstance().GetPushMsgTimeLong(onePushMsgInfo.dCreateTime, 20) > 0;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38029, new object[0]), "ffdd00", "system");
					return;
				}
			}
			this.OnSendMsgToGuildServer(91000, new ArrayList
			{
				91023,
				new ArrayList
				{
					Fun.EncodingUTF8(szName),
					0
				}
			});
		}
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00095260 File Offset: 0x00093460
	public void OnSetZhiWeiSendMsgGuild(string szName, int nZhiWei)
	{
		bool flag = this.m_nGuildId == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38028, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.OnSendMsgToGuildServer(91000, new ArrayList
			{
				91008,
				new ArrayList
				{
					Fun.EncodingUTF8(szName),
					nZhiWei
				}
			});
		}
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00095310 File Offset: 0x00093510
	private void OnSetGuildAutoJoin(ArrayList szData)
	{
		int nValue = (int)szData[1];
		int num = (int)szData[0];
		int num2 = num;
		if (num2 != -3)
		{
			if (num2 != -2)
			{
				bool flag = GuildInfoCon.inst != null;
				if (flag)
				{
					GuildInfoCon.inst.OnChangeIsAutoJoin(nValue);
				}
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00095400 File Offset: 0x00093600
	public void OnDeleteSenInvite(string sName)
	{
		bool flag = this.AllSendYaoQing.ContainsKey(sName);
		if (flag)
		{
			this.AllSendYaoQing.Remove(sName);
		}
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00095430 File Offset: 0x00093630
	private void OnInvite(ArrayList szData)
	{
		int num = (int)szData[0];
		string text = Fun.DecodingUTF8(szData[1].ToString());
		string text2 = Fun.DecodingUTF8(szData[2].ToString());
		int num2 = (int)szData[3];
		int num3 = (int)szData[4];
		string text3 = Fun.DecodingUTF8(szData[5].ToString());
		bool flag = num2 == 0;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nLevel >= 10 && GuildManager.GetInstance().m_nGuildId == 0;
			if (flag2)
			{
				fUIManager.GetInstance().OnPushMsg(new ArrayList
				{
					1,
					Language.GetLang(38036, new object[]
					{
						text2,
						text
					}),
					new ArrayList
					{
						num3,
						text2
					}
				}, 20);
			}
		}
		else
		{
			int num4 = num;
			int num5 = num4;
			if (num5 <= -12)
			{
				if (num5 == -100)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38053, new object[0]), "ffdd00", "system");
					return;
				}
				if (num5 == -12)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38037, new object[0]), "ffdd00", "system");
					return;
				}
			}
			else
			{
				if (num5 == -11)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38059, new object[]
					{
						text3
					}), "ffdd00", "system");
					return;
				}
				if (num5 == -6)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38035, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
					return;
				}
			}
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38038, new object[0]), "ffdd00", "system");
			OnePushMsgInfo value = new OnePushMsgInfo
			{
				m_nType = 1,
				m_sMsg = string.Empty,
				dCreateTime = Fun.GetServerDate().ToString()
			};
			bool flag3 = this.AllSendYaoQing.ContainsKey(text3);
			if (flag3)
			{
				this.AllSendYaoQing[text3] = value;
			}
			else
			{
				this.AllSendYaoQing.Add(text3, value);
			}
		}
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x000956E0 File Offset: 0x000938E0
	public void OnInviteOk(int nGuildId, string sNick, int nType)
	{
		bool flag = nType == 1 && GuildManager.GetInstance().m_nGuildId != 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38019, new object[0]), "ffdd00", "system");
		}
		else
		{
			this.OnSendMsgToGuildServer(91000, new ArrayList
			{
				91026,
				new ArrayList
				{
					nGuildId,
					Fun.EncodingUTF8(sNick),
					nType
				}
			});
		}
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00095783 File Offset: 0x00093983
	public void OnSendMsgToGuildServer(int nNetID, ArrayList szNetMsg)
	{
		fGameManager.SendMsgToHallServer(nNetID, szNetMsg);
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x00095790 File Offset: 0x00093990
	private void OnGuildAddActive(ArrayList szData)
	{
		int num = (int)szData[0];
		ArrayList arrayList = (ArrayList)szData[1];
		bool flag = num == -2;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.OnUpdateGuildBase((int)arrayList[1], (int)arrayList[3]);
			}
		}
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x00095844 File Offset: 0x00093A44
	private void OnSetRank(int nRet, ArrayList szGuild)
	{
		ArrayList szData = (ArrayList)szGuild[2];
		bool flag = nRet < 0;
		if (flag)
		{
			switch (nRet)
			{
			case -10:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38066, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -8:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38017, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38020, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -4:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38018, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -3:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -2:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			}
		}
		else
		{
			this.OnGuildInfo(szData);
		}
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x00095A7C File Offset: 0x00093C7C
	private void OnUpGuildLv(ArrayList szGuild)
	{
		int num = (int)szGuild[0];
		bool flag = num < 0;
		if (flag)
		{
			switch (num)
			{
			case -5:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38022, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -4:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38021, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -3:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			case -2:
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				break;
			}
		}
		else
		{
			bool flag2 = GuildInfoCon.inst != null;
			if (flag2)
			{
				GuildInfoCon.inst.OnUpdateGuildBase((int)szGuild[1], (int)szGuild[0]);
			}
		}
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00095C30 File Offset: 0x00093E30
	public void UpdatePageBtnState(int m_nMaxPage, int m_nCurPage, GameObject pBtn_Zuo, GameObject pBtn_You)
	{
		bool flag = m_nMaxPage <= 1;
		if (flag)
		{
			pBtn_Zuo.GetComponent<Button>().interactable = false;
			pBtn_You.GetComponent<Button>().interactable = false;
		}
		else
		{
			bool flag2 = m_nCurPage == 1;
			if (flag2)
			{
				pBtn_Zuo.GetComponent<Button>().interactable = false;
				pBtn_You.GetComponent<Button>().interactable = true;
			}
			else
			{
				bool flag3 = m_nCurPage == m_nMaxPage;
				if (flag3)
				{
					pBtn_Zuo.GetComponent<Button>().interactable = true;
					pBtn_You.GetComponent<Button>().interactable = false;
				}
				else
				{
					pBtn_Zuo.GetComponent<Button>().interactable = true;
					pBtn_You.GetComponent<Button>().interactable = true;
				}
			}
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x00095CD4 File Offset: 0x00093ED4
	public void SetGuildFuli(ArrayList szData)
	{
		bool flag = szData.Count != 0;
		if (flag)
		{
			this.m_szGuildFuli = (ArrayList)szData.Clone();
			this.m_nDayHuoyue = (int)this.m_szGuildFuli[0];
			ArrayList arrayList = (ArrayList)this.m_szGuildFuli[3];
			this.m_nWeekHuoyue = (int)arrayList[1];
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.OnShowGuildNum();
			}
			bool flag3 = GuildUICon.inst != null && GuildUICon.inst.visible;
			if (flag3)
			{
				GuildUICon.inst.ShowGuildFuli();
			}
		}
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x00095D88 File Offset: 0x00093F88
	public int GetGuildFuliState(int nidx, int nday)
	{
		int num = 0;
		bool flag = this.m_szGuildFuli.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			ArrayList arrayList = (ArrayList)this.m_szGuildFuli[nidx];
			bool flag2 = nidx == 2;
			if (flag2)
			{
				result = (int)arrayList[nday];
			}
			else
			{
				arrayList = (ArrayList)arrayList[2];
				result = (int)arrayList[nday];
			}
		}
		return result;
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00095DF8 File Offset: 0x00093FF8
	private void SetGuildFuliData(ArrayList szMsg)
	{
		bool flag = (int)szMsg[0] == this.m_nGuildId;
		if (flag)
		{
			this.m_nGuildLv = (int)szMsg[1];
			this.m_szGuildFuli = (ArrayList)szMsg[2];
			this.m_nDayHuoyue = (int)this.m_szGuildFuli[0];
			ArrayList arrayList = (ArrayList)this.m_szGuildFuli[3];
			this.m_nWeekHuoyue = (int)arrayList[1];
		}
		bool flag2 = MainUICon.inst != null;
		if (flag2)
		{
			MainUICon.inst.OnShowGuildNum();
		}
		bool flag3 = GuildUICon.inst != null && GuildUICon.inst.visible;
		if (flag3)
		{
			GuildUICon.inst.ShowGuildFuli();
		}
		bool flag4 = GuildFuliCon.inst;
		if (flag4)
		{
			GuildFuliCon.inst.InitData();
		}
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x00095EE4 File Offset: 0x000940E4
	public int GetGuildfuliReward()
	{
		int num = 0;
		bool flag = this.m_nGuildId == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = this.m_szGuildFuli.Count == 0;
			if (flag2)
			{
				result = num;
			}
			else
			{
				ArrayList arrayList = (ArrayList)this.m_szGuildFuli[2];
				int num2 = arrayList.IndexOf(1);
				bool flag3 = num2 != -1;
				if (flag3)
				{
					num = 1;
				}
				arrayList = (ArrayList)this.m_szGuildFuli[3];
				arrayList = (ArrayList)arrayList[2];
				num2 = arrayList.IndexOf(1);
				bool flag4 = num2 != -1;
				if (flag4)
				{
					num = 1;
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x04000CCA RID: 3274
	public static GuildManager mInst;

	// Token: 0x04000CCB RID: 3275
	public int m_nGuildId;

	// Token: 0x04000CCC RID: 3276
	public string m_sGuildName = string.Empty;

	// Token: 0x04000CCD RID: 3277
	public int m_nZhiWei;

	// Token: 0x04000CCE RID: 3278
	public int m_nGuildLv;

	// Token: 0x04000CCF RID: 3279
	public int m_nGuildRequstNum;

	// Token: 0x04000CD0 RID: 3280
	public static bool isChangeGong;

	// Token: 0x04000CD1 RID: 3281
	public string m_sTanheUser;

	// Token: 0x04000CD2 RID: 3282
	public string m_sTanheTime;

	// Token: 0x04000CD3 RID: 3283
	public int m_nTanheCost = 1000;

	// Token: 0x04000CD4 RID: 3284
	public int m_nDayHuoyue;

	// Token: 0x04000CD5 RID: 3285
	public int m_nWeekHuoyue;

	// Token: 0x04000CD6 RID: 3286
	public ArrayList m_szGuildFuli = new ArrayList();

	// Token: 0x04000CD7 RID: 3287
	public Dictionary<string, OnePushMsgInfo> AllSendYaoQing = new Dictionary<string, OnePushMsgInfo>();
}
