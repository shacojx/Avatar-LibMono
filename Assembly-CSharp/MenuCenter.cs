using System;

// Token: 0x02000164 RID: 356
public class MenuCenter : MenuMain
{
	// Token: 0x0600094D RID: 2381 RVA: 0x00059B94 File Offset: 0x00057F94
	public MenuCenter()
	{
		this.w = 175 * AvMain.hd;
		this.h = 200 * AvMain.hd;
		this.hCell = 35 * AvMain.hd;
		this.hDis = this.h - 15 * AvMain.hd;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x00059BEC File Offset: 0x00057FEC
	public static MenuCenter gI()
	{
		return (MenuCenter.me != null) ? MenuCenter.me : (MenuCenter.me = new MenuCenter());
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00059C10 File Offset: 0x00058010
	public void startAt(MyVector list)
	{
		this.x = (Canvas.w - this.w) / 2;
		this.y = (Canvas.hCan - this.h) / 2;
		this.list = list;
		this.cmyLim = list.size() * this.hCell - this.hDis;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = 0);
		this.isHide = true;
		base.show();
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00059C96 File Offset: 0x00058096
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
		this.moveCamera();
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00059CCC File Offset: 0x000580CC
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
				if (this.cmy < -this.h / 2)
				{
					this.cmy = -this.h / 2;
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

	// Token: 0x06000952 RID: 2386 RVA: 0x00059ECC File Offset: 0x000582CC
	public void click1()
	{
		Out.println("click1: " + this.selected);
		this.trans = false;
		Canvas.menuMain = null;
		if (this.selected < this.list.size())
		{
			Command command = (Command)this.list.elementAt(this.selected);
			command.perform();
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00059F34 File Offset: 0x00058334
	public override void updateKey()
	{
		this.count += 1L;
		if (Canvas.isPointerClick)
		{
			this.isClick = true;
			Canvas.isPointerClick = false;
			if (Canvas.isPoint(this.x, this.y, this.w, this.h))
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
				this.dyTran = Canvas.py;
				this.timePoint = this.count;
				this.vY = 0;
				if (global::Math.abs(num) < 10 * AvMain.hd)
				{
					int num3 = this.y + 10;
					int num4 = (this.cmtoY + Canvas.py - num3) / this.hCell;
					if (num4 >= 0 && num4 < this.list.size())
					{
						this.selected = num4;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num2 > 3L && num2 < 8L)
				{
					int num5 = this.y + 10;
					int num6 = (this.cmtoY + Canvas.py - num5) / this.hCell;
					if (num6 >= 0 && num6 < this.list.size() && !this.isG)
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
				int num7 = 0;
				this.isG = false;
				int num8 = (int)(this.count - this.timePoint);
				int num9 = this.dyTran - Canvas.py;
				if (CRes.abs(num9) > 40 && num8 < 10 && this.cmtoY > 0 && this.cmtoY < this.cmyLim)
				{
					this.vY = num9 / num8 * 10;
				}
				if (CRes.abs(Canvas.dy()) > 10 * AvMain.hd)
				{
					num7 = 1;
				}
				this.timePoint = -1L;
				if (global::Math.abs(num) < 10 * AvMain.hd)
				{
					if (num2 <= 4L)
					{
						this.isHide = false;
						this.timeOpen1 = 5;
						num7 = 1;
					}
					else if (!this.isHide)
					{
						this.click1();
						num7 = 1;
					}
				}
				this.trans = false;
				this.isClick = false;
				Canvas.isPointerRelease = false;
				if (num7 == 0)
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
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0005A298 File Offset: 0x00058698
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, -1, false);
		g.translate((float)this.x, (float)(this.y + 5 * AvMain.hd));
		g.setClip(0f, 0f, (float)this.w, (float)this.hDis);
		g.translate(0f, (float)(-(float)this.cmy));
		for (int i = 0; i < this.list.size(); i++)
		{
			if (i == this.selected && !this.isHide)
			{
				g.setColor(16777215);
				g.fillRect((float)(10 * AvMain.hd), (float)(i * this.hCell), (float)(this.w - 20 * AvMain.hd), (float)this.hCell);
			}
			Command command = (Command)this.list.elementAt(i);
			Canvas.normalFont.drawString(g, command.caption, this.w / 2, this.hCell / 2 + i * this.hCell - Canvas.normalFont.getHeight() / 2, 2);
		}
	}

	// Token: 0x04000BA2 RID: 2978
	public static MenuCenter me;

	// Token: 0x04000BA3 RID: 2979
	private int x;

	// Token: 0x04000BA4 RID: 2980
	private int y;

	// Token: 0x04000BA5 RID: 2981
	private int w;

	// Token: 0x04000BA6 RID: 2982
	private int h;

	// Token: 0x04000BA7 RID: 2983
	private int hCell;

	// Token: 0x04000BA8 RID: 2984
	private MyVector list;

	// Token: 0x04000BA9 RID: 2985
	public int cmtoY;

	// Token: 0x04000BAA RID: 2986
	public int cmy;

	// Token: 0x04000BAB RID: 2987
	public int cmdy;

	// Token: 0x04000BAC RID: 2988
	public int cmvy;

	// Token: 0x04000BAD RID: 2989
	public int cmyLim;

	// Token: 0x04000BAE RID: 2990
	public int selected;

	// Token: 0x04000BAF RID: 2991
	public int hDis;

	// Token: 0x04000BB0 RID: 2992
	private new bool isHide;

	// Token: 0x04000BB1 RID: 2993
	private int timeOpen1;

	// Token: 0x04000BB2 RID: 2994
	private bool trans;

	// Token: 0x04000BB3 RID: 2995
	private bool isG;

	// Token: 0x04000BB4 RID: 2996
	private bool isClick;

	// Token: 0x04000BB5 RID: 2997
	private int pa;

	// Token: 0x04000BB6 RID: 2998
	private int dxTran;

	// Token: 0x04000BB7 RID: 2999
	private int timeOpen;

	// Token: 0x04000BB8 RID: 3000
	private int pyLast;

	// Token: 0x04000BB9 RID: 3001
	private int dyTran;

	// Token: 0x04000BBA RID: 3002
	private long delay;

	// Token: 0x04000BBB RID: 3003
	private long timeDelay;

	// Token: 0x04000BBC RID: 3004
	private long count;

	// Token: 0x04000BBD RID: 3005
	private long timePoint;

	// Token: 0x04000BBE RID: 3006
	private int vX;

	// Token: 0x04000BBF RID: 3007
	private int vY;
}
