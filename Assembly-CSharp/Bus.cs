using System;

// Token: 0x0200002E RID: 46
public class Bus
{
	// Token: 0x060001D8 RID: 472 RVA: 0x0000ED60 File Offset: 0x0000D160
	public void setBus(sbyte dir)
	{
		if (Bus.isRun || (int)GameMidlet.avatar.action == -1)
		{
			return;
		}
		Bus.direct = dir;
		this.y = (int)LoadMap.Hmap * LoadMap.w - Bus.imgBus.getHeight() / AvMain.hd + 10 + 20 * AvMain.hd;
		this.x = Bus.posBusStop.x + 300;
		if ((int)Bus.direct == 1)
		{
			AvCamera.gI().setToPos((float)Bus.posBusStop.x, AvCamera.gI().yCam + (float)(Canvas.hCan / 2));
			AvCamera.gI().xCam = AvCamera.gI().xTo;
			GameMidlet.avatar.y = (GameMidlet.avatar.yCur -= LoadMap.w);
		}
		this.v = (this.g = 15);
		this.count = 0;
		Bus.damToc = 1;
		Bus.isRun = true;
		GameMidlet.avatar.setAction(-1);
		AvCamera.disable = true;
		Bus.isExit = false;
		if ((int)Bus.direct == 1)
		{
			GameMidlet.avatar.ableShow = true;
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000EE90 File Offset: 0x0000D290
	public void update()
	{
		if ((((int)Bus.damToc == 1 && (int)Bus.direct == 1) || ((int)Bus.damToc == -1 && (int)Bus.direct == -1)) && (int)Bus.direct == -1 && !Bus.isExit)
		{
			GlobalService.gI().getHandler(8);
			Bus.isExit = true;
			GameMidlet.avatar.ableShow = true;
		}
		this.x -= this.v;
		this.count += CRes.abs(this.g - this.v / 2);
		if (this.count >= 20)
		{
			this.count = 0;
			this.v -= (int)Bus.damToc;
			if (this.v == 0)
			{
				Bus.damToc = -1;
				this.g = 8;
				GameMidlet.avatar.setAction(0);
				AvCamera.disable = false;
				GameMidlet.avatar.ableShow = false;
				AvCamera.gI().setPosFollowPlayer();
				if (Canvas.isInitChar && Session_ME.gI().isConnected())
				{
					if (LoadMap.TYPEMAP == 9)
					{
						Canvas.welcome = new Welcome();
						Canvas.welcome.initMapScr();
					}
					else if ((int)Bus.direct == 1 && LoadMap.TYPEMAP == 25)
					{
						Canvas.welcome = new Welcome();
						Canvas.welcome.initFarmPath(MapScr.instance);
					}
					else if (LoadMap.TYPEMAP == 13 && Welcome.indexFish < 8)
					{
						Canvas.welcome = new Welcome();
						Canvas.welcome.initFish();
					}
					else if ((int)Bus.direct == 1 && LoadMap.TYPEMAP == 23)
					{
						Canvas.welcome = new Welcome();
						Canvas.welcome.initKhuMuaSam();
					}
				}
			}
			else if ((int)Bus.direct == 1 && (int)Bus.damToc == 1)
			{
				AvCamera.gI().update();
			}
		}
		if ((float)((this.x + 58) * AvMain.hd) < AvCamera.gI().xCam)
		{
			Bus.isRun = false;
			if ((int)Bus.direct == -1)
			{
				Canvas.startWaitDlg();
			}
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000F0C8 File Offset: 0x0000D4C8
	public void paint(MyGraphics g)
	{
		int num = 0;
		if (this.v > 1)
		{
			num = ((Canvas.gameTick % 6 >= 3) ? 0 : 1);
		}
		g.drawImage(Bus.imgBus, (float)(this.x * AvMain.hd), (float)((this.y + num) * AvMain.hd), MyGraphics.TOP | MyGraphics.HCENTER);
	}

	// Token: 0x0400021F RID: 543
	public static Image imgBus;

	// Token: 0x04000220 RID: 544
	private int x;

	// Token: 0x04000221 RID: 545
	private int y;

	// Token: 0x04000222 RID: 546
	private int v;

	// Token: 0x04000223 RID: 547
	private int g;

	// Token: 0x04000224 RID: 548
	private int count;

	// Token: 0x04000225 RID: 549
	public static sbyte damToc;

	// Token: 0x04000226 RID: 550
	public static sbyte direct;

	// Token: 0x04000227 RID: 551
	public static AvPosition posBusStop;

	// Token: 0x04000228 RID: 552
	public static bool isRun;

	// Token: 0x04000229 RID: 553
	public static bool isExit;
}
