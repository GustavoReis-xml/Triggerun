using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B5 RID: 437
public class HeroRoundEndCon : Window
{
	// Token: 0x06001413 RID: 5139 RVA: 0x000BD3E8 File Offset: 0x000BB5E8
	protected override void Awake()
	{
		base.Awake();
		this.bg = base.transform.FindChild("bg").GetComponent<RawImage>();
		this.ui_hhjs = base.transform.FindChild("ui_hhjs").gameObject;
		this.ui_hhjs.SetActive(false);
		this.roundnum = base.transform.FindChild("roundnum").GetComponent<Text>();
		this.roundnum.text = string.Empty;
		this.result = base.transform.FindChild("result").GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.result.font = fUIManager.mNewComic;
		}
		this.result.text = string.Empty;
		this.clonedian = base.transform.FindChild("clonedian").gameObject;
		this.clonedian.SetActive(false);
		this.roundlist = base.transform.FindChild("roundlist").gameObject;
		this.roundlist.SetActive(false);
		this.LeftNum = base.transform.FindChild("LeftNum").gameObject;
		this.RightNum = base.transform.FindChild("RightNum").gameObject;
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			this.ui_shengli = base.transform.FindChild("ui_shengli_bx").gameObject;
			this.ui_zhanbai = base.transform.FindChild("ui_zhanbai_bx").gameObject;
			this.ui_pengju = base.transform.FindChild("ui_pingju_bx").gameObject;
		}
		else
		{
			this.ui_shengli = base.transform.FindChild("ui_shengli").gameObject;
			this.ui_zhanbai = base.transform.FindChild("ui_zhanbai").gameObject;
			this.ui_pengju = base.transform.FindChild("ui_pingju").gameObject;
		}
		this.ui_shengli.SetActive(false);
		this.ui_zhanbai.SetActive(false);
		this.ui_pengju.SetActive(false);
		this.lefttext = base.transform.FindChild("lefttext").gameObject;
		this.LeftText1 = this.lefttext.transform.Find("Text2").GetComponent<Text>();
		this.LeftText2 = this.lefttext.transform.Find("Text4").GetComponent<Text>();
		this.lefttext.SetActive(false);
		this.righttext = base.transform.FindChild("righttext").gameObject;
		this.RightText1 = this.righttext.transform.Find("Text2").GetComponent<Text>();
		this.RightText2 = this.righttext.transform.Find("Text4").GetComponent<Text>();
		this.righttext.SetActive(false);
		HeroRoundEndCon.Inst = this;
		fUIManager.GetInstance().GetIconAsset(string.Format("SelectHeroUI/bg/{0}.txt", "roundendbg"), this.bg);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x000BD72C File Offset: 0x000BB92C
	private void Update()
	{
		bool flag = !this.mboolstat;
		if (!flag)
		{
			float num = Time.time - this.fTime;
			bool flag2 = num >= 0.2f && !this.roundnum.enabled;
			if (flag2)
			{
				this.roundnum.enabled = true;
				bool flag3 = !this.result.enabled;
				if (flag3)
				{
					this.result.enabled = true;
				}
			}
			bool flag4 = num >= 0.3f && !this.LeftNum.activeSelf;
			if (flag4)
			{
				int nNum = (this.m_nResult == 1) ? this.m_nMeBf : 0;
				this.setScaleEffect(this.LeftNum, nNum);
			}
			bool flag5 = num >= 0.4f && !this.RightNum.activeSelf;
			if (flag5)
			{
				int nNum2 = (this.m_nResult == 2) ? this.m_nTargetBf : 0;
				this.setScaleEffect(this.RightNum, nNum2);
			}
			bool flag6 = this.m_nIsResult <= 0 || num < 3f;
			if (!flag6)
			{
				bool enabled = this.result.enabled;
				if (enabled)
				{
					this.result.enabled = false;
				}
				GameObject resultObj = this.GetResultObj(this.m_nIsResult);
				bool flag7 = resultObj != null && !resultObj.activeSelf;
				if (flag7)
				{
					resultObj.SetActive(true);
					bool flag8 = this.m_nIsResult == 1;
					if (flag8)
					{
						PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[881];
						fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
					}
					bool flag9 = this.m_nIsResult == 2;
					if (flag9)
					{
						PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[862];
						fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
					}
					bool flag10 = this.m_nIsResult == 3;
					if (flag10)
					{
						PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[885];
						fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
					}
					this.m_nIsResult = 0;
				}
			}
		}
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x000BD964 File Offset: 0x000BBB64
	private GameObject GetResultObj(int nResult)
	{
		GameObject gameObject = null;
		switch (nResult)
		{
		case 1:
			gameObject = this.ui_shengli;
			break;
		case 2:
			gameObject = this.ui_pengju;
			break;
		case 3:
			gameObject = this.ui_zhanbai;
			break;
		}
		return gameObject;
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x000BD9B0 File Offset: 0x000BBBB0
	public void OnSetUI(ArrayList szData)
	{
		string text = string.Empty;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		bool flag = szData.Count < 1;
		if (!flag)
		{
			int num = (int)szData[0];
			this.m_nResult = (int)szData[1];
			this.m_nMeBf = (int)szData[2];
			this.m_nTargetBf = (int)szData[3];
			this.m_szRoundResult = (ArrayList)szData[4];
			this.m_nIsResult = (int)szData[5];
			this.lefttext.SetActive(false);
			this.righttext.SetActive(false);
			bool flag2 = szData.Count > 6;
			if (flag2)
			{
				arrayList2 = (ArrayList)szData[6];
				bool flag3 = arrayList2.Count > 0;
				if (flag3)
				{
					int num2 = (int)arrayList2[0];
					this.lefttext.SetActive(false);
					bool flag4 = (int)arrayList2[1] != 0 || (int)arrayList2[2] != 0;
					if (flag4)
					{
						this.lefttext.SetActive(true);
						float num3 = (float)((int)arrayList2[1]) / 10f;
						this.LeftText1.text = Language.GetLang(20006, new object[]
						{
							num3
						});
						this.LeftText2.text = Fun.sencondsToHMS_CXP((int)arrayList2[2]);
					}
					this.righttext.SetActive(false);
					bool flag5 = (int)arrayList2[3] != 0 || (int)arrayList2[4] != 0;
					if (flag5)
					{
						this.righttext.SetActive(true);
						float num3 = (float)((int)arrayList2[3]) / 10f;
						this.LeftText1.text = Language.GetLang(20006, new object[]
						{
							num3
						});
						this.LeftText2.text = Fun.sencondsToHMS_CXP((int)arrayList2[4]);
					}
				}
			}
			base.gameObject.SetActive(true);
			bool activeSelf = this.ui_shengli.activeSelf;
			if (activeSelf)
			{
				this.ui_shengli.SetActive(false);
			}
			bool activeSelf2 = this.ui_zhanbai.activeSelf;
			if (activeSelf2)
			{
				this.ui_zhanbai.SetActive(false);
			}
			bool activeSelf3 = this.ui_pengju.activeSelf;
			if (activeSelf3)
			{
				this.ui_pengju.SetActive(false);
			}
			this.roundnum.enabled = false;
			bool flag6 = num < this.szNumString.Count;
			if (flag6)
			{
				text = ((!(WebParam.client_language == "zh")) ? this.szArabNum[num].ToString() : this.szNumString[num].ToString());
			}
			this.roundnum.text = Language.GetLang(20001, new object[]
			{
				text
			});
			this.result.enabled = false;
			FbXmlData pFbData = MatchManager.GetInstance().m_pFbData;
			bool flag7 = pFbData.nType != 2;
			if (flag7)
			{
				this.result.text = ((this.m_nResult != 1) ? Language.GetLang(20003, new object[0]) : Language.GetLang(20002, new object[0]));
			}
			int num4 = this.m_nMeBf;
			int num5 = this.m_nTargetBf;
			bool flag8 = pFbData.nType == 2;
			if (flag8)
			{
				bool flag9 = this.m_nResult == 1;
				if (flag9)
				{
					num4 = 0;
				}
				bool flag10 = this.m_nResult == 2;
				if (flag10)
				{
					num5 = 0;
				}
			}
			else
			{
				bool flag11 = this.m_nResult == 1;
				if (flag11)
				{
					num4 = this.m_nMeBf - 1;
				}
				bool flag12 = this.m_nResult == 2;
				if (flag12)
				{
					num5 = this.m_nTargetBf - 1;
				}
			}
			this.LeftNum.SetActive(false);
			this.RightNum.SetActive(false);
			this.LeftNum.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, num4.ToString());
			this.RightNum.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, num5.ToString());
			Fun.ClearChildObejct(this.roundlist);
			bool flag13 = pFbData.nType != 2 && this.m_szRoundResult.Count > 0;
			if (flag13)
			{
				arrayList = (ArrayList)this.m_szRoundResult.Clone();
				arrayList[num - 1] = 0;
				this.roundlist.SetActive(false);
				this.OnRoundList(arrayList);
			}
			this.ui_hhjs.SetActive(false);
			this.ui_hhjs.SetActive(true);
			this.mboolstat = true;
			this.fTime = Time.time;
		}
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x000BDEA4 File Offset: 0x000BC0A4
	private void OnRoundList(ArrayList szData)
	{
		FbXmlData pFbData = MatchManager.GetInstance().m_pFbData;
		bool flag = pFbData.nType == 2;
		if (!flag)
		{
			int count = szData.Count;
			for (int i = 0; i < count; i++)
			{
				int nResult = (int)szData[i];
				Transform transform = this.roundlist.transform.Find(i.ToString());
				bool flag2 = transform == null;
				GameObject gameObject;
				if (flag2)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonedian);
					gameObject.transform.SetParent(this.roundlist.transform);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.name = i.ToString();
				}
				else
				{
					gameObject = transform.gameObject;
				}
				gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, this.GetSpriteStr(nResult));
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000BDFD4 File Offset: 0x000BC1D4
	private string GetSpriteStr(int nResult)
	{
		string text = "baiquan";
		if (nResult != 1)
		{
			if (nResult == 2)
			{
				text = "hongdian";
			}
		}
		else
		{
			text = "landian";
		}
		return text;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x000BE010 File Offset: 0x000BC210
	public override void Close()
	{
		this.mboolstat = false;
		this.fTime = 0f;
		this.m_nMeBf = 0;
		this.m_nTargetBf = 0;
		this.m_nResult = 0;
		this.m_nIsResult = 0;
		this.m_szRoundResult.Clear();
		this.lefttext.SetActive(false);
		this.righttext.SetActive(false);
		base.gameObject.SetActive(false);
		base.Close();
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x000BE088 File Offset: 0x000BC288
	private void setScaleEffect(GameObject oObj, int nNum)
	{
		oObj.transform.localScale = new Vector3(5f, 5f, 1f);
		oObj.SetActive(true);
		bool flag = !this.roundlist.activeSelf;
		if (flag)
		{
			this.roundlist.SetActive(true);
		}
		Tweener t = oObj.transform.transform.DOScale(Vector3.one, 0.1f);
		t.OnComplete(delegate
		{
			Tweener t2 = oObj.transform.transform.DOScale(Vector3.one, 2f);
			t2.OnComplete(delegate
			{
				bool flag2 = nNum > 0;
				if (flag2)
				{
					this.CheckBfNumEffect(oObj, nNum);
				}
			});
		});
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000BE138 File Offset: 0x000BC338
	private void CheckBfNumEffect(GameObject oObj, int nNum)
	{
		Tweener t = oObj.transform.transform.DOScale(new Vector3(3f, 3f, 1f), 0.1f);
		t.OnComplete(delegate
		{
			oObj.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_ROUNDEND, nNum.ToString());
			this.OnRoundList(this.m_szRoundResult);
			oObj.transform.transform.DOScale(Vector3.one, 0.1f);
		});
	}

	// Token: 0x04000F68 RID: 3944
	public static HeroRoundEndCon Inst;

	// Token: 0x04000F69 RID: 3945
	private RawImage bg;

	// Token: 0x04000F6A RID: 3946
	private GameObject ui_hhjs;

	// Token: 0x04000F6B RID: 3947
	private GameObject clonedian;

	// Token: 0x04000F6C RID: 3948
	private GameObject roundlist;

	// Token: 0x04000F6D RID: 3949
	private GameObject LeftNum;

	// Token: 0x04000F6E RID: 3950
	private GameObject RightNum;

	// Token: 0x04000F6F RID: 3951
	private Text roundnum;

	// Token: 0x04000F70 RID: 3952
	private Text result;

	// Token: 0x04000F71 RID: 3953
	private GameObject ui_shengli;

	// Token: 0x04000F72 RID: 3954
	private GameObject ui_zhanbai;

	// Token: 0x04000F73 RID: 3955
	private GameObject ui_pengju;

	// Token: 0x04000F74 RID: 3956
	private GameObject lefttext;

	// Token: 0x04000F75 RID: 3957
	private Text LeftText1;

	// Token: 0x04000F76 RID: 3958
	private Text LeftText2;

	// Token: 0x04000F77 RID: 3959
	private GameObject righttext;

	// Token: 0x04000F78 RID: 3960
	private Text RightText1;

	// Token: 0x04000F79 RID: 3961
	private Text RightText2;

	// Token: 0x04000F7A RID: 3962
	private ArrayList szNumString = new ArrayList
	{
		string.Empty,
		"一",
		"二",
		"三",
		"四",
		"五",
		"六",
		"七",
		"八",
		"九"
	};

	// Token: 0x04000F7B RID: 3963
	private ArrayList szArabNum = new ArrayList
	{
		string.Empty,
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9"
	};

	// Token: 0x04000F7C RID: 3964
	public bool mboolstat;

	// Token: 0x04000F7D RID: 3965
	private float fTime;

	// Token: 0x04000F7E RID: 3966
	private int m_nMeBf;

	// Token: 0x04000F7F RID: 3967
	private int m_nTargetBf;

	// Token: 0x04000F80 RID: 3968
	private int m_nResult;

	// Token: 0x04000F81 RID: 3969
	private int m_nIsResult;

	// Token: 0x04000F82 RID: 3970
	private ArrayList m_szRoundResult = new ArrayList();
}
