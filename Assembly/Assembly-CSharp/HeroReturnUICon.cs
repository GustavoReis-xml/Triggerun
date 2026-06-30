using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B2 RID: 434
public class HeroReturnUICon : Window
{
	// Token: 0x06001407 RID: 5127 RVA: 0x000BCE28 File Offset: 0x000BB028
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.transform.GetComponent<RawImage>();
		this.timeTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.descRender = this.UIGameObj.Get_GameObj(1);
		this.descRender.SetActive(false);
		GameObject gameObject = this.UIGameObj.Get_GameObj(2);
		this.btnTxt = gameObject.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnUrlBtnClick);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.descTxt = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		HeroReturnUICon.inst = this;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x000BCEF6 File Offset: 0x000BB0F6
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x000BCF00 File Offset: 0x000BB100
	public void Update()
	{
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000BCF04 File Offset: 0x000BB104
	public void ShowUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(38);
		bool flag = actDataByType == null || !fGameManager.mloadConfig.AllHeroReturnData.ContainsKey(actDataByType.nXmlId);
		if (!flag)
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
			HeroReturnData heroReturnData = fGameManager.mloadConfig.AllHeroReturnData[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + heroReturnData.sIconName + ".txt", this.bgRimg);
			bool flag2 = this.szDescRender.Count == 0;
			if (flag2)
			{
				for (int i = 0; i < heroReturnData.szActDesc.Length; i += 2)
				{
					bool flag3 = !string.IsNullOrEmpty(heroReturnData.szActDesc[i]);
					if (flag3)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.descRender);
						gameObject.transform.SetParent(this.descRender.transform.parent);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
						gameObject.transform.localScale = Vector3.one;
						gameObject.SetActive(true);
						gameObject.name = "Render" + (i / 2).ToString();
						Text component = gameObject.transform.Find("Text1").GetComponent<Text>();
						component.text = heroReturnData.szActDesc[i];
						component = gameObject.transform.Find("Text2").GetComponent<Text>();
						component.text = heroReturnData.szActDesc[i + 1];
					}
				}
			}
			this.btnTxt.text = heroReturnData.sBtnTxt;
			this.descTxt.text = heroReturnData.sNoteDesc;
		}
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000BD184 File Offset: 0x000BB384
	private void OnUrlBtnClick(GameObject go)
	{
		bool flag = MainUICon.inst != null && MainUICon.inst.visible;
		if (flag)
		{
			MainUICon.inst.ChangeTitleToIdx(1, null);
		}
	}

	// Token: 0x04000F60 RID: 3936
	public static HeroReturnUICon inst;

	// Token: 0x04000F61 RID: 3937
	private RawImage bgRimg;

	// Token: 0x04000F62 RID: 3938
	private Text timeTxt;

	// Token: 0x04000F63 RID: 3939
	private GameObject descRender;

	// Token: 0x04000F64 RID: 3940
	private List<GameObject> szDescRender = new List<GameObject>();

	// Token: 0x04000F65 RID: 3941
	private Text btnTxt;

	// Token: 0x04000F66 RID: 3942
	private Text descTxt;
}
