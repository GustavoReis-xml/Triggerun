using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200008A RID: 138
public class CHeroRender : MonoBehaviour
{
	// Token: 0x060004E0 RID: 1248 RVA: 0x00028034 File Offset: 0x00026234
	public void GetRenders()
	{
		this._renders = base.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < this.HiddenTransform.Length; i++)
		{
			Renderer[] componentsInChildren = this.HiddenTransform[i].gameObject.GetComponentsInChildren<Renderer>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				this._renderChild.Add(componentsInChildren[j]);
			}
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x000280A0 File Offset: 0x000262A0
	public void SetRenderMode(bool shadow)
	{
		for (int i = 0; i < this._renders.Length; i++)
		{
			if (shadow)
			{
				this._renders[i].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
			}
			else
			{
				this._renders[i].shadowCastingMode = ShadowCastingMode.On;
			}
		}
		for (int j = 0; j < this._renderChild.Count; j++)
		{
			if (shadow)
			{
				((Renderer)this._renderChild[j]).shadowCastingMode = ShadowCastingMode.ShadowsOnly;
			}
			else
			{
				((Renderer)this._renderChild[j]).shadowCastingMode = ShadowCastingMode.On;
			}
		}
		CHeroWeapon component = base.GetComponent<CHeroWeapon>();
		bool flag = component != null;
		if (flag)
		{
			component.SetWeaponParts(shadow);
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00028170 File Offset: 0x00026370
	public void SetParticleSystem(bool shadow)
	{
		for (int i = 0; i < this.HiddenTransform.Length; i++)
		{
			bool flag = this.HiddenTransform[i].gameObject.activeSelf != shadow;
			if (flag)
			{
				this.HiddenTransform[i].gameObject.SetActive(shadow);
			}
		}
		for (int j = 0; j < this.ShowTransform.Length; j++)
		{
			bool flag2 = this.ShowTransform[j] != null && this.ShowTransform[j].gameObject.activeSelf != !shadow;
			if (flag2)
			{
				this.ShowTransform[j].gameObject.SetActive(!shadow);
			}
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00028234 File Offset: 0x00026434
	public void SetTransprtMeshVisible()
	{
		for (int i = 0; i < this.TransportMeshVisible.Length; i++)
		{
			this.TransportMeshVisible[i].gameObject.SetActive(true);
		}
	}

	// Token: 0x040003B6 RID: 950
	private Renderer[] _renders;

	// Token: 0x040003B7 RID: 951
	private ArrayList _renderChild = new ArrayList();

	// Token: 0x040003B8 RID: 952
	public Transform[] HiddenTransform;

	// Token: 0x040003B9 RID: 953
	public Transform[] ShowTransform;

	// Token: 0x040003BA RID: 954
	public Transform[] TransportMeshVisible;
}
