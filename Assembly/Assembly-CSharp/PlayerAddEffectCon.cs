using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000290 RID: 656
public class PlayerAddEffectCon : Window
{
	// Token: 0x06001DCB RID: 7627 RVA: 0x00116518 File Offset: 0x00114718
	protected override void Awake()
	{
		base.Awake();
		this.effectImg = base.transform.Find("effect").GetComponent<Image>();
		this.heroname = base.transform.Find("name").GetComponent<Text>();
		this.oRoundObj = base.transform.Find("round").gameObject;
		this.RoundImg = this.oRoundObj.transform.Find("Image (1)").GetComponent<Image>();
		this.lshowclone = base.transform.Find("lshowclone").gameObject;
		this.lshowclone.SetActive(false);
		this.zg_name = base.transform.Find("zg_name").GetComponent<Text>();
		this.zhiliao_jiahao = base.transform.Find("ui_zhiliao_jiahao").gameObject;
		this.hpObj = base.transform.FindChild("hp").gameObject;
		this.hpcon = this.hpObj.AddComponent<HpControl>();
		this.Init();
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x00116630 File Offset: 0x00114830
	private void Init()
	{
		this.effectImg.enabled = false;
		this.heroname.text = string.Empty;
		this.oRoundObj.SetActive(false);
		this.RoundImg.enabled = false;
		this.zg_name.text = string.Empty;
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x00116688 File Offset: 0x00114888
	public void OnShowFriendName(int targetID)
	{
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(targetID);
		bool flag = netLiving != null;
		if (flag)
		{
			this.zg_name.text = Language.GetLang(3016, new object[]
			{
				netLiving.playerCommon.m_sName
			});
			base.CancelInvoke("doWait");
			base.Invoke("doWait", 3f);
		}
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x001166FD File Offset: 0x001148FD
	private void doWait()
	{
		this.zg_name.text = string.Empty;
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x00116714 File Offset: 0x00114914
	private void LockTimeEffect()
	{
		float num = (float)(Fun.Now - this.m_dDate).TotalMilliseconds;
		bool flag = num > 500f;
		if (flag)
		{
			this.doClose();
			base.transform.gameObject.SetActive(false);
			this.Init();
		}
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x0011676A File Offset: 0x0011496A
	private void doClose()
	{
		base.CancelInvoke("LockTimeEffect");
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x0011677C File Offset: 0x0011497C
	public void OnToMeData(int damageType, int attackId, int nCureTarghtID)
	{
		NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(attackId);
		bool flag = netLiving != null;
		if (flag)
		{
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			bool flag2 = !base.transform.gameObject.activeSelf;
			if (flag2)
			{
				base.transform.gameObject.SetActive(true);
			}
			this.effectImg.enabled = true;
			this.oRoundObj.SetActive(true);
			this.RoundImg.enabled = true;
			string imageName = "jiagong";
			string imageName2 = "jiagong_02";
			bool flag3 = damageType == 1;
			if (flag3)
			{
				imageName = "jiaxue";
				imageName2 = "jiaxue_02";
				this.zhiliao_jiahao.SetActive(true);
			}
			else
			{
				this.zhiliao_jiahao.SetActive(false);
			}
			this.effectImg.sprite = Window.spriteName(AtlasType.ATLAS_GAME, imageName);
			this.RoundImg.sprite = Window.spriteName(AtlasType.ATLAS_GAME, imageName2);
			this.heroname.text = netLiving.playerCommon.m_sName;
			this.OnAttackDir(player.thisModel, netLiving.thisModel);
			this.CheckHp((GamePlayer)netLiving);
			this.m_dDate = Fun.Now;
			bool flag4 = !base.IsInvoking("LockTimeEffect");
			if (flag4)
			{
				this.doClose();
				base.InvokeRepeating("LockTimeEffect", 0f, 0.1f);
			}
		}
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x001168F4 File Offset: 0x00114AF4
	public void OnToItData(int damageType, int attackId, int nCureTarghtID, GamePlayer _NowGamePlayer, ArrayList szHp)
	{
		bool flag = !base.transform.gameObject.activeSelf;
		if (flag)
		{
			base.transform.gameObject.SetActive(true);
		}
		this.effectImg.enabled = true;
		string imageName = "icon_jiagong";
		bool flag2 = damageType == 1;
		if (flag2)
		{
			imageName = "icon_jiaxue";
			this.zhiliao_jiahao.SetActive(true);
		}
		else
		{
			this.zhiliao_jiahao.SetActive(false);
		}
		this.effectImg.sprite = Window.spriteName(AtlasType.ATLAS_GAME, imageName);
		this.heroname.text = _NowGamePlayer.playerCommon.m_sName;
		this.CheckHp(_NowGamePlayer);
		this.m_dDate = Fun.Now;
		bool flag3 = !base.IsInvoking("LockTimeEffect");
		if (flag3)
		{
			this.doClose();
			base.InvokeRepeating("LockTimeEffect", 0f, 0.1f);
		}
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x001169E4 File Offset: 0x00114BE4
	private void OnAttackDir(GameObject _MeModel, GameObject _AttackModel)
	{
		bool flag = !(_MeModel == null) && !(_AttackModel == null);
		if (flag)
		{
			Vector3 position = _MeModel.transform.position;
			Vector3 position2 = _AttackModel.transform.position;
			Vector3 from = position2 - position;
			from.y = 0f;
			Vector3 forward = _MeModel.transform.forward;
			forward.y = 0f;
			float num = Vector3.Angle(from, forward);
			bool flag2 = _MeModel.transform.InverseTransformPoint(position2).x > 0f;
			if (flag2)
			{
				num = 360f - num;
			}
			bool flag3 = num != this.oldRet;
			if (flag3)
			{
				this.oRoundObj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, num);
				this.oldRet = num;
			}
		}
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x00116ACC File Offset: 0x00114CCC
	private void CheckHp(GamePlayer gameplayer)
	{
		bool flag = !(this.hpcon == null) && gameplayer != null;
		if (flag)
		{
			int nowHp = gameplayer.playerCommon.GetNowHp();
			bool flag2 = nowHp != this.nOldHp;
			if (flag2)
			{
				int maxHp = gameplayer.playerCommon.GetMaxHp();
				this.hpcon.SetHp(maxHp, gameplayer.playerCommon.m_nHp, gameplayer.playerCommon.m_nArmor, gameplayer.playerCommon.m_nShield, 0);
				this.nOldHp = nowHp;
			}
		}
	}

	// Token: 0x04001AF8 RID: 6904
	private Image effectImg;

	// Token: 0x04001AF9 RID: 6905
	private Text heroname;

	// Token: 0x04001AFA RID: 6906
	private Text zg_name;

	// Token: 0x04001AFB RID: 6907
	private GameObject oRoundObj;

	// Token: 0x04001AFC RID: 6908
	private GameObject lshowclone;

	// Token: 0x04001AFD RID: 6909
	private GameObject zhiliao_jiahao;

	// Token: 0x04001AFE RID: 6910
	private Image RoundImg;

	// Token: 0x04001AFF RID: 6911
	private DateTime m_dDate;

	// Token: 0x04001B00 RID: 6912
	private GameObject hpObj;

	// Token: 0x04001B01 RID: 6913
	private HpControl hpcon;

	// Token: 0x04001B02 RID: 6914
	private float oldRet;

	// Token: 0x04001B03 RID: 6915
	private int nOldHp;
}
