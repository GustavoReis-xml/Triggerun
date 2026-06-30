using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class Npc_Handle : MonoBehaviour
{
	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0010A68C File Offset: 0x0010888C
	// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0010A6A4 File Offset: 0x001088A4
	public NetLiving Hero_Player_Component
	{
		get
		{
			return this._Hero_Player_Component;
		}
		set
		{
			this._Hero_Player_Component = value;
		}
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x0010A6B0 File Offset: 0x001088B0
	public void Receive_Network_Message(int _Network_Message_ID, ArrayList _Parameters)
	{
		switch (_Network_Message_ID)
		{
		case 1020002:
			this.Npc_Synchronization_Validate_Succeed((int)_Parameters[0]);
			break;
		case 1020004:
			this.Npc_Synchronization_Validate_Request_Timeout((int)_Parameters[0]);
			break;
		}
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x0010A710 File Offset: 0x00108910
	public void Create_Npc(NetLiving mom, int _Target_ID, int _Index, Vector3 _Position, Vector3 _Rotation, Vector3 _Scale, int _Skill_ID, Vector3 _Force, string _colliderName)
	{
		ArrayList arrayList = null;
		int nForce = mom.nForce;
		arrayList = new ArrayList();
		arrayList.Add(mom.getNetid());
		arrayList.Add(_Index);
		arrayList.Add(nForce);
		try
		{
			arrayList.Add(Convert.ToInt32(_Position.x * 100f));
			arrayList.Add(Convert.ToInt32(_Position.y * 100f));
			arrayList.Add(Convert.ToInt32(_Position.z * 100f));
			arrayList.Add(Convert.ToInt32(_Rotation.x * 100f));
			arrayList.Add(Convert.ToInt32(_Rotation.y * 100f));
			arrayList.Add(Convert.ToInt32(_Rotation.z * 100f));
			arrayList.Add(Convert.ToInt32(_Scale.x * 100f));
			arrayList.Add(Convert.ToInt32(_Scale.y * 100f));
			arrayList.Add(Convert.ToInt32(_Scale.z * 100f));
			arrayList.Add(Convert.ToInt32(_Force.x * 100f));
			arrayList.Add(Convert.ToInt32(_Force.y * 100f));
			arrayList.Add(Convert.ToInt32(_Force.z * 100f));
		}
		catch (OverflowException ex)
		{
			Fun.LogError(string.Concat(new string[]
			{
				ex.Message,
				"_Target_ID:",
				_Target_ID.ToString(),
				" - _Index:",
				_Index.ToString(),
				" - _Position:",
				_Position.ToString(),
				" - _Rotation:",
				_Rotation.ToString(),
				" - _Scale:",
				_Scale.ToString(),
				" - _Force:",
				_Force.ToString()
			}), null);
			return;
		}
		arrayList.Add(_Skill_ID);
		arrayList.Add(_Target_ID);
		arrayList.Add(mom.netobject_type);
		arrayList.Add(_colliderName);
		mom.SendMsgToServer(1010001, arrayList);
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x0010A9D4 File Offset: 0x00108BD4
	public void Create_Npc(int _Target_ID, int _Index, int _Country, Vector3 _Position, Vector3 _Rotation, Vector3 _Scale, int _Skill_ID, Vector3 _Force)
	{
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		player.SendMessageToServer(1010001, new ArrayList
		{
			player.getNetid(),
			_Index,
			_Country,
			Convert.ToInt32(_Position.x * 100f),
			Convert.ToInt32(_Position.y * 100f),
			Convert.ToInt32(_Position.z * 100f),
			Convert.ToInt32(_Rotation.x * 100f),
			Convert.ToInt32(_Rotation.y * 100f),
			Convert.ToInt32(_Rotation.z * 100f),
			Convert.ToInt32(_Scale.x * 100f),
			Convert.ToInt32(_Scale.y * 100f),
			Convert.ToInt32(_Scale.z * 100f),
			Convert.ToInt32(_Force.x * 100f),
			Convert.ToInt32(_Force.y * 100f),
			Convert.ToInt32(_Force.z * 100f),
			_Skill_ID,
			_Target_ID,
			0,
			string.Empty
		});
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x0010ABCC File Offset: 0x00108DCC
	public void Npc_Synchronization_Validate(int _Npc_ID)
	{
		bool flag = !(this.Hero_Player_Component.thisModel == null);
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(_Npc_ID);
			arrayList.Add(0);
			this.Hero_Player_Component.SendMessageToServer(1020001, arrayList);
		}
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x0010AC28 File Offset: 0x00108E28
	public void Npc_Synchronization_Validate_Succeed(int _Npc_ID)
	{
		bool flag = !(this.Hero_Player_Component.thisModel == null);
		if (flag)
		{
			Npc_Entity npc_Entity = (Npc_Entity)GameRoot.SceneSocket.unFsorket.GetNetObj(_Npc_ID);
			bool flag2 = npc_Entity != null;
			if (flag2)
			{
				npc_Entity.Synchronization_Me((HeroPlayer)this.Hero_Player_Component);
				ArrayList arrayList = new ArrayList();
				arrayList.Add(_Npc_ID);
				arrayList.Add(0);
				this.Hero_Player_Component.SendMessageToServer(1020001, arrayList);
			}
		}
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x0010ACB8 File Offset: 0x00108EB8
	public void Npc_Synchronization_Validate_Request_Timeout(int _Npc_ID)
	{
		bool flag = !(this.Hero_Player_Component.thisModel == null);
		if (flag)
		{
			Npc_Entity npc_Entity = (Npc_Entity)GameRoot.SceneSocket.unFsorket.GetNetObj(_Npc_ID);
			bool flag2 = npc_Entity != null;
			if (flag2)
			{
				npc_Entity.Synchronization_Object = null;
			}
		}
	}

	// Token: 0x0400196E RID: 6510
	private NetLiving _Hero_Player_Component;
}
