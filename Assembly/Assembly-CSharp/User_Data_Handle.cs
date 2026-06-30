using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class User_Data_Handle
{
	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0018D844 File Offset: 0x0018BA44
	// (set) Token: 0x06002D39 RID: 11577 RVA: 0x0018D873 File Offset: 0x0018BA73
	public static Dictionary<int, Dictionary<int, GamePlayer>> User_Data
	{
		get
		{
			bool flag = User_Data_Handle._User_Data == null;
			if (flag)
			{
				User_Data_Handle._User_Data = new Dictionary<int, Dictionary<int, GamePlayer>>();
			}
			return User_Data_Handle._User_Data;
		}
		set
		{
			User_Data_Handle._User_Data = value;
		}
	}

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x06002D3A RID: 11578 RVA: 0x0018D87C File Offset: 0x0018BA7C
	// (set) Token: 0x06002D3B RID: 11579 RVA: 0x0018D893 File Offset: 0x0018BA93
	public static bool Watch_Fight_State
	{
		get
		{
			return User_Data_Handle._Watch_Fight_State;
		}
		set
		{
			User_Data_Handle._Watch_Fight_State = value;
		}
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x0018D89C File Offset: 0x0018BA9C
	public static void Add_User_Data(int _Country, int _Net_ID, GamePlayer _GamePlayer)
	{
		bool flag = !User_Data_Handle.User_Data.ContainsKey(_Country);
		if (flag)
		{
			User_Data_Handle.User_Data.Add(_Country, new Dictionary<int, GamePlayer>());
		}
		Dictionary<int, GamePlayer> dictionary = User_Data_Handle.User_Data[_Country];
		bool flag2 = !dictionary.ContainsKey(_Net_ID);
		if (flag2)
		{
			dictionary.Add(_Net_ID, _GamePlayer);
		}
		else
		{
			dictionary[_Net_ID] = _GamePlayer;
		}
		bool flag3 = !User_Data_Handle.allGamePlayer.ContainsKey(_GamePlayer.playerCommon.m_nCid);
		if (flag3)
		{
			User_Data_Handle.allGamePlayer.Add(_GamePlayer.playerCommon.m_nCid, _GamePlayer);
		}
		else
		{
			User_Data_Handle.allGamePlayer[_GamePlayer.playerCommon.m_nCid] = _GamePlayer;
		}
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x0018D954 File Offset: 0x0018BB54
	public static Dictionary<int, GamePlayer> Get_User_Data(int _Country)
	{
		Dictionary<int, GamePlayer> dictionary = new Dictionary<int, GamePlayer>();
		bool flag = !User_Data_Handle.User_Data.ContainsKey(_Country);
		Dictionary<int, GamePlayer> result;
		if (flag)
		{
			result = dictionary;
		}
		else
		{
			result = User_Data_Handle.User_Data[_Country];
		}
		return result;
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x0018D990 File Offset: 0x0018BB90
	public static ArrayList Get_User_Data_Array(int _Country)
	{
		ArrayList arrayList = new ArrayList();
		Dictionary<int, GamePlayer> dictionary = User_Data_Handle.Get_User_Data(_Country);
		bool flag = dictionary == null;
		ArrayList result;
		if (flag)
		{
			result = null;
		}
		else
		{
			List<int> list = new List<int>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				GamePlayer gamePlayer = dictionary[list[i]];
				bool flag2 = gamePlayer != null && !gamePlayer.isDead && !gamePlayer.haveDestroy && gamePlayer.getNetid() != fGameManager.GetInstance().getPlayer().getNetid();
				if (flag2)
				{
					arrayList.Add(gamePlayer);
				}
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x0018DA48 File Offset: 0x0018BC48
	public static GamePlayer Get_User_Data(int _Country, int _Net_ID)
	{
		GamePlayer gamePlayer = null;
		Dictionary<int, GamePlayer> dictionary = User_Data_Handle.Get_User_Data(_Country);
		bool flag = dictionary == null;
		GamePlayer result;
		if (flag)
		{
			result = gamePlayer;
		}
		else
		{
			bool flag2 = !dictionary.ContainsKey(_Net_ID);
			if (flag2)
			{
				result = gamePlayer;
			}
			else
			{
				result = dictionary[_Net_ID];
			}
		}
		return result;
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x0018DA90 File Offset: 0x0018BC90
	public static GamePlayer GetPlayerByCid(int cid)
	{
		GamePlayer result = null;
		bool flag = User_Data_Handle.allGamePlayer.ContainsKey(cid);
		if (flag)
		{
			result = User_Data_Handle.allGamePlayer[cid];
		}
		return result;
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x0018DAC4 File Offset: 0x0018BCC4
	public static object Watch_Fight(int _Country)
	{
		User_Data_Handle.Watch_Fight_State = true;
		GamePlayer gamePlayer = null;
		ArrayList arrayList = User_Data_Handle.Get_User_Data_Array(_Country);
		bool flag = arrayList == null || arrayList.Count < 1;
		object result;
		if (flag)
		{
			User_Data_Handle.CanWatchTeamates = false;
			result = null;
		}
		else
		{
			User_Data_Handle.CanWatchTeamates = true;
			bool flag2 = User_Data_Handle.Watch_Random < 0;
			if (flag2)
			{
				User_Data_Handle.Watch_Random = UnityEngine.Random.Range(0, arrayList.Count);
			}
			else
			{
				bool flag3 = User_Data_Handle.Watch_State == 1;
				if (flag3)
				{
					User_Data_Handle.Watch_Random--;
					bool flag4 = User_Data_Handle.Watch_Random < 0;
					if (flag4)
					{
						User_Data_Handle.Watch_Random = arrayList.Count - 1;
					}
				}
				else
				{
					User_Data_Handle.Watch_Random++;
					bool flag5 = User_Data_Handle.Watch_Random >= arrayList.Count;
					if (flag5)
					{
						User_Data_Handle.Watch_Random = 0;
					}
				}
			}
			bool flag6 = User_Data_Handle.Watch_Random >= arrayList.Count;
			if (flag6)
			{
				result = null;
			}
			else
			{
				bool flag7 = arrayList[User_Data_Handle.Watch_Random] != null;
				if (flag7)
				{
					gamePlayer = (GamePlayer)arrayList[User_Data_Handle.Watch_Random];
				}
				CameraControl.Instance.nowTarget = gamePlayer.camTarget;
				result = gamePlayer;
			}
		}
		return result;
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x0018DBF0 File Offset: 0x0018BDF0
	public static ArrayList _getNpcType(int _Net_ID)
	{
		ArrayList arrayList = new ArrayList();
		object obj = null;
		NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(_Net_ID);
		bool flag = netObj == null;
		ArrayList result;
		if (flag)
		{
			arrayList.Add(4);
			arrayList.Add(null);
			result = arrayList;
		}
		else
		{
			bool bePlayer = netObj.bePlayer;
			int num;
			if (bePlayer)
			{
				num = 1;
			}
			else
			{
				num = 2;
				bool flag2 = ((Npc_Entity)netObj).Time > 0f;
				if (flag2)
				{
					num = 3;
				}
				obj = ((Npc_Entity)netObj).Synchronization_Object;
				bool flag3 = obj == null;
				if (flag3)
				{
					obj = GameRoot.SceneSocket.unFsorket.GetNetObj(((Npc_Entity)netObj).Master_ID);
				}
				bool flag4 = obj != null;
				if (flag4)
				{
				}
			}
			arrayList.Add(num);
			arrayList.Add(obj);
			result = arrayList;
		}
		return result;
	}

	// Token: 0x0400265D RID: 9821
	private static Dictionary<int, Dictionary<int, GamePlayer>> _User_Data = null;

	// Token: 0x0400265E RID: 9822
	public static bool CanWatchTeamates = false;

	// Token: 0x0400265F RID: 9823
	public static Dictionary<int, GamePlayer> allGamePlayer = new Dictionary<int, GamePlayer>();

	// Token: 0x04002660 RID: 9824
	private static bool _Watch_Fight_State = false;

	// Token: 0x04002661 RID: 9825
	public static int Watch_Random = -1;

	// Token: 0x04002662 RID: 9826
	public static int Watch_State = 1;
}
