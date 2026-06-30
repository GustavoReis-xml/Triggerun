using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class CPlayerRexx : MonoBehaviour
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x000397C4 File Offset: 0x000379C4
	// (set) Token: 0x0600069C RID: 1692 RVA: 0x000397DB File Offset: 0x000379DB
	public static CPlayerRexx Instance
	{
		get
		{
			return CPlayerRexx._instance;
		}
		set
		{
			CPlayerRexx._instance = value;
		}
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x000397E4 File Offset: 0x000379E4
	private void Start()
	{
		CPlayerRexx._instance = this;
		this.Init();
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x000397F4 File Offset: 0x000379F4
	private void Init()
	{
		this.allClass = new Dictionary<string, int>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		int num = 0;
		for (int i = 0; i < assemblies.Length; i++)
		{
			Module[] modules = assemblies[i].GetModules();
			foreach (Module module in modules)
			{
				bool flag = !this.allClass.ContainsKey(module.Name.ToString());
				if (flag)
				{
					this.allClass.Add(module.Name.ToString(), 1);
					num++;
				}
			}
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00039898 File Offset: 0x00037A98
	public void exit()
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Module[] modules = assemblies[i].GetModules();
			foreach (Module module in modules)
			{
				bool flag = !this.allClass.ContainsKey(module.Name.ToString());
				if (flag)
				{
					Type[] types = module.GetTypes();
					string text = module.Name + ":";
					for (int k = 0; k < types.Length; k++)
					{
						text = text + types[k].FullName + ((k + 1 != types.Length) ? " , " : string.Empty);
					}
					bool flag2 = fGameManager.Heroplayer != null;
					if (flag2)
					{
						FunManager.Instance.Send(fGameManager.Heroplayer, "162", text);
					}
					else
					{
						bool flag3 = fGameManager.LobbyPlayer != null;
						if (flag3)
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add("162");
							arrayList.Add(text);
							arrayList.Add(DateTime.Now.ToString());
							arrayList.Add(SysConfig.g_client);
							arrayList.Add(string.Empty);
							fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
						}
					}
				}
			}
		}
	}

	// Token: 0x040004D1 RID: 1233
	private Dictionary<string, int> allClass;

	// Token: 0x040004D2 RID: 1234
	private static CPlayerRexx _instance;
}
