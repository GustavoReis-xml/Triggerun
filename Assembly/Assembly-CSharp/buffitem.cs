using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class buffitem : NetLiving
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600020D RID: 525 RVA: 0x0000D634 File Offset: 0x0000B834
	public int canTrigger
	{
		get
		{
			bool flag = this.buffinfo.m_nCD == -1;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				DateTime serverDate = Fun.GetServerDate();
				TimeSpan timeSpan = serverDate - this.buffinfo.m_dTrigger;
				result = this.buffinfo.m_nCD - (int)timeSpan.TotalSeconds;
			}
			return result;
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000D68C File Offset: 0x0000B88C
	public void createBuffItem(int nX, int nY, int nZ, int nCD, string sModel, int nBuffId, string sTrigger, int nType, int nScaleX, int nScaleY, int nScaleZ, int nRotationX, int nRotationY, int nRotationZ, int nBuffForce, int nMapEffectId)
	{
		this.buffinfo = new buffitem.buffInfo();
		this.buffinfo.m_nX = (float)nX / 100f;
		this.buffinfo.m_nY = (float)nY / 100f;
		this.buffinfo.m_nZ = (float)nZ / 100f;
		this.buffinfo.m_nCD = nCD / 1000;
		this.buffinfo.sModel = sModel;
		this.buffinfo.m_nBuffId = nBuffId;
		this.buffinfo.m_dTrigger = Convert.ToDateTime(sTrigger);
		this.buffinfo.m_nBType = nType;
		this.buffinfo.m_nScaleX = nScaleX;
		this.buffinfo.m_nScaleY = nScaleY;
		this.buffinfo.m_nScaleZ = nScaleZ;
		this.buffinfo.m_nRotationX = nRotationX;
		this.buffinfo.m_nRotationY = nRotationY;
		this.buffinfo.m_nRotationZ = nRotationZ;
		this.buffinfo.m_nBuffForce = nBuffForce;
		this.buffinfo.m_nMapEffectId = nMapEffectId;
		this.LoadStyle();
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000D79C File Offset: 0x0000B99C
	private void LoadStyle()
	{
		bool flag = this.buffinfo.m_nBType == 0;
		if (flag)
		{
			string url = "HeroAnimator/xuebao_dipan.mo";
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.InitStyle), "dizuo");
		}
		else
		{
			string url2 = "EffectRes/" + this.buffinfo.sModel + ".ef";
			GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.InitStyle), "model");
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000D814 File Offset: 0x0000BA14
	public void InitStyle(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			bool flag2 = fGameManager.GetInstance().nNowScnID < 1;
			if (flag2)
			{
				this.OnNetDestroy();
			}
			else
			{
				string text = (string)sStyle;
				Vector3 position = new Vector3(this.buffinfo.m_nX, this.buffinfo.m_nY, this.buffinfo.m_nZ);
				bool flag3 = this.thisModelParent == null;
				if (flag3)
				{
					this.thisModelParent = new GameObject(base.getNetid().ToString());
					this.thisModelParent.tag = "can_interaction";
					this.thisModelParent.layer = LayerMask.NameToLayer("BuffItem");
					buffitemManager.GetInstance().SetParent(this.thisModelParent.transform);
					this.thisModelParent.transform.position = position;
				}
				bool flag4 = text == "dizuo";
				if (flag4)
				{
					bool flag5 = this._thisDizuo != null;
					if (flag5)
					{
						UnityEngine.Object.Destroy(this._thisDizuo);
					}
					this._thisDizuo = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
					this._thisDizuo.transform.SetParent(this.thisModelParent.transform);
					this._thisDizuo.transform.localScale = Vector3.one;
					this._thisDizuo.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
					this._thisDizuo.transform.localPosition = new Vector3(0f, 0.1f, 0f);
					this._thisDizuo.name = text;
					this._thisDizuo.layer = LayerMask.NameToLayer("BuffItem");
					string url = "HeroAnimator/" + this.buffinfo.sModel + ".mo";
					GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.InitStyle), "model");
				}
				else
				{
					bool flag6 = this._thisModel != null;
					if (flag6)
					{
						UnityEngine.Object.Destroy(this._thisModel);
					}
					this._thisModel = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
					this._thisModel.transform.SetParent(this.thisModelParent.transform);
					this._thisModel.transform.localScale = new Vector3((float)this.buffinfo.m_nScaleX, (float)this.buffinfo.m_nScaleY, (float)this.buffinfo.m_nScaleZ);
					this._thisModel.transform.localEulerAngles = new Vector3((float)this.buffinfo.m_nRotationX, (float)this.buffinfo.m_nRotationY, (float)this.buffinfo.m_nRotationZ);
					this._thisModel.transform.localPosition = new Vector3(0f, 0.1f, 0f);
					this._thisModel.name = text;
					BoxCollider[] componentsInChildren = this._thisModel.GetComponentsInChildren<BoxCollider>();
					BoxCollider[] array = componentsInChildren;
					foreach (BoxCollider boxCollider in array)
					{
						boxCollider.gameObject.layer = LayerMask.NameToLayer("BuffItem");
					}
					bool flag7 = this.buffinfo.m_nBType == 1;
					if (flag7)
					{
						this.OnCheckJiDiCollider(this._thisModel);
					}
					bool flag8 = this.buffinfo.m_nBType == 0;
					if (flag8)
					{
						buffitemCon buffitemCon = this.thisModelParent.AddComponent<buffitemCon>();
						buffitemCon.setData(this);
						buffitemCon.SetUI(this.thisModelParent.transform);
					}
					this.loadOver = true;
				}
			}
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000DC0C File Offset: 0x0000BE0C
	private void OnCheckJiDiCollider(GameObject gthisModel)
	{
		BoxCollider[] componentsInChildren = gthisModel.GetComponentsInChildren<BoxCollider>();
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			BoxCollider boxCollider = componentsInChildren[i];
			bool flag = !(boxCollider == null);
			if (flag)
			{
				boxCollider.gameObject.layer = LayerMask.NameToLayer("BuffItem");
				boxCollider.name = "_jidi_";
				jidiitemUI jidiitemUI = boxCollider.gameObject.AddComponent<jidiitemUI>();
				jidiitemUI.setData(this, boxCollider, null);
			}
		}
		SphereCollider[] componentsInChildren2 = gthisModel.GetComponentsInChildren<SphereCollider>();
		num = componentsInChildren2.Length;
		for (int j = 0; j < num; j++)
		{
			SphereCollider sphereCollider = componentsInChildren2[j];
			bool flag2 = !(sphereCollider == null);
			if (flag2)
			{
				sphereCollider.gameObject.layer = LayerMask.NameToLayer("BuffItem");
				sphereCollider.name = "_jidi_";
				jidiitemUI jidiitemUI2 = sphereCollider.gameObject.AddComponent<jidiitemUI>();
				jidiitemUI2.setData(this, null, sphereCollider);
			}
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000DD09 File Offset: 0x0000BF09
	public void triggeD(string sDTime)
	{
		this.buffinfo.m_dTrigger = Convert.ToDateTime(sDTime);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000DD1D File Offset: 0x0000BF1D
	public override void OnDeath(int _attackId, int o, ArrayList forcev3, int forcemax, int weaponName, int launcherName, int m_nIsSupperKill, ArrayList m_szCanYuXiaoMie, int nDeadType, ArrayList _warData)
	{
		this.OnNetDestroy();
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000DD28 File Offset: 0x0000BF28
	public override void OnNetDestroy()
	{
		bool flag = this.thisModelParent != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.thisModelParent);
		}
		this.thisModelParent = null;
		this._thisModel = null;
		this._thisDizuo = null;
		base.OnNetDestroy();
	}

	// Token: 0x040001AF RID: 431
	public buffitem.buffInfo buffinfo;

	// Token: 0x040001B0 RID: 432
	public GameObject thisModelParent;

	// Token: 0x040001B1 RID: 433
	public GameObject _thisModel;

	// Token: 0x040001B2 RID: 434
	private GameObject _thisDizuo;

	// Token: 0x040001B3 RID: 435
	public bool loadOver;

	// Token: 0x02000527 RID: 1319
	public class buffInfo
	{
		// Token: 0x04002B97 RID: 11159
		public float m_nX;

		// Token: 0x04002B98 RID: 11160
		public float m_nY;

		// Token: 0x04002B99 RID: 11161
		public float m_nZ;

		// Token: 0x04002B9A RID: 11162
		public int m_nCD;

		// Token: 0x04002B9B RID: 11163
		public string sModel;

		// Token: 0x04002B9C RID: 11164
		public int m_nBuffId;

		// Token: 0x04002B9D RID: 11165
		public DateTime m_dTrigger;

		// Token: 0x04002B9E RID: 11166
		public int m_nBType;

		// Token: 0x04002B9F RID: 11167
		public int m_nScaleX;

		// Token: 0x04002BA0 RID: 11168
		public int m_nScaleY;

		// Token: 0x04002BA1 RID: 11169
		public int m_nScaleZ;

		// Token: 0x04002BA2 RID: 11170
		public int m_nRotationX;

		// Token: 0x04002BA3 RID: 11171
		public int m_nRotationY;

		// Token: 0x04002BA4 RID: 11172
		public int m_nRotationZ;

		// Token: 0x04002BA5 RID: 11173
		public int m_nBuffForce;

		// Token: 0x04002BA6 RID: 11174
		public int m_nMapEffectId;
	}
}
