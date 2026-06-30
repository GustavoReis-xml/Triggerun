using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000242 RID: 578
public class MatchUICon : Window
{
	// Token: 0x06001A4E RID: 6734 RVA: 0x000F99A4 File Offset: 0x000F7BA4
	protected override void Awake()
	{
		base.Awake();
		MatchUICon.inst = this;
		this.StartObj = this.UIGameObj.Get_GameObj(0);
		this.YuJiTime = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.YiyongTime = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.BtnObj = this.UIGameObj.Get_GameObj(4);
		this.matchok = this.UIGameObj.Get_GameObj(5);
		this.matchok.SetActive(false);
		this.playernum = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.playernum.text = string.Empty;
		EventTriggerListener.Get(this.BtnObj).onClick = new EventTriggerListener.VoidDelegate(this.OnEscMatch);
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000F9A74 File Offset: 0x000F7C74
	public void Init()
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch > 0;
		if (flag)
		{
			this.StartObj.SetActive(true);
			this.nSTime = MatchManager.GetInstance().GetYjTime();
			bool flag2 = this.nSTime == -1;
			if (flag2)
			{
				this.YuJiTime.text = Language.GetLang(17010, new object[0]);
			}
			else
			{
				this.YuJiTime.text = Fun.sencondsToHMS_CXP(this.nSTime);
			}
			this.nYTime = 0;
			base.CancelInvoke("OnYuJiTimer");
			base.InvokeRepeating("OnYuJiTimer", 1f, 1f);
			bool flag3 = TeamManager.GetInstance().m_nTeamID == 0;
			if (flag3)
			{
				this.BtnObj.SetActive(true);
			}
			else
			{
				bool flag4 = TeamManager.GetInstance().m_nCaptain == 1;
				if (flag4)
				{
					this.BtnObj.SetActive(true);
				}
				else
				{
					this.BtnObj.SetActive(false);
				}
			}
			this.YiyongTime.text = Fun.sencondsToHMS_CXP(this.nYTime);
		}
		bool flag5 = MatchManager.GetInstance().m_nIsMatch == 2;
		if (flag5)
		{
			this.playernum.text = Language.GetLang(24014, new object[0]);
		}
		else
		{
			this.OnUpDataMatchTeamNum(new ArrayList
			{
				0,
				MatchManager.GetInstance().nNowNum,
				MatchManager.GetInstance().nMaxNum,
				MatchManager.GetInstance().m_szAllModelNum
			});
		}
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000F9C18 File Offset: 0x000F7E18
	private void OnEscMatch(GameObject obj)
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch != 4 || MatchManager.GetInstance().nNowNum < MatchManager.GetInstance().nMaxNum;
		if (flag)
		{
			MatchManager.GetInstance().OnCancelMatch();
			bool flag2 = MatchModelCon.inst != null;
			if (flag2)
			{
				MatchModelCon.inst.CloseYinDaoTiShi();
			}
		}
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000F9C79 File Offset: 0x000F7E79
	private void OnStarTimer()
	{
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x000F9C7C File Offset: 0x000F7E7C
	private void OnYuJiTimer()
	{
		this.nYTime++;
		this.YiyongTime.text = Fun.sencondsToHMS_CXP(this.nYTime);
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x000F9CA4 File Offset: 0x000F7EA4
	private void Start()
	{
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000F9CA7 File Offset: 0x000F7EA7
	private void Update()
	{
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000F9CAC File Offset: 0x000F7EAC
	public void OnUpDataMatchTeamNum(ArrayList arr)
	{
		int num = (int)arr[0];
		int num2 = (int)arr[1];
		int num3 = (int)arr[2];
		MatchManager.GetInstance().nNowNum = num2;
		MatchManager.GetInstance().nMaxNum = num3;
		MatchManager.GetInstance().m_szAllModelNum = (ArrayList)arr[3];
		bool flag = MatchManager.GetInstance().m_nIsMatch == 4;
		if (flag)
		{
			bool flag2 = num2 >= num3;
			if (flag2)
			{
				this.playernum.text = Language.GetLang(17006, new object[0]);
			}
			else
			{
				this.playernum.text = Language.GetLang(17018, new object[]
				{
					num2.ToString(),
					num3.ToString()
				});
			}
		}
		else
		{
			bool flag3 = num == 1 || num2 >= num3;
			if (flag3)
			{
				this.playernum.text = Language.GetLang(17012, new object[0]);
			}
			else
			{
				this.playernum.text = Language.GetLang(17011, new object[]
				{
					num2.ToString(),
					num3.ToString()
				});
			}
		}
	}

	// Token: 0x040014E6 RID: 5350
	public static MatchUICon inst;

	// Token: 0x040014E7 RID: 5351
	private GameObject StartObj;

	// Token: 0x040014E8 RID: 5352
	private Text YuJiTime;

	// Token: 0x040014E9 RID: 5353
	private Text YiyongTime;

	// Token: 0x040014EA RID: 5354
	private int nSTime = 60;

	// Token: 0x040014EB RID: 5355
	private int nYTime;

	// Token: 0x040014EC RID: 5356
	private GameObject BtnObj;

	// Token: 0x040014ED RID: 5357
	private GameObject matchok;

	// Token: 0x040014EE RID: 5358
	private Text playernum;
}
