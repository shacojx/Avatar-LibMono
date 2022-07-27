using System;

// Token: 0x0200002B RID: 43
public class Avatar : Base
{
	// Token: 0x060001AB RID: 427 RVA: 0x0000CD38 File Offset: 0x0000B138
	public Avatar()
	{
		int[] array = new int[4];
		array[1] = -1;
		this.money = array;
		this.strMoney = string.Empty;
		this.moveList = new MyVector();
		this.typeHome = -1;
		this.lvFarm = -1;
		this.isJumps = -1;
		this.feel = 4;
		this.idPet = -1;
		this.idImg = -1;
		this.idWedding = -1;
		this.idStatus = -1;
		this.blogNews = -1;
		this.countDefent = -1;
		this.hp = 1000;
		this.mp = 300;
		this.maxHP = 1000;
		this.maxMP = 1000;
		this.indexChat = -1;
		base..ctor();
		this.catagory = 0;
		this.height = 42;
		this.cFrame = (sbyte)CRes.rnd(9);
		this.maxJump = (sbyte)(CRes.rnd(30) + 10);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000CE18 File Offset: 0x0000B218
	static Avatar()
	{
		Avatar.FRAME[0] = new sbyte[]
		{
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1
		};
		Avatar.FRAME[1] = new sbyte[]
		{
			0,
			0,
			0,
			0,
			0,
			2,
			2,
			2,
			2,
			2
		};
		Avatar.FRAME[2] = new sbyte[]
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
			3
		};
		Avatar.FRAME[3] = new sbyte[]
		{
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};
		Avatar.FRAME[4] = new sbyte[]
		{
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};
		Avatar.FRAME[5] = new sbyte[]
		{
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6
		};
		Avatar.FRAME[6] = new sbyte[]
		{
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7
		};
		Avatar.FRAME[7] = new sbyte[]
		{
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8
		};
		Avatar.FRAME[8] = new sbyte[]
		{
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9
		};
		Avatar.FRAME[9] = new sbyte[]
		{
			10,
			10,
			10,
			10,
			10,
			10,
			10,
			10,
			10,
			10
		};
		Avatar.FRAME[10] = new sbyte[]
		{
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11
		};
		Avatar.FRAME[11] = new sbyte[]
		{
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12
		};
		Avatar.FRAME[12] = new sbyte[]
		{
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13
		};
		Avatar.FRAME[13] = new sbyte[]
		{
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14
		};
		Avatar.FRAME[14] = new sbyte[]
		{
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1
		};
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000CFE6 File Offset: 0x0000B3E6
	public int getMoney()
	{
		return this.money[0];
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000CFF0 File Offset: 0x0000B3F0
	public int getMoneyNew()
	{
		if (MapScr.isNewVersion)
		{
			return this.money[3];
		}
		return this.money[0];
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000D00D File Offset: 0x0000B40D
	public void setMoneyNew(int mo)
	{
		if (MapScr.isNewVersion)
		{
			this.money[3] = mo;
		}
		else
		{
			this.money[0] = mo;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000D030 File Offset: 0x0000B430
	public void setMoney(int mo)
	{
		this.money[0] = mo;
		this.strMoney = Canvas.getMoneys(this.money[0]) + T.dola;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000D058 File Offset: 0x0000B458
	public void setGold(int gold)
	{
		this.money[2] = gold;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000D064 File Offset: 0x0000B464
	public override void paint(MyGraphics g)
	{
		if ((float)((this.x + 15) * MyObject.hd) < AvCamera.gI().xCam || (float)((this.x - 15) * MyObject.hd) > AvCamera.gI().xCam + (float)Canvas.w || this.ableShow || Canvas.currentMyScreen == MainMenu.gI() || Canvas.currentMyScreen == ParkListSrc.gI())
		{
			return;
		}
		if (Canvas.currentMyScreen == MessageScr.me || Canvas.currentMyScreen == ListScr.instance)
		{
			return;
		}
		if ((int)this.action != 14)
		{
			g.drawImage(LoadMap.imgShadow, (float)((this.x + (((int)this.direct != (int)Base.LEFT) ? -2 : 2)) * MyObject.hd), (float)((this.y - 1) * MyObject.hd), 3);
		}
		int num = this.seriPart.size();
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			Part part = AvatarData.getPart(((SeriPart)this.seriPart.elementAt(i)).idPart);
			if (part != null)
			{
				if ((int)this.action != 14 || (int)part.zOrder == 30 || (int)part.zOrder == 40 || (int)part.zOrder == 50)
				{
					if ((int)part.zOrder == 40)
					{
						if (this.feel != 4)
						{
							part = AvatarData.getPart(this.feel);
						}
						else if ((this.feel == 4 || this.feel == 6) && (int)this.nFrame < 1 + (int)this.gender)
						{
							flag = true;
						}
					}
					if ((LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53) || AvatarData.isZOrderMain((int)part.zOrder) || (int)part.zOrder == 52)
					{
						part.paintAvatar(g, this.frame, this.x * MyObject.hd, (this.y + (int)this.vh + (int)this.ySat) * MyObject.hd, (int)this.direct, 0);
						if (flag)
						{
							flag = false;
							Part part2 = AvatarData.getPart(606);
							if ((LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53) || AvatarData.isZOrderMain((int)part2.zOrder) || (int)part2.zOrder == 52)
							{
								part2.paintAvatar(g, this.frame, this.x * MyObject.hd, (this.y + (int)this.vh + (int)this.ySat) * MyObject.hd, (int)this.direct, 0);
							}
						}
					}
				}
			}
		}
		if ((OptionScr.gI().mapFocus[0] == 0 || this == LoadMap.focusObj) && LoadMap.TYPEMAP != 24)
		{
			this.paintName(g, this.x * MyObject.hd, this.y * MyObject.hd - (int)AvMain.hSmall);
		}
		if (this.kiss != null)
		{
			this.kiss.paint(g);
		}
		if ((int)this.timeHit > 0 && this.task == -2)
		{
			Avatar.imgHit.drawFrame((Canvas.gameTick % 6 < 3) ? 1 : 0, this.x * MyObject.hd, this.y * MyObject.hd - (int)(this.height / 2), 0, 3, g);
		}
		if (Canvas.currentMyScreen != MainMenu.gI())
		{
			base.paint(g);
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000D410 File Offset: 0x0000B810
	public override void paintIcon(MyGraphics g, int x, int y, bool isName)
	{
		g.drawImage(LoadMap.imgShadow, (float)(x + (((int)this.direct != (int)Base.LEFT) ? -2 : 2)), (float)(y - 1), 3);
		if (this.seriPart != null)
		{
			int num = this.seriPart.size();
			for (int i = 0; i < num; i++)
			{
				SeriPart seriPart = (SeriPart)this.seriPart.elementAt(i);
				Part part = AvatarData.getPart(seriPart.idPart);
				if (part != null)
				{
					if ((int)part.zOrder == 40 && this.feel != 4)
					{
						part = AvatarData.getPart(this.feel);
					}
					part.paintAvatar(g, this.frame, x, y, (int)this.direct, 0);
				}
			}
		}
		if (isName)
		{
			this.paintName(g, x, y - (int)AvMain.hSmall);
		}
		base.paint(g);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000D4F8 File Offset: 0x0000B8F8
	public void paintName(MyGraphics g, int x, int y)
	{
		int num = 0;
		int num2 = y - (int)this.height * MyObject.hd + ((int)this.vh + (int)this.ySat) * MyObject.hd;
		if (this.idImg != -1)
		{
			num = 7;
			AvatarData.paintImg(g, (int)this.idImg, x + (int)Avatar.duX[(int)this.direct] * MyObject.hd - (int)(this.wName / 2), num2 + Canvas.smallFontRed.getHeight() / 2, 3);
		}
		int num3 = x + ((int)Avatar.duX[(int)this.direct] + num) * MyObject.hd;
		if (this.idWedding != -1)
		{
			AvatarData.paintImg(g, (int)this.idWedding, num3 + (int)(this.wName / 2) + 7 * MyObject.hd, num2 + (int)AvMain.hSmall / 2, 3);
		}
		if ((int)this.blogNews != -1)
		{
			Avatar.imgBlog.drawFrame((int)this.blogNews, num3 + (int)(this.wName / 2) + 7 * MyObject.hd, num2 + 3, 0, 3, g);
		}
		if (this.IDDB == GameMidlet.avatar.IDDB)
		{
			Canvas.smallFontRed.drawString(g, this.name, num3, num2, 2);
		}
		else
		{
			Canvas.smallFontYellow.drawString(g, this.name, num3, num2, 2);
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000D63C File Offset: 0x0000BA3C
	public void setExp(int exp)
	{
		this.exp = exp;
		int num = 1;
		int num3;
		for (;;)
		{
			int num2 = num * 100;
			num3 = exp;
			int num4 = exp - num2;
			if (num4 < 0)
			{
				break;
			}
			num++;
			exp = num4;
		}
		this.lvMain = (short)((sbyte)num);
		this.perLvMain = (sbyte)(num3 * 100 / (num * 100));
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000D695 File Offset: 0x0000BA95
	public void addSeri(SeriPart seri)
	{
		if (this.seriPart == null)
		{
			this.seriPart = new MyVector();
		}
		this.seriPart.addElement(seri);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000D6BC File Offset: 0x0000BABC
	public void setName(string name)
	{
		this.name = name;
		if (name.Length > 7)
		{
			this.showName = name.Substring(0, 6) + "..";
		}
		else
		{
			this.showName = name;
		}
		this.wName = (short)Canvas.smallFontYellow.getWidth(name);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000D712 File Offset: 0x0000BB12
	public void setFeel(int f)
	{
		this.feel = (short)f;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000D71C File Offset: 0x0000BB1C
	public override void update()
	{
		if (this.kiss != null)
		{
			this.kiss.update();
		}
		if (this.isLoad && Canvas.gameTick % 20 == 10)
		{
			this.orderSeriesPath();
		}
		if (this.isLeave)
		{
			this.updateLeave();
		}
		this.updateAvatar();
		if (this.emotionList != null)
		{
			for (int i = 0; i < this.emotionList.size(); i++)
			{
				Emotion emotion = (Emotion)this.emotionList.elementAt(i);
				if (emotion.time == this.timeEmotion)
				{
					this.timeEmotion = 0;
					this.feel = emotion.id;
					this.emotionList.removeElement(emotion);
					break;
				}
			}
			this.timeEmotion += 1;
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000D7F4 File Offset: 0x0000BBF4
	private void updateLeave()
	{
		if (MapScr.isWedding && (this.IDDB == MapScr.idUserWedding_1 || this.IDDB == MapScr.idUserWedding_2))
		{
			return;
		}
		if (this.moveList.size() == 0 && this.x == this.xCur && this.y == this.yCur)
		{
			LoadMap.removePlayer(this);
			if (MapScr.focusP != null && MapScr.focusP.IDDB == this.IDDB)
			{
				MapScr.focusP = null;
				LoadMap.focusObj = null;
			}
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000D88F File Offset: 0x0000BC8F
	public void setFrame(int action)
	{
		if (action < 0)
		{
			this.frame = (int)Avatar.FRAME[0][(int)this.cFrame];
			return;
		}
		this.frame = (int)Avatar.FRAME[action][(int)this.cFrame];
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000D8C8 File Offset: 0x0000BCC8
	public void updateFrame()
	{
		if (this.nFrame < 1)
		{
			this.nFrame = (short)(10 + CRes.rnd(70) / ((int)this.gender + 1));
		}
		this.nFrame -= 1;
		this.cFrame = (sbyte)((int)this.cFrame + 1);
		if ((int)this.cFrame >= 10)
		{
			this.cFrame = 0;
		}
		if ((int)this.action < 0)
		{
			this.frame = (int)Avatar.FRAME[0][(int)this.cFrame];
		}
		else if ((int)this.action < Avatar.FRAME.Length)
		{
			this.frame = (int)Avatar.FRAME[(int)this.action][(int)this.cFrame];
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000D988 File Offset: 0x0000BD88
	public void updateAvatar()
	{
		this.updateFrame();
		if (this.numFeel != 0 || this.feel == 11 || this.feel == 7 || this.feel == 9)
		{
			if (this.numFeel == 0)
			{
				this.firFeel = this.feel;
			}
			this.numFeel += 1;
			if (this.numFeel % 10 > 5)
			{
				if (this.numFeel > 45)
				{
					this.numFeel = 0;
				}
				this.setFeel(4);
			}
			else
			{
				this.setFeel((int)this.firFeel);
			}
		}
		this.move();
		this.x += this.vx;
		this.y += this.vy;
		this.vh = (sbyte)((int)this.vh + (int)this.vhy);
		if ((int)this.action == 10)
		{
			this.vhy = (sbyte)((int)this.vhy + 1);
		}
		if (global::Math.abs((int)this.vhy) >= (int)this.g || global::Math.abs((int)this.vh) > 28)
		{
			Out.println("11111111111");
			this.action = 0;
			this.vhy = 0;
			this.vh = 0;
		}
		if (this.isJumps != -1 && (int)this.action == 0)
		{
			this.isJumps++;
			if (this.isJumps > (int)this.maxJump)
			{
				this.isJumps = -1;
			}
			else if (this.isJumps % 6 == 0)
			{
				this.doJumps();
			}
		}
		if ((int)this.action == 0)
		{
			this.ySat = 0;
		}
		if ((int)this.action == 1 && this.vx == 0 && this.vy == 0)
		{
			this.action = 0;
		}
		this.vx = 0;
		this.vy = 0;
		if ((int)this.timeHit > 0)
		{
			this.timeHit = (sbyte)((int)this.timeHit - 1);
			if ((int)this.timeHit == 0)
			{
				if (this.task == -2)
				{
					this.focus.action = 4;
					this.focus.feel = 20;
					this.action = 4;
					this.feel = 20;
				}
				else if (this.task == 11)
				{
					this.feel = 12;
					this.focus.feel = 12;
					this.kiss = null;
				}
				this.task = 0;
				this.focus.task = 0;
				this.focus = null;
			}
		}
		if (this.textChat != null)
		{
			if (this.chat == null)
			{
				if (this.countChat != -1 && Canvas.getTick() / 1000L - (long)this.countChat > 1L)
				{
					this.indexChat++;
					if (this.indexChat >= this.textChat.Length)
					{
						this.indexChat = 0;
					}
					this.countChat = -1;
					this.chat = new ChatPopup(100, this.textChat[this.indexChat], (this.idFrom < 2000000000) ? 0 : 1);
					this.chat.setPos(this.x, this.y - 45);
				}
			}
			else
			{
				this.countChat = (int)(Canvas.getTick() / 1000L);
			}
		}
		base.update();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000DCF8 File Offset: 0x0000C0F8
	public void updateKey()
	{
		if ((int)this.action == -1 || this.ableShow)
		{
			return;
		}
		if (this.task != 0)
		{
			return;
		}
		this.vx = 0;
		this.vy = 0;
		this.numSleep = 0;
		if (this.vx == 0 && this.vy == 0 && (int)this.action == 1)
		{
			this.action = 0;
			Out.println("33333333333333333333");
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000DD74 File Offset: 0x0000C174
	public void doAction(sbyte a)
	{
		if ((int)this.action == 10)
		{
			return;
		}
		if ((int)a == 2 || (int)a == 13 || (int)a == 4)
		{
			int num = (int)LoadMap.type[(this.y - 15) / LoadMap.w * (int)LoadMap.wMap + this.x / LoadMap.w];
			if (num == 79 || num == 81 || num == 54)
			{
				this.ySat = -6;
				if (num == 81)
				{
					if ((int)a != 4)
					{
						this.ySat = (sbyte)(-6 * MyObject.hd);
					}
					else
					{
						this.ySat = 0;
					}
				}
			}
			else if (num == 92 || num == 67)
			{
				this.ySat = -10;
			}
			this.action = a;
		}
		else if ((int)this.action != 14 && (int)a != 14)
		{
			this.action = 0;
		}
		if ((int)a == 10)
		{
			this.doJumps();
		}
		else if (this.IDDB != GameMidlet.avatar.IDDB)
		{
			this.moveList.addElement(new AvPosition(-1, -1, (int)a));
		}
		else
		{
			this.action = a;
		}
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000DEB0 File Offset: 0x0000C2B0
	private void updateTask()
	{
		if (this.task == 0 || this.task == -5)
		{
			return;
		}
		if (this.IDDB == this.idTo && LoadMap.getAvatar(this.idFrom) == null)
		{
			this.task = 0;
			this.idFrom = -1;
			return;
		}
		if (this.idFrom == -1 || this.idTo == -1)
		{
			return;
		}
		Avatar avatar = LoadMap.getAvatar(this.idFrom);
		Avatar avatar2 = LoadMap.getAvatar(this.idTo);
		if (avatar2 == null || avatar == null)
		{
			if (avatar != null)
			{
				avatar.task = 0;
				avatar.ableShow = false;
			}
			if (avatar2 != null)
			{
				avatar2.task = 0;
				avatar2.ableShow = false;
			}
			return;
		}
		if (avatar2.x > avatar.x)
		{
			avatar2.direct = (avatar2.dirFirst = Base.LEFT);
			avatar.direct = (avatar.dirFirst = Base.RIGHT);
		}
		else
		{
			avatar2.direct = (avatar2.dirFirst = Base.RIGHT);
			avatar.direct = (avatar.dirFirst = Base.LEFT);
		}
		if (this.IDDB != this.idFrom)
		{
			return;
		}
		if (this.timeTask > 0)
		{
			this.timeTask -= 1;
			return;
		}
		int num = this.task;
		switch (num)
		{
		case 9:
			if (this == GameMidlet.avatar)
			{
				MapScr.doAction(9);
			}
			else if (GameMidlet.avatar.task == 8 && this.IDDB == GameMidlet.avatar.idFrom)
			{
				MapScr.doAction(8);
				GameMidlet.avatar.task = 0;
			}
			MapScr.gI().setGifts(avatar2);
			this.task = 0;
			avatar2.task = 0;
			break;
		default:
			if (num == -3)
			{
				if ((int)LoadMap.weather == -1)
				{
					AnimateEffect animateEffect = new AnimateEffect(2, true, 0);
					animateEffect.show();
				}
				this.task = 0;
				avatar2.task = 0;
			}
			break;
		case 12:
			avatar2.setTask(0);
			this.setTask(0);
			break;
		}
		this.idGift = -1;
		this.idFrom = -1;
		this.idTo = -1;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000E0F5 File Offset: 0x0000C4F5
	public void setPosTo(int x0, int y0)
	{
		this.xCur = x0;
		this.yCur = y0;
		this.setDir(x0);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000E10C File Offset: 0x0000C50C
	public void setDir(int x0)
	{
		if (x0 > this.x)
		{
			this.direct = Base.RIGHT;
		}
		else if (x0 < this.x)
		{
			this.direct = Base.LEFT;
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000E144 File Offset: 0x0000C544
	public void move()
	{
		if (this == GameMidlet.avatar && this.task == 0 && Canvas.currentMyScreen != BoardScr.me)
		{
			return;
		}
		if ((int)this.action == 10)
		{
			return;
		}
		if (CRes.distance(this.x, this.y, this.xCur, this.yCur) <= this.v)
		{
			if (this.focus != null && (int)this.timeHit == 0)
			{
				if (this.task == -2)
				{
					this.timeHit = 20;
				}
				else if (this.task == 11)
				{
					this.timeHit = 30;
					this.feel = 107;
					this.focus.feel = 107;
					this.kiss = new Kiss(this.x, this.y);
				}
			}
			if (this.task == -5)
			{
				this.dirFirst = this.direct;
				this.x = this.xCur;
				this.y = this.yCur;
				if (LoadMap.nPath <= 0)
				{
					this.task = 0;
				}
				if (Canvas.currentDialog == null)
				{
					MapScr.gI().doMove(this.x, this.y, (int)this.direct);
				}
			}
			else
			{
				if (this.IDDB != GameMidlet.avatar.IDDB)
				{
					this.xCur = this.x;
					this.yCur = this.y;
				}
				if (this.moveList.size() == 0)
				{
					if ((int)this.action == 1)
					{
						this.action = 0;
					}
					this.direct = this.dirFirst;
				}
				else
				{
					AvPosition avPosition = (AvPosition)this.moveList.elementAt(0);
					this.setPosTo(avPosition.x, avPosition.y);
					if (this.xCur == -1 && this.yCur == -1)
					{
						this.xCur = this.x;
						this.yCur = this.y;
						if ((int)this.action == 14)
						{
							LoadMap.type[this.y / LoadMap.w * (int)LoadMap.wMap + this.x / LoadMap.w] = 112;
						}
						this.action = (sbyte)avPosition.anchor;
						if ((int)this.action == 14)
						{
							LoadMap.type[this.y / LoadMap.w * (int)LoadMap.wMap + this.x / LoadMap.w] = 90;
						}
						this.setLay();
					}
					else
					{
						this.dirFirst = (sbyte)avPosition.anchor;
						this.direct = this.dirFirst;
					}
					this.moveList.removeElementAt(0);
				}
			}
			this.updateTask();
			return;
		}
		this.angle = CRes.angle(this.xCur - this.x, -(this.yCur - this.y));
		int num = this.v * CRes.cos(this.angle) >> 10;
		int num2 = -(this.v * CRes.sin(this.angle)) >> 10;
		if (this.isSetAction && this.task == -5 && GameMidlet.avatar.setLayPLayer(this.x + num, this.y + num2))
		{
			LoadMap.resetPath();
			this.vx = (this.vy = 0);
			return;
		}
		this.vx = num;
		this.vy = num2;
		this.vhy = 0;
		this.vh = 0;
		this.ySat = 0;
		this.setDir(this.x + num);
		if (this.x != this.xCur)
		{
			this.resetTypeChair();
		}
		if (this.y != this.yCur)
		{
			this.resetTypeChair();
		}
		this.action = 1;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000E4FC File Offset: 0x0000C8FC
	private void setLay()
	{
		if ((int)this.action != 2 && (int)this.action != 13 && (int)this.action != 4)
		{
			return;
		}
		int num = (this.y - LoadMap.w) / LoadMap.w * (int)LoadMap.wMap + this.x / LoadMap.w;
		if (num < 0 || num >= LoadMap.type.Length)
		{
			return;
		}
		if ((int)this.action == 4)
		{
			int num2 = (int)LoadMap.type[num];
			if (num2 == 67)
			{
				this.ySat = -10;
			}
		}
		int num3 = LoadMap.getposMap(this.x, this.y - 10);
		this.yCur = this.y;
		if (num3 == -1)
		{
			return;
		}
		int num4 = (int)LoadMap.type[num3];
		if (num4 == 92)
		{
			this.ySat = -10;
		}
		if (num4 == 79 || num4 == 92 || num4 == 90 || num4 == 54)
		{
			LoadMap.type[num3] = 90;
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000E600 File Offset: 0x0000CA00
	public void resetTypeChair()
	{
		if ((int)this.action == 2 || (int)this.action == 13)
		{
			int num = LoadMap.getposMap(this.x, this.y - 18);
			if (num == -1)
			{
				return;
			}
			this.action = 0;
			this.ySat = 0;
			int num2 = (int)LoadMap.type[num];
			if (num2 == 90)
			{
				if (LoadMap.map[num] == 80)
				{
					LoadMap.type[num] = 92;
				}
				else if (LoadMap.map[num] == 97)
				{
					LoadMap.type[num] = 54;
				}
				else
				{
					LoadMap.type[num] = 79;
				}
			}
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000E6A8 File Offset: 0x0000CAA8
	public void resetNam_nghi(int vx, int vy)
	{
		if ((int)this.action == 4)
		{
			int num = LoadMap.getposMap(this.x, this.y - 18);
			if (num == -1)
			{
				return;
			}
			int typeMap = LoadMap.getTypeMap(this.x + vx * 12, this.y + vy * 12 - 10);
			if (typeMap == 80)
			{
				this.action = 0;
				this.ySat = 0;
			}
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000E714 File Offset: 0x0000CB14
	public bool doJoin(int vX, int vY)
	{
		if ((int)this.action == -1 || Canvas.currentDialog != null)
		{
			return false;
		}
		bool flag = Canvas.loadMap.doJoin(this.x + vX, this.y + vY);
		if (flag && (int)this.action == 1)
		{
			this.action = 0;
		}
		return flag;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000E770 File Offset: 0x0000CB70
	public bool detectCollisionMap(int vX, int vY)
	{
		bool flag = this.detectCollision(vX, vY);
		if (flag)
		{
			base.setWay(vX, vY);
		}
		return flag;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000E798 File Offset: 0x0000CB98
	public bool setLayPLayer(int vX, int vY)
	{
		if (((int)this.action != 0 && (int)this.action != 1) || (this.task != 0 && this.task != -5))
		{
			return false;
		}
		int num = (int)LoadMap.type[vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w];
		if (num == 79 || num == 81 || num == 54)
		{
			this.action = 2;
			this.ySat = -6;
			this.x = vX / LoadMap.w * LoadMap.w + LoadMap.w / 2;
			this.y = vY / LoadMap.w * LoadMap.w + LoadMap.w - 1;
			MapScr.gI().doMove(this.x, this.y, (int)this.direct);
			MapScr.doAction(this.action);
			return true;
		}
		if (num == 92)
		{
			this.action = 2;
			this.ySat = -10;
			this.x = vX / LoadMap.w * LoadMap.w + LoadMap.w / 2;
			this.y = vY / LoadMap.w * LoadMap.w + LoadMap.w - 1;
			MapScr.gI().doMove(this.x, vY, (int)this.direct);
			MapScr.doAction(2);
			return true;
		}
		if (num == 67)
		{
			this.action = 4;
			this.ySat = -10;
			this.x = vX / LoadMap.w * LoadMap.w + LoadMap.w / 2;
			MapScr.gI().doMove(this.x, vY, (int)this.direct);
			MapScr.doAction(4);
			return true;
		}
		return false;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000E93C File Offset: 0x0000CD3C
	public void doJumps()
	{
		if ((int)this.action == 0 || (int)this.action == 1)
		{
			this.action = 10;
			if (this.isJumps == -1)
			{
				this.isJumps = 0;
			}
			this.vhy = (sbyte)(-(sbyte)((int)this.g));
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000E98C File Offset: 0x0000CD8C
	public void orderSeriesPath()
	{
		this.isLoad = false;
		try
		{
			for (int i = 0; i < this.seriPart.size() - 1; i++)
			{
				SeriPart seriPart = (SeriPart)this.seriPart.elementAt(i);
				if (AvatarData.getPart(seriPart.idPart) != null)
				{
					for (int j = i + 1; j < this.seriPart.size(); j++)
					{
						SeriPart seriPart2 = (SeriPart)this.seriPart.elementAt(j);
						if (AvatarData.getPart(seriPart2.idPart).IDPart == -1)
						{
							this.isLoad = true;
						}
						if (AvatarData.getPart(seriPart2.idPart) != null && (int)AvatarData.getPart(seriPart.idPart).zOrder > (int)AvatarData.getPart(seriPart2.idPart).zOrder)
						{
							this.seriPart.setElementAt(seriPart, j);
							this.seriPart.setElementAt(seriPart2, i);
							seriPart = seriPart2;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			this.isLoad = true;
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000EAA4 File Offset: 0x0000CEA4
	public void setAction(sbyte ac)
	{
		this.action = ac;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000EAAD File Offset: 0x0000CEAD
	public void setTask(int task)
	{
		this.task = task;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000EAB8 File Offset: 0x0000CEB8
	public void addSeriPart(SeriPart seri)
	{
		Part part = AvatarData.getPart(seri.idPart);
		if (part == null || part.follow == -2)
		{
			return;
		}
		SeriPart seriByZ = AvatarData.getSeriByZ((int)part.zOrder, this.seriPart);
		if (seriByZ != null)
		{
			this.seriPart.removeElement(seriByZ);
		}
		this.seriPart.addElement(seri);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000EB18 File Offset: 0x0000CF18
	public void initPet()
	{
		try
		{
			for (int i = 0; i < this.seriPart.size(); i++)
			{
				SeriPart seriPart = (SeriPart)this.seriPart.elementAt(i);
				if ((int)AvatarData.getPart(seriPart.idPart).zOrder == -1)
				{
					this.seriPart.removeElement(seriPart);
					this.idPet = seriPart.idPart;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000EBA0 File Offset: 0x0000CFA0
	public void setPet()
	{
		this.initPet();
		LoadMap.setPet(this);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000EBB0 File Offset: 0x0000CFB0
	public void changePet(short idPart)
	{
		this.idPet = idPart;
		Pet pet = LoadMap.getPet(this.IDDB);
		if (pet != null)
		{
			LoadMap.removePlayer(pet);
			this.idPet = idPart;
		}
		this.setPet();
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000EBE9 File Offset: 0x0000CFE9
	public void resetAction()
	{
		this.vhy = 0;
		this.vh = 0;
		this.action = 0;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000EC00 File Offset: 0x0000D000
	public void addPart(int idPart, int zOrder)
	{
		for (int i = 0; i < this.seriPart.size() - 1; i++)
		{
			SeriPart seriPart = (SeriPart)this.seriPart.elementAt(i);
			Part part = AvatarData.getPart(seriPart.idPart);
			if (zOrder == (int)part.zOrder)
			{
				this.seriPart.removeElement(seriPart);
				break;
			}
		}
		this.addSeri(new SeriPart((short)idPart));
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000EC74 File Offset: 0x0000D074
	public void updateMoney(int xu, int luong, int luongK)
	{
		if (this.money[0] != xu)
		{
			Canvas.addFlyTextSmall(xu - this.money[0] + "xu", this.x, this.y, -1, 0, -1);
			this.money[0] = xu;
		}
		if (this.money[2] != luong)
		{
			Canvas.addFlyTextSmall(luong - this.money[2] + "luong", this.x, this.y, -1, 0, -1);
			this.money[2] = luong;
		}
		if (this.luongKhoa != luongK)
		{
			Canvas.addFlyTextSmall(luongK - this.luongKhoa + "luong", this.x, this.y, -1, 0, -1);
			this.luongKhoa = luongK;
		}
	}

	// Token: 0x040001B1 RID: 433
	public sbyte cFrame;

	// Token: 0x040001B2 RID: 434
	public int[] money;

	// Token: 0x040001B3 RID: 435
	public int luongKhoa;

	// Token: 0x040001B4 RID: 436
	public string strMoney;

	// Token: 0x040001B5 RID: 437
	public sbyte gender;

	// Token: 0x040001B6 RID: 438
	public sbyte maxJump;

	// Token: 0x040001B7 RID: 439
	public MyVector seriPart;

	// Token: 0x040001B8 RID: 440
	public MyVector emotionList;

	// Token: 0x040001B9 RID: 441
	public MyVector moveList;

	// Token: 0x040001BA RID: 442
	public sbyte ySat;

	// Token: 0x040001BB RID: 443
	public int friendShip;

	// Token: 0x040001BC RID: 444
	public int idFrom;

	// Token: 0x040001BD RID: 445
	public int idTo;

	// Token: 0x040001BE RID: 446
	public int idGift;

	// Token: 0x040001BF RID: 447
	public string showName;

	// Token: 0x040001C0 RID: 448
	public string text2;

	// Token: 0x040001C1 RID: 449
	public int exp;

	// Token: 0x040001C2 RID: 450
	public bool isReady;

	// Token: 0x040001C3 RID: 451
	public bool isLeave;

	// Token: 0x040001C4 RID: 452
	public sbyte typeHome;

	// Token: 0x040001C5 RID: 453
	public sbyte perLvMain;

	// Token: 0x040001C6 RID: 454
	public sbyte perLvFarm;

	// Token: 0x040001C7 RID: 455
	public sbyte dirFirst;

	// Token: 0x040001C8 RID: 456
	public short lvFarm;

	// Token: 0x040001C9 RID: 457
	public short lvMain;

	// Token: 0x040001CA RID: 458
	public bool isLoad;

	// Token: 0x040001CB RID: 459
	public bool isSetAction;

	// Token: 0x040001CC RID: 460
	public const sbyte HEART = -6;

	// Token: 0x040001CD RID: 461
	public const sbyte POINTER = -5;

	// Token: 0x040001CE RID: 462
	public const sbyte BUS_WAIT = -4;

	// Token: 0x040001CF RID: 463
	public const sbyte GIFT = -3;

	// Token: 0x040001D0 RID: 464
	public const sbyte HIT = -2;

	// Token: 0x040001D1 RID: 465
	public const sbyte SAT_DOWN_STAND = 5;

	// Token: 0x040001D2 RID: 466
	public const sbyte NAM_NGHI = 4;

	// Token: 0x040001D3 RID: 467
	public const sbyte ANEMONES = 6;

	// Token: 0x040001D4 RID: 468
	public const sbyte HANDSHAKE_LEFT = 10;

	// Token: 0x040001D5 RID: 469
	public const sbyte SURRENDER = 7;

	// Token: 0x040001D6 RID: 470
	public const sbyte GIFT_GIVING = 9;

	// Token: 0x040001D7 RID: 471
	public const sbyte RECEIVING_GIFT = 8;

	// Token: 0x040001D8 RID: 472
	public const sbyte QUY_CHO = 11;

	// Token: 0x040001D9 RID: 473
	public const sbyte QUY_NHAN = 12;

	// Token: 0x040001DA RID: 474
	public const sbyte NGOI_NHAN = 13;

	// Token: 0x040001DB RID: 475
	public int task;

	// Token: 0x040001DC RID: 476
	public int isJumps;

	// Token: 0x040001DD RID: 477
	private int numSleep;

	// Token: 0x040001DE RID: 478
	public const sbyte NOT_FEEL = 4;

	// Token: 0x040001DF RID: 479
	public const sbyte SAD = 5;

	// Token: 0x040001E0 RID: 480
	public const sbyte FUNNY = 6;

	// Token: 0x040001E1 RID: 481
	public const sbyte DA_LONG_NHEO = 7;

	// Token: 0x040001E2 RID: 482
	public const sbyte DAN = 8;

	// Token: 0x040001E3 RID: 483
	public const sbyte CRY = 9;

	// Token: 0x040001E4 RID: 484
	public const sbyte KISS = 11;

	// Token: 0x040001E5 RID: 485
	public const sbyte OTHER = 12;

	// Token: 0x040001E6 RID: 486
	public short feel;

	// Token: 0x040001E7 RID: 487
	public short numFeel;

	// Token: 0x040001E8 RID: 488
	public short firFeel;

	// Token: 0x040001E9 RID: 489
	public short wName;

	// Token: 0x040001EA RID: 490
	public short nFrame;

	// Token: 0x040001EB RID: 491
	public static int iHit = 0;

	// Token: 0x040001EC RID: 492
	public short idPet;

	// Token: 0x040001ED RID: 493
	public short hungerPet;

	// Token: 0x040001EE RID: 494
	public short idImg;

	// Token: 0x040001EF RID: 495
	public short timeTask;

	// Token: 0x040001F0 RID: 496
	public short idWedding;

	// Token: 0x040001F1 RID: 497
	public short idStatus;

	// Token: 0x040001F2 RID: 498
	private int angle;

	// Token: 0x040001F3 RID: 499
	public sbyte blogNews;

	// Token: 0x040001F4 RID: 500
	public bool isHit;

	// Token: 0x040001F5 RID: 501
	public bool isNo;

	// Token: 0x040001F6 RID: 502
	public static FrameImage imgBlog;

	// Token: 0x040001F7 RID: 503
	public sbyte fight;

	// Token: 0x040001F8 RID: 504
	public sbyte countDefent;

	// Token: 0x040001F9 RID: 505
	public short hp;

	// Token: 0x040001FA RID: 506
	public short mp;

	// Token: 0x040001FB RID: 507
	public short plusHP;

	// Token: 0x040001FC RID: 508
	public short plusMP;

	// Token: 0x040001FD RID: 509
	public short maxHP;

	// Token: 0x040001FE RID: 510
	public short maxMP;

	// Token: 0x040001FF RID: 511
	public short defence;

	// Token: 0x04000200 RID: 512
	public Avatar focus;

	// Token: 0x04000201 RID: 513
	public static FrameImage imgHit;

	// Token: 0x04000202 RID: 514
	public static FrameImage imgKiss;

	// Token: 0x04000203 RID: 515
	public sbyte timeHit;

	// Token: 0x04000204 RID: 516
	public Kiss kiss;

	// Token: 0x04000205 RID: 517
	public string[] textChat;

	// Token: 0x04000206 RID: 518
	public int countChat;

	// Token: 0x04000207 RID: 519
	public sbyte[] indexP;

	// Token: 0x04000208 RID: 520
	public static sbyte I_FRIENDLY = 0;

	// Token: 0x04000209 RID: 521
	public static sbyte I_CRAZY = 1;

	// Token: 0x0400020A RID: 522
	public static sbyte I_STYLISH = 2;

	// Token: 0x0400020B RID: 523
	public static sbyte I_HAPPY = 3;

	// Token: 0x0400020C RID: 524
	public static sbyte I_HUNGER = 4;

	// Token: 0x0400020D RID: 525
	public static sbyte[][] FRAME = new sbyte[15][];

	// Token: 0x0400020E RID: 526
	private static sbyte[] duX = new sbyte[]
	{
		-3,
		0,
		1
	};

	// Token: 0x0400020F RID: 527
	public short timeEmotion;

	// Token: 0x04000210 RID: 528
	private int indexChat;

	// Token: 0x04000211 RID: 529
	private int pa;

	// Token: 0x04000212 RID: 530
	private int pb;
}
