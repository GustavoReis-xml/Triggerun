using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000485 RID: 1157
public class TuijianUICon : Window
{
	// Token: 0x06002CB6 RID: 11446 RVA: 0x001898D4 File Offset: 0x00187AD4
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.transform.GetComponent<RawImage>();
		GameObject go = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(go).onClick = new EventTriggerListener.VoidDelegate(this.OnUrlBtnClick);
		EventTriggerListener.Get(go).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.btnText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		TuijianUICon.inst = this;
	}

	// Token: 0x06002CB7 RID: 11447 RVA: 0x00189951 File Offset: 0x00187B51
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06002CB8 RID: 11448 RVA: 0x0018995B File Offset: 0x00187B5B
	public void Update()
	{
	}

	// Token: 0x06002CB9 RID: 11449 RVA: 0x00189960 File Offset: 0x00187B60
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(40);
		bool flag = actDataByType != null;
		if (flag)
		{
			TuijianData tuijianData = fGameManager.mloadConfig.AllTuijianData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + tuijianData.sIconName + ".txt", this.bgRimg);
			this.btnText.text = tuijianData.sBtnTxt;
		}
	}

	// Token: 0x06002CBA RID: 11450 RVA: 0x001899D4 File Offset: 0x00187BD4
	private void OnUrlBtnClick(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(40);
		bool flag = actDataByType != null;
		if (flag)
		{
			TuijianData tuijianData = fGameManager.mloadConfig.AllTuijianData[actDataByType.nXmlId];
			fUIManager.GetInstance().OpenUIByUrl(tuijianData.szBtnData);
		}
	}

	// Token: 0x040025E9 RID: 9705
	public static TuijianUICon inst;

	// Token: 0x040025EA RID: 9706
	private RawImage bgRimg;

	// Token: 0x040025EB RID: 9707
	private Text btnText;
}
