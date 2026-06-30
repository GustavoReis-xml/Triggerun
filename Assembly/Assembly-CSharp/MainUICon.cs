using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000230 RID: 560
public class MainUICon : Window
{
	// Token: 0x06001929 RID: 6441 RVA: 0x000EC00C File Offset: 0x000EA20C
	protected override void Awake()
	{
		base.Awake();
		GameObject gameObject = this.UIGameObj.Get_GameObj(64);
		EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OverFaceIcon);
		EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.ExitFaceIcon);
		this.myFaceIcon = this.UIGameObj.Get_GameObj(1).GetComponent<RawImage>();
		EventTriggerListener.Get(this.myFaceIcon.transform.parent.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.myFaceIcon.transform.parent.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnClickFace);
		this.myLzLvl = this.UIGameObj.Get_GameObj(51).GetComponent<Image>();
		this.myLzNian = this.UIGameObj.Get_GameObj(52).GetComponent<Image>();
		this.myNameTxt = this.UIGameObj.Get_GameObj(30).GetComponent<Text>();
		this.myLevelTxt = this.UIGameObj.Get_GameObj(29).GetComponent<Text>();
		this.changeNameBtn = this.UIGameObj.Get_GameObj(78);
		EventTriggerListener.Get(this.changeNameBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnChangeNameClick);
		EventTriggerListener.Get(this.changeNameBtn).onEnter = new EventTriggerListener.VoidDelegate(this.OverChangeName);
		EventTriggerListener.Get(this.changeNameBtn).onExit = new EventTriggerListener.VoidDelegate(this.ExitChangeName);
		this.soundBtn = this.UIGameObj.Get_GameObj(0).GetComponent<Button>();
		EventTriggerListener.Get(this.soundBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickSound);
		EventTriggerListener.Get(this.soundBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.otherTeamRender = this.UIGameObj.Get_GameObj(2);
		this.otherTeamRender.AddComponent<TeamCDCon>();
		this.otherTeamRender.name = "TeamRender0";
		Text component = this.otherTeamRender.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
		GameObject gameObject2 = this.otherTeamRender.transform.FindChild("Image").gameObject;
		gameObject2.SetActive(false);
		this.AllTeamObj.Add(this.otherTeamRender);
		EventTriggerListener.Get(this.otherTeamRender).onDown = new EventTriggerListener.VoidDelegate(this.OnAddTeamPlayer);
		EventTriggerListener.Get(this.otherTeamRender).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.TeamRenderCD1 = this.UIGameObj.Get_GameObj(42);
		this.TeamRenderCD1.AddComponent<TeamCDCon>();
		component = this.TeamRenderCD1.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
		gameObject2 = this.TeamRenderCD1.transform.FindChild("Image").gameObject;
		gameObject2.SetActive(false);
		this.TeamRenderCD1.name = "TeamRender1";
		this.AllTeamObj.Add(this.TeamRenderCD1);
		EventTriggerListener.Get(this.TeamRenderCD1).onDown = new EventTriggerListener.VoidDelegate(this.OnAddTeamPlayer);
		EventTriggerListener.Get(this.otherTeamRender).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.TeamRenderCD2 = this.UIGameObj.Get_GameObj(43);
		this.TeamRenderCD2.AddComponent<TeamCDCon>();
		component = this.TeamRenderCD2.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
		gameObject2 = this.TeamRenderCD2.transform.FindChild("Image").gameObject;
		gameObject2.SetActive(false);
		this.TeamRenderCD2.name = "TeamRender2";
		this.AllTeamObj.Add(this.TeamRenderCD2);
		EventTriggerListener.Get(this.TeamRenderCD2).onDown = new EventTriggerListener.VoidDelegate(this.OnAddTeamPlayer);
		EventTriggerListener.Get(this.otherTeamRender).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.TeamRenderCD3 = this.UIGameObj.Get_GameObj(44);
		this.TeamRenderCD3.AddComponent<TeamCDCon>();
		component = this.TeamRenderCD3.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
		gameObject2 = this.TeamRenderCD3.transform.FindChild("Image").gameObject;
		gameObject2.SetActive(false);
		this.TeamRenderCD3.name = "TeamRender3";
		this.AllTeamObj.Add(this.TeamRenderCD3);
		EventTriggerListener.Get(this.TeamRenderCD3).onDown = new EventTriggerListener.VoidDelegate(this.OnAddTeamPlayer);
		EventTriggerListener.Get(this.otherTeamRender).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleToggle = new List<Toggle>();
		this.szTitleIcon = new List<Image>();
		Toggle component2 = this.UIGameObj.Get_GameObj(3).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(4).GetComponent<Image>());
		component2 = this.UIGameObj.Get_GameObj(5).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		this.heroNewSp = component2.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(6).GetComponent<Image>());
		component2 = this.UIGameObj.Get_GameObj(7).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(8).GetComponent<Image>());
		component2 = this.UIGameObj.Get_GameObj(9).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(10).GetComponent<Image>());
		component2 = this.UIGameObj.Get_GameObj(11).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(12).GetComponent<Image>());
		component2 = this.UIGameObj.Get_GameObj(86).GetComponent<Toggle>();
		this.szTitleToggle.Add(component2);
		EventTriggerListener.Get(component2.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTitle);
		EventTriggerListener.Get(component2.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterTitle);
		EventTriggerListener.Get(component2.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitTitle);
		EventTriggerListener.Get(component2.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.szTitleIcon.Add(this.UIGameObj.Get_GameObj(87).GetComponent<Image>());
		this.lzRewardBtn = this.UIGameObj.Get_GameObj(47).GetComponent<Button>();
		this.lzRewardBtn.gameObject.SetActive(false);
		EventTriggerListener.Get(this.lzRewardBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLzBtn);
		EventTriggerListener.Get(this.lzRewardBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverLzBtn);
		EventTriggerListener.Get(this.lzRewardBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitLzBtn);
		EventTriggerListener.Get(this.lzRewardBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.lzParticle = this.UIGameObj.Get_GameObj(48);
		this.lzParticle.SetActive(false);
		this.qqRewardBtn = this.UIGameObj.Get_GameObj(53).GetComponent<Button>();
		this.qqRewardBtn.gameObject.SetActive(false);
		EventTriggerListener.Get(this.qqRewardBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickQQBtn);
		EventTriggerListener.Get(this.qqRewardBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverQQBtn);
		EventTriggerListener.Get(this.qqRewardBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitQQBtn);
		EventTriggerListener.Get(this.qqRewardBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.qqParticle = this.UIGameObj.Get_GameObj(54);
		this.qqParticle.SetActive(false);
		this.WenJuanBtn = this.UIGameObj.Get_GameObj(62);
		EventTriggerListener.Get(this.WenJuanBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickWenJuanBtn);
		EventTriggerListener.Get(this.WenJuanBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.WenJuanBtn.SetActive(false);
		this.HuoDongBtn = this.UIGameObj.Get_GameObj(63);
		EventTriggerListener.Get(this.HuoDongBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickLiBaoBtn);
		EventTriggerListener.Get(this.HuoDongBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.HuoDongBtn.SetActive(false);
		this.backBtn = this.UIGameObj.Get_GameObj(13);
		EventTriggerListener.Get(this.backBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBack);
		EventTriggerListener.Get(this.backBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.backBtn.SetActive(false);
		this.datingUI = this.UIGameObj.Get_GameObj(14);
		this.shangchengBtn = this.UIGameObj.Get_GameObj(15).GetComponent<Button>();
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Text component3 = this.shangchengBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component3.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.shangchengBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHomeFunc);
		EventTriggerListener.Get(this.shangchengBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.shangchengBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.shangchengBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		Image component4 = this.shangchengBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.shangchengBtn.gameObject.name, component4);
		this.shangchengLock = this.shangchengBtn.transform.Find("DisableSp").gameObject;
		this.shangchengLock.SetActive(false);
		this.pipeiBtn = this.UIGameObj.Get_GameObj(16).GetComponent<Button>();
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			Text component5 = this.pipeiBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component5.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.pipeiBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHomeFunc);
		EventTriggerListener.Get(this.pipeiBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.pipeiBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.pipeiBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		component4 = this.pipeiBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.pipeiBtn.gameObject.name, component4);
		this.pipeiLock = this.pipeiBtn.transform.Find("DisableSp").gameObject;
		this.pipeiLock.SetActive(false);
		this.datingBtn = this.UIGameObj.Get_GameObj(17).GetComponent<Button>();
		bool flag3 = WebParam.client_language == "baxi";
		if (flag3)
		{
			Text component6 = this.datingBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component6.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.datingBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHomeFunc);
		EventTriggerListener.Get(this.datingBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.datingBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.datingBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		component4 = this.datingBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.datingBtn.gameObject.name, component4);
		this.datingLock = this.datingBtn.transform.Find("DisableSp").gameObject;
		this.datingLock.SetActive(false);
		this.tiantiBtn = this.UIGameObj.Get_GameObj(18).GetComponent<Button>();
		bool flag4 = WebParam.client_language == "baxi";
		if (flag4)
		{
			Text component7 = this.tiantiBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component7.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.tiantiBtn.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.OnDownSkyLadder);
		EventTriggerListener.Get(this.tiantiBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.tiantiBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.tiantiBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		component4 = this.tiantiBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.tiantiBtn.gameObject.name, component4);
		this.tiantiLock = this.tiantiBtn.transform.Find("DisableSp").gameObject;
		this.tiantiDesc = this.UIGameObj.Get_GameObj(79);
		this.tiantiLock.SetActive(false);
		this.maoxianBtn = this.UIGameObj.Get_GameObj(85).GetComponent<Button>();
		bool flag5 = WebParam.client_language == "baxi";
		if (flag5)
		{
			Text component8 = this.maoxianBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component8.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.maoxianBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHomeFunc);
		EventTriggerListener.Get(this.maoxianBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.maoxianBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.maoxianBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		component4 = this.maoxianBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.maoxianBtn.gameObject.name, component4);
		this.maoxianLock = this.maoxianBtn.transform.Find("DisableSp").gameObject;
		this.maoxianLock.SetActive(false);
		this.dataoshaBtn = this.UIGameObj.Get_GameObj(56).GetComponent<Button>();
		bool flag6 = WebParam.client_language == "baxi";
		if (flag6)
		{
			Text component9 = this.dataoshaBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component9.font = fUIManager.mNewComic;
		}
		EventTriggerListener.Get(this.dataoshaBtn.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickHomeFunc);
		EventTriggerListener.Get(this.dataoshaBtn.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		EventTriggerListener.Get(this.dataoshaBtn.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverHomeFunc);
		EventTriggerListener.Get(this.dataoshaBtn.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitHomeFunc);
		component4 = this.dataoshaBtn.transform.Find("Check").GetComponent<Image>();
		component4.enabled = false;
		this.szFuncCheckImg.Add(this.dataoshaBtn.gameObject.name, component4);
		this.dataoshaLock = this.dataoshaBtn.transform.Find("DisableSp").gameObject;
		this.dataoshaLock.SetActive(false);
		this.dtsmsg = this.dataoshaBtn.transform.Find("msg").gameObject;
		this.dtsmsgText = this.dtsmsg.transform.Find("msgText").GetComponent<Text>();
		this.dtsmsg.SetActive(false);
		this.downLoadUI = this.UIGameObj.Get_GameObj(26).GetComponent<RectTransform>();
		gameObject = this.UIGameObj.Get_GameObj(19);
		this.downLoadDesc = gameObject.transform.Find("RunText").GetComponent<Text>();
		this.downLoadBar = this.UIGameObj.Get_GameObj(49).GetComponent<Image>();
		this.downLoadTip = this.UIGameObj.Get_GameObj(50).GetComponent<Text>();
		this.downLoadTip.enabled = false;
		this.downLoadHide = this.UIGameObj.Get_GameObj(21).GetComponent<Button>();
		EventTriggerListener.Get(this.downLoadHide.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDownLoad);
		EventTriggerListener.Get(this.downLoadHide.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.downLoadShow = this.UIGameObj.Get_GameObj(32).GetComponent<Button>();
		EventTriggerListener.Get(this.downLoadShow.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickDownLoad);
		EventTriggerListener.Get(this.downLoadShow.gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(22);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(GameRoot.inst.Setting.Open_Setting_Option);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(23);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickMsg);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(24);
		this.friendSign = gameObject.transform.Find("sing").GetComponent<Image>();
		this.friendSign.enabled = false;
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickFriend);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.friendNum = this.UIGameObj.Get_GameObj(25).GetComponent<Text>();
		this.friendNum.text = Language.GetLang(46119, new object[0]);
		gameObject = this.UIGameObj.Get_GameObj(31);
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickTask);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.SystemMsgTip = this.UIGameObj.Get_GameObj(27);
		this.SystemMsgTip.transform.FindChild("BaseTip").gameObject.AddComponent<ShowTipCon>();
		this.SystemMsgTip.SetActive(false);
		GameObject gameObject3 = this.UIGameObj.Get_GameObj(36);
		this.ZuanShiText = this.UIGameObj.Get_GameObj(33).GetComponent<Text>();
		this.MoneyText = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		Button button = this.ZuanShiText.gameObject.AddComponent<Button>();
		EventTriggerListener.Get(gameObject3.transform.FindChild("moneyImg").gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMoneyEnter);
		EventTriggerListener.Get(gameObject3.transform.FindChild("moneyImg").gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMoneyExit);
		EventTriggerListener.Get(button.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMoneyEnter);
		EventTriggerListener.Get(button.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMoneyExit);
		button = this.MoneyText.gameObject.AddComponent<Button>();
		EventTriggerListener.Get(button.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMoneyEnter);
		EventTriggerListener.Get(button.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMoneyExit);
		EventTriggerListener.Get(gameObject3.transform.FindChild("zuanshiImg").gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnMoneyEnter);
		EventTriggerListener.Get(gameObject3.transform.FindChild("zuanshiImg").gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnMoneyExit);
		this.ChongZhiBtn = this.UIGameObj.Get_GameObj(35);
		EventTriggerListener.Get(this.ChongZhiBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnOpenChongZhi);
		this.OnUpdateMoeny();
		this.FcmBtn = this.UIGameObj.Get_GameObj(37);
		EventTriggerListener.Get(this.FcmBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnFcmCertificate);
		this.FcmTime = this.UIGameObj.Get_GameObj(38).GetComponent<Text>();
		this.SetFcmShow();
		this.oneBuff = this.UIGameObj.Get_GameObj(40);
		this.OneBuffImg = this.oneBuff.transform.Find("BuffIcon").GetComponent<Image>();
		this.twoBuff = this.UIGameObj.Get_GameObj(41);
		this.TwoBuffImg = this.twoBuff.transform.Find("BuffIcon").GetComponent<Image>();
		this.pLiBaoNumObj = this.UIGameObj.Get_GameObj(45);
		this.isTeamMaster = this.UIGameObj.Get_GameObj(60);
		this.isTeamMaster.SetActive(false);
		this.pGuildNumObj = this.UIGameObj.Get_GameObj(61);
		this.pGuildNumObj.SetActive(false);
		this.PunUI = this.UIGameObj.Get_GameObj(65);
		this.maintext = this.UIGameObj.Get_GameObj(66).GetComponent<Text>();
		this.RedImg = this.UIGameObj.Get_GameObj(67);
		this.YellowImg = this.UIGameObj.Get_GameObj(68);
		this.infotext = this.UIGameObj.Get_GameObj(69).GetComponent<Text>();
		this.PunUI.SetActive(false);
		this.FirstYinDao = this.UIGameObj.Get_GameObj(72);
		this.ArrowObj = this.UIGameObj.Get_GameObj(73);
		this.pMailAin = this.UIGameObj.Get_GameObj(80);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(70)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickMail);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(71)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickUpMsg);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(71)).onEnter = new EventTriggerListener.VoidDelegate(this.OnOverUpMsg);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(71)).onExit = new EventTriggerListener.VoidDelegate(this.OnExitUpMsg);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(74)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickRankUI);
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(75)).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBbs);
		this.OtherBuff1 = this.UIGameObj.Get_GameObj(76);
		this.OtherBuff2 = this.UIGameObj.Get_GameObj(77);
		EventTriggerListener.Get(this.OtherBuff1).onEnter = new EventTriggerListener.VoidDelegate(this.OnOtherBuffOverUpMsg);
		EventTriggerListener.Get(this.OtherBuff1).onExit = new EventTriggerListener.VoidDelegate(this.OnOtherBuffExitUpMsg);
		EventTriggerListener.Get(this.OtherBuff2).onEnter = new EventTriggerListener.VoidDelegate(this.OnOtherBuffOverUpMsg);
		EventTriggerListener.Get(this.OtherBuff2).onExit = new EventTriggerListener.VoidDelegate(this.OnOtherBuffExitUpMsg);
		this.Carnival = this.UIGameObj.Get_GameObj(81);
		EventTriggerListener.Get(this.Carnival).onClick = new EventTriggerListener.VoidDelegate(this.Open_Carnival_Page);
		EventTriggerListener.Get(this.Carnival).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.JoinTestBtn = this.UIGameObj.Get_GameObj(82);
		EventTriggerListener.Get(this.JoinTestBtn).onClick = new EventTriggerListener.VoidDelegate(this.OpenJoinTestServerUI);
		EventTriggerListener.Get(this.JoinTestBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.JoinTestBtn.SetActive(false);
		this.skyLadderTip = this.UIGameObj.Get_GameObj(83);
		this.skyLadderTipTxt = this.UIGameObj.Get_GameObj(84).GetComponent<Text>();
		this.skyLadderTip.SetActive(false);
		this.OnShowOrHideChargeBtn();
		this.sevenTargBtn = this.UIGameObj.Get_GameObj(88);
		EventTriggerListener.Get(this.sevenTargBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnSevenTargBtn);
		EventTriggerListener.Get(this.sevenTargBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.teacherActBtn = this.UIGameObj.Get_GameObj(91);
		EventTriggerListener.Get(this.teacherActBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnTeacherActBtn);
		EventTriggerListener.Get(this.teacherActBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.luckyDrawBtn = this.UIGameObj.Get_GameObj(92);
		EventTriggerListener.Get(this.luckyDrawBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnLuckyDrawBtn);
		EventTriggerListener.Get(this.luckyDrawBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.heroDiscountBtn = this.UIGameObj.Get_GameObj(93);
		EventTriggerListener.Get(this.heroDiscountBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnHeroDiscountBtn);
		EventTriggerListener.Get(this.heroDiscountBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.nationalDayBtn = this.UIGameObj.Get_GameObj(94);
		this.nationalDayImg = this.nationalDayBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.nationalDayBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnNationalDayBtn);
		EventTriggerListener.Get(this.nationalDayBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.huiliuActBtn = this.UIGameObj.Get_GameObj(95);
		EventTriggerListener.Get(this.huiliuActBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnHuiliuBtn);
		EventTriggerListener.Get(this.huiliuActBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.wishTreeBtn = this.UIGameObj.Get_GameObj(96);
		this.wishTreeBtn.SetActive(Fun.IsFunctionOpen("WishTreeBtn", null));
		EventTriggerListener.Get(this.wishTreeBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnWishTreeBtn);
		EventTriggerListener.Get(this.wishTreeBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.jieriActBtn = this.UIGameObj.Get_GameObj(97);
		this.jieriActImg = this.jieriActBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.jieriActBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnJieRiActBtn);
		EventTriggerListener.Get(this.jieriActBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		gameObject = this.UIGameObj.Get_GameObj(89);
		gameObject.SetActive(Fun.IsFunctionOpen("LookingTeamBtn", null));
		EventTriggerListener.Get(this.UIGameObj.Get_GameObj(89)).onClick = new EventTriggerListener.VoidDelegate(this.OpenLookingTeamUI);
		this.pQiangHuaNumObj = this.UIGameObj.Get_GameObj(90);
		gameObject = this.UIGameObj.Get_GameObj(98);
		gameObject.SetActive(Fun.IsFunctionOpen("ExchangeCenterBtn", null));
		EventTriggerListener.Get(gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnExchgCenterBtn);
		EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.fuliActBtn = this.UIGameObj.Get_GameObj(99);
		this.fuliActImg = this.fuliActBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.fuliActBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnFuliActBtn);
		EventTriggerListener.Get(this.fuliActBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		this.winterActBtn = this.UIGameObj.Get_GameObj(100);
		this.winterActImg = this.winterActBtn.transform.Find("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.winterActBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnWinterActBtn);
		EventTriggerListener.Get(this.winterActBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		MainUICon.inst = this;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x000EE0A0 File Offset: 0x000EC2A0
	public void OnShowOrHideChargeBtn()
	{
		bool flag = Fun.GetIsCharge() == 1;
		if (flag)
		{
			this.ChongZhiBtn.SetActive(true);
		}
		else
		{
			this.ChongZhiBtn.SetActive(false);
		}
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x000EE0DC File Offset: 0x000EC2DC
	public void Set_Carnival_State()
	{
		bool flag = ActivityManager.GetInstance().GetOpenActIdByType(16) == 0;
		if (flag)
		{
			this.Carnival.SetActive(false);
		}
		else
		{
			this.Carnival.SetActive(true);
		}
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x000EE11D File Offset: 0x000EC31D
	private void ExitFaceIcon(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x000EE12C File Offset: 0x000EC32C
	private void OverFaceIcon(GameObject go)
	{
		bool flag = !this.isTeamTip;
		if (flag)
		{
			TipManager.GetInstance().ShowTextTip(Language.GetLang(5036, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x000EE174 File Offset: 0x000EC374
	public void SetTeamMaster(bool bl)
	{
		this.isTeamMaster.SetActive(bl);
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x000EE184 File Offset: 0x000EC384
	private void OnClickSound(GameObject go)
	{
		bool flag = GameRoot.inst.Setting.Voice_Class.Main_Volume == 0f;
		if (flag)
		{
			GameRoot.inst.Setting.Voice_Class.Main_Volume = 100f;
		}
		else
		{
			GameRoot.inst.Setting.Voice_Class.Main_Volume = 0f;
		}
		GameRoot.inst.Setting.Save_Data();
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x000EE1FC File Offset: 0x000EC3FC
	public void SetSoundBtn()
	{
		SpriteState spriteState = default(SpriteState);
		bool flag = GameRoot.inst.Setting.Voice_Class.Main_Volume == 0f;
		if (flag)
		{
			spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinguan1");
			spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinguan");
			this.soundBtn.spriteState = spriteState;
			this.soundBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinguan");
		}
		else
		{
			spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinkai1");
			spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinkai");
			this.soundBtn.spriteState = spriteState;
			this.soundBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "shengyinkai");
		}
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x000EE2F8 File Offset: 0x000EC4F8
	public void ShowLiBaoNum(int nNum)
	{
		bool flag = nNum > 0;
		if (flag)
		{
			this.pLiBaoNumObj.SetActive(true);
		}
		else
		{
			this.pLiBaoNumObj.SetActive(false);
		}
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x000EE330 File Offset: 0x000EC530
	public void OnShowQinagHuaTanHao(bool isShow)
	{
		bool flag = this.pQiangHuaNumObj.activeSelf != isShow;
		if (flag)
		{
			this.pQiangHuaNumObj.SetActive(isShow);
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x000EE364 File Offset: 0x000EC564
	public void OnShowGuildNum()
	{
		int nGuildRequstNum = GuildManager.GetInstance().m_nGuildRequstNum;
		int guildfuliReward = GuildManager.GetInstance().GetGuildfuliReward();
		bool flag = nGuildRequstNum > 0 || guildfuliReward > 0;
		if (flag)
		{
			this.pGuildNumObj.SetActive(true);
		}
		else
		{
			this.pGuildNumObj.SetActive(false);
		}
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x000EE3B8 File Offset: 0x000EC5B8
	public void SetFcmShow()
	{
		int nFcmOpen = HeroDataManager.GetInstance().nFcmOpen;
		int nIsFcmCheck = HeroDataManager.GetInstance().nIsFcmCheck;
		string sIDCard = HeroDataManager.GetInstance().sIDCard;
		bool flag = nFcmOpen == 0 || nIsFcmCheck == 0;
		if (flag)
		{
			this.FcmBtn.gameObject.SetActive(false);
			this.FcmTime.gameObject.SetActive(false);
		}
		else
		{
			bool flag2 = string.IsNullOrEmpty(sIDCard);
			if (flag2)
			{
				this.FcmBtn.gameObject.SetActive(true);
			}
			else
			{
				this.FcmBtn.gameObject.SetActive(false);
			}
			this.FcmTime.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000EE464 File Offset: 0x000EC664
	private void OnFcmCertificate(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("FCMUI", NetMsg.UITRANSFROM.PublicUI, null, null, true);
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x000EE47B File Offset: 0x000EC67B
	public void OnOpenChongZhi(GameObject obj)
	{
		fUIManager.GetInstance().OpenChongZhiUI("chongzhi");
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x000EE490 File Offset: 0x000EC690
	public void OnUpdateMoeny()
	{
		this.ZuanShiText.text = Fun.MoneyToShow(PlayerDataManager.getInstance().playerdet.chargeCurrency);
		this.MoneyText.text = Fun.MoneyToShow(PlayerDataManager.getInstance().playerdet.gameCurrency);
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x000EE4E0 File Offset: 0x000EC6E0
	private void OnMoneyEnter(GameObject obj)
	{
		string a = string.Empty;
		string sText = string.Empty;
		a = obj.name;
		sText = ((!(a == "zhuanshitxt") && !(a == "zuanshiImg")) ? Language.GetLang(5026, new object[]
		{
			PlayerDataManager.getInstance().playerdet.gameCurrency
		}) : Language.GetLang(5025, new object[]
		{
			PlayerDataManager.getInstance().playerdet.chargeCurrency
		}));
		TipManager.GetInstance().ShowTextTip(sText, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x000EE588 File Offset: 0x000EC788
	private void OnMoneyExit(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x000EE598 File Offset: 0x000EC798
	public void UpdownLoadSdPr(float nNowPro, float nMaxPro, float fpre, string url, bool bl)
	{
		bool flag = !this.IsMainUiShowPre && nNowPro > 20f && !this.IsTipShow && !bl;
		if (flag)
		{
			this.IsMainUiShowPre = true;
			bool flag2 = MainUICon.inst != null;
			if (flag2)
			{
				MainUICon.inst.SetPreShow(!bl);
			}
		}
		bool flag3 = nMaxPro == 1f && fpre == 0f;
		if (flag3)
		{
			this.downLoadDesc.text = Language.GetLang(5029, new object[0]);
			this.preLoadState = 2;
			this.HideDownloadTip();
		}
		else
		{
			this.downLoadDesc.text = Language.GetLang(5028, new object[0]);
			this.preLoadState = 1;
			bool flag4 = this.showDownloadTip && !this.isShowDownLoad;
			if (flag4)
			{
				base.Invoke("HideDownloadTip", 10f);
				this.showDownloadTip = false;
			}
		}
		this.downLoadBar.fillAmount = nNowPro / nMaxPro;
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x000EE6A0 File Offset: 0x000EC8A0
	public void SetPreShow(bool isshow)
	{
		bool flag = this.IsTipShow && isshow;
		if (!flag)
		{
			if (isshow)
			{
				bool flag2 = !this.isShowDownLoad;
				if (flag2)
				{
					this.OnClickDownLoad(null);
				}
			}
			else
			{
				bool flag3 = this.isShowDownLoad;
				if (flag3)
				{
					this.OnClickDownLoad(null);
				}
			}
		}
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x000EE6F1 File Offset: 0x000EC8F1
	private void HideDownloadTip()
	{
		this.downLoadTip.enabled = false;
		this.showDownloadTip = false;
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000EE708 File Offset: 0x000EC908
	private void Start()
	{
		this.ChangeTitleToIdx(0, null);
		this.SetPlayerInfo();
		this.CreateBuffIco();
		this.SetHeroNewState();
		this.ShowLiBaoNum(ShopManager.GetInstance().nLiBaoNum);
		this.SetLzAwardState();
		this.SetQQAwardState();
		this.SetSoundBtn();
		bool isRobot = SysConfig.IsRobot;
		if (isRobot)
		{
			base.StartCoroutine(this.wait());
		}
		this.OnShowGuildNum();
		this.WenJuanBtnActive();
		bool flag = LiBaoManager.GetInstance() != null;
		if (flag)
		{
			LiBaoManager.GetInstance().CheckLiBaoCanGet();
			LiBaoManager.GetInstance().CheckNationalActReward();
			LiBaoManager.GetInstance().CheckJieRiActReward();
			LiBaoManager.GetInstance().CheckFuliCanGet();
			LiBaoManager.GetInstance().OnLink_OpenLiBiaoUI();
		}
		this.CreateOtherBuffIco();
		this.OnMailAin(false);
		bool bLoginIn = MailManager.m_bLoginIn;
		if (bLoginIn)
		{
			MailManager.m_bLoginIn = false;
			bool flag2 = MailManager.m_nUnReadNum > 0;
			if (flag2)
			{
				this.OnMailAin(true);
			}
		}
		this.Set_Carnival_State();
		this.InItJoinTestButState();
		this.SetSevenTargBtn();
		this.OnShowQinagHuaTanHao(PveManager.GetInstance().IsShowTanHao);
		this.SetTeacherActBtn();
		this.SetLuckyDrawBtn();
		this.SetHeroDiscountBtn();
		this.OnShowDtsMsg();
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x000EE840 File Offset: 0x000ECA40
	private void OnShowDtsMsg()
	{
		DateTime d = DateTime.MinValue;
		string sDtsDayTime = PlayerDataManager.getInstance().HUserData.m_sDtsDayTime;
		bool flag = sDtsDayTime != string.Empty;
		if (flag)
		{
			d = DateTime.Parse(sDtsDayTime.ToString());
		}
		bool flag2 = d != DateTime.MinValue && d.Date != Fun.GetServerDate().Date;
		if (flag2)
		{
			this.dtsmsgText.text = Language.GetLang(29068, new object[]
			{
				PlayerDataManager.getInstance().HUserData.m_nDtsFristGold
			});
			bool flag3 = !this.dtsmsg.activeSelf;
			if (flag3)
			{
				this.dtsmsg.SetActive(true);
			}
		}
		else
		{
			bool activeSelf = this.dtsmsg.activeSelf;
			if (activeSelf)
			{
				this.dtsmsg.SetActive(false);
			}
		}
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000EE92C File Offset: 0x000ECB2C
	public void InitFirstYinDao()
	{
		bool flag = fGameManager.IsNeedShowPiPeiYinDao && !this.hadshowyindao;
		if (flag)
		{
			this.hadshowyindao = true;
			this.FirstYinDao.SetActive(true);
			Tweener t = this.ArrowObj.transform.DOLocalMoveY(-100f, 0.8f, false);
			t.SetEase(Ease.Linear);
			t.SetLoops(-1, LoopType.Yoyo);
		}
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000EE994 File Offset: 0x000ECB94
	private IEnumerator wait()
	{
		yield return new WaitForSeconds(2f);
		this.doPiPei();
		yield break;
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x000EE9A3 File Offset: 0x000ECBA3
	private void Update()
	{
		this.SetFcmTime();
		this.SetSkyLadderTime();
		this.SetSevenTargTime();
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x000EE9BC File Offset: 0x000ECBBC
	public override void Reset()
	{
		base.Reset();
		this.fLastClick = 0f;
		this.ChangeTitleToIdx(0, null);
		this.SetPlayerInfo();
		this.SetLzAwardState();
		this.SetQQAwardState();
		this.SetSoundBtn();
		this.WenJuanBtnActive();
		bool flag = LiBaoManager.GetInstance() != null;
		if (flag)
		{
			LiBaoManager.GetInstance().CheckLiBaoCanGet();
			LiBaoManager.GetInstance().CheckNationalActReward();
			LiBaoManager.GetInstance().CheckJieRiActReward();
			LiBaoManager.GetInstance().CheckFuliCanGet();
		}
		this.InitFirstYinDao();
		this.CreateOtherBuffIco();
		this.SetSevenTargBtn();
		this.SetTeacherActBtn();
		this.SetLuckyDrawBtn();
		this.SetHeroDiscountBtn();
		this.OnShowDtsMsg();
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x000EEA74 File Offset: 0x000ECC74
	public override void OnMauiOpen()
	{
		bool flag = VoiceTalkCon.inst != null;
		if (flag)
		{
			VoiceTalkCon.inst.Open();
		}
		bool flag2 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
		if (flag2)
		{
			VoiceUICon.inst.Close();
		}
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x000EEACC File Offset: 0x000ECCCC
	private void SetFcmTime()
	{
		int num = HeroDataManager.GetInstance().CountOnLineTime();
		bool flag = num >= 0;
		if (flag)
		{
			bool flag2 = num > 10800;
			if (flag2)
			{
				num = 10800;
			}
			bool flag3 = num != this.onlineCount;
			if (flag3)
			{
				this.onlineCount = num;
				this.FcmTime.text = Language.GetLang(5018, new object[]
				{
					Fun.SecToFullTimeString(num)
				});
			}
		}
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x000EEB44 File Offset: 0x000ECD44
	public void SetDatingBtn()
	{
		bool flag = Fun.m_nIsTopUp() == 1;
		if (flag)
		{
			Text component = this.datingBtn.transform.Find("FuncName/Text").GetComponent<Text>();
			component.text = Language.GetLang(5049, new object[0]);
			component = this.datingLock.transform.Find("Text").GetComponent<Text>();
			Image component2 = this.datingBtn.transform.Find("FuncName/FuncIcon").GetComponent<Image>();
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nLevel < 5;
			if (flag2)
			{
				component2.enabled = false;
				this.datingBtn.interactable = false;
				this.datingLock.SetActive(true);
				component.text = "5级解锁";
			}
			else
			{
				component2.enabled = true;
				component.text = Language.GetLang(5019, new object[0]);
				this.SetMatchState();
			}
		}
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x000EEC3C File Offset: 0x000ECE3C
	public void SetPunUI()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nPunNum == -1;
		if (!flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nPunNum < 45;
			if (flag2)
			{
				this.PunUI.SetActive(true);
				this.maintext.text = Language.GetLang(50001, new object[0]);
				this.maintext.enabled = true;
				int num = Mathf.CeilToInt((float)(45 - PlayerDataManager.getInstance().HUserData.m_nPunNum) / 3f);
				this.infotext.text = Language.GetLang(50002, new object[]
				{
					num
				});
				this.infotext.enabled = true;
				this.RedImg.SetActive(true);
				this.YellowImg.SetActive(false);
			}
			else
			{
				bool flag3 = PlayerDataManager.getInstance().HUserData.m_nPunNum >= 45 && PlayerDataManager.getInstance().HUserData.m_nPunNum <= 53;
				if (flag3)
				{
					bool flag4 = PlayerDataManager.getInstance().HUserData.m_nPunNumBak >= PlayerDataManager.getInstance().HUserData.m_nPunNum;
					if (flag4)
					{
						this.PunUI.SetActive(true);
						this.maintext.text = Language.GetLang(50000, new object[0]);
						this.maintext.enabled = true;
						this.infotext.enabled = false;
						this.RedImg.SetActive(false);
						this.YellowImg.SetActive(true);
					}
					else
					{
						this.PunUI.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x000EEDF8 File Offset: 0x000ECFF8
	public void SetMatchState()
	{
		int nIsMatch = MatchManager.GetInstance().m_nIsMatch;
		bool flag = nIsMatch > 0;
		if (flag)
		{
			this.shangchengBtn.interactable = false;
			this.shangchengLock.SetActive(true);
			this.datingBtn.interactable = false;
			this.datingLock.SetActive(true);
			this.pipeiBtn.interactable = false;
			this.pipeiLock.SetActive(true);
			this.tiantiBtn.interactable = false;
			this.tiantiLock.SetActive(true);
			this.dataoshaBtn.interactable = false;
			this.dataoshaLock.SetActive(true);
			this.OnSetMaoXianBtnState();
		}
		else
		{
			this.shangchengBtn.interactable = true;
			this.shangchengLock.SetActive(false);
			this.datingBtn.interactable = true;
			this.datingLock.SetActive(false);
			this.pipeiBtn.interactable = true;
			this.pipeiLock.SetActive(false);
			this.tiantiBtn.interactable = true;
			this.tiantiLock.SetActive(false);
			this.dataoshaBtn.interactable = true;
			this.dataoshaLock.SetActive(false);
			this.OnSetMaoXianBtnState();
		}
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x000EEF38 File Offset: 0x000ED138
	public void OnSetMaoXianBtnState()
	{
		PveXmlData pveData = PveManager.GetInstance().GetPveData();
		bool flag = pveData == null;
		if (!flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nLevel < PveManager.GetInstance().m_nOpenLv;
			bool flag3;
			if (flag2)
			{
				flag3 = false;
				this.maoxianLock.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(27046, new object[]
				{
					PveManager.GetInstance().m_nOpenLv
				});
			}
			else
			{
				flag3 = (MatchManager.GetInstance().m_nIsMatch <= 0);
				this.maoxianLock.transform.FindChild("Text").GetComponent<Text>().text = Language.GetLang(5019, new object[0]);
			}
			bool flag4 = WebParam.client_language != "baxi";
			if (flag4)
			{
				GameObject gameObject = this.maoxianBtn.transform.Find("Top").gameObject;
				ArrayList szPveFirstReward = PveManager.GetInstance().m_szPveFirstReward;
				bool flag5 = szPveFirstReward.Count > 0;
				if (flag5)
				{
					gameObject.SetActive(false);
				}
				else
				{
					gameObject.SetActive(true);
				}
			}
			this.maoxianBtn.interactable = flag3;
			this.maoxianLock.SetActive(!flag3);
		}
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x000EF090 File Offset: 0x000ED290
	public void SetLzAwardState()
	{
		bool flag = Prerogative_Data.Instance == null;
		if (!flag)
		{
			this.isLzAward = Prerogative_Data.Instance.Get_Reward_State();
			SpriteState spriteState = default(SpriteState);
			string qq_Zuan_Type = Fun.GetQQ_Zuan_Type(null);
			bool flag2 = this.isLzAward;
			if (flag2)
			{
				bool flag3 = qq_Zuan_Type == "hz";
				if (flag3)
				{
					spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz1_up");
					spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz1_down");
					this.lzRewardBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz1_normal");
					this.lzRewardBtn.spriteState = spriteState;
				}
				else
				{
					spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan1_up");
					spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan1_down");
					this.lzRewardBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan1_normal");
					this.lzRewardBtn.spriteState = spriteState;
				}
			}
			else
			{
				bool flag4 = qq_Zuan_Type == "hz";
				if (flag4)
				{
					spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz2_up");
					spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz2_down");
					this.lzRewardBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "hz2_normal");
					this.lzRewardBtn.spriteState = spriteState;
				}
				else
				{
					spriteState.highlightedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan_up");
					spriteState.pressedSprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan_down");
					this.lzRewardBtn.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "lanzuan_normal");
					this.lzRewardBtn.spriteState = spriteState;
				}
			}
			this.lzRewardBtn.gameObject.SetActive(Fun.IsFunctionOpen("lzRewardBtn", null));
			this.lzParticle.SetActive(this.isLzAward);
		}
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x000EF2C0 File Offset: 0x000ED4C0
	public void SetQQAwardState()
	{
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			this.qqRewardBtn.gameObject.SetActive(false);
		}
		else
		{
			this.qqRewardBtn.gameObject.SetActive(Fun.IsFunctionOpen("qqRewardBtn", null));
			bool flag2 = Prerogative_Data.Instance != null;
			if (flag2)
			{
				this.isQQAward = Prerogative_Data.Instance.Get_QQ_Games_Hall_Reward_State();
				this.qqParticle.SetActive(this.isQQAward);
			}
		}
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x000EF343 File Offset: 0x000ED543
	private void SetGameQuality()
	{
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x000EF348 File Offset: 0x000ED548
	public override void Close()
	{
		base.Close();
		TipManager.GetInstance().HideTextTip();
		this.OnCloseOpenWin();
		bool flag = VoiceTalkCon.inst != null;
		if (flag)
		{
			VoiceTalkCon.inst.Close();
		}
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x000EF38C File Offset: 0x000ED58C
	public void OpenSystemMsgTip(ArrayList szData)
	{
		OnePushMsgInfo onePushMsgInfo = new OnePushMsgInfo
		{
			m_nType = (int)szData[0],
			m_sMsg = szData[1].ToString(),
			szData = (ArrayList)szData[2],
			dCreateTime = Fun.GetServerDate().ToString()
		};
		bool activeSelf = this.SystemMsgTip.activeSelf;
		if (activeSelf)
		{
			this.m_szAllMsgList.Add(onePushMsgInfo);
		}
		else
		{
			this.AddHallPushMsg(onePushMsgInfo);
		}
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x000EF418 File Offset: 0x000ED618
	public void AddHallPushMsg(OnePushMsgInfo pData)
	{
		bool flag = pData.m_nType != 2 || TeamManager.GetInstance().IsShowMsg(pData) != 0;
		if (flag)
		{
			this.IsTipShow = true;
			this.IsMainUiShowPre = false;
			this.SetPreShow(false);
			this.SystemMsgTip.SetActive(true);
			bool flag2 = ShowTipCon.inst != null;
			if (flag2)
			{
				ShowTipCon.inst.InitTip(pData);
			}
		}
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x000EF488 File Offset: 0x000ED688
	public void OnHallChangeNextMsg()
	{
		OnePushMsgInfo onePushMsgInfo = null;
		while (this.m_szAllMsgList.Count > 0)
		{
			onePushMsgInfo = this.m_szAllMsgList[0];
			this.m_szAllMsgList.RemoveAt(0);
			bool flag = this.GetHallPushMsgTimeLong(onePushMsgInfo.dCreateTime) == -1;
			if (!flag)
			{
				break;
			}
			onePushMsgInfo = null;
		}
		bool flag2 = onePushMsgInfo == null;
		if (flag2)
		{
			this.ClosePushMsg();
		}
		else
		{
			this.AddHallPushMsg(onePushMsgInfo);
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x000EF500 File Offset: 0x000ED700
	private int GetHallPushMsgTimeLong(string sCreateTime)
	{
		int endCDTime = Fun.GetEndCDTime(sCreateTime, 20);
		bool flag = endCDTime < 1;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = endCDTime;
		}
		return result;
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x000EF52C File Offset: 0x000ED72C
	public void ClosePushMsg()
	{
		this.IsTipShow = false;
		this.SystemMsgTip.SetActive(false);
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x000EF544 File Offset: 0x000ED744
	public void ChangeTitleToIdx(int idx, object szParam = null)
	{
		bool flag = !this.visible;
		if (flag)
		{
			fUIManager.GetInstance().SetWinActive("MainUI", true);
		}
		bool flag2 = idx > -1 && idx < this.szTitleToggle.Count;
		if (flag2)
		{
			this.ChangeTitle(idx, szParam);
		}
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x000EF598 File Offset: 0x000ED798
	public void SetCurOpenUI(string sUI)
	{
		bool flag = !string.IsNullOrEmpty(this.curOpenUI) && sUI != this.curOpenUI;
		if (flag)
		{
			GameObject ui = fUIManager.GetInstance().GetUI(this.curOpenUI);
			bool flag2 = ui != null;
			if (flag2)
			{
				Window component = ui.GetComponent<Window>();
				bool visible = component.visible;
				if (visible)
				{
					fUIManager.GetInstance().SetWinActive(this.curOpenUI, false);
				}
			}
		}
		this.curOpenUI = sUI;
		bool flag3 = this.datingUI != null;
		if (flag3)
		{
			this.datingUI.SetActive(false);
		}
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x000EF635 File Offset: 0x000ED835
	public void OnGotoPveFbUI()
	{
		this.OnClickHomeFunc(this.maoxianBtn.gameObject);
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x000EF64C File Offset: 0x000ED84C
	private void OnClickHomeFunc(GameObject go)
	{
		bool flag = Time.time - this.fLastClick < 1f;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
		}
		else
		{
			this.fLastClick = Time.time;
			string name = go.name;
			string a = name;
			if (!(a == "Shangcheng"))
			{
				if (!(a == "Pipei"))
				{
					if (!(a == "Dating"))
					{
						if (!(a == "Tianti"))
						{
							if (!(a == "Maoxian"))
							{
								if (a == "Dataosha")
								{
									bool flag2 = Fun.IsFunctionOpen("MatchDts", null);
									if (flag2)
									{
										this.OnStartDtsGame();
									}
								}
							}
							else
							{
								bool flag3 = PveManager.GetInstance().IsPveOpen();
								if (flag3)
								{
									fUIManager.GetInstance().ShowWin("PveUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
								}
							}
						}
					}
					else
					{
						bool flag4 = VoiceUICon.inst != null && VoiceUICon.inst.visible;
						if (flag4)
						{
							VoiceUICon.inst.Close();
						}
						fUIManager.GetInstance().ShowWin("FindRoom", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
					}
				}
				else
				{
					bool flag5 = MatchManager.GetInstance().m_nIsMatch <= 0 && TeamManager.GetInstance().LoadTeamOK;
					if (flag5)
					{
						bool flag6 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
						if (flag6)
						{
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
						}
						else
						{
							bool flag7 = TeamManager.GetInstance().Dyaoqing.Count > 0;
							if (flag7)
							{
								ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
							}
							else
							{
								bool flag8 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
								if (flag8)
								{
									TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40024, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.QuickMatch), null, null, null);
								}
								else
								{
									MatchManager.GetInstance().OnGetMatchModelNum();
									this.FirstYinDao.SetActive(false);
								}
							}
						}
					}
				}
			}
			else
			{
				fUIManager.GetInstance().ShowWin("ChooseFb", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
			}
		}
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x000EF948 File Offset: 0x000EDB48
	private void OnStartDtsGame()
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch <= 0 && TeamManager.GetInstance().LoadTeamOK;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(17009, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
			}
			else
			{
				bool flag3 = TeamManager.GetInstance().Dyaoqing.Count > 0;
				if (flag3)
				{
					ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(40015, new object[0]), "ffdd00", "system");
				}
				else
				{
					bool flag4 = TeamManager.GetInstance().m_nTeamID != 0;
					if (flag4)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40024, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.OnDtsInvoke), null, null, null);
					}
					else
					{
						this.OnDtsMatch();
					}
				}
			}
		}
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x000EFA9C File Offset: 0x000EDC9C
	private void OnDtsInvoke(object opent)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 || TeamManager.GetInstance().m_nCaptain != 1;
		if (flag)
		{
			bool flag2 = fGameManager.LobbyPlayer == null;
			if (flag2)
			{
				return;
			}
			TeamManager.GetInstance().OnLeaveTeam();
		}
		base.CancelInvoke("OnDtsMatch");
		base.Invoke("OnDtsMatch", 0.5f);
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x000EFB08 File Offset: 0x000EDD08
	private void OnDtsMatch()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(80002);
		arrayList.Add(4);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		arrayList.Add(10);
		DtsBatteManager.GetInstance().isDtsMatching = 1;
		fGameManager.SendMsgToHallServer(80001, arrayList);
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x000EFB7B File Offset: 0x000EDD7B
	public void OpenMatchModelUI()
	{
		fUIManager.GetInstance().ShowWin("MatchModel", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x000EFBA0 File Offset: 0x000EDDA0
	private void QuickMatch(object szPram)
	{
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
		if (flag)
		{
			bool flag2 = fGameManager.LobbyPlayer == null;
			if (flag2)
			{
				return;
			}
			TeamManager.GetInstance().OnLeaveTeam();
		}
		base.Invoke("PiPei", 0.5f);
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x000EFBFE File Offset: 0x000EDDFE
	private void PiPei()
	{
		fUIManager.GetInstance().ShowWin("MatchModel", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000EFC20 File Offset: 0x000EDE20
	private void OnOverHomeFunc(GameObject go)
	{
		this.ShowHomeFuncCheck(go.name);
		string name = go.name;
		bool flag = name != null && name == "Tianti" && this.tiantiDesc.activeSelf;
		if (flag)
		{
			TipManager.GetInstance().ShowTextTip(Language.GetLang(5044, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x000EFC94 File Offset: 0x000EDE94
	private void OnExitHomeFunc(GameObject go)
	{
		this.ShowHomeFuncCheck(string.Empty);
		string name = go.name;
		bool flag = name != null && name == "Tianti";
		if (flag)
		{
			TipManager.GetInstance().HideTextTip();
		}
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000EFCD8 File Offset: 0x000EDED8
	private void ShowHomeFuncCheck(string sbtn)
	{
		foreach (string text in this.szFuncCheckImg.Keys)
		{
			bool flag = text == sbtn;
			if (flag)
			{
				this.szFuncCheckImg[text].enabled = true;
			}
			else
			{
				this.szFuncCheckImg[text].enabled = false;
			}
		}
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000EFD68 File Offset: 0x000EDF68
	private void OnEnterTraining(object szParam)
	{
		bool flag = TeamManager.GetInstance().LoadTeamOK && fGameManager.LobbyPlayer != null;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
			if (flag2)
			{
				RoomManage.GetInstance().OnLeave();
			}
			bool flag3 = MatchManager.GetInstance().m_nIsMatch > 0;
			if (flag3)
			{
				MatchManager.GetInstance().OnCancelMatch();
			}
			bool flag4 = TeamManager.GetInstance().m_nTeamID != 0;
			if (flag4)
			{
				TeamManager.GetInstance().OnLeaveTeam();
			}
			fGameManager.LobbyPlayer.SendMsgToHallServer(5030, new ArrayList());
		}
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000EFE08 File Offset: 0x000EE008
	private void doPiPei()
	{
		fGameManager.SendMsgToHallServer(80001, new ArrayList
		{
			80002,
			1,
			Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName)
		});
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x000EFE61 File Offset: 0x000EE061
	private void OnTestBack(object param)
	{
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000EFE64 File Offset: 0x000EE064
	public void SetBackBtnData(bool isshow, MainUICon.BackBtnDelegate func = null, object param = null)
	{
		bool flag = this.backBtn != null;
		if (flag)
		{
			this.backBtn.SetActive(isshow);
			this.backBtnFunc = func;
			this.backBtnParam = param;
		}
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000EFEA0 File Offset: 0x000EE0A0
	public void OnClickBack(GameObject go)
	{
		bool flag = this.backBtnFunc != null;
		if (flag)
		{
			this.backBtnFunc(this.backBtnParam);
		}
		else
		{
			this.ChangeTitleToIdx(this.curTitleIdx, null);
		}
		this.ShowHomeFuncCheck(string.Empty);
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x000EFEED File Offset: 0x000EE0ED
	private void OnClickLzBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("Prerogative", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000EFF10 File Offset: 0x000EE110
	private void OnOverLzBtn(GameObject go)
	{
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		if (flag)
		{
			TipManager.GetInstance().ShowTextTip(Language.GetLang(5041, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
		else
		{
			TipManager.GetInstance().ShowTextTip(Language.GetLang(5023, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x000EFF8E File Offset: 0x000EE18E
	private void OnExitLzBtn(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x000EFF9C File Offset: 0x000EE19C
	private void OnClickQQBtn(GameObject go)
	{
		bool flag = Prerogative_Data.Instance != null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("QQ_Games_Hall_Prerogative", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x000EFFD7 File Offset: 0x000EE1D7
	private void OnOverQQBtn(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(5032, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x000F0005 File Offset: 0x000EE205
	private void OnExitQQBtn(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x000F0013 File Offset: 0x000EE213
	private void OnClickMsg(GameObject go)
	{
		fGameManager.OpenQQUrl("kfopen", string.Empty, "Prerogative");
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x000F002C File Offset: 0x000EE22C
	private void OnClickBbs(GameObject go)
	{
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			Application.OpenURL("http://www.triggerun.com/");
		}
		else
		{
			Application.OpenURL("http://qqgame.qq.com/gameinfo/10677.html");
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x000F0069 File Offset: 0x000EE269
	private void OnClickMail(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("MailUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		this.OnMailAin(false);
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x000F0093 File Offset: 0x000EE293
	public void OnMailAin(bool isShow)
	{
		this.pMailAin.SetActive(isShow);
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x000F00A3 File Offset: 0x000EE2A3
	private void OnClickRankUI(GameObject go)
	{
		fUIManager.GetInstance().SetWinActive("MainUI", false);
		RankManager.GetInstance().RankUiTabIndex = 0;
		fUIManager.GetInstance().ShowWin("RankUI", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x000F00D7 File Offset: 0x000EE2D7
	private void OnOverUpMsg(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(5042, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x000F0105 File Offset: 0x000EE305
	private void OnExitUpMsg(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x000F0113 File Offset: 0x000EE313
	private void OnClickUpMsg(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("UpGongGao", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x000F012A File Offset: 0x000EE32A
	private void OpenLookingTeamUI(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("LookingTeam", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x000F014C File Offset: 0x000EE34C
	private void OnOverMsg(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(5024, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x000F017A File Offset: 0x000EE37A
	private void OnExitMsg(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000F0188 File Offset: 0x000EE388
	private void OnClickFriend(GameObject go)
	{
		ChatManager.mInst.OnShowOrHideChatWin(true);
		fUIManager.GetInstance().ShowSheJiaoUIWin();
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x000F01A4 File Offset: 0x000EE3A4
	private void OnClickTask(GameObject go)
	{
		bool flag = TaskUICon.inst != null;
		if (flag)
		{
			bool visible = TaskUICon.inst.visible;
			if (visible)
			{
				TaskUICon.inst.Close();
			}
			else
			{
				TaskUICon.inst.Open();
			}
		}
		else
		{
			fUIManager.GetInstance().ShowWin("TaskUI", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
		}
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x000F0208 File Offset: 0x000EE408
	private void OnCloseOpenWin()
	{
		bool flag = !string.IsNullOrEmpty(this.curOpenUI) && this.curOpenUI != "MainUI";
		if (flag)
		{
			fUIManager.GetInstance().SetWinActive(this.curOpenUI, false);
			this.curOpenUI = string.Empty;
		}
		GameRoot.inst.Setting.Close_Setting_Option(null);
		fUIManager.GetInstance().SetWinActive("TaskUI", false);
		fUIManager.GetInstance().SetWinActive("Friend_UI", false);
		fUIManager.GetInstance().SetWinActive("Setting_Option", false);
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x000F02A0 File Offset: 0x000EE4A0
	public void SetPlayerInfo()
	{
		this.SetFriendOnLineNum();
		int nFace = PlayerDataManager.getInstance().HUserData.m_nFace;
		bool flag = fGameManager.mloadConfig.AllFaceData.ContainsKey(nFace);
		if (flag)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[nFace];
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.myFaceIcon);
		}
		this.myLevelTxt.text = string.Format("Lv.{0}", PlayerDataManager.getInstance().HUserData.m_nLevel);
		this.myNameTxt.text = PlayerDataManager.getInstance().HUserData.m_sName;
		string text = "s" + Fun.GetLzImgName();
		bool flag2 = text == "s";
		if (flag2)
		{
			this.myLzLvl.gameObject.SetActive(false);
		}
		else
		{
			this.myLzLvl.gameObject.SetActive(true);
			this.myLzLvl.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
		}
		text = "s" + Fun.GetLzNianImgName();
		bool flag3 = text == "s";
		if (flag3)
		{
			this.myLzNian.gameObject.SetActive(false);
		}
		else
		{
			this.myLzNian.gameObject.SetActive(true);
			this.myLzNian.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, text);
		}
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x000F0418 File Offset: 0x000EE618
	public void UpdateLv()
	{
		this.myLevelTxt.text = string.Format("Lv.{0}", PlayerDataManager.getInstance().HUserData.m_nLevel);
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x000F0448 File Offset: 0x000EE648
	public void UpdateUnMsgNum()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg.Count > 0;
		if (flag)
		{
			this.friendSign.enabled = true;
		}
		else
		{
			this.friendSign.enabled = false;
		}
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x000F0491 File Offset: 0x000EE691
	public void SetFriendOnLineNum()
	{
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x000F0494 File Offset: 0x000EE694
	private void ChangeTitle(int nIndex, object szParam = null)
	{
		bool flag = nIndex == 3 && PlayerDataManager.getInstance().HUserData.m_nLevel < 10;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38058, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
		else
		{
			this.curTitleIdx = nIndex;
			for (int i = 0; i < this.szTitleToggle.Count; i++)
			{
				bool flag2 = i == this.curTitleIdx;
				if (flag2)
				{
					this.OnEnterTitle(this.szTitleToggle[i].gameObject);
					this.szTitleToggle[i].isOn = true;
				}
				else
				{
					this.szTitleToggle[i].isOn = false;
					this.OnExitTitle(this.szTitleToggle[i].gameObject);
				}
			}
			ChatManager.mInst.OnShowOrHideChatWin(true);
			this.OnCloseOpenWin();
			fUIManager.GetInstance().SetWinActive("Setting", false);
			fUIManager.GetInstance().SetWinActive("FaceUI", false);
			fUIManager.GetInstance().SetWinActive("SheJiaoUI", false);
			fUIManager.GetInstance().SetWinActive("RankUI", false);
			fUIManager.GetInstance().SetWinActive("UpGongGao", false);
			this.SetBackBtnData(false, null, null);
			switch (this.curTitleIdx)
			{
			case 0:
			{
				this.fLastClick = 0f;
				bool flag3 = BackgroundUICon.inst == null;
				if (flag3)
				{
					BackgroundUICon.isShowBack = true;
				}
				else
				{
					BackgroundUICon.inst.SetMainBackground(true, string.Empty);
				}
				bool flag4 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
				if (flag4)
				{
					this.OnClickHomeFunc(this.datingBtn.gameObject);
				}
				else
				{
					this.datingUI.SetActive(true);
					this.SetTiantiBtn();
					this.OnSetMaoXianBtnState();
				}
				break;
			}
			case 1:
				this.OnGetCareerData(szParam);
				break;
			case 2:
				fUIManager.GetInstance().ShowWin("ShopUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
				break;
			case 3:
				fUIManager.GetInstance().ShowWin("GuildUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
				break;
			case 4:
				this.OnGetCareerData(szParam);
				break;
			case 5:
			{
				bool flag5 = Fun.IsFunctionOpen("OpenQiangHua", null);
				if (flag5)
				{
					fUIManager.GetInstance().ShowWin("QiangHuaUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000F0758 File Offset: 0x000EE958
	private void OnAddTeamPlayer(GameObject go)
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(1);
		if (mouseButtonDown)
		{
			Text component = go.transform.FindChild("savename").GetComponent<Text>();
			bool flag = component.text == string.Empty;
			if (!flag)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(1);
				bool flag2 = TeamManager.GetInstance().m_nCaptain != 1;
				if (flag2)
				{
					arrayList.Add(2);
					arrayList.Add(3);
				}
				bool flag3 = go != null;
				if (flag3)
				{
					Text component2 = go.transform.FindChild("savename").GetComponent<Text>();
					fUIManager.GetInstance().sChatName = component2.text;
				}
				bool flag4 = fUIManager.GetInstance().sChatName != PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag4)
				{
					bool flag5 = !FriendManager.GetInstance().IsFriendRlt(fUIManager.GetInstance().sChatName);
					if (flag5)
					{
						arrayList.Add(5);
					}
					else
					{
						arrayList.Add(4);
					}
				}
				else
				{
					arrayList.Add(4);
					arrayList.Add(5);
				}
				bool flag6 = go.name != string.Empty;
				if (flag6)
				{
					ChatManager.GetInstance().OnMsgTip(fUIManager.GetInstance().sChatName, 5, arrayList, PlayerDataManager.getInstance().HUserData.m_nCid);
				}
			}
		}
		else
		{
			bool flag7 = TeamManager.GetInstance().m_nTeamID == 0 || TeamManager.GetInstance().m_nCaptain == 1;
			if (flag7)
			{
				bool flag8 = MatchManager.GetInstance().m_nIsMatch > 0;
				if (flag8)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40026, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.AddFrienJoinTeam), null, null, null);
				}
				else
				{
					bool flag9 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
					if (flag9)
					{
						TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(40013, new object[0]), Language.GetLang(8012, new object[0]), Language.GetLang(8020, new object[0]), string.Empty, new TipManager.TipOKDelegate(this.AddFrienJoinTeam), null, null, null);
					}
					else
					{
						this.AddFrienJoinTeam(null);
					}
				}
			}
		}
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000F0A0C File Offset: 0x000EEC0C
	private void AddFrienJoinTeam(object szpram)
	{
		bool flag = MatchManager.GetInstance().m_nIsMatch > 0;
		if (flag)
		{
			MatchManager.GetInstance().OnCancelMatch();
		}
		bool flag2 = PlayerDataManager.getInstance().HUserData.m_nRoomUID != 0;
		if (flag2)
		{
			RoomManage.GetInstance().OnLeave();
		}
		fUIManager.GetInstance().ShowInviteUI(2, 0, null);
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x000F0A6C File Offset: 0x000EEC6C
	private void OnMsgTip(GameObject go)
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(1);
		if (mouseButtonDown)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(1);
			bool flag = TeamManager.GetInstance().m_nCaptain != 1;
			if (flag)
			{
				arrayList.Add(2);
				arrayList.Add(3);
			}
			bool flag2 = go != null;
			if (flag2)
			{
				Text component = go.transform.FindChild("savename").GetComponent<Text>();
				fUIManager.GetInstance().sChatName = component.text;
			}
			bool flag3 = go.name != string.Empty;
			if (flag3)
			{
				ChatManager.GetInstance().OnMsgTip(fUIManager.GetInstance().sChatName, 5, arrayList, PlayerDataManager.getInstance().HUserData.m_nCid);
			}
		}
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000F0B40 File Offset: 0x000EED40
	private void OnClickTitle(GameObject go)
	{
		int nIndex = int.Parse(go.name.Replace("Toggle", string.Empty));
		this.ChangeTitle(nIndex, null);
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x000F0B74 File Offset: 0x000EED74
	private void OnExitTitle(GameObject go)
	{
		int index = int.Parse(go.name.Replace("Toggle", string.Empty));
		Toggle toggle = this.szTitleToggle[index];
		Image image = this.szTitleIcon[index];
		string name = image.sprite.name;
		bool isOn = toggle.isOn;
		if (isOn)
		{
			image.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, name.Replace("normal", "up"));
		}
		else
		{
			image.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, name.Replace("up", "normal"));
		}
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x000F0C24 File Offset: 0x000EEE24
	private void OnEnterTitle(GameObject go)
	{
		int index = int.Parse(go.name.Replace("Toggle", string.Empty));
		Image image = this.szTitleIcon[index];
		string name = image.sprite.name;
		image.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, name.Replace("normal", "up"));
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x000F0C88 File Offset: 0x000EEE88
	private void LoadFriendUIOk(string sUI, GameObject UIObj, object szParam)
	{
		UIObj.transform.localPosition = new Vector3(824.22f, -5.89f, 0f);
		bool flag = Friend_UICon.inst != null;
		if (flag)
		{
			Friend_UICon.inst.InitFriend();
		}
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000F0CD4 File Offset: 0x000EEED4
	public void OnLoadContentUIOk(string sUI, GameObject UIObj, object szParam)
	{
		this.SetCurOpenUI(sUI);
		uint num = PrivateImplementationDetails.ComputeStringHash(sUI);
		if (num <= 1308950890U)
		{
			if (num <= 996954139U)
			{
				if (num != 137033000U)
				{
					if (num != 360592600U)
					{
						if (num == 996954139U)
						{
							if (sUI == "Prerogative")
							{
								GameRoot.inst.Setting_FPS.Load_Prerogative_UI_Complete(sUI, UIObj, szParam);
							}
						}
					}
					else if (sUI == "MailUI")
					{
						this.SetBackBtnData(true, null, null);
					}
				}
				else if (sUI == "SkyLadderUI")
				{
					bool flag = SkyLadderUICon.inst != null;
					if (flag)
					{
						SkyLadderUICon.inst.ResetSkyLadderInfo((int)szParam);
					}
				}
			}
			else if (num <= 1085932700U)
			{
				if (num != 1033011385U)
				{
					if (num == 1085932700U)
					{
						if (sUI == "PveUI")
						{
							this.SetBackBtnData(true, null, null);
						}
					}
				}
				else if (sUI == "CareerUI")
				{
					bool flag2 = CareerUICon.inst != null;
					if (flag2)
					{
						CareerUICon.inst.InitCareerData();
					}
				}
			}
			else if (num != 1257619216U)
			{
				if (num == 1308950890U)
				{
					if (sUI == "LiBaoUI")
					{
						bool flag3 = LiBaoUICon.inst != null;
						if (flag3)
						{
							LiBaoUICon.inst.ShowUIData();
						}
					}
				}
			}
			else if (!(sUI == "ChooseFb"))
			{
			}
		}
		else if (num <= 2425047938U)
		{
			if (num != 1748278905U)
			{
				if (num != 2160962648U)
				{
					if (num == 2425047938U)
					{
						if (sUI == "InviteUI")
						{
							bool flag4 = InviteUICon.inst != null;
							if (flag4)
							{
								ArrayList arrayList = (ArrayList)szParam;
								InviteUICon.inst.ShowListByInfo((int)arrayList[0], (int)arrayList[1], arrayList[2]);
							}
						}
					}
				}
				else if (sUI == "QQ_Games_Hall_Prerogative")
				{
					GameRoot.inst.Setting_FPS.Load_QQ_Games_Hall_Prerogative_UI_Complete(sUI, UIObj, szParam);
				}
			}
			else if (sUI == "FindRoom")
			{
				bool flag5 = !RoomManage.GetInstance().isShowRoomUI;
				if (flag5)
				{
					RoomManage.GetInstance().isShowRoomUI = true;
					bool flag6 = FindRoomCon.inst != null;
					if (flag6)
					{
						FindRoomCon.inst.Close();
					}
				}
			}
		}
		else if (num <= 2774640507U)
		{
			if (num != 2545764389U)
			{
				if (num == 2774640507U)
				{
					if (sUI == "LookingTeam")
					{
						this.SetBackBtnData(true, null, null);
					}
				}
			}
			else if (!(sUI == "WenJuanUI"))
			{
			}
		}
		else if (num != 3206616807U)
		{
			if (num == 3881045295U)
			{
				if (sUI == "HeroInfoUI")
				{
					bool flag7 = HeroInfoUICon.inst != null;
					if (flag7)
					{
						HeroInfoUICon.inst.OnChangeTab(szParam);
					}
				}
			}
		}
		else if (sUI == "ShopUI")
		{
			bool flag8 = ShopUICon.inst != null;
			if (flag8)
			{
				ShopUICon.inst.OnOpenType(1);
			}
		}
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x000F10A8 File Offset: 0x000EF2A8
	private void OnLoadCoverUIOk(string sUI, GameObject UIObj, object szParam)
	{
		this.Close();
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x000F10B4 File Offset: 0x000EF2B4
	public void InitTeamUI(ArrayList szTeamInfo, int nCid, string sName)
	{
		ArrayList arrayList = new ArrayList();
		int num = 1;
		int num2 = 0;
		bool flag = nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			for (int i = 0; i < szTeamInfo.Count; i += 2)
			{
				bool flag2 = szTeamInfo[i].ToString() != PlayerDataManager.getInstance().HUserData.m_sName;
				if (flag2)
				{
					GameObject gameObject = (GameObject)this.AllTeamObj[num2];
					arrayList = (ArrayList)szTeamInfo[i + 1];
					gameObject.name = arrayList[7].ToString();
					RawImage component = gameObject.transform.FindChild("RawImage").GetComponent<RawImage>();
					GameObject gameObject2 = gameObject.transform.FindChild("Image").gameObject;
					bool flag3 = (int)arrayList[17] == 1;
					if (flag3)
					{
						gameObject2.SetActive(true);
					}
					else
					{
						gameObject2.SetActive(false);
					}
					Text component2 = gameObject.transform.FindChild("Text").GetComponent<Text>();
					bool flag4 = arrayList[0].ToString().Length > 3;
					if (flag4)
					{
						component2.text = arrayList[0].ToString().Substring(0, 3) + "...";
					}
					else
					{
						component2.text = arrayList[0].ToString();
					}
					Text component3 = gameObject.transform.FindChild("savename").GetComponent<Text>();
					component3.text = arrayList[0].ToString();
					bool flag5 = fGameManager.mloadConfig.AllFaceData.ContainsKey((int)arrayList[10]);
					if (flag5)
					{
						PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[(int)arrayList[10]];
						fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", component);
					}
					bool flag6 = !this.FaceTeamObj.ContainsKey((int)arrayList[7]);
					if (flag6)
					{
						this.FaceTeamObj.Add((int)arrayList[7], gameObject);
					}
					EventTriggerListener.Get(gameObject).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
					num++;
					num2++;
				}
			}
		}
		else
		{
			int num3 = TeamManager.GetInstance().MyTeamData.LastIndexOf(sName);
			bool flag7 = num3 == -1;
			if (flag7)
			{
				return;
			}
			GameObject gameObject3 = null;
			ArrayList arrayList2 = new ArrayList();
			arrayList2 = (ArrayList)TeamManager.GetInstance().MyTeamData[num3 + 1];
			bool flag8 = TeamManager.GetInstance().m_nCaptain == 1;
			if (flag8)
			{
				bool flag9 = TeamManager.GetInstance().Dyaoqing.ContainsKey(arrayList2[0].ToString());
				if (flag9)
				{
					for (int j = 0; j < 4; j++)
					{
						GameObject gameObject4 = (GameObject)this.AllTeamObj[j];
						bool flag10 = gameObject4 != null;
						if (flag10)
						{
							Text component4 = gameObject4.transform.FindChild("savename").GetComponent<Text>();
							bool flag11 = component4.text == arrayList2[0].ToString();
							if (flag11)
							{
								gameObject3 = gameObject4;
								break;
							}
						}
					}
				}
				bool flag12 = gameObject3 == null;
				if (flag12)
				{
					for (int k = 0; k < 4; k++)
					{
						GameObject gameObject5 = (GameObject)this.AllTeamObj[k];
						bool flag13 = gameObject5 != null;
						if (flag13)
						{
							Text component5 = gameObject5.transform.FindChild("savename").GetComponent<Text>();
							bool flag14 = !TeamManager.GetInstance().MyTeamData.Contains(component5.text);
							if (flag14)
							{
								gameObject3 = gameObject5;
								break;
							}
						}
					}
				}
			}
			else
			{
				for (int l = 0; l < 4; l++)
				{
					GameObject gameObject6 = (GameObject)this.AllTeamObj[l];
					bool flag15 = gameObject6 != null;
					if (flag15)
					{
						Text component6 = gameObject6.transform.FindChild("savename").GetComponent<Text>();
						bool flag16 = component6.text == string.Empty;
						if (flag16)
						{
							gameObject3 = gameObject6;
							break;
						}
					}
				}
			}
			bool flag17 = gameObject3 == null;
			if (flag17)
			{
				Debug.Log("队伍UI初始化错误");
				return;
			}
			TeamCDCon component7 = gameObject3.GetComponent<TeamCDCon>();
			component7.StopCD();
			gameObject3.name = arrayList2[7].ToString();
			RawImage component = gameObject3.transform.FindChild("RawImage").GetComponent<RawImage>();
			GameObject gameObject7 = gameObject3.transform.FindChild("Image").gameObject;
			Text component8 = gameObject3.transform.FindChild("Text").GetComponent<Text>();
			bool flag18 = arrayList2[0].ToString().Length > 3;
			if (flag18)
			{
				component8.text = arrayList2[0].ToString().Substring(0, 3) + "...";
			}
			else
			{
				component8.text = arrayList2[0].ToString();
			}
			Text component9 = gameObject3.transform.FindChild("savename").GetComponent<Text>();
			component9.text = arrayList2[0].ToString();
			bool flag19 = (int)arrayList2[17] == 1;
			if (flag19)
			{
				gameObject7.SetActive(true);
			}
			else
			{
				gameObject7.SetActive(false);
			}
			bool flag20 = fGameManager.mloadConfig.AllFaceData.ContainsKey((int)arrayList2[10]);
			if (flag20)
			{
				PlayerFaceData playerFaceData2 = fGameManager.mloadConfig.AllFaceData[(int)arrayList2[10]];
				fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData2.sFaceIcon + ".txt", component);
			}
			gameObject3.SetActive(true);
			bool flag21 = !this.FaceTeamObj.ContainsKey((int)arrayList2[7]);
			if (flag21)
			{
				this.FaceTeamObj.Add((int)arrayList2[7], gameObject3);
			}
			EventTriggerListener.Get(gameObject3).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
			num++;
		}
		int num4 = TeamManager.GetInstance().MyTeamData.LastIndexOf(PlayerDataManager.getInstance().HUserData.m_sName);
		bool flag22 = num4 != -1;
		if (flag22)
		{
			ArrayList arrayList3 = new ArrayList();
			arrayList3 = (ArrayList)TeamManager.GetInstance().MyTeamData[num4 + 1];
			bool flag23 = (int)arrayList3[17] == 1;
			if (flag23)
			{
				this.SetTeamMaster(true);
			}
			else
			{
				this.SetTeamMaster(false);
			}
			this.SortTeamObj();
		}
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x000F17C0 File Offset: 0x000EF9C0
	public void ChangeTeamMemberNick(string sOld, string sNew)
	{
		for (int i = 0; i < this.AllTeamObj.Count; i++)
		{
			GameObject gameObject = (GameObject)this.AllTeamObj[i];
			Text component = gameObject.transform.FindChild("savename").GetComponent<Text>();
			bool flag = component.text == sOld;
			if (flag)
			{
				component.text = sNew;
				component = gameObject.transform.FindChild("Text").GetComponent<Text>();
				bool flag2 = sNew.ToString().Length > 3;
				if (flag2)
				{
					component.text = sNew.Substring(0, 3) + "...";
				}
				else
				{
					component.text = sNew;
				}
				break;
			}
		}
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x000F1888 File Offset: 0x000EFA88
	public void GetTeamObj(string sname)
	{
		for (int i = 0; i < 4; i++)
		{
			GameObject gameObject = (GameObject)this.AllTeamObj[i];
			bool flag = gameObject != null;
			if (flag)
			{
				Text component = gameObject.transform.FindChild("savename").GetComponent<Text>();
				bool flag2 = component.text == sname;
				if (flag2)
				{
					this.OnClearCD(gameObject);
					break;
				}
			}
		}
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x000F18FC File Offset: 0x000EFAFC
	private void OnClearCD(GameObject obj)
	{
		Text component = obj.transform.FindChild("Text").GetComponent<Text>();
		component.text = string.Empty;
		Text component2 = obj.transform.FindChild("savename").GetComponent<Text>();
		component2.text = string.Empty;
		TeamCDCon component3 = obj.GetComponent<TeamCDCon>();
		component3.StopCD();
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x000F195C File Offset: 0x000EFB5C
	public void DelTeamUI(ArrayList szTeamInfo, int nCid)
	{
		ArrayList arrayList = new ArrayList();
		bool flag = nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			for (int i = 0; i < szTeamInfo.Count; i += 2)
			{
				arrayList = (ArrayList)szTeamInfo[i + 1];
				bool flag2 = arrayList.Count > 0 && this.FaceTeamObj.ContainsKey((int)arrayList[7]);
				if (flag2)
				{
					GameObject obj = this.FaceTeamObj[(int)arrayList[7]];
					this.OnClearTeamObj(obj);
					this.FaceTeamObj.Remove((int)arrayList[7]);
				}
			}
			this.SetTeamMaster(false);
		}
		else
		{
			bool flag3 = this.FaceTeamObj.ContainsKey(nCid);
			if (flag3)
			{
				GameObject obj = this.FaceTeamObj[nCid];
				this.OnClearTeamObj(obj);
				this.FaceTeamObj.Remove(nCid);
			}
			this.SortTeamObj();
		}
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x000F1A64 File Offset: 0x000EFC64
	public void SortTeamObj()
	{
		for (int i = this.AllTeamObj.Count - 1; i >= 0; i--)
		{
			GameObject gameObject = (GameObject)this.AllTeamObj[i];
			bool flag = !(gameObject == null);
			if (flag)
			{
				Text component = gameObject.transform.FindChild("savename").GetComponent<Text>();
				bool flag2 = component != null && component.text != string.Empty;
				if (flag2)
				{
					gameObject.transform.SetSiblingIndex(0);
				}
			}
		}
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x000F1B04 File Offset: 0x000EFD04
	public void DeTeamObj()
	{
		foreach (GameObject gameObject in this.FaceTeamObj.Values)
		{
			bool flag = gameObject != null;
			if (flag)
			{
				this.OnClearTeamObj(gameObject);
			}
		}
		foreach (string sname in TeamManager.GetInstance().Dyaoqing.Keys)
		{
			MainUICon.inst.GetTeamObj(sname);
		}
		TeamManager.GetInstance().Dyaoqing.Clear();
		this.FaceTeamObj.Clear();
		this.SetTeamMaster(false);
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x000F1BEC File Offset: 0x000EFDEC
	private void OnClearTeamObj(GameObject obj)
	{
		obj.name = string.Empty;
		RawImage component = obj.transform.FindChild("RawImage").GetComponent<RawImage>();
		GetIconComponent component2 = component.transform.GetComponent<GetIconComponent>();
		bool flag = component2 != null;
		if (flag)
		{
			component2._Url = string.Empty;
		}
		component.enabled = false;
		GameObject gameObject = obj.transform.FindChild("Image").gameObject;
		gameObject.SetActive(false);
		Text component3 = obj.transform.FindChild("Text").GetComponent<Text>();
		component3.text = string.Empty;
		Text component4 = obj.transform.FindChild("savename").GetComponent<Text>();
		component4.text = string.Empty;
		TeamCDCon component5 = obj.GetComponent<TeamCDCon>();
		component5.StopCD();
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x000F1CC0 File Offset: 0x000EFEC0
	public void OnChangeTeamCap(int nCid, ArrayList szallcid)
	{
		bool flag = nCid == PlayerDataManager.getInstance().HUserData.m_nCid;
		if (flag)
		{
			this.SetTeamMaster(true);
		}
		else
		{
			this.SetTeamMaster(false);
		}
		for (int i = 0; i < szallcid.Count; i++)
		{
			bool flag2 = PlayerDataManager.getInstance().HUserData.m_nCid == (int)szallcid[i];
			if (!flag2)
			{
				Transform transform = this.otherTeamRender.transform.parent.Find(szallcid[i].ToString());
				bool flag3 = !(transform == null);
				if (flag3)
				{
					GameObject gameObject = transform.Find("Image").gameObject;
					bool flag4 = nCid == (int)szallcid[i];
					if (flag4)
					{
						gameObject.SetActive(true);
					}
					else
					{
						gameObject.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x000F1DB0 File Offset: 0x000EFFB0
	private void OverDownloadBar(GameObject go)
	{
		string text = string.Empty;
		bool flag = this.preLoadState == 1;
		if (flag)
		{
			text = Language.GetLang(5028, new object[0]);
		}
		bool flag2 = !string.IsNullOrEmpty(text);
		if (flag2)
		{
			TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
		}
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x000F1E0F File Offset: 0x000F000F
	private void ExitDownloadBar(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x000F1E20 File Offset: 0x000F0020
	private void OnClickDownLoad(GameObject go)
	{
		bool flag = !this.isShowDownLoad;
		if (flag)
		{
			this.downLoadUI.DOAnchorPosY(0f, 0.2f, false).OnComplete(new TweenCallback(this.SetDownLoadBtnImage));
			this.HideDownloadTip();
		}
		else
		{
			this.downLoadUI.DOAnchorPosY(-71f, 0.2f, false).OnComplete(new TweenCallback(this.SetDownLoadBtnImage));
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000F1E98 File Offset: 0x000F0098
	private void SetDownLoadBtnImage()
	{
		bool flag = !this.isShowDownLoad;
		if (flag)
		{
			this.isShowDownLoad = true;
			this.downLoadShow.gameObject.SetActive(false);
			this.downLoadHide.gameObject.SetActive(true);
		}
		else
		{
			this.isShowDownLoad = false;
			this.downLoadShow.gameObject.SetActive(true);
			this.downLoadHide.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x000F1F10 File Offset: 0x000F0110
	private void OnFaceMsg(GameObject go)
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(1);
		if (mouseButtonDown)
		{
			ArrayList arrayList = new ArrayList();
			bool flag = TeamManager.GetInstance().m_nCaptain != 1;
			if (flag)
			{
				arrayList.Add(0);
				arrayList.Add(2);
				arrayList.Add(3);
				arrayList.Add(4);
			}
			bool flag2 = go.name != string.Empty;
			if (flag2)
			{
				ChatManager.GetInstance().OnMsgTip(go.name, 5, arrayList, PlayerDataManager.getInstance().HUserData.m_nCid);
			}
		}
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x000F1FB8 File Offset: 0x000F01B8
	private void OnClickFace(GameObject go)
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(1);
		if (mouseButtonDown)
		{
			bool flag = TeamManager.GetInstance().m_nTeamID == 0;
			if (flag)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(0);
			arrayList.Add(2);
			arrayList.Add(3);
			arrayList.Add(4);
			arrayList.Add(5);
			arrayList.Add(6);
			arrayList.Add(7);
			bool flag2 = go.name != string.Empty;
			if (flag2)
			{
				ChatManager.GetInstance().OnMsgTip(go.name, 5, arrayList, PlayerDataManager.getInstance().HUserData.m_nCid);
				this.isTeamTip = true;
			}
		}
		else
		{
			fUIManager.GetInstance().ShowWin("FaceUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadCoverUIOk), null, true);
		}
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x000F20BC File Offset: 0x000F02BC
	public void CreateBuffIco()
	{
		this.oneBuff.name = "1";
		this.twoBuff.name = "2";
		EventTriggerListener.Get(this.oneBuff.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.oneBuff.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
		EventTriggerListener.Get(this.twoBuff.gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterObj);
		EventTriggerListener.Get(this.twoBuff.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnExitObj);
		EventTriggerListener.Get(this.oneBuff.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuffObj);
		EventTriggerListener.Get(this.twoBuff.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnClickBuffObj);
		this.OneBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_wanjia1");
		this.TwoBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_yingxiong1");
		fUIManager.OnClearObj(this.ObjBuffItemBtnList);
		Dictionary<int, List<ItemBuffData>> buffDataList = ShopManager.GetInstance().BuffDataList;
		int count = buffDataList.Count;
		bool flag = count < 1;
		if (!flag)
		{
			foreach (List<ItemBuffData> list in buffDataList.Values)
			{
				bool flag2 = list.Count >= 1;
				if (flag2)
				{
					this.OnCreateOneItemBuff(list[0]);
				}
			}
			this.onItemBuffCd();
		}
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000F2280 File Offset: 0x000F0480
	public void UpdateItemBuff(int nType)
	{
		bool flag = ShopManager.GetInstance().BuffDataList.ContainsKey(nType);
		if (flag)
		{
			List<ItemBuffData> list = ShopManager.GetInstance().BuffDataList[nType];
			this.OnCreateOneItemBuff(list[0]);
			this.onItemBuffCd();
			bool flag2 = MainUICon.inst != null && MainUICon.inst.visible;
			if (flag2)
			{
				this.LoadAllInEff("mainuibuff", nType);
			}
		}
	}

	// Token: 0x06001997 RID: 6551 RVA: 0x000F22F8 File Offset: 0x000F04F8
	private void OnCreateOneItemBuff(ItemBuffData data)
	{
		bool flag = data.nType == 1;
		if (flag)
		{
			this.OneBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_wanjia");
		}
		else
		{
			this.TwoBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_yingxiong");
		}
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x000F2350 File Offset: 0x000F0550
	private void OnEnterObj(GameObject obj)
	{
		string text = string.Empty;
		int num = int.Parse(obj.name);
		bool flag = !ShopManager.GetInstance().BuffDataList.ContainsKey(num);
		if (flag)
		{
			text = this.GetBuffTipMsg(num, 0);
		}
		else
		{
			List<ItemBuffData> list = ShopManager.GetInstance().BuffDataList[num];
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ItemBuffData itemBuffData = list[i];
				int endCDTime = Fun.GetEndCDTime(itemBuffData.sEndTime);
				bool flag2 = endCDTime > 0;
				if (flag2)
				{
					bool flag3 = text != string.Empty;
					if (flag3)
					{
						text += "\n";
					}
					text += this.GetBuffTipMsg(num, endCDTime);
				}
			}
			bool flag4 = text == string.Empty;
			if (flag4)
			{
				text = this.GetBuffTipMsg(num, 0);
			}
		}
		TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000F245C File Offset: 0x000F065C
	private string GetBuffTipMsg(int nId, int nLong = 0)
	{
		string empty = string.Empty;
		int key = (nId != 1) ? 1012 : 1011;
		ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
		bool flag = nLong == 0;
		string result;
		if (flag)
		{
			string text = empty;
			result = string.Concat(new string[]
			{
				text,
				itemData.m_sName,
				"<color='#e60012'>",
				Language.GetLang(46066, new object[0]),
				"</color>\n",
				itemData.m_sTipDesc,
				"\n<color=#ffff00>",
				Language.GetLang(46067, new object[0]),
				"</color>"
			});
		}
		else
		{
			string text = empty;
			result = string.Concat(new string[]
			{
				text,
				itemData.m_sName,
				"\n",
				itemData.m_sTipDesc,
				"\n<color=#ff0000>",
				Language.GetLang(46068, new object[0]),
				string.Empty,
				Fun.sencondsToHMS_CXP(nLong),
				"</color>"
			});
		}
		return result;
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000F2574 File Offset: 0x000F0774
	private void OnClickBuffObj(GameObject obj)
	{
		int num = int.Parse(obj.name);
		bool flag = num == 1;
		int key;
		int key2;
		if (flag)
		{
			key = 1002;
			key2 = 1001;
		}
		else
		{
			key = 1003;
			key2 = 1002;
		}
		bool flag2 = fGameManager.mloadConfig.AllItemData.ContainsKey(key2) && fGameManager.mloadConfig.AllShopData.ContainsKey(key);
		if (flag2)
		{
			ShopData pShopData = fGameManager.mloadConfig.AllShopData[key];
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key2];
			ShopManager.GetInstance().OpenBuyItemWin(1, pShopData);
		}
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000F2619 File Offset: 0x000F0819
	private void OnExitObj(GameObject obj)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000F2628 File Offset: 0x000F0828
	private void onItemBuffCd()
	{
		bool flag = !base.IsInvoking("onItemBuffCdRun");
		if (flag)
		{
			base.InvokeRepeating("onItemBuffCdRun", 1f, 1f);
		}
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x000F2660 File Offset: 0x000F0860
	private void onItemBuffCdRun()
	{
		Dictionary<int, List<ItemBuffData>> buffDataList = ShopManager.GetInstance().BuffDataList;
		bool flag = buffDataList.Count < 1;
		if (flag)
		{
			base.CancelInvoke("onItemBuffCdRun");
		}
		else
		{
			foreach (List<ItemBuffData> list in buffDataList.Values)
			{
				bool flag2 = this.ItemBuffEndTimeLong(list);
				if (flag2)
				{
					bool flag3 = list[0].nType == 1;
					if (flag3)
					{
						this.OneBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_wanjia1");
					}
					else
					{
						this.TwoBuffImg.sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, "mainui_yingxiong1");
					}
					buffDataList.Remove(list[0].nType);
					ShopManager.GetInstance().BuffDataList = buffDataList;
					TipManager.GetInstance().HideTextTip();
					break;
				}
			}
		}
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000F276C File Offset: 0x000F096C
	private bool ItemBuffEndTimeLong(List<ItemBuffData> data)
	{
		int count = data.Count;
		for (int i = 0; i < count; i++)
		{
			ItemBuffData itemBuffData = data[i];
			bool flag = Fun.GetEndCDTime(itemBuffData.sEndTime) > 0;
			if (flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x000F27BC File Offset: 0x000F09BC
	public void LoadAllInEff(string sEffName, int nType)
	{
		string url = "UI/ab/" + sEffName + ".ab";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.ShowAllInEff), new ArrayList
			{
				nType,
				0
			});
		}
		else
		{
			this.ShowAllInEff(resWWW, new ArrayList
			{
				nType,
				0
			});
		}
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x000F284C File Offset: 0x000F0A4C
	public void ShowAllInEff(WWW www, object Param)
	{
		bool flag = www == null || www.error != null || this.oneBuff == null || this.twoBuff == null;
		if (!flag)
		{
			int num = (int)((ArrayList)Param)[0];
			Transform transform = (num != 1) ? this.twoBuff.transform.FindChild("buffan") : this.oneBuff.transform.FindChild("buffan");
			bool flag2 = transform != null;
			if (flag2)
			{
				transform.gameObject.SetActive(true);
				base.StartCoroutine(this.BuffEff_End(transform.gameObject));
			}
			else
			{
				UnityEngine.Object original = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
				bool flag3 = num == 1;
				if (flag3)
				{
					gameObject.transform.SetParent(this.oneBuff.transform);
				}
				else
				{
					gameObject.transform.SetParent(this.twoBuff.transform);
				}
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.name = "buffan";
				UIDepth uidepth = gameObject.AddComponent<UIDepth>();
				uidepth.SetUIDepth(5, true);
				uidepth.isUI = false;
				base.StartCoroutine(this.BuffEff_End(gameObject));
			}
		}
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x000F29DB File Offset: 0x000F0BDB
	private IEnumerator BuffEff_End(GameObject effObj)
	{
		yield return new WaitForSeconds(1f);
		effObj.SetActive(false);
		yield break;
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x000F29F4 File Offset: 0x000F0BF4
	public void SetHeroNewState()
	{
		int heroNewState = HeroDataManager.GetInstance().GetHeroNewState();
		bool flag = heroNewState == 2;
		if (flag)
		{
			this.heroNewSp.enabled = true;
		}
		else
		{
			this.heroNewSp.enabled = false;
		}
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x000F2A35 File Offset: 0x000F0C35
	private void OnGetCareerData(object szParam)
	{
		this.openUIParam = szParam;
		fGameManager.SendMsgToHallServer(100001, new ArrayList
		{
			0,
			0
		});
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x000F2A6C File Offset: 0x000F0C6C
	public void OnOpenCareerUI()
	{
		bool flag = this.curTitleIdx == 1;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("HeroInfoUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), this.openUIParam, true);
		}
		bool flag2 = this.curTitleIdx == 4;
		if (flag2)
		{
			fUIManager.GetInstance().ShowWin("CareerUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000F2ADC File Offset: 0x000F0CDC
	public void OnCheckTipMsg(int nType)
	{
		for (int i = 0; i < this.m_szAllMsgList.Count; i++)
		{
			OnePushMsgInfo onePushMsgInfo = this.m_szAllMsgList[i];
			bool flag = onePushMsgInfo != null && onePushMsgInfo.m_nType == nType;
			if (flag)
			{
				this.m_szAllMsgList.RemoveAt(i);
			}
		}
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x000F2B38 File Offset: 0x000F0D38
	public void SetTeamCD(string sName)
	{
		int num = TeamManager.GetInstance().Dyaoqing.Count + TeamManager.GetInstance().MyTeamData.Count / 2;
		bool flag = TeamManager.GetInstance().MyTeamData.Count > 0;
		if (flag)
		{
			num--;
		}
		bool flag2 = !TeamManager.GetInstance().Dyaoqing.ContainsKey(sName);
		if (flag2)
		{
			TeamManager.GetInstance().Dyaoqing.Add(sName, null);
		}
		for (int i = 0; i < this.AllTeamObj.Count; i++)
		{
			GameObject gameObject = (GameObject)this.AllTeamObj[i];
			Text component = gameObject.transform.FindChild("savename").GetComponent<Text>();
			bool flag3 = component.text == string.Empty;
			if (flag3)
			{
				Text component2 = gameObject.transform.FindChild("Text").GetComponent<Text>();
				bool flag4 = sName.Length > 3;
				if (flag4)
				{
					component2.text = sName.Substring(0, 3) + "...";
				}
				else
				{
					component2.text = sName;
				}
				component = gameObject.transform.FindChild("savename").GetComponent<Text>();
				component.text = sName;
				TeamCDCon component3 = gameObject.GetComponent<TeamCDCon>();
				component3.SetCD(sName);
				break;
			}
		}
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000F2CA0 File Offset: 0x000F0EA0
	private void OnClickWenJuanBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("WenJuanUI", NetMsg.UITRANSFROM.PublicUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x000F2CC2 File Offset: 0x000F0EC2
	private void Open_Carnival_Page(GameObject _GameObject)
	{
		Application.OpenURL("http://act.qqgame.qq.com/a20180110spring/index.html");
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
	public void WenJuanBtnActive()
	{
		this.WenJuanBtn.SetActive(WenJuanManager.GetInstance().ButShow);
		bool butShow = WenJuanManager.GetInstance().ButShow;
		if (butShow)
		{
			Transform transform = this.WenJuanBtn.transform.FindChild("Text");
			bool flag = transform != null;
			if (flag)
			{
				transform.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x000F2D34 File Offset: 0x000F0F34
	public void OnClickLiBaoBtn(GameObject go)
	{
		LiBaoManager.GetInstance().ClickMainUIBnt();
		fUIManager.GetInstance().ShowWin("LiBaoUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x000F2D61 File Offset: 0x000F0F61
	public void SetHuoDongBtnGuangXiao(bool state)
	{
		this.HuoDongBtn.transform.FindChild("LiBaoBtn_xinmu").gameObject.SetActive(state);
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x000F2D85 File Offset: 0x000F0F85
	public void SetHuoDongBtnActive(bool state)
	{
		this.HuoDongBtn.SetActive(state);
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x000F2D98 File Offset: 0x000F0F98
	public void SetTiantiBtn()
	{
		List<int> actIdByType = ActivityManager.GetInstance().GetActIdByType(2);
		int num = 0;
		bool flag = actIdByType.Count == 0 || fGameManager.LobbyPlayer == null || !fGameManager.LobbyPlayer.Load_Data_State;
		if (!flag)
		{
			ActivityData activityData = ActivityManager.GetInstance().GetActDataById(actIdByType[0]);
			bool flag2 = activityData == null || !fGameManager.mloadConfig.AllSkyLadderData.ContainsKey(activityData.nXmlId);
			if (!flag2)
			{
				SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[activityData.nXmlId];
				GameObject gameObject = this.tiantiBtn.transform.Find("Top").gameObject;
				Text component = this.tiantiBtn.transform.Find("Top/TopText").GetComponent<Text>();
				Image component2 = this.tiantiBtn.transform.Find("Score/SkyLvlIcon").GetComponent<Image>();
				Text component3 = this.tiantiBtn.transform.Find("Score/CenterText").GetComponent<Text>();
				DateTime dateTime = Fun.GetServerDate();
				bool flag3 = activityData.nIsActOpen == 0;
				if (flag3)
				{
					SkyLadderManager.GetInstance().m_nSkyLadderOpen = num;
					component.text = string.Empty;
					component2.gameObject.SetActive(false);
					DateTime t = DateTime.Parse(skyLadderData.sPreTime);
					bool flag4 = dateTime >= t;
					if (flag4)
					{
						t = DateTime.Parse(skyLadderData.sNextTime);
					}
					component3.text = string.Format(skyLadderData.sBtnDesc, t.Month, t.Day);
					this.tiantiDesc.SetActive(false);
					this.skyLadderTip.SetActive(false);
					gameObject.SetActive(false);
				}
				else
				{
					num = 1;
					string[] skyLadderOpenTime = SkyLadderManager.GetInstance().GetSkyLadderOpenTime(dateTime);
					bool flag5 = !string.IsNullOrEmpty(skyLadderOpenTime[0]) || SkyLadderManager.GetInstance().m_nSkyRelink == 1;
					if (flag5)
					{
						num = 2;
					}
					SkyLadderManager.GetInstance().m_nSkyLadderActId = activityData.nXmlId;
					SkyLadderManager.GetInstance().m_nSkyLadderOpen = num;
					dateTime = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd HH:mm:ss"));
					DateTime dateTime2 = DateTime.Parse(activityData.sEndTime);
					bool flag6 = num == 2;
					if (flag6)
					{
						activityData = ActivityManager.GetInstance().GetActDataByType(21);
						bool flag7 = activityData != null && LiBaoManager.GetInstance().GetLastMatchNum() > 0;
						if (flag7)
						{
							SkyEveryDayData skyEveryDayData = fGameManager.mloadConfig.AllSkyEveryDay[activityData.nXmlId];
							component.text = Language.GetLang(5055, new object[]
							{
								skyEveryDayData.nMatchNum,
								skyEveryDayData.nGoldNum
							});
							gameObject.SetActive(true);
						}
						else
						{
							bool flag8 = (int)(dateTime2 - dateTime).TotalDays <= skyLadderData.nOverTipDay;
							if (flag8)
							{
								component.text = Language.GetLang(5040, new object[]
								{
									dateTime2.Month,
									dateTime2.Day
								});
								gameObject.SetActive(true);
							}
							else
							{
								component.text = string.Empty;
								gameObject.SetActive(false);
							}
						}
					}
					else
					{
						bool flag9 = DateTime.Parse(skyLadderOpenTime[4]).DayOfWeek == DayOfWeek.Saturday;
						if (flag9)
						{
							component.text = Language.GetLang(5053, new object[0]);
						}
						else
						{
							component.text = Language.GetLang(5046, new object[]
							{
								DateTime.Parse(skyLadderOpenTime[4]).ToString("HH:mm"),
								DateTime.Parse(skyLadderOpenTime[5]).ToString("HH:mm")
							});
						}
						gameObject.SetActive(true);
					}
					int nSkyMatchnum = SkyLadderManager.GetInstance().m_nSkyMatchnum;
					bool flag10 = nSkyMatchnum < skyLadderData.nMinMatchNum;
					if (flag10)
					{
						component3.text = Language.GetLang(5039, new object[]
						{
							nSkyMatchnum,
							skyLadderData.nMinMatchNum
						});
						component2.gameObject.SetActive(false);
					}
					else
					{
						component3.text = SkyLadderManager.GetInstance().m_nSkyScore.ToString();
						component2.gameObject.SetActive(true);
						component2.sprite = Window.spriteName(AtlasType.ATLAS_SKYLADDER, string.Format("skyladdersmall_{0}", SkyLadderManager.GetInstance().m_nSkyLevel));
					}
					this.tiantiDesc.SetActive(true);
					bool flag11 = !string.IsNullOrEmpty(SkyLadderManager.GetInstance().m_sSkyPunishTime);
					if (flag11)
					{
						dateTime = Fun.GetServerDate();
						dateTime2 = DateTime.Parse(SkyLadderManager.GetInstance().m_sSkyPunishTime);
						bool flag12 = dateTime < dateTime2;
						if (flag12)
						{
							this.skyLadderTip.SetActive(true);
							return;
						}
					}
					this.skyLadderTip.SetActive(false);
				}
				SkyLadderManager.GetInstance().CheckRefreshTime();
				SkyLadderManager.GetInstance().CheckTipTime();
				LiBaoManager.GetInstance().RefreshSkyEveryDayEff();
			}
		}
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000F32B8 File Offset: 0x000F14B8
	private void SetSkyLadderTime()
	{
		bool flag = !this.skyLadderTip.activeSelf;
		if (!flag)
		{
			string sSkyPunishTime = SkyLadderManager.GetInstance().m_sSkyPunishTime;
			bool flag2 = string.IsNullOrEmpty(sSkyPunishTime);
			if (flag2)
			{
				this.skyLadderTip.SetActive(false);
			}
			else
			{
				DateTime dateTime = DateTime.Parse(sSkyPunishTime);
				ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(2);
				DateTime serverDate = Fun.GetServerDate();
				DateTime dateTime2 = DateTime.Parse(actDataByType.sEndTime);
				bool flag3 = dateTime >= dateTime2;
				if (flag3)
				{
					dateTime = dateTime2;
				}
				bool flag4 = dateTime < serverDate;
				if (flag4)
				{
					this.skyLadderTip.SetActive(false);
				}
				else
				{
					this.skyLadderTipTxt.text = Language.GetLang(24020, new object[]
					{
						Fun.sencondsToHMS_CXP((int)(dateTime - serverDate).TotalSeconds)
					});
				}
			}
		}
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x000F3398 File Offset: 0x000F1598
	private void OnDownSkyLadder(GameObject go)
	{
		bool flag = Time.time - this.fLastClick < 1f;
		if (flag)
		{
			ChatManager.GetInstance().OnSendSystemMsg(Language.GetLang(5031, new object[0]), "ffdd00", "system");
		}
		else
		{
			this.fLastClick = Time.time;
			int openActIdByType = ActivityManager.GetInstance().GetOpenActIdByType(2);
			ActivityData actDataById = ActivityManager.GetInstance().GetActDataById(openActIdByType);
			bool flag2 = actDataById == null || SkyLadderManager.GetInstance().isSkyMatching == 1;
			if (!flag2)
			{
				bool flag3 = SkyLadderManager.GetInstance().m_nSkyLadderOpen == 0;
				if (flag3)
				{
					TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(23014, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
				}
				else
				{
					bool mouseButtonDown = Input.GetMouseButtonDown(0);
					if (mouseButtonDown)
					{
						bool flag4 = SkyLadderManager.GetInstance().m_nSkyLadderOpen == 1;
						if (flag4)
						{
							SkyLadderData skyLadderData = fGameManager.mloadConfig.AllSkyLadderData[actDataById.nXmlId];
							string[] skyLadderOpenTime = SkyLadderManager.GetInstance().GetSkyLadderOpenTime(Fun.GetServerDate());
							string lang = Language.GetLang(23034, new object[0]);
							bool flag5 = DateTime.Parse(skyLadderOpenTime[4]).DayOfWeek == DayOfWeek.Saturday;
							if (flag5)
							{
								lang = Language.GetLang(23041, new object[0]);
							}
							TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), lang, Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
							return;
						}
						SkyLadderManager.GetInstance().OnSkyLadderMatch(actDataById.nXmlId, new SkyLadderManager.MatchBtnDelegate(this.SkyLadderMatch), null);
					}
					bool mouseButtonDown2 = Input.GetMouseButtonDown(1);
					if (mouseButtonDown2)
					{
						fUIManager.GetInstance().ShowWin("SkyLadderUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), actDataById.nXmlId, true);
					}
					TipManager.GetInstance().HideTextTip();
				}
			}
		}
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x000F35B8 File Offset: 0x000F17B8
	private void SkyLadderMatch(object szParam)
	{
		base.CancelInvoke("SkyLadderMatch");
		bool flag = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain != 1;
		if (flag)
		{
			TeamManager.GetInstance().OnLeaveTeam();
			base.Invoke("SkyLadderMatch", 0.5f);
		}
		else
		{
			bool flag2 = TeamManager.GetInstance().m_nTeamID != 0 && TeamManager.GetInstance().m_nCaptain == 1;
			if (flag2)
			{
				TeamManager.GetInstance().LeaveLookingTeam(4, 0, 0, 0);
			}
			else
			{
				this.SkyStartMatch();
			}
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000F3654 File Offset: 0x000F1854
	public void SkyStartMatch()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(80002);
		arrayList.Add(2);
		arrayList.Add(Fun.EncodingUTF8(PlayerDataManager.getInstance().HUserData.m_sName));
		arrayList.Add(0);
		SkyLadderManager.GetInstance().isSkyMatching = 1;
		fGameManager.SendMsgToHallServer(80001, arrayList);
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x000F36C8 File Offset: 0x000F18C8
	public void CreateOtherBuffIco()
	{
		bool flag = LiBaoManager.GetInstance() == null;
		if (!flag)
		{
			AItemData actExpBuff = LiBaoManager.GetInstance().GetActExpBuff();
			bool flag2 = actExpBuff == null;
			if (flag2)
			{
				this.OtherBuff1.SetActive(false);
				this.OtherBuff2.SetActive(false);
			}
			else
			{
				ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(12);
				bool flag3 = actDataByType == null;
				if (flag3)
				{
					this.OtherBuff1.SetActive(false);
					this.OtherBuff2.SetActive(false);
				}
				else
				{
					bool flag4 = actExpBuff.UserBuff != 0;
					if (flag4)
					{
						this.OtherBuff1.SetActive(true);
					}
					else
					{
						this.OtherBuff1.SetActive(false);
					}
					bool flag5 = actExpBuff.HeroBuff != 0;
					if (flag5)
					{
						this.OtherBuff2.SetActive(true);
					}
					else
					{
						this.OtherBuff2.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x000F37B0 File Offset: 0x000F19B0
	private void OnOtherBuffOverUpMsg(GameObject go)
	{
		int num = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		int nMsgID = 0;
		bool flag = LiBaoManager.GetInstance() == null;
		if (!flag)
		{
			AItemData actExpBuff = LiBaoManager.GetInstance().GetActExpBuff();
			bool flag2 = actExpBuff == null;
			if (flag2)
			{
				this.OtherBuff1.SetActive(false);
				this.OtherBuff2.SetActive(false);
			}
			else
			{
				ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(12);
				bool flag3 = actDataByType == null;
				if (flag3)
				{
					this.OtherBuff1.SetActive(false);
					this.OtherBuff2.SetActive(false);
				}
				else
				{
					bool flag4 = go.name == "BuffGrid1";
					if (flag4)
					{
						num = actExpBuff.UserBuff;
						text2 = actExpBuff.sName1;
						nMsgID = 42024;
					}
					else
					{
						bool flag5 = go.name == "BuffGrid2";
						if (flag5)
						{
							num = actExpBuff.HeroBuff;
							text2 = actExpBuff.sName2;
							nMsgID = 42025;
						}
					}
					int num2 = (int)(Convert.ToDateTime(actDataByType.sEndTime) - Fun.GetServerDate()).TotalSeconds;
					bool flag6 = num2 < 3600;
					if (flag6)
					{
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							"<color=#ff0000>",
							text2,
							"                         ",
							Language.GetLang(7029, new object[]
							{
								Fun.SecToFullTimeShop(num2)
							}),
							"</color>"
						});
					}
					else
					{
						string text4 = text;
						text = string.Concat(new string[]
						{
							text4,
							"<color=#00ff00>",
							text2,
							"                         ",
							Language.GetLang(7029, new object[]
							{
								Fun.SecToFullTimeShop(num2)
							}),
							"</color>"
						});
					}
					text += "\n";
					text += Language.GetLang(nMsgID, new object[]
					{
						num
					});
					TipManager.GetInstance().ShowTextTip(text, TextAnchor.MiddleLeft, 1f, 20f, -20f);
				}
			}
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x000F39C5 File Offset: 0x000F1BC5
	private void OnOtherBuffExitUpMsg(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x000F39D3 File Offset: 0x000F1BD3
	private void OnChangeNameClick(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("CreateRoleUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnChangeNickUI), null, true);
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x000F39F8 File Offset: 0x000F1BF8
	private void OnChangeNickUI(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = CreateRoleUICon.inst != null && CreateRoleUICon.inst.visible;
		if (flag)
		{
			CreateRoleUICon.inst.SetUIType(0);
		}
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000F3A32 File Offset: 0x000F1C32
	private void ExitChangeName(GameObject go)
	{
		TipManager.GetInstance().HideTextTip();
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x000F3A40 File Offset: 0x000F1C40
	private void OverChangeName(GameObject go)
	{
		TipManager.GetInstance().ShowTextTip(Language.GetLang(10013, new object[0]), TextAnchor.MiddleLeft, 1f, 20f, -20f);
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x000F3A70 File Offset: 0x000F1C70
	private void InItJoinTestButState()
	{
		bool joinTestButState = ActivityManager.GetInstance().JoinTestButState;
		if (joinTestButState)
		{
			this.ShoworHiddenJoinTestBUt(true);
		}
		else
		{
			this.ShoworHiddenJoinTestBUt(false);
		}
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x000F3AA2 File Offset: 0x000F1CA2
	public void ShoworHiddenJoinTestBUt(bool state)
	{
		this.JoinTestBtn.SetActive(state);
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x000F3AB4 File Offset: 0x000F1CB4
	private void OpenJoinTestServerUI(GameObject obj)
	{
		fGameManager.SendMsgToHallServer(16105, new ArrayList
		{
			PlayerDataManager.getInstance().HUserData.m_nCid.ToString(),
			WebParam.client_account
		});
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x000F3AFC File Offset: 0x000F1CFC
	private void SetSevenTargTime()
	{
		string a = Fun.GetServerDate().ToString("yyyy-MM-dd");
		bool flag = a != this.nowDate0;
		if (flag)
		{
			this.nowDate0 = a;
			this.SetSevenTargBtn();
			this.SetTeacherActBtn();
			this.SetLuckyDrawBtn();
			this.SetHeroDiscountBtn();
			this.OnShowDtsMsg();
		}
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x000F3B5C File Offset: 0x000F1D5C
	private void OnSevenTargBtn(GameObject go)
	{
		DateTime d = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
		DateTime d2 = DateTime.Parse(PlayerDataManager.getInstance().HUserData.m_dRegDate.ToString("yyyy-MM-dd"));
		int num = (int)(d - d2).TotalDays + 1;
		bool flag = num <= 7;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("SevenTargetUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000F3BE4 File Offset: 0x000F1DE4
	private void SetSevenTargBtn()
	{
		DateTime d = DateTime.Parse(Fun.GetServerDate().ToString("yyyy-MM-dd"));
		DateTime d2 = DateTime.Parse(PlayerDataManager.getInstance().HUserData.m_dRegDate.ToString("yyyy-MM-dd"));
		int num = (int)(d - d2).TotalDays + 1;
		bool flag = num > 7;
		if (flag)
		{
			this.sevenTargBtn.SetActive(false);
		}
		else
		{
			this.sevenTargBtn.SetActive(true);
		}
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x000F3C68 File Offset: 0x000F1E68
	private void OnTeacherActBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(3);
		bool flag = actDataByType != null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("TeacherDayUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x000F3CAC File Offset: 0x000F1EAC
	public void SetTeacherActBtn()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(3);
		bool flag = actDataByType == null;
		if (flag)
		{
			this.teacherActBtn.SetActive(false);
		}
		else
		{
			this.teacherActBtn.SetActive(true);
		}
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x000F3CF0 File Offset: 0x000F1EF0
	public void OnLuckyDrawBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType != null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("LuckyDrawUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x000F3D34 File Offset: 0x000F1F34
	public void SetLuckyDrawBtn()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(28);
		bool flag = actDataByType == null;
		if (flag)
		{
			this.luckyDrawBtn.SetActive(false);
		}
		else
		{
			this.luckyDrawBtn.SetActive(true);
		}
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x000F3D78 File Offset: 0x000F1F78
	private void OnHeroDiscountBtn(GameObject go)
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(30);
		bool flag = actDataByType != null;
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("HeroDiscountUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x000F3DBC File Offset: 0x000F1FBC
	public void SetHeroDiscountBtn()
	{
		ActivityData actDataByType = ActivityManager.GetInstance().GetActDataByType(30);
		bool flag = actDataByType == null;
		if (flag)
		{
			this.heroDiscountBtn.SetActive(false);
		}
		else
		{
			this.heroDiscountBtn.SetActive(true);
		}
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x000F3E00 File Offset: 0x000F2000
	private void OnNationalDayBtn(GameObject go)
	{
		bool flag = LiBaoManager.GetInstance().IsShowNationalDayBtn();
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("NationalDayUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x000F3E40 File Offset: 0x000F2040
	public void SetNationalDayBtn(bool state)
	{
		bool flag = this.nationalDayBtn != null;
		if (flag)
		{
			this.nationalDayBtn.SetActive(state);
		}
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x000F3E70 File Offset: 0x000F2070
	public void SetNationalDayImg(bool state)
	{
		bool flag = this.nationalDayImg != null;
		if (flag)
		{
			this.nationalDayImg.enabled = state;
		}
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000F3EA0 File Offset: 0x000F20A0
	private void OnHuiliuBtn(GameObject go)
	{
		bool huiliuRewardState = LiBaoManager.GetInstance().GetHuiliuRewardState();
		if (huiliuRewardState)
		{
			fUIManager.GetInstance().ShowWin("HuiliuUI", NetMsg.UITRANSFROM.LobbyUI, null, null, true);
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000F3ED4 File Offset: 0x000F20D4
	public void SetHuiliuBtn(bool state)
	{
		bool flag = this.huiliuActBtn != null;
		if (flag)
		{
			this.huiliuActBtn.SetActive(state);
		}
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x000F3F04 File Offset: 0x000F2104
	private void OnWishTreeBtn(GameObject go)
	{
		bool flag = LiBaoManager.GetInstance().CanShowActUI(43);
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("WishTreeUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x000F3F44 File Offset: 0x000F2144
	public void SetWishTreeBtn(bool state)
	{
		bool flag = Fun.IsFunctionOpen("WishTreeBtn", null) && this.wishTreeBtn != null;
		if (flag)
		{
			this.wishTreeBtn.SetActive(state);
		}
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x000F3F84 File Offset: 0x000F2184
	private void OnJieRiActBtn(GameObject go)
	{
		bool flag = LiBaoManager.GetInstance().IsShowJieRiActBtn();
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("JieRiActUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x000F3FC4 File Offset: 0x000F21C4
	public void SetJieRiActBtn(bool state)
	{
		bool flag = this.jieriActBtn != null;
		if (flag)
		{
			this.jieriActBtn.SetActive(state);
		}
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x000F3FF4 File Offset: 0x000F21F4
	public void SetJieRiActImg(bool state)
	{
		bool flag = this.jieriActImg != null;
		if (flag)
		{
			this.jieriActImg.enabled = state;
		}
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x000F4021 File Offset: 0x000F2221
	private void OnExchgCenterBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("ExchangeUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x000F4043 File Offset: 0x000F2243
	private void OnFuliActBtn(GameObject go)
	{
		fUIManager.GetInstance().ShowWin("FuliUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x000F4065 File Offset: 0x000F2265
	public void SetFuliBtnTip(bool state)
	{
		this.fuliActImg.enabled = state;
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x000F4075 File Offset: 0x000F2275
	public void SetFuliBtnActive(bool state)
	{
		this.fuliActBtn.SetActive(state);
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x000F4088 File Offset: 0x000F2288
	private void OnWinterActBtn(GameObject go)
	{
		bool flag = LiBaoManager.GetInstance().CanShowActUI(46);
		if (flag)
		{
			fUIManager.GetInstance().ShowWin("WinterActUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.OnLoadContentUIOk), null, true);
		}
		LiBaoManager.GetInstance().SetWinterActClick(true);
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x000F40D3 File Offset: 0x000F22D3
	public void SetWinterActBtnTip(bool state)
	{
		this.winterActImg.enabled = state;
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x000F40E3 File Offset: 0x000F22E3
	public void SetWinterActBtn(bool state)
	{
		this.winterActBtn.SetActive(state);
	}

	// Token: 0x040013E5 RID: 5093
	public static MainUICon inst;

	// Token: 0x040013E6 RID: 5094
	private RawImage myFaceIcon;

	// Token: 0x040013E7 RID: 5095
	private Image myLzLvl;

	// Token: 0x040013E8 RID: 5096
	private Image myLzNian;

	// Token: 0x040013E9 RID: 5097
	private Text myNameTxt;

	// Token: 0x040013EA RID: 5098
	private Text myLevelTxt;

	// Token: 0x040013EB RID: 5099
	private GameObject changeNameBtn;

	// Token: 0x040013EC RID: 5100
	private Button soundBtn;

	// Token: 0x040013ED RID: 5101
	private GameObject otherTeamRender;

	// Token: 0x040013EE RID: 5102
	private GameObject TeamRenderCD1;

	// Token: 0x040013EF RID: 5103
	private GameObject TeamRenderCD2;

	// Token: 0x040013F0 RID: 5104
	private GameObject TeamRenderCD3;

	// Token: 0x040013F1 RID: 5105
	private List<GamePlayer> szTeamRender;

	// Token: 0x040013F2 RID: 5106
	private List<Toggle> szTitleToggle;

	// Token: 0x040013F3 RID: 5107
	private List<Image> szTitleIcon;

	// Token: 0x040013F4 RID: 5108
	private Image heroNewSp;

	// Token: 0x040013F5 RID: 5109
	private GameObject datingUI;

	// Token: 0x040013F6 RID: 5110
	private Button shangchengBtn;

	// Token: 0x040013F7 RID: 5111
	private GameObject shangchengLock;

	// Token: 0x040013F8 RID: 5112
	private Button pipeiBtn;

	// Token: 0x040013F9 RID: 5113
	private GameObject pipeiLock;

	// Token: 0x040013FA RID: 5114
	private Button datingBtn;

	// Token: 0x040013FB RID: 5115
	private GameObject datingLock;

	// Token: 0x040013FC RID: 5116
	private Button tiantiBtn;

	// Token: 0x040013FD RID: 5117
	private GameObject tiantiLock;

	// Token: 0x040013FE RID: 5118
	private GameObject tiantiDesc;

	// Token: 0x040013FF RID: 5119
	private Button maoxianBtn;

	// Token: 0x04001400 RID: 5120
	private GameObject maoxianLock;

	// Token: 0x04001401 RID: 5121
	private Button dataoshaBtn;

	// Token: 0x04001402 RID: 5122
	private GameObject dataoshaLock;

	// Token: 0x04001403 RID: 5123
	private GameObject dtsmsg;

	// Token: 0x04001404 RID: 5124
	private Text dtsmsgText;

	// Token: 0x04001405 RID: 5125
	private Dictionary<string, Image> szFuncCheckImg = new Dictionary<string, Image>();

	// Token: 0x04001406 RID: 5126
	private RectTransform downLoadUI;

	// Token: 0x04001407 RID: 5127
	private Image downLoadBar;

	// Token: 0x04001408 RID: 5128
	private Text downLoadTip;

	// Token: 0x04001409 RID: 5129
	private Text downLoadDesc;

	// Token: 0x0400140A RID: 5130
	private Button downLoadHide;

	// Token: 0x0400140B RID: 5131
	private Button downLoadShow;

	// Token: 0x0400140C RID: 5132
	private Text friendNum;

	// Token: 0x0400140D RID: 5133
	private int curTitleIdx = -1;

	// Token: 0x0400140E RID: 5134
	private string curOpenUI = string.Empty;

	// Token: 0x0400140F RID: 5135
	private bool isShowDownLoad;

	// Token: 0x04001410 RID: 5136
	private GameObject isTeamMaster;

	// Token: 0x04001411 RID: 5137
	private GameObject SystemMsgTip;

	// Token: 0x04001412 RID: 5138
	public List<OnePushMsgInfo> m_szAllMsgList = new List<OnePushMsgInfo>();

	// Token: 0x04001413 RID: 5139
	public Dictionary<int, GameObject> FaceTeamObj = new Dictionary<int, GameObject>();

	// Token: 0x04001414 RID: 5140
	public ArrayList TeamCD = new ArrayList();

	// Token: 0x04001415 RID: 5141
	public ArrayList AllTeamObj = new ArrayList();

	// Token: 0x04001416 RID: 5142
	private Text ZuanShiText;

	// Token: 0x04001417 RID: 5143
	private Text MoneyText;

	// Token: 0x04001418 RID: 5144
	private GameObject ChongZhiBtn;

	// Token: 0x04001419 RID: 5145
	private GameObject FcmBtn;

	// Token: 0x0400141A RID: 5146
	private Text FcmTime;

	// Token: 0x0400141B RID: 5147
	private GameObject oneBuff;

	// Token: 0x0400141C RID: 5148
	private GameObject twoBuff;

	// Token: 0x0400141D RID: 5149
	private Image OneBuffImg;

	// Token: 0x0400141E RID: 5150
	private Image TwoBuffImg;

	// Token: 0x0400141F RID: 5151
	private GameObject pLiBaoNumObj;

	// Token: 0x04001420 RID: 5152
	private GameObject pGuildNumObj;

	// Token: 0x04001421 RID: 5153
	private GameObject pQiangHuaNumObj;

	// Token: 0x04001422 RID: 5154
	private Button lzRewardBtn;

	// Token: 0x04001423 RID: 5155
	private GameObject lzParticle;

	// Token: 0x04001424 RID: 5156
	private Button qqRewardBtn;

	// Token: 0x04001425 RID: 5157
	private GameObject qqParticle;

	// Token: 0x04001426 RID: 5158
	private GameObject WenJuanBtn;

	// Token: 0x04001427 RID: 5159
	private GameObject HuoDongBtn;

	// Token: 0x04001428 RID: 5160
	public GameObject Carnival;

	// Token: 0x04001429 RID: 5161
	private bool isLzAward;

	// Token: 0x0400142A RID: 5162
	private bool isQQAward;

	// Token: 0x0400142B RID: 5163
	private Image friendSign;

	// Token: 0x0400142C RID: 5164
	private GameObject backBtn;

	// Token: 0x0400142D RID: 5165
	private MainUICon.BackBtnDelegate backBtnFunc;

	// Token: 0x0400142E RID: 5166
	private object backBtnParam;

	// Token: 0x0400142F RID: 5167
	private GameObject PunUI;

	// Token: 0x04001430 RID: 5168
	private GameObject RedImg;

	// Token: 0x04001431 RID: 5169
	private GameObject YellowImg;

	// Token: 0x04001432 RID: 5170
	private Text maintext;

	// Token: 0x04001433 RID: 5171
	private Text infotext;

	// Token: 0x04001434 RID: 5172
	private GameObject skyLadderTip;

	// Token: 0x04001435 RID: 5173
	private Text skyLadderTipTxt;

	// Token: 0x04001436 RID: 5174
	private GameObject FirstYinDao;

	// Token: 0x04001437 RID: 5175
	private GameObject ArrowObj;

	// Token: 0x04001438 RID: 5176
	private bool hadshowyindao;

	// Token: 0x04001439 RID: 5177
	private bool IsTipShow;

	// Token: 0x0400143A RID: 5178
	private GameObject OtherBuff1;

	// Token: 0x0400143B RID: 5179
	private GameObject OtherBuff2;

	// Token: 0x0400143C RID: 5180
	private GameObject pMailAin;

	// Token: 0x0400143D RID: 5181
	private GameObject JoinTestBtn;

	// Token: 0x0400143E RID: 5182
	private GameObject sevenTargBtn;

	// Token: 0x0400143F RID: 5183
	private GameObject teacherActBtn;

	// Token: 0x04001440 RID: 5184
	private GameObject luckyDrawBtn;

	// Token: 0x04001441 RID: 5185
	private GameObject heroDiscountBtn;

	// Token: 0x04001442 RID: 5186
	private GameObject nationalDayBtn;

	// Token: 0x04001443 RID: 5187
	private Image nationalDayImg;

	// Token: 0x04001444 RID: 5188
	private GameObject huiliuActBtn;

	// Token: 0x04001445 RID: 5189
	private GameObject wishTreeBtn;

	// Token: 0x04001446 RID: 5190
	private GameObject jieriActBtn;

	// Token: 0x04001447 RID: 5191
	private Image jieriActImg;

	// Token: 0x04001448 RID: 5192
	private GameObject fuliActBtn;

	// Token: 0x04001449 RID: 5193
	private Image fuliActImg;

	// Token: 0x0400144A RID: 5194
	private GameObject winterActBtn;

	// Token: 0x0400144B RID: 5195
	private Image winterActImg;

	// Token: 0x0400144C RID: 5196
	public int preLoadState;

	// Token: 0x0400144D RID: 5197
	private bool showDownloadTip = true;

	// Token: 0x0400144E RID: 5198
	private bool IsMainUiShowPre;

	// Token: 0x0400144F RID: 5199
	private int onlineCount;

	// Token: 0x04001450 RID: 5200
	private float fLastClick;

	// Token: 0x04001451 RID: 5201
	public bool isTeamTip;

	// Token: 0x04001452 RID: 5202
	private List<GameObject> ObjBuffItemBtnList = new List<GameObject>();

	// Token: 0x04001453 RID: 5203
	private GameObject EffObj;

	// Token: 0x04001454 RID: 5204
	private object openUIParam;

	// Token: 0x04001455 RID: 5205
	private string nowDate0 = string.Empty;

	// Token: 0x020005E6 RID: 1510
	// (Invoke) Token: 0x0600364F RID: 13903
	public delegate void BackBtnDelegate(object szParam);
}
