using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class PreLoadManager : MonoBehaviour
{
	// Token: 0x06001E65 RID: 7781 RVA: 0x0011AF0D File Offset: 0x0011910D
	private void Start()
	{
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x0011AF10 File Offset: 0x00119110
	public void RemoveDown(string url)
	{
		PreLoadManager.nofiledown.Add(url);
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x0011AF20 File Offset: 0x00119120
	private void Update()
	{
		bool flag = ((this.wPreLoad == null || this.wPreLoad.isDone) && !this.IsDownUI) || !(MainUICon.inst != null);
		if (!flag)
		{
			this.m_nEndTick = Environment.TickCount;
			this.nTime = this.m_nEndTick - this.m_nStartTick;
			bool flag2 = this.nTime == 0;
			if (!flag2)
			{
				int num = Environment.TickCount - this.m_nTickTime;
				bool isDownUI = this.IsDownUI;
				if (isDownUI)
				{
					bool flag3 = this.nfpre == 0f;
					if (flag3)
					{
						this.nfpre = (float)this.DownloadedByte * this.ResManager.wwwp(this.sUIname) / (float)this.nTime * 1024f;
						this.m_nTickTime = Environment.TickCount;
					}
					else
					{
						bool flag4 = num / 2500 > 1;
						if (flag4)
						{
							this.m_nTickTime = Environment.TickCount;
							this.nfpre = (float)this.DownloadedByte * this.ResManager.wwwp(this.sUIname) / (float)this.nTime * 1024f;
						}
					}
					bool flag5 = this.DelHouZhui(this.ResManager.wwwpurl(this.sUIname)) != string.Empty;
					if (flag5)
					{
						MainUICon.inst.UpdownLoadSdPr((float)(this.m_nNowPre + this.DownloadedByte), (float)this.m_nMaxPre, this.nfpre, string.Empty, this.isShowPro);
					}
				}
				else
				{
					bool flag6 = this.nfpre == 0f;
					if (flag6)
					{
						this.m_nTickTime = Environment.TickCount;
						this.nfpre = (float)this.DownloadedByte * this.wPreLoad.progress / (float)this.nTime * 1024f;
					}
					else
					{
						bool flag7 = num / 2500 > 1;
						if (flag7)
						{
							this.m_nTickTime = Environment.TickCount;
							this.nfpre = (float)this.DownloadedByte * this.wPreLoad.progress / (float)this.nTime * 1024f;
						}
					}
					MainUICon.inst.UpdownLoadSdPr((float)this.m_nNowPre + (float)this.DownloadedByte * this.wPreLoad.progress, (float)this.m_nMaxPre, this.nfpre, string.Empty, true);
				}
			}
		}
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x0011B174 File Offset: 0x00119374
	private string DelHouZhui(string sName)
	{
		string result = string.Empty;
		int num = sName.IndexOf(".");
		bool flag = num != -1;
		if (flag)
		{
			result = sName.Substring(0, num);
		}
		return result;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x0011B1B0 File Offset: 0x001193B0
	public void InitPreLoad()
	{
		fGameManager.IsModelDownOK = WebParam.client_resurl_force_local;
		bool flag = !WebParam.client_resurl_force_local;
		if (flag)
		{
			base.InvokeRepeating("StartPreLoad", 5f, 2f);
		}
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0011B1EC File Offset: 0x001193EC
	private void PreMaxPre()
	{
		PreloadData preloadData = new PreloadData();
		for (int i = 0; i < fGameManager.mloadConfig.AllProLodaList.Count; i++)
		{
			preloadData = fGameManager.mloadConfig.AllProLodaList[i];
			bool flag = !string.IsNullOrEmpty(preloadData.sName);
			if (flag)
			{
				this.m_nMaxPre += preloadData.nPre;
			}
		}
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0011B258 File Offset: 0x00119458
	public void StartPreLoad()
	{
		bool flag = WebParam.client_resurl_force_local || PlayerDataManager.getInstance().HUserData.m_nLevel < 5 || fGameManager.m_nPreLoadOff;
		if (!flag)
		{
			bool isDown = this.IsDown;
			if (isDown)
			{
				bool flag2 = this.sNowPreNum != this.m_nPreloadNum;
				if (flag2)
				{
					this.sNowPreNum = this.m_nPreloadNum;
				}
				else
				{
					this.nIsReDown++;
				}
				bool flag3 = this.nIsReDown > 15 && this.wPreLoad != null;
				if (flag3)
				{
					PreloadData preloadData = new PreloadData();
					preloadData = fGameManager.mloadConfig.AllProLodaList[this.m_nPreloadNum];
					Debug.Log("预加载 15次后 没下载成功的资源~~~~+" + preloadData.sName);
					this.m_nPreloadNum++;
					this.nIsReDown = 0;
					this.IsDown = false;
					this.wPreLoad.Dispose();
					this.wPreLoad = null;
				}
			}
			else
			{
				string text = string.Empty;
				string text2 = string.Empty;
				string url = string.Empty;
				bool flag4 = this.wPreLoad != null && !this.wPreLoad.isDone;
				if (flag4)
				{
					Debug.Log("StartPreLoad ---5");
				}
				else
				{
					bool flag5 = this.SpecialList.Count > 0;
					if (flag5)
					{
						text = (string)this.SpecialList[0];
						text2 = text;
						url = text2;
						bool flag6 = this.ResIsLoad(url);
						if (flag6)
						{
							MatchManager.GetInstance().OnCheckModel((string)this.SpecialList[0]);
							this.SpecialList.RemoveAt(0);
							this.StartPreLoad();
						}
					}
					else
					{
						bool flag7 = this.m_nPreloadNum < fGameManager.mloadConfig.AllProLodaList.Count;
						if (flag7)
						{
							bool flag8 = this.m_nMaxPre <= 0;
							if (flag8)
							{
								this.PreMaxPre();
							}
							PreloadData preloadData2 = new PreloadData();
							preloadData2 = fGameManager.mloadConfig.AllProLodaList[this.m_nPreloadNum];
							bool flag9 = string.IsNullOrEmpty(preloadData2.sName);
							if (flag9)
							{
								this.m_nPreloadNum++;
								this.StartPreLoad();
							}
							else
							{
								bool flag10 = preloadData2 != null;
								if (flag10)
								{
									text = SysConfig.StreamURL + "/" + preloadData2.sName;
									url = preloadData2.sName;
								}
								bool flag11 = this.ResIsLoad(url);
								if (flag11)
								{
									this.m_nMaxPre -= preloadData2.nPre;
									this.m_nPreloadNum++;
									this.StartPreLoad();
								}
								else
								{
									bool flag12 = preloadData2.nType == 1;
									if (flag12)
									{
										this.ResManager = GameRoot.mUIAssetManager;
									}
									bool flag13 = preloadData2.nType == 2;
									if (flag13)
									{
										this.ResManager = GameRoot.mMapSceneManager;
									}
									bool flag14 = preloadData2.nType == 3;
									if (flag14)
									{
										this.ResManager = GameRoot.mHeroAniManager;
									}
									bool flag15 = preloadData2.nType == 4;
									if (flag15)
									{
										this.ResManager = GameRoot.mEffectResManager;
									}
									bool flag16 = preloadData2.nType == 5;
									if (flag16)
									{
										this.ResManager = GameRoot.mSoundResManager;
									}
									bool flag17 = preloadData2.nType == 6;
									if (flag17)
									{
										this.ResManager = GameRoot.mGlobalAssetManager;
									}
									this.IsDown = true;
									this.IsDownUI = true;
									this.DownloadedByte = preloadData2.nPre;
									this.sUIname = url;
									this.m_nStartTick = Environment.TickCount;
									GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.GetUiOK), preloadData2);
									int num = 0;
									bool flag18 = Application.platform == RuntimePlatform.WindowsPlayer;
									if (flag18)
									{
										bool flag19 = this.m_nPreloadNum > 1;
										if (flag19)
										{
											this.isShowPro = Caching.IsVersionCached(this.sUIname, int.Parse(SysConfig.GetFileVersion(this.sUIname)));
										}
										else
										{
											this.isShowPro = true;
										}
										bool flag20 = num == 0 && !this.isShowPro;
										if (flag20)
										{
											this.isShowPro = Caching.IsVersionCached(this.sUIname, int.Parse(SysConfig.GetFileVersion(this.sUIname)));
											num++;
										}
										bool flag21 = num == 1 && !this.isShowPro;
										if (flag21)
										{
											this.isShowPro = Caching.IsVersionCached(this.sUIname, int.Parse(SysConfig.GetFileVersion(this.sUIname)));
											num++;
										}
									}
								}
							}
						}
						else
						{
							bool flag22 = !fGameManager.IsPreDownOK && MainUICon.inst != null;
							if (flag22)
							{
								MainUICon.inst.SetPreShow(false);
							}
							fGameManager.IsPreDownOK = true;
							base.CancelInvoke("StartPreLoad");
							bool flag23 = MainUICon.inst != null;
							if (flag23)
							{
								MainUICon.inst.UpdownLoadSdPr(1f, 1f, 0f, string.Empty, true);
							}
							bool flag24 = this.wPreLoad != null;
							if (flag24)
							{
								this.IsDown = false;
								this.wPreLoad.Dispose();
								this.wPreLoad = null;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x0011B764 File Offset: 0x00119964
	public void GetUiOK(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("资源问题4");
		}
		else
		{
			PreloadData preloadData = new PreloadData();
			preloadData = (PreloadData)customParam;
			this.m_nNowPre += preloadData.nPre;
			bool flag2 = preloadData.nAssindex >= preloadData.nAssNum && preloadData.nType == 3;
			if (flag2)
			{
				fGameManager.IsModelDownOK = true;
			}
			this.m_nPreloadNum++;
			this.IsDown = false;
			this.IsDownUI = false;
			this.sUIname = string.Empty;
			this.StartPreLoad();
		}
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x0011B808 File Offset: 0x00119A08
	public void StopPreLoad()
	{
		bool flag = this.wPreLoad != null;
		if (flag)
		{
			this.IsDown = false;
			this.m_sNowDownAss = string.Empty;
			this.wPreLoad.Dispose();
			this.wPreLoad = null;
		}
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x0011B84C File Offset: 0x00119A4C
	public void OnCheckPreLoad(string sUrl)
	{
		bool flag = this.m_sNowDownAss == sUrl;
		if (flag)
		{
			this.wPreLoad.Dispose();
			this.StopPreLoad();
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x0011B880 File Offset: 0x00119A80
	public string GetResPath(int nType)
	{
		string result = string.Empty;
		switch (nType)
		{
		case 1:
			result = "UI/";
			break;
		case 2:
			result = "scene/";
			break;
		case 3:
			result = "HeroAnimator/";
			break;
		case 4:
			result = "EffectRes/";
			break;
		case 5:
			result = "SoundRes/";
			break;
		}
		return result;
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x0011B8E3 File Offset: 0x00119AE3
	public void SpecialLoad(int nType, string sResName)
	{
		sResName = this.GetResPath(nType) + sResName;
		this.SpecialList.Add(sResName);
		this.StartPreLoad();
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x0011B90C File Offset: 0x00119B0C
	public void SetResWWW(string url, WWW www)
	{
		bool flag = www == null || www.error != null;
		if (flag)
		{
			Debug.LogError("资源问题5");
		}
		else
		{
			bool flag2 = url.StartsWith("/");
			if (flag2)
			{
				url = url.Substring(1, url.Length - 2);
			}
			string[] array = url.Split(new char[]
			{
				'/'
			});
			string text = url;
			int num = text.IndexOf("/");
			bool flag3 = num > 0;
			if (flag3)
			{
				string text2 = text.Substring(0, num);
				string a = text2;
				if (!(a == "file:"))
				{
					if (!(a == "UI"))
					{
						if (!(a == "scene"))
						{
							if (!(a == "HeroAnimator"))
							{
								if (!(a == "EffectRes"))
								{
									if (!(a == "SoundRes"))
									{
										GameRoot.mGlobalAssetManager.AddDic(url, www);
									}
									else
									{
										GameRoot.mSoundResManager.AddDic(array[1], www);
									}
								}
								else
								{
									GameRoot.mEffectResManager.AddDic(array[1], www);
								}
							}
							else
							{
								GameRoot.mHeroAniManager.AddDic(array[1], www);
							}
						}
						else
						{
							GameRoot.mMapSceneManager.AddDic(array[1], www);
						}
					}
					else
					{
						GameRoot.mUIAssetManager.AddDic(array[1], www);
					}
				}
				else
				{
					Debug.LogError("GetResWWW请传入相对路径" + url);
				}
			}
		}
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x0011BA6C File Offset: 0x00119C6C
	public bool ResIsLoad(string url)
	{
		bool flag = url.StartsWith("/");
		if (flag)
		{
			url = url.Substring(1, url.Length - 2);
		}
		string[] array = url.Split(new char[]
		{
			'/'
		});
		string text = url;
		int num = text.IndexOf("/");
		bool flag2 = num > 0;
		bool result;
		if (flag2)
		{
			string a = text.Substring(0, num);
			if (!true)
			{
			}
			bool containsKey;
			if (!(a == "UI"))
			{
				if (!(a == "scene"))
				{
					if (!(a == "HeroAnimator"))
					{
						if (!(a == "EffectRes"))
						{
							if (!(a == "SoundRes"))
							{
								containsKey = GameRoot.mGlobalAssetManager.GetContainsKey(url);
							}
							else
							{
								containsKey = GameRoot.mSoundResManager.GetContainsKey(array[1]);
							}
						}
						else
						{
							containsKey = GameRoot.mEffectResManager.GetContainsKey(array[1]);
						}
					}
					else
					{
						containsKey = GameRoot.mHeroAniManager.GetContainsKey(array[1]);
					}
				}
				else
				{
					containsKey = GameRoot.mMapSceneManager.GetContainsKey(array[1]);
				}
			}
			else
			{
				containsKey = GameRoot.mUIAssetManager.GetContainsKey(array[1]);
			}
			if (!true)
			{
			}
			result = containsKey;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0011BB98 File Offset: 0x00119D98
	public WWW GetResWWW(string url)
	{
		bool flag = url.StartsWith("/");
		if (flag)
		{
			url = url.Substring(1, url.Length - 2);
		}
		string[] array = url.Split(new char[]
		{
			'/'
		});
		string text = url;
		int num = text.IndexOf("/");
		bool flag2 = num > 0;
		WWW result;
		if (flag2)
		{
			string a = text.Substring(0, num);
			if (!true)
			{
			}
			WWW resWWW;
			if (!(a == "UI"))
			{
				if (!(a == "scene"))
				{
					if (!(a == "HeroAnimator"))
					{
						if (!(a == "EffectRes"))
						{
							if (!(a == "SoundRes"))
							{
								resWWW = GameRoot.mGlobalAssetManager.GetResWWW(url);
							}
							else
							{
								resWWW = GameRoot.mSoundResManager.GetResWWW(array[1]);
							}
						}
						else
						{
							resWWW = GameRoot.mEffectResManager.GetResWWW(array[1]);
						}
					}
					else
					{
						resWWW = GameRoot.mHeroAniManager.GetResWWW(array[1]);
					}
				}
				else
				{
					resWWW = GameRoot.mMapSceneManager.GetResWWW(array[1]);
				}
			}
			else
			{
				resWWW = GameRoot.mUIAssetManager.GetResWWW(array[1]);
			}
			if (!true)
			{
			}
			result = resWWW;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x0011BCC4 File Offset: 0x00119EC4
	private string[] GetAssetBundle(string url)
	{
		string[] result = null;
		bool flag = url.StartsWith("/");
		if (flag)
		{
			url = url.Substring(1, url.Length - 2);
		}
		string text = url;
		int num = text.IndexOf("/");
		bool flag2 = num > 0;
		if (flag2)
		{
			string text2 = text.Substring(0, num);
			string a = text2;
			if (!(a == "HeroAnimator"))
			{
				if (!(a == "EffectRes"))
				{
					if (!(a == "SoundRes"))
					{
						return result;
					}
					url = url.Substring(num + 1, url.Length - num - 1);
					result = GameRoot.mSoundResManager.m_AssetBundleManifest.GetAllDependencies(url);
				}
				else
				{
					url = url.Substring(num + 1, url.Length - num - 1);
					result = GameRoot.mEffectResManager.m_AssetBundleManifest.GetAllDependencies(url);
				}
			}
			else
			{
				url = url.Substring(num + 1, url.Length - num - 1);
				result = GameRoot.mHeroAniManager.m_AssetBundleManifest.GetAllDependencies(url);
			}
		}
		return result;
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0011BDD4 File Offset: 0x00119FD4
	public void WritePreLog()
	{
		string value = string.Empty;
		value = string.Concat(new string[]
		{
			PlayerDataManager.getInstance().HUserData.m_sName,
			":",
			fGameManager.IsModelDownOK.ToString(),
			":",
			this.m_nPreloadNum.ToString()
		});
		ArrayList arrayList = new ArrayList();
		arrayList.Add("500");
		arrayList.Add(value);
		arrayList.Add(DateTime.Now.ToString());
		arrayList.Add(SysConfig.g_client);
		string value2 = string.Empty;
		bool flag = fGameManager.Heroplayer != null;
		if (flag)
		{
			value2 = string.Format("now:{0},in:{1},load:{2}", Fun.GetServerDate(), fGameManager.Heroplayer.initCameraTime, CReadModel.Instance.lastLoadModelTime.ToString("F1"));
		}
		arrayList.Add(value2);
		fGameManager.LobbyPlayer.SendMsgToHallServer(2110, arrayList);
	}

	// Token: 0x04001C27 RID: 7207
	public static List<string> nofiledown = new List<string>();

	// Token: 0x04001C28 RID: 7208
	public int m_nPreloadNum;

	// Token: 0x04001C29 RID: 7209
	private WWW wPreLoad;

	// Token: 0x04001C2A RID: 7210
	private bool nIsPreLoad;

	// Token: 0x04001C2B RID: 7211
	private int DownloadedByte;

	// Token: 0x04001C2C RID: 7212
	public int m_nStartTick;

	// Token: 0x04001C2D RID: 7213
	public int m_nEndTick;

	// Token: 0x04001C2E RID: 7214
	private ArrayList SpecialList = new ArrayList();

	// Token: 0x04001C2F RID: 7215
	private string m_sNowDownAss = string.Empty;

	// Token: 0x04001C30 RID: 7216
	private int m_nNowPre;

	// Token: 0x04001C31 RID: 7217
	private int m_nMaxPre;

	// Token: 0x04001C32 RID: 7218
	private int nTime;

	// Token: 0x04001C33 RID: 7219
	private bool IsDown;

	// Token: 0x04001C34 RID: 7220
	private int nIsReDown;

	// Token: 0x04001C35 RID: 7221
	private int sNowPreNum;

	// Token: 0x04001C36 RID: 7222
	private bool IsDownUI;

	// Token: 0x04001C37 RID: 7223
	private string sUIname = string.Empty;

	// Token: 0x04001C38 RID: 7224
	private float nfpre;

	// Token: 0x04001C39 RID: 7225
	private int m_nTickTime;

	// Token: 0x04001C3A RID: 7226
	private fResourceManager ResManager;

	// Token: 0x04001C3B RID: 7227
	private bool isShowPro;
}
