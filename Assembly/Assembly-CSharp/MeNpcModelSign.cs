using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000247 RID: 583
public class MeNpcModelSign : MonoBehaviour
{
	// Token: 0x06001A6B RID: 6763 RVA: 0x000FAED8 File Offset: 0x000F90D8
	public void OnStart(NetLiving _Npc)
	{
		this.Npc = _Npc;
		bool flag = !(gameuiCon.inst == null);
		if (flag)
		{
			this.oNpcSign = gameuiCon.inst.mainMsgUI.AddNpcObj(_Npc.getNetid());
			this.oNpcSign.SetActive(false);
			this.zhenmianUI = this.oNpcSign.transform.Find("zhenmian").gameObject;
			this.npc_stat = this.zhenmianUI.transform.Find("npc_stat").gameObject;
			this.npc_ico = this.zhenmianUI.transform.Find("npc_ico").gameObject;
			this.imgIco = this.npc_ico.GetComponent<RawImage>();
			sysload.loadImage(this.imgIco, "skillicon/" + ((Npc_Entity)this.Npc).Icon + ".txt", null);
			_Npc.oGameUISignObj = this.oNpcSign;
			this.jiantou = this.zhenmianUI.transform.Find("jiantou").gameObject;
			this.jiantou1 = this.zhenmianUI.transform.Find("jiantou/Image").gameObject;
			this.zhenmianUI.SetActive(false);
			this.otherUI = this.oNpcSign.transform.Find("other").gameObject;
			this.jtico = this.otherUI.transform.Find("ico").gameObject;
			this.jtjt = this.otherUI.transform.Find("jt").gameObject;
			this.otherUI.SetActive(false);
			this.vSize = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
		}
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000FB0AC File Offset: 0x000F92AC
	private void Update()
	{
		bool flag = !(this.oNpcSign == null) && this.Npc != null && !(((Npc_Entity)this.Npc).thisModel == null);
		if (flag)
		{
			bool flag2 = !this.oNpcSign.activeSelf;
			if (flag2)
			{
				this.oNpcSign.SetActive(true);
			}
			bool flag3 = this.Npc.mModelTopObjbool != this.oNpcSign.activeSelf;
			if (flag3)
			{
				this.oNpcSign.SetActive(this.Npc.mModelTopObjbool);
			}
			this.CheckWay();
		}
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x000FB154 File Offset: 0x000F9354
	private void CheckWay()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = this.vSize.x / num;
		float num4 = this.vSize.y / num2;
		Camera controlCamera = CameraControl.Instance.controlCamera;
		Transform transform = ((Npc_Entity)this.Npc).thisModel.transform;
		float num5 = (float)((Npc_Entity)this.Npc).SignHeight * 0.001f;
		float num6 = Vector3.Distance(transform.position, controlCamera.transform.position);
		num5 += 0.06f * num6;
		Vector3 vector = transform.position + Vector3.up * num5;
		Vector3 vector2 = controlCamera.WorldToScreenPoint(vector);
		bool flag = vector2.z > 0f && vector2.x > 0f && vector2.y > 0f && vector2.x < (float)Screen.width && vector2.y < (float)Screen.height;
		if (flag)
		{
			this.scrPos = vector2;
			this.scrPos.z = 0f;
			this.scrPos.x = (this.scrPos.x - (float)Screen.width * 0.5f) * this.vSize.x / (float)Screen.width;
			this.scrPos.y = (this.scrPos.y - (float)Screen.height * 0.5f) * this.vSize.y / (float)Screen.height;
		}
		else
		{
			bool flag2 = vector2.z < 0f;
			if (flag2)
			{
				vector2.x = (float)Screen.width - vector2.x;
				vector2.y = (float)Screen.height - vector2.y;
			}
			Vector3 vector3 = new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
			vector2 -= vector3;
			float num7 = Mathf.Atan2(vector2.y, vector2.x);
			num7 -= 1.5707964f;
			float num8 = Mathf.Cos(num7);
			float num9 = 0f - Mathf.Sin(num7);
			vector2 = vector3 + new Vector3(num9 * 150f, num8 * 150f, 0f);
			float num10 = num8 / num9;
			Vector3 vector4 = vector3 * 0.9f;
			vector2 = ((num8 <= 0f) ? new Vector3((0f - vector4.y) / num10, 0f - vector4.y, 0f) : new Vector3(vector4.y / num10, vector4.y, 0f));
			bool flag3 = vector2.x > vector4.x;
			if (flag3)
			{
				vector2 = new Vector3(vector4.x, vector4.x * num10, 0f);
			}
			else
			{
				bool flag4 = vector2.x < 0f - vector4.x;
				if (flag4)
				{
					vector2 = new Vector3(0f - vector4.x, (0f - vector4.x) * num10, 0f);
				}
			}
			vector2 += vector3;
			this.scrPos = vector2;
			this.scrPos.z = 0f;
			this.scrPos.x = (this.scrPos.x - (float)Screen.width * 0.5f) * this.vSize.x / (float)Screen.width;
			this.scrPos.y = (this.scrPos.y - (float)Screen.height * 0.5f) * this.vSize.y / (float)Screen.height;
		}
		float num11 = num * num3 * 0.5f - 50f;
		float num12 = num2 * num4 * 0.5f - 50f;
		float num13 = (0f - num) * num3 * 0.5f + 50f;
		float num14 = (0f - num2) * num4 * 0.5f + 150f;
		bool flag5 = this.scrPos.x < num13;
		if (flag5)
		{
			this.scrPos.x = num13;
		}
		else
		{
			bool flag6 = this.scrPos.x > num11;
			if (flag6)
			{
				this.scrPos.x = num11;
			}
		}
		bool flag7 = this.scrPos.y < num14;
		if (flag7)
		{
			this.scrPos.y = num14;
		}
		else
		{
			bool flag8 = this.scrPos.y > num12;
			if (flag8)
			{
				this.scrPos.y = num12;
			}
		}
		this.oNpcSign.transform.localPosition = this.scrPos;
		this.CheckStat(this.scrPos, vector);
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x000FB638 File Offset: 0x000F9838
	public void OnDel()
	{
		bool flag = this.Npc != null && this.Npc.oGameUISignObj != null && gameuiCon.inst != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.imgIco.texture);
			UnityEngine.Object.Destroy(this.Npc.oGameUISignObj);
		}
		this.Npc = null;
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x000FB6A0 File Offset: 0x000F98A0
	private void CheckStat(Vector3 szReturn, Vector3 bAt)
	{
		float num = Mathf.Atan2(szReturn.x, szReturn.y) * 57.29578f;
		num = 360f - num;
		float num2 = (float)Screen.width;
		float num3 = (float)Screen.height;
		Vector3 vector = Vector3.zero;
		vector = CameraControl.Instance.controlCamera.WorldToScreenPoint(bAt);
		bool flag = vector.x < 0f || vector.x > num2 || vector.y < 150f || vector.y > num3 || vector.z <= 0f;
		if (flag)
		{
			bool activeSelf = this.zhenmianUI.activeSelf;
			if (activeSelf)
			{
				this.zhenmianUI.SetActive(false);
			}
			bool flag2 = !this.otherUI.activeSelf;
			if (flag2)
			{
				this.otherUI.SetActive(true);
			}
			bool flag3 = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat != this.nFightStat;
			if (flag3)
			{
				bool flag4 = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat == 1;
				if (flag4)
				{
					this.jtico.GetComponent<Image>().color = new Color(1f, 0.59607846f, 0f, 1f);
					this.jtjt.GetComponent<Image>().color = new Color(1f, 0.59607846f, 0f, 1f);
				}
				else
				{
					this.jtico.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
					this.jtjt.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
				}
				this.nFightStat = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat;
				this.nFightStat1 = -1;
			}
			this.otherUI.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, num);
		}
		else
		{
			bool activeSelf2 = this.otherUI.activeSelf;
			if (activeSelf2)
			{
				this.otherUI.SetActive(false);
			}
			bool flag5 = !this.zhenmianUI.activeSelf;
			if (flag5)
			{
				this.zhenmianUI.SetActive(true);
			}
			bool flag6 = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat == this.nFightStat1;
			if (!flag6)
			{
				bool flag7 = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat == 1;
				if (flag7)
				{
					bool flag8 = !this.npc_stat.activeSelf;
					if (flag8)
					{
						this.npc_stat.SetActive(true);
					}
					this.imgIco.color = new Color(1f, 0.59607846f, 0f, 1f);
					this.jiantou.GetComponent<Image>().color = new Color(1f, 0.59607846f, 0f, 1f);
					this.jiantou1.GetComponent<Image>().color = new Color(1f, 0.59607846f, 0f, 1f);
					this.npc_stat.GetComponent<Image>().color = new Color(1f, 0.59607846f, 0f, 1f);
				}
				else
				{
					bool activeSelf3 = this.npc_stat.activeSelf;
					if (activeSelf3)
					{
						this.npc_stat.SetActive(false);
					}
					this.imgIco.color = new Color(0f, 1f, 1f, 1f);
					this.jiantou.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
					this.jiantou1.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
					this.npc_stat.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
				}
				this.nFightStat1 = ((Npc_Entity)this.Npc).playerCommon.m_nFightStat;
				this.nFightStat = -1;
			}
		}
	}

