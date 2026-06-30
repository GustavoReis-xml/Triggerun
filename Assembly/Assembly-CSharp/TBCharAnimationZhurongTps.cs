using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class TBCharAnimationZhurongTps : TBCharAnimationThirdIK
{
	// Token: 0x06002BFB RID: 11259 RVA: 0x00181909 File Offset: 0x0017FB09
	protected override void Start()
	{
		base.Start();
		this.fireEnergyObj = base.GetComponent<UIGameObjList>().Get_GameObj(0);
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x00181928 File Offset: 0x0017FB28
	protected override void Update()
	{
		bool flag = Time.deltaTime == 0f || this.characterController == null;
		if (!flag)
		{
			base.Update();
			bool flag2 = this.characterController.userControl == null || base.hero == null || base.hero.wc == null;
			if (!flag2)
			{
				bool flag3 = base.hero.wc is CWeaponControlMe;
				if (flag3)
				{
					float num = (float)((CWeaponControlMe)base.hero.wc).bulletNow * 1f / (float)((CWeaponControlMe)base.hero.wc).bulletMax * 1.5f;
					bool flag4 = num == this.lastVal;
					if (flag4)
					{
						return;
					}
					this.fireEnergyObj.transform.DOScaleY(num, Mathf.Abs(num - this.lastVal) * 0.2f);
					this.lastVal = num;
				}
				bool flag5 = base.hero.wc is CWeaponControlIt;
				if (flag5)
				{
					float num2 = (float)((CWeaponControlIt)base.hero.wc).cartridgePublicNow[0] * 1f / (float)((CWeaponControlIt)base.hero.wc).cartridgePublicMax[0] * 1.5f;
					bool flag6 = num2 != this.lastVal;
					if (flag6)
					{
						this.fireEnergyObj.transform.DOScaleY(num2, Mathf.Abs(num2 - this.lastVal) * 0.2f);
						this.lastVal = num2;
					}
				}
			}
		}
	}

	// Token: 0x0400256F RID: 9583
	private GameObject fireEnergyObj;

	// Token: 0x04002570 RID: 9584
	private float lastVal;
}
