using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class EffectAutoDestroy : MonoBehaviour
{
	// Token: 0x060009F0 RID: 2544 RVA: 0x0005ACEC File Offset: 0x00058EEC
	private void Start()
	{
		this.pars = base.transform.GetComponentsInChildren<ParticleSystem>();
		this.deleteTime = Time.time + this.maxDelayTime;
		base.StartCoroutine(this.TickDelay());
		this.poolIsOpen = (CEffectPools.Instance.isOpen && this.PutInPools);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0005AD45 File Offset: 0x00058F45
	private IEnumerator TickDelay()
	{
		bool alive = true;
		while (alive)
		{
			yield return new WaitForSeconds(this.autoDestructCheckInterval);
			alive = false;
			bool flag = Time.time > this.deleteTime;
			if (flag)
			{
				break;
			}
			bool flag2 = this.checkPar;
			if (flag2)
			{
				alive = false;
				int num;
				for (int i = 0; i < this.pars.Length; i = num + 1)
				{
					bool flag3 = !this.pars[i];
					if (!flag3)
					{
						bool flag4 = !this.pars[i].IsAlive();
						if (flag4)
						{
							bool flag5 = !this.poolIsOpen;
							if (flag5)
							{
								UnityEngine.Object.Destroy(this.pars[i]);
							}
						}
						else
						{
							alive = true;
						}
					}
					num = i;
				}
			}
			else
			{
				alive = true;
			}
		}
		bool flag6 = !alive;
		if (flag6)
		{
			bool flag7 = !this.poolIsOpen;
			if (flag7)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				yield break;
			}
			CEffectPools.Instance.PutInPools(this.ResGID, base.transform);
			UnityEngine.Object.Destroy(this);
		}
		yield break;
	}

	// Token: 0x0400080B RID: 2059
	public int ResGID;

	// Token: 0x0400080C RID: 2060
	public bool PutInPools;

	// Token: 0x0400080D RID: 2061
	private float deleteTime;

	// Token: 0x0400080E RID: 2062
	public float maxDelayTime = 3f;

	// Token: 0x0400080F RID: 2063
	private bool poolIsOpen;

	// Token: 0x04000810 RID: 2064
	private float autoDestructCheckInterval = 0.1f;

	// Token: 0x04000811 RID: 2065
	public bool checkPar = true;

	// Token: 0x04000812 RID: 2066
	private ParticleSystem[] pars;
}
