using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class CTarget : MonoBehaviour
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000795 RID: 1941 RVA: 0x0004303C File Offset: 0x0004123C
	// (set) Token: 0x06000796 RID: 1942 RVA: 0x00043054 File Offset: 0x00041254
	public int targetType
	{
		get
		{
			return this._targetType;
		}
		set
		{
			this._targetType = value;
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000797 RID: 1943 RVA: 0x00043060 File Offset: 0x00041260
	// (set) Token: 0x06000798 RID: 1944 RVA: 0x00043078 File Offset: 0x00041278
	public NetLiving NetObject
	{
		get
		{
			return this._netObject;
		}
		set
		{
			this._netObject = value;
		}
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00043082 File Offset: 0x00041282
	private void Start()
	{
		this.InitTarget();
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0004308C File Offset: 0x0004128C
	public virtual void InitTarget()
	{
		this._targetType = 0;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00043098 File Offset: 0x00041298
	public virtual UnityEngine.Object GetTarget()
	{
		return null;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000430AC File Offset: 0x000412AC
	public virtual UnityEngine.Object[] GetTargets()
	{
		return null;
	}

	// Token: 0x040005AB RID: 1451
	private int _targetType;

	// Token: 0x040005AC RID: 1452
	private NetLiving _netObject;
}
