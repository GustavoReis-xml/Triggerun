using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020004D7 RID: 1239
public class _LushLODTree : MonoBehaviour
{
	// Token: 0x06002F67 RID: 12135 RVA: 0x001A6B94 File Offset: 0x001A4D94
	public static Shader GetBillboardLeavesShader(_LushLODTreesManager.BillboardQualitySettings billboard_quality)
	{
		bool flag = _LushLODTree.ShaderReferences == null || _LushLODTree.ShaderReferences == null;
		if (flag)
		{
			_LushLODTree.ShaderReferences = new _LushLODTree.Shader_References();
		}
		return _LushLODTree.ShaderReferences.Tree_Leaves_Ultra;
	}

	// Token: 0x06002F68 RID: 12136 RVA: 0x001A6C28 File Offset: 0x001A4E28
	public static Shader GetBillboardLeavesFarShader(_LushLODTreesManager.BillboardQualitySettings billboard_quality, _LushLODTreesManager.BillBoardSettings BillBoardSetting)
	{
		bool flag = _LushLODTree.ShaderReferences == null || _LushLODTree.ShaderReferences == null;
		if (flag)
		{
			_LushLODTree.ShaderReferences = new _LushLODTree.Shader_References();
		}
		return (BillBoardSetting != _LushLODTreesManager.BillBoardSettings.BillboardsOnly) ? _LushLODTree.ShaderReferences.Tree_Leaves_Far_Ultra : _LushLODTree.ShaderReferences.Tree_Leaves_Far_Ultra_BB;
	}

