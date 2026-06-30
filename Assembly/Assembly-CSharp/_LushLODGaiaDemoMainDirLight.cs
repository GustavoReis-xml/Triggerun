using System;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class _LushLODGaiaDemoMainDirLight : MonoBehaviour
{
	// Token: 0x06002F50 RID: 12112 RVA: 0x001A6647 File Offset: 0x001A4847
	private void Start()
	{
		_LushLODGaiaDemoMainDirLight.LightRef = base.GetComponent<Light>();
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x001A6655 File Offset: 0x001A4855
	private void Awake()
	{
		_LushLODGaiaDemoMainDirLight.LightRef = base.GetComponent<Light>();
	}

	// Token: 0x06002F52 RID: 12114 RVA: 0x001A6663 File Offset: 0x001A4863
	private void OnEnable()
	{
		_LushLODGaiaDemoMainDirLight.LightRef = base.GetComponent<Light>();
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x001A6674 File Offset: 0x001A4874
	private void Update()
	{
		bool flag = _LushLODGaiaDemoMainDirLight.LightRef == null || _LushLODGaiaDemoMainDirLight.LightRef == null;
		if (flag)
		{
			_LushLODGaiaDemoMainDirLight.LightRef = base.GetComponent<Light>();
		}
	}

	// Token: 0x04002902 RID: 10498
	public static Light LightRef;
}
