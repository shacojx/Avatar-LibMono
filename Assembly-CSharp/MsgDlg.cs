using System;

// Token: 0x02000179 RID: 377
public class MsgDlg : Dialog
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x00060D07 File Offset: 0x0005F107
	public MsgDlg()
	{
		this.hText = (int)AvMain.hBlack;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00060D39 File Offset: 0x0005F139
	public override void show()
	{
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00060D3C File Offset: 0x0005F13C
	public void setInfoLR(string info, Command lef, Command righ)
	{
		this.isWaiting = false;
		Canvas.currentDialog = null;
		this.w = Canvas.w - 100 * AvMain.hd;
		if (this.w > 300 * AvMain.hd)
		{
			this.w = 300 * AvMain.hd;
		}
		this.str = info;
		this.index = 0;
		this.timeDelay = -1L;
		this.left = lef;
		this.right = righ;
		this.center = null;
		this.init();
		Canvas.currentDialog = this;
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00060DC8 File Offset: 0x0005F1C8
	public void setInfoLCR(string info, Command lef, Command cente, Command righ)
	{
		Canvas.currentDialog = null;
		this.isWaiting = false;
		this.w = Canvas.w - 60 * AvMain.hd;
		if (this.w > 350 * AvMain.hd)
		{
			this.w = 350 * AvMain.hd;
		}
		this.str = info;
		this.index = 0;
		this.timeDelay = -1L;
		this.left = lef;
		this.right = righ;
		this.center = cente;
		this.init();
		Canvas.currentDialog = this;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00060E58 File Offset: 0x0005F258
	public void setInfoC(string info, Command cente)
	{
		this.isWaiting = false;
		Canvas.currentDialog = null;
		this.w = Canvas.w - 100 * AvMain.hd;
		if (this.w > 300 * AvMain.hd)
		{
			this.w = 300 * AvMain.hd;
		}
		if (cente == null && this.w > 500)
		{
			this.w = 500;
		}
		this.str = info;
		this.index = 0;
		this.timeDelay = -1L;
		this.left = null;
		this.right = null;
		this.center = null;
		this.center = cente;
		this.init();
		Canvas.currentDialog = this;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00060F0C File Offset: 0x0005F30C
	public void setDelay(int limit)
	{
		this.limitTime = (long)limit;
		this.timeDelay = Canvas.getTick() / 100L;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00060F28 File Offset: 0x0005F328
	public void init()
	{
		if (Canvas.currentDialog == null && this.left == null && this.center == null && this.right == null)
		{
			this.timeOpen = Canvas.getTick() / 100L;
			this.list.removeAllElements();
		}
		if (Canvas.load == 0 && onMainMenu.isOngame)
		{
			Canvas.load = -1;
		}
		MsgDlg.isBack = true;
		if (this.str.Equals(T.pleaseWait))
		{
			this.w = Canvas.hw;
		}
		this.info = Canvas.tempFont.splitFontBStrInLineV(this.str, this.w - this.w / 3);
		this.h = AvMain.hCmd / 2 + 10 + this.info.size() * Canvas.tempFont.getHeight() + 30 * AvMain.hd;
		this.hDu = 0;
		this.x = Canvas.hw - this.w / 2;
		this.y = Canvas.hCan / 2 - this.h / 2;
		if (onMainMenu.isOngame)
		{
			if (this.center != null)
			{
				this.center.x = -1;
			}
			if (this.left != null)
			{
				this.left.x = -1;
			}
			if (this.right != null)
			{
				this.right.x = -1;
			}
		}
		else
		{
			if (this.center == null && this.left != null && this.right != null)
			{
				this.left.x = this.x + this.w / 2 - MyScreen.wTab / 2 - 10 * AvMain.hd;
				this.left.y = (this.right.y = this.y + this.h - AvMain.hCmd / 2);
				this.right.x = this.x + this.w / 2 + MyScreen.wTab / 2 + 10 * AvMain.hd;
			}
			if (this.left != null && this.center != null && this.right != null)
			{
				this.left.x = this.x + this.w / 2 - MyScreen.wTab - 20;
				this.left.y = this.y + this.h - AvMain.hCmd / 2;
				this.right.x = this.x + this.w / 2 + MyScreen.wTab + 20;
				this.right.y = this.y + this.h - AvMain.hCmd / 2;
				this.center.x = Canvas.hw;
				this.center.y = this.y + this.h - AvMain.hCmd / 2;
			}
			if (this.left == null && this.right == null && this.center != null)
			{
				this.center.y = this.y + this.h - AvMain.hCmd / 2;
				this.center.x = Canvas.hw;
			}
		}
		if (onMainMenu.isOngame)
		{
			this.y = Canvas.hCan - 36 * AvMain.hd - this.h - 10;
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00061289 File Offset: 0x0005F689
	public void setIsWaiting(bool isa)
	{
		this.isWaiting = isa;
		this.timeEnd = (long)Canvas.getSecond();
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000612A0 File Offset: 0x0005F6A0
	public override void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		if (this.isWaiting && Canvas.getTick() / 100L - this.timeOpen < 4L)
		{
			return;
		}
		if (MsgDlg.isBack && !onMainMenu.isOngame)
		{
			Canvas.paint.paintTransBack(g);
		}
		if (onMainMenu.isOngame)
		{
			g.drawImageScale(MsgDlg.imgLoadOn, this.x, this.y, this.w, this.h, 0);
		}
		else
		{
			Canvas.paint.paintPopupBack(g, this.x, this.y, this.w, this.h, -1, false);
		}
		int num = this.y + 4 * AvMain.hd + (this.h - 4 * AvMain.hd) / 2 - Canvas.tempFont.getHeight() * this.info.size() / 2 - 2 * AvMain.hd;
		if (this.left != null || this.center != null || this.right != null)
		{
			num = this.y + 4 * AvMain.hd + (this.h - 4 * AvMain.hd - AvMain.hCmd / 2) / 2 - Canvas.tempFont.getHeight() * this.info.size() / 2 - 2 * AvMain.hd;
		}
		if (this.isWaiting)
		{
			MsgDlg.imgLoad.drawFrame(this.num / 2, this.x + this.w / 2 - Canvas.tempFont.getWidth((string)this.info.elementAt(0)) / 2 - 30 * AvMain.hd, this.y + this.h / 2, 0, 3, g);
		}
		for (int i = 0; i < this.info.size(); i++)
		{
			if (onMainMenu.isOngame)
			{
				Canvas.menuFont.drawString(g, (string)this.info.elementAt(i), Canvas.hw, this.y + this.h / 2 - Canvas.menuFont.getHeight() * this.info.size() / 2 + i * Canvas.tempFont.getHeight(), 2);
			}
			else
			{
				Canvas.tempFont.drawString(g, (string)this.info.elementAt(i), Canvas.hw, num + i * Canvas.tempFont.getHeight() - 3, 2);
			}
		}
		if (onMainMenu.isOngame)
		{
			Canvas.paint.paintTabSoft(g);
			Canvas.paint.paintCmdBar(g, this.left, this.center, this.right);
		}
		else
		{
			base.paint(g);
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00061550 File Offset: 0x0005F950
	public override void commandTab(int index)
	{
		if (index != -2)
		{
			if (index != -1)
			{
				Canvas.currentMyScreen.commandTab(index);
			}
			else
			{
				this.isWaiting = false;
				Canvas.currentDialog = null;
			}
		}
		else
		{
			MapScr.gI().doExitGame();
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000615A4 File Offset: 0x0005F9A4
	private void setIndex(int ina)
	{
		if (this.size > 0)
		{
			this.index += ina;
			if (this.index < 0)
			{
				this.index = this.size - 1;
			}
			if (this.index >= this.size)
			{
				this.index = 0;
			}
			Command command = (Command)this.list.elementAt(this.index);
			this.center.action = command.action;
			this.center.indexMenu = command.indexMenu;
		}
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00061638 File Offset: 0x0005FA38
	public override void updateKey()
	{
		if (onMainMenu.isOngame)
		{
			Canvas.paint.updateKeyOn(this.left, this.center, this.right);
		}
		else
		{
			base.updateKey();
		}
		if (this.isWaiting)
		{
			this.num++;
			if (this.num >= 16)
			{
				this.num = 0;
			}
			if ((long)Canvas.getSecond() - this.timeEnd > 30L)
			{
				this.isWaiting = false;
				string str = string.Empty;
				for (int i = 0; i < this.info.size(); i++)
				{
					str = str + (string)this.info.elementAt(i) + " ";
				}
				Canvas.startOKDlg(T.doYouWantExit2, new GlobalLogicHandler.IActionDisconnect());
			}
		}
		if (this.timeDelay == -1L || Canvas.getTick() / 100L - this.timeDelay > this.limitTime)
		{
		}
		if (this.indexLeft > 0)
		{
			this.indexLeft--;
		}
		if (this.indexRight > 0)
		{
			this.indexRight--;
		}
		if (Canvas.isPointerClick)
		{
			if (!this.isWaiting && this.size > 0)
			{
				if (Canvas.isPointer(Canvas.hw - this.wStr / 2 - 11 - MsgDlg.hCell, ((AvMain.hd == 2) ? 0 : (Canvas.normalFont.getHeight() / 2)) + this.y + this.h - (MsgDlg.hCell + 15 * AvMain.hd - 4) + MsgDlg.hCell / 2 + 1 + ((AvMain.hd != 1) ? 0 : -7) + ((AvMain.hd != 0) ? 0 : -3) - MsgDlg.hCell / 2, MsgDlg.hCell * 2, MsgDlg.hCell))
				{
					this.setIndex(-1);
					this.indexLeft = 5;
					Canvas.isPointerRelease = false;
				}
				else if (Canvas.isPointer(Canvas.hw - this.wStr / 2 - 11 - MsgDlg.hCell + this.wStr + 20, ((AvMain.hd == 2) ? 0 : (Canvas.normalFont.getHeight() / 2)) + this.y + this.h - (MsgDlg.hCell + 15 * AvMain.hd - 4) + MsgDlg.hCell / 2 + 1 + ((AvMain.hd != 1) ? 0 : -7) + ((AvMain.hd != 0) ? 0 : -3) - MsgDlg.hCell / 2, MsgDlg.hCell * 2, MsgDlg.hCell))
				{
					this.setIndex(1);
					this.indexRight = 5;
					Canvas.isPointerRelease = false;
				}
			}
			if (!this.isWaiting && Canvas.isPointer(Canvas.hw - MsgDlg.hCell, ((AvMain.hd == 2) ? 0 : (Canvas.normalFont.getHeight() / 2)) + this.y + this.h - (MsgDlg.hCell + 15 * AvMain.hd - 4) + MsgDlg.hCell / 2 + 1 + ((AvMain.hd != 1) ? 0 : -7) + ((AvMain.hd != 0) ? 0 : -3) - MsgDlg.hCell / 2, MsgDlg.hCell * 2, MsgDlg.hCell))
			{
				Canvas.endDlg();
				this.perform(this.center);
				Canvas.isPointerRelease = false;
				Canvas.paint.clickSound();
			}
		}
		base.updateKey();
	}

	// Token: 0x04000CAF RID: 3247
	protected MyVector info;

	// Token: 0x04000CB0 RID: 3248
	private string str = string.Empty;

	// Token: 0x04000CB1 RID: 3249
	public MyVector list = new MyVector();

	// Token: 0x04000CB2 RID: 3250
	public int index;

	// Token: 0x04000CB3 RID: 3251
	public int w;

	// Token: 0x04000CB4 RID: 3252
	public int h;

	// Token: 0x04000CB5 RID: 3253
	public int x;

	// Token: 0x04000CB6 RID: 3254
	public int y;

	// Token: 0x04000CB7 RID: 3255
	public int yWait;

	// Token: 0x04000CB8 RID: 3256
	public bool isWaiting;

	// Token: 0x04000CB9 RID: 3257
	private int num;

	// Token: 0x04000CBA RID: 3258
	private int size;

	// Token: 0x04000CBB RID: 3259
	private int hText;

	// Token: 0x04000CBC RID: 3260
	private int hDu;

	// Token: 0x04000CBD RID: 3261
	private new int indexLeft;

	// Token: 0x04000CBE RID: 3262
	private new int indexRight;

	// Token: 0x04000CBF RID: 3263
	public static int hCell;

	// Token: 0x04000CC0 RID: 3264
	public static FrameImage imgLoad;

	// Token: 0x04000CC1 RID: 3265
	public static Image imgLoadOn;

	// Token: 0x04000CC2 RID: 3266
	private long timeDelay = -1L;

	// Token: 0x04000CC3 RID: 3267
	private long limitTime;

	// Token: 0x04000CC4 RID: 3268
	private long timeEnd;

	// Token: 0x04000CC5 RID: 3269
	public static bool isBack = true;

	// Token: 0x04000CC6 RID: 3270
	public long timeOpen;

	// Token: 0x04000CC7 RID: 3271
	public static sbyte[] fr = new sbyte[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000CC8 RID: 3272
	public int xCow;

	// Token: 0x04000CC9 RID: 3273
	private int wStr;

	// Token: 0x04000CCA RID: 3274
	private int frame;

	// Token: 0x04000CCB RID: 3275
	private int direct;
}
