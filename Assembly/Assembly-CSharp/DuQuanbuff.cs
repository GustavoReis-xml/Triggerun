using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class DuQuanbuff : NetLiving
{
	// Token: 0x0600098F RID: 2447 RVA: 0x000554BC File Offset: 0x000536BC
	public DuQuanbuff()
	{
		this.bDestroyMovie = false;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x000554EC File Offset: 0x000536EC
	public void createDuQuanbuff(ArrayList szPam)
	{
		this.netobject_type = 4;
		this.m_nPointX = (float)((int)szPam[1]);
		this.m_nPointZ = (float)((int)szPam[2]);
		this.m_nRadius = (int)szPam[3];
		this.m_nSafePointX = (float)((int)szPam[8]);
		this.m_nSafePointZ = (float)((int)szPam[9]);
		this.m_nSafeRadius = (int)szPam[10];
		this.MoveTime = (float)((int)szPam[11]);
		this.m_nWaitLongTime = (int)szPam[12];
		this.m_nShow = (int)szPam[5];
		bool flag = this.m_nShow == 1;
		if (flag)
		{
			this.m_bShowState = true;
		}
		else
		{
			this.m_bShowState = false;
		}
		this.m_nState = (int)szPam[6];
		this.m_dMoveStartTime = DateTime.Parse((string)szPam[4]);
		bool flag2 = this.thisDuQuanModel == null;
		if (flag2)
		{
			this.OnLoadQuan();
		}
		else
		{
			this.SetDuQuanPos(this.m_nRadius, this.m_nPointX, this.m_nPointZ);
			this.OnSetSafePos();
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00055630 File Offset: 0x00053830
	public void OnLoadQuan()
	{
		this.m_sModelName = "duquan";
		string url = "EffectRes/" + this.m_sModelName + ".ef";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), 0);
		}
		else
		{
			this.loadReayPrefab(resWWW, 0);
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0005569C File Offset: 0x0005389C
	private void loadReayPrefab(WWW www, object dd)
	{
		this.thisDuQuanModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
		GameObject gameObject = this.thisDuQuanModel.transform.Find("Sphere001").gameObject;
		this.thisDuQuanModel.transform.SetParent(fGameManager.allBaozhaFather.transform);
		this.thisDuQuanModel.name = "DuQuanModel";
		this.thisSafeModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
		GameObject gameObject2 = this.thisSafeModel.transform.Find("Sphere001").gameObject;
		this.thisSafeModel.transform.SetParent(fGameManager.allBaozhaFather.transform);
		this.thisSafeModel.name = "SafeModel";
		this.thisSafeModel.SetActive(false);
		this.dqInfo = this.thisDuQuanModel.GetComponent<DuQuanInfo>();
		bool flag = this.dqInfo == null;
		if (flag)
		{
			this.dqInfo = this.thisDuQuanModel.AddComponent<DuQuanInfo>();
		}
		this.dqInfo.InitInfo(this);
		QuanManager.GetInstance().SetDuQuan(this);
		this.SetDuQuanPos(this.m_nRadius, this.m_nPointX, this.m_nPointZ);
		this.OnSetSafePos();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0005580C File Offset: 0x00053A0C
	public void SetDuQuanPos(int nRadius, float nPointX, float nPointZ)
	{
		float num = (float)nRadius * 2f;
		float quanHigh = this.QuanHigh;
		float num2 = (float)nRadius * 2f;
		this.thisDuQuanModel.transform.localScale = new Vector3(num / 100f, quanHigh, num2 / 100f);
		this.thisDuQuanModel.transform.localPosition = new Vector3(nPointX / 100f, 0f, nPointZ / 100f);
		this.thisDuQuanModel.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0005589C File Offset: 0x00053A9C
	private void OnSetSafePos()
	{
		float num = (float)(this.m_nSafeRadius * 2);
		float num2 = (float)(this.m_nSafeRadius * 2);
		this.thisSafeModel.transform.localScale = new Vector3(num / 100f, 0f, num2 / 100f);
		this.thisSafeModel.transform.localPosition = new Vector3(this.m_nSafePointX / 100f, 0f, this.m_nSafePointZ / 100f);
		this.thisSafeModel.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x00055935 File Offset: 0x00053B35
	public void UpDataBuffData(ArrayList szData)
	{
		this.createDuQuanbuff(szData);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00055940 File Offset: 0x00053B40
	private void ShowQuanData()
	{
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00055944 File Offset: 0x00053B44
	public Vector3 GetEffPoint()
	{
		bool flag = this.thisDuQuanModel == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = this.thisDuQuanModel.transform.localPosition;
		}
		return result;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00055980 File Offset: 0x00053B80
	public Vector3 GetEffScale()
	{
		bool flag = this.thisDuQuanModel == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = this.thisDuQuanModel.transform.localScale;
		}
		return result;
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x000559BC File Offset: 0x00053BBC
	public Vector3 GetSafeEff()
	{
		bool flag = this.thisSafeModel == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = this.thisSafeModel.transform.localPosition;
		}
		return result;
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x000559F8 File Offset: 0x00053BF8
	public Vector3 GetSafeScale()
	{
		bool flag = this.thisSafeModel == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			result = this.thisSafeModel.transform.localScale;
		}
		return result;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00055A33 File Offset: 0x00053C33
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.OnNetDestroy();
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00055A40 File Offset: 0x00053C40
	public override void OnNetDestroy()
	{
		bool flag = this.thisDuQuanModel != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.thisDuQuanModel);
		}
		bool flag2 = this.thisSafeModel != null;
		if (flag2)
		{
			UnityEngine.Object.Destroy(this.thisSafeModel);
		}
		bool flag3 = this.dqInfo != null;
		if (flag3)
		{
			UnityEngine.Object.Destroy(this.dqInfo);
		}
		this.thisDuQuanModel = null;
		this.thisSafeModel = null;
		base.OnNetDestroy();
	}

	// Token: 0x0400075F RID: 1887
	public float QuanHigh = 2500f;

	// Token: 0x04000760 RID: 1888
	public bool bDestroyMovie;

	// Token: 0x04000761 RID: 1889
	private string m_sModelName = string.Empty;

	// Token: 0x04000762 RID: 1890
	private int m_nBuffId;

	// Token: 0x04000763 RID: 1891
	private string m_sTriggerDate;

	// Token: 0x04000764 RID: 1892
	private int m_nBType;

	// Token: 0x04000765 RID: 1893
	public float m_nPointX;

	// Token: 0x04000766 RID: 1894
	public float m_nPointY;

	// Token: 0x04000767 RID: 1895
	public float m_nPointZ;

	// Token: 0x04000768 RID: 1896
	private float m_nRotationX;

	// Token: 0x04000769 RID: 1897
	private float m_nRotationY;

	// Token: 0x0400076A RID: 1898
	private float m_nRotationZ;

	// Token: 0x0400076B RID: 1899
	public float m_nSafePointX;

	// Token: 0x0400076C RID: 1900
	public float m_nSafePointY;

	// Token: 0x0400076D RID: 1901
	public float m_nSafePointZ;

	// Token: 0x0400076E RID: 1902
	private float m_nMapEffectId;

	// Token: 0x0400076F RID: 1903
	public int m_nShow = -1;

	// Token: 0x04000770 RID: 1904
	public int m_nState;

	// Token: 0x04000771 RID: 1905
	public bool m_bShowState;

	// Token: 0x04000772 RID: 1906
	public DateTime m_dMoveStartTime;

	// Token: 0x04000773 RID: 1907
	public float MoveTime;

	// Token: 0x04000774 RID: 1908
	public int m_nRadius;

	// Token: 0x04000775 RID: 1909
	public int m_nSafeRadius;

	// Token: 0x04000776 RID: 1910
	public int m_nWaitLongTime;

	// Token: 0x04000777 RID: 1911
	private float ScaleTime;

	// Token: 0x04000778 RID: 1912
	public GameObject thisDuQuanModel;

	// Token: 0x04000779 RID: 1913
	private GameObject thisSafeModel;

	// Token: 0x0400077A RID: 1914
	private DuQuanInfo dqInfo;
}
