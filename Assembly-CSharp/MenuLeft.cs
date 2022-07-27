using System;

// Token: 0x02000165 RID: 357
public class MenuLeft : MenuMain
{
	// Token: 0x06000956 RID: 2390 RVA: 0x0005A3E3 File Offset: 0x000587E3
	public static MenuLeft gI()
	{
		return (MenuLeft.me != null) ? MenuLeft.me : new MenuLeft();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0005A400 File Offset: 0x00058800
	public void startAt(MyVector list)
	{
		if (this.imgBack == null)
		{
			this.imgBack = Image.createImagePNG(T.getPath() + "/iconMenu/backMenu");
			this.x = 32 * AvMain.hd - this.imgBack.w / 2;
			this.y = 28 * AvMain.hd;
			this.w = this.imgBack.w;
			this.h = this.imgBack.h - 11 * AvMain.hd;
		}
		this.hCell = this.h / list.size();
		this.imgIcon = new Image[list.size()][];
		for (int i = 0; i < list.size(); i++)
		{
			Command command = (Command)list.elementAt(i);
			this.imgIcon[i] = new Image[2];
			this.imgIcon[i][0] = Image.createImagePNG(string.Concat(new object[]
			{
				T.getPath(),
				"/iconMenu/",
				command.indexImage,
				"0"
			}));
			this.imgIcon[i][1] = Image.createImagePNG(string.Concat(new object[]
			{
				T.getPath(),
				"/iconMenu/",
				command.indexImage,
				"1"
			}));
		}
		this.list = list;
		this.selected = -1;
		this.count = 0;
		base.show();
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0005A578 File Offset: 0x00058978
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
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0005A5A8 File Offset: 0x000589A8
	public override void updateKey()
	{
		this.count++;
		if (Canvas.isPointerClick)
		{
			this.isClick = true;
			Canvas.isPointerClick = false;
			if (Canvas.isPoint(this.x, this.y + Canvas.countTab, this.w, this.h))
			{
				this.isTranKey = true;
			}
			this.countCur = this.count;
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown)
			{
				if (CRes.abs(Canvas.dx()) < 10 * AvMain.hd && CRes.abs(Canvas.dy()) < 10 * AvMain.hd)
				{
					if (this.count - this.countCur > 3)
					{
						int num = (Canvas.py - (this.y + Canvas.countTab)) / this.hCell;
						if (num >= 0 && num < this.list.size())
						{
							this.selected = num;
						}
					}
				}
				else if (this.selected != -1)
				{
					int num2 = (Canvas.py - (this.y + Canvas.countTab)) / this.hCell;
					if (num2 != this.selected)
					{
						this.selected = -1;
					}
				}
			}
			if (Canvas.isPointerRelease)
			{
				this.isTranKey = false;
				if (CRes.abs(Canvas.dx()) < 10 * AvMain.hd && CRes.abs(Canvas.dy()) < 10 * AvMain.hd)
				{
					int num3 = (Canvas.py - this.y) / this.hCell;
					if (num3 >= 0 && num3 < this.list.size())
					{
						this.selected = num3;
					}
				}
				if (this.selected != -1)
				{
					this.click();
				}
				Canvas.isPointerRelease = false;
			}
		}
		if (this.isClick && Canvas.isPointerRelease)
		{
			this.isClick = false;
			Canvas.isPointerRelease = false;
			Canvas.menuMain = null;
		}
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0005A794 File Offset: 0x00058B94
	private void click()
	{
		this.close();
		Command command = (Command)this.list.elementAt(this.selected);
		command.perform();
		this.selected = -1;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0005A7CB File Offset: 0x00058BCB
	public void close()
	{
		Canvas.menuMain = null;
		this.imgIcon = null;
		this.imgBack = null;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0005A7E4 File Offset: 0x00058BE4
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.drawImage(this.imgBack, (float)this.x, (float)(19 * AvMain.hd + Canvas.countTab), 0);
		g.translate((float)this.x, (float)(this.y + Canvas.countTab));
		for (int i = 0; i < this.list.size(); i++)
		{
			Command command = (Command)this.list.elementAt(i);
			g.drawImage(this.imgIcon[(int)command.indexImage][(this.selected != i) ? 0 : 1], (float)(this.w / 2), (float)(this.hCell / 2 + i * this.hCell), 3);
		}
	}

	// Token: 0x04000BC0 RID: 3008
	public static MenuLeft me;

	// Token: 0x04000BC1 RID: 3009
	public MyVector list;

	// Token: 0x04000BC2 RID: 3010
	private Image imgBack;

	// Token: 0x04000BC3 RID: 3011
	private Image[][] imgIcon;

	// Token: 0x04000BC4 RID: 3012
	private int x;

	// Token: 0x04000BC5 RID: 3013
	private int y;

	// Token: 0x04000BC6 RID: 3014
	private int w;

	// Token: 0x04000BC7 RID: 3015
	private int h;

	// Token: 0x04000BC8 RID: 3016
	private int hCell;

	// Token: 0x04000BC9 RID: 3017
	private int selected = -1;

	// Token: 0x04000BCA RID: 3018
	private bool isTranKey;

	// Token: 0x04000BCB RID: 3019
	private bool isClick;

	// Token: 0x04000BCC RID: 3020
	private int count;

	// Token: 0x04000BCD RID: 3021
	private int countCur;

	// Token: 0x04000BCE RID: 3022
	private int timeOpen;
}
