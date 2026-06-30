using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000076 RID: 118
public class CDKRewardUICon : Window
{
	// Token: 0x060003C7 RID: 967 RVA: 0x0001E1DC File Offset: 0x0001C3DC
	protected override void Awake()
	{
		base.Awake();
		CDKRewardUICon.inst = this;
		this.RewardBtn = this.UIGameObj.Get_GameObj(0);
		EventTriggerListener.Get(this.RewardBtn).onClick = new EventTriggerListener.VoidDelegate(this.GetReward);
		this.CDK_InputField = this.UIGameObj.Get_GameObj(2).GetComponent<InputField>();
		this.CDK_InputField.placeholder.GetComponent<Text>().text = string.Empty;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001E257 File Offset: 0x0001C457
	public void ShowUIData()
	{
		this.CDK_InputField.placeholder.GetComponent<Text>().text = string.Empty;
		this.UpDataBtnState();
		this.CDK_InputField.text = string.Empty;
		this.ShowBg();
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001E294 File Offset: 0x0001C494
	private void ShowBg()
	{
		fUIManager.GetInstance().GetIconAsset("libao/CKD_Bg.txt", base.transform.GetComponent<RawImage>());
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
	private void GetReward(GameObject go)
	{
		bool flag = !go.GetComponent<Button>().interactable;
		if (!flag)
		{
			bool flag2 = !this.bSend;
			if (flag2)
			{
				ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(42034, new object[0]), "ffdd00", "system");
			}
			else
			{
				string text = this.CDK_InputField.text;
				bool flag3 = text.Length != 16;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(42035, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(6019, new object[0]), string.Empty, null, null, null, null);
				}
				else
				{
					this.bSend = false;
					text.Replace("\r\n", string.Empty).Replace("\n", string.Empty);
					fGameManager.SendMsgToHallServer(1316, new ArrayList
					{
						18002,
						new ArrayList
						{
							text,
							0
						}
					});
					base.Invoke("ClearSend", 5f);
				}
			}
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0001E400 File Offset: 0x0001C600
	public void ClearSend()
	{
		bool flag = base.IsInvoking("ClearSend");
		if (flag)
		{
			base.CancelInvoke("ClearSend");
		}
		this.bSend = true;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001E432 File Offset: 0x0001C632
	public void UpDataBtnState()
	{
	}

	// Token: 0x04000317 RID: 791
	public static CDKRewardUICon inst;

	// Token: 0x04000318 RID: 792
	private GameObject RewardBtn;

	// Token: 0x04000319 RID: 793
	private InputField CDK_InputField;

	// Token: 0x0400031A RID: 794
	private bool bSend = true;
}
