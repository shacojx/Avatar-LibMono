using System;

// Token: 0x02000069 RID: 105
public class PositionTran
{
	// Token: 0x060003A2 RID: 930 RVA: 0x00021536 File Offset: 0x0001F936
	public PositionTran(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0002154C File Offset: 0x0001F94C
	public void update()
	{
		if (this.x != this.xTo)
		{
			this.cmvx = this.xTo - this.x << 2;
			this.cmdx += this.cmvx;
			this.x += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		if (this.y != this.yTo)
		{
			this.cmvy = this.yTo - this.y << 2;
			this.cmdy += this.cmvy;
			this.y += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x040004A4 RID: 1188
	public int x;

	// Token: 0x040004A5 RID: 1189
	public int y;

	// Token: 0x040004A6 RID: 1190
	public int xTo;

	// Token: 0x040004A7 RID: 1191
	public int yTo;

	// Token: 0x040004A8 RID: 1192
	public int cmdx;

	// Token: 0x040004A9 RID: 1193
	public int cmvx;

	// Token: 0x040004AA RID: 1194
	public int cmdy;

	// Token: 0x040004AB RID: 1195
	public int cmvy;
}
