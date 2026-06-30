using System;
using System.Collections;
using System.Xml;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class Npc_Xml_Data_AI
{
	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0010B3D0 File Offset: 0x001095D0
	// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0010B402 File Offset: 0x00109602
	public Npc_AI_Entity EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Npc_AI_Entity();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x0010B40C File Offset: 0x0010960C
	public void Initialize(XmlElement _XmlElement)
	{
		this.EntityComponent.Index = Fun.Convert_Int(_XmlElement.GetAttribute("Index"));
		this.EntityComponent.Resurgence_Time = Fun.Convert_Float(_XmlElement.GetAttribute("Resurgence_Time"), 100);
		this.EntityComponent.Move_Type = Fun.Convert_Int(_XmlElement.GetAttribute("Move_Type"));
		this.EntityComponent.Move_Range = Fun.Convert_Float(_XmlElement.GetAttribute("Move_Range"), 100);
		this.EntityComponent.Alert_Range = (float)Fun.Convert_Int(_XmlElement.GetAttribute("Alert_Range"));
		string text = _XmlElement.GetAttribute("Node_Data").ToString();
		bool flag = text != string.Empty;
		string[] array;
		if (flag)
		{
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
				bool flag2 = array4.Length >= 1;
				if (flag2)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(new Vector3((float)int.Parse(array4[1].ToString()) / 100f, (float)int.Parse(array4[2].ToString()) / 100f, (float)int.Parse(array4[3].ToString()) / 100f));
					arrayList.Add((float)int.Parse(array4[4].ToString()) / 100f);
					this.EntityComponent.Node_Data.Add(int.Parse(array4[0].ToString()), arrayList);
				}
			}
		}
		text = _XmlElement.GetAttribute("Position").ToString();
		array = text.Split(new char[]
		{
			','
		});
		this.EntityComponent.Initial_Position = new Vector3((float)int.Parse(array[0].ToString()) / 100f, (float)int.Parse(array[1].ToString()) / 100f, (float)int.Parse(array[2].ToString()) / 100f);
		text = _XmlElement.GetAttribute("Rotation").ToString();
		array = text.Split(new char[]
		{
			','
		});
		this.EntityComponent.Initial_Rotation = new Vector3((float)int.Parse(array[0].ToString()) / 100f, (float)int.Parse(array[1].ToString()) / 100f, (float)int.Parse(array[2].ToString()) / 100f);
	}

	// Token: 0x04001975 RID: 6517
	private Npc_AI_Entity _EntityComponent;
}