	// Token: 0x04001511 RID: 5393
	private NetLiving Npc;

	// Token: 0x04001512 RID: 5394
	private GameObject oNpcSign;

	// Token: 0x04001513 RID: 5395
	private GameObject zhenmianUI;

	// Token: 0x04001514 RID: 5396
	private GameObject npc_stat;

	// Token: 0x04001515 RID: 5397
	private GameObject npc_ico;

	// Token: 0x04001516 RID: 5398
	private GameObject jiantou;

	// Token: 0x04001517 RID: 5399
	private GameObject jiantou1;

	// Token: 0x04001518 RID: 5400
	private RawImage imgIco;

	// Token: 0x04001519 RID: 5401
	private GameObject otherUI;

	// Token: 0x0400151A RID: 5402
	private GameObject jtico;

	// Token: 0x0400151B RID: 5403
	private GameObject jtjt;

	// Token: 0x0400151C RID: 5404
	private Vector2 vSize = Vector2.zero;

	// Token: 0x0400151D RID: 5405
	private Vector3 scrPos = Vector3.zero;

	// Token: 0x0400151E RID: 5406
	private const float borderUnit = 50f;

	// Token: 0x0400151F RID: 5407
	private const float bottomBorderUnit = 150f;

	// Token: 0x04001520 RID: 5408
	private int nFightStat = -1;

	// Token: 0x04001521 RID: 5409
	private int nFightStat1 = -1;
}
