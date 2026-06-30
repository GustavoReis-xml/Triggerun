using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class buffIt : buffBase
{
	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000203 RID: 515 RVA: 0x0000CE42 File Offset: 0x0000B042
	protected NetLiving gameplayer
	{
		get
		{
			return this._hero;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000CE4A File Offset: 0x0000B04A
	public override void OneBuffInit(NetLiving net)
	{
		base.OneBuffInit(net);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000CE58 File Offset: 0x0000B058
	public bool GetIsBuff(int nBuffId)
	{
		bool result = false;
		bool flag = this.mBuffItData.ContainsKey(nBuffId);
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000CE84 File Offset: 0x0000B084
	public void AddBuffList(ArrayList szBuff)
	{
		int count = szBuff.Count;
		for (int i = 0; i < count; i++)
		{
			ArrayList arrayList = (ArrayList)szBuff[i];
			bool flag = arrayList.Count >= 1;
			if (flag)
			{
				this.AddOneBuff(arrayList[0].ToString(), (int)arrayList[1], (int)arrayList[2]);
			}
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
	public void AddOneBuff(string sStartTime, int nBuffId, int nOwnerId)
	{
		bool flag = this.gameplayer == null || this.gameplayer.haveDestroy || !this.gameplayer.playerHaveCreate;
		if (!flag)
		{
			bool flag2 = !this.mBuffItData.ContainsKey(nBuffId);
			BuffInfo buffInfo;
			if (flag2)
			{
				buffInfo = new BuffInfo();
				buffInfo.InitBuff(nBuffId, this._hero);
				this.mBuffItData.Add(nBuffId, buffInfo);
			}
			else
			{
				buffInfo = this.mBuffItData[nBuffId];
			}
			buffData buffdata = buffInfo.buffdata;
			switch (buffdata.bType)
			{
			case 4:
			{
				NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nOwnerId);
				bool flag3 = netLiving != null && !netLiving.haveDestroy;
				if (flag3)
				{
					int num = CFilmManager.Instance.filmForce(this.gameplayer);
					bool flag4 = this.gameplayer.renderBianSe && netLiving.playerCommon.m_nShiliServer == num;
					if (flag4)
					{
						this.gameplayer.renderBianSe.ChangeMateial(false);
					}
				}
				break;
			}
			case 5:
				this.gameplayer.playerCommon.m_nStatForce = true;
				this.gameplayer.bePlayActive = false;
				break;
			case 8:
			{
				int nBuffChangeValueSe = buffdata.m_nBuffChangeValueSe;
				this.gameplayer.playerCommon.buffSpeed = (float)nBuffChangeValueSe * 0.01f;
				this.gameplayer.ChangeSpeed();
				break;
			}
			case 10:
			{
				bool flag5 = this.gameplayer.heroBase != null;
				if (flag5)
				{
					this.gameplayer.heroBase.ChangeYinShenState();
				}
				else
				{
					Debug.Log("玩家中隐身buff时该玩家还没有heroBase数据: 玩家名: " + this.gameplayer.playerCommon.m_sName + "英雄名: " + this.gameplayer.heroData.hname);
				}
				break;
			}
			case 11:
				this.SetDongJieState(true);
				break;
			case 12:
				this.SetHunMi(true);
				break;
			case 13:
			{
				bool flag6 = this.gameplayer.renderBianSe;
				if (flag6)
				{
					this.gameplayer.renderBianSe.InvincibleMat(true);
				}
				break;
			}
			case 14:
				this.gameplayer.playerCommon.m_nChenMo = 1;
				break;
			}
			this.gameplayer.playerEffectBase.AddOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps, DateTime.Parse(sStartTime), 0);
			base.PlayBuffBeginSound(buffInfo.audioBegin);
			bool flag7 = this.gameplayer.newplayercon != null;
			if (flag7)
			{
				this.gameplayer.newplayercon.AddBuffIco(nBuffId, buffdata.m_sHeadeffectico);
			}
			bool flag8 = nBuffId != 1010 && nBuffId != 1021;
			if (!flag8)
			{
				int num2 = CFilmManager.Instance.filmParentNetId(this.gameplayer);
				bool flag9 = nOwnerId == num2;
				if (flag9)
				{
					MeBuffModelSign meBuffModelSign = this.gameplayer.UserControl.GetComponent<MeBuffModelSign>();
					bool flag10 = meBuffModelSign == null;
					if (flag10)
					{
						meBuffModelSign = this.gameplayer.UserControl.AddComponent<MeBuffModelSign>();
					}
					meBuffModelSign.OnStart(nBuffId, this.gameplayer);
				}
			}
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000D244 File Offset: 0x0000B444
	public void DeleteOneBuff(int nBuffId, int nReason, int nOwnerId)
	{
		bool flag = this.gameplayer == null || this.gameplayer.haveDestroy || !this.mBuffItData.ContainsKey(nBuffId);
		if (!flag)
		{
			BuffInfo buffInfo = this.mBuffItData[nBuffId];
			buffData buffdata = buffInfo.buffdata;
			switch (buffdata.bType)
			{
			case 4:
				this.gameplayer.renderBianSe.ChangeMateial(true);
				break;
			case 5:
				this.gameplayer.playerCommon.m_nStatForce = false;
				this.gameplayer.bePlayActive = true;
				break;
			case 8:
				this.gameplayer.playerCommon.buffSpeed = 0f;
				this.gameplayer.ChangeSpeed();
				break;
			case 10:
				this.gameplayer.heroBase.changeBackToNormalState();
				break;
			case 11:
				this.SetDongJieState(false);
				break;
			case 12:
				this.SetHunMi(false);
				break;
			case 13:
			{
				bool flag2 = this.gameplayer.renderBianSe;
				if (flag2)
				{
					this.gameplayer.renderBianSe.InvincibleMat(false);
				}
				break;
			}
			case 14:
				this.gameplayer.playerCommon.m_nChenMo = 0;
				break;
			}
			this.gameplayer.playerEffectBase.DelOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps);
			base.StopBuffSound(buffInfo.audioBegin);
			base.PlayBuffEndSound(buffInfo.audioEnd);
			this.mBuffItData.Remove(nBuffId);
			bool flag3 = this.gameplayer.newplayercon != null;
			if (flag3)
			{
				this.gameplayer.newplayercon.DelBuffIco(nBuffId);
			}
			bool flag4 = nBuffId != 1010 && nBuffId != 1021;
			if (!flag4)
			{
				int num = CFilmManager.Instance.filmParentNetId(this.gameplayer);
				bool flag5 = nOwnerId == num;
				if (flag5)
				{
					MeBuffModelSign component = this.gameplayer.UserControl.GetComponent<MeBuffModelSign>();
					bool flag6 = component != null;
					if (flag6)
					{
						component.OnDel();
					}
				}
			}
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000D47C File Offset: 0x0000B67C
	private void SetDongJieState(bool mbool)
	{
		if (mbool)
		{
			this.gameplayer.heroBase.BreakSupperSkill();
		}
		this.gameplayer.playerCommon.m_nDongjie = (mbool ? 1 : 0);
		this.gameplayer.bePlayActive = !mbool;
		this.gameplayer.heroBase.Freeze(mbool);
		bool playerHaveCreate = this.gameplayer.playerHaveCreate;
		if (playerHaveCreate)
		{
			if (mbool)
			{
				this.gameplayer.tChar.animatorAffectBySpeed = true;
				this.gameplayer.tCharAnimationTps.animator.speed = 0f;
			}
			else
			{
				bool flag = !mbool;
				if (flag)
				{
					this.gameplayer.tChar.animatorAffectBySpeed = false;
					this.gameplayer.tCharAnimationTps.animator.speed = 1f;
				}
			}
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000D557 File Offset: 0x0000B757
	private IEnumerator Wait_Action(bool mbool)
	{
		yield return new WaitForSeconds(0.2f);
		yield break;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000D570 File Offset: 0x0000B770
	private void SetHunMi(bool mbool)
	{
		if (mbool)
		{
			this.gameplayer.heroBase.BreakSupperSkill();
		}
		this.gameplayer.playerCommon.m_nHunMi = (mbool ? 1 : 0);
		this.gameplayer.bePlayActive = !mbool;
		this.gameplayer.heroBase.Stun(mbool);
		bool playerHaveCreate = this.gameplayer.playerHaveCreate;
		if (playerHaveCreate)
		{
			this.gameplayer.charMove.notMove = mbool;
			bool flag = this.gameplayer.aimSystem != null;
			if (flag)
			{
				this.gameplayer.aimSystem.AimEnable(!mbool);
			}
		}
	}

	// Token: 0x040001AE RID: 430
	public Dictionary<int, BuffInfo> mBuffItData = new Dictionary<int, BuffInfo>();
}
