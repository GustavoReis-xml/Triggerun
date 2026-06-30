using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E8 RID: 488
public class HuiliuUICon : Window
{
	// Token: 0x060015E4 RID: 5604 RVA: 0x000CCDD0 File Offset: 0x000CAFD0
	protected override void Awake()
	{
		base.Awake();
		this.blurRimg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.timeTxt = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.gridRender = this.UIGameObj.Get_GameObj(2);
		this.gridRender.SetActive(false);
		this.noteTxt = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.getRewardBtn = this.UIGameObj.Get_GameObj(4).GetComponent<Button>();
		EventTriggerListener.Get(this.getRewardBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGetRewardBtn);
		EventTriggerListener.Get(this.getRewardBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		HuiliuUICon.inst = this;
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x000CCEA7 File Offset: 0x000CB0A7
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x000CCEB1 File Offset: 0x000CB0B1
	public override void Reset()
	{
		base.Reset();
		this.ShowUIData();
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x000CCEC4 File Offset: 0x000CB0C4
	private void Update()
	{
		bool flag = this.blurRimg.texture != null;
		if (flag)
		{
			this.blurRimg.enabled = true;
		}
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000CCEF8 File Offset: 0x000CB0F8
	public void ShowUIData()
	{
		ArrayList huiliuActData = LiBaoManager.GetInstance().GetHuiliuActData();
		bool flag = huiliuActData.Count == 0;
		if (!flag)
		{
			int num = (int)huiliuActData[3];
			bool flag2 = num != 1;
			if (!flag2)
			{
				int key = (int)huiliuActData[0];
				bool flag3 = !fGameManager.mloadConfig.AllHuiliuActData.ContainsKey(key);
				if (!flag3)
				{
					HuiliuActData huiliuActData2 = fGameManager.mloadConfig.AllHuiliuActData[key];
					DateTime dateTime = DateTime.Parse(huiliuActData[1].ToString());
					DateTime dateTime2 = DateTime.Parse(huiliuActData[2].ToString()).AddDays(-1.0);
					this.timeTxt.text = Language.GetLang(28013, new object[]
					{
						dateTime.Year,
						dateTime.Month,
						dateTime.Day,
						dateTime2.Year,
						dateTime2.Month,
						dateTime2.Day
					});
					this.noteTxt.text = huiliuActData2.sDesc;
					fUIManager.OnClearObj(this.szRewardGrid);
					for (int i = 0; i < huiliuActData2.szReward.Length; i += 2)
					{
						bool flag4 = !string.IsNullOrEmpty(huiliuActData2.szReward[i]);
						if (flag4)
						{
							key = int.Parse(huiliuActData2.szReward[i]);
							bool flag5 = fGameManager.mloadConfig.AllItemData.ContainsKey(key);
							if (flag5)
							{
								ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
								GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gridRender);
								gameObject.transform.SetParent(this.gridRender.transform.parent);
								gameObject.transform.localPosition = Vector3.zero;
								gameObject.transform.localScale = Vector3.one;
								gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
								gameObject.name = "ItemGrid" + itemData.m_nItemId.ToString();
								gameObject.SetActive(true);
								this.szRewardGrid.Add(gameObject);
								RawImage component = gameObject.transform.Find("Image").GetComponent<RawImage>();
								fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
								Text component2 = gameObject.transform.Find("Label").GetComponent<Text>();
								component2.text = string.Format("<color=" + Fun.GetQualColor(itemData.m_nQuality) + ">{0}*{1}</color>", itemData.m_sName, huiliuActData2.szReward[i + 1]);
							}
						}
					}
					this.blurRimg.enabled = false;
					this.blurRimg.texture = null;
					fUIManager.tipBlur.rawImage = this.blurRimg;
					fUIManager.tipBlur.enabled = true;
				}
			}
		}
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x000CD22C File Offset: 0x000CB42C
	private void OnGetRewardBtn(GameObject go)
	{
		fGameManager.SendMsgToHallServer(1315, new ArrayList
		{
			15036,
			new ArrayList()
		});
		this.blurRimg.enabled = false;
		this.blurRimg.texture = null;
		this.Close();
	}

	// Token: 0x04001107 RID: 4359
	public static HuiliuUICon inst;

	// Token: 0x04001108 RID: 4360
	private RawImage blurRimg;

	// Token: 0x04001109 RID: 4361
	private Text timeTxt;

	// Token: 0x0400110A RID: 4362
	private GameObject gridRender;

	// Token: 0x0400110B RID: 4363
	private List<GameObject> szRewardGrid = new List<GameObject>();

	// Token: 0x0400110C RID: 4364
	private Text noteTxt;

	// Token: 0x0400110D RID: 4365
	private Button getRewardBtn;
}
