using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A1 RID: 417
public class HeroLuoLiMe : HeroBaseMe
{
	// Token: 0x06001366 RID: 4966 RVA: 0x000B8BF8 File Offset: 0x000B6DF8
	public override void HeroInit(NetLiving net, string heroclass)
	{
		base.HeroInit(net, heroclass);
		this.LoadLinghunqiuPrefeb();
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x000B8C0C File Offset: 0x000B6E0C
	private void LoadLinghunqiuPrefeb()
	{
		WWW resWWW = GameRoot.GetResWWW("EffectRes/linhunqiu.ef");
		bool flag = resWWW == null || resWWW.error != null;
		if (flag)
		{
			GameRoot.LoadAsset("EffectRes/linhunqiu.ef", new fResourceManager.DownFinishDelegate(this.loadLinghunqiuPrefab), 0);
		}
		else
		{
			this.loadLinghunqiuPrefab(resWWW, 0);
		}
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x000B8C6C File Offset: 0x000B6E6C
	private void loadLinghunqiuPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
		}
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x000B8C90 File Offset: 0x000B6E90
	public override void SuperSkillDo(object parm)
	{
		base.SuperSkillDo(parm);
		this.superDoing = true;
		base.hero.SetAnimator("skill_Q", 0, true, 2);
		base.hero.wInput.notInput = true;
		base.hero.charMove.notMove = true;
		base.StartCoroutine(this.SuperFinish());
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000B8CF6 File Offset: 0x000B6EF6
	private IEnumerator SuperFinish()
	{
		yield return new WaitForSeconds(0.8f);
		this.SuperSkillFinish();
		yield break;
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x000B8D08 File Offset: 0x000B6F08
	public override void SuperSkillFinish()
	{
		bool flag = base.hero != null && !base.hero.haveDestroy;
		if (flag)
		{
			base.hero.wInput.notInput = false;
			base.hero.charMove.notMove = false;
			base.hero.SetAnimator("skill_Q", 0, false, 2);
			this.superDoing = false;
			base.SuperSkillFinish();
		}
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x000B8D80 File Offset: 0x000B6F80
	public override void InitForModelLoad()
	{
		base.InitForModelLoad();
		bool flag = !base.hero.isDead && !base.hero.haveDestroy;
		if (flag)
		{
		}
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x000B8DBC File Offset: 0x000B6FBC
	public void AddHP()
	{
		this.npcAddHP.Clear();
		this.gpAddHP.Clear();
		foreach (KeyValuePair<string, NetObj> keyValuePair in GameRoot.SceneSocket.unFsorket.NetObjTable)
		{
			bool flag = !keyValuePair.Value.bePlayer;
			if (flag)
			{
				NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(keyValuePair.Value.getNetid());
				bool flag2 = netObj.netobject_type == 2;
				if (flag2)
				{
					Npc_Entity npc_Entity = (Npc_Entity)netObj;
					bool flag3 = !npc_Entity.isDead && !npc_Entity.haveDestroy && npc_Entity.nForce == this._hero.nForce && npc_Entity.Cure == 1 && npc_Entity.playerCommon.GetHpBiLi() != 1f;
					if (flag3)
					{
						this.npcAddHP.Add(npc_Entity);
					}
				}
			}
			else
			{
				NetLiving netLiving = (NetLiving)keyValuePair.Value;
				bool flag4 = !netLiving.isDead && !netLiving.haveDestroy && netLiving.nForce == this._hero.nForce && netLiving.playerCommon.GetHpBiLi() != 1f;
				if (flag4)
				{
					this.gpAddHP.Add(netLiving);
				}
			}
		}
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x000B8F58 File Offset: 0x000B7158
	public void Heal()
	{
		this.canServe = false;
		this.lifeTime += Time.deltaTime;
		bool flag = this.lifeTime < 1f;
		if (!flag)
		{
			this.trueAddHP.Clear();
			this.lifeTime = 0f;
			this.AddHP();
			for (int i = 0; i < this.gpAddHP.Count; i++)
			{
				float num = (float)Mathf.FloorToInt(Vector3.Distance(this.gpAddHP[i].UserControl.transform.position, base.transform.position));
				bool flag2 = num < 15f;
				if (flag2)
				{
					this.trueAddHP.Add(this.gpAddHP[i].getNetid());
				}
			}
			for (int j = 0; j < this.npcAddHP.Count; j++)
			{
				float num = (float)Mathf.FloorToInt(Vector3.Distance(this.npcAddHP[j].UserControl.transform.position, base.transform.position));
				bool flag3 = num < 15f;
				if (flag3)
				{
					this.trueAddHP.Add(this.npcAddHP[j].getNetid());
				}
			}
			bool flag4 = this.otherAddHP.Count == this.trueAddHP.Count;
			if (flag4)
			{
				for (int k = 0; k < this.trueAddHP.Count; k++)
				{
					bool flag5 = !this.trueAddHP.Contains(this.otherAddHP[k]);
					if (flag5)
					{
						this.canServe = true;
					}
				}
			}
			else
			{
				this.canServe = true;
			}
			bool flag6 = this.canServe;
			if (flag6)
			{
				base.hero.SendMessageToServer(2070001, this.trueAddHP);
			}
			this.otherAddHP.Clear();
			for (int l = 0; l < this.trueAddHP.Count; l++)
			{
				this.otherAddHP.Add(this.trueAddHP[l]);
			}
		}
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x000B91A8 File Offset: 0x000B73A8
	protected override void Update()
	{
		base.Update();
		this.Heal();
		int deadMemberNum = this.GetDeadMemberNum();
		bool flag = deadMemberNum != this.canReviveNum;
		if (flag)
		{
			this.canReviveNum = deadMemberNum;
			bool flag2 = gameuiCon.inst != null;
			if (flag2)
			{
				gameuiCon.inst.mainMsgUI.OnFriendDeadNum(this.canReviveNum);
			}
		}
		bool flag3 = this.InputCheck() && Input_Manager.Get_Button_Down("superskill") && deadMemberNum > 0 && !base.hero.wInput.notInput;
		if (flag3)
		{
			base.hero.superSkill(this.szReliveId);
		}
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x000B9254 File Offset: 0x000B7454
	public void SetDeadMember(NetLiving gp, int ntype)
	{
		bool flag = !gp.bePlayer;
		if (!flag)
		{
			bool flag2 = this.szDeadMember.Contains(gp);
			bool flag3 = ntype == 1;
			if (flag3)
			{
				bool flag4 = !flag2;
				if (flag4)
				{
					this.szDeadMember.Add(gp);
				}
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					this.szDeadMember.Remove(gp);
				}
			}
		}
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x000B92B8 File Offset: 0x000B74B8
	private int GetDeadMemberNum()
	{
		int num = 0;
		bool flag = this.szDeadMember.Count == 0;
		int result;
		if (flag)
		{
			result = num;
		}
		else
		{
			bool flag2 = !base.hero.playerDazhao.canDo;
			if (flag2)
			{
				result = num;
			}
			else
			{
				bool isDead = base.hero.isDead;
				if (isDead)
				{
					result = num;
				}
				else
				{
					this.szReliveId.Clear();
					for (int i = 0; i < this.szDeadMember.Count; i++)
					{
						bool flag3 = this.szDeadMember[i] == null || this.szDeadMember[i].thisModel == null;
						if (!flag3)
						{
							bool rollDead = this.szDeadMember[i].RollDead;
							if (rollDead)
							{
								int num2 = Mathf.FloorToInt(Vector3.Distance(this.szDeadMember[i].userBip.transform.position, base.transform.position));
								bool flag4 = num2 <= 20;
								if (flag4)
								{
									this.szReliveId.Add(this.szDeadMember[i].getNetid());
									ArrayList arrayList = new ArrayList();
									arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].UserControl.transform.position.x * 100f));
									arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].userBip.transform.position.y * 100f + 30f));
									arrayList.Add(Mathf.RoundToInt(this.szDeadMember[i].UserControl.transform.position.z * 100f));
									this.szReliveId.Add(arrayList);
									num++;
								}
							}
							else
							{
								int num3 = Mathf.FloorToInt(Vector3.Distance(this.szDeadMember[i].UserControl.transform.position, base.transform.position));
								bool flag5 = num3 <= 20;
								if (flag5)
								{
									this.szReliveId.Add(this.szDeadMember[i].getNetid());
									ArrayList arrayList2 = new ArrayList();
									arrayList2.Add(Mathf.RoundToInt(this.szDeadMember[i].UserControl.transform.position.x * 100f));
									arrayList2.Add(Mathf.RoundToInt(this.szDeadMember[i].UserControl.transform.position.y * 100f + 5f));
									arrayList2.Add(Mathf.RoundToInt(this.szDeadMember[i].UserControl.transform.position.z * 100f));
									this.szReliveId.Add(arrayList2);
									num++;
								}
							}
						}
					}
					result = num;
				}
			}
		}
		return result;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000B9615 File Offset: 0x000B7815
	public override void OnDeath()
	{
		base.OnDeath();
		gameuiCon.inst.mainMsgUI.OnFriendDeadNum(0);
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000B9630 File Offset: 0x000B7830
	public override void Revive()
	{
		base.Revive();
		base.hero.wInput.notInput = false;
		base.hero.charMove.notMove = false;
	}

	// Token: 0x04000F15 RID: 3861
	public bool superDoing;

	// Token: 0x04000F16 RID: 3862
	private int canReviveNum;

	// Token: 0x04000F17 RID: 3863
	private List<NetLiving> szDeadMember = new List<NetLiving>();

	// Token: 0x04000F18 RID: 3864
	private ArrayList szReliveId = new ArrayList();

	// Token: 0x04000F19 RID: 3865
	private List<NetLiving> gpAddHP = new List<NetLiving>();

	// Token: 0x04000F1A RID: 3866
	private List<Npc_Entity> npcAddHP = new List<Npc_Entity>();

	// Token: 0x04000F1B RID: 3867
	private ArrayList trueAddHP = new ArrayList();

	// Token: 0x04000F1C RID: 3868
	private ArrayList otherAddHP = new ArrayList();

	// Token: 0x04000F1D RID: 3869
	private float lifeTime;

	// Token: 0x04000F1E RID: 3870
	private bool canServe;
}
