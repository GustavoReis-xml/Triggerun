using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class CHeroWeaponCamera : MonoBehaviour
{
	// Token: 0x060004F0 RID: 1264 RVA: 0x00028A78 File Offset: 0x00026C78
	private void Awake()
	{
		this.miaozhunObj = Fun.FindGameObjectChild(base.gameObject, "miaozhun", false).gameObject;
		Canvas component = this.miaozhunObj.GetComponent<Canvas>();
		component.worldCamera = CameraControl.Instance.controlCamera;
		this.miaozhunObj.SetActive(false);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00028ACC File Offset: 0x00026CCC
	private void OnEnable()
	{
		bool flag = !(this.miaozhunObj == null);
		if (flag)
		{
			CameraControl.Instance.controlCamera.fieldOfView = 20f;
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00028B04 File Offset: 0x00026D04
	private void OnDisable()
	{
		bool flag = !(this.miaozhunObj == null);
		if (flag)
		{
			CameraControl.Instance.controlCamera.fieldOfView = SysConfig.cameraNormalFOV;
		}
	}

	// Token: 0x040003C9 RID: 969
	private HeroPlayer hero;

	// Token: 0x040003CA RID: 970
	private GameObject miaozhunObj;
}
