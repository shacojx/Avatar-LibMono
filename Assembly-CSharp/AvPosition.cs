using System;

// Token: 0x0200002A RID: 42
public class AvPosition
{
	// Token: 0x060001A5 RID: 421 RVA: 0x0000CC72 File Offset: 0x0000B072
	public AvPosition()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000CC8F File Offset: 0x0000B08F
	public AvPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000CCAC File Offset: 0x0000B0AC
	public AvPosition(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000CCD0 File Offset: 0x0000B0D0
	public bool setDetect(AvPosition pos, int num)
	{
		return CRes.abs(pos.x - this.x) <= num && CRes.abs(pos.y - this.y) <= num;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000CD05 File Offset: 0x0000B105
	public bool setDetectX(int xx, int num)
	{
		return CRes.abs(xx - this.x) <= num;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000CD1D File Offset: 0x0000B11D
	public bool setDetectY(int yy, int num)
	{
		return CRes.abs(yy - this.y) <= num;
	}

	// Token: 0x040001AA RID: 426
	public int x;

	// Token: 0x040001AB RID: 427
	public int y;

	// Token: 0x040001AC RID: 428
	public int anchor;

	// Token: 0x040001AD RID: 429
	public int xTo;

	// Token: 0x040001AE RID: 430
	public int yTo;

	// Token: 0x040001AF RID: 431
	public sbyte depth;

	// Token: 0x040001B0 RID: 432
	public short index = -1;
}
