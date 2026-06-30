using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class RobotAIManager : MonoBehaviour
{
	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06002185 RID: 8581 RVA: 0x00137D70 File Offset: 0x00135F70
	// (set) Token: 0x06002186 RID: 8582 RVA: 0x00137D87 File Offset: 0x00135F87
	public static RobotAIManager Instance
	{
		get
		{
			return RobotAIManager._instance;
		}
		set
		{
			RobotAIManager._instance = value;
		}
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x00137D90 File Offset: 0x00135F90
	private void Start()
	{
		RobotAIManager.Instance = this;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x00137D9C File Offset: 0x00135F9C
	public void BeginRobotAIForUser(int id, int selectID)
	{
		RobotAI robotAI = ((NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(id)) as RobotAI;
		bool flag = robotAI != null;
		if (flag)
		{
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(selectID);
			bool flag2 = selectID == fGameManager.GetInstance().getPlayer().getNetid();
			if (flag2)
			{
				netLiving.SendMessageToServer(4010003, new ArrayList
				{
					id
				});
				robotAI.SyncNetObj = netLiving;
				robotAI.SyncObjSuccess(true);
			}
			else
			{
				robotAI.SyncObjSuccess(false);
			}
		}
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x00137E3C File Offset: 0x0013603C
	public void EndRobotAIForUser(int id)
	{
		RobotAI robotAI = ((NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(id)) as RobotAI;
		bool flag = robotAI == null;
		if (flag)
		{
			Debug.Log("EndAIForUser??服务端决定你取消计算ai,但是你没有找到这个怪:");
		}
		else
		{
			Debug.Log("我取消了同步这个怪:");
			robotAI.SyncObjSuccess(false);
			robotAI.SyncNetObj = null;
		}
	}

	// Token: 0x04001E8A RID: 7818
	private static RobotAIManager _instance;
}
