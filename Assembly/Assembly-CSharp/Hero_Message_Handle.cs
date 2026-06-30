using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class Hero_Message_Handle
{
	// Token: 0x0600157A RID: 5498 RVA: 0x000C93E4 File Offset: 0x000C75E4
	public void Handle(HeroPlayer _HeroPlayer, ArrayList _Parameters)
	{
		int num = (int)_Parameters[0];
		int num2 = num;
		if (num2 != 1010100)
		{
			if (num2 != 2010002)
			{
				if (num2 == 2010105)
				{
					((TBCharacterThirdMe)_HeroPlayer.tChar).SpeedPowerForce((-Camera.main.transform.forward + Camera.main.transform.up) * 10f, 2.5f);
				}
			}
			else
			{
				bool flag = _HeroPlayer.skillEffectDict.ContainsKey(1021);
				if (flag)
				{
					((SkillShieldMe)_HeroPlayer.skillEffectDict[1021]).Update_Shield_Data((ArrayList)_Parameters[1]);
				}
			}
		}
		else
		{
			_HeroPlayer.setPlayerNewPos(this.Split_Position((ArrayList)_Parameters[1]));
		}
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000C94CC File Offset: 0x000C76CC
	private Vector3 Split_Position(ArrayList _Value)
	{
		return new Vector3((float)int.Parse(_Value[0].ToString()) / 100f, (float)int.Parse(_Value[1].ToString()) / 100f, (float)int.Parse(_Value[2].ToString()) / 100f);
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000C952C File Offset: 0x000C772C
	public void Reinhardt_Shield_Break(NetLiving _NetLiving)
	{
		bool flag = _NetLiving != null;
		if (flag)
		{
			HeroXingTianMe component = _NetLiving.thisModel.GetComponent<HeroXingTianMe>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.StartCoroutine(component.Shield_Break_Effect());
			}
			HeroXingTianIt component2 = _NetLiving.thisModel.GetComponent<HeroXingTianIt>();
			bool flag3 = component2 != null;
			if (flag3)
			{
				component2.StartCoroutine(component2.Shield_Break_Effect());
			}
		}
	}
}
