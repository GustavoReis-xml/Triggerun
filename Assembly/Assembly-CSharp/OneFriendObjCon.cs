using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000275 RID: 629
public class OneFriendObjCon : Window
{
	// Token: 0x06001D23 RID: 7459 RVA: 0x0010C908 File Offset: 0x0010AB08
	protected override void Awake()
	{
		base.Awake();
		this.bgimg = base.transform.GetComponent<Image>();
		this.bgimg.enabled = true;
		this.oBody = base.transform.Find("body").gameObject;
		this.bg = this.oBody.transform.Find("bg").GetComponent<Image>();
		this.top = this.oBody.transform.Find("Raw").GetComponent<RawImage>();
		this.tName = this.oBody.transform.Find("list/name").GetComponent<Text>();
		this.oMsg = this.oBody.transform.Find("list/msg").gameObject;
		this.tMsg = this.oMsg.GetComponent<Text>();
		this.oMsg.SetActive(false);
		this.ttico = this.oBody.transform.Find("ttico").GetComponent<Image>();
		this.oBody.SetActive(false);
		this.tTime = this.oBody.transform.Find("time").GetComponent<Text>();
		this.tTime.text = string.Empty;
		this.btn = base.transform.Find("body").gameObject;
		this.bButton = this.btn.GetComponent<Button>();
		EventTriggerListener.Get(this.btn).onClick = new EventTriggerListener.VoidDelegate(this.OnOpenMsgTip);
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0010CA9A File Offset: 0x0010AC9A
	private void Update()
	{
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0010CA9D File Offset: 0x0010AC9D
	private void InitUI()
	{
		this.top.enabled = false;
		this.tName.text = string.Empty;
		this.ttico.enabled = false;
		this.tMsg.text = string.Empty;
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0010CADC File Offset: 0x0010ACDC
	public void OnSetUI(int nTageType, ArrayList szFrendData)
	{
		this.szFriendMsgData = szFrendData;
		this.m_nTageType = nTageType;
		bool flag = szFrendData.Count < 1;
		if (flag)
		{
			this.InitUI();
			this.oBody.SetActive(false);
			this.bgimg.enabled = true;
		}
		else
		{
			int num = 0;
			string text = string.Empty;
			string text2 = string.Empty;
			this.tTime.text = string.Empty;
			bool flag2 = this.m_nTageType == 0;
			if (flag2)
			{
				this.bButton.interactable = true;
				bool flag3 = (int)szFrendData[5] == 1;
				if (flag3)
				{
					text = szFrendData[1].ToString();
					this.oMsg.SetActive(false);
					this.bg.enabled = true;
					this.bgimg.enabled = false;
				}
				else
				{
					this.bgimg.enabled = true;
					text = "<color='#838383'>" + szFrendData[1].ToString() + "</color>";
					this.bg.enabled = false;
					text2 = szFrendData[4].ToString();
					bool flag4 = text2 != string.Empty;
					if (flag4)
					{
						int endCDTime = this.GetEndCDTime(text2);
						this.tMsg.text = Language.GetLang(31008, new object[]
						{
							this.SecToFullTimeStr(endCDTime)
						});
					}
					else
					{
						this.tMsg.text = Language.GetLang(31012, new object[0]);
					}
					this.oMsg.SetActive(true);
				}
			}
			else
			{
				text = szFrendData[1].ToString();
				int endCDTime2 = this.GetEndCDTime(szFrendData[4].ToString());
				this.tMsg.text = this.SecToFullTimeStr(endCDTime2);
				this.oMsg.SetActive(true);
			}
			this.tName.text = text;
			int key = (int)szFrendData[3];
			this.top.enabled = false;
			bool flag5 = fGameManager.mloadConfig.AllFaceData.ContainsKey(key);
			if (flag5)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[key];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.top);
			}
			bool flag6 = szFrendData.Count > 8;
			if (flag6)
			{
				num = (int)szFrendData[8];
				bool flag7 = num > 8;
				if (flag7)
				{
					num = 8;
				}
				this.ttico.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num.ToString());
			}
			else
			{
				Debug.LogError("szFrendData好友数据没有第8位天梯数据:friendname:" + szFrendData[1].ToString() + ",Count:" + szFrendData.Count.ToString());
			}
			this.ttico.enabled = true;
			this.ttico.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + num.ToString());
			this.oBody.SetActive(true);
		}
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0010CDFC File Offset: 0x0010AFFC
	private int GetEndCDTime(string sEndTime)
	{
		int num = 0;
		bool flag = sEndTime == string.Empty || sEndTime == "0";
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			DateTime d = DateTime.Parse(sEndTime);
			result = (int)(Fun.GetServerDate() - d).TotalSeconds;
		}
		return result;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0010CE50 File Offset: 0x0010B050
	private void OnOpenMsgTip(GameObject obj)
	{
		bool flag = this.szFriendMsgData.Count >= 1;
		if (flag)
		{
			int nType = 2;
			bool flag2 = this.m_nTageType == 1;
			if (flag2)
			{
				nType = 4;
			}
			ArrayList arrayList = new ArrayList();
			bool flag3 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
			if (flag3)
			{
				arrayList.Add(2);
			}
			bool flag4 = GuildManager.GetInstance().m_nZhiWei <= 0;
			if (flag4)
			{
				arrayList.Add(3);
			}
			ChatManager.GetInstance().OnMsgTip(this.szFriendMsgData[1].ToString(), nType, arrayList, (int)this.szFriendMsgData[0]);
		}
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0010CF1C File Offset: 0x0010B11C
	private string SecToFullTimeStr(int sec)
	{
		string empty = string.Empty;
		bool flag = sec <= 0;
		string lang;
		if (flag)
		{
			lang = Language.GetLang(9006, new object[]
			{
				"<1"
			});
		}
		else
		{
			int num = sec / 3600;
			int num2 = sec / 60;
			bool flag2 = num >= 1;
			if (flag2)
			{
				lang = Language.GetLang(9005, new object[]
				{
					num.ToString()
				});
			}
			else
			{
				bool flag3 = num2 >= 1;
				if (flag3)
				{
					lang = Language.GetLang(9006, new object[]
					{
						num2.ToString()
					});
				}
				else
				{
					lang = Language.GetLang(9007, new object[]
					{
						sec.ToString()
					});
				}
			}
		}
		return lang;
	}

	// Token: 0x04001996 RID: 6550
	private GameObject oBody;

	// Token: 0x04001997 RID: 6551
	private Image bg;

	// Token: 0x04001998 RID: 6552
	private RawImage top;

	// Token: 0x04001999 RID: 6553
	private Text tName;

	// Token: 0x0400199A RID: 6554
	private Image ttico;

	// Token: 0x0400199B RID: 6555
	private Image bgimg;

	// Token: 0x0400199C RID: 6556
	private Text tMsg;

	// Token: 0x0400199D RID: 6557
	private GameObject btn;

	// Token: 0x0400199E RID: 6558
	private Button bButton;

	// Token: 0x0400199F RID: 6559
	private GameObject oMsg;

	// Token: 0x040019A0 RID: 6560
	private Text tTime;

	// Token: 0x040019A1 RID: 6561
	private ArrayList szFriendMsgData = new ArrayList();

	// Token: 0x040019A2 RID: 6562
	private int m_nTageType;
}
