using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class CCheckPlace : MonoBehaviour
{
	// Token: 0x060003AB RID: 939 RVA: 0x0001CF78 File Offset: 0x0001B178
	public void initStart()
	{
		this.cdCheckTime = Time.time + 1f;
		this.myParent = base.transform.root.name;
		this.layMask = 1 << LayerMask.NameToLayer("Default");
		this.coll = base.GetComponent<Collider>();
		this.vDirs = new Vector3[8];
		this.vDirs[0] = (Quaternion.Euler(-45f, -45f, 0f) * Vector3.forward).normalized;
		this.vDirs[1] = (Quaternion.Euler(-45f, 45f, 0f) * Vector3.forward).normalized;
		this.vDirs[2] = (Quaternion.Euler(45f, -45f, 0f) * Vector3.forward).normalized;
		this.vDirs[3] = (Quaternion.Euler(45f, 45f, 0f) * Vector3.forward).normalized;
		this.vDirs[4] = (Quaternion.Euler(-45f, 225f, 0f) * Vector3.forward).normalized;
		this.vDirs[5] = (Quaternion.Euler(45f, 225f, 0f) * Vector3.forward).normalized;
		this.vDirs[6] = (Quaternion.Euler(-45f, 135f, 0f) * Vector3.forward).normalized;
		this.vDirs[7] = (Quaternion.Euler(45f, 135f, 0f) * Vector3.forward).normalized;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001D1B2 File Offset: 0x0001B3B2
	private void Start()
	{
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001D1B8 File Offset: 0x0001B3B8
	private void initParm()
	{
		this.lastAt = this.coll.bounds.center;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001D1DF File Offset: 0x0001B3DF
	public void resetCheck(float t = 1f)
	{
		this.cdCheckTime = Time.time + t;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
	private void Update()
	{
		bool flag = !(this.coll == null);
		if (flag)
		{
			bool flag2 = this.hero == null;
			if (flag2)
			{
				this.hero = fGameManager.GetInstance().getPlayer();
				this.scName = fGameManager.GetInstance().mloadName;
			}
			else
			{
				bool flag3 = Time.time < this.cdCheckTime;
				if (flag3)
				{
					this.initParm();
				}
				else
				{
					this.checkMove();
					this.checkBox();
				}
			}
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0001D270 File Offset: 0x0001B470
	private void tuolikasi(string type)
	{
		bool flag = this.hero == null;
		if (flag)
		{
			this.tuoliType = type;
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001D294 File Offset: 0x0001B494
	private void checkMove()
	{
		bool flag = Time.time < this.timeOutMove;
		if (flag)
		{
			this.lastAt = this.coll.bounds.center;
		}
		else
		{
			bool isDead = this.hero.isDead;
			if (!isDead)
			{
				Vector3 center = this.coll.bounds.center;
				float num = Vector3.SqrMagnitude(center - this.lastAt);
				bool flag2 = num <= 0f;
				if (flag2)
				{
					this.lastAt = this.coll.bounds.center;
				}
				else
				{
					string sMsg = string.Empty;
					Vector3 normalized = (center - this.lastAt).normalized;
					float maxDistance = Vector3.Distance(this.lastAt, center);
					RaycastHit raycastHit;
					bool flag3 = Physics.Raycast(this.lastAt, normalized, out raycastHit, maxDistance, this.layMask) && raycastHit.transform.root.name != this.myParent && !raycastHit.transform.tag.Contains("can_damage");
					if (flag3)
					{
						Collider component = raycastHit.transform.gameObject.GetComponent<Collider>();
						bool flag4 = component != null && component.isTrigger;
						if (flag4)
						{
							this.lastAt = this.coll.bounds.center;
							return;
						}
						sMsg = string.Format(Language.GetLang(37026, new object[]
						{
							this.hero.playerCommon.m_sName,
							raycastHit.transform.name,
							this.scName,
							raycastHit.point.ToString("F3"),
							this.lastAt.ToString("F3"),
							center.ToString("F3"),
							maxDistance.ToString("F3"),
							this.hero.thisModel.transform.position.ToString()
						}), new object[0]);
						this.timeOutMove = Time.time + 3f;
						this.Send("151", sMsg);
					}
					this.lastAt = center;
				}
			}
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
	private void checkBox()
	{
		bool flag = Time.time < this.timeoutBox;
		if (!flag)
		{
			this.timeoutBox = Time.time + 1f;
			bool isDead = this.hero.isDead;
			if (!isDead)
			{
				this.inBox = false;
				string text = string.Empty;
				bool flag2 = this.iWhich == 0;
				Vector3 center;
				if (flag2)
				{
					center = this.coll.bounds.center;
				}
				else
				{
					bool flag3 = this.iWhich == 1;
					if (flag3)
					{
						center = this.coll.bounds.center;
						center.y += 0.5f;
					}
					else
					{
						center = this.coll.bounds.center;
						Debug.Log("checkplayer - 怎么超出了.");
					}
				}
				this.iWhich++;
				bool flag4 = this.iWhich > 1;
				if (flag4)
				{
					this.iWhich = 0;
				}
				int num = 0;
				for (int i = 0; i < this.vDirs.Length; i++)
				{
					Debug.DrawRay(center, this.vDirs[i] * 100f, Color.grey);
					bool flag5 = num > 1;
					if (flag5)
					{
						break;
					}
					RaycastHit raycastHit;
					bool flag6 = Physics.Raycast(center, this.vDirs[i], out raycastHit, 100f, this.layMask);
					if (flag6)
					{
						Collider component = raycastHit.transform.gameObject.GetComponent<Collider>();
						bool flag7 = component != null && component.isTrigger;
						if (!flag7)
						{
							Vector3 vector = raycastHit.point;
							vector = Vector3.MoveTowards(vector, center, 0.1f);
							float num2 = Vector3.Distance(center, vector);
							bool flag8 = num2 == 0f;
							if (!flag8)
							{
								RaycastHit[] array = Physics.RaycastAll(vector, -this.vDirs[i], num2, this.layMask);
								bool flag9 = array.Length == 0;
								if (!flag9)
								{
									for (int j = 0; j < array.Length; j++)
									{
										bool flag10 = !(array[j].transform.root.name != this.myParent) || array[j].transform.tag.Contains("can_damage");
										if (!flag10)
										{
											Collider component2 = array[j].transform.gameObject.GetComponent<Collider>();
											bool flag11 = !(component2 != null) || !component2.isTrigger;
											if (flag11)
											{
												this.inBox = true;
												this.boxInNum++;
												num++;
												text = string.Format(Language.GetLang(37027, new object[]
												{
													this.hero.playerCommon.m_sName,
													array[j].transform.name,
													array[j].transform.GetInstanceID(),
													this.scName,
													array[j].point.ToString(),
													this.hero.thisModel.transform.position.ToString()
												}), new object[0]);
												bool flag12 = !(this.lastObj == array[j].transform.name);
												if (flag12)
												{
													this.lastObj = array[j].transform.name;
													this.Send("152", text);
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						Vector3 origin = center + this.vDirs[i] * 100f;
						bool flag13 = !Physics.Raycast(origin, -this.vDirs[i], out raycastHit, 100f, this.layMask) || !(raycastHit.transform.root.name != this.myParent) || raycastHit.transform.tag.Contains("can_damage");
						if (!flag13)
						{
							Collider component3 = raycastHit.transform.gameObject.GetComponent<Collider>();
							bool flag14 = !(component3 != null) || !component3.isTrigger;
							if (flag14)
							{
								this.inBox = true;
								this.boxInNum++;
								num++;
								text = string.Format(Language.GetLang(37027, new object[]
								{
									this.hero.playerCommon.m_sName,
									raycastHit.transform.name,
									raycastHit.transform.GetInstanceID(),
									this.scName,
									raycastHit.point.ToString(),
									this.hero.thisModel.transform.position.ToString()
								}), new object[0]) + "," + this.hero.isCopy.ToString();
								bool flag15 = !(this.lastObj == raycastHit.transform.name);
								if (flag15)
								{
									this.lastObj = raycastHit.transform.name;
									this.Send("153", text);
								}
							}
						}
					}
				}
				bool flag16 = !this.inBox;
				if (flag16)
				{
					this.boxInNum = 0;
				}
				else
				{
					bool flag17 = this.boxInNum >= 3 && num > 1 && this.thisObj != this.lastObj;
					if (flag17)
					{
						this.thisObj = this.lastObj;
						this.boxInNum = 0;
						text = string.Format(Language.GetLang(37021, new object[]
						{
							2,
							text
						}), new object[0]);
						this.Send("154", text);
						this.tuolikasi("1010");
					}
				}
			}
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001DB4E File Offset: 0x0001BD4E
	public void Send(string sType, string sMsg)
	{
		FunManager.Instance.Send(this.hero, sType, sMsg);
	}

	// Token: 0x040002F5 RID: 757
	private HeroPlayer hero;

	// Token: 0x040002F6 RID: 758
	private Vector3[] vDirs;

	// Token: 0x040002F7 RID: 759
	private Collider coll;

	// Token: 0x040002F8 RID: 760
	private int layMask;

	// Token: 0x040002F9 RID: 761
	private const float longRay = 100f;

	// Token: 0x040002FA RID: 762
	private string myParent;

	// Token: 0x040002FB RID: 763
	public string scName = string.Empty;

	// Token: 0x040002FC RID: 764
	private string lastObj = string.Empty;

	// Token: 0x040002FD RID: 765
	private string thisObj = string.Empty;

	// Token: 0x040002FE RID: 766
	public bool showLost;

	// Token: 0x040002FF RID: 767
	private float cdCheckTime;

	// Token: 0x04000300 RID: 768
	private Vector3 lastAt;

	// Token: 0x04000301 RID: 769
	public string tuoliType = string.Empty;

	// Token: 0x04000302 RID: 770
	private float timeOutMove;

	// Token: 0x04000303 RID: 771
	private float timeoutBox;

	// Token: 0x04000304 RID: 772
	public bool inBox;

	// Token: 0x04000305 RID: 773
	private int iWhich;

	// Token: 0x04000306 RID: 774
	private int boxInNum;
}
