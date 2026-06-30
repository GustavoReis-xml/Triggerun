using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B3 RID: 1203
public class ZhanDianCon : Window
{
	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x06002E2A RID: 11818 RVA: 0x00198A8C File Offset: 0x00196C8C
	// (set) Token: 0x06002E2B RID: 11819 RVA: 0x00198AA4 File Offset: 0x00196CA4
	public Chariot_Entity Chariot
	{
		get
		{
			return this._Chariot;
		}
		set
		{
			this._Chariot = value;
		}
	}

	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x06002E2C RID: 11820 RVA: 0x00198AB0 File Offset: 0x00196CB0
	// (set) Token: 0x06002E2D RID: 11821 RVA: 0x00198AE2 File Offset: 0x00196CE2
	public Dictionary<int, string> Special_Door_State_Data
	{
		get
		{
			bool flag = this._Special_Door_State_Data == null;
			if (flag)
			{
				this._Special_Door_State_Data = new Dictionary<int, string>();
			}
			return this._Special_Door_State_Data;
		}
		set
		{
			this._Special_Door_State_Data = value;
		}
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x00198AEC File Offset: 0x00196CEC
	protected override void Awake()
	{
		base.Awake();
		ZhanDianCon.inst = this;
		this.Scrollimg = this.UIGameObj.Get_GameObj(1).GetComponent<Image>();
		this.Scrollbar = this.UIGameObj.Get_GameObj(0);
		this.Scrollbar.SetActive(false);
		this.Text_pnum = this.UIGameObj.Get_GameObj(6).GetComponent<Text>();
		this.Text_time = this.UIGameObj.Get_GameObj(7).GetComponent<Text>();
		this.Text_info = this.UIGameObj.Get_GameObj(8).GetComponent<Text>();
		this.JiaShiSlider = this.UIGameObj.Get_GameObj(9);
		this.JiaShiSlider.SetActive(false);
		this.ZhanDian_IngSlider = this.UIGameObj.Get_GameObj(11).GetComponent<Slider>();
		this.ZhanDian_State = this.UIGameObj.Get_GameObj(14);
		this.leftImage = this.ZhanDian_State.transform.FindChild("leftImage").GetComponent<Image>();
		this.RightImage = this.ZhanDian_State.transform.FindChild("RightImage").GetComponent<Image>();
		this.ZhanDian_State.SetActive(false);
		this.ZhanDian_Ing = this.UIGameObj.Get_GameObj(15);
		this.StateText = this.ZhanDian_Ing.transform.FindChild("Slider/Text").GetComponent<Text>();
		this.ZhanDian_Ing.SetActive(false);
		this.pnumobj = this.UIGameObj.Get_GameObj(16);
		this.pnumobj.SetActive(false);
		this.Text_vinner = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.Text_vinner.text = string.Empty;
		this.AllindoObj = this.UIGameObj.Get_GameObj(18);
		this.zhandianPos1 = this.UIGameObj.Get_GameObj(19);
		this.zhandianPos2 = this.UIGameObj.Get_GameObj(20);
		this.ImageA_bk = this.UIGameObj.Get_GameObj(2).GetComponent<Image>();
		this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
		this.Image_A = this.UIGameObj.Get_GameObj(3).GetComponent<Image>();
		this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
		this.ImageB_bk = this.UIGameObj.Get_GameObj(4).GetComponent<Image>();
		this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
		this.Image_B = this.UIGameObj.Get_GameObj(5).GetComponent<Image>();
		this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
		this.Image_A.SetNativeSize();
		this.Image_B.SetNativeSize();
		this.GuangXiao = this.UIGameObj.Get_GameObj(21);
		this.GuangXiao.SetActive(false);
		bool flag = WebParam.client_language == "baxi";
		if (flag)
		{
			this.ShengliGuangxiao = this.UIGameObj.Get_GameObj(82);
			this.ShibaiGuangxiao = this.UIGameObj.Get_GameObj(83);
		}
		else
		{
			this.ShengliGuangxiao = this.UIGameObj.Get_GameObj(22);
			this.ShibaiGuangxiao = this.UIGameObj.Get_GameObj(23);
		}
		this.ShengliGuangxiao.SetActive(false);
		this.ShibaiGuangxiao.SetActive(false);
		this.GameOverImg = this.UIGameObj.Get_GameObj(24).GetComponent<RawImage>();
		this.GameOverImg.enabled = false;
		this.ZhanDianObj = this.UIGameObj.Get_GameObj(25);
		this.CartObj = this.UIGameObj.Get_GameObj(26);
		this.CartTime = this.UIGameObj.Get_GameObj(27).GetComponent<Text>();
		this.CartInfo = this.UIGameObj.Get_GameObj(28).GetComponent<Text>();
		this.SilObj = this.UIGameObj.Get_GameObj(29);
		this.CartSlider = this.SilObj.GetComponent<Slider>();
		this.wenzibeijin = this.UIGameObj.Get_GameObj(37);
		this.DistanceText = this.wenzibeijin.transform.FindChild("distance").GetComponent<Text>();
		this.CartGai1 = this.UIGameObj.Get_GameObj(72).GetComponent<Image>();
		this.CartGai2 = this.UIGameObj.Get_GameObj(73).GetComponent<Image>();
		this.CartGai3 = this.UIGameObj.Get_GameObj(74).GetComponent<Image>();
		this.CartGai4 = this.UIGameObj.Get_GameObj(75).GetComponent<Image>();
		this.szCartGai.Add(this.CartGai1);
		this.szCartGai.Add(this.CartGai2);
		this.szCartGai.Add(this.CartGai3);
		this.szCartGai.Add(this.CartGai4);
		this.CarJc = this.UIGameObj.Get_GameObj(76);
		this.CarJc.SetActive(false);
		this.CatZd = this.UIGameObj.Get_GameObj(77);
		this.CatZd.SetActive(false);
		this.CartPlayerNum = this.UIGameObj.Get_GameObj(34).GetComponent<Text>();
		this.CartPlayerNum.text = "0";
		this.CartBk = this.UIGameObj.Get_GameObj(35);
		this.CartZhengDuo = this.UIGameObj.Get_GameObj(36);
		this.carleftImage = this.CartZhengDuo.transform.FindChild("CarleftImage").GetComponent<Image>();
		this.carRightImage = this.CartZhengDuo.transform.FindChild("CarRightImage").GetComponent<Image>();
		this.CartZhengDuo.SetActive(false);
		this.CartObj.SetActive(false);
		this.RobObj = this.UIGameObj.Get_GameObj(38);
		this.changeimg = this.UIGameObj.Get_GameObj(39).GetComponent<Image>();
		this.RobSlider = this.UIGameObj.Get_GameObj(40).GetComponent<Image>();
		this.LeftSlider = this.UIGameObj.Get_GameObj(41).GetComponent<Slider>();
		this.RightSlider = this.UIGameObj.Get_GameObj(42).GetComponent<Slider>();
		this.robleft1 = this.UIGameObj.Get_GameObj(43).GetComponent<Image>();
		this.robright1 = this.UIGameObj.Get_GameObj(44).GetComponent<Image>();
		this.robright2 = this.UIGameObj.Get_GameObj(45).GetComponent<Image>();
		this.robleft2 = this.UIGameObj.Get_GameObj(46).GetComponent<Image>();
		this.robleftobj = this.UIGameObj.Get_GameObj(78);
		this.robrightobj = this.UIGameObj.Get_GameObj(79);
		this.robleft3 = this.UIGameObj.Get_GameObj(80).GetComponent<Image>();
		this.robright3 = this.UIGameObj.Get_GameObj(81).GetComponent<Image>();
		this.leftnum = this.UIGameObj.Get_GameObj(47).GetComponent<Text>();
		this.rightnum = this.UIGameObj.Get_GameObj(48).GetComponent<Text>();
		this.huihejieshu = this.UIGameObj.Get_GameObj(49);
		this.huihetext = this.UIGameObj.Get_GameObj(50).GetComponent<Text>();
		this.robjindul = this.UIGameObj.Get_GameObj(51).GetComponent<Text>();
		this.robjinbu2 = this.UIGameObj.Get_GameObj(52).GetComponent<Text>();
		this.robtime = this.UIGameObj.Get_GameObj(54).GetComponent<Text>();
		this.robinfo = this.UIGameObj.Get_GameObj(53).GetComponent<Text>();
		this.robzhenduo = this.UIGameObj.Get_GameObj(55).GetComponent<Text>();
		this.robsuo = this.UIGameObj.Get_GameObj(56);
		this.robsuotext = this.robsuo.GetComponent<Text>();
		this.Suo = this.UIGameObj.Get_GameObj(58).GetComponent<Image>();
		this.ZhanDian_Ing_QD = this.UIGameObj.Get_GameObj(59);
		this.ZhanDian_IngSlider_QD = this.UIGameObj.Get_GameObj(59).transform.FindChild("Slider").GetComponent<Slider>();
		this.ZhandianAddSlider = this.UIGameObj.Get_GameObj(60).GetComponent<Slider>();
		GameObject gameObject = this.ZhandianAddSlider.transform.Find("jiashi").gameObject;
		bool flag2 = WebParam.client_language == "baxi";
		if (flag2)
		{
			gameObject.SetActive(false);
		}
		gameObject = this.ZhandianAddSlider.transform.Find("jiashi_bx").gameObject;
		bool flag3 = WebParam.client_language == "zh";
		if (flag3)
		{
			gameObject.SetActive(false);
		}
		this.textbaifen1 = this.UIGameObj.Get_GameObj(61).GetComponent<Text>();
		this.textbaifen2 = this.UIGameObj.Get_GameObj(62).GetComponent<Text>();
		this.imgscr1 = this.UIGameObj.Get_GameObj(63).GetComponent<Image>();
		this.imgscr2 = this.UIGameObj.Get_GameObj(64).GetComponent<Image>();
		this.downjiantou = this.UIGameObj.Get_GameObj(65).GetComponent<Image>();
		bool flag4 = WebParam.client_language == "baxi";
		if (flag4)
		{
			this.DaoJiShiDonghua = this.UIGameObj.Get_GameObj(84);
			this.DaoJiShiDonghua.SetActive(false);
			gameObject = this.UIGameObj.Get_GameObj(89);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(88);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(87);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(86);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(85);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
		}
		else
		{
			this.DaoJiShiDonghua = this.UIGameObj.Get_GameObj(66);
			this.DaoJiShiDonghua.SetActive(false);
			gameObject = this.UIGameObj.Get_GameObj(71);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(70);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(69);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(68);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
			gameObject = this.UIGameObj.Get_GameObj(67);
			gameObject.SetActive(false);
			this.DJSDongHua.Add(gameObject);
		}
		this.ZhandianAddSlider.gameObject.SetActive(false);
		this.ZhanDian_Ing_QD.SetActive(false);
		this.huihejieshu.SetActive(false);
		this.RobObj.SetActive(false);
		this.vSize = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x00199644 File Offset: 0x00197844
	public void OnSetIsPlaySound(ArrayList arr)
	{
		this.nIsPlaySound = (int)arr[0];
		this.nIsChangeHero = (int)arr[1];
		this.sStartChangeTime = (string)arr[2];
		this.OnSetChangeTime(this.sStartChangeTime);
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x00199698 File Offset: 0x00197898
	private void OnSetChangeTime(string sTime)
	{
		bool flag = sTime == string.Empty;
		if (flag)
		{
			this.m_dStartChangeTime = DateTime.MinValue;
		}
		else
		{
			this.m_dStartChangeTime = DateTime.Parse(sTime);
		}
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x001996D4 File Offset: 0x001978D4
	public void LoadMapEffect()
	{
		bool flag = fUIManager.MapEffectObj1 == null && fUIManager.MapEffectObj2 == null;
		if (!flag)
		{
			this.EffectObj1 = fUIManager.MapEffectObj1.transform.FindChild("boxjudian(Clone)").gameObject;
			bool flag2 = fUIManager.MapEffectObj2 != null;
			if (flag2)
			{
				this.EffectObj2 = fUIManager.MapEffectObj2.transform.FindChild("boxjudian(Clone)").gameObject;
			}
			else
			{
				this.EffectObj2 = this.EffectObj1;
			}
			this.SetZhanDianPos();
			bool flag3 = this.m_nMapType == 2;
			if (flag3)
			{
				bool flag4 = fUIManager.MapEffectObj1 != null;
				if (flag4)
				{
					fUIManager.MapEffectObj1.transform.FindChild("boxjudian(Clone)").gameObject.SetActive(false);
				}
				bool flag5 = fUIManager.MapEffectObj2 != null;
				if (flag5)
				{
					fUIManager.MapEffectObj2.transform.FindChild("boxjudian(Clone)").gameObject.SetActive(false);
				}
				bool flag6 = fUIManager.MapEffectObj3 != null;
				if (flag6)
				{
					fUIManager.MapEffectObj3.transform.FindChild("boxjudian(Clone)").gameObject.SetActive(false);
				}
				this.Refresh_Map_Effect();
				this.SetFangXiangXian();
			}
		}
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x00199824 File Offset: 0x00197A24
	public void SetZhanDianPos()
	{
		bool flag = !(this.EffectObj1 == null) || !(this.EffectObj2 == null);
		if (flag)
		{
			this.maplantiao = this.EffectObj1.transform.FindChild("lan_bianse_1").gameObject;
			this.maphongtiao = this.EffectObj1.transform.FindChild("hong_bianse_2").gameObject;
			bool flag2 = this.m_nMapType == 3;
			if (flag2)
			{
				this.InitMapEffectColor();
			}
			bool flag3 = this.m_nGameState < 2 && this.m_nMapType != 3 && this.EffectObj2 != null;
			if (flag3)
			{
				this.EffectObj2.SetActive(false);
			}
		}
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x001998E8 File Offset: 0x00197AE8
	public void SetCartPos()
	{
		this.InitShowObj();
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			bool flag2 = this.m_nGameState > 0;
			if (flag2)
			{
				this.CartInfo.text = Language.GetLang(46078, new object[0]);
			}
			else
			{
				this.CartInfo.text = Language.GetLang(46089, new object[0]);
			}
		}
		else
		{
			bool flag3 = this.m_nGameState > 1;
			if (flag3)
			{
				this.CartInfo.text = Language.GetLang(46080, new object[0]);
			}
			else
			{
				this.CartInfo.text = Language.GetLang(46121, new object[0]);
			}
		}
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x001999B0 File Offset: 0x00197BB0
	private void InitShowObj()
	{
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
			if (flag2)
			{
				this.ShowObj = this.zhandianPos1;
				this.zhandianPos1.SetActive(true);
				this.zhandianPos2.SetActive(false);
			}
			else
			{
				this.ShowObj = this.zhandianPos2;
				this.zhandianPos1.SetActive(false);
				this.zhandianPos2.SetActive(true);
			}
			bool flag3 = this.m_nMapType == 3;
			if (flag3)
			{
				this.ShowObj = this.zhandianPos1;
				this.zhandianPos1.SetActive(true);
				this.zhandianPos2.SetActive(false);
			}
			this.rect = this.ShowObj.GetComponent<RectTransform>();
			this.jiantou = this.ShowObj.transform.FindChild("jiantou").gameObject;
			this.ZLScrollbar = this.ShowObj.transform.FindChild("ZLScrollbar").gameObject;
			this.ZLScrollbar_Slider = this.ZLScrollbar.transform.FindChild("Sliding Area/JuDianPosHandle").GetComponent<Image>();
			this.BkColor = this.ShowObj.transform.FindChild("Image").GetComponent<Image>();
			this.InitBkColor();
			this.ZLScrollbar.SetActive(false);
			this.ZLtext = this.ShowObj.transform.FindChild("Text").gameObject;
			this.ZDStateText = this.ShowObj.transform.FindChild("Text").GetComponent<Text>();
			this.mEffcetDianHong = this.ShowObj.transform.FindChild("ui_zhandian_hong").gameObject;
			this.mEffcetDianLan = this.ShowObj.transform.FindChild("ui_zhandian_lv").gameObject;
			this.ShowObj.SetActive(false);
		}
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x00199BA0 File Offset: 0x00197DA0
	private void UpPos()
	{
		int num = Environment.TickCount - this.m_nTickTime;
		this.m_nTickTime = Environment.TickCount;
		bool flag = num > 0 && this.m_nIsAddScr > 0;
		if (flag)
		{
			bool flag2 = this.m_nIsAddScr == 1;
			if (flag2)
			{
				this.RobSlider.fillAmount = (float)(num * this.m_nSpeed) / 1000f / 2100f + this.RobSlider.fillAmount;
				this.ZhanDian_IngSlider_QD.value = (float)(num * this.m_nSpeed) / 1000f / 2100f + this.ZhanDian_IngSlider_QD.value;
				bool flag3 = this.RobSlider.fillAmount == 1f;
				if (flag3)
				{
					this.m_nIsAddScr = 0;
				}
			}
			else
			{
				this.RobSlider.fillAmount -= (float)(num * this.m_nSpeed) / 1000f / 2100f;
				this.ZhanDian_IngSlider_QD.value -= (float)(num * this.m_nSpeed) / 1000f / 2100f;
				bool flag4 = this.RobSlider.fillAmount == 0f;
				if (flag4)
				{
					this.m_nIsAddScr = 0;
				}
			}
		}
		num = Environment.TickCount - this.m_nTickTimeJiaShi;
		this.m_nTickTimeJiaShi = Environment.TickCount;
		bool flag5 = num > 0 && this.m_nIsAddScrJiaShi == 0;
		if (flag5)
		{
			this.ZhandianAddSlider.value -= (float)num * this.m_nSpeedJiaShi / 1000f / 1000f;
			bool flag6 = this.ZhandianAddSlider.value == 0f;
			if (flag6)
			{
				this.ZhandianAddSlider.gameObject.SetActive(false);
				this.m_nIsAddScrJiaShi = 1;
			}
		}
		bool flag7 = this.m_nGameState < 1;
		if (!flag7)
		{
			bool flag8 = this.npc != null && this.EffectObj1 == null;
			if (flag8)
			{
				this.EffectObj1 = this.npc.thisModel;
				this.EffectObj2 = this.npc.thisModel;
			}
			bool flag9 = !(this.EffectObj1 != null) || !(this.EffectObj2 != null);
			if (!flag9)
			{
				this.f = (float)Screen.width / SysConfig.ScreenWidth;
				bool flag10 = this.m_nGameState == 1;
				if (flag10)
				{
					this.position = CameraControl.Instance.controlCamera.WorldToScreenPoint(this.EffectObj1.transform.position);
				}
				else
				{
					this.position = CameraControl.Instance.controlCamera.WorldToScreenPoint(this.EffectObj2.transform.position);
				}
				this.fCx = this.position.x / this.fScreenW;
				this.fCy = this.position.y / this.fScreenH;
				this.fx = this.vSize.x * this.fCx - this.vSize.x / 2f;
				this.fy = this.vSize.y * this.fCy - this.vSize.y / 2f;
				this.szReturn.x = this.fx;
				this.szReturn.y = this.fy;
				this.szReturn.z = 0f;
				bool flag11 = this.position.z < 0f;
				if (flag11)
				{
					this.nChaoXiang = 1;
				}
				else
				{
					this.nChaoXiang = 0;
				}
				this.position = new Vector2((this.position.x - (float)(Screen.width / 2)) * this.f, (this.position.y - (float)(Screen.height / 2)) * this.f);
				this.fn_x = Mathf.Abs(this.position.x);
				bool flag12 = this.fn_x > (float)(Screen.width / 2);
				if (flag12)
				{
					bool flag13 = this.position.x < 0f;
					if (flag13)
					{
						this.position.x = (float)(-(float)Screen.width / 2);
					}
					else
					{
						this.position.x = (float)(Screen.width / 2);
					}
					bool flag14 = !this.jiantou.activeSelf;
					if (flag14)
					{
						this.jiantou.SetActive(true);
					}
					this.ZLtext.transform.localPosition = new Vector3(this.ZLtext.transform.localPosition.x, -72.1f, 0f);
				}
				else
				{
					bool activeSelf = this.jiantou.activeSelf;
					if (activeSelf)
					{
						this.jiantou.SetActive(false);
					}
					this.ZLtext.transform.localPosition = new Vector3(this.ZLtext.transform.localPosition.x, -50f, 0f);
				}
				bool flag15 = this.nChaoXiang == 1;
				if (flag15)
				{
					bool flag16 = !this.jiantou.activeSelf;
					if (flag16)
					{
						this.jiantou.SetActive(true);
					}
					this.ZLtext.transform.localPosition = new Vector3(this.ZLtext.transform.localPosition.x, -72.1f, 0f);
					this.position = new Vector2(this.position.x, 0f);
					bool flag17 = this.position.x > 0f;
					if (flag17)
					{
						this.position.x = (float)(-(float)Screen.width / 2);
					}
					else
					{
						this.position.x = (float)(Screen.width / 2);
					}
				}
				this.fn_y = Mathf.Abs(this.position.y);
				bool flag18 = this.fn_y >= (float)(Screen.height / 2);
				if (flag18)
				{
					bool flag19 = this.position.y < 0f;
					if (flag19)
					{
						this.position.y = (float)(-(float)Screen.height / 2);
					}
					else
					{
						this.position.y = (float)(Screen.height / 2);
					}
					this.rect.localPosition = new Vector3(this.position.x, this.position.y, 0f);
				}
				else
				{
					this.rect.localPosition = new Vector3(this.position.x, this.szReturn.y, 0f);
				}
				this.q1 = Quaternion.Euler(0f, 0f, 0f);
				bool flag20 = this.rect.localPosition.x > 0f;
				if (flag20)
				{
					bool flag21 = this.rect.localPosition.x <= 200f;
					if (flag21)
					{
						this.q1 = Quaternion.Euler(0f, 0f, 55f);
					}
					else
					{
						this.q1 = Quaternion.Euler(0f, 0f, 90f);
					}
				}
				bool flag22 = this.position.x < 0f;
				if (flag22)
				{
					bool flag23 = this.rect.localPosition.x >= -200f;
					if (flag23)
					{
						this.q1 = Quaternion.Euler(0f, 0f, 55f);
					}
					else
					{
						this.q1 = Quaternion.Euler(0f, 0f, 0f);
					}
				}
				bool flag24 = this.position.x == 0f;
				if (flag24)
				{
					this.q1 = Quaternion.Euler(0f, 0f, 55f);
				}
				this.jiantou.transform.localRotation = this.q1;
			}
		}
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x0019A3A0 File Offset: 0x001985A0
	private void BkColorChange()
	{
		bool flag = this.nBk == 0;
		if (flag)
		{
			this.BkColor.color = Color.white;
			this.nBk = 1;
		}
		else
		{
			this.InitBkColor();
			this.nBk = 0;
		}
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x0019A3E7 File Offset: 0x001985E7
	private void Start()
	{
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x0019A3EC File Offset: 0x001985EC
	private void FixedUpdate()
	{
		this.UpPos();
		this.OnCheckNewPlayerMsg();
		bool flag = this.m_nMapType != 3 && this.BkState;
		if (flag)
		{
			this.BkColorChange();
		}
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x0019A428 File Offset: 0x00198628
	public void InitData(ArrayList arr)
	{
		FbXmlData fbXmlData = null;
		bool flag = fGameManager.mloadConfig.FbData.ContainsKey(fGameManager.GetInstance().nNowScnID);
		if (flag)
		{
			fbXmlData = fGameManager.mloadConfig.FbData[fGameManager.GetInstance().nNowScnID];
		}
		bool flag2 = fbXmlData == null;
		if (!flag2)
		{
			this.m_nMapType = fbXmlData.nType;
			this.SetUIType(fbXmlData.nType);
			this.GameOverImg.enabled = false;
			base.CancelInvoke("TimeCall");
			ArrayList arrayList = (ArrayList)arr[0];
			this.m_nGameState = (int)arrayList[0];
			bool flag3 = this.m_nMapType == 1 || this.m_nMapType == 3;
			if (flag3)
			{
				this.InitShowObj();
			}
			bool flag4 = this.m_nGameState <= 0;
			if (flag4)
			{
				bool flag5 = this.m_nMapType == 3;
				if (flag5)
				{
					this.Text_info.text = Fun.GetHintS(1);
					this.SetTipText(Fun.GetHintS(1));
				}
				else
				{
					bool flag6 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
					if (flag6)
					{
						this.Text_info.text = Fun.GetHintS(1);
					}
					else
					{
						this.SetTipText(Fun.GetHintS(0));
						this.Text_info.text = Fun.GetHintS(0);
					}
				}
				bool flag7 = this.m_nMapType == 2;
				if (flag7)
				{
					bool flag8 = this.fangxiangxian == null;
					if (flag8)
					{
						this.fangxiangxian = GameObject.Find("fangxiangxian");
					}
					bool flag9 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
					if (flag9)
					{
						bool flag10 = this.fangxiangxian != null;
						if (flag10)
						{
							this.fangxiangxian.SetActive(false);
						}
					}
					else
					{
						bool flag11 = this.fangxiangxian != null;
						if (flag11)
						{
							this.fangxiangxian.SetActive(true);
						}
					}
				}
				this.Text_time.text = Fun.sencondsToHMS_CXP((int)arrayList[1]);
			}
			else
			{
				this.Text_time.text = Fun.sencondsToHMS_CXP((int)arrayList[2]);
			}
			switch (this.m_nGameState)
			{
			case 0:
				this.nTime = (int)arrayList[1];
				break;
			case 1:
			case 2:
				this.nTime = (int)arrayList[2];
				break;
			case 3:
				this.nTime = (int)arrayList[4];
				break;
			}
			this.ZhanDian_State.SetActive(false);
			this.InitTween();
			bool flag12 = fbXmlData.nType != 3 && this.m_nGameState > 0;
			if (flag12)
			{
				this.m_nGameOver = (int)arrayList[14];
			}
			bool flag13 = fbXmlData.nType == 1;
			if (flag13)
			{
				this.LoadMapEffect();
				this.SetDianIcon();
				this.nIsPlaySound = (int)arrayList[10];
				this.nIsChangeHero = (int)arrayList[11];
			}
			bool flag14 = fbXmlData.nType == 2;
			if (flag14)
			{
				bool flag15 = gameuiCon.inst != null;
				if (flag15)
				{
					gameuiCon.inst.CloseRoundEndUI();
				}
				fGameManager.Heroplayer.canControl = true;
				this.wenzibeijin.SetActive(false);
				this.DistanceText.text = string.Empty;
				this.nCarid = (int)arrayList[8];
				this.CartGai1.enabled = false;
				this.CartGai2.enabled = false;
				this.CartGai3.enabled = false;
				this.CartGai4.enabled = false;
				int num = fbXmlData.uipos.Length;
				for (int i = 1; i < num; i++)
				{
					Image image = this.szCartGai[i - 1] as Image;
					float num2 = 358f * (float.Parse(fbXmlData.uipos[i]) / float.Parse(fbXmlData.uipos[0]));
					image.transform.localPosition = new Vector3(-177f + num2, image.transform.localPosition.y, image.transform.localPosition.z);
					image.enabled = true;
				}
				this.SetCartPos();
				this.SetCartUi(arrayList);
				this.nIsPlaySound = (int)arrayList[10];
				this.nIsChangeHero = (int)arrayList[11];
				this.sStartChangeTime = (string)arrayList[12];
				this.Refresh_Map_Effect();
				this.Special_Door_State_Data.Clear();
			}
			bool flag16 = fbXmlData.nType == 3;
			if (flag16)
			{
				this.LoadMapEffect();
				this.SetRobIcon(arrayList);
				this.nIsPlaySound = (int)arrayList[18];
				this.nIsChangeHero = (int)arrayList[19];
				this.sStartChangeTime = (string)arrayList[20];
				bool flag17 = (int)arrayList[23] == 1;
				if (flag17)
				{
					Door_Control.Get_Instantiate.General_Door_Handle(3, "open_close", true);
				}
			}
			this.OnSetChangeTime(this.sStartChangeTime);
			bool flag18 = fbXmlData.nType == 2;
			if (flag18)
			{
				Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, this.m_nGameState, false);
			}
			else
			{
				Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
			}
			Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			bool flag19 = (int)arr[2] > 0;
			if (flag19)
			{
				bool flag20 = this.m_nRound > 0;
				if (flag20)
				{
					Door_Control.Get_Instantiate.General_Door_Handle(3, "open_close", true);
				}
			}
			else
			{
				int num3 = Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
				bool flag21 = this.m_nGameState > 0 && num3 > 0;
				if (flag21)
				{
					this.m_nOpen = 1;
				}
			}
		}
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x0019AA5C File Offset: 0x00198C5C
	private void InitMapEffectColor()
	{
		bool activeSelf = this.zhandianPos2.activeSelf;
		if (activeSelf)
		{
			this.zhandianPos2.SetActive(false);
		}
		bool flag = this.EffectObj1 != null;
		if (flag)
		{
			this.maplantiao.SetActive(true);
			this.maphongtiao.SetActive(true);
		}
	}

	// Token: 0x06002E3B RID: 11835 RVA: 0x0019AAB4 File Offset: 0x00198CB4
	private void SetUIType(int ntype)
	{
		this.AllindoObj.SetActive(true);
		this.ZhanDianObj.SetActive(false);
		this.CartObj.SetActive(false);
		this.RobObj.SetActive(false);
		switch (ntype)
		{
		case 1:
			this.ZhanDianObj.SetActive(true);
			break;
		case 2:
			this.CartObj.SetActive(true);
			break;
		case 3:
			this.RobObj.SetActive(true);
			break;
		}
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x0019AB40 File Offset: 0x00198D40
	public void SetallTime(int nallTime)
	{
		this.nTime = nallTime;
		this.TimeCall();
		bool flag = this.m_nOpen == 0 && this.m_nGameState > 0;
		if (flag)
		{
			int num = Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			bool flag2 = num > 0;
			if (flag2)
			{
				this.m_nOpen = 1;
			}
		}
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x0019ABA0 File Offset: 0x00198DA0
	private void TimeCall()
	{
		bool flag = this.m_nGameState == 0;
		if (flag)
		{
			this.FirstSoundCon();
		}
		bool flag2 = this.m_nGameState == 1 || this.m_nGameState == 2;
		if (flag2)
		{
			this.SecSoundCon();
		}
		bool flag3 = this.m_nMapType == 2 && this.m_nGameState > 0 && this.nTime == 10;
		if (flag3)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[882];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		}
		this.Text_time.text = Fun.sencondsToHMS_CXP(this.nTime);
		this.CartTime.text = Fun.sencondsToHMS_CXP(this.nTime);
		HeroPlayer player = fGameManager.GetInstance().getPlayer();
		bool flag4 = player != null && gameuiCon.inst != null && player.isDead && player.GameAttackPlayer != null;
		if (flag4)
		{
			gameuiCon.inst.mainDead.OnShowBsTime(Fun.sencondsToHMS_CXP(this.nTime));
		}
		bool flag5 = this.nTime == 0;
		if (flag5)
		{
			base.CancelInvoke("TimeCall");
		}
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x0019ACD4 File Offset: 0x00198ED4
	public void ReadyTimeCall(int ntime)
	{
		this.nTime = ntime;
		this.FirstSoundCon();
		bool flag = this.nTime == 0;
		if (flag)
		{
			this.robtime.text = string.Empty;
			this.robinfo.text = string.Empty;
			base.Invoke("yincantext", 1f);
		}
		else
		{
			this.robtime.text = Fun.sencondsToHMS_CXP(this.nTime);
			bool flag2 = this.RobSlider.fillAmount != 0f;
			if (flag2)
			{
				this.RobSlider.fillAmount = 0f;
				this.ZhanDian_IngSlider_QD.value = 0f;
				this.InitScrollRob(0, 0, 0);
				this.InitPlayerNum(0, 0, 0, 0);
			}
		}
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x0019AD9C File Offset: 0x00198F9C
	private void FirstSoundCon()
	{
		bool flag = this.nTime <= 5;
		if (flag)
		{
			this.DaoJiShiDonghua.SetActive(true);
			bool flag2 = this.nTime > 0;
			if (flag2)
			{
				GameObject gameObject = (GameObject)this.DJSDongHua[this.nTime - 1];
				gameObject.SetActive(true);
			}
		}
		bool flag3 = this.nTime == 5 && this.m_nGameState <= 0;
		if (flag3)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[878];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		}
		bool flag4 = this.nTime == 10;
		if (flag4)
		{
			PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[879];
			fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 10);
		}
		bool flag5 = this.nTime == 30;
		if (flag5)
		{
			bool flag6 = this.m_nMapType == 3;
			if (flag6)
			{
				PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[874];
				fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
				this.SetTipText(Language.GetLang(46090, new object[0]));
			}
			else
			{
				bool flag7 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
				if (flag7)
				{
					PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[874];
					fUISoundManager.instance.PlayUISound(preloadData4.sUrl, 0, null, 20);
					this.SetTipText(Language.GetLang(46090, new object[0]));
				}
				else
				{
					PreloadData preloadData5 = fGameManager.mloadConfig.AllProLodaList[865];
					fUISoundManager.instance.PlayUISound(preloadData5.sUrl, 0, null, 20);
					this.SetTipText(Language.GetLang(46091, new object[0]));
				}
			}
		}
		this.Text_time.text = Fun.sencondsToHMS_CXP(this.nTime);
		this.CartTime.text = Fun.sencondsToHMS_CXP(this.nTime);
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x0019AFC4 File Offset: 0x001991C4
	private void SecSoundCon()
	{
		bool flag = this.nTime == 30;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound("bisai_gaochaoyinyue", 0, null, 20);
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[883];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		}
		bool flag2 = this.nTime == 60;
		if (flag2)
		{
			PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[884];
			fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
		}
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x0019B05C File Offset: 0x0019925C
	private void SetTipText(string sMsg)
	{
		this.GuangXiao.SetActive(true);
		this.Text_vinner.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		this.Text_vinner.text = sMsg;
		this.Text_vinner.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 2f);
		base.Invoke("yincantext", 3f);
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x0019B0E4 File Offset: 0x001992E4
	private void SetTip_NoDo(string sMsg, string color)
	{
		bool flag = fGameManager.Heroplayer.playerCommon.m_nShili == 2;
		if (flag)
		{
			this.Text_vinner.text = sMsg;
			this.Text_vinner.color = Fun.StrToColor(color, 1f);
		}
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x0019B12E File Offset: 0x0019932E
	private void yincantext()
	{
		this.GuangXiao.SetActive(false);
		this.Text_vinner.text = string.Empty;
		this.Text_vinner.color = Color.white;
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x0019B160 File Offset: 0x00199360
	private void DisVinnerTest()
	{
		this.Text_vinner.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		this.Text_vinner.transform.DOScale(new Vector3(0f, 0f, 0f), 5f);
		base.CancelInvoke("DisVinnerTest");
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x0019B1CC File Offset: 0x001993CC
	public void GameStartInfo(ArrayList arr)
	{
		base.CancelInvoke("TimeCall");
		this.m_nGameState = (int)arr[0];
		this.m_nGameOver = 0;
		this.nTime = (int)arr[2];
		this.ShowObj.SetActive(true);
		this.ZLScrollbar.SetActive(false);
		bool flag = this.m_nMapType == 3;
		if (flag)
		{
			this.RobGameStart(arr);
		}
		else
		{
			bool flag2 = this.m_nMapType == 2;
			if (flag2)
			{
				bool flag3 = this.Chariot != null;
				if (flag3)
				{
					this.EffectObj1 = this.Chariot.thisModel;
					this.EffectObj2 = this.Chariot.thisModel;
				}
				this.SetCartUi(arr);
				bool flag4 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
				if (flag4)
				{
					this.Text_vinner.color = Color.red;
					this.SetTipText(Language.GetLang(46078, new object[0]));
					this.CartInfo.text = Language.GetLang(46078, new object[0]);
					Text component = this.ShowObj.transform.FindChild("Text").GetComponent<Text>();
					component.text = Language.GetLang(46079, new object[0]);
					PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[867];
					fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
					bool flag5 = this.fangxiangxian != null;
					if (flag5)
					{
						this.fangxiangxian.SetActive(false);
					}
				}
				else
				{
					bool flag6 = this.fangxiangxian != null;
					if (flag6)
					{
						this.fangxiangxian.SetActive(true);
					}
					this.Text_vinner.color = Color.blue;
					this.SetTipText(Language.GetLang(46080, new object[0]));
					this.CartInfo.text = Language.GetLang(46080, new object[0]);
					PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[890];
					fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
				}
				Image component2 = this.ShowObj.transform.FindChild("didianfuhao").GetComponent<Image>();
				component2.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "p1");
				Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, this.m_nGameState, false);
			}
			else
			{
				this.SetDianIcon();
				this.Text_time.text = Fun.sencondsToHMS_CXP((int)arr[2]);
				Text component3 = this.ShowObj.transform.FindChild("Text").GetComponent<Text>();
				bool flag7 = this.m_nGameState == 1;
				if (flag7)
				{
					Image component4 = this.ShowObj.transform.FindChild("didianfuhao").GetComponent<Image>();
					component4.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "A");
				}
				bool flag8 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
				if (flag8)
				{
					this.Text_vinner.color = Color.red;
					this.SetTipText(string.Format(Fun.GetHintS(2), "A"));
					PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[873];
					fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
					component3.text = Language.GetLang(46081, new object[0]);
				}
				else
				{
					component3.text = Language.GetLang(46077, new object[0]);
					this.Text_vinner.color = Color.blue;
					this.SetTipText(string.Format(Fun.GetHintS(3), "A"));
					PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[864];
					fUISoundManager.instance.PlayUISound(preloadData4.sUrl, 0, null, 20);
				}
			}
			Door_Control.Get_Instantiate.General_Door_Handle(1, "close_open", true);
			this.m_nOpen = 1;
			this.Refresh_Map_Effect();
		}
	}

	// Token: 0x06002E46 RID: 11846 RVA: 0x0019B600 File Offset: 0x00199800
	public void SetFangXiangXian()
	{
		bool flag = this.fangxiangxian == null;
		if (flag)
		{
			this.fangxiangxian = GameObject.Find("fangxiangxian");
		}
		bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag2)
		{
			bool flag3 = this.fangxiangxian != null;
			if (flag3)
			{
				this.fangxiangxian.SetActive(false);
			}
		}
		else
		{
			bool flag4 = this.fangxiangxian != null;
			if (flag4)
			{
				this.fangxiangxian.SetActive(true);
			}
		}
	}

	// Token: 0x06002E47 RID: 11847 RVA: 0x0019B688 File Offset: 0x00199888
	public void SetCartUi(ArrayList szinfo)
	{
		bool flag = this.m_nGameState == 0;
		if (flag)
		{
			this.CartTime.text = Fun.sencondsToHMS_CXP((int)szinfo[1]);
			this.SilObj.SetActive(false);
			this.CartBk.SetActive(true);
		}
		else
		{
			bool flag2 = this.m_nGameState > 1;
			if (flag2)
			{
				this.CarJc.SetActive(true);
				fUISoundManager.instance.PlayUISound("match_car_checkpoint", 0, null, 20);
			}
			this.CartTime.text = Fun.sencondsToHMS_CXP((int)szinfo[2]);
			this.SilObj.SetActive(true);
			this.CartBk.SetActive(false);
			this.SetNewPlayerMsgStat(-1);
		}
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x0019B74C File Offset: 0x0019994C
	private void SetDianIcon()
	{
		bool flag = this.m_nGameState == 0;
		if (flag)
		{
			this.AllindoObj.SetActive(true);
			this.ZhanDian_Ing.SetActive(false);
			this.pnumobj.SetActive(false);
			bool flag2 = this.ShowObj != null;
			if (flag2)
			{
				this.ShowObj.SetActive(false);
			}
			this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
			this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
			this.Text_vinner.color = Color.white;
			this.Text_vinner.text = string.Empty;
			this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
			this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
			this.Scrollbar.SetActive(false);
			this.Text_pnum.text = string.Empty;
		}
		bool flag3 = this.m_nGameState == 1;
		if (flag3)
		{
			bool flag4 = this.ShowObj != null;
			if (flag4)
			{
				this.ShowObj.SetActive(true);
			}
			bool flag5 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
			if (flag5)
			{
				this.Text_info.text = string.Format(Fun.GetHintS(2), "A");
				this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "dl0");
				this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "A");
				this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
				this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
			}
			else
			{
				this.Text_info.text = string.Format(Fun.GetHintS(3), "A");
				this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "dl");
				this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "A");
				this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
				this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SD");
			}
		}
		bool flag6 = this.m_nGameState == 2;
		if (flag6)
		{
			bool flag7 = this.ShowObj != null;
			if (flag7)
			{
				this.ShowObj.SetActive(true);
			}
			bool flag8 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
			if (flag8)
			{
				this.Text_info.text = string.Format(Fun.GetHintS(2), "B");
				this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
				this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "D");
				this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "dl0");
				this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "B");
			}
			else
			{
				this.Text_info.text = string.Format(Fun.GetHintS(3), "B");
				this.ImageA_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "SDK");
				this.Image_A.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "X");
				this.ImageB_bk.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "dl");
				this.Image_B.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "B");
			}
		}
		this.Image_A.SetNativeSize();
		this.Image_B.SetNativeSize();
	}

	// Token: 0x06002E49 RID: 11849 RVA: 0x0019BB44 File Offset: 0x00199D44
	private void RobGameStart(ArrayList arr)
	{
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, false);
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			this.Text_vinner.color = Color.red;
			this.SetTipText(Language.GetLang(46076, new object[0]));
			Text component = this.ShowObj.transform.FindChild("Text").GetComponent<Text>();
			component.text = Language.GetLang(46077, new object[0]);
		}
		else
		{
			this.Text_vinner.color = Color.blue;
			this.SetTipText(Language.GetLang(46076, new object[0]));
		}
		this.PlayRobGameSoud();
		bool flag2 = (int)arr[13] > 0;
		if (flag2)
		{
			this.robtime.text = string.Empty;
			this.robinfo.text = string.Empty;
			this.robsuo.SetActive(true);
			this.m_nLockTime = (int)arr[13];
			this.robsuotext.text = Language.GetLang(46127, new object[0]) + this.m_nLockTime.ToString();
			this.LockTime(this.m_nLockTime);
		}
		bool flag3 = this.m_nOpen == 0;
		if (flag3)
		{
			Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			bool flag4 = this.m_nGameState > 0;
			if (flag4)
			{
				this.m_nOpen = 1;
			}
		}
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x0019BCE4 File Offset: 0x00199EE4
	private void PlayRobGameSoud()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[873];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		Door_Control.Get_Instantiate.General_Door_Handle(1, "close_open", true);
		this.m_nOpen = 1;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x0019BD38 File Offset: 0x00199F38
	private void SetRobIcon(ArrayList szinfo)
	{
		bool flag = this.m_nGameState <= 0;
		if (flag)
		{
			this.robtime.text = Fun.sencondsToHMS_CXP((int)szinfo[1]);
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
			if (flag2)
			{
				this.robinfo.text = Language.GetLang(46089, new object[0]);
			}
			else
			{
				this.robinfo.text = Language.GetLang(46121, new object[0]);
			}
			this.downjiantou.enabled = true;
			this.robleft1.enabled = false;
			this.robleft2.enabled = false;
			this.robright1.enabled = false;
			this.robright2.enabled = false;
			this.robleft3.enabled = false;
			this.robright3.enabled = false;
			this.leftnum.text = "0";
			this.rightnum.text = "0";
			this.huihejieshu.SetActive(false);
			this.robjindul.text = "0";
			this.robjinbu2.text = "0";
			this.robjindul.color = Fun.StrToColor("ffffff", 1f);
			this.robjinbu2.color = Fun.StrToColor("ffffff", 1f);
			this.textbaifen1.color = Fun.StrToColor("ffffff", 1f);
			this.imgscr1.color = Fun.StrToColor("ffffff", 1f);
			this.textbaifen2.color = Fun.StrToColor("ffffff", 1f);
			this.imgscr2.color = Fun.StrToColor("ffffff", 1f);
			this.robzhenduo.enabled = false;
			this.Suo.enabled = true;
			this.robsuo.SetActive(false);
			this.RobSlider.fillAmount = 0f;
			this.ZhanDian_IngSlider_QD.value = 0f;
			this.LeftSlider.value = 0.01f;
			this.RightSlider.value = 0.01f;
			this.SetShowZhanDianPos(false);
		}
		else
		{
			this.SetShowZhanDianPos(true);
			this.robtime.text = string.Empty;
			this.robinfo.text = string.Empty;
			this.huihejieshu.SetActive(false);
			bool flag3 = (int)szinfo[13] > 0;
			if (flag3)
			{
				this.m_nLockTime = (int)szinfo[13];
				this.robsuotext.text = Language.GetLang(46127, new object[0]) + this.m_nLockTime.ToString();
				this.LockTime(this.m_nLockTime);
			}
			else
			{
				this.Suo.enabled = false;
				this.robsuo.SetActive(false);
			}
			this.InitVinner((int)szinfo[14], (int)szinfo[15]);
			this.InitScrollRob((int)szinfo[11], (int)szinfo[12], (int)szinfo[5]);
			this.InitPlayerNum((int)szinfo[16], (int)szinfo[17], (int)szinfo[6], (int)szinfo[5]);
			this.ShowObj.SetActive(true);
		}
		this.robleftobj.SetActive(false);
		this.robrightobj.SetActive(false);
		this.SetPointUI((int)szinfo[5]);
	}

