using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000245 RID: 581
public class MeBuffModelSign : MonoBehaviour
{
	// Token: 0x06001A5F RID: 6751 RVA: 0x000FA420 File Offset: 0x000F8620
	public void OnStart(int nBuffId, NetLiving oNetObj)
	{
		this.m_dNetObj = oNetObj;
		bool flag = !(gameuiCon.inst == null);
		if (flag)
		{
			this.oNpcSign = gameuiCon.inst.mainMsgUI.AddNpcObj(oNetObj.getNetid());
			this.oNpcSign.SetActive(false);
			this.zhenmianUI = this.oNpcSign.transform.Find("zhenmian").gameObject;
			this.npc_stat = this.zhenmianUI.transform.Find("npc_stat").gameObject;
			this.npc_stat.SetActive(false);
			this.npc_ico = this.zhenmianUI.transform.Find("npc_ico").gameObject;
			this.imgIco = this.npc_ico.GetComponent<RawImage>();
			this.m_dNetObj.oGameUISignObj = this.oNpcSign;
			this.zhenmianUI.SetActive(false);
			this.otherUI = this.oNpcSign.transform.Find("other").gameObject;
			this.jtico = this.otherUI.transform.Find("ico").gameObject;
			this.otherUI.SetActive(false);
			string str = "TaliyaZLSJ";
			this.imgIco.color = new Color(0f, 0.88235295f, 1f, 1f);
			this.jtico.GetComponent<Image>().color = new Color(0f, 0.88235295f, 1f, 1f);
			bool flag2 = nBuffId == 1010;
			if (flag2)
			{
				str = "MulanXJDB";
				this.imgIco.color = new Color(1f, 0f, 0f, 1f);
				this.jtico.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
			}
			sysload.loadImage(this.imgIco, "skillicon/" + str + ".txt", null);
			this.vSize = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
		}
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000FA64C File Offset: 0x000F884C
	public void OnDel()
	{
		bool flag = this.m_dNetObj != null && this.m_dNetObj.oGameUISignObj != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.imgIco.texture);
			UnityEngine.Object.Destroy(this.m_dNetObj.oGameUISignObj);
		}
		this.m_dNetObj = null;
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x000FA6A8 File Offset: 0x000F88A8
	private void Update()
	{
		bool flag = this.oNpcSign == null || this.m_dNetObj == null || this.m_dNetObj.thisModel == null;
		if (flag)
		{
			this.OnDel();
		}
		else
		{
			bool flag2 = this.m_dNetObj.isDead || this.m_dNetObj.haveDestroy;
			if (flag2)
			{
				this.OnDel();
			}
			else
			{
				bool flag3 = !this.oNpcSign.activeSelf;
				if (flag3)
				{
					this.oNpcSign.SetActive(true);
				}
				bool flag4 = this.m_dNetObj.mModelTopObjbool != this.oNpcSign.activeSelf;
				if (flag4)
				{
					this.oNpcSign.SetActive(this.m_dNetObj.mModelTopObjbool);
				}
				this.CheckWay();
			}
		}
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000FA77C File Offset: 0x000F897C
	private float GetHeadPos()
	{
		float num = 0f;
		bool flag = this.m_dNetObj.haveDestroy || this.m_dNetObj.userHeadTransform == null;
		float result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = this.m_dNetObj.netobject_type == 1;
			if (flag2)
			{
				num = ((GamePlayer)this.m_dNetObj).heroData.vHeadNamePos[1];
			}
			bool flag3 = this.m_dNetObj.netobject_type == 2;
			if (flag3)
			{
				num = (float)((Npc_Entity)this.m_dNetObj).SignHeight * 0.001f;
			}
			result = num + 0.3f;
		}
		return result;
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000FA824 File Offset: 0x000F8A24
	private void CheckWay()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = this.vSize.x / num;
		float num4 = this.vSize.y / num2;
		Camera controlCamera = CameraControl.Instance.controlCamera;
		Transform transform = this.m_dNetObj.thisModel.transform;
		float num5 = this.GetHeadPos();
		Vector3 position = transform.position;
		position.y = this.m_dNetObj.userHeadTransform.transform.position.y;
		float num6 = Vector3.Distance(transform.position, controlCamera.transform.position);
		num5 += 0.1f * num6;
		Vector3 vector = position + Vector3.up * num5;
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

	// Token: 0x06001A64 RID: 6756 RVA: 0x000FAD18 File Offset: 0x000F8F18
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
			this.otherUI.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, num);
		}
		else
		{
			bool activeSelf2 = this.otherUI.activeSelf;
			if (activeSelf2)
			{
				this.otherUI.SetActive(false);
			}
			bool flag3 = !this.zhenmianUI.activeSelf;
			if (flag3)
			{
				this.zhenmianUI.SetActive(true);
			}
		}
	}

	// Token: 0x04001504 RID: 5380
	private GameObject oNpcSign;

	// Token: 0x04001505 RID: 5381
	private GameObject zhenmianUI;

	// Token: 0x04001506 RID: 5382
	private GameObject npc_stat;

	// Token: 0x04001507 RID: 5383
	private GameObject npc_ico;

	// Token: 0x04001508 RID: 5384
	private RawImage imgIco;

	// Token: 0x04001509 RID: 5385
	private GameObject otherUI;

	// Token: 0x0400150A RID: 5386
	private GameObject jtico;

	// Token: 0x0400150B RID: 5387
	private Vector2 vSize = Vector2.zero;

	// Token: 0x0400150C RID: 5388
	private NetLiving m_dNetObj;

	// Token: 0x0400150D RID: 5389
	private Vector3 scrPos = Vector3.zero;

	// Token: 0x0400150E RID: 5390
	private const float borderUnit = 50f;

	// Token: 0x0400150F RID: 5391
	private const float bottomBorderUnit = 150f;
}
