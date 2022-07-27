using System;

// Token: 0x0200006A RID: 106
public class RoomInfo
{
	// Token: 0x060003A4 RID: 932 RVA: 0x00021613 File Offset: 0x0001FA13
	public RoomInfo(sbyte id, sbyte roomFree, sbyte roomWait, sbyte lv)
	{
		this.id = id;
		this.roomFree = roomFree;
		this.roomWait = roomWait;
		this.lv = lv;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00021638 File Offset: 0x0001FA38
	public RoomInfo()
	{
	}

	// Token: 0x040004AC RID: 1196
	public sbyte id;

	// Token: 0x040004AD RID: 1197
	public sbyte roomFree;

	// Token: 0x040004AE RID: 1198
	public sbyte roomWait;

	// Token: 0x040004AF RID: 1199
	public sbyte lv;
}
