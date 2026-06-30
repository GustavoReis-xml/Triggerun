using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class Npc_Entity : NetLiving
{
	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06001C29 RID: 7209 RVA: 0x001078DC File Offset: 0x00105ADC
	// (set) Token: 0x06001C2A RID: 7210 RVA: 0x001078F4 File Offset: 0x00105AF4
	public Component UI_Component
	{
		get
		{
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00107900 File Offset: 0x00105B00
	// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00107918 File Offset: 0x00105B18
	public Component View_Component
	{
		get
		{
			return this._View_Component;
		}
		set
		{
			this._View_Component = value;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00107924 File Offset: 0x00105B24
	// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0010793C File Offset: 0x00105B3C
	public Component Controller_Component
	{
		get
		{
			return this._Controller_Component;
		}
		set
		{
			this._Controller_Component = value;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00107948 File Offset: 0x00105B48
	// (set) Token: 0x06001C30 RID: 7216 RVA: 0x00107960 File Offset: 0x00105B60
	public Component Model_Component
	{
		get
		{
			return this._Model_Component;
		}
		set
		{
			this._Model_Component = value;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0010796C File Offset: 0x00105B6C
	// (set) Token: 0x06001C32 RID: 7218 RVA: 0x00107984 File Offset: 0x00105B84
	public GameObject Container
	{
		get
		{
			return this._Container;
		}
		set
		{
			this._Container = value;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00107990 File Offset: 0x00105B90
	// (set) Token: 0x06001C34 RID: 7220 RVA: 0x001079BE File Offset: 0x00105BBE
	public HeroPlayer Parent
	{
		get
		{
			bool flag = this._Parent == null;
			if (flag)
			{
				this._Parent = null;
			}
			return this._Parent;
		}
		set
		{
			this._Parent = value;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06001C35 RID: 7221 RVA: 0x001079C8 File Offset: 0x00105BC8
	// (set) Token: 0x06001C36 RID: 7222 RVA: 0x001079E0 File Offset: 0x00105BE0
	public HeroPlayer Synchronization_Object
	{
		get
		{
			return this._Synchronization_Object;
		}
		set
		{
			this._Synchronization_Object = value;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06001C37 RID: 7223 RVA: 0x001079EC File Offset: 0x00105BEC
	// (set) Token: 0x06001C38 RID: 7224 RVA: 0x00107A04 File Offset: 0x00105C04
	public NetLiving SynchronizationRB_Object
	{
		get
		{
			return this._SynchronizationRB_Object;
		}
		set
		{
			this._SynchronizationRB_Object = value;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00107A10 File Offset: 0x00105C10
	// (set) Token: 0x06001C3A RID: 7226 RVA: 0x00107A28 File Offset: 0x00105C28
	public Detection_Controller Detection_Controller_Component
	{
		get
		{
			return this._Detection_Controller_Component;
		}
		set
		{
			this._Detection_Controller_Component = value;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00107A34 File Offset: 0x00105C34
	// (set) Token: 0x06001C3C RID: 7228 RVA: 0x00107A4C File Offset: 0x00105C4C
	public Npc_Info Npc_Info_Component
	{
		get
		{
			return this._Npc_Info_Component;
		}
		set
		{
			this._Npc_Info_Component = value;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00107A58 File Offset: 0x00105C58
	// (set) Token: 0x06001C3E RID: 7230 RVA: 0x00107A93 File Offset: 0x00105C93
	public Rigidbody Rigidbody_Component
	{
		get
		{
			bool flag = this._Rigidbody_Component == null;
			if (flag)
			{
				this._Rigidbody_Component = this.UserControl.GetComponent<Rigidbody>();
			}
			return this._Rigidbody_Component;
		}
		set
		{
			this._Rigidbody_Component = value;
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00107AA0 File Offset: 0x00105CA0
	// (set) Token: 0x06001C40 RID: 7232 RVA: 0x00107AB8 File Offset: 0x00105CB8
	public int Master_ID
	{
		get
		{
			return this._Master_ID;
		}
		set
		{
			this._Master_ID = value;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06001C41 RID: 7233 RVA: 0x00107AC4 File Offset: 0x00105CC4
	// (set) Token: 0x06001C42 RID: 7234 RVA: 0x00107ADC File Offset: 0x00105CDC
	public bool Main
	{
		get
		{
			return this._Main;
		}
		set
		{
			this._Main = value;
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00107AE8 File Offset: 0x00105CE8
	// (set) Token: 0x06001C44 RID: 7236 RVA: 0x00107B00 File Offset: 0x00105D00
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			this._ID = value;
			this.setNetid(this._ID);
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00107B18 File Offset: 0x00105D18
	// (set) Token: 0x06001C46 RID: 7238 RVA: 0x00107B30 File Offset: 0x00105D30
	public int Index
	{
		get
		{
			return this._Index;
		}
		set
		{
			this._Index = value;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00107B3C File Offset: 0x00105D3C
	// (set) Token: 0x06001C48 RID: 7240 RVA: 0x00107B54 File Offset: 0x00105D54
	public int Type
	{
		get
		{
			return this._Type;
		}
		set
		{
			this._Type = value;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00107B60 File Offset: 0x00105D60
	// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00107B78 File Offset: 0x00105D78
	public string Resouce_Name
	{
		get
		{
			return this._Resouce_Name;
		}
		set
		{
			this._Resouce_Name = value;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00107B84 File Offset: 0x00105D84
	// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00107B9C File Offset: 0x00105D9C
	public int Current_HP
	{
		get
		{
			return this._Current_HP;
		}
		set
		{
			this._Current_HP = value;
			bool flag = this.Max_HP != 0 && this._Current_HP < 1;
			if (flag)
			{
				this.Suicide();
			}
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00107BD4 File Offset: 0x00105DD4
	// (set) Token: 0x06001C4E RID: 7246 RVA: 0x00107BEC File Offset: 0x00105DEC
	public int Max_HP
	{
		get
		{
			return this._Max_HP;
		}
		set
		{
			this._Max_HP = value;
		}
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00107BF8 File Offset: 0x00105DF8
	// (set) Token: 0x06001C50 RID: 7248 RVA: 0x00107C10 File Offset: 0x00105E10
	public int Current_Shield
	{
		get
		{
			return this._Current_Shield;
		}
		set
		{
			this._Current_Shield = value;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00107C1C File Offset: 0x00105E1C
	// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00107C34 File Offset: 0x00105E34
	public int Max_Shield
	{
		get
		{
			return this._Max_Shield;
		}
		set
		{
			this._Max_Shield = value;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00107C40 File Offset: 0x00105E40
	// (set) Token: 0x06001C54 RID: 7252 RVA: 0x00107C58 File Offset: 0x00105E58
	public int Weapon_Index
	{
		get
		{
			return this._Weapon_Index;
		}
		set
		{
			this._Weapon_Index = value;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00107C62 File Offset: 0x00105E62
	public override Vector3 userCore
	{
		get
		{
			return this._Position + this._userCoreAdd;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00107C78 File Offset: 0x00105E78
	// (set) Token: 0x06001C57 RID: 7255 RVA: 0x00107C90 File Offset: 0x00105E90
	public float Detection_Range
	{
		get
		{
			return this._Detection_Range;
		}
		set
		{
			this._Detection_Range = value;
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00107C9C File Offset: 0x00105E9C
	// (set) Token: 0x06001C59 RID: 7257 RVA: 0x00107CB4 File Offset: 0x00105EB4
	public string Create_Sound
	{
		get
		{
			return this._Create_Sound;
		}
		set
		{
			this._Create_Sound = value;
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00107CC0 File Offset: 0x00105EC0
	// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00107CD8 File Offset: 0x00105ED8
	public float Time
	{
		get
		{
			return this._Time;
		}
		set
		{
			this._Time = value;
		}
	}

	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00107CE4 File Offset: 0x00105EE4
	// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00107CFC File Offset: 0x00105EFC
	public Vector3 Position
	{
		get
		{
			return this._Position;
		}
		set
		{
			this._Position = value;
		}
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00107D08 File Offset: 0x00105F08
	// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00107D20 File Offset: 0x00105F20
	public Vector3 Rotation
	{
		get
		{
			return this._Rotation;
		}
		set
		{
			this._Rotation = value;
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00107D2C File Offset: 0x00105F2C
	// (set) Token: 0x06001C61 RID: 7265 RVA: 0x00107D44 File Offset: 0x00105F44
	public Vector3 Scale
	{
		get
		{
			return this._Scale;
		}
		set
		{
			this._Scale = value;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00107D50 File Offset: 0x00105F50
	// (set) Token: 0x06001C63 RID: 7267 RVA: 0x00107D68 File Offset: 0x00105F68
	public Vector3 Target_Position
	{
		get
		{
			return this._Target_Position;
		}
		set
		{
			this._Target_Position = value;
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00107D74 File Offset: 0x00105F74
	// (set) Token: 0x06001C65 RID: 7269 RVA: 0x00107D8C File Offset: 0x00105F8C
	public int Target_Net_ID
	{
		get
		{
			return this._Target_Net_ID;
		}
		set
		{
			this._Target_Net_ID = value;
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00107D98 File Offset: 0x00105F98
	// (set) Token: 0x06001C67 RID: 7271 RVA: 0x00107DB0 File Offset: 0x00105FB0
	public Vector3 ST
	{
		get
		{
			return this._ST;
		}
		set
		{
			this._ST = value;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00107DBC File Offset: 0x00105FBC
	// (set) Token: 0x06001C69 RID: 7273 RVA: 0x00107DD4 File Offset: 0x00105FD4
	public Vector3 Force
	{
		get
		{
			return this._Force;
		}
		set
		{
			this._Force = value;
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00107DE0 File Offset: 0x00105FE0
	// (set) Token: 0x06001C6B RID: 7275 RVA: 0x00107DF8 File Offset: 0x00105FF8
	public int Country
	{
		get
		{
			return this._Country;
		}
		set
		{
			this._Country = value;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00107E04 File Offset: 0x00106004
	// (set) Token: 0x06001C6D RID: 7277 RVA: 0x00107E1C File Offset: 0x0010601C
	public bool Death_State
	{
		get
		{
			return this._Death_State;
		}
		set
		{
			this._Death_State = value;
			this.isDead = this._Death_State;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00107E34 File Offset: 0x00106034
	// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00107E4C File Offset: 0x0010604C
	public bool Gravity
	{
		get
		{
			return this._Gravity;
		}
		set
		{
			this._Gravity = value;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00107E58 File Offset: 0x00106058
	// (set) Token: 0x06001C71 RID: 7281 RVA: 0x00107E70 File Offset: 0x00106070
	public bool Can_Buff
	{
		get
		{
			return this._Can_Buff;
		}
		set
		{
			this._Can_Buff = value;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00107E7C File Offset: 0x0010607C
	// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00107E94 File Offset: 0x00106094
	public string Damage_Sound
	{
		get
		{
			return this._Damage_Sound;
		}
		set
		{
			this._Damage_Sound = value;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00107EA0 File Offset: 0x001060A0
	// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00107EB8 File Offset: 0x001060B8
	public int AI_ID
	{
		get
		{
			return this._AI_ID;
		}
		set
		{
			this._AI_ID = value;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00107EC4 File Offset: 0x001060C4
	// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00107EDC File Offset: 0x001060DC
	public bool Need_Synchronization
	{
		get
		{
			return this._Need_Synchronization;
		}
		set
		{
			this._Need_Synchronization = value;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00107EE8 File Offset: 0x001060E8
	// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00107F00 File Offset: 0x00106100
	public string Death_Effect
	{
		get
		{
			return this._Death_Effect;
		}
		set
		{
			this._Death_Effect = value;
		}
	}

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00107F0C File Offset: 0x0010610C
	// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00107F24 File Offset: 0x00106124
	public int Adhere
	{
		get
		{
			return this._Adhere;
		}
		set
		{
			this._Adhere = value;
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00107F30 File Offset: 0x00106130
	// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00107F48 File Offset: 0x00106148
	public bool Need_Change_Layer
	{
		get
		{
			return this._Need_Change_Layer;
		}
		set
		{
			this._Need_Change_Layer = value;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00107F54 File Offset: 0x00106154
	// (set) Token: 0x06001C7F RID: 7295 RVA: 0x00107F6C File Offset: 0x0010616C
	public bool Can_Shoot
	{
		get
		{
			return this._Can_Shoot;
		}
		set
		{
			this._Can_Shoot = value;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00107F78 File Offset: 0x00106178
	// (set) Token: 0x06001C81 RID: 7297 RVA: 0x00107F90 File Offset: 0x00106190
	public string Icon
	{
		get
		{
			return this._Icon;
		}
		set
		{
			this._Icon = value;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00107F9C File Offset: 0x0010619C
	// (set) Token: 0x06001C83 RID: 7299 RVA: 0x00107FB4 File Offset: 0x001061B4
	public string Death_Sound
	{
		get
		{
			return this._Death_Sound;
		}
		set
		{
			this._Death_Sound = value;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00107FC0 File Offset: 0x001061C0
	// (set) Token: 0x06001C85 RID: 7301 RVA: 0x00107FD8 File Offset: 0x001061D8
	public int SignHeight
	{
		get
		{
			return this._SignHeight;
		}
		set
		{
			this._SignHeight = value;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00107FE4 File Offset: 0x001061E4
	// (set) Token: 0x06001C87 RID: 7303 RVA: 0x00107FFC File Offset: 0x001061FC
	public int ShowHp
	{
		get
		{
			return this._Showhp;
		}
		set
		{
			this._Showhp = value;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00108008 File Offset: 0x00106208
	// (set) Token: 0x06001C89 RID: 7305 RVA: 0x00108020 File Offset: 0x00106220
	public int Stunt
	{
		get
		{
			return this._Stunt;
		}
		set
		{
			this._Stunt = value;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0010802C File Offset: 0x0010622C
	// (set) Token: 0x06001C8B RID: 7307 RVA: 0x00108044 File Offset: 0x00106244
	public int Cure
	{
		get
		{
			return this._Cure;
		}
		set
		{
			this._Cure = value;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00108050 File Offset: 0x00106250
	// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00108068 File Offset: 0x00106268
	public int Show_Locked
	{
		get
		{
			return this._Show_Locked;
		}
		set
		{
			this._Show_Locked = value;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00108074 File Offset: 0x00106274
	// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0010808C File Offset: 0x0010628C
	public bool Force_Impact
	{
		get
		{
			return this._Force_Impact;
		}
		set
		{
			this._Force_Impact = value;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00108098 File Offset: 0x00106298
	// (set) Token: 0x06001C91 RID: 7313 RVA: 0x001080B0 File Offset: 0x001062B0
	public float Scale_Multiple
	{
		get
		{
			return this._Scale_Multiple;
		}
		set
		{
			this._Scale_Multiple = value;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06001C92 RID: 7314 RVA: 0x001080BC File Offset: 0x001062BC
	// (set) Token: 0x06001C93 RID: 7315 RVA: 0x001080D4 File Offset: 0x001062D4
	public string Focus
	{
		get
		{
			return this._Focus;
		}
		set
		{
			this._Focus = value;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06001C94 RID: 7316 RVA: 0x001080E0 File Offset: 0x001062E0
	// (set) Token: 0x06001C95 RID: 7317 RVA: 0x001080F8 File Offset: 0x001062F8
	public int Physics
	{
		get
		{
			return this._Physics;
		}
		set
		{
			this._Physics = value;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00108104 File Offset: 0x00106304
	// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0010811C File Offset: 0x0010631C
	public bool Action_State
	{
		get
		{
			return this._Action_State;
		}
		set
		{
			this._Action_State = value;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00108128 File Offset: 0x00106328
	// (set) Token: 0x06001C99 RID: 7321 RVA: 0x00108140 File Offset: 0x00106340
	public float Mass
	{
		get
		{
			return this._Mass;
		}
		set
		{
			this._Mass = value;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0010814C File Offset: 0x0010634C
	// (set) Token: 0x06001C9B RID: 7323 RVA: 0x00108164 File Offset: 0x00106364
	public int camerswitch
	{
		get
		{
			return this._camerswitch;
		}
		set
		{
			this._camerswitch = value;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00108170 File Offset: 0x00106370
	// (set) Token: 0x06001C9D RID: 7325 RVA: 0x001081B6 File Offset: 0x001063B6
	public GameObject Eye_Object
	{
		get
		{
			bool flag = this._Eye_Object == null;
			if (flag)
			{
				this._Eye_Object = Fun.Get_Child_Transform(this.thisModel, this.Focus).gameObject;
			}
			return this._Eye_Object;
		}
		set
		{
			this._Eye_Object = value;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06001C9E RID: 7326 RVA: 0x001081C0 File Offset: 0x001063C0
	// (set) Token: 0x06001C9F RID: 7327 RVA: 0x001081D8 File Offset: 0x001063D8
	public Npc_AI_Controller AI_Component
	{
		get
		{
			return this._AI_Component;
		}
		set
		{
			this._AI_Component = value;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x001081E4 File Offset: 0x001063E4
	// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x001081FC File Offset: 0x001063FC
	public float Camera_Distance
	{
		get
		{
			return this._Camera_Distance;
		}
		set
		{
			this._Camera_Distance = value;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00108208 File Offset: 0x00106408
	// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x00108220 File Offset: 0x00106420
	public effectMe npceffectBase
	{
		get
		{
			return this._npceffectBase;
		}
		set
		{
			this._npceffectBase = value;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0010822C File Offset: 0x0010642C
	// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x00108244 File Offset: 0x00106444
	public buffNpc NpcBuff
	{
		get
		{
			return this._NpcBuff;
		}
		set
		{
			this._NpcBuff = value;
		}
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00108250 File Offset: 0x00106450
	public Npc_Entity()
	{
		this.filmObj = true;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x001082CC File Offset: 0x001064CC
	public void Fill_Xml_Data(int _Index)
	{
		Npc_Entity npc_Entity = fGameManager.mloadConfig._Npc_Xml_Data[_Index];
		this.playerCommon = new PlayerCommon();
		this.playerCommon.m_nHp = npc_Entity.Max_HP;
		this.playerCommon.m_nMaxHp = npc_Entity.Max_HP;
		this.playerCommon.m_nShield = npc_Entity.Max_Shield;
		this.playerCommon.m_nMaxShield = npc_Entity.Max_Shield;
		this.playerCommon.m_sName = npc_Entity.Name;
		base.Name = npc_Entity.Name;
		this.Type = npc_Entity.Type;
		this.Resouce_Name = npc_Entity.Resouce_Name;
		this.Current_HP = npc_Entity.Max_HP;
		this.Max_HP = npc_Entity.Max_HP;
		this.Current_Shield = npc_Entity.Max_Shield;
		this.Max_Shield = npc_Entity.Max_Shield;
		this.Weapon_Index = npc_Entity.Weapon_Index;
		this.Detection_Range = npc_Entity.Detection_Range;
		this.Create_Sound = npc_Entity.Create_Sound;
		this.Time = npc_Entity.Time;
		this.Gravity = npc_Entity.Gravity;
		this.Can_Buff = npc_Entity.Can_Buff;
		this.Death_Effect = npc_Entity.Death_Effect;
		this.Adhere = npc_Entity.Adhere;
		this.Need_Change_Layer = npc_Entity.Need_Change_Layer;
		this.Icon = npc_Entity.Icon;
		this.SignHeight = npc_Entity.SignHeight;
		this.Death_Sound = npc_Entity.Death_Sound;
		this.Stunt = npc_Entity.Stunt;
		this.ShowHp = npc_Entity.ShowHp;
		this.AI_ID = npc_Entity.AI_ID;
		this.Cure = npc_Entity.Cure;
		this.Show_Locked = npc_Entity.Show_Locked;
		this.Force_Impact = npc_Entity.Force_Impact;
		this.Scale_Multiple = npc_Entity.Scale_Multiple;
		this.Focus = npc_Entity.Focus;
		this.Physics = npc_Entity.Physics;
		this.Mass = npc_Entity.Mass;
		this.camerswitch = npc_Entity.camerswitch;
		this.Camera_Distance = npc_Entity.Camera_Distance;
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x001084E0 File Offset: 0x001066E0
	public void Initialize(ArrayList _Parameters, ArrayList _weaponData, int herono)
	{
		int num = int.Parse(_Parameters[0].ToString());
		int id = int.Parse(_Parameters[1].ToString());
		int index = int.Parse(_Parameters[2].ToString());
		int num2 = int.Parse(_Parameters[3].ToString());
		ArrayList arrayList = (ArrayList)_Parameters[4];
		ArrayList arrayList2 = (ArrayList)_Parameters[5];
		ArrayList arrayList3 = (ArrayList)_Parameters[6];
		ArrayList arrayList4 = (ArrayList)_Parameters[7];
		int target_ID = int.Parse(_Parameters[8].ToString());
		int num3 = int.Parse(_Parameters[9].ToString());
		int num4 = int.Parse(_Parameters[10].ToString());
		int num5 = int.Parse(_Parameters[11].ToString());
		string collider_Name = _Parameters[13].ToString();
		bool flag = num5 < 1;
		if (flag)
		{
			this.OnNetDestroy();
		}
		else
		{
			this.netobject_type = 2;
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			int nShiliServer = num2;
			bool flag2 = MatchManager.GetInstance().m_pFbData.nType == 10 || MatchManager.GetInstance().m_pFbData.nType == 11 || MatchManager.GetInstance().m_nMatchType == 4;
			if (flag2)
			{
				int num6 = (player != null && player.playerCommon != null) ? player.playerCommon.m_nShili : 1;
				num2 = ((num == player.getNetid()) ? num6 : ((num6 == 1) ? 2 : 1));
			}
			this.Master_ID = num;
			NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(this.Master_ID);
			NetLiving netLiving = netObj as NetLiving;
			bool flag3 = netLiving != null && netObj.netobject_type == 6;
			if (flag3)
			{
				bool needSendToServer = ((RobotAI)netLiving).needSendToServer;
				if (needSendToServer)
				{
					this.needSendToServer = true;
					this.SynchronizationRB_Object = netLiving;
					this.Main = true;
				}
			}
			else
			{
				bool flag4 = player.getNetid() == num && !this.isCopy;
				if (flag4)
				{
					this.Synchronization_Me(player);
					if (player != null)
					{
						player.heroBase.GetHeroNpc(this);
					}
					this.Main = true;
				}
				else
				{
					NetLiving netLiving2 = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(num);
					this.Synchronization_It();
					if (netLiving2 != null)
					{
						netLiving2.heroBase.GetHeroNpc(this);
					}
					this.Main = false;
				}
			}
			this.ID = id;
			this.Index = index;
			this.Country = num2;
			this.Fill_Xml_Data(index);
			this.playerCommon.m_nHp = num3;
			this.playerCommon.m_nMaxHp = num3;
			this.Max_HP = num3;
			this.Time = Fun.Convert_Float(num4.ToString(), 1000);
			this.nForce = num2;
			this.Position = new Vector3((float)Convert.ToInt32(arrayList[0]) * 1f / 100f, (float)Convert.ToInt32(arrayList[1]) * 1f / 100f, (float)Convert.ToInt32(arrayList[2]) * 1f / 100f);
			this.Rotation = new Vector3((float)Convert.ToInt32(arrayList2[0]) * 1f / 100f, (float)Convert.ToInt32(arrayList2[1]) * 1f / 100f, (float)Convert.ToInt32(arrayList2[2]) * 1f / 100f);
			this.Scale = new Vector3((float)Convert.ToInt32(arrayList3[0]) * 1f / 100f * this.Scale_Multiple, (float)Convert.ToInt32(arrayList3[1]) * 1f / 100f * this.Scale_Multiple, (float)Convert.ToInt32(arrayList3[2]) * 1f / 100f * this.Scale_Multiple);
			this.Force = new Vector3((float)Convert.ToInt32(arrayList4[0]) * 1f / 100f, (float)Convert.ToInt32(arrayList4[1]) * 1f / 100f, (float)Convert.ToInt32(arrayList4[2]) * 1f / 100f);
			bool flag5 = _weaponData.Count >= 2;
			if (flag5)
			{
				this.playerCommon.aryBulletCartridgeID = (ArrayList)_weaponData[0];
				this.playerCommon.aryBulletGemData = (ArrayList)_weaponData[1];
			}
			this.playerCommon.m_nHeroNo = herono;
			this.playerCommon.pos = this.Position;
			this.playerCommon.m_nShili = this.nForce;
			this.playerCommon.m_nShiliServer = nShiliServer;
			bool flag6 = this.Container == null;
			if (flag6)
			{
				this.Container = new GameObject("Npc" + this.ID.ToString());
				this.Container.transform.localPosition = Vector3.zero;
				this.Container.transform.localEulerAngles = Vector3.zero;
				this.Container.transform.localScale = Vector3.one;
			}
			this.userRoot = this.Container;
			this.UserControl = new GameObject("control");
			this.UserControl.transform.parent = this.userRoot.transform;
			this.UserControl.transform.position = this.Position;
			this.UserControl.transform.eulerAngles = this.Rotation;
			this.defaultMesh = new GameObject("defaultMesh");
			this.defaultMesh.transform.parent = this.UserControl.transform;
			this.defaultMesh.transform.localPosition = Vector3.zero;
			this.heroBase = this.UserControl.AddComponent<HeroBaseIt>();
			this.heroBase.HeroInit(this, null);
			this.camTarget = new GameObject("Camera_Target")
			{
				transform = 
				{
					parent = this.UserControl.transform,
					localPosition = Vector3.zero,
					localEulerAngles = Vector3.zero,
					localScale = Vector3.one
				}
			}.transform;
			this.FindMubiaoTarget = new GameObject("FindMubiaoTarget");
			this.FindMubiaoTarget.transform.parent = this.camTarget;
			this.FindMubiaoTarget.transform.localPosition = Vector3.zero;
			this.FindMubiaoTarget.transform.localEulerAngles = Vector3.zero;
			this.playerTarget = new GameObject("PlayerTarget");
			this.playerTarget.transform.parent = this.userRoot.transform;
			this.playerTarget.transform.position = this.camTarget.position + this.camTarget.forward * 200f;
			this.Initialize_Component();
			this._playerSoundContainer = new GameObject("_playerSoundContainer");
			this._playerSoundContainer.transform.parent = this.UserControl.transform;
			this._playerSoundContainer.transform.localPosition = new Vector3(0f, 1f, 1f);
			this._playerSoundContainer.transform.localEulerAngles = Vector3.zero;
			this._playerEffectContainer = new GameObject("_playerEffectContainer");
			this._playerEffectContainer.transform.parent = this.UserControl.transform;
			this._playerEffectContainer.transform.localPosition = Vector3.zero;
			this._playerEffectContainer.transform.localEulerAngles = Vector3.zero;
			this._playerRootEffectContainer = new GameObject("_playerRootEffectContainer");
			this._playerRootEffectContainer.transform.parent = this.userRoot.transform;
			this._playerRootEffectContainer.transform.localPosition = Vector3.zero;
			this._playerRootEffectContainer.transform.localEulerAngles = Vector3.zero;
			base.userSpine = this.UserControl.transform;
			this.npceffectBase = this.UserControl.AddComponent<effectMe>();
			this.npceffectBase.gameplayer = this;
			this._NpcBuff = this.UserControl.AddComponent<buffNpc>();
			this._NpcBuff.OneBuffInit(this);
			((Npc_Controller)this.Controller_Component).Load_Model(target_ID, collider_Name);
		}
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x00108D9C File Offset: 0x00106F9C
	public virtual void Initialize_Component()
	{
		bool flag = this.AI_ID > 0 && this.AI_Component == null;
		if (flag)
		{
			this.AI_Component = this.Container.GetComponent<Npc_AI_Controller>();
			bool flag2 = this.AI_Component == null;
			if (flag2)
			{
				this.AI_Component = this.Container.AddComponent<Npc_AI_Controller>();
			}
			this.AI_Component.Npc_Entity_Component = this;
			this.AI_Component.Initialize(this.AI_ID);
		}
		bool flag3 = this.Detection_Controller_Component == null;
		if (flag3)
		{
			this.Detection_Controller_Component = this.Container.GetComponent<Detection_Controller>();
			bool flag4 = this.Detection_Controller_Component == null;
			if (flag4)
			{
				this.Detection_Controller_Component = this.Container.AddComponent<Detection_Controller>();
			}
		}
		this.Detection_Controller_Component.Type = this.Type;
		this.Detection_Controller_Component.Country = this.Country;
		bool flag5 = this.AI_Component != null;
		if (flag5)
		{
			this.Detection_Controller_Component.Detection_Range = this.AI_Component.Model_Component.Entity_Component.Alert_Range;
		}
		else
		{
			this.Detection_Controller_Component.Detection_Range = this.Detection_Range;
		}
		this.Npc_Info_Component = this.Container.AddComponent<Npc_Info>();
		this.publicInfo = this.Npc_Info_Component;
		this.Npc_Info_Component.InitInfo(this);
		this.Npc_Info_Component.type = "Npc";
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x00108F10 File Offset: 0x00107110
	public void Synchronization_Validate()
	{
		bool flag = !(this.thisModel == null);
		if (flag)
		{
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			bool flag2 = player != null && !(player.thisModel == null);
			if (flag2)
			{
				player.SendMessageToServer(1020001, new ArrayList
				{
					base.getNetid()
				});
			}
		}
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x00108F84 File Offset: 0x00107184
	public void NSP(ArrayList _Parameters)
	{
		this.playerCommon.pos = new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f);
		bool flag = !this.Main && this.Need_Synchronization;
		if (flag)
		{
			this.Position = this.playerCommon.pos;
			bool flag2 = this.charMove != null;
			if (flag2)
			{
				((TBUserControlNpcIt)this.charMove).moveTarget = this.Position;
			}
		}
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x00109040 File Offset: 0x00107240
	public void NSR(ArrayList _Parameters)
	{
		bool flag = !this.Main && this.Need_Synchronization;
		if (flag)
		{
			this.Rotation = new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f);
		}
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x001090BC File Offset: 0x001072BC
	public new void NSS(ArrayList _Parameters)
	{
		bool flag = !this.Main && this.Need_Synchronization;
		if (flag)
		{
			this.Scale = new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f);
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00109138 File Offset: 0x00107338
	public void NSTP(int _Target_Net_ID, ArrayList _Parameters)
	{
		bool flag = !this.Main;
		if (flag)
		{
			this.Target_Net_ID = _Target_Net_ID;
			this.Target_Position = new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f);
			bool flag2 = this.playerTarget != null;
			if (flag2)
			{
				this.playerTarget.transform.position = this.Target_Position;
			}
			bool flag3 = this.charMove != null;
			if (flag3)
			{
				((TBUserControlNpcIt)this.charMove).lookAt = this.Target_Position;
			}
		}
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x00109208 File Offset: 0x00107408
	public void NST(ArrayList _Parameters)
	{
		bool flag = !this.Main && this.Need_Synchronization;
		if (flag)
		{
			this.ST = new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f);
			bool flag2 = this.playerTarget != null;
			if (flag2)
			{
				this.playerTarget.transform.position = this.ST;
			}
			bool flag3 = this.charMove != null;
			if (flag3)
			{
				((TBUserControlNpcIt)this.charMove).lookAt = this.ST;
			}
		}
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x001092D8 File Offset: 0x001074D8
	public virtual void Suicide()
	{
		bool flag = this.Synchronization_Object != null || this.SynchronizationRB_Object != null;
		if (flag)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.ID);
			arrayList.Add(0);
			bool flag2 = this.Synchronization_Object != null;
			if (flag2)
			{
				this.Synchronization_Object.SendMessageToServer(1010004, arrayList);
			}
			bool flag3 = this.SynchronizationRB_Object != null;
			if (flag3)
			{
				this.SynchronizationRB_Object.SendMsgToServer(1010004, arrayList);
			}
		}
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x0010936C File Offset: 0x0010756C
	public void Custom_Death(int _Attack_ID, int _State, ArrayList m_szCanYuXiaoMie, int nDeadType)
	{
		bool flag = !this.Death_State;
		if (flag)
		{
			Fun.Change_Layer(this.UserControl, 2);
			this.OnDeathNpc(_Attack_ID, 0, new ArrayList(), 0, _State, m_szCanYuXiaoMie, nDeadType);
			this.Death_State = true;
			bool flag2 = this.AI_Component != null;
			if (flag2)
			{
				this.AI_Component.Switch_Animation("Death");
			}
			bool flag3 = this.Synchronization_Object != null && this.Synchronization_Object.isMainPlayer;
			if (flag3)
			{
				this.Synchronization_Object.Npc_Death(this.Type);
			}
		}
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00109404 File Offset: 0x00107604
	public void Destroy_Model(int _Death_Type)
	{
		bool flag = !this.haveDestroy;
		if (flag)
		{
			bool flag2 = this.View_Component == null;
			if (flag2)
			{
				this.OnNetDestroy();
			}
			else
			{
				((Npc_View)this.View_Component).Destroy_Wait(_Death_Type);
			}
		}
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x00109450 File Offset: 0x00107650
	public void Chariot_Create_Scene_Effect(ArrayList _Parameters)
	{
		Fun.Load_Scene_Effect("cai_tt_point", new Vector3((float)Convert.ToInt32(_Parameters[0]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[1]) * 1f / 100f, (float)Convert.ToInt32(_Parameters[2]) * 1f / 100f));
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x001094BC File Offset: 0x001076BC
	public void NCAS()
	{
		bool flag = !this.haveDestroy && this.playerHaveCreate && !(this._Animator_Component == null) && !(this.Controller_Component == null);
		if (flag)
		{
			this._Animator_Component.SetBool("Attack", true);
			((Npc_Controller)this.Controller_Component).Attack_Start();
		}
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00109524 File Offset: 0x00107724
	public void NCAP()
	{
		bool flag = !this.haveDestroy && this.playerHaveCreate && !(this._Animator_Component == null) && !(this.Controller_Component == null);
		if (flag)
		{
			this._Animator_Component.SetBool("Attack", false);
			((Npc_Controller)this.Controller_Component).Attack_Stop();
		}
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x0010958C File Offset: 0x0010778C
	public virtual void OnDeathNpc(int _attackId, int o, ArrayList forcev3, int forcemax, int _State, ArrayList m_szCanYuXiaoMie, int nDeadType)
	{
		base.OnDeath(_attackId, 0, new ArrayList(), 0, 0, 0, _State, m_szCanYuXiaoMie, nDeadType, new ArrayList());
		this.heroBase.OnDeath();
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			PlayerLearnManager.GetInstance().OnNpcDead(this.Index, this.ID, _State);
		}
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x001095E6 File Offset: 0x001077E6
	public void Synchronization_Me(HeroPlayer _HeroPlayer)
	{
		this.needSendToServer = true;
		this.Synchronization_Object = _HeroPlayer;
		this.Main = true;
		this.Load_Weapon();
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x00109608 File Offset: 0x00107808
	public void Synchronization_It()
	{
		this.needSendToServer = false;
		this.Synchronization_Object = null;
		this.Main = false;
		this.Load_Weapon();
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x0010962C File Offset: 0x0010782C
	public void Load_Weapon()
	{
		bool flag = !(this.thisModel == null);
		if (flag)
		{
			bool flag2 = this.wc != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.wc);
				this.wc = null;
			}
			bool flag3 = this.Weapon_Index != 0;
			if (flag3)
			{
				base.InitHeroWeapon(this.Weapon_Index);
			}
		}
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x00109690 File Offset: 0x00107890
	public override void CB(int launcherName, int launcherNo, string muzzleName, int bullId, int bullettype, ArrayList born, ArrayList target, ArrayList parm, int numberBullet, int _cartridge, int _loadid)
	{
		bool flag = !this.Main && !(this.userModelTps == null);
		if (flag)
		{
			base.CB(launcherName, launcherNo, muzzleName, bullId, bullettype, born, target, parm, numberBullet, _cartridge, _loadid);
		}
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x001096D8 File Offset: 0x001078D8
	public override void CBP(int weaponName, int launcherNo, string muzzleName, int bullId, int x, int y, int z)
	{
		bool flag = this.Synchronization_Object == null || this.Synchronization_Object.getNetid() != fGameManager.GetInstance().getPlayer().getNetid();
		if (flag)
		{
			base.CBP(weaponName, launcherNo, muzzleName, bullId, x, y, z);
		}
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x00109728 File Offset: 0x00107928
	public override void DamageMsgToServer(NetLiving _HeroPlayer, ArrayList _Parameters)
	{
		bool flag = this.SynchronizationRB_Object != null;
		if (flag)
		{
			this.SynchronizationRB_Object.DamageMsgToServer(_HeroPlayer, _Parameters);
		}
		else
		{
			this.Synchronization_Object.DamageMsgToServer(_HeroPlayer, _Parameters);
		}
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x00109765 File Offset: 0x00107965
	public override void SendMessageToServer(int _NetID, ArrayList _Parameters)
	{
		this.Synchronization_Object.SendMessageToServer(_NetID, _Parameters);
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x00109778 File Offset: 0x00107978
	public override void OnFightStat(int m_nFightStat, int m_nFightID)
	{
		bool flag = !this.haveDestroy;
		if (flag)
		{
			base.OnFightStat(m_nFightStat, m_nFightID);
			this.playerCommon.m_nFightStat = m_nFightStat;
			NetLiving netLiving = (NetLiving)GameRoot.SceneSocket.unFsorket.GetNetObj(this.Master_ID);
			bool flag2 = netLiving != null && netLiving.playerHaveCreate && !netLiving.haveDestroy && netLiving.playerCommon.m_nIsFristFight == 0 && m_nFightStat == 1;
			if (flag2)
			{
				netLiving.playerCommon.m_nIsFristFight = 1;
			}
		}
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x001097FE File Offset: 0x001079FE
	public void Net_Destroy()
	{
		this.OnNetDestroy();
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00109808 File Offset: 0x00107A08
	public override void OnNetDestroy()
	{
		base.OnNetDestroy();
		bool flag = this.UserControl != null;
		if (flag)
		{
			MeNpcModelSign component = this.UserControl.GetComponent<MeNpcModelSign>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.OnDel();
			}
		}
		bool flag3 = this.newplayercon != null;
		if (flag3)
		{
			this.newplayercon.OnDel();
		}
		UnityEngine.Object.Destroy(this.UserControl);
		UnityEngine.Object.Destroy(this.Container);
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x00109888 File Offset: 0x00107A88
	public override void HaveDamage(ArrayList szDamage, ArrayList szHp)
	{
		base.HaveDamage(szDamage, szHp);
		bool flag = this.Main && this.Force_Impact;
		if (flag)
		{
			int num = (int)szDamage[2];
			ArrayList arrayList = (ArrayList)szDamage[1];
			bool flag2 = arrayList.Count > 1;
			if (flag2)
			{
				this.forceDamage = new Vector3((float)((int)arrayList[0]) * 0.01f, (float)((int)arrayList[1]) * 0.01f, (float)((int)arrayList[2]) * 0.01f);
			}
			bool flag3 = num > 0 && arrayList.Count > 0;
			if (flag3)
			{
				this.Add_Force(this.forceDamage * (float)num * 0.01f);
			}
		}
		bool flag4 = szHp.Count > 0;
		if (flag4)
		{
			this.SetHp(szHp);
		}
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00109978 File Offset: 0x00107B78
	public void Add_Force(Vector3 _Force)
	{
		this.Rigidbody_Component.AddForce(_Force, ForceMode.Impulse);
		bool flag = this.AI_Component != null;
		if (flag)
		{
			this.AI_Component.Restore_Position();
		}
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x001099B4 File Offset: 0x00107BB4
	public void Add_Velocity(Vector3 _Speed)
	{
		this.Rigidbody_Component.velocity = _Speed;
		bool flag = this.AI_Component != null;
		if (flag)
		{
			this.AI_Component.Restore_Position();
		}
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x001099ED File Offset: 0x00107BED
	public override void SetHp(ArrayList szHp)
	{
		base.SetHp(szHp);
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x001099F8 File Offset: 0x00107BF8
	public virtual void Load_Model_Resouce_Complete()
	{
		this.Load_Weapon();
		bool flag = this.tChar != null;
		if (flag)
		{
			this.tCharAnimationTps = this.thisModel.AddComponent<TBCharAnimationThirdNpc>();
			this.tChar.InitCharThird(this, this.tCharAnimationTps, this.charMove, null);
		}
		this._Animator_Component = this.thisModel.GetComponent<Animator>();
		this.userModelTps = this.thisModel;
		bool flag2 = this.Create_Sound != "0" && this.Create_Sound != string.Empty;
		if (flag2)
		{
			Fun.Load_Sound(this, this.Create_Sound, this.UserControl, true, "Npc_Create_Sound");
		}
		bool need_Change_Layer = this.Need_Change_Layer;
		if (need_Change_Layer)
		{
			bool flag3 = this.Country == 1;
			if (flag3)
			{
				Fun.Change_Layer(this.UserControl, 11);
			}
			else
			{
				Fun.Change_Layer(this.UserControl, 12);
			}
		}
		bool flag4 = this.Max_HP > 0;
		if (flag4)
		{
			Fun.Change_Tag(this.UserControl, "can_damage_robot");
			this.defaultMesh.tag = "can_damage_robot";
		}
		int type = this.Type;
		int num = type;
		if (num != 5)
		{
			if (num == 12)
			{
				Fun.Change_Tag(this.thisModel, "Transmission");
			}
		}
		else
		{
			Fun.Change_Tag(this.thisModel, "Elevator");
		}
		Collider[] componentsInChildren = this.thisModel.transform.GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			bool flag5 = componentsInChildren[i].GetType() == typeof(BoxCollider) && !componentsInChildren[i].isTrigger;
			if (flag5)
			{
				this.bodyCollider = componentsInChildren[i];
			}
			bool flag6 = componentsInChildren[i].name.ToLower().Contains("head");
			if (flag6)
			{
				base.userHeadTransform = componentsInChildren[i].transform;
			}
		}
		this.renderBianSe = this.thisModel.AddComponent<CMeshRenderBianSe>();
		this.renderBianSe.InitBianSe(this);
		this.renderBianSe.ChangeMateial(true);
		this.renderBianSe.ChangeYinShenMat(2);
		this.Add_Other_Component();
		base.Get_Stroke_Material_Array();
		this.CheckNpcJianYing();
		base.ModeLoadOK();
		this.playerHaveCreate = true;
		bool isSingle = fGameManager.IsSingle;
		if (isSingle)
		{
			PlayerLearnManager.GetInstance().OnLearnNpcCreateOk(this, this.Index, this.ID);
		}
		int num2 = CFilmManager.Instance.filmForce(this);
		CObjectRender component = this.thisModel.GetComponent<CObjectRender>();
		bool flag7 = num2 != this.playerCommon.m_nShiliServer;
		if (flag7)
		{
			bool flag8 = component != null;
			if (flag8)
			{
				component.SetEnemyFriendRenderParticle(true, false);
			}
		}
		else
		{
			bool flag9 = component != null;
			if (flag9)
			{
				component.SetEnemyFriendRenderParticle(false, false);
			}
		}
		this.NetLivingCreateFinish();
		bool flag10 = this.Resouce_Name.Contains("hl_npc");
		if (flag10)
		{
			this.Need_Synchronization = false;
		}
		bool flag11 = CFilmManager.Instance.filmParentNetId(this) == this.Master_ID && this.SignHeight > 0 && !fGameManager.IsSingle;
		if (flag11)
		{
			MeNpcModelSign meNpcModelSign = this.UserControl.AddComponent<MeNpcModelSign>();
			meNpcModelSign.OnStart(this);
		}
		bool flag12 = this.ShowHp > 0;
		if (flag12)
		{
			GameObject gameObject = new GameObject("NpcHeadTarget");
			this.UserHeadTarget = gameObject.transform;
			this.UserHeadTarget.parent = this.UserControl.transform;
			this.UserHeadTarget.localPosition = Vector3.zero;
			this.UserHeadTarget.localEulerAngles = Vector3.zero;
			this.UserHeadTarget.localScale = Vector3.one;
			this.LoadNpcHeadUi();
		}
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x00109DBC File Offset: 0x00107FBC
	private void Add_Other_Component()
	{
		foreach (Collider collider in this.UserControl.GetComponentsInChildren<Collider>())
		{
			bool isTrigger = collider.isTrigger;
			if (isTrigger)
			{
				Npc_Trigger x = collider.gameObject.GetComponent<Npc_Trigger>();
				bool flag = x == null;
				if (flag)
				{
					x = collider.gameObject.AddComponent<Npc_Trigger>();
				}
			}
			Npc_Data npc_Data = collider.gameObject.GetComponent<Npc_Data>();
			bool flag2 = npc_Data == null;
			if (flag2)
			{
				npc_Data = collider.gameObject.AddComponent<Npc_Data>();
			}
			npc_Data.Info_Component = this.Npc_Info_Component;
			npc_Data.Entity_Component = this;
		}
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x00109E74 File Offset: 0x00108074
	public void Receive_Transmission_Durability(int _Value)
	{
		bool flag = this.Synchronization_Object != null && !(this.Synchronization_Object.heroBase == null) && this.Synchronization_Object.heroBase.GetType() == typeof(HeroLingLongMe);
		if (flag)
		{
			((IHeroDataLingLong)this.Synchronization_Object.heroBase.IHD).Transmission_Durability = _Value;
		}
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00109EE0 File Offset: 0x001080E0
	public override void SetAnimator(string ani, int type, object parm, int sendtype = 1)
	{
		bool flag = ani == "0" || this._Animator_Component == null || this._Animator_Component.parameterCount < 1;
		if (!flag)
		{
			bool flag2 = this.Type == 8 && this.Main && ani.Contains("AttackState") && (int)parm > 0;
			if (flag2)
			{
				CameraControl.Instance.onceFire();
			}
			bool flag3 = !this.isDead;
			if (!flag3)
			{
				switch (type)
				{
				case 0:
					this._Animator_Component.SetBool(ani, (bool)parm);
					break;
				case 1:
					this._Animator_Component.SetFloat(ani, (float)parm);
					break;
				case 2:
					this._Animator_Component.SetInteger(ani, (int)parm);
					break;
				default:
					Debug.LogError("AnimationThird这个动画类型还不支持,请找LPJ.");
					break;
				}
				bool flag4 = this.Main && this.Synchronization_Object != null;
				if (flag4)
				{
					switch (type)
					{
					case 0:
						parm = (((bool)parm) ? 1 : 0);
						break;
					case 1:
						parm = (int)Math.Ceiling((double)((float)parm * 100f));
						break;
					case 2:
						break;
					default:
						return;
					}
					ArrayList arrayList = new ArrayList
					{
						new ArrayList
						{
							ani,
							type,
							parm
						},
						sendtype,
						base.getNetid(),
						this.loadID
					};
					this.Synchronization_Object.TosFun("SA_Npc", new object[]
					{
						arrayList
					});
				}
			}
		}
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0010A0D4 File Offset: 0x001082D4
	public void CA(ArrayList aryParam)
	{
		bool flag = !this.Main && this._Animator_Component && this._Animator_Component.parameterCount >= 1;
		if (flag)
		{
			ArrayList arrayList = (ArrayList)aryParam[0];
			bool flag2 = (int)aryParam[1] == 0;
			if (flag2)
			{
				((TBUserControlNpcIt)this.charMove).SAGet((string)arrayList[0], (int)arrayList[1], arrayList[2]);
			}
			else
			{
				this.SetAnimatorUpdate((string)arrayList[0], (int)arrayList[1], arrayList[2]);
			}
		}
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x0010A190 File Offset: 0x00108390
	public override void SetAnimatorUpdate(string ani, int type, object parm)
	{
		bool flag = !this.Main && this._Animator_Component && this._Animator_Component.parameterCount >= 1;
		if (flag)
		{
			switch (type)
			{
			case 0:
				parm = ((int)parm == 1);
				this._Animator_Component.SetBool(ani, (bool)parm);
				break;
			case 1:
				parm = (float)((int)parm) * 0.01f;
				this._Animator_Component.SetFloat(ani, (float)parm);
				break;
			case 2:
				this._Animator_Component.SetInteger(ani, (int)parm);
				break;
			}
		}
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x0010A248 File Offset: 0x00108448
	public void NpcKilled(GamePlayer deathNet)
	{
		bool main = this.Main;
		if (main)
		{
			bool flag = this.Synchronization_Object != null && !(this.Synchronization_Object.heroBase == null);
			if (flag)
			{
				this.Synchronization_Object.heroBase.NpcKill(this);
			}
		}
		else
		{
			HeroPlayer heroplayer = fGameManager.Heroplayer;
			bool flag2 = deathNet.getNetid() == heroplayer.getNetid();
			if (flag2)
			{
				NetObj netObj = GameRoot.SceneSocket.unFsorket.GetNetObj(this.Master_ID);
				bool flag3 = netObj != null && netObj.netobject_type == 1;
				if (flag3)
				{
					GamePlayer gamePlayer = (GamePlayer)netObj;
					if (gamePlayer != null)
					{
						gamePlayer.heroBase.NpcKill(this);
					}
				}
			}
		}
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x0010A300 File Offset: 0x00108500
	public new void SendMsgToAllClient(int nNetID, ArrayList szNetMsg)
	{
		if (nNetID != 51002)
		{
			if (nNetID == 51003)
			{
				this.NpcBuff.DeleteOneBuff((int)szNetMsg[0], (int)szNetMsg[1], (int)szNetMsg[2]);
			}
		}
		else
		{
			this.NpcBuff.AddOneBuff((string)szNetMsg[0], (int)szNetMsg[1], (int)szNetMsg[2]);
		}
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x0010A38C File Offset: 0x0010858C
	private void LoadNpcHeadUi()
	{
		string url = string.Format("EffectRes/{0}.ef", "newplayerui");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), 0);
		}
		else
		{
			this.loadReayPrefab(resWWW, 0);
		}
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x0010A3E8 File Offset: 0x001085E8
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && !this.haveDestroy && !(this.UserHeadTarget == null);
		if (flag)
		{
			GameObject original = (GameObject)www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(this.UserHeadTarget.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
			bool flag2 = this.ShowHp == 1;
			if (flag2)
			{
				GameNpcUiCon gameNpcUiCon = gameObject.AddComponent<GameNpcUiCon>();
				gameNpcUiCon.LoadModeSetUi(this);
			}
			bool flag3 = this.ShowHp == 2;
			if (flag3)
			{
				this.newplayercon = gameObject.AddComponent<NewPlayerTopUICon>();
				this.newplayercon.setUI(this);
			}
		}
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x0010A4EC File Offset: 0x001086EC
	private void CheckNpcJianYing()
	{
		bool flag = this.ShowHp == 2;
		if (flag)
		{
			HeroPlayer player = fGameManager.GetInstance().getPlayer();
			bool flag2 = player.nForce == this.playerCommon.m_nShiliServer;
			if (flag2)
			{
				this.renderBianSe.ChangeHealerSeeThroughMat();
			}
		}
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x0010A53C File Offset: 0x0010873C
	public NetLiving Get_Camera_Switch()
	{
		bool flag = this.camerswitch == 1;
		NetLiving result;
		if (flag)
		{
			result = this;
		}
		else
		{
			NetLiving netLiving = Fun.Get_NetLiving(this.Master_ID);
			bool flag2 = netLiving != null;
			if (flag2)
			{
				result = netLiving;
			}
			else
			{
				result = this;
			}
		}
		return result;
	}

	// Token: 0x04001929 RID: 6441
	public Animator _Animator_Component;

	// Token: 0x0400192A RID: 6442
	private Component _UI_Component;

	// Token: 0x0400192B RID: 6443
	private Component _View_Component;

	// Token: 0x0400192C RID: 6444
	private Component _Controller_Component;

	// Token: 0x0400192D RID: 6445
	private Component _Model_Component;

	// Token: 0x0400192E RID: 6446
	public const int COUNTRY_1 = 11;

	// Token: 0x0400192F RID: 6447
	public const int COUNTRY_2 = 12;

	// Token: 0x04001930 RID: 6448
	private GameObject _Container;

	// Token: 0x04001931 RID: 6449
	private HeroPlayer _Parent;

	// Token: 0x04001932 RID: 6450
	private HeroPlayer _Synchronization_Object;

	// Token: 0x04001933 RID: 6451
	private NetLiving _SynchronizationRB_Object;

	// Token: 0x04001934 RID: 6452
	private Detection_Controller _Detection_Controller_Component;

	// Token: 0x04001935 RID: 6453
	private Npc_Info _Npc_Info_Component;

	// Token: 0x04001936 RID: 6454
	private Rigidbody _Rigidbody_Component;

	// Token: 0x04001937 RID: 6455
	private int _Master_ID;

	// Token: 0x04001938 RID: 6456
	private bool _Main;

	// Token: 0x04001939 RID: 6457
	private int _ID;

	// Token: 0x0400193A RID: 6458
	private int _Index;

	// Token: 0x0400193B RID: 6459
	private int _Type;

	// Token: 0x0400193C RID: 6460
	private string _Resouce_Name;

	// Token: 0x0400193D RID: 6461
	private int _Current_HP;

	// Token: 0x0400193E RID: 6462
	private int _Max_HP;

	// Token: 0x0400193F RID: 6463
	private int _Current_Shield;

	// Token: 0x04001940 RID: 6464
	private int _Max_Shield;

	// Token: 0x04001941 RID: 6465
	private int _Weapon_Index;

	// Token: 0x04001942 RID: 6466
	private float _Detection_Range;

	// Token: 0x04001943 RID: 6467
	private string _Create_Sound;

	// Token: 0x04001944 RID: 6468
	private float _Time;

	// Token: 0x04001945 RID: 6469
	private Vector3 _Position = Vector3.zero;

	// Token: 0x04001946 RID: 6470
	private Vector3 _Rotation = Vector3.zero;

	// Token: 0x04001947 RID: 6471
	private Vector3 _Scale = Vector3.zero;

	// Token: 0x04001948 RID: 6472
	private Vector3 _Target_Position = Vector3.zero;

	// Token: 0x04001949 RID: 6473
	private int _Target_Net_ID = -1;

	// Token: 0x0400194A RID: 6474
	private Vector3 _ST = Vector3.zero;

	// Token: 0x0400194B RID: 6475
	private Vector3 _Force = Vector3.zero;

	// Token: 0x0400194C RID: 6476
	private int _Country;

	// Token: 0x0400194D RID: 6477
	private bool _Death_State;

	// Token: 0x0400194E RID: 6478
	private bool _Gravity;

	// Token: 0x0400194F RID: 6479
	private bool _Can_Buff;

	// Token: 0x04001950 RID: 6480
	private string _Damage_Sound;

	// Token: 0x04001951 RID: 6481
	private int _AI_ID;

	// Token: 0x04001952 RID: 6482
	private bool _Need_Synchronization = true;

	// Token: 0x04001953 RID: 6483
	private string _Death_Effect;

	// Token: 0x04001954 RID: 6484
	private int _Adhere;

	// Token: 0x04001955 RID: 6485
	private bool _Need_Change_Layer;

	// Token: 0x04001956 RID: 6486
	private bool _Can_Shoot = true;

	// Token: 0x04001957 RID: 6487
	private string _Icon;

	// Token: 0x04001958 RID: 6488
	private string _Death_Sound;

	// Token: 0x04001959 RID: 6489
	private int _SignHeight;

	// Token: 0x0400195A RID: 6490
	private int _Showhp;

	// Token: 0x0400195B RID: 6491
	private int _Stunt;

	// Token: 0x0400195C RID: 6492
	private int _Cure;

	// Token: 0x0400195D RID: 6493
	private int _Show_Locked;

	// Token: 0x0400195E RID: 6494
	private bool _Force_Impact;

	// Token: 0x0400195F RID: 6495
	private float _Scale_Multiple;

	// Token: 0x04001960 RID: 6496
	private string _Focus;

	// Token: 0x04001961 RID: 6497
	private int _Physics;

	// Token: 0x04001962 RID: 6498
	private bool _Action_State = true;

	// Token: 0x04001963 RID: 6499
	private float _Mass;

	// Token: 0x04001964 RID: 6500
	private int _camerswitch;

	// Token: 0x04001965 RID: 6501
	private GameObject _Eye_Object;

	// Token: 0x04001966 RID: 6502
	private Npc_AI_Controller _AI_Component;

	// Token: 0x04001967 RID: 6503
	private float _Camera_Distance;

	// Token: 0x04001968 RID: 6504
	private effectMe _npceffectBase;

	// Token: 0x04001969 RID: 6505
	private buffNpc _NpcBuff;
}
