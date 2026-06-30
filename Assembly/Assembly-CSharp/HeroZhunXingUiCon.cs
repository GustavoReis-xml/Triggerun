using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class HeroZhunXingUiCon : Window
{
	// Token: 0x06001559 RID: 5465 RVA: 0x000C89F4 File Offset: 0x000C6BF4
	protected override void Awake()
	{
		base.Awake();
		this.tclone = base.transform.FindChild("clone");
		this.tclone.gameObject.SetActive(false);
		this.oShow = base.transform.transform.Find("show").gameObject;
		this.tothershow = base.transform.FindChild("show/other");
		this.tcommonshow = base.transform.FindChild("show/common");
		this.tleftbottomshow = base.transform.parent.transform.FindChild("newleftbottom/lshowother");
		this.tleftbottomshow.gameObject.SetActive(false);
		this.oShow.SetActive(false);
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000C8ABC File Offset: 0x000C6CBC
	public void Refresh(NetLiving _net)
	{
		Transform transform = null;
		bool flag = _net == null;
		if (!flag)
		{
			this._NetLiving = _net;
			int nModelIndex = this._NetLiving.playerCommon.m_nModelIndex;
			HeroData heroData = roleuiManager.getHeroData(nModelIndex);
			bool flag2 = heroData == null;
			if (flag2)
			{
				Debug.LogError("还没有选择英雄就进入游戏或选择的英雄配表中没有" + nModelIndex.ToString());
			}
			else
			{
				bool flag3 = this.oShow == null;
				if (!flag3)
				{
					bool flag4 = !this.oShow.activeSelf;
					if (flag4)
					{
						this.oShow.SetActive(true);
					}
					Fun.ClearChildObejct(this.tothershow.gameObject);
					Transform transform2 = this.tclone.FindChild(heroData.hzhunxingClass);
					bool flag5 = transform2 != null;
					if (flag5)
					{
						transform = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject).transform;
						transform.SetParent(this.tothershow);
						Fun.Reset(transform.gameObject);
					}
					else
					{
						Debug.LogError("没有找到准星类:" + heroData.hzhunxingClass);
					}
					bool flag6 = _net.playerZhunxingMe != null;
					if (flag6)
					{
						bool flag7 = _net.playerZhunxingMe.GetType().ToString() != heroData.hzhunxingClass;
						if (flag7)
						{
							Debug.LogError("准星类不匹配:" + _net.playerZhunxingMe.GetType().ToString() + "," + heroData.hzhunxingClass);
						}
						_net.playerZhunxingMe.setUi(this.tcommonshow, transform);
					}
				}
			}
		}
	}

	// Token: 0x04001068 RID: 4200
	private GameObject oShow;

	// Token: 0x04001069 RID: 4201
	private Transform tclone;

	// Token: 0x0400106A RID: 4202
	private Transform tothershow;

	// Token: 0x0400106B RID: 4203
	public Transform tcommonshow;

	// Token: 0x0400106C RID: 4204
	private Transform tleftbottomshow;

	// Token: 0x0400106D RID: 4205
	private NetLiving _NetLiving;
}
