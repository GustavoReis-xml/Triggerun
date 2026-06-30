using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000313 RID: 787
public class ShowTipCon : Window
{
	// Token: 0x0600262B RID: 9771 RVA: 0x0015BC94 File Offset: 0x00159E94
	private void Update()
	{
		int num = Environment.TickCount - this.m_nTickTime;
		this.m_nTickTime = Environment.TickCount;
		bool flag = num > 0 && this.m_nTime > -1;
		if (flag)
		{
			this.TimeSlider.fillAmount -= (float)num * this.m_nSpeed / 1000f / (float)this.m_nAllTime;
			bool flag2 = this.TimeSlider.fillAmount <= 0f;
			if (flag2)
			{
				this.OnEscBtnClick(null);
			}
		}
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0015BD20 File Offset: 0x00159F20
	protected override void Awake()
	{
		base.Awake();
		ShowTipCon.inst = this;
		this.Conttext = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.OkBtn = this.UIGameObj.Get_GameObj(2).GetComponent<Button>();
		this.EscBtn = this.UIGameObj.Get_GameObj(3).GetComponent<Button>();
		this.TimeSlider = this.UIGameObj.Get_GameObj(4).GetComponent<Image>();
		this.OkBtnText = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.EscBtnText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		EventTriggerListener.Get(this.OkBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnOkClick);
		EventTriggerListener.Get(this.EscBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnEscBtnClick);
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x0015BE08 File Offset: 0x0015A008
	private void OnOkClick(GameObject obj)
	{
		bool flag = !MatchManager.GetInstance().isEnterFb;
		if (flag)
		{
			base.CancelInvoke("TimeCall");
			switch (this.m_nType)
			{
			case 1:
				GuildManager.GetInstance().OnInviteOk((int)this.m_szPram[0], this.m_szPram[1].ToString(), 1);
				break;
			case 2:
				TeamManager.GetInstance().onMsgClick(true, 1, this.m_szPram);
				break;
			case 3:
				FriendManager.GetInstance().UnRequstMsg(1, this.m_szPram[0].ToString(), (int)this.m_szPram[1]);
				break;
			case 4:
				RoomManage.GetInstance().OnInviteBack(1, this.m_szPram);
				break;
			}
			this.DelUI();
		}
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x0015BEF0 File Offset: 0x0015A0F0
	private void OnEscBtnClick(GameObject obj)
	{
		bool isEnterFb = MatchManager.GetInstance().isEnterFb;
		if (!isEnterFb)
		{
			base.CancelInvoke("TimeCall");
			switch (this.m_nType)
			{
			case 1:
				GuildManager.GetInstance().OnInviteOk((int)this.m_szPram[0], this.m_szPram[1].ToString(), 0);
				break;
			case 2:
				TeamManager.GetInstance().onMsgClick(true, 0, this.m_szPram);
				break;
			case 3:
			{
				bool flag = obj != null;
				if (flag)
				{
					FriendManager.GetInstance().UnRequstMsg(0, this.m_szPram[0].ToString(), (int)this.m_szPram[1]);
				}
				break;
			}
			case 4:
				RoomManage.GetInstance().OnInviteBack(0, this.m_szPram);
				break;
			}
			this.DelUI();
		}
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0015BFE4 File Offset: 0x0015A1E4
	public void InitTip(OnePushMsgInfo pData)
	{
		this.m_nType = pData.m_nType;
		this.m_szPram = pData.szData;
		int pushMsgTimeLong = this.GetPushMsgTimeLong(pData.dCreateTime);
		this.TimeSlider.fillAmount = (float)pushMsgTimeLong * 1f / (float)this.m_nAllTime * 1f;
		this.InitSlider(pushMsgTimeLong);
		this.m_nTime = pushMsgTimeLong;
		this.Conttext.text = pData.m_sMsg;
		fUISoundManager.instance.PlayUISound("ui_team_invitation", 0, null, 20);
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x0015C070 File Offset: 0x0015A270
	private void InitSlider(int nTime)
	{
		base.CancelInvoke("TimeCall");
		bool flag = nTime > 0;
		if (flag)
		{
			this.TimeSlider.gameObject.SetActive(true);
			this.m_nTickTime = Environment.TickCount;
			base.InvokeRepeating("TimeCall", 1f, 1f);
		}
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x0015C0C8 File Offset: 0x0015A2C8
	private int GetPushMsgTimeLong(string sCreateTime)
	{
		int endCDTime = Fun.GetEndCDTime(sCreateTime, this.m_nAllTime);
		bool flag = endCDTime < 1;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = endCDTime;
		}
		return result;
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x0015C0F8 File Offset: 0x0015A2F8
	private void InitBtn(ArrayList szBtnlist)
	{
		bool flag = szBtnlist.Count == 1;
		if (flag)
		{
			this.OkBtn.gameObject.SetActive(false);
			this.EscBtn.gameObject.SetActive(false);
			this.OneBtn.gameObject.SetActive(true);
			this.OneBtnText.text = szBtnlist[0].ToString();
		}
		bool flag2 = szBtnlist.Count == 2;
		if (flag2)
		{
			this.OkBtn.gameObject.SetActive(true);
			this.EscBtn.gameObject.SetActive(true);
			this.OneBtn.gameObject.SetActive(false);
			this.OkBtnText.text = szBtnlist[0].ToString();
			this.EscBtnText.text = szBtnlist[1].ToString();
		}
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x0015C1D8 File Offset: 0x0015A3D8
	private void TimeCall()
	{
		bool flag = this.m_nTime == 0;
		if (flag)
		{
			base.CancelInvoke("TimeCall");
		}
		else
		{
			this.m_nTime--;
		}
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x0015C214 File Offset: 0x0015A414
	private void DelUI()
	{
		this.TimeSlider.fillAmount = 1f;
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.OnHallChangeNextMsg();
		}
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x0015C24F File Offset: 0x0015A44F
	internal void InitTip()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040021F1 RID: 8689
	public static ShowTipCon inst;

	// Token: 0x040021F2 RID: 8690
	private Text Conttext;

	// Token: 0x040021F3 RID: 8691
	private Button OkBtn;

	// Token: 0x040021F4 RID: 8692
	private Button EscBtn;

	// Token: 0x040021F5 RID: 8693
	private Button OneBtn;

	// Token: 0x040021F6 RID: 8694
	private Image TimeSlider;

	// Token: 0x040021F7 RID: 8695
	private Text OkBtnText;

	// Token: 0x040021F8 RID: 8696
	private Text EscBtnText;

	// Token: 0x040021F9 RID: 8697
	private Text OneBtnText;

	// Token: 0x040021FA RID: 8698
	private ArrayList m_szPram;

	// Token: 0x040021FB RID: 8699
	private int m_nTime;

	// Token: 0x040021FC RID: 8700
	private float m_nSpeed = 1f;

	// Token: 0x040021FD RID: 8701
	private int m_nTickTime;

	// Token: 0x040021FE RID: 8702
	private int m_nAllTime = 20;

	// Token: 0x040021FF RID: 8703
	private int m_nType;

	// Token: 0x04002200 RID: 8704
	private SystemMsgTipCon.onOkClick OnFun;
}
