using System;

// Token: 0x02000169 RID: 361
public class MenuSub : MenuMain
{
	// Token: 0x0600097C RID: 2428 RVA: 0x0005BEA1 File Offset: 0x0005A2A1
	public MenuSub()
	{
		this.w = 200 * AvMain.hd;
		this.hItem = 35 * AvMain.hd;
		this.h = this.hItem * 4;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0005BED6 File Offset: 0x0005A2D6
	public static MenuSub gI()
	{
		return (MenuSub.me != null) ? MenuSub.me : (MenuSub.me = new MenuSub());
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0005BEF8 File Offset: 0x0005A2F8
	public void startAt(MyVector menuList, int x, int y)
	{
		this.list = menuList;
		this.x = x;
		this.y = y;
		MenuSub.cmyLim = this.list.size() * this.hItem - this.h / 2 - 90;
		MenuSub.cmy = (MenuSub.cmtoY = 0);
		if (MenuSub.cmyLim < 0)
		{
			MenuSub.cmyLim = 0;
		}
		this.isHide = true;
		base.show();
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0005BF68 File Offset: 0x0005A368
	private void click()
	{
		Command command = (Command)this.list.elementAt(this.index);
		command.perform();
		this.isHide = true;
		this.list.removeAllElements();
		Canvas.menuMain = null;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0005BFAC File Offset: 0x0005A3AC
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
		if (this.vY != 0)
		{
			if (MenuSub.cmy < 0 || MenuSub.cmy > MenuSub.cmyLim)
			{
				if (this.vY > 500)
				{
					this.vY = 500;
				}
				else if (this.vY < -500)
				{
					this.vY = -500;
				}
				this.vY -= this.vY / 5;
				if (CRes.abs(this.vY / 10) <= 10)
				{
					this.vY = 0;
				}
			}
			MenuSub.cmy += this.vY / 15;
			MenuSub.cmtoY = MenuSub.cmy;
			this.vY -= this.vY / 20;
		}
		else if (MenuSub.cmy < 0)
		{
			MenuSub.cmtoY = 0;
		}
		else if (MenuSub.cmy > MenuSub.cmyLim)
		{
			MenuSub.cmtoY = MenuSub.cmyLim;
		}
		if (MenuSub.cmy != MenuSub.cmtoY)
		{
			MenuSub.cmvy = MenuSub.cmtoY - MenuSub.cmy << 2;
			MenuSub.cmdy += MenuSub.cmvy;
			MenuSub.cmy += MenuSub.cmdy >> 4;
			MenuSub.cmdy &= 15;
		}
		Canvas.loadMap.update();
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0005C138 File Offset: 0x0005A538
	public override void updateKey()
	{
		this.count += 1L;
		PaintPopup.gI().setupdateTab();
		if (Canvas.isPointerClick && Canvas.isPoint(this.x, this.y, this.w, this.h))
		{
			this.isTran = true;
			this.isG = false;
			if (this.vY != 0)
			{
				this.isG = true;
			}
			this.pyLast = Canvas.pyLast;
			Canvas.isPointerClick = false;
			this.pa = MenuSub.cmy;
			this.transY = true;
			this.timeDelay = this.count;
			this.isFire = true;
		}
		if (this.transY)
		{
			long num = this.count - this.timeDelay;
			int num2 = this.pyLast - Canvas.py;
			this.pyLast = Canvas.py;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2L == 0L)
				{
					this.dyTran = Canvas.py;
					this.timePoint = this.count;
				}
				this.vY = 0;
				int num3 = (MenuSub.cmtoY + Canvas.py - this.y) / this.hItem;
				this.index = num3;
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num > 3L && num < 8L && this.isFire && !this.isG)
				{
					this.isHide = false;
				}
				MenuSub.cmtoY = this.pa + num2;
				if (MenuSub.cmtoY < 0 || MenuSub.cmtoY > MenuSub.cmyLim)
				{
					MenuSub.cmtoY = this.pa + num2 / 2;
				}
				this.pa = MenuSub.cmtoY;
				MenuSub.cmy = MenuSub.cmtoY;
			}
			if (Canvas.isPointerRelease && Canvas.isPoint(this.x, this.y, this.w, this.h))
			{
				this.isG = false;
				int num4 = (int)(this.count - this.timePoint);
				int num5 = this.dyTran - Canvas.py;
				if (CRes.abs(num5) > 40 && num4 < 10 && MenuSub.cmtoY > 0 && MenuSub.cmtoY < MenuSub.cmyLim)
				{
					this.vY = num5 / num4 * 10;
				}
				this.timePoint = -1L;
				if (global::Math.abs(Canvas.dy()) < 10 * AvMain.hd && this.isFire)
				{
					if (num <= 4L)
					{
						if (this.isFire)
						{
							this.isHide = false;
						}
						this.timeOpen = 5;
					}
					else if (!this.isHide)
					{
						this.click();
					}
					this.isFire = false;
				}
			}
		}
		if (Canvas.isPointerRelease && !this.transY && !Canvas.isPoint(this.x, this.y, this.w, this.h))
		{
			this.transY = false;
			this.list.removeAllElements();
			Canvas.menuMain = null;
		}
		if (Canvas.isPointerRelease)
		{
			this.transY = false;
		}
		base.updateKey();
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0005C464 File Offset: 0x0005A864
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.x, this.y - 8 * AvMain.hd, this.w, this.h + 16 * AvMain.hd, -1, false);
		g.translate((float)this.x, (float)this.y);
		g.setClip(0f, 0f, (float)this.w, (float)this.h);
		g.translate(0f, (float)(-(float)MenuSub.cmy));
		for (int i = 0; i < this.list.size(); i++)
		{
			Command command = (Command)this.list.elementAt(i);
			if (i == this.index && !this.isHide)
			{
				g.setColor(16777215);
				g.fillRect((float)(10 * AvMain.hd), (float)(i * this.hItem), (float)(this.w - 20 * AvMain.hd), (float)this.hItem);
			}
			Canvas.normalFont.drawString(g, command.caption, 20, i * this.hItem + this.hItem / 2 - Canvas.normalFont.getHeight() / 2, 0);
		}
	}

