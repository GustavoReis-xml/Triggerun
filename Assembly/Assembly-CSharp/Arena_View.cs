using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001F RID: 31
public class Arena_View : MonoBehaviour
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000136 RID: 310 RVA: 0x000071D8 File Offset: 0x000053D8
	// (set) Token: 0x06000137 RID: 311 RVA: 0x00007236 File Offset: 0x00005436
	public Arena_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<Arena_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<Arena_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000138 RID: 312 RVA: 0x00007240 File Offset: 0x00005440
	// (set) Token: 0x06000139 RID: 313 RVA: 0x00007258 File Offset: 0x00005458
	public float Countdown
	{
		get
		{
			return this._Countdown;
		}
		set
		{
			this._Countdown = value;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x0600013A RID: 314 RVA: 0x00007264 File Offset: 0x00005464
	// (set) Token: 0x0600013B RID: 315 RVA: 0x0000729E File Offset: 0x0000549E
	public GameObject Area_Effect
	{
		get
		{
			bool flag = this._Area_Effect == null;
			if (flag)
			{
				this._Area_Effect = GameObject.Find("duqi");
			}
			return this._Area_Effect;
		}
		set
		{
			this._Area_Effect = value;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600013C RID: 316 RVA: 0x000072A8 File Offset: 0x000054A8
	// (set) Token: 0x0600013D RID: 317 RVA: 0x00007302 File Offset: 0x00005502
	public List<Image> Icon_Image_Component_List_1
	{
		get
		{
			bool flag = this._Icon_Image_Component_List_1 == null;
			if (flag)
			{
				this._Icon_Image_Component_List_1 = new List<Image>
				{
					null,
					null,
					null,
					null,
					null
				};
			}
			return this._Icon_Image_Component_List_1;
		}
		set
		{
			this._Icon_Image_Component_List_1 = value;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600013E RID: 318 RVA: 0x0000730C File Offset: 0x0000550C
	// (set) Token: 0x0600013F RID: 319 RVA: 0x00007366 File Offset: 0x00005566
	public List<Image> Icon_Image_Component_List_2
	{
		get
		{
			bool flag = this._Icon_Image_Component_List_2 == null;
			if (flag)
			{
				this._Icon_Image_Component_List_2 = new List<Image>
				{
					null,
					null,
					null,
					null,
					null
				};
			}
			return this._Icon_Image_Component_List_2;
		}
		set
		{
			this._Icon_Image_Component_List_2 = value;
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007370 File Offset: 0x00005570
	public void Initialize()
	{
		this.Initialize_Area_Effect();
		this.UI_Component.Center_Background.SetActive(false);
		this.Display_A_Victory_Number(4, "shenglidi");
		this.Display_B_Victory_Number(4, "shenglidi");
		this.Initialize_List(this.UI_Component.A_User_List);
		this.Initialize_List(this.UI_Component.B_User_List);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000073D5 File Offset: 0x000055D5
	public void Revert_Area_Open()
	{
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000073D8 File Offset: 0x000055D8
	public void Revert_Area_Close()
	{
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000073DC File Offset: 0x000055DC
	public void Display_Prepare_State_Describe(bool _Value)
	{
		this.UI_Component.Effect_Object.SetActive(_Value);
		bool flag = !_Value;
		if (flag)
		{
			this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.zero, 1f);
		}
		else
		{
			this.UI_Component.Prepare_Describe_Text.transform.DOScale(Vector3.one, 0f);
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000744C File Offset: 0x0000564C
	public void Display_Fight_State_Describe(string _Value)
	{
		this.UI_Component.Fight_Describe_Text.text = _Value;
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.zero, 0f);
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.one, 1f);
		this.UI_Component.Effect_Object.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_State_Describe());
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000074CB File Offset: 0x000056CB
	public IEnumerator Wait_Hidden_Fight_State_Describe()
	{
		yield return new WaitForSeconds(4f);
		this.UI_Component.Fight_Describe_Text.transform.DOScale(Vector3.zero, 1f);
		this.UI_Component.Effect_Object.SetActive(false);
		yield break;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000074DC File Offset: 0x000056DC
	private void Initialize_List(GameObject _Parent)
	{
		foreach (object obj in _Parent.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00007540 File Offset: 0x00005740
	public void Display_Fight_Data(bool _Fight_State, ArrayList _A_User_Data, ArrayList _B_User_Data, int _Current_Round_Number, int _A_Victory_Round_Number, int _B_Victory_Round_Number)
	{
		this.Display_User_Data(_A_User_Data, _B_User_Data, _Fight_State);
		this.Display_Socre(_A_Victory_Round_Number, _B_Victory_Round_Number);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00007558 File Offset: 0x00005758
	public void Display_User_Data(ArrayList _A_User_Data, ArrayList _B_User_Data, bool _Fight_State)
	{
		bool flag = PlayerDataManager.getInstance().playerCommon.m_nShili == 1;
		if (flag)
		{
			this.Display_User_Data(_A_User_Data, this.UI_Component.A_User_List, _Fight_State, true, this.Icon_Image_Component_List_1);
			this.Display_User_Data(_B_User_Data, this.UI_Component.B_User_List, _Fight_State, false, this.Icon_Image_Component_List_2);
		}
		else
		{
			bool flag2 = PlayerDataManager.getInstance().playerCommon.m_nShili == 2;
			if (flag2)
			{
				this.Display_User_Data(_B_User_Data, this.UI_Component.A_User_List, _Fight_State, true, this.Icon_Image_Component_List_1);
				this.Display_User_Data(_A_User_Data, this.UI_Component.B_User_List, _Fight_State, false, this.Icon_Image_Component_List_2);
			}
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007604 File Offset: 0x00005804
	private void Display_User_Data(ArrayList _Parameters, GameObject _Parent, bool _Fight_State, bool _Display_Icon_State, List<Image> _Icon_Image_Component_List)
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = _Parent.transform.FindChild((i + 1).ToString()).gameObject;
			Image image = (!(_Icon_Image_Component_List[i] == null)) ? _Icon_Image_Component_List[i] : (_Icon_Image_Component_List[i] = gameObject.transform.FindChild("Icon").GetComponent<Image>());
			bool flag = i >= _Parameters.Count;
			if (flag)
			{
				gameObject.SetActive(false);
			}
			else
			{
				gameObject.SetActive(true);
				Arena_User_Entity arena_User_Entity = (Arena_User_Entity)_Parameters[i];
				image.enabled = true;
				bool flag2 = arena_User_Entity.State > 0;
				if (flag2)
				{
					bool flag3 = arena_User_Entity.GamePlayer_Object != null && arena_User_Entity.GamePlayer_Object.playerCommon != null && arena_User_Entity.GamePlayer_Object.playerCommon.m_nModelIndex > 0;
					if (flag3)
					{
						bool fight_State = ArenaCon.inst.Fight_State;
						if (fight_State)
						{
							image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, fGameManager.mloadConfig.AllHeroUnlock[arena_User_Entity.GamePlayer_Object.playerCommon.m_nModelIndex].szSkinUnlock[1].sIconName);
						}
						else if (_Display_Icon_State)
						{
							image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, fGameManager.mloadConfig.AllHeroUnlock[arena_User_Entity.GamePlayer_Object.playerCommon.m_nModelIndex].szSkinUnlock[1].sIconName);
						}
						else
						{
							image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, "wenhao");
						}
					}
				}
				else
				{
					image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, "siwang");
				}
			}
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000077E8 File Offset: 0x000059E8
	public void Display_Socre(int _A_Victory_Round_Number, int _B_Victory_Round_Number)
	{
		bool flag = PlayerDataManager.getInstance().playerCommon.m_nShili == 1;
		if (flag)
		{
			this.Display_A_Victory_Number(_A_Victory_Round_Number, "shengli");
			this.UI_Component.A_Score_Image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _A_Victory_Round_Number.ToString());
			this.Display_B_Victory_Number(_B_Victory_Round_Number, "shengli");
			this.UI_Component.B_Score_Image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _B_Victory_Round_Number.ToString());
		}
		else
		{
			this.Display_A_Victory_Number(_B_Victory_Round_Number, "shengli");
			this.UI_Component.A_Score_Image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _B_Victory_Round_Number.ToString());
			this.Display_B_Victory_Number(_A_Victory_Round_Number, "shengli");
			this.UI_Component.B_Score_Image.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _A_Victory_Round_Number.ToString());
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000078D0 File Offset: 0x00005AD0
	public void Display_A_Victory_Number(int _Number, string _Sprite_Name)
	{
		for (int i = 0; i < _Number; i++)
		{
			Image component = this.UI_Component.A_Victory_List.transform.Find((i + 1).ToString()).GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _Sprite_Name);
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000792C File Offset: 0x00005B2C
	public void Display_B_Victory_Number(int _Number, string _Sprite_Name)
	{
		for (int i = 0; i < _Number; i++)
		{
			Image component = this.UI_Component.B_Victory_List.transform.Find((i + 1).ToString()).GetComponent<Image>();
			component.sprite = Window.spriteName(AtlasType.ATLAS_ARENA, _Sprite_Name);
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00007988 File Offset: 0x00005B88
	public void Display_Countdown(int _Number)
	{
		bool flag = _Number < 0;
		if (flag)
		{
			this.UI_Component.Remaining_Time_Text.text = Language.GetLang(150002, new object[0]);
		}
		else
		{
			this.UI_Component.Remaining_Time_Text.text = Fun.sencondsToHMS_CXP(_Number);
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000079DC File Offset: 0x00005BDC
	public void Display_Countdown_Describe(int _Number)
	{
		bool flag = _Number > 0 && _Number <= 5;
		if (flag)
		{
			this.UI_Component.Countdown_Effect.transform.Find(_Number.ToString()).gameObject.SetActive(true);
		}
		else
		{
			bool flag2 = _Number == 0;
			if (flag2)
			{
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.one, 0f);
				this.UI_Component.Describe_Text.gameObject.SetActive(true);
				this.UI_Component.Describe_Text.text = Language.GetLang(150001, new object[0]);
				this.UI_Component.Describe_Text.transform.DOScale(Vector3.zero, 1f);
				this.UI_Component.Countdown_Effect.transform.Find("1").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("2").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("3").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("4").gameObject.SetActive(false);
				this.UI_Component.Countdown_Effect.transform.Find("5").gameObject.SetActive(false);
			}
			else
			{
				this.UI_Component.Describe_Text.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007B8C File Offset: 0x00005D8C
	public void Display_Fight_Result(bool _State)
	{
		GameObject gameObject = (!_State) ? this.UI_Component.Failure_Effect : this.UI_Component.Victory_Effect;
		gameObject.SetActive(true);
		base.StartCoroutine(this.Wait_Hidden_Fight_Result(gameObject));
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00007BCE File Offset: 0x00005DCE
	private IEnumerator Wait_Hidden_Fight_Result(GameObject _Parent)
	{
		yield return new WaitForSeconds(3f);
		_Parent.SetActive(false);
		yield break;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00007BE4 File Offset: 0x00005DE4
	public void Narrow_Area(int _State)
	{
		bool flag = _State < 1;
		if (flag)
		{
			this.Initialize_Area_Effect();
		}
		else
		{
			bool flag2 = _State <= 4;
			if (flag2)
			{
				this.Area_Effect.SetActive(true);
				for (int i = 1; i <= _State; i++)
				{
					this.Area_Effect.transform.Find("zong/" + i.ToString()).gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007C64 File Offset: 0x00005E64
	public void Initialize_Area_Effect()
	{
		this.Area_Effect.transform.Find("zong/1").gameObject.SetActive(false);
		this.Area_Effect.transform.Find("zong/2").gameObject.SetActive(false);
		this.Area_Effect.transform.Find("zong/3").gameObject.SetActive(false);
		this.Area_Effect.transform.Find("zong/4").gameObject.SetActive(false);
		this.Area_Effect.SetActive(false);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00007D03 File Offset: 0x00005F03
	public void Round_End()
	{
	}

	// Token: 0x040000E0 RID: 224
	private Arena_UI _UI_Component;

	// Token: 0x040000E1 RID: 225
	private float _Countdown;

	// Token: 0x040000E2 RID: 226
	private GameObject _Area_Effect;

	// Token: 0x040000E3 RID: 227
	private List<Image> _Icon_Image_Component_List_1;

	// Token: 0x040000E4 RID: 228
	private List<Image> _Icon_Image_Component_List_2;
}
