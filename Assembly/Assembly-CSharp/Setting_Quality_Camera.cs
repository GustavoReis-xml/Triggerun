using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class Setting_Quality_Camera : MonoBehaviour
{
	// Token: 0x06002485 RID: 9349 RVA: 0x0014A40E File Offset: 0x0014860E
	public void Initialize()
	{
		this.Handle(this.Low, false);
		this.Handle(this.Middle, false);
		this.Handle(this.High, false);
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x0014A43C File Offset: 0x0014863C
	public void Use_Quality(int _Value)
	{
		switch (_Value)
		{
		case 0:
			this.Handle(this.Low, true);
			this.Handle(this.Middle, false);
			this.Handle(this.High, false);
			break;
		case 1:
			this.Handle(this.Low, true);
			this.Handle(this.Middle, true);
			this.Handle(this.High, false);
			break;
		case 2:
			this.Handle(this.Low, true);
			this.Handle(this.Middle, true);
			this.Handle(this.High, true);
			break;
		}
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x0014A4EC File Offset: 0x001486EC
	private void Handle(UnityEngine.Object[] _Data, bool _State)
	{
		for (int i = 0; i < _Data.Length; i++)
		{
			bool type = this.GetType(typeof(GameObject), _Data[i].GetType());
			if (type)
			{
				GameObject gameObject = (GameObject)_Data[i];
				gameObject.SetActive(_State);
			}
			else
			{
				bool type2 = this.GetType(typeof(MonoBehaviour), _Data[i].GetType());
				if (type2)
				{
					MonoBehaviour monoBehaviour = (MonoBehaviour)_Data[i];
					monoBehaviour.enabled = _State;
				}
			}
		}
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x0014A578 File Offset: 0x00148778
	private bool GetType(Type _Target, Type _Source)
	{
		bool flag = false;
		bool flag2 = _Source == null;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			bool flag3 = _Target != _Source;
			result = (!flag3 || this.GetType(_Target, _Source.BaseType));
		}
		return result;
	}

	// Token: 0x0400208C RID: 8332
	public UnityEngine.Object[] Low;

	// Token: 0x0400208D RID: 8333
	public UnityEngine.Object[] Middle;

	// Token: 0x0400208E RID: 8334
	public UnityEngine.Object[] High;
}
