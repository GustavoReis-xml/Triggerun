using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class GameNpcUiCon : MonoBehaviour
{
	// Token: 0x06000C93 RID: 3219 RVA: 0x00076F50 File Offset: 0x00075150
	private void Awake()
	{
		this._showUi = base.transform.FindChild("show").gameObject;
		this.showUiTransform = this._showUi.GetComponent<Transform>();
		this.topNameObj = this._showUi.transform.FindChild("top").gameObject;
		this.lzname = this.topNameObj.transform.Find("name").GetComponent<MeshRenderer>();
		this.lzname.enabled = false;
		this._showUi.transform.FindChild("hp").gameObject.SetActive(false);
		this.hpObj = this._showUi.transform.FindChild("newhp").gameObject;
		this.hpcon = this.hpObj.AddComponent<HpNewControl>();
		this._hpParent = this.hpObj;
		this.ui_jiantou = this._showUi.transform.FindChild("ui_jiantou_bai").gameObject;
		this.clonebuff = this._showUi.transform.FindChild("clonebuff").gameObject;
		this.clonebuff.SetActive(false);
		this.bufflist = this._showUi.transform.FindChild("bufflist").gameObject;
		this.bufflist.SetActive(false);
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x000770B0 File Offset: 0x000752B0
	public void LoadModeSetUi(Npc_Entity oNpc)
	{
		this.heroplayer = fGameManager.GetInstance().getPlayer();
		bool flag = this.heroplayer != null && oNpc != null && this.heroplayer.playerCommon.m_nShili != oNpc.nForce;
		if (flag)
		{
			this._NpcEntity = oNpc;
			this.nOldHp = 0;
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0007710C File Offset: 0x0007530C
	private void Update()
	{
		bool flag = this._showUi == null || this._NpcEntity == null || this._NpcEntity.thisModel == null || this._NpcEntity.haveDestroy || !this._NpcEntity.mModelTopObjbool;
		if (flag)
		{
			bool activeSelf = this._showUi.activeSelf;
			if (activeSelf)
			{
				this._showUi.SetActive(false);
			}
		}
		else
		{
			bool isDead = this._NpcEntity.isDead;
			if (isDead)
			{
				bool activeSelf2 = this.bufflist.activeSelf;
				if (activeSelf2)
				{
					this.bufflist.SetActive(false);
				}
				bool activeSelf3 = this.ui_jiantou.activeSelf;
				if (activeSelf3)
				{
					this.ui_jiantou.SetActive(false);
				}
				bool activeSelf4 = this._hpParent.activeSelf;
				if (activeSelf4)
				{
					this._hpParent.SetActive(false);
				}
			}
			else
			{
				bool flag2 = !this._showUi.activeSelf;
				if (flag2)
				{
					this._showUi.SetActive(true);
				}
				Vector3 position = base.transform.position;
				Vector3 position2 = CameraControl.Instance.controlCamera.transform.position;
				float num = Vector3.Distance(position, position2);
				num = (float)((int)(num * 100f)) / 100f;
				this.CheckHeadPos();
				this.CheckHp();
				this.CheckRotaion(position, position2);
				this.CheckHeadActive(num);
				this.CheckSize(num);
			}
		}
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00077288 File Offset: 0x00075488
	private void CheckHeadPos()
	{
		Vector3 zero = Vector3.zero;
		zero.y = (float)this._NpcEntity.SignHeight / 1000f;
		this._NpcEntity.UserHeadTarget.transform.localPosition = zero;
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x000772CC File Offset: 0x000754CC
	private void CheckHp()
	{
		bool flag = !(this.hpcon == null) && this._NpcEntity != null;
		if (flag)
		{
			int nowHp = this._NpcEntity.playerCommon.GetNowHp();
			bool flag2 = nowHp != this.nOldHp;
			if (flag2)
			{
				int maxHp = this._NpcEntity.playerCommon.GetMaxHp();
				this.hpcon.SetHp(maxHp, this._NpcEntity.playerCommon.m_nHp, this._NpcEntity.playerCommon.m_nArmor, this._NpcEntity.playerCommon.m_nShield, 1);
				this.nOldHp = nowHp;
			}
		}
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00077374 File Offset: 0x00075574
	private void CheckRotaion(Vector3 bAt, Vector3 bTo)
	{
		Vector3 forward = bTo - bAt;
		Quaternion rotation = Quaternion.LookRotation(forward);
		base.transform.rotation = rotation;
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0007739E File Offset: 0x0007559E
	private void CheckHeadActive(float fDis)
	{
		this.CheckLookEnemyHpUi();
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000773A8 File Offset: 0x000755A8
	private void CheckLookEnemyHpUi()
	{
		bool flag = MatchManager.GetInstance().m_pFbData.nType == 10;
		if (flag)
		{
			bool activeSelf = this._hpParent.activeSelf;
			if (activeSelf)
			{
				this._hpParent.SetActive(false);
			}
		}
		else
		{
			DateTime livingDamageDate = this._NpcEntity.livingDamageDate;
			DateTime now = Fun.Now;
			int num = (int)(now - livingDamageDate).TotalSeconds;
			bool flag2 = num >= 0 && num <= this.lookEnemyHpTime;
			if (flag2)
			{
				bool flag3 = !this._hpParent.activeSelf;
				if (flag3)
				{
					this._hpParent.SetActive(true);
				}
			}
			else
			{
				bool activeSelf2 = this._hpParent.activeSelf;
				if (activeSelf2)
				{
					this._hpParent.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00077478 File Offset: 0x00075678
	private void CheckSize(float fDis)
	{
		bool flag = fDis > 40f;
		if (flag)
		{
			fDis = 40f;
		}
		bool cameraZoom = CameraControl.Instance.CameraZoom;
		if (cameraZoom)
		{
			fDis = CameraControl.Instance.controlCamera.fieldOfView * fDis / SysConfig.cameraNormalFOV;
		}
		float num = fDis * 0.12f;
		bool flag2 = num < 1f;
		if (flag2)
		{
			num = 1f;
		}
		bool flag3 = this.fOldScale != num;
		if (flag3)
		{
			this.showUiTransform.localScale = new Vector3(num, num, 1f);
			this.fOldScale = num;
		}
	}

	// Token: 0x04000A65 RID: 2661
	private GameObject _showUi;

	// Token: 0x04000A66 RID: 2662
	private Transform showUiTransform;

	// Token: 0x04000A67 RID: 2663
	private GameObject topNameObj;

	// Token: 0x04000A68 RID: 2664
	private MeshRenderer lzname;

	// Token: 0x04000A69 RID: 2665
	private GameObject hpObj;

	// Token: 0x04000A6A RID: 2666
	private HpNewControl hpcon;

	// Token: 0x04000A6B RID: 2667
	private GameObject _hpParent;

	// Token: 0x04000A6C RID: 2668
	private GameObject ui_jiantou;

	// Token: 0x04000A6D RID: 2669
	private GameObject clonebuff;

	// Token: 0x04000A6E RID: 2670
	private GameObject bufflist;

	// Token: 0x04000A6F RID: 2671
	private HeroPlayer heroplayer;

	// Token: 0x04000A70 RID: 2672
	private Npc_Entity _NpcEntity;

	// Token: 0x04000A71 RID: 2673
	private int lookEnemyHpTime = 5;

	// Token: 0x04000A72 RID: 2674
	private int nOldHp;

	// Token: 0x04000A73 RID: 2675
	private float fOldScale;
}
