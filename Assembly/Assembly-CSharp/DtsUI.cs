using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class DtsUI : Window
{
	// Token: 0x06000950 RID: 2384 RVA: 0x000506B8 File Offset: 0x0004E8B8
	protected override void Awake()
	{
		base.Awake();
		this.oCreateList = new GameObject("mapcreatelist");
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x000506D2 File Offset: 0x0004E8D2
	private void Start()
	{
		this.OnStartCreate();
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x000506DC File Offset: 0x0004E8DC
	private void OnStartCreate()
	{
		Vector3 zero = Vector3.zero;
		foreach (int num in this.szAllItemPos.Keys)
		{
			string text = this.szAllItemPos[num].ToString();
			bool flag = text == string.Empty;
			if (!flag)
			{
				string[] array = text.Split(new char[]
				{
					'|'
				});
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					bool flag2 = !(array[i] == string.Empty);
					if (flag2)
					{
						string[] array2 = array[i].Split(new char[]
						{
							','
						});
						bool flag3 = array2.Length >= 1;
						if (flag3)
						{
							zero.x = float.Parse(array2[0]) / 100f;
							zero.y = float.Parse(array2[1]) / 100f;
							zero.z = float.Parse(array2[2]) / 100f;
							this.CreateItemObj(num, zero);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0005083C File Offset: 0x0004EA3C
	private void Update()
	{
		bool flag = false;
		bool mouseButton = Input.GetMouseButton(2);
		if (mouseButton)
		{
			flag = true;
		}
		bool flag2 = Input.GetKeyDown(KeyCode.Alpha0) && flag;
		if (flag2)
		{
			this.OnAllPosInPut();
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Alpha1);
		if (keyDown)
		{
			bool flag3 = flag;
			if (flag3)
			{
				bool flag4 = this.szAllItemPos[1].ToString() != string.Empty;
				if (flag4)
				{
					Debug.Log(string.Concat(new object[]
					{
						"<color='#ff0000'>",
						this.szIcoNameData[1],
						" : ",
						this.szAllItemPos[1].ToString(),
						"</color>"
					}));
				}
			}
			else
			{
				this.OnCreateItem(1);
			}
		}
		else
		{
			bool keyDown2 = Input.GetKeyDown(KeyCode.E);
			if (keyDown2)
			{
				bool flag5 = flag;
				if (flag5)
				{
					bool flag6 = this.szAllItemPos[2].ToString() != string.Empty;
					if (flag6)
					{
						Debug.Log(string.Concat(new object[]
						{
							"<color='#ff0000'>",
							this.szIcoNameData[2],
							" : ",
							this.szAllItemPos[2].ToString(),
							"</color>"
						}));
					}
				}
				else
				{
					this.OnCreateItem(2);
				}
			}
			else
			{
				bool keyDown3 = Input.GetKeyDown(KeyCode.LeftShift);
				if (keyDown3)
				{
					bool flag7 = flag;
					if (flag7)
					{
						bool flag8 = this.szAllItemPos[3].ToString() != string.Empty;
						if (flag8)
						{
							Debug.Log(string.Concat(new object[]
							{
								"<color='#ff0000'>",
								this.szIcoNameData[3],
								" : ",
								this.szAllItemPos[3].ToString(),
								"</color>"
							}));
						}
					}
					else
					{
						this.OnCreateItem(3);
					}
				}
				else
				{
					bool keyDown4 = Input.GetKeyDown(KeyCode.Alpha2);
					if (keyDown4)
					{
						bool flag9 = flag;
						if (flag9)
						{
							bool flag10 = this.szAllItemPos[4].ToString() != string.Empty;
							if (flag10)
							{
								Debug.Log(string.Concat(new object[]
								{
									"<color='#ff0000'>",
									this.szIcoNameData[4],
									" : ",
									this.szAllItemPos[4].ToString(),
									"</color>"
								}));
							}
						}
						else
						{
							this.OnCreateItem(4);
						}
					}
					else
					{
						bool keyDown5 = Input.GetKeyDown(KeyCode.Alpha3);
						if (keyDown5)
						{
							bool flag11 = flag;
							if (flag11)
							{
								bool flag12 = this.szAllItemPos[5].ToString() != string.Empty;
								if (flag12)
								{
									Debug.Log(string.Concat(new object[]
									{
										"<color='#ff0000'>",
										this.szIcoNameData[5],
										" : ",
										this.szAllItemPos[5].ToString(),
										"</color>"
									}));
								}
							}
							else
							{
								this.OnCreateItem(5);
							}
						}
						else
						{
							bool keyDown6 = Input.GetKeyDown(KeyCode.Alpha4);
							if (keyDown6)
							{
								bool flag13 = flag;
								if (flag13)
								{
									bool flag14 = this.szAllItemPos[6].ToString() != string.Empty;
									if (flag14)
									{
										Debug.Log(string.Concat(new object[]
										{
											"<color='#ff0000'>",
											this.szIcoNameData[6],
											" : ",
											this.szAllItemPos[6].ToString(),
											"</color>"
										}));
									}
								}
								else
								{
									this.OnCreateItem(6);
								}
							}
							else
							{
								bool keyDown7 = Input.GetKeyDown(KeyCode.Alpha5);
								if (keyDown7)
								{
									bool flag15 = flag;
									if (flag15)
									{
										bool flag16 = this.szAllItemPos[7].ToString() != string.Empty;
										if (flag16)
										{
											Debug.Log(string.Concat(new object[]
											{
												"<color='#ff0000'>",
												this.szIcoNameData[7],
												" : ",
												this.szAllItemPos[7].ToString(),
												"</color>"
											}));
										}
									}
									else
									{
										this.OnCreateItem(7);
									}
								}
								else
								{
									bool keyDown8 = Input.GetKeyDown(KeyCode.Alpha7);
									if (keyDown8)
									{
										bool flag17 = flag;
										if (flag17)
										{
											bool flag18 = this.szAllItemPos[8].ToString() != string.Empty;
											if (flag18)
											{
												Debug.Log(string.Concat(new object[]
												{
													"<color='#ff0000'>",
													this.szIcoNameData[8],
													" : ",
													this.szAllItemPos[8].ToString(),
													"</color>"
												}));
											}
										}
										else
										{
											this.OnCreateItem(8);
										}
									}
									else
									{
										bool flag19 = !Input.GetKeyDown(KeyCode.Alpha8);
										if (!flag19)
										{
											bool flag20 = flag;
											if (flag20)
											{
												bool flag21 = this.szAllItemPos[9].ToString() != string.Empty;
												if (flag21)
												{
													Debug.Log(string.Concat(new object[]
													{
														"<color='#ff0000'>",
														this.szIcoNameData[9],
														" : ",
														this.szAllItemPos[9].ToString(),
														"</color>"
													}));
												}
											}
											else
											{
												this.OnCreateItem(9);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00050DEC File Offset: 0x0004EFEC
	private string getPosString(Vector3 ve)
	{
		return string.Concat(new string[]
		{
			((int)(ve.x * 100f)).ToString(),
			",",
			((int)(ve.y * 100f)).ToString(),
			",",
			((int)(ve.z * 100f)).ToString(),
			",0|"
		});
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00050E6C File Offset: 0x0004F06C
	private void OnCreateItem(int nType)
	{
		string str = this.szIcoNameData[nType].ToString();
		Vector3 nowPlayerPos = this.GetNowPlayerPos();
		string posString = this.getPosString(nowPlayerPos);
		int num = this.m_szAllPosVe.IndexOf(posString);
		bool flag = num != -1;
		if (flag)
		{
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.AddGameMsg("该坐标位置已经使用了");
			}
		}
		else
		{
			bool flag3 = this.szAllItemPos.ContainsKey(nType);
			if (flag3)
			{
				this.CreateItemObj(nType, nowPlayerPos);
				string text = this.szAllItemPos[nType].ToString();
				text += posString;
				this.szAllItemPos[nType] = text;
				bool flag4 = gameuiCon.inst != null;
				if (flag4)
				{
					gameuiCon.inst.mainMsgUI.AddGameMsg("创建了一个[<color='#ff0000'>" + str + "</color>]坐标");
				}
			}
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00050F60 File Offset: 0x0004F160
	private void CreateItemObj(int nType, Vector3 vepos)
	{
		string text = this.szIcoNameData[nType].ToString();
		string text2 = this.szIcoColorData[nType].ToString();
		string posString = this.getPosString(vepos);
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		gameObject.transform.position = vepos;
		gameObject.GetComponent<BoxCollider>().enabled = true;
		gameObject.GetComponent<BoxCollider>().isTrigger = true;
		CheckCreateItemCon checkCreateItemCon = gameObject.AddComponent<CheckCreateItemCon>();
		checkCreateItemCon.SetValue(nType, text, posString, this);
		gameObject.transform.SetParent(this.oCreateList.transform);
		text = string.Concat(new string[]
		{
			"<color='#",
			text2,
			"'>",
			text,
			"</color>"
		});
		this.LoadDoorHeadUi(gameObject, text);
		this.m_szAllPosVe.Add(posString);
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00051038 File Offset: 0x0004F238
	public void OnDelPos(int nType, string spos)
	{
		bool flag = this.szAllItemPos.ContainsKey(nType);
		if (flag)
		{
			string text = this.szAllItemPos[nType].ToString();
			text = text.Replace(spos, string.Empty);
			this.szAllItemPos[nType] = text;
		}
		int num = this.m_szAllPosVe.IndexOf(spos);
		bool flag2 = num != -1;
		if (flag2)
		{
			this.m_szAllPosVe.Remove(spos);
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x000510AC File Offset: 0x0004F2AC
	private Vector3 GetNowPlayerPos()
	{
		Vector3 zero = Vector3.zero;
		NetLiving player = fGameManager.GetInstance().getPlayer();
		zero = new Vector3(player.playerCommon.pos.x, player.playerCommon.pos.y, player.playerCommon.pos.z);
		float upPos = this.GetUpPos(player.thisModel.transform.position);
		bool flag = upPos != 0f;
		if (flag)
		{
			zero.y = upPos;
		}
		zero.y += 0.5f;
		return zero;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0005114C File Offset: 0x0004F34C
	private float GetUpPos(Vector3 ve)
	{
		float result = 0f;
		Ray ray = new Ray(ve, -Vector3.up * 10f);
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(ray, out raycastHit, 10f, layerMask);
		if (flag)
		{
			result = raycastHit.point.y;
		}
		return result;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000511B8 File Offset: 0x0004F3B8
	private void LoadDoorHeadUi(GameObject oCreateObj, string sName)
	{
		string url = string.Format("EffectRes/{0}.ef", "newplayerui");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), new ArrayList
			{
				oCreateObj,
				sName
			});
		}
		else
		{
			this.loadReayPrefab(resWWW, new ArrayList
			{
				oCreateObj,
				sName
			});
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00051230 File Offset: 0x0004F430
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)sStyle;
			GameObject gameObject = (GameObject)arrayList[0];
			GameObject original = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
			GameObject3DName gameObject3DName = gameObject2.AddComponent<GameObject3DName>();
			string sName = "<color='#00aedd'>" + arrayList[1] + "</color>";
			gameObject3DName.setUI(sName, new Vector3(0f, 0.7f, 0f));
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00051328 File Offset: 0x0004F528
	public void OnAllPosInPut()
	{
		Debug.Log("<color='#00FF00'>全部导出</color>");
		foreach (int num in this.szAllItemPos.Keys)
		{
			bool flag = !(this.szAllItemPos[num] == string.Empty);
			if (flag)
			{
				Debug.Log(string.Concat(new object[]
				{
					"<color='#ff0000'>",
					this.szIcoNameData[num],
					":",
					this.szAllItemPos[num],
					"</color>"
				}));
			}
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000513F0 File Offset: 0x0004F5F0
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 1000f, 50f), "<size=20>按下<color=red>数字键1</color> - 创建【英雄】</size>");
		GUI.Label(new Rect(10f, 40f, 1000f, 50f), "<size=20>按下<color=red>字母键E</color> - 创建【技能E】</size>");
		GUI.Label(new Rect(10f, 70f, 1000f, 50f), "<size=20>按下<color=red>左SHIFT</color> - 创建【技能SHIFT】</size>");
		GUI.Label(new Rect(10f, 100f, 1000f, 50f), "<size=20>按下<color=red>数字键2</color> - 创建【技能右键】</size>");
		GUI.Label(new Rect(10f, 130f, 1000f, 50f), "<size=20>按下<color=red>数字键3</color> - 创建【血包】</size>");
		GUI.Label(new Rect(10f, 160f, 1000f, 50f), "<size=20>按下<color=red>数字键4</color> - 创建【攻击球】</size>");
		GUI.Label(new Rect(10f, 190f, 1000f, 50f), "<size=20>按下<color=red>数字键5</color> - 创建【防御球】</size>");
		GUI.Label(new Rect(10f, 220f, 1000f, 50f), "<size=20>按下<color=red>数字键7</color> - 创建【彩蛋】 </size>");
		GUI.Label(new Rect(10f, 250f, 1000f, 50f), "<size=20>按下<color=red>数字键8</color> - 创建【充能器】</size>");
		GUI.Label(new Rect(10f, 280f, 1000f, 50f), "<size=20>按住鼠标<color=red>中键</color>再按下<color=red>数字键0</color>全部导出</size>");
		GUI.Label(new Rect(10f, 310f, 1000f, 50f), "<size=20>按住鼠标<color=red>右键</color>玩家进入<color=red>创建物体中</color>删除创建物</size>");
		GUI.Label(new Rect(10f, 340f, 1000f, 50f), "<size=20>按住鼠标<color=red>中键</color>再按下<color=red>相应创建快捷键</color>按类型导出</size>");
		GUI.Label(new Rect(10f, 370f, 1000f, 50f), "<size=20><color=red>可以把坐标字符串粘贴到脚本中自动创建填充到场景中</color></size>");
	}

	// Token: 0x040006EC RID: 1772
	private GameObject oCreateList;

	// Token: 0x040006ED RID: 1773
	private ArrayList m_szAllPosVe = new ArrayList();

	// Token: 0x040006EE RID: 1774
	private ArrayList szIcoNameData = new ArrayList
	{
		string.Empty,
		"英雄--1",
		"技能E--2",
		"技能SHIFT--2",
		"技能右键--2",
		"血包--3",
		"攻击球--4",
		"防御球--5",
		"彩蛋--7",
		"充能器--8"
	};

	// Token: 0x040006EF RID: 1775
	private ArrayList szIcoColorData = new ArrayList
	{
		string.Empty,
		"ff0000",
		"00EAFF",
		"00FF8B",
		"002CFF",
		"FF7900",
		"FF00E3",
		"FFF800",
		"FF00A4",
		"00FF17"
	};

	// Token: 0x040006F0 RID: 1776
	private Dictionary<int, string> szAllItemPos = new Dictionary<int, string>
	{
		{
			0,
			string.Empty
		},
		{
			1,
			string.Empty
		},
		{
			2,
			string.Empty
		},
		{
			3,
			string.Empty
		},
		{
			4,
			string.Empty
		},
		{
			5,
			string.Empty
		},
		{
			6,
			string.Empty
		},
		{
			7,
			string.Empty
		},
		{
			8,
			string.Empty
		},
		{
			9,
			string.Empty
		}
	};
}
