using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DC RID: 476
public class HintMsgEffects : MonoBehaviour
{
	// Token: 0x0600159E RID: 5534 RVA: 0x000CA310 File Offset: 0x000C8510
	public void OnStart(int nMoveNum, float fMonveTime, float fDelayTime, int nMsgMoveType, int nType)
	{
		this.iMoveNum = nMoveNum;
		this.fFcardMoveTime = fMonveTime;
		this.fDealyDel = fDelayTime;
		this.m_nMsgMoveType = nMsgMoveType;
		bool flag = nType == 1;
		if (flag)
		{
			this.a_top = base.transform.Find("a_top/top").GetComponent<RawImage>();
			this.t_top = base.transform.Find("t_top/top").GetComponent<RawImage>();
		}
		bool flag2 = base.IsInvoking("ClearObj");
		if (flag2)
		{
			base.CancelInvoke("ClearObj");
		}
		base.Invoke("ClearObj", this.fDealyDel);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000CA3AC File Offset: 0x000C85AC
	private void Update()
	{
		bool flag = !this.isCanMove();
		if (!flag)
		{
			Tweener t = base.transform.DOLocalMove(this.veTarget, this.fFcardMoveTime, false);
			t.OnUpdate(delegate
			{
				bool flag2 = this.m_nMsgMoveType == 0 && base.transform.localPosition.y <= -110f;
				if (flag2)
				{
					this.ClearObj();
				}
				bool flag3 = this.m_nMsgMoveType == 1 && base.transform.localPosition.y > 0f;
				if (flag3)
				{
					this.ClearObj();
				}
			});
		}
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000CA3F8 File Offset: 0x000C85F8
	private bool isCanMove()
	{
		bool result = false;
		Vector3 moveVector = this.getMoveVector(this.iMoveNum);
		bool flag = this.veTarget != moveVector;
		if (flag)
		{
			this.veTarget = moveVector;
			result = true;
		}
		return result;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000CA438 File Offset: 0x000C8638
	private Vector3 getMoveVector(int nMove)
	{
		Vector3 zero = Vector3.zero;
		bool flag = this.m_nMsgMoveType == 0;
		if (flag)
		{
			zero.y = 100f - (float)nMove * 50f;
		}
		bool flag2 = this.m_nMsgMoveType == 1;
		if (flag2)
		{
			zero.y = -140f + (float)nMove * 40f;
		}
		return zero;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x000CA49C File Offset: 0x000C869C
	public void ClearObj()
	{
		this.veTarget = new Vector3(50000f, 50000f, 50000f);
		base.CancelInvoke("ClearObj");
		bool flag = this.t_top != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.t_top.texture);
		}
		bool flag2 = this.a_top != null;
		if (flag2)
		{
			UnityEngine.Object.Destroy(this.a_top.texture);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400107F RID: 4223
	public int iMoveNum;

	// Token: 0x04001080 RID: 4224
	private float fFcardMoveTime = 0.5f;

	// Token: 0x04001081 RID: 4225
	private float fDealyDel;

	// Token: 0x04001082 RID: 4226
	private int m_nMsgMoveType;

	// Token: 0x04001083 RID: 4227
	private Vector3 veTarget = new Vector3(50000f, 50000f, 50000f);

	// Token: 0x04001084 RID: 4228
	private RawImage a_top;

	// Token: 0x04001085 RID: 4229
	private RawImage t_top;
}
