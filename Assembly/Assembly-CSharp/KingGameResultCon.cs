using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020B RID: 523
public class KingGameResultCon : Window
{
	// Token: 0x060016CF RID: 5839 RVA: 0x000D3E84 File Offset: 0x000D2084
	protected override void Awake()
	{
		base.Awake();
		this.mygamedata = this.UIGameObj.Get_GameObj(0);
		this.clonegameuserdata = this.UIGameObj.Get_GameObj(1);
		this.pUserList = this.UIGameObj.Get_GameObj(2);
		this.clonegameuserdata.SetActive(false);
		this.pXingXingList = this.UIGameObj.Get_GameObj(3);
		this.pXingXing = this.UIGameObj.Get_GameObj(4);
		this.pXingXing.SetActive(false);
		this.ImgResult = this.UIGameObj.Get_GameObj(5).GetComponent<Image>();
		this.InitShowXingXing();
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x000D3F2C File Offset: 0x000D212C
	public void OnSetDada(GameResultUICon cResult)
	{
		this.heroMe = fGameManager.GetInstance().getPlayer();
		this.gameResult = cResult;
		ArrayList szFbResultData = FBManager.GetInstance().m_szFbResultData;
		bool flag = szFbResultData.Count < 1;
		if (flag)
		{
			Debug.Log("打开结算界面时副本结算数据已经清空结算已经结束--名称:" + roleuiManager.instance.GetMeName() + "--时间:" + Fun.GetServerDate().ToString("yyyy-MM-dd hh:mm:ss fff"));
		}
		else
		{
			this.m_szResultUserList = (ArrayList)szFbResultData[0];
			this.m_szResultHeroList = (ArrayList)szFbResultData[1];
			this.OnResultData();
		}
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000D3FC8 File Offset: 0x000D21C8
	public void InitShowXingXing()
	{
		for (int i = 0; i < this.m_nMaxXing; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pXingXing);
			gameObject.transform.SetParent(this.pXingXingList.transform);
			gameObject.SetActive(true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_szXingList.Add(gameObject);
		}
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000D4058 File Offset: 0x000D2258
	public void SerXingXingLv(int nLv)
	{
		int num = 0;
		bool flag = nLv > this.m_nMaxXing;
		if (!flag)
		{
			foreach (GameObject gameObject in this.m_szXingList)
			{
				bool flag2 = num < nLv;
				if (flag2)
				{
					gameObject.transform.FindChild("Image").gameObject.SetActive(true);
				}
				else
				{
					gameObject.transform.FindChild("Image").gameObject.SetActive(false);
				}
				num++;
			}
		}
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000D4108 File Offset: 0x000D2308
	private void OnResultData()
	{
		string sName = string.Empty;
		int nLv = 0;
		ArrayList arrayList = new ArrayList();
		string imageName = string.Empty;
		sName = this.heroMe.playerCommon.m_sName;
		int nShili = this.heroMe.playerCommon.m_nShili;
		this.myGameResult = this.mygamedata.GetComponent<OneGameResultCon>();
		bool flag = this.myGameResult == null;
		if (flag)
		{
			this.myGameResult = this.mygamedata.AddComponent<OneGameResultCon>();
		}
		arrayList = this.GetResultData(sName);
		this.myGameResult.SetData(arrayList, 0, this.gameResult);
		this.OnCreateResultData(nShili, 1, this.pUserList);
		bool flag2 = (int)arrayList[5] == 1;
		if (flag2)
		{
			imageName = "cg";
			nLv = KingTrainManager.GetInstance().GetXing((int)arrayList[22]);
		}
		else
		{
			imageName = "sb";
		}
		this.ImgResult.sprite = Window.spriteName(AtlasType.ATLAS_TABWIN, imageName);
		this.SerXingXingLv(nLv);
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x000D4210 File Offset: 0x000D2410
	private void OnCreateResultData(int nShili, int nForceType, GameObject oParent)
	{
		ArrayList szData = new ArrayList();
		ArrayList resultList = this.GetResultList(nShili, nForceType);
		int num = 5;
		bool flag = nForceType == 1;
		if (flag)
		{
			num--;
			string sName = this.heroMe.playerCommon.m_sName;
			int num2 = resultList.IndexOf(sName);
			bool flag2 = num2 != -1;
			if (flag2)
			{
				resultList.Remove(sName);
			}
		}
		int count = resultList.Count;
		for (int i = 0; i < num; i++)
		{
			Transform transform = oParent.transform.Find(i.ToString());
			bool flag3 = transform == null;
			GameObject gameObject;
			if (flag3)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonegameuserdata);
				gameObject.transform.SetParent(oParent.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				gameObject.name = i.ToString();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			szData = new ArrayList();
			bool flag4 = i < count;
			if (flag4)
			{
				szData = this.GetResultData(resultList[i].ToString());
			}
			OneGameResultCon oneGameResultCon = gameObject.GetComponent<OneGameResultCon>();
			bool flag5 = oneGameResultCon == null;
			if (flag5)
			{
				oneGameResultCon = gameObject.AddComponent<OneGameResultCon>();
			}
			oneGameResultCon.SetData(szData, nForceType, this.gameResult);
		}
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000D4398 File Offset: 0x000D2598
	private ArrayList GetResultList(int nShili, int nForceType)
	{
		ArrayList arrayList = new ArrayList();
		int count = this.m_szResultUserList.Count;
		for (int i = 0; i < count; i += 2)
		{
			string value = Fun.DecodingUTF8(this.m_szResultUserList[i].ToString());
			bool flag = count < 2;
			if (!flag)
			{
				ArrayList arrayList2 = (ArrayList)this.m_szResultUserList[i + 1];
				bool flag2 = arrayList2.Count >= 1;
				if (flag2)
				{
					int num = (int)arrayList2[2];
					bool flag3 = nForceType == 1 && nShili == num;
					if (flag3)
					{
						arrayList.Add(value);
					}
					bool flag4 = nForceType == 2 && nShili != num;
					if (flag4)
					{
						arrayList.Add(value);
					}
				}
			}
		}
		return arrayList;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000D4474 File Offset: 0x000D2674
	private ArrayList GetResultData(string sName)
	{
		ArrayList arrayList = new ArrayList();
		int num = this.m_szResultUserList.IndexOf(Fun.EncodingUTF8(sName));
		bool flag = num == -1;
		ArrayList result;
		if (flag)
		{
			result = arrayList;
		}
		else
		{
			result = (ArrayList)this.m_szResultUserList[num + 1];
		}
		return result;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000D44BE File Offset: 0x000D26BE
	private void Start()
	{
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000D44C1 File Offset: 0x000D26C1
	private void Update()
	{
	}

	// Token: 0x0400120B RID: 4619
	private GameObject mygamedata;

	// Token: 0x0400120C RID: 4620
	private Image ImgResult;

	// Token: 0x0400120D RID: 4621
	private GameObject pXingXingList;

	// Token: 0x0400120E RID: 4622
	private GameObject pXingXing;

	// Token: 0x0400120F RID: 4623
	private GameObject clonegameuserdata;

	// Token: 0x04001210 RID: 4624
	private GameObject pUserList;

	// Token: 0x04001211 RID: 4625
	private HeroPlayer heroMe;

	// Token: 0x04001212 RID: 4626
	private ArrayList m_szResultUserList = new ArrayList();

	// Token: 0x04001213 RID: 4627
	private ArrayList m_szResultHeroList = new ArrayList();

	// Token: 0x04001214 RID: 4628
	private GameResultUICon gameResult;

	// Token: 0x04001215 RID: 4629
	private OneGameResultCon myGameResult;

	// Token: 0x04001216 RID: 4630
	private List<GameObject> m_szXingList = new List<GameObject>();

	// Token: 0x04001217 RID: 4631
	private int m_nMaxXing = 3;
}
