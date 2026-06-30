using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000497 RID: 1175
internal class WarPersonManager : MonoBehaviour
{
	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x06002D76 RID: 11638 RVA: 0x0018FF30 File Offset: 0x0018E130
	// (set) Token: 0x06002D77 RID: 11639 RVA: 0x0018FF6C File Offset: 0x0018E16C
	public static WarPersonManager Instance
	{
		get
		{
			bool flag = WarPersonManager._instance == null;
			if (flag)
			{
				WarPersonManager._instance = GameRoot.inst.gameObject.AddComponent<WarPersonManager>();
			}
			return WarPersonManager._instance;
		}
		set
		{
			WarPersonManager._instance = value;
		}
	}

	// Token: 0x06002D78 RID: 11640 RVA: 0x0018FF78 File Offset: 0x0018E178
	public void ClientMsg(int nNetID, ArrayList szMsg, NetLiving _net)
	{
		if (nNetID != 19701)
		{
			if (nNetID == 19703)
			{
				WarPersonManager.Instance.WarPersonEnd(szMsg);
			}
		}
		else
		{
			WarPersonManager.Instance.WarInitData(szMsg);
		}
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x0018FFBC File Offset: 0x0018E1BC
	public void Killed(NetLiving killer, NetLiving deader)
	{
		bool flag = killer.playerCommon.aryWarData.Count != 0;
		if (flag)
		{
			this.KillNumberCheck((int)killer.playerCommon.aryWarData[0]);
			bool flag2 = killer.getNetid() == fGameManager.Heroplayer.getNetid();
			if (flag2)
			{
				this.PlayJiFenSound();
			}
		}
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x0019001F File Offset: 0x0018E21F
	public void HeroInto()
	{
		this.WarPersonInit();
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x00190029 File Offset: 0x0018E229
	public void PersonChange()
	{
		this.fChange = Time.time + 0.5f;
		this.bChange = true;
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x00190044 File Offset: 0x0018E244
	public void UpdateChange()
	{
		this.nets = new List<NetLiving>();
		List<UserData> list = new List<UserData>(MatchManager.GetInstance().getAllUserData().Values);
		for (int i = 0; i < list.Count; i++)
		{
			UserData userData = list[i];
			bool flag = userData != null && userData.netLiving != null && userData.m_nIsChsok != 0;
			if (flag)
			{
				this.nets.Add(userData.netLiving);
			}
		}
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x001900C2 File Offset: 0x0018E2C2
	public void Closed()
	{
		this.nets.Clear();
		this.bTabPerson = false;
		this.bChange = false;
		UnityEngine.Object.DestroyObject(this.soundWarParent);
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x001900F4 File Offset: 0x0018E2F4
	public void WarInitData(ArrayList data)
	{
		this.huiheTime = (int)data[0];
		this.needKill = (int)data[1];
		this.killGaochao = this.needKill - 3;
		this.passSec = (float)this.huiheTime - 30f;
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x00190148 File Offset: 0x0018E348
	private void WarPersonInit()
	{
		bool flag = !this.isInit && !this.bShowEnd;
		if (flag)
		{
			WarPersonManager._instance.soundWarParent = new GameObject("_warSounds");
			Door_Control.Get_Instantiate.Delete_All_Door();
			WarPersonManager._instance.LoadGaochaoSound();
			WarPersonManager._instance.LoadJiFenSound();
			WarPersonManager._instance.LoadEndSound();
			WarPersonManager._instance.LoadMeYoushiSound();
			WarPersonManager._instance.LoadMeLueshiSound();
			WarPersonManager._instance.LoadListSound();
			bool flag2 = PersonTabCon.Inst == null;
			if (flag2)
			{
				fUIManager.GetInstance().ShowWin("PersonTab", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.openPersonTabUiLoadOK), null, true);
			}
			bool flag3 = PersonWarCon.Inst == null;
			if (flag3)
			{
				fUIManager.GetInstance().ShowWin("PersonWar", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.openPersonMainUiLoadOK), null, true);
			}
			this.isInit = true;
		}
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x0019023C File Offset: 0x0018E43C
	private void WarPersonEnd(ArrayList _data)
	{
		fGameManager.Heroplayer.canControl = false;
		bool flag = (int)_data[0] == 1;
		if (flag)
		{
			this.sWiner = Fun.DecodingUTF8((string)_data[1]) + " " + Language.GetLang(1109, new object[0]);
		}
		else
		{
			this.sWiner = Language.GetLang(1108, new object[0]);
		}
		PersonWarCon.Inst.SetWinder(this.sWiner);
		this.StopGaochaoSound();
		this.PlayEndSound();
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x001902D4 File Offset: 0x0018E4D4
	private void Update()
	{
		bool flag = this.bIsOver || Time.deltaTime == 0f;
		if (!flag)
		{
			int mynum = 0;
			bool flag2 = this.lastNumber != MatchManager.GetInstance().getAllUserData().Count || this.nets.Count != MatchManager.GetInstance().getAllUserData().Count;
			if (flag2)
			{
				this.lastNumber = MatchManager.GetInstance().getAllUserData().Count;
				this.bChange = true;
			}
			bool flag3 = !this.bShowEnd;
			if (flag3)
			{
				this.CheckTabUI();
				bool flag4 = this.bChange && Time.time > this.fChange;
				if (flag4)
				{
					this.bChange = false;
					this.UpdateChange();
				}
			}
			bool flag5 = this.bShowEnd || Time.time > this.timeOut;
			if (flag5)
			{
				this.timeOut = Time.time + 1f;
				bool flag6 = this.nets.Count == 0;
				if (flag6)
				{
					return;
				}
				this.nets.Sort(new PersonListDataSort());
				for (int i = 0; i < this.nets.Count; i++)
				{
					bool flag7 = this.nets[i] == fGameManager.Heroplayer;
					if (flag7)
					{
						mynum = i;
						break;
					}
				}
				bool flag8 = this.bTabPerson && PersonTabCon.Inst != null;
				if (flag8)
				{
					PersonTabCon.Inst.UpdateUI(this.nets);
				}
				bool flag9 = PersonWarCon.Inst != null;
				if (flag9)
				{
					PersonWarCon.Inst.UpdateUI(mynum, this.nets);
				}
				bool flag10 = PersonEndCon.Inst != null;
				if (flag10)
				{
					PersonEndCon.Inst.UpdateUI(this.nets);
					this.bIsOver = true;
				}
			}
			bool flag11 = !this.gaoChaoSoundPlayed && (float)this.getNowGameTimeLong() > this.passSec;
			if (flag11)
			{
				this.gaoChaoSoundPlayed = true;
				this.PlayGaochaoSound();
			}
		}
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x001904F4 File Offset: 0x0018E6F4
	public void KillNumberCheck(int num)
	{
		bool flag = !this.gaoChaoSoundPlayed && num >= this.killGaochao;
		if (flag)
		{
			this.gaoChaoSoundPlayed = true;
			this.PlayGaochaoSound();
		}
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x00190530 File Offset: 0x0018E730
	public void No1KillNumber(NetLiving netNo1, int num)
	{
		bool flag = num > 0 && netNo1.getNetid() != this.no1Id;
		if (flag)
		{
			this.no1Id = netNo1.getNetid();
			bool flag2 = netNo1 == fGameManager.Heroplayer;
			if (flag2)
			{
				this.PlayMeYoushiSound();
			}
			else
			{
				this.PlayMeLueshiSound();
			}
		}
		int num2 = this.needKill - num;
		string k = string.Empty;
		int num3 = num2;
		int num4 = num3;
		switch (num4)
		{
		case 1:
		{
			PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1699];
			k = preloadData.sUrl;
			goto IL_191;
		}
		case 2:
		{
			PreloadData preloadData2 = fGameManager.mloadConfig.AllProLodaList[1702];
			k = preloadData2.sUrl;
			goto IL_191;
		}
		case 3:
		{
			PreloadData preloadData3 = fGameManager.mloadConfig.AllProLodaList[1703];
			k = preloadData3.sUrl;
			goto IL_191;
		}
		case 4:
		{
			PreloadData preloadData4 = fGameManager.mloadConfig.AllProLodaList[1704];
			k = preloadData4.sUrl;
			goto IL_191;
		}
		case 5:
		{
			PreloadData preloadData5 = fGameManager.mloadConfig.AllProLodaList[1705];
			k = preloadData5.sUrl;
			goto IL_191;
		}
		case 6:
		case 7:
		case 8:
		case 9:
			break;
		case 10:
		{
			PreloadData preloadData6 = fGameManager.mloadConfig.AllProLodaList[1700];
			k = preloadData6.sUrl;
			goto IL_191;
		}
		default:
			if (num4 == 15)
			{
				PreloadData preloadData7 = fGameManager.mloadConfig.AllProLodaList[1701];
				k = preloadData7.sUrl;
				goto IL_191;
			}
			break;
		}
		return;
		IL_191:
		base.StopCoroutine("playNumberList");
		bool flag3 = netNo1 == fGameManager.Heroplayer;
		if (flag3)
		{
			base.StartCoroutine(this.playNumberList(true, k));
		}
		else
		{
			base.StartCoroutine(this.playNumberList(false, k));
		}
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x0019070C File Offset: 0x0018E90C
	private IEnumerator playNumberList(bool isMe, string k)
	{
		bool flag = this._listSound == null;
		if (flag)
		{
			yield break;
		}
		bool flag2 = !isMe;
		if (flag2)
		{
			PreloadData data = fGameManager.mloadConfig.AllProLodaList[1706];
			this.PlayListSound(data.sUrl);
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				bool flag3 = this._listSound == null || !this._listSound.ContainsKey(data.sUrl);
				if (flag3)
				{
					break;
				}
				if (!this._listSound[data.sUrl].isPlaying)
				{
					goto Block_5;
				}
			}
			yield break;
			Block_5:
			data = null;
		}
		this.PlayListSound(k);
		yield break;
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x00190729 File Offset: 0x0018E929
	public void ShowEnd()
	{
		this.bShowEnd = true;
		this.CloseTabWin();
		this.UpdateChange();
	}

	// Token: 0x06002D86 RID: 11654 RVA: 0x00190744 File Offset: 0x0018E944
	private void CheckTabUI()
	{
		bool flag = Input_Manager.Get_Button_Down("Score_Window");
		if (flag)
		{
			this.OpenTableUI();
		}
		bool flag2 = Input_Manager.Get_Button_Up("Score_Window");
		if (flag2)
		{
			this.CloseTabWin();
		}
		bool flag3 = !HeroPlayer.isPlayerControl;
		if (flag3)
		{
			this.CloseTabWin();
		}
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x00190798 File Offset: 0x0018E998
	public void OpenTableUI()
	{
		bool flag = this.bShowEnd;
		if (!flag)
		{
			this.bTabPerson = true;
			bool flag2 = PersonTabCon.Inst == null;
			if (flag2)
			{
				fUIManager.GetInstance().ShowWin("PersonTab", NetMsg.UITRANSFROM.GameUI, new fUIManager.UILoadFinishDelegate(this.openPersonTabUiLoadOK), null, true);
			}
			else
			{
				bool flag3 = !PersonTabCon.Inst.visible;
				if (flag3)
				{
					bool flag4 = this.bTabPerson && PersonTabCon.Inst != null;
					if (flag4)
					{
						PersonTabCon.Inst.UpdateUI(this.nets);
					}
					PersonTabCon.Inst.Open();
				}
			}
		}
	}

	// Token: 0x06002D88 RID: 11656 RVA: 0x0019083C File Offset: 0x0018EA3C
	private void openPersonTabUiLoadOK(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = !this.bShowEnd;
		if (flag)
		{
			bool flag2 = !this.bTabPerson || fGameManager.GetInstance().nNowScnID == 0;
			if (flag2)
			{
				PersonTabCon.Inst.Close();
			}
			else
			{
				PersonTabCon.Inst.Open();
			}
		}
	}

	// Token: 0x06002D89 RID: 11657 RVA: 0x00190894 File Offset: 0x0018EA94
	public void CloseTabWin()
	{
		bool flag = this.bTabPerson;
		if (flag)
		{
			this.bTabPerson = false;
			bool flag2 = PersonTabCon.Inst != null && PersonTabCon.Inst.visible;
			if (flag2)
			{
				PersonTabCon.Inst.Close();
			}
		}
	}

	// Token: 0x06002D8A RID: 11658 RVA: 0x001908E0 File Offset: 0x0018EAE0
	private void openPersonMainUiLoadOK(string sUI, GameObject UIObj, object szParam)
	{
		bool flag = !this.bShowEnd;
		if (flag)
		{
			bool flag2 = fGameManager.GetInstance().nNowScnID == 0;
			if (flag2)
			{
				PersonWarCon.Inst.Close();
			}
			else
			{
				PersonWarCon.Inst.Open();
			}
		}
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x0019092C File Offset: 0x0018EB2C
	private int getNowGameTimeLong()
	{
		DateTime dGameStartTime = MatchManager.GetInstance().m_dGameStartTime;
		return (int)(Fun.GetServerDate() - dGameStartTime).TotalSeconds;
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x00190960 File Offset: 0x0018EB60
	public void LoadGaochaoSound()
	{
		string text = "bisai_gaochaoyinyue";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadGaochaoSoundPrefab), 0);
		}
		else
		{
			this.LoadGaochaoSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x001909C4 File Offset: 0x0018EBC4
	private void LoadGaochaoSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			this._soundGaochao = component;
			CSoundManager.Instance.SetSceneSoundMixer(component);
		}
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x00190A5C File Offset: 0x0018EC5C
	public void PlayGaochaoSound()
	{
		bool flag = !(this._soundGaochao == null) && !this._soundGaochao.isPlaying;
		if (flag)
		{
			this._soundGaochao.Play();
		}
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x00190A9C File Offset: 0x0018EC9C
	public void StopGaochaoSound()
	{
		bool flag = !(this._soundGaochao == null);
		if (flag)
		{
			this._soundGaochao.Stop();
		}
	}

	// Token: 0x06002D90 RID: 11664 RVA: 0x00190ACC File Offset: 0x0018ECCC
	public void LoadJiFenSound()
	{
		string text = "sidousolo_jiafen";
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadJiFenSoundPrefab), 0);
		}
		else
		{
			this.LoadJiFenSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002D91 RID: 11665 RVA: 0x00190B30 File Offset: 0x0018ED30
	private void LoadJiFenSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			this._soundJifen = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, fGameManager.Heroplayer, 20);
		}
	}

	// Token: 0x06002D92 RID: 11666 RVA: 0x00190BD0 File Offset: 0x0018EDD0
	public void PlayJiFenSound()
	{
		bool flag = !(this._soundJifen == null);
		if (flag)
		{
			this._soundJifen.Play();
		}
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x00190C00 File Offset: 0x0018EE00
	public void LoadEndSound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1709];
		string text = preloadData.sUrl;
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadEndSoundPrefab), 0);
		}
		else
		{
			this.LoadEndSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002D94 RID: 11668 RVA: 0x00190C78 File Offset: 0x0018EE78
	private void LoadEndSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			this._soundEnd = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, fGameManager.Heroplayer, 20);
		}
	}

	// Token: 0x06002D95 RID: 11669 RVA: 0x00190D18 File Offset: 0x0018EF18
	public void PlayEndSound()
	{
		bool flag = !(this._soundEnd == null);
		if (flag)
		{
			this._soundEnd.Play();
		}
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x00190D48 File Offset: 0x0018EF48
	public void LoadMeYoushiSound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1707];
		string text = preloadData.sUrl;
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadMeYoushiSoundPrefab), 0);
		}
		else
		{
			this.LoadMeYoushiSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x00190DC0 File Offset: 0x0018EFC0
	private void LoadMeYoushiSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			this._MeYoushi = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, fGameManager.Heroplayer, 20);
		}
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x00190E60 File Offset: 0x0018F060
	public void PlayMeYoushiSound()
	{
		bool flag = !(this._MeYoushi == null);
		if (flag)
		{
			this._MeYoushi.Play();
		}
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x00190E90 File Offset: 0x0018F090
	public void LoadMeLueshiSound()
	{
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1710];
		string text = preloadData.sUrl;
		text = "SoundRes/" + text + ".sds";
		WWW resWWW = GameRoot.GetResWWW(text);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(text, new fResourceManager.DownFinishDelegate(this.LoadMeLueshiSoundPrefab), 0);
		}
		else
		{
			this.LoadMeLueshiSoundPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002D9A RID: 11674 RVA: 0x00190F08 File Offset: 0x0018F108
	private void LoadMeLueshiSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.Stop();
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			this._MeLueshi = component;
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, fGameManager.Heroplayer, 20);
		}
	}

	// Token: 0x06002D9B RID: 11675 RVA: 0x00190FA8 File Offset: 0x0018F1A8
	public void PlayMeLueshiSound()
	{
		bool flag = !(this._MeLueshi == null);
		if (flag)
		{
			this._MeLueshi.Play();
		}
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x00190FD8 File Offset: 0x0018F1D8
	private void LoadListSound()
	{
		this._listSound = new Dictionary<string, AudioSource>();
		ArrayList arrayList = new ArrayList();
		PreloadData preloadData = fGameManager.mloadConfig.AllProLodaList[1706];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1699];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1702];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1703];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1704];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1705];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1700];
		arrayList.Add(preloadData.sUrl);
		preloadData = fGameManager.mloadConfig.AllProLodaList[1701];
		arrayList.Add(preloadData.sUrl);
		for (int i = 0; i < arrayList.Count; i++)
		{
			string url = "SoundRes/" + (string)arrayList[i] + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag = resWWW == null;
			if (flag)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadListSoundPrefab), (string)arrayList[i]);
			}
			else
			{
				this.LoadListSoundPrefab(resWWW, (string)arrayList[i]);
			}
		}
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x0019118C File Offset: 0x0018F38C
	private void LoadListSoundPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			string key = (string)sStyle;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.playOnAwake = false;
			component.transform.parent = this.soundWarParent.transform;
			component.transform.localPosition = Vector3.zero;
			bool flag2 = !this._listSound.ContainsKey(key);
			if (flag2)
			{
				this._listSound.Add(key, component);
			}
			CSoundManager.Instance.SetEffectSoundMixer(component, 0, fGameManager.Heroplayer, 20);
		}
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x0019124C File Offset: 0x0018F44C
	public void PlayListSound(string foot)
	{
		bool flag = this._listSound.Count != 0 && this._listSound.ContainsKey(foot);
		if (flag)
		{
			this._listSound[foot].Play();
		}
	}

	// Token: 0x04002686 RID: 9862
	public int huiheTime = 5940;

	// Token: 0x04002687 RID: 9863
	public int needKill = 20;

	// Token: 0x04002688 RID: 9864
	private float passSec = 570f;

	// Token: 0x04002689 RID: 9865
	public int killGaochao = 17;

	// Token: 0x0400268A RID: 9866
	private bool isInit;

	// Token: 0x0400268B RID: 9867
	private bool bShowEnd;

	// Token: 0x0400268C RID: 9868
	private bool bIsOver;

	// Token: 0x0400268D RID: 9869
	private bool bTabPerson;

	// Token: 0x0400268E RID: 9870
	private List<NetLiving> nets = new List<NetLiving>();

	// Token: 0x0400268F RID: 9871
	private GameObject soundWarParent;

	// Token: 0x04002690 RID: 9872
	private AudioSource _soundGaochao;

	// Token: 0x04002691 RID: 9873
	private bool gaoChaoSoundPlayed;

	// Token: 0x04002692 RID: 9874
	private AudioSource _soundJifen;

	// Token: 0x04002693 RID: 9875
	private AudioSource _soundEnd;

	// Token: 0x04002694 RID: 9876
	private AudioSource _MeYoushi;

	// Token: 0x04002695 RID: 9877
	private AudioSource _MeLueshi;

	// Token: 0x04002696 RID: 9878
	public string sWiner = string.Empty;

	// Token: 0x04002697 RID: 9879
	private Dictionary<string, AudioSource> _listSound = new Dictionary<string, AudioSource>();

	// Token: 0x04002698 RID: 9880
	public int lastTopNo;

	// Token: 0x04002699 RID: 9881
	public int no1Id = -1;

	// Token: 0x0400269A RID: 9882
	private static WarPersonManager _instance;

	// Token: 0x0400269B RID: 9883
	private float fChange;

	// Token: 0x0400269C RID: 9884
	private bool bChange;

	// Token: 0x0400269D RID: 9885
	private int lastNumber = -1;

	// Token: 0x0400269E RID: 9886
	private float timeOut;
}
