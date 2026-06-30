using System;
using System.Xml;

// Token: 0x0200015A RID: 346
public class Graphic_Card_Ladder_Xml_Data
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0009068C File Offset: 0x0008E88C
	// (set) Token: 0x06000FBC RID: 4028 RVA: 0x000906BE File Offset: 0x0008E8BE
	public Graphic_Card_Ladder_Entity Entity_Component
	{
		get
		{
			bool flag = this._Entity_Component == null;
			if (flag)
			{
				this._Entity_Component = new Graphic_Card_Ladder_Entity();
			}
			return this._Entity_Component;
		}
		set
		{
			this._Entity_Component = value;
		}
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x000906C8 File Offset: 0x0008E8C8
	public void Initialize(XmlElement _XmlElement)
	{
		this.Entity_Component.Name = _XmlElement.GetAttribute("Name").ToString();
		this.Entity_Component.Score = (float)Fun.Convert_Int(_XmlElement.GetAttribute("Score"));
	}

	// Token: 0x04000C70 RID: 3184
	private Graphic_Card_Ladder_Entity _Entity_Component;
}
