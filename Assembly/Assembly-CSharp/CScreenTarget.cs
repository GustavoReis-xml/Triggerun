using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class CScreenTarget : MonoBehaviour
{
	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x00041C10 File Offset: 0x0003FE10
	// (set) Token: 0x06000774 RID: 1908 RVA: 0x00041C28 File Offset: 0x0003FE28
	public NetLiving NetTarget
	{
		get
		{
			return this._netTarget;
		}
		set
		{
			this._netTarget = value;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x00041C34 File Offset: 0x0003FE34
	// (set) Token: 0x06000776 RID: 1910 RVA: 0x00041C4C File Offset: 0x0003FE4C
	public ArrayList NetTargets
	{
		get
		{
			return this._netTargets;
		}
		set
		{
			this._netTargets = value;
		}
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00041C56 File Offset: 0x0003FE56
	public void InitSTarget(NetLiving _hero, string className)
	{
		this.hero = _hero;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00041C60 File Offset: 0x0003FE60
	public void SetBlockLayer(int lay)
	{
		this.layer = lay;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00041C6C File Offset: 0x0003FE6C
	public static void InitScreenTarget(NetLiving _hero, string className)
	{
		bool flag = className.Length >= 2;
		if (flag)
		{
			int blockLayer = (_hero.nForce != 1) ? (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1")) : (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_2"));
			Type type = Type.GetType("CScreen" + className);
			_hero.screentarget = (CScreenTarget)_hero.UserControl.AddComponent(type);
			_hero.screentarget.SetBlockLayer(blockLayer);
			_hero.screentarget.InitSTarget(_hero, className);
		}
	}

	// Token: 0x04000569 RID: 1385
	public bool lockTarget;

	// Token: 0x0400056A RID: 1386
	public float targetDis = 10f;

	// Token: 0x0400056B RID: 1387
	public float targetAngle = 0.9f;

	// Token: 0x0400056C RID: 1388
	protected int layer = 1;

	// Token: 0x0400056D RID: 1389
	public NetLiving hero;

	// Token: 0x0400056E RID: 1390
	private NetLiving _netTarget;

	// Token: 0x0400056F RID: 1391
	private ArrayList _netTargets = new ArrayList();
}
