using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AA RID: 682
public class Progress_Effect_Handle_Slider : MonoBehaviour
{
	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0011F994 File Offset: 0x0011DB94
	// (set) Token: 0x06001F45 RID: 8005 RVA: 0x0011F9AC File Offset: 0x0011DBAC
	public Slider Slider_Component
	{
		get
		{
			return this._Slider_Component;
		}
		set
		{
			this._Slider_Component = value;
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0011F9B8 File Offset: 0x0011DBB8
	// (set) Token: 0x06001F47 RID: 8007 RVA: 0x0011F9D0 File Offset: 0x0011DBD0
	public float Current_Value
	{
		get
		{
			return this._Current_Value;
		}
		set
		{
			this._Current_Value = value;
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0011F9DC File Offset: 0x0011DBDC
	// (set) Token: 0x06001F49 RID: 8009 RVA: 0x0011F9F4 File Offset: 0x0011DBF4
	public bool State
	{
		get
		{
			return this._State;
		}
		set
		{
			this._State = value;
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0011FA00 File Offset: 0x0011DC00
	// (set) Token: 0x06001F4B RID: 8011 RVA: 0x0011FA18 File Offset: 0x0011DC18
	public Progress_Effect_Handle_Image Progress_Effect_Handle_Image_Component
	{
		get
		{
			return this._Progress_Effect_Handle_Image_Component;
		}
		set
		{
			bool flag = value != null;
			if (flag)
			{
				this.State = true;
				base.StopCoroutine(this.Wait_Display_Or_Hidden_Slider());
				base.StartCoroutine(this.Wait_Display_Or_Hidden_Slider());
			}
			else
			{
				this.State = false;
				this.Slider_Component.gameObject.SetActive(false);
			}
			this._Progress_Effect_Handle_Image_Component = value;
		}
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x0011FA7C File Offset: 0x0011DC7C
	private void FixedUpdate()
	{
		bool flag = !(this.Progress_Effect_Handle_Image_Component == null) && this.State;
		if (flag)
		{
			this.Slider_Component.value = this.Progress_Effect_Handle_Image_Component.Image_Progress_Component.fillAmount;
		}
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x0011FAC3 File Offset: 0x0011DCC3
	private IEnumerator Wait_Display_Or_Hidden_Slider()
	{
		yield return new WaitForSeconds(0.1f);
		this.Slider_Component.value = this.Progress_Effect_Handle_Image_Component.Image_Progress_Component.fillAmount;
		this.Slider_Component.gameObject.SetActive(this.Progress_Effect_Handle_Image_Component.Display_State);
		yield break;
	}

	// Token: 0x04001C8D RID: 7309
	private Slider _Slider_Component;

	// Token: 0x04001C8E RID: 7310
	private float _Current_Value;

	// Token: 0x04001C8F RID: 7311
	private bool _State;

	// Token: 0x04001C90 RID: 7312
	private Progress_Effect_Handle_Image _Progress_Effect_Handle_Image_Component;
}
