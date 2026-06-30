using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000090 RID: 144
public class ChongZhiUICon : Window
{
	// Token: 0x06000500 RID: 1280 RVA: 0x0002938C File Offset: 0x0002758C
	protected override void Awake()
	{
		base.Awake();
		ChongZhiUICon.inst = this;
		this.BgRewaImg = this.UIGameObj.Get_GameObj(0).GetComponent<RawImage>();
		this.ChongZhi_0 = this.UIGameObj.Get_GameObj(2);
		this.ChongZhi_1 = this.UIGameObj.Get_GameObj(3);
		this.ChongZhi_2 = this.UIGameObj.Get_GameObj(4);
		this.ChongZhi_3 = this.UIGameObj.Get_GameObj(5);
		this.ChongZhi_4 = this.UIGameObj.Get_GameObj(6);
		this.ChongZhi_0.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OpenChongZhi(0);
		});
		this.ChongZhi_1.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OpenChongZhi(1);
		});
		this.ChongZhi_2.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OpenChongZhi(2);
		});
		this.ChongZhi_3.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OpenChongZhi(3);
		});
		this.ChongZhi_4.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.OpenChongZhi(4);
		});
		this.CloseButton = this.UIGameObj.Get_GameObj(7);
		EventTriggerListener.Get(this.CloseButton).onClick = new EventTriggerListener.VoidDelegate(this.OnCloseClick);
		EventTriggerListener.Get(this.ChongZhi_0.transform.FindChild("Button").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ChongZhi_1.transform.FindChild("Button").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ChongZhi_2.transform.FindChild("Button").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ChongZhi_3.transform.FindChild("Button").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.ChongZhi_4.transform.FindChild("Button").gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00029628 File Offset: 0x00027828
	private void Start()
	{
		fUIManager.tipBlur.rawImage = this.BgRewaImg;
		fUIManager.tipBlur.enabled = true;
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.NumList = new ArrayList
			{
				200,
				550,
				1200,
				1875,
				2600
			};
		}
		else
		{
			this.NumList = new ArrayList
			{
				10,
				100,
				500,
				1000,
				5000
			};
		}
		this.ChongZhi_0.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(43001, new object[]
		{
			this.NumList[0].ToString()
		});
		this.ChongZhi_1.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(43001, new object[]
		{
			this.NumList[1].ToString()
		});
		this.ChongZhi_2.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(43001, new object[]
		{
			this.NumList[2].ToString()
		});
		this.ChongZhi_3.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(43001, new object[]
		{
			this.NumList[3].ToString()
		});
		this.ChongZhi_4.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(43001, new object[]
		{
			this.NumList[4].ToString()
		});
		this.Reset();
		this.ShowBg();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00029887 File Offset: 0x00027A87
	public override void Reset()
	{
		base.Reset();
		GameRoot.inst.Setting.Load_Setting_Data_State = false;
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000298A4 File Offset: 0x00027AA4
	private void ShowBg()
	{
		string str = string.Empty;
		str = ((!(Fun.GetQQ_Zuan_Type(null) == "hz")) ? "ChongZhi_ditu.txt" : "hz_ChongZhi_ditu.txt");
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			str = "ChongZhi_ditu_baxi.txt";
		}
		fUIManager.GetInstance().GetIconAsset("chognzhibg/" + str, this.UIGameObj.Get_GameObj(8).GetComponent<RawImage>());
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002991C File Offset: 0x00027B1C
	private void Update()
	{
		bool flag = !this.BgRewaImgState && this.BgRewaImg.texture != null;
		if (flag)
		{
			this.BgRewaImg.enabled = true;
			this.BgRewaImgState = true;
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00029960 File Offset: 0x00027B60
	private void OnCloseClick(GameObject go)
	{
		this.Close();
		GameRoot.inst.Setting.Load_Setting_Data_State = true;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0002997C File Offset: 0x00027B7C
	private void OpenChongZhi(int index)
	{
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Application.OpenURL(string.Concat(new string[]
			{
				"https://www.doubleaxion.com/portal/triggerun/api/recharge/?server_id=s9&game_login_uid=",
				WebParam.client_account,
				"&game_char_id=",
				WebParam.client_cid,
				"&buynum=",
				this.NumList[index].ToString()
			}));
		}
		else
		{
			fGameManager.OpenQQUrl("buygold", this.NumList[index].ToString(), fUIManager.GetInstance().BuyGoldName);
		}
	}

	// Token: 0x040003E1 RID: 993
	public static ChongZhiUICon inst;

	// Token: 0x040003E2 RID: 994
	private bool BgRewaImgState;

	// Token: 0x040003E3 RID: 995
	private RawImage BgRewaImg;

	// Token: 0x040003E4 RID: 996
	private GameObject ChongZhi_0;

	// Token: 0x040003E5 RID: 997
	private GameObject ChongZhi_1;

	// Token: 0x040003E6 RID: 998
	private GameObject ChongZhi_2;

	// Token: 0x040003E7 RID: 999
	private GameObject ChongZhi_3;

	// Token: 0x040003E8 RID: 1000
	private GameObject ChongZhi_4;

	// Token: 0x040003E9 RID: 1001
	private GameObject CloseButton;

	// Token: 0x040003EA RID: 1002
	private ArrayList NumList = new ArrayList
	{
		10,
		100,
		500,
		1000,
		5000
	};
}
