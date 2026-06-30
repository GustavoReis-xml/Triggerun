using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200022F RID: 559
public class MailUICon : Window
{
	// Token: 0x06001914 RID: 6420 RVA: 0x000EB1A8 File Offset: 0x000E93A8
	protected override void Awake()
	{
		base.Awake();
		MailUICon.inst = this;
		this.pMailList = this.UIGameObj.Get_GameObj(0);
		this.pOneMail = this.UIGameObj.Get_GameObj(1);
		this.tPageText = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.pYou_btn = this.UIGameObj.Get_GameObj(3);
		this.pZuo_btn = this.UIGameObj.Get_GameObj(4);
		this.pGetItemBtn = this.UIGameObj.Get_GameObj(5);
		this.pDelBtn = this.UIGameObj.Get_GameObj(6);
		this.pGetAllItemBtn = this.UIGameObj.Get_GameObj(7);
		this.tMailTitle = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.tMailText = this.UIGameObj.Get_GameObj(9).GetComponent<LinkImageText>();
		this.pItemList = this.UIGameObj.Get_GameObj(10);
		this.pOneItem = this.UIGameObj.Get_GameObj(11);
		EventTriggerListener.Get(this.pGetItemBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGetItemClick);
		EventTriggerListener.Get(this.pGetItemBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pGetAllItemBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnGetAllItemClick);
		EventTriggerListener.Get(this.pGetAllItemBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pDelBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnDelClick);
		EventTriggerListener.Get(this.pDelBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pYou_btn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.pYou_btn).SetSoundStr("sound_main_mouse", "jiemian_page");
		EventTriggerListener.Get(this.pZuo_btn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFanYeClick);
		EventTriggerListener.Get(this.pZuo_btn).SetSoundStr("sound_main_mouse", "jiemian_page");
		this.pItemBtnText = this.pGetItemBtn.transform.FindChild("Text").GetComponent<Text>();
		this.pOneItem.SetActive(false);
		this.pOneMail.SetActive(false);
		this.pGetItemBtn.SetActive(false);
		this.pGetAllItemBtn.SetActive(false);
		MailManager.OnSendMsgToMailServer(3408, new ArrayList
		{
			0,
			0
		});
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x000EB458 File Offset: 0x000E9658
	public void OnSetMailCount()
	{
		int count = MailManager.GetInstance().AllMailStateList.Count;
		bool flag = count % this.m_nMaxPageNum == 0;
		if (flag)
		{
			this.m_nMaxPage = count / this.m_nMaxPageNum;
		}
		else
		{
			this.m_nMaxPage = count / this.m_nMaxPageNum + 1;
		}
		bool flag2 = this.m_nMaxPage < 1;
		if (flag2)
		{
			this.m_nMaxPage = 1;
			this.OnClearData();
		}
		bool flag3 = this.m_nCurPage == 0;
		if (flag3)
		{
			this.m_nCurPage = 1;
		}
		bool flag4 = this.m_nCurPage > this.m_nMaxPage;
		if (flag4)
		{
			this.m_nCurPage = this.m_nMaxPage;
		}
		this.OnGetPageMailInfo(this.m_nCurPage);
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x000EB50B File Offset: 0x000E970B
	public void OnUpdateGetAllItemBtnState(int nCount)
	{
		this.pGetAllItemBtn.SetActive(nCount > 0);
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x000EB51E File Offset: 0x000E971E
	public override void Reset()
	{
		base.Reset();
		this.m_nCurPage = 1;
		MailManager.GetInstance().OnRestList();
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x000EB53C File Offset: 0x000E973C
	private void OnFanYeClick(GameObject obj)
	{
		string a = string.Empty;
		a = obj.name;
		bool flag = a == "you_btn";
		if (flag)
		{
			bool flag2 = this.m_nCurPage >= this.m_nMaxPage;
			if (flag2)
			{
				return;
			}
			this.m_nCurPage++;
		}
		else
		{
			bool flag3 = this.m_nCurPage <= 1;
			if (flag3)
			{
				return;
			}
			this.m_nCurPage--;
		}
		this.OnGetPageMailInfo(this.m_nCurPage);
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x000EB5C0 File Offset: 0x000E97C0
	public void OnGetPageMailInfo(int nPage)
	{
		bool flag = false;
		ArrayList arrayList = new ArrayList();
		bool flag2 = nPage < 1 || nPage > this.m_nMaxPage;
		if (!flag2)
		{
			List<int> allMailIdList = MailManager.GetInstance().AllMailIdList;
			int count = allMailIdList.Count;
			int num = (nPage != this.m_nMaxPage) ? (this.m_nMaxPageNum * nPage) : count;
			for (int i = (nPage - 1) * this.m_nMaxPageNum; i < num; i++)
			{
				int num2 = allMailIdList[i];
				bool flag3 = !MailManager.GetInstance().AllMailData.ContainsKey(num2);
				if (flag3)
				{
					flag = true;
				}
				arrayList.Add(num2);
			}
			bool flag4 = flag;
			if (flag4)
			{
				MailManager.OnSendMsgToMailServer(3401, arrayList);
			}
			else
			{
				this.OnShowMailList(arrayList);
			}
			this.tPageText.text = this.m_nCurPage.ToString() + "/" + this.m_nMaxPage.ToString();
		}
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000EB6C0 File Offset: 0x000E98C0
	public void OnShowMailList(ArrayList szMsg)
	{
		int count = szMsg.Count;
		fUIManager.OnClearObj(this.m_szAllMailObjList);
		for (int i = 0; i < count; i++)
		{
			int num = (int)szMsg[i];
			bool flag = MailManager.GetInstance().AllMailData.ContainsKey(num);
			if (flag)
			{
				MailInfo mailInfo = MailManager.GetInstance().AllMailData[num];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneMail);
				gameObject.transform.SetParent(this.pMailList.transform);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				OneMailCon oneMailCon = gameObject.AddComponent<OneMailCon>();
				oneMailCon.OnInit(num, i);
				this.m_szAllMailObjList.Add(num, gameObject);
			}
		}
		GuildManager.GetInstance().UpdatePageBtnState(this.m_nMaxPage, this.m_nCurPage, this.pZuo_btn, this.pYou_btn);
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000EB7DC File Offset: 0x000E99DC
	public void OnShowDetailedMail(int nMailId, GameObject obj)
	{
		bool flag = MailManager.GetInstance().AllMailData.ContainsKey(nMailId);
		if (flag)
		{
			bool flag2 = this.pSelectMailObj != null;
			if (flag2)
			{
				this.pSelectMailObj.GetComponent<Button>().enabled = true;
				this.pSelectMailObj.GetComponent<Button>().interactable = true;
			}
			bool flag3 = this.pSelectMailObj != null;
			if (flag3)
			{
				obj.GetComponent<Button>().enabled = false;
			}
			obj.GetComponent<Button>().interactable = false;
			this.pSelectMailObj = obj;
			this.m_nSelectMailId = nMailId;
			MailInfo mailInfo = MailManager.GetInstance().AllMailData[nMailId];
			MailStateInfo mailStateInfo = MailManager.GetInstance().AllMailStateList[nMailId];
			this.tMailTitle.text = mailInfo.m_sMailtitle;
			string text = mailInfo.m_sMailtext;
			text = text.Replace("\\\\r\\\\n", "\n");
			text = text.Replace("\\n", "\n");
			text = text.Replace("\\", string.Empty);
			this.tMailText.onHrefClick.RemoveListener(new UnityAction<string>(this.OnHrefClick));
			this.tMailText.onHrefClick.AddListener(new UnityAction<string>(this.OnHrefClick));
			this.tMailText.m_PreText = text;
			this.tMailText.SetVerticesDirty();
			this.OnShowItemList(mailInfo.m_szItem);
			bool flag4 = mailInfo.m_szItem.Count > 1;
			if (flag4)
			{
				this.pGetItemBtn.SetActive(true);
			}
			else
			{
				this.pGetItemBtn.SetActive(false);
			}
			bool flag5 = mailStateInfo.m_nIsRead == 0;
			if (flag5)
			{
				MailManager.OnSendMsgToMailServer(3403, new ArrayList
				{
					nMailId,
					nMailId
				});
			}
			this.IsCanGetItemBtn(mailStateInfo.m_nIsGet == 0);
		}
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000EB9C3 File Offset: 0x000E9BC3
	private void OnHrefClick(string hrefName)
	{
		Debuger.OpenURL(hrefName);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000EB9D0 File Offset: 0x000E9BD0
	public void OnShowItemList(ArrayList szItem)
	{
		string format = string.Empty;
		int count = szItem.Count;
		fUIManager.OnDelRawImage();
		fUIManager.OnClearObj(this.m_szAllItem);
		for (int i = 0; i < count; i += 2)
		{
			int num = (int)szItem[i];
			int num2 = (int)szItem[i + 1];
			bool flag = fGameManager.mloadConfig.AllItemData.ContainsKey(num);
			if (flag)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[num];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneItem);
				gameObject.transform.SetParent(this.pItemList.transform);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.name = string.Format("OneItem{0}_{1}", num, num2);
				RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
				Text component2 = gameObject.transform.FindChild("Text").GetComponent<Text>();
				format = ShopManager.GetIcoPath(itemData.m_nType);
				bool flag2 = itemData.m_nType != 20;
				if (flag2)
				{
					fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sIco), component);
				}
				component2.text = string.Empty;
				fUIManager.OnAddRawImage(component);
				this.m_szAllItem.Add(gameObject);
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverOneItem);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitOneItem);
			}
		}
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000EBBA4 File Offset: 0x000E9DA4
	private void ExitOneItem(GameObject go)
	{
		Text component = go.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x000EBBD4 File Offset: 0x000E9DD4
	private void OverOneItem(GameObject go)
	{
		string[] array = go.name.Replace("OneItem", string.Empty).Split(new char[]
		{
			'_'
		});
		int key = int.Parse(array[0]);
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		Text component = go.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Concat(new string[]
		{
			"<color=",
			Fun.GetQualColor(itemData.m_nQuality),
			">",
			itemData.m_sName,
			"*",
			array[1],
			"</color>"
		});
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x000EBC88 File Offset: 0x000E9E88
	public void OnUpdateMailInfo(int nMailId, int nType)
	{
		bool flag = this.m_szAllMailObjList.ContainsKey(nMailId);
		if (flag)
		{
			GameObject gameObject = this.m_szAllMailObjList[nMailId];
			OneMailCon component = gameObject.GetComponent<OneMailCon>();
			component.OnUpdateMailInfo(nType);
			bool flag2 = nType == 2;
			if (flag2)
			{
				this.IsCanGetItemBtn(false);
			}
		}
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x000EBCD8 File Offset: 0x000E9ED8
	public void IsCanGetItemBtn(bool isItem)
	{
		this.pGetItemBtn.GetComponent<Button>().interactable = isItem;
		this.pItemBtnText.text = ((!isItem) ? Language.GetLang(38507, new object[0]) : Language.GetLang(38506, new object[0]));
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x000EBD2C File Offset: 0x000E9F2C
	private void OnGetItemClick(GameObject obj)
	{
		bool flag = MailManager.GetInstance().AllMailData.ContainsKey(this.m_nSelectMailId);
		if (flag)
		{
			MailInfo mailInfo = MailManager.GetInstance().AllMailData[this.m_nSelectMailId];
			MailStateInfo mailStateInfo = MailManager.GetInstance().AllMailStateList[this.m_nSelectMailId];
			bool flag2 = mailInfo.m_szItem.Count < 1;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38508, new object[0]), "ffdd00", "system");
			}
			else
			{
				bool flag3 = mailStateInfo.m_nIsGet == 1;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38509, new object[0]), "ffdd00", "system");
				}
				else
				{
					MailManager.OnSendMsgToMailServer(3406, new ArrayList
					{
						this.m_nSelectMailId,
						this.m_nSelectMailId
					});
				}
			}
		}
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x000EBE2C File Offset: 0x000EA02C
	private void OnClearData()
	{
		fUIManager.OnClearObj(this.m_szAllItem);
		this.tMailTitle.text = string.Empty;
		this.tMailText.m_PreText = string.Empty;
		this.tMailText.SetVerticesDirty();
		this.pGetItemBtn.SetActive(false);
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x000EBE80 File Offset: 0x000EA080
	private void OnGetAllItemClick(GameObject obj)
	{
		bool flag = MailManager.GetInstance().AllMailStateList.Count == 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38510, new object[0]), "ffdd00", "system");
		}
		else
		{
			MailManager.OnSendMsgToMailServer(3410, new ArrayList
			{
				this.m_nSelectMailId,
				this.m_nSelectMailId
			});
		}
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x000EBF00 File Offset: 0x000EA100
	private void OnDelClick(GameObject obj)
	{
		bool flag = MailManager.GetInstance().AllMailStateList.Count == 0;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(38510, new object[0]), "ffdd00", "system");
		}
		else
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38511, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnDel), 0, null, null);
		}
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x000EBFB3 File Offset: 0x000EA1B3
	private void OnDel(object obj)
	{
		MailManager.OnSendMsgToMailServer(3409, new ArrayList
		{
			0,
			0
		});
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x000EBFE0 File Offset: 0x000EA1E0
	private void Start()
	{
	}

	// Token: 0x040013D0 RID: 5072
	public static MailUICon inst;

	// Token: 0x040013D1 RID: 5073
	private int m_nMaxPageNum = 7;

	// Token: 0x040013D2 RID: 5074
	private int m_nMaxPage;

	// Token: 0x040013D3 RID: 5075
	private int m_nCurPage;

	// Token: 0x040013D4 RID: 5076
	private int m_nSelectMailId;

	// Token: 0x040013D5 RID: 5077
	private GameObject pSelectMailObj;

	// Token: 0x040013D6 RID: 5078
	private GameObject pMailList;

	// Token: 0x040013D7 RID: 5079
	private GameObject pOneMail;

	// Token: 0x040013D8 RID: 5080
	private Text tPageText;

	// Token: 0x040013D9 RID: 5081
	private GameObject pYou_btn;

	// Token: 0x040013DA RID: 5082
	private GameObject pZuo_btn;

	// Token: 0x040013DB RID: 5083
	private GameObject pGetItemBtn;

	// Token: 0x040013DC RID: 5084
	private Text pItemBtnText;

	// Token: 0x040013DD RID: 5085
	private GameObject pDelBtn;

	// Token: 0x040013DE RID: 5086
	private GameObject pGetAllItemBtn;

	// Token: 0x040013DF RID: 5087
	private Text tMailTitle;

	// Token: 0x040013E0 RID: 5088
	private LinkImageText tMailText;

	// Token: 0x040013E1 RID: 5089
	private GameObject pItemList;

	// Token: 0x040013E2 RID: 5090
	private GameObject pOneItem;

	// Token: 0x040013E3 RID: 5091
	private Dictionary<int, GameObject> m_szAllMailObjList = new Dictionary<int, GameObject>();

	// Token: 0x040013E4 RID: 5092
	private List<GameObject> m_szAllItem = new List<GameObject>();
}
