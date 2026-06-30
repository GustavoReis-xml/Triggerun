using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E1 RID: 993
public class SoundAutoDestroy : MonoBehaviour
{
	// Token: 0x06002A4F RID: 10831 RVA: 0x00176084 File Offset: 0x00174284
	private void Start()
	{
		this.sound = base.transform.GetComponent<AudioSource>();
		this.deleteTime = Time.time + this.maxDelayTime;
		base.StartCoroutine(this.TickDelay());
		this.poolIsOpen = (CEffectPools.Instance.isOpen && this.PutInPools);
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x001760DD File Offset: 0x001742DD
	private IEnumerator TickDelay()
	{
		bool alive = true;
		while (alive)
		{
			yield return new WaitForSeconds(this.autoDestructCheckInterval);
			bool flag = Time.time > this.deleteTime;
			if (flag)
			{
				alive = false;
			}
			bool flag2 = !this.sound.isPlaying;
			if (flag2)
			{
				alive = false;
			}
		}
		bool flag3 = !alive;
		if (flag3)
		{
			bool flag4 = !this.poolIsOpen;
			if (flag4)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				yield break;
			}
			CEffectPools.Instance.PutInPools(this.ResGID, base.transform);
			UnityEngine.Object.Destroy(this);
		}
		yield break;
	}

	// Token: 0x04002403 RID: 9219
	public int ResGID;

	// Token: 0x04002404 RID: 9220
	public bool PutInPools = true;

	// Token: 0x04002405 RID: 9221
	private float deleteTime;

	// Token: 0x04002406 RID: 9222
	public float maxDelayTime = 3f;

	// Token: 0x04002407 RID: 9223
	private bool poolIsOpen;

	// Token: 0x04002408 RID: 9224
	private float autoDestructCheckInterval = 0.1f;

	// Token: 0x04002409 RID: 9225
	private AudioSource sound;
}
