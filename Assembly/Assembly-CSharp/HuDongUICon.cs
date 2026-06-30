using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E6 RID: 486
public class HuDongUICon : Window
{
	// Token: 0x060015D2 RID: 5586 RVA: 0x000CC23C File Offset: 0x000CA43C
	protected override void Awake()
	{
		base.Awake();
		HuDongUICon.inst = this;
		this.pTanksBtn = this.UIGameObj.Get_GameObj(0);
		this.pShouDaoBtn = this.UIGameObj.Get_GameObj(1);
		this.pYuYinBtn = this.UIGameObj.Get_GameObj(2);
		this.pSkillBtn = this.UIGameObj.Get_GameObj(3);
		this.pBiaoQingBtn = this.UIGameObj.Get_GameObj(4);
		this.pNiHaoBtn = this.UIGameObj.Get_GameObj(5);
		this.pZhiLiaoBtn = this.UIGameObj.Get_GameObj(6);
		this.pJiHeBtn = this.UIGameObj.Get_GameObj(7);
		this.pPenQiBtn = this.UIGameObj.Get_GameObj(8);
		this.pTanksTxt = this.UIGameObj.Get_GameObj(9).GetComponent<Text>();
		this.pShouDaoTxt = this.UIGameObj.Get_GameObj(10).GetComponent<Text>();
		this.pYuYinTxt = this.UIGameObj.Get_GameObj(11).GetComponent<Text>();
		this.pSkillTxt = this.UIGameObj.Get_GameObj(12).GetComponent<Text>();
		this.pBiaoQingTxt = this.UIGameObj.Get_GameObj(13).GetComponent<Text>();
		this.pNiHaoTxt = this.UIGameObj.Get_GameObj(14).GetComponent<Text>();
		this.pZhiLiaoTxt = this.UIGameObj.Get_GameObj(15).GetComponent<Text>();
		this.pJiHeTxt = this.UIGameObj.Get_GameObj(16).GetComponent<Text>();
		this.pPenQiTxt = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.bBiaoQing = this.pBiaoQingBtn.GetComponent<Button>();
		EventTriggerListener.Get(this.pTanksBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pShouDaoBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pYuYinBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pSkillBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pBiaoQingBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pZhiLiaoBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pNiHaoBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pJiHeBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pPenQiBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMsgEnter);
		EventTriggerListener.Get(this.pTanksBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pShouDaoBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pYuYinBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pSkillBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pBiaoQingBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pZhiLiaoBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pNiHaoBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pJiHeBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pPenQiBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMsgExit);
		EventTriggerListener.Get(this.pTanksBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pShouDaoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pYuYinBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pSkillBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pBiaoQingBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pZhiLiaoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pNiHaoBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pJiHeBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
		EventTriggerListener.Get(this.pPenQiBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnMsgClick);
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000CC760 File Offset: 0x000CA960
	private void Update()
	{
		bool huDongUIOpen = ChatManager.GetInstance().HuDongUIOpen;
		if (huDongUIOpen)
		{
			this.UpdateBiaoQingState();
		}
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000CC788 File Offset: 0x000CA988
	private void Start()
	{
		bool flag = HuDongUICon.CdStartTime != 0f;
		if (flag)
		{
			bool flag2 = Time.time - HuDongUICon.CdStartTime < HuDongUICon.fHuDongCD;
			if (flag2)
			{
				HuDongUICon.fHuDongCD -= Time.time - HuDongUICon.CdStartTime;
				this.OnUICd();
			}
			else
			{
				HuDongUICon.CdStartTime = 0f;
				HuDongUICon.fHuDongCD = 0f;
				HuDongUICon.bUICd = false;
			}
		}
		this.OnShowHuDongKey();
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000CC805 File Offset: 0x000CAA05
	public override void Reset()
	{
		base.Reset();
		this.OnShowHuDongKey();
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000CC818 File Offset: 0x000CAA18
	private void OnShowHuDongKey()
	{
		int nModelIndex = fGameManager.GetInstance().getPlayer().playerCommon.m_nModelIndex;
		Setting_Entity_Control setting_Entity_Control = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(nModelIndex);
		this.pTanksTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Thank);
		this.pShouDaoTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Roger);
		this.pYuYinTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Custom_Voice_Sound);
		this.pSkillTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Ultimate_Skill_State);
		this.pBiaoQingTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Expression);
		this.pNiHaoTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Hello);
		this.pZhiLiaoTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Need_Help);
		this.pJiHeTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Interact_Collection);
		this.pPenQiTxt.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Spray_Paint);
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000CC97C File Offset: 0x000CAB7C
	private void UpdateBiaoQingState()
	{
		HeroPlayer heroplayer = fGameManager.Heroplayer;
		bool flag = heroplayer != null && !heroplayer.isDead && !heroplayer.haveDestroy && heroplayer.playerHaveCreate && !(this.bBiaoQing == null);
		if (flag)
		{
			bool flag2 = !heroplayer.wc.CanFirePublicCD(1f) || HuDongUICon.bUICd || !heroplayer.tChar.onGround || !heroplayer.bePlayActive || heroplayer.nHuDongState != 0;
			if (flag2)
			{
				this.bBiaoQing.interactable = false;
			}
			else
			{
				this.bBiaoQing.interactable = true;
			}
		}
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000CCA21 File Offset: 0x000CAC21
	public static void SetStartCdTime(float fCd)
	{
		HuDongUICon.CdStartTime = Time.time;
		HuDongUICon.fHuDongCD = fCd;
		HuDongUICon.bUICd = true;
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000CCA3A File Offset: 0x000CAC3A
	public void OnUICd()
	{
		this.UpdateBtnState(false);
		base.Invoke("UiCdFun", HuDongUICon.fHuDongCD);
		HuDongUICon.CdStartTime = 0f;
		HuDongUICon.fHuDongCD = 0f;
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x000CCA6A File Offset: 0x000CAC6A
	private void UiCdFun()
	{
		this.UpdateBtnState(true);
		HuDongUICon.bUICd = false;
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000CCA7C File Offset: 0x000CAC7C
	public void UpdateBtnState(bool State)
	{
		this.pTanksBtn.GetComponent<Button>().interactable = State;
		this.pShouDaoBtn.GetComponent<Button>().interactable = State;
		this.pYuYinBtn.GetComponent<Button>().interactable = State;
		this.pSkillBtn.GetComponent<Button>().interactable = State;
		this.pBiaoQingBtn.GetComponent<Button>().interactable = State;
		this.pNiHaoBtn.GetComponent<Button>().interactable = State;
		this.pZhiLiaoBtn.GetComponent<Button>().interactable = State;
		this.pJiHeBtn.GetComponent<Button>().interactable = State;
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x000CCB1A File Offset: 0x000CAD1A
	private void OnMsgExit(GameObject obj)
	{
		ChatManager.IsHuDongHover = false;
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x000CCB24 File Offset: 0x000CAD24
	private void OnMsgEnter(GameObject obj)
	{
		this.nSelectType = 0;
		bool flag = obj.GetComponent<Button>().IsInteractable();
		if (flag)
		{
			ChatManager.IsHuDongHover = true;
			this.nSelectType = this.GetType(obj.name);
		}
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000CCB64 File Offset: 0x000CAD64
	private void OnMsgClick(GameObject obj)
	{
		this.nSelectType = 0;
		bool flag = obj.GetComponent<Button>().IsInteractable();
		if (flag)
		{
			this.nSelectType = this.GetType(obj.name);
			ChatManager.GetInstance().OnSendMsg(this.nSelectType);
		}
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x000CCBB0 File Offset: 0x000CADB0
	private int GetType(string sName)
	{
		int result = 0;
		uint num = PrivateImplementationDetails.ComputeStringHash(sName);
		if (num <= 1172580663U)
		{
			if (num <= 132990436U)
			{
				if (num != 101563593U)
				{
					if (num == 132990436U)
					{
						if (sName == "nihaobtn")
						{
							result = 3;
						}
					}
				}
				else if (sName == "jihebtn")
				{
					result = 8;
				}
			}
			else if (num != 321360759U)
			{
				if (num == 1172580663U)
				{
					if (sName == "zhiliaobtn")
					{
						result = 5;
					}
				}
			}
			else if (sName == "biaoqingbtn")
			{
				result = 1;
			}
		}
		else if (num <= 2412441114U)
		{
			if (num != 2368724890U)
			{
				if (num == 2412441114U)
				{
					if (sName == "tanksbtn")
					{
						result = 6;
					}
				}
			}
			else if (sName == "penqibtn")
			{
				result = 9;
			}
		}
		else if (num != 2661567490U)
		{
			if (num != 3020973019U)
			{
				if (num == 3935698118U)
				{
					if (sName == "shoudaobtn")
					{
						result = 7;
					}
				}
			}
			else if (sName == "yuyinbtn")
			{
				result = 2;
			}
		}
		else if (sName == "skillbtn")
		{
			result = 4;
		}
		return result;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x000CCD11 File Offset: 0x000CAF11
	public void OnCloseUI()
	{
		this.Close();
		ChatManager.GetInstance().HuDongUIOpen = false;
		ChatManager.GetInstance().OnSendMsg(this.nSelectType);
		this.nSelectType = 0;
		ChatManager.IsHuDongHover = false;
	}

	// Token: 0x040010EB RID: 4331
	public static HuDongUICon inst;

	// Token: 0x040010EC RID: 4332
	private GameObject pTanksBtn;

	// Token: 0x040010ED RID: 4333
	private GameObject pShouDaoBtn;

	// Token: 0x040010EE RID: 4334
	private GameObject pYuYinBtn;

	// Token: 0x040010EF RID: 4335
	private GameObject pSkillBtn;

	// Token: 0x040010F0 RID: 4336
	private GameObject pBiaoQingBtn;

	// Token: 0x040010F1 RID: 4337
	private GameObject pNiHaoBtn;

	// Token: 0x040010F2 RID: 4338
	private GameObject pZhiLiaoBtn;

	// Token: 0x040010F3 RID: 4339
	private GameObject pJiHeBtn;

	// Token: 0x040010F4 RID: 4340
	private GameObject pPenQiBtn;

	// Token: 0x040010F5 RID: 4341
	private int nSelectType;

	// Token: 0x040010F6 RID: 4342
	public static float CdStartTime;

	// Token: 0x040010F7 RID: 4343
	public static float fHuDongCD;

	// Token: 0x040010F8 RID: 4344
	public static bool bUICd;

	// Token: 0x040010F9 RID: 4345
	private Text pTanksTxt;

	// Token: 0x040010FA RID: 4346
	private Text pShouDaoTxt;

	// Token: 0x040010FB RID: 4347
	private Text pYuYinTxt;

	// Token: 0x040010FC RID: 4348
	private Text pSkillTxt;

	// Token: 0x040010FD RID: 4349
	private Text pBiaoQingTxt;

	// Token: 0x040010FE RID: 4350
	private Text pNiHaoTxt;

	// Token: 0x040010FF RID: 4351
	private Text pZhiLiaoTxt;

	// Token: 0x04001100 RID: 4352
	private Text pJiHeTxt;

	// Token: 0x04001101 RID: 4353
	private Text pPenQiTxt;

	// Token: 0x04001102 RID: 4354
	private Button bBiaoQing;
}
