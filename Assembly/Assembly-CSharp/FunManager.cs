using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class FunManager : MonoBehaviour
{
	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0007690C File Offset: 0x00074B0C
	// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00076923 File Offset: 0x00074B23
	public static FunManager Instance
	{
		get
		{
			return FunManager._instance;
		}
		set
		{
			FunManager._instance = value;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0007692C File Offset: 0x00074B2C
	public DateTime serverTimeNow
	{
		get
		{
			return this._tickTime = this._serverLastUpdateTime.AddSeconds((double)(Time.realtimeSinceStartup - this._tickEnvironment));
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0007695C File Offset: 0x00074B5C
	// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00076974 File Offset: 0x00074B74
	public DateTime serverTime
	{
		get
		{
			return this._serverTime;
		}
		set
		{
			this._serverTime = value;
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0007697E File Offset: 0x00074B7E
	private void Start()
	{
		FunManager.Instance = this;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00076988 File Offset: 0x00074B88
	public void reSet()
	{
		this.bTickInit = false;
		this.bKick = false;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0007699C File Offset: 0x00074B9C
	public void SetServerTime(string sServerTime, int no)
	{
		bool flag = no <= 0 || (fGameManager.GetInstance().nNowScnID <= 1 && fGameManager.Heroplayer == null && Time.realtimeSinceStartup <= this._getTimetick && this._getTimeNo == no);
		if (flag)
		{
			this._serverLastUpdateTime = Convert.ToDateTime(sServerTime);
			this._serverTime = this._serverLastUpdateTime;
			this._tickEnvironment = Time.realtimeSinceStartup;
			this._serverTimeTick = DateTime.Now;
			this._getServerTimeOut = this._serverTime.AddDays(2.0);
			this.bTickInit = true;
		}
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00076A33 File Offset: 0x00074C33
	private void checkOut()
	{
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00076A38 File Offset: 0x00074C38
	private void FixedUpdate()
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			bool flag2 = this.timeScaleKick && this.timeScaleOut < Time.time;
			if (flag2)
			{
				this.timeScaleOut = Time.time + 30f + UnityEngine.Random.value * 30f;
				FunManager.Instance.Send(fGameManager.Heroplayer, "156", "时间缩放出现异常:" + this.kickName);
				fGameManager.GetInstance().OnLostTip(Language.GetLang(1500, new object[0]));
				this.checkOut();
				return;
			}
			bool flag3 = !this.timeScaleKick && (Time.timeScale > 1.02f || Time.timeScale < 0.98f);
			if (flag3)
			{
				this.timeScaleKick = true;
				this.kickName = Time.timeScale.ToString("F3");
				this.timeScaleOut = Time.time + 2f + UnityEngine.Random.value * 3f;
				return;
			}
		}
		bool flag4 = !this.bTickInit;
		if (!flag4)
		{
			this.sp = DateTime.Now - this._serverTimeTick;
			bool flag5 = this.sp.TotalMilliseconds > 60000.0 || this.sp.TotalMilliseconds < -60000.0;
			if (flag5)
			{
				this._serverTimeTick = DateTime.Now;
			}
			else
			{
				this._serverTimeTick = DateTime.Now;
				this._tickTime = this._serverLastUpdateTime.AddSeconds((double)(Time.realtimeSinceStartup - this._tickEnvironment));
				this._serverTime = this._serverTime.AddMilliseconds(this.sp.TotalMilliseconds);
				bool flag6 = this._serverTime > this._getServerTimeOut;
				if (flag6)
				{
					this._getServerTimeOut = this._serverTime.AddSeconds(3.0);
					this._getTimetick = Time.realtimeSinceStartup + 2f;
					this._getTimeNo++;
					bool flag7 = this._getTimeNo > 9;
					if (flag7)
					{
						this._getTimeNo = 1;
					}
					bool flag8 = fGameManager.LobbyPlayer != null && fGameManager.GetInstance().nNowScnID == 0;
					if (flag8)
					{
						fGameManager.LobbyPlayer.SendMsgToHallServer(20058, new ArrayList
						{
							this._getTimeNo,
							0
						});
					}
				}
				this.spCheck = this._tickTime - this._serverTime;
				bool flag9 = !this.bKick && this.spCheck.TotalSeconds > 6.0;
				if (flag9)
				{
					this.bKick = true;
					this.bTickInit = false;
					fGameManager.GetInstance().OnLostTip(Language.GetLang(1501, new object[0]));
					ArrayList arrayList = new ArrayList();
					arrayList.Add("150");
					arrayList.Add(string.Format("可能操作系统问题,时钟问题:{0},{1},{2},hero:{3}", new object[]
					{
						this.spCheck.TotalSeconds.ToString("F3"),
						this._tickTime.ToString(),
						this._serverTime.ToString(),
						fGameManager.Heroplayer != null
					}));
					arrayList.Add(DateTime.Now.ToString());
					arrayList.Add(SysConfig.g_client);
					bool flag10 = fGameManager.LobbyPlayer != null;
					if (flag10)
					{
						arrayList.Add(string.Empty);
						fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
					}
					this.checkOut();
				}
			}
		}
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00076DFC File Offset: 0x00074FFC
	public Vector3 Vector3SetMaxOne(Vector3 v3)
	{
		bool flag = v3 == Vector3.zero;
		Vector3 result;
		if (flag)
		{
			result = v3;
		}
		else
		{
			float num = (Mathf.Abs(v3.x) <= Mathf.Abs(v3.y)) ? v3.y : v3.x;
			num = ((Mathf.Abs(num) <= Mathf.Abs(v3.z)) ? v3.z : num);
			num = Mathf.Abs(1f / num);
			result = v3 * num;
		}
		return result;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00076E7C File Offset: 0x0007507C
	public void Send(NetLiving hero, string sType, string sMsg)
	{
		bool flag = hero != null && hero.isMainPlayer;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(sType);
			arrayList.Add(sMsg);
			arrayList.Add(DateTime.Now.ToString());
			arrayList.Add(SysConfig.g_client);
			string value = string.Format("sNow:{0},load:{1}", Fun.GetServerDate(), CReadModel.Instance.lastLoadModelTime.ToString("F1"));
			arrayList.Add(value);
			hero.SendMessageToServer(2110, arrayList);
		}
	}

	// Token: 0x04000A52 RID: 2642
	private static FunManager _instance;

	// Token: 0x04000A53 RID: 2643
	private DateTime _serverLastUpdateTime = DateTime.Now;

	// Token: 0x04000A54 RID: 2644
	private float _tickEnvironment;

	// Token: 0x04000A55 RID: 2645
	private DateTime _tickTime;

	// Token: 0x04000A56 RID: 2646
	private DateTime _serverTime;

	// Token: 0x04000A57 RID: 2647
	private DateTime _serverTimeTick;

	// Token: 0x04000A58 RID: 2648
	private DateTime _getServerTimeOut;

	// Token: 0x04000A59 RID: 2649
	private float _getTimetick;

	// Token: 0x04000A5A RID: 2650
	private int _getTimeNo;

	// Token: 0x04000A5B RID: 2651
	private bool bTickInit;

	// Token: 0x04000A5C RID: 2652
	private bool bKick;

	// Token: 0x04000A5D RID: 2653
	public float XXX = 100f;

	// Token: 0x04000A5E RID: 2654
	public float XXX1 = 10f;

	// Token: 0x04000A5F RID: 2655
	public float XXX2 = 1f;

	// Token: 0x04000A60 RID: 2656
	private TimeSpan sp;

	// Token: 0x04000A61 RID: 2657
	private TimeSpan spCheck;

	// Token: 0x04000A62 RID: 2658
	private float timeScaleOut;

	// Token: 0x04000A63 RID: 2659
	private bool timeScaleKick;

	// Token: 0x04000A64 RID: 2660
	private string kickName = string.Empty;
}
