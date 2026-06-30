using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000228 RID: 552
public class LuckyDrawUICon : Window
{
	// Token: 0x060018DE RID: 6366 RVA: 0x000E8510 File Offset: 0x000E6710
	protected override void Awake()
	{
		base.Awake();
		this.timeText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		GameObject gameObject = this.UIGameObj.Get_GameObj(1);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(2);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(3);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(6);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(8);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(9);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(10);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(11);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(12);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(13);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(14);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverRewardGrid);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitRewardGrid);
		this.szRewardGird.Add(gameObject);
		this.luckyValueBar = this.UIGameObj.Get_GameObj(15).GetComponent<Image>();
		this.luckyValueTip = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.rankRender = this.UIGameObj.Get_GameObj(17);
		this.rankRender.SetActive(false);
		this.luckyDrawBtn = this.UIGameObj.Get_GameObj(18).GetComponent<Button>();
		this.luckyDrawBtnTxt = this.luckyDrawBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.luckyDrawBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLuckyDraw);
		EventTriggerListener.Get(this.luckyDrawBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(19);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(20);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(21);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(22);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(23);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(24);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(25);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(26);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(27);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(28);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(29);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(30);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(31);
		this.szRewardPart.Add(gameObject);
		gameObject = this.UIGameObj.Get_GameObj(32);
		this.szRewardPart.Add(gameObject);
		this.rankList = this.UIGameObj.Get_GameObj(33).GetComponent<ScrollRect>();
		this.bgRimg = this.UIGameObj.Get_GameObj(35).GetComponent<RawImage>();
		this.costIcon = this.UIGameObj.Get_GameObj(37).GetComponent<Image>();
		this.costNumTxt = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.haveIcon = this.UIGameObj.Get_GameObj(40).GetComponent<Image>();
		this.haveNumTxt = this.UIGameObj.Get_GameObj(41).GetComponent<Text>();
		this.buyItemBtn = this.UIGameObj.Get_GameObj(42);
		EventTriggerListener.Get(this.buyItemBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuyBtn);
		EventTriggerListener.Get(this.buyItemBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		LuckyDrawUICon.inst = this;
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000E8C58 File Offset: 0x000E6E58
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.InitUIData();
		this.ResetUIData(0);
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000E8C94 File Offset: 0x000E6E94
	private void Update()
	{
		bool flag = this.isAnimPlay && Time.time - this.loopDelay >= this.szLoopTime[this.nLoopCount];
		if (flag)
		{
			this.loopDelay = Time.time;
			this.szRewardPart[this.nLoopIdx].SetActive(false);
			this.nLoopIdx++;
			bool flag2 = this.nLoopIdx >= this.szRewardPart.Count;
			if (flag2)
			{
				this.nLoopIdx = 0;
			}
			bool flag3 = this.nLoopIdx == this.nLoopStart;
			if (flag3)
			{
				this.nLoopCount++;
			}
			this.szRewardPart[this.nLoopIdx].SetActive(true);
			fUISoundManager.instance.PlayUISound("ui_huodong_choujiang", 0, null, 20);
			bool flag4 = this.nLoopCount == this.szLoopTime.Length - 1 && this.nLoopIdx == this.nLoopEnd;
			if (flag4)
			{
				this.GetLuckyDrawReward();
			}
		}
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x000E8DA6 File Offset: 0x000E6FA6
	public void OnEnable()
	{
		this.rankList.content.anchoredPosition3D = Vector3.zero;
		this.isRankRool = false;
		this.StartRankRoll();
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000E8DCD File Offset: 0x000E6FCD
	public void OnDisable()
	{
		this.isRankRool = true;
		this.rankTw.Pause<Tweener>();
		this.rankTw.Kill(false);
		this.rankTw = null;
		this.ResetDrawAnim();
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x000E8E00 File Offset: 0x000E7000
	private void StartRankRoll()
	{
		bool flag = !LuckyDrawUICon.inst.visible || (this.rankTw != null && this.isRankRool) || this.szRankRender.Count <= 11;
		if (!flag)
		{
			bool flag2 = this.rankTw != null;
			if (flag2)
			{
				this.rankTw.Pause<Tweener>();
				this.rankTw.Kill(false);
				this.rankTw = null;
			}
			this.isRankRool = true;
			this.rankList.content.anchoredPosition3D = Vector3.zero;
			this.rankTw = this.rankList.content.DOAnchorPosY(this.movePos, this.rankMove, false);
			this.rankTw.OnUpdate(delegate
			{
				bool flag3 = this.rankList.content.anchoredPosition3D.y >= 50f;
				if (flag3)
				{
					UnityEngine.Object.DestroyImmediate(this.szRankRender[0]);
					this.szRankRender.RemoveAt(0);
					this.isRankRool = false;
					this.StartRankRoll();
					bool flag4 = this.szRankRender.Count < 15;
					if (flag4)
					{
						this.AddRankMsg(0);
					}
				}
			});
		}
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x000E8ED0 File Offset: 0x000E70D0
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.ResetUIData(0);
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x000E8F0C File Offset: 0x000E710C
	private void InitUIData()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType == null;
		if (!flag)
		{
			LuckyDrawData luckyDrawData = fGameManager.mloadConfig.AllLuckyDraw[actDataByType.nXmlId];
			fUIManager.GetInstance().GetIconAsset("libao/" + luckyDrawData.sIconName + ".txt", this.bgRimg);
			DateTime dateTime = DateTime.Parse(actDataByType.sStartTime);
			DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime).AddDays(-1.0);
			this.timeText.text = Language.GetLang(28004, new object[]
			{
				dateTime.Month,
				dateTime.Day,
				dateTime2.Month,
				dateTime2.Day
			});
			for (int i = 0; i < this.szRewardGird.Count; i++)
			{
				int num = int.Parse(this.szRewardGird[i].name.Replace("Render", string.Empty));
				LuckyDrawReward luckyDrawReward = luckyDrawData.szLuckyDrawReward[num];
				bool flag2 = num != 1;
				if (flag2)
				{
					ItemData itemData = fGameManager.mloadConfig.AllItemData[int.Parse(luckyDrawReward.szShowItem[0])];
					RawImage component = this.szRewardGird[i].transform.Find("RawImage").GetComponent<RawImage>();
					fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
					Text component2 = this.szRewardGird[i].transform.Find("Text").GetComponent<Text>();
					component2.text = string.Format("<color=" + Fun.GetQualColor(itemData.m_nQuality) + ">{0}*{1}</color>", itemData.m_sName, luckyDrawReward.szShowItem[1]);
				}
			}
			this.szRewardPart[this.nLoopStart].SetActive(true);
			ActivityManager.GetInstance().SendActMsgToServer(16108, new ArrayList());
		}
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x000E9158 File Offset: 0x000E7358
	public void ResetUIData(int ntype)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType == null;
		if (!flag)
		{
			LuckyDrawData luckyDrawData = fGameManager.mloadConfig.AllLuckyDraw[actDataByType.nXmlId];
			ArrayList szLuckyDrawData = LiBaoManager.GetInstance().m_szLuckyDrawData;
			bool flag2 = ntype == 1;
			if (flag2)
			{
				this.luckyValueBar.DOFillAmount((float)((int)szLuckyDrawData[2]) * 1f / (float)luckyDrawData.totalRate * 1f, 1f);
				this.luckyValueTip.text = "<color=#00ff00>" + Language.GetLang(28012, new object[0]) + "</color>";
				this.luckyValueTip.rectTransform.anchoredPosition3D = Vector3.zero;
				this.luckyValueTip.enabled = true;
				Tweener t = this.luckyValueTip.rectTransform.DOAnchorPosY(80f, 1f, false);
				t.OnComplete(delegate
				{
					this.luckyValueTip.enabled = false;
				});
			}
			else
			{
				this.luckyValueBar.fillAmount = (float)((int)szLuckyDrawData[2]) * 1f / (float)luckyDrawData.totalRate * 1f;
				this.luckyValueTip.enabled = false;
			}
			bool flag3 = int.Parse(luckyDrawData.costNum[0]) == 10;
			if (flag3)
			{
				this.costIcon.sprite = Window.spriteName(AtlasType.ATLAS_LUCKYDRAW, "luckydraw_xingyunquan");
				this.haveIcon.sprite = Window.spriteName(AtlasType.ATLAS_LUCKYDRAW, "luckydraw_xingyunquan");
				this.costNumTxt.text = luckyDrawData.costNum[2];
				this.haveNumTxt.text = ShopManager.GetInstance().GetHaveItemNum(int.Parse(luckyDrawData.costNum[1])).ToString();
				this.buyItemBtn.SetActive(true);
			}
			else
			{
				bool flag4 = int.Parse(luckyDrawData.costNum[0]) == 2;
				if (flag4)
				{
					this.costIcon.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
					this.haveIcon.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_zuanshi");
					this.costNumTxt.text = luckyDrawData.costNum[1];
					this.haveNumTxt.text = PlayerDataManager.getInstance().playerdet.chargeCurrency.ToString();
					this.buyItemBtn.SetActive(false);
				}
				else
				{
					bool flag5 = int.Parse(luckyDrawData.costNum[0]) == 0;
					if (flag5)
					{
						this.costIcon.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
						this.haveIcon.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_jinbi");
						this.costNumTxt.text = luckyDrawData.costNum[1];
						this.haveNumTxt.text = PlayerDataManager.getInstance().playerdet.gameCurrency.ToString();
						this.buyItemBtn.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x000E9468 File Offset: 0x000E7668
	public void AddRankMsg(int nType)
	{
		ArrayList szLuckyDrawRank = ActivityManager.GetInstance().m_szLuckyDrawRank;
		bool flag = nType == 0;
		if (flag)
		{
			this.isRankClone = true;
			for (int i = 0; i < szLuckyDrawRank.Count; i++)
			{
				ArrayList arrayList = (ArrayList)szLuckyDrawRank[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rankRender);
				gameObject.transform.SetParent(this.rankRender.transform.parent);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject.name = "rankRender" + this.rankIdx.ToString();
				gameObject.SetActive(true);
				this.szRankRender.Add(gameObject);
				ItemData itemData = fGameManager.mloadConfig.AllItemData[(int)arrayList[1]];
				Text component = gameObject.GetComponent<Text>();
				component.text = Language.GetLang(28003, new object[]
				{
					arrayList[0].ToString(),
					Fun.GetQualColor(itemData.m_nQuality),
					itemData.m_sName,
					arrayList[2].ToString()
				});
				this.rankIdx++;
			}
		}
		else
		{
			bool flag2 = !this.isRankClone;
			if (flag2)
			{
				ArrayList arrayList2 = (ArrayList)szLuckyDrawRank[szLuckyDrawRank.Count - 1];
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.rankRender);
				gameObject2.transform.SetParent(this.rankRender.transform.parent);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject2.name = "rankRender" + this.rankIdx.ToString();
				gameObject2.SetActive(true);
				this.szRankRender.Add(gameObject2);
				ItemData itemData2 = fGameManager.mloadConfig.AllItemData[(int)arrayList2[1]];
				Text component2 = gameObject2.GetComponent<Text>();
				component2.text = Language.GetLang(28003, new object[]
				{
					arrayList2[0].ToString(),
					Fun.GetQualColor(itemData2.m_nQuality),
					itemData2.m_sName,
					arrayList2[2].ToString()
				});
				this.rankIdx++;
			}
		}
		this.isRankClone = false;
		this.StartRankRoll();
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000E9738 File Offset: 0x000E7938
	private void OnClickLuckyDraw(GameObject go)
	{
		bool flag = this.isAnimPlay;
		if (!flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			bool flag2 = !string.IsNullOrEmpty(this.sClickTime) && (serverDate - DateTime.Parse(this.sClickTime)).TotalSeconds < 1.0;
			if (!flag2)
			{
				this.sClickTime = serverDate.ToString();
				ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
				bool flag3 = actDataByType == null;
				if (!flag3)
				{
					ArrayList arrayList = LiBaoManager.GetInstance().m_szLuckyDrawData;
					bool flag4 = arrayList.Count == 0;
					if (!flag4)
					{
						arrayList = (ArrayList)arrayList[4];
						bool flag5 = arrayList.Count > 0;
						if (!flag5)
						{
							LuckyDrawData luckyDrawData = fGameManager.mloadConfig.AllLuckyDraw[actDataByType.nXmlId];
							bool flag6 = int.Parse(luckyDrawData.costNum[0]) == 2 && PlayerDataManager.getInstance().playerdet.chargeCurrency < int.Parse(luckyDrawData.costNum[1]);
							if (flag6)
							{
								fUIManager.GetInstance().OpenChongZhiUI("chongzhi");
							}
							else
							{
								bool flag7 = int.Parse(luckyDrawData.costNum[0]) != 0 || PlayerDataManager.getInstance().playerdet.gameCurrency >= int.Parse(luckyDrawData.costNum[1]);
								if (flag7)
								{
									bool flag8 = int.Parse(luckyDrawData.costNum[0]) == 10 && int.Parse(luckyDrawData.costNum[2]) > ShopManager.GetInstance().GetHaveItemNum(int.Parse(luckyDrawData.costNum[1]));
									if (flag8)
									{
										ShopManager.GetInstance().OnBuyItem(int.Parse(luckyDrawData.costNum[1]));
									}
									else
									{
										fGameManager.SendMsgToHallServer(1315, new ArrayList
										{
											15029,
											new ArrayList()
										});
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000E9935 File Offset: 0x000E7B35
	private void ExitRewardGrid(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000E9944 File Offset: 0x000E7B44
	private void OverRewardGrid(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType == null;
		if (flag)
		{
			TipManager.GetInstance().HideTextTip();
		}
		else
		{
			LuckyDrawData luckyDrawData = fGameManager.mloadConfig.AllLuckyDraw[actDataByType.nXmlId];
			int key = int.Parse(go.name.Replace("Render", string.Empty));
			LuckyDrawReward luckyDrawReward = luckyDrawData.szLuckyDrawReward[key];
			string sText = string.Concat(new string[]
			{
				luckyDrawReward.sShowName,
				"\n",
				luckyDrawReward.sDesc1,
				"\n",
				luckyDrawReward.sDesc2
			});
			TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x000E9A10 File Offset: 0x000E7C10
	public void ShowRewardAnim()
	{
		ArrayList szLuckyDrawData = LiBaoManager.GetInstance().m_szLuckyDrawData;
		bool flag = szLuckyDrawData.Count != 0;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)szLuckyDrawData[4];
			bool flag2 = arrayList.Count != 0 && !this.isAnimPlay;
			if (flag2)
			{
				this.isAnimPlay = true;
				this.nLoopEnd = (int)szLuckyDrawData[3] - 1;
				this.nLoopIdx = this.nLoopStart;
				this.nLoopCount = 0;
				this.loopDelay = Time.time;
			}
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000E9A98 File Offset: 0x000E7C98
	private void ResetDrawAnim()
	{
		bool flag = !(LuckyDrawUICon.inst == null);
		if (flag)
		{
			this.szRewardPart[this.nLoopIdx].SetActive(false);
			this.nLoopIdx = 0;
			this.nLoopStart = 0;
			this.szRewardPart[this.nLoopStart].SetActive(true);
			bool flag2 = this.isAnimPlay;
			if (flag2)
			{
				this.GetLuckyDrawReward();
			}
		}
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000E9B0C File Offset: 0x000E7D0C
	private void GetLuckyDrawReward()
	{
		this.nLoopStart = this.nLoopIdx;
		this.nLoopCount = 0;
		fGameManager.SendMsgToHallServer(1315, new ArrayList
		{
			15028,
			new ArrayList()
		});
		this.isAnimPlay = false;
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000E9B64 File Offset: 0x000E7D64
	private void OnClickBuyBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType != null;
		if (flag)
		{
			LuckyDrawData luckyDrawData = fGameManager.mloadConfig.AllLuckyDraw[actDataByType.nXmlId];
			bool flag2 = int.Parse(luckyDrawData.costNum[0]) == 10;
			if (flag2)
			{
				ShopManager.GetInstance().OnBuyItem(int.Parse(luckyDrawData.costNum[1]));
			}
		}
	}

	// Token: 0x0400138C RID: 5004
	public static LuckyDrawUICon inst;

	// Token: 0x0400138D RID: 5005
	private Text timeText;

	// Token: 0x0400138E RID: 5006
	private List<GameObject> szRewardGird = new List<GameObject>();

	// Token: 0x0400138F RID: 5007
	private List<GameObject> szRewardPart = new List<GameObject>();

	// Token: 0x04001390 RID: 5008
	private Image luckyValueBar;

	// Token: 0x04001391 RID: 5009
	private Text luckyValueTip;

	// Token: 0x04001392 RID: 5010
	private GameObject rankRender;

	// Token: 0x04001393 RID: 5011
	private List<GameObject> szRankRender = new List<GameObject>();

	// Token: 0x04001394 RID: 5012
	private Button luckyDrawBtn;

	// Token: 0x04001395 RID: 5013
	private Text luckyDrawBtnTxt;

	// Token: 0x04001396 RID: 5014
	private ScrollRect rankList;

	// Token: 0x04001397 RID: 5015
	private RawImage bgRimg;

	// Token: 0x04001398 RID: 5016
	private Image costIcon;

	// Token: 0x04001399 RID: 5017
	private Text costNumTxt;

	// Token: 0x0400139A RID: 5018
	private Image haveIcon;

	// Token: 0x0400139B RID: 5019
	private Text haveNumTxt;

	// Token: 0x0400139C RID: 5020
	private GameObject buyItemBtn;

	// Token: 0x0400139D RID: 5021
	private string sClickTime;

	// Token: 0x0400139E RID: 5022
	private int nLoopStart;

	// Token: 0x0400139F RID: 5023
	private int nLoopEnd;

	// Token: 0x040013A0 RID: 5024
	private int nLoopIdx;

	// Token: 0x040013A1 RID: 5025
	private float[] szLoopTime = new float[]
	{
		0.1f,
		0.2f
	};

	// Token: 0x040013A2 RID: 5026
	private int nLoopCount;

	// Token: 0x040013A3 RID: 5027
	private float loopDelay;

	// Token: 0x040013A4 RID: 5028
	private bool isAnimPlay;

	// Token: 0x040013A5 RID: 5029
	public float rankMove = 1f;

	// Token: 0x040013A6 RID: 5030
	private int rankIdx = 1;

	// Token: 0x040013A7 RID: 5031
	private bool isRankClone;

	// Token: 0x040013A8 RID: 5032
	private bool isRankRool;

	// Token: 0x040013A9 RID: 5033
	private Tweener rankTw;

	// Token: 0x040013AA RID: 5034
	public float movePos = 85f;
}
