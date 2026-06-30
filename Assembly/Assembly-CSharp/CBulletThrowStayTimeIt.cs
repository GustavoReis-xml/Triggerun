using System;

// Token: 0x02000069 RID: 105
public class CBulletThrowStayTimeIt : CBulletIt
{
	// Token: 0x06000373 RID: 883 RVA: 0x0001A6E4 File Offset: 0x000188E4
	private void FixedUpdate()
	{
		bool flag = !this.renderDisable && this.objectRender != null && CFilmManager.Instance.playing && base.muzzle != null && base.muzzle.launcher != null && base.muzzle.launcher.NetObject != null && !base.muzzle.launcher.NetObject.isCopy;
		if (flag)
		{
			bool flag2 = base.transform.name.Contains("_em_");
			if (flag2)
			{
				base.transform.name = base.transform.name.Replace("_em_", string.Empty);
			}
			bool flag3 = base.transform.name.Contains("_fr_");
			if (flag3)
			{
				base.transform.name = base.transform.name.Replace("_fr_", string.Empty);
			}
			this.renderDisable = true;
			this.objectRender.SetEnemyFriendRenderParticle(false, true);
		}
		this.checkDelayBoom();
	}

	// Token: 0x040002CC RID: 716
	private bool renderDisable;
}
