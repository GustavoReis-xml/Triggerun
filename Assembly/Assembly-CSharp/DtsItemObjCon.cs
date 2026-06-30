using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class DtsItemObjCon : MonoBehaviour
{
	// Token: 0x06000927 RID: 2343 RVA: 0x0004DB1C File Offset: 0x0004BD1C
	private void Awake()
	{
		this._showUi = base.transform.FindChild("show").gameObject;
		this.gameshow = this._showUi.transform.Find("gameshow").gameObject;
		this.topNameObj = this.gameshow.transform.FindChild("top").gameObject;
		this.lzname = this.topNameObj.transform.Find("name").GetComponent<MeshRenderer>();
		this.lzname.enabled = false;
		this.oBg = this.gameshow.transform.FindChild("bg").gameObject;
		this.oBg.SetActive(true);
		this.bgMesh = this.oBg.GetComponent<MeshRenderer>();
		this.bgMesh.enabled = false;
		this.oIco = this.gameshow.transform.FindChild("ico").gameObject;
		this.oIco.SetActive(true);
		this.IcoMesh = this.oIco.GetComponent<MeshRenderer>();
		this.IcoMesh.enabled = true;
		this.oJinYong = this.gameshow.transform.FindChild("jinyong").gameObject;
		this.oJinYong.SetActive(true);
		this.JinYongMesh = this.oJinYong.GetComponent<MeshRenderer>();
		this.JinYongMesh.enabled = false;
		base.transform.FindChild("body").GetComponent<BoxCollider>().size = new Vector3(200f, 200f, 200f);
		this.AddIco("bg", this.bgMesh, true);
		this.AddIco("jinyong", this.JinYongMesh, false);
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
	public void OnClose()
	{
		bool flag = this.JinYongMesh != null && this.JinYongMesh.material.mainTexture != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.JinYongMesh.material.mainTexture, true);
		}
		bool flag2 = this.IcoMesh != null && this.IcoMesh.material.mainTexture != null;
		if (flag2)
		{
			UnityEngine.Object.DestroyImmediate(this.IcoMesh.material.mainTexture, true);
		}
		bool flag3 = this.bgMesh != null && this.bgMesh.material.mainTexture != null;
		if (flag3)
		{
			UnityEngine.Object.DestroyImmediate(this.bgMesh.material.mainTexture, true);
		}
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
	public void setUI(DtsMapItem dtsItme, Vector3 ve)
	{
		this.DtsMapItemObj = dtsItme;
		bool flag = dtsItme.m_nItmeType == 7;
		if (flag)
		{
			bool activeSelf = this.oBg.activeSelf;
			if (activeSelf)
			{
				this.oBg.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.oBg.activeSelf;
			if (flag2)
			{
				this.oBg.SetActive(true);
			}
		}
		string sName = "<color='#00aedd'>" + dtsItme.m_sName + "</color>";
		Texture2D nameText2D = CCOpyNameManager.Instance.GetNameText2D(sName);
		this.lzname.material.mainTexture = nameText2D;
		this.lzname.enabled = true;
		this.topNameObj.SetActive(true);
		base.transform.localPosition = ve;
		this.IcoMesh.enabled = true;
		this.AddIco(this.DtsMapItemObj.m_sLoadModel, this.IcoMesh, true);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0004DEAC File Offset: 0x0004C0AC
	private void Update()
	{
		bool flag = this._showUi == null || CameraControl.Instance.controlCamera == null;
		if (!flag)
		{
			Vector3 position = base.transform.position;
			Vector3 position2 = CameraControl.Instance.controlCamera.transform.position;
			float num = Vector3.Distance(position, position2);
			bool flag2 = num <= 0.001f;
			if (!flag2)
			{
				bool flag3 = num > 100f;
				if (flag3)
				{
					bool activeSelf = this.topNameObj.activeSelf;
					if (activeSelf)
					{
						this.topNameObj.SetActive(false);
					}
				}
				else
				{
					bool flag4 = !this.topNameObj.activeSelf;
					if (flag4)
					{
						this.topNameObj.SetActive(true);
					}
					bool flag5 = !this.lzname.enabled;
					if (flag5)
					{
						this.lzname.enabled = true;
					}
					this.CheckRotaion(position, position2);
				}
			}
		}
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0004DFA8 File Offset: 0x0004C1A8
	private void CheckRotaion(Vector3 bAt, Vector3 bTo)
	{
		Vector3 normalized = (bTo - bAt).normalized;
		Quaternion rotation = Quaternion.LookRotation(normalized);
		Quaternion rotation2 = this._showUi.transform.parent.rotation;
		bool flag = this.qOldIon != rotation2 || this.v3dirOld != normalized;
		if (flag)
		{
			this._showUi.transform.rotation = rotation;
			this.qOldIon = rotation2;
			this.v3dirOld = normalized;
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0004E028 File Offset: 0x0004C228
	private void AddIco(string sIco, MeshRenderer Mesh, bool mbool)
	{
		bool flag = !(sIco == string.Empty) && !(sIco == "0");
		if (flag)
		{
			string url = "Icon/dtsitemico/" + sIco + ".txt";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadIconEndOk), new ArrayList
				{
					Mesh,
					mbool,
					sIco
				});
			}
			else
			{
				this.LoadIconEndOk(resWWW, new ArrayList
				{
					Mesh,
					mbool,
					sIco
				});
			}
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
	private void LoadIconEndOk(WWW www, object obj)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			ArrayList arrayList = (ArrayList)obj;
			string text = arrayList[2].ToString();
			MeshRenderer meshRenderer = (MeshRenderer)arrayList[0];
			bool flag2 = !(meshRenderer == null);
			if (flag2)
			{
				bool enabled = (bool)arrayList[1];
				bool flag3 = meshRenderer.material.mainTexture != null;
				if (flag3)
				{
					UnityEngine.Object.DestroyImmediate(meshRenderer.material.mainTexture, true);
				}
				meshRenderer.material.mainTexture = new Texture2D(4, 4);
				www.LoadImageIntoTexture((Texture2D)meshRenderer.material.mainTexture);
				bool flag4 = meshRenderer.gameObject.name != "ico";
				if (flag4)
				{
					meshRenderer.enabled = enabled;
				}
			}
		}
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0004E1CC File Offset: 0x0004C3CC
	public void SetState(bool mbool)
	{
		bool flag = this.JinYongMesh.enabled != mbool;
		if (flag)
		{
			this.JinYongMesh.enabled = mbool;
		}
	}

	// Token: 0x0400069C RID: 1692
	private GameObject _showUi;

	// Token: 0x0400069D RID: 1693
	private GameObject gameshow;

	// Token: 0x0400069E RID: 1694
	private GameObject topNameObj;

	// Token: 0x0400069F RID: 1695
	private MeshRenderer lzname;

	// Token: 0x040006A0 RID: 1696
	private GameObject oBg;

	// Token: 0x040006A1 RID: 1697
	private MeshRenderer bgMesh;

	// Token: 0x040006A2 RID: 1698
	private GameObject oIco;

	// Token: 0x040006A3 RID: 1699
	private MeshRenderer IcoMesh;

	// Token: 0x040006A4 RID: 1700
	private GameObject oJinYong;

	// Token: 0x040006A5 RID: 1701
	private MeshRenderer JinYongMesh;

	// Token: 0x040006A6 RID: 1702
	private DtsMapItem DtsMapItemObj;

	// Token: 0x040006A7 RID: 1703
	private Quaternion qOldIon = Quaternion.identity;

	// Token: 0x040006A8 RID: 1704
	private Vector3 v3dirOld;
}
