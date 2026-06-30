using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200021C RID: 540
[AddComponentMenu("UI/LinkImageText", 10)]
public class LinkImageText : Text, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x060017FC RID: 6140 RVA: 0x000DEAD0 File Offset: 0x000DCCD0
	// (set) Token: 0x060017FD RID: 6141 RVA: 0x000DEAE8 File Offset: 0x000DCCE8
	public LinkImageText.HrefClickEvent onHrefClick
	{
		get
		{
			return this.m_OnHrefClick;
		}
		set
		{
			this.m_OnHrefClick = value;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x060017FE RID: 6142 RVA: 0x000DEAF4 File Offset: 0x000DCCF4
	// (set) Token: 0x060017FF RID: 6143 RVA: 0x000DEB0C File Offset: 0x000DCD0C
	public LinkImageText.HrefEnterEvent onHrefEnter
	{
		get
		{
			return this.m_OnHrefEnter;
		}
		set
		{
			this.m_OnHrefEnter = value;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06001800 RID: 6144 RVA: 0x000DEB18 File Offset: 0x000DCD18
	// (set) Token: 0x06001801 RID: 6145 RVA: 0x000DEB30 File Offset: 0x000DCD30
	public LinkImageText.HrefExitEvent onHrefExit
	{
		get
		{
			return this.m_OnHrefExit;
		}
		set
		{
			this.m_OnHrefExit = value;
		}
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000DEB3A File Offset: 0x000DCD3A
	public override void SetVerticesDirty()
	{
		base.SetVerticesDirty();
		this.UpdateQuadImage();
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x000DEB4C File Offset: 0x000DCD4C
	protected void UpdateQuadImage()
	{
		this.m_OutputText = this.GetOutputText(this.m_PreText);
		this.text = this.m_OutputText;
		this.m_ImagesVertexIndex.Clear();
		foreach (object obj in LinkImageText.s_ImageRegex.Matches(this.m_OutputText))
		{
			Match match = (Match)obj;
			int index = match.Index;
			int item = index * 4 + 3;
			this.m_ImagesVertexIndex.Add(item);
			this.m_ImagesPool.RemoveAll((Image image2) => image2 == null);
			bool flag = this.m_ImagesPool.Count == 0;
			if (flag)
			{
				base.GetComponentsInChildren<Image>(this.m_ImagesPool);
			}
			bool flag2 = this.m_ImagesVertexIndex.Count > this.m_ImagesPool.Count;
			if (flag2)
			{
				GameObject gameObject = DefaultControls.CreateImage(default(DefaultControls.Resources));
				gameObject.layer = base.gameObject.layer;
				RectTransform rectTransform = gameObject.transform as RectTransform;
				bool flag3 = rectTransform;
				if (flag3)
				{
					rectTransform.SetParent(base.rectTransform);
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localRotation = Quaternion.identity;
					rectTransform.localScale = Vector3.one;
				}
				this.m_ImagesPool.Add(gameObject.GetComponent<Image>());
			}
			string value = match.Groups[1].Value;
			float num = float.Parse(match.Groups[2].Value);
			Image image = this.m_ImagesPool[this.m_ImagesVertexIndex.Count - 1];
			bool flag4 = image.sprite == null || image.sprite.name != value;
			if (flag4)
			{
				image.sprite = ((LinkImageText.funLoadSprite == null) ? Resources.Load<Sprite>(value) : LinkImageText.funLoadSprite(value));
			}
			image.rectTransform.sizeDelta = new Vector2(num, num);
			image.enabled = true;
		}
		for (int i = this.m_ImagesVertexIndex.Count; i < this.m_ImagesPool.Count; i++)
		{
			bool flag5 = this.m_ImagesPool[i];
			if (flag5)
			{
				this.m_ImagesPool[i].enabled = false;
			}
		}
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000DEE10 File Offset: 0x000DD010
	protected override void OnPopulateMesh(VertexHelper toFill)
	{
		string text = this.m_Text;
		this.m_Text = this.m_OutputText;
		base.OnPopulateMesh(toFill);
		this.m_Text = text;
		UIVertex uivertex = default(UIVertex);
		for (int i = 0; i < this.m_ImagesVertexIndex.Count; i++)
		{
			int num = this.m_ImagesVertexIndex[i];
			RectTransform rectTransform = this.m_ImagesPool[i].rectTransform;
			Vector2 sizeDelta = rectTransform.sizeDelta;
			bool flag = num < toFill.currentVertCount;
			if (flag)
			{
				toFill.PopulateUIVertex(ref uivertex, num);
				rectTransform.anchoredPosition = new Vector2(uivertex.position.x + sizeDelta.x / 2f, uivertex.position.y + sizeDelta.y / 2f);
				toFill.PopulateUIVertex(ref uivertex, num - 3);
				Vector3 position = uivertex.position;
				int j = num;
				int num2 = num - 3;
				while (j > num2)
				{
					toFill.PopulateUIVertex(ref uivertex, num);
					uivertex.position = position;
					toFill.SetUIVertex(uivertex, j);
					j--;
				}
			}
		}
		bool flag2 = this.m_ImagesVertexIndex.Count != 0;
		if (flag2)
		{
			this.m_ImagesVertexIndex.Clear();
		}
		foreach (LinkImageText.HrefInfo hrefInfo in this.m_HrefInfos)
		{
			hrefInfo.boxes.Clear();
			bool flag3 = hrefInfo.startIndex >= toFill.currentVertCount;
			if (!flag3)
			{
				toFill.PopulateUIVertex(ref uivertex, hrefInfo.startIndex);
				Vector3 position2 = uivertex.position;
				Bounds bounds = new Bounds(position2, Vector3.zero);
				int num3 = hrefInfo.startIndex;
				int endIndex = hrefInfo.endIndex;
				while (num3 < endIndex && num3 < toFill.currentVertCount)
				{
					toFill.PopulateUIVertex(ref uivertex, num3);
					position2 = uivertex.position;
					bool flag4 = position2.x < bounds.min.x;
					if (flag4)
					{
						hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
						bounds = new Bounds(position2, Vector3.zero);
					}
					else
					{
						bounds.Encapsulate(position2);
					}
					num3++;
				}
				hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
			}
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000DF0E4 File Offset: 0x000DD2E4
	protected virtual string GetOutputText(string outputText)
	{
		LinkImageText.s_TextBuilder.Length = 0;
		this.m_HrefInfos.Clear();
		int num = 0;
		foreach (object obj in LinkImageText.s_HrefRegex.Matches(outputText))
		{
			Match match = (Match)obj;
			LinkImageText.s_TextBuilder.Append(outputText.Substring(num, match.Index - num));
			Group group = match.Groups[1];
			LinkImageText.HrefInfo item = new LinkImageText.HrefInfo
			{
				startIndex = LinkImageText.s_TextBuilder.Length * 4,
				endIndex = (LinkImageText.s_TextBuilder.Length + match.Groups[2].Length - 1) * 4 + 3,
				name = group.Value
			};
			this.m_HrefInfos.Add(item);
			LinkImageText.s_TextBuilder.Append(match.Groups[2].Value);
			num = match.Index + match.Length;
		}
		LinkImageText.s_TextBuilder.Append(outputText.Substring(num, outputText.Length - num));
		return LinkImageText.s_TextBuilder.ToString();
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000DF240 File Offset: 0x000DD440
	public void OnPointerClick(PointerEventData eventData)
	{
		Vector2 point;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, eventData.position, eventData.pressEventCamera, out point);
		foreach (LinkImageText.HrefInfo hrefInfo in this.m_HrefInfos)
		{
			List<Rect> boxes = hrefInfo.boxes;
			for (int i = 0; i < boxes.Count; i++)
			{
				bool flag = boxes[i].Contains(point);
				if (flag)
				{
					this.m_OnHrefClick.Invoke(hrefInfo.name);
					return;
				}
			}
		}
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x000DF2FC File Offset: 0x000DD4FC
	public void OnPointerEnter(PointerEventData eventData)
	{
		Vector2 point;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, eventData.position, eventData.pressEventCamera, out point);
		foreach (LinkImageText.HrefInfo hrefInfo in this.m_HrefInfos)
		{
			List<Rect> boxes = hrefInfo.boxes;
			for (int i = 0; i < boxes.Count; i++)
			{
				bool flag = boxes[i].Contains(point);
				if (flag)
				{
					this.m_OnHrefEnter.Invoke(hrefInfo.name);
					return;
				}
			}
		}
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x000DF3B8 File Offset: 0x000DD5B8
	public void OnPointerExit(PointerEventData eventData)
	{
		Vector2 point;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, eventData.position, eventData.pressEventCamera, out point);
		foreach (LinkImageText.HrefInfo hrefInfo in this.m_HrefInfos)
		{
			List<Rect> boxes = hrefInfo.boxes;
			for (int i = 0; i < boxes.Count; i++)
			{
				bool flag = boxes[i].Contains(point);
				if (flag)
				{
					this.m_OnHrefExit.Invoke(hrefInfo.name);
					return;
				}
			}
		}
	}

	// Token: 0x040012D2 RID: 4818
	private string m_OutputText;

	// Token: 0x040012D3 RID: 4819
	public string m_PreText = string.Empty;

	// Token: 0x040012D4 RID: 4820
	protected readonly List<Image> m_ImagesPool = new List<Image>();

	// Token: 0x040012D5 RID: 4821
	private readonly List<int> m_ImagesVertexIndex = new List<int>();

	// Token: 0x040012D6 RID: 4822
	private readonly List<LinkImageText.HrefInfo> m_HrefInfos = new List<LinkImageText.HrefInfo>();

	// Token: 0x040012D7 RID: 4823
	protected static readonly StringBuilder s_TextBuilder = new StringBuilder();

	// Token: 0x040012D8 RID: 4824
	[SerializeField]
	private LinkImageText.HrefClickEvent m_OnHrefClick = new LinkImageText.HrefClickEvent();

	// Token: 0x040012D9 RID: 4825
	[SerializeField]
	private LinkImageText.HrefEnterEvent m_OnHrefEnter = new LinkImageText.HrefEnterEvent();

	// Token: 0x040012DA RID: 4826
	[SerializeField]
	private LinkImageText.HrefExitEvent m_OnHrefExit = new LinkImageText.HrefExitEvent();

	// Token: 0x040012DB RID: 4827
	private static readonly Regex s_ImageRegex = new Regex("<quad name=(.+?) size=(\\d*\\.?\\d+%?) width=(\\d*\\.?\\d+%?) />", RegexOptions.Singleline);

	// Token: 0x040012DC RID: 4828
	private static readonly Regex s_HrefRegex = new Regex("<h=([^>\\n\\s]+)>(.*?)(</h>)", RegexOptions.Singleline);

	// Token: 0x040012DD RID: 4829
	public static Func<string, Sprite> funLoadSprite;

	// Token: 0x020005DF RID: 1503
	[Serializable]
	public class HrefClickEvent : UnityEvent<string>
	{
	}

	// Token: 0x020005E0 RID: 1504
	[Serializable]
	public class HrefEnterEvent : UnityEvent<string>
	{
	}

	// Token: 0x020005E1 RID: 1505
	[Serializable]
	public class HrefExitEvent : UnityEvent<string>
	{
	}

	// Token: 0x020005E2 RID: 1506
	private class HrefInfo
	{
		// Token: 0x04002E80 RID: 11904
		public int startIndex;

		// Token: 0x04002E81 RID: 11905
		public int endIndex;

		// Token: 0x04002E82 RID: 11906
		public string name;

		// Token: 0x04002E83 RID: 11907
		public readonly List<Rect> boxes = new List<Rect>();
	}
}
