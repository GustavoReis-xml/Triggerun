using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B1 RID: 1201
public class XinShouJiaoXueCon : Window
{
	// Token: 0x06002E1A RID: 11802 RVA: 0x00198428 File Offset: 0x00196628
	protected override void Awake()
	{
		base.Awake();
		XinShouJiaoXueCon.inst = this;
		this.uiTargetObj = base.transform.Find("RightMsg").gameObject;
		this.uiCenterMsg = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.uiTarget = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		bool flag = WebParam.client_language == "zh";
		if (flag)
		{
			this.uiShengLiEff = this.UIGameObj.Get_GameObj(4);
		}
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			this.uiShengLiEff = this.UIGameObj.Get_GameObj(6);
		}
		this.uiBlackBg = this.UIGameObj.Get_GameObj(5);
		this.HLG = base.transform.Find("RightMsg/Target").GetComponent<HorizontalLayoutGroup>();
		this.Reset();
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x00198510 File Offset: 0x00196710
	public override void Reset()
	{
		base.Reset();
		this.HideTarget();
		this.uiShengLiEff.SetActive(false);
		this.uiBlackBg.SetActive(false);
		bool flag = this.NowStep == null;
		if (flag)
		{
			LearnStepData nowStep = PlayerLearnManager.GetInstance().GetNowStep();
			bool flag2 = nowStep != null;
			if (flag2)
			{
				this.StartMsg(nowStep);
			}
		}
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x00198574 File Offset: 0x00196774
	public void StartMsg(LearnStepData Step)
	{
		bool flag = this.NowStep == null || this.NowStep.id != Step.id;
		if (flag)
		{
			this.CancelAllInvoke();
			this.uiCenterMsg.text = string.Empty;
			this.NowStep = Step;
			bool flag2 = this.NowStep.szShowMsg.Count > 0;
			if (flag2)
			{
				this.nShowMsgIndex = 0;
				this.ShowCenterMsg();
			}
		}
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x001985F0 File Offset: 0x001967F0
	private void ShowCenterMsg()
	{
		ArrayList arrayList = (ArrayList)this.NowStep.szShowMsg[this.nShowMsgIndex];
		string text = arrayList[1].ToString();
		string text2 = arrayList[0].ToString();
		float num = (float)arrayList[2];
		this.uiCenterMsg.text = Language.GetLang(51014, new object[]
		{
			text
		});
		fUISoundManager.instance.PlayUISound(text2, 0, null, 20);
		this.szSound.Add(text2);
		bool flag = base.IsInvoking("CenterMsgTime");
		if (flag)
		{
			base.CancelInvoke("CenterMsgTime");
		}
		base.Invoke("CenterMsgTime", num / 1000f);
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x001986B0 File Offset: 0x001968B0
	private void CenterMsgTime()
	{
		this.nShowMsgIndex++;
		this.uiCenterMsg.text = string.Empty;
		bool flag = this.nShowMsgIndex >= this.NowStep.szShowMsg.Count;
		if (flag)
		{
			bool flag2 = base.IsInvoking("TipTime");
			if (flag2)
			{
				base.CancelInvoke("TipTime");
			}
			bool flag3 = this.NowStep.comtype == 0;
			if (flag3)
			{
				PlayerLearnManager.GetInstance().OnStepEnd();
			}
			else
			{
				bool flag4 = this.NowStep.szTip.Count > 0;
				if (flag4)
				{
					float num = (float)this.NowStep.szTip[3];
					base.InvokeRepeating("TipTime", num / 1000f, num / 1000f);
				}
			}
		}
		else
		{
			this.ShowCenterMsg();
		}
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x00198797 File Offset: 0x00196997
	private void TipTime()
	{
		this.ShowWaitMsg();
		this.ShowTarget();
	}

	// Token: 0x06002E20 RID: 11808 RVA: 0x001987A8 File Offset: 0x001969A8
	private void ShowWaitMsg()
	{
		string text = this.NowStep.szTip[0].ToString();
		string text2 = this.NowStep.szTip[1].ToString();
		float num = (float)this.NowStep.szTip[2];
		this.uiCenterMsg.text = Language.GetLang(51014, new object[]
		{
			text2
		});
		fUISoundManager.instance.PlayUISound(text, 0, null, 20);
		this.szSound.Add(text);
		bool flag = base.IsInvoking("WaitMsgOverTime");
		if (flag)
		{
			base.CancelInvoke("WaitMsgOverTime");
		}
		base.Invoke("WaitMsgOverTime", num / 1000f);
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x00198868 File Offset: 0x00196A68
	private void WaitMsgOverTime()
	{
		this.uiCenterMsg.text = string.Empty;
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x0019887C File Offset: 0x00196A7C
	private void ShowTarget()
	{
		this.uiTargetObj.SetActive(true);
		this.uiTarget.text = this.NowStep.name;
		this.HLG.SetLayoutHorizontal();
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x001988AF File Offset: 0x00196AAF
	private void HideTarget()
	{
		this.uiTargetObj.SetActive(false);
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x001988C0 File Offset: 0x00196AC0
	private void CancelAllInvoke()
	{
		bool flag = base.IsInvoking("CenterMsgTime");
		if (flag)
		{
			base.CancelInvoke("CenterMsgTime");
		}
		bool flag2 = base.IsInvoking("TipTime");
		if (flag2)
		{
			base.CancelInvoke("TipTime");
		}
		bool flag3 = base.IsInvoking("WaitMsgOverTime");
		if (flag3)
		{
			base.CancelInvoke("WaitMsgOverTime");
		}
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x00198928 File Offset: 0x00196B28
	public void StepOver()
	{
		this.CancelAllInvoke();
		this.HideTarget();
		this.uiCenterMsg.text = string.Empty;
		int count = this.szSound.Count;
		for (int i = 0; i < count; i++)
		{
			bool flag = fUISoundManager.instance.SoundIsPlay(this.szSound[i]);
			if (flag)
			{
				fUISoundManager.instance.StopSound(this.szSound[i], 0);
			}
		}
		this.szSound.Clear();
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x001989B4 File Offset: 0x00196BB4
	public void ShowEndEff()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[881];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		this.uiShengLiEff.SetActive(true);
		this.uiBlackBg.SetActive(true);
		bool flag = base.IsInvoking("HideBlackEff");
		if (flag)
		{
			base.CancelInvoke("HideBlackEff");
		}
		base.Invoke("HideBlackEff", 3.5f);
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x00198A34 File Offset: 0x00196C34
	private void HideBlackEff()
	{
		this.uiShengLiEff.SetActive(false);
		this.uiBlackBg.SetActive(false);
		PlayerLearnManager.GetInstance().CloseLearn();
		fUIManager.GetInstance().GotoHall(0);
	}

	// Token: 0x04002788 RID: 10120
	public static XinShouJiaoXueCon inst;

	// Token: 0x04002789 RID: 10121
	private Text uiCenterMsg;

	// Token: 0x0400278A RID: 10122
	private Text uiTarget;

	// Token: 0x0400278B RID: 10123
	private GameObject uiTargetObj;

	// Token: 0x0400278C RID: 10124
	private LearnStepData NowStep;

	// Token: 0x0400278D RID: 10125
	private int nShowMsgIndex = -1;

	// Token: 0x0400278E RID: 10126
	private List<string> szSound = new List<string>();

	// Token: 0x0400278F RID: 10127
	private HorizontalLayoutGroup HLG;

	// Token: 0x04002790 RID: 10128
	private GameObject uiShengLiEff;

	// Token: 0x04002791 RID: 10129
	private GameObject uiBlackBg;
}
