using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YouMe;

// Token: 0x02000491 RID: 1169
public class UserVoiceObj : MonoBehaviour
{
	// Token: 0x06002D32 RID: 11570 RVA: 0x0018D159 File Offset: 0x0018B359
	private void Start()
	{
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x0018D15C File Offset: 0x0018B35C
	private void Update()
	{
	}

	// Token: 0x06002D34 RID: 11572 RVA: 0x0018D160 File Offset: 0x0018B360
	public void Init(ArrayList szTeamData, int nIndex)
	{
		this.sNick = (string)szTeamData[nIndex];
		this.maiopenbtn = base.transform.FindChild("Button_mai_open").gameObject;
		this.maiclosebtn = base.transform.FindChild("Button_mai_close").gameObject;
		this.labaopenbtn = base.transform.FindChild("Button_laba_open").gameObject;
		this.labaclosebtn = base.transform.FindChild("Button_laba_close").gameObject;
		bool flag = szTeamData[nIndex].ToString() == PlayerDataManager.getInstance().HUserData.m_sName;
		if (flag)
		{
			this.maiclosebtn.SetActive(false);
			this.labaopenbtn.SetActive(false);
			this.labaclosebtn.SetActive(false);
			this.maiopenbtn.SetActive(true);
		}
		else
		{
			this.labaopenbtn.SetActive(true);
			this.maiopenbtn.SetActive(false);
			this.labaclosebtn.SetActive(false);
			this.maiclosebtn.SetActive(false);
		}
		this.labaopenbtn.name = this.sNick;
		this.maiopenbtn.name = this.sNick;
		this.labaclosebtn.name = this.sNick;
		this.maiclosebtn.name = this.sNick;
		EventTriggerListener.Get(this.labaopenbtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSetOtherVoice);
		EventTriggerListener.Get(this.maiopenbtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSetOtherVoice);
		EventTriggerListener.Get(this.labaclosebtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSetOtherVoice);
		EventTriggerListener.Get(this.maiclosebtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSetOtherVoice);
		int num = VoiceUICon.inst.UserVoiceState.IndexOf(this.sNick);
		bool flag2 = num != -1;
		if (flag2)
		{
			bool flag3 = this.sNick == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag3)
			{
				this.maiopenbtn.SetActive(false);
				this.maiclosebtn.SetActive(true);
			}
			else
			{
				this.labaopenbtn.SetActive(false);
				this.labaclosebtn.SetActive(true);
			}
		}
		Text component = base.transform.FindChild("Image_name/Text").GetComponent<Text>();
		component.text = szTeamData[nIndex].ToString();
		RawImage component2 = base.transform.Find("RawImage").GetComponent<RawImage>();
		bool flag4 = MatchManager.GetInstance().m_nGameVoice == 0;
		if (flag4)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[VoiceUICon.inst.GetUserFaceID(this.sNick)];
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component2);
		}
		else
		{
			string sIconName = fGameManager.mloadConfig.AllHeroUnlock[VoiceUICon.inst.GetUserFaceID(this.sNick)].szSkinUnlock[1].sIconName;
			bool flag5 = sIconName != string.Empty;
			if (flag5)
			{
				fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + sIconName + ".txt", component2);
			}
		}
		Image component3 = base.transform.Find("Image_suo").GetComponent<Image>();
		bool flag6 = nIndex + 1 >= szTeamData.Count;
		if (flag6)
		{
			component3.enabled = false;
		}
		else
		{
			this.sNextNick = (string)szTeamData[nIndex + 1];
			int userTeamID = VoiceUICon.inst.GetUserTeamID(this.sNick);
			int userTeamID2 = VoiceUICon.inst.GetUserTeamID(this.sNextNick);
			bool flag7 = userTeamID != 0 && userTeamID == userTeamID2;
			if (flag7)
			{
				component3.enabled = true;
			}
			else
			{
				component3.enabled = false;
			}
		}
	}

	// Token: 0x06002D35 RID: 11573 RVA: 0x0018D54C File Offset: 0x0018B74C
	public void OnSetBtnActive(bool bl)
	{
		if (bl)
		{
			bool flag = this.sNick == PlayerDataManager.getInstance().HUserData.m_sName;
			if (flag)
			{
				this.maiopenbtn.SetActive(bl);
			}
			else
			{
				this.labaopenbtn.SetActive(bl);
			}
		}
		else
		{
			this.maiopenbtn.SetActive(bl);
			this.maiclosebtn.SetActive(bl);
			this.labaopenbtn.SetActive(bl);
			this.labaclosebtn.SetActive(bl);
		}
	}

	// Token: 0x06002D36 RID: 11574 RVA: 0x0018D5D8 File Offset: 0x0018B7D8
	private void OnSetOtherVoice(GameObject obj)
	{
		bool flag = !VoiceGameCon.Inst.IsStartVoice;
		if (!flag)
		{
			int num = VoiceUICon.inst.UserVoiceState.IndexOf(obj.name);
			bool flag2 = num == -1;
			if (flag2)
			{
				bool flag3 = obj.name == PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag3)
				{
					bool bTalkModel = VoiceGameCon.Inst.bTalkModel;
					if (bTalkModel)
					{
						ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40037, new object[0]), "ffdd00", "system");
						return;
					}
					this.maiopenbtn.SetActive(false);
					this.maiclosebtn.SetActive(true);
					VoiceGameCon.MyMai = true;
					YouMeVoiceAPI.GetInstance().SetMicrophoneMute(true);
					bool flag4 = VoiceTalkCon.inst != null;
					if (flag4)
					{
						VoiceTalkCon.inst.SetTalk(PlayerDataManager.getInstance().HUserData.m_nCid.ToString(), 1);
					}
				}
				else
				{
					this.labaopenbtn.SetActive(false);
					this.labaclosebtn.SetActive(true);
					YouMeVoiceAPI.GetInstance().SetListenOtherVoice(VoiceUICon.inst.GetUserCID(obj.name).ToString(), false);
				}
				VoiceUICon.inst.UserVoiceState.Add(obj.name);
				VoiceUICon.inst.UserVoiceState.Add(1);
			}
			else
			{
				bool flag5 = obj.name == PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag5)
				{
					this.maiopenbtn.SetActive(true);
					this.maiclosebtn.SetActive(false);
					VoiceGameCon.MyMai = false;
					YouMeVoiceAPI.GetInstance().SetMicrophoneMute(false);
					bool flag6 = VoiceTalkCon.inst != null;
					if (flag6)
					{
						VoiceTalkCon.inst.SetTalk(PlayerDataManager.getInstance().HUserData.m_nCid.ToString(), 2);
					}
				}
				else
				{
					this.labaopenbtn.SetActive(true);
					this.labaclosebtn.SetActive(false);
					YouMeVoiceAPI.GetInstance().SetListenOtherVoice(VoiceUICon.inst.GetUserCID(obj.name).ToString(), true);
				}
				VoiceUICon.inst.UserVoiceState.RemoveAt(num + 1);
				VoiceUICon.inst.UserVoiceState.RemoveAt(num);
			}
		}
	}

	// Token: 0x04002657 RID: 9815
	public GameObject maiopenbtn;

	// Token: 0x04002658 RID: 9816
	public GameObject maiclosebtn;

	// Token: 0x04002659 RID: 9817
	public GameObject labaopenbtn;

	// Token: 0x0400265A RID: 9818
	public GameObject labaclosebtn;

	// Token: 0x0400265B RID: 9819
	public string sNick;

	// Token: 0x0400265C RID: 9820
	public string sNextNick;
}
