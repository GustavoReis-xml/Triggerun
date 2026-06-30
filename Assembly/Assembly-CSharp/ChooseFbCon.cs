using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000091 RID: 145
public class ChooseFbCon : Window
{
	// Token: 0x0600050D RID: 1293 RVA: 0x00029AC0 File Offset: 0x00027CC0
	protected override void Awake()
	{
		base.Awake();
		ChooseFbCon.inst = this;
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = base.transform.Find("Title/Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component = this.UIGameObj.Get_GameObj(0).transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			component = this.UIGameObj.Get_GameObj(1).transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(0)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickJiaoXue);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(1)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBaChang);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(0)).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(0)).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(1)).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverGroup);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(1)).onExit = new EventTriggerListener.VoidDelegate(this.OnExitGroup);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(1)).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(0)).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.Reset();
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00029C84 File Offset: 0x00027E84
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, new MainUICon.BackBtnDelegate(this.OnClickBack), null);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00029CC4 File Offset: 0x00027EC4
	private void OnClickBack(object szParam)
	{
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(0, null);
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00029CFF File Offset: 0x00027EFF
	private void OnClickJiaoXue(GameObject obj)
	{
		MatchManager.GetInstance().OnEnterLearnFb();
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00029D0D File Offset: 0x00027F0D
	private void OnClickBaChang(GameObject obj)
	{
		MatchManager.GetInstance().OnEnterTrain();
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00029D1B File Offset: 0x00027F1B
	private void OnOverGroup(GameObject go)
	{
		go.transform.Find("Image").GetComponent<Image>().enabled = true;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00029D3A File Offset: 0x00027F3A
	private void OnExitGroup(GameObject go)
	{
		go.transform.Find("Image").GetComponent<Image>().enabled = false;
	}

	// Token: 0x040003EB RID: 1003
	public static ChooseFbCon inst;
}
