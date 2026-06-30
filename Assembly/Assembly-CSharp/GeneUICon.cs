using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000152 RID: 338
public class GeneUICon : Window
{
	// Token: 0x06000F71 RID: 3953 RVA: 0x0008D1C8 File Offset: 0x0008B3C8
	protected override void Awake()
	{
		base.Awake();
		this.ringBgSp = this.UIGameObj.Get_GameObj(1).GetComponent<Image>();
		this.nowEffectTxt = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.addAttrTxt = this.UIGameObj.Get_GameObj(3).GetComponent<Text>();
		this.starTxt = this.UIGameObj.Get_GameObj(4).GetComponent<Text>();
		this.szStar.Add(this.UIGameObj.Get_GameObj(5).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(6).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(7).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(8).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(9).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(10).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(11).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(12).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(13).GetComponent<Image>());
		this.szStar.Add(this.UIGameObj.Get_GameObj(14).GetComponent<Image>());
		this.rateTxt = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.costTxt = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.haveTxt = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.geneUpBtn = this.UIGameObj.Get_GameObj(18).GetComponent<Button>();
		this.geneUpTxt = this.geneUpBtn.transform.Find("Text").GetComponent<Text>();
		EventTriggerListener.Get(this.geneUpBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUpBtn);
		EventTriggerListener.Get(this.geneUpBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		GameObject gameObject = this.UIGameObj.Get_GameObj(33);
		gameObject.SetActive(false);
		this.autoBuyChip = this.UIGameObj.Get_GameObj(34).GetComponent<Toggle>();
		this.sucEffObj = this.UIGameObj.Get_GameObj(35);
		this.loseEffObj = this.UIGameObj.Get_GameObj(36);
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(20));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(21));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(22));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(23));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(24));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(25));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(26));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(27));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(28));
		this.szStarEff.Add(this.UIGameObj.Get_GameObj(29));
		gameObject = this.UIGameObj.Get_GameObj(30);
		for (int i = 1; i < 39; i++)
		{
			this.szMapOpen.Add(gameObject.transform.Find(i.ToString()).gameObject);
		}
		gameObject = this.UIGameObj.Get_GameObj(31);
		for (int j = 1; j < 39; j++)
		{
			this.szMapSuc.Add(gameObject.transform.Find(string.Format("{0}/jihuo", j)).gameObject);
			this.szMapSucBoom.Add(gameObject.transform.Find(string.Format("{0}/jihuo/cg", j)).gameObject);
			this.szMapLose.Add(gameObject.transform.Find(string.Format("{0}/sb", j)).gameObject);
		}
		gameObject = this.UIGameObj.Get_GameObj(32);
		for (int k = 1; k < 39; k++)
		{
			GameObject gameObject2 = gameObject.transform.Find(k.ToString()).gameObject;
			EventTriggerListener.Get(gameObject2).onEnter = new EventTriggerListener.VoidDelegate(this.OverMapStar);
			EventTriggerListener.Get(gameObject2).onExit = new EventTriggerListener.VoidDelegate(this.ExitMapStar);
		}
		GeneUICon.inst = this;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0008D6D7 File Offset: 0x0008B8D7
	private void Start()
	{
		this.InitUIData(0);
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x0008D6E4 File Offset: 0x0008B8E4
	public void Update()
	{
		bool flag = this.starEff > 0;
		if (flag)
		{
			bool flag2 = Time.time - this.starEffStart >= this.starEffDelay;
			if (flag2)
			{
				this.starEff = 0;
				this.CloseStarEff();
				this.SetAllStarSp();
			}
		}
		else
		{
			bool flag3 = this.isMapEff;
			if (flag3)
			{
				this.SetAllMapOpen(false);
				bool flag4 = !fUISoundManager.instance.SoundIsPlay("ui_gene_next");
				if (flag4)
				{
					fUISoundManager.instance.PlayUISound("ui_gene_next", 0, null, 20);
				}
				this.ringBgSp.material.mainTextureOffset += new Vector2(Time.deltaTime * this.moveSpeed, 0f);
				bool flag5 = this.ringBgSp.material.mainTextureOffset.x >= this.xMoveTo;
				if (flag5)
				{
					this.StopRingAnim();
				}
			}
		}
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0008D7DC File Offset: 0x0008B9DC
	public override void Reset()
	{
		base.Reset();
		TipManager.GetInstance().HideTextTip();
		this.starEff = 0;
		this.CloseStarEff();
		this.SetAllStarSp();
		this.StopRingAnim();
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0008D810 File Offset: 0x0008BA10
	private void StopRingAnim()
	{
		this.isMapEff = false;
		this.ringBgSp.material.mainTextureOffset = new Vector2(this.xMoveTo, 0f);
		fUISoundManager.instance.StopSound("ui_gene_next", 0);
		this.RefreshGeneData();
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0008D860 File Offset: 0x0008BA60
	public void InitUIData(int nType)
	{
		this.nNowStep = PveManager.GetInstance().StrengthenData.m_nNowStep;
		this.nNowStar = PveManager.GetInstance().StrengthenData.m_nNowStar;
		this.nNextStep = PveManager.GetInstance().StrengthenData.m_nNextStep;
		this.nNextStar = PveManager.GetInstance().StrengthenData.m_nNextStar;
		bool flag = fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNowStep);
		if (flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNowStep];
			bool flag2 = this.starEff == 1;
			if (flag2)
			{
				this.szStar[this.nNowStar - 1].sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, "dian1");
				this.szStarEff[this.nNowStar - 1].SetActive(true);
				this.szMapOpen[geneData.szStepMap[this.nNowStar - 1] - 1].SetActive(true);
				this.szMapSucBoom[geneData.szStepMap[this.nNowStar - 1] - 1].SetActive(true);
				this.szMapSuc[geneData.szStepMap[this.nNowStar - 1] - 1].SetActive(true);
			}
			bool flag3 = nType == 1 && this.nNowStar >= geneData.nMaxNum;
			if (flag3)
			{
				this.isMapEff = true;
			}
		}
		bool flag4 = this.xMoveTo == (float)this.nNextStep && this.isMapEff;
		if (flag4)
		{
			this.isMapEff = false;
		}
		this.xMoveTo = (float)this.nNextStep;
		bool flag5 = !this.isMapEff;
		if (flag5)
		{
			this.StopRingAnim();
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x0008DA30 File Offset: 0x0008BC30
	public void ShowUpdateEff(int nstep, int nstar, int nret, int ncost)
	{
		bool flag = nstep != this.nNextStep;
		if (!flag)
		{
			bool flag2 = nret > 0;
			if (flag2)
			{
				this.starEff = 1;
				this.starEffStart = Time.time;
				bool flag3 = WebParam.client_language == "zh";
				if (flag3)
				{
					this.sucEffObj.SetActive(true);
				}
				bool flag4 = ncost > 0;
				if (flag4)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(33025, new object[]
					{
						ncost
					}), "ffdd00", "system");
				}
				fUISoundManager.instance.PlayUISound("ui_gene_success", 0, null, 20);
			}
			else
			{
				bool flag5 = WebParam.client_language == "zh";
				if (flag5)
				{
					this.loseEffObj.SetActive(true);
				}
				bool flag6 = ncost > 0;
				if (flag6)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(33026, new object[]
					{
						ncost
					}), "ffdd00", "system");
				}
				bool flag7 = fGameManager.mloadConfig.AllGeneData.ContainsKey(nstep);
				if (flag7)
				{
					GeneData geneData = fGameManager.mloadConfig.AllGeneData[nstep];
					bool flag8 = nstar > 0 && nstar - 1 < geneData.szStepMap.Count;
					if (flag8)
					{
						this.starEff = 2;
						this.starEffStart = Time.time;
						this.szMapLose[geneData.szStepMap[nstar - 1] - 1].SetActive(true);
					}
				}
				fUISoundManager.instance.PlayUISound("ui_gene_fail", 0, null, 20);
			}
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0008DBDC File Offset: 0x0008BDDC
	private void RefreshGeneData()
	{
		GeneStarData geneStarData = null;
		bool flag = fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNextStep);
		if (flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNextStep];
			geneStarData = geneData.allStarData[this.nNextStar];
		}
		this.nowEffectTxt.text = Language.GetLang(33000, new object[]
		{
			PveManager.GetInstance().StrengthenData.GetGeneAddEffect(0, 0)
		});
		int[] geneAddAttr = PveManager.GetInstance().StrengthenData.GetGeneAddAttr(this.nNowStep, this.nNowStar);
		int[] geneAddAttr2 = PveManager.GetInstance().StrengthenData.GetGeneAddAttr(this.nNextStep, this.nNextStar);
		string[] attrText = this.GetAttrText(geneAddAttr, geneAddAttr2);
		bool flag2 = this.nNowStep == this.nNextStep && this.nNextStar == this.nNowStar;
		if (flag2)
		{
			this.addAttrTxt.text = attrText[1];
			this.rateTxt.text = string.Empty;
			this.costTxt.text = string.Empty;
			this.geneUpBtn.interactable = false;
			this.geneUpTxt.text = Language.GetLang(33031, new object[0]);
		}
		else
		{
			this.addAttrTxt.text = attrText[1];
			this.rateTxt.text = Language.GetLang(33004, new object[]
			{
				geneStarData.nRate
			});
			this.costTxt.text = Language.GetLang(33005, new object[]
			{
				geneStarData.szCost[1]
			});
			this.geneUpBtn.interactable = true;
			this.geneUpTxt.text = Language.GetLang(33006, new object[0]);
		}
		this.haveTxt.text = Language.GetLang(33007, new object[]
		{
			PlayerDataManager.getInstance().playerdet.m_nGeneChip
		});
		this.starTxt.text = Language.GetLang(33003, new object[]
		{
			(this.nNowStep * 10 + this.nNowStar) / 10
		});
		this.SetAllStarSp();
		this.SetAllMapOpen();
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0008DE38 File Offset: 0x0008C038
	private string[] GetAttrText(int[] szNow, int[] szNext)
	{
		string[] array = new string[2];
		for (int i = 0; i < 20; i++)
		{
			bool flag = szNow[i] > 0 || szNext[i] > 0;
			if (flag)
			{
				bool flag2 = !string.IsNullOrEmpty(array[0]);
				if (flag2)
				{
					string[] array2;
					(array2 = array)[0] = array2[0] + "\n";
				}
				bool flag3 = !string.IsNullOrEmpty(array[1]);
				if (flag3)
				{
					string[] array2;
					(array2 = array)[1] = array2[1] + "\n";
				}
				string[] array3;
				string text = (array3 = array)[0];
				array3[0] = string.Concat(new string[]
				{
					text,
					"<color=#00dcf9>",
					Fun.GetAttrName(i),
					"</color>",
					PveManager.GetInstance().OnShowValue(i, (float)szNow[i])
				});
				int num = szNext[i] - szNow[i];
				bool flag4 = num > 0;
				if (flag4)
				{
					string[] array4;
					text = (array4 = array)[1];
					array4[1] = string.Concat(new string[]
					{
						text,
						"<color=#00dcf9>",
						Fun.GetAttrName(i),
						"</color>",
						PveManager.GetInstance().OnShowValue(i, (float)szNow[i]),
						"<color=#0ce616> + ",
						PveManager.GetInstance().OnShowValue(i, (float)num),
						"</color>"
					});
				}
				else
				{
					string[] array5;
					text = (array5 = array)[1];
					array5[1] = string.Concat(new string[]
					{
						text,
						"<color=#00dcf9>",
						Fun.GetAttrName(i),
						"</color>",
						PveManager.GetInstance().OnShowValue(i, (float)szNow[i])
					});
				}
			}
		}
		return array;
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
	private void OnClickUpBtn(GameObject go)
	{
		bool flag = this.starEff != 0 || this.isMapEff || !fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNextStep);
		if (!flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNextStep];
			bool flag2 = !geneData.allStarData.ContainsKey(this.nNextStar);
			if (!flag2)
			{
				GeneStarData geneStarData = geneData.allStarData[this.nNextStar];
				bool flag3 = PlayerDataManager.getInstance().playerdet.m_nGeneChip < int.Parse(geneStarData.szCost[1]);
				if (flag3)
				{
					int num = Mathf.CeilToInt((float)(int.Parse(geneStarData.szCost[1]) - PlayerDataManager.getInstance().playerdet.m_nGeneChip) / 10f);
					bool flag4 = !this.autoBuyChip.isOn;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(33022, new object[]
						{
							num
						}), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), new TipManager.TipOKDelegate(this.OnClickYes), num, null, null, Language.GetLang(33023, new object[0]), false, new TipManager.TipToggleDelegate(this.OnTipToggleClick), 1, string.Empty, string.Empty, string.Empty);
						return;
					}
					bool flag5 = PlayerDataManager.getInstance().playerdet.chargeCurrency < num;
					if (flag5)
					{
						fUIManager.GetInstance().OpenChongZhiUI("GeneUICon");
						return;
					}
				}
				this.starEff = -1;
				PveManager.GetInstance().OnSendMsgToServer(19517, new ArrayList());
			}
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0008E1BE File Offset: 0x0008C3BE
	private void OnTipToggleClick(bool bstate)
	{
		this.autoBuyChip.isOn = bstate;
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
	private void OnClickYes(object szParam)
	{
		int num = (int)szParam;
		bool flag = num > PlayerDataManager.getInstance().playerdet.chargeCurrency;
		if (flag)
		{
			fUIManager.GetInstance().OpenChongZhiUI("GeneUICon");
		}
		else
		{
			this.starEff = -1;
			PveManager.GetInstance().OnSendMsgToServer(19517, new ArrayList());
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x0008E22A File Offset: 0x0008C42A
	private void onClickBuyChip(GameObject go)
	{
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0008E22D File Offset: 0x0008C42D
	private void OnClickYesBuy(object szParam, int type, int num)
	{
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0008E230 File Offset: 0x0008C430
	private void ExitMapStar(GameObject go)
	{
		QiangHuaUICon.inst.OnHideAttTip();
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0008E240 File Offset: 0x0008C440
	private void OverMapStar(GameObject go)
	{
		bool flag = !fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNextStep);
		if (!flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNextStep];
			int item = int.Parse(go.name);
			int num = geneData.szStepMap.IndexOf(item);
			bool flag2 = num != -1;
			if (flag2)
			{
				num++;
				bool flag3 = geneData.allStarData.ContainsKey(num);
				if (flag3)
				{
					GeneStarData geneStarData = geneData.allStarData[num];
					QiangHuaUICon.inst.OnShowAttTip(geneStarData.szTip[0], geneStarData.szTip[1], geneStarData.szTip[2]);
				}
			}
		}
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0008E2FC File Offset: 0x0008C4FC
	private void SetAllMapOpen()
	{
		bool flag = !fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNextStep);
		if (!flag)
		{
			GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNextStep];
			for (int i = 0; i < this.szMapOpen.Count; i++)
			{
				int num = geneData.szStepMap.IndexOf(i + 1);
				bool flag2 = num != -1;
				if (flag2)
				{
					this.szMapOpen[i].SetActive(true);
				}
				else
				{
					this.szMapOpen[i].SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0008E3A8 File Offset: 0x0008C5A8
	private void SetAllMapOpen(bool state)
	{
		for (int i = 0; i < this.szMapOpen.Count; i++)
		{
			bool activeSelf = this.szMapOpen[i].activeSelf;
			if (activeSelf)
			{
				this.szMapOpen[i].SetActive(state);
			}
		}
		for (int j = 0; j < this.szMapSuc.Count; j++)
		{
			bool activeSelf2 = this.szMapSuc[j].activeSelf;
			if (activeSelf2)
			{
				this.szMapSuc[j].SetActive(state);
			}
		}
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0008E448 File Offset: 0x0008C648
	private void SetAllStarSp()
	{
		bool flag = this.starEff > 0;
		if (!flag)
		{
			for (int i = 0; i < this.szStar.Count; i++)
			{
				bool flag2 = i < this.nNowStar && this.nNowStep == this.nNextStep;
				if (flag2)
				{
					this.szStar[i].sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, "dian1");
				}
				else
				{
					this.szStar[i].sprite = Window.spriteName(AtlasType.ATLAS_QIANGHUA, "dian");
				}
			}
			bool flag3 = !fGameManager.mloadConfig.AllGeneData.ContainsKey(this.nNextStep);
			if (!flag3)
			{
				GeneData geneData = fGameManager.mloadConfig.AllGeneData[this.nNextStep];
				for (int j = 0; j < this.szMapSuc.Count; j++)
				{
					int num = geneData.szStepMap.IndexOf(j + 1);
					bool flag4 = num != -1 && num < this.nNowStar && this.nNowStep == this.nNextStep;
					if (flag4)
					{
						this.szMapSuc[j].SetActive(true);
					}
					else
					{
						this.szMapSuc[j].SetActive(false);
					}
					bool activeSelf = this.szMapSucBoom[j].activeSelf;
					if (activeSelf)
					{
						this.szMapSucBoom[j].SetActive(false);
					}
					bool activeSelf2 = this.szMapLose[j].activeSelf;
					if (activeSelf2)
					{
						this.szMapLose[j].SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x0008E614 File Offset: 0x0008C814
	private void CloseStarEff()
	{
		for (int i = 0; i < this.szStarEff.Count; i++)
		{
			bool activeSelf = this.szStarEff[i].activeSelf;
			if (activeSelf)
			{
				this.szStarEff[i].SetActive(false);
			}
		}
		bool activeSelf2 = this.sucEffObj.activeSelf;
		if (activeSelf2)
		{
			this.sucEffObj.SetActive(false);
		}
		bool flag = this.loseEffObj.activeSelf || WebParam.client_language == "baxi";
		if (flag)
		{
			this.loseEffObj.SetActive(false);
			this.haveTxt.text = Language.GetLang(33007, new object[]
			{
				PlayerDataManager.getInstance().playerdet.m_nGeneChip
			});
		}
	}

	// Token: 0x04000BF8 RID: 3064
	public static GeneUICon inst;

	// Token: 0x04000BF9 RID: 3065
	private Image ringBgSp;

	// Token: 0x04000BFA RID: 3066
	private Text nowEffectTxt;

	// Token: 0x04000BFB RID: 3067
	private Text addAttrTxt;

	// Token: 0x04000BFC RID: 3068
	private Text starTxt;

	// Token: 0x04000BFD RID: 3069
	private List<Image> szStar = new List<Image>();

	// Token: 0x04000BFE RID: 3070
	private Text rateTxt;

	// Token: 0x04000BFF RID: 3071
	private Text costTxt;

	// Token: 0x04000C00 RID: 3072
	private Text haveTxt;

	// Token: 0x04000C01 RID: 3073
	private Button geneUpBtn;

	// Token: 0x04000C02 RID: 3074
	private Text geneUpTxt;

	// Token: 0x04000C03 RID: 3075
	private Toggle autoBuyChip;

	// Token: 0x04000C04 RID: 3076
	private GameObject sucEffObj;

	// Token: 0x04000C05 RID: 3077
	private GameObject loseEffObj;

	// Token: 0x04000C06 RID: 3078
	private List<GameObject> szStarEff = new List<GameObject>();

	// Token: 0x04000C07 RID: 3079
	private List<GameObject> szMapOpen = new List<GameObject>();

	// Token: 0x04000C08 RID: 3080
	private List<GameObject> szMapSuc = new List<GameObject>();

	// Token: 0x04000C09 RID: 3081
	private List<GameObject> szMapSucBoom = new List<GameObject>();

	// Token: 0x04000C0A RID: 3082
	private List<GameObject> szMapLose = new List<GameObject>();

	// Token: 0x04000C0B RID: 3083
	public float moveSpeed = 2f;

	// Token: 0x04000C0C RID: 3084
	private int nNowStep;

	// Token: 0x04000C0D RID: 3085
	private int nNowStar;

	// Token: 0x04000C0E RID: 3086
	private int nNextStep;

	// Token: 0x04000C0F RID: 3087
	private int nNextStar = 1;

	// Token: 0x04000C10 RID: 3088
	public int starEff;

	// Token: 0x04000C11 RID: 3089
	private bool isMapEff;

	// Token: 0x04000C12 RID: 3090
	private float xMoveTo;

	// Token: 0x04000C13 RID: 3091
	private float starEffStart;

	// Token: 0x04000C14 RID: 3092
	public float starEffDelay = 0.8f;
}
