using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class Npc_Entity_Derive : MonoBehaviour
{
	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0010A57C File Offset: 0x0010877C
	// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0010A594 File Offset: 0x00108794
	public int Npc_ID
	{
		get
		{
			return this._Npc_ID;
		}
		set
		{
			this._Npc_ID = value;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0010A5A0 File Offset: 0x001087A0
	// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0010A5B8 File Offset: 0x001087B8
	public int Country
	{
		get
		{
			return this._Country;
		}
		set
		{
			this._Country = value;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0010A5C4 File Offset: 0x001087C4
	// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x0010A5DC File Offset: 0x001087DC
	public Vector3 Origin_Point
	{
		get
		{
			return this._Origin_Point;
		}
		set
		{
			this._Origin_Point = value;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0010A5E8 File Offset: 0x001087E8
	// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x0010A600 File Offset: 0x00108800
	public Vector3 Origin_Angle
	{
		get
		{
			return this._Origin_Angle;
		}
		set
		{
			this._Origin_Angle = value;
		}
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x0010A60A File Offset: 0x0010880A
	public void Initialize(int _Index)
	{
		this.Fill_Xml_Data(_Index);
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x0010A618 File Offset: 0x00108818
	public void Fill_Xml_Data(int _Index)
	{
		Npc_Entity_Derive npc_Entity_Derive = fGameManager.mloadConfig._Npc_Derive_Xml_Data[_Index];
		this.Npc_ID = npc_Entity_Derive.Npc_ID;
		this.Country = npc_Entity_Derive.Country;
		this.Origin_Point = npc_Entity_Derive.Origin_Point;
		this.Origin_Angle = npc_Entity_Derive.Origin_Angle;
	}

	// Token: 0x0400196A RID: 6506
	private int _Npc_ID;

	// Token: 0x0400196B RID: 6507
	private int _Country;

	// Token: 0x0400196C RID: 6508
	private Vector3 _Origin_Point = Vector3.zero;

	// Token: 0x0400196D RID: 6509
	private Vector3 _Origin_Angle = Vector3.zero;
}
