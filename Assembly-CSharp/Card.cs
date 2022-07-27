using System;

// Token: 0x020000B8 RID: 184
public class Card
{
	// Token: 0x060005DB RID: 1499 RVA: 0x00036F87 File Offset: 0x00035387
	public Card(sbyte ID, bool isPhom) : this(ID)
	{
		if (isPhom)
		{
			this.cardMapping = new int[]
			{
				11,
				12,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10
			};
		}
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00036FB0 File Offset: 0x000353B0
	public Card(sbyte ID)
	{
		this.cardID = ID;
		this.phom = 0;
		this.cardType = (int)this.cardID % 4;
		this.cardValue = (int)this.cardID / 4;
		this.cardColor = ((this.cardType >= 2) ? 1 : 0);
		this.cardMapping = new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12
		};
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00037020 File Offset: 0x00035420
	public static void copyData(Card a, Card b)
	{
		a.phom = b.phom;
		a.cardID = b.cardID;
		a.isSelected = b.isSelected;
		a.isShow = b.isShow;
		a.isUp = b.isUp;
		for (int i = 0; i < b.cardMapping.Length; i++)
		{
			a.cardMapping[i] = b.cardMapping[i];
		}
		a.cardType = b.cardType;
		a.cardValue = b.cardValue;
		a.cardColor = b.cardColor;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000370B6 File Offset: 0x000354B6
	public void paintHalf(MyGraphics g)
	{
		Canvas.paint.paintHalf(g, this);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x000370C4 File Offset: 0x000354C4
	public void paintHalfBackFull(MyGraphics g)
	{
		Canvas.paint.paintHalfBackFull(g, this);
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x000370D2 File Offset: 0x000354D2
	public void paintFull(MyGraphics g)
	{
		Canvas.paint.paintFull(g, this);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000370E0 File Offset: 0x000354E0
	public void paintSmall(MyGraphics g, bool isCh)
	{
		Canvas.paint.paintSmall(g, this, isCh);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x000370EF File Offset: 0x000354EF
	public void setPosTo(int xT, int yT)
	{
		this.xTo = xT;
		this.yTo = yT;
		this.distant = CRes.distance(this.x, this.y, this.xTo, this.yTo);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00037124 File Offset: 0x00035524
	public int translate()
	{
		if (this.x == this.xTo && this.y == this.yTo)
		{
			return -1;
		}
		if (global::Math.abs((this.xTo - this.x) / 2) <= 1 && global::Math.abs((this.yTo - this.y) / 2) <= 1)
		{
			this.x = this.xTo;
			this.y = this.yTo;
			return 0;
		}
		if (this.y != this.yTo)
		{
			this.cmvy = this.yTo - this.y << 2;
			this.cmdy += this.cmvy;
			this.y += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		if (this.x != this.xTo)
		{
			this.cmvx = this.xTo - this.x << 2;
			this.cmdx += this.cmvx;
			this.x += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		if (CRes.distance(this.x, this.y, this.xTo, this.yTo) >= this.distant - this.distant / 4)
		{
			return 3;
		}
		if (CRes.distance(this.x, this.y, this.xTo, this.yTo) <= this.distant / 5)
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x000372C0 File Offset: 0x000356C0
	public bool setCollision()
	{
		return Canvas.px >= this.x - BoardScr.wCard / 2 && Canvas.px <= this.x + BoardScr.wCard / 2 && Canvas.py >= this.y - BoardScr.hcard / 2 && Canvas.py <= this.y + BoardScr.hcard / 2;
	}

	// Token: 0x040007FE RID: 2046
	public sbyte phom;

	// Token: 0x040007FF RID: 2047
	public sbyte cardID;

	// Token: 0x04000800 RID: 2048
	public int x;

	// Token: 0x04000801 RID: 2049
	public int y;

	// Token: 0x04000802 RID: 2050
	public int distant;

	// Token: 0x04000803 RID: 2051
	public bool isSelected;

	// Token: 0x04000804 RID: 2052
	public bool isShow;

	// Token: 0x04000805 RID: 2053
	public bool isUp;

	// Token: 0x04000806 RID: 2054
	public int cmdy;

	// Token: 0x04000807 RID: 2055
	public int cmvy;

	// Token: 0x04000808 RID: 2056
	public int cmdx;

	// Token: 0x04000809 RID: 2057
	public int cmvx;

	// Token: 0x0400080A RID: 2058
	public int[] cardMapping;

	// Token: 0x0400080B RID: 2059
	public int cardType;

	// Token: 0x0400080C RID: 2060
	public int cardValue;

	// Token: 0x0400080D RID: 2061
	public int cardColor;

	// Token: 0x0400080E RID: 2062
	public int yTo;

	// Token: 0x0400080F RID: 2063
	public int xTo;
}
