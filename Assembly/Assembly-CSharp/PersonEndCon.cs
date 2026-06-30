using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000289 RID: 649
public class PersonEndCon : Window
{
	// Token: 0x06001DB9 RID: 7609 RVA: 0x001149B8 File Offset: 0x00112BB8
	protected override void Awake()
	{
		base.Awake();
		PersonEndCon.Inst = this;
		this.title_wanjia = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.title_wanjia.text = Language.GetLang(1106, new object[0]);
		this.title_xiaomie = this.UIGameObj.Get_GameObj(1).GetComponent<Text>();
		this.title_xiaomie.text = Language.GetLang(1104, new object[0]);
		this.title_zhenwang = this.UIGameObj.Get_GameObj(2).GetComponent<Text>();
		this.title_zhenwang.text = Language.GetLang(1105, new object[0]);
		this.title_shejiao = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.title_shejiao.text = Language.GetLang(1107, new object[0]);
		this.lists = this.UIGameObj.Get_GameObj(4);
		this.UIData = new List<PersonEndListData>();
		for (int i = 0; i < 10; i++)
		{
			PersonEndListData personEndListData = new PersonEndListData();
			string str = string.Format("one ({0})", i);
			personEndListData.backRed = this.lists.transform.Find(str + "/bg/bg_red").GetComponent<Image>();
			personEndListData.backBlue = this.lists.transform.Find(str + "/bg/bg_blue").GetComponent<Image>();
			personEndListData.backBlack = this.lists.transform.Find(str + "/bg/bg_black").GetComponent<Image>();
			personEndListData.backRed.enabled = true;
			personEndListData.backBlue.enabled = false;
			personEndListData.backBlack.enabled = false;
			personEndListData.info = this.lists.transform.Find(str + "/info");
			personEndListData.tMingci = this.lists.transform.Find(str + "/info/mingci").GetComponent<Text>();
			personEndListData.headImage = this.lists.transform.Find(str + "/info/head").GetComponent<RawImage>();
			personEndListData.headDeadImage = this.lists.transform.Find(str + "/info/head_dead").GetComponent<Image>();
			personEndListData.tName = this.lists.transform.Find(str + "/info/name").GetComponent<Text>();
			personEndListData.tLvl = this.lists.transform.Find(str + "/info/Text").GetComponent<Text>();
			personEndListData.tKill = this.lists.transform.Find(str + "/info/lab_kill").GetComponent<Text>();
			personEndListData.tDead = this.lists.transform.Find(str + "/info/lab_dead").GetComponent<Text>();
			personEndListData.shejiao = this.lists.transform.Find(str + "/info/shejiao/shejiaobut").gameObject;
			personEndListData.tMingci.text = (i + 1).ToString();
			personEndListData.info.gameObject.SetActive(false);
			personEndListData.headDeadImage.enabled = false;
			this.UIData.Add(personEndListData);
		}
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x00114D1C File Offset: 0x00112F1C
	private void OnAddFriend(GameObject obj)
	{
		bool flag = !(this.sNowName == string.Empty);
		if (flag)
		{
			FriendManager.GetInstance().AddFriend(this.sNowName);
			obj.SetActive(false);
		}
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x00114D5C File Offset: 0x00112F5C
	public void UpdateUI(List<NetLiving> nets)
	{
		for (int i = 0; i < 10; i++)
		{
			bool flag = i >= nets.Count;
			if (flag)
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
				bool flag2 = !this.UIData[i].info.gameObject.activeSelf;
				if (flag2)
				{
					this.UIData[i].info.gameObject.SetActive(true);
				}
				bool flag3 = nets[i] == fGameManager.Heroplayer;
				if (flag3)
				{
					this.UIData[i].backRed.enabled = false;
					this.UIData[i].backBlue.enabled = true;
					this.UIData[i].backBlack.enabled = false;
					this.UIData[i].shejiao.SetActive(false);
				}
				else
				{
					this.UIData[i].backBlue.enabled = false;
					this.UIData[i].backRed.enabled = true;
					this.UIData[i].backBlack.enabled = false;
				}
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[nets[i].playerCommon.m_nModelIndex];
				sysload.loadImage(this.UIData[i].headImage, "kaixiangico/" + heroData.hhead + ".txt", null);
				this.UIData[i].tName.text = nets[i].playerCommon.m_sName;
				this.UIData[i].tLvl.text = nets[i].playerCommon.m_nLevel.ToString();
				this.UIData[i].tKill.text = nets[i].playerCommon.aryWarData[0].ToString();
				this.UIData[i].tDead.text = nets[i].playerCommon.aryWarData[1].ToString();
				bool flag4 = FriendManager.GetInstance().IsFriendRlt(nets[i].playerCommon.m_sName) || nets[i].netobject_type == 6;
				if (flag4)
				{
					bool activeSelf2 = this.UIData[i].shejiao.activeSelf;
					if (activeSelf2)
					{
						this.UIData[i].shejiao.SetActive(false);
					}
				}
				else
				{
					this.sNowName = nets[i].playerCommon.m_sName;
					EventTriggerListener.Get(this.UIData[i].shejiao).onClick = new EventTriggerListener.VoidDelegate(this.OnAddFriend);
				}
			}
		}
	}

	// Token: 0x04001AB7 RID: 6839
	public static PersonEndCon Inst;

	// Token: 0x04001AB8 RID: 6840
	private const int maxCount = 10;

	// Token: 0x04001AB9 RID: 6841
	private List<NetLiving> nets = new List<NetLiving>();

	// Token: 0x04001ABA RID: 6842
	private Text title_wanjia;

	// Token: 0x04001ABB RID: 6843
	private Text title_xiaomie;

	// Token: 0x04001ABC RID: 6844
	private Text title_zhenwang;

	// Token: 0x04001ABD RID: 6845
	private Text title_shejiao;

	// Token: 0x04001ABE RID: 6846
	private GameObject lists;

	// Token: 0x04001ABF RID: 6847
	private List<PersonEndListData> UIData;

	// Token: 0x04001AC0 RID: 6848
	private string sNowName = string.Empty;
}
