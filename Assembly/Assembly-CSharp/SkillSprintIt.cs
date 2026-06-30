using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class SkillSprintIt : SkillBaseIt
{
	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x06002844 RID: 10308 RVA: 0x0016A52C File Offset: 0x0016872C
	// (set) Token: 0x06002845 RID: 10309 RVA: 0x0016A58A File Offset: 0x0016878A
	public GameObject Sprint_Sound_Object
	{
		get
		{
			bool flag = this._Sprint_Sound_Object == null;
			if (flag)
			{
				Transform transform = Fun.Get_Child_Transform(base.hero.thisModel, "Terminator_Sprint_Sound");
				bool flag2 = transform == null;
				if (flag2)
				{
					return null;
				}
				this._Sprint_Sound_Object = transform.gameObject;
			}
			return this._Sprint_Sound_Object;
		}
		set
		{
			this._Sprint_Sound_Object = value;
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x06002846 RID: 10310 RVA: 0x0016A594 File Offset: 0x00168794
	// (set) Token: 0x06002847 RID: 10311 RVA: 0x0016A5D2 File Offset: 0x001687D2
	public AudioSource Sprint_Audio_Source
	{
		get
		{
			bool flag = this.Sprint_Sound_Object == null;
			AudioSource result;
			if (flag)
			{
				result = null;
			}
			else
			{
				this._Sprint_Audio_Source = this.Sprint_Sound_Object.GetComponent<AudioSource>();
				result = this._Sprint_Audio_Source;
			}
			return result;
		}
		set
		{
			this._Sprint_Audio_Source = value;
		}
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x0016A5DC File Offset: 0x001687DC
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x0016A5E8 File Offset: 0x001687E8
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
	}

	// Token: 0x04002308 RID: 8968
	private GameObject _Sprint_Sound_Object;

	// Token: 0x04002309 RID: 8969
	private AudioSource _Sprint_Audio_Source;
}
