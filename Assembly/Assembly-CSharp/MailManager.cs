using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class MailManager
{
	// Token: 0x060018FF RID: 6399 RVA: 0x000EA5A8 File Offset: 0x000E87A8
	public static MailManager GetInstance()
	{
		bool flag = MailManager.mInst == null;
		if (flag)
		{
			MailManager.mInst = new MailManager();
		}
		return MailManager.mInst;
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x000EA5D8 File Offset: 0x000E87D8
	public void MailServerMsgToClient(int nNetId, ArrayList szMsg)
	{
		switch (nNetId)
		{
		case 3401:
			this.GetMailDataList(szMsg);
			break;
		case 3403:
			this.OnReadMail(szMsg);
			break;
		case 3404:
			this.OnAddMail(szMsg);
			break;
		case 3405:
			this.OnDelMailBack(szMsg);
			break;
		case 3406:
			this.GetMailDrawBack(szMsg);
			break;
		case 3407:
			this.GetUnReadMail(szMsg);
			break;
		case 3408:
			this.GetUserMailStateList((ArrayList)szMsg[0], (ArrayList)szMsg[1]);
			break;
		case 3409:
			this.OnDelAllMailBack(szMsg);
			break;
		case 3410:
			this.GetAllMailItem(szMsg);
			break;
		}
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x000EA694 File Offset: 0x000E8894
	private void GetUnReadMail(ArrayList szMsg)
	{
		MailManager.m_nUnReadNum = (int)szMsg[1];
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			Debug.Log("------------------------------------GetUnReadMail===" + MailManager.m_nUnReadNum.ToString());
			bool flag2 = MailManager.m_nUnReadNum > 0;
			if (flag2)
			{
				MainUICon.inst.OnMailAin(true);
			}
			else
			{
				MainUICon.inst.OnMailAin(false);
			}
		}
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x000EA708 File Offset: 0x000E8908
	private void GetMailDataList(ArrayList szMsg)
	{
		ArrayList szMsg2 = (ArrayList)szMsg[0];
		ArrayList arrayList = (ArrayList)szMsg[1];
		int count = arrayList.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList szInfo = (ArrayList)arrayList[i];
			MailInfo mailInfo = new MailInfo();
			mailInfo.OnInit(szInfo);
			bool flag = !this.AllMailData.ContainsKey(mailInfo.m_nMailId);
			if (flag)
			{
				this.AllMailData.Add(mailInfo.m_nMailId, mailInfo);
			}
		}
		bool flag2 = MailUICon.inst != null;
		if (flag2)
		{
			MailUICon.inst.OnShowMailList(szMsg2);
		}
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x000EA7BC File Offset: 0x000E89BC
	private void OnAddMail(ArrayList szMsg)
	{
		bool flag = szMsg.Count >= 1;
		if (flag)
		{
			MailInfo mailInfo = new MailInfo();
			mailInfo.OnInit(szMsg);
			bool flag2 = this.AllMailData.ContainsKey(mailInfo.m_nMailId);
			if (flag2)
			{
				this.AllMailData[mailInfo.m_nMailId] = mailInfo;
			}
			else
			{
				this.AllMailData.Add(mailInfo.m_nMailId, mailInfo);
			}
			MailStateInfo mailStateInfo = new MailStateInfo();
			mailStateInfo.OnInit(new ArrayList
			{
				mailInfo.m_nMailId,
				0,
				0
			});
			mailStateInfo.OnSetIsItem(mailInfo.m_nIsItem);
			bool flag3 = this.AllMailStateList.ContainsKey(mailInfo.m_nMailId);
			if (flag3)
			{
				this.AllMailStateList[mailInfo.m_nMailId] = mailStateInfo;
			}
			else
			{
				this.AllMailStateList.Add(mailInfo.m_nMailId, mailStateInfo);
			}
			this.AllMailIdList.Insert(0, mailInfo.m_nMailId);
			MailManager.m_nUnGetNum++;
			this.OnUpdateGetAllItemBtnState(MailManager.m_nUnGetNum);
		}
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x000EA8E8 File Offset: 0x000E8AE8
	private void OnReadMail(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int num2 = (int)szMsg[1];
		bool flag = num >= 1 && this.AllMailStateList.ContainsKey(num2);
		if (flag)
		{
			this.AllMailStateList[num2].m_nIsRead = 1;
			bool flag2 = MailUICon.inst != null;
			if (flag2)
			{
				MailUICon.inst.OnUpdateMailInfo(num2, 1);
			}
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x000EA95C File Offset: 0x000E8B5C
	private void GetMailDrawBack(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		int num2 = (int)szMsg[1];
		bool flag = num < 1;
		if (flag)
		{
			bool flag2 = num == -1 || num == -3;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38500, new object[0]), "ffdd00", "system");
			}
			bool flag3 = num == -2;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38501, new object[0]), "ffdd00", "system");
			}
			bool flag4 = num == -4;
			if (flag4)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38502, new object[0]), "ffdd00", "system");
			}
		}
		else
		{
			bool flag5 = this.AllMailStateList.ContainsKey(num2);
			if (flag5)
			{
				this.AllMailStateList[num2].m_nIsGet = 1;
				MailManager.m_nUnGetNum--;
				this.OnUpdateGetAllItemBtnState(MailManager.m_nUnGetNum);
				bool flag6 = MailUICon.inst != null;
				if (flag6)
				{
					MailUICon.inst.OnUpdateMailInfo(num2, 2);
				}
			}
		}
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x000EAA8C File Offset: 0x000E8C8C
	private void OnDelMailBack(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		bool flag = num < 1;
		if (flag)
		{
			bool flag2 = num == -1 || num == -2;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38500, new object[0]), "ffdd00", "system");
			}
			bool flag3 = num == -3;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38503, new object[0]), "ffdd00", "system");
			}
		}
		else
		{
			int nMailId = (int)szMsg[1];
			this.OnDelMailSetData(nMailId);
			bool flag4 = MailUICon.inst != null;
			if (flag4)
			{
				MailUICon.inst.OnSetMailCount();
			}
		}
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x000EAB54 File Offset: 0x000E8D54
	private void OnDelAllMailBack(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		bool flag = num < 1;
		if (flag)
		{
			bool flag2 = num == -1;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38500, new object[0]), "ffdd00", "system");
			}
			bool flag3 = num == -2;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38504, new object[0]), "ffdd00", "system");
			}
			bool flag4 = num == -3;
			if (flag4)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38503, new object[0]), "ffdd00", "system");
			}
		}
		else
		{
			ArrayList arrayList = (ArrayList)szMsg[1];
			int count = arrayList.Count;
			for (int i = 0; i < count; i++)
			{
				int num2 = (int)arrayList[i];
				bool flag5 = num2 == 0;
				if (flag5)
				{
					break;
				}
				this.OnDelMailSetData(num2);
			}
			bool flag6 = MailUICon.inst != null;
			if (flag6)
			{
				MailUICon.inst.OnSetMailCount();
			}
		}
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x000EAC84 File Offset: 0x000E8E84
	private void OnDelMailSetData(int nMailId)
	{
		bool flag = this.AllMailStateList.ContainsKey(nMailId);
		if (flag)
		{
			this.AllMailStateList.Remove(nMailId);
		}
		bool flag2 = this.AllMailData.ContainsKey(nMailId);
		if (flag2)
		{
			this.AllMailData.Remove(nMailId);
		}
		this.AllMailIdList.Remove(nMailId);
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x000EACE0 File Offset: 0x000E8EE0
	private void GetAllMailItem(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		bool flag = num < 1;
		if (flag)
		{
			bool flag2 = num == -1;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38500, new object[0]), "ffdd00", "system");
			}
			bool flag3 = num == -2 || num == -3;
			if (flag3)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38505, new object[0]), "ffdd00", "system");
			}
		}
		else
		{
			ArrayList arrayList = (ArrayList)szMsg[1];
			int count = arrayList.Count;
			for (int i = 0; i < count; i++)
			{
				int num2 = (int)arrayList[i];
				bool flag4 = this.AllMailStateList.ContainsKey(num2);
				if (flag4)
				{
					this.AllMailStateList[num2].m_nIsGet = 1;
					this.AllMailStateList[num2].m_nIsRead = 1;
					bool flag5 = MailUICon.inst != null;
					if (flag5)
					{
						MailUICon.inst.OnUpdateMailInfo(num2, 2);
					}
				}
			}
			MailManager.m_nUnGetNum = 0;
			this.OnUpdateGetAllItemBtnState(MailManager.m_nUnGetNum);
		}
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x000EAE20 File Offset: 0x000E9020
	private void GetUserMailStateList(ArrayList szMsg, ArrayList szItemState)
	{
		int count = szMsg.Count;
		MailManager.m_nUnGetNum = 0;
		int num = 0;
		while (num < count && (int)szMsg[num] != 0)
		{
			MailStateInfo mailStateInfo = new MailStateInfo();
			mailStateInfo.OnInit(new ArrayList
			{
				(int)szMsg[num],
				(int)szMsg[num + 1],
				(int)szMsg[num + 2]
			});
			bool flag = szItemState.IndexOf(mailStateInfo.m_nMailId) != -1;
			if (flag)
			{
				mailStateInfo.OnSetIsItem(1);
			}
			bool flag2 = mailStateInfo.m_nIsGet == 0 && mailStateInfo.m_nIsItem == 1;
			if (flag2)
			{
				MailManager.m_nUnGetNum++;
			}
			bool flag3 = !this.AllMailStateList.ContainsKey(mailStateInfo.m_nMailId);
			if (flag3)
			{
				this.AllMailStateList.Add(mailStateInfo.m_nMailId, mailStateInfo);
			}
			num += 3;
		}
		this.OnUpdateGetAllItemBtnState(MailManager.m_nUnGetNum);
		this.AllMailStateList = this.DictonarySort(this.AllMailStateList);
		bool flag4 = MailUICon.inst != null;
		if (flag4)
		{
			MailUICon.inst.OnSetMailCount();
		}
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x000EAF80 File Offset: 0x000E9180
	public void OnUpdateGetAllItemBtnState(int nCount)
	{
		bool flag = MailUICon.inst != null;
		if (flag)
		{
			MailUICon.inst.OnUpdateGetAllItemBtnState(nCount);
		}
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x000EAFAC File Offset: 0x000E91AC
	public void OnRestList()
	{
		bool flag = MailManager.m_nUnReadNum > 0;
		if (flag)
		{
			MailManager.OnSendMsgToMailServer(3408, new ArrayList
			{
				0,
				0
			});
			MailManager.m_nUnReadNum = 0;
		}
		else
		{
			this.AllMailStateList = this.DictonarySort(this.AllMailStateList);
			bool flag2 = MailUICon.inst != null;
			if (flag2)
			{
				MailUICon.inst.OnSetMailCount();
			}
		}
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x000EB02C File Offset: 0x000E922C
	public Dictionary<int, MailStateInfo> DictonarySort(Dictionary<int, MailStateInfo> dic)
	{
		int num = 0;
		this.AllMailIdList.Clear();
		Dictionary<int, MailStateInfo> dictionary = new Dictionary<int, MailStateInfo>();
		IOrderedEnumerable<KeyValuePair<int, MailStateInfo>> orderedEnumerable = from objDic in dic
		orderby objDic.Value.m_nIsRead
		select objDic;
		foreach (KeyValuePair<int, MailStateInfo> keyValuePair in orderedEnumerable)
		{
			bool flag = num >= this.m_nMaxMailNum;
			if (flag)
			{
				break;
			}
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			num++;
			this.AllMailIdList.Add(keyValuePair.Key);
		}
		return dictionary;
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x000EB0F8 File Offset: 0x000E92F8
	public static void OnSendMsgToMailServer(int nNetID, ArrayList szNetMsg)
	{
		fGameManager.SendMsgToHallServer(3400, new ArrayList
		{
			nNetID,
			szNetMsg
		});
	}

	// Token: 0x040013C4 RID: 5060
	public static MailManager mInst;

	// Token: 0x040013C5 RID: 5061
	public int m_nMaxMailNum = 50;

	// Token: 0x040013C6 RID: 5062
	public static int m_nUnReadNum;

	// Token: 0x040013C7 RID: 5063
	public static bool m_bLoginIn = true;

	// Token: 0x040013C8 RID: 5064
	public Dictionary<int, MailInfo> AllMailData = new Dictionary<int, MailInfo>();

	// Token: 0x040013C9 RID: 5065
	public Dictionary<int, MailStateInfo> AllMailStateList = new Dictionary<int, MailStateInfo>();

	// Token: 0x040013CA RID: 5066
	public List<int> AllMailIdList = new List<int>();

	// Token: 0x040013CB RID: 5067
	public static int m_nUnGetNum;
}
