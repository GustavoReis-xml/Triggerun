using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class HeroShawangMe : HeroBaseMe
{
	// Token: 0x06001421 RID: 5153 RVA: 0x000BE3DC File Offset: 0x000BC5DC
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		bool flag = base.hero.playerCommon.m_nShili == 1;
		if (flag)
		{
			this.layer = (1 << LayerMask.NameToLayer("force2_player") | 1 << LayerMask.NameToLayer("force2_npc"));
		}
		else
		{
			this.layer = (1 << LayerMask.NameToLayer("force1_player") | 1 << LayerMask.NameToLayer("force1_npc"));
		}
		this.pullLayer = (1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2"));
		this.targetEnemyIDs = new ArrayList();
		this.newAddedEnemeyIDs = new ArrayList();
		this.SuperDoing = false;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		bool flag = !this.releasedSuper;
		if (flag)
		{
			base.hero.SetAnimator("SkillSuper", 0, true, 1);
			this.InteruptDash = true;
			((CWeaponControlMe)base.hero.wc).cancelFillCartridge();
			this.SuperDoing = true;
			this.releasedSuper = true;
			this.targetEnemyIDs.Clear();
			this.newAddedEnemeyIDs.Clear();
			this.goRoot = new List<GameObject>();
			this.targetId = 0;
			this.lasttargetId = 0;
			this.detectTimer = 0f;
			base.StartCoroutine(this.setDoSuper());
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000BE57D File Offset: 0x000BC77D
	private IEnumerator setDoSuper()
	{
		yield return new WaitForSeconds(1f);
		this.InteruptDash = false;
		yield break;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000BE58C File Offset: 0x000BC78C
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		Transform transform = Fun.Get_Child_Transform(base.gameObject, "chongci");
		bool flag = transform != null && transform.gameObject != null;
		if (flag)
		{
			string layerName = "force" + base.hero.playerCommon.m_nShili.ToString() + "_player";
			transform.gameObject.layer = LayerMask.NameToLayer(layerName);
			transform.gameObject.name = transform.gameObject.name + "_IgnoreDamge";
		}
		else
		{
			Debug.LogWarning("HeroShawangMe: 'chongci' child transform not found on " + base.gameObject.name);
		}
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000BE647 File Offset: 0x000BC847
	private IEnumerator superSkillCheckEnemy()
	{
		while (this.ReleaseSuper && this.detectTimer < 4.5f)
		{
			yield return new WaitForSeconds(0.1f);
			this.detectTimer += 0.1f;
			this.GetDeadMemberNum();
		}
		yield break;
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000BE656 File Offset: 0x000BC856
	private IEnumerator delaySkillFinish()
	{
		yield return new WaitForSeconds(5f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000BE668 File Offset: 0x000BC868
	public override void SuperSkillFinish()
	{
		base.SuperSkillFinish();
		this.SuperDoing = false;
		base.hero.SetAnimator("SkillSuper", 0, false, 1);
		base.hero.wInput.notInput = false;
		base.hero.bePlayActive = true;
		base.hero.charMove.notMove = false;
		this.targetEnemyIDs.Clear();
		this.newAddedEnemeyIDs.Clear();
		this.goRoot = new List<GameObject>();
		this.targetId = 0;
		this.lasttargetId = 0;
		this.releasedSuper = false;
		this.ReleaseSuper = false;
		base.hero.playerDazhao.superNpc = null;
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000BE720 File Offset: 0x000BC920
	protected override void Update()
	{
		base.Update();
		bool flag = base.hero.playerDazhao.superNpc != null;
		if (flag)
		{
			this.npcLifeTimer += Time.deltaTime;
		}
		bool flag2 = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && !Input_Manager.Get_Button_Down("Skill1") && !base.hero.wInput.notInput;
		if (flag2)
		{
			base.hero.superSkill(null);
		}
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000BE7AC File Offset: 0x000BC9AC
	private int GetDeadMemberNum()
	{
		int num = 0;
		bool flag = this.dazhaoNpc == null;
		int result;
		if (flag)
		{
			this.ReleaseSuper = false;
			result = num;
		}
		else
		{
			bool flag2 = !this.ReleaseSuper;
			if (flag2)
			{
				result = num;
			}
			else
			{
				this.newAddedEnemeyIDs.Clear();
				bool flag3 = this.dazhaoNpc != null && !this.dazhaoNpc.haveDestroy;
				if (flag3)
				{
					Vector3 position = this.dazhaoNpc.UserControl.transform.position;
					Collider[] array = Physics.OverlapSphere(position, 12f, this.layer);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag4 = (this.layer == LayerMask.NameToLayer("force2_npc") || this.layer == LayerMask.NameToLayer("force1_npc")) && array[i].gameObject.tag.CompareTo("can_damage_robot") != 0;
						if (!flag4)
						{
							GameObject gameObject = array[i].gameObject.transform.root.gameObject;
							bool flag5 = this.goRoot.IndexOf(gameObject) != -1;
							if (!flag5)
							{
								this.goRoot.Add(gameObject);
								PublicInfo component = gameObject.GetComponent<PublicInfo>();
								bool flag6 = component != null;
								if (flag6)
								{
									NetLiving netObject = component.NetObject;
									bool flag7 = !netObject.bePlayer && ((Npc_Entity)netObject).Physics != 1;
									if (flag7)
									{
										goto IL_26E;
									}
									bool flag8 = !netObject.isDead;
									if (flag8)
									{
										bool flag9 = Physics.Raycast(position + Vector3.up * 0.5f, array[i].transform.position - position, Vector3.Distance(position, array[i].transform.position), this.pullLayer);
										if (flag9)
										{
											goto IL_26E;
										}
										this.targetId = netObject.getNetid();
									}
								}
								bool flag10 = this.lasttargetId != this.targetId && !this.targetEnemyIDs.Contains(this.targetId);
								if (flag10)
								{
									this.newAddedEnemeyIDs.Add(this.targetId);
									this.targetEnemyIDs.Add(this.targetId);
									this.lasttargetId = this.targetId;
								}
							}
						}
						IL_26E:;
					}
					bool flag11 = this.newAddedEnemeyIDs.Count > 0;
					if (flag11)
					{
						base.hero.SendMessageToServer(2030003, this.newAddedEnemeyIDs);
					}
					result = array.Length;
				}
				else
				{
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x000BEA78 File Offset: 0x000BCC78
	public override void BreakSupperSkill()
	{
		bool superDoing = this.SuperDoing;
		if (superDoing)
		{
			base.SuperSkillFinish();
		}
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x000BEA99 File Offset: 0x000BCC99
	public override void Revive()
	{
		base.Revive();
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
	public override void OnDeath()
	{
		base.OnDeath();
		bool flag = base.hero.skillEffectDict.ContainsKey(1102);
		if (flag)
		{
			((SkillSandstormDashMe)base.hero.skillEffectDict[1102]).OneSkillFinish();
		}
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x000BEAF4 File Offset: 0x000BCCF4
	public override void GetHeroNpc(object obj)
	{
		base.GetHeroNpc(obj);
		base.hero.playerDazhao.superNpc = (Npc_Entity)obj;
		this.dazhaoNpc = (Npc_Entity)obj;
		this.ReleaseSuper = true;
		base.StartCoroutine(this.delaySkillFinish());
		base.StartCoroutine(this.superSkillCheckEnemy());
		this.npcLifeTimer = 0f;
		base.hero.OnStartSuperSkill(null);
	}

	// Token: 0x04000F84 RID: 3972
	private LayerMask layer;

	// Token: 0x04000F85 RID: 3973
	private ArrayList targetEnemyIDs;

	// Token: 0x04000F86 RID: 3974
	private ArrayList newAddedEnemeyIDs;

	// Token: 0x04000F87 RID: 3975
	private bool needCheck;

	// Token: 0x04000F88 RID: 3976
	private bool ReleaseSuper;

	// Token: 0x04000F89 RID: 3977
	private bool SuperDoing;

	// Token: 0x04000F8A RID: 3978
	public Npc_Entity dazhaoNpc;

	// Token: 0x04000F8B RID: 3979
	private float detectTimer;

	// Token: 0x04000F8C RID: 3980
	public int shawangSuperCount;

	// Token: 0x04000F8D RID: 3981
	private int pullLayer;

	// Token: 0x04000F8E RID: 3982
	private bool releasedSuper;

	// Token: 0x04000F8F RID: 3983
	public bool InteruptDash;

	// Token: 0x04000F90 RID: 3984
	private List<GameObject> goRoot = new List<GameObject>();

	// Token: 0x04000F91 RID: 3985
	private int targetId;

	// Token: 0x04000F92 RID: 3986
	private int lasttargetId;

	// Token: 0x04000F93 RID: 3987
	private float npcLifeTimer;
}
