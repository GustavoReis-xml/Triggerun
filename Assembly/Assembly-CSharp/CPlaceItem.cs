using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class CPlaceItem : MonoBehaviour
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x0600067D RID: 1661 RVA: 0x000370AC File Offset: 0x000352AC
	// (set) Token: 0x0600067E RID: 1662 RVA: 0x000370C4 File Offset: 0x000352C4
	public bool beReady
	{
		get
		{
			return this.b_HasTransPos;
		}
		set
		{
			this.b_HasTransPos = value;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x0600067F RID: 1663 RVA: 0x000370CE File Offset: 0x000352CE
	public bool canReleaseOnAir
	{
		get
		{
			return this.releaseOnAir;
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000680 RID: 1664 RVA: 0x000370D6 File Offset: 0x000352D6
	public Vector3 ColliderSize
	{
		get
		{
			return this.boxSize;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000681 RID: 1665 RVA: 0x000370E0 File Offset: 0x000352E0
	public int HitNetId
	{
		get
		{
			bool flag = this.hitNpc.collider != null;
			int result;
			if (flag)
			{
				GameObject gameObject = this.hitNpc.collider.transform.root.gameObject;
				PublicInfo component = gameObject.GetComponent<PublicInfo>();
				bool flag2 = component != null;
				if (flag2)
				{
					NetLiving netObject = component.NetObject;
					result = netObject.getNetid();
				}
				else
				{
					result = -1;
				}
			}
			else
			{
				result = -1;
			}
			return result;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000682 RID: 1666 RVA: 0x00037151 File Offset: 0x00035351
	public Transform Transs
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000683 RID: 1667 RVA: 0x00037159 File Offset: 0x00035359
	public Transform HitObjTrans
	{
		get
		{
			return this._hitObjTrans;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00037164 File Offset: 0x00035364
	public void InitPlace(Transform trs, int type, float dis)
	{
		this.NewTransAsThisTrans();
		this._Trans = trs;
		this._Trans.transform.parent = this.ThisGameObjTrans.transform;
		this._Trans.localPosition = Vector3.zero;
		this.placeType = type;
		this.fDisPlace = dis;
		this.ray1Count = 60;
		this.b_HasTransPos = false;
		this.g = new GameObject();
		this.hideItem(0);
		this.layer = (1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("force1_npc") | 1 << LayerMask.NameToLayer("force2_npc") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2") | 1 << LayerMask.NameToLayer("Chariot") | 1 << LayerMask.NameToLayer("BuffItem"));
		bool flag = type != 0;
		if (flag)
		{
			this.hong = trs.Find("hon").gameObject;
			this.lan = trs.Find("lan").gameObject;
			this.boxSize = trs.Find("size").transform.localScale;
			this.releaseOnAir = true;
		}
		else
		{
			this.boxSize = trs.Find("size").transform.localScale + new Vector3(0.5f, -1.6f, 0.5f);
			this.releaseOnAir = false;
		}
		this.changeHeroDes = false;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00037300 File Offset: 0x00035500
	private void NewTransAsThisTrans()
	{
		this.ThisGameObjTrans = new GameObject();
		this.ThisGameObjTrans.transform.SetParent(this.net._playerSoundContainer.transform, true);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00037330 File Offset: 0x00035530
	private void LateUpdate()
	{
		bool flag = CameraControl.Instance.controlCamera == null || this.net == null || CameraControl.Instance.nowTarget == null;
		if (!flag)
		{
			bool flag2 = this.g == null;
			if (flag2)
			{
				this.NewTransAsThisTrans();
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PrepareItem, base.transform.position, Quaternion.identity);
				this._Trans = gameObject.transform;
				this._Trans.transform.parent = this.ThisGameObjTrans.transform;
				this._Trans.localPosition = Vector3.zero;
				this.g = new GameObject();
				bool flag3 = this.placeType != 0;
				if (flag3)
				{
					this.hong = gameObject.transform.Find("hon").gameObject;
					this.lan = gameObject.transform.Find("lan").gameObject;
					this.boxSize = gameObject.transform.Find("size").transform.localScale;
					this.releaseOnAir = true;
				}
			}
			this.ShexianWeiZhie = CameraControl.Instance.nowTarget.transform.position;
			this.ray = new Ray(this.ShexianWeiZhie, CameraControl.Instance.nowTarget.forward);
			Physics.Raycast(this.ray, out this.hitt, this.fDisPlace, this.layer);
			bool flag4 = (this.placeType == 0 || this.placeType == 1) && MatchManager.GetInstance().m_pFbData.nType != 10;
			if (flag4)
			{
				this.RayHit();
			}
			bool flag5 = this.placeType != 2 && this.placeType != 3 && this.placeType != 4 && MatchManager.GetInstance().m_pFbData.nType != 10;
			if (!flag5)
			{
				float num = 0f;
				Vector3 forward = CameraControl.Instance.transform.forward;
				forward.y = 0f;
				Vector3 direction = this.ray.direction;
				while (num < 0.2f && (Vector3.Angle(this.lastHitRayDir, direction) != 0f || Vector3.Distance(this.ShexianWeiZhie, this.lastSheXianpos) != 0f))
				{
					Vector3 normalized = Vector3.Lerp(direction, forward, num).normalized;
					num += 0.02f;
					Debug.DrawRay(this.ShexianWeiZhie, normalized * 5f, Color.red);
					bool flag6 = Physics.Raycast(this.ShexianWeiZhie, normalized, out this.hitt, this.fDisPlace, this.layer);
					if (flag6)
					{
						bool flag7 = Mathf.Abs(this.hitt.normal.y) > this.angle;
						if (flag7)
						{
							this.g.transform.parent = null;
							this.g.transform.localScale = Vector3.one;
							this.g.transform.transform.rotation = this.hitt.collider.gameObject.transform.rotation;
							this.g.transform.transform.parent = this.hitt.collider.gameObject.transform;
							this._hitObjTrans = this.hitt.collider.gameObject.transform;
							this.ThisGameObjTrans.transform.parent = this.g.transform;
							this.ThisGameObjTrans.transform.position = this.hitt.point;
							this.ThisGameObjTrans.transform.up = Vector3.up * Mathf.Sign(this.hitt.normal.y);
							bool flag8 = (this.placeType == 4 || Mathf.Sign(this.hitt.normal.y) >= 0f) && (this.placeType == 0 || Mathf.Sign(this.hitt.normal.y) >= 0f);
							if (flag8)
							{
								bool flag9 = this.HasBoxNearBy(this.hitt, 0);
								if (flag9)
								{
									this.setPlaceItemTrans(true, this.hitt, Vector3.zero);
									this.hideItem(this.placeType);
									this.b_HasTransPos = false;
								}
								else
								{
									bool flag10 = this.CheckIfPlaceItem(this.hitt.collider.tag, this.hitt.collider.name);
									if (flag10)
									{
										this.setPlaceItemTrans(true, this.hitt, Vector3.zero);
										this.hideItem(this.placeType);
										this.b_HasTransPos = false;
									}
									else
									{
										this.hitNpc = this.hitt;
										this.showItem(this.placeType);
										this.lastHitRayDir = this.ray.direction;
										this.lastSheXianpos = this.ShexianWeiZhie;
										this.b_HasTransPos = true;
									}
								}
							}
						}
						else
						{
							bool flag11 = this.hitt.normal.y < 0.1f && this.hitt.normal.y > -0.5f && !this.b_HasTransPos && this.placeType != 0;
							if (flag11)
							{
								this.g.transform.parent = null;
								this.g.transform.localScale = Vector3.one;
								this.g.transform.transform.rotation = this.hitt.collider.gameObject.transform.rotation;
								this.g.transform.transform.parent = this.hitt.collider.gameObject.transform;
								this._hitObjTrans = this.hitt.collider.gameObject.transform;
								this.ThisGameObjTrans.transform.parent = this.g.transform;
								this.ThisGameObjTrans.transform.position = this.hitt.point;
								this.ThisGameObjTrans.transform.forward = this.hitt.normal;
								this.ThisGameObjTrans.transform.Rotate(Vector3.right, 90f);
								bool flag12 = this.HasBoxNearBy(this.hitt, 1);
								if (flag12)
								{
									this.setPlaceItemTrans(true, this.hitt, Vector3.zero);
									this.hideItem(this.placeType);
									this.b_HasTransPos = false;
								}
								else
								{
									bool flag13 = this.CheckIfPlaceItem(this.hitt.collider.tag, this.hitt.collider.name);
									if (flag13)
									{
										this.setPlaceItemTrans(true, this.hitt, Vector3.zero);
										this.hideItem(this.placeType);
										this.b_HasTransPos = false;
									}
									else
									{
										this.hitNpc = this.hitt;
										this.showItem(this.placeType);
										this.lastHitRayDir = this.ray.direction;
										this.lastSheXianpos = this.ShexianWeiZhie;
										this.b_HasTransPos = true;
									}
								}
							}
							else
							{
								bool flag14 = this.placeType != 0;
								if (flag14)
								{
									this.setPlaceItemTrans(true, this.hitt, Vector3.zero);
								}
								this.hideItem(this.placeType);
								this.b_HasTransPos = false;
							}
						}
					}
					else
					{
						this.setPlaceItemTrans(false, this.hitt, this.ShexianWeiZhie + normalized * this.fDisPlace);
						this.hideItem(this.placeType);
						this.b_HasTransPos = false;
					}
				}
			}
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00037B28 File Offset: 0x00035D28
	private void showItem(int type)
	{
		this.net.playerZhunxingMe.zhunxingshowOrHide(false);
		bool flag = type == 0;
		if (flag)
		{
			bool flag2 = !this._Trans.gameObject.activeSelf;
			if (flag2)
			{
				this._Trans.gameObject.SetActive(true);
				this._Trans.localRotation = Quaternion.identity;
			}
		}
		else
		{
			bool flag3 = !this._Trans.gameObject.activeSelf;
			if (flag3)
			{
				this._Trans.localRotation = Quaternion.identity;
				this._Trans.gameObject.SetActive(true);
			}
			bool activeSelf = this.hong.gameObject.activeSelf;
			if (activeSelf)
			{
				this._Trans.localRotation = Quaternion.identity;
				this.hong.SetActive(false);
			}
			bool flag4 = !this.lan.gameObject.activeSelf;
			if (flag4)
			{
				this._Trans.localRotation = Quaternion.identity;
				this.lan.SetActive(true);
			}
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00037C3C File Offset: 0x00035E3C
	private void hideItem(int type)
	{
		this.g.transform.SetParent(this.net._playerSoundContainer.transform, true);
		base.transform.SetParent(this.net._playerSoundContainer.transform, true);
		bool flag = type == 0;
		if (flag)
		{
			bool activeSelf = this._Trans.gameObject.activeSelf;
			if (activeSelf)
			{
				this._Trans.localRotation = Quaternion.identity;
				this._Trans.gameObject.SetActive(false);
			}
		}
		else
		{
			this._Trans.up = Vector3.up;
			this.net.playerZhunxingMe.zhunxingshowOrHide(false);
			bool flag2 = !this._Trans.gameObject.activeSelf;
			if (flag2)
			{
				this._Trans.gameObject.SetActive(true);
			}
			bool flag3 = !this.hong.gameObject.activeSelf;
			if (flag3)
			{
				this.hong.SetActive(true);
			}
			bool activeSelf2 = this.lan.gameObject.activeSelf;
			if (activeSelf2)
			{
				this.lan.SetActive(false);
			}
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00037D6C File Offset: 0x00035F6C
	private void RayHit()
	{
		bool flag = Physics.Raycast(this.ShexianWeiZhie, this.ray.direction, out this.hitt, this.fDisPlace, this.layer);
		if (flag)
		{
			bool flag2 = this.hitt.normal.y > this.angle;
			if (flag2)
			{
				Debug.DrawRay(this.ShexianWeiZhie, this.ray.direction * 100f, Color.black);
				bool flag3 = Physics.Raycast(this.hitt.point, Vector3.up, 2f, this.layer);
				Debug.DrawRay(this.hitt.point, Vector3.up * 2f);
				Vector3 f_dir = Quaternion.AngleAxis(90f, CameraControl.Instance.transform.right) * this.hitt.normal;
				bool flag4 = !flag3 && this.CanStandOnIt(this.hitt.point, f_dir, CameraControl.Instance.transform.right) && !this.CheckIfPlaceItem(this.hitt.collider.tag, this.hitt.collider.name) && !this.HasBoxNearBy(this.hitt, 2);
				if (flag4)
				{
					Debug.DrawRay(this.hitt.point, Vector3.up, Color.blue);
					this.g.transform.parent = null;
					this.g.transform.localScale = Vector3.one;
					this.g.transform.transform.rotation = this.hitt.collider.gameObject.transform.rotation;
					this.g.transform.transform.parent = this.hitt.collider.gameObject.transform;
					this._hitObjTrans = this.hitt.collider.gameObject.transform;
					this.ThisGameObjTrans.transform.parent = this.g.transform;
					this.ThisGameObjTrans.transform.position = this.hitt.point;
					this.ThisGameObjTrans.transform.forward = CameraControl.Instance.transform.forward;
					this.ThisGameObjTrans.transform.up = Vector3.up;
					this.ThisGameObjTrans.transform.right = CameraControl.Instance.transform.right;
					this.getPos = true;
					this.b_HasTransPos = true;
					this.hitNpc = this.hitt;
				}
				else
				{
					this.b_HasTransPos = false;
					this.getPos = true;
				}
			}
			else
			{
				this.getPos = false;
			}
		}
		else
		{
			this.getPos = false;
		}
		bool flag5 = !this.getPos;
		if (flag5)
		{
			Vector3 vector = Vector3.zero;
			bool flag6 = Physics.Raycast(this.ShexianWeiZhie, this.ray.direction, out this.QiangHit, this.fDisPlace, this.layer);
			bool flag7 = flag6;
			if (flag7)
			{
				Debug.DrawRay(this.ShexianWeiZhie, this.ray.direction * Vector3.Distance(this.QiangHit.point, this.ShexianWeiZhie), Color.yellow);
				vector = this.QiangHit.point;
				this.getPos = false;
				this.placePos = Vector3.zero;
				this.lastRay1Pos = vector;
				this.lastRay1Pos += this.QiangHit.normal * 0.5f;
				this.HitWallNormal = this.QiangHit.normal;
			}
			for (int i = 0; i < this.ray1Count; i++)
			{
				bool flag8 = this.getPos;
				if (!flag8)
				{
					bool flag9 = !flag6;
					if (flag9)
					{
						vector = new Vector3(this.ShexianWeiZhie.x, this.ShexianWeiZhie.y - (float)(i + 1) * 0.15f, this.ShexianWeiZhie.z);
						vector += this.ray.direction * this.fDisPlace;
					}
					else
					{
						vector = new Vector3(vector.x, vector.y - (float)(i + 1) * 0.15f, vector.z);
					}
					bool flag10 = Physics.Raycast(vector, -this.ray.direction, out this.hit, this.fDisPlace, this.layer);
					if (flag10)
					{
						Debug.DrawRay(vector, -this.ray.direction * Vector3.Distance(this.hit.point, vector), Color.black);
						bool flag11 = this.hit.point.y > this.ShexianWeiZhie.y;
						if (flag11)
						{
							bool flag12 = this.hit.normal.y <= this.angle;
							if (!flag12)
							{
								float num = Vector3.Angle(CameraControl.Instance.transform.forward, Vector3.up);
								Vector3 f_dir2 = Quaternion.AngleAxis(90f - num, CameraControl.Instance.transform.right) * -CameraControl.Instance.transform.forward;
								bool flag13 = this.placePos.y < this.hit.point.y;
								if (flag13)
								{
									bool flag14 = !this.CanStandOnIt(this.hit.point, f_dir2, CameraControl.Instance.transform.right) || this.CheckIfPlaceItem(this.hit.collider.tag, this.hit.collider.name) || this.HasBoxNearBy(this.hit, 2);
									if (flag14)
									{
										break;
									}
									this.placePos = this.hit.point;
									this.lastRay1Pos = Vector3.zero;
									this.PlaceHit = this.hit;
									this.placePos = Vector3.zero;
									this.getPos = true;
								}
							}
						}
						else
						{
							this.placePos = Vector3.zero;
							bool flag15 = !flag6;
							if (flag15)
							{
								this.lastRay1Pos = this.ShexianWeiZhie + this.ray.direction * this.fDisPlace;
								this.lastRay1Pos += -CameraControl.Instance.transform.forward * 0.5f;
								float num2 = Vector3.Angle(CameraControl.Instance.transform.forward, Vector3.up);
								this.HitWallNormal = Quaternion.AngleAxis(90f - num2, CameraControl.Instance.transform.right) * -CameraControl.Instance.transform.forward;
								Debug.DrawRay(this.lastRay1Pos, this.HitWallNormal * 5f, Color.magenta);
							}
							this.getPos = false;
						}
					}
					else
					{
						this.placePos = Vector3.zero;
						bool flag16 = !flag6;
						if (flag16)
						{
							this.lastRay1Pos = this.ShexianWeiZhie + this.ray.direction * this.fDisPlace;
							this.lastRay1Pos += -CameraControl.Instance.transform.forward * 0.5f;
							float num3 = Vector3.Angle(CameraControl.Instance.transform.forward, Vector3.up);
							this.HitWallNormal = Quaternion.AngleAxis(90f - num3, CameraControl.Instance.transform.right) * -CameraControl.Instance.transform.forward;
							Debug.DrawRay(this.lastRay1Pos, this.HitWallNormal * 5f, Color.magenta);
						}
						this.getPos = false;
					}
				}
			}
			bool flag17 = !this.getPos;
			if (flag17)
			{
				Vector3 down = Vector3.down;
				Vector3 normalized = (this.ShexianWeiZhie - this.lastRay1Pos).normalized;
				this.placePos = new Vector3(0f, 0f - this.fDisPlace, 0f);
				float num4 = 0f;
				while (num4 <= 1f && !this.getPos)
				{
					Vector3 normalized2 = Vector3.Lerp(down, normalized, num4).normalized;
					num4 += 0.01f;
					Debug.DrawRay(this.lastRay1Pos, normalized2 * this.fDisPlace, Color.red);
					bool flag18 = !Physics.Raycast(this.lastRay1Pos, normalized2, out this.hitt2, this.fDisPlace, this.layer);
					if (!flag18)
					{
						Debug.DrawRay(this.hitt2.point, -normalized2 * this.fDisPlace, Color.cyan);
						Vector3 r_dir = Quaternion.AngleAxis(90f, Vector3.up) * this.HitWallNormal;
						bool flag19 = this.hitt2.normal.y <= this.angle || !this.CanStandOnIt(this.hitt2.point, this.HitWallNormal, r_dir) || this.CheckIfPlaceItem(this.hitt2.collider.tag, this.hitt2.collider.name) || this.HasBoxNearBy(this.hitt2, 2);
						if (!flag19)
						{
							this.placePos = this.hitt2.point;
							this.PlaceHit = this.hitt2;
							Vector3 normalized3 = (this.placePos - CameraControl.Instance.transform.position).normalized;
							Vector3 forward = CameraControl.Instance.transform.forward;
							float num5 = Vector3.Dot(normalized3, forward);
							bool flag20 = !Physics.Raycast(this.PlaceHit.point + Vector3.up * 0.7f, this.ShexianWeiZhie - this.PlaceHit.point, out this.hitDiMian, Vector3.Distance(this.PlaceHit.point, this.ShexianWeiZhie), this.layer) && num5 > 0f;
							if (flag20)
							{
								bool flag21 = this.hitDiMian.collider != null;
								if (flag21)
								{
									Debug.DrawRay(this.PlaceHit.point + Vector3.up * 0.7f, this.ShexianWeiZhie - this.PlaceHit.point, Color.grey);
								}
								this.getPos = true;
							}
							else
							{
								this.getPos = false;
							}
						}
					}
				}
			}
			bool flag22 = this.getPos;
			if (flag22)
			{
				bool flag23 = !Physics.Raycast(this.PlaceHit.point + Vector3.up * 0.7f, this.ShexianWeiZhie - this.PlaceHit.point, out this.hitDiMian, Vector3.Distance(this.PlaceHit.point, this.ShexianWeiZhie), this.layer);
				if (flag23)
				{
					Vector3 vector2 = Camera.main.WorldToScreenPoint(this.PlaceHit.point);
					bool flag24 = Physics.Raycast(this.PlaceHit.point, Vector3.up, 2f);
					Debug.DrawRay(this.PlaceHit.point, Vector3.up * 2f, Color.black);
					bool flag25 = vector2.y > 0f && Vector3.Distance(CameraControl.Instance.transform.position, this.PlaceHit.point) > 2f && !flag24;
					if (flag25)
					{
						this.g.transform.parent = null;
						this.g.transform.localScale = Vector3.one;
						this.g.transform.transform.rotation = this.PlaceHit.collider.gameObject.transform.rotation;
						this.g.transform.transform.parent = this.PlaceHit.collider.gameObject.transform;
						this._hitObjTrans = this.PlaceHit.collider.gameObject.transform;
						this.ThisGameObjTrans.transform.parent = this.g.transform;
						this.ThisGameObjTrans.transform.position = this.PlaceHit.point;
						this.ThisGameObjTrans.transform.forward = CameraControl.Instance.transform.forward;
						this.ThisGameObjTrans.transform.up = Vector3.up;
						this.ThisGameObjTrans.transform.right = CameraControl.Instance.transform.right;
						this.b_HasTransPos = true;
						this.hitNpc = this.PlaceHit;
						this.getPos = false;
					}
					else
					{
						this.b_HasTransPos = false;
						this.getPos = false;
					}
				}
			}
			else
			{
				this.b_HasTransPos = false;
				this.getPos = false;
			}
		}
		bool flag26 = this.b_HasTransPos;
		if (flag26)
		{
			this.showItem(this.placeType);
		}
		else
		{
			this.hideItem(this.placeType);
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00038B50 File Offset: 0x00036D50
	private bool CanStandOnIt(Vector3 pos, Vector3 f_dir, Vector3 r_dir)
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		Vector3 vector3 = Vector3.zero;
		Vector3 vector4 = Vector3.zero;
		vector = pos + f_dir * 0.4f + Vector3.up;
		vector2 = pos - f_dir * 0.4f + Vector3.up;
		vector3 = pos - r_dir * 0.4f + Vector3.up;
		vector4 = pos + r_dir * 0.4f + Vector3.up;
		bool result = Physics.Raycast(vector, -Vector3.up, 1.1f) && Physics.Raycast(vector2, -Vector3.up, 1.1f) && Physics.Raycast(vector3, -Vector3.up, 1.1f) && Physics.Raycast(vector4, -Vector3.up, 1.1f);
		Debug.DrawRay(vector, -Vector3.up * 1.1f, Color.black);
		Debug.DrawRay(vector2, -Vector3.up * 1.1f, Color.blue);
		Debug.DrawRay(vector3, -Vector3.up * 1.1f, Color.cyan);
		Debug.DrawRay(vector4, -Vector3.up * 1.1f, Color.green);
		return result;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00038CD0 File Offset: 0x00036ED0
	private bool HasBoxNearBy(RaycastHit hit, int type)
	{
		if (!true)
		{
		}
		bool result;
		switch (type)
		{
		case 0:
			result = Physics.CheckBox(hit.point + Vector3.up * (this.boxSize.y + 0.1f) * Mathf.Sign(this.hitt.normal.y), this.boxSize / 2f, Quaternion.identity, this.layer);
			break;
		case 1:
			result = (Physics.CheckBox(hit.point + hit.normal * this.boxSize.y, this.boxSize / 2f, Quaternion.identity, this.layer) || Physics.Raycast(this.hitt.point, hit.normal, 0.7f, this.layer));
			break;
		case 2:
			result = Physics.CheckBox(hit.point + hit.normal * 1f, Vector3.one / 2.5f, Quaternion.identity, this.layer);
			break;
		default:
			result = false;
			break;
		}
		if (!true)
		{
		}
		return result;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00038E1C File Offset: 0x0003701C
	private bool CheckIfPlaceItem(string tag, string name)
	{
		bool flag = this.net.playerCommon.m_nInJidi == 1 && this.placeType == 3;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = tag.Contains("can_damage") || tag.Contains("Door") || name.Contains("Door") || name.Contains("door") || name.Contains("Slay") || name.Contains("burangfang") || name.Contains("jidi") || name.Contains("xuebao");
			result = flag2;
		}
		return result;
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x00038ECC File Offset: 0x000370CC
	private void setPlaceItemTrans(bool hasHit, RaycastHit h, Vector3 pos)
	{
		if (hasHit)
		{
			this.g.transform.parent = null;
			this.g.transform.localScale = Vector3.one;
			this.g.transform.transform.rotation = h.collider.gameObject.transform.rotation;
			this.g.transform.transform.parent = h.collider.gameObject.transform;
			this.ThisGameObjTrans.transform.parent = this.g.transform;
			this.ThisGameObjTrans.transform.position = h.point;
			this.ThisGameObjTrans.transform.forward = h.collider.transform.forward;
			this.ThisGameObjTrans.transform.up = h.collider.transform.up;
			this.ThisGameObjTrans.transform.right = h.collider.transform.right;
		}
		else
		{
			this.ThisGameObjTrans.transform.forward = this.ray.direction;
			this.ThisGameObjTrans.transform.position = pos;
		}
	}

	// Token: 0x040004A7 RID: 1191
	private int placeType;

	// Token: 0x040004A8 RID: 1192
	private float angle = 0.8f;

	// Token: 0x040004A9 RID: 1193
	private float fDisPlace = 100f;

	// Token: 0x040004AA RID: 1194
	private int layer;

	// Token: 0x040004AB RID: 1195
	public NetLiving net;

	// Token: 0x040004AC RID: 1196
	private int ray1Count;

	// Token: 0x040004AD RID: 1197
	private GameObject hong;

	// Token: 0x040004AE RID: 1198
	private GameObject lan;

	// Token: 0x040004AF RID: 1199
	private Transform _Trans;

	// Token: 0x040004B0 RID: 1200
	private Transform _hitObjTrans;

	// Token: 0x040004B1 RID: 1201
	private Vector3 boxSize = Vector3.zero;

	// Token: 0x040004B2 RID: 1202
	private bool releaseOnAir = true;

	// Token: 0x040004B3 RID: 1203
	private RaycastHit hitNpc;

	// Token: 0x040004B4 RID: 1204
	public bool changeHeroDes;

	// Token: 0x040004B5 RID: 1205
	public GameObject ThisGameObjTrans;

	// Token: 0x040004B6 RID: 1206
	private Vector3 lastHitRayDir = Vector3.zero;

	// Token: 0x040004B7 RID: 1207
	private Vector3 lastSheXianpos = Vector3.zero;

	// Token: 0x040004B8 RID: 1208
	private GameObject _PrepareItem;

	// Token: 0x040004B9 RID: 1209
	public GameObject PrepareItem;

	// Token: 0x040004BA RID: 1210
	private RaycastHit hitt;

	// Token: 0x040004BB RID: 1211
	private RaycastHit hitt2;

	// Token: 0x040004BC RID: 1212
	private RaycastHit hit;

	// Token: 0x040004BD RID: 1213
	private RaycastHit lastHit;

	// Token: 0x040004BE RID: 1214
	private RaycastHit PlaceHit;

	// Token: 0x040004BF RID: 1215
	private RaycastHit QiangHit;

	// Token: 0x040004C0 RID: 1216
	private Ray ray;

	// Token: 0x040004C1 RID: 1217
	private Ray canray;

	// Token: 0x040004C2 RID: 1218
	private Ray ray2;

	// Token: 0x040004C3 RID: 1219
	private Vector3 placePos;

	// Token: 0x040004C4 RID: 1220
	private Vector3 lastRay1Pos;

	// Token: 0x040004C5 RID: 1221
	private Vector3 lastPlacePos;

	// Token: 0x040004C6 RID: 1222
	private bool getPos;

	// Token: 0x040004C7 RID: 1223
	private bool b_HasTransPos;

	// Token: 0x040004C8 RID: 1224
	private RaycastHit hitDiMian = default(RaycastHit);

	// Token: 0x040004C9 RID: 1225
	private Vector3 ShexianWeiZhie;

	// Token: 0x040004CA RID: 1226
	private GameObject g;

	// Token: 0x040004CB RID: 1227
	private Vector3 HitWallNormal;
}
