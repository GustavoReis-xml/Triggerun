using System;
using TeamUtility.IO;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class CTestInput : MonoBehaviour
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x000437FA File Offset: 0x000419FA
	private void Start()
	{
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00043800 File Offset: 0x00041A00
	private void Update()
	{
		bool buttonDown = InputManager.GetButtonDown("Skill1", PlayerID.One);
		if (buttonDown)
		{
			Debug.Log("Skill1 Down");
		}
		bool keyDown = InputManager.GetKeyDown(KeyCode.C);
		if (keyDown)
		{
			Debug.Log("c Down");
		}
		bool button = InputManager.GetButton("Jump", PlayerID.One);
		if (button)
		{
			Debug.Log("Jump Down");
		}
	}
}
