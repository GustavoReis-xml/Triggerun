using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000137 RID: 311
public static class Fun
{
	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00072E64 File Offset: 0x00071064
	public static DateTime Now
	{
		get
		{
			bool playing = CFilmManager.Instance.playing;
			DateTime result;
			if (playing)
			{
				result = CFilmManager.Instance.filmTime;
			}
			else
			{
				result = FunManager.Instance.serverTime;
			}
			return result;
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00072E9C File Offset: 0x0007109C
	public static IEnumerator SendHttpRequest_Post(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		bool flag = www.error != null;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(18009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			yield break;
		}
		bool flag2 = www.text == "1";
		if (flag2)
		{
			bool flag3 = RegCon.inst != null;
			if (flag3)
			{
				RegCon.inst.Close();
			}
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(18008, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		bool flag4 = www.text == "-1";
		if (flag4)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(18007, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		yield break;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00072EAB File Offset: 0x000710AB
	public static IEnumerator shenfenzheng(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		bool flag = www.error != null;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(18010, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			yield break;
		}
		Debug.Log(www.text + "     注册");
		bool flag2 = www.text == "1";
		if (flag2)
		{
			bool flag3 = RegCon.inst != null;
			if (flag3)
			{
				RegCon.inst.SendReg();
			}
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(18010, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		yield break;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00072EBA File Offset: 0x000710BA
	public static IEnumerator FCMCheckIDCard(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		bool flag = www.error != null;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = www.text == "1";
			if (flag2)
			{
				bool flag3 = !(FCMUICon.inst != null);
				if (flag3)
				{
				}
			}
			else
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
		}
		yield break;
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00072EC9 File Offset: 0x000710C9
	public static IEnumerator TecentFCMCheck(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		bool flag = www.error != null;
		if (flag)
		{
			Debug.Log("error is :" + www.error);
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			yield break;
		}
		Debug.Log(www.text + "     注册");
		bool flag2 = !string.IsNullOrEmpty(www.text) && www.text.Split(new char[]
		{
			'|'
		}).Length == 2;
		if (flag2)
		{
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			bool flag3 = FCMUICon.inst != null;
			if (flag3)
			{
				int num = int.Parse(array[0]);
				bool flag4 = num == 1;
				if (flag4)
				{
					int ncheck = (int.Parse(array[1]) != 1) ? 1 : 0;
					FCMUICon.inst.SendFcmInfo(ncheck);
				}
				else
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
			}
			array = null;
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(16005, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		Debug.Log("request result :" + www.text);
		yield break;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00072ED8 File Offset: 0x000710D8
	public static IEnumerator SendHttpRequest_Post_NoRequest(string url)
	{
		yield return new WWW(url);
		yield break;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00072EE8 File Offset: 0x000710E8
	public static int m_nIsTopUp()
	{
		return fGameManager.m_nIsTopUp;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00072F00 File Offset: 0x00071100
	public static int GetIsCharge()
	{
		return fGameManager.m_nIsCharge;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00072F18 File Offset: 0x00071118
	public static string Md5Sum(string input)
	{
		MD5 md = MD5.Create();
		byte[] bytes = Encoding.UTF8.GetBytes(input);
		byte[] array = md.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += array[i].ToString("x2");
		}
		return text.ToString();
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00072F88 File Offset: 0x00071188
	public static string EncodeBase64(string source)
	{
		string text = string.Empty;
		byte[] bytes = Encoding.UTF8.GetBytes(source);
		string result;
		try
		{
			text = Convert.ToBase64String(bytes);
			text = text.Replace("/", "|");
			result = text.Replace("+", "_");
		}
		catch
		{
			result = source;
		}
		return result;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00072FEC File Offset: 0x000711EC
	public static string DecodeBase64(string result)
	{
		result = result.Replace("|", "/");
		result = result.Replace("_", "+");
		string empty = string.Empty;
		string result2;
		try
		{
			byte[] bytes = Convert.FromBase64String(result);
			result2 = Encoding.UTF8.GetString(bytes);
		}
		catch
		{
			result2 = result;
		}
		return result2;
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00073054 File Offset: 0x00071254
	public static Color StrToColor(string hexStr, float alpha)
	{
		int num = int.Parse(hexStr, NumberStyles.AllowHexSpecifier);
		Color color = new Color((float)(num >> 16 & 255), (float)(num >> 8 & 255), (float)(num & 255));
		color /= 255f;
		color.a = alpha;
		return color;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x000730AB File Offset: 0x000712AB
	public static void outlog(int _type, object _msg, UnityEngine.Object obj = null)
	{
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x000730AE File Offset: 0x000712AE
	public static void SocketLog(string sLog)
	{
		Debug.Log(sLog);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x000730B8 File Offset: 0x000712B8
	public static DateTime GetServerDateForEachTime()
	{
		return FunManager.Instance.serverTimeNow;
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x000730D4 File Offset: 0x000712D4
	public static DateTime GetNow(NetLiving net)
	{
		bool isCopy = net.isCopy;
		DateTime result;
		if (isCopy)
		{
			result = CFilmManager.Instance.filmTime;
		}
		else
		{
			result = FunManager.Instance.serverTime;
		}
		return result;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00073108 File Offset: 0x00071308
	public static string ClientDatatime()
	{
		return DateTime.Now.ToString();
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00073128 File Offset: 0x00071328
	public static DateTime GetServerDate()
	{
		bool flag = DateTime.Equals(FunManager.Instance.serverTime, DateTime.MinValue);
		DateTime result;
		if (flag)
		{
			result = DateTime.Now;
		}
		else
		{
			result = FunManager.Instance.serverTime;
		}
		return result;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00073165 File Offset: 0x00071365
	public static void Log(object _msg, UnityEngine.Object obj = null)
	{
		Debug.Log(_msg);
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0007316F File Offset: 0x0007136F
	public static void LogError(object _msg, UnityEngine.Object obj = null)
	{
		Debug.LogError(_msg, obj);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0007317C File Offset: 0x0007137C
	public static Transform LikeGameObjectChild(GameObject ob, string sFindName, bool findhide = false)
	{
		Transform[] componentsInChildren = ob.GetComponentsInChildren<Transform>(findhide);
		foreach (Transform transform in componentsInChildren)
		{
			bool flag = transform.name.Contains(sFindName);
			if (flag)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x000731C8 File Offset: 0x000713C8
	public static Transform FindGameObjectChild(GameObject ob, string sFindName, bool findhide = false)
	{
		Transform[] componentsInChildren = ob.GetComponentsInChildren<Transform>(findhide);
		foreach (Transform transform in componentsInChildren)
		{
			bool flag = transform.name == sFindName;
			if (flag)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00073214 File Offset: 0x00071414
	public static GameObject[] FindGameObjectsInChild(GameObject ob, string sFindName, bool findhide = false)
	{
		Transform[] componentsInChildren = ob.GetComponentsInChildren<Transform>(findhide);
		bool flag = componentsInChildren.Length == 0;
		GameObject[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			ArrayList arrayList = new ArrayList();
			foreach (Transform transform in componentsInChildren)
			{
				bool flag2 = transform.name == sFindName;
				if (flag2)
				{
					arrayList.Add(transform.gameObject);
				}
			}
			GameObject[] array2 = new GameObject[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array2[j] = (GameObject)arrayList[j];
			}
			result = array2;
		}
		return result;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x000732C8 File Offset: 0x000714C8
	public static Vector3 ServerToClientPos(int x, int y, int z)
	{
		return new Vector3((float)x * 0.01f, (float)y * 0.01f, (float)z * 0.01f);
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000732F7 File Offset: 0x000714F7
	public static void Reset(GameObject go)
	{
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0007331C File Offset: 0x0007151C
	public static string sencondsToHMS_CXP(int seconds)
	{
		int num = seconds / 3600;
		int num2 = seconds % 3600 / 60;
		int num3 = seconds % 60;
		string empty = string.Empty;
		bool flag = num >= 1;
		string result;
		if (flag)
		{
			result = string.Concat(new string[]
			{
				num.ToString().PadLeft(2, '0'),
				":",
				num2.ToString().PadLeft(2, '0'),
				":",
				num3.ToString().PadLeft(2, '0')
			});
		}
		else
		{
			result = num2.ToString().PadLeft(2, '0') + ":" + num3.ToString().PadLeft(2, '0');
		}
		return result;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x000733DC File Offset: 0x000715DC
	public static string MoneyToShow(int nMoney)
	{
		string empty = string.Empty;
		bool flag = WebParam.client_language != "zh";
		string result;
		if (flag)
		{
			result = Fun.MoneyToShow_K(nMoney);
		}
		else
		{
			bool flag2 = nMoney < 10000;
			if (flag2)
			{
				result = nMoney.ToString();
			}
			else
			{
				bool flag3 = nMoney >= 10000 && nMoney < 1000000;
				if (flag3)
				{
					int num = nMoney / 10000;
					bool flag4 = nMoney % 10000 == 0;
					if (flag4)
					{
						result = Language.GetLang(9010, new object[]
						{
							(nMoney / 10000).ToString()
						});
					}
					else
					{
						bool flag5 = ((float)nMoney / 10000f - (float)num).ToString().Length <= 3;
						if (flag5)
						{
							float num2 = (float)nMoney / 10000f;
							result = Language.GetLang(9010, new object[]
							{
								((float)Mathf.FloorToInt(num2 * 10f) / 10f).ToString()
							});
						}
						else
						{
							float num2 = (float)nMoney / 10000f;
							result = Language.GetLang(9010, new object[]
							{
								((float)Mathf.FloorToInt(num2 * 100f) / 100f).ToString()
							});
						}
					}
				}
				else
				{
					bool flag6 = nMoney >= 1000000 && nMoney < 10000000;
					if (flag6)
					{
						bool flag7 = nMoney % 10000 == 0;
						if (flag7)
						{
							result = Language.GetLang(9010, new object[]
							{
								(nMoney / 10000).ToString()
							});
						}
						else
						{
							float num3 = (float)nMoney / 10000f;
							result = Language.GetLang(9010, new object[]
							{
								((float)Mathf.FloorToInt(num3 * 10f) / 10f).ToString()
							});
						}
					}
					else
					{
						result = Language.GetLang(9010, new object[]
						{
							(nMoney / 10000).ToString()
						});
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x000735E8 File Offset: 0x000717E8
	private static string MoneyToShow_K(int nMoney)
	{
		string empty = string.Empty;
		bool flag = nMoney < 1000;
		string result;
		if (flag)
		{
			result = nMoney.ToString();
		}
		else
		{
			bool flag2 = nMoney >= 1000 && nMoney < 1000000;
			if (flag2)
			{
				result = Language.GetLang(9010, new object[]
				{
					(nMoney / 1000).ToString()
				});
			}
			else
			{
				result = Language.GetLang(9011, new object[]
				{
					(nMoney / 1000000).ToString()
				});
			}
		}
		return result;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00073678 File Offset: 0x00071878
	public static string DecimalCutZero(float fnum, string sformat)
	{
		string text = string.Empty;
		text = fnum.ToString(sformat);
		int num = text.Length - 1;
		for (int i = num; i > -1; i--)
		{
			bool flag = text.EndsWith(".");
			if (flag)
			{
				text = text.Remove(text.Length - 1, 1);
				break;
			}
			bool flag2 = text.EndsWith("0");
			if (flag2)
			{
				text = text.Remove(text.Length - 1, 1);
			}
		}
		return text;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00073700 File Offset: 0x00071900
	public static string GetHintS(int nid)
	{
		HintData hintData = fGameManager.mloadConfig.AllHintData[nid];
		bool flag = hintData == null;
		string result;
		if (flag)
		{
			result = "Error_Hint:" + nid.ToString();
		}
		else
		{
			result = hintData.sMsg;
		}
		return result;
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00073748 File Offset: 0x00071948
	public static Transform Get_Child_Transform(GameObject _Parent_Object, string _Child_Object_Name)
	{
		Transform transform = null;
		foreach (object obj in _Parent_Object.transform)
		{
			Transform transform2 = (Transform)obj;
			transform = ((!(transform2.gameObject.name == _Child_Object_Name)) ? Fun.Get_Child_Transform(transform2.gameObject, _Child_Object_Name) : transform2);
			bool flag = transform != null;
			if (flag)
			{
				break;
			}
		}
		return transform;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x000737DC File Offset: 0x000719DC
	public static void Change_Layer(GameObject _Parent_Object, int _Value)
	{
		bool flag = !(_Parent_Object == null);
		if (flag)
		{
			_Parent_Object.layer = _Value;
			foreach (Collider collider in _Parent_Object.GetComponentsInChildren<Collider>())
			{
				collider.gameObject.layer = _Value;
			}
		}
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00073838 File Offset: 0x00071A38
	public static void Change_Tag(GameObject _Parent_Object, string _Value)
	{
		bool flag = !(_Parent_Object == null);
		if (flag)
		{
			foreach (Collider collider in _Parent_Object.GetComponentsInChildren<Collider>())
			{
				collider.gameObject.tag = _Value;
			}
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0007388C File Offset: 0x00071A8C
	public static void Change_Collider(GameObject _Parent_Object, bool _State)
	{
		bool flag = !(_Parent_Object == null);
		if (flag)
		{
			foreach (Collider collider in _Parent_Object.GetComponentsInChildren<Collider>())
			{
				collider.enabled = _State;
			}
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x000738DC File Offset: 0x00071ADC
	public static NetLiving Get_NetLiving(int _Net_ID)
	{
		bool flag = _Net_ID == 0;
		NetLiving result;
		if (flag)
		{
			result = null;
		}
		else
		{
			NetObj netObj = CFilmManager.Instance.playing ? CFilmManager.Instance.GetFilmNetLiving(_Net_ID) : GameRoot.SceneSocket.unFsorket.GetNetObj(_Net_ID);
			bool flag2 = netObj == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = (NetLiving)netObj;
			}
		}
		return result;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00073940 File Offset: 0x00071B40
	public static T DeepCopy<T>(T RealObject)
	{
		T result;
		using (Stream stream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, RealObject);
			stream.Seek(0L, SeekOrigin.Begin);
			result = (T)((object)binaryFormatter.Deserialize(stream));
		}
		return result;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x000739A0 File Offset: 0x00071BA0
	public static void Load_Sound(NetLiving net, string _Value, GameObject _Parent_Object, bool _Play_State, string _Name)
	{
		bool flag = net.isCopy || !CFilmManager.Instance.playing;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			string text = "SoundRes/" + _Value + ".sds";
			arrayList.Add(_Parent_Object);
			arrayList.Add(_Play_State);
			arrayList.Add(_Name);
			WWW resWWW = GameRoot.GetResWWW(text);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				string url = text;
				fResourceManager.DownFinishDelegate callBackFun;
				if ((callBackFun = Fun.O_cache._State_0_Load_Sound_Complete) == null)
				{
					callBackFun = (Fun.O_cache._State_0_Load_Sound_Complete = new fResourceManager.DownFinishDelegate(Fun.Load_Sound_Complete));
				}
				GameRoot.LoadAsset(url, callBackFun, arrayList);
			}
			else
			{
				Fun.Load_Sound_Complete(resWWW, arrayList);
			}
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00073A48 File Offset: 0x00071C48
	public static void Load_Sound_Complete(WWW _WWW, object _Value)
	{
		bool flag = _WWW == null || !string.IsNullOrEmpty(_WWW.error);
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)_Value;
			GameObject gameObject = (GameObject)arrayList[0];
			bool flag2 = (bool)arrayList[1];
			string name = arrayList[2].ToString();
			GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(_WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]));
			gameObject2.name = name;
			AudioSource component = gameObject2.GetComponent<AudioSource>();
			bool flag3 = gameObject == null || component == null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
			else
			{
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
				component.playOnAwake = flag2;
				bool flag4 = flag2;
				if (flag4)
				{
					component.Play();
				}
				bool flag5 = flag2 && !component.loop;
				if (flag5)
				{
					Sound_Auto_Destory sound_Auto_Destory = gameObject2.AddComponent<Sound_Auto_Destory>();
					sound_Auto_Destory.Wait_Time = component.clip.length;
					sound_Auto_Destory.Wait_Destory();
				}
			}
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00073B98 File Offset: 0x00071D98
	public static void Load_Image(string _Url, Image _Image_Component, string _Resource_Name)
	{
		_Url = _Url + "/" + _Resource_Name + ".txt";
		WWW resWWW = GameRoot.GetResWWW(_Url);
		ArrayList arrayList = new ArrayList();
		arrayList.Add(_Image_Component);
		bool flag = resWWW == null;
		if (flag)
		{
			string url = _Url;
			fResourceManager.DownFinishDelegate callBackFun;
			if ((callBackFun = Fun.O_cache._State_1_Load_Image_Complete) == null)
			{
				callBackFun = (Fun.O_cache._State_1_Load_Image_Complete = new fResourceManager.DownFinishDelegate(Fun.Load_Image_Complete));
			}
			GameRoot.LoadAsset(url, callBackFun, arrayList);
		}
		else
		{
			Fun.Load_Image_Complete(resWWW, arrayList);
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00073C0C File Offset: 0x00071E0C
	public static void Load_Image_Complete(WWW www, object _Parameters)
	{
		ArrayList arrayList = (ArrayList)_Parameters;
		Image image = (Image)arrayList[0];
		bool flag = !(image == null);
		if (flag)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			www.LoadImageIntoTexture(texture2D);
			Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.zero);
			image.sprite = sprite;
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00073C88 File Offset: 0x00071E88
	public static void Load_Scene_Effect(string _Resource_Name, Vector3 _Posistion)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(_Posistion);
		arrayList.Add(0);
		string text = "EffectRes/" + _Resource_Name + ".ef";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			string url = text;
			fResourceManager.DownFinishDelegate callBackFun;
			if ((callBackFun = Fun.O_cache._State_2_Load_Scene_Effect_Complete) == null)
			{
				callBackFun = (Fun.O_cache._State_2_Load_Scene_Effect_Complete = new fResourceManager.DownFinishDelegate(Fun.Load_Scene_Effect_Complete));
			}
			GameRoot.LoadAsset(url, callBackFun, arrayList);
		}
		else
		{
			Fun.Load_Scene_Effect_Complete(resWWW, arrayList);
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00073D0C File Offset: 0x00071F0C
	public static void Load_Scene_Effect_Complete(WWW _WWW, object _Value)
	{
		ArrayList arrayList = new ArrayList();
		Vector3 localPosition = Vector3.zero;
		arrayList = (ArrayList)_Value;
		localPosition = (Vector3)arrayList[0];
		UnityEngine.Object original = _WWW.assetBundle.LoadAsset(_WWW.assetBundle.GetAllAssetNames()[0]);
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00073D94 File Offset: 0x00071F94
	public static ArrayList GetOtherPlayerQQData(string strqqdata)
	{
		ArrayList arrayList = new ArrayList
		{
			string.Empty,
			0,
			0,
			0,
			0
		};
		bool flag = strqqdata == string.Empty || strqqdata == "{}";
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			strqqdata = strqqdata.Replace("false", "0");
			strqqdata = strqqdata.Replace("true", "1");
			strqqdata = strqqdata.Replace("\\\"", "'");
			try
			{
				JsonData jsonData = JsonMapper.ToObject(strqqdata);
				bool flag2 = (string)jsonData["pf"] == "3366" || (string)jsonData["pf"] == "qqgame";
				if (flag2)
				{
					arrayList[0] = (string)jsonData["pf"];
					arrayList[1] = (int)jsonData["blue_vip_level"];
					arrayList[2] = (int)jsonData["is_blue_year_vip"];
					try
					{
						arrayList[3] = (int)jsonData["3366_grow_level"];
					}
					catch
					{
						arrayList[3] = 0;
					}
					arrayList[4] = (int)jsonData["is_super_blue_vip"];
					bool flag3 = (int)arrayList[4] > 0;
					if (flag3)
					{
						arrayList[4] = arrayList[1];
					}
				}
				else
				{
					bool flag4 = (string)jsonData["pf"] == "website" || (string)jsonData["pf"] == "qzone" || (string)jsonData["pf"] == "pengyou";
					if (flag4)
					{
						arrayList[0] = (string)jsonData["pf"];
						arrayList[1] = (int)jsonData["yellow_vip_level"];
						arrayList[2] = (int)jsonData["is_yellow_year_vip"];
						arrayList[3] = 0;
						arrayList[4] = (int)jsonData["is_yellow_high_vip"];
						bool flag5 = (int)arrayList[4] > 0;
						if (flag5)
						{
							arrayList[4] = arrayList[1];
						}
					}
				}
			}
			catch
			{
				Fun.outlog(2, "{=780013}qq{=780014}" + strqqdata, null);
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000740C8 File Offset: 0x000722C8
	public static int GetEndCDTime(string sTime, int nlong)
	{
		int num = 0;
		bool flag = sTime == string.Empty;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime d = DateTime.Parse(sTime).AddSeconds((double)nlong);
			result = (int)(d - Fun.GetServerDate()).TotalSeconds;
		}
		return result;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00074118 File Offset: 0x00072318
	public static int GetEndCDMilliseconds(string sTime, int nlong)
	{
		int num = 0;
		bool flag = sTime == string.Empty;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime d = DateTime.Parse(sTime).AddSeconds((double)nlong);
			result = (int)(d - Fun.GetServerDate()).TotalMilliseconds;
		}
		return result;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00074168 File Offset: 0x00072368
	public static int GetEndCDTime(string sEndTime)
	{
		int num = 0;
		bool flag = sEndTime == string.Empty;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime d = DateTime.Parse(sEndTime);
			result = (int)(d - Fun.GetServerDate()).TotalSeconds;
		}
		return result;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x000741AC File Offset: 0x000723AC
	public static string SecToFullTime(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			int num = sec / 86400;
			int num2 = sec % 86400 / 3600;
			int num3 = sec % 3600 / 60;
			int num4 = sec % 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				result = Language.GetLang(9001, new object[]
				{
					num.ToString()
				});
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					result = Language.GetLang(9002, new object[]
					{
						num2.ToString()
					});
				}
				else
				{
					bool flag4 = num3 >= 1;
					if (flag4)
					{
						result = Language.GetLang(9003, new object[]
						{
							num3.ToString()
						});
					}
					else
					{
						result = Language.GetLang(9004, new object[]
						{
							num4.ToString()
						});
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x000742A4 File Offset: 0x000724A4
	public static string SecToMaxTime(int sec, string smax = "d", string smin = "s")
	{
		string text = string.Empty;
		bool flag = sec < 0;
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			List<string> list = new List<string>
			{
				"d",
				"h",
				"m",
				"s"
			};
			List<int> list2 = new List<int>
			{
				sec / 86400,
				sec / 3600,
				sec / 60,
				sec
			};
			List<int> list3 = new List<int>
			{
				9008,
				9005,
				9006,
				9007
			};
			int num = list.IndexOf(smax);
			int num2 = list.IndexOf(smin) + 1;
			for (int i = num; i < num2; i++)
			{
				text = Language.GetLang(list3[i], new object[]
				{
					list2[i]
				});
				bool flag2 = list2[i] > 0;
				if (flag2)
				{
					break;
				}
			}
			result = text;
		}
		return result;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x000743E4 File Offset: 0x000725E4
	public static string SecToFullTimeShop(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			int num = sec / 86400;
			int num2 = sec % 86400 / 3600;
			int num3 = sec % 3600 / 60;
			int num4 = sec % 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				result = num.ToString() + "天";
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					result = num2.ToString() + "小时";
				}
				else
				{
					bool flag4 = num3 >= 1;
					if (flag4)
					{
						result = num3.ToString() + "分钟";
					}
					else
					{
						result = num4.ToString() + "秒";
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x000744B8 File Offset: 0x000726B8
	public static string GetQualColor(int nqual)
	{
		string result = string.Empty;
		switch (nqual)
		{
		case 1:
			result = "#bbbbbb";
			break;
		case 2:
			result = "#00ff00";
			break;
		case 3:
			result = "#00b4ff";
			break;
		case 4:
			result = "#ff00ff";
			break;
		case 5:
			result = "#f0c800";
			break;
		case 6:
			result = "#ff3333";
			break;
		}
		return result;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00074528 File Offset: 0x00072728
	public static string GetQualBgStr(int nqual)
	{
		string format = "mainui_qual_{0}";
		return string.Format(format, nqual);
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0007454C File Offset: 0x0007274C
	public static int Text_Length(string Text)
	{
		int num = 0;
		for (int i = 0; i < Text.Length; i++)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(Text.Substring(i, 1));
			num = ((bytes.Length <= 1) ? (num + 1) : (num + 2));
		}
		return num;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0007459C File Offset: 0x0007279C
	public static string GetShieldTxt(string content)
	{
		bool flag = WebParam.client_language != "zh";
		string result;
		if (flag)
		{
			result = Fun.GetShieldTxt_bx(content);
		}
		else
		{
			bool flag2 = string.IsNullOrEmpty(content);
			if (flag2)
			{
				result = string.Empty;
			}
			else
			{
				string text = content;
				string[] array = fGameManager.mloadConfig.GetbadCharArr();
				int i = 0;
				while (i < array.Length)
				{
					string text2 = string.Empty;
					bool flag3 = array[i].Length == 0;
					if (flag3)
					{
						i++;
					}
					else
					{
						bool flag4 = text.Contains(array[i]);
						if (flag4)
						{
							text2 = array[i];
						}
						bool flag5 = text2.Length <= 0;
						if (flag5)
						{
							i++;
						}
						else
						{
							for (int j = i + 1; j < array.Length; j++)
							{
								bool flag6 = array[j].Length != 0 && array[j].Contains(text2) && text.Contains(array[j]);
								if (flag6)
								{
									text2 = array[j];
								}
							}
							string text3 = string.Empty;
							for (int k = 0; k < text2.Length; k++)
							{
								text3 += "*";
							}
							text = text.Replace(text2, text3);
						}
					}
				}
				result = text;
			}
		}
		return result;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000746F4 File Offset: 0x000728F4
	private static string GetShieldTxt_bx(string content)
	{
		bool flag = string.IsNullOrEmpty(content);
		string result;
		if (flag)
		{
			result = string.Empty;
		}
		else
		{
			string text = content.ToLower();
			string[] array = fGameManager.mloadConfig.GetbadCharArr();
			int i = 0;
			while (i < array.Length)
			{
				string text2 = string.Empty;
				bool flag2 = array[i].Length == 0;
				if (flag2)
				{
					i++;
				}
				else
				{
					bool flag3 = text.Contains(array[i].ToLower());
					if (flag3)
					{
						text2 = array[i].ToLower();
					}
					bool flag4 = text2.Length <= 0;
					if (flag4)
					{
						i++;
					}
					else
					{
						for (int j = i + 1; j < array.Length; j++)
						{
							bool flag5 = array[j].Length != 0 && array[j].ToLower().Contains(text2) && text.Contains(array[j].ToLower());
							if (flag5)
							{
								text2 = array[j].ToLower();
							}
						}
						string text3 = string.Empty;
						for (int k = 0; k < text2.Length; k++)
						{
							text3 += "*";
						}
						text = text.Replace(text2, text3);
					}
				}
			}
			char[] array2 = content.ToCharArray();
			char[] array3 = text.ToCharArray();
			for (i = 0; i < array2.Length; i++)
			{
				bool flag6 = i < array3.Length && array3[i] == '*';
				if (flag6)
				{
					array2[i] = '*';
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(array2);
			content = stringBuilder.ToString();
			result = content;
		}
		return result;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000748A4 File Offset: 0x00072AA4
	public static int HaveShieldTxt(string content, int startidx)
	{
		bool flag = WebParam.client_language != "zh";
		int result;
		if (flag)
		{
			result = Fun.HaveShieldTxt_bx(content, startidx);
		}
		else
		{
			int num = -1;
			bool flag2 = string.IsNullOrEmpty(content);
			if (flag2)
			{
				result = num;
			}
			else
			{
				string[] array = fGameManager.mloadConfig.GetbadCharArr();
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = array[i].Length > 0 && content.Contains(array[i]);
					if (flag3)
					{
						num = content.IndexOf(array[i], startidx);
						break;
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0007493C File Offset: 0x00072B3C
	private static int HaveShieldTxt_bx(string content, int startidx)
	{
		int num = -1;
		bool flag = string.IsNullOrEmpty(content);
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			string[] array = fGameManager.mloadConfig.GetbadCharArr();
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = array[i].Length > 0 && content.ToLower().Contains(array[i].ToLower());
				if (flag2)
				{
					num = content.ToLower().IndexOf(array[i].ToLower(), startidx);
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x000749C8 File Offset: 0x00072BC8
	public static int HaveFilterTxt(string content, int startidx)
	{
		bool flag = WebParam.client_language != "zh";
		int result;
		if (flag)
		{
			result = Fun.HaveFilterTxt_bx(content, startidx);
		}
		else
		{
			int num = -1;
			bool flag2 = string.IsNullOrEmpty(content);
			if (flag2)
			{
				result = num;
			}
			else
			{
				string[] array = fGameManager.mloadConfig.GetfilterChatArr();
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = array[i].Length > 0 && content.Contains(array[i]);
					if (flag3)
					{
						num = content.IndexOf(array[i], startidx);
						break;
					}
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00074A60 File Offset: 0x00072C60
	private static int HaveFilterTxt_bx(string content, int startidx)
	{
		int num = -1;
		bool flag = string.IsNullOrEmpty(content);
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			string[] array = fGameManager.mloadConfig.GetfilterChatArr();
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = array[i].Length > 0 && content.ToLower().Contains(array[i].ToLower());
				if (flag2)
				{
					num = content.ToLower().IndexOf(array[i].ToLower(), startidx);
					break;
				}
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00074AEC File Offset: 0x00072CEC
	public static bool CheckSpecialStr(string str)
	{
		string[] array2;
		if (WebParam.client_language == "zh")
		{
			string[] array = new string[44];
			array[0] = "!";
			array[1] = "@";
			array[2] = "#";
			array[3] = "$";
			array[4] = "%";
			array[5] = "^";
			array[6] = "&";
			array[7] = "*";
			array[8] = "(";
			array[9] = ")";
			array[10] = "-";
			array[11] = "=";
			array[12] = ".";
			array[13] = "+";
			array[14] = "`";
			array[15] = "/";
			array[16] = "”";
			array[17] = "“";
			array[18] = "、";
			array[19] = "";
			array[20] = "";
			array[21] = " ";
			array[22] = " ";
			array[23] = "槑";
			array[24] = "，";
			array[25] = "。";
			array[26] = "、";
			array[27] = "；";
			array[28] = "’";
			array[29] = "【";
			array[30] = "】";
			array[31] = "｜";
			array[32] = "|";
			array[33] = "《";
			array[34] = "》";
			array[35] = "？";
			array[36] = "！";
			array[37] = "";
			array[38] = "";
			array[39] = "\u3000";
			array[40] = "'";
			array[41] = " '";
			array[42] = "\\";
			array2 = array;
			array[43] = "\"";
		}
		else
		{
			string[] array3 = new string[40];
			array3[0] = "!";
			array3[1] = "@";
			array3[2] = "#";
			array3[3] = "$";
			array3[4] = "%";
			array3[5] = "^";
			array3[6] = "*";
			array3[7] = "(";
			array3[8] = ")";
			array3[9] = "-";
			array3[10] = "=";
			array3[11] = ".";
			array3[12] = "+";
			array3[13] = "`";
			array3[14] = "/";
			array3[15] = "”";
			array3[16] = "“";
			array3[17] = "、";
			array3[18] = "";
			array3[19] = "";
			array3[20] = "槑";
			array3[21] = "，";
			array3[22] = "。";
			array3[23] = "、";
			array3[24] = "；";
			array3[25] = "’";
			array3[26] = "【";
			array3[27] = "】";
			array3[28] = "｜";
			array3[29] = "|";
			array3[30] = "《";
			array3[31] = "》";
			array3[32] = "？";
			array3[33] = "！";
			array3[34] = "";
			array3[35] = "";
			array3[36] = "'";
			array3[37] = " '";
			array3[38] = "\\";
			array2 = array3;
			array3[39] = "\"";
		}
		string[] array4 = array2;
		int num = 0;
		for (int i = 0; i < array4.Length; i++)
		{
			bool flag = str.Contains(array4[i]);
			if (flag)
			{
				num++;
			}
		}
		bool flag2 = num > 0;
		return !flag2;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00074E48 File Offset: 0x00073048
	public static string CheckChatSpecialStr(string str)
	{
		string[] array = new string[]
		{
			"",
			"",
			"\u3000",
			"'",
			" '",
			"\\"
		};
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = str.Contains(array[i]);
			if (flag)
			{
				str = str.Replace(array[i], "*");
			}
		}
		return str;
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00074EC8 File Offset: 0x000730C8
	public static string CheckChatSpecialStr_Gem_Data(string str)
	{
		string[] array = new string[]
		{
			"(",
			"((",
			"))"
		};
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = str.Contains(array[i]);
			if (flag)
			{
				str = str.Replace(array[i], string.Empty);
			}
		}
		str = str.Replace("),", "|");
		return str;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00074F44 File Offset: 0x00073144
	public static string SecToFullTimeString(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			int num = sec / 86400;
			int num2 = sec % 86400 / 3600;
			int num3 = sec % 3600 / 60;
			int num4 = sec % 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				result = Language.GetLang(9008, new object[]
				{
					num.ToString()
				}) + Language.GetLang(9005, new object[]
				{
					num2.ToString()
				}) + Language.GetLang(9006, new object[]
				{
					num3.ToString()
				}) + Language.GetLang(9007, new object[]
				{
					num4.ToString()
				});
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					result = Language.GetLang(9005, new object[]
					{
						num2.ToString()
					}) + Language.GetLang(9006, new object[]
					{
						num3.ToString()
					}) + Language.GetLang(9007, new object[]
					{
						num4.ToString()
					});
				}
				else
				{
					bool flag4 = num3 >= 1;
					if (flag4)
					{
						result = Language.GetLang(9006, new object[]
						{
							num3.ToString()
						}) + Language.GetLang(9007, new object[]
						{
							num4.ToString()
						});
					}
					else
					{
						result = Language.GetLang(9007, new object[]
						{
							num4.ToString()
						});
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x000750E8 File Offset: 0x000732E8
	public static int GetUserLvlupExp(int nlvl)
	{
		return Mathf.Min(nlvl / 5 * 1250 + nlvl * 250, 50000);
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00075118 File Offset: 0x00073318
	public static int GetHeroLvlupExp(int nlvl)
	{
		return Mathf.Min(nlvl / 5 * 500 + nlvl * 100, 20000);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00075144 File Offset: 0x00073344
	public static bool IDCardValidate(string sIdCard)
	{
		sIdCard = sIdCard.ToLower();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("11", "北京");
		dictionary.Add("12", "天津");
		dictionary.Add("13", "河北");
		dictionary.Add("14", "山西");
		dictionary.Add("15", "内蒙古");
		dictionary.Add("21", "辽宁");
		dictionary.Add("22", "吉林");
		dictionary.Add("23", "黑龙江");
		dictionary.Add("31", "上海");
		dictionary.Add("32", "江苏");
		dictionary.Add("33", "浙江");
		dictionary.Add("34", "安徽");
		dictionary.Add("35", "福建");
		dictionary.Add("36", "江西");
		dictionary.Add("37", "山东");
		dictionary.Add("41", "河南");
		dictionary.Add("42", "湖北");
		dictionary.Add("43", "湖南");
		dictionary.Add("44", "广东");
		dictionary.Add("45", "广西");
		dictionary.Add("46", "海南");
		dictionary.Add("50", "重庆");
		dictionary.Add("51", "四川");
		dictionary.Add("52", "贵州");
		dictionary.Add("53", "云南");
		dictionary.Add("54", "西藏");
		dictionary.Add("61", "陕西");
		dictionary.Add("62", "甘肃");
		dictionary.Add("63", "青海");
		dictionary.Add("64", "宁夏");
		dictionary.Add("65", "新疆");
		dictionary.Add("71", "台湾");
		dictionary.Add("81", "香港");
		dictionary.Add("82", "澳门");
		dictionary.Add("91", "国外");
		string[] array = new string[]
		{
			"1",
			"0",
			"x",
			"9",
			"8",
			"7",
			"6",
			"5",
			"4",
			"3",
			"2"
		};
		string[] array2 = new string[]
		{
			"7",
			"9",
			"10",
			"5",
			"8",
			"4",
			"2",
			"1",
			"6",
			"3",
			"7",
			"9",
			"10",
			"5",
			"8",
			"4",
			"2"
		};
		string text = string.Empty;
		bool flag = !Regex.IsMatch(sIdCard, "(^[1-9][0-9]{16}[0-9xX]$)");
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			text = sIdCard.Substring(0, 17);
			string text2 = text.Substring(6, 4);
			string text3 = text.Substring(10, 2);
			string text4 = text.Substring(12, 2);
			DateTime dateTime;
			bool flag2 = !DateTime.TryParse(string.Concat(new string[]
			{
				text2,
				"-",
				text3,
				"-",
				text4
			}), out dateTime);
			if (flag2)
			{
				result = false;
			}
			else
			{
				DateTime serverDate = Fun.GetServerDate();
				bool flag3 = serverDate.Year - dateTime.Year > 150 || serverDate.Year - dateTime.Year < 0;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = int.Parse(text3) > 12 || int.Parse(text3) == 0;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = int.Parse(text4) > 31 || int.Parse(text4) == 0;
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = !dictionary.ContainsKey(text.Substring(0, 2));
							if (flag6)
							{
								result = false;
							}
							else
							{
								int num = 0;
								char[] array3 = text.ToCharArray();
								for (int i = 0; i < 17; i++)
								{
									num += int.Parse(array3[i].ToString()) * int.Parse(array2[i]);
								}
								int num2 = num % 11;
								string str = array[num2];
								text += str;
								bool flag7 = text != sIdCard;
								result = !flag7;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00075660 File Offset: 0x00073860
	public static Vector3 SuperSmoothLerp(Vector3 x0, Vector3 y0, Vector3 yt, float t, float k)
	{
		Vector3 a = x0 - y0 + (yt - y0) / (k * t);
		return yt - (yt - y0) / (k * t) + a * Mathf.Exp((0f - k) * t);
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x000756C0 File Offset: 0x000738C0
	public static string GetLzImgName()
	{
		string empty = string.Empty;
		return Fun.GetOtherLzImgName(PlayerDataManager.getInstance().playerCommon);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x000756E8 File Offset: 0x000738E8
	public static string GetLzNianImgName()
	{
		string empty = string.Empty;
		return Fun.GetOtherLzNianImgName(PlayerDataManager.getInstance().playerCommon);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00075710 File Offset: 0x00073910
	public static string GetOtherLzImgName(PlayerCommon playercom)
	{
		string result = string.Empty;
		string qq_Zuan_Type = Fun.GetQQ_Zuan_Type(playercom);
		bool prerogative_State = playercom.Prerogative_Class.Prerogative_State;
		if (prerogative_State)
		{
			result = ((!playercom.Prerogative_Class.Prerogative_Super) ? string.Format("pt" + qq_Zuan_Type + "_0{0}", playercom.Prerogative_Class.Prerogative_Level + 1) : string.Format("hh" + qq_Zuan_Type + "_0{0}", playercom.Prerogative_Class.Prerogative_Level + 1));
		}
		return result;
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x000757A0 File Offset: 0x000739A0
	public static string GetOtherLzNianImgName(PlayerCommon playercom)
	{
		string result = string.Empty;
		string qq_Zuan_Type = Fun.GetQQ_Zuan_Type(playercom);
		bool prerogative_Year = playercom.Prerogative_Class.Prerogative_Year;
		if (prerogative_Year)
		{
			result = Fun.GetNainImgName(qq_Zuan_Type);
		}
		return result;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x000757D8 File Offset: 0x000739D8
	public static string GetQQ_Zuan_Type(PlayerCommon playercom = null)
	{
		string platform_Name = PlayerDataManager.getInstance().playerCommon.Prerogative_Class.Platform_Name;
		string text = (playercom != null) ? playercom.Prerogative_Class.Platform_Name : platform_Name;
		string result = string.Empty;
		bool flag = platform_Name == "qqgame" && text == "qqgame";
		if (flag)
		{
			result = "lz";
		}
		else
		{
			string text2 = platform_Name;
			string a = text2;
			if (a == "qzone" || a == "website" || a == "pengyou")
			{
				string text3 = text;
				string a2 = text3;
				if (a2 == "qzone" || a2 == "website" || a2 == "pengyou")
				{
					result = "hz";
					goto IL_EB;
				}
			}
			bool flag2 = text == string.Empty;
			if (flag2)
			{
				result = "lz";
			}
			IL_EB:;
		}
		return result;
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x000758D8 File Offset: 0x00073AD8
	public static string GetTwoQQ_State(string spt_0, string spt_1)
	{
		string result = string.Empty;
		bool flag = spt_0 == "qqgame" && spt_1 == "qqgame";
		if (flag)
		{
			result = "lz";
		}
		else if (spt_0 == "qzone" || spt_0 == "website" || spt_0 == "pengyou")
		{
			if (spt_1 == "qzone" || spt_1 == "website" || spt_1 == "pengyou")
			{
				result = "hz";
			}
		}
		return result;
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00075988 File Offset: 0x00073B88
	public static string GetNainImgName(string sQQType)
	{
		string result = string.Empty;
		bool flag = sQQType == "lz";
		if (flag)
		{
			result = "nian";
		}
		else
		{
			bool flag2 = sQQType == "hz";
			if (flag2)
			{
				result = "hz_nian";
			}
		}
		return result;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000759D4 File Offset: 0x00073BD4
	public static string SecToFullTimeStr(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string result;
		if (flag)
		{
			result = "0";
		}
		else
		{
			int num = sec / 3600;
			int num2 = sec / 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				result = Language.GetLang(9005, new object[]
				{
					num.ToString()
				});
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					result = Language.GetLang(9006, new object[]
					{
						num2.ToString()
					});
				}
				else
				{
					result = Language.GetLang(9006, new object[]
					{
						"<1"
					});
				}
			}
		}
		return result;
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x00075A84 File Offset: 0x00073C84
	public static string SecToFullTimeHStr(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string lang;
		if (flag)
		{
			lang = Language.GetLang(9006, new object[]
			{
				"<1"
			});
		}
		else
		{
			int num = sec / 3600;
			int num2 = sec / 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				lang = Language.GetLang(9005, new object[]
				{
					num.ToString()
				});
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					lang = Language.GetLang(9006, new object[]
					{
						num2.ToString()
					});
				}
				else
				{
					lang = Language.GetLang(9007, new object[]
					{
						sec.ToString()
					});
				}
			}
		}
		return lang;
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00075B48 File Offset: 0x00073D48
	public static int Convert_Int(string _Value)
	{
		int result = 0;
		bool flag = _Value != string.Empty;
		if (flag)
		{
			result = Convert.ToInt32(_Value);
		}
		return result;
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x00075B78 File Offset: 0x00073D78
	public static bool Convert_Boolean(string _Value)
	{
		return Fun.Convert_Int(_Value) == 1;
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x00075B94 File Offset: 0x00073D94
	public static float Convert_Float(string _Value, int _Unit)
	{
		return (float)Fun.Convert_Int(_Value) * 1f / (float)_Unit;
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x00075BB8 File Offset: 0x00073DB8
	public static ArrayList Convert_ArrayList(string _Value, char _Separator)
	{
		ArrayList arrayList = new ArrayList();
		string[] array = _Value.Split(new char[]
		{
			_Separator
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = array[i].ToString() != string.Empty;
			if (flag)
			{
				arrayList.Add(array[i].ToString());
			}
		}
		return arrayList;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x00075C28 File Offset: 0x00073E28
	public static Dictionary<int, List<int>> Convert_Dictionary(string _Value, char _Separator_1, char _Separator_2)
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		ArrayList arrayList = Fun.Convert_ArrayList(_Value, _Separator_1);
		for (int i = 0; i < arrayList.Count; i++)
		{
			string[] array = arrayList[i].ToString().Split(new char[]
			{
				_Separator_2
			});
			bool flag = array.Length >= 2 && !dictionary.ContainsKey(int.Parse(array[0].ToString()));
			if (flag)
			{
				dictionary.Add(int.Parse(array[0].ToString()), new List<int>
				{
					int.Parse(array[1].ToString()),
					int.Parse(array[2].ToString())
				});
			}
		}
		return dictionary;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00075CF8 File Offset: 0x00073EF8
	public static void ClearChildObejct(GameObject oList)
	{
		bool flag = !(oList == null);
		if (flag)
		{
			while (oList.transform.childCount > 0)
			{
				Transform child = oList.transform.GetChild(0);
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00075D48 File Offset: 0x00073F48
	public static string GetHZNum(int nnum)
	{
		string empty = string.Empty;
		string[] array = new string[]
		{
			Language.GetLang(25009, new object[0]),
			Language.GetLang(25010, new object[0]),
			Language.GetLang(25011, new object[0]),
			Language.GetLang(25012, new object[0]),
			Language.GetLang(25013, new object[0]),
			Language.GetLang(25014, new object[0]),
			Language.GetLang(25015, new object[0]),
			Language.GetLang(25016, new object[0]),
			Language.GetLang(25017, new object[0])
		};
		bool flag = nnum < 1 || nnum > 9;
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			result = array[nnum - 1];
		}
		return result;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00075E30 File Offset: 0x00074030
	public static string GetHZWeekDay(int weekday)
	{
		string empty = string.Empty;
		string[] array = new string[]
		{
			Language.GetLang(25009, new object[0]),
			Language.GetLang(25010, new object[0]),
			Language.GetLang(25011, new object[0]),
			Language.GetLang(25012, new object[0]),
			Language.GetLang(25013, new object[0]),
			Language.GetLang(25014, new object[0]),
			Language.GetLang(25020, new object[0])
		};
		bool flag = weekday < 1 || weekday > 7;
		string result;
		if (flag)
		{
			result = empty;
		}
		else
		{
			result = array[weekday - 1];
		}
		return result;
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00075EF0 File Offset: 0x000740F0
	public static int GetWeek2(DateTime dDate)
	{
		return ((int)(dDate - DateTime.Parse("2012-01-01")).TotalDays - 1) / 7;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00075F20 File Offset: 0x00074120
	public static string GetMoneyName(int moneytype)
	{
		string[] array = new string[]
		{
			Language.GetLang(28007, new object[0]),
			string.Empty,
			Language.GetLang(28006, new object[0]),
			string.Empty,
			string.Empty,
			Language.GetLang(28008, new object[0]),
			Language.GetLang(28009, new object[0])
		};
		return array[moneytype];
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00075FA0 File Offset: 0x000741A0
	public static string GetAttrName(int attrtype)
	{
		string result = string.Empty;
		switch (attrtype)
		{
		case 1:
			result = Language.GetLang(33010, new object[0]);
			break;
		case 2:
			result = Language.GetLang(33011, new object[0]);
			break;
		case 3:
			result = Language.GetLang(33012, new object[0]);
			break;
		case 4:
			result = Language.GetLang(33013, new object[0]);
			break;
		case 5:
			result = Language.GetLang(33014, new object[0]);
			break;
		case 6:
			result = Language.GetLang(33015, new object[0]);
			break;
		case 7:
			result = Language.GetLang(33016, new object[0]);
			break;
		case 8:
			result = Language.GetLang(33017, new object[0]);
			break;
		case 9:
			result = Language.GetLang(33018, new object[0]);
			break;
		case 10:
			result = Language.GetLang(33019, new object[0]);
			break;
		case 11:
			result = Language.GetLang(33020, new object[0]);
			break;
		case 12:
			result = Language.GetLang(33021, new object[0]);
			break;
		}
		return result;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x000760F0 File Offset: 0x000742F0
	public static string String2Unicode(string source)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(source);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i += 2)
		{
			stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x00076170 File Offset: 0x00074370
	public static string EncodingUTF8(string s)
	{
		bool flag = WebParam.client_language == "zh";
		string result;
		if (flag)
		{
			result = s;
		}
		else
		{
			string text = string.Empty;
			byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(s);
			for (int i = 0; i < bytes.Length; i++)
			{
				try
				{
					text += Convert.ToString(bytes[i], 16);
				}
				catch (Exception ex)
				{
					Debug.LogError("EncodingUTF8///" + s + "///" + ex.Message);
					return s;
				}
			}
			result = text;
		}
		return result;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00076218 File Offset: 0x00074418
	public static string DecodingUTF8(string s)
	{
		bool flag = WebParam.client_language == "zh";
		string result;
		if (flag)
		{
			result = s;
		}
		else
		{
			string empty = string.Empty;
			byte[] array = new byte[s.Length / 2];
			int num = 0;
			for (int i = 0; i < s.Length; i += 2)
			{
				try
				{
					array[num++] = Convert.ToByte(s.Substring(i, 2), 16);
				}
				catch (Exception ex)
				{
					Debug.LogError("DecodingUTF8///" + s + "///" + ex.Message);
					return s;
				}
			}
			result = Encoding.UTF8.GetString(array);
		}
		return result;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x000762D8 File Offset: 0x000744D8
	public static bool IsFunctionOpen(string sRea, object oParam = null)
	{
		bool flag = true;
		bool flag2 = WebParam.client_language == "zh";
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			uint num = PrivateImplementationDetails.ComputeStringHash(sRea);
			if (num <= 2922156751U)
			{
				if (num <= 2498766537U)
				{
					if (num <= 855296613U)
					{
						if (num != 218198237U)
						{
							if (num == 855296613U)
							{
								if (sRea == "FlowerRankTabBtn")
								{
									bool flag3 = WebParam.client_language == "baxi" && WebParam.version_language < 11;
									if (flag3)
									{
										flag = false;
									}
								}
							}
						}
						else if (sRea == "OpenQiangHua")
						{
							bool flag4 = WebParam.client_language == "baxi" && WebParam.version_language < 11;
							if (flag4)
							{
								flag = false;
							}
						}
					}
					else if (num != 1950090521U)
					{
						if (num == 2498766537U)
						{
							if (sRea == "LookingTeamBtn")
							{
								bool flag5 = WebParam.client_language == "baxi" && WebParam.version_language < 12;
								if (flag5)
								{
									flag = false;
								}
							}
						}
					}
					else if (sRea == "PveRankTabBtn")
					{
						bool flag6 = WebParam.client_language == "baxi" && WebParam.version_language < 11;
						if (flag6)
						{
							flag = false;
						}
					}
				}
				else if (num <= 2859700497U)
				{
					if (num != 2706256609U)
					{
						if (num == 2859700497U)
						{
							if (sRea == "DtsRankTabBtn")
							{
								bool flag7 = WebParam.client_language == "baxi" && WebParam.version_language < 14;
								if (flag7)
								{
									flag = false;
								}
							}
						}
					}
					else if (sRea == "chongzhi")
					{
						bool flag8 = WebParam.client_language == "baxi";
						if (flag8)
						{
							flag = false;
						}
					}
				}
				else if (num != 2906437912U)
				{
					if (num == 2922156751U)
					{
						if (sRea == "MatchDts")
						{
							bool flag9 = WebParam.client_language == "baxi" && WebParam.version_language < 14;
							if (flag9)
							{
								flag = false;
							}
						}
					}
				}
				else if (sRea == "ShowPveTabBtn")
				{
					bool flag10 = WebParam.client_language == "baxi";
					if (flag10)
					{
						bool flag11 = WebParam.version_language < 11;
						if (flag11)
						{
							flag = false;
						}
						bool flag12 = WebParam.version_language < 13 && (int)oParam != 9;
						if (flag12)
						{
							flag = false;
						}
					}
				}
			}
			else if (num <= 3831335541U)
			{
				if (num <= 3319635269U)
				{
					if (num != 3252728819U)
					{
						if (num == 3319635269U)
						{
							if (sRea == "HeroGemTabBtn")
							{
								bool flag13 = WebParam.client_language == "baxi" && WebParam.version_language < 12;
								if (flag13)
								{
									flag = false;
								}
							}
						}
					}
					else if (sRea == "ExchangeCenterBtn")
					{
						bool flag14 = WebParam.client_language == "baxi" && WebParam.version_language < 16;
						if (flag14)
						{
							flag = false;
						}
					}
				}
				else if (num != 3587343123U)
				{
					if (num == 3831335541U)
					{
						if (sRea == "GeneTabBtn")
						{
							bool flag15 = WebParam.client_language == "baxi" && WebParam.version_language < 13;
							if (flag15)
							{
								flag = false;
							}
						}
					}
				}
				else if (sRea == "WaiGuaOpen")
				{
					bool flag16 = WebParam.client_language == "baxi";
					if (flag16)
					{
						flag = false;
					}
				}
			}
			else if (num <= 4115793828U)
			{
				if (num != 3934241362U)
				{
					if (num == 4115793828U)
					{
						if (sRea == "lzRewardBtn")
						{
							bool flag17 = WebParam.client_language == "baxi";
							if (flag17)
							{
								flag = false;
							}
						}
					}
				}
				else if (sRea == "IsPveOpen")
				{
					bool flag18 = WebParam.client_language == "baxi" && WebParam.version_language < 11;
					if (flag18)
					{
						flag = false;
					}
				}
			}
			else if (num != 4202434808U)
			{
				if (num == 4259768942U)
				{
					if (sRea == "WishTreeBtn")
					{
						bool flag19 = WebParam.client_language == "baxi" && WebParam.version_language < 15;
						if (flag19)
						{
							flag = false;
						}
					}
				}
			}
			else if (sRea == "qqRewardBtn")
			{
				bool flag20 = WebParam.client_language == "baxi";
				if (flag20)
				{
					flag = false;
				}
			}
			result = flag;
		}
		return result;
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0007683C File Offset: 0x00074A3C
	public static string GetUdId()
	{
		string result = string.Empty;
		try
		{
			result = SystemInfo.deviceUniqueIdentifier;
		}
		catch
		{
		}
		return result;
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x00076874 File Offset: 0x00074A74
	public static string GetMAC0()
	{
		string result = string.Empty;
		try
		{
			string text = string.Empty;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			NetworkInterface[] array = allNetworkInterfaces;
			foreach (NetworkInterface networkInterface in array)
			{
				text = networkInterface.GetPhysicalAddress().ToString();
				bool flag = text.Length >= 12 && !text.StartsWith("000000000000");
				if (flag)
				{
					result = text;
					break;
				}
			}
		}
		catch
		{
		}
		return result;
	}

	// Token: 0x04000A50 RID: 2640
	public static DateTime m_dServerStart;

	// Token: 0x04000A51 RID: 2641
	public static DateTime m_dServerDate;

	// Token: 0x02000571 RID: 1393
	[CompilerGenerated]
	private static class O_cache
	{
		// Token: 0x04002CFB RID: 11515
		public static fResourceManager.DownFinishDelegate _State_0_Load_Sound_Complete;

		// Token: 0x04002CFC RID: 11516
		public static fResourceManager.DownFinishDelegate _State_1_Load_Image_Complete;

		// Token: 0x04002CFD RID: 11517
		public static fResourceManager.DownFinishDelegate _State_2_Load_Scene_Effect_Complete;
	}
}
