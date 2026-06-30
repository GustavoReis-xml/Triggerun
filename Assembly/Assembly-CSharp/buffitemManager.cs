using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class buffitemManager : MonoBehaviour
{
	// Token: 0x06000220 RID: 544 RVA: 0x0000E9DC File Offset: 0x0000CBDC
	public static buffitemManager GetInstance()
	{
		bool flag = buffitemManager.mInst == null;
		if (flag)
		{
			buffitemManager.mUIObj = new GameObject("_MapBuffs");
			UnityEngine.Object.DontDestroyOnLoad(buffitemManager.mUIObj);
			buffitemManager.mInst = buffitemManager.mUIObj.AddComponent<buffitemManager>();
			UnityEngine.Object.DontDestroyOnLoad(buffitemManager.mInst);
		}
		return buffitemManager.mInst;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000EA38 File Offset: 0x0000CC38
	public void SetParent(Transform t)
	{
		t.SetParent(buffitemManager.mUIObj.transform);
		t.gameObject.AddComponent<Rigidbody>().isKinematic = true;
	}

	// Token: 0x040001BE RID: 446
	private static buffitemManager mInst;

	// Token: 0x040001BF RID: 447
	private static GameObject mUIObj;
}
