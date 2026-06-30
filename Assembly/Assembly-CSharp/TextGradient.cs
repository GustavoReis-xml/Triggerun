using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000473 RID: 1139
[AddComponentMenu("UI/Effects/Gradient")]
public class TextGradient : BaseMeshEffect
{
	// Token: 0x06002C66 RID: 11366 RVA: 0x00186A3C File Offset: 0x00184C3C
	public override void ModifyMesh(VertexHelper vh)
	{
		bool flag = this.IsActive();
		if (flag)
		{
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			int count = list.Count;
			this.ApplyGradient(list, 0, count);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x00186A84 File Offset: 0x00184C84
	private void ApplyGradient(List<UIVertex> vertexList, int start, int end)
	{
		bool flag = vertexList.Count == 0;
		if (!flag)
		{
			float num = vertexList[0].position.y;
			float num2 = vertexList[0].position.y;
			for (int i = start; i < end; i++)
			{
				float y = vertexList[i].position.y;
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
			for (int j = start; j < end; j++)
			{
				UIVertex uivertex = vertexList[j];
				uivertex.color = Color32.Lerp(this.bottomColor, this.topColor, (uivertex.position.y - num) / num3);
				vertexList[j] = uivertex;
			}
		}
	}

	// Token: 0x040025C8 RID: 9672
	[SerializeField]
	private Color32 topColor = Color.white;

	// Token: 0x040025C9 RID: 9673
	[SerializeField]
	private Color32 bottomColor = Color.black;
}
