using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020004DD RID: 1245
public class _LushLODTreesUI : MonoBehaviour
{
	// Token: 0x06002F91 RID: 12177 RVA: 0x001AA904 File Offset: 0x001A8B04
	private void Start()
	{
		this.CurrentlyShadowsBaked = (SceneManager.GetActiveScene().name.EndsWith("scene_baked2") || SceneManager.GetActiveScene().name.EndsWith("Demo") || SceneManager.GetActiveScene().name.EndsWith("Demo Fast"));
		this.Panel1.color = this.Manager.maincolor_HQTrees_PREVIOUS;
		this.Panel2.color = this.Manager.maincolor_LQTrees_PREVIOUS;
		this.ColorsArray.Add(Color.black);
		this.ColorsArray.Add(Color.blue);
		this.ColorsArray.Add(Color.cyan);
		this.ColorsArray.Add(Color.gray);
		this.ColorsArray.Add(Color.green);
		this.ColorsArray.Add(Color.magenta);
		this.ColorsArray.Add(Color.red);
		this.ColorsArray.Add(Color.white);
		this.ColorsArray.Add(Color.yellow);
		bool flag = this.BillboardQualitySlider.value != (float)this.Manager.billboard_quality_PREVIOUS;
		if (flag)
		{
			this.BillboardQualitySlider.value = (float)this.Manager.billboard_quality_PREVIOUS;
			this.BillboardQualityChanged();
		}
		bool flag2 = this.BillboardSetting.value != (int)this.Manager.BillBoardSetting_PREVIOUS;
		if (flag2)
		{
			this.BillboardSetting.value = (int)this.Manager.BillBoardSetting_PREVIOUS;
			this.BillboardSettingChanged();
		}
		bool flag3 = this.LODSlider.value != this.Manager.LOD_adjust_PREVIOUS;
		if (flag3)
		{
			this.LODSlider.value = this.Manager.LOD_adjust_PREVIOUS;
			this.LODChanged();
		}
		this.WindZone.gameObject.SetActive(this.WindToggle.isOn);
		RenderSettings.fog = this.FogToggle.isOn;
		SceneManager.activeSceneChanged += this.SceneChanged;
		base.Invoke("meh", 0.001f);
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x001AAB3A File Offset: 0x001A8D3A
	private void meh()
	{
		this.ColorPicker.SetActive(false);
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x001AAB4C File Offset: 0x001A8D4C
	private void Update()
	{
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		float num = 1f / this.deltaTime;
		this.FPSText.text = "FPS: " + ((int)num).ToString();
		bool flag = Input.GetKeyDown(KeyCode.Escape) && Application.isPlaying;
		if (flag)
		{
			Application.Quit();
		}
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x001AABC4 File Offset: 0x001A8DC4
	private void LateUpdate()
	{
		bool flag = _LushLODTree.TreesManager.RealTimeShadowSetting_PREVIOUS == _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff;
		if (flag)
		{
			bool flag2 = _LushLODGaiaDemoMainDirLight.LightRef.shadows > LightShadows.None;
			if (flag2)
			{
				_LushLODGaiaDemoMainDirLight.LightRef.shadows = LightShadows.None;
			}
		}
		else
		{
			bool flag3 = _LushLODGaiaDemoMainDirLight.LightRef.shadows != LightShadows.Soft;
			if (flag3)
			{
				_LushLODGaiaDemoMainDirLight.LightRef.shadows = LightShadows.Soft;
			}
		}
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x001AAC28 File Offset: 0x001A8E28
	public void SceneChanged(Scene a, Scene b)
	{
		this.FogChanged();
		this.WindChanged();
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x001AAC39 File Offset: 0x001A8E39
	public void FogChanged()
	{
		RenderSettings.fog = this.FogToggle.isOn;
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x001AAC4D File Offset: 0x001A8E4D
	public void WindChanged()
	{
		this.WindZone.gameObject.SetActive(this.WindToggle.isOn);
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x001AAC6C File Offset: 0x001A8E6C
	public void SetColorBeingSelected(int which)
	{
		this.ColorBeingSelected = which;
	}

	// Token: 0x06002F99 RID: 12185 RVA: 0x001AAC78 File Offset: 0x001A8E78
	public void OnColorChanged(int Color)
	{
		int colorBeingSelected = this.ColorBeingSelected;
		int num = colorBeingSelected;
		if (num != 1)
		{
			if (num == 2)
			{
				this.Panel2.color = this.ColorsArray[Color];
			}
		}
		else
		{
			this.Panel1.color = this.ColorsArray[Color];
		}
	}

	// Token: 0x06002F9A RID: 12186 RVA: 0x001AACD0 File Offset: 0x001A8ED0
	public void FinalizeColor()
	{
		int colorBeingSelected = this.ColorBeingSelected;
		int num = colorBeingSelected;
		if (num != 1)
		{
			if (num == 2)
			{
				this.Manager.maincolor_LQTrees = this.Panel2.color;
				this.Manager.maincolor_LQTrees.a = 0.5f;
				this.Manager.ApplyAll(null, false, false);
			}
		}
		else
		{
			this.Manager.maincolor_HQTrees = this.Panel1.color;
			this.Manager.maincolor_HQTrees.a = 0.5f;
			this.Manager.ApplyAll(null, false, false);
		}
	}

	// Token: 0x06002F9B RID: 12187 RVA: 0x001AAD6C File Offset: 0x001A8F6C
	public void ResetColors()
	{
		this.Manager.maincolor_HQTrees = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		this.Manager.maincolor_LQTrees = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		this.Panel1.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		this.Panel2.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		this.Manager.ApplyAll(null, false, false);
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x001AAE1C File Offset: 0x001A901C
	public void LODChanged()
	{
		base.CancelInvoke();
		this.LODText.text = "LOD Distance:\n(" + ((int)this.LODSlider.value).ToString() + ")";
		base.InvokeRepeating("LODChanged2", 1f, 99f);
	}

	// Token: 0x06002F9D RID: 12189 RVA: 0x001AAE76 File Offset: 0x001A9076
	public void LODChanged2()
	{
		base.CancelInvoke();
		this.Manager.LOD_adjust = this.LODSlider.value;
		this.Manager.ApplyAll(null, false, false);
	}

	// Token: 0x06002F9E RID: 12190 RVA: 0x001AAEA8 File Offset: 0x001A90A8
	public void ShadowsSettingChanged()
	{
		switch (this.ShadowsSetting.value)
		{
		case 0:
		{
			this.Manager.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff;
			this.Manager.ApplyAll(null, false, false);
			bool flag = !this.CurrentlyShadowsBaked;
			if (flag)
			{
				this.CurrentlyShadowsBaked = true;
				SceneManager.LoadScene("Gaia Demo scene_baked2");
			}
			break;
		}
		case 1:
		{
			this.Manager.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.UseBoth;
			this.Manager.ApplyAll(null, false, false);
			bool currentlyShadowsBaked = this.CurrentlyShadowsBaked;
			if (currentlyShadowsBaked)
			{
				this.CurrentlyShadowsBaked = false;
				SceneManager.LoadScene("Gaia Demo scene2");
			}
			break;
		}
		case 2:
		{
			this.Manager.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.BillboardOnly;
			this.Manager.ApplyAll(null, false, false);
			bool currentlyShadowsBaked2 = this.CurrentlyShadowsBaked;
			if (currentlyShadowsBaked2)
			{
				this.CurrentlyShadowsBaked = false;
				SceneManager.LoadScene("Gaia Demo scene2");
			}
			break;
		}
		case 3:
		{
			this.Manager.RealTimeShadowSetting = _LushLODTreesManager.RealTimeShadowSettings.RealtimeShadowsOff;
			this.Manager.ApplyAll(null, false, false);
			bool currentlyShadowsBaked3 = this.CurrentlyShadowsBaked;
			if (currentlyShadowsBaked3)
			{
				this.CurrentlyShadowsBaked = false;
				SceneManager.LoadScene("Gaia Demo scene2");
			}
			break;
		}
		}
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x001AAFD8 File Offset: 0x001A91D8
	public void BillboardSettingChanged()
	{
		switch (this.BillboardSetting.value)
		{
		case 0:
			this.Manager.BillBoardSetting = _LushLODTreesManager.BillBoardSettings.UseBoth;
			this.BillboardQualitySlider.interactable = true;
			break;
		case 1:
			this.Manager.BillBoardSetting = _LushLODTreesManager.BillBoardSettings.BillboardsOnly;
			this.BillboardQualitySlider.interactable = true;
			break;
		case 2:
			this.Manager.BillBoardSetting = _LushLODTreesManager.BillBoardSettings.HighQualityModelsOnly;
			this.BillboardQualitySlider.interactable = false;
			break;
		}
		this.Manager.ApplyAll(null, false, false);
	}

	// Token: 0x06002FA0 RID: 12192 RVA: 0x001AB068 File Offset: 0x001A9268
	public void BillboardQualityChanged()
	{
		base.CancelInvoke();
		this.BillboardQualityText.text = "Billboard Quality:\n(" + (_LushLODTreesManager.BillboardQualitySettings)this.BillboardQualitySlider.value + ")";
		base.InvokeRepeating("BillboardQualityChanged2", 1f, 99f);
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x001AB0BF File Offset: 0x001A92BF
	public void BillboardQualityChanged2()
	{
		base.CancelInvoke();
		this.Manager.billboard_quality = (_LushLODTreesManager.BillboardQualitySettings)this.BillboardQualitySlider.value;
		this.Manager.ApplyAll(null, false, false);
	}

	// Token: 0x04002978 RID: 10616
	public _LushLODTreesManager Manager;

	// Token: 0x04002979 RID: 10617
	public int ColorBeingSelected;

	// Token: 0x0400297A RID: 10618
	public GameObject ColorPicker;

	// Token: 0x0400297B RID: 10619
	public Image Panel1;

	// Token: 0x0400297C RID: 10620
	public Image Panel2;

	// Token: 0x0400297D RID: 10621
	public Slider LODSlider;

	// Token: 0x0400297E RID: 10622
	public Text LODText;

	// Token: 0x0400297F RID: 10623
	public Slider BillboardQualitySlider;

	// Token: 0x04002980 RID: 10624
	public Text BillboardQualityText;

	// Token: 0x04002981 RID: 10625
	public Dropdown BillboardSetting;

	// Token: 0x04002982 RID: 10626
	public Dropdown ShadowsSetting;

	// Token: 0x04002983 RID: 10627
	public Toggle FogToggle;

	// Token: 0x04002984 RID: 10628
	public Toggle WindToggle;

	// Token: 0x04002985 RID: 10629
	public WindZone WindZone;

	// Token: 0x04002986 RID: 10630
	public Light DirectionalLight;

	// Token: 0x04002987 RID: 10631
	public Text FPSText;

	// Token: 0x04002988 RID: 10632
	private bool CurrentlyShadowsBaked = true;

	// Token: 0x04002989 RID: 10633
	private float deltaTime;

	// Token: 0x0400298A RID: 10634
	private _LushLODTreesManager.BillboardQualitySettings BillboardQualitySlider_PREVIOUS;

	// Token: 0x0400298B RID: 10635
	private List<Color> ColorsArray = new List<Color>();
}
