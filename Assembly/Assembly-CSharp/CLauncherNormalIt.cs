using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class CLauncherNormalIt : CLauncherIt
{
	// Token: 0x06000591 RID: 1425 RVA: 0x0002BC1C File Offset: 0x00029E1C
	public override void initLauncher(CWeapon weaponcon, XMLWeapon wInfo, Transform[] point, ArrayList mainParam, Transform[] pointFps, Transform[] pointTps)
	{
		object animators = 0;
		bool flag = point == null;
		if (flag)
		{
			point = new Transform[0];
		}
		base.initLauncher(weaponcon, wInfo, point, mainParam, pointFps, pointTps);
		this._muzzles = new List<CMuzzle>();
		for (int i = 0; i < point.Length; i++)
		{
			Transform transform = point[i];
			bool flag2 = transform == null;
			if (flag2)
			{
				Debug.LogError(string.Format("对象{0}的模型武器挂点{1}初始化错误,无法找到武器挂点", (weaponcon != null && weaponcon.NetObject != null && weaponcon.NetObject.thisModel != null) ? weaponcon.NetObject.thisModel.name : "unknown", wInfo.shootname));
				transform = ((weaponcon != null) ? weaponcon.transform : null);
				point[i] = transform;
			}
			bool flag3 = transform == null;
			if (!flag3)
			{
				int bullet_type = wInfo.bullet_type;
				if (!true)
				{
				}
				CMuzzleIt cmuzzleIt;
				switch (bullet_type)
				{
				case 1:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleNormalIt>();
					break;
				case 2:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleCircleIt>();
					break;
				case 3:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleThrowIt>();
					break;
				case 4:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleTrackIt>();
					break;
				case 5:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleBezierIt>();
					break;
				case 6:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleLineBendIt>();
					break;
				case 7:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleContraIt>();
					break;
				case 8:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzlePhysicalIt>();
					break;
				case 9:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleLaserIt>();
					break;
				default:
					cmuzzleIt = base.cwc.GetMuzzleContainer().AddComponent<CMuzzleNormalIt>();
					break;
				}
				if (!true)
				{
				}
				CMuzzleIt cmuzzleIt2 = cmuzzleIt;
				bool flag4 = wInfo.muzzleanimation.Count != 0 && i < wInfo.muzzleanimation.Count;
				if (flag4)
				{
					animators = wInfo.muzzleanimation[i];
				}
				Transform transform2 = null;
				bool flag5 = pointFps != null && i < pointFps.Length;
				if (flag5)
				{
					transform2 = pointFps[i];
				}
				bool flag6 = transform2 == null;
				if (flag6)
				{
					transform2 = transform;
				}
				Transform transform3 = null;
				bool flag7 = pointTps != null && i < pointTps.Length;
				if (flag7)
				{
					transform3 = pointTps[i];
				}
				bool flag8 = transform3 == null;
				if (flag8)
				{
					transform3 = transform;
				}
				cmuzzleIt2.initMuzzle(this, transform, i, animators, mainParam, transform2, transform3);
				bool flag9 = !this._muzzlesDict.ContainsKey(transform.name);
				if (flag9)
				{
					this._muzzlesDict.Add(transform.name, cmuzzleIt2);
				}
				else
				{
					Debug.LogError("策划注意:CLauncherNormalIt : 枪口的名字不能重复" + transform.name + ".");
				}
				this._muzzles.Add(cmuzzleIt2);
			}
		}
		base.checkLoadModel(mainParam);
	}
}
