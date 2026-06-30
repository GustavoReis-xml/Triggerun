using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class fCreateScene : MonoBehaviour
{
	// Token: 0x06000AC6 RID: 2758 RVA: 0x00062D0C File Offset: 0x00060F0C
	private void Start()
	{
		this.gameobj = GameObject.Find("_fGameManager");
		bool flag = this.gameobj;
		if (flag)
		{
			this.gameobj.SendMessage("OnSceneCreate", this.scenename);
		}
		fGameManager.allBaozhaFather = new GameObject("_EffectsParent");
		fGameManager.allBulletFather = new GameObject("_BulletsParent");
		fGameManager.allSoundFather = new GameObject("_SoundsParent");
		fGameManager.allPaintFather = new GameObject("_PaintsParent");
		fGameManager.allPaintFather.AddComponent<CPaintManager>();
		CEffectPools.Instance.InitPools();
		CSoundManager.Instance.checkSceneSound(base.gameObject);
		GameRoot.inst.SetJiaoSeLightObj(this.m_pJiaoSe_Light);
		Collider[] componentsInChildren = base.GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			bool flag2 = componentsInChildren[i].enabled && !componentsInChildren[i].isTrigger && componentsInChildren[i].gameObject.activeSelf && !componentsInChildren[i].transform.parent.tag.Contains("Door");
			if (flag2)
			{
				bool flag3 = componentsInChildren[i].gameObject.layer == 0;
				if (flag3)
				{
					this.allBuliderCollider.Add(componentsInChildren[i]);
				}
				bool flag4 = componentsInChildren[i].gameObject.layer == 1;
				if (flag4)
				{
					this.allNullWallCollider.Add(componentsInChildren[i]);
				}
			}
		}
		Light[] componentsInChildren2 = base.GetComponentsInChildren<Light>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			bool flag5 = !componentsInChildren2[j].name.Contains("(forever)");
			if (flag5)
			{
				componentsInChildren2[j].cullingMask = 32768;
			}
		}
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00062ED4 File Offset: 0x000610D4
	private void Update()
	{
		bool flag = this.haveFucked;
		if (flag)
		{
			bool flag2 = this.layerTimeout <= Time.time;
			if (flag2)
			{
				this.layerTimeout = Time.time + 60f;
				bool flag3 = fGameManager.Heroplayer != null;
				if (flag3)
				{
					FunManager.Instance.Send(fGameManager.Heroplayer, "157", "地图碰撞层异常:" + fGameManager.GetInstance().mloadName + "," + this.layername);
					fGameManager.GetInstance().OnLostTip(Language.GetLang(1501, new object[0]));
				}
			}
		}
		else
		{
			bool flag4 = this.noD < this.allBuliderCollider.Count;
			if (flag4)
			{
				bool flag5 = this.allBuliderCollider[this.noD] == null;
				if (flag5)
				{
					this.haveFucked = true;
					this.layername = string.Format("name:{0},destroy", "null");
					return;
				}
				bool flag6 = !this.allBuliderCollider[this.noD].enabled && this.allBuliderCollider[this.noD].gameObject.layer != 20 && this.allBuliderCollider[this.noD].gameObject.layer != 21;
				if (flag6)
				{
					this.haveFucked = true;
					this.layername = "name:" + this.allBuliderCollider[this.noD].gameObject.name + ",enabled";
					return;
				}
				bool flag7 = this.allBuliderCollider[this.noD].gameObject.layer != 0 && this.allBuliderCollider[this.noD].gameObject.layer != 20 && this.allBuliderCollider[this.noD].gameObject.layer != 21;
				if (flag7)
				{
					this.haveFucked = true;
					this.layername = "name:" + this.allBuliderCollider[this.noD].gameObject.name + ",layer";
					return;
				}
				bool flag8 = !this.allBuliderCollider[this.noD].gameObject.activeSelf && this.allBuliderCollider[this.noD].gameObject.layer != 20 && this.allBuliderCollider[this.noD].gameObject.layer != 21;
				if (flag8)
				{
					this.haveFucked = true;
					this.layername = "name:" + this.allBuliderCollider[this.noD].gameObject.name + ",active";
					return;
				}
				bool isTrigger = this.allBuliderCollider[this.noD].isTrigger;
				if (isTrigger)
				{
					this.haveFucked = true;
					this.layername = "name:" + this.allBuliderCollider[this.noD].gameObject.name + ",isTrigger";
					return;
				}
				this.noD++;
				this.noD = ((this.noD < this.allBuliderCollider.Count) ? this.noD : 0);
			}
			bool flag9 = this.noT < this.allNullWallCollider.Count;
			if (flag9)
			{
				bool flag10 = this.allNullWallCollider[this.noT] == null;
				if (flag10)
				{
					this.haveFucked = true;
					this.layername = string.Format("name:{0},destroy", "null");
				}
				else
				{
					bool flag11 = !this.allNullWallCollider[this.noT].enabled && this.allNullWallCollider[this.noT].gameObject.layer != 20 && this.allNullWallCollider[this.noT].gameObject.layer != 21;
					if (flag11)
					{
						this.haveFucked = true;
						this.layername = "name:" + this.allNullWallCollider[this.noT].gameObject.name + ",enabled";
					}
					else
					{
						bool flag12 = this.allNullWallCollider[this.noT].gameObject.layer != 1;
						if (flag12)
						{
							this.haveFucked = true;
							this.layername = "name:" + this.allNullWallCollider[this.noT].gameObject.name + ",layer";
						}
						else
						{
							bool flag13 = !this.allNullWallCollider[this.noT].gameObject.activeSelf;
							if (flag13)
							{
								this.haveFucked = true;
								this.layername = "name:" + this.allNullWallCollider[this.noT].gameObject.name + ",active";
							}
							else
							{
								bool isTrigger2 = this.allNullWallCollider[this.noT].isTrigger;
								if (isTrigger2)
								{
									this.haveFucked = true;
									this.layername = "name:" + this.allNullWallCollider[this.noT].gameObject.name + ",isTrigger";
								}
								else
								{
									this.noT++;
									this.noT = ((this.noT < this.allNullWallCollider.Count) ? this.noT : 0);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040008FD RID: 2301
	private GameObject gameobj;

	// Token: 0x040008FE RID: 2302
	public string scenename = string.Empty;

	// Token: 0x040008FF RID: 2303
	private GameObject playerModel;

	// Token: 0x04000900 RID: 2304
	public GameObject m_pJiaoSe_Light;

	// Token: 0x04000901 RID: 2305
	private List<Collider> allBuliderCollider = new List<Collider>();

	// Token: 0x04000902 RID: 2306
	private List<Collider> allNullWallCollider = new List<Collider>();

	// Token: 0x04000903 RID: 2307
	private int noD;

	// Token: 0x04000904 RID: 2308
	private int noT;

	// Token: 0x04000905 RID: 2309
	private bool haveFucked;

	// Token: 0x04000906 RID: 2310
	private string layername = string.Empty;

	// Token: 0x04000907 RID: 2311
	private float layerTimeout;
}
