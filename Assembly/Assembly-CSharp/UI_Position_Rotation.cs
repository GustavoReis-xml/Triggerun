using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200048A RID: 1162
public class UI_Position_Rotation : MonoBehaviour
{
	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0018B198 File Offset: 0x00189398
	// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x0018B1B0 File Offset: 0x001893B0
	public bool Fight_State
	{
		get
		{
			return this._Fight_State;
		}
		set
		{
			this._Fight_State = value;
		}
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x0018B1BC File Offset: 0x001893BC
	// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x0018B1D4 File Offset: 0x001893D4
	public GameObject UI_Object
	{
		get
		{
			return this._UI_Object;
		}
		set
		{
			this._UI_Object = value;
		}
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x0018B1E0 File Offset: 0x001893E0
	// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x0018B21F File Offset: 0x0018941F
	public GameObject Target_Object
	{
		get
		{
			bool flag = this._Target_Object == null;
			if (flag)
			{
				this._Target_Object = fGameManager.GetInstance().getPlayer().thisModel;
			}
			return this._Target_Object;
		}
		set
		{
			this._Target_Object = value;
		}
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x0018B22C File Offset: 0x0018942C
	// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x0018B267 File Offset: 0x00189467
	public Image Image_Component
	{
		get
		{
			bool flag = this._Image_Component == null;
			if (flag)
			{
				this._Image_Component = this.UI_Object.GetComponent<Image>();
			}
			return this._Image_Component;
		}
		set
		{
			this._Image_Component = value;
		}
	}

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0018B274 File Offset: 0x00189474
	// (set) Token: 0x06002CEA RID: 11498 RVA: 0x0018B2AF File Offset: 0x001894AF
	public RectTransform Image_RectTransform
	{
		get
		{
			bool flag = this._Image_RectTransform == null;
			if (flag)
			{
				this._Image_RectTransform = this.UI_Object.GetComponent<RectTransform>();
			}
			return this._Image_RectTransform;
		}
		set
		{
			this._Image_RectTransform = value;
		}
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x0018B2B9 File Offset: 0x001894B9
	private void Start()
	{
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x0018B2BC File Offset: 0x001894BC
	private void FixedUpdate()
	{
		bool flag = this.UI_Object == null;
		if (!flag)
		{
			bool flag2 = !this.Fight_State;
			if (flag2)
			{
				this.Update_State(false);
			}
			else
			{
				bool flag3 = !(this.Target_Object == null) && !(this.Image_RectTransform == null);
				if (flag3)
				{
					bool flag4 = Vector3.Distance(this.Target_Object.transform.position, base.gameObject.transform.position) < 10f;
					if (flag4)
					{
						this.Update_State(false);
					}
					else
					{
						this.Update_State(true);
					}
					this._Direction = this.Target_Object.transform.position - base.gameObject.transform.position;
					this._Direction.y = 0f;
					this._Direction = this._Direction.normalized;
					this._Dot = Vector3.Dot(this._Direction, this.Target_Object.transform.forward);
					this._Cross = Vector3.Cross(this._Direction, this.Target_Object.transform.forward);
					bool flag5 = this._Dot > 1f;
					if (flag5)
					{
						this._Dot = 1f;
					}
					bool flag6 = this._Dot < -1f;
					if (flag6)
					{
						this._Dot = -1f;
					}
					this._Dot = Mathf.Acos(this._Dot) * 57.29578f;
					this.Image_RectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, this._Dot * (float)((this._Cross.y > 0f) ? 1 : -1)));
					bool flag7 = !this.UI_Object.activeSelf;
					if (flag7)
					{
						this.UI_Object.SetActive(true);
					}
				}
			}
		}
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x0018B4B4 File Offset: 0x001896B4
	public void Update_State(bool _State)
	{
		bool flag = this.Image_Component.enabled != _State;
		if (flag)
		{
			this.Image_Component.enabled = _State;
		}
	}

	// Token: 0x06002CEE RID: 11502 RVA: 0x0018B4E8 File Offset: 0x001896E8
	public void Update_Color(int _Value)
	{
		Color color = Color.white;
		color = ((_Value == 0) ? Color.white : ((PlayerDataManager.getInstance().playercom.m_nShili != _Value) ? new Color(255f, 0f, 0f, 255f) : new Color(0f, 200f, 255f, 255f)));
		this.Image_Component.color = color;
	}

	// Token: 0x04002614 RID: 9748
	private bool _Fight_State;

	// Token: 0x04002615 RID: 9749
	private GameObject _UI_Object;

	// Token: 0x04002616 RID: 9750
	private GameObject _Target_Object;

	// Token: 0x04002617 RID: 9751
	private Image _Image_Component;

	// Token: 0x04002618 RID: 9752
	private RectTransform _Image_RectTransform;

	// Token: 0x04002619 RID: 9753
	private Vector3 _Direction = Vector3.zero;

	// Token: 0x0400261A RID: 9754
	private float _Dot;

	// Token: 0x0400261B RID: 9755
	private Vector3 _Cross = Vector3.zero;
}
