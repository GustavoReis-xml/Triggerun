using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class HeroShawangIt : HeroBaseIt
{
	// Token: 0x0600141D RID: 5149 RVA: 0x000BE2C9 File Offset: 0x000BC4C9
	public override void GetHeroNpc(object obj)
	{
		base.GetHeroNpc(obj);
		((GamePlayer)base.hero).playerDazhao.superNpc = (Npc_Entity)obj;
		this.dazhaoNpc = (Npc_Entity)obj;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000BE2FC File Offset: 0x000BC4FC
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		Transform transform = Fun.Get_Child_Transform(base.gameObject, "chongci");
		bool flag = transform != null && transform.gameObject != null;
		if (flag)
		{
			transform.gameObject.name = transform.gameObject.name + "_IgnoreDamge";
		}
		else
		{
			Debug.LogWarning("HeroShawangIt: 'chongci' child transform not found on " + base.gameObject.name);
		}
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x000BE380 File Offset: 0x000BC580
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = base.hero.skillEffectDict.ContainsKey(1102);
		if (flag)
		{
			((SkillSandstormDashIt)base.hero.skillEffectDict[1102]).OneSkillFinish();
		}
	}

	// Token: 0x04000F83 RID: 3971
	public Npc_Entity dazhaoNpc;
}