	// Token: 0x06002E4C RID: 11852 RVA: 0x0019C10C File Offset: 0x0019A30C
	public void RoundEnd_ByCar(ArrayList arr)
	{
		this.szCarResult = arr;
		this.m_nAttVinnerNum = (int)arr[0];
		this.m_nDefVinnerNum = (int)arr[1];
		this.m_nNowWin = (int)arr[2];
		this.m_nRound = (int)arr[5] + 1;
		this.szResult = (ArrayList)arr[3];
		this.m_nGameState = (int)arr[4];
		base.Invoke("OnRoundEnd", 1f);
		fGameManager.Heroplayer.canControl = false;
		this.huihetext.text = Language.GetLang(46088, new object[]
		{
			this.m_nRound
		});
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, 2, true);
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, 1, true);
		Door_Control.Get_Instantiate.Special_Door_Handle(1, "open_close");
		Door_Control.Get_Instantiate.General_Door_Handle(3, "open_close", true);
	}

	// Token: 0x06002E4D RID: 11853 RVA: 0x0019C230 File Offset: 0x0019A430
	public void CarRoundStart()
	{
		this.CartSlider.value = 0f;
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x0019C244 File Offset: 0x0019A444
	public void RoundEnd(ArrayList arr)
	{
		bool flag = this.m_nMapType == 2;
		if (flag)
		{
			this.RoundEnd_ByCar(arr);
		}
		else
		{
			this.m_nAttVinnerNum = (int)arr[0];
			this.m_nDefVinnerNum = (int)arr[1];
			this.m_nNowWin = (int)arr[2];
			this.m_nRound = (int)arr[0] + (int)arr[1];
			this.szResult = (ArrayList)arr[3];
			this.m_nGameState = (int)arr[4];
			fGameManager.Heroplayer.canControl = false;
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[733];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			bool flag2 = (int)arr[2] == PlayerDataManager.getInstance().playercom.m_nShili;
			if (flag2)
			{
				this.SetTipText(Language.GetLang(46086, new object[0]));
			}
			else
			{
				this.SetTipText(Language.GetLang(46087, new object[0]));
			}
			base.Invoke("OnRoundEnd", 3f);
			this.huihetext.text = Language.GetLang(46088, new object[]
			{
				this.m_nRound
			});
			this.InitVinner((int)arr[0], (int)arr[1]);
			this.RobSlider.fillAmount = 0f;
			this.ZhanDian_IngSlider_QD.value = 0f;
			this.InitScrollRob(0, 0, 0);
			this.InitPlayerNum(0, 0, 0, 0);
			this.ZhanDian_Ing_QD.SetActive(false);
			this.ZhandianAddSlider.gameObject.SetActive(false);
			bool flag3 = this.m_nRound > 0;
			if (flag3)
			{
				Door_Control.Get_Instantiate.General_Door_Handle(3, "open_close", true);
			}
			PlayerDataManager.getInstance().playercom.m_nIsFristFight = 0;
			bool flag4 = fGameManager.Heroplayer != null;
			if (flag4)
			{
				fGameManager.Heroplayer.MultiRoundBegin(1);
			}
			for (int i = 0; i < this.DJSDongHua.Count; i++)
			{
				GameObject gameObject = (GameObject)this.DJSDongHua[i];
				gameObject.SetActive(false);
			}
			this.SetShowZhanDianPos(false);
		}
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x0019C4B4 File Offset: 0x0019A6B4
	public void SetReadyTime(ArrayList arr)
	{
		bool flag = (int)arr[0] > 0;
		if (flag)
		{
			this.Text_info.text = Fun.GetHintS(1);
			this.SetTipText(Fun.GetHintS(1));
			this.nTime = (int)arr[0];
			this.ReadyTimeCall(this.nTime);
			this.Suo.enabled = true;
			this.robinfo.text = Language.GetLang(46089, new object[0]);
			this.downjiantou.enabled = true;
			this.robjinbu2.color = Fun.StrToColor("ffffff", 1f);
			this.textbaifen2.color = Fun.StrToColor("ffffff", 1f);
			this.imgscr2.color = Fun.StrToColor("ffffff", 1f);
			this.textbaifen1.color = Fun.StrToColor("ffffff", 1f);
			this.imgscr1.color = Fun.StrToColor("ffffff", 1f);
			this.robjindul.color = Fun.StrToColor("ffffff", 1f);
			this.SetShowZhanDianPos(false);
			this.ZhandianAddSlider.gameObject.SetActive(false);
		}
		else
		{
			bool flag2 = (int)arr[1] > 0;
			if (flag2)
			{
				this.SetShowZhanDianPos(true);
				this.nTime = 0;
				this.robtime.text = string.Empty;
				this.robinfo.text = string.Empty;
				this.robsuo.SetActive(true);
				this.m_nLockTime = (int)arr[1];
				this.robsuotext.text = Language.GetLang(46127, new object[0]) + this.m_nLockTime.ToString();
				this.LockTime(this.m_nLockTime);
				base.Invoke("yincantext", 1f);
				this.PlayRobGameSoud();
			}
		}
	}

	// Token: 0x06002E50 RID: 11856 RVA: 0x0019C6C8 File Offset: 0x0019A8C8
	public void RoundStart(ArrayList arr)
	{
		this.ZhandianAddSlider.gameObject.SetActive(false);
		this.m_nGameState = (int)arr[4];
		this.InitMapEffectColor();
		this.ShowRound();
		this.SetReadyTime(arr);
		this.InitVinner((int)arr[5], (int)arr[6]);
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fGameManager.Heroplayer.MultiRoundBegin(0);
		}
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x0019C74C File Offset: 0x0019A94C
	private void OnRoundEnd()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.m_nRound);
		bool flag = this.m_nNowWin == fGameManager.Heroplayer.playerCommon.m_nShili;
		int num;
		if (flag)
		{
			arrayList.Add(1);
			num = 1;
		}
		else
		{
			num = 3;
			arrayList.Add(2);
		}
		bool flag2 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
		if (flag2)
		{
			arrayList.Add(this.m_nAttVinnerNum);
			arrayList.Add(this.m_nDefVinnerNum);
		}
		else
		{
			arrayList.Add(this.m_nDefVinnerNum);
			arrayList.Add(this.m_nAttVinnerNum);
		}
		bool flag3 = this.szResult.Count > 3;
		ArrayList arrayList2;
		if (flag3)
		{
			arrayList2 = new ArrayList
			{
				0,
				0,
				0,
				0,
				0
			};
		}
		else
		{
			arrayList2 = new ArrayList
			{
				0,
				0,
				0
			};
		}
		for (int i = 0; i < this.szResult.Count; i++)
		{
			arrayList2[i] = (int)this.szResult[i];
			bool flag4 = (int)this.szResult[i] == fGameManager.Heroplayer.playerCommon.m_nShili && (int)this.szResult[i] != 0;
			if (flag4)
			{
				arrayList2[i] = 1;
			}
			else
			{
				bool flag5 = (int)this.szResult[i] != 0;
				if (flag5)
				{
					arrayList2[i] = 2;
				}
			}
		}
		arrayList.Add(arrayList2);
		arrayList.Add(0);
		bool flag6 = this.szResult.Count > 3;
		if (flag6)
		{
			bool flag7 = this.m_nDefVinnerNum >= 3 || this.m_nAttVinnerNum >= 3;
			if (flag7)
			{
				arrayList[5] = num;
			}
		}
		else
		{
			bool flag8 = this.m_nDefVinnerNum >= 2 || this.m_nAttVinnerNum >= 2;
			if (flag8)
			{
				arrayList[5] = num;
			}
		}
		bool flag9 = gameuiCon.inst != null;
		if (flag9)
		{
			Debug.Log("回合结束 数组长度+" + arrayList.Count.ToString());
			gameuiCon.inst.OpenRoundEndUI(arrayList);
		}
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x0019CA48 File Offset: 0x0019AC48
	private void RobGameOver(ArrayList arr)
	{
		this.ZhandianAddSlider.gameObject.SetActive(false);
		this.EndArrData = arr;
		this.m_nGameState = (int)arr[0];
		this.m_nAttVinnerNum = (int)arr[14];
		this.m_nDefVinnerNum = (int)arr[15];
		this.m_nNowWin = (int)arr[22];
		this.m_nRound = (int)arr[14] + (int)arr[15];
		this.szResult = (ArrayList)arr[21];
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x0019CAF0 File Offset: 0x0019ACF0
	private void OnShowWin()
	{
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
		this.InitVinner((int)this.EndArrData[14], (int)this.EndArrData[15]);
		bool flag2 = (int)this.EndArrData[14] == 2;
		if (flag2)
		{
			bool flag3 = fGameManager.Heroplayer.playerCommon.m_nShili == 2;
			if (flag3)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[885];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
				this.ShibaiGuangxiao.SetActive(true);
			}
			else
			{
				PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[881];
				fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
				this.ShengliGuangxiao.SetActive(true);
			}
		}
		else
		{
			bool flag4 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
			if (flag4)
			{
				PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[885];
				fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
				this.ShibaiGuangxiao.SetActive(true);
			}
			else
			{
				PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[881];
				fUISoundManager.instance.PlayUISound(preloadData4.sUrl, 0, null, 20);
				this.ShengliGuangxiao.SetActive(true);
			}
		}
	}

	// Token: 0x06002E54 RID: 11860 RVA: 0x0019CC84 File Offset: 0x0019AE84
	private void OpenShowHeroUI()
	{
		bool flag = roleuiManager.instance != null;
		if (flag)
		{
			gameuiCon.inst.OnLoadResultSence();
		}
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x0019CCAC File Offset: 0x0019AEAC
	private void ShowRound()
	{
		fGameManager.Heroplayer.canControl = true;
		this.SetPointUI(0);
		bool flag = gameuiCon.inst != null;
		if (flag)
		{
			gameuiCon.inst.CloseRoundEndUI();
		}
	}

	// Token: 0x06002E56 RID: 11862 RVA: 0x0019CCEC File Offset: 0x0019AEEC
	public void UpDateQdState(ArrayList arr)
	{
		this.mEffcetDianLan.SetActive(false);
		this.mEffcetDianHong.SetActive(false);
		bool flag = (int)arr[0] == fGameManager.Heroplayer.playerCommon.m_nShili;
		if (flag)
		{
			bool activeSelf = this.ShowObj.activeSelf;
			if (activeSelf)
			{
				this.mEffcetDianLan.SetActive(true);
			}
			bool flag2 = this.maplantiao != null;
			if (flag2)
			{
				this.maplantiao.SetActive(true);
			}
			bool flag3 = this.maphongtiao != null;
			if (flag3)
			{
				this.maphongtiao.SetActive(false);
			}
			bool canControl = fGameManager.Heroplayer.canControl;
			if (canControl)
			{
				fUISoundManager.instance.PlayUISound("bisai_judianzhanlingchenggong", 0, null, 20);
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1506];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			}
		}
		else
		{
			bool activeSelf2 = this.ShowObj.activeSelf;
			if (activeSelf2)
			{
				this.mEffcetDianHong.SetActive(true);
			}
			bool canControl2 = fGameManager.Heroplayer.canControl;
			if (canControl2)
			{
				PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1504];
				fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
			}
			bool flag4 = this.maplantiao != null;
			if (flag4)
			{
				this.maplantiao.SetActive(false);
			}
			bool flag5 = this.maphongtiao != null;
			if (flag5)
			{
				this.maphongtiao.SetActive(true);
			}
		}
		this.SetPointUI((int)arr[0]);
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x0019CEA4 File Offset: 0x0019B0A4
	private void SetRobInfo(ArrayList arr)
	{
		this.InitScrollRob((int)arr[7], (int)arr[8], (int)arr[11]);
		this.InitPlayerNum((int)arr[3], (int)arr[4], (int)arr[1], (int)arr[11]);
		this.InitVinner((int)arr[9], (int)arr[10]);
		this.SetQdScrShow(arr);
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x0019CF40 File Offset: 0x0019B140
	public void TongBuScr(ArrayList arr)
	{
		this.InitAttUi((int)arr[2], (int)arr[0], (int)arr[1], (int)arr[4], (int)arr[5]);
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x0019CF94 File Offset: 0x0019B194
	private void SetQdScrShow(ArrayList arr)
	{
		int netid = fGameManager.Heroplayer.getNetid();
		ArrayList arrayList = (ArrayList)arr[5];
		bool activeSelf = this.ZhanDian_Ing_QD.activeSelf;
		if (activeSelf)
		{
			this.ZhanDian_Ing_QD.SetActive(false);
		}
		bool flag = arrayList.IndexOf(netid) != -1;
		if (flag)
		{
			bool activeSelf2 = this.ShowObj.activeSelf;
			if (activeSelf2)
			{
				this.mEffcetDianLan.SetActive(false);
				this.mEffcetDianHong.SetActive(false);
				this.ShowObj.SetActive(false);
			}
			bool flag2 = fGameManager.Heroplayer.playerCommon.m_nShili == (int)arr[1] && (int)arr[11] != fGameManager.Heroplayer.playerCommon.m_nShili;
			if (flag2)
			{
				this.ZhanDian_Ing_QD.SetActive(true);
			}
		}
		else
		{
			bool flag3 = !this.ShowObj.activeSelf;
			if (flag3)
			{
				this.ShowObj.SetActive(true);
			}
		}
	}

	// Token: 0x06002E5A RID: 11866 RVA: 0x0019D0AC File Offset: 0x0019B2AC
	private void SetPointUI(int nType)
	{
		Image component = this.ShowObj.transform.FindChild("jiantou").GetComponent<Image>();
		bool flag = nType == 0;
		if (flag)
		{
			component.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "zlzs1");
			this.BkColor.color = Color.red;
			this.ZDStateText.color = Color.red;
			this.ZDStateText.text = Language.GetLang(46077, new object[0]);
			this.changeimg.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_muibiao_suoding");
		}
		else
		{
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == nType;
			if (flag2)
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "zlzs1-1");
				this.BkColor.color = Fun.StrToColor("00EBF5", 1f);
				this.ZDStateText.text = Language.GetLang(46081, new object[0]);
				this.ZDStateText.color = Fun.StrToColor("00EBF5", 1f);
				this.changeimg.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_mubiaodise_youfang");
			}
			else
			{
				component.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "zlzs1");
				this.BkColor.color = Color.red;
				this.ZDStateText.color = Color.red;
				this.ZDStateText.text = Language.GetLang(46077, new object[0]);
				this.changeimg.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_mubiaodise_difang");
			}
		}
		this.RobSlider.fillAmount = 0f;
		this.ZhanDian_IngSlider_QD.value = 0f;
	}

	// Token: 0x06002E5B RID: 11867 RVA: 0x0019D288 File Offset: 0x0019B488
	private void InitAttUi(int nstate, int natt, int ndef, int ntype, int nSpeed)
	{
		this.m_nSpeed = nSpeed * 10;
		int num = nstate;
		bool flag = natt > 0;
		if (flag)
		{
			num = 1;
		}
		bool flag2 = ndef > 0;
		if (flag2)
		{
			num = 2;
		}
		this.m_nIsAddScr = ntype;
		bool flag3 = this.m_nIsAddScr == 1;
		if (flag3)
		{
			bool flag4 = PlayerDataManager.getInstance().playercom.m_nShili == num && !fUISoundManager.instance.SoundIsPlay("bisai_judianzhanling");
			if (flag4)
			{
				fUISoundManager.instance.PlayUISound("bisai_judianzhanling", 0, null, 20);
			}
		}
		else
		{
			fUISoundManager.instance.StopSound("bisai_judianzhanling", 0);
		}
		this.m_nTickTime = Environment.TickCount;
		bool flag5 = nstate != 3;
		if (flag5)
		{
			bool flag6 = PlayerDataManager.getInstance().playercom.m_nShili == num;
			if (flag6)
			{
				bool flag7 = this.RobSlider.sprite.name != "jindu_zhengduo_jindutiao_lan";
				if (flag7)
				{
					this.RobSlider.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_jindutiao_lan");
				}
			}
			else
			{
				bool flag8 = this.RobSlider.sprite.name != "jindu_zhengduo_jindutiao_hong";
				if (flag8)
				{
					this.RobSlider.sprite = Window.spriteName(AtlasType.ATLAS_ROB, "jindu_zhengduo_jindutiao_hong");
				}
			}
		}
		bool flag9 = this.ZhanDian_IngSlider_QD.value == 1f && this.ZhanDian_Ing_QD.activeSelf;
		if (flag9)
		{
			this.ZhanDian_Ing_QD.SetActive(false);
		}
	}

	// Token: 0x06002E5C RID: 11868 RVA: 0x0019D412 File Offset: 0x0019B612
	public void Server_ScrStart()
	{
	}

	// Token: 0x06002E5D RID: 11869 RVA: 0x0019D418 File Offset: 0x0019B618
	public void AddTimeUICon(int isover, int time)
	{
		bool flag = isover == 3;
		if (flag)
		{
			this.m_nIsAddScrJiaShi = 1;
			this.ZhandianAddSlider.gameObject.SetActive(false);
		}
		else
		{
			this.m_nIsAddScrJiaShi = isover;
			bool flag2 = !this.ZhandianAddSlider.gameObject.activeSelf;
			if (flag2)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[870];
				bool flag3 = !fUISoundManager.instance.SoundIsPlay(preloadData.sUrl) && !this.bJiaShiCD;
				if (flag3)
				{
					this.bJiaShiCD = true;
					base.Invoke("JiaShiSoundCD", 15f);
					fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
				}
			}
			this.ZhandianAddSlider.gameObject.SetActive(true);
			bool flag4 = this.m_nIsAddScrJiaShi == 1;
			if (flag4)
			{
				this.ZhandianAddSlider.value = 1f;
			}
			bool flag5 = this.m_nIsAddScrJiaShi == 0;
			if (flag5)
			{
				this.m_nTickTimeJiaShi = Environment.TickCount;
			}
		}
	}

	// Token: 0x06002E5E RID: 11870 RVA: 0x0019D523 File Offset: 0x0019B723
	private void JiaShiSoundCD()
	{
		base.CancelInvoke("JiaShiSoundCD");
		this.bJiaShiCD = false;
	}

	// Token: 0x06002E5F RID: 11871 RVA: 0x0019D539 File Offset: 0x0019B739
	private void DuoQuSoundCD()
	{
		base.CancelInvoke("DuoQuSoundCD");
		this.bDuoQuCD = false;
	}

	// Token: 0x06002E60 RID: 11872 RVA: 0x0019D54F File Offset: 0x0019B74F
	private void AttSoundCD()
	{
		base.CancelInvoke("AttSoundCD");
		this.bAttCD = false;
	}

	// Token: 0x06002E61 RID: 11873 RVA: 0x0019D568 File Offset: 0x0019B768
	private void InitScrollRob(int attnum, int defnum, int nType)
	{
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			this.robjindul.text = attnum.ToString();
			this.robjinbu2.text = defnum.ToString();
			this.LeftSlider.value = (float)attnum / 100f;
			this.RightSlider.value = (float)defnum / 100f;
			bool flag2 = nType == 1;
			if (flag2)
			{
				this.textbaifen1.color = Fun.StrToColor("ffffff", 1f);
				this.imgscr1.color = Fun.StrToColor("ffffff", 1f);
				this.robjindul.color = Fun.StrToColor("ffffff", 1f);
				this.robjinbu2.color = Fun.StrToColor("ffffff", 0.5f);
				this.textbaifen2.color = Fun.StrToColor("ffffff", 0.5f);
				this.imgscr2.color = Fun.StrToColor("ffffff", 0.5f);
			}
			bool flag3 = nType == 2;
			if (flag3)
			{
				this.textbaifen1.color = Fun.StrToColor("ffffff", 0.5f);
				this.imgscr1.color = Fun.StrToColor("ffffff", 0.5f);
				this.robjindul.color = Fun.StrToColor("ffffff", 0.5f);
				this.robjinbu2.color = Fun.StrToColor("ffffff", 1f);
				this.textbaifen2.color = Fun.StrToColor("ffffff", 1f);
				this.imgscr2.color = Fun.StrToColor("ffffff", 1f);
			}
		}
		else
		{
			this.robjindul.text = defnum.ToString();
			this.robjinbu2.text = attnum.ToString();
			this.LeftSlider.value = (float)defnum / 100f;
			this.RightSlider.value = (float)attnum / 100f;
			bool flag4 = nType == 1;
			if (flag4)
			{
				this.textbaifen1.color = Fun.StrToColor("ffffff", 0.5f);
				this.imgscr1.color = Fun.StrToColor("ffffff", 0.5f);
				this.robjindul.color = Fun.StrToColor("ffffff", 0.5f);
				this.robjinbu2.color = Fun.StrToColor("ffffff", 1f);
				this.textbaifen2.color = Fun.StrToColor("ffffff", 1f);
				this.imgscr2.color = Fun.StrToColor("ffffff", 1f);
			}
			bool flag5 = nType == 2;
			if (flag5)
			{
				this.textbaifen1.color = Fun.StrToColor("ffffff", 1f);
				this.imgscr1.color = Fun.StrToColor("ffffff", 1f);
				this.robjindul.color = Fun.StrToColor("ffffff", 1f);
				this.robjinbu2.color = Fun.StrToColor("ffffff", 0.5f);
				this.textbaifen2.color = Fun.StrToColor("ffffff", 0.5f);
				this.imgscr2.color = Fun.StrToColor("ffffff", 0.5f);
			}
		}
		bool flag6 = this.m_nOpen == 0 && attnum + defnum > 0;
		if (flag6)
		{
			bool activeSelf = this.robsuo.activeSelf;
			if (activeSelf)
			{
				this.robsuo.SetActive(false);
			}
			this.Suo.enabled = false;
			this.robsuotext.text = string.Empty;
			Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			bool flag7 = this.m_nGameState > 0;
			if (flag7)
			{
				this.m_nOpen = 1;
			}
		}
	}

	// Token: 0x06002E62 RID: 11874 RVA: 0x0019D978 File Offset: 0x0019BB78
	private void OnBigScrollPoint(int attnum, int defnum, int nType)
	{
		fUISoundManager.instance.StopSound("bisai_daojishi", 0);
		bool flag = attnum > 85 && nType == 1;
		if (flag)
		{
			fUISoundManager.instance.PlayUISound("bisai_daojishi", 0, null, 20);
		}
		bool flag2 = defnum > 85 && nType == 2;
		if (flag2)
		{
			fUISoundManager.instance.PlayUISound("bisai_daojishi", 0, null, 20);
		}
	}

	// Token: 0x06002E63 RID: 11875 RVA: 0x0019D9E4 File Offset: 0x0019BBE4
	private void InitPlayerNum(int attnum, int defnum, int nsate, int ntype)
	{
		bool flag = this.m_nNowZhanLing != nsate && nsate != 0;
		if (flag)
		{
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == nsate && nsate != ntype;
			if (flag2)
			{
				bool flag3 = !this.bDuoQuCD;
				if (flag3)
				{
					this.bDuoQuCD = true;
					base.Invoke("DuoQuSoundCD", 15f);
					bool canControl = fGameManager.Heroplayer.canControl;
					if (canControl)
					{
						PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1505];
						fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
					}
				}
			}
			else
			{
				bool flag4 = ntype == PlayerDataManager.getInstance().playercom.m_nShili && ntype != nsate && !this.bAttCD;
				if (flag4)
				{
					this.bAttCD = true;
					base.Invoke("AttSoundCD", 15f);
					bool canControl2 = fGameManager.Heroplayer.canControl;
					if (canControl2)
					{
						PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1503];
						fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
					}
				}
			}
		}
		this.m_nNowZhanLing = nsate;
		bool flag5 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag5)
		{
			this.leftnum.text = attnum.ToString();
			this.rightnum.text = defnum.ToString();
		}
		else
		{
			this.leftnum.text = defnum.ToString();
			this.rightnum.text = attnum.ToString();
		}
		bool flag6 = nsate == 3;
		if (flag6)
		{
			this.robzhenduo.enabled = true;
			this.downjiantou.enabled = true;
		}
		else
		{
			this.robzhenduo.enabled = false;
			this.downjiantou.enabled = false;
		}
	}

	// Token: 0x06002E64 RID: 11876 RVA: 0x0019DBD0 File Offset: 0x0019BDD0
	private void InitVinner(int attnum, int defnum)
	{
		this.robleft1.enabled = false;
		this.robleft2.enabled = false;
		this.robleft3.enabled = false;
		this.robright1.enabled = false;
		this.robright2.enabled = false;
		this.robright3.enabled = false;
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			bool flag2 = attnum == 1;
			if (flag2)
			{
				this.robleft1.enabled = true;
			}
			bool flag3 = attnum == 2;
			if (flag3)
			{
				this.robleft1.enabled = true;
				this.robleft2.enabled = true;
			}
			bool flag4 = attnum == 3;
			if (flag4)
			{
				this.robleft1.enabled = true;
				this.robleft2.enabled = true;
				this.robleft3.enabled = true;
			}
			bool flag5 = defnum == 1;
			if (flag5)
			{
				this.robright1.enabled = true;
			}
			bool flag6 = defnum == 2;
			if (flag6)
			{
				this.robright1.enabled = true;
				this.robright2.enabled = true;
			}
			bool flag7 = defnum == 3;
			if (flag7)
			{
				this.robright1.enabled = true;
				this.robright2.enabled = true;
				this.robright3.enabled = true;
			}
		}
		else
		{
			bool flag8 = defnum == 1;
			if (flag8)
			{
				this.robleft1.enabled = true;
			}
			bool flag9 = defnum == 2;
			if (flag9)
			{
				this.robleft1.enabled = true;
				this.robleft2.enabled = true;
			}
			bool flag10 = defnum == 3;
			if (flag10)
			{
				this.robleft1.enabled = true;
				this.robleft2.enabled = true;
				this.robleft3.enabled = true;
			}
			bool flag11 = attnum == 1;
			if (flag11)
			{
				this.robright1.enabled = true;
			}
			bool flag12 = attnum == 2;
			if (flag12)
			{
				this.robright1.enabled = true;
				this.robright2.enabled = true;
			}
			bool flag13 = attnum == 3;
			if (flag13)
			{
				this.robright1.enabled = true;
				this.robright2.enabled = true;
				this.robright3.enabled = true;
			}
		}
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x0019DE10 File Offset: 0x0019C010
	public void LockTime(int ntime)
	{
		this.m_nLockTime = ntime;
		bool flag = this.m_nLockTime == 0;
		if (flag)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[734];
			fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
			bool activeSelf = this.robsuo.activeSelf;
			if (activeSelf)
			{
				this.robsuo.SetActive(false);
			}
			this.Suo.enabled = false;
			this.robsuotext.text = string.Empty;
			this.downjiantou.enabled = false;
			this.QdNewPlayerMsg();
		}
		else
		{
			this.robsuotext.text = Language.GetLang(46127, new object[0]) + this.m_nLockTime.ToString();
		}
		bool flag2 = this.m_nOpen == 0;
		if (flag2)
		{
			Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			bool flag3 = this.m_nGameState > 0;
			if (flag3)
			{
				this.m_nOpen = 1;
			}
		}
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x0019DF1C File Offset: 0x0019C11C
	public void GameOneOver(ArrayList arr)
	{
		base.CancelInvoke("TimeCall");
		fUISoundManager.instance.StopSound("bisai_gaochaoyinyue", 0);
		fUISoundManager.instance.StopSound("bisai_judianzhanling", 0);
		fUISoundManager.instance.StopSound("bisai_judianbeizhanling", 0);
		this.Text_time.text = Fun.sencondsToHMS_CXP((int)arr[2]);
		this.m_nGameState = (int)arr[0];
		this.nTime = (int)arr[2];
		this.ZhanDian_Ing.SetActive(false);
		this.Scrollbar.SetActive(false);
		this.pnumobj.SetActive(false);
		this.ZhanDian_State.SetActive(false);
		this.ZLScrollbar.SetActive(false);
		bool flag = this.fangxiangxian != null;
		if (flag)
		{
			this.fangxiangxian.SetActive(false);
		}
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[861];
		fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
		Door_Control.Get_Instantiate.Effect_Door_Handle(PlayerDataManager.getInstance().playercom.m_nShili, this.m_nGameState, false);
		this.BkState = false;
		bool flag2 = this.m_nMapType == 2;
		if (flag2)
		{
			this.SetCartUi(arr);
		}
		else
		{
			this.InitTween();
			this.InitBkColor();
			this.SetDianIcon();
			bool flag3 = this.m_nGameState == 2;
			if (flag3)
			{
				Image component = this.ShowObj.transform.FindChild("didianfuhao").GetComponent<Image>();
				component.sprite = Window.spriteName(AtlasType.ATLAS_ZHANDIAN, "B");
				bool flag4 = this.EffectObj1 != null;
				if (flag4)
				{
					this.EffectObj1.SetActive(false);
				}
				bool flag5 = this.EffectObj2 != null;
				if (flag5)
				{
					this.EffectObj2.SetActive(true);
				}
				bool flag6 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
				if (flag6)
				{
					this.Text_vinner.color = Color.red;
					this.SetTipText(string.Format(Fun.GetHintS(2), "B"));
					fUISoundManager.instance.PlayUISound("bisai_xiaoshengli", 0, null, 20);
					preloadData = fGameManager.mloadConfig.AllProLodaList[872];
					fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
				}
				else
				{
					this.Text_vinner.color = Color.blue;
					this.SetTipText(string.Format(Fun.GetHintS(3), "B"));
					fUISoundManager.instance.PlayUISound("bisai_xiaoshibai", 0, null, 20);
					preloadData = fGameManager.mloadConfig.AllProLodaList[863];
					fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
				}
			}
			this.Image_A.SetNativeSize();
			this.Image_B.SetNativeSize();
		}
		bool flag7 = this.m_nGameState > 0;
		if (flag7)
		{
			Door_Control.Get_Instantiate.General_Door_Handle(this.m_nGameState, "close_open", true);
			this.m_nOpen = 1;
		}
		this.Refresh_Map_Effect();
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x0019E250 File Offset: 0x0019C450
	public void UpdateCarDis(ArrayList arr)
	{
		int num = (int)arr[1];
		int num2 = (int)arr[2];
		int num3 = (int)arr[3];
		int num4 = (int)arr[4];
		bool flag = num3 != 0;
		if (flag)
		{
			float num5 = (float)num3 / 100f;
			bool flag2 = !this.carsound && num5 <= 20f && num4 == 1;
			if (flag2)
			{
				PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[871];
				fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
				this.carsound = true;
			}
			bool flag3 = num5 <= 5f;
			if (flag3)
			{
				this.wenzibeijin.SetActive(true);
				this.DistanceText.text = Language.GetLang(46085, new object[]
				{
					num5.ToString("0.00")
				});
			}
			else
			{
				this.wenzibeijin.SetActive(false);
				this.DistanceText.text = string.Empty;
			}
		}
		else
		{
			this.wenzibeijin.SetActive(false);
			this.DistanceText.text = string.Empty;
		}
		this.CartSlider.value = (float)num * 1f / (float)num2;
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x0019E3A8 File Offset: 0x0019C5A8
	public void UpdateUserNum(ArrayList arr)
	{
		int num = (int)arr[1];
		this.CartPlayerNum.text = num.ToString();
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x0019E3D8 File Offset: 0x0019C5D8
	public void UpdateCarstate(ArrayList arr)
	{
		int num = (int)arr[1];
		bool flag = num == 1;
		if (flag)
		{
			this.CartZhengDuo.SetActive(true);
			this.SetCarJianTouMove();
			this.CatZd.SetActive(true);
		}
		else
		{
			this.CatZd.SetActive(false);
			this.CartZhengDuo.SetActive(false);
		}
		this.PlaySound_car(num);
		this.SetNewPlayerMsgStat(num);
	}

	// Token: 0x06002E6A RID: 11882 RVA: 0x0019E44C File Offset: 0x0019C64C
	private void PlaySound_car(int _Flag)
	{
		bool flag = _Flag == 1 && PlayerDataManager.getInstance().playerCommon.m_nShili == 1;
		if (flag)
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1490];
			Sound_Manager.Instantiation.Play_Sound("Chariot", "Fight", preloadData.sUrl);
		}
		bool flag2 = _Flag == 0 && PlayerDataManager.getInstance().playerCommon.m_nShili == 1;
		if (flag2)
		{
			PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1491];
			Sound_Manager.Instantiation.Play_Sound("Chariot", "Forward", preloadData2.sUrl);
		}
		bool flag3 = _Flag == 0 && PlayerDataManager.getInstance().playerCommon.m_nShili == 2;
		if (flag3)
		{
			PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[1488];
			Sound_Manager.Instantiation.Play_Sound("Chariot", "Forward", preloadData3.sUrl);
		}
		bool flag4 = (_Flag == -1 || _Flag == 2) && PlayerDataManager.getInstance().playerCommon.m_nShili == 1 && ZhanDianCon.inst.nIsPlaySound > 0;
		if (flag4)
		{
			PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[1487];
			Sound_Manager.Instantiation.Play_Sound("Chariot", "Backward", preloadData4.sUrl);
		}
	}

	// Token: 0x06002E6B RID: 11883 RVA: 0x0019E5B0 File Offset: 0x0019C7B0
	public void OnBigScrollPoint(ArrayList arr)
	{
		bool flag = this.m_nMapType == 3;
		if (flag)
		{
			this.OnBigScrollPoint((int)arr[7], (int)arr[8], (int)arr[11]);
		}
	}

	// Token: 0x06002E6C RID: 11884 RVA: 0x0019E5FC File Offset: 0x0019C7FC
	public void UpdataScrollbar(ArrayList arr)
	{
		this.m_nGameState = (int)arr[0];
		bool flag = this.m_nMapType == 3;
		if (flag)
		{
			this.SetRobInfo(arr);
		}
		else
		{
			ArrayList arrayList = (ArrayList)arr[5];
			bool flag2 = !this.Scrollbar.activeSelf;
			if (flag2)
			{
				this.Scrollbar.SetActive(true);
			}
			bool flag3 = (int)arr[0] == 1;
			if (flag3)
			{
				this.Scrollbar.transform.localPosition = new Vector3(-0.65f, 0f, 0f);
				this.pnumobj.transform.localPosition = new Vector3(21.8f, -31.6f, 0f);
				this.ZhanDian_State.transform.localPosition = new Vector3(0.19f, 0f, 0f);
			}
			else
			{
				this.Scrollbar.transform.localPosition = new Vector3(56f, 0f, 0f);
				this.pnumobj.transform.localPosition = new Vector3(78.29f, -31.6f, 0f);
				this.ZhanDian_State.transform.localPosition = new Vector3(56.3f, 0f, 0f);
			}
			bool flag4 = (float)((int)arr[2]) * 1f / 180f == 0f;
			if (flag4)
			{
				this.Scrollimg.fillAmount = 0f;
				bool activeSelf = this.Scrollbar.activeSelf;
				if (activeSelf)
				{
					this.Scrollbar.SetActive(false);
				}
				bool activeSelf2 = this.ZLScrollbar.activeSelf;
				if (activeSelf2)
				{
					this.ZLScrollbar.SetActive(false);
				}
			}
			else
			{
				this.Scrollimg.fillAmount = (float)((int)arr[2]) * 1f / 180f;
			}
			bool flag5 = (int)arr[3] != 0 && (int)arr[4] != 0;
			if (flag5)
			{
				fUISoundManager.instance.PlayUISound("bisai_judianbeizhanling", 0, null, 20);
				bool flag6 = !this.SoundOff;
				if (flag6)
				{
					this.SoundOff = true;
					bool flag7 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
					if (flag7)
					{
						PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1505];
						fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
					}
					else
					{
						PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1503];
						fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
					}
					base.CancelInvoke("InitSoundOff");
					base.Invoke("InitSoundOff", 15f);
				}
				this.StateText.text = Language.GetLang(46082, new object[0]);
				bool flag8 = !this.Scrollbar.activeSelf;
				if (flag8)
				{
					this.Scrollbar.SetActive(true);
				}
				this.SetJianTouMove();
				bool flag9 = !this.ZhanDian_State.activeSelf;
				if (flag9)
				{
					this.ZhanDian_State.SetActive(true);
				}
				this.SetTip_NoDo(Language.GetLang(46083, new object[0]), "DEBD20");
			}
			else
			{
				fUISoundManager.instance.StopSound("bisai_judianbeizhanling", 0);
				this.StateText.text = Language.GetLang(46084, new object[0]);
				bool activeSelf3 = this.ZhanDian_State.activeSelf;
				if (activeSelf3)
				{
					this.ZhanDian_State.SetActive(false);
				}
				this.InitTween();
				this.yincantext();
			}
			bool flag10 = (int)arr[3] != 0;
			if (flag10)
			{
				bool flag11 = !this.pnumobj.activeSelf;
				if (flag11)
				{
					this.pnumobj.SetActive(true);
				}
				this.Text_pnum.text = arr[3].ToString();
			}
			else
			{
				bool activeSelf4 = this.pnumobj.activeSelf;
				if (activeSelf4)
				{
					this.pnumobj.SetActive(false);
				}
			}
			int netid = fGameManager.Heroplayer.getNetid();
			bool flag12 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
			if (flag12)
			{
				bool flag13 = (int)arr[1] == 1;
				if (flag13)
				{
					bool flag14 = arrayList.IndexOf(netid) != -1;
					if (flag14)
					{
						bool flag15 = !this.ZhanDian_Ing.activeSelf;
						if (flag15)
						{
							this.ZhanDian_Ing.SetActive(true);
						}
						bool flag16 = !fUISoundManager.instance.SoundIsPlay("bisai_judianzhanling");
						if (flag16)
						{
							fUISoundManager.instance.PlayUISound("bisai_judianzhanling", 0, null, 20);
						}
						this.ZhanDian_IngSlider.value = (float)((int)arr[2]) * 1f / 180f;
					}
				}
				else
				{
					fUISoundManager.instance.StopSound("bisai_judianzhanling", 0);
					bool flag17 = arrayList.IndexOf(netid) == -1 && this.ZhanDian_Ing.activeSelf;
					if (flag17)
					{
						this.ZhanDian_Ing.SetActive(false);
					}
				}
			}
			else
			{
				bool flag18 = (int)arr[1] == 1;
				if (flag18)
				{
					bool flag19 = !fUISoundManager.instance.SoundIsPlay("bisai_judianbeizhanling");
					if (flag19)
					{
						fUISoundManager.instance.PlayUISound("bisai_judianbeizhanling", 0, null, 20);
					}
				}
				else
				{
					bool flag20 = !this.ZhanDian_State.activeSelf;
					if (flag20)
					{
						fUISoundManager.instance.StopSound("bisai_judianbeizhanling", 0);
					}
				}
			}
			bool flag21 = arrayList.IndexOf(netid) != -1;
			if (flag21)
			{
				this.SetShowZhanDianPos(false);
			}
			else
			{
				this.SetShowZhanDianPos(true);
			}
			bool flag22 = (int)arr[1] == 0;
			if (flag22)
			{
				this.BkState = false;
				this.InitBkColor();
			}
			else
			{
				this.BkState = true;
			}
		}
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x0019EC2E File Offset: 0x0019CE2E
	private void InitSoundOff()
	{
		base.CancelInvoke("InitSoundOff");
		this.SoundOff = false;
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x0019EC44 File Offset: 0x0019CE44
	private void SetShowZhanDianPos(bool bl)
	{
		this.ShowObj.SetActive(bl);
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x0019EC54 File Offset: 0x0019CE54
	private void SetZhanDianScr(float fl)
	{
		bool flag = !this.ZLScrollbar.activeSelf;
		if (flag)
		{
			this.ZLScrollbar.SetActive(true);
		}
		this.ZLScrollbar_Slider.fillAmount = fl;
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x0019EC90 File Offset: 0x0019CE90
	private void InitBkColor()
	{
		bool flag = PlayerDataManager.getInstance().playercom.m_nShili == 1;
		if (flag)
		{
			this.BkColor.color = Color.red;
		}
		else
		{
			this.BkColor.color = Fun.StrToColor("00EBF5", 1f);
		}
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x0019ECE8 File Offset: 0x0019CEE8
	private void SetJianTouMove()
	{
		bool flag = this.m_Lpos != null;
		if (flag)
		{
			this.m_Lpos.Play<Tweener>();
			this.m_Rpos.Play<Tweener>();
		}
		else
		{
			this.m_Lpos = this.leftImage.rectTransform.DOLocalMoveX(-47f, 0.3f, false);
			this.m_Lpos.SetEase(Ease.OutCubic);
			this.m_Lpos.SetLoops(-1, LoopType.Yoyo);
			this.m_Rpos = this.RightImage.rectTransform.DOLocalMoveX(52f, 0.3f, false);
			this.m_Rpos.SetEase(Ease.OutCubic);
			this.m_Rpos.SetLoops(-1, LoopType.Yoyo);
		}
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x0019ED98 File Offset: 0x0019CF98
	private void SetCarJianTouMove()
	{
		bool flag = this.m_Lpos != null;
		if (flag)
		{
			this.m_Lpos.Play<Tweener>();
			this.m_Rpos.Play<Tweener>();
		}
		else
		{
			this.m_Lpos = this.carleftImage.rectTransform.DOLocalMoveX(-47f, 0.3f, false);
			this.m_Lpos.SetEase(Ease.OutCubic);
			this.m_Lpos.SetLoops(-1, LoopType.Yoyo);
			this.m_Rpos = this.carRightImage.rectTransform.DOLocalMoveX(52f, 0.3f, false);
			this.m_Rpos.SetEase(Ease.OutCubic);
			this.m_Rpos.SetLoops(-1, LoopType.Yoyo);
		}
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x0019EE48 File Offset: 0x0019D048
	private void InitTween()
	{
		bool flag = this.m_Lpos != null;
		if (flag)
		{
			this.m_Lpos.Pause<Tweener>();
		}
		bool flag2 = this.m_Rpos != null;
		if (flag2)
		{
			this.m_Rpos.Pause<Tweener>();
		}
	}

	// Token: 0x06002E74 RID: 11892 RVA: 0x0019EE8C File Offset: 0x0019D08C
	public void GameOver(ArrayList arr)
	{
		this.m_nGameOver = 1;
		base.CancelInvoke("TimeCall");
		fGameManager.Heroplayer.canControl = false;
		bool flag = this.m_nMapType == 3;
		if (flag)
		{
			this.RobGameOver(arr);
		}
		else
		{
			this.m_nGameState = 0;
			this.wenzibeijin.SetActive(false);
			this.DistanceText.text = string.Empty;
			this.GetMeVinner((int)arr[0], (int)arr[5], (int)arr[9]);
		}
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x0019EF24 File Offset: 0x0019D124
	private void GetMeVinner(int nGameState, int nStronghold, int nAllSt)
	{
		fUISoundManager.instance.StopSound("bisai_gaochaoyinyue", 0);
		fUISoundManager.instance.StopSound("bisai_judianzhanling", 0);
		fUISoundManager.instance.StopSound("bisai_judianbeizhanling", 0);
		if (nGameState != 1)
		{
			if (nGameState - 2 <= 3)
			{
				bool flag = nStronghold == nAllSt;
				if (flag)
				{
					bool flag2 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
					if (flag2)
					{
						PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[881];
						fUISoundManager.instance.PlayUISound(preloadData.sUrl, 0, null, 20);
						this.ShengliGuangxiao.SetActive(true);
					}
					else
					{
						PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[885];
						fUISoundManager.instance.PlayUISound(preloadData2.sUrl, 0, null, 20);
						this.ShibaiGuangxiao.SetActive(true);
					}
				}
				else
				{
					bool flag3 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
					if (flag3)
					{
						PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[885];
						fUISoundManager.instance.PlayUISound(preloadData3.sUrl, 0, null, 20);
						this.ShibaiGuangxiao.SetActive(true);
					}
					else
					{
						PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[881];
						fUISoundManager.instance.PlayUISound(preloadData4.sUrl, 0, null, 20);
						this.ShengliGuangxiao.SetActive(true);
					}
				}
			}
		}
		else
		{
			bool flag4 = nStronghold != 1;
			if (flag4)
			{
				bool flag5 = fGameManager.Heroplayer.playerCommon.m_nShili == 1;
				if (flag5)
				{
					PreloadData preloadData5 = fGameManager.mloadConfig.AllProLodaList[885];
					fUISoundManager.instance.PlayUISound(preloadData5.sUrl, 0, null, 20);
					this.ShibaiGuangxiao.SetActive(true);
				}
				else
				{
					PreloadData preloadData6 = fGameManager.mloadConfig.AllProLodaList[881];
					fUISoundManager.instance.PlayUISound(preloadData6.sUrl, 0, null, 20);
					this.ShengliGuangxiao.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x0019F158 File Offset: 0x0019D358
	public void GotoHall()
	{
		this.AllindoObj.SetActive(false);
		this.ShengliGuangxiao.SetActive(false);
		this.ShibaiGuangxiao.SetActive(false);
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			fUIManager.GetInstance().GotoHall(0);
		}
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x0019F1A7 File Offset: 0x0019D3A7
	private void ShowGameOverImg()
	{
		sysload.loadImageNoSetN(this.GameOverImg, "GameUI/gameover.txt", new sysload.loadImageFinish(this.LoadimageOk));
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x0019F1C7 File Offset: 0x0019D3C7
	public void LoadimageOk(object oPam, object oPam1)
	{
		this.GameOverImg.enabled = true;
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x0019F1D8 File Offset: 0x0019D3D8
	public void UpDateCarDis(int nDis, int nAllDis, float fDistance, HeroPlayer hero, int _State)
	{
		float num = fDistance * 100f;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(5013);
		arrayList.Add(nDis);
		arrayList.Add(nAllDis);
		arrayList.Add((int)num);
		arrayList.Add(_State);
		if (hero != null)
		{
			hero.SendMessageToServer(5016, arrayList);
		}
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x0019F250 File Offset: 0x0019D450
	public void UpDateCarNum(int nNum, HeroPlayer hero)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(5014);
		arrayList.Add(nNum);
		arrayList.Add(0);
		if (hero != null)
		{
			hero.SendMessageToServer(5016, arrayList);
		}
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x0019F2A4 File Offset: 0x0019D4A4
	public void UpDateCaPoint(int nState, int _Over, HeroPlayer hero)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(5015);
		arrayList.Add(nState);
		arrayList.Add(_Over);
		if (hero != null)
		{
			hero.SendMessageToServer(5016, arrayList);
		}
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x0019F2F8 File Offset: 0x0019D4F8
	public void UpDateCarState(ArrayList userlist, int nState, HeroPlayer hero)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(5017);
		arrayList.Add(nState);
		arrayList.Add(userlist);
		if (hero != null)
		{
			hero.SendMessageToServer(5016, arrayList);
		}
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x0019F344 File Offset: 0x0019D544
	public override void Reset()
	{
		base.Reset();
		this.m_nOpen = 0;
		this.AllindoObj.SetActive(false);
		this.ShengliGuangxiao.SetActive(false);
		this.ShibaiGuangxiao.SetActive(false);
		for (int i = 0; i < this.DJSDongHua.Count; i++)
		{
			GameObject gameObject = (GameObject)this.DJSDongHua[i];
			gameObject.SetActive(false);
		}
		this.Refresh_Map_Effect();
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x0019F3C4 File Offset: 0x0019D5C4
	private void Refresh_Map_Effect()
	{
		GameObject gameObject = null;
		int num = (this.m_nGameState < 1) ? 1 : this.m_nGameState;
		switch (num)
		{
		case 1:
			gameObject = fUIManager.MapEffectObj1;
			break;
		case 2:
			gameObject = fUIManager.MapEffectObj2;
			break;
		case 3:
			gameObject = fUIManager.MapEffectObj3;
			break;
		}
		bool flag = !(gameObject == null);
		if (flag)
		{
			gameObject.transform.FindChild("boxjudian(Clone)").gameObject.SetActive(true);
			gameObject.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(true);
			gameObject.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
			switch (num)
			{
			case 2:
				gameObject = fUIManager.MapEffectObj1;
				break;
			case 3:
				gameObject = fUIManager.MapEffectObj2;
				break;
			case 4:
				gameObject = fUIManager.MapEffectObj3;
				break;
			default:
				return;
			}
			bool flag2 = !(gameObject == null);
			if (flag2)
			{
				gameObject.transform.FindChild("boxjudian(Clone)").gameObject.SetActive(false);
				gameObject.transform.FindChild("boxjudian(Clone)/lan_bianse_1").gameObject.SetActive(false);
				gameObject.transform.FindChild("boxjudian(Clone)/hong_bianse_2").gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002E7F RID: 11903 RVA: 0x0019F524 File Offset: 0x0019D724
	public void Set_Special_Door_State_Data(int _Value, string _State)
	{
		bool flag = !this.Special_Door_State_Data.ContainsKey(_Value);
		if (flag)
		{
			this.Special_Door_State_Data.Add(_Value, _State);
		}
		this.Special_Door_State_Data[_Value] = _State;
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x0019F564 File Offset: 0x0019D764
	public string Get_Special_Door_State_Data(int _Value)
	{
		string text = null;
		bool flag = !this.Special_Door_State_Data.ContainsKey(_Value);
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			result = this.Special_Door_State_Data[_Value];
		}
		return result;
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x0019F59C File Offset: 0x0019D79C
	private void SetNewPlayerMsgStat(int nState)
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel > 10;
		if (!flag)
		{
			bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == 2;
			if (flag2)
			{
				bool flag3 = nState == 0 && this.m_nGameState > 0;
				if (flag3)
				{
					this.m_sCarStartMove = Fun.GetServerDate().ToString();
				}
				else
				{
					this.m_sCarStartMove = string.Empty;
				}
			}
			bool flag4 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
			if (flag4)
			{
				bool flag5 = nState == -1 && this.m_nGameState > 0;
				if (flag5)
				{
					this.m_sCarMoveTime = Fun.GetServerDate().ToString();
				}
				else
				{
					this.m_sCarMoveTime = string.Empty;
				}
			}
		}
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x0019F66C File Offset: 0x0019D86C
	private void OnCheckNewPlayerMsg()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel > 10;
		if (!flag)
		{
			bool flag2 = this.m_nMapType == 3;
			if (flag2)
			{
				bool flag3 = this.LeftSlider.value == 0f && this.RightSlider.value == 0f && this.m_nLockTime == 0 && this.m_nGameState > 0;
				if (flag3)
				{
					bool flag4 = base.IsInvoking("QdNewPlayerMsg");
					if (flag4)
					{
						return;
					}
					base.Invoke("QdNewPlayerMsg", 35f);
				}
				else
				{
					this.ClearNewPlayerMsg();
				}
			}
			bool flag5 = this.m_nMapType != 2;
			if (!flag5)
			{
				bool flag6 = this.m_nGameState > 0;
				if (flag6)
				{
					bool flag7 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
					if (flag7)
					{
						this.OnShowCarNewPlayerMsg(this.m_sCarMoveTime);
					}
					bool flag8 = PlayerDataManager.getInstance().playercom.m_nShili == 2;
					if (flag8)
					{
						this.OnShowCarNewPlayerMsg(this.m_sCarStartMove);
					}
				}
				else
				{
					bool flag9 = base.IsInvoking("QdNewPlayerMsg");
					if (flag9)
					{
						this.ClearNewPlayerMsg();
					}
				}
			}
		}
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x0019F7A8 File Offset: 0x0019D9A8
	private void OnShowCarNewPlayerMsg(string sTime)
	{
		int num = 0;
		bool flag = sTime != string.Empty;
		if (flag)
		{
			num = (int)(Fun.GetServerDate() - DateTime.Parse(sTime)).TotalSeconds;
		}
		bool flag2 = num % 35 == 0 && num > 0;
		if (flag2)
		{
			this.QdNewPlayerMsg();
		}
		else
		{
			bool flag3 = base.IsInvoking("QdNewPlayerMsg");
			if (flag3)
			{
				this.ClearNewPlayerMsg();
			}
		}
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x0019F81C File Offset: 0x0019DA1C
	private void QdNewPlayerMsg()
	{
		bool flag = PlayerDataManager.getInstance().HUserData.m_nLevel > 10;
		if (!flag)
		{
			base.CancelInvoke("QdNewPlayerMsg");
			string text = string.Empty;
			int nMapType = this.m_nMapType;
			int num = nMapType;
			if (num != 2)
			{
				if (num == 3)
				{
					text = Language.GetLang(2043, new object[0]);
				}
			}
			else
			{
				bool flag2 = PlayerDataManager.getInstance().playercom.m_nShili == 2;
				if (flag2)
				{
					text = Language.GetLang(2045, new object[0]);
				}
				bool flag3 = PlayerDataManager.getInstance().playercom.m_nShili == 1;
				if (flag3)
				{
					text = Language.GetLang(2044, new object[0]);
				}
			}
			bool flag4 = text != string.Empty && gameuiCon.inst != null;
			if (flag4)
			{
				gameuiCon.inst.mainMsgUI.OnShowNewPlayerMsg(text);
			}
		}
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x0019F910 File Offset: 0x0019DB10
	private void ClearNewPlayerMsg()
	{
		bool flag = base.IsInvoking("QdNewPlayerMsg");
		if (flag)
		{
			base.CancelInvoke("QdNewPlayerMsg");
		}
		bool flag2 = gameuiCon.inst != null;
		if (flag2)
		{
			gameuiCon.inst.mainMsgUI.OnClearMsg();
		}
	}

	// Token: 0x06002E86 RID: 11910 RVA: 0x0019F95C File Offset: 0x0019DB5C
	public override void Close()
	{
		this.m_sCarStartMove = string.Empty;
		this.m_sCarMoveTime = string.Empty;
		base.CancelInvoke("QdNewPlayerMsg");
		base.Close();
	}

	// Token: 0x06002E87 RID: 11911 RVA: 0x0019F988 File Offset: 0x0019DB88
	public void SetGmShow(bool bl)
	{
		bool flag = !bl;
		if (flag)
		{
			bool flag2 = this.m_nMapType == 2 || this.m_nMapType == 3;
			if (flag2)
			{
				base.Open();
			}
		}
		else
		{
			this.Close();
		}
	}

	// Token: 0x040027AB RID: 10155
	private GameObject ZhanDian_Ing;

	// Token: 0x040027AC RID: 10156
	private GameObject JiaShiSlider;

	// Token: 0x040027AD RID: 10157
	private GameObject ZhanDian_State;

	// Token: 0x040027AE RID: 10158
	private Image leftImage;

	// Token: 0x040027AF RID: 10159
	private Image RightImage;

	// Token: 0x040027B0 RID: 10160
	private GameObject Scrollbar;

	// Token: 0x040027B1 RID: 10161
	private Text StateText;

	// Token: 0x040027B2 RID: 10162
	private Image Scrollimg;

	// Token: 0x040027B3 RID: 10163
	private Image ImageA_bk;

	// Token: 0x040027B4 RID: 10164
	private Image Image_A;

	// Token: 0x040027B5 RID: 10165
	private Image ImageB_bk;

	// Token: 0x040027B6 RID: 10166
	private Image Image_B;

	// Token: 0x040027B7 RID: 10167
	private GameObject pnumobj;

	// Token: 0x040027B8 RID: 10168
	private Text Text_pnum;

	// Token: 0x040027B9 RID: 10169
	private Text Text_time;

	// Token: 0x040027BA RID: 10170
	private Text Text_info;

	// Token: 0x040027BB RID: 10171
	private Slider ZhanDian_IngSlider;

	// Token: 0x040027BC RID: 10172
	private Text Text_vinner;

	// Token: 0x040027BD RID: 10173
	public int nTime;

	// Token: 0x040027BE RID: 10174
	private GameObject AllindoObj;

	// Token: 0x040027BF RID: 10175
	public int m_nGameState = -1;

	// Token: 0x040027C0 RID: 10176
	public static ZhanDianCon inst;

	// Token: 0x040027C1 RID: 10177
	private GameObject zhandianPos1;

	// Token: 0x040027C2 RID: 10178
	private GameObject zhandianPos2;

	// Token: 0x040027C3 RID: 10179
	public GameObject EffectObj1;

	// Token: 0x040027C4 RID: 10180
	public GameObject EffectObj2;

	// Token: 0x040027C5 RID: 10181
	private GameObject maplantiao;

	// Token: 0x040027C6 RID: 10182
	private GameObject maphongtiao;

	// Token: 0x040027C7 RID: 10183
	private GameObject jiantou;

	// Token: 0x040027C8 RID: 10184
	private GameObject ShowObj;

	// Token: 0x040027C9 RID: 10185
	private GameObject ZLScrollbar;

	// Token: 0x040027CA RID: 10186
	private Image BkColor;

	// Token: 0x040027CB RID: 10187
	private Image ZLScrollbar_Slider;

	// Token: 0x040027CC RID: 10188
	private GameObject ZLtext;

	// Token: 0x040027CD RID: 10189
	private Text ZDStateText;

	// Token: 0x040027CE RID: 10190
	private GameObject mEffcetDianHong;

	// Token: 0x040027CF RID: 10191
	private GameObject mEffcetDianLan;

	// Token: 0x040027D0 RID: 10192
	private GameObject GuangXiao;

	// Token: 0x040027D1 RID: 10193
	private GameObject ShengliGuangxiao;

	// Token: 0x040027D2 RID: 10194
	private GameObject ShibaiGuangxiao;

	// Token: 0x040027D3 RID: 10195
	private RawImage GameOverImg;

	// Token: 0x040027D4 RID: 10196
	private bool BkState;

	// Token: 0x040027D5 RID: 10197
	private int nBk;

	// Token: 0x040027D6 RID: 10198
	private Tweener m_Lpos;

	// Token: 0x040027D7 RID: 10199
	private Tweener m_Rpos;

	// Token: 0x040027D8 RID: 10200
	private GameObject ZhanDianObj;

	// Token: 0x040027D9 RID: 10201
	private GameObject CartObj;

	// Token: 0x040027DA RID: 10202
	private GameObject RobObj;

	// Token: 0x040027DB RID: 10203
	private Text CartTime;

	// Token: 0x040027DC RID: 10204
	private Text CartInfo;

	// Token: 0x040027DD RID: 10205
	private GameObject SilObj;

	// Token: 0x040027DE RID: 10206
	private Slider CartSlider;

	// Token: 0x040027DF RID: 10207
	private Image CartGai1;

	// Token: 0x040027E0 RID: 10208
	private Image CartGai2;

	// Token: 0x040027E1 RID: 10209
	private Image CartGai3;

	// Token: 0x040027E2 RID: 10210
	private Image CartGai4;

	// Token: 0x040027E3 RID: 10211
	private ArrayList szCartGai = new ArrayList();

	// Token: 0x040027E4 RID: 10212
	private Text CartPlayerNum;

	// Token: 0x040027E5 RID: 10213
	private GameObject CartBk;

	// Token: 0x040027E6 RID: 10214
	private GameObject CartZhengDuo;

	// Token: 0x040027E7 RID: 10215
	private Image carleftImage;

	// Token: 0x040027E8 RID: 10216
	private Image carRightImage;

	// Token: 0x040027E9 RID: 10217
	private Text DistanceText;

	// Token: 0x040027EA RID: 10218
	private GameObject fangxiangxian;

	// Token: 0x040027EB RID: 10219
	private GameObject CarJc;

	// Token: 0x040027EC RID: 10220
	private GameObject CatZd;

	// Token: 0x040027ED RID: 10221
	private GameObject wenzibeijin;

	// Token: 0x040027EE RID: 10222
	private Image changeimg;

	// Token: 0x040027EF RID: 10223
	private Image RobSlider;

	// Token: 0x040027F0 RID: 10224
	private Slider LeftSlider;

	// Token: 0x040027F1 RID: 10225
	private Slider RightSlider;

	// Token: 0x040027F2 RID: 10226
	private Image robleft1;

	// Token: 0x040027F3 RID: 10227
	private Image robleft2;

	// Token: 0x040027F4 RID: 10228
	private Image robleft3;

	// Token: 0x040027F5 RID: 10229
	private Image robright1;

	// Token: 0x040027F6 RID: 10230
	private Image robright2;

	// Token: 0x040027F7 RID: 10231
	private Image robright3;

	// Token: 0x040027F8 RID: 10232
	private GameObject robleftobj;

	// Token: 0x040027F9 RID: 10233
	private GameObject robrightobj;

	// Token: 0x040027FA RID: 10234
	private Text leftnum;

	// Token: 0x040027FB RID: 10235
	private Text rightnum;

	// Token: 0x040027FC RID: 10236
	private GameObject huihejieshu;

	// Token: 0x040027FD RID: 10237
	private Text huihetext;

	// Token: 0x040027FE RID: 10238
	private Text robjindul;

	// Token: 0x040027FF RID: 10239
	private Text robjinbu2;

	// Token: 0x04002800 RID: 10240
	private Text robtime;

	// Token: 0x04002801 RID: 10241
	private Text robinfo;

	// Token: 0x04002802 RID: 10242
	private Text robzhenduo;

	// Token: 0x04002803 RID: 10243
	private GameObject robsuo;

	// Token: 0x04002804 RID: 10244
	private Text robsuotext;

	// Token: 0x04002805 RID: 10245
	private int m_nLockTime;

	// Token: 0x04002806 RID: 10246
	private GameObject ZhanDian_Ing_QD;

	// Token: 0x04002807 RID: 10247
	private Slider ZhanDian_IngSlider_QD;

	// Token: 0x04002808 RID: 10248
	public int m_nRound;

	// Token: 0x04002809 RID: 10249
	public int m_nAttVinnerNum;

	// Token: 0x0400280A RID: 10250
	public int m_nDefVinnerNum;

	// Token: 0x0400280B RID: 10251
	private int m_nNowWin = -1;

	// Token: 0x0400280C RID: 10252
	private ArrayList szResult = new ArrayList();

	// Token: 0x0400280D RID: 10253
	private ArrayList szCarResult = new ArrayList();

	// Token: 0x0400280E RID: 10254
	public int m_nGameOver;

	// Token: 0x0400280F RID: 10255
	private Image Suo;

	// Token: 0x04002810 RID: 10256
	private Slider ZhandianAddSlider;

	// Token: 0x04002811 RID: 10257
	private Text textbaifen1;

	// Token: 0x04002812 RID: 10258
	private Text textbaifen2;

	// Token: 0x04002813 RID: 10259
	private Image imgscr1;

	// Token: 0x04002814 RID: 10260
	private Image imgscr2;

	// Token: 0x04002815 RID: 10261
	private DateTime dd;

	// Token: 0x04002816 RID: 10262
	private Image downjiantou;

	// Token: 0x04002817 RID: 10263
	private int m_nSpeed = 300;

	// Token: 0x04002818 RID: 10264
	private int m_nTickTime;

	// Token: 0x04002819 RID: 10265
	private int m_nIsAddScr;

	// Token: 0x0400281A RID: 10266
	private float m_nSpeedJiaShi = 100f;

	// Token: 0x0400281B RID: 10267
	private int m_nTickTimeJiaShi;

	// Token: 0x0400281C RID: 10268
	private int m_nIsAddScrJiaShi;

	// Token: 0x0400281D RID: 10269
	private int m_nMapType;

	// Token: 0x0400281E RID: 10270
	private Chariot_Entity _Chariot;

	// Token: 0x0400281F RID: 10271
	public int nCarid;

	// Token: 0x04002820 RID: 10272
	private bool SoundOff;

	// Token: 0x04002821 RID: 10273
	public int nIsPlaySound;

	// Token: 0x04002822 RID: 10274
	public int nIsChangeHero;

	// Token: 0x04002823 RID: 10275
	public string sStartChangeTime = string.Empty;

	// Token: 0x04002824 RID: 10276
	public DateTime m_dStartChangeTime = DateTime.MinValue;

	// Token: 0x04002825 RID: 10277
	private int m_nNowZhanLing;

	// Token: 0x04002826 RID: 10278
	private bool bJiaShiCD;

	// Token: 0x04002827 RID: 10279
	private bool bDuoQuCD;

	// Token: 0x04002828 RID: 10280
	private bool bAttCD;

	// Token: 0x04002829 RID: 10281
	private int m_nOpen;

	// Token: 0x0400282A RID: 10282
	private ArrayList EndArrData = new ArrayList();

	// Token: 0x0400282B RID: 10283
	private GameObject DaoJiShiDonghua;

	// Token: 0x0400282C RID: 10284
	private ArrayList DJSDongHua = new ArrayList();

	// Token: 0x0400282D RID: 10285
	private Vector2 vSize;

	// Token: 0x0400282E RID: 10286
	private Vector3 szReturn = new Vector3(-1f, -1f, -1f);

	// Token: 0x0400282F RID: 10287
	private float fScreenW = (float)Screen.width;

	// Token: 0x04002830 RID: 10288
	private float fScreenH = (float)Screen.height;

	// Token: 0x04002831 RID: 10289
	private float fCx;

	// Token: 0x04002832 RID: 10290
	private float fCy;

	// Token: 0x04002833 RID: 10291
	private float fx;

	// Token: 0x04002834 RID: 10292
	private float fy;

	// Token: 0x04002835 RID: 10293
	private float f;

	// Token: 0x04002836 RID: 10294
	private Vector3 position;

	// Token: 0x04002837 RID: 10295
	private float fn_x;

	// Token: 0x04002838 RID: 10296
	private float fn_y;

	// Token: 0x04002839 RID: 10297
	private RectTransform rect;

	// Token: 0x0400283A RID: 10298
	private Quaternion q1;

	// Token: 0x0400283B RID: 10299
	private int nChaoXiang;

	// Token: 0x0400283C RID: 10300
	private Npc_Entity npc;

	// Token: 0x0400283D RID: 10301
	private bool carsound;

	// Token: 0x0400283E RID: 10302
	private Dictionary<int, string> _Special_Door_State_Data;

	// Token: 0x0400283F RID: 10303
	private string m_sCarStartMove = string.Empty;

	// Token: 0x04002840 RID: 10304
	private string m_sCarMoveTime = string.Empty;
}
