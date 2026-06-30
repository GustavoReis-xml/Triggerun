using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200048B RID: 1163
public class UnderlineText : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06002CF0 RID: 11504 RVA: 0x0018B57C File Offset: 0x0018977C
	private void Start()
	{
		bool flag = this.tarText != null;
		if (flag)
		{
			this.tarText.color = this.colors.normalColor;
		}
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x0018B5B4 File Offset: 0x001897B4
	private void Update()
	{
		bool flag = !(this.tarText == null);
		if (flag)
		{
			bool flag2 = this.sText != this.tarText.text;
			if (flag2)
			{
				this.sText = this.tarText.text;
				this.CreateLink();
			}
			bool flag3 = this.underlineText != null;
			if (flag3)
			{
				this.underlineText.rectTransform.anchoredPosition3D = Vector3.zero;
			}
		}
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x0018B634 File Offset: 0x00189834
	public void CreateLink()
	{
		bool flag = !(this.tarText == null);
		if (flag)
		{
			bool flag2 = this.underlineText == null;
			if (flag2)
			{
				this.underlineText = UnityEngine.Object.Instantiate<Text>(this.tarText);
				this.underlineText.name = "Underline";
				this.underlineText.alignment = TextAnchor.UpperLeft;
				this.underlineText.transform.SetParent(this.tarText.transform);
				this.underlineText.transform.localPosition = Vector3.zero;
				this.underlineText.transform.localScale = Vector3.one;
				this.underlineText.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.underlineText.rectTransform.pivot = new Vector2(0f, 1f);
				this.underlineText.rectTransform.anchorMax = new Vector2(0f, 1f);
				this.underlineText.rectTransform.anchorMin = new Vector2(0f, 1f);
				this.underlineText.raycastTarget = false;
			}
			this.underlineText.text = "_";
			float preferredWidth = this.underlineText.preferredWidth;
			float preferredWidth2 = this.tarText.preferredWidth;
			int num = (int)Mathf.Round(preferredWidth2 / preferredWidth);
			for (int i = 1; i < num; i++)
			{
				Text text = this.underlineText;
				text.text += "_";
			}
		}
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x0018B7D8 File Offset: 0x001899D8
	public void OnPointerEnter(PointerEventData eventData)
	{
		bool flag = !(this.tarText == null) && !(this.underlineText == null);
		if (flag)
		{
			this.tarText.color = this.colors.highlightedColor;
			this.underlineText.color = this.colors.highlightedColor;
		}
	}

	// Token: 0x06002CF4 RID: 11508 RVA: 0x0018B83C File Offset: 0x00189A3C
	public void OnPointerExit(PointerEventData eventData)
	{
		bool flag = !(this.tarText == null) && !(this.underlineText == null);
		if (flag)
		{
			this.tarText.color = this.colors.normalColor;
			this.underlineText.color = this.colors.normalColor;
		}
	}

	// Token: 0x06002CF5 RID: 11509 RVA: 0x0018B8A0 File Offset: 0x00189AA0
	public void OnPointerDown(PointerEventData eventData)
	{
		bool flag = !(this.tarText == null) && !(this.underlineText == null);
		if (flag)
		{
			this.tarText.color = this.colors.pressedColor;
			this.underlineText.color = this.colors.pressedColor;
		}
	}

	// Token: 0x06002CF6 RID: 11510 RVA: 0x0018B904 File Offset: 0x00189B04
	public void OnPointerUp(PointerEventData eventData)
	{
		bool flag = !(this.tarText == null) && !(this.underlineText == null);
		if (flag)
		{
			this.tarText.color = this.colors.highlightedColor;
			this.underlineText.color = this.colors.highlightedColor;
		}
	}

	// Token: 0x0400261C RID: 9756
	public Text tarText;

	// Token: 0x0400261D RID: 9757
	public ColorBlock colors = ColorBlock.defaultColorBlock;

	// Token: 0x0400261E RID: 9758
	private Text underlineText;

	// Token: 0x0400261F RID: 9759
	private string sText = string.Empty;
}
