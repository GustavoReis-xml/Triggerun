using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000155 RID: 341
public class GMUICon : Window
{
	// Token: 0x06000F92 RID: 3986 RVA: 0x0008E9DC File Offset: 0x0008CBDC
	protected override void Awake()
	{
		base.Awake();
		GameObject go = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		this.allOnlineNum = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.trainingNum = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.roomUserNum = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.matchUserNum = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.skyLadderUserNum = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		go = this.UIGameObj.Get_GameObj(6);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.itemIdInput = this.UIGameObj.Get_GameObj(7).GetComponent<InputField>();
		this.itemNumInput = this.UIGameObj.Get_GameObj(8).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(9);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.zuanshiInput = this.UIGameObj.Get_GameObj(10).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(11);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.jinbiInput = this.UIGameObj.Get_GameObj(12).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(13);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.expInput = this.UIGameObj.Get_GameObj(14).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(15);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.transNickInput = this.UIGameObj.Get_GameObj(16).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(17);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.hallName = this.UIGameObj.Get_GameObj(32).GetComponent<Text>();
		this.sceneName = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		this.xposTxt = this.UIGameObj.Get_GameObj(19).GetComponent<Text>();
		this.yposTxt = this.UIGameObj.Get_GameObj(20).GetComponent<Text>();
		this.zposTxt = this.UIGameObj.Get_GameObj(21).GetComponent<Text>();
		go = this.UIGameObj.Get_GameObj(22);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		go = this.UIGameObj.Get_GameObj(23);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		go = this.UIGameObj.Get_GameObj(24);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.lockNickInput = this.UIGameObj.Get_GameObj(25).GetComponent<InputField>();
		this.lockChatInput = this.UIGameObj.Get_GameObj(26).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(27);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		go = this.UIGameObj.Get_GameObj(28);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		this.lockLoginInput = this.UIGameObj.Get_GameObj(29).GetComponent<InputField>();
		go = this.UIGameObj.Get_GameObj(30);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		go = this.UIGameObj.Get_GameObj(31);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		go = this.UIGameObj.Get_GameObj(33);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.SendGMOrder);
		GMUICon.inst = this;
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x0008EDE1 File Offset: 0x0008CFE1
	private void Start()
	{
		this.ClearAllMsg();
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x0008EDEB File Offset: 0x0008CFEB
	public override void Reset()
	{
		base.Reset();
		this.ClearAllMsg();
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0008EDFC File Offset: 0x0008CFFC
	private void OnClickClose(GameObject go)
	{
		this.Close();
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0008EE08 File Offset: 0x0008D008
	private void SendGMOrder(GameObject go)
	{
		string sCmd = string.Empty;
		string name = go.name;
		string text = name;
		uint num = PrivateImplementationDetails.ComputeStringHash(text);
		if (num <= 2009679821U)
		{
			if (num <= 1422878156U)
			{
				if (num != 20306552U)
				{
					if (num != 134179961U)
					{
						if (num == 1422878156U)
						{
							if (text == "GetSkyDataBtn")
							{
								sCmd = "getskydata";
							}
						}
					}
					else if (text == "AddItemBtn")
					{
						bool flag = string.IsNullOrEmpty(this.itemIdInput.text) || !Fun.CheckSpecialStr(this.itemIdInput.text) || string.IsNullOrEmpty(this.itemNumInput.text) || !Fun.CheckSpecialStr(this.itemNumInput.text);
						if (flag)
						{
							return;
						}
						sCmd = "copy " + this.itemIdInput.text + "," + this.itemNumInput.text;
					}
				}
				else if (text == "FollowBtn")
				{
					bool flag2 = string.IsNullOrEmpty(this.transNickInput.text) || !Fun.CheckSpecialStr(this.transNickInput.text);
					if (flag2)
					{
						return;
					}
					sCmd = "gensui " + Fun.EncodingUTF8(this.transNickInput.text);
				}
			}
			else if (num <= 1550959896U)
			{
				if (num != 1541447860U)
				{
					if (num == 1550959896U)
					{
						if (text == "TransToBtn")
						{
							bool flag3 = string.IsNullOrEmpty(this.transNickInput.text) || !Fun.CheckSpecialStr(this.transNickInput.text);
							if (flag3)
							{
								return;
							}
							sCmd = "tranto " + Fun.EncodingUTF8(this.transNickInput.text);
						}
					}
				}
				else if (text == "AddJinbiBtn")
				{
					bool flag4 = string.IsNullOrEmpty(this.jinbiInput.text) || !Fun.CheckSpecialStr(this.jinbiInput.text);
					if (flag4)
					{
						return;
					}
					sCmd = "addmoney " + this.jinbiInput.text;
				}
			}
			else if (num != 1665533981U)
			{
				if (num == 2009679821U)
				{
					if (text == "LockLoginBtn")
					{
						bool flag5 = string.IsNullOrEmpty(this.lockNickInput.text) || !Fun.CheckSpecialStr(this.lockNickInput.text) || string.IsNullOrEmpty(this.lockLoginInput.text) || !Fun.CheckSpecialStr(this.lockLoginInput.text);
						if (flag5)
						{
							return;
						}
						sCmd = "lockplayer " + Fun.EncodingUTF8(this.lockNickInput.text) + "," + this.lockLoginInput.text;
					}
				}
			}
			else if (text == "OpenChatBtn")
			{
				bool flag6 = string.IsNullOrEmpty(this.lockNickInput.text) || !Fun.CheckSpecialStr(this.lockNickInput.text);
				if (flag6)
				{
					return;
				}
				sCmd = "openchat " + Fun.EncodingUTF8(this.lockNickInput.text);
			}
		}
		else if (num <= 3720614490U)
		{
			if (num != 2082128634U)
			{
				if (num != 2114753509U)
				{
					if (num == 3720614490U)
					{
						if (text == "AddUserExpBtn")
						{
							bool flag7 = string.IsNullOrEmpty(this.expInput.text) || !Fun.CheckSpecialStr(this.expInput.text);
							if (flag7)
							{
								return;
							}
							sCmd = "addexp " + this.expInput.text;
						}
					}
				}
				else if (text == "KickBtn")
				{
					bool flag8 = string.IsNullOrEmpty(this.transNickInput.text) || !Fun.CheckSpecialStr(this.transNickInput.text);
					if (flag8)
					{
						return;
					}
					sCmd = "kickplayer " + Fun.EncodingUTF8(this.transNickInput.text);
				}
			}
			else if (text == "OpenLoginBtn")
			{
				bool flag9 = string.IsNullOrEmpty(this.lockNickInput.text) || !Fun.CheckSpecialStr(this.lockNickInput.text);
				if (flag9)
				{
					return;
				}
				sCmd = "openplayer " + Fun.EncodingUTF8(this.lockNickInput.text);
			}
		}
		else if (num <= 4089150423U)
		{
			if (num != 4060433404U)
			{
				if (num == 4089150423U)
				{
					if (text == "GetNumBtn")
					{
						sCmd = "getplayernum";
					}
				}
			}
			else if (text == "AddZuanshiBtn")
			{
				bool flag10 = string.IsNullOrEmpty(this.zuanshiInput.text) || !Fun.CheckSpecialStr(this.zuanshiInput.text);
				if (flag10)
				{
					return;
				}
				sCmd = "addcharge " + this.zuanshiInput.text;
			}
		}
		else if (num != 4167359820U)
		{
			if (num == 4285504253U)
			{
				if (text == "GetPosBtn")
				{
					bool flag11 = string.IsNullOrEmpty(this.transNickInput.text) || !Fun.CheckSpecialStr(this.transNickInput.text);
					if (flag11)
					{
						return;
					}
					sCmd = "getplayerpos " + Fun.EncodingUTF8(this.transNickInput.text);
				}
			}
		}
		else if (text == "LockChatBtn")
		{
			bool flag12 = string.IsNullOrEmpty(this.lockNickInput.text) || !Fun.CheckSpecialStr(this.lockNickInput.text) || string.IsNullOrEmpty(this.lockChatInput.text) || !Fun.CheckSpecialStr(this.lockChatInput.text);
			if (flag12)
			{
				return;
			}
			sCmd = "lockchat " + Fun.EncodingUTF8(this.lockNickInput.text) + "," + this.lockChatInput.text;
		}
		this.OnCmd(sCmd);
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x0008F4CC File Offset: 0x0008D6CC
	private void OnCmd(string sCmd)
	{
		bool flag = string.IsNullOrEmpty(sCmd);
		if (!flag)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nGM != 0;
			if (flag2)
			{
				bool flag3 = sCmd.StartsWith("copy");
				if (flag3)
				{
					arrayList.Add(1001);
					this.OnCopy(sCmd, arrayList2);
					arrayList.Add(arrayList2);
				}
				bool flag4 = sCmd.StartsWith("addexp");
				if (flag4)
				{
					arrayList.Add(1005);
					this.OnAddExp(sCmd, arrayList2);
					arrayList.Add(arrayList2);
				}
				bool flag5 = sCmd.StartsWith("addcharge");
				if (flag5)
				{
					arrayList.Add(1006);
					this.OnAddCharge(sCmd, arrayList2);
					arrayList.Add(arrayList2);
				}
				bool flag6 = sCmd.StartsWith("addmoney");
				if (flag6)
				{
					arrayList.Add(1008);
					this.OnAddMoney(sCmd, arrayList2);
					arrayList.Add(arrayList2);
				}
				bool flag7 = sCmd == "getplayernum";
				if (flag7)
				{
					arrayList.Add(1015);
					arrayList.Add(arrayList2);
				}
				bool flag8 = sCmd == "getskydata";
				if (flag8)
				{
					arrayList.Add(1040);
					arrayList.Add(arrayList2);
				}
				bool flag9 = sCmd.StartsWith("getplayerpos");
				if (flag9)
				{
					arrayList.Add(1017);
					string[] array = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array[1]);
					arrayList.Add(arrayList2);
				}
				bool flag10 = sCmd.StartsWith("tranto");
				if (flag10)
				{
					arrayList.Add(1030);
					string[] array2 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array2[1]);
					arrayList.Add(arrayList2);
				}
				bool flag11 = sCmd.StartsWith("gensui");
				if (flag11)
				{
					arrayList.Add(1031);
					string[] array3 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array3[1]);
					arrayList.Add(arrayList2);
				}
				bool flag12 = sCmd.StartsWith("lockchat");
				if (flag12)
				{
					arrayList.Add(1019);
					this.OnGMLockChat(sCmd, arrayList2);
					arrayList.Add(arrayList2);
				}
				bool flag13 = sCmd.StartsWith("openchat");
				if (flag13)
				{
					arrayList.Add(1020);
					string[] array4 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array4[1]);
					arrayList.Add(arrayList2);
				}
				bool flag14 = sCmd.StartsWith("lockplayer");
				if (flag14)
				{
					arrayList.Add(1023);
					string[] array5 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array5[1]);
					arrayList2.Add(int.Parse(array5[2]));
					arrayList.Add(arrayList2);
				}
				bool flag15 = sCmd.StartsWith("openplayer");
				if (flag15)
				{
					arrayList.Add(1024);
					string[] array6 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array6[1]);
					arrayList.Add(arrayList2);
				}
				bool flag16 = sCmd.StartsWith("kickplayer");
				if (flag16)
				{
					arrayList.Add(1025);
					string[] array7 = sCmd.Split(new char[]
					{
						' ',
						','
					});
					arrayList2.Add(array7[1]);
					arrayList.Add(arrayList2);
				}
				bool flag17 = fGameManager.m_nUserType == 0;
				if (flag17)
				{
					fGameManager.LobbyPlayer.SendMsgToHallServer(1310, arrayList);
				}
			}
		}
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0008F8D4 File Offset: 0x0008DAD4
	private void OnCopySp(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' '
		});
		szParam.Add(int.Parse(array[1]));
		szParam.Add(array[2]);
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0008F914 File Offset: 0x0008DB14
	private void OnCopy(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		for (int i = 1; i < array.Length; i++)
		{
			szParam.Add(int.Parse(array[i]));
		}
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0008F964 File Offset: 0x0008DB64
	private void OnCut(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		for (int i = 1; i < array.Length; i++)
		{
			szParam.Add(int.Parse(array[i]));
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0008F9B4 File Offset: 0x0008DBB4
	private void OnAddExp(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(int.Parse(array[1]));
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0008F9F0 File Offset: 0x0008DBF0
	private void OnAddCharge(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(int.Parse(array[1]));
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0008FA2C File Offset: 0x0008DC2C
	private void OnAddChargeBind(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(int.Parse(array[1]));
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0008FA68 File Offset: 0x0008DC68
	private void OnAddMoney(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(int.Parse(array[1]));
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x0008FAA4 File Offset: 0x0008DCA4
	private void OnMove(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(int.Parse(array[1]));
		szParam.Add(0);
		szParam.Add(int.Parse(array[2]));
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x0008FB00 File Offset: 0x0008DD00
	private void OnTrans(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		for (int i = 1; i < array.Length; i++)
		{
			szParam.Add(int.Parse(array[i]));
		}
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0008FB50 File Offset: 0x0008DD50
	private void OnTransLine(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		for (int i = 1; i < array.Length; i++)
		{
			szParam.Add(int.Parse(array[i]));
		}
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0008FBA0 File Offset: 0x0008DDA0
	private void OnUpdateXml(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(array[2]);
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0008FBD0 File Offset: 0x0008DDD0
	private void OnUpdateScript(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(array[2]);
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0008FC00 File Offset: 0x0008DE00
	private void OnUpdateFollow(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		bool flag = array.Length == 1;
		if (flag)
		{
			array = new string[]
			{
				array[0],
				string.Empty
			};
		}
		szParam.Add(array[1]);
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0008FC50 File Offset: 0x0008DE50
	private void OnGMEnterFb(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		for (int i = 1; i < array.Length; i++)
		{
			szParam.Add(array[i]);
		}
		szParam.Add(array[1]);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0008FCA0 File Offset: 0x0008DEA0
	private void OnGMLockChat(string sCmd, ArrayList szParam)
	{
		string[] array = sCmd.Split(new char[]
		{
			' ',
			','
		});
		szParam.Add(array[1]);
		szParam.Add(int.Parse(array[2]));
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0008FCE4 File Offset: 0x0008DEE4
	public void SetPlayerNumTxt(int ntype, ArrayList arr, int nnum = -1)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = ntype == 2;
		if (flag)
		{
			arrayList = (ArrayList)arr[1];
		}
		string text = string.Empty;
		bool flag2 = nnum != -1;
		if (flag2)
		{
			text = nnum.ToString();
		}
		switch (ntype)
		{
		case 1:
			this.allOnlineNum.text = Language.GetLang(52001, new object[0]) + text;
			break;
		case 2:
		{
			text = (((int)arrayList[0] >= 0) ? arrayList[0].ToString() : string.Empty);
			this.trainingNum.text = Language.GetLang(52002, new object[0]) + text;
			text = (((int)arrayList[1] >= 0) ? arrayList[1].ToString() : string.Empty);
			this.matchUserNum.text = Language.GetLang(52004, new object[0]) + text;
			bool flag3 = (int)arrayList[2] < 0;
			if (flag3)
			{
				text = string.Empty;
			}
			else
			{
				text = arrayList[2].ToString();
			}
			break;
		}
		case 3:
			this.roomUserNum.text = Language.GetLang(52022, new object[]
			{
				text
			});
			break;
		}
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0008FE50 File Offset: 0x0008E050
	public void SetSkyMatchNum(ArrayList sznum, ArrayList szrate)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < 9; i++)
		{
			text = ((!string.IsNullOrEmpty(text)) ? (text + "," + sznum[i].ToString()) : (text + sznum[i].ToString()));
			text2 = ((!string.IsNullOrEmpty(text2)) ? (text2 + "," + szrate[i].ToString()) : (text2 + szrate[i].ToString()));
		}
		this.skyLadderUserNum.text = Language.GetLang(52023, new object[]
		{
			text,
			text2
		});
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0008FF0C File Offset: 0x0008E10C
	public void setPlayerPos(ArrayList playerpos)
	{
		string text = playerpos[0].ToString();
		int key = (int)playerpos[1];
		string text2 = text;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(key);
		if (flag)
		{
			int num = (int)playerpos[7];
			text2 += fGameManager.mloadConfig.FbData[key].sName;
			text2 = ((num >= 0) ? (text2 + "<color=#00ff00>" + Language.GetLang(52025, new object[0]) + "</color>") : (text2 + "<color=#ff0000>" + Language.GetLang(52024, new object[0]) + "</color>"));
			Vector3 vector = Fun.ServerToClientPos((int)playerpos[2], (int)playerpos[3], (int)playerpos[4]);
			this.sceneName.text = text2;
			this.xposTxt.text = vector.x.ToString("0.00");
			this.yposTxt.text = vector.y.ToString("0.00");
			this.zposTxt.text = vector.z.ToString("0.00");
		}
		else
		{
			this.hallName.text = text2;
		}
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00090064 File Offset: 0x0008E264
	public void ClearAllMsg()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(2);
		arrayList.Add(new ArrayList
		{
			-1,
			-1,
			-1
		});
		this.SetPlayerNumTxt(1, arrayList, -1);
		this.SetPlayerNumTxt(2, arrayList, -1);
		this.SetPlayerNumTxt(3, arrayList, -1);
		this.hallName.text = string.Empty;
		this.sceneName.text = string.Empty;
		this.xposTxt.text = string.Empty;
		this.yposTxt.text = string.Empty;
		this.zposTxt.text = string.Empty;
	}

	// Token: 0x04000C3E RID: 3134
	private const int GM_ADD_ITEM = 1001;

	// Token: 0x04000C3F RID: 3135
	private const int GM_ADD_EXP = 1005;

	// Token: 0x04000C40 RID: 3136
	private const int GM_ADD_CHARGE = 1006;

	// Token: 0x04000C41 RID: 3137
	private const int GM_ADD_MONEY = 1008;

	// Token: 0x04000C42 RID: 3138
	private const int GM_ALLPLAYERNUM = 1015;

	// Token: 0x04000C43 RID: 3139
	private const int GM_GETSKYDATA = 1040;

	// Token: 0x04000C44 RID: 3140
	private const int GM_GETPLAYERPOS = 1017;

	// Token: 0x04000C45 RID: 3141
	private const int GM_LOCKCHAT = 1019;

	// Token: 0x04000C46 RID: 3142
	private const int GM_OPENCHAT = 1020;

	// Token: 0x04000C47 RID: 3143
	private const int GM_LOCKPLAY = 1023;

	// Token: 0x04000C48 RID: 3144
	private const int GM_OPENPLAY = 1024;

	// Token: 0x04000C49 RID: 3145
	private const int GM_KICKPLAYER = 1025;

	// Token: 0x04000C4A RID: 3146
	private const int GM_TRANTO = 1030;

	// Token: 0x04000C4B RID: 3147
	private const int GM_GENSUI = 1031;

	// Token: 0x04000C4C RID: 3148
	public static GMUICon inst;

	// Token: 0x04000C4D RID: 3149
	private Text allOnlineNum;

	// Token: 0x04000C4E RID: 3150
	private Text trainingNum;

	// Token: 0x04000C4F RID: 3151
	private Text roomUserNum;

	// Token: 0x04000C50 RID: 3152
	private Text matchUserNum;

	// Token: 0x04000C51 RID: 3153
	private Text skyLadderUserNum;

	// Token: 0x04000C52 RID: 3154
	private InputField itemIdInput;

	// Token: 0x04000C53 RID: 3155
	private InputField itemNumInput;

	// Token: 0x04000C54 RID: 3156
	private InputField zuanshiInput;

	// Token: 0x04000C55 RID: 3157
	private InputField jinbiInput;

	// Token: 0x04000C56 RID: 3158
	private InputField expInput;

	// Token: 0x04000C57 RID: 3159
	private InputField transNickInput;

	// Token: 0x04000C58 RID: 3160
	private Text hallName;

	// Token: 0x04000C59 RID: 3161
	private Text sceneName;

	// Token: 0x04000C5A RID: 3162
	private Text xposTxt;

	// Token: 0x04000C5B RID: 3163
	private Text yposTxt;

	// Token: 0x04000C5C RID: 3164
	private Text zposTxt;

	// Token: 0x04000C5D RID: 3165
	private InputField lockNickInput;

	// Token: 0x04000C5E RID: 3166
	private InputField lockChatInput;

	// Token: 0x04000C5F RID: 3167
	private InputField lockLoginInput;
}
