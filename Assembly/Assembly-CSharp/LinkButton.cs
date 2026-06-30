using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021B RID: 539
public class LinkButton : Button
{
	// Token: 0x060017F8 RID: 6136 RVA: 0x000DE7C0 File Offset: 0x000DC9C0
	private void Update()
	{
		bool flag = !(base.targetGraphic == null);
		if (flag)
		{
			bool flag2 = this.linkText == null;
			if (flag2)
			{
				this.linkText = base.targetGraphic.GetComponent<Text>();
			}
			else
			{
				bool flag3 = this.sText != this.linkText.text;
				if (flag3)
				{
					this.sText = this.linkText.text;
					this.CreateLink();
				}
			}
		}
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000DE83C File Offset: 0x000DCA3C
	protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		bool flag = !(this.linkText == null) && !(this.underlineText == null);
		if (flag)
		{
			bool flag2 = state == Selectable.SelectionState.Normal;
			if (flag2)
			{
				this.underlineText.color = base.colors.normalColor;
			}
			bool flag3 = state == Selectable.SelectionState.Highlighted;
			if (flag3)
			{
				this.underlineText.color = base.colors.highlightedColor;
			}
			bool flag4 = state == Selectable.SelectionState.Pressed;
			if (flag4)
			{
				this.underlineText.color = base.colors.pressedColor;
			}
			bool flag5 = state == Selectable.SelectionState.Disabled;
			if (flag5)
			{
				this.underlineText.color = base.colors.disabledColor;
			}
		}
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000DE910 File Offset: 0x000DCB10
	public void CreateLink()
	{
		bool flag = !(this.linkText == null);
		if (flag)
		{
			bool flag2 = this.underlineText == null;
			if (flag2)
			{
				this.underlineText = UnityEngine.Object.Instantiate<Text>(this.linkText);
				this.underlineText.name = "Underline";
				this.underlineText.transform.SetParent(this.linkText.transform);
				this.underlineText.transform.localScale = Vector3.one;
				this.underlineText.rectTransform.anchoredPosition3D = this.linkText.rectTransform.anchoredPosition3D;
				this.underlineText.rectTransform.offsetMax = this.linkText.rectTransform.offsetMax;
				this.underlineText.rectTransform.offsetMin = this.linkText.rectTransform.offsetMin;
				this.underlineText.rectTransform.anchorMax = this.linkText.rectTransform.anchorMax;
				this.underlineText.rectTransform.anchorMin = this.linkText.rectTransform.anchorMin;
				this.underlineText.raycastTarget = false;
			}
			this.underlineText.text = "_";
			float preferredWidth = this.underlineText.preferredWidth;
			float preferredWidth2 = this.linkText.preferredWidth;
			int num = (int)Mathf.Round(preferredWidth2 / preferredWidth);
			for (int i = 1; i < num; i++)
			{
				Text text = this.underlineText;
				text.text += "_";
			}
		}
	}

	// Token: 0x040012CF RID: 4815
	private Text linkText;

	// Token: 0x040012D0 RID: 4816
	private Text underlineText;

	// Token: 0x040012D1 RID: 4817
	private string sText = string.Empty;
}
