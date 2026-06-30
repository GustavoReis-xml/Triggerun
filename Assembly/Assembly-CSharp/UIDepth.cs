using System;
using UnityEngine;

// Token: 0x02000486 RID: 1158
public class UIDepth : MonoBehaviour
{
	// Token: 0x06002CBC RID: 11452 RVA: 0x00189A29 File Offset: 0x00187C29
	private void Start()
	{
		this.ChangeDepth();
	}

	// Token: 0x06002CBD RID: 11453 RVA: 0x00189A34 File Offset: 0x00187C34
	private void Update()
	{
		bool flag = this.isUpdata;
		if (flag)
		{
			this.ChangeDepth();
			this.isUpdata = false;
		}
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x00189A5C File Offset: 0x00187C5C
	public void SetUIDepth(int nOrder, bool IsUI = true)
	{
		this.order = nOrder;
		this.isUI = IsUI;
		this.ChangeDepth();
	}

	// Token: 0x06002CBF RID: 11455 RVA: 0x00189A74 File Offset: 0x00187C74
	private void ChangeDepth()
	{
		bool flag = this.isUI;
		if (flag)
		{
			Canvas canvas = base.GetComponent<Canvas>();
			bool flag2 = canvas == null;
			if (flag2)
			{
				canvas = base.gameObject.AddComponent<Canvas>();
			}
			canvas.overrideSorting = true;
			canvas.sortingOrder = this.order;
		}
		else
		{
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>(true);
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.sortingOrder = this.order;
			}
		}
	}

	// Token: 0x040025EC RID: 9708
	public int order;

	// Token: 0x040025ED RID: 9709
	public bool isUI = true;

	// Token: 0x040025EE RID: 9710
	public bool isUpdata;
}
