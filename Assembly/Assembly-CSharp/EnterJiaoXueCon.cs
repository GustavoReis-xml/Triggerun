using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200011B RID: 283
public class EnterJiaoXueCon : Window
{
	// Token: 0x06000A41 RID: 2625 RVA: 0x0005C8B8 File Offset: 0x0005AAB8
	protected override void Awake()
	{
		base.Awake();
		EnterJiaoXueCon.inst = this;
		this.uiJiaoXue = this.UIGameObj.Get_GameObj(0);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = this.uiJiaoXue.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
		}
		this.uiChooseFb = this.UIGameObj.Get_GameObj(1);
		this.UIGameObj.Get_GameObj(2).GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnContinue));
		this.UIGameObj.Get_GameObj(3).GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClickJiaoXue));
		this.UIGameObj.Get_GameObj(4).GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClickJiaoXue));
		this.UIGameObj.Get_GameObj(5).GetComponent<Button>().onClick.AddListener(new UnityAction(this.GoToXunLianChang));
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0005C9D4 File Offset: 0x0005ABD4
	public void ShowInfo(int nType)
	{
		bool flag = nType == 1;
		if (flag)
		{
			this.uiJiaoXue.SetActive(true);
			this.uiChooseFb.SetActive(false);
		}
		else
		{
			this.uiJiaoXue.SetActive(false);
			this.uiChooseFb.SetActive(true);
		}
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0005CA24 File Offset: 0x0005AC24
	private void OnClickJiaoXue()
	{
		MatchManager.GetInstance().OnEnterLearnFb();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0005CA34 File Offset: 0x0005AC34
	private void OnContinue()
	{
		this.Close();
		fGameManager.IsNeedShowPiPeiYinDao = true;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.InitFirstYinDao();
		}
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0005CA6B File Offset: 0x0005AC6B
	private void GoToXunLianChang()
	{
		MatchManager.GetInstance().OnEnterTrain();
	}

	// Token: 0x04000847 RID: 2119
	public static EnterJiaoXueCon inst;

	// Token: 0x04000848 RID: 2120
	private GameObject uiJiaoXue;

	// Token: 0x04000849 RID: 2121
	private GameObject uiChooseFb;
}
