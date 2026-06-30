using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B4 RID: 1204
public class zhunxingBaolei : zhunxingBase
{
	// Token: 0x06002E89 RID: 11913 RVA: 0x0019FAA0 File Offset: 0x0019DCA0
	public override void setUi(Transform gcommonui, Transform gotherui)
	{
		base.setUi(gcommonui, gotherui);
		this.gTanke = gotherui.FindChild("tanke").gameObject;
		this.gYb_Left = this.gTanke.transform.Find("kd_left/yb").gameObject;
		this.gYb_Right = this.gTanke.transform.Find("kd_right/yb").gameObject;
		this.gJdt = this.gTanke.transform.Find("jdt").GetComponent<Image>();
		this.tTime = this.gTanke.transform.Find("time").GetComponent<Text>();
		GameObject gameObject = this.gTanke.transform.Find("zlist").gameObject;
		this.zsList = new List<Transform>();
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			bool flag = !(transform == null);
			if (flag)
			{
				this.zsList.Add(transform);
			}
		}
		this.gTanke.SetActive(false);
	}

	// Token: 0x06002E8A RID: 11914 RVA: 0x0019FBEC File Offset: 0x0019DDEC
	protected override void Update()
	{
		base.Update();
		this.checkYb();
	}

	// Token: 0x06002E8B RID: 11915 RVA: 0x0019FC00 File Offset: 0x0019DE00
	private void checkYb()
	{
		bool flag = this.hero == null || this.gTanke == null;
		if (!flag)
		{
			bool flag2 = this.hero.playerCommon.m_nWeaponGroup != 2;
			if (flag2)
			{
				bool activeSelf = this.gTanke.activeSelf;
				if (activeSelf)
				{
					this.gTanke.SetActive(false);
				}
				this.sDjsStartTime = string.Empty;
			}
			else
			{
				float num = 0f;
				this.gTanke.SetActive(true);
				bool flag3 = this.gYb_Left != null && this.fOldCy != CameraControl.Instance.cy;
				if (flag3)
				{
					Vector3 localPosition = this.gYb_Left.transform.localPosition;
					this.fOldCy = CameraControl.Instance.cy;
					bool flag4 = this.fOldCy < 0f;
					if (flag4)
					{
						num = this.ybLong * this.fOldCy / (float)CameraControl.Instance.yMinLimit;
					}
					bool flag5 = this.fOldCy > 0f;
					if (flag5)
					{
						num = (0f - this.ybLong) * this.fOldCy / (float)CameraControl.Instance.yMaxLimit;
					}
					localPosition.y = num;
					this.gYb_Left.transform.localPosition = localPosition;
				}
				bool flag6 = this.gYb_Right != null && this.fOldPz != this.hero.charMove.state.move.z;
				if (flag6)
				{
					Vector3 localPosition2 = this.gYb_Right.transform.localPosition;
					localPosition2.y = 0f;
					num = this.hero.charMove.state.move.z;
					float x = this.hero.charMove.state.move.x;
					bool flag7 = num > 0f;
					if (flag7)
					{
						localPosition2.y = 0f - this.ybLong;
						bool flag8 = x != 0f;
						if (flag8)
						{
							localPosition2.y += this.ybLong / 8f;
						}
					}
					bool flag9 = num < 0f;
					if (flag9)
					{
						localPosition2.y = this.ybLong;
						bool flag10 = x != 0f;
						if (flag10)
						{
							localPosition2.y -= this.ybLong / 8f;
						}
					}
					this.gYb_Right.transform.localPosition = localPosition2;
					this.fOldPz = num;
				}
				bool flag11 = !(this.hero.playerDazhao != null);
				if (!flag11)
				{
					bool flag12 = this.sDjsStartTime == string.Empty;
					if (flag12)
					{
						this.sDjsStartTime = Fun.Now.ToString();
						this.OnSetImage_Apa(0f);
					}
					float dcontinuetime = this.hero.playerDazhao.cdazhaomaininfo.dazhaoxml.dcontinuetime;
					DateTime d = DateTime.Parse(this.sDjsStartTime).AddSeconds((double)dcontinuetime);
					DateTime now = Fun.Now;
					float num2 = (float)(d - now).TotalSeconds;
					bool flag13 = num2 >= 0f;
					if (flag13)
					{
						float num3 = dcontinuetime - num2;
						bool flag14 = num3 <= 2f;
						if (flag14)
						{
							this.OnSetImage_Apa(num3 / 2f);
						}
						this.gJdt.fillAmount = num2 / dcontinuetime;
						this.tTime.text = Mathf.CeilToInt(num2).ToString();
					}
					else
					{
						this.gTanke.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x0019FFC8 File Offset: 0x0019E1C8
	private void OnSetImage_Apa(float fa)
	{
		foreach (Transform transform in this.zsList)
		{
			bool flag = !(transform == null);
			if (flag)
			{
				bool flag2 = transform.name == "z1_l" || transform.name == "z1_r" || transform.name == "z2_l" || transform.name == "z2_r";
				if (flag2)
				{
					transform.transform.localScale = new Vector3(1f, fa, 1f);
				}
				Color color = transform.GetComponent<Image>().color;
				color.a = fa;
				transform.GetComponent<Image>().color = color;
			}
		}
	}

	// Token: 0x04002841 RID: 10305
	private GameObject gTanke;

	// Token: 0x04002842 RID: 10306
	private GameObject gYb_Left;

	// Token: 0x04002843 RID: 10307
	private GameObject gYb_Right;

	// Token: 0x04002844 RID: 10308
	private List<Transform> zsList = new List<Transform>();

	// Token: 0x04002845 RID: 10309
	private Image gJdt;

	// Token: 0x04002846 RID: 10310
	private Text tTime;

	// Token: 0x04002847 RID: 10311
	private float ybLong = 165f;

	// Token: 0x04002848 RID: 10312
	private float fOldCy;

	// Token: 0x04002849 RID: 10313
	private float fOldPz;

	// Token: 0x0400284A RID: 10314
	private string sDjsStartTime = string.Empty;
}
