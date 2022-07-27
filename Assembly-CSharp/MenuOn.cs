using System;

// Token: 0x02000168 RID: 360
public class MenuOn : MenuMain
{
	// Token: 0x0600096C RID: 2412 RVA: 0x0005B4D8 File Offset: 0x000598D8
	public MenuOn()
	{
		this.initCmd();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0005B4E6 File Offset: 0x000598E6
	public static MenuOn gI()
	{
		return (MenuOn.me != null) ? MenuOn.me : (MenuOn.me = new MenuOn());
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0005B507 File Offset: 0x00059907
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index == 1)
			{
				this.showMenu = false;
			}
		}
		else
		{
			this.doFire();
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0005B532 File Offset: 0x00059932
	public void initCmd()
	{
		this.left = new Command(T.close, 1);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0005B545 File Offset: 0x00059945
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

	// Token: 0x06000971 RID: 2417 RVA: 0x0005B572 File Offset: 0x00059972
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

	// Token: 0x06000972 RID: 2418 RVA: 0x0005B5A8 File Offset: 0x000599A8
	private void doFire()
	{
		this.showMenu = false;
		if (MenuOn.cmdClose != null)
		{
			if (MenuOn.cmdClose.action != null)
			{
				MenuOn.cmdClose.action.perform();
			}
			else
			{
				this.commandTab((int)MenuOn.cmdClose.indexMenu);
			}
		}
		Command command = (Command)this.list.elementAt(this.selected);
		if (command.action != null)
		{
			command.action.perform();
		}
		else
		{
			Canvas.currentMyScreen.commandAction((int)command.indexMenu);
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0005B640 File Offset: 0x00059A40
	public override void updateKey()
	{
		this.updateMenuKeyMain();
		Canvas.paint.updateKeyOn(this.left, this.right, this.center);
		if (this.timeOpen > 0)
		{
			this.timeOpen--;
			if (this.timeOpen == 0)
			{
				this.click();
			}
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0005B69C File Offset: 0x00059A9C
	private void click()
	{
		int num = this.hItem;
		int num2 = this.menuTemY + Canvas.transTab;
		int num3 = (this.cmtoY + Canvas.py - num2) / num;
		if (num3 >= 0 && num3 < this.size)
		{
			this.selected = num3;
			this.doFire();
		}
		this.isHide = true;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0005B6F8 File Offset: 0x00059AF8
	private void updateMenuKeyMain()
	{
		this.count += 1L;
		if (this.chan == 0)
		{
			bool flag = false;
			if (Canvas.isPointerClick)
			{
				this.isClick = true;
				if (Canvas.isPoint(this.menuX - 2, this.menuTemY - 7 + Canvas.transTab, this.menuW + 4, this.menuH + 15))
				{
					this.isTran = true;
					Canvas.isPointerClick = false;
					this.pa = this.cmy;
					this.timeDelay = this.count;
					this.trans = true;
				}
			}
			if (this.trans)
			{
				int num = Canvas.dy();
				long num2 = this.count - this.timeDelay;
				if (Canvas.isPointerDown)
				{
					if (Canvas.gameTick % 3 == 0)
					{
						this.dyTran = Canvas.py;
						this.timePoint = this.count;
					}
					this.vY = 0;
					if (global::Math.abs(num) < 20 * AvMain.hd)
					{
						int num3 = this.menuTemY + 1 + Canvas.transTab;
						int num4 = this.hItem;
						int num5 = (this.cmtoY + Canvas.py - num3) / num4;
						if (num5 >= 0 && num5 < this.size)
						{
							this.selected = num5;
						}
					}
					if (CRes.abs(num) >= 20 * AvMain.hd)
					{
						this.isHide = true;
					}
					else if (num2 > 3L && num2 < 8L)
					{
						this.isHide = false;
					}
					this.cmtoY = this.pa + num;
					if (this.cmtoY < 0 || this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.pa + num / 3;
					}
					this.cmy = this.cmtoY;
				}
				if (Canvas.isPointerRelease && Canvas.isPoint(this.menuX - 2, this.menuTemY - 7 + Canvas.transTab, this.menuW + 4, this.menuH + 15))
				{
					int num6 = (int)(this.count - this.timePoint);
					int num7 = this.dyTran - Canvas.py;
					if (CRes.abs(num7) > 40 && num6 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
					{
						this.vY = num7 / num6 * 10;
					}
					this.timePoint = -1L;
					if (global::Math.abs(num) < 20 * AvMain.hd)
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
					Canvas.isPointerRelease = false;
				}
			}
			if (this.isClick && Canvas.isPointerRelease)
			{
				this.isClick = false;
				if (!this.trans)
				{
					this.showMenu = false;
				}
				this.trans = false;
				Canvas.isPointerRelease = false;
			}
			if (flag)
			{
				int num8 = this.hItem;
				this.cmtoY = this.selected * num8 - this.menuW / 2 + num8 / 2;
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
			}
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0005BA3C File Offset: 0x00059E3C
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		Canvas.resetTrans(g);
		if (Canvas.currentDialog == null)
		{
			Canvas.paint.paintTabSoft(g);
			Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0005BA88 File Offset: 0x00059E88
	public void paintMenuNormal(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.drawImage(MenuOn.imgTab, (float)this.menuX, (float)(this.menuY - 5 * AvMain.hd), 0);
		g.setClip((float)this.menuX, (float)this.menuY, (float)this.menuW, (float)(this.menuH - 10 * AvMain.hd));
		g.translate((float)(this.menuX + 3), (float)(this.menuTemY + 1));
		g.translate(0f, (float)(-(float)this.cmy));
		int num = (this.hItem - (int)AvMain.hNormal) / 2;
		for (int i = 0; i < this.size; i++)
		{
			g.setColor(0);
			if (!this.isHide && i == this.selected)
			{
				g.drawImageScale(MenuOn.imgSelect, 0, i * this.hItem, this.menuW - 6, this.hItem, 0);
			}
			int num2 = 0;
			Canvas.normalWhiteFont.drawString(g, ((Command)this.list.elementAt(i)).caption, 5 + num2, i * this.hItem + num, 0);
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0005BBAE File Offset: 0x00059FAE
	public void paintMain(MyGraphics g)
	{
		if (this.size == 0)
		{
			return;
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (this.chan == 0)
		{
			this.paintMenuNormal(g);
		}
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0005BBE4 File Offset: 0x00059FE4
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
		this.moveCamera();
		this.updateMain();
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0005BC5C File Offset: 0x0005A05C
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

	// Token: 0x0600097B RID: 2427 RVA: 0x0005BE50 File Offset: 0x0005A250
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

	// Token: 0x04000BF1 RID: 3057
	public static MenuOn me;

	// Token: 0x04000BF2 RID: 3058
	public bool showMenu;

	// Token: 0x04000BF3 RID: 3059
	public MyVector list;

	// Token: 0x04000BF4 RID: 3060
	public int selected;

	// Token: 0x04000BF5 RID: 3061
	public int chan;

	// Token: 0x04000BF6 RID: 3062
	public int menuX;

	// Token: 0x04000BF7 RID: 3063
	public int menuY;

	// Token: 0x04000BF8 RID: 3064
	public int menuW;

	// Token: 0x04000BF9 RID: 3065
	public int menuH;

	// Token: 0x04000BFA RID: 3066
	public int menuTemY;

	// Token: 0x04000BFB RID: 3067
	public int w;

	// Token: 0x04000BFC RID: 3068
	public int hItem;

	// Token: 0x04000BFD RID: 3069
	public int wItem;

	// Token: 0x04000BFE RID: 3070
	public int pos;

	// Token: 0x04000BFF RID: 3071
	public int cmtoY;

	// Token: 0x04000C00 RID: 3072
	public int cmy;

	// Token: 0x04000C01 RID: 3073
	public int cmdy;

	// Token: 0x04000C02 RID: 3074
	public int cmvy;

	// Token: 0x04000C03 RID: 3075
	public int cmyLim;

	// Token: 0x04000C04 RID: 3076
	private int xL;

	// Token: 0x04000C05 RID: 3077
	private int size;

	// Token: 0x04000C06 RID: 3078
	public static Command cmdClose;

	// Token: 0x04000C07 RID: 3079
	public static Image imgTab;

	// Token: 0x04000C08 RID: 3080
	public static Image imgSelect;

	// Token: 0x04000C09 RID: 3081
	private int vY;

	// Token: 0x04000C0A RID: 3082
	private int disY;

	// Token: 0x04000C0B RID: 3083
	private int pa;

	// Token: 0x04000C0C RID: 3084
	private int dyTran;

	// Token: 0x04000C0D RID: 3085
	private int timeOpen;

	// Token: 0x04000C0E RID: 3086
	private bool trans;

	// Token: 0x04000C0F RID: 3087
	private bool isClick;

	// Token: 0x04000C10 RID: 3088
	private long timeDelay;

	// Token: 0x04000C11 RID: 3089
	private long count;

	// Token: 0x04000C12 RID: 3090
	private long timePoint;
}
