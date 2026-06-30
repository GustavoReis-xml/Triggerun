using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class Skill_Vampire_Deformation_It : SkillBaseIt
{
	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00170074 File Offset: 0x0016E274
	// (set) Token: 0x060029DA RID: 10714 RVA: 0x001700EB File Offset: 0x0016E2EB
	public ArrayList Human_Renderer_List
	{
		get
		{
			bool flag = this._Human_Renderer_List == null || this._Human_Renderer_List.Count == 0;
			if (flag)
			{
				bool flag2 = base.hero != null && base.hero.thisModel != null;
				if (!flag2)
				{
					return new ArrayList();
				}
				this._Human_Renderer_List = new ArrayList();
				this.Get_Human_Renderer_List(null);
			}
			return this._Human_Renderer_List;
		}
		set
		{
			this._Human_Renderer_List = value;
		}
	}

	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x060029DB RID: 10715 RVA: 0x001700F8 File Offset: 0x0016E2F8
	// (set) Token: 0x060029DC RID: 10716 RVA: 0x0017016E File Offset: 0x0016E36E
	public ArrayList Human_Collider_List
	{
		get
		{
			bool flag = this._Human_Collider_List == null || this._Human_Collider_List.Count == 0;
			if (flag)
			{
				bool flag2 = base.hero != null && base.hero.thisModel != null;
				if (!flag2)
				{
					return new ArrayList();
				}
				this._Human_Collider_List = new ArrayList();
				this.Get_Human_Collider_List();
			}
			return this._Human_Collider_List;
		}
		set
		{
			this._Human_Collider_List = value;
		}
	}

	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x060029DD RID: 10717 RVA: 0x00170178 File Offset: 0x0016E378
	// (set) Token: 0x060029DE RID: 10718 RVA: 0x001702F9 File Offset: 0x0016E4F9
	public GameObject Bat_Object
	{
		get
		{
			bool flag = this._Bat_Object == null;
			if (flag)
			{
				bool flag2 = base.hero == null;
				if (flag2)
				{
					return null;
				}
				Transform transform = null;
				bool flag3 = base.hero.userModelTps != null;
				if (flag3)
				{
					Transform[] componentsInChildren = base.hero.userModelTps.GetComponentsInChildren<Transform>(true);
					foreach (Transform transform2 in componentsInChildren)
					{
						bool flag4 = transform2.gameObject.name == "Bat";
						if (flag4)
						{
							transform = transform2;
							break;
						}
					}
				}
				bool flag5 = transform == null && base.hero.thisModel != null;
				if (flag5)
				{
					Transform[] componentsInChildren2 = base.hero.thisModel.GetComponentsInChildren<Transform>(true);
					foreach (Transform transform3 in componentsInChildren2)
					{
						bool flag6 = transform3.gameObject.name == "Bat";
						if (flag6)
						{
							transform = transform3;
							break;
						}
					}
				}
				bool flag7 = transform == null;
				if (flag7)
				{
					return null;
				}
				this._Bat_Object = transform.gameObject;
				bool flag8 = base.hero.UserControl != null;
				if (flag8)
				{
					Fun.Change_Layer(this._Bat_Object, base.hero.UserControl.layer);
				}
			}
			return this._Bat_Object;
		}
		set
		{
			this._Bat_Object = value;
		}
	}

	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x060029DF RID: 10719 RVA: 0x00170304 File Offset: 0x0016E504
	// (set) Token: 0x060029E0 RID: 10720 RVA: 0x0017031C File Offset: 0x0016E51C
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

	// Token: 0x060029E1 RID: 10721 RVA: 0x00170326 File Offset: 0x0016E526
	public override void SkillInit(NetLiving net, int idx)
	{
		base.SkillInit(net, idx);
		((HeroVampireIt)base.hero.heroBase).skillit = this;
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x00170348 File Offset: 0x0016E548
	private void Get_Human_Renderer_List(GameObject _Parent_Object)
	{
		bool flag = base.hero == null || base.hero.thisModel == null;
		if (!flag)
		{
			bool flag2 = this._Human_Renderer_List == null;
			if (flag2)
			{
				this._Human_Renderer_List = new ArrayList();
			}
			this._Human_Renderer_List.Clear();
			Renderer[] componentsInChildren = base.hero.thisModel.GetComponentsInChildren<Renderer>(true);
			foreach (Renderer renderer in componentsInChildren)
			{
				bool flag3 = this.Bat_Object != null && (renderer.gameObject == this.Bat_Object || renderer.transform.IsChildOf(this.Bat_Object.transform));
				if (!flag3)
				{
					bool activeSelf = renderer.gameObject.activeSelf;
					if (activeSelf)
					{
						this._Human_Renderer_List.Add(renderer.gameObject);
					}
				}
			}
		}
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x00170444 File Offset: 0x0016E644
	private void Get_Human_Collider_List()
	{
		bool flag = base.hero == null || base.hero.thisModel == null;
		if (!flag)
		{
			bool flag2 = this._Human_Collider_List == null;
			if (flag2)
			{
				this._Human_Collider_List = new ArrayList();
			}
			this._Human_Collider_List.Clear();
			Collider[] componentsInChildren = base.hero.thisModel.GetComponentsInChildren<Collider>(true);
			foreach (Collider collider in componentsInChildren)
			{
				bool flag3 = this.Bat_Object != null && (collider.gameObject == this.Bat_Object || collider.transform.IsChildOf(this.Bat_Object.transform));
				if (!flag3)
				{
					bool activeSelf = collider.gameObject.activeSelf;
					if (activeSelf)
					{
						this._Human_Collider_List.Add(collider);
					}
				}
			}
		}
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x00170538 File Offset: 0x0016E738
	private void Human_Handle(bool _State)
	{
		int i = 0;
		for (i = 0; i < this.Human_Renderer_List.Count; i++)
		{
			try
			{
				GameObject gameObject = this.Human_Renderer_List[i] as GameObject;
				bool flag = gameObject != null;
				if (flag)
				{
					gameObject.SetActive(_State);
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception in Human_Handle renderer: " + ex.Message);
			}
		}
		for (i = 0; i < this.Human_Collider_List.Count; i++)
		{
			try
			{
				Collider collider = this.Human_Collider_List[i] as Collider;
				bool flag2 = collider != null && collider.gameObject != null;
				if (flag2)
				{
					string tag = collider.gameObject.tag;
					bool flag3 = tag == null || !tag.Contains("can_damage");
					if (flag3)
					{
						collider.enabled = _State;
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.LogWarning("Exception in Human_Handle collider: " + ex2.Message);
			}
		}
		try
		{
			bool flag4 = this.Bat_Object != null;
			if (flag4)
			{
				this.Bat_Object.SetActive(!_State);
			}
		}
		catch (Exception ex3)
		{
			Debug.LogWarning("Exception in Human_Handle bat: " + ex3.Message);
		}
		bool flag5 = (base.hero.isCopy && !base.hero.isDead && CFilmManager.Instance.filmParentNetId(base.hero) == base.hero.getNetid()) || GameRoot.inst.IsGmTarget(base.hero);
		if (flag5)
		{
			bool flag6 = !_State;
			if (flag6)
			{
				base.hero.changeCameraState(CameraState.ThirdState, null, string.Empty);
			}
			else
			{
				base.hero.changeCameraState(CameraState.FirstState, null, string.Empty);
			}
		}
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x0017075C File Offset: 0x0016E95C
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		Transform transform = null;
		bool flag = base.hero.userModelTps != null;
		if (flag)
		{
			Transform[] componentsInChildren = base.hero.userModelTps.GetComponentsInChildren<Transform>(true);
			foreach (Transform transform2 in componentsInChildren)
			{
				bool flag2 = transform2.gameObject.name == "Bat";
				if (flag2)
				{
					transform = transform2;
					break;
				}
			}
		}
		bool flag3 = transform == null && base.hero.thisModel != null;
		if (flag3)
		{
			Transform[] componentsInChildren2 = base.hero.thisModel.GetComponentsInChildren<Transform>(true);
			foreach (Transform transform3 in componentsInChildren2)
			{
				bool flag4 = transform3.gameObject.name == "Bat";
				if (flag4)
				{
					transform = transform3;
					break;
				}
			}
		}
		bool flag5 = transform != null;
		if (flag5)
		{
			AudioSource component = transform.GetComponent<AudioSource>();
			bool flag6 = component != null;
			if (flag6)
			{
				CSoundManager.Instance.SetEffectSoundMixer(component, 0, null, 20);
			}
		}
		this.Deformation_State = true;
		this.Human_Handle(false);
		base.OneSkillAction(__target, parm2);
	}

	// Token: 0x060029E6 RID: 10726 RVA: 0x001708A0 File Offset: 0x0016EAA0
	public override void OneSkillActionUpdate(ArrayList _Parameters)
	{
		bool flag = int.Parse(_Parameters[2].ToString()) >= 1;
		if (flag)
		{
			this.OneSkillFinish();
		}
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x001708D2 File Offset: 0x0016EAD2
	public override void OneSkillFinish()
	{
		this.Human_Handle(true);
		this.Deformation_State = false;
		base.OneSkillFinish();
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x001708EC File Offset: 0x0016EAEC
	private void Update()
	{
		bool flag = !(this.Bat_Object == null) && this.Bat_Object.activeSelf;
		if (flag)
		{
			this.Bat_Object.transform.LookAt(base.hero.playerTarget.transform);
		}
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x00170940 File Offset: 0x0016EB40
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		bool animatorBool = base.hero.GetAnimatorBool("Skill_2");
		if (animatorBool)
		{
			this.Deformation_State = true;
			this.Human_Handle(false);
		}
	}

	// Token: 0x0400234C RID: 9036
	private ArrayList _Human_Renderer_List;

	// Token: 0x0400234D RID: 9037
	private ArrayList _Human_Collider_List;

	// Token: 0x0400234E RID: 9038
	private GameObject _Bat_Object;

	// Token: 0x0400234F RID: 9039
	private bool _Deformation_State;
}
