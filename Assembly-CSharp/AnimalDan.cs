using System;

// Token: 0x02000025 RID: 37
public class AnimalDan : Animal
{
	// Token: 0x0600017C RID: 380 RVA: 0x0000B7E5 File Offset: 0x00009BE5
	public AnimalDan(int x, int y, int id, sbyte species) : base(x, y, id, species)
	{
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000B7F2 File Offset: 0x00009BF2
	public override void update()
	{
		base.update();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000B7FA File Offset: 0x00009BFA
	public override void setAngleAndDis()
	{
		base.setAngleAndDis();
		if (!this.isEat && this.IDDB == this.captainID && this.distant > 150)
		{
			this.distant = 150;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000B83C File Offset: 0x00009C3C
	public virtual Point getPosEat()
	{
		return (Point)FarmScr.listFood[(int)this.indexFood].elementAt(CRes.rnd(FarmScr.listFood[(int)this.indexFood].size()));
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000B87C File Offset: 0x00009C7C
	public override void updatePos()
	{
		if (!this.isEat && this.captainID == this.IDDB)
		{
			this.setPos();
		}
		else
		{
			AvPosition avPosition = new AvPosition();
			if (this.isEat && FarmScr.listFood[(int)this.indexFood].size() > 0)
			{
				Point posEat = this.getPosEat();
				if (posEat != null)
				{
					avPosition.x = posEat.x;
					avPosition.y = posEat.y;
					this.v = 2;
					base.setPosNext(avPosition);
				}
				else
				{
					this.setPos();
				}
			}
			else
			{
				int num = LoadMap.playerLists.size();
				for (int i = 0; i < num; i++)
				{
					Base @base = (Base)LoadMap.playerLists.elementAt(i);
					if (@base is AnimalDan && @base.IDDB == this.captainID)
					{
						avPosition = new AvPosition(@base.x, @base.y);
						break;
					}
				}
				if ((int)this.indexFood != 1 && !LoadMap.isTrans(this.x, this.y))
				{
					this.setPos();
				}
				else
				{
					this.setFollowPos(avPosition);
				}
			}
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000B9B7 File Offset: 0x00009DB7
	public virtual void setFollowPos(AvPosition pos)
	{
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000B9BC File Offset: 0x00009DBC
	public override void reset()
	{
		int num = FarmScr.listFood[(int)this.indexFood].size();
		if (this.hunger && this.isEat && num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				Point point = (Point)FarmScr.listFood[(int)this.indexFood].elementAt(i);
				if (CRes.abs(point.x - this.x) <= 2 && CRes.abs(point.y - this.y) <= 2)
				{
					FarmScr.listFood[(int)this.indexFood].removeElement(point);
					LoadMap.dynamicLists.removeElement(point);
					this.hunger = false;
					this.isEat = false;
					this.v = 1;
					FarmScr.gI().doEat(point.itemID, this.IDDB);
					break;
				}
			}
		}
		base.reset();
		this.cycle = 100 - ((this.captainID == this.IDDB) ? 0 : ((int)this.indexFood * CRes.rnd(70)));
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000BAD8 File Offset: 0x00009ED8
	public override void updateEat()
	{
		if (FarmScr.listFood[(int)this.indexFood].size() == 0)
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

	// Token: 0x0400016D RID: 365
	public int captainID;

	// Token: 0x0400016E RID: 366
	public sbyte indexFood;
}
