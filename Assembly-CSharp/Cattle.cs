using System;

// Token: 0x020000B3 RID: 179
public class Cattle : Animal
{
	// Token: 0x0600058D RID: 1421 RVA: 0x00034D8C File Offset: 0x0003318C
	public Cattle()
	{
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00034D94 File Offset: 0x00033194
	public Cattle(int id, sbyte species) : base(0, 0, id, species)
	{
		Cattle.numPig = (sbyte)((int)Cattle.numPig + 1);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00034DAE File Offset: 0x000331AE
	public override void setInit()
	{
		base.setPos(FarmScr.posBarn.x + 48 + CRes.rnd((FarmScr.numTileBarn - 2) * 6) * 4, FarmScr.posBarn.y + 24 + CRes.rnd(12) * 4);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00034DEC File Offset: 0x000331EC
	public override void updatePos()
	{
		this.posNext = new AvPosition();
		if (!this.isEat)
		{
			base.setPosNext(new AvPosition(FarmScr.posBarn.x + 12 + CRes.rnd(FarmScr.numTileBarn * 6) * 4, FarmScr.posBarn.y + 12 + CRes.rnd(18) * 4));
		}
		else
		{
			this.doTranToFood();
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00034E58 File Offset: 0x00033258
	private void doTranToFood()
	{
		base.setPosNext(Cattle.posPigTr);
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00034E65 File Offset: 0x00033265
	public override void updateEat()
	{
		if (!this.hunger || this.isEat)
		{
			return;
		}
		if (Cattle.itemID != -1)
		{
			this.isEat = true;
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00034E90 File Offset: 0x00033290
	public override void reset()
	{
		base.reset();
		if (this.isEat && CRes.abs(Cattle.posPigTr.x - this.x) < 20 && CRes.abs(Cattle.posPigTr.y - this.y) < 15)
		{
			this.isEat = false;
			this.hunger = false;
			if (FarmScr.gI().doEat(Cattle.itemID, this.IDDB))
			{
				Cattle.itemID = -1;
			}
		}
		this.cycle = 100 + 50 * ((int)this.species - 50);
	}

	// Token: 0x040007D4 RID: 2004
	public static AvPosition posPigTr;

	// Token: 0x040007D5 RID: 2005
	public static AvPosition posBucket;

	// Token: 0x040007D6 RID: 2006
	public static sbyte numPig;

	// Token: 0x040007D7 RID: 2007
	public static sbyte numTileW = 5;

	// Token: 0x040007D8 RID: 2008
	public static short itemID = -1;
}
