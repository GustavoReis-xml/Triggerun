using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class WeaponData
{
	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x001912F9 File Offset: 0x0018F4F9
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06002DA1 RID: 11681 RVA: 0x00191304 File Offset: 0x0018F504
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		this.zhunxing = int.Parse(xml.GetAttribute("zhunxing"));
		this.hitratesub = int.Parse(xml.GetAttribute("hitratesub"));
		string[] array = xml.GetAttribute("hitratebase").Split(new char[]
		{
			'|'
		});
		this.hitratebase = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			this.hitratebase[i] = int.Parse(array[i]);
		}
		string[] array2 = xml.GetAttribute("cartridgepublic").Split(new char[]
		{
			'|'
		});
		this.cartridgepublic = new int[array2.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			this.cartridgepublic[j] = int.Parse(array2[j]);
		}
		string[] array3 = xml.GetAttribute("cartridgenoforgroup").Split(new char[]
		{
			'|'
		});
		this.cartridgeNoForGroup = new int[array3.Length];
		for (int k = 0; k < array3.Length; k++)
		{
			this.cartridgeNoForGroup[k] = int.Parse(array3[k]);
		}
		bool flag = xml.GetAttribute("cartridgepublicsound") != "0";
		if (flag)
		{
			this.cartridgepublicsound = xml.GetAttribute("cartridgepublicsound").Split(new char[]
			{
				'|'
			});
		}
		string[] array4 = xml.GetAttribute("recoilrecover").Split(new char[]
		{
			'|'
		});
		this.recoilFireY = (float)int.Parse(array4[0]) / 10f;
		this.recoilFireX = (float)int.Parse(array4[1]) / 10f;
		this.recoilRecoverY = (float)int.Parse(array4[2]) / 10f;
		this.recoilRecoverX = (float)int.Parse(array4[3]) / 10f;
		this.recoilStop = (float)int.Parse(array4[4]) / 10f;
		string[] array5 = xml.GetAttribute("weaponicons").Split(new char[]
		{
			'|'
		});
		this.weaponicons = new ArrayList();
		for (int l = 0; l < array5.Length; l++)
		{
			string[] value = array5[l].Split(new char[]
			{
				','
			});
			this.weaponicons.Add(value);
		}
		this.hitratecd = int.Parse(xml.GetAttribute("hitratecd"));
		this.hitratesubfire = int.Parse(xml.GetAttribute("hitratesubfire"));
		this.weaponList = new ArrayList();
		foreach (object obj in xml.ChildNodes)
		{
			XmlElement xmlElement = (XmlElement)obj;
			XMLWeapon xmlweapon = new XMLWeapon();
			this.weaponList.Add(xmlweapon);
			xmlweapon.name = xmlElement.GetAttribute("name");
			xmlweapon.shootname = int.Parse(xmlElement.GetAttribute("shootname"));
			xmlweapon.cartridgeCD = int.Parse(xmlElement.GetAttribute("cartridgecd"));
			xmlweapon.key = xmlElement.GetAttribute("key");
			xmlweapon.keygroup = int.Parse(xmlElement.GetAttribute("keygroup"));
			xmlweapon.keyfull = xmlElement.GetAttribute("keyfull");
			xmlweapon.launcher_type = int.Parse(xmlElement.GetAttribute("launcher_type"));
			xmlweapon.bullet_type = int.Parse(xmlElement.GetAttribute("bullet_type"));
			xmlweapon.muzzlename = new ArrayList();
			string[] array6 = xmlElement.GetAttribute("muzzlename").Split(new char[]
			{
				'|'
			});
			for (int m = 0; m < array6.Length; m++)
			{
				xmlweapon.muzzlename.Add(array6[m]);
			}
			xmlweapon.muzzleanimation = new ArrayList();
			bool flag2 = xmlElement.GetAttribute("muzzleanimation") != "0";
			if (flag2)
			{
				string[] array7 = xmlElement.GetAttribute("muzzleanimation").Split(new char[]
				{
					'|'
				});
				for (int n = 0; n < array7.Length; n++)
				{
					xmlweapon.muzzleanimation.Add(int.Parse(array7[n]));
				}
			}
			xmlweapon.muzzleanimationname = new ArrayList();
			bool flag3 = xmlElement.GetAttribute("muzzleanimationname").Length > 2;
			if (flag3)
			{
				string[] array8 = xmlElement.GetAttribute("muzzleanimationname").Split(new char[]
				{
					'|'
				});
				for (int num = 0; num < array8.Length; num++)
				{
					xmlweapon.muzzleanimationname.Add(array8[num]);
				}
			}
			else
			{
				xmlweapon.muzzleanimationname.Add("AttackState");
			}
			string[] array9 = xmlElement.GetAttribute("recoil").Split(new char[]
			{
				'|'
			});
			xmlweapon.recoil = new float[array9.Length];
			for (int num2 = 0; num2 < array9.Length; num2++)
			{
				xmlweapon.recoil[num2] = (float)int.Parse(array9[num2]) / 10f;
			}
			string[] array10 = xmlElement.GetAttribute("recoilmax").Split(new char[]
			{
				'|'
			});
			xmlweapon.recoilMax = new float[array10.Length];
			for (int num3 = 0; num3 < array10.Length; num3++)
			{
				xmlweapon.recoilMax[num3] = (float)int.Parse(array10[num3]) / 10f;
			}
			bool flag4 = xmlElement.GetAttribute("breakcartrid") == "1";
			if (flag4)
			{
				xmlweapon.breakCartrid = true;
			}
			else
			{
				xmlweapon.breakCartrid = false;
			}
			xmlweapon.fillCartridShootName = int.Parse(xmlElement.GetAttribute("fillcartridshootname"));
			xmlweapon.breakWeaponShootName = int.Parse(xmlElement.GetAttribute("breakweaponshootname"));
			bool flag5 = xmlElement.GetAttribute("ignorepubliccd") == "1";
			if (flag5)
			{
				xmlweapon.ignorePubliccd = true;
			}
			else
			{
				xmlweapon.ignorePubliccd = false;
			}
			xmlweapon.launcherFullTime = (float)int.Parse(xmlElement.GetAttribute("launcherfulltime")) * 0.001f;
			xmlweapon.launcherfullType = int.Parse(xmlElement.GetAttribute("launcherfulltype"));
			xmlweapon.launcherFullZero = (int.Parse(xmlElement.GetAttribute("launcherfullzero")) != 0);
			xmlweapon.cartridgeNo = int.Parse(xmlElement.GetAttribute("cartridge_no"));
			xmlweapon.shootspeed = int.Parse(xmlElement.GetAttribute("launchershootspeed"));
			XmlNodeList elementsByTagName = xmlElement.GetElementsByTagName("launcher");
			xmlweapon.launcher = new ArrayList();
			for (int num4 = 0; num4 < elementsByTagName.Count; num4++)
			{
				XmlElement xmlElement2 = xmlElement.GetElementsByTagName("launcher").Item(num4) as XmlElement;
				ArrayList arrayList = new ArrayList();
				xmlweapon.launcher.Add(arrayList);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherno")));
				arrayList.Add(new ArrayList());
				arrayList.Add(new ArrayList());
				arrayList.Add(0);
				arrayList.Add(xmlElement2.GetAttribute("firemodelname_fps"));
				arrayList.Add(xmlElement2.GetAttribute("fullmodelname"));
				arrayList.Add(xmlElement2.GetAttribute("firesoundname"));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherhotmax")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherhotadd")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherhotsubtime")));
				arrayList.Add(xmlElement2.GetAttribute("bulletmodel_fps"));
				string[] value2 = new string[0];
				bool flag6 = xmlElement2.GetAttribute("talksound") != "0";
				if (flag6)
				{
					value2 = xmlElement2.GetAttribute("talksound").Split(new char[]
					{
						'|'
					});
				}
				arrayList.Add(value2);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("talksoundrand")));
				string[] value3 = new string[0];
				bool flag7 = xmlElement2.GetAttribute("damagedeadsounds") != "0";
				if (flag7)
				{
					value3 = xmlElement2.GetAttribute("damagedeadsounds").Split(new char[]
					{
						'|'
					});
				}
				arrayList.Add(value3);
				arrayList.Add(true);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherlocktarget")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherdelaytime")));
				arrayList.Add(xmlElement2.GetAttribute("launcherfullsound"));
				ArrayList arrayList2 = new ArrayList();
				bool flag8 = xmlElement2.GetAttribute("launchertags") != "0";
				if (flag8)
				{
					string[] array11 = xmlElement2.GetAttribute("launchertags").Split(new char[]
					{
						'|'
					});
					for (int num5 = 0; num5 < array11.Length; num5++)
					{
						arrayList2.Add(int.Parse(array11[num5]));
					}
				}
				arrayList.Add(arrayList2);
				int[] array12 = new int[0];
				bool flag9 = xmlElement2.GetAttribute("buff") != "0";
				if (flag9)
				{
					string[] array13 = xmlElement2.GetAttribute("buff").Split(new char[]
					{
						'|'
					});
					array12 = new int[array13.Length];
					for (int num6 = 0; num6 < array12.Length; num6++)
					{
						array12[num6] = int.Parse(array13[num6]);
					}
				}
				arrayList.Add(array12);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("publiccd")));
				arrayList.Add(0);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("damageparamme")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("buffrandom")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launchercontinuousnumber")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("hitrateadd")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("hitratemax")));
				arrayList.Add(0);
				arrayList.Add("0");
				arrayList.Add(xmlElement2.GetAttribute("linemuzzletarget"));
				string[] array14 = xmlElement2.GetAttribute("npcindex").Split(new char[]
				{
					'|'
				});
				int[] array15 = (!(array14[0] == "0")) ? new int[array14.Length] : new int[0];
				for (int num7 = 0; num7 < array15.Length; num7++)
				{
					array15[num7] = int.Parse(array14[num7]);
				}
				arrayList.Add(array15);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("forceplace")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("damageforce")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("layertype")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("damagelayertype")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("damagefall")) != 0);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("firemodelrotate")) != 0);
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("usebullet")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("forcesource")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("hitratetype")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("camerashake")));
				arrayList.Add(xmlElement2.GetAttribute("fireloopsound"));
				arrayList.Add(xmlElement2.GetAttribute("firemodelname_tps"));
				arrayList.Add(xmlElement2.GetAttribute("bulletmodel_tps"));
				arrayList.Add(xmlElement2.GetAttribute("bulletmodel_delay_fps"));
				arrayList.Add(xmlElement2.GetAttribute("bulletmodel_delay_tps"));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launchercontinuousshootspeed")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherdelaytype")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("launcherdealywork")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("muzzlesoundtimeout")));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("firefrom")));
				arrayList.Add(xmlElement2.GetAttribute("fireloopsounddamage"));
				arrayList.Add(int.Parse(xmlElement2.GetAttribute("fireloopsoundtype")));
				arrayList.Add(xmlElement2.GetAttribute("damagehittalk_friend"));
				arrayList.Add(xmlElement2.GetAttribute("damagehittalk_enemy"));
				XmlElement xmlElement3 = xmlElement2.GetElementsByTagName("bullet").Item(0) as XmlElement;
				ArrayList arrayList3 = (ArrayList)arrayList[1];
				arrayList3.Add(xmlElement3.GetAttribute("bulletboommodel"));
				arrayList3.Add(xmlElement3.GetAttribute("bulletboomsound"));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletboomrange")));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletboomdelay")));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletdestorytimeout")));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bullettrigger")) != 0);
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("triggertype")));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletthrough")));
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletboomblock")) != 0);
				arrayList3.Add(int.Parse(xmlElement3.GetAttribute("bulletboomcullback")));
				XmlElement xmlElement4 = xmlElement2.GetElementsByTagName("attribute").Item(0) as XmlElement;
				ArrayList arrayList4 = (ArrayList)arrayList[2];
				switch (xmlweapon.bullet_type)
				{
				case 1:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bulletflyspeed")));
					arrayList4.Add(Mathf.FloorToInt((float)int.Parse(xmlElement4.GetAttribute("bulletmaxdistance")) * 0.01f));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("morenumber")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("morerate")));
					break;
				case 2:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("hitdamage")));
					break;
				case 3:
				{
					arrayList4.Add(float.Parse(xmlElement4.GetAttribute("bulletphysicsforce")));
					arrayList4.Add(float.Parse(xmlElement4.GetAttribute("bulletmass")));
					arrayList4.Add(float.Parse(xmlElement4.GetAttribute("bulletdrag")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bulletgravity")) != 0);
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("centerup")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("checktime")));
					string[] array16 = xmlElement4.GetAttribute("parm").Split(new char[]
					{
						'|'
					});
					ArrayList arrayList5 = new ArrayList();
					bool flag10 = array16.Length > 1;
					if (flag10)
					{
						arrayList5.Add(int.Parse(array16[0]));
						arrayList5.Add(int.Parse(array16[1]));
						arrayList5.Add(int.Parse(array16[2]));
					}
					arrayList4.Add(arrayList5);
					break;
				}
				case 4:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("initspeed")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("initrotationspeed")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("acceleratedspeed")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("acceleratedrotationspeed")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("acceleratedspeedmax")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("acceleratedrotationspeedmax")));
					break;
				case 5:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bulletflyspeed")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bezierrate")));
					break;
				case 6:
					arrayList4.Add(Mathf.FloorToInt((float)int.Parse(xmlElement4.GetAttribute("bulletmaxdistance")) * 0.01f));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("isline")));
					break;
				case 7:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bulletflyspeed")));
					arrayList4.Add(Mathf.FloorToInt((float)int.Parse(xmlElement4.GetAttribute("bulletmaxdistance")) * 0.01f));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("morenumber")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("morerate")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("anglerate")));
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("snumber")));
					break;
				case 8:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("weaponradius")));
					break;
				case 9:
					arrayList4.Add(int.Parse(xmlElement4.GetAttribute("bulletflyspeed")));
					arrayList4.Add(Mathf.FloorToInt((float)int.Parse(xmlElement4.GetAttribute("bulletmaxdistance")) * 0.01f));
					arrayList4.Add(Mathf.FloorToInt((float)int.Parse(xmlElement4.GetAttribute("bulletradius"))));
					break;
				}
			}
		}
	}

	// Token: 0x0400269F RID: 9887
	public int id;

	// Token: 0x040026A0 RID: 9888
	public string name;

	// Token: 0x040026A1 RID: 9889
	public int zhunxing;

	// Token: 0x040026A2 RID: 9890
	public int[] hitratebase;

	// Token: 0x040026A3 RID: 9891
	public int hitratesub;

	// Token: 0x040026A4 RID: 9892
	public int[] cartridgepublic;

	// Token: 0x040026A5 RID: 9893
	public int[] cartridgeNoForGroup;

	// Token: 0x040026A6 RID: 9894
	public string[] cartridgepublicsound;

	// Token: 0x040026A7 RID: 9895
	public ArrayList weaponicons;

	// Token: 0x040026A8 RID: 9896
	public int hitratecd;

	// Token: 0x040026A9 RID: 9897
	public int hitratesubfire;

	// Token: 0x040026AA RID: 9898
	public float recoilFireX;

	// Token: 0x040026AB RID: 9899
	public float recoilFireY;

	// Token: 0x040026AC RID: 9900
	public float recoilRecoverX;

	// Token: 0x040026AD RID: 9901
	public float recoilRecoverY;

	// Token: 0x040026AE RID: 9902
	public float recoilStop;

	// Token: 0x040026AF RID: 9903
	public ArrayList weaponList;
}
