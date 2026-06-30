using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class Skill_Vampire_Deformation_Me : SkillBaseMe
{
	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x060029EB RID: 10731 RVA: 0x00170984 File Offset: 0x0016EB84
	// (set) Token: 0x060029EC RID: 10732 RVA: 0x001709FB File Offset: 0x0016EBFB
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

	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x060029ED RID: 10733 RVA: 0x00170A08 File Offset: 0x0016EC08
	// (set) Token: 0x060029EE RID: 10734 RVA: 0x00170A7E File Offset: 0x0016EC7E
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

	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x060029EF RID: 10735 RVA: 0x00170A88 File Offset: 0x0016EC88
	// (set) Token: 0x060029F0 RID: 10736 RVA: 0x00170C09 File Offset: 0x0016EE09
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

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00170C14 File Offset: 0x0016EE14
	// (set) Token: 0x060029F2 RID: 10738 RVA: 0x00170C2C File Offset: 0x0016EE2C
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

	// Token: 0x060029F3 RID: 10739 RVA: 0x00170C38 File Offset: 0x0016EE38
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

	// Token: 0x060029F4 RID: 10740 RVA: 0x00170D34 File Offset: 0x0016EF34
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

	// Token: 0x060029F5 RID: 10741 RVA: 0x00170E28 File Offset: 0x0016F028
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
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x00170FBC File Offset: 0x0016F1BC
	public override bool OneSkillCanDo()
	{
		bool superDoing = ((HeroVampireMe)base.hero.heroBase).superDoing;
		return !superDoing && base.OneSkillCanDo();
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x00170FF4 File Offset: 0x0016F1F4
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.hero.SetAnimator("Skill_2", 0, true, 2);
		base.hero.playerZhunxingMe.zhunxingshowOrHide(false);
		base.hero.heroData.fTPSCameraDis += GlobalDefine.Vampire_Bat_Camera_Distance;
		CameraControl.Instance.tpsHeight = new Vector3(CameraControl.Instance.tpsHeight.x + GlobalDefine.Vampire_Bat_Camera_Position.x, CameraControl.Instance.tpsHeight.y + GlobalDefine.Vampire_Bat_Camera_Position.y, CameraControl.Instance.tpsHeight.z + GlobalDefine.Vampire_Bat_Camera_Position.z);
		((CWeaponControlMe)this._hero.wc).cancelFillCartridge();
		base.hero.changeCameraState(CameraState.ThirdState, null, "playeraction");
		this.Deformation_State = true;
		base.hero.wInput.notInput = true;
		this.transformTime = Time.time;
		this.Human_Handle(false);
		base.StartCoroutine(this.Wait_Skill_Finish());
		base.hero.tChar.onAir = true;
		((TBCharacterThirdMe)base.hero.tChar).VelocitySetZero(false);
		base.hero.bePlayActive = false;
		base.OneSkillAction(__target, parm2);
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x0017114B File Offset: 0x0016F34B
	private IEnumerator Wait_Skill_Finish()
	{
		float duration = 3f;
		bool flag = base.heroSkillData != null;
		if (flag)
		{
			duration = (float)base.heroSkillData.m_nSKLongTime / 1000f;
			bool flag2 = duration <= 0f;
			if (flag2)
			{
				duration = 3f;
			}
		}
		yield return new WaitForSeconds(duration);
		this.OneSkillFinish();
		yield break;
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x0017115C File Offset: 0x0016F35C
	public override void OneSkillFinish()
	{
		bool deformation_State = this.Deformation_State;
		if (deformation_State)
		{
			this.Send_Messages(1);
			base.hero.tChar.notVelocity = false;
			((TBCharacterThirdMe)base.hero.tChar).MoveDirectionByVector(false, Vector3.zero);
			base.hero.heroData.fTPSCameraDis -= GlobalDefine.Vampire_Bat_Camera_Distance;
			CameraControl.Instance.tpsHeight = new Vector3(CameraControl.Instance.tpsHeight.x - GlobalDefine.Vampire_Bat_Camera_Position.x, CameraControl.Instance.tpsHeight.y - GlobalDefine.Vampire_Bat_Camera_Position.y, CameraControl.Instance.tpsHeight.z - GlobalDefine.Vampire_Bat_Camera_Position.z);
			base.hero.changeCameraState(CameraState.FirstState, null, "playeraction");
			base.hero.wInput.notInput = false;
			this.Human_Handle(true);
			base.hero.tChar.onAir = false;
			base.hero.bePlayActive = true;
			this.Deformation_State = false;
			base.hero.playerZhunxingMe.zhunxingshowOrHide(true);
			((CWeaponControlMe)base.hero.wc).CheckMustFillCartridge();
			base.OneSkillFinish();
			base.hero.SetAnimator("Skill_2", 0, false, 2);
			this.SkillSoundStopAll();
		}
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x001712D0 File Offset: 0x0016F4D0
	private void Update()
	{
		bool deformation_State = this.Deformation_State;
		if (deformation_State)
		{
			base.hero.tChar.notVelocity = true;
			((TBCharacterThirdMe)base.hero.tChar).MoveDirectionByVector(true, base.hero.charMove.state.moveAir * GlobalDefine.Vampire_Forward_Speed);
			bool flag = this.Bat_Object != null;
			if (flag)
			{
				this.Bat_Object.transform.forward = CameraControl.Instance.transform.forward;
			}
			bool flag2 = Time.time - this.transformTime > 0.5f && Input_Manager.Get_Button_Down("WeaponSec");
			if (flag2)
			{
				base.StopAllCoroutines();
				this.OneSkillFinish();
			}
		}
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x0017139C File Offset: 0x0016F59C
	private void Send_Messages(int _State)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(base.hero.getNetid());
		arrayList.Add(base.iSkillIndex);
		arrayList.Add(_State);
		base.hero.SendSkillParms(arrayList);
	}

	// Token: 0x04002350 RID: 9040
	private float transformTime;

	// Token: 0x04002351 RID: 9041
	private ArrayList _Human_Renderer_List;

	// Token: 0x04002352 RID: 9042
	private ArrayList _Human_Collider_List;

	// Token: 0x04002353 RID: 9043
	private GameObject _Bat_Object;

	// Token: 0x04002354 RID: 9044
	private bool _Deformation_State;
}
