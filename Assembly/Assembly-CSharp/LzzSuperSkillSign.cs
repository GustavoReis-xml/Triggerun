using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022B RID: 555
public class LzzSuperSkillSign : Window
{
	// Token: 0x060018F8 RID: 6392 RVA: 0x000EA020 File Offset: 0x000E8220
	protected override void Awake()
	{
		base.Awake();
		LzzSuperSkillSign.Inst = this;
		this.oZx = base.transform.Find("zx").gameObject;
		this.iImage = base.transform.Find("img").GetComponent<Image>();
		this.iImage.enabled = false;
		this.iImage.transform.localScale = Vector3.one;
		this.vScale = this.iImage.transform.localScale;
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x000EA0AA File Offset: 0x000E82AA
	private void Update()
	{
		this.CheckSign();
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x000EA0B4 File Offset: 0x000E82B4
	private void CheckSign()
	{
		Vector3 vector = Vector3.zero;
		NetLiving netLiving = Fun.Get_NetLiving(this.m_nNetId);
		bool flag = netLiving == null || netLiving.thisModel == null || netLiving.userHeadTransform == null || netLiving.haveDestroy;
		if (!flag)
		{
			bool isDead = netLiving.isDead;
			if (isDead)
			{
				UnityEngine.Object.DestroyImmediate(base.transform.gameObject);
			}
			else
			{
				vector = this.getScreenVector(netLiving.userHeadTransform.transform.position);
				bool flag2 = vector == this.VeInit;
				if (flag2)
				{
					bool enabled = this.iImage.enabled;
					if (enabled)
					{
						this.iImage.enabled = false;
					}
				}
				else
				{
					base.transform.localPosition = vector;
					string text = "daquan";
					bool flag3 = this.fScale <= 0.5f;
					float num;
					if (flag3)
					{
						text = "hongquan";
						this.iImage.transform.localScale = Vector3.one;
						this.iImage.rectTransform.sizeDelta = new Vector2(80f, 80f);
						bool activeSelf = this.oZx.activeSelf;
						if (activeSelf)
						{
							this.oZx.SetActive(false);
						}
						num = 100f;
					}
					else
					{
						bool flag4 = this.fScale != this.vScale.x;
						if (flag4)
						{
							this.vScale = new Vector3(this.fScale, this.fScale, this.fScale);
							this.iImage.transform.transform.DOScale(this.vScale, 0.1f);
							this.iImage.rectTransform.sizeDelta = new Vector2(150f, 150f);
						}
						bool flag5 = !this.oZx.activeSelf;
						if (flag5)
						{
							this.oZx.SetActive(true);
						}
						num = 80f;
					}
					bool flag6 = this.sSpriltname != text;
					if (flag6)
					{
						this.iImage.sprite = Window.spriteName(AtlasType.ATLAS_ZHUNXING, text);
						this.sSpriltname = text;
					}
					this.nZ = this.iImage.transform.localEulerAngles.z;
					this.nZ -= Time.deltaTime * num;
					bool flag7 = this.nZ <= -360f;
					if (flag7)
					{
						this.nZ = 0f;
					}
					this.iImage.transform.localRotation = Quaternion.Euler(0f, 0f, this.nZ);
					bool flag8 = !this.iImage.enabled;
					if (flag8)
					{
						this.iImage.enabled = true;
					}
				}
			}
		}
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x000EA388 File Offset: 0x000E8588
	private Vector3 getScreenVector(Vector3 bAt)
	{
		Vector3 result = new Vector3(-1f, -1f, -1f);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		bAt.y -= 0.2f;
		Vector3 vector = CameraControl.Instance.controlCamera.WorldToScreenPoint(bAt);
		bool flag = vector.x >= 0f && vector.x <= num && vector.y >= 0f && vector.y <= num2 && vector.z > 0f;
		if (flag)
		{
			Vector2 sizeDelta = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
			float num3 = vector.x / num;
			float num4 = vector.y / num2;
			float x = sizeDelta.x * num3 - sizeDelta.x / 2f;
			float y = sizeDelta.y * num4 - sizeDelta.y / 2f;
			result.x = x;
			result.y = y;
			result.z = 0f;
		}
		return result;
	}

	// Token: 0x040013B4 RID: 5044
	public static LzzSuperSkillSign Inst;

	// Token: 0x040013B5 RID: 5045
	public int m_nNetId;

	// Token: 0x040013B6 RID: 5046
	public float fScale = 1f;

	// Token: 0x040013B7 RID: 5047
	private Image iImage;

	// Token: 0x040013B8 RID: 5048
	private Vector3 vScale = Vector3.one;

	// Token: 0x040013B9 RID: 5049
	private GameObject oZx;

	// Token: 0x040013BA RID: 5050
	private float nZ;

	// Token: 0x040013BB RID: 5051
	private Vector3 VeInit = new Vector3(-1f, -1f, -1f);

	// Token: 0x040013BC RID: 5052
	private string sSpriltname = string.Empty;
}
