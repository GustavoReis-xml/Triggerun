using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x02000238 RID: 568
[Serializable]
public class MapEffectData
{
	// Token: 0x170002DF RID: 735
	// (get) Token: 0x060019EE RID: 6638 RVA: 0x000F4FD3 File Offset: 0x000F31D3
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x000F4FDC File Offset: 0x000F31DC
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		this.scnid = int.Parse(xml.GetAttribute("scnid"));
		this.x = int.Parse(xml.GetAttribute("x"));
		this.y = int.Parse(xml.GetAttribute("y"));
		this.z = int.Parse(xml.GetAttribute("z"));
		this.model = xml.GetAttribute("model");
		bool flag = xml.HasAttribute("model_little");
		if (flag)
		{
			this.model_little = xml.GetAttribute("model_little");
		}
		this.objtype = int.Parse(xml.GetAttribute("objtype"));
		this.buffid = int.Parse(xml.GetAttribute("buffid"));
		this.radio = float.Parse(xml.GetAttribute("radio"));
		this.alertRange = float.Parse(xml.GetAttribute("alertrange"));
		this.checkTime = float.Parse(xml.GetAttribute("checktime")) / 1000f;
		bool flag2 = this.checkTime > 0f && this.checkTime < 0.1f;
		if (flag2)
		{
			this.checkTime = 0.1f;
		}
		this.sAnimation = xml.GetAttribute("animation");
		this.triggerCollide = int.Parse(xml.GetAttribute("triggercollide"));
		bool flag3 = xml.HasAttribute("rotation_x");
		if (flag3)
		{
			this.rotationX = float.Parse(xml.GetAttribute("rotation_x"));
		}
		bool flag4 = xml.HasAttribute("rotation_y");
		if (flag4)
		{
			this.rotationY = float.Parse(xml.GetAttribute("rotation_y"));
		}
		bool flag5 = xml.HasAttribute("rotation_z");
		if (flag5)
		{
			this.rotationZ = float.Parse(xml.GetAttribute("rotation_z"));
		}
		bool flag6 = xml.HasAttribute("model_size_x");
		if (flag6)
		{
			this.model_size = new Vector3(float.Parse(xml.GetAttribute("model_size_x")), float.Parse(xml.GetAttribute("model_size_y")), float.Parse(xml.GetAttribute("model_size_z")));
		}
		bool flag7 = xml.HasAttribute("buffid_remove");
		if (flag7)
		{
			this.buffid_remove = int.Parse(xml.GetAttribute("buffid_remove"));
		}
		bool flag8 = xml.HasAttribute("model_height");
		if (flag8)
		{
			this.model_height = float.Parse(xml.GetAttribute("model_height"));
		}
		bool flag9 = xml.HasAttribute("musicsound");
		if (flag9)
		{
			this.musicsound = xml.GetAttribute("musicsound");
		}
		bool flag10 = xml.HasAttribute("animationsound");
		if (flag10)
		{
			this.animationsound = xml.GetAttribute("animationsound");
		}
		bool flag11 = xml.HasAttribute("checknum");
		if (flag11)
		{
			this.checkNum = int.Parse(xml.GetAttribute("checknum"));
		}
		bool flag12 = xml.HasAttribute("checkcd");
		if (flag12)
		{
			this.checkCd = int.Parse(xml.GetAttribute("checkcd"));
		}
		bool flag13 = xml.HasAttribute("getstaynetobj");
		if (flag13)
		{
			this.getStayNetObj = int.Parse(xml.GetAttribute("getstaynetobj"));
		}
		string attribute = xml.GetAttribute("jieduan");
		bool flag14 = attribute != "0";
		if (flag14)
		{
			string[] array = attribute.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.szJieDuan.Add(int.Parse(array[i].ToString()));
			}
		}
	}

	// Token: 0x0400146B RID: 5227
	public int id;

	// Token: 0x0400146C RID: 5228
	public string name;

	// Token: 0x0400146D RID: 5229
	public int scnid;

	// Token: 0x0400146E RID: 5230
	public int x;

	// Token: 0x0400146F RID: 5231
	public int y;

	// Token: 0x04001470 RID: 5232
	public int z;

	// Token: 0x04001471 RID: 5233
	public string model;

	// Token: 0x04001472 RID: 5234
	public string model_little;

	// Token: 0x04001473 RID: 5235
	public int objtype;

	// Token: 0x04001474 RID: 5236
	public int buffid;

	// Token: 0x04001475 RID: 5237
	public int buffRate;

	// Token: 0x04001476 RID: 5238
	public float radio;

	// Token: 0x04001477 RID: 5239
	public float checkTime;

	// Token: 0x04001478 RID: 5240
	public string sAnimation;

	// Token: 0x04001479 RID: 5241
	public float alertRange;

	// Token: 0x0400147A RID: 5242
	public int triggerCollide;

	// Token: 0x0400147B RID: 5243
	public float rotationX;

	// Token: 0x0400147C RID: 5244
	public float rotationY;

	// Token: 0x0400147D RID: 5245
	public float rotationZ;

	// Token: 0x0400147E RID: 5246
	public Vector3 model_size = Vector3.one;

	// Token: 0x0400147F RID: 5247
	public int buffid_remove;

	// Token: 0x04001480 RID: 5248
	public float model_height;

	// Token: 0x04001481 RID: 5249
	public string musicsound = "0";

	// Token: 0x04001482 RID: 5250
	public string animationsound = "0";

	// Token: 0x04001483 RID: 5251
	public int checkNum;

	// Token: 0x04001484 RID: 5252
	public int checkCd;

	// Token: 0x04001485 RID: 5253
	public int getStayNetObj;

	// Token: 0x04001486 RID: 5254
	public ArrayList szJieDuan = new ArrayList();
}
