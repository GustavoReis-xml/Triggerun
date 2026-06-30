using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class Setting_Quality_FPS_Model : MonoBehaviour
{
	// Token: 0x0600248A RID: 9354 RVA: 0x0014A5BF File Offset: 0x001487BF
	public void Initialize()
	{
		this.Handle(this.Low, false);
		this.Handle(this.Middle, false);
		this.Handle(this.High, false);
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x0014A5EC File Offset: 0x001487EC
	public void Use_Quality(int _Value)
	{
		switch (_Value)
		{
		case 0:
			this._Set = this.Low;
			break;
		case 1:
			this._Set = this.Middle;
			break;
		case 2:
			this._Set = this.High;
			break;
		default:
			this._Set = this.Low;
			break;
		}
		bool flag = this.Current_Set != null;
		if (flag)
		{
			this.Handle(this.Current_Set, false);
		}
		this.Current_Set = this._Set;
		this.Handle(this._Set, true);
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x0014A684 File Offset: 0x00148884
	private void Handle(UnityEngine.Object[] _Data, bool _State)
	{
		for (int i = 0; i < _Data.Length; i++)
		{
			bool flag = _Data[i].GetType() == typeof(GameObject);
			if (flag)
			{
				GameObject gameObject = (GameObject)_Data[i];
				gameObject.SetActive(_State);
			}
			else
			{
				bool flag2 = _Data[i].GetType() == typeof(MonoBehaviour);
				if (flag2)
				{
					MonoBehaviour monoBehaviour = (MonoBehaviour)_Data[i];
					monoBehaviour.enabled = _State;
				}
			}
		}
	}

	// Token: 0x0400208F RID: 8335
	public UnityEngine.Object[] Low;

	// Token: 0x04002090 RID: 8336
	public UnityEngine.Object[] Middle;

	// Token: 0x04002091 RID: 8337
	public UnityEngine.Object[] High;

	// Token: 0x04002092 RID: 8338
	private UnityEngine.Object[] Current_Set;

	// Token: 0x04002093 RID: 8339
	private UnityEngine.Object[] _Set;
}
