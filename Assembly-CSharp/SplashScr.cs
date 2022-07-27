using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class SplashScr : MyScreen
{
	// Token: 0x06000A8B RID: 2699 RVA: 0x000686A0 File Offset: 0x00066AA0
	public static SplashScr gI()
	{
		return (SplashScr.me != null) ? SplashScr.me : (SplashScr.me = new SplashScr());
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x000686C1 File Offset: 0x00066AC1
	public static void init()
	{
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x000686C4 File Offset: 0x00066AC4
	public override void switchToMe()
	{
		if (SplashScr.me == Canvas.currentMyScreen)
		{
			return;
		}
		SplashScr.splashScrStat = Canvas.getTick();
		SplashScr.imgLogo = Image.createImagePNG(string.Concat(new object[]
		{
			"sp/",
			Screen.width,
			"x",
			Screen.height
		}));
		if (SplashScr.imgLogo == null)
		{
			SplashScr.imgLogo = Image.createImagePNG(string.Concat(new object[]
			{
				"sp/",
				Screen.height,
				"x",
				Screen.width
			}));
		}
		if (SplashScr.imgLogo == null)
		{
			SplashScr.imgLogo = Image.createImagePNG("sp/2048x1536");
		}
		base.switchToMe();
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00068792 File Offset: 0x00066B92
	public override void commandTab(int index)
	{
		if (index != 50)
		{
			if (index == 51)
			{
				this.selectedLanguage(1);
			}
		}
		else
		{
			this.selectedLanguage(0);
		}
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000687C1 File Offset: 0x00066BC1
	public override void updateKey()
	{
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x000687C3 File Offset: 0x00066BC3
	public void switchLogin()
	{
		LoginScr.gI().loadLogin();
		OptionScr.gI().load();
		SplashScr.isSelected = true;
		LoginScr.gI().switchToMe();
		AvatarData.loadIP();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x000687F0 File Offset: 0x00066BF0
	public override void update()
	{
		if ((Canvas.getTick() - SplashScr.splashScrStat) / 1000L > 3L)
		{
			if (onMainMenu.iChangeGame != 0)
			{
				if (onMainMenu.iChangeGame == 2)
				{
					MapScr.gI().switchToMe();
					SplashScr.imgLogo = null;
					onMainMenu.iChangeGame = 0;
					Canvas.paint.resetOngame();
					onMainMenu.resetImg();
					onMainMenu.resetSize();
				}
			}
			else
			{
				this.switchLogin();
			}
		}
		else if (onMainMenu.iChangeGame != 0 && SplashScr.splashScrStat == 0L)
		{
			SplashScr.isOpen = true;
			SplashScr.splashScrStat += 1L;
			return;
		}
		SplashScr.splashScrStat += 1L;
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0006889C File Offset: 0x00066C9C
	private void selectedLanguage(int type)
	{
		LoginScr.isSelectedLanguage = true;
		SplashScr.isSelected = false;
		Canvas.isPointerClick = false;
		Canvas.paint.initString(type);
		OptionScr.gI().mapFocus[4] = type;
		OptionScr.gI().save(0);
		LoginScr.gI().switchToMe();
		LoginScr.gI().load();
		SplashScr.imgLogo = null;
		LoginScr.gI().tfUser.setFocus(true);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00068908 File Offset: 0x00066D08
	public override void paint(MyGraphics g)
	{
		g.setColor(16777215);
		g.fillRect(0f, 0f, (float)((int)ScaleGUI.WIDTH), (float)((int)ScaleGUI.HEIGHT));
		g.drawImageScale(SplashScr.imgLogo, 0, 0, (int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT, 0);
		Canvas.paintPlus(g);
	}

	// Token: 0x04000DBE RID: 3518
	public static SplashScr me;

	// Token: 0x04000DBF RID: 3519
	public static int iDraw = -1;

	// Token: 0x04000DC0 RID: 3520
	public static long splashScrStat;

	// Token: 0x04000DC1 RID: 3521
	public new static Image imgLogo;

	// Token: 0x04000DC2 RID: 3522
	public static bool isOpen;

	// Token: 0x04000DC3 RID: 3523
	public static bool isSelected;

	// Token: 0x04000DC4 RID: 3524
	private bool isDraw;
}
