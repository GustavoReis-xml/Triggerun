using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000147 RID: 327
public class Gem_Sell_UICon : Window
{
	// Token: 0x06000DDB RID: 3547 RVA: 0x000835AD File Offset: 0x000817AD
	protected override void Awake()
	{
		base.Awake();
		Gem_Sell_UICon.Instance = this;
		this._Background_Object = this.UIGameObj.Get_GameObj(0);
		this._Background_Raw_Image = this._Background_Object.GetComponent<RawImage>();
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000835E0 File Offset: 0x000817E0
	private void Update()
	{
		bool flag = this._Background_Raw_Image.texture != null && !this._Background_Raw_Image.enabled;
		if (flag)
		{
			this._Background_Raw_Image.enabled = true;
		}
	}

	// Token: 0x04000B53 RID: 2899
	public static Gem_Sell_UICon Instance;

	// Token: 0x04000B54 RID: 2900
	public GameObject _Background_Object;

	// Token: 0x04000B55 RID: 2901
	public RawImage _Background_Raw_Image;
}
