using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class CMapEffectManager : MonoBehaviour
{
	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x0002C288 File Offset: 0x0002A488
	// (set) Token: 0x06000596 RID: 1430 RVA: 0x0002C29F File Offset: 0x0002A49F
	public static CMapEffectManager Instance
	{
		get
		{
			return CMapEffectManager._instance;
		}
		set
		{
			CMapEffectManager._instance = value;
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
	private void Start()
	{
		CMapEffectManager.Instance = this;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
	public void BeginEffectForUser(HeroPlayer hero, int iEffectId)
	{
		MapEffect mapEffect = GameRoot.SceneSocket.unFsorket.GetNetObj(iEffectId) as MapEffect;
		bool flag = mapEffect != null;
		if (flag)
		{
			mapEffect.heroAIObjectForSearch = hero;
			hero.SendMessageToServer(40003, new ArrayList
			{
				iEffectId
			});
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0002C30C File Offset: 0x0002A50C
	public void EndMapEffectAIForUser(HeroPlayer hero, int iMapEffectNetId)
	{
		MapEffect mapEffect = null;
		bool flag;
		if (!(hero.thisModel == null))
		{
			mapEffect = (GameRoot.SceneSocket.unFsorket.GetNetObj(iMapEffectNetId) as MapEffect);
			flag = (mapEffect != null);
		}
		else
		{
			flag = false;
		}
		bool flag2 = flag;
		if (flag2)
		{
			mapEffect.heroAIObjectForSearch = null;
		}
	}

	// Token: 0x04000432 RID: 1074
	private static CMapEffectManager _instance;
}
