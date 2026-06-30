using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027A RID: 634
public class OneLookingteamdata : Window
{
	// Token: 0x06001D45 RID: 7493 RVA: 0x0010E777 File Offset: 0x0010C977
	private void Start()
	{
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x0010E77A File Offset: 0x0010C97A
	private void Update()
	{
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x0010E780 File Offset: 0x0010C980
	public void InitFindObj()
	{
		this.faceimg = base.transform.FindChild("faceimg").GetComponent<Image>();
		this.faceimgsky = base.transform.FindChild("faceimg_sky").GetComponent<RawImage>();
		this.sName = base.transform.FindChild("palyername").GetComponent<Text>();
		this.nLevel = base.transform.FindChild("LevelImg/Text").GetComponent<Text>();
		this.playerong = base.transform.FindChild("smallplayer1").GetComponent<Image>();
		this.playertwo = base.transform.FindChild("smallplayer2").GetComponent<Image>();
		this.playerthree = base.transform.FindChild("smallplayer3").GetComponent<Image>();
		this.playerfour = base.transform.FindChild("smallplayer4").GetComponent<Image>();
		this.joinbtn = base.transform.FindChild("Button").gameObject;
		EventTriggerListener.Get(this.joinbtn).onClick = new EventTriggerListener.VoidDelegate(this.OnJoin);
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x0010E8A0 File Offset: 0x0010CAA0
	public void InitData(ArrayList arr, int nType)
	{
		this.InitFindObj();
		this.m_nTeamID = (int)arr[0];
		this.m_nNum = (int)arr[3];
		this.m_nType = nType;
		this.SetFace((int)arr[4], (int)arr[5], nType);
		this.SetNum(this.m_nNum);
		this.SetName((string)arr[1], (int)arr[2]);
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0010E92C File Offset: 0x0010CB2C
	private void SetFace(int nSkyface, int nFaceID, int nType)
	{
		bool flag = nType == 2;
		if (flag)
		{
			this.faceimgsky.enabled = false;
			this.faceimg.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, "skyladdersmall_" + nSkyface.ToString());
		}
		else
		{
			this.faceimg.enabled = false;
			bool flag2 = fGameManager.mloadConfig.AllFaceData.ContainsKey(nFaceID);
			if (flag2)
			{
				PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nFaceID];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.faceimgsky);
			}
		}
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x0010E9D4 File Offset: 0x0010CBD4
	public void SetNum(int nNum)
	{
		this.m_nNum = nNum;
		this.playerong.enabled = false;
		this.playertwo.enabled = false;
		this.playerthree.enabled = false;
		this.playerfour.enabled = false;
		bool flag = this.m_nNum == 1;
		if (flag)
		{
			this.playerong.enabled = true;
		}
		bool flag2 = this.m_nNum == 2;
		if (flag2)
		{
			this.playerong.enabled = true;
			this.playertwo.enabled = true;
		}
		bool flag3 = this.m_nNum == 3;
		if (flag3)
		{
			this.playerong.enabled = true;
			this.playertwo.enabled = true;
			this.playerthree.enabled = true;
		}
		bool flag4 = this.m_nNum == 4;
		if (flag4)
		{
			this.playerong.enabled = true;
			this.playertwo.enabled = true;
			this.playerthree.enabled = true;
			this.playerfour.enabled = true;
		}
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0010EADB File Offset: 0x0010CCDB
	private void SetName(string sname, int Level)
	{
		this.sMastername = sname;
		this.sName.text = sname;
		this.nLevel.text = Level.ToString();
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0010EB08 File Offset: 0x0010CD08
	private void OnJoin(GameObject obj)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40052, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			bool flag2 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag2)
			{
				MatchManager.GetInstance().OnCancelMatch();
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40027, new object[0]), "ffdd00", "system");
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(60002);
			arrayList.Add(new ArrayList
			{
				Fun.EncodingUTF8(this.sMastername),
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				this.m_nTeamID
			});
			arrayList.Add(this.m_nType);
			bool flag3 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag3)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(6043, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnJoinLookingTeam), arrayList, null, null);
			}
			else
			{
				fGameManager.LobbyPlayer.SendMsgToHallServer(60000, arrayList);
			}
		}
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x0010ED30 File Offset: 0x0010CF30
	private void OnJoinLookingTeam(object szParam)
	{
		ArrayList szMsg = (ArrayList)szParam;
		bool flag = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag)
		{
			RoomManage.GetInstance().OnLeave();
		}
		fGameManager.LobbyPlayer.SendMsgToHallServer(60000, szMsg);
	}

	// Token: 0x040019DD RID: 6621
	private Image faceimg;

	// Token: 0x040019DE RID: 6622
	private RawImage faceimgsky;

	// Token: 0x040019DF RID: 6623
	private Text sName;

	// Token: 0x040019E0 RID: 6624
	private Text nLevel;

	// Token: 0x040019E1 RID: 6625
	private Image playerong;

	// Token: 0x040019E2 RID: 6626
	private Image playertwo;

	// Token: 0x040019E3 RID: 6627
	private Image playerthree;

	// Token: 0x040019E4 RID: 6628
	private Image playerfour;

	// Token: 0x040019E5 RID: 6629
	private GameObject joinbtn;

	// Token: 0x040019E6 RID: 6630
	private int m_nTeamID;

	// Token: 0x040019E7 RID: 6631
	private int m_nNum = 1;

	// Token: 0x040019E8 RID: 6632
	private string sMastername;

	// Token: 0x040019E9 RID: 6633
	private int m_nType;
}
