using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class Elevator_Xml_Data
{
	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0005C360 File Offset: 0x0005A560
	// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0005C392 File Offset: 0x0005A592
	public Elevator_Entity EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Elevator_Entity();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0005C39C File Offset: 0x0005A59C
	public void Initialize(XmlElement _XmlElement)
	{
		this.EntityComponent.Npc_ID = Fun.Convert_Int(_XmlElement.GetAttribute("Npc_ID"));
		this.EntityComponent.Map_ID = Fun.Convert_Int(_XmlElement.GetAttribute("Map_ID"));
		string text = _XmlElement.GetAttribute("Position").ToString();
		string[] array = text.Split(new char[]
		{
			'|'
		});
		this.EntityComponent.Origin_Point = new Vector3((float)int.Parse(array[0].ToString()) / 100f, (float)int.Parse(array[2].ToString()) / 100f, (float)int.Parse(array[1].ToString()) / 100f);
		text = _XmlElement.GetAttribute("Rotation").ToString();
		array = text.Split(new char[]
		{
			'|'
		});
		this.EntityComponent.Origin_Angle = new Vector3((float)int.Parse(array[0].ToString()), (float)int.Parse(array[2].ToString()), (float)int.Parse(array[1].ToString()));
		this.EntityComponent.Speed = Fun.Convert_Float(_XmlElement.GetAttribute("Speed"), 100);
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
	}

	// Token: 0x04000830 RID: 2096
	private Elevator_Entity _EntityComponent;
}
