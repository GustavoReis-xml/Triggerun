using System;
using System.Xml;

// Token: 0x0200026E RID: 622
public class Npc_Xml_Data
{
	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0010B010 File Offset: 0x00109210
	// (set) Token: 0x06001CFB RID: 7419 RVA: 0x0010B028 File Offset: 0x00109228
	public int Index
	{
		get
		{
			return this._Index;
		}
		set
		{
			this._Index = value;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0010B034 File Offset: 0x00109234
	// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0010B066 File Offset: 0x00109266
	public Npc_Entity EntityComponent
	{
		get
		{
			bool flag = this._EntityComponent == null;
			if (flag)
			{
				this._EntityComponent = new Npc_Entity();
			}
			return this._EntityComponent;
		}
		set
		{
			this._EntityComponent = value;
		}
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x0010B070 File Offset: 0x00109270
	public void Initialize(XmlElement _XmlElement)
	{
		this.Index = Fun.Convert_Int(_XmlElement.GetAttribute("Index"));
		this.EntityComponent.Index = this.Index;
		this.EntityComponent.Type = Fun.Convert_Int(_XmlElement.GetAttribute("Type"));
		this.EntityComponent.Name = _XmlElement.GetAttribute("Name");
		this.EntityComponent.Resouce_Name = _XmlElement.GetAttribute("Resouce_Name");
		this.EntityComponent.Max_HP = Fun.Convert_Int(_XmlElement.GetAttribute("HP"));
		this.EntityComponent.Max_Shield = Fun.Convert_Int(_XmlElement.GetAttribute("Shield"));
		this.EntityComponent.Weapon_Index = Fun.Convert_Int(_XmlElement.GetAttribute("Weapon_Index"));
		this.EntityComponent.Detection_Range = Fun.Convert_Float(_XmlElement.GetAttribute("Detection_Range"), 100);
		this.EntityComponent.Create_Sound = _XmlElement.GetAttribute("Create_Sound");
		this.EntityComponent.Time = Fun.Convert_Float(_XmlElement.GetAttribute("Time"), 1000);
		this.EntityComponent.Gravity = Fun.Convert_Boolean(_XmlElement.GetAttribute("Gravity"));
		this.EntityComponent.Can_Buff = Fun.Convert_Boolean(_XmlElement.GetAttribute("Can_Buff"));
		this.EntityComponent.Death_Effect = _XmlElement.GetAttribute("Death_Effect");
		this.EntityComponent.Adhere = Fun.Convert_Int(_XmlElement.GetAttribute("Adhere"));
		this.EntityComponent.Need_Change_Layer = Fun.Convert_Boolean(_XmlElement.GetAttribute("Need_Change_Layer"));
		this.EntityComponent.Icon = _XmlElement.GetAttribute("Icon");
		this.EntityComponent.SignHeight = Fun.Convert_Int(_XmlElement.GetAttribute("SignHeight"));
		this.EntityComponent.Death_Sound = _XmlElement.GetAttribute("Death_Sound");
		this.EntityComponent.Stunt = Fun.Convert_Int(_XmlElement.GetAttribute("Stunt"));
		this.EntityComponent.ShowHp = Fun.Convert_Int(_XmlElement.GetAttribute("showhp"));
		this.EntityComponent.AI_ID = Fun.Convert_Int(_XmlElement.GetAttribute("AI_ID"));
		this.EntityComponent.Cure = Fun.Convert_Int(_XmlElement.GetAttribute("Cure"));
		this.EntityComponent.Show_Locked = Fun.Convert_Int(_XmlElement.GetAttribute("Show_Locked"));
		this.EntityComponent.Force_Impact = Fun.Convert_Boolean(_XmlElement.GetAttribute("damageforce"));
		this.EntityComponent.Scale_Multiple = Fun.Convert_Float(_XmlElement.GetAttribute("model_size"), 100);
		this.EntityComponent.Focus = _XmlElement.GetAttribute("Focus").ToString();
		this.EntityComponent.Physics = Fun.Convert_Int(_XmlElement.GetAttribute("Physics"));
		this.EntityComponent.Mass = Fun.Convert_Float(_XmlElement.GetAttribute("Mass"), 100);
		this.EntityComponent.camerswitch = Fun.Convert_Int(_XmlElement.GetAttribute("camerswitch"));
		this.EntityComponent.Camera_Distance = Fun.Convert_Float(_XmlElement.GetAttribute("Camera_Distance").ToString(), 100);
	}

	// Token: 0x04001973 RID: 6515
	private int _Index;

	// Token: 0x04001974 RID: 6516
	private Npc_Entity _EntityComponent;
}
