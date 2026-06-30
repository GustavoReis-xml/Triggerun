using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FB RID: 1019
public class TaskDataManager
{
	// Token: 0x06002ABD RID: 10941 RVA: 0x001788BC File Offset: 0x00176ABC
	public void InitTaskData(ArrayList taskdata, string taskhis)
	{
		int count = taskdata.Count;
		for (int i = 0; i < count; i++)
		{
			taskinfo taskinfo = new taskinfo();
			taskinfo.inittask((ArrayList)taskdata[i]);
			bool flag = fGameManager.mloadConfig.AllTaskObj.ContainsKey(taskinfo.m_nTaskId) && !fGameManager.mloadConfig.AllTaskObj[taskinfo.m_nTaskId].m_nIsHide;
			if (flag)
			{
				bool flag2 = taskinfo.m_nTasktype == 1;
				if (flag2)
				{
					this.CurMainTaskId = taskinfo.m_nTaskId;
					this.CurMainState = taskinfo.m_nTaskState;
				}
				bool flag3 = !this.PlayHaveTask.ContainsKey(taskinfo.m_nAutoId);
				if (flag3)
				{
					this.PlayHaveTask.Add(taskinfo.m_nAutoId, taskinfo);
				}
			}
		}
		this.taskHistory = taskhis;
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x001789A0 File Offset: 0x00176BA0
	public void InitChapterData(ArrayList szData)
	{
		int count = szData.Count;
		for (int i = 0; i < count; i++)
		{
			Chapterinfo chapterinfo = new Chapterinfo();
			chapterinfo.InitChapter((ArrayList)szData[i]);
			bool flag = !this.PlayHaveChapter.ContainsKey(chapterinfo.m_nChapterId);
			if (flag)
			{
				this.PlayHaveChapter.Add(chapterinfo.m_nChapterId, chapterinfo);
			}
		}
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x00178A0F File Offset: 0x00176C0F
	public void UpdateHistoryTaskId(int taskid)
	{
		this.historyList.Add(taskid);
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x00178A24 File Offset: 0x00176C24
	public ArrayList getHistoryList()
	{
		return this.historyList;
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x00178A3C File Offset: 0x00176C3C
	public void OnAddTask(taskinfo tinof)
	{
		bool flag = this.PlayHaveTask.ContainsKey(tinof.m_nAutoId);
		if (flag)
		{
			this.PlayHaveTask[tinof.m_nAutoId] = tinof;
		}
		else
		{
			this.PlayHaveTask.Add(tinof.m_nAutoId, tinof);
		}
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x00178A8C File Offset: 0x00176C8C
	public void OnDelTask(taskinfo tinof)
	{
		bool flag = this.PlayHaveTask.ContainsKey(tinof.m_nAutoId);
		if (flag)
		{
			this.PlayHaveTask.Remove(tinof.m_nAutoId);
		}
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x00178AC4 File Offset: 0x00176CC4
	public void OnDelOldDailyTask(int nAutoId)
	{
		bool flag = this.PlayHaveTask.ContainsKey(nAutoId);
		if (flag)
		{
			this.PlayHaveTask.Remove(nAutoId);
		}
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x00178AF4 File Offset: 0x00176CF4
	public void OnUpdateTask(taskinfo tinof)
	{
		bool flag = this.PlayHaveTask.ContainsKey(tinof.m_nAutoId);
		if (flag)
		{
			this.PlayHaveTask[tinof.m_nAutoId] = tinof;
		}
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x00178B2C File Offset: 0x00176D2C
	public void OnAddChapter(Chapterinfo tinof)
	{
		bool flag = this.PlayHaveChapter.ContainsKey(tinof.m_nChapterId);
		if (flag)
		{
			this.PlayHaveChapter[tinof.m_nChapterId] = tinof;
		}
		else
		{
			this.PlayHaveChapter.Add(tinof.m_nChapterId, tinof);
		}
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x00178B7C File Offset: 0x00176D7C
	public void OnUpdateChapter(Chapterinfo tinof)
	{
		bool flag = this.PlayHaveChapter.ContainsKey(tinof.m_nChapterId);
		if (flag)
		{
			this.PlayHaveChapter[tinof.m_nChapterId] = tinof;
		}
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x00178BB4 File Offset: 0x00176DB4
	public Dictionary<int, taskinfo> GetTaskData()
	{
		return this.PlayHaveTask;
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x00178BCC File Offset: 0x00176DCC
	public string getTaskHistory()
	{
		return this.taskHistory;
	}

	// Token: 0x04002454 RID: 9300
	private Dictionary<int, taskinfo> PlayHaveTask = new Dictionary<int, taskinfo>();

	// Token: 0x04002455 RID: 9301
	private Dictionary<int, Chapterinfo> PlayHaveChapter = new Dictionary<int, Chapterinfo>();

	// Token: 0x04002456 RID: 9302
	private string taskHistory;

	// Token: 0x04002457 RID: 9303
	private ArrayList historyList = new ArrayList();

	// Token: 0x04002458 RID: 9304
	public int CurMainTaskId;

	// Token: 0x04002459 RID: 9305
	public int CurMainState;
}
