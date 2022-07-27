using System;

// Token: 0x0200017B RID: 379
public class MyInfoScr : MyScreen
{
	// Token: 0x060009F3 RID: 2547 RVA: 0x00061C74 File Offset: 0x00060074
	public MyInfoScr()
	{
		if (AvMain.hd == 2)
		{
			this.w = 580;
		}
		else
		{
			this.w = 300;
		}
		this.imgIcon = new Image[5];
		for (int i = 0; i < 5; i++)
		{
			this.imgIcon[i] = Image.createImagePNG(T.getPath() + "/myinfo/icon" + i);
		}
		this.imgShadow = Image.createImagePNG(T.getPath() + "/myinfo/shadow");
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00061D19 File Offset: 0x00060119
	public static MyInfoScr gI()
	{
		return (MyInfoScr.me != null) ? MyInfoScr.me : (MyInfoScr.me = new MyInfoScr());
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00061D3A File Offset: 0x0006013A
	public override void switchToMe()
	{
		this.lastScr = Canvas.currentMyScreen;
		base.switchToMe();
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00061D4D File Offset: 0x0006014D
	public new void close()
	{
		this.lastScr.switchToMe();
		this.center = null;
		this.friend = null;
		this.avatar = null;
		this.imgBack = null;
		this.imgTraiTim = null;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00061D80 File Offset: 0x00060180
	public void setInfo(Avatar ava, Avatar friend, string sologan, short idImage, sbyte lv, sbyte perLv, string tenQuanHe, short idActionWedding, string nameAction)
	{
		this.friend = friend;
		this.avatar = ava;
		this.level = lv;
		this.perLevel = perLv;
		this.tenQuanHe = tenQuanHe;
		this.loadImage();
		this.avatar.direct = Base.RIGHT;
		if (friend != null)
		{
			friend.direct = Base.LEFT;
		}
		this.hIndex = this.imgThanh[2].h + 10 * AvMain.hd;
		this.x = (Canvas.w - this.w) / 2;
		this.h = 120 * AvMain.hd + this.hIndex * 3 + 40 * AvMain.hd;
		this.yBackG = 25 * AvMain.hd;
		if (friend != null)
		{
			this.h += 54 * AvMain.hd;
		}
		if (AvMain.hd == 1)
		{
			this.h += 22;
			this.yLine = this.h - (this.hIndex * 3 + 48);
		}
		else
		{
			this.yLine = this.h - (this.hIndex * 3 + 84);
		}
		this.y = (Canvas.hCan - this.h) / 2 + (int)PaintPopup.hTab / 2;
		this.center = null;
		if (nameAction != string.Empty)
		{
			this.center = new Command(nameAction, new MyInfoScr.IActionWedding(idActionWedding));
			this.center.x = this.x + 20 * AvMain.hd + PaintPopup.wButtonSmall / 2;
			this.center.y = this.y + this.yLine + (int)PaintPopup.hTab - PaintPopup.hButtonSmall - 3 * AvMain.hd;
		}
		for (int i = 0; i < 3; i++)
		{
			string st = string.Empty;
			if (i == 0 && this.avatar.lvFarm != -1)
			{
				st = "Lv NT " + this.avatar.lvFarm;
			}
			else
			{
				st = T.myIndex[i] + ((i != 0) ? string.Empty : (string.Empty + this.avatar.lvMain));
			}
			int width = Canvas.fontBlu.getWidth(st);
			if (width > this.xLeft)
			{
				this.xLeft = width;
			}
			width = Canvas.fontBlu.getWidth(this.avatar.indexP[2 + i] + string.Empty);
			if (width > this.xRight)
			{
				this.xRight = width;
			}
		}
		this.xLeft -= 4 * AvMain.hd;
		this.switchToMe();
		Canvas.endDlg();
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0006203C File Offset: 0x0006043C
	private void loadImage()
	{
		if (this.imgBack != null)
		{
			return;
		}
		this.imgBack = Image.createImagePNG(T.getPath() + "/myinfo/back");
		this.imgTraiTim = Image.createImagePNG(T.getPath() + "/myinfo/traitim");
		this.imgThanh = new Image[3];
		for (int i = 0; i < 3; i++)
		{
			this.imgThanh[i] = Image.createImagePNG(T.getPath() + "/myinfo/thanh" + i);
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x000620C9 File Offset: 0x000604C9
	public override void update()
	{
		this.lastScr.update();
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x000620D8 File Offset: 0x000604D8
	public override void updateKey()
	{
		base.updateKey();
		int num = MyScreen.wTab + 20;
		if (Canvas.isPointerClick)
		{
			if (Canvas.isPoint(this.x + this.w + 5 - 5 * AvMain.hd - 20 * AvMain.hd, this.y + MyScreen.hTab - 6 + 3 * AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
			{
				this.isTranKey = true;
				Canvas.isPointerClick = false;
				this.countClose = 5;
			}
			else
			{
				for (int i = 0; i < 2; i++)
				{
					if (Canvas.isPoint(this.x + 12 * AvMain.hd + i * num, this.y - (int)PaintPopup.hTab, num, (int)PaintPopup.hTab))
					{
						this.isTranKey = true;
						Canvas.isPointerClick = false;
						this.indexTab = (sbyte)i;
						break;
					}
				}
			}
		}
		if (this.isTranKey)
		{
			if (Canvas.isPointerDown)
			{
				if ((int)this.countClose == 5 && !Canvas.isPoint(this.x + this.w + 5 - 5 * AvMain.hd - 20 * AvMain.hd, this.y + MyScreen.hTab - 6 + 3 * AvMain.hd - 20 * AvMain.hd, 40 * AvMain.hd, 40 * AvMain.hd))
				{
					this.countClose = 0;
				}
				else if ((int)this.indexTab != -1 && !Canvas.isPoint(this.x + 12 * AvMain.hd + (int)this.indexTab * num, this.y - (int)PaintPopup.hTab, num, (int)PaintPopup.hTab))
				{
					this.indexTab = -1;
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranKey = false;
				if ((int)this.indexTab != -1)
				{
					this.focusTab = (int)this.indexTab;
					this.indexTab = -1;
				}
				else if ((int)this.countClose == 5)
				{
					this.close();
					this.countClose = 0;
				}
			}
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x000622F8 File Offset: 0x000606F8
	public override void paint(MyGraphics g)
	{
		this.lastScr.paintMain(g);
		Canvas.resetTrans(g);
		Canvas.paint.paintBoxTab(g, this.x, this.y - (int)PaintPopup.hTab, this.h + (int)PaintPopup.hTab, this.w, (int)((sbyte)this.focusTab), PaintPopup.gI().wSub, MyScreen.wTab, (int)PaintPopup.hTab, 2, 2, PaintPopup.gI().count, PaintPopup.gI().colorTab, T.nameTab[this.focusTab], (sbyte)((int)this.countClose / 3), -1, false, false, T.nameTab, 0f, null);
		g.translate((float)this.x, (float)this.y);
		int num = 23 * AvMain.hd;
		int num2 = 20 * AvMain.hd;
		for (int i = 0; i < 4; i++)
		{
			g.drawImage(this.imgIcon[i], (float)(16 * AvMain.hd + num / 2), (float)(20 * AvMain.hd + num / 2 + i * num), 3);
		}
		Canvas.normalFont.drawString(g, this.avatar.money[0] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 - Canvas.normalFont.getHeight() / 2, 0);
		Canvas.normalFont.drawString(g, this.avatar.money[2] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num - Canvas.normalFont.getHeight() / 2, 0);
		Canvas.normalFont.drawString(g, this.avatar.money[3] + string.Empty, 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num * 2 - Canvas.normalFont.getHeight() / 2, 0);
		Canvas.normalFont.drawString(g, string.Concat(new object[]
		{
			this.avatar.lvMain,
			"+",
			this.avatar.perLvMain,
			"%"
		}), 16 * AvMain.hd + num, 20 * AvMain.hd + num / 2 + num * 3 - Canvas.normalFont.getHeight() / 2, 0);
		g.drawImage(this.imgBack, (float)(this.w - this.imgBack.w - 20 * AvMain.hd), (float)this.yBackG, 0);
		this.avatar.paintIcon(g, this.w - this.imgBack.w - 20 * AvMain.hd + 40 * AvMain.hd, this.yBackG + this.imgBack.h - 10 * AvMain.hd, true);
		if (this.friend == null)
		{
			g.drawImage(this.imgShadow, (float)(this.w - 20 * AvMain.hd - this.imgShadow.w / 2 - 20 * AvMain.hd), (float)(this.yBackG + this.imgBack.h - 10 * AvMain.hd - this.imgShadow.h / 2), 3);
		}
		else
		{
			this.friend.paintIcon(g, this.w - 20 * AvMain.hd - this.imgShadow.w / 2 - 20 * AvMain.hd, this.yBackG + this.imgBack.h - 10 * AvMain.hd, true);
			Canvas.fontBlu.drawString(g, this.tenQuanHe, this.w - this.imgBack.w / 2 - 20 * AvMain.hd, this.yBackG + AvMain.hd + this.imgBack.h, 2);
			g.drawImage(this.imgTraiTim, (float)(this.w - this.imgBack.w / 2 - 20 * AvMain.hd), (float)(this.yBackG + 2 * AvMain.hd + this.imgBack.h + Canvas.fontBlu.getHeight() + this.imgTraiTim.h / 2), 3);
			Canvas.fontBlu.drawString(g, string.Concat(new object[]
			{
				this.level,
				"+",
				this.perLevel,
				"%"
			}), this.w - this.imgBack.w / 2 - 20 * AvMain.hd, this.yBackG - AvMain.hd + this.imgBack.h + Canvas.fontBlu.getHeight() + this.imgTraiTim.h, 2);
			g.drawImage(this.imgThanh[2], (float)(this.w - this.imgBack.w / 2 - 20 * AvMain.hd), (float)(this.yLine - this.imgThanh[2].h), 3);
			int num3 = (int)this.perLevel * this.imgThanh[1].w / 100;
			g.setClip((float)(this.w - this.imgBack.w / 2 - 20 * AvMain.hd - this.imgThanh[2].w / 2), (float)(this.yLine - this.imgThanh[2].h - this.imgThanh[2].h / 2), (float)num3, (float)this.imgThanh[2].h);
			g.drawImage(this.imgThanh[1], (float)(this.w - this.imgBack.w / 2 - 20 * AvMain.hd), (float)(this.yLine - this.imgThanh[2].h), 3);
			g.setClip(0f, (float)this.yLine, (float)this.w, (float)(this.h - (int)PaintPopup.hTab - this.yLine));
			g.drawImage(this.imgThanh[0], (float)(this.w - this.imgBack.w / 2 - 20 * AvMain.hd), (float)(this.yLine - this.imgThanh[2].h - 2 * AvMain.hd), 3);
		}
		g.translate(0f, (float)this.yLine);
		g.setColor(29068);
		g.fillRect((float)(20 * AvMain.hd), 0f, (float)(this.w - 40 * AvMain.hd), 1f);
		g.setColor(12255224);
		g.fillRect((float)(20 * AvMain.hd), 1f, (float)(this.w - 40 * AvMain.hd), 1f);
		int num4 = (this.h - this.yLine - (int)PaintPopup.hTab - this.hIndex * 3) / 2 - 2;
		for (int j = 0; j < 3; j++)
		{
			g.drawImage(this.imgThanh[2], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2), (float)(num4 + this.hIndex / 2 + this.hIndex * j), 3);
			g.drawImage(this.imgThanh[2], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2), (float)(num4 + this.hIndex / 2 + this.hIndex * j), 3);
			if (j == 0)
			{
				if (this.avatar.lvFarm != -1)
				{
					Canvas.fontBlu.drawString(g, "Lv NT " + this.avatar.lvFarm, num2 + this.xLeft - 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
				}
				else
				{
					Canvas.fontBlu.drawString(g, T.myIndex[j] + ((j != 0) ? string.Empty : (string.Empty + this.avatar.lvMain)), num2 + this.xLeft - 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
				}
			}
			else
			{
				Canvas.fontBlu.drawString(g, T.myIndex[j] + ((j != 0) ? string.Empty : (string.Empty + this.avatar.lvMain)), num2 + this.xLeft - 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
			}
			Canvas.fontBlu.drawString(g, T.myIndex[3 + j], this.w - num2 - this.xRight - this.imgThanh[2].w - 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * j - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 1);
		}
		if (this.avatar.lvFarm != -1)
		{
			Canvas.fontBlu.drawString(g, this.avatar.perLvFarm + "%", num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num4 + this.hIndex / 2 - Canvas.fontBlu.getHeight() / 2 - 3 * AvMain.hd, 0);
		}
		else
		{
			Canvas.fontBlu.drawString(g, this.avatar.perLvMain + "%", num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num4 + this.hIndex / 2 - Canvas.fontBlu.getHeight() / 2 - 3 * AvMain.hd, 0);
		}
		for (int k = 0; k < 3; k++)
		{
			if (k > 0)
			{
				Canvas.fontBlu.drawString(g, this.avatar.indexP[k - 1] + string.Empty, num2 + this.xLeft + this.imgThanh[2].w + 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * k - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 0);
			}
			Canvas.fontBlu.drawString(g, this.avatar.indexP[2 + k] + string.Empty, this.w - num2 - this.xRight + 2 * AvMain.hd, num4 + this.hIndex / 2 + this.hIndex * k - Canvas.fontBlu.getHeight() / 2 - 2 * AvMain.hd, 0);
		}
		int num5 = 0;
		if (AvMain.hd == 2)
		{
			num5 = 1;
		}
		for (int l = 0; l < 3; l++)
		{
			int num3;
			if (l == 0)
			{
				if (this.avatar.lvFarm != -1)
				{
					num3 = (int)this.avatar.perLvFarm * this.imgThanh[1].w / 100;
				}
				else
				{
					num3 = (int)this.avatar.perLvMain * this.imgThanh[1].w / 100;
				}
			}
			else
			{
				num3 = (int)this.avatar.indexP[l - 1] * this.imgThanh[1].w / 100;
			}
			g.setClip((float)(num2 + this.xLeft + 1), (float)(num4 + this.hIndex * l), (float)num3, (float)this.hIndex);
			g.drawImage(this.imgThanh[1], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2 + 1), (float)(num4 + this.hIndex / 2 + this.hIndex * l - num5), 3);
			num3 = (int)this.avatar.indexP[2 + l] * this.imgThanh[1].w / 100;
			g.setClip((float)(this.w - num2 - this.xRight - this.imgThanh[2].w + 2), (float)(num4 + this.hIndex * l), (float)num3, (float)this.hIndex);
			g.drawImage(this.imgThanh[1], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2 + 1), (float)(num4 + this.hIndex / 2 + this.hIndex * l - num5), 3);
		}
		g.setClip(0f, 0f, (float)this.w, (float)(this.h - (int)PaintPopup.hTab - this.yLine));
		for (int m = 0; m < 3; m++)
		{
			g.drawImage(this.imgThanh[0], (float)(num2 + this.xLeft + this.imgThanh[2].w / 2 + 1), (float)(num4 + this.hIndex / 2 + this.hIndex * m - 2 * AvMain.hd), 3);
			g.drawImage(this.imgThanh[0], (float)(this.w - num2 - this.xRight - this.imgThanh[2].w / 2 + 1), (float)(num4 + this.hIndex / 2 + this.hIndex * m - 2 * AvMain.hd), 3);
		}
		base.paint(g);
		g.setColor(0);
		g.drawRect((float)(this.x + this.w + 5 - 5 * AvMain.hd - 20 * AvMain.hd), (float)(this.y + MyScreen.hTab - 6 + 3 * AvMain.hd - 20 * AvMain.hd), (float)(40 * AvMain.hd), (float)(40 * AvMain.hd));
	}

	// Token: 0x04000CD3 RID: 3283
	public static MyInfoScr me;

	// Token: 0x04000CD4 RID: 3284
	public int x;

	// Token: 0x04000CD5 RID: 3285
	public int y;

	// Token: 0x04000CD6 RID: 3286
	public int w;

	// Token: 0x04000CD7 RID: 3287
	public int h;

	// Token: 0x04000CD8 RID: 3288
	public int yLine;

	// Token: 0x04000CD9 RID: 3289
	public int hIndex;

	// Token: 0x04000CDA RID: 3290
	public int yBackG;

	// Token: 0x04000CDB RID: 3291
	public int xLeft;

	// Token: 0x04000CDC RID: 3292
	public int xRight;

	// Token: 0x04000CDD RID: 3293
	public sbyte countClose;

	// Token: 0x04000CDE RID: 3294
	private MyScreen lastScr;

	// Token: 0x04000CDF RID: 3295
	public Image[] imgIcon;

	// Token: 0x04000CE0 RID: 3296
	public Image[] imgThanh;

	// Token: 0x04000CE1 RID: 3297
	private Image imgBack;

	// Token: 0x04000CE2 RID: 3298
	private Image imgTraiTim;

	// Token: 0x04000CE3 RID: 3299
	public Image imgShadow;

	// Token: 0x04000CE4 RID: 3300
	private Avatar friend;

	// Token: 0x04000CE5 RID: 3301
	private Avatar avatar;

	// Token: 0x04000CE6 RID: 3302
	private sbyte level;

	// Token: 0x04000CE7 RID: 3303
	private sbyte perLevel;

	// Token: 0x04000CE8 RID: 3304
	private string tenQuanHe = string.Empty;

	// Token: 0x04000CE9 RID: 3305
	private int focusTab;

	// Token: 0x04000CEA RID: 3306
	private bool isTranKey;

	// Token: 0x04000CEB RID: 3307
	private sbyte indexTab = -1;

	// Token: 0x0200017C RID: 380
	private class IActionWedding : IAction
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x0006313B File Offset: 0x0006153B
		public IActionWedding(short idActionWedding)
		{
			this.idActionWedding = idActionWedding;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0006314A File Offset: 0x0006154A
		public void perform()
		{
			GlobalService.gI().doRequestCmdRotate((int)this.idActionWedding, -1);
			MyInfoScr.me.close();
			Canvas.startWaitDlg();
		}

		// Token: 0x04000CEC RID: 3308
		private short idActionWedding;
	}
}
