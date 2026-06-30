using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023B RID: 571
public class MatchModelCon : Window
{
	// Token: 0x06001A29 RID: 6697 RVA: 0x000F79A0 File Offset: 0x000F5BA0
	protected override void Awake()
	{
		base.Awake();
		MatchModelCon.inst = this;
		this.Model1 = this.UIGameObj.Get_GameObj(1);
		this.imgobj = this.Model1.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model1.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model1);
		EventTriggerListener.Get(this.Model1).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model1).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model1).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model2 = this.UIGameObj.Get_GameObj(2);
		this.imgobj = this.Model2.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model2.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model2);
		EventTriggerListener.Get(this.Model2).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model2).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model2).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model3 = this.UIGameObj.Get_GameObj(3);
		this.imgobj = this.Model3.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model3.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model3);
		EventTriggerListener.Get(this.Model3).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model3).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model3).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model4 = this.UIGameObj.Get_GameObj(4);
		this.imgobj = this.Model4.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model4.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model4);
		EventTriggerListener.Get(this.Model4).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model4).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model4).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model5 = this.UIGameObj.Get_GameObj(5);
		this.imgobj = this.Model5.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model5.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model5);
		EventTriggerListener.Get(this.Model5).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model5).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model5).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model6 = this.UIGameObj.Get_GameObj(6);
		this.imgobj = this.Model6.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model6.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model6);
		EventTriggerListener.Get(this.Model6).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model6).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model6).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		this.Model7 = this.UIGameObj.Get_GameObj(7);
		this.imgobj = this.Model7.transform.FindChild("SlcSp").gameObject;
		this.imgobj.SetActive(false);
		this.lockobj = this.Model7.transform.FindChild("LockSp").gameObject;
		this.lockobj.SetActive(false);
		this.ALLModelObj.Add(this.Model7);
		EventTriggerListener.Get(this.Model7).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBtn);
		EventTriggerListener.Get(this.Model7).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterBtn);
		EventTriggerListener.Get(this.Model7).onExit = new EventTriggerListener.VoidDelegate(this.OnExitBtn);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component = base.transform.Find("Text").GetComponent<Text>();
			component.font = fUIManager.mNewComic;
			for (int i = 0; i < this.ALLModelObj.Count; i++)
			{
				component = (this.ALLModelObj[i] as GameObject).transform.FindChild("TextName").GetComponent<Text>();
				component.font = fUIManager.mNewComic;
			}
		}
		this.ScrollObj = this.UIGameObj.Get_GameObj(8);
		this.FirstLoginTiShi = this.UIGameObj.Get_GameObj(9);
		this.UIGameObj.Get_GameObj(10).GetComponent<Text>().text = Language.GetLang(51016, new object[0]);
		this.InitShowModel();
		this.SetLvShow();
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x000F805C File Offset: 0x000F625C
	private void Start()
	{
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.InitFirstYinDao();
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x000F8090 File Offset: 0x000F6290
	private void InitFirstYinDao()
	{
		bool flag = fGameManager.IsNeedShowPiPeiYinDao && !this.hadshowyindao;
		if (flag)
		{
			int count = this.ALLModelObj.Count;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = (GameObject)this.ALLModelObj[i];
				Tweener t = gameObject.transform.Find("arrow").DOLocalMoveY(-245f, 0.8f, false);
				t.SetEase(Ease.Linear);
				t.SetLoops(-1, LoopType.Yoyo);
			}
			this.FirstLoginTiShi.SetActive(true);
		}
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x000F812C File Offset: 0x000F632C
	private void HideAllFirstYinDao()
	{
		int count = this.ALLModelObj.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = (GameObject)this.ALLModelObj[i];
			GameObject gameObject2 = gameObject.transform.Find("arrow").gameObject;
			bool flag = gameObject2 != null;
			if (flag)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x000F8199 File Offset: 0x000F6399
	private void Update()
	{
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x000F819C File Offset: 0x000F639C
	private void InitShowModel()
	{
		this.nDisShow = 0;
		for (int i = 0; i < fGameManager.mloadConfig.AllMatchMode.Count; i++)
		{
			bool flag = fGameManager.mloadConfig.AllMatchMode.ContainsKey(i);
			if (flag)
			{
				MatchModelData matchModelData = fGameManager.mloadConfig.AllMatchMode[i];
				this.InitOneModel((GameObject)this.ALLModelObj[i], matchModelData);
				bool flag2 = !this.ModelObj.ContainsKey(matchModelData.m_nfbtype);
				if (flag2)
				{
					this.ModelObj.Add(matchModelData.m_nfbtype, (GameObject)this.ALLModelObj[i]);
				}
			}
		}
		this.SetPos();
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x000F8260 File Offset: 0x000F6460
	private void InitOneModel(GameObject obj, MatchModelData modeldata)
	{
		int num = 0;
		RawImage component = obj.transform.FindChild("RawImage").GetComponent<RawImage>();
		Text component2 = obj.transform.FindChild("TextName").GetComponent<Text>();
		Text component3 = obj.transform.FindChild("Text").GetComponent<Text>();
		Text component4 = obj.transform.FindChild("fbtype").GetComponent<Text>();
		Text component5 = obj.transform.FindChild("matchnum").GetComponent<Text>();
		GameObject gameObject = obj.transform.FindChild("tj").gameObject;
		bool active = modeldata.m_nIsTuiJian == 1;
		gameObject.SetActive(active);
		for (int i = 0; i < MatchManager.GetInstance().m_szAllModelNum.Count; i += 2)
		{
			bool flag = (int)MatchManager.GetInstance().m_szAllModelNum[i] == modeldata.m_nfbtype;
			if (flag)
			{
				num = 1;
				bool flag2 = (int)MatchManager.GetInstance().m_szAllModelNum[i + 1] < 10000;
				if (flag2)
				{
					component5.text = Language.GetLang(17015, new object[]
					{
						"<color=#00FF00>" + MatchManager.GetInstance().m_szAllModelNum[i + 1] + "</color>"
					});
				}
				else
				{
					bool flag3 = (int)MatchManager.GetInstance().m_szAllModelNum[i + 1] >= 10000 && (int)MatchManager.GetInstance().m_szAllModelNum[i + 1] < 11000;
					if (flag3)
					{
						component5.text = Language.GetLang(17015, new object[]
						{
							"<color=#00FF00>1万</color>"
						});
					}
					else
					{
						component5.text = Language.GetLang(17015, new object[]
						{
							"<color=#00FF00>" + ((float)((int)MatchManager.GetInstance().m_szAllModelNum[i + 1]) / 10000f).ToString("0.0") + "万</color>"
						});
					}
				}
			}
		}
		bool flag4 = num == 0;
		if (flag4)
		{
			component5.text = Language.GetLang(17015, new object[]
			{
				"<color=#00FF00>" + 0.ToString() + "</color>"
			});
		}
		component2.text = modeldata.m_sName;
		component3.text = modeldata.m_sDesc;
		component4.text = modeldata.m_nfbtype.ToString();
		component.enabled = false;
		fUIManager.GetInstance().GetIconAsset("matchmodel/" + modeldata.m_sIcon + ".txt", component);
		bool flag5 = modeldata.m_nIsOpen == 0;
		if (flag5)
		{
			obj.SetActive(false);
			this.nDisShow++;
		}
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x000F8550 File Offset: 0x000F6750
	public void SetMatchModelUI()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < MatchManager.GetInstance().m_szAllModelNum.Count; i += 2)
		{
			arrayList.Add(MatchManager.GetInstance().m_szAllModelNum[i].ToString());
			arrayList.Add(MatchManager.GetInstance().m_szAllModelNum[i + 1]);
		}
		foreach (int key in this.ModelObj.Keys)
		{
			GameObject gameObject = this.ModelObj[key];
			Text component = gameObject.transform.FindChild("matchnum").GetComponent<Text>();
			int num = arrayList.IndexOf(key.ToString());
			bool flag = num != -1;
			if (flag)
			{
				bool flag2 = (int)arrayList[num + 1] < 10000;
				if (flag2)
				{
					component.text = Language.GetLang(17015, new object[]
					{
						"<color=#00FF00>" + arrayList[num + 1] + "</color>"
					});
				}
				else
				{
					bool flag3 = (int)arrayList[num + 1] >= 10000 && (int)arrayList[num + 1] < 11000;
					if (flag3)
					{
						component.text = Language.GetLang(17015, new object[]
						{
							"<color=#00FF00>" + Language.GetLang(46092, new object[]
							{
								1
							}) + "</color>"
						});
					}
					else
					{
						component.text = Language.GetLang(17015, new object[]
						{
							"<color=#00FF00>" + Language.GetLang(46092, new object[]
							{
								((float)((int)arrayList[num + 1]) / 10000f).ToString("0.0")
							}) + "</color>"
						});
					}
				}
			}
			else
			{
				component.text = Language.GetLang(17015, new object[]
				{
					"<color=#00FF00>0</color>"
				});
			}
		}
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x000F87B4 File Offset: 0x000F69B4
	private void SetPos()
	{
		bool flag = this.nDisShow != 0;
		if (flag)
		{
			Vector3 vector = default(Vector3);
			vector = this.ScrollObj.transform.localPosition;
			vector.x = (float)(this.nDisShow * 218 / 2 + 40);
			this.ScrollObj.transform.localPosition = new Vector3(vector.x, vector.y, vector.z);
		}
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x000F882C File Offset: 0x000F6A2C
	private void OnEnterBtn(GameObject obj)
	{
		GameObject gameObject = obj.transform.FindChild("SlcSp").gameObject;
		gameObject.SetActive(true);
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x000F8858 File Offset: 0x000F6A58
	private void OnExitBtn(GameObject obj)
	{
		GameObject gameObject = obj.transform.FindChild("SlcSp").gameObject;
		gameObject.SetActive(false);
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000F8884 File Offset: 0x000F6A84
	private void OnClickBtn(GameObject obj)
	{
		bool flag = obj == null;
		if (!flag)
		{
			DateTime serverDate = Fun.GetServerDate();
			Text component = obj.transform.FindChild("fbtype").GetComponent<Text>();
			int num = int.Parse(component.text);
			bool flag2 = ActivityManager.GetInstance().IsOpenLimit(num) == 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = !string.IsNullOrEmpty(this.sClickTime);
				if (flag3)
				{
					DateTime d = DateTime.Parse(this.sClickTime);
					bool flag4 = (serverDate - d).TotalSeconds < 1.0;
					if (flag4)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
						return;
					}
				}
				bool flag5 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
				if (flag5)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(17025, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag6 = MatchManager.GetInstance().m_nIsMatch > 0;
					if (flag6)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(17013, new object[0]), "ffdd00", "system");
					}
					else
					{
						this.sClickTime = serverDate.ToString();
						MatchManager.GetInstance().m_nIsMatch = 1;
						bool flag7 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
						if (flag7)
						{
							TeamManager.GetInstance().LeaveLookingTeam(3, num, 0, 0);
						}
						else
						{
							float num2 = Time.realtimeSinceStartup - this.nBtnTime;
							bool flag8 = num2 < 3f;
							if (flag8)
							{
								this.SendmatchLog();
							}
							this.nBtnTime = Time.realtimeSinceStartup;
							this.StartMatch_Go(num);
							GameObject gameObject = obj.transform.FindChild("SlcSp").gameObject;
							gameObject.SetActive(false);
							bool activeSelf = obj.transform.Find("arrow").gameObject.activeSelf;
							if (activeSelf)
							{
								this.FirstLoginTiShi.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(17016, new object[0]);
								this.HideAllFirstYinDao();
								this.hadshowyindao = true;
								bool flag9 = base.IsInvoking("YinDaoTimer");
								if (flag9)
								{
									base.CancelInvoke("YinDaoTimer");
								}
								base.Invoke("YinDaoTimer", 3f);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x000F8B6A File Offset: 0x000F6D6A
	private void OnExitTeam(object szParam)
	{
		this.continueMatchObj = (GameObject)szParam;
		base.CancelInvoke("ContinueMatch");
		TeamManager.GetInstance().OnLeaveTeam();
		base.Invoke("ContinueMatch", 0.5f);
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000F8BA1 File Offset: 0x000F6DA1
	private void ContinueMatch()
	{
		this.OnClickBtn(this.continueMatchObj);
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x000F8BB4 File Offset: 0x000F6DB4
	public void StartMatch_Go(int nFbType)
	{
		int num = 1;
		bool flag = ActivityManager.GetInstance().IsOpenLimit(nFbType) == 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17023, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = nFbType == 10;
			if (flag2)
			{
				num = 4;
			}
			fGameManager.SendMsgToHallServer(80001, new ArrayList
			{
				80002,
				num,
				Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName),
				nFbType
			});
		}
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x000F8C87 File Offset: 0x000F6E87
	private void SendmatchLog()
	{
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x000F8C8A File Offset: 0x000F6E8A
	private void ClearCD()
	{
		base.CancelInvoke("ClearCD");
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000F8C9C File Offset: 0x000F6E9C
	public void CloseYinDaoTiShi()
	{
		bool activeSelf = this.FirstLoginTiShi.activeSelf;
		if (activeSelf)
		{
			this.FirstLoginTiShi.SetActive(false);
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000F8CC8 File Offset: 0x000F6EC8
	private void YinDaoTimer()
	{
		this.FirstLoginTiShi.SetActive(false);
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
	public void SetLvShow()
	{
		ArrayList arrayList = new ArrayList
		{
			"-1",
			15,
			"2",
			7,
			"3",
			4,
			"4",
			15,
			"5",
			10
		};
		DateTime serverDate = Fun.GetServerDate();
		for (int i = 0; i < this.ALLModelObj.Count; i++)
		{
			bool flag = false;
			GameObject gameObject = (GameObject)this.ALLModelObj[i];
			Text component = gameObject.transform.FindChild("fbtype").GetComponent<Text>();
			Text component2 = gameObject.transform.FindChild("OpenTime").GetComponent<Text>();
			Text component3 = gameObject.transform.FindChild("matchnum").GetComponent<Text>();
			GameObject gameObject2 = gameObject.transform.FindChild("LockSp").gameObject;
			Text component4 = gameObject2.transform.FindChild("Text").GetComponent<Text>();
			Button component5 = gameObject.GetComponent<Button>();
			int num = arrayList.IndexOf(component.text);
			int num2 = (num != -1) ? ((int)arrayList[num + 1]) : 0;
			bool flag2 = ActivityManager.GetInstance().IsOpenLimit(int.Parse(component.text)) == 0;
			if (flag2)
			{
				DateTime d = DateTime.Parse(ActivityManager.GetInstance().GetFbOpenTime(int.Parse(component.text)));
				bool flag3 = (d - serverDate).TotalHours > 24.0;
				if (flag3)
				{
					component2.text = Language.GetLang(17022, new object[]
					{
						d.Month,
						d.Day
					});
				}
				else
				{
					component2.text = Language.GetLang(17021, new object[0]);
				}
				flag = true;
				component2.enabled = true;
				component3.enabled = false;
			}
			else
			{
				component2.enabled = false;
				component3.enabled = true;
			}
			bool flag4 = PlayerDataManager.getInstance().HUserData.m_nLevel < num2;
			if (flag4)
			{
				component4.text = string.Format(Language.GetLang(17017, new object[]
				{
					num2.ToString()
				}), new object[0]);
				flag = true;
			}
			else
			{
				component4.text = string.Empty;
			}
			bool flag5 = flag;
			if (flag5)
			{
				gameObject2.SetActive(true);
				component5.interactable = false;
			}
			else
			{
				gameObject2.SetActive(false);
				component5.interactable = true;
			}
			bool flag6 = fGameManager.IsNeedShowPiPeiYinDao && !gameObject.transform.FindChild("LockSp").gameObject.activeSelf && !this.hadshowyindao;
			if (flag6)
			{
				gameObject.transform.Find("arrow").gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000F901C File Offset: 0x000F721C
	public override void Reset()
	{
		base.Reset();
		bool flag = MainUICon.inst != null;
		if (flag)
		{
			MainUICon.inst.SetBackBtnData(true, null, null);
		}
		this.SetLvShow();
	}

	// Token: 0x040014AE RID: 5294
	public static MatchModelCon inst;

	// Token: 0x040014AF RID: 5295
	private GameObject Model1;

	// Token: 0x040014B0 RID: 5296
	private GameObject Model2;

	// Token: 0x040014B1 RID: 5297
	private GameObject Model3;

	// Token: 0x040014B2 RID: 5298
	private GameObject Model4;

	// Token: 0x040014B3 RID: 5299
	private GameObject Model5;

	// Token: 0x040014B4 RID: 5300
	private GameObject Model6;

	// Token: 0x040014B5 RID: 5301
	private GameObject Model7;

	// Token: 0x040014B6 RID: 5302
	private ArrayList ALLModelObj = new ArrayList();

	// Token: 0x040014B7 RID: 5303
	private Dictionary<int, GameObject> ModelObj = new Dictionary<int, GameObject>();

	// Token: 0x040014B8 RID: 5304
	private GameObject ScrollObj;

	// Token: 0x040014B9 RID: 5305
	private GameObject imgobj;

	// Token: 0x040014BA RID: 5306
	private GameObject lockobj;

	// Token: 0x040014BB RID: 5307
	private int nDisShow;

	// Token: 0x040014BC RID: 5308
	private int nLv;

	// Token: 0x040014BD RID: 5309
	private GameObject FirstLoginTiShi;

	// Token: 0x040014BE RID: 5310
	private bool hadshowyindao;

	// Token: 0x040014BF RID: 5311
	private float nBtnTime;

	// Token: 0x040014C0 RID: 5312
	private string sClickTime = string.Empty;

	// Token: 0x040014C1 RID: 5313
	private GameObject continueMatchObj;
}
