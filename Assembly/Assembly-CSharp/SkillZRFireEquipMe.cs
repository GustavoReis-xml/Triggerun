using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class SkillZRFireEquipMe : SkillBaseMe
{
	// Token: 0x0600293A RID: 10554 RVA: 0x0016D170 File Offset: 0x0016B370
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
			CPlaceItem readyItem = base.hero.skillInfo.getReadyItem(base.iSkillIndex);
			bool flag2 = !(readyItem == null);
			if (flag2)
			{
				base.hero.Npc_Handle_Component.Create_Npc(base.hero, readyItem.HitNetId, 1001, readyItem.ThisGameObjTrans.transform.position, readyItem.ThisGameObjTrans.transform.transform.eulerAngles, Vector3.one, this.skillData.id, Vector3.zero, string.Empty);
				base.StartCoroutine(this.WaitEquipReady(base.heroSkillData.m_nSKLongTime));
			}
		}
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x0016D25F File Offset: 0x0016B45F
	private IEnumerator WaitEquipReady(int nsec)
	{
		yield return new WaitForSeconds(0.5f);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x0016D278 File Offset: 0x0016B478
	public override bool OneSkillCanDo()
	{
		base.hero.wc.UpdateFireTime(0f);
		bool flag = base.hero.playerCommon.m_nJiangzhi == 1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = base.hero.playerCommon.m_nDongjie == 1;
			result = (!flag2 && base.OneSkillCanDo());
		}
		return result;
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x0016D2DE File Offset: 0x0016B4DE
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.hero.SetAnimator("SkillShift", 0, false, 2);
		this.Send_Messages(1);
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x0016D30C File Offset: 0x0016B50C
	public override void readyItemSwitch(bool switchOpen, bool isDoSkill = false)
	{
		base.readyItemSwitch(switchOpen, false);
		bool flag = !switchOpen;
		if (flag)
		{
			this.OneSkillFinish();
		}
		base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
		base.hero.wc.UpdateFireTime(0.2f);
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x0016D35C File Offset: 0x0016B55C
	private void Send_Messages(int _State)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.hero.getNetid());
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(_State);
		base.hero.SendSkillParms(arrayList);
	}
}
