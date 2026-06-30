using System;
using UnityEngine.UI;

// Token: 0x0200046D RID: 1133
public class TeacherDayUICon : Window
{
	// Token: 0x06002C26 RID: 11302 RVA: 0x00183D73 File Offset: 0x00181F73
	protected override void Awake()
	{
		base.Awake();
		this.timeText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.bgRimg = this.UIGameObj.Get_GameObj(2).GetComponent<RawImage>();
		TeacherDayUICon.inst = this;
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x00183DB4 File Offset: 0x00181FB4
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(3);
		bool flag2 = actDataByType != null;
		if (flag2)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			OpenHeroActData openHeroActData = fGameManager.mloadConfig.AllOpenHeroAct[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + openHeroActData.sIconName + ".txt", this.bgRimg);
		}
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x00183EA4 File Offset: 0x001820A4
	private void Update()
	{
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x00183EA8 File Offset: 0x001820A8
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x06002C2A RID: 11306 RVA: 0x00183EDC File Offset: 0x001820DC
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x040025A2 RID: 9634
	public static TeacherDayUICon inst;

	// Token: 0x040025A3 RID: 9635
	private Text timeText;

	// Token: 0x040025A4 RID: 9636
	private RawImage bgRimg;
}
