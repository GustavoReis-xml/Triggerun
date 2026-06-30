using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class MovingContent : MonoBehaviour
{
	// Token: 0x06001A81 RID: 6785 RVA: 0x000FC7E2 File Offset: 0x000FA9E2
	private void Start()
	{
		this.selfRectTrans = base.GetComponent<RectTransform>();
		this.begineMove = false;
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x000FC7F8 File Offset: 0x000FA9F8
	private void Update()
	{
		bool flag = this.begineMove;
		if (flag)
		{
			bool flag2 = this.showNextMsgTimer > 0f;
			if (flag2)
			{
				this.showNextMsgTimer -= Time.deltaTime;
			}
			else
			{
				bool flag3 = !this.haveSetNextMsgTimer;
				if (flag3)
				{
					this.haveSetNextMsgTimer = true;
					BroadCastMSGCon.inst.ShowNextMsg();
				}
			}
			bool flag4 = this.timer > 0f;
			if (flag4)
			{
				this.selfRectTrans.localPosition = new Vector3((0f - Time.deltaTime) * 100f + this.selfRectTrans.localPosition.x, 0f, 0f);
				this.timer -= Time.deltaTime;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x000FC8D0 File Offset: 0x000FAAD0
	public void setContentWidth(float w)
	{
		this.begineMove = true;
		this.width = w;
		this.timer = (1280f + this.width + 10f) / 100f;
		this.showNextMsgTimer = (1280f + this.width + 5f) / 100f;
	}

	// Token: 0x04001530 RID: 5424
	private Font font;

	// Token: 0x04001531 RID: 5425
	private CharacterInfo characterInfo;

	// Token: 0x04001532 RID: 5426
	private string text;

	// Token: 0x04001533 RID: 5427
	private float width;

	// Token: 0x04001534 RID: 5428
	private float timer;

	// Token: 0x04001535 RID: 5429
	private RectTransform selfRectTrans;

	// Token: 0x04001536 RID: 5430
	private float showNextMsgTimer = 1f;

	// Token: 0x04001537 RID: 5431
	private bool haveSetNextMsgTimer;

	// Token: 0x04001538 RID: 5432
	private bool begineMove;
}
