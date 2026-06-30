using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class Npc_View : MonoBehaviour
{
	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0010AEF4 File Offset: 0x001090F4
	// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x0010AF52 File Offset: 0x00109152
	public Npc_Controller Controller_Component
	{
		get
		{
			bool flag = this._Controller_Component == null;
			if (flag)
			{
				this._Controller_Component = base.gameObject.GetComponent<Npc_Controller>();
				bool flag2 = this._Controller_Component == null;
				if (flag2)
				{
					this._Controller_Component = base.gameObject.AddComponent<Npc_Controller>();
				}
			}
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0010AF5C File Offset: 0x0010915C
	public void Destroy_Wait(int _Death_Type)
	{
		base.StartCoroutine(this.Destroy_Start(_Death_Type));
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x0010AF6D File Offset: 0x0010916D
	public IEnumerator Destroy_Start(int _Death_Type)
	{
		bool flag = this == null || !base.gameObject.activeSelf || !this.Controller_Component.Entity_Component.playerHaveCreate || this.Controller_Component.Entity_Component.haveDestroy;
		if (flag)
		{
			yield break;
		}
		bool flag2 = _Death_Type < 1;
		if (flag2)
		{
			this.Play_Death_Effect();
		}
		bool flag3 = this.Controller_Component.Entity_Component.Death_Effect == string.Empty || this.Controller_Component.Entity_Component.Death_Effect == "0";
		if (flag3)
		{
			bool flag4 = this.Controller_Component.Entity_Component.AI_Component != null;
			if (flag4)
			{
				yield return new WaitForSeconds(3f);
				this.Controller_Component.Entity_Component.thisModel.SetActive(false);
			}
			else
			{
				this.Controller_Component.Entity_Component.thisModel.SetActive(false);
				yield return new WaitForSeconds(2f);
			}
		}
		else
		{
			this.Controller_Component.Entity_Component.thisModel.SetActive(false);
			yield return new WaitForSeconds(3f);
		}
		yield break;
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x0010AF84 File Offset: 0x00109184
	public void Play_Death_Effect()
	{
		bool flag = this.Controller_Component.Entity_Component.isCopy || !CFilmManager.Instance.playing;
		if (flag)
		{
			bool flag2 = this.Controller_Component.Death_Effect_Object != null;
			if (flag2)
			{
				this.Controller_Component.Death_Effect_Object.SetActive(true);
			}
			bool flag3 = this.Controller_Component.Death_Sound_Object != null;
			if (flag3)
			{
				this.Controller_Component.Play_Death_Sound();
			}
		}
	}

	// Token: 0x04001972 RID: 6514
	private Npc_Controller _Controller_Component;
}
