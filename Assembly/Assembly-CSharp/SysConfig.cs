using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class SysConfig
{
	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x06002A90 RID: 10896 RVA: 0x001778A3 File Offset: 0x00175AA3
	public static string streamPath
	{
		get
		{
			return SysConfig._streamPath;
		}
	}

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x06002A91 RID: 10897 RVA: 0x001778AC File Offset: 0x00175AAC
	public static string StreamURL
	{
		get
		{
			bool flag = Application.platform == RuntimePlatform.WindowsPlayer;
			string result;
			if (flag)
			{
				bool client_resurl_force_local = WebParam.client_resurl_force_local;
				if (client_resurl_force_local)
				{
					bool flag2 = SysConfig.sSoucePath == string.Empty;
					if (flag2)
					{
						SysConfig.setSoucePath();
					}
					result = SysConfig.sSoucePath + "/" + SysConfig.streamPath;
				}
				else
				{
					result = string.Format("{0}/{1}", WebParam.client_resurl + "/res", SysConfig.streamPath);
				}
			}
			else
			{
				bool flag3 = Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WebGLPlayer;
				if (flag3)
				{
					result = string.Format("{0}/{1}", WebParam.client_resurl + "/res", SysConfig.streamPath);
				}
				else
				{
					bool flag4 = SysConfig.sSoucePath == string.Empty;
					if (flag4)
					{
						SysConfig.setSoucePath();
					}
					result = SysConfig.sSoucePath + "/" + SysConfig.streamPath;
				}
			}
			return result;
		}
	}

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0017799C File Offset: 0x00175B9C
	public static string DataUrl
	{
		get
		{
			bool flag = SysConfig.sSoucePath == string.Empty;
			if (flag)
			{
				SysConfig.setSoucePath();
			}
			return SysConfig.sSoucePath + "/Data";
		}
	}

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x06002A93 RID: 10899 RVA: 0x001779D8 File Offset: 0x00175BD8
	public static string DataSteamUrl
	{
		get
		{
			bool flag = SysConfig.sSoucePath == string.Empty;
			if (flag)
			{
				SysConfig.setSoucePath();
			}
			return SysConfig.sSoucePath + "/Data/" + SysConfig.streamPath;
		}
	}

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x06002A94 RID: 10900 RVA: 0x00177A1C File Offset: 0x00175C1C
	public static string BakResUrl
	{
		get
		{
			bool flag = Application.platform == RuntimePlatform.WindowsPlayer;
			string client_resurl;
			if (flag)
			{
				client_resurl = WebParam.client_resurl2;
			}
			else
			{
				bool flag2 = SysConfig.sSoucePath == string.Empty;
				if (flag2)
				{
					SysConfig.setSoucePath();
				}
				client_resurl = SysConfig.sSoucePath;
			}
			return client_resurl;
		}
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x06002A95 RID: 10901 RVA: 0x00177A63 File Offset: 0x00175C63
	public static string SceneUrl
	{
		get
		{
			return SysConfig.StreamURL + "/scene";
		}
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00177A74 File Offset: 0x00175C74
	public static string UIUrl
	{
		get
		{
			return SysConfig.StreamURL + "/UI";
		}
	}

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x06002A97 RID: 10903 RVA: 0x00177A85 File Offset: 0x00175C85
	public static string IconUrl
	{
		get
		{
			return SysConfig.StreamURL + "/Icon";
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x06002A98 RID: 10904 RVA: 0x00177A96 File Offset: 0x00175C96
	public static string ConfigUrl
	{
		get
		{
			return SysConfig.StreamURL + "/config";
		}
	}

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x06002A99 RID: 10905 RVA: 0x00177AA7 File Offset: 0x00175CA7
	public static string MiniMapUrl
	{
		get
		{
			return SysConfig.StreamURL + "/minimap";
		}
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x06002A9A RID: 10906 RVA: 0x00177AB8 File Offset: 0x00175CB8
	public static string SceneMapUrl
	{
		get
		{
			return SysConfig.StreamURL + "/scenemap";
		}
	}

	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x06002A9B RID: 10907 RVA: 0x00177AC9 File Offset: 0x00175CC9
	public static string AnimationUrl
	{
		get
		{
			return SysConfig.StreamURL + "/Animations";
		}
	}

	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06002A9C RID: 10908 RVA: 0x00177ADA File Offset: 0x00175CDA
	public static string NpcModel
	{
		get
		{
			return SysConfig.StreamURL + "/NPC";
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06002A9D RID: 10909 RVA: 0x00177AEB File Offset: 0x00175CEB
	public static string CamEffect
	{
		get
		{
			return SysConfig.StreamURL + "/CamEffect";
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06002A9E RID: 10910 RVA: 0x00177AFC File Offset: 0x00175CFC
	public static string SoundURL
	{
		get
		{
			return SysConfig.StreamURL + "/sound";
		}
	}

	// Token: 0x06002A9F RID: 10911 RVA: 0x00177B0D File Offset: 0x00175D0D
	public static void Init()
	{
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x00177B10 File Offset: 0x00175D10
	public static void setSoucePath()
	{
		string dataPath = Application.dataPath;
		bool flag = Directory.Exists(dataPath + "/StreamingAssets");
		if (flag)
		{
			SysConfig.sSoucePath = "file://" + dataPath;
		}
		else
		{
			string directoryName = Path.GetDirectoryName(dataPath);
			bool flag2 = !string.IsNullOrEmpty(directoryName) && Directory.Exists(directoryName + "/StreamingAssets");
			if (flag2)
			{
				SysConfig.sSoucePath = "file://" + directoryName.Replace("\\", "/");
			}
			else
			{
				string[] array = dataPath.Split(new char[]
				{
					'/'
				});
				string text = string.Empty;
				int num = array.Length - 2;
				for (int i = 0; i < num; i++)
				{
					text += array[i];
					bool flag3 = i != num - 1;
					if (flag3)
					{
						text += "/";
					}
				}
				SysConfig.sSoucePath = "file://" + text;
			}
		}
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x00177C0C File Offset: 0x00175E0C
	public static string getWebURL(string url)
	{
		return string.Concat(new string[]
		{
			"http://",
			WebParam.client_serverweburl,
			string.Empty,
			url,
			"&areaID=",
			WebParam.client_areaID,
			"&serverID=",
			WebParam.client_serverid
		});
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x00177C68 File Offset: 0x00175E68
	public static string GetFileNameByGrade(string sFileName, int iLevel)
	{
		int num = SysConfig.g_Grade;
		bool flag = iLevel != -3;
		if (flag)
		{
			num = iLevel;
		}
		string text = sFileName;
		string text2 = Path.GetFileNameWithoutExtension(sFileName);
		string extension = Path.GetExtension(sFileName);
		string str = sFileName.Replace(text2 + extension, string.Empty);
		int num2 = num;
		int num3 = num2;
		if (num3 != -2)
		{
			if (num3 == -1)
			{
				text = str + text2 + "_l" + extension;
			}
		}
		else
		{
			text = str + text2 + "_nl" + extension;
		}
		bool flag2 = num == 1;
		if (flag2)
		{
			text = str + text2 + "_h" + extension;
		}
		text2 = text.Replace(SysConfig.StreamURL + "/", string.Empty);
		bool flag3 = SysConfig.FileVersion.ContainsKey(text2);
		string result;
		if (flag3)
		{
			result = text;
		}
		else
		{
			bool flag4 = Application.platform == RuntimePlatform.WindowsEditor && File.Exists(text.Replace("file://", string.Empty));
			if (flag4)
			{
				result = text;
			}
			else
			{
				result = sFileName;
			}
		}
		return result;
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x00177D6C File Offset: 0x00175F6C
	public static string GetFilePath(string sFileName)
	{
		bool flag = SysConfig.g_client == "1.1.1";
		if (flag)
		{
		}
		bool flag2 = SysConfig.FileVersion.ContainsKey(sFileName);
		string result;
		if (flag2)
		{
			bool flag3 = SysConfig.FileVersion[sFileName].m_sVersion == string.Empty;
			if (flag3)
			{
				result = string.Format("{0}/{1}", WebParam.client_resurl + "/res", SysConfig.streamPath);
			}
			else
			{
				result = string.Format("{0}/{1}/{2}", WebParam.client_resurl + "/res", SysConfig.FileVersion[sFileName].m_sVersion, SysConfig.streamPath);
			}
		}
		else
		{
			result = string.Format("{0}/{1}/{2}", WebParam.client_resurl + "/res", WebParam.client_gameversion, SysConfig.streamPath);
		}
		return result;
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x00177E38 File Offset: 0x00176038
	public static string GetFileVersion(string sFileName)
	{
		bool flag = SysConfig.FileVersion.ContainsKey(sFileName);
		string result;
		if (flag)
		{
			result = SysConfig.FileVersion[sFileName].m_sCrc;
		}
		else
		{
			result = WebParam.client_version;
		}
		return result;
	}

	// Token: 0x0400241D RID: 9245
	public static string g_client = "1.3.5";

	// Token: 0x0400241E RID: 9246
	public static string _streamPath = "StreamingAssets";

	// Token: 0x0400241F RID: 9247
	public static string g_version = "0.1";

	// Token: 0x04002420 RID: 9248
	public static string g_IP = "192.168.0.20";

	// Token: 0x04002421 RID: 9249
	public static string g_login = string.Empty;

	// Token: 0x04002422 RID: 9250
	public static int g_PORT = 10002;

	// Token: 0x04002423 RID: 9251
	public static string sModelNum = "1";

	// Token: 0x04002424 RID: 9252
	public static float cameraNormalFOV = 60f;

	// Token: 0x04002425 RID: 9253
	public static float cameraDistance = 1.8f;

	// Token: 0x04002426 RID: 9254
	public static float cameraPosx = 0.7f;

	// Token: 0x04002427 RID: 9255
	public static float cameraPosy = 1.85f;

	// Token: 0x04002428 RID: 9256
	public static float cameraPosz = 0f;

	// Token: 0x04002429 RID: 9257
	public static float OriginalCameraPostionX = 0.7f;

	// Token: 0x0400242A RID: 9258
	public static float OriginalCameraPostionY = 1.85f;

	// Token: 0x0400242B RID: 9259
	public static float OriginalCameraPostionZ = 0f;

	// Token: 0x0400242C RID: 9260
	public static float OriginalCameraDistance = 1.8f;

	// Token: 0x0400242D RID: 9261
	public static float CenterCameraPostionX = 0.2f;

	// Token: 0x0400242E RID: 9262
	public static float CenterCameraPostionY = 2.3f;

	// Token: 0x0400242F RID: 9263
	public static float CenterCameraPostionZ = 0.2f;

	// Token: 0x04002430 RID: 9264
	public static float CenterCameraDistance = 3f;

	// Token: 0x04002431 RID: 9265
	public static Dictionary<string, Asset> FileVersion = new Dictionary<string, Asset>();

	// Token: 0x04002432 RID: 9266
	public static Dictionary<string, RuntimeAnimatorController> allAni = new Dictionary<string, RuntimeAnimatorController>();

	// Token: 0x04002433 RID: 9267
	public static int g_Grade = 0;

	// Token: 0x04002434 RID: 9268
	public static int g_logType = 1;

	// Token: 0x04002435 RID: 9269
	public static string gUserName = string.Empty;

	// Token: 0x04002436 RID: 9270
	public static float ScreenWidth = 1920f;

	// Token: 0x04002437 RID: 9271
	public static float ScreenHeight = 1080f;

	// Token: 0x04002438 RID: 9272
	public static bool IsRobot = false;

	// Token: 0x04002439 RID: 9273
	public static string sSoucePath = string.Empty;
}
