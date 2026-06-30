using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class PlayerLearnManager : MonoBehaviour
{
	// Token: 0x06001E00 RID: 7680 RVA: 0x00117F00 File Offset: 0x00116100
	public static PlayerLearnManager GetInstance()
	{
		bool flag = PlayerLearnManager.instance == null;
		if (flag)
		{
			PlayerLearnManager.mLearn = new GameObject("PlayerLearn");
			UnityEngine.Object.DontDestroyOnLoad(PlayerLearnManager.mLearn);
			PlayerLearnManager.instance = PlayerLearnManager.mLearn.AddComponent<PlayerLearnManager>();
			PlayerLearnManager.mLearnEff = new GameObject("LearnEff");
			UnityEngine.Object.DontDestroyOnLoad(PlayerLearnManager.mLearnEff);
			PlayerLearnManager.mLearnEff.transform.parent = PlayerLearnManager.mLearn.transform;
		}
		return PlayerLearnManager.instance;
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00117F88 File Offset: 0x00116188
	private void Update()
	{
		bool flag = fGameManager.Heroplayer != null && !fGameManager.Heroplayer.haveDestroy && fGameManager.Heroplayer.playerHaveCreate;
		if (flag)
		{
			bool flag2 = this.needheropos;
			if (flag2)
			{
				this.OnHeroMove();
			}
			bool flag3 = this.needmouse;
			if (flag3)
			{
				this.OnMouseMove();
			}
		}
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00117FE4 File Offset: 0x001161E4
	public void PlayerLearnStart()
	{
		bool flag = this.NowStep == null;
		if (flag)
		{
			fUISoundManager.instance.StopSound("zhujiemian_beijingyinyue", 0);
			bool flag2 = !PlayerLearnManager.mLearn.activeSelf;
			if (flag2)
			{
				PlayerLearnManager.mLearn.SetActive(true);
			}
			bool flag3 = GameRoot.inst != null;
			if (flag3)
			{
				GameRoot.inst.Setting.Use_Key_Setting(new Setting_Entity_Control());
			}
			bool flag4 = base.IsInvoking("LearnTime");
			if (flag4)
			{
				base.CancelInvoke("LearnTime");
			}
			base.Invoke("LearnTime", 1800f);
			this.Init();
			this.DoStep();
		}
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x00118096 File Offset: 0x00116296
	private void LearnTime()
	{
		this.CloseLearn();
		fUIManager.GetInstance().GotoHall(0);
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x001180AC File Offset: 0x001162AC
	private void Init()
	{
		this.m_szLimit = new int[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};
		bool flag = this.szStepID.Count == 0;
		if (flag)
		{
			this.szStepID.AddRange(fGameManager.mloadConfig.AllLearnStepData.Keys);
		}
		this.HideHero = true;
		this.HideDaZhao = true;
		this.HideSkillUI = true;
		this.HideHeroHpUI = true;
		this.needheropos = false;
		this.needmouse = false;
		this.mousexmove = 0f;
		this.mouseymove = 0f;
		this.needPos = Vector3.zero;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x0011814C File Offset: 0x0011634C
	private void DoStep()
	{
		bool flag = this.NowStep == null;
		if (flag)
		{
			this.NowStep = fGameManager.mloadConfig.AllLearnStepData[this.szStepID[0]];
		}
		else
		{
			int num = this.szStepID.IndexOf(this.NowStep.id);
			bool flag2 = num >= this.szStepID.Count - 1;
			if (flag2)
			{
				this.OnAllStepOver();
				return;
			}
			this.NowStep = fGameManager.mloadConfig.AllLearnStepData[this.szStepID[num + 1]];
		}
		this.OnPlayerLimit(this.NowStep.szBanActId);
		bool flag3 = XinShouJiaoXueCon.inst != null;
		if (flag3)
		{
			XinShouJiaoXueCon.inst.StartMsg(this.NowStep);
		}
		bool flag4 = this.NowStep.szOpenUi.Count > 0;
		if (flag4)
		{
			int count = this.NowStep.szOpenUi.Count;
			for (int i = 0; i < count; i++)
			{
				this.OpenUi(this.NowStep.szOpenUi[i]);
			}
		}
		bool flag5 = this.NowStep.szUiEff.Count > 0;
		if (flag5)
		{
			int count2 = this.NowStep.szUiEff.Count;
			for (int j = 0; j < count2; j++)
			{
				ArrayList arrayList = (ArrayList)this.NowStep.szUiEff[j];
				this.AddUiEff((int)arrayList[0]);
			}
		}
		bool flag6 = this.NowStep.szNpc.Count > 0;
		if (flag6)
		{
			int count3 = this.NowStep.szNpc.Count;
			for (int k = 0; k < count3; k++)
			{
				ArrayList arrayList2 = (ArrayList)this.NowStep.szNpc[k];
				this.szNeedNpcIndex.Add((int)arrayList2[0]);
				this.CreateNpc(arrayList2);
			}
		}
		bool flag7 = this.NowStep.comtype == 1;
		if (flag7)
		{
			this.needmouse = true;
		}
		bool flag8 = this.NowStep.comtype == 2;
		if (flag8)
		{
			this.needheropos = true;
			this.needPos = new Vector3((float)this.NowStep.szComParam[0] / 100f, (float)this.NowStep.szComParam[1] / 100f, (float)this.NowStep.szComParam[2] / 100f);
		}
		bool flag9 = this.NowStep.comtype == 0;
		if (flag9)
		{
		}
		bool flag10 = this.NowStep.szMapEff.Count > 0;
		if (flag10)
		{
			int count4 = this.NowStep.szMapEff.Count;
			for (int l = 0; l < count4; l++)
			{
				ArrayList szMapEff = (ArrayList)this.NowStep.szMapEff[l];
				this.LoadMapEff(szMapEff);
			}
		}
		bool flag11 = this.NowStep.sethp > 0;
		if (flag11)
		{
			fGameManager.Heroplayer.SendMessageToServer(5033, new ArrayList
			{
				this.NowStep.sethp,
				this.NowStep.id
			});
		}
		bool isfillpower = this.NowStep.isfillpower;
		if (isfillpower)
		{
			fGameManager.Heroplayer.SendMessageToServer(5034, new ArrayList
			{
				0,
				this.NowStep.id
			});
		}
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x00118530 File Offset: 0x00116730
	public LearnStepData GetNowStep()
	{
		return this.NowStep;
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x00118548 File Offset: 0x00116748
	public void OnHpFull()
	{
		bool flag = this.NowStep != null && this.NowStep.comtype == 4;
		if (flag)
		{
			bool flag2 = NewHeroSkillUi.Inst != null;
			if (flag2)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(1032, false);
			}
			this.OnStepEnd();
		}
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x001185A0 File Offset: 0x001167A0
	private void OpenUi(int nID)
	{
		switch (nID)
		{
		case 1:
		{
			this.HideHero = false;
			bool flag = fGameManager.Heroplayer.userModelFps;
			if (flag)
			{
				fGameManager.Heroplayer.userModelFps.SetActive(true);
			}
			break;
		}
		case 2:
			this.HideHeroHpUI = false;
			break;
		case 3:
			this.HideSkillUI = false;
			break;
		case 4:
		{
			bool flag2 = NewHeroSkillUi.Inst != null;
			if (flag2)
			{
				NewHeroSkillUi.Inst.SetOneSkillActive(1032, true);
			}
			break;
		}
		case 5:
		{
			bool flag3 = NewHeroSkillUi.Inst != null;
			if (flag3)
			{
				NewHeroSkillUi.Inst.SetOneSkillActive(1031, true);
			}
			break;
		}
		case 6:
			this.HideDaZhao = false;
			break;
		}
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x0011866C File Offset: 0x0011686C
	private void AddUiEff(int nID)
	{
		switch (nID)
		{
		case 3:
		{
			bool flag = NewHeroSkillUi.Inst != null;
			if (flag)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(4000, true);
			}
			bool flag2 = NewHeroHpUi.Inst != null;
			if (flag2)
			{
				NewHeroHpUi.Inst.SetUiEffActive(true);
			}
			break;
		}
		case 4:
		{
			bool flag3 = NewHeroSkillUi.Inst != null;
			if (flag3)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(1032, true);
			}
			break;
		}
		case 5:
		{
			bool flag4 = NewHeroSkillUi.Inst != null;
			if (flag4)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(1031, true);
			}
			break;
		}
		case 6:
		{
			this.HideDaZhao = false;
			bool flag5 = NewHeroDaZhaoUi.Inst != null;
			if (flag5)
			{
				NewHeroDaZhaoUi.Inst.SetUiEffActive(true);
			}
			break;
		}
		}
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x00118750 File Offset: 0x00116950
	private void CreateNpc(ArrayList szNpc)
	{
		fGameManager.Heroplayer.Npc_Handle_Component.Create_Npc(-1, (int)szNpc[0], 2, new Vector3((float)szNpc[1] / 100f, (float)szNpc[2] / 100f, (float)szNpc[3] / 100f), new Vector3(0f, (float)szNpc[4], 0f), Vector3.one, 0, Vector3.zero);
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x001187E0 File Offset: 0x001169E0
	private void LoadMapEff(ArrayList szMapEff)
	{
		string str = szMapEff[0].ToString();
		string url = "UI/learn/" + str + ".ab";
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadMapEffOk), new ArrayList
			{
				this.NowStep.id,
				Environment.TickCount,
				szMapEff
			});
		}
		else
		{
			this.LoadMapEffOk(resWWW, new ArrayList
			{
				this.NowStep.id,
				Environment.TickCount,
				szMapEff
			});
		}
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x001188A8 File Offset: 0x00116AA8
	private void LoadMapEffOk(WWW www, object Param)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)Param;
			int num = (int)arrayList[0];
			int num2 = (int)arrayList[1];
			ArrayList arrayList2 = (ArrayList)arrayList[2];
			bool flag2 = this.NowStep != null && this.NowStep.id == num;
			if (flag2)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				gameObject.transform.parent = PlayerLearnManager.mLearnEff.transform;
				gameObject.transform.localPosition = new Vector3((float)arrayList2[1] / 100f, (float)arrayList2[2] / 100f, (float)arrayList2[3] / 100f);
				gameObject.transform.localEulerAngles = Vector3.zero;
				this.m_szMapEffObj.Add(gameObject);
			}
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x001189C8 File Offset: 0x00116BC8
	private void OnHeroMove()
	{
		Vector3 position = fGameManager.Heroplayer.UserControl.transform.position;
		float num = Mathf.Abs(Vector3.Distance(position, this.needPos));
		float num2 = (float)this.NowStep.szComParam[3] / 100f;
		bool flag = num <= num2;
		if (flag)
		{
			this.needheropos = false;
			this.OnStepEnd();
		}
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x00118A38 File Offset: 0x00116C38
	private void OnMouseMove()
	{
		float num = (float)this.NowStep.szComParam[0];
		float num2 = (float)this.NowStep.szComParam[1];
		float value = Input_Manager.Get_Axis("Mouse X") * 1f;
		float value2 = Input_Manager.Get_Axis("Mouse Y") * 1f;
		this.mousexmove += Math.Abs(value);
		this.mouseymove += Math.Abs(value2);
		bool flag = this.mousexmove >= num && this.mouseymove >= num2;
		if (flag)
		{
			this.needmouse = false;
			this.OnStepEnd();
		}
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x00118AEC File Offset: 0x00116CEC
	public bool GetPlayerLimitByBtnName(string sBtnName)
	{
		if (!true)
		{
		}
		uint num = PrivateImplementationDetails.ComputeStringHash(sBtnName);
		bool result;
		if (num <= 2636044757U)
		{
			if (num <= 797409828U)
			{
				if (num != 271751353U)
				{
					if (num == 797409828U)
					{
						if (sBtnName == "Replace_Hero")
						{
							result = this.GetPlayerLimit(11);
							goto IL_168;
						}
					}
				}
				else if (sBtnName == "superskill")
				{
					result = this.GetPlayerLimit(10);
					goto IL_168;
				}
			}
			else if (num != 888928126U)
			{
				if (num == 2636044757U)
				{
					if (sBtnName == "WeaponFirst")
					{
						result = this.GetPlayerLimit(5);
						goto IL_168;
					}
				}
			}
			else if (sBtnName == "WeaponSec")
			{
				result = this.GetPlayerLimit(6);
				goto IL_168;
			}
		}
		else if (num <= 3799924101U)
		{
			if (num != 3440918889U)
			{
				if (num == 3799924101U)
				{
					if (sBtnName == "Check_Hero_Information")
					{
						result = this.GetPlayerLimit(12);
						goto IL_168;
					}
				}
			}
			else if (sBtnName == "Fight")
			{
				result = this.GetPlayerLimit(7);
				goto IL_168;
			}
		}
		else if (num != 3932862956U)
		{
			if (num == 3983195813U)
			{
				if (sBtnName == "Skill1")
				{
					result = this.GetPlayerLimit(8);
					goto IL_168;
				}
			}
		}
		else if (sBtnName == "Skill2")
		{
			result = this.GetPlayerLimit(9);
			goto IL_168;
		}
		result = false;
		IL_168:
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x00118C6C File Offset: 0x00116E6C
	public bool IsInLimit(string sBtnName)
	{
		bool flag = this.m_szLimitName.IndexOf(sBtnName) == -1;
		return !flag;
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x00118C98 File Offset: 0x00116E98
	public bool GetPlayerLimit(int index)
	{
		bool flag = index >= this.m_szLimit.Length;
		return flag || this.m_szLimit[index] == 1;
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x00118CCC File Offset: 0x00116ECC
	private void OnPlayerLimit(List<int> szLimit)
	{
		int num = 0;
		bool flag = szLimit.Count == 0;
		if (flag)
		{
			num = 1;
		}
		for (int i = 0; i < this.m_szLimit.Length; i++)
		{
			this.m_szLimit[i] = num;
		}
		for (int j = 0; j < szLimit.Count; j++)
		{
			int num2 = szLimit[j] - 1;
			this.m_szLimit[num2] = 1;
		}
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x00118D44 File Offset: 0x00116F44
	public void OnLearnNpcCreateOk(Npc_Entity npc, int nNpcID, int nNetID)
	{
		bool flag = this.NowStep != null && this.szNeedNpcIndex.IndexOf(nNpcID) != -1;
		if (flag)
		{
			this.szNeedNpcIndex.Remove(nNpcID);
			this.szNpcList.Add(nNetID, npc);
		}
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x00118D90 File Offset: 0x00116F90
	public void OnNpcDead(int nNpcID, int nNetID, int IsSuper)
	{
		bool flag = this.NowStep == null || !this.szNpcList.ContainsKey(nNetID);
		if (!flag)
		{
			this.szNpcList.Remove(nNetID);
			bool flag2 = this.NowStep.comtype == 3 && NewHeroSkillUi.Inst != null;
			if (flag2)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(1031, false);
			}
			bool flag3 = this.NowStep.comtype == 5;
			int count;
			if (flag3)
			{
				bool flag4 = IsSuper == 1;
				if (flag4)
				{
					this.AddDeadNpc(nNpcID);
				}
				else
				{
					count = this.NowStep.szNpc.Count;
					for (int i = 0; i < count; i++)
					{
						ArrayList arrayList = (ArrayList)this.NowStep.szNpc[i];
						bool flag5 = (int)arrayList[0] == nNpcID;
						if (flag5)
						{
							this.szNeedNpcIndex.Add(nNpcID);
							this.CreateNpc(arrayList);
							break;
						}
					}
				}
			}
			else
			{
				this.AddDeadNpc(nNpcID);
			}
			count = this.NowStep.szComParam.Count;
			for (int j = 0; j < count; j += 2)
			{
				int key = int.Parse(this.NowStep.szComParam[j].ToString());
				int num = int.Parse(this.NowStep.szComParam[j + 1].ToString());
				bool flag6 = !this.szDeadNpc.ContainsKey(key) || this.szDeadNpc[key] < num;
				if (flag6)
				{
					return;
				}
			}
			bool flag7 = this.NowStep.comtype == 5 && NewHeroDaZhaoUi.Inst != null;
			if (flag7)
			{
				NewHeroDaZhaoUi.Inst.SetUiEffActive(false);
			}
			this.OnStepEnd();
		}
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x00118F78 File Offset: 0x00117178
	private void AddDeadNpc(int nNpcID)
	{
		bool flag = this.szDeadNpc.ContainsKey(nNpcID);
		if (flag)
		{
			Dictionary<int, int> dictionary = this.szDeadNpc;
			int num = dictionary[nNpcID];
			dictionary[nNpcID] = num + 1;
		}
		else
		{
			this.szDeadNpc.Add(nNpcID, 1);
		}
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x00118FC4 File Offset: 0x001171C4
	public void OnStepEnd()
	{
		this.CleanStep();
		bool flag = this.NowStep.szUiEff.Count > 0;
		if (flag)
		{
			bool flag2 = NewHeroHpUi.Inst != null;
			if (flag2)
			{
				NewHeroHpUi.Inst.SetUiEffActive(false);
			}
			bool flag3 = NewHeroSkillUi.Inst != null;
			if (flag3)
			{
				NewHeroSkillUi.Inst.AddEffOnSkill(4000, false);
			}
		}
		this.DoStep();
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x00119038 File Offset: 0x00117238
	private void CleanStep()
	{
		fUIManager.OnClearObj(this.m_szMapEffObj);
		this.szNeedNpcIndex.Clear();
		bool flag = XinShouJiaoXueCon.inst != null;
		if (flag)
		{
			XinShouJiaoXueCon.inst.StepOver();
		}
		bool flag2 = this.szNpcList.Count > 0;
		if (flag2)
		{
			List<int> list = new List<int>();
			list.AddRange(this.szNpcList.Keys);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				this.szNpcList[list[i]].Suicide();
			}
		}
		this.szNpcList.Clear();
		this.szDeadNpc.Clear();
		this.szNeedNpcIndex.Clear();
		bool flag3 = base.IsInvoking("SuperPowerTime");
		if (flag3)
		{
			base.CancelInvoke("SuperPowerTime");
		}
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x00119124 File Offset: 0x00117324
	public void CloseLearn()
	{
		this.CleanStep();
		this.NowStep = null;
		this.HideHero = true;
		bool flag = NewHeroSkillUi.Inst != null;
		if (flag)
		{
			NewHeroSkillUi.Inst.CleanSingleEff();
		}
		bool flag2 = NewHeroDaZhaoUi.Inst != null;
		if (flag2)
		{
			NewHeroDaZhaoUi.Inst.CleanSingleEff();
		}
		bool flag3 = base.IsInvoking("LearnTime");
		if (flag3)
		{
			base.CancelInvoke("LearnTime");
		}
		PlayerLearnManager.mLearn.SetActive(false);
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x001191A8 File Offset: 0x001173A8
	private void OnAllStepOver()
	{
		this.NowStep = null;
		this.HideHero = true;
		bool flag = NewHeroSkillUi.Inst != null;
		if (flag)
		{
			NewHeroSkillUi.Inst.CleanSingleEff();
		}
		bool flag2 = NewHeroDaZhaoUi.Inst != null;
		if (flag2)
		{
			NewHeroDaZhaoUi.Inst.CleanSingleEff();
		}
		bool flag3 = base.IsInvoking("LearnTime");
		if (flag3)
		{
			base.CancelInvoke("LearnTime");
		}
		XinShouJiaoXueCon.inst.ShowEndEff();
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x00119224 File Offset: 0x00117424
	public void OnSuperPower(string sEndTime)
	{
		bool flag = !(sEndTime == string.Empty) && !base.IsInvoking("SuperPowerTime");
		if (flag)
		{
			DateTime d = DateTime.Parse(sEndTime);
			int num = (int)(d - Fun.GetServerDate()).TotalSeconds;
			bool flag2 = num > 0;
			if (flag2)
			{
				base.Invoke("SuperPowerTime", (float)(num + 2));
			}
		}
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x00119290 File Offset: 0x00117490
	private void SuperPowerTime()
	{
		bool flag = this.NowStep != null && this.NowStep.comtype == 5;
		if (flag)
		{
			fGameManager.Heroplayer.SendMessageToServer(5034, new ArrayList
			{
				0,
				this.NowStep.id
			});
		}
	}

	// Token: 0x04001B8A RID: 7050
	private static PlayerLearnManager instance;

	// Token: 0x04001B8B RID: 7051
	private static GameObject mLearn;

	// Token: 0x04001B8C RID: 7052
	private static GameObject mLearnEff;

	// Token: 0x04001B8D RID: 7053
	private int[] m_szLimit = new int[]
	{
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04001B8E RID: 7054
	private List<string> m_szLimitName = new List<string>
	{
		"WeaponFirst",
		"WeaponSec",
		"Fight",
		"Skill1",
		"Skill2",
		"superskill",
		"Replace_Hero",
		"Check_Hero_Information"
	};

	// Token: 0x04001B8F RID: 7055
	public bool HideHero = true;

	// Token: 0x04001B90 RID: 7056
	public bool HideDaZhao = true;

	// Token: 0x04001B91 RID: 7057
	public bool HideSkillUI = true;

	// Token: 0x04001B92 RID: 7058
	public bool HideHeroHpUI = true;

	// Token: 0x04001B93 RID: 7059
	private LearnStepData NowStep;

	// Token: 0x04001B94 RID: 7060
	public bool needheropos;

	// Token: 0x04001B95 RID: 7061
	public bool needmouse;

	// Token: 0x04001B96 RID: 7062
	public float mousexmove;

	// Token: 0x04001B97 RID: 7063
	public float mouseymove;

	// Token: 0x04001B98 RID: 7064
	public Vector3 needPos;

	// Token: 0x04001B99 RID: 7065
	private List<int> szStepID = new List<int>();

	// Token: 0x04001B9A RID: 7066
	private List<GameObject> m_szMapEffObj = new List<GameObject>();

	// Token: 0x04001B9B RID: 7067
	private List<int> szNeedNpcIndex = new List<int>();

	// Token: 0x04001B9C RID: 7068
	private Dictionary<int, Npc_Entity> szNpcList = new Dictionary<int, Npc_Entity>();

	// Token: 0x04001B9D RID: 7069
	private Dictionary<int, int> szDeadNpc = new Dictionary<int, int>();
}
