using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A9 RID: 681
public class Progress_Effect_Handle_Image : MonoBehaviour
{
	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0011F718 File Offset: 0x0011D918
	// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0011F753 File Offset: 0x0011D953
	public Image Image_Progress_Component
	{
		get
		{
			bool flag = this._Image_Progress_Component == null;
			if (flag)
			{
				this._Image_Progress_Component = base.gameObject.GetComponent<Image>();
			}
			return this._Image_Progress_Component;
		}
		set
		{
			this._Image_Progress_Component = value;
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0011F760 File Offset: 0x0011D960
	// (set) Token: 0x06001F39 RID: 7993 RVA: 0x0011F7B4 File Offset: 0x0011D9B4
	public Image Image_Group_Component
	{
		get
		{
			bool flag = this._Image_Group_Component == null;
			if (flag)
			{
				this._Image_Group_Component = base.gameObject.transform.parent.Find("Image_Group").gameObject.GetComponent<Image>();
			}
			return this._Image_Group_Component;
		}
		set
		{
			this._Image_Group_Component = value;
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0011F7C0 File Offset: 0x0011D9C0
	// (set) Token: 0x06001F3B RID: 7995 RVA: 0x0011F7D8 File Offset: 0x0011D9D8
	public int Current_Value
	{
		get
		{
			return this._Current_Value;
		}
		set
		{
			this._Current_Value = value;
			this.Difference_Value = this.Image_Progress_Component.fillAmount * (float)this.Max_Value;
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0011F7FC File Offset: 0x0011D9FC
	// (set) Token: 0x06001F3D RID: 7997 RVA: 0x0011F814 File Offset: 0x0011DA14
	public int Max_Value
	{
		get
		{
			return this._Max_Value;
		}
		set
		{
			this._Max_Value = value;
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0011F820 File Offset: 0x0011DA20
	// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0011F838 File Offset: 0x0011DA38
	public float Difference_Value
	{
		get
		{
			return this._Difference_Value;
		}
		set
		{
			this._Difference_Value = value;
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0011F844 File Offset: 0x0011DA44
	// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0011F85C File Offset: 0x0011DA5C
	public bool Display_State
	{
		get
		{
			return this._Display_State;
		}
		set
		{
			this._Display_State = value;
		}
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x0011F868 File Offset: 0x0011DA68
	private void FixedUpdate()
	{
		bool flag = this.Current_Value == 0 && (double)this.Image_Progress_Component.fillAmount >= 0.99;
		if (flag)
		{
			this.Image_Progress_Component.fillAmount = 0f;
		}
		else
		{
			this.Image_Progress_Component.fillAmount += ((float)this.Current_Value * 1f - this.Difference_Value) * Time.deltaTime / (float)this.Max_Value;
			bool flag2 = float.IsNaN(this.Image_Progress_Component.fillAmount);
			if (flag2)
			{
				this.Image_Progress_Component.fillAmount = 0f;
			}
			bool display_State = this.Display_State;
			if (display_State)
			{
				bool flag3 = this.Image_Progress_Component.fillAmount == 1f || this.Image_Progress_Component.fillAmount == 0f;
				if (flag3)
				{
					this.Image_Group_Component.enabled = false;
					this.Image_Progress_Component.enabled = false;
				}
				else
				{
					this.Image_Group_Component.enabled = true;
					this.Image_Progress_Component.enabled = true;
				}
			}
		}
	}

	// Token: 0x04001C87 RID: 7303
	private Image _Image_Progress_Component;

	// Token: 0x04001C88 RID: 7304
	private Image _Image_Group_Component;

	// Token: 0x04001C89 RID: 7305
	private int _Current_Value;

	// Token: 0x04001C8A RID: 7306
	private int _Max_Value;

	// Token: 0x04001C8B RID: 7307
	private float _Difference_Value;

	// Token: 0x04001C8C RID: 7308
	private bool _Display_State = true;
}
