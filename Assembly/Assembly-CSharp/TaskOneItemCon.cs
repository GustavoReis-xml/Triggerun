using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FF RID: 1023
public class TaskOneItemCon : Window
{
	// Token: 0x06002ADE RID: 10974 RVA: 0x00179930 File Offset: 0x00177B30
	protected override void Awake()
	{
		base.Awake();
		TaskOneItemCon.inst = this;
		this.TaskNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.DescText = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.TaskIco = this.UIGameObj.Get_GameObj(2).GetComponent<RawImage>();
		this.RewardIco = this.UIGameObj.Get_GameObj(3).GetComponent<RawImage>();
		this.pIsNew = this.UIGameObj.Get_GameObj(4);
		this.JinBiText = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		Button component = base.GetComponent<Button>();
		component.onClick.RemoveAllListeners();
		component.onClick.AddListener(delegate()
		{
			this.OnBuyClick();
		});
		this.pPinZhiBtn = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(this.pPinZhiBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnRefPinZhiClick);
		EventTriggerListener.Get(this.pPinZhiBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x00179A44 File Offset: 0x00177C44
	private void Start()
	{
		this.IsInitOk = true;
		this.OnSetData(this.TaskData, this.BaseXmlData);
		this.OnUpdateData(this.TaskData);
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x00179A70 File Offset: 0x00177C70
	public void OnSetData(taskinfo tf, TaskBaseData BaseXml)
	{
		this.TaskData = tf;
		this.BaseXmlData = BaseXml;
		bool flag = !this.IsInitOk;
		if (!flag)
		{
			DateTime dateTime = DateTime.Parse(this.TaskData.sStartTime);
			DateTime serverDate = Fun.GetServerDate();
			bool flag2 = this.pIsNew != null;
			if (flag2)
			{
				bool flag3 = dateTime.Date == serverDate.Date;
				if (flag3)
				{
					this.pIsNew.SetActive(true);
				}
				else
				{
					this.pIsNew.SetActive(false);
				}
			}
			bool flag4 = this.pPinZhiBtn != null;
			if (flag4)
			{
				bool flag5 = tf.m_nTasktype == 2;
				if (flag5)
				{
					this.pPinZhiBtn.SetActive(true);
				}
				else
				{
					this.pPinZhiBtn.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x00179B44 File Offset: 0x00177D44
	public void OnUpdateData(taskinfo tf)
	{
		string text = string.Empty;
		string format = string.Empty;
		this.TaskData = tf;
		bool flag = !this.IsInitOk;
		if (!flag)
		{
			this.TaskNameText.text = string.Concat(new string[]
			{
				"<color=",
				TaskManager.GetQualColor(tf.m_nPinZhi),
				">",
				this.BaseXmlData.m_sTaskName,
				"</color>"
			});
			text = TaskManager.GetInstance().GetsTarget(this.BaseXmlData, this.TaskData);
			this.DescText.text = text;
			this.JinBiText.text = string.Empty;
			this.RewardIco.gameObject.SetActive(false);
			bool flag2 = this.BaseXmlData.m_szReward.Count > 0;
			if (flag2)
			{
				ArrayList arrayList = (ArrayList)this.BaseXmlData.m_szReward[0];
				int key = (int)arrayList[0];
				int num = (int)arrayList[1];
				bool flag3 = !fGameManager.mloadConfig.AllItemData.ContainsKey(key);
				if (flag3)
				{
					Debug.Log("任务Id" + tf.m_nTaskId.ToString() + "奖励物品Id错误---" + key.ToString());
					return;
				}
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
				int nHeroId = itemData.m_nHeroId;
				bool flag4 = nHeroId == 0;
				if (flag4)
				{
					this.JinBiText.text = num.ToString();
					format = ShopManager.GetIcoPath(itemData.m_nType);
					this.RewardIco.gameObject.SetActive(true);
					fUIManager.GetInstance().GetIconAsset(string.Format(format, itemData.m_sIco), this.RewardIco);
				}
				else
				{
					bool flag5 = !fGameManager.mloadConfig.AllHeroList.ContainsKey(nHeroId);
					if (flag5)
					{
						Debug.Log(string.Concat(new string[]
						{
							"任务Id",
							tf.m_nTaskId.ToString(),
							"奖励物品Id错误---",
							key.ToString(),
							"---heroId===",
							nHeroId.ToString()
						}));
						return;
					}
					this.RewardIco.gameObject.SetActive(true);
					HeroData heroData = fGameManager.mloadConfig.AllHeroList[nHeroId];
					fUIManager.GetInstance().GetIconAsset(string.Format("herounlock/skin/{0}_{1}.txt", heroData.hhead, 1), this.RewardIco);
					this.JinBiText.text = heroData.hname;
				}
			}
			else
			{
				this.JinBiText.text = this.BaseXmlData.m_nMoney.ToString();
				this.RewardIco.gameObject.SetActive(true);
				fUIManager.GetInstance().GetIconAsset(string.Format("ItemIco/{0}.txt", "jinbi"), this.RewardIco);
			}
			fUIManager.GetInstance().GetIconAsset("TaskIco/" + this.BaseXmlData.m_sIco + ".txt", this.TaskIco);
		}
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x00179E6B File Offset: 0x0017806B
	private void OnBuyClick()
	{
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x00179E70 File Offset: 0x00178070
	private void OnRefPinZhiClick(GameObject obj)
	{
		TaskManager.GetInstance().TaskCmdToServer(71006, new ArrayList
		{
			this.TaskData.m_nTaskId,
			this.TaskData.m_nAutoId
		});
	}

	// Token: 0x04002473 RID: 9331
	public static TaskOneItemCon inst;

	// Token: 0x04002474 RID: 9332
	private taskinfo TaskData;

	// Token: 0x04002475 RID: 9333
	private TaskBaseData BaseXmlData;

	// Token: 0x04002476 RID: 9334
	private Text TaskNameText;

	// Token: 0x04002477 RID: 9335
	private Text DescText;

	// Token: 0x04002478 RID: 9336
	private RawImage TaskIco;

	// Token: 0x04002479 RID: 9337
	private GameObject pPinZhiBtn;

	// Token: 0x0400247A RID: 9338
	private RawImage RewardIco;

	// Token: 0x0400247B RID: 9339
	private GameObject pIsNew;

	// Token: 0x0400247C RID: 9340
	private Text JinBiText;

	// Token: 0x0400247D RID: 9341
	private GameObject pCube;

	// Token: 0x0400247E RID: 9342
	private bool IsInitOk;
}
