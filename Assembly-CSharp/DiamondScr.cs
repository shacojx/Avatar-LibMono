using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class DiamondScr : BoardScr
{
	// Token: 0x06000B12 RID: 2834 RVA: 0x0006CE04 File Offset: 0x0006B204
	public DiamondScr()
	{
		this.xSetSelected[0] = new int[]
		{
			-1,
			-2
		};
		this.xSetSelected[1] = new int[2];
		this.xSetSelected[2] = new int[]
		{
			1,
			2
		};
		this.xSetSelected[3] = new int[2];
		this.xSetSelected[4] = new int[]
		{
			-1,
			1
		};
		this.xSetSelected[5] = new int[2];
		this.ySetSelected[0] = new int[2];
		this.ySetSelected[1] = new int[]
		{
			-1,
			-2
		};
		this.ySetSelected[2] = new int[2];
		this.ySetSelected[3] = new int[]
		{
			1,
			2
		};
		this.ySetSelected[4] = new int[2];
		this.ySetSelected[5] = new int[]
		{
			-1,
			1
		};
		this.yCheck[0] = new int[2];
		this.yCheck[1] = new int[]
		{
			0,
			-1
		};
		this.yCheck[2] = new int[]
		{
			0,
			1
		};
		this.yCheck[3] = new int[2];
		this.yCheck[4] = new int[]
		{
			0,
			-1
		};
		this.yCheck[5] = new int[]
		{
			0,
			1
		};
		this.yCheck[6] = new int[]
		{
			1,
			3
		};
		this.yCheck[7] = new int[]
		{
			1,
			2
		};
		this.yCheck[8] = new int[]
		{
			1,
			2
		};
		this.yCheck[9] = new int[]
		{
			1,
			-2
		};
		this.yCheck[10] = new int[]
		{
			1,
			-1
		};
		this.yCheck[11] = new int[]
		{
			1,
			-1
		};
		this.yCheck[12] = new int[]
		{
			-1,
			-1
		};
		this.yCheck[13] = new int[]
		{
			1,
			1
		};
		this.yCheck[14] = new int[]
		{
			-1,
			1
		};
		this.yCheck[15] = new int[]
		{
			-1,
			1
		};
		this.xCheck[0] = new int[]
		{
			1,
			-2
		};
		this.xCheck[1] = new int[]
		{
			1,
			-1
		};
		this.xCheck[2] = new int[]
		{
			1,
			-1
		};
		this.xCheck[3] = new int[]
		{
			1,
			3
		};
		this.xCheck[4] = new int[]
		{
			1,
			2
		};
		this.xCheck[5] = new int[]
		{
			1,
			2
		};
		this.xCheck[6] = new int[2];
		this.xCheck[7] = new int[]
		{
			0,
			-1
		};
		this.xCheck[8] = new int[]
		{
			0,
			1
		};
		this.xCheck[9] = new int[2];
		this.xCheck[10] = new int[]
		{
			0,
			-1
		};
		this.xCheck[11] = new int[]
		{
			0,
			1
		};
		this.xCheck[12] = new int[]
		{
			-1,
			1
		};
		this.xCheck[13] = new int[]
		{
			-1,
			1
		};
		this.xCheck[14] = new int[]
		{
			-1,
			-1
		};
		this.xCheck[15] = new int[]
		{
			1,
			1
		};
		for (int i = 0; i < 8; i++)
		{
			this.array[i] = new Point[8];
		}
		this.cmdSelected = new Command(T.selectt, 20);
		this.cmdSkip = new Command(T.skip, 21);
		this.imgFireWork = new FrameImage(Image.createImage(T.getPath() + "/dialLucky/st"), 11 * AvMain.hd, 11 * AvMain.hd);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0006D25D File Offset: 0x0006B65D
	public static DiamondScr gI()
	{
		return (DiamondScr.me != null) ? DiamondScr.me : (DiamondScr.me = new DiamondScr());
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0006D27E File Offset: 0x0006B67E
	public override void commandTab(int index)
	{
		if (index != 20)
		{
			if (index != 21)
			{
				base.commandTab(index);
			}
			else
			{
				this.doSkip();
			}
		}
		else
		{
			this.doSelected();
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0006D2B7 File Offset: 0x0006B6B7
	public void doSkip()
	{
		CasinoService.gI().doSkipDaimond();
		this.turn = -1;
		this.cmdCenter = BoardScr.cmdWaiting;
		this.right = null;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0006D2DC File Offset: 0x0006B6DC
	protected void doSelected()
	{
		if (!this.isPath)
		{
			if (this.iSelected == -1 && this.cmdCenter == this.cmdSelected && this.turn == GameMidlet.avatar.IDDB && !this.isTrans)
			{
				this.iSelected = this.selected;
			}
			else
			{
				this.iSelected = -1;
			}
		}
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0006D34C File Offset: 0x0006B74C
	public override void init()
	{
		base.init();
		if (!this.isJoin)
		{
			if (Canvas.hCan > 250)
			{
				this.wCell = 24 * AvMain.hd;
				this.wImg = (sbyte)(24 * AvMain.hd);
			}
			else
			{
				this.wCell = 16;
				this.wImg = 16;
			}
			if (AvMain.hd == 2 && Screen.height > 480)
			{
				this.wCell = (int)(this.wImg = 72);
			}
		}
		this.hhFill = 40 * AvMain.hd;
		this.y = (Canvas.hCan - PaintPopup.hButtonSmall - this.wCell * 8) / 2;
		if ((int)this.countHit == -1 || !BoardScr.isStartGame)
		{
			if (this.y < 0)
			{
				this.x = Canvas.w - this.wCell * 8 - this.wCell / 2;
			}
			else
			{
				this.x = Canvas.w - this.wCell * 8 - this.y;
			}
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0006D464 File Offset: 0x0006B864
	public void start(int whoMoveFirst, int interval2, sbyte[][] arr)
	{
		base.repaint();
		base.start(whoMoveFirst, interval2);
		this.isEnd = false;
		this.arr = arr;
		this.turn = whoMoveFirst;
		BoardScr.interval = interval2;
		this.cmdCenter = (this.center = null);
		this.right = null;
		this.idWin = -1;
		BoardScr.dieTime = Canvas.getTick() + (long)(BoardScr.interval * 1000);
		if (GameMidlet.avatar.IDDB == this.turn)
		{
			this.isInit = true;
		}
		this.init();
		BoardScr.isStartGame = true;
		this.setPosPlaying();
		this.iSelected = -1;
		this.setArray(arr);
		Canvas.endDlg();
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x0006D510 File Offset: 0x0006B910
	public override void setPosPlaying()
	{
		AvCamera.gI().setPos(0, 0);
		if (BoardScr.isStartGame)
		{
			int num = this.wCell / 2;
			int num2 = 0;
			if (AvMain.hd == 1)
			{
				num2 = 30;
			}
			int num3 = Canvas.w - this.x - this.wCell - this.wCell;
			for (int i = 0; i < BoardScr.numPlayer; i++)
			{
				Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
				if (avatar.IDDB != -1)
				{
					if (avatar.IDDB != GameMidlet.avatar.IDDB)
					{
						LoadMap.addPlayer(avatar);
					}
					avatar.yCur = (avatar.y = Canvas.hCan - Canvas.hTab - AvMain.hFillTab - (int)AvMain.hSmall / 2 - 50);
					if (avatar.IDDB == GameMidlet.avatar.IDDB)
					{
						this.xPlayer1 = num + 16 * AvMain.hd + this.hhFill + num2;
						avatar.xCur = (avatar.x = this.xPlayer1 - this.hhFill / 2);
						avatar.direct = (avatar.dirFirst = Base.RIGHT);
					}
					else
					{
						this.xPlayer2 = this.x - this.wCell - 16 * AvMain.hd - this.hhFill - num2;
						avatar.xCur = (avatar.x = this.xPlayer2 + this.hhFill / 2);
						avatar.direct = (avatar.dirFirst = Base.LEFT);
					}
					avatar.ySat = 0;
					avatar.setAction(0);
					avatar.setFrame((int)avatar.action);
				}
			}
		}
		else
		{
			for (int j = 0; j < BoardScr.avatarInfos.size(); j++)
			{
				Avatar avatar2 = (Avatar)BoardScr.avatarInfos.elementAt(j);
				avatar2.x = (avatar2.xCur = Canvas.hw);
				if (avatar2.IDDB == GameMidlet.avatar.IDDB)
				{
					avatar2.y = (avatar2.yCur = Canvas.h - 5 * AvMain.hd);
				}
				else
				{
					avatar2.y = (avatar2.yCur = 55 * AvMain.hd);
				}
			}
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0006D780 File Offset: 0x0006BB80
	private void setArray(sbyte[][] arr)
	{
		this.isTrans = true;
		for (int i = 7; i >= 0; i--)
		{
			int num = 20;
			for (int j = 7; j >= 0; j--)
			{
				this.array[i][j] = new Point(j * this.wCell, i * this.wCell, (int)arr[i][j]);
				this.array[i][j].color = this.array[i][j].y;
				this.array[i][j].h = -num;
				num--;
				this.array[i][j].isFire = true;
				this.array[i][j].y = -(j * this.wCell + 24);
			}
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0006D840 File Offset: 0x0006BC40
	private void clear()
	{
		for (int i = 4 - this.clearCount / 10; i < 4 + this.clearCount / 10; i++)
		{
			for (int j = 4 - this.clearCount / 10; j < 4 + this.clearCount / 10; j++)
			{
				this.addFire(this.array[i][j].x + 12, this.array[i][j].y + 12, (int)this.array[i][j].itemID);
				this.array[i][j].itemID = -1;
			}
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0006D8E4 File Offset: 0x0006BCE4
	public override void update()
	{
		base.update();
		if (BoardScr.isStartGame || BoardScr.disableReady)
		{
			if (AvMain.hd == 2 && Screen.height > 480)
			{
				this.wCell = (int)((sbyte)((ScaleGUI.HEIGHT - (float)PaintPopup.hButtonSmall - 60f) / 8f));
				ImageIcon imgIcon = AvatarData.getImgIcon(1028);
				ImageIcon imgIcon2 = AvatarData.getImgIcon(1027);
				if (imgIcon.count != -1 && imgIcon2.count != -1 && imgIcon.img != null && imgIcon2.img != null && !this.isJoin)
				{
					this.isJoin = true;
					int num = (int)(imgIcon.h / imgIcon.w);
					int num2 = (int)(imgIcon2.h / imgIcon2.w);
					this.wImg = (sbyte)this.wCell;
					this.hhFill = 40 * AvMain.hd;
					imgIcon.img.texture = CRes.ScaleTexture(imgIcon.img.texture, this.wCell, num * this.wCell);
					imgIcon2.img.texture = CRes.ScaleTexture(imgIcon2.img.texture, this.wCell, num2 * this.wCell);
					imgIcon.img.w = this.wCell;
					imgIcon.img.h = this.wCell;
					imgIcon.w = (imgIcon.h = (short)this.wCell);
					imgIcon2.w = (imgIcon2.h = (short)this.wCell);
					this.y = ((int)ScaleGUI.HEIGHT - PaintPopup.hButtonSmall - this.wCell * 8) / 2;
					if ((int)this.countHit == -1 || !BoardScr.isStartGame)
					{
						if (this.y < 0)
						{
							this.x = Canvas.w - this.wCell * 8;
						}
						else
						{
							this.x = Canvas.w - this.wCell * 8 - this.y;
						}
					}
					this.imgSeleced = imgIcon;
					this.imgDiamond = imgIcon2;
					this.setArray(this.arr);
					this.setPosPlaying();
				}
			}
			if (BoardScr.dieTime != 0L)
			{
				BoardScr.currentTime = Canvas.getTick();
				if (BoardScr.currentTime > BoardScr.dieTime)
				{
					BoardScr.dieTime = 0L;
					if (this.turn == GameMidlet.avatar.IDDB && this.cmdCenter == this.cmdSelected)
					{
						this.doSkip();
					}
				}
			}
			if (this.turn == GameMidlet.avatar.IDDB)
			{
				this.countSelected++;
				if (this.countSelected >= 20)
				{
					this.countSelected = 0;
				}
			}
			else
			{
				this.countSelected = 0;
			}
			int num3 = 0;
			int num4 = 0;
			for (int i = 63; i >= 0; i--)
			{
				if (this.array[i / 8][i % 8] != null && (int)this.array[i / 8][i % 8].catagory == 1)
				{
					if (this.array[i / 8][i % 8].translate() == -1)
					{
						this.array[i / 8][i % 8].catagory = 0;
						num4 = 1;
					}
					else
					{
						num3 = 1;
					}
				}
			}
			if (num4 == 1 && this.isPath)
			{
				Out.println(string.Concat(new object[]
				{
					"selected: ",
					this.selected,
					"     ",
					this.iSelected
				}));
				if (!this.setSelected(this.selected) && !this.setSelected(this.iSelected))
				{
					int num5 = this.selected;
					this.selected = this.iSelected;
					this.iSelected = num5;
					this.change();
					this.cmdCenter = this.cmdSelected;
					this.right = this.cmdSkip;
				}
				else if (this.turn == GameMidlet.avatar.IDDB)
				{
					CasinoService.gI().doMoveDiamond(this.iSelected, this.selected);
				}
				this.isPath = false;
				this.iSelected = -1;
			}
			if (num3 == 0)
			{
				int num6 = 0;
				for (int j = 63; j >= 0; j--)
				{
					if (this.array[j / 8][j % 8] != null && this.array[j / 8][j % 8].isFire)
					{
						this.array[j / 8][j % 8].x += this.array[j / 8][j % 8].g;
						if (this.array[j / 8][j % 8].g > 1 || this.array[j / 8][j % 8].g < -1)
						{
							this.array[j / 8][j % 8].g -= this.array[j / 8][j % 8].g / CRes.abs(this.array[j / 8][j % 8].g);
						}
						this.array[j / 8][j % 8].y += this.array[j / 8][j % 8].h;
						this.array[j / 8][j % 8].h += 2;
						if (this.array[j / 8][j % 8].y >= this.array[j / 8][j % 8].color)
						{
							this.array[j / 8][j % 8].y = this.array[j / 8][j % 8].color;
							this.array[j / 8][j % 8].isFire = false;
						}
						else
						{
							num6 = 1;
						}
					}
				}
				if (num6 == 0 && this.isTrans)
				{
					if (this.turn == GameMidlet.avatar.IDDB)
					{
						if (!this.isInit)
						{
							if (this.ableMove)
							{
								this.setPath();
							}
						}
						else if (this.setOutPath())
						{
							this.cmdCenter = this.cmdSelected;
							this.right = this.cmdSkip;
						}
						else
						{
							CasinoService.gI().doOutPath();
						}
						this.isInit = false;
					}
					this.isTrans = false;
				}
			}
			if (this.clearCount != -1)
			{
				if (this.clearCount % 10 == 0)
				{
					this.clear();
				}
				this.clearCount += 2;
				if (this.clearCount >= 50)
				{
					this.createPoint();
					this.clearCount = -1;
				}
			}
			int k = 0;
			while (k < this.listFireWork.size())
			{
				Point point = (Point)this.listFireWork.elementAt(k);
				if (point.limitY < 1)
				{
					goto IL_716;
				}
				point.limitY++;
				if (point.limitY != 3)
				{
					goto IL_716;
				}
				this.listFireWork.removeElement(point);
				IL_931:
				k++;
				continue;
				IL_716:
				if (point.isFire)
				{
					point.x += point.g;
					if (point.g > 1 || point.g < -1)
					{
						point.g -= point.g / CRes.abs(point.g);
					}
					point.y += point.h;
					point.h++;
					if ((int)point.catagory == 1 && point.color < 19)
					{
						point.color++;
					}
					if (point.y + this.y > Canvas.h)
					{
						this.listFireWork.removeElement(point);
					}
					goto IL_931;
				}
				int num7 = CRes.angle((int)point.xTo - point.x, -((int)point.yTo - point.y));
				if (CRes.abs(num7 - point.h) > 10)
				{
					point.h -= (int)point.height * (int)point.catagory;
					point.h = CRes.fixangle(point.h);
				}
				else
				{
					point.h = num7;
					Point point2 = point;
					point2.dis = (sbyte)((int)point2.dis + 2);
				}
				if (point.color >= 4)
				{
					point.color = 0;
				}
				point.color++;
				int num8 = (int)point.dis * CRes.cos(point.h) >> 10;
				int num9 = -((int)point.dis * CRes.sin(point.h)) >> 10;
				if (CRes.distance(point.x, point.y, (int)point.xTo, (int)point.yTo) >= (int)point.dis)
				{
					point.x += num8;
					point.y += num9;
					goto IL_931;
				}
				this.listFireWork.removeElement(point);
				goto IL_931;
			}
			for (int l = 0; l < 2; l++)
			{
				Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(l);
				if (avatar.task == -1 && CRes.abs(avatar.xCur - avatar.x) < 10)
				{
					if ((int)this.countHit == -2)
					{
						this.countHit = -1;
						avatar.task = 0;
						if (avatar.IDDB == this.idWin)
						{
							avatar.doAction(10);
							avatar.setFeel(10);
						}
						else
						{
							avatar.action = 0;
							if (this.idWin != -1)
							{
								avatar.setFeel(9);
							}
						}
						this.isNo = false;
						if (avatar.IDDB == GameMidlet.avatar.IDDB)
						{
							avatar.direct = Base.RIGHT;
						}
					}
					else if (avatar.task == -1)
					{
						if (avatar.isNo && Canvas.gameTick % 6 == 3)
						{
							this.addFireNo(avatar.x, avatar.y - (int)avatar.height, 0);
						}
						if ((int)this.countHit == -1)
						{
							for (int m = 0; m < 2; m++)
							{
								Avatar avatar2 = (Avatar)BoardScr.avatarInfos.elementAt(m);
								if (avatar2.IDDB != avatar.IDDB)
								{
									avatar2.setFeel(20);
									avatar2.action = 4;
									avatar2.ableShow = true;
									avatar.ableShow = true;
								}
							}
							this.countHit = 20;
							if (this.isNo)
							{
								this.countHit = 30;
							}
						}
						else if ((int)this.countHit >= 0)
						{
							this.countHit = (sbyte)((int)this.countHit - 1);
							if ((int)this.countHit == -1)
							{
								this.countHit = -2;
								if (avatar.IDDB == GameMidlet.avatar.IDDB)
								{
									avatar.xCur = this.xPlayer1 - this.hhFill / 2;
								}
								else
								{
									avatar.xCur = this.xPlayer2 + this.hhFill / 2;
								}
							}
						}
					}
				}
				if (avatar.plusHP > 0)
				{
					int num10 = (int)(avatar.maxHP / 100 + 1);
					if ((int)avatar.plusHP - num10 < 0)
					{
						num10 = (int)avatar.plusHP;
					}
					avatar.plusHP = (short)((int)avatar.plusHP - num10);
					avatar.hp = (short)((int)avatar.hp + num10);
				}
				else if (avatar.plusHP < 0)
				{
					int num11 = (int)(avatar.maxHP / 100 + 1);
					if ((int)avatar.plusHP + num11 > 0)
					{
						num11 = (int)(-(int)avatar.plusHP);
					}
					avatar.hp = (short)((int)avatar.hp - num11);
					avatar.plusHP = (short)((int)avatar.plusHP + num11);
				}
				if (avatar.plusMP > 0)
				{
					int num12 = (int)(avatar.maxHP / 100 + 1);
					if ((int)avatar.plusMP - num12 < 0)
					{
						num12 = (int)avatar.plusMP;
					}
					avatar.plusMP = (short)((int)avatar.plusMP - num12);
					avatar.mp = (short)((int)avatar.mp + num12);
				}
				else if (avatar.plusMP < 0)
				{
					int num13 = (int)(avatar.maxHP / 100 + 1);
					if ((int)avatar.plusMP + num13 > 0)
					{
						num13 = (int)(-(int)avatar.plusMP);
					}
					avatar.mp = (short)((int)avatar.mp - num13);
					avatar.plusMP = (short)((int)avatar.plusMP + num13);
				}
			}
			for (int n = 0; n < this.listSmall.size(); n++)
			{
				Point point3 = (Point)this.listSmall.elementAt(n);
				point3.limitY--;
				if (point3.limitY <= 0)
				{
					this.listSmall.removeElement(point3);
				}
			}
		}
		else
		{
			this.right = null;
			this.updateReady();
		}
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0006E640 File Offset: 0x0006CA40
	private bool setOutPath()
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < this.xCheck.Length; k++)
				{
					if (i + this.yCheck[k][0] >= 0 && i + this.yCheck[k][0] < 8 && i + this.yCheck[k][1] >= 0 && i + this.yCheck[k][1] < 8 && j + this.xCheck[k][0] >= 0 && j + this.xCheck[k][0] < 8 && j + this.xCheck[k][1] >= 0 && j + this.xCheck[k][1] < 8 && this.array[i][j].itemID == this.array[i + this.yCheck[k][0]][j + this.xCheck[k][0]].itemID && this.array[i][j].itemID == this.array[i + this.yCheck[k][1]][j + this.xCheck[k][1]].itemID)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0006E790 File Offset: 0x0006CB90
	private void addFire(int x, int y, int index)
	{
		if (index == -1)
		{
			return;
		}
		Avatar avatarByID = BoardScr.getAvatarByID(this.turn);
		if (avatarByID == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		switch (index)
		{
		case 0:
			this.addFireNo(x + this.x, y + this.y, 0);
			return;
		case 1:
			num = avatarByID.x;
			num2 = avatarByID.y - (int)(avatarByID.height / 2);
			if (avatarByID.defence > 0)
			{
				if (avatarByID.IDDB == GameMidlet.avatar.IDDB)
				{
					num = this.xPlayer1 - 20 - 7;
				}
				else
				{
					num = this.xPlayer2 + 7 + 20;
				}
				num2 = avatarByID.y - 22;
			}
			break;
		case 2:
			if (avatarByID.IDDB == GameMidlet.avatar.IDDB)
			{
				num = this.xPlayer1 - 20 - this.hhFill + (int)avatarByID.hp * this.hhFill / (int)avatarByID.maxHP;
			}
			else
			{
				num = this.xPlayer2 + (this.hhFill - (int)avatarByID.hp * this.hhFill / (int)avatarByID.maxHP) + 20 - (int)avatarByID.hp * this.hhFill / (int)avatarByID.maxHP;
			}
			num2 = avatarByID.y - 2 - 10 * AvMain.hd;
			break;
		case 3:
			if (avatarByID.IDDB == GameMidlet.avatar.IDDB)
			{
				num = this.xPlayer1 - 20 - this.hhFill + (int)avatarByID.mp * this.hhFill / (int)avatarByID.maxMP;
			}
			else
			{
				num = this.xPlayer2 + (this.hhFill - (int)avatarByID.mp * this.hhFill / (int)avatarByID.maxMP) + 20 - (int)avatarByID.hp * this.hhFill / (int)avatarByID.maxHP;
			}
			num2 = avatarByID.y - 5 * AvMain.hd;
			break;
		case 4:
			this.addFireNo(x + this.x, y + this.y, 4);
			return;
		case 5:
			return;
		}
		Point point = new Point(x + this.x, y + this.y);
		point.limitY = 1;
		this.listFireWork.addElement(point);
		for (int i = 0; i < ((index == 1) ? 1 : 3); i++)
		{
			Point point2 = new Point(x + this.x, y + this.y);
			point2.distant = (short)index;
			point2.color = CRes.rnd(3);
			int g = CRes.angle(num - x, -(num2 - y));
			point2.g = g;
			point2.catagory = (sbyte)CRes.rnd(-1, 1);
			point2.h = CRes.fixangle(point2.g + (int)point2.catagory * 90);
			int num3 = 10 * CRes.cos(point2.h) >> 10;
			int num4 = -(10 * CRes.sin(point2.h)) >> 10;
			point2.xTo = (short)num;
			point2.yTo = (short)num2;
			point2.x += num3;
			point2.y += num4;
			point2.color = 0;
			point2.dis = (sbyte)(CRes.rnd(4) + 4);
			point2.height = (short)(10 + CRes.rnd(5));
			this.listFireWork.addElement(point2);
		}
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0006EAE4 File Offset: 0x0006CEE4
	private void addFireNo(int x, int y, int index)
	{
		if (index == -1)
		{
			return;
		}
		Point point = new Point(x, y);
		point.limitY = 1;
		this.listFireWork.addElement(point);
		for (int i = 0; i < 3; i++)
		{
			int num = CRes.rnd(-1, 1);
			Point point2 = new Point(x, y);
			point2.isFire = true;
			point2.color = CRes.rnd(3);
			point2.g = num * (CRes.rnd(100) / 10);
			point2.h = -CRes.rnd(100) / 10;
			point2.dis = (sbyte)index;
			point2.catagory = 1;
			point2.limitY = 0;
			this.listFireWork.addElement(point2);
		}
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0006EB90 File Offset: 0x0006CF90
	private bool setSelected(int index)
	{
		Out.println(string.Concat(new object[]
		{
			"setSelected: ",
			index,
			"    ",
			this.iSelected,
			"     ",
			this.isTrans
		}));
		if (this.iSelected == -1 || this.isTrans)
		{
			return false;
		}
		for (int i = 0; i < this.xSetSelected.Length; i++)
		{
			if (index / 8 + this.ySetSelected[i][0] >= 0 && index / 8 + this.ySetSelected[i][0] < 8 && index / 8 + this.ySetSelected[i][1] >= 0 && index / 8 + this.ySetSelected[i][1] < 8 && index % 8 + this.xSetSelected[i][0] >= 0 && index % 8 + this.xSetSelected[i][0] < 8 && index % 8 + this.xSetSelected[i][1] >= 0 && index % 8 + this.xSetSelected[i][1] < 8)
			{
				Out.println("-----------------------------");
				Out.println(string.Concat(new object[]
				{
					"ppp: ",
					index / 8,
					"    ",
					index % 8
				}));
				Out.println(string.Concat(new object[]
				{
					"aaa: ",
					i,
					"    ",
					this.array[index / 8][index % 8].itemID
				}));
				Out.println(string.Concat(new object[]
				{
					"bbb: ",
					index / 8 + this.ySetSelected[i][0],
					"     ",
					index % 8 + this.xSetSelected[i][0]
				}));
				Out.println(string.Concat(new object[]
				{
					"ccc: ",
					index / 8 + this.ySetSelected[i][1],
					"      ",
					index % 8 + this.xSetSelected[i][1]
				}));
				Out.println(string.Concat(new object[]
				{
					"ddd: ",
					this.array[index / 8 + this.ySetSelected[i][0]][index % 8 + this.xSetSelected[i][0]].itemID,
					"    ",
					this.array[index / 8 + this.ySetSelected[i][1]][index % 8 + this.xSetSelected[i][1]].itemID
				}));
				if (this.array[index / 8][index % 8].itemID == this.array[index / 8 + this.ySetSelected[i][0]][index % 8 + this.xSetSelected[i][0]].itemID && this.array[index / 8][index % 8].itemID == this.array[index / 8 + this.ySetSelected[i][1]][index % 8 + this.xSetSelected[i][1]].itemID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0006EEE0 File Offset: 0x0006D2E0
	public override void updateKey()
	{
		base.updateKey();
		if (Canvas.isPointerClick && Canvas.isPointer(this.x, this.y + Canvas.transTab, this.wCell * 8, this.wCell * 8) && this.iSelected == -1)
		{
			Canvas.isPointerClick = false;
			this.isTranCam = true;
			int num = (Canvas.px - this.x) / this.wCell;
			int num2 = (Canvas.py - this.y - Canvas.transTab) / this.wCell;
			this.selected = num2 * 8 + num;
		}
		if (Canvas.getTick() > BoardScr.dieTime - (long)((BoardScr.interval - 1) * 1000) && !this.isPath && !this.isTrans && this.cmdCenter == this.cmdSelected && this.cmdCenter != BoardScr.cmdWaiting && this.isTranCam)
		{
			if (Canvas.isPointerDown)
			{
				int num3 = Canvas.dx();
				int num4 = Canvas.dy();
				if (num3 < -this.wCell / 2)
				{
					if (this.selected % 8 < 7)
					{
						this.iSelected = this.selected;
						this.selected++;
						this.isTranCam = false;
						this.change();
					}
				}
				else if (num3 > this.wCell / 2)
				{
					if (this.selected % 8 > 0)
					{
						this.iSelected = this.selected;
						this.selected--;
						this.isTranCam = false;
						this.change();
					}
				}
				else if (num4 < -this.wCell / 2)
				{
					if (this.selected / 8 < 7)
					{
						this.iSelected = this.selected;
						this.selected += 8;
						this.isTranCam = false;
						this.change();
					}
				}
				else if (num4 > this.wCell / 2 && this.selected >= 8)
				{
					this.iSelected = this.selected;
					this.selected -= 8;
					this.isTranCam = false;
					this.change();
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranCam = false;
			}
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0006F124 File Offset: 0x0006D524
	private void change()
	{
		if (this.iSelected == -1 || this.isTrans)
		{
			return;
		}
		this.cmdCenter = BoardScr.cmdWaiting;
		this.right = null;
		this.isPath = true;
		this.isTranCam = false;
		Point point = this.array[this.selected / 8][this.selected % 8];
		Point point2 = this.array[this.iSelected / 8][this.iSelected % 8];
		int num = point.x;
		int num2 = point.y;
		short itemID = point.itemID;
		point.x = point2.x;
		point.y = point2.y;
		point.itemID = point2.itemID;
		point2.x = num;
		point2.y = num2;
		point2.itemID = itemID;
		point2.catagory = 1;
		point.catagory = 1;
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0006F1FC File Offset: 0x0006D5FC
	private void setPath()
	{
		bool flag = false;
		for (int i = 0; i < 64; i++)
		{
			if (this.array[i / 8][i % 8].itemID != -2)
			{
				int num = 0;
				int num2 = i + 1;
				while (num2 % 8 < 8 && num2 < 64 && num2 / 8 == i / 8)
				{
					if (this.array[i / 8][i % 8].itemID != this.array[num2 / 8][num2 % 8].itemID)
					{
						break;
					}
					num++;
					num2++;
				}
				if (num > 1)
				{
					for (int j = i; j < i + num + 1; j++)
					{
						this.array[j / 8][j % 8].isRemove = true;
						flag = true;
					}
				}
				num = 0;
				int num3 = i + 8;
				while (num3 < 64 && num3 % 8 == i % 8)
				{
					if (this.array[i / 8][i % 8].itemID != this.array[num3 / 8][num3 % 8].itemID)
					{
						break;
					}
					num++;
					num3 += 8;
				}
				if (num > 1)
				{
					for (int k = i; k < i + (num + 1) * 8; k += 8)
					{
						this.array[k / 8][k % 8].isRemove = true;
						flag = true;
					}
				}
			}
		}
		if (flag)
		{
			CasinoService.gI().createCell(this.array);
		}
		else if (this.isMove)
		{
			this.isMove = false;
			this.doSkip();
		}
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0006F3AC File Offset: 0x0006D7AC
	private void createPoint()
	{
		for (int i = 0; i < 8; i++)
		{
			int du = 4;
			for (int j = 7; j >= 0; j--)
			{
				if (this.array[(j * 8 + i) / 8][(j * 8 + i) % 8].itemID == -1)
				{
					this.setChange(j * 8 + i, du, -2);
					j++;
				}
			}
		}
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0006F415 File Offset: 0x0006D815
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		base.paint(g);
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0006F428 File Offset: 0x0006D828
	public override void paintMain(MyGraphics g)
	{
		base.paintMain(g);
		if (BoardScr.isStartGame)
		{
			Canvas.resetTransNotZoom(g);
			if ((AvMain.hd == 2 && Screen.height > 480 && !this.isJoin) || this.isJoin || AvMain.hd == 1 || Screen.height < 480)
			{
				this.paintGame(g);
			}
			Canvas.resetTransNotZoom(g);
			this.paintNamePlayers(g);
			this.paintPlayer(g);
			Canvas.resetTransNotZoom(g);
			string text = string.Empty;
			if (BoardScr.dieTime != 0L)
			{
				long num = (BoardScr.currentTime - BoardScr.dieTime) / 1000L;
				text += -num;
			}
			Canvas.numberFont.drawString(g, text + string.Empty, this.x - this.wCell / 2 - 5, this.y + this.wCell * 8 / 2 - Canvas.numberFont.getHeight() / 2, 1);
			this.paintFireWork(g);
		}
		else
		{
			this.paintPlayerCo(g);
		}
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0006F544 File Offset: 0x0006D944
	public void paintCaro(MyGraphics g)
	{
		if ((AvMain.hd == 2 && Screen.height > 480 && !this.isJoin) || this.isJoin || AvMain.hd == 1 || Screen.height < 480)
		{
			Canvas.resetTransNotZoom(g);
			g.setClip((float)(this.x - this.wCell / 2), (float)(this.y - this.wCell / 2), (float)(this.wCell * 8 + this.wCell + 1), (float)(this.wCell * 8 + this.wCell + 1));
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (j % 2 == i % 2)
					{
						g.setColor(5197647);
					}
					else
					{
						g.setColor(2697513);
					}
					g.fillRect((float)(this.x - this.wCell + i * this.wCell), (float)(this.y + j * this.wCell - this.wCell), (float)this.wCell, (float)this.wCell);
				}
			}
			g.setColor(0);
			g.drawRect((float)(this.x - this.wCell / 2), (float)(this.y - this.wCell / 2), (float)(this.wCell * 8 + this.wCell), (float)(this.wCell * 8 + this.wCell));
			g.drawRect((float)(this.x - this.wCell / 2 + 1), (float)(this.y - this.wCell / 2 + 1), (float)(this.wCell * 8 + this.wCell - 2), (float)(this.wCell * 8 + this.wCell - 2));
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x0006F714 File Offset: 0x0006DB14
	private void paintPlayer(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 2; i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			int num3 = avatar.y + 50;
			int num4 = avatar.x;
			if (avatar.IDDB == GameMidlet.avatar.IDDB)
			{
				num4 += this.hhFill / 2;
			}
			else
			{
				num4 -= this.hhFill / 2;
			}
			if ((int)this.countHit != -1 && avatar.task == -1 && (int)avatar.action == 0)
			{
				ImageIcon imgIcon = AvatarData.getImgIcon((!this.isNo) ? 881 : 882);
				if (imgIcon.count != -1)
				{
					g.drawRegion(imgIcon.img, 0f, (float)(48 * AvMain.hd * ((Canvas.gameTick % 6 >= 3) ? 1 : 0)), 48 * AvMain.hd, 48 * AvMain.hd, 0, (float)avatar.x, (float)(avatar.y - (int)(avatar.height / 2)), 3);
				}
			}
			int num5;
			int num7;
			int num6;
			int num8;
			int align;
			int num9;
			int num10;
			if (avatar.IDDB == GameMidlet.avatar.IDDB)
			{
				num5 = this.xPlayer1 + this.hhFill / 2;
				num5 -= 10 + 10 * AvMain.hd + this.hhFill;
				num = 0;
				num6 = (num2 = (num7 = 0));
				num8 = -2;
				align = 1;
				num9 = this.hhFill - 7;
				num10 = this.hhFill - 16 * AvMain.hd;
				Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), num5 + this.hhFill, num3, 1);
			}
			else
			{
				num5 = this.xPlayer2 - this.hhFill / 2;
				num5 += 10 + 10 * AvMain.hd;
				num += this.hhFill - (int)avatar.hp * this.hhFill / (int)avatar.maxHP;
				num2 += this.hhFill - (int)avatar.mp * this.hhFill / (int)avatar.maxMP;
				num6 = this.hhFill - (int)(avatar.hp + avatar.plusHP) * this.hhFill / (int)avatar.maxHP;
				num7 = this.hhFill - (int)(avatar.mp + avatar.plusMP) * this.hhFill / (int)avatar.maxMP;
				num8 = this.hhFill + 2;
				num9 = 8;
				align = 0;
				num10 = 16 * AvMain.hd;
				Canvas.smallFontYellow.drawString(g, avatar.getMoneyNew() + " " + T.getMoney(), num5, num3, 0);
			}
			Canvas.smallFontYellow.drawString(g, avatar.hp + string.Empty, num5 + num8, num3 - (int)AvMain.hSmall * 2 + 3 * AvMain.hd - (int)AvMain.hSmall / 2, align);
			Canvas.smallFontYellow.drawString(g, avatar.mp + string.Empty, num5 + num8, num3 - (int)AvMain.hSmall + 3 * AvMain.hd - (int)AvMain.hSmall / 2, align);
			if ((avatar.defence > 0 && (int)avatar.countDefent <= 0) || ((int)avatar.countDefent > 0 && Canvas.gameTick % 6 < 3))
			{
				AvatarData.paintImg(g, 880, num5 + num9, num3 - (int)AvMain.hSmall * 3, 3);
				Canvas.smallFontYellow.drawString(g, avatar.defence + string.Empty, num5 + num10, num3 - (int)AvMain.hSmall * 3 - (int)AvMain.hSmall / 2, align);
				if ((int)avatar.countDefent > 0)
				{
					Avatar avatar2 = avatar;
					avatar2.countDefent = (sbyte)((int)avatar2.countDefent - 1);
				}
			}
			if (avatar.plusHP != 0 && Canvas.gameTick % 6 >= 3)
			{
				g.setColor(1908254);
			}
			else
			{
				g.setColor(0);
			}
			g.fillRect((float)num5, (float)(num3 - (int)AvMain.hSmall * 2), (float)this.hhFill, (float)(6 * AvMain.hd));
			g.fillRect((float)num5, (float)(num3 - (int)AvMain.hSmall), (float)this.hhFill, (float)(6 * AvMain.hd));
			if (avatar.plusHP > 0)
			{
				g.setColor(16583178);
				g.fillRect((float)(num5 + num6), (float)(num3 - 4 - 10 * AvMain.hd), (float)((int)(avatar.hp + avatar.plusHP) * this.hhFill / (int)avatar.maxHP), (float)(6 * AvMain.hd));
			}
			if (avatar.plusHP != 0 && Canvas.gameTick % 6 >= 3)
			{
				g.setColor(16734553);
			}
			else
			{
				g.setColor(16711680);
			}
			g.fillRect((float)(num5 + num), (float)(num3 - (int)AvMain.hSmall * 2), (float)((int)avatar.hp * this.hhFill / (int)avatar.maxHP), (float)(6 * AvMain.hd));
			g.setColor(14137273);
			g.drawRect((float)num5, (float)(num3 - (int)AvMain.hSmall * 2), (float)this.hhFill, (float)(6 * AvMain.hd));
			g.drawRect((float)num5, (float)(num3 - (int)AvMain.hSmall), (float)this.hhFill, (float)(6 * AvMain.hd));
			if (avatar.plusMP > 0)
			{
				g.setColor(3771903);
				g.fillRect((float)(num5 + num7), (float)(num3 - (int)AvMain.hSmall + 1), (float)((int)(avatar.mp + avatar.plusMP) * this.hhFill / (int)avatar.maxMP), (float)(6 * AvMain.hd - 1));
			}
			if ((avatar.plusMP != 0 || avatar.isNo) && Canvas.gameTick % 6 >= 3)
			{
				g.setColor(6799871);
			}
			else
			{
				g.setColor(299247);
			}
			g.fillRect((float)(num5 + num2), (float)(num3 - (int)AvMain.hSmall + 1), (float)((int)avatar.mp * this.hhFill / (int)avatar.maxMP), (float)(6 * AvMain.hd - 1));
		}
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x0006FD70 File Offset: 0x0006E170
	private void paintGame(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		g.translate((float)this.x, (float)this.y);
		if (AvatarData.getImgIcon(876).count != -1)
		{
			for (int i = 0; i < this.listSmall.size(); i++)
			{
				Point point = (Point)this.listSmall.elementAt(i);
				int num = i * 17 - this.wCell / 2 + 8;
				if (point.color != GameMidlet.avatar.IDDB)
				{
					num = this.wCell * 8 - i * 17 + this.wCell / 2 - 8;
				}
				g.drawRegion(AvatarData.getImgIcon(876).img, 0f, (float)(point.itemID * 16), 16, 16, 0, (float)num, (float)(this.wCell * 8 + this.wCell), 3);
				Canvas.smallFontYellow.drawString(g, point.dis + string.Empty, num, this.wCell * 8 + this.wCell - (int)AvMain.hSmall / 2, 2);
			}
		}
		g.setClip((float)(-(float)this.wCell / 2), (float)(-(float)this.wCell / 2), (float)(this.wCell * 8 + this.wCell), (float)(this.wCell * 8 + this.wCell));
		if (this.selected >= 0 && this.array[this.selected / 8][this.selected % 8] != null)
		{
			ImageIcon imgIcon;
			if (AvMain.hd == 2 && Screen.height > 480)
			{
				imgIcon = this.imgSeleced;
			}
			else
			{
				imgIcon = AvatarData.getImgIcon((Canvas.hCan <= 250) ? 879 : 878);
			}
			if (imgIcon != null && imgIcon.count != -1)
			{
				g.drawRegion(imgIcon.img, 0f, (float)(this.countSelected / 10 * this.wCell), this.wCell, this.wCell, 0, (float)this.array[this.selected / 8][this.selected % 8].x, (float)this.array[this.selected / 8][this.selected % 8].y, 0);
			}
		}
		ImageIcon imgIcon2;
		if (AvMain.hd == 2 && Screen.height > 480)
		{
			imgIcon2 = this.imgDiamond;
		}
		else
		{
			imgIcon2 = AvatarData.getImgIcon((Canvas.hCan <= 250) ? 876 : 875);
		}
		if (imgIcon2 != null && imgIcon2.count != -1)
		{
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					if (this.array[j][k] != null && this.array[j][k].itemID >= 0)
					{
						g.drawRegion(imgIcon2.img, 0f, (float)((int)this.array[j][k].itemID * (int)this.wImg), (int)this.wImg, (int)this.wImg, 0, (float)this.array[j][k].x, (float)this.array[j][k].y, 0);
					}
				}
			}
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x000700D8 File Offset: 0x0006E4D8
	private void paintFireWork(MyGraphics g)
	{
		for (int i = 0; i < this.listFireWork.size(); i++)
		{
			Point point = (Point)this.listFireWork.elementAt(i);
			if (point.limitY > 0)
			{
				AvatarData.paintImg(g, 877, point.x, point.y, 3);
			}
			else if (point.isFire)
			{
				this.imgFireWork.drawFrame(point.color / 5, point.x, point.y, 0, 3, g);
			}
			else if ((int)point.dis >= 0)
			{
				this.imgFireWork.drawFrame(point.color / 2 + 1, point.x, point.y, 0, 3, g);
			}
		}
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x000701A0 File Offset: 0x0006E5A0
	public void onCreateCell(sbyte[] arrClear, AvPosition[] listCreate, sbyte countCombo, MyVector listStr)
	{
		for (int i = 0; i < arrClear.Length; i++)
		{
			this.array[(int)arrClear[i] / 8][(int)arrClear[i] % 8].isRemove = true;
			if (Canvas.h > 300)
			{
				int num = 0;
				for (int j = 0; j < this.listSmall.size(); j++)
				{
					Point point = (Point)this.listSmall.elementAt(j);
					if (point.itemID == this.array[(int)arrClear[i] / 8][(int)arrClear[i] % 8].itemID)
					{
						point.limitY += 20;
						num = 1;
						Point point2 = point;
						point2.dis = (sbyte)((int)point2.dis + 1);
						break;
					}
				}
				if (num == 0)
				{
					Point point3 = new Point();
					point3.itemID = this.array[(int)arrClear[i] / 8][(int)arrClear[i] % 8].itemID;
					point3.limitY = 40;
					point3.dis = 1;
					point3.color = this.turn;
					this.listSmall.addElement(point3);
				}
			}
		}
		this.removePoint();
		for (int k = 0; k < listCreate.Length; k++)
		{
			int anchor = listCreate[k].anchor;
			this.array[anchor / 8][anchor % 8].itemID = (short)listCreate[k].depth;
		}
		if ((int)countCombo > 1)
		{
			Canvas.addFlyTextSmall("Combo x" + countCombo, Canvas.hw, Canvas.hh, -1, 1, 20);
		}
		if (listStr.size() > 0)
		{
			for (int l = 0; l < listStr.size(); l++)
			{
				Canvas.addFlyTextSmall((string)listStr.elementAt(l), Canvas.hw, Canvas.hh + 40, -1, 1, l * 30);
			}
		}
		for (int m = 0; m < 2; m++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(m);
			avatar.setFeel(4);
			if (avatar.IDDB != this.turn && (int)avatar.fight > 0)
			{
				Avatar avatarByID = BoardScr.getAvatarByID(this.turn);
				if (avatarByID.task != -1)
				{
					avatarByID.setPosTo(avatar.x, avatar.y);
				}
				avatarByID.task = -1;
				if (avatar.defence > 0)
				{
					avatar.countDefent = 20;
				}
			}
		}
		Canvas.endDlg();
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00070424 File Offset: 0x0006E824
	private void removePoint()
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (this.array[i][j].isRemove)
				{
					this.array[i][j].isRemove = false;
					this.addFire(this.array[i][j].x + 12, this.array[i][j].y + 12, (int)this.array[i][j].itemID);
					this.array[i][j].itemID = -1;
				}
			}
		}
		this.createPoint();
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000704C8 File Offset: 0x0006E8C8
	private int setChange(int a, int du, short ItemID)
	{
		this.isTrans = true;
		int num = a;
		while (num / 8 > 0)
		{
			this.array[num / 8][num % 8].itemID = this.array[(num - 8) / 8][(num - 8) % 8].itemID;
			this.array[num / 8][num % 8].color = num / 8 * this.wCell;
			if (!this.array[num / 8][num % 8].isFire)
			{
				this.array[num / 8][num % 8].h = -du;
				du++;
				this.array[num / 8][num % 8].isFire = true;
			}
			this.array[num / 8][num % 8].y = this.array[(num - 8) / 8][(num - 8) % 8].y;
			num -= 8;
		}
		this.array[0][a % 8].itemID = ItemID;
		this.array[0][a % 8].color = 0;
		if (!this.array[0][a % 8].isFire)
		{
			this.array[0][a % 8].h = -du;
			du++;
			this.array[0][a % 8].isFire = true;
			this.array[0][a % 8].y = 0;
		}
		this.array[0][a % 8].y -= 24;
		return du;
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00070638 File Offset: 0x0006EA38
	public void move(int whoMove, int iSelected, int selected)
	{
		if (this.isEnd)
		{
			return;
		}
		Avatar avatarByID = BoardScr.getAvatarByID(whoMove);
		if (avatarByID != null && (int)avatarByID.action == 4)
		{
			avatarByID.action = 0;
		}
		if (whoMove == GameMidlet.avatar.IDDB)
		{
			this.isMove = true;
			this.setPath();
			this.ableMove = true;
		}
		else
		{
			this.cmdCenter = BoardScr.cmdWaiting;
			this.right = null;
			this.iSelected = iSelected;
			this.selected = selected;
			this.change();
			if (whoMove == -1)
			{
				this.isPath = false;
				this.iSelected = -1;
			}
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000706D8 File Offset: 0x0006EAD8
	public void onSkip(int whoMove1)
	{
		if (this.isEnd)
		{
			return;
		}
		this.iSelected = -1;
		BoardScr.dieTime = Canvas.getTick() + (long)(BoardScr.interval * 1000);
		this.turn = whoMove1;
		this.ableMove = false;
		if (whoMove1 == GameMidlet.avatar.IDDB)
		{
			if (this.setOutPath())
			{
				this.cmdCenter = this.cmdSelected;
				this.right = this.cmdSkip;
			}
			else
			{
				CasinoService.gI().doOutPath();
			}
		}
		else
		{
			this.isMove = false;
			this.cmdCenter = null;
			this.right = null;
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00070779 File Offset: 0x0006EB79
	public void onOutPath(int whoMove, sbyte[][] arr)
	{
		this.turn = whoMove;
		if (whoMove == GameMidlet.avatar.IDDB)
		{
			this.isInit = true;
		}
		this.setArray(arr);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x000707A0 File Offset: 0x0006EBA0
	public void setContinue()
	{
		this.right = BoardScr.cmdBack;
		this.turn = -1;
		base.resetReady();
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x000707BC File Offset: 0x0006EBBC
	public override void doContinue()
	{
		base.doContinue();
		BoardScr.isStartGame = false;
		this.setPosPlaying();
		this.isEnd = false;
		this.idWin = -1;
		for (int i = 0; i < BoardScr.avatarInfos.size(); i++)
		{
			Avatar avatar = (Avatar)BoardScr.avatarInfos.elementAt(i);
			avatar.resetAction();
			avatar.setFeel(4);
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00070822 File Offset: 0x0006EC22
	public void onFinish(MyVector list)
	{
		this.setContinue();
		this.left = null;
		this.isEnd = true;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00070838 File Offset: 0x0006EC38
	public void onData(sbyte[][] arr)
	{
		Out.println("on Data");
		for (int i = 7; i >= 0; i--)
		{
			for (int j = 7; j >= 0; j--)
			{
				this.array[i][j].itemID = (short)arr[i][j];
			}
		}
	}

	// Token: 0x04000E57 RID: 3671
	public new static DiamondScr me;

	// Token: 0x04000E58 RID: 3672
	private Point[][] array = new Point[8][];

	// Token: 0x04000E59 RID: 3673
	private int x;

	// Token: 0x04000E5A RID: 3674
	private int y;

	// Token: 0x04000E5B RID: 3675
	private int wCell;

	// Token: 0x04000E5C RID: 3676
	private int iSelected;

	// Token: 0x04000E5D RID: 3677
	private int clearCount = -1;

	// Token: 0x04000E5E RID: 3678
	private int xPlayer1;

	// Token: 0x04000E5F RID: 3679
	private int xPlayer2;

	// Token: 0x04000E60 RID: 3680
	private sbyte countHit = -1;

	// Token: 0x04000E61 RID: 3681
	private MyVector listFireWork = new MyVector();

	// Token: 0x04000E62 RID: 3682
	private bool isPath;

	// Token: 0x04000E63 RID: 3683
	private bool isTrans;

	// Token: 0x04000E64 RID: 3684
	private Command cmdSelected;

	// Token: 0x04000E65 RID: 3685
	private Command cmdSkip;

	// Token: 0x04000E66 RID: 3686
	private Command cmdCenter;

	// Token: 0x04000E67 RID: 3687
	private FrameImage imgFireWork;

	// Token: 0x04000E68 RID: 3688
	private sbyte wImg;

	// Token: 0x04000E69 RID: 3689
	public new int selected;

	// Token: 0x04000E6A RID: 3690
	public int idWin = -1;

	// Token: 0x04000E6B RID: 3691
	private bool isEnd;

	// Token: 0x04000E6C RID: 3692
	public bool isNo;

	// Token: 0x04000E6D RID: 3693
	private int yStart;

	// Token: 0x04000E6E RID: 3694
	private int[][] xCheck = new int[16][];

	// Token: 0x04000E6F RID: 3695
	private int[][] yCheck = new int[16][];

	// Token: 0x04000E70 RID: 3696
	private int[][] xSetSelected = new int[6][];

	// Token: 0x04000E71 RID: 3697
	private int[][] ySetSelected = new int[6][];

	// Token: 0x04000E72 RID: 3698
	public bool isInit;

	// Token: 0x04000E73 RID: 3699
	public bool isHd;

	// Token: 0x04000E74 RID: 3700
	private sbyte[][] arr;

	// Token: 0x04000E75 RID: 3701
	private bool isJoin;

	// Token: 0x04000E76 RID: 3702
	private ImageIcon imgSeleced;

	// Token: 0x04000E77 RID: 3703
	private ImageIcon imgDiamond;

	// Token: 0x04000E78 RID: 3704
	private bool isTranCam;

	// Token: 0x04000E79 RID: 3705
	private int hhFill;

	// Token: 0x04000E7A RID: 3706
	private int countSelected;

	// Token: 0x04000E7B RID: 3707
	private MyVector listSmall = new MyVector();

	// Token: 0x04000E7C RID: 3708
	private bool isMove;

	// Token: 0x04000E7D RID: 3709
	private bool ableMove;
}
