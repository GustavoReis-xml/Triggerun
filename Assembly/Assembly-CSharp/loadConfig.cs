using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class loadConfig : MonoBehaviour
{
	// Token: 0x0600180B RID: 6155 RVA: 0x000DF503 File Offset: 0x000DD703
	private void Start()
	{
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000DF506 File Offset: 0x000DD706
	public void LoadConfig()
	{
		fGameManager.m_nJoinState = 3;
		this.LoadFileDir();
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000DF518 File Offset: 0x000DD718
	private void LoadFileDir()
	{
		string url = "files_dir.xml";
		GameRoot.mGlobalAssetManager.LoadDownRequest(url, new fResourceManager.DownFinishDelegate(this.DownloadFileDirOK), "files_dir", "files_dir.xml");
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x000DF550 File Offset: 0x000DD750
	private void DownloadFileDirOK(WWW www, object sParam)
	{
		fGameManager.m_nJoinState = 4;
		fGameManager.GetInstance().SetUserLoadStep_log("_load_2_files_dir.xml");
		bool flag = www.error == null;
		if (flag)
		{
			this.doLoadVersion(www);
		}
		else
		{
			WebParam.client_cache = "0";
		}
		this.LoadLang(this.nVersion);
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x000DF5A8 File Offset: 0x000DD7A8
	public void LoadLang(int nVer)
	{
		fGameManager.m_nJoinState = 6;
		string text = "Lang/" + WebParam.client_language + ".xml";
		GameRoot.mGlobalAssetManager.LoadDownRequest(text, new fResourceManager.DownFinishDelegate(this.DownloadLangOK), "Lang", text);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000DF5F0 File Offset: 0x000DD7F0
	private void DownloadLangOK(WWW www, object sParam)
	{
		bool flag = www.error == null;
		if (flag)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text = www.text;
			text = Regex.Replace(text, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
			xmlDocument.LoadXml(text);
			XmlNodeList childNodes = xmlDocument.SelectSingleNode("/root").ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlElement xmlElement = (XmlElement)obj;
				bool flag2 = !this.LangObj.ContainsKey(int.Parse(xmlElement.GetAttribute("id")));
				if (flag2)
				{
					this.LangObj.Add(int.Parse(xmlElement.GetAttribute("id")), xmlElement.GetAttribute("name").Replace("\\n", "\n"));
				}
			}
		}
		else
		{
			Debug.LogError("加载语言包失败." + WebParam.client_language);
		}
		fGameManager.m_nJoinState = 8;
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000DF714 File Offset: 0x000DD914
	private void LoadFileDirOK()
	{
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x000DF718 File Offset: 0x000DD918
	public void LoadConfigXml()
	{
		bool bLoadDataCmpl = this.m_bLoadDataCmpl;
		if (bLoadDataCmpl)
		{
			this.DownLoadPreResOK();
		}
		else
		{
			bool flag = this.m_bLoadData && !this.m_bLoadDataCmpl;
			if (flag)
			{
				fGameManager.mloadConfig.LoadConfigXml();
			}
			else
			{
				this.m_bLoadData = true;
				fGameManager.m_nJoinState = 10;
				bool flag2 = WebParam.client_loadtype == "2";
				if (flag2)
				{
					this.LoadDataXML();
				}
				bool flag3 = WebParam.client_loadtype == "1";
				if (flag3)
				{
					this.doConfigFile();
				}
			}
		}
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000DF7A8 File Offset: 0x000DD9A8
	private void LoadDataXML()
	{
		string url = "data.assetbundle";
		GameRoot.mGlobalAssetManager.LoadDownRequest(url, new fResourceManager.DownFinishDelegate(this.DownloadDataXmlOK), "data.assetbundle", "data.assetbundle");
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x000DF7E0 File Offset: 0x000DD9E0
	private void DownloadDataXmlOK(WWW www, object sParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			fGameManager.m_nJoinState = 11;
			bool flag2 = www.error != null;
			if (flag2)
			{
				WebParam.client_loadtype = "1";
			}
			else
			{
				UnityEngine.Object[] array = www.assetBundle.LoadAllAssets();
				UnityEngine.Object[] array2 = array;
				foreach (UnityEngine.Object @object in array2)
				{
					this.m_szData[@object.name + ".xml"] = @object;
				}
			}
			this.doConfigFile();
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000DF87C File Offset: 0x000DDA7C
	private void doGBFile()
	{
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000DF880 File Offset: 0x000DDA80
	private void doShieldFile(int nVer)
	{
		string url = "shieldLab.assetbundle";
		GameRoot.mGlobalAssetManager.RequestWithDesVesion(url, new fResourceManager.DownFinishDelegate(this.DownFinishPrefab), "shieldLab", nVer);
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000DF8B4 File Offset: 0x000DDAB4
	private void doRandomNameFile(int nVer)
	{
		string url = "randomlab.xml";
		GameRoot.mGlobalAssetManager.RequestWithDesVesion(url, new fResourceManager.DownFinishDelegate(this.DownFinishPrefab), "randomlab", nVer);
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x000DF8E8 File Offset: 0x000DDAE8
	private void doConfigFile()
	{
		bool flag = WebParam.client_loadtype == "2";
		if (flag)
		{
			bool flag2 = this.m_szData.ContainsKey("config.xml");
			if (flag2)
			{
				this.doLoadConfig(this.m_szData["config.xml"].ToString());
			}
			else
			{
				Debug.LogError("doConfigFile:data.assetbundle not find config.xml");
			}
		}
		else
		{
			string url = WebParam.client_language + "/config.xml";
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.DownFinishPrefab), "config");
		}
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x000DF978 File Offset: 0x000DDB78
	public void DownFinishPrefab(WWW wwwParam, object customParam)
	{
		bool flag = customParam.ToString() == "config";
		if (flag)
		{
			this.doLoadConfig(wwwParam.text);
		}
		else
		{
			bool flag2 = customParam.ToString() == "gb2312";
			if (flag2)
			{
				this.doLoadGb2312(wwwParam);
			}
			else
			{
				bool flag3 = customParam.ToString() == "shieldLab";
				if (flag3)
				{
					fGameManager.GetInstance().SetUserLoadStep_log("_load_3_shieldLab");
					this.doLoadShieldLab(wwwParam);
				}
				else
				{
					bool flag4 = customParam.ToString() == "randomlab";
					if (flag4)
					{
						fGameManager.GetInstance().SetUserLoadStep_log("_load_3_randomlab");
						this.doLoadRandomStr(wwwParam.text);
					}
					else
					{
						Fun.Log("not find---------------------回调:" + ((customParam != null) ? customParam.ToString() : null), null);
					}
				}
			}
		}
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000DFA58 File Offset: 0x000DDC58
	private void doLoadGb2312(WWW gbobj)
	{
		this.isLoadGBEx = true;
		byte[] bytes = gbobj.bytes;
		bool flag = DBCSEncoding.mMemfs == null;
		if (flag)
		{
			DBCSEncoding.mMemfs = new MemoryStream(bytes);
		}
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000DFA90 File Offset: 0x000DDC90
	public void doLoadShieldLab(WWW shieldobj)
	{
		shieldobj.assetBundle.LoadAllAssets();
		string text = shieldobj.assetBundle.mainAsset.ToString();
		this.badCharArr = text.Split(new char[]
		{
			'、'
		});
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000DFAD8 File Offset: 0x000DDCD8
	public void doLoadRandomStr(string stext)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(stext);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.GetAttribute("id").ToString() == "0";
			if (flag)
			{
				this.RandomNameArr_first = xmlElement.Attributes["str"].Value.ToString().Split(new char[]
				{
					','
				});
			}
			else
			{
				bool flag2 = xmlElement.GetAttribute("id").ToString() == "1";
				if (flag2)
				{
					this.RandomNameArr_second = xmlElement.Attributes["str"].Value.ToString().Split(new char[]
					{
						','
					});
				}
				else
				{
					bool flag3 = xmlElement.GetAttribute("id").ToString() == "3";
					if (flag3)
					{
						this.RandomNameArr_special = xmlElement.Attributes["str"].Value.ToString().Split(new char[]
						{
							','
						});
					}
				}
			}
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000DFC64 File Offset: 0x000DDE64
	public void doLoadfilterchat(WWW shieldobj)
	{
		shieldobj.assetBundle.LoadAllAssets();
		string text = shieldobj.assetBundle.mainAsset.ToString();
		this.filterChatArr = text.Split(new char[]
		{
			'、'
		});
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x000DFCAC File Offset: 0x000DDEAC
	private void doLoadServerInfo(WWW xmlobj)
	{
		int num = 0;
		string text = string.Empty;
		int g_PORT = 0;
		XmlDocument xmlDocument = new XmlDocument();
		string text2 = xmlobj.text;
		text2 = Regex.Replace(text2, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(text2);
		XmlNodeList childNodes = xmlDocument.SelectSingleNode("root/server").ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			foreach (object obj2 in xmlElement.ChildNodes)
			{
				XmlElement xmlElement2 = (XmlElement)obj2;
				string name = xmlElement2.Name;
				string a = name;
				if (!(a == "id"))
				{
					if (!(a == "ip"))
					{
						if (a == "port")
						{
							g_PORT = int.Parse(xmlElement2.InnerText);
						}
					}
					else
					{
						text = xmlElement2.InnerText;
					}
				}
				else
				{
					num = int.Parse(xmlElement2.InnerText);
				}
			}
			bool flag = num == 1;
			if (flag)
			{
				Debug.Log("XML--ip:" + text + "|port:" + g_PORT.ToString());
				SysConfig.g_IP = text;
				SysConfig.g_PORT = g_PORT;
				break;
			}
		}
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000DFE3C File Offset: 0x000DE03C
	private void doLoadConfig(string objTxt)
	{
		XmlDocument xmlDocument = new XmlDocument();
		objTxt = Regex.Replace(objTxt, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(objTxt);
		XmlNodeList childNodes = xmlDocument.SelectSingleNode("/data").ChildNodes;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name != "resource";
			if (flag)
			{
				dictionary.Add(xmlElement.Name, xmlElement.InnerText);
			}
			else
			{
				dictionary.Add(xmlElement.Name, xmlElement.InnerXml);
			}
		}
		SysConfig.g_version = dictionary["version"].ToString();
		string downloadResXML = dictionary["resource"].ToString();
		this.doDownloadRes(downloadResXML);
		fUIManager.SetProgreessShow(true);
		dictionary.Clear();
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000DFF54 File Offset: 0x000DE154
	private string GetVersion(string sPath, string sVersion)
	{
		return sVersion;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000DFF68 File Offset: 0x000DE168
	private void doLoadVersion(WWW xmlobj)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string text = xmlobj.text;
		text = Regex.Replace(text, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(text);
		XmlNodeList childNodes = xmlDocument.SelectSingleNode("/data").ChildNodes;
		Asset asset = default(Asset);
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = !SysConfig.FileVersion.ContainsKey(xmlElement.GetAttribute("path"));
			if (flag)
			{
				asset.m_sFile = xmlElement.GetAttribute("path");
				asset.m_sCrc = xmlElement.GetAttribute("crc");
				asset.m_sVersion = xmlElement.GetAttribute("version");
				SysConfig.FileVersion.Add(asset.m_sFile, asset);
			}
		}
		fGameManager.m_nJoinState = 5;
		fGameManager.GetInstance().mGameRoot.LoadFirOk();
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000E008C File Offset: 0x000DE28C
	private void doDownloadRes(string DownloadResXML)
	{
		this.m_nAllResNum = 0;
		XmlDocument xmlDocument = new XmlDocument();
		DownloadResXML = "<res>" + DownloadResXML + "</res>";
		xmlDocument.LoadXml(DownloadResXML);
		XmlNodeList childNodes = xmlDocument.SelectSingleNode("/res").ChildNodes;
		string empty = string.Empty;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			string name = xmlElement.Name;
			string a = name;
			if (!(a == "main"))
			{
				if (!(a == "data"))
				{
					if (a == "res")
					{
						foreach (object obj2 in xmlElement.ChildNodes)
						{
							XmlElement xmlElement2 = (XmlElement)obj2;
							this.Reslist.Add(new string[]
							{
								xmlElement2.InnerText,
								xmlElement2.GetAttribute("title")
							});
						}
					}
				}
				else
				{
					foreach (object obj3 in xmlElement.ChildNodes)
					{
						XmlElement xmlElement3 = (XmlElement)obj3;
						this.Asslist.Add(new string[]
						{
							xmlElement3.InnerText,
							xmlElement3.GetAttribute("title")
						});
					}
				}
			}
			else
			{
				this.Asslist.Add(new string[]
				{
					xmlElement.InnerText,
					Language.GetLang(5002, new object[0])
				});
			}
		}
		this.m_nAllResNum = this.Asslist.Count + this.Reslist.Count;
		this.doWWWResFile();
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000E02DC File Offset: 0x000DE4DC
	private void doWWWResFile()
	{
		string[] array = (string[])this.Asslist[this.icont];
		string text = array[0].ToString();
		bool flag = WebParam.client_loadtype == "2";
		if (flag)
		{
			text = text.Replace(WebParam.client_language + "/", string.Empty);
			bool flag2 = this.m_szData.ContainsKey(text);
			if (flag2)
			{
				this.LoadFunOK(this.m_szData[text].ToString(), text);
			}
			else
			{
				Debug.LogError("doWWWResFile: data.assetbundle not find " + text + string.Empty);
				this.LoadFunOK(string.Empty, text);
			}
		}
		else
		{
			string text2 = string.Empty + text + string.Empty;
			this.seturl(text2);
			GameRoot.LoadAsset(text2, new fResourceManager.DownFinishDelegate(this.LoadFun), text);
		}
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000E03BD File Offset: 0x000DE5BD
	private void seturl(string surl)
	{
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000E03C0 File Offset: 0x000DE5C0
	private void LoadFun(WWW wwwParam, object customParam)
	{
		string text = customParam.ToString();
		this.LoadFunOK(wwwParam.text, text.Replace(WebParam.client_language + "/", string.Empty));
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000E03FC File Offset: 0x000DE5FC
	private void LoadFunOK(string sText, string path)
	{
		this.icont++;
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		uint num = PrivateImplementationDetails.ComputeStringHash(path);
		if (num <= 1796281312U)
		{
			if (num <= 721622268U)
			{
				if (num <= 507353382U)
				{
					if (num <= 259481241U)
					{
						if (num <= 160903519U)
						{
							if (num != 64485686U)
							{
								if (num == 160903519U)
								{
									if (path == "Hudong.xml")
									{
										this.AllHuDongXML(sText);
									}
								}
							}
							else if (path == "act_namatchreward.xml")
							{
								this.LoadNaMatchRewardData(sText);
							}
						}
						else if (num != 192211551U)
						{
							if (num != 202535846U)
							{
								if (num == 259481241U)
								{
									if (path == "Npc_Derive.xml")
									{
										this.LoadNpcDeriveXmlData(sText);
									}
								}
							}
							else if (path == "taskchapter.xml")
							{
								this.TaskChapterDataXml(sText);
							}
						}
						else if (path == "act_discountitem.xml")
						{
							this.LoadDiscountItemData(sText);
						}
					}
					else if (num <= 373316515U)
					{
						if (num != 324374417U)
						{
							if (num == 373316515U)
							{
								if (path == "NaLianXureward.xml")
								{
									this.LoadNaLianXuData(sText);
								}
							}
						}
						else if (path == "iteminfo.xml")
						{
							this.ItemDataXml(sText);
						}
					}
					else if (num != 414984562U)
					{
						if (num != 485916684U)
						{
							if (num == 507353382U)
							{
								if (path == "shopdata.xml")
								{
									this.ShopDataXml(sText);
								}
							}
						}
						else if (path == "act_bogoact.xml")
						{
							this.LoadBOGOActData(sText);
						}
					}
					else if (path == "act_weeklogin.xml")
					{
						this.LoadWeekLoginData(sText);
					}
				}
				else if (num <= 616154273U)
				{
					if (num <= 551501478U)
					{
						if (num != 530240564U)
						{
							if (num == 551501478U)
							{
								if (path == "exchangecenter.xml")
								{
									this.LoadExchangeData(sText);
								}
							}
						}
						else if (path == "act_rechargezuan.xml")
						{
							this.LoadRechargeZuanData(sText);
						}
					}
					else if (num != 558965798U)
					{
						if (num != 614068623U)
						{
							if (num == 616154273U)
							{
								if (path == "preload.xml")
								{
									this.ReadLoadPreLoad(sText);
								}
							}
						}
						else if (path == "act_multibogo.xml")
						{
							this.LoadMultiBOGOData(sText);
						}
					}
					else if (path == "herounlock.xml")
					{
						this.ReadHeroUnlockXml(sText);
					}
				}
				else if (num <= 633647552U)
				{
					if (num != 633256389U)
					{
						if (num == 633647552U)
						{
							if (path == "fbset.xml")
							{
								this.ReadFbXml(sText);
							}
						}
					}
					else if (path == "NewPlayerLearn.xml")
					{
						this.LoadLearnXml(sText);
					}
				}
				else if (num != 634475357U)
				{
					if (num != 678900206U)
					{
						if (num == 721622268U)
						{
							if (path == "act_winteract.xml")
							{
								this.LoadWinterActData(sText);
							}
						}
					}
					else if (path == "weapon.xml")
					{
						this.LoadWeaponXmlData(sText);
					}
				}
				else if (path == "pvedata.xml")
				{
					this.LoadPveData(sText);
				}
			}
			else if (num <= 1238728966U)
			{
				if (num <= 1045739760U)
				{
					if (num <= 870897537U)
					{
						if (num != 784872280U)
						{
							if (num == 870897537U)
							{
								if (path == "guildfuli.xml")
								{
									this.LoadGuildFuliData(sText);
								}
							}
						}
						else if (path == "libaoreward.xml")
						{
							this.LoadLiBaoData(sText);
						}
					}
					else if (num != 899849517U)
					{
						if (num != 987321218U)
						{
							if (num == 1045739760U)
							{
								if (path == "Elevator.xml")
								{
									this.Load_Elevator_Xml_Data(sText);
								}
							}
						}
						else if (path == "act_herolimitbuy.xml")
						{
							this.LoadHeroLimitBuyData(sText);
						}
					}
					else if (path == "heroskillinfo.xml")
					{
						this.ReadHeroSkillInfo(sText);
					}
				}
				else if (num <= 1125492206U)
				{
					if (num != 1096916605U)
					{
						if (num == 1125492206U)
						{
							if (path == "qianghuadata.xml")
							{
								this.LoadQiangHuaData(sText);
							}
						}
					}
					else if (path == "act_skyeveryday.xml")
					{
						this.LoadSkyEveryDayData(sText);
					}
				}
				else if (num != 1139047267U)
				{
					if (num != 1159148175U)
					{
						if (num == 1238728966U)
						{
							if (path == "heroparts.xml")
							{
								this.LoadHeroPartsData(sText);
							}
						}
					}
					else if (path == "hint.xml")
					{
						this.HintDataXML(sText);
					}
				}
				else if (path == "act_newsoldier.xml")
				{
					this.LoadNewSoldierActData(sText);
				}
			}
			else if (num <= 1552926937U)
			{
				if (num <= 1291837504U)
				{
					if (num != 1291060558U)
					{
						if (num == 1291837504U)
						{
							if (path == "Npc_AI.xml")
							{
								this.LoadNpcAIXmlData(sText);
							}
						}
					}
					else if (path == "act_pvpeveryweek.xml")
					{
						this.LoadPvpWeekReward(sText);
					}
				}
				else if (num != 1316059985U)
				{
					if (num != 1472542886U)
					{
						if (num == 1552926937U)
						{
							if (path == "act_jierilogin.xml")
							{
								this.LoadJieRiOnlineData(sText);
							}
						}
					}
					else if (path == "actdatingaward.xml")
					{
						this.LoadActAwardData(sText);
					}
				}
				else if (path == "fbmodel.xml")
				{
					this.ReadFbModelXml(sText);
				}
			}
			else if (num <= 1598566327U)
			{
				if (num != 1595288155U)
				{
					if (num == 1598566327U)
					{
						if (path == "act_tuijian.xml")
						{
							this.LoadTuijianData(sText);
						}
					}
				}
				else if (path == "task.xml")
				{
					this.TaskDataXml(sText);
				}
			}
			else if (num != 1677430395U)
			{
				if (num != 1775329421U)
				{
					if (num == 1796281312U)
					{
						if (path == "act_wishtree.xml")
						{
							this.LoadWishTreeData(sText);
						}
					}
				}
				else if (path == "act_weekendonline.xml")
				{
					this.LoadWeekendOnlineData(sText);
				}
			}
			else if (path == "playerpaint.xml")
			{
				this.ReadPlayerPaintXml(sText);
			}
		}
		else if (num <= 2749342254U)
		{
			if (num <= 2235282567U)
			{
				if (num <= 1992871992U)
				{
					if (num <= 1877212123U)
					{
						if (num != 1827995323U)
						{
							if (num == 1877212123U)
							{
								if (path == "sevenday.xml")
								{
									this.LoadSevenTargetData(sText);
								}
							}
						}
						else if (path == "buff.xml")
						{
							this.loadBuffXmlData(sText);
						}
					}
					else if (num != 1979239210U)
					{
						if (num != 1991639931U)
						{
							if (num == 1992871992U)
							{
								if (path == "act_herodiscount.xml")
								{
									this.LoadHeroDiscountData(sText);
								}
							}
						}
						else if (path == "act_huiliuact.xml")
						{
							this.LoadHuiliuActData(sText);
						}
					}
					else if (path == "act_skyladder.xml")
					{
						this.LoadSkyLadderData(sText);
					}
				}
				else if (num <= 2062389460U)
				{
					if (num != 2032536684U)
					{
						if (num == 2062389460U)
						{
							if (path == "act_discountshop.xml")
							{
								this.LoadDiscountShopData(sText);
							}
						}
					}
					else if (path == "fbopenset.xml")
					{
						this.LoadFbOpenSetData(sText);
					}
				}
				else if (num != 2117809054U)
				{
					if (num != 2141419150U)
					{
						if (num == 2235282567U)
						{
							if (path == "Npc.xml")
							{
								this.LoadNpcXmlData(sText);
							}
						}
					}
					else if (path == "playerface.xml")
					{
						this.ReadPlayerFaceXml(sText);
					}
				}
				else if (path == "monsterunlock.xml")
				{
					this.ReadRobotUnlockXml(sText);
				}
			}
			else if (num <= 2376794181U)
			{
				if (num <= 2297912544U)
				{
					if (num != 2280213655U)
					{
						if (num == 2297912544U)
						{
							if (path == "Chariot.xml")
							{
								this.LoadChariotXmlData(sText);
							}
						}
					}
					else if (path == "materialTag.xml")
					{
						this.ReadMaterialTagXML(sText);
					}
				}
				else if (num != 2364540858U)
				{
					if (num != 2366191931U)
					{
						if (num == 2376794181U)
						{
							if (path == "herounlockbg.xml")
							{
								this.ReadHeroInfoBg(sText);
							}
						}
					}
					else if (path == "act_openhero.xml")
					{
						this.LoadOpenHeroAct(sText);
					}
				}
				else if (path == "LanZuanReward.xml")
				{
					this.Load_Prerogative_Xml_Data(sText);
				}
			}
			else if (num <= 2459093320U)
			{
				if (num != 2451178393U)
				{
					if (num == 2459093320U)
					{
						if (path == "wenjuan.xml")
						{
							this.LoadWenJuanDiaoChaData(sText);
						}
					}
				}
				else if (path == "act_luckydraw.xml")
				{
					this.LoadLuckyDrawData(sText);
				}
			}
			else if (num != 2463274442U)
			{
				if (num != 2702869294U)
				{
					if (num == 2749342254U)
					{
						if (path == "heroinfo.xml")
						{
							this.ReadHeroInfoXML(sText);
						}
					}
				}
				else if (path == "act_kaixuegift.xml")
				{
					this.LoadKaiXueGiftData(sText);
				}
			}
			else if (path == "loadtipstxt.xml")
			{
				this.ReadAllLoadTipstxtData(sText);
			}
		}
		else if (num <= 3490535485U)
		{
			if (num <= 3151068773U)
			{
				if (num <= 2852030555U)
				{
					if (num != 2776070196U)
					{
						if (num == 2852030555U)
						{
							if (path == "dtsmap.xml")
							{
								this.LoadDtsMapXmlData(sText);
							}
						}
					}
					else if (path == "HuangZuanReward.xml")
					{
						this.Load_HuanZuanPrerogative_Xml_Data(sText);
					}
				}
				else if (num != 3101349402U)
				{
					if (num != 3124613812U)
					{
						if (num == 3151068773U)
						{
							if (path == "act_heroreturn.xml")
							{
								this.LoadHeroReturnData(sText);
							}
						}
					}
					else if (path == "MatchModel.xml")
					{
						this.MatchModelXml(sText);
					}
				}
				else if (path == "act_goddessgift.xml")
				{
					this.LoadGoddessGiftData(sText);
				}
			}
			else if (num <= 3436244671U)
			{
				if (num != 3323951877U)
				{
					if (num == 3436244671U)
					{
						if (path == "mapeffect.xml")
						{
							this.ReadMapEffectXml(sText);
						}
					}
				}
				else if (path == "act_rechargeitem.xml")
				{
					this.LoadRechargeItemData(sText);
				}
			}
			else if (num != 3440789893U)
			{
				if (num != 3444308239U)
				{
					if (num == 3490535485U)
					{
						if (path == "bgloading.xml")
						{
							this.bgloadingXML(xmlDocument);
						}
					}
				}
				else if (path == "genedata.xml")
				{
					this.LoadGeneData(sText);
				}
			}
			else if (path == "heroaward.xml")
			{
				this.ReadHeroAwardXml(sText);
			}
		}
		else if (num <= 4022539957U)
		{
			if (num <= 3707375885U)
			{
				if (num != 3509195275U)
				{
					if (num == 3707375885U)
					{
						if (path == "act_matchreward.xml")
						{
							this.LoadMatchRewardData(sText);
						}
					}
				}
				else if (path == "act_weekcharge.xml")
				{
					this.LoadWeekChargeData(sText);
				}
			}
			else if (num != 3833071559U)
			{
				if (num != 3918415538U)
				{
					if (num == 4022539957U)
					{
						if (path == "heroskill.xml")
						{
							this.LoadHeroAllSkillData(sText);
						}
					}
				}
				else if (path == "playerskill.xml")
				{
					this.ReadAllPlayerSkillXML(sText);
				}
			}
			else if (path == "dazhao.xml")
			{
				this.loadDazhaoXmlData(sText);
			}
		}
		else if (num <= 4143311160U)
		{
			if (num != 4091799691U)
			{
				if (num == 4143311160U)
				{
					if (path == "robot.xml")
					{
						this.LoadRobotCommonData(sText);
					}
				}
			}
			else if (path == "Gem.xml")
			{
				this.Load_Gem_Xml_Data(sText);
			}
		}
		else if (num != 4195791780U)
		{
			if (num != 4214840008U)
			{
				if (num == 4278422473U)
				{
					if (path == "robot_ai.xml")
					{
						this.LoadRobotAIData(sText);
					}
				}
			}
			else if (path == "Graphic_Card_Ladder.xml")
			{
				this.LoadGraphicCardLadderXmlData(sText);
			}
		}
		else if (path == "SignReward.xml")
		{
			this.LoadSignAwardData(sText);
		}
		bool flag = this.icont < this.Asslist.Count;
		if (flag)
		{
			this.doWWWResFile();
		}
		else
		{
			this.Asslist.Clear();
			this.m_szData.Clear();
			this.DownLoadConfigRes();
		}
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000E146C File Offset: 0x000DF66C
	private void bgloadingXML(XmlDocument xmldoc)
	{
		SysData.m_bgloading.Clear();
		XmlNode xmlNode = xmldoc.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			bgloading bgloading = new bgloading();
			bgloading.Init(xml);
			SysData.m_bgloading.Add(num, bgloading);
			num++;
		}
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000E1508 File Offset: 0x000DF708
	private void DownLoadConfigRes()
	{
		bool flag = this.nResNum >= this.Reslist.Count;
		if (flag)
		{
			this.Reslist.Clear();
			this.DownLoadPreResOK();
		}
		else
		{
			string[] array = (string[])this.Reslist[this.nResNum];
			string text = array[0].ToString();
			string text2 = string.Empty + text + string.Empty;
			string value = SysConfig.streamPath + "/UI/";
			bool flag2 = text2.Contains(value);
			if (flag2)
			{
				GameRoot.LoadAsset(text2, new fResourceManager.DownFinishDelegate(this.LoadResFun), text);
			}
			else
			{
				GameRoot.LoadAsset(text2, new fResourceManager.DownFinishDelegate(this.LoadResFun), text);
			}
		}
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x000E15C4 File Offset: 0x000DF7C4
	private void LoadResFun(WWW wwwParam, object customParam)
	{
		this.icont++;
		this.nResNum++;
		bool flag = this.icont < this.m_nAllResNum;
		if (flag)
		{
			this.DownLoadConfigRes();
		}
		else
		{
			this.Reslist.Clear();
			this.DownLoadPreResOK();
		}
		bool flag2 = customParam as string == "UI/UIAssets/Atlas/UICommon.assetbundle";
		if (flag2)
		{
			wwwParam.assetBundle.LoadAllAssets();
		}
		else
		{
			bool flag3 = customParam as string == "UI/common_shader.unity3d";
			if (flag3)
			{
				Debug.Log("先屏蔽公用shader");
			}
		}
		string text = customParam as string;
		bool flag4 = text != null && text == "ani/FPS_Hand_Animator_Controller.assetbundle";
		if (flag4)
		{
			RuntimeAnimatorController value = (RuntimeAnimatorController)wwwParam.assetBundle.mainAsset;
			SysConfig.allAni.Add("FPS_Hand_Animator_Controller", value);
		}
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000E16AC File Offset: 0x000DF8AC
	private void DownLoadPreResOK()
	{
		Debug.Log("预下载资源完成");
		fGameManager.m_nJoinState = 12;
		this.m_bLoadDataCmpl = true;
		fUIManager.GetInstance().ProgressChange(true, "linksocket", 1f, 0.34f);
		fGameManager.GetInstance().ConnectToHall();
		fUIManager.SetProgreessShow(false);
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000E1700 File Offset: 0x000DF900
	public void onlineLoadXml(string fileName)
	{
		string text = string.Empty + fileName + string.Empty;
		this.seturl(text);
		GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadFun), fileName);
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000E173C File Offset: 0x000DF93C
	public string[] GetbadCharArr()
	{
		bool flag = this.badCharArr != null;
		string[] result;
		if (flag)
		{
			result = this.badCharArr;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000E1768 File Offset: 0x000DF968
	public string[] GetfilterChatArr()
	{
		bool flag = this.filterChatArr != null;
		string[] result;
		if (flag)
		{
			result = this.filterChatArr;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x000E1794 File Offset: 0x000DF994
	public string[] GetRandomNameArr_first()
	{
		bool flag = this.RandomNameArr_first != null;
		string[] result;
		if (flag)
		{
			result = this.RandomNameArr_first;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x000E17C0 File Offset: 0x000DF9C0
	public string[] GetRandomNameArr_second()
	{
		bool flag = this.RandomNameArr_second != null;
		string[] result;
		if (flag)
		{
			result = this.RandomNameArr_second;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000E17EC File Offset: 0x000DF9EC
	public string[] GetRandomNameArr_special()
	{
		bool flag = this.RandomNameArr_special != null;
		string[] result;
		if (flag)
		{
			result = this.RandomNameArr_special;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000E1818 File Offset: 0x000DFA18
	private void LoadWeaponXmlData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WeaponData weaponData = new WeaponData();
			weaponData.Init(xml);
			this.WeaponXmlData.Add(weaponData.id, weaponData);
		}
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000E18B8 File Offset: 0x000DFAB8
	private void loadDazhaoXmlData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			dazhaoData dazhaoData = new dazhaoData();
			dazhaoData.Init(xml);
			this.AllDazhaoObj.Add(dazhaoData.id, dazhaoData);
		}
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000E1958 File Offset: 0x000DFB58
	private void loadBuffXmlData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			buffData buffData = new buffData();
			buffData.Init(xml);
			this.AllBuffObj.Add(buffData.id, buffData);
		}
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000E19F8 File Offset: 0x000DFBF8
	private void ReadLoadPreLoad(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			PreloadData preloadData = new PreloadData();
			preloadData.Init(xml);
			this.AllProLodaList.Add(preloadData.nId, preloadData);
		}
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000E1A98 File Offset: 0x000DFC98
	private void ReadMaterialTagXML(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			MaterialTag materialTag = new MaterialTag();
			materialTag.Init(xml);
			this.AllMaterialObj.Add(materialTag.id, materialTag);
		}
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000E1B38 File Offset: 0x000DFD38
	private void ReadAllPlayerSkillXML(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			PlayerSkillData playerSkillData = new PlayerSkillData();
			playerSkillData.Init(xml);
			this.AllPlayerSkill.Add(playerSkillData.id, playerSkillData);
		}
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000E1BD8 File Offset: 0x000DFDD8
	private void ReadHeroInfoXML(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroData heroData = new HeroData();
			heroData.Init(xml);
			this.AllHeroList.Add(heroData.id, heroData);
			int hcareer = heroData.hcareer;
			bool flag = !this.HeroCareerList.ContainsKey(hcareer);
			if (flag)
			{
				this.HeroCareerList.Add(hcareer, new ArrayList());
			}
			this.HeroCareerList[hcareer].Add(heroData.id);
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000E1CD0 File Offset: 0x000DFED0
	private void ReadMapEffectXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			MapEffectData mapEffectData = new MapEffectData();
			mapEffectData.Init(xml);
			this.AllMapEffectObj.Add(mapEffectData.id, mapEffectData);
		}
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000E1D70 File Offset: 0x000DFF70
	private void LoadNpcXmlData(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Npc_Xml_Data npc_Xml_Data = new Npc_Xml_Data();
			npc_Xml_Data.Initialize(xmlElement);
			bool flag = !this._Npc_Xml_Data.ContainsKey(npc_Xml_Data.Index);
			if (flag)
			{
				this._Npc_Xml_Data.Add(npc_Xml_Data.Index, npc_Xml_Data.EntityComponent);
			}
			else
			{
				this._Npc_Xml_Data[npc_Xml_Data.Index] = npc_Xml_Data.EntityComponent;
			}
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000E1E4C File Offset: 0x000E004C
	private void LoadNpcDeriveXmlData(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Npc_Xml_Data_Derive npc_Xml_Data_Derive = new Npc_Xml_Data_Derive();
			npc_Xml_Data_Derive.Initialize(xmlElement);
			bool flag = !this._Npc_Derive_Xml_Data.ContainsKey(npc_Xml_Data_Derive.EntityComponent.Npc_ID);
			if (flag)
			{
				this._Npc_Derive_Xml_Data.Add(npc_Xml_Data_Derive.EntityComponent.Npc_ID, npc_Xml_Data_Derive.EntityComponent);
			}
			else
			{
				this._Npc_Derive_Xml_Data[npc_Xml_Data_Derive.EntityComponent.Npc_ID] = npc_Xml_Data_Derive.EntityComponent;
			}
		}
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x000E1F3C File Offset: 0x000E013C
	private void LoadNpcAIXmlData(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Npc_Xml_Data_AI npc_Xml_Data_AI = new Npc_Xml_Data_AI();
			npc_Xml_Data_AI.Initialize(xmlElement);
			bool flag = !this._Npc_AI_Xml_Data.ContainsKey(npc_Xml_Data_AI.EntityComponent.Index);
			if (flag)
			{
				this._Npc_AI_Xml_Data.Add(npc_Xml_Data_AI.EntityComponent.Index, npc_Xml_Data_AI.EntityComponent);
			}
			else
			{
				this._Npc_AI_Xml_Data[npc_Xml_Data_AI.EntityComponent.Index] = npc_Xml_Data_AI.EntityComponent;
			}
		}
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000E202C File Offset: 0x000E022C
	private void Load_Gem_Xml_Data(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Gem_Xml_Data gem_Xml_Data = new Gem_Xml_Data();
			gem_Xml_Data.Initialize(xmlElement);
			bool flag = !this.Gem_Xml_Data.ContainsKey(gem_Xml_Data.EntityComponent.Index);
			if (flag)
			{
				this.Gem_Xml_Data.Add(gem_Xml_Data.EntityComponent.Index, gem_Xml_Data.EntityComponent);
			}
			else
			{
				this.Gem_Xml_Data[gem_Xml_Data.EntityComponent.Index] = gem_Xml_Data.EntityComponent;
			}
		}
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000E211C File Offset: 0x000E031C
	private void LoadGraphicCardLadderXmlData(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Graphic_Card_Ladder_Xml_Data graphic_Card_Ladder_Xml_Data = new Graphic_Card_Ladder_Xml_Data();
			graphic_Card_Ladder_Xml_Data.Initialize(xmlElement);
			bool flag = !this._Graphic_Card_Ladder_Xml_Data.ContainsKey(graphic_Card_Ladder_Xml_Data.Entity_Component.Name);
			if (flag)
			{
				this._Graphic_Card_Ladder_Xml_Data.Add(graphic_Card_Ladder_Xml_Data.Entity_Component.Name, graphic_Card_Ladder_Xml_Data.Entity_Component);
			}
			else
			{
				this._Graphic_Card_Ladder_Xml_Data[graphic_Card_Ladder_Xml_Data.Entity_Component.Name] = graphic_Card_Ladder_Xml_Data.Entity_Component;
			}
		}
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000E220C File Offset: 0x000E040C
	private void LoadChariotXmlData(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Chariot_Xml_Data chariot_Xml_Data = new Chariot_Xml_Data();
			chariot_Xml_Data.Initialize(xmlElement);
			bool flag = !this._Chariot_Xml_Data.ContainsKey(chariot_Xml_Data.EntityComponent.Npc_ID);
			if (flag)
			{
				this._Chariot_Xml_Data.Add(chariot_Xml_Data.EntityComponent.Npc_ID, chariot_Xml_Data.EntityComponent);
			}
			else
			{
				this._Chariot_Xml_Data[chariot_Xml_Data.EntityComponent.Npc_ID] = chariot_Xml_Data.EntityComponent;
			}
		}
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000E22FC File Offset: 0x000E04FC
	private void Load_Elevator_Xml_Data(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Elevator_Xml_Data elevator_Xml_Data = new Elevator_Xml_Data();
			elevator_Xml_Data.Initialize(xmlElement);
			bool flag = !this._Elevator_Xml_Data.ContainsKey(elevator_Xml_Data.EntityComponent.Npc_ID);
			if (flag)
			{
				this._Elevator_Xml_Data.Add(elevator_Xml_Data.EntityComponent.Npc_ID, elevator_Xml_Data.EntityComponent);
			}
			else
			{
				this._Elevator_Xml_Data[elevator_Xml_Data.EntityComponent.Npc_ID] = elevator_Xml_Data.EntityComponent;
			}
		}
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x000E23EC File Offset: 0x000E05EC
	private void Load_Prerogative_Xml_Data(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Prerogative_Xml_Data prerogative_Xml_Data = new Prerogative_Xml_Data();
			prerogative_Xml_Data.Initialize(xmlElement);
			bool flag = !this._Prerogative_Xml_Data.ContainsKey(prerogative_Xml_Data.Entity_Component.ID);
			if (flag)
			{
				this._Prerogative_Xml_Data.Add(prerogative_Xml_Data.Entity_Component.ID, prerogative_Xml_Data.Entity_Component);
			}
			else
			{
				this._Prerogative_Xml_Data[prerogative_Xml_Data.Entity_Component.ID] = prerogative_Xml_Data.Entity_Component;
			}
		}
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x000E24DC File Offset: 0x000E06DC
	private void Load_HuanZuanPrerogative_Xml_Data(string _Text)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(_Text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			Prerogative_Xml_Data prerogative_Xml_Data = new Prerogative_Xml_Data();
			prerogative_Xml_Data.Initialize(xmlElement);
			bool flag = !this._HuangZuanPrerogative_Xml_Data.ContainsKey(prerogative_Xml_Data.Entity_Component.ID);
			if (flag)
			{
				this._HuangZuanPrerogative_Xml_Data.Add(prerogative_Xml_Data.Entity_Component.ID, prerogative_Xml_Data.Entity_Component);
			}
			else
			{
				this._HuangZuanPrerogative_Xml_Data[prerogative_Xml_Data.Entity_Component.ID] = prerogative_Xml_Data.Entity_Component;
			}
		}
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x000E25CC File Offset: 0x000E07CC
	private void ReadFbXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			FbXmlData fbXmlData = new FbXmlData();
			fbXmlData.Init(xml);
			this.FbData.Add(fbXmlData.nId, fbXmlData);
		}
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x000E266C File Offset: 0x000E086C
	private void ReadFbModelXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(sText);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			FbModelXmlData fbModelXmlData = new FbModelXmlData();
			fbModelXmlData.Init(xml);
			this.FbModeData.Add(fbModelXmlData.nId, fbModelXmlData);
		}
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x000E270C File Offset: 0x000E090C
	private void HintDataXML(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HintData hintData = new HintData();
			hintData.Init(xml2);
			this.AllHintData.Add(hintData.nID, hintData);
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000E27C4 File Offset: 0x000E09C4
	private void AllHuDongXML(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HuDongHeroData huDongHeroData = new HuDongHeroData();
			huDongHeroData.Init(xml2);
			this.AllHuDongUserData.Add(huDongHeroData.id, huDongHeroData);
		}
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x000E287C File Offset: 0x000E0A7C
	private void ReadHeroUnlockXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HeroUnlockData heroUnlockData = new HeroUnlockData();
			heroUnlockData.Init(xml2, num);
			this.AllHeroUnlock.Add(heroUnlockData.key, heroUnlockData);
			num++;
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000E293C File Offset: 0x000E0B3C
	private void ReadHeroAwardXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HeroAwardData heroAwardData = new HeroAwardData();
			heroAwardData.Init(xml2);
			this.AllHeroAward.Add(heroAwardData.key, heroAwardData);
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000E29F4 File Offset: 0x000E0BF4
	private void ShopDataXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			ShopData shopData = new ShopData();
			shopData.Init(xml2);
			this.AllShopData.Add(shopData.id, shopData);
			ShopManager.GetInstance().OnInitShopData(shopData);
		}
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000E2AB8 File Offset: 0x000E0CB8
	private void ItemDataXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			ItemData itemData = new ItemData();
			itemData.Init(xml2);
			this.AllItemData.Add(itemData.m_nItemId, itemData);
		}
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000E2B70 File Offset: 0x000E0D70
	private void MatchModelXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			MatchModelData matchModelData = new MatchModelData();
			matchModelData.Init(xml2);
			this.AllMatchMode.Add(matchModelData.m_nId, matchModelData);
		}
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x000E2C28 File Offset: 0x000E0E28
	private void ReadPlayerFaceXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			PlayerFaceData playerFaceData = new PlayerFaceData();
			playerFaceData.Init(xml2, num);
			this.AllFaceData.Add(playerFaceData.key, playerFaceData);
			num++;
		}
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000E2CE8 File Offset: 0x000E0EE8
	private void ReadPlayerPaintXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			PlayerPaintData playerPaintData = new PlayerPaintData();
			playerPaintData.Init(xml2, num);
			this.AllPaintData.Add(playerPaintData.nPaintId, playerPaintData);
			num++;
		}
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000E2DA8 File Offset: 0x000E0FA8
	private void ReadHeroSkillInfo(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HeroSkillInfo heroSkillInfo = new HeroSkillInfo();
			heroSkillInfo.Init(xml2);
			this.AllHeroSkillInfo.Add(heroSkillInfo.key, heroSkillInfo);
		}
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000E2E60 File Offset: 0x000E1060
	private void TaskDataXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			TaskBaseData taskBaseData = new TaskBaseData();
			taskBaseData.Init(xml2);
			this.AllTaskObj.Add(taskBaseData.m_nTaskId, taskBaseData);
		}
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000E2F18 File Offset: 0x000E1118
	private void TaskChapterDataXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			TaskChapterData taskChapterData = new TaskChapterData();
			taskChapterData.Init(xml2);
			this.AllTaskChapterObj.Add(taskChapterData.m_nChapterId, taskChapterData);
		}
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000E2FD0 File Offset: 0x000E11D0
	private void ReadHeroInfoBg(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			HeroInfoBg heroInfoBg = new HeroInfoBg();
			heroInfoBg.Init(xml2);
			this.AllHeroInBg.Add(heroInfoBg.nID, heroInfoBg);
		}
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x000E3088 File Offset: 0x000E1288
	private void ReadAllLoadTipstxtData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			LoadTipstxtData loadTipstxtData = new LoadTipstxtData();
			loadTipstxtData.Init(xml2);
			this.AllLoadTipstxtData.Add(loadTipstxtData.m_nId, loadTipstxtData);
		}
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x000E3140 File Offset: 0x000E1340
	private void LoadLearnXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			LearnStepData learnStepData = new LearnStepData();
			learnStepData.Init(xml2);
			this.AllLearnStepData.Add(learnStepData.id, learnStepData);
		}
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x000E31F8 File Offset: 0x000E13F8
	private void LoadWenJuanDiaoChaData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.Log("LoadWenJuanDiaoChaData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WenJuanXmlData wenJuanXmlData = new WenJuanXmlData();
			wenJuanXmlData.Init(xml);
			this.WenJuanXmlData.Add(wenJuanXmlData.wId, wenJuanXmlData);
		}
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x000E32C4 File Offset: 0x000E14C4
	private void LoadLiBaoData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.Log("LoadLiBaoData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "s";
			if (flag)
			{
				ShouChongXmlData shouChongXmlData = new ShouChongXmlData();
				shouChongXmlData.Init(xmlElement);
				this.AllShouChongData.Add(shouChongXmlData.id, shouChongXmlData);
			}
			else
			{
				bool flag2 = xmlElement.Name == "l";
				if (flag2)
				{
					LvRewardXmlData lvRewardXmlData = new LvRewardXmlData();
					lvRewardXmlData.Init(xmlElement);
					this.AllLvReward.Add(lvRewardXmlData.id, lvRewardXmlData);
				}
				else
				{
					LianXuXmlData lianXuXmlData = new LianXuXmlData();
					lianXuXmlData.Init(xmlElement);
					this.AllLianXuData.Add(lianXuXmlData.id, lianXuXmlData);
				}
			}
		}
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000E341C File Offset: 0x000E161C
	private void LoadRobotCommonData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			RobotCommonData robotCommonData = new RobotCommonData();
			robotCommonData.Init(xml2);
			this.AllRobotCommonData.Add(robotCommonData.key, robotCommonData);
		}
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000E34D4 File Offset: 0x000E16D4
	private void LoadRobotAIData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			RobotAIData robotAIData = new RobotAIData();
			robotAIData.Init(xml2);
			this.AllRobotAIData.Add(robotAIData.key, robotAIData);
		}
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x000E358C File Offset: 0x000E178C
	private void LoadSkyLadderData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			SkyLadderData skyLadderData = new SkyLadderData();
			skyLadderData.Init(xml2);
			this.AllSkyLadderData.Add(skyLadderData.nId, skyLadderData);
		}
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000E3644 File Offset: 0x000E1844
	public Dictionary<int, object> GetItemData(int index)
	{
		Dictionary<int, object> dictionary = new Dictionary<int, object>();
		bool flag = Fun.GetQQ_Zuan_Type(null) == "hz";
		Dictionary<int, object> item_Data;
		if (flag)
		{
			item_Data = fGameManager.mloadConfig._HuangZuanPrerogative_Xml_Data[index].Item_Data;
		}
		else
		{
			item_Data = fGameManager.mloadConfig._Prerogative_Xml_Data[index].Item_Data;
		}
		return item_Data;
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000E36A0 File Offset: 0x000E18A0
	private void LoadActAwardData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadActAwardData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			ActDaTingAward actDaTingAward = new ActDaTingAward();
			actDaTingAward.InitData(xml, false);
			this.AllActDaTingAward.Add(actDaTingAward.id, actDaTingAward);
		}
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000E376C File Offset: 0x000E196C
	private void LoadSignAwardData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadSignAwardData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			SignXMLData signXMLData = new SignXMLData();
			signXMLData.Init(xml);
			this.AllSignAward.Add(signXMLData.id, signXMLData);
		}
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000E3838 File Offset: 0x000E1A38
	private void LoadPveData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadPveData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "f";
			if (flag)
			{
				PveRewardData pveRewardData = new PveRewardData();
				pveRewardData.Init(xmlElement);
				this.AllPveRewardData.Add(pveRewardData.fbId, pveRewardData);
			}
			else
			{
				PveXmlData pveXmlData = new PveXmlData();
				pveXmlData.Init(xmlElement);
				this.AllPveFbData.Add(pveXmlData.nId, pveXmlData);
			}
		}
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000E394C File Offset: 0x000E1B4C
	private void ReadRobotUnlockXml(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		string xml = Regex.Replace(sText, "<!--[^-]*-->", string.Empty, RegexOptions.IgnoreCase);
		xmlDocument.LoadXml(xml);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlElement xml2 = (XmlElement)obj;
			RobotUnlockData robotUnlockData = new RobotUnlockData();
			robotUnlockData.Init(xml2, num);
			this.AllRobotUnlock.Add(robotUnlockData.key, robotUnlockData);
			num++;
		}
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000E3A0C File Offset: 0x000E1C0C
	private void LoadQiangHuaData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadQiangHuaData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			QiangHuaHeroTypeXmlData qiangHuaHeroTypeXmlData = new QiangHuaHeroTypeXmlData();
			qiangHuaHeroTypeXmlData.Init(xml);
			this.AllQiangHuaData.Add(qiangHuaHeroTypeXmlData.nType, qiangHuaHeroTypeXmlData);
		}
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000E3AD8 File Offset: 0x000E1CD8
	private void LoadSevenTargetData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadSevenTargetData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			SevenTargetData sevenTargetData = new SevenTargetData();
			sevenTargetData.Init(xml);
			this.AllSevenTarget.Add(sevenTargetData.nId, sevenTargetData);
		}
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000E3BA4 File Offset: 0x000E1DA4
	private void LoadWeekLoginData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadWeekLoginData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WeekLoginData weekLoginData = new WeekLoginData();
			weekLoginData.Init(xml);
			this.AllWeekLogin.Add(weekLoginData.nId, weekLoginData);
		}
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000E3C70 File Offset: 0x000E1E70
	private void LoadSkyEveryDayData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadSkyEveryDayData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			SkyEveryDayData skyEveryDayData = new SkyEveryDayData();
			skyEveryDayData.Init(xml);
			this.AllSkyEveryDay.Add(skyEveryDayData.nId, skyEveryDayData);
		}
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000E3D3C File Offset: 0x000E1F3C
	private void LoadNewSoldierActData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadNewSoldierActData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			NewSoldierActData newSoldierActData = new NewSoldierActData();
			newSoldierActData.Init(xml);
			this.AllNewSoldier.Add(newSoldierActData.nId, newSoldierActData);
		}
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x000E3E08 File Offset: 0x000E2008
	private void LoadWeekendOnlineData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadWeekendOnlineData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WeekendOnlineData weekendOnlineData = new WeekendOnlineData();
			weekendOnlineData.Init(xml);
			this.AllWeekendOnline.Add(weekendOnlineData.nId, weekendOnlineData);
		}
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000E3ED4 File Offset: 0x000E20D4
	private void LoadKaiXueGiftData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadKaiXueGiftData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			KaiXueGiftData kaiXueGiftData = new KaiXueGiftData();
			kaiXueGiftData.Init(xml);
			this.AllKaiXueGift.Add(kaiXueGiftData.nId, kaiXueGiftData);
		}
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x000E3FA0 File Offset: 0x000E21A0
	private void LoadMatchRewardData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadMatchRewardData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			MatchRewardActData matchRewardActData = new MatchRewardActData();
			matchRewardActData.Init(xml);
			this.AllMatchRewardAct.Add(matchRewardActData.nId, matchRewardActData);
		}
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x000E406C File Offset: 0x000E226C
	private void LoadNaMatchRewardData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadNaMatchRewardData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			MatchNaRewardActData matchNaRewardActData = new MatchNaRewardActData();
			matchNaRewardActData.Init(xml);
			this.AllNaMatchRewardAct.Add(matchNaRewardActData.nId, matchNaRewardActData);
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x000E4138 File Offset: 0x000E2338
	private void LoadGoddessGiftData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadGoddessGiftData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			GoddessGiftData goddessGiftData = new GoddessGiftData();
			goddessGiftData.Init(xml);
			this.AllGoddessGift.Add(goddessGiftData.nId, goddessGiftData);
		}
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x000E4204 File Offset: 0x000E2404
	private void LoadWeekChargeData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadWeekChargeData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WeekChargeData weekChargeData = new WeekChargeData();
			weekChargeData.Init(xml);
			this.AllWeekCharge.Add(weekChargeData.nId, weekChargeData);
		}
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x000E42D0 File Offset: 0x000E24D0
	private void LoadHeroAllSkillData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHeroAllSkillData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroAllSkillData heroAllSkillData = new HeroAllSkillData();
			heroAllSkillData.Init(xml);
			this.HeroAllSkillXml.Add(heroAllSkillData.nId, heroAllSkillData);
		}
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x000E439C File Offset: 0x000E259C
	private void LoadDtsMapXmlData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadDtsMapXmlData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			DtsMapXmlData dtsMapXmlData = new DtsMapXmlData();
			dtsMapXmlData.Init(xml);
			this.DtsMapXml.Add(dtsMapXmlData.nId, dtsMapXmlData);
		}
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x000E4468 File Offset: 0x000E2668
	private void LoadLuckyDrawData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadLuckyDrawData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			LuckyDrawData luckyDrawData = new LuckyDrawData();
			luckyDrawData.Init(xml);
			this.AllLuckyDraw.Add(luckyDrawData.nId, luckyDrawData);
		}
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x000E4534 File Offset: 0x000E2734
	private void LoadBOGOActData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadBOGOActData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			BOGOActData bogoactData = new BOGOActData();
			bogoactData.Init(xml);
			this.AllBOGOActData.Add(bogoactData.nId, bogoactData);
		}
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x000E4600 File Offset: 0x000E2800
	private void LoadHeroDiscountData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHeroDiscountData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroDiscountData heroDiscountData = new HeroDiscountData();
			heroDiscountData.Init(xml);
			this.AllHeroDiscountData.Add(heroDiscountData.nId, heroDiscountData);
		}
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x000E46CC File Offset: 0x000E28CC
	private void LoadRechargeZuanData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadRechargeZuanData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			RechargeZuanData rechargeZuanData = new RechargeZuanData();
			rechargeZuanData.Init(xml);
			this.AllRechargeZuan.Add(rechargeZuanData.nId, rechargeZuanData);
		}
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x000E4798 File Offset: 0x000E2998
	private void LoadNaLianXuData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.Log("LoadNaLianXuData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			NaLianXuXmlDatas naLianXuXmlDatas = new NaLianXuXmlDatas();
			naLianXuXmlDatas.Init(xml);
			this.AllNaLianXuData.Add(naLianXuXmlDatas.id, naLianXuXmlDatas);
		}
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x000E4864 File Offset: 0x000E2A64
	private void LoadRechargeItemData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadRechargeItemData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			RechargeItemData rechargeItemData = new RechargeItemData();
			rechargeItemData.Init(xml);
			this.AllRechargeItem.Add(rechargeItemData.nId, rechargeItemData);
		}
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x000E4930 File Offset: 0x000E2B30
	private void LoadDiscountItemData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadDiscountItemData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			DiscountItemData discountItemData = new DiscountItemData();
			discountItemData.Init(xml);
			this.AllDiscountItem.Add(discountItemData.nId, discountItemData);
		}
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000E49FC File Offset: 0x000E2BFC
	private void LoadHeroLimitBuyData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHeroLimitBuyData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroLimitBuyData heroLimitBuyData = new HeroLimitBuyData();
			heroLimitBuyData.Init(xml);
			this.AllHeroLimitBuyData.Add(heroLimitBuyData.nId, heroLimitBuyData);
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x000E4AC8 File Offset: 0x000E2CC8
	private void LoadHuiliuActData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHuiliuActData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HuiliuActData huiliuActData = new HuiliuActData();
			huiliuActData.Init(xml);
			this.AllHuiliuActData.Add(huiliuActData.nId, huiliuActData);
		}
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x000E4B94 File Offset: 0x000E2D94
	private void LoadHeroReturnData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHeroReturnData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroReturnData heroReturnData = new HeroReturnData();
			heroReturnData.Init(xml);
			this.AllHeroReturnData.Add(heroReturnData.nId, heroReturnData);
		}
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x000E4C60 File Offset: 0x000E2E60
	private void LoadDiscountShopData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadDiscountShopData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			DiscountShopData discountShopData = new DiscountShopData();
			discountShopData.Init(xml);
			this.AllDiscountShopData.Add(discountShopData.nId, discountShopData);
		}
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x000E4D2C File Offset: 0x000E2F2C
	private void LoadGeneData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadGeneData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			GeneData geneData = new GeneData();
			geneData.Init(xml);
			this.AllGeneData.Add(geneData.nStep, geneData);
		}
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x000E4DF8 File Offset: 0x000E2FF8
	private void LoadTuijianData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadTuijianData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			TuijianData tuijianData = new TuijianData();
			tuijianData.Init(xml);
			this.AllTuijianData.Add(tuijianData.nId, tuijianData);
		}
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x000E4EC4 File Offset: 0x000E30C4
	private void LoadFbOpenSetData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadFbOpenSetData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			FbOpenSetData fbOpenSetData = new FbOpenSetData();
			fbOpenSetData.Init(xml);
			this.AllFbOpenSetData.Add(fbOpenSetData.nId, fbOpenSetData);
		}
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x000E4F90 File Offset: 0x000E3190
	private void LoadWishTreeData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadWishTreeData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WishTreeData wishTreeData = new WishTreeData();
			wishTreeData.Init(xml);
			this.AllWishTreeData.Add(wishTreeData.nId, wishTreeData);
		}
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x000E505C File Offset: 0x000E325C
	private void LoadPvpWeekReward(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadPvpWeekReward error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			PvpWeekReward pvpWeekReward = new PvpWeekReward();
			pvpWeekReward.Init(xml);
			this.AllPvpWeekReward.Add(pvpWeekReward.nId, pvpWeekReward);
		}
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x000E5128 File Offset: 0x000E3328
	private void LoadOpenHeroAct(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadOpenHeroAct error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			OpenHeroActData openHeroActData = new OpenHeroActData();
			openHeroActData.Init(xml);
			this.AllOpenHeroAct.Add(openHeroActData.nId, openHeroActData);
		}
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x000E51F4 File Offset: 0x000E33F4
	private void LoadExchangeData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadExchangeData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			ExchangeData exchangeData = new ExchangeData();
			exchangeData.Init(xml);
			this.AllExchangeData.Add(exchangeData.nId, exchangeData);
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x000E52C0 File Offset: 0x000E34C0
	private void LoadMultiBOGOData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadMultiBOGOData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			MultiBOGOData multiBOGOData = new MultiBOGOData();
			multiBOGOData.Init(xml);
			this.AllMultiBOGOData.Add(multiBOGOData.nId, multiBOGOData);
		}
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x000E538C File Offset: 0x000E358C
	private void LoadWinterActData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadWinterActData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			WinterActData winterActData = new WinterActData();
			winterActData.Init(xml);
			this.AllWinterActData.Add(winterActData.nId, winterActData);
		}
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000E5458 File Offset: 0x000E3658
	private void LoadHeroPartsData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadHeroPartsData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			HeroPartsData heroPartsData = new HeroPartsData();
			heroPartsData.Init(xml);
			this.AllHeroPartsData.Add(heroPartsData.nId, heroPartsData);
		}
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x000E5524 File Offset: 0x000E3724
	private void LoadJieRiOnlineData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadJieRiOnlineData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xml = (XmlElement)obj;
			JieRiOnline jieRiOnline = new JieRiOnline();
			jieRiOnline.Init(xml);
			this.AllJieRiOnline.Add(jieRiOnline.nId, jieRiOnline);
		}
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000E55F0 File Offset: 0x000E37F0
	private void LoadGuildFuliData(string sText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(sText);
		}
		catch (Exception ex)
		{
			Debug.LogError("LoadGuildFuliData error" + ex.Message);
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			bool flag = xmlElement.Name == "d";
			if (flag)
			{
				GuildFuliDay guildFuliDay = new GuildFuliDay();
				guildFuliDay.Init(xmlElement);
				this.AllGuildFuliDay.Add(guildFuliDay.nDay, guildFuliDay);
			}
			bool flag2 = xmlElement.Name == "w";
			if (flag2)
			{
				GuildFuliWeek guildFuliWeek = new GuildFuliWeek();
				guildFuliWeek.Init(xmlElement);
				this.AllGuildFuliWeek.Add(guildFuliWeek.nId, guildFuliWeek);
			}
		}
	}

	// Token: 0x040012DE RID: 4830
	private ArrayList Asslist = new ArrayList();

	// Token: 0x040012DF RID: 4831
	private ArrayList Reslist = new ArrayList();

	// Token: 0x040012E0 RID: 4832
	public XmlDocument Xmldoc;

	// Token: 0x040012E1 RID: 4833
	public Dictionary<string, UnityEngine.Object> m_szData = new Dictionary<string, UnityEngine.Object>();

	// Token: 0x040012E2 RID: 4834
	public bool isLoadGBEx;

	// Token: 0x040012E3 RID: 4835
	public Dictionary<int, string> LangObj = new Dictionary<int, string>();

	// Token: 0x040012E4 RID: 4836
	private string[] badCharArr;

	// Token: 0x040012E5 RID: 4837
	private string[] RandomNameArr_first;

	// Token: 0x040012E6 RID: 4838
	private string[] RandomNameArr_second;

	// Token: 0x040012E7 RID: 4839
	private string[] RandomNameArr_special;

	// Token: 0x040012E8 RID: 4840
	private string[] filterChatArr;

	// Token: 0x040012E9 RID: 4841
	private WWW www;

	// Token: 0x040012EA RID: 4842
	public Dictionary<int, WeaponData> WeaponXmlData = new Dictionary<int, WeaponData>();

	// Token: 0x040012EB RID: 4843
	public Dictionary<int, MaterialTag> AllMaterialObj = new Dictionary<int, MaterialTag>();

	// Token: 0x040012EC RID: 4844
	public Dictionary<int, PlayerSkillData> AllPlayerSkill = new Dictionary<int, PlayerSkillData>();

	// Token: 0x040012ED RID: 4845
	public Dictionary<int, MapEffectData> AllMapEffectObj = new Dictionary<int, MapEffectData>();

	// Token: 0x040012EE RID: 4846
	public Dictionary<int, HeroData> AllHeroList = new Dictionary<int, HeroData>();

	// Token: 0x040012EF RID: 4847
	public Dictionary<int, ArrayList> HeroCareerList = new Dictionary<int, ArrayList>();

	// Token: 0x040012F0 RID: 4848
	public Dictionary<int, dazhaoData> AllDazhaoObj = new Dictionary<int, dazhaoData>();

	// Token: 0x040012F1 RID: 4849
	public Dictionary<int, buffData> AllBuffObj = new Dictionary<int, buffData>();

	// Token: 0x040012F2 RID: 4850
	public Dictionary<int, PreloadData> AllProLodaList = new Dictionary<int, PreloadData>();

	// Token: 0x040012F3 RID: 4851
	public Dictionary<int, int> dBetPuCongRank = new Dictionary<int, int>
	{
		{
			60001,
			1
		},
		{
			60008,
			2
		},
		{
			60010,
			3
		},
		{
			60018,
			4
		},
		{
			60009,
			5
		}
	};

	// Token: 0x040012F4 RID: 4852
	public Dictionary<int, Npc_Entity> _Npc_Xml_Data = new Dictionary<int, Npc_Entity>();

	// Token: 0x040012F5 RID: 4853
	public Dictionary<int, Npc_Entity_Derive> _Npc_Derive_Xml_Data = new Dictionary<int, Npc_Entity_Derive>();

	// Token: 0x040012F6 RID: 4854
	public Dictionary<int, Npc_AI_Entity> _Npc_AI_Xml_Data = new Dictionary<int, Npc_AI_Entity>();

	// Token: 0x040012F7 RID: 4855
	public Dictionary<int, Gem_Entity> Gem_Xml_Data = new Dictionary<int, Gem_Entity>();

	// Token: 0x040012F8 RID: 4856
	public Dictionary<int, Chariot_Entity> _Chariot_Xml_Data = new Dictionary<int, Chariot_Entity>();

	// Token: 0x040012F9 RID: 4857
	public Dictionary<int, Elevator_Entity> _Elevator_Xml_Data = new Dictionary<int, Elevator_Entity>();

	// Token: 0x040012FA RID: 4858
	public Dictionary<string, Graphic_Card_Ladder_Entity> _Graphic_Card_Ladder_Xml_Data = new Dictionary<string, Graphic_Card_Ladder_Entity>();

	// Token: 0x040012FB RID: 4859
	public Dictionary<int, Prerogative_Entity> _Prerogative_Xml_Data = new Dictionary<int, Prerogative_Entity>();

	// Token: 0x040012FC RID: 4860
	public Dictionary<int, Prerogative_Entity> _HuangZuanPrerogative_Xml_Data = new Dictionary<int, Prerogative_Entity>();

	// Token: 0x040012FD RID: 4861
	public Dictionary<int, FbXmlData> FbData = new Dictionary<int, FbXmlData>();

	// Token: 0x040012FE RID: 4862
	public Dictionary<int, FbModelXmlData> FbModeData = new Dictionary<int, FbModelXmlData>();

	// Token: 0x040012FF RID: 4863
	public Dictionary<int, HintData> AllHintData = new Dictionary<int, HintData>();

	// Token: 0x04001300 RID: 4864
	public Dictionary<int, HuDongHeroData> AllHuDongUserData = new Dictionary<int, HuDongHeroData>();

	// Token: 0x04001301 RID: 4865
	public Dictionary<int, HeroUnlockData> AllHeroUnlock = new Dictionary<int, HeroUnlockData>();

	// Token: 0x04001302 RID: 4866
	public Dictionary<int, HeroAwardData> AllHeroAward = new Dictionary<int, HeroAwardData>();

	// Token: 0x04001303 RID: 4867
	public Dictionary<int, ShopData> AllShopData = new Dictionary<int, ShopData>();

	// Token: 0x04001304 RID: 4868
	public Dictionary<int, ItemData> AllItemData = new Dictionary<int, ItemData>();

	// Token: 0x04001305 RID: 4869
	public Dictionary<int, PlayerFaceData> AllFaceData = new Dictionary<int, PlayerFaceData>();

	// Token: 0x04001306 RID: 4870
	public Dictionary<int, PlayerPaintData> AllPaintData = new Dictionary<int, PlayerPaintData>();

	// Token: 0x04001307 RID: 4871
	public Dictionary<int, HeroSkillInfo> AllHeroSkillInfo = new Dictionary<int, HeroSkillInfo>();

	// Token: 0x04001308 RID: 4872
	public Dictionary<int, HeroInfoBg> AllHeroInBg = new Dictionary<int, HeroInfoBg>();

	// Token: 0x04001309 RID: 4873
	public Dictionary<int, TaskBaseData> AllTaskObj = new Dictionary<int, TaskBaseData>();

	// Token: 0x0400130A RID: 4874
	public Dictionary<int, TaskChapterData> AllTaskChapterObj = new Dictionary<int, TaskChapterData>();

	// Token: 0x0400130B RID: 4875
	public Dictionary<int, LoadTipstxtData> AllLoadTipstxtData = new Dictionary<int, LoadTipstxtData>();

	// Token: 0x0400130C RID: 4876
	public Dictionary<int, WenJuanXmlData> WenJuanXmlData = new Dictionary<int, WenJuanXmlData>();

	// Token: 0x0400130D RID: 4877
	public Dictionary<int, LearnStepData> AllLearnStepData = new Dictionary<int, LearnStepData>();

	// Token: 0x0400130E RID: 4878
	public Dictionary<int, ShouChongXmlData> AllShouChongData = new Dictionary<int, ShouChongXmlData>();

	// Token: 0x0400130F RID: 4879
	public Dictionary<int, LianXuXmlData> AllLianXuData = new Dictionary<int, LianXuXmlData>();

	// Token: 0x04001310 RID: 4880
	public Dictionary<int, LvRewardXmlData> AllLvReward = new Dictionary<int, LvRewardXmlData>();

	// Token: 0x04001311 RID: 4881
	public Dictionary<int, MatchModelData> AllMatchMode = new Dictionary<int, MatchModelData>();

	// Token: 0x04001312 RID: 4882
	public Dictionary<int, RobotCommonData> AllRobotCommonData = new Dictionary<int, RobotCommonData>();

	// Token: 0x04001313 RID: 4883
	public Dictionary<int, RobotAIData> AllRobotAIData = new Dictionary<int, RobotAIData>();

	// Token: 0x04001314 RID: 4884
	public Dictionary<int, SkyLadderData> AllSkyLadderData = new Dictionary<int, SkyLadderData>();

	// Token: 0x04001315 RID: 4885
	public Dictionary<int, ActDaTingAward> AllActDaTingAward = new Dictionary<int, ActDaTingAward>();

	// Token: 0x04001316 RID: 4886
	public Dictionary<int, SignXMLData> AllSignAward = new Dictionary<int, SignXMLData>();

	// Token: 0x04001317 RID: 4887
	public Dictionary<int, PveXmlData> AllPveFbData = new Dictionary<int, PveXmlData>();

	// Token: 0x04001318 RID: 4888
	public Dictionary<int, PveRewardData> AllPveRewardData = new Dictionary<int, PveRewardData>();

	// Token: 0x04001319 RID: 4889
	public int nVersion = 10000;

	// Token: 0x0400131A RID: 4890
	public Dictionary<int, RobotUnlockData> AllRobotUnlock = new Dictionary<int, RobotUnlockData>();

	// Token: 0x0400131B RID: 4891
	public Dictionary<int, QiangHuaHeroTypeXmlData> AllQiangHuaData = new Dictionary<int, QiangHuaHeroTypeXmlData>();

	// Token: 0x0400131C RID: 4892
	public Dictionary<int, SevenTargetData> AllSevenTarget = new Dictionary<int, SevenTargetData>();

	// Token: 0x0400131D RID: 4893
	public Dictionary<int, WeekLoginData> AllWeekLogin = new Dictionary<int, WeekLoginData>();

	// Token: 0x0400131E RID: 4894
	public Dictionary<int, SkyEveryDayData> AllSkyEveryDay = new Dictionary<int, SkyEveryDayData>();

	// Token: 0x0400131F RID: 4895
	public Dictionary<int, NewSoldierActData> AllNewSoldier = new Dictionary<int, NewSoldierActData>();

	// Token: 0x04001320 RID: 4896
	public Dictionary<int, WeekendOnlineData> AllWeekendOnline = new Dictionary<int, WeekendOnlineData>();

	// Token: 0x04001321 RID: 4897
	public Dictionary<int, KaiXueGiftData> AllKaiXueGift = new Dictionary<int, KaiXueGiftData>();

	// Token: 0x04001322 RID: 4898
	public Dictionary<int, MatchRewardActData> AllMatchRewardAct = new Dictionary<int, MatchRewardActData>();

	// Token: 0x04001323 RID: 4899
	public Dictionary<int, GoddessGiftData> AllGoddessGift = new Dictionary<int, GoddessGiftData>();

	// Token: 0x04001324 RID: 4900
	public Dictionary<int, WeekChargeData> AllWeekCharge = new Dictionary<int, WeekChargeData>();

	// Token: 0x04001325 RID: 4901
	public Dictionary<int, LuckyDrawData> AllLuckyDraw = new Dictionary<int, LuckyDrawData>();

	// Token: 0x04001326 RID: 4902
	public Dictionary<int, DtsMapXmlData> DtsMapXml = new Dictionary<int, DtsMapXmlData>();

	// Token: 0x04001327 RID: 4903
	public Dictionary<int, BOGOActData> AllBOGOActData = new Dictionary<int, BOGOActData>();

	// Token: 0x04001328 RID: 4904
	public Dictionary<int, HeroDiscountData> AllHeroDiscountData = new Dictionary<int, HeroDiscountData>();

	// Token: 0x04001329 RID: 4905
	public Dictionary<int, MatchNaRewardActData> AllNaMatchRewardAct = new Dictionary<int, MatchNaRewardActData>();

	// Token: 0x0400132A RID: 4906
	public Dictionary<int, HeroAllSkillData> HeroAllSkillXml = new Dictionary<int, HeroAllSkillData>();

	// Token: 0x0400132B RID: 4907
	public Dictionary<int, RechargeZuanData> AllRechargeZuan = new Dictionary<int, RechargeZuanData>();

	// Token: 0x0400132C RID: 4908
	public Dictionary<int, NaLianXuXmlDatas> AllNaLianXuData = new Dictionary<int, NaLianXuXmlDatas>();

	// Token: 0x0400132D RID: 4909
	public Dictionary<int, RechargeItemData> AllRechargeItem = new Dictionary<int, RechargeItemData>();

	// Token: 0x0400132E RID: 4910
	public Dictionary<int, DiscountItemData> AllDiscountItem = new Dictionary<int, DiscountItemData>();

	// Token: 0x0400132F RID: 4911
	public Dictionary<int, HeroLimitBuyData> AllHeroLimitBuyData = new Dictionary<int, HeroLimitBuyData>();

	// Token: 0x04001330 RID: 4912
	public Dictionary<int, HuiliuActData> AllHuiliuActData = new Dictionary<int, HuiliuActData>();

	// Token: 0x04001331 RID: 4913
	public Dictionary<int, HeroReturnData> AllHeroReturnData = new Dictionary<int, HeroReturnData>();

	// Token: 0x04001332 RID: 4914
	public Dictionary<int, DiscountShopData> AllDiscountShopData = new Dictionary<int, DiscountShopData>();

	// Token: 0x04001333 RID: 4915
	public Dictionary<int, GeneData> AllGeneData = new Dictionary<int, GeneData>();

	// Token: 0x04001334 RID: 4916
	public Dictionary<int, TuijianData> AllTuijianData = new Dictionary<int, TuijianData>();

	// Token: 0x04001335 RID: 4917
	public Dictionary<int, FbOpenSetData> AllFbOpenSetData = new Dictionary<int, FbOpenSetData>();

	// Token: 0x04001336 RID: 4918
	public Dictionary<int, WishTreeData> AllWishTreeData = new Dictionary<int, WishTreeData>();

	// Token: 0x04001337 RID: 4919
	public Dictionary<int, PvpWeekReward> AllPvpWeekReward = new Dictionary<int, PvpWeekReward>();

	// Token: 0x04001338 RID: 4920
	public Dictionary<int, OpenHeroActData> AllOpenHeroAct = new Dictionary<int, OpenHeroActData>();

	// Token: 0x04001339 RID: 4921
	public Dictionary<int, ExchangeData> AllExchangeData = new Dictionary<int, ExchangeData>();

	// Token: 0x0400133A RID: 4922
	public Dictionary<int, MultiBOGOData> AllMultiBOGOData = new Dictionary<int, MultiBOGOData>();

	// Token: 0x0400133B RID: 4923
	public Dictionary<int, WinterActData> AllWinterActData = new Dictionary<int, WinterActData>();

	// Token: 0x0400133C RID: 4924
	public Dictionary<int, HeroPartsData> AllHeroPartsData = new Dictionary<int, HeroPartsData>();

	// Token: 0x0400133D RID: 4925
	public Dictionary<int, JieRiOnline> AllJieRiOnline = new Dictionary<int, JieRiOnline>();

	// Token: 0x0400133E RID: 4926
	public Dictionary<int, GuildFuliDay> AllGuildFuliDay = new Dictionary<int, GuildFuliDay>();

	// Token: 0x0400133F RID: 4927
	public Dictionary<int, GuildFuliWeek> AllGuildFuliWeek = new Dictionary<int, GuildFuliWeek>();

	// Token: 0x04001340 RID: 4928
	private bool m_bLoadData;

	// Token: 0x04001341 RID: 4929
	private bool m_bLoadDataCmpl;

	// Token: 0x04001342 RID: 4930
	private int m_nAllResNum;

	// Token: 0x04001343 RID: 4931
	private int icont;

	// Token: 0x04001344 RID: 4932
	private int nResNum;
}
