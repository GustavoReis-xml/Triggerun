using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200025F RID: 607
public class Npc_AI_Controller : MonoBehaviour
{
	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0010548C File Offset: 0x0010368C
	// (set) Token: 0x06001B9F RID: 7071 RVA: 0x001054EA File Offset: 0x001036EA
	public Npc_AI_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Npc_AI_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Npc_AI_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x001054F4 File Offset: 0x001036F4
	// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x00105552 File Offset: 0x00103752
	public Npc_AI_View View_Component
	{
		get
		{
			bool flag = this._View_Component == null;
			if (flag)
			{
				this._View_Component = base.gameObject.GetComponent<Npc_AI_View>();
				bool flag2 = this._View_Component == null;
				if (flag2)
				{
					this._View_Component = base.gameObject.AddComponent<Npc_AI_View>();
				}
			}
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0010555C File Offset: 0x0010375C
	// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x001055BA File Offset: 0x001037BA
	public Npc_AI_Model Model_Component
	{
		get
		{
			bool flag = this._Model_Component == null;
			if (flag)
			{
				this._Model_Component = base.gameObject.GetComponent<Npc_AI_Model>();
				bool flag2 = this._Model_Component == null;
				if (flag2)
				{
					this._Model_Component = base.gameObject.AddComponent<Npc_AI_Model>();
				}
			}
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x001055C4 File Offset: 0x001037C4
	// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x001055DC File Offset: 0x001037DC
	public Npc_Entity Npc_Entity_Component
	{
		get
		{
			return this._Npc_Entity_Component;
		}
		set
		{
			this._Npc_Entity_Component = value;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x001055E8 File Offset: 0x001037E8
	// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x001056B7 File Offset: 0x001038B7
	public NavMeshAgent NavMeshAgent_Component
	{
		get
		{
			bool flag = this._NavMeshAgent_Component == null;
			if (flag)
			{
				this._NavMeshAgent_Component = this.Npc_Entity_Component.UserControl.GetComponent<NavMeshAgent>();
				bool flag2 = this._NavMeshAgent_Component == null;
				if (flag2)
				{
					this._NavMeshAgent_Component = this.Npc_Entity_Component.UserControl.AddComponent<NavMeshAgent>();
				}
				this._NavMeshAgent_Component.angularSpeed = 5000f;
				this._NavMeshAgent_Component.acceleration = 5000f;
				this._NavMeshAgent_Component.baseOffset = -0.1f;
				bool flag3 = this.Model_Component.Node_Data.Count > 0;
				if (flag3)
				{
					this.Arrive_Destination();
				}
				else
				{
					this._NavMeshAgent_Component.enabled = false;
				}
			}
			return this._NavMeshAgent_Component;
		}
		set
		{
			this._NavMeshAgent_Component = value;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x001056C4 File Offset: 0x001038C4
	// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0010572B File Offset: 0x0010392B
	public Animator Animator_Component
	{
		get
		{
			bool flag = this._Animator_Component == null;
			if (flag)
			{
				bool flag2 = this.Npc_Entity_Component.thisModel == null;
				if (flag2)
				{
					return null;
				}
				this._Animator_Component = this.Npc_Entity_Component.thisModel.GetComponent<Animator>();
				this._Animator_Component.applyRootMotion = true;
			}
			return this._Animator_Component;
		}
		set
		{
			this._Animator_Component = value;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00105738 File Offset: 0x00103938
	// (set) Token: 0x06001BAB RID: 7083 RVA: 0x00105750 File Offset: 0x00103950
	public string Current_Animator_State
	{
		get
		{
			return this._Current_Animator_State;
		}
		set
		{
			this._Current_Animator_State = value;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0010575C File Offset: 0x0010395C
	// (set) Token: 0x06001BAD RID: 7085 RVA: 0x00105774 File Offset: 0x00103974
	public bool Arrive_State
	{
		get
		{
			return this._Arrive_State;
		}
		set
		{
			this._Arrive_State = value;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00105780 File Offset: 0x00103980
	// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00105798 File Offset: 0x00103998
	public float Speed
	{
		get
		{
			return this._Speed;
		}
		set
		{
			this._Speed = value;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x001057A4 File Offset: 0x001039A4
	// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x001057BC File Offset: 0x001039BC
	public int Order_Value
	{
		get
		{
			return this._Order_Value;
		}
		set
		{
			this._Order_Value = value;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x001057C8 File Offset: 0x001039C8
	// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x001057E0 File Offset: 0x001039E0
	public bool Restore_State
	{
		get
		{
			return this._Restore_State;
		}
		set
		{
			this._Restore_State = value;
		}
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x001057EA File Offset: 0x001039EA
	public void Initialize(int _Index)
	{
		this.Model_Component.Initialize(_Index);
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x001057FC File Offset: 0x001039FC
	private void Update()
	{
		bool flag = fGameManager.Heroplayer == null || fGameManager.Heroplayer.delayNet;
		if (!flag)
		{
			this.Switch_Animation(this.Current_Animator_State);
			bool flag2 = !fGameManager.Npc_Action_State || !this.Npc_Entity_Component.Action_State;
			if (flag2)
			{
				this.NavMeshAgent_Component.enabled = false;
			}
			else
			{
				bool flag3 = this.Npc_Entity_Component.UserControl == null;
				if (!flag3)
				{
					bool death_State = this.Npc_Entity_Component.Death_State;
					if (death_State)
					{
						this.NavMeshAgent_Component.enabled = false;
					}
					else
					{
						bool flag4 = this.Arrive_State || this.NavMeshAgent_Component == null;
						if (!flag4)
						{
							bool flag5 = this.Npc_Entity_Component.playerCommon.m_nDongjie == 1;
							if (flag5)
							{
								this.NavMeshAgent_Component.enabled = false;
							}
							else
							{
								bool flag6 = this.Model_Component.Entity_Component.Move_Type == 0;
								if (flag6)
								{
									bool flag7 = this.Restore_State && ((this.NavMeshAgent_Component.pathStatus == NavMeshPathStatus.PathComplete && this.NavMeshAgent_Component.remainingDistance <= 0f) || Vector3.Distance(this.Npc_Entity_Component.UserControl.transform.localPosition, this.Model_Component.Entity_Component.Initial_Position) <= 0.01f);
									if (flag7)
									{
										this.Restore_Rotation();
									}
								}
								else
								{
									bool flag8 = (this.NavMeshAgent_Component.pathStatus == NavMeshPathStatus.PathComplete && this.NavMeshAgent_Component.remainingDistance <= 0f) || Vector3.Distance(this.Npc_Entity_Component.UserControl.transform.localPosition, this.Npc_Entity_Component.Position) <= 0.01f;
									if (flag8)
									{
										this.Arrive_Destination();
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x001059E4 File Offset: 0x00103BE4
	public void Arrive_Destination()
	{
		this.Current_Animator_State = "Stand";
		this.Arrive_State = true;
		this.NavMeshAgent_Component.enabled = false;
		float current_Node_Wait_Time = this.Model_Component.Get_Current_Node_Wait_Time();
		base.StartCoroutine(this.Wait_Move(current_Node_Wait_Time));
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00105A33 File Offset: 0x00103C33
	private IEnumerator Wait_Move(float _Time)
	{
		yield return new WaitForSeconds(_Time);
		bool flag = this.Model_Component.Current_Node_Number == 1;
		if (flag)
		{
			this.Order_Value = 1;
		}
		else
		{
			bool flag2 = this.Model_Component.Current_Node_Number >= this.Model_Component.Node_Data.Count;
			if (flag2)
			{
				this.Order_Value = -1;
			}
		}
		this.Model_Component.Modify_Current_Node_Number(this.Model_Component.Current_Node_Number + this.Order_Value);
		bool flag3 = this.Order_Value > 0;
		if (flag3)
		{
			this.Npc_Entity_Component.Position = this.Model_Component.Get_Next_Node_Position();
		}
		else
		{
			this.Npc_Entity_Component.Position = this.Model_Component.Get_Previous_Node_Position();
		}
		this.Arrive_State = false;
		this.Current_Animator_State = "Move";
		this.NavMeshAgent_Component.enabled = true;
		this.NavMeshAgent_Component.SetDestination(this.Npc_Entity_Component.Position);
		yield break;
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00105A4C File Offset: 0x00103C4C
	public void Switch_Animation(string _State)
	{
		bool flag = !(this.Animator_Component == null);
		if (flag)
		{
			if (!(_State == "Stand"))
			{
				if (!(_State == "Move"))
				{
					if (!(_State == "Death"))
					{
						if (_State == "Attack")
						{
							this.Attack();
						}
					}
					else
					{
						this.Death();
					}
				}
				else
				{
					this.Move();
				}
			}
			else
			{
				this.Stand();
			}
		}
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00105AD0 File Offset: 0x00103CD0
	private void Stand()
	{
		bool flag = this.Animator_Component.gameObject.transform.parent.gameObject.activeSelf && this.Animator_Component.gameObject.activeSelf && this.Animator_Component.enabled;
		if (flag)
		{
			this.Animator_Component.SetFloat("Move_State", 0f);
		}
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00105B3C File Offset: 0x00103D3C
	private void Move()
	{
		bool flag = this.Animator_Component.gameObject.transform.parent.gameObject.activeSelf && this.Animator_Component.gameObject.activeSelf && this.Animator_Component.enabled;
		if (flag)
		{
			this.Animator_Component.SetFloat("Move_State", 1f);
		}
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00105BA8 File Offset: 0x00103DA8
	private void Death()
	{
		bool flag = this.Animator_Component.gameObject.transform.parent.gameObject.activeSelf && this.Animator_Component.gameObject.activeSelf && this.Animator_Component.enabled;
		if (flag)
		{
			this.Animator_Component.SetBool("Death_State", true);
		}
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x00105C10 File Offset: 0x00103E10
	private void Attack()
	{
		bool flag = this.Animator_Component.gameObject.transform.parent.gameObject.activeSelf && this.Animator_Component.gameObject.activeSelf && this.Animator_Component.enabled;
		if (flag)
		{
			this.Animator_Component.SetBool("Attack", true);
		}
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x00105C78 File Offset: 0x00103E78
	public void Attack_Stop()
	{
		bool flag = this.Animator_Component.gameObject.transform.parent.gameObject.activeSelf && this.Animator_Component.gameObject.activeSelf && this.Animator_Component.enabled;
		if (flag)
		{
			this.Animator_Component.SetBool("Attack", false);
		}
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x00105CDF File Offset: 0x00103EDF
	public void Start_All_Action()
	{
		this.NavMeshAgent_Component.enabled = true;
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00105CEF File Offset: 0x00103EEF
	public void Stop_All_Action()
	{
		this.NavMeshAgent_Component.enabled = false;
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x00105CFF File Offset: 0x00103EFF
	public void Restore_Position()
	{
		this.NavMeshAgent_Component.enabled = false;
		base.StartCoroutine(this.Wait_Restore_Position());
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00105D1C File Offset: 0x00103F1C
	private IEnumerator Wait_Restore_Position()
	{
		bool flag = this.Npc_Entity_Component.playerCommon.m_nDongjie == 1;
		if (flag)
		{
			yield return new WaitForSeconds(0.2f);
			base.StartCoroutine(this.Wait_Restore_Position());
			yield break;
		}
		yield return new WaitForSeconds(3f);
		this.NavMeshAgent_Component.enabled = true;
		this.Restore_State = true;
		this.Current_Animator_State = "Move";
		this.NavMeshAgent_Component.SetDestination(this.Model_Component.Entity_Component.Initial_Position);
		yield break;
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x00105D2C File Offset: 0x00103F2C
	private void Restore_Rotation()
	{
		this.Restore_State = false;
		this.Current_Animator_State = "Stand";
		this.Npc_Entity_Component.UserControl.transform.localEulerAngles = this.Model_Component.Entity_Component.Initial_Rotation;
		this._NavMeshAgent_Component.enabled = false;
	}

	// Token: 0x040018FF RID: 6399
	private Npc_AI_UI _UI_Component;

	// Token: 0x04001900 RID: 6400
	private Npc_AI_View _View_Component;

	// Token: 0x04001901 RID: 6401
	private Npc_AI_Model _Model_Component;

	// Token: 0x04001902 RID: 6402
	private Npc_Entity _Npc_Entity_Component;

	// Token: 0x04001903 RID: 6403
	private NavMeshAgent _NavMeshAgent_Component;

	// Token: 0x04001904 RID: 6404
	private Animator _Animator_Component;

	// Token: 0x04001905 RID: 6405
	private string _Current_Animator_State = "Stand";

	// Token: 0x04001906 RID: 6406
	private bool _Arrive_State;

	// Token: 0x04001907 RID: 6407
	private Vector3 _Velocity;

	// Token: 0x04001908 RID: 6408
	private float _Speed = 1f;

	// Token: 0x04001909 RID: 6409
	private int _Order_Value = 1;

	// Token: 0x0400190A RID: 6410
	private bool _Restore_State;
}
