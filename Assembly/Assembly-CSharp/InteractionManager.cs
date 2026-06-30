using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class InteractionManager : MonoBehaviour
{
	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06001627 RID: 5671 RVA: 0x000CE01C File Offset: 0x000CC21C
	// (set) Token: 0x06001628 RID: 5672 RVA: 0x000CE033 File Offset: 0x000CC233
	public static InteractionManager Instance
	{
		get
		{
			return InteractionManager._instance;
		}
		set
		{
			InteractionManager._instance = value;
		}
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000CE03C File Offset: 0x000CC23C
	private void Start()
	{
		InteractionManager.Instance = this;
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000CE048 File Offset: 0x000CC248
	public void addDict(NetLiving net)
	{
		bool flag = !this.dictNetLiveing.ContainsKey(net.getNetid());
		if (flag)
		{
			this.dictNetLiveing[net.getNetid()] = net;
		}
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000CE084 File Offset: 0x000CC284
	private void FixedUpdate()
	{
		bool flag = this.dictNetLiveing.Count <= 0;
		if (!flag)
		{
			foreach (int key in this.dictNetLiveing.Keys)
			{
				bool flag2 = this.dictNetLiveing[key] != null;
				if (flag2)
				{
					this.dictNetLiveing[key].continueInit();
				}
			}
			this.dictNetLiveing.Clear();
		}
	}

	// Token: 0x0400112B RID: 4395
	private static InteractionManager _instance;

	// Token: 0x0400112C RID: 4396
	private InteractionManager weponSkillMsg;

	// Token: 0x0400112D RID: 4397
	public Dictionary<int, NetLiving> dictNetLiveing = new Dictionary<int, NetLiving>();

	// Token: 0x0400112E RID: 4398
	private const float updateTimeLoop = 60f;
}