	// Token: 0x06002F69 RID: 12137 RVA: 0x001A6CCC File Offset: 0x001A4ECC
	public static Shader GetLeavesShader(bool GetSimpleShadows, bool UsingFastLeavesShader, bool Ultra, int ForceFastState = -1)
	{
		if (!true)
		{
		}
		bool flag = ForceFastState != 0 && (ForceFastState == 1 || UsingFastLeavesShader);
		if (!true)
		{
		}
		bool flag2 = flag;
		string text = string.Empty;
		bool flag3 = Ultra && !flag2;
		if (flag3)
		{
			text = " Ultra";
		}
		Shader result;
		if (GetSimpleShadows)
		{
			bool flag4 = flag2;
			if (flag4)
			{
				bool flag5 = _LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows == null || _LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows == null || _LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows.Equals(null) || Ultra != _LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows.name.Contains(text);
				if (flag5)
				{
					_LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows = Shader.Find("Hidden/LushLODTree/Tree Creator Leaves Fast Optimized Simple Shadows" + text);
				}
				result = _LushLODTree.TreeCreatorLeavesOptimized_FAST_SimpleShadows;
			}
			else
			{
				bool flag6 = _LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows == null || _LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows == null || _LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows.Equals(null) || Ultra != _LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows.name.Contains(text);
				if (flag6)
				{
					_LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows = Shader.Find("Hidden/LushLODTree/Tree Creator Leaves Optimized Simple Shadows" + text);
				}
				result = _LushLODTree.TreeCreatorLeavesOptimized_SimpleShadows;
			}
		}
		else
		{
			bool flag7 = flag2;
			if (flag7)
			{
				bool flag8 = _LushLODTree.TreeCreatorLeavesOptimized_FAST == null || _LushLODTree.TreeCreatorLeavesOptimized_FAST == null || _LushLODTree.TreeCreatorLeavesOptimized_FAST.Equals(null) || Ultra != _LushLODTree.TreeCreatorLeavesOptimized_FAST.name.Contains(text);
				if (flag8)
				{
					_LushLODTree.TreeCreatorLeavesOptimized_FAST = Shader.Find("Hidden/LushLODTree/Tree Creator Leaves Fast Optimized" + text);
				}
				result = _LushLODTree.TreeCreatorLeavesOptimized_FAST;
			}
			else
			{
				bool flag9 = _LushLODTree.TreeCreatorLeavesOptimized == null || _LushLODTree.TreeCreatorLeavesOptimized == null || _LushLODTree.TreeCreatorLeavesOptimized.Equals(null) || Ultra != _LushLODTree.TreeCreatorLeavesOptimized.name.Contains(text);
				if (flag9)
				{
					_LushLODTree.TreeCreatorLeavesOptimized = Shader.Find("Hidden/LushLODTree/Tree Creator Leaves Optimized" + text);
				}
				result = _LushLODTree.TreeCreatorLeavesOptimized;
			}
		}
		return result;
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x001A6EBC File Offset: 0x001A50BC
	public static Shader GetBarkShader(bool GetSimpleShadows, bool Ultra)
	{
		string text = string.Empty;
		if (Ultra)
		{
			text = "_Ultra";
		}
		Shader result;
		if (GetSimpleShadows)
		{
			bool flag = _LushLODTree.TreeCreatorBarkOptimized_SimpleShadows == null || _LushLODTree.TreeCreatorBarkOptimized_SimpleShadows == null || _LushLODTree.TreeCreatorBarkOptimized_SimpleShadows.Equals(null) || Ultra != _LushLODTree.TreeCreatorBarkOptimized_SimpleShadows.name.Contains(text);
			if (flag)
			{
				_LushLODTree.TreeCreatorBarkOptimized_SimpleShadows = Shader.Find("Hidden/LushLODTree/HQTreeBark_SimpleShadows" + text);
			}
			result = _LushLODTree.TreeCreatorBarkOptimized_SimpleShadows;
		}
		else
		{
			bool flag2 = _LushLODTree.TreeCreatorBarkOptimized == null || _LushLODTree.TreeCreatorBarkOptimized == null || _LushLODTree.TreeCreatorBarkOptimized.Equals(null) || Ultra != _LushLODTree.TreeCreatorBarkOptimized.name.Contains(text);
			if (flag2)
			{
				_LushLODTree.TreeCreatorBarkOptimized = Shader.Find("Hidden/LushLODTree/HQTreeBark" + text);
			}
			result = _LushLODTree.TreeCreatorBarkOptimized;
		}
		return result;
	}

	// Token: 0x06002F6B RID: 12139 RVA: 0x001A6FA4 File Offset: 0x001A51A4
	private void OnDestroy()
	{
		bool isParent = this.IsParent;
		if (isParent)
		{
			_LushLODTree.TreesManager.OnDestroyParentS();
		}
	}

	// Token: 0x06002F6C RID: 12140 RVA: 0x001A6FC9 File Offset: 0x001A51C9
	public void RunStart()
	{
		this.Start();
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x001A6FD4 File Offset: 0x001A51D4
	private void Start()
	{
		bool flag = !this.TreeCreationFinished;
		if (!flag)
		{
			Transform parent = base.transform.parent;
			GameObject newManagersnewParentRoot = null;
			bool flag2 = parent != null && parent != null && !parent.Equals(null);
			if (flag2)
			{
				newManagersnewParentRoot = parent.gameObject;
			}
			_LushLODTree.CreateTreeManager(false, false, newManagersnewParentRoot);
			bool flag3 = this.lodDistance != _LushLODTree.TreesManager.LOD_adjust_PREVIOUS;
			if (flag3)
			{
				bool flag4 = !_LushLODTree.warnedaboutdynamicTrees;
				if (flag4)
				{
					_LushLODTree.warnedaboutdynamicTrees = true;
					Debug.LogWarning("Long Warning message: Tree doesn't have correct LOD distance! This can happen if you either: (a) haven't finished setting up your parents in your tree manager, or (b) you are spawning new trees during the game. If (a) is true, then go set up your tree manager before you start the game (see the documentation if you need help with that). If (b) is true, please note that after all your trees have finished being spawned, moved, and scaled, you MUST click the \"Re-Calculate Parents\" button in the manager, or call its underlying function through your scripts. Re-Calculation of parents does not happen automatically if the game is running, you must do it yourself. Any trees that you spawn while the game is running may not work correctly until you re-calculate parents. End of long warning message!");
				}
				this.lodDistance = _LushLODTree.TreesManager.LOD_adjust_PREVIOUS;
				_LushLODTree.TreesManager.ReCalculateParents(false);
			}
			bool flag5 = !this.IsParent && (this.MyParent == null || this.MyParent == null || this.MyParent.Equals(null));
			if (flag5)
			{
				bool flag6 = !_LushLODTree.warnedaboutdynamicTrees;
				if (flag6)
				{
					_LushLODTree.warnedaboutdynamicTrees = true;
					Debug.LogWarning("Long Warning message: Tree doesn't have any parent! This can happen if you either: (a) haven't finished setting up your parents in your tree manager, or (b) you are spawning new trees during the game. If (a) is true, then go set up your tree manager before you start the game (see the documentation if you need help with that). If (b) is true, please note that after all your trees have finished being spawned, moved, and scaled, you MUST click the \"Re-Calculate Parents\" button in the manager, or call its underlying function through your scripts. Re-Calculation of parents does not happen automatically if the game is running, you must do it yourself. Any trees that you spawn while the game is running may not work correctly until you re-calculate parents. End of long warning message!");
				}
				_LushLODTree.TreesManager.ReCalculateParents(false);
			}
			this.Reset();
			bool flag7 = this.TreeIsSetup || !(_LushLODTree.TreesManager != null) || _LushLODTree.TreesManager == null || _LushLODTree.TreesManager.Equals(null);
			if (!flag7)
			{
				this.TreesManager_ref = _LushLODTree.TreesManager;
				bool flag8 = _LushLODTree.TreesManager.TreesRoot != null && _LushLODTree.TreesManager.TreesRoot != null && !_LushLODTree.TreesManager.TreesRoot.Equals(null);
				if (flag8)
				{
					bool flag9 = base.transform.parent == null || base.transform.parent.gameObject != _LushLODTree.TreesManager.TreesRoot;
					if (flag9)
					{
						base.transform.parent = _LushLODTree.TreesManager.TreesRoot.transform;
					}
					_LushLODTree.TreesManager.ApplyAll(this, false, false);
				}
			}
		}
	}

	// Token: 0x06002F6E RID: 12142 RVA: 0x001A71DC File Offset: 0x001A53DC
	public static void CreateTreeManager(bool WarnIfalreadyexists, bool DoNotCreate = false, GameObject newManagersnewParentRoot = null)
	{
		bool flag = _LushLODTree.TreesManager == null || _LushLODTree.TreesManager == null || _LushLODTree.TreesManager.Equals(null);
		if (flag)
		{
			GameObject gameObject = GameObject.Find("_LushLODTreesManager");
			bool flag2 = gameObject != null;
			if (flag2)
			{
				_LushLODTree.TreesManager = gameObject.GetComponent<_LushLODTreesManager>();
				bool flag3 = _LushLODTree.TreesManager == null;
				if (flag3)
				{
					_LushLODTree.TreesManager = gameObject.AddComponent<_LushLODTreesManager>();
					bool flag4 = _LushLODTree.TreesManager.TreesRoot != null && _LushLODTree.TreesManager.TreesRoot != null && !_LushLODTree.TreesManager.TreesRoot.Equals(null);
					if (flag4)
					{
						_LushLODTree[] componentsInChildren = _LushLODTree.TreesManager.TreesRoot.GetComponentsInChildren<_LushLODTree>();
						_LushLODTree[] array = componentsInChildren;
						foreach (_LushLODTree lushLODTree in array)
						{
							lushLODTree.TreesManager_ref = _LushLODTree.TreesManager;
						}
					}
				}
				else if (WarnIfalreadyexists)
				{
					Debug.LogWarning("_LushLODTreesManager already exists in your scene. We found it. Everything is better now.");
				}
			}
			else if (!DoNotCreate)
			{
				_LushLODTree.TreesManager = new GameObject().AddComponent<_LushLODTreesManager>();
				_LushLODTree.TreesManager.gameObject.name = "_LushLODTreesManager";
				bool flag5 = _LushLODTree.TreesManager.TreesRoot != null && _LushLODTree.TreesManager.TreesRoot != null && !_LushLODTree.TreesManager.TreesRoot.Equals(null);
				if (flag5)
				{
					_LushLODTree[] componentsInChildren2 = _LushLODTree.TreesManager.TreesRoot.GetComponentsInChildren<_LushLODTree>();
					_LushLODTree[] array3 = componentsInChildren2;
					foreach (_LushLODTree lushLODTree2 in array3)
					{
						lushLODTree2.TreesManager_ref = _LushLODTree.TreesManager;
					}
				}
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					bool flag6 = newManagersnewParentRoot != null && newManagersnewParentRoot != null && !newManagersnewParentRoot.Equals(null);
					if (flag6)
					{
						_LushLODTree.TreesManager.TreesRoot = newManagersnewParentRoot;
						_LushLODTree.TreesManager.ReCalculateParents(false);
					}
					else
					{
						Debug.LogError("Your scene has no _LushLODTreesManager, and you appear to be adding trees to the scene, while the game is running, and you are adding trees to the root of the hierarchy. The _LushLODTreesManager has been automatically added to the scene, but its TreesRoot gameobject could not be automatically set because your tree was added to the root of the hierarchy. When you add trees to the scene, please make sure they are added as children of at least one gameobject above them in the hierarchy.");
					}
				}
				bool isPlaying2 = Application.isPlaying;
				if (isPlaying2)
				{
					Debug.LogWarning("Could not find _LushLODTreesManager! Creating it at runtime (this creates a delay loading the scene). To get rid of this delay, you should set up your tree manager before you start the game. To do this, exit the game, and select one of your trees, and click the button to create the LushLOD Trees Manager. Once that's done, then select it in your hierarchy, and follow the instructions to finish setting up your trees.");
				}
			}
		}
		else if (WarnIfalreadyexists)
		{
			Debug.LogWarning("_LushLODTreesManager already exists in your scene. We found it. Everything is better now.");
		}
	}

	// Token: 0x06002F6F RID: 12143 RVA: 0x001A7420 File Offset: 0x001A5620
	public static void SetupStaticMaterial(_LushLODTree SpecificTree)
	{
		bool flag = _LushLODTree.Materials_FullyOpaque_static == null || _LushLODTree.Materials_FullyOpaque_static == null || _LushLODTree.Materials_FullyOpaque_static.Equals(null) || _LushLODTree.Materials_FullyOpaque_static.Count == 0;
		if (flag)
		{
			_LushLODTree.Materials_FullyOpaque_static = new List<_LushLODTree.Material_FullyOpaque_References>();
			_LushLODTree.Material_FullyOpaque_References material_FullyOpaque_References = new _LushLODTree.Material_FullyOpaque_References(SpecificTree.Material_NotInstance_FullyOpaque, SpecificTree.Material_File_ID);
			_LushLODTree.Materials_FullyOpaque_static.Add(material_FullyOpaque_References);
			SpecificTree.Material_FullyOpaque_static = material_FullyOpaque_References;
		}
		else
		{
			bool flag2 = _LushLODTree.Materials_FullyOpaque_static.Count <= 0;
			if (!flag2)
			{
				bool flag3 = false;
				foreach (_LushLODTree.Material_FullyOpaque_References material_FullyOpaque_References2 in _LushLODTree.Materials_FullyOpaque_static)
				{
					bool flag4 = material_FullyOpaque_References2.Material_File_ID == SpecificTree.Material_File_ID;
					if (flag4)
					{
						flag3 = true;
						SpecificTree.Material_FullyOpaque_static = material_FullyOpaque_References2;
						break;
					}
				}
				bool flag5 = !flag3;
				if (flag5)
				{
					_LushLODTree.Material_FullyOpaque_References item = SpecificTree.Material_FullyOpaque_static = new _LushLODTree.Material_FullyOpaque_References(SpecificTree.Material_NotInstance_FullyOpaque, SpecificTree.Material_File_ID);
					_LushLODTree.Materials_FullyOpaque_static.Add(item);
				}
			}
		}
	}

	// Token: 0x06002F70 RID: 12144 RVA: 0x001A7550 File Offset: 0x001A5750
	public void Reset()
	{
		bool treeCreationFinished = this.TreeCreationFinished;
		if (treeCreationFinished)
		{
			this.curPosition = base.gameObject.transform.position;
			this.LodDistance_PREVIOUS = this.lodDistance;
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			bool flag = base.transform.FindChild("Lod_0");
			if (flag)
			{
				gameObject = base.transform.FindChild("Lod_0").gameObject;
			}
			bool flag2 = base.transform.FindChild("Lod_1");
			if (flag2)
			{
				gameObject2 = base.transform.FindChild("Lod_1").gameObject;
			}
			this.sqrTransition = _LushLODTreesManager.CalculateLODDistances(this.lodDistance, true, _LushLODTree.TreesManager.AverageTreeScale);
			this.lod0Rend = gameObject.GetComponent<Renderer>();
			this.lod1Rend = gameObject2.GetComponent<Renderer>();
			_LushLODTree.SetupStaticMaterial(this);
			bool flag3 = Application.isPlaying && this.Material_Instance_Transitioning.name != base.GetInstanceID().ToString();
			if (flag3)
			{
				this.Material_Instance_Transitioning = UnityEngine.Object.Instantiate<Material>(this.Material_Instance_Transitioning);
				this.Material_Instance_Transitioning.name = base.GetInstanceID().ToString();
			}
		}
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x001A7690 File Offset: 0x001A5890
	public void DoShaderUpdate()
	{
		bool flag = this.Material_FullyOpaque_static == null;
		if (flag)
		{
			_LushLODTree.SetupStaticMaterial(this);
		}
		bool flag2 = this.ShaderSettingsChanged == _LushLODTree.TreesManager.ShaderSettingsChanged;
		if (!flag2)
		{
			this.ShaderSettingsChanged = _LushLODTree.TreesManager.ShaderSettingsChanged;
			Shader billboardLeavesShader = _LushLODTree.GetBillboardLeavesShader(_LushLODTree.TreesManager.billboard_quality_PREVIOUS);
			Shader billboardLeavesFarShader = _LushLODTree.GetBillboardLeavesFarShader(_LushLODTree.TreesManager.billboard_quality_PREVIOUS, _LushLODTree.TreesManager.BillBoardSetting_PREVIOUS);
			bool flag3 = this.Material_Instance_Transitioning.name != base.GetInstanceID().ToString();
			if (flag3)
			{
				this.Material_Instance_Transitioning = UnityEngine.Object.Instantiate<Material>(this.Material_Instance_Transitioning);
				this.Material_Instance_Transitioning.name = base.GetInstanceID().ToString();
			}
			bool flag4 = this.lod1Rend.sharedMaterial.name != base.GetInstanceID().ToString() && this.lod1Rend.sharedMaterial.name != this.Material_FullyOpaque_static.Material_FullyOpaque_static.name;
			if (flag4)
			{
				this.lod1Rend.sharedMaterial = this.Material_Instance_Transitioning;
			}
			bool flag5 = !this.lod1Rend.sharedMaterial.shader.name.Contains("Far");
			if (flag5)
			{
				this.lod1Rend.sharedMaterial.shader = billboardLeavesShader;
			}
			else
			{
				bool flag6 = this.lod1Rend.sharedMaterial.shader.name.Contains("Far");
				if (flag6)
				{
					this.lod1Rend.sharedMaterial.shader = billboardLeavesFarShader;
				}
			}
			this.Material_Instance_Transitioning.shader = billboardLeavesShader;
			this.Material_FullyOpaque_static.Material_FullyOpaque_static.shader = billboardLeavesFarShader;
			Shader leavesShader = _LushLODTree.GetLeavesShader(true, this.UsingFastLeavesShader, _LushLODTree.TreesManager.UsingUltraShader(true), -1);
			Shader leavesShader2 = _LushLODTree.GetLeavesShader(false, this.UsingFastLeavesShader, _LushLODTree.TreesManager.UsingUltraShader(true), -1);
			Shader barkShader = _LushLODTree.GetBarkShader(true, _LushLODTree.TreesManager.UsingUltraShader(true));
			Shader barkShader2 = _LushLODTree.GetBarkShader(false, _LushLODTree.TreesManager.UsingUltraShader(true));
			switch (_LushLODTree.TreesManager.ShadowReceiveSetting_PREVIOUS)
			{
			case _LushLODTreesManager.ShadowReceiveSettings.AllShadows:
			{
				Material[] materials = this.lod0Rend.materials;
				foreach (Material material in materials)
				{
					bool flag7 = material.shader.name.Contains("Leaves");
					if (flag7)
					{
						material.shader = leavesShader2;
					}
					else
					{
						material.shader = barkShader2;
					}
				}
				break;
			}
			case _LushLODTreesManager.ShadowReceiveSettings.SimpleShadows:
			{
				Material[] materials2 = this.lod0Rend.materials;
				foreach (Material material2 in materials2)
				{
					bool flag8 = material2.shader.name.Contains("Leaves");
					if (flag8)
					{
						material2.shader = leavesShader;
					}
					else
					{
						material2.shader = barkShader;
					}
				}
				break;
			}
			case _LushLODTreesManager.ShadowReceiveSettings.NoShadows:
			{
				Material[] materials3 = this.lod0Rend.materials;
				foreach (Material material3 in materials3)
				{
					bool flag9 = material3.shader.name.Contains("Leaves");
					if (flag9)
					{
						material3.shader = leavesShader2;
					}
					else
					{
						material3.shader = barkShader2;
					}
				}
				break;
			}
			}
		}
	}

	// Token: 0x06002F72 RID: 12146 RVA: 0x001A7A14 File Offset: 0x001A5C14
	private void Update()
	{
		bool flag = Camera.main == null;
		if (!flag)
		{
			bool flag2 = _LushLODTree.TreesManager.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra;
			if (flag2)
			{
				bool flag3 = Time.time < this.timeSkip;
				if (flag3)
				{
					return;
				}
				this.timeSkip = Time.time + UnityEngine.Random.Range(0.03f, 0.06f) + Time.deltaTime;
			}
			bool flag4 = !this.TreeCreationFinished;
			if (!flag4)
			{
				bool flag5 = this.ShaderSettingsChanged != _LushLODTree.TreesManager.ShaderSettingsChanged;
				if (flag5)
				{
					this.DoShaderUpdate();
				}
				bool flag6 = this.BillBoardSetting > _LushLODTreesManager.BillBoardSettings.UseBoth;
				if (flag6)
				{
					base.enabled = false;
					this.MyChildrenEnabled = false;
				}
				else
				{
					bool flag7 = this.LodDistance_PREVIOUS != this.lodDistance;
					if (flag7)
					{
						this.Reset();
					}
					this.curDistance = (this.curPosition - Camera.main.transform.position).sqrMagnitude;
					bool flag8 = this.curDistance < this.sqrTransition[4];
					if (flag8)
					{
						bool flag9 = this.curDistance > this.sqrTransition[3];
						if (flag9)
						{
							bool flag10 = !this.lod1Rend.enabled;
							if (flag10)
							{
								this.lod1Rend.enabled = true;
							}
							bool flag11 = this.lod1Rend.sharedMaterial != this.Material_FullyOpaque_static.Material_FullyOpaque_static;
							if (flag11)
							{
								this.lod1Rend.sharedMaterial = this.Material_FullyOpaque_static.Material_FullyOpaque_static;
							}
							bool shadowsOnlyIsSet = this.ShadowsOnlyIsSet;
							if (shadowsOnlyIsSet)
							{
								this.ShadowsOnlyIsSet = false;
								bool flag12 = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true) > ShadowCastingMode.Off;
								if (flag12)
								{
									this.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
								}
								else
								{
									this.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
								}
							}
							bool enabled = this.lod0Rend.enabled;
							if (enabled)
							{
								this.lod0Rend.enabled = false;
							}
						}
						else
						{
							bool flag13 = this.curDistance > this.sqrTransition[2];
							if (flag13)
							{
								bool flag14 = !this.lod1Rend.enabled;
								if (flag14)
								{
									this.lod1Rend.enabled = true;
								}
								bool flag15 = this.lod1Rend.sharedMaterial != this.Material_FullyOpaque_static.Material_FullyOpaque_static;
								if (flag15)
								{
									this.lod1Rend.sharedMaterial = this.Material_FullyOpaque_static.Material_FullyOpaque_static;
								}
								bool shadowsOnlyIsSet2 = this.ShadowsOnlyIsSet;
								if (shadowsOnlyIsSet2)
								{
									this.ShadowsOnlyIsSet = false;
									bool flag16 = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true) > ShadowCastingMode.Off;
									if (flag16)
									{
										this.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
									}
									else
									{
										this.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
									}
								}
								float num = Mathf.InverseLerp(this.sqrTransition[3], this.sqrTransition[1], this.curDistance);
								bool flag17 = !this.lod0Rend.enabled;
								if (flag17)
								{
									this.lod0Rend.enabled = true;
								}
								bool flag18 = this.rendIsFloat != num;
								if (flag18)
								{
									this.rendIs1 = false;
									this.rendIsFloat = num;
									float num2 = Mathf.Clamp01(this.Original_ShadowStrength - Mathf.Clamp01(0.5f - num));
									this.lastShadowStr = ((this.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra) ? num2 : Mathf.Max(1f, num2));
									Material[] materials = this.lod0Rend.materials;
									foreach (Material material in materials)
									{
										material.SetFloat("_Transparency", num);
										material.SetFloat("_ShadowStrength", this.lastShadowStr);
									}
								}
							}
							else
							{
								bool flag19 = this.curDistance > this.sqrTransition[1];
								if (flag19)
								{
									float num3 = Mathf.InverseLerp(this.sqrTransition[3], this.sqrTransition[1], this.curDistance);
									float num4 = Mathf.InverseLerp(this.sqrTransition[2], this.sqrTransition[0], this.curDistance);
									float num5 = num3;
									bool flag20 = this.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra;
									if (flag20)
									{
										num5 = Mathf.InverseLerp(_LushLODTree.TreesManager.halfsqrTransition, 0f, this.curDistance);
									}
									bool flag21 = !this.lod1Rend.enabled;
									if (flag21)
									{
										this.lod1Rend.enabled = true;
									}
									bool flag22 = this.lod1Rend.sharedMaterial != this.Material_Instance_Transitioning;
									if (flag22)
									{
										this.lod1Rend.sharedMaterial = this.Material_Instance_Transitioning;
									}
									this.lod1Rend.sharedMaterial.SetFloat("_Transparency", 1f - num4);
									bool shadowsOnlyIsSet3 = this.ShadowsOnlyIsSet;
									if (shadowsOnlyIsSet3)
									{
										this.ShadowsOnlyIsSet = false;
										bool flag23 = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true) > ShadowCastingMode.Off;
										if (flag23)
										{
											this.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
										}
										else
										{
											this.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
										}
									}
									switch (this.RealTimeShadowSetting)
									{
									case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
									{
										bool flag24 = this.billboard_quality_PREVIOUS == _LushLODTreesManager.BillboardQualitySettings.Ultra;
										if (flag24)
										{
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f - num5);
										}
										else
										{
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f);
										}
										break;
									}
									case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
										this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f);
										break;
									case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
										this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 0f);
										break;
									}
									bool flag25 = !this.lod0Rend.enabled;
									if (flag25)
									{
										this.lod0Rend.enabled = true;
									}
									bool flag26 = this.rendIsFloat != num3;
									if (flag26)
									{
										this.rendIs1 = false;
										this.rendIsFloat = num3;
										float num6 = Mathf.Clamp01(this.Original_ShadowStrength - Mathf.Clamp01(0.5f - num5));
										this.lastShadowStr = ((this.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra) ? num6 : Mathf.Max(1f, num6));
										Material[] materials2 = this.lod0Rend.materials;
										foreach (Material material2 in materials2)
										{
											material2.SetFloat("_Transparency", num3);
											material2.SetFloat("_ShadowStrength", this.lastShadowStr);
										}
									}
								}
								else
								{
									bool flag27 = this.curDistance > this.sqrTransition[0];
									if (flag27)
									{
										float num7 = Mathf.InverseLerp(this.sqrTransition[2], this.sqrTransition[0], this.curDistance);
										bool flag28 = !this.lod1Rend.enabled;
										if (flag28)
										{
											this.lod1Rend.enabled = true;
										}
										bool flag29 = this.lod1Rend.sharedMaterial != this.Material_Instance_Transitioning;
										if (flag29)
										{
											this.lod1Rend.sharedMaterial = this.Material_Instance_Transitioning;
										}
										this.lod1Rend.sharedMaterial.SetFloat("_Transparency", 1f - num7);
										bool shadowsOnlyIsSet4 = this.ShadowsOnlyIsSet;
										if (shadowsOnlyIsSet4)
										{
											this.ShadowsOnlyIsSet = false;
											bool flag30 = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true) > ShadowCastingMode.Off;
											if (flag30)
											{
												this.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
											}
											else
											{
												this.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
											}
										}
										switch (this.RealTimeShadowSetting)
										{
										case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
										{
											float num8 = Mathf.InverseLerp(_LushLODTree.TreesManager.halfsqrTransition, 0f, this.curDistance);
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f - num8);
											break;
										}
										case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f);
											break;
										case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 0f);
											break;
										}
										bool flag31 = !this.lod0Rend.enabled;
										if (flag31)
										{
											this.lod0Rend.enabled = true;
										}
										bool flag32 = !this.rendIs1;
										if (flag32)
										{
											this.rendIs1 = true;
											this.rendIsFloat = 1f;
											this.lastShadowStr = ((this.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra) ? this.Original_ShadowStrength : Mathf.Max(1f, this.Original_ShadowStrength));
											Material[] materials3 = this.lod0Rend.materials;
											foreach (Material material3 in materials3)
											{
												material3.SetFloat("_Transparency", 1f);
												material3.SetFloat("_ShadowStrength", this.lastShadowStr);
											}
										}
									}
									else
									{
										bool flag33 = !this.lod1Rend.enabled;
										if (flag33)
										{
											this.lod1Rend.enabled = true;
										}
										bool flag34 = this.lod1Rend.sharedMaterial != this.Material_Instance_Transitioning;
										if (flag34)
										{
											this.lod1Rend.sharedMaterial = this.Material_Instance_Transitioning;
										}
										this.lod1Rend.sharedMaterial.SetFloat("_Transparency", 0f);
										this.ShadowsOnlyIsSet = true;
										ShadowCastingMode billboardShadowCastingMode = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true);
										bool flag35 = billboardShadowCastingMode != ShadowCastingMode.Off && !_LushLODPostProcessor_DisableHQ.DisablingHQ;
										if (flag35)
										{
											this.lod1Rend.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
										}
										else
										{
											this.lod1Rend.shadowCastingMode = billboardShadowCastingMode;
										}
										switch (this.RealTimeShadowSetting)
										{
										case _LushLODTreesManager.RealTimeShadowSettings.UseBoth:
										{
											float num9 = Mathf.InverseLerp(_LushLODTree.TreesManager.halfsqrTransition, 0f, this.curDistance);
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f - num9);
											break;
										}
										case _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly:
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 1f);
											break;
										case _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff:
											this.lod1Rend.sharedMaterial.SetFloat("_ShadowTransparency", 0f);
											break;
										}
										bool flag36 = !this.lod0Rend.enabled;
										if (flag36)
										{
											this.lod0Rend.enabled = true;
										}
										bool flag37 = !this.rendIs1;
										if (flag37)
										{
											this.rendIs1 = true;
											this.rendIsFloat = 1f;
											this.lastShadowStr = ((this.billboard_quality_PREVIOUS != _LushLODTreesManager.BillboardQualitySettings.Ultra) ? this.Original_ShadowStrength : Mathf.Max(1f, this.Original_ShadowStrength));
											Material[] materials4 = this.lod0Rend.materials;
											foreach (Material material4 in materials4)
											{
												material4.SetFloat("_Transparency", 1f);
												material4.SetFloat("_ShadowStrength", this.lastShadowStr);
											}
										}
									}
								}
							}
						}
						bool flag38 = this.MyChildrenEnabled && !this.CheckChildren;
						if (!flag38)
						{
							bool flag39 = this.MyChildren != null && this.MyChildren != null && !this.MyChildren.Equals(null) && this.MyChildren.Count > 0;
							if (flag39)
							{
								foreach (_LushLODTree lushLODTree in this.MyChildren)
								{
									bool flag40 = lushLODTree == null || lushLODTree == null || lushLODTree.Equals(null);
									if (flag40)
									{
										bool flag41 = !_LushLODTree.TreesManager.should_recalculate_parents && !_LushLODTree.TreesManager.IsInvoking("OnDestroyParent");
										if (flag41)
										{
											_LushLODTree.TreesManager.OnDestroyParentS();
										}
										return;
									}
									bool flag42 = !lushLODTree.enabled;
									if (flag42)
									{
										lushLODTree.enabled = true;
									}
								}
							}
							this.CheckChildren = false;
							this.MyChildrenEnabled = true;
						}
					}
					else
					{
						bool flag43 = !this.lod1Rend.enabled;
						if (flag43)
						{
							this.lod1Rend.enabled = true;
						}
						bool flag44 = this.lod1Rend.sharedMaterial != this.Material_FullyOpaque_static.Material_FullyOpaque_static;
						if (flag44)
						{
							this.lod1Rend.sharedMaterial = this.Material_FullyOpaque_static.Material_FullyOpaque_static;
						}
						bool shadowsOnlyIsSet5 = this.ShadowsOnlyIsSet;
						if (shadowsOnlyIsSet5)
						{
							this.ShadowsOnlyIsSet = false;
							bool flag45 = _LushLODTree.TreesManager.GetBillboardShadowCastingMode(true) > ShadowCastingMode.Off;
							if (flag45)
							{
								this.lod1Rend.shadowCastingMode = ShadowCastingMode.TwoSided;
							}
							else
							{
								this.lod1Rend.shadowCastingMode = ShadowCastingMode.Off;
							}
						}
						bool enabled2 = this.lod0Rend.enabled;
						if (enabled2)
						{
							this.lod0Rend.enabled = false;
						}
						bool flag46 = !this.MyChildrenEnabled && !this.CheckChildren;
						if (!flag46)
						{
							bool flag47 = this.MyChildren != null && this.MyChildren != null && !this.MyChildren.Equals(null) && this.MyChildren.Count > 0;
							if (flag47)
							{
								foreach (_LushLODTree lushLODTree2 in this.MyChildren)
								{
									bool flag48 = lushLODTree2 == null || lushLODTree2 == null || lushLODTree2.Equals(null);
									if (flag48)
									{
										bool flag49 = !_LushLODTree.TreesManager.should_recalculate_parents && !_LushLODTree.TreesManager.IsInvoking("OnDestroyParent");
										if (flag49)
										{
											_LushLODTree.TreesManager.OnDestroyParentS();
										}
										return;
									}
									bool flag50 = !lushLODTree2.lod1Rend.enabled;
									if (flag50)
									{
										lushLODTree2.lod1Rend.enabled = true;
									}
									bool flag51 = lushLODTree2.lod1Rend.sharedMaterial != lushLODTree2.Material_FullyOpaque_static.Material_FullyOpaque_static;
									if (flag51)
									{
										lushLODTree2.lod1Rend.sharedMaterial = lushLODTree2.Material_FullyOpaque_static.Material_FullyOpaque_static;
									}
									bool enabled3 = lushLODTree2.lod0Rend.enabled;
									if (enabled3)
									{
										lushLODTree2.lod0Rend.enabled = false;
									}
									bool enabled4 = lushLODTree2.enabled;
									if (enabled4)
									{
										lushLODTree2.enabled = false;
									}
								}
							}
							this.CheckChildren = false;
							this.MyChildrenEnabled = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x0400290D RID: 10509
	public static _LushLODTreesManager TreesManager;

	// Token: 0x0400290E RID: 10510
	public int ShaderSettingsChanged = -1;

	// Token: 0x0400290F RID: 10511
	public static _LushLODTree.Shader_References ShaderReferences;

	// Token: 0x04002910 RID: 10512
	public static List<_LushLODTree.Material_FullyOpaque_References> Materials_FullyOpaque_static;

	// Token: 0x04002911 RID: 10513
	private static bool warnedaboutdynamicTrees;

	// Token: 0x04002912 RID: 10514
	public static Shader TreeCreatorLeavesOptimized_FAST;

	// Token: 0x04002913 RID: 10515
	public static Shader TreeCreatorLeavesOptimized;

	// Token: 0x04002914 RID: 10516
	public static Shader TreeCreatorLeavesOptimized_FAST_SimpleShadows;

	// Token: 0x04002915 RID: 10517
	public static Shader TreeCreatorLeavesOptimized_SimpleShadows;

	// Token: 0x04002916 RID: 10518
	public static Shader TreeCreatorBarkOptimized_FAST;

	// Token: 0x04002917 RID: 10519
	public static Shader TreeCreatorBarkOptimized;

	// Token: 0x04002918 RID: 10520
	public static Shader TreeCreatorBarkOptimized_FAST_SimpleShadows;

	// Token: 0x04002919 RID: 10521
	public static Shader TreeCreatorBarkOptimized_SimpleShadows;

	// Token: 0x0400291A RID: 10522
	public _LushLODTree.Material_FullyOpaque_References Material_FullyOpaque_static;

	// Token: 0x0400291B RID: 10523
	public string LushLODTree_VersionNumber;

	// Token: 0x0400291C RID: 10524
	public _LushLODTreesManager TreesManager_ref;

	// Token: 0x0400291D RID: 10525
	public int Material_File_ID;

	// Token: 0x0400291E RID: 10526
	public Material Material_NotInstance_FullyOpaque;

	// Token: 0x0400291F RID: 10527
	public Material Material_Instance_Transitioning;

	// Token: 0x04002920 RID: 10528
	public _LushLODTreesManager.BillBoardSettings BillBoardSetting;

	// Token: 0x04002921 RID: 10529
	public _LushLODTreesManager.ShadowReceiveSettings ShadowReceiveSetting;

	// Token: 0x04002922 RID: 10530
	public _LushLODTreesManager.RealTimeShadowSettings RealTimeShadowSetting;

	// Token: 0x04002923 RID: 10531
	public List<Color> maincolor_HQTrees_ORIGINALS;

	// Token: 0x04002924 RID: 10532
	public Color AverageColor_Bark;

	// Token: 0x04002925 RID: 10533
	public Color AverageColor_Leaves;

	// Token: 0x04002926 RID: 10534
	public bool ShadowBakingReady;

	// Token: 0x04002927 RID: 10535
	public bool CheckChildren;

	// Token: 0x04002928 RID: 10536
	private float LodDistance_PREVIOUS = _LushLODTreesManager.DefaultLodDistance;

	// Token: 0x04002929 RID: 10537
	public float lodDistance = _LushLODTreesManager.DefaultLodDistance;

	// Token: 0x0400292A RID: 10538
	public _LushLODTreesManager.BillboardQualitySettings billboard_quality_PREVIOUS = _LushLODTreesManager.BillboardQualitySettings.Great;

	// Token: 0x0400292B RID: 10539
	public _LushLODTree MyParent;

	// Token: 0x0400292C RID: 10540
	public bool IsParent;

	// Token: 0x0400292D RID: 10541
	public List<_LushLODTree> MyChildren;

	// Token: 0x0400292E RID: 10542
	public bool MyChildrenEnabled = true;

	// Token: 0x0400292F RID: 10543
	public float Original_ShadowStrength;

	// Token: 0x04002930 RID: 10544
	public string Original_TreeName;

	// Token: 0x04002931 RID: 10545
	public string Original_UniqueID;

	// Token: 0x04002932 RID: 10546
	public float[] sqrTransition;

	// Token: 0x04002933 RID: 10547
	public Renderer lod0Rend;

	// Token: 0x04002934 RID: 10548
	public Renderer lod1Rend;

	// Token: 0x04002935 RID: 10549
	private float timeSkip;

	// Token: 0x04002936 RID: 10550
	private float curDistance;

	// Token: 0x04002937 RID: 10551
	private Vector3 curPosition;

	// Token: 0x04002938 RID: 10552
	public bool rendIs1 = true;

	// Token: 0x04002939 RID: 10553
	public float rendIsFloat = 1f;

	// Token: 0x0400293A RID: 10554
	public float lastShadowStr = -1f;

	// Token: 0x0400293B RID: 10555
	public bool ShadowsOnlyIsSet;

	// Token: 0x0400293C RID: 10556
	public bool TreeIsSetup;

	// Token: 0x0400293D RID: 10557
	public bool TreeCreationFinished;

	// Token: 0x0400293E RID: 10558
	public bool UsingFastLeavesShader;

	// Token: 0x0400293F RID: 10559
	public bool UsingFastBarkShader;

	// Token: 0x0200068F RID: 1679
	public class Shader_References
	{
		// Token: 0x06003977 RID: 14711 RVA: 0x001C89C0 File Offset: 0x001C6BC0
		public Shader_References()
		{
			this.Tree_Leaves_Ultra = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Ultra");
			this.Tree_Leaves2 = Shader.Find("Hidden/LushLODTree/Tree_Leaves2");
			this.Tree_Leaves3 = Shader.Find("Hidden/LushLODTree/Tree_Leaves3");
			this.Tree_Leaves4 = Shader.Find("Hidden/LushLODTree/Tree_Leaves4");
			this.Tree_Leaves_Far_Ultra = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Far_Ultra");
			this.Tree_Leaves_Far_Ultra_BB = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Far_Ultra_BB");
			this.Tree_Leaves_Far2 = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Far2");
			this.Tree_Leaves_Far3 = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Far3");
			this.Tree_Leaves_Far4 = Shader.Find("Hidden/LushLODTree/Tree_Leaves_Far4");
		}

		// Token: 0x040030F0 RID: 12528
		public bool ShaderRefrencesSetup;

		// Token: 0x040030F1 RID: 12529
		public Shader Tree_Leaves_Ultra;

		// Token: 0x040030F2 RID: 12530
		public Shader Tree_Leaves2;

		// Token: 0x040030F3 RID: 12531
		public Shader Tree_Leaves3;

		// Token: 0x040030F4 RID: 12532
		public Shader Tree_Leaves4;

		// Token: 0x040030F5 RID: 12533
		public Shader Tree_Leaves_Far_Ultra;

		// Token: 0x040030F6 RID: 12534
		public Shader Tree_Leaves_Far_Ultra_BB;

		// Token: 0x040030F7 RID: 12535
		public Shader Tree_Leaves_Far2;

		// Token: 0x040030F8 RID: 12536
		public Shader Tree_Leaves_Far3;

		// Token: 0x040030F9 RID: 12537
		public Shader Tree_Leaves_Far4;
	}

	// Token: 0x02000690 RID: 1680
	public class Material_FullyOpaque_References
	{
		// Token: 0x06003978 RID: 14712 RVA: 0x001C8A65 File Offset: 0x001C6C65
		public Material_FullyOpaque_References(Material Material_NotInstance_FullyOpaque, int Material_File_ID)
		{
			this.Material_FullyOpaque_static = UnityEngine.Object.Instantiate<Material>(Material_NotInstance_FullyOpaque);
			this.Material_File_ID = Material_File_ID;
			this.Material_FullyOpaque_static.name = "Static_FullyOpaque_Far__Instanced_No." + Material_File_ID.ToString();
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x001C8AA0 File Offset: 0x001C6CA0
		public static void SetColor(Color color)
		{
			foreach (_LushLODTree.Material_FullyOpaque_References material_FullyOpaque_References in _LushLODTree.Materials_FullyOpaque_static)
			{
				material_FullyOpaque_References.Material_FullyOpaque_static.color = color;
			}
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x001C8B00 File Offset: 0x001C6D00
		public static void SetShader(Shader shader)
		{
			foreach (_LushLODTree.Material_FullyOpaque_References material_FullyOpaque_References in _LushLODTree.Materials_FullyOpaque_static)
			{
				material_FullyOpaque_References.Material_FullyOpaque_static.shader = shader;
			}
		}

		// Token: 0x040030FA RID: 12538
		public Material Material_FullyOpaque_static;

		// Token: 0x040030FB RID: 12539
		public int Material_File_ID;
	}
}
