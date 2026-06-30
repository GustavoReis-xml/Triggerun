using System;
using System.Collections.Generic;

// Token: 0x0200003F RID: 63
public class buffNpc : buffBase
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000234 RID: 564 RVA: 0x0000F455 File Offset: 0x0000D655
	protected Npc_Entity Npc
	{
		get
		{
			return (Npc_Entity)this._hero;
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000F462 File Offset: 0x0000D662
	public override void OneBuffInit(NetLiving net)
	{
		base.OneBuffInit(net);
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000F470 File Offset: 0x0000D670
	public bool GetIsBuff(int nBuffId)
	{
		bool result = false;
		bool flag = this.mBuffData.ContainsKey(nBuffId);
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000F49C File Offset: 0x0000D69C
	public void AddOneBuff(string sStartTime, int nBuffId, int nOwnerId)
	{
		bool flag = !this.mBuffData.ContainsKey(nBuffId);
		BuffInfo buffInfo;
		buffData buffdata;
		if (flag)
		{
			buffInfo = new BuffInfo();
			buffInfo.InitBuff(nBuffId, this._hero);
			this.mBuffData.Add(nBuffId, buffInfo);
			buffdata = buffInfo.buffdata;
		}
		else
		{
			buffInfo = this.mBuffData[nBuffId];
		}
		buffdata = buffInfo.buffdata;
		int bType = buffdata.bType;
		int num = bType;
		if (num != 4)
		{
			if (num != 11)
			{
				if (num == 12)
				{
					this.SetDongJieState(true);
				}
			}
			else
			{
				this.SetDongJieState(true);
			}
		}
		else
		{
			int num2 = CFilmManager.Instance.filmForce(this.Npc);
			bool flag2 = this.Npc.playerCommon.m_nShiliServer != num2;
			if (flag2)
			{
				this.Npc.renderBianSe.ChangeMateial(false);
			}
		}
		this.Npc.npceffectBase.AddOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps, DateTime.Parse(sStartTime), 0);
		bool flag3 = nBuffId != 1010 && nBuffId != 1021;
		if (!flag3)
		{
			int num3 = CFilmManager.Instance.filmParentNetId(this.Npc);
			bool flag4 = nOwnerId == num3;
			if (flag4)
			{
				MeBuffModelSign meBuffModelSign = this._hero.UserControl.GetComponent<MeBuffModelSign>();
				bool flag5 = meBuffModelSign == null;
				if (flag5)
				{
					meBuffModelSign = this._hero.UserControl.AddComponent<MeBuffModelSign>();
				}
				meBuffModelSign.OnStart(nBuffId, this._hero);
			}
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000F624 File Offset: 0x0000D824
	public void DeleteOneBuff(int nBuffId, int nReason, int nOwnerId)
	{
		bool flag = this.Npc == null;
		if (!flag)
		{
			bool flag2 = !this.mBuffData.ContainsKey(nBuffId);
			if (!flag2)
			{
				BuffInfo buffInfo = this.mBuffData[nBuffId];
				buffData buffdata = buffInfo.buffdata;
				int bType = buffdata.bType;
				int num = bType;
				if (num != 4)
				{
					if (num != 11)
					{
						if (num == 12)
						{
							this.SetDongJieState(false);
						}
					}
					else
					{
						this.SetDongJieState(false);
					}
				}
				else
				{
					int num2 = CFilmManager.Instance.filmForce(this.Npc);
					bool flag3 = this.Npc.playerCommon.m_nShiliServer != num2;
					if (flag3)
					{
						this.Npc.renderBianSe.ChangeMateial(true);
					}
				}
				this.Npc.npceffectBase.DelOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps);
				this.mBuffData.Remove(nBuffId);
				bool flag4 = nBuffId != 1010 && nBuffId != 1021;
				if (!flag4)
				{
					int num3 = CFilmManager.Instance.filmParentNetId(this.Npc);
					bool flag5 = nOwnerId == num3;
					if (flag5)
					{
						MeBuffModelSign component = this._hero.UserControl.GetComponent<MeBuffModelSign>();
						bool flag6 = component != null;
						if (flag6)
						{
							component.OnDel();
						}
					}
				}
			}
		}
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000F788 File Offset: 0x0000D988
	private void SetDongJieState(bool mbool)
	{
		this.Npc.playerCommon.m_nDongjie = (mbool ? 1 : 0);
		this.Npc.Can_Shoot = !mbool;
		if (mbool)
		{
			fGameManager.GetInstance().Stop_Npc_All_Action(this.Npc);
		}
		else
		{
			fGameManager.GetInstance().Start_Npc_All_Action(this.Npc);
		}
	}

	// Token: 0x040001C5 RID: 453
	public Dictionary<int, BuffInfo> mBuffData = new Dictionary<int, BuffInfo>();
}
