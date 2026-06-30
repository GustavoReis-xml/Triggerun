using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000488 RID: 1160
public class UILoading : Window
{
	// Token: 0x06002CC4 RID: 11460 RVA: 0x00189B9C File Offset: 0x00187D9C
	private void Start()
	{
		GameObject gameObject = GameObject.Find("LoadingImage");
		bool flag = gameObject != null;
		if (flag)
		{
			UILoading.uis2 = gameObject.GetComponent<RawImage>();
		}
		bool flag2 = UILoading.uis2 != null;
		if (flag2)
		{
			bool flag3 = WebParam.client_language == "zh";
			if (flag3)
			{
				UILoading.uis2.texture = (Resources.Load("NewProgress/default", typeof(Texture2D)) as Texture2D);
			}
			else
			{
				UILoading.uis2.texture = (Resources.Load("NewProgress/default_baxi", typeof(Texture2D)) as Texture2D);
			}
		}
		GameObject gameObject2 = GameObject.Find("LoadingSlider");
		bool flag4 = gameObject2 != null;
		if (flag4)
		{
			this.mProgressValue = gameObject2.GetComponent<Slider>();
			bool flag5 = this.mProgressValue != null;
			if (flag5)
			{
				this.mProgressValue.value = 0f;
				this.mProgressValue.gameObject.SetActive(false);
			}
		}
		GameObject gameObject3 = GameObject.Find("LoadingSliderBig");
		bool flag6 = gameObject3 != null;
		if (flag6)
		{
			this.mProgressBigValue = gameObject3.GetComponent<Slider>();
			bool flag7 = this.mProgressBigValue != null;
			if (flag7)
			{
				this.mProgressBigValue.value = 0f;
				this.mProgressBigValue.gameObject.SetActive(false);
			}
		}
		Transform transform = null;
		GameObject gameObject4 = GameObject.Find("LoadScnSlider_bx");
		GameObject gameObject5 = GameObject.Find("LoadScnSlider");
		bool flag8 = WebParam.client_language == "baxi";
		if (flag8)
		{
			bool flag9 = gameObject5 != null;
			if (flag9)
			{
				gameObject5.SetActive(false);
			}
			bool flag10 = gameObject4 != null;
			if (flag10)
			{
				transform = gameObject4.transform;
			}
		}
		else
		{
			bool flag11 = gameObject4 != null;
			if (flag11)
			{
				gameObject4.SetActive(false);
			}
			bool flag12 = gameObject5 != null;
			if (flag12)
			{
				transform = gameObject5.transform;
			}
		}
		bool flag13 = transform != null;
		if (flag13)
		{
			this.mScnProgressValue = transform.GetComponent<Slider>();
			bool flag14 = this.mScnProgressValue != null;
			if (flag14)
			{
				this.mScnProgressValue.value = 0f;
				Transform transform2 = this.mScnProgressValue.transform.Find("loadtipstxt");
				bool flag15 = transform2 != null;
				if (flag15)
				{
					this.LoadTipstxt = transform2.GetComponent<Text>();
					bool flag16 = this.LoadTipstxt != null;
					if (flag16)
					{
						this.LoadTipstxt.text = string.Empty;
					}
				}
				Transform transform3 = this.mScnProgressValue.transform.Find("LoadScnjindu");
				bool flag17 = transform3 != null;
				if (flag17)
				{
					this.ScnJinDu = transform3.GetComponent<Text>();
					bool flag18 = this.ScnJinDu != null;
					if (flag18)
					{
						this.ScnJinDu.text = string.Empty;
					}
				}
				Transform transform4 = this.mScnProgressValue.transform.Find("Image");
				bool flag19 = transform4 != null;
				if (flag19)
				{
					this.loadimg = transform4.gameObject;
					bool flag20 = this.loadimg != null;
					if (flag20)
					{
						this.loadimg.SetActive(false);
					}
				}
			}
		}
		GameObject gameObject6 = GameObject.Find("ScnName");
		bool flag21 = gameObject6 != null;
		if (flag21)
		{
			this.ScnName = gameObject6.GetComponent<Text>();
			bool flag22 = this.ScnName != null;
			if (flag22)
			{
				bool flag23 = WebParam.client_language == "baxi";
				if (flag23)
				{
					this.ScnName.font = fUIManager.mNewComic;
				}
				this.ScnName.text = string.Empty;
			}
		}
		GameObject gameObject7 = GameObject.Find("jiazaizhong");
		bool flag24 = gameObject7 != null;
		if (flag24)
		{
			this.jiazaizhong = gameObject7.GetComponent<Text>();
			bool flag25 = this.jiazaizhong != null;
			if (flag25)
			{
				bool flag26 = WebParam.client_language == "baxi";
				if (flag26)
				{
					this.jiazaizhong.font = fUIManager.mNewComic;
					this.jiazaizhong.fontStyle = FontStyle.Normal;
				}
				bool flag27 = WebParam.client_language == "zh";
				if (flag27)
				{
					this.jiazaizhong.text = "加 载 中 ...";
				}
				else
				{
					this.jiazaizhong.text = string.Empty;
				}
			}
		}
		this.Chenmi = GameObject.Find("ChenmiText");
		bool flag28 = this.Chenmi != null;
		if (flag28)
		{
			Text component = this.Chenmi.GetComponent<Text>();
			bool flag29 = component != null;
			if (flag29)
			{
				bool flag30 = WebParam.client_language != "zh";
				if (flag30)
				{
					component.text = string.Empty;
				}
				else
				{
					component.fontSize = 20;
					component.lineSpacing = 1f;
					component.rectTransform.sizeDelta = new Vector2(1000f, 128f);
					component.text = "健康游戏公告\n抵制不良游戏 拒绝盗版游戏 注意自我保护 谨防受骗上当\n 适度游戏益脑 沉迷游戏伤身 合理安排时间 享受健康生活\n本网络游戏适合年满18周岁以上的用户使用；请您确定已如实进行实名注册；为了您的健康，请合理控制游戏时间。";
				}
			}
		}
		this.wenhuabu = GameObject.Find("wenhuabu");
		bool flag31 = this.wenhuabu != null;
		if (flag31)
		{
			Transform transform5 = this.wenhuabu.transform.Find("Text");
			Transform transform6 = this.wenhuabu.transform.Find("Text (1)");
			Text text = (transform5 != null) ? transform5.GetComponent<Text>() : null;
			Text text2 = (transform6 != null) ? transform6.GetComponent<Text>() : null;
			bool flag32 = text != null && text2 != null;
			if (flag32)
			{
				bool flag33 = WebParam.client_language == "zh";
				if (flag33)
				{
					text2.text = "新出网证（粤）字010号  粤网文【2017】6138-1456号  文网游备字[2017]C-CSG 0014号  批准文号：新广出审[2017]4818号  出版物号：ISBN 978-7-7979-8161-3  著作权人：西安火神网络科技有限公司";
					text2.fontSize = 20;
					text.fontSize = 20;
					text.horizontalOverflow = HorizontalWrapMode.Overflow;
					text.text = "全国文化 market 统一举报电话：12318  著作权登记号：2016SR311980  出版单位：天津电子出版社有限公司";
				}
				else
				{
					text2.text = string.Empty;
					text.text = string.Empty;
				}
			}
		}
		this.ScnDesc = GameObject.Find("ScnDesc");
		GameObject gameObject8 = GameObject.Find("ModelRawImage");
		bool flag34 = gameObject8 != null;
		if (flag34)
		{
			this.ModelRawImage = gameObject8.GetComponent<RawImage>();
		}
		GameObject gameObject9 = GameObject.Find("ModelText");
		bool flag35 = gameObject9 != null;
		if (flag35)
		{
			this.ModelText = gameObject9.GetComponent<Text>();
			bool flag36 = this.ModelText != null && WebParam.client_language == "baxi";
			if (flag36)
			{
				this.ModelText.font = fUIManager.mNewComic;
			}
		}
		GameObject gameObject10 = GameObject.Find("ModeldescText");
		bool flag37 = gameObject10 != null;
		if (flag37)
		{
			this.ModeldescText = gameObject10.GetComponent<Text>();
		}
		bool flag38 = this.ScnDesc != null;
		if (flag38)
		{
			this.ScnDesc.SetActive(false);
		}
		this.ChaoShi = GameObject.Find("chaoshitishi");
		bool flag39 = this.ChaoShi != null;
		if (flag39)
		{
			Transform transform7 = this.ChaoShi.transform.Find("Image/Text");
			bool flag40 = transform7 != null;
			if (flag40)
			{
				this.chaoShiTipTxt = transform7.GetComponent<Text>();
				bool flag41 = this.chaoShiTipTxt != null;
				if (flag41)
				{
					this.chaoShiTipTxt.horizontalOverflow = HorizontalWrapMode.Overflow;
					bool flag42 = WebParam.client_language == "zh";
					if (flag42)
					{
						this.chaoShiTipTxt.text = "由于您当前网络环境问题，导致下载时间过长，请耐心等待";
					}
					else
					{
						this.chaoShiTipTxt.text = "Devido a sua conexão, isso levará um tempo razoável para download, aguarde por favor";
					}
				}
			}
			this.ChaoShi.SetActive(false);
		}
		RectTransform component2 = base.GetComponent<RectTransform>();
		bool flag43 = component2 != null;
		if (flag43)
		{
			component2.sizeDelta = new Vector2(0f, 0f);
			component2.anchoredPosition = new Vector2(0f, 0f);
		}
		UIDepth uidepth = base.gameObject.AddComponent<UIDepth>();
		bool flag44 = uidepth != null;
		if (flag44)
		{
			uidepth.SetUIDepth(20, true);
		}
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x0018A3DC File Offset: 0x001885DC
	private void SetJiaZai()
	{
		bool flag = this.jiazaizhong == null;
		if (!flag)
		{
			bool flag2 = this.nDianShu == 0;
			if (flag2)
			{
				bool flag3 = WebParam.client_language == "zh";
				if (flag3)
				{
					this.jiazaizhong.text = "加 载 中";
				}
				else
				{
					this.jiazaizhong.text = string.Empty;
				}
			}
			bool flag4 = this.nDianShu == 1;
			if (flag4)
			{
				bool flag5 = WebParam.client_language == "zh";
				if (flag5)
				{
					this.jiazaizhong.text = "加 载 中 .";
				}
				else
				{
					this.jiazaizhong.text = string.Empty;
				}
			}
			bool flag6 = this.nDianShu == 2;
			if (flag6)
			{
				bool flag7 = WebParam.client_language == "zh";
				if (flag7)
				{
					this.jiazaizhong.text = "加 载 中 ..";
				}
				else
				{
					this.jiazaizhong.text = string.Empty;
				}
			}
			bool flag8 = this.nDianShu == 3;
			if (flag8)
			{
				bool flag9 = WebParam.client_language == "zh";
				if (flag9)
				{
					this.jiazaizhong.text = "加 载 中 ...";
				}
				else
				{
					this.jiazaizhong.text = string.Empty;
				}
				this.nDianShu = 0;
			}
			this.nDianShu++;
		}
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x0018A548 File Offset: 0x00188748
	public void SetModelArr(string sname)
	{
		this.Modelname = sname;
		bool flag = sname == string.Empty;
		if (flag)
		{
			this.fScnFloatVlaue += this.fScnVlaue;
		}
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x0018A584 File Offset: 0x00188784
	private void Update()
	{
		int num = Environment.TickCount - this.m_nTickTime;
		bool flag = num / 300 > 1;
		if (flag)
		{
			this.SetJiaZai();
			this.m_nTickTime = Environment.TickCount;
		}
		bool flag2 = this.Modelname != string.Empty && GameRoot.mHeroAniManager != null;
		if (flag2)
		{
			this.fScnVlaue = GameRoot.mHeroAniManager.wwwp(this.Modelname) / 4f / 5f;
			this.setProgressValue(this.fScnVlaue);
		}
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x0018A618 File Offset: 0x00188818
	private void LoadIconOk(WWW www, object customParam)
	{
		bool flag = www != null && www.error == null && !(UILoading.uis2 == null);
		if (flag)
		{
			bool flag2 = UILoading.uis2.texture == null;
			if (flag2)
			{
				UILoading.uis2.texture = new Texture2D(4, 4);
			}
			www.LoadImageIntoTexture((Texture2D)UILoading.uis2.texture);
		}
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x0018A688 File Offset: 0x00188888
	public void SetRawImg(string smapid, string sScnName)
	{
		bool flag = smapid == "0";
		if (flag)
		{
			smapid = "5000";
		}
		bool flag2 = this.sNowID == smapid;
		if (!flag2)
		{
			this.sNowID = smapid;
			FbXmlData fbXmlData = null;
			bool flag3 = sScnName != null;
			if (flag3)
			{
				base.CancelInvoke("SetShowChaoShi");
				this.InitData();
				bool flag4 = this.Chenmi != null;
				if (flag4)
				{
					this.Chenmi.SetActive(false);
				}
				bool flag5 = this.wenhuabu != null;
				if (flag5)
				{
					this.wenhuabu.SetActive(false);
				}
				bool flag6 = this.ChaoShi != null;
				if (flag6)
				{
					this.ChaoShi.SetActive(false);
				}
				this.IsLoad = false;
				bool flag7 = fGameManager.mloadConfig != null && fGameManager.mloadConfig.FbData != null && fGameManager.mloadConfig.FbData.ContainsKey(int.Parse(this.sNowID));
				if (flag7)
				{
					fbXmlData = fGameManager.mloadConfig.FbData[int.Parse(this.sNowID)];
					bool flag8 = fbXmlData != null;
					if (flag8)
					{
						bool flag9 = this.ScnName != null;
						if (flag9)
						{
							this.ScnName.text = fbXmlData.sName;
						}
						this.SetLoadTips(fbXmlData.nType);
						bool flag10 = fbXmlData.scnmodelimage != "0";
						if (flag10)
						{
							bool flag11 = this.ModelRawImage != null;
							if (flag11)
							{
								fUIManager.GetInstance().GetIconAsset("scndesc/" + fbXmlData.scnmodelimage + ".txt", this.ModelRawImage);
							}
							bool flag12 = this.ModelText != null;
							if (flag12)
							{
								this.ModelText.text = fbXmlData.scnmodelname;
							}
							bool flag13 = this.ModeldescText != null;
							if (flag13)
							{
								this.ModeldescText.text = fbXmlData.scnmodeldesc;
							}
							bool flag14 = this.ScnDesc != null;
							if (flag14)
							{
								this.ScnDesc.SetActive(true);
							}
						}
						else
						{
							bool flag15 = this.ScnDesc != null;
							if (flag15)
							{
								this.ScnDesc.SetActive(false);
							}
						}
					}
				}
			}
			else
			{
				this.InitData();
				bool flag16 = this.ScnDesc != null;
				if (flag16)
				{
					this.ScnDesc.SetActive(false);
				}
				bool flag17 = this.loadimg != null;
				if (flag17)
				{
					this.loadimg.SetActive(false);
				}
				bool flag18 = this.LoadTipstxt != null;
				if (flag18)
				{
					this.LoadTipstxt.text = string.Empty;
				}
			}
			bool flag19 = fbXmlData != null;
			if (flag19)
			{
				smapid = fbXmlData.nloadmap.ToString();
			}
			bool flag20 = UILoading.uis2 != null;
			if (flag20)
			{
				fUIManager.GetInstance().GetIconAsset("selectherobg/" + smapid + ".txt", UILoading.uis2);
			}
		}
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x0018A984 File Offset: 0x00188B84
	public void setLoadBack()
	{
		bool flag = UILoading.uibg == null || UILoading.uis2 == null;
		if (!flag)
		{
			int count = UILoading.uibg.Count;
			bool flag2 = count == 0;
			if (flag2)
			{
				Debug.LogError(count);
			}
			else
			{
				bool flag3 = count > 0;
				if (flag3)
				{
					int index = UnityEngine.Random.Range(0, count);
					UILoading.uis2.texture = UILoading.uibg[index];
				}
			}
		}
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x0018A9FC File Offset: 0x00188BFC
	private void SetShowChaoShi()
	{
		base.CancelInvoke("SetShowChaoShi");
		bool flag = this.ChaoShi != null;
		if (flag)
		{
			this.ChaoShi.SetActive(true);
		}
	}

	// Token: 0x06002CCC RID: 11468 RVA: 0x0018AA38 File Offset: 0x00188C38
	public void SetMainPro(float fpro)
	{
		bool flag = this.ScnJinDu != null;
		if (flag)
		{
			this.ScnJinDu.text = (fpro * 100f).ToString("0") + "%";
		}
		bool flag2 = this.mScnProgressValue != null;
		if (flag2)
		{
			this.mScnProgressValue.value = fpro;
		}
	}

	// Token: 0x06002CCD RID: 11469 RVA: 0x0018AAA4 File Offset: 0x00188CA4
	public void setProgressValue(float v)
	{
		bool flag = !this.IsLoad;
		if (flag)
		{
			base.Invoke("SetShowChaoShi", 20f);
			this.IsLoad = true;
		}
		bool flag2 = this.ScnJinDu != null;
		if (flag2)
		{
			this.ScnJinDu.text = ((this.fScnFloatVlaue + v) * 100f).ToString("0") + "%";
		}
		bool flag3 = this.mScnProgressValue != null;
		if (flag3)
		{
			this.mScnProgressValue.value = this.fScnFloatVlaue + v;
		}
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x0018AB44 File Offset: 0x00188D44
	public void setTuiSongValue(float v)
	{
		bool flag = this.fScnFloatVlaue != 0.2f;
		if (flag)
		{
			this.fScnFloatVlaue = 0.2f;
		}
		float num = this.fScnFloatVlaue + v * 80f / 100f;
		bool flag2 = this.ScnJinDu != null;
		if (flag2)
		{
			this.ScnJinDu.text = (num * 100f).ToString("0") + "%";
		}
		bool flag3 = this.mScnProgressValue != null;
		if (flag3)
		{
			this.mScnProgressValue.value = num;
		}
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x0018ABE5 File Offset: 0x00188DE5
	public void LoadScn()
	{
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x0018ABE8 File Offset: 0x00188DE8
	public void SetScnName(string sScnName)
	{
		bool flag = this.ScnName != null;
		if (flag)
		{
			this.ScnName.text = sScnName;
		}
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x0018AC18 File Offset: 0x00188E18
	public void SetLoadTips(int FbType)
	{
		bool flag = this.LoadTipstxt == null || fGameManager.mloadConfig == null || fGameManager.mloadConfig.AllLoadTipstxtData == null;
		if (!flag)
		{
			List<int> list = new List<int>();
			this.LoadTipstxt.text = string.Empty;
			foreach (int key in fGameManager.mloadConfig.AllLoadTipstxtData.Keys)
			{
				LoadTipstxtData loadTipstxtData = fGameManager.mloadConfig.AllLoadTipstxtData[key];
				bool flag2 = loadTipstxtData != null && loadTipstxtData.m_nMaptype == FbType;
				if (flag2)
				{
					list.Add(loadTipstxtData.m_nId);
				}
			}
			int num = -1;
			string text = string.Empty;
			bool flag3 = list.Count > 0;
			if (flag3)
			{
				num = UnityEngine.Random.Range(0, list.Count);
			}
			bool flag4 = num >= 0;
			if (flag4)
			{
				int key2 = list[num];
				LoadTipstxtData loadTipstxtData2 = fGameManager.mloadConfig.AllLoadTipstxtData[key2];
				bool flag5 = loadTipstxtData2 != null;
				if (flag5)
				{
					text = loadTipstxtData2.m_sTipstxt;
					bool flag6 = this.loadimg != null;
					if (flag6)
					{
						this.loadimg.SetActive(true);
					}
					this.LoadTipstxt.text = text;
				}
			}
		}
	}

	// Token: 0x06002CD2 RID: 11474 RVA: 0x0018AD90 File Offset: 0x00188F90
	private void InitData()
	{
		this.fScnFloatVlaue = 0f;
		this.fScnVlaue = 0f;
		bool flag = this.jiazaizhong != null;
		if (flag)
		{
			bool flag2 = WebParam.client_language == "zh";
			if (flag2)
			{
				this.jiazaizhong.text = "加 载 中 ...";
			}
			else
			{
				this.jiazaizhong.text = string.Empty;
			}
		}
		this.nDianShu = 0;
		bool flag3 = this.mScnProgressValue != null;
		if (flag3)
		{
			this.mScnProgressValue.value = 0f;
		}
		bool flag4 = this.ScnJinDu != null;
		if (flag4)
		{
			this.ScnJinDu.text = string.Empty;
		}
	}

	// Token: 0x06002CD3 RID: 11475 RVA: 0x0018AE50 File Offset: 0x00189050
	public override void Reset()
	{
		base.Reset();
		base.CancelInvoke("SetShowChaoShi");
		bool flag = this.ChaoShi != null;
		if (flag)
		{
			this.ChaoShi.SetActive(false);
		}
		this.IsLoad = false;
		this.InitData();
	}

	// Token: 0x040025F1 RID: 9713
	private static List<Texture2D> uibg = new List<Texture2D>();

	// Token: 0x040025F2 RID: 9714
	private static RawImage uis2;

	// Token: 0x040025F3 RID: 9715
	private Slider mProgressValue;

	// Token: 0x040025F4 RID: 9716
	private Slider mProgressBigValue;

	// Token: 0x040025F5 RID: 9717
	private Slider mScnProgressValue;

	// Token: 0x040025F6 RID: 9718
	private string sNowID;

	// Token: 0x040025F7 RID: 9719
	private Text ScnName;

	// Token: 0x040025F8 RID: 9720
	private Text ScnJinDu;

	// Token: 0x040025F9 RID: 9721
	public string sUIname = string.Empty;

	// Token: 0x040025FA RID: 9722
	private string Modelname = string.Empty;

	// Token: 0x040025FB RID: 9723
	private float fScnFloatVlaue;

	// Token: 0x040025FC RID: 9724
	private float fScnVlaue;

	// Token: 0x040025FD RID: 9725
	private Text jiazaizhong;

	// Token: 0x040025FE RID: 9726
	private int nDianShu = 3;

	// Token: 0x040025FF RID: 9727
	private GameObject loadimg;

	// Token: 0x04002600 RID: 9728
	private Text LoadTipstxt;

	// Token: 0x04002601 RID: 9729
	private GameObject Chenmi;

	// Token: 0x04002602 RID: 9730
	private GameObject wenhuabu;

	// Token: 0x04002603 RID: 9731
	private GameObject ScnDesc;

	// Token: 0x04002604 RID: 9732
	private RawImage ModelRawImage;

	// Token: 0x04002605 RID: 9733
	private Text ModelText;

	// Token: 0x04002606 RID: 9734
	private Text ModeldescText;

	// Token: 0x04002607 RID: 9735
	private GameObject ChaoShi;

	// Token: 0x04002608 RID: 9736
	private Text chaoShiTipTxt;

	// Token: 0x04002609 RID: 9737
	private bool IsLoad;

	// Token: 0x0400260A RID: 9738
	private int m_nTickTime;
}
