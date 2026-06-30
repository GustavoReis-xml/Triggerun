using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class SkillInfo : MonoBehaviour
{
	// Token: 0x0600270F RID: 9999 RVA: 0x00161634 File Offset: 0x0015F834
	public void InitSkills(NetLiving liv, List<int> list)
	{
		this.skillsinfoDict = new Dictionary<int, SkillStruct>();
		this.NetObject = liv;
		for (int i = 0; i < list.Count; i++)
		{
			int num = list[i];
			SkillStruct skillStruct = new SkillStruct();
			bool flag = !fGameManager.mloadConfig.AllPlayerSkill.ContainsKey(num);
			if (flag)
			{
				Debug.LogError(string.Format("配表没这个技能:{0}", num));
				break;
			}
			skillStruct.idx = num;
			skillStruct._data = fGameManager.mloadConfig.AllPlayerSkill[num];
			this.skillsinfoDict.Add(num, skillStruct);
			this.InitSound(skillStruct);
			this.InitReayItem(skillStruct);
		}
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x001616EC File Offset: 0x0015F8EC
	public void SkillAnimation(int idx)
	{
		bool flag = this.skillsinfoDict.ContainsKey(idx) && this.skillsinfoDict[idx]._data.animation.Length > 1;
		if (flag)
		{
			this.NetObject.SetAnimator(this.skillsinfoDict[idx]._data.animation, 0, true, 1);
		}
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x0016175C File Offset: 0x0015F95C
	public void SkillTalk(int idx, bool isFriend)
	{
		bool flag = !this.skillsinfoDict.ContainsKey(idx) || (!this.NetObject.isCopy && CFilmManager.Instance.playing) || UnityEngine.Random.value >= this.skillsinfoDict[idx]._data.soundRand;
		if (!flag)
		{
			if (isFriend)
			{
				bool flag2 = this.skillsinfoDict[idx].friend != null;
				if (flag2)
				{
					this.skillsinfoDict[idx].friend.Play();
					this.NetObject.playerSound.ExeptStopTalkSounds(this.skillsinfoDict[idx].friend);
				}
			}
			else
			{
				bool flag3 = this.skillsinfoDict[idx].enemy != null;
				if (flag3)
				{
					this.skillsinfoDict[idx].enemy.Play();
					this.NetObject.playerSound.ExeptStopTalkSounds(this.skillsinfoDict[idx].enemy);
				}
			}
		}
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x00161874 File Offset: 0x0015FA74
	public void SkillTalkStopAll()
	{
		foreach (SkillStruct skillStruct in this.skillsinfoDict.Values)
		{
			bool flag = skillStruct.friend != null;
			if (flag)
			{
				skillStruct.friend.Stop();
			}
			bool flag2 = skillStruct.enemy != null;
			if (flag2)
			{
				skillStruct.enemy.Stop();
			}
		}
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x00161908 File Offset: 0x0015FB08
	private void InitReayItem(SkillStruct stru)
	{
		bool flag = this.NetObject.isMainPlayer && stru._data.readyItem != "0";
		if (flag)
		{
			string url = "EffectRes/" + stru._data.readyItem + ".ef";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayItemEffectPrefab), stru.idx);
			}
			else
			{
				this.loadReayItemEffectPrefab(resWWW, stru.idx);
			}
		}
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x001619A4 File Offset: 0x0015FBA4
	public void DisableReadyItem(int idx)
	{
		bool flag = this.skillsinfoDict.ContainsKey(idx) && !(this.skillsinfoDict[idx].readyItem == null) && this.skillsinfoDict[idx].readyItem.gameObject.activeSelf;
		if (flag)
		{
			this.skillsinfoDict[idx].readyItem.gameObject.SetActive(false);
			bool flag2 = this.skillsinfoDict[idx].readyItem.ThisGameObjTrans != null;
			if (flag2)
			{
				this.skillsinfoDict[idx].readyItem.ThisGameObjTrans.SetActive(false);
			}
		}
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x00161A5C File Offset: 0x0015FC5C
	public void DestroyReadyItem(int idx)
	{
		bool flag = this.skillsinfoDict.ContainsKey(idx) && !(this.skillsinfoDict[idx].readyItem == null);
		if (flag)
		{
			this.skillsinfoDict[idx].readyItem.changeHeroDes = true;
			UnityEngine.Object.Destroy(this.skillsinfoDict[idx].readyItem.gameObject);
			bool flag2 = this.skillsinfoDict[idx].readyItem.ThisGameObjTrans != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.skillsinfoDict[idx].readyItem.ThisGameObjTrans);
			}
		}
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x00161B0C File Offset: 0x0015FD0C
	public void ShowReadyItem(int idx)
	{
		bool flag = this.skillsinfoDict.ContainsKey(idx) && !(this.skillsinfoDict[idx].readyItem == null) && !this.skillsinfoDict[idx].readyItem.gameObject.activeSelf;
		if (flag)
		{
			this.skillsinfoDict[idx].readyItem.gameObject.SetActive(true);
			bool flag2 = this.skillsinfoDict[idx].readyItem.ThisGameObjTrans != null;
			if (flag2)
			{
				this.skillsinfoDict[idx].readyItem.ThisGameObjTrans.SetActive(true);
			}
		}
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x00161BC4 File Offset: 0x0015FDC4
	public CPlaceItem getReadyItem(int idx)
	{
		bool flag = !this.skillsinfoDict.ContainsKey(idx);
		CPlaceItem result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.skillsinfoDict[idx].readyItem;
		}
		return result;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x00161C00 File Offset: 0x0015FE00
	public int getReadyItemKeyUpOrDown(int idx)
	{
		bool flag = !this.skillsinfoDict.ContainsKey(idx);
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.skillsinfoDict[idx]._data.readySwitch;
		}
		return result;
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x00161C40 File Offset: 0x0015FE40
	private void loadReayItemEffectPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			int key = (int)sStyle;
			bool flag2 = this.skillsinfoDict.ContainsKey(key) && this.NetObject != null && !this.NetObject.haveDestroy && !(this.NetObject._playerSoundContainer == null);
			if (flag2)
			{
				GameObject gameObject = new GameObject("placeItem");
				gameObject.transform.SetParent(this.NetObject._playerSoundContainer.transform, false);
				gameObject.SetActive(false);
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), gameObject.transform.position, Quaternion.identity);
				this.skillsinfoDict[key].readyItem = gameObject.AddComponent<CPlaceItem>();
				this.skillsinfoDict[key].readyItem.net = this.NetObject;
				this.skillsinfoDict[key].readyItem.PrepareItem = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]), gameObject.transform.position, Quaternion.identity);
				this.skillsinfoDict[key].readyItem.PrepareItem.SetActive(false);
				this.skillsinfoDict[key].readyItem.PrepareItem.transform.SetParent(this.NetObject._playerSoundContainer.transform, true);
				this.skillsinfoDict[key].readyItem.InitPlace(gameObject2.transform, this.skillsinfoDict[key]._data.readyItemType, this.skillsinfoDict[key]._data.taregetradius);
			}
		}
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x00161E28 File Offset: 0x00160028
	private void InitSound(SkillStruct stru)
	{
		int num = CFilmManager.Instance.filmForce(this.NetObject);
		bool flag = num == this.NetObject.playerCommon.m_nShiliServer;
		bool flag2 = flag && stru._data.soundFriend != "0";
		if (flag2)
		{
			string url = "SoundRes/" + stru._data.soundFriend + ".sds";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag3 = resWWW == null;
			if (flag3)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadSoundFriendPrefab), stru.idx);
			}
			else
			{
				this.loadSoundFriendPrefab(resWWW, stru.idx);
			}
		}
		bool flag4 = !flag && stru._data.soundEnemy != "0";
		if (flag4)
		{
			string url2 = "SoundRes/" + stru._data.soundEnemy + ".sds";
			WWW resWWW2 = GameRoot.GetResWWW(url2);
			bool flag5 = resWWW2 == null;
			if (flag5)
			{
				GameRoot.LoadAsset(url2, new fResourceManager.DownFinishDelegate(this.loadSoundEnemyPrefab), stru.idx);
			}
			else
			{
				this.loadSoundEnemyPrefab(resWWW2, stru.idx);
			}
		}
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x00161F74 File Offset: 0x00160174
	private void loadSoundFriendPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null && this.NetObject != null && !this.NetObject.haveDestroy;
		if (flag)
		{
			int key = (int)sStyle;
			bool flag2 = this.skillsinfoDict.ContainsKey(key);
			if (flag2)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.SetParent(this.NetObject._playerSoundContainer.transform, false);
				this.skillsinfoDict[key].friend = component;
				CSoundManager.Instance.SetTalkSoundMixer(this.skillsinfoDict[key].friend, 0, this.NetObject);
			}
		}
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x0016205C File Offset: 0x0016025C
	private void loadSoundEnemyPrefab(WWW www, object sStyle)
	{
		bool flag = this.NetObject != null && !this.NetObject.haveDestroy;
		if (flag)
		{
			int key = (int)sStyle;
			bool flag2 = this.skillsinfoDict.ContainsKey(key) && this.NetObject != null && !this.NetObject.haveDestroy;
			if (flag2)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]));
				AudioSource component = gameObject.GetComponent<AudioSource>();
				component.playOnAwake = false;
				component.Stop();
				component.transform.SetParent(this.NetObject._playerSoundContainer.transform, false);
				this.skillsinfoDict[key].enemy = component;
				CSoundManager.Instance.SetTalkSoundMixer(this.skillsinfoDict[key].enemy, 0, this.NetObject);
			}
		}
	}

	// Token: 0x04002254 RID: 8788
	private Dictionary<int, SkillStruct> skillsinfoDict = new Dictionary<int, SkillStruct>();

	// Token: 0x04002255 RID: 8789
	private NetLiving NetObject;
}
