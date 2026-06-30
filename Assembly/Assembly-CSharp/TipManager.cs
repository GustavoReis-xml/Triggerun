using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047B RID: 1147
public class TipManager
{
	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x06002C7D RID: 11389 RVA: 0x0018726C File Offset: 0x0018546C
	// (set) Token: 0x06002C7E RID: 11390 RVA: 0x00187284 File Offset: 0x00185484
	public bool isPopTip
	{
		get
		{
			return this._isPopTip;
		}
		set
		{
			this._isPopTip = value;
		}
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x00187290 File Offset: 0x00185490
	public static TipManager GetInstance()
	{
		bool flag = TipManager.instance == null;
		if (flag)
		{
			TipManager.instance = new TipManager();
		}
		return TipManager.instance;
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x001872C0 File Offset: 0x001854C0
	public void ShowTextTip(string sText, TextAnchor align = TextAnchor.MiddleLeft, float nline = 1f, float xpos = 20f, float ypos = -20f)
	{
		sText = "<color=#cfb9a1>" + sText + "</color>";
		ArrayList szParam = new ArrayList
		{
			sText,
			align,
			nline,
			xpos,
			ypos
		};
		this.isTextTip = true;
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			this.TipLoadOk("easy_tip", easy_tipCon.inst.gameObject, szParam);
		}
		else
		{
			fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.TipLoadOk), szParam, true);
		}
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x00187378 File Offset: 0x00185578
	public void HideTextTip()
	{
		this.isTextTip = false;
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			easy_tipCon.inst.HideTextTip();
		}
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x001873AC File Offset: 0x001855AC
	private void TipLoadOk(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = easy_tipCon.inst != null;
		if (flag)
		{
			bool flag2 = !this.isTextTip;
			if (flag2)
			{
				easy_tipCon.inst.HideTextTip();
			}
			else
			{
				ArrayList arrayList = (ArrayList)szParam;
				easy_tipCon.inst.ShowTextTip(arrayList[0].ToString(), (TextAnchor)arrayList[1], (float)arrayList[2], (float)arrayList[3], (float)arrayList[4]);
			}
		}
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x00187434 File Offset: 0x00185634
	public void ShowComTipMsg(string title, string content, float delay = 2f, TipManager.TipOKDelegate okfun = null, object okParam = null)
	{
		bool flag = this.delayTipData == null || !(this.delayTipData.szInfo[0].ToString() == title) || !(this.delayTipData.szInfo[1].ToString() == content);
		if (flag)
		{
			this.delayTipData = new TipMsgData(4, 0, new ArrayList
			{
				title,
				content,
				delay
			});
			this.delayTipData.SetBackFunc(okfun, okParam, null, null, null, null, null);
			this.isPopTip = true;
			GameRoot.inst.Setting.Load_Setting_Data_State = false;
			bool flag2 = easy_tipCon.inst != null;
			if (flag2)
			{
				this.ComTipLoadOk("easy_tip", easy_tipCon.inst.gameObject, this.delayTipData);
			}
			else
			{
				fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.ComTipLoadOk), this.delayTipData, true);
			}
		}
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x00187548 File Offset: 0x00185748
	public void ShowRelinkTip(string title, string content, int tipid, int delay, string canceltxt, TipManager.TipOKDelegate okfun = null, object okParam = null, TipManager.TipCancelDelegate cancelfun = null, object cancelParam = null)
	{
		bool flag = this.relinkData == null || !(title == this.relinkData.szInfo[0].ToString()) || !(content == this.relinkData.szInfo[1].ToString());
		if (flag)
		{
			this.relinkData = new TipMsgData(7, 0, new ArrayList
			{
				title,
				content,
				string.Empty,
				canceltxt
			});
			this.relinkData.SetBackFunc(okfun, new ArrayList
			{
				okParam,
				tipid,
				delay
			}, cancelfun, cancelParam, null, null, null);
			this.isPopTip = true;
			GameRoot.inst.Setting.Load_Setting_Data_State = false;
			bool flag2 = easy_tipCon.inst != null;
			if (flag2)
			{
				this.ComTipLoadOk("easy_tip", easy_tipCon.inst.gameObject, this.relinkData);
			}
			else
			{
				fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.ComTipLoadOk), this.relinkData, true);
			}
		}
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x0018768C File Offset: 0x0018588C
	public void ShowComTipMsg(string title, string content, string oktxt, string canceltxt, string stip = "", TipManager.TipOKDelegate okfun = null, object okParam = null, TipManager.TipCancelDelegate cancelfun = null, object cancelParam = null)
	{
		this.ShowComTipMsg(title, content, oktxt, canceltxt, okfun, okParam, cancelfun, cancelParam, string.Empty, false, null, 1, string.Empty, string.Empty, stip);
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x001876C4 File Offset: 0x001858C4
	public void ShowComTipMsg(string title, string content, string oktxt, string canceltxt, TipManager.TipOKDelegate okfun, object okParam, TipManager.TipCancelDelegate cancelfun, object cancelParam, string toggletxt = "", bool toggleon = false, TipManager.TipToggleDelegate togglefun = null, int tipType = 1, string sreward = "", string simg = "", string stip = "")
	{
		TipMsgData tipMsgData = new TipMsgData(tipType, 0, new ArrayList
		{
			title,
			content,
			oktxt,
			canceltxt,
			toggletxt,
			toggleon,
			sreward,
			simg,
			stip
		});
		tipMsgData.SetBackFunc(okfun, okParam, cancelfun, cancelParam, togglefun, null, null);
		bool flag = !this.allTipMsg.Contains(tipMsgData);
		if (flag)
		{
			this.allTipMsg.Add(tipMsgData);
			this.allTipMsg.Sort();
			this.OnCheckTip();
		}
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x0018777C File Offset: 0x0018597C
	public void ShowRewardTip(int ntype, ArrayList szinfo, TipManager.TipOKDelegate okfunc = null, object okparam = null)
	{
		TipMsgData tipMsgData = new TipMsgData(2, ntype, szinfo);
		tipMsgData.SetBackFunc(okfunc, okparam, null, null, null, null, null);
		bool flag = !this.allTipMsg.Contains(tipMsgData);
		if (flag)
		{
			this.allTipMsg.Add(tipMsgData);
			this.allTipMsg.Sort();
			this.OnCheckTip();
		}
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x001877D8 File Offset: 0x001859D8
	public void ShowItemTip(string title, string content, ArrayList szitem, string oktxt, TipManager.TipOKDelegate okfunc = null, object okparam = null)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < szitem.Count; i += 2)
		{
			arrayList.Add(szitem[i]);
			arrayList.Add(szitem[i + 1]);
			bool flag = arrayList.Count == 20;
			if (flag)
			{
				TipMsgData tipMsgData = new TipMsgData(5, 1, new ArrayList
				{
					title,
					content,
					arrayList,
					oktxt
				});
				tipMsgData.SetBackFunc(okfunc, okparam, null, null, null, null, null);
				bool flag2 = !this.allTipMsg.Contains(tipMsgData);
				if (flag2)
				{
					this.allTipMsg.Add(tipMsgData);
				}
				arrayList = new ArrayList();
			}
		}
		bool flag3 = arrayList.Count > 0;
		if (flag3)
		{
			TipMsgData tipMsgData2 = new TipMsgData(5, 1, new ArrayList
			{
				title,
				content,
				arrayList,
				oktxt
			});
			tipMsgData2.SetBackFunc(okfunc, okparam, null, null, null, null, null);
			bool flag4 = !this.allTipMsg.Contains(tipMsgData2);
			if (flag4)
			{
				this.allTipMsg.Add(tipMsgData2);
			}
		}
		this.allTipMsg.Sort();
		this.OnCheckTip();
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x0018792C File Offset: 0x00185B2C
	public void ShowInputTip(string title, string inputtip, string oktxt, string canceltxt, TipManager.TipInputDelegate okfun = null, object okparam = null, TipManager.TipCancelDelegate cancelfun = null, object cancelparam = null, int length = 0, float width = 272f, float height = 36f)
	{
		TipMsgData tipMsgData = new TipMsgData(10, 1, new ArrayList
		{
			title,
			inputtip,
			oktxt,
			canceltxt,
			length,
			width,
			height
		});
		tipMsgData.SetBackFunc(null, null, cancelfun, cancelparam, null, okfun, okparam);
		bool flag = !this.allTipMsg.Contains(tipMsgData);
		if (flag)
		{
			this.allTipMsg.Add(tipMsgData);
			this.allTipMsg.Sort();
			this.OnCheckTip();
		}
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x001879DC File Offset: 0x00185BDC
	public void OnCheckTip()
	{
		bool flag = this.allTipMsg.Count == 0 || this.isPopTip;
		if (!flag)
		{
			TipMsgData tipMsgData = this.allTipMsg[0];
			bool flag2 = (tipMsgData.tipType != 2 && tipMsgData.tipType != 3 && tipMsgData.tipType != 5 && tipMsgData.tipType != 6) || fGameManager.m_nUserType != 1;
			if (flag2)
			{
				this.isPopTip = true;
				GameRoot.inst.Setting.Load_Setting_Data_State = false;
				bool flag3 = easy_tipCon.inst != null;
				if (flag3)
				{
					this.ComTipLoadOk("easy_tip", easy_tipCon.inst.gameObject, tipMsgData);
				}
				else
				{
					fUIManager.GetInstance().ShowWin("easy_tip", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.ComTipLoadOk), tipMsgData, true);
				}
			}
		}
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x00187AB4 File Offset: 0x00185CB4
	private void ComTipLoadOk(string sUI, GameObject UIObj, object szParam)
	{
		TipMsgData tipMsgData = (TipMsgData)szParam;
		bool flag = this.relinkData != null;
		if (flag)
		{
			tipMsgData = this.relinkData;
		}
		bool flag2 = this.delayTipData != null;
		if (flag2)
		{
			tipMsgData = this.delayTipData;
		}
		bool flag3 = !this.isPopTip;
		if (flag3)
		{
			easy_tipCon.inst.ClearPopTip(true);
		}
		else
		{
			switch (tipMsgData.tipType)
			{
			case 1:
			case 3:
			case 8:
				easy_tipCon.inst.ShowCommonTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), tipMsgData.szInfo[2].ToString(), tipMsgData.szInfo[3].ToString(), new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData, new easy_tipCon.TipCancelDelegate(this.OnClickCancelBtn), tipMsgData, tipMsgData.szInfo[4].ToString(), (bool)tipMsgData.szInfo[5], new easy_tipCon.TipToggleDelegate(this.OnClickToggle), tipMsgData, tipMsgData.szInfo[6].ToString(), tipMsgData.szInfo[7].ToString(), tipMsgData.szInfo[8].ToString(), 2f);
				break;
			case 2:
			case 6:
				easy_tipCon.inst.ShowRewardTip(string.Empty, tipMsgData.GetTitle(), tipMsgData.GetIcon(), tipMsgData.GetQual(), tipMsgData.GetDesc(), new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData);
				break;
			case 4:
				easy_tipCon.inst.ClearPopTip(true);
				easy_tipCon.inst.ShowCommonTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), string.Empty, string.Empty, new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData, null, null, string.Empty, false, null, null, string.Empty, string.Empty, string.Empty, (float)tipMsgData.szInfo[2]);
				break;
			case 5:
				easy_tipCon.inst.ShowItemTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), (ArrayList)tipMsgData.szInfo[2], tipMsgData.szInfo[3].ToString(), new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData);
				break;
			case 7:
				easy_tipCon.inst.ClearPopTip(true);
				easy_tipCon.inst.ShowRelinkTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), (int)(tipMsgData.okParam as ArrayList)[1], (int)(tipMsgData.okParam as ArrayList)[2], tipMsgData.szInfo[3].ToString(), new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData, new easy_tipCon.TipCancelDelegate(this.OnClickCancelBtn), tipMsgData);
				break;
			case 10:
				easy_tipCon.inst.ShowInputTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), tipMsgData.szInfo[2].ToString(), tipMsgData.szInfo[3].ToString(), new easy_tipCon.TipInputDelegate(this.OnInputOkBtn), tipMsgData, new easy_tipCon.TipCancelDelegate(this.OnClickCancelBtn), tipMsgData, (int)tipMsgData.szInfo[4], (float)tipMsgData.szInfo[5], (float)tipMsgData.szInfo[6]);
				break;
			case 11:
				easy_tipCon.inst.ShowCommonTip(tipMsgData.szInfo[0].ToString(), tipMsgData.szInfo[1].ToString(), tipMsgData.szInfo[2].ToString(), tipMsgData.szInfo[3].ToString(), new easy_tipCon.TipOKDelegate(this.OnClickOkBtn), tipMsgData, new easy_tipCon.TipCancelDelegate(this.OnClickCancelBtn), tipMsgData, tipMsgData.szInfo[4].ToString(), (bool)tipMsgData.szInfo[5], new easy_tipCon.TipToggleDelegate(this.OnClickToggle), tipMsgData, tipMsgData.szInfo[6].ToString(), tipMsgData.szInfo[7].ToString(), tipMsgData.szInfo[8].ToString(), 2f);
				easy_tipCon.inst.ShowTipZhekouObj();
				break;
			}
			bool flag4 = tipMsgData.tipType != 7 && tipMsgData.tipType != 4;
			if (flag4)
			{
				fUISoundManager.instance.PlayUISound("jiemian_tishikuang", 0, null, 20);
			}
		}
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x00187F90 File Offset: 0x00186190
	private void OnInputOkBtn(object szParam, string sinput)
	{
		bool flag = szParam is TipMsgData;
		if (flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = true;
			TipMsgData tipMsgData = (TipMsgData)szParam;
			bool flag2 = tipMsgData.inputFunc != null;
			if (flag2)
			{
				tipMsgData.inputFunc(tipMsgData.inputParam, sinput);
			}
			this.allTipMsg.Remove(tipMsgData);
			this.isPopTip = false;
			this.OnCheckTip();
		}
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x00188004 File Offset: 0x00186204
	private void OnClickToggle(object szParam, bool bstate)
	{
		bool flag = szParam is TipMsgData;
		if (flag)
		{
			TipMsgData tipMsgData = (TipMsgData)szParam;
			bool flag2 = tipMsgData.toggleFunc != null;
			if (flag2)
			{
				tipMsgData.toggleFunc(bstate);
			}
		}
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x00188044 File Offset: 0x00186244
	private void OnClickCancelBtn(object szParam)
	{
		bool flag = szParam is TipMsgData;
		if (flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = true;
			TipMsgData tipMsgData = (TipMsgData)szParam;
			bool flag2 = tipMsgData.cancelFunc != null;
			if (flag2)
			{
				tipMsgData.cancelFunc(tipMsgData.cancelParam);
			}
			this.isPopTip = false;
			bool flag3 = tipMsgData.tipType == 7;
			if (flag3)
			{
				this.relinkData = null;
			}
			else
			{
				bool flag4 = tipMsgData.tipType == 4;
				if (flag4)
				{
					this.delayTipData = null;
				}
				else
				{
					this.allTipMsg.Remove(tipMsgData);
					this.OnCheckTip();
				}
			}
		}
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x001880EC File Offset: 0x001862EC
	private void OnClickOkBtn(object szParam)
	{
		bool flag = !(szParam is TipMsgData);
		if (!flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = true;
			TipMsgData tipMsgData = (TipMsgData)szParam;
			bool flag2 = tipMsgData.okFunc != null;
			if (flag2)
			{
				bool flag3 = tipMsgData.tipType == 7;
				if (flag3)
				{
					tipMsgData.okFunc((tipMsgData.okParam as ArrayList)[0]);
				}
				else
				{
					tipMsgData.okFunc(tipMsgData.okParam);
				}
			}
			this.isPopTip = false;
			bool flag4 = tipMsgData.tipType == 7;
			if (flag4)
			{
				this.relinkData = null;
			}
			else
			{
				bool flag5 = tipMsgData.tipType == 4;
				if (flag5)
				{
					this.delayTipData = null;
				}
				else
				{
					this.allTipMsg.Remove(tipMsgData);
					this.OnCheckTip();
				}
			}
		}
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x001881C8 File Offset: 0x001863C8
	public void OnPopDelayOver(object szParam)
	{
		bool flag = szParam is TipMsgData;
		if (flag)
		{
			GameRoot.inst.Setting.Load_Setting_Data_State = true;
			TipMsgData tipMsgData = (TipMsgData)szParam;
			bool flag2 = tipMsgData.tipType == 7;
			if (flag2)
			{
				this.relinkData = null;
			}
			bool flag3 = tipMsgData.tipType == 4;
			if (flag3)
			{
				this.delayTipData = null;
			}
			this.isPopTip = false;
			bool flag4 = tipMsgData.tipType == 8;
			if (flag4)
			{
				tipMsgData.okFunc(tipMsgData.okParam);
			}
			else
			{
				bool flag5 = this.allTipMsg.Contains(tipMsgData);
				if (flag5)
				{
					this.allTipMsg.Remove(tipMsgData);
					this.OnCheckTip();
				}
			}
		}
	}

	// Token: 0x040025D2 RID: 9682
	private static TipManager instance;

	// Token: 0x040025D3 RID: 9683
	public List<TipMsgData> allTipMsg = new List<TipMsgData>();

	// Token: 0x040025D4 RID: 9684
	private bool _isPopTip;

	// Token: 0x040025D5 RID: 9685
	private bool isTextTip;

	// Token: 0x040025D6 RID: 9686
	private TipMsgData relinkData;

	// Token: 0x040025D7 RID: 9687
	private TipMsgData delayTipData;

	// Token: 0x02000689 RID: 1673
	// (Invoke) Token: 0x0600395C RID: 14684
	public delegate void TipOKDelegate(object szParam);

	// Token: 0x0200068A RID: 1674
	// (Invoke) Token: 0x06003960 RID: 14688
	public delegate void TipCancelDelegate(object szParam);

	// Token: 0x0200068B RID: 1675
	// (Invoke) Token: 0x06003964 RID: 14692
	public delegate void TipToggleDelegate(bool bstate);

	// Token: 0x0200068C RID: 1676
	// (Invoke) Token: 0x06003968 RID: 14696
	public delegate void TipInputDelegate(object szParam, string sinput);
}
