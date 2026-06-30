using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B9 RID: 441
public class HeroSkillInfoUICon : Window
{
	// Token: 0x06001432 RID: 5170 RVA: 0x000BEC50 File Offset: 0x000BCE50
	protected override void Awake()
	{
		base.Awake();
		this.heroName = this.UIGameObj.Get_GameObj(0).GetComponent<Text>();
		this.typeIcon = this.UIGameObj.Get_GameObj(1).GetComponent<Image>();
		this.starObj = this.UIGameObj.Get_GameObj(2);
		this.starObj.SetActive(false);
		this.gridObj = this.UIGameObj.Get_GameObj(3);
		this.gridObj.SetActive(false);
		this.heroIcon = this.UIGameObj.Get_GameObj(4).GetComponent<RawImage>();
		this.closeBtn = this.UIGameObj.Get_GameObj(5);
		EventTriggerListener.Get(this.closeBtn).onClick = new EventTriggerListener.VoidDelegate(this.OnClickClose);
		EventTriggerListener.Get(this.closeBtn).SetSoundStr("sound_main_mouse", "sound_main_buttonclick");
		HeroSkillInfoUICon.inst = this;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x000BED38 File Offset: 0x000BCF38
	public void ShowHeroSkill(int ntype, int heroid)
	{
		bool flag = ntype == this.nowType && heroid == this.nowHeroId;
		if (!flag)
		{
			this.nowType = ntype;
			this.nowHeroId = heroid;
			fUIManager.OnClearObj(this.szStar);
			fUIManager.OnClearObj(this.szGrid);
			string str = string.Empty;
			bool flag2 = heroid == 2000;
			if (flag2)
			{
				str = "luomo";
			}
			else
			{
				OpenHeroData oneHeroData = HeroDataManager.GetInstance().GetOneHeroData(heroid);
				bool flag3 = oneHeroData == null;
				if (flag3)
				{
					this.Close();
					return;
				}
				str = oneHeroData.unlockData.szSkinUnlock[1].sIconName;
			}
			this.isShowClose = (ntype == 0);
			bool flag4 = HeroSkillInfoUICon.isClosed && !this.isShowClose;
			if (flag4)
			{
				this.Close();
			}
			else
			{
				HeroSkillInfoUICon.isClosed = false;
				HeroData heroData = fGameManager.mloadConfig.AllHeroList[heroid];
				this.heroName.text = heroData.hname;
				this.typeIcon.sprite = Window.spriteName(AtlasType.ATLAS_SELECTHERO, this.GetHeroTypeSpName(heroData.hcareer));
				fUIManager.GetInstance().GetIconAsset("SelectHeroUI/headbig/" + str + ".txt", this.heroIcon);
				this.closeBtn.SetActive(this.isShowClose);
				HeroSkillInfo heroSkillInfo = fGameManager.mloadConfig.AllHeroSkillInfo[heroid];
				float num = 84f;
				for (int i = 0; i < heroSkillInfo.nStar; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.starObj);
					gameObject.transform.SetParent(this.starObj.transform.parent);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject.SetActive(true);
					Image component = gameObject.GetComponent<Image>();
					component.rectTransform.anchoredPosition3D = new Vector3(num, 5f, 0f);
					num += 36f;
					this.szStar.Add(gameObject);
				}
				Setting_Entity_Control control = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(heroid);
				for (int j = 0; j < heroSkillInfo.szInfoData.Count; j++)
				{
					SkillInfoData skillInfoData = heroSkillInfo.szInfoData[j];
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.gridObj);
					gameObject2.transform.SetParent(this.gridObj.transform.parent);
					gameObject2.transform.localPosition = skillInfoData.showPos;
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject2.SetActive(true);
					RawImage component2 = gameObject2.transform.Find("Background/RawImage").GetComponent<RawImage>();
					sysload.loadImage(component2, "skillicon/" + skillInfoData.sIconName + ".txt", new sysload.loadImageFinish(this.OnLoadIconOk));
					bool flag5 = skillInfoData.nCol == 3;
					if (flag5)
					{
						GameObject gameObject3 = gameObject2.transform.Find("Background/ui_dazhao").gameObject;
						gameObject3.SetActive(true);
					}
					Image component3 = gameObject2.transform.Find("KeyDesc/KeySp").GetComponent<Image>();
					Text component4 = gameObject2.transform.Find("KeyDesc/KeyText").GetComponent<Text>();
					int num2 = 0;
					string text = this.GetSettingKeyCode(control, skillInfoData.sKeyDesc);
					int num3 = text.IndexOf("Mouse0");
					bool flag6 = num3 == -1;
					if (flag6)
					{
						num3 = text.IndexOf("Mouse1");
						bool flag7 = num3 != -1;
						if (flag7)
						{
							num2 = 1;
						}
					}
					bool flag8 = num3 != -1;
					if (flag8)
					{
						component3.sprite = Window.spriteName(AtlasType.ATLAS_HEROSKILL, (num2 != 0) ? "heroskill_youjian" : "heroskill_zuojian");
						text = ((num2 != 0) ? text.Replace("Mouse1", string.Empty) : text.Replace("Mouse0", string.Empty));
						bool flag9 = num3 == 0;
						if (flag9)
						{
							component3.transform.SetSiblingIndex(0);
							component4.transform.SetSiblingIndex(1);
						}
						else
						{
							component3.transform.SetSiblingIndex(1);
							component4.transform.SetSiblingIndex(0);
						}
					}
					else
					{
						component3.gameObject.SetActive(false);
					}
					bool flag10 = !string.IsNullOrEmpty(text);
					if (flag10)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						text = string.Empty;
						for (int k = 0; k < array.Length; k++)
						{
							bool flag11 = !string.IsNullOrEmpty(array[k]);
							if (flag11)
							{
								bool flag12 = k > 0;
								if (flag12)
								{
									text += Language.GetLang(13003, new object[0]);
								}
								text += GameRoot.inst.Setting.Get_Key_Describe(array[k]);
							}
						}
						component4.text = text;
					}
					else
					{
						component4.gameObject.SetActive(false);
					}
					component4 = gameObject2.transform.Find("SkillType/TypeText").GetComponent<Text>();
					component4.text = skillInfoData.sType;
					component3 = gameObject2.transform.Find("SkillType/SkillName").GetComponent<Image>();
					component3.sprite = Window.spriteName(AtlasType.ATLAS_HEROSKILL, string.Format("heroskill_di{0}", skillInfoData.nCol));
					component4 = gameObject2.transform.Find("SkillType/SkillName/Text").GetComponent<Text>();
					component4.text = skillInfoData.sName;
					component4 = gameObject2.transform.Find("SkillDesc").GetComponent<Text>();
					component4.text = skillInfoData.sDesc;
					this.szGrid.Add(gameObject2);
				}
			}
		}
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x000BF36C File Offset: 0x000BD56C
	private string GetSettingKeyCode(Setting_Entity_Control control, string skey)
	{
		skey = skey.Replace("Key_Use_Weapon_1", control.Key_Use_Weapon_1);
		skey = skey.Replace("Key_Use_Weapon_2", control.Key_Use_Weapon_2);
		skey = skey.Replace("Key_Skill_1", control.Key_Skill_1);
		skey = skey.Replace("Key_Skill_2", control.Key_Skill_2);
		skey = skey.Replace("Key_Skill_3", control.Key_Skill_3);
		skey = skey.Replace("Key_Jump", control.Key_Jump);
		return skey;
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000BF3F1 File Offset: 0x000BD5F1
	private void OnLoadIconOk(object oImg, object oRaw)
	{
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000BF3F4 File Offset: 0x000BD5F4
	private string GetHeroTypeSpName(int nType)
	{
		string result = string.Empty;
		switch (nType)
		{
		case 1:
			result = "tuji";
			break;
		case 2:
			result = "fangyu";
			break;
		case 3:
			result = "zhongjia";
			break;
		case 4:
			result = "fuzhu";
			break;
		}
		return result;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000BF44B File Offset: 0x000BD64B
	private void OnClickClose(GameObject go)
	{
		this.isShowClose = false;
		this.Close();
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000BF45C File Offset: 0x000BD65C
	public override void Close()
	{
		bool flag = !this.isShowClose;
		if (flag)
		{
			base.Close();
			this.nowType = 0;
			this.nowHeroId = 0;
			HeroSkillInfoUICon.isClosed = false;
		}
	}

	// Token: 0x04000F97 RID: 3991
	public static HeroSkillInfoUICon inst;

	// Token: 0x04000F98 RID: 3992
	private Text heroName;

	// Token: 0x04000F99 RID: 3993
	private Image typeIcon;

	// Token: 0x04000F9A RID: 3994
	private GameObject starObj;

	// Token: 0x04000F9B RID: 3995
	private List<GameObject> szStar = new List<GameObject>();

	// Token: 0x04000F9C RID: 3996
	private GameObject gridObj;

	// Token: 0x04000F9D RID: 3997
	private List<GameObject> szGrid = new List<GameObject>();

	// Token: 0x04000F9E RID: 3998
	private RawImage heroIcon;

	// Token: 0x04000F9F RID: 3999
	private GameObject closeBtn;

	// Token: 0x04000FA0 RID: 4000
	public bool isShowClose = true;

	// Token: 0x04000FA1 RID: 4001
	public static bool isClosed;

	// Token: 0x04000FA2 RID: 4002
	private int nowType;

	// Token: 0x04000FA3 RID: 4003
	private int nowHeroId;
}
