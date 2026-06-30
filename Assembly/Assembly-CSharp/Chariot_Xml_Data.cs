using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class Chariot_Xml_Data
{
	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x0600045D RID: 1117 RVA: 0x0002237C File Offset: 0x0002057C
	// (set) Token: 0x0600045E RID: 1118 RVA: 0x000223AE File Offset: 0x000205AE
	public Chariot_Entity EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Chariot_Entity();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000223B8 File Offset: 0x000205B8
	public void Initialize(XmlElement _XmlElement)
	{
		this.EntityComponent.Npc_ID = Fun.Convert_Int(_XmlElement.GetAttribute("Npc_ID"));
		this.EntityComponent.Map_ID = Fun.Convert_Int(_XmlElement.GetAttribute("Map_ID"));
		string text = _XmlElement.GetAttribute("Origin_Point").ToString();
		string[] array = text.Split(new char[]
		{
			'|'
		});
		this.EntityComponent.Origin_Point = new Vector3((float)int.Parse(array[0].ToString()) / 100f, (float)int.Parse(array[2].ToString()) / 100f, (float)int.Parse(array[1].ToString()) / 100f);
		text = _XmlElement.GetAttribute("Rotation_Angle").ToString();
		array = text.Split(new char[]
		{
			'|'
		});
		this.EntityComponent.Origin_Angle = new Vector3((float)int.Parse(array[0].ToString()), (float)int.Parse(array[2].ToString()), (float)int.Parse(array[1].ToString()));
		text = _XmlElement.GetAttribute("Move_Speed").ToString();
		array = text.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.EntityComponent.Move_Speed.Add(i + 1, (float)int.Parse(array[i].ToString()) / 100f);
		}
		text = _XmlElement.GetAttribute("Node_Data").ToString();
		array = text.Split(new char[]
		{
			'|'
		});
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			string[] array4 = text2.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Vector3((float)int.Parse(array4[1].ToString()) / 100f, (float)int.Parse(array4[3].ToString()) / 100f, (float)int.Parse(array4[2].ToString()) / 100f));
			arrayList.Add((float)int.Parse(array4[4].ToString()) / 100f);
			this.EntityComponent.Node_Data.Add(int.Parse(array4[0].ToString()), arrayList);
		}
		text = _XmlElement.GetAttribute("Pass_Node_Data").ToString();
		array = text.Split(new char[]
		{
			'|'
		});
		string[] array5 = array;
		foreach (string text3 in array5)
		{
			string[] array4 = text3.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Vector3((float)int.Parse(array4[1].ToString()) / 100f, (float)int.Parse(array4[3].ToString()) / 100f, (float)int.Parse(array4[2].ToString()) / 100f));
			arrayList.Add((float)int.Parse(array4[4].ToString()) / 100f);
			this.EntityComponent.Pass_Node_Data.Add(int.Parse(array4[0].ToString()), arrayList);
		}
		this.EntityComponent.Detection_Range_1 = Fun.Convert_Float(_XmlElement.GetAttribute("Detection_Range_1"), 100);
		this.EntityComponent.Detection_Range_2 = Fun.Convert_Float(_XmlElement.GetAttribute("Detection_Range_2"), 100);
	}

	// Token: 0x0400035A RID: 858
	private Chariot_Entity _EntityComponent;
}