	// Token: 0x04000C13 RID: 3091
	public static MenuSub me;

	// Token: 0x04000C14 RID: 3092
	public MyVector list;

	// Token: 0x04000C15 RID: 3093
	public int x;

	// Token: 0x04000C16 RID: 3094
	public int y;

	// Token: 0x04000C17 RID: 3095
	public int w;

	// Token: 0x04000C18 RID: 3096
	public int h;

	// Token: 0x04000C19 RID: 3097
	public int hItem;

	// Token: 0x04000C1A RID: 3098
	public int index;

	// Token: 0x04000C1B RID: 3099
	public static int cmtoY;

	// Token: 0x04000C1C RID: 3100
	public static int cmy;

	// Token: 0x04000C1D RID: 3101
	public static int cmdy;

	// Token: 0x04000C1E RID: 3102
	public static int cmvy;

	// Token: 0x04000C1F RID: 3103
	public static int cmyLim;

	// Token: 0x04000C20 RID: 3104
	private int vY;

	// Token: 0x04000C21 RID: 3105
	private long count;

	// Token: 0x04000C22 RID: 3106
	private long timePoint;

	// Token: 0x04000C23 RID: 3107
	private int dyTran;

	// Token: 0x04000C24 RID: 3108
	private int timeOpen;

	// Token: 0x04000C25 RID: 3109
	private int pyLast;

	// Token: 0x04000C26 RID: 3110
	private bool isFire;

	// Token: 0x04000C27 RID: 3111
	private bool isG;

	// Token: 0x04000C28 RID: 3112
	private bool transY;

	// Token: 0x04000C29 RID: 3113
	private int pa;

	// Token: 0x04000C2A RID: 3114
	private long timeDelay;
}
