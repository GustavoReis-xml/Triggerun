using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000158 RID: 344
[AddComponentMenu("UI/Effects/GradientTowColor")]
public class GradientTowColor : BaseMeshEffect
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x000904EC File Offset: 0x0008E6EC
	public override void ModifyMesh(VertexHelper helper)
	{
		bool flag = !this.IsActive() || helper.currentVertCount == 0;
		if (!flag)
		{
			List<UIVertex> list = new List<UIVertex>();
			helper.GetUIVertexStream(list);
			float num = list[0].position.y;
			float num2 = list[0].position.y;
			for (int i = 1; i < list.Count; i++)
			{
				float y = list[i].position.y;
				bool flag2 = y > num2;
				if (flag2)
				{
					num2 = y;
				}
				else
				{
					bool flag3 = y < num;
					if (flag3)
					{
						num = y;
					}
				}
			}
			float num3 = num2 - num;
			UIVertex uivertex = default(UIVertex);
			for (int j = 0; j < helper.currentVertCount; j++)
			{
				helper.PopulateUIVertex(ref uivertex, j);
				uivertex.color = Color32.Lerp(this.bottomColor, this.topColor, (uivertex.position.y - num) / num3);
				helper.SetUIVertex(uivertex, j);
			}
		}
	}

	// Token: 0x04000C6C RID: 3180
	public Color32 topColor = Color.white;

	// Token: 0x04000C6D RID: 3181
	public Color32 bottomColor = Color.black;
}
