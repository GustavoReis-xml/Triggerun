using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class GameObject3DName : MonoBehaviour
{
	// Token: 0x06000C9D RID: 3229 RVA: 0x0007752C File Offset: 0x0007572C
	private void Awake()
	{
		this._showUi = base.transform.FindChild("show").gameObject;
		this.showUiTransform = this._showUi.GetComponent<Transform>();
		this.topNameObj = this._showUi.transform.FindChild("top").gameObject;
		this.lzname = this.topNameObj.transform.Find("name").GetComponent<MeshRenderer>();
		this.lzname.enabled = false;
		this._showUi.transform.FindChild("hp").gameObject.SetActive(false);
		this._showUi.transform.FindChild("newhp").gameObject.SetActive(false);
		this._showUi.transform.FindChild("ui_jiantou_bai").gameObject.SetActive(false);
		this._showUi.transform.FindChild("clonebuff").gameObject.SetActive(false);
		this._showUi.transform.FindChild("bufflist").gameObject.SetActive(false);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00077658 File Offset: 0x00075858
	public void setUI(string sName, Vector3 ve)
	{
		this.topNameObj.SetActive(true);
		Texture2D nameText2D = CCOpyNameManager.Instance.GetNameText2D(sName);
		this.lzname.material.mainTexture = nameText2D;
		this.lzname.enabled = true;
		base.transform.localPosition = ve;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x000776AC File Offset: 0x000758AC
	private void Update()
	{
		bool flag = this._showUi == null || CameraControl.Instance == null || CameraControl.Instance.controlCamera == null;
		if (!flag)
		{
			Vector3 position = base.transform.position;
			Vector3 position2 = CameraControl.Instance.controlCamera.transform.position;
			float num = Vector3.Distance(position, position2);
			bool flag2 = num <= 0.001f;
			if (!flag2)
			{
				bool flag3 = num > 200f;
				if (flag3)
				{
					bool activeSelf = this.topNameObj.activeSelf;
					if (activeSelf)
					{
						this.topNameObj.SetActive(false);
					}
				}
				else
				{
					bool flag4 = !this.topNameObj.activeSelf;
					if (flag4)
					{
						this.topNameObj.SetActive(true);
					}
					num = (float)((int)(num * 100f)) / 100f;
					this.CheckRotaion(position, position2);
					this.CheckSize(num);
				}
			}
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000777A4 File Offset: 0x000759A4
	private void CheckRotaion(Vector3 bAt, Vector3 bTo)
	{
		Vector3 normalized = (bTo - bAt).normalized;
		Quaternion rotation = Quaternion.LookRotation(normalized);
		Quaternion rotation2 = base.transform.parent.rotation;
		bool flag = this.qOldIon != rotation2 || this.v3dirOld != normalized;
		if (flag)
		{
			base.transform.rotation = rotation;
			this.qOldIon = rotation2;
			this.v3dirOld = normalized;
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0007781C File Offset: 0x00075A1C
	private void CheckSize(float fDis)
	{
		bool flag = fDis > 40f;
		if (flag)
		{
			fDis = 40f;
		}
		bool cameraZoom = CameraControl.Instance.CameraZoom;
		if (cameraZoom)
		{
			fDis = CameraControl.Instance.controlCamera.fieldOfView * fDis / SysConfig.cameraNormalFOV;
		}
		float num = fDis * 0.12f;
		bool flag2 = num < 1f;
		if (flag2)
		{
			num = 1f;
		}
		bool flag3 = this.fOldScale != num;
		if (flag3)
		{
			this.showUiTransform.localScale = new Vector3(num, num, 1f);
			this.fOldScale = num;
		}
	}

	// Token: 0x04000A74 RID: 2676
	private GameObject _showUi;

	// Token: 0x04000A75 RID: 2677
	private GameObject topNameObj;

	// Token: 0x04000A76 RID: 2678
	private Transform showUiTransform;

	// Token: 0x04000A77 RID: 2679
	private MeshRenderer lzname;

	// Token: 0x04000A78 RID: 2680
	private Quaternion qOldIon = Quaternion.identity;

	// Token: 0x04000A79 RID: 2681
	private Vector3 v3dirOld;

	// Token: 0x04000A7A RID: 2682
	private float fOldScale;
}
