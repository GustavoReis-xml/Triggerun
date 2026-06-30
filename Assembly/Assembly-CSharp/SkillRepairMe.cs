using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class SkillRepairMe : SkillBaseMe
{
	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00165FF4 File Offset: 0x001641F4
	// (set) Token: 0x060027B3 RID: 10163 RVA: 0x0016600C File Offset: 0x0016420C
	public bool E_Key
	{
		get
		{
			return this._E_Key;
		}
		set
		{
			this._E_Key = value;
		}
	}

	// Token: 0x17000510 RID: 1296
	// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00166018 File Offset: 0x00164218
	// (set) Token: 0x060027B5 RID: 10165 RVA: 0x00166030 File Offset: 0x00164230
	public bool State
	{
		get
		{
			return this._State;
		}
		set
		{
			this._State = value;
		}
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x0016603A File Offset: 0x0016423A
	private void Start()
	{
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x00166040 File Offset: 0x00164240
	private void Update()
	{
		this.E_Key = Input.GetKey(KeyCode.E);
		bool e_Key = this.E_Key;
		if (e_Key)
		{
			base.StartCoroutine(this.Wait_Repair());
		}
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x00166075 File Offset: 0x00164275
	private IEnumerator Wait_Repair()
	{
		bool flag = !this.State;
		if (flag)
		{
			base.hero.playerRuntimeData.NoInputMove = true;
			this.State = true;
			base.hero.heroSkill.DoSomeOne("Skill2", 0);
			yield return new WaitForSeconds(1f);
			this.State = false;
			bool e_Key = this.E_Key;
			if (e_Key)
			{
				base.StartCoroutine(this.Wait_Repair());
			}
			else
			{
				base.hero.playerRuntimeData.NoInputMove = false;
			}
		}
		yield break;
	}

	// Token: 0x040022AA RID: 8874
	private bool _E_Key;

	// Token: 0x040022AB RID: 8875
	private bool _State;
}
