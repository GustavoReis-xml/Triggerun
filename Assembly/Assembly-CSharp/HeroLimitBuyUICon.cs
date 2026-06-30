using System;
using UnityEngine.UI;

// Token: 0x0200019B RID: 411
public class HeroLimitBuyUICon : Window
{
	// Token: 0x06001343 RID: 4931 RVA: 0x000B81C4 File Offset: 0x000B63C4
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.timeText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.noteText = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		HeroLimitBuyUICon.inst = this;
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000B8224 File Offset: 0x000B6424
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000B8230 File Offset: 0x000B6430
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(36);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			HeroLimitBuyData heroLimitBuyData = fGameManager.mloadConfig.AllHeroLimitBuyData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + heroLimitBuyData.sIconName + ".txt", this.bgRimg);
			this.noteText.text = heroLimitBuyData.sNoteDesc;
		}
	}

	// Token: 0x04000F07 RID: 3847
	public static HeroLimitBuyUICon inst;

	// Token: 0x04000F08 RID: 3848
	private RawImage bgRimg;

	// Token: 0x04000F09 RID: 3849
	private Text timeText;

	// Token: 0x04000F0A RID: 3850
	private Text noteText;
}
