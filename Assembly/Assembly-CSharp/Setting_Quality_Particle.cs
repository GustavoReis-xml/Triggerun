using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class Setting_Quality_Particle : MonoBehaviour
{
	// Token: 0x0600248E RID: 9358 RVA: 0x0014A711 File Offset: 0x00148911
	public void Initialize()
	{
		this.Handle(this.Low, false);
		this.Handle(this.Middle, false);
		this.Handle(this.High, false);
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x0014A740 File Offset: 0x00148940
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
			this.Handle(this.Low, false);
			this.Handle(this.Middle, true);
			this.Handle(this.High, false);
			break;
		case 2:
			this.Handle(this.Low, false);
			this.Handle(this.Middle, false);
			this.Handle(this.High, true);
			break;
		}
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x0014A7F0 File Offset: 0x001489F0
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

	// Token: 0x06002491 RID: 9361 RVA: 0x0014A87C File Offset: 0x00148A7C
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

	// Token: 0x04002094 RID: 8340
	public UnityEngine.Object[] Low;

	// Token: 0x04002095 RID: 8341
	public UnityEngine.Object[] Middle;

	// Token: 0x04002096 RID: 8342
	public UnityEngine.Object[] High;
}
