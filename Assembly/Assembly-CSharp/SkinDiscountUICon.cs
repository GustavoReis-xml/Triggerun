using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D4 RID: 980
public class SkinDiscountUICon : Window
{
	// Token: 0x06002A00 RID: 10752 RVA: 0x00171418 File Offset: 0x0016F618
	protected override void Awake()
	{
		base.Awake();
		this.timeText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.noteText = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(2);
		this.btnText = gameObject.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.bgRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		SkinDiscountUICon.inst = this;
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x001714D0 File Offset: 0x0016F6D0
	private void Start()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(47);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
			this.timeText.text = Language.GetLang(42021, new object[]
			{
				dateTime.ToString("yyyy/MM/dd") + "-" + dateTime2.AddDays(-1.0).ToString("yyyy/MM/dd")
			});
			HeroDiscountData heroDiscountData = fGameManager.mloadConfig.AllHeroDiscountData[actDataByType.nXmlId];
			this.noteText.text = heroDiscountData.sNoteDesc;
			this.btnText.text = heroDiscountData.sBtnTxt;
			fUIManager.GetInstance().GetIconAsset("libao/" + heroDiscountData.sIconName + ".txt", this.bgRimg);
		}
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x001715C8 File Offset: 0x0016F7C8
	private void OnClickBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(47);
		bool flag = actDataByType != null;
		if (flag)
		{
			HeroDiscountData heroDiscountData = fGameManager.mloadConfig.AllHeroDiscountData[actDataByType.nXmlId];
			bool flag2 = heroDiscountData.szBtnUrl.Length == 4;
			if (flag2)
			{
				fUIManager.GetInstance().OpenUIByUrl(heroDiscountData.szBtnUrl);
			}
		}
	}

	// Token: 0x04002355 RID: 9045
	public static SkinDiscountUICon inst;

	// Token: 0x04002356 RID: 9046
	private Text timeText;

	// Token: 0x04002357 RID: 9047
	private Text noteText;

	// Token: 0x04002358 RID: 9048
	private Text btnText;

	// Token: 0x04002359 RID: 9049
	private RawImage bgRimg;
}
