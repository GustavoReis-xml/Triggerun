using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017A RID: 378
public class HeroDiscountUICon : Window
{
	// Token: 0x06001179 RID: 4473 RVA: 0x000A3D74 File Offset: 0x000A1F74
	protected override void Awake()
	{
		base.Awake();
		this.timeText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.noteText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(2);
		this.btnText = gameObject.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.bgRimg = this.UIGameObj.Get_GameObj(3).GetComponent<RawImage>();
		HeroDiscountUICon.inst = this;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x000A3E2C File Offset: 0x000A202C
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(30);
		bool flag2 = actDataByType != null;
		if (flag2)
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

	// Token: 0x0600117B RID: 4475 RVA: 0x000A3F43 File Offset: 0x000A2143
	private void Update()
	{
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000A3F48 File Offset: 0x000A2148
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x000A3F7C File Offset: 0x000A217C
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x000A3F88 File Offset: 0x000A2188
	private void OnClickBtn(GameObject go)
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(1, null);
		}
	}

	// Token: 0x04000DDD RID: 3549
	public static HeroDiscountUICon inst;

	// Token: 0x04000DDE RID: 3550
	private Text timeText;

	// Token: 0x04000DDF RID: 3551
	private Text noteText;

	// Token: 0x04000DE0 RID: 3552
	private Text btnText;

	// Token: 0x04000DE1 RID: 3553
	private RawImage bgRimg;
}
