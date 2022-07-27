using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class OnSplashScr : MyScreen
{
	// Token: 0x06000A26 RID: 2598 RVA: 0x0006382E File Offset: 0x00061C2E
	public static OnSplashScr gI()
	{
		return (OnSplashScr.me != null) ? OnSplashScr.me : (OnSplashScr.me = new OnSplashScr());
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00063850 File Offset: 0x00061C50
	public override void switchToMe()
	{
		if (Screen.orientation == 1)
		{
			Screen.orientation = 3;
			Canvas.isRotateTop = 2;
		}
		Canvas.listInfoSV.removeAllElements();
		Canvas.transTab = 0;
		Canvas.instance.setSize((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
		onMainMenu.isOngame = true;
		OnSplashScr.imgLogomainMenu = Image.createImagePNG(T.mode[AvMain.hd - 1] + "/hd/on/logo");
		if (OnSplashScr.imgBg == null)
		{
			OnSplashScr.imgBg = Image.createImage("backgroundOn");
		}
		base.switchToMe();
		Canvas.paint.clearImgAvatar();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000638EC File Offset: 0x00061CEC
	public override void update()
	{
		if (OnSplashScr.isOpen)
		{
			return;
		}
		if (this.splashScrStat > 10)
		{
			onMainMenu.gI().switchToMe();
		}
		else if (this.splashScrStat == 1)
		{
			onMainMenu.initSize();
			Canvas.paint.initOngame();
		}
		this.splashScrStat++;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00063949 File Offset: 0x00061D49
	public override void paint(MyGraphics g)
	{
		Canvas.paint.paintDefaultBg(g);
		g.drawImage(OnSplashScr.imgLogomainMenu, (float)Canvas.hw, (float)Canvas.hh, 3);
		Canvas.paintPlus(g);
	}

	// Token: 0x04000D1D RID: 3357
	public static OnSplashScr me;

	// Token: 0x04000D1E RID: 3358
	private static int runningPercent;

	// Token: 0x04000D1F RID: 3359
	public int splashScrStat;

	// Token: 0x04000D20 RID: 3360
	public static Image imgLogomainMenu;

	// Token: 0x04000D21 RID: 3361
	public static Image imgBg;

	// Token: 0x04000D22 RID: 3362
	public static bool isOpen;
}
