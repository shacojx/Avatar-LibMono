using System;

// Token: 0x02000002 RID: 2
public class Point : MyObject
{
	// Token: 0x06000001 RID: 1 RVA: 0x0000206F File Offset: 0x0000046F
	public Point()
	{
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002077 File Offset: 0x00000477
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000208D File Offset: 0x0000048D
	public Point(int x, int y, int id)
	{
		this.x = x;
		this.y = y;
		this.xTo = (short)x;
		this.yTo = (short)y;
		this.itemID = (short)id;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020BB File Offset: 0x000004BB
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)CRes.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000004F4
	public int translate()
	{
		if (this.x == (int)this.xTo && this.y == (int)this.yTo)
		{
			return -1;
		}
		if (global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1)
		{
			this.x = (int)this.xTo;
			this.y = (int)this.yTo;
			return 0;
		}
		if (this.x != (int)this.xTo)
		{
			this.x += ((int)this.xTo - this.x) / 2;
		}
		if (this.y != (int)this.yTo)
		{
			this.y += ((int)this.yTo - this.y) / 2;
		}
		if (CRes.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5))
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021FA File Offset: 0x000005FA
	public override void update()
	{
		this.layer.update();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002207 File Offset: 0x00000607
	public override void paint(MyGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000001 RID: 1
	public Layer layer;

	// Token: 0x04000002 RID: 2
	public int g;

	// Token: 0x04000003 RID: 3
	public int v;

	// Token: 0x04000004 RID: 4
	public new int w;

	// Token: 0x04000005 RID: 5
	public new int h;

	// Token: 0x04000006 RID: 6
	public int color;

	// Token: 0x04000007 RID: 7
	public int limitY;

	// Token: 0x04000008 RID: 8
	public int countFr;

	// Token: 0x04000009 RID: 9
	public sbyte dis;

	// Token: 0x0400000A RID: 10
	public short itemID;

	// Token: 0x0400000B RID: 11
	public bool isFire;

	// Token: 0x0400000C RID: 12
	public bool isRemove;

	// Token: 0x0400000D RID: 13
	public short yTo;

	// Token: 0x0400000E RID: 14
	public short xTo;

	// Token: 0x0400000F RID: 15
	public short distant;
}
