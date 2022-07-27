using System;

// Token: 0x020001C1 RID: 449
public class onMainMenu : OnScreen
{
	// Token: 0x06000C30 RID: 3120 RVA: 0x0007A2F4 File Offset: 0x000786F4
	public onMainMenu()
	{
		this.cmdSellect = new Command(T.selectt, 0);
		this.right = new Command("Top", 2);
		base.addCmd(1, 1);
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0007A348 File Offset: 0x00078748
	public static void initImg()
	{
		if (onMainMenu.imgBaiLon != null)
		{
			return;
		}
		onMainMenu.imgBaiLon = new FrameImage[5];
		onMainMenu.imgTitle = new Image[3];
		for (int i = 0; i < 5; i++)
		{
			onMainMenu.imgBaiLon[i] = new FrameImage(Image.createImagePNG(T.mode[AvMain.hd - 1] + "/hd/on/icon" + i), 71 * AvMain.hd, 76 * AvMain.hd);
		}
		for (int j = 0; j < 3; j++)
		{
			onMainMenu.imgTitle[j] = Image.createImagePNG(T.mode[AvMain.hd - 1] + "/hd/on/imgTitle" + j);
		}
		onMainMenu.smallGrey = new HDFont(13, "arialSmall", 12907498, 12907498);
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0007A41C File Offset: 0x0007881C
	public static void resetImg()
	{
		onMainMenu.smallGrey = null;
		onMainMenu.imgBaiLon = null;
		onMainMenu.imgSelected = null;
		onMainMenu.smallGrey = null;
		Menu.me = null;
		OnSplashScr.imgLogomainMenu = null;
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0007A442 File Offset: 0x00078842
	public static void resetIcon()
	{
		onMainMenu.imgBaiLon = null;
		onMainMenu.imgSelected = null;
		OnSplashScr.imgLogomainMenu = null;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0007A456 File Offset: 0x00078856
	public static onMainMenu gI()
	{
		if (onMainMenu.me == null)
		{
			onMainMenu.me = new onMainMenu();
		}
		return onMainMenu.me;
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0007A474 File Offset: 0x00078874
	public override void switchToMe()
	{
		MyScreen.colorBar = MyScreen.colorMiniMap;
		this.selected = 2;
		Canvas.menuMain = null;
		Canvas.endDlg();
		onMainMenu.initImg();
		base.switchToMe();
		this.init();
		if (Canvas.load == 0)
		{
			Canvas.load = 1;
		}
		onMainMenu.isHide = true;
		onMainMenu.isOngame = true;
		ChatTextField.gI().init(Canvas.hCan);
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0007A4DC File Offset: 0x000788DC
	public void init()
	{
		this.numW = 4;
		this.wImg = 70 * AvMain.hd;
		this.wBai = Canvas.w / this.numW;
		if (this.wBai > 100 * AvMain.hd)
		{
			this.wBai = 100 * AvMain.hd;
		}
		this.hBai = onMainMenu.imgBaiLon[0].frameHeight + 5 * AvMain.hd;
		this.y = Canvas.h - this.hBai * 2 + this.hBai / 2;
		this.x = (Canvas.w - this.numW * this.wBai) / 2 + this.wBai / 2;
		onMainMenu.cmxLim = this.numW * this.wBai - Canvas.w;
		if (onMainMenu.cmxLim < 0)
		{
			onMainMenu.cmxLim = 0;
		}
		onMainMenu.initSize();
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0007A5BC File Offset: 0x000789BC
	public static void initSize()
	{
		Canvas.hTab = MyScreen.hText;
		Canvas.h = (int)ScaleGUI.HEIGHT - Canvas.hTab;
		for (int i = 0; i < 3; i++)
		{
			Canvas.posCmd[i].y = Canvas.hCan - Canvas.hTab;
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0007A60D File Offset: 0x00078A0D
	public static void resetSize()
	{
		Canvas.hCan = (Canvas.h = (int)ScaleGUI.HEIGHT);
		Canvas.paint.initPos();
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x0007A62C File Offset: 0x00078A2C
	public override void commandTab(int index)
	{
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					GlobalService.gI().doCommunicate(1);
					Canvas.startWaitDlg();
				}
			}
			else
			{
				onMainMenu.iChangeGame = 1;
				Canvas.cameraList.close();
				GlobalService.gI().joinAvatar();
				Canvas.startWaitDlg();
			}
		}
		else
		{
			switch (this.index)
			{
			case 0:
			case 1:
			case 2:
			case 3:
				this.doGetHandlerCasino(this.index);
				break;
			case 4:
				TransMoneyDlg.gI().show();
				break;
			}
		}
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0007A6D6 File Offset: 0x00078AD6
	public override void close()
	{
		this.commandTab(1);
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0007A6DF File Offset: 0x00078ADF
	public void doGetHandlerCasino(int i)
	{
		GlobalService.gI().getHandler(3);
		MapScr.typeCasino = (sbyte)i;
		Canvas.load = 0;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0007A6F9 File Offset: 0x00078AF9
	public void click()
	{
		onMainMenu.isHide = true;
		this.cmdSellect.perform();
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0007A70C File Offset: 0x00078B0C
	public override void update()
	{
		if (this.timeOpen > 0f)
		{
			this.timeOpen -= 1f;
			if (this.timeOpen == 0f && Canvas.currentMyScreen != PopupShop.me)
			{
				this.click();
			}
		}
		if (this.vX != 0f)
		{
			if (onMainMenu.cmx < 0 || onMainMenu.cmx > onMainMenu.cmxLim)
			{
				if (this.vX > 500f)
				{
					this.vX = 500f;
				}
				else if (this.vX < -500f)
				{
					this.vX = -500f;
				}
				this.vX -= this.vX / 5f;
				if (CRes.abs((int)(this.vX / 10f)) <= 10)
				{
					this.vX = 0f;
				}
			}
			onMainMenu.cmx += (int)(this.vX / 15f);
			onMainMenu.cmtoX = onMainMenu.cmx;
			this.vX -= this.vX / 20f;
		}
		else if (onMainMenu.cmx < 0)
		{
			onMainMenu.cmtoX = 0;
		}
		else if (onMainMenu.cmx > onMainMenu.cmxLim)
		{
			onMainMenu.cmtoX = onMainMenu.cmxLim;
		}
		if (onMainMenu.cmx != onMainMenu.cmtoX)
		{
			onMainMenu.cmvx = onMainMenu.cmtoX - onMainMenu.cmx << 2;
			onMainMenu.cmdx += onMainMenu.cmvx;
			onMainMenu.cmx += onMainMenu.cmdx >> 4;
			onMainMenu.cmdx &= 15;
		}
		if (this.g >= 0)
		{
			this.ylogo += this.dir * this.g;
			this.g += this.dir * this.v;
			if (this.g <= 0)
			{
				this.dir *= -1;
			}
			if (this.ylogo > 0)
			{
				this.dir *= -1;
				this.g -= 2 * this.v;
			}
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0007A94C File Offset: 0x00078D4C
	public override void updateKey()
	{
		this.count += 1f;
		base.updateKey();
		if (Canvas.isPointerClick)
		{
			for (int i = 0; i < T.nameMenuOn.Length; i++)
			{
				if (Canvas.isPointer(this.x + i % this.numW * this.wBai - this.wImg / 2, this.y + i / this.numW * this.hBai - this.wImg / 2, this.wImg, this.wImg + Canvas.normalWhiteFont.getHeight() + 10))
				{
					this.pxLast = Canvas.pxLast;
					this.timeDelay = this.count;
					this.pb = (float)onMainMenu.cmx;
					this.vX = 0f;
					Canvas.isPointerClick = false;
					this.transX = true;
					break;
				}
			}
		}
		if (this.transX)
		{
			float num = this.count - this.timeDelay;
			int num2 = this.pxLast - Canvas.px;
			this.pxLast = Canvas.px;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2f == 0f)
				{
					this.dxTran = (float)Canvas.px;
					this.timePointX = this.count;
				}
				this.vX = 0f;
				if (onMainMenu.cmtoX <= 0 || onMainMenu.cmtoX >= onMainMenu.cmxLim)
				{
					onMainMenu.cmtoX = (int)(this.pb + (float)(Canvas.dx() / 2));
				}
				else
				{
					onMainMenu.cmtoX = (int)(this.pb + (float)num2);
					this.pb = (float)onMainMenu.cmtoX;
				}
				onMainMenu.cmx = onMainMenu.cmtoX;
				if (num < 20f)
				{
					int num3 = (onMainMenu.cmtoX + Canvas.px - (this.x - this.wBai / 2)) / this.wBai;
					int num4 = (Canvas.py - (this.y - this.wBai / 2)) / this.hBai;
					this.index = num4 * this.numW + num3;
					if (this.index < 0)
					{
						this.index = 0;
					}
					if (this.index >= T.nameMenuOn.Length)
					{
						this.index = T.nameMenuOn.Length - 1;
					}
				}
				if (CRes.abs(Canvas.dy()) >= 10 * AvMain.hd || CRes.abs(Canvas.dx()) >= 10 * AvMain.hd)
				{
					onMainMenu.isHide = true;
				}
				else if (num > 3f && num < 8f)
				{
					onMainMenu.isHide = false;
				}
			}
			if (Canvas.isPointerRelease)
			{
				float num5 = this.dxTran - (float)Canvas.px;
				int num6 = (int)(this.count - this.timePointX);
				if (CRes.abs((int)num5) > 40 && num6 < 20 && onMainMenu.cmtoX > 0 && onMainMenu.cmtoX < onMainMenu.cmxLim)
				{
					this.vX = num5 / (float)num6 * 10f;
				}
				this.timePointX = -1f;
				if (CRes.abs(Canvas.dy()) < 10 * AvMain.hd && CRes.abs(Canvas.dx()) < 10 * AvMain.hd)
				{
					if (num <= 4f)
					{
						this.timeOpen = 5f;
						onMainMenu.isHide = false;
					}
					else if (!onMainMenu.isHide)
					{
						this.click();
					}
					Canvas.paint.clickSound();
				}
				this.transX = false;
			}
		}
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x0007ACD0 File Offset: 0x000790D0
	public override void paintMain(MyGraphics g)
	{
		Canvas.paint.paintDefaultBg(g);
		if (onMainMenu.imgBaiLon != null && Canvas.load == -1)
		{
			if (Canvas.iOpenOngame != 2)
			{
				onMainMenu.paintTitle(g, T.listNameGame, Canvas.w / 2, (this.y - this.hBai / 2) / 2);
			}
			g.translate((float)this.x, (float)this.y);
			g.translate((float)(-(float)onMainMenu.cmx), 0f);
			if (Canvas.load == -1)
			{
				for (int i = 0; i < T.nameMenuOn.Length; i++)
				{
					onMainMenu.imgBaiLon[i].drawFrame((this.index != i || onMainMenu.isHide) ? 0 : 1, i % this.numW * this.wBai, i / this.numW * this.hBai, 0, 3, g);
				}
			}
		}
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0007ADBE File Offset: 0x000791BE
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		this.paintMain(g);
		base.paint(g);
		Canvas.paintPlus2(g);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0007ADDC File Offset: 0x000791DC
	public static void paintTitle(MyGraphics g, string text, int x, int y)
	{
		int num = Canvas.numberFont.getWidth(text) + 20;
		int width = onMainMenu.imgTitle[1].getWidth();
		int num2 = num / width;
		for (int i = 0; i < num2; i++)
		{
			g.drawImage(onMainMenu.imgTitle[1], (float)(x - num2 * width / 2 + width * i), (float)(y - onMainMenu.imgTitle[1].getHeight() / 2), 0);
		}
		g.drawImage(onMainMenu.imgTitle[0], (float)(x - num2 * width / 2 - onMainMenu.imgTitle[0].getWidth()), (float)(y - onMainMenu.imgTitle[1].getHeight() / 2), 0);
		g.drawImage(onMainMenu.imgTitle[2], (float)(x - num2 * width / 2 + num2 * width), (float)(y - onMainMenu.imgTitle[1].getHeight() / 2), 0);
		Canvas.numberFont.drawString(g, text, x, y - Canvas.numberFont.getHeight(), 2);
	}

	// Token: 0x04000F9E RID: 3998
	public static onMainMenu me;

	// Token: 0x04000F9F RID: 3999
	public Command cmdSellect;

	// Token: 0x04000FA0 RID: 4000
	private int index;

	// Token: 0x04000FA1 RID: 4001
	private int x;

	// Token: 0x04000FA2 RID: 4002
	private int y;

	// Token: 0x04000FA3 RID: 4003
	private int wBai;

	// Token: 0x04000FA4 RID: 4004
	private int hBai;

	// Token: 0x04000FA5 RID: 4005
	private int numW;

	// Token: 0x04000FA6 RID: 4006
	private int wImg;

	// Token: 0x04000FA7 RID: 4007
	private static FrameImage[] imgBaiLon;

	// Token: 0x04000FA8 RID: 4008
	public static int cmtoX;

	// Token: 0x04000FA9 RID: 4009
	public static int cmx;

	// Token: 0x04000FAA RID: 4010
	public static int cmdx;

	// Token: 0x04000FAB RID: 4011
	public static int cmvx;

	// Token: 0x04000FAC RID: 4012
	public static int cmxLim;

	// Token: 0x04000FAD RID: 4013
	public static bool isOngame;

	// Token: 0x04000FAE RID: 4014
	public new static bool isHide;

	// Token: 0x04000FAF RID: 4015
	public static Image imgSelected;

	// Token: 0x04000FB0 RID: 4016
	public static FontX smallGrey;

	// Token: 0x04000FB1 RID: 4017
	public static Image[] imgTitle;

	// Token: 0x04000FB2 RID: 4018
	public static int iChangeGame;

	// Token: 0x04000FB3 RID: 4019
	public float pb;

	// Token: 0x04000FB4 RID: 4020
	public float vX;

	// Token: 0x04000FB5 RID: 4021
	public float dxTran;

	// Token: 0x04000FB6 RID: 4022
	public float timeOpen;

	// Token: 0x04000FB7 RID: 4023
	public float xCamLast;

	// Token: 0x04000FB8 RID: 4024
	public float count;

	// Token: 0x04000FB9 RID: 4025
	public float timeDelay;

	// Token: 0x04000FBA RID: 4026
	public float timePointX;

	// Token: 0x04000FBB RID: 4027
	public bool transX;

	// Token: 0x04000FBC RID: 4028
	private int pxLast;

	// Token: 0x04000FBD RID: 4029
	private int a;

	// Token: 0x04000FBE RID: 4030
	private int b;

	// Token: 0x04000FBF RID: 4031
	private int v = 2;

	// Token: 0x04000FC0 RID: 4032
	private int g;

	// Token: 0x04000FC1 RID: 4033
	private int ylogo = -40;

	// Token: 0x04000FC2 RID: 4034
	private int dir = 1;
}
