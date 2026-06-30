using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class CLauncherNormalMe : CLauncherMe
{
	// Token: 0x06000593 RID: 1427 RVA: 0x0002BF3C File Offset: 0x0002A13C
	public override void initLauncher(CWeapon weaponcon, XMLWeapon wInfo, Transform[] point, ArrayList mainParam, Transform[] pointFps, Transform[] pointTps)
	{
		int num = 0;
		bool flag = point == null;
		if (flag)
		{
			point = new Transform[0];
		}
		base.initLauncher(weaponcon, wInfo, point, mainParam, pointFps, pointTps);
		bool flag2 = base.launcherDelayTime > 0f;
		if (flag2)
		{
			base.needDelay = true;
		}
		this._muzzles = new List<CMuzzle>();
		for (int i = 0; i < point.Length; i++)
		{
			Transform transform = point[i];
			bool flag3 = transform == null;
			if (flag3)
			{
				Debug.LogError(string.Format("对象{0}的模型武器挂点{1}初始化错误,无法找到武器挂点", (weaponcon != null && weaponcon.NetObject != null && weaponcon.NetObject.thisModel != null) ? weaponcon.NetObject.thisModel.name : "unknown", wInfo.shootname));
				transform = ((weaponcon != null) ? weaponcon.transform : null);
				point[i] = transform;
			}
			bool flag4 = transform == null;
			if (!flag4)
			{
				int bullet_type = wInfo.bullet_type;
				if (!true)
				{
				}
				CMuzzleMe cmuzzleMe;
				switch (bullet_type)
				{
				case 1:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleNormalMe>();
					break;
				case 2:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleCircleMe>();
					break;
				case 3:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleThrowMe>();
					break;
				case 4:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleTrackMe>();
					break;
				case 5:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleBezierMe>();
					break;
				case 6:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleLineBendMe>();
					break;
				case 7:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleContraMe>();
					break;
				case 8:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzlePhysicalMe>();
					break;
				case 9:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleLaserMe>();
					break;
				default:
					cmuzzleMe = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleNormalMe>();
					break;
				}
				if (!true)
				{
				}
				CMuzzleMe cmuzzleMe2 = cmuzzleMe;
				bool flag5 = wInfo.muzzleanimation.Count != 0 && i < wInfo.muzzleanimation.Count;
				if (flag5)
				{
					num = (int)wInfo.muzzleanimation[i];
				}
				Transform transform2 = null;
				bool flag6 = pointFps != null && i < pointFps.Length;
				if (flag6)
				{
					transform2 = pointFps[i];
				}
				bool flag7 = transform2 == null;
				if (flag7)
				{
					transform2 = transform;
				}
				Transform transform3 = null;
				bool flag8 = pointTps != null && i < pointTps.Length;
				if (flag8)
				{
					transform3 = pointTps[i];
				}
				bool flag9 = transform3 == null;
				if (flag9)
				{
					transform3 = transform;
				}
				cmuzzleMe2.initMuzzle(this, transform, i, num, mainParam, transform2, transform3);
				bool flag10 = !this._muzzlesDict.ContainsKey(transform.name);
				if (flag10)
				{
					this._muzzlesDict.Add(transform.name, cmuzzleMe2);
				}
				else
				{
					Debug.LogError("策划注意:CLauncherNormalMe : 同一个武器的枪口的名字不能重复" + transform.name + ".");
				}
				this._muzzles.Add(cmuzzleMe2);
			}
		}
		base.checkLoadModel(mainParam);
	}
}
