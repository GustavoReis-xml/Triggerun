using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public abstract class TBCharAnimation : MonoBehaviour
{
	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x06002B50 RID: 11088 RVA: 0x0017E6DA File Offset: 0x0017C8DA
	public virtual bool animationGrounded
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06002B51 RID: 11089 RVA: 0x0017E6E0 File Offset: 0x0017C8E0
	protected void InitCharAnimation()
	{
		bool flag = this.animator == null;
		if (flag)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06002B52 RID: 11090 RVA: 0x0017E70C File Offset: 0x0017C90C
	public virtual Vector3 GetPivotPoint()
	{
		return base.transform.position;
	}

	// Token: 0x06002B53 RID: 11091 RVA: 0x0017E72C File Offset: 0x0017C92C
	public virtual bool GetAnimatorBool(string ani)
	{
		return false;
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x0017E740 File Offset: 0x0017C940
	public virtual float GetAnimatorFloat(string ani)
	{
		return -999f;
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x0017E758 File Offset: 0x0017C958
	public virtual int GetAnimatorInteger(string ani)
	{
		return -999;
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x0017E76F File Offset: 0x0017C96F
	public virtual void SetAnimator(string ani, int type, object parm)
	{
		this.InputAnimatorCache(ani, type, parm);
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x0017E77C File Offset: 0x0017C97C
	private void InputAnimatorCache(string ani, int type, object parm)
	{
		bool flag = !this.animator.isActiveAndEnabled;
		if (flag)
		{
			bool flag2 = !this.dictAnimator.ContainsKey(ani);
			if (flag2)
			{
				CAnimatorData canimatorData = new CAnimatorData();
				canimatorData.t = Time.time;
				canimatorData.animatorName = ani;
				canimatorData.animatorType = type;
				canimatorData.parm = parm;
				this.dictAnimator.Add(ani, canimatorData);
			}
			else
			{
				CAnimatorData canimatorData2 = new CAnimatorData();
				canimatorData2.t = Time.time;
				canimatorData2.animatorName = ani;
				canimatorData2.animatorType = type;
				canimatorData2.parm = parm;
				this.dictAnimator[ani] = canimatorData2;
			}
		}
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x0017E824 File Offset: 0x0017CA24
	private void CheckAnimatorActive()
	{
		bool flag = this.dictAnimator.Count != 0 && this.animator.isActiveAndEnabled;
		if (flag)
		{
			CAnimatorData[] array = new CAnimatorData[this.dictAnimator.Count];
			this.dictAnimator.Values.CopyTo(array, 0);
			Array.Sort<CAnimatorData>(array, new CAnimatorDataSort());
			foreach (CAnimatorData canimatorData in array)
			{
				this.SetAnimator(canimatorData.animatorName, canimatorData.animatorType, canimatorData.parm);
			}
			this.dictAnimator.Clear();
		}
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x0017E8C3 File Offset: 0x0017CAC3
	protected virtual void Update()
	{
		this.CheckAnimatorActive();
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x0017E8D0 File Offset: 0x0017CAD0
	public virtual object AnimatorIsPlaying(string ani)
	{
		return false;
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x0017E8E8 File Offset: 0x0017CAE8
	public float GetAngleFromForward(Vector3 worldDirection)
	{
		Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
		return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x0017E920 File Offset: 0x0017CB20
	protected virtual void Start()
	{
		bool flag = base.transform.parent.GetComponent<TBCharacter>() == null;
		if (flag)
		{
			Debug.LogWarning("Animation controllers should be parented to character controllers!", base.transform);
		}
		this.lastPosition = base.transform.position;
		this.localPosition = base.transform.parent.InverseTransformPoint(base.transform.position);
		this.lastRotation = base.transform.rotation;
		this.localRotation = Quaternion.Inverse(base.transform.parent.rotation) * base.transform.rotation;
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x0017E9CC File Offset: 0x0017CBCC
	protected virtual void LateUpdate()
	{
		bool flag = this.smoothFollow;
		if (flag)
		{
			base.transform.position = Fun.SuperSmoothLerp(base.transform.position, this.lastPosition, base.transform.parent.TransformPoint(this.localPosition), this.smoothFollowSpeed, Time.deltaTime);
		}
		bool flag2 = this.smoothRotFollow;
		if (flag2)
		{
			base.transform.rotation = Quaternion.Lerp(this.lastRotation, base.transform.parent.rotation * this.localRotation, 1f - Mathf.Exp((0f - this.smoothFollowRotSpeed) * Time.deltaTime));
		}
		this.lastPosition = base.transform.position;
		this.lastRotation = base.transform.rotation;
	}

	// Token: 0x04002537 RID: 9527
	public Animator animator;

	// Token: 0x04002538 RID: 9528
	public bool smoothFollow;

	// Token: 0x04002539 RID: 9529
	private float smoothFollowSpeed = 30f;

	// Token: 0x0400253A RID: 9530
	public bool smoothRotFollow;

	// Token: 0x0400253B RID: 9531
	private float smoothFollowRotSpeed = 50f;

	// Token: 0x0400253C RID: 9532
	private Vector3 lastPosition;

	// Token: 0x0400253D RID: 9533
	private Vector3 localPosition;

	// Token: 0x0400253E RID: 9534
	private Quaternion localRotation;

	// Token: 0x0400253F RID: 9535
	private Quaternion lastRotation;

	// Token: 0x04002540 RID: 9536
	private Dictionary<string, CAnimatorData> dictAnimator = new Dictionary<string, CAnimatorData>();

	// Token: 0x04002541 RID: 9537
	protected float lateUpdateRate;
}
