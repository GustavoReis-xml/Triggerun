using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class MapEffectBox : MonoBehaviour
{
	// Token: 0x060019EA RID: 6634 RVA: 0x000F4BAF File Offset: 0x000F2DAF
	public void Init(MapEffect m)
	{
		this.NetObject = m;
		this.coll = base.GetComponentInChildren<BoxCollider>();
		this.layMask = LayerManager.GetPlayerAllLayer();
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x000F4BD0 File Offset: 0x000F2DD0
	private void Update()
	{
		bool flag = this.NetObject == null || this.NetObject.heroAIObjectForSearch == null || this.NetObject.thisModel == null || this.NetObject.heroAIObjectForSearch.getNetid() != fGameManager.Heroplayer.getNetid() || Time.time < this.ctime;
		if (!flag)
		{
			this.ctime = Time.time + 0.25f;
			ArrayList boxColliderInsideNetObject = this.getBoxColliderInsideNetObject();
			bool flag2 = boxColliderInsideNetObject.Count != this.lastList.Count;
			if (flag2)
			{
				this.NetObject.heroAIObjectForSearch.TosFun("SendMsgToServer", new object[]
				{
					5007,
					boxColliderInsideNetObject
				});
				this.lastList = boxColliderInsideNetObject;
			}
			else
			{
				bool flag3 = false;
				for (int i = 0; i < boxColliderInsideNetObject.Count; i++)
				{
					bool flag4 = i != this.lastList.IndexOf(boxColliderInsideNetObject[i]);
					if (flag4)
					{
						flag3 = true;
						break;
					}
				}
				bool flag5 = flag3;
				if (flag5)
				{
					this.NetObject.heroAIObjectForSearch.TosFun("SendMsgToServer", new object[]
					{
						5007,
						boxColliderInsideNetObject
					});
					this.lastList = boxColliderInsideNetObject;
				}
			}
		}
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x000F4D2C File Offset: 0x000F2F2C
	private ArrayList getBoxColliderInsideNetObject()
	{
		ArrayList arrayList = new ArrayList();
		bool flag = this.coll == null;
		ArrayList result;
		if (flag)
		{
			Debug.LogError("对象没有箱子碰撞体，无法获取碰撞对象");
			result = arrayList;
		}
		else
		{
			Vector3 halfExtents = new Vector3(this.coll.transform.lossyScale.x * this.coll.size.x * 0.5f, this.coll.transform.lossyScale.y * this.coll.size.y * 0.5f, this.coll.transform.lossyScale.z * this.coll.size.z * 0.5f);
			RaycastHit[] array = Physics.BoxCastAll(this.coll.bounds.center, halfExtents, Vector3.up, this.coll.transform.rotation, 0f, this.layMask);
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = this.coll == array[i].collider || !array[i].collider.tag.Contains("can_damage_") || array[i].collider.tag.Contains("can_damage_dun");
					if (!flag3)
					{
						GameObject gameObject = array[i].collider.transform.root.gameObject;
						PublicInfo component = gameObject.GetComponent<PublicInfo>();
						bool flag4 = component == null;
						if (!flag4)
						{
							int netid = component.NetObject.getNetid();
							bool flag5 = arrayList.Contains(netid);
							if (!flag5)
							{
								string type = component.type;
								string a = type;
								if (a == "HeroPlayer" || a == "GamePlayer" || a == "RobotAI")
								{
									PlayerInfo playerInfo = (PlayerInfo)component;
									NetLiving netObject = playerInfo.NetObject;
									bool flag6 = netObject != null && !netObject.isDead && !netObject.isCopy;
									if (flag6)
									{
										arrayList.Add(netid);
									}
								}
							}
						}
					}
				}
			}
			arrayList.Sort();
			arrayList.Add(this.NetObject.id);
			result = arrayList;
		}
		return result;
	}

	// Token: 0x04001466 RID: 5222
	private MapEffect NetObject;

	// Token: 0x04001467 RID: 5223
	private BoxCollider coll;

	// Token: 0x04001468 RID: 5224
	private int layMask;

	// Token: 0x04001469 RID: 5225
	private ArrayList lastList = new ArrayList();

	// Token: 0x0400146A RID: 5226
	private float ctime;
}
