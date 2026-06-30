using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class IHeroDataLeiZhenZi : IHeroData
{
	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06001604 RID: 5636 RVA: 0x000CD9C4 File Offset: 0x000CBBC4
	// (set) Token: 0x06001605 RID: 5637 RVA: 0x000CD9DC File Offset: 0x000CBBDC
	public bool Energy_Deduct_State
	{
		get
		{
			return this._Energy_Deduct_State;
		}
		set
		{
			this._Energy_Deduct_State = value;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06001606 RID: 5638 RVA: 0x000CD9E8 File Offset: 0x000CBBE8
	// (set) Token: 0x06001607 RID: 5639 RVA: 0x000CDA00 File Offset: 0x000CBC00
	public float Current_Energy
	{
		get
		{
			return this._Current_Energy;
		}
		set
		{
			this._Current_Energy = value;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06001608 RID: 5640 RVA: 0x000CDA0C File Offset: 0x000CBC0C
	// (set) Token: 0x06001609 RID: 5641 RVA: 0x000CDA24 File Offset: 0x000CBC24
	public float Max_Energy
	{
		get
		{
			return this._Max_Energy;
		}
		set
		{
			this._Max_Energy = value;
		}
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x000CDA30 File Offset: 0x000CBC30
	public Vector3 getScreenVector(Vector3 bAt)
	{
		Vector3 result = new Vector3(-1f, -1f, -1f);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
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

	// Token: 0x0600160B RID: 5643 RVA: 0x000CDB3C File Offset: 0x000CBD3C
	public LayerMask getMiaoZhunLayer(int force)
	{
		bool flag = force == 2;
		LayerMask result;
		if (flag)
		{
			result = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("force1_npc"));
		}
		else
		{
			result = (1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("force2_npc"));
		}
		return result;
	}

	// Token: 0x0400111A RID: 4378
	private bool _Energy_Deduct_State;

	// Token: 0x0400111B RID: 4379
	private float _Current_Energy = -99f;

	// Token: 0x0400111C RID: 4380
	private float _Max_Energy = -100f;
}
