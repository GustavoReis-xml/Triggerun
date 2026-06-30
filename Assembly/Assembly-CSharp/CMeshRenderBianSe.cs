using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class CMeshRenderBianSe : MonoBehaviour
{
	// Token: 0x0600059B RID: 1435 RVA: 0x0002C35C File Offset: 0x0002A55C
	public void InitBianSe(NetLiving gameplayer)
	{
		this.trans_Layer = LayerMask.NameToLayer("TransparentFX");
		this.default_Layer = LayerMask.NameToLayer("Default");
		this.frontCam_Layer = LayerMask.NameToLayer("FrontCamera");
		this.UI_Layer = LayerMask.NameToLayer("UIModel");
		this.renders = base.GetComponentsInChildren<Renderer>(true);
		this.allRenders = base.GetComponentsInChildren<Renderer>(true);
		this.gp = gameplayer;
		bool bePlayer = this.gp.bePlayer;
		if (bePlayer)
		{
			this.layerName = "force" + this.gp.playerCommon.m_nShili.ToString() + "_player";
		}
		else
		{
			this.layerName = "force" + this.gp.playerCommon.m_nShili.ToString() + "_npc";
		}
		this.BackNormalLayer = LayerMask.NameToLayer(this.layerName);
		this.szHongWaiList = this.GetRenderer("hongwai_fri");
		this.wudiList = this.GetRenderer("fuhuo_wudi");
		this.toushiList = this.GetRenderer("_silhouetted");
		this.enemyEffRender = this.GetRenderer("ptc_df");
		this.enemyEff = base.GetComponentsInChildren<ParticleSystem>(true);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0002C498 File Offset: 0x0002A698
	private void Update()
	{
		bool flag = Time.time >= this.updateRate;
		if (flag)
		{
			this.updateRate = Time.time + 0.1f;
			bool flag2 = !this.bJianYinStat;
			if (flag2)
			{
				this.CheckJianYingColor();
			}
			bool flag3 = !this.changeFileMaterial && this.needChangeYinShenMat;
			if (flag3)
			{
				this.needChangeYinShenMat = false;
				this.ChangeYinShenMat(this.YinShenType);
			}
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0002C510 File Offset: 0x0002A710
	public void makeHongXianDisable()
	{
		for (int i = 0; i < this.allRenders.Length; i++)
		{
			bool flag = this.allRenders[i] != null && this.allRenders[i].name == "hongxian";
			if (flag)
			{
				this.allRenders[i].enabled = false;
				this.allRenders[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0002C58C File Offset: 0x0002A78C
	public void ChangeMateial(bool normalShow)
	{
		for (int i = 0; i < this.toushiList.Count; i++)
		{
			bool flag = this.toushiList[i] == null;
			if (!flag)
			{
				if (normalShow)
				{
					bool flag2 = this.toushiList[i].name.Contains("_silhouetted");
					if (flag2)
					{
						this.toushiList[i].enabled = false;
					}
				}
				else
				{
					bool flag3 = this.toushiList[i].name.Contains("_silhouetted");
					if (flag3)
					{
						this.toushiList[i].material.SetColor("_OutlineColor", Color.red);
						this.toushiList[i].enabled = true;
					}
				}
			}
		}
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0002C670 File Offset: 0x0002A870
	public void ChangeYinShenMat(int type)
	{
		this.YinShenType = type;
		bool flag = this.changeFileMaterial;
		if (flag)
		{
			this.needChangeYinShenMat = true;
		}
		else
		{
			for (int i = 0; i < this.renders.Length; i++)
			{
				bool flag2 = this.renders[i] == null;
				if (!flag2)
				{
					switch (type)
					{
					case 0:
					{
						bool flag3 = this.renders[i].name.Contains("zhengchang");
						if (flag3)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag4 = this.renders[i].name.Contains("yinsheng");
						if (flag4)
						{
							this.renders[i].gameObject.layer = this.default_Layer;
						}
						bool flag5 = this.renders[i].name.Contains("jjs_yinsheng2");
						if (flag5)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag6 = this.renders[i].name.Contains("hongxian");
						if (flag6)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						break;
					}
					case 1:
					{
						bool flag7 = this.renders[i].name.Contains("zhengchang");
						if (flag7)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag8 = this.renders[i].name.Contains("yinsheng");
						if (flag8)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag9 = this.renders[i].name.Contains("jjs_yinsheng2");
						if (flag9)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag10 = this.renders[i].name.Contains("hongxian");
						if (flag10)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						break;
					}
					case 2:
					{
						bool flag11 = this.renders[i].name.Contains("zhengchang");
						if (flag11)
						{
							this.renders[i].gameObject.layer = this.UI_Layer;
						}
						bool flag12 = this.renders[i].name.Contains("yinsheng");
						if (flag12)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag13 = this.renders[i].name.Contains("jjs_yinsheng2");
						if (flag13)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag14 = this.renders[i].name.Contains("hongxian");
						if (flag14)
						{
							this.renders[i].gameObject.layer = this.UI_Layer;
						}
						break;
					}
					case 3:
					{
						bool flag15 = this.renders[i].name.Contains("zhengchang");
						if (flag15)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag16 = this.renders[i].name.Contains("yinsheng");
						if (flag16)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						bool flag17 = this.renders[i].name.Contains("jjs_yinsheng2");
						if (flag17)
						{
							this.renders[i].gameObject.layer = this.default_Layer;
						}
						bool flag18 = this.renders[i].name.Contains("hongxian");
						if (flag18)
						{
							this.renders[i].gameObject.layer = this.trans_Layer;
						}
						break;
					}
					}
				}
			}
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0002CA88 File Offset: 0x0002AC88
	public void ChangeYinShenFPSMat(int type)
	{
		for (int i = 0; i < this.renders.Length; i++)
		{
			bool flag = this.renders[i] == null;
			if (!flag)
			{
				switch (type)
				{
				case 0:
				{
					bool flag2 = this.renders[i].name.Contains("zhengchang");
					if (flag2)
					{
						this.renders[i].gameObject.layer = this.trans_Layer;
					}
					bool flag3 = this.renders[i].name.Contains("yinsheng");
					if (flag3)
					{
						this.renders[i].gameObject.layer = this.frontCam_Layer;
					}
					break;
				}
				case 1:
				{
					bool flag4 = this.renders[i].name.Contains("zhengchang");
					if (flag4)
					{
						this.renders[i].gameObject.layer = this.trans_Layer;
					}
					bool flag5 = this.renders[i].name.Contains("yinsheng");
					if (flag5)
					{
						this.renders[i].gameObject.layer = this.trans_Layer;
					}
					break;
				}
				case 2:
				{
					bool flag6 = this.renders[i].name.Contains("zhengchang");
					if (flag6)
					{
						this.renders[i].gameObject.layer = this.frontCam_Layer;
					}
					bool flag7 = this.renders[i].name.Contains("yinsheng");
					if (flag7)
					{
						this.renders[i].gameObject.layer = this.trans_Layer;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0002CC44 File Offset: 0x0002AE44
	private List<Renderer> GetRenderer(string sName)
	{
		int num = this.allRenders.Length;
		List<Renderer> list = new List<Renderer>();
		for (int i = 0; i < num; i++)
		{
			bool flag = !(this.allRenders[i] == null) && this.allRenders[i].name.Contains(sName);
			if (flag)
			{
				list.Add(this.allRenders[i]);
			}
		}
		return list;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
	public void ChangeHealerSeeThroughMat()
	{
		bool flag = this.szHongWaiList.Count >= 1;
		if (flag)
		{
			this.bJianYinStat = false;
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0002CCE4 File Offset: 0x0002AEE4
	private void addlog()
	{
		int num = this.allRenders.Length;
		for (int i = 0; i < num; i++)
		{
			bool flag = this.allRenders[i] == null;
			if (!flag)
			{
				bool flag2 = this.allRenders[i].name == "hongwai_fri";
				if (flag2)
				{
					bool flag3 = this.allRenders[i].GetType() == typeof(SkinnedMeshRenderer);
					if (flag3)
					{
						bool flag4 = ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.x < 100f || ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.y < 100f || ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.z < 100f;
						if (flag4)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"模型 ",
								this.gp.heroBase.heroClassName,
								" renders name ",
								this.allRenders[i].name,
								" 有问题bounds size < 100 应该改为999 找老朱解决"
							}));
						}
						bool updateWhenOffscreen = ((SkinnedMeshRenderer)this.allRenders[i]).updateWhenOffscreen;
						if (updateWhenOffscreen)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"模型 ",
								this.gp.heroBase.heroClassName,
								" renders name ",
								this.allRenders[i].name,
								" updateWhenOffscreen == true 有问题 应该勾掉选项， 找老朱修改"
							}));
						}
					}
				}
				else
				{
					bool flag5 = !this.allRenders[i].name.Contains("_silhouetted") && !this.allRenders[i].name.Contains("hongwai_fri") && this.allRenders[i].GetType() == typeof(SkinnedMeshRenderer);
					if (flag5)
					{
						bool flag6 = ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.x > 100f || ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.y > 100f || ((SkinnedMeshRenderer)this.allRenders[i]).localBounds.extents.z > 100f;
						if (flag6)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"模型 ",
								this.gp.heroBase.heroClassName,
								" renders name ",
								this.allRenders[i].name,
								" 有问题bounds size 》 100 应该改小 找老朱解决"
							}));
						}
						bool updateWhenOffscreen2 = ((SkinnedMeshRenderer)this.allRenders[i]).updateWhenOffscreen;
						if (updateWhenOffscreen2)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"模型 ",
								this.gp.heroBase.heroClassName,
								" renders name ",
								this.allRenders[i].name,
								" updateWhenOffscreen == true 有问题 应该勾掉选项， 找老朱修改"
							}));
						}
					}
				}
			}
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0002D034 File Offset: 0x0002B234
	private void CheckJianYingColor()
	{
		Color hpColor = this.GetHpColor();
		bool flag = this.cOldColor == hpColor;
		if (!flag)
		{
			this.cOldColor = hpColor;
			int count = this.szHongWaiList.Count;
			for (int i = 0; i < count; i++)
			{
				Renderer renderer = this.szHongWaiList[i];
				bool flag2 = renderer == null;
				if (!flag2)
				{
					bool flag3 = !renderer.enabled;
					if (flag3)
					{
						renderer.enabled = true;
					}
					bool flag4 = !renderer.gameObject.activeSelf;
					if (flag4)
					{
						renderer.gameObject.SetActive(true);
					}
					Material[] materials = renderer.materials;
					int num = materials.Length;
					for (int j = 0; j < num; j++)
					{
						Material material = materials[j];
						bool flag5 = !(material == null);
						if (flag5)
						{
							material.SetColor("_Color", hpColor);
						}
					}
				}
			}
		}
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0002D140 File Offset: 0x0002B340
	private Color GetHpColor()
	{
		Color result = this.col;
		float hpBiLi = this.gp.playerCommon.GetHpBiLi();
		bool flag = hpBiLi >= 0.9f;
		if (flag)
		{
			result = this.col1;
		}
		else
		{
			bool flag2 = hpBiLi >= 0.4f && hpBiLi < 0.9f;
			if (flag2)
			{
				result = this.col2;
			}
			else
			{
				bool flag3 = hpBiLi > 0f && hpBiLi < 0.4f;
				if (flag3)
				{
					result = this.col3;
				}
			}
		}
		return result;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0002D1CC File Offset: 0x0002B3CC
	public void InvincibleMat(bool show)
	{
		for (int i = 0; i < this.wudiList.Count; i++)
		{
			bool flag = this.wudiList[i] == null;
			if (!flag)
			{
				if (show)
				{
					bool flag2 = this.wudiList[i].name.Contains("wudi");
					if (flag2)
					{
						bool flag3 = !this.wudiList[i].gameObject.activeSelf;
						if (flag3)
						{
							this.wudiList[i].gameObject.SetActive(true);
						}
						this.wudiList[i].enabled = true;
					}
				}
				else
				{
					bool flag4 = this.wudiList[i].name.Contains("wudi");
					if (flag4)
					{
						bool activeSelf = this.wudiList[i].gameObject.activeSelf;
						if (activeSelf)
						{
							this.wudiList[i].gameObject.SetActive(false);
						}
						this.wudiList[i].enabled = false;
					}
				}
			}
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0002D2FC File Offset: 0x0002B4FC
	public void SetEnemyEff(bool active)
	{
		for (int i = 0; i < this.enemyEff.Length; i++)
		{
			bool flag = this.enemyEff[i].name.Contains("ptc_df");
			if (flag)
			{
				this.enemyEff[i].gameObject.SetActive(active);
			}
		}
		for (int j = 0; j < this.enemyEffRender.Count; j++)
		{
			bool flag2 = this.enemyEffRender[j].name.Contains("ptc_df");
			if (flag2)
			{
				this.enemyEffRender[j].gameObject.SetActive(active);
			}
		}
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0002D3B0 File Offset: 0x0002B5B0
	public void SetLuoLiParticle(bool active)
	{
		for (int i = 0; i < this.enemyEff.Length; i++)
		{
			bool flag = this.enemyEff[i].name.Contains("toushe");
			if (flag)
			{
				this.enemyEff[i].gameObject.SetActive(active);
			}
		}
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0002D408 File Offset: 0x0002B608
	public void SetFilmMaterial(bool active)
	{
		if (active)
		{
			for (int i = 0; i < this.allRenders.Length; i++)
			{
				bool flag = !(this.allRenders[i] == null);
				if (flag)
				{
					this.allRenders[i].gameObject.layer = this.trans_Layer;
				}
			}
			this.changeFileMaterial = true;
		}
		else
		{
			for (int j = 0; j < this.allRenders.Length; j++)
			{
				bool flag2 = !(this.allRenders[j] == null);
				if (flag2)
				{
					this.allRenders[j].gameObject.layer = this.BackNormalLayer;
				}
			}
			this.ChangeYinShenMat(2);
			this.changeFileMaterial = false;
		}
	}

	// Token: 0x04000433 RID: 1075
	private int trans_Layer;

	// Token: 0x04000434 RID: 1076
	private int default_Layer;

	// Token: 0x04000435 RID: 1077
	private int frontCam_Layer;

	// Token: 0x04000436 RID: 1078
	private int UI_Layer;

	// Token: 0x04000437 RID: 1079
	private Renderer[] renders;

	// Token: 0x04000438 RID: 1080
	private Renderer[] allRenders;

	// Token: 0x04000439 RID: 1081
	private NetLiving gp;

	// Token: 0x0400043A RID: 1082
	private List<Renderer> szHongWaiList = new List<Renderer>();

	// Token: 0x0400043B RID: 1083
	private bool bJianYinStat = true;

	// Token: 0x0400043C RID: 1084
	private List<Renderer> wudiList = new List<Renderer>();

	// Token: 0x0400043D RID: 1085
	private List<Renderer> toushiList = new List<Renderer>();

	// Token: 0x0400043E RID: 1086
	private List<Renderer> enemyEffRender = new List<Renderer>();

	// Token: 0x0400043F RID: 1087
	private ParticleSystem[] enemyEff;

	// Token: 0x04000440 RID: 1088
	private bool changeFileMaterial;

	// Token: 0x04000441 RID: 1089
	private bool needChangeYinShenMat;

	// Token: 0x04000442 RID: 1090
	private int YinShenType;

	// Token: 0x04000443 RID: 1091
	private int BackNormalLayer;

	// Token: 0x04000444 RID: 1092
	private string layerName;

	// Token: 0x04000445 RID: 1093
	private float updateRate;

	// Token: 0x04000446 RID: 1094
	private Color cOldColor = Color.white;

	// Token: 0x04000447 RID: 1095
	private Color col = Fun.StrToColor("ffffff", 0.11764706f);

	// Token: 0x04000448 RID: 1096
	private Color col1 = Fun.StrToColor("00fffc", 0.11764706f);

	// Token: 0x04000449 RID: 1097
	private Color col2 = Fun.StrToColor("b59f23", 0.11764706f);

	// Token: 0x0400044A RID: 1098
	private Color col3 = Fun.StrToColor("d79137", 0.11764706f);
}
