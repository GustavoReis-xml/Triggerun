using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class CObjectRender : MonoBehaviour
{
	// Token: 0x0600066A RID: 1642 RVA: 0x00036158 File Offset: 0x00034358
	public void SetEnemyFriendRenderParticle(bool active, bool mustClose = false)
	{
		bool flag = !mustClose && active;
		for (int i = 0; i < this.EnemyRender.Length; i++)
		{
			bool flag2 = this.EnemyRender[i].gameObject != null;
			if (flag2)
			{
				bool flag3 = this.EnemyRender[i].gameObject.activeSelf != flag;
				if (flag3)
				{
					this.EnemyRender[i].gameObject.SetActive(flag);
				}
			}
			else
			{
				Debug.LogError("老朱EnemyRender挂错了，请老朱查找");
			}
		}
		flag = (mustClose || active);
		for (int j = 0; j < this.FriendRender.Length; j++)
		{
			bool flag4 = this.FriendRender[j] != null;
			if (flag4)
			{
				bool flag5 = this.FriendRender[j].gameObject.activeSelf != !flag;
				if (flag5)
				{
					this.FriendRender[j].gameObject.SetActive(!flag);
				}
			}
			else
			{
				Debug.LogError("老朱FriendRender挂错了，请老朱查找");
			}
		}
		flag = (!mustClose && active);
		for (int k = 0; k < this.EnemyParticleSystem.Length; k++)
		{
			bool flag6 = this.EnemyParticleSystem[k] != null;
			if (flag6)
			{
				bool flag7 = this.EnemyParticleSystem[k].gameObject.activeSelf != flag;
				if (flag7)
				{
					this.EnemyParticleSystem[k].gameObject.SetActive(flag);
				}
			}
			else
			{
				Debug.LogError("老朱EnemyParticleSystem挂错了，请老朱查找");
			}
		}
		flag = (mustClose || active);
		for (int l = 0; l < this.FriendParticleSystem.Length; l++)
		{
			bool flag8 = this.FriendParticleSystem[l] != null;
			if (flag8)
			{
				bool flag9 = this.FriendParticleSystem[l].gameObject.activeSelf != !flag;
				if (flag9)
				{
					this.FriendParticleSystem[l].gameObject.SetActive(!flag);
				}
			}
			else
			{
				Debug.LogError("老朱FriendParticleSystem挂错了，请老朱查找");
			}
		}
		for (int m = 0; m < this.EnemyHideProjector.Length; m++)
		{
			bool flag10 = this.EnemyHideProjector[m] != null;
			if (flag10)
			{
				this.EnemyHideProjector[m].enabled = active;
			}
			else
			{
				Debug.LogError("老朱EnemyHideProjector挂错了，请老朱查找");
			}
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x000363C3 File Offset: 0x000345C3
	public void OnDeathCloseTrans(bool active)
	{
		base.StartCoroutine(this.delayCloseOnDeathrans());
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x000363D3 File Offset: 0x000345D3
	private IEnumerator delayCloseOnDeathrans()
	{
		yield return new WaitForSeconds(0.5f);
		int num;
		for (int i = 0; i < this.DeathCloseTrans.Length; i = num + 1)
		{
			bool flag = this.DeathCloseTrans[i] != null;
			if (flag)
			{
				this.DeathCloseTrans[i].gameObject.SetActive(false);
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x04000499 RID: 1177
	public Renderer[] EnemyRender;

	// Token: 0x0400049A RID: 1178
	public Renderer[] FriendRender;

	// Token: 0x0400049B RID: 1179
	public Transform[] EnemyParticleSystem;

	// Token: 0x0400049C RID: 1180
	public Transform[] FriendParticleSystem;

	// Token: 0x0400049D RID: 1181
	public Transform[] DeathCloseTrans;

	// Token: 0x0400049E RID: 1182
	public Projector[] EnemyHideProjector;
}
