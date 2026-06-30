using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class CFilmManager : MonoBehaviour
{
	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060003DA RID: 986 RVA: 0x0001E672 File Offset: 0x0001C872
	public ArrayList Films
	{
		get
		{
			return this._film;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060003DB RID: 987 RVA: 0x0001E67C File Offset: 0x0001C87C
	// (set) Token: 0x060003DC RID: 988 RVA: 0x0001E693 File Offset: 0x0001C893
	public static CFilmManager Instance
	{
		get
		{
			return CFilmManager._instance;
		}
		set
		{
			CFilmManager._instance = value;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060003DD RID: 989 RVA: 0x0001E69C File Offset: 0x0001C89C
	public Dictionary<string, NetObj> GetFilmNetLivingAll
	{
		get
		{
			return this.NetListCreate;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060003DE RID: 990 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
	public DateTime filmTime
	{
		get
		{
			return Fun.GetServerDate().AddSeconds((double)(0f - this.fileLongTime));
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001E6CB File Offset: 0x0001C8CB
	private void Start()
	{
		CFilmManager.Instance = this;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
	public NetLiving GetFilmNetLiving(int netid)
	{
		string key = netid.ToString();
		bool flag = this.NetListCreate.ContainsKey(key);
		NetLiving result;
		if (flag)
		{
			result = (NetLiving)this.NetListCreate[key];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001E718 File Offset: 0x0001C918
	public void InitFilm()
	{
		this._film = new ArrayList();
		this._filmCreateObj = new Dictionary<int, ArrayList>();
		this.NetListCreate = new Dictionary<string, NetObj>();
		this.NetListDisable = new ArrayList();
		this.playing = false;
		this.playerForce = -1;
		this.filmnetid = -1;
		this.filmparentnetid = -1;
		this.removeFilmFrameNumber = 0;
		this.filmParentNpcOrPlayerID = -1;
		this.breakFilm = false;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001E784 File Offset: 0x0001C984
	public int filmForce(NetLiving net)
	{
		bool isCopy = net.isCopy;
		int nShiliServer;
		if (isCopy)
		{
			nShiliServer = this.playerForce;
		}
		else
		{
			bool flag = fGameManager.Heroplayer != null;
			if (flag)
			{
				nShiliServer = fGameManager.Heroplayer.playerCommon.m_nShiliServer;
			}
			else
			{
				nShiliServer = this.playerForce;
			}
		}
		return nShiliServer;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
	public int filmParentNetId(NetLiving net)
	{
		bool isCopy = net.isCopy;
		int netid;
		if (isCopy)
		{
			netid = this.filmparentnetid;
		}
		else
		{
			bool flag = fGameManager.Heroplayer != null;
			if (flag)
			{
				netid = fGameManager.Heroplayer.getNetid();
			}
			else
			{
				netid = this.filmparentnetid;
			}
		}
		return netid;
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0001E818 File Offset: 0x0001CA18
	public int filmNetId(NetLiving net)
	{
		bool isCopy = net.isCopy;
		int netid;
		if (isCopy)
		{
			netid = this.filmnetid;
		}
		else
		{
			bool flag = fGameManager.Heroplayer != null;
			if (flag)
			{
				netid = fGameManager.Heroplayer.getNetid();
			}
			else
			{
				netid = this.filmnetid;
			}
		}
		return netid;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001E860 File Offset: 0x0001CA60
	private void InsertPlayerData(float n, int netid, char funtype, string funName, string type, object[] data)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(n);
		arrayList.Add(netid);
		arrayList.Add(funName);
		arrayList.Add(type);
		arrayList.Add(data);
		arrayList.Add(funtype);
		arrayList.Add(new object[0]);
		arrayList.Add(new ArrayList
		{
			new object[0],
			new object[0],
			new object[0],
			new object[0],
			new object[0],
			new object[0],
			new object[0]
		});
		arrayList.Add(new Dictionary<string, CAData>());
		arrayList.Add(new object[0]);
		arrayList.Add(new Dictionary<int, int>());
		arrayList.Add(new ArrayList());
		bool flag = this._filmCreateObj.ContainsKey(netid);
		if (flag)
		{
			Debug.Log("录像出现了创建多个相同对象!");
		}
		else
		{
			this._filmCreateObj.Add(netid, arrayList);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001E98C File Offset: 0x0001CB8C
	private void UpdatePlayerData(int netid, string type, object[] data)
	{
		bool flag = !this._filmCreateObj.ContainsKey(netid);
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)this._filmCreateObj[netid][7];
			Dictionary<string, CAData> dictionary = (Dictionary<string, CAData>)this._filmCreateObj[netid][8];
			Dictionary<int, int> dictionary2 = (Dictionary<int, int>)this._filmCreateObj[netid][10];
			uint num = PrivateImplementationDetails.ComputeStringHash(type);
			if (num <= 1792671826U)
			{
				if (num <= 1530604730U)
				{
					if (num != 70662023U)
					{
						if (num == 1530604730U)
						{
							if (type == "HaveDamage")
							{
								ArrayList value = (ArrayList)data[1];
								this._filmCreateObj[netid][11] = value;
							}
						}
					}
					else if (type == "SendMsgToAllClient")
					{
						int num2 = (int)data[0];
						ArrayList arrayList2 = (ArrayList)data[1];
						int num3 = num2;
						int num4 = num3;
						if (num4 != 31000)
						{
							if (num4 == 50001)
							{
								int num5 = (int)arrayList2[0];
								ArrayList arrayList3 = (ArrayList)arrayList2[1];
								switch (num5)
								{
								case 50004:
									arrayList[4] = data;
									break;
								case 50005:
									arrayList[6] = data;
									break;
								case 50008:
									arrayList[3] = data;
									break;
								}
							}
						}
						else
						{
							ArrayList arrayList4 = (ArrayList)arrayList2[1];
							int num6 = (int)arrayList2[0];
							bool flag2 = num6 == 31209;
							if (flag2)
							{
								arrayList[5] = data;
							}
						}
					}
				}
				else if (num != 1692006112U)
				{
					if (num != 1725561350U)
					{
						if (num == 1792671826U)
						{
							if (type == "CT")
							{
								arrayList[2] = data;
							}
						}
					}
					else if (type == "CP")
					{
						arrayList[0] = data;
					}
				}
				else if (type == "CR")
				{
					arrayList[1] = data;
				}
			}
			else if (num <= 2010780873U)
			{
				if (num != 1960448016U)
				{
					if (num == 2010780873U)
					{
						if (type == "CA")
						{
							ArrayList arrayList5 = (ArrayList)data[0];
							ArrayList arrayList6 = (ArrayList)arrayList5[0];
							string text = (string)arrayList6[0];
							bool flag3 = (int)arrayList5[1] != 2 && ((int)arrayList5[1] != 0 || !this.animatorStatSynchronization.Contains(text));
							if (!flag3)
							{
								bool flag4 = (int)arrayList5[1] == 2 && text.Contains("HuDongState") && (int)arrayList6[2] < 200;
								if (flag4)
								{
									bool flag5 = dictionary.ContainsKey("HuDongState");
									if (flag5)
									{
										dictionary.Remove("HuDongState");
									}
								}
								else
								{
									bool flag6 = (int)arrayList5[1] == 2 && text.Contains("ActiveSwith");
									if (flag6)
									{
										bool flag7 = dictionary.ContainsKey("HuDongState");
										if (flag7)
										{
											dictionary.Remove("HuDongState");
										}
									}
									else
									{
										bool flag8 = !dictionary.ContainsKey(text);
										if (flag8)
										{
											dictionary.Add(text, new CAData
											{
												t = Time.realtimeSinceStartup,
												n = text,
												data = data
											});
										}
										else
										{
											dictionary[text] = new CAData
											{
												t = Time.realtimeSinceStartup,
												n = text,
												data = data
											};
										}
									}
								}
							}
						}
					}
				}
				else if (type == "CB")
				{
					int num7 = (int)data[9];
					bool flag9 = num7 >= 0;
					if (flag9)
					{
						bool flag10 = dictionary2.ContainsKey(num7);
						if (flag10)
						{
							dictionary2[num7] = (int)data[8];
						}
						else
						{
							dictionary2.Add(num7, (int)data[8]);
						}
					}
				}
			}
			else if (num != 2645101852U)
			{
				if (num != 2678657090U)
				{
					if (num == 2971283538U)
					{
						if (type == "NSTP")
						{
							arrayList[2] = data;
						}
					}
				}
				else if (type == "NSP")
				{
					arrayList[0] = data;
				}
			}
			else if (type == "NSR")
			{
				arrayList[1] = data;
			}
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
	public void InsertGamePlayerFilm(Type typeObj, object net, char funtype, string funName, object[] data)
	{
		bool flag = !this.isopen;
		if (!flag)
		{
			string text = typeObj.ToString();
			NetObj netObj = (NetObj)net;
			bool flag2 = text == "HallUser" || ((NetObj)net).isCopy || !netObj.filmObj;
			if (!flag2)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				uint num = PrivateImplementationDetails.ComputeStringHash(funName);
				if (num <= 2534049169U)
				{
					if (num <= 1725561350U)
					{
						if (num <= 1344779576U)
						{
							if (num != 55514139U)
							{
								if (num != 70662023U)
								{
									if (num == 1344779576U)
									{
										if (funName == "SuperSkillUpdate")
										{
											goto IL_719;
										}
									}
								}
								else if (funName == "SendMsgToAllClient")
								{
									int num2 = (int)data[0];
									int num3 = num2;
									if (num3 <= 31000)
									{
										if (num3 == 10022)
										{
											goto IL_715;
										}
										if (num3 == 31000)
										{
											ArrayList arrayList = (ArrayList)data[1];
											int num4 = (int)arrayList[0];
											bool flag3 = num4 != 31209 && num4 != 31212;
											if (!flag3)
											{
												goto IL_717;
											}
										}
									}
									else if (num3 == 50001 || num3 - 51002 <= 1 || num3 == 2010002)
									{
										goto IL_715;
									}
									return;
									IL_715:
									IL_717:
									goto IL_71B;
								}
							}
							else if (funName == "Destroy_Model")
							{
								goto IL_719;
							}
						}
						else if (num <= 1692006112U)
						{
							if (num != 1530604730U)
							{
								if (num == 1692006112U)
								{
									if (funName == "CR")
									{
										goto IL_719;
									}
								}
							}
							else if (funName == "HaveDamage")
							{
								goto IL_719;
							}
						}
						else if (num != 1697414975U)
						{
							if (num == 1725561350U)
							{
								if (funName == "CP")
								{
									goto IL_719;
								}
							}
						}
						else if (funName == "DeleteBuff")
						{
							goto IL_719;
						}
					}
					else if (num <= 1960448016U)
					{
						if (num != 1775894207U)
						{
							if (num != 1792671826U)
							{
								if (num == 1960448016U)
								{
									if (funName == "CB")
									{
										goto IL_719;
									}
								}
							}
							else if (funName == "CT")
							{
								goto IL_719;
							}
						}
						else if (funName == "CW")
						{
							goto IL_719;
						}
					}
					else if (num <= 2100148498U)
					{
						if (num != 2010780873U)
						{
							if (num == 2100148498U)
							{
								if (funName == "NCAS")
								{
									goto IL_719;
								}
							}
						}
						else if (funName == "CA")
						{
							goto IL_719;
						}
					}
					else if (num != 2508901530U)
					{
						if (num == 2534049169U)
						{
							if (funName == "ChangeWeaponGroupUpdate")
							{
								goto IL_719;
							}
						}
					}
					else if (funName == "SendMsgToClient")
					{
						int num5 = (int)data[0];
						int num6 = num5;
						if (num6 != 2000)
						{
							if (num6 != 10001)
							{
								return;
							}
							ArrayList arrayList2 = (ArrayList)data[1];
							ArrayList arrayList3 = (ArrayList)arrayList2[0];
							ArrayList arrayList4 = (ArrayList)arrayList3[0];
							bool flag4 = (int)arrayList4[10] > 0 && ((ArrayList)arrayList4[13]).Count > 0;
							if (flag4)
							{
								return;
							}
							this.InsertPlayerData(realtimeSinceStartup, netObj.getNetid(), funtype, funName, "HeroPlayer", data);
						}
						else
						{
							ArrayList arrayList5 = (ArrayList)data[1];
							int num7 = (int)arrayList5[0];
							bool flag5 = num7 != 2001;
							if (flag5)
							{
								return;
							}
						}
						goto IL_71B;
					}
				}
				else if (num <= 2822196027U)
				{
					if (num <= 2645101852U)
					{
						if (num != 2611546614U)
						{
							if (num != 2634220395U)
							{
								if (num == 2645101852U)
								{
									if (funName == "NSR")
									{
										goto IL_719;
									}
								}
							}
							else if (funName == "Initialize")
							{
								this.InsertPlayerData(realtimeSinceStartup, netObj.getNetid(), funtype, funName, text, data);
								goto IL_71B;
							}
						}
						else if (funName == "NST")
						{
							goto IL_719;
						}
					}
					else if (num <= 2726886115U)
					{
						if (num != 2678657090U)
						{
							if (num == 2726886115U)
							{
								if (funName == "RemoteCreate")
								{
									ArrayList arrayList6 = (ArrayList)data[0];
									ArrayList arrayList7 = (ArrayList)arrayList6[0];
									ArrayList arrayList8 = (ArrayList)arrayList7[0];
									bool flag6 = (int)arrayList8[10] > 0 && ((ArrayList)arrayList8[13]).Count > 0;
									if (!flag6)
									{
										this.InsertPlayerData(realtimeSinceStartup, netObj.getNetid(), funtype, funName, "GamePlayer", data);
										goto IL_71B;
									}
								}
							}
						}
						else if (funName == "NSP")
						{
							goto IL_719;
						}
					}
					else if (num != 2818791336U)
					{
						if (num == 2822196027U)
						{
							if (funName == "SkillDo")
							{
								goto IL_719;
							}
						}
					}
					else if (funName == "PlayerSelect")
					{
						goto IL_719;
					}
				}
				else if (num <= 3006950371U)
				{
					if (num <= 2913268402U)
					{
						if (num != 2856038445U)
						{
							if (num == 2913268402U)
							{
								if (funName == "OnDeath")
								{
									goto IL_719;
								}
							}
						}
						else if (funName == "OnFillBullet")
						{
							goto IL_719;
						}
					}
					else if (num != 2971283538U)
					{
						if (num == 3006950371U)
						{
							if (funName == "OnNetDestroy")
							{
								goto IL_719;
							}
						}
					}
					else if (funName == "NSTP")
					{
						goto IL_719;
					}
				}
				else if (num <= 3573589764U)
				{
					if (num != 3109924991U)
					{
						if (num == 3573589764U)
						{
							if (funName == "SkillParmsUpdate")
							{
								goto IL_719;
							}
						}
					}
					else if (funName == "SkillDataUpdate")
					{
						goto IL_719;
					}
				}
				else if (num != 3863612076U)
				{
					if (num == 4100294365U)
					{
						if (funName == "AddBuff")
						{
							goto IL_719;
						}
					}
				}
				else if (funName == "CB_READY")
				{
					goto IL_719;
				}
				return;
				IL_719:
				IL_71B:
				float num8 = realtimeSinceStartup - this.fileBeginTime;
				int num9 = 0;
				while (this._film.Count > 0 && this.removeFilmFrameNumber < this._film.Count)
				{
					ArrayList arrayList9 = (ArrayList)this._film[this.removeFilmFrameNumber];
					float num10 = (float)arrayList9[0];
					object[] array = (object[])arrayList9[4];
					float num11 = num10 - num8;
					bool flag7 = num11 >= 0f;
					if (flag7)
					{
						break;
					}
					bool flag8 = !this.playing;
					if (flag8)
					{
						bool flag9 = this.removeFilmFrameNumber != 0;
						if (flag9)
						{
							Debug.LogError("这个咋会不是0," + this.removeFilmFrameNumber.ToString());
						}
						this._film.RemoveAt(this.removeFilmFrameNumber);
					}
					else
					{
						this.removeFilmFrameNumber++;
					}
					string text2 = (string)arrayList9[1];
					int num12 = (int)arrayList9[2];
					string text3 = text2;
					string text4 = text3;
					num = PrivateImplementationDetails.ComputeStringHash(text4);
					if (num <= 1960448016U)
					{
						if (num <= 1692006112U)
						{
							if (num != 70662023U)
							{
								if (num != 1530604730U)
								{
									if (num == 1692006112U)
									{
										if (text4 == "CR")
										{
											goto IL_BE6;
										}
									}
								}
								else if (text4 == "HaveDamage")
								{
									goto IL_BE6;
								}
							}
							else if (text4 == "SendMsgToAllClient")
							{
								int num13 = (int)array[0];
								ArrayList arrayList10 = (ArrayList)array[1];
								int num14 = num13;
								int num15 = num14;
								if (num15 != 10022)
								{
									if (num15 != 31000)
									{
										if (num15 == 50001)
										{
											int num16 = (int)arrayList10[0];
											ArrayList arrayList11 = (ArrayList)arrayList10[1];
											bool flag10 = num16 == 50004 || num16 == 50005 || num16 == 50008;
											if (flag10)
											{
												this.UpdatePlayerData(num12, text2, array);
											}
										}
									}
									else
									{
										ArrayList arrayList12 = (ArrayList)arrayList10[1];
										int num17 = (int)arrayList10[0];
										bool flag11 = num17 == 31209;
										if (flag11)
										{
											this.UpdatePlayerData(num12, text2, array);
										}
									}
								}
								else
								{
									this._filmCreateObj[num12][9] = new object[0];
									this.DeadData(num12, false);
								}
							}
						}
						else if (num != 1725561350U)
						{
							if (num != 1792671826U)
							{
								if (num == 1960448016U)
								{
									if (text4 == "CB")
									{
										goto IL_BE6;
									}
								}
							}
							else if (text4 == "CT")
							{
								goto IL_BE6;
							}
						}
						else if (text4 == "CP")
						{
							goto IL_BE6;
						}
					}
					else if (num <= 2678657090U)
					{
						if (num != 2010780873U)
						{
							if (num != 2645101852U)
							{
								if (num == 2678657090U)
								{
									if (text4 == "NSP")
									{
										goto IL_BE6;
									}
								}
							}
							else if (text4 == "NSR")
							{
								goto IL_BE6;
							}
						}
						else if (text4 == "CA")
						{
							goto IL_BE6;
						}
					}
					else if (num <= 2913268402U)
					{
						if (num != 2818791336U)
						{
							if (num == 2913268402U)
							{
								if (text4 == "OnDeath")
								{
									this._filmCreateObj[num12][9] = array;
									this.DeadData(num12, true);
								}
							}
						}
						else if (text4 == "PlayerSelect")
						{
							bool flag12 = this._filmCreateObj.ContainsKey(num12);
							if (flag12)
							{
								arrayList9[4] = new object[0];
								this._filmCreateObj[num12][6] = array;
								ArrayList arrayList13 = (ArrayList)this._filmCreateObj[num12][7];
								Dictionary<string, CAData> dictionary = (Dictionary<string, CAData>)this._filmCreateObj[num12][8];
								bool flag13 = dictionary.Count > 0;
								if (flag13)
								{
									dictionary.Clear();
								}
								bool flag14 = ((object[])arrayList13[3]).Length != 0;
								if (flag14)
								{
									arrayList13[3] = new object[0];
								}
								bool flag15 = ((object[])arrayList13[4]).Length != 0;
								if (flag15)
								{
									arrayList13[4] = new object[0];
								}
								bool flag16 = ((object[])arrayList13[5]).Length != 0;
								if (flag16)
								{
									arrayList13[5] = new object[0];
								}
								bool flag17 = ((object[])arrayList13[6]).Length != 0;
								if (flag17)
								{
									arrayList13[6] = new object[0];
								}
								this._filmCreateObj[num12][9] = new object[0];
								this._filmCreateObj[num12][10] = new Dictionary<int, int>();
								this._filmCreateObj[num12][11] = new ArrayList();
								this.DeadData(num12, false);
							}
						}
					}
					else if (num != 2971283538U)
					{
						if (num == 3006950371U)
						{
							if (text4 == "OnNetDestroy")
							{
								bool flag18 = this._filmCreateObj.ContainsKey(num12);
								if (flag18)
								{
									this._filmCreateObj.Remove(num12);
								}
							}
						}
					}
					else if (text4 == "NSTP")
					{
						goto IL_BE6;
					}
					IL_CFA:
					num9++;
					bool flag19 = num9 > 100;
					if (flag19)
					{
						break;
					}
					continue;
					IL_BE6:
					this.UpdatePlayerData(num12, text2, array);
					goto IL_CFA;
				}
				ArrayList arrayList14 = new ArrayList();
				arrayList14.Add(realtimeSinceStartup);
				arrayList14.Add(funName);
				arrayList14.Add(netObj.getNetid());
				arrayList14.Add(funtype);
				arrayList14.Add(data);
				arrayList14.Add(text);
				this._film.Add(arrayList14);
			}
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001FC74 File Offset: 0x0001DE74
	private void DeadData(int netid, bool isdead)
	{
		ArrayList arrayList = this._filmCreateObj[netid];
		object[] array = (object[])arrayList[4];
		string a = (string)arrayList[3];
		bool flag = array.Length == 0;
		if (flag)
		{
			array = (object[])arrayList[6];
		}
		ArrayList arrayList2 = (!(a == "HeroPlayer")) ? ((ArrayList)array[0]) : ((ArrayList)array[1]);
		ArrayList arrayList3 = (ArrayList)arrayList2[1];
		if (isdead)
		{
			arrayList3[0] = 0;
		}
		else
		{
			arrayList3[0] = 1;
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001FD1C File Offset: 0x0001DF1C
	public void PlayFilms(int fromMSec, int netid, int force, int filmNetparentid, int _filmParentNpcOrPlayerID)
	{
		bool flag = this._film.Count != 0 && !this.playing;
		if (flag)
		{
			this.playerForce = force;
			this.filmnetid = netid;
			this.breakFilm = false;
			this.filmparentnetid = filmNetparentid;
			this.filmParentNpcOrPlayerID = _filmParentNpcOrPlayerID;
			this.beginplayDateTime = Time.realtimeSinceStartup - this.fileBeginTime;
			this.endplayDateTime = Time.realtimeSinceStartup + this.fileLongTime;
			this._filmCopy = this._film;
			this._filmCreateObjCopy = this._filmCreateObj;
			this.playing = true;
			this.NetListCreate = new Dictionary<string, NetObj>();
			this.NetListDisable = new ArrayList();
			base.StopAllCoroutines();
			base.StartCoroutine(this.filming(fromMSec, netid, this.filmparentnetid, _filmParentNpcOrPlayerID));
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
	private IEnumerator filming(int fromMSec, int id, int parentNetid, int _filmParentNpcOrPlayerID)
	{
		int beginNo = 0;
		float theTime = 0f;
		bool BeHeroClass = false;
		bool haveCamera = false;
		int lastcameraId = -1;
		NetLiving film1Net = null;
		NetLiving film2Net = null;
		NetLiving filmChangeNet = null;
		float now = Time.realtimeSinceStartup;
		bool flag = this._filmCopy.Count == 0 || this.breakFilm || fGameManager.Heroplayer == null || fGameManager.Heroplayer.haveDestroy;
		if (flag)
		{
			this.FilmFinish(true, 0);
			yield break;
		}
		bool flag2 = id == -1;
		if (flag2)
		{
			id = fGameManager.Heroplayer.getNetid();
		}
		List<string> keys = new List<string>(GameRoot.SceneSocket.unFsorket.NetObjTable.Keys);
		int i;
		int num2;
		for (i = 0; i < keys.Count; i = num2 + 1)
		{
			string key = keys[i];
			bool flag3 = !GameRoot.SceneSocket.unFsorket.NetObjTable.ContainsKey(key);
			if (!flag3)
			{
				NetLiving netliv = (NetLiving)GameRoot.SceneSocket.unFsorket.NetObjTable[key];
				bool flag4 = netliv == null || !netliv.filmObj || netliv.haveDestroy || !netliv.playerHaveCreate;
				if (!flag4)
				{
					bool flag5 = netliv.wc != null;
					if (flag5)
					{
						netliv.wc.StopAllFireEffect();
					}
					bool flag6 = netliv.getNetid() == fGameManager.Heroplayer.getNetid();
					if (flag6)
					{
						netliv.FpsOrTps = 2;
						netliv.ChangeFpsTpsState();
					}
					netliv.renderBianSe.SetFilmMaterial(true);
					netliv.heroBase.SetFilmMaterial(true);
					netliv.publicInfo.SetAllColliderActive(false);
					netliv.SetGameUiSignObj(false);
					bool flag7 = netliv.userModelTps != null;
					if (flag7)
					{
						CHeroWeapon component = netliv.userModelTps.GetComponent<CHeroWeapon>();
						bool flag8 = component != null;
						if (flag8)
						{
							component.SetWeaponParts(true);
						}
						component = null;
					}
					this.NetListDisable.Add(netliv);
					key = null;
					netliv = null;
				}
			}
			num2 = i;
		}
		List<int> thenetids = new List<int>(this._filmCreateObjCopy.Keys);
		for (i = 0; i < thenetids.Count; i = num2 + 1)
		{
			int thenetid = thenetids[i];
			bool flag9 = !fGameManager.Heroplayer.canControl || fGameManager.GetInstance().nNowScnID == 0 || this.breakFilm;
			if (flag9)
			{
				this.FilmFinish(true, 0);
				yield break;
			}
			bool flag10 = !this._filmCreateObjCopy.ContainsKey(thenetid);
			if (!flag10)
			{
				ArrayList dHead = this._filmCreateObjCopy[thenetid];
				float timeCreate = (float)dHead[0];
				string netsid = thenetid.ToString();
				string funName = (string)dHead[2];
				string typeString = (string)dHead[3];
				object[] parm = (object[])dHead[4];
				object[] pselect = (object[])dHead[6];
				ArrayList posData = (ArrayList)dHead[7];
				Dictionary<string, CAData> dictCA = (Dictionary<string, CAData>)dHead[8];
				object[] deaddata = (object[])dHead[9];
				Dictionary<int, int> bulletdata = (Dictionary<int, int>)dHead[10];
				ArrayList hpData = (ArrayList)dHead[11];
				bool flag11 = this.NetListCreate.ContainsKey(netsid);
				if (!flag11)
				{
					bool flag12 = typeString == "HeroPlayer";
					if (flag12)
					{
						typeString = "GamePlayer";
						BeHeroClass = true;
					}
					else
					{
						BeHeroClass = false;
					}
					NetLiving netliv2 = null;
					bool flag13 = timeCreate <= this.beginplayDateTime;
					if (flag13)
					{
						Type type = Type.GetType(typeString);
						netliv2 = (NetLiving)Activator.CreateInstance(type);
						netliv2.setNetid(thenetid);
						netliv2.isCopy = true;
						netliv2.OnCreate();
						bool flag14 = parentNetid == thenetid;
						if (flag14)
						{
							netliv2.filmMainLayer = true;
						}
						netliv2.FpsOrTps = -1;
						this.NetListCreate.Add(netsid, netliv2);
						bool flag15 = dictCA.Count > 0;
						if (flag15)
						{
							netliv2.cAnimationData = dictCA;
						}
						bool flag16 = pselect.Length != 0;
						if (flag16)
						{
							netliv2.FpsOrTps = -1;
							this.RunFunction(netliv2, "PlayerSelect", pselect);
						}
						else
						{
							bool flag17 = !BeHeroClass;
							if (flag17)
							{
								this.RunFunction(netliv2, funName, parm);
							}
							else
							{
								ArrayList data = (ArrayList)parm[1];
								((GamePlayer)netliv2).RemoteCreate(data);
								data = null;
							}
						}
						type = null;
					}
					bool flag18 = netliv2 != null;
					if (flag18)
					{
						while (!netliv2.playerHaveCreate)
						{
							yield return 0;
						}
						bool flag19 = ((object[])posData[0]).Length != 0;
						if (flag19)
						{
							bool flag20 = netliv2.netobject_type == 2;
							if (flag20)
							{
								this.RunFunction(netliv2, "NSP", (object[])posData[0]);
								netliv2.UserControl.transform.position = netliv2.playerCommon.pos;
							}
							else
							{
								this.RunFunction(netliv2, "CP", (object[])posData[0]);
								netliv2.UserControl.transform.position = netliv2.playerCommon.pos;
							}
						}
						bool flag21 = ((object[])posData[1]).Length != 0;
						if (flag21)
						{
							bool flag22 = netliv2.netobject_type == 2;
							if (flag22)
							{
								this.RunFunction(netliv2, "NSR", (object[])posData[1]);
							}
							else
							{
								this.RunFunction(netliv2, "CR", (object[])posData[1]);
							}
							netliv2.UserControl.transform.eulerAngles = new Vector3(0f, netliv2.playerCommon.rot.y, 0f);
						}
						bool flag23 = ((object[])posData[2]).Length != 0;
						if (flag23)
						{
							bool flag24 = netliv2.netobject_type == 2;
							if (flag24)
							{
								this.RunFunction(netliv2, "NSTP", (object[])posData[2]);
							}
							else
							{
								this.RunFunction(netliv2, "CT", (object[])posData[2]);
							}
						}
						bool flag25 = ((object[])posData[3]).Length != 0;
						if (flag25)
						{
							this.RunFunction(netliv2, "SendMsgToAllClient", (object[])posData[3]);
						}
						bool flag26 = ((object[])posData[6]).Length != 0;
						if (flag26)
						{
							this.RunFunction(netliv2, "SendMsgToAllClient", (object[])posData[6]);
						}
						bool flag27 = ((object[])posData[4]).Length != 0;
						if (flag27)
						{
							this.RunFunction(netliv2, "SendMsgToAllClient", (object[])posData[4]);
						}
						bool flag28 = ((object[])posData[5]).Length != 0;
						if (flag28)
						{
							this.RunFunction(netliv2, "SendMsgToAllClient", (object[])posData[5]);
						}
						bool flag29 = deaddata.Length != 0;
						if (flag29)
						{
							this.RunFunction(netliv2, "OnDeath", deaddata);
						}
						bool flag30 = bulletdata.Count > 0;
						if (flag30)
						{
							List<int> list = new List<int>(bulletdata.Keys);
							for (int j = 0; j < list.Count; j = num2 + 1)
							{
								int num = list[j];
								netliv2.playerCommon.aryCartridgePublicNow[num] = bulletdata[num];
								netliv2.wc.cartridgePublicNow[num] = bulletdata[num];
								num2 = j;
							}
							list = null;
						}
						bool flag31 = hpData.Count > 0;
						if (flag31)
						{
							netliv2.SetHp(hpData);
						}
					}
					bool flag32 = !haveCamera && netliv2 != null;
					if (flag32)
					{
						bool flag33 = id == thenetid;
						if (flag33)
						{
							film1Net = netliv2;
						}
						bool flag34 = parentNetid == thenetid;
						if (flag34)
						{
							film2Net = netliv2;
						}
					}
					dHead = null;
					netsid = null;
					funName = null;
					typeString = null;
					parm = null;
					pselect = null;
					posData = null;
					dictCA = null;
					deaddata = null;
					bulletdata = null;
					hpData = null;
					netliv2 = null;
				}
			}
			num2 = i;
		}
		bool flag35 = !fGameManager.Heroplayer.canControl || fGameManager.GetInstance().nNowScnID == 0 || this.breakFilm;
		if (flag35)
		{
			this.FilmFinish(true, 0);
			yield break;
		}
		bool flag36 = film1Net != null;
		if (flag36)
		{
			CCameraChange.Instance.playFilmByPlayer(film1Net);
			film1Net.SetGameUiSignObj(false);
			this.nowDisableTopNameNetLiving = film1Net;
			haveCamera = true;
			lastcameraId = CameraControl.Instance.camerHero.getNetid();
		}
		bool flag37 = !haveCamera && film2Net != null;
		if (flag37)
		{
			CCameraChange.Instance.playFilmByPlayer(film2Net);
			film2Net.SetGameUiSignObj(false);
			this.nowDisableTopNameNetLiving = film2Net;
			lastcameraId = CameraControl.Instance.camerHero.getNetid();
			haveCamera = true;
		}
		i = beginNo;
		float bTime = now;
		haveCamera = false;
		while (i < this._filmCopy.Count && Time.realtimeSinceStartup <= this.endplayDateTime && fGameManager.Heroplayer.canControl && fGameManager.GetInstance().nNowScnID != 0 && !this.breakFilm)
		{
			ArrayList data2 = (ArrayList)this._filmCopy[i];
			theTime = (float)data2[0];
			float theSpan = theTime - this.beginplayDateTime;
			float timeSpan = Time.realtimeSinceStartup - bTime;
			bool flag38 = theSpan > timeSpan;
			if (flag38)
			{
				yield return 0;
			}
			else
			{
				num2 = i;
				i = num2 + 1;
				int netid = (int)data2[2];
				string netsid2 = netid.ToString();
				string funName2 = (string)data2[1];
				object[] parm2 = (object[])data2[4];
				bool flag39 = !this.NetListCreate.ContainsKey(netsid2);
				NetLiving netObj;
				if (flag39)
				{
					string typeString2 = (string)data2[5];
					bool flag40 = typeString2 == "HeroPlayer";
					if (flag40)
					{
						typeString2 = "GamePlayer";
						BeHeroClass = true;
					}
					else
					{
						BeHeroClass = false;
					}
					Type type2 = Type.GetType(typeString2);
					NetLiving netliv3 = (NetLiving)Activator.CreateInstance(type2);
					netliv3.setNetid(netid);
					netliv3.isCopy = true;
					bool flag41 = parentNetid == netid;
					if (flag41)
					{
						netliv3.filmMainLayer = true;
					}
					netliv3.OnCreate();
					netliv3.FpsOrTps = -1;
					this.NetListCreate.Add(netsid2, netliv3);
					bool flag42 = !BeHeroClass;
					if (flag42)
					{
						this.RunFunction(netliv3, funName2, parm2);
					}
					else
					{
						data2 = (ArrayList)parm2[1];
						((GamePlayer)netliv3).RemoteCreate(data2);
					}
					netObj = netliv3;
					typeString2 = null;
					type2 = null;
					netliv3 = null;
				}
				else
				{
					netObj = (NetLiving)this.NetListCreate[netsid2];
					bool flag43 = funName2 == "OnDeath" && netObj == CameraControl.Instance.camerHero;
					if (flag43)
					{
						netObj.changeCameraState(CameraState.ThirdState, null, string.Empty);
					}
					this.RunFunction(netObj, funName2, parm2);
				}
				bool flag44 = _filmParentNpcOrPlayerID == netid;
				if (flag44)
				{
					bool flag45 = funName2.Contains("PlayerSelect");
					if (flag45)
					{
						lastcameraId = -1;
					}
					bool flag46 = lastcameraId != netid;
					if (flag46)
					{
						lastcameraId = -1;
						bool flag47 = CameraControl.Instance.camerHero != null && netObj.netobject_type == 2;
						if (flag47)
						{
							CameraControl.Instance.camerHero.FpsOrTps = 2;
							CameraControl.Instance.camerHero.ChangeFpsTpsState();
						}
					}
					bool flag48 = CameraControl.Instance.camerHero.getNetid() != lastcameraId;
					if (flag48)
					{
						bool flag49 = funName2.Contains("PlayerSelect");
						if (flag49)
						{
							netObj.FpsOrTps = -1;
						}
						filmChangeNet = netObj;
					}
				}
				bool flag50 = filmChangeNet != null;
				if (flag50)
				{
					while (!filmChangeNet.playerHaveCreate)
					{
						yield return 0f;
					}
					bool flag51 = this.nowDisableTopNameNetLiving != null;
					if (flag51)
					{
						this.nowDisableTopNameNetLiving.SetGameUiSignObj(true);
					}
					CCameraChange.Instance.playFilmByPlayer(filmChangeNet);
					filmChangeNet.SetGameUiSignObj(false);
					this.nowDisableTopNameNetLiving = filmChangeNet;
					lastcameraId = CameraControl.Instance.camerHero.getNetid();
					filmChangeNet = null;
				}
				data2 = null;
				netsid2 = null;
				funName2 = null;
				parm2 = null;
				netObj = null;
			}
		}
		bool flag52 = !fGameManager.Heroplayer.canControl || fGameManager.GetInstance().nNowScnID == 0 || this.breakFilm;
		if (flag52)
		{
			this.FilmFinish(true, 0);
			yield break;
		}
		while (Time.realtimeSinceStartup < this.endplayDateTime && fGameManager.Heroplayer.canControl && fGameManager.GetInstance().nNowScnID != 0 && !this.breakFilm)
		{
			yield return new WaitForSeconds(0.1f);
		}
		this.FilmFinish(false, 0);
		yield break;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001FE14 File Offset: 0x0001E014
	public void CheckFilmNetLivingState(NetLiving netliv)
	{
		bool flag = !netliv.isCopy && this.playing;
		if (flag)
		{
			netliv.renderBianSe.SetFilmMaterial(true);
			netliv.heroBase.SetFilmMaterial(true);
			netliv.publicInfo.SetAllColliderActive(false);
			netliv.SetGameUiSignObj(false);
			this.NetListDisable.Add(netliv);
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001FE78 File Offset: 0x0001E078
	public void FilmFinish(bool interrupt = false, int type = 0)
	{
		base.StopAllCoroutines();
		foreach (NetObj netObj in this.NetListCreate.Values)
		{
			if (netObj != null)
			{
				netObj.OnNetDestroy();
			}
		}
		for (int i = 0; i < this.NetListDisable.Count; i++)
		{
			NetLiving netLiving = (NetLiving)this.NetListDisable[i];
			bool flag = netLiving == null;
			if (!flag)
			{
				netLiving.renderBianSe.SetFilmMaterial(false);
				netLiving.heroBase.SetFilmMaterial(false);
				netLiving.publicInfo.SetAllColliderActive(true);
				netLiving.SetGameUiSignObj(true);
				bool flag2 = netLiving.userModelTps != null && fGameManager.Heroplayer != null && netLiving.getNetid() != fGameManager.Heroplayer.getNetid();
				if (flag2)
				{
					CHeroWeapon component = netLiving.userModelTps.GetComponent<CHeroWeapon>();
					bool flag3 = component != null;
					if (flag3)
					{
						component.SetWeaponParts(false);
					}
				}
			}
		}
		bool flag4 = fGameManager.Heroplayer != null;
		if (flag4)
		{
			CCameraChange.Instance.overFilmByPlayer(interrupt, type);
		}
		bool flag5 = this.removeFilmFrameNumber > 0;
		if (flag5)
		{
			this._film.RemoveRange(0, this.removeFilmFrameNumber);
			this.removeFilmFrameNumber = 0;
		}
		this.playing = false;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00020000 File Offset: 0x0001E200
	private void RunFunction(NetObj netObj, string funName, object[] parm)
	{
		bool flag = netObj == null;
		if (flag)
		{
			Debug.Log("netObj is null ");
		}
		else
		{
			Type type = netObj.GetType();
			Type[] array = this.conParaType(parm);
			MethodInfo methodInfo = (array != null) ? type.GetMethod(funName, array) : type.GetMethod(funName);
			if (methodInfo != null)
			{
				methodInfo.Invoke(netObj, parm);
			}
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00020058 File Offset: 0x0001E258
	private Type[] conParaType(object[] ali)
	{
		bool flag = ali == null;
		Type[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Type[] array = new Type[ali.Length];
			for (int i = 0; i < ali.Length; i++)
			{
				array[i] = ali[i].GetType();
			}
			result = array;
		}
		return result;
	}

	// Token: 0x04000321 RID: 801
	private List<string> animatorStatSynchronization = new List<string>
	{
		"onwall",
		"fly",
		"onair",
		"onground"
	};

	// Token: 0x04000322 RID: 802
	public bool isopen = true;

	// Token: 0x04000323 RID: 803
	private ArrayList _film = new ArrayList();

	// Token: 0x04000324 RID: 804
	private Dictionary<int, ArrayList> _filmCreateObj = new Dictionary<int, ArrayList>();

	// Token: 0x04000325 RID: 805
	private ArrayList _filmCopy;

	// Token: 0x04000326 RID: 806
	private Dictionary<int, ArrayList> _filmCreateObjCopy;

	// Token: 0x04000327 RID: 807
	private ArrayList NetListDisable;

	// Token: 0x04000328 RID: 808
	public Dictionary<string, NetObj> NetListCreate;

	// Token: 0x04000329 RID: 809
	public bool breakFilm;

	// Token: 0x0400032A RID: 810
	public bool playing;

	// Token: 0x0400032B RID: 811
	public int playerForce = -1;

	// Token: 0x0400032C RID: 812
	private int removeFilmFrameNumber;

	// Token: 0x0400032D RID: 813
	private float fileBeginTime = 8f;

	// Token: 0x0400032E RID: 814
	private float fileLongTime = 5.5f;

	// Token: 0x0400032F RID: 815
	public int filmnetid = -1;

	// Token: 0x04000330 RID: 816
	public int filmparentnetid = -1;

	// Token: 0x04000331 RID: 817
	public int filmParentNpcOrPlayerID = -1;

	// Token: 0x04000332 RID: 818
	private float beginplayDateTime;

	// Token: 0x04000333 RID: 819
	private float endplayDateTime;

	// Token: 0x04000334 RID: 820
	private static CFilmManager _instance;

	// Token: 0x04000335 RID: 821
	private NetLiving nowDisableTopNameNetLiving;
}
