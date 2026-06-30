using System;

// Token: 0x0200017D RID: 381
public class HeroFaShiIt : HeroBaseIt
{
	// Token: 0x06001191 RID: 4497 RVA: 0x000A46A4 File Offset: 0x000A28A4
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		CScreenTarget.InitScreenTarget(base.hero, "TargetCenterOne");
		base.hero.screentarget.targetAngle = 0.99f;
		base.hero.screentarget.targetDis = 40f;
	}
}
