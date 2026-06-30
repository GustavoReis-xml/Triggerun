using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class VoiceTalkCon : Window
{
	// Token: 0x06002D54 RID: 11604 RVA: 0x0018EAC2 File Offset: 0x0018CCC2
	private void Start()
	{
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x0018EAC5 File Offset: 0x0018CCC5
	private void Update()
	{
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x0018EAC8 File Offset: 0x0018CCC8
	protected override void Awake()
	{
		base.Awake();
		VoiceTalkCon.inst = this;
		this.Content = this.UIGameObj.Get_GameObj(0);
		this.UserObj = this.UIGameObj.Get_GameObj(1);
		this.UserObj.SetActive(false);
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x0018EB14 File Offset: 0x0018CD14
	public void SetTalk(string nCid, int nIsPlay)
	{
		bool flag = nIsPlay == 1;
		if (flag)
		{
			bool flag2 = !this.ObjArr.ContainsKey(nCid);
			if (flag2)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UserObj);
				gameObject.transform.SetParent(this.UserObj.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				UserVoiceModel userVoiceModel = gameObject.AddComponent<UserVoiceModel>();
				userVoiceModel.Init(nCid);
				bool flag3 = int.Parse(nCid) == PlayerDataManager.getInstance().HUserData.m_nCid && VoiceGameCon.MyMai;
				if (flag3)
				{
					userVoiceModel.SetMai();
				}
				else
				{
					userVoiceModel.SetPlay();
				}
				gameObject.name = nCid;
				gameObject.SetActive(true);
				this.ObjArr.Add(nCid, gameObject);
			}
			else
			{
				GameObject gameObject = this.ObjArr[nCid];
				bool flag4 = gameObject != null;
				if (flag4)
				{
					UserVoiceModel component = gameObject.GetComponent<UserVoiceModel>();
					component.SetPlay();
					gameObject.SetActive(true);
				}
			}
		}
		bool flag5 = nIsPlay == 2 && (int.Parse(nCid) != PlayerDataManager.getInstance().HUserData.m_nCid || !VoiceGameCon.MyMai) && this.ObjArr.ContainsKey(nCid);
		if (flag5)
		{
			UnityEngine.Object.Destroy(this.ObjArr[nCid]);
			this.ObjArr.Remove(nCid);
		}
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x0018ECA4 File Offset: 0x0018CEA4
	public void DelAllUser()
	{
		foreach (GameObject obj in this.ObjArr.Values)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.ObjArr.Clear();
		foreach (object obj2 in this.Content.transform)
		{
			Transform transform = (Transform)obj2;
			bool flag = transform.gameObject.name != "uservoice";
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x0400266B RID: 9835
	public static VoiceTalkCon inst;

	// Token: 0x0400266C RID: 9836
	private GameObject Content;

	// Token: 0x0400266D RID: 9837
	private GameObject UserObj;

	// Token: 0x0400266E RID: 9838
	private Dictionary<string, GameObject> ObjArr = new Dictionary<string, GameObject>();
}
