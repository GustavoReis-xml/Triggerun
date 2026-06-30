using System;
using UnityEngine.UI;

// Token: 0x020000FE RID: 254
public class DoubleExpUICon : Window
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x0004C3FF File Offset: 0x0004A5FF
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.transform.GetComponent<RawImage>();
		this.timeTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		DoubleExpUICon.inst = this;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0004C437 File Offset: 0x0004A637
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0004C444 File Offset: 0x0004A644
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(12);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			this.timeTxt.text = Language.GetLang(28013, new object[]
			{
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				dateTime2.Year,
				dateTime2.Month,
				dateTime2.Day
			});
			ActDaTingAward actDaTingAward = fGameManager.mloadConfig.AllActDaTingAward[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + actDaTingAward.bgimg + ".txt", this.bgRimg);
		}
	}

	// Token: 0x04000689 RID: 1673
	public static DoubleExpUICon inst;

	// Token: 0x0400068A RID: 1674
	private RawImage bgRimg;

	// Token: 0x0400068B RID: 1675
	private Text timeTxt;
}
