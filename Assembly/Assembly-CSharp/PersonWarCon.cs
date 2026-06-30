using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028F RID: 655
public class PersonWarCon : Window
{
	// Token: 0x06001DC6 RID: 7622 RVA: 0x00115C13 File Offset: 0x00113E13
	public void SetWinder(string xx)
	{
		this.WinText.enabled = true;
		this.WinText.text = xx;
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x00115C30 File Offset: 0x00113E30
	protected override void Awake()
	{
		base.Awake();
		PersonWarCon.Inst = this;
		this.titleTimeShow = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.WinText = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.WinText.enabled = false;
		this.lists = this.UIGameObj.Get_GameObj(1);
		this.UIData = new List<PersonTopListData>();
		for (int i = 0; i < 2; i++)
		{
			PersonTopListData personTopListData = new PersonTopListData();
			string str = string.Format("one ({0})", i);
			personTopListData.backRed = this.lists.transform.Find(str + "/bg/bg_red").GetComponent<Image>();
			personTopListData.backBlue = this.lists.transform.Find(str + "/bg/bg_blue").GetComponent<Image>();
			personTopListData.backBlack = this.lists.transform.Find(str + "/bg/bg_black").GetComponent<Image>();
			personTopListData.backRed.enabled = false;
			personTopListData.backBlue.enabled = false;
			personTopListData.backBlack.enabled = false;
			personTopListData.info = this.lists.transform.Find(str + "/info");
			personTopListData.tMingci = this.lists.transform.Find(str + "/info/mingci").GetComponent<Text>();
			personTopListData.headImage = this.lists.transform.Find(str + "/info/head").GetComponent<RawImage>();
			personTopListData.headDeadImage = this.lists.transform.Find(str + "/info/head_dead").GetComponent<Image>();
			personTopListData.tName = this.lists.transform.Find(str + "/info/name").GetComponent<Text>();
			personTopListData.tKill = this.lists.transform.Find(str + "/info/lab_kill").GetComponent<Text>();
			personTopListData.tDead = this.lists.transform.Find(str + "/info/lab_dead").GetComponent<Text>();
			personTopListData.tMingci.text = (i + 1).ToString();
			personTopListData.info.gameObject.SetActive(false);
			personTopListData.headDeadImage.enabled = false;
			this.UIData.Add(personTopListData);
		}
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x00115EB8 File Offset: 0x001140B8
	public void UpdateUI(int mynum, List<NetLiving> nets)
	{
		int num = WarPersonManager.Instance.huiheTime - this.getNowGameTimeLong();
		bool flag = num < 0;
		if (flag)
		{
			num = 0;
		}
		this.titleTimeShow.text = Fun.sencondsToHMS_CXP(num);
		bool flag2 = nets.Count == 0;
		if (!flag2)
		{
			string text = string.Empty;
			for (int i = 0; i < nets.Count; i++)
			{
				NetLiving netLiving = nets[i];
				bool flag3 = netLiving != null && !netLiving.haveDestroy;
				if (flag3)
				{
					text += string.Copy(string.Format("{0}-{1}-{2}-{3}", new object[]
					{
						netLiving.getNetid(),
						netLiving.playerCommon.aryWarData[0],
						netLiving.playerCommon.aryWarData[1],
						netLiving.playerCommon.m_nModelIndex
					}));
				}
			}
			bool flag4 = text.Length == this.lastSort.Length && text.Contains(this.lastSort);
			if (!flag4)
			{
				this.lastSort = text;
				bool flag5 = nets.Count > 1;
				if (flag5)
				{
					bool flag6 = mynum == 0;
					if (flag6)
					{
						this.tops[0] = nets[0];
						this.UIData[0].tMingci.text = "1";
						this.tops[1] = nets[1];
						this.UIData[1].tMingci.text = "2";
					}
					else
					{
						this.tops[0] = nets[mynum - 1];
						this.UIData[0].tMingci.text = mynum.ToString();
						this.tops[1] = nets[mynum];
						this.UIData[1].tMingci.text = (mynum + 1).ToString();
					}
				}
				else
				{
					this.tops[1] = null;
					this.tops[0] = nets[mynum];
					this.UIData[0].tMingci.text = (mynum + 1).ToString();
				}
				bool flag7 = WarPersonManager.Instance.lastTopNo != (int)nets[0].playerCommon.aryWarData[0];
				if (flag7)
				{
					WarPersonManager.Instance.lastTopNo = (int)nets[0].playerCommon.aryWarData[0];
					WarPersonManager.Instance.No1KillNumber(nets[0], (int)nets[0].playerCommon.aryWarData[0]);
				}
				for (int i = 0; i < 2; i++)
				{
					bool flag8 = this.tops[i] == null;
					if (flag8)
					{
						bool activeSelf = this.UIData[i].info.gameObject.activeSelf;
						if (activeSelf)
						{
							this.UIData[i].info.gameObject.SetActive(false);
						}
						this.UIData[i].backRed.enabled = true;
						this.UIData[i].backBlue.enabled = false;
						this.UIData[i].backBlack.enabled = false;
					}
					else
					{
						bool flag9 = !this.UIData[i].info.gameObject.activeSelf;
						if (flag9)
						{
							this.UIData[i].info.gameObject.SetActive(true);
						}
						bool flag10 = this.tops[i] == fGameManager.Heroplayer;
						if (flag10)
						{
							this.UIData[i].backRed.enabled = false;
							this.UIData[i].backBlue.enabled = true;
							this.UIData[i].backBlack.enabled = false;
						}
						else
						{
							bool flag11 = i > 0 && i <= mynum;
							if (flag11)
							{
								bool activeSelf2 = this.UIData[i].info.gameObject.activeSelf;
								if (activeSelf2)
								{
									this.UIData[i].info.gameObject.SetActive(false);
								}
								goto IL_5DC;
							}
							this.UIData[i].backBlue.enabled = false;
							this.UIData[i].backRed.enabled = true;
							this.UIData[i].backBlack.enabled = false;
						}
						HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.tops[i].playerCommon.m_nModelIndex];
						sysload.loadImage(this.UIData[i].headImage, "kaixiangico/" + heroData.hhead + ".txt", null);
						this.UIData[i].tName.text = this.tops[i].playerCommon.m_sName;
						this.UIData[i].tKill.text = this.tops[i].playerCommon.aryWarData[0].ToString();
						this.UIData[i].tDead.text = this.tops[i].playerCommon.aryWarData[1].ToString();
					}
					IL_5DC:;
				}
			}
		}
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x001164B4 File Offset: 0x001146B4
	private int getNowGameTimeLong()
	{
		DateTime dGameStartTime = MatchManager.GetInstance().m_dGameStartTime;
		return (int)(Fun.GetServerDate() - dGameStartTime).TotalSeconds;
	}

	// Token: 0x04001AF0 RID: 6896
	private const int maxCount = 2;

	// Token: 0x04001AF1 RID: 6897
	public static PersonWarCon Inst;

	// Token: 0x04001AF2 RID: 6898
	private Text titleTimeShow;

	// Token: 0x04001AF3 RID: 6899
	private Text WinText;

	// Token: 0x04001AF4 RID: 6900
	private GameObject lists;

	// Token: 0x04001AF5 RID: 6901
	private List<PersonTopListData> UIData;

	// Token: 0x04001AF6 RID: 6902
	private string lastSort = string.Empty;

	// Token: 0x04001AF7 RID: 6903
	private List<NetLiving> tops = new List<NetLiving>
	{
		null,
		null
	};
}
