using System;

// Token: 0x0200005C RID: 92
public class MenuIcon : MenuMain
{
	// Token: 0x06000350 RID: 848 RVA: 0x0001D37F File Offset: 0x0001B77F
	public static MenuIcon gI()
	{
		return (MenuIcon.me != null) ? MenuIcon.me : (MenuIcon.me = new MenuIcon());
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001D3A0 File Offset: 0x0001B7A0
	public void setInfo(MyVector list, int xCen, int yCen)
	{
		if (this.imgFocus == null)
		{
			this.imgFocus = new Image[2];
			for (int i = 0; i < 2; i++)
			{
				this.imgFocus[i] = Image.createImagePNG(T.getPath() + "/iconMenu/focusAction" + i);
			}
		}
		this.list = list;
		this.tranPos = new PositionTran[list.size()];
		this.hCell = 45 * AvMain.hd;
		this.xCenter = xCen;
		this.yCenter = yCen;
		for (int j = 0; j < list.size(); j++)
		{
			this.tranPos[j] = new PositionTran(this.xCenter, this.yCenter);
			this.tranPos[j].xTo = this.tranPos[j].x;
			this.tranPos[j].yTo = 25 * AvMain.hd + Canvas.countTab + j * this.hCell;
		}
		this.wCell = 30 * AvMain.hd;
		this.hDis = Canvas.hCan;
		this.cmyLim = list.size() * this.hCell - (this.hDis - Canvas.countTab * 2);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = 0);
		this.isHide = true;
		base.show();
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0001D508 File Offset: 0x0001B908
	public override void update()
	{
		if (this.timeOpen1 > 0)
		{
			this.timeOpen1--;
			if (this.timeOpen1 == 0)
			{
				this.click1();
			}
		}
		for (int i = 0; i < this.list.size(); i++)
		{
			this.tranPos[i].update();
		}
		this.moveCamera();
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0001D570 File Offset: 0x0001B970
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
				if (this.cmy < -this.hDis / 2)
				{
					this.cmy = -this.hDis / 2;
					this.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (this.cmy > this.cmyLim)
			{
				if (this.cmy < this.cmyLim + this.hDis / 2)
				{
					this.cmy = this.cmyLim + this.hDis / 2;
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
		else if (!this.trans)
		{
			if (this.cmy < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmy > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
		}
		if (this.cmy != this.cmtoY)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0001D770 File Offset: 0x0001BB70
	public void click1()
	{
		this.trans = false;
		Command command = (Command)this.list.elementAt(this.selected);
		command.perform();
		Canvas.menuMain = null;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0001D7A8 File Offset: 0x0001BBA8
	public override void updateKey()
	{
		this.count += 1L;
		if (Canvas.isPointerClick)
		{
			this.isClick = true;
			Canvas.isPointerClick = false;
			if (Canvas.isPoint(this.xCenter - this.wCell - this.wCell / 2, Canvas.countTab, this.wCell * 3, this.hDis))
			{
				this.trans = true;
				this.pyLast = Canvas.pyLast;
				this.isG = false;
				if (this.vY != 0)
				{
					this.isG = true;
				}
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
					int countTab = Canvas.countTab;
					int num3 = (this.cmtoY + Canvas.py - countTab) / this.hCell;
					if (num3 >= 0 && num3 < this.list.size())
					{
						this.selected = num3;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num2 > 3L && num2 < 8L)
				{
					int countTab2 = Canvas.countTab;
					int num4 = (this.cmtoY + Canvas.py - countTab2) / this.hCell;
					if (num4 >= 0 && num4 < this.list.size() && !this.isG)
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
			if (Canvas.isPointerRelease)
			{
				this.trans = false;
				Canvas.isPointerClick = false;
				int num5 = 0;
				this.isG = false;
				int num6 = (int)(this.count - this.timePoint);
				int num7 = this.dyTran - Canvas.py;
				if (CRes.abs(num7) > 40 && num6 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
				{
					this.vY = num7 / num6 * 10;
				}
				if (CRes.abs(Canvas.dy()) > 10 * AvMain.hd)
				{
					num5 = 1;
				}
				this.timePoint = -1L;
				if (global::Math.abs(num) < 10 * AvMain.hd)
				{
					if (num2 <= 4L)
					{
						this.isHide = false;
						this.timeOpen1 = 5;
						num5 = 1;
					}
					else if (!this.isHide)
					{
						this.click1();
						num5 = 1;
					}
				}
				this.trans = false;
				this.isClick = false;
				Canvas.isPointerRelease = false;
				if (num5 == 0)
				{
					Canvas.menuMain = null;
					return;
				}
			}
		}
		if (this.isClick && Canvas.isPointerRelease)
		{
			this.isClick = false;
			Canvas.isPointerRelease = false;
			Canvas.menuMain = null;
		}
		base.updateKey();
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001DB2C File Offset: 0x0001BF2C
	public override void paint(MyGraphics g)
	{
		Canvas.resetTransNotZoom(g);
		g.translate(0f, (float)(-(float)this.cmy));
		for (int i = 0; i < this.list.size(); i++)
		{
			Command command = (Command)this.list.elementAt(i);
			if (!this.isHide && i == this.selected)
			{
				g.drawImage(this.imgFocus[1], (float)this.tranPos[i].x, (float)this.tranPos[i].y, 3);
			}
			else
			{
				g.drawImage(this.imgFocus[0], (float)this.tranPos[i].x, (float)this.tranPos[i].y, 3);
			}
			Canvas.smallFontYellow.drawString(g, command.caption, this.tranPos[i].x, this.tranPos[i].y - 20 * AvMain.hd - (int)AvMain.hSmall / 2, 2);
			command.paint(g, this.tranPos[i].x, this.tranPos[i].y);
		}
		base.paint(g);
	}

	// Token: 0x040003FA RID: 1018
	public static MenuIcon me;

	// Token: 0x040003FB RID: 1019
	private MyVector list;

	// Token: 0x040003FC RID: 1020
	private int xCenter;

	// Token: 0x040003FD RID: 1021
	private int yCenter;

	// Token: 0x040003FE RID: 1022
	private int wCell;

	// Token: 0x040003FF RID: 1023
	private int hCell;

	// Token: 0x04000400 RID: 1024
	public int cmtoY;

	// Token: 0x04000401 RID: 1025
	public int cmy;

	// Token: 0x04000402 RID: 1026
	public int cmdy;

	// Token: 0x04000403 RID: 1027
	public int cmvy;

	// Token: 0x04000404 RID: 1028
	public int cmyLim;

	// Token: 0x04000405 RID: 1029
	public int hDis;

	// Token: 0x04000406 RID: 1030
	public int selected;

	// Token: 0x04000407 RID: 1031
	private PositionTran[] tranPos;

	// Token: 0x04000408 RID: 1032
	private Image[] imgFocus;

	// Token: 0x04000409 RID: 1033
	private int timeOpen1;

	// Token: 0x0400040A RID: 1034
	private bool trans;

	// Token: 0x0400040B RID: 1035
	private bool isG;

	// Token: 0x0400040C RID: 1036
	private bool isClick;

	// Token: 0x0400040D RID: 1037
	private int pa;

	// Token: 0x0400040E RID: 1038
	private int dxTran;

	// Token: 0x0400040F RID: 1039
	private int timeOpen;

	// Token: 0x04000410 RID: 1040
	private int pyLast;

	// Token: 0x04000411 RID: 1041
	private int dyTran;

	// Token: 0x04000412 RID: 1042
	private long delay;

	// Token: 0x04000413 RID: 1043
	private long timeDelay;

	// Token: 0x04000414 RID: 1044
	private long count;

	// Token: 0x04000415 RID: 1045
	private long timePoint;

	// Token: 0x04000416 RID: 1046
	private int vX;

	// Token: 0x04000417 RID: 1047
	private int vY;
}
