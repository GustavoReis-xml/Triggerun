using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025A RID: 602
public class NewPlayerTopUICon : MonoBehaviour
{
	// Token: 0x06001B6D RID: 7021 RVA: 0x001030E4 File Offset: 0x001012E4
	private void Awake()
	{
		this._showUi = base.transform.FindChild("show").gameObject;
		this.showUiTransform = this._showUi.GetComponent<Transform>();
		this.topNameObj = this._showUi.transform.FindChild("top").gameObject;
		this.lzname = this.topNameObj.transform.Find("name").GetComponent<MeshRenderer>();
		this.lzname.enabled = false;
		this._showUi.transform.FindChild("hp").gameObject.SetActive(false);
		this.hpObj = this._showUi.transform.FindChild("newhp").gameObject;
		this.hpcon = this.hpObj.AddComponent<HpNewControl>();
		this._hpParent = this.hpObj;
		this.ui_jiantou = this._showUi.transform.FindChild("ui_jiantou_bai").gameObject;
		this.jiantouhuang = this.ui_jiantou.transform.Find("huang").gameObject;
		this.jiantoulan = this.ui_jiantou.transform.Find("lan").gameObject;
		this.clonebuff = this._showUi.transform.FindChild("clonebuff").gameObject;
		this.clonebuff.SetActive(false);
		this.bufflist = this._showUi.transform.FindChild("bufflist").gameObject;
		this.bufflist.SetActive(false);
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x00103284 File Offset: 0x00101484
	public void setUI(NetLiving gPlayer)
	{
		this.gameplayer = gPlayer;
		bool isCopy = this.gameplayer.isCopy;
		if (isCopy)
		{
			bool flag = gPlayer.nForce == CFilmManager.Instance.playerForce;
			if (flag)
			{
				this.IsEnemy = false;
			}
			else
			{
				this.IsEnemy = true;
			}
		}
		else
		{
			bool flag2 = fGameManager.Heroplayer.playerCommon.m_nShili == this.gameplayer.playerCommon.m_nShili;
			if (flag2)
			{
				this.IsEnemy = false;
			}
			else
			{
				this.IsEnemy = true;
			}
		}
		this.nOldHp = 0;
		CCOpyNameManager.Instance.GetName(gPlayer);
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x00103324 File Offset: 0x00101524
	private void Update()
	{
		bool flag = this._showUi == null;
		if (!flag)
		{
			bool flag2 = this.gameplayer == null || !this.gameplayer.playerHaveCreate || this.gameplayer.haveDestroy || this.gameplayer.thisModel == null || !this.gameplayer.mModelTopObjbool || CameraControl.Instance == null || CameraControl.Instance.controlCamera == null || (GameRoot.inst.Setting_FPS != null && GameRoot.inst.Setting_FPS.GM_State) || fGameManager.Heroplayer == null;
			if (flag2)
			{
				bool activeSelf = this._showUi.activeSelf;
				if (activeSelf)
				{
					this._showUi.SetActive(false);
				}
			}
			else
			{
				bool flag3 = !fUIManager.mUIObj.activeSelf;
				if (flag3)
				{
					bool activeSelf2 = this._showUi.activeSelf;
					if (activeSelf2)
					{
						this._showUi.SetActive(false);
					}
				}
				else
				{
					bool flag4 = !this._showUi.activeSelf;
					if (flag4)
					{
						this._showUi.SetActive(true);
					}
					bool flag5 = this.CheckPlayerDead();
					if (!flag5)
					{
						bool flag6 = !this._showUi.activeSelf;
						if (flag6)
						{
							this._showUi.SetActive(true);
						}
						Vector3 position = base.transform.position;
						Vector3 position2 = CameraControl.Instance.controlCamera.transform.position;
						bool flag7 = fGameManager.Heroplayer.playerCommon.m_nGM > 0 && fGameManager.Heroplayer.playerCommon.m_szGMTran.Count > 0 && GameRoot.inst != null && GameRoot.inst.Setting_FPS.GM_Camera_Component != null && GameRoot.inst.Setting_FPS.GM_Camera_Component.enabled;
						if (flag7)
						{
							position2 = GameRoot.inst.Setting_FPS.GM_Camera_Component.transform.position;
						}
						float num = Vector3.Distance(position, position2);
						bool flag8 = num > 0.001f;
						if (flag8)
						{
							num = (float)((int)(num * 100f)) / 100f;
							this.CheckHeadActive(num);
							bool flag9 = !this.GetActiveStat();
							if (flag9)
							{
								this.CheckRotaion(position, position2);
								this.CheckHeadPos();
								this.CheckHp();
								this.CheckSize(num);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x001035A0 File Offset: 0x001017A0
	private void CheckSize(float fDis)
	{
		bool flag = fDis > 40f;
		if (flag)
		{
			fDis = 40f;
		}
		bool cameraZoom = CameraControl.Instance.CameraZoom;
		if (cameraZoom)
		{
			fDis = CameraControl.Instance.controlCamera.fieldOfView * fDis / SysConfig.cameraNormalFOV;
		}
		float num = fDis * 0.12f;
		bool flag2 = num < 1f;
		if (flag2)
		{
			num = 1f;
		}
		bool flag3 = this.fOldScale != num;
		if (flag3)
		{
			this.showUiTransform.localScale = new Vector3(num, num, 1f);
			this.fOldScale = num;
		}
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x00103644 File Offset: 0x00101844
	private void CheckHp()
	{
		bool flag = this.hpcon == null || !this._hpParent.activeSelf;
		if (!flag)
		{
			int nowHp = this.gameplayer.playerCommon.GetNowHp();
			bool flag2 = nowHp != this.nOldHp;
			if (flag2)
			{
				int maxHp = this.gameplayer.playerCommon.GetMaxHp();
				int nStat = 0;
				bool isEnemy = this.IsEnemy;
				if (isEnemy)
				{
					nStat = 1;
				}
				this.hpcon.SetHp(maxHp, this.gameplayer.playerCommon.m_nHp, this.gameplayer.playerCommon.m_nArmor, this.gameplayer.playerCommon.m_nShield, nStat);
				this.nOldHp = nowHp;
			}
		}
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0010370C File Offset: 0x0010190C
	private void CheckRotaion(Vector3 bAt, Vector3 bTo)
	{
		Vector3 normalized = (bTo - bAt).normalized;
		Quaternion rotation = Quaternion.LookRotation(normalized);
		Quaternion rotation2 = base.transform.parent.rotation;
		bool flag = this.qOldIon != rotation2 || this.v3dirOld != normalized;
		if (flag)
		{
			base.transform.rotation = rotation;
			this.qOldIon = rotation2;
			this.v3dirOld = normalized;
		}
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x00103784 File Offset: 0x00101984
	private void CheckHeadPos()
	{
		Vector3 rhs = Vector3.zero;
		float num = 0.5f;
		float num2 = 0f;
		string text = string.Empty;
		bool flag = !this.gameplayer.haveDestroy && !(this.gameplayer.userHeadTransform == null);
		if (flag)
		{
			rhs = this.gameplayer.thisModel.transform.position;
			bool flag2 = this.gameplayer.netobject_type == 1;
			if (flag2)
			{
				num = ((GamePlayer)this.gameplayer).heroData.vHeadNamePos[1];
				text = ((GamePlayer)this.gameplayer).heroData.hname;
			}
			bool flag3 = this.gameplayer.netobject_type == 2;
			if (flag3)
			{
				num = (float)((Npc_Entity)this.gameplayer).SignHeight / 1000f;
				text = ((Npc_Entity)this.gameplayer).Name;
			}
			bool flag4 = this.gameplayer.userHeadTransform == null;
			if (flag4)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"计算模型头顶血条.名称位置时找不到userHeadTransform对象,-netobject_type:",
					this.gameplayer.netobject_type.ToString(),
					"--m_sName:",
					this.gameplayer.playerCommon.m_sName,
					"--sHeroName:",
					text
				}));
			}
			else
			{
				num2 = this.gameplayer.userHeadTransform.transform.position.y;
			}
			rhs.y = num2 + num;
			bool flag5 = this.vOldHeadPos != rhs;
			if (flag5)
			{
				this.vOldHeadPos = rhs;
				this.gameplayer.UserHeadTarget.transform.position = this.vOldHeadPos;
			}
		}
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x00103944 File Offset: 0x00101B44
	private bool GetActiveStat()
	{
		bool result = false;
		bool flag = !this._showUi.activeSelf || (!this.topNameObj.activeSelf && !this.bufflist.activeSelf && !this.ui_jiantou.activeSelf && !this._hpParent.activeSelf);
		if (flag)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x001039AC File Offset: 0x00101BAC
	private void CheckHeadActive(float fDis)
	{
		bool flag = !this.IsEnemy;
		if (flag)
		{
			this.bName = true;
			bool flag2 = !this.ui_jiantou.activeSelf;
			if (flag2)
			{
				this.ui_jiantou.SetActive(true);
			}
			bool flag3 = this.gameplayer.playerCommon.m_nFightStat == 1;
			if (flag3)
			{
				bool activeSelf = this.jiantoulan.activeSelf;
				if (activeSelf)
				{
					this.jiantoulan.SetActive(false);
				}
				bool flag4 = !this.jiantouhuang.activeSelf;
				if (flag4)
				{
					this.jiantouhuang.SetActive(true);
				}
			}
			else
			{
				bool flag5 = !this.jiantoulan.activeSelf;
				if (flag5)
				{
					this.jiantoulan.SetActive(true);
				}
				bool activeSelf2 = this.jiantouhuang.activeSelf;
				if (activeSelf2)
				{
					this.jiantouhuang.SetActive(false);
				}
			}
			bool flag6 = fGameManager.Heroplayer.heroData != null;
			if (flag6)
			{
				int teammate_Health_Bars = GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(fGameManager.Heroplayer.playerCommon.m_nModelIndex).Teammate_Health_Bars;
				bool flag7 = teammate_Health_Bars == 1;
				if (flag7)
				{
					this.bHp = true;
				}
			}
			bool flag8 = this.bufflist.transform.childCount > 0;
			if (flag8)
			{
				bool flag9 = !this.bufflist.activeSelf;
				if (flag9)
				{
					this.bufflist.SetActive(true);
				}
			}
			else
			{
				bool activeSelf3 = this.bufflist.activeSelf;
				if (activeSelf3)
				{
					this.bufflist.SetActive(false);
				}
			}
		}
		else
		{
			this.CheckEnemyShow(fDis);
		}
		bool flag10 = MatchManager.GetInstance().m_pFbData.nType == 10;
		if (flag10)
		{
			this.bHp = false;
		}
		bool flag11 = this.bHp != this._hpParent.activeSelf;
		if (flag11)
		{
			this._hpParent.SetActive(this.bHp);
		}
		bool flag12 = this.bName != this.topNameObj.activeSelf;
		if (flag12)
		{
			this.topNameObj.SetActive(this.bName);
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x00103BD8 File Offset: 0x00101DD8
	private void CheckEnemyShow(float fDis)
	{
		bool flag = this.topNameObj.activeSelf;
		bool flag2 = this._hpParent.activeSelf;
		bool flag3 = this.bufflist.activeSelf;
		bool flag4 = this.ui_jiantou.activeSelf;
		bool heroInvi = this.gameplayer.heroInvi;
		if (heroInvi)
		{
			bool flag5 = flag;
			if (flag5)
			{
				flag = false;
			}
			bool flag6 = flag2;
			if (flag6)
			{
				flag2 = false;
			}
			bool flag7 = flag3;
			if (flag7)
			{
				flag3 = false;
			}
		}
		else
		{
			bool flag8 = fDis > this.lookEnemyDis;
			if (flag8)
			{
				bool flag9 = flag;
				if (flag9)
				{
					flag = false;
				}
			}
			else
			{
				bool flag10 = !flag;
				if (flag10)
				{
					flag = true;
				}
			}
			bool flag11 = flag4;
			if (flag11)
			{
				flag4 = false;
			}
			bool flag12 = this.bufflist.transform.childCount > 0;
			if (flag12)
			{
				bool flag13 = !flag3;
				if (flag13)
				{
					flag3 = true;
				}
			}
			else
			{
				bool flag14 = flag3;
				if (flag14)
				{
					flag3 = false;
				}
			}
			bool enemyHitHp = this.GetEnemyHitHp();
			if (enemyHitHp)
			{
				bool flag15 = !flag;
				if (flag15)
				{
					flag = true;
				}
				bool flag16 = !flag2;
				if (flag16)
				{
					flag2 = true;
				}
			}
			else
			{
				bool flag17 = flag2;
				if (flag17)
				{
					flag2 = false;
				}
			}
		}
		this.bName = flag;
		this.bHp = flag2;
		bool flag18 = flag3 != this.bufflist.activeSelf;
		if (flag18)
		{
			this.bufflist.SetActive(flag3);
		}
		bool flag19 = flag4 != this.ui_jiantou.activeSelf;
		if (flag19)
		{
			this.ui_jiantou.SetActive(flag4);
		}
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x00103D50 File Offset: 0x00101F50
	private bool GetEnemyHitHp()
	{
		bool flag = false;
		bool flag2 = MatchManager.GetInstance().m_pFbData.nType == 10;
		bool result;
		if (flag2)
		{
			result = flag;
		}
		else
		{
			DateTime livingDamageDate = this.gameplayer.livingDamageDate;
			DateTime now = Fun.Now;
			int num = (int)(now - livingDamageDate).TotalSeconds;
			bool flag3 = (float)num >= 0f && num <= this.lookEnemyHpTime;
			if (flag3)
			{
				flag = true;
			}
			result = flag;
		}
		return result;
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x00103DCC File Offset: 0x00101FCC
	private bool CheckPlayerDead()
	{
		bool result = false;
		bool isDead = this.gameplayer.isDead;
		if (isDead)
		{
			bool activeSelf = this.topNameObj.activeSelf;
			if (activeSelf)
			{
				this.topNameObj.SetActive(false);
			}
			bool activeSelf2 = this.bufflist.activeSelf;
			if (activeSelf2)
			{
				this.bufflist.SetActive(false);
			}
			bool activeSelf3 = this.ui_jiantou.activeSelf;
			if (activeSelf3)
			{
				this.ui_jiantou.SetActive(false);
			}
			bool activeSelf4 = this._hpParent.activeSelf;
			if (activeSelf4)
			{
				this._hpParent.SetActive(false);
			}
			bool flag = gameuiCon.inst != null && gameuiCon.inst.mainMsgUI != null;
			if (flag)
			{
				gameuiCon.inst.mainMsgUI.FriendPinSiMate(false, Vector3.zero);
				bool flag2 = !this.IsEnemy && !this.gameplayer.isInitDead;
				if (flag2)
				{
					Vector3 screenVector = this.getScreenVector(this.ui_jiantou.transform.position);
					gameuiCon.inst.mainMsgUI.FriendDeadMate(this.gameplayer, screenVector);
				}
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x00103F04 File Offset: 0x00102104
	public void setName(Texture t)
	{
		bool flag = !this.gameplayer.haveDestroy;
		if (flag)
		{
			this.lzname.material.mainTexture = t;
			this.lzname.enabled = true;
		}
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x00103F48 File Offset: 0x00102148
	private void OnLzIcoData(PlayerCommon playercom)
	{
		string text = string.Empty;
		Fun.ClearChildObejct(this._lzList);
		text = Fun.GetOtherLzImgName(playercom);
		bool flag = !string.IsNullOrEmpty(text);
		if (flag)
		{
			this.AddLzIco(text);
		}
		text = Fun.GetOtherLzNianImgName(playercom);
		bool flag2 = !string.IsNullOrEmpty(text);
		if (flag2)
		{
			this.AddLzIco(text);
		}
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x00103FA4 File Offset: 0x001021A4
	private void AddLzIco(string imgname)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._lzlzimg);
		gameObject.transform.SetParent(this._lzList.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<Image>().sprite = Window.spriteName(AtlasType.ATLAS_MAINUI, imgname);
		gameObject.SetActive(true);
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x00104028 File Offset: 0x00102228
	private Texture2D copytext()
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
		this.cloneCamera.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(this.picSize, this.picSize, TextureFormat.ARGB32, false);
		this.cloneCamera.Render();
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect((float)(Screen.width / 2 - this.picSize / 2), (float)(Screen.height / 2 - this.picSize / 2), (float)this.picSize, (float)this.picSize), 0, 0, false);
		texture2D.Apply();
		this.cloneCamera.targetTexture = null;
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		return texture2D;
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x001040E4 File Offset: 0x001022E4
	private Vector3 getScreenVector(Vector3 bAt)
	{
		Vector3 result = new Vector3(-1f, -1f, -1f);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		Vector3 vector = CameraControl.Instance.controlCamera.WorldToScreenPoint(bAt);
		bool flag = vector.x >= 0f && vector.x <= num && vector.y >= 0f && vector.y <= num2 && vector.z > 0f;
		if (flag)
		{
			Vector2 sizeDelta = fUIManager.mCanObj.transform.GetComponent<RectTransform>().sizeDelta;
			float num3 = vector.x / num;
			float num4 = vector.y / num2;
			float x = sizeDelta.x * num3 - sizeDelta.x / 2f;
			float y = sizeDelta.y * num4 - sizeDelta.y / 2f;
			result.x = x;
			result.y = y;
			result.z = 0f;
		}
		return result;
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x001041F0 File Offset: 0x001023F0
	public void AddBuffIco(int nBuff, string sIco)
	{
		bool flag = !(sIco == "0") && !(sIco == string.Empty) && !this.mTopBuffIco.ContainsKey(nBuff.ToString());
		if (flag)
		{
			string url = "Icon/buffico/" + sIco + ".txt";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadIconEndOk), new ArrayList
				{
					nBuff.ToString(),
					sIco
				});
			}
			else
			{
				this.LoadIconEndOk(resWWW, new ArrayList
				{
					nBuff.ToString(),
					sIco
				});
			}
		}
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x001042B0 File Offset: 0x001024B0
	public void AddFaceIco(string sIco, int nHuDongType)
	{
		bool flag = !(sIco == string.Empty) && !(sIco == "0") && ((nHuDongType != 7 && nHuDongType != 5 && nHuDongType != 4 && nHuDongType != 8) || !this.IsEnemy) && !this.mTopBuffIco.ContainsKey("hudong");
		if (flag)
		{
			string url = "Icon/hudong/" + sIco + ".txt";
			WWW resWWW = GameRoot.GetResWWW(url);
			bool flag2 = resWWW == null;
			if (flag2)
			{
				GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.LoadIconEndOk), new ArrayList
				{
					"hudong",
					sIco
				});
			}
			else
			{
				this.LoadIconEndOk(resWWW, new ArrayList
				{
					"hudong",
					sIco
				});
			}
		}
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x00104380 File Offset: 0x00102580
	private void LoadIconEndOk(WWW www, object obj)
	{
		bool flag = www != null && www.error == null && this.gameplayer != null && !this.gameplayer.haveDestroy && this.gameplayer.playerHaveCreate && !(this.clonebuff == null) && !(this.bufflist == null);
		if (flag)
		{
			ArrayList arrayList = (ArrayList)obj;
			string text = arrayList[0].ToString();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.clonebuff);
			gameObject.transform.SetParent(this.bufflist.transform);
			gameObject.transform.localScale = new Vector3(90f, 90f, 0f);
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localPosition = Vector3.one;
			gameObject.name = text;
			gameObject.SetActive(true);
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			bool flag2 = component.material.mainTexture == null;
			if (flag2)
			{
				component.material.mainTexture = new Texture2D(4, 4);
			}
			www.LoadImageIntoTexture((Texture2D)component.material.mainTexture);
			bool flag3 = !this.mTopBuffIco.ContainsKey(text);
			if (flag3)
			{
				this.mTopBuffIco.Add(text, gameObject);
			}
			else
			{
				this.mTopBuffIco[text] = gameObject;
			}
			bool flag4 = text == "hudong";
			if (flag4)
			{
				base.StopCoroutine("DeayDestroy");
				base.StartCoroutine(this.DeayDestroy(gameObject));
			}
		}
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00104524 File Offset: 0x00102724
	public void DelBuffIco(int nBuff)
	{
		bool flag = this.mTopBuffIco.ContainsKey(nBuff.ToString());
		if (flag)
		{
			GameObject obj = this.mTopBuffIco[nBuff.ToString()];
			UnityEngine.Object.Destroy(obj);
			this.mTopBuffIco.Remove(nBuff.ToString());
		}
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00104579 File Offset: 0x00102779
	public IEnumerator DeayDestroy(GameObject oClone)
	{
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(oClone);
		this.mTopBuffIco.Remove("hudong");
		yield break;
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x00104590 File Offset: 0x00102790
	public void OnDel()
	{
		CCOpyNameManager.Instance.removeGetName(this.gameplayer);
		bool flag = this.lzname != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.lzname.material.mainTexture);
		}
	}

	// Token: 0x040018C3 RID: 6339
	private GameObject _showUi;

	// Token: 0x040018C4 RID: 6340
	private Transform showUiTransform;

	// Token: 0x040018C5 RID: 6341
	private NetLiving gameplayer;

	// Token: 0x040018C6 RID: 6342
	private bool IsEnemy;

	// Token: 0x040018C7 RID: 6343
	private float lookEnemyDis = 15f;

	// Token: 0x040018C8 RID: 6344
	private int lookEnemyHpTime = 5;

	// Token: 0x040018C9 RID: 6345
	private GameObject topNameObj;

	// Token: 0x040018CA RID: 6346
	private MeshRenderer lzname;

	// Token: 0x040018CB RID: 6347
	private GameObject hpObj;

	// Token: 0x040018CC RID: 6348
	private HpNewControl hpcon;

	// Token: 0x040018CD RID: 6349
	private GameObject _hpParent;

	// Token: 0x040018CE RID: 6350
	private GameObject ui_jiantou;

	// Token: 0x040018CF RID: 6351
	private GameObject jiantouhuang;

	// Token: 0x040018D0 RID: 6352
	private GameObject jiantoulan;

	// Token: 0x040018D1 RID: 6353
	private GameObject clonebuff;

	// Token: 0x040018D2 RID: 6354
	private GameObject bufflist;

	// Token: 0x040018D3 RID: 6355
	private Camera cloneCamera;

	// Token: 0x040018D4 RID: 6356
	private GameObject _lzList;

	// Token: 0x040018D5 RID: 6357
	private GameObject _lzlzimg;

	// Token: 0x040018D6 RID: 6358
	private static Camera picCamera;

	// Token: 0x040018D7 RID: 6359
	private float fOldScale;

	// Token: 0x040018D8 RID: 6360
	private int nOldHp;

	// Token: 0x040018D9 RID: 6361
	private Quaternion qOldIon = Quaternion.identity;

	// Token: 0x040018DA RID: 6362
	private Vector3 v3dirOld;

	// Token: 0x040018DB RID: 6363
	private Vector3 vOldHeadPos = Vector3.zero;

	// Token: 0x040018DC RID: 6364
	private bool bHp;

	// Token: 0x040018DD RID: 6365
	private bool bName;

	// Token: 0x040018DE RID: 6366
	private int picSize = 256;

	// Token: 0x040018DF RID: 6367
	private Dictionary<string, GameObject> mTopBuffIco = new Dictionary<string, GameObject>();
}
