using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class TaskManager
{
	// Token: 0x06002ACD RID: 10957 RVA: 0x00178DBB File Offset: 0x00176FBB
	public TaskManager()
	{
		this.taskdata = new TaskDataManager();
	}

	// Token: 0x06002ACE RID: 10958 RVA: 0x00178DD0 File Offset: 0x00176FD0
	public static TaskManager GetInstance()
	{
		bool flag = TaskManager.mInst == null;
		if (flag)
		{
			TaskManager.mInst = new TaskManager();
		}
		return TaskManager.mInst;
	}

	// Token: 0x06002ACF RID: 10959 RVA: 0x00178E00 File Offset: 0x00177000
	public void OnServerMsg(int nNetId, ArrayList arr)
	{
		if (nNetId != 71002)
		{
			switch (nNetId)
			{
			case 71007:
				this.OnTaskRewardTip(arr);
				break;
			case 71008:
				this.OnTaskTiShi(arr);
				break;
			case 71009:
				this.UpdateChapterData(arr);
				break;
			}
		}
		else
		{
			this.UpdateTaskData(arr);
		}
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x00178E60 File Offset: 0x00177060
	private void OnTaskTiShi(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		bool flag = num == -1;
		if (flag)
		{
			TipManager.GetInstance().ShowComTipMsg(Language.GetLang(8009, new object[0]), Language.GetLang(38300, new object[0]), Language.GetLang(8012, new object[0]), string.Empty, string.Empty, null, null, null, null);
		}
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x00178ED0 File Offset: 0x001770D0
	public void UpdateTaskData(ArrayList arr)
	{
		int count = arr.Count;
		for (int i = 0; i < count; i += 2)
		{
			taskinfo taskinfo = new taskinfo();
			taskinfo.inittask((ArrayList)arr[i + 1]);
			bool flag = fGameManager.mloadConfig.AllTaskObj.ContainsKey(taskinfo.m_nTaskId) && !fGameManager.mloadConfig.AllTaskObj[taskinfo.m_nTaskId].m_nIsHide;
			if (flag)
			{
				int nUpdateType = (int)arr[i];
				switch (nUpdateType)
				{
				case 0:
					this.taskdata.OnDelTask(taskinfo);
					break;
				case 1:
					this.taskdata.OnAddTask(taskinfo);
					break;
				case 2:
					this.taskdata.OnUpdateTask(taskinfo);
					break;
				case 3:
					goto IL_EB;
				case 4:
					this.taskdata.OnAddTask(taskinfo);
					this.taskdata.OnDelOldDailyTask(taskinfo.m_nFlogId);
					break;
				default:
					goto IL_EB;
				}
				IL_104:
				bool flag2 = TaskUICon.inst != null;
				if (flag2)
				{
					TaskUICon.inst.UpdateTaskData(taskinfo, nUpdateType);
				}
				goto IL_126;
				IL_EB:
				Debug.LogError("更新类型错误===" + nUpdateType.ToString());
				goto IL_104;
			}
			IL_126:;
		}
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x00179018 File Offset: 0x00177218
	public void UpdateChapterData(ArrayList arr)
	{
		int count = arr.Count;
		for (int i = 0; i < count; i += 2)
		{
			Chapterinfo chapterinfo = new Chapterinfo();
			chapterinfo.InitChapter((ArrayList)arr[i + 1]);
			int num = (int)arr[i];
			switch (num)
			{
			case 5:
				this.taskdata.OnAddChapter(chapterinfo);
				break;
			case 6:
				this.taskdata.OnAddChapter(chapterinfo);
				break;
			case 7:
				this.taskdata.OnUpdateChapter(chapterinfo);
				break;
			default:
				Debug.LogError("更新类型错误===" + num.ToString());
				break;
			}
		}
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x001790D4 File Offset: 0x001772D4
	private void OnTaskRewardTip(ArrayList arr)
	{
		string text = string.Empty;
		ArrayList arrayList = (ArrayList)arr[0];
		int num = (int)arr[1];
		int num2 = (int)arr[2];
		int num3 = (int)arr[3];
		int key = (int)arr[4];
		int count = arrayList.Count;
		TaskBaseData taskBaseData = fGameManager.mloadConfig.AllTaskObj[key];
		string text2 = this.GetsTarget(taskBaseData, new taskinfo
		{
			m_nTaskState = 0
		});
		text2 = "<color=ffffff><size=20>" + text2 + "</size></color>";
		string title = (taskBaseData.m_nTaskType == 2) ? ("<color=fff0c8><size=32>" + Language.GetLang(38301, new object[0]) + "</size></color>") : ((taskBaseData.m_nTaskType != 1) ? ("<color=fff0c8><size=32>" + Language.GetLang(38303, new object[0]) + "</size></color>") : ("<color=fff0c8><size=32>" + Language.GetLang(38302, new object[0]) + "</size></color>"));
		for (int i = 0; i < count; i += 3)
		{
			int num4 = (int)arrayList[i + 1];
			int key2 = (int)arrayList[i];
			bool flag = num4 == 0;
			if (!flag)
			{
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key2];
				bool flag2 = itemData.m_nHeroId <= 0;
				if (flag2)
				{
					bool flag3 = text != string.Empty;
					if (flag3)
					{
						text += ",";
					}
					text = text + itemData.m_sName + "*" + num4.ToString();
				}
			}
		}
		bool flag4 = num > 0;
		if (flag4)
		{
			bool flag5 = text != string.Empty;
			if (flag5)
			{
				text += ",";
			}
			text = text + Language.GetLang(39521, new object[0]) + " :" + num.ToString();
		}
		bool flag6 = num2 > 0;
		if (flag6)
		{
			bool flag7 = text != string.Empty;
			if (flag7)
			{
				text += ",";
			}
			text = text + Language.GetLang(39523, new object[0]) + " :" + num2.ToString();
		}
		bool flag8 = num3 > 0;
		if (flag8)
		{
			bool flag9 = text != string.Empty;
			if (flag9)
			{
				text += ",";
			}
			text = text + Language.GetLang(39529, new object[0]) + " :" + num3.ToString();
		}
		bool flag10 = !(text == string.Empty);
		if (flag10)
		{
			text2 = text2 + "\n<color=#FFAE00FF>" + text + "</color>";
			TipManager.GetInstance().ShowComTipMsg(title, text2, Language.GetLang(8012, new object[0]), string.Empty, null, null, null, null, string.Empty, false, null, 3, string.Empty, string.Empty, string.Empty);
		}
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x001793F4 File Offset: 0x001775F4
	public void AutoFindTarget(taskinfo tf, string sUrl, int IsBreakGuaJi)
	{
		string text = string.Empty;
		bool flag = tf == null || PlayerDataManager.getInstance().playerCommon.m_nPlayerState == 0;
		if (!flag)
		{
			TaskBaseData taskBaseData = fGameManager.mloadConfig.AllTaskObj[tf.m_nTaskId];
			bool flag2 = taskBaseData == null || tf.m_nTaskState == 0;
			if (!flag2)
			{
				bool flag3 = sUrl == string.Empty;
				if (flag3)
				{
					switch (tf.m_nTaskState)
					{
					case 0:
						text = taskBaseData.sbaseXml.GetAttribute("compdesc");
						text = RegexTxt.GetTaskLinkTxt(text);
						break;
					case 1:
						text = taskBaseData.sbaseXml.GetAttribute("fulldesc");
						text = RegexTxt.GetTaskLinkTxt(text);
						break;
					case 2:
						text = taskBaseData.sbaseXml.GetAttribute("desc");
						text = RegexTxt.GetTaskLinkManyTargetTxt(text, tf.sz_targetArr);
						break;
					}
				}
				else
				{
					text = sUrl;
				}
				bool flag4 = !(text == string.Empty);
				if (flag4)
				{
					text = text + "|" + tf.m_nTaskId.ToString();
					string[] array = text.Split(new char[]
					{
						'|'
					});
					bool flag5 = array.Length >= 7;
					if (flag5)
					{
						this.FindTextLind(text);
					}
				}
			}
		}
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x00179544 File Offset: 0x00177744
	public string GetsTarget(TaskBaseData BaseXml, taskinfo tf)
	{
		string text = string.Empty;
		switch (tf.m_nTaskState)
		{
		case 0:
			text = BaseXml.sbaseXml.GetAttribute("compdesc");
			text = RegexTxt.taskRegTxt(text, RegexTxt.taskspattern, false);
			break;
		case 1:
			text = BaseXml.sbaseXml.GetAttribute("accdesc");
			text = RegexTxt.taskRegTxt(text, RegexTxt.taskspattern, false);
			break;
		case 2:
			text = BaseXml.sbaseXml.GetAttribute("desc");
			text = RegexTxt.taskRegTxtFroManyTartet(text, RegexTxt.taskspattern, tf.sz_targetArr, tf);
			break;
		case 3:
			text = BaseXml.sbaseXml.GetAttribute("desc");
			text = RegexTxt.taskRegTxtFroManyTartet(text, RegexTxt.taskspattern, tf.sz_targetArr, tf) + "<color=#ff0000>等级不足</color>";
			break;
		}
		return text;
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x00179618 File Offset: 0x00177818
	public void FindTextLind(string sLink)
	{
		bool flag = !(sLink == string.Empty);
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = sLink.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			int num2 = int.Parse(array[6]);
			int num3 = int.Parse(array[num - 1]);
			arrayList.Add(num3);
			arrayList.Add(num2);
			arrayList.Add(int.Parse(array[5]));
			bool flag2 = !(array[0] == "chuan");
			if (flag2)
			{
			}
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x001796B4 File Offset: 0x001778B4
	public void TaskAIMoveEnd(ArrayList arrList, int endType)
	{
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x001796C8 File Offset: 0x001778C8
	public ArrayList sortTaskByState(ArrayList arrl)
	{
		for (int i = 0; i < arrl.Count; i++)
		{
			for (int j = 0; j < arrl.Count - i - 1; j++)
			{
				bool flag = ((taskinfo)arrl[j]).m_nTaskState > ((taskinfo)arrl[j + 1]).m_nTaskState;
				if (flag)
				{
					taskinfo value = (taskinfo)arrl[j];
					arrl[j] = arrl[j + 1];
					arrl[j + 1] = value;
				}
			}
		}
		return arrl;
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x0017976C File Offset: 0x0017796C
	public void TaskCmdToServer(int nNetId, ArrayList nNetMsg)
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.SendMsgToServer(71000, new ArrayList
			{
				nNetId,
				nNetMsg
			});
		}
		else
		{
			bool flag2 = fGameManager.LobbyPlayer != null;
			if (flag2)
			{
				fGameManager.SendMsgToHallServer(71000, new ArrayList
				{
					nNetId,
					nNetMsg
				});
			}
		}
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x001797E4 File Offset: 0x001779E4
	public void ClearObjList(Dictionary<int, GameObject> itemObjList)
	{
		foreach (GameObject gameObject in itemObjList.Values)
		{
			bool flag = !(gameObject == null);
			if (flag)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		itemObjList.Clear();
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x00179854 File Offset: 0x00177A54
	public void OnOpenTaskTalk(taskinfo tf)
	{
		bool flag = TaskTalkUICon.inst != null;
		if (flag)
		{
			bool flag2 = !TaskTalkUICon.inst.visible;
			if (flag2)
			{
				TaskTalkUICon.inst.Open();
			}
			TaskTalkUICon.inst.OnUpdateData(tf);
		}
		else
		{
			fUIManager.GetInstance().ShowWin("TaskTalkUI", NetMsg.UITRANSFROM.LobbyUI, new fUIManager.UILoadFinishDelegate(this.LoadTaskTalkUIOk), tf, true);
		}
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x001798C0 File Offset: 0x00177AC0
	private void LoadTaskTalkUIOk(string sUI, GameObject UIObj, object szParam)
	{
		taskinfo tf = (taskinfo)szParam;
		TaskTalkUICon.inst.OnUpdateData(tf);
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x001798E4 File Offset: 0x00177AE4
	public static string GetQualColor(int nqual)
	{
		string result = string.Empty;
		switch (nqual)
		{
		case 1:
			result = "#bbbbbb";
			break;
		case 2:
			result = "#00b4ff";
			break;
		case 3:
			result = "#ff00ff";
			break;
		}
		return result;
	}

	// Token: 0x0400246F RID: 9327
	public static TaskManager mInst;

	// Token: 0x04002470 RID: 9328
	public int isServerOk;

	// Token: 0x04002471 RID: 9329
	public TaskDataManager taskdata;

	// Token: 0x04002472 RID: 9330
	public taskinfo curTaskInfo;
}
