using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class ParticleLateUpdate : MonoBehaviour
{
	// Token: 0x06001DB5 RID: 7605 RVA: 0x00114979 File Offset: 0x00112B79
	private void Start()
	{
		this.LateUpdate();
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x00114983 File Offset: 0x00112B83
	private void Update()
	{
		base.transform.position = this.targetPosition;
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00114998 File Offset: 0x00112B98
	private void LateUpdate()
	{
		this.targetPosition = this.target.position;
	}

	// Token: 0x04001AB4 RID: 6836
	public Transform target;

	// Token: 0x04001AB5 RID: 6837
	private Vector3 targetPosition;

	// Token: 0x04001AB6 RID: 6838
	private Quaternion targetRotation;
}
