using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class CPlayerSkill : MonoBehaviour
{
	// Token: 0x060006A1 RID: 1697 RVA: 0x00039A20 File Offset: 0x00037C20
	public void InitPSkills(NetLiving hp)
	{
		this.hero = hp;
		this.defaultReadySkill = 0;
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00039A34 File Offset: 0x00037C34
	public void ClearSkill()
	{
		bool flag = this.defaultReadySkill != 0;
		if (flag)
		{
			this.ReadyItemOp(this.defaultReadySkill, false, false);
		}
		this.skillsDict.Clear();
		this.haveCDSkills = new ArrayList();
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00039A78 File Offset: 0x00037C78
	public void destroyReadyItem()
	{
		int[] array = new int[this.skillsDict.Count];
		this.skillsDict.Keys.CopyTo(array, 0);
		for (int i = 0; i < array.Length; i++)
		{
			this.hero.skillInfo.DestroyReadyItem(array[i]);
		}
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00039AD4 File Offset: 0x00037CD4
	public int GetKeySkill(string key)
	{
		bool flag = this.skillsDict.Count == 0;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = !this.skillKeys.ContainsKey(key);
			if (flag2)
			{
				result = -1;
			}
			else
			{
				result = this.skillKeys[key];
			}
		}
		return result;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00039B24 File Offset: 0x00037D24
	public void DoSomeOne(string key, int robot = 0)
	{
		bool flag = this.skillsDict.Count != 0 && this.skillKeys.ContainsKey(key);
		if (flag)
		{
			int idx = this.skillKeys[key];
			this.DoSkill(idx, robot);
		}
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00039B6C File Offset: 0x00037D6C
	public int CanDoSomeOne(string key)
	{
		bool flag = this.skillsDict.Count == 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = !this.skillKeys.ContainsKey(key);
			if (flag2)
			{
				result = 0;
			}
			else
			{
				int idx = this.skillKeys[key];
				result = this.CanDo(idx);
			}
		}
		return result;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00039BC4 File Offset: 0x00037DC4
	public void SkillInput(int idx)
	{
		bool flag = this.hero.skillEffectDict.ContainsKey(idx);
		if (flag)
		{
			this.hero.skillEffectDict[idx].OneSkillInput();
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00039C00 File Offset: 0x00037E00
	public int CanDo(int idx)
	{
		bool flag = this.hero != null && this.hero.isDead;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = !this.hero.playerHaveCreate;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = this.hero.playerCommon.m_nJiangzhi == 1;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					bool flag4 = this.hero.playerCommon.m_nChenMo == 1;
					if (flag4)
					{
						result = 0;
					}
					else
					{
						bool flag5 = this.hero.playerCommon.m_nDongjie == 1 || this.hero.playerCommon.m_nHunMi == 1;
						if (flag5)
						{
							result = 0;
						}
						else
						{
							bool flag6 = this.hero.CheckActiveStat();
							if (flag6)
							{
								result = 0;
							}
							else
							{
								bool flag7 = this.hero.netobject_type != 6 && ((HeroBaseMe)this.hero.heroBase).doSkillTime > Time.time;
								if (flag7)
								{
									result = 0;
								}
								else
								{
									bool flag8 = !this.hero.CanHuDong;
									if (flag8)
									{
										result = 0;
									}
									else
									{
										bool flag9 = this.skillsDict.ContainsKey(idx);
										if (flag9)
										{
											CPSkillStruct cpskillStruct = this.skillsDict[idx];
											bool flag10 = !this.hero.skillEffectDict.ContainsKey(idx);
											if (flag10)
											{
												return 0;
											}
											bool flag11 = cpskillStruct.mClientDoTime > Time.time;
											if (flag11)
											{
												return 0;
											}
											bool flag12 = !this.hero.skillEffectDict[idx].OneSkillCanDo();
											if (flag12)
											{
												return 0;
											}
											bool flag13 = cpskillStruct.m_nDoNumberNow > 0 || cpskillStruct.doKey == "0";
											if (flag13)
											{
												return 1;
											}
										}
										result = 0;
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00039DE0 File Offset: 0x00037FE0
	public CPSkillStruct GetSkillData(int idx)
	{
		bool flag = this.skillsDict.ContainsKey(idx);
		CPSkillStruct result;
		if (flag)
		{
			result = this.skillsDict[idx];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00039E14 File Offset: 0x00038014
	public void InitSkill(int idx, ArrayList list)
	{
		bool flag = !this.skillsDict.ContainsKey(idx);
		CPSkillStruct cpskillStruct;
		if (flag)
		{
			bool flag2 = !fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(idx);
			if (flag2)
			{
				Debug.LogError(string.Format("配表没这个玩家技能:{0}", idx));
				return;
			}
			PlayerSkillData playerSkillData = fGameManager.mloadConfig.AllPlayerSkill[idx];
			cpskillStruct = new CPSkillStruct();
			cpskillStruct.idx = idx;
			cpskillStruct.name = playerSkillData.name;
			cpskillStruct.cd = 100000;
			cpskillStruct.icon = playerSkillData.icon;
			cpskillStruct.desc = playerSkillData.desc;
			cpskillStruct._skilldata = playerSkillData;
			cpskillStruct.doKey = playerSkillData.dokey;
			cpskillStruct.readyItem = playerSkillData.readyItem;
			cpskillStruct.triggerWeapon = playerSkillData.triggerWeapon;
			cpskillStruct.xingtai = playerSkillData.xingtai;
			cpskillStruct.m_nDoNumberMax = playerSkillData.maxSkillNumber;
			cpskillStruct.readySwitch = playerSkillData.readySwitch;
			this.skillsDict.Add(idx, cpskillStruct);
		}
		else
		{
			cpskillStruct = this.skillsDict[idx];
			bool flag3 = this.hero == fGameManager.Heroplayer && cpskillStruct.cd > 1 && cpskillStruct.m_nDoNumberNow < (int)list[2];
			if (flag3)
			{
				((CPlayerSoundMe)this.hero.playerSound).PlayCDFinishSound();
			}
		}
		bool flag4 = cpskillStruct.m_nDoNumberNow < (int)list[2];
		if (flag4)
		{
			cpskillStruct.cdCheckTime = 0f;
		}
		cpskillStruct.m_nDoNumberNow = (int)list[2];
		cpskillStruct.m_nItemNumberNow = (int)list[3];
		cpskillStruct.m_nActive = (int)list[4];
		cpskillStruct.havecd = (int)list[5];
		cpskillStruct.m_nNpcNum = (int)list[7];
		cpskillStruct.m_nSKLongTime = (int)list[9];
		cpskillStruct.cd = (int)list[10];
		bool isCopy = this.hero.isCopy;
		if (isCopy)
		{
			cpskillStruct.beginTime = Fun.Now.AddMilliseconds((double)cpskillStruct.havecd);
		}
		else
		{
			cpskillStruct.beginTime = Fun.GetServerDate().AddMilliseconds((double)cpskillStruct.havecd);
		}
		cpskillStruct.mClientDoTime = 0f;
		bool flag5 = this.hero.isMainPlayer || this.hero.netobject_type == 6;
		if (flag5)
		{
			int num = DtsBatteManager.GetInstance().CanDtsScnSkill(this.hero, idx);
			bool flag6 = num != -1;
			if (flag6)
			{
				bool flag7 = cpskillStruct.m_nDoNumberNow == 0;
				if (flag7)
				{
					bool flag8 = !this.haveCDSkills.Contains(idx);
					if (flag8)
					{
						this.haveCDSkills.Add(cpskillStruct.idx);
					}
				}
				else
				{
					bool flag9 = this.haveCDSkills.Contains(idx);
					if (flag9)
					{
						this.haveCDSkills.Remove(cpskillStruct.idx);
					}
				}
			}
			else
			{
				bool flag10 = num == -1;
				if (flag10)
				{
					bool flag11 = cpskillStruct.m_nDoNumberNow < cpskillStruct.m_nDoNumberMax;
					if (flag11)
					{
						bool flag12 = !this.haveCDSkills.Contains(idx);
						if (flag12)
						{
							this.haveCDSkills.Add(cpskillStruct.idx);
						}
					}
					else
					{
						bool flag13 = this.haveCDSkills.Contains(idx);
						if (flag13)
						{
							this.haveCDSkills.Remove(cpskillStruct.idx);
						}
					}
				}
			}
		}
		bool flag14 = gameuiCon.inst != null && CFilmManager.Instance.filmParentNetId(this.hero) == this.hero.getNetid();
		if (flag14)
		{
			gameuiCon.inst.NewMainHeroSkill.InitSkill(idx);
		}
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0003A20C File Offset: 0x0003840C
	public void SwitchSkillGroup(int _group)
	{
		this.skillKeys = new Dictionary<string, int>();
		foreach (int num in this.skillsDict.Keys)
		{
			CPSkillStruct cpskillStruct = this.skillsDict[num];
			bool flag = cpskillStruct.xingtai == _group && !(cpskillStruct.doKey == "0");
			if (flag)
			{
				this.skillKeys.Add(cpskillStruct.doKey, num);
			}
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0003A2B4 File Offset: 0x000384B4
	public void ReadyItemOp(int idx, bool show, bool doSkill = false)
	{
		CPSkillStruct cpskillStruct = this.skillsDict[idx];
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		((CWeaponControlMe)player.wc).cancelFillCartridge();
		bool flag = player.skillEffectDict.ContainsKey(idx);
		if (flag)
		{
			player.skillEffectDict[idx].readyItemSwitch(show, doSkill);
		}
		if (show)
		{
			cpskillStruct.ReadyKeyOk = true;
			this.defaultReadySkill = idx;
			player.skillInfo.ShowReadyItem(idx);
			player.bePlayActive = false;
			player.playerZhunxingMe.readyShow();
			bool flag2 = cpskillStruct._skilldata.readyAnimation.Length > 1;
			if (flag2)
			{
				player.SetAnimator(cpskillStruct._skilldata.readyAnimation, 0, true, 2);
			}
		}
		else
		{
			cpskillStruct.ReadyKeyOk = false;
			this.defaultReadySkill = 0;
			player.skillInfo.DisableReadyItem(idx);
			bool flag3 = player.playerCommon.m_nHunMi == 0;
			if (flag3)
			{
				player.bePlayActive = true;
			}
			player.playerZhunxingMe.readyDisable();
			bool flag4 = player.skillInfo.getReadyItem(idx) != null && !doSkill;
			if (flag4)
			{
				player.skillInfo.getReadyItem(idx).beReady = false;
			}
			bool flag5 = cpskillStruct._skilldata.readyAnimation.Length > 1 && !doSkill;
			if (flag5)
			{
				player.SetAnimator(cpskillStruct._skilldata.readyAnimation, 0, false, 2);
			}
			((CWeaponControlMe)player.wc).CheckMustFillCartridge();
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0003A448 File Offset: 0x00038648
	public void DoSkill(int idx, int robot = 0)
	{
		bool flag = !this.skillsDict.ContainsKey(idx);
		if (!flag)
		{
			CPSkillStruct cpskillStruct = this.skillsDict[idx];
			this.SkillInput(idx);
			bool flag2 = this.CanDo(idx) == 0 && robot == 0;
			if (flag2)
			{
				fUISoundManager.instance.PlayUISound("ui_skill_cd", 0, null, 20);
			}
			else
			{
				bool flag3 = cpskillStruct.readySwitch != 0 && this.hero.netobject_type != 6;
				if (flag3)
				{
					bool flag4 = !cpskillStruct.ReadyKeyOk;
					if (flag4)
					{
						this.ReadyItemOp(idx, true, false);
						return;
					}
					CPlaceItem readyItem = this.hero.skillInfo.getReadyItem(idx);
					bool flag5 = readyItem != null && !readyItem.beReady;
					if (flag5)
					{
						return;
					}
					bool flag6 = this.hero.skillInfo.getReadyItem(idx) != null && !this.hero.tChar.animState.onGround && !this.hero.skillInfo.getReadyItem(idx).canReleaseOnAir;
					if (flag6)
					{
						this.ReadyItemOp(idx, false, false);
						return;
					}
					this.ReadyItemOp(idx, false, true);
				}
				Vector3 vector = this.hero.getMubiao(cpskillStruct._skilldata.targettype);
				bool flag7 = this.hero.skillInfo.getReadyItem(idx) != null;
				if (flag7)
				{
					bool flag8 = !this.hero.skillInfo.getReadyItem(idx).beReady;
					if (flag8)
					{
						return;
					}
					vector = this.hero.skillInfo.getReadyItem(idx).ThisGameObjTrans.transform.position;
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				bool flag9 = cpskillStruct._skilldata.taregetlock != 0;
				if (flag9)
				{
					GameObject mubiaoObj = this.hero.getMubiaoObj(cpskillStruct._skilldata.targettype, true);
					bool flag10 = mubiaoObj == null || !mubiaoObj.tag.Contains("can_damage");
					if (flag10)
					{
						bool flag11 = gameuiCon.inst != null && this.hero.netobject_type != 6;
						if (flag11)
						{
							gameuiCon.inst.mainMsgUI.SkillMsg(Language.GetLang(2031, new object[0]));
						}
						return;
					}
					PublicInfo component = mubiaoObj.transform.root.gameObject.GetComponent<PublicInfo>();
					bool flag12 = component == null;
					if (flag12)
					{
						return;
					}
					NetLiving netObject = component.NetObject;
					bool flag13 = !netObject.netinitok;
					if (flag13)
					{
						return;
					}
					bool flag14 = cpskillStruct._skilldata.targettype != 0;
					if (flag14)
					{
						bool flag15 = cpskillStruct._skilldata.targettype == 1;
						if (flag15)
						{
							bool flag16 = !LayerManager.CanDamage(this.hero.nForce, this.hero.bePlayer, netObject.nForce, netObject.bePlayer);
							if (flag16)
							{
								bool flag17 = gameuiCon.inst != null && this.hero.netobject_type != 6;
								if (flag17)
								{
									gameuiCon.inst.mainMsgUI.SkillMsg(Language.GetLang(2033, new object[0]));
								}
								return;
							}
						}
						else
						{
							bool flag18 = cpskillStruct._skilldata.targettype != 2;
							if (flag18)
							{
								return;
							}
							bool flag19 = LayerManager.CanDamage(this.hero.nForce, this.hero.bePlayer, netObject.nForce, netObject.bePlayer);
							if (flag19)
							{
								bool flag20 = gameuiCon.inst != null && this.hero.netobject_type != 6;
								if (flag20)
								{
									gameuiCon.inst.mainMsgUI.SkillMsg(Language.GetLang(2032, new object[0]));
								}
								return;
							}
						}
					}
					bool flag21 = cpskillStruct._skilldata.taregetradius > 0f;
					if (flag21)
					{
						float num = Vector3.Distance(netObject.thisModel.transform.position, this.hero.thisModel.transform.position);
						bool flag22 = num > cpskillStruct._skilldata.taregetradius;
						if (flag22)
						{
							bool flag23 = gameuiCon.inst != null && this.hero.netobject_type != 6;
							if (flag23)
							{
								gameuiCon.inst.mainMsgUI.SkillMsg(Language.GetLang(2034, new object[0]));
							}
							return;
						}
					}
					arrayList2.Add(netObject.getNetid());
					arrayList.Add(netObject);
				}
				else
				{
					bool flag24 = cpskillStruct._skilldata.targettype == 0 || cpskillStruct._skilldata.targettype == 1;
					if (flag24)
					{
						arrayList2.Add(this.hero.getNetid());
						arrayList.Add(this.hero);
					}
				}
				bool flag25 = (cpskillStruct._skilldata.mapeffect_idx > 0 || cpskillStruct._skilldata.buff_idx > 0) && cpskillStruct._skilldata.taregetradius > 0f;
				if (flag25)
				{
					int layerMask = (cpskillStruct._skilldata.targettype == 1) ? LayerManager.EnemyLayer(this.hero.getAttackForce()) : ((cpskillStruct._skilldata.targettype != 2) ? LayerManager.GetLivingAllLayer() : LayerManager.FriendLayer(this.hero.getAttackForce()));
					Collider[] array = Physics.OverlapSphere(this.hero.thisModel.transform.position, cpskillStruct._skilldata.taregetradius, layerMask);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag26 = !array[i].tag.Contains("can_damage");
						if (!flag26)
						{
							PublicInfo component2 = array[i].transform.root.gameObject.GetComponent<PublicInfo>();
							bool flag27 = !(component2 == null);
							if (flag27)
							{
								NetLiving netObject2 = component2.NetObject;
								bool flag28 = netObject2.getNetid() != this.hero.getNetid() && !netObject2.isDead && !arrayList2.Contains(netObject2.getNetid());
								if (flag28)
								{
									arrayList2.Add(netObject2.getNetid());
									arrayList.Add(netObject2);
								}
							}
						}
					}
				}
				cpskillStruct.mClientDoTime = Time.time + 1f;
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(idx);
				ArrayList value = new ArrayList
				{
					(int)(vector.x * 100f),
					(int)(vector.y * 100f),
					(int)(vector.z * 100f)
				};
				arrayList3.Add(value);
				arrayList3.Add(arrayList2);
				bool flag29 = robot == 0;
				if (flag29)
				{
					((HeroBaseMe)this.hero.heroBase).doSkillTime = Time.time + 1f;
					arrayList3.Add(-1);
					this.hero.SendMessageToServer(31103, arrayList3);
				}
				else
				{
					arrayList3.Add(this.hero.getNetid());
					bool flag30 = ((RobotAI)this.hero).SyncNetObj != null && ((RobotAI)this.hero).needSendToServer;
					if (flag30)
					{
						((RobotAI)this.hero).SendMsgToServer(31103, arrayList3);
					}
				}
			}
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0003AC48 File Offset: 0x00038E48
	public void OnUpdateItemNum(int nSkIll, int nNum)
	{
		bool flag = this.skillsDict.ContainsKey(nSkIll);
		if (flag)
		{
			CPSkillStruct cpskillStruct = this.skillsDict[nSkIll];
			cpskillStruct.m_nNpcNum = nNum;
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0003AC80 File Offset: 0x00038E80
	private void FixedUpdate()
	{
		bool flag = this.hero == null || (!this.hero.isMainPlayer && this.hero.netobject_type != 6);
		if (!flag)
		{
			float time = Time.time;
			bool flag2 = time < this.cdCheckTimeOut;
			if (!flag2)
			{
				this.cdCheckTimeOut = time + 0.05f;
				bool flag3 = this.hero.isCopy || this.haveCDSkills.Count == 0;
				if (!flag3)
				{
					DateTime serverDate = Fun.GetServerDate();
					for (int i = 0; i < this.haveCDSkills.Count; i++)
					{
						int num = (int)this.haveCDSkills[i];
						CPSkillStruct cpskillStruct = this.skillsDict[num];
						bool flag4 = time < cpskillStruct.cdCheckTime;
						if (!flag4)
						{
							bool flag5 = cpskillStruct.m_nDoNumberNow >= cpskillStruct.m_nDoNumberMax;
							if (flag5)
							{
								this.haveCDSkills.RemoveAt(i);
								i--;
							}
							else
							{
								bool flag6 = serverDate > cpskillStruct.beginTime && DtsBatteManager.GetInstance().CanDtsScnSkill(this.hero, num) != 0;
								if (flag6)
								{
									cpskillStruct.cdCheckTime = time + 1f;
									bool flag7 = this.hero.netobject_type != 6;
									if (flag7)
									{
										this.hero = fGameManager.GetInstance().getPlayer();
										this.hero.SendMessageToServer(31104, new ArrayList
										{
											num,
											0,
											-1
										});
									}
									else
									{
										bool needSendToServer = this.hero.needSendToServer;
										if (needSendToServer)
										{
											((RobotAI)this.hero).SendMsgToServer(31104, new ArrayList
											{
												num,
												0,
												this.hero.getNetid()
											});
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040004D3 RID: 1235
	public Dictionary<int, CPSkillStruct> skillsDict = new Dictionary<int, CPSkillStruct>();

	// Token: 0x040004D4 RID: 1236
	public Dictionary<string, int> skillKeys = new Dictionary<string, int>();

	// Token: 0x040004D5 RID: 1237
	private ArrayList haveCDSkills = new ArrayList();

	// Token: 0x040004D6 RID: 1238
	private NetLiving hero;

	// Token: 0x040004D7 RID: 1239
	public int defaultReadySkill;

	// Token: 0x040004D8 RID: 1240
	private float cdCheckTimeOut;
}
