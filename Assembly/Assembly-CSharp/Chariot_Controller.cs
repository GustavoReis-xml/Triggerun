using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.Track;

// Token: 0x0200007A RID: 122
public class Chariot_Controller : Npc_Controller
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00020184 File Offset: 0x0001E384
	// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0002019C File Offset: 0x0001E39C
	public int Player_Amount
	{
		get
		{
			return this._Player_Amount;
		}
		set
		{
			this._Player_Amount = value;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000201A8 File Offset: 0x0001E3A8
	// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000201C0 File Offset: 0x0001E3C0
	public bool Wait_State
	{
		get
		{
			return this._Wait_State;
		}
		set
		{
			this._Wait_State = value;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000201CC File Offset: 0x0001E3CC
	// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000201E4 File Offset: 0x0001E3E4
	public bool Phase_Wait_State
	{
		get
		{
			return this._Phase_Wait_State;
		}
		set
		{
			this._Phase_Wait_State = value;
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000201F0 File Offset: 0x0001E3F0
	// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00020208 File Offset: 0x0001E408
	public bool Over_State
	{
		get
		{
			return this._Over_State;
		}
		set
		{
			this._Over_State = value;
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060003FA RID: 1018 RVA: 0x00020214 File Offset: 0x0001E414
	public float Move_Speed
	{
		get
		{
			switch (this.Player_Amount)
			{
			case 0:
				this._Move_Speed = GlobalDefine.Chariot_Back_Move_Speed;
				break;
			case 1:
				this._Move_Speed = GlobalDefine.Chariot_Forward_Move_Speed[1];
				break;
			case 2:
				this._Move_Speed = GlobalDefine.Chariot_Forward_Move_Speed[2];
				break;
			default:
				this._Move_Speed = GlobalDefine.Chariot_Forward_Move_Speed[3];
				break;
			}
			return this._Move_Speed;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060003FB RID: 1019 RVA: 0x00020290 File Offset: 0x0001E490
	// (set) Token: 0x060003FC RID: 1020 RVA: 0x000202C8 File Offset: 0x0001E4C8
	public Chariot_Entity Chariot_Entity_Component
	{
		get
		{
			bool flag = this._Chariot_Entity_Component == null;
			if (flag)
			{
				this._Chariot_Entity_Component = (Chariot_Entity)base.Entity_Component;
			}
			return this._Chariot_Entity_Component;
		}
		set
		{
			this._Chariot_Entity_Component = value;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060003FD RID: 1021 RVA: 0x000202D4 File Offset: 0x0001E4D4
	// (set) Token: 0x060003FE RID: 1022 RVA: 0x0002030F File Offset: 0x0001E50F
	public Chariot_Model Chariot_Model_Component
	{
		get
		{
			bool flag = this._Chariot_Model_Component == null;
			if (flag)
			{
				this._Chariot_Model_Component = (Chariot_Model)base.Model_Component;
			}
			return this._Chariot_Model_Component;
		}
		set
		{
			this._Chariot_Model_Component = value;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060003FF RID: 1023 RVA: 0x0002031C File Offset: 0x0001E51C
	// (set) Token: 0x06000400 RID: 1024 RVA: 0x00020334 File Offset: 0x0001E534
	public TrackCart TrackCart_Component
	{
		get
		{
			return this._TrackCart_Component;
		}
		set
		{
			this._TrackCart_Component = value;
			this._TrackCart_Component.prorateForces = true;
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000401 RID: 1025 RVA: 0x0002034C File Offset: 0x0001E54C
	// (set) Token: 0x06000402 RID: 1026 RVA: 0x00020364 File Offset: 0x0001E564
	public int Contend_State
	{
		get
		{
			return this._Contend_State;
		}
		set
		{
			this._Contend_State = value;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000403 RID: 1027 RVA: 0x00020370 File Offset: 0x0001E570
	// (set) Token: 0x06000404 RID: 1028 RVA: 0x00020388 File Offset: 0x0001E588
	public int Global_Player_Amount
	{
		get
		{
			return this._Global_Player_Amount;
		}
		set
		{
			this._Global_Player_Amount = value;
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00020394 File Offset: 0x0001E594
	public override void Update()
	{
		bool flag = !base.Entity_Component.Main;
		if (flag)
		{
			bool flag2 = this.TrackCart_Component != null && this.TrackCart_Component.enabled;
			if (flag2)
			{
				this.TrackCart_Component.enabled = false;
			}
		}
		else
		{
			bool flag3 = this.TrackCart_Component != null && !this.TrackCart_Component.enabled;
			if (flag3)
			{
				bool flag4 = !this.TrackCart_Component.enabled;
				if (flag4)
				{
					((Chariot_View)base.View_Component).Find_Nearest_Track(this.TrackCart_Component);
				}
				this.TrackCart_Component.enabled = true;
			}
		}
		base.Update();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0002044B File Offset: 0x0001E64B
	public override void Auto_Detection()
	{
		this.Detection();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00020458 File Offset: 0x0001E658
	public override void Detection()
	{
		ArrayList arrayList = new ArrayList();
		bool flag = false;
		bool flag2 = false;
		bool over_State = this.Over_State;
		if (over_State)
		{
			this.Move_Stop();
			bool flag3 = ZhanDianCon.inst != null;
			if (flag3)
			{
				ZhanDianCon.inst.UpDateCarState(arrayList, 0, base.Entity_Component.Synchronization_Object);
			}
		}
		else
		{
			bool flag4 = this.TrackCart_Component == null || this.TrackCart_Component.CurrentTrack == null || !base.Entity_Component.Main;
			if (!flag4)
			{
				this.Display_Remaining_Distance();
				bool wait_State = this.Wait_State;
				if (!wait_State)
				{
					bool isKinematic = base.Rigidbody_Component.isKinematic;
					if (isKinematic)
					{
						base.Rigidbody_Component.isKinematic = false;
					}
					bool flag5 = base.Entity_Component.UserControl == null || base.Entity_Component.Detection_Controller_Component == null || base.Entity_Component.Death_State || base.Entity_Component.isCopy;
					if (!flag5)
					{
						Dictionary<int, GameObject> dictionary = base.Entity_Component.Detection_Controller_Component.Range_Detection(base.Entity_Component.Eye_Object.transform.position);
						this.Player_Amount = 0;
						arrayList = new ArrayList();
						List<int> list = new List<int>(dictionary.Keys);
						for (int i = 0; i < list.Count; i++)
						{
							int key = list[i];
							GameObject gameObject = dictionary[key];
							bool flag6 = gameObject == null;
							if (!flag6)
							{
								PublicInfo component = gameObject.transform.root.GetComponent<PublicInfo>();
								bool flag7 = component == null || component.type == "Npc";
								if (!flag7)
								{
									GamePlayer gamePlayer = gameObject.transform.root.GetComponent<PublicInfo>().NetObject as GamePlayer;
									bool flag8 = gamePlayer.nForce != base.Entity_Component.nForce;
									if (flag8)
									{
										float num = Vector3.Distance(gamePlayer.UserControl.transform.position, base.Entity_Component.UserControl.transform.position);
										float num2 = num - ((Chariot_Entity)base.Entity_Component).Detection_Range_2;
										bool flag9 = num2 > 0f;
										if (flag9)
										{
											goto IL_2BF;
										}
										flag = true;
									}
									else
									{
										bool flag10 = gamePlayer.nForce == base.Entity_Component.nForce;
										if (flag10)
										{
											flag2 = true;
											int player_Amount = this.Player_Amount;
											this.Player_Amount = player_Amount + 1;
										}
									}
									arrayList.Add(gamePlayer.getNetid());
								}
							}
							IL_2BF:;
						}
						bool flag11 = flag && flag2;
						int num3;
						int num4;
						if (flag11)
						{
							num3 = 0;
							num4 = 1;
						}
						else
						{
							bool flag12 = !flag && flag2;
							if (flag12)
							{
								num3 = 1;
								num4 = 0;
							}
							else
							{
								bool flag13 = flag && !flag2;
								if (flag13)
								{
									num3 = -1;
									num4 = 2;
								}
								else
								{
									num3 = -1;
									num4 = -1;
								}
							}
						}
						bool flag14 = ZhanDianCon.inst != null;
						if (flag14)
						{
							bool flag15 = this.Global_Player_Amount != this.Player_Amount;
							if (flag15)
							{
								this.Global_Player_Amount = this._Player_Amount;
								ZhanDianCon.inst.UpDateCarNum(this.Player_Amount, base.Entity_Component.Synchronization_Object);
								ZhanDianCon.inst.UpDateCarState(arrayList, num4, base.Entity_Component.Synchronization_Object);
							}
							bool flag16 = this.Contend_State != num4;
							if (flag16)
							{
								this.Contend_State = num4;
							}
						}
						bool flag17 = this.Player_Amount > 2;
						if (flag17)
						{
							this.Player_Amount = 3;
						}
						bool flag18 = this.Chariot_Entity_Component.Movement_State == num3;
						if (flag18)
						{
							bool flag19 = this.Chariot_Entity_Component.Movement_State == 1;
							if (flag19)
							{
								this.Set_Track_Acceleration_Or_Brakes_State(this.Move_Speed, this.Move_Speed, Track.SpeedAndForce.ActDirection.Forward);
							}
						}
						else
						{
							base.StartCoroutine(this.Modify_State(num3));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00020889 File Offset: 0x0001EA89
	private IEnumerator Modify_State(int _State)
	{
		base.StartCoroutine(this.Wait_Move_Stop());
		this.Chariot_Entity_Component.Movement_State = _State;
		if (_State != -1)
		{
			if (_State == 1)
			{
				yield return new WaitForSeconds(1.3f);
				bool flag = this.Chariot_Entity_Component.Movement_State == 1;
				if (flag)
				{
					base.StartCoroutine(this.Wait_Move_Forward());
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(GlobalDefine.Chariot_State_Change_Wait_Time);
			bool flag2 = this.Chariot_Entity_Component.Movement_State == -1;
			if (flag2)
			{
				base.StartCoroutine(this.Wait_Move_Backward());
			}
		}
		yield break;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0002089F File Offset: 0x0001EA9F
	private IEnumerator Wait_Move_Forward()
	{
		this.Wait_State = true;
		yield return new WaitForSeconds(0.1f);
		this.Wait_State = false;
		this.Move_Forward();
		yield break;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000208B0 File Offset: 0x0001EAB0
	private void Move_Forward()
	{
		bool wait_State = this.Wait_State;
		if (wait_State)
		{
			this.Move_Stop();
		}
		else
		{
			this.Set_Track_Acceleration_Or_Brakes_State(this.Move_Speed, this.Move_Speed, Track.SpeedAndForce.ActDirection.Forward);
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x000208E9 File Offset: 0x0001EAE9
	private IEnumerator Wait_Move_Backward()
	{
		this.Wait_State = true;
		yield return new WaitForSeconds(0.1f);
		this.Wait_State = false;
		this.Move_Backward();
		yield break;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x000208F8 File Offset: 0x0001EAF8
	private void Move_Backward()
	{
		bool wait_State = this.Wait_State;
		if (wait_State)
		{
			this.Move_Stop();
		}
		else
		{
			this.Set_Track_Acceleration_Or_Brakes_State(this.Move_Speed, this.Move_Speed, Track.SpeedAndForce.ActDirection.Backward);
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00020931 File Offset: 0x0001EB31
	private IEnumerator Wait_Move_Stop()
	{
		this.Wait_State = true;
		yield return new WaitForSeconds(0.1f);
		this.Wait_State = false;
		this.Move_Stop();
		yield break;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00020940 File Offset: 0x0001EB40
	private void Move_Stop()
	{
		this.Set_Track_Acceleration_Or_Brakes_State(0f, 0f, Track.SpeedAndForce.ActDirection.Both);
		Rigidbody component = base.Entity_Component.UserControl.GetComponent<Rigidbody>();
		component.velocity = Vector3.zero;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00020980 File Offset: 0x0001EB80
	private void Set_Track_Acceleration_Or_Brakes_State(float _Acceleration_Speed, float _Brakes_Speed, Track.SpeedAndForce.ActDirection _State)
	{
		List<Track> track_Object_List = this.Chariot_Model_Component.Track_Object_List;
		for (int i = 0; i < track_Object_List.Count; i++)
		{
			Track track = track_Object_List[i];
			Track.SpeedAndForce acceleration = track.acceleration;
			acceleration.direction = _State;
			Track.SpeedAndForce brakes = track.brakes;
			switch (_State)
			{
			case Track.SpeedAndForce.ActDirection.Both:
				acceleration.targetSpeed = 0f;
				brakes.targetSpeed = _Brakes_Speed;
				break;
			case Track.SpeedAndForce.ActDirection.Forward:
				acceleration.targetSpeed = _Acceleration_Speed;
				brakes.targetSpeed = _Brakes_Speed;
				break;
			case Track.SpeedAndForce.ActDirection.Backward:
				acceleration.targetSpeed = _Acceleration_Speed;
				brakes.targetSpeed = _Brakes_Speed;
				break;
			}
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00020A34 File Offset: 0x0001EC34
	public void Arrive_Destination(string _Type, int _Value)
	{
		bool isCopy = base.Entity_Component.isCopy;
		if (!isCopy)
		{
			bool flag = _Type != "Node";
			if (flag)
			{
				bool flag2 = _Type == "Pass_Node";
				if (flag2)
				{
					bool flag3 = ZhanDianCon.inst != null;
					if (flag3)
					{
						bool flag4 = ZhanDianCon.inst.Get_Special_Door_State_Data(_Value) != "Close";
						if (flag4)
						{
							Door_Control.Get_Instantiate.Special_Door_Handle(_Value, "open_close");
						}
						ZhanDianCon.inst.Set_Special_Door_State_Data(_Value, "Close");
					}
					bool flag5 = this.Chariot_Model_Component.Modify_Current_Pass_Node_Number(_Value);
					if (flag5)
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(this.Chariot_Model_Component.Current_Node_Number);
						arrayList.Add(1);
						fGameManager.GetInstance().getPlayer().SendMessageToServer(1030000, arrayList);
					}
				}
				this.TrackCart_Component.CurrentTrack.PrevTrack = null;
				this.TrackCart_Component.CurrentTrack.outOfTrack = Track.EndOfLineBehavior.EndStop;
			}
			else
			{
				bool flag6 = ZhanDianCon.inst != null;
				if (flag6)
				{
					bool flag7 = ZhanDianCon.inst.Get_Special_Door_State_Data(_Value) != "Open";
					if (flag7)
					{
						Door_Control.Get_Instantiate.Special_Door_Handle(_Value, "close_open");
					}
					ZhanDianCon.inst.Set_Special_Door_State_Data(_Value, "Open");
					ZhanDianCon.inst.UpDateCaPoint(_Value, 0, fGameManager.GetInstance().getPlayer());
				}
				bool flag8 = this.Chariot_Model_Component.Modify_Current_Node_Number(_Value);
				if (flag8)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(this.Chariot_Model_Component.Current_Node_Number);
					arrayList.Add(0);
					fGameManager.GetInstance().getPlayer().SendMessageToServer(1030000, arrayList);
				}
				bool flag9 = !base.Entity_Component.Main;
				if (!flag9)
				{
					this.Wait_State = true;
					this.Phase_Wait_State = true;
					this.Chariot_Entity_Component.Movement_State = 0;
					this.Move_Stop();
					bool flag10 = this.Chariot_Model_Component.Get_Next_Node_Data() == null;
					if (flag10)
					{
						this.Over_State = true;
					}
					else
					{
						bool flag11 = this.TrackCart_Component.CurrentTrack != null;
						if (flag11)
						{
							this.TrackCart_Component.CurrentTrack.PrevTrack = null;
							this.TrackCart_Component.CurrentTrack.outOfTrack = Track.EndOfLineBehavior.EndStop;
						}
						float current_Node_Wait_Time = this.Chariot_Model_Component.Get_Current_Node_Wait_Time();
						bool flag12 = current_Node_Wait_Time == 0f;
						if (flag12)
						{
							this.Wait_State = false;
						}
						else
						{
							base.StartCoroutine(this.Wait_Move(current_Node_Wait_Time, _Value));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00020CE3 File Offset: 0x0001EEE3
	private IEnumerator Wait_Move(float _Time, int _Value)
	{
		yield return new WaitForSeconds(_Time);
		this.Wait_State = false;
		this.Phase_Wait_State = false;
		yield break;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00020D00 File Offset: 0x0001EF00
	private float Compute_Pass_Distance()
	{
		bool flag = true;
		bool flag2 = base.Entity_Component.Synchronization_Object != fGameManager.GetInstance().getPlayer();
		float result;
		if (flag2)
		{
			result = 0f;
		}
		else
		{
			Track track = this.TrackCart_Component.CurrentTrack;
			float num = Vector3.Distance(base.Entity_Component.UserControl.transform.position, track.TrackAbsoluteStart.position);
			while (flag)
			{
				Track prevTrack = track.PrevTrack;
				bool flag3 = prevTrack == null;
				if (flag3)
				{
					break;
				}
				num += prevTrack.Distance;
				track = prevTrack;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00020DAC File Offset: 0x0001EFAC
	private float Compute_Remaining_Distance()
	{
		bool flag = true;
		bool flag2 = base.Entity_Component.Synchronization_Object != fGameManager.GetInstance().getPlayer();
		float result;
		if (flag2)
		{
			result = 0f;
		}
		else
		{
			Track track = this.TrackCart_Component.CurrentTrack;
			float num = Vector3.Distance(base.Entity_Component.UserControl.transform.position, track.TrackAbsoluteEnd.position);
			while (flag)
			{
				Track nextTrack = track.NextTrack;
				bool flag3 = nextTrack == null;
				if (flag3)
				{
					break;
				}
				num += nextTrack.Distance;
				track = nextTrack;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00020E58 File Offset: 0x0001F058
	private void Display_Remaining_Distance()
	{
		Vector3 b = Vector3.zero;
		float num = 0f;
		List<int> list = new List<int>(this.Chariot_Model_Component.Node_Data.Keys);
		int state = 0;
		int num2 = (int)(((Chariot_View)base.View_Component).Track_Total_Distance - this.Compute_Remaining_Distance());
		ArrayList next_Node_Data = this.Chariot_Model_Component.Get_Next_Node_Data();
		bool flag = next_Node_Data != null;
		if (flag)
		{
			b = (Vector3)next_Node_Data[0];
			num = Vector3.Distance(base.Entity_Component.UserControl.transform.position, b);
		}
		bool flag2 = ZhanDianCon.inst != null;
		if (flag2)
		{
			bool flag3 = num >= 20f || num <= 0f || this.Phase_Wait_State;
			if (flag3)
			{
				num = 0f;
			}
			bool flag4 = this.Chariot_Model_Component.Node_Data.ContainsKey(this.Chariot_Model_Component.Current_Node_Number + 1) && list[list.Count - 1] == this.Chariot_Model_Component.Current_Node_Number + 1;
			if (flag4)
			{
				state = 1;
			}
			ZhanDianCon.inst.UpDateCarDis(num2 + 1, (int)((Chariot_View)base.View_Component).Track_Total_Distance, num, base.Entity_Component.Synchronization_Object, state);
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00020FA0 File Offset: 0x0001F1A0
	public override void NCD()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.Entity_Component.ID);
		arrayList.Add(((Chariot_Model)base.Model_Component).Current_Node_Number);
		arrayList.Add(this.Get_Track_State());
		base.Entity_Component.Synchronization_Object.TosFun("NCD", new object[]
		{
			new ArrayList
			{
				arrayList
			}
		});
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00021024 File Offset: 0x0001F224
	private ArrayList Get_Track_State()
	{
		ArrayList arrayList = new ArrayList();
		List<Track> track_Object_List = this.Chariot_Model_Component.Track_Object_List;
		for (int i = 0; i < track_Object_List.Count; i++)
		{
			Track track = track_Object_List[i];
			bool flag = track.enabled && track.outOfTrack == Track.EndOfLineBehavior.EndStop;
			if (flag)
			{
				arrayList.Add(track.gameObject.name);
			}
		}
		return arrayList;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000210A0 File Offset: 0x0001F2A0
	public void Set_Track_State(ArrayList _Parameters)
	{
		List<Track> track_Object_List = this.Chariot_Model_Component.Track_Object_List;
		for (int i = 0; i < track_Object_List.Count; i++)
		{
			Track track = track_Object_List[i];
			bool flag = track.enabled && _Parameters.Contains(track.gameObject.name);
			if (flag)
			{
				track.PrevTrack = null;
				track.outOfTrack = Track.EndOfLineBehavior.EndStop;
			}
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00021112 File Offset: 0x0001F312
	public void Set_ZhanDian_Data()
	{
		base.StartCoroutine(this.Wait_Set_ZhanDian_Data());
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00021122 File Offset: 0x0001F322
	private IEnumerator Wait_Set_ZhanDian_Data()
	{
		bool flag = ZhanDianCon.inst != null;
		if (flag)
		{
			ZhanDianCon.inst.nCarid = base.Entity_Component.ID;
			ZhanDianCon.inst.Chariot = (Chariot_Entity)base.Entity_Component;
			bool flag2 = ZhanDianCon.inst.EffectObj1 == null;
			if (flag2)
			{
				ZhanDianCon.inst.EffectObj1 = base.Entity_Component.thisModel;
			}
			bool flag3 = ZhanDianCon.inst.EffectObj2 == null;
			if (flag3)
			{
				ZhanDianCon.inst.EffectObj2 = base.Entity_Component.thisModel;
			}
		}
		else
		{
			yield return new WaitForSeconds(0.1f);
			base.StartCoroutine(this.Wait_Set_ZhanDian_Data());
		}
		yield break;
	}

	// Token: 0x04000339 RID: 825
	private int _Player_Amount;

	// Token: 0x0400033A RID: 826
	private bool _Wait_State;

	// Token: 0x0400033B RID: 827
	private bool _Phase_Wait_State;

	// Token: 0x0400033C RID: 828
	private bool _Over_State;

	// Token: 0x0400033D RID: 829
	private float _Move_Speed;

	// Token: 0x0400033E RID: 830
	private Chariot_Entity _Chariot_Entity_Component;

	// Token: 0x0400033F RID: 831
	private Chariot_Model _Chariot_Model_Component;

	// Token: 0x04000340 RID: 832
	private TrackCart _TrackCart_Component;

	// Token: 0x04000341 RID: 833
	private int _Contend_State = -1;

	// Token: 0x04000342 RID: 834
	private int _Global_Player_Amount = -1;
}
