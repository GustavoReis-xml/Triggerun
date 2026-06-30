using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class HackLogMemo : MonoBehaviour
{
	// Token: 0x06001033 RID: 4147 RVA: 0x000968DA File Offset: 0x00094ADA
	private void Start()
	{
		this.HandleLog(string.Empty, string.Empty, LogType.Log);
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x000968EF File Offset: 0x00094AEF
	public void OnEnable()
	{
		Application.logMessageReceived += this.HandleLog;
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x00096904 File Offset: 0x00094B04
	public void OnDisable()
	{
		Application.logMessageReceived -= this.HandleLog;
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0009691C File Offset: 0x00094B1C
	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		bool flag = (type == LogType.Error || type == LogType.Exception) && !logString.Contains("Coroutine continue failure");
		if (flag)
		{
			this.AddToDb(logString, stackTrace);
		}
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x00096954 File Offset: 0x00094B54
	private void AddToDb(string condition, string stackTrace)
	{
		bool flag = Time.time < this.idbTimeOut;
		if (!flag)
		{
			bool flag2 = this.idbNum > 1;
			if (flag2)
			{
				this.idbTimeOut = Time.time + 300f;
				this.idbNum = 0;
			}
			else
			{
				bool flag3 = this.hero == null;
				if (flag3)
				{
					this.hero = fGameManager.GetInstance().getPlayer();
				}
				bool flag4 = this.LobbyPlayer == null;
				if (flag4)
				{
					this.LobbyPlayer = fGameManager.LobbyPlayer;
				}
				bool flag5 = this.hero != null || this.LobbyPlayer != null;
				if (flag5)
				{
					ArrayList arrayList = new ArrayList();
					string text = condition + " (handletype) ||| " + stackTrace;
					text = text.Replace("\n", " ||| ");
					text = text.Replace("\r", " ||| ");
					text = text.Replace("'", " \" ");
					text = Language.GetServerLang(text);
					arrayList.Add("1");
					arrayList.Add(text);
					arrayList.Add(DateTime.Now.ToString());
					arrayList.Add(SysConfig.g_client);
					bool flag6 = this.hero != null;
					if (flag6)
					{
						string value = string.Format("cam_time:{0},load:{1}", fGameManager.Heroplayer.initCameraTime, CReadModel.Instance.lastLoadModelTime.ToString("F1"));
						arrayList.Add(value);
						this.hero.SendMessageToServer(2110, arrayList);
						this.idbNum++;
					}
					else
					{
						bool flag7 = this.LobbyPlayer != null;
						if (flag7)
						{
							arrayList.Add(string.Empty);
							fGameManager.SendMsgToHallServer(2110, arrayList);
							this.idbNum++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x00096B33 File Offset: 0x00094D33
	private void OnGUI()
	{
	}

	// Token: 0x04000CEB RID: 3307
	private Rect mRect;

	// Token: 0x04000CEC RID: 3308
	private int idbNum;

	// Token: 0x04000CED RID: 3309
	private float idbTimeOut;

	// Token: 0x04000CEE RID: 3310
	private HeroPlayer hero;

	// Token: 0x04000CEF RID: 3311
	private HallUser LobbyPlayer;

	// Token: 0x0200058F RID: 1423
	private struct LogEntry
	{
		// Token: 0x04002D82 RID: 11650
		public string text;

		// Token: 0x04002D83 RID: 11651
		public Color color;
	}
}
