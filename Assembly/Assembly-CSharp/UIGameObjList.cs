using System;
using UnityEngine;

// Token: 0x02000487 RID: 1159
public class UIGameObjList : MonoBehaviour
{
	// Token: 0x06002CC1 RID: 11457 RVA: 0x00189B14 File Offset: 0x00187D14
	public GameObject Get_GameObj(int nPos)
	{
		bool flag = nPos < 0 || nPos >= this.gameobjs.Length;
		GameObject result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.gameobjs[nPos];
		}
		return result;
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x00189B4C File Offset: 0x00187D4C
	public void Set_GameObj(int nPos, GameObject obj)
	{
		bool flag = nPos >= 0 && nPos < this.gameobjs.Length;
		if (flag)
		{
			this.gameobjs[nPos] = obj;
		}
	}

	// Token: 0x040025EF RID: 9711
	public GameObject[] gameobjs = new GameObject[0];

	// Token: 0x040025F0 RID: 9712
	public string[] gameobjname = new string[0];
}
