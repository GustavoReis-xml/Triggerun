using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
{
	// Token: 0x0600015B RID: 347 RVA: 0x00007E3D File Offset: 0x0000603D
	public AssetBundleLoadAssetOperationSimulation(UnityEngine.Object simulatedObject)
	{
		this.m_SimulatedObject = simulatedObject;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00007E50 File Offset: 0x00006050
	public override T GetAsset<T>()
	{
		return this.m_SimulatedObject as T;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00007E74 File Offset: 0x00006074
	public override bool Update()
	{
		return false;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00007E88 File Offset: 0x00006088
	public override bool IsDone()
	{
		return true;
	}

	// Token: 0x040000ED RID: 237
	private UnityEngine.Object m_SimulatedObject;
}
