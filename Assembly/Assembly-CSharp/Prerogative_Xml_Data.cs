using System;
using System.Xml;

// Token: 0x020002A6 RID: 678
public class Prerogative_Xml_Data
{
	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0011E830 File Offset: 0x0011CA30
	// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0011E862 File Offset: 0x0011CA62
	public Prerogative_Entity Entity_Component
	{
		get
		{
			bool flag = this._Entity_Component == null;
			if (flag)
			{
				this._Entity_Component = new Prerogative_Entity();
			}
			return this._Entity_Component;
		}
		set
		{
			this._Entity_Component = value;
		}
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x0011E86C File Offset: 0x0011CA6C
	public void Initialize(XmlElement _XmlElement)
	{
		this.Entity_Component.ID = Fun.Convert_Int(_XmlElement.GetAttribute("id").ToString());
		this.Entity_Component.Name = _XmlElement.GetAttribute("name").ToString();
		this.Entity_Component.Reward_Data = _XmlElement.GetAttribute("itemlist").ToString();
	}

	// Token: 0x04001C75 RID: 7285
	private Prerogative_Entity _Entity_Component;
}
