using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000157 RID: 343
public class GoddessGiftUICon : Window
{
	// Token: 0x06000FAE RID: 4014 RVA: 0x000901E4 File Offset: 0x0008E3E4
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.GetComponent<RawImage>();
		this.descTxt1 = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.descTxt2 = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.getBtn = this.UIGameObj.Get_GameObj(2).GetComponent<Button>();
		this.getBtnTxt = this.getBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.getBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGetBtnClick);
		EventTriggerListener.Get(this.getBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.getBtnTxt.font = fUIManager.mDINMedium;
		}
		GoddessGiftUICon.inst = this;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x000902D7 File Offset: 0x0008E4D7
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000902E1 File Offset: 0x0008E4E1
	public void Update()
	{
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x000902E4 File Offset: 0x0008E4E4
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(26);
		bool flag = actDataByType != null;
		if (flag)
		{
			DateTime dateTime = DateTime.Parse(DateTime.Parse(actDataByType.sStartTime).ToString("yyyy-MM-dd"));
			DateTime dateTime2 = DateTime.Parse(DateTime.Parse(actDataByType.sEndTime).ToString("yyyy-MM-dd"));
			GoddessGiftData goddessGiftData = fGameManager.mloadConfig.AllGoddessGift[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + goddessGiftData.sIconName + ".txt", this.bgRimg);
			this.descTxt1.text = string.Format(goddessGiftData.sActDesc1, dateTime.ToString("yyyy.MM.dd"), dateTime2.AddDays(-1.0).ToString("yyyy.MM.dd"));
			this.descTxt2.text = goddessGiftData.sActDesc2;
			int goddessGiftReward = LiBaoManager.GetInstance().GetGoddessGiftReward();
			int num = goddessGiftReward;
			if (num != 1)
			{
				if (num != 2)
				{
					this.getBtn.interactable = true;
					this.getBtnTxt.text = Language.GetLang(42067, new object[0]);
				}
				else
				{
					this.getBtn.interactable = false;
					this.getBtnTxt.text = Language.GetLang(42069, new object[0]);
				}
			}
			else
			{
				this.getBtn.interactable = true;
				this.getBtnTxt.text = Language.GetLang(42068, new object[0]);
			}
		}
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0009047C File Offset: 0x0008E67C
	private void OnGetBtnClick(GameObject go)
	{
		int goddessGiftReward = LiBaoManager.GetInstance().GetGoddessGiftReward();
		int num = goddessGiftReward;
		if (num != 0)
		{
			if (num == 1)
			{
				fGameManager.SendMsgToHallServer(1315, new ArrayList
				{
					15026,
					new ArrayList()
				});
			}
		}
		else
		{
			fUIManager.GetInstance().OpenChongZhiUI("GoddessGiftUICon");
		}
	}

	// Token: 0x04000C66 RID: 3174
	public static GoddessGiftUICon inst;

	// Token: 0x04000C67 RID: 3175
	private RawImage bgRimg;

	// Token: 0x04000C68 RID: 3176
	private Text descTxt1;

	// Token: 0x04000C69 RID: 3177
	private Text descTxt2;

	// Token: 0x04000C6A RID: 3178
	private Button getBtn;

	// Token: 0x04000C6B RID: 3179
	private Text getBtnTxt;
}
