using System;
using System.Collections;
using UnityEngine.UI;

// Token: 0x02000490 RID: 1168
public class UserVoiceModel : Window
{
	// Token: 0x06002D27 RID: 11559 RVA: 0x0018CD32 File Offset: 0x0018AF32
	protected override void Awake()
	{
		base.Awake();
		UserVoiceModel.inst = this;
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x0018CD42 File Offset: 0x0018AF42
	private void Start()
	{
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x0018CD45 File Offset: 0x0018AF45
	private void Update()
	{
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x0018CD48 File Offset: 0x0018AF48
	public void Init(string nCid)
	{
		this.m_nCid = int.Parse(nCid);
		this.rawimage = base.transform.FindChild("touxiang/RawImage").GetComponent<RawImage>();
		this.tName = base.transform.FindChild("mingzi").GetComponent<Text>();
		this.laba = base.transform.FindChild("laba").GetComponent<Image>();
		this.GetUserFaceID(this.m_nCid);
		this.SetImg();
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x0018CDC8 File Offset: 0x0018AFC8
	private void SetImg()
	{
		bool flag = fGameManager.m_nUserType == 0;
		if (flag)
		{
			bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(this.nFaceID);
			if (flag2)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[this.nFaceID];
				bool flag3 = playerFaceData != null;
				if (flag3)
				{
					fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.rawimage);
				}
			}
		}
		else
		{
			bool flag4 = fGameManager.mloadConfig.AllHeroList.ContainsKey(this.nFaceID);
			if (flag4)
			{
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[this.nFaceID];
				bool flag5 = heroData != null;
				if (flag5)
				{
					fUIManager.GetInstance().GetIconAsset("kaixiangico/" + heroData.hhead + ".txt", this.rawimage);
				}
			}
		}
		bool flag6 = this.m_nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag6)
		{
			this.tName.text = "<color='#00FF00'>" + this.sNick + "</color>";
		}
		else
		{
			this.tName.text = "<color='#00C3FF'>" + this.sNick + "</color>";
		}
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x0018CF14 File Offset: 0x0018B114
	public void SetMai()
	{
		this.laba.sprite = Window.spriteName(AtlasType.ATLAS_VOICEATLAS, "mai3");
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x0018CF32 File Offset: 0x0018B132
	public void SetPlay()
	{
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x0018CF38 File Offset: 0x0018B138
	public void LabaPlay()
	{
		bool flag = this.laba.sprite.name == "mai3";
		if (flag)
		{
			base.CancelInvoke("LabaPlay");
		}
		else
		{
			this.laba.sprite = Window.spriteName(AtlasType.ATLAS_VOICEATLAS, this.labanum.ToString());
			bool flag2 = this.labanum >= 4;
			if (flag2)
			{
				this.labanum = 0;
			}
			this.labanum++;
		}
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x0018CFBC File Offset: 0x0018B1BC
	public void GetUserFaceID(int nCid)
	{
		bool flag = fGameManager.m_nUserType == 0;
		if (flag)
		{
			for (int i = 1; i < TeamManager.GetInstance().MyTeamData.Count; i += 2)
			{
				bool flag2 = TeamManager.GetInstance().MyTeamData[i] is ArrayList;
				if (flag2)
				{
					ArrayList arrayList = (ArrayList)TeamManager.GetInstance().MyTeamData[i];
					bool flag3 = arrayList.Count > 10 && (int)arrayList[7] == nCid;
					if (flag3)
					{
						this.sNick = arrayList[0].ToString();
						this.nFaceID = (int)arrayList[10];
						break;
					}
				}
			}
		}
		else
		{
			ArrayList userList = MatchManager.GetInstance().GetUserList();
			for (int j = 1; j < userList.Count; j += 2)
			{
				bool flag4 = userList[j] is ArrayList;
				if (flag4)
				{
					ArrayList arrayList2 = (ArrayList)userList[j];
					bool flag5 = arrayList2.Count > 10 && (int)arrayList2[7] == nCid;
					if (flag5)
					{
						this.sNick = arrayList2[0].ToString();
						this.nFaceID = (int)arrayList2[6];
						break;
					}
				}
			}
		}
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x0018D129 File Offset: 0x0018B329
	public override void Reset()
	{
		base.Reset();
	}

	// Token: 0x0400264F RID: 9807
	public static UserVoiceModel inst;

	// Token: 0x04002650 RID: 9808
	private RawImage rawimage;

	// Token: 0x04002651 RID: 9809
	private Text tName;

	// Token: 0x04002652 RID: 9810
	private Image laba;

	// Token: 0x04002653 RID: 9811
	private int labanum = 1;

	// Token: 0x04002654 RID: 9812
	private int nFaceID = 1009;

	// Token: 0x04002655 RID: 9813
	private string sNick = string.Empty;

	// Token: 0x04002656 RID: 9814
	private int m_nCid;
}
