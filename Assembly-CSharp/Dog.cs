using System;

// Token: 0x020000B5 RID: 181
public class Dog : Animal
{
	// Token: 0x0600059B RID: 1435 RVA: 0x000350C9 File Offset: 0x000334C9
	public Dog()
	{
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000350D1 File Offset: 0x000334D1
	public Dog(int id, sbyte species) : base(0, 0, id, species)
	{
		Dog.numBer = (sbyte)((int)Dog.numBer + 1);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x000350EB File Offset: 0x000334EB
	public override void setInit()
	{
		base.setPos((FarmScr.numTileBarn + 3) * 24 + this.setX(), 48 + CRes.rnd(30) * 4);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00035110 File Offset: 0x00033510
	public int setX()
	{
		int num = (int)LoadMap.wMap - FarmScr.numTilePond - FarmScr.numTileBarn - 5;
		return CRes.rnd(num * 6) * 4;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0003513C File Offset: 0x0003353C
	public override void updateEat()
	{
		if (Dog.isHound && FarmScr.idFarm != GameMidlet.avatar.IDDB)
		{
			if (this.v < 3)
			{
				this.reset();
			}
			if (!Dog.isCan && CRes.abs(GameMidlet.avatar.x - this.x) <= 24 && CRes.abs(GameMidlet.avatar.y - this.y) <= 24)
			{
				Dog.isCan = true;
				Canvas.startOK(T.youAreBittenByDog, new Dog.IActionBitten());
				Canvas.msgdlg.setDelay(20);
			}
		}
		if (Dog.itemID == -1)
		{
			this.isEat = false;
			return;
		}
		if (!this.hunger || this.isEat)
		{
			return;
		}
		this.isEat = true;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0003520F File Offset: 0x0003360F
	public override void updatePos()
	{
		this.posNext = new AvPosition();
		this.setPos();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00035224 File Offset: 0x00033624
	public override void setPos()
	{
		if (Dog.isHound && FarmScr.idFarm != GameMidlet.avatar.IDDB)
		{
			if (this.health > 20 && !this.hunger)
			{
				this.v = 8;
			}
			else
			{
				this.v = 6;
			}
			base.setPosNext(new AvPosition(GameMidlet.avatar.x, GameMidlet.avatar.y));
			return;
		}
		if (this.isEat)
		{
			this.v = 2;
			base.setPosNext(Dog.posDosTr);
			return;
		}
		base.setPosNext(new AvPosition(288 + CRes.rnd(126) * 4, 24 + CRes.rnd(36) * 4));
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000352E0 File Offset: 0x000336E0
	public override void reset()
	{
		if (!this.isEat && CRes.random(2) == 0)
		{
			this.cycle = 200;
		}
		if (this.isEat && CRes.distance(Dog.posDosTr.x, Dog.posDosTr.y, this.x, this.y) < 18)
		{
			this.isEat = false;
			this.hunger = false;
			this.cycle = 200;
			if (FarmScr.gI().doEat(Dog.itemID, this.IDDB))
			{
				Dog.itemID = -1;
			}
		}
		base.reset();
		if (Dog.isHound && FarmScr.idFarm != GameMidlet.avatar.IDDB)
		{
			this.cycle = 0;
		}
	}

	// Token: 0x040007DB RID: 2011
	public static bool isHound;

	// Token: 0x040007DC RID: 2012
	public static bool isCan;

	// Token: 0x040007DD RID: 2013
	public static AvPosition posDosTr;

	// Token: 0x040007DE RID: 2014
	public static sbyte numBer;

	// Token: 0x040007DF RID: 2015
	public static short itemID = -1;

	// Token: 0x020000B6 RID: 182
	private class IActionBitten : IAction
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x000353B9 File Offset: 0x000337B9
		public void perform()
		{
			FarmScr.gI().onBittenByDog();
			Canvas.endDlg();
		}
	}
}
