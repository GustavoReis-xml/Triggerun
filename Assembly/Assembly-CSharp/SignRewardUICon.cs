using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000314 RID: 788
public class SignRewardUICon : Window
{
	// Token: 0x06002637 RID: 9783 RVA: 0x0015C274 File Offset: 0x0015A474
	protected override void Awake()
	{
		base.Awake();
		SignRewardUICon.inst = this;
		this.OneReward = this.UIGameObj.Get_GameObj(0);
		this.RewardList = this.UIGameObj.Get_GameObj(1);
		this.DaySignReward = this.UIGameObj.Get_GameObj(6);
		this.SignButton = this.UIGameObj.Get_GameObj(7);
		this.RepairSignButton = this.UIGameObj.Get_GameObj(10);
		this.TitileMsgTxt = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.TitileMsgTxt.font = fUIManager.mDINMedium;
		}
		this.DateText = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.SignAllNum = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.AllSignData = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.RepairSignNumText = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.AttTime = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		EventTriggerListener.Get(this.SignButton).onClick = new EventTriggerListener.VoidDelegate(this.SignBtnClick);
		EventTriggerListener.Get(this.RepairSignButton).onClick = new EventTriggerListener.VoidDelegate(this.RepairSignBtnClick);
		int num = 0;
		foreach (object obj in this.UIGameObj.Get_GameObj(4).transform)
		{
			Transform transform = (Transform)obj;
			this.DayList.Add(num, transform.gameObject);
			num++;
		}
		this.DaySignReward.transform.FindChild("Reward").GetComponent<Text>().text = Language.GetLang(42016, new object[0]);
		this.DaySignReward.transform.FindChild("Msg").GetComponent<Text>().text = string.Empty;
		this.TitileMsgTxt.text = Language.GetLang(42018, new object[0]);
		this.Reset();
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x0015C4BC File Offset: 0x0015A6BC
	public override void Reset()
	{
		base.Reset();
		int month = Fun.GetServerDate().Month;
		bool flag = false;
		bool flag2 = this.OldMonth == 0;
		if (flag2)
		{
			this.OldMonth = month;
			flag = true;
		}
		else
		{
			bool flag3 = this.OldMonth != month;
			if (flag3)
			{
				flag = true;
				this.OldMonth = month;
			}
		}
		bool flag4 = flag;
		if (flag4)
		{
			this.ShowSignDayData();
			this.ShowSignReward();
		}
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x0015C530 File Offset: 0x0015A730
	public void ShowUIData()
	{
		this.Reset();
		DateTime serverDate = Fun.GetServerDate();
		bool flag = LiBaoManager.GetInstance().GetSignData(serverDate.Day) == 1;
		if (flag)
		{
			this.SignButton.GetComponent<Button>().interactable = false;
			this.SignButton.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42023, new object[0]);
		}
		else
		{
			this.SignButton.GetComponent<Button>().interactable = true;
			this.SignButton.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(42014, new object[0]);
		}
		this.DateText.text = Language.GetLang(42019, new object[]
		{
			serverDate.Year,
			serverDate.Month
		});
		this.SignAllNum.text = Language.GetLang(42015, new object[]
		{
			LiBaoManager.GetInstance().GetSignAllNum(),
			DateTime.DaysInMonth(serverDate.Year, serverDate.Month)
		});
		this.AllSignData.text = Language.GetLang(42020, new object[]
		{
			LiBaoManager.GetInstance().GetSignAllNum()
		});
		this.UpDataDayList();
		this.UpDataAllReward();
		this.ShowRepairSignData();
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x0015C6B4 File Offset: 0x0015A8B4
	private void ShowSignDayData()
	{
		this.NowMonthDayList.Clear();
		DateTime serverDate = Fun.GetServerDate();
		DateTime dateTime = new DateTime(serverDate.Year, serverDate.Month, 1);
		int num = DateTime.DaysInMonth(serverDate.Year, serverDate.Month);
		int dayOfWeek = (int)dateTime.DayOfWeek;
		int num2 = 1;
		for (int i = 0; i < this.DayList.Count; i++)
		{
			bool flag = i < dayOfWeek || i >= num + dayOfWeek;
			if (flag)
			{
				this.DayList[i].transform.FindChild("Image").gameObject.SetActive(false);
				this.DayList[i].transform.FindChild("Text").gameObject.SetActive(false);
			}
			else
			{
				bool flag2 = LiBaoManager.GetInstance().GetSignData(num2) == 1;
				if (flag2)
				{
					this.DayList[i].transform.FindChild("Image").gameObject.SetActive(true);
				}
				else
				{
					this.DayList[i].transform.FindChild("Image").gameObject.SetActive(false);
				}
				this.DayList[i].transform.FindChild("Text").gameObject.SetActive(true);
				this.DayList[i].transform.FindChild("Text").GetComponent<Text>().text = num2.ToString();
				this.NowMonthDayList.Add(num2, this.DayList[i]);
				num2++;
			}
		}
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x0015C884 File Offset: 0x0015AA84
	private void UpDataDayList()
	{
		foreach (int num in this.NowMonthDayList.Keys)
		{
			bool flag = LiBaoManager.GetInstance().GetSignData(num) == 1;
			if (flag)
			{
				this.NowMonthDayList[num].transform.FindChild("Image").gameObject.SetActive(true);
			}
			else
			{
				this.NowMonthDayList[num].transform.FindChild("Image").gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x0015C940 File Offset: 0x0015AB40
	private void ShowSignReward()
	{
		SignXMLData signXML_Data = LiBaoManager.GetInstance().GetSignXML_Data();
		bool flag = signXML_Data == null;
		if (!flag)
		{
			this.ClearAllReward();
			foreach (int num in signXML_Data.AllReward.Keys)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OneReward);
				gameObject.transform.SetParent(this.RewardList.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.FindChild("RewardTitle").GetComponent<Text>().text = signXML_Data.AllReward[num].name;
				gameObject.transform.FindChild("RewardMsg").GetComponent<Text>().text = signXML_Data.AllReward[num].msg;
				fUIManager.GetInstance().GetIconAsset("libao/userlv/" + signXML_Data.AllReward[num].img, gameObject.transform.FindChild("RewardImage").GetComponent<RawImage>());
				bool flag2 = !LiBaoManager.GetInstance().Get_SignAllRewardState(num) || !LiBaoManager.GetInstance().Get_OneSignRewardState();
				if (flag2)
				{
					gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
				}
				else
				{
					gameObject.transform.FindChild("RewardBtn/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
				gameObject.SetActive(true);
				GameObject gameObject2 = gameObject.transform.FindChild("RewardBtn").gameObject;
				gameObject2.name = "Btn_" + num.ToString();
				EventTriggerListener.Get(gameObject2).onClick = new EventTriggerListener.VoidDelegate(this.RewardBtnClick);
				this.AllRewardObj.Add(num, gameObject);
			}
		}
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x0015CB8C File Offset: 0x0015AD8C
	public void UpDataAllReward()
	{
		this.nGetNum = 0;
		foreach (int num in this.AllRewardObj.Keys)
		{
			bool flag = !LiBaoManager.GetInstance().Get_OneSignRewardState(num);
			if (flag)
			{
				this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString()).GetComponent<Button>().interactable = false;
				bool flag2 = LiBaoManager.GetInstance().Get_SignAllRewardState(num);
				if (flag2)
				{
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42011, new object[0]);
				}
				else
				{
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
			}
			else
			{
				bool flag3 = !LiBaoManager.GetInstance().Get_SignAllRewardState(num);
				if (flag3)
				{
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString()).GetComponent<Button>().interactable = false;
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42006, new object[0]);
				}
				else
				{
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString()).GetComponent<Button>().interactable = true;
					this.AllRewardObj[num].transform.FindChild("Btn_" + num.ToString() + "/Text").GetComponent<Text>().text = Language.GetLang(42005, new object[0]);
					this.nGetNum++;
				}
			}
		}
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x0015CE10 File Offset: 0x0015B010
	private void RewardBtnClick(GameObject go)
	{
		string[] array = go.name.Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[1]);
		bool interactable = go.GetComponent<Button>().interactable;
		if (interactable)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(num);
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15011,
				arrayList
			});
		}
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x0015CE90 File Offset: 0x0015B090
	private void SignBtnClick(GameObject go)
	{
		bool interactable = go.GetComponent<Button>().interactable;
		if (interactable)
		{
			fGameManager.SendMsgToHallServer(1315, new ArrayList
			{
				15010,
				new ArrayList()
			});
		}
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x0015CEDC File Offset: 0x0015B0DC
	private void ClearAllReward()
	{
		foreach (int key in this.AllRewardObj.Keys)
		{
			bool flag = this.AllRewardObj.ContainsKey(key);
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.AllRewardObj[key]);
			}
		}
		this.AllRewardObj.Clear();
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x0015CF64 File Offset: 0x0015B164
	public void ShowRepairSignData()
	{
		ArrayList repairSignData = LiBaoManager.GetInstance().GetRepairSignData();
		int num = ((int)repairSignData[0] - (int)repairSignData[1]) / 60;
		int num2 = 5 - (int)repairSignData[2];
		this.AttTime.text = Language.GetLang(46114, new object[]
		{
			num.ToString()
		});
		bool flag = num >= 60;
		if (flag)
		{
			this.AttTime.color = Fun.StrToColor("00FF00", 255f);
		}
		else
		{
			this.AttTime.color = Fun.StrToColor("FF0000", 255f);
		}
		this.RepairSignNumText.text = Language.GetLang(46115, new object[]
		{
			num2.ToString()
		});
		bool flag2 = num2 > 0 && this.IsRepairSign() == 1;
		if (flag2)
		{
			this.RepairSignButton.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.RepairSignButton.GetComponent<Button>().interactable = false;
		}
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x0015D080 File Offset: 0x0015B280
	private void RepairSignBtnClick(GameObject go)
	{
		bool interactable = go.GetComponent<Button>().interactable;
		if (interactable)
		{
			ArrayList repairSignData = LiBaoManager.GetInstance().GetRepairSignData();
			int num = ((int)repairSignData[0] - (int)repairSignData[1]) / 60;
			bool flag = num < 60;
			if (flag)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42075, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
			}
			else
			{
				fGameManager.SendMsgToHallServer(1315, new ArrayList
				{
					15037,
					new ArrayList()
				});
			}
		}
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x0015D158 File Offset: 0x0015B358
	private int IsRepairSign()
	{
		int day = Fun.GetServerDate().Day;
		int result = 0;
		for (int i = 1; i < day; i++)
		{
			bool flag = LiBaoManager.GetInstance().GetSignData(i) == 0;
			if (flag)
			{
				result = 1;
				break;
			}
		}
		return result;
	}

	// Token: 0x04002201 RID: 8705
	public static SignRewardUICon inst;

	// Token: 0x04002202 RID: 8706
	private int OldMonth;

	// Token: 0x04002203 RID: 8707
	private GameObject OneReward;

	// Token: 0x04002204 RID: 8708
	private GameObject RewardList;

	// Token: 0x04002205 RID: 8709
	private GameObject SignButton;

	// Token: 0x04002206 RID: 8710
	private GameObject DaySignReward;

	// Token: 0x04002207 RID: 8711
	private GameObject RepairSignButton;

	// Token: 0x04002208 RID: 8712
	private Text DateText;

	// Token: 0x04002209 RID: 8713
	private Text SignAllNum;

	// Token: 0x0400220A RID: 8714
	private Text TitileMsgTxt;

	// Token: 0x0400220B RID: 8715
	private Text AllSignData;

	// Token: 0x0400220C RID: 8716
	private Text RepairSignNumText;

	// Token: 0x0400220D RID: 8717
	private Text AttTime;

	// Token: 0x0400220E RID: 8718
	private Dictionary<int, GameObject> DayList = new Dictionary<int, GameObject>();

	// Token: 0x0400220F RID: 8719
	private Dictionary<int, GameObject> NowMonthDayList = new Dictionary<int, GameObject>();

	// Token: 0x04002210 RID: 8720
	private Dictionary<int, GameObject> AllRewardObj = new Dictionary<int, GameObject>();

	// Token: 0x04002211 RID: 8721
	public int nGetNum;
}
