using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002DC RID: 732
public class RoomData
{
	// Token: 0x060021B2 RID: 8626 RVA: 0x0013944C File Offset: 0x0013764C
	public void InitRoomInfo(ArrayList szRoomInfo)
	{
		bool flag = szRoomInfo.Count != 0;
		if (flag)
		{
			szRoomInfo[1] = Fun.DecodingUTF8(szRoomInfo[1].ToString());
			szRoomInfo[2] = Fun.DecodingUTF8(szRoomInfo[2].ToString());
			this.m_szRoomInfo = szRoomInfo;
			this.m_nRoomID = (int)this.m_szRoomInfo[0];
			this.m_sCreator = (string)this.m_szRoomInfo[1];
			this.m_sRoomName = (string)this.m_szRoomInfo[2];
			int key = (int)this.m_szRoomInfo[4];
			bool flag2 = fGameManager.mloadConfig.FbData.ContainsKey(key);
			if (flag2)
			{
				this.m_pRoomFbData = fGameManager.mloadConfig.FbData[key];
			}
			this.m_nRoomNowNum = (int)this.m_szRoomInfo[11];
			this.m_nRoomMaxNum = (int)this.m_szRoomInfo[7];
			this.m_nRoomState = (int)this.m_szRoomInfo[13];
			this.m_sCreateTime = (string)this.m_szRoomInfo[16];
			this.m_nRoomUID = (int)this.m_szRoomInfo[17];
		}
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x001395A0 File Offset: 0x001377A0
	public void InitRoom(ArrayList szRoomData)
	{
		this.InitRoomInfo((ArrayList)(szRoomData[0] as ArrayList).Clone());
		ArrayList arrayList = (ArrayList)(szRoomData[1] as ArrayList).Clone();
		for (int i = 0; i < arrayList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(arrayList[i].ToString());
			if (flag)
			{
				arrayList[i] = Fun.DecodingUTF8(arrayList[i].ToString());
				ArrayList arrayList2 = (ArrayList)(arrayList[i + 1] as ArrayList).Clone();
				arrayList2[0] = Fun.DecodingUTF8(arrayList2[0].ToString());
				arrayList[i + 1] = arrayList2;
			}
		}
		this.m_szUserList = arrayList;
		this.m_nRoomNowNum = this.GetPlayerNum();
		this.CreateUserData();
		this.ReSortUserList();
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x00139690 File Offset: 0x00137890
	public int GetPlayerNum()
	{
		int num = 0;
		for (int i = 0; i < this.m_szUserList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(this.m_szUserList[i].ToString());
			if (flag)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060021B5 RID: 8629 RVA: 0x001396E8 File Offset: 0x001378E8
	public string GetModeStr()
	{
		return this.m_pRoomFbData.GetModeStr();
	}

	// Token: 0x060021B6 RID: 8630 RVA: 0x00139708 File Offset: 0x00137908
	public string GetStateStr()
	{
		string result = string.Empty;
		switch (this.m_nRoomState)
		{
		case 0:
			result = Language.GetLang(8006, new object[0]);
			break;
		case 1:
		case 2:
		case 3:
			result = Language.GetLang(8007, new object[0]);
			break;
		case 4:
			result = Language.GetLang(8008, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x00139780 File Offset: 0x00137980
	public ArrayList GetRoomInfo()
	{
		return this.m_szRoomInfo;
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x00139798 File Offset: 0x00137998
	public ArrayList GetUserList()
	{
		return this.m_szUserList;
	}

	// Token: 0x060021B9 RID: 8633 RVA: 0x001397B0 File Offset: 0x001379B0
	public ArrayList GetAllUserData()
	{
		return new ArrayList(this.m_szAllUserData.Keys);
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x001397D4 File Offset: 0x001379D4
	public UserData GetUserData(string sName)
	{
		bool flag = this.m_szAllUserData.ContainsKey(sName);
		UserData result;
		if (flag)
		{
			result = this.m_szAllUserData[sName];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x00139807 File Offset: 0x00137A07
	public void ClearRoomUserData()
	{
		this.m_szAllUserData.Clear();
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x00139818 File Offset: 0x00137A18
	public object GetRoomInfo(int nIndex)
	{
		bool flag = nIndex >= this.m_szRoomInfo.Count;
		object result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.m_szRoomInfo[nIndex];
		}
		return result;
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x00139850 File Offset: 0x00137A50
	public void ChangeGameInfo(int nIndex, object value)
	{
		bool flag = nIndex == 1 || nIndex == 2;
		if (flag)
		{
			this.m_szRoomInfo[nIndex] = Fun.DecodingUTF8(value.ToString());
		}
		else
		{
			this.m_szRoomInfo[nIndex] = value;
		}
		bool flag2 = nIndex == 4;
		if (flag2)
		{
			int key = (int)value;
			bool flag3 = fGameManager.mloadConfig.FbData.ContainsKey(key);
			if (flag3)
			{
				this.m_pRoomFbData = fGameManager.mloadConfig.FbData[key];
			}
		}
		bool flag4 = nIndex == 13;
		if (flag4)
		{
			this.m_nRoomState = (int)value;
		}
		bool flag5 = nIndex == 1;
		if (flag5)
		{
			this.m_sCreator = this.m_szRoomInfo[1].ToString();
		}
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x00139914 File Offset: 0x00137B14
	public void SetRoomUserList(ArrayList szList)
	{
		for (int i = 0; i < szList.Count; i += 2)
		{
			bool flag = !string.IsNullOrEmpty(szList[i].ToString());
			if (flag)
			{
				szList[i] = Fun.DecodingUTF8(szList[i].ToString());
				ArrayList arrayList = (ArrayList)szList[i + 1];
				arrayList[0] = Fun.DecodingUTF8(arrayList[0].ToString());
			}
		}
		this.m_szUserList = (ArrayList)szList.Clone();
		this.m_nRoomNowNum = this.GetPlayerNum();
		this.ClearRoomUserData();
		this.CreateUserData();
		this.ReSortUserList();
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x001399C8 File Offset: 0x00137BC8
	public void SetRoomUserList(string uname, ArrayList szUser)
	{
		int num = this.m_szUserList.IndexOf(uname);
		bool flag = num != -1;
		if (flag)
		{
			szUser[0] = Fun.DecodingUTF8(szUser[0].ToString());
			this.m_szUserList[num + 1] = szUser;
			this.OnUpUserDate(uname, szUser);
			this.ReSortUserList();
		}
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x00139A2C File Offset: 0x00137C2C
	public void SetRoomUserListByIdx(string uname, ArrayList szInfo)
	{
		int num = this.m_szUserList.IndexOf(uname);
		bool flag = num == -1;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)this.m_szUserList[num + 1];
			for (int i = 0; i < szInfo.Count; i += 2)
			{
				bool flag2 = (int)szInfo[i] == 0;
				if (flag2)
				{
					arrayList[0] = Fun.DecodingUTF8(szInfo[i + 1].ToString());
				}
				else
				{
					arrayList[(int)szInfo[i]] = szInfo[i + 1];
				}
			}
			this.m_szUserList[num + 1] = arrayList;
			this.OnUpUserDate(uname, arrayList);
			this.ReSortUserList();
		}
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x00139AF4 File Offset: 0x00137CF4
	private void CreateUserData()
	{
		int i = 0;
		while (i < this.m_szUserList.Count)
		{
			bool flag = (string)this.m_szUserList[i] != string.Empty;
			if (flag)
			{
				bool flag2 = this.m_szAllUserData.ContainsKey((string)this.m_szUserList[i]);
				if (flag2)
				{
					this.OnUpUserDate((string)this.m_szUserList[i], (ArrayList)this.m_szUserList[i + 1]);
				}
				else
				{
					UserData userData = new UserData();
					userData.InitUserData((ArrayList)this.m_szUserList[i + 1]);
					this.m_szAllUserData.Add((string)this.m_szUserList[i], userData);
				}
			}
			IL_BA:
			i += 2;
			continue;
			goto IL_BA;
		}
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x00139BD8 File Offset: 0x00137DD8
	public void OnUserLeave(string sNick)
	{
		sNick = Fun.DecodingUTF8(sNick);
		bool flag = this.m_szAllUserData.ContainsKey(sNick);
		if (flag)
		{
			this.m_szAllUserData.Remove(sNick);
		}
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x00139C10 File Offset: 0x00137E10
	private void OnUpUserDate(string sNick, ArrayList arr)
	{
		bool flag = this.m_szAllUserData.ContainsKey(sNick);
		if (flag)
		{
			UserData userData = this.m_szAllUserData[sNick];
			userData.InitUserData(arr);
		}
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x00139C48 File Offset: 0x00137E48
	private void ReSortUserList()
	{
		for (int i = 0; i < 10; i += 2)
		{
			string value = this.m_szUserList[i].ToString();
			bool flag = string.IsNullOrEmpty(value);
			if (!flag)
			{
				ArrayList arrayList = (ArrayList)this.m_szUserList[i + 1];
				int num = (int)arrayList[12];
				bool flag2 = num == 0;
				if (!flag2)
				{
					for (int j = i + 2; j < 10; j += 2)
					{
						value = (string)this.m_szUserList[j].ToString().Clone();
						bool flag3 = string.IsNullOrEmpty(value);
						if (!flag3)
						{
							arrayList = (ArrayList)(this.m_szUserList[j + 1] as ArrayList).Clone();
							bool flag4 = num == (int)arrayList[12] && i + 2 < 10;
							if (flag4)
							{
								this.m_szUserList[j] = (string)this.m_szUserList[i + 2].ToString().Clone();
								bool flag5 = this.m_szUserList[i + 3] is ArrayList;
								if (flag5)
								{
									this.m_szUserList[j + 1] = (ArrayList)(this.m_szUserList[i + 3] as ArrayList).Clone();
								}
								else
								{
									this.m_szUserList[j + 1] = string.Empty;
								}
								this.m_szUserList[i + 2] = value;
								this.m_szUserList[i + 3] = arrayList;
								break;
							}
						}
					}
				}
			}
		}
		for (int k = 10; k < 20; k += 2)
		{
			string value2 = this.m_szUserList[k].ToString();
			bool flag6 = string.IsNullOrEmpty(value2);
			if (!flag6)
			{
				ArrayList arrayList2 = (ArrayList)this.m_szUserList[k + 1];
				int num2 = (int)arrayList2[12];
				bool flag7 = num2 == 0;
				if (!flag7)
				{
					for (int l = k + 2; l < 20; l += 2)
					{
						value2 = (string)this.m_szUserList[l].ToString().Clone();
						bool flag8 = string.IsNullOrEmpty(value2);
						if (!flag8)
						{
							arrayList2 = (ArrayList)(this.m_szUserList[l + 1] as ArrayList).Clone();
							bool flag9 = num2 == (int)arrayList2[12] && k + 2 < 20;
							if (flag9)
							{
								this.m_szUserList[l] = (string)this.m_szUserList[k + 2].ToString().Clone();
								bool flag10 = this.m_szUserList[k + 3] is ArrayList;
								if (flag10)
								{
									this.m_szUserList[l + 1] = (ArrayList)(this.m_szUserList[k + 3] as ArrayList).Clone();
								}
								else
								{
									this.m_szUserList[l + 1] = string.Empty;
								}
								this.m_szUserList[k + 2] = value2;
								this.m_szUserList[k + 3] = arrayList2;
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04001F3A RID: 7994
	public const int ROOM_INDEX_ID = 0;

	// Token: 0x04001F3B RID: 7995
	public const int ROOM_INDEX_NICK = 1;

	// Token: 0x04001F3C RID: 7996
	public const int ROOM_INDEX_NAME = 2;

	// Token: 0x04001F3D RID: 7997
	public const int ROOM_INDEX_PWD = 3;

	// Token: 0x04001F3E RID: 7998
	public const int ROOM_INDEX_SCNID = 4;

	// Token: 0x04001F3F RID: 7999
	public const int ROOM_INDEX_MODE = 5;

	// Token: 0x04001F40 RID: 8000
	public const int ROOM_INDEX_DIFF = 6;

	// Token: 0x04001F41 RID: 8001
	public const int ROOM_INDEX_MAXPL = 7;

	// Token: 0x04001F42 RID: 8002
	public const int ROOM_INDEX_LMEFT = 8;

	// Token: 0x04001F43 RID: 8003
	public const int ROOM_INDEX_LMLVL = 9;

	// Token: 0x04001F44 RID: 8004
	public const int ROOM_INDEX_AUTO = 10;

	// Token: 0x04001F45 RID: 8005
	public const int ROOM_INDEX_PLAYER = 11;

	// Token: 0x04001F46 RID: 8006
	public const int ROOM_INDEX_APPID = 12;

	// Token: 0x04001F47 RID: 8007
	public const int ROOM_INDEX_STATE = 13;

	// Token: 0x04001F48 RID: 8008
	public const int ROOM_INDEX_GROUP = 14;

	// Token: 0x04001F49 RID: 8009
	public const int ROOM_INDEX_PORT = 15;

	// Token: 0x04001F4A RID: 8010
	public const int ROOM_INDEX_CREATETIME = 16;

	// Token: 0x04001F4B RID: 8011
	public const int ROOM_INDEX_ROOMUID = 17;

	// Token: 0x04001F4C RID: 8012
	public const int ROOM_READY_TIME = 18;

	// Token: 0x04001F4D RID: 8013
	private Dictionary<string, UserData> m_szAllUserData = new Dictionary<string, UserData>();

	// Token: 0x04001F4E RID: 8014
	public int m_nRoomID;

	// Token: 0x04001F4F RID: 8015
	public int m_nRoomUID;

	// Token: 0x04001F50 RID: 8016
	public string m_sCreator;

	// Token: 0x04001F51 RID: 8017
	public string m_sRoomName;

	// Token: 0x04001F52 RID: 8018
	public string m_sCreateTime;

	// Token: 0x04001F53 RID: 8019
	public FbXmlData m_pRoomFbData;

	// Token: 0x04001F54 RID: 8020
	public int m_nRoomNowNum;

	// Token: 0x04001F55 RID: 8021
	public int m_nRoomMaxNum;

	// Token: 0x04001F56 RID: 8022
	public int m_nRoomState;

	// Token: 0x04001F57 RID: 8023
	private ArrayList m_szRoomInfo = new ArrayList();

	// Token: 0x04001F58 RID: 8024
	private ArrayList m_szUserList = new ArrayList();
}
