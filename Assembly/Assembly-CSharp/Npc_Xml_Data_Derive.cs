using System;
using System.Xml;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class Npc_Xml_Data_Derive
{
	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0010B6B8 File Offset: 0x001098B8
	// (set) Token: 0x06001D05 RID: 7429 RVA: 0x0010B6ED File Offset: 0x001098ED
	public Npc_Entity_Derive EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Npc_Entity_Derive();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x0010B6F8 File Offset: 0x001098F8
	public void Initialize(XmlElement _XmlElement)
	{
		this.EntityComponent.Npc_ID = Fun.Convert_Int(_XmlElement.GetAttribute("Npc_ID"));
		this.EntityComponent.Country = Fun.Convert_Int(_XmlElement.GetAttribute("Country"));
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
	}

	// Token: 0x04001976 RID: 6518
	private Npc_Entity_Derive _EntityComponent;
}
