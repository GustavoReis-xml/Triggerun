using System;
using UnityEngine.UI;

// Token: 0x02000496 RID: 1174
public class WaiGuaUICon : Window
{
	// Token: 0x06002D72 RID: 11634 RVA: 0x0018FEB9 File Offset: 0x0018E0B9
	protected override void Awake()
	{
		base.Awake();
		this.bgRimg = base.transform.GetComponent<RawImage>();
		WaiGuaUICon.inst = this;
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x0018FEDA File Offset: 0x0018E0DA
	private void Start()
	{
		this.ShowUIData();
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x0018FEE4 File Offset: 0x0018E0E4
	public void ShowUIData()
	{
		bool isWaiGuaOpen = LiBaoManager.isWaiGuaOpen;
		if (isWaiGuaOpen)
		{
			fUIManager.GetInstance().GetIconAsset("libao/" + LiBaoManager.GetInstance().sWaiGuaBg + ".txt", this.bgRimg);
		}
	}

	// Token: 0x04002684 RID: 9860
	public static WaiGuaUICon inst;

	// Token: 0x04002685 RID: 9861
	private RawImage bgRimg;
}
