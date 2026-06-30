using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class HeroGameMsgCon : Window
{
	// Token: 0x06001193 RID: 4499 RVA: 0x000A4700 File Offset: 0x000A2900
	protected override void Awake()
	{
		base.Awake();
		this.blackcar = base.transform.FindChild("blackcarMsg");
		this.msg = this.blackcar.transform.Find("msg").GetComponent<Text>();
		this.fill = this.blackcar.transform.Find("fill").GetComponent<Image>();
		this.blackcar.gameObject.SetActive(false);
		this.fill.fillAmount = 1f;
		this.tFriendSignNum = base.transform.FindChild("frienddeadNum");
		this.FriendSignNumValue = this.tFriendSignNum.transform.Find("Text").GetComponent<Text>();
		this.tFriendSignNum.gameObject.SetActive(false);
		this.tpinsiIco = base.transform.FindChild("pinsiIco");
		this.tpinsiIco.gameObject.SetActive(false);
		this.revivemsg = base.transform.FindChild("revivemsg").gameObject;
		this.revivemsg.GetComponent<Text>().text = Language.GetLang(3007, new object[0]);
		this.revivemsg.SetActive(false);
		this.buffmsgtxt = base.transform.FindChild("buffmsgtxt").GetComponent<Text>();
		this.buffmsgtxt.text = string.Empty;
		this.tSignClone = base.transform.FindChild("duiyousign/signparent");
		this.tSignClone.gameObject.SetActive(false);
		this.tsignshow = base.transform.FindChild("duiyousign/signshow");
		this.MeTeamMembers = new Dictionary<int, GameObject>();
		this.ohpeffect_left = base.transform.FindChild("hpeffect_left").gameObject;
		this.oAddEffectCon_left = this.ohpeffect_left.AddComponent<PlayerAddEffectCon>();
		this.ohpeffect_left.SetActive(false);
		this.ohpeffect_right = base.transform.FindChild("hpeffect_right").gameObject;
		this.oAddEffectCon_right = this.ohpeffect_right.AddComponent<PlayerAddEffectCon>();
		this.ohpeffect_right.SetActive(false);
		this._HKeyMsg = base.transform.Find("HKeyMsg").gameObject;
		this.SetKeyText();
		this._HKeyMsg.SetActive(false);
		this.oJiShaMsgList = base.transform.Find("jishamsg").gameObject;
		this.oCloneJiShaMsg = base.transform.Find("onemsg").gameObject;
		this.oCloneJiShaMsg.transform.Find("killcount").gameObject.SetActive(false);
		this.oCloneJiShaMsg.SetActive(false);
		this.oCenterMsg = base.transform.Find("centermsglist/Viewport/centermsg").gameObject;
		this.oClonemsglist = base.transform.Find("clonemsglist").gameObject;
		this.oClonemsglist.SetActive(false);
		this.oClonemsgLab = base.transform.Find("clonemsgLab").gameObject;
		this.oClonemsgLab.SetActive(false);
		this.oClonemsgimg = base.transform.Find("clonemsgimg").gameObject;
		this.oClonemsgimg.SetActive(false);
		this.oClonenpc = base.transform.Find("clonenpc").gameObject;
		this.oClonenpc.SetActive(false);
		this.oNpclist = base.transform.Find("npclist").gameObject;
		this.lookherokey = base.transform.Find("lookherokey").gameObject;
		this.lookherokeyText = this.lookherokey.transform.Find("Text").GetComponent<Text>();
		this.lookherokeyBg = this.lookherokey.transform.Find("keybg").GetComponent<RectTransform>();
		this.lookherokey.SetActive(false);
		this.clonehit = base.transform.Find("clonehit").gameObject;
		this.clonehit.SetActive(false);
		this.hitlist = base.transform.Find("hitlist").gameObject;
		this.useface = base.transform.Find("UserMsg/useface").gameObject;
		this.useface1 = base.transform.Find("UserMsg/useface1").gameObject;
		this.useT = base.transform.Find("UserMsg/useT").gameObject;
		this.useface.SetActive(false);
		this.useface1.SetActive(false);
		this.useT.SetActive(false);
		this.cloneauto = base.transform.Find("cloneauto").gameObject;
		this.cloneauto.SetActive(false);
		this.autolist = base.transform.Find("autolist").gameObject;
		this.exitgamemsg = base.transform.Find("exitgamemsg").gameObject;
		this.ExitGameMsg1 = this.exitgamemsg.transform.Find("msg1").GetComponent<Text>();
		this.ExitGameMsg2 = this.exitgamemsg.transform.Find("msg2").GetComponent<Text>();
		this.exitgamemsg.SetActive(false);
		this.tNewPlayerMsg = base.transform.Find("newplayermsg").GetComponent<Text>();
		this.tNewPlayerMsg.text = string.Empty;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x000A4C94 File Offset: 0x000A2E94
	public void OnUpdateNetLiving(NetLiving _net)
	{
		bool flag = _net != null;
		if (flag)
		{
			this._NetLiving = _net;
		}
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x000A4CB4 File Offset: 0x000A2EB4
	private void Update()
	{
		bool flag = (this._NetLiving != null && (KingTrainManager.GetInstance().GetGameIsReady() == 1 || MatchManager.GetInstance().m_pFbData.nType == 6 || this._NetLiving.playerCommon.m_nInJidi == 1) && MatchManager.GetInstance().m_pFbData.nType != 10) || fUIManager.GetInstance().Arena_Score_Death_Change_Hero();
		if (flag)
		{
			bool flag2 = !this._HKeyMsg.activeSelf;
			if (flag2)
			{
				this._HKeyMsg.SetActive(true);
			}
			int gameTime = fUIManager.mInst.GetGameTime();
			bool flag3 = gameTime > 0;
			if (flag3)
			{
				bool flag4 = !this._HKeyMsg.activeSelf;
				if (flag4)
				{
					this._HKeyMsg.SetActive(true);
				}
				bool flag5 = WebParam.client_language == "zh";
				if (flag5)
				{
					this._HKeyMsg.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2022, new object[]
					{
						Fun.SecToFullTimeString(gameTime)
					});
				}
				else
				{
					this._HKeyMsg.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2022, new object[]
					{
						(gameTime / 60).ToString("00") + ":" + (gameTime % 60).ToString("00")
					});
				}
			}
			else
			{
				bool flag6 = !this._HKeyMsg.activeSelf;
				if (flag6)
				{
					this._HKeyMsg.SetActive(true);
				}
				this.SetKeyText();
			}
		}
		else
		{
			bool activeSelf = this._HKeyMsg.activeSelf;
			if (activeSelf)
			{
				this._HKeyMsg.SetActive(false);
			}
		}
		this.OnCheckExitGame();
		bool flag7 = this._NetLiving == null;
		if (!flag7)
		{
			bool isCopy = this._NetLiving.isCopy;
			if (isCopy)
			{
				bool activeSelf2 = this.oJiShaMsgList.activeSelf;
				if (activeSelf2)
				{
					this.oJiShaMsgList.SetActive(false);
				}
				bool activeSelf3 = this.oCenterMsg.activeSelf;
				if (activeSelf3)
				{
					this.oCenterMsg.SetActive(false);
				}
			}
			else
			{
				bool flag8 = !this.oJiShaMsgList.activeSelf;
				if (flag8)
				{
					this.oJiShaMsgList.SetActive(true);
				}
				bool flag9 = !this.oCenterMsg.activeSelf;
				if (flag9)
				{
					this.oCenterMsg.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000A4F40 File Offset: 0x000A3140
	private void SetKeyText()
	{
		bool flag = this._NetLiving != null;
		if (flag)
		{
			string key_Replace_Hero = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Replace_Hero;
			this._HKeyMsg.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(2021, new object[]
			{
				GameRoot.inst.Setting.Get_Key_Describe(key_Replace_Hero)
			});
		}
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x000A4FCC File Offset: 0x000A31CC
	public void OnCarHint(bool oncar, string laosiji, float speed)
	{
		if (oncar)
		{
			bool flag = !this.blackcar.gameObject.activeSelf;
			if (flag)
			{
				this.blackcar.gameObject.SetActive(true);
			}
		}
		else
		{
			bool activeSelf = this.blackcar.gameObject.activeSelf;
			if (activeSelf)
			{
				this.blackcar.gameObject.SetActive(false);
				return;
			}
		}
		this.msg.text = "<color=#ffb345>" + laosiji + "</color>开启了传送门，是否立即传送";
		this.fill.fillAmount = speed;
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x000A5060 File Offset: 0x000A3260
	public void OnFriendDeadNum(int nNum)
	{
		bool flag = nNum < 1;
		if (flag)
		{
			bool activeSelf = this.tFriendSignNum.gameObject.activeSelf;
			if (activeSelf)
			{
				this.tFriendSignNum.gameObject.SetActive(false);
			}
		}
		else
		{
			bool flag2 = !this.tFriendSignNum.gameObject.activeSelf;
			if (flag2)
			{
				this.tFriendSignNum.gameObject.SetActive(true);
			}
			this.FriendSignNumValue.text = nNum.ToString();
		}
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000A50E0 File Offset: 0x000A32E0
	public void FriendPinSiMate(bool fbool, Vector3 ve)
	{
		if (fbool)
		{
			this.tpinsiIco.gameObject.transform.localPosition = ve;
		}
		bool flag = fbool != this.tpinsiIco.gameObject.activeSelf;
		if (flag)
		{
			this.tpinsiIco.gameObject.SetActive(fbool);
		}
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x000A513C File Offset: 0x000A333C
	public void OnDaZhaoFriendName(int nServerId)
	{
		string sMsg = string.Empty;
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nServerId);
		bool flag = this._NetLiving != null && netLiving != null && nServerId != this._NetLiving.getNetid();
		if (flag)
		{
			sMsg = Language.GetLang(3005, new object[]
			{
				netLiving.playerCommon.m_sName
			});
		}
		this.AddGameMsg(sMsg);
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x000A51B4 File Offset: 0x000A33B4
	public void OnSkillReviveUI(bool mbool)
	{
		bool flag = this.revivemsg.activeSelf != mbool;
		if (flag)
		{
			this.revivemsg.SetActive(mbool);
		}
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x000A51E8 File Offset: 0x000A33E8
	public void FriendDeadMate(NetLiving _gameplayer, Vector3 ve)
	{
		bool flag = _gameplayer != null;
		if (flag)
		{
			int netid = _gameplayer.getNetid();
			bool flag2 = !this.MeTeamMembers.ContainsKey(netid);
			GameObject gameObject;
			if (flag2)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tSignClone.gameObject);
				gameObject.transform.SetParent(this.tsignshow);
				Fun.Reset(gameObject);
				gameObject.SetActive(true);
				NewteamSign newteamSign = gameObject.AddComponent<NewteamSign>();
				newteamSign.init(_gameplayer);
				this.MeTeamMembers.Add(_gameplayer.getNetid(), gameObject);
			}
			else
			{
				gameObject = this.MeTeamMembers[netid];
			}
			bool flag3 = ve == this.vsv;
			if (flag3)
			{
				gameObject.SetActive(false);
			}
			else
			{
				gameObject.transform.localPosition = ve;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000A52BC File Offset: 0x000A34BC
	public void DeleteSignOne(int netId)
	{
		bool flag = this.MeTeamMembers.ContainsKey(netId);
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.MeTeamMembers[netId]);
			this.MeTeamMembers.Remove(netId);
		}
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x000A52FC File Offset: 0x000A34FC
	public void clear(int nType)
	{
		bool flag = nType == 0;
		if (flag)
		{
			while (this.tsignshow.childCount > 0)
			{
				UnityEngine.Object.DestroyImmediate(this.tsignshow.GetChild(0).gameObject);
			}
			bool flag2 = this.MeTeamMembers != null;
			if (flag2)
			{
				this.MeTeamMembers.Clear();
			}
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000A535C File Offset: 0x000A355C
	public void OnShowToMeDamageEffect(int damageType, int attackId, int nCureTarghtID)
	{
		this.oAddEffectCon_left.OnToMeData(damageType, attackId, nCureTarghtID);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x000A536E File Offset: 0x000A356E
	public void OnShowToItDamageEffect(int damageType, int attackId, int nCureTarghtID, GamePlayer _GamePlayer, ArrayList szHp)
	{
		this.oAddEffectCon_right.OnToItData(damageType, attackId, nCureTarghtID, _GamePlayer, szHp);
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x000A5384 File Offset: 0x000A3584
	public void OnFriendDamage(int targetID)
	{
		this.oAddEffectCon_right.OnShowFriendName(targetID);
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000A5394 File Offset: 0x000A3594
	public void OnDeadname(string sDeadname)
	{
		this.AddGameMsg(sDeadname);
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x000A53A0 File Offset: 0x000A35A0
	public void SkillMsg(string sMsg)
	{
		bool flag = !(this.sTopMsg == sMsg) || Time.time - this.fTopMsgTime > 5f;
		if (flag)
		{
			this.fTopMsgTime = Time.time;
			this.sTopMsg = sMsg;
			this.AddGameMsg(sMsg);
		}
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000A53F4 File Offset: 0x000A35F4
	public void AddGameMsg(string sMsg)
	{
		bool flag = sMsg == string.Empty;
		if (!flag)
		{
			GameObject oClone = this.getMsgListObj(sMsg);
			bool flag2 = oClone.transform.childCount >= 1;
			if (flag2)
			{
				this.OnStartMoveMsg(this.oCenterMsg);
				oClone.transform.SetParent(this.oCenterMsg.transform);
				oClone.transform.localScale = Vector3.one;
				oClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
				oClone.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -100f, 0f);
				HintMsgEffects hintMsgEffects = oClone.AddComponent<HintMsgEffects>();
				hintMsgEffects.OnStart(1, 0.5f, 3f, 1, 0);
				oClone.SetActive(true);
				Tweener t = oClone.transform.DOScale(new Vector3(2f, 2f, 1f), 0.1f);
				t.OnComplete(delegate
				{
					oClone.transform.DOScale(Vector3.one, 0.1f);
				});
			}
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x000A5534 File Offset: 0x000A3734
	private GameObject getMsgListObj(string sMsg)
	{
		string[] array = sMsg.Split(new char[]
		{
			'@'
		});
		int num = array.Length;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oClonemsglist);
		for (int i = 0; i < num; i++)
		{
			string text = array[i].ToString();
			bool flag = !(text == string.Empty);
			if (flag)
			{
				string[] array2 = text.Split(new char[]
				{
					'&'
				});
				bool flag2 = array2.Length > 1;
				GameObject gameObject2;
				if (flag2)
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.oClonemsgimg);
					gameObject2.GetComponent<Image>().sprite = Window.spriteName(array2[0], array2[1]);
				}
				else
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.oClonemsgLab);
					gameObject2.GetComponent<Text>().text = text;
				}
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
				gameObject2.SetActive(true);
			}
		}
		return gameObject;
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000A565C File Offset: 0x000A385C
	public void ShowHintMsg(int nAttackId, int nTargetID)
	{
		string sName = string.Empty;
		string text = string.Empty;
		int nForce = 0;
		string sHeroHead = string.Empty;
		string sHeroHead2 = string.Empty;
		string text2 = string.Empty;
		int num = 0;
		bool isCopy = this._NetLiving.isCopy;
		if (!isCopy)
		{
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nAttackId);
			NetLiving netLiving2 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nTargetID);
			bool flag = netLiving == null || netLiving2 == null;
			if (!flag)
			{
				bool flag2 = netLiving.netobject_type == 2;
				if (flag2)
				{
					int master_ID = ((Npc_Entity)netLiving).Master_ID;
					NetLiving netLiving3 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID);
					bool flag3 = netLiving3 != null;
					if (flag3)
					{
						num = netLiving3.getNetid();
					}
				}
				else
				{
					num = nAttackId;
				}
				NetLiving netLiving4 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
				bool flag4 = netLiving2.netobject_type == 2 && num != this._NetLiving.getNetid();
				if (flag4)
				{
					int master_ID2 = ((Npc_Entity)netLiving2).Master_ID;
					bool flag5 = ((Npc_Entity)netLiving2).Type == 1 && master_ID2 == this._NetLiving.getNetid();
					if (flag5)
					{
						this.AddGameMsg(Language.GetLang(3026, new object[]
						{
							((Npc_Entity)netLiving2).Name
						}));
					}
				}
				bool flag6 = num == this._NetLiving.getNetid() && netLiving2.netobject_type == 2;
				if (flag6)
				{
					int master_ID3 = ((Npc_Entity)netLiving2).Master_ID;
					bool flag7 = num == master_ID3 || ((Npc_Entity)netLiving2).Index == 1017;
					if (flag7)
					{
						return;
					}
					NetLiving netLiving5 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID3);
					bool flag8 = netLiving5 != null;
					if (flag8)
					{
						sHeroHead2 = this.GetHeroSmallTop(netLiving5.playerCommon.m_nModelIndex);
					}
				}
				bool flag9 = nAttackId != nTargetID;
				if (flag9)
				{
					bool flag10 = netLiving.netobject_type == 2;
					if (flag10)
					{
						int master_ID4 = ((Npc_Entity)netLiving).Master_ID;
						NetLiving netLiving6 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID4);
						bool flag11 = netLiving6 != null;
						if (flag11)
						{
							sHeroHead = this.GetHeroSmallTop(netLiving6.playerCommon.m_nModelIndex);
							sName = netLiving6.playerCommon.m_sName;
							nForce = netLiving6.nForce;
						}
						else
						{
							sHeroHead = ((Npc_Entity)netLiving).Icon;
							sName = ((Npc_Entity)netLiving).Name;
							nForce = ((Npc_Entity)netLiving).nForce;
						}
					}
					else
					{
						sName = netLiving.playerCommon.m_sName;
						sHeroHead = this.GetHeroSmallTop(netLiving.playerCommon.m_nModelIndex);
						nForce = netLiving.nForce;
					}
				}
				else
				{
					sName = string.Empty;
					sHeroHead = string.Empty;
				}
				bool flag12 = netLiving2.netobject_type == 2;
				int nForce2;
				if (flag12)
				{
					bool flag13 = ((Npc_Entity)netLiving2).Icon == string.Empty || ((Npc_Entity)netLiving2).Icon == "0";
					if (flag13)
					{
						return;
					}
					int master_ID5 = ((Npc_Entity)netLiving2).Master_ID;
					bool flag14 = nAttackId == master_ID5;
					if (flag14)
					{
						return;
					}
					NetLiving netLiving7 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID5);
					bool flag15 = netLiving7 == null;
					if (flag15)
					{
						text = ((Npc_Entity)netLiving2).Name;
						nForce2 = ((Npc_Entity)netLiving2).nForce;
					}
					else
					{
						text = netLiving7.playerCommon.m_sName;
						nForce2 = netLiving7.nForce;
					}
					sHeroHead2 = ((Npc_Entity)netLiving2).Icon;
				}
				else
				{
					text = netLiving2.playerCommon.m_sName;
					bool flag16 = MatchManager.GetInstance().m_nMatchType == 4;
					if (flag16)
					{
						text = text + "  " + Language.GetLang(29022, new object[]
						{
							DtsBatteManager.GetInstance().m_nLivePlayerNum
						});
					}
					sHeroHead2 = this.GetHeroSmallTop(netLiving2.playerCommon.m_nModelIndex);
					nForce2 = netLiving2.nForce;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oCloneJiShaMsg);
				gameObject.transform.SetParent(this.oJiShaMsgList.transform);
				gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 100f, 0f);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				bool flag17 = num != nTargetID && (netLiving2.netobject_type == 1 || netLiving2.netobject_type == 6 || (netLiving2.netobject_type == 2 && MatchManager.GetInstance().m_pFbData.nType == 6));
				if (flag17)
				{
					int nKillCount = (netLiving4 != null) ? netLiving4.playerCommon.m_nKillCount : netLiving.playerCommon.m_nKillCount;
					text2 = this.GetKillCount(nKillCount);
					bool flag18 = text2 != string.Empty;
					if (flag18)
					{
						gameObject.transform.Find("killcount/count").GetComponent<Text>().text = text2;
						gameObject.transform.Find("killcount").gameObject.SetActive(true);
					}
				}
				Text component = gameObject.transform.Find("a_name").GetComponent<Text>();
				GameObject gameObject2 = gameObject.transform.Find("a_top").gameObject;
				this.setMsgValue(gameObject2, component, sName, nForce, sHeroHead);
				component = gameObject.transform.Find("t_name").GetComponent<Text>();
				gameObject2 = gameObject.transform.Find("t_top").gameObject;
				this.setMsgValue(gameObject2, component, text, nForce2, sHeroHead2);
				HintMsgEffects hintMsgEffects = gameObject.AddComponent<HintMsgEffects>();
				hintMsgEffects.OnStart(0, 0.5f, 5f, 0, 1);
				gameObject.SetActive(true);
				this.OnStartMoveMsg(this.oJiShaMsgList);
			}
		}
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x000A5C8C File Offset: 0x000A3E8C
	private string GetHeroSmallTop(int nModelIndex)
	{
		string result = string.Empty;
		bool flag = fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(nModelIndex);
		if (flag)
		{
			result = fGameManager.mloadConfig.AllHeroUnlock[nModelIndex].szSkinUnlock[1].sIconName;
		}
		else
		{
			bool flag2 = nModelIndex == 2000;
			if (flag2)
			{
				result = "luomo_1";
			}
		}
		return result;
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x000A5CF4 File Offset: 0x000A3EF4
	private string GetKillCount(int nKillCount)
	{
		string result = string.Empty;
		switch (nKillCount)
		{
		case 2:
			result = Language.GetLang(2037, new object[0]);
			break;
		case 3:
			result = Language.GetLang(2038, new object[0]);
			break;
		case 4:
			result = Language.GetLang(2039, new object[0]);
			break;
		case 5:
			result = Language.GetLang(2040, new object[0]);
			break;
		default:
			if (nKillCount == 100)
			{
				result = Language.GetLang(2041, new object[0]);
			}
			break;
		}
		return result;
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000A5D94 File Offset: 0x000A3F94
	public void OnAddKillDeadMsg(int nAttackId, int nTargetID)
	{
		int num = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nAttackId);
		NetLiving netLiving2 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nTargetID);
		bool flag = netLiving == null || netLiving2 == null;
		if (!flag)
		{
			bool flag2 = netLiving.netobject_type == 2;
			if (flag2)
			{
				int master_ID = ((Npc_Entity)netLiving).Master_ID;
				NetLiving netLiving3 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID);
				bool flag3 = netLiving3 != null;
				if (flag3)
				{
					num = netLiving3.getNetid();
				}
			}
			else
			{
				num = nAttackId;
			}
			bool flag4 = num != this._NetLiving.getNetid();
			if (!flag4)
			{
				bool flag5 = netLiving2.netobject_type == 2;
				if (flag5)
				{
					int master_ID2 = ((Npc_Entity)netLiving2).Master_ID;
					bool flag6 = num == master_ID2 || ((Npc_Entity)netLiving2).Index == 1017;
					if (flag6)
					{
						return;
					}
					NetLiving netLiving4 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(master_ID2);
					bool flag7 = netLiving4 != null;
					if (flag7)
					{
						text = " " + netLiving4.playerCommon.m_sName + " " + netLiving2.Name;
					}
				}
				else
				{
					bool flag8 = this._NetLiving.getNetid() != netLiving2.getNetid();
					if (flag8)
					{
						text = netLiving2.playerCommon.m_sName;
					}
				}
				bool flag9 = text != string.Empty;
				if (flag9)
				{
					text2 = Language.GetLang(3022, new object[]
					{
						text
					});
					bool flag10 = MatchManager.GetInstance().m_nMatchType == 4;
					if (flag10)
					{
						text2 = text2 + "     " + Language.GetLang(29034, new object[]
						{
							DtsBatteManager.GetInstance().m_nLivePlayerNum
						});
					}
					this.OnDeadname(text2);
				}
			}
		}
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x000A5F98 File Offset: 0x000A4198
	private void OnStartMoveMsg(GameObject oGameList)
	{
		foreach (object obj in oGameList.transform)
		{
			Transform transform = (Transform)obj;
			bool flag = !(transform == null);
			if (flag)
			{
				HintMsgEffects component = transform.GetComponent<HintMsgEffects>();
				bool flag2 = !(component == null);
				if (flag2)
				{
					component.iMoveNum++;
				}
			}
		}
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x000A602C File Offset: 0x000A422C
	private void setMsgValue(GameObject oHeroTop, Text tName, string sName, int nForce, string sHeroHead)
	{
		string text = "#11AEBE";
		int num = 0;
		bool flag = this._NetLiving.nForce != nForce;
		if (flag)
		{
			text = "#DF2830";
			num = 1;
		}
		bool flag2 = sName != string.Empty;
		if (flag2)
		{
			tName.text = string.Concat(new string[]
			{
				"<color=",
				text,
				">",
				sName,
				"</color>"
			});
		}
		else
		{
			tName.text = string.Empty;
		}
		bool flag3 = sHeroHead != string.Empty;
		if (flag3)
		{
			oHeroTop.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_NEWGAMEUI, "yuan_" + num.ToString());
			RawImage component = oHeroTop.transform.Find("top").GetComponent<RawImage>();
			bool flag4 = sHeroHead != string.Empty;
			if (flag4)
			{
				sysload.loadImage(component, "headjiaodou/" + sHeroHead + ".txt", null);
			}
		}
		else
		{
			oHeroTop.SetActive(false);
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000A6144 File Offset: 0x000A4344
	public void AddBuffMsg(string sbuffmsg, int nTime)
	{
		this.buffmsgtxt.text = sbuffmsg;
		bool flag = sbuffmsg != string.Empty && nTime > 0;
		if (flag)
		{
			base.CancelInvoke("DealyBuffClear");
			base.Invoke("DealyBuffClear", (float)nTime);
		}
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x000A6193 File Offset: 0x000A4393
	private void DealyBuffClear()
	{
		this.buffmsgtxt.text = string.Empty;
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x000A61A8 File Offset: 0x000A43A8
	public GameObject AddNpcObj(int nNetId)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.oClonenpc);
		gameObject.transform.SetParent(this.oNpclist.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		gameObject.name = nNetId.ToString();
		return gameObject;
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x000A6228 File Offset: 0x000A4428
	public void DelNpcObj(int nNetId)
	{
		Transform transform = this.oNpclist.transform.Find(nNetId.ToString());
		bool flag = transform != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(transform.gameObject);
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000A6268 File Offset: 0x000A4468
	public void OnLookHeroTextEffect()
	{
		bool flag = fGameManager.IsSingle || this._NetLiving == null;
		if (!flag)
		{
			int nWidth = 0;
			bool flag2 = !this._NetLiving.isCopy;
			if (flag2)
			{
				this.OnUseKeyMsg();
				string text = GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Check_Hero_Information);
				this.lookherokeyText.text = Language.GetLang(2029, new object[]
				{
					text
				});
				this.lookherokeyText.color = new Color(1f, 1f, 1f, 0f);
				this.lookherokeyBg.sizeDelta = new Vector2(0f, 48f);
				this.lookherokey.SetActive(true);
				Tween t = DOTween.To(() => nWidth, delegate(int x)
				{
					nWidth = x;
				}, 1920, 0.5f);
				t.OnUpdate(delegate
				{
					this.UpdateTween(nWidth);
				});
				t.OnComplete(delegate
				{
					this.EdnOverTween();
				});
				base.CancelInvoke("showend");
				base.Invoke("showend", 5f);
			}
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x000A63D9 File Offset: 0x000A45D9
	private void UpdateTween(int nWidth)
	{
		this.lookherokeyBg.sizeDelta = new Vector2((float)nWidth * 1f, 48f);
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000A63FA File Offset: 0x000A45FA
	private void EdnOverTween()
	{
		this.lookherokeyText.DOColor(Color.white, 0.5f);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000A6414 File Offset: 0x000A4614
	private void showend()
	{
		Color endValue = new Color(1f, 1f, 1f, 0f);
		Tween t = this.lookherokeyText.DOColor(endValue, 0.5f);
		t.OnComplete(delegate
		{
			this.OverShowUI();
		});
		this.useface.SetActive(false);
		this.useface1.SetActive(false);
		this.useT.SetActive(false);
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x000A6489 File Offset: 0x000A4689
	private void OverShowUI()
	{
		this.lookherokey.SetActive(false);
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x000A649C File Offset: 0x000A469C
	public void OnUseKeyMsg()
	{
		bool flag = MatchManager.GetInstance().m_nMatchType == 4;
		if (flag)
		{
			this.useface.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(29057, new object[0]);
			this.useface.SetActive(true);
			this.useface1.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(29069, new object[0]);
			this.useface1.SetActive(true);
		}
		else
		{
			bool flag2 = this._NetLiving.playerCommon.m_nLevel > 9;
			if (flag2)
			{
				return;
			}
			string text = GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Interact_Expression);
			this.useface.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(3027, new object[]
			{
				text
			});
			this.useface.SetActive(true);
			bool activeSelf = this.useface1.activeSelf;
			if (activeSelf)
			{
				this.useface1.SetActive(false);
			}
		}
		string text2 = GameRoot.inst.Setting.Get_Key_Describe(GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(this._NetLiving.playerCommon.m_nModelIndex).Key_Spray_Paint);
		this.useT.transform.Find("Text").GetComponent<Text>().text = Language.GetLang(3028, new object[]
		{
			text2
		});
		this.useT.SetActive(true);
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000A6674 File Offset: 0x000A4874
	public void OnHurtEffect(int nNetId, float fangle)
	{
		Vector3 zero = Vector3.zero;
		bool flag = !this._NetLiving.isDead;
		if (flag)
		{
			Transform transform = this.hitlist.transform.Find(nNetId.ToString());
			bool flag2 = transform == null;
			GameObject gameObject;
			if (flag2)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonehit);
				gameObject.transform.SetParent(this.hitlist.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = nNetId.ToString();
				gameObject.SetActive(true);
			}
			else
			{
				gameObject = transform.gameObject;
			}
			zero.z = fangle;
			HurtEffectCon hurtEffectCon = gameObject.GetComponent<HurtEffectCon>();
			bool flag3 = hurtEffectCon == null;
			if (flag3)
			{
				hurtEffectCon = gameObject.AddComponent<HurtEffectCon>();
			}
			hurtEffectCon.OnStart(zero);
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x000A6760 File Offset: 0x000A4960
	public void ClearHurtEffect()
	{
		Fun.ClearChildObejct(this.hitlist);
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x000A6770 File Offset: 0x000A4970
	public void OnAddSuperSkillEffect(int nNetId, float sca = 1f)
	{
		Transform transform = this.autolist.transform.Find(nNetId.ToString());
		bool flag = transform == null;
		GameObject gameObject;
		if (flag)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cloneauto);
			gameObject.transform.SetParent(this.autolist.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.name = nNetId.ToString();
			gameObject.SetActive(true);
		}
		else
		{
			gameObject = transform.gameObject;
		}
		LzzSuperSkillSign lzzSuperSkillSign = gameObject.GetComponent<LzzSuperSkillSign>();
		bool flag2 = lzzSuperSkillSign == null;
		if (flag2)
		{
			lzzSuperSkillSign = gameObject.AddComponent<LzzSuperSkillSign>();
		}
		lzzSuperSkillSign.m_nNetId = nNetId;
		lzzSuperSkillSign.fScale = sca;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000A6850 File Offset: 0x000A4A50
	public void ClearLzzEffect()
	{
		Fun.ClearChildObejct(this.autolist);
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000A6860 File Offset: 0x000A4A60
	public void DelOneLzzEffect(int nNetId)
	{
		Transform transform = this.autolist.transform.Find(nNetId.ToString());
		bool flag = transform != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(transform.gameObject);
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000A68A4 File Offset: 0x000A4AA4
	public void OnExitGameMsg(string sMsg1, int nZh, int nTime)
	{
		bool flag = !(this.exitgamemsg == null);
		if (flag)
		{
			this.nExitGameZh = nZh;
			this.nExitGameTime = nTime;
			string lang = Language.GetLang(this.nExitGameZh, new object[]
			{
				Fun.sencondsToHMS_CXP(this.nExitGameTime)
			});
			this.OnShowExitMsg(sMsg1, lang);
			this.fCheckTime = Time.time;
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000A690C File Offset: 0x000A4B0C
	public void OnShowExitMsg(string sMsg1, string sMsg2)
	{
		this.ExitGameMsg1.text = sMsg1;
		this.ExitGameMsg2.text = sMsg2;
		bool flag = !this.exitgamemsg.activeSelf;
		if (flag)
		{
			this.exitgamemsg.SetActive(true);
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000A6958 File Offset: 0x000A4B58
	private void OnCheckExitGame()
	{
		bool flag = this.nExitGameTime >= 0 && !(this.exitgamemsg == null) && this.nExitGameZh != 0 && this.exitgamemsg.activeSelf && Time.time - this.fCheckTime >= 1f;
		if (flag)
		{
			this.fCheckTime = Time.time;
			this.nExitGameTime--;
			bool flag2 = this.nExitGameTime < 0;
			if (flag2)
			{
				this.OnCloseExitMsg();
			}
			else
			{
				this.ExitGameMsg2.text = Language.GetLang(this.nExitGameZh, new object[]
				{
					Fun.sencondsToHMS_CXP(this.nExitGameTime)
				});
			}
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x000A6A0C File Offset: 0x000A4C0C
	public void OnCloseExitMsg()
	{
		this.nExitGameTime = 0;
		this.nExitGameZh = 0;
		this.fCheckTime = 0f;
		bool activeSelf = this.exitgamemsg.activeSelf;
		if (activeSelf)
		{
			this.exitgamemsg.SetActive(false);
		}
		this.ExitGameMsg1.text = string.Empty;
		this.ExitGameMsg2.text = string.Empty;
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x000A6A74 File Offset: 0x000A4C74
	public void OnShowNewPlayerMsg(string sStr)
	{
		bool flag = this._NetLiving != null && !this._NetLiving.isCopy && !this._NetLiving.isDead;
		if (flag)
		{
			this.tNewPlayerMsg.text = sStr;
			base.CancelInvoke("OnClearMsg");
			base.Invoke("OnClearMsg", 5f);
		}
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000A6AD8 File Offset: 0x000A4CD8
	public void OnClearMsg()
	{
		base.CancelInvoke("OnClearMsg");
		this.tNewPlayerMsg.text = string.Empty;
	}

	// Token: 0x04000DF0 RID: 3568
	private Transform blackcar;

	// Token: 0x04000DF1 RID: 3569
	private Text msg;

	// Token: 0x04000DF2 RID: 3570
	private Image fill;

	// Token: 0x04000DF3 RID: 3571
	private Transform tFriendSignNum;

	// Token: 0x04000DF4 RID: 3572
	private Text FriendSignNumValue;

	// Token: 0x04000DF5 RID: 3573
	private Transform tpinsiIco;

	// Token: 0x04000DF6 RID: 3574
	private GameObject revivemsg;

	// Token: 0x04000DF7 RID: 3575
	private Transform tsignshow;

	// Token: 0x04000DF8 RID: 3576
	private Transform tSignClone;

	// Token: 0x04000DF9 RID: 3577
	private Dictionary<int, GameObject> MeTeamMembers;

	// Token: 0x04000DFA RID: 3578
	private GameObject ohpeffect_left;

	// Token: 0x04000DFB RID: 3579
	private PlayerAddEffectCon oAddEffectCon_left;

	// Token: 0x04000DFC RID: 3580
	private GameObject ohpeffect_right;

	// Token: 0x04000DFD RID: 3581
	private PlayerAddEffectCon oAddEffectCon_right;

	// Token: 0x04000DFE RID: 3582
	private GameObject oCloneJiShaMsg;

	// Token: 0x04000DFF RID: 3583
	public GameObject oJiShaMsgList;

	// Token: 0x04000E00 RID: 3584
	private GameObject _HKeyMsg;

	// Token: 0x04000E01 RID: 3585
	public GameObject oCenterMsg;

	// Token: 0x04000E02 RID: 3586
	private Text buffmsgtxt;

	// Token: 0x04000E03 RID: 3587
	private GameObject oClonemsglist;

	// Token: 0x04000E04 RID: 3588
	private GameObject oClonemsgLab;

	// Token: 0x04000E05 RID: 3589
	private GameObject oClonemsgimg;

	// Token: 0x04000E06 RID: 3590
	private GameObject oClonenpc;

	// Token: 0x04000E07 RID: 3591
	private GameObject oNpclist;

	// Token: 0x04000E08 RID: 3592
	private GameObject lookherokey;

	// Token: 0x04000E09 RID: 3593
	private Text lookherokeyText;

	// Token: 0x04000E0A RID: 3594
	private RectTransform lookherokeyBg;

	// Token: 0x04000E0B RID: 3595
	private NetLiving _NetLiving;

	// Token: 0x04000E0C RID: 3596
	private GameObject clonehit;

	// Token: 0x04000E0D RID: 3597
	private GameObject hitlist;

	// Token: 0x04000E0E RID: 3598
	private GameObject useface;

	// Token: 0x04000E0F RID: 3599
	private GameObject useface1;

	// Token: 0x04000E10 RID: 3600
	private GameObject useT;

	// Token: 0x04000E11 RID: 3601
	private GameObject cloneauto;

	// Token: 0x04000E12 RID: 3602
	private GameObject autolist;

	// Token: 0x04000E13 RID: 3603
	private GameObject exitgamemsg;

	// Token: 0x04000E14 RID: 3604
	private Text ExitGameMsg1;

	// Token: 0x04000E15 RID: 3605
	private Text ExitGameMsg2;

	// Token: 0x04000E16 RID: 3606
	private Text tNewPlayerMsg;

	// Token: 0x04000E17 RID: 3607
	private Vector3 vsv = new Vector3(-1f, -1f, -1f);

	// Token: 0x04000E18 RID: 3608
	private string sTopMsg = string.Empty;

	// Token: 0x04000E19 RID: 3609
	private float fTopMsgTime;

	// Token: 0x04000E1A RID: 3610
	private int nExitGameTime;

	// Token: 0x04000E1B RID: 3611
	private int nExitGameZh;

	// Token: 0x04000E1C RID: 3612
	private float fCheckTime;
}
