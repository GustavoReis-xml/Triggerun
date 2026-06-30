using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class SkillGhostIt : SkillBaseIt
{
	// Token: 0x060026D9 RID: 9945 RVA: 0x00160D28 File Offset: 0x0015EF28
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		Collider[] componentsInChildren = base.gameplayer.userModelTps.GetComponentsInChildren<Collider>();
		this.colChange = new ArrayList();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
			this.colChange.Add(componentsInChildren[i]);
		}
		base.gameplayer.tChar.m_MoveByAnimator = false;
		base.gameplayer.tChar.moveByControlIt = true;
		base.StartCoroutine(this.endSkill());
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x00160DB8 File Offset: 0x0015EFB8
	private IEnumerator endSkill()
	{
		yield return new WaitForSeconds(3f);
		int num;
		for (int i = 0; i < this.colChange.Count; i = num + 1)
		{
			Collider col = (Collider)this.colChange[i];
			bool flag = !(col == null);
			if (flag)
			{
				col.enabled = true;
			}
			col = null;
			num = i;
		}
		base.gameplayer.tChar.m_MoveByAnimator = true;
		base.gameplayer.tChar.moveByControlIt = false;
		yield break;
	}

	// Token: 0x0400224F RID: 8783
	private ArrayList colChange;
}
