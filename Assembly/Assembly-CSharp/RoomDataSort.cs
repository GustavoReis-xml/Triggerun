using System;
using System.Collections.Generic;

// Token: 0x020002DD RID: 733
internal class RoomDataSort : IComparer<RoomData>
{
	// Token: 0x060021C6 RID: 8646 RVA: 0x0013A008 File Offset: 0x00138208
	public int Compare(RoomData x, RoomData y)
	{
		int result = 0;
		switch (this.sorttype)
		{
		case 0:
			result = DateTime.Parse(x.m_sCreateTime).CompareTo(DateTime.Parse(y.m_sCreateTime));
			break;
		case 1:
			result = x.m_nRoomID.CompareTo(y.m_nRoomID);
			break;
		case 2:
			result = x.m_pRoomFbData.nId.CompareTo(y.m_pRoomFbData.nId);
			break;
		case 3:
			result = x.m_pRoomFbData.nType.CompareTo(y.m_pRoomFbData.nType);
			break;
		case 4:
			result = x.m_nRoomNowNum.CompareTo(y.m_nRoomNowNum);
			break;
		case 5:
			result = x.m_nRoomState.CompareTo(y.m_nRoomState);
			break;
		}
		return result;
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x0013A0E4 File Offset: 0x001382E4
	public void SetCompare(int type)
	{
		this.sorttype = type;
	}

	// Token: 0x04001F59 RID: 8025
	private int sorttype;
}
