using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class PlayerRuntimeData : MonoBehaviour
{
	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0011A520 File Offset: 0x00118720
	// (set) Token: 0x06001E39 RID: 7737 RVA: 0x0011A538 File Offset: 0x00118738
	public bool SkillMove
	{
		get
		{
			return this._SkillMove;
		}
		set
		{
			this._SkillMove = value;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0011A544 File Offset: 0x00118744
	// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0011A55C File Offset: 0x0011875C
	public Vector2 CameraMove
	{
		get
		{
			return this._CameraMove;
		}
		set
		{
			this._CameraMove = value;
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0011A568 File Offset: 0x00118768
	// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0011A580 File Offset: 0x00118780
	public bool PlayerFly
	{
		get
		{
			return this._PlayerFly;
		}
		set
		{
			this._PlayerFly = value;
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0011A58C File Offset: 0x0011878C
	// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0011A5A4 File Offset: 0x001187A4
	public bool PlayerPopUp
	{
		get
		{
			return this._PlayerPopUp;
		}
		set
		{
			this._PlayerPopUp = value;
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0011A5B0 File Offset: 0x001187B0
	// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0011A5C8 File Offset: 0x001187C8
	public bool CameraZoom
	{
		get
		{
			return this._CameraZoom;
		}
		set
		{
			this._CameraZoom = value;
			bool cameraZoom = this._CameraZoom;
			if (cameraZoom)
			{
				this.MoveState = 1;
			}
			else
			{
				this.MoveState = 0;
			}
		}
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0011A600 File Offset: 0x00118800
	// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0011A618 File Offset: 0x00118818
	public bool Deformation_State
	{
		get
		{
			return this._Deformation_State;
		}
		set
		{
			this._Deformation_State = value;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0011A624 File Offset: 0x00118824
	// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0011A63C File Offset: 0x0011883C
	public int MoveState
	{
		get
		{
			return this._MoveState;
		}
		set
		{
			this._MoveState = value;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0011A648 File Offset: 0x00118848
	// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0011A660 File Offset: 0x00118860
	public bool HaveDun
	{
		get
		{
			return this._HaveDun;
		}
		set
		{
			this._HaveDun = value;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0011A66C File Offset: 0x0011886C
	// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0011A684 File Offset: 0x00118884
	public bool Chongci
	{
		get
		{
			return this._Chongci;
		}
		set
		{
			this._Chongci = value;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0011A690 File Offset: 0x00118890
	// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0011A6A8 File Offset: 0x001188A8
	public Vector3 V3FPSModelPos
	{
		get
		{
			return this._V3FPSModelPos;
		}
		set
		{
			this._V3FPSModelPos = value;
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0011A6B4 File Offset: 0x001188B4
	// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0011A6CC File Offset: 0x001188CC
	public bool Daodi
	{
		get
		{
			return this._Daodi;
		}
		set
		{
			this._Daodi = value;
			if (value)
			{
				base.StartCoroutine(this.DaodiCameraPos());
			}
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0011A6F8 File Offset: 0x001188F8
	// (set) Token: 0x06001E4F RID: 7759 RVA: 0x0011A710 File Offset: 0x00118910
	public bool ToShaowei
	{
		get
		{
			return this._ToShaowei;
		}
		set
		{
			this._ToShaowei = value;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001E50 RID: 7760 RVA: 0x0011A71C File Offset: 0x0011891C
	// (set) Token: 0x06001E51 RID: 7761 RVA: 0x0011A734 File Offset: 0x00118934
	public bool NoInputMove
	{
		get
		{
			return this._NoInputMove;
		}
		set
		{
			this._NoInputMove = value;
		}
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x0011A73E File Offset: 0x0011893E
	private IEnumerator SetMoveState(int iState)
	{
		yield return new WaitForSeconds(0.2f);
		this.MoveState = iState;
		yield break;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x0011A754 File Offset: 0x00118954
	public void initData()
	{
		this.bSuperJump = false;
		this.SkillMove = false;
		this.CameraMove = Vector2.one;
		this.PlayerFly = false;
		this.PlayerPopUp = false;
		this.CameraZoom = false;
		this.HaveDun = false;
		this.Chongci = false;
		this.Daodi = false;
		this.MoveState = 0;
		this.PopUpDir = Vector3.zero;
		this.PopUpAddspeed = 0f;
		this.PopUpTime = 0f;
		this.SetPlayerData();
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x0011A7DD File Offset: 0x001189DD
	private IEnumerator DaodiCameraPos()
	{
		yield return new WaitForSeconds(2f);
		yield return new WaitForSeconds(0.5f);
		this._Daodi = false;
		yield break;
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x0011A7EC File Offset: 0x001189EC
	public void SetPlayerPopUpData(Vector3 v3Dir, float fForce)
	{
		this.PlayerPopUp = true;
		this.PopUpDir = v3Dir.normalized;
		this.PopUpDir.y = 0f;
		this.PopUpAddspeed = fForce;
		this.PopUpTime = Time.time;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x0011A828 File Offset: 0x00118A28
	public Vector3 GetPopUpMovement()
	{
		Vector3 a = this.PopUpDir * Time.deltaTime * 5f;
		Vector3 b = Vector3.up * this.PopUpAddspeed * Time.deltaTime;
		Vector3 result = a + b;
		this.PopUpAddspeed -= 20f * Time.deltaTime;
		return result;
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x0011A891 File Offset: 0x00118A91
	public void SetHeroPlayer(HeroPlayer heroplayer)
	{
		this.hero = heroplayer;
		this.SetPlayerData();
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x0011A8A2 File Offset: 0x00118AA2
	public void SetPlayerData()
	{
	}

	// Token: 0x04001BD9 RID: 7129
	public HeroPlayer hero;

	// Token: 0x04001BDA RID: 7130
	public bool bSuperJump;

	// Token: 0x04001BDB RID: 7131
	public float baseHeight = 1.2f;

	// Token: 0x04001BDC RID: 7132
	public float extraHeight;

	// Token: 0x04001BDD RID: 7133
	private bool _SkillMove;

	// Token: 0x04001BDE RID: 7134
	private Vector2 _CameraMove = Vector2.one;

	// Token: 0x04001BDF RID: 7135
	private bool _PlayerFly;

	// Token: 0x04001BE0 RID: 7136
	private bool _PlayerPopUp;

	// Token: 0x04001BE1 RID: 7137
	private bool _CameraZoom;

	// Token: 0x04001BE2 RID: 7138
	private bool _HaveDun;

	// Token: 0x04001BE3 RID: 7139
	private bool _Chongci;

	// Token: 0x04001BE4 RID: 7140
	private bool _Daodi;

	// Token: 0x04001BE5 RID: 7141
	private bool _ToShaowei;

	// Token: 0x04001BE6 RID: 7142
	private bool _NoInputMove;

	// Token: 0x04001BE7 RID: 7143
	private int _MoveState;

	// Token: 0x04001BE8 RID: 7144
	private Vector3 _V3FPSModelPos = Vector3.zero;

	// Token: 0x04001BE9 RID: 7145
	public Vector3 PopUpDir = Vector3.zero;

	// Token: 0x04001BEA RID: 7146
	public float PopUpAddspeed;

	// Token: 0x04001BEB RID: 7147
	public float PopUpTime;

	// Token: 0x04001BEC RID: 7148
	private bool _Deformation_State;
}
