using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047F RID: 1151
public class Trap_Controller : Npc_Controller
{
	// Token: 0x06002CAC RID: 11436 RVA: 0x00189484 File Offset: 0x00187684
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
			GameRoot.LoadAsset(string.Format("HeroAnimator/{0}.mo", base.Entity_Component.Resouce_Name + "_red"), new fResourceManager.DownFinishDelegate(base.Load_Model_Complete), new ArrayList
			{
				_Target_ID,
				_Collider_Name
			});
		}
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x00189550 File Offset: 0x00187750
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
			bool flag3 = !(gameObject == null);
			if (flag3)
			{
				((CWeaponControlMe)base.Entity_Component.wc).WeaponFire(1, "0", gameObject.transform.position, gameObject);
				base.Entity_Component.Suicide();
			}
		}
	}
}
