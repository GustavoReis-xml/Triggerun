using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class MaterialManager : MonoBehaviour
{
	// Token: 0x06001A57 RID: 6743 RVA: 0x000F9DF8 File Offset: 0x000F7FF8
	public static MaterialManager GetInstance()
	{
		bool flag = MaterialManager.mInst == null;
		if (flag)
		{
			MaterialManager.mGame = new GameObject("_fMaterialManager");
			UnityEngine.Object.DontDestroyOnLoad(MaterialManager.mGame);
			MaterialManager.mInst = MaterialManager.mGame.AddComponent<MaterialManager>();
		}
		return MaterialManager.mInst;
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x000F9E4C File Offset: 0x000F804C
	public void addOneMaterial(GameObject gameObj)
	{
		bool flag = this.iEndIndex - this.iStartIndex > this.iMaxNum;
		if (flag)
		{
			GameObject gameObject = this.allMaterial[this.iStartIndex];
			bool flag2 = gameObject != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			this.allMaterial.Remove(this.iStartIndex);
			this.iStartIndex++;
		}
		this.iEndIndex++;
		bool flag3 = this.iEndIndex == 0;
		if (flag3)
		{
			this.iStartIndex = 0;
		}
		this.allMaterial.Add(this.iEndIndex, gameObj);
	}

	// Token: 0x040014EF RID: 5359
	public static GameObject mGame;

	// Token: 0x040014F0 RID: 5360
	public static MaterialManager mInst;

	// Token: 0x040014F1 RID: 5361
	private Dictionary<int, GameObject> allMaterial = new Dictionary<int, GameObject>();

	// Token: 0x040014F2 RID: 5362
	private int iStartIndex = -1;

	// Token: 0x040014F3 RID: 5363
	private int iEndIndex = -1;

	// Token: 0x040014F4 RID: 5364
	private int iMaxNum = 200;
}
