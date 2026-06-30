using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class SkillGhostMe : SkillBaseMe
{
	// Token: 0x060026DC RID: 9948 RVA: 0x00160DD0 File Offset: 0x0015EFD0
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		Collider[] componentsInChildren = base.hero.userModelTps.GetComponentsInChildren<Collider>();
		((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
		base.hero.wInput.notInput = true;
		base.hero.tChar.m_MoveByAnimator = false;
		base.hero.playerCommon.m_fMoveSpeedAnimation = 4f;
		base.hero.ChangeSpeed();
		this.colChange = new ArrayList();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
			this.colChange.Add(componentsInChildren[i]);
		}
		base.StartCoroutine(this.endSkill());
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x00160E97 File Offset: 0x0015F097
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.tChar.m_MoveByAnimator = true;
		base.hero.wInput.notInput = false;
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x00160EC4 File Offset: 0x0015F0C4
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
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x04002250 RID: 8784
	private ArrayList colChange;
}
