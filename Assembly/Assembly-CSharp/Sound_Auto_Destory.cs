using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class Sound_Auto_Destory : MonoBehaviour
{
	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x06002A52 RID: 10834 RVA: 0x00176114 File Offset: 0x00174314
	// (set) Token: 0x06002A53 RID: 10835 RVA: 0x0017612C File Offset: 0x0017432C
	public float Wait_Time
	{
		get
		{
			return this._Wait_Time;
		}
		set
		{
			this._Wait_Time = value;
		}
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x00176136 File Offset: 0x00174336
	public void Wait_Destory()
	{
		base.StartCoroutine(this.Begin_Destory());
	}

	// Token: 0x06002A55 RID: 10837 RVA: 0x00176146 File Offset: 0x00174346
	private IEnumerator Begin_Destory()
	{
		yield return new WaitForSeconds(this.Wait_Time);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400240A RID: 9226
	private float _Wait_Time;
}
