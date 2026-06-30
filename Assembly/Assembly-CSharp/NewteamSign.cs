using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025E RID: 606
public class NewteamSign : MonoBehaviour
{
	// Token: 0x06001B9B RID: 7067 RVA: 0x0010533E File Offset: 0x0010353E
	public void init(NetLiving _gameplayer)
	{
		this.gameplayer = _gameplayer;
		this.imageFill = base.transform.FindChild("fill").GetComponent<Image>();
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x00105364 File Offset: 0x00103564
	private void Update()
	{
		bool flag = this.imageFill == null;
		if (!flag)
		{
			bool flag2 = this.gameplayer == null || this.gameplayer.UserControl == null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.DeleteSignOne(this.gameplayer.getNetid());
			}
			else
			{
				bool isDead = this.gameplayer.isDead;
				if (isDead)
				{
					DateTime dPlayerDeathDate = this.gameplayer.playerCommon.m_dPlayerDeathDate;
					DateTime d = dPlayerDeathDate.AddSeconds((double)this.gameplayer.playerCommon.m_nDeathTimeLong);
					DateTime now = Fun.Now;
					float num = (float)(d - now).TotalSeconds;
					bool flag3 = num < 0f;
					if (flag3)
					{
						num = 0f;
					}
					this.imageFill.fillAmount = num / (float)this.gameplayer.playerCommon.m_nDeathTimeLong * 1f;
				}
				else
				{
					gameuiCon.inst.mainMsgUI.DeleteSignOne(this.gameplayer.getNetid());
				}
			}
		}
	}

	// Token: 0x040018FD RID: 6397
	private NetLiving gameplayer;

	// Token: 0x040018FE RID: 6398
	private Image imageFill;
}
