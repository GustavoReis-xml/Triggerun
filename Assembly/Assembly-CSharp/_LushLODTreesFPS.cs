using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D9 RID: 1241
public class _LushLODTreesFPS : MonoBehaviour
{
	// Token: 0x06002F77 RID: 12151 RVA: 0x001A8DA8 File Offset: 0x001A6FA8
	private void Update()
	{
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		float num = 1f / this.deltaTime;
		this.FPSText.text = "FPS: " + ((int)num).ToString();
		bool flag = Input.GetKeyDown(KeyCode.Escape) && Application.isPlaying;
		if (flag)
		{
			Application.Quit();
		}
	}

	// Token: 0x0400294A RID: 10570
	public Text FPSText;

	// Token: 0x0400294B RID: 10571
	private float deltaTime;
}
