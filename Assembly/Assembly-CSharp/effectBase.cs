using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class effectBase : MonoBehaviour
{
	// Token: 0x060009F3 RID: 2547 RVA: 0x0005AD7C File Offset: 0x00058F7C
	public virtual void AddOneEffect(string sFpsUrl, string sTpsUrl, DateTime m_dDate, int nEffectType)
	{
		bool flag = sTpsUrl == "0" || sTpsUrl == string.Empty || this.gameplayer.haveDestroy || this.gameplayer.userSpine == null || this.gameplayer._playerEffectContainer == null;
		if (!flag)
		{
			bool flag2 = !this.ThirdStateEffectInfo.ContainsKey(sTpsUrl);
			if (flag2)
			{
				EffectData effectData = new EffectData();
				effectData.Init(sFpsUrl, sTpsUrl, m_dDate, nEffectType);
				this.ThirdStateEffectInfo.Add(sTpsUrl, effectData);
				string url = "EffectRes/" + sTpsUrl + ".ef";
				WWW resWWW = GameRoot.GetResWWW(url);
				bool flag3 = resWWW == null;
				if (flag3)
				{
					GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), sTpsUrl);
				}
				else
				{
					this.loadReayPrefab(resWWW, sTpsUrl);
				}
			}
			else
			{
				EffectData effectData = this.ThirdStateEffectInfo[sTpsUrl];
				effectData.Init(sFpsUrl, sTpsUrl, m_dDate, nEffectType);
			}
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0005AE80 File Offset: 0x00059080
	public void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			bool active = false;
			bool flag2 = this.gameplayer.haveDestroy || !this.gameplayer.playerHaveCreate;
			if (!flag2)
			{
				string key = (string)sStyle;
				bool flag3 = this.ThirdStateEffectInfo.ContainsKey(key);
				if (flag3)
				{
					EffectData effectData = this.ThirdStateEffectInfo[key];
					GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), Vector3.zero, Quaternion.identity);
					int num = CFilmManager.Instance.filmForce(this.gameplayer);
					bool flag4 = this.gameplayer.playerCommon.m_nShiliServer != num;
					if (flag4)
					{
						active = true;
					}
					CObjectRender component = gameObject.GetComponent<CObjectRender>();
					bool flag5 = component != null;
					if (flag5)
					{
						component.SetEnemyFriendRenderParticle(active, false);
					}
					gameObject.transform.parent = this.gameplayer._playerEffectContainer.transform;
					gameObject.transform.position = this.getPosVe(effectData.m_sTpsUrl);
					effectData.oLoadObj = gameObject;
				}
			}
		}
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x0005AFC4 File Offset: 0x000591C4
	private Vector3 getPosVe(string sTpsUrl)
	{
		Vector3 vector = Vector3.zero;
		bool flag = this.gameplayer.haveDestroy || !this.gameplayer.playerHaveCreate;
		Vector3 result;
		if (flag)
		{
			result = vector;
		}
		else
		{
			bool flag2 = !(sTpsUrl == "hunxuan");
			if (flag2)
			{
				vector = ((this.gameplayer.bePlayer || !(this.gameplayer.userHeadTransform != null)) ? this.gameplayer.userSpine.transform.position : this.gameplayer.userHeadTransform.transform.position);
			}
			else
			{
				bool flag3 = !this.gameplayer.bePlayer && this.gameplayer.userHeadTransform == null;
				if (flag3)
				{
					vector = this.gameplayer.userSpine.transform.position;
					vector.y += (float)((Npc_Entity)this.gameplayer).SignHeight / 1000f;
				}
				else
				{
					vector = this.gameplayer.userHeadTransform.transform.position;
				}
			}
			result = vector;
		}
		return result;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0005B0E8 File Offset: 0x000592E8
	public virtual void DelOneEffect(string sFpsUrl, string sTpsUrl)
	{
		bool flag = !(sTpsUrl == "0") && this.ThirdStateEffectInfo.ContainsKey(sTpsUrl);
		if (flag)
		{
			EffectData effectData = this.ThirdStateEffectInfo[sTpsUrl];
			bool flag2 = effectData != null;
			if (flag2)
			{
				GameObject oLoadObj = effectData.oLoadObj;
				UnityEngine.Object.DestroyImmediate(oLoadObj);
			}
			this.ThirdStateEffectInfo.Remove(sTpsUrl);
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0005B14C File Offset: 0x0005934C
	protected virtual void Update()
	{
		bool flag = Time.time < this.ftime;
		if (!flag)
		{
			this.ftime = Time.time + 0.1f;
			bool flag2 = this.gameplayer.haveDestroy || !this.gameplayer.playerHaveCreate;
			if (!flag2)
			{
				bool flag3 = this.gameplayer.mModelTopObjbool != this.gameplayer._playerEffectContainer.transform.gameObject.activeSelf;
				if (flag3)
				{
					this.gameplayer._playerEffectContainer.transform.gameObject.SetActive(this.gameplayer.mModelTopObjbool);
				}
				bool flag4 = this.ThirdStateEffectInfo.Count <= 0;
				if (!flag4)
				{
					bool flag5 = this.mDelList.Count > 0;
					if (flag5)
					{
						this.mDelList.Clear();
					}
					foreach (string text in this.ThirdStateEffectInfo.Keys)
					{
						EffectData effectData = this.ThirdStateEffectInfo[text];
						Vector3 posVe = this.getPosVe(effectData.m_sTpsUrl);
						bool flag6 = effectData != null && effectData.oLoadObj != null && effectData.oLoadObj.transform.position != posVe;
						if (flag6)
						{
							effectData.oLoadObj.transform.position = posVe;
						}
						bool flag7 = effectData.m_nType == 1;
						if (flag7)
						{
							float num = (float)(Fun.GetServerDate() - effectData.m_dDate).TotalMilliseconds;
							bool flag8 = num > 300f && !this.mDelList.Contains(text);
							if (flag8)
							{
								this.mDelList.Add(text);
							}
						}
					}
					bool flag9 = this.mDelList.Count > 0;
					if (flag9)
					{
						int count = this.mDelList.Count;
						for (int i = 0; i < count; i++)
						{
							string key = this.mDelList[i].ToString();
							EffectData effectData = this.ThirdStateEffectInfo[key];
							this.DelOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl);
						}
					}
				}
			}
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0005B3C4 File Offset: 0x000595C4
	public virtual void OnDeadClearEffect()
	{
		ArrayList arrayList = new ArrayList();
		arrayList = new ArrayList(this.ThirdStateEffectInfo.Keys);
		while (arrayList.Count > 0)
		{
			string key = arrayList[0].ToString();
			EffectData effectData = this.ThirdStateEffectInfo[key];
			arrayList.RemoveAt(0);
			this.DelOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl);
		}
	}

	// Token: 0x04000813 RID: 2067
	public Dictionary<string, EffectData> ThirdStateEffectInfo = new Dictionary<string, EffectData>();

	// Token: 0x04000814 RID: 2068
	public NetLiving gameplayer;

	// Token: 0x04000815 RID: 2069
	private float ftime;

	// Token: 0x04000816 RID: 2070
	private List<string> mDelList = new List<string>();
}
