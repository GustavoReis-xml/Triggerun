using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004B RID: 75
public class CareerUICon : Window
{
	// Token: 0x06000281 RID: 641 RVA: 0x00012DD0 File Offset: 0x00010FD0
	protected override void Awake()
	{
		base.Awake();
		this.rFace = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.tName = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.tLevel = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.tExp = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.sExpSlider = this.UIGameObj.Get_GameObj(18).GetComponent<Slider>();
		this.dSky = this.UIGameObj.Get_GameObj(4).GetComponent<Dropdown>();
		this.tSkylevel = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.tSkyHighlevel = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.tSkytime = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.tSkynum = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.dSky.AddOptions(this.HeroDropdown);
		this.dSky.onValueChanged.AddListener(delegate(int _Value)
		{
			this.Selected_Dropdown_Value(_Value);
		});
		this.UIGameObj.Get_GameObj(10).SetActive(false);
		this.gHeroObj = this.UIGameObj.Get_GameObj(12);
		this.gHeroShowObj = this.UIGameObj.Get_GameObj(13);
		this.gHeroShowObj.SetActive(false);
		this.gherolist = this.UIGameObj.Get_GameObj(16);
		this.gherodata = this.UIGameObj.Get_GameObj(17);
		this.gHeroDataObj = this.UIGameObj.Get_GameObj(14);
		this.gHeroDataShowObj = this.UIGameObj.Get_GameObj(15);
		this.gHeroDataShowObj.SetActive(false);
		this.gLzList = this.UIGameObj.Get_GameObj(19);
		this.lzImg = this.UIGameObj.Get_GameObj(20);
		this.lzImg.SetActive(false);
		this.zwsj = this.UIGameObj.Get_GameObj(22);
		this.zwsj.SetActive(false);
		this.tTopName = this.UIGameObj.Get_GameObj(23).GetComponent<Text>();
		this.topico1 = this.UIGameObj.Get_GameObj(24);
		this.topico1.SetActive(false);
		this.topico2 = this.UIGameObj.Get_GameObj(25);
		this.topico2.SetActive(false);
		CareerUICon.inst = this;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0001304D File Offset: 0x0001124D
	private void Start()
	{
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00013050 File Offset: 0x00011250
	private void Update()
	{
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00013053 File Offset: 0x00011253
	public void InitCareerData()
	{
		this.SetCareerFace();
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00013060 File Offset: 0x00011260
	public void SetCareerFace()
	{
		string item = string.Empty;
		int nFace = PlayerDataManager.getInstance().HUserData.m_nFace;
		bool flag = fGameManager.mloadConfig.AllFaceData.ContainsKey(nFace);
		if (flag)
		{
			this.data = fGameManager.mloadConfig.AllFaceData[nFace];
			fUIManager.GetInstance().GetIconAsset("userface/" + this.data.sFaceIcon + ".txt", this.rFace);
		}
		this.OnLzIcoData(PlayerDataManager.getInstance().playerCommon);
		this.tName.text = PlayerDataManager.getInstance().HUserData.m_sName;
		this.tLevel.text = PlayerDataManager.getInstance().HUserData.m_nLevel.ToString();
		this.sExpSlider.value = (float)PlayerDataManager.getInstance().HUserData.m_nExp * 1f / (float)Fun.GetUserLvlupExp(PlayerDataManager.getInstance().HUserData.m_nLevel);
		this.tExp.text = string.Format("{0}/{1}", PlayerDataManager.getInstance().HUserData.m_nExp, Fun.GetUserLvlupExp(PlayerDataManager.getInstance().HUserData.m_nLevel));
		int nSkySeasonId = SkyLadderManager.GetInstance().m_nSkySeasonId;
		bool flag2 = nSkySeasonId > 0;
		if (flag2)
		{
			for (int i = 1; i <= nSkySeasonId; i++)
			{
				item = Language.GetLang(30061, new object[]
				{
					i
				});
				bool flag3 = !this.HeroDropdown.Contains(item);
				if (flag3)
				{
					this.HeroDropdown.Add(item);
				}
			}
		}
		this.dSky.ClearOptions();
		this.dSky.AddOptions(this.HeroDropdown);
		this.Selected_Dropdown_Value(0);
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0001323C File Offset: 0x0001143C
	private void OnLzIcoData(PlayerCommon playercom)
	{
		string text = string.Empty;
		Fun.ClearChildObejct(this.gLzList);
		text = Fun.GetOtherLzImgName(playercom);
		bool flag = !string.IsNullOrEmpty(text);
		if (flag)
		{
			this.AddLzIco(text);
		}
		text = Fun.GetOtherLzNianImgName(playercom);
		bool flag2 = !string.IsNullOrEmpty(text);
		if (flag2)
		{
			this.AddLzIco(text);
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00013298 File Offset: 0x00011498
	private void AddLzIco(string imgname)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.lzImg);
		gameObject.transform.SetParent(this.gLzList.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, imgname);
		gameObject.SetActive(true);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0001331C File Offset: 0x0001151C
	private void Selected_Dropdown_Value(int nValue)
	{
		int nAllTime = 0;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		this.m_nTagType = 0;
		bool flag = nValue == 0;
		if (flag)
		{
			this.m_nHeroType = -1;
		}
		else
		{
			bool flag2 = nValue > 1;
			if (flag2)
			{
				this.m_nHeroType = 2;
				this.m_nTagType = nValue - 1;
			}
			else
			{
				this.m_nHeroType = nValue;
			}
		}
		this.dSky.value = nValue;
		this.m_szCareerData = new ArrayList();
		this.m_szCareerData = this.GetTypeCareerData(this.m_nHeroType, this.m_nTagType);
		int count = this.m_szCareerData.Count;
		Fun.ClearChildObejct(this.gHeroObj);
		this.oSelectHeroObj = null;
		bool flag3 = count < 1;
		if (flag3)
		{
			bool flag4 = !this.zwsj.activeSelf;
			if (flag4)
			{
				this.zwsj.SetActive(true);
			}
			this.tTopName.text = string.Empty;
			this.tSkytime.text = string.Empty;
			this.tSkylevel.text = string.Empty;
			this.topico1.SetActive(false);
			this.topico2.SetActive(false);
			this.tSkyHighlevel.text = string.Empty;
			this.tSkynum.text = string.Empty;
			bool activeSelf = this.gherolist.activeSelf;
			if (activeSelf)
			{
				this.gherolist.SetActive(false);
			}
			bool activeSelf2 = this.gherodata.activeSelf;
			if (activeSelf2)
			{
				this.gherodata.SetActive(false);
			}
		}
		else
		{
			bool flag5 = !this.gherolist.activeSelf;
			if (flag5)
			{
				this.gherolist.SetActive(true);
			}
			bool flag6 = !this.gherodata.activeSelf;
			if (flag6)
			{
				this.gherodata.SetActive(true);
			}
			this.tTopName.text = this.HeroDropdown[nValue];
			bool activeSelf3 = this.zwsj.activeSelf;
			if (activeSelf3)
			{
				this.zwsj.SetActive(false);
			}
			for (int i = 0; i < count; i++)
			{
				arrayList = (ArrayList)this.m_szCareerData[i];
				bool flag7 = arrayList.Count >= 1;
				if (flag7)
				{
					int num = (int)arrayList[0];
					bool flag8 = num == -1;
					if (flag8)
					{
						nAllTime = (int)arrayList[4];
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gHeroShowObj);
					gameObject.transform.SetParent(this.gHeroObj.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.transform.localScale = Vector3.one;
					gameObject.SetActive(true);
					gameObject.name = num.ToString();
					gameObject.transform.Find("select").GetComponent<Image>().enabled = false;
					EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.CheckHeroTypeData);
					EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
					EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
					OneHeroCareerUI oneHeroCareerUI = gameObject.GetComponent<OneHeroCareerUI>();
					bool flag9 = oneHeroCareerUI == null;
					if (flag9)
					{
						oneHeroCareerUI = gameObject.AddComponent<OneHeroCareerUI>();
					}
					oneHeroCareerUI.OnInit(arrayList, nAllTime);
					bool flag10 = this.oSelectHeroObj == null && num == -1;
					if (flag10)
					{
						this.CheckHeroTypeData(gameObject);
					}
				}
			}
			bool flag11 = this.m_nHeroType == 2;
			if (flag11)
			{
				arrayList2 = this.GetTypeUserCareerData(this.m_nHeroType, this.m_nTagType);
				bool flag12 = arrayList2.Count > 0;
				if (flag12)
				{
					int num2 = (int)arrayList2[19];
					this.topico1.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num2.ToString());
					this.topico1.SetActive(true);
					this.tSkylevel.text = "<color=#FF8E00>" + arrayList2[18] + "</color>";
					this.tSkyHighlevel.text = Language.GetLang(30062, new object[0]);
					this.topico2.SetActive(false);
					int num3 = (int)arrayList2[21];
					this.topico2.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num3.ToString());
					this.topico2.SetActive(true);
					this.tSkytime.text = "<color=#FF8E00>" + arrayList2[20] + "</color>";
					this.tSkynum.text = Language.GetLang(30063, new object[0]);
				}
			}
			else
			{
				this.topico1.SetActive(false);
				this.topico2.SetActive(false);
				bool flag13 = count > 0;
				if (flag13)
				{
					arrayList2 = (ArrayList)this.m_szCareerData[0];
				}
				bool flag14 = arrayList2.Count > 0;
				if (flag14)
				{
					this.tSkylevel.text = "<color=#FF8E00>" + CareerUICon.sencondsToHMS((int)arrayList2[4]) + "</color>";
				}
				this.tSkyHighlevel.text = Language.GetLang(7030, new object[0]);
				arrayList2 = this.GetTypeUserCareerData(this.m_nHeroType, this.m_nTagType);
				bool flag15 = arrayList2.Count > 0;
				if (flag15)
				{
					this.tSkytime.text = Language.GetLang(30057, new object[]
					{
						arrayList2[3]
					});
				}
				this.tSkynum.text = Language.GetLang(7031, new object[0]);
			}
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00013921 File Offset: 0x00011B21
	private void OnEnterTitle(GameObject obj)
	{
		obj.transform.localScale = new Vector3(1.04f, 1.1f, 1.05f);
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00013944 File Offset: 0x00011B44
	private void OnExitTitle(GameObject obj)
	{
		obj.transform.localScale = Vector3.one;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00013958 File Offset: 0x00011B58
	private ArrayList GetTypeCareerData(int nType, int nTagType)
	{
		int num = 0;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList();
		ArrayList szData = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szHeroCareerData.Clone();
		arrayList4 = this.GetAllData(szData, nType, nTagType);
		int count = arrayList4.Count;
		for (int i = 0; i < count; i++)
		{
			arrayList3 = (ArrayList)arrayList4[i];
			bool flag = arrayList3.Count >= 1;
			if (flag)
			{
				arrayList = this.GerTimeSort(arrayList3, arrayList);
				num += (int)arrayList3[4];
				arrayList2 = this.GetSumData(arrayList2, arrayList3);
			}
		}
		bool flag2 = arrayList.Count > 0;
		if (flag2)
		{
			bool flag3 = arrayList2.Count < 1;
			if (flag3)
			{
				arrayList2 = new ArrayList
				{
					-1,
					0
				};
			}
			arrayList.Insert(0, arrayList2);
		}
		return arrayList;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00013A68 File Offset: 0x00011C68
	private ArrayList GetAllData(ArrayList szData, int nType, int nTagType)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList szCareerData = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		Dictionary<int, ArrayList> dictionary = new Dictionary<int, ArrayList>();
		int count = szData.Count;
		for (int i = 0; i < count; i += 2)
		{
			int num = (int)szData[i];
			arrayList = (ArrayList)szData[i + 1];
			bool flag = arrayList.Count < 1;
			if (!flag)
			{
				int num2 = (int)arrayList[18];
				bool flag2 = (num == nType || nType == -1) && (nTagType <= 0 || num2 == nTagType);
				if (flag2)
				{
					int key = (int)arrayList[0];
					bool flag3 = !dictionary.ContainsKey(key);
					if (flag3)
					{
						dictionary.Add(key, arrayList);
					}
					else
					{
						szCareerData = dictionary[key];
						dictionary[key] = this.GetArraryAddData(szCareerData, arrayList);
					}
				}
			}
		}
		foreach (int key2 in dictionary.Keys)
		{
			arrayList3 = dictionary[key2];
			bool flag4 = arrayList3.Count >= 1;
			if (flag4)
			{
				arrayList2.Add(arrayList3);
			}
		}
		return arrayList2;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00013BDC File Offset: 0x00011DDC
	private ArrayList GetSumData(ArrayList szCareerData, ArrayList szData)
	{
		int count = szData.Count;
		ArrayList arrayList = (ArrayList)szCareerData.Clone();
		bool flag = count < 1;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			bool flag2 = arrayList.Count > 0;
			if (flag2)
			{
				for (int i = 0; i < count; i++)
				{
					bool flag3 = i >= 2;
					if (flag3)
					{
						int num = (int)arrayList[i] + (int)szData[i];
						arrayList[i] = num;
					}
				}
			}
			else
			{
				arrayList = (ArrayList)szData.Clone();
				arrayList[0] = -1;
				arrayList[1] = string.Empty;
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00013CA4 File Offset: 0x00011EA4
	private ArrayList GetArraryAddData(ArrayList szCareerData, ArrayList szData)
	{
		ArrayList arrayList = (ArrayList)szCareerData.Clone();
		int count = szData.Count;
		bool flag = count < 1;
		ArrayList result;
		if (flag)
		{
			result = szCareerData;
		}
		else
		{
			bool flag2 = arrayList.Count > 0;
			if (flag2)
			{
				for (int i = 0; i < count; i++)
				{
					bool flag3 = i >= 2;
					if (flag3)
					{
						int num = (int)arrayList[i] + (int)szData[i];
						arrayList[i] = num;
					}
				}
			}
			else
			{
				arrayList = (ArrayList)szData.Clone();
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00013D50 File Offset: 0x00011F50
	private ArrayList GerTimeSort(ArrayList szData, ArrayList szCareerData)
	{
		ArrayList arrayList = new ArrayList();
		int count = szCareerData.Count;
		bool flag = count > 0;
		if (flag)
		{
			int num = (int)szData[4];
			for (int i = 0; i < count; i++)
			{
				arrayList = (ArrayList)szCareerData[i];
				bool flag2 = arrayList.Count >= 1;
				if (flag2)
				{
					int num2 = (int)arrayList[4];
					bool flag3 = num > num2;
					if (flag3)
					{
						szCareerData.Insert(i, szData);
						break;
					}
					bool flag4 = i + 1 >= count;
					if (flag4)
					{
						szCareerData.Add(szData);
						break;
					}
				}
			}
		}
		else
		{
			szCareerData.Add(szData);
		}
		return szCareerData;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00013E18 File Offset: 0x00012018
	private void CheckHeroTypeData(GameObject obj)
	{
		ArrayList szCareerData = new ArrayList();
		int num = int.Parse(obj.name);
		bool flag = !(this.oSelectHeroObj == obj);
		if (flag)
		{
			bool flag2 = this.oSelectHeroObj != null;
			if (flag2)
			{
				this.oSelectHeroObj.transform.Find("select").GetComponent<Image>().enabled = false;
			}
			obj.transform.Find("select").GetComponent<Image>().enabled = true;
			this.oSelectHeroObj = obj;
			Fun.ClearChildObejct(this.gHeroDataObj);
			OneHeroCareerUI component = obj.GetComponent<OneHeroCareerUI>();
			bool flag3 = component != null;
			if (flag3)
			{
				szCareerData = component.m_szCareerData;
			}
			bool flag4 = num == -1;
			if (flag4)
			{
				this.OnAddCareerAllData(szCareerData);
			}
			else
			{
				this.OnHeroCareerAllData(num, szCareerData);
			}
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00013EF4 File Offset: 0x000120F4
	private void OnAddCareerObj(int nTypeMsg, string sValue)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gHeroDataShowObj);
		gameObject.transform.SetParent(this.gHeroDataObj.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		gameObject.transform.Find("value").GetComponent<Text>().text = sValue;
		gameObject.transform.Find("desc").GetComponent<Text>().text = Language.GetLang(nTypeMsg, new object[0]);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00013FAC File Offset: 0x000121AC
	private float GetBfb(int value, int value1)
	{
		float num = 0f;
		bool flag = value < 1 || value1 < 1;
		float result;
		if (flag)
		{
			result = num;
		}
		else
		{
			result = (float)value / (float)value1 * 100f;
		}
		return result;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00013FE4 File Offset: 0x000121E4
	private float GetPjz(int value, int value1)
	{
		float num = 0f;
		bool flag = value < 1 || value1 < 1;
		float result;
		if (flag)
		{
			result = num;
		}
		else
		{
			result = (float)value / (float)value1;
		}
		return result;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00014018 File Offset: 0x00012218
	private void OnAddCareerAllData(ArrayList szCareerData)
	{
		ArrayList arrayList = new ArrayList();
		string sValue = string.Empty;
		arrayList = this.GetTypeUserCareerData(this.m_nHeroType, this.m_nTagType);
		bool flag = arrayList.Count >= 1;
		if (flag)
		{
			int num = (int)arrayList[6];
			bool flag2 = szCareerData.Count > 0;
			if (flag2)
			{
				num = (int)szCareerData[4];
			}
			sValue = arrayList[2].ToString();
			this.OnAddCareerObj(30007, sValue);
			sValue = this.GetBfb((int)arrayList[3], (int)arrayList[2]).ToString("0.0") + "%";
			this.OnAddCareerObj(30008, sValue);
			sValue = arrayList[7].ToString();
			this.OnAddCareerObj(30009, sValue);
			sValue = this.GetPjz((int)arrayList[8], (int)arrayList[2]).ToString("0.0");
			this.OnAddCareerObj(30012, sValue);
			float pjz = this.GetPjz((int)arrayList[11], (int)arrayList[2]);
			sValue = ((int)pjz).ToString();
			this.OnAddCareerObj(30014, sValue);
			pjz = this.GetPjz((int)arrayList[12], (int)arrayList[2]);
			sValue = ((int)pjz).ToString();
			this.OnAddCareerObj(30016, sValue);
			sValue = this.GetPjz((int)arrayList[9], (int)arrayList[2]).ToString("0.0");
			this.OnAddCareerObj(30018, sValue);
			sValue = string.Empty;
			bool flag3 = (int)arrayList[10] > 0 && (int)arrayList[2] > 0;
			if (flag3)
			{
				sValue = CareerUICon.sencondsToHMS((int)arrayList[10] / (int)arrayList[2]);
			}
			this.OnAddCareerObj(30020, sValue);
			sValue = string.Empty;
			bool flag4 = (int)arrayList[13] > 0 && (int)arrayList[2] > 0;
			if (flag4)
			{
				sValue = this.GetPjz((int)arrayList[13], (int)arrayList[2]).ToString("0.0");
			}
			this.OnAddCareerObj(30022, sValue);
			sValue = arrayList[17].ToString();
			this.OnAddCareerObj(30025, sValue);
			sValue = arrayList[16].ToString();
			this.OnAddCareerObj(30026, sValue);
			sValue = arrayList[15].ToString();
			this.OnAddCareerObj(30027, sValue);
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00014308 File Offset: 0x00012508
	private ArrayList GetTypeUserCareerData(int nType, int nfbtype)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList szUserCareerData = PlayerDataManager.getInstance().HUserData.m_szUserCareerData;
		int count = szUserCareerData.Count;
		for (int i = 0; i < count; i += 2)
		{
			int num = (int)szUserCareerData[i];
			arrayList2 = (ArrayList)szUserCareerData[i + 1];
			bool flag = arrayList2.Count < 1;
			if (!flag)
			{
				int num2 = (int)arrayList2[1];
				bool flag2 = num != nType && nType != -1;
				if (!flag2)
				{
					bool flag3 = nfbtype > 0;
					if (flag3)
					{
						bool flag4 = nfbtype == num2;
						if (flag4)
						{
							arrayList = this.GetSumData(arrayList, arrayList2);
						}
					}
					else
					{
						arrayList = this.GetSumData(arrayList, arrayList2);
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x06000296 RID: 662 RVA: 0x000143E4 File Offset: 0x000125E4
	private void OnHeroCareerAllData(int nHeroId, ArrayList szCareerData)
	{
		ArrayList arrayList = new ArrayList();
		string sValue = string.Empty;
		int seconds = 0;
		bool flag = szCareerData.Count < 1;
		if (!flag)
		{
			arrayList = this.GetTypeHeroCareerHigh(this.m_nHeroType, nHeroId);
			bool flag2 = arrayList.Count >= 1;
			if (flag2)
			{
				sValue = CareerUICon.sencondsToHMS((int)szCareerData[4]);
				this.OnAddCareerObj(30006, sValue);
				sValue = szCareerData[3].ToString();
				this.OnAddCareerObj(30007, sValue);
				sValue = this.GetBfb((int)szCareerData[2], (int)szCareerData[3]).ToString("0.0") + "%";
				this.OnAddCareerObj(30008, sValue);
				int num = (int)szCareerData[16];
				bool flag3 = num > 0;
				float num2;
				if (flag3)
				{
					num2 = (float)((int)szCareerData[7]) * 1f / (float)num * 1f * 100f;
					seconds = (int)num2;
					sValue = seconds.ToString() + "%";
				}
				else
				{
					sValue = "0%";
				}
				this.OnAddCareerObj(30034, sValue);
				bool flag4 = num > 0;
				if (flag4)
				{
					num2 = (float)((int)szCareerData[8]) * 1f / (float)num * 1f * 100f;
					seconds = (int)num2;
					sValue = seconds.ToString() + "%";
				}
				else
				{
					sValue = "0%";
				}
				this.OnAddCareerObj(30035, sValue);
				num2 = this.GetPjz((int)szCareerData[12], (int)szCareerData[3]);
				sValue = ((int)num2).ToString();
				this.OnAddCareerObj(30036, sValue);
				sValue = this.GetPjz((int)szCareerData[9], (int)szCareerData[3]).ToString("0.0");
				this.OnAddCareerObj(30037, sValue);
				num2 = this.GetPjz((int)szCareerData[13], (int)szCareerData[3]);
				sValue = ((int)num2).ToString();
				this.OnAddCareerObj(30040, sValue);
				bool flag5 = (int)szCareerData[3] > 0;
				if (flag5)
				{
					seconds = (int)szCareerData[11] / (int)szCareerData[3];
				}
				sValue = CareerUICon.sencondsToHMS(seconds);
				this.OnAddCareerObj(30041, sValue);
				sValue = this.GetPjz((int)szCareerData[10], (int)szCareerData[3]).ToString("0.0");
				this.OnAddCareerObj(30042, sValue);
			}
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x000146C0 File Offset: 0x000128C0
	private ArrayList GetTypeHeroCareerHigh(int nType, int nHeroId)
	{
		ArrayList result = new ArrayList();
		ArrayList arrayList = new ArrayList();
		ArrayList szHeroCareerHighData = PlayerDataManager.getInstance().HUserData.m_szHeroCareerHighData;
		int count = szHeroCareerHighData.Count;
		for (int i = 0; i < count; i += 2)
		{
			int num = (int)szHeroCareerHighData[i];
			arrayList = (ArrayList)szHeroCareerHighData[i + 1];
			bool flag = arrayList.Count >= 1 && (num == nType || nType == -1) && (int)arrayList[0] == nHeroId;
			if (flag)
			{
				result = arrayList;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00014760 File Offset: 0x00012960
	public static string sencondsToHMS(int seconds)
	{
		int num = seconds / 3600;
		int num2 = seconds % 3600 / 60;
		int num3 = seconds % 60;
		string empty = string.Empty;
		bool flag = num >= 1;
		string result;
		if (flag)
		{
			result = Language.GetLang(9005, new object[]
			{
				num.ToString()
			});
		}
		else
		{
			result = num2.ToString().PadLeft(2, '0') + ":" + num3.ToString().PadLeft(2, '0');
		}
		return result;
	}

	// Token: 0x04000247 RID: 583
	public static CareerUICon inst;

	// Token: 0x04000248 RID: 584
	private RawImage rFace;

	// Token: 0x04000249 RID: 585
	private Text tName;

	// Token: 0x0400024A RID: 586
	private Text tLevel;

	// Token: 0x0400024B RID: 587
	private Text tExp;

	// Token: 0x0400024C RID: 588
	private Dropdown dSky;

	// Token: 0x0400024D RID: 589
	private Text tSkylevel;

	// Token: 0x0400024E RID: 590
	private Text tSkyHighlevel;

	// Token: 0x0400024F RID: 591
	private Text tSkytime;

	// Token: 0x04000250 RID: 592
	private Text tSkynum;

	// Token: 0x04000251 RID: 593
	private Slider sExpSlider;

	// Token: 0x04000252 RID: 594
	private Dropdown dHeromatch;

	// Token: 0x04000253 RID: 595
	private GameObject gHeroObj;

	// Token: 0x04000254 RID: 596
	private GameObject gHeroShowObj;

	// Token: 0x04000255 RID: 597
	private GameObject gHeroDataObj;

	// Token: 0x04000256 RID: 598
	private GameObject gHeroDataShowObj;

	// Token: 0x04000257 RID: 599
	private GameObject gherolist;

	// Token: 0x04000258 RID: 600
	private GameObject gherodata;

	// Token: 0x04000259 RID: 601
	private GameObject gLzList;

	// Token: 0x0400025A RID: 602
	private GameObject lzImg;

	// Token: 0x0400025B RID: 603
	private GameObject zwsj;

	// Token: 0x0400025C RID: 604
	private Text tTopName;

	// Token: 0x0400025D RID: 605
	private PlayerFaceData data;

	// Token: 0x0400025E RID: 606
	private GameObject topico1;

	// Token: 0x0400025F RID: 607
	private GameObject topico2;

	// Token: 0x04000260 RID: 608
	private List<string> HeroDropdown = new List<string>
	{
		Language.GetLang(30000, new object[0]),
		Language.GetLang(5007, new object[0])
	};

	// Token: 0x04000261 RID: 609
	private int m_nHeroType = -1;

	// Token: 0x04000262 RID: 610
	private int m_nTagType;

	// Token: 0x04000263 RID: 611
	private GameObject oSelectHeroObj;

	// Token: 0x04000264 RID: 612
	private ArrayList m_szCareerData = new ArrayList();
}
