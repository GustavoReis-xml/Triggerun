using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class Supply_Controller : Npc_Controller
{
	// Token: 0x06002A88 RID: 10888 RVA: 0x001774CC File Offset: 0x001756CC
	public override void Load_Model(int _Target_ID, string _Collider_Name)
	{
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == base.Entity_Component.nForce;
		if (flag)
		{
			GameRoot.LoadAsset(string.Format("HeroAnimator/{0}.mo", base.Entity_Component.Resouce_Name + "_blue"), new fResourceManager.DownFinishDelegate(base.Load_Model_Complete), new ArrayList
			{
				_Target_ID,
				_Collider_Name
			});
		}
		else
		{
			base.Entity_Component.OnNetDestroy();
		}
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x00177558 File Offset: 0x00175758
	public override void Detection()
	{
		GameObject gameObject = null;
		bool flag = !base.Entity_Component.Main || base.Entity_Component.Detection_Controller_Component == null || base.Entity_Component.Death_State || base.Entity_Component.thisModel == null;
		if (!flag)
		{
			Dictionary<int, GameObject> dictionary = base.Entity_Component.Detection_Controller_Component.Range_Detection(base.Entity_Component.Eye_Object.transform.position);
			List<int> list = new List<int>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int key = list[i];
				gameObject = dictionary[key];
				bool flag2 = gameObject == null;
				if (flag2)
				{
				}
			}
			bool flag3 = gameObject == null;
			if (!flag3)
			{
				PublicInfo component = gameObject.transform.root.gameObject.GetComponent<PublicInfo>();
				bool flag4 = component.NetObject.netobject_type == 1;
				if (flag4)
				{
					GamePlayer gamePlayer = (GamePlayer)component.NetObject;
					bool flag5 = gamePlayer.playerCommon.GetHpBiLi() < 1f;
					if (flag5)
					{
						((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", gameObject.transform.position, gameObject);
						base.Entity_Component.Suicide();
					}
				}
			}
		}
	}
}
