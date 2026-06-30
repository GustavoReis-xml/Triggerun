using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class CPaintManager : MonoBehaviour
{
	// Token: 0x17000145 RID: 325
	// (get) Token: 0x0600066E RID: 1646 RVA: 0x000363EC File Offset: 0x000345EC
	// (set) Token: 0x0600066F RID: 1647 RVA: 0x00036403 File Offset: 0x00034603
	public static CPaintManager Instance
	{
		get
		{
			return CPaintManager._instance;
		}
		set
		{
			CPaintManager._instance = value;
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0003640C File Offset: 0x0003460C
	private void Awake()
	{
		CPaintManager.Instance = this;
		GameRoot.LoadAsset(string.Format("EffectRes/{0}.ef", "paint_prefab"), new fResourceManager.DownFinishDelegate(this.LoadPaintOK), "paint_prefab");
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0003643C File Offset: 0x0003463C
	private void Update()
	{
		bool flag = this.preLoadState == 1 && fGameManager.Heroplayer != null && !fGameManager.Heroplayer.haveDestroy;
		if (flag)
		{
			fGameManager.Heroplayer.SendMessageToServer(10095, new ArrayList
			{
				2,
				new ArrayList()
			});
			this.preLoadState = 2;
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x000364A8 File Offset: 0x000346A8
	private void LoadPaintOK(WWW wwwObj, object customParam)
	{
		bool flag = wwwObj != null && !(fGameManager.allPaintFather == null);
		if (flag)
		{
			this.paintPrefab = (GameObject)UnityEngine.Object.Instantiate(wwwObj.assetBundle.LoadAsset(wwwObj.assetBundle.GetAllAssetNames()[0]));
			this.paintPrefab.transform.SetParent(fGameManager.allPaintFather.transform);
			this.paintPrefab.transform.position = Vector3.zero;
			this.paintPrefab.transform.rotation = Quaternion.Euler(Vector3.zero);
			this.paintPrefab.transform.localScale = Vector3.one;
			this.paintMat = this.paintPrefab.GetComponent<Projector>().material;
			this.paintPrefab.SetActive(false);
			this.preLoadState = 1;
			this.AddInitPaint();
		}
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00036590 File Offset: 0x00034790
	private void AddInitPaint()
	{
		bool flag = this.szAddPaint.Count != 0;
		if (flag)
		{
			for (int i = 0; i < this.szAddPaint.Count; i += 2)
			{
				this.AddOnePaint((int)this.szAddPaint[i], (ArrayList)this.szAddPaint[i + 1]);
			}
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x000365FC File Offset: 0x000347FC
	private bool CheckPaintTime()
	{
		bool flag = true;
		bool chatInput = ChatManager.ChatInput;
		bool result;
		if (chatInput)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.preLoadState == 0;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = GameResultUICon.Inst != null && GameResultUICon.Inst.visible;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = gameuiCon.inst != null && gameuiCon.inst.mainRoundUI.mboolstat;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool resultModelScn = fUIManager.GetInstance().GetResultModelScn();
						if (resultModelScn)
						{
							result = false;
						}
						else
						{
							PlayerPaintData usePaintData = HeroDataManager.GetInstance().GetUsePaintData(PlayerDataManager.getInstance().HUserData.m_nCid);
							bool flag5 = usePaintData == null;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = usePaintData.nLockState < 1;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = string.IsNullOrEmpty(this.lastPaintTime);
									if (flag7)
									{
										this.lastPaintTime = Fun.GetServerDate().ToString();
									}
									else
									{
										DateTime d = DateTime.Parse(this.lastPaintTime);
										int num = (int)(Fun.GetServerDate() - d).TotalSeconds;
										bool flag8 = num < 1;
										if (flag8)
										{
											flag = false;
										}
									}
									result = flag;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00036744 File Offset: 0x00034944
	public void OnSetPaint()
	{
		bool flag = !this.CheckPaintTime();
		if (!flag)
		{
			Ray cursorRay = CameraControl.Instance.cursorRay;
			int layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Door_1") | 1 << LayerMask.NameToLayer("Door_2");
			ArrayList arrayList = new ArrayList();
			RaycastHit raycastHit;
			bool flag2 = !Physics.Raycast(cursorRay, out raycastHit, 5f, layerMask);
			if (!flag2)
			{
				bool flag3 = this.myPaintObj;
				if (flag3)
				{
					UnityEngine.Object.DestroyImmediate(this.myPaintObj);
					this.myPaintObj = null;
				}
				bool flag4 = this.paintPrefab != null;
				if (flag4)
				{
					this.myPaintObj = UnityEngine.Object.Instantiate<GameObject>(this.paintPrefab);
					this.myPaintObj.transform.position = raycastHit.point - cursorRay.direction * 0.01f;
					Vector3 forward = Vector3.Cross(raycastHit.normal, -CameraControl.Instance.transform.right);
					Quaternion rhs = Quaternion.FromToRotation(Vector3.up, Vector3.forward);
					Quaternion lhs = Quaternion.LookRotation(forward, raycastHit.normal);
					this.myPaintObj.transform.rotation = lhs * rhs;
					Transform transform = raycastHit.collider.transform;
					int num = -1;
					GameObject gameObject = raycastHit.collider.transform.root.gameObject;
					PublicInfo component = gameObject.GetComponent<PublicInfo>();
					bool flag5 = component != null;
					if (flag5)
					{
						NetLiving netObject = component.NetObject;
						num = netObject.getNetid();
						transform = netObject.defaultMesh.transform;
					}
					this.myPaintObj.transform.SetParent(transform);
					this.myPaintObj.gameObject.SetActive(true);
					this.myPaintObj.name = "painttestobj";
					Material material = UnityEngine.Object.Instantiate<Material>(this.paintMat);
					this.myPaintObj.GetComponent<Projector>().material = material;
					this.lastPaintTime = Fun.GetServerDate().ToString();
					PlayerPaintData usePaintData = HeroDataManager.GetInstance().GetUsePaintData(PlayerDataManager.getInstance().HUserData.m_nCid);
					string sIconName = usePaintData.sIconName;
					GameRoot.LoadAsset("Icon/painticon/" + sIconName + ".txt", new fResourceManager.DownFinishDelegate(this.LoadPaintIconOK), PlayerDataManager.getInstance().HUserData.m_nCid);
					arrayList.Add(usePaintData.nPaintId);
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.position.x * 100f));
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.position.y * 100f));
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.position.z * 100f));
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.eulerAngles.x * 100f));
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.eulerAngles.y * 100f));
					arrayList.Add(Mathf.CeilToInt(this.myPaintObj.transform.eulerAngles.z * 100f));
					arrayList.Add(num);
					fGameManager.Heroplayer.SendMessageToServer(10095, new ArrayList
					{
						1,
						arrayList
					});
				}
			}
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00036B18 File Offset: 0x00034D18
	private void LoadPaintIconOK(WWW www, object customParam)
	{
		bool flag = www == null || www.error != null;
		if (!flag)
		{
			int num = (int)customParam;
			bool flag2 = PlayerDataManager.getInstance().playerCommon.m_nCid == num;
			if (flag2)
			{
				bool flag3 = !(this.myPaintObj == null);
				if (flag3)
				{
					Projector component = this.myPaintObj.GetComponent<Projector>();
					bool flag4 = !(component == null);
					if (flag4)
					{
						www.LoadImageIntoTexture((Texture2D)component.material.mainTexture);
					}
				}
			}
			else
			{
				bool flag5 = this.otherPaints.ContainsKey(num) && !(this.otherPaints[num] == null);
				if (flag5)
				{
					Projector component2 = this.otherPaints[num].GetComponent<Projector>();
					bool flag6 = !(component2 == null);
					if (flag6)
					{
						component2.material.SetTexture("_MainTex", www.texture);
					}
				}
			}
		}
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00036C1C File Offset: 0x00034E1C
	public void PaintInfoSync(ArrayList szMsg)
	{
		int num = (int)szMsg[0];
		ArrayList arrayList = (ArrayList)szMsg[1];
		int num2 = (int)szMsg[2];
		bool flag = num2 == 0;
		if (flag)
		{
			this.DelOnePaint(num);
		}
		bool flag2 = num2 == 1;
		if (flag2)
		{
			bool flag3 = this.paintPrefab == null;
			if (flag3)
			{
				this.szAddPaint.Add(num);
				this.szAddPaint.Add(arrayList);
			}
			else
			{
				this.AddOnePaint(num, arrayList);
			}
			int nNetID = (int)szMsg[3];
			GamePlayer gamePlayer = (GamePlayer)GameRoot.SceneSocket.unFsorket.GetNetObj(nNetID);
			if (gamePlayer != null)
			{
				gamePlayer.playerSound.PlayPaintSound();
			}
		}
		bool flag4 = num2 == 2;
		if (flag4)
		{
			for (int i = 0; i < arrayList.Count; i += 2)
			{
				this.AddOnePaint((int)arrayList[i], (ArrayList)arrayList[i + 1]);
			}
			PlayerPaintData usePaintData = HeroDataManager.GetInstance().GetUsePaintData(PlayerDataManager.getInstance().HUserData.m_nCid);
			bool flag5 = usePaintData != null && !string.IsNullOrEmpty(usePaintData.sIconName);
			if (flag5)
			{
				GameRoot.LoadAsset("Icon/painticon/" + usePaintData.sIconName + ".txt", new fResourceManager.DownFinishDelegate(this.LoadPrefabIconOK), usePaintData.sIconName);
			}
		}
		bool flag6 = num2 != 3;
		if (!flag6)
		{
			foreach (GameObject obj in this.otherPaints.Values)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			this.otherPaints.Clear();
			UnityEngine.Object.DestroyImmediate(this.myPaintObj);
			this.myPaintObj = null;
		}
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00036E1C File Offset: 0x0003501C
	private void LoadPrefabIconOK(WWW www, object customParam)
	{
		bool flag = www != null && www.error == null && !(this.paintMat == null);
		if (flag)
		{
			this.paintMat.SetTexture("_MainTex", www.texture);
		}
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x00036E64 File Offset: 0x00035064
	private void AddOnePaint(int cid, ArrayList szdata)
	{
		bool flag = cid != PlayerDataManager.getInstance().playerCommon.m_nCid;
		if (flag)
		{
			int key = (int)szdata[0];
			int num = (int)szdata[7];
			Vector3 position = Fun.ServerToClientPos((int)szdata[1], (int)szdata[2], (int)szdata[3]);
			Vector3 euler = Fun.ServerToClientPos((int)szdata[4], (int)szdata[5], (int)szdata[6]);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.paintPrefab);
			gameObject.transform.position = position;
			gameObject.transform.rotation = Quaternion.Euler(euler);
			gameObject.transform.SetParent(fGameManager.allPaintFather.transform);
			gameObject.gameObject.SetActive(true);
			gameObject.name = "painttestobj";
			bool flag2 = num >= 0;
			if (flag2)
			{
				base.StartCoroutine(this.SearchParentObj(num, gameObject));
			}
			Material material = UnityEngine.Object.Instantiate<Material>(this.paintMat);
			gameObject.GetComponent<Projector>().material = material;
			bool flag3 = this.otherPaints.ContainsKey(cid);
			if (flag3)
			{
				UnityEngine.Object.DestroyImmediate(this.otherPaints[cid]);
				this.otherPaints[cid] = gameObject;
			}
			else
			{
				this.otherPaints.Add(cid, gameObject);
			}
			string sIconName = fGameManager.mloadConfig.AllPaintData[key].sIconName;
			GameRoot.LoadAsset("Icon/painticon/" + sIconName + ".txt", new fResourceManager.DownFinishDelegate(this.LoadPaintIconOK), cid);
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00037023 File Offset: 0x00035223
	private IEnumerator SearchParentObj(int hitid, GameObject paint)
	{
		NetObj _NetObj = null;
		GamePlayer _GamePlayer = null;
		Npc_Entity _Npc_Entity = null;
		_NetObj = GameRoot.SceneSocket.unFsorket.GetNetObj(hitid);
		bool flag = _NetObj != null;
		if (flag)
		{
			bool flag2 = _NetObj.netobject_type == 2;
			if (flag2)
			{
				_Npc_Entity = (Npc_Entity)_NetObj;
				paint.transform.SetParent(_Npc_Entity.defaultMesh.transform);
			}
			else
			{
				_GamePlayer = (GamePlayer)_NetObj;
				paint.transform.SetParent(_GamePlayer.defaultMesh.transform);
			}
		}
		else
		{
			yield return new WaitForSeconds(1f);
			base.StartCoroutine(this.SearchParentObj(hitid, paint));
		}
		yield break;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00037040 File Offset: 0x00035240
	private void DelOnePaint(int cid)
	{
		bool flag = this.otherPaints.ContainsKey(cid);
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.otherPaints[cid]);
			this.otherPaints.Remove(cid);
		}
	}

	// Token: 0x0400049F RID: 1183
	private static CPaintManager _instance;

	// Token: 0x040004A0 RID: 1184
	private GameObject paintPrefab;

	// Token: 0x040004A1 RID: 1185
	private Material paintMat;

	// Token: 0x040004A2 RID: 1186
	private GameObject myPaintObj;

	// Token: 0x040004A3 RID: 1187
	private Dictionary<int, GameObject> otherPaints = new Dictionary<int, GameObject>();

	// Token: 0x040004A4 RID: 1188
	private string lastPaintTime = string.Empty;

	// Token: 0x040004A5 RID: 1189
	private int preLoadState;

	// Token: 0x040004A6 RID: 1190
	private ArrayList szAddPaint = new ArrayList();
}
