using System;

// Token: 0x02000167 RID: 359
public class MenuNPC : MenuMain
{
	// Token: 0x06000963 RID: 2403 RVA: 0x0005A8D8 File Offset: 0x00058CD8
	public MenuNPC()
	{
		this.w = 250 * AvMain.hd;
		this.h = 187 * AvMain.hd;
		this.x = (Canvas.w - this.w) / 2;
		this.y = (Canvas.h - this.h) / 2;
		this.yList = 75 * AvMain.hd;
		this.wList = 160 * AvMain.hd;
		this.xList = this.w - this.wList - 13 * AvMain.hd;
		this.hItem = 30 * AvMain.hd;
		this.hList = 100 * AvMain.hd;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0005A997 File Offset: 0x00058D97
	public static MenuNPC gI()
	{
		return (MenuNPC.me != null) ? MenuNPC.me : (MenuNPC.me = new MenuNPC());
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0005A9B8 File Offset: 0x00058DB8
	public void setInfo(MyVector list, int idUser, string nameNPC, string textChat, bool[] isMenu)
	{
		if (this.imgBackPopup == null)
		{
			this.imgBackPopup = Image.createImagePNG(T.getPath() + "/effect/backMenuNPC");
			this.imgBackChat = Image.createImagePNG(T.getPath() + "/effect/popupChat");
		}
		this.list = list;
		this.isMenu = isMenu;
		this.idUser = idUser;
		this.cmyLim = list.size() * this.hItem - (this.hList - 20 * AvMain.hd);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.nameNPC = nameNPC;
		this.textChat = Canvas.fontChatB.splitFontBStrInLine(textChat, this.w - 70 * AvMain.hd);
		base.show();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0005AA7E File Offset: 0x00058E7E
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
		this.moveCamera();
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0005AAB4 File Offset: 0x00058EB4
	public override void updateKey()
	{
		this.count += 1L;
		if (Canvas.isPointerClick)
		{
			this.pyLast = Canvas.pyLast;
			this.isG = false;
			if (Canvas.isPoint(this.x + this.xList, this.y + this.yList, this.wList, this.hList))
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
					int num3 = this.y + this.yList + 10 * AvMain.hd;
					int num4 = this.hItem;
					int num5 = (this.cmtoY + Canvas.py - num3) / num4;
					if (num5 >= 0 && num5 < this.list.size())
					{
						this.focus = num5;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd)
				{
					this.isHide = true;
				}
				else if (num2 > 3L && num2 < 8L)
				{
					int num6 = this.y + this.yList + 10 * AvMain.hd;
					int num7 = this.hItem;
					int num8 = (this.cmtoY + Canvas.py - num6) / num7;
					if (num8 >= 0 && num8 < this.list.size() && !this.isG)
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
			if (Canvas.isPointerRelease && Canvas.isPoint(this.x, this.y, this.w, this.h))
			{
				this.isG = false;
				int num9 = (int)(this.count - this.timePoint);
				int num10 = this.dyTran - Canvas.py;
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
		else if (Canvas.isPointerRelease && !Canvas.isPoint(this.x, this.y, this.w, this.h))
		{
			Canvas.isPointerRelease = false;
			Canvas.menuMain = null;
		}
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0005AE40 File Offset: 0x00059240
	private void click()
	{
		if (!this.isMenu[this.focus])
		{
			Canvas.menuMain = null;
		}
		else
		{
			Canvas.startWaitDlg();
		}
		Command command = (Command)this.list.elementAt(this.focus);
		command.perform();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0005AE8C File Offset: 0x0005928C
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, -1, false);
		g.translate((float)this.x, (float)this.y);
		g.drawImage(this.imgBackChat, (float)(12 * AvMain.hd), (float)(12 * AvMain.hd), 0);
		for (int i = 0; i < this.textChat.Length; i++)
		{
			Canvas.fontChatB.drawString(g, this.textChat[i], 20 * AvMain.hd, 12 * AvMain.hd + 25 * AvMain.hd - this.textChat.Length * Canvas.fontChatB.getHeight() / 2 + i * (int)AvMain.hBlack + ((AvMain.hd != 1) ? 0 : 10), 0);
		}
		Avatar avatar = LoadMap.getAvatar(this.idUser);
		Canvas.normalFont.drawString(g, this.nameNPC, this.xList / 2, this.yList + this.hList / 2 - (int)AvMain.hNormal - 20 * AvMain.hd, 2);
		avatar.paintIcon(g, this.xList / 2, this.yList + this.hList / 2 + (int)avatar.height, false);
		g.translate((float)this.xList, (float)this.yList);
		g.drawImage(this.imgBackPopup, 0f, 0f, 0);
		g.setClip(0f, 0f, (float)this.w, (float)this.hList);
		g.translate(0f, (float)(-(float)this.cmy));
		for (int j = 0; j < this.list.size(); j++)
		{
			Command command = (Command)this.list.elementAt(j);
			if (j == this.focus && !this.isHide)
			{
				g.setColor(16777215);
				g.fillRect((float)(4 * AvMain.hd), (float)(10 * AvMain.hd + j * this.hItem), (float)(this.wList - 6 * AvMain.hd), (float)this.hItem);
			}
			Canvas.normalFont.drawString(g, command.caption, 10 * AvMain.hd, 10 * AvMain.hd + j * this.hItem + this.hItem / 2 - (int)AvMain.hNormal / 2, 0);
		}
		base.paint(g);
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0005B104 File Offset: 0x00059504
	public static void paintPopupTilte(MyGraphics g, int x, int y, int w, int h, FrameImage img, int color)
	{
		img.drawFrame(0, x, y, 0, g);
		img.drawFrame(2, x + w - img.frameWidth, y, 0, g);
		img.drawFrame(5, x, y + h - img.frameHeight, 0, g);
		img.drawFrame(7, x + w - img.frameWidth, y + h - img.frameHeight, 0, g);
		for (int i = 0; i < (w - img.frameWidth * 2) / img.frameWidth; i++)
		{
			img.drawFrame(1, x + (i + 1) * img.frameWidth, y, 0, g);
			img.drawFrame(6, x + (i + 1) * img.frameWidth, y + h - img.frameHeight, 0, g);
		}
		img.drawFrame(1, x + w - img.frameWidth * 2, y, 0, g);
		img.drawFrame(6, x + w - img.frameWidth * 2, y + h - img.frameHeight, 0, g);
		for (int j = 0; j < (h - img.frameHeight * 2) / img.frameHeight; j++)
		{
			img.drawFrame(3, x, y + (j + 1) * img.frameHeight, 0, g);
			img.drawFrame(4, x + w - img.frameWidth, y + (j + 1) * img.frameHeight, 0, g);
		}
		img.drawFrame(3, x, y + h - img.frameHeight * 2, 0, g);
		img.drawFrame(4, x + w - img.frameWidth, y + h - img.frameHeight * 2, 0, g);
		if (color != -1)
		{
			g.setColor(color);
			g.fillRect((float)(x + img.frameWidth), (float)(y + img.frameHeight), (float)(w - img.frameWidth * 2), (float)(h - img.frameHeight * 2));
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0005B2E4 File Offset: 0x000596E4
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
				if (this.cmy < -this.hList / 2)
				{
					this.cmy = -this.hList / 2;
					this.cmtoY = 0;
					this.vY = 0;
				}
			}
			else if (this.cmy > this.cmyLim)
			{
				if (this.cmy < this.cmyLim + this.hList / 2)
				{
					this.cmy = this.cmyLim + this.hList / 2;
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

	// Token: 0x04000BCF RID: 3023
	public static MenuNPC me;

	// Token: 0x04000BD0 RID: 3024
	private int idUser;

	// Token: 0x04000BD1 RID: 3025
	private int x;

	// Token: 0x04000BD2 RID: 3026
	private int y;

	// Token: 0x04000BD3 RID: 3027
	private int w;

	// Token: 0x04000BD4 RID: 3028
	private int h;

	// Token: 0x04000BD5 RID: 3029
	private int xList;

	// Token: 0x04000BD6 RID: 3030
	private int yList;

	// Token: 0x04000BD7 RID: 3031
	private int wList;

	// Token: 0x04000BD8 RID: 3032
	private int hList;

	// Token: 0x04000BD9 RID: 3033
	private int hItem;

	// Token: 0x04000BDA RID: 3034
	private int focus;

	// Token: 0x04000BDB RID: 3035
	public static FrameImage imgDc = new FrameImage(Image.createImagePNG(T.getPath() + "/race/popup/tile0"), 20 * AvMain.hd, 20 * AvMain.hd);

	// Token: 0x04000BDC RID: 3036
	private MyVector list = new MyVector();

	// Token: 0x04000BDD RID: 3037
	private string nameNPC;

	// Token: 0x04000BDE RID: 3038
	private string[] textChat;

	// Token: 0x04000BDF RID: 3039
	private bool[] isMenu;

	// Token: 0x04000BE0 RID: 3040
	private Image imgBackPopup;

	// Token: 0x04000BE1 RID: 3041
	private Image imgBackChat;

	// Token: 0x04000BE2 RID: 3042
	private int pa;

	// Token: 0x04000BE3 RID: 3043
	private int dyTran;

	// Token: 0x04000BE4 RID: 3044
	private int timeOpen;

	// Token: 0x04000BE5 RID: 3045
	private int pyLast;

	// Token: 0x04000BE6 RID: 3046
	private bool trans;

	// Token: 0x04000BE7 RID: 3047
	private bool isG;

	// Token: 0x04000BE8 RID: 3048
	private long timeDelay;

	// Token: 0x04000BE9 RID: 3049
	private long count;

	// Token: 0x04000BEA RID: 3050
	private long timePoint;

	// Token: 0x04000BEB RID: 3051
	private int vY;

	// Token: 0x04000BEC RID: 3052
	public int cmtoY;

	// Token: 0x04000BED RID: 3053
	public int cmy;

	// Token: 0x04000BEE RID: 3054
	public int cmdy;

	// Token: 0x04000BEF RID: 3055
	public int cmvy;

	// Token: 0x04000BF0 RID: 3056
	public int cmyLim;
}
