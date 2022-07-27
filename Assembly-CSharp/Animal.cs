using System;

// Token: 0x02000024 RID: 36
public class Animal : Base
{
	// Token: 0x0600016E RID: 366 RVA: 0x0000B00B File Offset: 0x0000940B
	public Animal()
	{
		this.catagory = 2;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000B028 File Offset: 0x00009428
	public Animal(int x, int y, int id, sbyte species)
	{
		AnimalInfo animalByID = FarmData.getAnimalByID((int)species);
		this.name = animalByID.name;
		this.catagory = 2;
		base.setPos(x, y);
		this.direct = 0;
		this.action = 0;
		this.IDDB = id;
		this.period = 0;
		this.g = 4;
		this.vy = (int)this.g;
		this.v = 1;
		this.species = species;
		this.frame = CRes.rnd(12);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000B0B7 File Offset: 0x000094B7
	public virtual void setInit()
	{
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000B0BC File Offset: 0x000094BC
	public override void paint(MyGraphics g)
	{
		if ((float)(this.x * MyObject.hd + 30) < AvCamera.gI().xCam || (float)(this.x * MyObject.hd - 30) > AvCamera.gI().xCam + (float)Canvas.w || Canvas.currentMyScreen == MainMenu.gI() || (Canvas.menuMain != null && Canvas.menuMain == Menu.me))
		{
			return;
		}
		AnimalInfo animalByID = FarmData.getAnimalByID((int)this.species);
		ImageIcon imgIcon = AvatarData.getImgIcon(animalByID.idImg[this.period]);
		if (imgIcon.count == -1)
		{
			return;
		}
		if (this.height == 0)
		{
			this.height = (short)((int)imgIcon.h / (int)animalByID.frame);
		}
		if ((int)this.catagory != 7)
		{
			this.indexFr = animalByID.arrFrame[(int)this.action][this.frame];
		}
		g.drawRegion(imgIcon.img, 0f, (float)((int)this.indexFr * (int)this.height), (int)imgIcon.w, (int)this.height, (int)this.direct, (float)(this.x * MyObject.hd), (float)(this.y * MyObject.hd + (int)this.hDelta), ((int)animalByID.area == 4) ? 17 : 33);
		base.paint(g);
		this.paintFocus(g, (int)imgIcon.w, (int)(this.height + 2), this.x * MyObject.hd, this.y * MyObject.hd, LoadMap.focusObj);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000B250 File Offset: 0x00009650
	public override void paintIcon(MyGraphics g, int x, int y, bool isName)
	{
		AnimalInfo animalByID = FarmData.getAnimalByID((int)this.species);
		ImageIcon imgIcon = AvatarData.getImgIcon(animalByID.idImg[this.period]);
		if (imgIcon.count == -1)
		{
			return;
		}
		if (this.height == 0)
		{
			this.height = (short)((int)imgIcon.h / (int)animalByID.frame);
		}
		if ((int)this.catagory != 7)
		{
			this.indexFr = animalByID.arrFrame[(int)this.action][this.frame];
		}
		g.drawRegion(imgIcon.img, 0f, (float)((int)this.indexFr * (int)this.height), (int)imgIcon.w, (int)this.height, (int)this.direct, (float)x, (float)(y + (int)this.hDelta), ((int)animalByID.area == 4) ? 17 : 33);
		this.paintFocus(g, (int)imgIcon.w, (int)(this.height + 2), x, y, this);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000B340 File Offset: 0x00009740
	public void paintFocus(MyGraphics g, int ww, int hh, int x, int y, MyObject obj)
	{
		if ((int)this.catagory == 7)
		{
			hh = 10;
		}
		int num = this.period * 5;
		if (this.disease[0])
		{
			FarmScr.imgBenh.drawFrame(0, x - 10 * MyObject.hd, y - (24 + (int)this.hDelta) * MyObject.hd - hh, 0, 3, g);
		}
		if (this.disease[1])
		{
			FarmScr.imgBenh.drawFrame(1, x + 10 * MyObject.hd, y - (24 + (int)this.hDelta) * MyObject.hd - hh, 0, 3, g);
		}
		PaintPopup.fill(x - (22 + num) * MyObject.hd / 2, y - (18 + (int)this.hDelta) * MyObject.hd - hh, (22 + num) * MyObject.hd, 4 * MyObject.hd, 1, g);
		PaintPopup.fill(x - (21 + num) * MyObject.hd / 2, y - (17 + (int)this.hDelta) * MyObject.hd - hh, this.health * (20 + num) / 100 * MyObject.hd, 2 * MyObject.hd, 65280, g);
		int num2 = FarmData.getAnimalByID((int)this.species).harvestTime * 60 - this.bornTime;
		if (this.bornTime <= FarmData.getAnimalByID((int)this.species).harvestTime * 60)
		{
			Canvas.smallFontYellow.drawString(g, num2 / 60 + ":" + (num2 - num2 / 60 * 60), x, y - (13 + (int)this.hDelta) * MyObject.hd - hh, 2);
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000B4E8 File Offset: 0x000098E8
	public override void update()
	{
		base.update();
		if (this.isStand)
		{
			if (Environment.TickCount / 1000 - this.timeStand > 10)
			{
				this.isStand = false;
			}
			return;
		}
		this.frame++;
		if (this.frame >= 12)
		{
			this.frame = 0;
		}
		this.updateEat();
		if ((int)this.action != 1)
		{
			if (this.frame == 0)
			{
				this.action = (sbyte)CRes.rnd(5 + ((int)this.species - 50) * 5);
				if ((int)this.action != 2)
				{
					this.action = 0;
				}
				else
				{
					this.direct = (sbyte)CRes.rnd((int)Base.RIGHT, (int)Base.LEFT);
				}
			}
			if (this.cycle > 0)
			{
				this.cycle--;
				return;
			}
			this.updatePos();
			if (this.posNext.x > this.x)
			{
				this.direct = Base.RIGHT;
			}
			else
			{
				this.direct = Base.LEFT;
			}
			this.setAngleAndDis();
			this.action = 1;
		}
		else
		{
			this.move();
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000B61C File Offset: 0x00009A1C
	public virtual void updatePos()
	{
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000B61E File Offset: 0x00009A1E
	public virtual void updateEat()
	{
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000B620 File Offset: 0x00009A20
	public virtual void move()
	{
		int num = this.v * (this.disTrans * CRes.cos(CRes.fixangle(this.angle)) >> 10);
		int num2 = -this.v * (this.disTrans * CRes.sin(CRes.fixangle(this.angle))) >> 10;
		if (this.detectCollision(num, num2))
		{
			if (base.setWay(num, num2))
			{
				this.x += this.vx;
				this.y += this.vy;
			}
			this.reset();
			return;
		}
		this.x = this.xCur + num;
		this.y = this.yCur + num2;
		int num3 = CRes.distance(this.xCur, this.yCur, this.x, this.y);
		this.disTrans++;
		if (num3 > this.distant)
		{
			this.reset();
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000B713 File Offset: 0x00009B13
	public void setPosNext(AvPosition pos)
	{
		this.posNext = pos;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000B71C File Offset: 0x00009B1C
	public virtual void setAngleAndDis()
	{
		this.distant = CRes.distance(this.x, this.y, this.posNext.x, this.posNext.y);
		this.angle = CRes.angle(this.posNext.x - this.x, -(this.posNext.y - this.y));
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000B786 File Offset: 0x00009B86
	public virtual void setPos()
	{
		this.setPosNext(new AvPosition(CRes.rnd((int)(LoadMap.wMap * 6)) * 4, CRes.rnd((int)(LoadMap.Hmap * 6)) * 4));
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000B7AF File Offset: 0x00009BAF
	public virtual void reset()
	{
		this.action = 0;
		this.xCur = this.x;
		this.yCur = this.y;
		this.vx = 0;
		this.vy = 0;
		this.disTrans = 0;
	}

	// Token: 0x04000153 RID: 339
	public const sbyte CHICKEN = 50;

	// Token: 0x04000154 RID: 340
	public const sbyte COW = 51;

	// Token: 0x04000155 RID: 341
	public const sbyte PIG = 52;

	// Token: 0x04000156 RID: 342
	public const sbyte DOG = 53;

	// Token: 0x04000157 RID: 343
	public const sbyte ANY_GO = 1;

	// Token: 0x04000158 RID: 344
	public const sbyte LIMIT_CHUONG = 2;

	// Token: 0x04000159 RID: 345
	public const sbyte LIMIT_WATER = 3;

	// Token: 0x0400015A RID: 346
	public int disTrans;

	// Token: 0x0400015B RID: 347
	public int angle;

	// Token: 0x0400015C RID: 348
	public int distant;

	// Token: 0x0400015D RID: 349
	public int period;

	// Token: 0x0400015E RID: 350
	public int cycle;

	// Token: 0x0400015F RID: 351
	public bool isEat;

	// Token: 0x04000160 RID: 352
	public int bornTime;

	// Token: 0x04000161 RID: 353
	public int health;

	// Token: 0x04000162 RID: 354
	public bool isHarvest;

	// Token: 0x04000163 RID: 355
	public bool hunger;

	// Token: 0x04000164 RID: 356
	public bool[] disease = new bool[2];

	// Token: 0x04000165 RID: 357
	public sbyte species;

	// Token: 0x04000166 RID: 358
	public sbyte harvestPer;

	// Token: 0x04000167 RID: 359
	public sbyte hDelta;

	// Token: 0x04000168 RID: 360
	public sbyte indexFr;

	// Token: 0x04000169 RID: 361
	public AvPosition posNext;

	// Token: 0x0400016A RID: 362
	public int numEggOne;

	// Token: 0x0400016B RID: 363
	public bool isStand;

	// Token: 0x0400016C RID: 364
	public int timeStand;
}
