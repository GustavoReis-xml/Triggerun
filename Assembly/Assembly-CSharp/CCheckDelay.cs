using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class CCheckDelay : MonoBehaviour
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x0600039A RID: 922 RVA: 0x0001C798 File Offset: 0x0001A998
	// (set) Token: 0x0600039B RID: 923 RVA: 0x0001C7AF File Offset: 0x0001A9AF
	public static CCheckDelay Instance
	{
		get
		{
			return CCheckDelay._instance;
		}
		set
		{
			CCheckDelay._instance = value;
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
	private void Start()
	{
		CCheckDelay._instance = this;
		this.lastBegin = Time.realtimeSinceStartup;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001C7CC File Offset: 0x0001A9CC
	public void resetTime()
	{
		this.lastBegin = Time.realtimeSinceStartup;
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0001C7DC File Offset: 0x0001A9DC
	private void Update()
	{
		float num = Time.realtimeSinceStartup - this.lastBegin;
		bool flag = num > 15f;
		if (flag)
		{
			string text = string.Format("卡:{0}", num.ToString("F1"));
			bool flag2 = fGameManager.Heroplayer != null;
			if (flag2)
			{
				FunManager.Instance.Send(fGameManager.Heroplayer, "158", text);
			}
			else
			{
				bool flag3 = fGameManager.LobbyPlayer != null;
				if (flag3)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add("158");
					arrayList.Add(text);
					arrayList.Add(DateTime.Now.ToString());
					arrayList.Add(SysConfig.g_client);
					arrayList.Add(string.Empty);
					fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
				}
			}
		}
		this.lastBegin = Time.realtimeSinceStartup;
		bool flag4 = this.hacked;
		if (flag4)
		{
			bool flag5 = Time.time > this.hckTime;
			if (flag5)
			{
				this.hacked = false;
				fGameManager.GetInstance().OnLostTip(Language.GetLang(1500, new object[0]));
			}
		}
		else
		{
			this.checkSpd();
			this.checkTs();
			this.checkWeaponCD();
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001C920 File Offset: 0x0001AB20
	private void checkSpd()
	{
		bool flag = Time.time < this._spdTime;
		if (!flag)
		{
			this._spdTime = Time.time + 0.5f;
			HeroPlayer heroplayer = fGameManager.Heroplayer;
			float num = 0f;
			bool flag2 = heroplayer != null && heroplayer.playerHaveCreate && !heroplayer.haveDestroy && heroplayer.tChar.isChangeSpd > 0.01f && (!heroplayer.playerCommon.m_fMoveSpeedCheck.equla(heroplayer.playerCommon.m_fMoveSpeed, out num) || Mathf.Abs(heroplayer.tChar.animSpeedMove - heroplayer.tCharAnimationTps.animator.GetFloat("Speed")) >= 0.01f);
			if (flag2)
			{
				this._spdTime = Time.time + 60f;
				string text = string.Format("速度:{0},{1},{2},{3},{4}", new object[]
				{
					heroplayer.playerCommon.m_fMoveSpeed.ToString("F1"),
					heroplayer.playerCommon.m_fMoveSpeedCheck.CCParm.ToString("F1"),
					heroplayer.tChar.animSpeedMove.ToString("F1"),
					heroplayer.tChar.isChangeSpd.ToString("F1"),
					num.ToString("F4")
				});
				bool flag3 = fGameManager.Heroplayer != null;
				if (flag3)
				{
					FunManager.Instance.Send(fGameManager.Heroplayer, "159", text);
				}
				else
				{
					bool flag4 = fGameManager.LobbyPlayer != null;
					if (flag4)
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add("159");
						arrayList.Add(text);
						arrayList.Add(DateTime.Now.ToString());
						arrayList.Add(SysConfig.g_client);
						arrayList.Add(string.Empty);
						fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
					}
				}
				this.hacked = true;
				this.hckTime = Time.time + UnityEngine.Random.Range(1f, 1.5f);
			}
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001CB40 File Offset: 0x0001AD40
	private void checkTs()
	{
		bool flag = Time.time < this._tsTime;
		if (!flag)
		{
			this._tsTime = Time.time + 0.5f;
			HeroPlayer heroplayer = fGameManager.Heroplayer;
			bool flag2 = heroplayer != null && heroplayer.playerHaveCreate && !heroplayer.haveDestroy && heroplayer.playerCommon.m_dtToushiCheck != null && (!heroplayer.playerCommon.m_dtToushiCheck.equla(heroplayer.playerCommon.m_dtToushi) || this._tsBool);
			if (flag2)
			{
				this._tsTime = Time.time + 60f;
				string text = string.Format("透视:{0},{1},{2}", heroplayer.playerCommon.m_dtToushi.ToString(), heroplayer.playerCommon.m_dtToushiCheck.CCParm.ToString(), this._tsBool);
				bool flag3 = fGameManager.Heroplayer != null;
				if (flag3)
				{
					FunManager.Instance.Send(fGameManager.Heroplayer, "160", text);
				}
				else
				{
					bool flag4 = fGameManager.LobbyPlayer != null;
					if (flag4)
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add("160");
						arrayList.Add(text);
						arrayList.Add(DateTime.Now.ToString());
						arrayList.Add(SysConfig.g_client);
						arrayList.Add(string.Empty);
						fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
					}
				}
				this.hacked = true;
				this.hckTime = Time.time + UnityEngine.Random.Range(1f, 1.5f);
			}
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001CCD8 File Offset: 0x0001AED8
	private void checkWeaponCD()
	{
		bool flag = Time.time < this._weaponCDTime;
		if (!flag)
		{
			this._weaponCDTime = Time.time + 0.5f;
			bool flag2 = this.weaponHacked == 0;
			if (!flag2)
			{
				HeroPlayer heroplayer = fGameManager.Heroplayer;
				bool flag3 = heroplayer != null && heroplayer.playerHaveCreate && !heroplayer.haveDestroy;
				if (flag3)
				{
					string text = string.Format("武器锁:{0},{1}", this.weaponHacked, this.weaponStr);
					this._weaponCDTime = Time.time + 30f;
					bool flag4 = fGameManager.Heroplayer != null;
					if (flag4)
					{
						FunManager.Instance.Send(fGameManager.Heroplayer, "161", text);
					}
					else
					{
						bool flag5 = fGameManager.LobbyPlayer != null;
						if (flag5)
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add("161");
							arrayList.Add(text);
							arrayList.Add(DateTime.Now.ToString());
							arrayList.Add(SysConfig.g_client);
							arrayList.Add(string.Empty);
							fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
						}
					}
					this.hacked = true;
					this.hckTime = Time.time + UnityEngine.Random.Range(1f, 1.5f);
				}
			}
		}
	}

	// Token: 0x040002E4 RID: 740
	private static CCheckDelay _instance;

	// Token: 0x040002E5 RID: 741
	private float hckTime;

	// Token: 0x040002E6 RID: 742
	private float lastBegin;

	// Token: 0x040002E7 RID: 743
	private bool hacked;

	// Token: 0x040002E8 RID: 744
	private float _spdTime;

	// Token: 0x040002E9 RID: 745
	private const float _spdRound = 0.5f;

	// Token: 0x040002EA RID: 746
	private float _tsTime;

	// Token: 0x040002EB RID: 747
	private const float _tsRound = 0.5f;

	// Token: 0x040002EC RID: 748
	public bool _tsBool;

	// Token: 0x040002ED RID: 749
	private float _weaponCDTime;

	// Token: 0x040002EE RID: 750
	private const float _weaponCDTimeRound = 0.5f;

	// Token: 0x040002EF RID: 751
	public int weaponHacked;

	// Token: 0x040002F0 RID: 752
	public string weaponStr = string.Empty;
}
