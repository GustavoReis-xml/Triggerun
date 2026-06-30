using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020004DA RID: 1242
[ExecuteInEditMode]
public class _LushLODTreesManager : MonoBehaviour
{
	// Token: 0x06002F79 RID: 12153 RVA: 0x001A8E28 File Offset: 0x001A7028
	public bool UsingUltraShader(bool UsePrevious)
	{
		_LushLODTreesManager.BillboardQualitySettings billboardQualitySettings = (!UsePrevious) ? this.billboard_quality : this.billboard_quality_PREVIOUS;
		return billboardQualitySettings == _LushLODTreesManager.BillboardQualitySettings.Ultra;
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x001A8E5C File Offset: 0x001A705C
	public bool UsingFastShader(_LushLODTree FirstTree)
	{
		bool flag = FirstTree == null || FirstTree == null || FirstTree.Equals(null);
		return !flag && (FirstTree.UsingFastLeavesShader || FirstTree.UsingFastBarkShader);
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x001A8EA4 File Offset: 0x001A70A4
	public _LushLODTree FindTreeUsingFastShader()
	{
		_LushLODTree[] componentsInChildren = this.TreesRoot.GetComponentsInChildren<_LushLODTree>();
		bool flag = componentsInChildren.Length == 0;
		_LushLODTree result;
		if (flag)
		{
			result = null;
		}
		else
		{
			_LushLODTree[] array = componentsInChildren;
			foreach (_LushLODTree lushLODTree in array)
			{
				bool flag2 = lushLODTree.UsingFastLeavesShader || lushLODTree.UsingFastBarkShader;
				if (flag2)
				{
					return lushLODTree;
				}
			}
			result = null;
		}
		return result;
	}

	// Token: 0x06002F7C RID: 12156 RVA: 0x001A8F14 File Offset: 0x001A7114
	public ShadowCastingMode GetBillboardShadowCastingMode(bool UsePrevious)
	{
		ShadowCastingMode result = ShadowCastingMode.TwoSided;
		switch ((!UsePrevious) ? this.RealTimeShadowSetting : this.RealTimeShadowSetting_PREVIOUS)
		{
		case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
			result = ShadowCastingMode.TwoSided;
			break;
		case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
			result = ShadowCastingMode.TwoSided;
			break;
		case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
			result = ShadowCastingMode.Off;
			break;
		}
		return result;
	}

	// Token: 0x06002F7D RID: 12157 RVA: 0x001A8F60 File Offset: 0x001A7160
	public ShadowCastingMode GetHighQualityShadowCastingMode(bool UsePrevious)
	{
		ShadowCastingMode result = ShadowCastingMode.TwoSided;
		switch ((!UsePrevious) ? this.RealTimeShadowSetting : this.RealTimeShadowSetting_PREVIOUS)
		{
		case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
			result = ShadowCastingMode.On;
			break;
		case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
			result = ShadowCastingMode.Off;
			break;
		case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
			result = ShadowCastingMode.Off;
			break;
		}
		return result;
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x001A8FA9 File Offset: 0x001A71A9
	public void OnDestroyParentS()
	{
		this.should_recalculate_parents = true;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x001A8FB3 File Offset: 0x001A71B3
	public void OnDestroyParent()
	{
		base.CancelInvoke("OnDestroyParent");
		this.ReCalculateParents(false);
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x001A8FCC File Offset: 0x001A71CC
	public void SetupHalfSrqTransition(bool UsePrevious)
	{
		float[] array = _LushLODTreesManager.CalculateLODDistances(this.LOD_adjust, UsePrevious, this.AverageTreeScale);
		bool flag = _LushLODTree.TreesManager.UsingUltraShader(UsePrevious);
		if (flag)
		{
			this.halfsqrTransition = Mathf.Lerp(array[0], array[3], 0.5f);
		}
		else
		{
			this.halfsqrTransition = array[1];
		}
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x001A9024 File Offset: 0x001A7224
	public static float[] CalculateLODDistances(float lodDistance, bool UsePrevious, float AverageTreeScale)
	{
		float[] array = new float[5];
		float num = lodDistance * lodDistance;
		bool flag = _LushLODTree.TreesManager.UsingUltraShader(UsePrevious);
		if (flag)
		{
			array[0] = num - 0.95f * num;
			array[1] = num - 0.95f * num;
			array[2] = num + 0.95f * num;
			array[3] = num + 0.95f * num;
			array[4] = array[3] * 2.5f;
		}
		else
		{
			array[0] = num - 0.95f * num;
			array[1] = num - 0f * num;
			array[2] = num - 0f * num;
			array[3] = num + 0.95f * num;
			array[4] = array[3] * 2.5f;
		}
		array[0] *= AverageTreeScale;
		array[1] *= AverageTreeScale;
		array[2] *= AverageTreeScale;
		array[3] *= AverageTreeScale;
		array[4] *= AverageTreeScale;
		return array;
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x001A910C File Offset: 0x001A730C
	public void RootAllTrees()
	{
		bool flag = this.TreesRoot == null || this.TreesRoot == null || this.TreesRoot.Equals(null);
		if (flag)
		{
			Debug.LogError("You must set up the TreesRoot first before you can RootAllTrees()");
		}
		else
		{
			_LushLODTree[] array = UnityEngine.Object.FindObjectsOfType<_LushLODTree>();
			_LushLODTree[] array2 = array;
			foreach (_LushLODTree lushLODTree in array2)
			{
				lushLODTree.transform.parent = this.TreesRoot.transform;
			}
			bool flag2 = array.Length != 0;
			if (flag2)
			{
			}
		}
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x001A919B File Offset: 0x001A739B
	private void OnDestroy()
	{
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x001A91A0 File Offset: 0x001A73A0
	public bool ReCalculateParents(bool DuringGame = false)
	{
		base.CancelInvoke("OnDestroyParent");
		this.should_recalculate_parents = false;
		bool flag = this.TreesRoot == null || this.TreesRoot == null || this.TreesRoot.Equals(null);
		bool result;
		if (flag)
		{
			Debug.LogError("You must set up the TreesRoot first before you can ReCalculate Parents.");
			result = false;
		}
		else
		{
			_LushLODTree[] componentsInChildren = this.TreesRoot.GetComponentsInChildren<_LushLODTree>();
			bool flag2 = componentsInChildren.Length == 0;
			if (flag2)
			{
				Debug.LogError("Cannot recalculate parents, because no trees were found in your TreesRoot. Please spawn some _LushLODTrees, or move your _LushLODTrees to your TreesRoot gameobject.");
				result = false;
			}
			else
			{
				_LushLODTree.TreesManager.FirstTreeFound = componentsInChildren[0];
				this.AverageTreeScale = 0f;
				_LushLODTree[] array = componentsInChildren;
				foreach (_LushLODTree lushLODTree in array)
				{
					float num = (lushLODTree.gameObject.transform.lossyScale.x + lushLODTree.gameObject.transform.lossyScale.y + lushLODTree.gameObject.transform.lossyScale.z) / 3f;
					this.AverageTreeScale += num;
				}
				this.AverageTreeScale /= (float)componentsInChildren.Length;
				_LushLODTree.TreesManager.AllParents = null;
				_LushLODTree[] array3 = componentsInChildren;
				foreach (_LushLODTree lushLODTree2 in array3)
				{
					lushLODTree2.MyChildren = new List<_LushLODTree>();
					lushLODTree2.MyChildrenEnabled = true;
					lushLODTree2.MyParent = null;
					lushLODTree2.IsParent = false;
					lushLODTree2.enabled = true;
					lushLODTree2.lodDistance = this.LOD_adjust_PREVIOUS;
				}
				_LushLODTree[] array5 = componentsInChildren;
				foreach (_LushLODTree lushLODTree3 in array5)
				{
					bool flag3 = _LushLODTree.TreesManager.AllParents == null || _LushLODTree.TreesManager.AllParents == null || _LushLODTree.TreesManager.AllParents.Equals(null) || _LushLODTree.TreesManager.AllParents.Count == 0;
					if (flag3)
					{
						_LushLODTree.TreesManager.AllParents = new List<_LushLODTree>();
						_LushLODTree.TreesManager.AllParents.Add(lushLODTree3);
						lushLODTree3.MyChildren = new List<_LushLODTree>();
						lushLODTree3.IsParent = true;
					}
					else
					{
						float num2 = float.MaxValue;
						_LushLODTree lushLODTree4 = _LushLODTree.TreesManager.AllParents[0];
						lushLODTree3.sqrTransition = _LushLODTreesManager.CalculateLODDistances(lushLODTree3.lodDistance, true, this.AverageTreeScale);
						foreach (_LushLODTree lushLODTree5 in _LushLODTree.TreesManager.AllParents)
						{
							bool flag4 = !(lushLODTree5 == null) && lushLODTree5 != null && !lushLODTree5.Equals(null);
							if (flag4)
							{
								float sqrMagnitude = (lushLODTree3.gameObject.transform.position - lushLODTree5.gameObject.transform.position).sqrMagnitude;
								bool flag5 = sqrMagnitude < num2;
								if (flag5)
								{
									num2 = sqrMagnitude;
									lushLODTree4 = lushLODTree5;
								}
							}
						}
						bool flag6 = num2 < lushLODTree3.sqrTransition[1];
						if (flag6)
						{
							lushLODTree3.MyParent = lushLODTree4;
							lushLODTree4.MyChildren.Add(lushLODTree3);
							lushLODTree3.IsParent = false;
						}
						else
						{
							_LushLODTree.TreesManager.AllParents.Add(lushLODTree3);
							lushLODTree3.MyChildren = new List<_LushLODTree>();
							lushLODTree3.IsParent = true;
						}
					}
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x001A9530 File Offset: 0x001A7730
	public void ApplyAll(_LushLODTree SpecificTree = null, bool ForceFullUpdate = false, bool NoUndo = false)
	{
		bool flag = false;
		bool flag2 = SpecificTree != null;
		if (flag2)
		{
			flag = true;
		}
		bool flag3 = this.TreesRoot == null || this.TreesRoot == null || this.TreesRoot.Equals(null);
		if (flag3)
		{
			Debug.LogError("You must set up the TreesRoot first before you can ApplyAll().");
		}
		else
		{
			bool flag4 = _LushLODTree.TreesManager.AllParents == null || _LushLODTree.TreesManager.AllParents == null || _LushLODTree.TreesManager.AllParents.Equals(null) || _LushLODTree.TreesManager.AllParents.Count == 0;
			if (flag4)
			{
				Debug.LogError("You must set up the Parents first before you can ApplyAll(). Do this by clicking the Calculate Parents button.");
			}
			else
			{
				_LushLODTree[] componentsInChildren = this.TreesRoot.GetComponentsInChildren<_LushLODTree>();
				bool flag5 = componentsInChildren == null || componentsInChildren == null || componentsInChildren.Equals(null) || componentsInChildren.Length == 0;
				if (flag5)
				{
					Debug.LogError("Couldn't find any trees in your scene. Have you added trees? The number of _LushLODTree's found in all children of your TreesRoot gameobject was zero.");
				}
				else
				{
					_LushLODTree.TreesManager.FirstTreeFound = componentsInChildren[0];
					bool flag6 = this.maincolor_LQTrees != this.maincolor_LQTrees_PREVIOUS || this.maincolor_HQTrees != this.maincolor_HQTrees_PREVIOUS || flag || ForceFullUpdate || this.DoColorReset;
					if (flag6)
					{
						this.DoColorReset = false;
						bool flag7 = !flag;
						if (flag7)
						{
							this.maincolor_LQTrees_PREVIOUS = this.maincolor_LQTrees;
							this.maincolor_HQTrees_PREVIOUS = this.maincolor_HQTrees;
						}
						Color b = this.maincolor_HQTrees;
						b.r -= 0.5f;
						b.g -= 0.5f;
						b.b -= 0.5f;
						b.a -= 0.5f;
						Color b2 = this.maincolor_LQTrees;
						b2.r -= 0.5f;
						b2.g -= 0.5f;
						b2.b -= 0.5f;
						b2.a -= 0.5f;
						bool isPlaying = Application.isPlaying;
						if (isPlaying)
						{
							_LushLODTree.Material_FullyOpaque_References.SetColor(this.maincolor_LQTrees_ORIGINAL + b2);
							_LushLODTree[] array = componentsInChildren;
							foreach (_LushLODTree lushLODTree in array)
							{
								_LushLODTree lushLODTree2 = lushLODTree;
								bool flag8 = SpecificTree != null;
								if (flag8)
								{
									lushLODTree2 = SpecificTree;
								}
								bool flag9 = lushLODTree2.Material_Instance_Transitioning.HasProperty("_Color");
								if (flag9)
								{
									lushLODTree2.Material_Instance_Transitioning.color = this.maincolor_LQTrees_ORIGINAL + b2;
								}
								for (int j = 0; j < lushLODTree2.lod0Rend.materials.Length; j++)
								{
									Material material = lushLODTree2.lod0Rend.materials[j];
									material.color = lushLODTree2.maincolor_HQTrees_ORIGINALS[j] + b;
								}
								bool flag10 = SpecificTree != null;
								if (flag10)
								{
									break;
								}
							}
						}
						else
						{
							bool flag11 = componentsInChildren[0].lod1Rend.sharedMaterial.HasProperty("_Color");
							if (flag11)
							{
								componentsInChildren[0].lod1Rend.sharedMaterial.color = this.maincolor_LQTrees_ORIGINAL + b2;
							}
							componentsInChildren[0].Material_NotInstance_FullyOpaque.color = this.maincolor_LQTrees_ORIGINAL + b2;
							componentsInChildren[0].Material_Instance_Transitioning.color = this.maincolor_LQTrees_ORIGINAL + b2;
							List<Material> list = new List<Material>();
							_LushLODTree[] array3 = componentsInChildren;
							foreach (_LushLODTree lushLODTree3 in array3)
							{
								_LushLODTree lushLODTree4 = lushLODTree3;
								bool flag12 = SpecificTree != null;
								if (flag12)
								{
									lushLODTree4 = SpecificTree;
								}
								for (int l = 0; l < lushLODTree4.lod0Rend.sharedMaterials.Length; l++)
								{
									Material material2 = lushLODTree4.lod0Rend.sharedMaterials[l];
									bool flag13 = !list.Contains(material2);
									if (flag13)
									{
										list.Add(material2);
										material2.color = lushLODTree4.maincolor_HQTrees_ORIGINALS[l] + b;
									}
								}
								bool flag14 = SpecificTree != null;
								if (flag14)
								{
									break;
								}
							}
							list.Clear();
						}
					}
					bool flag15 = false;
					bool flag16 = !Mathf.Approximately(this.LOD_adjust, this.LOD_adjust_PREVIOUS);
					if (flag16)
					{
						this.LOD_adjust_PREVIOUS = this.LOD_adjust;
						flag15 = true;
					}
					bool flag17 = flag15 || flag || ForceFullUpdate;
					if (flag17)
					{
						this.SetupHalfSrqTransition(false);
						_LushLODTree[] array5 = componentsInChildren;
						foreach (_LushLODTree lushLODTree5 in array5)
						{
							_LushLODTree lushLODTree6 = lushLODTree5;
							bool flag18 = SpecificTree != null;
							if (flag18)
							{
								lushLODTree6 = SpecificTree;
							}
							lushLODTree6.lodDistance = (float)Mathf.RoundToInt(this.LOD_adjust);
							lushLODTree6.Reset();
							bool flag19 = SpecificTree != null;
							if (flag19)
							{
								break;
							}
						}
						bool flag20 = flag15;
						if (flag20)
						{
							bool isPlaying2 = Application.isPlaying;
							if (isPlaying2)
							{
								Debug.LogWarning("You changed the LOD Distance during the game. Changing the LOD Distance requires a full recalculation of the parents, which will freeze the game momentarily. You may want to adjust the LOD Distance when the game isn't running (in the Unity editor).");
							}
							_LushLODTree.TreesManager.ReCalculateParents(true);
						}
					}
					bool flag21 = false;
					bool flag22 = this.billboard_quality == this.billboard_quality_PREVIOUS && this.billboard_quality == _LushLODTreesManager.BillboardQualitySettings.Ultra && ((this.BillBoardSetting == _LushLODTreesManager.BillBoardSettings.BillboardsOnly && this.BillBoardSetting_PREVIOUS != _LushLODTreesManager.BillBoardSettings.BillboardsOnly) || (this.BillBoardSetting != _LushLODTreesManager.BillBoardSettings.BillboardsOnly && this.BillBoardSetting_PREVIOUS == _LushLODTreesManager.BillBoardSettings.BillboardsOnly));
					if (flag22)
					{
						flag21 = true;
					}
					bool flag23 = this.billboard_quality != this.billboard_quality_PREVIOUS || flag || ForceFullUpdate || flag21;
					if (flag23)
					{
						bool flag24 = !flag;
						if (flag24)
						{
							bool flag25 = this.billboard_quality != this.billboard_quality_PREVIOUS && ((this.billboard_quality == _LushLODTreesManager.BillboardQualitySettings.Ultra && this.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra) || (this.billboard_quality != _LushLODTreesManager.BillboardQualitySettings.Ultra && this.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra));
							if (flag25)
							{
								this.SetupHalfSrqTransition(false);
								flag21 = true;
							}
							this.billboard_quality_PREVIOUS = this.billboard_quality;
						}
						this.ShaderSettingsChanged++;
						bool usePrevious = flag;
						bool isPlaying3 = Application.isPlaying;
						if (isPlaying3)
						{
							_LushLODTree[] array7 = componentsInChildren;
							foreach (_LushLODTree lushLODTree7 in array7)
							{
								_LushLODTree lushLODTree8 = lushLODTree7;
								bool flag26 = SpecificTree != null;
								if (flag26)
								{
									lushLODTree8 = SpecificTree;
								}
								lushLODTree8.billboard_quality_PREVIOUS = this.billboard_quality;
								lushLODTree8.sqrTransition = _LushLODTreesManager.CalculateLODDistances(lushLODTree8.lodDistance, usePrevious, this.AverageTreeScale);
								bool flag27 = SpecificTree != null;
								if (flag27)
								{
									break;
								}
							}
						}
						else
						{
							_LushLODTree[] array9 = componentsInChildren;
							foreach (_LushLODTree lushLODTree9 in array9)
							{
								_LushLODTree lushLODTree10 = lushLODTree9;
								bool flag28 = SpecificTree != null;
								if (flag28)
								{
									lushLODTree10 = SpecificTree;
								}
								lushLODTree10.billboard_quality_PREVIOUS = this.billboard_quality;
								lushLODTree10.sqrTransition = _LushLODTreesManager.CalculateLODDistances(lushLODTree10.lodDistance, usePrevious, this.AverageTreeScale);
								bool flag29 = SpecificTree != null;
								if (flag29)
								{
									break;
								}
							}
						}
					}
					bool flag30 = this.ShadowReceiveSetting != this.ShadowReceiveSetting_PREVIOUS || flag || ForceFullUpdate || flag21;
					if (flag30)
					{
						bool flag31 = !flag;
						if (flag31)
						{
							this.ShadowReceiveSetting_PREVIOUS = this.ShadowReceiveSetting;
						}
						this.ShaderSettingsChanged++;
						_LushLODTree[] array11 = componentsInChildren;
						foreach (_LushLODTree lushLODTree11 in array11)
						{
							_LushLODTree lushLODTree12 = lushLODTree11;
							bool flag32 = SpecificTree != null;
							if (flag32)
							{
								lushLODTree12 = SpecificTree;
							}
							switch (this.ShadowReceiveSetting)
							{
							case _LushLODTreesManager.ShadowReceiveSettings.AllShadows:
								lushLODTree12.ShadowReceiveSetting = _LushLODTreesManager.ShadowReceiveSettings.AllShadows;
								lushLODTree12.lod0Rend.receiveShadows = true;
								break;
							case _LushLODTreesManager.ShadowReceiveSettings.SimpleShadows:
								lushLODTree12.ShadowReceiveSetting = _LushLODTreesManager.ShadowReceiveSettings.SimpleShadows;
								lushLODTree12.lod0Rend.receiveShadows = false;
								break;
							case _LushLODTreesManager.ShadowReceiveSettings.NoShadows:
								lushLODTree12.ShadowReceiveSetting = _LushLODTreesManager.ShadowReceiveSettings.NoShadows;
								lushLODTree12.lod0Rend.receiveShadows = false;
								break;
							}
							bool flag33 = SpecificTree != null;
							if (flag33)
							{
								break;
							}
						}
					}
					bool flag34 = this.BillBoardSetting != this.BillBoardSetting_PREVIOUS || flag || ForceFullUpdate;
					if (flag34)
					{
						bool flag35 = !flag;
						if (flag35)
						{
							this.BillBoardSetting_PREVIOUS = this.BillBoardSetting;
						}
						_LushLODTree[] array13 = componentsInChildren;
						foreach (_LushLODTree lushLODTree13 in array13)
						{
							_LushLODTree lushLODTree14 = lushLODTree13;
							bool flag36 = SpecificTree != null;
							if (flag36)
							{
								lushLODTree14 = SpecificTree;
							}
							switch (this.BillBoardSetting)
							{
							case _LushLODTreesManager.BillBoardSettings.UseBoth:
							{
								lushLODTree14.BillBoardSetting = _LushLODTreesManager.BillBoardSettings.UseBoth;
								bool isPlaying4 = Application.isPlaying;
								if (isPlaying4)
								{
									bool isParent = lushLODTree14.IsParent;
									if (isParent)
									{
										lushLODTree14.CheckChildren = true;
										lushLODTree14.enabled = true;
									}
								}
								else
								{
									lushLODTree14.lod1Rend.enabled = true;
									lushLODTree14.lod0Rend.enabled = true;
									lushLODTree14.enabled = true;
									lushLODTree14.MyChildrenEnabled = true;
								}
								break;
							}
							case _LushLODTreesManager.BillBoardSettings.BillboardsOnly:
							{
								lushLODTree14.BillBoardSetting = _LushLODTreesManager.BillBoardSettings.BillboardsOnly;
								lushLODTree14.lod1Rend.enabled = true;
								bool isPlaying5 = Application.isPlaying;
								if (isPlaying5)
								{
									bool flag37 = lushLODTree14.lod1Rend.sharedMaterial != lushLODTree14.Material_FullyOpaque_static.Material_FullyOpaque_static;
									if (flag37)
									{
										lushLODTree14.lod1Rend.sharedMaterial = lushLODTree14.Material_FullyOpaque_static.Material_FullyOpaque_static;
										lushLODTree14.lod1Rend.enabled = true;
									}
									lushLODTree14.enabled = false;
									lushLODTree14.MyChildrenEnabled = false;
								}
								else
								{
									bool flag38 = lushLODTree14.lod1Rend.sharedMaterial != lushLODTree14.Material_NotInstance_FullyOpaque;
									if (flag38)
									{
										lushLODTree14.lod1Rend.sharedMaterial = lushLODTree14.Material_NotInstance_FullyOpaque;
										lushLODTree14.lod1Rend.enabled = true;
									}
									lushLODTree14.enabled = true;
								}
								lushLODTree14.lod0Rend.enabled = false;
								break;
							}
							case _LushLODTreesManager.BillBoardSettings.HighQualityModelsOnly:
							{
								lushLODTree14.lod1Rend.enabled = false;
								lushLODTree14.lod0Rend.enabled = true;
								lushLODTree14.rendIs1 = true;
								lushLODTree14.rendIsFloat = 1f;
								bool isPlaying6 = Application.isPlaying;
								if (isPlaying6)
								{
									Material[] materials = lushLODTree14.lod0Rend.materials;
									foreach (Material material3 in materials)
									{
										material3.SetFloat("_Transparency", 1f);
									}
									lushLODTree14.enabled = false;
									lushLODTree14.MyChildrenEnabled = false;
								}
								else
								{
									lushLODTree14.enabled = true;
								}
								break;
							}
							}
							bool flag39 = SpecificTree != null;
							if (flag39)
							{
								break;
							}
						}
					}
					bool flag40 = this.RealTimeShadowSetting != this.RealTimeShadowSetting_PREVIOUS || flag || ForceFullUpdate;
					if (flag40)
					{
						bool flag41 = !flag;
						if (flag41)
						{
							this.RealTimeShadowSetting_PREVIOUS = this.RealTimeShadowSetting;
						}
						_LushLODTree[] array16 = componentsInChildren;
						foreach (_LushLODTree lushLODTree15 in array16)
						{
							_LushLODTree lushLODTree16 = lushLODTree15;
							bool flag42 = SpecificTree != null;
							if (flag42)
							{
								lushLODTree16 = SpecificTree;
							}
							switch (this.RealTimeShadowSetting)
							{
							case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
								lushLODTree16.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.UseBoth;
								lushLODTree16.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
								lushLODTree16.lod0Rend.shadowCastingMode = ShadowCastingMode.On;
								break;
							case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
							{
								lushLODTree16.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly;
								lushLODTree16.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
								lushLODTree16.lod0Rend.shadowCastingMode = ShadowCastingMode.Off;
								bool isPlaying7 = Application.isPlaying;
								if (isPlaying7)
								{
									bool flag43 = !lushLODTree16.lod1Rend.enabled;
									if (flag43)
									{
										lushLODTree16.lod1Rend.enabled = true;
									}
									bool flag44 = !lushLODTree16.enabled;
									if (flag44)
									{
										lushLODTree16.lod1Rend.sharedMaterial.SetFloat("_Transparency", 1f);
									}
									lushLODTree16.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f);
								}
								break;
							}
							case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
								lushLODTree16.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff;
								lushLODTree16.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
								lushLODTree16.lod0Rend.shadowCastingMode = ShadowCastingMode.Off;
								break;
							}
							bool flag45 = SpecificTree != null;
							if (flag45)
							{
								break;
							}
						}
					}
					bool flag46 = Application.isPlaying && this.ShaderSettingsChanged_PREVIOUS != this.ShaderSettingsChanged;
					if (flag46)
					{
						this.ShaderSettingsChanged_PREVIOUS = this.ShaderSettingsChanged;
						_LushLODTree[] array18 = componentsInChildren;
						foreach (_LushLODTree lushLODTree17 in array18)
						{
							bool flag47 = lushLODTree17.ShaderSettingsChanged != this.ShaderSettingsChanged;
							if (flag47)
							{
								lushLODTree17.DoShaderUpdate();
							}
						}
					}
					bool flag48 = !Application.isPlaying && SpecificTree == null;
					if (flag48)
					{
						_LushLODTree[] array20 = componentsInChildren;
						foreach (_LushLODTree lushLODTree18 in array20)
						{
							lushLODTree18.TreeIsSetup = true;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002F86 RID: 12166 RVA: 0x001AA1E0 File Offset: 0x001A83E0
	[ExecuteInEditMode]
	private void Start()
	{
		this.ShaderSettingsChanged = 0;
		this.ShaderSettingsChanged_PREVIOUS = 0;
		bool flag = _LushLODTree.TreesManager != null;
		if (flag)
		{
			this.SetupHalfSrqTransition(true);
		}
		bool flag2 = this.UsingLinearColor == 2;
		if (flag2)
		{
			this.UsingLinearColor = 2;
			Shader.SetGlobalFloat("_LushLODTreeLinear", 0.8f);
		}
		else
		{
			bool flag3 = this.UsingLinearColor == 1;
			if (flag3)
			{
				this.UsingLinearColor = 1;
				Shader.SetGlobalFloat("_LushLODTreeLinear", 0f);
			}
			else
			{
				bool flag4 = this.UsingLinearColor == 0;
				if (flag4)
				{
					this.UsingLinearColor = 1;
					Shader.SetGlobalFloat("_LushLODTreeLinear", 0f);
				}
			}
		}
		this.Update();
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x001AA294 File Offset: 0x001A8494
	[ExecuteInEditMode]
	private void OnEnable()
	{
		bool flag = _LushLODTree.TreesManager != null;
		if (flag)
		{
			this.SetupHalfSrqTransition(true);
		}
		this.Update();
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x001AA2C4 File Offset: 0x001A84C4
	[ExecuteInEditMode]
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
		if (isPlaying)
		{
			base.CancelInvoke("Update");
		}
		else
		{
			base.CancelInvoke("Update");
			base.InvokeRepeating("Update", 5f, 5f);
		}
		bool flag = !(this.MainDirectionalLight == null) && this.MainDirectionalLight != null && !this.MainDirectionalLight.Equals(null);
		if (flag)
		{
			Light mainDirectionalLight = this.MainDirectionalLight;
			Vector3 vector = -(mainDirectionalLight.transform.rotation * Vector3.forward);
			bool flag2 = _LushLODTreesManager.PreviousLight != mainDirectionalLight || this.PreviousLightColor != mainDirectionalLight.color || this.PreviousLightIntensity != mainDirectionalLight.intensity || this.PreviousLightDir != vector;
			if (flag2)
			{
				_LushLODTreesManager.PreviousLight = mainDirectionalLight;
				this.PreviousLightColor = mainDirectionalLight.color;
				this.PreviousLightIntensity = mainDirectionalLight.intensity;
				this.PreviousLightDir = vector;
				Vector3 normalized = vector.normalized;
				Shader.SetGlobalColor("_LushLODTreeMainLightCol", mainDirectionalLight.color);
				Shader.SetGlobalFloat("_LushLODTreeMainLightIntensity", mainDirectionalLight.intensity);
				Shader.SetGlobalVector("_LushLODTreeMainLightDir", new Vector4(normalized.x, normalized.y, normalized.z, 2f));
			}
		}
		bool flag3 = _LushLODTree.TreesManager == null || _LushLODTree.TreesManager == null || _LushLODTree.TreesManager.Equals(null);
		if (flag3)
		{
			_LushLODTree.TreesManager = this;
			this.SetupHalfSrqTransition(true);
			bool flag4 = this.TreesRoot != null && this.TreesRoot != null && !this.TreesRoot.Equals(null);
			if (flag4)
			{
				_LushLODTree[] componentsInChildren = this.TreesRoot.GetComponentsInChildren<_LushLODTree>();
				_LushLODTree[] array = componentsInChildren;
				foreach (_LushLODTree lushLODTree in array)
				{
					bool flag5 = lushLODTree.Material_FullyOpaque_static == null;
					if (flag5)
					{
						_LushLODTree.SetupStaticMaterial(lushLODTree);
					}
				}
			}
		}
		bool flag6 = this.PrentsRootNotSet && this.TreesRoot != null && this.TreesRoot != null && !this.TreesRoot.Equals(null);
		if (flag6)
		{
			this.PrentsRootNotSet = false;
			bool flag7 = _LushLODTree.TreesManager == null;
			if (flag7)
			{
				_LushLODTree[] componentsInChildren2 = this.TreesRoot.GetComponentsInChildren<_LushLODTree>();
				_LushLODTree[] array3 = componentsInChildren2;
				foreach (_LushLODTree lushLODTree2 in array3)
				{
					lushLODTree2.TreesManager_ref = this;
				}
			}
		}
		bool flag8 = this.should_recalculate_parents;
		if (flag8)
		{
			this.should_recalculate_parents = false;
			base.CancelInvoke("OnDestroyParent");
			base.Invoke("OnDestroyParent", 3f);
		}
	}

	// Token: 0x0400294C RID: 10572
	[HideInInspector]
	public int ShaderSettingsChanged;

	// Token: 0x0400294D RID: 10573
	[HideInInspector]
	private int ShaderSettingsChanged_PREVIOUS;

	// Token: 0x0400294E RID: 10574
	public static float DefaultLodDistance = 250f;

	// Token: 0x0400294F RID: 10575
	[HideInInspector]
	public float AverageTreeScale = 1f;

	// Token: 0x04002950 RID: 10576
	private static Light PreviousLight;

	// Token: 0x04002951 RID: 10577
	[HideInInspector]
	public bool EnableUndo = true;

	// Token: 0x04002952 RID: 10578
	[HideInInspector]
	public List<_LushLODTree> AllParents;

	// Token: 0x04002953 RID: 10579
	public GameObject TreesRoot;

	// Token: 0x04002954 RID: 10580
	private bool PrentsRootNotSet = true;

	// Token: 0x04002955 RID: 10581
	[HideInInspector]
	public _LushLODTree FirstTreeFound;

	// Token: 0x04002956 RID: 10582
	[HideInInspector]
	public bool ShadowBakingReady;

	// Token: 0x04002957 RID: 10583
	[HideInInspector]
	public _LushLODTreesManager.RealTimeShadowSettings RealTimeShadowSetting;

	// Token: 0x04002958 RID: 10584
	[HideInInspector]
	public _LushLODTreesManager.RealTimeShadowSettings RealTimeShadowSetting_PREVIOUS;

	// Token: 0x04002959 RID: 10585
	[HideInInspector]
	public _LushLODTreesManager.BillBoardSettings BillBoardSetting;

	// Token: 0x0400295A RID: 10586
	[HideInInspector]
	public _LushLODTreesManager.BillBoardSettings BillBoardSetting_PREVIOUS;

	// Token: 0x0400295B RID: 10587
	[HideInInspector]
	public _LushLODTreesManager.ShadowReceiveSettings ShadowReceiveSetting;

	// Token: 0x0400295C RID: 10588
	[HideInInspector]
	public _LushLODTreesManager.ShadowReceiveSettings ShadowReceiveSetting_PREVIOUS;

	// Token: 0x0400295D RID: 10589
	[HideInInspector]
	public Color maincolor_HQTrees = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x0400295E RID: 10590
	[HideInInspector]
	public Color maincolor_LQTrees = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x0400295F RID: 10591
	[HideInInspector]
	public Color maincolor_LQTrees_PREVIOUS = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x04002960 RID: 10592
	[HideInInspector]
	public Color maincolor_HQTrees_PREVIOUS = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x04002961 RID: 10593
	[HideInInspector]
	public readonly Color maincolor_LQTrees_ORIGINAL = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x04002962 RID: 10594
	[HideInInspector]
	public float LOD_adjust = _LushLODTreesManager.DefaultLodDistance;

	// Token: 0x04002963 RID: 10595
	[HideInInspector]
	public float LOD_adjust_PREVIOUS = _LushLODTreesManager.DefaultLodDistance;

	// Token: 0x04002964 RID: 10596
	[HideInInspector]
	public _LushLODTreesManager.BillboardQualitySettings billboard_quality = _LushLODTreesManager.BillboardQualitySettings.Great;

	// Token: 0x04002965 RID: 10597
	[HideInInspector]
	public _LushLODTreesManager.BillboardQualitySettings billboard_quality_PREVIOUS = _LushLODTreesManager.BillboardQualitySettings.Great;

	// Token: 0x04002966 RID: 10598
	[HideInInspector]
	public bool DoColorReset;

	// Token: 0x04002967 RID: 10599
	[HideInInspector]
	public bool should_recalculate_parents;

	// Token: 0x04002968 RID: 10600
	[HideInInspector]
	public float halfsqrTransition;

	// Token: 0x04002969 RID: 10601
	[HideInInspector]
	public _LushLODTreesManager.Settings_BeforeBake BeforeBakeSettings;

	// Token: 0x0400296A RID: 10602
	[HideInInspector]
	public Light MainDirectionalLight;

	// Token: 0x0400296B RID: 10603
	private Color PreviousLightColor = Color.black;

	// Token: 0x0400296C RID: 10604
	private float PreviousLightIntensity = -1f;

	// Token: 0x0400296D RID: 10605
	private Vector3 PreviousLightDir = new Vector4(-1f, -1f, -1f);

	// Token: 0x0400296E RID: 10606
	[HideInInspector]
	public int UsingLinearColor;

	// Token: 0x0400296F RID: 10607
	private DateTime nowee;

	// Token: 0x02000691 RID: 1681
	public enum BillboardQualitySettings
	{
		// Token: 0x040030FD RID: 12541
		Low,
		// Token: 0x040030FE RID: 12542
		Medium,
		// Token: 0x040030FF RID: 12543
		Great,
		// Token: 0x04003100 RID: 12544
		Ultra
	}

	// Token: 0x02000692 RID: 1682
	public enum BillBoardSettings
	{
		// Token: 0x04003102 RID: 12546
		UseBoth,
		// Token: 0x04003103 RID: 12547
		BillboardsOnly,
		// Token: 0x04003104 RID: 12548
		HighQualityModelsOnly
	}

	// Token: 0x02000693 RID: 1683
	public enum ShadowReceiveSettings
	{
		// Token: 0x04003106 RID: 12550
		AllShadows,
		// Token: 0x04003107 RID: 12551
		SimpleShadows,
		// Token: 0x04003108 RID: 12552
		NoShadows
	}

	// Token: 0x02000694 RID: 1684
	public enum RealTimeShadowSettings
	{
		// Token: 0x0400310A RID: 12554
		UseBoth,
		// Token: 0x0400310B RID: 12555
		BillboardOnly,
		// Token: 0x0400310C RID: 12556
		RealtimeShadowsOff
	}

	// Token: 0x02000695 RID: 1685
	[HideInInspector]
	private enum BlendMode
	{
		// Token: 0x0400310E RID: 12558
		Opaque,
		// Token: 0x0400310F RID: 12559
		Cutout,
		// Token: 0x04003110 RID: 12560
		Fade,
		// Token: 0x04003111 RID: 12561
		Transparent
	}

	// Token: 0x02000696 RID: 1686
	[Serializable]
	public class Settings_BeforeBake
	{
		// Token: 0x04003112 RID: 12562
		[HideInInspector]
		public _LushLODTreesManager.BillboardQualitySettings billboard_quality_BEFOREBAKE = _LushLODTreesManager.BillboardQualitySettings.Great;

		// Token: 0x04003113 RID: 12563
		[HideInInspector]
		public _LushLODTreesManager.BillBoardSettings BillBoardSetting_BEFOREBAKE;

		// Token: 0x04003114 RID: 12564
		[HideInInspector]
		public Material AtlasMaterial;

		// Token: 0x04003115 RID: 12565
		[HideInInspector]
		public bool LOD0_isStatic;

		// Token: 0x04003116 RID: 12566
		[HideInInspector]
		public bool LOD1_isStatic;

		// Token: 0x04003117 RID: 12567
		[HideInInspector]
		public bool Tree_isStatic;

		// Token: 0x04003118 RID: 12568
		[HideInInspector]
		public ShadowCastingMode LOD1_shadowCastingMode = ShadowCastingMode.TwoSided;

		// Token: 0x04003119 RID: 12569
		[HideInInspector]
		public bool LOD1_receiveShadows;

		// Token: 0x0400311A RID: 12570
		[HideInInspector]
		public bool LOD1_enabled = true;

		// Token: 0x0400311B RID: 12571
		[HideInInspector]
		public MaterialGlobalIlluminationFlags LOD1_globalIlluminationFlags;
	}
}
