using System;

// Token: 0x02000064 RID: 100
public class Pet : Animal
{
	// Token: 0x06000374 RID: 884 RVA: 0x0001EAD4 File Offset: 0x0001CED4
	static Pet()
	{
		Pet.FRAME[0] = new sbyte[]
		{
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3
		};
		Pet.FRAME[1] = new sbyte[]
		{
			0,
			0,
			0,
			1,
			1,
			1,
			0,
			0,
			0,
			1,
			1,
			1
		};
		Pet.FRAME[2] = new sbyte[]
		{
			2,
			2,
			2,
			3,
			3,
			3,
			2,
			2,
			2,
			3,
			3,
			3
		};
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001EB44 File Offset: 0x0001CF44
	public Pet(Avatar fo)
	{
		this.catagory = 4;
		this.follow = fo;
		this.posNext = new AvPosition();
		this.posNext.x = this.follow.x - 40 + CRes.rnd(80);
		this.posNext.y = this.follow.y - 20 + CRes.rnd(40);
		this.xCur = (this.x = this.posNext.x);
		this.yCur = (this.y = this.posNext.y);
		APartInfo apartInfo = (APartInfo)AvatarData.getPart(this.follow.idPet);
		this.quich = (int)apartInfo.level;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001EC14 File Offset: 0x0001D014
	public override void setPos()
	{
		if (this.listMove.size() > 0)
		{
			AvPosition avPosition = (AvPosition)this.listMove.elementAt(0);
			this.posNext.x = avPosition.x;
			this.posNext.y = avPosition.y;
			this.listMove.removeElementAt(0);
		}
		else
		{
			int num = CRes.rnd(20) - 10;
			if (CRes.abs(this.posNext.x + num - GameMidlet.avatar.x) >= 35)
			{
				num = 0;
			}
			this.posNext.x += num;
			this.posNext.y = this.y;
		}
		if (this.posNext.x < 0)
		{
			this.posNext.x = 5;
		}
		else if (this.posNext.x > (int)(LoadMap.wMap * 24))
		{
			this.posNext.x = (int)(LoadMap.wMap * 24 - 5);
		}
		else if (this.posNext.y < 0)
		{
			this.posNext.y = 5;
		}
		else if (this.posNext.y > (int)(LoadMap.Hmap * 24 - 24))
		{
			this.posNext.y = (int)(LoadMap.Hmap * 24 - 30);
		}
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001ED73 File Offset: 0x0001D173
	public override void updatePos()
	{
		this.setPos();
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001ED7C File Offset: 0x0001D17C
	public override void update()
	{
		if (Canvas.gameTick % (3 - this.quich) == 0)
		{
			this.frame++;
		}
		if (Canvas.gameTick % 1 == 0 && (int)this.action == 1 && this.y == this.yCur && this.isFly)
		{
			if (this.dir == 1)
			{
				this.yFly++;
				if (this.yFly > 3)
				{
					this.dir = -1;
				}
			}
			else
			{
				this.yFly--;
				if (this.yFly < -3)
				{
					this.dir = 1;
				}
			}
		}
		if (this.frame >= 12)
		{
			this.frame = 0;
		}
		this.setPosNext();
		if ((int)this.action != 1)
		{
			if (this.cycle > 0)
			{
				if (this.frame == 0)
				{
					this.action = (sbyte)CRes.rnd(3 + this.quich * 2);
					if ((int)this.action != 2)
					{
						this.action = 0;
					}
					else
					{
						this.direct = (sbyte)CRes.rnd((int)Base.RIGHT, (int)Base.LEFT);
					}
					if (this.isFly)
					{
						this.action = 2;
					}
				}
				this.cycle--;
				if (CRes.distance(this.x, this.y, this.follow.x, this.follow.y) > 35)
				{
					base.reset();
					this.cycle = 0;
					this.v = 4;
				}
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

	// Token: 0x06000379 RID: 889 RVA: 0x0001EF6C File Offset: 0x0001D36C
	private void setPosNext()
	{
		if (this.xFir != this.follow.x || this.yFir != this.follow.y)
		{
			int num = CRes.distance(this.xFir, this.yFir, this.follow.x, this.follow.y);
			if (num > 40)
			{
				int num2 = 10 + CRes.rnd(20);
				if ((int)this.follow.direct == (int)Base.RIGHT)
				{
					num2 = -(10 + CRes.rnd(20));
				}
				if (LoadMap.getTypeMap(this.follow.x + num2, this.follow.y) != 80)
				{
					num2 = 0;
				}
				this.listMove.addElement(new AvPosition(this.follow.x + num2, this.follow.y));
				this.xFir = this.follow.x + num2;
				this.yFir = this.follow.y;
			}
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001F078 File Offset: 0x0001D478
	public override void reset()
	{
		base.reset();
		this.cycle = 50 + CRes.rnd(100);
		if (this.listMove.size() > 0)
		{
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
			this.cycle = 0;
			this.disTrans = 1;
			this.v = 2 + this.quich;
		}
		else
		{
			this.v = 1 + CRes.rnd(this.quich);
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001F124 File Offset: 0x0001D524
	public override void paint(MyGraphics g)
	{
		if ((float)((this.x + 15) * MyObject.hd) < AvCamera.gI().xCam || (float)((this.x - 15) * MyObject.hd) > AvCamera.gI().xCam + (float)Canvas.w || this.follow.ableShow || (Canvas.stypeInt > 0 && Canvas.currentMyScreen == MainMenu.gI()))
		{
			return;
		}
		APartInfo apartInfo = (APartInfo)AvatarData.getPart(this.follow.idPet);
		if (apartInfo.IDPart != -1)
		{
			if (apartInfo.IDPart >= 2000)
			{
				ImageIcon imagePart = AvatarData.getImagePart(apartInfo.imgID[(int)Pet.FRAME[(int)this.action][this.frame]]);
				if (!this.isFly && (int)apartInfo.dy[0] + (int)imagePart.h < -10 && imagePart.h > 0)
				{
					this.isFly = true;
					this.dir = 1;
				}
				if (imagePart.count != -1)
				{
					g.drawImage(Pet.imgShadow[(!this.isFly) ? 1 : 0], (float)(this.x * MyObject.hd), (float)((this.y - 1) * MyObject.hd), 3);
					g.drawRegion(imagePart.img, 0f, 0f, (int)imagePart.w, (int)imagePart.h, (int)this.direct, (float)(this.x * MyObject.hd - imagePart.img.w / 2), (float)((this.y - this.yFly) * MyObject.hd + (int)apartInfo.dy[(int)Pet.FRAME[(int)this.action][this.frame]] * MyObject.hd), 0);
				}
			}
			else
			{
				ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[(int)Pet.FRAME[(int)this.action][this.frame]]];
				if (!this.isFly && (int)apartInfo.dy[0] + (int)imageInfo.h * MyObject.hd < -10 && imageInfo.h > 0)
				{
					this.isFly = true;
					this.dir = 1;
				}
				g.drawImage(Pet.imgShadow[(!this.isFly) ? 1 : 0], (float)(this.x * MyObject.hd), (float)((this.y - 1) * MyObject.hd), 3);
				g.drawRegion(AvatarData.getBigImgInfo((int)imageInfo.bigID).img, (float)((int)imageInfo.x0 * MyObject.hd), (float)((int)imageInfo.y0 * MyObject.hd), (int)imageInfo.w * MyObject.hd, (int)imageInfo.h * MyObject.hd, (int)this.direct, (float)(this.x * MyObject.hd + (int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * MyObject.hd - (((int)this.direct != (int)Base.LEFT) ? 0 : ((int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * AvMain.hd * 2 + (int)imageInfo.w * AvMain.hd))), (float)((this.y + this.yFly) * MyObject.hd + (int)apartInfo.dy[(int)Pet.FRAME[(int)this.action][this.frame]] * MyObject.hd), 0);
			}
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001F4A0 File Offset: 0x0001D8A0
	public void paintIcon(MyGraphics g, int x, int y, int hunger)
	{
		APartInfo apartInfo = (APartInfo)AvatarData.getPart(this.follow.idPet);
		if (apartInfo.IDPart != -1)
		{
			int num = y + (int)apartInfo.dy[(int)Pet.FRAME[(int)this.action][this.frame]];
			PaintPopup.fill(x - 10, num - 10, 20, 3, 11381824, g);
			g.setColor(11072024);
			g.drawRect((float)(x - 10), (float)(num - 10), 20f, 3f);
			PaintPopup.fill(x - 9, num - 9, hunger * 20 / 100, 2, 16644608, g);
			if (apartInfo.IDPart >= 2000)
			{
				ImageIcon imagePart = AvatarData.getImagePart(apartInfo.imgID[(int)Pet.FRAME[(int)this.action][this.frame]]);
				if (imagePart.count != -1)
				{
					g.drawImage(Pet.imgShadow[(!this.isFly) ? 1 : 0], (float)x, (float)(y - 1), 3);
					g.drawRegion(imagePart.img, 0f, 0f, (int)imagePart.w, (int)imagePart.h, (int)this.direct, (float)(x + (int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * MyObject.hd - (((int)this.direct != (int)Base.LEFT) ? 0 : ((int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * AvMain.hd * 2 + (int)imagePart.w * AvMain.hd))), (float)(num + this.yFly), 0);
				}
			}
			else
			{
				ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[(int)Pet.FRAME[(int)this.action][this.frame]]];
				g.drawImage(Pet.imgShadow[(!this.isFly) ? 1 : 0], (float)x, (float)(y - 1), 3);
				g.drawRegion(AvatarData.getBigImgInfo((int)imageInfo.bigID).img, (float)((int)imageInfo.x0 * MyObject.hd), (float)((int)imageInfo.y0 * MyObject.hd), (int)imageInfo.w * MyObject.hd, (int)imageInfo.h * MyObject.hd, (int)this.direct, (float)(x + (int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * MyObject.hd - (((int)this.direct != (int)Base.LEFT) ? 0 : ((int)apartInfo.dx[(int)Pet.FRAME[(int)this.action][this.frame]] * AvMain.hd * 2 + (int)imageInfo.w * AvMain.hd))), (float)(num + this.yFly), 0);
			}
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001F764 File Offset: 0x0001DB64
	public override void move()
	{
		int num = this.v * (int)this.follow.hungerPet / 100;
		if (this.follow.hungerPet >= 70)
		{
			num = this.v;
		}
		if (num < 1)
		{
			num = 1;
		}
		int num2 = num * (this.disTrans * CRes.cos(CRes.fixangle(this.angle)) >> 10);
		int num3 = -num * (this.disTrans * CRes.sin(CRes.fixangle(this.angle))) >> 10;
		this.x = this.xCur + num2;
		this.y = this.yCur + num3;
		int num4 = CRes.distance(this.xCur, this.yCur, this.x, this.y);
		this.disTrans++;
		if (num4 > this.distant)
		{
			this.reset();
		}
	}

	// Token: 0x0400045D RID: 1117
	public Avatar follow;

	// Token: 0x0400045E RID: 1118
	private MyVector listMove = new MyVector();

	// Token: 0x0400045F RID: 1119
	private int xFir;

	// Token: 0x04000460 RID: 1120
	private int yFir;

	// Token: 0x04000461 RID: 1121
	private int quich;

	// Token: 0x04000462 RID: 1122
	private int yFly;

	// Token: 0x04000463 RID: 1123
	private int dir;

	// Token: 0x04000464 RID: 1124
	private bool isFly;

	// Token: 0x04000465 RID: 1125
	public static Image[] imgShadow = new Image[2];

	// Token: 0x04000466 RID: 1126
	private static sbyte[][] FRAME = new sbyte[3][];
}
