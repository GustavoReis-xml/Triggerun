using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YouMe
{
	// Token: 0x020004FB RID: 1275
	public class YouMeVoiceAPI
	{
		// Token: 0x060030FC RID: 12540 RVA: 0x001B0D95 File Offset: 0x001AEF95
		private YouMeVoiceAPI()
		{
		}

		// Token: 0x060030FD RID: 12541
		[DllImport("youme_voice_engine")]
		private static extern int youme_init(string strAPPKey, string strAPPSecret, int serverRegionId, string strExtServerRegionName);

		// Token: 0x060030FE RID: 12542
		[DllImport("youme_voice_engine")]
		private static extern int youme_unInit();

		// Token: 0x060030FF RID: 12543
		[DllImport("youme_voice_engine")]
		private static extern IntPtr youme_getCbMessage();

		// Token: 0x06003100 RID: 12544
		[DllImport("youme_voice_engine")]
		private static extern void youme_freeCbMessage(IntPtr pMsg);

		// Token: 0x06003101 RID: 12545
		[DllImport("youme_voice_engine")]
		private static extern int youme_setOutputToSpeaker(bool bOutputToSpeaker);

		// Token: 0x06003102 RID: 12546
		[DllImport("youme_voice_engine")]
		private static extern void youme_setSpeakerMute(bool bOn);

		// Token: 0x06003103 RID: 12547
		[DllImport("youme_voice_engine")]
		private static extern bool youme_getSpeakerMute();

		// Token: 0x06003104 RID: 12548
		[DllImport("youme_voice_engine")]
		private static extern bool youme_getMicrophoneMute();

		// Token: 0x06003105 RID: 12549
		[DllImport("youme_voice_engine")]
		private static extern void youme_setMicrophoneMute(bool mute);

		// Token: 0x06003106 RID: 12550
		[DllImport("youme_voice_engine")]
		private static extern void youme_setAutoSendStatus(bool bAutoSend);

		// Token: 0x06003107 RID: 12551
		[DllImport("youme_voice_engine")]
		private static extern int youme_getVolume();

		// Token: 0x06003108 RID: 12552
		[DllImport("youme_voice_engine")]
		private static extern void youme_setVolume(uint uiVolume);

		// Token: 0x06003109 RID: 12553
		[DllImport("youme_voice_engine")]
		private static extern bool youme_getUseMobileNetworkEnabled();

		// Token: 0x0600310A RID: 12554
		[DllImport("youme_voice_engine")]
		private static extern void youme_setUseMobileNetworkEnabled(bool bEnabled);

		// Token: 0x0600310B RID: 12555
		[DllImport("youme_voice_engine")]
		private static extern int youme_joinChannelSingleMode(string strUserID, string strChannelID, int userRole);

		// Token: 0x0600310C RID: 12556
		[DllImport("youme_voice_engine")]
		private static extern int youme_joinChannelMultiMode(string strUserID, string strChannelID);

		// Token: 0x0600310D RID: 12557
		[DllImport("youme_voice_engine")]
		private static extern int youme_speakToChannel(string strChannelID);

		// Token: 0x0600310E RID: 12558
		[DllImport("youme_voice_engine")]
		private static extern int youme_leaveChannelMultiMode(string strChannelID);

		// Token: 0x0600310F RID: 12559
		[DllImport("youme_voice_engine")]
		private static extern int youme_leaveChannelAll();

		// Token: 0x06003110 RID: 12560
		[DllImport("youme_voice_engine")]
		private static extern int youme_setOtherMicMute(string userID, bool mute);

		// Token: 0x06003111 RID: 12561
		[DllImport("youme_voice_engine")]
		private static extern int youme_setOtherSpeakerMute(string userID, bool mute);

		// Token: 0x06003112 RID: 12562
		[DllImport("youme_voice_engine")]
		private static extern int youme_setListenOtherVoice(string userID, bool isOn);

		// Token: 0x06003113 RID: 12563
		[DllImport("youme_voice_engine")]
		private static extern void youme_setServerRegion(int regionId, string strExtRegionId, bool bAppend);

		// Token: 0x06003114 RID: 12564
		[DllImport("youme_voice_engine")]
		private static extern int youme_playBackgroundMusic(string pFilePath, bool bRepeat);

		// Token: 0x06003115 RID: 12565
		[DllImport("youme_voice_engine")]
		private static extern int youme_pauseBackgroundMusic();

		// Token: 0x06003116 RID: 12566
		[DllImport("youme_voice_engine")]
		private static extern int youme_resumeBackgroundMusic();

		// Token: 0x06003117 RID: 12567
		[DllImport("youme_voice_engine")]
		private static extern int youme_stopBackgroundMusic();

		// Token: 0x06003118 RID: 12568
		[DllImport("youme_voice_engine")]
		private static extern int youme_setBackgroundMusicVolume(int volume);

		// Token: 0x06003119 RID: 12569
		[DllImport("youme_voice_engine")]
		private static extern int youme_setHeadsetMonitorOn(bool enabled);

		// Token: 0x0600311A RID: 12570
		[DllImport("youme_voice_engine")]
		private static extern int youme_setReverbEnabled(bool enabled);

		// Token: 0x0600311B RID: 12571
		[DllImport("youme_voice_engine")]
		private static extern int youme_setVadCallbackEnabled(bool enabled);

		// Token: 0x0600311C RID: 12572
		[DllImport("youme_voice_engine")]
		private static extern int youme_setMicLevelCallback(int maxLevel);

		// Token: 0x0600311D RID: 12573
		[DllImport("youme_voice_engine")]
		private static extern int youme_pauseChannel();

		// Token: 0x0600311E RID: 12574
		[DllImport("youme_voice_engine")]
		private static extern int youme_resumeChannel();

		// Token: 0x0600311F RID: 12575
		[DllImport("youme_voice_engine")]
		private static extern void youme_setRecordingTimeMs(uint timeMs);

		// Token: 0x06003120 RID: 12576
		[DllImport("youme_voice_engine")]
		private static extern void youme_setPlayingTimeMs(uint timeMs);

		// Token: 0x06003121 RID: 12577
		[DllImport("youme_voice_engine")]
		private static extern int youme_getSDKVersion();

		// Token: 0x06003122 RID: 12578
		[DllImport("youme_voice_engine")]
		private static extern int youme_requestRestApi(string strCommand, string strQueryBody, ref int requestID);

		// Token: 0x06003123 RID: 12579
		[DllImport("youme_voice_engine")]
		private static extern int youme_getChannelUserList(string strChannelID, int maxCount, bool notifyMemChange);

		// Token: 0x06003124 RID: 12580
		[DllImport("youme_voice_engine")]
		private static extern int youme_setToken(string strToken);

		// Token: 0x06003125 RID: 12581
		[DllImport("youme_voice_engine")]
		private static extern int youme_setReleaseMicWhenMute(bool enabled);

		// Token: 0x06003126 RID: 12582
		[DllImport("youme_voice_engine")]
		private static extern int youme_setGrabMicOption(string pChannelID, int mode, int maxAllowCount, int maxTalkTime, uint voteTime);

		// Token: 0x06003127 RID: 12583
		[DllImport("youme_voice_engine")]
		private static extern int youme_startGrabMicAction(string pChannelID, string pContent);

		// Token: 0x06003128 RID: 12584
		[DllImport("youme_voice_engine")]
		private static extern int youme_stopGrabMicAction(string pChannelID, string pContent);

		// Token: 0x06003129 RID: 12585
		[DllImport("youme_voice_engine")]
		private static extern int youme_requestGrabMic(string pChannelID, int score, bool isAutoOpenMic, string pContent);

		// Token: 0x0600312A RID: 12586
		[DllImport("youme_voice_engine")]
		private static extern int youme_releaseGrabMic(string pChannelID);

		// Token: 0x0600312B RID: 12587
		[DllImport("youme_voice_engine")]
		private static extern int youme_setInviteMicOption(string pChannelID, int waitTimeout, int maxTalkTime);

		// Token: 0x0600312C RID: 12588
		[DllImport("youme_voice_engine")]
		private static extern int youme_requestInviteMic(string pChannelID, string pUserID, string pContent);

		// Token: 0x0600312D RID: 12589
		[DllImport("youme_voice_engine")]
		private static extern int youme_responseInviteMic(string pUserID, bool isAccept, string pContent);

		// Token: 0x0600312E RID: 12590
		[DllImport("youme_voice_engine")]
		private static extern int youme_stopInviteMic();

		// Token: 0x0600312F RID: 12591
		[DllImport("youme_voice_engine")]
		private static extern int youme_sendMessage(string pChannelID, string pContent, ref int requestID);

		// Token: 0x06003130 RID: 12592 RVA: 0x001B0DA0 File Offset: 0x001AEFA0
		private void ParseJsonCallbackMessage(string strMessage)
		{
			string text = null;
			string text2 = null;
			JsonData jsonData = JsonMapper.ToObject(strMessage);
			switch ((int)jsonData["type"])
			{
			case 0:
			{
				text = "OnEvent";
				int num = (int)jsonData["event"];
				int num2 = (int)jsonData["error"];
				string text3 = (string)jsonData["channelid"];
				string text4 = (string)jsonData["param"];
				text2 = string.Concat(new string[]
				{
					string.Empty,
					num.ToString(),
					",",
					num2.ToString(),
					",",
					text3,
					",",
					text4
				});
				break;
			}
			case 1:
				text = "OnRequestRestApi";
				text2 = strMessage;
				break;
			case 2:
				text = "OnMemberChange";
				text2 = strMessage;
				break;
			case 3:
			{
				text = "OnBroadcast";
				int num3 = (int)jsonData["bc"];
				string text5 = (string)jsonData["channelid"];
				string text6 = (string)jsonData["param1"];
				string text7 = (string)jsonData["param2"];
				string text8 = (string)jsonData["content"];
				text2 = string.Concat(new string[]
				{
					string.Empty,
					num3.ToString(),
					",",
					text5,
					",",
					text6,
					",",
					text7,
					",",
					text8
				});
				break;
			}
			}
			bool flag = this.mCallbackObjName != null && text != null && text2 != null;
			if (flag)
			{
				GameObject gameObject = GameObject.Find(this.mCallbackObjName);
				bool flag2 = gameObject != null;
				if (flag2)
				{
					gameObject.SendMessage(text, text2);
				}
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x001B0F9C File Offset: 0x001AF19C
		public static YouMeVoiceAPI GetInstance()
		{
			bool flag = YouMeVoiceAPI.mInstance == null;
			if (flag)
			{
				YouMeVoiceAPI.mInstance = new YouMeVoiceAPI();
			}
			return YouMeVoiceAPI.mInstance;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x001B0FCB File Offset: 0x001AF1CB
		public void SetCallback(string strObjName)
		{
			this.mCallbackObjName = strObjName;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x001B0FD8 File Offset: 0x001AF1D8
		public YouMeErrorCode Init(string strAppKey, string strAPPSecret, YOUME_RTC_SERVER_REGION serverRegionId, string strExtServerRegionName)
		{
			GameObject gameObject = new GameObject("youme_update_once");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			gameObject.AddComponent<YouMeVoiceAPI.YoumeCallbackObject>();
			return (YouMeErrorCode)YouMeVoiceAPI.youme_init(strAppKey, strAPPSecret, (int)serverRegionId, strExtServerRegionName);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x001B1018 File Offset: 0x001AF218
		public YouMeErrorCode UnInit()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_unInit();
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x001B102F File Offset: 0x001AF22F
		public void SetServerRegion(YOUME_RTC_SERVER_REGION regionId, string strExtRegionName)
		{
			YouMeVoiceAPI.youme_setServerRegion((int)regionId, strExtRegionName, false);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x001B103C File Offset: 0x001AF23C
		public void SetServerRegion(string[] regionNames)
		{
			bool flag = regionNames != null && regionNames.Length != 0;
			if (flag)
			{
				YouMeVoiceAPI.youme_setServerRegion(10000, regionNames[0], false);
			}
			for (int i = 1; i < regionNames.Length; i++)
			{
				YouMeVoiceAPI.youme_setServerRegion(10000, regionNames[i], true);
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x001B1090 File Offset: 0x001AF290
		public YouMeErrorCode SetOutputToSpeaker(bool bOutputToSpeaker)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setOutputToSpeaker(bOutputToSpeaker);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x001B10A8 File Offset: 0x001AF2A8
		public void SetSpeakerMute(bool bMute)
		{
			YouMeVoiceAPI.youme_setSpeakerMute(bMute);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x001B10B4 File Offset: 0x001AF2B4
		public bool GetSpeakerMute()
		{
			return YouMeVoiceAPI.youme_getSpeakerMute();
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x001B10CB File Offset: 0x001AF2CB
		public void SetMicrophoneMute(bool mute)
		{
			YouMeVoiceAPI.youme_setMicrophoneMute(mute);
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x001B10D8 File Offset: 0x001AF2D8
		public bool GetMicrophoneMute()
		{
			return YouMeVoiceAPI.youme_getMicrophoneMute();
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x001B10EF File Offset: 0x001AF2EF
		public void SetAutoSendStatus(bool bAutoSend)
		{
			YouMeVoiceAPI.youme_setAutoSendStatus(bAutoSend);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x001B10F9 File Offset: 0x001AF2F9
		public void SetVolume(uint uiVolume)
		{
			YouMeVoiceAPI.youme_setVolume(uiVolume);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x001B1104 File Offset: 0x001AF304
		public int GetVolume()
		{
			return YouMeVoiceAPI.youme_getVolume();
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x001B111B File Offset: 0x001AF31B
		public void SetUseMobileNetworkEnabled(bool bEnabled)
		{
			YouMeVoiceAPI.youme_setUseMobileNetworkEnabled(bEnabled);
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x001B1128 File Offset: 0x001AF328
		public bool GetUseMobileNetworkEnabled()
		{
			return YouMeVoiceAPI.youme_getUseMobileNetworkEnabled();
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x001B1140 File Offset: 0x001AF340
		public YouMeErrorCode JoinChannelSingleMode(string strUserID, string strChannelID, int userRole)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_joinChannelSingleMode(strUserID, strChannelID, userRole);
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x001B115C File Offset: 0x001AF35C
		public YouMeErrorCode JoinChannelMultiMode(string strUserID, string strChannelID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_joinChannelMultiMode(strUserID, strChannelID);
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x001B1178 File Offset: 0x001AF378
		public YouMeErrorCode SpeakToChannel(string strChannelID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_speakToChannel(strChannelID);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x001B1190 File Offset: 0x001AF390
		public YouMeErrorCode LeaveChannelMultiMode(string strChannelID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_leaveChannelMultiMode(strChannelID);
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x001B11A8 File Offset: 0x001AF3A8
		public YouMeErrorCode LeaveChannelAll()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_leaveChannelAll();
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x001B11C0 File Offset: 0x001AF3C0
		public YouMeErrorCode SetOtherMicMute(string userID, bool mute)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setOtherMicMute(userID, mute);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x001B11DC File Offset: 0x001AF3DC
		public YouMeErrorCode SetOtherSpeakerMute(string userID, bool mute)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setOtherSpeakerMute(userID, mute);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x001B11F8 File Offset: 0x001AF3F8
		public YouMeErrorCode SetListenOtherVoice(string userID, bool isOn)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setListenOtherVoice(userID, isOn);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x001B1214 File Offset: 0x001AF414
		public YouMeErrorCode PlayBackgroundMusic(string strFilePath, bool bRepeat)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_playBackgroundMusic(strFilePath, bRepeat);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x001B1230 File Offset: 0x001AF430
		public YouMeErrorCode PauseBackgroundMusic()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_pauseBackgroundMusic();
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x001B1248 File Offset: 0x001AF448
		public YouMeErrorCode ResumeBackgroundMusic()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_resumeBackgroundMusic();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x001B1260 File Offset: 0x001AF460
		public YouMeErrorCode StopBackgroundMusic()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_stopBackgroundMusic();
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x001B1278 File Offset: 0x001AF478
		public YouMeErrorCode SetBackgroundMusicVolume(int volume)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setBackgroundMusicVolume(volume);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x001B1290 File Offset: 0x001AF490
		public YouMeErrorCode SetHeadsetMonitorOn(bool enabled)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setHeadsetMonitorOn(enabled);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x001B12A8 File Offset: 0x001AF4A8
		public YouMeErrorCode SetReverbEnabled(bool enabled)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setReverbEnabled(enabled);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x001B12C0 File Offset: 0x001AF4C0
		public YouMeErrorCode SetVadCallbackEnabled(bool enabled)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setVadCallbackEnabled(enabled);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x001B12D8 File Offset: 0x001AF4D8
		public YouMeErrorCode SetMicLevelCallback(int maxMicLevel)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setMicLevelCallback(maxMicLevel);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x001B12F0 File Offset: 0x001AF4F0
		public YouMeErrorCode PauseChannel()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_pauseChannel();
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x001B1308 File Offset: 0x001AF508
		public YouMeErrorCode ResumeChannel()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_resumeChannel();
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x001B131F File Offset: 0x001AF51F
		public void SetRecordingTimeMs(uint timeMs)
		{
			YouMeVoiceAPI.youme_setRecordingTimeMs(timeMs);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x001B1329 File Offset: 0x001AF529
		public void SetPlayingTimeMs(uint timeMs)
		{
			YouMeVoiceAPI.youme_setPlayingTimeMs(timeMs);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x001B1334 File Offset: 0x001AF534
		public int GetSDKVersion()
		{
			return YouMeVoiceAPI.youme_getSDKVersion();
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x001B134C File Offset: 0x001AF54C
		public YouMeErrorCode RequestRestApi(string command, string queryBody, ref int requestID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_requestRestApi(command, queryBody, ref requestID);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x001B1368 File Offset: 0x001AF568
		public YouMeErrorCode GetChannelUserList(string channelID, int maxCount, bool notifyMemChange)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_getChannelUserList(channelID, maxCount, notifyMemChange);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x001B1382 File Offset: 0x001AF582
		public void SetToken(string strToken)
		{
			YouMeVoiceAPI.youme_setToken(strToken);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x001B138C File Offset: 0x001AF58C
		public YouMeErrorCode SetReleaseMicWhenMute(bool enabled)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setReleaseMicWhenMute(enabled);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x001B13A4 File Offset: 0x001AF5A4
		public YouMeErrorCode SetGrabMicOption(string pChannelID, int mode, int maxAllowCount, int maxTalkTime, uint voteTime)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setGrabMicOption(pChannelID, mode, maxAllowCount, maxTalkTime, voteTime);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x001B13C4 File Offset: 0x001AF5C4
		public YouMeErrorCode StartGrabMicAction(string pChannelID, string pContent)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_startGrabMicAction(pChannelID, pContent);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x001B13E0 File Offset: 0x001AF5E0
		public YouMeErrorCode StopGrabMicAction(string pChannelID, string pContent)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_stopGrabMicAction(pChannelID, pContent);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x001B13FC File Offset: 0x001AF5FC
		public YouMeErrorCode requestGrabMic(string pChannelID, int score, bool isAutoOpenMic, string pContent)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_requestGrabMic(pChannelID, score, isAutoOpenMic, pContent);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x001B1418 File Offset: 0x001AF618
		public YouMeErrorCode releaseGrabMic(string pChannelID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_releaseGrabMic(pChannelID);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x001B1430 File Offset: 0x001AF630
		public YouMeErrorCode setInviteMicOption(string pChannelID, int waitTimeout, int maxTalkTime)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_setInviteMicOption(pChannelID, waitTimeout, maxTalkTime);
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x001B144C File Offset: 0x001AF64C
		public YouMeErrorCode requestInviteMic(string pChannelID, string pUserID, string pContent)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_requestInviteMic(pChannelID, pUserID, pContent);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x001B1468 File Offset: 0x001AF668
		public YouMeErrorCode responseInviteMic(string pUserID, bool isAccept, string pContent)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_responseInviteMic(pUserID, isAccept, pContent);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x001B1484 File Offset: 0x001AF684
		public YouMeErrorCode stopInviteMic()
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_stopInviteMic();
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x001B149C File Offset: 0x001AF69C
		public YouMeErrorCode SendMessage(string channelID, string content, ref int requestID)
		{
			return (YouMeErrorCode)YouMeVoiceAPI.youme_sendMessage(channelID, content, ref requestID);
		}

		// Token: 0x04002A87 RID: 10887
		private static YouMeVoiceAPI mInstance;

		// Token: 0x04002A88 RID: 10888
		private string mCallbackObjName;

		// Token: 0x0200069C RID: 1692
		private enum CallbackType
		{
			// Token: 0x04003155 RID: 12629
			CALLBACK_TYPE_EVENT,
			// Token: 0x04003156 RID: 12630
			CALLBACK_TYPE_REST_API_RESPONSE,
			// Token: 0x04003157 RID: 12631
			CALLBACK_TYPE_MEMBER_CHANGE,
			// Token: 0x04003158 RID: 12632
			CALLBACK_TYPE_BROADCAST
		}

		// Token: 0x0200069D RID: 1693
		private class YoumeCallbackObject : MonoBehaviour
		{
			// Token: 0x0600399F RID: 14751 RVA: 0x001C8D84 File Offset: 0x001C6F84
			private void Start()
			{
				base.InvokeRepeating("YoumeCallback", 0.5f, 0.05f);
			}

			// Token: 0x060039A0 RID: 14752 RVA: 0x001C8DA0 File Offset: 0x001C6FA0
			private void YoumeCallback()
			{
				IntPtr intPtr = YouMeVoiceAPI.youme_getCbMessage();
				bool flag = intPtr == IntPtr.Zero;
				if (!flag)
				{
					string text = Marshal.PtrToStringAuto(intPtr);
					bool flag2 = text != null;
					if (flag2)
					{
						try
						{
							YouMeVoiceAPI.GetInstance().ParseJsonCallbackMessage(text);
						}
						catch (Exception ex)
						{
							Debug.LogError(ex.StackTrace);
						}
					}
					YouMeVoiceAPI.youme_freeCbMessage(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
		}
	}
}
