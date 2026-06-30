using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class TBCharAnimationZhurongFps : TBCharAnimationPlay
{
	// Token: 0x06002BF8 RID: 11256 RVA: 0x001817E6 File Offset: 0x0017F9E6
	protected override void Start()
	{
		base.Start();
		this.fireEnergyObj = base.GetComponent<UIGameObjList>().Get_GameObj(0);
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x00181804 File Offset: 0x0017FA04
	protected override void Update()
	{
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			base.Update();
			bool flag2 = !(this.characterController.userControl == null) && base.hero != null && !(base.hero.wc == null) && base.hero.wc is CWeaponControlMe;
			if (flag2)
			{
				float num = (float)((CWeaponControlMe)base.hero.wc).bulletNow * 1f / (float)((CWeaponControlMe)base.hero.wc).bulletMax * 1.8f;
				bool flag3 = num != this.lastVal;
				if (flag3)
				{
					this.fireEnergyObj.transform.DOScaleY(num, Mathf.Abs(num - this.lastVal) * 0.2f);
					this.lastVal = num;
				}
			}
		}
	}

	// Token: 0x0400256D RID: 9581
	private GameObject fireEnergyObj;

	// Token: 0x0400256E RID: 9582
	private float lastVal;
}
