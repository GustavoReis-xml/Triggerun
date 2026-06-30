using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class CEffectPools : MonoBehaviour
{
	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060003CE RID: 974 RVA: 0x0001E448 File Offset: 0x0001C648
	// (set) Token: 0x060003CF RID: 975 RVA: 0x0001E45F File Offset: 0x0001C65F
	public static CEffectPools Instance
	{
		get
		{
			return CEffectPools._instance;
		}
		set
		{
			CEffectPools._instance = value;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001E468 File Offset: 0x0001C668
	public int allNumber
	{
		get
		{
			return this._allNumber;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001E470 File Offset: 0x0001C670
	public bool isOpen
	{
		get
		{
			return this._isOpen;
		}
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0001E478 File Offset: 0x0001C678
	private void Start()
	{
		CEffectPools.Instance = this;
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001E482 File Offset: 0x0001C682
	public void InitPools()
	{
		this.dictPools = new Dictionary<int, CObjectPool>();
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0001E490 File Offset: 0x0001C690
	private Transform GetTransFormFromPools(int GID)
	{
		bool flag = !this.dictPools.ContainsKey(GID);
		Transform result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.dictPools[GID].getOne();
		}
		return result;
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0001E4CB File Offset: 0x0001C6CB
	private IEnumerator ICount()
	{
		do
		{
			yield return new WaitForSeconds(0.5f);
		}
		while (Time.time - this.idleTime <= 1f);
		this._allNumber = 0;
		foreach (int key in this.dictPools.Keys)
		{
			this._allNumber += this.dictPools[key].transFormFree.Count;
		}
		Dictionary<int, CObjectPool>.KeyCollection.Enumerator enumerator = default(Dictionary<int, CObjectPool>.KeyCollection.Enumerator);
		yield break;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001E4DC File Offset: 0x0001C6DC
	public void PutInPools(int GID, Transform tf)
	{
		bool isOpen = this.isOpen;
		if (isOpen)
		{
			bool flag = !this.dictPools.ContainsKey(GID);
			if (flag)
			{
				this.dictPools.Add(GID, new CObjectPool(GID));
			}
			this.dictPools[GID].PutOne(tf);
			Rigidbody[] componentsInChildren = tf.gameObject.GetComponentsInChildren<Rigidbody>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].velocity = Vector3.zero;
				componentsInChildren[i].angularVelocity = Vector3.zero;
			}
			tf.gameObject.SetActive(false);
		}
		else
		{
			UnityEngine.Object.Destroy(tf.gameObject);
		}
		this.idleTime = Time.time;
		bool flag2 = this.showNumber;
		if (flag2)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.ICount());
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0001E5BC File Offset: 0x0001C7BC
	public GameObject CreateGameObject(UnityEngine.Object obj)
	{
		return this.CreateGameObject(obj, Vector3.one, Quaternion.identity);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
	public GameObject CreateGameObject(UnityEngine.Object obj, Vector3 pos, Quaternion qua)
	{
		int instanceID = obj.GetInstanceID();
		Transform transFormFromPools = this.GetTransFormFromPools(instanceID);
		bool flag = this.isOpen && transFormFromPools != null;
		GameObject gameObject;
		if (flag)
		{
			gameObject = transFormFromPools.gameObject;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = qua;
			gameObject.SetActive(true);
		}
		else
		{
			gameObject = (GameObject)UnityEngine.Object.Instantiate(obj, pos, qua);
		}
		return gameObject;
	}

	// Token: 0x0400031B RID: 795
	public bool _isOpen = true;

	// Token: 0x0400031C RID: 796
	private static CEffectPools _instance;

	// Token: 0x0400031D RID: 797
	private Dictionary<int, CObjectPool> dictPools = new Dictionary<int, CObjectPool>();

	// Token: 0x0400031E RID: 798
	private int _allNumber;

	// Token: 0x0400031F RID: 799
	private bool showNumber;

	// Token: 0x04000320 RID: 800
	private float idleTime;
}
