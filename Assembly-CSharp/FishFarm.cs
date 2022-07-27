using System;

// Token: 0x0200003E RID: 62
public class FishFarm : AnimalDan
{
	// Token: 0x0600023C RID: 572 RVA: 0x00012D0F File Offset: 0x0001110F
	public FishFarm(int id, sbyte species, sbyte cap) : base(0, 0, id, species)
	{
		this.captainID = (int)cap;
		this.indexFood = 1;
		this.catagory = 7;
		this.waves = new AvPosition(-10, 0, CRes.rnd(8));
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00012D48 File Offset: 0x00011148
	public override void update()
	{
		if (this.period == 2)
		{
			if (this.waves.anchor == 6 || this.waves.x == -10)
			{
				this.waves.x = this.x + (((int)this.direct != (int)Base.RIGHT) ? -3 : 3);
				this.waves.y = this.y + 2;
			}
			this.waves.anchor++;
			if (this.waves.anchor > 17)
			{
				this.waves.anchor = 6;
			}
		}
		AnimalInfo animalByID = FarmData.getAnimalByID((int)this.species);
		this.indexFr = animalByID.arrFrame[(int)this.action][this.frame];
		int num = CRes.rnd(100);
		if (num == 2 && (int)this.zump <= 0 && (int)this.action == 0)
		{
			this.zump = 8;
		}
		if ((int)this.zump > 0)
		{
			this.indexFr = (sbyte)(2 - (int)this.zump / 3 + 2);
			this.zump = (sbyte)((int)this.zump - 1);
			this.hDelta = this.zump;
			if ((int)this.hDelta >= 4)
			{
				this.hDelta = (sbyte)(4 - (int)this.zump % 4);
			}
			this.hDelta = (sbyte)((int)this.hDelta + 5);
			this.hDelta = (sbyte)((int)this.hDelta * -1);
		}
		else
		{
			this.hDelta = 0;
		}
		base.update();
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00012ED9 File Offset: 0x000112D9
	public override void paint(MyGraphics g)
	{
		base.paint(g);
		if (this.period == 2 && this.waves.anchor < 16)
		{
			g.setColor(Fish.color[(int)LoadMap.status]);
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00012F14 File Offset: 0x00011314
	public override void setInit()
	{
		this.posNext = new AvPosition();
		this.x = (this.xCur = (this.posNext.x = FarmScr.posPond.x + CRes.rnd(FarmScr.numTilePond - 1) * 24));
		this.y = (this.yCur = (this.posNext.y = FarmScr.posPond.y + 12 + CRes.rnd(3) * 24));
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00012F98 File Offset: 0x00011398
	public override void setPos()
	{
		base.setPosNext(new AvPosition(FarmScr.posPond.x + 30 + CRes.rnd(FarmScr.numTilePond - 2) * 24, FarmScr.posPond.y + 12 + CRes.rnd(3) * 24));
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00012FE4 File Offset: 0x000113E4
	public override void setFollowPos(AvPosition pos)
	{
		base.setPosNext(new AvPosition(pos.x - 10 + CRes.rnd(20), pos.y - 10 + CRes.rnd(20)));
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00013014 File Offset: 0x00011414
	public override bool detectCollision(int vX, int vY)
	{
		if ((int)this.action == -1)
		{
			this.vx = 0;
			this.vy = 0;
			return true;
		}
		if ((int)this.action != 0 && (int)this.action != 1)
		{
			this.vx = 0;
			this.vy = 0;
			return true;
		}
		this.action = 1;
		int xCur = this.xCur;
		int yCur = this.yCur;
		if (!LoadMap.isTrans(xCur + vX, yCur + vY))
		{
			if (vX != 0)
			{
				if (vX > 0)
				{
					this.vx = this.v;
				}
				else
				{
					this.vx = -this.v;
				}
			}
			if (vY != 0)
			{
				if (vY > 0)
				{
					this.vy = this.v;
				}
				else
				{
					this.vy = -this.v;
				}
			}
			return false;
		}
		this.vx = 0;
		this.vy = 0;
		return true;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x000130F8 File Offset: 0x000114F8
	public override Point getPosEat()
	{
		Point point = (Point)FarmScr.listFood[(int)this.indexFood].elementAt(CRes.rnd(FarmScr.listFood[(int)this.indexFood].size()));
		if (LoadMap.isTrans(point.x, point.y) || point.g != 0)
		{
			return null;
		}
		return point;
	}

	// Token: 0x040002E0 RID: 736
	public new sbyte index;

	// Token: 0x040002E1 RID: 737
	public static int WTile = 5;

	// Token: 0x040002E2 RID: 738
	private AvPosition waves;

	// Token: 0x040002E3 RID: 739
	public sbyte zump;
}
