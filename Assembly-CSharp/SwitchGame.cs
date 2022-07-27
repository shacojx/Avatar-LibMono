using System;
using System.IO;

// Token: 0x02000190 RID: 400
public class SwitchGame : MyScreen
{
	// Token: 0x06000A96 RID: 2710 RVA: 0x0006897A File Offset: 0x00066D7A
	public static SwitchGame gI()
	{
		return (SwitchGame.me != null) ? SwitchGame.me : (SwitchGame.me = new SwitchGame());
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0006899C File Offset: 0x00066D9C
	public void setInfo(int type)
	{
		this.type = type;
		this.last = Canvas.currentMyScreen;
		try
		{
			if (type == 0)
			{
				this.stringElements = new string[]
				{
					"Ongame",
					"Avatar"
				};
				this.imageElements[0] = Image.createImage(T.getPath() + "/on/10");
				this.imageElements[1] = Image.createImage(T.getPath() + "/on/icon57");
				this.aa = this.imageElements[1].getHeight() * 2;
			}
			else
			{
				this.stringElements = new string[]
				{
					"Avatar",
					"Ongame"
				};
				this.imageElements[0] = Image.createImage(T.getPath() + "/on/icon57");
				this.imageElements[1] = Image.createImage(T.getPath() + "/on/10");
				this.aa = this.imageElements[0].getHeight() * 2;
			}
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		this.aa++;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00068AC8 File Offset: 0x00066EC8
	public override void updateKey()
	{
		if (Canvas.isPointerRelease)
		{
			Canvas.isPointerRelease = false;
			for (int i = 0; i < 2; i++)
			{
				if (Canvas.isPoint(10, 10 + i * this.aa, this.aa + Canvas.blackF.getWidth(this.stringElements[i]) + 10, this.aa))
				{
					if (i == 0)
					{
						if (this.type == 0)
						{
							this.last.switchToMe();
						}
						else
						{
							onMainMenu.iChangeGame = 1;
							Canvas.cameraList.close();
							GlobalService.gI().joinAvatar();
							SplashScr.gI().switchToMe();
							onMainMenu.resetSize();
						}
					}
					else if (OnSplashScr.isOpen)
					{
						onMainMenu.isOngame = false;
						MapScr.gI().switchToMe();
						AvCamera.gI().init(LoadMap.TYPEMAP);
					}
					else
					{
						onMainMenu.isOngame = true;
						onMainMenu.gI().switchToMe();
					}
					OnSplashScr.isOpen = false;
					SplashScr.isOpen = false;
				}
			}
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00068BCB File Offset: 0x00066FCB
	public override void update()
	{
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x00068BD0 File Offset: 0x00066FD0
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		g.setColor(16777215);
		g.fillRect(0f, 0f, (float)Canvas.w, (float)((int)ScaleGUI.HEIGHT));
		for (int i = 0; i < 2; i++)
		{
			g.drawImage(this.imageElements[i], (float)(10 + this.aa / 2), (float)(this.aa / 2 + 10 + this.aa * i), 3);
			Canvas.blackF.drawString(g, this.stringElements[i], 20 + this.aa, this.aa / 2 + 10 + this.aa * i - (int)AvMain.hBlack / 2, 0);
		}
	}

	// Token: 0x04000DC5 RID: 3525
	public static SwitchGame me;

	// Token: 0x04000DC6 RID: 3526
	private int type;

	// Token: 0x04000DC7 RID: 3527
	private Image[] imageElements = new Image[2];

	// Token: 0x04000DC8 RID: 3528
	private string[] stringElements;

	// Token: 0x04000DC9 RID: 3529
	private int aa;

	// Token: 0x04000DCA RID: 3530
	private MyScreen last;
}
