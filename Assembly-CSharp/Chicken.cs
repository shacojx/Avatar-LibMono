using System;

// Token: 0x020000B4 RID: 180
public class Chicken : AnimalDan
{
	// Token: 0x06000595 RID: 1429 RVA: 0x00034F3A File Offset: 0x0003333A
	public Chicken(int id, sbyte species, sbyte cap) : base(0, 0, id, species)
	{
		this.captainID = (int)cap;
		this.indexFood = 0;
		Chicken.numChicken++;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00034F64 File Offset: 0x00033364
	public override void setInit()
	{
		this.posNext = new AvPosition();
		if (this.captainID == this.IDDB)
		{
			this.x = (this.xCur = (this.posNext.x = (FarmScr.numTileBarn + 3) * 24 + this.setX()));
			this.y = (this.yCur = (this.posNext.y = 72 + CRes.rnd(24) * 4));
		}
		else
		{
			this.updatePos();
			if (!LoadMap.isTrans(this.x, this.y))
			{
				base.setPosNext(new AvPosition((FarmScr.numTileBarn + 3) * 24 + this.setX(), 72 + CRes.rnd(24) * 4));
			}
			this.x = (this.xCur = this.posNext.x);
			this.y = (this.yCur = this.posNext.y);
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00035064 File Offset: 0x00033464
	public int setX()
	{
		int num = (int)LoadMap.wMap - FarmScr.numTilePond - FarmScr.numTileBarn - 5;
		return CRes.rnd(num * 6) * 4;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0003508F File Offset: 0x0003348F
	public override void setFollowPos(AvPosition pos)
	{
		base.setPosNext(new AvPosition(pos.x - 48 + this.setX(), pos.y - 48 + CRes.rnd(24) * 4));
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000350BF File Offset: 0x000334BF
	public override void setPos()
	{
		base.setPos();
	}

	// Token: 0x040007D9 RID: 2009
	public static int numChicken;

	// Token: 0x040007DA RID: 2010
	public static AvPosition posNest;
}
