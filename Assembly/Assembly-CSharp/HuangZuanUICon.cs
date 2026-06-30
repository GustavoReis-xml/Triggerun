using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E3 RID: 483
public class HuangZuanUICon : Window
{
	// Token: 0x060015C7 RID: 5575 RVA: 0x000CB73C File Offset: 0x000C993C
	protected override void Awake()
	{
		base.Awake();
		HuangZuanUICon.inst = this;
		this.QQType = Fun.GetQQ_Zuan_Type(null);
		this.ShowUI_TxtData();
		this.ShowUI_ImgData();
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x000CB768 File Offset: 0x000C9968
	private void ShowUI_TxtData()
	{
		bool flag = this.QQType == "hz";
		if (flag)
		{
			this.UIGameObj.Get_GameObj(0).GetComponent<Text>().text = Language.GetLang(92002, new object[0]);
			this.UIGameObj.Get_GameObj(1).GetComponent<Text>().text = Language.GetLang(92004, new object[0]);
			this.UIGameObj.Get_GameObj(2).GetComponent<Text>().text = Language.GetLang(92006, new object[0]);
			this.UIGameObj.Get_GameObj(3).GetComponent<Text>().text = Language.GetLang(92008, new object[0]);
			this.UIGameObj.Get_GameObj(4).GetComponent<Text>().text = Language.GetLang(92034, new object[0]);
			this.UIGameObj.Get_GameObj(5).GetComponent<Text>().text = Language.GetLang(92010, new object[0]);
			this.UIGameObj.Get_GameObj(7).GetComponent<Text>().text = Language.GetLang(92012, new object[0]);
			this.UIGameObj.Get_GameObj(9).GetComponent<Text>().text = Language.GetLang(92014, new object[0]);
			this.UIGameObj.Get_GameObj(10).GetComponent<Text>().text = Language.GetLang(92016, new object[0]);
			this.UIGameObj.Get_GameObj(11).GetComponent<Text>().text = Language.GetLang(92018, new object[0]);
			this.UIGameObj.Get_GameObj(12).GetComponent<Text>().text = Language.GetLang(92020, new object[0]);
			this.UIGameObj.Get_GameObj(15).GetComponent<Text>().text = Language.GetLang(92014, new object[0]);
			this.UIGameObj.Get_GameObj(16).GetComponent<Text>().text = Language.GetLang(92022, new object[0]);
			this.UIGameObj.Get_GameObj(17).GetComponent<Text>().text = Language.GetLang(92024, new object[0]);
			this.UIGameObj.Get_GameObj(18).GetComponent<Text>().text = Language.GetLang(92026, new object[0]);
			this.UIGameObj.Get_GameObj(22).GetComponent<Text>().text = Language.GetLang(92028, new object[0]);
			this.UIGameObj.Get_GameObj(24).GetComponent<Text>().text = Language.GetLang(92030, new object[0]);
		}
		else
		{
			this.UIGameObj.Get_GameObj(0).GetComponent<Text>().text = Language.GetLang(92001, new object[0]);
			this.UIGameObj.Get_GameObj(1).GetComponent<Text>().text = Language.GetLang(92003, new object[0]);
			this.UIGameObj.Get_GameObj(2).GetComponent<Text>().text = Language.GetLang(92005, new object[0]);
			this.UIGameObj.Get_GameObj(3).GetComponent<Text>().text = Language.GetLang(92007, new object[0]);
			this.UIGameObj.Get_GameObj(4).GetComponent<Text>().text = Language.GetLang(92033, new object[0]);
			this.UIGameObj.Get_GameObj(5).GetComponent<Text>().text = Language.GetLang(92009, new object[0]);
			this.UIGameObj.Get_GameObj(7).GetComponent<Text>().text = Language.GetLang(92011, new object[0]);
			this.UIGameObj.Get_GameObj(9).GetComponent<Text>().text = Language.GetLang(92013, new object[0]);
			this.UIGameObj.Get_GameObj(10).GetComponent<Text>().text = Language.GetLang(92015, new object[0]);
			this.UIGameObj.Get_GameObj(11).GetComponent<Text>().text = Language.GetLang(92017, new object[0]);
			this.UIGameObj.Get_GameObj(12).GetComponent<Text>().text = Language.GetLang(92019, new object[0]);
			this.UIGameObj.Get_GameObj(15).GetComponent<Text>().text = Language.GetLang(92013, new object[0]);
			this.UIGameObj.Get_GameObj(16).GetComponent<Text>().text = Language.GetLang(92021, new object[0]);
			this.UIGameObj.Get_GameObj(17).GetComponent<Text>().text = Language.GetLang(92023, new object[0]);
			this.UIGameObj.Get_GameObj(18).GetComponent<Text>().text = Language.GetLang(92025, new object[0]);
			this.UIGameObj.Get_GameObj(22).GetComponent<Text>().text = Language.GetLang(92027, new object[0]);
			this.UIGameObj.Get_GameObj(24).GetComponent<Text>().text = Language.GetLang(92029, new object[0]);
		}
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x000CBCD8 File Offset: 0x000C9ED8
	private void ShowUI_ImgData()
	{
		Button component = this.UIGameObj.Get_GameObj(13).GetComponent<Button>();
		Button component2 = this.UIGameObj.Get_GameObj(14).GetComponent<Button>();
		bool flag = this.QQType == "hz";
		if (flag)
		{
			this.UIGameObj.Get_GameObj(25).GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "hz_beijingtu");
			SpriteState spriteState = new SpriteState
			{
				highlightedSprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "huangzuan_an21")
			};
			component.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "huangzuan_an2");
			component.spriteState = spriteState;
			component.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(152f, 46f);
			SpriteState spriteState2 = new SpriteState
			{
				highlightedSprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "huangzuan_an11")
			};
			component2.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "huangzuan_an1");
			component2.spriteState = spriteState2;
			component2.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(186f, 46f);
		}
		else
		{
			this.UIGameObj.Get_GameObj(25).GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "beijingtu");
			SpriteState spriteState3 = new SpriteState
			{
				highlightedSprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "kaitong_up")
			};
			component.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "kaitong_normal");
			component.spriteState = spriteState3;
			component.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(155f, 33f);
			SpriteState spriteState4 = new SpriteState
			{
				highlightedSprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "kaitongnian_up")
			};
			component2.targetGraphic.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_PREROGATIVE, "kaitongnian_normal");
			component2.spriteState = spriteState4;
			component2.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(195f, 33f);
		}
	}

	// Token: 0x040010DB RID: 4315
	public static HuangZuanUICon inst;

	// Token: 0x040010DC RID: 4316
	private string QQType = string.Empty;
}
