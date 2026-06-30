using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000276 RID: 630
public class OneGameResultCon : Window
{
	// Token: 0x06001D2B RID: 7467 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
	protected override void Awake()
	{
		base.Awake();
		this.WinBg = base.transform.Find("bg").GetComponent<Image>();
		this.head = base.transform.Find("head").GetComponent<RawImage>();
		this.tName = base.transform.Find("name").GetComponent<Text>();
		this.oLevel = base.transform.Find("lvl").gameObject;
		this.oLevel.SetActive(false);
		this.tLvl = base.transform.Find("lvl/Text").GetComponent<Text>();
		this.Mvp = base.transform.Find("mvp").gameObject;
		this.mvppic = this.Mvp.transform.Find("pic").GetComponent<Image>();
		this.ui_mvp = this.Mvp.transform.Find("ui_mvp").gameObject;
		this.xiaomie = base.transform.Find("xiaomie").gameObject;
		this.gongfangxiaomie = base.transform.Find("gongfangxiaomie").gameObject;
		this.gongfangshijiang = base.transform.Find("gongfangshijiang").gameObject;
		this.shanghailiang = base.transform.Find("shanghailiang").gameObject;
		this.zhiliaoliang = base.transform.Find("zhiliaoliang").gameObject;
		this.zhenwang = base.transform.Find("zhenwang").gameObject;
		this.shejiaobut = base.transform.Find("shejiao/shejiaobut").gameObject;
		EventTriggerListener.Get(this.shejiaobut).onClick = new EventTriggerListener.VoidDelegate(this.OnAddFriend);
		this.sky = base.transform.Find("sky").gameObject;
		this.skybg = this.sky.GetComponent<Image>();
		this.skyico = this.sky.transform.Find("skyico").gameObject.GetComponent<Image>();
		this.skylvl = this.sky.transform.Find("skyico/lvl").GetComponent<Text>();
		this.sky.SetActive(false);
		this.Init();
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0010D258 File Offset: 0x0010B458
	private void OnAddFriend(GameObject obj)
	{
		bool flag = !(this.sNowName == string.Empty);
		if (flag)
		{
			this.oGameResultUi.SetAddFriendBut(this);
			FriendManager.GetInstance().AddFriend(this.sNowName);
		}
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x0010D2A0 File Offset: 0x0010B4A0
	private void Init()
	{
		this.head.enabled = false;
		this.tName.text = string.Empty;
		this.Mvp.SetActive(false);
		this.oLevel.SetActive(false);
		this.xiaomie.SetActive(false);
		this.gongfangxiaomie.SetActive(false);
		this.gongfangshijiang.SetActive(false);
		this.shanghailiang.SetActive(false);
		this.zhiliaoliang.SetActive(false);
		this.zhenwang.SetActive(false);
		this.shejiaobut.SetActive(false);
		this.sky.SetActive(false);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x0010D350 File Offset: 0x0010B550
	private void setOneData(GameObject obj, int nValue, int nRank, int nPjz, int nIsMax)
	{
		int num = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		bool flag = this.m_nForceType > 0;
		if (flag)
		{
			num = 1;
		}
		GameObject gameObject = obj.transform.Find("hzbg").gameObject;
		bool flag2 = gameObject.transform.childCount > 0;
		if (flag2)
		{
			Fun.ClearChildObejct(gameObject);
		}
		GameObject rankEffect = this.oGameResultUi.GetRankEffect(nRank, num);
		bool flag3 = rankEffect != null;
		if (flag3)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(rankEffect);
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, -2f);
			Vector3 localScale = new Vector3(108f, 108f, 108f);
			bool flag4 = num == 1;
			if (flag4)
			{
				localScale = new Vector3(57f, 57f, 57f);
			}
			gameObject2.transform.localScale = localScale;
			gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject2.SetActive(true);
		}
		bool flag5 = this.m_nForceType == 0;
		if (flag5)
		{
			obj.transform.Find("pj_value").GetComponent<Text>().text = string.Empty;
			bool flag6 = nPjz > 0;
			if (flag6)
			{
				int sec = nPjz / 100;
				text = sec.ToString();
				bool flag7 = obj.name == "gongfangshijiang";
				if (flag7)
				{
					text = Fun.SecToFullTimeString(sec);
				}
				obj.transform.Find("pj_value").GetComponent<Text>().text = Language.GetLang(11015, new object[]
				{
					text
				});
			}
			text2 = nValue.ToString();
			bool flag8 = obj.name == "gongfangshijiang";
			if (flag8)
			{
				text2 = Fun.sencondsToHMS_CXP(nValue);
			}
			obj.transform.Find("syzj").gameObject.SetActive(false);
			bool flag9 = nIsMax == 1;
			if (flag9)
			{
				bool flag10 = WebParam.client_language != "baxi";
				if (flag10)
				{
					obj.transform.Find("syzj").gameObject.SetActive(true);
				}
				text2 = "<color=#00FF00>" + text2 + "</color>";
			}
			obj.transform.Find("value").GetComponent<Text>().text = text2;
		}
		else
		{
			text2 = nValue.ToString();
			bool flag11 = obj.name == "gongfangshijiang";
			if (flag11)
			{
				text2 = Fun.sencondsToHMS_CXP(nValue);
			}
			obj.transform.Find("value").GetComponent<Text>().text = text2;
		}
		bool flag12 = !obj.activeSelf;
		if (flag12)
		{
			obj.SetActive(true);
		}
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x0010D634 File Offset: 0x0010B834
	public void SetData(ArrayList szData, int nForceType, GameResultUICon gameResult)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList
		{
			0,
			0,
			0,
			0,
			0
		};
		string str = string.Empty;
		this.oGameResultUi = gameResult;
		this.Init();
		string text = "zijikuang";
		if (nForceType != 1)
		{
			if (nForceType == 2)
			{
				text = "hongtiao";
			}
		}
		else
		{
			text = "lantiao";
		}
		this.WinBg.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, text);
		bool flag = szData.Count < 1;
		if (!flag)
		{
			this.m_nForceType = nForceType;
			this.vepicSize = new Vector2(50f, 50f);
			bool flag2 = this.m_nForceType == 0;
			if (flag2)
			{
				this.vepicSize = new Vector2(90f, 90f);
			}
			int nNetID = (int)szData[0];
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
			string text2 = this.sNowName = Fun.DecodingUTF8(szData[1].ToString());
			this.tName.text = text2;
			bool flag3 = !this.oLevel.activeSelf;
			if (flag3)
			{
				this.oLevel.SetActive(true);
			}
			this.tLvl.text = ((int)szData[3] + (int)szData[17]).ToString();
			this.Mvp.SetActive(false);
			bool flag4 = (int)szData[9] == 1;
			if (flag4)
			{
				bool flag5 = (int)szData[5] == 1 || (int)szData[7] == 1;
				if (flag5)
				{
					this.mvppic.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, "mvp");
					this.ui_mvp.SetActive(true);
					this.Mvp.SetActive(true);
				}
				bool flag6 = (int)szData[6] == 1;
				if (flag6)
				{
					this.mvppic.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, "ace");
					this.ui_mvp.SetActive(false);
					this.Mvp.SetActive(true);
				}
			}
			int num = (int)szData[4];
			int key = 1;
			UserData userData = MatchManager.GetInstance().GetUserData(text2);
			bool flag7 = userData != null;
			if (flag7)
			{
				key = userData.GetUseEquipId(1, 0);
			}
			this.head.enabled = false;
			bool flag8 = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(num) && num != 0;
			if (flag8)
			{
				str = fGameManager.mloadConfig.AllHeroUnlock[num].szSkinUnlock[key].sIconName;
				sysload.loadImage(this.head, "herounlock/skin/" + str + ".txt", new sysload.loadImageFinish(this.imageloadok));
				this.head.GetComponent<RectTransform>().sizeDelta = this.vepicSize;
			}
			this.OnShowSkyLadder(nForceType, text, userData);
			arrayList = (ArrayList)szData[18];
			bool flag9 = arrayList.Count < 1;
			if (flag9)
			{
				arrayList = arrayList4;
			}
			arrayList2 = (ArrayList)szData[19];
			bool flag10 = arrayList2.Count < 1;
			if (flag10)
			{
				arrayList2 = arrayList4;
			}
			arrayList3 = (ArrayList)szData[20];
			bool flag11 = arrayList3.Count < 1;
			if (flag11)
			{
				arrayList3 = arrayList4;
			}
			this.setOneData(this.xiaomie, (int)szData[10], (int)arrayList[0], (int)arrayList2[0], (int)arrayList3[0]);
			bool flag12 = MatchManager.GetInstance().m_nMatchType != 3;
			if (flag12)
			{
				this.setOneData(this.gongfangxiaomie, (int)szData[11], (int)arrayList[1], (int)arrayList2[1], (int)arrayList3[1]);
				this.setOneData(this.gongfangshijiang, (int)szData[12], (int)arrayList[2], (int)arrayList2[2], (int)arrayList3[2]);
			}
			this.setOneData(this.shanghailiang, (int)szData[13], (int)arrayList[3], (int)arrayList2[3], (int)arrayList3[3]);
			this.setOneData(this.zhiliaoliang, (int)szData[14], (int)arrayList[4], (int)arrayList2[4], (int)arrayList3[4]);
			this.setOneData(this.zhenwang, (int)szData[15], -1, 0, 0);
			bool flag13 = nForceType != 0;
			if (flag13)
			{
				this.shejiaobut.SetActive(false);
				int num2 = 0;
				bool flag14 = netLiving != null && netLiving.netobject_type == 6;
				if (flag14)
				{
					num2 = 1;
				}
				bool flag15 = !FriendManager.GetInstance().IsFriendRlt(text2) && num2 == 0;
				if (flag15)
				{
					this.shejiaobut.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x0010DBE0 File Offset: 0x0010BDE0
	private void OnShowSkyLadder(int nForceType, string sBg, UserData userdata)
	{
		bool flag = userdata != null && SkyLadderManager.GetInstance().isSkyLadderMatch && userdata.m_szSkyData.Count > 0 && (int)userdata.m_szSkyData[1] > 0;
		if (flag)
		{
			bool flag2 = nForceType == 0;
			if (flag2)
			{
				sBg = string.Empty;
			}
			bool flag3 = sBg != string.Empty;
			if (flag3)
			{
				this.skybg.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, sBg);
				this.skybg.enabled = true;
			}
			else
			{
				this.skybg.enabled = false;
			}
			int num = (int)userdata.m_szSkyData[1];
			bool flag4 = num > 8;
			if (flag4)
			{
				num = 8;
			}
			this.skylvl.text = string.Empty;
			this.skyico.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num.ToString());
			bool flag5 = !this.sky.activeSelf;
			if (flag5)
			{
				this.sky.SetActive(true);
			}
		}
		else
		{
			bool activeSelf = this.sky.activeSelf;
			if (activeSelf)
			{
				this.sky.SetActive(false);
			}
		}
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x0010DD1F File Offset: 0x0010BF1F
	public void OnUpdateSheJieBut(string sName)
	{
		this.shejiaobut.SetActive(false);
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x0010DD2F File Offset: 0x0010BF2F
	public void imageloadok(object oImg, object ra)
	{
		((RawImage)ra).GetComponent<RectTransform>().sizeDelta = this.vepicSize;
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x0010DD4C File Offset: 0x0010BF4C
	private string GetRankIco(int nRank)
	{
		string result = string.Empty;
		switch (nRank)
		{
		case 0:
			result = "jin";
			break;
		case 1:
			result = "yin";
			break;
		case 2:
			result = "tong";
			break;
		}
		return result;
	}

	// Token: 0x040019A3 RID: 6563
	private Image WinBg;

	// Token: 0x040019A4 RID: 6564
	private RawImage head;

	// Token: 0x040019A5 RID: 6565
	private Text tName;

	// Token: 0x040019A6 RID: 6566
	private Text tLvl;

	// Token: 0x040019A7 RID: 6567
	private GameObject Mvp;

	// Token: 0x040019A8 RID: 6568
	private GameObject oLevel;

	// Token: 0x040019A9 RID: 6569
	private GameObject xiaomie;

	// Token: 0x040019AA RID: 6570
	private Image xiaomie_hzbg;

	// Token: 0x040019AB RID: 6571
	private Text xiaomie_value;

	// Token: 0x040019AC RID: 6572
	private Text xiaomie_pj_value;

	// Token: 0x040019AD RID: 6573
	private Image xiaomie_syzj;

	// Token: 0x040019AE RID: 6574
	private GameObject gongfangxiaomie;

	// Token: 0x040019AF RID: 6575
	private GameObject gongfangshijiang;

	// Token: 0x040019B0 RID: 6576
	private GameObject shanghailiang;

	// Token: 0x040019B1 RID: 6577
	private GameObject zhiliaoliang;

	// Token: 0x040019B2 RID: 6578
	private GameObject zhenwang;

	// Token: 0x040019B3 RID: 6579
	private GameObject shejiaobut;

	// Token: 0x040019B4 RID: 6580
	private int m_nForceType;

	// Token: 0x040019B5 RID: 6581
	private Vector2 vepicSize = Vector2.zero;

	// Token: 0x040019B6 RID: 6582
	private GameResultUICon oGameResultUi;

	// Token: 0x040019B7 RID: 6583
	private GameObject ui_mvp;

	// Token: 0x040019B8 RID: 6584
	private Image mvppic;

	// Token: 0x040019B9 RID: 6585
	private GameObject sky;

	// Token: 0x040019BA RID: 6586
	private Image skybg;

	// Token: 0x040019BB RID: 6587
	private Image skyico;

	// Token: 0x040019BC RID: 6588
	private Text skylvl;

	// Token: 0x040019BD RID: 6589
	private string sNowName = string.Empty;
}
