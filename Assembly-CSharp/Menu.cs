using System;

// Token: 0x02000163 RID: 355
public class Menu : MenuMain
{
	// Token: 0x0600093B RID: 2363 RVA: 0x000585F5 File Offset: 0x000569F5
	public Menu()
	{
		this.initCmd();
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00058617 File Offset: 0x00056A17
	public static Menu gI()
	{
		return (Menu.me != null) ? Menu.me : new Menu();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00058634 File Offset: 0x00056A34
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				this.showMenu = (this.showMenuFarm = false);
				Canvas.menuMain = null;
				if (this.iNo != null)
				{
					this.iNo.perform();
				}
			}
		}
		else
		{
			this.doFire();
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0005868F File Offset: 0x00056A8F
	public void initCmd()
	{
		if (Canvas.stypeInt == 0)
		{
			this.left = new Command(T.selectt, 0);
		}
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000586AC File Offset: 0x00056AAC
	public void startMenuFarm(MyVector menuItem, int x, int y, int w, int h)
	{
		if (menuItem.size() == 0)
		{
			return;
		}
		this.isHide = true;
		this.size = menuItem.size();
		this.xL = Canvas.h;
		this.showMenuFarm = true;
		this.showMenu = true;
		this.menuW = this.size * w + AvMain.hDuBox * 2 + 2;
		if (this.menuW > Canvas.w)
		{
			this.menuW = Canvas.w;
		}
		this.menuX = x - this.menuW / 2;
		this.menuH = h + AvMain.hDuBox / 2;
		if (this.menuX < 0)
		{
			this.menuX = 0;
		}
		if (Canvas.currentMyScreen != FarmScr.instance)
		{
			MainMenu.gI().avaPaint = null;
		}
		if (MainMenu.gI().avaPaint != null)
		{
			this.menuY = MainMenu.gI().avaPaint.y + (int)AvMain.hNormal + AvMain.hDuBox;
			if (this.menuY + this.menuH > Canvas.h)
			{
				this.menuY = MainMenu.gI().avaPaint.y - Canvas.hTab - this.menuH - AvMain.hDuBox * 2;
			}
		}
		else
		{
			this.menuY = (int)((float)(GameMidlet.avatar.y * AvMain.hd) * ((Canvas.currentMyScreen != FarmScr.instance) ? 1f : AvMain.zoom) - AvCamera.gI().yCam - (float)this.menuH - (float)(AvMain.hDuBox * 2));
			if (AvMain.zoom == 2f)
			{
				this.menuY -= 20;
			}
			if (this.menuY < 10 + AvMain.hDuBox + (int)AvMain.hBlack)
			{
				this.menuY = 10 + AvMain.hDuBox + (int)AvMain.hBlack;
			}
		}
		if (Canvas.currentMyScreen == HouseScr.me)
		{
			this.menuY = Canvas.hCan - this.menuH - 30 * AvMain.hd;
		}
		this.menuTemY = this.menuY;
		this.hItem = h;
		this.wItem = w;
		this.list = menuItem;
		this.setSelected();
		this.cmyLim = this.size * this.wItem - (this.menuW - AvMain.hDuBox * 2 - 4);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.disY = this.menuW;
		Menu.cmdClose = null;
		this.pos = 0;
		this.iNo = null;
		this.xTranTo = 0;
		Canvas.xTran = 0;
		this.isClose = false;
		base.show();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0005894D File Offset: 0x00056D4D
	private void setSelected()
	{
		if (this.selected < 0)
		{
			this.selected = 0;
		}
		if (this.selected >= this.size)
		{
			this.selected = 0;
		}
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0005897A File Offset: 0x00056D7A
	public void setPos(int x, int y)
	{
		this.menuX = x;
		this.menuY = y;
		if (this.menuX < 0)
		{
			this.menuX = 0;
		}
		if (this.menuY < 0)
		{
			this.menuY = 0;
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x000589B0 File Offset: 0x00056DB0
	private void doFire()
	{
		Canvas.xTran = 0;
		this.xTranMenu = (this.xTranTo = 0);
		if (Menu.cmdClose != null)
		{
			if (Menu.cmdClose.action != null)
			{
				Menu.cmdClose.action.perform();
			}
			else if (Menu.cmdClose.pointer != null)
			{
				Menu.cmdClose.pointer.commandActionPointer((int)Menu.cmdClose.indexMenu, (int)Menu.cmdClose.subIndex);
			}
			else
			{
				this.commandTab((int)Menu.cmdClose.indexMenu);
			}
		}
		Command command = (Command)this.list.elementAt(this.selected);
		if (command.action != null)
		{
			command.action.perform();
		}
		else if (command.pointer != null)
		{
			command.pointer.commandActionPointer((int)command.indexMenu, (int)command.subIndex);
		}
		else
		{
			Canvas.currentMyScreen.commandAction((int)command.indexMenu);
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00058AB6 File Offset: 0x00056EB6
	public override void updateKey()
	{
		this.updateMenuKeyMain();
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00058AC0 File Offset: 0x00056EC0
	private void click()
	{
		int num = this.hItem;
		if (this.showMenuFarm)
		{
			num = this.wItem;
		}
		int num2 = this.menuTemY + Canvas.transTab;
		int num3 = (this.cmtoY + Canvas.py - num2) / num;
		if (this.showMenuFarm)
		{
			num2 = this.menuX;
			num3 = (this.cmtoY + Canvas.px - num2) / num;
		}
		this.isHide = true;
		if (num3 >= 0 && num3 < this.size)
		{
			this.selected = num3;
			if (!this.showMenuFarm)
			{
				this.xTranTo = -this.menuW;
				this.isClose = true;
				this.isFire = true;
			}
			else
			{
				this.isFire = false;
				this.showMenu = (this.showMenuFarm = false);
				Canvas.menuMain = null;
				this.doFire();
			}
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00058B98 File Offset: 0x00056F98
	private void updateMenuKeyMain()
	{
		this.count += 1L;
		if (this.chan == 0)
		{
			if (!this.showMenuFarm && CRes.abs(CRes.abs(this.xTranMenu) - CRes.abs(this.xTranTo)) < 5)
			{
				if (Canvas.isPointerClick)
				{
					this.pyLast = Canvas.pyLast;
					this.dir = 1;
					this.xText = 0;
					this.isClick = true;
					this.isG = false;
					if (Canvas.isPoint(this.menuX + 4 * AvMain.hd, this.menuY + 4 * AvMain.hd, this.menuW - 8 * AvMain.hd, this.menuH - 8 * AvMain.hd))
					{
						if (this.vY != 0)
						{
							this.isG = true;
						}
						Canvas.isPointerClick = false;
						this.pa = this.cmtoY;
						this.timeDelay = this.count;
						this.trans = true;
					}
				}
				if (this.trans)
				{
					int num = this.pyLast - Canvas.py;
					this.pyLast = Canvas.py;
					long num2 = this.count - this.timeDelay;
					if (Canvas.isPointerDown)
					{
						if (this.count % 2L == 0L)
						{
							this.dyTran = Canvas.py;
							this.timePoint = this.count;
						}
						this.vY = 0;
						if (global::Math.abs(num) < 10 * AvMain.hd)
						{
							int num3 = this.menuY + 8 * AvMain.hd;
							int num4 = this.hItem;
							int num5 = (this.cmtoY + Canvas.py - num3) / num4;
							if (num5 >= 0 && num5 < this.size)
							{
								this.selected = num5;
							}
						}
						if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
						{
							this.isHide = true;
						}
						else if (num2 > 3L && num2 < 8L)
						{
							int num6 = this.menuY + 8 * AvMain.hd;
							int num7 = this.hItem;
							int num8 = (this.cmtoY + Canvas.py - num6) / num7;
							if (num8 >= 0 && num8 < this.size && !this.isG)
							{
								this.isHide = false;
							}
						}
						if (this.cmtoY < 0 || this.cmtoY > this.cmyLim)
						{
							this.cmtoY = this.pa + num / 2;
							this.pa = this.cmtoY;
						}
						else
						{
							this.cmtoY = this.pa + num / 2;
							this.pa = this.cmtoY;
						}
						this.cmy = this.cmtoY;
					}
					if (Canvas.isPointerRelease && Canvas.isPoint(this.menuX + 4 * AvMain.hd, this.menuY + 4 * AvMain.hd, this.menuW - 8 * AvMain.hd + 12 * AvMain.hd, this.menuH - 8 * AvMain.hd))
					{
						this.isG = false;
						int num9 = (int)(this.count - this.timePoint);
						int num10 = this.dyTran - Canvas.py;
						if (this.showMenuFarm)
						{
							num10 = this.dyTran - Canvas.px;
						}
						if (CRes.abs(num10) > 40 && num9 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
						{
							this.vY = num10 / num9 * 10;
						}
						this.timePoint = -1L;
						if (global::Math.abs(num) < 10 * AvMain.hd)
						{
							if (num2 <= 4L)
							{
								this.isHide = false;
								this.timeOpen = 5;
							}
							else if (!this.isHide)
							{
								this.click();
							}
						}
						this.trans = false;
						Canvas.isPointerRelease = false;
					}
				}
				if (!this.trans && !Canvas.isPoint(this.menuX + 4 * AvMain.hd, this.menuY + 4 * AvMain.hd, this.menuW - 8 * AvMain.hd, this.menuH - 8 * AvMain.hd) && Canvas.isPointerRelease && this.isClick)
				{
					this.isG = false;
					this.isClick = false;
					int num11 = this.dyTran - Canvas.py;
					if (!this.trans)
					{
						if (!this.isClose)
						{
							this.xTranTo = -this.menuW;
						}
						this.isClose = true;
					}
					this.trans = false;
					Canvas.isPointerRelease = false;
				}
			}
			else
			{
				if (Canvas.isPointerClick)
				{
					this.pyLast = Canvas.pyLast;
					if (this.showMenuFarm)
					{
						this.pyLast = Canvas.pxLast;
					}
					this.dir = 1;
					this.xText = 0;
					this.isClick = true;
					if (Canvas.isPoint(this.menuX - 2, this.menuTemY - 7 + Canvas.transTab, this.menuW + 4, this.menuH + 15))
					{
						Canvas.isPointerClick = false;
						this.pa = this.cmy;
						this.timeDelay = this.count;
						this.trans = true;
					}
				}
				if (this.trans)
				{
					int num12 = this.pyLast - Canvas.py;
					if (this.showMenuFarm)
					{
						num12 = this.pyLast - Canvas.px;
						this.pyLast = Canvas.px;
					}
					else
					{
						this.pyLast = Canvas.py;
					}
					long num13 = this.count - this.timeDelay;
					if (Canvas.isPointerDown)
					{
						this.dyTran = Canvas.py;
						if (this.showMenuFarm)
						{
							this.dyTran = Canvas.px;
						}
						this.timePoint = this.count;
						this.vY = 0;
						if (global::Math.abs(num12) < 10 * AvMain.hd)
						{
							int num14 = this.menuTemY;
							int num15 = this.hItem;
							if (this.showMenuFarm)
							{
								num15 = this.wItem;
							}
							int num16 = (this.cmtoY + Canvas.py - num14) / num15;
							if (this.showMenuFarm)
							{
								num14 = this.menuX + AvMain.hDuBox / 4;
								num16 = (this.cmtoY + Canvas.px - num14) / num15;
							}
							if (num16 >= 0 && num16 < this.size)
							{
								this.selected = num16;
							}
						}
						if (CRes.abs(Canvas.dx()) >= 10 * AvMain.hd || CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
						{
							this.isHide = true;
						}
						else if (num13 > 3L && num13 < 8L)
						{
							this.isHide = false;
						}
						if (this.cmy < 0 || this.cmy >= this.cmyLim)
						{
							this.cmtoY = this.pa + num12 / 2;
							this.pa = this.cmtoY;
						}
						else
						{
							this.cmtoY = this.pa + num12;
							this.pa = this.cmtoY;
						}
						this.cmy = this.cmtoY;
					}
					if (Canvas.isPointerRelease && Canvas.isPoint(this.menuX - 2, this.menuTemY - 7 + Canvas.transTab, this.menuW + 4, this.menuH + 15))
					{
						int num17 = (int)(this.count - this.timePoint);
						int num18 = this.dyTran - Canvas.py;
						if (this.showMenuFarm)
						{
							num18 = this.dyTran - Canvas.px;
						}
						if (CRes.abs(num18) > 40 && num17 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
						{
							this.vY = num18 / num17 * 10;
						}
						this.timePoint = -1L;
						if (global::Math.abs(num12) < 10 * AvMain.hd)
						{
							if (num13 <= 4L)
							{
								this.isHide = false;
								this.timeOpen = 5;
							}
							else if (!this.isHide)
							{
								this.click();
							}
						}
						this.trans = false;
						Canvas.isPointerRelease = false;
					}
				}
				if (!Canvas.isPoint(this.menuX - 2, this.menuTemY - 7 + Canvas.transTab, this.menuW + 4, this.menuH + 15) && this.isClick && Canvas.isPointerRelease)
				{
					this.close();
					this.trans = false;
					Canvas.isPointerRelease = false;
				}
			}
		}
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0005940C File Offset: 0x0005780C
	public void close()
	{
		this.isClick = false;
		if (!this.trans)
		{
			this.showMenu = (this.showMenuFarm = false);
			Canvas.menuMain = null;
			if (this.iNo != null)
			{
				this.iNo.perform();
			}
			this.xTranTo = (this.xTranMenu = (Canvas.xTran = 0));
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0005946D File Offset: 0x0005786D
	public override void paint(MyGraphics g)
	{
		g.translate(0f, (float)this.xL);
		this.paintMenuFarm(g);
		base.paint(g);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00059490 File Offset: 0x00057890
	private void paintMenuFarm(MyGraphics g)
	{
		Canvas.xTran = 0;
		Canvas.resetTrans(g);
		if (Canvas.currentMyScreen != MainMenu.me)
		{
			Canvas.paint.paintTransBack(g);
		}
		if (LoadMap.focusObj != null && MainMenu.gI().avaPaint != null)
		{
			((Animal)LoadMap.focusObj).paintIcon(g, MainMenu.gI().avaPaint.x, MainMenu.gI().avaPaint.y, false);
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0f, 0f, (float)Canvas.w, (float)Canvas.hCan);
		Canvas.paint.paintPopupBack(g, this.menuX, this.menuY, this.menuW, this.menuH + 3 * AvMain.hd, -1, false);
		g.translate((float)(this.menuX + AvMain.hDuBox + 1), (float)this.menuY);
		g.setClip(0f, 0f, (float)(this.menuW - AvMain.hDuBox * 2 - 2), (float)(this.menuH + 10));
		g.translate((float)(-(float)this.cmy), 0f);
		int num = this.cmy / this.wItem;
		if (num < 0)
		{
			num = 0;
		}
		int num2 = num + this.menuW / this.wItem + 2;
		if (num2 > this.size)
		{
			num2 = this.size;
		}
		if (!this.isHide)
		{
			g.setColor(16777215);
			g.fillRect((float)(this.selected * this.wItem + 4 * AvMain.hd), (float)(this.menuH / 2 - this.hItem / 2 + 4 * AvMain.hd), (float)(this.wItem - 8 * AvMain.hd), (float)(this.hItem + 4 * AvMain.hd - 8 * AvMain.hd));
		}
		for (int i = num; i < num2; i++)
		{
			Command command = (Command)this.list.elementAt(i);
			command.paint(g, i * this.wItem + this.wItem / 2, this.hItem / 2 + 4 * AvMain.hd);
		}
		if (this.selected >= 0 && this.selected < this.list.size())
		{
			Command command2 = (Command)this.list.elementAt(this.selected);
			g.setClip((float)(this.cmy - 50 * AvMain.hd), -100f, (float)(this.cmy + Canvas.w + 100 * AvMain.hd), (float)(this.menuH + 200));
			int num3 = this.selected * this.wItem + this.wItem / 2;
			if (this.size * this.wItem + AvMain.hDuBox * 2 + 10 > Canvas.w)
			{
				int num4 = Canvas.borderFont.getWidth(command2.caption) / 2;
				if (num3 - num4 < this.cmy)
				{
					num3 = this.cmy + num4;
				}
				else if (num3 + num4 > Canvas.w + this.cmy - 15)
				{
					num3 = Canvas.w + this.cmy - num4 - 15;
				}
			}
			Canvas.smallFontYellow.drawString(g, command2.caption, num3, -(int)AvMain.hSmall - 3 * AvMain.hd, 2);
		}
		Canvas.resetTrans(g);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000597FC File Offset: 0x00057BFC
	public override void update()
	{
		if (this.timeOpen > 0)
		{
			this.timeOpen--;
			if (this.timeOpen == 0)
			{
				this.click();
			}
		}
		if (this.xL != 0)
		{
			this.xL += -this.xL >> 1;
		}
		if (this.xL == -1)
		{
			this.xL = 0;
		}
		if (CRes.abs(CRes.abs(this.xTranMenu) - CRes.abs(this.xTranTo)) > 0 && !this.showMenuFarm)
		{
			this.xTranMenu += (this.xTranTo - this.xTranMenu) / 3;
			Canvas.xTran = this.menuW + this.xTranMenu;
			if (this.isClose && (this.xTranTo - this.xTranMenu) / 3 == 0)
			{
				this.trans = false;
				this.isClose = false;
				this.isClick = false;
				this.showMenu = false;
				if (this.isFire)
				{
					this.isFire = false;
					this.doFire();
				}
				Canvas.xTran = 0;
			}
		}
		else
		{
			this.xTranMenu = this.xTranTo;
		}
		if (this.xTranMenu == -1)
		{
			this.xTranMenu = 0;
		}
		this.moveCamera();
		this.updateMain();
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0005994C File Offset: 0x00057D4C
	public void moveCamera()
	{
		if (this.vY != 0)
		{
			if (this.cmy < 0 || this.cmy > this.cmyLim)
			{
				this.vY -= this.vY / 4;
				this.cmy += this.vY / 20;
				if (this.vY / 10 <= 1)
				{
					this.vY = 0;
				}
			}
			if (this.cmy < 0)
			{
				if (this.cmy < -this.disY / 2)
				{
					this.cmy = -this.disY / 2;
					this.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (this.cmy > this.cmyLim)
			{
				if (this.cmy < this.cmyLim + this.disY / 2)
				{
					this.cmy = this.cmyLim + this.disY / 2;
					this.cmtoY = this.cmyLim;
					this.vY = 0;
				}
			}
			else
			{
				this.cmy += this.vY / 10;
			}
			this.cmtoY = this.cmy;
			this.vY -= this.vY / 10;
			if (this.vY / 10 == 0)
			{
				this.vY = 0;
			}
		}
		else if (this.cmy < 0)
		{
			this.cmtoY = 0;
		}
		else if (this.cmy > this.cmyLim)
		{
			this.cmtoY = this.cmyLim;
		}
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00059B40 File Offset: 0x00057F40
	private void updateMain()
	{
		if (this.menuTemY > this.menuY)
		{
			int num = this.menuTemY - this.menuY >> 2;
			if (num < 1)
			{
				num = 1;
			}
			this.menuTemY -= num;
		}
		this.menuTemY = this.menuY;
	}

	// Token: 0x04000B74 RID: 2932
	public static Menu me;

	// Token: 0x04000B75 RID: 2933
	public bool showMenu;

	// Token: 0x04000B76 RID: 2934
	public MyVector list;

	// Token: 0x04000B77 RID: 2935
	public int selected;

	// Token: 0x04000B78 RID: 2936
	public int chan;

	// Token: 0x04000B79 RID: 2937
	public int menuX;

	// Token: 0x04000B7A RID: 2938
	public int menuY;

	// Token: 0x04000B7B RID: 2939
	public int menuW;

	// Token: 0x04000B7C RID: 2940
	public int menuH;

	// Token: 0x04000B7D RID: 2941
	public int menuTemY;

	// Token: 0x04000B7E RID: 2942
	public int w;

	// Token: 0x04000B7F RID: 2943
	public int hItem;

	// Token: 0x04000B80 RID: 2944
	public int wItem;

	// Token: 0x04000B81 RID: 2945
	public static FrameImage imgSellect;

	// Token: 0x04000B82 RID: 2946
	public static FrameImage imgBackIcon;

	// Token: 0x04000B83 RID: 2947
	public short[] radius;

	// Token: 0x04000B84 RID: 2948
	public int pos;

	// Token: 0x04000B85 RID: 2949
	public bool showMenuFarm;

	// Token: 0x04000B86 RID: 2950
	public int cmtoY;

	// Token: 0x04000B87 RID: 2951
	public int cmy;

	// Token: 0x04000B88 RID: 2952
	public int cmdy;

	// Token: 0x04000B89 RID: 2953
	public int cmvy;

	// Token: 0x04000B8A RID: 2954
	public int cmyLim;

	// Token: 0x04000B8B RID: 2955
	public int xTranMenu;

	// Token: 0x04000B8C RID: 2956
	public int xTranTo;

	// Token: 0x04000B8D RID: 2957
	private int xL;

	// Token: 0x04000B8E RID: 2958
	private int size;

	// Token: 0x04000B8F RID: 2959
	public static Command cmdClose;

	// Token: 0x04000B90 RID: 2960
	public IAction iNo;

	// Token: 0x04000B91 RID: 2961
	private MyVector listText = new MyVector();

	// Token: 0x04000B92 RID: 2962
	private int vY;

	// Token: 0x04000B93 RID: 2963
	private int disY;

	// Token: 0x04000B94 RID: 2964
	private int pa;

	// Token: 0x04000B95 RID: 2965
	private int dyTran;

	// Token: 0x04000B96 RID: 2966
	private int timeOpen;

	// Token: 0x04000B97 RID: 2967
	private int pyLast;

	// Token: 0x04000B98 RID: 2968
	private bool trans;

	// Token: 0x04000B99 RID: 2969
	private bool isClick;

	// Token: 0x04000B9A RID: 2970
	private bool isClose;

	// Token: 0x04000B9B RID: 2971
	private bool isFire;

	// Token: 0x04000B9C RID: 2972
	private bool isG;

	// Token: 0x04000B9D RID: 2973
	private long timeDelay;

	// Token: 0x04000B9E RID: 2974
	private long count;

	// Token: 0x04000B9F RID: 2975
	private long timePoint;

	// Token: 0x04000BA0 RID: 2976
	private int dir = 1;

	// Token: 0x04000BA1 RID: 2977
	private int xText;
}
