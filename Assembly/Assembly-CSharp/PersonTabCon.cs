using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028D RID: 653
public class PersonTabCon : Window
{
	// Token: 0x06001DC1 RID: 7617 RVA: 0x0011521C File Offset: 0x0011341C
	protected override void Awake()
	{
		base.Awake();
		PersonTabCon.Inst = this;
		this.mapNameText = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.titleTime = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.titleTimeShow = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.mapNameText.text = MatchManager.GetInstance().m_pFbData.sName;
		this.titleTime.text = Language.GetLang(1101, new object[0]) + " :";
		this.top_readme = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.top_readme.text = Language.GetLang(1102, new object[0]);
		this.top_20 = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.top_20.text = Language.GetLang(1103, new object[0]);
		this.title_xiaomie = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.title_xiaomie.text = Language.GetLang(1104, new object[0]);
		this.title_zhenwang = this.UIGameObj.Get_GameObj(5).GetComponent<Text>();
		this.title_zhenwang.text = Language.GetLang(1105, new object[0]);
		this.lists = this.UIGameObj.Get_GameObj(6);
		this.UIData = new List<PersonListData>();
		for (int i = 0; i < 10; i++)
		{
			PersonListData personListData = new PersonListData();
			string str = string.Format("one ({0})", i);
			personListData.backRed = this.lists.transform.Find(str + "/bg/bg_red").GetComponent<Image>();
			personListData.backBlue = this.lists.transform.Find(str + "/bg/bg_blue").GetComponent<Image>();
			personListData.backBlack = this.lists.transform.Find(str + "/bg/bg_black").GetComponent<Image>();
			personListData.backRed.enabled = true;
			personListData.backBlue.enabled = false;
			personListData.backBlack.enabled = false;
			personListData.info = this.lists.transform.Find(str + "/info");
			personListData.tMingci = this.lists.transform.Find(str + "/info/mingci").GetComponent<Text>();
			personListData.headImage = this.lists.transform.Find(str + "/info/head").GetComponent<RawImage>();
			personListData.headDeadImage = this.lists.transform.Find(str + "/info/head_dead").GetComponent<Image>();
			personListData.tName = this.lists.transform.Find(str + "/info/name").GetComponent<Text>();
			personListData.tLvl = this.lists.transform.Find(str + "/info/Text").GetComponent<Text>();
			personListData.tKill = this.lists.transform.Find(str + "/info/lab_kill").GetComponent<Text>();
			personListData.tDead = this.lists.transform.Find(str + "/info/lab_dead").GetComponent<Text>();
			personListData.tMingci.text = (i + 1).ToString();
			personListData.info.gameObject.SetActive(false);
			personListData.dazhao = this.lists.transform.Find(str + "/dazhao");
			personListData.dazhao.gameObject.SetActive(false);
			this.UIData.Add(personListData);
		}
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x00115608 File Offset: 0x00113808
	public void UpdateUI(List<NetLiving> nets)
	{
		this.titleTimeShow.text = Fun.sencondsToHMS_CXP(this.getNowGameTimeLong());
		bool flag = nets.Count == 0;
		if (!flag)
		{
			string text = string.Empty;
			for (int i = 0; i < nets.Count; i++)
			{
				NetLiving netLiving = nets[i];
				bool flag2 = netLiving != null;
				if (flag2)
				{
					float daZhaoValue = netLiving.playerDazhao.GetDaZhaoValue();
					bool flag3 = daZhaoValue >= 1f;
					text += string.Copy(string.Format("{0}-{1}-{2}-{3}-{4}-{5}", new object[]
					{
						netLiving.getNetid(),
						netLiving.playerCommon.aryWarData[0],
						netLiving.playerCommon.aryWarData[1],
						netLiving.isDead,
						netLiving.playerCommon.m_nModelIndex,
						flag3
					}));
				}
			}
			bool flag4 = text.Length == this.lastSort.Length && text.Contains(this.lastSort);
			if (!flag4)
			{
				this.lastSort = text;
				this.top_20.text = WarPersonManager.Instance.needKill.ToString();
				for (int j = 0; j < 10; j++)
				{
					bool flag5 = j >= nets.Count;
					if (flag5)
					{
						bool activeSelf = this.UIData[j].info.gameObject.activeSelf;
						if (activeSelf)
						{
							this.UIData[j].info.gameObject.SetActive(false);
						}
						this.UIData[j].backRed.enabled = true;
						this.UIData[j].backBlue.enabled = false;
						this.UIData[j].backBlack.enabled = false;
					}
					else
					{
						bool flag6 = !this.UIData[j].info.gameObject.activeSelf;
						if (flag6)
						{
							this.UIData[j].info.gameObject.SetActive(true);
						}
						bool flag7 = nets[j] == fGameManager.Heroplayer;
						if (flag7)
						{
							this.UIData[j].backRed.enabled = false;
							this.UIData[j].backBlue.enabled = true;
							this.UIData[j].backBlack.enabled = false;
							bool isDead = nets[j].isDead;
							if (isDead)
							{
								this.UIData[j].headDeadImage.enabled = true;
							}
							else
							{
								this.UIData[j].headDeadImage.enabled = false;
							}
						}
						else
						{
							this.UIData[j].backBlue.enabled = false;
							bool isDead2 = nets[j].isDead;
							if (isDead2)
							{
								this.UIData[j].backRed.enabled = false;
								this.UIData[j].backBlack.enabled = true;
								this.UIData[j].headDeadImage.enabled = true;
							}
							else
							{
								this.UIData[j].backRed.enabled = true;
								this.UIData[j].backBlack.enabled = false;
								this.UIData[j].headDeadImage.enabled = false;
							}
						}
						HeroData heroData = fGameManager.mloadConfig.AllHeroList[nets[j].playerCommon.m_nModelIndex];
						sysload.loadImage(this.UIData[j].headImage, "kaixiangico/" + heroData.hhead + ".txt", null);
						this.UIData[j].tName.text = nets[j].playerCommon.m_sName;
						this.UIData[j].tLvl.text = nets[j].playerCommon.m_nLevel.ToString();
						this.UIData[j].tKill.text = nets[j].playerCommon.aryWarData[0].ToString();
						this.UIData[j].tDead.text = nets[j].playerCommon.aryWarData[1].ToString();
						float daZhaoValue2 = nets[j].playerDazhao.GetDaZhaoValue();
						bool flag8 = daZhaoValue2 >= 1f;
						if (flag8)
						{
							bool flag9 = !this.UIData[j].dazhao.gameObject.activeSelf;
							if (flag9)
							{
								this.UIData[j].dazhao.gameObject.SetActive(true);
							}
						}
						else
						{
							bool activeSelf2 = this.UIData[j].dazhao.gameObject.activeSelf;
							if (activeSelf2)
							{
								this.UIData[j].dazhao.gameObject.SetActive(false);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x00115BB8 File Offset: 0x00113DB8
	private int getNowGameTimeLong()
	{
		DateTime dGameStartTime = MatchManager.GetInstance().m_dGameStartTime;
		return (int)(Fun.GetServerDate() - dGameStartTime).TotalSeconds;
	}

	// Token: 0x04001AD9 RID: 6873
	public static PersonTabCon Inst;

	// Token: 0x04001ADA RID: 6874
	private const int maxCount = 10;

	// Token: 0x04001ADB RID: 6875
	private List<NetLiving> nets = new List<NetLiving>();

	// Token: 0x04001ADC RID: 6876
	private Text mapNameText;

	// Token: 0x04001ADD RID: 6877
	private Text titleTime;

	// Token: 0x04001ADE RID: 6878
	private Text titleTimeShow;

	// Token: 0x04001ADF RID: 6879
	private Text top_readme;

	// Token: 0x04001AE0 RID: 6880
	private Text top_20;

	// Token: 0x04001AE1 RID: 6881
	private Text title_xiaomie;

	// Token: 0x04001AE2 RID: 6882
	private Text title_zhenwang;

	// Token: 0x04001AE3 RID: 6883
	private GameObject lists;

	// Token: 0x04001AE4 RID: 6884
	private List<PersonListData> UIData;

	// Token: 0x04001AE5 RID: 6885
	private string lastSort = string.Empty;
}
