using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000153 RID: 339
public class GetIconComponent : MonoBehaviour
{
	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0008E764 File Offset: 0x0008C964
	// (set) Token: 0x06000F87 RID: 3975 RVA: 0x0008E77C File Offset: 0x0008C97C
	public string _Url
	{
		get
		{
			return this.Url;
		}
		set
		{
			this.Url = value;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0008E788 File Offset: 0x0008C988
	// (set) Token: 0x06000F89 RID: 3977 RVA: 0x0008E7A0 File Offset: 0x0008C9A0
	public bool IsShow
	{
		get
		{
			return this.isShow;
		}
		set
		{
			this.isShow = value;
		}
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0008E7AC File Offset: 0x0008C9AC
	public void OnDestroy()
	{
		this.rawImage = base.transform.GetComponent<RawImage>();
		bool flag = this.rawImage != null && this.rawImage.texture != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.rawImage.texture);
			this.rawImage.texture = null;
		}
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0008E811 File Offset: 0x0008CA11
	public void OnDisable()
	{
		this.OnDestroy();
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x0008E81C File Offset: 0x0008CA1C
	public void OnEnable()
	{
		this.rawImage = base.transform.GetComponent<RawImage>();
		bool flag = !string.IsNullOrEmpty(this.Url) && this.rawImage != null && this.rawImage.texture == null;
		if (flag)
		{
			fUIManager.GetInstance().GetIconAsset(this.Url, this.rawImage);
		}
	}

	// Token: 0x04000C15 RID: 3093
	private string Url;

	// Token: 0x04000C16 RID: 3094
	private RawImage rawImage;

	// Token: 0x04000C17 RID: 3095
	private bool isShow = true;
}
