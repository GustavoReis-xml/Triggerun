using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class CReport : MonoBehaviour
{
	// Token: 0x06000756 RID: 1878 RVA: 0x00041070 File Offset: 0x0003F270
	private void OnGUI()
	{
		CrashReport[] reports = CrashReport.reports;
		bool flag = reports.Length != 0;
		if (flag)
		{
			string text = string.Empty;
			bool flag2 = fGameManager.Heroplayer != null && fGameManager.Heroplayer.playerCommon != null;
			if (flag2)
			{
				text = text + fGameManager.Heroplayer.playerCommon.m_sName + " |||";
			}
			CrashReport[] array = reports;
			foreach (CrashReport crashReport in array)
			{
				text = text + crashReport.text + " |||";
			}
			Debug.LogError(text);
		}
	}
}
