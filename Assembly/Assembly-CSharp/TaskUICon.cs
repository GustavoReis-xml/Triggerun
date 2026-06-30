using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000403 RID: 1027
public class TaskUICon : Window
{
	// Token: 0x06002AED RID: 10989 RVA: 0x0017A318 File Offset: 0x00178518
	protected override void Awake()
	{
		base.Awake();
		TaskUICon.inst = this;
		this.pMainContent = this.UIGameObj.Get_GameObj(0);
		this.pDailyContent = this.UIGameObj.Get_GameObj(1);
		this.pTeShuContent = this.UIGameObj.Get_GameObj(2);
		this.pOneTask = this.UIGameObj.Get_GameObj(4);
		this.pTeShuPanel = this.UIGameObj.Get_GameObj(5);
		this.pBgImg = this.UIGameObj.Get_GameObj(3);
		this.tNoTaskTiShi = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.tNoTaskTiShi.text = string.Empty;
		this.pOneTask.SetActive(false);
		this.InitData();
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x0017A3DC File Offset: 0x001785DC
	public void InitData()
	{
		Dictionary<int, taskinfo> taskData = TaskManager.GetInstance().taskdata.GetTaskData();
		foreach (taskinfo taskinfo in taskData.Values)
		{
			bool flag = taskinfo != null;
			if (flag)
			{
				this.OnAddTask(taskinfo);
			}
		}
		this.OnDailyTaskShow();
		this.OnHaveTeShuTask();
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x0017A460 File Offset: 0x00178660
	private void OnDailyTaskShow()
	{
		bool flag = this.nDailyTaskNum > 0;
		if (flag)
		{
			this.tNoTaskTiShi.text = string.Empty;
		}
		else
		{
			this.tNoTaskTiShi.text = Language.GetLang(38307, new object[0]);
		}
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x0017A4B0 File Offset: 0x001786B0
	public void OnHaveTeShuTask()
	{
		bool flag = this.nActiveTaskNum > 0;
		if (flag)
		{
			this.pTeShuPanel.SetActive(true);
			this.pBgImg.GetComponent<RectTransform>().sizeDelta = new Vector2(396f, 798f);
		}
		else
		{
			this.pTeShuPanel.SetActive(false);
			this.pBgImg.GetComponent<RectTransform>().sizeDelta = new Vector2(396f, 555f);
		}
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x0017A52C File Offset: 0x0017872C
	public void UpdateTaskData(taskinfo tf, int nUpdateType)
	{
		switch (nUpdateType)
		{
		case 0:
			this.OnDeleteTask(tf);
			this.OnDailyTaskShow();
			this.OnHaveTeShuTask();
			return;
		case 1:
			this.OnAddTask(tf);
			this.OnDailyTaskShow();
			this.OnHaveTeShuTask();
			return;
		case 2:
			this.OnUpdateTask(tf);
			return;
		case 4:
			this.OnRefDailyTaskShow(tf);
			return;
		}
		Debug.LogError("更新类型错误=UI==" + nUpdateType.ToString());
	}

	// Token: 0x06002AF2 RID: 10994 RVA: 0x0017A5B8 File Offset: 0x001787B8
	private void OnRefDailyTaskShow(taskinfo tf)
	{
		int nFlogId = tf.m_nFlogId;
		bool flag = this.m_szTaskListObj.ContainsKey(nFlogId);
		if (flag)
		{
			GameObject gameObject = this.m_szTaskListObj[nFlogId];
			TaskBaseData baseXml = fGameManager.mloadConfig.AllTaskObj[tf.m_nTaskId];
			TaskOneItemCon component = gameObject.GetComponent<TaskOneItemCon>();
			component.OnSetData(tf, baseXml);
			component.OnUpdateData(tf);
			this.m_szTaskListObj.Remove(nFlogId);
			this.m_szTaskListObj.Add(tf.m_nAutoId, gameObject);
		}
		else
		{
			this.OnAddTask(tf);
		}
	}

	// Token: 0x06002AF3 RID: 10995 RVA: 0x0017A64C File Offset: 0x0017884C
	private void OnAddTask(taskinfo tf)
	{
		bool flag = !fGameManager.mloadConfig.AllTaskObj.ContainsKey(tf.m_nTaskId);
		if (flag)
		{
			Debug.LogError("任务不存在 == Id==" + tf.m_nTaskId.ToString());
		}
		else
		{
			GameObject parent = this.GetParent(tf.m_nTasktype);
			bool flag2 = parent == null;
			if (flag2)
			{
				Debug.LogError("未知任务类型 == type==" + tf.m_nTasktype.ToString());
			}
			else
			{
				bool flag3 = this.m_szTaskListObj.ContainsKey(tf.m_nAutoId);
				if (flag3)
				{
					this.OnUpdateTask(tf);
				}
				else
				{
					bool flag4 = tf.m_nTasktype == 2;
					if (flag4)
					{
						this.nDailyTaskNum++;
					}
					bool flag5 = tf.m_nTasktype == 3;
					if (flag5)
					{
						this.nActiveTaskNum++;
					}
					TaskBaseData baseXml = fGameManager.mloadConfig.AllTaskObj[tf.m_nTaskId];
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pOneTask);
					gameObject.transform.SetParent(parent.transform);
					gameObject.transform.SetSiblingIndex(0);
					gameObject.name = tf.m_nTaskId.ToString();
					gameObject.SetActive(true);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					TaskOneItemCon taskOneItemCon = gameObject.AddComponent<TaskOneItemCon>();
					taskOneItemCon.OnSetData(tf, baseXml);
					this.m_szTaskListObj.Add(tf.m_nAutoId, gameObject);
					EventTriggerListener.Get(gameObject.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnTaskClick);
				}
			}
		}
	}

	// Token: 0x06002AF4 RID: 10996 RVA: 0x0017A804 File Offset: 0x00178A04
	private void OnUpdateTask(taskinfo tf)
	{
		bool flag = this.m_szTaskListObj.ContainsKey(tf.m_nAutoId);
		if (flag)
		{
			GameObject gameObject = this.m_szTaskListObj[tf.m_nAutoId];
			TaskOneItemCon component = gameObject.GetComponent<TaskOneItemCon>();
			component.OnUpdateData(tf);
		}
	}

	// Token: 0x06002AF5 RID: 10997 RVA: 0x0017A84C File Offset: 0x00178A4C
	private void OnDeleteTask(taskinfo tf)
	{
		bool flag = this.m_szTaskListObj.ContainsKey(tf.m_nAutoId);
		if (flag)
		{
			GameObject obj = this.m_szTaskListObj[tf.m_nAutoId];
			UnityEngine.Object.DestroyImmediate(obj);
			this.m_szTaskListObj.Remove(tf.m_nAutoId);
			bool flag2 = tf.m_nTasktype == 2;
			if (flag2)
			{
				this.nDailyTaskNum--;
			}
			bool flag3 = tf.m_nTasktype == 3;
			if (flag3)
			{
				this.nActiveTaskNum--;
			}
		}
	}

	// Token: 0x06002AF6 RID: 10998 RVA: 0x0017A8D8 File Offset: 0x00178AD8
	private GameObject GetParent(int nTaskState)
	{
		if (!true)
		{
		}
		GameObject result;
		switch (nTaskState)
		{
		case 1:
			result = this.pMainContent;
			break;
		case 2:
			result = this.pDailyContent;
			break;
		case 3:
			result = this.pTeShuContent;
			break;
		default:
			result = null;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x0017A928 File Offset: 0x00178B28
	private void OnTaskClick(GameObject obj)
	{
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x0017A92B File Offset: 0x00178B2B
	private void OnHrefClick(string ddddd)
	{
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x0017A92E File Offset: 0x00178B2E
	private void OnHrefEnter(string ddddd)
	{
	}

	// Token: 0x06002AFA RID: 11002 RVA: 0x0017A931 File Offset: 0x00178B31
	private void OnHrefExit(string ddddd)
	{
	}

	// Token: 0x04002496 RID: 9366
	public static TaskUICon inst;

	// Token: 0x04002497 RID: 9367
	private GameObject pMainContent;

	// Token: 0x04002498 RID: 9368
	private GameObject pDailyContent;

	// Token: 0x04002499 RID: 9369
	private GameObject pTeShuContent;

	// Token: 0x0400249A RID: 9370
	private GameObject pOneTask;

	// Token: 0x0400249B RID: 9371
	private GameObject pTeShuPanel;

	// Token: 0x0400249C RID: 9372
	private GameObject pBgImg;

	// Token: 0x0400249D RID: 9373
	private Text tNoTaskTiShi;

	// Token: 0x0400249E RID: 9374
	private int nDailyTaskNum;

	// Token: 0x0400249F RID: 9375
	private int nActiveTaskNum;

	// Token: 0x040024A0 RID: 9376
	private Dictionary<int, GameObject> m_szTaskListObj = new Dictionary<int, GameObject>();
}
