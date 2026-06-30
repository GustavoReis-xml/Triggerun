using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000035 RID: 53
public class BroadCastMSGCon : Window
{
	// Token: 0x060001E9 RID: 489 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
	private void Start()
	{
		BroadCastMSGCon.inst = this;
		this.canShowNextMsg = true;
		this.fadeOut = true;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
	protected override void Awake()
	{
		base.Awake();
		this.BG = this.UIGameObj.Get_GameObj(0).GetComponent<Image>();
		this.list = this.UIGameObj.Get_GameObj(1);
		this.content = this.UIGameObj.Get_GameObj(2).gameObject;
		this.bgcolor = this.BG.color;
		this.BG.color = new Color(this.bgcolor.r, this.bgcolor.g, this.bgcolor.b, 0f);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000C678 File Offset: 0x0000A878
	private void Update()
	{
		bool flag = this.canShowNextMsg && this.broadMsgs.Count > 0;
		if (flag)
		{
			this.AddContent(this.broadMsgs[0], this.radioTypes[0]);
			this.broadMsgs.RemoveAt(0);
			this.radioTypes.RemoveAt(0);
			this.canShowNextMsg = false;
		}
		bool flag2 = this.fadeIn && this.list.transform.childCount == 0;
		if (flag2)
		{
			this.FadeOut();
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000C710 File Offset: 0x0000A910
	public void AddMSG(string msg, int nRadioType)
	{
		msg = msg.Replace("&lt;", "<").Replace("&gt;", ">").Replace("\\\\n", "\n");
		bool flag = !this.fadeIn;
		if (flag)
		{
			this.FadeIn();
		}
		this.broadMsgs.Add(msg);
		this.radioTypes.Add(nRadioType);
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000C780 File Offset: 0x0000A980
	public void AddContent(string text, int radioType)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.content);
		gameObject.transform.SetParent(this.list.transform);
		gameObject.transform.localPosition = this.content.transform.localPosition;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		LinkImageText component = gameObject.GetComponent<LinkImageText>();
		component.m_PreText = text;
		component.SetVerticesDirty();
		component.onHrefClick.AddListener(new UnityAction<string>(this.OpenCustomeUrl));
		this.FadeIn();
		MovingContent m = gameObject.AddComponent<MovingContent>();
		base.StartCoroutine(this.setSystemMsg(text, gameObject, m, radioType));
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000C840 File Offset: 0x0000AA40
	private IEnumerator setSystemMsg(string text, GameObject go, MovingContent m, int radioType)
	{
		yield return new WaitForSeconds(0.5f);
		m.setContentWidth(go.GetComponent<RectTransform>().sizeDelta.x);
		go.SetActive(true);
		bool flag = radioType == 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(text, ChatColor.system, "system");
		}
		yield break;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000C86C File Offset: 0x0000AA6C
	public void FadeIn()
	{
		this.fadeIn = true;
		this.fadeOut = false;
		this.BG.DOColor(this.bgcolor, 2f);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000C894 File Offset: 0x0000AA94
	public void FadeOut()
	{
		this.fadeOut = true;
		this.fadeIn = false;
		this.BG.DOFade(0f, 1f);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000C8BB File Offset: 0x0000AABB
	public void ShowNextMsg()
	{
		this.canShowNextMsg = true;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000C8C5 File Offset: 0x0000AAC5
	private void OpenCustomeUrl(string hrefName)
	{
		Debug.Log("click on: " + hrefName);
		Debuger.OpenURL(hrefName);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
	private void OnGUI()
	{
	}

	// Token: 0x0400018E RID: 398
	public static BroadCastMSGCon inst;

	// Token: 0x0400018F RID: 399
	private List<string> broadMsgs = new List<string>();

	// Token: 0x04000190 RID: 400
	private List<int> radioTypes = new List<int>();

	// Token: 0x04000191 RID: 401
	private Image BG;

	// Token: 0x04000192 RID: 402
	private GameObject content;

	// Token: 0x04000193 RID: 403
	private GameObject list;

	// Token: 0x04000194 RID: 404
	private Color bgcolor;

	// Token: 0x04000195 RID: 405
	private bool canShowNextMsg;

	// Token: 0x04000196 RID: 406
	public bool fadeOut;

	// Token: 0x04000197 RID: 407
	private bool fadeIn;
}
