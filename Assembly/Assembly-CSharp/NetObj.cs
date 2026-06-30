using System;

// Token: 0x02000256 RID: 598
public class NetObj
{
	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00101128 File Offset: 0x000FF328
	// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00101140 File Offset: 0x000FF340
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
		}
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0010114C File Offset: 0x000FF34C
	public BaseSocket getCs()
	{
		return this.cs;
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x00101164 File Offset: 0x000FF364
	public void setCs(BaseSocket cs)
	{
		this.cs = cs;
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x00101170 File Offset: 0x000FF370
	public int getNetid()
	{
		return this.netid;
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x00101188 File Offset: 0x000FF388
	public virtual void setNetid(int netid)
	{
		this.netid = netid;
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x00101192 File Offset: 0x000FF392
	public void setUobj(UserSocket obj)
	{
		this.uObj = obj;
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x0010119C File Offset: 0x000FF39C
	public virtual void OnDestroy()
	{
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x0010119F File Offset: 0x000FF39F
	public virtual void OnCreate()
	{
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x001011A2 File Offset: 0x000FF3A2
	public virtual void OnNetDestroy()
	{
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x001011A8 File Offset: 0x000FF3A8
	public virtual void TosFun(string funname, params object[] args)
	{
		bool flag = this.cs != null;
		if (flag)
		{
			this.cs.ToServerFunReady(this, false, funname, args);
		}
		else
		{
			Fun.SocketLog("TosFun: " + funname + "," + BaseSocket.GetShortStr(args).ToString());
		}
	}

	// Token: 0x04001883 RID: 6275
	private int netid;

	// Token: 0x04001884 RID: 6276
	public BaseSocket cs;

	// Token: 0x04001885 RID: 6277
	public UserSocket uObj;

	// Token: 0x04001886 RID: 6278
	public bool isMainPlayer;

	// Token: 0x04001887 RID: 6279
	private string _Name;

	// Token: 0x04001888 RID: 6280
	public int netobject_type;

	// Token: 0x04001889 RID: 6281
	public bool bePlayer;

	// Token: 0x0400188A RID: 6282
	public bool filmObj;

	// Token: 0x0400188B RID: 6283
	public bool isCopy;

	// Token: 0x0400188C RID: 6284
	public bool filmMainLayer;

	// Token: 0x0400188D RID: 6285
	public float pingDelayBegin;

	// Token: 0x0400188E RID: 6286
	public bool delayNet;
}
