using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000083 RID: 131
public class ChatLaBaCon : Window
{
	// Token: 0x06000462 RID: 1122 RVA: 0x000227CB File Offset: 0x000209CB
	protected override void Awake()
	{
		base.Awake();
		ChatLaBaCon.inst = this;
		this.m_nUserType = fGameManager.m_nUserType;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000227E6 File Offset: 0x000209E6
	private void Start()
	{
		base.StartCoroutine(this.OnSetSizeDelta());
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x000227F6 File Offset: 0x000209F6
	private IEnumerator OnSetSizeDelta()
	{
		GameObject oobj = base.gameObject;
		yield return new WaitForSeconds(0.03f);
		ArrayList szLanZuanInfo = (ArrayList)this.m_szData[8];
		bool flag = szLanZuanInfo.Count > 1 && szLanZuanInfo[2].ToString() == Fun.GetQQ_Zuan_Type(null);
		if (flag)
		{
			string sImgName = szLanZuanInfo[0].ToString();
			bool flag2 = !string.IsNullOrEmpty(sImgName);
			if (flag2)
			{
				GameObject pImg = oobj.transform.FindChild("NamePanel/buleImg").gameObject;
				pImg.SetActive(true);
				pImg.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, sImgName);
				pImg = null;
			}
			sImgName = szLanZuanInfo[1].ToString();
			bool flag3 = !string.IsNullOrEmpty(sImgName);
			if (flag3)
			{
				GameObject pImg2 = oobj.transform.FindChild("NamePanel/buleYearImg").gameObject;
				pImg2.SetActive(true);
				pImg2.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, sImgName);
				pImg2 = null;
			}
			sImgName = null;
		}
		GameObject pName = base.transform.FindChild("NamePanel").gameObject;
		pName.SetActive(false);
		pName.SetActive(true);
		yield break;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00022805 File Offset: 0x00020A05
	public void InitData(string sNick, string sMsg, ArrayList szData, Text pMsgText)
	{
		this.m_szData = szData;
		this.m_fCreateTime = Time.time;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0002281C File Offset: 0x00020A1C
	private void Update()
	{
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			ChatManager.AllLaBaList.Clear();
			this.OnDeleteThis();
		}
		else
		{
			bool flag = this.m_nUserType != fGameManager.m_nUserType;
			if (flag)
			{
				this.m_fLaBaTimeLong = 0f;
			}
			bool flag2 = Time.time - this.m_fCreateTime >= this.m_fLaBaTimeLong;
			if (flag2)
			{
				GameObject gameObject = base.gameObject;
				Tweener t = gameObject.transform.transform.DOScaleY(0f, 0.2f);
				t.OnComplete(delegate
				{
					this.OnDeleteThis();
				});
			}
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000228C0 File Offset: 0x00020AC0
	private void OnDeleteThis()
	{
		ChatManager.ShowLaBaList.Remove(base.gameObject);
		bool flag = ChatManager.AllLaBaList.Count > 0;
		if (flag)
		{
			ArrayList arrayList = ChatManager.AllLaBaList[0];
			ChatManager.AllLaBaList.RemoveAt(0);
			ChatUICon.inst.OnLaBaChat(arrayList[0].ToString(), arrayList[1].ToString(), (ArrayList)arrayList[2]);
		}
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04000364 RID: 868
	public static ChatLaBaCon inst;

	// Token: 0x04000365 RID: 869
	public float m_fLaBaTimeLong = 15f;

	// Token: 0x04000366 RID: 870
	private float m_fCreateTime;

	// Token: 0x04000367 RID: 871
	private ArrayList m_szData;

	// Token: 0x04000368 RID: 872
	private int m_nUserType;
}
