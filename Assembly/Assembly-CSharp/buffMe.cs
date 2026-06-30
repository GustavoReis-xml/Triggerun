using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class buffMe : buffBase
{
	// Token: 0x17000072 RID: 114
	// (get) Token: 0x0600022B RID: 555 RVA: 0x0000EC75 File Offset: 0x0000CE75
	protected HeroPlayer player
	{
		get
		{
			return (HeroPlayer)this._hero;
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000EC82 File Offset: 0x0000CE82
	public override void OneBuffInit(NetLiving net)
	{
		base.OneBuffInit(net);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000EC90 File Offset: 0x0000CE90
	public void AddOneBuff(string sStartTime, int nBuffId, int nOwnerId)
	{
		bool flag = !this.mBuffMeData.ContainsKey(nBuffId);
		BuffInfo buffInfo;
		buffData buffdata;
		if (flag)
		{
			buffInfo = new BuffInfo();
			buffInfo.InitBuff(nBuffId, this._hero);
			this.mBuffMeData.Add(nBuffId, buffInfo);
			buffdata = buffInfo.buffdata;
			bool flag2 = buffdata.m_nIsMsgTxt == 1;
			if (flag2)
			{
				NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nOwnerId);
				bool flag3 = netLiving != null;
				if (flag3)
				{
					string sMsg = (netLiving.nForce != this.player.nForce) ? Language.GetLang(3020, new object[]
					{
						netLiving.playerCommon.m_sName,
						"“" + buffdata.bname + "”"
					}) : ((nOwnerId != this.player.getNetid()) ? Language.GetLang(3019, new object[]
					{
						netLiving.playerCommon.m_sName,
						"“" + buffdata.bname + "”"
					}) : Language.GetLang(3025, new object[]
					{
						"“" + buffdata.bname + "”"
					}));
					bool flag4 = gameuiCon.inst != null;
					if (flag4)
					{
						gameuiCon.inst.mainMsgUI.AddGameMsg(sMsg);
					}
				}
			}
		}
		else
		{
			buffInfo = this.mBuffMeData[nBuffId];
		}
		buffdata = buffInfo.buffdata;
		bool flag5 = buffdata.m_nStopFaceActive == 1;
		if (flag5)
		{
			this.player.CheckActiveStat();
		}
		bool flag6 = buffdata.m_nStopHelloActive == 1 && !this.player.CanHuDong;
		if (flag6)
		{
			this.player.CheckHuDongState();
		}
		switch (buffdata.bType)
		{
		case 5:
			this.player.playerCommon.m_nStatForce = true;
			this.player.bePlayActive = false;
			break;
		case 8:
		{
			int nBuffChangeValueSe = buffdata.m_nBuffChangeValueSe;
			this.player.playerCommon.buffSpeed = (float)nBuffChangeValueSe * 0.01f;
			this.player.ChangeSpeed();
			break;
		}
		case 10:
			this.player.heroBase.ChangeYinShenState();
			break;
		case 11:
			this.SetDongJieState(true);
			break;
		case 12:
			this.SetHunMi(true);
			break;
		case 14:
			this.player.playerCommon.m_nChenMo = 1;
			this.player.heroBase.SetChenMo(true);
			break;
		}
		this.player.playerEffectBase.AddOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps, DateTime.Parse(sStartTime), 0);
		base.PlayBuffBeginSound(buffInfo.audioBegin);
		bool flag7 = buffdata.m_nSoundbuff == 1;
		if (flag7)
		{
			bool flag8 = !this.player.playerSound.hasBuffSound(nBuffId);
			if (flag8)
			{
				string url = string.Format("buff_{0}_{1}", nBuffId, this.player.heroData.heroclass);
				this.player.playerSound.LoadBuffSound(url, nBuffId);
			}
			else
			{
				this.player.playerSound.PlayBuffSound(nBuffId);
			}
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000EFEC File Offset: 0x0000D1EC
	public void DeleteOneBuff(int nBuffId, int nReason, int nOwnerId)
	{
		bool flag = this.player == null;
		if (!flag)
		{
			string sMsg = string.Empty;
			bool flag2 = !this.mBuffMeData.ContainsKey(nBuffId);
			if (!flag2)
			{
				BuffInfo buffInfo = this.mBuffMeData[nBuffId];
				buffData buffdata = buffInfo.buffdata;
				switch (buffdata.bType)
				{
				case 5:
					this.player.playerCommon.m_nStatForce = false;
					this.player.bePlayActive = true;
					break;
				case 8:
					this.player.playerCommon.buffSpeed = 0f;
					this.player.ChangeSpeed();
					break;
				case 10:
					this.player.heroBase.changeBackToNormalState();
					break;
				case 11:
					this.SetDongJieState(false);
					break;
				case 12:
					this.SetHunMi(false);
					break;
				case 14:
					this.player.playerCommon.m_nChenMo = 0;
					this.player.heroBase.SetChenMo(false);
					break;
				}
				this.player.playerEffectBase.DelOneEffect(buffdata.m_sBuffEff_fps, buffdata.m_sBuffEff_tps);
				base.StopBuffSound(buffInfo.audioBegin);
				base.PlayBuffEndSound(buffInfo.audioEnd);
				bool flag3 = buffdata.m_nIsMsgTxt == 1;
				if (flag3)
				{
					NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(nOwnerId);
					bool flag4 = netLiving != null;
					if (flag4)
					{
						string text = (netLiving.nForce == this.player.nForce) ? "#00FFFF" : "#ff0000";
						sMsg = Language.GetLang(3021, new object[]
						{
							text,
							"“" + buffdata.bname + "”"
						});
						gameuiCon.inst.mainMsgUI.AddGameMsg(sMsg);
					}
				}
				this.mBuffMeData.Remove(nBuffId);
			}
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
	private void SetDongJieState(bool mbool)
	{
		if (mbool)
		{
			this.player.heroBase.BreakSupperSkill();
		}
		this.player.playerCommon.m_nDongjie = (mbool ? 1 : 0);
		this.player.bePlayActive = !mbool;
		bool playerHaveCreate = this.player.playerHaveCreate;
		if (playerHaveCreate)
		{
			this.player.charMove.notMove = mbool;
			this.player.wInput.notInput = mbool;
			if (mbool)
			{
				this.player.tChar.animatorAffectBySpeed = true;
				this.player.tCharAnimationTps.animator.speed = 0f;
			}
			else
			{
				bool flag = !mbool;
				if (flag)
				{
					this.player.tChar.animatorAffectBySpeed = false;
					this.player.tCharAnimationTps.animator.speed = 1f;
				}
			}
			this.player.heroBase.Freeze(mbool);
			CameraControl.Instance.dontMove = mbool;
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000F300 File Offset: 0x0000D500
	private IEnumerator Wait_Action(bool mbool)
	{
		yield return new WaitForSeconds(0.4f);
		this.player.tCharAnimationFps.animator.enabled = !mbool;
		this.player.tCharAnimationTps.animator.enabled = !mbool;
		yield break;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000F318 File Offset: 0x0000D518
	private void SetHunMi(bool mbool)
	{
		if (mbool)
		{
			this.player.heroBase.BreakSupperSkill();
		}
		this.player.playerCommon.m_nHunMi = (mbool ? 1 : 0);
		this.player.bePlayActive = !mbool;
		bool playerHaveCreate = this.player.playerHaveCreate;
		if (playerHaveCreate)
		{
			this.player.charMove.notMove = mbool;
			this.player.wInput.notInput = mbool;
			bool flag = this.player.aimSystem != null;
			if (flag)
			{
				this.player.aimSystem.enabled = !mbool;
			}
			this.player.SetAnimator("Stun", 0, true, 1);
			this.player.SetAnimator("HuDongState", 2, 0, 2);
			this.player.heroBase.Stun(mbool);
			CameraControl.Instance.dontMove = mbool;
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000F418 File Offset: 0x0000D618
	public bool GetIsBuff(int nBuffId)
	{
		bool result = false;
		bool flag = this.mBuffMeData.ContainsKey(nBuffId);
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x040001C4 RID: 452
	public Dictionary<int, BuffInfo> mBuffMeData = new Dictionary<int, BuffInfo>();
}
